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

namespace RRHDMIControl
{
    public partial class InputLabels : Form
    {
        public InputLabels()
        {
            InitializeComponent();
            if (((string)Settings.Default["Input1Label"]) != string.Empty) {
                this.input1TxtBox.Text = Settings.Default["Input1Label"].ToString();
            }

            if (((string)Settings.Default["Input2Label"]) != string.Empty) {
                this.input2TxtBox.Text = Settings.Default["Input2Label"].ToString();
            }

            if (((string)Settings.Default["Input3Label"]) != string.Empty) {
                this.input3TxtBox.Text = Settings.Default["Input3Label"].ToString();
            }

            if (((string)Settings.Default["Input4Label"]) != string.Empty) {
                this.input4TxtBox.Text = Settings.Default["Input4Label"].ToString();
            }       
        }

        private void InputLabels_FormClosing(Object sender, FormClosingEventArgs e) {
            Settings.Default["Input1Label"] = this.input1TxtBox.Text.Trim();
            Settings.Default["Input2Label"] = this.input2TxtBox.Text.Trim();
            Settings.Default["Input3Label"] = this.input3TxtBox.Text.Trim();
            Settings.Default["Input4Label"] = this.input4TxtBox.Text.Trim();
            Settings.Default.Save();
        }
    }
}
