using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_InfrastructureAudit_Summary_Print : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("43").Replace(" Form", "")).ToString() + " : Summary", CultureInfo.CurrentCulture);

          if (Request.QueryString["InfrastructureAudit_Id"] != null)
          {
            TableReviewInfo.Visible = true;
            TableList.Visible = true;
            Button_Print.Visible = true;

            InfrastructureAuditSummary();
          }
          else
          {
            TableReviewInfo.Visible = false;
            TableList.Visible = false;
            Button_Print.Visible = false;
          }

          if (TableReviewInfo.Visible == true)
          {
            TableReviewInfoVisible();
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
        string SecurityAllowForm = "0";

        string SQLStringSecurity = "";
        if (Request.QueryString["InfrastructureAudit_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('43')) AND (SecurityRole_Id IN ('173','174') OR Facility_Id IN (SELECT Facility_Id FROM Form_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
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
        InfoQuestWCF.InfoQuest_All.All_Maintenance("43");

        if (PageSecurity() == "1")
        {
        }
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_InfrastructureAudit_Summary_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_InfrastructureAudit_Summary_List.SelectCommand = "spForm_Get_InfrastructureAudit_Summary_List";
      SqlDataSource_InfrastructureAudit_Summary_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_InfrastructureAudit_Summary_List.CancelSelectOnNullParameter = false;
      SqlDataSource_InfrastructureAudit_Summary_List.SelectParameters.Clear();
      SqlDataSource_InfrastructureAudit_Summary_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_InfrastructureAudit_Summary_List.SelectParameters.Add("InfrastructureAuditId", TypeCode.String, Request.QueryString["InfrastructureAudit_Id"]);
    }

    private void InfrastructureAuditSummary()
    {
      Session["InfrastructureAuditId"] = "";
      string SQLStringInfrastructureAudit = "SELECT InfrastructureAudit_Id FROM Form_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id AND InfrastructureAudit_IsActive = 1";
      using (SqlCommand SqlCommand_InfrastructureAudit = new SqlCommand(SQLStringInfrastructureAudit))
      {
        SqlCommand_InfrastructureAudit.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);
        DataTable DataTable_InfrastructureAudit;
        using (DataTable_InfrastructureAudit = new DataTable())
        {
          DataTable_InfrastructureAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_InfrastructureAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfrastructureAudit).Copy();
          if (DataTable_InfrastructureAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfrastructureAudit.Rows)
            {
              Session["InfrastructureAuditId"] = DataRow_Row["InfrastructureAudit_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["InfrastructureAuditId"].ToString()))
      {
        TableReviewInfo.Visible = false;
        TableList.Visible = false;
        Button_Print.Visible = false;
      }
      else
      {
        TableReviewInfo.Visible = true;
        TableList.Visible = true;
        Button_Print.Visible = true;
      }
    }

    private void TableReviewInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["InfrastructureAuditDate"] = "";
      Session["InfrastructureAuditCompleted"] = "";
      string SQLStringReviewInfo = "SELECT DISTINCT Facility_FacilityDisplayName , InfrastructureAudit_Date , InfrastructureAudit_Completed FROM vForm_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id";
      using (SqlCommand SqlCommand_ReviewInfo = new SqlCommand(SQLStringReviewInfo))
      {
        SqlCommand_ReviewInfo.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);
        DataTable DataTable_ReviewInfo;
        using (DataTable_ReviewInfo = new DataTable())
        {
          DataTable_ReviewInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_ReviewInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ReviewInfo).Copy();
          if (DataTable_ReviewInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ReviewInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["InfrastructureAuditDate"] = DataRow_Row["InfrastructureAudit_Date"];
              Session["InfrastructureAuditCompleted"] = DataRow_Row["InfrastructureAudit_Completed"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label_Facility.Text = Session["FacilityFacilityDisplayName"].ToString();
        Label_Date.Text = Convert.ToDateTime(Session["InfrastructureAuditDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("InfrastructureAuditDate");
      Session.Remove("InfrastructureAuditCompleted");
    }


    protected void Button_Print_Click(object sender, EventArgs e)
    {
      Button_Print.Visible = false;
      ScriptManager.RegisterStartupScript(UpdatePanel_InfrastructureAudit_Summary_Print, this.GetType(), "Print", "window.print()", true);
      GridView_InfrastructureAudit_Summary_List.DataBind();
    }

    protected void GridView_InfrastructureAudit_Summary_List_PreRender(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_InfrastructureAudit_Summary_List.Rows.Count; i++)
      {
        if (GridView_InfrastructureAudit_Summary_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_InfrastructureAudit_Summary_List.Rows[i].Cells[3].Text.IndexOf("Criteria", StringComparison.CurrentCulture) == 0)
          {
            GridView_InfrastructureAudit_Summary_List.Rows[i].Cells[3].ColumnSpan = 2;
            GridView_InfrastructureAudit_Summary_List.Rows[i].Cells[3].HorizontalAlign = HorizontalAlign.Left;

            GridView_InfrastructureAudit_Summary_List.Rows[i].Cells[4].Visible = false;
          }
        }
      }
    }
  }
}