using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Intcode;

namespace AdventOfCode2019.Puzzles.Day5
{
    [Day(5)]
    public class SunnyWithAChanceOfAsteroids : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            int[] registers = line.Split(',').Select(x => int.Parse(x)).ToArray();

            var program = new IntcodeProgram(registers);
            var outputChannel = Channel.CreateUnbounded<int>();
            await program.RunAsync(await 1.ToChannelAsync(), outputChannel);
            return (await outputChannel.Reader.ReadAllAsync().LastAsync()).ToString();
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            int[] registers = line.Split(',').Select(x => int.Parse(x)).ToArray();

            var program = new IntcodeProgram(registers);
            var outputChannel = Channel.CreateUnbounded<int>();
            await program.RunAsync(await 5.ToChannelAsync(), outputChannel);
            return (await outputChannel.Reader.ReadAllAsync().LastAsync()).ToString();
        }
    }
}
