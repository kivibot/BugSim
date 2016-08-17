using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Neural
{
    public class Network
    {
        private List<Layer> _layers = new List<Layer>();

        public Network(params Layer[] layers)
            : this(layers.ToList())
        { }

        public Network(List<Layer> layers)
        {
            _layers = layers;
        }

        public double[] Activate(double[] input)
        {
            double[] current = input;
            foreach (Layer layer in _layers)
                current = layer.Ativate(current);
            return current;
        }
    }
}
