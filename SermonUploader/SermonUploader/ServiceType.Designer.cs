namespace SermonUploader
{
    partial class ServiceType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceType));
            this.morningBtn = new System.Windows.Forms.Button();
            this.eveningButton = new System.Windows.Forms.Button();
            this.lblWhich = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // morningBtn
            // 
            this.morningBtn.BackColor = System.Drawing.Color.SkyBlue;
            this.morningBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.morningBtn.ForeColor = System.Drawing.Color.Black;
            this.morningBtn.Location = new System.Drawing.Point(12, 67);
            this.morningBtn.Name = "morningBtn";
            this.morningBtn.Size = new System.Drawing.Size(189, 90);
            this.morningBtn.TabIndex = 0;
            this.morningBtn.Text = "Morning Sermon";
            this.morningBtn.UseVisualStyleBackColor = false;
            this.morningBtn.Click += new System.EventHandler(this.morningBtn_Click);
            // 
            // eveningButton
            // 
            this.eveningButton.BackColor = System.Drawing.Color.Black;
            this.eveningButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eveningButton.ForeColor = System.Drawing.Color.White;
            this.eveningButton.Location = new System.Drawing.Point(346, 67);
            this.eveningButton.Name = "eveningButton";
            this.eveningButton.Size = new System.Drawing.Size(186, 90);
            this.eveningButton.TabIndex = 1;
            this.eveningButton.Text = "Evening Sermon";
            this.eveningButton.UseVisualStyleBackColor = false;
            this.eveningButton.Click += new System.EventHandler(this.eveningButton_Click);
            // 
            // lblWhich
            // 
            this.lblWhich.AutoSize = true;
            this.lblWhich.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhich.Location = new System.Drawing.Point(32, 13);
            this.lblWhich.Name = "lblWhich";
            this.lblWhich.Size = new System.Drawing.Size(468, 29);
            this.lblWhich.TabIndex = 2;
            this.lblWhich.Text = "Please select which service this file is from";
            // 
            // ServiceType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 181);
            this.ControlBox = false;
            this.Controls.Add(this.lblWhich);
            this.Controls.Add(this.eveningButton);
            this.Controls.Add(this.morningBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceType";
            this.Text = "Which Service?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button morningBtn;
        private System.Windows.Forms.Button eveningButton;
        private System.Windows.Forms.Label lblWhich;
    }
}