using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Bug
{
    public class Bug
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Rotation { get; set; }

        public double ForwardPower { get; set; }
        public double AngularPower { get; set; }
    }
}
