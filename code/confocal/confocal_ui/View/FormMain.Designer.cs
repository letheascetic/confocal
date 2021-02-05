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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.mainMenu = new C1.Win.C1Command.C1MainMenu();
            this.cmdHolder = new C1.Win.C1Command.C1CommandHolder();
            this.cmdFileNew = new C1.Win.C1Command.C1Command();
            this.cmdFileOpen = new C1.Win.C1Command.C1Command();
            this.cmdFileSave = new C1.Win.C1Command.C1Command();
            this.cmdFileClose = new C1.Win.C1Command.C1Command();
            this.cmdMenuFile = new C1.Win.C1Command.C1CommandMenu();
            this.cmdLinkFileNew = new C1.Win.C1Command.C1CommandLink();
            this.cmdLindFileOpen = new C1.Win.C1Command.C1CommandLink();
            this.cmdLindFileSave = new C1.Win.C1Command.C1CommandLink();
            this.cmdLindFileClose = new C1.Win.C1Command.C1CommandLink();
            this.cmdMenuView = new C1.Win.C1Command.C1CommandMenu();
            this.cmdLinkTheme = new C1.Win.C1Command.C1CommandLink();
            this.cmdTheme = new C1.Win.C1Command.C1Command();
            this.cmdMenuWindow = new C1.Win.C1Command.C1CommandMenu();
            this.cmdLinkScanArea = new C1.Win.C1Command.C1CommandLink();
            this.cmdScanArea = new C1.Win.C1Command.C1Command();
            this.cmdLinkScanSettings = new C1.Win.C1Command.C1CommandLink();
            this.cmdScanSettings = new C1.Win.C1Command.C1Command();
            this.cmdLinkScanImage = new C1.Win.C1Command.C1CommandLink();
            this.cmdScanImage = new C1.Win.C1Command.C1Command();
            this.cmdLinkCfg = new C1.Win.C1Command.C1CommandLink();
            this.cmdSysCfg = new C1.Win.C1Command.C1Command();
            this.cmdLinkScanParas = new C1.Win.C1Command.C1CommandLink();
            this.cmdScanParas = new C1.Win.C1Command.C1Command();
            this.cmdLinkHistogram = new C1.Win.C1Command.C1CommandLink();
            this.cmdHistogram = new C1.Win.C1Command.C1Command();
            this.cmdLinkFile = new C1.Win.C1Command.C1CommandLink();
            this.cmdLinkView = new C1.Win.C1Command.C1CommandLink();
            this.cmdLinkWindow = new C1.Win.C1Command.C1CommandLink();
            this.dockBottom = new C1.Win.C1Command.C1CommandDock();
            this.dockTabOutput = new C1.Win.C1Command.C1DockingTab();
            this.tpgLog = new C1.Win.C1Command.C1DockingTabPage();
            this.textLog = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.snapFormExtender = new SnapFormExtender.SnapFormExtender(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lbSelectLaser = new System.Windows.Forms.ToolStripLabel();
            this.cbxSelectLaser = new System.Windows.Forms.ToolStripComboBox();
            this.btnLaserConnect = new System.Windows.Forms.ToolStripButton();
            this.btnLaserRelease = new System.Windows.Forms.ToolStripButton();
            this.dockToolBar = new C1.Win.C1Command.C1CommandDock();
            this.toolBar = new C1.Win.C1Command.C1ToolBar();
            this.cmdLinkPI = new C1.Win.C1Command.C1CommandLink();
            this.cmdPI = new C1.Win.C1Command.C1Command();
            ((System.ComponentModel.ISupportInitialize)(this.cmdHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockBottom)).BeginInit();
            this.dockBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockTabOutput)).BeginInit();
            this.dockTabOutput.SuspendLayout();
            this.tpgLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.snapFormExtender)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockToolBar)).BeginInit();
            this.dockToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.AccessibleName = "Menu Bar";
            this.mainMenu.CommandHolder = this.cmdHolder;
            this.mainMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.cmdLinkFile,
            this.cmdLinkView,
            this.cmdLinkWindow});
            this.mainMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1192, 26);
            this.mainMenu.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.mainMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // cmdHolder
            // 
            this.cmdHolder.Commands.Add(this.cmdFileNew);
            this.cmdHolder.Commands.Add(this.cmdFileOpen);
            this.cmdHolder.Commands.Add(this.cmdFileSave);
            this.cmdHolder.Commands.Add(this.cmdFileClose);
            this.cmdHolder.Commands.Add(this.cmdMenuFile);
            this.cmdHolder.Commands.Add(this.cmdMenuView);
            this.cmdHolder.Commands.Add(this.cmdTheme);
            this.cmdHolder.Commands.Add(this.cmdMenuWindow);
            this.cmdHolder.Commands.Add(this.cmdScanArea);
            this.cmdHolder.Commands.Add(this.cmdScanSettings);
            this.cmdHolder.Commands.Add(this.cmdScanImage);
            this.cmdHolder.Commands.Add(this.cmdSysCfg);
            this.cmdHolder.Commands.Add(this.cmdScanParas);
            this.cmdHolder.Commands.Add(this.cmdHistogram);
            this.cmdHolder.Commands.Add(this.cmdPI);
            this.cmdHolder.Owner = this;
            // 
            // cmdFileNew
            // 
            this.cmdFileNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileNew.Image")));
            this.cmdFileNew.Name = "cmdFileNew";
            this.cmdFileNew.ShortcutText = "";
            this.cmdFileNew.Text = "新建（&N）";
            // 
            // cmdFileOpen
            // 
            this.cmdFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileOpen.Image")));
            this.cmdFileOpen.Name = "cmdFileOpen";
            this.cmdFileOpen.ShortcutText = "";
            this.cmdFileOpen.Text = "打开（&O）";
            // 
            // cmdFileSave
            // 
            this.cmdFileSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileSave.Image")));
            this.cmdFileSave.Name = "cmdFileSave";
            this.cmdFileSave.ShortcutText = "";
            this.cmdFileSave.Text = "保存（&S）";
            // 
            // cmdFileClose
            // 
            this.cmdFileClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdFileClose.Image")));
            this.cmdFileClose.Name = "cmdFileClose";
            this.cmdFileClose.ShortcutText = "";
            this.cmdFileClose.Text = "关闭（&C）";
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
            // cmdLindFileOpen
            // 
            this.cmdLindFileOpen.Command = this.cmdFileOpen;
            this.cmdLindFileOpen.SortOrder = 1;
            // 
            // cmdLindFileSave
            // 
            this.cmdLindFileSave.Command = this.cmdFileSave;
            this.cmdLindFileSave.SortOrder = 2;
            // 
            // cmdLindFileClose
            // 
            this.cmdLindFileClose.Command = this.cmdFileClose;
            this.cmdLindFileClose.SortOrder = 3;
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
            this.cmdTheme.Click += new C1.Win.C1Command.ClickEventHandler(this.ThemeClick);
            // 
            // cmdMenuWindow
            // 
            this.cmdMenuWindow.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.cmdLinkScanArea,
            this.cmdLinkScanSettings,
            this.cmdLinkScanImage,
            this.cmdLinkCfg,
            this.cmdLinkScanParas,
            this.cmdLinkHistogram,
            this.cmdLinkPI});
            this.cmdMenuWindow.HideNonRecentLinks = false;
            this.cmdMenuWindow.Name = "cmdMenuWindow";
            this.cmdMenuWindow.ShortcutText = "";
            this.cmdMenuWindow.Text = "窗口（&W）";
            // 
            // cmdLinkScanArea
            // 
            this.cmdLinkScanArea.Command = this.cmdScanArea;
            // 
            // cmdScanArea
            // 
            this.cmdScanArea.CheckAutoToggle = true;
            this.cmdScanArea.Name = "cmdScanArea";
            this.cmdScanArea.ShortcutText = "";
            this.cmdScanArea.Text = "扫描区域（&A）";
            this.cmdScanArea.Click += new C1.Win.C1Command.ClickEventHandler(this.ScanAreaClick);
            // 
            // cmdLinkScanSettings
            // 
            this.cmdLinkScanSettings.Command = this.cmdScanSettings;
            this.cmdLinkScanSettings.SortOrder = 1;
            // 
            // cmdScanSettings
            // 
            this.cmdScanSettings.CheckAutoToggle = true;
            this.cmdScanSettings.Name = "cmdScanSettings";
            this.cmdScanSettings.ShortcutText = "";
            this.cmdScanSettings.Text = "扫描设置（&S）";
            this.cmdScanSettings.Click += new C1.Win.C1Command.ClickEventHandler(this.ScanSettingsClick);
            // 
            // cmdLinkScanImage
            // 
            this.cmdLinkScanImage.Command = this.cmdScanImage;
            this.cmdLinkScanImage.SortOrder = 2;
            // 
            // cmdScanImage
            // 
            this.cmdScanImage.CheckAutoToggle = true;
            this.cmdScanImage.Name = "cmdScanImage";
            this.cmdScanImage.ShortcutText = "";
            this.cmdScanImage.Text = "扫描图像（&I）";
            this.cmdScanImage.Click += new C1.Win.C1Command.ClickEventHandler(this.ScanImageClick);
            // 
            // cmdLinkCfg
            // 
            this.cmdLinkCfg.Command = this.cmdSysCfg;
            this.cmdLinkCfg.SortOrder = 3;
            // 
            // cmdSysCfg
            // 
            this.cmdSysCfg.CheckAutoToggle = true;
            this.cmdSysCfg.Name = "cmdSysCfg";
            this.cmdSysCfg.ShortcutText = "";
            this.cmdSysCfg.Text = "系统设置（&C）";
            this.cmdSysCfg.Click += new C1.Win.C1Command.ClickEventHandler(this.SysSettingsClick);
            // 
            // cmdLinkScanParas
            // 
            this.cmdLinkScanParas.Command = this.cmdScanParas;
            this.cmdLinkScanParas.SortOrder = 4;
            // 
            // cmdScanParas
            // 
            this.cmdScanParas.CheckAutoToggle = true;
            this.cmdScanParas.Name = "cmdScanParas";
            this.cmdScanParas.ShortcutText = "";
            this.cmdScanParas.Text = "扫描参数（&P）";
            this.cmdScanParas.Click += new C1.Win.C1Command.ClickEventHandler(this.ScanParasClick);
            // 
            // cmdLinkHistogram
            // 
            this.cmdLinkHistogram.Command = this.cmdHistogram;
            this.cmdLinkHistogram.SortOrder = 5;
            // 
            // cmdHistogram
            // 
            this.cmdHistogram.Name = "cmdHistogram";
            this.cmdHistogram.ShortcutText = "";
            this.cmdHistogram.Text = "直方图分布（&H）";
            this.cmdHistogram.Click += new C1.Win.C1Command.ClickEventHandler(this.HistogramClick);
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
            // cmdLinkWindow
            // 
            this.cmdLinkWindow.Command = this.cmdMenuWindow;
            this.cmdLinkWindow.SortOrder = 2;
            // 
            // dockBottom
            // 
            this.dockBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.dockBottom.Controls.Add(this.dockTabOutput);
            this.dockBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockBottom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dockBottom.Id = 1;
            this.dockBottom.Location = new System.Drawing.Point(0, 607);
            this.dockBottom.Name = "dockBottom";
            this.dockBottom.Size = new System.Drawing.Size(1192, 138);
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
            this.dockTabOutput.Size = new System.Drawing.Size(1192, 138);
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
            this.tpgLog.Size = new System.Drawing.Size(1190, 111);
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
            this.textLog.Size = new System.Drawing.Size(1190, 88);
            this.textLog.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 745);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1192, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // snapFormExtender
            // 
            this.snapFormExtender.Distance = 10;
            this.snapFormExtender.Form = this;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbSelectLaser,
            this.cbxSelectLaser,
            this.btnLaserConnect,
            this.btnLaserRelease});
            this.toolStrip.Location = new System.Drawing.Point(0, 26);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1192, 25);
            this.toolStrip.TabIndex = 14;
            this.toolStrip.Text = "toolStrip1";
            // 
            // lbSelectLaser
            // 
            this.lbSelectLaser.Name = "lbSelectLaser";
            this.lbSelectLaser.Size = new System.Drawing.Size(59, 22);
            this.lbSelectLaser.Text = "激光端口:";
            // 
            // cbxSelectLaser
            // 
            this.cbxSelectLaser.BackColor = System.Drawing.SystemColors.Control;
            this.cbxSelectLaser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectLaser.Font = new System.Drawing.Font("微软雅黑", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxSelectLaser.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.cbxSelectLaser.Name = "cbxSelectLaser";
            this.cbxSelectLaser.Size = new System.Drawing.Size(75, 25);
            // 
            // btnLaserConnect
            // 
            this.btnLaserConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLaserConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnLaserConnect.Image")));
            this.btnLaserConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLaserConnect.Name = "btnLaserConnect";
            this.btnLaserConnect.Size = new System.Drawing.Size(23, 22);
            // 
            // btnLaserRelease
            // 
            this.btnLaserRelease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLaserRelease.Image = ((System.Drawing.Image)(resources.GetObject("btnLaserRelease.Image")));
            this.btnLaserRelease.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLaserRelease.Name = "btnLaserRelease";
            this.btnLaserRelease.Size = new System.Drawing.Size(23, 22);
            // 
            // dockToolBar
            // 
            this.dockToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dockToolBar.Controls.Add(this.toolBar);
            this.dockToolBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dockToolBar.Id = 3;
            this.dockToolBar.Location = new System.Drawing.Point(0, 51);
            this.dockToolBar.Name = "dockToolBar";
            this.dockToolBar.Size = new System.Drawing.Size(1192, 28);
            // 
            // toolBar
            // 
            this.toolBar.AccessibleName = "Tool Bar";
            this.toolBar.CommandHolder = this.cmdHolder;
            this.toolBar.Location = new System.Drawing.Point(3, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(25, 25);
            this.toolBar.Text = "工具栏";
            this.toolBar.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.toolBar.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // cmdLinkPI
            // 
            this.cmdLinkPI.Command = this.cmdPI;
            this.cmdLinkPI.SortOrder = 6;
            // 
            // cmdPI
            // 
            this.cmdPI.Name = "cmdPI";
            this.cmdPI.ShortcutText = "";
            this.cmdPI.Text = "压电平台（&P）";
            this.cmdPI.Click += new C1.Win.C1Command.ClickEventHandler(this.PIClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 767);
            this.Controls.Add(this.dockToolBar);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.dockBottom);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Z1 Confocal v";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Custom;
            this.Load += new System.EventHandler(this.FormMainLoad);
            ((System.ComponentModel.ISupportInitialize)(this.cmdHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockBottom)).EndInit();
            this.dockBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockTabOutput)).EndInit();
            this.dockTabOutput.ResumeLayout(false);
            this.tpgLog.ResumeLayout(false);
            this.tpgLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.snapFormExtender)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockToolBar)).EndInit();
            this.dockToolBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Command.C1MainMenu mainMenu;
        private C1.Win.C1Command.C1CommandHolder cmdHolder;
        private C1.Win.C1Command.C1CommandDock dockBottom;
        private C1.Win.C1Command.C1DockingTab dockTabOutput;
        private C1.Win.C1Command.C1DockingTabPage tpgLog;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.StatusStrip statusStrip;
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
        private SnapFormExtender.SnapFormExtender snapFormExtender;
        private C1.Win.C1Command.C1CommandMenu cmdMenuWindow;
        private C1.Win.C1Command.C1CommandLink cmdLinkWindow;
        private C1.Win.C1Command.C1CommandLink cmdLinkScanArea;
        private C1.Win.C1Command.C1Command cmdScanArea;
        private C1.Win.C1Command.C1CommandLink cmdLinkScanSettings;
        private C1.Win.C1Command.C1Command cmdScanSettings;
        private C1.Win.C1Command.C1CommandLink cmdLinkScanImage;
        private C1.Win.C1Command.C1Command cmdScanImage;
        private C1.Win.C1Command.C1CommandLink cmdLinkCfg;
        private C1.Win.C1Command.C1Command cmdSysCfg;
        private C1.Win.C1Command.C1CommandDock dockToolBar;
        private C1.Win.C1Command.C1ToolBar toolBar;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel lbSelectLaser;
        private System.Windows.Forms.ToolStripComboBox cbxSelectLaser;
        private System.Windows.Forms.ToolStripButton btnLaserConnect;
        private System.Windows.Forms.ToolStripButton btnLaserRelease;
        private C1.Win.C1Command.C1CommandLink cmdLinkScanParas;
        private C1.Win.C1Command.C1Command cmdScanParas;
        private C1.Win.C1Command.C1CommandLink cmdLinkHistogram;
        private C1.Win.C1Command.C1Command cmdHistogram;
        private C1.Win.C1Command.C1CommandLink cmdLinkPI;
        private C1.Win.C1Command.C1Command cmdPI;
    }
}