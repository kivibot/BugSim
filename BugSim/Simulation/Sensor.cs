using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public class Sensor
    {
        public double Rotation { get; private set; }
        public double Length { get; set; }
        public double MaxLength { get; private set; }

        public double A { get; set; }
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }

        public Sensor(double rotation, double max, double a, double r, double g, double b)
        {
            this.Rotation = rotation;
            this.MaxLength = max;
            A = a;
            R = r;
            G = g;
            B = b;
        }
    }
}
