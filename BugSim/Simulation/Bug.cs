using BugSim.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public class Bug
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Rotation { get; set; }
        public double Radius { get; set; }

        public double ForwardPower { get; set; }
        public double AngularPower { get; set; }

        public List<Sensor> Sensors { get; set; }

        public Network Network { get; set; }

        public double Score { get; set; }

        public double Memory0 { get; set; }
        public double Memory1 { get; set; }
        public double Memory2 { get; set; }

        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }

        public double Health { get; set; }
        public double Energy { get; set; }
    }
}
