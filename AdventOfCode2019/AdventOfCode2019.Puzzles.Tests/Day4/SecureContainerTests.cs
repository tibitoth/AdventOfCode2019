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
        public void Input_111111_ShouldBeValid()
        {
            // Arrange
            var subject = new SecureContainer();

            // Act
            var result = subject.IsValid("111111");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Input_223450_ShouldNotBeValid()
        {
            // Arrange
            var subject = new SecureContainer();

            // Act
            var result = subject.IsValid("223450");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Input_123789_ShouldNotBeValid()
        {
            // Arrange
            var subject = new SecureContainer();

            // Act
            var result = subject.IsValid("123789");

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
    }
}
