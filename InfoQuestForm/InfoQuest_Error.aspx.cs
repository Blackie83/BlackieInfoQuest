using System;
using System.Web.UI.WebControls;

namespace InfoQuestForm
{
  public partial class InfoQuest_Error : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Error(object sender, EventArgs e)
    {
      Exception Exception_Error = Server.GetLastError().GetBaseException();
      Server.ClearError();

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      ((Label)PageUpdateProgress_Error.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
    }
  }
}