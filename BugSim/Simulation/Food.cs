using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public class Food : WorldObject, IColored
    {
        public double Radius { get; private set; }

        public double Red { get; private set; }
        public double Green { get; private set; }
        public double Blue { get; private set; }

        public Body Body { get; set; }

        public Food(Body body, double radius)
            : base(body)
        {
            this.Radius = radius;
            this.Red = 0;
            this.Green = 1;
            this.Blue = 0;
        }

    }
}
