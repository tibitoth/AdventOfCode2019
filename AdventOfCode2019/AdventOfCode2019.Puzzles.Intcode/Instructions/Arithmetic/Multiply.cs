using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Arithmetic
{
    public class Multiply : TwoOperandInstructionBase
    {
        public Multiply(Span<int> operands, int address)
            : base(operands, address)
        {
        }

        protected override int ExecuteCore(int param1, int param2)
        {
            return param1 * param2;
        }
    }
}
