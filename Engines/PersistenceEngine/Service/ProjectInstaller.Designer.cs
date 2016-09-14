namespace PersistenceEngine
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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PersistenceEngineInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // PersistenceEngineInstaller
            // 
            this.PersistenceEngineInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.PersistenceEngineInstaller.Password = null;
            this.PersistenceEngineInstaller.Username = null;
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.Description = "Persistence Engine for Charon Software Services.";
            this.serviceInstaller.DisplayName = "Charon Persistence Engine";
            this.serviceInstaller.ServiceName = "PersistenceEngineInstaller";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.PersistenceEngineInstaller,
            this.serviceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller PersistenceEngineInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
    }
}