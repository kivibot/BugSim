using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Genetic
{
    public class FitnessSurvivorSelector : ISurvivorSelector
    {
        private int _survivorCount;

        public FitnessSurvivorSelector(int survivors)
        {
            _survivorCount = survivors;
        }

        public List<T> Select<T>(List<T> candidates) where T : IChromosome
        {
            return candidates.OrderBy(a => -a.Fitness).Take(_survivorCount).ToList();
        }
    }
}
