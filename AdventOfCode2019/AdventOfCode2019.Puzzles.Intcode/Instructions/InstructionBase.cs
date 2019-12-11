using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions
{
    public abstract class InstructionBase
    {
        public abstract int InstructionLength { get; }

        protected int InstructionAddress { get; }

        public virtual int Execute(ProgramMemory memory)
        {
            return InstructionAddress + InstructionLength;
        }

        protected InstructionBase(int instructionAddress)
        {
            InstructionAddress = instructionAddress;
        }

        protected int GetParameterValue(Span<int> memory, int instructionAddress, int parameterPosition)
        {
            int digit = (int)Math.Pow(10, parameterPosition) * 100;
            var instructionCode = memory[instructionAddress];

            if ((instructionCode % digit * 10 / digit) == 0) // position mode
            {
                return memory[memory[instructionAddress + parameterPosition]];
            }
            else if ((instructionCode % digit * 10 / digit) == 1) // immediate mode (value)
            {
                return memory[instructionAddress + parameterPosition];
            }
            else
            {
                throw new InvalidOperationException("Not supported parameter mode");
            }
        }
    }
}