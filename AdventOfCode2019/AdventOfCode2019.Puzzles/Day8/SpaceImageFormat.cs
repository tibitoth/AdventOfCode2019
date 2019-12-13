using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using Microsoft.Extensions.Options;

namespace AdventOfCode2019.Puzzles.Day8
{
    [Day(8)]
    public class SpaceImageFormat : IPuzzleSolver
    {
        private readonly SpaceImageFormatParameters _param;

        public SpaceImageFormat(IOptions<SpaceImageFormatParameters> optionsAccessor)
        {
            _param = optionsAccessor.Value;
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            var line = await input.ReadLineAsync();

            var layer = line.AsEnumerable()
                .Batch(_param.Width * _param.Height)
                // Find the least zero layer
                .Aggregate<IEnumerable<char>, (IEnumerable<char> Layer, int ZeroCount)>(
                    (Layer: null, ZeroCount: int.MaxValue),
                    (currMin, x) =>
                    {
                        var zeroCount = x.Count(c => c == '0');
                        return currMin.Layer == null || currMin.ZeroCount > zeroCount
                            ? (x, ZeroCount: zeroCount)
                            : currMin;
                    })
                .Layer;

            return (layer.Count(c => c == '1') * layer.Count(c => c == '2')).ToString();
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }

    }
}
