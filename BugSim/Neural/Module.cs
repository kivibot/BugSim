using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Neural
{
    public class Module
    {
        private double[] _weights;
        private double _bias;
        private IActivationFunction _activationFunction;

        public Module(double[] weights, double bias, IActivationFunction activationFunction)
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
            return _activationFunction.GetValue(value);
        }

    }
}
