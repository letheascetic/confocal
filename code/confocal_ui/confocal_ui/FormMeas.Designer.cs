namespace confocal_ui
{
    partial class FormMeas
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMeas));
            this.pnlMeas = new System.Windows.Forms.Panel();
            this.pnlHistogram = new System.Windows.Forms.Panel();
            this.pnlHisBottom = new System.Windows.Forms.Panel();
            this.chartHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbxHSelect = new System.Windows.Forms.ComboBox();
            this.pnlScale = new System.Windows.Forms.Panel();
            this.pblScaleBottom = new System.Windows.Forms.Panel();
            this.cbxSSelect = new System.Windows.Forms.ComboBox();
            this.chartScale = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlMeas.SuspendLayout();
            this.pnlHistogram.SuspendLayout();
            this.pnlHisBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).BeginInit();
            this.pnlScale.SuspendLayout();
            this.pblScaleBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartScale)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMeas
            // 
            this.pnlMeas.AutoScroll = true;
            this.pnlMeas.AutoSize = true;
            this.pnlMeas.Controls.Add(this.pnlScale);
            this.pnlMeas.Controls.Add(this.pnlHistogram);
            this.pnlMeas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMeas.Location = new System.Drawing.Point(0, 0);
            this.pnlMeas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlMeas.Name = "pnlMeas";
            this.pnlMeas.Size = new System.Drawing.Size(381, 734);
            this.pnlMeas.TabIndex = 0;
            // 
            // pnlHistogram
            // 
            this.pnlHistogram.Controls.Add(this.chartHistogram);
            this.pnlHistogram.Controls.Add(this.pnlHisBottom);
            this.pnlHistogram.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHistogram.Location = new System.Drawing.Point(0, 0);
            this.pnlHistogram.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHistogram.Name = "pnlHistogram";
            this.pnlHistogram.Size = new System.Drawing.Size(381, 165);
            this.pnlHistogram.TabIndex = 1;
            // 
            // pnlHisBottom
            // 
            this.pnlHisBottom.Controls.Add(this.cbxHSelect);
            this.pnlHisBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHisBottom.Location = new System.Drawing.Point(0, 124);
            this.pnlHisBottom.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHisBottom.Name = "pnlHisBottom";
            this.pnlHisBottom.Size = new System.Drawing.Size(381, 41);
            this.pnlHisBottom.TabIndex = 3;
            // 
            // chartHistogram
            // 
            chartArea2.Name = "ChartArea1";
            this.chartHistogram.ChartAreas.Add(chartArea2);
            this.chartHistogram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartHistogram.Location = new System.Drawing.Point(0, 0);
            this.chartHistogram.Margin = new System.Windows.Forms.Padding(4);
            this.chartHistogram.Name = "chartHistogram";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            series2.YValuesPerPoint = 4;
            this.chartHistogram.Series.Add(series2);
            this.chartHistogram.Size = new System.Drawing.Size(381, 124);
            this.chartHistogram.TabIndex = 4;
            this.chartHistogram.Text = "chart1";
            // 
            // cbxHSelect
            // 
            this.cbxHSelect.FormattingEnabled = true;
            this.cbxHSelect.Items.AddRange(new object[] {
            "X方向",
            "Y方向",
            "自定义"});
            this.cbxHSelect.Location = new System.Drawing.Point(12, 7);
            this.cbxHSelect.Name = "cbxHSelect";
            this.cbxHSelect.Size = new System.Drawing.Size(100, 23);
            this.cbxHSelect.TabIndex = 0;
            // 
            // pnlScale
            // 
            this.pnlScale.Controls.Add(this.chartScale);
            this.pnlScale.Controls.Add(this.pblScaleBottom);
            this.pnlScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlScale.Location = new System.Drawing.Point(0, 165);
            this.pnlScale.Name = "pnlScale";
            this.pnlScale.Size = new System.Drawing.Size(381, 187);
            this.pnlScale.TabIndex = 2;
            // 
            // pblScaleBottom
            // 
            this.pblScaleBottom.Controls.Add(this.cbxSSelect);
            this.pblScaleBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pblScaleBottom.Location = new System.Drawing.Point(0, 146);
            this.pblScaleBottom.Margin = new System.Windows.Forms.Padding(0);
            this.pblScaleBottom.Name = "pblScaleBottom";
            this.pblScaleBottom.Size = new System.Drawing.Size(381, 41);
            this.pblScaleBottom.TabIndex = 4;
            // 
            // cbxSSelect
            // 
            this.cbxSSelect.FormattingEnabled = true;
            this.cbxSSelect.Items.AddRange(new object[] {
            "X方向",
            "Y方向",
            "自定义"});
            this.cbxSSelect.Location = new System.Drawing.Point(12, 7);
            this.cbxSSelect.Name = "cbxSSelect";
            this.cbxSSelect.Size = new System.Drawing.Size(100, 23);
            this.cbxSSelect.TabIndex = 0;
            // 
            // chartScale
            // 
            chartArea1.Name = "ChartArea1";
            this.chartScale.ChartAreas.Add(chartArea1);
            this.chartScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartScale.Location = new System.Drawing.Point(0, 0);
            this.chartScale.Margin = new System.Windows.Forms.Padding(4);
            this.chartScale.Name = "chartScale";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 4;
            this.chartScale.Series.Add(series1);
            this.chartScale.Size = new System.Drawing.Size(381, 146);
            this.chartScale.TabIndex = 5;
            this.chartScale.Text = "chart1";
            // 
            // FormMeas
            // 
            this.AutoHidePortion = 0.4D;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(381, 734);
            this.Controls.Add(this.pnlMeas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMeas";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "测量和调整";
            this.pnlMeas.ResumeLayout(false);
            this.pnlHistogram.ResumeLayout(false);
            this.pnlHisBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).EndInit();
            this.pnlScale.ResumeLayout(false);
            this.pblScaleBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMeas;
        private System.Windows.Forms.Panel pnlHistogram;
        private System.Windows.Forms.Panel pnlHisBottom;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistogram;
        private System.Windows.Forms.ComboBox cbxHSelect;
        private System.Windows.Forms.Panel pnlScale;
        private System.Windows.Forms.Panel pblScaleBottom;
        private System.Windows.Forms.ComboBox cbxSSelect;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartScale;
    }
}