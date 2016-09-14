namespace ProductEngine
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
            this.ProductEngineInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ProductEngineInstaller
            // 
            this.ProductEngineInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ProductEngineInstaller.Password = null;
            this.ProductEngineInstaller.Username = null;
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.Description = "Product Engine for Charon Software Services.";
            this.serviceInstaller.DisplayName = "Charon Product Engine";
            this.serviceInstaller.ServiceName = "ProductEngineInstaller";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ProductEngineInstaller,
            this.serviceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ProductEngineInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
    }
}