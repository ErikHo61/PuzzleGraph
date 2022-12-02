using PuzzleGraph.Models.ShapeGrammars.DungeonStructure;
using PuzzleGraph.Models.ShapeGrammars.HRMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PuzzleGraph.Models.ShapeGrammars
{
    class ShapeManager
    {
        Bitmap hrMap;
        Piece[,] pieces;
        Tuple<int, int> rootPos;
        TileFactory tf;
        Dictionary<string, System.Drawing.Color> colorDict;

        public ShapeManager(Piece[,] pieces, Tuple<int, int> rootPos) {
            hrMap = new Bitmap(60, 60);
            tf = new TileFactory();
            colorDict = new Dictionary<string, System.Drawing.Color>();
            initColorDict();
            this.pieces = pieces;
            this.rootPos = rootPos;
        }

        private void initColorDict() {
            colorDict.Add("pp", Color.FromArgb(234, 44, 240));//pink
            colorDict.Add("pi", Color.FromArgb(195, 44, 240));//purple
            colorDict.Add("pr", Color.FromArgb(103, 44, 240));//blue
            colorDict.Add("k", Color.FromArgb(255, 255, 51));//yellow
            colorDict.Add("km", Color.FromArgb(220, 220, 51));//yellow
            colorDict.Add("l", Color.FromArgb(118, 16, 16)); //brown
            colorDict.Add("lm", Color.FromArgb(118, 16, 16));//brown
            colorDict.Add("tp", Color.FromArgb(134, 106, 192)); // light purple
            colorDict.Add("g", Color.FromArgb(240, 44, 64)); // red
            
        }

        public BitmapImage refreshBitMap() {
            //var resizedBitmap = new Bitmap(hrMap, new Size(hrMap.Width/2, hrMap.Height/2));
            return BitmapToImageSource(hrMap);
        }

        public void ConvertToHRMap() {
            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    Tile newTile;
                    if (pieces[i, j] != null) {
                        //newTile = tf.GetTile(pieces[i, j].nodeType);
                        newTile = tf.GetTile("basic");
                    }
                    else {
                        newTile = tf.GetTile("nothing");
                    }
                    
                    for (int h = 0; h < newTile.map.Height; h++) {
                        for (int k = 0; k < newTile.map.Width; k++) {
                            hrMap.SetPixel(k+j*6, h+i*6, newTile.map.GetPixel(k, h));
                                                      
                        }
                    }
                    if (pieces[i, j] != null) {
                        DrillPathways(newTile, i, j);
                        if (pieces[i, j].GetOtherNodes().Count > 0) {
                            AddLocks(newTile, i, j);
                        }

                        placeObjects(newTile, i, j);
                    }
                    
                }
            }
        }

        //Place objects into  bitmap
        private void placeObjects(Tile newTile, int y, int x) {
            var p = pieces[y, x];
            if (colorDict.ContainsKey(pieces[y, x].nodeType))
            {
                hrMap.SetPixel(3 + x * newTile.map.Width, 3 + y * newTile.map.Height, colorDict[pieces[y, x].nodeType]);
            }
            else {
                hrMap.SetPixel(3 + x * newTile.map.Width, 3 + y * newTile.map.Height, Color.FromArgb(255,255,255));
            }
            
        }

        //Drill Pathways into walls
        private void DrillPathways(Tile newTile, int i, int j) {
            System.Drawing.Color floorColor = System.Drawing.Color.FromArgb(114, 205, 114);
            if (pieces[i, j].dp.north)
            {
                hrMap.SetPixel(2 + j * newTile.map.Width, 0 + i * newTile.map.Height, floorColor);
                hrMap.SetPixel(3 + j * newTile.map.Width, 0 + i * newTile.map.Height, floorColor);
            }
            if (pieces[i, j].dp.west)
            {
                hrMap.SetPixel(0 + j * newTile.map.Width, 2 + i * newTile.map.Height, floorColor);
                hrMap.SetPixel(0 + j * newTile.map.Width, 3 + i * newTile.map.Height, floorColor);
            }
            if (pieces[i, j].dp.east)
            {
                hrMap.SetPixel(newTile.map.Width - 1 + j * newTile.map.Width, 2 + i * newTile.map.Height, floorColor);
                hrMap.SetPixel(newTile.map.Width - 1 + j * newTile.map.Width, 3 + i * newTile.map.Height, floorColor);
            }
            if (pieces[i, j].dp.south)
            {
                hrMap.SetPixel(2 + j * newTile.map.Width, newTile.map.Height - 1 + i * newTile.map.Height, floorColor);
                hrMap.SetPixel(3 + j * newTile.map.Width, newTile.map.Height - 1 + i * newTile.map.Height, floorColor);
            }
        }

        private void AddLocks(Tile newTile, int i, int j) {
            System.Drawing.Color lockColor = System.Drawing.Color.FromArgb(205, 30, 64);
            if (pieces[i, j].GetOtherNodes().Contains("Lock-North")) {
                hrMap.SetPixel(2 + j * newTile.map.Width, 0 + i * newTile.map.Height, lockColor);
                hrMap.SetPixel(3 + j * newTile.map.Width, 0 + i * newTile.map.Height, lockColor);
            }
            if (pieces[i, j].GetOtherNodes().Contains("Lock-West"))
            {
                hrMap.SetPixel(0 + j * newTile.map.Width, 2 + i * newTile.map.Height, lockColor);
                hrMap.SetPixel(0 + j * newTile.map.Width, 3 + i * newTile.map.Height, lockColor);
            }
            if (pieces[i, j].GetOtherNodes().Contains("Lock-East"))
            {
                hrMap.SetPixel(newTile.map.Width - 1 + j * newTile.map.Width, 2 + i * newTile.map.Height, lockColor);
                hrMap.SetPixel(newTile.map.Width - 1 + j * newTile.map.Width, 3 + i * newTile.map.Height, lockColor);
            }
            if (pieces[i, j].GetOtherNodes().Contains("Lock-South"))
            {
                hrMap.SetPixel(2 + j * newTile.map.Width, newTile.map.Height - 1 + i * newTile.map.Height, lockColor);
                hrMap.SetPixel(3 + j * newTile.map.Width, newTile.map.Height - 1 + i * newTile.map.Height, lockColor);
            }
        }

        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public void printHRBitmap() {
            for (int i = 0; i < hrMap.Width; i++)
            {
                for (int j = 0; j < hrMap.Height; j++)
                {
                    Console.Write(hrMap.GetPixel(i, j) + " ");
                    
                }
                Console.WriteLine();
            }
        }
    }
}
