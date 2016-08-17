using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Genetic
{
    public class GeneticAlgorithm<T> where T : IChromosome
    {
        private IFitnessFunction<T> _fitnessFunc;
        private ISurvivorSelector _survivorSelector;
        private IParentSelector _parentSelector;
        private double _mutationProbability;
        private Random _random;

        public List<T> Chromosomes { get; set; }

        public GeneticAlgorithm(IFitnessFunction<T> fitnessFunc, ISurvivorSelector survivorSelector, IParentSelector parentSelector, double mutationProbability, Random random, List<T> chromosomes)
        {
            _fitnessFunc = fitnessFunc;
            _survivorSelector = survivorSelector;
            _parentSelector = parentSelector;
            _mutationProbability = mutationProbability;
            _random = random;
            Chromosomes = chromosomes;
        }

        public void RunOneGeneration()
        {
            List<T> survivors = _survivorSelector.Select(Chromosomes);

            List<Parents<T>> allParents = _parentSelector.Select(survivors);
            

            foreach (var parents in allParents)
            {
                var child = parents.ParentA.Crossover(parents.ParentB, _random);
                child.Mutate(_mutationProbability, _random);
                survivors.Add((T)child);
            }

            foreach (T chromosome in survivors)
                chromosome.Fitness = _fitnessFunc.Test(chromosome);

            Chromosomes = survivors;
        }

        public void RunFirstGen()
        {
            foreach (T chromosome in Chromosomes)
                chromosome.Fitness = _fitnessFunc.Test(chromosome);
        }
    }
}
