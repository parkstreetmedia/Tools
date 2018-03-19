using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2Control {
    public partial class ControlGUI : Form {
        IAmazonEC2 EC2;

        public ControlGUI() {
            InitializeComponent();
            //get config and start a client
            this.EC2 = AWSClientFactory.CreateAmazonEC2Client();
            this.GetStatus(true);

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool shouldWatch = Boolean.Parse(configuration.AppSettings.Settings["ShouldWeMonitor"].Value);

            if (shouldWatch)
            {
                System.Timers.Timer watcher = new System.Timers.Timer(600000);
                watcher.Elapsed += new System.Timers.ElapsedEventHandler(WatchTheServer);
                watcher.Interval = 1000;
                watcher.Enabled = true;
            }
        }

        private void WatchTheServer(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {

                // get the time running
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                int maxHours = Int32.Parse(configuration.AppSettings.Settings["WatchHours"].Value);

                DescribeInstancesRequest req = new DescribeInstancesRequest();
                var result = this.EC2.DescribeInstances(req);
                bool areWeOver = false;
                string overBy = "";

                List<string> toKill = new List<string>();

                foreach (var reservation in result.Reservations)
                {
                    foreach (var ns in reservation.Instances)
                    {
                        if (ns.State.Name == InstanceStateName.Running)
                        {
                            //check time running
                            DateTime launch = ns.LaunchTime;
                            if (DateTime.Now >= launch.AddHours(maxHours))
                            {
                                areWeOver = true;
                                TimeSpan howManyHours = launch - DateTime.Now;
                                overBy = howManyHours.Hours.ToString();
                                toKill.Add(ns.InstanceId);
                            }
                        }
                    }
                }

                if (areWeOver)
                {
                    var deleteRequest = new TerminateInstancesRequest()
                    {
                        InstanceIds = toKill
                    };

                    Environment.Exit(0);

                }
            }

            catch (Exception ex)
            {

            }           
        }
               

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            Settings newSettings = new Settings();
            newSettings.Show();
        }

        public void GetStatus(bool shouldUpdateStatusToUser) {
            if (shouldUpdateStatusToUser) { 
            this.statusStripLbl.Text = "Refreshing Status";
        }
            this.serverDG.Rows.Clear();
            try {
               
                DescribeInstancesRequest req = new DescribeInstancesRequest();
                var result = this.EC2.DescribeInstances(req);

                foreach (var reservation in result.Reservations) {
                    foreach (var ns in reservation.Instances) {
                        this.serverDG.Rows.Add(ns.InstanceId, ns.LaunchTime, ns.State.Name.Value, ns.PrivateIpAddress, ns.PublicIpAddress, ns.ImageId, ns.InstanceType.Value, ns.Placement.AvailabilityZone);
                    }
                }

                if (this.serverDG.Rows.Count <= 0) {
                   this.lblNoServers.Visible = true;
                } else {
                    this.lblNoServers.Visible = false;
                }

                this.serverDG.Update();

                if (shouldUpdateStatusToUser) {
                    this.statusStripLbl.Text = "Status Updated - " + DateTime.Now.ToShortTimeString();
                }
            } catch (Exception ex) {
                this.statusStripLbl.Text = "Caught Exception: " + ex.Message;
            }
        }

        private void btnRefreshStatus_Click(object sender, EventArgs e) {
            this.GetStatus(true);
        }

        private void btnServerStopAll_Click(object sender, EventArgs e) {
            try {
                List<string> allInstances = new List<string>();
                
                DescribeInstancesRequest req = new DescribeInstancesRequest();
                var result = this.EC2.DescribeInstances(req);

                foreach (var reservation in result.Reservations) {
                    foreach (var ns in reservation.Instances) {
                        allInstances.Add(ns.InstanceId);
                    }
                }

                var deleteRequest = new TerminateInstancesRequest() {
                    InstanceIds = allInstances
                };

                var deleteResponse = this.EC2.TerminateInstances(deleteRequest);
                foreach (InstanceStateChange item in deleteResponse.TerminatingInstances) {
                    //status
                }

                this.statusStripLbl.Text = "Terminated ALL Servers - " + DateTime.Now.ToShortTimeString();
                this.GetStatus(false);
            } catch (Exception ex) {
                this.statusStripLbl.Text = "Caught Exception: " + ex.Message;
            }
        }

        private void btnServerStart_Click(object sender, EventArgs e) {

            this.btnRefreshStatus.Enabled = false;
            this.btnServerStart.Enabled = false;
            this.btnServerStopAll.Enabled = false;

            //Stop any other ones running first...
            this.btnServerStopAll_Click(null, null);

            try {

                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string AMI = configuration.AppSettings.Settings["AMI"].Value;
                string IP = configuration.AppSettings.Settings["IPAddress"].Value;
                string type = configuration.AppSettings.Settings["InstanceType"].Value;
                string securityGroup = configuration.AppSettings.Settings["SecurityGroup"].Value;
                string urlToStartupPackage = "WZA_startupPackageURL=" + configuration.AppSettings.Settings["URLToStartupPackage"].Value;
                bool shouldUseStartupPackage = bool.Parse(configuration.AppSettings.Settings["ShouldUseStartupPackage"].Value);

                this.statusStripLbl.Text = "Starting a Wowza server - " + DateTime.Now.ToShortTimeString();
                RunInstancesRequest req = new RunInstancesRequest();
                req.EbsOptimized = false;
                req.ImageId = AMI;
                req.InstanceType = InstanceType.FindValue(type);
                req.MaxCount = 1;
                req.MinCount = 1;
                if (shouldUseStartupPackage) {
                    byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(urlToStartupPackage);
                    req.UserData = Convert.ToBase64String(encbuff);
                }
                req.SecurityGroupIds = new List<string> { securityGroup };
                RunInstancesResponse response = this.EC2.RunInstances(req);

                //wait
                AssociateAddressRequest addressReq = new AssociateAddressRequest();
                Instance latestInstance = response.Reservation.Instances.OrderBy(o => o.LaunchTime).FirstOrDefault();

                int giveUpCount = 0;
                while (giveUpCount < 20) {

                  var statusRequest = new DescribeInstanceStatusRequest {
                      InstanceIds = { latestInstance.InstanceId }
                  };

                  var result = this.EC2.DescribeInstanceStatus(statusRequest);
                  if (result.InstanceStatuses.Count > 0 && result.InstanceStatuses[0].InstanceState.Code == 16) {
                      break;
                  }

                  this.GetStatus(false);
                  giveUpCount++;

                  int timeout = 0;
                  while (timeout < 1000) {
                      this.statusStripLbl.Text = "Waiting for the Wowza server to start- " + DateTime.Now.ToShortTimeString();
                      Application.DoEvents();
                      System.Threading.Thread.Sleep(100);
                      timeout++;
                  }                 
              }

                this.statusStripLbl.Text = "Associated the IP address " + IP + " to the new server - " + DateTime.Now.ToShortTimeString();

                addressReq.InstanceId = latestInstance.InstanceId;
                addressReq.PublicIp = IP;
                AssociateAddressResponse addressResponse = this.EC2.AssociateAddress(addressReq);

                this.GetStatus(false);

            } catch (Exception ex) {
                this.statusStripLbl.Text = "Caught Exception: " + ex.Message;
            }

            this.btnRefreshStatus.Enabled = true;
            this.btnServerStart.Enabled = true;
            this.btnServerStopAll.Enabled = true;
        }

        //No longer used, we pull this from the website
        public string LoadStartupPackageFromFile() {
            string path = Application.UserAppDataPath + "\\StartUpPackage.zip";
            StreamReader read = new StreamReader(path);
            string userDataString = read.ReadToEnd();
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(userDataString);
            return Convert.ToBase64String(encbuff);
        }

        public void GUIClosing(Object sender, FormClosingEventArgs e)
        {
            DialogResult resultShouldClose = MessageBox.Show("Exiting will immediately STOP the server and video streaming will stop\n\nReady to turn off streaming?", "Yes to stop streaming", MessageBoxButtons.YesNoCancel);
            if (resultShouldClose == DialogResult.Yes) {
                this.btnServerStopAll_Click(null,null);
                e.Cancel = false;                             
            } else {
                e.Cancel = true;                 
            }          
        }

        private void openServerAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string AMI = configuration.AppSettings.Settings["AMI"].Value;
            string IP = configuration.AppSettings.Settings["IPAddress"].Value;
               
            string linkToAdmin = "http://root:GodIsAwesome@" + IP + ":8088/enginemanager/";
            System.Diagnostics.Process.Start(linkToAdmin);               
        }

    }
}
