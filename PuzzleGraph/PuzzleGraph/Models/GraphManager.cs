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
        public HostGraph hostGraph { get; set; }
        public GraphNode rootNode { get; set; }
        Canvas cv;
        GridManager gridMan;
        

        public GraphManager(Canvas cv) {
            hostGraph = new HostGraph();
            gridMan = new GridManager(12, 12);
            this.cv = cv;
            
        }

        //Sets up the graph with nodes and edges
        internal void graphSetup()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    GraphNode gn = new GraphNode();
                    Canvas.SetTop(gn, 60 + i * 60);
                    Canvas.SetLeft(gn, 100 + j * 60);
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

            MakeAndAddEdge(gn6, gn5);
            MakeAndAddEdge(gn7, gn6);
            MakeAndAddEdge(gn8, gn7);
            MakeAndAddEdge(gn3, gn8);
            MakeAndAddEdge(gn14, gn9);

            MakeAndAddEdge(gn5, gn10);
            MakeAndAddEdge(gn16, gn11);
            MakeAndAddEdge(gn12, gn13);
            MakeAndAddEdge(gn13, gn14);
            MakeAndAddEdge(gn18, gn13);           

            MakeAndAddEdge(gn10, gn15);
            MakeAndAddEdge(gn15, gn16);
            MakeAndAddEdge(gn16, gn17);
            MakeAndAddEdge(gn19, gn18);
            MakeAndAddEdge(gn14, gn19);

            MakeAndAddEdge(gn17, gn18, false);
        }

        internal void SmallGraphSetup() {

            
            GraphNode gn = new GraphNode();
            Canvas.SetTop(gn, 60);
            Canvas.SetLeft(gn, 100);
                    
            hostGraph.AddVertex(gn);
            cv.Children.Add(gn);
          
            List<GraphNode> myl = hostGraph.Vertices.ToList();
            gridMan.InitGrid(myl);
            GraphNode gn0 = myl[0];
            
            gn0.Type = "S";
            rootNode = gn0;
        }

        public void ExecuteRecipe(Recipe recipe) {
            var rules = recipe.getRules();
            foreach (var rule in rules) {
                ExecuteGrammar(rule);             
            }
        }

        //public HostGraph GetHostGraph() {
        //    return hostGraph;
        //}

        public bool ExecuteGrammar(Rule rule) {
            //Console.WriteLine("Executing Rule {0}", rule);
            var matches = SubgraphSearch(rule, rootNode);
            if (matches.Count == 0)
            {
                //Console.WriteLine("No Matches were found for the following: {0}", rule);
                return false;
            }
            else {
                RuleReplacement(rule, matches);
                return true;
            }
            
        }

        //Makes an edge and adds it to the Canvas
        private void MakeAndAddEdge(GraphNode source, GraphNode target, bool path=true) {
            DataEdge de = new DataEdge(source, target) {
                isPath = path
            };
            hostGraph.AddEdge(de);
            cv.Children.Add(de);
            
        }
        //Makes an edge and adds it to the Canvas. Then returns it.
        private DataEdge MakeAndAddEdgeReturn(GraphNode source, GraphNode target, bool path = true)
        {
            DataEdge de = new DataEdge(source, target)
            {
                isPath = path
            };
            hostGraph.AddEdge(de);
            cv.Children.Add(de);
            return de;
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
            List<DataEdge> newEdges = new List<DataEdge>();
            int iteration = 0;
            //used to skip attaching nodes in the first loop
            bool reattachOutEdges = false;

            
            while (!replacementFinished)
            {
                //All nodes that have a match should be replaced first
                //Then attached nodes can be added in
                for (int i = 0; i < pGraph.Count; i++)
                {
                    if (states[i]) {
                        continue;
                    }
                    //Console.WriteLine("Replace pNode #{0}", i);
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
                        } else { // only product node IDs that exist in rule node is 1 and 2
                            m = match.Item2;
                        }

                        m.productGraphNode = node;
                        morphs.Add(m);
                        //Console.WriteLine("Replace hgNode #{0} with Type {1}", m.hostGraphNode.graphID, node.Type);
                        //Reattach only if the hostgraph node of the last rule node has an out edge
                        if (rule.GetLastRuleNode() == m.ruleGraphNode && hostGraph.OutDegree(m.hostGraphNode) > 0) {
                            reattachOutEdges = true;
                        }
                        m.hostGraphNode.Type = node.Type;
                        states[i] = true;
                    }
                    else if (iteration > 0 && (checkAttached(node, rule, morphs).Item1.Count > 0 || checkAttached(node, rule, morphs).Item2.Count > 0))
                    { //If there is no match for it, check if it is attached to a node that has already been replaced
                      //Then add the attached node and edge


                        var attachedNodes = checkAttached(node, rule, morphs);
                        var newNode = new GraphNode()
                        {
                            Type = node.Type
                        };
                        hostGraph.AddVertex(newNode);
                        cv.Children.Add(newNode);
                        //Add edges that go into this node                       
                        foreach (var atNode in attachedNodes.Item1) {
                            //Console.WriteLine("Added Edge from atNode {0} to newNode {1}", atNode, newNode);
                            newEdges.Add(MakeAndAddEdgeReturn(atNode, newNode));

                        }
                        //Add edges that go out of this node
                        foreach (var atNode in attachedNodes.Item2) {
                            //Console.WriteLine("Added Edge from newNode {0} to atNode {1}", newNode, atNode);
                            newEdges.Add(MakeAndAddEdgeReturn(newNode, atNode));

                        }
                        //Add nodes to GridManager
                        if (attachedNodes.Item1.Count > 0) {
                            var atNodePos = gridMan.getPosition(attachedNodes.Item1[0]);
                            gridMan.addNode(new Tuple<int, int>(atNodePos.Item1, atNodePos.Item2), newNode);
                        }
                        else {
                            var atNodePos = gridMan.getPosition(attachedNodes.Item2[0]);
                            gridMan.addNode(new Tuple<int, int>(atNodePos.Item1, atNodePos.Item2), newNode);
                        }
                        //Adding a new node's morphism. No equivalent rule graph node exists so it is not added here
                        Morphism morph2 = new Morphism(hostGraphNode: newNode, productGraphNode: node);
                        morphs.Add(morph2);
                        states[i] = true;


                    }
                    else
                    {
                        states[i] = false;
                    }

                }
                replacementFinished = !states.Contains(false);
                //If the replacement is finished, perform one final check to see if
                //the edge in the rule graph exists in the product graph.
                //if it doesn't exist, delete it from the hostGraph and the canvas children
                if (replacementFinished && rule.GetRuleEdgeCount() > 0)
                {

                    var edges = rule.GetRuleEdges();
                    bool found = false;
                    foreach (var edge in edges)
                    {
                        var src = edge.Source;
                        var target = edge.Target;
                        var productEdges = rule.GetProductEdges();

                        foreach (var pEdge in productEdges)
                        {
                            if (src.ruleID == pEdge.Source.ruleID && target.ruleID == pEdge.Target.ruleID)
                            {
                                found = true;
                            }
                        }

                    }
                    if (!found)
                    {
                        //Find the corresponding edge
                        //Then delete the corresponding edge in the hostgraph and remove from canvas children
                        GraphNode src2 = null;
                        GraphNode tgt2 = null;
                        foreach (var morph in morphs)
                        {
                            if (morph.ruleGraphNode == edges[0].Source)
                            {
                                src2 = morph.hostGraphNode;
                            }
                            if (morph.ruleGraphNode == edges[0].Target)
                            {
                                tgt2 = morph.hostGraphNode;
                            }
                        }

                        foreach (var e in hostGraph.Edges.ToList())
                        {
                            if (e.Source == src2 && e.Target == tgt2)
                            {
                                cv.Children.Remove(e);
                                hostGraph.RemoveEdge(e);
                            }
                        }

                    }
                }
                iteration++;
            }

            //Check all product nodes in the rule
            foreach (var morph in morphs) {
                if (morph.productGraphNode.coupleNode != null) {
                    var curHostNode = GetEqHostNodeWithProductNode(morph.productGraphNode, morphs);
                    var otherHostNode = GetEqHostNodeWithProductNode(morph.productGraphNode.coupleNode, morphs);
                    curHostNode.coupleNode = otherHostNode;
                    //Console.WriteLine("Coupled Node {0} to {1}", curHostNode.Type, otherHostNode.Type);
                }
            }

            //If product has more nodes then, the out edges of the last rule node need to be reattached
            //And the current out edges of the last rule node need to be deleted

            if (rule.GetRuleNodeCount() != rule.GetProductNodeCount() && reattachOutEdges)
            {
                //Console.WriteLine("Reattaching Edges");
                //First get the equivalent hostgraph node of the last product node             
                var hstNode = GetEqHostNodeWithProductNode(rule.GetLastProductNode(), morphs);

                //Get the out edges of the equivalent last Rule Node?              
                var hstNode2 = GetEqHostNodeWithRuleNode(rule.GetLastRuleNode(), morphs);
                var outEdges = hostGraph.OutEdges(hstNode2).ToList();

                foreach (var edge in outEdges)
                {
                    if (edge.Target != hstNode && !newEdges.Contains(edge)) {

                        MakeAndAddEdge(hstNode, edge.Target);
                        //Console.WriteLine("Removing Edge: {0} to {1}", edge.Source, edge.Target);
                        cv.Children.Remove(edge);
                        hostGraph.RemoveEdge(edge);
                        
                    }
                   
                }

            }

        }

        //  Finds a list of attached hostGraph node to attach a new graph node to.
        // @return two lists of attached hostGraph nodes which either go in or out of the new node
        private Tuple<List<GraphNode>, List<GraphNode>> checkAttached(GraphNode pNode, Rule rule, List<Morphism> replacedMorphs)
        {
            //check if pNode is attached to another product node that has already been replaced in the graph
            var edgeIn = rule.GetProductInEdges(pNode);
            var edgeOut = rule.GetProductOutEdges(pNode);
            List<GraphNode> inList = new List<GraphNode>();
            List<GraphNode> outList = new List<GraphNode>();
         
            foreach (var morph in replacedMorphs) {
                for (int i = 0; i < edgeIn.Count(); i++) {
                    if (edgeIn[i].Source == morph.productGraphNode) {
                        //Find the hostGraph node equivalent to the above productGraph node
                         inList.Add(morph.hostGraphNode);
                    } 
                }

                for (int i = 0; i < edgeOut.Count(); i++) {
                    if (edgeOut[i].Target == morph.productGraphNode)
                    {
                        outList.Add(morph.hostGraphNode);

                    }
                }

            }
            return new Tuple<List<GraphNode>, List<GraphNode>>(inList, outList);

        }

        //private bool CheckReAttachment() { 
        
        //}

        public Tuple<Morphism, Morphism> GetFirstMatch(List<Tuple<Morphism, Morphism>> matches) {
            if (matches.Count <= 0) {
                //Console.WriteLine("There are no matches");
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
                //Console.WriteLine(node);
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
        //private List<GraphNode> GetInAdjNodes(GraphNode node)
        //{
        //    var edges = hostGraph.InEdges(node);
        //    List<GraphNode> adjacentNodes = new List<GraphNode>();
        //    foreach (var edge in edges)
        //    {
        //        adjacentNodes.Add(edge.Target);
        //    }
        //    return adjacentNodes;
        //}

        //Gets the equivalent hostgraph node given the product node within the list of morphisms
        public GraphNode GetEqHostNodeWithProductNode(GraphNode productNode, List<Morphism> morphs) {
            foreach (var morph in morphs) {
                if (morph.productGraphNode == productNode) {
                    return morph.hostGraphNode;
                }
            }
            //Console.WriteLine("P: There is no equivalent host node");
            return null;
        }

        public GraphNode GetEqHostNodeWithRuleNode(GraphNode ruleNode, List<Morphism> morphs)
        {
            foreach (var morph in morphs)
            {
                if (morph.ruleGraphNode == ruleNode)
                {
                    return morph.hostGraphNode;
                }
            }
            //Console.WriteLine("R: There is no equivalent host node");
            return null;
        }

        public void refreshGraph() {
            var nodes = hostGraph.Vertices.ToList();
            var edges = hostGraph.Edges.ToList();
            foreach (var node in nodes) {
                var gridPos = gridMan.getPosition(node);
                Canvas.SetTop(node, 60 + gridPos.Item1 * 60);
                Canvas.SetLeft(node, 100 + gridPos.Item2 * 60);

            }

            foreach (var edge in edges)
            {
                edge.UpdateLine();
            }
        }

        public void printGraph() {
            hostGraph.printVertices();
        }

        private void DFSHelper(GraphNode node, List<bool> visited) {
            visited[node.graphID] = true;
            Console.Write(node + " ");

            List<GraphNode> vList = GetAdjNodes(node);
            foreach (var v in vList) {
                if (!visited[v.graphID]) {
                    DFSHelper(v, visited);
                }
            }
            //Console.WriteLine("End {0}", node.graphID);
        }

        public void printGraphDFS() {
            var visited = Enumerable.Repeat(false, 100).ToList();
            DFSHelper(rootNode, visited);
        }

        public void printEdges() {
            var hgEdges = hostGraph.Edges.ToList();

            foreach (var ed in hgEdges) {
                Console.WriteLine(ed);
            }
        }
    }
}
