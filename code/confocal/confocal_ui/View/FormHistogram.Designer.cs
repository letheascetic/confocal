
namespace confocal_ui.View
{
    partial class FormHistogram
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
            this.inputPanel = new C1.Win.C1InputPanel.C1InputPanel();
            this.histogramBox = new Emgu.CV.UI.HistogramBox();
            this.cbxChannel = new C1.Win.C1InputPanel.InputComboBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputPanel.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.inputPanel.Items.Add(this.cbxChannel);
            this.inputPanel.Location = new System.Drawing.Point(0, 224);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(272, 25);
            this.inputPanel.TabIndex = 0;
            // 
            // histogramBox
            // 
            this.histogramBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.histogramBox.Location = new System.Drawing.Point(0, 0);
            this.histogramBox.Name = "histogramBox";
            this.histogramBox.Size = new System.Drawing.Size(272, 224);
            this.histogramBox.TabIndex = 1;
            // 
            // cbxChannel
            // 
            this.cbxChannel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxChannel.Name = "cbxChannel";
            this.cbxChannel.Width = 50;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.ImageToUpdate);
            // 
            // FormHistogram
            // 
            this.ClientSize = new System.Drawing.Size(272, 249);
            this.Controls.Add(this.histogramBox);
            this.Controls.Add(this.inputPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(280, 280);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(280, 280);
            this.Name = "FormHistogram";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.inputPanel)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private C1.Win.C1InputPanel.C1InputPanel inputPanel;
        private Emgu.CV.UI.HistogramBox histogramBox;
        private C1.Win.C1InputPanel.InputComboBox cbxChannel;
        private System.Windows.Forms.Timer timer;
    }
}
