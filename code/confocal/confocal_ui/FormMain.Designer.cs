namespace confocal_ui
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiScan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSysCfg = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemManual = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lbSelectLaser = new System.Windows.Forms.ToolStripLabel();
            this.cbxSelectLaser = new System.Windows.Forms.ToolStripComboBox();
            this.btnLaserConnect = new System.Windows.Forms.ToolStripButton();
            this.btnLaserRelease = new System.Windows.Forms.ToolStripButton();
            this.sp1 = new System.Windows.Forms.ToolStripSeparator();
            this.sp2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Menu;
            this.menuStrip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemView,
            this.menuItemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(41, 20);
            this.menuItemFile.Text = "文件";
            // 
            // menuItemView
            // 
            this.menuItemView.Checked = true;
            this.menuItemView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiScan,
            this.tsmiShow,
            this.tsmiSysCfg});
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.Size = new System.Drawing.Size(41, 20);
            this.menuItemView.Tag = "0";
            this.menuItemView.Text = "窗口";
            // 
            // tsmiScan
            // 
            this.tsmiScan.Name = "tsmiScan";
            this.tsmiScan.Size = new System.Drawing.Size(180, 22);
            this.tsmiScan.Text = "扫描控制";
            // 
            // tsmiShow
            // 
            this.tsmiShow.Name = "tsmiShow";
            this.tsmiShow.Size = new System.Drawing.Size(180, 22);
            this.tsmiShow.Text = "参数显示";
            // 
            // tsmiSysCfg
            // 
            this.tsmiSysCfg.Name = "tsmiSysCfg";
            this.tsmiSysCfg.Size = new System.Drawing.Size(180, 22);
            this.tsmiSysCfg.Text = "系统配置";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemManual,
            this.menuItemAbout});
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(41, 20);
            this.menuItemHelp.Text = "帮助";
            // 
            // menuItemManual
            // 
            this.menuItemManual.Name = "menuItemManual";
            this.menuItemManual.Size = new System.Drawing.Size(180, 22);
            this.menuItemManual.Text = "用户手册";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(180, 22);
            this.menuItemAbout.Text = "关于我们";
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 739);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbSelectLaser,
            this.cbxSelectLaser,
            this.btnLaserConnect,
            this.btnLaserRelease,
            this.sp1,
            this.sp2});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1184, 27);
            this.toolStrip.TabIndex = 9;
            this.toolStrip.Text = "toolStrip1";
            // 
            // lbSelectLaser
            // 
            this.lbSelectLaser.Name = "lbSelectLaser";
            this.lbSelectLaser.Size = new System.Drawing.Size(59, 24);
            this.lbSelectLaser.Text = "激光端口:";
            // 
            // cbxSelectLaser
            // 
            this.cbxSelectLaser.BackColor = System.Drawing.SystemColors.Control;
            this.cbxSelectLaser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectLaser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxSelectLaser.Font = new System.Drawing.Font("微软雅黑", 6.6F);
            this.cbxSelectLaser.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.cbxSelectLaser.Name = "cbxSelectLaser";
            this.cbxSelectLaser.Size = new System.Drawing.Size(92, 27);
            // 
            // btnLaserConnect
            // 
            this.btnLaserConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLaserConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnLaserConnect.Image")));
            this.btnLaserConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLaserConnect.Name = "btnLaserConnect";
            this.btnLaserConnect.Size = new System.Drawing.Size(24, 24);
            // 
            // btnLaserRelease
            // 
            this.btnLaserRelease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLaserRelease.Image = ((System.Drawing.Image)(resources.GetObject("btnLaserRelease.Image")));
            this.btnLaserRelease.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLaserRelease.Name = "btnLaserRelease";
            this.btnLaserRelease.Size = new System.Drawing.Size(24, 24);
            // 
            // sp1
            // 
            this.sp1.Name = "sp1";
            this.sp1.Size = new System.Drawing.Size(6, 27);
            // 
            // sp2
            // 
            this.sp2.Name = "sp2";
            this.sp2.Size = new System.Drawing.Size(6, 27);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confocal Imaging v";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemView;
        private System.Windows.Forms.ToolStripMenuItem tsmiScan;
        private System.Windows.Forms.ToolStripMenuItem tsmiShow;
        private System.Windows.Forms.ToolStripMenuItem tsmiSysCfg;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemManual;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel lbSelectLaser;
        private System.Windows.Forms.ToolStripComboBox cbxSelectLaser;
        private System.Windows.Forms.ToolStripButton btnLaserConnect;
        private System.Windows.Forms.ToolStripButton btnLaserRelease;
        private System.Windows.Forms.ToolStripSeparator sp1;
        private System.Windows.Forms.ToolStripSeparator sp2;
    }
}

