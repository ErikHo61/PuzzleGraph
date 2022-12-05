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
        //type of graphnode, for determining if nodes match in a graph grammar
        public string Type { get; set; }
        //for determining if nodes match in a graph grammar
        public int ruleID { get; set; }
        //for numbering nodes in a hostgraph
        public int graphID { get; set; }
        public Tuple<int, int> gridPos;
        private Ellipse body;
        private TextBlock content;

        //the height of the ellipse in the template
        public int dHeight;
        //the width of the ellipse in the template
        public int dWidth;
        public GraphNode coupleNode;
        //children that are used to activate this node
        public List<GraphNode> actChildren;

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
            content = Template.FindName("PART_Text", this) as TextBlock;
            dHeight = (int) body.Height;
            dWidth = (int) body.Width;
            content.Text = Type;
            base.OnApplyTemplate();
        }

        public override string ToString()
        {
            return  Type + graphID + ruleID;

        }

    }
}
