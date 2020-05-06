namespace confocal_test
{
    partial class FormEntry
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
            this.btnCfg = new System.Windows.Forms.Button();
            this.btnMain = new System.Windows.Forms.Button();
            this.btnLaser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCfg
            // 
            this.btnCfg.Location = new System.Drawing.Point(12, 12);
            this.btnCfg.Name = "btnCfg";
            this.btnCfg.Size = new System.Drawing.Size(127, 35);
            this.btnCfg.TabIndex = 0;
            this.btnCfg.Text = "Config Test";
            this.btnCfg.UseVisualStyleBackColor = true;
            this.btnCfg.Click += new System.EventHandler(this.btnCfg_Click);
            // 
            // btnMain
            // 
            this.btnMain.Location = new System.Drawing.Point(12, 53);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(127, 35);
            this.btnMain.TabIndex = 1;
            this.btnMain.Text = "Main";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnLaser
            // 
            this.btnLaser.Location = new System.Drawing.Point(145, 12);
            this.btnLaser.Name = "btnLaser";
            this.btnLaser.Size = new System.Drawing.Size(127, 35);
            this.btnLaser.TabIndex = 2;
            this.btnLaser.Text = "Laser Test";
            this.btnLaser.UseVisualStyleBackColor = true;
            this.btnLaser.Click += new System.EventHandler(this.btnLaser_Click);
            // 
            // FormEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 353);
            this.Controls.Add(this.btnLaser);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.btnCfg);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormEntry";
            this.Text = "测试入口";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCfg;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Button btnLaser;
    }
}

