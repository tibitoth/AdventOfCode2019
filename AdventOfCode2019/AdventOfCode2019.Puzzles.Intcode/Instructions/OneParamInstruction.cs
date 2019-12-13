namespace AdventOfCode2019.Puzzles.Intcode.Instructions
{
    public abstract class OneParamInstruction : InstructionBase
    {
        public override int InstructionLength => 2;

        public long Param { get; set; }

        protected OneParamInstruction(ProgramContext context) : base(context)
        {
        }
    }
}
