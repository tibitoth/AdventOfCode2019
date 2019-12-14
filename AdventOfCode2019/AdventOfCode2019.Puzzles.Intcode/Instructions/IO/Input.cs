using System;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.IO
{
    public class Input : OneParamInstruction
    {
        private readonly ChannelReader<long> _reader;

        public Input(ProgramContext context, ChannelReader<long> reader)
            : base(context)
        {
            Param = GetParameterIndex(1);
            _reader = reader;
        }

        public override async Task<int> ExecuteAsync()
        {
            ProgramContext[(int)Param] = await _reader.ReadAsync();

            return await base.ExecuteAsync();
        }
    }
}
