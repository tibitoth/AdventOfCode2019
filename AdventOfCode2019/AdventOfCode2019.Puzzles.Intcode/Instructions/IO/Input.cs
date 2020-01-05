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

        private static List<long> Movements = new List<long>();
        private static int currentIndex = 0;

        public override async Task<int> ExecuteAsync()
        {
            //_logger.LogDebug("Reading from input");

            //long? GetInput()
            //{
            //    if (currentIndex < Movements.Count)
            //    {
            //        return Movements[currentIndex++];
            //    }

            //    ConsoleKey c;
            //    switch (Console.ReadKey(true).Key)
            //    {
            //        case ConsoleKey.RightArrow:
            //            Movements.Add(1);
            //            currentIndex++;
            //            return 1;
            //        case ConsoleKey.LeftArrow:
            //            Movements.Add(-1);
            //            currentIndex++;
            //            return -1;
            //        case ConsoleKey.S:
            //            var json = JsonConvert.SerializeObject(Movements);
            //            File.WriteAllText("day13.data", json);
            //            break;
            //        case ConsoleKey.L:
            //            var json2 = File.ReadAllText("day13.data");
            //            Movements = JsonConvert.DeserializeObject<List<long>>(json2);
            //            currentIndex = 0;
            //            break;
            //        default:
            //            Movements.Add(0);
            //            currentIndex++;
            //            return 0;
            //    }

            //    return null;
            //}

            //long? i;
            //do
            //{
            //    i = GetInput();
            //} while (i == null);

            //ProgramContext[(int) Param] = i.Value;

            ProgramContext[(int)Param] = await _reader.ReadAsync();

            //_logger.LogDebug("Input has been read: {0}", ProgramContext[(int)Param]);

            return await base.ExecuteAsync();
        }
    }
}
