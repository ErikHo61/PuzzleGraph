using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars
{
    abstract class ShapeRule
    {
        Bitmap bm;

        public ShapeRule() {
            InitBitMap();
        }

        protected abstract void InitBitMap();
    }
}
