using System;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions
{
    public abstract class TwoOperandInstructionBase : InstructionBase
    {
        public override int InstructionLength => 4;

        protected TwoOperandInstructionBase(ProgramContext context) 
            : base(context)
        {
            FirstParam = GetParameterValue(1);
            SecondParam = GetParameterValue(2);

            TargetAddress = (int)context.Memory[context.InstructionPointer + 3];
        }

        public long FirstParam { get; set; }
        public long SecondParam { get; set; }
        public int TargetAddress { get; set; }

        public override async Task<int> ExecuteAsync()
        {
            ProgramContext.Memory[TargetAddress] = ExecuteCore(FirstParam, SecondParam);

            return await base.ExecuteAsync();
        }

        protected abstract long ExecuteCore(long param1, long param2);
    }
}
