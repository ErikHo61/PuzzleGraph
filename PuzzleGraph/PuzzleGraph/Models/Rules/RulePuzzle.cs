using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    class RulePuzzle : Rule
    {

        protected override void InitProductGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "pp",
                ruleID = 1
            };
            GraphNode gn2 = new GraphNode()
            {
                Type = "pi",
                ruleID = 2,
                actChildren = new List<GraphNode>() {gn}
            };

            GraphNode gn3 = new GraphNode()
            {
                Type = "k",
                ruleID = 3
            };
            GraphNode gn4 = new GraphNode()
            {
                Type = "l",
                ruleID = 4,
                actChildren = new List<GraphNode>() { 
                    gn3
                }
            };

            gn2.coupleNode = gn3;

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn2, gn3);
            DataEdge de3 = new DataEdge(gn3, gn4);

            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            ProductGraph.AddVertex(gn3);
            ProductGraph.AddVertex(gn4);

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
