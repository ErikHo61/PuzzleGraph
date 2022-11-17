﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure.PathPieces
{
    class Piece_pr : Piece
    {
        public override void initPiece()
        {
            Direction = Orientation.Vertical;
            nodeType = "pr";        
        }

        public override Piece CreateInstance()
        {
            return new Piece_pr();
        }
    }
}
