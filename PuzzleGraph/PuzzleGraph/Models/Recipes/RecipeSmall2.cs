using PuzzleGraph.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Recipes
{
    class RecipeSmall2 : Recipe
    {
        protected override void InitRecipe()
        {
            rules = new List<Rule>();
            rules.Add(new RuleStartSmall2());
            rules.Add(new RuleGate());
            rules.Add(new RulePuzzleToPuzzle());
            rules.Add(new RuleGate());
            rules.Add(new RuleSingleLock());
        }
    }
}
