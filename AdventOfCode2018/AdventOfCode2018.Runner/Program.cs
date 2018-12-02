using AdventOfCode2018.Infrastructure;
using System;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var client = new PuzzleClient(new ChronalCalibrationSolver());

            await client.SolveAndSendAsync();
        }
    }
}
