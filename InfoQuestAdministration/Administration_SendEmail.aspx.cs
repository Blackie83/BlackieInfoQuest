using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SendEmail : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          string SendEmailBody = "";
          string SQLStringSendEmail = "SELECT SendEmail_Body FROM Administration_SendEmail WHERE SendEmail_Id = @SendEmail_Id";
          using (SqlCommand SqlCommand_SendEmail = new SqlCommand(SQLStringSendEmail))
          {
            SqlCommand_SendEmail.Parameters.AddWithValue("@SendEmail_Id", Request.QueryString["SendEmail_Id"]);
            DataTable DataTable_SendEmail;
            using (DataTable_SendEmail = new DataTable())
            {
              DataTable_SendEmail.Locale = CultureInfo.CurrentCulture;

              DataTable_SendEmail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SendEmail).Copy();
              if (DataTable_SendEmail.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_SendEmail.Rows)
                {
                  SendEmailBody = DataRow_Row["SendEmail_Body"].ToString();
                }
              }
            }
          }

          SendEmailBody = SendEmailBody.Replace(Convert.ToString("<a href=", CultureInfo.CurrentCulture), Convert.ToString("<a target=\"_blank\" href=", CultureInfo.CurrentCulture));

          Label_SendEmail_Id.Text = SendEmailBody;
        }
      }
    }

    private string PageSecurity()
    {
      string SecurityAllow = "0";

      string SecurityAllowAdministration = "0";

      SecurityAllowAdministration = InfoQuestWCF.InfoQuest_Security.Security_Form_Administration(Request.ServerVariables["LOGON_USER"]);

      if (SecurityAllowAdministration == "1")
      {
        SecurityAllow = "1";
      }
      else
      {
        SecurityAllow = "0";
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
        Response.End();
      }

      return SecurityAllow;
    }

    protected void Page_Error(object sender, EventArgs e)
    {
      Exception Exception_Error = Server.GetLastError().GetBaseException();
      Server.ClearError();

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      InfoQuestWCF.InfoQuest_All.All_Maintenance("0");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SendEmail.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }
  }
}