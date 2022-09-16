using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph;
using PuzzleGraph.CustomControls;
using System.Windows;
using System.Windows.Controls;

namespace PuzzleGraph.Models
{
    public class DataEdge : DataEdgeBase<GraphNode>
    {
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;
        public DataEdge(GraphNode source, GraphNode target) : base(source, target)
        {

        }

        public DataEdge()
           : base(null, null)
        {
        }

        static DataEdge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataEdge), new FrameworkPropertyMetadata(typeof(DataEdge)));
        }

    }
}
