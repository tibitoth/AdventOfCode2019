using System;
using System.IO;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.IO
{
    public class Output : OneParamInstruction
    {
        private readonly StreamWriter _streamWriter;

        public Output(Span<int> memory, int address, StreamWriter streamWriter)
            : base(address)
        {
            Param = GetParameterValue(memory, address, 1);
            _streamWriter = streamWriter;
        }

        public override int Execute(Span<int> memory)
        {
            _streamWriter.WriteLine(Param);

            return base.Execute(memory);
        }
    }
}
