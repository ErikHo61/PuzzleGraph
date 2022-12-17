using PuzzleGraph.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Recipes
{
    class RecipeMulti : Recipe
    {
        protected override void InitRecipe()
        {
            rules = new List<Rule>();
            rules.Add(new RuleStartSmall());
            rules.Add(new RulePuzzleReward());
            rules.Add(new RuleDoublePuzzle());
            rules.Add(new RuleGate());
            rules.Add(new RuleMultiKey());
        }
    }
}
