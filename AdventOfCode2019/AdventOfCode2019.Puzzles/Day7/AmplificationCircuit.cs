﻿using System;
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
using AdventOfCode2019.Puzzles.Intcode.Instructions.IO;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode2019.Puzzles.Day7
{
    [Day(7)]
    public class AmplificationCircuit : IPuzzleSolver
    {
        private readonly IServiceProvider _serviceProvider;

        public AmplificationCircuit(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            return (await GetMaxThrusterSignalAsync(input, PhaseSettingsType.OneShot)).MaxSignal.ToString();
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            return (await GetMaxThrusterSignalAsync(input, PhaseSettingsType.FeedbackLoop)).MaxSignal.ToString();
        }

        internal async Task<(long MaxSignal, int[] MaxPermutation)> GetMaxThrusterSignalAsync(Stream input, PhaseSettingsType phaseSettingsType)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            long max = long.MinValue;
            int[] maxPermutation = null;
            var phaseSettingsRange = phaseSettingsType switch
            {
                PhaseSettingsType.OneShot => Enumerable.Range(0, 5),
                PhaseSettingsType.FeedbackLoop => Enumerable.Range(5, 5),
                _ => throw new ArgumentOutOfRangeException(nameof(phaseSettingsType), phaseSettingsType, $"Invalid {nameof(PhaseSettingsType)}")
            };

            foreach (var p in phaseSettingsRange.GetPermutations())
            {
                var permutation = p.ToArray();
                var amplifiers = new (IIntcodeProgram Program, Channel<long> Input)[5];
                for (int i = 0; i < amplifiers.Length; i++)
                {
                    var inputChannel = Channel.CreateUnbounded<long>();
                    await inputChannel.Writer.WriteAsync(permutation[i]);

                    var program = _serviceProvider.GetRequiredService<IIntcodeProgram>();
                    program.Init(registers.ToArray()); // copy registers
                    amplifiers[i] = (program, inputChannel);
                }

                // initial input is 0
                await amplifiers[0].Input.Writer.WriteAsync(0);

                var tasks = new Task[amplifiers.Length];

                for (int i = 0; i < amplifiers.Length; i++)
                {
                    // wire up input and output channels
                    // first amp's input is the last amp's output
                    tasks[i] = amplifiers[i].Program.RunAsync(amplifiers[i].Input, amplifiers[(i + 1) % amplifiers.Length].Input);
                }

                await Task.WhenAll(tasks);

                // Last amp's output is the first amp's input
                var result = await amplifiers[0].Input.Reader.ReadAsync();

                if (result > max)
                {
                    max = result;
                    maxPermutation = permutation;
                }
            }

            return (max, maxPermutation);
        }
    }
}
