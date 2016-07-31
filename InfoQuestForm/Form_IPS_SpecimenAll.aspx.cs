using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_IPS_SpecimenAll : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          PageTitle();

          if (Request.QueryString["IPSVisitInformationId"] != null && Request.QueryString["IPSInfectionId"] != null)
          {
            TableInfo.Visible = true;
            TableSpecimenAll.Visible = true;
          }
          else
          {
            TableInfo.Visible = false;
            TableSpecimenAll.Visible = false;
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

        string SQLStringSecurity = "";
        if (Request.QueryString["IPSVisitInformationId"] == null)
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_IPS_SpecimenAll.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infection Prevention Surveillance", "5");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_IPS_SpecimenAll_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_SpecimenAll_List.SelectCommand = "spForm_Get_IPS_SpecimenAll_List";
      SqlDataSource_IPS_SpecimenAll_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_SpecimenAll_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_SpecimenAll_List.SelectParameters.Clear();
      SqlDataSource_IPS_SpecimenAll_List.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName("37") + " : Specimen All", CultureInfo.CurrentCulture);
      Label_InfoHeading.Text = Convert.ToString("Visit and Infection Information", CultureInfo.CurrentCulture);
      Label_SpecimenAllHeading.Text = Convert.ToString("Specimens", CultureInfo.CurrentCulture);
    }


    private class FromDataBase_SecurityRole
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
      public DataRow[] SecurityFacilityAdminUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminView { get; set; }
    }

    private FromDataBase_SecurityRole GetSecurityRole()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_New = new FromDataBase_SecurityRole();

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('37')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '22'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '155'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '11'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '10'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_InfectionCompleted
    {
      public string IPSInfectionCompleted { get; set; }
      public string IPSInfectionIsActive { get; set; }
    }

    private FromDataBase_InfectionCompleted GetInfectionCompleted()
    {
      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_New = new FromDataBase_InfectionCompleted();

      string SQLStringInfection = "SELECT IPS_Infection_Completed , IPS_Infection_IsActive FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              FromDataBase_InfectionCompleted_New.IPSInfectionCompleted = DataRow_Row["IPS_Infection_Completed"].ToString();
              FromDataBase_InfectionCompleted_New.IPSInfectionIsActive = DataRow_Row["IPS_Infection_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_InfectionCompleted_New;
    }


    //--START-- --VisitInfo--//
    private void TableInfoVisible()
    {
      Session["IPSInfectionId"] = "";
      Session["FacilityFacilityDisplayName"] = "";
      Session["IPSVisitInformationVisitNumber"] = "";
      Session["PatientInformationName"] = "";
      Session["PatientInformationSurname"] = "";
      Session["IPSInfectionReportNumber"] = "";
      Session["IPSInfectionCategoryName"] = "";
      Session["IPSInfectionTypeName"] = "";
      Session["IPSInfectionCompleted"] = "";
      Session["IPSInfectionModifiedDate"] = "";
      Session["IPSInfectionModifiedBy"] = "";
      Session["IPSInfectionHistory"] = "";
      Session["IPSInfectionIsActive"] = "";
      Session["IPSHAIId"] = "";
      Session["IPSHAIModifiedDate"] = "";
      Session["Specimen"] = "";
      Session["Infection"] = "";
      Session["HAI"] = "";
      string SQLStringVisitInfo = "EXECUTE spForm_Get_IPS_InfectionInformation @IPS_Infection_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_VisitInfo;
        using (DataTable_VisitInfo = new DataTable())
        {
          DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
          if (DataTable_VisitInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
            {
              Session["IPSInfectionId"] = DataRow_Row["IPS_Infection_Id"];
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["IPSVisitInformationVisitNumber"] = DataRow_Row["IPS_VisitInformation_VisitNumber"];
              Session["PatientInformationName"] = DataRow_Row["PatientInformation_Name"];
              Session["PatientInformationSurname"] = DataRow_Row["PatientInformation_Surname"];
              Session["IPSInfectionReportNumber"] = DataRow_Row["IPS_Infection_ReportNumber"];
              Session["IPSInfectionCategoryName"] = DataRow_Row["IPS_Infection_Category_Name"];
              Session["IPSInfectionTypeName"] = DataRow_Row["IPS_Infection_Type_Name"];
              Session["IPSInfectionCompleted"] = DataRow_Row["IPS_Infection_Completed"];
              Session["IPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["IPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSInfectionIsActive"] = DataRow_Row["IPS_Infection_IsActive"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
              Session["IPSHAIModifiedDate"] = DataRow_Row["IPS_HAI_ModifiedDate"];
              Session["Specimen"] = DataRow_Row["Specimen"];
              Session["Infection"] = DataRow_Row["Infection"];
              Session["HAI"] = DataRow_Row["HAI"];
            }
          }
        }
      }

      Label_IFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_IVisitNumber.Text = Session["IPSVisitInformationVisitNumber"].ToString();
      Label_IName.Text = Session["PatientInformationSurname"].ToString() + Convert.ToString(", ", CultureInfo.CurrentCulture) + Session["PatientInformationName"].ToString();
      Label_IInfectionReportNumber.Text = Session["IPSInfectionReportNumber"].ToString();
      Label_IInfectionCategoryName.Text = Session["IPSInfectionCategoryName"].ToString();
      Label_IInfectionTypeName.Text = Session["IPSInfectionTypeName"].ToString();
      Label_IInfectionCompleted.Text = Session["IPSInfectionCompleted"].ToString();
      Label_IHAI.Text = Session["HAI"].ToString();
      Label_ISpecimen.Text = Session["Specimen"].ToString();

      Session.Remove("IPSInfectionId");
      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("IPSVisitInformationVisitNumber");
      Session.Remove("PatientInformationName");
      Session.Remove("PatientInformationSurname");
      Session.Remove("IPSInfectionReportNumber");
      Session.Remove("IPSInfectionCategoryName");
      Session.Remove("IPSInfectionTypeName");
      Session.Remove("IPSInfectionCompleted");
      Session.Remove("IPSInfectionModifiedDate");
      Session.Remove("IPSInfectionModifiedBy");
      Session.Remove("IPSInfectionHistory");
      Session.Remove("IPSInfectionIsActive");
      Session.Remove("IPSHAIId");
      Session.Remove("IPSHAIModifiedDate");
      Session.Remove("Specimen");
      Session.Remove("Infection");
      Session.Remove("HAI");
    }

    protected void Button_InfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }

    protected void Button_SpecimenHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"]), false);
    }
    //---END--- --VisitInfo--//


    //--START-- --SpecimenAll--//
    protected void SqlDataSource_IPS_SpecimenAll_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenSpecimenAllTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_SpecimenAll_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }

      for (int i = 0; i < GridView_IPS_SpecimenAll_List.Rows.Count; i++)
      {
        if (GridView_IPS_SpecimenAll_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_IPS_SpecimenAll_List.Rows[i].Cells[11].Text == "Complete")
          {
            GridView_IPS_SpecimenAll_List.Rows[i].Cells[11].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_SpecimenAll_List.Rows[i].Cells[11].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_IPS_SpecimenAll_List.Rows[i].Cells[11].Text == "Incomplete")
          {
            GridView_IPS_SpecimenAll_List.Rows[i].Cells[11].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_SpecimenAll_List.Rows[i].Cells[11].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_IPS_SpecimenAll_List.Rows[i].Cells[11].Text == "Cancelled")
          {
            GridView_IPS_SpecimenAll_List.Rows[i].Cells[11].BackColor = Color.FromName("#ffcc66");
            GridView_IPS_SpecimenAll_List.Rows[i].Cells[11].ForeColor = Color.FromName("#333333");
          }
        }
      }

      GridView_IPS_SpecimenAll_List_GroupRows();
    }

    protected void GridView_IPS_SpecimenAll_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_SpecimenAllTotalRecords = (Label)e.Row.FindControl("Label_SpecimenAllTotalRecords");
          Label_SpecimenAllTotalRecords.Text = Label_HiddenSpecimenAllTotalRecords.Text;
        }


        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_CaptureNewSpecimen = (Button)e.Row.FindControl("Button_CaptureNewSpecimen");

          FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
          DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
          DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
          DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
          DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
          DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
          {
            Security = "0";
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
            {
              Button_CaptureNewSpecimen.Visible = true;
            }
            else
            {
              Button_CaptureNewSpecimen.Visible = false;
            }
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
          {
            Security = "0";
            Button_CaptureNewSpecimen.Visible = false;
          }

          if (Security == "1")
          {
            Security = "0";
            Button_CaptureNewSpecimen.Visible = false;
          }
        }
      }
    }

    private void GridView_IPS_SpecimenAll_List_GroupRows()
    {
      if (GridView_IPS_SpecimenAll_List.Rows.Count == 1)
      {
        GridViewRow GridViewRow_Row = GridView_IPS_SpecimenAll_List.Rows[0];

        GridViewRow_Row.Cells[0].Visible = false;
        GridViewRow_Row.Cells[5].Visible = false;
        GridViewRow_Row.Cells[7].Visible = false;

        GridView_IPS_SpecimenAll_List.HeaderRow.Cells[0].Visible = false;
        GridView_IPS_SpecimenAll_List.HeaderRow.Cells[5].Visible = false;
        GridView_IPS_SpecimenAll_List.HeaderRow.Cells[7].Visible = false;
      }
      else
      {
        for (int rowIndex = GridView_IPS_SpecimenAll_List.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
          GridViewRow GridViewRow_Row = GridView_IPS_SpecimenAll_List.Rows[rowIndex];
          GridViewRow GridViewRow_PreviousRow = GridView_IPS_SpecimenAll_List.Rows[rowIndex + 1];

          if (GridViewRow_Row.Cells[0].Text == GridViewRow_PreviousRow.Cells[0].Text)
          {
            if (GridViewRow_Row.Cells[0].Text != "&nbsp;" && GridViewRow_PreviousRow.Cells[0].Text != "&nbsp;")
            {
              GridViewRow_Row.Cells[1].RowSpan = GridViewRow_PreviousRow.Cells[1].RowSpan < 2 ? 2 : GridViewRow_PreviousRow.Cells[1].RowSpan + 1;
              GridViewRow_Row.Cells[2].RowSpan = GridViewRow_PreviousRow.Cells[2].RowSpan < 2 ? 2 : GridViewRow_PreviousRow.Cells[2].RowSpan + 1;
              GridViewRow_Row.Cells[3].RowSpan = GridViewRow_PreviousRow.Cells[3].RowSpan < 2 ? 2 : GridViewRow_PreviousRow.Cells[3].RowSpan + 1;
              GridViewRow_Row.Cells[4].RowSpan = GridViewRow_PreviousRow.Cells[4].RowSpan < 2 ? 2 : GridViewRow_PreviousRow.Cells[4].RowSpan + 1;

              GridViewRow_PreviousRow.Cells[1].Visible = false;
              GridViewRow_PreviousRow.Cells[2].Visible = false;
              GridViewRow_PreviousRow.Cells[3].Visible = false;
              GridViewRow_PreviousRow.Cells[4].Visible = false;
            }
          }

          if (GridViewRow_Row.Cells[5].Text == GridViewRow_PreviousRow.Cells[5].Text)
          {
            if (GridViewRow_Row.Cells[5].Text != "&nbsp;" && GridViewRow_PreviousRow.Cells[5].Text != "&nbsp;")
            {
              GridViewRow_Row.Cells[6].RowSpan = GridViewRow_PreviousRow.Cells[6].RowSpan < 2 ? 2 : GridViewRow_PreviousRow.Cells[6].RowSpan + 1;

              GridViewRow_PreviousRow.Cells[6].Visible = false;
            }
          }

          if (GridViewRow_Row.Cells[7].Text == GridViewRow_PreviousRow.Cells[7].Text)
          {
            if (GridViewRow_Row.Cells[7].Text != "&nbsp;" && GridViewRow_PreviousRow.Cells[7].Text != "&nbsp;")
            {
              GridViewRow_Row.Cells[8].RowSpan = GridViewRow_PreviousRow.Cells[8].RowSpan < 2 ? 2 : GridViewRow_PreviousRow.Cells[8].RowSpan + 1;

              GridViewRow_PreviousRow.Cells[8].Visible = false;
            }
          }

          GridViewRow_PreviousRow.Cells[0].Visible = false;
          GridViewRow_PreviousRow.Cells[5].Visible = false;
          GridViewRow_PreviousRow.Cells[7].Visible = false;

          GridViewRow_Row.Cells[0].Visible = false;
          GridViewRow_Row.Cells[5].Visible = false;
          GridViewRow_Row.Cells[7].Visible = false;
        }

        GridView_IPS_SpecimenAll_List.HeaderRow.Cells[0].Visible = false;
        GridView_IPS_SpecimenAll_List.HeaderRow.Cells[5].Visible = false;
        GridView_IPS_SpecimenAll_List.HeaderRow.Cells[7].Visible = false;
      }
    } 

    protected void Button_CaptureNewSpecimen_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentSpecimen"), false);
    }

    public string GetSpecimenLink(object ips_Specimen_Id, object specimenStatus)
    {
      string FinalURL = "";
      if (ips_Specimen_Id != null && specimenStatus != null)
      {
        if (!string.IsNullOrEmpty(ips_Specimen_Id.ToString()) && !string.IsNullOrEmpty(specimenStatus.ToString()))
        {
          FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + ips_Specimen_Id) + "#CurrentSpecimen'>" + specimenStatus + "</a>";
        }
      }

      return FinalURL;
    }

    public string GetSpecimenResultLink(object ips_Specimen_Id, object ips_SpecimenResult_Id, object specimenResultLabNumber)
    {
      string FinalURL = "";
      if (ips_Specimen_Id != null && ips_SpecimenResult_Id != null && specimenResultLabNumber != null)
      {
        if (!string.IsNullOrEmpty(ips_Specimen_Id.ToString()) && !string.IsNullOrEmpty(ips_SpecimenResult_Id.ToString()) && !string.IsNullOrEmpty(specimenResultLabNumber.ToString()))
        {
          FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + ips_Specimen_Id) + "&IPSSpecimenResultId=" + ips_SpecimenResult_Id + "#CurrentSpecimenResult'>" + specimenResultLabNumber + "</a>";
        }
      }

      return FinalURL;
    }

    public string GetOrganismLink(object ips_Specimen_Id, object ips_SpecimenResult_Id, object ips_Organism_Id, object organism)
    {
      string FinalURL = "";
      if (ips_Specimen_Id != null && ips_SpecimenResult_Id != null && ips_Organism_Id != null && organism != null)
      {
        if (!string.IsNullOrEmpty(ips_Specimen_Id.ToString()) && !string.IsNullOrEmpty(ips_SpecimenResult_Id.ToString()) && !string.IsNullOrEmpty(ips_Organism_Id.ToString()) && !string.IsNullOrEmpty(organism.ToString()))
        {
          FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + ips_Specimen_Id) + "&IPSSpecimenResultId=" + ips_SpecimenResult_Id + "&IPSOrganismId=" + ips_Organism_Id + "#CurrentOrganism'>" + organism + "</a>";
        }
      }

      return FinalURL;
    }
    //---END--- --SpecimenAll--//
  }
}