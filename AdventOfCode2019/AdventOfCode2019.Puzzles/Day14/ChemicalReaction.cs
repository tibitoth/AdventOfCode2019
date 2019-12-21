using System.Collections.Generic;

namespace AdventOfCode2019.Puzzles.Day14
{
    internal class ChemicalReaction
    {
        public int Amount { get; set; }
        public string Name { get; set; }
        public List<ChemicalIngredient> Ingredients { get; } = new List<ChemicalIngredient>();
    }
}