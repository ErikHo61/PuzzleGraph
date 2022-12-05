using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    class RuleDoublePuzzle : Rule
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
                Type = "pp",
                ruleID = 3
            };

            GraphNode gn4 = new GraphNode()
            {
                Type = "pi",
                ruleID = 4,
                actChildren = new List<GraphNode>() { 
                    gn3                
                }
            };

            GraphNode gn5 = new GraphNode()
            {
                Type = "pr",
                ruleID = 5
            };

            gn4.coupleNode = gn5;

            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            ProductGraph.AddVertex(gn3);
            ProductGraph.AddVertex(gn4);
            ProductGraph.AddVertex(gn5);

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn, gn3);
            DataEdge de3 = new DataEdge(gn2, gn4);
            DataEdge de4 = new DataEdge(gn3, gn4);
            DataEdge de5 = new DataEdge(gn4, gn5);

            ProductGraph.AddEdge(de);
            ProductGraph.AddEdge(de2);
            ProductGraph.AddEdge(de3);
            ProductGraph.AddEdge(de4);
            ProductGraph.AddEdge(de5);
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
