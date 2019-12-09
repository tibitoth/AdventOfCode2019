using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public class Output : OneParamInstruction
    {
        private readonly StreamWriter _streamWriter;

        public Output(Span<int> memory, int address, StreamWriter streamWriter)
        {
            Param = GetParameterValue(memory, address, 1);
            _streamWriter = streamWriter;
        }

        public override void Execute(ProgramMemory memory)
        {
            _streamWriter.WriteLine(Param);
        }
    }
}
