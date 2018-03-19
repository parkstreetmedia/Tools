namespace EC2Control {
    partial class ControlGUI {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlGUI));
            this.theMenuStrip = new System.Windows.Forms.MenuStrip();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openServerAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverDG = new System.Windows.Forms.DataGridView();
            this.InstanceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LaunchTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrivateIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PublicIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AMI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnServerStart = new System.Windows.Forms.Button();
            this.btnServerStopAll = new System.Windows.Forms.Button();
            this.btnRefreshStatus = new System.Windows.Forms.Button();
            this.statusStripLbl = new System.Windows.Forms.Label();
            this.lblServers = new System.Windows.Forms.Label();
            this.lblNoServers = new System.Windows.Forms.Label();
            this.theMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverDG)).BeginInit();
            this.SuspendLayout();
            // 
            // theMenuStrip
            // 
            this.theMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem});
            this.theMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.theMenuStrip.Name = "theMenuStrip";
            this.theMenuStrip.Size = new System.Drawing.Size(892, 24);
            this.theMenuStrip.TabIndex = 0;
            this.theMenuStrip.Text = "menuStrip1";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.openServerAdminToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.configureToolStripMenuItem.Text = "Options";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // openServerAdminToolStripMenuItem
            // 
            this.openServerAdminToolStripMenuItem.Name = "openServerAdminToolStripMenuItem";
            this.openServerAdminToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.openServerAdminToolStripMenuItem.Text = "Open Server Admin";
            this.openServerAdminToolStripMenuItem.Click += new System.EventHandler(this.openServerAdminToolStripMenuItem_Click);
            // 
            // serverDG
            // 
            this.serverDG.AllowUserToAddRows = false;
            this.serverDG.AllowUserToDeleteRows = false;
            this.serverDG.AllowUserToResizeRows = false;
            this.serverDG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverDG.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.serverDG.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.serverDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serverDG.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InstanceID,
            this.LaunchTime,
            this.Status,
            this.PrivateIP,
            this.PublicIP,
            this.AMI,
            this.Type,
            this.Location});
            this.serverDG.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.serverDG.Location = new System.Drawing.Point(7, 47);
            this.serverDG.MaximumSize = new System.Drawing.Size(1900, 895);
            this.serverDG.MinimumSize = new System.Drawing.Size(615, 115);
            this.serverDG.MultiSelect = false;
            this.serverDG.Name = "serverDG";
            this.serverDG.ReadOnly = true;
            this.serverDG.ShowEditingIcon = false;
            this.serverDG.Size = new System.Drawing.Size(720, 115);
            this.serverDG.TabIndex = 0;
            // 
            // InstanceID
            // 
            this.InstanceID.HeaderText = "InstanceID";
            this.InstanceID.Name = "InstanceID";
            this.InstanceID.ReadOnly = true;
            this.InstanceID.Width = 84;
            // 
            // LaunchTime
            // 
            this.LaunchTime.HeaderText = "Launch Time";
            this.LaunchTime.Name = "LaunchTime";
            this.LaunchTime.ReadOnly = true;
            this.LaunchTime.Width = 94;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 62;
            // 
            // PrivateIP
            // 
            this.PrivateIP.HeaderText = "Private IP";
            this.PrivateIP.Name = "PrivateIP";
            this.PrivateIP.ReadOnly = true;
            this.PrivateIP.Width = 78;
            // 
            // PublicIP
            // 
            this.PublicIP.HeaderText = "Public IP";
            this.PublicIP.Name = "PublicIP";
            this.PublicIP.ReadOnly = true;
            this.PublicIP.Width = 74;
            // 
            // AMI
            // 
            this.AMI.HeaderText = "AMI";
            this.AMI.Name = "AMI";
            this.AMI.ReadOnly = true;
            this.AMI.Width = 51;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 56;
            // 
            // Location
            // 
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 73;
            // 
            // btnServerStart
            // 
            this.btnServerStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServerStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnServerStart.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btnServerStart.Location = new System.Drawing.Point(746, 47);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(134, 23);
            this.btnServerStart.TabIndex = 2;
            this.btnServerStart.Text = "Start Server";
            this.btnServerStart.UseVisualStyleBackColor = false;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // btnServerStopAll
            // 
            this.btnServerStopAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServerStopAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnServerStopAll.BackColor = System.Drawing.Color.LightCoral;
            this.btnServerStopAll.Location = new System.Drawing.Point(746, 130);
            this.btnServerStopAll.Name = "btnServerStopAll";
            this.btnServerStopAll.Size = new System.Drawing.Size(134, 23);
            this.btnServerStopAll.TabIndex = 1;
            this.btnServerStopAll.Text = "Stop Server";
            this.btnServerStopAll.UseVisualStyleBackColor = false;
            this.btnServerStopAll.Click += new System.EventHandler(this.btnServerStopAll_Click);
            // 
            // btnRefreshStatus
            // 
            this.btnRefreshStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefreshStatus.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnRefreshStatus.Location = new System.Drawing.Point(746, 90);
            this.btnRefreshStatus.Name = "btnRefreshStatus";
            this.btnRefreshStatus.Size = new System.Drawing.Size(134, 23);
            this.btnRefreshStatus.TabIndex = 0;
            this.btnRefreshStatus.Text = "Refresh Status";
            this.btnRefreshStatus.UseVisualStyleBackColor = false;
            this.btnRefreshStatus.Click += new System.EventHandler(this.btnRefreshStatus_Click);
            // 
            // statusStripLbl
            // 
            this.statusStripLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusStripLbl.AutoSize = true;
            this.statusStripLbl.Location = new System.Drawing.Point(4, 172);
            this.statusStripLbl.Name = "statusStripLbl";
            this.statusStripLbl.Size = new System.Drawing.Size(59, 13);
            this.statusStripLbl.TabIndex = 2;
            this.statusStripLbl.Text = "Updating...";
            // 
            // lblServers
            // 
            this.lblServers.AutoSize = true;
            this.lblServers.Location = new System.Drawing.Point(6, 31);
            this.lblServers.Name = "lblServers";
            this.lblServers.Size = new System.Drawing.Size(49, 13);
            this.lblServers.TabIndex = 3;
            this.lblServers.Text = "Server(s)";
            // 
            // lblNoServers
            // 
            this.lblNoServers.AutoSize = true;
            this.lblNoServers.BackColor = System.Drawing.Color.Red;
            this.lblNoServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoServers.Location = new System.Drawing.Point(64, 70);
            this.lblNoServers.Name = "lblNoServers";
            this.lblNoServers.Size = new System.Drawing.Size(506, 24);
            this.lblNoServers.TabIndex = 4;
            this.lblNoServers.Text = "There are currently no servers running, starting, or stopping";
            this.lblNoServers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoServers.Visible = false;
            // 
            // ControlGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(892, 188);
            this.Controls.Add(this.lblNoServers);
            this.Controls.Add(this.btnServerStopAll);
            this.Controls.Add(this.btnServerStart);
            this.Controls.Add(this.lblServers);
            this.Controls.Add(this.serverDG);
            this.Controls.Add(this.btnRefreshStatus);
            this.Controls.Add(this.statusStripLbl);
            this.Controls.Add(this.theMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.theMenuStrip;
            this.MinimumSize = new System.Drawing.Size(795, 215);
            this.Name = "ControlGUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "AWS Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUIClosing);
            this.theMenuStrip.ResumeLayout(false);
            this.theMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverDG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip theMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.DataGridView serverDG;
        private System.Windows.Forms.Button btnServerStart;
        private System.Windows.Forms.Button btnServerStopAll;
        private System.Windows.Forms.Button btnRefreshStatus;
        private System.Windows.Forms.Label statusStripLbl;
        private System.Windows.Forms.Label lblServers;
        private System.Windows.Forms.Label lblNoServers;
        private System.Windows.Forms.ToolStripMenuItem openServerAdminToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstanceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LaunchTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrivateIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn PublicIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn AMI;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
    }
}