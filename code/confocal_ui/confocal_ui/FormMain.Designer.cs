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
            this.menuItemOption = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiScan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSysCfg = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSkin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSkinDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemManual = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lbSelectLaser = new System.Windows.Forms.ToolStripLabel();
            this.cbxSelectLaser = new System.Windows.Forms.ToolStripComboBox();
            this.btnLaserConnect = new System.Windows.Forms.ToolStripButton();
            this.btnLaserRelease = new System.Windows.Forms.ToolStripButton();
            this.sp1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOperateDevice = new System.Windows.Forms.ToolStripDropDownButton();
            this.itemAddDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.itemConfigDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.itemResetDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDeleteDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.sp2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
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
            this.menuItemOption,
            this.menuItemView,
            this.menuItemSkin,
            this.menuItemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1186, 24);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(51, 20);
            this.menuItemFile.Text = "文件";
            // 
            // menuItemOption
            // 
            this.menuItemOption.Name = "menuItemOption";
            this.menuItemOption.Size = new System.Drawing.Size(51, 20);
            this.menuItemOption.Text = "操作";
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
            this.menuItemView.Size = new System.Drawing.Size(51, 20);
            this.menuItemView.Tag = "0";
            this.menuItemView.Text = "窗口";
            // 
            // tsmiScan
            // 
            this.tsmiScan.Name = "tsmiScan";
            this.tsmiScan.Size = new System.Drawing.Size(148, 26);
            this.tsmiScan.Text = "扫描控制";
            this.tsmiScan.Click += new System.EventHandler(this.tsmiScan_Click);
            // 
            // tsmiShow
            // 
            this.tsmiShow.Name = "tsmiShow";
            this.tsmiShow.Size = new System.Drawing.Size(148, 26);
            this.tsmiShow.Text = "参数显示";
            this.tsmiShow.Click += new System.EventHandler(this.tsmiShow_Click);
            // 
            // tsmiSysCfg
            // 
            this.tsmiSysCfg.Name = "tsmiSysCfg";
            this.tsmiSysCfg.Size = new System.Drawing.Size(148, 26);
            this.tsmiSysCfg.Text = "系统配置";
            this.tsmiSysCfg.Click += new System.EventHandler(this.tsmiSysCfg_Click);
            // 
            // menuItemSkin
            // 
            this.menuItemSkin.Checked = true;
            this.menuItemSkin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemSkin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSkinDefault});
            this.menuItemSkin.Name = "menuItemSkin";
            this.menuItemSkin.Size = new System.Drawing.Size(51, 20);
            this.menuItemSkin.Tag = "0";
            this.menuItemSkin.Text = "皮肤";
            // 
            // menuItemSkinDefault
            // 
            this.menuItemSkinDefault.Checked = true;
            this.menuItemSkinDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemSkinDefault.Name = "menuItemSkinDefault";
            this.menuItemSkinDefault.Size = new System.Drawing.Size(144, 26);
            this.menuItemSkinDefault.Text = "Default";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemManual,
            this.menuItemAbout});
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(51, 20);
            this.menuItemHelp.Text = "帮助";
            // 
            // menuItemManual
            // 
            this.menuItemManual.Name = "menuItemManual";
            this.menuItemManual.Size = new System.Drawing.Size(148, 26);
            this.menuItemManual.Text = "用户手册";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(148, 26);
            this.menuItemAbout.Text = "关于我们";
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
            this.btnOperateDevice,
            this.sp2});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1186, 27);
            this.toolStrip.TabIndex = 6;
            this.toolStrip.Text = "toolStrip1";
            // 
            // lbSelectLaser
            // 
            this.lbSelectLaser.Name = "lbSelectLaser";
            this.lbSelectLaser.Size = new System.Drawing.Size(75, 24);
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
            this.btnLaserConnect.Size = new System.Drawing.Size(29, 24);
            this.btnLaserConnect.Click += new System.EventHandler(this.btnLaserConnect_Click);
            // 
            // btnLaserRelease
            // 
            this.btnLaserRelease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLaserRelease.Image = ((System.Drawing.Image)(resources.GetObject("btnLaserRelease.Image")));
            this.btnLaserRelease.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLaserRelease.Name = "btnLaserRelease";
            this.btnLaserRelease.Size = new System.Drawing.Size(29, 24);
            this.btnLaserRelease.Click += new System.EventHandler(this.btnLaserRelease_Click);
            // 
            // sp1
            // 
            this.sp1.Name = "sp1";
            this.sp1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnOperateDevice
            // 
            this.btnOperateDevice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAddDevice,
            this.itemConfigDevice,
            this.itemResetDevice,
            this.itemDeleteDevice});
            this.btnOperateDevice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOperateDevice.Name = "btnOperateDevice";
            this.btnOperateDevice.Size = new System.Drawing.Size(81, 24);
            this.btnOperateDevice.Text = "操作设备";
            // 
            // itemAddDevice
            // 
            this.itemAddDevice.Name = "itemAddDevice";
            this.itemAddDevice.Size = new System.Drawing.Size(148, 26);
            this.itemAddDevice.Text = "添加设备";
            // 
            // itemConfigDevice
            // 
            this.itemConfigDevice.Name = "itemConfigDevice";
            this.itemConfigDevice.Size = new System.Drawing.Size(148, 26);
            this.itemConfigDevice.Text = "配置设备";
            this.itemConfigDevice.ToolTipText = "复位所有CAN通道";
            // 
            // itemResetDevice
            // 
            this.itemResetDevice.Name = "itemResetDevice";
            this.itemResetDevice.Size = new System.Drawing.Size(148, 26);
            this.itemResetDevice.Text = "复位设备";
            this.itemResetDevice.ToolTipText = "复位所有CAN通道";
            // 
            // itemDeleteDevice
            // 
            this.itemDeleteDevice.Name = "itemDeleteDevice";
            this.itemDeleteDevice.Size = new System.Drawing.Size(148, 26);
            this.itemDeleteDevice.Text = "删除设备";
            this.itemDeleteDevice.ToolTipText = "复位所有CAN通道";
            // 
            // sp2
            // 
            this.sp2.Name = "sp2";
            this.sp2.Size = new System.Drawing.Size(6, 27);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 660);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(1186, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // dockPanel
            // 
            this.dockPanel.BackColor = System.Drawing.SystemColors.Control;
            this.dockPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.dockPanel.DockBottomPortion = 0.16D;
            this.dockPanel.DockLeftPortion = 0.16D;
            this.dockPanel.DockRightPortion = 0.16D;
            this.dockPanel.DockTopPortion = 0.16D;
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel.Location = new System.Drawing.Point(0, 51);
            this.dockPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1186, 609);
            this.dockPanel.TabIndex = 12;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 682);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confocal Imaging v";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
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
        private System.Windows.Forms.ToolStripMenuItem menuItemOption;
        private System.Windows.Forms.ToolStripMenuItem menuItemView;
        private System.Windows.Forms.ToolStripMenuItem menuItemSkin;
        private System.Windows.Forms.ToolStripMenuItem menuItemSkinDefault;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemManual;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel lbSelectLaser;
        private System.Windows.Forms.ToolStripComboBox cbxSelectLaser;
        private System.Windows.Forms.ToolStripSeparator sp1;
        private System.Windows.Forms.ToolStripSeparator sp2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripButton btnLaserConnect;
        private System.Windows.Forms.ToolStripButton btnLaserRelease;
        private System.Windows.Forms.ToolStripDropDownButton btnOperateDevice;
        private System.Windows.Forms.ToolStripMenuItem itemAddDevice;
        private System.Windows.Forms.ToolStripMenuItem itemConfigDevice;
        private System.Windows.Forms.ToolStripMenuItem itemResetDevice;
        private System.Windows.Forms.ToolStripMenuItem itemDeleteDevice;
        private System.Windows.Forms.ToolStripMenuItem tsmiScan;
        private System.Windows.Forms.ToolStripMenuItem tsmiShow;
        private System.Windows.Forms.ToolStripMenuItem tsmiSysCfg;
    }
}

