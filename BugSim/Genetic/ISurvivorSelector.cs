using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Genetic
{
    public interface ISurvivorSelector
    {
        List<T> Select<T>(List<T> candidates) where T : IChromosome;
    }
}
