using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using AdventOfCode2019.Puzzles.Intcode;

namespace AdventOfCode2019.Puzzles.Day13
{
    internal class Tile
    {
        public TileType TileType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    internal enum TileType
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        Paddle = 3,
        Ball = 4,
    }

    internal enum BallDirection
    {
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 3, 
        BottomRight = 4, 
    }

    [Day(13)]
    public class CarePackage : IPuzzleSolver
    {
        private Dictionary<(long x, long y), TileType> _tiles;
        private (long x, long y) _ball;
        private (long x, long y) _paddle;
        private long _score;

        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            var program = new IntcodeProgram(registers);

            var output = Channel.CreateUnbounded<long>();
            await program.RunAsync(Channel.CreateUnbounded<long>(), output);

            return (await GetBlockTileCountAsync(output)).ToString();
        }

        internal async Task<int> GetBlockTileCountAsync(Channel<long> output)
        {
            return await output.Reader
                .ReadAllAsync()
                .Batch(3)
                .CountAsync(t => (TileType)t.Skip(2).Single() == TileType.Block);
        }

        private async Task CreateInitialStateAsync(Channel<long> output)
        {
            _tiles = new Dictionary<(long x, long y), TileType>();
            _ball = (0, 0);

            await foreach (var tileData in output.Reader.ReadAllAsync().Batch(3))
            {
                var data = tileData.ToList();
                var coord = (x: data[0], y: data[1]);
                if (coord.x == -1 && coord.y == 0)
                {
                    _score = data[2];
                }
                else
                {
                    var type = (TileType) data[2];
                    _tiles[coord] = type;

                    if (type == TileType.Ball)
                    {
                        _ball = coord;
                    }

                    if (type == TileType.Paddle)
                    {
                        _paddle = coord;
                    }
                }
            }
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            // free coins, play mode
            registers[0] = 2;

            var program = new IntcodeProgram(registers);

            var output = Channel.CreateUnbounded<long>();
            var inputChannel = Channel.CreateBounded<long>(1);


            var programTask = program.RunAsync(inputChannel, output);
            var inputTask = Task.Run(async () =>
            {
                while (!programTask.IsCompleted)
                {
                    await inputChannel.Writer.WriteAsync(0);
                }
            });

            await CreateInitialStateAsync(output);

            await programTask;

            return "";
        }
    }
}
