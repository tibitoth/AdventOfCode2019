using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Arithmetic
{
    public class Add : TwoOperandInstructionBase
    {
        public Add(ProgramContext context)
            : base(context)
        {
        }

        protected override int ExecuteCore(int param1, int param2)
        {
            return param1 + param2;
        }
    }
}
