using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.HRMap.Tiles
{
    //A Basic walled in tile with 4 walls
    class Tile_Basic : Tile
    {
        public override Tile CreateInstance()
        {
            return new Tile_Basic();
        }

        public override void initTile()
        {
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    Color tileColor;
                    if (i == 0 || j == 0 || i == map.Height - 1 || j == map.Width - 1)
                    {
                        tileColor = Color.FromArgb(205, 142, 142); //wall color
                    }
                    else {
                        tileColor = Color.FromArgb(114, 205, 114); //Floor Color
                    }
                    
                    map.SetPixel(i, j, tileColor);
                }
            }
            //Color reddishColor = Color.FromArgb(150, 0, 0);
            
        }
    }
}
