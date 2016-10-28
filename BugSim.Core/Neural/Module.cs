using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugSim.Core.Neural
{
    public class Module
    {
        private readonly double[] _weights;
        private readonly double _bias;
        private Func<double, double> _activationFunction;

        public Module(double[] weights, double bias, Func<double, double> activationFunction)
        {
            _weights = weights;
            _bias = bias;
            _activationFunction = activationFunction;
        }

        public double Activate(double[] inputs)
        {
            double value = _bias;
            for (int i = 0; i < inputs.Length; i++)
                value += _weights[i] * inputs[i];
            return _activationFunction(value);
        }
    }
}
