using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public interface IColored
    {
        double Red { get; }
        double Green { get; }
        double Blue { get; }
    }
}
