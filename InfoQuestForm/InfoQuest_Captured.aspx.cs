using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class InfoQuest_Captured : InfoQuestWCF.Override_SystemWebUIPage
  {
    private Dictionary<string, Action> FormHandler = new Dictionary<string, Action>();
    private Dictionary<string, string> FormIdHandler = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

        if (Request.QueryString["CapturedPage"] == null || Request.QueryString["CapturedNumber"] == null)
        {
          Label_FormName.Text = "";
          Hyperlink_View.NavigateUrl = "";
          Hyperlink_Captured.NavigateUrl = "";
        }
        else
        {
          Form(Request.QueryString["CapturedPage"]);
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
          SqlCommand_Security.Parameters.AddWithValue("@Form_Id", FormId(Request.QueryString["CapturedPage"]));

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          string FormIdValue = FormId(Request.QueryString["CapturedPage"]);

          if (FormIdValue == "1" || FormIdValue == "2" || FormIdValue == "44")
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
        ((Label)PageUpdateProgress_Captured.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }


    // TODO : New Form : InfoQuest_Captured : Add new Form to FormHandlers()
    protected void FormHandlers()
    {
      FormHandler.Add("Form_HeadOfficeQualityAudit", new Action(Form_HeadOfficeQualityAudit));
      FormHandler.Add("Form_FSM_Location", new Action(Form_FSM_Location));
      FormHandler.Add("Form_FSM_BusinessUnit", new Action(Form_FSM_BusinessUnit));
      FormHandler.Add("Form_FSM_BusinessUnitComponent", new Action(Form_FSM_BusinessUnitComponent));
      FormHandler.Add("Form_TransparencyRegister", new Action(Form_TransparencyRegister));
    }

    // TODO : New Form : InfoQuest_Captured : Add new Form to FormIdHandlers()
    protected void FormIdHandlers()
    {
      FormIdHandler.Add("Form_HeadOfficeQualityAudit", "40");
      FormIdHandler.Add("Form_FSM_Location", "41");
      FormIdHandler.Add("Form_FSM_BusinessUnit", "41");
      FormIdHandler.Add("Form_FSM_BusinessUnitComponent", "41");
      FormIdHandler.Add("Form_TransparencyRegister", "44");
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
    private void Form_HeadOfficeQualityAudit()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["CapturedPage"]));

      string HQAFindingId = "";
      string SQLStringFormDetail = "SELECT HQA_Finding_Id FROM Form_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @CapturedNumber";
      using (SqlCommand SqlCommand_FormDetail = new SqlCommand(SQLStringFormDetail))
      {
        SqlCommand_FormDetail.Parameters.AddWithValue("@CapturedNumber", Request.QueryString["CapturedNumber"]);
        DataTable DataTable_FormDetail;
        using (DataTable_FormDetail = new DataTable())
        {
          DataTable_FormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_FormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormDetail).Copy();
          if (DataTable_FormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormDetail.Rows)
            {
              HQAFindingId = DataRow_Row["HQA_Finding_Id"].ToString();
            }
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_HeadOfficeQualityAudit.aspx?HQAFindingId=" + HQAFindingId + "";
      Hyperlink_Captured.NavigateUrl = "Form_HeadOfficeQualityAudit_List.aspx";

      HQAFindingId = "";
    }

    private void Form_FSM_Location()
    {
      Label_FormName.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["CapturedPage"])) + " : Location", CultureInfo.CurrentCulture);
      Hyperlink_View.NavigateUrl = "Form_FSM_Location.aspx?LocationKey=" + Request.QueryString["CapturedNumber"] + "";
      Hyperlink_Captured.NavigateUrl = "Form_FSM_Location.aspx";
    }

    private void Form_FSM_BusinessUnit()
    {
      Label_FormName.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["CapturedPage"])) + " : Business Unit", CultureInfo.CurrentCulture);
      Hyperlink_View.NavigateUrl = "Form_FSM_BusinessUnit.aspx?BusinessUnitKey=" + Request.QueryString["CapturedNumber"] + "";
      Hyperlink_Captured.NavigateUrl = "Form_FSM_BusinessUnit.aspx";
    }

    private void Form_FSM_BusinessUnitComponent()
    {
      Label_FormName.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["CapturedPage"])) + " : Business Unit Component", CultureInfo.CurrentCulture);
      Hyperlink_View.NavigateUrl = "Form_FSM_BusinessUnitComponent.aspx?BusinessUnitComponentKey=" + Request.QueryString["CapturedNumber"] + "";
      Hyperlink_Captured.NavigateUrl = "Form_FSM_BusinessUnitComponent.aspx";
    }

    private void Form_TransparencyRegister()
    {
      Label_FormName.Text = (InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["CapturedPage"])).Replace(" Form", "")).ToString();
      Hyperlink_View.NavigateUrl = "Form_TransparencyRegister.aspx?TransparencyRegister_Id=" + Request.QueryString["CapturedNumber"] + "";
      Hyperlink_Captured.NavigateUrl = "Form_TransparencyRegister.aspx";
    }
  }
}