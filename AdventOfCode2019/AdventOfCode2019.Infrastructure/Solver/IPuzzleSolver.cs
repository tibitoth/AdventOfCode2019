using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode2018.Infrastructure
{
    public interface IPuzzleSolver
    {
        bool IsInputFromHttp { get => true; }
        Task<Stream> PrepareInputAsync(Stream input, int part) => Task.FromResult(input);

        Task<string> SolvePart1Async(Stream input);
        Task<string> SolvePart2Async(Stream input);
    }
}
