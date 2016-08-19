using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Genetic
{
    public class FitnessWeightedParentSelector : IParentSelector
    {
        private Random _random;
        private int _count;

        public FitnessWeightedParentSelector(int count, Random random)
        {
            _count = count;
            _random = random;
        }

        public List<Parents<T>> Select<T>(List<T> candidates) where T : IChromosome
        {
            List<Parents<T>> parents = new List<Parents<T>>();

            candidates.Sort((c0, c1) => c1.Fitness.CompareTo(c1.Fitness));

            double totalFitness = 0;
            foreach (T candidate in candidates)
                totalFitness += candidate.Fitness;

            for (int i = 0; i < _count; i++)
            {
                parents.Add(new Parents<T>(SelectCandidate(candidates, totalFitness), SelectCandidate(candidates, totalFitness)));
            }


            return parents;
        }

        private T SelectCandidate<T>(List<T> candidates, double totalFitness) where T : IChromosome
        {
            double value = _random.NextDouble() * totalFitness;
            foreach (T chromosome in candidates)
            {
                value -= chromosome.Fitness;
                if (value <= 0)
                    return chromosome;
            }
            throw new AggregateException();
        }
    }
}
