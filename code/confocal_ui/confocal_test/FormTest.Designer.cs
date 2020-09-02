namespace confocal_test
{
    partial class FormTest
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTest));
            this.cbxLaser = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.lbLaser = new System.Windows.Forms.Label();
            this.btnLaserConnect = new System.Windows.Forms.Button();
            this.btnLaserRelease = new System.Windows.Forms.Button();
            this.gbx640 = new System.Windows.Forms.GroupBox();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.trackBar6 = new System.Windows.Forms.TrackBar();
            this.tbx640Cp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx640HV = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lb640 = new System.Windows.Forms.Label();
            this.chbx640 = new System.Windows.Forms.CheckBox();
            this.gbx561 = new System.Windows.Forms.GroupBox();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.tbx561Cp = new System.Windows.Forms.TextBox();
            this.lb561Cp = new System.Windows.Forms.Label();
            this.tbx561HV = new System.Windows.Forms.TextBox();
            this.lb561HV = new System.Windows.Forms.Label();
            this.lb561 = new System.Windows.Forms.Label();
            this.chbx561 = new System.Windows.Forms.CheckBox();
            this.gbx488 = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.tbx488Cp = new System.Windows.Forms.TextBox();
            this.lb488Cp = new System.Windows.Forms.Label();
            this.tbx488HV = new System.Windows.Forms.TextBox();
            this.lb488HV = new System.Windows.Forms.Label();
            this.lb488 = new System.Windows.Forms.Label();
            this.chbx488 = new System.Windows.Forms.CheckBox();
            this.gbxChan405 = new System.Windows.Forms.GroupBox();
            this.tb405Cp = new System.Windows.Forms.TrackBar();
            this.tb405HV = new System.Windows.Forms.TrackBar();
            this.tbx405Cp = new System.Windows.Forms.TextBox();
            this.lb405Cp = new System.Windows.Forms.Label();
            this.tbx405HV = new System.Windows.Forms.TextBox();
            this.lb405HV = new System.Windows.Forms.Label();
            this.lb405 = new System.Windows.Forms.Label();
            this.chbx405 = new System.Windows.Forms.CheckBox();
            this.btnArrayTest = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.gbx640.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).BeginInit();
            this.gbx561.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            this.gbx488.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.gbxChan405.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb405Cp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb405HV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxLaser
            // 
            this.cbxLaser.FormattingEnabled = true;
            this.cbxLaser.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cbxLaser.Location = new System.Drawing.Point(100, 6);
            this.cbxLaser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxLaser.Name = "cbxLaser";
            this.cbxLaser.Size = new System.Drawing.Size(121, 23);
            this.cbxLaser.TabIndex = 0;
            // 
            // lbLaser
            // 
            this.lbLaser.AutoSize = true;
            this.lbLaser.Location = new System.Drawing.Point(12, 9);
            this.lbLaser.Name = "lbLaser";
            this.lbLaser.Size = new System.Drawing.Size(82, 15);
            this.lbLaser.TabIndex = 1;
            this.lbLaser.Text = "激光端口：";
            // 
            // btnLaserConnect
            // 
            this.btnLaserConnect.Location = new System.Drawing.Point(227, 6);
            this.btnLaserConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLaserConnect.Name = "btnLaserConnect";
            this.btnLaserConnect.Size = new System.Drawing.Size(75, 22);
            this.btnLaserConnect.TabIndex = 2;
            this.btnLaserConnect.Text = "连接";
            this.btnLaserConnect.UseVisualStyleBackColor = true;
            this.btnLaserConnect.Click += new System.EventHandler(this.btnLaserConnect_Click);
            // 
            // btnLaserRelease
            // 
            this.btnLaserRelease.Location = new System.Drawing.Point(308, 6);
            this.btnLaserRelease.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLaserRelease.Name = "btnLaserRelease";
            this.btnLaserRelease.Size = new System.Drawing.Size(75, 22);
            this.btnLaserRelease.TabIndex = 3;
            this.btnLaserRelease.Text = "断开";
            this.btnLaserRelease.UseVisualStyleBackColor = true;
            this.btnLaserRelease.Click += new System.EventHandler(this.btnLaserRelease_Click);
            // 
            // gbx640
            // 
            this.gbx640.Controls.Add(this.trackBar5);
            this.gbx640.Controls.Add(this.trackBar6);
            this.gbx640.Controls.Add(this.tbx640Cp);
            this.gbx640.Controls.Add(this.label3);
            this.gbx640.Controls.Add(this.tbx640HV);
            this.gbx640.Controls.Add(this.label6);
            this.gbx640.Controls.Add(this.lb640);
            this.gbx640.Controls.Add(this.chbx640);
            this.gbx640.Location = new System.Drawing.Point(395, 188);
            this.gbx640.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbx640.Name = "gbx640";
            this.gbx640.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbx640.Size = new System.Drawing.Size(357, 120);
            this.gbx640.TabIndex = 31;
            this.gbx640.TabStop = false;
            // 
            // trackBar5
            // 
            this.trackBar5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.trackBar5.LargeChange = 1;
            this.trackBar5.Location = new System.Drawing.Point(65, 86);
            this.trackBar5.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar5.MaximumSize = new System.Drawing.Size(200, 25);
            this.trackBar5.Minimum = 1;
            this.trackBar5.MinimumSize = new System.Drawing.Size(187, 25);
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(200, 56);
            this.trackBar5.TabIndex = 24;
            this.trackBar5.TabStop = false;
            this.trackBar5.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar5.Value = 1;
            // 
            // trackBar6
            // 
            this.trackBar6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.trackBar6.LargeChange = 1;
            this.trackBar6.Location = new System.Drawing.Point(65, 55);
            this.trackBar6.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar6.MaximumSize = new System.Drawing.Size(200, 25);
            this.trackBar6.Minimum = 1;
            this.trackBar6.MinimumSize = new System.Drawing.Size(187, 25);
            this.trackBar6.Name = "trackBar6";
            this.trackBar6.Size = new System.Drawing.Size(200, 56);
            this.trackBar6.TabIndex = 22;
            this.trackBar6.TabStop = false;
            this.trackBar6.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar6.Value = 1;
            // 
            // tbx640Cp
            // 
            this.tbx640Cp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbx640Cp.Location = new System.Drawing.Point(288, 86);
            this.tbx640Cp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx640Cp.Name = "tbx640Cp";
            this.tbx640Cp.Size = new System.Drawing.Size(55, 25);
            this.tbx640Cp.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(5, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "补偿";
            // 
            // tbx640HV
            // 
            this.tbx640HV.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbx640HV.Location = new System.Drawing.Point(288, 55);
            this.tbx640HV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx640HV.Name = "tbx640HV";
            this.tbx640HV.Size = new System.Drawing.Size(55, 25);
            this.tbx640HV.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(5, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "HV";
            // 
            // lb640
            // 
            this.lb640.AutoSize = true;
            this.lb640.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb640.Location = new System.Drawing.Point(179, 25);
            this.lb640.Name = "lb640";
            this.lb640.Size = new System.Drawing.Size(111, 15);
            this.lb640.TabIndex = 21;
            this.lb640.Text = "Laser 640.0nm";
            // 
            // chbx640
            // 
            this.chbx640.AutoSize = true;
            this.chbx640.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chbx640.Location = new System.Drawing.Point(9, 24);
            this.chbx640.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbx640.Name = "chbx640";
            this.chbx640.Size = new System.Drawing.Size(69, 19);
            this.chbx640.TabIndex = 20;
            this.chbx640.Text = "CY5.5";
            this.chbx640.UseVisualStyleBackColor = true;
            // 
            // gbx561
            // 
            this.gbx561.Controls.Add(this.trackBar3);
            this.gbx561.Controls.Add(this.trackBar4);
            this.gbx561.Controls.Add(this.tbx561Cp);
            this.gbx561.Controls.Add(this.lb561Cp);
            this.gbx561.Controls.Add(this.tbx561HV);
            this.gbx561.Controls.Add(this.lb561HV);
            this.gbx561.Controls.Add(this.lb561);
            this.gbx561.Controls.Add(this.chbx561);
            this.gbx561.Location = new System.Drawing.Point(15, 178);
            this.gbx561.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbx561.Name = "gbx561";
            this.gbx561.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbx561.Size = new System.Drawing.Size(357, 120);
            this.gbx561.TabIndex = 30;
            this.gbx561.TabStop = false;
            // 
            // trackBar3
            // 
            this.trackBar3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.trackBar3.LargeChange = 1;
            this.trackBar3.Location = new System.Drawing.Point(65, 86);
            this.trackBar3.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar3.MaximumSize = new System.Drawing.Size(200, 25);
            this.trackBar3.Minimum = 1;
            this.trackBar3.MinimumSize = new System.Drawing.Size(187, 25);
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(200, 56);
            this.trackBar3.TabIndex = 24;
            this.trackBar3.TabStop = false;
            this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar3.Value = 1;
            // 
            // trackBar4
            // 
            this.trackBar4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.trackBar4.LargeChange = 1;
            this.trackBar4.Location = new System.Drawing.Point(65, 55);
            this.trackBar4.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar4.MaximumSize = new System.Drawing.Size(200, 25);
            this.trackBar4.Minimum = 1;
            this.trackBar4.MinimumSize = new System.Drawing.Size(187, 25);
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(200, 56);
            this.trackBar4.TabIndex = 22;
            this.trackBar4.TabStop = false;
            this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar4.Value = 1;
            // 
            // tbx561Cp
            // 
            this.tbx561Cp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbx561Cp.Location = new System.Drawing.Point(288, 86);
            this.tbx561Cp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx561Cp.Name = "tbx561Cp";
            this.tbx561Cp.Size = new System.Drawing.Size(55, 25);
            this.tbx561Cp.TabIndex = 23;
            // 
            // lb561Cp
            // 
            this.lb561Cp.AutoSize = true;
            this.lb561Cp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb561Cp.Location = new System.Drawing.Point(5, 89);
            this.lb561Cp.Name = "lb561Cp";
            this.lb561Cp.Size = new System.Drawing.Size(37, 15);
            this.lb561Cp.TabIndex = 23;
            this.lb561Cp.Text = "补偿";
            // 
            // tbx561HV
            // 
            this.tbx561HV.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbx561HV.Location = new System.Drawing.Point(288, 55);
            this.tbx561HV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx561HV.Name = "tbx561HV";
            this.tbx561HV.Size = new System.Drawing.Size(55, 25);
            this.tbx561HV.TabIndex = 22;
            // 
            // lb561HV
            // 
            this.lb561HV.AutoSize = true;
            this.lb561HV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb561HV.Location = new System.Drawing.Point(5, 58);
            this.lb561HV.Name = "lb561HV";
            this.lb561HV.Size = new System.Drawing.Size(23, 15);
            this.lb561HV.TabIndex = 22;
            this.lb561HV.Text = "HV";
            // 
            // lb561
            // 
            this.lb561.AutoSize = true;
            this.lb561.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb561.Location = new System.Drawing.Point(179, 25);
            this.lb561.Name = "lb561";
            this.lb561.Size = new System.Drawing.Size(111, 15);
            this.lb561.TabIndex = 21;
            this.lb561.Text = "Laser 561.0nm";
            // 
            // chbx561
            // 
            this.chbx561.AutoSize = true;
            this.chbx561.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chbx561.Location = new System.Drawing.Point(9, 24);
            this.chbx561.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbx561.Name = "chbx561";
            this.chbx561.Size = new System.Drawing.Size(69, 19);
            this.chbx561.TabIndex = 20;
            this.chbx561.Text = "TRITC";
            this.chbx561.UseVisualStyleBackColor = true;
            // 
            // gbx488
            // 
            this.gbx488.Controls.Add(this.trackBar1);
            this.gbx488.Controls.Add(this.trackBar2);
            this.gbx488.Controls.Add(this.tbx488Cp);
            this.gbx488.Controls.Add(this.lb488Cp);
            this.gbx488.Controls.Add(this.tbx488HV);
            this.gbx488.Controls.Add(this.lb488HV);
            this.gbx488.Controls.Add(this.lb488);
            this.gbx488.Controls.Add(this.chbx488);
            this.gbx488.Location = new System.Drawing.Point(395, 51);
            this.gbx488.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbx488.Name = "gbx488";
            this.gbx488.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbx488.Size = new System.Drawing.Size(357, 120);
            this.gbx488.TabIndex = 29;
            this.gbx488.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(65, 86);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar1.MaximumSize = new System.Drawing.Size(200, 25);
            this.trackBar1.Minimum = 1;
            this.trackBar1.MinimumSize = new System.Drawing.Size(187, 25);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(200, 56);
            this.trackBar1.TabIndex = 24;
            this.trackBar1.TabStop = false;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 1;
            // 
            // trackBar2
            // 
            this.trackBar2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.trackBar2.LargeChange = 1;
            this.trackBar2.Location = new System.Drawing.Point(65, 55);
            this.trackBar2.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar2.MaximumSize = new System.Drawing.Size(200, 25);
            this.trackBar2.Minimum = 1;
            this.trackBar2.MinimumSize = new System.Drawing.Size(187, 25);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(200, 56);
            this.trackBar2.TabIndex = 22;
            this.trackBar2.TabStop = false;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar2.Value = 1;
            // 
            // tbx488Cp
            // 
            this.tbx488Cp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbx488Cp.Location = new System.Drawing.Point(288, 86);
            this.tbx488Cp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx488Cp.Name = "tbx488Cp";
            this.tbx488Cp.Size = new System.Drawing.Size(55, 25);
            this.tbx488Cp.TabIndex = 23;
            // 
            // lb488Cp
            // 
            this.lb488Cp.AutoSize = true;
            this.lb488Cp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb488Cp.Location = new System.Drawing.Point(5, 89);
            this.lb488Cp.Name = "lb488Cp";
            this.lb488Cp.Size = new System.Drawing.Size(37, 15);
            this.lb488Cp.TabIndex = 23;
            this.lb488Cp.Text = "补偿";
            // 
            // tbx488HV
            // 
            this.tbx488HV.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbx488HV.Location = new System.Drawing.Point(288, 55);
            this.tbx488HV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx488HV.Name = "tbx488HV";
            this.tbx488HV.Size = new System.Drawing.Size(55, 25);
            this.tbx488HV.TabIndex = 22;
            // 
            // lb488HV
            // 
            this.lb488HV.AutoSize = true;
            this.lb488HV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb488HV.Location = new System.Drawing.Point(5, 58);
            this.lb488HV.Name = "lb488HV";
            this.lb488HV.Size = new System.Drawing.Size(23, 15);
            this.lb488HV.TabIndex = 22;
            this.lb488HV.Text = "HV";
            // 
            // lb488
            // 
            this.lb488.AutoSize = true;
            this.lb488.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb488.Location = new System.Drawing.Point(179, 25);
            this.lb488.Name = "lb488";
            this.lb488.Size = new System.Drawing.Size(111, 15);
            this.lb488.TabIndex = 21;
            this.lb488.Text = "Laser 488.0nm";
            // 
            // chbx488
            // 
            this.chbx488.AutoSize = true;
            this.chbx488.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chbx488.Location = new System.Drawing.Point(9, 24);
            this.chbx488.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbx488.Name = "chbx488";
            this.chbx488.Size = new System.Drawing.Size(61, 19);
            this.chbx488.TabIndex = 20;
            this.chbx488.Text = "FITC";
            this.chbx488.UseVisualStyleBackColor = true;
            this.chbx488.CheckedChanged += new System.EventHandler(this.chbx488_CheckedChanged);
            // 
            // gbxChan405
            // 
            this.gbxChan405.Controls.Add(this.tb405Cp);
            this.gbxChan405.Controls.Add(this.tb405HV);
            this.gbxChan405.Controls.Add(this.tbx405Cp);
            this.gbxChan405.Controls.Add(this.lb405Cp);
            this.gbxChan405.Controls.Add(this.tbx405HV);
            this.gbxChan405.Controls.Add(this.lb405HV);
            this.gbxChan405.Controls.Add(this.lb405);
            this.gbxChan405.Controls.Add(this.chbx405);
            this.gbxChan405.Location = new System.Drawing.Point(15, 51);
            this.gbxChan405.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbxChan405.Name = "gbxChan405";
            this.gbxChan405.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbxChan405.Size = new System.Drawing.Size(357, 120);
            this.gbxChan405.TabIndex = 28;
            this.gbxChan405.TabStop = false;
            // 
            // tb405Cp
            // 
            this.tb405Cp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tb405Cp.LargeChange = 1;
            this.tb405Cp.Location = new System.Drawing.Point(65, 86);
            this.tb405Cp.Margin = new System.Windows.Forms.Padding(0);
            this.tb405Cp.MaximumSize = new System.Drawing.Size(200, 25);
            this.tb405Cp.Minimum = 1;
            this.tb405Cp.MinimumSize = new System.Drawing.Size(187, 25);
            this.tb405Cp.Name = "tb405Cp";
            this.tb405Cp.Size = new System.Drawing.Size(200, 56);
            this.tb405Cp.TabIndex = 24;
            this.tb405Cp.TabStop = false;
            this.tb405Cp.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tb405Cp.Value = 1;
            // 
            // tb405HV
            // 
            this.tb405HV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tb405HV.LargeChange = 1;
            this.tb405HV.Location = new System.Drawing.Point(65, 55);
            this.tb405HV.Margin = new System.Windows.Forms.Padding(0);
            this.tb405HV.MaximumSize = new System.Drawing.Size(200, 25);
            this.tb405HV.Minimum = 1;
            this.tb405HV.MinimumSize = new System.Drawing.Size(187, 25);
            this.tb405HV.Name = "tb405HV";
            this.tb405HV.Size = new System.Drawing.Size(200, 56);
            this.tb405HV.TabIndex = 22;
            this.tb405HV.TabStop = false;
            this.tb405HV.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tb405HV.Value = 1;
            // 
            // tbx405Cp
            // 
            this.tbx405Cp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbx405Cp.Location = new System.Drawing.Point(288, 86);
            this.tbx405Cp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx405Cp.Name = "tbx405Cp";
            this.tbx405Cp.Size = new System.Drawing.Size(55, 25);
            this.tbx405Cp.TabIndex = 23;
            // 
            // lb405Cp
            // 
            this.lb405Cp.AutoSize = true;
            this.lb405Cp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb405Cp.Location = new System.Drawing.Point(5, 89);
            this.lb405Cp.Name = "lb405Cp";
            this.lb405Cp.Size = new System.Drawing.Size(37, 15);
            this.lb405Cp.TabIndex = 23;
            this.lb405Cp.Text = "补偿";
            // 
            // tbx405HV
            // 
            this.tbx405HV.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbx405HV.Location = new System.Drawing.Point(288, 55);
            this.tbx405HV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbx405HV.Name = "tbx405HV";
            this.tbx405HV.Size = new System.Drawing.Size(55, 25);
            this.tbx405HV.TabIndex = 22;
            // 
            // lb405HV
            // 
            this.lb405HV.AutoSize = true;
            this.lb405HV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb405HV.Location = new System.Drawing.Point(5, 58);
            this.lb405HV.Name = "lb405HV";
            this.lb405HV.Size = new System.Drawing.Size(23, 15);
            this.lb405HV.TabIndex = 22;
            this.lb405HV.Text = "HV";
            // 
            // lb405
            // 
            this.lb405.AutoSize = true;
            this.lb405.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb405.Location = new System.Drawing.Point(179, 25);
            this.lb405.Name = "lb405";
            this.lb405.Size = new System.Drawing.Size(111, 15);
            this.lb405.TabIndex = 21;
            this.lb405.Text = "Laser 405.0nm";
            // 
            // chbx405
            // 
            this.chbx405.AutoSize = true;
            this.chbx405.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chbx405.Location = new System.Drawing.Point(9, 24);
            this.chbx405.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chbx405.Name = "chbx405";
            this.chbx405.Size = new System.Drawing.Size(61, 19);
            this.chbx405.TabIndex = 20;
            this.chbx405.Text = "DAPI";
            this.chbx405.UseVisualStyleBackColor = true;
            this.chbx405.CheckedChanged += new System.EventHandler(this.chbx405_CheckedChanged);
            // 
            // btnArrayTest
            // 
            this.btnArrayTest.Location = new System.Drawing.Point(16, 324);
            this.btnArrayTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnArrayTest.Name = "btnArrayTest";
            this.btnArrayTest.Size = new System.Drawing.Size(144, 76);
            this.btnArrayTest.TabIndex = 32;
            this.btnArrayTest.Text = "队列测试";
            this.btnArrayTest.UseVisualStyleBackColor = true;
            this.btnArrayTest.Click += new System.EventHandler(this.btnArrayTest_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(237, 336);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(133, 125);
            this.pictureBox.TabIndex = 33;
            this.pictureBox.TabStop = false;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(481, 372);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 552);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnArrayTest);
            this.Controls.Add(this.gbx640);
            this.Controls.Add(this.gbx561);
            this.Controls.Add(this.gbx488);
            this.Controls.Add(this.gbxChan405);
            this.Controls.Add(this.btnLaserRelease);
            this.Controls.Add(this.btnLaserConnect);
            this.Controls.Add(this.lbLaser);
            this.Controls.Add(this.cbxLaser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormTest";
            this.Text = "测试界面";
            this.Load += new System.EventHandler(this.FormTest_Load);
            this.gbx640.ResumeLayout(false);
            this.gbx640.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).EndInit();
            this.gbx561.ResumeLayout(false);
            this.gbx561.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            this.gbx488.ResumeLayout(false);
            this.gbx488.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.gbxChan405.ResumeLayout(false);
            this.gbxChan405.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb405Cp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb405HV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxLaser;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label lbLaser;
        private System.Windows.Forms.Button btnLaserConnect;
        private System.Windows.Forms.Button btnLaserRelease;
        private System.Windows.Forms.GroupBox gbx640;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.TrackBar trackBar6;
        private System.Windows.Forms.TextBox tbx640Cp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx640HV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lb640;
        private System.Windows.Forms.CheckBox chbx640;
        private System.Windows.Forms.GroupBox gbx561;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.TextBox tbx561Cp;
        private System.Windows.Forms.Label lb561Cp;
        private System.Windows.Forms.TextBox tbx561HV;
        private System.Windows.Forms.Label lb561HV;
        private System.Windows.Forms.Label lb561;
        private System.Windows.Forms.CheckBox chbx561;
        private System.Windows.Forms.GroupBox gbx488;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TextBox tbx488Cp;
        private System.Windows.Forms.Label lb488Cp;
        private System.Windows.Forms.TextBox tbx488HV;
        private System.Windows.Forms.Label lb488HV;
        private System.Windows.Forms.Label lb488;
        private System.Windows.Forms.CheckBox chbx488;
        private System.Windows.Forms.GroupBox gbxChan405;
        private System.Windows.Forms.TrackBar tb405Cp;
        private System.Windows.Forms.TrackBar tb405HV;
        private System.Windows.Forms.TextBox tbx405Cp;
        private System.Windows.Forms.Label lb405Cp;
        private System.Windows.Forms.TextBox tbx405HV;
        private System.Windows.Forms.Label lb405HV;
        private System.Windows.Forms.Label lb405;
        private System.Windows.Forms.CheckBox chbx405;
        private System.Windows.Forms.Button btnArrayTest;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button button1;
    }
}

