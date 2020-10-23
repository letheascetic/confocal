﻿namespace confocal_ui
{
    partial class FormDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDisplay));
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
            this.sp6 = new System.Windows.Forms.ToolStripSeparator();
            this.lbCurrent = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.cbxSelect = new System.Windows.Forms.ToolStripComboBox();
            this.ts1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnColor = new System.Windows.Forms.ToolStripButton();
            this.ts3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.ts2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripRight = new System.Windows.Forms.ToolStrip();
            this.btnDisplayCenter = new System.Windows.Forms.ToolStripButton();
            this.btnDisplayZoom = new System.Windows.Forms.ToolStripButton();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pictureBox = new Emgu.CV.UI.ImageBox();
            this.ibxColorMapping = new Emgu.CV.UI.ImageBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsResetZone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSelectZone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsStartAOI = new System.Windows.Forms.ToolStripMenuItem();
            this.m_cursorTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.toolStripTop.SuspendLayout();
            this.toolStripRight.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibxColorMapping)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
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
            this.lbTimeSpan,
            this.sp6,
            this.lbCurrent});
            this.statusStrip.Location = new System.Drawing.Point(0, 535);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(588, 23);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // lbPixelSize
            // 
            this.lbPixelSize.BackColor = System.Drawing.Color.Transparent;
            this.lbPixelSize.Name = "lbPixelSize";
            this.lbPixelSize.Size = new System.Drawing.Size(64, 18);
            this.lbPixelSize.Text = "{0} um/px";
            // 
            // sp2
            // 
            this.sp2.Name = "sp2";
            this.sp2.Size = new System.Drawing.Size(6, 23);
            // 
            // lbScanPixels
            // 
            this.lbScanPixels.BackColor = System.Drawing.Color.Transparent;
            this.lbScanPixels.Name = "lbScanPixels";
            this.lbScanPixels.Size = new System.Drawing.Size(101, 18);
            this.lbScanPixels.Text = "512 x 512 pixels";
            // 
            // sp1
            // 
            this.sp1.Name = "sp1";
            this.sp1.Size = new System.Drawing.Size(6, 23);
            // 
            // lbBitDepth
            // 
            this.lbBitDepth.BackColor = System.Drawing.Color.Transparent;
            this.lbBitDepth.Name = "lbBitDepth";
            this.lbBitDepth.Size = new System.Drawing.Size(43, 18);
            this.lbBitDepth.Text = "16bits";
            // 
            // sp4
            // 
            this.sp4.Name = "sp4";
            this.sp4.Size = new System.Drawing.Size(6, 23);
            // 
            // lbFps
            // 
            this.lbFps.BackColor = System.Drawing.Color.Transparent;
            this.lbFps.Name = "lbFps";
            this.lbFps.Size = new System.Drawing.Size(54, 18);
            this.lbFps.Text = "1.28 fps";
            // 
            // sp3
            // 
            this.sp3.Name = "sp3";
            this.sp3.Size = new System.Drawing.Size(6, 23);
            // 
            // lbFrame
            // 
            this.lbFrame.BackColor = System.Drawing.Color.Transparent;
            this.lbFrame.Name = "lbFrame";
            this.lbFrame.Size = new System.Drawing.Size(80, 18);
            this.lbFrame.Text = "NO. 1 frame";
            // 
            // sp5
            // 
            this.sp5.Name = "sp5";
            this.sp5.Size = new System.Drawing.Size(6, 23);
            // 
            // lbTimeSpan
            // 
            this.lbTimeSpan.BackColor = System.Drawing.Color.Transparent;
            this.lbTimeSpan.Name = "lbTimeSpan";
            this.lbTimeSpan.Size = new System.Drawing.Size(72, 18);
            this.lbTimeSpan.Text = "xx seconds";
            // 
            // sp6
            // 
            this.sp6.Name = "sp6";
            this.sp6.Size = new System.Drawing.Size(6, 23);
            // 
            // lbCurrent
            // 
            this.lbCurrent.Name = "lbCurrent";
            this.lbCurrent.Size = new System.Drawing.Size(101, 18);
            this.lbCurrent.Text = "[255, (512, 512)]";
            // 
            // m_timer
            // 
            this.m_timer.Interval = 200;
            this.m_timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // toolStripTop
            // 
            this.toolStripTop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbxSelect,
            this.ts1,
            this.btnColor,
            this.ts3,
            this.btnOpen,
            this.ts2,
            this.btnSave});
            this.toolStripTop.Location = new System.Drawing.Point(0, 508);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(588, 27);
            this.toolStripTop.TabIndex = 4;
            this.toolStripTop.Text = "toolStrip";
            // 
            // cbxSelect
            // 
            this.cbxSelect.DropDownHeight = 80;
            this.cbxSelect.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbxSelect.IntegralHeight = false;
            this.cbxSelect.Name = "cbxSelect";
            this.cbxSelect.Size = new System.Drawing.Size(75, 27);
            this.cbxSelect.ToolTipText = "通道选择";
            this.cbxSelect.SelectedIndexChanged += new System.EventHandler(this.cbxSelect_SelectedIndexChanged);
            // 
            // ts1
            // 
            this.ts1.Name = "ts1";
            this.ts1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnColor
            // 
            this.btnColor.BackColor = System.Drawing.Color.Gray;
            this.btnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.btnColor.Image = ((System.Drawing.Image)(resources.GetObject("btnColor.Image")));
            this.btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 24);
            this.btnColor.ToolTipText = "选择伪彩色";
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // ts3
            // 
            this.ts3.Name = "ts3";
            this.ts3.Size = new System.Drawing.Size(6, 27);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(24, 24);
            this.btnOpen.Text = "保存";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // ts2
            // 
            this.ts2.Name = "ts2";
            this.ts2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(24, 24);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripRight
            // 
            this.toolStripRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStripRight.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDisplayCenter,
            this.btnDisplayZoom});
            this.toolStripRight.Location = new System.Drawing.Point(563, 0);
            this.toolStripRight.Name = "toolStripRight";
            this.toolStripRight.Size = new System.Drawing.Size(25, 508);
            this.toolStripRight.TabIndex = 6;
            this.toolStripRight.Text = "toolStrip";
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
            // pnlImage
            // 
            this.pnlImage.AutoScroll = true;
            this.pnlImage.AutoSize = true;
            this.pnlImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImage.Controls.Add(this.pictureBox);
            this.pnlImage.Controls.Add(this.ibxColorMapping);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(0, 0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(563, 508);
            this.pnlImage.TabIndex = 7;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.RightClickMenu;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(563, 485);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // ibxColorMapping
            // 
            this.ibxColorMapping.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ibxColorMapping.Location = new System.Drawing.Point(0, 485);
            this.ibxColorMapping.Name = "ibxColorMapping";
            this.ibxColorMapping.Size = new System.Drawing.Size(563, 23);
            this.ibxColorMapping.TabIndex = 2;
            this.ibxColorMapping.TabStop = false;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsResetZone,
            this.tsSelectZone,
            this.tsStartAOI});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(148, 70);
            // 
            // tsResetZone
            // 
            this.tsResetZone.Name = "tsResetZone";
            this.tsResetZone.Size = new System.Drawing.Size(147, 22);
            this.tsResetZone.Text = "重置到视图全区域";
            // 
            // tsSelectZone
            // 
            this.tsSelectZone.Name = "tsSelectZone";
            this.tsSelectZone.Size = new System.Drawing.Size(147, 22);
            this.tsSelectZone.Text = "选择指定扫描区域";
            // 
            // tsStartAOI
            // 
            this.tsStartAOI.Name = "tsStartAOI";
            this.tsStartAOI.Size = new System.Drawing.Size(147, 22);
            this.tsStartAOI.Text = "扫描感兴趣区域";
            // 
            // m_cursorTimer
            // 
            this.m_cursorTimer.Interval = 500;
            this.m_cursorTimer.Tick += new System.EventHandler(this.m_cursorTimer_Tick);
            // 
            // FormDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(588, 558);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.toolStripRight);
            this.Controls.Add(this.toolStripTop);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "扫描图像";
            this.Load += new System.EventHandler(this.FormDisplay_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.toolStripRight.ResumeLayout(false);
            this.toolStripRight.PerformLayout();
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibxColorMapping)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lbPixelSize;
        private System.Windows.Forms.ToolStripSeparator sp2;
        private System.Windows.Forms.ToolStripStatusLabel lbScanPixels;
        private System.Windows.Forms.ToolStripSeparator sp1;
        private System.Windows.Forms.ToolStripStatusLabel lbBitDepth;
        private System.Windows.Forms.ToolStripSeparator sp4;
        private System.Windows.Forms.ToolStripStatusLabel lbFps;
        private System.Windows.Forms.ToolStripSeparator sp3;
        private System.Windows.Forms.ToolStripStatusLabel lbFrame;
        private System.Windows.Forms.ToolStripSeparator sp5;
        private System.Windows.Forms.ToolStripStatusLabel lbTimeSpan;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator ts2;
        private System.Windows.Forms.ToolStrip toolStripRight;
        private System.Windows.Forms.ToolStripButton btnDisplayCenter;
        private System.Windows.Forms.ToolStripButton btnDisplayZoom;
        private System.Windows.Forms.ToolStripComboBox cbxSelect;
        private System.Windows.Forms.ToolStripSeparator ts1;
        private System.Windows.Forms.ToolStripSeparator ts3;
        private System.Windows.Forms.ToolStripButton btnColor;
        private System.Windows.Forms.ToolStripSeparator sp6;
        private System.Windows.Forms.ToolStripStatusLabel lbCurrent;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.Timer m_cursorTimer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsSelectZone;
        private System.Windows.Forms.ToolStripMenuItem tsResetZone;
        private System.Windows.Forms.ToolStripMenuItem tsStartAOI;
        private Emgu.CV.UI.ImageBox ibxColorMapping;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private Emgu.CV.UI.ImageBox pictureBox;
    }
}