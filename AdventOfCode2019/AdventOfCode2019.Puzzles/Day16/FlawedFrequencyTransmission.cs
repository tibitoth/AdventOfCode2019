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

        private readonly int[] _basePattern = new[] { 0, 1, 0, -1 };

        internal int GetPatternItem(int index, int iteration)
        {
            return _basePattern[((index + 1) / iteration) % _basePattern.Length];
        }

        internal int[] CalculateOutput(int[] signal)
        {
            for (int i = 0; i < _option.PhaseCount; i++)
            {
                var newSignal = new int[signal.Length];

                Parallel.For(0, signal.Length, j =>
                {
                    int sum = 0;
                    for (int k = 0; k < signal.Length; k++)
                    {
                        sum += signal[k] * GetPatternItem(k, j + 1);
                        //sum += signal[k] * _basePattern[((k + 1) / (j + 1)) % _basePattern.Length];
                    }

                    newSignal[j] = Math.Abs(sum % 10);
                });

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
