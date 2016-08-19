using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BugSim.UI
{
    public partial class Form1 : Form
    {
        public int Speed { get; private set; }
        public int Gen { get; set; }
        public int Step { get; set; }
        public int MaxSteps { get; set; }

        public IEnumerable<double> Scores { get; set; }

        public Form1()
        {
            InitializeComponent();
            Speed = tbSpeed.Value;
            pbSteps.Minimum = 0;
            Scores = new List<double>();
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
            var series = fitnessChart.Series[0];
            series.Points.Clear();
            double max = Scores.Any() ? Scores.Max() : 0;
            if (max == 0)
                max = 1;
            foreach (var grp in Scores.Select(s => Math.Floor(max * Math.Floor(s / max * 15.0) / 15.0)).GroupBy(s => s))
            {
                series.Points.Add(new DataPoint(grp.Key, grp.Count()));
            }
        }
    }
}
