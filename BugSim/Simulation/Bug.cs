using BugSim.Neural;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public class Bug : WorldObject, IColored
    {
        public List<ISensor> Sensors { get; set; }
        public Network Network { get; set; }

        public double Score { get; set; }
        public double[] Memory { get; set; }

        public double Radius { get; private set; }
        public double Health { get; set; }
        public double Energy { get; set; }

        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }

        public double ForwardPower { get; set; }
        public double RotationPower { get; set; }

        public double MaxForwardSpeed { get; set; }
        public double MaxRotationSpeed { get; set; }

        public Bug(List<ISensor> sensors, Network network, Body body, double radius, double health, double energy, double forwardSpeed, double rotationSpeed)
            : base(body)
        {
            this.Sensors = sensors;
            this.Network = network;
            this.Radius = radius;
            this.Health = health;
            this.Energy = energy;
            this.MaxForwardSpeed = forwardSpeed;
            this.MaxRotationSpeed = rotationSpeed;
            this.Memory = new double[3];
        }


        public bool Simulate(World world, double step)
        {
            double[] inputs = Sensors.SelectMany(sensor => sensor.GetValues(world, this)).ToArray();
            double[] outputs = Network.Activate(inputs);
            int i = 0;

            ForwardPower = outputs[i++];
            RotationPower = outputs[i++];

            Red = outputs[i++];
            Green = outputs[i++];
            Blue = outputs[i++];

            for (int j = 0; j < Memory.Length; j++)
                Memory[j] = outputs[i++];


            _body.Rotation += (float)(step * RotationPower * MaxRotationSpeed);
            _body.LinearVelocity = new Vector2((float)(Math.Cos(_body.Rotation) * step * MaxForwardSpeed * ForwardPower), (float)(Math.Sin(_body.Rotation) * MaxForwardSpeed * step * ForwardPower));

            return false;
        }


    }
}
