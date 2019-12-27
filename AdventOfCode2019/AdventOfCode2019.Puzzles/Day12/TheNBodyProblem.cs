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
using static System.Math;

namespace AdventOfCode2019.Puzzles.Day12
{
    [Day(12)]
    public class TheNBodyProblem : IPuzzleSolver
    {
        private List<Moon> _moons;

        internal class Moon
        {
            public Vector3 Position;
            public Vector3 Velocity;
        }

        internal async Task ParseInputAsync(Stream input)
        {
            _moons = new List<Moon>();

            await foreach (var line in input.AsAsyncEnumerable())
            {
                var split = line.Replace("<", "").Replace(">", "").Split(",");
                _moons.Add(new Moon()
                {
                    Position = new Vector3(
                        int.Parse(split[0].Split("=")[1]),
                        int.Parse(split[1].Split("=")[1]),
                        int.Parse(split[2].Split("=")[1])),
                    Velocity = new Vector3(0, 0, 0),
                });
            }
        }

        internal void Simulate(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                // calculate new velocity
                for (int j = 0; j < _moons.Count - 1; j++)
                {
                    for (int k = j + 1; k < _moons.Count; k++)
                    {
                        var m1 = _moons[j];
                        var m2 = _moons[k];

                        if (m1.Position.X > m2.Position.X)
                        {
                            m1.Velocity.X--;
                            m2.Velocity.X++;
                        }
                        else if (m1.Position.X < m2.Position.X)
                        {
                            m1.Velocity.X++;
                            m2.Velocity.X--;
                        }

                        if (m1.Position.Y > m2.Position.Y)
                        {
                            m1.Velocity.Y--;
                            m2.Velocity.Y++;
                        }
                        else if (m1.Position.Y < m2.Position.Y)
                        {
                            m1.Velocity.Y++;
                            m2.Velocity.Y--;
                        }

                        if (m1.Position.Z > m2.Position.Z)
                        {
                            m1.Velocity.Z--;
                            m2.Velocity.Z++;
                        }
                        else if (m1.Position.Z < m2.Position.Z)
                        {
                            m1.Velocity.Z++;
                            m2.Velocity.Z--;
                        }
                    }
                }

                // calculate new position
                for (int j = 0; j < _moons.Count; j++)
                {
                    _moons[j].Position.X += _moons[j].Velocity.X;
                    _moons[j].Position.Y += _moons[j].Velocity.Y;
                    _moons[j].Position.Z += _moons[j].Velocity.Z;
                }
            }
        }

        internal int GetSystemEnergy()
        {
            return (int)_moons.Sum(m => (Abs(m.Position.X) + Abs(m.Position.Y) + Abs(m.Position.Z))
                * (Abs(m.Velocity.X) + Abs(m.Velocity.Y) + Abs(m.Velocity.Z)));
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            await ParseInputAsync(input);

            Simulate(1000);

            return GetSystemEnergy().ToString();
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
