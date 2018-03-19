namespace YouUp
{
    partial class YouUp {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YouUp));
            this.btnStartUpload = new System.Windows.Forms.Button();
            this.lblFileDrop = new System.Windows.Forms.Label();
            this.panelDropFile = new System.Windows.Forms.Panel();
            this.lblVideoTitle = new System.Windows.Forms.Label();
            this.lblScripture = new System.Windows.Forms.Label();
            this.dropDownPrivacy = new System.Windows.Forms.ComboBox();
            this.lblPrivacy = new System.Windows.Forms.Label();
            this.txtBoxTitle = new System.Windows.Forms.TextBox();
            this.txtBoxDescription = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelDropFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartUpload
            // 
            this.btnStartUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartUpload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStartUpload.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btnStartUpload.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartUpload.Location = new System.Drawing.Point(16, 397);
            this.btnStartUpload.Name = "btnStartUpload";
            this.btnStartUpload.Size = new System.Drawing.Size(273, 33);
            this.btnStartUpload.TabIndex = 2;
            this.btnStartUpload.Text = "Start Upload";
            this.btnStartUpload.UseVisualStyleBackColor = false;
            this.btnStartUpload.Click += new System.EventHandler(this.btnStartUpload_Click);
            // 
            // lblFileDrop
            // 
            this.lblFileDrop.AutoSize = true;
            this.lblFileDrop.Font = new System.Drawing.Font("Open Sans", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileDrop.Location = new System.Drawing.Point(95, 42);
            this.lblFileDrop.Name = "lblFileDrop";
            this.lblFileDrop.Size = new System.Drawing.Size(310, 51);
            this.lblFileDrop.TabIndex = 3;
            this.lblFileDrop.Text = "Drop Video Here";
            // 
            // panelDropFile
            // 
            this.panelDropFile.AllowDrop = true;
            this.panelDropFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelDropFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDropFile.Controls.Add(this.lblFileDrop);
            this.panelDropFile.Location = new System.Drawing.Point(12, 12);
            this.panelDropFile.Name = "panelDropFile";
            this.panelDropFile.Size = new System.Drawing.Size(508, 142);
            this.panelDropFile.TabIndex = 4;
            // 
            // lblVideoTitle
            // 
            this.lblVideoTitle.AutoSize = true;
            this.lblVideoTitle.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVideoTitle.Location = new System.Drawing.Point(12, 174);
            this.lblVideoTitle.Name = "lblVideoTitle";
            this.lblVideoTitle.Size = new System.Drawing.Size(81, 20);
            this.lblVideoTitle.TabIndex = 5;
            this.lblVideoTitle.Text = "Video Title";
            // 
            // lblScripture
            // 
            this.lblScripture.AutoSize = true;
            this.lblScripture.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScripture.Location = new System.Drawing.Point(12, 235);
            this.lblScripture.Name = "lblScripture";
            this.lblScripture.Size = new System.Drawing.Size(131, 20);
            this.lblScripture.TabIndex = 6;
            this.lblScripture.Text = "Video Description";
            // 
            // dropDownPrivacy
            // 
            this.dropDownPrivacy.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropDownPrivacy.FormattingEnabled = true;
            this.dropDownPrivacy.Items.AddRange(new object[] {
            "unlisted",
            "private"});
            this.dropDownPrivacy.Location = new System.Drawing.Point(16, 354);
            this.dropDownPrivacy.Name = "dropDownPrivacy";
            this.dropDownPrivacy.Size = new System.Drawing.Size(149, 28);
            this.dropDownPrivacy.TabIndex = 7;
            // 
            // lblPrivacy
            // 
            this.lblPrivacy.AutoSize = true;
            this.lblPrivacy.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrivacy.Location = new System.Drawing.Point(12, 331);
            this.lblPrivacy.Name = "lblPrivacy";
            this.lblPrivacy.Size = new System.Drawing.Size(59, 20);
            this.lblPrivacy.TabIndex = 8;
            this.lblPrivacy.Text = "Privacy";
            // 
            // txtBoxTitle
            // 
            this.txtBoxTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxTitle.Location = new System.Drawing.Point(15, 196);
            this.txtBoxTitle.Name = "txtBoxTitle";
            this.txtBoxTitle.Size = new System.Drawing.Size(505, 24);
            this.txtBoxTitle.TabIndex = 9;
            // 
            // txtBoxDescription
            // 
            this.txtBoxDescription.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxDescription.Location = new System.Drawing.Point(15, 258);
            this.txtBoxDescription.Multiline = true;
            this.txtBoxDescription.Name = "txtBoxDescription";
            this.txtBoxDescription.Size = new System.Drawing.Size(504, 64);
            this.txtBoxDescription.TabIndex = 10;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 453);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(504, 34);
            this.progressBar.TabIndex = 11;
            // 
            // YouUp
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(532, 499);
            this.Controls.Add(this.panelDropFile);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtBoxDescription);
            this.Controls.Add(this.txtBoxTitle);
            this.Controls.Add(this.lblPrivacy);
            this.Controls.Add(this.dropDownPrivacy);
            this.Controls.Add(this.lblScripture);
            this.Controls.Add(this.lblVideoTitle);
            this.Controls.Add(this.btnStartUpload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(395, 115);
            this.Name = "YouUp";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Youtube Uploader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUIClosing);
            this.panelDropFile.ResumeLayout(false);
            this.panelDropFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button btnStartUpload;
        private System.Windows.Forms.Label lblFileDrop;
        private System.Windows.Forms.Panel panelDropFile;
        private System.Windows.Forms.Label lblVideoTitle;
        private System.Windows.Forms.Label lblScripture;
        private System.Windows.Forms.ComboBox dropDownPrivacy;
        private System.Windows.Forms.Label lblPrivacy;
        private System.Windows.Forms.TextBox txtBoxTitle;
        private System.Windows.Forms.TextBox txtBoxDescription;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}