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

namespace AdventOfCode2019.Puzzles.Day7
{
    [Day(7)]
    public class AmplificationCircuit : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            return (await GetMaxThrusterSignalAsync(input)).MaxSignal.ToString();
        }

        internal async Task<(int MaxSignal, int[] MaxPermutation)> GetMaxThrusterSignalAsync(Stream input)
        {
            var line = await input.ReadLineAsync();
            int[] registers = line.Split(',').Select(x => int.Parse(x)).ToArray();

            var max = int.MinValue;
            int[] maxPermutation = null;

            foreach (var p in Enumerable.Range(0,5).GetPermutations())
            {
                var permutation = p.ToArray();
                var amplifiers = new IntcodeProgram[5];
                for (int i = 0; i < amplifiers.Length; i++)
                {
                    amplifiers[i] = new IntcodeProgram(registers.ToArray()); //copy
                }

                var outputChannel = Channel.CreateUnbounded<int>();
                await outputChannel.Writer.WriteAsync(0);

                for (int i = 0; i < amplifiers.Length; i++)
                {
                    var inputChannel = Channel.CreateUnbounded<int>();
                    await inputChannel.Writer.WriteAsync(permutation[i]);
                    await inputChannel.Writer.WriteAsync(await outputChannel.Reader.ReadAsync());

                    outputChannel = Channel.CreateUnbounded<int>();

                    await amplifiers[i].RunAsync(inputChannel, outputChannel);
                }

                var result = await outputChannel.Reader.ReadAsync();

                if (result > max)
                {
                    max = result;
                    maxPermutation = permutation;
                }
            }

            return (max, maxPermutation);
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
