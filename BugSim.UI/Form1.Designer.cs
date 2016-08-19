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
            this.lblGen = new System.Windows.Forms.Label();
            this.pbSteps = new System.Windows.Forms.ProgressBar();
            this.tbSpeed = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).BeginInit();
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
            this.pbSteps.Location = new System.Drawing.Point(12, 29);
            this.pbSteps.Name = "pbSteps";
            this.pbSteps.Size = new System.Drawing.Size(260, 23);
            this.pbSteps.TabIndex = 2;
            // 
            // tbSpeed
            // 
            this.tbSpeed.Location = new System.Drawing.Point(13, 59);
            this.tbSpeed.Minimum = 1;
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(259, 45);
            this.tbSpeed.TabIndex = 3;
            this.tbSpeed.Value = 1;
            this.tbSpeed.ValueChanged += new System.EventHandler(this.tbSpeed_ValueChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tbSpeed);
            this.Controls.Add(this.pbSteps);
            this.Controls.Add(this.lblGen);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblGen;
        private System.Windows.Forms.ProgressBar pbSteps;
        private System.Windows.Forms.TrackBar tbSpeed;
        private System.Windows.Forms.Timer timer1;
    }
}