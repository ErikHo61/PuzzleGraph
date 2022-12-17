using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGraph.Models.STCC.PuzzlePieces
{
    class Paper : PuzzlePiece
    {
        public Paper() {
            States = new ObjectStates{ 
                fire = true,
                water = true,
                air = false,
                electric = false,
                cut = true
                
            };


                

            
        }
    }
}
