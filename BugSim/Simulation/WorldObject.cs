using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public class WorldObject : IDisposable
    {
        public Vector2 Position { get { return _body.Position; } }
        public double Rotation { get { return _body.Rotation; } }

        protected Body _body;

        public WorldObject(Body body)
        {
            _body = body;
        }

        public virtual void Dispose()
        {
            _body.Dispose();
        }
    }
}
