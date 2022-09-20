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

namespace PuzzleGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
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
            var graph = new DataGraph();

            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 5; j++)
                {
                    GraphNode gn = new GraphNode();
                    Canvas.SetTop(gn, 60 +  i * 70);
                    Canvas.SetLeft(gn, 100 + j * 70);                
                    graph.AddVertex(gn);
                    GraphCanvas.Children.Add(gn);
                }

            }

            //foreach (GraphNode v in graph.Vertices)
            //{ 

            //}

            List<GraphNode> myl = graph.Vertices.ToList();
            Console.WriteLine("Number of graphnodes {0}", myl.Count);
            GraphNode gn1 = myl[0];
            Console.WriteLine("Left = {0}, Top={1}", gn1.GetValue(Canvas.LeftProperty), gn1.GetValue(Canvas.TopProperty));
            GraphNode gn2 = myl[1];
            Console.WriteLine("Left = {0}, Top={1}", gn2.GetValue(Canvas.LeftProperty), gn2.GetValue(Canvas.TopProperty));
            GraphNode gn3 = myl[2];
            Console.WriteLine("Left = {0}, Top={1}", gn3.GetValue(Canvas.LeftProperty), gn3.GetValue(Canvas.TopProperty));
            GraphNode gn4 = myl[3];
            Console.WriteLine("Left = {0}, Top={1}", gn4.GetValue(Canvas.LeftProperty), gn4.GetValue(Canvas.TopProperty));
            GraphNode gn5 = myl[4];
            Console.WriteLine("Left = {0}, Top={1}", gn5.GetValue(Canvas.LeftProperty), gn5.GetValue(Canvas.TopProperty));
            GraphNode gn6 = myl[5];
            Console.WriteLine("Left = {0}, Top={1}", gn6.GetValue(Canvas.LeftProperty), gn6.GetValue(Canvas.TopProperty));
            GraphNode gn7 = myl[6];
            Console.WriteLine("Left = {0}, Top={1}", gn7.GetValue(Canvas.LeftProperty), gn7.GetValue(Canvas.TopProperty));
            GraphNode gn8 = myl[7];
            Console.WriteLine("Left = {0}, Top={1}", gn7.GetValue(Canvas.LeftProperty), gn7.GetValue(Canvas.TopProperty));
            GraphNode gn9 = myl[8];
            Console.WriteLine("Left = {0}, Top={1}", gn7.GetValue(Canvas.LeftProperty), gn7.GetValue(Canvas.TopProperty));
            GraphNode gn10 = myl[9];
            Console.WriteLine("Left = {0}, Top={1}", gn7.GetValue(Canvas.LeftProperty), gn7.GetValue(Canvas.TopProperty));
            GraphNode gn13 = myl[12];
            Console.WriteLine("Left = {0}, Top={1}", gn7.GetValue(Canvas.LeftProperty), gn7.GetValue(Canvas.TopProperty));


            Line l = new Line()
            {
                X1 = 100+40,
                X2 = 170,
                Y1 = 60+20,
                Y2 = 60+20,
                StrokeThickness = 3,
                Stroke = Brushes.Black
            };

            DataEdge de = new DataEdge(gn1, gn2);
            DataEdge de2 = new DataEdge(gn2, gn3);
            DataEdge de3 = new DataEdge(gn7, gn8);
            DataEdge de4 = new DataEdge(gn8, gn9);
            DataEdge de5 = new DataEdge(gn8, gn3);
            DataEdge de6 = new DataEdge(gn8, gn13);

            GraphCanvas.Children.Add(de);
            GraphCanvas.Children.Add(de2);
            GraphCanvas.Children.Add(de3);
            GraphCanvas.Children.Add(de4);
            GraphCanvas.Children.Add(de5);
            GraphCanvas.Children.Add(de6);

            Console.WriteLine("Hello");

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
