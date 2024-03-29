﻿using PuzzleGraph.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Recipes
{
    class RecipeLoop : Recipe
    {
        protected override void InitRecipe()
        {
            rules = new List<Rule>();
            rules.Add(new RuleStartSmall());
            rules.Add(new RuleGate());
            rules.Add(new RuleFeedbackLoop());
            rules.Add(new RuleGate());
            rules.Add(new RuleSingleLock());
        }
    }
}
