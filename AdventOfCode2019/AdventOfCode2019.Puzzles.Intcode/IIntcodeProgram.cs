using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Intcode
{
    public interface IIntcodeProgram
    {
        Task RunAsync(Channel<long> input, Channel<long> output);
        long this[int index] { get; }
    }
}