using PuzzleGraph.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Recipes
{
    class RecipeSmall : Recipe
    {
        protected override void InitRecipe()
        {
            rules = new List<Rule>();
            rules.Add(new RuleStartMedium());
            rules.Add(new RuleKeyLockItem());
            rules.Add(new RuleSingleLock());
        }
    }
}
