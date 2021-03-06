﻿namespace confocal_ui
{
    partial class FormParas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParas));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel = new System.Windows.Forms.Panel();
            this.gbxResults = new System.Windows.Forms.GroupBox();
            this.tbxScanTimePerFm = new System.Windows.Forms.TextBox();
            this.lbScanTimePerFm = new System.Windows.Forms.Label();
            this.tbxValidSpCtPerLn = new System.Windows.Forms.TextBox();
            this.lbValidSpCtPerLine = new System.Windows.Forms.Label();
            this.tbxPixelRate = new System.Windows.Forms.TextBox();
            this.lbPixelRate = new System.Windows.Forms.Label();
            this.tbxScanTimePerLine = new System.Windows.Forms.TextBox();
            this.lbScanTimePerLine = new System.Windows.Forms.Label();
            this.tbxFPS = new System.Windows.Forms.TextBox();
            this.lbFPS = new System.Windows.Forms.Label();
            this.tbxSpCtPerFm = new System.Windows.Forms.TextBox();
            this.lbAoSpCtPerFm = new System.Windows.Forms.Label();
            this.tbxAoSpCtPerLn = new System.Windows.Forms.TextBox();
            this.lbAoSpCtPerLn = new System.Windows.Forms.Label();
            this.tbxVoltagePerPixel = new System.Windows.Forms.TextBox();
            this.lbVoltagePerPixel = new System.Windows.Forms.Label();
            this.tbxPixelSize = new System.Windows.Forms.TextBox();
            this.lbPixelSize = new System.Windows.Forms.Label();
            this.tbxAORate = new System.Windows.Forms.TextBox();
            this.lbAORate = new System.Windows.Forms.Label();
            this.gbxGalv = new System.Windows.Forms.GroupBox();
            this.tbxCurveCoff = new System.Windows.Forms.TextBox();
            this.lbCurveCoff = new System.Windows.Forms.Label();
            this.tbxCalibrationV = new System.Windows.Forms.TextBox();
            this.lbCalibrationV = new System.Windows.Forms.Label();
            this.tbxResponseTime = new System.Windows.Forms.TextBox();
            this.lbResponseTime = new System.Windows.Forms.Label();
            this.tbxFieldSize = new System.Windows.Forms.TextBox();
            this.lbFieldSize = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbScanRowsPerAcq = new System.Windows.Forms.Label();
            this.lbScanPixelsPerAcq = new System.Windows.Forms.Label();
            this.tbxScanRowsPerAcq = new System.Windows.Forms.TextBox();
            this.tbxScanPixelsPerAcq = new System.Windows.Forms.TextBox();
            this.lbIntervalPerAcq = new System.Windows.Forms.Label();
            this.tbxIntervalPerAcq = new System.Windows.Forms.TextBox();
            this.panel.SuspendLayout();
            this.gbxResults.SuspendLayout();
            this.gbxGalv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            resources.ApplyResources(this.panel, "panel");
            this.panel.Controls.Add(this.gbxResults);
            this.panel.Controls.Add(this.gbxGalv);
            this.panel.Controls.Add(this.chart);
            this.panel.Name = "panel";
            // 
            // gbxResults
            // 
            resources.ApplyResources(this.gbxResults, "gbxResults");
            this.gbxResults.Controls.Add(this.tbxIntervalPerAcq);
            this.gbxResults.Controls.Add(this.lbIntervalPerAcq);
            this.gbxResults.Controls.Add(this.tbxScanPixelsPerAcq);
            this.gbxResults.Controls.Add(this.tbxScanRowsPerAcq);
            this.gbxResults.Controls.Add(this.lbScanPixelsPerAcq);
            this.gbxResults.Controls.Add(this.lbScanRowsPerAcq);
            this.gbxResults.Controls.Add(this.tbxScanTimePerFm);
            this.gbxResults.Controls.Add(this.lbScanTimePerFm);
            this.gbxResults.Controls.Add(this.tbxValidSpCtPerLn);
            this.gbxResults.Controls.Add(this.lbValidSpCtPerLine);
            this.gbxResults.Controls.Add(this.tbxPixelRate);
            this.gbxResults.Controls.Add(this.lbPixelRate);
            this.gbxResults.Controls.Add(this.tbxScanTimePerLine);
            this.gbxResults.Controls.Add(this.lbScanTimePerLine);
            this.gbxResults.Controls.Add(this.tbxFPS);
            this.gbxResults.Controls.Add(this.lbFPS);
            this.gbxResults.Controls.Add(this.tbxSpCtPerFm);
            this.gbxResults.Controls.Add(this.lbAoSpCtPerFm);
            this.gbxResults.Controls.Add(this.tbxAoSpCtPerLn);
            this.gbxResults.Controls.Add(this.lbAoSpCtPerLn);
            this.gbxResults.Controls.Add(this.tbxVoltagePerPixel);
            this.gbxResults.Controls.Add(this.lbVoltagePerPixel);
            this.gbxResults.Controls.Add(this.tbxPixelSize);
            this.gbxResults.Controls.Add(this.lbPixelSize);
            this.gbxResults.Controls.Add(this.tbxAORate);
            this.gbxResults.Controls.Add(this.lbAORate);
            this.gbxResults.Name = "gbxResults";
            this.gbxResults.TabStop = false;
            // 
            // tbxScanTimePerFm
            // 
            resources.ApplyResources(this.tbxScanTimePerFm, "tbxScanTimePerFm");
            this.tbxScanTimePerFm.Name = "tbxScanTimePerFm";
            // 
            // lbScanTimePerFm
            // 
            resources.ApplyResources(this.lbScanTimePerFm, "lbScanTimePerFm");
            this.lbScanTimePerFm.Name = "lbScanTimePerFm";
            // 
            // tbxValidSpCtPerLn
            // 
            resources.ApplyResources(this.tbxValidSpCtPerLn, "tbxValidSpCtPerLn");
            this.tbxValidSpCtPerLn.Name = "tbxValidSpCtPerLn";
            // 
            // lbValidSpCtPerLine
            // 
            this.lbValidSpCtPerLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbValidSpCtPerLine, "lbValidSpCtPerLine");
            this.lbValidSpCtPerLine.Name = "lbValidSpCtPerLine";
            // 
            // tbxPixelRate
            // 
            resources.ApplyResources(this.tbxPixelRate, "tbxPixelRate");
            this.tbxPixelRate.Name = "tbxPixelRate";
            // 
            // lbPixelRate
            // 
            this.lbPixelRate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbPixelRate, "lbPixelRate");
            this.lbPixelRate.Name = "lbPixelRate";
            // 
            // tbxScanTimePerLine
            // 
            resources.ApplyResources(this.tbxScanTimePerLine, "tbxScanTimePerLine");
            this.tbxScanTimePerLine.Name = "tbxScanTimePerLine";
            // 
            // lbScanTimePerLine
            // 
            resources.ApplyResources(this.lbScanTimePerLine, "lbScanTimePerLine");
            this.lbScanTimePerLine.Name = "lbScanTimePerLine";
            // 
            // tbxFPS
            // 
            resources.ApplyResources(this.tbxFPS, "tbxFPS");
            this.tbxFPS.Name = "tbxFPS";
            // 
            // lbFPS
            // 
            this.lbFPS.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbFPS, "lbFPS");
            this.lbFPS.Name = "lbFPS";
            // 
            // tbxSpCtPerFm
            // 
            resources.ApplyResources(this.tbxSpCtPerFm, "tbxSpCtPerFm");
            this.tbxSpCtPerFm.Name = "tbxSpCtPerFm";
            // 
            // lbAoSpCtPerFm
            // 
            this.lbAoSpCtPerFm.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbAoSpCtPerFm, "lbAoSpCtPerFm");
            this.lbAoSpCtPerFm.Name = "lbAoSpCtPerFm";
            // 
            // tbxAoSpCtPerLn
            // 
            resources.ApplyResources(this.tbxAoSpCtPerLn, "tbxAoSpCtPerLn");
            this.tbxAoSpCtPerLn.Name = "tbxAoSpCtPerLn";
            // 
            // lbAoSpCtPerLn
            // 
            this.lbAoSpCtPerLn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbAoSpCtPerLn, "lbAoSpCtPerLn");
            this.lbAoSpCtPerLn.Name = "lbAoSpCtPerLn";
            // 
            // tbxVoltagePerPixel
            // 
            resources.ApplyResources(this.tbxVoltagePerPixel, "tbxVoltagePerPixel");
            this.tbxVoltagePerPixel.Name = "tbxVoltagePerPixel";
            // 
            // lbVoltagePerPixel
            // 
            this.lbVoltagePerPixel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbVoltagePerPixel, "lbVoltagePerPixel");
            this.lbVoltagePerPixel.Name = "lbVoltagePerPixel";
            // 
            // tbxPixelSize
            // 
            resources.ApplyResources(this.tbxPixelSize, "tbxPixelSize");
            this.tbxPixelSize.Name = "tbxPixelSize";
            // 
            // lbPixelSize
            // 
            this.lbPixelSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbPixelSize, "lbPixelSize");
            this.lbPixelSize.Name = "lbPixelSize";
            // 
            // tbxAORate
            // 
            resources.ApplyResources(this.tbxAORate, "tbxAORate");
            this.tbxAORate.Name = "tbxAORate";
            // 
            // lbAORate
            // 
            this.lbAORate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbAORate, "lbAORate");
            this.lbAORate.Name = "lbAORate";
            // 
            // gbxGalv
            // 
            resources.ApplyResources(this.gbxGalv, "gbxGalv");
            this.gbxGalv.Controls.Add(this.tbxCurveCoff);
            this.gbxGalv.Controls.Add(this.lbCurveCoff);
            this.gbxGalv.Controls.Add(this.tbxCalibrationV);
            this.gbxGalv.Controls.Add(this.lbCalibrationV);
            this.gbxGalv.Controls.Add(this.tbxResponseTime);
            this.gbxGalv.Controls.Add(this.lbResponseTime);
            this.gbxGalv.Controls.Add(this.tbxFieldSize);
            this.gbxGalv.Controls.Add(this.lbFieldSize);
            this.gbxGalv.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gbxGalv.Name = "gbxGalv";
            this.gbxGalv.TabStop = false;
            // 
            // tbxCurveCoff
            // 
            resources.ApplyResources(this.tbxCurveCoff, "tbxCurveCoff");
            this.tbxCurveCoff.Name = "tbxCurveCoff";
            // 
            // lbCurveCoff
            // 
            this.lbCurveCoff.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbCurveCoff, "lbCurveCoff");
            this.lbCurveCoff.Name = "lbCurveCoff";
            // 
            // tbxCalibrationV
            // 
            resources.ApplyResources(this.tbxCalibrationV, "tbxCalibrationV");
            this.tbxCalibrationV.Name = "tbxCalibrationV";
            // 
            // lbCalibrationV
            // 
            this.lbCalibrationV.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbCalibrationV, "lbCalibrationV");
            this.lbCalibrationV.Name = "lbCalibrationV";
            // 
            // tbxResponseTime
            // 
            resources.ApplyResources(this.tbxResponseTime, "tbxResponseTime");
            this.tbxResponseTime.Name = "tbxResponseTime";
            // 
            // lbResponseTime
            // 
            this.lbResponseTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbResponseTime, "lbResponseTime");
            this.lbResponseTime.Name = "lbResponseTime";
            // 
            // tbxFieldSize
            // 
            resources.ApplyResources(this.tbxFieldSize, "tbxFieldSize");
            this.tbxFieldSize.Name = "tbxFieldSize";
            // 
            // lbFieldSize
            // 
            this.lbFieldSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbFieldSize, "lbFieldSize");
            this.lbFieldSize.Name = "lbFieldSize";
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            resources.ApplyResources(this.chart, "chart");
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "X振镜";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Y1振镜";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Y2振镜";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series4.Legend = "Legend1";
            series4.Name = "触发";
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Series.Add(series3);
            this.chart.Series.Add(series4);
            // 
            // lbScanRowsPerAcq
            // 
            this.lbScanRowsPerAcq.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbScanRowsPerAcq, "lbScanRowsPerAcq");
            this.lbScanRowsPerAcq.Name = "lbScanRowsPerAcq";
            // 
            // lbScanPixelsPerAcq
            // 
            this.lbScanPixelsPerAcq.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbScanPixelsPerAcq, "lbScanPixelsPerAcq");
            this.lbScanPixelsPerAcq.Name = "lbScanPixelsPerAcq";
            // 
            // tbxScanRowsPerAcq
            // 
            resources.ApplyResources(this.tbxScanRowsPerAcq, "tbxScanRowsPerAcq");
            this.tbxScanRowsPerAcq.Name = "tbxScanRowsPerAcq";
            // 
            // tbxScanPixelsPerAcq
            // 
            resources.ApplyResources(this.tbxScanPixelsPerAcq, "tbxScanPixelsPerAcq");
            this.tbxScanPixelsPerAcq.Name = "tbxScanPixelsPerAcq";
            // 
            // lbIntervalPerAcq
            // 
            this.lbIntervalPerAcq.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbIntervalPerAcq, "lbIntervalPerAcq");
            this.lbIntervalPerAcq.Name = "lbIntervalPerAcq";
            // 
            // tbxIntervalPerAcq
            // 
            resources.ApplyResources(this.tbxIntervalPerAcq, "tbxIntervalPerAcq");
            this.tbxIntervalPerAcq.Name = "tbxIntervalPerAcq";
            // 
            // FormParas
            // 
            this.AutoHidePortion = 0.4D;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HideOnClose = true;
            this.Name = "FormParas";
            this.Load += new System.EventHandler(this.FormShowBox_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.gbxResults.ResumeLayout(false);
            this.gbxResults.PerformLayout();
            this.gbxGalv.ResumeLayout(false);
            this.gbxGalv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.GroupBox gbxGalv;
        private System.Windows.Forms.TextBox tbxCurveCoff;
        private System.Windows.Forms.Label lbCurveCoff;
        private System.Windows.Forms.TextBox tbxCalibrationV;
        private System.Windows.Forms.Label lbCalibrationV;
        private System.Windows.Forms.TextBox tbxResponseTime;
        private System.Windows.Forms.Label lbResponseTime;
        private System.Windows.Forms.TextBox tbxFieldSize;
        private System.Windows.Forms.Label lbFieldSize;
        private System.Windows.Forms.GroupBox gbxResults;
        private System.Windows.Forms.TextBox tbxFPS;
        private System.Windows.Forms.Label lbFPS;
        private System.Windows.Forms.TextBox tbxSpCtPerFm;
        private System.Windows.Forms.Label lbAoSpCtPerFm;
        private System.Windows.Forms.TextBox tbxAoSpCtPerLn;
        private System.Windows.Forms.Label lbAoSpCtPerLn;
        private System.Windows.Forms.TextBox tbxVoltagePerPixel;
        private System.Windows.Forms.Label lbVoltagePerPixel;
        private System.Windows.Forms.TextBox tbxPixelSize;
        private System.Windows.Forms.Label lbPixelSize;
        private System.Windows.Forms.TextBox tbxAORate;
        private System.Windows.Forms.Label lbAORate;
        private System.Windows.Forms.Label lbScanTimePerLine;
        private System.Windows.Forms.TextBox tbxScanTimePerLine;
        private System.Windows.Forms.Label lbPixelRate;
        private System.Windows.Forms.TextBox tbxPixelRate;
        private System.Windows.Forms.TextBox tbxValidSpCtPerLn;
        private System.Windows.Forms.Label lbValidSpCtPerLine;
        private System.Windows.Forms.TextBox tbxScanTimePerFm;
        private System.Windows.Forms.Label lbScanTimePerFm;
        private System.Windows.Forms.Label lbScanRowsPerAcq;
        private System.Windows.Forms.Label lbScanPixelsPerAcq;
        private System.Windows.Forms.TextBox tbxScanRowsPerAcq;
        private System.Windows.Forms.TextBox tbxIntervalPerAcq;
        private System.Windows.Forms.Label lbIntervalPerAcq;
        private System.Windows.Forms.TextBox tbxScanPixelsPerAcq;
    }
}