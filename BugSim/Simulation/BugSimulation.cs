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

        private double _worldWidth = 12;
        private double _worldHeight = 7;

        private double _foodRadius = 0.05;
        private double _bugRadius = 0.05;

        private double _initialHealth = 1;
        private double _initialEnergy = 0.3;

        private List<Bug> _bugs = new List<Bug>();
        private List<Food> _foods = new List<Food>();


        private double _forwardFactor = 3;
        private double _angularFactor = 3;

        private double _foodSpawnSpeed = 3;
        private double _foodCounter = 0;

        private double _drainRate = 0.02;
        private double _damageRate = 0.03;
        private double _energyPerFoor = 0.2;



        private Random _random;

        public BugSimulation(Random random)
        {
            _random = random;
            _world = new World(Vector2.Zero);
        }

        public void CreateFood()
        {
            double x = _random.NextDouble() * _worldWidth;
            double y = _random.NextDouble() * _worldHeight;
            Body body = BodyFactory.CreateCircle(_world, (float)_foodRadius, 1, new Vector2((float)x, (float)y));
            body.BodyType = BodyType.Static;
            Food food = new Food(body, _foodRadius);
            body.UserData = food;
            _foods.Add(food);
        }


        public void CreateBug(List<ISensor> sensors, Network network)
        {
            double x = _random.NextDouble() * _worldWidth;
            double y = _random.NextDouble() * _worldHeight;
            Body body = BodyFactory.CreateCircle(_world, (float)_bugRadius, 1, new Vector2((float)x, (float)y));
            body.BodyType = BodyType.Dynamic;
            body.FixedRotation = true;
            Bug bug = new Bug(sensors, network, body, _bugRadius, _initialHealth, _initialEnergy, _forwardFactor, _angularFactor);
            _bugs.Add(bug);
        }

        public void SimulateStep(double step)
        {
            _foodCounter += _foodSpawnSpeed;
            for (; _foodCounter >= 0; _foodCounter -= 1)
                CreateFood();


            _bugs.RemoveAll(bug => bug.Simulate(_world, step));

            _world.Step((float)step);
        }

        public IEnumerable<Bug> Bugs { get { return _bugs; } }
        public IEnumerable<Food> Foods { get { return _foods; } }

    }
}
