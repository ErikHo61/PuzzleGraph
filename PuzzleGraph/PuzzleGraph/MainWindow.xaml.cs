using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GraphX.Common.Enums;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Controls;
using PuzzleGraph.Models;
using PuzzleGraph.CustomControls;
using QuikGraph;
using System.Drawing;
using PuzzleGraph.Models.Rules;
using PuzzleGraph.Models.Recipes;
using PuzzleGraph.Models.ShapeGrammars;
using System.IO;

namespace PuzzleGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        GraphManager gm;
        public MainWindow()
        {
            InitializeComponent();
            //ZoomControl.SetViewFinderVisibility(gg_zoomctrl, Visibility.Visible);
            //gg_zoomctrl.ZoomToFill();

            Loaded += MainWindow_Loaded;
        }

        public void Dispose()
        {
      
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            

            gm = new GraphManager(GraphCanvas);
            //st.graphSetup();
            //gm.SmallGraphSetup();
            //Rule rl = new RuleStartSmall();
            //Rule rl2 = new RuleExpand();
            //Rule rl3 = new RuleEnd();
            //Rule rl4 = new RuleBetween();
            //Recipe rec = new RecipeBasic();
            //gm.ExecuteRecipe(rec);
            //gm.ExecuteGrammar(rl);
            //gm.ExecuteGrammar(rl2);
            //gm.ExecuteGrammar(rl);
            //gm.ExecuteGrammar(rl3);
            //gm.ExecuteGrammar(rl4);
            //gm.printGraph();

            ShapeManager sm = new ShapeManager();
            
            myImage.Source = sm.initBitMap();
            sm.printHRBitmap();
            
            
            

        }

        void OnClick_Refresh(object sender, RoutedEventArgs e) {
            Console.WriteLine("Refreshing Graph!");
            gm.refreshGraph();
        }

        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }


    }
}
