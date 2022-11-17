using PuzzleGraph.CustomControls;
using PuzzleGraph.Models.ShapeGrammars.DungeonStructure.PathPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure
{
    //Encompasses all the steps taken in phase 2
    // Responsible for converting the hostgraph into populated dungeon grid for exportation
    public class DungeonManager
    {
        private Piece[,] pieces;
        private Tuple<int, int> rootPos;
        //private List<Piece> piecesList;
        public HostGraph hostGraph;
        private PieceFactory pm;


        public DungeonManager(int h, int w) {
            pieces = new Piece[h, w];
            rootPos = new Tuple<int, int>(3, 3);
            pm = new PieceFactory();
            
        }

        public void Init(HostGraph hg)
        {
            hostGraph = hg;
            //Add the initial piece into the grid
            var verts = hg.Vertices.ToList();
            foreach (var nod in verts)
            {
                if (nod.Type == "e")
                {
                    addPiece(rootPos.Item1, rootPos.Item2, "e");
                    break;
                }
            }
        }

        public void CreateDungeonStructure(GraphNode rootNode)
        {
            List<GraphNode> visited = new List<GraphNode>();
            //fills visited with Mission nodes in DFS order
            DFSHelper(rootNode, visited);
            //Remove initial piece because it was added in Init()
            visited.RemoveAll(containsE);
            bool containsE(GraphNode gn)
            {
                return gn.Type == "e";
            }

            List<GraphNode> added = new List<GraphNode>();
            foreach (var v in visited)
            {
                if (!added.Contains(v))
                {
                    var pos = findPosition();
                    if (v.coupleNode == null)
                    {

                        addPieceOriented(pos.Item1, pos.Item2, v.Type);
                        added.Add(v);
                    }
                    else
                    {
                        //If there is a coupleNode.

                        addPieceOriented(pos.Item1, pos.Item2, v.Type);
                        added.Add(v);

                        addCoupledNode(pos, v, added);

                        //var poss = GetAdjacentPos(pos);
                        //poss.RemoveAll(RemoveNonNulls);
                        //Shuffle(poss);

                        //addPieceOriented(poss[0].Item1, poss[0].Item2, v.coupleNode.Type);
                        //added.Add(v.coupleNode);
                    }
                }


            }
        }

        //Adds the coupled node of the passed in argument in the adjacent pos
        //@arg pos the position of the passed in node
        //@arg node the node of which has a couple node to add
        //@arg added a list of added mission nodes to update
        private void addCoupledNode(Tuple<int, int> pos, GraphNode node, List<GraphNode> added) {
            var poss = GetAdjacentPos(pos);
            poss.RemoveAll(RemoveNonNulls);
            Shuffle(poss);

            addPieceOriented(poss[0].Item1, poss[0].Item2, node.coupleNode.Type);
            added.Add(node.coupleNode);
            //Recursively add coupled nodes until there is no more
            if (node.coupleNode.coupleNode != null) {
                
                addCoupledNode(new Tuple<int, int>(poss[0].Item1, poss[0].Item2), node.coupleNode, added);
            }
        }

        //DFS to Traverse the mission graph
        private void DFSHelper(GraphNode node, List<GraphNode> visited)
        {
            visited.Add(node);
            Console.Write(node + " ");

            List<GraphNode> vList = hostGraph.GetAdjNodes(node);
            foreach (var v in vList)
            {
                if (!visited.Contains(v))
                {
                    DFSHelper(v, visited);
                }
            }
            //Console.WriteLine("End {0}", node.graphID);
        }

        //Add a piece that is oriented so that it is connected to the dungeon
        public void addPieceOriented(int posh, int posw, string s) {
            var p = pm.GetPiece(s);
            p.Direction = FindOrientation(posh, posw, p);
            pieces[posh, posw] = p;
        }

        public Orientation FindOrientation(int posh, int posw, Piece p) {
            if (checkValid(posh - 1, posw) && pieces[posh - 1, posw] != null && pieces[posh - 1, posw].dp.south) {
                if (p.Direction == Orientation.TriS) {
                    return Orientation.TriN;                
                }
                return Orientation.Vertical;
            }
            if (checkValid(posh + 1, posw) && pieces[posh + 1, posw] != null && pieces[posh + 1, posw].dp.north) {
                if (p.Direction == Orientation.TriN)
                {
                    return Orientation.TriS;
                }
                return Orientation.Vertical;
            }
            if (checkValid(posh, posw-1) && pieces[posh, posw-1] != null && pieces[posh, posw-1].dp.east)
            {
                return Orientation.Horizontal;
            }
            if (checkValid(posh, posw+1) && pieces[posh, posw+1] != null && pieces[posh, posw+1].dp.west)
            {
                return Orientation.Horizontal;
            }
            Console.WriteLine("Orientation not found");
            return Orientation.All;
        }

        public void addPiece(int posh, int posw, string s)
        {
            //pieceslist.add(pm.getpiece(s));
            pieces[posh, posw] = pm.GetPiece(s);
        }



        //The DFS Helper for finding positions to place pieces
        //Recurisvely traverses the pieces to find all available positions
        private void FindPosHelper(Tuple<int, int> pos, List<Tuple<int, int>> visited, List<Tuple<int, int>> positions)
        {
            visited.Add(pos);

            //Get empty adjacent positions
            bool RemoveNullsOrDups(Tuple<int, int> p)
            {
                return pieces[p.Item1, p.Item2] == null || visited.Contains(p);
            }
            AddPossibleEmptyPos(pos, positions);

            List<Tuple<int, int>> availablePos = GetAdjacentPos(pos);
            availablePos.RemoveAll(RemoveNullsOrDups);
            //Traverse the next positions
            foreach (var nextPos in availablePos)
            {
                FindPosHelper(nextPos, visited, positions);
            }


        }



        //Adds the empty positions where the next piece can be placed to the passed in List
        private void AddPossibleEmptyPos(Tuple<int, int> pos, List<Tuple<int, int>> positions)
        {
            var poss = GetAdjacentPos(pos);
            poss.RemoveAll(RemoveNonNulls);
            positions.AddRange(poss);
        }

        private bool RemoveNonNulls(Tuple<int, int> pos)
        {
            return pieces[pos.Item1, pos.Item2] != null;
        }

        //Gets adjacent positions to the passed in positions of which there is an exit to
        private List<Tuple<int, int>> GetAdjacentPos(Tuple<int, int> pos)
        {
            var dp = pieces[pos.Item1, pos.Item2].dp;
            List<Tuple<int, int>> poss = new List<Tuple<int, int>>();
            if (dp.north && checkValid(pos.Item1 - 1, pos.Item2))
            {
                poss.Add(new Tuple<int, int>(pos.Item1 - 1, pos.Item2));
            }

            if (dp.east && checkValid(pos.Item1, pos.Item2 + 1))
            {
                poss.Add(new Tuple<int, int>(pos.Item1, pos.Item2 + 1));
            }

            if (dp.west && checkValid(pos.Item1, pos.Item2 - 1))
            {
                poss.Add(new Tuple<int, int>(pos.Item1, pos.Item2 - 1));
            }

            if (dp.south && checkValid(pos.Item1 + 1, pos.Item2))
            {
                poss.Add(new Tuple<int, int>(pos.Item1 + 1, pos.Item2));
            }

            return poss;
        }

        //Check if the position is within the grid
        private bool checkValid(int h, int w)
        {
            return h >= 0 && h < pieces.GetLength(0) && w >= 0 && w < pieces.GetLength(1);
        }

        private Random rng = new Random();

        public void Shuffle(List<Tuple<int, int>> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Tuple<int, int> value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private Tuple<int, int> findPosition()
        {
            List<Tuple<int, int>> positions = new List<Tuple<int, int>>();
            List<Tuple<int, int>> visited = new List<Tuple<int, int>>();
            FindPosHelper(rootPos, visited, positions);
            Shuffle(positions);
            //Add some advanced algorithm in picking a position here ^_^. None for now.
            if (positions[0] == null) {
                print();
                Console.WriteLine("BUG");

            }
            return positions[0];

        }

        public void print()
        {
            Console.WriteLine();
            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    if (pieces[i, j] != null)
                        Console.Write(pieces[i, j].nodeType + "     ");
                    else {
                        Console.Write("NA" + "     ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
