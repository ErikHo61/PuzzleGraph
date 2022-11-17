using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure.PathPieces
{
    class Piece_g : Piece
    {
       
        public override void initPiece()
        {
            Direction = Orientation.Vertical;
            nodeType = "g";
        }

        public override Piece CreateInstance()
        {
            return new Piece_g();
        }
    }
}
