using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Control
{
    public abstract class JumpIf : InstructionBase
    {
        public override int InstructionLength => 3;

        public int Param { get; }

        public int AddressIf { get; set; }

        public JumpIf(Span<int> memory, int instructionAddress)
            : base(instructionAddress)
        {
            Param = GetParameterValue(memory, instructionAddress, 1);
            AddressIf = GetParameterValue(memory, instructionAddress, 2);
        }
    }
}