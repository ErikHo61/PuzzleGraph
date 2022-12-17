using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.STCC.PuzzlePieces
{
    public struct ObjectStates
    {
        public bool fire;
        public bool water;
        public bool air;
        public bool electric;
        public bool cut;
    }

    public abstract class PuzzlePiece
    {
        protected ObjectStates States { get; set; }

        protected ObjectStates currentState { get; set; }
    }

}
