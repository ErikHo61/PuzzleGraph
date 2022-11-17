using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure
{
    public enum Orientation{
        Vertical, Horizontal, TriN, TriS, EndN, All
    }

    public abstract class Piece
    {
        public Piece() {
            initPiece();
        }
        //Which pathways are open
        public  DungeonPathway dp;
        //The corresponding mission graph node type
        public string nodeType;

        public abstract void initPiece();

        public abstract Piece CreateInstance();

        private Orientation _direction;

        public Orientation Direction {

            get { return _direction; } 
            
            set {
                _direction = value;
                switchDirection(value);
            } 
        }

        //Aligns the second piece with the first piece
        public void alignPieces(Piece otherPiece) { 
            
        }

        public void switchDirection(Orientation o) {
            switch (o) {
                case Orientation.Vertical:
                    dp = new DungeonPathway()
                    {
                        north = true,
                        south = true,
                        east = false,
                        west = false
                    };
                    break;
                case Orientation.Horizontal:
                    dp = new DungeonPathway()
                    {
                        north = false,
                        south = false,
                        east = true,
                        west = true
                    };
                    break;
                case Orientation.TriN:
                    dp = new DungeonPathway()
                    {
                        north = true,
                        south = false,
                        east = true,
                        west = true
                    };
                    break;
                case Orientation.TriS:
                    dp = new DungeonPathway()
                    {
                        north = false,
                        south = true,
                        east = true,
                        west = true
                    };
                    break;
                case Orientation.All:
                    dp = new DungeonPathway()
                    {
                        north = true,
                        south = true,
                        east = true,
                        west = true
                    };
                    break;
                case Orientation.EndN:
                    dp = new DungeonPathway()
                    {
                        north = true,
                        south = false,
                        east = false,
                        west = false
                    };
                    break;
                default:
                    break;
            }
        }
    }
}
