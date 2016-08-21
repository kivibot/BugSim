using BugSim.Genetic;
using BugSim.Neural;
using BugSim.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugSim.UI
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private Texture2D _filledCircleTexture;
        private Texture2D _whiteTexture;
        private float _sizeRatio = 100.0f;

        private BugSimulation _simulation;
        private List<DoubleChromosome> _chromosomes = new List<DoubleChromosome>();
        private List<Bug> _bugs = new List<Bug>();

        private int _currentStep = 0;
        private int _maxSteps = 10000;
        private int _currentGen = 0;

        private Form1 _form = new Form1();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            for (int i = 0; i < 100; i++)
                _chromosomes.Add(DoubleChromosome.Random(1282, _random));

            _form.Show();

            StartNewSim();


            base.Initialize();
        }

        private void StartNewSim()
        {
            _currentGen++;
            Console.WriteLine("Generation: " + _currentGen);
            _bugs.Clear();
            _currentStep = 0;
            for (int i = 0; i < _chromosomes.Count; i++)
                _bugs.Add(BugFromChromosome(_random.NextDouble() * 12.8, _random.NextDouble() * 7.2, _chromosomes[i]));


            _simulation = new BugSimulation(_bugs, 10, 3, 2, _random);
        }

        private Bug BugFromChromosome(double x, double y, DoubleChromosome chromosome)
        {
            int index = 0;
            Network nn = new Network(LayerFromArray(chromosome.Data, ref index, 13, 26, new TanhActivationFunction()), 
                LayerFromArray(chromosome.Data, ref index, 26, 26, new TanhActivationFunction()), 
                LayerFromArray(chromosome.Data, ref index, 26, 8, new ClampingActivationFunction()));
            return new Bug() { X = x, Y = y, Radius = 0.05, Network = nn, Sensors = CreateSensors() };
        }

        private List<Sensor> CreateSensors()
        {
            List<Sensor> sensors = new List<Sensor>();
            sensors.Add(new Sensor(-Math.PI / 4, 1, 1, 0, 0, 0));
            sensors.Add(new Sensor(-Math.PI / 8, 1, 1, 0, 0, 0));
            sensors.Add(new Sensor(0, 1, 1, 0, 0, 0));
            sensors.Add(new Sensor(0, 1, 0, 1, 0, 0));
            sensors.Add(new Sensor(0, 1, 0, 0, 1, 0));
            sensors.Add(new Sensor(0, 1, 0, 0, 0, 1));
            sensors.Add(new Sensor(Math.PI / 8, 1, 1, 0, 0, 0));
            sensors.Add(new Sensor(Math.PI / 4, 1, 1, 0, 0, 0));

            return sensors;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _filledCircleTexture = Content.Load<Texture2D>("filled-circle");
            _whiteTexture = Content.Load<Texture2D>("white");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            _form.Hide();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < _form.Speed; i++)
            {
                if (_currentStep == _maxSteps || _simulation.GetBugs().Count() < 5)
                {

                    for (int j = 0; j < _chromosomes.Count; j++)
                    {
                        _chromosomes[j].Fitness = _bugs[j].Score;
                    }

                    GeneticAlgorithm<DoubleChromosome> ga = new GeneticAlgorithm<DoubleChromosome>(new DummyFitnessFunction<DoubleChromosome>(), new FitnessSurvivorSelector(15), new FitnessWeightedParentSelector(85, _random), 0.0125, _random, _chromosomes);


                    ga.RunOneGeneration();

                    _chromosomes = ga.Chromosomes;

                    StartNewSim();
                }

                _form.Step = _currentStep;
                _form.Gen = _currentGen;
                _form.MaxSteps = _maxSteps;
                _form.Scores = _bugs.Select(b => b.Score);

                // Console.WriteLine(String.Format("Current Step {0}/{1} {2}%", _currentStep, _maxSteps, 100.0 * _currentStep / _maxSteps));

                _simulation.SimulateStep(1.0 / 30.0);
                _currentStep++;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (Food food in _simulation.GetFoods())
                DrawFood(food);
            foreach (Bug bug in _bugs)
            {
                if (bug.Health > 0)
                    DrawBug(bug);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawCircle(double x, double y, double radius, Color color)
        {
            _spriteBatch.Draw(_filledCircleTexture, new Rectangle((int)(x - radius), (int)(y - radius), (int)(radius * 2f), (int)(radius * 2f)), color);
        }

        private void DrawBug(Bug bug)
        {
            double x = _sizeRatio * bug.X;
            double y = _sizeRatio * bug.Y;
            double radius = _sizeRatio * bug.Radius;
            DrawCircle(x, y, radius, new Color((float)bug.Red, (float)bug.Green, (float)bug.Blue));
            DrawCircle(x, y, radius - 2, new Color(1f - (float)bug.Energy, (float)bug.Health, 0f));
            //foreach (Sensor sensor in bug.Sensors)
            //    DrawSensor(sensor, bug);
        }

        private void DrawLine(double x0, double y0, double x1, double y1, int width, Color color)
        {
            double xd = x0 - x1;
            double yd = y0 - y1;
            double d = Math.Sqrt(xd * xd + yd * yd);
            double rotation = Math.Atan2(yd, xd);
            _spriteBatch.Draw(_whiteTexture, new Rectangle((int)Math.Min(x0, x1), (int)Math.Min(y0, y1), (int)d, width), null, color, (float)rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

        private void DrawLineFrom(double x, double y, double rotation, double len, int width, Color color)
        {
            _spriteBatch.Draw(_whiteTexture, new Rectangle((int)x, (int)y, (int)len, width), null, color, (float)rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

        private Random _random = new Random(1);

        public Module CreateRandomModule(int inputs, IActivationFunction func)
        {
            double[] w = new double[inputs];
            for (int i = 0; i < inputs; i++)
            { w[i] = (_random.NextDouble() - 0.5) * 4; };
            return new Module(w, (_random.NextDouble() - 0.5) * 4, func);
        }

        public Layer CreateRandomLayer(int inputs, int outputs, IActivationFunction func)
        {
            List<Module> modules = new List<Module>();
            for (int i = 0; i < outputs; i++)
                modules.Add(CreateRandomModule(inputs, func));
            return new Layer(modules);
        }

        private void DrawSensor(Sensor sensor, Bug bug)
        {
            DrawLineFrom(bug.X * _sizeRatio, bug.Y * _sizeRatio, bug.Rotation + sensor.Rotation, _sizeRatio * sensor.Length, 1, Color.Red);
        }

        private void DrawFood(Food food)
        {
            double x = _sizeRatio * food.X;
            double y = _sizeRatio * food.Y;
            double radius = _sizeRatio * food.Radius;
            DrawCircle(x, y, radius, new Color((float)food.R, (float)food.G, (float)food.B));
        }

        public Layer LayerFromArray(double[] weights, ref int pos, int inputs, int outputs, IActivationFunction func)
        {
            List<Module> modules = new List<Module>();
            for (int i = 0; i < outputs; i++)
                modules.Add(ModuleFromArray(weights, ref pos, inputs, func));
            return new Layer(modules);
        }

        public Module ModuleFromArray(double[] weights, ref int pos, int inputs, IActivationFunction func)
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
                    d[i] = (random.NextDouble() - 0.5);
                }
                return new DoubleChromosome(d);
            }
        }
    }
}
