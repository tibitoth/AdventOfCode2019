using AdventOfCode2019.Puzzles.Day2;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day2
{
    public class ProgramAlarm1202Tests
    {
        [Fact]
        public async Task Part1_DetailedExample()
        {
            // Arrange
            var subject = new ProgramAlarm1202();
            var input = "1,9,10,3,2,3,11,0,99,30,40,50";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("3500", result);
        }

        [Fact]
        public async Task Part1_SmallExample1()
        {
            // Arrange
            var subject = new ProgramAlarm1202();
            var input = "1,0,0,0,99";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("2", result);
        }

        [Fact]
        public async Task Part1_SmallExample2()
        {
            // Arrange
            var subject = new ProgramAlarm1202();
            var input = "2,3,0,3,99";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("2", result);
        }

        [Fact]
        public async Task Part1_SmallExample3()
        {
            // Arrange
            var subject = new ProgramAlarm1202();
            var input = "2,4,4,5,99,0";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("2", result);
        }

        [Fact]
        public async Task Part1_SmallExample4()
        {
            // Arrange
            var subject = new ProgramAlarm1202();
            var input = "1,1,1,4,99,5,6,0,99";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("30", result);
        }

        [Fact]
        public async Task Part1_ConcrateExcercise()
        {
            // Arrange
            var subject = new ProgramAlarm1202();
            var input = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,1,19,5,23,2,23,6,27,1,27,5,31,2,6,31,35,1,5,35,39,2,39,9,43,1,43,5,47,1,10,47,51,1,51,6,55,1,55,10,59,1,59,6,63,2,13,63,67,1,9,67,71,2,6,71,75,1,5,75,79,1,9,79,83,2,6,83,87,1,5,87,91,2,6,91,95,2,95,9,99,1,99,6,103,1,103,13,107,2,13,107,111,2,111,10,115,1,115,6,119,1,6,119,123,2,6,123,127,1,127,5,131,2,131,6,135,1,135,2,139,1,139,9,0,99,2,14,0,0";

            // Act
            var modifiedInput = await subject.PrepareInputAsync(input.ToMemoryStream(), 1);
            var result = await subject.SolvePart1Async(modifiedInput);

            // Assert
            Assert.Equal("5866663", result);
        }

        [Fact]
        public async Task Part2_ConcrateExcercise()
        {
            // Arrange
            var subject = new ProgramAlarm1202();
            var input = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,1,19,5,23,2,23,6,27,1,27,5,31,2,6,31,35,1,5,35,39,2,39,9,43,1,43,5,47,1,10,47,51,1,51,6,55,1,55,10,59,1,59,6,63,2,13,63,67,1,9,67,71,2,6,71,75,1,5,75,79,1,9,79,83,2,6,83,87,1,5,87,91,2,6,91,95,2,95,9,99,1,99,6,103,1,103,13,107,2,13,107,111,2,111,10,115,1,115,6,119,1,6,119,123,2,6,123,127,1,127,5,131,2,131,6,135,1,135,2,139,1,139,9,0,99,2,14,0,0";

            // Act
            var result = await subject.SolvePart2Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("4259", result);
        }
    }
}
