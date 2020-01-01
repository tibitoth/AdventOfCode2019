using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public async Task Part2_DetailedExample()
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
            var asteroids = await subject.GetAsteroidsAsync(input.ToMemoryStream());
            var laser = subject.GetLaser(asteroids);
            var vaporized = subject.GetVaporizedAsteroids(asteroids, laser).ToList();

            Assert.Equal(299, vaporized.Count);
            Assert.Equal(11, laser.Coord.X);
            Assert.Equal(13, laser.Coord.Y);

            // Assert
            Assert.Equal(11, vaporized[0].X);
            Assert.Equal(12, vaporized[0].Y);

            Assert.Equal(12, vaporized[1].X);
            Assert.Equal(1, vaporized[1].Y);

            Assert.Equal(12, vaporized[2].X);
            Assert.Equal(2, vaporized[2].Y);

            Assert.Equal(12, vaporized[9].X);
            Assert.Equal(8, vaporized[9].Y);

            Assert.Equal(16, vaporized[19].X);
            Assert.Equal(0, vaporized[19].Y);

            Assert.Equal(16, vaporized[49].X);
            Assert.Equal(9, vaporized[49].Y);

            Assert.Equal(10, vaporized[99].X);
            Assert.Equal(16, vaporized[99].Y);

            Assert.Equal(9, vaporized[198].X);
            Assert.Equal(6, vaporized[198].Y);

            Assert.Equal(8, vaporized[199].X);
            Assert.Equal(2, vaporized[199].Y);

            Assert.Equal(10, vaporized[200].X);
            Assert.Equal(9, vaporized[200].Y);

            Assert.Equal(11, vaporized[298].X);
            Assert.Equal(1, vaporized[298].Y);
        }
    }
}
