using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure
{
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

        //Aligns the second piece with the first piece
        public void alignPieces(Piece otherPiece) { 
            
        }
    }
}
