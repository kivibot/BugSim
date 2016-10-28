using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugSim.Core.Neural
{
    public class DefaultActivationFunctions
    {
        public static readonly Func<double, double> Tanh = Math.Tanh;
        public static readonly Func<double, double> Linear = (value) => value;
        public static readonly Func<double, double> Clamping = (value) => Math.Min(1, Math.Max(-1, value));
        public static readonly Func<double, double> Sigmoid = (value) => 1.0 / (1.0 + Math.Exp(-value));
    }
}
