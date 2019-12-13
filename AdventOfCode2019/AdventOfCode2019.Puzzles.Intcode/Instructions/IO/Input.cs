using System;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.IO
{
    public class Input : OneParamInstruction
    {
        private readonly ChannelReader<int> _reader;

        public Input(Span<int> memory, int address, ChannelReader<int> reader)
            : base(address)
        {
            Param = memory[address + 1];
            _reader = reader;
        }

        public override async Task<int> ExecuteAsync(Memory<int> memory)
        {
            var x = await _reader.ReadAsync();
            memory.Span[Param] = x;

            return await base.ExecuteAsync(memory);
        }
    }
}
