
namespace confocal_ui.View
{
    partial class FormSysSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSysSettings));
            this.inputPanel = new C1.Win.C1InputPanel.C1InputPanel();
            this.ghDevice = new C1.Win.C1InputPanel.InputGroupHeader();
            this.btnSearch = new C1.Win.C1InputPanel.InputButton();
            this.lbInfo = new C1.Win.C1InputPanel.InputLabel();
            this.ghScanControl = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lbXGalvo = new C1.Win.C1InputPanel.InputLabel();
            this.cbxXGalvo = new C1.Win.C1InputPanel.InputComboBox();
            this.lbYGalvo = new C1.Win.C1InputPanel.InputLabel();
            this.cbxYGalvo = new C1.Win.C1InputPanel.InputComboBox();
            this.lbYGalvo2 = new C1.Win.C1InputPanel.InputLabel();
            this.cbxYGalvo2 = new C1.Win.C1InputPanel.InputComboBox();
            this.inputSeparator1 = new C1.Win.C1InputPanel.InputSeparator();
            this.lbXGalvaOffset = new C1.Win.C1InputPanel.InputLabel();
            this.tbxXGalvoOffset = new C1.Win.C1InputPanel.InputTextBox();
            this.lbYGalvoOffset = new C1.Win.C1InputPanel.InputLabel();
            this.tbxYGalvoOffset = new C1.Win.C1InputPanel.InputTextBox();
            this.lbXGalvoScaleFactor = new C1.Win.C1InputPanel.InputLabel();
            this.tbxXGalvoScaleFactor = new C1.Win.C1InputPanel.InputTextBox();
            this.lbUnit1 = new C1.Win.C1InputPanel.InputLabel();
            this.lbYGalvoScaleFactor = new C1.Win.C1InputPanel.InputLabel();
            this.tbxYGalvoScaleFactor = new C1.Win.C1InputPanel.InputTextBox();
            this.lbUnit2 = new C1.Win.C1InputPanel.InputLabel();
            this.ghScanPara = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lbGalvoResponseTime = new C1.Win.C1InputPanel.InputLabel();
            this.nbGalvoResponseTime = new C1.Win.C1InputPanel.InputNumericBox();
            this.lbGalvoResponseTimeUnit = new C1.Win.C1InputPanel.InputLabel();
            this.lbScanRange = new C1.Win.C1InputPanel.InputLabel();
            this.nbScanRange = new C1.Win.C1InputPanel.InputNumericBox();
            this.lbScanRangeUnit = new C1.Win.C1InputPanel.InputLabel();
            this.ghAcq = new C1.Win.C1InputPanel.InputGroupHeader();
            this.rbtnPMT = new C1.Win.C1InputPanel.InputRadioButton();
            this.rbtnAPD = new C1.Win.C1InputPanel.InputRadioButton();
            this.lbStartSync = new C1.Win.C1InputPanel.InputLabel();
            this.cbxStartSync = new C1.Win.C1InputPanel.InputComboBox();
            this.lbTrigger = new C1.Win.C1InputPanel.InputLabel();
            this.cbxTrigger = new C1.Win.C1InputPanel.InputComboBox();
            this.lbTriggerR = new C1.Win.C1InputPanel.InputLabel();
            this.cbxTriggerR = new C1.Win.C1InputPanel.InputComboBox();
            this.ghPMT = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lb405 = new C1.Win.C1InputPanel.InputLabel();
            this.cbx405PMT = new C1.Win.C1InputPanel.InputComboBox();
            this.lb488 = new C1.Win.C1InputPanel.InputLabel();
            this.cbx488PMT = new C1.Win.C1InputPanel.InputComboBox();
            this.lb561 = new C1.Win.C1InputPanel.InputLabel();
            this.cbx561PMT = new C1.Win.C1InputPanel.InputComboBox();
            this.lb640 = new C1.Win.C1InputPanel.InputLabel();
            this.cbx640PMT = new C1.Win.C1InputPanel.InputComboBox();
            this.ghAPD = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lbCtr = new C1.Win.C1InputPanel.InputLabel();
            this.lbAPDSource = new C1.Win.C1InputPanel.InputLabel();
            this.lb4052 = new C1.Win.C1InputPanel.InputLabel();
            this.cbx405Ctr = new C1.Win.C1InputPanel.InputComboBox();
            this.cbx405CtrSource = new C1.Win.C1InputPanel.InputComboBox();
            this.lb4882 = new C1.Win.C1InputPanel.InputLabel();
            this.cbx488Ctr = new C1.Win.C1InputPanel.InputComboBox();
            this.cbx488Source = new C1.Win.C1InputPanel.InputComboBox();
            this.lb5612 = new C1.Win.C1InputPanel.InputLabel();
            this.cbx561Ctr = new C1.Win.C1InputPanel.InputComboBox();
            this.cbx561Source = new C1.Win.C1InputPanel.InputComboBox();
            this.lb6402 = new C1.Win.C1InputPanel.InputLabel();
            this.cbx640Ctr = new C1.Win.C1InputPanel.InputComboBox();
            this.cbx640Source = new C1.Win.C1InputPanel.InputComboBox();
            this.inputLabel1 = new C1.Win.C1InputPanel.InputLabel();
            this.inputLabel2 = new C1.Win.C1InputPanel.InputLabel();
            this.inputGroupHeader1 = new C1.Win.C1InputPanel.InputGroupHeader();
            this.inputLabel3 = new C1.Win.C1InputPanel.InputLabel();
            this.inputLabel4 = new C1.Win.C1InputPanel.InputLabel();
            this.inputLabel5 = new C1.Win.C1InputPanel.InputLabel();
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.inputPanel.Items.Add(this.ghDevice);
            this.inputPanel.Items.Add(this.btnSearch);
            this.inputPanel.Items.Add(this.lbInfo);
            this.inputPanel.Items.Add(this.ghScanControl);
            this.inputPanel.Items.Add(this.lbXGalvo);
            this.inputPanel.Items.Add(this.cbxXGalvo);
            this.inputPanel.Items.Add(this.lbYGalvo);
            this.inputPanel.Items.Add(this.cbxYGalvo);
            this.inputPanel.Items.Add(this.lbYGalvo2);
            this.inputPanel.Items.Add(this.cbxYGalvo2);
            this.inputPanel.Items.Add(this.inputSeparator1);
            this.inputPanel.Items.Add(this.lbXGalvaOffset);
            this.inputPanel.Items.Add(this.tbxXGalvoOffset);
            this.inputPanel.Items.Add(this.lbYGalvoOffset);
            this.inputPanel.Items.Add(this.tbxYGalvoOffset);
            this.inputPanel.Items.Add(this.lbXGalvoScaleFactor);
            this.inputPanel.Items.Add(this.tbxXGalvoScaleFactor);
            this.inputPanel.Items.Add(this.lbUnit1);
            this.inputPanel.Items.Add(this.lbYGalvoScaleFactor);
            this.inputPanel.Items.Add(this.tbxYGalvoScaleFactor);
            this.inputPanel.Items.Add(this.lbUnit2);
            this.inputPanel.Items.Add(this.ghScanPara);
            this.inputPanel.Items.Add(this.lbGalvoResponseTime);
            this.inputPanel.Items.Add(this.nbGalvoResponseTime);
            this.inputPanel.Items.Add(this.lbGalvoResponseTimeUnit);
            this.inputPanel.Items.Add(this.lbScanRange);
            this.inputPanel.Items.Add(this.nbScanRange);
            this.inputPanel.Items.Add(this.lbScanRangeUnit);
            this.inputPanel.Items.Add(this.ghAcq);
            this.inputPanel.Items.Add(this.rbtnPMT);
            this.inputPanel.Items.Add(this.rbtnAPD);
            this.inputPanel.Items.Add(this.lbStartSync);
            this.inputPanel.Items.Add(this.cbxStartSync);
            this.inputPanel.Items.Add(this.lbTrigger);
            this.inputPanel.Items.Add(this.cbxTrigger);
            this.inputPanel.Items.Add(this.lbTriggerR);
            this.inputPanel.Items.Add(this.cbxTriggerR);
            this.inputPanel.Items.Add(this.ghPMT);
            this.inputPanel.Items.Add(this.lb405);
            this.inputPanel.Items.Add(this.cbx405PMT);
            this.inputPanel.Items.Add(this.lb488);
            this.inputPanel.Items.Add(this.cbx488PMT);
            this.inputPanel.Items.Add(this.lb561);
            this.inputPanel.Items.Add(this.cbx561PMT);
            this.inputPanel.Items.Add(this.lb640);
            this.inputPanel.Items.Add(this.cbx640PMT);
            this.inputPanel.Items.Add(this.ghAPD);
            this.inputPanel.Items.Add(this.lbCtr);
            this.inputPanel.Items.Add(this.lbAPDSource);
            this.inputPanel.Items.Add(this.lb4052);
            this.inputPanel.Items.Add(this.cbx405Ctr);
            this.inputPanel.Items.Add(this.cbx405CtrSource);
            this.inputPanel.Items.Add(this.lb4882);
            this.inputPanel.Items.Add(this.cbx488Ctr);
            this.inputPanel.Items.Add(this.cbx488Source);
            this.inputPanel.Items.Add(this.lb5612);
            this.inputPanel.Items.Add(this.cbx561Ctr);
            this.inputPanel.Items.Add(this.cbx561Source);
            this.inputPanel.Items.Add(this.lb6402);
            this.inputPanel.Items.Add(this.cbx640Ctr);
            this.inputPanel.Items.Add(this.cbx640Source);
            this.inputPanel.Location = new System.Drawing.Point(0, 0);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(272, 569);
            this.inputPanel.TabIndex = 0;
            // 
            // ghDevice
            // 
            this.ghDevice.Collapsible = true;
            this.ghDevice.Name = "ghDevice";
            this.ghDevice.Text = "设备";
            // 
            // btnSearch
            // 
            this.btnSearch.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnSearch.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Text = "搜索设备";
            // 
            // lbInfo
            // 
            this.lbInfo.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Text = "                        没有发现NI设备！";
            this.lbInfo.VerticalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            // 
            // ghScanControl
            // 
            this.ghScanControl.Collapsible = true;
            this.ghScanControl.Name = "ghScanControl";
            this.ghScanControl.Text = "扫描控制";
            // 
            // lbXGalvo
            // 
            this.lbXGalvo.Name = "lbXGalvo";
            this.lbXGalvo.Text = "X振镜：";
            this.lbXGalvo.Width = 45;
            // 
            // cbxXGalvo
            // 
            this.cbxXGalvo.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbxXGalvo.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxXGalvo.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxXGalvo.Name = "cbxXGalvo";
            this.cbxXGalvo.Width = 70;
            // 
            // lbYGalvo
            // 
            this.lbYGalvo.Name = "lbYGalvo";
            this.lbYGalvo.Text = "Y振镜：";
            this.lbYGalvo.Width = 45;
            // 
            // cbxYGalvo
            // 
            this.cbxYGalvo.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbxYGalvo.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxYGalvo.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxYGalvo.Name = "cbxYGalvo";
            this.cbxYGalvo.Width = 70;
            // 
            // lbYGalvo2
            // 
            this.lbYGalvo2.Name = "lbYGalvo2";
            this.lbYGalvo2.Text = "Y补偿镜：";
            // 
            // cbxYGalvo2
            // 
            this.cbxYGalvo2.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbxYGalvo2.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxYGalvo2.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxYGalvo2.Name = "cbxYGalvo2";
            this.cbxYGalvo2.Width = 70;
            // 
            // inputSeparator1
            // 
            this.inputSeparator1.Name = "inputSeparator1";
            this.inputSeparator1.Width = 250;
            // 
            // lbXGalvaOffset
            // 
            this.lbXGalvaOffset.Name = "lbXGalvaOffset";
            this.lbXGalvaOffset.Text = "X振镜偏置：";
            this.lbXGalvaOffset.Width = 70;
            // 
            // tbxXGalvoOffset
            // 
            this.tbxXGalvoOffset.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbxXGalvoOffset.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxXGalvoOffset.Name = "tbxXGalvoOffset";
            this.tbxXGalvoOffset.Width = 50;
            // 
            // lbYGalvoOffset
            // 
            this.lbYGalvoOffset.Name = "lbYGalvoOffset";
            this.lbYGalvoOffset.Text = "Y振镜偏置：";
            this.lbYGalvoOffset.Width = 70;
            // 
            // tbxYGalvoOffset
            // 
            this.tbxYGalvoOffset.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.tbxYGalvoOffset.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxYGalvoOffset.Name = "tbxYGalvoOffset";
            this.tbxYGalvoOffset.Width = 50;
            // 
            // lbXGalvoScaleFactor
            // 
            this.lbXGalvoScaleFactor.Name = "lbXGalvoScaleFactor";
            this.lbXGalvoScaleFactor.Text = "X振镜标定电压：";
            this.lbXGalvoScaleFactor.Width = 95;
            // 
            // tbxXGalvoScaleFactor
            // 
            this.tbxXGalvoScaleFactor.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbxXGalvoScaleFactor.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxXGalvoScaleFactor.Name = "tbxXGalvoScaleFactor";
            this.tbxXGalvoScaleFactor.Width = 50;
            // 
            // lbUnit1
            // 
            this.lbUnit1.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbUnit1.Name = "lbUnit1";
            this.lbUnit1.Text = "V/um";
            // 
            // lbYGalvoScaleFactor
            // 
            this.lbYGalvoScaleFactor.Name = "lbYGalvoScaleFactor";
            this.lbYGalvoScaleFactor.Text = "Y振镜标定电压：";
            this.lbYGalvoScaleFactor.Width = 95;
            // 
            // tbxYGalvoScaleFactor
            // 
            this.tbxYGalvoScaleFactor.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbxYGalvoScaleFactor.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxYGalvoScaleFactor.Name = "tbxYGalvoScaleFactor";
            this.tbxYGalvoScaleFactor.Width = 50;
            // 
            // lbUnit2
            // 
            this.lbUnit2.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbUnit2.Name = "lbUnit2";
            this.lbUnit2.Text = "V/um";
            // 
            // ghScanPara
            // 
            this.ghScanPara.Collapsible = true;
            this.ghScanPara.Name = "ghScanPara";
            this.ghScanPara.Text = "扫描参数";
            // 
            // lbGalvoResponseTime
            // 
            this.lbGalvoResponseTime.Name = "lbGalvoResponseTime";
            this.lbGalvoResponseTime.Text = "振镜响应时间：";
            // 
            // nbGalvoResponseTime
            // 
            this.nbGalvoResponseTime.Break = C1.Win.C1InputPanel.BreakType.None;
            this.nbGalvoResponseTime.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nbGalvoResponseTime.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nbGalvoResponseTime.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nbGalvoResponseTime.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nbGalvoResponseTime.Name = "nbGalvoResponseTime";
            this.nbGalvoResponseTime.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nbGalvoResponseTime.Width = 50;
            // 
            // lbGalvoResponseTimeUnit
            // 
            this.lbGalvoResponseTimeUnit.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbGalvoResponseTimeUnit.Name = "lbGalvoResponseTimeUnit";
            this.lbGalvoResponseTimeUnit.Text = "us";
            // 
            // lbScanRange
            // 
            this.lbScanRange.Name = "lbScanRange";
            this.lbScanRange.Text = "扫描视场大小：";
            // 
            // nbScanRange
            // 
            this.nbScanRange.Break = C1.Win.C1InputPanel.BreakType.None;
            this.nbScanRange.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nbScanRange.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nbScanRange.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nbScanRange.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nbScanRange.Name = "nbScanRange";
            this.nbScanRange.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nbScanRange.Width = 50;
            // 
            // lbScanRangeUnit
            // 
            this.lbScanRangeUnit.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbScanRangeUnit.Name = "lbScanRangeUnit";
            this.lbScanRangeUnit.Text = "um";
            // 
            // ghAcq
            // 
            this.ghAcq.Collapsible = true;
            this.ghAcq.Name = "ghAcq";
            this.ghAcq.Text = "采集控制";
            // 
            // rbtnPMT
            // 
            this.rbtnPMT.Break = C1.Win.C1InputPanel.BreakType.None;
            this.rbtnPMT.GroupName = "AcqMode";
            this.rbtnPMT.Name = "rbtnPMT";
            this.rbtnPMT.Text = "PMT";
            // 
            // rbtnAPD
            // 
            this.rbtnAPD.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.rbtnAPD.GroupName = "AcqMode";
            this.rbtnAPD.Name = "rbtnAPD";
            this.rbtnAPD.Text = "APD";
            // 
            // lbStartSync
            // 
            this.lbStartSync.Name = "lbStartSync";
            this.lbStartSync.Text = "启动同步：";
            // 
            // cbxStartSync
            // 
            this.cbxStartSync.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxStartSync.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxStartSync.Name = "cbxStartSync";
            this.cbxStartSync.Width = 80;
            // 
            // lbTrigger
            // 
            this.lbTrigger.Name = "lbTrigger";
            this.lbTrigger.Text = "触发信号：";
            // 
            // cbxTrigger
            // 
            this.cbxTrigger.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxTrigger.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxTrigger.Name = "cbxTrigger";
            this.cbxTrigger.Width = 80;
            // 
            // lbTriggerR
            // 
            this.lbTriggerR.Name = "lbTriggerR";
            this.lbTriggerR.Text = "触发接收：";
            // 
            // cbxTriggerR
            // 
            this.cbxTriggerR.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxTriggerR.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxTriggerR.Name = "cbxTriggerR";
            this.cbxTriggerR.Width = 80;
            // 
            // ghPMT
            // 
            this.ghPMT.Collapsible = true;
            this.ghPMT.Name = "ghPMT";
            this.ghPMT.Text = "PMT";
            // 
            // lb405
            // 
            this.lb405.Name = "lb405";
            this.lb405.Text = "405nm：";
            // 
            // cbx405PMT
            // 
            this.cbx405PMT.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.cbx405PMT.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx405PMT.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx405PMT.Name = "cbx405PMT";
            this.cbx405PMT.Width = 60;
            // 
            // lb488
            // 
            this.lb488.Name = "lb488";
            this.lb488.Text = "488nm：";
            // 
            // cbx488PMT
            // 
            this.cbx488PMT.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbx488PMT.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx488PMT.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx488PMT.Name = "cbx488PMT";
            this.cbx488PMT.Width = 60;
            // 
            // lb561
            // 
            this.lb561.Name = "lb561";
            this.lb561.Text = "561nm：";
            // 
            // cbx561PMT
            // 
            this.cbx561PMT.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.cbx561PMT.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx561PMT.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx561PMT.Name = "cbx561PMT";
            this.cbx561PMT.Width = 60;
            // 
            // lb640
            // 
            this.lb640.Name = "lb640";
            this.lb640.Text = "640nm：";
            // 
            // cbx640PMT
            // 
            this.cbx640PMT.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbx640PMT.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx640PMT.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx640PMT.Name = "cbx640PMT";
            this.cbx640PMT.Width = 60;
            // 
            // ghAPD
            // 
            this.ghAPD.Collapsible = true;
            this.ghAPD.Name = "ghAPD";
            this.ghAPD.Text = "APD";
            // 
            // lbCtr
            // 
            this.lbCtr.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.lbCtr.HorizontalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            this.lbCtr.Name = "lbCtr";
            this.lbCtr.Text = "                 计数器通道";
            // 
            // lbAPDSource
            // 
            this.lbAPDSource.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbAPDSource.Name = "lbAPDSource";
            this.lbAPDSource.Text = "    APD接收端";
            this.lbAPDSource.VerticalAlign = C1.Win.C1InputPanel.InputContentAlignment.Far;
            // 
            // lb4052
            // 
            this.lb4052.Name = "lb4052";
            this.lb4052.Text = "405nm：";
            // 
            // cbx405Ctr
            // 
            this.cbx405Ctr.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.cbx405Ctr.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx405Ctr.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx405Ctr.Name = "cbx405Ctr";
            this.cbx405Ctr.Width = 80;
            // 
            // cbx405CtrSource
            // 
            this.cbx405CtrSource.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbx405CtrSource.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx405CtrSource.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx405CtrSource.Name = "cbx405CtrSource";
            this.cbx405CtrSource.Width = 80;
            // 
            // lb4882
            // 
            this.lb4882.Name = "lb4882";
            this.lb4882.Text = "488nm：";
            // 
            // cbx488Ctr
            // 
            this.cbx488Ctr.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.cbx488Ctr.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx488Ctr.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx488Ctr.Name = "cbx488Ctr";
            this.cbx488Ctr.Width = 80;
            // 
            // cbx488Source
            // 
            this.cbx488Source.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbx488Source.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx488Source.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx488Source.Name = "cbx488Source";
            this.cbx488Source.Width = 80;
            // 
            // lb5612
            // 
            this.lb5612.Name = "lb5612";
            this.lb5612.Text = "561nm：";
            // 
            // cbx561Ctr
            // 
            this.cbx561Ctr.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.cbx561Ctr.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx561Ctr.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx561Ctr.Name = "cbx561Ctr";
            this.cbx561Ctr.Width = 80;
            // 
            // cbx561Source
            // 
            this.cbx561Source.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbx561Source.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx561Source.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx561Source.Name = "cbx561Source";
            this.cbx561Source.Width = 80;
            // 
            // lb6402
            // 
            this.lb6402.Name = "lb6402";
            this.lb6402.Text = "640nm：";
            // 
            // cbx640Ctr
            // 
            this.cbx640Ctr.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.cbx640Ctr.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx640Ctr.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx640Ctr.Name = "cbx640Ctr";
            this.cbx640Ctr.Width = 80;
            // 
            // cbx640Source
            // 
            this.cbx640Source.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbx640Source.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbx640Source.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx640Source.Name = "cbx640Source";
            this.cbx640Source.Width = 80;
            // 
            // inputLabel1
            // 
            this.inputLabel1.Name = "inputLabel1";
            this.inputLabel1.Text = "X振镜：";
            // 
            // inputLabel2
            // 
            this.inputLabel2.Name = "inputLabel2";
            this.inputLabel2.Text = "X振镜：";
            // 
            // inputGroupHeader1
            // 
            this.inputGroupHeader1.Collapsible = true;
            this.inputGroupHeader1.Name = "inputGroupHeader1";
            this.inputGroupHeader1.Text = "PMT";
            // 
            // inputLabel3
            // 
            this.inputLabel3.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.inputLabel3.Name = "inputLabel3";
            this.inputLabel3.Text = "信息提示";
            // 
            // inputLabel4
            // 
            this.inputLabel4.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.inputLabel4.Name = "inputLabel4";
            this.inputLabel4.Text = "信息提示";
            // 
            // inputLabel5
            // 
            this.inputLabel5.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.inputLabel5.Name = "inputLabel5";
            this.inputLabel5.Text = "信息提示";
            // 
            // FormSysSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 569);
            this.Controls.Add(this.inputPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSysSettings";
            this.Text = "系统配置";
            this.Load += new System.EventHandler(this.FormSysSettingsLoad);
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1InputPanel.C1InputPanel inputPanel;
        private C1.Win.C1InputPanel.InputGroupHeader ghScanControl;
        private C1.Win.C1InputPanel.InputLabel lbXGalvo;
        private C1.Win.C1InputPanel.InputComboBox cbxXGalvo;
        private C1.Win.C1InputPanel.InputLabel inputLabel1;
        private C1.Win.C1InputPanel.InputLabel inputLabel2;
        private C1.Win.C1InputPanel.InputLabel lbInfo;
        private C1.Win.C1InputPanel.InputLabel lbYGalvo;
        private C1.Win.C1InputPanel.InputComboBox cbxYGalvo;
        private C1.Win.C1InputPanel.InputLabel lbYGalvo2;
        private C1.Win.C1InputPanel.InputComboBox cbxYGalvo2;
        private C1.Win.C1InputPanel.InputGroupHeader ghScanPara;
        private C1.Win.C1InputPanel.InputLabel lbGalvoResponseTime;
        private C1.Win.C1InputPanel.InputNumericBox nbGalvoResponseTime;
        private C1.Win.C1InputPanel.InputLabel lbGalvoResponseTimeUnit;
        private C1.Win.C1InputPanel.InputLabel lbScanRange;
        private C1.Win.C1InputPanel.InputLabel lbScanRangeUnit;
        private C1.Win.C1InputPanel.InputNumericBox nbScanRange;
        private C1.Win.C1InputPanel.InputSeparator inputSeparator1;
        private C1.Win.C1InputPanel.InputLabel lbXGalvaOffset;
        private C1.Win.C1InputPanel.InputTextBox tbxXGalvoOffset;
        private C1.Win.C1InputPanel.InputLabel lbYGalvoOffset;
        private C1.Win.C1InputPanel.InputTextBox tbxYGalvoOffset;
        private C1.Win.C1InputPanel.InputLabel lbXGalvoScaleFactor;
        private C1.Win.C1InputPanel.InputTextBox tbxXGalvoScaleFactor;
        private C1.Win.C1InputPanel.InputLabel lbYGalvoScaleFactor;
        private C1.Win.C1InputPanel.InputTextBox tbxYGalvoScaleFactor;
        private C1.Win.C1InputPanel.InputLabel lbUnit1;
        private C1.Win.C1InputPanel.InputLabel lbUnit2;
        private C1.Win.C1InputPanel.InputGroupHeader ghAcq;
        private C1.Win.C1InputPanel.InputRadioButton rbtnPMT;
        private C1.Win.C1InputPanel.InputRadioButton rbtnAPD;
        private C1.Win.C1InputPanel.InputLabel lbStartSync;
        private C1.Win.C1InputPanel.InputLabel lbTrigger;
        private C1.Win.C1InputPanel.InputComboBox cbxStartSync;
        private C1.Win.C1InputPanel.InputComboBox cbxTrigger;
        private C1.Win.C1InputPanel.InputGroupHeader ghPMT;
        private C1.Win.C1InputPanel.InputLabel lb405;
        private C1.Win.C1InputPanel.InputComboBox cbx405PMT;
        private C1.Win.C1InputPanel.InputLabel lb488;
        private C1.Win.C1InputPanel.InputComboBox cbx488PMT;
        private C1.Win.C1InputPanel.InputLabel lb561;
        private C1.Win.C1InputPanel.InputComboBox cbx561PMT;
        private C1.Win.C1InputPanel.InputLabel lb640;
        private C1.Win.C1InputPanel.InputComboBox cbx640PMT;
        private C1.Win.C1InputPanel.InputLabel lbTriggerR;
        private C1.Win.C1InputPanel.InputComboBox cbxTriggerR;
        private C1.Win.C1InputPanel.InputGroupHeader inputGroupHeader1;
        private C1.Win.C1InputPanel.InputGroupHeader ghAPD;
        private C1.Win.C1InputPanel.InputLabel lb4052;
        private C1.Win.C1InputPanel.InputLabel lb4882;
        private C1.Win.C1InputPanel.InputLabel lb5612;
        private C1.Win.C1InputPanel.InputLabel lb6402;
        private C1.Win.C1InputPanel.InputComboBox cbx405CtrSource;
        private C1.Win.C1InputPanel.InputComboBox cbx405Ctr;
        private C1.Win.C1InputPanel.InputComboBox cbx488Source;
        private C1.Win.C1InputPanel.InputComboBox cbx488Ctr;
        private C1.Win.C1InputPanel.InputComboBox cbx561Ctr;
        private C1.Win.C1InputPanel.InputComboBox cbx561Source;
        private C1.Win.C1InputPanel.InputComboBox cbx640Ctr;
        private C1.Win.C1InputPanel.InputComboBox cbx640Source;
        private C1.Win.C1InputPanel.InputLabel lbCtr;
        private C1.Win.C1InputPanel.InputLabel lbAPDSource;
        private C1.Win.C1InputPanel.InputGroupHeader ghDevice;
        private C1.Win.C1InputPanel.InputLabel inputLabel3;
        private C1.Win.C1InputPanel.InputLabel inputLabel4;
        private C1.Win.C1InputPanel.InputLabel inputLabel5;
        private C1.Win.C1InputPanel.InputButton btnSearch;
    }
}