using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2019.Puzzles.Extensions
{
    public static class StreamExtensions
    {
        public static async IAsyncEnumerable<string> AsAsyncEnumerable(this Stream stream)
        {
            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                yield return await reader.ReadLineAsync();
            }
        }
    }
}
