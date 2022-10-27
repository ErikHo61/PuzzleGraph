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
using System.Windows.Shapes;
using System.Windows.Media;

namespace PuzzleGraph.Models
{
    public class DataEdge : DataEdgeBase<GraphNode>
    {
        // The direction which the target is relative to the source GraphNode
        public enum TargetDirection{ 
            Right, Down, Left, Up
        }
        //The Line control in the custom template
        public Line edge;

        //Whether this is a walkable path
        public bool isPath { get; set; } = false;
        //Is it a door?
        public bool isDoor { get; set; } = false;

        public DataEdge(GraphNode source, GraphNode target) : base(source, target)
        {
            edge = new Line();
            if (edge != null && !Double.IsNaN((double) source.GetValue(Canvas.LeftProperty))
                && !Double.IsNaN((double)target.GetValue(Canvas.LeftProperty))) {

                edge.X1 = (double)source.GetValue(Canvas.LeftProperty) + source.dWidth;
                edge.Y1 = (double)source.GetValue(Canvas.TopProperty) + source.dHeight/2;

                edge.X2 = (double)target.GetValue(Canvas.LeftProperty);
                edge.Y2 = (double)target.GetValue(Canvas.TopProperty) + target.dHeight/2;
            }
           
        }

        public DataEdge()
           : base(null, null)
        {
        }

        //For overridining the default style with the template
        static DataEdge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataEdge), new FrameworkPropertyMetadata(typeof(DataEdge)));
        }

        public override void OnApplyTemplate()
        {
            edge = Template.FindName("PART_Edge", this) as Line;
            //Console.WriteLine("StrokeThickness = {0}", edge.StrokeThickness);
            UpdateLine();
            base.OnApplyTemplate();

        }

        // Update Line Method
        // Summary: Should update the line everytime anything changes source/target changes
        // also adjusts the length of line to fit between nodes
        public void UpdateLine() {

            if (edge != null && !Double.IsNaN((double)Source.GetValue(Canvas.LeftProperty))
                && !Double.IsNaN((double)Target.GetValue(Canvas.LeftProperty))) {
                if (!isPath) edge.Stroke = Brushes.Red;
                else if (isDoor) edge.Stroke = Brushes.Blue;
                else edge.Stroke = Brushes.Black;

                edge.X1 = (double)Source.GetValue(Canvas.LeftProperty) + Source.dWidth/2;
                edge.Y1 = (double)Source.GetValue(Canvas.TopProperty) + Source.dHeight/2;
        
                edge.X2 = (double)Target.GetValue(Canvas.LeftProperty) + Source.dWidth/2;
                edge.Y2 = (double)Target.GetValue(Canvas.TopProperty) + Target.dHeight/2;

                switch (findDirection(Source, Target)) {
                    case TargetDirection.Right:
                        edge.X2 -= Source.dWidth / 2;
                        edge.X1 += Source.dWidth / 2;
                        break;
                    case TargetDirection.Left:
                        edge.X2 += Source.dWidth / 2;
                        edge.X1 -= Source.dWidth / 2;
                        break;
                    case TargetDirection.Up:
                        edge.Y1 -= Source.dHeight / 2;
                        edge.Y2 += Source.dHeight / 2;
                        break;
                    case TargetDirection.Down:
                        edge.Y1 += Source.dHeight / 2;
                        edge.Y2 -= Source.dHeight / 2;
                        //Console.WriteLine("Below");
                        break;
                    default:
                        Console.WriteLine("NONE");
                        break;
                }
                
            }
          
        }

        //Finds the direction of which the target is relative to the source GraphNode
        private TargetDirection findDirection(GraphNode source, GraphNode target) {
            if ((double)target.GetValue(Canvas.LeftProperty) > (double)source.GetValue(Canvas.LeftProperty))
            {
                return TargetDirection.Right;
            }
            else if ((double)target.GetValue(Canvas.LeftProperty) < (double)source.GetValue(Canvas.LeftProperty)) {
                return TargetDirection.Left;
            } else if ((double) target.GetValue(Canvas.TopProperty) > (double)source.GetValue(Canvas.TopProperty)){
                return TargetDirection.Down;
            } else{
                return TargetDirection.Up;
            }
            
        }
    }
}
