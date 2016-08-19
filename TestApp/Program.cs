using BugSim.Genetic;
using BugSim.Neural;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        private class DoubleChromosome : IChromosome
        {
            public double Fitness
            {
                get; set;
            }

            public double[] Data { get; private set; }

            public DoubleChromosome(double[] data)
            {
                Data = data;
            }

            public void Mutate(double probability, Random random)
            {
                for (int i = 0; i < Data.Length; i++)
                {
                    if (random.NextDouble() < probability)
                    {
                        int oper = random.Next() % 3;
                        if (oper == 0)
                        {
                            Data[i] += Math.Min(3, Math.Max(-3, (random.NextDouble() - 0.5) / 2));
                        }
                        else if (oper == 1)
                        {
                            int a = random.Next(Data.Length);
                            int b = random.Next(Data.Length);
                            double t = Data[a];
                            Data[a] = Data[b];
                            Data[b] = t;
                        }
                        else
                        {
                            Data[i] += random.NextDouble() * 6 - 3;
                        }
                    }
                }
            }

            public IChromosome Crossover(IChromosome parentB, Random random)
            {
                double[] child = new double[Data.Length];
                DoubleChromosome b = parentB as DoubleChromosome;
                for (int i = 0; i < Data.Length; i++)
                {
                    child[i] = random.Next() % 2 == 0 ? Data[i] : b.Data[i];
                }
                return new DoubleChromosome(child);
            }

            public static DoubleChromosome Random(int len, Random random)
            {
                double[] d = new double[len];
                for (int i = 0; i < len; i++)
                {
                    d[i] = (random.NextDouble() - 0.5) / 100.0;
                }
                return new DoubleChromosome(d);
            }
        }

        private class TestFitnessFunction : IFitnessFunction<DoubleChromosome>
        {
            public double Test(DoubleChromosome chromosome)
            {
                return -RunTest(chromosome.Data);
            }
        }


        private static bool _print;

        static void Main(string[] args)
        {
            //double best = double.MaxValue;
            //while (true)
            //{
            //    double e = RunTest();
            //    Console.WriteLine("error: " + e);
            //    if (e < best)
            //    {
            //        Debugger.Break();
            //        best = e;
            //    }
            //}

            List<DoubleChromosome> chromosomes = new List<DoubleChromosome>();

            //for (int i = 0; i < 1000; i++)
            //{
            //    chromosomes.Add(DoubleChromosome.Random(21, _random));
            //}

            //GeneticAlgorithm<DoubleChromosome> ga = new GeneticAlgorithm<DoubleChromosome>(new TestFitnessFunction(), new FitnessSurvivorSelector(1000), new RandomParentSelector(2000, _random), 0.0025, _random, chromosomes);

            //ga.RunFitness();

            //while (true)
            //{
            //    var generation = ga.Chromosomes.OrderBy(a => -a.Fitness).Select(a => a.Fitness).ToList();

            //    double med = generation.Skip(generation.Count / 2).First();

            //    Console.WriteLine(generation[0] + " " + med);

            //    if (generation[0] > -0.0000005)
            //    {
            //        Debugger.Break();
            //        _print = true;
            //    }

            //    ga.RunOneGeneration();

            //}

        }

        private static Random _random = new Random(123);

        public static double Test(Network nn, double[] input, double[] expected)
        {
            double[] output = nn.Activate(input);


            double error = 0;
            for (int i = 0; i < expected.Length; i++)
                error += Math.Abs(output[i] - expected[i]);
            error /= output.Length;

            if (_print)
            {
                Console.Write("Input: " + GetString(input));
                Console.Write(" Output: " + GetString(output));
                Console.Write(" Expected: " + GetString(expected));
                Console.Write(" Error: " + error);
                Console.WriteLine();
            }

            return error;

        }

        public static double RunTest(double[] weights)
        {
            int pos = 0;
            Layer l1 = LayerFromArray(weights, ref pos, 2, 5, new TanhActivationFunction());
            Layer l3 = LayerFromArray(weights, ref pos, 5, 1, new LinearActivationFunction());


            Network nn = new Network(l1, l3);

            double error = 0;
            int asd = 0;

            for (double d = -1; d <= 1; d += 0.1)
            {
                error += TestSin(nn, d + (_random.NextDouble() - 0.5) / 10);
                asd++;
            }

            if (_print)
            {
                for (double d = 0; d < Math.PI * 2; d += 0.1)
                    PrintSin(d, nn);
            }

            return error / asd;
        }

        public static double TestSin(Network nn, double val)
        {
            double expected = val * val; ;
            double[] output = nn.Activate(new double[] { val, val });
            return Math.Abs(expected - output[0]);
        }

        public static void PrintSin(double val, Network nn)
        {
            double expected = Math.Sin(val);
            double[] output = nn.Activate(new double[] { val, val });
            Console.WriteLine(Math.Round(val, 3) + " " + expected + " " + output[0]);
        }

        public static Layer LayerFromArray(double[] weights, ref int pos, int inputs, int outputs, IActivationFunction func)
        {
            List<Module> modules = new List<Module>();
            for (int i = 0; i < outputs; i++)
                modules.Add(ModuleFromArray(weights, ref pos, inputs, func));
            return new Layer(modules);
        }

        public static Module ModuleFromArray(double[] weights, ref int pos, int inputs, IActivationFunction func)
        {
            double[] w = new double[inputs];
            for (int i = 0; i < inputs; i++)
            {
                w[i] = weights[pos];
                pos++;
            }
            double bias = weights[pos];
            pos++;
            return new Module(w, bias, func);
        }

        public static Module CreateRandomModule(int inputs, IActivationFunction func)
        {
            double[] w = new double[inputs];
            for (int i = 0; i < inputs; i++)
            { w[i] = (_random.NextDouble() - 0.5) * 4; };
            return new Module(w, (_random.NextDouble() - 0.5) * 4, func);
        }

        public static Layer CreateRandomLayer(int inputs, int outputs, IActivationFunction func)
        {
            List<Module> modules = new List<Module>();
            for (int i = 0; i < outputs; i++)
                modules.Add(CreateRandomModule(inputs, func));
            return new Layer(modules);
        }

        public static string GetString(double[] values)
        {

            return "[" + String.Join(", ", values) + "]";
        }
    }
}
