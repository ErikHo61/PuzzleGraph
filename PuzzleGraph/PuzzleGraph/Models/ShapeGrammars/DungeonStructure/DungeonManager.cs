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
        //private List<Tuple<int, int>> availablePositions;
        private int dHeight;
        private int dWidth;

        private List<string> roomEnders;
        private List<List<Tuple<int, int>>> formedRooms;

        public DungeonManager(int h, int w) {
            dHeight = h;
            dWidth = w;
            pieces = new Piece[h, w];
            rootPos = new Tuple<int, int>(0, 0);
            pm = new PieceFactory();
            roomEnders = new List<string>() {"fn", "tp", "e", "g"};
            formedRooms = new List<List<Tuple<int, int>>>();

            
        }

        public void Init(HostGraph hg)
        {
            hostGraph = hg;
        }

        public Piece[,] GetPieces() {
            return pieces;
        }

        public Tuple<int, int> GetRootPos() {
            return rootPos;
        }
            
        public void CreateDungeonStructure(GraphNode rootNode) {
            List<Tuple<int, int>> availablePositions = new List<Tuple<int, int>>();
            
            var verts = hostGraph.Vertices.ToList();
            foreach (var nod in verts)
            {
                if (nod.Type == "e")
                {
                    addPiece(rootPos.Item1, rootPos.Item2, "e");
                    AddPossibleEmptyPos(rootPos, availablePositions);
                    break;
                }
            }

            List<GraphNode> visited = new List<GraphNode>();
            //fills visited with Mission nodes in DFS order
            GetNodesBFSOrder(rootNode, visited);
            //Remove initial piece because it was added in Init()
            visited.RemoveAll(containsE);
            bool containsE(GraphNode gn)
            {
                return gn.Type == "e";
            }
            //Console.WriteLine(hostGraph.getGraphNode(0));
            //Console.WriteLine(hostGraph.OutDegree(hostGraph.getGraphNode(0)));
            bool runOnce = true;
            List<GraphNode> added = new List<GraphNode>();
            foreach (var v in visited) {
                
                if (!added.Contains(v))
                {
                    Tuple<int, int> pos;
                    if (availablePositions.Count > 0)
                    {
                        pos = choosePosition(availablePositions);
                        
                    }
                    else {
                        print();
                        throw new ArgumentOutOfRangeException("No available positions to choose from");
                        
                    }
                    
                    
                    if (v.coupleNode == null)
                    {
                        addPieceOriented(pos.Item1, pos.Item2, v.Type, v.graphID);
                        updateAvailablePositions(pos, availablePositions);
                        added.Add(v);  
                    }
                    else
                    {
                        //If there is a coupleNode.
                        addPieceOriented(pos.Item1, pos.Item2, v.Type, v.graphID);
                        added.Add(v);
                        updateAvailablePositions(pos, availablePositions);
                        if (!added.Contains(v.coupleNode)) {
                            addCoupledNode(pos, v, added, availablePositions);
                        }
                        
                    }
                }
                if (runOnce)
                {
                    closeRemainingExits(rootPos, availablePositions);
                    runOnce = false;
                }
            }
        }

        //Update available positions with positions surrounding pos
        private void updateAvailablePositions(Tuple<int, int> pos, List<Tuple<int, int>> listPos) {
            listPos.Remove(pos);
            AddPossibleEmptyPos(pos, listPos);
        }

        //Set a piece's dungeon pathways that lead to null as false
        // @arg p the piece whose dungeon pathways will be closed
        private void closeRemainingExits(Tuple<int, int> p, List<Tuple<int, int>> availablePos) {
            var pos = pieces[p.Item1, p.Item2];
            var dp = pos.dp;
            if (dp.north && checkValid(p.Item1 - 1, p.Item2) && pieces[p.Item1 - 1, p.Item2] == null)
            {
                availablePos.Remove(new Tuple<int, int>(p.Item1 - 1, p.Item2));
                pieces[p.Item1, p.Item2].dp.north = false;
            }

            if (dp.east && checkValid(p.Item1, p.Item2 + 1) && pieces[p.Item1, p.Item2 + 1] == null)
            {
                availablePos.Remove(new Tuple<int, int>(p.Item1, p.Item2 + 1));
                pieces[p.Item1, p.Item2].dp.east = false;
            }

            if (dp.west && checkValid(p.Item1, p.Item2 - 1) && pieces[p.Item1, p.Item2 - 1] == null)
            {
                availablePos.Remove(new Tuple<int, int>(p.Item1, p.Item2 - 1));
                pieces[p.Item1, p.Item2].dp.west = false;
            }

            if (dp.south && checkValid(p.Item1 + 1, p.Item2) && pieces[p.Item1 + 1, p.Item2] == null)
            {
                availablePos.Remove(new Tuple<int, int>(p.Item1 + 1, p.Item2));
                pieces[p.Item1, p.Item2].dp.south = false;
            }

        }

        //Set a piece's dungeon pathways that lead to null as false
        // Also performs CloseWalledExits();
        // @arg p the piece whose dungeon pathways will be closed
        private void closeRemainingExits(Tuple<int, int> p)
        {
            var pos = pieces[p.Item1, p.Item2];
            var dp = pos.dp;
            if (dp.north)
            {
                if (!checkValid(p.Item1 - 1, p.Item2)) //leads to out of bounds, therefore close the exit
                {
                    pieces[p.Item1, p.Item2].dp.north = false;
                }
                else if (pieces[p.Item1 - 1, p.Item2] == null) { // if it doesn't, if it leads to null, close it
                    pieces[p.Item1, p.Item2].dp.north = false;
                }              
            }

            if (dp.east)
            {
                if (!checkValid(p.Item1, p.Item2 + 1)) {
                    pieces[p.Item1, p.Item2].dp.east = false;
                } else if (pieces[p.Item1, p.Item2 + 1] == null) {
                    pieces[p.Item1, p.Item2].dp.east = false;
                }               
            }

            if (dp.west)
            {
                if (!checkValid(p.Item1, p.Item2 - 1)) {
                    pieces[p.Item1, p.Item2].dp.west = false;
                } else if (pieces[p.Item1, p.Item2 - 1] == null) {
                    pieces[p.Item1, p.Item2].dp.west = false;
                }               
            }

            if (dp.south)
            {
                if (!checkValid(p.Item1 + 1, p.Item2)) {
                    pieces[p.Item1, p.Item2].dp.south = false;
                } else if (pieces[p.Item1 + 1, p.Item2] == null) {
                    pieces[p.Item1, p.Item2].dp.south = false;
                }          
            }
        }


        private void CloseWalledExit(Tuple<int, int> pos)
        {
            var dp = pieces[pos.Item1, pos.Item2].dp;
            if (dp.north && !checkValid(pos.Item1-1, pos.Item2)) {
                pieces[pos.Item1, pos.Item2].dp.north = false;
            }
            if (dp.south && !checkValid(pos.Item1 + 1, pos.Item2))
            {
                pieces[pos.Item1, pos.Item2].dp.south = false;
            }
            if (dp.east && !checkValid(pos.Item1, pos.Item2+1))
            {
                pieces[pos.Item1, pos.Item2].dp.east = false;
            }
            if (dp.west && !checkValid(pos.Item1, pos.Item2-1))
            {
                pieces[pos.Item1, pos.Item2].dp.west = false;
            }
        }

        //Form rooms
        public void DungeonRealization() {
            formRooms();
            //The following two lines, ensure that whenever a dp is true, it always leads to another non-null node
            connectDungeon();
            CloseAllRemainingExits();

            placeLocks();

            Console.WriteLine("Dungeon Creation complete?");
            
        }

        //Closes all remaining exits that lead to null on the full dungeon structure.
        private void CloseAllRemainingExits() {
            for (int i = 0; i < dHeight; i++) {
                for (int j = 0; j < dWidth; j++) {
                    if (pieces[i, j] != null) {
                        closeRemainingExits(new Tuple<int, int>(i, j));
                    }
                    
                }
            }
        }

        //Connect any exits that are facing each other and are opposite booleans
        private void connectDungeon() {
            foreach (var roomList in formedRooms) {
                connectHangingExit(roomList[0]);
                connectHangingExit(roomList[roomList.Count - 1]);
            }
        }

        private void connectHangingExit(Tuple<int, int> pos) {
            var posDP = pieces[pos.Item1, pos.Item2].dp;

            if (!posDP.north && checkValid(pos.Item1 - 1, pos.Item2)
                && pieces[pos.Item1 - 1, pos.Item2] != null
                && pieces[pos.Item1 - 1, pos.Item2].dp.south)
            {
                pieces[pos.Item1, pos.Item2].dp.north = true;
            }
            if (!posDP.south && checkValid(pos.Item1 + 1, pos.Item2)
                && pieces[pos.Item1 + 1, pos.Item2] != null
                && pieces[pos.Item1 + 1, pos.Item2].dp.north)
            {
                pieces[pos.Item1, pos.Item2].dp.south = true;
            }
            if (!posDP.west && checkValid(pos.Item1, pos.Item2 - 1)
                && pieces[pos.Item1, pos.Item2 - 1] != null
                && pieces[pos.Item1, pos.Item2 - 1].dp.east)
            {
                pieces[pos.Item1, pos.Item2].dp.west = true;
            }
            if (!posDP.east && checkValid(pos.Item1, pos.Item2 + 1)
                && pieces[pos.Item1, pos.Item2 + 1] != null
                && pieces[pos.Item1, pos.Item2 + 1].dp.west)
            {
                pieces[pos.Item1, pos.Item2].dp.east = true;
            }
        }

        //Treat e, fn, tp, g as end points, everything inbetween should be combined into a room(For Now)
        //fn should be a long hallway
        //tp can be small hallway
        private void formRooms() {
            List<Tuple<int, int>> roomList = new List<Tuple<int, int>>();
            List<Tuple<int, int>> visited = new List<Tuple<int, int>>();
            formRoomsHelper(rootPos, visited, roomList);
            //combineRooms(formedRooms);

        }

        private void formRoomsHelper(Tuple<int, int> pos, List<Tuple<int, int>> visited, List<Tuple<int, int>> roomList) {
            visited.Add(pos);

            //Get empty adjacent positions
            bool RemoveNullsOrDups(Tuple<int, int> p)
            {
                return pieces[p.Item1, p.Item2] == null || visited.Contains(p);
            }
            //AddPossibleEmptyPos(pos, positions);

            List<Tuple<int, int>> availablePos = GetAdjacentPos(pos);
            availablePos.RemoveAll(RemoveNullsOrDups);
            //Traverse the next positions
            //Add to the list
            //e, fn, tp, g
            if (!roomEnders.Contains(pieces[pos.Item1, pos.Item2].nodeType))
            {
                roomList.Add(pos);
            }
            //Console.Write(pieces[pos.Item1, pos.Item2].nodeType);
            foreach (var nextPos in availablePos)
            {
                if (!visited.Contains(nextPos)) {
                    formRoomsHelper(nextPos, visited, roomList);
                }
                          
            }
            //End the list
            if (roomList.Count > 0)
            {
                List<Tuple<int, int>> newRoom = new List<Tuple<int, int>>(roomList);
                formedRooms.Add(newRoom);
            }
            roomList.Clear();
            //Console.WriteLine();
        }

        //Place the locks
        // Combine any key in the formed rooms to the first room
        //
        public void placeLocks() {
            foreach (var roomList in formedRooms) {
                List<Tuple<int, int>> rL = new List<Tuple<int, int>>();
                foreach(var pos in roomList) {
                    if (pieces[pos.Item1, pos.Item2].nodeType == "l" || pieces[pos.Item1, pos.Item2].nodeType == "lm") {
                        
                        addLocks(roomList, pos);
                    }
                }
            }
        }
        
        
        private void addLocks(List<Tuple<int, int>> roomList, Tuple<int, int> pos) {
            if (!AddLockToPosition(pos, roomList)) {
                if (AddLockToPositionInBetween(pos, roomList)) {
                    return;   
                }
                else if (roomList.Count <= 2)
                {
                    AddLockToPosition(roomList[0], roomList);

                }
                

            }         
        }

        //adds lock so that it is between the pos and the previous room in the roomList
        private bool AddLockToPositionInBetween(Tuple<int, int> pos, List<Tuple<int, int>> roomList) {
            var posDP = pieces[pos.Item1, pos.Item2].dp;
            var index = roomList.IndexOf(pos);
            var otherPiece = pieces[roomList[index - 1].Item1, roomList[index - 1].Item2];

            if (pieces[pos.Item1-1, pos.Item2] == otherPiece) {
                pieces[pos.Item1, pos.Item2].AddOtherNode("Lock-North");
                return true;
            }
            if (pieces[pos.Item1 + 1, pos.Item2] == otherPiece) {
                pieces[pos.Item1, pos.Item2].AddOtherNode("Lock-South");
                return true;
            }
            if (pieces[pos.Item1, pos.Item2-1] == otherPiece)
            {
                pieces[pos.Item1, pos.Item2].AddOtherNode("Lock-West");
                return true;
            }
            if (pieces[pos.Item1, pos.Item2+1] == otherPiece)
            {
                pieces[pos.Item1, pos.Item2].AddOtherNode("Lock-East");
                return true;
            }

            return false;
        }

        //Adds locks to position
        //Cannot add locks to inbetween rooms
        private bool AddLockToPosition(Tuple<int, int> pos, List<Tuple<int, int>> roomList) {
            var posDP = pieces[pos.Item1, pos.Item2].dp;
            if (posDP.north && !roomList.Contains(new Tuple<int, int>(pos.Item1-1, pos.Item2))) {
                pieces[pos.Item1, pos.Item2].AddOtherNode("Lock-North");
                return true;
            }

            if(posDP.south && !roomList.Contains(new Tuple<int, int>(pos.Item1+1, pos.Item2))){
                pieces[pos.Item1, pos.Item2].AddOtherNode("Lock-South");
                return true;
            }
            if (posDP.west && !roomList.Contains(new Tuple<int, int>(pos.Item1, pos.Item2-1))){
                pieces[pos.Item1, pos.Item2].AddOtherNode("Lock-West");
                return true;
            }
            if (posDP.east && !roomList.Contains(new Tuple<int, int>(pos.Item1, pos.Item2 + 1))){
                pieces[pos.Item1, pos.Item2].AddOtherNode("Lock-East");
                return true;
            }
            return false;
        }

        //Choose a position
        public Tuple<int, int> choosePosition(List<Tuple<int, int>> availablePositions) {
            //Shuffle(availablePositions);
            //insert algorithm here or use shuffle()
            return availablePositions[0];
        }

        //Change the position of the exit so that it doesn't lead into the wall.
        private void BendExit(Tuple<int, int> pos) {
            var dp = pieces[pos.Item1, pos.Item2].dp;
            if (pieces[pos.Item1, pos.Item2].Direction == Orientation.Vertical)
            {
                if (!dp.west && checkValid(pos.Item1, pos.Item2-1) && pieces[pos.Item1, pos.Item2-1] == null)
                {
                    pieces[pos.Item1, pos.Item2].dp.east = true;
                }
                else if (!dp.east && checkValid(pos.Item1, pos.Item2+1) && pieces[pos.Item1, pos.Item2+1] == null)
                {
                    pieces[pos.Item1, pos.Item2].dp.east = true;
                }

                CloseWalledExit(pos);
            }
            else if (pieces[pos.Item1, pos.Item2].Direction == Orientation.Horizontal)
            {
                //If its not already open, its a valid position and nothing is there
                if (!dp.north && checkValid(pos.Item1 - 1, pos.Item2) && pieces[pos.Item1 - 1, pos.Item2] == null)
                {
                    pieces[pos.Item1, pos.Item2].dp.north = true;
                    
                }
                else if (!dp.south && checkValid(pos.Item1 + 1, pos.Item2) && pieces[pos.Item1 + 1, pos.Item2] == null) {
                    pieces[pos.Item1, pos.Item2].dp.south = true;
                }
                CloseWalledExit(pos);
            }
            else {
                throw new SpaceNotFoundException("All exits are occupied and full");
            }

            Console.WriteLine("Bent position {0}", pos);
        }


        //public void CreateDungeonStructure2(GraphNode rootNode)
        //{
        //    List<GraphNode> visited = new List<GraphNode>();
            
        //    //fills visited with Mission nodes in DFS order
        //    DFSHelper(rootNode, visited);
        //    //Remove initial piece because it was added in Init()
        //    visited.RemoveAll(containsE);
        //    bool containsE(GraphNode gn)
        //    {
        //        return gn.Type == "e";
        //    }

        //    List<GraphNode> added = new List<GraphNode>();
        //    foreach (var v in visited)
        //    {
        //        if (!added.Contains(v))
        //        {
                    
        //            var pos = findPosition();
        //            if (v.coupleNode == null)
        //            {

        //                addPieceOriented(pos.Item1, pos.Item2, v.Type);
                        
        //                added.Add(v);
        //            }
        //            else
        //            {
        //                //If there is a coupleNode.
        //                addPieceOriented(pos.Item1, pos.Item2, v.Type);
        //                added.Add(v);

        //                addCoupledNode(pos, v, added);
        //            }
        //        }


        //    }
        //}

        //Adds the coupled node of the passed in argument in the adjacent pos
        //@arg pos the position of the passed in node
        //@arg node the node of which has a couple node to add
        //@arg added a list of added mission nodes to update
        private void addCoupledNode(Tuple<int, int> pos, GraphNode node, List<GraphNode> added, List<Tuple<int, int>> availablePositions) {
            var poss = GetAdjacentPos(pos);
            poss.RemoveAll(RemoveNonNulls);
            //If there are no available positions attempt to create one
            if (poss.Count == 0) {
                BendExit(pos);
                poss = GetAdjacentPos(pos);
                poss.RemoveAll(RemoveNonNulls);
                if (poss.Count == 0) {
                    print();
                    throw new ArgumentOutOfRangeException("STILL NO AVAILABLE POSITIONS");
                }
            }
            
            Shuffle(poss);
            addPieceOriented(poss[0].Item1, poss[0].Item2, node.coupleNode.Type, node.coupleNode.graphID);
            added.Add(node.coupleNode);
            //addPieceOriented(poss[0].Item1, poss[0].Item2, node.coupleNode.Type);

            updateAvailablePositions(poss[0], availablePositions);

            //Recursively add coupled nodes until there is no more
            if (node.coupleNode.coupleNode != null) {
                
                addCoupledNode(new Tuple<int, int>(poss[0].Item1, poss[0].Item2), node.coupleNode, added, availablePositions);
            }
        }

        private void addCoupledNode(Tuple<int, int> pos, GraphNode node, List<GraphNode> added)
        {
            var poss = GetAdjacentPos(pos);
            poss.RemoveAll(RemoveNonNulls);
            Shuffle(poss);

            addPieceOriented(poss[0].Item1, poss[0].Item2, node.coupleNode.Type, node.coupleNode.graphID);
            added.Add(node.coupleNode);
            //Recursively add coupled nodes until there is no more
            if (node.coupleNode.coupleNode != null)
            {

                addCoupledNode(new Tuple<int, int>(poss[0].Item1, poss[0].Item2), node.coupleNode, added);
            }
        }

        //DFS to Traverse the mission graph
        private void DFSHelper(GraphNode node, List<GraphNode> visited)
        {
            visited.Add(node);
            //Console.Write(node + " ");

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

        private void GetNodesBFSOrder(GraphNode node, List<GraphNode> visited) {
            Queue<GraphNode> que = new Queue<GraphNode>();

            visited.Add(node);
            que.Enqueue(node);
            while (que.Any()) {
                var nd = que.Dequeue();
                List<GraphNode> vList = hostGraph.GetAdjNodes(nd);
                foreach (var adjNode in vList) {
                    if (!visited.Contains(adjNode)) {
                        visited.Add(adjNode);
                        que.Enqueue(adjNode);
                    }
                }
            }
        }

        //Add a piece that is oriented so that it is connected to the dungeon
        public void addPieceOriented(int posh, int posw, string s, int id) {
            var p = pm.GetPiece(s);
            p.Direction = FindOrientation(posh, posw, p);
            p.hostgraphID = id;
            pieces[posh, posw] = p;
        }

        //Needs Two positions
        public Orientation FindOrientation(int posh, int posw, Piece p) {
            if (p.Direction == Orientation.All) {
                return Orientation.All;
            }
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
                if (p.Direction == Orientation.TriN || p.Direction == Orientation.TriS)
                {
                    return p.Direction;
                }
                return Orientation.Horizontal;
            }
            if (checkValid(posh, posw+1) && pieces[posh, posw+1] != null && pieces[posh, posw+1].dp.west)
            {
                if (p.Direction == Orientation.TriN || p.Direction == Orientation.TriS)
                {
                    return p.Direction;
                }
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
        //private void FindPosHelper(Tuple<int, int> pos, List<Tuple<int, int>> visited, List<Tuple<int, int>> positions)
        //{
        //    visited.Add(pos);

        //    //Get empty adjacent positions
        //    bool RemoveNullsOrDups(Tuple<int, int> p)
        //    {
        //        return pieces[p.Item1, p.Item2] == null || visited.Contains(p);
        //    }
        //    AddPossibleEmptyPos(pos, positions);

        //    List<Tuple<int, int>> availablePos = GetAdjacentPos(pos);
        //    availablePos.RemoveAll(RemoveNullsOrDups);
        //    //Traverse the next positions
        //    foreach (var nextPos in availablePos)
        //    {
        //        FindPosHelper(nextPos, visited, positions);
        //    }


        //}



        //Adds the empty positions where the next piece can be placed to the passed in List
        //Add possible empty positions surrounding pos
        private void AddPossibleEmptyPos(Tuple<int, int> pos, List<Tuple<int, int>> positions)
        {
            var poss = GetAdjacentPos(pos);
            poss.RemoveAll(RemoveNonNulls);
            //Only add it if its unique. temporary measure. Should use hashset for unique collection
            foreach (var p in poss) {
                if (!positions.Contains(p)) {
                    positions.Add(p);
                }
                    
            }
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

        //Check if the position is within the grid otherwise it is out of bounds
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

        //private Tuple<int, int> findPosition()
        //{
        //    List<Tuple<int, int>> positions = new List<Tuple<int, int>>();
        //    List<Tuple<int, int>> visited = new List<Tuple<int, int>>();
        //    FindPosHelper(rootPos, visited, positions);
        //    Shuffle(positions);
        //    //Add some advanced algorithm in picking a position here ^_^. None for now.
        //    if (positions[0] == null) {
        //        print();
        //        Console.WriteLine("BUG");

        //    }
        //    return positions[0];

        //}

        public void print()
        {
            Console.WriteLine();
            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    string s = "";
                    if (pieces[i, j] != null)
                    {
                        s += pieces[i, j].nodeType;
                        if (pieces[i, j].nodeType.Length == 1)
                        {
                            s += "    ";
                        }
                        if (pieces[i, j].nodeType.Length == 2)
                        {
                            s += "   ";
                        }
                        Console.Write(s);
                    }
                    else
                    {
                        Console.Write("NA" + "   ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
