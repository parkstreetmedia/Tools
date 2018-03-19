using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SermonUploader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			SermonUploader main = new SermonUploader();
			if (main != null && !main.IsDisposed) {
				Application.Run(main);
			}           
        }
    }
}
