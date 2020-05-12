namespace confocal
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
            this.menuItemSkin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSkinDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemManual = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lbSelectDevice = new System.Windows.Forms.ToolStripLabel();
            this.cbxSelectDevice = new System.Windows.Forms.ToolStripComboBox();
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
            this.menuStrip.Size = new System.Drawing.Size(1582, 24);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(49, 20);
            this.menuItemFile.Text = "文件";
            // 
            // menuItemOption
            // 
            this.menuItemOption.Name = "menuItemOption";
            this.menuItemOption.Size = new System.Drawing.Size(49, 20);
            this.menuItemOption.Text = "操作";
            // 
            // menuItemView
            // 
            this.menuItemView.Checked = true;
            this.menuItemView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.Size = new System.Drawing.Size(49, 20);
            this.menuItemView.Tag = "0";
            this.menuItemView.Text = "视图";
            // 
            // menuItemSkin
            // 
            this.menuItemSkin.Checked = true;
            this.menuItemSkin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemSkin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSkinDefault});
            this.menuItemSkin.Name = "menuItemSkin";
            this.menuItemSkin.Size = new System.Drawing.Size(49, 20);
            this.menuItemSkin.Tag = "0";
            this.menuItemSkin.Text = "皮肤";
            // 
            // menuItemSkinDefault
            // 
            this.menuItemSkinDefault.Checked = true;
            this.menuItemSkinDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemSkinDefault.Name = "menuItemSkinDefault";
            this.menuItemSkinDefault.Size = new System.Drawing.Size(136, 26);
            this.menuItemSkinDefault.Text = "Default";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemManual,
            this.menuItemAbout});
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(49, 20);
            this.menuItemHelp.Text = "帮助";
            // 
            // menuItemManual
            // 
            this.menuItemManual.Name = "menuItemManual";
            this.menuItemManual.Size = new System.Drawing.Size(140, 26);
            this.menuItemManual.Text = "用户手册";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(140, 26);
            this.menuItemAbout.Text = "关于我们";
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbSelectDevice,
            this.cbxSelectDevice,
            this.sp1,
            this.btnOperateDevice,
            this.sp2});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1582, 25);
            this.toolStrip.TabIndex = 6;
            this.toolStrip.Text = "toolStrip1";
            // 
            // lbSelectDevice
            // 
            this.lbSelectDevice.Name = "lbSelectDevice";
            this.lbSelectDevice.Size = new System.Drawing.Size(75, 22);
            this.lbSelectDevice.Text = "选择设备:";
            // 
            // cbxSelectDevice
            // 
            this.cbxSelectDevice.BackColor = System.Drawing.SystemColors.Control;
            this.cbxSelectDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxSelectDevice.Font = new System.Drawing.Font("微软雅黑", 6.6F);
            this.cbxSelectDevice.Name = "cbxSelectDevice";
            this.cbxSelectDevice.Size = new System.Drawing.Size(121, 25);
            // 
            // sp1
            // 
            this.sp1.Name = "sp1";
            this.sp1.Size = new System.Drawing.Size(6, 25);
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
            this.btnOperateDevice.Size = new System.Drawing.Size(81, 22);
            this.btnOperateDevice.Text = "操作设备";
            // 
            // itemAddDevice
            // 
            this.itemAddDevice.Name = "itemAddDevice";
            this.itemAddDevice.Size = new System.Drawing.Size(140, 26);
            this.itemAddDevice.Text = "添加设备";
            // 
            // itemConfigDevice
            // 
            this.itemConfigDevice.Name = "itemConfigDevice";
            this.itemConfigDevice.Size = new System.Drawing.Size(140, 26);
            this.itemConfigDevice.Text = "配置设备";
            this.itemConfigDevice.ToolTipText = "复位所有CAN通道";
            // 
            // itemResetDevice
            // 
            this.itemResetDevice.Name = "itemResetDevice";
            this.itemResetDevice.Size = new System.Drawing.Size(140, 26);
            this.itemResetDevice.Text = "复位设备";
            this.itemResetDevice.ToolTipText = "复位所有CAN通道";
            // 
            // itemDeleteDevice
            // 
            this.itemDeleteDevice.Name = "itemDeleteDevice";
            this.itemDeleteDevice.Size = new System.Drawing.Size(140, 26);
            this.itemDeleteDevice.Text = "删除设备";
            this.itemDeleteDevice.ToolTipText = "复位所有CAN通道";
            // 
            // sp2
            // 
            this.sp2.Name = "sp2";
            this.sp2.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 831);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1582, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // dockPanel
            // 
            this.dockPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBottomPortion = 0.3D;
            this.dockPanel.Location = new System.Drawing.Point(0, 49);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1582, 782);
            this.dockPanel.TabIndex = 12;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confocal Imaging v";
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
        private System.Windows.Forms.ToolStripLabel lbSelectDevice;
        private System.Windows.Forms.ToolStripComboBox cbxSelectDevice;
        private System.Windows.Forms.ToolStripSeparator sp1;
        private System.Windows.Forms.ToolStripDropDownButton btnOperateDevice;
        private System.Windows.Forms.ToolStripMenuItem itemAddDevice;
        private System.Windows.Forms.ToolStripMenuItem itemConfigDevice;
        private System.Windows.Forms.ToolStripMenuItem itemResetDevice;
        private System.Windows.Forms.ToolStripMenuItem itemDeleteDevice;
        private System.Windows.Forms.ToolStripSeparator sp2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
    }
}

