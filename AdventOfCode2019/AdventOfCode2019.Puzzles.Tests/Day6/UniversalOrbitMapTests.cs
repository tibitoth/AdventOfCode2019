using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Day6;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day6
{
    public class UniversalOrbitMapTests
    {
        [Fact]
        public async Task DetailedExample()
        {
            // Arrange
            var subject = new UniversalOrbitMap();
            var input = 
@"COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("42", result);
        }

        [Fact]
        public async Task DetailedExampleNotOrderedInput()
        {
            // Arrange
            var subject = new UniversalOrbitMap();
            var input = 
                @"G)H
B)C
C)D
D)E
COM)B
E)F
J)K
D)I
E)J
B)G
K)L";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("42", result);
        }

        [Fact]
        public async Task ConcrateExcercise()
        {
            // Arrange
            var subject = new UniversalOrbitMap();
            using var input = File.OpenRead("Day6/Part1Input.txt");

            // Act
            var result = await subject.SolvePart1Async(input);

            // Assert
            Assert.Equal("160040", result);
        }
    }
}
