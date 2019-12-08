using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Extensions
{
    public static class StreamExtensions
    {
        public static async IAsyncEnumerable<string> AsAsyncEnumerable(this Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                yield return await reader.ReadLineAsync();
            }
        }

        public static async Task<string> ReadLineAsync(this Stream stream)
        {
            using var reader = new StreamReader(stream);
            return await reader.ReadLineAsync();
        }
    }
}
