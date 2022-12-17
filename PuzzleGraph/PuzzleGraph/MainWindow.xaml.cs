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
using PuzzleGraph.Models.ShapeGrammars.DungeonStructure;
using PuzzleGraph.Models.YAMLExport;
using YamlDotNet.Serialization;

namespace PuzzleGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        GraphManager gm;
        LevelNode[,] initialGrid;
        LevelGraph initialGraph;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        public void Dispose()
        {
      
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gm = new GraphManager(GraphCanvas);
            //st.graphSetup();
            gm.SmallGraphSetup();
            //Rule rl = new RuleStartSmall2();
            //Rule rl2 = new RuleGate();
            //Rule rl3 = new RulePuzzleToPuzzle();
            //Rule rl4 = new RuleSingleLock();

            //Rule rl5 = new RulePuzzle();
            //Rule rl6 = new RuleStartG();
            //Rule rl7 = new RuleResolvePuzzle();

            //Rule rl4 = new RuleGate();
            //Rule rl8 = new RulePuzzleReward();

            //Rule rl9 = new RuleSinglePuzzle();
            //Rule rl10 = new RuleDoublePuzzle();
            //Rule rl11 = new RuleSingleLock();
            //Rule rl12 = new RuleKeyLockItem();
            //Rule rl13 = new RuleMultiKey();
            //Recipe rec = new RecipeBasic();
            //gm.ExecuteRecipe(rec);
            //gm.ExecuteRecipe(new RecipeSmall());
            //gm.ExecuteRecipe(new RecipeLoop());
            //gm.ExecuteRecipe(new RecipeLoop());
            //gm.ExecuteRecipe(new RecipeMedium());
            gm.ExecuteRecipe(new RecipeMulti());
            //gm.ExecuteGrammar(rl);
            //gm.ExecuteGrammar(rl2);
            //gm.ExecuteGrammar(rl3);
            //gm.ExecuteGrammar(rl2);
            //gm.ExecuteGrammar(rl4);

            gm.refreshGraph();
            gm.printGraphDFS();

            gm.printGraphIDs();
            Console.WriteLine("HELLO WHERE IS MY PRINT");

            DungeonManager dm = new DungeonManager(8, 8);
            dm.Init(gm.hostGraph);
            dm.CreateDungeonStructure(gm.rootNode);
            dm.print();

            dm.DungeonRealization();

            ShapeManager sm = new ShapeManager(gm.hostGraph, dm.GetPieces(), dm.GetRootPos());
            sm.ConvertToHRMap();
            myImage.Source = sm.refreshBitMap();

            ExportManager em = new ExportManager(sm.GetHRBitmap(), gm.hostGraph, sm.GetTilesInfo());
            em.path = "C:/Users/Erik/Downloads/RecipeMulti.yaml";
            em.Serialize();
            //sm.printHRBitmap();

        }

        

        private void createInitialGraph(int w, int h) {
            initialGraph = new LevelGraph();
            initialGrid = new LevelNode[h, w];
            for (int i = 0; i < h; i++) {
                for (int j = 0; j < w; j++) {
                    initialGrid[i, j] = new LevelNode();
                    initialGraph.AddVertex(initialGrid[i, j]);
                }
            }

            for (int i = 0; i < w-1; i++) {
                initialGraph.AddEdge(new LevelEdge(initialGrid[0, i], initialGrid[0, i + 1]));
                
            }

            for (int i = 1; i < h; i++)
            {
                for (int j = 0; j < w-1; j++)
                {
                    initialGraph.AddEdge(new LevelEdge(initialGrid[i, j], initialGrid[i, j + 1]));
                    //initialGraph.AddEdge(new LevelEdge(initialGrid[i, j+1], initialGrid[i, j]));

                    initialGraph.AddEdge(new LevelEdge(initialGrid[i-1, j], initialGrid[i, j]));
                    //initialGraph.AddEdge(new LevelEdge(initialGrid[i, j], initialGrid[i-1, j]));

                }
                initialGraph.AddEdge(new LevelEdge(initialGrid[i - 1, w-1], initialGrid[i, w-1]));
                //initialGraph.AddEdge(new LevelEdge(initialGrid[i, w-1], initialGrid[i-1, w-1]));
            }
            initialGraph.AddEdge(new LevelEdge(initialGrid[h - 1, w - 1], initialGrid[h-2, w - 1]));
            //initialGraph.AddEdge(new LevelEdge(initialGrid[h - 2, w - 1], initialGrid[h-1, w - 1]));

            //Printing for checking out edges
            //for (int i = 0; i < h; i++)
            //{
            //    for (int j = 0; j < w; j++)
            //    {
            //        Console.Write(initialGraph.OutDegree(initialGrid[i, j]));
            //    }
            //    Console.WriteLine();
            //}

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
