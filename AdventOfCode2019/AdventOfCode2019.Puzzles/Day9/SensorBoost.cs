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
        private IIntcodeProgram _program;

        public SensorBoost(IIntcodeProgram program)
        {
            _program = program;
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();
            _program.Init(registers);
            var outputChannel = Channel.CreateUnbounded<long>();
            await _program.RunAsync(await 1.ToChannelAsync(), outputChannel);

            return (await outputChannel.Reader.ReadAllAsync().FirstAsync()).ToString();
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();
            _program.Init(registers);

            var outputChannel = Channel.CreateUnbounded<long>();
            await _program.RunAsync(await 2.ToChannelAsync(), outputChannel);

            return (await outputChannel.Reader.ReadAllAsync().FirstAsync()).ToString();
        }
    }
}
