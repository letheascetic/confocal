namespace confocal_ui
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.dockLeft = new C1.Win.C1Command.C1CommandDock();
            this.dockBottom = new C1.Win.C1Command.C1CommandDock();
            this.dockTabOutput = new C1.Win.C1Command.C1DockingTab();
            this.tpgLog = new C1.Win.C1Command.C1DockingTabPage();
            this.textLog = new System.Windows.Forms.TextBox();
            this.dockTabScan = new C1.Win.C1Command.C1DockingTab();
            this.tpgScanSettings = new C1.Win.C1Command.C1DockingTabPage();
            this.tpgScanField = new C1.Win.C1Command.C1DockingTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dockLeft)).BeginInit();
            this.dockLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockBottom)).BeginInit();
            this.dockBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockTabOutput)).BeginInit();
            this.dockTabOutput.SuspendLayout();
            this.tpgLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockTabScan)).BeginInit();
            this.dockTabScan.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 739);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(984, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(984, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(984, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // dockLeft
            // 
            this.dockLeft.Controls.Add(this.dockTabScan);
            this.dockLeft.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockLeft.Id = 3;
            this.dockLeft.Location = new System.Drawing.Point(751, 49);
            this.dockLeft.Name = "dockLeft";
            this.dockLeft.Size = new System.Drawing.Size(233, 690);
            // 
            // dockBottom
            // 
            this.dockBottom.Controls.Add(this.dockTabOutput);
            this.dockBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockBottom.Id = 1;
            this.dockBottom.Location = new System.Drawing.Point(0, 601);
            this.dockBottom.Name = "dockBottom";
            this.dockBottom.Size = new System.Drawing.Size(751, 138);
            // 
            // dockTabOutput
            // 
            this.dockTabOutput.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.dockTabOutput.AutoHiding = true;
            this.dockTabOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockTabOutput.CanAutoHide = true;
            this.dockTabOutput.CanCloseTabs = true;
            this.dockTabOutput.CanMoveTabs = true;
            this.dockTabOutput.Controls.Add(this.tpgLog);
            this.dockTabOutput.Location = new System.Drawing.Point(0, 0);
            this.dockTabOutput.Name = "dockTabOutput";
            this.dockTabOutput.ShowCaption = true;
            this.dockTabOutput.Size = new System.Drawing.Size(751, 138);
            this.dockTabOutput.TabIndex = 1;
            this.dockTabOutput.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.dockTabOutput.TabsSpacing = 0;
            // 
            // tpgLog
            // 
            this.tpgLog.CaptionVisible = true;
            this.tpgLog.Controls.Add(this.textLog);
            this.tpgLog.Location = new System.Drawing.Point(0, 3);
            this.tpgLog.Name = "tpgLog";
            this.tpgLog.Size = new System.Drawing.Size(751, 112);
            this.tpgLog.TabIndex = 0;
            this.tpgLog.Text = "Log";
            // 
            // textLog
            // 
            this.textLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLog.Location = new System.Drawing.Point(0, 23);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(751, 89);
            this.textLog.TabIndex = 0;
            // 
            // dockTabScan
            // 
            this.dockTabScan.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.dockTabScan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockTabScan.CanAutoHide = true;
            this.dockTabScan.CanCloseTabs = true;
            this.dockTabScan.CanMoveTabs = true;
            this.dockTabScan.Controls.Add(this.tpgScanSettings);
            this.dockTabScan.Controls.Add(this.tpgScanField);
            this.dockTabScan.Location = new System.Drawing.Point(0, 0);
            this.dockTabScan.Name = "dockTabScan";
            this.dockTabScan.SelectedIndex = 1;
            this.dockTabScan.ShowCaption = true;
            this.dockTabScan.Size = new System.Drawing.Size(233, 690);
            this.dockTabScan.TabIndex = 0;
            this.dockTabScan.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.dockTabScan.TabsSpacing = 0;
            // 
            // tpgScanSettings
            // 
            this.tpgScanSettings.CaptionVisible = true;
            this.tpgScanSettings.Location = new System.Drawing.Point(3, 0);
            this.tpgScanSettings.Name = "tpgScanSettings";
            this.tpgScanSettings.Size = new System.Drawing.Size(230, 667);
            this.tpgScanSettings.TabIndex = 0;
            this.tpgScanSettings.Text = "Scan Settings";
            // 
            // tpgScanField
            // 
            this.tpgScanField.CaptionVisible = true;
            this.tpgScanField.Location = new System.Drawing.Point(3, 0);
            this.tpgScanField.Name = "tpgScanField";
            this.tpgScanField.Size = new System.Drawing.Size(230, 667);
            this.tpgScanField.TabIndex = 1;
            this.tpgScanField.Text = "Scan Field";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.dockBottom);
            this.Controls.Add(this.dockLeft);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Z1 Confocal v";
            ((System.ComponentModel.ISupportInitialize)(this.dockLeft)).EndInit();
            this.dockLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockBottom)).EndInit();
            this.dockBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockTabOutput)).EndInit();
            this.dockTabOutput.ResumeLayout(false);
            this.tpgLog.ResumeLayout(false);
            this.tpgLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockTabScan)).EndInit();
            this.dockTabScan.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private C1.Win.C1Command.C1CommandDock dockLeft;
        private C1.Win.C1Command.C1CommandDock dockBottom;
        private C1.Win.C1Command.C1DockingTab dockTabOutput;
        private C1.Win.C1Command.C1DockingTabPage tpgLog;
        private System.Windows.Forms.TextBox textLog;
        private C1.Win.C1Command.C1DockingTab dockTabScan;
        private C1.Win.C1Command.C1DockingTabPage tpgScanSettings;
        private C1.Win.C1Command.C1DockingTabPage tpgScanField;
    }
}