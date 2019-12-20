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

namespace AdventOfCode2019.Puzzles.Day11
{
    [Day(11)]
    public class SpacePolice : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            var program = new IntcodeProgram(registers);

            return (await GetPaintedPanelsAsync(program)).ToString();
        }

        internal async Task<int> GetPaintedPanelsAsync(IIntcodeProgram program)
        {
            var inputChannel = Channel.CreateUnbounded<long>();
            var outputChannel = Channel.CreateUnbounded<long>();

            var programTask = program.RunAsync(inputChannel, outputChannel);

            var paintedPanels = new Dictionary<(int x, int y), PaintedColor>();
            var x = 0;
            var y = 0;
            var dir = Direction.Up;

            await inputChannel.Writer.WriteAsync((long)PaintedColor.Black);

            do
            {
                var paintColor = await outputChannel.Reader.ReadAsync();
                paintedPanels[(x, y)] = (PaintedColor) paintColor;

                var rotate = await outputChannel.Reader.ReadAsync();
                dir = rotate switch
                {
                    0 => (Direction) ((int) (dir + 3) % 4),
                    1 => (Direction) ((int) (dir + 1) % 4),
                };

                switch (dir)
                {
                    case Direction.Up:
                        y++;
                        break;
                    case Direction.Right:
                        x++;
                        break;
                    case Direction.Down:
                        y--;
                        break;
                    case Direction.Left:
                        x--;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                await inputChannel.Writer.WriteAsync(paintedPanels.ContainsKey((x, y)) ? (long)paintedPanels[(x, y)] : (long)PaintedColor.Black);
            }
            while (!programTask.IsCompleted && await outputChannel.Reader.WaitToReadAsync());

            return paintedPanels.Count;
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
