namespace InfoQuestWS
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
      this.serviceProcessInstaller_InfoQuest = new System.ServiceProcess.ServiceProcessInstaller();
      this.serviceInstaller_InfoQuest = new System.ServiceProcess.ServiceInstaller();
      // 
      // serviceProcessInstaller_InfoQuest
      // 
      this.serviceProcessInstaller_InfoQuest.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
      this.serviceProcessInstaller_InfoQuest.Password = null;
      this.serviceProcessInstaller_InfoQuest.Username = null;
      // 
      // serviceInstaller_InfoQuest
      // 
      this.serviceInstaller_InfoQuest.ServiceName = "InfoQuestWindowsService";
      this.serviceInstaller_InfoQuest.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller_InfoQuest,
            this.serviceInstaller_InfoQuest});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller_InfoQuest;
    private System.ServiceProcess.ServiceInstaller serviceInstaller_InfoQuest;
  }
}