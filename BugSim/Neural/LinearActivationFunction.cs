using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Neural
{
    public class LinearActivationFunction : IActivationFunction
    {
        public double GetValue(double input)
        {
            return input;
        }
    }
}
