using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2Control {
    class Program {

        [STAThread]
        public static void Main(string[] args) {
             if (args.Length == 0) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ControlGUI());
            } else {
                   //stop all instances if run by command line
                    List<string> allInstances = new List<string>();
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    string key = configuration.AppSettings.Settings["AWSAccessKey"].Value;
                    string secret = configuration.AppSettings.Settings["AWSSecretKey"].Value;
                    IAmazonEC2 ec2  = AWSClientFactory.CreateAmazonEC2Client(key, secret);
                    DescribeInstancesRequest req = new DescribeInstancesRequest();
                    var result = ec2.DescribeInstances(req);

                    foreach (var reservation in result.Reservations) {
                        foreach (var ns in reservation.Instances) {
                            allInstances.Add(ns.InstanceId);
                        }
                    }

                    var deleteRequest = new TerminateInstancesRequest() {
                        InstanceIds = allInstances
                    };

                    var deleteResponse = ec2.TerminateInstances(deleteRequest);                    
             }   
        }
    }
}