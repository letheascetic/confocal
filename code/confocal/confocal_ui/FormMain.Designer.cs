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
            this.mainMenu = new C1.Win.C1Command.C1MainMenu();
            this.cmdHolder = new C1.Win.C1Command.C1CommandHolder();
            this.cmdLinkFile = new C1.Win.C1Command.C1CommandLink();
            this.cmdMenuFile = new C1.Win.C1Command.C1CommandMenu();
            this.dockToolbar = new C1.Win.C1Command.C1CommandDock();
            this.toolBar = new C1.Win.C1Command.C1ToolBar();
            this.cmdLineFileNew = new C1.Win.C1Command.C1CommandLink();
            this.cmdFileNew = new C1.Win.C1Command.C1Command();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tpgStatistics = new C1.Win.C1Command.C1DockingTabPage();
            this.tpgImage = new C1.Win.C1Command.C1DockingTabPage();
            this.dockLeft = new C1.Win.C1Command.C1DockingTab();
            this.tpgScanField = new C1.Win.C1Command.C1DockingTabPage();
            this.tpgScanSettings = new C1.Win.C1Command.C1DockingTabPage();
            this.dockRight = new C1.Win.C1Command.C1DockingTab();
            this.c1DockingTabPage3 = new C1.Win.C1Command.C1DockingTabPage();
            this.textLog = new System.Windows.Forms.TextBox();
            this.dockBottom = new C1.Win.C1Command.C1DockingTab();
            ((System.ComponentModel.ISupportInitialize)(this.cmdHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockToolbar)).BeginInit();
            this.dockToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockLeft)).BeginInit();
            this.dockLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockRight)).BeginInit();
            this.dockRight.SuspendLayout();
            this.c1DockingTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockBottom)).BeginInit();
            this.dockBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.AccessibleName = "Menu Bar";
            this.mainMenu.CommandHolder = this.cmdHolder;
            this.mainMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.cmdLinkFile});
            this.mainMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(884, 20);
            this.mainMenu.VisualStyle = C1.Win.C1Command.VisualStyle.System;
            this.mainMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.System;
            // 
            // cmdHolder
            // 
            this.cmdHolder.Commands.Add(this.cmdMenuFile);
            this.cmdHolder.Commands.Add(this.cmdFileNew);
            this.cmdHolder.Owner = this;
            // 
            // cmdLinkFile
            // 
            this.cmdLinkFile.Command = this.cmdMenuFile;
            // 
            // cmdMenuFile
            // 
            this.cmdMenuFile.HideNonRecentLinks = false;
            this.cmdMenuFile.Name = "cmdMenuFile";
            this.cmdMenuFile.ShortcutText = "";
            this.cmdMenuFile.Text = "文件";
            // 
            // dockToolbar
            // 
            this.dockToolbar.Controls.Add(this.toolBar);
            this.dockToolbar.Id = 1;
            this.dockToolbar.Location = new System.Drawing.Point(0, 20);
            this.dockToolbar.Name = "dockToolbar";
            this.dockToolbar.Size = new System.Drawing.Size(884, 25);
            // 
            // toolBar
            // 
            this.toolBar.AccessibleName = "Tool Bar";
            this.toolBar.CommandHolder = null;
            this.toolBar.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.cmdLineFileNew});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(29, 24);
            this.toolBar.Text = "c1ToolBar1";
            // 
            // cmdLineFileNew
            // 
            this.cmdLineFileNew.Command = this.cmdFileNew;
            // 
            // cmdFileNew
            // 
            this.cmdFileNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileNew.Image")));
            this.cmdFileNew.Name = "cmdFileNew";
            this.cmdFileNew.ShortcutText = "";
            this.cmdFileNew.Text = "新建（&N）";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(884, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip";
            // 
            // tpgStatistics
            // 
            this.tpgStatistics.CaptionVisible = true;
            this.tpgStatistics.Location = new System.Drawing.Point(0, 0);
            this.tpgStatistics.Name = "tpgStatistics";
            this.tpgStatistics.Size = new System.Drawing.Size(203, 471);
            this.tpgStatistics.TabIndex = 1;
            this.tpgStatistics.Text = "Statistics";
            // 
            // tpgImage
            // 
            this.tpgImage.CaptionVisible = true;
            this.tpgImage.Location = new System.Drawing.Point(0, 0);
            this.tpgImage.Name = "tpgImage";
            this.tpgImage.Size = new System.Drawing.Size(203, 471);
            this.tpgImage.TabIndex = 0;
            this.tpgImage.Text = "Image Settings";
            // 
            // dockLeft
            // 
            this.dockLeft.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.dockLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockLeft.CanAutoHide = true;
            this.dockLeft.CanCloseTabs = true;
            this.dockLeft.CanMoveTabs = true;
            this.dockLeft.Controls.Add(this.tpgImage);
            this.dockLeft.Controls.Add(this.tpgStatistics);
            this.dockLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockLeft.Location = new System.Drawing.Point(0, 45);
            this.dockLeft.Name = "dockLeft";
            this.dockLeft.SelectedIndex = 1;
            this.dockLeft.ShowCaption = true;
            this.dockLeft.Size = new System.Drawing.Size(203, 494);
            this.dockLeft.TabIndex = 10;
            this.dockLeft.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.dockLeft.TabsSpacing = 0;
            // 
            // tpgScanField
            // 
            this.tpgScanField.CaptionVisible = true;
            this.tpgScanField.Location = new System.Drawing.Point(0, 0);
            this.tpgScanField.Name = "tpgScanField";
            this.tpgScanField.Size = new System.Drawing.Size(203, 471);
            this.tpgScanField.TabIndex = 1;
            this.tpgScanField.Text = "Scan Field";
            // 
            // tpgScanSettings
            // 
            this.tpgScanSettings.CaptionVisible = true;
            this.tpgScanSettings.Location = new System.Drawing.Point(0, 0);
            this.tpgScanSettings.Name = "tpgScanSettings";
            this.tpgScanSettings.Size = new System.Drawing.Size(203, 471);
            this.tpgScanSettings.TabIndex = 0;
            this.tpgScanSettings.Text = "Scan Settings";
            // 
            // dockRight
            // 
            this.dockRight.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.dockRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockRight.CanAutoHide = true;
            this.dockRight.CanCloseTabs = true;
            this.dockRight.CanMoveTabs = true;
            this.dockRight.Controls.Add(this.tpgScanSettings);
            this.dockRight.Controls.Add(this.tpgScanField);
            this.dockRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockRight.Location = new System.Drawing.Point(681, 45);
            this.dockRight.Name = "dockRight";
            this.dockRight.SelectedIndex = 1;
            this.dockRight.ShowCaption = true;
            this.dockRight.Size = new System.Drawing.Size(203, 494);
            this.dockRight.TabIndex = 8;
            this.dockRight.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.dockRight.TabsSpacing = 0;
            // 
            // c1DockingTabPage3
            // 
            this.c1DockingTabPage3.CaptionVisible = true;
            this.c1DockingTabPage3.Controls.Add(this.textLog);
            this.c1DockingTabPage3.Location = new System.Drawing.Point(0, 0);
            this.c1DockingTabPage3.Name = "c1DockingTabPage3";
            this.c1DockingTabPage3.Size = new System.Drawing.Size(478, 115);
            this.c1DockingTabPage3.TabIndex = 0;
            this.c1DockingTabPage3.Text = "Log";
            // 
            // textLog
            // 
            this.textLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLog.Location = new System.Drawing.Point(0, 23);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(478, 92);
            this.textLog.TabIndex = 0;
            // 
            // dockBottom
            // 
            this.dockBottom.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.dockBottom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dockBottom.CanAutoHide = true;
            this.dockBottom.CanCloseTabs = true;
            this.dockBottom.CanMoveTabs = true;
            this.dockBottom.Controls.Add(this.c1DockingTabPage3);
            this.dockBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockBottom.Location = new System.Drawing.Point(203, 401);
            this.dockBottom.Name = "dockBottom";
            this.dockBottom.ShowCaption = true;
            this.dockBottom.Size = new System.Drawing.Size(478, 138);
            this.dockBottom.TabIndex = 11;
            this.dockBottom.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.dockBottom.TabsSpacing = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.dockBottom);
            this.Controls.Add(this.dockLeft);
            this.Controls.Add(this.dockRight);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dockToolbar);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Z1 Confocal v";
            ((System.ComponentModel.ISupportInitialize)(this.cmdHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockToolbar)).EndInit();
            this.dockToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockLeft)).EndInit();
            this.dockLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockRight)).EndInit();
            this.dockRight.ResumeLayout(false);
            this.c1DockingTabPage3.ResumeLayout(false);
            this.c1DockingTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockBottom)).EndInit();
            this.dockBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Command.C1MainMenu mainMenu;
        private C1.Win.C1Command.C1CommandHolder cmdHolder;
        private C1.Win.C1Command.C1CommandMenu cmdMenuFile;
        private C1.Win.C1Command.C1CommandLink cmdLinkFile;
        private C1.Win.C1Command.C1CommandDock dockToolbar;
        private C1.Win.C1Command.C1ToolBar toolBar;
        private C1.Win.C1Command.C1Command cmdFileNew;
        private C1.Win.C1Command.C1CommandLink cmdLineFileNew;
        private System.Windows.Forms.StatusStrip statusStrip;
        private C1.Win.C1Command.C1DockingTab dockBottom;
        private C1.Win.C1Command.C1DockingTabPage c1DockingTabPage3;
        private System.Windows.Forms.TextBox textLog;
        private C1.Win.C1Command.C1DockingTab dockLeft;
        private C1.Win.C1Command.C1DockingTabPage tpgImage;
        private C1.Win.C1Command.C1DockingTabPage tpgStatistics;
        private C1.Win.C1Command.C1DockingTab dockRight;
        private C1.Win.C1Command.C1DockingTabPage tpgScanSettings;
        private C1.Win.C1Command.C1DockingTabPage tpgScanField;
    }
}