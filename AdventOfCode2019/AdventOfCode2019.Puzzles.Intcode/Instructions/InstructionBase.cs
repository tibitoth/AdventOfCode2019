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

        protected long GetParameterValue(int parameterPosition)
        {
            return ProgramContext[GetParameterIndex(parameterPosition)];
        }

        protected int GetParameterIndex(int parameterPosition)
        {
            int digit = (int)Math.Pow(10, parameterPosition) * 100;
            var instructionCode = ProgramContext[ProgramContext.InstructionPointer];

            if ((instructionCode % digit * 10 / digit) == 0) // absolute position mode
            {
                return 0 + (int) ProgramContext[ProgramContext.InstructionPointer + parameterPosition];
            }
            else if ((instructionCode % digit * 10 / digit) == 1) // immediate mode (value)
            {
                return ProgramContext.InstructionPointer + parameterPosition;
            }
            else if ((instructionCode % digit * 10 / digit) == 2) // relative position mode 
            {
                return ProgramContext.RelativeBase + (int)ProgramContext[ProgramContext.InstructionPointer + parameterPosition];
            }
            else
            {
                throw new InvalidOperationException("Not supported parameter mode");
            }
        }
    }
}