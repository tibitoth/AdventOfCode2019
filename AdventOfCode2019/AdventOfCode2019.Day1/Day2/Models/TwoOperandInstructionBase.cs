using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public abstract class TwoOperandInstructionBase : InstructionBase
    {
        public override int InstructionLength => 4;

        public TwoOperandInstructionBase(int leftParam, int rightParam, int target)
        {
            LeftParam = leftParam;
            RightParam = rightParam;
            Target = target;
        }

        public int LeftParam { get; set; }
        public int RightParam { get; set; }
        public int Target { get; set; }
    }
}
