using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.HRMap
{
    public abstract class Tile
    {
        public Bitmap map;
        public int height;
        public int width;

        public Tile() {
            map = new Bitmap(6, 6);
            initTile();
        }

        public abstract void initTile();

        public abstract Tile CreateInstance();
    }
}
