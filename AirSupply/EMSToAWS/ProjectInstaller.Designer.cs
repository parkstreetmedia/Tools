namespace EMSToAWS
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.EMSToAWSServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EMSToAWSServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // EMSToAWSServiceProcessInstaller
            // 
            this.EMSToAWSServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.EMSToAWSServiceProcessInstaller.Password = null;
            this.EMSToAWSServiceProcessInstaller.Username = null;
            // 
            // EMSToAWSServiceInstaller
            // 
            this.EMSToAWSServiceInstaller.Description = "Pushes EMS Events to an AWS Database for signage and information and sets Unit te" +
    "mperature and gathers status for all C-50a controlled units";
            this.EMSToAWSServiceInstaller.DisplayName = "EMS to AWS";
            this.EMSToAWSServiceInstaller.ServiceName = "EMSToAWS";
            this.EMSToAWSServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EMSToAWSServiceProcessInstaller,
            this.EMSToAWSServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EMSToAWSServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EMSToAWSServiceInstaller;
    }
}