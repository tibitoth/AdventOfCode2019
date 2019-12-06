using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Day2.Models;
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
            var line = await input.ReadLineAsync();
            var firstIndex = line.IndexOf(',');
            var secondIndex = line.IndexOf(',', firstIndex + 1);
            var thirdIndex = line.IndexOf(',', secondIndex + 1);

            var modified = line.Substring(0, firstIndex + 1) + "12,2" + line.Substring(thirdIndex);

            return new MemoryStream(Encoding.UTF8.GetBytes(modified));
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            int[] registers = line.Split(',').Select(x => int.Parse(x)).ToArray();

            var program = new IntcodeProgram(new ProgramMemory(registers));
            program.Run();

            return program[0].ToString();
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
