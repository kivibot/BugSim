using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace BugSim.Simulation.Sensors
{
    public class RaycastSensor : ISensor
    {
        public int Outputs { get; private set; }

        private double _rotation;
        private double _maxLength;
        private bool _colors;

        public RaycastSensor(double rotation, double maxLen, bool colors)
        {
            _rotation = rotation;
            _maxLength = maxLen;
            _colors = colors;

            Outputs = colors ? 4 : 1;
        }

        public double[] GetValues(World world, Bug bug)
        {
            Vector2 startPoint = new Vector2((float)bug.X, (float)bug.Y);
            Vector2 endPoint = new Vector2((float)(bug.X + Math.Cos(bug.Rotation + _rotation) * _maxLength),
                (float)(bug.Y + Math.Sin(bug.Rotation + _rotation) * _maxLength));

            double minFraction = 1;
            Fixture targetFixture = null;

            world.RayCast((fixture, point, normal, fraction) =>
            {
                targetFixture = fixture;
                minFraction = fraction;
                return fraction;
            }, startPoint, endPoint);

            double[] values = new double[Outputs];

            values[0] = 1.0 - minFraction;

            if (_colors && targetFixture != null && targetFixture.UserData is IColored)
            {
                IColored colored = (IColored)targetFixture.UserData;
                values[1] = colored.Red;
                values[2] = colored.Green;
                values[3] = colored.Blue;
            }

            return values;
        }

    }
}
