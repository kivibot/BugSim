using BugSim.Neural;
using FarseerPhysics.Dynamics;
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
            _world.ContactManager.OnBroadphaseCollision += HandleCollision;
            foreach (Bug bug in bugs)
            {
                Body body = BodyFactory.CreateBody(_world);
                body.BodyType = BodyType.Dynamic;
                body.Position = new Vector2((float)bug.X, (float)bug.Y);
                body.FixedRotation = true;
                SimBug simBug = new SimBug(bug);
                Fixture fixture = FixtureFactory.AttachCircle((float)bug.Radius, 1.0f, body, simBug);
                simBug.Fixture = fixture;
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

        private void HandleCollision(ref FixtureProxy fp1, ref FixtureProxy fp2)
        {
            SimFood food;
            SimBug bug;
            if (fp1.Fixture.UserData is SimFood && fp2.Fixture.UserData is SimBug)
            {
                food = (SimFood)fp1.Fixture.UserData;
                bug = (SimBug)fp2.Fixture.UserData;
            }
            else if (fp2.Fixture.UserData is SimFood && fp1.Fixture.UserData is SimBug)
            {
                food = (SimFood)fp2.Fixture.UserData;
                bug = (SimBug)fp1.Fixture.UserData;
            }
            else
            {
                return;
            }
            EatFood(bug, food);
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

                    _world.RayCast((fix, point, normal, fraction) =>
                    {
                        if (fixture == fix)
                            return -1;
                        minFraction = fraction;
                        return fraction;
                    }, new Vector2((float)bug.X, (float)bug.Y), toPoint);

                    sensor.Length = minFraction * sensor.MaxLength;

                    input[i] = sensor.Length;
                }

                input[input.Length - 1] = bug.Memory0;

                double[] output = bug.Network.Activate(input);
                bug.ForwardPower = Math.Max(0, output[0]);
                bug.AngularPower = output[1];
                bug.Memory0 = output[2];

                bug.Red = output[3];
                bug.Green = output[4];
                bug.Blue = output[5];

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
