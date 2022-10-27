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
            //Another possible entry point

            //Rectangle rect = new Rectangle
            //{
            //    Width = 50,
            //    Height = 50,
            //    Fill = new SolidColorBrush(Color.FromRgb(200, 60, 90)),
            //    Stroke = Brushes.Black
            //};
            //Canvas.SetLeft(rect, GraphCanvas.ActualWidth/2);
            //Canvas.SetTop(rect, GraphCanvas.ActualHeight/2);
            //GraphCanvas.Children.Add(rect);
            

            //foreach (GraphNode v in graph.Vertices)
            //{ 

            //}

            gm = new GraphManager(GraphCanvas);
            //st.graphSetup();
            gm.SmallGraphSetup();
            Rule rl = new RuleStart();
            Rule rl2 = new RuleExpand();
            Rule rl3 = new RuleEnd();
            Recipe rec = new RecipeBasic();
            gm.ExecuteRecipe(rec);
            //gm.ExecuteGrammar(rl);
            //gm.ExecuteGrammar(rl2);
            //gm.ExecuteGrammar(rl);
            //gm.ExecuteGrammar(rl3);
            gm.printGraph();

        }

        void OnClick_Refresh(object sender, RoutedEventArgs e) {
            Console.WriteLine("Refreshing Graph!");
            gm.refreshGraph();
        }

      

       

        //private GraphExample GraphExample_Setup()
        //{
        //    //Lets make new data graph instance
        //    var dataGraph = new GraphExample();
        //    //Now we need to create edges and vertices to fill data graph
        //    //This edges and vertices will represent graph structure and connections
        //    //Lets make some vertices
        //    for (int i = 1; i < 16; i++)
        //    {
        //        //Create new vertex with specified Text. Also we will assign custom unique ID.
        //        //This ID is needed for several features such as serialization and edge routing algorithms.
        //        //If you don't need any custom IDs and you are using automatic Area.GenerateGraph() method then you can skip ID assignment
        //        //because specified method automaticaly assigns missing data ids (this behavior is controlled by method param).
        //        var dataVertex = new DataVertex("MyVertex " + i);
        //        //Add vertex to data graph
        //        dataGraph.AddVertex(dataVertex);
        //    }

        //    //Now lets make some edges that will connect our vertices
        //    //get the indexed list of graph vertices we have already added
        //    var vlist = dataGraph.Vertices.ToList();
        //    //Then create two edges optionaly defining Text property to show who are connected
        //    //TOP ROW
        //    var dataEdge = new DataEdge(vlist[0], vlist[5]) { Text = string.Format("{0} -> {1}", vlist[0], vlist[5]) };
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[1], vlist[0]) { Text = string.Format("{0} -> {1}", vlist[1], vlist[0]) };
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[2], vlist[1]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[3], vlist[2]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[4], vlist[3]);
        //    dataGraph.AddEdge(dataEdge);

        //    //MIDDLE ROW
        //    dataEdge = new DataEdge(vlist[5], vlist[10]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[7], vlist[8]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[8], vlist[9]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[9], vlist[14]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[9], vlist[4]);
        //    dataGraph.AddEdge(dataEdge);

        //    ////BOTTOM ROW
        //    dataEdge = new DataEdge(vlist[10], vlist[11]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[11], vlist[6]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[11], vlist[12]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[13], vlist[8]);
        //    dataGraph.AddEdge(dataEdge);
        //    dataEdge = new DataEdge(vlist[14], vlist[13]);
        //    dataGraph.AddEdge(dataEdge);



        //    return dataGraph;

        //}
       
    }
}
