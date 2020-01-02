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
using Microsoft.Extensions.Logging;

namespace AdventOfCode2019.Puzzles.Day11
{
    [Day(11)]
    public class SpacePolice : IPuzzleSolver
    {
        private readonly ILogger<SpacePolice> _logger;
        private readonly IIntcodeProgram _intcodeProgram;

        public SpacePolice(ILogger<SpacePolice> logger, IIntcodeProgram intcodeProgram)
        {
            _logger = logger;
            _intcodeProgram = intcodeProgram;
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();

            return (await GetPaintedPanelsAsync(_intcodeProgram, PaintedColor.Black)).Count.ToString();
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var line = await input.ReadLineAsync();
            var registers = line.Split(',').Select(x => long.Parse(x)).ToArray();
            _intcodeProgram.Init(registers);

            var panels = await GetPaintedPanelsAsync(_intcodeProgram, PaintedColor.White);

            var s = new StringBuilder();

            for (int j = panels.Keys.Max(c => c.y) ; j > panels.Keys.Min(c => c.y) - 1; j--)
            {
                for (int i = panels.Keys.Min(c => c.x); i < panels.Keys.Max(c => c.x) + 1; i++)
                {
                    s.Append(panels.ContainsKey((i, j))
                        ? panels[(i, j)] == PaintedColor.White ? "#" : " "
                        : " ");
                }

                s.Append(Environment.NewLine);
            }

            _logger.LogInformation(s.ToString());

            // todo ocr
            return null; // URCAFLCP
        }

        internal async Task<Dictionary<(int x, int y), PaintedColor>> GetPaintedPanelsAsync(IIntcodeProgram program, PaintedColor startColor)
        {
            var inputChannel = Channel.CreateUnbounded<long>();
            var outputChannel = Channel.CreateUnbounded<long>();

            var programTask = program.RunAsync(inputChannel, outputChannel);

            var paintedPanels = new Dictionary<(int x, int y), PaintedColor>();
            var x = 0;
            var y = 0;
            var dir = Direction.Up;

            await inputChannel.Writer.WriteAsync((long)startColor);

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

            return paintedPanels;
        }
    }
}
