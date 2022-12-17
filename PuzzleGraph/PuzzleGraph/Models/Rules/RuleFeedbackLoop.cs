using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    class RuleFeedbackLoop : Rule
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
                Type = "ppfl",
                ruleID = 2
            };
            GraphNode gn3 = new GraphNode()
            {
                Type = "pifl",
                ruleID = 3
            };
            GraphNode gn4 = new GraphNode()
            {
                Type = "ppfl",
                ruleID = 4
            };
            GraphNode gn5 = new GraphNode()
            {
                Type = "ppfl",
                ruleID = 5
            };
            GraphNode gn6 = new GraphNode()
            {
                Type = "k",
                ruleID = 6
            };
            GraphNode gn7 = new GraphNode()
            {
                Type = "l",
                ruleID = 7
            };
            gn3.actChildren = new List<GraphNode>()
            {
                gn2, gn4, gn5
            };
            gn3.coupleNode = gn6;

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn2, gn3);
            DataEdge de3 = new DataEdge(gn3, gn4);
            DataEdge de4 = new DataEdge(gn4, gn5);
            DataEdge de5 = new DataEdge(gn5, gn6);
            DataEdge de6 = new DataEdge(gn6, gn7);

            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            ProductGraph.AddVertex(gn3);
            ProductGraph.AddVertex(gn4);
            ProductGraph.AddVertex(gn5);
            ProductGraph.AddVertex(gn6);
            ProductGraph.AddVertex(gn7);

            ProductGraph.AddEdge(de);
            ProductGraph.AddEdge(de2);
            ProductGraph.AddEdge(de3);
            ProductGraph.AddEdge(de4);
            ProductGraph.AddEdge(de5);
            ProductGraph.AddEdge(de6);
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
