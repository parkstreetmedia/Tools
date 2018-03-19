using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirSupply
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            //That is causing the comboboxes to ignore the alternating colors in windows 10
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SplashForm.ShowSplashScreen();
            System.Threading.Thread.Sleep(1000);
            AirSupplyForm mainForm = new AirSupplyForm();
            SplashForm.CloseForm();
            Application.Run(mainForm);                    
        }
    }
}
