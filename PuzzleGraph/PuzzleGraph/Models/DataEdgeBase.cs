using System;
using System.Windows.Controls;
using PuzzleGraph.CustomControls;
using QuikGraph;

namespace PuzzleGraph.Models
{
    public abstract class DataEdgeBase<TVertex> : Control, IEdge<TVertex>
    {
        

        public TVertex Source { get; set; }

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