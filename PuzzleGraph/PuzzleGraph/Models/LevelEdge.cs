using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models
{
    public class LevelEdge : DataEdgeBase<LevelNode>
    {

        public bool activated = false;

        public LevelEdge(LevelNode source, LevelNode target) : base(source, target)
        { 
        
        }

            public LevelEdge()
           : base(null, null)
        {
        }
    }
}
