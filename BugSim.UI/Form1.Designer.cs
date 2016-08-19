namespace BugSim.UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblGen = new System.Windows.Forms.Label();
            this.pbSteps = new System.Windows.Forms.ProgressBar();
            this.tbSpeed = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.fitnessChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitnessChart)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGen
            // 
            this.lblGen.AutoSize = true;
            this.lblGen.Location = new System.Drawing.Point(13, 13);
            this.lblGen.Name = "lblGen";
            this.lblGen.Size = new System.Drawing.Size(35, 13);
            this.lblGen.TabIndex = 0;
            this.lblGen.Text = "label1";
            // 
            // pbSteps
            // 
            this.pbSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSteps.Location = new System.Drawing.Point(12, 29);
            this.pbSteps.Name = "pbSteps";
            this.pbSteps.Size = new System.Drawing.Size(367, 23);
            this.pbSteps.TabIndex = 2;
            // 
            // tbSpeed
            // 
            this.tbSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSpeed.Location = new System.Drawing.Point(13, 59);
            this.tbSpeed.Minimum = 1;
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(366, 45);
            this.tbSpeed.TabIndex = 3;
            this.tbSpeed.Value = 1;
            this.tbSpeed.ValueChanged += new System.EventHandler(this.tbSpeed_ValueChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // fitnessChart
            // 
            this.fitnessChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.fitnessChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.fitnessChart.Legends.Add(legend2);
            this.fitnessChart.Location = new System.Drawing.Point(12, 97);
            this.fitnessChart.Name = "fitnessChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Fitness";
            this.fitnessChart.Series.Add(series2);
            this.fitnessChart.Size = new System.Drawing.Size(367, 299);
            this.fitnessChart.TabIndex = 4;
            this.fitnessChart.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 408);
            this.Controls.Add(this.fitnessChart);
            this.Controls.Add(this.tbSpeed);
            this.Controls.Add(this.pbSteps);
            this.Controls.Add(this.lblGen);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitnessChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblGen;
        private System.Windows.Forms.ProgressBar pbSteps;
        private System.Windows.Forms.TrackBar tbSpeed;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart fitnessChart;
    }
}