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
        private World _world;

        private double _forwardFactor;
        private double _angularFactor;

        private double _foodSpawnSpeed;

        private double _foodCounter;

        private double _drainRate = 0.02;
        private double _damageRate = 0.03;
        private double _energyPerFoor = 0.2;

        private double _initialHealth = 1;
        private double _initialEnergy = 0.3;

        private List<Bug> _bugs = new List<Bug>();
        private List<Food> _foods = new List<Food>();

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
                SpawnFood();

                bug.Health = _initialHealth;
                bug.Energy = _initialEnergy;

                Body body = BodyFactory.CreateBody(_world, new Vector2((float)bug.X, (float)bug.Y), 0, bug);
                body.BodyType = BodyType.Dynamic;
                body.FixedRotation = true;
                Fixture fixture = FixtureFactory.AttachCircle((float)bug.Radius, 1.0f, body, bug);

                bug.Body = body;

                _bugs.Add(bug);
            }
        }

        private void SpawnFood()
        {

        }

        private void EatFood(SimBug bug, SimFood food)
        {
            bug.Bug.Score += 1;
            bug.Bug.Energy = Math.Min(1, bug.Bug.Energy + _energyPerFoor);
            food.Fixture.Body.Dispose();
            _foods.Remove(food);
        }

        public void SimulateStep(double step)
        {
            _bugs.RemoveAll(bug => bug.Simulate(_world));

            _world.Step((float)step);
        }

    }
}
