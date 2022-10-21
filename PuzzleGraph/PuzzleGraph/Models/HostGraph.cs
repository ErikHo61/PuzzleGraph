using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models
{
    class HostGraph:DataGraph
    {
        int graphIDCount = 0;
        public override bool AddVertex(GraphNode vertex)
        {
            vertex.graphID = graphIDCount++;
            return base.AddVertex(vertex);
        }
    }
}
