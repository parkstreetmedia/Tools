namespace EC2Control {
    partial class Settings {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.btnSave = new System.Windows.Forms.Button();
            this.lblProfile = new System.Windows.Forms.Label();
            this.txtBoxAWSProfileName = new System.Windows.Forms.TextBox();
            this.lblAMI = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtBoxIPAddress = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbBoxInstanceType = new System.Windows.Forms.ComboBox();
            this.txtBoxSecurityGroup = new System.Windows.Forms.TextBox();
            this.lblSecGrp = new System.Windows.Forms.Label();
            this.txtBoxURLToStartupPackage = new System.Windows.Forms.TextBox();
            this.lblStartupPackage = new System.Windows.Forms.Label();
            this.chkBoxShouldUseStartupPackage = new System.Windows.Forms.CheckBox();
            this.txtBoxAWSAccessKey = new System.Windows.Forms.TextBox();
            this.lblAccessKey = new System.Windows.Forms.Label();
            this.txtBoxAWSSecretKey = new System.Windows.Forms.TextBox();
            this.lblSecretKey = new System.Windows.Forms.Label();
            this.cmbBoxHours = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkBoxShouldWeMonitor = new System.Windows.Forms.CheckBox();
            this.lblMonitorGrpBox = new System.Windows.Forms.GroupBox();
            this.lblHours1 = new System.Windows.Forms.Label();
            this.comboBoxAMI = new System.Windows.Forms.ComboBox();
            this.lblMonitorGrpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(67, 475);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(223, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfile.Location = new System.Drawing.Point(12, 19);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(124, 17);
            this.lblProfile.TabIndex = 1;
            this.lblProfile.Text = "AWS Profile Name";
            // 
            // txtBoxAWSProfileName
            // 
            this.txtBoxAWSProfileName.Location = new System.Drawing.Point(142, 19);
            this.txtBoxAWSProfileName.Name = "txtBoxAWSProfileName";
            this.txtBoxAWSProfileName.Size = new System.Drawing.Size(223, 20);
            this.txtBoxAWSProfileName.TabIndex = 2;
            // 
            // lblAMI
            // 
            this.lblAMI.AutoSize = true;
            this.lblAMI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAMI.Location = new System.Drawing.Point(13, 127);
            this.lblAMI.Name = "lblAMI";
            this.lblAMI.Size = new System.Drawing.Size(31, 17);
            this.lblAMI.TabIndex = 5;
            this.lblAMI.Text = "AMI";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(13, 161);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(76, 17);
            this.lblIP.TabIndex = 7;
            this.lblIP.Text = "IP Address";
            // 
            // txtBoxIPAddress
            // 
            this.txtBoxIPAddress.Location = new System.Drawing.Point(143, 158);
            this.txtBoxIPAddress.Name = "txtBoxIPAddress";
            this.txtBoxIPAddress.Size = new System.Drawing.Size(223, 20);
            this.txtBoxIPAddress.TabIndex = 8;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(13, 194);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(97, 17);
            this.lblType.TabIndex = 9;
            this.lblType.Text = "Instance Type";
            // 
            // cmbBoxInstanceType
            // 
            this.cmbBoxInstanceType.FormattingEnabled = true;
            this.cmbBoxInstanceType.Items.AddRange(new object[] {
            "t2.small",
            "t2.medium",
            "m3.medium",
            "m3.large",
            "m3.xlarge",
            "m3.2xlarge",
            "c3.large",
            "c3.xlarge",
            "c3.2xlarge",
            "c3.4xlarge",
            "c3.8xlarge",
            "r3.large",
            "r3.xlarge",
            "r3.2xlarge",
            "r3.4xlarge",
            "r3.8xlarge",
            "c4.large",
            "c4.xlarge",
            "c4.2xlarge",
            "c4.4xlarge",
            "c4.8xlarge",
            "t2.large",
            "m4.large",
            "m4.xlarge",
            "m4.2xlarge",
            "m4.4xlarge"});
            this.cmbBoxInstanceType.Location = new System.Drawing.Point(143, 191);
            this.cmbBoxInstanceType.Name = "cmbBoxInstanceType";
            this.cmbBoxInstanceType.Size = new System.Drawing.Size(223, 21);
            this.cmbBoxInstanceType.TabIndex = 10;
            // 
            // txtBoxSecurityGroup
            // 
            this.txtBoxSecurityGroup.Location = new System.Drawing.Point(143, 229);
            this.txtBoxSecurityGroup.Name = "txtBoxSecurityGroup";
            this.txtBoxSecurityGroup.Size = new System.Drawing.Size(223, 20);
            this.txtBoxSecurityGroup.TabIndex = 12;
            // 
            // lblSecGrp
            // 
            this.lblSecGrp.AutoSize = true;
            this.lblSecGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecGrp.Location = new System.Drawing.Point(13, 232);
            this.lblSecGrp.Name = "lblSecGrp";
            this.lblSecGrp.Size = new System.Drawing.Size(103, 17);
            this.lblSecGrp.TabIndex = 11;
            this.lblSecGrp.Text = "Security Group";
            // 
            // txtBoxURLToStartupPackage
            // 
            this.txtBoxURLToStartupPackage.Location = new System.Drawing.Point(15, 326);
            this.txtBoxURLToStartupPackage.Name = "txtBoxURLToStartupPackage";
            this.txtBoxURLToStartupPackage.Size = new System.Drawing.Size(350, 20);
            this.txtBoxURLToStartupPackage.TabIndex = 14;
            // 
            // lblStartupPackage
            // 
            this.lblStartupPackage.AutoSize = true;
            this.lblStartupPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartupPackage.Location = new System.Drawing.Point(12, 302);
            this.lblStartupPackage.Name = "lblStartupPackage";
            this.lblStartupPackage.Size = new System.Drawing.Size(161, 17);
            this.lblStartupPackage.TabIndex = 13;
            this.lblStartupPackage.Text = "URL to Startup Package";
            // 
            // chkBoxShouldUseStartupPackage
            // 
            this.chkBoxShouldUseStartupPackage.AutoSize = true;
            this.chkBoxShouldUseStartupPackage.Checked = true;
            this.chkBoxShouldUseStartupPackage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxShouldUseStartupPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxShouldUseStartupPackage.Location = new System.Drawing.Point(10, 274);
            this.chkBoxShouldUseStartupPackage.Name = "chkBoxShouldUseStartupPackage";
            this.chkBoxShouldUseStartupPackage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkBoxShouldUseStartupPackage.Size = new System.Drawing.Size(266, 21);
            this.chkBoxShouldUseStartupPackage.TabIndex = 15;
            this.chkBoxShouldUseStartupPackage.Text = "?Should We Use the Startup Package";
            this.chkBoxShouldUseStartupPackage.UseVisualStyleBackColor = true;
            // 
            // txtBoxAWSAccessKey
            // 
            this.txtBoxAWSAccessKey.Location = new System.Drawing.Point(142, 53);
            this.txtBoxAWSAccessKey.Name = "txtBoxAWSAccessKey";
            this.txtBoxAWSAccessKey.Size = new System.Drawing.Size(223, 20);
            this.txtBoxAWSAccessKey.TabIndex = 17;
            // 
            // lblAccessKey
            // 
            this.lblAccessKey.AutoSize = true;
            this.lblAccessKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccessKey.Location = new System.Drawing.Point(12, 53);
            this.lblAccessKey.Name = "lblAccessKey";
            this.lblAccessKey.Size = new System.Drawing.Size(116, 17);
            this.lblAccessKey.TabIndex = 16;
            this.lblAccessKey.Text = "AWS Access Key";
            // 
            // txtBoxAWSSecretKey
            // 
            this.txtBoxAWSSecretKey.Location = new System.Drawing.Point(142, 88);
            this.txtBoxAWSSecretKey.Name = "txtBoxAWSSecretKey";
            this.txtBoxAWSSecretKey.Size = new System.Drawing.Size(223, 20);
            this.txtBoxAWSSecretKey.TabIndex = 19;
            // 
            // lblSecretKey
            // 
            this.lblSecretKey.AutoSize = true;
            this.lblSecretKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecretKey.Location = new System.Drawing.Point(12, 88);
            this.lblSecretKey.Name = "lblSecretKey";
            this.lblSecretKey.Size = new System.Drawing.Size(112, 17);
            this.lblSecretKey.TabIndex = 18;
            this.lblSecretKey.Text = "AWS Secret Key";
            // 
            // cmbBoxHours
            // 
            this.cmbBoxHours.FormattingEnabled = true;
            this.cmbBoxHours.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.cmbBoxHours.Location = new System.Drawing.Point(171, 55);
            this.cmbBoxHours.Name = "cmbBoxHours";
            this.cmbBoxHours.Size = new System.Drawing.Size(66, 21);
            this.cmbBoxHours.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Turn off the server after  ";
            // 
            // chkBoxShouldWeMonitor
            // 
            this.chkBoxShouldWeMonitor.AutoSize = true;
            this.chkBoxShouldWeMonitor.Checked = true;
            this.chkBoxShouldWeMonitor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxShouldWeMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxShouldWeMonitor.Location = new System.Drawing.Point(7, 19);
            this.chkBoxShouldWeMonitor.Name = "chkBoxShouldWeMonitor";
            this.chkBoxShouldWeMonitor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkBoxShouldWeMonitor.Size = new System.Drawing.Size(222, 21);
            this.chkBoxShouldWeMonitor.TabIndex = 24;
            this.chkBoxShouldWeMonitor.Text = "?Should We Enable Monitoring";
            this.chkBoxShouldWeMonitor.UseVisualStyleBackColor = true;
            // 
            // lblMonitorGrpBox
            // 
            this.lblMonitorGrpBox.Controls.Add(this.lblHours1);
            this.lblMonitorGrpBox.Controls.Add(this.chkBoxShouldWeMonitor);
            this.lblMonitorGrpBox.Controls.Add(this.cmbBoxHours);
            this.lblMonitorGrpBox.Controls.Add(this.label2);
            this.lblMonitorGrpBox.Location = new System.Drawing.Point(3, 364);
            this.lblMonitorGrpBox.Name = "lblMonitorGrpBox";
            this.lblMonitorGrpBox.Size = new System.Drawing.Size(385, 95);
            this.lblMonitorGrpBox.TabIndex = 25;
            this.lblMonitorGrpBox.TabStop = false;
            this.lblMonitorGrpBox.Text = "Monitor";
            // 
            // lblHours1
            // 
            this.lblHours1.AutoSize = true;
            this.lblHours1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHours1.Location = new System.Drawing.Point(243, 56);
            this.lblHours1.Name = "lblHours1";
            this.lblHours1.Size = new System.Drawing.Size(44, 17);
            this.lblHours1.TabIndex = 25;
            this.lblHours1.Text = "hours";
            // 
            // comboBoxAMI
            // 
            this.comboBoxAMI.FormattingEnabled = true;
            this.comboBoxAMI.Location = new System.Drawing.Point(142, 123);
            this.comboBoxAMI.Name = "comboBoxAMI";
            this.comboBoxAMI.Size = new System.Drawing.Size(223, 21);
            this.comboBoxAMI.TabIndex = 26;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 520);
            this.Controls.Add(this.comboBoxAMI);
            this.Controls.Add(this.lblMonitorGrpBox);
            this.Controls.Add(this.txtBoxAWSSecretKey);
            this.Controls.Add(this.lblSecretKey);
            this.Controls.Add(this.txtBoxAWSAccessKey);
            this.Controls.Add(this.lblAccessKey);
            this.Controls.Add(this.chkBoxShouldUseStartupPackage);
            this.Controls.Add(this.txtBoxURLToStartupPackage);
            this.Controls.Add(this.lblStartupPackage);
            this.Controls.Add(this.txtBoxSecurityGroup);
            this.Controls.Add(this.lblSecGrp);
            this.Controls.Add(this.cmbBoxInstanceType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtBoxIPAddress);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.lblAMI);
            this.Controls.Add(this.txtBoxAWSProfileName);
            this.Controls.Add(this.lblProfile);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings";
            this.lblMonitorGrpBox.ResumeLayout(false);
            this.lblMonitorGrpBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.TextBox txtBoxAWSProfileName;
        private System.Windows.Forms.Label lblAMI;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtBoxIPAddress;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbBoxInstanceType;
        private System.Windows.Forms.TextBox txtBoxSecurityGroup;
        private System.Windows.Forms.Label lblSecGrp;
        private System.Windows.Forms.TextBox txtBoxURLToStartupPackage;
        private System.Windows.Forms.Label lblStartupPackage;
        private System.Windows.Forms.CheckBox chkBoxShouldUseStartupPackage;
        private System.Windows.Forms.TextBox txtBoxAWSAccessKey;
        private System.Windows.Forms.Label lblAccessKey;
        private System.Windows.Forms.TextBox txtBoxAWSSecretKey;
        private System.Windows.Forms.Label lblSecretKey;
        private System.Windows.Forms.ComboBox cmbBoxHours;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkBoxShouldWeMonitor;
        private System.Windows.Forms.GroupBox lblMonitorGrpBox;
        private System.Windows.Forms.Label lblHours1;
        private System.Windows.Forms.ComboBox comboBoxAMI;
    }
}