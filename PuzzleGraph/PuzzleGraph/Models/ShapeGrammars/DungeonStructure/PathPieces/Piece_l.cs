using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure.PathPieces
{
    class Piece_l : Piece
    {
        public override void initPiece()
        {
            dp.north = false;
            dp.west = true;
            dp.east = true;
            dp.south = false;
            nodeType = "l";
        }
    }
}
