using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    public class RuleStart : Rule
    {

        public RuleStart() : base() {
            //RuleGraph = new DataGraph();
            //ProductGraph = new DataGraph();
            //InitRuleGraph();
            //InitProductGraph();
            name = "Rule Start";
        } 

        protected override void InitRuleGraph()
        {

            GraphNode gn = new GraphNode()
            {
                Type = "CL",
                ruleID = 1
            };
            GraphNode gn2 = new GraphNode()
            {
                Type = "CL",
                ruleID = 2
            };

            DataEdge de = new DataEdge(gn, gn2);

            RuleGraph.AddVertex(gn);
            RuleGraph.AddVertex(gn2);

            RuleGraph.AddEdge(de);
            
        }

        protected override void InitProductGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "S",
                ruleID = 1
            };
            GraphNode gn2 = new GraphNode()
            {
                Type = "G",
                ruleID = 2
            };

            GraphNode gn3 = new GraphNode()
            {
                Type = "H",
                ruleID = 3
            };

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn2, gn3);

            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            ProductGraph.AddVertex(gn3);

            ProductGraph.AddEdge(de);
            ProductGraph.AddEdge(de2);
        }

        
    }
}
