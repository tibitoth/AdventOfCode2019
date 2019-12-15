using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;

namespace AdventOfCode2019.Puzzles.Day10
{
    [Day(10)]
    public class MonitoringStation : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            var asteroids = new List<(Vector2 Coord, HashSet<Vector2> Hidden)>();

            int y = 0;
            await foreach (var line in input.AsAsyncEnumerable())
            {
                int x = 0;
                foreach (var field in line)
                {
                    if (field == '#')
                    {
                        asteroids.Add((new Vector2(x, y), new HashSet<Vector2>()));
                    }

                    x++;
                }

                y++;
            }

            foreach (var from in asteroids)
            {
                foreach (var a1 in asteroids)
                {
                    foreach (var a2 in asteroids)
                    {
                        if (from.Coord == a1.Coord || from.Coord == a2.Coord || a1.Coord == a2.Coord)
                        {
                            continue;
                        }

                        if (Math.Abs(Vector2.Distance(@from.Coord, a1.Coord) + Vector2.Distance(a1.Coord, a2.Coord) - Vector2.Distance(@from.Coord, a2.Coord)) < 0.00001)
                        {
                            from.Hidden.Add(a2.Coord);
                        }

                        if (Math.Abs(Vector2.Distance(@from.Coord, a2.Coord) + Vector2.Distance(a2.Coord, a1.Coord) - Vector2.Distance(@from.Coord, a1.Coord)) < 0.00001)
                        {
                            from.Hidden.Add(a1.Coord);
                        }
                    }
                }
            }

            return (asteroids.Count - 1 - asteroids.Min(x => x.Hidden.Count)).ToString();
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
