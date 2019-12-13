using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Intcode
{
    public class ProgramContext
    {
        public List<long> Memory { get; set; }
        public int InstructionPointer { get; set; }
        public int RelativeBase { get; set; }
    }
}
