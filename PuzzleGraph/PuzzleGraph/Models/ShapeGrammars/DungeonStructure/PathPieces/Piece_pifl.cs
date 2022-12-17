using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure.PathPieces
{
    class Piece_pifl : Piece
    {
        public override Piece CreateInstance()
        {
            return new Piece_pifl();
        }

        public override void initPiece()
        {
            Direction = Orientation.TriS;
            nodeType = "pifl";
        }
    }
}
