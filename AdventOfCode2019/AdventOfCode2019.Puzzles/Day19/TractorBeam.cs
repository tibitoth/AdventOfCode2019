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

namespace AdventOfCode2019.Puzzles.Day19
{
    [Day(19)]
    public class TractorBeam : IPuzzleSolver
    {
        private readonly IIntcodeProgram _intcodeProgram;

        public TractorBeam(IIntcodeProgram intcodeProgram)
        {
            _intcodeProgram = intcodeProgram;
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

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
