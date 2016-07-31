using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace InfoQuestForm
{
  public partial class Form_IPS_Theatre : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();
        
        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName("37") + " : Visit History", CultureInfo.CurrentCulture);
          Label_InfoHeading.Text = Convert.ToString("Visit and Infection Information", CultureInfo.CurrentCulture);
          Label_TheatreHeading.Text = Convert.ToString("Visit History", CultureInfo.CurrentCulture);

          if (Request.QueryString["IPSVisitInformationId"] != null && Request.QueryString["IPSInfectionId"] != null)
          {
            FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
            string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
            string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

            FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
            string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True" && IPSInfectionSiteInfectionIsActive == "True")
            {
              TableInfo.Visible = true;
              TableTheatre.Visible = true;

              Session["Facility_Id"] = "";
              Session["FacilityFacilityCode"] = "";
              Session["IPSVisitInformationVisitNumber"] = "";
              string SQLStringVisitInfo = "SELECT Facility_Id , Facility_FacilityCode , IPS_VisitInformation_VisitNumber FROM vForm_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id";
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
                      Session["FacilityId"] = DataRow_Row["Facility_Id"];
                      Session["FacilityFacilityCode"] = DataRow_Row["Facility_FacilityCode"];
                      Session["IPSVisitInformationVisitNumber"] = DataRow_Row["IPS_VisitInformation_VisitNumber"];
                    }
                  }
                }
              }

              ObjectDataSource_IPS_Theatre.SelectParameters["facilityId"].DefaultValue = Session["FacilityId"].ToString();
              ObjectDataSource_IPS_Theatre.SelectParameters["patientVisitNumber"].DefaultValue = Session["IPSVisitInformationVisitNumber"].ToString();

              Session.Remove("FacilityId");
              Session.Remove("FacilityFacilityCode");
              Session.Remove("IPSVisitInformationVisitNumber");
            }
            else
            {
              TableInfo.Visible = false;
              TableTheatre.Visible = false;
            }
          }
          else
          {
            TableInfo.Visible = false;
            TableTheatre.Visible = false;
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_IPS_Theatre.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infection Prevention Surveillance", "5");
      }
    }

    private void SqlDataSourceSetup()
    {
      ObjectDataSource_IPS_Theatre.SelectMethod = "DataPatient_EDW_IPS_TheatreInformation";
      ObjectDataSource_IPS_Theatre.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_IPS_Theatre.SelectParameters.Clear();
      ObjectDataSource_IPS_Theatre.SelectParameters.Add("facilityId", TypeCode.String, "");
      ObjectDataSource_IPS_Theatre.SelectParameters.Add("patientVisitNumber", TypeCode.String, "");
    }

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

    private class FromDataBase_InfectionSite
    {
      public string IPSInfectionSiteInfectionIsActive { get; set; }
    }

    private FromDataBase_InfectionSite GetInfectionSite()
    {
      FromDataBase_InfectionSite FromDataBase_InfectionSite_New = new FromDataBase_InfectionSite();

      string SQLStringInfection = "SELECT CASE WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4996 THEN 'True' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'False' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4996') THEN 'False' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'False' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4997') THEN 'False' ELSE 'True' END	AS IPS_Infection_Site_Infection_IsActive FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
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
              FromDataBase_InfectionSite_New.IPSInfectionSiteInfectionIsActive = DataRow_Row["IPS_Infection_Site_Infection_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_InfectionSite_New;
    }


    //--START-- --Theatre--//
    protected void ObjectDataSource_IPS_Theatre_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenTotalRecords.Text = ((DataTable)e.ReturnValue).Rows.Count.ToString(CultureInfo.CurrentCulture);
        Label_TopTotalRecords.Text = ((DataTable)e.ReturnValue).Rows.Count.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_Theatre_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }

      for (int i = 0; i < GridView_IPS_Theatre.Rows.Count; i++)
      {
        if (GridView_IPS_Theatre.Rows[i].RowType == DataControlRowType.DataRow)
        {
          HiddenField HiddenField_ServiceCategory = (HiddenField)GridView_IPS_Theatre.Rows[i].FindControl("HiddenField_ServiceCategory");
          HtmlTableRow SurgicalRow1 = (HtmlTableRow)GridView_IPS_Theatre.Rows[i].FindControl("SurgicalRow1");
          HtmlTableRow SurgicalRow2 = (HtmlTableRow)GridView_IPS_Theatre.Rows[i].FindControl("SurgicalRow2");
          HtmlTableRow SurgicalRow3 = (HtmlTableRow)GridView_IPS_Theatre.Rows[i].FindControl("SurgicalRow3");
          HtmlTableRow SurgicalRow4 = (HtmlTableRow)GridView_IPS_Theatre.Rows[i].FindControl("SurgicalRow4");
          HtmlTableRow InfectionHistory1 = (HtmlTableRow)GridView_IPS_Theatre.Rows[i].FindControl("InfectionHistory1");
          HtmlTableRow InfectionHistory2 = (HtmlTableRow)GridView_IPS_Theatre.Rows[i].FindControl("InfectionHistory2");
          HtmlTableRow InfectionHistory3 = (HtmlTableRow)GridView_IPS_Theatre.Rows[i].FindControl("InfectionHistory3");

          if (HiddenField_ServiceCategory.Value == "SURGICAL")
          {
            SurgicalRow1.Visible = true;
            SurgicalRow2.Visible = true;
            SurgicalRow3.Visible = true;
            SurgicalRow4.Visible = true;
            InfectionHistory1.Visible = false;
            InfectionHistory2.Visible = true;
            InfectionHistory3.Visible = true;
          }
          else
          {
            SurgicalRow1.Visible = false;
            SurgicalRow2.Visible = true;
            SurgicalRow3.Visible = false;
            SurgicalRow4.Visible = false;
            InfectionHistory1.Visible = false;
            InfectionHistory2.Visible = true;
            InfectionHistory3.Visible = true;
          }


          HiddenField HiddenField_FacilityCode = (HiddenField)GridView_IPS_Theatre.Rows[i].FindControl("HiddenField_FacilityCode");
          HiddenField HiddenField_VisitNumber = (HiddenField)GridView_IPS_Theatre.Rows[i].FindControl("HiddenField_VisitNumber");
          Button Button_VisitInfectionHistory = (Button)GridView_IPS_Theatre.Rows[i].FindControl("Button_VisitInfectionHistory");
          Button_VisitInfectionHistory.Visible = false;


          Int32 InfectionHistory = 0;
          string SQLStringInfectionHistory = "SELECT COUNT(1) AS InfectionHistory FROM vForm_IPS_Infection WHERE IPS_VisitInformation_Id IN ( SELECT IPS_VisitInformation_Id FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_VisitNumber = @IPS_VisitInformation_VisitNumber AND Facility_FacilityCode = @Facility_FacilityCode ) AND IPS_Infection_IsActive = 1";
          using (SqlCommand SqlCommand_InfectionHistory = new SqlCommand(SQLStringInfectionHistory))
          {
            SqlCommand_InfectionHistory.Parameters.AddWithValue("@IPS_VisitInformation_VisitNumber", HiddenField_VisitNumber.Value);
            SqlCommand_InfectionHistory.Parameters.AddWithValue("@Facility_FacilityCode", HiddenField_FacilityCode.Value);
            DataTable DataTable_InfectionHistory;
            using (DataTable_InfectionHistory = new DataTable())
            {
              DataTable_InfectionHistory.Locale = CultureInfo.CurrentCulture;
              DataTable_InfectionHistory = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionHistory).Copy();
              if (DataTable_InfectionHistory.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_InfectionHistory.Rows)
                {
                  InfectionHistory = Convert.ToInt32(DataRow_Row["InfectionHistory"], CultureInfo.CurrentCulture);
                }
              }
            }
          }

          if (InfectionHistory > 1)
          {
            Button_VisitInfectionHistory.Enabled = true;
            Button_VisitInfectionHistory.Text = Convert.ToString("Visit Infection History", CultureInfo.CurrentCulture);
          }
          else
          {
            Button_VisitInfectionHistory.Enabled = false;
            Button_VisitInfectionHistory.Text = Convert.ToString("No Visit Infection History", CultureInfo.CurrentCulture);
          }


          SqlDataSource SqlDataSource_IPS_Theatre_InfectionHistory_List = (SqlDataSource)GridView_IPS_Theatre.Rows[i].FindControl("SqlDataSource_IPS_Theatre_InfectionHistory_List");
          SqlDataSource_IPS_Theatre_InfectionHistory_List.SelectParameters["Facility_FacilityCode"].DefaultValue = HiddenField_FacilityCode.Value;
          SqlDataSource_IPS_Theatre_InfectionHistory_List.SelectParameters["IPS_VisitInformation_VisitNumber"].DefaultValue = HiddenField_VisitNumber.Value;
        }
      }
    }

    protected void GridView_IPS_Theatre_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_TotalRecords = (Label)e.Row.FindControl("Label_TotalRecords");
          Label_TotalRecords.Text = Label_HiddenTotalRecords.Text;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          SqlDataSource SqlDataSource_IPS_Theatre_InfectionHistory_List = (SqlDataSource)e.Row.FindControl("SqlDataSource_IPS_Theatre_InfectionHistory_List");
          SqlDataSource_IPS_Theatre_InfectionHistory_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_IPS_Theatre_InfectionHistory_List.SelectCommand = "spForm_Get_IPS_Theatre_InfectionHistory_List";
          SqlDataSource_IPS_Theatre_InfectionHistory_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
          SqlDataSource_IPS_Theatre_InfectionHistory_List.CancelSelectOnNullParameter = false;
          SqlDataSource_IPS_Theatre_InfectionHistory_List.SelectParameters.Clear();
          SqlDataSource_IPS_Theatre_InfectionHistory_List.SelectParameters.Add("Facility_FacilityCode", TypeCode.String, "");
          SqlDataSource_IPS_Theatre_InfectionHistory_List.SelectParameters.Add("IPS_VisitInformation_VisitNumber", TypeCode.Int32, "");
        }
      }
    }

    protected void GridView_IPS_Theatre_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          HiddenField HiddenField_FacilityCode = (HiddenField)e.Row.FindControl("HiddenField_FacilityCode");
          HiddenField HiddenField_AdmissionDate = (HiddenField)e.Row.FindControl("HiddenField_AdmissionDate");
          HiddenField HiddenField_DischargeDate = (HiddenField)e.Row.FindControl("HiddenField_DischargeDate");
          HiddenField HiddenField_FinalDiagnosisCode = (HiddenField)e.Row.FindControl("HiddenField_FinalDiagnosisCode");
          HiddenField HiddenField_VisitNumber = (HiddenField)e.Row.FindControl("HiddenField_VisitNumber");
          HiddenField HiddenField_ServiceCategory = (HiddenField)e.Row.FindControl("HiddenField_ServiceCategory");
          HiddenField HiddenField_VisitType = (HiddenField)e.Row.FindControl("HiddenField_VisitType");
          HiddenField HiddenField_Theatre = (HiddenField)e.Row.FindControl("HiddenField_Theatre");
          HiddenField HiddenField_TheatreTime = (HiddenField)e.Row.FindControl("HiddenField_TheatreTime");
          HiddenField HiddenField_ProcedureDate = (HiddenField)e.Row.FindControl("HiddenField_ProcedureDate");
          HiddenField HiddenField_ProcedureCode = (HiddenField)e.Row.FindControl("HiddenField_ProcedureCode");
          HiddenField HiddenField_TheatreInvoice = (HiddenField)e.Row.FindControl("HiddenField_TheatreInvoice");
          HiddenField HiddenField_Surgeon = (HiddenField)e.Row.FindControl("HiddenField_Surgeon");
          HiddenField HiddenField_Anaesthetist = (HiddenField)e.Row.FindControl("HiddenField_Anaesthetist");
          HiddenField HiddenField_Assistant = (HiddenField)e.Row.FindControl("HiddenField_Assistant");
          HiddenField HiddenField_WoundCategory = (HiddenField)e.Row.FindControl("HiddenField_WoundCategory");
          HiddenField HiddenField_ScrubNurse = (HiddenField)e.Row.FindControl("HiddenField_ScrubNurse");

          Session["IPSTheatreId"] = "";
          string SQLStringTheatre = "SELECT IPS_Theatre_Id FROM Form_IPS_Theatre WHERE IPS_Infection_Id = @IPS_Infection_Id AND IPS_Theatre_Facility = @IPS_Theatre_Facility AND IPS_Theatre_VisitNumber = @IPS_Theatre_VisitNumber AND IPS_Theatre_FinalDiagnosisCode = @IPS_Theatre_FinalDiagnosisCode AND IPS_Theatre_DateOfAdmission = @IPS_Theatre_DateOfAdmission AND IPS_Theatre_DateOfDischarge = @IPS_Theatre_DateOfDischarge AND IPS_Theatre_ProcedureDate = @IPS_Theatre_ProcedureDate AND IPS_Theatre_TheatreInvoice = @IPS_Theatre_TheatreInvoice AND IPS_Theatre_Theatre = @IPS_Theatre_Theatre AND IPS_Theatre_TheatreTime = @IPS_Theatre_TheatreTime AND IPS_Theatre_Surgeon = @IPS_Theatre_Surgeon AND IPS_Theatre_Anaesthetist = @IPS_Theatre_Anaesthetist AND IPS_Theatre_Assistant = @IPS_Theatre_Assistant AND IPS_Theatre_ProcedureCode = @IPS_Theatre_ProcedureCode AND IPS_Theatre_ScrubNurse = @IPS_Theatre_ScrubNurse AND IPS_Theatre_WoundCategory = @IPS_Theatre_WoundCategory AND IPS_Theatre_ServiceCategory = @IPS_Theatre_ServiceCategory";
          using (SqlCommand SqlCommand_Theatre = new SqlCommand(SQLStringTheatre))
          {
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Facility", HiddenField_FacilityCode.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_VisitNumber", HiddenField_VisitNumber.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_FinalDiagnosisCode", HiddenField_FinalDiagnosisCode.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_DateOfAdmission", HiddenField_AdmissionDate.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_DateOfDischarge", HiddenField_DischargeDate.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_ProcedureDate", HiddenField_ProcedureDate.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_TheatreInvoice", HiddenField_TheatreInvoice.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Theatre", HiddenField_Theatre.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_TheatreTime", HiddenField_TheatreTime.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Surgeon", HiddenField_Surgeon.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Anaesthetist", HiddenField_Anaesthetist.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Assistant", HiddenField_Assistant.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_ProcedureCode", HiddenField_ProcedureCode.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_ScrubNurse", HiddenField_ScrubNurse.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_WoundCategory", HiddenField_WoundCategory.Value);
            SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_ServiceCategory", HiddenField_ServiceCategory.Value + " (" + HiddenField_VisitType.Value + ")");
            DataTable DataTable_Theatre;
            using (DataTable_Theatre = new DataTable())
            {
              DataTable_Theatre.Locale = CultureInfo.CurrentCulture;
              DataTable_Theatre = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Theatre).Copy();
              if (DataTable_Theatre.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Theatre.Rows)
                {
                  Session["IPSTheatreId"] = DataRow_Row["IPS_Theatre_Id"];
                }
              }
            }
          }

          CheckBox CheckBox_Selected = (CheckBox)e.Row.FindControl("CheckBox_Selected");

          if (!string.IsNullOrEmpty(Session["IPSTheatreId"].ToString()))
          {
            CheckBox_Selected.Checked = true;
          }
          else
          {
            CheckBox_Selected.Checked = false;
          }

          Session.Remove("IPSTheatreId");
        }
      }
    }

    protected void Button_VisitInfectionHistory_OnClick(object sender, EventArgs e)
    {
      Button Button_VisitInfectionHistory = (Button)sender;
      GridViewRow GridViewRow_IPS_Theatre = (GridViewRow)Button_VisitInfectionHistory.NamingContainer;

      HiddenField HiddenField_VisitNumber = (HiddenField)GridViewRow_IPS_Theatre.FindControl("HiddenField_VisitNumber");

      ScriptManager.RegisterStartupScript(UpdatePanel_IPS_Theatre, this.GetType(), "Visit Infection History", "FormPatientInfectionHistory('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance History", "Form_IPS_InfectionHistory.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSVisitInformationVisitNumber=" + HiddenField_VisitNumber.Value.ToString() + "") + "')", true);
    }

    protected void Button_Update_OnClick(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_IPS_Theatre.Rows.Count; i++)
      {
        CheckBox CheckBox_Selected = (CheckBox)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("CheckBox_Selected");

        HiddenField HiddenField_FacilityCode = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_FacilityCode");
        HiddenField HiddenField_AdmissionDate = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_AdmissionDate");
        HiddenField HiddenField_DischargeDate = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_DischargeDate");
        HiddenField HiddenField_FinalDiagnosisCode = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_FinalDiagnosisCode");
        HiddenField HiddenField_FinalDiagnosisDescription = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_FinalDiagnosisDescription");
        HiddenField HiddenField_VisitNumber = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_VisitNumber");
        HiddenField HiddenField_ServiceCategory = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_ServiceCategory");
        HiddenField HiddenField_VisitType = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_VisitType");

        HiddenField HiddenField_Theatre = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_Theatre");
        HiddenField HiddenField_TheatreTime = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_TheatreTime");
        HiddenField HiddenField_ProcedureDate = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_ProcedureDate");
        HiddenField HiddenField_ProcedureCode = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_ProcedureCode");
        HiddenField HiddenField_ProcedureDescription = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_ProcedureDescription");
        HiddenField HiddenField_TheatreInvoice = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_TheatreInvoice");
        HiddenField HiddenField_Surgeon = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_Surgeon");
        HiddenField HiddenField_Anaesthetist = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_Anaesthetist");
        HiddenField HiddenField_Assistant = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_Assistant");
        HiddenField HiddenField_WoundCategory = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_WoundCategory");
        HiddenField HiddenField_ScrubNurse = (HiddenField)GridView_IPS_Theatre.Rows[i].Cells[0].FindControl("HiddenField_ScrubNurse");

        Session["IPSTheatreId"] = "";
        string SQLStringTheatre = "SELECT IPS_Theatre_Id FROM Form_IPS_Theatre WHERE IPS_Infection_Id = @IPS_Infection_Id AND IPS_Theatre_Facility = @IPS_Theatre_Facility AND IPS_Theatre_VisitNumber = @IPS_Theatre_VisitNumber AND IPS_Theatre_FinalDiagnosisCode = @IPS_Theatre_FinalDiagnosisCode AND IPS_Theatre_DateOfAdmission = @IPS_Theatre_DateOfAdmission AND IPS_Theatre_DateOfDischarge = @IPS_Theatre_DateOfDischarge AND IPS_Theatre_ProcedureDate = @IPS_Theatre_ProcedureDate AND IPS_Theatre_TheatreInvoice = @IPS_Theatre_TheatreInvoice AND IPS_Theatre_Theatre = @IPS_Theatre_Theatre AND IPS_Theatre_TheatreTime = @IPS_Theatre_TheatreTime AND IPS_Theatre_Surgeon = @IPS_Theatre_Surgeon AND IPS_Theatre_Anaesthetist = @IPS_Theatre_Anaesthetist AND IPS_Theatre_Assistant = @IPS_Theatre_Assistant AND IPS_Theatre_ProcedureCode = @IPS_Theatre_ProcedureCode AND IPS_Theatre_ScrubNurse = @IPS_Theatre_ScrubNurse AND IPS_Theatre_WoundCategory = @IPS_Theatre_WoundCategory AND IPS_Theatre_ServiceCategory = @IPS_Theatre_ServiceCategory";
        using (SqlCommand SqlCommand_Theatre = new SqlCommand(SQLStringTheatre))
        {
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Facility", HiddenField_FacilityCode.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_VisitNumber", HiddenField_VisitNumber.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_FinalDiagnosisCode", HiddenField_FinalDiagnosisCode.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_DateOfAdmission", HiddenField_AdmissionDate.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_DateOfDischarge", HiddenField_DischargeDate.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_ProcedureDate", HiddenField_ProcedureDate.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_TheatreInvoice", HiddenField_TheatreInvoice.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Theatre", HiddenField_Theatre.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_TheatreTime", HiddenField_TheatreTime.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Surgeon", HiddenField_Surgeon.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Anaesthetist", HiddenField_Anaesthetist.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_Assistant", HiddenField_Assistant.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_ProcedureCode", HiddenField_ProcedureCode.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_ScrubNurse", HiddenField_ScrubNurse.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_WoundCategory", HiddenField_WoundCategory.Value);
          SqlCommand_Theatre.Parameters.AddWithValue("@IPS_Theatre_ServiceCategory", HiddenField_ServiceCategory.Value + " (" + HiddenField_VisitType.Value + ")");
          DataTable DataTable_Theatre;
          using (DataTable_Theatre = new DataTable())
          {
            DataTable_Theatre.Locale = CultureInfo.CurrentCulture;
            DataTable_Theatre = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Theatre).Copy();
            if (DataTable_Theatre.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Theatre.Rows)
              {
                Session["IPSTheatreId"] = DataRow_Row["IPS_Theatre_Id"];
              }
            }
          }
        }

        if (CheckBox_Selected.Checked == true)
        {
          if (string.IsNullOrEmpty(Session["IPSTheatreId"].ToString()))
          {
            string SQLStringInsertTheatre = "INSERT INTO Form_IPS_Theatre ( IPS_Infection_Id , IPS_Theatre_Facility , IPS_Theatre_VisitNumber , IPS_Theatre_FinalDiagnosisCode , IPS_Theatre_FinalDiagnosisDescription , IPS_Theatre_DateOfAdmission , IPS_Theatre_DateOfDischarge , IPS_Theatre_ProcedureDate , IPS_Theatre_TheatreInvoice , IPS_Theatre_Theatre , IPS_Theatre_TheatreTime , IPS_Theatre_Surgeon , IPS_Theatre_Anaesthetist , IPS_Theatre_Assistant , IPS_Theatre_ProcedureCode , IPS_Theatre_ProcedureDescription , IPS_Theatre_ScrubNurse , IPS_Theatre_WoundCategory , IPS_Theatre_ServiceCategory ) VALUES ( @IPS_Infection_Id , @IPS_Theatre_Facility , @IPS_Theatre_VisitNumber , @IPS_Theatre_FinalDiagnosisCode , @IPS_Theatre_FinalDiagnosisDescription , @IPS_Theatre_DateOfAdmission , @IPS_Theatre_DateOfDischarge , @IPS_Theatre_ProcedureDate , @IPS_Theatre_TheatreInvoice , @IPS_Theatre_Theatre , @IPS_Theatre_TheatreTime , @IPS_Theatre_Surgeon , @IPS_Theatre_Anaesthetist , @IPS_Theatre_Assistant , @IPS_Theatre_ProcedureCode , @IPS_Theatre_ProcedureDescription , @IPS_Theatre_ScrubNurse , @IPS_Theatre_WoundCategory , @IPS_Theatre_ServiceCategory )";
            using (SqlCommand SqlCommand_InsertTheatre = new SqlCommand(SQLStringInsertTheatre))
            {
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_Facility", HiddenField_FacilityCode.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_VisitNumber", HiddenField_VisitNumber.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_FinalDiagnosisCode", HiddenField_FinalDiagnosisCode.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_FinalDiagnosisDescription", HiddenField_FinalDiagnosisDescription.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_DateOfAdmission", HiddenField_AdmissionDate.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_DateOfDischarge", HiddenField_DischargeDate.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_ProcedureDate", HiddenField_ProcedureDate.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_TheatreInvoice", HiddenField_TheatreInvoice.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_Theatre", HiddenField_Theatre.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_TheatreTime", HiddenField_TheatreTime.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_Surgeon", HiddenField_Surgeon.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_Anaesthetist", HiddenField_Anaesthetist.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_Assistant", HiddenField_Assistant.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_ProcedureCode", HiddenField_ProcedureCode.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_ProcedureDescription", HiddenField_ProcedureDescription.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_ScrubNurse", HiddenField_ScrubNurse.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_WoundCategory", HiddenField_WoundCategory.Value);
              SqlCommand_InsertTheatre.Parameters.AddWithValue("@IPS_Theatre_ServiceCategory", HiddenField_ServiceCategory.Value + " (" + HiddenField_VisitType.Value + ")");
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertTheatre);
            }
          }
          else
          {
            string SQLStringUpdateTheatre = "UPDATE Form_IPS_Theatre SET IPS_Theatre_FinalDiagnosisDescription = @IPS_Theatre_FinalDiagnosisDescription , IPS_Theatre_ProcedureDescription = @IPS_Theatre_ProcedureDescription WHERE IPS_Theatre_Id = @IPS_Theatre_Id";
            using (SqlCommand SqlCommand_UpdateTheatre = new SqlCommand(SQLStringUpdateTheatre))
            {
              SqlCommand_UpdateTheatre.Parameters.AddWithValue("@IPS_Theatre_FinalDiagnosisDescription", HiddenField_FinalDiagnosisDescription.Value);
              SqlCommand_UpdateTheatre.Parameters.AddWithValue("@IPS_Theatre_ProcedureDescription", HiddenField_ProcedureDescription.Value);
              SqlCommand_UpdateTheatre.Parameters.AddWithValue("@IPS_Theatre_Id", Session["IPSTheatreId"].ToString());
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateTheatre);
            }
          }
        }
        else
        {
          if (!string.IsNullOrEmpty(Session["IPSTheatreId"].ToString()))
          {
            string SQLStringDeleteTheatre = "DELETE FROM Form_IPS_Theatre WHERE IPS_Theatre_Id = @IPS_Theatre_Id";
            using (SqlCommand SqlCommand_DeleteTheatre = new SqlCommand(SQLStringDeleteTheatre))
            {
              SqlCommand_DeleteTheatre.Parameters.AddWithValue("@IPS_Theatre_Id", Session["IPSTheatreId"].ToString());
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteTheatre);
            }
          }
        }

        Session.Remove("IPSTheatreId");
      }

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#Theatre"), false);
    }

    public static string GetFacilityName(object facilityCode)
    {
      string FacilityName = "";

      if (facilityCode != null)
      {
        string FacilityFacilityDisplayName = "";
        string SQLStringFacility = "SELECT Facility_FacilityDisplayName FROM vAdministration_Facility_All WHERE Facility_FacilityCode = @Facility_FacilityCode";
        using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
        {
          SqlCommand_Facility.Parameters.AddWithValue("@Facility_FacilityCode", facilityCode);
          DataTable DataTable_Facility;
          using (DataTable_Facility = new DataTable())
          {
            DataTable_Facility.Locale = CultureInfo.CurrentCulture;
            DataTable_Facility = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
            if (DataTable_Facility.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Facility.Rows)
              {
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              }
            }
          }
        }

        FacilityName = FacilityFacilityDisplayName;
      }

      return FacilityName;
    }

    public static string GetFinalDiagnosis(object finalDiagnosisCode, object finalDiagnosisDescription)
    {
      string FinalDiagnosis = "";

      if (finalDiagnosisDescription != null)
      {
        if (finalDiagnosisCode == null || string.IsNullOrEmpty(finalDiagnosisCode.ToString()))
        {
          FinalDiagnosis = finalDiagnosisDescription.ToString();
        }
        else
        {
          FinalDiagnosis = finalDiagnosisCode.ToString() + " : " + finalDiagnosisDescription.ToString();
        }
      }

      return FinalDiagnosis;
    }

    public static string GetVisitType(object serviceCategory, object visitType)
    {
      string VisitType = "";

      if (serviceCategory != null && visitType != null)
      {
        VisitType = serviceCategory.ToString() + " (" + visitType.ToString() + ")";
      }

      return VisitType;
    }

    public static string GetProcedure(object procedureCode, object procedureDescription)
    {
      string Procedure = "";

      if (procedureDescription != null)
      {
        if (procedureCode == null || string.IsNullOrEmpty(procedureCode.ToString()))
        {
          Procedure = procedureDescription.ToString();
        }
        else
        {
          Procedure = procedureCode.ToString() + " : " + procedureDescription.ToString();
        }
      }

      return Procedure;
    }


    protected void GridView_IPS_Theatre_InfectionHistory_List_PreRender(object sender, EventArgs e)
    {
    }

    protected void GridView_IPS_Theatre_InfectionHistory_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }
    //---END--- --Theatre--//
  }
}