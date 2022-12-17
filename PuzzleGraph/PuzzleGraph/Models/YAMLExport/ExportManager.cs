using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace PuzzleGraph.Models.YAMLExport
{
    public class ExportManager
    {
        private readonly Bitmap hrMap;
        private readonly HostGraph hostGraph;
        private Dictionary<Color, string> colorDict;
        private readonly List<TileInfo> tiles;
        public String path { get; set; }

        public ExportManager(Bitmap bm, HostGraph hg, List<TileInfo> tiles) {
            hrMap = bm;
            hostGraph = hg;
            colorDict = new Dictionary<Color, string>();
            initColorDict();
            this.tiles = tiles;
        }

        private void initColorDict() {
            colorDict.Add(Color.FromArgb(234, 44, 235), "pp");//pink
            colorDict.Add(Color.FromArgb(194, 44, 240), "pi");//purple
            colorDict.Add(Color.FromArgb(234, 44, 240), "ppfl");//pink
            colorDict.Add(Color.FromArgb(195, 44, 240), "pifl");//purple
            colorDict.Add(Color.FromArgb(103, 44, 240), "pr");//blue
            colorDict.Add(Color.FromArgb(255, 255, 51), "k");//yellow
            colorDict.Add(Color.FromArgb(220, 220, 51), "km");//yellow
            colorDict.Add(Color.FromArgb(118, 16, 16), "l"); //brown
            colorDict.Add(Color.FromArgb(118, 18, 18), "lm");//brown
            colorDict.Add(Color.FromArgb(134, 106, 192), "tp"); // light purple
            colorDict.Add(Color.FromArgb(240, 44, 64), "g"); // red
            colorDict.Add(Color.FromArgb(205, 142, 142), "wall");
            colorDict.Add(Color.FromArgb(114, 205, 114), "floor"); //Floor Color
            colorDict.Add(Color.FromArgb(64, 64, 64), "empty");
            colorDict.Add(Color.FromArgb(0, 0, 0, 0), "empty");
            colorDict.Add(Color.FromArgb(32, 44, 200), "e");//entrance blue
        }

        private TileInfo ContainsPos(List<TileInfo> tiles, int y, int x) {
            foreach (var tile in tiles) {
                if (tile.y == y && tile.x == x) {
                    return tile;
                }
            }
            return null;
        }

        public void Serialize()
        {


            
            var writer = new StreamWriter(path);
            var serializer = new SerializerBuilder().Build();
            string yaml;
            for (int i = 0; i < hrMap.Height; i++) {
                for (int j = 0; j < hrMap.Width; j++) {
                    TileInfo ti;
                    if (ContainsPos(tiles, i, j) != null)
                    {
                        ti = ContainsPos(tiles, i, j);
                        ti.uid = i*hrMap.Height + j;
                    }
                    else {
                        var color = hrMap.GetPixel(j, i);
                        string s = colorDict[color];

                        ti = new TileInfo
                        {
                            type = s,
                            graphID = -1,
                            uid = i*hrMap.Height + j,
                            x = j,
                            y = i,
                            coupleID = -1

                        };

                    }
                    yaml = serializer.Serialize(ti);
                    //Console.WriteLine(yaml);
                    writer.WriteLine(yaml);
                }
            }

         

            


            writer.Close();

        }
    }
}
