﻿using System;
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
            dp.north = true;
            dp.west = false;
            dp.east = false;
            dp.south = true;
            nodeType = "pi";
        }
    }
}