using System;
using System.Collections.Generic;
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

namespace AdventOfCode2019.Puzzles.Day19
{
    [Day(19)]
    public class TractorBeam : IPuzzleSolver
    {
        private readonly IIntcodeProgram _intcodeProgram;
        private readonly ILogger<TractorBeam> _logger;

        public TractorBeam(IIntcodeProgram intcodeProgram, ILogger<TractorBeam> logger)
        {
            _intcodeProgram = intcodeProgram;
            _logger = logger;
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            var map = new Dictionary<(long x, long y), long>();

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    var outputChannel = Channel.CreateUnbounded<long>();
                    var inputChannel = Channel.CreateUnbounded<long>();

                    _intcodeProgram.Init(registers.ToArray()); // copy

                    await inputChannel.Writer.WriteAsync(x);
                    await inputChannel.Writer.WriteAsync(y);
                    await _intcodeProgram.RunAsync(inputChannel, outputChannel);

                    map[(x, y)] = await outputChannel.Reader.ReadAsync();
                }
            }

            return map.Count(x => x.Value == 1).ToString();
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            var map = new Dictionary<(long x, long y), long>();
            var found = false;

            async Task<long> IsBeamValueAsync(long x, long y)
            {
                var outputChannel = Channel.CreateUnbounded<long>();
                var inputChannel = Channel.CreateUnbounded<long>();

                _intcodeProgram.Init(registers.ToArray()); // copy

                await inputChannel.Writer.WriteAsync(x);
                await inputChannel.Writer.WriteAsync(y);
                await _intcodeProgram.RunAsync(inputChannel, outputChannel);

                return await outputChannel.Reader.ReadAsync();
            }

            for (int i = 10; !found; i++)
            {
                Parallel.For(0, i, j =>
                {
                    map[(i, j)] = IsBeamValueAsync(i, j).GetAwaiter().GetResult();
                    map[(j, i)] = IsBeamValueAsync(j, i).GetAwaiter().GetResult();
                });

                _logger.LogDebug("Finding enough space");

                foreach (var m in map)
                {
                    if (m.Value == 1 && map.ContainsKey((m.Key.x + 100, m.Key.y)) && map[(m.Key.x + 100, m.Key.y)] == 1 &&
                        map.ContainsKey((m.Key.x, m.Key.y + 100)) && map[(m.Key.x, m.Key.y + 100)] == 1)
                    {
                        _logger.LogDebug("Enough space found ({x}, {y})", m.Key.x, m.Key.y);

                        return (m.Key.x * 10000 + m.Key.y).ToString();
                    }
                }

                _logger.LogDebug("Not found enough");
            }

            return null;
        }
    }
}
