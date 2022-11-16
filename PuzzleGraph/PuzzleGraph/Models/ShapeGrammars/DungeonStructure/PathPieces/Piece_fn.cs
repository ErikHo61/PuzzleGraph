using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure.PathPieces
{
    class Piece_fn : Piece
    {
        public override void initPiece()
        {
            dp.north = true;
            dp.west = true;
            dp.east = true;
            dp.south = true;
            nodeType = "fn";
        }

       
    }
}
