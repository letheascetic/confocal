﻿
using C1.Win.C1InputPanel;

namespace confocal_ui.View
{
    partial class FormScanArea
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScanArea));
            this.inputPanel = new C1.Win.C1InputPanel.C1InputPanel();
            this.inputSeparator1 = new C1.Win.C1InputPanel.InputSeparator();
            this.lbScanPixel = new C1.Win.C1InputPanel.InputLabel();
            this.cbxScanPixel = new C1.Win.C1InputPanel.InputComboBox();
            this.lbPosition = new C1.Win.C1InputPanel.InputLabel();
            this.lbPixelPosition = new C1.Win.C1InputPanel.InputLabel();
            this.inputSeparator2 = new C1.Win.C1InputPanel.InputSeparator();
            this.lbPixelSize = new C1.Win.C1InputPanel.InputLabel();
            this.lbPixelSizeValue = new C1.Win.C1InputPanel.InputLabel();
            this.lbPixelDwell = new C1.Win.C1InputPanel.InputLabel();
            this.lbPixelDwellValue = new C1.Win.C1InputPanel.InputLabel();
            this.lbWidth = new C1.Win.C1InputPanel.InputLabel();
            this.lbScanWidth = new C1.Win.C1InputPanel.InputLabel();
            this.lbHeight = new C1.Win.C1InputPanel.InputLabel();
            this.lbScanHeight = new C1.Win.C1InputPanel.InputLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.dockToolBar = new C1.Win.C1Command.C1CommandDock();
            this.toolBar = new C1.Win.C1Command.C1ToolBar();
            this.cmdLinkSquare = new C1.Win.C1Command.C1CommandLink();
            this.cmdSquare = new C1.Win.C1Command.C1Command();
            this.cmdLinkRect = new C1.Win.C1Command.C1CommandLink();
            this.cmdRect = new C1.Win.C1Command.C1Command();
            this.cmdLinkFullRange = new C1.Win.C1Command.C1CommandLink();
            this.cmdFullRange = new C1.Win.C1Command.C1Command();
            this.cmdLinkLastScanRange = new C1.Win.C1Command.C1CommandLink();
            this.cmdLastScanRange = new C1.Win.C1Command.C1Command();
            this.contextMenu = new C1.Win.C1Command.C1ContextMenu();
            this.cmdLinkConfirm = new C1.Win.C1Command.C1CommandLink();
            this.cmdConfirm = new C1.Win.C1Command.C1Command();
            this.c1CommandHolder = new C1.Win.C1Command.C1CommandHolder();
            this.pbxScanArea = new System.Windows.Forms.PictureBox();
            this.pictureBox = new Emgu.CV.UI.ImageBox();
            this.lbScanRange = new C1.Win.C1InputPanel.InputLabel();
            this.lbScanRangeValue = new C1.Win.C1InputPanel.InputLabel();
            this.lbMaxScanRange = new C1.Win.C1InputPanel.InputLabel();
            this.lbMaxScanRangeValue = new C1.Win.C1InputPanel.InputLabel();
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockToolBar)).BeginInit();
            this.dockToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxScanArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.inputPanel.Items.Add(this.inputSeparator1);
            this.inputPanel.Items.Add(this.lbScanPixel);
            this.inputPanel.Items.Add(this.cbxScanPixel);
            this.inputPanel.Items.Add(this.lbPosition);
            this.inputPanel.Items.Add(this.lbPixelPosition);
            this.inputPanel.Items.Add(this.inputSeparator2);
            this.inputPanel.Items.Add(this.lbPixelSize);
            this.inputPanel.Items.Add(this.lbPixelSizeValue);
            this.inputPanel.Items.Add(this.lbPixelDwell);
            this.inputPanel.Items.Add(this.lbPixelDwellValue);
            this.inputPanel.Items.Add(this.lbWidth);
            this.inputPanel.Items.Add(this.lbScanWidth);
            this.inputPanel.Items.Add(this.lbHeight);
            this.inputPanel.Items.Add(this.lbScanHeight);
            this.inputPanel.Items.Add(this.lbScanRange);
            this.inputPanel.Items.Add(this.lbScanRangeValue);
            this.inputPanel.Items.Add(this.lbMaxScanRange);
            this.inputPanel.Items.Add(this.lbMaxScanRangeValue);
            this.inputPanel.Location = new System.Drawing.Point(0, 308);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(272, 121);
            this.inputPanel.TabIndex = 2;
            // 
            // inputSeparator1
            // 
            this.inputSeparator1.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.inputSeparator1.Name = "inputSeparator1";
            this.inputSeparator1.Width = 260;
            // 
            // lbScanPixel
            // 
            this.lbScanPixel.Name = "lbScanPixel";
            this.lbScanPixel.Text = "扫描像素：";
            // 
            // cbxScanPixel
            // 
            this.cbxScanPixel.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbxScanPixel.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxScanPixel.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxScanPixel.Name = "cbxScanPixel";
            this.cbxScanPixel.Width = 60;
            // 
            // lbPosition
            // 
            this.lbPosition.Name = "lbPosition";
            this.lbPosition.Text = "像素位置：";
            // 
            // lbPixelPosition
            // 
            this.lbPixelPosition.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbPixelPosition.Name = "lbPixelPosition";
            this.lbPixelPosition.Text = "[9999,1000]";
            // 
            // inputSeparator2
            // 
            this.inputSeparator2.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.inputSeparator2.Name = "inputSeparator2";
            this.inputSeparator2.Width = 260;
            // 
            // lbPixelSize
            // 
            this.lbPixelSize.Name = "lbPixelSize";
            this.lbPixelSize.Text = "像素尺寸：";
            // 
            // lbPixelSizeValue
            // 
            this.lbPixelSizeValue.Name = "lbPixelSizeValue";
            this.lbPixelSizeValue.Text = "xxum/pixel";
            this.lbPixelSizeValue.Width = 60;
            // 
            // lbPixelDwell
            // 
            this.lbPixelDwell.Name = "lbPixelDwell";
            this.lbPixelDwell.Text = "像素时间：";
            // 
            // lbPixelDwellValue
            // 
            this.lbPixelDwellValue.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbPixelDwellValue.Name = "lbPixelDwellValue";
            this.lbPixelDwellValue.Text = "2.0us";
            this.lbPixelDwellValue.Width = 60;
            // 
            // lbWidth
            // 
            this.lbWidth.Name = "lbWidth";
            this.lbWidth.Text = "扫描宽度：";
            // 
            // lbScanWidth
            // 
            this.lbScanWidth.Name = "lbScanWidth";
            this.lbScanWidth.Text = "512";
            this.lbScanWidth.Width = 60;
            // 
            // lbHeight
            // 
            this.lbHeight.Name = "lbHeight";
            this.lbHeight.Text = "扫描高度：";
            // 
            // lbScanHeight
            // 
            this.lbScanHeight.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbScanHeight.Name = "lbScanHeight";
            this.lbScanHeight.Text = "512";
            this.lbScanHeight.Width = 60;
            // 
            // timer
            // 
            this.timer.Interval = 250;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // dockToolBar
            // 
            this.dockToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dockToolBar.Controls.Add(this.toolBar);
            this.dockToolBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dockToolBar.Id = 3;
            this.dockToolBar.Location = new System.Drawing.Point(0, 0);
            this.dockToolBar.Name = "dockToolBar";
            this.dockToolBar.Size = new System.Drawing.Size(272, 26);
            // 
            // toolBar
            // 
            this.toolBar.AccessibleName = "Tool Bar";
            this.toolBar.CommandHolder = null;
            this.toolBar.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.cmdLinkSquare,
            this.cmdLinkRect,
            this.cmdLinkFullRange,
            this.cmdLinkLastScanRange,
            this.cmdLinkConfirm});
            this.toolBar.Location = new System.Drawing.Point(3, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(152, 26);
            this.toolBar.Text = "工具栏";
            this.toolBar.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
            this.toolBar.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // cmdLinkSquare
            // 
            this.cmdLinkSquare.ButtonLook = C1.Win.C1Command.ButtonLookFlags.Text;
            this.cmdLinkSquare.Command = this.cmdSquare;
            // 
            // cmdSquare
            // 
            this.cmdSquare.Name = "cmdSquare";
            this.cmdSquare.ShortcutText = "";
            this.cmdSquare.Text = "方形";
            // 
            // cmdLinkRect
            // 
            this.cmdLinkRect.ButtonLook = C1.Win.C1Command.ButtonLookFlags.Text;
            this.cmdLinkRect.Command = this.cmdRect;
            this.cmdLinkRect.SortOrder = 1;
            // 
            // cmdRect
            // 
            this.cmdRect.Name = "cmdRect";
            this.cmdRect.ShortcutText = "";
            this.cmdRect.Text = "矩形";
            // 
            // cmdLinkFullRange
            // 
            this.cmdLinkFullRange.Command = this.cmdFullRange;
            this.cmdLinkFullRange.SortOrder = 2;
            this.cmdLinkFullRange.Text = "使用最大扫描视场";
            // 
            // cmdFullRange
            // 
            this.cmdFullRange.Image = global::confocal_ui.Properties.Resources.fullrange;
            this.cmdFullRange.Name = "cmdFullRange";
            this.cmdFullRange.ShortcutText = "";
            this.cmdFullRange.Text = "全视场";
            this.cmdFullRange.Click += new C1.Win.C1Command.ClickEventHandler(this.FullRangeClick);
            // 
            // cmdLinkLastScanRange
            // 
            this.cmdLinkLastScanRange.Command = this.cmdLastScanRange;
            this.cmdLinkLastScanRange.SortOrder = 3;
            this.cmdLinkLastScanRange.Text = "使用上一个扫描视场";
            // 
            // cmdLastScanRange
            // 
            this.cmdLastScanRange.C1ContextMenu = this.contextMenu;
            this.cmdLastScanRange.Image = global::confocal_ui.Properties.Resources.reset;
            this.cmdLastScanRange.Name = "cmdLastScanRange";
            this.cmdLastScanRange.ShortcutText = "";
            this.cmdLastScanRange.Text = "上一个视场";
            this.cmdLastScanRange.Click += new C1.Win.C1Command.ClickEventHandler(this.LastScanRangeClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.ShortcutText = "";
            // 
            // cmdLinkConfirm
            // 
            this.cmdLinkConfirm.Command = this.cmdConfirm;
            this.cmdLinkConfirm.SortOrder = 4;
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.Image = global::confocal_ui.Properties.Resources.confirm;
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.ShortcutText = "";
            this.cmdConfirm.Text = "设置为当前扫描视场";
            this.cmdConfirm.Click += new C1.Win.C1Command.ClickEventHandler(this.ScanRangeConfirmClick);
            // 
            // c1CommandHolder
            // 
            this.c1CommandHolder.Commands.Add(this.contextMenu);
            this.c1CommandHolder.Commands.Add(this.cmdSquare);
            this.c1CommandHolder.Commands.Add(this.cmdRect);
            this.c1CommandHolder.Commands.Add(this.cmdFullRange);
            this.c1CommandHolder.Commands.Add(this.cmdLastScanRange);
            this.c1CommandHolder.Commands.Add(this.cmdConfirm);
            this.c1CommandHolder.Owner = this;
            // 
            // pbxScanArea
            // 
            this.pbxScanArea.BackColor = System.Drawing.Color.Transparent;
            this.pbxScanArea.Location = new System.Drawing.Point(12, 43);
            this.pbxScanArea.Name = "pbxScanArea";
            this.pbxScanArea.Size = new System.Drawing.Size(100, 100);
            this.pbxScanArea.TabIndex = 21;
            this.pbxScanArea.TabStop = false;
            this.pbxScanArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownChanged);
            this.pbxScanArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScanRangeMoved);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Black;
            this.pictureBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.pictureBox.Location = new System.Drawing.Point(0, 30);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(272, 272);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 16;
            this.pictureBox.TabStop = false;
            // 
            // lbScanRange
            // 
            this.lbScanRange.Name = "lbScanRange";
            this.lbScanRange.Text = "当前视场：";
            // 
            // lbScanRangeValue
            // 
            this.lbScanRangeValue.Break = C1.Win.C1InputPanel.BreakType.Group;
            this.lbScanRangeValue.Name = "lbScanRangeValue";
            this.lbScanRangeValue.Text = "xxxx";
            // 
            // lbMaxScanRange
            // 
            this.lbMaxScanRange.Name = "lbMaxScanRange";
            this.lbMaxScanRange.Text = "最大视场：";
            // 
            // lbMaxScanRangeValue
            // 
            this.lbMaxScanRangeValue.Name = "lbMaxScanRangeValue";
            this.lbMaxScanRangeValue.Text = "xxxx";
            // 
            // FormScanArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 429);
            this.Controls.Add(this.pbxScanArea);
            this.Controls.Add(this.dockToolBar);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.inputPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(280, 460);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(280, 460);
            this.Name = "FormScanArea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "扫描区域";
            this.Load += new System.EventHandler(this.FormScanAreaLoad);
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockToolBar)).EndInit();
            this.dockToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxScanArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private C1InputPanel inputPanel;
        private Emgu.CV.UI.ImageBox pictureBox;
        private InputLabel lbPixelSize;
        private InputLabel lbWidth;
        private InputLabel lbHeight;
        private InputSeparator inputSeparator1;
        private InputLabel lbScanPixel;
        private InputComboBox cbxScanPixel;
        private InputSeparator inputSeparator2;
        private InputLabel lbPixelSizeValue;
        private InputLabel lbPixelDwell;
        private InputLabel lbPixelDwellValue;
        private InputLabel lbScanWidth;
        private InputLabel lbScanHeight;
        private InputLabel lbPosition;
        private InputLabel lbPixelPosition;
        private System.Windows.Forms.Timer timer;
        private C1.Win.C1Command.C1CommandDock dockToolBar;
        private C1.Win.C1Command.C1ToolBar toolBar;
        private System.Windows.Forms.PictureBox pbxScanArea;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder;
        private C1.Win.C1Command.C1ContextMenu contextMenu;
        private C1.Win.C1Command.C1CommandLink cmdLinkSquare;
        private C1.Win.C1Command.C1Command cmdSquare;
        private C1.Win.C1Command.C1CommandLink cmdLinkRect;
        private C1.Win.C1Command.C1Command cmdRect;
        private C1.Win.C1Command.C1CommandLink cmdLinkFullRange;
        private C1.Win.C1Command.C1Command cmdFullRange;
        private C1.Win.C1Command.C1CommandLink cmdLinkLastScanRange;
        private C1.Win.C1Command.C1Command cmdLastScanRange;
        private C1.Win.C1Command.C1Command cmdConfirm;
        private C1.Win.C1Command.C1CommandLink cmdLinkConfirm;
        private InputLabel lbScanRange;
        private InputLabel lbScanRangeValue;
        private InputLabel lbMaxScanRange;
        private InputLabel lbMaxScanRangeValue;
    }
}