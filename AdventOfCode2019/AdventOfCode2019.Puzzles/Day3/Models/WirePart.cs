using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day3
{
    public struct WirePart
    {
        public WirePart(Direction dir, int length)
        {
            Direction = dir;
            Length = length;
        }

        public Direction Direction { get; }
        public int Length { get; }
    }
}
