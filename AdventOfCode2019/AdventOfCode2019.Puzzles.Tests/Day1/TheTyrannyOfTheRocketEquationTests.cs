using AdventOfCode2019.Puzzles.Day1;
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
        public async Task Test()
        {
            // Arrange
            var subject = new TheTyrannyOfTheRocketEquation();
            var input = new string[] { "12", "14", "1969", "100756" };

            // Act
            var result = await subject.SolveAsync(new MemoryStream(Encoding.UTF8.GetBytes(string.Join('\n', input))));

            // Assert
            var expectedSum = 2 + 2 + 654 + 33583;
            Assert.Equal(expectedSum.ToString(), result);
        }
    }
}
