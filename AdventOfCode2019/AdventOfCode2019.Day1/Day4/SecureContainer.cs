using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Day4
{
    [Day(4)]
    public class SecureContainer : IPuzzleSolver
    {
        public bool IsInputFromHttp { get => false; }

        // TODO refactor
        private readonly string _personalInput = "128392-643281";

        public async Task<string> SolvePart1Async(Stream input)
        {
            int start = 0;
            int end = 0;

            (int Start, int End) GetRange(string inputString)
            {
                var split = inputString.Split('-');
                return (int.Parse(split[0]), int.Parse(split[1]));
            }

            if (input != null)
            {
                await foreach (var line in input.AsAsyncEnumerable())
                {
                    (start, end) = GetRange(line);

                    //we expect only one line 
                    break;
                }
            }
            else
            {
                (start, end) = GetRange(_personalInput);
            }

            var count = Enumerable.Range(start, end - start)
                .Select(x => x.ToString("000000"))
                .Where(IsValid)
                .Count();

            return count.ToString();
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }

        internal bool IsValid(string x)
        {
            return IsNeverDecrease(x) && HasTwoSameAdjacentDigits(x);
        }

        private bool IsNeverDecrease(string x)
        {
            for (int i = 0; i < x.Length - 1; i++)
            {
                if (x[i] > x[i + 1])
                    return false;
            }

            return true;
        }

        private bool HasTwoSameAdjacentDigits(string x)
        {
            for (int i = 0; i < x.Length - 1; i++)
            {
                if (x[i] == x[i + 1])
                    return true;
            }

            return false;
        }
    }
}
