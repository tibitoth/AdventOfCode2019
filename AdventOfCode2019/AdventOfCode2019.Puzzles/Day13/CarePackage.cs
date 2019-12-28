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

    [Day(13)]
    public class CarePackage : IPuzzleSolver
    {
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

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
