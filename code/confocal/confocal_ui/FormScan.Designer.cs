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
            this.c1ThemeController = new C1.Win.C1Themes.C1ThemeController();
            this.btnLive = new C1.Win.C1Input.C1Button();
            this.btnCapture = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLive
            // 
            this.btnLive.Location = new System.Drawing.Point(12, 12);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(60, 50);
            this.btnLive.TabIndex = 0;
            this.btnLive.Text = "实时";
            this.c1ThemeController.SetTheme(this.btnLive, "(default)");
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(78, 12);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(60, 50);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "捕捉";
            this.c1ThemeController.SetTheme(this.btnCapture, "(default)");
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // FormScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 561);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.btnLive);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormScan";
            this.Text = "扫描控制";
            this.c1ThemeController.SetTheme(this, "(default)");
            this.Load += new System.EventHandler(this.FormScan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCapture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController c1ThemeController;
        private C1.Win.C1Input.C1Button btnLive;
        private C1.Win.C1Input.C1Button btnCapture;
    }
}