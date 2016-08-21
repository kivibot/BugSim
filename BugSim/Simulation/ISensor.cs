using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public interface ISensor
    {
        int Outputs { get; }

        double[] GetValues(World world, Bug bug);
    }
}
