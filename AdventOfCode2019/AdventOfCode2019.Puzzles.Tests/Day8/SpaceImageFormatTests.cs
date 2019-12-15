using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Puzzles.Day8;
using AdventOfCode2019.Puzzles.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day8
{
    public class SpaceImageFormatTests
    {
        [Fact]
        public async Task Part1_Example1()
        {
            // Arrange
            var input = "123456789012";
            var subject = new SpaceImageFormat(Options.Create(new SpaceImageFormatParameters { Height = 2, Width = 3 }), new NullLogger<SpaceImageFormat>());

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(1.ToString(), result);
        }

        [Fact]
        public async Task Part1_Example2()
        {
            // Arrange
            var input = "121456789012";
            var subject = new SpaceImageFormat(Options.Create(new SpaceImageFormatParameters { Height = 2, Width = 3 }), new NullLogger<SpaceImageFormat>());

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(2.ToString(), result);
        }

        [Fact]
        public async Task Part1_ConcrateExcercise()
        {
            // Arrange
            using var input = File.OpenRead("Day8/input.txt");
            var subject = new SpaceImageFormat(Options.Create(new SpaceImageFormatParameters { Height = 25, Width = 6 }), new NullLogger<SpaceImageFormat>());

            // Act
            var result = await subject.SolvePart1Async(input);

            // Assert
            Assert.Equal(1862.ToString(), result);
        }
    }
}
