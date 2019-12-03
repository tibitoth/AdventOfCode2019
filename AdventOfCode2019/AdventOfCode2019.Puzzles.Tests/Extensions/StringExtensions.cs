using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2019.Puzzles.Tests.Extensions
{
    public static class StringExtensions
    {
        public static Stream ToMemoryStream(this string s) => new MemoryStream(Encoding.UTF8.GetBytes(s));
    }
}
