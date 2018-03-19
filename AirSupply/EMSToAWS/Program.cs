using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMSToAWS
{
    static class Program
    {
        static void Main(string[] args) {

            // In interactive and debug mode ?
            if (Environment.UserInteractive) {
                //EMSToAWSService em = new EMSToAWSService();
                //em.DoAllTheThings();
                MessageBox.Show("Please don't run me like that! I'm a service, not some interactive app! Consult the help documentation please.", "Nooooo");
            }
            else {
                ServiceBase[] ServicesToRun = new ServiceBase[] { new EMSToAWSService(args) };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
