namespace AirSupply
{
    partial class AirSupplyForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AirSupplyForm));
            this.lastUpdateLbl = new System.Windows.Forms.Label();
            this.mainDataGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomToUnitsAssignmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temperatureSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendRawXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.dataPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lastUpdateLbl
            // 
            this.lastUpdateLbl.AutoSize = true;
            this.lastUpdateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastUpdateLbl.Location = new System.Drawing.Point(11, 8);
            this.lastUpdateLbl.Name = "lastUpdateLbl";
            this.lastUpdateLbl.Size = new System.Drawing.Size(101, 20);
            this.lastUpdateLbl.TabIndex = 4;
            this.lastUpdateLbl.Text = "Last Update:";
            // 
            // mainDataGrid
            // 
            this.mainDataGrid.AllowUserToAddRows = false;
            this.mainDataGrid.AllowUserToDeleteRows = false;
            this.mainDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.mainDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.mainDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.mainDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.mainDataGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.mainDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.mainDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDataGrid.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mainDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.mainDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDataGrid.GridColor = System.Drawing.Color.Azure;
            this.mainDataGrid.Location = new System.Drawing.Point(0, 59);
            this.mainDataGrid.MultiSelect = false;
            this.mainDataGrid.Name = "mainDataGrid";
            this.mainDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.mainDataGrid.RowHeadersVisible = false;
            this.mainDataGrid.RowTemplate.Height = 30;
            this.mainDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mainDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainDataGrid.ShowCellToolTips = false;
            this.mainDataGrid.Size = new System.Drawing.Size(1164, 682);
            this.mainDataGrid.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1164, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roomToUnitsAssignmentsToolStripMenuItem,
            this.temperatureSettingsToolStripMenuItem,
            this.documentationToolStripMenuItem,
            this.viewLogsToolStripMenuItem,
            this.sendRawXMLToolStripMenuItem,
            this.enableLoggingToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // roomToUnitsAssignmentsToolStripMenuItem
            // 
            this.roomToUnitsAssignmentsToolStripMenuItem.Name = "roomToUnitsAssignmentsToolStripMenuItem";
            this.roomToUnitsAssignmentsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.roomToUnitsAssignmentsToolStripMenuItem.Text = "Room Settings";
            this.roomToUnitsAssignmentsToolStripMenuItem.Click += new System.EventHandler(this.roomToUnitsAssignmentsToolStripMenuItem_Click);
            // 
            // temperatureSettingsToolStripMenuItem
            // 
            this.temperatureSettingsToolStripMenuItem.Name = "temperatureSettingsToolStripMenuItem";
            this.temperatureSettingsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.temperatureSettingsToolStripMenuItem.Text = "Temperature Settings";
            this.temperatureSettingsToolStripMenuItem.Click += new System.EventHandler(this.temperatureSettingsToolStripMenuItem_Click);
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.documentationToolStripMenuItem.Text = "Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.documentationToolStripMenuItem_Click);
            // 
            // viewLogsToolStripMenuItem
            // 
            this.viewLogsToolStripMenuItem.Name = "viewLogsToolStripMenuItem";
            this.viewLogsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.viewLogsToolStripMenuItem.Text = "View EMS Control Logs";
            this.viewLogsToolStripMenuItem.Click += new System.EventHandler(this.viewLogsToolStripMenuItem_Click);
            // 
            // sendRawXMLToolStripMenuItem
            // 
            this.sendRawXMLToolStripMenuItem.Name = "sendRawXMLToolStripMenuItem";
            this.sendRawXMLToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.sendRawXMLToolStripMenuItem.Text = "Send Raw XML";
            this.sendRawXMLToolStripMenuItem.Click += new System.EventHandler(this.sendRawXMLToolStripMenuItem_Click);
            // 
            // enableLoggingToolStripMenuItem
            // 
            this.enableLoggingToolStripMenuItem.CheckOnClick = true;
            this.enableLoggingToolStripMenuItem.Name = "enableLoggingToolStripMenuItem";
            this.enableLoggingToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.enableLoggingToolStripMenuItem.Text = "Enable Logging";
            this.enableLoggingToolStripMenuItem.Click += new System.EventHandler(this.enableLoggingToolStripMenuItem_Click);
            // 
            // dataPanel
            // 
            this.dataPanel.AutoScroll = true;
            this.dataPanel.AutoSize = true;
            this.dataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataPanel.Controls.Add(this.lastUpdateLbl);
            this.dataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataPanel.Location = new System.Drawing.Point(0, 24);
            this.dataPanel.Margin = new System.Windows.Forms.Padding(10, 20, 30, 30);
            this.dataPanel.MinimumSize = new System.Drawing.Size(300, 35);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(1164, 35);
            this.dataPanel.TabIndex = 6;
            // 
            // AirSupplyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 741);
            this.Controls.Add(this.mainDataGrid);
            this.Controls.Add(this.dataPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AirSupplyForm";
            this.Text = "AirSupply";
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.dataPanel.ResumeLayout(false);
            this.dataPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lastUpdateLbl;
        private System.Windows.Forms.DataGridView mainDataGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roomToUnitsAssignmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendRawXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableLoggingToolStripMenuItem;
        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.ToolStripMenuItem temperatureSettingsToolStripMenuItem;
    }
}

