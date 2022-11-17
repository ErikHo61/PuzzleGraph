using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure.PathPieces
{
    class Piece_lm : Piece
    {
        public override void initPiece()
        {
            Direction = Orientation.Vertical;
            nodeType = "lm";
        }

        public override Piece CreateInstance()
        {
            return new Piece_lm();
        }
    }
}
