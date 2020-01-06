using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.IO
{
    public class Input : OneParamInstruction
    {
        private readonly ChannelReader<long> _reader;
        private readonly ILogger _logger;

        public Input(ProgramContext context, ChannelReader<long> reader, ILogger logger)
            : base(context)
        {
            Param = GetParameterIndex(1);
            _reader = reader;
            _logger = logger;
        }

        public override async Task<int> ExecuteAsync()
        {
            _logger.LogDebug("Reading from input");

            ProgramContext[(int)Param] = await _reader.ReadAsync();

            _logger.LogDebug("Input has been read: {0}", ProgramContext[(int)Param]);

            return await base.ExecuteAsync();
        }
    }
}
