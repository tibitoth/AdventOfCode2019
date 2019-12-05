using AdventOfCode2019.Puzzles.Day4;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day4
{
    public class SecureContainerTests
    {
        [Fact]
        public void Part1_Input_111111_ShouldBeValid()
        {
            // Arrange
            var subject = new SecureContainer();

            // Act
            var result = subject.IsValidPart1("111111");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Part1_Input_223450_ShouldNotBeValid()
        {
            // Arrange
            var subject = new SecureContainer();

            // Act
            var result = subject.IsValidPart1("223450");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Part1_Input_123789_ShouldNotBeValid()
        {
            // Arrange
            var subject = new SecureContainer();

            // Act
            var result = subject.IsValidPart1("123789");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Part1_ConcrateExcerciseRetro()
        {
            // Arrange
            string input = "128392-643281";
            var subject = new SecureContainer();

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(2050.ToString(), result);
        }

        [Fact]
        public void Part2_Input_112233_ShouldBeValid()
        {
            // Arrange
            var subject = new SecureContainer();

            // Act
            var result = subject.IsValidPart2("112233");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Part2_Input_123444_ShouldNotBeValid()
        {
            // Arrange
            var subject = new SecureContainer();

            // Act
            var result = subject.IsValidPart2("123444");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Part2_Input_111122_ShouldBeValid()
        {
            // Arrange
            var subject = new SecureContainer();

            // Act
            var result = subject.IsValidPart2("111122");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Part2_ConcrateExcerciseRetro()
        {
            // Arrange
            string input = "128392-643281";
            var subject = new SecureContainer();

            // Act
            var result = await subject.SolvePart2Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(1390.ToString(), result);
        }
    }
}