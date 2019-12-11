namespace AdventOfCode2019.Puzzles.Intcode
{
    public class ProgramMemory
    {
        public int[] Registers { get; }

        public ProgramMemory(int[] registers)
        {
            Registers = registers;
        }
    }
}
