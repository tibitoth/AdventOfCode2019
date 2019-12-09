using System;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public abstract class InstructionBase
    {
        public abstract int InstructionLength { get; }

        public abstract void Execute(ProgramMemory memory);

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