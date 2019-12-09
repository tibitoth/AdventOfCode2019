using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public class Add : TwoOperandInstructionBase
    {
        public Add(Span<int> memory, int pointer)
            : base(memory, pointer)
        {
        }

        protected override int ExecuteCore(int param1, int param2)
        {
            return param1 + param2;
        }
    }
}
