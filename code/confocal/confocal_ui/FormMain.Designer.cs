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
            this.cmdMenuFile = new C1.Win.C1Command.C1CommandMenu();
            this.cmdLinkFileNew = new C1.Win.C1Command.C1CommandLink();
            this.cmdFileNew = new C1.Win.C1Command.C1Command();
            this.cmdLindFileOpen = new C1.Win.C1Command.C1CommandLink();
            this.cmdFileOpen = new C1.Win.C1Command.C1Command();
            this.cmdLindFileSave = new C1.Win.C1Command.C1CommandLink();
            this.cmdFileSave = new C1.Win.C1Command.C1Command();
            this.cmdLindFileClose = new C1.Win.C1Command.C1CommandLink();
            this.cmdFileClose = new C1.Win.C1Command.C1Command();
            this.cmdMenuView = new C1.Win.C1Command.C1CommandMenu();
            this.cmdLinkTheme = new C1.Win.C1Command.C1CommandLink();
            this.cmdTheme = new C1.Win.C1Command.C1Command();
            this.cmdLinkFile = new C1.Win.C1Command.C1CommandLink();
            this.cmdLinkView = new C1.Win.C1Command.C1CommandLink();
            this.dockToolBar = new C1.Win.C1Command.C1CommandDock();
            this.toolBar = new C1.Win.C1Command.C1ToolBar();
            this.dockMain = new C1.Win.C1Command.C1DockingTab();
            this.dockBottom = new C1.Win.C1Command.C1CommandDock();
            this.dockTabOutput = new C1.Win.C1Command.C1DockingTab();
            this.tpgLog = new C1.Win.C1Command.C1DockingTabPage();
            this.textLog = new System.Windows.Forms.TextBox();
            this.dockLeft = new C1.Win.C1Command.C1CommandDock();
            this.dockTabScan = new C1.Win.C1Command.C1DockingTab();
            this.tpgScanSettings = new C1.Win.C1Command.C1DockingTabPage();
            this.tpgScanField = new C1.Win.C1Command.C1DockingTabPage();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.cmdHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockToolBar)).BeginInit();
            this.dockToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockBottom)).BeginInit();
            this.dockBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockTabOutput)).BeginInit();
            this.dockTabOutput.SuspendLayout();
            this.tpgLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockLeft)).BeginInit();
            this.dockLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockTabScan)).BeginInit();
            this.dockTabScan.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.AccessibleName = "Menu Bar";
            this.mainMenu.CommandHolder = this.cmdHolder;
            this.mainMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.cmdLinkFile,
            this.cmdLinkView});
            this.mainMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(984, 26);
            this.mainMenu.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.mainMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // cmdHolder
            // 
            this.cmdHolder.Commands.Add(this.cmdMenuFile);
            this.cmdHolder.Commands.Add(this.cmdFileNew);
            this.cmdHolder.Commands.Add(this.cmdFileOpen);
            this.cmdHolder.Commands.Add(this.cmdFileSave);
            this.cmdHolder.Commands.Add(this.cmdFileClose);
            this.cmdHolder.Commands.Add(this.cmdMenuView);
            this.cmdHolder.Commands.Add(this.cmdTheme);
            this.cmdHolder.Owner = this;
            // 
            // cmdMenuFile
            // 
            this.cmdMenuFile.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.cmdLinkFileNew,
            this.cmdLindFileOpen,
            this.cmdLindFileSave,
            this.cmdLindFileClose});
            this.cmdMenuFile.HideNonRecentLinks = false;
            this.cmdMenuFile.Name = "cmdMenuFile";
            this.cmdMenuFile.ShortcutText = "";
            this.cmdMenuFile.Text = "文件（&F）";
            this.cmdMenuFile.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.cmdMenuFile.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // cmdLinkFileNew
            // 
            this.cmdLinkFileNew.Command = this.cmdFileNew;
            // 
            // cmdFileNew
            // 
            this.cmdFileNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileNew.Image")));
            this.cmdFileNew.Name = "cmdFileNew";
            this.cmdFileNew.ShortcutText = "";
            this.cmdFileNew.Text = "新建（&N）";
            // 
            // cmdLindFileOpen
            // 
            this.cmdLindFileOpen.Command = this.cmdFileOpen;
            this.cmdLindFileOpen.SortOrder = 1;
            // 
            // cmdFileOpen
            // 
            this.cmdFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileOpen.Image")));
            this.cmdFileOpen.Name = "cmdFileOpen";
            this.cmdFileOpen.ShortcutText = "";
            this.cmdFileOpen.Text = "打开（&O）";
            // 
            // cmdLindFileSave
            // 
            this.cmdLindFileSave.Command = this.cmdFileSave;
            this.cmdLindFileSave.SortOrder = 2;
            // 
            // cmdFileSave
            // 
            this.cmdFileSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileSave.Image")));
            this.cmdFileSave.Name = "cmdFileSave";
            this.cmdFileSave.ShortcutText = "";
            this.cmdFileSave.Text = "保存（&S）";
            // 
            // cmdLindFileClose
            // 
            this.cmdLindFileClose.Command = this.cmdFileClose;
            this.cmdLindFileClose.SortOrder = 3;
            // 
            // cmdFileClose
            // 
            this.cmdFileClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileClose.Image")));
            this.cmdFileClose.Name = "cmdFileClose";
            this.cmdFileClose.ShortcutText = "";
            this.cmdFileClose.Text = "关闭（&C）";
            // 
            // cmdMenuView
            // 
            this.cmdMenuView.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.cmdLinkTheme});
            this.cmdMenuView.HideNonRecentLinks = false;
            this.cmdMenuView.Name = "cmdMenuView";
            this.cmdMenuView.ShortcutText = "";
            this.cmdMenuView.Text = "视图（&V）";
            this.cmdMenuView.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.cmdMenuView.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // cmdLinkTheme
            // 
            this.cmdLinkTheme.Command = this.cmdTheme;
            // 
            // cmdTheme
            // 
            this.cmdTheme.Name = "cmdTheme";
            this.cmdTheme.ShortcutText = "";
            this.cmdTheme.Text = "主题（&T）";
            this.cmdTheme.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdTheme_Click);
            // 
            // cmdLinkFile
            // 
            this.cmdLinkFile.Command = this.cmdMenuFile;
            // 
            // cmdLinkView
            // 
            this.cmdLinkView.Command = this.cmdMenuView;
            this.cmdLinkView.SortOrder = 1;
            // 
            // dockToolBar
            // 
            this.dockToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dockToolBar.Controls.Add(this.toolBar);
            this.dockToolBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dockToolBar.Id = 3;
            this.dockToolBar.Location = new System.Drawing.Point(0, 26);
            this.dockToolBar.Name = "dockToolBar";
            this.dockToolBar.Size = new System.Drawing.Size(984, 26);
            // 
            // toolBar
            // 
            this.toolBar.AccessibleName = "Tool Bar";
            this.toolBar.CommandHolder = this.cmdHolder;
            this.toolBar.Location = new System.Drawing.Point(3, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(25, 25);
            this.toolBar.Text = "c1ToolBar1";
            this.toolBar.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.toolBar.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // dockMain
            // 
            this.dockMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockMain.CanCloseTabs = true;
            this.dockMain.CanMoveTabs = true;
            this.dockMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockMain.HotTrack = true;
            this.dockMain.Location = new System.Drawing.Point(0, 52);
            this.dockMain.Name = "dockMain";
            this.dockMain.Size = new System.Drawing.Size(751, 549);
            this.dockMain.TabIndex = 11;
            this.dockMain.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.dockMain.TabsSpacing = 5;
            this.dockMain.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2010;
            this.dockMain.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.dockMain.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // dockBottom
            // 
            this.dockBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.dockBottom.Controls.Add(this.dockTabOutput);
            this.dockBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockBottom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dockBottom.Id = 1;
            this.dockBottom.Location = new System.Drawing.Point(0, 601);
            this.dockBottom.Name = "dockBottom";
            this.dockBottom.Size = new System.Drawing.Size(751, 138);
            // 
            // dockTabOutput
            // 
            this.dockTabOutput.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.dockTabOutput.AutoHiding = true;
            this.dockTabOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockTabOutput.CanAutoHide = true;
            this.dockTabOutput.CanCloseTabs = true;
            this.dockTabOutput.CanMoveTabs = true;
            this.dockTabOutput.Controls.Add(this.tpgLog);
            this.dockTabOutput.HotTrack = true;
            this.dockTabOutput.Location = new System.Drawing.Point(0, 0);
            this.dockTabOutput.Name = "dockTabOutput";
            this.dockTabOutput.ShowCaption = true;
            this.dockTabOutput.Size = new System.Drawing.Size(751, 138);
            this.dockTabOutput.TabIndex = 1;
            this.dockTabOutput.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.dockTabOutput.TabsSpacing = 5;
            this.dockTabOutput.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2010;
            this.dockTabOutput.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.dockTabOutput.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // tpgLog
            // 
            this.tpgLog.CaptionVisible = true;
            this.tpgLog.Controls.Add(this.textLog);
            this.tpgLog.Location = new System.Drawing.Point(1, 4);
            this.tpgLog.Name = "tpgLog";
            this.tpgLog.Size = new System.Drawing.Size(749, 111);
            this.tpgLog.TabIndex = 0;
            this.tpgLog.Text = "Log";
            // 
            // textLog
            // 
            this.textLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.textLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textLog.Location = new System.Drawing.Point(0, 23);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(749, 88);
            this.textLog.TabIndex = 0;
            // 
            // dockLeft
            // 
            this.dockLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.dockLeft.Controls.Add(this.dockTabScan);
            this.dockLeft.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dockLeft.Id = 3;
            this.dockLeft.Location = new System.Drawing.Point(751, 52);
            this.dockLeft.Name = "dockLeft";
            this.dockLeft.Size = new System.Drawing.Size(233, 687);
            // 
            // dockTabScan
            // 
            this.dockTabScan.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.dockTabScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockTabScan.CanAutoHide = true;
            this.dockTabScan.CanCloseTabs = true;
            this.dockTabScan.CanMoveTabs = true;
            this.dockTabScan.Controls.Add(this.tpgScanSettings);
            this.dockTabScan.Controls.Add(this.tpgScanField);
            this.dockTabScan.HotTrack = true;
            this.dockTabScan.Location = new System.Drawing.Point(0, 0);
            this.dockTabScan.Name = "dockTabScan";
            this.dockTabScan.SelectedIndex = 1;
            this.dockTabScan.ShowCaption = true;
            this.dockTabScan.Size = new System.Drawing.Size(233, 687);
            this.dockTabScan.TabIndex = 0;
            this.dockTabScan.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.dockTabScan.TabsSpacing = 5;
            this.dockTabScan.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2010;
            this.dockTabScan.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.dockTabScan.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // tpgScanSettings
            // 
            this.tpgScanSettings.CaptionVisible = true;
            this.tpgScanSettings.Location = new System.Drawing.Point(4, 1);
            this.tpgScanSettings.Name = "tpgScanSettings";
            this.tpgScanSettings.Size = new System.Drawing.Size(228, 663);
            this.tpgScanSettings.TabIndex = 0;
            this.tpgScanSettings.Text = "Scan Settings";
            // 
            // tpgScanField
            // 
            this.tpgScanField.CaptionVisible = true;
            this.tpgScanField.Location = new System.Drawing.Point(4, 1);
            this.tpgScanField.Name = "tpgScanField";
            this.tpgScanField.Size = new System.Drawing.Size(228, 663);
            this.tpgScanField.TabIndex = 1;
            this.tpgScanField.Text = "Scan Field";
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.statusStrip.Location = new System.Drawing.Point(0, 739);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(984, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.dockMain);
            this.Controls.Add(this.dockBottom);
            this.Controls.Add(this.dockLeft);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dockToolBar);
            this.Controls.Add(this.mainMenu);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Z1 Confocal v";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Custom;
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmdHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockToolBar)).EndInit();
            this.dockToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockBottom)).EndInit();
            this.dockBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockTabOutput)).EndInit();
            this.dockTabOutput.ResumeLayout(false);
            this.tpgLog.ResumeLayout(false);
            this.tpgLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockLeft)).EndInit();
            this.dockLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockTabScan)).EndInit();
            this.dockTabScan.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Command.C1MainMenu mainMenu;
        private C1.Win.C1Command.C1CommandHolder cmdHolder;
        private C1.Win.C1Command.C1DockingTab dockMain;
        private C1.Win.C1Command.C1CommandDock dockBottom;
        private C1.Win.C1Command.C1DockingTab dockTabOutput;
        private C1.Win.C1Command.C1DockingTabPage tpgLog;
        private System.Windows.Forms.TextBox textLog;
        private C1.Win.C1Command.C1CommandDock dockLeft;
        private C1.Win.C1Command.C1DockingTab dockTabScan;
        private C1.Win.C1Command.C1DockingTabPage tpgScanSettings;
        private C1.Win.C1Command.C1DockingTabPage tpgScanField;
        private System.Windows.Forms.StatusStrip statusStrip;
        private C1.Win.C1Command.C1CommandDock dockToolBar;
        private C1.Win.C1Command.C1ToolBar toolBar;
        private C1.Win.C1Command.C1CommandMenu cmdMenuFile;
        private C1.Win.C1Command.C1CommandLink cmdLinkFileNew;
        private C1.Win.C1Command.C1CommandLink cmdLinkFile;
        private C1.Win.C1Command.C1Command cmdFileNew;
        private C1.Win.C1Command.C1CommandLink cmdLindFileOpen;
        private C1.Win.C1Command.C1Command cmdFileOpen;
        private C1.Win.C1Command.C1CommandLink cmdLindFileSave;
        private C1.Win.C1Command.C1Command cmdFileSave;
        private C1.Win.C1Command.C1CommandLink cmdLindFileClose;
        private C1.Win.C1Command.C1Command cmdFileClose;
        private C1.Win.C1Command.C1CommandMenu cmdMenuView;
        private C1.Win.C1Command.C1CommandLink cmdLinkTheme;
        private C1.Win.C1Command.C1CommandLink cmdLinkView;
        private C1.Win.C1Command.C1Command cmdTheme;
    }
}