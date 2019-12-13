using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Boolean
{
    public class LessThan : TwoOperandInstructionBase
    {
        public LessThan(ProgramContext context) 
            : base(context)
        {
        }

        protected override long ExecuteCore(long param1, long param2)
        {
            return param1 < param2 ? 1 : 0;
        }
    }
}
