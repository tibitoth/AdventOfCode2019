using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

                var output = "0".ToMemoryStream();

                for (int i = 0; i < amplifiers.Length; i++)
                {
                    // TODO very ugly
                    using var programInput = new MemoryStream();
                    using var writer = new StreamWriter(programInput);
                    await writer.WriteLineAsync(permutation[i].ToString());
                    await writer.FlushAsync();
                    output.Seek(0, SeekOrigin.Begin);
                    await output.CopyToAsync(programInput);
                    programInput.Seek(0, SeekOrigin.Begin);

                    output = new MemoryStream();

                    amplifiers[i].Run(programInput, output);
                }

                var result = int.Parse(await output.AsAsyncEnumerable().LastAsync());

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
