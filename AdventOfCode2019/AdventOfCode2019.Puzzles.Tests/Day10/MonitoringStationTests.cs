using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Day10;
using AdventOfCode2019.Puzzles.Extensions;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day10
{
    public class MonitoringStationTests
    {
        [Fact]
        public async Task Part1_DetailedExample()
        {
            // Arrange
            var subject = new MonitoringStation();
            var input = 
@".#..#
.....
#####
....#
...##";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(8.ToString(), result);
        }

        [Fact]
        public async Task Part1_Example1()
        {
            // Arrange
            var subject = new MonitoringStation();
            var input = 
                @"......#.#.
#..#.#....
..#######.
.#.#.###..
.#..#.....
..#....#.#
#..#....#.
.##.#..###
##...#..#.
.#....####";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(33.ToString(), result);
        }

        [Fact]
        public async Task Part1_Example2()
        {
            // Arrange
            var subject = new MonitoringStation();
            var input = 
                @"#.#...#.#.
.###....#.
.#....#...
##.#.#.#.#
....#.#.#.
.##..###.#
..#...##..
..##....##
......#...
.####.###.";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(35.ToString(), result);
        }

        [Fact]
        public async Task Part1_Example3()
        {
            // Arrange
            var subject = new MonitoringStation();
            var input = 
                @".#..#..###
####.###.#
....###.#.
..###.##.#
##.##.#.#.
....###..#
..#.#..#.#
#..#.#.###
.##...##.#
.....#.#..";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(41.ToString(), result);
        }

        [Fact]
        public async Task Part1_Example4()
        {
            // Arrange
            var subject = new MonitoringStation();
            var input = 
                @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(210.ToString(), result);
        }

        [Fact]
        public async Task Part1_ConcreteExcercise()
        {
            // Arrange
            var subject = new MonitoringStation();
            var input = 
                @"#..#....#...#.#..#.......##.#.####
#......#..#.#..####.....#..#...##.
.##.......#..#.#....#.#..#.#....#.
###..#.....###.#....##.....#...#..
...#.##..#.###.......#....#....###
.####...##...........##..#..#.##..
..#...#.#.#.###....#.#...##.....#.
......#.....#..#...##.#..##.#..###
...###.#....#..##.#.#.#....#...###
..#.###.####..###.#.##..#.##.###..
...##...#.#..##.#............##.##
....#.##.##.##..#......##.........
.#..#.#..#.##......##...#.#.#...##
.##.....#.#.##...#.#.#...#..###...
#.#.#..##......#...#...#.......#..
#.......#..#####.###.#..#..#.#.#..
.#......##......##...#..#..#..###.
#.#...#..#....##.#....#.##.#....#.
....#..#....##..#...##..#..#.#.##.
#.#.#.#.##.#.#..###.......#....###
...#.#..##....###.####.#..#.#..#..
#....##..#...##.#.#.........##.#..
.#....#.#...#.#.........#..#......
...#..###...#...#.#.#...#.#..##.##
.####.##.#..#.#.#.#...#.##......#.
.##....##..#.#.#.......#.....####.
#.##.##....#...#..#.#..###..#.###.
...###.#..#.....#.#.#.#....#....#.
......#...#.........##....#....##.
.....#.....#..#.##.#.###.#..##....
.#.....#.#.....#####.....##..#....
.####.##...#.......####..#....##..
.#.#.......#......#.##..##.#.#..##
......##.....##...##.##...##......";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(334.ToString(), result);
        }
    }
}
