namespace AirSupply
{
    partial class ViewAWSLogs
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewAWSLogs));
            this.copyToClipIAM = new System.Windows.Forms.Button();
            this.credsIAM = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.credsUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.credsPass = new System.Windows.Forms.TextBox();
            this.copyToClipUser = new System.Windows.Forms.Button();
            this.copyToClipPass = new System.Windows.Forms.Button();
            this.goButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // copyToClipIAM
            // 
            this.copyToClipIAM.Location = new System.Drawing.Point(207, 130);
            this.copyToClipIAM.Name = "copyToClipIAM";
            this.copyToClipIAM.Size = new System.Drawing.Size(104, 23);
            this.copyToClipIAM.TabIndex = 0;
            this.copyToClipIAM.Text = "copy to clipboard";
            this.copyToClipIAM.UseVisualStyleBackColor = true;
            this.copyToClipIAM.Click += new System.EventHandler(this.copyToClipIAM_Click);
            // 
            // credsIAM
            // 
            this.credsIAM.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.credsIAM.Location = new System.Drawing.Point(16, 124);
            this.credsIAM.Name = "credsIAM";
            this.credsIAM.ReadOnly = true;
            this.credsIAM.Size = new System.Drawing.Size(154, 29);
            this.credsIAM.TabIndex = 1;
            this.credsIAM.WordWrap = false;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(12, 18);
            this.lblMessage.MaximumSize = new System.Drawing.Size(500, 200);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(480, 60);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "I am cheap and so am directing you to the Amazon Console where you can view the t" +
    "ables without incuring expense! Login using the details here:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "IAM Account";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Username";
            // 
            // credsUser
            // 
            this.credsUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.credsUser.Location = new System.Drawing.Point(16, 206);
            this.credsUser.Name = "credsUser";
            this.credsUser.ReadOnly = true;
            this.credsUser.Size = new System.Drawing.Size(185, 29);
            this.credsUser.TabIndex = 4;
            this.credsUser.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password";
            // 
            // credsPass
            // 
            this.credsPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.credsPass.Location = new System.Drawing.Point(16, 292);
            this.credsPass.Name = "credsPass";
            this.credsPass.ReadOnly = true;
            this.credsPass.Size = new System.Drawing.Size(185, 29);
            this.credsPass.TabIndex = 6;
            this.credsPass.WordWrap = false;
            // 
            // copyToClipUser
            // 
            this.copyToClipUser.Location = new System.Drawing.Point(207, 212);
            this.copyToClipUser.Name = "copyToClipUser";
            this.copyToClipUser.Size = new System.Drawing.Size(104, 23);
            this.copyToClipUser.TabIndex = 8;
            this.copyToClipUser.Text = "copy to clipboard";
            this.copyToClipUser.UseVisualStyleBackColor = true;
            this.copyToClipUser.Click += new System.EventHandler(this.copyToClipUser_Click);
            // 
            // copyToClipPass
            // 
            this.copyToClipPass.Location = new System.Drawing.Point(207, 298);
            this.copyToClipPass.Name = "copyToClipPass";
            this.copyToClipPass.Size = new System.Drawing.Size(104, 23);
            this.copyToClipPass.TabIndex = 9;
            this.copyToClipPass.Text = "copy to clipboard";
            this.copyToClipPass.UseVisualStyleBackColor = true;
            this.copyToClipPass.Click += new System.EventHandler(this.copyToClipPass_Click);
            // 
            // goButton
            // 
            this.goButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goButton.Location = new System.Drawing.Point(352, 155);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(125, 90);
            this.goButton.TabIndex = 10;
            this.goButton.Text = "Open AWS Console";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // ViewAWSLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 333);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.copyToClipPass);
            this.Controls.Add(this.copyToClipUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.credsPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.credsUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.credsIAM);
            this.Controls.Add(this.copyToClipIAM);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ViewAWSLogs";
            this.Text = "View Logs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button copyToClipIAM;
        private System.Windows.Forms.TextBox credsIAM;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox credsUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox credsPass;
        private System.Windows.Forms.Button copyToClipUser;
        private System.Windows.Forms.Button copyToClipPass;
        private System.Windows.Forms.Button goButton;
    }
}