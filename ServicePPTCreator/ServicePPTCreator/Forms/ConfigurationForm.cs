using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ServicePPTCreator
{
    public partial class ConfigurationForm : Form
    {

        public string MasterTemplate { get; set; }
        public string MorningHymnFolder { get; set; }
        public string MorningHymnnalFolder { get; set; }
        public string EveningHymnFolder { get; set; }
        public string EveningAnnoucementVideoSourceDir { get; set; }

        public string PCOServiceID { get; set; }
        public string PCOAPIKey { get; set; }
        public string PCOAPISecret { get; set; }

        public string HymnaryBaseURL { get; set; }
        public string BulletinBaseURL { get; set; }

        public string RootDirectory { get { return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Service Files\\"; } }

        public string NIVFile { get { return RootDirectory + "Bible\\niv1984.db3"; } }
        public string BibleConnectionString { get { return "Data Source=" + NIVFile + ";Version=3;"; } }

        public string MorningDirectory { get { return RootDirectory + "Morning\\"; } }
        public string MorningBulletins { get { return MorningDirectory + "Bulletins\\"; } }
        public string MorningHymnal { get { return MorningDirectory + "Hymnal\\"; } }
        public string MorningHymns { get { return MorningDirectory + "Hymns\\"; } }
        public string MorningStandardSlides { get { return MorningDirectory + "StandardSlides\\"; } }

        public string EveningDirectory { get { return RootDirectory + "Evening\\"; } }
        public string EveningHymns { get { return EveningDirectory + "Hymns\\"; } }
        public string EveningImages { get { return EveningDirectory + "Images\\"; } }
        public string EveningStandardSlides { get { return EveningDirectory + "StandardSlides\\"; } }

        public ConfigurationForm() {
            InitializeComponent();

            //Load keys
            XDocument xmlKeys = XDocument.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\config.xml");
            foreach (var aKey in xmlKeys.Descendants("Local")) {
                foreach (var aKeyAttribute in aKey.Elements("Templates")) {
                    foreach (var keyItem in aKeyAttribute.Attributes()) {
                        if (keyItem.Name == "MasterTemplate") {
                            MasterTemplate = keyItem.Value;
                        }
                    }
                }

                foreach (var aKeyAttribute in aKey.Elements("Folders")) {
                    foreach (var keyItem in aKeyAttribute.Attributes()) {
                        if (keyItem.Name == "MorningHymnFolder") {
                            MorningHymnFolder = keyItem.Value;
                        }

                        if (keyItem.Name == "MorningHymnnalFolder") {
                            MorningHymnnalFolder = keyItem.Value;
                        }

                        if (keyItem.Name == "EveningHymnFolder") {
                            EveningHymnFolder = keyItem.Value;
                        }

                        if (keyItem.Name == "EveningAnnoucementVideoSourceDir") {
                            EveningAnnoucementVideoSourceDir = keyItem.Value;
                        }
                    }
                }
            }

            foreach (var aKey in xmlKeys.Descendants("Remote")) {
                foreach (var aKeyAttribute in aKey.Elements("PCO")) {
                    foreach (var keyItem in aKeyAttribute.Attributes()) {
                        if (keyItem.Name == "PCOServiceID") {
                            PCOServiceID = keyItem.Value;
                        }

                        if (keyItem.Name == "PCOAPIKey") {
                            PCOAPIKey = keyItem.Value;
                        }

                        if (keyItem.Name == "PCOAPISecret") {
                            PCOAPISecret = keyItem.Value;
                        }
                    }
                }

                foreach (var aKeyAttribute in aKey.Elements("URLs")) {
                    foreach (var keyItem in aKeyAttribute.Attributes()) {
                        if (keyItem.Name == "HymnaryBaseURL") {
                            HymnaryBaseURL = keyItem.Value;
                        }

                        if (keyItem.Name == "BulletinBaseURL") {
                            BulletinBaseURL = keyItem.Value;
                        }
                    }
                }
            }


            this.browseFileMasterTemplateTxtBox.Text = MasterTemplate;
            this.browseFolderMorningHymnTxtBox.Text = MorningHymnFolder;
            this.browseFolderMorningHymnalTxtBox.Text = MorningHymnnalFolder;
            this.browseFolderEveningHymnTxtBox.Text = EveningHymnFolder;
            this.browseFolderEveningAnnoucementVideoSourceDir.Text = EveningAnnoucementVideoSourceDir;

            this.pcoServiceIDTxtBox.Text = PCOServiceID;
            this.pcoAPIKeyTxtBox.Text = PCOAPIKey;
            this.pcoAPISecretTxtBox.Text = PCOAPISecret;

            this.hymnaryBaseURLTxtBox.Text = HymnaryBaseURL;
            this.bulletinBaseURLTxtBox.Text = BulletinBaseURL;
        }

        private void ConfigurationForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            StreamWriter w = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "\\config.xml");
            w.WriteLine("<Config>");
            w.WriteLine("<Local>");
            w.WriteLine("<Templates MasterTemplate=\"" + this.browseFileMasterTemplateTxtBox.Text + "\" />");
            w.WriteLine("<Folders MorningHymnFolder=\"" + this.browseFolderMorningHymnTxtBox.Text + "\" MorningHymnnalFolder=\"" + this.browseFolderMorningHymnalTxtBox.Text + "\" EveningHymnFolder=\"" + this.browseFolderEveningHymnTxtBox.Text + "\" EveningAnnoucementVideoSourceDir=\"" + this.browseFolderEveningAnnoucementVideoSourceDir.Text + "\" />");
            w.WriteLine("</Local>");
            w.WriteLine("<Remote>");
            w.WriteLine("<PCO PCOServiceID=\"" + this.pcoServiceIDTxtBox.Text + "\" PCOAPIKey=\"" + this.pcoAPIKeyTxtBox.Text + "\" PCOAPISecret=\"" + this.pcoAPISecretTxtBox.Text + "\" />");
            w.WriteLine("<URLs HymnaryBaseURL=\"" + this.hymnaryBaseURLTxtBox.Text + "\" BulletinBaseURL=\"" + this.bulletinBaseURLTxtBox.Text + "\" />");
            w.WriteLine("</Remote>");
            w.WriteLine("</Config>");
        }
    }
}
