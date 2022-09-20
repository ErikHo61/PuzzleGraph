using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using PuzzleGraph.Models;

namespace PuzzleGraph.CustomControls
{
    public class GraphNode : Control
    {
        public string Text { get; set; }
        public int nodeNumber { get; set; }
        public List<LevelObject> LevelObjects { get => levelObjects; set => levelObjects = value; }

        private List<LevelObject> levelObjects;
        private Ellipse body;

        public int dHeight;
        public int dWidth;

        //public static DependencyProperty dWidthProperty = DependencyProperty.Register("Width", typeof(int), typeof(GraphNode), new PropertyMetadata(true));

        //public int dWidth
        //{
        //    get { return (int)GetValue(dWidthProperty); }
        //    set { SetValue(dWidthProperty, value); }
        //}

        static GraphNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphNode), new FrameworkPropertyMetadata(typeof(GraphNode)));
        }

        public override void OnApplyTemplate()
        {
            body = Template.FindName("PART_Body", this) as Ellipse;
            dHeight = (int) body.Height;
            dWidth = (int) body.Width;
            base.OnApplyTemplate();
        }
    }
}
