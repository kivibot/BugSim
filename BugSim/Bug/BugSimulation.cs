using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Bug
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

        private World _world;

        private double _forwardFactor;
        private double _angularFactor;

        private List<SimBug> _bugs = new List<SimBug>();

        public BugSimulation(List<Bug> bugs, double forwardFactor, double angularFactor)
        {
            _forwardFactor = forwardFactor;
            _angularFactor = angularFactor;

            _world = new World(Vector2.Zero);
            foreach (Bug bug in bugs)
            {
                Body body = BodyFactory.CreateBody(_world);
                body.BodyType = BodyType.Dynamic;
                SimBug simBug = new SimBug(bug);
                Fixture fixture = FixtureFactory.AttachCircle(1.0f, 1.0f, body, simBug);
                simBug.Fixture = fixture;
            }
        }

        public void SimulateStep(double step)
        {
            foreach (SimBug simBug in _bugs)
            {
                Bug bug = simBug.Bug;
                Fixture fixture = simBug.Fixture;

                fixture.Body.Rotation += (float)(_angularFactor * bug.AngularPower);
                fixture.Body.LinearVelocity = new Vector2((float)(Math.Cos(fixture.Body.Rotation) * _forwardFactor * bug.ForwardPower), (float)(Math.Sin(fixture.Body.Rotation) * _forwardFactor * bug.ForwardPower));
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

        private IEnumerable<Bug> GetBugs()
        {
            return _bugs.Select(sb => sb.Bug);
        }

    }
}
