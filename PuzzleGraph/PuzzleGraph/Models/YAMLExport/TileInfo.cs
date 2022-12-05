using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.YAMLExport
{
    public class TileInfo
    {
        public string type { get; set; }
        public int uid { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int graphID { get; set; }
        public int coupleID { get; set; }
        public string actChildren { get; set; }

    }
}
