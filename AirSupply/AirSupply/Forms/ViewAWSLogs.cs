using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AirSupply
{
    public partial class ViewAWSLogs : Form
    {
        public ViewAWSLogs() {
            InitializeComponent();
            //Load keys
            this.credsIAM.Text = XMLConfigData.AWSDynamoCredsIAM;
            this.credsUser.Text = XMLConfigData.AWSDynamoCredsUsername;
            this.credsPass.Text = XMLConfigData.AWSDynamoCredsPassword;

        }

        private void copyToClipIAM_Click(object sender, EventArgs e) {
            Clipboard.SetText(this.credsIAM.Text.Trim());
        }

        private void copyToClipUser_Click(object sender, EventArgs e) {
            Clipboard.SetText(this.credsUser.Text.Trim());
        }

        private void copyToClipPass_Click(object sender, EventArgs e) {
            Clipboard.SetText(this.credsPass.Text.Trim());
        }

        private void goButton_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("https://console.aws.amazon.com/dynamodb/home?region=us-east-1#tables:selected=LastRun");
        }
    }
}
