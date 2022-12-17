using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.STCC.PuzzlePieces
{
    class Circuit : PuzzlePiece
    {
        public Circuit() {
            States = new ObjectStates()
            {
                fire = false,
                water = true,
                air = false,
                electric = true,
                cut = true
            };
            
        }
    }
}
