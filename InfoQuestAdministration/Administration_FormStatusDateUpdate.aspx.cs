using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_FormStatusDateUpdate : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (!string.IsNullOrEmpty(Request.QueryString["s_ReportNumber"]))
          {
            TextBox_ReportNumber.Text = Request.QueryString["s_ReportNumber"];

            GetFormData();
          }
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_FormStatusDateUpdate.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }


    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = Server.HtmlEncode(TextBox_ReportNumber.Text);

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text) + "&";
      }

      string FinalURL = "Administration_FormStatusDateUpdate.aspx?" + SearchField1;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form Status Date Update", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form Status Date Update", "Administration_FormStatusDateUpdate.aspx");
      Response.Redirect(FinalURL, false);
    }


    protected void GetFormData()
    {
      string ReportNumber = "";
      string Status = "";
      string OLD_StatusDate = "";
      string OLD_Month = "";
      string NEW_StatusDate = "";
      string NEW_Month = "";
      string Query = "";

      string SQLStringForm = "EXECUTE spAdministration_Execute_FormStatusDateUpdate @ReportNumber";
      using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
      {
        SqlCommand_Form.Parameters.AddWithValue("@ReportNumber", Request.QueryString["s_ReportNumber"]);
        DataTable DataTable_Form;
        using (DataTable_Form = new DataTable())
        {
          DataTable_Form.Locale = CultureInfo.CurrentCulture;
          DataTable_Form = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
          foreach (DataRow DataRow_Row in DataTable_Form.Rows)
          {
            ReportNumber = DataRow_Row["ReportNumber"].ToString();
            Status = DataRow_Row["Status"].ToString();
            OLD_StatusDate = DataRow_Row["OLD_StatusDate"].ToString();
            OLD_Month = DataRow_Row["OLD_Month"].ToString();
            NEW_StatusDate = DataRow_Row["NEW_StatusDate"].ToString();
            NEW_Month = DataRow_Row["NEW_Month"].ToString();
            Query = DataRow_Row["Query"].ToString();
          }
        }
      }

      Label_ReportNumber.Text = ReportNumber;
      Label_Status.Text = Status;
      Label_OLD_StatusDate.Text = OLD_StatusDate;
      Label_OLD_Month.Text = OLD_Month;
      Label_NEW_StatusDate.Text = NEW_StatusDate;
      Label_NEW_Month.Text = NEW_Month;
      Label_Query.Text = Query;
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(Label_Query.Text))
      {
        string SQLStringUpdate = Label_Query.Text;
        using (SqlCommand SqlCommand_Update = new SqlCommand(SQLStringUpdate))
        {
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_Update);
        }

        Label_UpdateSuccess.Text = "<font style='color:#003768; font-weight:bold;'>Form Status Date was successfully updated<?font>";
      }
      else
      {
        Label_UpdateSuccess.Text = "<font style='color:#b0262e; font-weight:bold;'>Form Status Date was not successfully updated, Query is missing</font>";
      }
    }
  }
}