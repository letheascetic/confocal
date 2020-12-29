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
            this.inputPanel = new C1.Win.C1InputPanel.C1InputPanel();
            this.btnLive = new C1.Win.C1InputPanel.InputButton();
            this.btnCapture = new C1.Win.C1InputPanel.InputButton();
            this.rbtnTwoScanners = new C1.Win.C1InputPanel.InputRadioButton();
            this.rbtnThreeScanners = new C1.Win.C1InputPanel.InputRadioButton();
            this.rbtnGalvano = new C1.Win.C1InputPanel.InputRadioButton();
            this.rbtnResonant = new C1.Win.C1InputPanel.InputRadioButton();
            this.separator1 = new C1.Win.C1InputPanel.InputSeparator();
            this.lbPixelDwell = new C1.Win.C1InputPanel.InputLabel();
            this.rbtnFastMode = new C1.Win.C1InputPanel.InputButton();
            this.btnPixelDwell2 = new C1.Win.C1InputPanel.InputButton();
            this.btnPixelDwell4 = new C1.Win.C1InputPanel.InputButton();
            this.btnPixelDwell6 = new C1.Win.C1InputPanel.InputButton();
            this.btnPixelDwell8 = new C1.Win.C1InputPanel.InputButton();
            this.btnPixelDwell10 = new C1.Win.C1InputPanel.InputButton();
            this.btnPixelDwell20 = new C1.Win.C1InputPanel.InputButton();
            this.btnPixelDwell50 = new C1.Win.C1InputPanel.InputButton();
            this.btnPixelDwell100 = new C1.Win.C1InputPanel.InputButton();
            this.separator2 = new C1.Win.C1InputPanel.InputSeparator();
            this.lbScanPixel = new C1.Win.C1InputPanel.InputLabel();
            this.btnScanPixel64 = new C1.Win.C1InputPanel.InputButton();
            this.btnScanPixel128 = new C1.Win.C1InputPanel.InputButton();
            this.btnScanPixel256 = new C1.Win.C1InputPanel.InputButton();
            this.btnScanPixel512 = new C1.Win.C1InputPanel.InputButton();
            this.btnScanPixel1024 = new C1.Win.C1InputPanel.InputButton();
            this.btnScanPixel2048 = new C1.Win.C1InputPanel.InputButton();
            this.btnScanPixel4096 = new C1.Win.C1InputPanel.InputButton();
            this.separator3 = new C1.Win.C1InputPanel.InputSeparator();
            this.btnLineOptionNormal = new C1.Win.C1InputPanel.InputButton();
            this.btnAveraging = new C1.Win.C1InputPanel.InputSplitButton();
            this.btnIntegrate = new C1.Win.C1InputPanel.InputSplitButton();
            this.chbxSequence = new C1.Win.C1InputPanel.InputCheckBox();
            this.btnSequence = new C1.Win.C1InputPanel.InputSplitButton();
            this.lbFrame = new C1.Win.C1InputPanel.InputLabel();
            this.lbFrameTime = new C1.Win.C1InputPanel.InputLabel();
            this.separator4 = new C1.Win.C1InputPanel.InputSeparator();
            this.lbPinHoleSelect = new C1.Win.C1InputPanel.InputLabel();
            this.cbxPinHoleSelect = new C1.Win.C1InputPanel.InputComboBox();
            this.lbPinHole = new C1.Win.C1InputPanel.InputLabel();
            this.tbarPinHole = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbxPinHole = new C1.Win.C1InputPanel.InputTextBox();
            this.tbxPinHoleAU = new C1.Win.C1InputPanel.InputTextBox();
            this.separator5 = new C1.Win.C1InputPanel.InputSeparator();
            this.gh405 = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lb405HV = new C1.Win.C1InputPanel.InputLabel();
            this.tbar405HV = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx405HV = new C1.Win.C1InputPanel.InputTextBox();
            this.lb405Offset = new C1.Win.C1InputPanel.InputLabel();
            this.tbar405Offset = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx405Offset = new C1.Win.C1InputPanel.InputTextBox();
            this.btn405Power = new C1.Win.C1InputPanel.InputButton();
            this.tbar405Power = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx405Power = new C1.Win.C1InputPanel.InputTextBox();
            this.gh488 = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lb488HV = new C1.Win.C1InputPanel.InputLabel();
            this.tbar488HV = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx488HV = new C1.Win.C1InputPanel.InputTextBox();
            this.lb488Offset = new C1.Win.C1InputPanel.InputLabel();
            this.tbar488Offset = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx488Offset = new C1.Win.C1InputPanel.InputTextBox();
            this.btn488Power = new C1.Win.C1InputPanel.InputButton();
            this.tbar488Power = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx488Power = new C1.Win.C1InputPanel.InputTextBox();
            this.gh561 = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lb561HV = new C1.Win.C1InputPanel.InputLabel();
            this.tbar561HV = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx561HV = new C1.Win.C1InputPanel.InputTextBox();
            this.lb561Offset = new C1.Win.C1InputPanel.InputLabel();
            this.tbar561Offset = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx561Offset = new C1.Win.C1InputPanel.InputTextBox();
            this.btn561Power = new C1.Win.C1InputPanel.InputButton();
            this.tbar561Power = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx561Power = new C1.Win.C1InputPanel.InputTextBox();
            this.gh640 = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lb640HV = new C1.Win.C1InputPanel.InputLabel();
            this.tbar640HV = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx640HV = new C1.Win.C1InputPanel.InputTextBox();
            this.lb640Offset = new C1.Win.C1InputPanel.InputLabel();
            this.tbar640Offset = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx640Offset = new C1.Win.C1InputPanel.InputTextBox();
            this.btn640Power = new C1.Win.C1InputPanel.InputButton();
            this.tbar640Power = new C1.Win.C1InputPanel.InputTrackBar();
            this.tbx640Power = new C1.Win.C1InputPanel.InputTextBox();
            this.tpgScanField = new C1.Win.C1Command.C1DockingTabPage();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnUniDirection = new C1.Win.C1InputPanel.InputButton();
            this.btnBiDirection = new C1.Win.C1InputPanel.InputButton();
            this.separator6 = new C1.Win.C1InputPanel.InputSeparator();
            this.btnLineSkip = new C1.Win.C1InputPanel.InputSplitButton();
            this.chbxLineSkip = new C1.Win.C1InputPanel.InputCheckBox();
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
            this.tpgScanSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).BeginInit();
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
            this.mainMenu.Size = new System.Drawing.Size(992, 26);
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
            this.dockToolBar.Size = new System.Drawing.Size(992, 26);
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
            // dockMain
            // 
            this.dockMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockMain.CanCloseTabs = true;
            this.dockMain.CanMoveTabs = true;
            this.dockMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockMain.HotTrack = true;
            this.dockMain.Location = new System.Drawing.Point(0, 52);
            this.dockMain.Name = "dockMain";
            this.dockMain.Size = new System.Drawing.Size(729, 655);
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
            this.dockBottom.Location = new System.Drawing.Point(0, 707);
            this.dockBottom.Name = "dockBottom";
            this.dockBottom.Size = new System.Drawing.Size(729, 138);
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
            this.dockTabOutput.Size = new System.Drawing.Size(729, 138);
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
            this.tpgLog.Size = new System.Drawing.Size(727, 111);
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
            this.textLog.Size = new System.Drawing.Size(727, 88);
            this.textLog.TabIndex = 0;
            // 
            // dockLeft
            // 
            this.dockLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.dockLeft.Controls.Add(this.dockTabScan);
            this.dockLeft.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dockLeft.Id = 3;
            this.dockLeft.Location = new System.Drawing.Point(729, 52);
            this.dockLeft.Name = "dockLeft";
            this.dockLeft.Size = new System.Drawing.Size(263, 793);
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
            this.dockTabScan.Size = new System.Drawing.Size(263, 793);
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
            this.tpgScanSettings.Controls.Add(this.inputPanel);
            this.tpgScanSettings.Location = new System.Drawing.Point(4, 1);
            this.tpgScanSettings.Name = "tpgScanSettings";
            this.tpgScanSettings.Size = new System.Drawing.Size(258, 767);
            this.tpgScanSettings.TabIndex = 0;
            this.tpgScanSettings.Text = "扫描设置";
            // 
            // inputPanel
            // 
            this.inputPanel.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.inputPanel.Items.Add(this.btnLive);
            this.inputPanel.Items.Add(this.btnCapture);
            this.inputPanel.Items.Add(this.rbtnTwoScanners);
            this.inputPanel.Items.Add(this.rbtnThreeScanners);
            this.inputPanel.Items.Add(this.rbtnGalvano);
            this.inputPanel.Items.Add(this.rbtnResonant);
            this.inputPanel.Items.Add(this.separator6);
            this.inputPanel.Items.Add(this.btnUniDirection);
            this.inputPanel.Items.Add(this.btnBiDirection);
            this.inputPanel.Items.Add(this.chbxLineSkip);
            this.inputPanel.Items.Add(this.btnLineSkip);
            this.inputPanel.Items.Add(this.separator1);
            this.inputPanel.Items.Add(this.lbPixelDwell);
            this.inputPanel.Items.Add(this.rbtnFastMode);
            this.inputPanel.Items.Add(this.btnPixelDwell2);
            this.inputPanel.Items.Add(this.btnPixelDwell4);
            this.inputPanel.Items.Add(this.btnPixelDwell6);
            this.inputPanel.Items.Add(this.btnPixelDwell8);
            this.inputPanel.Items.Add(this.btnPixelDwell10);
            this.inputPanel.Items.Add(this.btnPixelDwell20);
            this.inputPanel.Items.Add(this.btnPixelDwell50);
            this.inputPanel.Items.Add(this.btnPixelDwell100);
            this.inputPanel.Items.Add(this.separator2);
            this.inputPanel.Items.Add(this.lbScanPixel);
            this.inputPanel.Items.Add(this.btnScanPixel64);
            this.inputPanel.Items.Add(this.btnScanPixel128);
            this.inputPanel.Items.Add(this.btnScanPixel256);
            this.inputPanel.Items.Add(this.btnScanPixel512);
            this.inputPanel.Items.Add(this.btnScanPixel1024);
            this.inputPanel.Items.Add(this.btnScanPixel2048);
            this.inputPanel.Items.Add(this.btnScanPixel4096);
            this.inputPanel.Items.Add(this.separator3);
            this.inputPanel.Items.Add(this.btnLineOptionNormal);
            this.inputPanel.Items.Add(this.btnAveraging);
            this.inputPanel.Items.Add(this.btnIntegrate);
            this.inputPanel.Items.Add(this.chbxSequence);
            this.inputPanel.Items.Add(this.btnSequence);
            this.inputPanel.Items.Add(this.lbFrame);
            this.inputPanel.Items.Add(this.lbFrameTime);
            this.inputPanel.Items.Add(this.separator4);
            this.inputPanel.Items.Add(this.lbPinHoleSelect);
            this.inputPanel.Items.Add(this.cbxPinHoleSelect);
            this.inputPanel.Items.Add(this.lbPinHole);
            this.inputPanel.Items.Add(this.tbarPinHole);
            this.inputPanel.Items.Add(this.tbxPinHole);
            this.inputPanel.Items.Add(this.tbxPinHoleAU);
            this.inputPanel.Items.Add(this.separator5);
            this.inputPanel.Items.Add(this.gh405);
            this.inputPanel.Items.Add(this.lb405HV);
            this.inputPanel.Items.Add(this.tbar405HV);
            this.inputPanel.Items.Add(this.tbx405HV);
            this.inputPanel.Items.Add(this.lb405Offset);
            this.inputPanel.Items.Add(this.tbar405Offset);
            this.inputPanel.Items.Add(this.tbx405Offset);
            this.inputPanel.Items.Add(this.btn405Power);
            this.inputPanel.Items.Add(this.tbar405Power);
            this.inputPanel.Items.Add(this.tbx405Power);
            this.inputPanel.Items.Add(this.gh488);
            this.inputPanel.Items.Add(this.lb488HV);
            this.inputPanel.Items.Add(this.tbar488HV);
            this.inputPanel.Items.Add(this.tbx488HV);
            this.inputPanel.Items.Add(this.lb488Offset);
            this.inputPanel.Items.Add(this.tbar488Offset);
            this.inputPanel.Items.Add(this.tbx488Offset);
            this.inputPanel.Items.Add(this.btn488Power);
            this.inputPanel.Items.Add(this.tbar488Power);
            this.inputPanel.Items.Add(this.tbx488Power);
            this.inputPanel.Items.Add(this.gh561);
            this.inputPanel.Items.Add(this.lb561HV);
            this.inputPanel.Items.Add(this.tbar561HV);
            this.inputPanel.Items.Add(this.tbx561HV);
            this.inputPanel.Items.Add(this.lb561Offset);
            this.inputPanel.Items.Add(this.tbar561Offset);
            this.inputPanel.Items.Add(this.tbx561Offset);
            this.inputPanel.Items.Add(this.btn561Power);
            this.inputPanel.Items.Add(this.tbar561Power);
            this.inputPanel.Items.Add(this.tbx561Power);
            this.inputPanel.Items.Add(this.gh640);
            this.inputPanel.Items.Add(this.lb640HV);
            this.inputPanel.Items.Add(this.tbar640HV);
            this.inputPanel.Items.Add(this.tbx640HV);
            this.inputPanel.Items.Add(this.lb640Offset);
            this.inputPanel.Items.Add(this.tbar640Offset);
            this.inputPanel.Items.Add(this.tbx640Offset);
            this.inputPanel.Items.Add(this.btn640Power);
            this.inputPanel.Items.Add(this.tbar640Power);
            this.inputPanel.Items.Add(this.tbx640Power);
            this.inputPanel.Location = new System.Drawing.Point(0, 23);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(258, 744);
            this.inputPanel.TabIndex = 2;
            // 
            // btnLive
            // 
            this.btnLive.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnLive.CheckOnClick = true;
            this.btnLive.Height = 40;
            this.btnLive.Name = "btnLive";
            this.btnLive.Text = "实时";
            this.btnLive.Width = 50;
            // 
            // btnCapture
            // 
            this.btnCapture.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.btnCapture.CheckOnClick = true;
            this.btnCapture.Height = 40;
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Text = "捕捉";
            this.btnCapture.Width = 50;
            // 
            // rbtnTwoScanners
            // 
            this.rbtnTwoScanners.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnTwoScanners.GroupName = "Scanners";
            this.rbtnTwoScanners.Name = "rbtnTwoScanners";
            this.rbtnTwoScanners.Text = "双镜";
            // 
            // rbtnThreeScanners
            // 
            this.rbtnThreeScanners.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.rbtnThreeScanners.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnThreeScanners.GroupName = "Scanners";
            this.rbtnThreeScanners.Name = "rbtnThreeScanners";
            this.rbtnThreeScanners.Text = "三镜";
            // 
            // rbtnGalvano
            // 
            this.rbtnGalvano.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnGalvano.GroupName = "ScanMode";
            this.rbtnGalvano.Name = "rbtnGalvano";
            this.rbtnGalvano.Text = "Galvano";
            this.rbtnGalvano.Width = 60;
            // 
            // rbtnResonant
            // 
            this.rbtnResonant.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.rbtnResonant.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnResonant.GroupName = "ScanMode";
            this.rbtnResonant.Name = "rbtnResonant";
            this.rbtnResonant.Text = "Resonant";
            // 
            // separator1
            // 
            this.separator1.Height = 10;
            this.separator1.Name = "separator1";
            this.separator1.Width = 240;
            // 
            // lbPixelDwell
            // 
            this.lbPixelDwell.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbPixelDwell.Height = 20;
            this.lbPixelDwell.Name = "lbPixelDwell";
            this.lbPixelDwell.Text = "像素时间（us）";
            // 
            // rbtnFastMode
            // 
            this.rbtnFastMode.Break = C1.Win.C1InputPanel.BreakType.None;
            this.rbtnFastMode.CheckOnClick = true;
            this.rbtnFastMode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnFastMode.Name = "rbtnFastMode";
            this.rbtnFastMode.Text = "快速模式";
            // 
            // btnPixelDwell2
            // 
            this.btnPixelDwell2.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnPixelDwell2.CheckOnClick = true;
            this.btnPixelDwell2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPixelDwell2.Name = "btnPixelDwell2";
            this.btnPixelDwell2.Text = "2";
            // 
            // btnPixelDwell4
            // 
            this.btnPixelDwell4.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnPixelDwell4.CheckOnClick = true;
            this.btnPixelDwell4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPixelDwell4.Name = "btnPixelDwell4";
            this.btnPixelDwell4.Text = "4";
            // 
            // btnPixelDwell6
            // 
            this.btnPixelDwell6.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnPixelDwell6.CheckOnClick = true;
            this.btnPixelDwell6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPixelDwell6.Name = "btnPixelDwell6";
            this.btnPixelDwell6.Text = "6";
            // 
            // btnPixelDwell8
            // 
            this.btnPixelDwell8.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnPixelDwell8.CheckOnClick = true;
            this.btnPixelDwell8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPixelDwell8.Name = "btnPixelDwell8";
            this.btnPixelDwell8.Text = "8";
            // 
            // btnPixelDwell10
            // 
            this.btnPixelDwell10.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnPixelDwell10.CheckOnClick = true;
            this.btnPixelDwell10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPixelDwell10.Name = "btnPixelDwell10";
            this.btnPixelDwell10.Text = "10";
            // 
            // btnPixelDwell20
            // 
            this.btnPixelDwell20.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnPixelDwell20.CheckOnClick = true;
            this.btnPixelDwell20.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPixelDwell20.Name = "btnPixelDwell20";
            this.btnPixelDwell20.Text = "20";
            // 
            // btnPixelDwell50
            // 
            this.btnPixelDwell50.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnPixelDwell50.CheckOnClick = true;
            this.btnPixelDwell50.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPixelDwell50.Name = "btnPixelDwell50";
            this.btnPixelDwell50.Text = "50";
            // 
            // btnPixelDwell100
            // 
            this.btnPixelDwell100.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.btnPixelDwell100.CheckOnClick = true;
            this.btnPixelDwell100.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPixelDwell100.Name = "btnPixelDwell100";
            this.btnPixelDwell100.Text = "100";
            // 
            // separator2
            // 
            this.separator2.Height = 10;
            this.separator2.Name = "separator2";
            this.separator2.Width = 240;
            // 
            // lbScanPixel
            // 
            this.lbScanPixel.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbScanPixel.Height = 20;
            this.lbScanPixel.Name = "lbScanPixel";
            this.lbScanPixel.Text = "扫描像素";
            // 
            // btnScanPixel64
            // 
            this.btnScanPixel64.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnScanPixel64.CheckOnClick = true;
            this.btnScanPixel64.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScanPixel64.Name = "btnScanPixel64";
            this.btnScanPixel64.Text = "64 ";
            // 
            // btnScanPixel128
            // 
            this.btnScanPixel128.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnScanPixel128.CheckOnClick = true;
            this.btnScanPixel128.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScanPixel128.Name = "btnScanPixel128";
            this.btnScanPixel128.Text = "128";
            // 
            // btnScanPixel256
            // 
            this.btnScanPixel256.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnScanPixel256.CheckOnClick = true;
            this.btnScanPixel256.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScanPixel256.Name = "btnScanPixel256";
            this.btnScanPixel256.Text = "256";
            // 
            // btnScanPixel512
            // 
            this.btnScanPixel512.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnScanPixel512.CheckOnClick = true;
            this.btnScanPixel512.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScanPixel512.Name = "btnScanPixel512";
            this.btnScanPixel512.Text = "512";
            // 
            // btnScanPixel1024
            // 
            this.btnScanPixel1024.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnScanPixel1024.CheckOnClick = true;
            this.btnScanPixel1024.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScanPixel1024.Name = "btnScanPixel1024";
            this.btnScanPixel1024.Text = "1024";
            // 
            // btnScanPixel2048
            // 
            this.btnScanPixel2048.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnScanPixel2048.CheckOnClick = true;
            this.btnScanPixel2048.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScanPixel2048.Name = "btnScanPixel2048";
            this.btnScanPixel2048.Text = "2048";
            // 
            // btnScanPixel4096
            // 
            this.btnScanPixel4096.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.btnScanPixel4096.CheckOnClick = true;
            this.btnScanPixel4096.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnScanPixel4096.Name = "btnScanPixel4096";
            this.btnScanPixel4096.Text = "4096";
            // 
            // separator3
            // 
            this.separator3.Height = 10;
            this.separator3.Name = "separator3";
            this.separator3.Width = 240;
            // 
            // btnLineOptionNormal
            // 
            this.btnLineOptionNormal.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnLineOptionNormal.CheckOnClick = true;
            this.btnLineOptionNormal.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLineOptionNormal.Name = "btnLineOptionNormal";
            this.btnLineOptionNormal.Text = "标准";
            // 
            // btnAveraging
            // 
            this.btnAveraging.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnAveraging.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAveraging.Name = "btnAveraging";
            this.btnAveraging.Text = "平均";
            // 
            // btnIntegrate
            // 
            this.btnIntegrate.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnIntegrate.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnIntegrate.Name = "btnIntegrate";
            this.btnIntegrate.Text = "积分";
            // 
            // chbxSequence
            // 
            this.chbxSequence.Break = C1.Win.C1InputPanel.BreakType.None;
            this.chbxSequence.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbxSequence.Name = "chbxSequence";
            this.chbxSequence.Text = "扫描序列";
            // 
            // btnSequence
            // 
            this.btnSequence.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.btnSequence.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSequence.Name = "btnSequence";
            this.btnSequence.Text = "序列";
            // 
            // lbFrame
            // 
            this.lbFrame.Name = "lbFrame";
            this.lbFrame.Text = "帧率：";
            this.lbFrame.Width = 80;
            // 
            // lbFrameTime
            // 
            this.lbFrameTime.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbFrameTime.Name = "lbFrameTime";
            this.lbFrameTime.Text = "帧时间：";
            this.lbFrameTime.Width = 80;
            // 
            // separator4
            // 
            this.separator4.Height = 10;
            this.separator4.Name = "separator4";
            this.separator4.Width = 240;
            // 
            // lbPinHoleSelect
            // 
            this.lbPinHoleSelect.Name = "lbPinHoleSelect";
            this.lbPinHoleSelect.Text = "选择小孔：";
            // 
            // cbxPinHoleSelect
            // 
            this.cbxPinHoleSelect.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbxPinHoleSelect.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxPinHoleSelect.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxPinHoleSelect.Name = "cbxPinHoleSelect";
            this.cbxPinHoleSelect.Width = 44;
            // 
            // lbPinHole
            // 
            this.lbPinHole.Name = "lbPinHole";
            this.lbPinHole.Text = "孔径：";
            // 
            // tbarPinHole
            // 
            this.tbarPinHole.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbarPinHole.Maximum = 100;
            this.tbarPinHole.Name = "tbarPinHole";
            this.tbarPinHole.TickFrequency = 10;
            // 
            // tbxPinHole
            // 
            this.tbxPinHole.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbxPinHole.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxPinHole.Name = "tbxPinHole";
            this.tbxPinHole.Width = 30;
            // 
            // tbxPinHoleAU
            // 
            this.tbxPinHoleAU.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbxPinHoleAU.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxPinHoleAU.Name = "tbxPinHoleAU";
            this.tbxPinHoleAU.Width = 30;
            // 
            // separator5
            // 
            this.separator5.Height = 10;
            this.separator5.Name = "separator5";
            this.separator5.Width = 240;
            // 
            // gh405
            // 
            this.gh405.Collapsible = true;
            this.gh405.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gh405.FontPadding = true;
            this.gh405.Name = "gh405";
            this.gh405.Text = "405nm                                  0.0";
            // 
            // lb405HV
            // 
            this.lb405HV.Name = "lb405HV";
            this.lb405HV.Text = "增益 ";
            // 
            // tbar405HV
            // 
            this.tbar405HV.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar405HV.Maximum = 100;
            this.tbar405HV.Name = "tbar405HV";
            this.tbar405HV.TickFrequency = 10;
            this.tbar405HV.Width = 160;
            // 
            // tbx405HV
            // 
            this.tbx405HV.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx405HV.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx405HV.Name = "tbx405HV";
            this.tbx405HV.Width = 40;
            // 
            // lb405Offset
            // 
            this.lb405Offset.Name = "lb405Offset";
            this.lb405Offset.Text = "偏置 ";
            // 
            // tbar405Offset
            // 
            this.tbar405Offset.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar405Offset.Maximum = 128;
            this.tbar405Offset.Minimum = -128;
            this.tbar405Offset.Name = "tbar405Offset";
            this.tbar405Offset.TickFrequency = 16;
            this.tbar405Offset.Width = 160;
            // 
            // tbx405Offset
            // 
            this.tbx405Offset.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx405Offset.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx405Offset.Name = "tbx405Offset";
            this.tbx405Offset.Width = 40;
            // 
            // btn405Power
            // 
            this.btn405Power.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btn405Power.CheckOnClick = true;
            this.btn405Power.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn405Power.Name = "btn405Power";
            this.btn405Power.Text = "功率";
            // 
            // tbar405Power
            // 
            this.tbar405Power.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar405Power.Maximum = 100;
            this.tbar405Power.Name = "tbar405Power";
            this.tbar405Power.TickFrequency = 10;
            this.tbar405Power.Width = 160;
            // 
            // tbx405Power
            // 
            this.tbx405Power.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx405Power.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx405Power.Name = "tbx405Power";
            this.tbx405Power.Width = 40;
            // 
            // gh488
            // 
            this.gh488.Collapsible = true;
            this.gh488.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gh488.FontPadding = true;
            this.gh488.Name = "gh488";
            this.gh488.Text = "488nm                                  0.0";
            // 
            // lb488HV
            // 
            this.lb488HV.Name = "lb488HV";
            this.lb488HV.Text = "增益 ";
            // 
            // tbar488HV
            // 
            this.tbar488HV.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar488HV.Maximum = 100;
            this.tbar488HV.Name = "tbar488HV";
            this.tbar488HV.TickFrequency = 10;
            this.tbar488HV.Width = 160;
            // 
            // tbx488HV
            // 
            this.tbx488HV.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx488HV.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx488HV.Name = "tbx488HV";
            this.tbx488HV.Width = 40;
            // 
            // lb488Offset
            // 
            this.lb488Offset.Name = "lb488Offset";
            this.lb488Offset.Text = "偏置 ";
            // 
            // tbar488Offset
            // 
            this.tbar488Offset.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar488Offset.Maximum = 128;
            this.tbar488Offset.Minimum = -128;
            this.tbar488Offset.Name = "tbar488Offset";
            this.tbar488Offset.TickFrequency = 16;
            this.tbar488Offset.Width = 160;
            // 
            // tbx488Offset
            // 
            this.tbx488Offset.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx488Offset.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx488Offset.Name = "tbx488Offset";
            this.tbx488Offset.Width = 40;
            // 
            // btn488Power
            // 
            this.btn488Power.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btn488Power.CheckOnClick = true;
            this.btn488Power.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn488Power.Name = "btn488Power";
            this.btn488Power.Text = "功率";
            // 
            // tbar488Power
            // 
            this.tbar488Power.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar488Power.Maximum = 100;
            this.tbar488Power.Name = "tbar488Power";
            this.tbar488Power.TickFrequency = 10;
            this.tbar488Power.Width = 160;
            // 
            // tbx488Power
            // 
            this.tbx488Power.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx488Power.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx488Power.Name = "tbx488Power";
            this.tbx488Power.Width = 40;
            // 
            // gh561
            // 
            this.gh561.Collapsible = true;
            this.gh561.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gh561.FontPadding = true;
            this.gh561.Name = "gh561";
            this.gh561.Text = "561nm                                  0.0";
            // 
            // lb561HV
            // 
            this.lb561HV.Name = "lb561HV";
            this.lb561HV.Text = "增益 ";
            // 
            // tbar561HV
            // 
            this.tbar561HV.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar561HV.Maximum = 100;
            this.tbar561HV.Name = "tbar561HV";
            this.tbar561HV.TickFrequency = 10;
            this.tbar561HV.Width = 160;
            // 
            // tbx561HV
            // 
            this.tbx561HV.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx561HV.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx561HV.Name = "tbx561HV";
            this.tbx561HV.Width = 40;
            // 
            // lb561Offset
            // 
            this.lb561Offset.Name = "lb561Offset";
            this.lb561Offset.Text = "偏置 ";
            // 
            // tbar561Offset
            // 
            this.tbar561Offset.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar561Offset.Maximum = 128;
            this.tbar561Offset.Minimum = -128;
            this.tbar561Offset.Name = "tbar561Offset";
            this.tbar561Offset.TickFrequency = 16;
            this.tbar561Offset.Width = 160;
            // 
            // tbx561Offset
            // 
            this.tbx561Offset.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx561Offset.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx561Offset.Name = "tbx561Offset";
            this.tbx561Offset.Width = 40;
            // 
            // btn561Power
            // 
            this.btn561Power.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btn561Power.CheckOnClick = true;
            this.btn561Power.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn561Power.Name = "btn561Power";
            this.btn561Power.Text = "功率";
            // 
            // tbar561Power
            // 
            this.tbar561Power.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar561Power.Maximum = 100;
            this.tbar561Power.Name = "tbar561Power";
            this.tbar561Power.TickFrequency = 10;
            this.tbar561Power.Width = 160;
            // 
            // tbx561Power
            // 
            this.tbx561Power.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx561Power.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx561Power.Name = "tbx561Power";
            this.tbx561Power.Width = 40;
            // 
            // gh640
            // 
            this.gh640.Collapsible = true;
            this.gh640.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gh640.FontPadding = true;
            this.gh640.Name = "gh640";
            this.gh640.Text = "640nm                                  0.0";
            // 
            // lb640HV
            // 
            this.lb640HV.Name = "lb640HV";
            this.lb640HV.Text = "增益 ";
            // 
            // tbar640HV
            // 
            this.tbar640HV.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar640HV.Maximum = 100;
            this.tbar640HV.Name = "tbar640HV";
            this.tbar640HV.TickFrequency = 10;
            this.tbar640HV.Width = 160;
            // 
            // tbx640HV
            // 
            this.tbx640HV.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx640HV.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx640HV.Name = "tbx640HV";
            this.tbx640HV.Width = 40;
            // 
            // lb640Offset
            // 
            this.lb640Offset.Name = "lb640Offset";
            this.lb640Offset.Text = "偏置 ";
            // 
            // tbar640Offset
            // 
            this.tbar640Offset.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar640Offset.Maximum = 128;
            this.tbar640Offset.Minimum = -128;
            this.tbar640Offset.Name = "tbar640Offset";
            this.tbar640Offset.TickFrequency = 16;
            this.tbar640Offset.Width = 160;
            // 
            // tbx640Offset
            // 
            this.tbx640Offset.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx640Offset.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx640Offset.Name = "tbx640Offset";
            this.tbx640Offset.Width = 40;
            // 
            // btn640Power
            // 
            this.btn640Power.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btn640Power.CheckOnClick = true;
            this.btn640Power.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn640Power.Name = "btn640Power";
            this.btn640Power.Text = "功率";
            // 
            // tbar640Power
            // 
            this.tbar640Power.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbar640Power.Maximum = 100;
            this.tbar640Power.Name = "tbar640Power";
            this.tbar640Power.TickFrequency = 10;
            this.tbar640Power.Width = 160;
            // 
            // tbx640Power
            // 
            this.tbx640Power.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbx640Power.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx640Power.Name = "tbx640Power";
            this.tbx640Power.Width = 40;
            // 
            // tpgScanField
            // 
            this.tpgScanField.CaptionVisible = true;
            this.tpgScanField.Location = new System.Drawing.Point(4, 1);
            this.tpgScanField.Name = "tpgScanField";
            this.tpgScanField.Size = new System.Drawing.Size(258, 767);
            this.tpgScanField.TabIndex = 1;
            this.tpgScanField.Text = "扫描区域";
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.statusStrip.Location = new System.Drawing.Point(0, 845);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(992, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // btnUniDirection
            // 
            this.btnUniDirection.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnUniDirection.CheckOnClick = true;
            this.btnUniDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnUniDirection.Name = "btnUniDirection";
            this.btnUniDirection.Text = "单向";
            // 
            // btnBiDirection
            // 
            this.btnBiDirection.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.btnBiDirection.CheckOnClick = true;
            this.btnBiDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnBiDirection.Name = "btnBiDirection";
            this.btnBiDirection.Text = "双向";
            // 
            // separator6
            // 
            this.separator6.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.separator6.Height = 10;
            this.separator6.Name = "separator6";
            this.separator6.Width = 240;
            // 
            // btnLineSkip
            // 
            this.btnLineSkip.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.btnLineSkip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnLineSkip.Name = "btnLineSkip";
            this.btnLineSkip.Text = "2x";
            this.btnLineSkip.Width = 40;
            // 
            // chbxLineSkip
            // 
            this.chbxLineSkip.Break = C1.Win.C1InputPanel.BreakType.None;
            this.chbxLineSkip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chbxLineSkip.Name = "chbxLineSkip";
            this.chbxLineSkip.Text = "跳行扫描";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 867);
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
            this.tpgScanSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).EndInit();
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
        private C1.Win.C1Command.C1DockingTabPage tpgScanSettings;
        private C1.Win.C1InputPanel.C1InputPanel inputPanel;
        private C1.Win.C1InputPanel.InputButton btnLive;
        private C1.Win.C1InputPanel.InputButton btnCapture;
        private C1.Win.C1InputPanel.InputRadioButton rbtnGalvano;
        private C1.Win.C1InputPanel.InputRadioButton rbtnResonant;
        private C1.Win.C1InputPanel.InputRadioButton rbtnTwoScanners;
        private C1.Win.C1InputPanel.InputRadioButton rbtnThreeScanners;
        private C1.Win.C1InputPanel.InputButton btnPixelDwell2;
        private C1.Win.C1InputPanel.InputButton btnPixelDwell4;
        private C1.Win.C1InputPanel.InputButton btnPixelDwell6;
        private C1.Win.C1InputPanel.InputButton btnPixelDwell8;
        private C1.Win.C1InputPanel.InputButton btnPixelDwell10;
        private C1.Win.C1InputPanel.InputButton btnPixelDwell20;
        private C1.Win.C1InputPanel.InputButton btnPixelDwell50;
        private C1.Win.C1InputPanel.InputButton btnPixelDwell100;
        private C1.Win.C1InputPanel.InputButton btnScanPixel64;
        private C1.Win.C1InputPanel.InputButton btnScanPixel128;
        private C1.Win.C1InputPanel.InputButton btnScanPixel256;
        private C1.Win.C1InputPanel.InputButton btnScanPixel512;
        private C1.Win.C1InputPanel.InputButton btnScanPixel1024;
        private C1.Win.C1InputPanel.InputButton btnScanPixel2048;
        private C1.Win.C1InputPanel.InputButton btnScanPixel4096;
        private C1.Win.C1InputPanel.InputButton rbtnFastMode;
        private C1.Win.C1InputPanel.InputSeparator separator2;
        private C1.Win.C1InputPanel.InputSeparator separator1;
        private C1.Win.C1InputPanel.InputLabel lbPixelDwell;
        private C1.Win.C1InputPanel.InputLabel lbScanPixel;
        private C1.Win.C1InputPanel.InputSeparator separator3;
        private C1.Win.C1InputPanel.InputButton btnLineOptionNormal;
        private C1.Win.C1InputPanel.InputSplitButton btnAveraging;
        private C1.Win.C1InputPanel.InputSplitButton btnIntegrate;
        private C1.Win.C1InputPanel.InputCheckBox chbxSequence;
        private C1.Win.C1InputPanel.InputSplitButton btnSequence;
        private C1.Win.C1InputPanel.InputSeparator separator4;
        private C1.Win.C1InputPanel.InputLabel lbFrameTime;
        private C1.Win.C1InputPanel.InputLabel lbFrame;
        private C1.Win.C1InputPanel.InputLabel lbPinHole;
        private C1.Win.C1InputPanel.InputTrackBar tbarPinHole;
        private C1.Win.C1InputPanel.InputTextBox tbxPinHole;
        private C1.Win.C1InputPanel.InputTextBox tbxPinHoleAU;
        private C1.Win.C1InputPanel.InputLabel lbPinHoleSelect;
        private C1.Win.C1InputPanel.InputComboBox cbxPinHoleSelect;
        private C1.Win.C1InputPanel.InputSeparator separator5;
        private C1.Win.C1InputPanel.InputGroupHeader gh405;
        private C1.Win.C1InputPanel.InputLabel lb405HV;
        private C1.Win.C1InputPanel.InputTrackBar tbar405HV;
        private C1.Win.C1InputPanel.InputTextBox tbx405HV;
        private C1.Win.C1InputPanel.InputLabel lb405Offset;
        private C1.Win.C1InputPanel.InputTrackBar tbar405Offset;
        private C1.Win.C1InputPanel.InputTextBox tbx405Offset;
        private C1.Win.C1InputPanel.InputButton btn405Power;
        private C1.Win.C1InputPanel.InputTrackBar tbar405Power;
        private C1.Win.C1InputPanel.InputTextBox tbx405Power;
        private C1.Win.C1InputPanel.InputGroupHeader gh488;
        private C1.Win.C1InputPanel.InputLabel lb488HV;
        private C1.Win.C1InputPanel.InputTrackBar tbar488HV;
        private C1.Win.C1InputPanel.InputTextBox tbx488HV;
        private C1.Win.C1InputPanel.InputLabel lb488Offset;
        private C1.Win.C1InputPanel.InputTrackBar tbar488Offset;
        private C1.Win.C1InputPanel.InputTextBox tbx488Offset;
        private C1.Win.C1InputPanel.InputButton btn488Power;
        private C1.Win.C1InputPanel.InputTrackBar tbar488Power;
        private C1.Win.C1InputPanel.InputTextBox tbx488Power;
        private C1.Win.C1InputPanel.InputGroupHeader gh561;
        private C1.Win.C1InputPanel.InputLabel lb561HV;
        private C1.Win.C1InputPanel.InputTrackBar tbar561HV;
        private C1.Win.C1InputPanel.InputTextBox tbx561HV;
        private C1.Win.C1InputPanel.InputLabel lb561Offset;
        private C1.Win.C1InputPanel.InputTrackBar tbar561Offset;
        private C1.Win.C1InputPanel.InputTextBox tbx561Offset;
        private C1.Win.C1InputPanel.InputButton btn561Power;
        private C1.Win.C1InputPanel.InputTrackBar tbar561Power;
        private C1.Win.C1InputPanel.InputTextBox tbx561Power;
        private C1.Win.C1InputPanel.InputGroupHeader gh640;
        private C1.Win.C1InputPanel.InputLabel lb640HV;
        private C1.Win.C1InputPanel.InputTrackBar tbar640HV;
        private C1.Win.C1InputPanel.InputTextBox tbx640HV;
        private C1.Win.C1InputPanel.InputLabel lb640Offset;
        private C1.Win.C1InputPanel.InputTrackBar tbar640Offset;
        private C1.Win.C1InputPanel.InputTextBox tbx640Offset;
        private C1.Win.C1InputPanel.InputButton btn640Power;
        private C1.Win.C1InputPanel.InputTrackBar tbar640Power;
        private C1.Win.C1InputPanel.InputTextBox tbx640Power;
        private C1.Win.C1InputPanel.InputButton btnUniDirection;
        private C1.Win.C1InputPanel.InputButton btnBiDirection;
        private C1.Win.C1InputPanel.InputSeparator separator6;
        private C1.Win.C1InputPanel.InputSplitButton btnLineSkip;
        private C1.Win.C1InputPanel.InputCheckBox chbxLineSkip;
    }
}