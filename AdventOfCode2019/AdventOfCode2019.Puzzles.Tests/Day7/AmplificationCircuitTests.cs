using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Day7;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day7
{
    public class AmplificationCircuitTests
    {
        [Fact]
        public async Task Part1_Example1()
        {
            // Arrange
            var subject = new AmplificationCircuit();
            var input = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";

            // Act
            var result = await subject.GetMaxThrusterSignalAsync(input.ToMemoryStream());

            // Assert
            Assert.Equal(43210, result.MaxSignal);
            Assert.Equal(new[] { 4, 3, 2, 1, 0 }, result.MaxPermutation);
        }

        [Fact]
        public async Task Part1_Example2()
        {
            // Arrange
            var subject = new AmplificationCircuit();
            var input = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";

            // Act
            var result = await subject.GetMaxThrusterSignalAsync(input.ToMemoryStream());

            // Assert
            Assert.Equal(54321, result.MaxSignal);
            Assert.Equal(new[] { 0, 1, 2, 3, 4 }, result.MaxPermutation);
        }

        [Fact]
        public async Task Part1_Example3()
        {
            // Arrange
            var subject = new AmplificationCircuit();
            var input = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";

            // Act
            var result = await subject.GetMaxThrusterSignalAsync(input.ToMemoryStream());

            // Assert
            Assert.Equal(65210, result.MaxSignal);
            Assert.Equal(new[] { 1, 0, 4, 3, 2 }, result.MaxPermutation);
        }

        [Fact]
        public async Task Part1_ConcrateExcercise()
        {
            // Arrange
            var subject = new AmplificationCircuit();
            var input = "3,8,1001,8,10,8,105,1,0,0,21,38,55,64,81,106,187,268,349,430,99999,3,9,101,2,9,9,1002,9,2,9,101,5,9,9,4,9,99,3,9,102,2,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,102,2,9,9,4,9,99,3,9,1002,9,5,9,1001,9,4,9,102,4,9,9,4,9,99,3,9,102,2,9,9,1001,9,5,9,102,3,9,9,1001,9,4,9,102,5,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,99";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("117312", result);
        }
    }
}
