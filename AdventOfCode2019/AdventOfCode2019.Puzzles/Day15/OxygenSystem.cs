//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Channels;
//using System.Threading.Tasks;
//using AdventOfCode2018.Infrastructure;
//using AdventOfCode2019.Infrastructure;
//using AdventOfCode2019.Puzzles.Extensions;
//using AdventOfCode2019.Puzzles.Intcode;

//namespace AdventOfCode2019.Puzzles.Day15
//{
//    internal enum CommandResponse
//    {
//        WallHit = 0,
//        MoveSucceeded = 1,
//        MoveSucceededAndOxygenSystemFound = 2,
//    }

//    internal enum MovementCommand
//    {
//        North = 1,
//        South = 2,
//        West = 3,
//        East = 4,
//    }

//    internal class BackTrackStatus
//    {
//        public int XFrom { get; set; }
//        public int YFrom { get; set; }
//        public HashSet<MovementCommand> AvailableCommands { get; set; } = new HashSet<MovementCommand>();
//    }

//    [Day(15)]
//    public class OxygenSystem : IPuzzleSolver
//    {
//        public async Task<string> SolvePart1Async(Stream input)
//        {
//            var line = await input.ReadLineAsync();
//            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

//            var program = new IntcodeProgram(registers);

//            var outputChannel = Channel.CreateUnbounded<long>();
//            var inputChannel = Channel.CreateUnbounded<long>();
//            await program.RunAsync(inputChannel, outputChannel);
//        }

//        public Task<string> SolvePart2Async(Stream input)
//        {
//            throw new NotImplementedException();
//        }

//        internal async Task<int> GetFewestMovementCommandCountAsync(IIntcodeProgram program)
//        {
//            var walls = new HashSet<(int x, int y)>();
//            var movements = new Dictionary<(int x, int y), BackTrackStatus>();
//            int x = 0;
//            int y = 0;

//            var inputChannel = Channel.CreateUnbounded<long>();
//            var outputChannel = Channel.CreateUnbounded<long>();

//            var programTask = program.RunAsync(inputChannel, outputChannel);

//            (int x, int y) GetNewCoords(MovementCommand movementCommand)
//            {
//                return movementCommand switch
//                {
//                    MovementCommand.North => (x, y + 1),
//                    MovementCommand.South => (x, y - 1),
//                    MovementCommand.West => (x - 1, y),
//                    MovementCommand.East => (x + 1, y),
//                    _ => throw new ArgumentOutOfRangeException(nameof(movementCommand), movementCommand, null)
//                };
//            }

//            async Task MoveAsync(MovementCommand movementCommand)
//            {
//                await inputChannel.Writer.WriteAsync((long)movementCommand);

//                var m = movements.ContainsKey((x, y)) ? movements[(x, y)] : movements[(x, y)] = new BackTrackStatus();
//                m.

//                var response = (CommandResponse)await outputChannel.Reader.ReadAsync();
//                switch (response)
//                {
//                    case CommandResponse.WallHit:
//                        walls.Add(GetNewCoords(movementCommand));
//                        break;
//                    case CommandResponse.MoveSucceeded:
//                        (x, y) = GetNewCoords(movementCommand);
//                        break;
//                    case CommandResponse.MoveSucceededAndOxygenSystemFound:
//                        (x, y) = GetNewCoords(movementCommand);
//                        break;
//                    default:
//                        throw new ArgumentOutOfRangeException();
//                }
//            }



//            do
//            {
//                var paintColor = await outputChannel.Reader.ReadAsync();

//                await inputChannel.Writer.WriteAsync();
//            }
//            while (!programTask.IsCompleted && await outputChannel.Reader.WaitToReadAsync());
//        }
//    }
//}
