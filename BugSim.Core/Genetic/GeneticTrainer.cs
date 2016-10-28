using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugSim.Core.Genetic
{
    public class GeneticTrainer<T> where T : IChromosome
    {
        private readonly Func<List<T>, List<T>> _terminalConditionFilter;
        private readonly Func<List<T>, List<T>> _survivalFilter;
        private readonly Func<List<T>, List<Tuple<T, T>>> _parentSelector;

        public List<T> Population { get; set; }

        private readonly Random _radom;

        public double MutationProbability { get; set; }

        public GeneticTrainer(Func<List<T>, List<T>> terminalConditionFilter, Func<List<T>, List<T>> survivalFilter,
            Func<List<T>, List<Tuple<T, T>>> parentSelector, List<T> population, Random random, double mutationProbability)
        {
            _terminalConditionFilter = terminalConditionFilter;
            _survivalFilter = survivalFilter;
            _parentSelector = parentSelector;
            Population = population;
            _radom = random;
        }
        
        public void PopulateNextGeneration()
        {
            List<T> alive = _terminalConditionFilter(Population);
            List<T> children = new List<T>();

            foreach (Tuple<T, T> parents in _parentSelector(alive))
            {
                T child = (T)parents.Item1.Crossover(parents.Item2, _radom);
                child.Mutate(MutationProbability, _radom);
                children.Add(child);
            }

            Population = _survivalFilter(alive).Union(children).ToList();
        }

    }
}
