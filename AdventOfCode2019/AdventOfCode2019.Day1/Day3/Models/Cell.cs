using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day3.Models
{
    public struct Cell
    {
        private byte _wires;

        public void AddWire(int wireId) => _wires |= (byte)(1 << wireId);

        public bool IsCrossingWithOtherWires(int wireId) => (~(1 << wireId) & _wires) > 0;
    }
}
