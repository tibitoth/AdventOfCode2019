using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day6
{
    public class Node
    {
        public string Id { get; }
        public Node OrbitAround { get; set; }

        public Node(string id)
        {
            Id = id;
        }
    }
}
