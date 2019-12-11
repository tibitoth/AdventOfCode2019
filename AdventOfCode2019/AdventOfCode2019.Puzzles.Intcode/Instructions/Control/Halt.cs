namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Control
{
    public class Halt : InstructionBase
    {
        public override int InstructionLength => 1;

        public Halt(int instructionAddress) : base(instructionAddress)
        {
        }
    }
}
