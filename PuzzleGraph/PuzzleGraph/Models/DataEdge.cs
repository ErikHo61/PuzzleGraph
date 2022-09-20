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

namespace PuzzleGraph.Models
{
    public class DataEdge : DataEdgeBase<GraphNode>
    {
        public enum TargetDirection{ 
            Right, Down, Left, Up
        }

        public Line edge;

        public DataEdge(GraphNode source, GraphNode target) : base(source, target)
        {
            edge = new Line();
            if (edge != null) {
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

        //Should update the position of the line everytime source/target changes
        private void UpdateLine() {
            if (edge != null && Source != null) {
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
                        Console.WriteLine("Below");
                        break;
                    default:
                        Console.WriteLine("NONE");
                        break;
                }
               //edge.X2 = (double)Source.GetValue(Canvas.LeftProperty) + Source.dWidth / 2 + 70;
                //edge.Y2 = (double)Source.GetValue(Canvas.TopProperty) + Source.dHeight / 2 + 70;
            }
          
        }

        private TargetDirection findDirection(GraphNode source, GraphNode target) {

            if ((double)target.GetValue(Canvas.LeftProperty) > (double)source.GetValue(Canvas.LeftProperty))
            {
                return TargetDirection.Right;
            }
            else if ((double)target.GetValue(Canvas.LeftProperty) < (double)source.GetValue(Canvas.LeftProperty)) {
                return TargetDirection.Left;
            } else if ((double) target.GetValue(Canvas.TopProperty) > (double)source.GetValue(Canvas.TopProperty)){
                return TargetDirection.Down;
            } else
            {
                return TargetDirection.Up;
            }
            
        }
    }
}
