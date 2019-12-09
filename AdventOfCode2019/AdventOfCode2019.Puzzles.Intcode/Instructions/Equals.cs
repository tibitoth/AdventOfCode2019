using AdventOfCode2019.Puzzles.Day2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions
{
    public class Equals : TwoOperandInstructionBase
    {
        public Equals(Span<int> memory, int instructionAddress)
            : base(memory, instructionAddress)
        {
        }

        protected override int ExecuteCore(int param1, int param2)
        {
            return param1 == param2 ? 1 : 0;
        }
    }
}
