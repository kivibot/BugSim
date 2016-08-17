using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Neural
{
    public class ThresholdActivationFunction : IActivationFunction
    {
        private double _threshold;

        public ThresholdActivationFunction(double threshold)
        {
            _threshold = threshold;
        }

        public double GetValue(double input)
        {
            if (input > _threshold)
                return 1;
            return 0;
        }
    }
}
