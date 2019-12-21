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
    public class Asteroid
    {
        public Vector2 Coord { get; set; }
        public HashSet<Vector2> Hidden { get; set; } = new HashSet<Vector2>();

        public Asteroid(int x, int y)
        {
            Coord = new Vector2(x, y);
        }
    }

    [Day(10)]
    public class MonitoringStation : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            var asteroids = await GetAsteroidsAsync(input);

            return (asteroids.Count - 1 - GetLaser(asteroids).Hidden.Count).ToString();
        }

        private async Task<List<Asteroid>> GetAsteroidsAsync(Stream input)
        {
            var asteroids = new List<Asteroid>();

            int y = 0;
            await foreach (var line in input.AsAsyncEnumerable())
            {
                int x = 0;
                foreach (var field in line)
                {
                    if (field == '#')
                    {
                        asteroids.Add(new Asteroid(x, y));
                    }

                    x++;
                }

                y++;
            }

            return asteroids;
        }

        private Asteroid GetLaser(List<Asteroid> asteroids)
        {
            foreach (var from in asteroids)
            {
                FillHiddenAsteroids(asteroids, from);
            }

            var min = asteroids.Min(x => x.Hidden.Count);
            return asteroids.Single(a => a.Hidden.Count == min);
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var asteroids = await GetAsteroidsAsync(input);
            var laser = GetLaser(asteroids);
            var a200 = Get200thAsteroid(asteroids, laser);

            return (a200.X * 100 + a200.Y).ToString();
        }

        private Vector2 Get200thAsteroid(List<Asteroid> asteroids, Asteroid laser)
        {
            int i = 1;

            while (true)
            {
                FillHiddenAsteroids(asteroids, laser);

                var currentIteration = asteroids.Where(x => x.Coord != laser.Coord && !laser.Hidden.Contains(x.Coord)).ToList();
                while (currentIteration.Any())
                {
                    Asteroid min = null;
                    double minAngle = double.MaxValue;

                    foreach (var a in currentIteration)
                    {
                        var angle = Math.Acos(Vector2.Dot(a.Coord, laser.Coord) / (a.Coord.Length() * laser.Coord.Length()));
                        if (angle < minAngle)
                        {
                            minAngle = angle;
                            min = a;
                        }
                    }

                    asteroids.Remove(min);
                    currentIteration.Remove(min);
                    if (i == 200)
                    {
                        return min.Coord;
                    }

                    i++;
                }
            }
        }

        private void FillHiddenAsteroids(List<Asteroid> asteroids, Asteroid from)
        {
            from.Hidden.Clear();

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
    }
}
