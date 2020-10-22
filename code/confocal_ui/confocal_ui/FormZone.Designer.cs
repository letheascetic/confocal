namespace confocal_ui
{
    partial class FormZone
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormZone));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lbPixelSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.sp = new System.Windows.Forms.ToolStripSeparator();
            this.lbFieldRange = new System.Windows.Forms.ToolStripStatusLabel();
            this.sp2 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.cbxScanPixels = new System.Windows.Forms.ComboBox();
            this.lbScanPixels = new System.Windows.Forms.Label();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pbxZone = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsResetZone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSelectZone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsStartAOI = new System.Windows.Forms.ToolStripMenuItem();
            this.pbxImage = new System.Windows.Forms.PictureBox();
            this.m_cursorTimer = new System.Windows.Forms.Timer(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxZone)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(382, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbPixelSize,
            this.sp,
            this.lbFieldRange,
            this.sp2});
            this.statusStrip.Location = new System.Drawing.Point(0, 375);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(382, 23);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lbPixelSize
            // 
            this.lbPixelSize.BackColor = System.Drawing.Color.Transparent;
            this.lbPixelSize.Name = "lbPixelSize";
            this.lbPixelSize.Size = new System.Drawing.Size(64, 18);
            this.lbPixelSize.Text = "{0} um/px";
            // 
            // sp
            // 
            this.sp.Name = "sp";
            this.sp.Size = new System.Drawing.Size(6, 23);
            // 
            // lbFieldRange
            // 
            this.lbFieldRange.Name = "lbFieldRange";
            this.lbFieldRange.Size = new System.Drawing.Size(215, 18);
            this.lbFieldRange.Text = "Field: [{0}um, {1}um] [{2}um x {3}um]";
            // 
            // sp2
            // 
            this.sp2.Name = "sp2";
            this.sp2.Size = new System.Drawing.Size(6, 23);
            // 
            // pnlBottom
            // 
            this.pnlBottom.AutoScroll = true;
            this.pnlBottom.AutoSize = true;
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBottom.Controls.Add(this.cbxScanPixels);
            this.pnlBottom.Controls.Add(this.lbScanPixels);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 341);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(382, 34);
            this.pnlBottom.TabIndex = 4;
            // 
            // cbxScanPixels
            // 
            this.cbxScanPixels.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbxScanPixels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxScanPixels.FormattingEnabled = true;
            this.cbxScanPixels.ItemHeight = 12;
            this.cbxScanPixels.Location = new System.Drawing.Point(74, 8);
            this.cbxScanPixels.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxScanPixels.Name = "cbxScanPixels";
            this.cbxScanPixels.Size = new System.Drawing.Size(80, 20);
            this.cbxScanPixels.TabIndex = 32;
            // 
            // lbScanPixels
            // 
            this.lbScanPixels.AutoSize = true;
            this.lbScanPixels.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbScanPixels.Location = new System.Drawing.Point(8, 10);
            this.lbScanPixels.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbScanPixels.Name = "lbScanPixels";
            this.lbScanPixels.Size = new System.Drawing.Size(65, 12);
            this.lbScanPixels.TabIndex = 31;
            this.lbScanPixels.Text = "扫描像素：";
            // 
            // pnlImage
            // 
            this.pnlImage.AutoScroll = true;
            this.pnlImage.AutoSize = true;
            this.pnlImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImage.Controls.Add(this.pbxZone);
            this.pnlImage.Controls.Add(this.pbxImage);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(0, 25);
            this.pnlImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(382, 316);
            this.pnlImage.TabIndex = 5;
            // 
            // pbxZone
            // 
            this.pbxZone.BackColor = System.Drawing.Color.Transparent;
            this.pbxZone.ContextMenuStrip = this.contextMenuStrip;
            this.pbxZone.Location = new System.Drawing.Point(142, 126);
            this.pbxZone.Name = "pbxZone";
            this.pbxZone.Size = new System.Drawing.Size(100, 76);
            this.pbxZone.TabIndex = 11;
            this.pbxZone.TabStop = false;
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
            // pbxImage
            // 
            this.pbxImage.BackColor = System.Drawing.Color.Transparent;
            this.pbxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbxImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxImage.Location = new System.Drawing.Point(0, 0);
            this.pbxImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbxImage.Name = "pbxImage";
            this.pbxImage.Size = new System.Drawing.Size(382, 316);
            this.pbxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxImage.TabIndex = 0;
            this.pbxImage.TabStop = false;
            // 
            // m_cursorTimer
            // 
            this.m_cursorTimer.Interval = 1000;
            this.m_cursorTimer.Tick += new System.EventHandler(this.m_cursorTimer_Tick);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FormZone
            // 
            this.AutoHidePortion = 0.4D;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 398);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormZone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "扫描区域";
            this.Load += new System.EventHandler(this.FormZone_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxZone)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.ComboBox cbxScanPixels;
        private System.Windows.Forms.Label lbScanPixels;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.PictureBox pbxImage;
        private System.Windows.Forms.ToolStripStatusLabel lbPixelSize;
        private System.Windows.Forms.ToolStripSeparator sp;
        private System.Windows.Forms.Timer m_cursorTimer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsResetZone;
        private System.Windows.Forms.ToolStripMenuItem tsSelectZone;
        private System.Windows.Forms.ToolStripMenuItem tsStartAOI;
        private System.Windows.Forms.PictureBox pbxZone;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripStatusLabel lbFieldRange;
        private System.Windows.Forms.ToolStripSeparator sp2;
    }
}