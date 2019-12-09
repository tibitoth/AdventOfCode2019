using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public class Multiply : TwoOperandInstructionBase
    {
        public Multiply(Span<int> operands, int address)
            : base(operands, address)
        {
        }

        protected override int ExecuteCore(int param1, int param2)
        {
            return param1 * param2;
        }
    }
}
