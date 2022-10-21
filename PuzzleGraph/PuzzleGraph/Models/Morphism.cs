using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PuzzleGraph.Models
{
    // Morphism
    // This class is responsible for helping with the graph grammar replacement algorithm by
    // aiding in mapping the nodes between the Rule Graph. Host graph and Product graph.
    class Morphism
    {
        //Three nodes that are all mapped together in this Morphism
        public GraphNode ruleGraphNode { get; set; }
        public GraphNode hostGraphNode { get; set; }
        public GraphNode productGraphNode { get; set; }

        


        public Morphism(GraphNode ruleGraphNode = null, GraphNode hostGraphNode = null, 
        GraphNode productGraphNode=null){
            this.ruleGraphNode = ruleGraphNode;
            this.hostGraphNode = hostGraphNode;
            this.productGraphNode = productGraphNode;
     

        }
       
        

        
    }
}
