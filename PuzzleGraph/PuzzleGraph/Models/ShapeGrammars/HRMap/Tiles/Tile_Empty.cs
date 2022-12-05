using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.HRMap.Tiles
{
    class Tile_Empty : Tile
    {
        public override Tile CreateInstance()
        {
            return new Tile_Empty();
        }

        public override void initTile()
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    Color tileColor = Color.FromArgb(64, 64, 64); //Empty Color
                    

                    map.SetPixel(i, j, tileColor);
                }
            }
        }
    }
}
