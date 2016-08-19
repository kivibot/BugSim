using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugSim.UI
{
    public partial class Form1 : Form
    {
        public int Speed { get; private set; }
        public int Gen { get; set; }
        public int Step { get; set; }
        public int MaxSteps { get; set; }

        public Form1()
        {
            InitializeComponent();
            Speed = tbSpeed.Value;
            pbSteps.Minimum = 0;
        }

        private void tbSpeed_ValueChanged(object sender, EventArgs e)
        {
            Speed = tbSpeed.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblGen.Text = "Generation: " + Gen;
            pbSteps.Maximum = MaxSteps;
            pbSteps.Value = Step;
        }
    }
}
