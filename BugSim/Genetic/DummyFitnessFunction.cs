using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Genetic
{
    public class DummyFitnessFunction<T> : IFitnessFunction<T> where T : IChromosome
    {
        public double Test(T chromosome)
        {
            return 0;
        }
    }
}
