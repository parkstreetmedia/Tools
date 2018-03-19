using System;
using System.Windows.Forms;

namespace ServicePPTCreator
{
    public partial class HelpForm : Form
    {
        public HelpForm(string helpFile) {
            InitializeComponent();
            this.helpBrowser.Url = new Uri(helpFile);
        }
    }
}
