using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day2.Models
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
