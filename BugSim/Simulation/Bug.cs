using BugSim.Neural;
using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugSim.Simulation
{
    public class Bug : IColored
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Rotation { get; set; }
        public double Radius { get; set; }

        public double ForwardPower { get; set; }
        public double RotationPower { get; set; }

        public List<ISensor> Sensors { get; set; }

        public Network Network { get; set; }

        public double Score { get; set; }

        public double[] Memory { get; set; }

        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }

        public double Health { get; set; }
        public double Energy { get; set; }

        public Body Body { get; set; }


        public bool Simulate(World world)
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

            return false;
        }


    }
}
