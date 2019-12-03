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
    }
}
