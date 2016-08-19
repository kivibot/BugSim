using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public class Food
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }

        public Food(double x, double y, double radius)
        {
            this.X = x;
            this.Y = y;
            this.Radius = radius;
        }
    }
}
