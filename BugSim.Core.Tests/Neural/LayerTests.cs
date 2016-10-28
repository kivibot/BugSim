using BugSim.Core.Neural;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugSim.Core.Tests.Neural
{
    [TestFixture]
    public class LayerTests
    {

        [Test]
        public void TestActivate()
        {
            Module m1 = new Module(new double[] { 1.1 }, 3, (v) => v);
            Module m2 = new Module(new double[] { 1 }, 0, (v) => v);
            Layer layer = new Layer(m1, m2);

            double[] result = layer.Activate(new double[] { 2 });

            CollectionAssert.AreEqual(new double[] { 5.2, 2 }, result);
        }

    }
}
