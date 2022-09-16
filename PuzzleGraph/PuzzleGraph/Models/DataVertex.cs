using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models
{
    public class DataVertex
    {
        public string Text { get; set; }
        public int nodeNumber { get; set; }
        public List<LevelObject> levelObjects;
    }
}
