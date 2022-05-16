namespace UnamDownloader
{
    partial class File
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(File));
            this.formFile = new MephTheme();
            this.labelFileFilename = new System.Windows.Forms.Label();
            this.txtFilename = new MephTextBox();
            this.labelFileExecuteFile = new System.Windows.Forms.Label();
            this.toggleExecute = new MephToggleSwitch();
            this.labelFileDropLocation = new System.Windows.Forms.Label();
            this.comboDropLocation = new MephComboBox();
            this.labelFileDownloadURL = new System.Windows.Forms.Label();
            this.txtDownloadURL = new MephTextBox();
            this.formFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // formFile
            // 
            this.formFile.AccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.formFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.formFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.formFile.Controls.Add(this.labelFileFilename);
            this.formFile.Controls.Add(this.txtFilename);
            this.formFile.Controls.Add(this.labelFileExecuteFile);
            this.formFile.Controls.Add(this.toggleExecute);
            this.formFile.Controls.Add(this.labelFileDropLocation);
            this.formFile.Controls.Add(this.comboDropLocation);
            this.formFile.Controls.Add(this.labelFileDownloadURL);
            this.formFile.Controls.Add(this.txtDownloadURL);
            this.formFile.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.formFile.Location = new System.Drawing.Point(0, 0);
            this.formFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.formFile.Name = "formFile";
            this.formFile.Size = new System.Drawing.Size(201, 283);
            this.formFile.SubHeader = "File to download";
            this.formFile.TabIndex = 0;
            this.formFile.Text = "Edit File";
            // 
            // labelFileFilename
            // 
            this.labelFileFilename.AutoSize = true;
            this.labelFileFilename.BackColor = System.Drawing.Color.Transparent;
            this.labelFileFilename.ForeColor = System.Drawing.Color.Gray;
            this.labelFileFilename.Location = new System.Drawing.Point(15, 71);
            this.labelFileFilename.Name = "labelFileFilename";
            this.labelFileFilename.Size = new System.Drawing.Size(62, 17);
            this.labelFileFilename.TabIndex = 8;
            this.labelFileFilename.Text = "Filename:";
            // 
            // txtFilename
            // 
            this.txtFilename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtFilename.ForeColor = System.Drawing.Color.Silver;
            this.txtFilename.Location = new System.Drawing.Point(18, 91);
            this.txtFilename.MaxLength = 32767;
            this.txtFilename.MultiLine = false;
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(166, 24);
            this.txtFilename.TabIndex = 7;
            this.txtFilename.Text = "File.exe";
            this.txtFilename.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFilename.UseSystemPasswordChar = false;
            this.txtFilename.WordWrap = false;
            // 
            // labelFileExecuteFile
            // 
            this.labelFileExecuteFile.AutoSize = true;
            this.labelFileExecuteFile.BackColor = System.Drawing.Color.Transparent;
            this.labelFileExecuteFile.ForeColor = System.Drawing.Color.Gray;
            this.labelFileExecuteFile.Location = new System.Drawing.Point(15, 241);
            this.labelFileExecuteFile.Name = "labelFileExecuteFile";
            this.labelFileExecuteFile.Size = new System.Drawing.Size(78, 17);
            this.labelFileExecuteFile.TabIndex = 6;
            this.labelFileExecuteFile.Text = "Execute File:";
            // 
            // toggleExecute
            // 
            this.toggleExecute.BackColor = System.Drawing.Color.Transparent;
            this.toggleExecute.Checked = true;
            this.toggleExecute.ForeColor = System.Drawing.Color.Black;
            this.toggleExecute.Location = new System.Drawing.Point(134, 239);
            this.toggleExecute.Name = "toggleExecute";
            this.toggleExecute.Size = new System.Drawing.Size(50, 24);
            this.toggleExecute.TabIndex = 5;
            this.toggleExecute.Text = "toggleExecuteFile";
            // 
            // labelFileDropLocation
            // 
            this.labelFileDropLocation.AutoSize = true;
            this.labelFileDropLocation.BackColor = System.Drawing.Color.Transparent;
            this.labelFileDropLocation.ForeColor = System.Drawing.Color.Gray;
            this.labelFileDropLocation.Location = new System.Drawing.Point(15, 177);
            this.labelFileDropLocation.Name = "labelFileDropLocation";
            this.labelFileDropLocation.Size = new System.Drawing.Size(91, 17);
            this.labelFileDropLocation.TabIndex = 4;
            this.labelFileDropLocation.Text = "Drop location:";
            // 
            // comboDropLocation
            // 
            this.comboDropLocation.BackColor = System.Drawing.Color.Transparent;
            this.comboDropLocation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboDropLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDropLocation.Font = new System.Drawing.Font("Verdana", 8F);
            this.comboDropLocation.ForeColor = System.Drawing.Color.Silver;
            this.comboDropLocation.FormattingEnabled = true;
            this.comboDropLocation.ItemHeight = 16;
            this.comboDropLocation.ItemHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.comboDropLocation.Items.AddRange(new object[] {
            "Temp",
            "AppData",
            "UserProfile",
            "Current Directory"});
            this.comboDropLocation.Location = new System.Drawing.Point(18, 197);
            this.comboDropLocation.Name = "comboDropLocation";
            this.comboDropLocation.Size = new System.Drawing.Size(166, 22);
            this.comboDropLocation.StartIndex = 0;
            this.comboDropLocation.TabIndex = 3;
            // 
            // labelFileDownloadURL
            // 
            this.labelFileDownloadURL.AutoSize = true;
            this.labelFileDownloadURL.BackColor = System.Drawing.Color.Transparent;
            this.labelFileDownloadURL.ForeColor = System.Drawing.Color.Gray;
            this.labelFileDownloadURL.Location = new System.Drawing.Point(15, 122);
            this.labelFileDownloadURL.Name = "labelFileDownloadURL";
            this.labelFileDownloadURL.Size = new System.Drawing.Size(97, 17);
            this.labelFileDownloadURL.TabIndex = 2;
            this.labelFileDownloadURL.Text = "Download URL:";
            // 
            // txtDownloadURL
            // 
            this.txtDownloadURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtDownloadURL.ForeColor = System.Drawing.Color.Silver;
            this.txtDownloadURL.Location = new System.Drawing.Point(18, 142);
            this.txtDownloadURL.MaxLength = 32767;
            this.txtDownloadURL.MultiLine = false;
            this.txtDownloadURL.Name = "txtDownloadURL";
            this.txtDownloadURL.Size = new System.Drawing.Size(166, 24);
            this.txtDownloadURL.TabIndex = 0;
            this.txtDownloadURL.Text = "https://example.com/download.exe";
            this.txtDownloadURL.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDownloadURL.UseSystemPasswordChar = false;
            this.txtDownloadURL.WordWrap = false;
            // 
            // File
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(201, 283);
            this.Controls.Add(this.formFile);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(201, 283);
            this.MinimumSize = new System.Drawing.Size(201, 283);
            this.Name = "File";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit File";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.File_FormClosing);
            this.formFile.ResumeLayout(false);
            this.formFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MephTheme formFile;
        private System.Windows.Forms.Label labelFileDownloadURL;
        private System.Windows.Forms.Label labelFileExecuteFile;
        private System.Windows.Forms.Label labelFileDropLocation;
        private System.Windows.Forms.Label labelFileFilename;
        public MephTextBox txtFilename;
        public MephTextBox txtDownloadURL;
        public MephToggleSwitch toggleExecute;
        public MephComboBox comboDropLocation;
    }
}