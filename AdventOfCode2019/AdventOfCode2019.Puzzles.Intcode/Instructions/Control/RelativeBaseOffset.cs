using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Control
{
    public class RelativeBaseOffset : InstructionBase
    {
        public int Param { get; set; }

        public RelativeBaseOffset(ProgramContext context)
            : base(context)
        {
            Param = GetParameterValue(1);
        }

        public override int InstructionLength => 2;

        public override Task<int> ExecuteAsync()
        {
            ProgramContext.RelativeBase += Param;

            return base.ExecuteAsync();
        }
    }
}
