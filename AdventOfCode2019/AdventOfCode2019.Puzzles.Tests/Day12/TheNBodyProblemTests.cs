using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Day12;
using AdventOfCode2019.Puzzles.Extensions;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day12
{
    public class TheNBodyProblemTests
    {
        [Fact]
        public async Task Part1_DetailedExample()
        {
            // Arrange
            var input = @"<x=-1, y=0, z=2>
<x=2, y=-10, z=-7>
<x=4, y=-8, z=8>
<x=3, y=5, z=-1>";
            var subject = new TheNBodyProblem();
            await subject.ParseInputAsync(input.ToMemoryStream());

            // Act
            subject.Simulate(10);
            var result = subject.GetSystemEnergy();

            // Assert
            Assert.Equal(179, result);
        }

        [Fact]
        public async Task Part1_Example1()
        {
            // Arrange
            var input = @"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>";
            var subject = new TheNBodyProblem();
            await subject.ParseInputAsync(input.ToMemoryStream());

            // Act
            subject.Simulate(100);
            var result = subject.GetSystemEnergy();

            // Assert
            Assert.Equal(1940, result);
        }

        [Fact]
        public async Task Part1_ConcreteExcercise()
        {
            // Arrange
            var input = @"<x=-9, y=10, z=-1>
<x=-14, y=-8, z=14>
<x=1, y=5, z=6>
<x=-19, y=7, z=8>";
            var subject = new TheNBodyProblem();
            await subject.ParseInputAsync(input.ToMemoryStream());

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("8538", result);
        }
    }
}
