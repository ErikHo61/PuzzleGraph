using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.STCC.PuzzlePieces
{
    class Metal : PuzzlePiece
    {
        public Metal() {
            States = new ObjectStates()
            {
                fire = true,
                water = false,
                air = false,
                electric = true,
                cut = true
            };
        }
    }
}
