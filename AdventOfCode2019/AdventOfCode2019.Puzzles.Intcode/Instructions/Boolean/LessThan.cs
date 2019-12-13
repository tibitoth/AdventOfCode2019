using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Boolean
{
    public class LessThan : TwoOperandInstructionBase
    {
        public LessThan(ProgramContext context) 
            : base(context)
        {
        }

        protected override int ExecuteCore(int param1, int param2)
        {
            return param1 < param2 ? 1 : 0;
        }
    }
}
