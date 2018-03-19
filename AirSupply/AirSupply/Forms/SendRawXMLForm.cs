using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirSupply
{
    public partial class SendRawXMLForm : Form
    {
        public SendRawXMLForm() {
            InitializeComponent();
        }

        private void sendCommandButton_Click(object sender, EventArgs e) {
            string command = this.inputTxtBox.Text;
            if (!string.IsNullOrEmpty(command)) {
                command = command.Trim();
                string reponse = C50Communication.SendRequestAndWaitReply(command);
                this.outputTxtBox.Text = reponse;
            }
        }
    }
}
