using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Control
{
    public class JumpIfFalse : JumpIf
    {
        public JumpIfFalse(Span<int> memory, int instructionAddress)
            : base(memory, instructionAddress)
        {
        }

        public override int Execute(Span<int> memory)
        {
            return Param == 0 ? AddressIf : base.Execute(memory);
        }
    }
}