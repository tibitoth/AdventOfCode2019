namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public abstract class InstructionBase
    {
        public abstract int InstructionLength { get; }

        public abstract void Operate(ProgramMemory memory);
    }
}