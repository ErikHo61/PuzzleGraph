using PuzzleGraph.CustomControls;
using PuzzleGraph.Models.ShapeGrammars.DungeonStructure;
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

        public DungeonPathway openings;

        public Rule() {
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

        public GraphNode GetLastProductNode() {
            var nodes = GetProductVertices();
            var lastID = 0;
            foreach (var node in nodes) {
                if (node.ruleID > lastID) {
                    lastID = node.ruleID;
                }
            }
            return ProductGraph.getRuleNode(lastID);
        }

        public GraphNode GetLastRuleNode() {
            var nodes = GetRuleVertices();
            var lastID = 0;
            foreach (var node in nodes)
            {
                if (node.ruleID > lastID)
                {
                    lastID = node.ruleID;
                }
            }
            return RuleGraph.getRuleNode(lastID);
        }

        public List<DataEdge> GetRuleEdges() {
            return RuleGraph.Edges.ToList();
        }

        public List<DataEdge> GetProductEdges() {
            return ProductGraph.Edges.ToList();
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
        
        protected abstract void InitProductGraph();

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }

        
    
}
