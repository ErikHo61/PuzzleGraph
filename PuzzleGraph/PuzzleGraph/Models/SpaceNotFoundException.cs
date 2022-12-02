using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models
{
    [Serializable]
    public class SpaceNotFoundException : Exception
    {
        public SpaceNotFoundException() { }

        public SpaceNotFoundException(string message)
            : base(message) { }

        public SpaceNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
