using System;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Control
{
    public class JumpIfFalse : JumpIf
    {
        public JumpIfFalse(ProgramContext context)
            : base(context)
        {
        }

        public override async Task<int> ExecuteAsync()
        {
            return Param == 0 ? AddressIf : await base.ExecuteAsync();
        }
    }
}