using RRHDMIControl.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHDMIControl {
    public partial class ConnectionSettings : Form {
        public ConnectionSettings() {
            InitializeComponent();
            //Load Settings
            bool isSerial = true;
            this.serialCommPortCombo.SelectedIndex = 0;
            try {                
                Boolean.TryParse(Settings.Default["UseSerial"].ToString(), out isSerial);
                this.serialCommPortCombo.SelectedText = (string)Settings.Default["SerialPort"];
            } catch (Exception) {//no settings.. ah well 
            }
        }

        private void ConnectionSettings_FormClosing(Object sender, FormClosingEventArgs e) {        
            Settings.Default["SerialPort"] = this.serialCommPortCombo.SelectedText.Trim();
            Settings.Default.Save();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
