namespace confocal_test
{
    partial class FormGalv
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGalv));
            this.gbxGalv = new System.Windows.Forms.GroupBox();
            this.cbxDirection = new System.Windows.Forms.ComboBox();
            this.lbDirection = new System.Windows.Forms.Label();
            this.cbxGalvNum = new System.Windows.Forms.ComboBox();
            this.lbGalvNum = new System.Windows.Forms.Label();
            this.tbxCurveCoff = new System.Windows.Forms.TextBox();
            this.lbCurveCoff = new System.Windows.Forms.Label();
            this.tbxCalibrationV = new System.Windows.Forms.TextBox();
            this.tbxPixelTime = new System.Windows.Forms.TextBox();
            this.lbPixelTime = new System.Windows.Forms.Label();
            this.lbCalibrationV = new System.Windows.Forms.Label();
            this.cbxPixels = new System.Windows.Forms.ComboBox();
            this.tbxResponseTime = new System.Windows.Forms.TextBox();
            this.lbResponseTime = new System.Windows.Forms.Label();
            this.tbxFieldSize = new System.Windows.Forms.TextBox();
            this.lbPixels = new System.Windows.Forms.Label();
            this.lbFieldSize = new System.Windows.Forms.Label();
            this.ctGalv = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.gbxResults = new System.Windows.Forms.GroupBox();
            this.tbxFPS = new System.Windows.Forms.TextBox();
            this.lbFPS = new System.Windows.Forms.Label();
            this.tbxTotalSpCtPerLn = new System.Windows.Forms.TextBox();
            this.lbTotalSpCtPerLn = new System.Windows.Forms.Label();
            this.tbxPostSpCtPerLn = new System.Windows.Forms.TextBox();
            this.lbPostSpCtPerLn = new System.Windows.Forms.Label();
            this.tbxVaildSpCtPerLn = new System.Windows.Forms.TextBox();
            this.lbVaildSpCtPerLn = new System.Windows.Forms.Label();
            this.tbxPrevSpCtPerLn = new System.Windows.Forms.TextBox();
            this.lbPrevSpCtPerLn = new System.Windows.Forms.Label();
            this.tbxVoltagePerPixel = new System.Windows.Forms.TextBox();
            this.lbVoltagePerPixel = new System.Windows.Forms.Label();
            this.tbxPixelSize = new System.Windows.Forms.TextBox();
            this.lbPixelSize = new System.Windows.Forms.Label();
            this.tbxAORate = new System.Windows.Forms.TextBox();
            this.lbAORate = new System.Windows.Forms.Label();
            this.gbxGalv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctGalv)).BeginInit();
            this.gbxResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxGalv
            // 
            this.gbxGalv.Controls.Add(this.cbxDirection);
            this.gbxGalv.Controls.Add(this.lbDirection);
            this.gbxGalv.Controls.Add(this.cbxGalvNum);
            this.gbxGalv.Controls.Add(this.lbGalvNum);
            this.gbxGalv.Controls.Add(this.tbxCurveCoff);
            this.gbxGalv.Controls.Add(this.lbCurveCoff);
            this.gbxGalv.Controls.Add(this.tbxCalibrationV);
            this.gbxGalv.Controls.Add(this.tbxPixelTime);
            this.gbxGalv.Controls.Add(this.lbPixelTime);
            this.gbxGalv.Controls.Add(this.lbCalibrationV);
            this.gbxGalv.Controls.Add(this.cbxPixels);
            this.gbxGalv.Controls.Add(this.tbxResponseTime);
            this.gbxGalv.Controls.Add(this.lbResponseTime);
            this.gbxGalv.Controls.Add(this.tbxFieldSize);
            this.gbxGalv.Controls.Add(this.lbPixels);
            this.gbxGalv.Controls.Add(this.lbFieldSize);
            this.gbxGalv.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gbxGalv.Location = new System.Drawing.Point(12, 12);
            this.gbxGalv.Name = "gbxGalv";
            this.gbxGalv.Size = new System.Drawing.Size(395, 431);
            this.gbxGalv.TabIndex = 3;
            this.gbxGalv.TabStop = false;
            this.gbxGalv.Text = "配置参数";
            // 
            // cbxDirection
            // 
            this.cbxDirection.Location = new System.Drawing.Point(192, 388);
            this.cbxDirection.Name = "cbxDirection";
            this.cbxDirection.Size = new System.Drawing.Size(179, 28);
            this.cbxDirection.TabIndex = 15;
            // 
            // lbDirection
            // 
            this.lbDirection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbDirection.Location = new System.Drawing.Point(26, 391);
            this.lbDirection.Name = "lbDirection";
            this.lbDirection.Size = new System.Drawing.Size(179, 20);
            this.lbDirection.TabIndex = 16;
            this.lbDirection.Text = "扫描方向:";
            // 
            // cbxGalvNum
            // 
            this.cbxGalvNum.Location = new System.Drawing.Point(192, 336);
            this.cbxGalvNum.Name = "cbxGalvNum";
            this.cbxGalvNum.Size = new System.Drawing.Size(179, 28);
            this.cbxGalvNum.TabIndex = 13;
            // 
            // lbGalvNum
            // 
            this.lbGalvNum.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbGalvNum.Location = new System.Drawing.Point(26, 339);
            this.lbGalvNum.Name = "lbGalvNum";
            this.lbGalvNum.Size = new System.Drawing.Size(179, 20);
            this.lbGalvNum.TabIndex = 14;
            this.lbGalvNum.Text = "振镜:";
            // 
            // tbxCurveCoff
            // 
            this.tbxCurveCoff.Location = new System.Drawing.Point(192, 281);
            this.tbxCurveCoff.Name = "tbxCurveCoff";
            this.tbxCurveCoff.Size = new System.Drawing.Size(179, 27);
            this.tbxCurveCoff.TabIndex = 12;
            this.tbxCurveCoff.Text = "10";
            // 
            // lbCurveCoff
            // 
            this.lbCurveCoff.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbCurveCoff.Location = new System.Drawing.Point(26, 284);
            this.lbCurveCoff.Name = "lbCurveCoff";
            this.lbCurveCoff.Size = new System.Drawing.Size(166, 22);
            this.lbCurveCoff.TabIndex = 11;
            this.lbCurveCoff.Text = "曲线系数（%）：";
            // 
            // tbxCalibrationV
            // 
            this.tbxCalibrationV.Location = new System.Drawing.Point(192, 231);
            this.tbxCalibrationV.Name = "tbxCalibrationV";
            this.tbxCalibrationV.Size = new System.Drawing.Size(179, 27);
            this.tbxCalibrationV.TabIndex = 10;
            this.tbxCalibrationV.Text = "4.09855e-5";
            // 
            // tbxPixelTime
            // 
            this.tbxPixelTime.Location = new System.Drawing.Point(192, 182);
            this.tbxPixelTime.Name = "tbxPixelTime";
            this.tbxPixelTime.Size = new System.Drawing.Size(179, 27);
            this.tbxPixelTime.TabIndex = 8;
            this.tbxPixelTime.Text = "2";
            // 
            // lbPixelTime
            // 
            this.lbPixelTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbPixelTime.Location = new System.Drawing.Point(26, 185);
            this.lbPixelTime.Name = "lbPixelTime";
            this.lbPixelTime.Size = new System.Drawing.Size(166, 22);
            this.lbPixelTime.TabIndex = 7;
            this.lbPixelTime.Text = "像素时间 (us)：";
            // 
            // lbCalibrationV
            // 
            this.lbCalibrationV.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbCalibrationV.Location = new System.Drawing.Point(26, 234);
            this.lbCalibrationV.Name = "lbCalibrationV";
            this.lbCalibrationV.Size = new System.Drawing.Size(166, 22);
            this.lbCalibrationV.TabIndex = 9;
            this.lbCalibrationV.Text = "标定电压 (V)：";
            // 
            // cbxPixels
            // 
            this.cbxPixels.Location = new System.Drawing.Point(192, 133);
            this.cbxPixels.Name = "cbxPixels";
            this.cbxPixels.Size = new System.Drawing.Size(179, 28);
            this.cbxPixels.TabIndex = 1;
            // 
            // tbxResponseTime
            // 
            this.tbxResponseTime.Location = new System.Drawing.Point(192, 33);
            this.tbxResponseTime.Name = "tbxResponseTime";
            this.tbxResponseTime.Size = new System.Drawing.Size(179, 27);
            this.tbxResponseTime.TabIndex = 6;
            this.tbxResponseTime.Text = "200";
            // 
            // lbResponseTime
            // 
            this.lbResponseTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbResponseTime.Location = new System.Drawing.Point(26, 36);
            this.lbResponseTime.Name = "lbResponseTime";
            this.lbResponseTime.Size = new System.Drawing.Size(153, 22);
            this.lbResponseTime.TabIndex = 0;
            this.lbResponseTime.Text = "振镜响应时间（us）：";
            // 
            // tbxFieldSize
            // 
            this.tbxFieldSize.Location = new System.Drawing.Point(192, 83);
            this.tbxFieldSize.Name = "tbxFieldSize";
            this.tbxFieldSize.Size = new System.Drawing.Size(179, 27);
            this.tbxFieldSize.TabIndex = 3;
            this.tbxFieldSize.Text = "200";
            // 
            // lbPixels
            // 
            this.lbPixels.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbPixels.Location = new System.Drawing.Point(26, 136);
            this.lbPixels.Name = "lbPixels";
            this.lbPixels.Size = new System.Drawing.Size(179, 22);
            this.lbPixels.TabIndex = 4;
            this.lbPixels.Text = "分辨率（扫描像素）:";
            // 
            // lbFieldSize
            // 
            this.lbFieldSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbFieldSize.Location = new System.Drawing.Point(26, 86);
            this.lbFieldSize.Name = "lbFieldSize";
            this.lbFieldSize.Size = new System.Drawing.Size(166, 22);
            this.lbFieldSize.TabIndex = 2;
            this.lbFieldSize.Text = "视场大小 (um)：";
            // 
            // ctGalv
            // 
            chartArea1.Name = "ChartArea1";
            this.ctGalv.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ctGalv.Legends.Add(legend1);
            this.ctGalv.Location = new System.Drawing.Point(425, 20);
            this.ctGalv.Name = "ctGalv";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "XGalv";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Y1Galv";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Y2Galv";
            this.ctGalv.Series.Add(series1);
            this.ctGalv.Series.Add(series2);
            this.ctGalv.Series.Add(series3);
            this.ctGalv.Size = new System.Drawing.Size(668, 423);
            this.ctGalv.TabIndex = 4;
            this.ctGalv.Text = "振镜控制曲线";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(812, 584);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(124, 30);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(949, 584);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(124, 30);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 622);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1104, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // gbxResults
            // 
            this.gbxResults.Controls.Add(this.tbxFPS);
            this.gbxResults.Controls.Add(this.lbFPS);
            this.gbxResults.Controls.Add(this.tbxTotalSpCtPerLn);
            this.gbxResults.Controls.Add(this.lbTotalSpCtPerLn);
            this.gbxResults.Controls.Add(this.tbxPostSpCtPerLn);
            this.gbxResults.Controls.Add(this.lbPostSpCtPerLn);
            this.gbxResults.Controls.Add(this.tbxVaildSpCtPerLn);
            this.gbxResults.Controls.Add(this.lbVaildSpCtPerLn);
            this.gbxResults.Controls.Add(this.tbxPrevSpCtPerLn);
            this.gbxResults.Controls.Add(this.lbPrevSpCtPerLn);
            this.gbxResults.Controls.Add(this.tbxVoltagePerPixel);
            this.gbxResults.Controls.Add(this.lbVoltagePerPixel);
            this.gbxResults.Controls.Add(this.tbxPixelSize);
            this.gbxResults.Controls.Add(this.lbPixelSize);
            this.gbxResults.Controls.Add(this.tbxAORate);
            this.gbxResults.Controls.Add(this.lbAORate);
            this.gbxResults.Location = new System.Drawing.Point(12, 449);
            this.gbxResults.Name = "gbxResults";
            this.gbxResults.Size = new System.Drawing.Size(1081, 129);
            this.gbxResults.TabIndex = 8;
            this.gbxResults.TabStop = false;
            this.gbxResults.Text = "计算结果";
            // 
            // tbxFPS
            // 
            this.tbxFPS.Location = new System.Drawing.Point(981, 81);
            this.tbxFPS.Name = "tbxFPS";
            this.tbxFPS.Size = new System.Drawing.Size(80, 27);
            this.tbxFPS.TabIndex = 30;
            this.tbxFPS.Text = "200";
            // 
            // lbFPS
            // 
            this.lbFPS.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbFPS.Location = new System.Drawing.Point(841, 81);
            this.lbFPS.Name = "lbFPS";
            this.lbFPS.Size = new System.Drawing.Size(125, 22);
            this.lbFPS.TabIndex = 31;
            this.lbFPS.Text = "帧率（fps）：";
            // 
            // tbxTotalSpCtPerLn
            // 
            this.tbxTotalSpCtPerLn.Location = new System.Drawing.Point(981, 33);
            this.tbxTotalSpCtPerLn.Name = "tbxTotalSpCtPerLn";
            this.tbxTotalSpCtPerLn.Size = new System.Drawing.Size(80, 27);
            this.tbxTotalSpCtPerLn.TabIndex = 28;
            this.tbxTotalSpCtPerLn.Text = "200";
            // 
            // lbTotalSpCtPerLn
            // 
            this.lbTotalSpCtPerLn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbTotalSpCtPerLn.Location = new System.Drawing.Point(841, 33);
            this.lbTotalSpCtPerLn.Name = "lbTotalSpCtPerLn";
            this.lbTotalSpCtPerLn.Size = new System.Drawing.Size(125, 22);
            this.lbTotalSpCtPerLn.TabIndex = 29;
            this.lbTotalSpCtPerLn.Text = "每行像素总数：";
            // 
            // tbxPostSpCtPerLn
            // 
            this.tbxPostSpCtPerLn.Location = new System.Drawing.Point(717, 81);
            this.tbxPostSpCtPerLn.Name = "tbxPostSpCtPerLn";
            this.tbxPostSpCtPerLn.Size = new System.Drawing.Size(80, 27);
            this.tbxPostSpCtPerLn.TabIndex = 26;
            this.tbxPostSpCtPerLn.Text = "200";
            // 
            // lbPostSpCtPerLn
            // 
            this.lbPostSpCtPerLn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbPostSpCtPerLn.Location = new System.Drawing.Point(577, 81);
            this.lbPostSpCtPerLn.Name = "lbPostSpCtPerLn";
            this.lbPostSpCtPerLn.Size = new System.Drawing.Size(125, 22);
            this.lbPostSpCtPerLn.TabIndex = 27;
            this.lbPostSpCtPerLn.Text = "每行后置像素数：";
            // 
            // tbxVaildSpCtPerLn
            // 
            this.tbxVaildSpCtPerLn.Location = new System.Drawing.Point(717, 33);
            this.tbxVaildSpCtPerLn.Name = "tbxVaildSpCtPerLn";
            this.tbxVaildSpCtPerLn.Size = new System.Drawing.Size(80, 27);
            this.tbxVaildSpCtPerLn.TabIndex = 24;
            this.tbxVaildSpCtPerLn.Text = "200";
            // 
            // lbVaildSpCtPerLn
            // 
            this.lbVaildSpCtPerLn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbVaildSpCtPerLn.Location = new System.Drawing.Point(577, 33);
            this.lbVaildSpCtPerLn.Name = "lbVaildSpCtPerLn";
            this.lbVaildSpCtPerLn.Size = new System.Drawing.Size(125, 22);
            this.lbVaildSpCtPerLn.TabIndex = 25;
            this.lbVaildSpCtPerLn.Text = "每行有效像素数：";
            // 
            // tbxPrevSpCtPerLn
            // 
            this.tbxPrevSpCtPerLn.Location = new System.Drawing.Point(453, 84);
            this.tbxPrevSpCtPerLn.Name = "tbxPrevSpCtPerLn";
            this.tbxPrevSpCtPerLn.Size = new System.Drawing.Size(80, 27);
            this.tbxPrevSpCtPerLn.TabIndex = 22;
            this.tbxPrevSpCtPerLn.Text = "200";
            // 
            // lbPrevSpCtPerLn
            // 
            this.lbPrevSpCtPerLn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbPrevSpCtPerLn.Location = new System.Drawing.Point(310, 84);
            this.lbPrevSpCtPerLn.Name = "lbPrevSpCtPerLn";
            this.lbPrevSpCtPerLn.Size = new System.Drawing.Size(137, 22);
            this.lbPrevSpCtPerLn.TabIndex = 23;
            this.lbPrevSpCtPerLn.Text = "每行前置像素数：";
            // 
            // tbxVoltagePerPixel
            // 
            this.tbxVoltagePerPixel.Location = new System.Drawing.Point(453, 33);
            this.tbxVoltagePerPixel.Name = "tbxVoltagePerPixel";
            this.tbxVoltagePerPixel.Size = new System.Drawing.Size(80, 27);
            this.tbxVoltagePerPixel.TabIndex = 20;
            this.tbxVoltagePerPixel.Text = "200";
            // 
            // lbVoltagePerPixel
            // 
            this.lbVoltagePerPixel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbVoltagePerPixel.Location = new System.Drawing.Point(310, 33);
            this.lbVoltagePerPixel.Name = "lbVoltagePerPixel";
            this.lbVoltagePerPixel.Size = new System.Drawing.Size(137, 22);
            this.lbVoltagePerPixel.TabIndex = 21;
            this.lbVoltagePerPixel.Text = "像素电压差（V）：";
            // 
            // tbxPixelSize
            // 
            this.tbxPixelSize.Location = new System.Drawing.Point(192, 81);
            this.tbxPixelSize.Name = "tbxPixelSize";
            this.tbxPixelSize.Size = new System.Drawing.Size(80, 27);
            this.tbxPixelSize.TabIndex = 18;
            this.tbxPixelSize.Text = "200";
            // 
            // lbPixelSize
            // 
            this.lbPixelSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbPixelSize.Location = new System.Drawing.Point(6, 84);
            this.lbPixelSize.Name = "lbPixelSize";
            this.lbPixelSize.Size = new System.Drawing.Size(186, 22);
            this.lbPixelSize.TabIndex = 19;
            this.lbPixelSize.Text = "像素大小（um）：";
            // 
            // tbxAORate
            // 
            this.tbxAORate.Location = new System.Drawing.Point(192, 30);
            this.tbxAORate.Name = "tbxAORate";
            this.tbxAORate.Size = new System.Drawing.Size(80, 27);
            this.tbxAORate.TabIndex = 17;
            this.tbxAORate.Text = "200";
            // 
            // lbAORate
            // 
            this.lbAORate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbAORate.Location = new System.Drawing.Point(6, 33);
            this.lbAORate.Name = "lbAORate";
            this.lbAORate.Size = new System.Drawing.Size(186, 22);
            this.lbAORate.TabIndex = 17;
            this.lbAORate.Text = "样本输出速率（MS/s）：";
            // 
            // FormGalv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 644);
            this.Controls.Add(this.gbxResults);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.ctGalv);
            this.Controls.Add(this.gbxGalv);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FormGalv";
            this.Text = "Glav测试";
            this.Load += new System.EventHandler(this.FormGalv_Load);
            this.gbxGalv.ResumeLayout(false);
            this.gbxGalv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctGalv)).EndInit();
            this.gbxResults.ResumeLayout(false);
            this.gbxResults.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxGalv;
        private System.Windows.Forms.ComboBox cbxPixels;
        private System.Windows.Forms.Label lbResponseTime;
        private System.Windows.Forms.TextBox tbxFieldSize;
        private System.Windows.Forms.Label lbPixels;
        private System.Windows.Forms.Label lbFieldSize;
        private System.Windows.Forms.TextBox tbxResponseTime;
        private System.Windows.Forms.TextBox tbxPixelTime;
        private System.Windows.Forms.Label lbPixelTime;
        private System.Windows.Forms.TextBox tbxCalibrationV;
        private System.Windows.Forms.Label lbCalibrationV;
        private System.Windows.Forms.TextBox tbxCurveCoff;
        private System.Windows.Forms.Label lbCurveCoff;
        private System.Windows.Forms.DataVisualization.Charting.Chart ctGalv;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cbxGalvNum;
        private System.Windows.Forms.Label lbGalvNum;
        private System.Windows.Forms.ComboBox cbxDirection;
        private System.Windows.Forms.Label lbDirection;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox gbxResults;
        private System.Windows.Forms.Label lbAORate;
        private System.Windows.Forms.TextBox tbxAORate;
        private System.Windows.Forms.TextBox tbxPixelSize;
        private System.Windows.Forms.Label lbPixelSize;
        private System.Windows.Forms.TextBox tbxPrevSpCtPerLn;
        private System.Windows.Forms.Label lbPrevSpCtPerLn;
        private System.Windows.Forms.TextBox tbxVoltagePerPixel;
        private System.Windows.Forms.Label lbVoltagePerPixel;
        private System.Windows.Forms.TextBox tbxVaildSpCtPerLn;
        private System.Windows.Forms.Label lbVaildSpCtPerLn;
        private System.Windows.Forms.TextBox tbxPostSpCtPerLn;
        private System.Windows.Forms.Label lbPostSpCtPerLn;
        private System.Windows.Forms.TextBox tbxTotalSpCtPerLn;
        private System.Windows.Forms.Label lbTotalSpCtPerLn;
        private System.Windows.Forms.TextBox tbxFPS;
        private System.Windows.Forms.Label lbFPS;
    }
}