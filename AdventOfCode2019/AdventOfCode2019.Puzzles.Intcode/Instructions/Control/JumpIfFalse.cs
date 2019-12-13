using System;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Control
{
    public class JumpIfFalse : JumpIf
    {
        public JumpIfFalse(Span<int> memory, int instructionAddress)
            : base(memory, instructionAddress)
        {
        }

        public override async Task<int> ExecuteAsync(Memory<int> memory)
        {
            return Param == 0 ? AddressIf : await base.ExecuteAsync(memory);
        }
    }
}