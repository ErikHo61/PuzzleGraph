using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models
{   
    //Rule class
    //Responsible for executing a rule. It will determine whether a match is found between the host graph and the rule graph
    //
    public abstract class Rule
    {
        //contains the graph to be matched with the host graph
        protected DataGraph RuleGraph;
        //contains the replacement graph
        protected DataGraph ProductGraph;

        public Rule(string name) {
            //initialize rulegraph
            RuleGraph = new DataGraph();
            ProductGraph = new DataGraph();
            InitRuleGraph();
            InitProductGraph();
        }

        public List<DataEdge> GetProductOutEdges(GraphNode node) {
            return (List<DataEdge>) ProductGraph.OutEdges(node);
        }

        public List<DataEdge> GetProductInEdges(GraphNode node)
        {
            return (List<DataEdge>)ProductGraph.InEdges(node);
        }

        public GraphNode GetRuleNode(int ID) {
            return RuleGraph.getRuleNode(ID);      
        }

        public int GetRuleNodeCount() {
            return RuleGraph.VertexCount;
        }

        public int GetProductNodeCount()
        {
            return ProductGraph.VertexCount;
        }

        public int GetRuleEdgeCount() {
            return RuleGraph.EdgeCount;
        }

        public int GetProductEdgeCount() {
            return ProductGraph.EdgeCount;
        }

        public List<GraphNode> GetRuleVertices() {
            return RuleGraph.Vertices.ToList();
        }

        public List<GraphNode> GetProductVertices() {
            return ProductGraph.Vertices.ToList();
        }


        public Tuple<string, string> GetNodeTypes() {
            int vertCount = RuleGraph.VertexCount;
            var verts = RuleGraph.Vertices.ToList();

            if (vertCount == 1)
            {
                return new Tuple<string, string>(verts[0].Type, null);
            }
            else { //There is 2
                return new Tuple<string, string>(verts[0].Type, verts[1].Type);
            }
        }

        protected abstract void InitRuleGraph(); 
        //{           
            //GraphNode gn = new GraphNode()
            //{
            //    Type = "S",
            //    ID = 1
            //};
            //GraphNode gn2 = new GraphNode()
            //{
            //    Type = "e",
            //    ID = 2
            //};

            //DataEdge de = new DataEdge(gn, gn2);

            //RuleGraph.AddVertex(gn);
            //RuleGraph.AddVertex(gn2);
                   
            
            
           
        //}

        protected abstract void InitProductGraph();
        //{
        //    //GraphNode gn = new GraphNode();
        //    //GraphNode gn2 = new GraphNode();
        //    //DataEdge de = new DataEdge(gn, gn2);

        //    switch (name) {
        //        case "start":
        //            gn.Type = "S";
        //            gn.ID = 1;
        //            gn2.Type = "e";
        //            gn2.ID = 2;
                  

        //            DataEdge de = new DataEdge(gn, gn2);

        //            RuleGraph.AddVertex(gn);
        //            RuleGraph.AddVertex(gn2);

        //            break;
        //        case "LinearChain":
        //            gn.Type = "S";
        //            gn.ID = 1;
        //            gn2.Type = "e";
        //            gn2.ID = 2;


        //            DataEdge de = new DataEdge(gn, gn2);

        //            RuleGraph.AddVertex(gn);
        //            RuleGraph.AddVertex(gn2);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //Searches for the matching subgraph in the host graph
        //And returns nodes that will be replaced? or
        public abstract void SubgraphSearch(DataGraph hostGraph);
        //{
        //    //foreach (GraphNode vertex in hostGraph.Vertices)
        //    //{
        //    //    foreach (DataEdge edge in hostGraph.InEdges(vertex))
        //    //    {
        //    //        //nothing so far
        //    //    }
        //    //}
        //}

        //
        //Input: Host graph and nodes that will be replaced?
        public abstract void ApplyRule(DataGraph hostGraph);
    }

        
    
}
