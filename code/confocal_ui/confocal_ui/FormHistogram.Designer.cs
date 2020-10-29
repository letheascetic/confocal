namespace confocal_ui
{
    partial class FormHistogram
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
            this.pnlGamma = new System.Windows.Forms.Panel();
            this.pnlHistogram = new System.Windows.Forms.Panel();
            this.lbGamma = new System.Windows.Forms.Label();
            this.gtbGamma = new gTrackBar.gTrackBar();
            this.histogramBox = new Emgu.CV.UI.HistogramBox();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.pnlGamma.SuspendLayout();
            this.pnlHistogram.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGamma
            // 
            this.pnlGamma.Controls.Add(this.gtbGamma);
            this.pnlGamma.Controls.Add(this.lbGamma);
            this.pnlGamma.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGamma.Location = new System.Drawing.Point(0, 253);
            this.pnlGamma.Name = "pnlGamma";
            this.pnlGamma.Size = new System.Drawing.Size(378, 36);
            this.pnlGamma.TabIndex = 1;
            // 
            // pnlHistogram
            // 
            this.pnlHistogram.Controls.Add(this.histogramBox);
            this.pnlHistogram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistogram.Location = new System.Drawing.Point(0, 0);
            this.pnlHistogram.Name = "pnlHistogram";
            this.pnlHistogram.Size = new System.Drawing.Size(378, 253);
            this.pnlHistogram.TabIndex = 2;
            // 
            // lbGamma
            // 
            this.lbGamma.AutoSize = true;
            this.lbGamma.Location = new System.Drawing.Point(12, 14);
            this.lbGamma.Name = "lbGamma";
            this.lbGamma.Size = new System.Drawing.Size(59, 13);
            this.lbGamma.TabIndex = 13;
            this.lbGamma.Text = "伽马矫正";
            // 
            // gtbGamma
            // 
            this.gtbGamma.BackColor = System.Drawing.SystemColors.Control;
            this.gtbGamma.Label = null;
            this.gtbGamma.Location = new System.Drawing.Point(76, 3);
            this.gtbGamma.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gtbGamma.MaxValue = 300;
            this.gtbGamma.MinValue = -300;
            this.gtbGamma.Name = "gtbGamma";
            this.gtbGamma.Size = new System.Drawing.Size(253, 30);
            this.gtbGamma.SliderWidthHigh = 1F;
            this.gtbGamma.SliderWidthLow = 1F;
            this.gtbGamma.TabIndex = 12;
            this.gtbGamma.TickThickness = 1F;
            this.gtbGamma.Value = 0;
            this.gtbGamma.ValueAdjusted = 0F;
            this.gtbGamma.ValueBox = gTrackBar.gTrackBar.eValueBox.Right;
            this.gtbGamma.ValueDivisor = gTrackBar.gTrackBar.eValueDivisor.e1;
            this.gtbGamma.ValueStrFormat = null;
            // 
            // histogramBox
            // 
            this.histogramBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.histogramBox.Location = new System.Drawing.Point(0, 0);
            this.histogramBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.histogramBox.Name = "histogramBox";
            this.histogramBox.Size = new System.Drawing.Size(378, 253);
            this.histogramBox.TabIndex = 0;
            // 
            // m_timer
            // 
            this.m_timer.Interval = 1000;
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // FormHistogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 289);
            this.Controls.Add(this.pnlHistogram);
            this.Controls.Add(this.pnlGamma);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HideOnClose = true;
            this.Name = "FormHistogram";
            this.Text = "直方图";
            this.Load += new System.EventHandler(this.FormHistogram_Load);
            this.pnlGamma.ResumeLayout(false);
            this.pnlGamma.PerformLayout();
            this.pnlHistogram.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlGamma;
        private System.Windows.Forms.Panel pnlHistogram;
        private System.Windows.Forms.Label lbGamma;
        private gTrackBar.gTrackBar gtbGamma;
        private Emgu.CV.UI.HistogramBox histogramBox;
        private System.Windows.Forms.Timer m_timer;
    }
}