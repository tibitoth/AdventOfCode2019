using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;
using AdventOfCode2019.Puzzles.Intcode;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2019.Puzzles.Day13
{
    internal class Tile
    {
        public TileType TileType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
