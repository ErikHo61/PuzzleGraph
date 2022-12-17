using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.STCC.PuzzlePieces
{
    class Towel : PuzzlePiece
    {
        public Towel() {
            States = new ObjectStates()
            {
                fire = false,
                water = true,
                air = true,
                electric = false,
                cut = false
            };
        }
    }
}
