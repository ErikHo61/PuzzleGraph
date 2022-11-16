using PuzzleGraph.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Recipes
{
    class RecipeBasic : Recipe
    {
        public RecipeBasic() {
            InitRecipe();
        }

        protected override void InitRecipe()
        {
            rules = new List<Rule>();
            rules.Add(new RuleExpand());
            rules.Add(new RuleStartSmall());
            
        }

        
    }
}
