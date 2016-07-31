using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_IPS_VisitDiagnosis : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName("37") + " : Visit Diagnosis", CultureInfo.CurrentCulture);
          Label_InfoHeading.Text = Convert.ToString("Visit and Infection Information", CultureInfo.CurrentCulture);
          Label_VisitDiagnosisHeading.Text = Convert.ToString("Visit Diagnosis", CultureInfo.CurrentCulture);

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
              TableVisitDiagnosis.Visible = true;

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

              ObjectDataSource_IPS_VisitDiagnosis.SelectParameters["facilityId"].DefaultValue = Session["FacilityId"].ToString();
              ObjectDataSource_IPS_VisitDiagnosis.SelectParameters["patientVisitNumber"].DefaultValue = Session["IPSVisitInformationVisitNumber"].ToString();

              Session.Remove("FacilityId");
              Session.Remove("FacilityFacilityCode");
              Session.Remove("IPSVisitInformationVisitNumber");
            }
            else
            {
              TableInfo.Visible = false;
              TableVisitDiagnosis.Visible = false;
            }
          }
          else
          {
            TableInfo.Visible = false;
            TableVisitDiagnosis.Visible = false;
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_IPS_VisitDiagnosis.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infection Prevention Surveillance", "5");
      }
    }

    private void SqlDataSourceSetup()
    {
      ObjectDataSource_IPS_VisitDiagnosis.SelectMethod = "DataPatient_EDW_IPS_CodingInformation";
      ObjectDataSource_IPS_VisitDiagnosis.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_IPS_VisitDiagnosis.SelectParameters.Clear();
      ObjectDataSource_IPS_VisitDiagnosis.SelectParameters.Add("facilityId", TypeCode.String, "");
      ObjectDataSource_IPS_VisitDiagnosis.SelectParameters.Add("patientVisitNumber", TypeCode.String, "");
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


    //--START-- --VisitDiagnosis--//
    protected void ObjectDataSource_IPS_VisitDiagnosis_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenTotalRecords.Text = ((DataTable)e.ReturnValue).Rows.Count.ToString(CultureInfo.CurrentCulture);
        Label_TopTotalRecords.Text = ((DataTable)e.ReturnValue).Rows.Count.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_VisitDiagnosis_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_VisitDiagnosis_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_TotalRecords = (Label)e.Row.FindControl("Label_TotalRecords");
          Label_TotalRecords.Text = Label_HiddenTotalRecords.Text;
        }
      }
    }

    protected void GridView_IPS_VisitDiagnosis_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          HiddenField HiddenField_CodeType = (HiddenField)e.Row.FindControl("HiddenField_CodeType");
          HiddenField HiddenField_Code = (HiddenField)e.Row.FindControl("HiddenField_Code");

          Session["IPSVisitDiagnosisId"] = "";
          string SQLStringVisitDiagnosis = "SELECT IPS_VisitDiagnosis_Id FROM Form_IPS_VisitDiagnosis WHERE IPS_Infection_Id = @IPS_Infection_Id AND IPS_VisitDiagnosis_CodeType = @IPS_VisitDiagnosis_CodeType AND IPS_VisitDiagnosis_Code = @IPS_VisitDiagnosis_Code";
          using (SqlCommand SqlCommand_VisitDiagnosis = new SqlCommand(SQLStringVisitDiagnosis))
          {
            SqlCommand_VisitDiagnosis.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_VisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_CodeType", HiddenField_CodeType.Value);
            SqlCommand_VisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_Code", HiddenField_Code.Value);
            DataTable DataTable_VisitDiagnosis;
            using (DataTable_VisitDiagnosis = new DataTable())
            {
              DataTable_VisitDiagnosis.Locale = CultureInfo.CurrentCulture;
              DataTable_VisitDiagnosis = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitDiagnosis).Copy();
              if (DataTable_VisitDiagnosis.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_VisitDiagnosis.Rows)
                {
                  Session["IPSVisitDiagnosisId"] = DataRow_Row["IPS_VisitDiagnosis_Id"];
                }
              }
            }
          }

          CheckBox CheckBox_Selected = (CheckBox)e.Row.FindControl("CheckBox_Selected");

          if (!string.IsNullOrEmpty(Session["IPSVisitDiagnosisId"].ToString()))
          {
            CheckBox_Selected.Checked = true;
          }
          else
          {
            CheckBox_Selected.Checked = false;
          }

          Session.Remove("IPSVisitDiagnosisId");
        }       
      }
    }

    protected void Button_Update_OnClick(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_IPS_VisitDiagnosis.Rows.Count; i++)
      {
        CheckBox CheckBox_Selected = (CheckBox)GridView_IPS_VisitDiagnosis.Rows[i].Cells[0].FindControl("CheckBox_Selected");
        HiddenField HiddenField_CodeType = (HiddenField)GridView_IPS_VisitDiagnosis.Rows[i].Cells[0].FindControl("HiddenField_CodeType");
        HiddenField HiddenField_Code = (HiddenField)GridView_IPS_VisitDiagnosis.Rows[i].Cells[0].FindControl("HiddenField_Code");
        HiddenField HiddenField_CodeDescription = (HiddenField)GridView_IPS_VisitDiagnosis.Rows[i].Cells[0].FindControl("HiddenField_CodeDescription");

        Session["IPSVisitDiagnosisId"] = "";
        string SQLStringVisitDiagnosis = "SELECT IPS_VisitDiagnosis_Id FROM Form_IPS_VisitDiagnosis WHERE IPS_Infection_Id = @IPS_Infection_Id AND IPS_VisitDiagnosis_CodeType = @IPS_VisitDiagnosis_CodeType AND IPS_VisitDiagnosis_Code = @IPS_VisitDiagnosis_Code";
        using (SqlCommand SqlCommand_VisitDiagnosis = new SqlCommand(SQLStringVisitDiagnosis))
        {
          SqlCommand_VisitDiagnosis.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
          SqlCommand_VisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_CodeType", HiddenField_CodeType.Value);
          SqlCommand_VisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_Code", HiddenField_Code.Value);
          DataTable DataTable_VisitDiagnosis;
          using (DataTable_VisitDiagnosis = new DataTable())
          {
            DataTable_VisitDiagnosis.Locale = CultureInfo.CurrentCulture;
            DataTable_VisitDiagnosis = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitDiagnosis).Copy();
            if (DataTable_VisitDiagnosis.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_VisitDiagnosis.Rows)
              {
                Session["IPSVisitDiagnosisId"] = DataRow_Row["IPS_VisitDiagnosis_Id"];
              }
            }
          }
        }

        if (CheckBox_Selected.Checked == true)
        {
          if (string.IsNullOrEmpty(Session["IPSVisitDiagnosisId"].ToString()))
          {
            string SQLStringInsertVisitDiagnosis = "INSERT INTO Form_IPS_VisitDiagnosis ( IPS_Infection_Id , IPS_VisitDiagnosis_CodeType , IPS_VisitDiagnosis_Code , IPS_VisitDiagnosis_Description ) VALUES ( @IPS_Infection_Id , @IPS_VisitDiagnosis_CodeType , @IPS_VisitDiagnosis_Code , @IPS_VisitDiagnosis_Description )";
            using (SqlCommand SqlCommand_InsertVisitDiagnosis = new SqlCommand(SQLStringInsertVisitDiagnosis))
            {
              SqlCommand_InsertVisitDiagnosis.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
              SqlCommand_InsertVisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_CodeType", HiddenField_CodeType.Value);
              SqlCommand_InsertVisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_Code", HiddenField_Code.Value);
              SqlCommand_InsertVisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_Description", HiddenField_CodeDescription.Value);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertVisitDiagnosis);
            }
          }
          else
          {
            string SQLStringUpdateVisitDiagnosis = "UPDATE Form_IPS_VisitDiagnosis SET IPS_VisitDiagnosis_Description = @IPS_VisitDiagnosis_Description WHERE IPS_VisitDiagnosis_Id = @IPS_VisitDiagnosis_Id";
            using (SqlCommand SqlCommand_UpdateVisitDiagnosis = new SqlCommand(SQLStringUpdateVisitDiagnosis))
            {
              SqlCommand_UpdateVisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_Description", HiddenField_CodeDescription.Value);
              SqlCommand_UpdateVisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_Id", Session["IPSVisitDiagnosisId"].ToString());
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateVisitDiagnosis);
            }
          }
        }
        else
        {
          if (!string.IsNullOrEmpty(Session["IPSVisitDiagnosisId"].ToString()))
          {
            string SQLStringDeleteVisitDiagnosis = "DELETE FROM Form_IPS_VisitDiagnosis WHERE IPS_VisitDiagnosis_Id = @IPS_VisitDiagnosis_Id";
            using (SqlCommand SqlCommand_DeleteVisitDiagnosis = new SqlCommand(SQLStringDeleteVisitDiagnosis))
            {
              SqlCommand_DeleteVisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_Id", Session["IPSVisitDiagnosisId"].ToString());
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteVisitDiagnosis);
            }
          }
        }

        Session.Remove("IPSVisitDiagnosisId");
      }

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#VisitDiagnosis"), false);
    }    
    //---END--- --VisitDiagnosis--//
  }
}