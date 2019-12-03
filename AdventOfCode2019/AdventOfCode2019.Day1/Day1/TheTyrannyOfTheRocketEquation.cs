using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
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
        public async Task<string> SolveAsync(Stream input)
        {
            var sum = 0;

            var reader = new StreamReader(input);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (int.TryParse(line, out int x))
                {
                    sum += x / 3 - 2;
                }
            }

            return sum.ToString();
        }
    }
}
