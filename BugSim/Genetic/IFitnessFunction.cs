using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Genetic
{
    public interface IFitnessFunction<T> where T : IChromosome
    {
        double Test(T chromosome);
    }
}
