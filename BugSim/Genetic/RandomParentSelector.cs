using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Genetic
{
    public class RandomParentSelector : IParentSelector
    {
        private int _children;
        private Random _random;

        public RandomParentSelector(int children, Random random)
        {
            _children = children;
            _random = random;
        }

        public List<Parents<T>> Select<T>(List<T> candidates) where T : IChromosome
        {
            List<Parents<T>> allParents = new List<Parents<T>>();
            while (allParents.Count < _children)
            {
                T parentA = candidates[_random.Next(candidates.Count)];
                T parentB = candidates[_random.Next(candidates.Count)];

                if (Object.Equals(parentA, parentB))
                    continue;

                allParents.Add(new Parents<T>(parentA, parentB));
            }
            return allParents;
        }
    }
}
