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

        public Sensor(double rotation, double max)
        {
            this.Rotation = rotation;
            this.MaxLength = max;
        }
    }
}
