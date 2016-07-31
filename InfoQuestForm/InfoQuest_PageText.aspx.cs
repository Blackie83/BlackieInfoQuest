using System;
using System.Web.UI.WebControls;

namespace InfoQuestForm
{
  public partial class InfoQuest_PageText : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

        if (Request.QueryString["PageTextValue"] == null)
        {
          label_PageText.Text = "";
        }
        else
        {
          label_PageText.Text = InfoQuestWCF.InfoQuest_All.All_SystemPageText(Request.QueryString["PageTextValue"]);
        }
      }
    }

    protected void Page_Error(object sender, EventArgs e)
    {
      Exception Exception_Error = Server.GetLastError().GetBaseException();
      Server.ClearError();

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      ((Label)PageUpdateProgress_PageText.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
    }
  }
}