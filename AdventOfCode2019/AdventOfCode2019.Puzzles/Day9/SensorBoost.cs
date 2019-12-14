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
using AdventOfCode2019.Puzzles.Tests.Extensions;

namespace AdventOfCode2019.Puzzles.Day9
{
    [Day(9)]
    public class SensorBoost : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            var program = new IntcodeProgram(registers);
            var outputChannel = Channel.CreateUnbounded<long>();
            await program.RunAsync(await 1.ToChannelAsync(), outputChannel);

            var x = await outputChannel.Reader.ReadAllAsync().ToListAsync();

            return (x.First()).ToString();
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
