namespace confocal_ui.View
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
            this.dockTop = new C1.Win.C1Command.C1CommandDock();
            this.toolBar = new C1.Win.C1Command.C1ToolBar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lbPixelSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.sp2 = new System.Windows.Forms.ToolStripSeparator();
            this.lbScanPixel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.dockRight = new C1.Win.C1Command.C1CommandDock();
            this.toolBarLeft = new C1.Win.C1Command.C1ToolBar();
            this.dockTab = new C1.Win.C1Command.C1DockingTab();
            this.pageAll = new C1.Win.C1Command.C1DockingTabPage();
            this.page405 = new C1.Win.C1Command.C1DockingTabPage();
            this.imageAll = new Emgu.CV.UI.ImageBox();
            this.image405 = new Emgu.CV.UI.ImageBox();
            this.page488 = new C1.Win.C1Command.C1DockingTabPage();
            this.image488 = new Emgu.CV.UI.ImageBox();
            this.page561 = new C1.Win.C1Command.C1DockingTabPage();
            this.page640 = new C1.Win.C1Command.C1DockingTabPage();
            this.image640 = new Emgu.CV.UI.ImageBox();
            this.imageB561 = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.dockTop)).BeginInit();
            this.dockTop.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockRight)).BeginInit();
            this.dockRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockTab)).BeginInit();
            this.dockTab.SuspendLayout();
            this.pageAll.SuspendLayout();
            this.page405.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image405)).BeginInit();
            this.page488.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image488)).BeginInit();
            this.page561.SuspendLayout();
            this.page640.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image640)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageB561)).BeginInit();
            this.SuspendLayout();
            // 
            // dockTop
            // 
            this.dockTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dockTop.Controls.Add(this.toolBar);
            this.dockTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dockTop.Id = 3;
            this.dockTop.Location = new System.Drawing.Point(0, 0);
            this.dockTop.Name = "dockTop";
            this.dockTop.Size = new System.Drawing.Size(592, 26);
            // 
            // toolBar
            // 
            this.toolBar.AccessibleName = "Tool Bar";
            this.toolBar.CommandHolder = null;
            this.toolBar.Location = new System.Drawing.Point(3, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(25, 25);
            this.toolBar.Text = "工具栏";
            this.toolBar.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.toolBar.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbPixelSize,
            this.sp2,
            this.lbScanPixel,
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
            this.statusStrip.Location = new System.Drawing.Point(0, 546);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip.Size = new System.Drawing.Size(592, 23);
            this.statusStrip.TabIndex = 13;
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
            // lbScanPixel
            // 
            this.lbScanPixel.BackColor = System.Drawing.Color.Transparent;
            this.lbScanPixel.Name = "lbScanPixel";
            this.lbScanPixel.Size = new System.Drawing.Size(101, 18);
            this.lbScanPixel.Text = "512 x 512 pixels";
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
            // dockRight
            // 
            this.dockRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dockRight.Controls.Add(this.toolBarLeft);
            this.dockRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dockRight.Id = 3;
            this.dockRight.Location = new System.Drawing.Point(566, 26);
            this.dockRight.Name = "dockRight";
            this.dockRight.Size = new System.Drawing.Size(26, 520);
            // 
            // toolBarLeft
            // 
            this.toolBarLeft.AccessibleName = "Tool Bar";
            this.toolBarLeft.CommandHolder = null;
            this.toolBarLeft.Horizontal = false;
            this.toolBarLeft.Location = new System.Drawing.Point(0, 0);
            this.toolBarLeft.Name = "toolBarLeft";
            this.toolBarLeft.Size = new System.Drawing.Size(25, 25);
            this.toolBarLeft.Text = "工具栏";
            this.toolBarLeft.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.toolBarLeft.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // dockTab
            // 
            this.dockTab.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.dockTab.Controls.Add(this.pageAll);
            this.dockTab.Controls.Add(this.page405);
            this.dockTab.Controls.Add(this.page488);
            this.dockTab.Controls.Add(this.page561);
            this.dockTab.Controls.Add(this.page640);
            this.dockTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockTab.Location = new System.Drawing.Point(0, 26);
            this.dockTab.Name = "dockTab";
            this.dockTab.SelectedIndex = 4;
            this.dockTab.Size = new System.Drawing.Size(566, 520);
            this.dockTab.TabIndex = 21;
            this.dockTab.TabsSpacing = 5;
            this.dockTab.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.dockTab.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
            // 
            // pageAll
            // 
            this.pageAll.Controls.Add(this.imageAll);
            this.pageAll.Location = new System.Drawing.Point(1, 1);
            this.pageAll.Name = "pageAll";
            this.pageAll.Size = new System.Drawing.Size(564, 494);
            this.pageAll.TabIndex = 0;
            this.pageAll.Text = "全部";
            // 
            // page405
            // 
            this.page405.Controls.Add(this.image405);
            this.page405.Location = new System.Drawing.Point(1, 1);
            this.page405.Name = "page405";
            this.page405.Size = new System.Drawing.Size(564, 494);
            this.page405.TabIndex = 1;
            this.page405.Text = "405nm";
            // 
            // imageAll
            // 
            this.imageAll.BackColor = System.Drawing.Color.Black;
            this.imageAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageAll.Location = new System.Drawing.Point(0, 0);
            this.imageAll.Name = "imageAll";
            this.imageAll.Size = new System.Drawing.Size(564, 494);
            this.imageAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageAll.TabIndex = 2;
            this.imageAll.TabStop = false;
            // 
            // image405
            // 
            this.image405.BackColor = System.Drawing.Color.Black;
            this.image405.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image405.Location = new System.Drawing.Point(0, 0);
            this.image405.Name = "image405";
            this.image405.Size = new System.Drawing.Size(564, 494);
            this.image405.TabIndex = 2;
            this.image405.TabStop = false;
            // 
            // page488
            // 
            this.page488.Controls.Add(this.image488);
            this.page488.Location = new System.Drawing.Point(1, 1);
            this.page488.Name = "page488";
            this.page488.Size = new System.Drawing.Size(564, 494);
            this.page488.TabIndex = 2;
            this.page488.Text = "488nm";
            // 
            // image488
            // 
            this.image488.BackColor = System.Drawing.Color.Black;
            this.image488.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image488.Location = new System.Drawing.Point(0, 0);
            this.image488.Name = "image488";
            this.image488.Size = new System.Drawing.Size(564, 494);
            this.image488.TabIndex = 2;
            this.image488.TabStop = false;
            // 
            // page561
            // 
            this.page561.Controls.Add(this.imageB561);
            this.page561.Location = new System.Drawing.Point(1, 1);
            this.page561.Name = "page561";
            this.page561.Size = new System.Drawing.Size(564, 494);
            this.page561.TabIndex = 3;
            this.page561.Text = "561nm";
            // 
            // page640
            // 
            this.page640.Controls.Add(this.image640);
            this.page640.Location = new System.Drawing.Point(1, 1);
            this.page640.Name = "page640";
            this.page640.Size = new System.Drawing.Size(564, 494);
            this.page640.TabIndex = 4;
            this.page640.Text = "640nm";
            // 
            // image640
            // 
            this.image640.BackColor = System.Drawing.Color.Black;
            this.image640.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image640.Location = new System.Drawing.Point(0, 0);
            this.image640.Name = "image640";
            this.image640.Size = new System.Drawing.Size(564, 494);
            this.image640.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image640.TabIndex = 2;
            this.image640.TabStop = false;
            // 
            // imageB561
            // 
            this.imageB561.BackColor = System.Drawing.Color.Black;
            this.imageB561.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageB561.Location = new System.Drawing.Point(0, 0);
            this.imageB561.Name = "imageB561";
            this.imageB561.Size = new System.Drawing.Size(564, 494);
            this.imageB561.TabIndex = 2;
            this.imageB561.TabStop = false;
            // 
            // FormImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 569);
            this.ControlBox = false;
            this.Controls.Add(this.dockTab);
            this.Controls.Add(this.dockRight);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dockTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "扫描图像";
            ((System.ComponentModel.ISupportInitialize)(this.dockTop)).EndInit();
            this.dockTop.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockRight)).EndInit();
            this.dockRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockTab)).EndInit();
            this.dockTab.ResumeLayout(false);
            this.pageAll.ResumeLayout(false);
            this.page405.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image405)).EndInit();
            this.page488.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.image488)).EndInit();
            this.page561.ResumeLayout(false);
            this.page640.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.image640)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageB561)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Command.C1CommandDock dockTop;
        private C1.Win.C1Command.C1ToolBar toolBar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lbPixelSize;
        private System.Windows.Forms.ToolStripSeparator sp2;
        private System.Windows.Forms.ToolStripStatusLabel lbScanPixel;
        private System.Windows.Forms.ToolStripSeparator sp1;
        private System.Windows.Forms.ToolStripStatusLabel lbBitDepth;
        private System.Windows.Forms.ToolStripSeparator sp4;
        private System.Windows.Forms.ToolStripStatusLabel lbFps;
        private System.Windows.Forms.ToolStripSeparator sp3;
        private System.Windows.Forms.ToolStripStatusLabel lbFrame;
        private System.Windows.Forms.ToolStripSeparator sp5;
        private System.Windows.Forms.ToolStripStatusLabel lbTimeSpan;
        private System.Windows.Forms.ToolStripSeparator sp6;
        private System.Windows.Forms.ToolStripStatusLabel lbCurrent;
        private C1.Win.C1Command.C1CommandDock dockRight;
        private C1.Win.C1Command.C1ToolBar toolBarLeft;
        private C1.Win.C1Command.C1DockingTab dockTab;
        private C1.Win.C1Command.C1DockingTabPage pageAll;
        private C1.Win.C1Command.C1DockingTabPage page405;
        private Emgu.CV.UI.ImageBox imageAll;
        private Emgu.CV.UI.ImageBox image405;
        private C1.Win.C1Command.C1DockingTabPage page488;
        private Emgu.CV.UI.ImageBox image488;
        private C1.Win.C1Command.C1DockingTabPage page561;
        private C1.Win.C1Command.C1DockingTabPage page640;
        private Emgu.CV.UI.ImageBox image640;
        private Emgu.CV.UI.ImageBox imageB561;
    }
}