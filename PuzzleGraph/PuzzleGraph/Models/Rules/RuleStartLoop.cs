using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    //Modeled after Re:Village House Beneviento's basement
    class RuleStartLoop : Rule
    {
        
        protected override void InitProductGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "e",
                ruleID = 1
            };
            GraphNode gn2 = new GraphNode()
            {
                Type = "fn",
                ruleID = 2
            };
            GraphNode gn3 = new GraphNode()
            {
                Type = "PC",
                ruleID = 3
            };
            GraphNode gn4 = new GraphNode()
            {
                Type = "PC",
                ruleID = 4
            };

            GraphNode gn5 = new GraphNode()
            {
                Type = "fn",
                ruleID = 5
            };
            GraphNode gn6 = new GraphNode()
            {
                Type = "PC",
                ruleID = 6
            };

            GraphNode gn7 = new GraphNode()
            {
                Type = "g",
                ruleID = 7
            };

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn2, gn3);
            DataEdge de3 = new DataEdge(gn2, gn4);
            DataEdge de4 = new DataEdge(gn3, gn5);
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
            //ProductGraph.AddEdge(de7);
        }

        protected override void InitRuleGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "S",
                ruleID = 1
            };

            RuleGraph.AddVertex(gn);
        }
    }
}
