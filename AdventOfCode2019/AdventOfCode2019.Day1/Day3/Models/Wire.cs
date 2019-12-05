using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day3
{
    public class Wire
    {
        public Wire(IEnumerable<WirePart> parts)
        {
            Parts = parts;
        }

        public IEnumerable<WirePart> Parts { get; }
    }
}
