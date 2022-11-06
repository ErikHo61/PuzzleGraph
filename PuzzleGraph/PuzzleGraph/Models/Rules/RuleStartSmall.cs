using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    public class RuleStartSmall : Rule
    {
        protected override void InitRuleGraph()
        {

            GraphNode gn = new GraphNode()
            {
                Type = "S",
                ruleID = 1
            };
          
            RuleGraph.AddVertex(gn);
        }

        protected override void InitProductGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "TP",
                ruleID = 1
            };
            GraphNode gn2 = new GraphNode()
            {
                Type = "PC",
                ruleID = 2
            };
            GraphNode gn3 = new GraphNode()
            {
                Type = "PC",
                ruleID = 3
            };
            GraphNode gn4 = new GraphNode()
            {
                Type = "TP",
                ruleID = 4
            };
            GraphNode gn5 = new GraphNode()
            {
                Type = "g",
                ruleID = 5
            };

            DataEdge de = new DataEdge(gn, gn2);
            DataEdge de2 = new DataEdge(gn2, gn3);
            DataEdge de3 = new DataEdge(gn3, gn4);
            DataEdge de4 = new DataEdge(gn4, gn5);


            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            ProductGraph.AddVertex(gn3);
            ProductGraph.AddVertex(gn4);
            ProductGraph.AddVertex(gn5);

            ProductGraph.AddEdge(de);
            ProductGraph.AddEdge(de2);
            ProductGraph.AddEdge(de3);
            ProductGraph.AddEdge(de4);

        }

        
    }
}
