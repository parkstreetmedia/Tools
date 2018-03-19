namespace RRHDMIControl {
    partial class HDMISwitchRemote {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HDMISwitchRemote));
            this.grpBoxInputs = new System.Windows.Forms.GroupBox();
            this.changeInput4Checkbox = new System.Windows.Forms.CheckBox();
            this.changeInput3Checkbox = new System.Windows.Forms.CheckBox();
            this.changeInput2Checkbox = new System.Windows.Forms.CheckBox();
            this.changeInput1Checkbox = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.grpSwitcherOut = new System.Windows.Forms.GroupBox();
            this.txtBoxStatus = new System.Windows.Forms.TextBox();
            this.grpBoxInputs.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.grpSwitcherOut.SuspendLayout();
            this.SuspendLayout();
            this.FormClosing += HDMISwitchRemote_FormClosing; 
            // 
            // grpBoxInputs
            // 
            this.grpBoxInputs.Controls.Add(this.changeInput4Checkbox);
            this.grpBoxInputs.Controls.Add(this.changeInput3Checkbox);
            this.grpBoxInputs.Controls.Add(this.changeInput2Checkbox);
            this.grpBoxInputs.Controls.Add(this.changeInput1Checkbox);
            this.grpBoxInputs.Location = new System.Drawing.Point(12, 27);
            this.grpBoxInputs.Name = "grpBoxInputs";
            this.grpBoxInputs.Size = new System.Drawing.Size(168, 173);
            this.grpBoxInputs.TabIndex = 1;
            this.grpBoxInputs.TabStop = false;
            this.grpBoxInputs.Text = "Inputs";
            // 
            // changeInput4Checkbox
            // 
            this.changeInput4Checkbox.Appearance = System.Windows.Forms.Appearance.Button;
            this.changeInput4Checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeInput4Checkbox.Location = new System.Drawing.Point(8, 132);
            this.changeInput4Checkbox.Name = "changeInput4Checkbox";
            this.changeInput4Checkbox.Size = new System.Drawing.Size(152, 29);
            this.changeInput4Checkbox.TabIndex = 7;
            this.changeInput4Checkbox.Text = "Input 4";
            this.changeInput4Checkbox.UseVisualStyleBackColor = true;
            this.changeInput4Checkbox.CheckedChanged += new System.EventHandler(this.changeInput4Checkbox_CheckedChanged);
            // 
            // changeInput3Checkbox
            // 
            this.changeInput3Checkbox.Appearance = System.Windows.Forms.Appearance.Button;
            this.changeInput3Checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeInput3Checkbox.Location = new System.Drawing.Point(8, 95);
            this.changeInput3Checkbox.Name = "changeInput3Checkbox";
            this.changeInput3Checkbox.Size = new System.Drawing.Size(152, 30);
            this.changeInput3Checkbox.TabIndex = 6;
            this.changeInput3Checkbox.Text = "Input 3";
            this.changeInput3Checkbox.UseVisualStyleBackColor = true;
            this.changeInput3Checkbox.CheckedChanged += new System.EventHandler(this.changeInput3Checkbox_CheckedChanged);
            // 
            // changeInput2Checkbox
            // 
            this.changeInput2Checkbox.Appearance = System.Windows.Forms.Appearance.Button;
            this.changeInput2Checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeInput2Checkbox.Location = new System.Drawing.Point(8, 56);
            this.changeInput2Checkbox.Name = "changeInput2Checkbox";
            this.changeInput2Checkbox.Size = new System.Drawing.Size(152, 33);
            this.changeInput2Checkbox.TabIndex = 5;
            this.changeInput2Checkbox.Text = "Input 2";
            this.changeInput2Checkbox.UseVisualStyleBackColor = true;
            this.changeInput2Checkbox.CheckedChanged += new System.EventHandler(this.changeInput2Checkbox_CheckedChanged);
            // 
            // changeInput1Checkbox
            // 
            this.changeInput1Checkbox.Appearance = System.Windows.Forms.Appearance.Button;
            this.changeInput1Checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeInput1Checkbox.Location = new System.Drawing.Point(8, 19);
            this.changeInput1Checkbox.Name = "changeInput1Checkbox";
            this.changeInput1Checkbox.Size = new System.Drawing.Size(152, 31);
            this.changeInput1Checkbox.TabIndex = 4;
            this.changeInput1Checkbox.Text = "Input 1";
            this.changeInput1Checkbox.UseVisualStyleBackColor = true;
            this.changeInput1Checkbox.CheckedChanged += new System.EventHandler(this.changeInput1Checkbox_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1392, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionSetupToolStripMenuItem,
            this.inputLabelsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // connectionSetupToolStripMenuItem
            // 
            this.connectionSetupToolStripMenuItem.Name = "connectionSetupToolStripMenuItem";
            this.connectionSetupToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.connectionSetupToolStripMenuItem.Text = "Connection Setup";
            this.connectionSetupToolStripMenuItem.Click += new System.EventHandler(this.connectionSetupToolStripMenuItem_Click);
            // 
            // inputLabelsToolStripMenuItem
            // 
            this.inputLabelsToolStripMenuItem.Name = "inputLabelsToolStripMenuItem";
            this.inputLabelsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.inputLabelsToolStripMenuItem.Text = "Input Labels";
            this.inputLabelsToolStripMenuItem.Click += new System.EventHandler(this.inputLabelsToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Projector Remote";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_Click);
            // 
            // grpSwitcherOut
            // 
            this.grpSwitcherOut.Controls.Add(this.txtBoxStatus);
            this.grpSwitcherOut.Location = new System.Drawing.Point(186, 27);
            this.grpSwitcherOut.Name = "grpSwitcherOut";
            this.grpSwitcherOut.Size = new System.Drawing.Size(1194, 173);
            this.grpSwitcherOut.TabIndex = 9;
            this.grpSwitcherOut.TabStop = false;
            this.grpSwitcherOut.Text = "Switch output";
            // 
            // txtBoxStatus
            // 
            this.txtBoxStatus.Location = new System.Drawing.Point(6, 19);
            this.txtBoxStatus.Multiline = true;
            this.txtBoxStatus.Name = "txtBoxStatus";
            this.txtBoxStatus.ReadOnly = true;
            this.txtBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxStatus.Size = new System.Drawing.Size(1182, 142);
            this.txtBoxStatus.TabIndex = 0;
            // 
            // HDMISwitchRemote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 209);
            this.Controls.Add(this.grpSwitcherOut);
            this.Controls.Add(this.grpBoxInputs);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "HDMISwitchRemote";
            this.Text = "HDMI Switch Control";
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.grpBoxInputs.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpSwitcherOut.ResumeLayout(false);
            this.grpSwitcherOut.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.GroupBox grpBoxInputs;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputLabelsToolStripMenuItem;
        private System.Windows.Forms.CheckBox changeInput1Checkbox;
        private System.Windows.Forms.CheckBox changeInput4Checkbox;
        private System.Windows.Forms.CheckBox changeInput3Checkbox;
        private System.Windows.Forms.CheckBox changeInput2Checkbox;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.GroupBox grpSwitcherOut;
        private System.Windows.Forms.TextBox txtBoxStatus;
    }
}

