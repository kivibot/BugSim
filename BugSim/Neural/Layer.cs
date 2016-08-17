using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Neural
{
    public class Layer
    {

        private List<Module> _modules;

        public Layer(params Module[] modules)
            : this(modules.ToList())
        {
        }

        public Layer(List<Module> modules)
        {
            _modules = modules;
        }

        public double[] Ativate(double[] input)
        {
            double[] output = new double[_modules.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = _modules[i].Activate(input);
            }
            return output;
        }
    }
}
