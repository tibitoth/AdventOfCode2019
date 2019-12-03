using AdventOfCode2019.Puzzles.Day1;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests
{
    public class TheTyrannyOfTheRocketEquationTests
    {
        [Fact]
        public async Task Part1()
        {
            // Arrange
            var subject = new TheTyrannyOfTheRocketEquation();
            var input = new string[] { "14", "14", "1969", "100756" };

            // Act
            var result = await subject.SolvePart1Async(string.Join('\n', input).ToMemoryStream());

            // Assert
            var expectedSum = 2 + 2 + 654 + 33583;
            Assert.Equal(expectedSum.ToString(), result);
        }

        [Fact]
        public async Task Part2()
        {
            // Arrange
            var subject = new TheTyrannyOfTheRocketEquation();
            var input = new string[] { "14", "1969", "100756" };

            // Act
            var result = await subject.SolvePart2Async(new MemoryStream(Encoding.UTF8.GetBytes(string.Join('\n', input))));

            // Assert
            var expectedSum = 2 // 14
                + 654 + 216 + 70 + 21 + 5 // 1989 
                + 33583 + 11192 + 3728 + 1240 + 411 + 135 + 43 + 12 + 2; // 100756
            Assert.Equal(expectedSum.ToString(), result);
        }
    }
}
