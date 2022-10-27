using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models
{
    //Contains an execution list of Rules. Rules are executed inorder.
    public abstract class Recipe
    {
        protected List<Rule> rules;

        public Recipe(){
            InitRecipe();
        }

        protected abstract void InitRecipe();

        public List<Rule> getRules() {
            return rules;
        }
    }
}
