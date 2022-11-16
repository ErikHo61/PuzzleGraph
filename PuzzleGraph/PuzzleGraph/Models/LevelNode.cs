using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models
{
    public class LevelNode
    {
        public List<LevelObject> LevelObjects { get => levelObjects; set => levelObjects = value; }

        private List<LevelObject> levelObjects;

        
    }
}
