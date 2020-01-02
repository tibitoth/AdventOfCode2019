using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using AdventOfCode2019.Puzzles.Intcode;

namespace AdventOfCode2019.Puzzles.Day13
{
    internal class Tile
    {
        public TileType TileType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    internal enum TileType
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        Paddle = 3,
        Ball = 4,
    }

    internal enum BallDirection
    {
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 3, 
        BottomRight = 4, 
    }

    [Day(13)]
    public class CarePackage : IPuzzleSolver
    {
        private readonly IIntcodeProgram _intcodeProgram;

        private Dictionary<(long x, long y), TileType> _tiles;
        //private TileType[,] _tiles;
        private (long x, long y) _ball;
        private (long x, long y) _paddle;
        private long _score;

        public CarePackage(IIntcodeProgram intcodeProgram)
        {
            _intcodeProgram = intcodeProgram;
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();
            _intcodeProgram.Init(registers);
            var output = Channel.CreateUnbounded<long>();
            await _intcodeProgram.RunAsync(Channel.CreateUnbounded<long>(), output);

            return (await GetBlockTileCountAsync(output)).ToString();
        }

        internal async Task<int> GetBlockTileCountAsync(Channel<long> output)
        {
            return await output.Reader
                .ReadAllAsync()
                .Batch(3)
                .CountAsync(t => (TileType)t.Skip(2).Single() == TileType.Block);
        }

        private async Task RunGameAsync(Channel<long> output, Channel<long> input)
        {
            _tiles = new Dictionary<(long x, long y), TileType>();
            _ball = (0, 0);
            var paddleTargetX = 0;

            while (await output.Reader.WaitToReadAsync())
            //await foreach (var tileData in output.Reader.ReadAllAsync().Batch(3))
            {
                var coord = (x: await output.Reader.ReadAsync(), y: await output.Reader.ReadAsync());
                var data = await output.Reader.ReadAsync();
                if (coord.x == -1 && coord.y == 0)
                {
                    _score = data;
                    //var width = _tilesDictionary.Keys.Max(k => k.x) - _tilesDictionary.Keys.Min(k => k.x);
                    //var height = _tilesDictionary.Keys.Max(k => k.y) - _tilesDictionary.Keys.Min(k => k.y);
                    //_tiles = new TileType[width, height];
                    await Task.Delay(1000);
                }
                else
                {
                    var type = (TileType)data;
                    if (type == TileType.Ball)
                    {
                        //if (paddleTargetX > 0)
                        //{
                        //    input.Reader.TryRead(out _);
                        //    await input.Writer.WriteAsync(1);
                        //    paddleTargetX--;
                        //}
                        //else if (paddleTargetX == 0)
                        //{
                        //    input.Reader.TryRead(out _);
                        //    input.Writer.TryWrite(0);
                        //}
                        //else if (paddleTargetX < 0)
                        //{
                        //    input.Reader.TryRead(out _);
                        //    await input.Writer.WriteAsync(-1);
                        //    paddleTargetX--;
                        //}

                        _ball = coord;
                        _tiles[coord] = type;

                        // ball hit paddle, need to move to the next calculated hit location
                        //if (_paddle.y == _ball.y + 1 && _paddle.x == _ball.x)
                        //{
                        //    paddleTargetX = 6;
                        //}

                       

                        Draw();
                        await Task.Delay(1000);
                    }
                    else if (type == TileType.Paddle)
                    {
                        _paddle = coord;
                        _tiles[coord] = type;
                        Draw();
                        await Task.Delay(1000);
                    } 
                    else if (type == TileType.Block || type == TileType.Empty || type == TileType.Wall)
                    {
                        _tiles[coord] = type;
                        Draw();
                        //_tiles[coord.x, coord.y] = type;
                    }
                }

                
            }
        }

        private void Draw()
        {
            //Console.Clear();
            Console.WriteLine($"Score: {_score}");
            foreach (var g in _tiles.OrderBy(k => k.Key.y).GroupBy(k => k.Key.y))
            {
                var line = new string(g.OrderBy(g => g.Key.x).Select(g => g.Value switch
                {
                    TileType.Wall => '#',
                    TileType.Empty => ' ',
                    TileType.Paddle => 'T',
                    TileType.Block => 'X',
                    TileType.Ball => '0',
                }).ToArray());
                Console.WriteLine(line);
            }
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();
            _intcodeProgram.Init(registers);

            // free coins, play mode
            registers[0] = 2;

            var output = Channel.CreateUnbounded<long>();
            var inputChannel = Channel.CreateBounded<long>(1);
            

            var programTask = _intcodeProgram.RunAsync(inputChannel, output);

            var gameTask = RunGameAsync(output, inputChannel);
            var idleKeyboardTask = Task.Run(async () =>
            {
                while (!gameTask.IsCompleted)
                {
                    lock (_intcodeProgram)
                    {
                        inputChannel.Writer.TryWrite(0);
                    }
                }
            });

            var kKeyboardTask = Task.Run(() =>
            {
                while (!gameTask.IsCompleted)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        lock (_intcodeProgram)
                        {
                            inputChannel.Reader.TryRead(out _);
                            inputChannel.Writer.TryWrite(1);
                        }
                    }

                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        lock (_intcodeProgram)
                        {
                            inputChannel.Reader.TryRead(out _);
                            inputChannel.Writer.TryWrite(-1);
                        }
                    }
                }
            });


            //LowLevelKeyboardHook kbh = new LowLevelKeyboardHook();
            //kbh.OnKeyPressed += async (sender, key) =>
            //{
            //    if (key == ConsoleKey.RightArrow)
            //    {
            //        inputChannel.Reader.TryRead(out _);
            //        await inputChannel.Writer.WriteAsync(1);
            //    }

            //    if (key == ConsoleKey.LeftArrow)
            //    {
            //        inputChannel.Reader.TryRead(out _);
            //        await inputChannel.Writer.WriteAsync(-1);
            //    }
            //};



            //kbh.HookKeyboard();

            await gameTask;
            await programTask;

            //kbh.UnHookKeyboard();

            return _score.ToString();
        }
    }
}
