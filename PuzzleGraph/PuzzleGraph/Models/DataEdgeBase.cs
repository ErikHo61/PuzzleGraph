using System;
using System.Windows.Controls;
using PuzzleGraph.CustomControls;
using QuikGraph;

namespace PuzzleGraph.Models
{
    public abstract class DataEdgeBase<TVertex> : Control, IEdge<TVertex>
    {
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;

        public TVertex Source {
            get => Source;
  
            set { 
                X1 = (GraphNode) Source.GetValue(Canvas.LeftProperty); 
            }
        }

        /// <summary>
        /// Target vertex
        /// </summary>
        public TVertex Target { get; set; }

       

        public DataEdgeBase(TVertex source, TVertex target)
        {
            Source = source;
            Target = target;
        }


    }
}