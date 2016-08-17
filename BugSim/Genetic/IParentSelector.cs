using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Genetic
{

    public class Parents<T>
    {
        public T ParentA { get; set; }
        public T ParentB { get; set; }

        public Parents(T a, T b)
        {
            ParentA = a;
            ParentB = b;
        }
    }

    public interface IParentSelector
    {
        List<Parents<T>> Select<T>(List<T> candidates) where T : IChromosome;
    }
}
