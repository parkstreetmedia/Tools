namespace RRHDMIControl
{
    partial class ConnectionSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionSettings));
            this.grpBoxSettings = new System.Windows.Forms.GroupBox();
            this.serialCommPortCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxSettings
            // 
            this.grpBoxSettings.Controls.Add(this.btnExit);
            this.grpBoxSettings.Controls.Add(this.serialCommPortCombo);
            this.grpBoxSettings.Controls.Add(this.label1);
            this.grpBoxSettings.Location = new System.Drawing.Point(3, 5);
            this.grpBoxSettings.Name = "grpBoxSettings";
            this.grpBoxSettings.Size = new System.Drawing.Size(178, 75);
            this.grpBoxSettings.TabIndex = 0;
            this.grpBoxSettings.TabStop = false;
            // 
            // serialCommPortCombo
            // 
            this.serialCommPortCombo.FormattingEnabled = true;
            this.serialCommPortCombo.Items.AddRange(new object[] {
            "Com1",
            "Com2 ",
            "Com3 ",
            "Com4"});
            this.serialCommPortCombo.Location = new System.Drawing.Point(70, 15);
            this.serialCommPortCombo.Name = "serialCommPortCombo";
            this.serialCommPortCombo.Size = new System.Drawing.Size(93, 21);
            this.serialCommPortCombo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Com Port: ";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Maroon;
            this.btnExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExit.Location = new System.Drawing.Point(72, 42);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(91, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Don\'t Press Me!";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.UseWaitCursor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ConnectionSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(187, 80);
            this.Controls.Add(this.grpBoxSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionSettings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionSettings_FormClosing);
            this.grpBoxSettings.ResumeLayout(false);
            this.grpBoxSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox serialCommPortCombo;
        private System.Windows.Forms.Button btnExit;
    }
}