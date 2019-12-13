using System;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.IO
{
    public class Output : OneParamInstruction
    {
        private readonly ChannelWriter<long> _writer;

        public Output(ProgramContext context, ChannelWriter<long> writer)
            : base(context)
        {
            Param = GetParameterValue(1);
            _writer = writer;
        }

        public override async Task<int> ExecuteAsync()
        {
            await _writer.WriteAsync(Param);

            return await base.ExecuteAsync();
        }
    }
}
