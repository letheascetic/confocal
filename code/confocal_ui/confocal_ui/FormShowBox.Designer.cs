﻿namespace confocal_ui
{
    partial class FormShowBox
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea26 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend26 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowBox));
            System.Windows.Forms.DataVisualization.Charting.Series series26 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gbxGalv = new System.Windows.Forms.GroupBox();
            this.tbxCurveCoff = new System.Windows.Forms.TextBox();
            this.lbCurveCoff = new System.Windows.Forms.Label();
            this.tbxCalibrationV = new System.Windows.Forms.TextBox();
            this.lbCalibrationV = new System.Windows.Forms.Label();
            this.tbxResponseTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxFieldSize = new System.Windows.Forms.TextBox();
            this.lbFieldSize = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.gbxGalv.SuspendLayout();
            this.gbxResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea26.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea26);
            legend26.Name = "Legend1";
            this.chart1.Legends.Add(legend26);
            resources.ApplyResources(this.chart1, "chart1");
            this.chart1.Name = "chart1";
            series26.ChartArea = "ChartArea1";
            series26.Legend = "Legend1";
            series26.Name = "Series1";
            this.chart1.Series.Add(series26);
            // 
            // gbxGalv
            // 
            this.gbxGalv.Controls.Add(this.tbxCurveCoff);
            this.gbxGalv.Controls.Add(this.lbCurveCoff);
            this.gbxGalv.Controls.Add(this.tbxCalibrationV);
            this.gbxGalv.Controls.Add(this.lbCalibrationV);
            this.gbxGalv.Controls.Add(this.tbxResponseTime);
            this.gbxGalv.Controls.Add(this.label1);
            this.gbxGalv.Controls.Add(this.tbxFieldSize);
            this.gbxGalv.Controls.Add(this.lbFieldSize);
            this.gbxGalv.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.gbxGalv, "gbxGalv");
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
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            resources.ApplyResources(this.gbxResults, "gbxResults");
            this.gbxResults.Name = "gbxResults";
            this.gbxResults.TabStop = false;
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
            // tbxTotalSpCtPerLn
            // 
            resources.ApplyResources(this.tbxTotalSpCtPerLn, "tbxTotalSpCtPerLn");
            this.tbxTotalSpCtPerLn.Name = "tbxTotalSpCtPerLn";
            // 
            // lbTotalSpCtPerLn
            // 
            this.lbTotalSpCtPerLn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbTotalSpCtPerLn, "lbTotalSpCtPerLn");
            this.lbTotalSpCtPerLn.Name = "lbTotalSpCtPerLn";
            // 
            // tbxPostSpCtPerLn
            // 
            resources.ApplyResources(this.tbxPostSpCtPerLn, "tbxPostSpCtPerLn");
            this.tbxPostSpCtPerLn.Name = "tbxPostSpCtPerLn";
            // 
            // lbPostSpCtPerLn
            // 
            this.lbPostSpCtPerLn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbPostSpCtPerLn, "lbPostSpCtPerLn");
            this.lbPostSpCtPerLn.Name = "lbPostSpCtPerLn";
            // 
            // tbxVaildSpCtPerLn
            // 
            resources.ApplyResources(this.tbxVaildSpCtPerLn, "tbxVaildSpCtPerLn");
            this.tbxVaildSpCtPerLn.Name = "tbxVaildSpCtPerLn";
            // 
            // lbVaildSpCtPerLn
            // 
            this.lbVaildSpCtPerLn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbVaildSpCtPerLn, "lbVaildSpCtPerLn");
            this.lbVaildSpCtPerLn.Name = "lbVaildSpCtPerLn";
            // 
            // tbxPrevSpCtPerLn
            // 
            resources.ApplyResources(this.tbxPrevSpCtPerLn, "tbxPrevSpCtPerLn");
            this.tbxPrevSpCtPerLn.Name = "tbxPrevSpCtPerLn";
            // 
            // lbPrevSpCtPerLn
            // 
            this.lbPrevSpCtPerLn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.lbPrevSpCtPerLn, "lbPrevSpCtPerLn");
            this.lbPrevSpCtPerLn.Name = "lbPrevSpCtPerLn";
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
            // FormShowBox
            // 
            this.AutoHidePortion = 0.4D;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxResults);
            this.Controls.Add(this.gbxGalv);
            this.Controls.Add(this.chart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HideOnClose = true;
            this.Name = "FormShowBox";
            this.Load += new System.EventHandler(this.FormShowBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.gbxGalv.ResumeLayout(false);
            this.gbxGalv.PerformLayout();
            this.gbxResults.ResumeLayout(false);
            this.gbxResults.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox gbxGalv;
        private System.Windows.Forms.TextBox tbxCurveCoff;
        private System.Windows.Forms.Label lbCurveCoff;
        private System.Windows.Forms.TextBox tbxCalibrationV;
        private System.Windows.Forms.Label lbCalibrationV;
        private System.Windows.Forms.TextBox tbxResponseTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxFieldSize;
        private System.Windows.Forms.Label lbFieldSize;
        private System.Windows.Forms.GroupBox gbxResults;
        private System.Windows.Forms.TextBox tbxFPS;
        private System.Windows.Forms.Label lbFPS;
        private System.Windows.Forms.TextBox tbxTotalSpCtPerLn;
        private System.Windows.Forms.Label lbTotalSpCtPerLn;
        private System.Windows.Forms.TextBox tbxPostSpCtPerLn;
        private System.Windows.Forms.Label lbPostSpCtPerLn;
        private System.Windows.Forms.TextBox tbxVaildSpCtPerLn;
        private System.Windows.Forms.Label lbVaildSpCtPerLn;
        private System.Windows.Forms.TextBox tbxPrevSpCtPerLn;
        private System.Windows.Forms.Label lbPrevSpCtPerLn;
        private System.Windows.Forms.TextBox tbxVoltagePerPixel;
        private System.Windows.Forms.Label lbVoltagePerPixel;
        private System.Windows.Forms.TextBox tbxPixelSize;
        private System.Windows.Forms.Label lbPixelSize;
        private System.Windows.Forms.TextBox tbxAORate;
        private System.Windows.Forms.Label lbAORate;
    }
}