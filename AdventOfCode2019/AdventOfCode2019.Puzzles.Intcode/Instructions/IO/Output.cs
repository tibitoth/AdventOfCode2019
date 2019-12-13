using System;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.IO
{
    public class Output : OneParamInstruction
    {
        private readonly ChannelWriter<int> _writer;

        public Output(Span<int> memory, int address, ChannelWriter<int> writer)
            : base(address)
        {
            Param = GetParameterValue(memory, address, 1);
            _writer = writer;
        }

        public override async Task<int> ExecuteAsync(Memory<int> memory)
        {
            await _writer.WriteAsync(Param);

            return await base.ExecuteAsync(memory);
        }
    }
}
