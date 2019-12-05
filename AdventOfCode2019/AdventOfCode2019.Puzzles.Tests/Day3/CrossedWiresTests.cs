using AdventOfCode2019.Puzzles.Day3;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day3
{
    public class CrossedWiresTests
    {
        [Fact]
        public async Task Part1_DetailedExample()
        {
            // Arrange
            var input = "R8,U5,L5,D3\nU7,R6,D4,L4";
            var subject = new CrossedWires();

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(6.ToString(), result);
        }

        [Fact]
        public async Task Part1_ShortExample1()
        {
            // Arrange
            var input = "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83";
            var subject = new CrossedWires();

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(159.ToString(), result);
        }

        [Fact]
        public async Task Part1_ShortExample2()
        {
            // Arrange
            var input = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
            var subject = new CrossedWires();

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(135.ToString(), result);
        }
    }
}
