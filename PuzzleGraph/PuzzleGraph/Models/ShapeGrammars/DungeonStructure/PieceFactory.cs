﻿using PuzzleGraph.Models.ShapeGrammars.DungeonStructure.PathPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.DungeonStructure
{
    //This should be a factory pattern
    class PieceFactory
    {
        public Dictionary<string, Piece> pieces;

        public PieceFactory() {
            pieces = new Dictionary<string, Piece>();
            init();
        }

        public void init() {
            pieces.Add("fn", new Piece_fn());
            pieces.Add("e", new Piece_e());
            pieces.Add("g", new Piece_g());
            pieces.Add("k", new Piece_k());
            pieces.Add("km", new Piece_km());         
            pieces.Add("l", new Piece_l());
            pieces.Add("lm", new Piece_lm());
            pieces.Add("pi", new Piece_pi());
            pieces.Add("pp", new Piece_pp());
            pieces.Add("pr", new Piece_pr());
            pieces.Add("tp", new Piece_tp());
            pieces.Add("pifl", new Piece_pifl());
            pieces.Add("ppfl", new Piece_ppfl());

        }

        public Piece GetPiece(string s) {
            return pieces[s].CreateInstance();
        }


    }
}
