using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day3.Models
{
    public struct Cell
    {
        private Dictionary<int, int> _wires;

        public void AddWire(int wireId, int length) => (_wires ?? (_wires = new Dictionary<int, int>()))[wireId] = length;

        public bool IsCrossingWithOtherWires(int wireId) => _wires.Keys.Except(new[] { wireId }).Count() > 0;

        public List<int> GetOtherWireLength(int wireId) => _wires.Where(x => x.Key != wireId).Select(x => x.Value).ToList();
    }
}
