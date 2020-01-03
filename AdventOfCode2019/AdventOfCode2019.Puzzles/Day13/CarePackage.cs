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
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<CarePackage> _logger;

        private Dictionary<(long x, long y), TileType> _tiles;
        //private TileType[,] _tiles;
        private (long x, long y) _ball;
        private (long x, long y) _paddle;
        private long _score;
        private bool _initialized;

        public CarePackage(IIntcodeProgram intcodeProgram, ILogger<CarePackage> logger)
        {
            _intcodeProgram = intcodeProgram;
            _logger = logger;
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
            _initialized = false;

            while (await output.Reader.WaitToReadAsync())
            {
                var coord = (x: await output.Reader.ReadAsync(), y: await output.Reader.ReadAsync());
                var data = await output.Reader.ReadAsync();
                if (coord.x == -1 && coord.y == 0)
                {
                    _logger.LogDebug("Getting Score data");
                    _initialized = true;
                    _score = data;
                    Input(input);
                    Draw();

                    await Task.Delay(0);
                }
                else
                {
                    var type = (TileType)data;
                    if (type == TileType.Ball)
                    {
                        _logger.LogDebug("Getting ball data");

                        Input(input);

                        _ball = coord;
                        _tiles[coord] = type;

                        Draw();
                        await Task.Delay(0);
                    }
                    else if (type == TileType.Paddle)
                    {
                        _logger.LogDebug("Getting paddle data");

                        Input(input);

                        _paddle = coord;
                        _tiles[coord] = type;
                        Draw();
                        await Task.Delay(0);
                    }
                    else if (type == TileType.Block || type == TileType.Empty || type == TileType.Wall)
                    {
                        //_logger.LogDebug("Getting {0} data", type);

                        Input(input);

                        _tiles[coord] = type;
                        Draw();
                    }
                }
            }
        }

        private void Input(Channel<long> input)
        {
            if (!_initialized) return;

            //if (Console.KeyAvailable)
            //{
            //    var key = Console.ReadKey(true);
            //    if (key.Key == ConsoleKey.RightArrow)
            //    {
            //        input.Reader.TryRead(out _);
            //        var r = input.Writer.TryWrite(1);
            //    }

            //    if (key.Key == ConsoleKey.LeftArrow)
            //    {
            //        input.Reader.TryRead(out _);
            //        var r = input.Writer.TryWrite(-1);
            //    }
            //}
            //else
            //{
            //    if (input.Reader.TryRead(out long x))
            //    {
            //        var r = input.Writer.TryWrite(x);
            //    }
            //    else
            //    {
            //        var r = input.Writer.TryWrite(0);
            //    }
            //}
        }

        private void Draw()
        {
            //if (!_initialized) return;

            //Console.Clear();
            var sb = new StringBuilder();
            sb.Append($"Score: {_score}\n");
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
                sb.Append(line);
                sb.Append(Environment.NewLine);
            }

            Console.WriteLine(sb.ToString());
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

            await gameTask;
            await programTask;

            return _score.ToString();
        }
    }
}
