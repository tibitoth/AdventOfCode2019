using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Day1
{
    [Day(1)]
    public class TheTyrannyOfTheRocketEquation : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            var sum = 0;
           
            await foreach (var line in input.AsAsyncEnumerable())
            {
                if (int.TryParse(line, out int mass))
                {
                    sum += mass / 3 - 2;
                }
            }

            return sum.ToString();
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var sum = 0;

            int CalculateRequiredFuelPart2(int mass)
            {
                var fuel = mass / 3 - 2;
                return fuel > 0 ? fuel + CalculateRequiredFuelPart2(fuel) : 0;
            }

            await foreach (var line in input.AsAsyncEnumerable())
            {
                if (int.TryParse(line, out int mass))
                {
                    sum += CalculateRequiredFuelPart2(mass);
                }
            }

            return sum.ToString();
        }
    }
}
