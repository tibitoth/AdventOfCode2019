using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public class Halt : InstructionBase
    {
        public override int InstructionLength => 1;

        public override void Execute(ProgramMemory memory)
        {
        }
    }
}
