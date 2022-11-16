using PuzzleGraph.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PuzzleGraph.Models
{
    //Responsible for graph-layout algorithm using a 2D array data structure
    class GridManager
    {
        GridData[,] grid;
        int width;
        int height;
        Tuple<int, int> initialPosition;

        public GridManager(int width, int height) {
            this.width = width;
            this.height = height;
            grid = new GridData[width, height];
            initialPosition = new Tuple<int, int>(1, 1);

            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    grid[i, j] = new GridData();
                }
            }
        }

        public bool InitGrid(List<GraphNode> nodes) {
            foreach (var node in nodes) {

                int leftPos =(int) ((double) node.GetValue(Canvas.LeftProperty)-100) / 60 + initialPosition.Item2;
                int topPos =(int) ((double) node.GetValue(Canvas.TopProperty)-60) / 60 + initialPosition.Item1;
                if (!checkAdd(new Tuple<int, int>(topPos, leftPos))){
                    Console.WriteLine("Init: Could not add to {0} {1}", topPos, leftPos);
                    return false;
                }
                grid[topPos, leftPos].activated = true;
                grid[topPos, leftPos].graphID = node.graphID;
                Console.WriteLine("Occupied Space {0} {1}", topPos, leftPos);
            }
            return true;
        }

        public bool addNode(Tuple<int, int> adjPos, GraphNode node) {
            var above = new Tuple<int, int>(adjPos.Item1 - 1, adjPos.Item2);
            var left = new Tuple<int, int>(adjPos.Item1, adjPos.Item2 - 1);
            var left2 = new Tuple<int, int>(adjPos.Item1, adjPos.Item2 - 2);
            var right = new Tuple<int, int>(adjPos.Item1, adjPos.Item2 + 1);
            var right2 = new Tuple<int, int>(adjPos.Item1, adjPos.Item2 + 2);
            var below = new Tuple<int, int>(adjPos.Item1 + 1, adjPos.Item2);

            if (checkAdd(below))
            {
                grid[below.Item1, below.Item2].activated = true;
                grid[below.Item1, below.Item2].graphID = node.graphID;
                Console.WriteLine("Occupied Below Space {0} {1}", below.Item1, below.Item2);
                return true;
            }
            else if (checkAdd(right))
            {
                grid[right.Item1, right.Item2].activated = true;
                grid[right.Item1, right.Item2].graphID = node.graphID;
                Console.WriteLine("Occupied Right Space {0} {1}", right.Item1, right.Item2);
                return true;
            }
            else if (checkAdd(above))
            {
                grid[above.Item1, above.Item2].activated = true;
                grid[above.Item1, above.Item2].graphID = node.graphID;
                Console.WriteLine("Occupied Above Space {0} {1}", above.Item1, above.Item2);
                return true;
            }
            else if (checkAdd(left))
            {
                grid[left.Item1, left.Item2].activated = true;
                grid[left.Item1, left.Item2].graphID = node.graphID;
                Console.WriteLine("Occupied Left Space {0} {1}", left.Item1, left.Item2);
                return true;
            }
            else if (checkAdd(left2))
            {
                grid[left2.Item1, left2.Item2].activated = true;
                grid[left2.Item1, left2.Item2].graphID = node.graphID;
                Console.WriteLine("Occupied Left2 Space {0} {1}", left2.Item1, left2.Item2);
                return true;
            }
            else if (checkAdd(right2)) {
                grid[right2.Item1, right2.Item2].activated = true;
                grid[right2.Item1, right2.Item2].graphID = node.graphID;
                Console.WriteLine("Occupied Right2 Space {0} {1}", right2.Item1, right2.Item2);
                return true;
            }
                
            Console.WriteLine("Could not add Node");
            return false;
        }

        private bool checkAdd(Tuple<int, int> pos) {
            return checkInBounds(pos) && checkAvailable(pos);
        }

        private bool checkInBounds(Tuple<int, int> pos) {
            
            return pos.Item1 >= 0 && pos.Item1 < height && pos.Item2 >= 0 && pos.Item2 < width;
        }

        private bool checkAvailable(Tuple<int, int> pos) {
           
            return !grid[pos.Item1, pos.Item2].activated;
        }
        public Tuple<int, int> getPosition(GraphNode node) {
            for (int i = 0; i < grid.GetLength(0); i++){
                for (int j = 0; j < grid.GetLength(1); j++) {
                    if (grid[i, j].graphID == node.graphID && grid[i, j].activated) {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }
            return null;
        }

        public void refreshGrid(List<GraphNode> hostGraphNodes) { 
            
        }
    }
}
