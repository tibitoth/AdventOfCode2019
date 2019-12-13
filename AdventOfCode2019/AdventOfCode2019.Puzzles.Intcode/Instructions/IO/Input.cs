using System;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.IO
{
    public class Input : OneParamInstruction
    {
        private readonly ChannelReader<int> _reader;

        public Input(ProgramContext context, ChannelReader<int> reader)
            : base(context)
        {
            Param = ProgramContext.Memory[ProgramContext.InstructionPointer + 1];
            _reader = reader;
        }

        public override async Task<int> ExecuteAsync()
        {
            ProgramContext.Memory[Param] = await _reader.ReadAsync();

            return await base.ExecuteAsync();
        }
    }
}
