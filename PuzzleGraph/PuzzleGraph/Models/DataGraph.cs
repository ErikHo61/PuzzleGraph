using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph;
using PuzzleGraph.CustomControls;

namespace PuzzleGraph.Models
{
    public class DataGraph : BidirectionalGraph<GraphNode, DataEdge>
    {
        public DataGraph()
        {
        }

        //Get node matching graphID from the graph
        public GraphNode getGraphNode(int graphID) {
            var verts = Vertices.ToList();
            foreach (GraphNode vert in verts) {
                if (vert.graphID == graphID) {
                    return vert;
                }
            }
            return null;
        }

        public GraphNode getRuleNode(int ruleID) {
            var verts = Vertices.ToList();
            foreach (GraphNode vert in verts)
            {
                if (vert.ruleID == ruleID)
                {
                    return vert;
                }
            }
            return null;
        }

        

        public void deleteEdge(GraphNode source, GraphNode target) {
            throw new NotImplementedException();
        }

        public void deleteNode() {
            throw new NotImplementedException();
        }

        public void printVertices() {
            foreach (var vert in Vertices.ToList()) {
                Console.WriteLine(vert);
            }
            
        }
    }
}
