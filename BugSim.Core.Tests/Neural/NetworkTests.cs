using BugSim.Core.Neural;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugSim.Core.Tests.Neural
{
    [TestFixture]
    public class NetworkTests
    {
        [Test]
        public void TestActivate()
        {
            Layer layer1 = new Layer(new Module(new double[] { 2 }, 3, (v) => v));
            Layer layer2 = new Layer(new Module(new double[] { 6 }, 4, (v) => v));
            Network network = new Network(layer1, layer2);

            double[] results = network.Activate(new double[] { 1 });

            CollectionAssert.AreEqual(new double[] { 34 }, results);
        }
    }
}
