using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    class RuleMultiKey : Rule
    {
        protected override void InitProductGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "fn",
                ruleID = 1
            };
            GraphNode gn2 = new GraphNode()
            {
                Type = "km",
                ruleID = 2
            };
            GraphNode gn3 = new GraphNode()
            {
                Type = "km",
                ruleID = 3
            };
            GraphNode gn4 = new GraphNode()
            {
                Type = "lm",
                ruleID = 4
            };

            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            ProductGraph.AddVertex(gn3);
            ProductGraph.AddVertex(gn4);

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn, gn3);
            DataEdge de3 = new DataEdge(gn2, gn4);
            DataEdge de4 = new DataEdge(gn3, gn4);

            ProductGraph.AddEdge(de);
            ProductGraph.AddEdge(de2);
            ProductGraph.AddEdge(de3);
            ProductGraph.AddEdge(de4);
        }

        protected override void InitRuleGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "PC",
                ruleID = 1
            };

            GraphNode gn2 = new GraphNode()
            {
                Type = "G",
                ruleID = 2
            };
            DataEdge de = new DataEdge(gn, gn2);

            RuleGraph.AddVertex(gn);
            RuleGraph.AddVertex(gn2);

            RuleGraph.AddEdge(de);
        }
    }
}
