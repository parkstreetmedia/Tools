using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC2Control {
    public partial class Settings : Form {
        public Settings() {
            InitializeComponent();

            var amis = new BindingList<KeyValuePair<string, string>>();

            amis.Add(new KeyValuePair<string, string>("Asia Pacific (Mumbai)", "ami-ccc9baa3"));
            amis.Add(new KeyValuePair<string, string>("EU (London)", "ami-61c5d105"));
            amis.Add(new KeyValuePair<string, string>("EU (Ireland)", "ami-34f1f552"));
            amis.Add(new KeyValuePair<string, string>("Asia Pacific (Seoul)", "ami-811fcdef"));
            amis.Add(new KeyValuePair<string, string>("Asia Pacific (Tokyo)", "ami-a6a689c1"));
            amis.Add(new KeyValuePair<string, string>("South America (Sao Paulo)", "ami-6ddbb901"));
            amis.Add(new KeyValuePair<string, string>("Canada (Central)", "ami-82d569e6"));
            amis.Add(new KeyValuePair<string, string>("Asia Pacific (Singapore)", "ami-de3a83bd"));
            amis.Add(new KeyValuePair<string, string>("Asia Pacific (Sydney)", "ami-8a343de9"));
            amis.Add(new KeyValuePair<string, string>("EU (Frankfurt)", "ami-c38d50ac"));
            amis.Add(new KeyValuePair<string, string>("US East (N. Virginia)", "ami-4cd0405a"));
            amis.Add(new KeyValuePair<string, string>("US East (Ohio)", "ami-9c9fbbf9"));
            amis.Add(new KeyValuePair<string, string>("US West (N. California)", "ami-6695b006"));
            amis.Add(new KeyValuePair<string, string>("US West (Oregon)", "ami-18fd6078"));

            this.comboBoxAMI.DataSource = amis;           
            this.comboBoxAMI.ValueMember = "Value";
            this.comboBoxAMI.DisplayMember = "Key";

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.txtBoxAWSProfileName.Text = configuration.AppSettings.Settings["AWSProfileName"].Value;
            this.txtBoxAWSAccessKey.Text = configuration.AppSettings.Settings["AWSAccessKey"].Value;
            this.txtBoxAWSSecretKey.Text = configuration.AppSettings.Settings["AWSSecretKey"].Value;
            this.comboBoxAMI.SelectedValue = configuration.AppSettings.Settings["AMI"].Value;
            this.txtBoxIPAddress.Text = configuration.AppSettings.Settings["IPAddress"].Value;
            this.txtBoxSecurityGroup.Text = configuration.AppSettings.Settings["SecurityGroup"].Value;
            this.chkBoxShouldUseStartupPackage.Checked = bool.Parse(configuration.AppSettings.Settings["ShouldUseStartupPackage"].Value);
            this.txtBoxURLToStartupPackage.Text = configuration.AppSettings.Settings["URLToStartupPackage"].Value;
            this.cmbBoxInstanceType.Text = configuration.AppSettings.Settings["InstanceType"].Value;
            this.cmbBoxHours.Text = configuration.AppSettings.Settings["WatchHours"].Value;
            this.chkBoxShouldWeMonitor.Checked = bool.Parse(configuration.AppSettings.Settings["ShouldWeMonitor"].Value);
        }

        private void btnSave_Click(object sender, EventArgs e) {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["AWSProfileName"].Value = this.txtBoxAWSProfileName.Text.Trim();
            configuration.AppSettings.Settings["AWSAccessKey"].Value = this.txtBoxAWSAccessKey.Text.Trim();
            configuration.AppSettings.Settings["AWSSecretKey"].Value = this.txtBoxAWSSecretKey.Text.Trim();
            configuration.AppSettings.Settings["AMI"].Value = this.comboBoxAMI.SelectedValue.ToString().Trim();
            configuration.AppSettings.Settings["IPAddress"].Value = this.txtBoxIPAddress.Text.Trim();
            configuration.AppSettings.Settings["InstanceType"].Value = this.cmbBoxInstanceType.SelectedItem.ToString();
            configuration.AppSettings.Settings["SecurityGroup"].Value = this.txtBoxSecurityGroup.Text.Trim();
            configuration.AppSettings.Settings["ShouldUseStartupPackage"].Value = this.chkBoxShouldUseStartupPackage.Checked.ToString();
            configuration.AppSettings.Settings["URLToStartupPackage"].Value = this.txtBoxURLToStartupPackage.Text.Trim();
            configuration.AppSettings.Settings["WatchHours"].Value = this.cmbBoxHours.SelectedItem.ToString();
            configuration.AppSettings.Settings["ShouldWeMonitor"].Value = this.chkBoxShouldWeMonitor.Checked.ToString();
            configuration.Save(ConfigurationSaveMode.Modified);          
            this.Close();
        }
    }
}
