
namespace confocal_ui.View
{
    partial class FormConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.inputPanel = new C1.Win.C1InputPanel.C1InputPanel();
            this.ghScanControl = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lbXGalvo = new C1.Win.C1InputPanel.InputLabel();
            this.cbxXGalvo = new C1.Win.C1InputPanel.InputComboBox();
            this.inputLabel1 = new C1.Win.C1InputPanel.InputLabel();
            this.inputLabel2 = new C1.Win.C1InputPanel.InputLabel();
            this.lbInfo = new C1.Win.C1InputPanel.InputLabel();
            this.lbYGalvo = new C1.Win.C1InputPanel.InputLabel();
            this.lbYGalvo2 = new C1.Win.C1InputPanel.InputLabel();
            this.cbxYGalvo = new C1.Win.C1InputPanel.InputComboBox();
            this.cbxYGalvo2 = new C1.Win.C1InputPanel.InputComboBox();
            this.ghScanPara = new C1.Win.C1InputPanel.InputGroupHeader();
            this.lbGalvoResponseTime = new C1.Win.C1InputPanel.InputLabel();
            this.lbScanRange = new C1.Win.C1InputPanel.InputLabel();
            this.lbGalvoResponseTimeUnit = new C1.Win.C1InputPanel.InputLabel();
            this.lbScanRangeUnit = new C1.Win.C1InputPanel.InputLabel();
            this.nbGalvoResponseTime = new C1.Win.C1InputPanel.InputNumericBox();
            this.nbScanRange = new C1.Win.C1InputPanel.InputNumericBox();
            this.lbXGalvaOffset = new C1.Win.C1InputPanel.InputLabel();
            this.inputSeparator1 = new C1.Win.C1InputPanel.InputSeparator();
            this.tbxXGalvoOffset = new C1.Win.C1InputPanel.InputTextBox();
            this.lbYGalvoOffset = new C1.Win.C1InputPanel.InputLabel();
            this.tbxYGalvoOffset = new C1.Win.C1InputPanel.InputTextBox();
            this.lbXGalvoScaleFactor = new C1.Win.C1InputPanel.InputLabel();
            this.lbYGalvoScaleFactor = new C1.Win.C1InputPanel.InputLabel();
            this.tbxXGalvoScaleFactor = new C1.Win.C1InputPanel.InputTextBox();
            this.inputTextBox2 = new C1.Win.C1InputPanel.InputTextBox();
            this.lbUnit2 = new C1.Win.C1InputPanel.InputLabel();
            this.lbUnit1 = new C1.Win.C1InputPanel.InputLabel();
            this.ghAcq = new C1.Win.C1InputPanel.InputGroupHeader();
            this.rbtnPMT = new C1.Win.C1InputPanel.InputRadioButton();
            this.rbtnAPD = new C1.Win.C1InputPanel.InputRadioButton();
            this.lbStartSync = new C1.Win.C1InputPanel.InputLabel();
            this.lbTrigger = new C1.Win.C1InputPanel.InputLabel();
            this.cbxTrigger = new C1.Win.C1InputPanel.InputComboBox();
            this.cbxStartSync = new C1.Win.C1InputPanel.InputComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
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
            this.inputPanel.Items.Add(this.inputTextBox2);
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
            this.inputPanel.Location = new System.Drawing.Point(0, 0);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(272, 429);
            this.inputPanel.TabIndex = 0;
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
            this.cbxXGalvo.Width = 60;
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
            // lbInfo
            // 
            this.lbInfo.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Text = "信息提示";
            // 
            // lbYGalvo
            // 
            this.lbYGalvo.Name = "lbYGalvo";
            this.lbYGalvo.Text = "Y振镜：";
            this.lbYGalvo.Width = 45;
            // 
            // lbYGalvo2
            // 
            this.lbYGalvo2.Name = "lbYGalvo2";
            this.lbYGalvo2.Text = "Y补偿镜：";
            // 
            // cbxYGalvo
            // 
            this.cbxYGalvo.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.cbxYGalvo.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxYGalvo.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxYGalvo.Name = "cbxYGalvo";
            this.cbxYGalvo.Width = 60;
            // 
            // cbxYGalvo2
            // 
            this.cbxYGalvo2.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.cbxYGalvo2.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxYGalvo2.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxYGalvo2.Name = "cbxYGalvo2";
            this.cbxYGalvo2.Width = 60;
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
            // lbScanRange
            // 
            this.lbScanRange.Name = "lbScanRange";
            this.lbScanRange.Text = "扫描视场大小：";
            // 
            // lbGalvoResponseTimeUnit
            // 
            this.lbGalvoResponseTimeUnit.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbGalvoResponseTimeUnit.Name = "lbGalvoResponseTimeUnit";
            this.lbGalvoResponseTimeUnit.Text = "us";
            // 
            // lbScanRangeUnit
            // 
            this.lbScanRangeUnit.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbScanRangeUnit.Name = "lbScanRangeUnit";
            this.lbScanRangeUnit.Text = "um";
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
            // lbXGalvaOffset
            // 
            this.lbXGalvaOffset.Name = "lbXGalvaOffset";
            this.lbXGalvaOffset.Text = "X振镜偏置：";
            this.lbXGalvaOffset.Width = 70;
            // 
            // inputSeparator1
            // 
            this.inputSeparator1.Name = "inputSeparator1";
            this.inputSeparator1.Width = 250;
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
            // lbYGalvoScaleFactor
            // 
            this.lbYGalvoScaleFactor.Name = "lbYGalvoScaleFactor";
            this.lbYGalvoScaleFactor.Text = "Y振镜标定电压：";
            this.lbYGalvoScaleFactor.Width = 95;
            // 
            // tbxXGalvoScaleFactor
            // 
            this.tbxXGalvoScaleFactor.Break = C1.Win.C1InputPanel.BreakType.None;
            this.tbxXGalvoScaleFactor.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxXGalvoScaleFactor.Name = "tbxXGalvoScaleFactor";
            this.tbxXGalvoScaleFactor.Width = 50;
            // 
            // inputTextBox2
            // 
            this.inputTextBox2.Break = C1.Win.C1InputPanel.BreakType.None;
            this.inputTextBox2.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.inputTextBox2.Name = "inputTextBox2";
            this.inputTextBox2.Width = 50;
            // 
            // lbUnit2
            // 
            this.lbUnit2.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbUnit2.Name = "lbUnit2";
            this.lbUnit2.Text = "V/nm";
            // 
            // lbUnit1
            // 
            this.lbUnit1.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbUnit1.Name = "lbUnit1";
            this.lbUnit1.Text = "V/nm";
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
            // cbxStartSync
            // 
            this.cbxStartSync.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxStartSync.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxStartSync.Name = "cbxStartSync";
            this.cbxStartSync.Width = 80;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 429);
            this.Controls.Add(this.inputPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormConfig";
            this.Text = "系统配置";
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
        private C1.Win.C1InputPanel.InputTextBox inputTextBox2;
        private C1.Win.C1InputPanel.InputLabel lbUnit1;
        private C1.Win.C1InputPanel.InputLabel lbUnit2;
        private C1.Win.C1InputPanel.InputGroupHeader ghAcq;
        private C1.Win.C1InputPanel.InputRadioButton rbtnPMT;
        private C1.Win.C1InputPanel.InputRadioButton rbtnAPD;
        private C1.Win.C1InputPanel.InputLabel lbStartSync;
        private C1.Win.C1InputPanel.InputLabel lbTrigger;
        private C1.Win.C1InputPanel.InputComboBox cbxStartSync;
        private C1.Win.C1InputPanel.InputComboBox cbxTrigger;
    }
}