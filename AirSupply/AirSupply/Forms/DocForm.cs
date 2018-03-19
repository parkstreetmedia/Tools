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

namespace AirSupply
{
    public partial class DocForm : Form
    {
        public DocForm() {
            InitializeComponent();
            string appDir = Path.GetDirectoryName(Application.ExecutablePath);
            string docFile = Path.Combine(appDir, "Doc.html");
            this.browser.Url = new Uri("file:///" + docFile);
        }
    }
}
