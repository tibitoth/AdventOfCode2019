using System;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Control
{
    public class JumpIfTrue : JumpIf
    {
        public JumpIfTrue(ProgramContext context)
            : base(context)
        {
        }

        public override async Task<int> ExecuteAsync()
        {
            return Param > 0 ? AddressIf : await base.ExecuteAsync();
        }
    }
}
