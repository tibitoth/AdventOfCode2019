using AdventOfCode2018.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day1
{
    public class ChronalCalibrationSolver : IPuzzleSolver
    {
        public int Day => 1;

        public async Task<string> SolveAsync(Stream input)
        {
            var sum = 0;

            var reader = new StreamReader(input);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (int.TryParse(line, out int x))
                {
                    sum += x;
                }
            }

            return sum.ToString();
        }
    }
}
