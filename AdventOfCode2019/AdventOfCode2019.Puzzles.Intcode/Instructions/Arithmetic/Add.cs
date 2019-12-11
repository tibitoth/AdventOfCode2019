using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Arithmetic
{
    public class Add : TwoOperandInstructionBase
    {
        public Add(Span<int> memory, int pointer)
            : base(memory, pointer)
        {
        }

        protected override int ExecuteCore(int param1, int param2)
        {
            return param1 + param2;
        }
    }
}
