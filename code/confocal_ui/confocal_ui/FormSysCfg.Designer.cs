namespace confocal_ui
{
    partial class FormSysCfg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSysCfg));
            this.gbxGalv = new System.Windows.Forms.GroupBox();
            this.nudFieldSize = new System.Windows.Forms.NumericUpDown();
            this.nudResponseTime = new System.Windows.Forms.NumericUpDown();
            this.tbxCurveCoff = new System.Windows.Forms.TextBox();
            this.lbCurveCoff = new System.Windows.Forms.Label();
            this.tbxCalibrationV = new System.Windows.Forms.TextBox();
            this.lbCalibrationV = new System.Windows.Forms.Label();
            this.lbResponseTime = new System.Windows.Forms.Label();
            this.lbFieldSize = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxScan = new System.Windows.Forms.GroupBox();
            this.lbInfo = new System.Windows.Forms.Label();
            this.cbxY2Galvo = new System.Windows.Forms.ComboBox();
            this.cbxYGalvo = new System.Windows.Forms.ComboBox();
            this.cbxXGalvo = new System.Windows.Forms.ComboBox();
            this.lbY2Mirror = new System.Windows.Forms.Label();
            this.lbYMirror = new System.Windows.Forms.Label();
            this.lbXMirror = new System.Windows.Forms.Label();
            this.gbxAcqMode = new System.Windows.Forms.GroupBox();
            this.cbxStartSync = new System.Windows.Forms.ComboBox();
            this.cbxAcqTrigger = new System.Windows.Forms.ComboBox();
            this.lbAcqTrigger = new System.Windows.Forms.Label();
            this.lbStartSync = new System.Windows.Forms.Label();
            this.rbtnApd = new System.Windows.Forms.RadioButton();
            this.rbtnPmt = new System.Windows.Forms.RadioButton();
            this.panAcq = new System.Windows.Forms.Panel();
            this.tbcAcq = new System.Windows.Forms.TabControl();
            this.tpgApd = new System.Windows.Forms.TabPage();
            this.lbApdTriggerIn = new System.Windows.Forms.Label();
            this.cbxApdTriggerIn = new System.Windows.Forms.ComboBox();
            this.cbxApd640CiSrc = new System.Windows.Forms.ComboBox();
            this.cbxApd640Ci = new System.Windows.Forms.ComboBox();
            this.lbApd640 = new System.Windows.Forms.Label();
            this.cbxApd561CiSrc = new System.Windows.Forms.ComboBox();
            this.cbxApd561Ci = new System.Windows.Forms.ComboBox();
            this.lbApd561 = new System.Windows.Forms.Label();
            this.cbxApd488CiSrc = new System.Windows.Forms.ComboBox();
            this.cbxApd488Ci = new System.Windows.Forms.ComboBox();
            this.lbApd488 = new System.Windows.Forms.Label();
            this.lbCounterSrc = new System.Windows.Forms.Label();
            this.cbxApd405CiSrc = new System.Windows.Forms.ComboBox();
            this.cbxApd405Ci = new System.Windows.Forms.ComboBox();
            this.lbCounter = new System.Windows.Forms.Label();
            this.lbApd405 = new System.Windows.Forms.Label();
            this.tpgPmt = new System.Windows.Forms.TabPage();
            this.cbxPmtTriggerIn = new System.Windows.Forms.ComboBox();
            this.lbPmtTriggerIn = new System.Windows.Forms.Label();
            this.cbxPmt640Ai = new System.Windows.Forms.ComboBox();
            this.lbPmt640 = new System.Windows.Forms.Label();
            this.cbxPmt561Ai = new System.Windows.Forms.ComboBox();
            this.lbPmt561 = new System.Windows.Forms.Label();
            this.cbxPmt488Ai = new System.Windows.Forms.ComboBox();
            this.lbPmt488 = new System.Windows.Forms.Label();
            this.cbxPmt405Ai = new System.Windows.Forms.ComboBox();
            this.lbAi = new System.Windows.Forms.Label();
            this.lbPmt405 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gbxGalv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFieldSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResponseTime)).BeginInit();
            this.gbxScan.SuspendLayout();
            this.gbxAcqMode.SuspendLayout();
            this.panAcq.SuspendLayout();
            this.tbcAcq.SuspendLayout();
            this.tpgApd.SuspendLayout();
            this.tpgPmt.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxGalv
            // 
            this.gbxGalv.Controls.Add(this.nudFieldSize);
            this.gbxGalv.Controls.Add(this.nudResponseTime);
            this.gbxGalv.Controls.Add(this.tbxCurveCoff);
            this.gbxGalv.Controls.Add(this.lbCurveCoff);
            this.gbxGalv.Controls.Add(this.tbxCalibrationV);
            this.gbxGalv.Controls.Add(this.lbCalibrationV);
            this.gbxGalv.Controls.Add(this.lbResponseTime);
            this.gbxGalv.Controls.Add(this.lbFieldSize);
            this.gbxGalv.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gbxGalv.Location = new System.Drawing.Point(12, 135);
            this.gbxGalv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbxGalv.Name = "gbxGalv";
            this.gbxGalv.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbxGalv.Size = new System.Drawing.Size(485, 162);
            this.gbxGalv.TabIndex = 5;
            this.gbxGalv.TabStop = false;
            this.gbxGalv.Text = "扫描参数";
            // 
            // nudFieldSize
            // 
            this.nudFieldSize.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudFieldSize.Location = new System.Drawing.Point(203, 56);
            this.nudFieldSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudFieldSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFieldSize.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudFieldSize.Name = "nudFieldSize";
            this.nudFieldSize.Size = new System.Drawing.Size(93, 25);
            this.nudFieldSize.TabIndex = 14;
            this.nudFieldSize.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // nudResponseTime
            // 
            this.nudResponseTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudResponseTime.Location = new System.Drawing.Point(203, 21);
            this.nudResponseTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudResponseTime.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudResponseTime.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudResponseTime.Name = "nudResponseTime";
            this.nudResponseTime.Size = new System.Drawing.Size(93, 25);
            this.nudResponseTime.TabIndex = 13;
            this.nudResponseTime.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // tbxCurveCoff
            // 
            this.tbxCurveCoff.Location = new System.Drawing.Point(203, 128);
            this.tbxCurveCoff.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxCurveCoff.Name = "tbxCurveCoff";
            this.tbxCurveCoff.Size = new System.Drawing.Size(93, 25);
            this.tbxCurveCoff.TabIndex = 12;
            this.tbxCurveCoff.Text = "10";
            // 
            // lbCurveCoff
            // 
            this.lbCurveCoff.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbCurveCoff.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCurveCoff.Location = new System.Drawing.Point(27, 131);
            this.lbCurveCoff.Name = "lbCurveCoff";
            this.lbCurveCoff.Size = new System.Drawing.Size(160, 22);
            this.lbCurveCoff.TabIndex = 11;
            this.lbCurveCoff.Text = "曲线系数（%）：";
            // 
            // tbxCalibrationV
            // 
            this.tbxCalibrationV.Location = new System.Drawing.Point(203, 94);
            this.tbxCalibrationV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxCalibrationV.Name = "tbxCalibrationV";
            this.tbxCalibrationV.Size = new System.Drawing.Size(93, 25);
            this.tbxCalibrationV.TabIndex = 10;
            this.tbxCalibrationV.Text = "4.09855e-5";
            // 
            // lbCalibrationV
            // 
            this.lbCalibrationV.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbCalibrationV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCalibrationV.Location = new System.Drawing.Point(27, 98);
            this.lbCalibrationV.Name = "lbCalibrationV";
            this.lbCalibrationV.Size = new System.Drawing.Size(160, 22);
            this.lbCalibrationV.TabIndex = 9;
            this.lbCalibrationV.Text = "标定电压 (V)：";
            // 
            // lbResponseTime
            // 
            this.lbResponseTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbResponseTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbResponseTime.Location = new System.Drawing.Point(27, 24);
            this.lbResponseTime.Name = "lbResponseTime";
            this.lbResponseTime.Size = new System.Drawing.Size(160, 22);
            this.lbResponseTime.TabIndex = 0;
            this.lbResponseTime.Text = "振镜响应时间（us）：";
            // 
            // lbFieldSize
            // 
            this.lbFieldSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbFieldSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFieldSize.Location = new System.Drawing.Point(27, 58);
            this.lbFieldSize.Name = "lbFieldSize";
            this.lbFieldSize.Size = new System.Drawing.Size(160, 22);
            this.lbFieldSize.TabIndex = 2;
            this.lbFieldSize.Text = "视场大小 (um)：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(336, 620);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 38);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(423, 620);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 38);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbxScan
            // 
            this.gbxScan.Controls.Add(this.lbInfo);
            this.gbxScan.Controls.Add(this.cbxY2Galvo);
            this.gbxScan.Controls.Add(this.cbxYGalvo);
            this.gbxScan.Controls.Add(this.cbxXGalvo);
            this.gbxScan.Controls.Add(this.lbY2Mirror);
            this.gbxScan.Controls.Add(this.lbYMirror);
            this.gbxScan.Controls.Add(this.lbXMirror);
            this.gbxScan.Location = new System.Drawing.Point(12, 12);
            this.gbxScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbxScan.Name = "gbxScan";
            this.gbxScan.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbxScan.Size = new System.Drawing.Size(485, 118);
            this.gbxScan.TabIndex = 8;
            this.gbxScan.TabStop = false;
            this.gbxScan.Text = "扫描控制";
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.ForeColor = System.Drawing.Color.Red;
            this.lbInfo.Location = new System.Drawing.Point(329, 14);
            this.lbInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(151, 15);
            this.lbInfo.TabIndex = 20;
            this.lbInfo.Text = "No NI Board Found.";
            // 
            // cbxY2Galvo
            // 
            this.cbxY2Galvo.FormattingEnabled = true;
            this.cbxY2Galvo.Location = new System.Drawing.Point(331, 74);
            this.cbxY2Galvo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxY2Galvo.Name = "cbxY2Galvo";
            this.cbxY2Galvo.Size = new System.Drawing.Size(119, 23);
            this.cbxY2Galvo.TabIndex = 19;
            // 
            // cbxYGalvo
            // 
            this.cbxYGalvo.FormattingEnabled = true;
            this.cbxYGalvo.Location = new System.Drawing.Point(87, 74);
            this.cbxYGalvo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxYGalvo.Name = "cbxYGalvo";
            this.cbxYGalvo.Size = new System.Drawing.Size(119, 23);
            this.cbxYGalvo.TabIndex = 18;
            // 
            // cbxXGalvo
            // 
            this.cbxXGalvo.FormattingEnabled = true;
            this.cbxXGalvo.Location = new System.Drawing.Point(87, 31);
            this.cbxXGalvo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxXGalvo.Name = "cbxXGalvo";
            this.cbxXGalvo.Size = new System.Drawing.Size(119, 23);
            this.cbxXGalvo.TabIndex = 17;
            // 
            // lbY2Mirror
            // 
            this.lbY2Mirror.AutoSize = true;
            this.lbY2Mirror.Location = new System.Drawing.Point(256, 78);
            this.lbY2Mirror.Name = "lbY2Mirror";
            this.lbY2Mirror.Size = new System.Drawing.Size(60, 15);
            this.lbY2Mirror.TabIndex = 4;
            this.lbY2Mirror.Text = "Y补偿镜";
            // 
            // lbYMirror
            // 
            this.lbYMirror.AutoSize = true;
            this.lbYMirror.Location = new System.Drawing.Point(27, 78);
            this.lbYMirror.Name = "lbYMirror";
            this.lbYMirror.Size = new System.Drawing.Size(45, 15);
            this.lbYMirror.TabIndex = 3;
            this.lbYMirror.Text = "Y振镜";
            // 
            // lbXMirror
            // 
            this.lbXMirror.AutoSize = true;
            this.lbXMirror.Location = new System.Drawing.Point(27, 35);
            this.lbXMirror.Name = "lbXMirror";
            this.lbXMirror.Size = new System.Drawing.Size(45, 15);
            this.lbXMirror.TabIndex = 2;
            this.lbXMirror.Text = "X振镜";
            // 
            // gbxAcqMode
            // 
            this.gbxAcqMode.Controls.Add(this.cbxStartSync);
            this.gbxAcqMode.Controls.Add(this.cbxAcqTrigger);
            this.gbxAcqMode.Controls.Add(this.lbAcqTrigger);
            this.gbxAcqMode.Controls.Add(this.lbStartSync);
            this.gbxAcqMode.Controls.Add(this.rbtnApd);
            this.gbxAcqMode.Controls.Add(this.rbtnPmt);
            this.gbxAcqMode.Location = new System.Drawing.Point(12, 304);
            this.gbxAcqMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxAcqMode.Name = "gbxAcqMode";
            this.gbxAcqMode.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxAcqMode.Size = new System.Drawing.Size(485, 91);
            this.gbxAcqMode.TabIndex = 9;
            this.gbxAcqMode.TabStop = false;
            this.gbxAcqMode.Text = "采集方式";
            // 
            // cbxStartSync
            // 
            this.cbxStartSync.FormattingEnabled = true;
            this.cbxStartSync.Location = new System.Drawing.Point(299, 24);
            this.cbxStartSync.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxStartSync.Name = "cbxStartSync";
            this.cbxStartSync.Size = new System.Drawing.Size(163, 23);
            this.cbxStartSync.TabIndex = 21;
            // 
            // cbxAcqTrigger
            // 
            this.cbxAcqTrigger.FormattingEnabled = true;
            this.cbxAcqTrigger.Location = new System.Drawing.Point(299, 56);
            this.cbxAcqTrigger.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxAcqTrigger.Name = "cbxAcqTrigger";
            this.cbxAcqTrigger.Size = new System.Drawing.Size(163, 23);
            this.cbxAcqTrigger.TabIndex = 20;
            // 
            // lbAcqTrigger
            // 
            this.lbAcqTrigger.AutoSize = true;
            this.lbAcqTrigger.Location = new System.Drawing.Point(212, 60);
            this.lbAcqTrigger.Name = "lbAcqTrigger";
            this.lbAcqTrigger.Size = new System.Drawing.Size(67, 15);
            this.lbAcqTrigger.TabIndex = 20;
            this.lbAcqTrigger.Text = "触发信号";
            // 
            // lbStartSync
            // 
            this.lbStartSync.AutoSize = true;
            this.lbStartSync.Location = new System.Drawing.Point(212, 28);
            this.lbStartSync.Name = "lbStartSync";
            this.lbStartSync.Size = new System.Drawing.Size(67, 15);
            this.lbStartSync.TabIndex = 22;
            this.lbStartSync.Text = "启动同步";
            // 
            // rbtnApd
            // 
            this.rbtnApd.AutoSize = true;
            this.rbtnApd.Location = new System.Drawing.Point(120, 25);
            this.rbtnApd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtnApd.Name = "rbtnApd";
            this.rbtnApd.Size = new System.Drawing.Size(52, 19);
            this.rbtnApd.TabIndex = 1;
            this.rbtnApd.Text = "APD";
            this.rbtnApd.UseVisualStyleBackColor = true;
            // 
            // rbtnPmt
            // 
            this.rbtnPmt.AutoSize = true;
            this.rbtnPmt.Checked = true;
            this.rbtnPmt.Location = new System.Drawing.Point(29, 25);
            this.rbtnPmt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtnPmt.Name = "rbtnPmt";
            this.rbtnPmt.Size = new System.Drawing.Size(52, 19);
            this.rbtnPmt.TabIndex = 0;
            this.rbtnPmt.TabStop = true;
            this.rbtnPmt.Text = "PMT";
            this.rbtnPmt.UseVisualStyleBackColor = true;
            // 
            // panAcq
            // 
            this.panAcq.BackColor = System.Drawing.Color.Transparent;
            this.panAcq.Controls.Add(this.tbcAcq);
            this.panAcq.Location = new System.Drawing.Point(12, 402);
            this.panAcq.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panAcq.Name = "panAcq";
            this.panAcq.Size = new System.Drawing.Size(485, 212);
            this.panAcq.TabIndex = 10;
            // 
            // tbcAcq
            // 
            this.tbcAcq.Controls.Add(this.tpgApd);
            this.tbcAcq.Controls.Add(this.tpgPmt);
            this.tbcAcq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcAcq.Location = new System.Drawing.Point(0, 0);
            this.tbcAcq.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbcAcq.Name = "tbcAcq";
            this.tbcAcq.SelectedIndex = 0;
            this.tbcAcq.Size = new System.Drawing.Size(485, 212);
            this.tbcAcq.TabIndex = 0;
            // 
            // tpgApd
            // 
            this.tpgApd.BackColor = System.Drawing.Color.Gainsboro;
            this.tpgApd.Controls.Add(this.lbApdTriggerIn);
            this.tpgApd.Controls.Add(this.cbxApdTriggerIn);
            this.tpgApd.Controls.Add(this.cbxApd640CiSrc);
            this.tpgApd.Controls.Add(this.cbxApd640Ci);
            this.tpgApd.Controls.Add(this.lbApd640);
            this.tpgApd.Controls.Add(this.cbxApd561CiSrc);
            this.tpgApd.Controls.Add(this.cbxApd561Ci);
            this.tpgApd.Controls.Add(this.lbApd561);
            this.tpgApd.Controls.Add(this.cbxApd488CiSrc);
            this.tpgApd.Controls.Add(this.cbxApd488Ci);
            this.tpgApd.Controls.Add(this.lbApd488);
            this.tpgApd.Controls.Add(this.lbCounterSrc);
            this.tpgApd.Controls.Add(this.cbxApd405CiSrc);
            this.tpgApd.Controls.Add(this.cbxApd405Ci);
            this.tpgApd.Controls.Add(this.lbCounter);
            this.tpgApd.Controls.Add(this.lbApd405);
            this.tpgApd.Location = new System.Drawing.Point(4, 25);
            this.tpgApd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgApd.Name = "tpgApd";
            this.tpgApd.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgApd.Size = new System.Drawing.Size(477, 183);
            this.tpgApd.TabIndex = 0;
            this.tpgApd.Text = "APD";
            // 
            // lbApdTriggerIn
            // 
            this.lbApdTriggerIn.AutoSize = true;
            this.lbApdTriggerIn.Location = new System.Drawing.Point(331, 11);
            this.lbApdTriggerIn.Name = "lbApdTriggerIn";
            this.lbApdTriggerIn.Size = new System.Drawing.Size(127, 15);
            this.lbApdTriggerIn.TabIndex = 64;
            this.lbApdTriggerIn.Text = "触发接收（共用）";
            // 
            // cbxApdTriggerIn
            // 
            this.cbxApdTriggerIn.FormattingEnabled = true;
            this.cbxApdTriggerIn.Location = new System.Drawing.Point(325, 39);
            this.cbxApdTriggerIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxApdTriggerIn.Name = "cbxApdTriggerIn";
            this.cbxApdTriggerIn.Size = new System.Drawing.Size(133, 23);
            this.cbxApdTriggerIn.TabIndex = 60;
            // 
            // cbxApd640CiSrc
            // 
            this.cbxApd640CiSrc.FormattingEnabled = true;
            this.cbxApd640CiSrc.Location = new System.Drawing.Point(197, 136);
            this.cbxApd640CiSrc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxApd640CiSrc.Name = "cbxApd640CiSrc";
            this.cbxApd640CiSrc.Size = new System.Drawing.Size(119, 23);
            this.cbxApd640CiSrc.TabIndex = 59;
            // 
            // cbxApd640Ci
            // 
            this.cbxApd640Ci.FormattingEnabled = true;
            this.cbxApd640Ci.Location = new System.Drawing.Point(81, 136);
            this.cbxApd640Ci.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxApd640Ci.Name = "cbxApd640Ci";
            this.cbxApd640Ci.Size = new System.Drawing.Size(107, 23);
            this.cbxApd640Ci.TabIndex = 58;
            // 
            // lbApd640
            // 
            this.lbApd640.AutoSize = true;
            this.lbApd640.Location = new System.Drawing.Point(21, 140);
            this.lbApd640.Name = "lbApd640";
            this.lbApd640.Size = new System.Drawing.Size(47, 15);
            this.lbApd640.TabIndex = 57;
            this.lbApd640.Text = "640nm";
            // 
            // cbxApd561CiSrc
            // 
            this.cbxApd561CiSrc.FormattingEnabled = true;
            this.cbxApd561CiSrc.Location = new System.Drawing.Point(197, 104);
            this.cbxApd561CiSrc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxApd561CiSrc.Name = "cbxApd561CiSrc";
            this.cbxApd561CiSrc.Size = new System.Drawing.Size(119, 23);
            this.cbxApd561CiSrc.TabIndex = 56;
            // 
            // cbxApd561Ci
            // 
            this.cbxApd561Ci.FormattingEnabled = true;
            this.cbxApd561Ci.Location = new System.Drawing.Point(81, 104);
            this.cbxApd561Ci.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxApd561Ci.Name = "cbxApd561Ci";
            this.cbxApd561Ci.Size = new System.Drawing.Size(107, 23);
            this.cbxApd561Ci.TabIndex = 55;
            // 
            // lbApd561
            // 
            this.lbApd561.AutoSize = true;
            this.lbApd561.Location = new System.Drawing.Point(21, 108);
            this.lbApd561.Name = "lbApd561";
            this.lbApd561.Size = new System.Drawing.Size(47, 15);
            this.lbApd561.TabIndex = 54;
            this.lbApd561.Text = "561nm";
            // 
            // cbxApd488CiSrc
            // 
            this.cbxApd488CiSrc.FormattingEnabled = true;
            this.cbxApd488CiSrc.Location = new System.Drawing.Point(197, 71);
            this.cbxApd488CiSrc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxApd488CiSrc.Name = "cbxApd488CiSrc";
            this.cbxApd488CiSrc.Size = new System.Drawing.Size(119, 23);
            this.cbxApd488CiSrc.TabIndex = 53;
            // 
            // cbxApd488Ci
            // 
            this.cbxApd488Ci.FormattingEnabled = true;
            this.cbxApd488Ci.Location = new System.Drawing.Point(81, 71);
            this.cbxApd488Ci.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxApd488Ci.Name = "cbxApd488Ci";
            this.cbxApd488Ci.Size = new System.Drawing.Size(107, 23);
            this.cbxApd488Ci.TabIndex = 52;
            // 
            // lbApd488
            // 
            this.lbApd488.AutoSize = true;
            this.lbApd488.Location = new System.Drawing.Point(21, 75);
            this.lbApd488.Name = "lbApd488";
            this.lbApd488.Size = new System.Drawing.Size(47, 15);
            this.lbApd488.TabIndex = 51;
            this.lbApd488.Text = "488nm";
            // 
            // lbCounterSrc
            // 
            this.lbCounterSrc.AutoSize = true;
            this.lbCounterSrc.Location = new System.Drawing.Point(213, 11);
            this.lbCounterSrc.Name = "lbCounterSrc";
            this.lbCounterSrc.Size = new System.Drawing.Size(76, 15);
            this.lbCounterSrc.TabIndex = 50;
            this.lbCounterSrc.Text = "APD接线端";
            // 
            // cbxApd405CiSrc
            // 
            this.cbxApd405CiSrc.FormattingEnabled = true;
            this.cbxApd405CiSrc.Location = new System.Drawing.Point(197, 39);
            this.cbxApd405CiSrc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxApd405CiSrc.Name = "cbxApd405CiSrc";
            this.cbxApd405CiSrc.Size = new System.Drawing.Size(119, 23);
            this.cbxApd405CiSrc.TabIndex = 49;
            // 
            // cbxApd405Ci
            // 
            this.cbxApd405Ci.FormattingEnabled = true;
            this.cbxApd405Ci.Location = new System.Drawing.Point(81, 39);
            this.cbxApd405Ci.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxApd405Ci.Name = "cbxApd405Ci";
            this.cbxApd405Ci.Size = new System.Drawing.Size(107, 23);
            this.cbxApd405Ci.TabIndex = 48;
            // 
            // lbCounter
            // 
            this.lbCounter.AutoSize = true;
            this.lbCounter.Location = new System.Drawing.Point(83, 11);
            this.lbCounter.Name = "lbCounter";
            this.lbCounter.Size = new System.Drawing.Size(82, 15);
            this.lbCounter.TabIndex = 47;
            this.lbCounter.Text = "计数器通道";
            // 
            // lbApd405
            // 
            this.lbApd405.AutoSize = true;
            this.lbApd405.Location = new System.Drawing.Point(21, 42);
            this.lbApd405.Name = "lbApd405";
            this.lbApd405.Size = new System.Drawing.Size(47, 15);
            this.lbApd405.TabIndex = 46;
            this.lbApd405.Text = "405nm";
            // 
            // tpgPmt
            // 
            this.tpgPmt.BackColor = System.Drawing.Color.Gainsboro;
            this.tpgPmt.Controls.Add(this.cbxPmtTriggerIn);
            this.tpgPmt.Controls.Add(this.lbPmtTriggerIn);
            this.tpgPmt.Controls.Add(this.cbxPmt640Ai);
            this.tpgPmt.Controls.Add(this.lbPmt640);
            this.tpgPmt.Controls.Add(this.cbxPmt561Ai);
            this.tpgPmt.Controls.Add(this.lbPmt561);
            this.tpgPmt.Controls.Add(this.cbxPmt488Ai);
            this.tpgPmt.Controls.Add(this.lbPmt488);
            this.tpgPmt.Controls.Add(this.cbxPmt405Ai);
            this.tpgPmt.Controls.Add(this.lbAi);
            this.tpgPmt.Controls.Add(this.lbPmt405);
            this.tpgPmt.Location = new System.Drawing.Point(4, 25);
            this.tpgPmt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgPmt.Name = "tpgPmt";
            this.tpgPmt.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpgPmt.Size = new System.Drawing.Size(477, 183);
            this.tpgPmt.TabIndex = 1;
            this.tpgPmt.Text = "PMT";
            // 
            // cbxPmtTriggerIn
            // 
            this.cbxPmtTriggerIn.FormattingEnabled = true;
            this.cbxPmtTriggerIn.Location = new System.Drawing.Point(293, 42);
            this.cbxPmtTriggerIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxPmtTriggerIn.Name = "cbxPmtTriggerIn";
            this.cbxPmtTriggerIn.Size = new System.Drawing.Size(151, 23);
            this.cbxPmtTriggerIn.TabIndex = 60;
            // 
            // lbPmtTriggerIn
            // 
            this.lbPmtTriggerIn.AutoSize = true;
            this.lbPmtTriggerIn.Location = new System.Drawing.Point(311, 15);
            this.lbPmtTriggerIn.Name = "lbPmtTriggerIn";
            this.lbPmtTriggerIn.Size = new System.Drawing.Size(127, 15);
            this.lbPmtTriggerIn.TabIndex = 59;
            this.lbPmtTriggerIn.Text = "触发接收（共用）";
            // 
            // cbxPmt640Ai
            // 
            this.cbxPmt640Ai.FormattingEnabled = true;
            this.cbxPmt640Ai.Location = new System.Drawing.Point(97, 140);
            this.cbxPmt640Ai.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxPmt640Ai.Name = "cbxPmt640Ai";
            this.cbxPmt640Ai.Size = new System.Drawing.Size(151, 23);
            this.cbxPmt640Ai.TabIndex = 58;
            // 
            // lbPmt640
            // 
            this.lbPmt640.AutoSize = true;
            this.lbPmt640.Location = new System.Drawing.Point(37, 144);
            this.lbPmt640.Name = "lbPmt640";
            this.lbPmt640.Size = new System.Drawing.Size(47, 15);
            this.lbPmt640.TabIndex = 57;
            this.lbPmt640.Text = "640nm";
            // 
            // cbxPmt561Ai
            // 
            this.cbxPmt561Ai.FormattingEnabled = true;
            this.cbxPmt561Ai.Location = new System.Drawing.Point(97, 108);
            this.cbxPmt561Ai.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxPmt561Ai.Name = "cbxPmt561Ai";
            this.cbxPmt561Ai.Size = new System.Drawing.Size(151, 23);
            this.cbxPmt561Ai.TabIndex = 55;
            // 
            // lbPmt561
            // 
            this.lbPmt561.AutoSize = true;
            this.lbPmt561.Location = new System.Drawing.Point(37, 111);
            this.lbPmt561.Name = "lbPmt561";
            this.lbPmt561.Size = new System.Drawing.Size(47, 15);
            this.lbPmt561.TabIndex = 54;
            this.lbPmt561.Text = "561nm";
            // 
            // cbxPmt488Ai
            // 
            this.cbxPmt488Ai.FormattingEnabled = true;
            this.cbxPmt488Ai.Location = new System.Drawing.Point(97, 75);
            this.cbxPmt488Ai.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxPmt488Ai.Name = "cbxPmt488Ai";
            this.cbxPmt488Ai.Size = new System.Drawing.Size(151, 23);
            this.cbxPmt488Ai.TabIndex = 52;
            // 
            // lbPmt488
            // 
            this.lbPmt488.AutoSize = true;
            this.lbPmt488.Location = new System.Drawing.Point(37, 79);
            this.lbPmt488.Name = "lbPmt488";
            this.lbPmt488.Size = new System.Drawing.Size(47, 15);
            this.lbPmt488.TabIndex = 51;
            this.lbPmt488.Text = "488nm";
            // 
            // cbxPmt405Ai
            // 
            this.cbxPmt405Ai.FormattingEnabled = true;
            this.cbxPmt405Ai.Location = new System.Drawing.Point(97, 42);
            this.cbxPmt405Ai.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxPmt405Ai.Name = "cbxPmt405Ai";
            this.cbxPmt405Ai.Size = new System.Drawing.Size(151, 23);
            this.cbxPmt405Ai.TabIndex = 48;
            // 
            // lbAi
            // 
            this.lbAi.AutoSize = true;
            this.lbAi.Location = new System.Drawing.Point(128, 15);
            this.lbAi.Name = "lbAi";
            this.lbAi.Size = new System.Drawing.Size(76, 15);
            this.lbAi.TabIndex = 47;
            this.lbAi.Text = "PMT接线端";
            // 
            // lbPmt405
            // 
            this.lbPmt405.AutoSize = true;
            this.lbPmt405.Location = new System.Drawing.Point(37, 46);
            this.lbPmt405.Name = "lbPmt405";
            this.lbPmt405.Size = new System.Drawing.Size(47, 15);
            this.lbPmt405.TabIndex = 46;
            this.lbPmt405.Text = "405nm";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 620);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 38);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FormSysCfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 664);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.panAcq);
            this.Controls.Add(this.gbxAcqMode);
            this.Controls.Add(this.gbxScan);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbxGalv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSysCfg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统配置";
            this.Load += new System.EventHandler(this.FormSysCfg_Load);
            this.gbxGalv.ResumeLayout(false);
            this.gbxGalv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFieldSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResponseTime)).EndInit();
            this.gbxScan.ResumeLayout(false);
            this.gbxScan.PerformLayout();
            this.gbxAcqMode.ResumeLayout(false);
            this.gbxAcqMode.PerformLayout();
            this.panAcq.ResumeLayout(false);
            this.tbcAcq.ResumeLayout(false);
            this.tpgApd.ResumeLayout(false);
            this.tpgApd.PerformLayout();
            this.tpgPmt.ResumeLayout(false);
            this.tpgPmt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxGalv;
        private System.Windows.Forms.TextBox tbxCurveCoff;
        private System.Windows.Forms.Label lbCurveCoff;
        private System.Windows.Forms.TextBox tbxCalibrationV;
        private System.Windows.Forms.Label lbCalibrationV;
        private System.Windows.Forms.Label lbResponseTime;
        private System.Windows.Forms.Label lbFieldSize;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nudResponseTime;
        private System.Windows.Forms.NumericUpDown nudFieldSize;
        private System.Windows.Forms.GroupBox gbxScan;
        private System.Windows.Forms.Label lbXMirror;
        private System.Windows.Forms.Label lbYMirror;
        private System.Windows.Forms.Label lbY2Mirror;
        private System.Windows.Forms.GroupBox gbxAcqMode;
        private System.Windows.Forms.RadioButton rbtnPmt;
        private System.Windows.Forms.RadioButton rbtnApd;
        private System.Windows.Forms.Panel panAcq;
        private System.Windows.Forms.TabControl tbcAcq;
        private System.Windows.Forms.TabPage tpgApd;
        private System.Windows.Forms.TabPage tpgPmt;
        private System.Windows.Forms.ComboBox cbxXGalvo;
        private System.Windows.Forms.ComboBox cbxYGalvo;
        private System.Windows.Forms.ComboBox cbxY2Galvo;
        private System.Windows.Forms.ComboBox cbxApd640CiSrc;
        private System.Windows.Forms.ComboBox cbxApd640Ci;
        private System.Windows.Forms.Label lbApd640;
        private System.Windows.Forms.ComboBox cbxApd561CiSrc;
        private System.Windows.Forms.ComboBox cbxApd561Ci;
        private System.Windows.Forms.Label lbApd561;
        private System.Windows.Forms.ComboBox cbxApd488CiSrc;
        private System.Windows.Forms.ComboBox cbxApd488Ci;
        private System.Windows.Forms.Label lbApd488;
        private System.Windows.Forms.Label lbCounterSrc;
        private System.Windows.Forms.ComboBox cbxApd405CiSrc;
        private System.Windows.Forms.ComboBox cbxApd405Ci;
        private System.Windows.Forms.Label lbCounter;
        private System.Windows.Forms.Label lbApd405;
        private System.Windows.Forms.ComboBox cbxPmt640Ai;
        private System.Windows.Forms.Label lbPmt640;
        private System.Windows.Forms.ComboBox cbxPmt561Ai;
        private System.Windows.Forms.Label lbPmt561;
        private System.Windows.Forms.ComboBox cbxPmt488Ai;
        private System.Windows.Forms.Label lbPmt488;
        private System.Windows.Forms.ComboBox cbxPmt405Ai;
        private System.Windows.Forms.Label lbAi;
        private System.Windows.Forms.Label lbPmt405;
        private System.Windows.Forms.Label lbAcqTrigger;
        private System.Windows.Forms.ComboBox cbxAcqTrigger;
        private System.Windows.Forms.Label lbPmtTriggerIn;
        private System.Windows.Forms.ComboBox cbxPmtTriggerIn;
        private System.Windows.Forms.ComboBox cbxApdTriggerIn;
        private System.Windows.Forms.Label lbApdTriggerIn;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbxStartSync;
        private System.Windows.Forms.Label lbStartSync;
    }
}