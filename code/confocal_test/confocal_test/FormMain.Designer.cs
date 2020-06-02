namespace confocal_test
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOption = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSkin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSkinDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLanguageCN = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLanguageEN = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.SystemColors.MenuBar;
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 731);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(982, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // dockPanel
            // 
            this.dockPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 0);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(982, 731);
            this.dockPanel.TabIndex = 3;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Menu;
            this.menuStrip.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemOption,
            this.menuItemView,
            this.menuItemSkin,
            this.menuItemLanguage,
            this.menuItemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(982, 28);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.Image = ((System.Drawing.Image)(resources.GetObject("menuItemFile.Image")));
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(71, 24);
            this.menuItemFile.Text = "文件";
            // 
            // menuItemOption
            // 
            this.menuItemOption.Image = ((System.Drawing.Image)(resources.GetObject("menuItemOption.Image")));
            this.menuItemOption.Name = "menuItemOption";
            this.menuItemOption.Size = new System.Drawing.Size(71, 24);
            this.menuItemOption.Text = "操作";
            // 
            // menuItemView
            // 
            this.menuItemView.Checked = true;
            this.menuItemView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemViewData,
            this.menuItemViewDevice,
            this.menuItemViewStatus});
            this.menuItemView.Image = ((System.Drawing.Image)(resources.GetObject("menuItemView.Image")));
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.Size = new System.Drawing.Size(71, 24);
            this.menuItemView.Tag = "0";
            this.menuItemView.Text = "视图";
            // 
            // menuItemViewData
            // 
            this.menuItemViewData.Name = "menuItemViewData";
            this.menuItemViewData.Size = new System.Drawing.Size(216, 26);
            this.menuItemViewData.Text = "数据窗口";
            // 
            // menuItemViewDevice
            // 
            this.menuItemViewDevice.Name = "menuItemViewDevice";
            this.menuItemViewDevice.Size = new System.Drawing.Size(216, 26);
            this.menuItemViewDevice.Text = "设备窗口";
            // 
            // menuItemViewStatus
            // 
            this.menuItemViewStatus.Name = "menuItemViewStatus";
            this.menuItemViewStatus.Size = new System.Drawing.Size(216, 26);
            this.menuItemViewStatus.Text = "状态窗口";
            // 
            // menuItemSkin
            // 
            this.menuItemSkin.Checked = true;
            this.menuItemSkin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemSkin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSkinDefault});
            this.menuItemSkin.Image = ((System.Drawing.Image)(resources.GetObject("menuItemSkin.Image")));
            this.menuItemSkin.Name = "menuItemSkin";
            this.menuItemSkin.Size = new System.Drawing.Size(71, 24);
            this.menuItemSkin.Tag = "0";
            this.menuItemSkin.Text = "皮肤";
            // 
            // menuItemSkinDefault
            // 
            this.menuItemSkinDefault.Checked = true;
            this.menuItemSkinDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemSkinDefault.Name = "menuItemSkinDefault";
            this.menuItemSkinDefault.Size = new System.Drawing.Size(216, 26);
            this.menuItemSkinDefault.Text = "Default";
            // 
            // menuItemLanguage
            // 
            this.menuItemLanguage.AutoToolTip = true;
            this.menuItemLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLanguageCN,
            this.menuItemLanguageEN});
            this.menuItemLanguage.Image = ((System.Drawing.Image)(resources.GetObject("menuItemLanguage.Image")));
            this.menuItemLanguage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuItemLanguage.Name = "menuItemLanguage";
            this.menuItemLanguage.Size = new System.Drawing.Size(101, 24);
            this.menuItemLanguage.Tag = "0";
            this.menuItemLanguage.Text = "语言设置";
            this.menuItemLanguage.ToolTipText = "设置语言";
            // 
            // menuItemLanguageCN
            // 
            this.menuItemLanguageCN.Checked = true;
            this.menuItemLanguageCN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemLanguageCN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuItemLanguageCN.Name = "menuItemLanguageCN";
            this.menuItemLanguageCN.Size = new System.Drawing.Size(216, 26);
            this.menuItemLanguageCN.Tag = "zh-CN";
            this.menuItemLanguageCN.Text = "简体中文";
            // 
            // menuItemLanguageEN
            // 
            this.menuItemLanguageEN.Name = "menuItemLanguageEN";
            this.menuItemLanguageEN.Size = new System.Drawing.Size(216, 26);
            this.menuItemLanguageEN.Tag = "en";
            this.menuItemLanguageEN.Text = "英语";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemManual,
            this.menuItemAbout});
            this.menuItemHelp.Image = ((System.Drawing.Image)(resources.GetObject("menuItemHelp.Image")));
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(71, 24);
            this.menuItemHelp.Text = "帮助";
            // 
            // menuItemManual
            // 
            this.menuItemManual.Name = "menuItemManual";
            this.menuItemManual.Size = new System.Drawing.Size(216, 26);
            this.menuItemManual.Text = "用户手册";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(216, 26);
            this.menuItemAbout.Text = "关于我们";
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbSelectDevice,
            this.cbxSelectDevice,
            this.sp1,
            this.btnOperateDevice,
            this.sp2});
            this.toolStrip.Location = new System.Drawing.Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(982, 27);
            this.toolStrip.TabIndex = 6;
            this.toolStrip.Text = "toolStrip1";
            // 
            // lbSelectDevice
            // 
            this.lbSelectDevice.Name = "lbSelectDevice";
            this.lbSelectDevice.Size = new System.Drawing.Size(73, 24);
            this.lbSelectDevice.Text = "选择设备:";
            // 
            // cbxSelectDevice
            // 
            this.cbxSelectDevice.BackColor = System.Drawing.SystemColors.Control;
            this.cbxSelectDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxSelectDevice.Font = new System.Drawing.Font("微软雅黑", 6.6F);
            this.cbxSelectDevice.Name = "cbxSelectDevice";
            this.cbxSelectDevice.Size = new System.Drawing.Size(121, 27);
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
            this.btnOperateDevice.Size = new System.Drawing.Size(83, 24);
            this.btnOperateDevice.Text = "操作设备";
            // 
            // itemAddDevice
            // 
            this.itemAddDevice.Name = "itemAddDevice";
            this.itemAddDevice.Size = new System.Drawing.Size(144, 26);
            this.itemAddDevice.Text = "添加设备";
            // 
            // itemConfigDevice
            // 
            this.itemConfigDevice.Name = "itemConfigDevice";
            this.itemConfigDevice.Size = new System.Drawing.Size(144, 26);
            this.itemConfigDevice.Text = "配置设备";
            this.itemConfigDevice.ToolTipText = "复位所有CAN通道";
            // 
            // itemResetDevice
            // 
            this.itemResetDevice.Name = "itemResetDevice";
            this.itemResetDevice.Size = new System.Drawing.Size(144, 26);
            this.itemResetDevice.Text = "复位设备";
            this.itemResetDevice.ToolTipText = "复位所有CAN通道";
            // 
            // itemDeleteDevice
            // 
            this.itemDeleteDevice.Name = "itemDeleteDevice";
            this.itemDeleteDevice.Size = new System.Drawing.Size(144, 26);
            this.itemDeleteDevice.Text = "删除设备";
            this.itemDeleteDevice.ToolTipText = "复位所有CAN通道";
            // 
            // sp2
            // 
            this.sp2.Name = "sp2";
            this.sp2.Size = new System.Drawing.Size(6, 27);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 753);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        private System.Windows.Forms.StatusStrip statusStrip;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOption;
        private System.Windows.Forms.ToolStripMenuItem menuItemView;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewData;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewDevice;
        private System.Windows.Forms.ToolStripMenuItem menuItemViewStatus;
        private System.Windows.Forms.ToolStripMenuItem menuItemSkin;
        private System.Windows.Forms.ToolStripMenuItem menuItemSkinDefault;
        private System.Windows.Forms.ToolStripMenuItem menuItemLanguage;
        private System.Windows.Forms.ToolStripMenuItem menuItemLanguageCN;
        private System.Windows.Forms.ToolStripMenuItem menuItemLanguageEN;
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
    }
}