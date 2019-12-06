using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public class Multiply : TwoOperandInstructionBase
    {
        public Multiply(Span<int> operands) 
            : base(operands[1], operands[2], operands[3])
        {
        }

        public override void Operate(ProgramMemory memory)
        {
            memory.Registers[Target] = memory.Registers[LeftParam] * memory.Registers[RightParam];
        }
    }
}
