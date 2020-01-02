using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Day7;
using AdventOfCode2019.Puzzles.Extensions;
using AdventOfCode2019.Puzzles.Intcode;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day7
{
    public class AmplificationCircuitTests
    {
        [Fact]
        public async Task Part1_Example1()
        {
            // Arrange
            var subject = new AmplificationCircuit(new ServiceCollection().AddTransient<IIntcodeProgram, IntcodeProgram>().BuildServiceProvider());
            var input = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";

            // Act
            var result = await subject.GetMaxThrusterSignalAsync(input.ToMemoryStream(), PhaseSettingsType.OneShot);

            // Assert
            Assert.Equal(43210, result.MaxSignal);
            Assert.Equal(new[] { 4, 3, 2, 1, 0 }, result.MaxPermutation);
        }

        [Fact]
        public async Task Part1_Example2()
        {
            // Arrange
            var subject = new AmplificationCircuit(new ServiceCollection().AddTransient<IIntcodeProgram, IntcodeProgram>().BuildServiceProvider());
            var input = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";

            // Act
            var result = await subject.GetMaxThrusterSignalAsync(input.ToMemoryStream(), PhaseSettingsType.OneShot);

            // Assert
            Assert.Equal(54321, result.MaxSignal);
            Assert.Equal(new[] { 0, 1, 2, 3, 4 }, result.MaxPermutation);
        }

        [Fact]
        public async Task Part1_Example3()
        {
            // Arrange
            var subject = new AmplificationCircuit(new ServiceCollection().AddTransient<IIntcodeProgram, IntcodeProgram>().BuildServiceProvider());
            var input = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";

            // Act
            var result = await subject.GetMaxThrusterSignalAsync(input.ToMemoryStream(), PhaseSettingsType.OneShot);

            // Assert
            Assert.Equal(65210, result.MaxSignal);
            Assert.Equal(new[] { 1, 0, 4, 3, 2 }, result.MaxPermutation);
        }

        [Fact]
        public async Task Part1_ConcrateExcercise()
        {
            // Arrange
            var subject = new AmplificationCircuit(new ServiceCollection().AddTransient<IIntcodeProgram, IntcodeProgram>().BuildServiceProvider());
            var input = "3,8,1001,8,10,8,105,1,0,0,21,38,55,64,81,106,187,268,349,430,99999,3,9,101,2,9,9,1002,9,2,9,101,5,9,9,4,9,99,3,9,102,2,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,102,2,9,9,4,9,99,3,9,1002,9,5,9,1001,9,4,9,102,4,9,9,4,9,99,3,9,102,2,9,9,1001,9,5,9,102,3,9,9,1001,9,4,9,102,5,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,99";

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("117312", result);
        }

        [Fact]
        public async Task Part2_Example1()
        {
            // Arrange
            var subject = new AmplificationCircuit(new ServiceCollection().AddTransient<IIntcodeProgram, IntcodeProgram>().BuildServiceProvider());
            var input = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";

            // Act
            var result = await subject.GetMaxThrusterSignalAsync(input.ToMemoryStream(), PhaseSettingsType.FeedbackLoop);

            // Assert
            Assert.Equal(139629729, result.MaxSignal);
            Assert.Equal(new[] { 9, 8, 7, 6, 5 }, result.MaxPermutation);
        }

        [Fact]
        public async Task Part2_Example2()
        {
            // Arrange
            var subject = new AmplificationCircuit(new ServiceCollection().AddTransient<IIntcodeProgram, IntcodeProgram>().BuildServiceProvider());
            var input = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";

            // Act
            var result = await subject.GetMaxThrusterSignalAsync(input.ToMemoryStream(), PhaseSettingsType.FeedbackLoop);

            // Assert
            Assert.Equal(18216, result.MaxSignal);
            Assert.Equal(new[] { 9, 7, 8, 5, 6 }, result.MaxPermutation);
        }

        [Fact]
        public async Task Part2_ConcrateExcercise()
        {
            // Arrange
            var subject = new AmplificationCircuit(new ServiceCollection().AddTransient<IIntcodeProgram, IntcodeProgram>().BuildServiceProvider());
            var input = "3,8,1001,8,10,8,105,1,0,0,21,38,55,64,81,106,187,268,349,430,99999,3,9,101,2,9,9,1002,9,2,9,101,5,9,9,4,9,99,3,9,102,2,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,102,2,9,9,4,9,99,3,9,1002,9,5,9,1001,9,4,9,102,4,9,9,4,9,99,3,9,102,2,9,9,1001,9,5,9,102,3,9,9,1001,9,4,9,102,5,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,99";

            // Act
            var result = await subject.SolvePart2Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("1336480", result);
        }
    }
}
