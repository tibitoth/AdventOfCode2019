using System;
using System.IO;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.IO
{
    public class Input : OneParamInstruction
    {
        private readonly StreamReader _streamReader;

        public Input(Span<int> memory, int address, StreamReader streamReader)
            : base(address)
        {
            Param = memory[address + 1];
            _streamReader = streamReader;
        }

        public override int Execute(ProgramMemory memory)
        {
            var input = _streamReader.ReadLine();
            memory.Registers[Param] = int.Parse(input);

            return base.Execute(memory);
        }
    }
}
