namespace AirSupply
{
    partial class RoomConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomConfigForm));
            this.configRoomsGridView = new System.Windows.Forms.DataGridView();
            this.lblConfig = new System.Windows.Forms.Label();
            this.dataPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.configRoomsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // configRoomsGridView
            // 
            this.configRoomsGridView.AllowUserToAddRows = false;
            this.configRoomsGridView.AllowUserToDeleteRows = false;
            this.configRoomsGridView.AllowUserToOrderColumns = true;
            this.configRoomsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.configRoomsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.configRoomsGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.configRoomsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.configRoomsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configRoomsGridView.Location = new System.Drawing.Point(0, 40);
            this.configRoomsGridView.MultiSelect = false;
            this.configRoomsGridView.Name = "configRoomsGridView";
            this.configRoomsGridView.RowHeadersVisible = false;
            this.configRoomsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.configRoomsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.configRoomsGridView.Size = new System.Drawing.Size(941, 560);
            this.configRoomsGridView.TabIndex = 0;
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfig.Location = new System.Drawing.Point(7, 13);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(142, 18);
            this.lblConfig.TabIndex = 1;
            this.lblConfig.Text = "Room Configuration";
            // 
            // dataPanel
            // 
            this.dataPanel.AutoSize = true;
            this.dataPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataPanel.Location = new System.Drawing.Point(0, 0);
            this.dataPanel.MinimumSize = new System.Drawing.Size(0, 40);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(941, 40);
            this.dataPanel.TabIndex = 2;
            // 
            // RoomConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 600);
            this.Controls.Add(this.configRoomsGridView);
            this.Controls.Add(this.lblConfig);
            this.Controls.Add(this.dataPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "RoomConfigForm";
            this.Text = "Configure";
            ((System.ComponentModel.ISupportInitialize)(this.configRoomsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView configRoomsGridView;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.Panel dataPanel;
    }
}