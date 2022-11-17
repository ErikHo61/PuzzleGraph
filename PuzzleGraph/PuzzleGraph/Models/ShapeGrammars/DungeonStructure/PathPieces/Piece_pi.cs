using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure.PathPieces
{
    class Piece_pi : Piece
    {
        public override void initPiece()
        {
            Direction = Orientation.Vertical;
            nodeType = "pi";
        }

        public override Piece CreateInstance()
        {
            return new Piece_pi();
        }
    }
}
