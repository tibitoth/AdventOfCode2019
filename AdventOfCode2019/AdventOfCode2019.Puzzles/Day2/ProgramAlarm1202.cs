﻿using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Channels;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Intcode;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode2019.Puzzles.Day2
{
    [Day(2)]
    public class ProgramAlarm1202 : IPuzzleSolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ProgramAlarm1202(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Stream> PrepareInputAsync(Stream input, int part)
        {
            if (part == 1)
            {
                var line = await input.ReadLineAsync();
                var firstIndex = line.IndexOf(',');
                var secondIndex = line.IndexOf(',', firstIndex + 1);
                var thirdIndex = line.IndexOf(',', secondIndex + 1);

                var modified = line.Substring(0, firstIndex + 1) + "12,2" + line.Substring(thirdIndex);

                return new MemoryStream(Encoding.UTF8.GetBytes(modified));
            }
            else
            {
                return input;
            }
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            long[] registers = line.Split(',').Select(x => long.Parse(x)).ToArray();
            var program = _serviceProvider.GetRequiredService<IIntcodeProgram>();
            program.Init(registers);

            await program.RunAsync(Channel.CreateUnbounded<long>(), Channel.CreateUnbounded<long>());

            return program[0].ToString();
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            long[] registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    registers[1] = noun;
                    registers[2] = verb;
                    var program = _serviceProvider.GetRequiredService<IIntcodeProgram>();
                    program.Init(registers);
                    await program.RunAsync(Channel.CreateUnbounded<long>(), Channel.CreateUnbounded<long>());

                    if (program[0] == 19690720)
                    {
                        return (100 * noun + verb).ToString();
                    }
                }
            }

            return string.Empty;
        }
    }
}
