using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode2018.Infrastructure
{
    public interface IPuzzleSolver
    {
        Task<string> SolveAsync(Stream input);
    }
}
