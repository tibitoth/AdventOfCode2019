using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Extensions;

namespace AdventOfCode2019.Puzzles.Day14
{
    [Day(14)]
    public class SpaceStoichiometry : IPuzzleSolver
    {
        private readonly Dictionary<string, ChemicalReaction> _chemicalReactions = new Dictionary<string, ChemicalReaction>();
        private readonly Dictionary<string, int> _inventory = new Dictionary<string, int>();

        private async Task ParseInputAsync(Stream input)
        {
            await foreach (var line in input.AsAsyncEnumerable())
            {
                // example: 7 A, 1 D => 1 E

                var split = line.Split(" => ");
                var name = split[1].Trim().Split(" ")[1];
                var c = new ChemicalReaction()
                {
                    Name = name,
                    Amount = int.Parse(split[1].Trim().Split(" ")[0]),
                };

                foreach (var i in split[0].Trim().Split(", "))
                {
                    c.Ingredients.Add(new ChemicalIngredient()
                    {
                        Chemical = i.Split(" ")[1],
                        Amount = int.Parse(i.Split(" ")[0])
                    });
                }

                _chemicalReactions[name] = c;
                _inventory[name] = 0;
            }
        }

        public async Task<string> SolvePart1Async(Stream input)
        {
            await ParseInputAsync(input);

            return CreateChemical("FUEL", 1).ToString();
        }

        private int CreateChemical(string name, int amount)
        {
            if (name == "ORE")
            {
                return amount;
            }

            if (_inventory[name] >= amount)
            {
                _inventory[name] -= amount;
                return 0;
            }
            else
            {
                var required = amount - _inventory[name];
                var reactionAmount = required / _chemicalReactions[name].Amount + (required % _chemicalReactions[name].Amount != 0 ? 1 : 0);

                _inventory[name] += reactionAmount * _chemicalReactions[name].Amount;
                _inventory[name] -= amount;

                return _chemicalReactions[name].Ingredients.Sum(i => CreateChemical(i.Chemical, reactionAmount * i.Amount));
            }
        }

        public async Task<string> SolvePart2Async(Stream input)
        {
            await ParseInputAsync(input);
            return (1000000000000 / CreateChemical("FUEL", 1)).ToString();
        }
    }
}
