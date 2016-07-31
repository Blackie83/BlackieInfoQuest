using System;

namespace InfoQuestAdministration
{
  public partial class Controls_Header : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Label_Header.Text = InfoQuestWCF.InfoQuest_All.All_FormHeader("Administration");
    }
  }
}