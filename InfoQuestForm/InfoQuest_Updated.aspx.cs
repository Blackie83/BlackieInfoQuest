using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace InfoQuestForm
{
  public partial class InfoQuest_Updated : InfoQuestWCF.Override_SystemWebUIPage
  {
    private Dictionary<string, Action> FormHandler = new Dictionary<string, Action>();
    private Dictionary<string, string> FormIdHandler = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

        if (Request.QueryString["UpdatedPage"] == null || Request.QueryString["UpdatedNumber"] == null)
        {
          Label_FormName.Text = "";
          Hyperlink_View.NavigateUrl = "";
          Hyperlink_Captured.NavigateUrl = "";
        }
        else
        {
          Form(Request.QueryString["UpdatedPage"]);
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
        string SecurityAllowForm = "0";

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id = @Form_Id)";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Form_Id", FormId(Request.QueryString["UpdatedPage"]));

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          string FormIdValue = FormId(Request.QueryString["UpdatedPage"]);

          if (FormIdValue == "1" || FormIdValue == "2" || FormIdValue == "44" || FormIdValue == "47")
          {
            SecurityAllow = "1";
          }
          else
          {
            SecurityAllow = "0";
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
            Response.End();
          }
        }
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
      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_Updated.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }


    // TODO : New Form : InfoQuest_Captured : Add new Form to FormHandlers()
    protected void FormHandlers()
    {
      FormHandler.Add("Form_PharmacySurveys", new Action(Form_PharmacySurveys));
    }

    // TODO : New Form : InfoQuest_Captured : Add new Form to FormIdHandlers()
    protected void FormIdHandlers()
    {
      FormIdHandler.Add("Form_PharmacySurveys", "47");
    }

    protected new void Form(string formName)
    {
      FormHandlers();

      if (FormHandler.ContainsKey(formName))
      {
        FormHandler[formName].Invoke();
      }

      FormHandler.Clear();
    }

    protected string FormId(string formName)
    {
      if (formName == null)
      {
        return "0";
      }
      else
      {
        FormIdHandlers();

        if (FormIdHandler.ContainsKey(formName))
        {
          string FormIdValue = FormIdHandler[formName];
          FormIdHandler.Clear();
          return FormIdValue;
        }
        else
        {
          return "0";
        }
      }
    }


    // TODO : New Form : InfoQuest_Captured : Add new Procedure for Form
    private void Form_PharmacySurveys()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["UpdatedPage"]));
      Hyperlink_View.NavigateUrl = "Form_PharmacySurveys.aspx?CreatedSurveysId=" + Request.QueryString["UpdatedNumber"] + "";
      Label_Split.Visible = false;
      Hyperlink_Captured.Visible = false;
    }
  }
}