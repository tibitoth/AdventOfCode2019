using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Extensions;
using Microsoft.Extensions.Options;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day16
{
    public class FlawedFrequencyTransmissionTests
    {
        [Theory]
        [InlineData(1, new[] { 1, 0, -1, 0, 1, 0, -1, 0, 1, 0 })]
        [InlineData(2, new[] { 0, 1, 1, 0, 0, -1, -1, 0, 0, 1 })]
        [InlineData(3, new[] { 0, 0, 1, 1, 1, 0, 0, 0, -1, -1 })]
        public void Part1_CreatePattern(int iteration, int[] expectedPattern)
        {
            // Arrange
            var subject = new FlawedFrequencyTransmission(Options.Create(new FlawedFrequencyTransmissionParameters() { PhaseCount = 4 }));

            // Act
            var result = subject.CreatePattern(10, iteration);

            // Assert
            Assert.Equal(expectedPattern, result);
        }

        [Fact]
        public async Task Part1_DetailedExample()
        {
            // Arrange
            var input = "12345678";
            var subject = new FlawedFrequencyTransmission(Options.Create(new FlawedFrequencyTransmissionParameters() { PhaseCount = 4 }));

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("01029498", result);
        }

        [Theory]
        [InlineData("80871224585914546619083218645595", "24176176")]
        [InlineData("19617804207202209144916044189917", "73745418")]
        [InlineData("69317163492948606335995924319873", "52432133")]
        public async Task Part1_Examples(string input, string expectedOutput)
        {
            // Arrange
            var subject = new FlawedFrequencyTransmission(Options.Create(new FlawedFrequencyTransmissionParameters() { PhaseCount = 100 }));

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public async Task Part1_ConcreteExcercise()
        {
            // Arrange
            var input = "59718730609456731351293131043954182702121108074562978243742884161871544398977055503320958653307507508966449714414337735187580549358362555889812919496045724040642138706110661041990885362374435198119936583163910712480088609327792784217885605021161016819501165393890652993818130542242768441596060007838133531024988331598293657823801146846652173678159937295632636340994166521987674402071483406418370292035144241585262551324299766286455164775266890428904814988362921594953203336562273760946178800473700853809323954113201123479775212494228741821718730597221148998454224256326346654873824296052279974200167736410629219931381311353792034748731880630444730593";
            var subject = new FlawedFrequencyTransmission(Options.Create(new FlawedFrequencyTransmissionParameters() { PhaseCount = 100 }));

            // Act
            var result = await subject.SolvePart1Async(input.ToMemoryStream());

            // Assert
            Assert.Equal("19944447", result);
        }

        [Theory]
        [InlineData("03036732577212944063491565474664", "84462026")]
        [InlineData("02935109699940807407585447034323", "78725270")]
        [InlineData("03081770884921959731165446850517", "53553731")]
        public async Task Part2_Examples(string input, string expectedOutput)
        {
            // Arrange
            var subject = new FlawedFrequencyTransmission(Options.Create(new FlawedFrequencyTransmissionParameters() { PhaseCount = 100 }));

            // Act
            var result = await subject.SolvePart2Async(input.ToMemoryStream());

            // Assert
            Assert.Equal(expectedOutput, result);
        }
    }
}
