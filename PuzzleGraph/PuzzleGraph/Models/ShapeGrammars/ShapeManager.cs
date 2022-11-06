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
        Bitmap lrMap;
        Bitmap hrMap;

        public ShapeManager() {
            lrMap = new Bitmap(8, 8);
            hrMap = new Bitmap(128, 128);
        }

        public BitmapImage initBitMap() {
            for (int i = 0; i < hrMap.Width; i++) {
                for (int j = 0; j < hrMap.Height; j++) {
                    Color newColor = Color.FromArgb(150, 0, 0);
                    hrMap.SetPixel(i, j, newColor);
                }
            }

            

            return BitmapToImageSource(hrMap);
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
                    Console.Write(hrMap.GetPixel(i, j).R + " ");
                    
                }
                Console.WriteLine();
            }
        }
    }
}
