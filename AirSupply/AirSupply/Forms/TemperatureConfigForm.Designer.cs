namespace AirSupply
{
    partial class TemperatureConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemperatureConfigForm));
            this.latTxtBox = new System.Windows.Forms.TextBox();
            this.lblConfig = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.weatherGrpBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.switchToCoolAtCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.switchToHeatAtCombo = new System.Windows.Forms.ComboBox();
            this.longTxtBox = new System.Windows.Forms.TextBox();
            this.lblCalculateUse = new System.Windows.Forms.Label();
            this.currentAvgLbl = new System.Windows.Forms.Label();
            this.currentLowLbl = new System.Windows.Forms.Label();
            this.currentHighLbl = new System.Windows.Forms.Label();
            this.currentWeatherLbl = new System.Windows.Forms.Label();
            this.dailyTempOptionComboBox = new System.Windows.Forms.ComboBox();
            this.lblCalculateTemp = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.defaultCoolUnOccupiedTo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.defaultCoolOccupiedTo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.defaultHeatUnOccupiedTo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTemp2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.defaultHeatOccupiedTo = new System.Windows.Forms.ComboBox();
            this.lblTemp1 = new System.Windows.Forms.Label();
            this.weatherGrpBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // latTxtBox
            // 
            this.latTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.latTxtBox.Location = new System.Drawing.Point(225, 28);
            this.latTxtBox.Name = "latTxtBox";
            this.latTxtBox.Size = new System.Drawing.Size(79, 26);
            this.latTxtBox.TabIndex = 0;
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfig.Location = new System.Drawing.Point(12, 9);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(199, 20);
            this.lblConfig.TabIndex = 1;
            this.lblConfig.Text = "Temperature Configuration";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lat Long for Weather Data:";
            // 
            // weatherGrpBox
            // 
            this.weatherGrpBox.Controls.Add(this.label5);
            this.weatherGrpBox.Controls.Add(this.label4);
            this.weatherGrpBox.Controls.Add(this.switchToCoolAtCombo);
            this.weatherGrpBox.Controls.Add(this.label3);
            this.weatherGrpBox.Controls.Add(this.label8);
            this.weatherGrpBox.Controls.Add(this.switchToHeatAtCombo);
            this.weatherGrpBox.Controls.Add(this.longTxtBox);
            this.weatherGrpBox.Controls.Add(this.lblCalculateUse);
            this.weatherGrpBox.Controls.Add(this.currentAvgLbl);
            this.weatherGrpBox.Controls.Add(this.currentLowLbl);
            this.weatherGrpBox.Controls.Add(this.currentHighLbl);
            this.weatherGrpBox.Controls.Add(this.currentWeatherLbl);
            this.weatherGrpBox.Controls.Add(this.dailyTempOptionComboBox);
            this.weatherGrpBox.Controls.Add(this.lblCalculateTemp);
            this.weatherGrpBox.Controls.Add(this.label1);
            this.weatherGrpBox.Controls.Add(this.latTxtBox);
            this.weatherGrpBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weatherGrpBox.Location = new System.Drawing.Point(16, 47);
            this.weatherGrpBox.Name = "weatherGrpBox";
            this.weatherGrpBox.Size = new System.Drawing.Size(711, 325);
            this.weatherGrpBox.TabIndex = 3;
            this.weatherGrpBox.TabStop = false;
            this.weatherGrpBox.Text = "Weather";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(217, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "Degrees";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Switch to Cool at";
            // 
            // switchToCoolAtCombo
            // 
            this.switchToCoolAtCombo.FormattingEnabled = true;
            this.switchToCoolAtCombo.Items.AddRange(new object[] {
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100"});
            this.switchToCoolAtCombo.Location = new System.Drawing.Point(154, 275);
            this.switchToCoolAtCombo.Name = "switchToCoolAtCombo";
            this.switchToCoolAtCombo.Size = new System.Drawing.Size(58, 28);
            this.switchToCoolAtCombo.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Switch to Heat at";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(217, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Degrees";
            // 
            // switchToHeatAtCombo
            // 
            this.switchToHeatAtCombo.FormattingEnabled = true;
            this.switchToHeatAtCombo.Items.AddRange(new object[] {
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80"});
            this.switchToHeatAtCombo.Location = new System.Drawing.Point(154, 241);
            this.switchToHeatAtCombo.Name = "switchToHeatAtCombo";
            this.switchToHeatAtCombo.Size = new System.Drawing.Size(58, 28);
            this.switchToHeatAtCombo.TabIndex = 19;
            // 
            // longTxtBox
            // 
            this.longTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.longTxtBox.Location = new System.Drawing.Point(328, 28);
            this.longTxtBox.Name = "longTxtBox";
            this.longTxtBox.Size = new System.Drawing.Size(79, 26);
            this.longTxtBox.TabIndex = 10;
            // 
            // lblCalculateUse
            // 
            this.lblCalculateUse.AutoSize = true;
            this.lblCalculateUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalculateUse.Location = new System.Drawing.Point(438, 184);
            this.lblCalculateUse.Name = "lblCalculateUse";
            this.lblCalculateUse.Size = new System.Drawing.Size(259, 20);
            this.lblCalculateUse.TabIndex = 9;
            this.lblCalculateUse.Text = "to Decide to Switch Auto Heat/Cool";
            // 
            // currentAvgLbl
            // 
            this.currentAvgLbl.AutoSize = true;
            this.currentAvgLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentAvgLbl.Location = new System.Drawing.Point(47, 137);
            this.currentAvgLbl.Name = "currentAvgLbl";
            this.currentAvgLbl.Size = new System.Drawing.Size(200, 20);
            this.currentAvgLbl.TabIndex = 8;
            this.currentAvgLbl.Text = "Current Average Forecast: ";
            // 
            // currentLowLbl
            // 
            this.currentLowLbl.AutoSize = true;
            this.currentLowLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentLowLbl.Location = new System.Drawing.Point(47, 107);
            this.currentLowLbl.Name = "currentLowLbl";
            this.currentLowLbl.Size = new System.Drawing.Size(170, 20);
            this.currentLowLbl.TabIndex = 7;
            this.currentLowLbl.Text = "Current Low Forecast: ";
            // 
            // currentHighLbl
            // 
            this.currentHighLbl.AutoSize = true;
            this.currentHighLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentHighLbl.Location = new System.Drawing.Point(47, 74);
            this.currentHighLbl.Name = "currentHighLbl";
            this.currentHighLbl.Size = new System.Drawing.Size(174, 20);
            this.currentHighLbl.TabIndex = 6;
            this.currentHighLbl.Text = "Current High Forecast: ";
            // 
            // currentWeatherLbl
            // 
            this.currentWeatherLbl.AutoSize = true;
            this.currentWeatherLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentWeatherLbl.Location = new System.Drawing.Point(15, 137);
            this.currentWeatherLbl.Name = "currentWeatherLbl";
            this.currentWeatherLbl.Size = new System.Drawing.Size(0, 20);
            this.currentWeatherLbl.TabIndex = 5;
            // 
            // dailyTempOptionComboBox
            // 
            this.dailyTempOptionComboBox.FormattingEnabled = true;
            this.dailyTempOptionComboBox.Items.AddRange(new object[] {
            "Average of Daily High and Daily Low Forecast",
            "Daily High Forecast",
            "Daily Low Forecast"});
            this.dailyTempOptionComboBox.Location = new System.Drawing.Point(63, 182);
            this.dailyTempOptionComboBox.Name = "dailyTempOptionComboBox";
            this.dailyTempOptionComboBox.Size = new System.Drawing.Size(369, 28);
            this.dailyTempOptionComboBox.TabIndex = 4;
            // 
            // lblCalculateTemp
            // 
            this.lblCalculateTemp.AutoSize = true;
            this.lblCalculateTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalculateTemp.Location = new System.Drawing.Point(15, 185);
            this.lblCalculateTemp.Name = "lblCalculateTemp";
            this.lblCalculateTemp.Size = new System.Drawing.Size(42, 20);
            this.lblCalculateTemp.TabIndex = 3;
            this.lblCalculateTemp.Text = "Use ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.defaultCoolUnOccupiedTo);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.defaultCoolOccupiedTo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.defaultHeatUnOccupiedTo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblTemp2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.defaultHeatOccupiedTo);
            this.groupBox1.Controls.Add(this.lblTemp1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 394);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(711, 197);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Temperature Defaults";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(292, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "Degrees";
            // 
            // defaultCoolUnOccupiedTo
            // 
            this.defaultCoolUnOccupiedTo.FormattingEnabled = true;
            this.defaultCoolUnOccupiedTo.Items.AddRange(new object[] {
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82"});
            this.defaultCoolUnOccupiedTo.Location = new System.Drawing.Point(229, 160);
            this.defaultCoolUnOccupiedTo.Name = "defaultCoolUnOccupiedTo";
            this.defaultCoolUnOccupiedTo.Size = new System.Drawing.Size(58, 28);
            this.defaultCoolUnOccupiedTo.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(15, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(207, 20);
            this.label10.TabIndex = 16;
            this.label10.Text = "Cool Unoccupied Rooms to ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(292, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 20);
            this.label11.TabIndex = 15;
            this.label11.Text = "Degrees";
            // 
            // defaultCoolOccupiedTo
            // 
            this.defaultCoolOccupiedTo.FormattingEnabled = true;
            this.defaultCoolOccupiedTo.Items.AddRange(new object[] {
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82"});
            this.defaultCoolOccupiedTo.Location = new System.Drawing.Point(229, 126);
            this.defaultCoolOccupiedTo.Name = "defaultCoolOccupiedTo";
            this.defaultCoolOccupiedTo.Size = new System.Drawing.Size(58, 28);
            this.defaultCoolOccupiedTo.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(15, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(189, 20);
            this.label12.TabIndex = 13;
            this.label12.Text = "Cool Occupied Rooms to ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(292, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Degrees";
            // 
            // defaultHeatUnOccupiedTo
            // 
            this.defaultHeatUnOccupiedTo.FormattingEnabled = true;
            this.defaultHeatUnOccupiedTo.Items.AddRange(new object[] {
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82"});
            this.defaultHeatUnOccupiedTo.Location = new System.Drawing.Point(229, 72);
            this.defaultHeatUnOccupiedTo.Name = "defaultHeatUnOccupiedTo";
            this.defaultHeatUnOccupiedTo.Size = new System.Drawing.Size(58, 28);
            this.defaultHeatUnOccupiedTo.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Heat Unoccupied Rooms to ";
            // 
            // lblTemp2
            // 
            this.lblTemp2.AutoSize = true;
            this.lblTemp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp2.Location = new System.Drawing.Point(292, 41);
            this.lblTemp2.Name = "lblTemp2";
            this.lblTemp2.Size = new System.Drawing.Size(70, 20);
            this.lblTemp2.TabIndex = 9;
            this.lblTemp2.Text = "Degrees";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 20);
            this.label6.TabIndex = 5;
            // 
            // defaultHeatOccupiedTo
            // 
            this.defaultHeatOccupiedTo.FormattingEnabled = true;
            this.defaultHeatOccupiedTo.Items.AddRange(new object[] {
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82"});
            this.defaultHeatOccupiedTo.Location = new System.Drawing.Point(229, 38);
            this.defaultHeatOccupiedTo.Name = "defaultHeatOccupiedTo";
            this.defaultHeatOccupiedTo.Size = new System.Drawing.Size(58, 28);
            this.defaultHeatOccupiedTo.TabIndex = 4;
            // 
            // lblTemp1
            // 
            this.lblTemp1.AutoSize = true;
            this.lblTemp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp1.Location = new System.Drawing.Point(15, 41);
            this.lblTemp1.Name = "lblTemp1";
            this.lblTemp1.Size = new System.Drawing.Size(192, 20);
            this.lblTemp1.TabIndex = 3;
            this.lblTemp1.Text = "Heat Occupied Rooms to ";
            // 
            // TemperatureConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 621);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.weatherGrpBox);
            this.Controls.Add(this.lblConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemperatureConfigForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Configure";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TemperatureConfigForm_FormClosing);
            this.weatherGrpBox.ResumeLayout(false);
            this.weatherGrpBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox latTxtBox;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox weatherGrpBox;
        private System.Windows.Forms.ComboBox dailyTempOptionComboBox;
        private System.Windows.Forms.Label lblCalculateTemp;
        private System.Windows.Forms.Label lblCalculateUse;
        private System.Windows.Forms.Label currentAvgLbl;
        private System.Windows.Forms.Label currentLowLbl;
        private System.Windows.Forms.Label currentHighLbl;
        private System.Windows.Forms.Label currentWeatherLbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTemp2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox defaultHeatOccupiedTo;
        private System.Windows.Forms.Label lblTemp1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox defaultCoolUnOccupiedTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox defaultCoolOccupiedTo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox defaultHeatUnOccupiedTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox longTxtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox switchToCoolAtCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox switchToHeatAtCombo;
    }
}