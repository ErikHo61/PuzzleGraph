using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    //WIP
    public class RulePuzzleToPuzzle : Rule
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
            GraphNode gn6 = new GraphNode()
            {
                Type = "pi",
                ruleID = 6,
                actChildren = new List<GraphNode>() { 
                    gn2, gn5
                }
            };
            GraphNode gn7 = new GraphNode()
            {
                Type = "k",
                ruleID = 7
            };

            GraphNode gn8 = new GraphNode()
            {
                Type = "l",
                ruleID = 8,
                actChildren = new List<GraphNode>() { 
                    gn7
                }
            };


            gn4.coupleNode = gn5;
            gn6.coupleNode = gn7;

            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            ProductGraph.AddVertex(gn3);
            ProductGraph.AddVertex(gn4);
            ProductGraph.AddVertex(gn5);
            ProductGraph.AddVertex(gn6);
            ProductGraph.AddVertex(gn7);
            ProductGraph.AddVertex(gn8);

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn, gn3);
            DataEdge de3 = new DataEdge(gn2, gn4);
            DataEdge de4 = new DataEdge(gn3, gn6);
            DataEdge de5 = new DataEdge(gn4, gn5);
            DataEdge de6 = new DataEdge(gn5, gn6);
            DataEdge de7 = new DataEdge(gn6, gn7);
            DataEdge de8 = new DataEdge(gn7, gn8);

            ProductGraph.AddEdge(de);
            ProductGraph.AddEdge(de2);
            ProductGraph.AddEdge(de3);
            ProductGraph.AddEdge(de4);
            ProductGraph.AddEdge(de5);
            ProductGraph.AddEdge(de6);
            ProductGraph.AddEdge(de7);
            ProductGraph.AddEdge(de8);
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


            RuleGraph.AddVertex(gn);
            RuleGraph.AddVertex(gn2);

            DataEdge de = new DataEdge(gn, gn2);
            RuleGraph.AddEdge(de);
        }
    }
}
