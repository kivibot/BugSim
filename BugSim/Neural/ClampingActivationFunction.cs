using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Neural
{
    public class ClampingActivationFunction : IActivationFunction
    {
        public double GetValue(double input)
        {
            return Math.Min(1, Math.Max(-1, input));
        }
    }
}
