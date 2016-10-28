using BugSim.Core.Neural;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugSim.Core.Tests.Neural
{
    [TestFixture]
    public class ModuleTests
    {

        [Test]
        public void TestActivate()
        {
            Module module = new Module(new double[] { 0, 2, 1 }, 123.54, (v) => Math.Pow(v, 2));
            double expected = Math.Pow(4 + 123.54, 2);

            double output = module.Activate(new double[] { 3, 1, 2 });

            Assert.AreEqual(expected, output);
        }

    }
}
