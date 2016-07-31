using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace InfoQuestWS
{
  [RunInstaller(true)]
  public partial class ProjectInstaller : System.Configuration.Install.Installer
  {
    public ProjectInstaller()
    {
      InitializeComponent();

      this.AfterInstall += new InstallEventHandler(ServiceInstaller_AfterInstall);
    }

    void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
    {
      using (ServiceController ServiceController_InfoQuest = new ServiceController("InfoQuestWindowsService"))
      {
        ServiceController_InfoQuest.Start();
      }
    }
  }
}
