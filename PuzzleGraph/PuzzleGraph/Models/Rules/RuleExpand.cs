﻿using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.Rules
{
    class RuleExpand : Rule
    {

        protected override void InitProductGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "PC",
                ruleID = 1
            };
            GraphNode gn2 = new GraphNode()
            {
                Type = "PC",
                ruleID = 2
            };

            DataEdge de = new DataEdge(gn, gn2);
         
            ProductGraph.AddVertex(gn);
            ProductGraph.AddVertex(gn2);
           
            ProductGraph.AddEdge(de);          
        }

        protected override void InitRuleGraph()
        {
            GraphNode gn = new GraphNode()
            {
                Type = "PC",
                ruleID = 1
            };
           

           

            RuleGraph.AddVertex(gn);
            

            
        }
    }
}
