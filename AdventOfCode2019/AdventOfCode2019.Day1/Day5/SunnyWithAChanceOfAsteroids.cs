﻿using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            using var program = new IntcodeProgram(registers);
            var output = new MemoryStream();
            program.Run("1".ToMemoryStream(), output);
            return await output.AsAsyncEnumerable().LastAsync();
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            int[] registers = line.Split(',').Select(x => int.Parse(x)).ToArray();

            using var program = new IntcodeProgram(registers);
            var output = new MemoryStream();
            program.Run("5".ToMemoryStream(), output);
            return await output.AsAsyncEnumerable().LastAsync();
        }
    }
}