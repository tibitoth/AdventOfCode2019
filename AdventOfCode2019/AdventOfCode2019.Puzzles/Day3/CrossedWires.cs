using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Day3.Models;
using AdventOfCode2019.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Day3
{
    [Day(3)]
    public class CrossedWires : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            return (await SolveAsync(input)).Part1Answer;
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            return (await SolveAsync(input)).Part2Answer;
        }

        private async Task<(string Part1Answer, string Part2Answer)> SolveAsync(Stream input)
        {
            var wires = new List<Wire>();

            await foreach (var line in input.AsAsyncEnumerable())
            {
                wires.Add(new Wire(line.Split(',').Select(x =>
                    new WirePart(
                        x[0] switch
                        {
                            'R' => Direction.Right,
                            'L' => Direction.Left,
                            'U' => Direction.Up,
                            'D' => Direction.Down,
                        },
                        int.Parse(x.Substring(1))))));
            }

            var (xLenght, yLength, startx, starty) = GetFieldLength(wires);
            var field = new Cell[xLenght, yLength];
            int x = startx;
            int y = starty;
            int minManhattanDistance = int.MaxValue;
            int minDistanceOnWire = int.MaxValue;
            byte wireId = 0;

            foreach (var wire in wires)
            {
                int wireLenght = 0;
                foreach (var part in wire.Parts)
                {
                    for (int i = 1; i <= part.Length; i++)
                    {
                        switch (part.Direction)
                        {
                            case Direction.Left:
                                x--;
                                break;
                            case Direction.Right:
                                x++;
                                break;
                            case Direction.Up:
                                y++;
                                break;
                            case Direction.Down:
                                y--;
                                break;
                        }

                        field[x, y].AddWire(wireId, ++wireLenght);

                        if (field[x, y].IsCrossingWithOtherWires(wireId))
                        {
                            var manhattanDistanceFromStart = Math.Abs(startx - x) + Math.Abs(starty - y);
                            if (manhattanDistanceFromStart < minManhattanDistance)
                            {
                                minManhattanDistance = manhattanDistanceFromStart;
                            }

                            var distanceOnWire = wireLenght + field[x, y].GetOtherWireLength(wireId).Sum();
                            if (distanceOnWire < minDistanceOnWire)
                            {
                                minDistanceOnWire = distanceOnWire;
                            }
                        }
                    }
                }

                wireId++;
                x = startx;
                y = starty;
            }

            return (Part1Answer: minManhattanDistance.ToString(), Part2Answer: minDistanceOnWire.ToString());
        }

        private (int xLength, int yLength, int startX, int startY) GetFieldLength(List<Wire> wires)
        {
            int minX, maxX, minY, maxY;
            minX = minY = int.MaxValue;
            maxX = maxY = int.MinValue;

            foreach (var wire in wires)
            {
                int x = 0;
                int y = 0;

                foreach (var part in wire.Parts)
                {
                    switch (part.Direction)
                    {
                        case Direction.Left:
                            x -= part.Length;
                            break;
                        case Direction.Right:
                            x += part.Length;
                            break;
                        case Direction.Up:
                            y += part.Length;
                            break;
                        case Direction.Down:
                            y -= part.Length;
                            break;
                    }

                    if (x < minX)
                        minX = x;
                    if (x > maxX)
                        maxX = x;
                    if (y < minY)
                        minY = y;
                    if (y > maxY)
                        maxY = y;
                }
            }

            return
            (
                xLength: maxX - minX + 1,
                yLength: maxY - minY + 1,
                startX: - minX,
                startY: - minY
            );
        }
    }
}
