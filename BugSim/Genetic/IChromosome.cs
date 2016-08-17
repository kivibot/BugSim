using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Genetic
{
    public interface IChromosome
    {
        double Fitness { get; set; }

        void Mutate(double probability, Random random);
        IChromosome Crossover(IChromosome parentB, Random random);
    }
}
