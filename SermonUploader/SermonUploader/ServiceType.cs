using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SermonUploader
{
    public partial class ServiceType : Form
    {
        public ServiceType()
        {
            InitializeComponent();
        }

        private void morningBtn_Click(object sender, EventArgs e)
        {
            SermonUploader.IsThisAM = true;
            this.Hide();
            this.Close();

        }

        private void eveningButton_Click(object sender, EventArgs e)
        {
            SermonUploader.IsThisAM = false;
            this.Hide();
            this.Close();
        }
    }
}
