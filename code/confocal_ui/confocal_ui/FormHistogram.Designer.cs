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
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.cbxChannel = new System.Windows.Forms.ToolStripComboBox();
            this.sp = new System.Windows.Forms.ToolStripSeparator();
            this.pnlHistogram = new System.Windows.Forms.Panel();
            this.histogramBox = new Emgu.CV.UI.HistogramBox();
            this.pnlGamma = new System.Windows.Forms.Panel();
            this.gtbGamma = new gTrackBar.gTrackBar();
            this.lbGamma = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.pnlHistogram.SuspendLayout();
            this.pnlGamma.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_timer
            // 
            this.m_timer.Interval = 1000;
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbxChannel,
            this.sp});
            this.toolStrip.Location = new System.Drawing.Point(0, 293);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(313, 28);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // cbxChannel
            // 
            this.cbxChannel.DropDownHeight = 80;
            this.cbxChannel.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbxChannel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.cbxChannel.IntegralHeight = false;
            this.cbxChannel.Name = "cbxChannel";
            this.cbxChannel.Size = new System.Drawing.Size(75, 28);
            this.cbxChannel.ToolTipText = "通道选择";
            this.cbxChannel.SelectedIndexChanged += new System.EventHandler(this.cbxChannel_SelectedIndexChanged);
            // 
            // sp
            // 
            this.sp.Name = "sp";
            this.sp.Size = new System.Drawing.Size(6, 28);
            // 
            // pnlHistogram
            // 
            this.pnlHistogram.Controls.Add(this.histogramBox);
            this.pnlHistogram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistogram.Location = new System.Drawing.Point(0, 0);
            this.pnlHistogram.Margin = new System.Windows.Forms.Padding(2);
            this.pnlHistogram.Name = "pnlHistogram";
            this.pnlHistogram.Size = new System.Drawing.Size(313, 257);
            this.pnlHistogram.TabIndex = 4;
            // 
            // histogramBox
            // 
            this.histogramBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.histogramBox.Location = new System.Drawing.Point(0, 0);
            this.histogramBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.histogramBox.Name = "histogramBox";
            this.histogramBox.Size = new System.Drawing.Size(313, 257);
            this.histogramBox.TabIndex = 0;
            // 
            // pnlGamma
            // 
            this.pnlGamma.Controls.Add(this.gtbGamma);
            this.pnlGamma.Controls.Add(this.lbGamma);
            this.pnlGamma.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGamma.Location = new System.Drawing.Point(0, 257);
            this.pnlGamma.Margin = new System.Windows.Forms.Padding(2);
            this.pnlGamma.Name = "pnlGamma";
            this.pnlGamma.Size = new System.Drawing.Size(313, 36);
            this.pnlGamma.TabIndex = 3;
            // 
            // gtbGamma
            // 
            this.gtbGamma.BackColor = System.Drawing.SystemColors.Control;
            this.gtbGamma.Label = null;
            this.gtbGamma.Location = new System.Drawing.Point(72, 4);
            this.gtbGamma.Margin = new System.Windows.Forms.Padding(2);
            this.gtbGamma.MaxValue = 300;
            this.gtbGamma.MinValue = -300;
            this.gtbGamma.Name = "gtbGamma";
            this.gtbGamma.Size = new System.Drawing.Size(190, 24);
            this.gtbGamma.SliderWidthHigh = 1F;
            this.gtbGamma.SliderWidthLow = 1F;
            this.gtbGamma.TabIndex = 12;
            this.gtbGamma.TickThickness = 1F;
            this.gtbGamma.Value = 0;
            this.gtbGamma.ValueAdjusted = 0F;
            this.gtbGamma.ValueBox = gTrackBar.gTrackBar.eValueBox.Right;
            this.gtbGamma.ValueDivisor = gTrackBar.gTrackBar.eValueDivisor.e1;
            this.gtbGamma.ValueStrFormat = null;
            this.gtbGamma.ValueChanged += new gTrackBar.gTrackBar.ValueChangedEventHandler(this.gtbGamma_ValueChanged);
            // 
            // lbGamma
            // 
            this.lbGamma.AutoSize = true;
            this.lbGamma.Location = new System.Drawing.Point(9, 11);
            this.lbGamma.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbGamma.Name = "lbGamma";
            this.lbGamma.Size = new System.Drawing.Size(59, 13);
            this.lbGamma.TabIndex = 13;
            this.lbGamma.Text = "伽马矫正";
            // 
            // FormHistogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 321);
            this.Controls.Add(this.pnlHistogram);
            this.Controls.Add(this.pnlGamma);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HideOnClose = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormHistogram";
            this.Text = "直方图";
            this.Load += new System.EventHandler(this.FormHistogram_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.pnlHistogram.ResumeLayout(false);
            this.pnlGamma.ResumeLayout(false);
            this.pnlGamma.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.Panel pnlHistogram;
        private Emgu.CV.UI.HistogramBox histogramBox;
        private System.Windows.Forms.Panel pnlGamma;
        private gTrackBar.gTrackBar gtbGamma;
        private System.Windows.Forms.Label lbGamma;
        private System.Windows.Forms.ToolStripComboBox cbxChannel;
        private System.Windows.Forms.ToolStripSeparator sp;
    }
}