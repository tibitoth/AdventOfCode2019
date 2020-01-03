using System;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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

            //ProgramContext[(int)Param] = await _reader.ReadAsync();
            //if (Console.KeyAvailable)
            //{
            ProgramContext[(int)Param] = Console.ReadKey(true).Key switch
            {
                ConsoleKey.RightArrow => 1,
                ConsoleKey.LeftArrow => -1,
                _ => 0,
            };
            //}
            //else
            //{
            //    ProgramContext[(int) Param] = 0;
            //}

            //ProgramContext[(int)Param] = await _reader.ReadAsync();

            _logger.LogDebug("Input has been read: {0}", ProgramContext[(int)Param]);

            return await base.ExecuteAsync();
        }
    }
}
