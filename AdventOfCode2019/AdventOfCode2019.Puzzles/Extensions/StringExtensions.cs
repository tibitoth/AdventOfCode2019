using System;   
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Tests.Extensions
{
    public static class ChannelExtensions
    {
        public static async Task<Channel<long>> ToChannelAsync(this string s)
        {
            var c = Channel.CreateUnbounded<long>();
            using var stringReader = new StringReader(s);
            string line = null;
            while ((line = await stringReader.ReadLineAsync()) != null)
            {
                await c.Writer.WriteAsync(long.Parse(line));
            }

            return c;
        }

        public static async Task<Channel<long>> ToChannelAsync(this int x)
        {
            var c = Channel.CreateUnbounded<long>();
            await c.Writer.WriteAsync(x);
            return c;
        }
    }
}
