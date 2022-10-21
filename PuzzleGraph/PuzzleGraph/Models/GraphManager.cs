using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using QuikGraph;
using PuzzleGraph.Models.Rules;

namespace PuzzleGraph.Models
{
    //Graph Storage
    //Responsible for mediating between the hostgraph and the canvas
    //Also responsible for creating the starting graph...temporarily
    class GraphManager
    {
        HostGraph hostGraph;
        GraphNode rootNode;
        Canvas cv;

        public GraphManager(Canvas cv) {
            hostGraph = new HostGraph();
            this.cv = cv;

            //Rectangle rect = new Rectangle
            //{
            //    Width = 50,
            //    Height = 50,
            //    Fill = new SolidColorBrush(Color.FromRgb(200, 60, 90)),
            //    Stroke = Brushes.Black
            //};

            //cv.Children.Add(rect);
        }

        //Sets up the graph with nodes and edges
        internal void graphSetup()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    GraphNode gn = new GraphNode();
                    Canvas.SetTop(gn, 60 + i * 70);
                    Canvas.SetLeft(gn, 100 + j * 70);
                    hostGraph.AddVertex(gn);
                    cv.Children.Add(gn);
                }

            }
            List<GraphNode> myl = hostGraph.Vertices.ToList();
            GraphNode gn0 = myl[0];
            GraphNode gn1 = myl[1];
            GraphNode gn2 = myl[2];
            GraphNode gn3 = myl[3];
            GraphNode gn4 = myl[4];

            GraphNode gn5 = myl[5];
            GraphNode gn6 = myl[6];
            GraphNode gn7 = myl[7];
            GraphNode gn8 = myl[8];
            GraphNode gn9 = myl[9];

            GraphNode gn10 = myl[10];
            GraphNode gn11 = myl[11];
            GraphNode gn12 = myl[12];
            GraphNode gn13 = myl[13];
            GraphNode gn14 = myl[14];

            GraphNode gn15 = myl[15];
            GraphNode gn16 = myl[16];
            GraphNode gn17 = myl[17];
            GraphNode gn18 = myl[18];
            GraphNode gn19 = myl[19];

            MakeAndAddEdge(gn5, gn0);
            MakeAndAddEdge(gn6, gn1);
            MakeAndAddEdge(gn7, gn2);
            MakeAndAddEdge(gn4, gn3);
            MakeAndAddEdge(gn9, gn4);
            //DataEdge de = new DataEdge(gn5, gn0);
            //DataEdge de2 = new DataEdge(gn6, gn1);
            //DataEdge de3 = new DataEdge(gn7, gn2);
            //DataEdge de4 = new DataEdge(gn4, gn3);
            //DataEdge de5 = new DataEdge(gn9, gn4);

            MakeAndAddEdge(gn6, gn5);
            MakeAndAddEdge(gn7, gn6);
            MakeAndAddEdge(gn8, gn7);
            MakeAndAddEdge(gn3, gn8);
            MakeAndAddEdge(gn14, gn9);
            //DataEdge de6 = new DataEdge(gn6, gn5);
            //DataEdge de7 = new DataEdge(gn7, gn6);
            //DataEdge de8 = new DataEdge(gn8, gn7);
            //DataEdge de9 = new DataEdge(gn3, gn8);
            //DataEdge de10 = new DataEdge(gn14, gn9);


            MakeAndAddEdge(gn5, gn10);
            MakeAndAddEdge(gn16, gn11);
            MakeAndAddEdge(gn12, gn13);
            MakeAndAddEdge(gn13, gn14);
            MakeAndAddEdge(gn18, gn13);
            
            //DataEdge de11 = new DataEdge(gn5, gn10);
            //DataEdge de12 = new DataEdge(gn16, gn11);
            //DataEdge de13 = new DataEdge(gn18, gn13);
            //DataEdge de14 = new DataEdge(gn13, gn14);
            //DataEdge de15 = new DataEdge(gn14, gn9);


            MakeAndAddEdge(gn10, gn15);
            MakeAndAddEdge(gn15, gn16);
            MakeAndAddEdge(gn16, gn17);
            MakeAndAddEdge(gn19, gn18);
            MakeAndAddEdge(gn14, gn19);

            MakeAndAddEdge(gn17, gn18, false);
            //DataEdge de16 = new DataEdge(gn10, gn15);
            //DataEdge de17 = new DataEdge(gn15, gn16);
            //DataEdge de18 = new DataEdge(gn16, gn17);
            //DataEdge de19 = new DataEdge(gn19, gn18);
        }

        internal void SmallGraphSetup() {
            int count = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    GraphNode gn = new GraphNode();
                    Canvas.SetTop(gn, 60 + i * 70);
                    Canvas.SetLeft(gn, 100 + j * 70);
                    hostGraph.AddVertex(gn);
                    cv.Children.Add(gn);
                }

            }
            List<GraphNode> myl = hostGraph.Vertices.ToList();
            GraphNode gn0 = myl[0];
            GraphNode gn1 = myl[1];
            GraphNode gn2 = myl[2];
            GraphNode gn3 = myl[3];
            gn0.Type = "C";
            gn1.Type = "CL";
            gn3.Type = "CL";
            gn2.Type = "C";
            MakeAndAddEdge(gn0, gn1);
            MakeAndAddEdge(gn1, gn3);
            MakeAndAddEdge(gn3, gn2);

            rootNode = gn0;

        }

        public void ExecuteGrammar(Rule rule) {
            var matches = SubgraphSearch(rule, rootNode);
            RuleReplacement(rule, matches);
        }

        //Makes an edge and adds it to the Canvas
        private void MakeAndAddEdge(GraphNode source, GraphNode target, bool path=true) {
            DataEdge de = new DataEdge(source, target) {
                isPath = path
            };
            hostGraph.AddEdge(de);
            cv.Children.Add(de);
        }

        //Performs the graph grammar replacement
        //Uses loops to first add nodes that are found in the rule graph,
        //then iteratively add nodes that are attached to the added nodes.

        private void RuleReplacement(Rule rule, List<Tuple<Morphism, Morphism>> matches) {
            if (matches.Count == 0) {
                Console.WriteLine("No Matches were found.");
                return;
            }
            var productNodeCount = rule.GetProductNodeCount();
            var ruleNodeCount = rule.GetRuleNodeCount();
            var pGraph = rule.GetProductVertices();

            bool replacementFinished = false;
            List<bool> states = Enumerable.Repeat(false, productNodeCount).ToList();
            //List of Morphisms that occurred in this rule replacement
            List<Morphism> morphs = new List<Morphism>();

            while (!replacementFinished)
            {
                for (int i = 0; i < pGraph.Count; i++)
                {
                    if (states[i]) {
                        continue;
                    }
                    Console.WriteLine("Replace Node #{0}", i);
                    var node = pGraph[i];
                    //get Rule graph Node using Product Graph ID
                    var ruleGraphNode = rule.GetRuleNode(node.ruleID);
                                   
                    //We already have a matching between host graph and rule graph
                    //Then we need a matching between hostgraph and product graph
                    if (!(ruleGraphNode is null)) //should i check the rule or the morphism list?
                    {
                        Tuple<Morphism, Morphism> match = GetFirstMatch(matches);
                        Morphism m;
                        if (node.ruleID == 1) {
                            m = match.Item1;  
                        } else  { // only product node IDs that exist in rule node is 1 and 2
                            m = match.Item2;  
                        }
                       
                        m.productGraphNode = node;
                        morphs.Add(m);

                        //perform replacement
                        m.hostGraphNode.Type = node.Type;
                        states[i] = true;
                    }
                    else if (!(checkAttached(node, rule, morphs) is null))
                    { //If there is no match for it, check if it is attached to a node that has already been replaced
                        //Then add the attached node and edge
                        var attachedNode = checkAttached(node, rule, morphs);
                        var newNode = new GraphNode() {
                            Type = node.Type
                        };

                        var edge = new DataEdge(attachedNode, newNode);
                        hostGraph.AddVertex(newNode);                    
                        hostGraph.AddEdge(edge);

                        states[i] = true;
                    }
                    else
                    {
                        states[i] = false;
                    }
                 
                }
                replacementFinished = !states.Contains(false);

            }
        }

        //  Finds the hostGraph node to attach a new graph node to.
        // 
        private GraphNode checkAttached(GraphNode pNode, Rule rule, List<Morphism> replacedMorphs)
        {
            //check if pNode is attached to another product node that has already been replaced in the graph
            var edgeIn = rule.GetProductInEdges(pNode);
            var edgeOut = rule.GetProductOutEdges(pNode);
            foreach (var morph in replacedMorphs) {
                for (int i = 0; i < edgeIn.Count(); i++) {
                    if (edgeIn[i].Source == morph.productGraphNode) {
                        //Find the hostGraph node equivalent to the above productGraph node
                        return morph.hostGraphNode;
                    } 
                }

                for (int i = 0; i < edgeOut.Count(); i++) {
                    if (edgeOut[i].Target == morph.productGraphNode)
                    {
                        return morph.hostGraphNode;

                    }
                }

            }
            return null;

        }

        public Tuple<Morphism, Morphism> GetFirstMatch(List<Tuple<Morphism, Morphism>> matches) {
            if (matches.Count <= 0) {
                Console.WriteLine("There are no matches");
                return null;
            }
            else {
                return matches[0];
            }
        }

        //Uses BFS and Searches for a rule match by searching through vertices
        //Requirement: Starting node must always be the entrance(root) node where the player spawns
        private List<Tuple<Morphism, Morphism>> SubgraphSearch(Rule rule, GraphNode startingNode) {

            //BFS Method
            //Create a list of Morphisms -mappings of matching nodes
            //
            List<Tuple<Morphism, Morphism>> matches = new List<Tuple<Morphism, Morphism>>();
            Queue<GraphNode> nodesQueue = new Queue<GraphNode>();

            var visited = Enumerable.Repeat(false, 100).ToList();
            visited[startingNode.graphID] = true;
            nodesQueue.Enqueue(startingNode);

            int ruleNodeCount = rule.GetRuleNodeCount();
            bool matchingBegins = false;
            var matchingNodeTypes = rule.GetNodeTypes();
            var firstNodeType = matchingNodeTypes.Item1;
            while (nodesQueue.Count != 0)
            {
                var node = nodesQueue.Dequeue();
                Console.WriteLine(node);
                var adjNodes = GetAdjNodes(node);
                              
                if (node.Type == firstNodeType) {
                    if (ruleNodeCount > 1) {
                        matchingBegins = true;

                    } else matches.Add(new Tuple<Morphism, Morphism>(
                            new Morphism(ruleGraphNode: rule.GetRuleNode(1), hostGraphNode: node), 
                            null));

                }
                foreach (GraphNode n in adjNodes)
                {
                    if (!visited[n.graphID])
                    {
                        visited[n.graphID] = true;
                        nodesQueue.Enqueue(n);
                    }
                    //
                    if (ruleNodeCount == 2 && matchingBegins)
                    {
                        if (n.Type == matchingNodeTypes.Item2)
                        {
                            matches.Add(new Tuple<Morphism, Morphism>(
                                new Morphism(ruleGraphNode: rule.GetRuleNode(1), hostGraphNode: node),
                                new Morphism(ruleGraphNode: rule.GetRuleNode(2), hostGraphNode: n)));
                        }
                    }
                }

                matchingBegins = false;
            }
            return matches;
        }

        //gets out edges from hostgraph
        private List<GraphNode> GetAdjNodes(GraphNode node) {
            var edges = hostGraph.OutEdges(node);
            List<GraphNode> adjacentNodes = new List<GraphNode>();
            foreach (var edge in edges) {
                adjacentNodes.Add(edge.Target);
            }
            return adjacentNodes;
        }

        
        //gets in edges from hostgraph
        private List<GraphNode> GetInAdjNodes(GraphNode node)
        {
            var edges = hostGraph.InEdges(node);
            List<GraphNode> adjacentNodes = new List<GraphNode>();
            foreach (var edge in edges)
            {
                adjacentNodes.Add(edge.Target);
            }
            return adjacentNodes;
        }

        public void printGraph() {
            hostGraph.printVertices();
        }
    }
}
