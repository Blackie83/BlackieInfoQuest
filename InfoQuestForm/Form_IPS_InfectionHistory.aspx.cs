using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_IPS_InfectionHistory : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName("37") + " : Infection History", CultureInfo.CurrentCulture);
          Label_InfoHeading.Text = Convert.ToString("Visit Information", CultureInfo.CurrentCulture);
          Label_InfectionHistoryHeading.Text = Convert.ToString("Infection History", CultureInfo.CurrentCulture);

          if (Request.QueryString["IPSVisitInformationId"] != null && Request.QueryString["IPSInfectionId"] != null)
          {
            TableInfo.Visible = true;

            Button_Close.Attributes.Add("onclick", "window.close();");

            TableInfectionHistory.Visible = true;
          }
          else
          {
            TableInfo.Visible = false;
            TableInfectionHistory.Visible = false;
          }

          if (TableInfo.Visible == true)
          {
            TableInfoVisible();
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37'))";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          SecurityAllow = "0";
          Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("37");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_IPS_InfectionHistory.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_IPS_InfectionHistory_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InfectionHistory_List.SelectCommand = "spForm_Get_IPS_InfectionHistory_List";
      SqlDataSource_IPS_InfectionHistory_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InfectionHistory_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_InfectionHistory_List.SelectParameters.Clear();
      SqlDataSource_IPS_InfectionHistory_List.SelectParameters.Add("IPS_VisitInformation_Id", TypeCode.String, Request.QueryString["IPSVisitInformationId"]);
      SqlDataSource_IPS_InfectionHistory_List.SelectParameters.Add("IPS_VisitInformation_VisitNumber", TypeCode.String, Request.QueryString["IPSVisitInformationVisitNumber"]);
    }

    private void TableInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["IPSVisitInformationVisitNumber"] = "";
      Session["PatientInformationName"] = "";
      Session["PatientInformationSurname"] = "";
      string SQLStringVisitInfo = "EXECUTE spForm_Get_IPS_InfectionHistoryInformation @IPS_VisitInformation_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);
        DataTable DataTable_VisitInfo;
        using (DataTable_VisitInfo = new DataTable())
        {
          DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
          if (DataTable_VisitInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["IPSVisitInformationVisitNumber"] = DataRow_Row["IPS_VisitInformation_VisitNumber"];
              Session["PatientInformationName"] = DataRow_Row["PatientInformation_Name"];
              Session["PatientInformationSurname"] = DataRow_Row["PatientInformation_Surname"];
            }
          }
        }
      }

      Label_IFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_IVisitNumber.Text = Session["IPSVisitInformationVisitNumber"].ToString();
      Label_IName.Text = Session["PatientInformationSurname"].ToString() + Convert.ToString(", ", CultureInfo.CurrentCulture) + Session["PatientInformationName"].ToString();

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("IPSVisitInformationVisitNumber");
      Session.Remove("PatientInformationName");
      Session.Remove("PatientInformationSurname");
    }

    protected void Button_Close_OnClick(object sender, EventArgs e)
    {
      ScriptManager.RegisterStartupScript(UpdatePanel_IPS_InfectionHistory, this.GetType(), "Close Patient Infection History Window", "<script language='javascript'> { self.close() }</script>", true);
    }


    //--START-- --InfectionHistory--//
    protected void SqlDataSource_IPS_InfectionHistory_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenInfectionHistoryTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_InfectionHistory_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_InfectionHistory_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_InfectionHistoryTotalRecords = (Label)e.Row.FindControl("Label_InfectionHistoryTotalRecords");
          Label_InfectionHistoryTotalRecords.Text = Label_HiddenInfectionHistoryTotalRecords.Text;
        }
      }
    }
    //---END--- --InfectionHistory--//
  }
}