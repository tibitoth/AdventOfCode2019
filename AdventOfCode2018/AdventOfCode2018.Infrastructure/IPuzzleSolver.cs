using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode2018.Infrastructure
{
    public interface IPuzzleSolver
    {
        int Day { get; }

        Task<string> SolveAsync(Stream input);
    }
}
