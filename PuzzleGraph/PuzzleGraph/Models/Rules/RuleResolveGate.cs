﻿using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    class RuleResolveGate : Rule
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

            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
            DataEdge de = new DataEdge(gn, gn2);
            ProductGraph.AddEdge(de);
        }

        protected override void InitRuleGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "G",
                ruleID = 1
            };

            RuleGraph.AddVertex(gn);
        }
    }
}
