using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Day2
{
    [Day(2)]
    public class ProgramAlarm1202 : IPuzzleSolver
    {
        public async Task<Stream> PrepareInputAsync(Stream input)
        {
            await foreach (var line in input.AsAsyncEnumerable())
            {
                var firstIndex = line.IndexOf(',');
                var secondIndex = line.IndexOf(',', firstIndex + 1);
                var thirdIndex = line.IndexOf(',', secondIndex + 1);

                var modified = line.Substring(0, firstIndex + 1) + "12,2" + line.Substring(thirdIndex);

                // we expect only one line
                return new MemoryStream(Encoding.UTF8.GetBytes(modified));
            }

            return null;
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            int[] registers = null;

            await foreach (var line in input.AsAsyncEnumerable())
            {
                registers = line.Split(',').Select(x => int.Parse(x)).ToArray();

                // we expect only one line
                break;
            }

            // run intcode program
            for (int i = 0; registers[i] != 99 && i < registers.Length; i += 4)
            {
                if (registers[i] == 1)
                {
                    registers[registers[i + 3]] = registers[registers[i + 1]] + registers[registers[i + 2]];
                }
                else if (registers[i] == 2)
                {
                    registers[registers[i + 3]] = registers[registers[i + 1]] * registers[registers[i + 2]];
                }
            }

            return registers[0].ToString();
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }


    }
}
