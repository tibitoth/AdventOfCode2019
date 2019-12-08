using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public class Input : OneParamInstruction
    {
        private readonly StreamReader _streamReader;

        public Input(Span<int> memory, int address, StreamReader streamReader)
        {
            Param = memory[address + 1];
            _streamReader = streamReader;
        }

        public override void Execute(ProgramMemory memory)
        {
            var input = _streamReader.ReadLine();
            memory.Registers[Param] = int.Parse(input);
        }
    }
}
