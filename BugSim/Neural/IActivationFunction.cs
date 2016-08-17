using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Neural
{
    public interface IActivationFunction
    {
        double GetValue(double input);
    }
}
