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
            this.gbxNI = new System.Windows.Forms.GroupBox();
            this.lbDevice = new System.Windows.Forms.Label();
            this.lbXMirror = new System.Windows.Forms.Label();
            this.lbYMirror = new System.Windows.Forms.Label();
            this.lbY2Mirror = new System.Windows.Forms.Label();
            this.cbxDevice = new System.Windows.Forms.ComboBox();
            this.cbxXMirror = new System.Windows.Forms.ComboBox();
            this.cbxYMirror = new System.Windows.Forms.ComboBox();
            this.cbxY2Mirror = new System.Windows.Forms.ComboBox();
            this.gbxGalv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFieldSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResponseTime)).BeginInit();
            this.gbxNI.SuspendLayout();
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
            this.gbxGalv.Location = new System.Drawing.Point(12, 165);
            this.gbxGalv.Name = "gbxGalv";
            this.gbxGalv.Size = new System.Drawing.Size(374, 163);
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
            this.tbxCurveCoff.Name = "tbxCurveCoff";
            this.tbxCurveCoff.Size = new System.Drawing.Size(93, 25);
            this.tbxCurveCoff.TabIndex = 12;
            this.tbxCurveCoff.Text = "10";
            // 
            // lbCurveCoff
            // 
            this.lbCurveCoff.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbCurveCoff.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCurveCoff.Location = new System.Drawing.Point(26, 131);
            this.lbCurveCoff.Name = "lbCurveCoff";
            this.lbCurveCoff.Size = new System.Drawing.Size(160, 22);
            this.lbCurveCoff.TabIndex = 11;
            this.lbCurveCoff.Text = "曲线系数（%）：";
            // 
            // tbxCalibrationV
            // 
            this.tbxCalibrationV.Location = new System.Drawing.Point(203, 94);
            this.tbxCalibrationV.Name = "tbxCalibrationV";
            this.tbxCalibrationV.Size = new System.Drawing.Size(93, 25);
            this.tbxCalibrationV.TabIndex = 10;
            this.tbxCalibrationV.Text = "4.09855e-5";
            // 
            // lbCalibrationV
            // 
            this.lbCalibrationV.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbCalibrationV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCalibrationV.Location = new System.Drawing.Point(26, 97);
            this.lbCalibrationV.Name = "lbCalibrationV";
            this.lbCalibrationV.Size = new System.Drawing.Size(160, 22);
            this.lbCalibrationV.TabIndex = 9;
            this.lbCalibrationV.Text = "标定电压 (V)：";
            // 
            // lbResponseTime
            // 
            this.lbResponseTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbResponseTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbResponseTime.Location = new System.Drawing.Point(26, 24);
            this.lbResponseTime.Name = "lbResponseTime";
            this.lbResponseTime.Size = new System.Drawing.Size(160, 22);
            this.lbResponseTime.TabIndex = 0;
            this.lbResponseTime.Text = "振镜响应时间（us）：";
            // 
            // lbFieldSize
            // 
            this.lbFieldSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbFieldSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbFieldSize.Location = new System.Drawing.Point(26, 58);
            this.lbFieldSize.Name = "lbFieldSize";
            this.lbFieldSize.Size = new System.Drawing.Size(160, 22);
            this.lbFieldSize.TabIndex = 2;
            this.lbFieldSize.Text = "视场大小 (um)：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(215, 334);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 38);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(311, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 38);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbxNI
            // 
            this.gbxNI.Controls.Add(this.cbxY2Mirror);
            this.gbxNI.Controls.Add(this.cbxYMirror);
            this.gbxNI.Controls.Add(this.lbY2Mirror);
            this.gbxNI.Controls.Add(this.cbxXMirror);
            this.gbxNI.Controls.Add(this.cbxDevice);
            this.gbxNI.Controls.Add(this.lbDevice);
            this.gbxNI.Controls.Add(this.lbYMirror);
            this.gbxNI.Controls.Add(this.lbXMirror);
            this.gbxNI.Location = new System.Drawing.Point(12, 12);
            this.gbxNI.Name = "gbxNI";
            this.gbxNI.Size = new System.Drawing.Size(374, 147);
            this.gbxNI.TabIndex = 8;
            this.gbxNI.TabStop = false;
            this.gbxNI.Text = "DAQ设备";
            // 
            // lbDevice
            // 
            this.lbDevice.AutoSize = true;
            this.lbDevice.Location = new System.Drawing.Point(26, 36);
            this.lbDevice.Name = "lbDevice";
            this.lbDevice.Size = new System.Drawing.Size(52, 15);
            this.lbDevice.TabIndex = 1;
            this.lbDevice.Text = "设备名";
            // 
            // lbXMirror
            // 
            this.lbXMirror.AutoSize = true;
            this.lbXMirror.Location = new System.Drawing.Point(26, 72);
            this.lbXMirror.Name = "lbXMirror";
            this.lbXMirror.Size = new System.Drawing.Size(45, 15);
            this.lbXMirror.TabIndex = 2;
            this.lbXMirror.Text = "X振镜";
            // 
            // lbYMirror
            // 
            this.lbYMirror.AutoSize = true;
            this.lbYMirror.Location = new System.Drawing.Point(26, 105);
            this.lbYMirror.Name = "lbYMirror";
            this.lbYMirror.Size = new System.Drawing.Size(45, 15);
            this.lbYMirror.TabIndex = 3;
            this.lbYMirror.Text = "Y振镜";
            // 
            // lbY2Mirror
            // 
            this.lbY2Mirror.AutoSize = true;
            this.lbY2Mirror.Location = new System.Drawing.Point(187, 108);
            this.lbY2Mirror.Name = "lbY2Mirror";
            this.lbY2Mirror.Size = new System.Drawing.Size(60, 15);
            this.lbY2Mirror.TabIndex = 4;
            this.lbY2Mirror.Text = "Y补偿镜";
            // 
            // cbxDevice
            // 
            this.cbxDevice.FormattingEnabled = true;
            this.cbxDevice.Location = new System.Drawing.Point(84, 33);
            this.cbxDevice.Name = "cbxDevice";
            this.cbxDevice.Size = new System.Drawing.Size(87, 23);
            this.cbxDevice.TabIndex = 9;
            // 
            // cbxXMirror
            // 
            this.cbxXMirror.FormattingEnabled = true;
            this.cbxXMirror.Location = new System.Drawing.Point(84, 69);
            this.cbxXMirror.Name = "cbxXMirror";
            this.cbxXMirror.Size = new System.Drawing.Size(87, 23);
            this.cbxXMirror.TabIndex = 10;
            // 
            // cbxYMirror
            // 
            this.cbxYMirror.FormattingEnabled = true;
            this.cbxYMirror.Location = new System.Drawing.Point(84, 105);
            this.cbxYMirror.Name = "cbxYMirror";
            this.cbxYMirror.Size = new System.Drawing.Size(87, 23);
            this.cbxYMirror.TabIndex = 11;
            // 
            // cbxY2Mirror
            // 
            this.cbxY2Mirror.FormattingEnabled = true;
            this.cbxY2Mirror.Location = new System.Drawing.Point(253, 105);
            this.cbxY2Mirror.Name = "cbxY2Mirror";
            this.cbxY2Mirror.Size = new System.Drawing.Size(87, 23);
            this.cbxY2Mirror.TabIndex = 12;
            // 
            // FormSysCfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 380);
            this.Controls.Add(this.gbxNI);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbxGalv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            this.gbxNI.ResumeLayout(false);
            this.gbxNI.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbxNI;
        private System.Windows.Forms.Label lbDevice;
        private System.Windows.Forms.Label lbXMirror;
        private System.Windows.Forms.Label lbYMirror;
        private System.Windows.Forms.Label lbY2Mirror;
        private System.Windows.Forms.ComboBox cbxDevice;
        private System.Windows.Forms.ComboBox cbxXMirror;
        private System.Windows.Forms.ComboBox cbxYMirror;
        private System.Windows.Forms.ComboBox cbxY2Mirror;
    }
}