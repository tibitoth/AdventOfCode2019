using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using Microsoft.Extensions.Options;

namespace AdventOfCode2019.Puzzles
{
    [Day(16)]
    public class FlawedFrequencyTransmission : IPuzzleSolver
    {
        private readonly FlawedFrequencyTransmissionParameters _option;

        public FlawedFrequencyTransmission(IOptions<FlawedFrequencyTransmissionParameters> option)
        {
            _option = option.Value;
        }

        internal int[] CreatePattern(int length, int iteration)
        {
            var basePattern = new[] { 0, 1, 0, -1 };

            var iterationBasePattern = basePattern.SelectMany(p => Enumerable.Range(0, iteration).Select(i => p)).Take(length + 1);
            if (iterationBasePattern.Count() - 1 >= length)
            {
                return iterationBasePattern.Take(length + 1).Skip(1).ToArray();
            }
            else
            {
                return Enumerable.Repeat(iterationBasePattern, length).SelectMany(p => p).Take(length + 1).Skip(1).ToArray();
            }
        }

        internal int[] CalculateOutput(int[] signal)
        {
            for (int i = 0; i < _option.PhaseCount; i++)
            {
                var newSignal = new int[signal.Length];

                for (int j = 0; j < signal.Length; j++)
                {
                    var pattern = CreatePattern(signal.Length, j + 1);
                    newSignal[j] = Math.Abs(Enumerable.Range(0, signal.Length).Select(k => signal[k] * pattern[k]).Sum() % 10);
                }

                signal = newSignal;
            }

            return signal;
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            var signal = (await input.ReadLineAsync())
                .ToCharArray()
                .Select(c => int.Parse(c.ToString()))
                .ToArray();

            return new string(CalculateOutput(signal).Take(8).Select(c => c.ToString()[0]).ToArray());
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var signal = (await input.ReadLineAsync())
                .ToCharArray()
                .Select(c => int.Parse(c.ToString()))
                .ToArray();

            signal = Enumerable.Repeat(signal, 10000).SelectMany(p => p).ToArray();

            var output = CalculateOutput(signal);

            var offset = int.Parse(output.Take(7).Select(c => c.ToString()[0]).ToArray());

            return new string(output.Skip(offset).Take(8).Select(c => c.ToString()[0]).ToArray());
        }
    }
}
