namespace confocal_ui
{
    partial class FormScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScan));
            this.inputPanel = new C1.Win.C1InputPanel.C1InputPanel();
            this.btnLive = new C1.Win.C1InputPanel.InputButton();
            this.btnCapture = new C1.Win.C1InputPanel.InputButton();
            this.cbxScanners = new C1.Win.C1InputPanel.InputComboBox();
            this.twoGalv = new C1.Win.C1InputPanel.InputOption();
            this.threeGalv = new C1.Win.C1InputPanel.InputOption();
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.inputPanel.Items.Add(this.btnLive);
            this.inputPanel.Items.Add(this.btnCapture);
            this.inputPanel.Items.Add(this.cbxScanners);
            this.inputPanel.Location = new System.Drawing.Point(0, 0);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(244, 459);
            this.inputPanel.TabIndex = 0;
            // 
            // btnLive
            // 
            this.btnLive.Break = C1.Win.C1InputPanel.BreakType.None;
            this.btnLive.CheckOnClick = true;
            this.btnLive.Height = 40;
            this.btnLive.Name = "btnLive";
            this.btnLive.Text = "实时";
            this.btnLive.Width = 50;
            // 
            // btnCapture
            // 
            this.btnCapture.Break = C1.Win.C1InputPanel.BreakType.Column;
            this.btnCapture.CheckOnClick = true;
            this.btnCapture.Height = 40;
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Text = "捕捉";
            this.btnCapture.Width = 50;
            // 
            // cbxScanners
            // 
            this.cbxScanners.Break = C1.Win.C1InputPanel.BreakType.None;
            this.cbxScanners.DropDownStyle = C1.Win.C1InputPanel.InputComboBoxStyle.DropDownList;
            this.cbxScanners.Items.Add(this.twoGalv);
            this.cbxScanners.Items.Add(this.threeGalv);
            this.cbxScanners.Name = "cbxScanners";
            // 
            // twoGalv
            // 
            this.twoGalv.Name = "twoGalv";
            this.twoGalv.Text = "双振镜";
            // 
            // threeGalv
            // 
            this.threeGalv.Name = "threeGalv";
            this.threeGalv.Text = "三振镜";
            // 
            // FormScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(244, 459);
            this.Controls.Add(this.inputPanel);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormScan";
            this.Text = "扫描控制";
            this.Load += new System.EventHandler(this.FormScan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1InputPanel.C1InputPanel inputPanel;
        private C1.Win.C1InputPanel.InputButton btnLive;
        private C1.Win.C1InputPanel.InputButton btnCapture;
        private C1.Win.C1InputPanel.InputComboBox cbxScanners;
        private C1.Win.C1InputPanel.InputOption twoGalv;
        private C1.Win.C1InputPanel.InputOption threeGalv;
    }
}