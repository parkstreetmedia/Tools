using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace AirSupply
{
    public partial class TemperatureConfigForm : Form
    {
        private TemperatureDefaults TempDefaults;
       
        public TemperatureConfigForm() {
            InitializeComponent();

            //Get stored values
            this.TempDefaults = AWSCommunication.GetTemperatureDefaults();           

            this.defaultCoolOccupiedTo.SelectedItem = this.TempDefaults.CoolOccupiedRoomsTo;
            this.defaultCoolUnOccupiedTo.SelectedItem = this.TempDefaults.CoolUnoccupiedRoomsTo;
            this.defaultHeatOccupiedTo.SelectedItem = this.TempDefaults.HeatOccupiedRoomsTo;
            this.defaultHeatUnOccupiedTo.SelectedItem = this.TempDefaults.HeatUnoccupiedRoomsTo;

            this.dailyTempOptionComboBox.SelectedItem = this.TempDefaults.ForecastTempToUse;

            this.latTxtBox.Text = this.TempDefaults.LocationLat;
            this.longTxtBox.Text = this.TempDefaults.LocationLong;

            this.switchToHeatAtCombo.SelectedItem = this.TempDefaults.TempToSwitchToHeat.ToString();
            this.switchToCoolAtCombo.SelectedItem = this.TempDefaults.TempToSwitchToCool.ToString();

            if (this.latTxtBox.Text == "") {
                this.latTxtBox.Text = "42.35";
                this.TempDefaults.LocationLat = this.latTxtBox.Text;
            }
            if (this.longTxtBox.Text == "") {
                this.longTxtBox.Text = "-71.06";
                this.TempDefaults.LocationLong = this.longTxtBox.Text;
            }

            this.dailyTempOptionComboBox.SelectedValue = this.TempDefaults.ForecastTempToUse;

            if(this.dailyTempOptionComboBox.SelectedIndex < 0) {
                this.dailyTempOptionComboBox.SelectedIndex = 0;
            }

            if (this.TempDefaults.GetForecast()) {
                this.currentLowLbl.Text = "Current Low Forecast: " + this.TempDefaults.ForecastMin;
                this.currentHighLbl.Text = "Current High Forecast: " + this.TempDefaults.ForecastMax;
                this.currentAvgLbl.Text = "Current Average Forecast: " + this.TempDefaults.ForecastAvg;
            }
            else {
                MessageBox.Show("Could not get the weather... this will be a problem");
            }           

        }

        private void TemperatureConfigForm_FormClosing(object sender, FormClosingEventArgs e) {
            //verify the lat/long works
            double alat;
            double along;
            Double.TryParse(this.latTxtBox.Text.Trim(), out alat);
            Double.TryParse(this.longTxtBox.Text.Trim(), out along);
            alat = Math.Round(alat, 2);
            along = Math.Round(along, 2);
            if (alat == 0 || along == 0) {
                MessageBox.Show("Your Lat/Long values are invalid! They need to be in the form ##.##", "Input Error");
                e.Cancel = true;
                return;
            }
            this.latTxtBox.Text = alat.ToString();
            this.longTxtBox.Text = along.ToString();

            //save changes
            this.TempDefaults.CoolOccupiedRoomsTo = this.defaultCoolOccupiedTo.SelectedItem.ToString();
            this.TempDefaults.CoolUnoccupiedRoomsTo = this.defaultCoolUnOccupiedTo.SelectedItem.ToString();
            this.TempDefaults.HeatOccupiedRoomsTo = this.defaultHeatOccupiedTo.SelectedItem.ToString();
            this.TempDefaults.HeatUnoccupiedRoomsTo = this.defaultHeatUnOccupiedTo.SelectedItem.ToString();

            this.TempDefaults.ForecastTempToUse = this.dailyTempOptionComboBox.SelectedItem.ToString();

            this.TempDefaults.LocationLat = this.latTxtBox.Text.Trim();
            this.TempDefaults.LocationLong = this.longTxtBox.Text.Trim();

            this.TempDefaults.TempToSwitchToHeat = Int32.Parse(this.switchToHeatAtCombo.SelectedItem.ToString());
            this.TempDefaults.TempToSwitchToCool = Int32.Parse(this.switchToCoolAtCombo.SelectedItem.ToString());

            try {
                //write to AWS 
                AWSCommunication.SaveTemperatureDefaults(this.TempDefaults);
            }
            catch (Exception ex) {
                MessageBox.Show("Could not save your changes! They are being lost...sorry!\nError: " + ex.Message);
            }

            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
