using PuzzleGraph.Models.ShapeGrammars.DungeonStructure;
using PuzzleGraph.Models.ShapeGrammars.HRMap;
using PuzzleGraph.Models.YAMLExport;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PuzzleGraph.Models.ShapeGrammars
{
    public class ShapeManager
    {
        Bitmap hrMap;
        Piece[,] pieces;
        readonly Tuple<int, int> rootPos;
        TileFactory tf;
        Dictionary<string, Color> colorDict;
        List<TileInfo> tiles;
        HostGraph hg;

        public ShapeManager(HostGraph hg, Piece[,] pieces, Tuple<int, int> rootPos) {
            hrMap = new Bitmap(36, 36);
            tf = new TileFactory();
            colorDict = new Dictionary<string, System.Drawing.Color>();
            tiles = new List<TileInfo>();
            initColorDict();
            this.pieces = pieces;
            this.rootPos = rootPos;
            this.hg = hg;
        }

        public Bitmap GetHRBitmap() {
            return hrMap;
        }

        private void initColorDict() {
            colorDict.Add("pp", Color.FromArgb(234, 44, 240));//pink
            colorDict.Add("pi", Color.FromArgb(195, 44, 240));//purple
            colorDict.Add("pr", Color.FromArgb(103, 44, 240));//blue
            colorDict.Add("k", Color.FromArgb(255, 255, 51));//yellow
            colorDict.Add("km", Color.FromArgb(220, 220, 51));//yellow
            //colorDict.Add("l", Color.FromArgb(118, 16, 16)); //brown
            //colorDict.Add("lm", Color.FromArgb(118, 18, 18));//brown
            colorDict.Add("tp", Color.FromArgb(134, 106, 192)); // light purple
            colorDict.Add("g", Color.FromArgb(240, 44, 64)); // red
            
        }

        public List<TileInfo> GetTilesInfo() {
            return tiles;
        }

        public BitmapImage refreshBitMap() {
            //var resizedBitmap = new Bitmap(hrMap, new Size(hrMap.Width/2, hrMap.Height/2));
            //var bi = Sharpen(hrMap);
            return BitmapToImageSource(hrMap);
        }

        public static Bitmap Sharpen(Bitmap image)
        {
            Bitmap sharpenImage = (Bitmap)image.Clone();

            int filterWidth = 3;
            int filterHeight = 3;
            int width = image.Width;
            int height = image.Height;

            // Create sharpening filter.
            double[,] filter = new double[filterWidth, filterHeight];
            filter[0, 0] = filter[0, 1] = filter[0, 2] = filter[1, 0] = filter[1, 2] = filter[2, 0] = filter[2, 1] = filter[2, 2] = -1;
            filter[1, 1] = 9;

            double factor = 1.0;
            double bias = 0.0;

            Color[,] result = new Color[image.Width, image.Height];

            // Lock image bits for read/write.
            BitmapData pbits = sharpenImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // Declare an array to hold the bytes of the bitmap.
            int bytes = pbits.Stride * height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(pbits.Scan0, rgbValues, 0, bytes);

            int rgb;
            // Fill the color array with the new sharpened color values.
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    double red = 0.0, green = 0.0, blue = 0.0;

                    for (int filterX = 0; filterX < filterWidth; filterX++)
                    {
                        for (int filterY = 0; filterY < filterHeight; filterY++)
                        {
                            int imageX = (x - filterWidth / 2 + filterX + width) % width;
                            int imageY = (y - filterHeight / 2 + filterY + height) % height;

                            rgb = imageY * pbits.Stride + 3 * imageX;

                            red += rgbValues[rgb + 2] * filter[filterX, filterY];
                            green += rgbValues[rgb + 1] * filter[filterX, filterY];
                            blue += rgbValues[rgb + 0] * filter[filterX, filterY];
                        }
                        int r = Math.Min(Math.Max((int)(factor * red + bias), 0), 255);
                        int g = Math.Min(Math.Max((int)(factor * green + bias), 0), 255);
                        int b = Math.Min(Math.Max((int)(factor * blue + bias), 0), 255);

                        result[x, y] = Color.FromArgb(r, g, b);
                    }
                }
            }

            // Update the image with the sharpened pixels.
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    rgb = y * pbits.Stride + 3 * x;

                    rgbValues[rgb + 2] = result[x, y].R;
                    rgbValues[rgb + 1] = result[x, y].G;
                    rgbValues[rgb + 0] = result[x, y].B;
                }
            }

            // Copy the RGB values back to the bitmap.
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, pbits.Scan0, bytes);
            // Release image bits.
            sharpenImage.UnlockBits(pbits);

            return sharpenImage;
        }

        public void ConvertToHRMap() {
            //hrMap.UnlockBits();
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
                        newTile = tf.GetTile("empty");
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

                        if (colorDict.ContainsKey(pieces[i, j].nodeType)) {
                            placeObjects(newTile, i, j);
                        }
                        
                    }
                    
                }
            }
            Console.WriteLine("");
        }

        //Place objects into  bitmap
        private void placeObjects(Tile newTile, int y, int x) {
            var p = pieces[y, x];
            
            hrMap.SetPixel(3 + x * newTile.map.Width, 3 + y * newTile.map.Height, colorDict[pieces[y, x].nodeType]);
            TileInfo ti = new TileInfo();
            bool toAdd = false;
            if (hg.getGraphNode(pieces[y, x].hostgraphID).coupleNode != null) { // if it has a couple node
                toAdd = true;
                ti.coupleID = hg.getGraphNode(pieces[y, x].hostgraphID).coupleNode.graphID;
                
            } if (hg.getGraphNode(pieces[y, x].hostgraphID).actChildren != null) { //if it is pi, l or lm it will have act children
                toAdd = true;
                ti.actChildren ="";
                foreach (var child in hg.getGraphNode(pieces[y, x].hostgraphID).actChildren) {
                    ti.actChildren += "-";
                    ti.actChildren+=child.graphID;
                    
                }
                
                
            } else {
                toAdd = true;
            }
            if (toAdd) {
                ti.type = pieces[y, x].nodeType;
                ti.graphID = pieces[y, x].hostgraphID;
                ti.x = 3 + x * newTile.map.Width;
                ti.y = 3 + y * newTile.map.Height;
                tiles.Add(ti);
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
            System.Drawing.Color lockColor = System.Drawing.Color.FromArgb(118, 16, 16);
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
