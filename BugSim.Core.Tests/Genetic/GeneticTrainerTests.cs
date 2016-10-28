using BugSim.Core.Genetic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugSim.Core.Tests.Genetic
{
    [TestFixture]
    public class GeneticTrainerTests
    {
        private class DummyChromosome : IChromosome
        {
            public string Id { get; set; }
            public double Fitness { get; set; }
            public bool MutateCalled { get; set; }

            public DummyChromosome(string id)
            {
                Id = id;
                MutateCalled = false;
            }

            public IChromosome Crossover(IChromosome parentB, Random random)
            {
                return new DummyChromosome(Id + ((DummyChromosome)parentB).Id);
            }

            public void Mutate(double probability, Random random)
            {
                MutateCalled = true;
            }

            public override bool Equals(object obj)
            {
                return obj != null && obj is DummyChromosome && ((DummyChromosome)obj).Id == Id;
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }
        }

        private readonly Func<List<DummyChromosome>, List<DummyChromosome>> _passthroughFilter = (list) => list;
        private readonly Func<List<DummyChromosome>, List<DummyChromosome>> _dummyFilter = (list) => new List<DummyChromosome>();
        private readonly Func<List<DummyChromosome>, List<Tuple<DummyChromosome, DummyChromosome>>> _dummyParentSelector = (list) => new List<Tuple<DummyChromosome, DummyChromosome>>();

        [Test]
        public void TestPopulateNextGenerationWithoutFiltersOrCrossover()
        {
            List<DummyChromosome> initialPopulation = new DummyChromosome[] { new DummyChromosome("a"), new DummyChromosome("b") }.ToList();
            GeneticTrainer<DummyChromosome> trainer = new GeneticTrainer<DummyChromosome>(_passthroughFilter, _passthroughFilter,
                _dummyParentSelector, initialPopulation.ToList(), new Random(0), 0);

            trainer.PopulateNextGeneration();

            CollectionAssert.AreEquivalent(initialPopulation, trainer.Population);
            foreach (var chromosome in trainer.Population)
                Assert.IsFalse(chromosome.MutateCalled);
        }

        [Test]
        public void TestPopulateNextGenerationWithTerminalFilter()
        {
            List<DummyChromosome> initialPopulation = new DummyChromosome[] { new DummyChromosome("a"), new DummyChromosome("b"), new DummyChromosome("c") }.ToList();
            Func<List<DummyChromosome>, List<DummyChromosome>> terminalFilter = (list) => list.Where((v) => v.Id != "b").ToList();

            GeneticTrainer<DummyChromosome> trainer = new GeneticTrainer<DummyChromosome>(terminalFilter, _passthroughFilter,
                _dummyParentSelector, initialPopulation.ToList(), new Random(0), 0);

            trainer.PopulateNextGeneration();

            CollectionAssert.AreEquivalent(new DummyChromosome[] { new DummyChromosome("a"), new DummyChromosome("c") },
                trainer.Population);
        }

        [Test]
        public void TestPopulateNextGenerationWithSurvivalFilter()
        {
            List<DummyChromosome> initialPopulation = new DummyChromosome[] { new DummyChromosome("a"), new DummyChromosome("b"), new DummyChromosome("c") }.ToList();
            Func<List<DummyChromosome>, List<DummyChromosome>> survivalFilter = (list) => list.Where((v) => v.Id != "c").ToList();

            GeneticTrainer<DummyChromosome> trainer = new GeneticTrainer<DummyChromosome>(_passthroughFilter, survivalFilter,
                _dummyParentSelector, initialPopulation.ToList(), new Random(0), 0);

            trainer.PopulateNextGeneration();

            CollectionAssert.AreEquivalent(new DummyChromosome[] { new DummyChromosome("a"), new DummyChromosome("b") },
                trainer.Population);
        }

        [Test]
        public void TestPopulateNextGenerationWithCrossover()
        {
            List<DummyChromosome> initialPopulation = new DummyChromosome[] { new DummyChromosome("a"), new DummyChromosome("b") }.ToList();
            GeneticTrainer<DummyChromosome> trainer = new GeneticTrainer<DummyChromosome>(_passthroughFilter, _dummyFilter,
                (ignore) => new Tuple<DummyChromosome, DummyChromosome>[] { new Tuple<DummyChromosome, DummyChromosome>(new DummyChromosome("a"), new DummyChromosome("b")) }.ToList(), initialPopulation.ToList(), new Random(0), 0);

            trainer.PopulateNextGeneration();

            CollectionAssert.AreEquivalent(new DummyChromosome[] { new DummyChromosome("ab") },  trainer.Population);
            Assert.IsTrue(trainer.Population[0].MutateCalled);
        }

    }
}
