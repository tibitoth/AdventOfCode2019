using System;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions
{
    public abstract class InstructionBase
    {
        public abstract int InstructionLength { get; }

        protected ProgramContext ProgramContext { get; }

        public virtual Task<int> ExecuteAsync()
        {
            return Task.FromResult(ProgramContext.InstructionPointer + InstructionLength);
        }

        protected InstructionBase(ProgramContext context)
        {
            ProgramContext = context;
        }

        protected int GetParameterValue(int parameterPosition)
        {
            int digit = (int)Math.Pow(10, parameterPosition) * 100;
            var instructionCode = ProgramContext.Memory[ProgramContext.InstructionPointer];

            if ((instructionCode % digit * 10 / digit) == 0) // absolute position mode
            {
                return ProgramContext.Memory[0 + ProgramContext.Memory[ProgramContext.InstructionPointer + parameterPosition]];
            }
            else if ((instructionCode % digit * 10 / digit) == 1) // immediate mode (value)
            {
                return ProgramContext.Memory[ProgramContext.InstructionPointer + parameterPosition];
            }
            else if ((instructionCode % digit * 10 / digit) == 2) // relative position mode 
            {
                return ProgramContext.Memory[ProgramContext.RelativeBase + ProgramContext.Memory[ProgramContext.InstructionPointer + parameterPosition]];
            }
            else
            {
                throw new InvalidOperationException("Not supported parameter mode");
            }
        }
    }
}