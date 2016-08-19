using BugSim.Neural;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public class BugSimulation
    {
        private class SimBug
        {
            public Bug Bug { get; set; }
            public Fixture Fixture { get; set; }

            public SimBug(Bug bug)
            {
                this.Bug = bug;
            }
        }

        private class SimFood
        {
            public Food Food { get; set; }
            public Fixture Fixture { get; set; }

            public SimFood(Food food)
            {
                this.Food = food;
            }
        }

        private World _world;

        private double _forwardFactor;
        private double _angularFactor;

        private double _foodSpawnSpeed;

        private double _foodCounter;

        private List<SimBug> _bugs = new List<SimBug>();
        private List<SimFood> _foods = new List<SimFood>();

        private Random _random;

        public BugSimulation(List<Bug> bugs, double forwardFactor, double angularFactor, double foodSpawnSpeed, Random random)
        {
            _forwardFactor = forwardFactor;
            _angularFactor = angularFactor;
            _foodSpawnSpeed = foodSpawnSpeed;
            _random = random;

            _world = new World(Vector2.Zero);
            foreach (Bug bug in bugs)
            {
                Body body = BodyFactory.CreateBody(_world);
                body.BodyType = BodyType.Dynamic;
                body.Position = new Vector2((float)bug.X, (float)bug.Y);
                body.FixedRotation = true;
                SimBug simBug = new SimBug(bug);
                Fixture fixture = FixtureFactory.AttachCircle((float)bug.Radius, 1.0f, body, simBug);
                simBug.Fixture = fixture;
                fixture.OnCollision += HandleCollision;
                _bugs.Add(simBug);
            }
        }

        private void SpawnFood()
        {
            double x = _random.NextDouble() * 12.8;
            double y = _random.NextDouble() * 7.2;
            Food food = new Food(x, y, 0.05);
            SimFood simFood = new SimFood(food);

            Body body = BodyFactory.CreateBody(_world);
            body.Position = new Vector2((float)food.X, (float)food.Y);
            Fixture fixture = FixtureFactory.AttachCircle((float)food.Radius, 1.0f, body, simFood);

            simFood.Fixture = fixture;

            _foods.Add(simFood);
        }

        private bool HandleCollision(Fixture fix1, Fixture fix2, Contact contact)
        {
            SimFood food;
            SimBug bug;
            if (fix1.UserData is SimFood && fix2.UserData is SimBug)
            {
                food = (SimFood)fix1.UserData;
                bug = (SimBug)fix2.UserData;
            }
            else if (fix2.UserData is SimFood && fix1.UserData is SimBug)
            {
                food = (SimFood)fix2.UserData;
                bug = (SimBug)fix1.UserData;
            }
            else
            {
                return true;
            }
            EatFood(bug, food);
            return false;
        }

        private void EatFood(SimBug bug, SimFood food)
        {
            bug.Bug.Score += 1;
            food.Fixture.Body.Dispose();
            _foods.Remove(food);
        }

        public void SimulateStep(double step)
        {
            _foodCounter += step * _foodSpawnSpeed;
            for (; _foodCounter >= 1; _foodCounter -= 1)
            {
                SpawnFood();
            }
            foreach (SimBug simBug in _bugs)
            {
                Bug bug = simBug.Bug;
                Fixture fixture = simBug.Fixture;
                bug.Rotation += (float)(step * _angularFactor * bug.AngularPower);

                double[] input = new double[bug.Sensors.Count + 1];
                for (int i = 0; i < bug.Sensors.Count; i++)
                {
                    Sensor sensor = bug.Sensors[i];

                    Vector2 toPoint = new Vector2((float)(bug.X + Math.Cos(bug.Rotation + sensor.Rotation) * sensor.MaxLength), (float)(bug.Y + Math.Sin(bug.Rotation + sensor.Rotation) * sensor.MaxLength));

                    double minFraction = 1;
                    Fixture minFix = null;

                    _world.RayCast((fix, point, normal, fraction) =>
                    {
                        if (fixture == fix)
                            return -1;
                        minFraction = fraction;
                        minFix = fix;
                        return fraction;
                    }, new Vector2((float)bug.X, (float)bug.Y), toPoint);

                    Vector3 vec = new Vector3(0, 0, 0);

                    if (minFix != null)
                    {
                        if (minFix.UserData is SimBug)
                        {
                            var trg = minFix.UserData as SimBug;
                            vec = new Vector3((float)trg.Bug.Red, (float)trg.Bug.Green, (float)trg.Bug.Blue);
                        }
                        else if (minFix.UserData is SimFood)
                        {
                            var trg = minFix.UserData as SimFood;
                            vec = new Vector3((float)trg.Food.R, (float)trg.Food.G, (float)trg.Food.B);
                        }
                    }

                    sensor.Length = minFraction * sensor.MaxLength * sensor.A
                        + minFraction * sensor.MaxLength * sensor.R * vec.X
                        + minFraction * sensor.MaxLength * sensor.G * vec.Y
                        + minFraction * sensor.MaxLength * sensor.B * vec.Z;


                    input[i] = sensor.Length;
                }

                input[input.Length - 1] = bug.Memory0;

                double[] output = bug.Network.Activate(input);
                bug.ForwardPower = Math.Max(0, output[0]);
                bug.AngularPower = output[1];
                bug.Memory0 = output[2];

                bug.Red = output[3] / 2.0 + 0.5;
                bug.Green = output[4] / 2.0 + 0.5;
                bug.Blue = output[5] / 2.0 + 0.5;

                fixture.Body.Rotation = (float)bug.Rotation;

                fixture.Body.LinearVelocity = new Vector2((float)(Math.Cos(fixture.Body.Rotation) * step * _forwardFactor * bug.ForwardPower), (float)(Math.Sin(fixture.Body.Rotation) * _forwardFactor * step * bug.ForwardPower));
            }
            _world.Step((float)step);
            foreach (SimBug simBug in _bugs)
            {
                Bug bug = simBug.Bug;
                Fixture fixture = simBug.Fixture;
                bug.Rotation = fixture.Body.Rotation;
                bug.X = fixture.Body.Position.X;
                bug.Y = fixture.Body.Position.Y;
                bug.Radius = 0.05f + Math.Max(0, Math.Log10(bug.Score)) * 0.01;
            }
        }

        public IEnumerable<Bug> GetBugs()
        {
            return _bugs.Select(sb => sb.Bug);
        }

        public IEnumerable<Food> GetFoods()
        {
            return _foods.Select(sf => sf.Food);
        }

    }
}
