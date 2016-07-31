using System;

namespace InfoQuestAdministration
{
  public partial class Controls_Footer : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Label_Footer.Text = InfoQuestWCF.InfoQuest_All.All_FormFooter();
    }
  }
}