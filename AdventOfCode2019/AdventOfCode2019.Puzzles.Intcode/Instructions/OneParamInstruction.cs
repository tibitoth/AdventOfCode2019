using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public abstract class OneParamInstruction : InstructionBase
    {
        public override int InstructionLength => 2;

        public int Param { get; set; }
    }
}
