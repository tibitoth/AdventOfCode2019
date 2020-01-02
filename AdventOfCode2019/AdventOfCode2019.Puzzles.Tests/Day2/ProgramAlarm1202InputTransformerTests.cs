using AdventOfCode2019.Puzzles.Day2;
using AdventOfCode2019.Puzzles.Extensions;
using AdventOfCode2019.Puzzles.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Intcode;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace AdventOfCode2019.Puzzles.Tests.Day2
{
    public class ProgramAlarm1202InputTransformerTests
    {
        [Fact]
        public async Task SecondAndThirdShouldBe12And2()
        {
            // Arrange
            var subject = new ProgramAlarm1202(new ServiceCollection().AddTransient<IIntcodeProgram, IntcodeProgram>().BuildServiceProvider());
            var input = "1,9,10,3,2,3,11,0,99,30,40,50";

            // Act
            var modified = await subject.PrepareInputAsync(input.ToMemoryStream(), 1);
            string[] registers = null;
            await foreach (var line in modified.AsAsyncEnumerable())
            {
                registers = line.Split(",").ToArray();
            }

            // Assert
            Assert.Equal("12", registers[1]);
            Assert.Equal("2", registers[2]);
        }
    }
}
