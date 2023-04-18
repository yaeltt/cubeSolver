using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;

namespace tryy
{
   
    public class Program
    {
       public static void Main(string[] args)
        {
            BuildDirectory b1 = new BuildDirectory();
            Cube b2 = new Cube();
           // b2.side_twist(0, 2);
            //b2.vertical_twist(1, 0);
            //b2.vertical_twist(0, 1);
            //b2.side_twist(0, 2);
            //b2.vertical_twist(0, 2);
            b1.buidStates();
            CubeSolver cs = new CubeSolver();

           cs.build_heuristic_db(b2,"wwwwwwwwwgggggggggrrrrrrrrrbbbbbbbbboooooooooyyyyyyyyy", b1.actions);
            //List<ActionInCube> l= cs.run(b2, b1.actions);
        // Console.ReadLine();
        }
    }
}
