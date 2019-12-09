using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public abstract class TwoOperandInstructionBase : InstructionBase
    {
        public override int InstructionLength => 4;

        public TwoOperandInstructionBase(Span<int> memory, int instructionAddress)
        {
            FirstParam = GetParameterValue(memory, instructionAddress, 1);
            SecondParam = GetParameterValue(memory, instructionAddress, 2);

            TargetAddress = memory[instructionAddress + 3];
        }

        public int FirstParam { get; set; }
        public int SecondParam { get; set; }
        public int TargetAddress { get; set; }

        public override void Execute(ProgramMemory memory)
        {
            memory.Registers[TargetAddress] = ExecuteCore(FirstParam, SecondParam);
        }

        protected abstract int ExecuteCore(int param1, int param2);
    }
}
