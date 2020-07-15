namespace confocal_ui
{
    partial class FormImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImage));
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lbPixelSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.sp2 = new System.Windows.Forms.ToolStripSeparator();
            this.lbScanPixels = new System.Windows.Forms.ToolStripStatusLabel();
            this.sp1 = new System.Windows.Forms.ToolStripSeparator();
            this.lbBitDepth = new System.Windows.Forms.ToolStripStatusLabel();
            this.sp4 = new System.Windows.Forms.ToolStripSeparator();
            this.lbFps = new System.Windows.Forms.ToolStripStatusLabel();
            this.sp3 = new System.Windows.Forms.ToolStripSeparator();
            this.lbFrame = new System.Windows.Forms.ToolStripStatusLabel();
            this.sp5 = new System.Windows.Forms.ToolStripSeparator();
            this.lbTimeSpan = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStripRight = new System.Windows.Forms.ToolStrip();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpgAll = new System.Windows.Forms.TabPage();
            this.tpg405 = new System.Windows.Forms.TabPage();
            this.tpg488 = new System.Windows.Forms.TabPage();
            this.tpg561 = new System.Windows.Forms.TabPage();
            this.tpg640 = new System.Windows.Forms.TabPage();
            this.pbxAll = new System.Windows.Forms.PictureBox();
            this.pbx405 = new System.Windows.Forms.PictureBox();
            this.pbx488 = new System.Windows.Forms.PictureBox();
            this.pbx561 = new System.Windows.Forms.PictureBox();
            this.pbx640 = new System.Windows.Forms.PictureBox();
            this.btnDisplayCenter = new System.Windows.Forms.ToolStripButton();
            this.btnDisplaytNormal = new System.Windows.Forms.ToolStripButton();
            this.btnDisplayAutosize = new System.Windows.Forms.ToolStripButton();
            this.btnDisplayStretch = new System.Windows.Forms.ToolStripButton();
            this.btnDisplayZoom = new System.Windows.Forms.ToolStripButton();
            this.statusStrip.SuspendLayout();
            this.toolStripRight.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpgAll.SuspendLayout();
            this.tpg405.SuspendLayout();
            this.tpg488.SuspendLayout();
            this.tpg561.SuspendLayout();
            this.tpg640.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx405)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx488)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx561)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx640)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripTop
            // 
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(689, 25);
            this.toolStripTop.TabIndex = 1;
            this.toolStripTop.Text = "toolStrip";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbPixelSize,
            this.sp2,
            this.lbScanPixels,
            this.sp1,
            this.lbBitDepth,
            this.sp4,
            this.lbFps,
            this.sp3,
            this.lbFrame,
            this.sp5,
            this.lbTimeSpan});
            this.statusStrip.Location = new System.Drawing.Point(0, 578);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(689, 25);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // lbPixelSize
            // 
            this.lbPixelSize.BackColor = System.Drawing.Color.Transparent;
            this.lbPixelSize.Name = "lbPixelSize";
            this.lbPixelSize.Size = new System.Drawing.Size(79, 20);
            this.lbPixelSize.Text = "{0} um/px";
            // 
            // sp2
            // 
            this.sp2.Name = "sp2";
            this.sp2.Size = new System.Drawing.Size(6, 25);
            // 
            // lbScanPixels
            // 
            this.lbScanPixels.BackColor = System.Drawing.Color.Transparent;
            this.lbScanPixels.Name = "lbScanPixels";
            this.lbScanPixels.Size = new System.Drawing.Size(125, 20);
            this.lbScanPixels.Text = "512 x 512 pixels";
            // 
            // sp1
            // 
            this.sp1.Name = "sp1";
            this.sp1.Size = new System.Drawing.Size(6, 25);
            // 
            // lbBitDepth
            // 
            this.lbBitDepth.BackColor = System.Drawing.Color.Transparent;
            this.lbBitDepth.Name = "lbBitDepth";
            this.lbBitDepth.Size = new System.Drawing.Size(54, 20);
            this.lbBitDepth.Text = "16bits";
            // 
            // sp4
            // 
            this.sp4.Name = "sp4";
            this.sp4.Size = new System.Drawing.Size(6, 25);
            // 
            // lbFps
            // 
            this.lbFps.BackColor = System.Drawing.Color.Transparent;
            this.lbFps.Name = "lbFps";
            this.lbFps.Size = new System.Drawing.Size(66, 20);
            this.lbFps.Text = "1.28 fps";
            // 
            // sp3
            // 
            this.sp3.Name = "sp3";
            this.sp3.Size = new System.Drawing.Size(6, 25);
            // 
            // lbFrame
            // 
            this.lbFrame.BackColor = System.Drawing.Color.Transparent;
            this.lbFrame.Name = "lbFrame";
            this.lbFrame.Size = new System.Drawing.Size(96, 20);
            this.lbFrame.Text = "NO. 1 frame";
            // 
            // sp5
            // 
            this.sp5.Name = "sp5";
            this.sp5.Size = new System.Drawing.Size(6, 25);
            // 
            // lbTimeSpan
            // 
            this.lbTimeSpan.BackColor = System.Drawing.Color.Transparent;
            this.lbTimeSpan.Name = "lbTimeSpan";
            this.lbTimeSpan.Size = new System.Drawing.Size(89, 20);
            this.lbTimeSpan.Text = "xx seconds";
            // 
            // timer
            // 
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // toolStripRight
            // 
            this.toolStripRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStripRight.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDisplayCenter,
            this.btnDisplaytNormal,
            this.btnDisplayAutosize,
            this.btnDisplayStretch,
            this.btnDisplayZoom});
            this.toolStripRight.Location = new System.Drawing.Point(664, 25);
            this.toolStripRight.Name = "toolStripRight";
            this.toolStripRight.Size = new System.Drawing.Size(25, 553);
            this.toolStripRight.TabIndex = 5;
            this.toolStripRight.Text = "toolStrip";
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.tpgAll);
            this.tabControl.Controls.Add(this.tpg405);
            this.tabControl.Controls.Add(this.tpg488);
            this.tabControl.Controls.Add(this.tpg561);
            this.tabControl.Controls.Add(this.tpg640);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl.SelectedIndex = 0;
            this.tabControl.ShowToolTips = true;
            this.tabControl.Size = new System.Drawing.Size(664, 553);
            this.tabControl.TabIndex = 6;
            // 
            // tpgAll
            // 
            this.tpgAll.BackColor = System.Drawing.Color.Transparent;
            this.tpgAll.Controls.Add(this.pbxAll);
            this.tpgAll.Location = new System.Drawing.Point(4, 4);
            this.tpgAll.Name = "tpgAll";
            this.tpgAll.Padding = new System.Windows.Forms.Padding(3);
            this.tpgAll.Size = new System.Drawing.Size(656, 524);
            this.tpgAll.TabIndex = 0;
            this.tpgAll.Tag = "-1";
            this.tpgAll.Text = "全部";
            // 
            // tpg405
            // 
            this.tpg405.Controls.Add(this.pbx405);
            this.tpg405.Location = new System.Drawing.Point(4, 4);
            this.tpg405.Name = "tpg405";
            this.tpg405.Padding = new System.Windows.Forms.Padding(3);
            this.tpg405.Size = new System.Drawing.Size(656, 524);
            this.tpg405.TabIndex = 1;
            this.tpg405.Tag = "0";
            this.tpg405.Text = "405nm";
            this.tpg405.UseVisualStyleBackColor = true;
            // 
            // tpg488
            // 
            this.tpg488.Controls.Add(this.pbx488);
            this.tpg488.Location = new System.Drawing.Point(4, 4);
            this.tpg488.Name = "tpg488";
            this.tpg488.Size = new System.Drawing.Size(656, 524);
            this.tpg488.TabIndex = 2;
            this.tpg488.Tag = "1";
            this.tpg488.Text = "488nm";
            this.tpg488.UseVisualStyleBackColor = true;
            // 
            // tpg561
            // 
            this.tpg561.Controls.Add(this.pbx561);
            this.tpg561.Location = new System.Drawing.Point(4, 4);
            this.tpg561.Name = "tpg561";
            this.tpg561.Size = new System.Drawing.Size(656, 524);
            this.tpg561.TabIndex = 3;
            this.tpg561.Tag = "2";
            this.tpg561.Text = "561nm";
            this.tpg561.UseVisualStyleBackColor = true;
            // 
            // tpg640
            // 
            this.tpg640.Controls.Add(this.pbx640);
            this.tpg640.Location = new System.Drawing.Point(4, 4);
            this.tpg640.Name = "tpg640";
            this.tpg640.Size = new System.Drawing.Size(656, 524);
            this.tpg640.TabIndex = 4;
            this.tpg640.Tag = "3";
            this.tpg640.Text = "640nm";
            this.tpg640.UseVisualStyleBackColor = true;
            // 
            // pbxAll
            // 
            this.pbxAll.BackgroundImage = global::confocal_ui.Properties.Resources.background;
            this.pbxAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxAll.Location = new System.Drawing.Point(3, 3);
            this.pbxAll.Name = "pbxAll";
            this.pbxAll.Size = new System.Drawing.Size(650, 518);
            this.pbxAll.TabIndex = 0;
            this.pbxAll.TabStop = false;
            // 
            // pbx405
            // 
            this.pbx405.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbx405.Location = new System.Drawing.Point(3, 3);
            this.pbx405.Name = "pbx405";
            this.pbx405.Size = new System.Drawing.Size(650, 518);
            this.pbx405.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbx405.TabIndex = 0;
            this.pbx405.TabStop = false;
            // 
            // pbx488
            // 
            this.pbx488.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbx488.Location = new System.Drawing.Point(0, 0);
            this.pbx488.Name = "pbx488";
            this.pbx488.Size = new System.Drawing.Size(656, 524);
            this.pbx488.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbx488.TabIndex = 0;
            this.pbx488.TabStop = false;
            // 
            // pbx561
            // 
            this.pbx561.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbx561.Location = new System.Drawing.Point(0, 0);
            this.pbx561.Name = "pbx561";
            this.pbx561.Size = new System.Drawing.Size(656, 524);
            this.pbx561.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbx561.TabIndex = 0;
            this.pbx561.TabStop = false;
            // 
            // pbx640
            // 
            this.pbx640.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbx640.Location = new System.Drawing.Point(0, 0);
            this.pbx640.Name = "pbx640";
            this.pbx640.Size = new System.Drawing.Size(656, 524);
            this.pbx640.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbx640.TabIndex = 0;
            this.pbx640.TabStop = false;
            // 
            // btnDisplayCenter
            // 
            this.btnDisplayCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDisplayCenter.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplayCenter.Image")));
            this.btnDisplayCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisplayCenter.Name = "btnDisplayCenter";
            this.btnDisplayCenter.Size = new System.Drawing.Size(22, 24);
            this.btnDisplayCenter.Text = "CenterIamge";
            this.btnDisplayCenter.ToolTipText = "CenterIamge";
            this.btnDisplayCenter.Click += new System.EventHandler(this.btnDisplayCenter_Click);
            // 
            // btnDisplaytNormal
            // 
            this.btnDisplaytNormal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDisplaytNormal.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplaytNormal.Image")));
            this.btnDisplaytNormal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisplaytNormal.Name = "btnDisplaytNormal";
            this.btnDisplaytNormal.Size = new System.Drawing.Size(22, 24);
            this.btnDisplaytNormal.Text = "Normal";
            this.btnDisplaytNormal.Click += new System.EventHandler(this.btnDisplaytNormal_Click);
            // 
            // btnDisplayAutosize
            // 
            this.btnDisplayAutosize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDisplayAutosize.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplayAutosize.Image")));
            this.btnDisplayAutosize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisplayAutosize.Name = "btnDisplayAutosize";
            this.btnDisplayAutosize.Size = new System.Drawing.Size(22, 24);
            this.btnDisplayAutosize.Text = "AutoSize";
            this.btnDisplayAutosize.Click += new System.EventHandler(this.btnDisplayAutosize_Click);
            // 
            // btnDisplayStretch
            // 
            this.btnDisplayStretch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDisplayStretch.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplayStretch.Image")));
            this.btnDisplayStretch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisplayStretch.Name = "btnDisplayStretch";
            this.btnDisplayStretch.Size = new System.Drawing.Size(22, 24);
            this.btnDisplayStretch.Text = "StretchImage";
            this.btnDisplayStretch.Click += new System.EventHandler(this.btnDisplayStretch_Click);
            // 
            // btnDisplayZoom
            // 
            this.btnDisplayZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDisplayZoom.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplayZoom.Image")));
            this.btnDisplayZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisplayZoom.Name = "btnDisplayZoom";
            this.btnDisplayZoom.Size = new System.Drawing.Size(22, 24);
            this.btnDisplayZoom.Text = "Zoom";
            this.btnDisplayZoom.Click += new System.EventHandler(this.btnDisplayZoom_Click);
            // 
            // FormImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(689, 603);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStripRight);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormImage";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "扫描图像";
            this.Load += new System.EventHandler(this.FormImage_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStripRight.ResumeLayout(false);
            this.toolStripRight.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tpgAll.ResumeLayout(false);
            this.tpg405.ResumeLayout(false);
            this.tpg488.ResumeLayout(false);
            this.tpg561.ResumeLayout(false);
            this.tpg640.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx405)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx488)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx561)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx640)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripStatusLabel lbPixelSize;
        private System.Windows.Forms.ToolStripSeparator sp1;
        private System.Windows.Forms.ToolStripStatusLabel lbScanPixels;
        private System.Windows.Forms.ToolStripSeparator sp2;
        private System.Windows.Forms.ToolStripStatusLabel lbBitDepth;
        private System.Windows.Forms.ToolStripSeparator sp3;
        private System.Windows.Forms.ToolStripSeparator sp4;
        private System.Windows.Forms.ToolStripStatusLabel lbFrame;
        private System.Windows.Forms.ToolStripStatusLabel lbFps;
        private System.Windows.Forms.ToolStripSeparator sp5;
        private System.Windows.Forms.ToolStripStatusLabel lbTimeSpan;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStrip toolStripRight;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpgAll;
        private System.Windows.Forms.TabPage tpg405;
        private System.Windows.Forms.PictureBox pbx405;
        private System.Windows.Forms.TabPage tpg488;
        private System.Windows.Forms.PictureBox pbx488;
        private System.Windows.Forms.TabPage tpg561;
        private System.Windows.Forms.PictureBox pbx561;
        private System.Windows.Forms.TabPage tpg640;
        private System.Windows.Forms.PictureBox pbx640;
        private System.Windows.Forms.ToolStripButton btnDisplaytNormal;
        private System.Windows.Forms.ToolStripButton btnDisplayCenter;
        private System.Windows.Forms.ToolStripButton btnDisplayAutosize;
        private System.Windows.Forms.ToolStripButton btnDisplayStretch;
        private System.Windows.Forms.ToolStripButton btnDisplayZoom;
        private System.Windows.Forms.PictureBox pbxAll;
    }
}