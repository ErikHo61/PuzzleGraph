using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    class RuleSingleLock : Rule
    {
        protected override void InitProductGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "k",
                ruleID = 1
            };
            GraphNode gn2 = new GraphNode()
            {
                Type = "l",
                ruleID = 2
            };
            GraphNode gn3 = new GraphNode()
            {
                Type = "pr",
                ruleID = 3
            };

            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            ProductGraph.AddVertex(gn3);

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn2, gn3);

            ProductGraph.AddEdge(de);
            ProductGraph.AddEdge(de2);
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
