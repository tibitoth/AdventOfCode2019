using System.Collections.Generic;
using System.Numerics;

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
}