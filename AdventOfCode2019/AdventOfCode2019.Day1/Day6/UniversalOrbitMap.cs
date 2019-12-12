using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;

namespace AdventOfCode2019.Puzzles.Day6
{
    [Day(6)]
    public class UniversalOrbitMap : IPuzzleSolver
    {
        public async Task<string> SolvePart1Async(Stream input)
        {
            var nodes = await CreateNodesAsync(input);

            int sum = 0;
            foreach (var node in nodes.Values)
            {
                var count = 0;
                var n = node;
                while (n.Id != "COM")
                {
                    count++;
                    n = n.OrbitAround;
                }

                sum += count;
            }

            return sum.ToString();
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            var nodes = await CreateNodesAsync(input);

            List<Node> GetNodesToCom(string fromNodeId)
            {
                var nodesToCom = new List<Node>();
                var node = nodes[fromNodeId];
                while (node.OrbitAround != null)
                {
                    nodesToCom.Add(node.OrbitAround);
                    node = node.OrbitAround;
                }

                return nodesToCom;
            }

            var nodesFromSanToCom = GetNodesToCom("SAN");
            var nodesFromYouToCom = GetNodesToCom("YOU");

            var commonNodes = nodesFromYouToCom.Intersect(nodesFromSanToCom);
            var result = nodesFromYouToCom.Except(commonNodes).Count() + nodesFromSanToCom.Except(commonNodes).Count();

            return result.ToString();
        }

        private async Task<Dictionary<string, Node>> CreateNodesAsync(Stream input)
        {
            var nodes = new Dictionary<string, Node>();
            await foreach (var line in input.AsAsyncEnumerable())
            {
                var orbit = line.Split(")");
                if (!nodes.ContainsKey(orbit[0]))
                {
                    nodes.Add(orbit[0], new Node(orbit[0]));
                }

                if (!nodes.ContainsKey(orbit[1]))
                {
                    nodes.Add(orbit[1], new Node(orbit[1]) { OrbitAround = nodes[orbit[0]] });
                }
                else
                {
                    nodes[orbit[1]].OrbitAround = nodes[orbit[0]];
                }
            }

            return nodes;
        }
    }
}
