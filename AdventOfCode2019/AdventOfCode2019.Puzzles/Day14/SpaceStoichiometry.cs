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
        public async Task<string> SolvePart1Async(Stream input)
        {
            var chemicalReactions = new Dictionary<string, ChemicalReaction>();
            var inventory = new Dictionary<string, int>();

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

                chemicalReactions[name] = c;
                inventory[name] = 0;
            }

            int CreateChemical(string name, int amount)
            {
                if (name == "ORE")
                {
                    return amount;
                }

                if (inventory[name] >= amount)
                {
                    inventory[name] -= amount;
                    return 0;
                }
                else
                {
                    var required = amount - inventory[name];
                    var reactionAmount = required / chemicalReactions[name].Amount + (required % chemicalReactions[name].Amount != 0 ? 1 : 0);

                    inventory[name] += reactionAmount * chemicalReactions[name].Amount;
                    inventory[name] -= amount;

                    return chemicalReactions[name].Ingredients.Sum(i => CreateChemical(i.Chemical, reactionAmount * i.Amount));
                } 
            }

            return CreateChemical("FUEL", 1).ToString();
        }

        public Task<string> SolvePart2Async(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}
