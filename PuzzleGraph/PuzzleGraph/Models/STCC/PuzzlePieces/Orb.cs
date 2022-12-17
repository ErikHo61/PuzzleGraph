using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.STCC.PuzzlePieces
{
    class Orb : PuzzlePiece
    {
        public Orb() {
            States = new ObjectStates()
            {
                fire = true,
                water = true,
                air = true,
                electric = true,
                cut = true
            };
        }
    }
}
