using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using AdventOfCode2019.Puzzles.Intcode;

namespace AdventOfCode2019.Puzzles.Day17
{
    [Day(17)]
    public class SetAndForget : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            var program = new IntcodeProgram(registers);

            var outputChannel = Channel.CreateUnbounded<long>();
            var inputChannel = Channel.CreateUnbounded<long>();
            await program.RunAsync(inputChannel, outputChannel);

            return (await GetAlignmentParametersAsync(outputChannel)).ToString();
        }

        private readonly Dictionary<(int x, int y), bool> _scaffoldIntersections = new Dictionary<(int x, int y), bool>();

        internal bool IsIntersection(int x, int y)
        {
            return _scaffoldIntersections.ContainsKey((x - 1, y))
                   && _scaffoldIntersections.ContainsKey((x + 1, y))
                   && _scaffoldIntersections.ContainsKey((x, y + 1))
                   && _scaffoldIntersections.ContainsKey((x, y - 1));
        }

        internal async Task<int> GetAlignmentParametersAsync(Channel<long> output)
        {
            int x = 0;
            int y = 0;

            await foreach (var item in output.Reader.ReadAllAsync())
            {
                if (item == '#' || item == '^' || item == '<' || item == '>' || item == 'v')
                {
                    _scaffoldIntersections[(x, y)] = false;
                    x++;
                }
                else if (item == 10) // new line
                {
                    y++;
                    x = 0;
                }
                else if (item == '.')
                {
                    x++;
                }
                else
                {
                    Debugger.Break();
                }
            }

            foreach (var s in _scaffoldIntersections.ToArray())
            {
                _scaffoldIntersections[s.Key] = IsIntersection(s.Key.x, s.Key.y);
            }

            return _scaffoldIntersections.Where(x => x.Value).Sum(x => x.Key.x * x.Key.y);
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
