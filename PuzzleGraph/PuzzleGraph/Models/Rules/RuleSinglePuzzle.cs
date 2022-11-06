using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    class RuleSinglePuzzle : Rule
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
                Type = "pp",
                ruleID = 2
            };

            GraphNode gn3 = new GraphNode()
            {
                Type = "pi",
                ruleID = 3
            };

            GraphNode gn4 = new GraphNode()
            {
                Type = "pr",
                ruleID = 4
            };

            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            ProductGraph.AddVertex(gn3);
            ProductGraph.AddVertex(gn4);

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn2, gn3);
            DataEdge de3 = new DataEdge(gn3, gn4);

            ProductGraph.AddEdge(de);
            ProductGraph.AddEdge(de2);
            ProductGraph.AddEdge(de3);
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
                Type = "PR",
                ruleID = 2
            };


            RuleGraph.AddVertex(gn);
            RuleGraph.AddVertex(gn2);

            DataEdge de = new DataEdge(gn, gn2);

            RuleGraph.AddEdge(de);

        }
    }
}
