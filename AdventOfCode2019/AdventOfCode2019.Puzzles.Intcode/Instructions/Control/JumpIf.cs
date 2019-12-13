using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Control
{
    public abstract class JumpIf : InstructionBase
    {
        public override int InstructionLength => 3;

        public int Param { get; }

        public int AddressIf { get; set; }

        protected JumpIf(ProgramContext context)
            : base(context)
        {
            Param = GetParameterValue(1);
            AddressIf = GetParameterValue(2);
        }
    }
}