using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Neural
{
    public class SigmoidActivationFunction : IActivationFunction
    {
        public double GetValue(double input)
        {
            return 1.0 / (1.0 + Math.Exp(-input));
        }
    }
}
