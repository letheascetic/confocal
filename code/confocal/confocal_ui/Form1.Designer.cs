namespace ThemeManager
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ctdbgThemes = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.btnAdd = new C1.Win.C1Input.C1Button();
            this.btnRemove = new C1.Win.C1Input.C1Button();
            this.btnSetAsApplicationTheme = new C1.Win.C1Input.C1Button();
            this.c1ThemeController1 = new C1.Win.C1Themes.C1ThemeController();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.tbApplicationTheme = new C1.Win.C1Input.C1TextBox();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            this.c1Label3 = new C1.Win.C1Input.C1Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ctdbgThemes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetAsApplicationTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbApplicationTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
            this.SuspendLayout();
            // 
            // ctdbgThemes
            // 
            this.ctdbgThemes.AllowColMove = false;
            this.ctdbgThemes.AllowUpdate = false;
            this.ctdbgThemes.AlternatingRows = true;
            this.ctdbgThemes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctdbgThemes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ctdbgThemes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ctdbgThemes.CaptionHeight = 17;
            this.ctdbgThemes.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.ctdbgThemes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ctdbgThemes.GroupByCaption = "Drag a column header here to group by that column";
            this.ctdbgThemes.Images.Add(((System.Drawing.Image)(resources.GetObject("ctdbgThemes.Images"))));
            this.ctdbgThemes.Location = new System.Drawing.Point(12, 49);
            this.ctdbgThemes.Name = "ctdbgThemes";
            this.ctdbgThemes.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ctdbgThemes.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ctdbgThemes.PreviewInfo.ZoomFactor = 75D;
            this.ctdbgThemes.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ctdbgThemes.PrintInfo.PageSettings")));
            this.ctdbgThemes.RowDivider.Color = System.Drawing.Color.Gray;
            this.ctdbgThemes.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.ctdbgThemes.RowHeight = 15;
            this.ctdbgThemes.RowSubDividerColor = System.Drawing.Color.Gray;
            this.ctdbgThemes.Size = new System.Drawing.Size(300, 279);
            this.ctdbgThemes.TabIndex = 3;
            this.ctdbgThemes.Text = "c1TrueDBGrid1";
            this.c1ThemeController1.SetTheme(this.ctdbgThemes, "(default)");
            this.ctdbgThemes.UseCompatibleTextRendering = false;
            this.ctdbgThemes.DoubleClick += new System.EventHandler(this.ctdbgThemes_DoubleClick);
            this.ctdbgThemes.Resize += new System.EventHandler(this.ctdbgThemes_Resize);
            this.ctdbgThemes.PropBag = resources.GetString("ctdbgThemes.PropBag");
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnAdd.Location = new System.Drawing.Point(118, 365);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add...";
            this.c1ThemeController1.SetTheme(this.btnAdd, "(default)");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRemove.Location = new System.Drawing.Point(118, 394);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.c1ThemeController1.SetTheme(this.btnRemove, "(default)");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSetAsApplicationTheme
            // 
            this.btnSetAsApplicationTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetAsApplicationTheme.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSetAsApplicationTheme.Location = new System.Drawing.Point(118, 336);
            this.btnSetAsApplicationTheme.Name = "btnSetAsApplicationTheme";
            this.btnSetAsApplicationTheme.Size = new System.Drawing.Size(75, 23);
            this.btnSetAsApplicationTheme.TabIndex = 5;
            this.btnSetAsApplicationTheme.Text = "Apply";
            this.c1ThemeController1.SetTheme(this.btnSetAsApplicationTheme, "(default)");
            this.btnSetAsApplicationTheme.UseVisualStyleBackColor = true;
            this.btnSetAsApplicationTheme.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnSetAsApplicationTheme.Click += new System.EventHandler(this.btnSetAsApplicationTheme_Click);
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.c1Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.c1Label1.Location = new System.Drawing.Point(12, 9);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(77, 19);
            this.c1Label1.TabIndex = 0;
            this.c1Label1.Tag = null;
            this.c1Label1.Text = "Current theme:";
            this.c1Label1.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.c1Label1, "(default)");
            this.c1Label1.UseCompatibleTextRendering = true;
            // 
            // tbApplicationTheme
            // 
            this.tbApplicationTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbApplicationTheme.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.tbApplicationTheme.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbApplicationTheme.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(172)))), ((int)(((byte)(172)))));
            this.tbApplicationTheme.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.tbApplicationTheme.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbApplicationTheme.Location = new System.Drawing.Point(118, 7);
            this.tbApplicationTheme.Name = "tbApplicationTheme";
            this.tbApplicationTheme.ReadOnly = true;
            this.tbApplicationTheme.Size = new System.Drawing.Size(194, 20);
            this.tbApplicationTheme.TabIndex = 1;
            this.tbApplicationTheme.Tag = null;
            this.c1ThemeController1.SetTheme(this.tbApplicationTheme, "(default)");
            this.tbApplicationTheme.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // c1Label2
            // 
            this.c1Label2.AutoSize = true;
            this.c1Label2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.c1Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.c1Label2.Location = new System.Drawing.Point(12, 30);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(89, 19);
            this.c1Label2.TabIndex = 2;
            this.c1Label2.Tag = null;
            this.c1Label2.Text = "Available themes:";
            this.c1Label2.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.c1Label2, "(default)");
            this.c1Label2.UseCompatibleTextRendering = true;
            // 
            // c1Label3
            // 
            this.c1Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.c1Label3.AutoSize = true;
            this.c1Label3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.c1Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.c1Label3.Location = new System.Drawing.Point(12, 341);
            this.c1Label3.Name = "c1Label3";
            this.c1Label3.Size = new System.Drawing.Size(77, 19);
            this.c1Label3.TabIndex = 4;
            this.c1Label3.Tag = null;
            this.c1Label3.Text = "Theme actions:";
            this.c1Label3.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.c1Label3, "(default)");
            this.c1Label3.UseCompatibleTextRendering = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "c1theme";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "C1Theme files (*.c1theme)|*.c1theme|All files (*.*)|*.*";
            this.openFileDialog1.Title = "Select Theme";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 426);
            this.Controls.Add(this.tbApplicationTheme);
            this.Controls.Add(this.c1Label3);
            this.Controls.Add(this.c1Label2);
            this.Controls.Add(this.c1Label1);
            this.Controls.Add(this.btnSetAsApplicationTheme);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.ctdbgThemes);
            this.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Name = "Form1";
            this.Text = "Theme Manager";
            this.c1ThemeController1.SetTheme(this, "(default)");
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ctdbgThemes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetAsApplicationTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbApplicationTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ctdbgThemes;
        private C1.Win.C1Input.C1Button btnAdd;
        private C1.Win.C1Input.C1Button btnRemove;
        private C1.Win.C1Input.C1Button btnSetAsApplicationTheme;
        private C1.Win.C1Themes.C1ThemeController c1ThemeController1;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1TextBox tbApplicationTheme;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private C1.Win.C1Input.C1Label c1Label2;
        private C1.Win.C1Input.C1Label c1Label3;
    }
}

