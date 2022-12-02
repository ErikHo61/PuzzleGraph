using PuzzleGraph.Models.ShapeGrammars.HRMap.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.ShapeGrammars.HRMap
{
    public class TileFactory
    {

        private Dictionary<string, Tile> tiles;

        public TileFactory() {
            tiles = new Dictionary<string, Tile>();
            init();    
        }

        private void init() {
            tiles.Add("basic", new Tile_Basic());
            tiles.Add("nothing", new Tile_Empty());
        }

        public Tile GetTile(string s) {
            return tiles[s];
        }
    }
}
