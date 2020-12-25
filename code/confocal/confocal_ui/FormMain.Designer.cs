namespace confocal_ui
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
            this.c1ThemeController = new C1.Win.C1Themes.C1ThemeController();
            this.c1CommandHolder = new C1.Win.C1Command.C1CommandHolder();
            this.c1CommandLink = new C1.Win.C1Command.C1CommandLink();
            this.menu = new C1.Win.C1Command.C1MainMenu();
            this.c1CommandDock = new C1.Win.C1Command.C1CommandDock();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1CommandHolder
            // 
            this.c1CommandHolder.Owner = this;
            // 
            // c1CommandLink
            // 
            this.c1CommandLink.Text = "新命令";
            // 
            // menu
            // 
            this.menu.AccessibleName = "Menu Bar";
            this.menu.CommandHolder = this.c1CommandHolder;
            this.menu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink});
            this.menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 26);
            this.menu.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.menu.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // c1CommandDock
            // 
            this.c1CommandDock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.c1CommandDock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.c1CommandDock.Id = 2;
            this.c1CommandDock.Location = new System.Drawing.Point(0, 26);
            this.c1CommandDock.Name = "c1CommandDock";
            this.c1CommandDock.Size = new System.Drawing.Size(800, 18);
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(87)))), ((int)(((byte)(87)))));
            this.c1CommandDock1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1CommandDock1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.c1CommandDock1.Id = 5;
            this.c1CommandDock1.Location = new System.Drawing.Point(0, 495);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(800, 34);
            this.c1ThemeController.SetTheme(this.c1CommandDock1, "(default)");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 529);
            this.Controls.Add(this.c1CommandDock1);
            this.Controls.Add(this.c1CommandDock);
            this.Controls.Add(this.menu);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Z1 Confocal v";
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Themes.C1ThemeController c1ThemeController;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder;
        private C1.Win.C1Command.C1MainMenu menu;
        private C1.Win.C1Command.C1CommandLink c1CommandLink;
        private C1.Win.C1Command.C1CommandDock c1CommandDock;
        private C1.Win.C1Command.C1CommandDock c1CommandDock1;
    }
}