namespace AdventOfCode2019.Puzzles.Intcode.Instructions
{
    public abstract class OneParamInstruction : InstructionBase
    {
        public override int InstructionLength => 2;

        public int Param { get; set; }

        protected OneParamInstruction(int instructionAddress) : base(instructionAddress)
        {
        }
    }
}
