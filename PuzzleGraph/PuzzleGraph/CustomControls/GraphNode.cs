using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PuzzleGraph.Models;

namespace PuzzleGraph.CustomControls
{
    public class GraphNode : Control
    {
        public string Text { get; set; }
        public int nodeNumber { get; set; }
        public List<LevelObject> LevelObjects { get => levelObjects; set => levelObjects = value; }

        private List<LevelObject> levelObjects;

        static GraphNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphNode), new FrameworkPropertyMetadata(typeof(GraphNode)));
        }
    }
}
