using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2019.Puzzles.Extensions
{
    public static class ChannelExtensions
    {
        public static async Task<Channel<T>> ToChannelAsync<T>(this IEnumerable<T> source)
        {
            var channel = Channel.CreateUnbounded<T>();

            foreach (var x in source)
            {
                await channel.Writer.WriteAsync(x);
            }

            channel.Writer.Complete();

            return channel;
        }
    }
}
