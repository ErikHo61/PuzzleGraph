using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models
{
    public class HostGraph:DataGraph
    {
        int graphIDCount = 0;
        public override bool AddVertex(GraphNode vertex)
        {
            vertex.graphID = graphIDCount++;
            return base.AddVertex(vertex);
        }

        internal List<GraphNode> GetAdjNodes(GraphNode node)
        {
            var edges = OutEdges(node);
            List<GraphNode> adjacentNodes = new List<GraphNode>();
            foreach (var edge in edges)
            {
                adjacentNodes.Add(edge.Target);
            }
            return adjacentNodes;
        }
    }
}
