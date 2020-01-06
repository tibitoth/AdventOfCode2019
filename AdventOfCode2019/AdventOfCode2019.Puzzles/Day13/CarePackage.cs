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
using Newtonsoft.Json;

namespace AdventOfCode2019.Puzzles.Day13
{
    [Day(13)]
    public class CarePackage : IPuzzleSolver
    {
        private readonly IIntcodeProgram _intcodeProgram;
        private readonly ILogger<CarePackage> _logger;

        private Dictionary<(long x, long y), TileType> _tiles;
        private long _score;
        private bool _initialized;
        private List<long> _movementsHistory = new List<long>();
        private int _currentMovementIndex = 0;

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
            _initialized = false;

            while (await output.Reader.WaitToReadAsync())
            {
                var coord = (x: await output.Reader.ReadAsync(), y: await output.Reader.ReadAsync());
                var data = await output.Reader.ReadAsync();
                if (coord.x == -1 && coord.y == 0)
                {
                    _score = data;

                    if (!_initialized)
                    {
                        _initialized = true;
                        await InputAsync(input);
                    }

                    Draw();
                }
                else
                {
                    var type = (TileType)data;
                    if (type == TileType.Ball)
                    {
                        _tiles[coord] = type;
                        
                    }
                    else if (type == TileType.Paddle)
                    {
                        _tiles[coord] = type;
                    }
                    else if (type == TileType.Block || type == TileType.Empty || type == TileType.Wall)
                    {
                        _tiles[coord] = type;
                    }

                    Draw();

                    if (type == TileType.Ball)
                    {
                        await InputAsync(input);
                    }
                }
            }
        }

        private async Task InputAsync(Channel<long> input)
        {
            if (!_initialized) return;

            if (!input.Reader.TryRead(out long activeInput) && await input.Writer.WaitToWriteAsync())
            {
                long? i = null;
                do
                {
                    if (_currentMovementIndex < _movementsHistory.Count)
                    {
                        i = _movementsHistory[_currentMovementIndex++];
                    }
                    else
                    {
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.RightArrow:
                                _movementsHistory.Add(1);
                                _currentMovementIndex++;
                                i = 1;
                                break;
                            case ConsoleKey.LeftArrow:
                                _movementsHistory.Add(-1);
                                _currentMovementIndex++;
                                i = -1;
                                break;
                            case ConsoleKey.S:
                                var json = JsonConvert.SerializeObject(_movementsHistory);
                                File.WriteAllText("day13.data", json);
                                break;
                            case ConsoleKey.L:
                                var json2 = File.ReadAllText("day13.data");
                                _movementsHistory = JsonConvert.DeserializeObject<List<long>>(json2);
                                _currentMovementIndex = 0;
                                break;
                            default:
                                _movementsHistory.Add(0);
                                _currentMovementIndex++;
                                i = 0;
                                break;
                        }
                    }
                } while (i == null);

                await input.Writer.WriteAsync(i.Value);
            }
            else
            {
                await input.Writer.WriteAsync(activeInput);
            }
        }

        private void Draw()
        {
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

            await RunGameAsync(output, inputChannel);

            await programTask;

            return _score.ToString(); // 13331
        }
    }
}
