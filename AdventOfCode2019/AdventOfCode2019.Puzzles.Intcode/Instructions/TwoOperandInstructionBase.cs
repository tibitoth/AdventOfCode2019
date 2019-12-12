using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions
{
    public abstract class TwoOperandInstructionBase : InstructionBase
    {
        public override int InstructionLength => 4;

        public TwoOperandInstructionBase(Span<int> memory, int instructionAddress) 
            : base(instructionAddress)
        {
            FirstParam = GetParameterValue(memory, instructionAddress, 1);
            SecondParam = GetParameterValue(memory, instructionAddress, 2);

            TargetAddress = memory[instructionAddress + 3];
        }

        public int FirstParam { get; set; }
        public int SecondParam { get; set; }
        public int TargetAddress { get; set; }

        public override int Execute(Span<int> memory)
        {
            memory[TargetAddress] = ExecuteCore(FirstParam, SecondParam);

            return base.Execute(memory);
        }

        protected abstract int ExecuteCore(int param1, int param2);
    }
}
