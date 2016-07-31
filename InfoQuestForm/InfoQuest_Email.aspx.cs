using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.Permissions;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class InfoQuest_Email : InfoQuestWCF.Override_SystemWebUIPage
  {
    private Dictionary<string, string> FormIdHandler = new Dictionary<string, string>();
    private Dictionary<string, string> SqlStringFacilityIdPrimaryHandler = new Dictionary<string, string>();
    private Dictionary<string, string> EmailTemplateHandler = new Dictionary<string, string>();
    private Dictionary<string, string> SqlStringFormDetailHandler = new Dictionary<string, string>();

    private string EmailMessage = "";
    private string TextBoxDescription = "";
    private string EmailTemplate = "";
    private string URLAuthority = "";
    private string FormName = "";
    private string FirstNameSendFrom = "";
    private string LastNameSendFrom = "";
    private string FirstNameSendTo = "";
    private string LastNameSendTo = "";
    private string BodyString = "";
    private string HeaderString = "";
    private string FooterString = "";
    private string EmailBody = "";

    // TODO : New Form : InfoQuest_Email : Add new SQLStringFormDetailValue() variables to Page
    private string FacilityId = "";
    private string FacilityFacilityDisplayName = "";
    private string PatientVisitNumber = "";
    private string QMSReviewId = "";
    private string QMSReviewDate = "";
    private string QMSReviewFindingsSystem = "";
    private string QMSReviewFindingsMeasurementCriteria = "";
    private string QMSReviewFindingsCategory = "";
    private string CLAId = "";
    private string CLADate = "";
    private string CLAFindingsSystem = "";
    private string CLAFindingsMeasurementCriteria = "";
    private string CLAFindingsCategory = "";
    private string OHAId = "";
    private string OHADate = "";
    private string OHAFindingsSystem = "";
    private string OHAFindingsMeasurementCriteria = "";
    private string OHAFindingsCategory = "";
    private string MHSPeriod = "";
    private string MHSFYPeriod = "";
    private string MPSPeriod = "";
    private string MPSFYPeriod = "";
    private string IncidentReportNumber = "";
    private string AlertReportNumber = "";
    private string NewProductReportNumber = "";
    private string CRMId = "";
    private string CRMReportNumber = "";
    private string CRMTypeName = "";
    private string pkiInfectionFormID = "";
    private string sPatientVisitNumber = "";
    private string sReportNumber = "";
    private string IPSVisitInformationId = "";
    private string IPSInfectionId = "";
    private string IPSInfectionReportNumber = "";
    private string MBCVisitInformationId = "";
    private string MBCBundlesId = "";
    private string MBCBundlesReportNumber = "";
    private string ASIVisitInformationId = "";
    private string ASIInterventionId = "";
    private string ASIInterventionReportNumber = "";
    private string HQAFindingFunctionName = "";
    private string HQAFindingFindingNo = "";
    private string HQAFindingId = "";
    private string TransparencyRegisterEmployeeNumber = "";
    private string TransparencyRegisterFirstName = "";
    private string TransparencyRegisterLastName = "";
    private string SustainabilityManagementPeriod = "";
    private string SustainabilityManagementFYPeriod = "";
    private string MOHSPeriod = "";
    private string MOHSFYPeriod = "";
    private string LoadedSurveysName = "";
    private string LoadedSurveysFY = "";
    private string CreatedSurveysName = "";
    private string VTEVisitInformationId = "";
    private string VTEAssesmentsId = "";
    private string VTEAssesmentsReportNumber = "";
    private string PCMTIId = "";
    private string PCMInterventionId = "";
    private string PCMTIReportNumber = "";
    private string PCMInterventionInterventionName = "";
    private string PCMPTId = "";
    private string PCMPTReportNumber = "";


    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

        SqlDataSourceSetup();

        if (Request.QueryString["EmailPage"] == null || Request.QueryString["EmailValue"] == null)
        {
          TableEmail.Visible = false;

          SqlDataSource_InfoQuest_Email.SelectParameters["Facility_Id_Primary"].DefaultValue = "0";
          SqlDataSource_InfoQuest_Email.SelectParameters["Facility_Id_Secondary"].DefaultValue = "0";
          SqlDataSource_InfoQuest_Email.SelectParameters["Form_Id"].DefaultValue = "0";
        }
        else
        {
          string FormIdValue = FormId(Request.QueryString["EmailPage"]);
          string SQLStringFacilityIdPrimaryValue = SqlStringFacilityIdPrimary(Request.QueryString["EmailPage"]);

          if (string.IsNullOrEmpty(FormIdValue) || string.IsNullOrEmpty(SQLStringFacilityIdPrimaryValue))
          {
            TableEmail.Visible = false;

            SqlDataSource_InfoQuest_Email.SelectParameters["Facility_Id_Primary"].DefaultValue = "0";
            SqlDataSource_InfoQuest_Email.SelectParameters["Facility_Id_Secondary"].DefaultValue = "0";
            SqlDataSource_InfoQuest_Email.SelectParameters["Form_Id"].DefaultValue = "0";
          }
          else
          {
            TableEmail.Visible = true;

            string FacilityIdPrimaryValue = "0";
            using (SqlCommand SqlCommand_FacilityId = new SqlCommand(SQLStringFacilityIdPrimaryValue))
            {
              SqlCommand_FacilityId.Parameters.AddWithValue("@EmailValue", Request.QueryString["EmailValue"]);
              DataTable DataTable_FacilityId;
              using (DataTable_FacilityId = new DataTable())
              {
                DataTable_FacilityId.Locale = CultureInfo.CurrentCulture;
                DataTable_FacilityId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FacilityId).Copy();
                if (DataTable_FacilityId.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_FacilityId.Rows)
                  {
                    FacilityIdPrimaryValue = DataRow_Row["Facility_Id"].ToString();
                  }
                }
              }
            }

            string FacilityIdSecondaryValue = "0";
            if (FormIdValue == "36")
            {
              FromDataBase_FormCRM_RoutedFacility FromDataBase_FormCRM_RoutedFacility_Current = GetFormCRMRoutedFacility();
              FacilityIdSecondaryValue = FromDataBase_FormCRM_RoutedFacility_Current.RoutedFacility;
            }

            string FormNameValue = InfoQuestWCF.InfoQuest_All.All_FormName(FormIdValue);            

            SqlDataSource_InfoQuest_Email.SelectParameters["Facility_Id_Primary"].DefaultValue = FacilityIdPrimaryValue;
            SqlDataSource_InfoQuest_Email.SelectParameters["Facility_Id_Secondary"].DefaultValue = FacilityIdSecondaryValue;
            SqlDataSource_InfoQuest_Email.SelectParameters["Form_Id"].DefaultValue = FormIdValue;

            Label_FormName.Text = FormNameValue;

            if (FormIdValue == "1" || FormIdValue == "2" || FormIdValue == "44")
            {
              EnterEmailAddress.Visible = true;
            }
            else
            {
              EnterEmailAddress.Visible = false;
            }

            FormIdValue = "";
            SQLStringFacilityIdPrimaryValue = "";
            FacilityIdPrimaryValue = "";
            FacilityIdSecondaryValue = "";
            FormNameValue = "";
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id = @Form_Id)";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Form_Id", FormId(Request.QueryString["EmailPage"]));

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          string FormIdValue = FormId(Request.QueryString["EmailPage"]);

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
        ((Label)PageUpdateProgress_Email.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_InfoQuest_Email.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_InfoQuest_Email.SelectCommand = "spAdministration_Execute_Email";
      SqlDataSource_InfoQuest_Email.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_InfoQuest_Email.SelectParameters.Clear();
      SqlDataSource_InfoQuest_Email.SelectParameters.Add("Facility_Id_Primary", TypeCode.String, "");
      SqlDataSource_InfoQuest_Email.SelectParameters.Add("Facility_Id_Secondary", TypeCode.String, "");
      SqlDataSource_InfoQuest_Email.SelectParameters.Add("Form_Id", TypeCode.String, "");
    }


    private class FromDataBase_FormCRM_RoutedFacility
    {
      public string RoutedFacility { get; set; }
    }

    private FromDataBase_FormCRM_RoutedFacility GetFormCRMRoutedFacility()
    {
      FromDataBase_FormCRM_RoutedFacility FromDataBase_FormCRM_RoutedFacility_New = new FromDataBase_FormCRM_RoutedFacility();

      string SQLStringCRMRouteComplete = "SELECT TOP 1 CASE ISNULL(vForm_CRM_Route.CRM_Route_Complete,'1') WHEN '1' THEN 0 ELSE vForm_CRM_Route.Facility_Id END AS RoutedFacility FROM Form_CRM LEFT JOIN vForm_CRM_Route ON Form_CRM.CRM_Id = vForm_CRM_Route.CRM_Id WHERE Form_CRM.CRM_Id = @EmailValue ORDER BY CRM_Route_CreatedDate DESC";
      using (SqlCommand SqlCommand_CRMRouteComplete = new SqlCommand(SQLStringCRMRouteComplete))
      {
        SqlCommand_CRMRouteComplete.Parameters.AddWithValue("@EmailValue", Request.QueryString["EmailValue"]);
        DataTable DataTable_CRMRouteComplete;
        using (DataTable_CRMRouteComplete = new DataTable())
        {
          DataTable_CRMRouteComplete.Locale = CultureInfo.CurrentCulture;
          DataTable_CRMRouteComplete = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMRouteComplete).Copy();
          if (DataTable_CRMRouteComplete.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CRMRouteComplete.Rows)
            {
              FromDataBase_FormCRM_RoutedFacility_New.RoutedFacility = DataRow_Row["RoutedFacility"].ToString();
            }
          }
        }
      }

      return FromDataBase_FormCRM_RoutedFacility_New;
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetEmailAddressList(string prefixText, int count, string contextKey)
    {
      if (string.IsNullOrEmpty(contextKey))
      {
        List<string> List_Items = new List<string>(count);

        DataTable DataTable_EmailAddress;
        using (DataTable_EmailAddress = new DataTable())
        {
          DataTable_EmailAddress.Locale = CultureInfo.CurrentCulture;
          DataTable_EmailAddress = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindAll_InfoQuest_Email_GetEmailAddressList(prefixText).Copy();
          if (DataTable_EmailAddress.Rows.Count > 0)
          {
            Int32 Count = 1;
            foreach (DataRow DataRow_Row in DataTable_EmailAddress.Rows)
            {
              if (DataRow_Row.Table.Columns["Email"] != null)
              {
                if (Count <= 10)
                {
                  string WorkEmail = DataRow_Row["Email"].ToString();
                  List_Items.Add(WorkEmail);
                }

                Count = Count + 1;
              }
            }
          }
          else
          {
            List_Items.Clear();
          }
        }

        return List_Items.ToArray();
      }
      else
      {
        return null;
      }
    }

    protected void TextBox_EmailAddress_TextChanged(object sender, EventArgs e)
    {
      string EmailAddressErrorMessage = "";

      if (string.IsNullOrEmpty(TextBox_EmailAddress.Text))
      {
        Page.MaintainScrollPositionOnPostBack = true;
        EmailAddressErrorMessage = "";
      }
      else
      {
        Page.MaintainScrollPositionOnPostBack = true;

        string EmailTextBox = TextBox_EmailAddress.Text.ToString();
        EmailTextBox = EmailTextBox.Replace(";", ",");
        EmailTextBox = EmailTextBox.Replace(":", ",");

        string EmailTextBoxSplit = EmailTextBox;
        string[] EmailTextBoxSplitEmails = EmailTextBoxSplit.Split(',');

        foreach (string EmailTextBoxSplitEmail in EmailTextBoxSplitEmails)
        {
          InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
          string ValidEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailTextBoxSplitEmail);

          if (ValidEmailAddress == "No")
          {
            EmailAddressErrorMessage = EmailAddressErrorMessage + "Email Address " + EmailTextBoxSplitEmail + " is not a valid Email address<br />";
            ToolkitScriptManager_EmailAddress.SetFocus(TextBox_EmailAddress);
          }
        }

        TextBox_EmailAddress.Text = Convert.ToString(EmailTextBox, CultureInfo.CurrentCulture);
      }

      Label_EmailAddressError.Text = Convert.ToString(EmailAddressErrorMessage, CultureInfo.CurrentCulture);
    }

    protected void Button_SendEmail_Click(object sender, EventArgs e)
    {
      if (Request.QueryString["EmailPage"] != null && Request.QueryString["EmailValue"] != null)
      {
        string EmptyEmailAddress = "Yes";
        if (!string.IsNullOrEmpty(TextBox_EmailAddress.Text.ToString()))
        {
          EmptyEmailAddress = "No";
        }

        for (int i = 0; i < CheckBoxList_EmailAddress.Items.Count; i++)
        {
          if (CheckBoxList_EmailAddress.Items[i].Selected)
          {
            EmptyEmailAddress = "No";
          }
        }

        if (EmptyEmailAddress == "Yes")
        {
          EmailMessage = EmailMessage + "<strong style='color:#B0262e'>No email address provided to send email to<strong><br />";
        }
        else
        {
          SendEmail_TextBox();

          SendEmail_CheckBoxList();
        }

        Label_EmailMessage.Text = Convert.ToString(EmailMessage, CultureInfo.CurrentCulture);
      }
    }


    // TODO : New Form : InfoQuest_Email : Add new Form to FormIdHandlers()
    protected void FormIdHandlers()
    {
      FormIdHandler.Add("Form_FIMFAM", "25");
      FormIdHandler.Add("Form_Isidima", "27");
      FormIdHandler.Add("Form_AMSPI", "29");
      FormIdHandler.Add("Form_PROMS", "30");
      FormIdHandler.Add("Form_QMSReview_Findings", "31");
      FormIdHandler.Add("Form_QMSReview_Summary", "31");
      FormIdHandler.Add("Form_CollegeLearningAudit_Findings", "49");
      FormIdHandler.Add("Form_CollegeLearningAudit_Summary", "49");
      FormIdHandler.Add("Form_OccupationalHealthAudit_Findings", "48");
      FormIdHandler.Add("Form_OccupationalHealthAudit_Summary", "48");
      FormIdHandler.Add("Form_MonthlyHospitalStatistics", "5");
      FormIdHandler.Add("Form_MonthlyPharmacyStatistics", "32");
      FormIdHandler.Add("Form_Incident", "1");
      FormIdHandler.Add("Form_Alert", "2");
      FormIdHandler.Add("Form_Pharmacy_NewProduct", "33");
      FormIdHandler.Add("Form_MHQ14", "34");
      FormIdHandler.Add("Form_BundleCompliance", "17");
      FormIdHandler.Add("Form_RehabBundleCompliance", "35");
      FormIdHandler.Add("Form_CRM", "36");
      FormIdHandler.Add("Form_InfectionPrevention", "37");
      FormIdHandler.Add("Form_IPS", "37");
      FormIdHandler.Add("Form_MedicationBundleCompliance", "38");
      FormIdHandler.Add("Form_AntimicrobialStewardshipIntervention", "39");
      FormIdHandler.Add("Form_HeadOfficeQualityAudit", "40");
      FormIdHandler.Add("Form_TransparencyRegister", "44");
      FormIdHandler.Add("Form_SustainabilityManagement", "45");
      FormIdHandler.Add("Form_MonthlyOccupationalHealthStatistics", "46");
      FormIdHandler.Add("Form_PharmacySurveys", "47");
      FormIdHandler.Add("Form_VTE", "51");
      FormIdHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention", "52");
      FormIdHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime", "52");
    }

    // TODO : New Form : InfoQuest_Email : Add new Form to EmailTemplateHandlers()
    protected void EmailTemplateHandlers()
    {
      EmailTemplateHandler.Add("Form_FIMFAM", "36");
      EmailTemplateHandler.Add("Form_Isidima", "39");
      EmailTemplateHandler.Add("Form_AMSPI", "27");
      EmailTemplateHandler.Add("Form_PROMS", "51");
      EmailTemplateHandler.Add("Form_QMSReview_Findings", "52");
      EmailTemplateHandler.Add("Form_QMSReview_Summary", "53");
      EmailTemplateHandler.Add("Form_CollegeLearningAudit_Findings", "81");
      EmailTemplateHandler.Add("Form_CollegeLearningAudit_Summary", "82");
      EmailTemplateHandler.Add("Form_OccupationalHealthAudit_Findings", "83");
      EmailTemplateHandler.Add("Form_OccupationalHealthAudit_Summary", "84");
      EmailTemplateHandler.Add("Form_MonthlyHospitalStatistics", "41");
      EmailTemplateHandler.Add("Form_MonthlyPharmacyStatistics", "42");
      EmailTemplateHandler.Add("Form_Incident", "38");
      EmailTemplateHandler.Add("Form_Alert", "26");
      EmailTemplateHandler.Add("Form_Pharmacy_NewProduct", "46");
      EmailTemplateHandler.Add("Form_MHQ14", "40");
      EmailTemplateHandler.Add("Form_BundleCompliance", "28");
      EmailTemplateHandler.Add("Form_RehabBundleCompliance", "54");
      EmailTemplateHandler.Add("Form_CRM", "32");
      EmailTemplateHandler.Add("Form_InfectionPrevention", "37");
      EmailTemplateHandler.Add("Form_IPS", "56");
      EmailTemplateHandler.Add("Form_MedicationBundleCompliance", "61");
      EmailTemplateHandler.Add("Form_AntimicrobialStewardshipIntervention", "69");
      EmailTemplateHandler.Add("Form_HeadOfficeQualityAudit", "71");
      EmailTemplateHandler.Add("Form_TransparencyRegister", "73");
      EmailTemplateHandler.Add("Form_SustainabilityManagement", "76");
      EmailTemplateHandler.Add("Form_MonthlyOccupationalHealthStatistics", "77");
      EmailTemplateHandler.Add("Form_PharmacySurveys", "78");
      EmailTemplateHandler.Add("Form_VTE", "89");
      EmailTemplateHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention", "90");
      EmailTemplateHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime", "91");
    }

    // TODO : New Form : InfoQuest_Email : Add new Form to SqlStringFacilityIdPrimaryHandlers()
    protected void SqlStringFacilityIdPrimaryHandlers()
    {
      SqlStringFacilityIdPrimaryHandler.Add("Form_FIMFAM", "SELECT Facility_Id FROM vForm_FIMFAM_Elements WHERE FIMFAM_Elements_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_Isidima", "SELECT Facility_Id FROM vForm_Isidima_Category WHERE Isidima_Category_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_AMSPI", "SELECT Facility_Id FROM vForm_AMSPI_Intervention WHERE AMSPI_Intervention_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_PROMS", "SELECT Facility_Id FROM vForm_PROMS_Questionnaire WHERE PROMS_Questionnaire_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_QMSReview_Findings", "SELECT Facility_Id FROM vForm_QMSReview_Findings WHERE QMSReview_Findings_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_QMSReview_Summary", "SELECT Facility_Id FROM vForm_QMSReview WHERE QMSReview_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_CollegeLearningAudit_Findings", "SELECT Facility_Id FROM vForm_CollegeLearningAudit_Findings WHERE CLA_Findings_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_CollegeLearningAudit_Summary", "SELECT Facility_Id FROM vForm_CollegeLearningAudit WHERE CLA_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_OccupationalHealthAudit_Findings", "SELECT Facility_Id FROM vForm_OccupationalHealthAudit_Findings WHERE OHA_Findings_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_OccupationalHealthAudit_Summary", "SELECT Facility_Id FROM vForm_OccupationalHealthAudit WHERE OHA_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_MonthlyHospitalStatistics", "SELECT Facility_Id FROM vForm_MonthlyHospitalStatistics WHERE MHS_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_MonthlyPharmacyStatistics", "SELECT Facility_Id FROM vForm_MonthlyPharmacyStatistics WHERE MPS_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_Incident", "SELECT Facility_Id FROM vForm_Incident WHERE Incident_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_Alert", "SELECT Facility_Id FROM vForm_Alert WHERE Alert_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_Pharmacy_NewProduct", "SELECT Facility_Id FROM vForm_Pharmacy_NewProduct WHERE NewProduct_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_MHQ14", "SELECT Facility_Id FROM vForm_MHQ14_Questionnaire WHERE MHQ14_Questionnaire_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_BundleCompliance", "SELECT Facility_Id FROM vForm_BundleCompliance WHERE BC_Bundles_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_RehabBundleCompliance", "SELECT Facility_Id FROM vForm_RehabBundleCompliance WHERE RBC_Bundles_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_CRM", "SELECT Facility_Id FROM vForm_CRM WHERE CRM_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_InfectionPrevention", "SELECT fkiFacilityID AS Facility_Id FROM tblInfectionPrevention WHERE pkiInfectionFormID = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_IPS", "SELECT Facility_Id FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_MedicationBundleCompliance", "SELECT Facility_Id FROM vForm_MedicationBundleCompliance_Bundles WHERE MBC_Bundles_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_AntimicrobialStewardshipIntervention", "SELECT Facility_Id FROM vForm_AntimicrobialStewardshipIntervention_Intervention WHERE ASI_Intervention_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_HeadOfficeQualityAudit", "SELECT Facility_Id FROM vForm_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_TransparencyRegister", "SELECT '0' AS Facility_Id FROM Form_TransparencyRegister WHERE TransparencyRegister_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_SustainabilityManagement", "SELECT Facility_Id FROM vForm_SustainabilityManagement WHERE SustainabilityManagement_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_MonthlyOccupationalHealthStatistics", "SELECT Facility_Id FROM vForm_MonthlyOccupationalHealthStatistics WHERE MOHS_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_PharmacySurveys", "SELECT Facility_Id FROM Form_PharmacySurveys_CreatedSurveys WHERE CreatedSurveys_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_VTE", "SELECT Facility_Id FROM vForm_VTE_Assesments WHERE VTE_Assesments_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention", "SELECT Facility_Id FROM vForm_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_TI_Id = @EmailValue");
      SqlStringFacilityIdPrimaryHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime", "SELECT Facility_Id FROM vForm_PharmacyClinicalMetrics_PharmacistTime WHERE PCM_PT_Id = @EmailValue");
    }

    // TODO : New Form : InfoQuest_Email : Add new Form to SqlStringFormDetailHandlers()
    protected void SqlStringFormDetailHandlers()
    {
      SqlStringFormDetailHandler.Add("Form_FIMFAM", "SELECT Facility_Id , Facility_FacilityDisplayName AS FacilityDisplayName , FIMFAM_Elements_PatientVisitNumber AS PatientVisitNumber FROM vForm_FIMFAM_Elements WHERE FIMFAM_Elements_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_Isidima", "SELECT Facility_Id , Facility_FacilityDisplayName AS FacilityDisplayName , Isidima_Category_PatientVisitNumber AS PatientVisitNumber FROM vForm_Isidima_Category WHERE Isidima_Category_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_AMSPI", "SELECT Facility_Id , Facility_FacilityDisplayName AS FacilityDisplayName , AMSPI_Intervention_PatientVisitNumber AS PatientVisitNumber FROM vForm_AMSPI_Intervention WHERE AMSPI_Intervention_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_PROMS", "SELECT Facility_Id , Facility_FacilityDisplayName AS FacilityDisplayName , PROMS_Questionnaire_PatientVisitNumber AS PatientVisitNumber FROM vForm_PROMS_Questionnaire WHERE PROMS_Questionnaire_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_QMSReview_Findings", "SELECT Facility_Id ,Facility_FacilityDisplayName AS FacilityDisplayName ,QMSReview_Id ,CONVERT(NVARCHAR(MAX),CONVERT(DATETIME,QMSReview_Date),111) AS QMSReview_Date ,QMSReview_Findings_System ,QMSReview_Findings_MeasurementCriteria , QMSReview_Findings_Category FROM vForm_QMSReview_Findings WHERE QMSReview_Findings_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_QMSReview_Summary", "SELECT Facility_Id ,Facility_FacilityDisplayName AS FacilityDisplayName ,QMSReview_Id ,CONVERT(NVARCHAR(MAX),CONVERT(DATETIME,QMSReview_Date),111) AS QMSReview_Date FROM vForm_QMSReview WHERE QMSReview_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_CollegeLearningAudit_Findings", "SELECT Facility_Id ,Facility_FacilityDisplayName AS FacilityDisplayName ,CLA_Id ,CONVERT(NVARCHAR(MAX),CONVERT(DATETIME,CLA_Date),111) AS CLA_Date ,CLA_Findings_System ,CLA_Findings_MeasurementCriteria , CLA_Findings_Category FROM vForm_CollegeLearningAudit_Findings WHERE CLA_Findings_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_CollegeLearningAudit_Summary", "SELECT Facility_Id ,Facility_FacilityDisplayName AS FacilityDisplayName ,CLA_Id ,CONVERT(NVARCHAR(MAX),CONVERT(DATETIME,CLA_Date),111) AS CLA_Date FROM vForm_CollegeLearningAudit WHERE CLA_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_OccupationalHealthAudit_Findings", "SELECT Facility_Id ,Facility_FacilityDisplayName AS FacilityDisplayName ,OHA_Id ,CONVERT(NVARCHAR(MAX),CONVERT(DATETIME,OHA_Date),111) AS OHA_Date ,OHA_Findings_System ,OHA_Findings_MeasurementCriteria , OHA_Findings_Category FROM vForm_OccupationalHealthAudit_Findings WHERE OHA_Findings_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_OccupationalHealthAudit_Summary", "SELECT Facility_Id ,Facility_FacilityDisplayName AS FacilityDisplayName ,OHA_Id ,CONVERT(NVARCHAR(MAX),CONVERT(DATETIME,OHA_Date),111) AS OHA_Date FROM vForm_OccupationalHealthAudit WHERE OHA_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_MonthlyHospitalStatistics", "SELECT Facility_Id ,Facility_FacilityDisplayName AS FacilityDisplayName ,MHS_Period ,MHS_FYPeriod FROM vForm_MonthlyHospitalStatistics WHERE MHS_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_MonthlyPharmacyStatistics", "SELECT Facility_Id ,Facility_FacilityDisplayName AS FacilityDisplayName ,MPS_Period ,MPS_FYPeriod FROM vForm_MonthlyPharmacyStatistics WHERE MPS_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_Incident", "SELECT Facility_Id ,Facility_FacilityDisplayName ,Incident_ReportNumber FROM vForm_Incident WHERE Incident_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_Alert", "SELECT Facility_Id ,Facility_FacilityDisplayName ,Alert_ReportNumber FROM vForm_Alert WHERE Alert_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_Pharmacy_NewProduct", "SELECT Facility_FacilityDisplayName AS FacilityDisplayName ,NewProduct_ReportNumber FROM vForm_Pharmacy_NewProduct WHERE NewProduct_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_MHQ14", "SELECT Facility_Id , Facility_FacilityDisplayName AS FacilityDisplayName , MHQ14_Questionnaire_PatientVisitNumber AS PatientVisitNumber FROM vForm_MHQ14_Questionnaire WHERE MHQ14_Questionnaire_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_BundleCompliance", "SELECT Facility_Id , Facility_FacilityDisplayName , BC_Bundles_PatientVisitNumber FROM vForm_BundleCompliance WHERE BC_Bundles_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_RehabBundleCompliance", "SELECT Facility_Id , Facility_FacilityDisplayName , RBC_Bundles_PatientVisitNumber FROM vForm_RehabBundleCompliance WHERE RBC_Bundles_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_CRM", "SELECT CRM_Id , Facility_FacilityDisplayName AS FacilityDisplayName , CRM_ReportNumber , CRM_Type_Name FROM vForm_CRM WHERE CRM_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_InfectionPrevention", "SELECT pkiInfectionFormID , fkiFacilityID AS Facility_Id ,  Facility_FacilityDisplayName AS FacilityDisplayName , sPatientVisitNumber , sReportNumber FROM tblInfectionPrevention LEFT JOIN dbo.vAdministration_Facility_All ON tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id WHERE pkiInfectionFormID = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_IPS", "SELECT IPS_VisitInformation_Id , IPS_Infection_Id , Facility_FacilityDisplayName , IPS_Infection_ReportNumber FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_MedicationBundleCompliance", "SELECT MBC_VisitInformation_Id , MBC_Bundles_Id , Facility_FacilityDisplayName , MBC_Bundles_ReportNumber FROM vForm_MedicationBundleCompliance_Bundles WHERE MBC_Bundles_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_AntimicrobialStewardshipIntervention", "SELECT ASI_VisitInformation_Id , ASI_Intervention_Id , Facility_FacilityDisplayName , ASI_Intervention_ReportNumber FROM vForm_AntimicrobialStewardshipIntervention_Intervention WHERE ASI_Intervention_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_HeadOfficeQualityAudit", "SELECT HQA_Finding_Id , HQA_Finding_Function_Name , HQA_Finding_FindingNo , Facility_FacilityDisplayName FROM vForm_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_TransparencyRegister", "SELECT TransparencyRegister_EmployeeNumber , TransparencyRegister_FirstName , TransparencyRegister_LastName FROM vForm_TransparencyRegister WHERE TransparencyRegister_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_SustainabilityManagement", "SELECT Facility_Id , Facility_FacilityDisplayName , SustainabilityManagement_Period , SustainabilityManagement_FYPeriod FROM vForm_SustainabilityManagement WHERE SustainabilityManagement_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_MonthlyOccupationalHealthStatistics", "SELECT Facility_Id , Facility_FacilityDisplayName , MOHS_Period , MOHS_FYPeriod FROM vForm_MonthlyOccupationalHealthStatistics WHERE MOHS_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_PharmacySurveys", "SELECT Facility_FacilityDisplayName , LoadedSurveys_Name , LoadedSurveys_FY , CreatedSurveys_Name FROM vForm_PharmacySurveys_CreatedSurveys WHERE vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_VTE", "SELECT VTE_VisitInformation_Id , VTE_Assesments_Id , Facility_FacilityDisplayName , VTE_Assesments_ReportNumber FROM vForm_VTE_Assesments WHERE VTE_Assesments_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention", "SELECT PCM_TI_Id , PCM_Intervention_Id , Facility_FacilityDisplayName , PCM_TI_ReportNumber , PCM_Intervention_Intervention_Name FROM vForm_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_TI_Id = @EmailValue");
      SqlStringFormDetailHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime", "SELECT PCM_PT_Id , PCM_Intervention_Id , Facility_FacilityDisplayName , PCM_PT_ReportNumber , PCM_Intervention_Intervention_Name FROM vForm_PharmacyClinicalMetrics_PharmacistTime WHERE PCM_PT_Id = @EmailValue");
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
          FormIdHandler.Clear();
          return "0";
        }
      }
    }

    protected string EmailTemplateId(string formName)
    {
      if (formName == null)
      {
        return "0";
      }
      else
      {
        EmailTemplateHandlers();

        if (EmailTemplateHandler.ContainsKey(formName))
        {
          string EmailTemplateValue = EmailTemplateHandler[formName];
          EmailTemplateHandler.Clear();
          return EmailTemplateValue;
        }
        else
        {
          EmailTemplateHandler.Clear();
          return "0";
        }
      }
    }

    protected string SqlStringFacilityIdPrimary(string formName)
    {
      if (formName == null)
      {
        return "";
      }
      else
      {
        SqlStringFacilityIdPrimaryHandlers();

        if (SqlStringFacilityIdPrimaryHandler.ContainsKey(formName))
        {
          string SqlStringFacilityIdPrimaryValue = SqlStringFacilityIdPrimaryHandler[formName];
          SqlStringFacilityIdPrimaryHandler.Clear();
          return SqlStringFacilityIdPrimaryValue;
        }
        else
        {
          SqlStringFacilityIdPrimaryHandler.Clear();
          return "";
        }
      }
    }

    protected string SqlStringFormDetail(string formName)
    {
      if (formName == null)
      {
        return "";
      }
      else
      {
        SqlStringFormDetailHandlers();

        if (SqlStringFormDetailHandler.ContainsKey(formName))
        {
          string SqlStringFormDetailValue = SqlStringFormDetailHandler[formName];
          SqlStringFormDetailHandler.Clear();
          return SqlStringFormDetailValue;
        }
        else
        {
          SqlStringFormDetailHandler.Clear();
          return "";
        }
      }
    }


    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    protected void SendEmail_TextBox()
    {
      string TextBoxEmailAddress = TextBox_EmailAddress.Text.ToString();
      TextBoxEmailAddress = TextBoxEmailAddress.Replace(";", ",");
      TextBoxEmailAddress = TextBoxEmailAddress.Replace(":", ",");

      string[] TextBoxEmailAddress_Split = TextBoxEmailAddress.Split(',');

      foreach (string TextBoxEmailAddress_SplitEmail in TextBoxEmailAddress_Split)
      {
        string EmailAddress = TextBoxEmailAddress_SplitEmail;
        string ValidEmailAddress = EmailAddressValidation(EmailAddress);

        if (ValidEmailAddress == "Yes")
        {
          TextBoxDescription = TextBox_Description.Text.ToString();
          EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate(EmailTemplateId(Request.QueryString["EmailPage"]));
          URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
          FormName = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["EmailPage"]));
          SendFromDetail(out FirstNameSendFrom, out LastNameSendFrom);
          SendToDetail(EmailAddress, out FirstNameSendTo, out LastNameSendTo);

          ClearSQLStringFormDetailValueVariables();
          AssignSQLStringFormDetailValueVariables();

          BodyString = EmailTemplate;
          BodyStringReplace();
          HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
          FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
          EmailBody = HeaderString + BodyString + FooterString;

          string EmailSendValue = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailAddress, FormName, EmailBody);
          EmailMessage = EmailSend(EmailSendValue, EmailAddress);
          EmailBody = "";
        }
      }
    }

    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    protected void SendEmail_CheckBoxList()
    {
      for (int i = 0; i < CheckBoxList_EmailAddress.Items.Count; i++)
      {
        if (CheckBoxList_EmailAddress.Items[i].Selected)
        {
          string EmailAddress = CheckBoxList_EmailAddress.Items[i].Value;
          string ValidEmailAddress = EmailAddressValidation(EmailAddress);

          if (ValidEmailAddress == "Yes")
          {
            TextBoxDescription = TextBox_Description.Text.ToString();
            EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate(EmailTemplateId(Request.QueryString["EmailPage"]));
            URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
            FormName = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["EmailPage"]));
            SendFromDetail(out FirstNameSendFrom, out LastNameSendFrom);
            SendToDetail(EmailAddress, out FirstNameSendTo, out LastNameSendTo);

            ClearSQLStringFormDetailValueVariables();
            AssignSQLStringFormDetailValueVariables();

            BodyString = EmailTemplate;
            BodyStringReplace();
            HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
            FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
            EmailBody = HeaderString + BodyString + FooterString;

            string EmailSendValue = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailAddress, FormName, EmailBody);
            EmailMessage = EmailSend(EmailSendValue, EmailAddress);
            EmailBody = "";
          }
        }
      }
    }

    
    // TODO : New Form : InfoQuest_Email : Add new SQLStringFormDetailValue() variables to AssignSQLStringFormDetailValueVariables()
    private void AssignSQLStringFormDetailValueVariables()
    {
      string SQLStringFormDetailValue = SqlStringFormDetail(Request.QueryString["EmailPage"]);

      using (SqlCommand SqlCommand_FormDetail = new SqlCommand(SQLStringFormDetailValue))
      {
        SqlCommand_FormDetail.Parameters.AddWithValue("@EmailValue", Request.QueryString["EmailValue"]);
        DataTable DataTable_FormDetail;
        using (DataTable_FormDetail = new DataTable())
        {
          DataTable_FormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_FormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormDetail).Copy();
          if (DataTable_FormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormDetail.Rows)
            {
              if (Request.QueryString["EmailPage"] == "Form_QMSReview_Findings")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                QMSReviewId = DataRow_Row["QMSReview_Id"].ToString();
                QMSReviewDate = DataRow_Row["QMSReview_Date"].ToString();
                QMSReviewFindingsSystem = DataRow_Row["QMSReview_Findings_System"].ToString();
                QMSReviewFindingsMeasurementCriteria = DataRow_Row["QMSReview_Findings_MeasurementCriteria"].ToString();
                QMSReviewFindingsCategory = DataRow_Row["QMSReview_Findings_Category"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_QMSReview_Summary")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                QMSReviewId = DataRow_Row["QMSReview_Id"].ToString();
                QMSReviewDate = DataRow_Row["QMSReview_Date"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_CollegeLearningAudit_Findings")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                CLAId = DataRow_Row["CLA_Id"].ToString();
                CLADate = DataRow_Row["CLA_Date"].ToString();
                CLAFindingsSystem = DataRow_Row["CLA_Findings_System"].ToString();
                CLAFindingsMeasurementCriteria = DataRow_Row["CLA_Findings_MeasurementCriteria"].ToString();
                CLAFindingsCategory = DataRow_Row["CLA_Findings_Category"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_CollegeLearningAudit_Summary")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                CLAId = DataRow_Row["CLA_Id"].ToString();
                CLADate = DataRow_Row["CLA_Date"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_OccupationalHealthAudit_Findings")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                OHAId = DataRow_Row["OHA_Id"].ToString();
                OHADate = DataRow_Row["OHA_Date"].ToString();
                OHAFindingsSystem = DataRow_Row["OHA_Findings_System"].ToString();
                OHAFindingsMeasurementCriteria = DataRow_Row["OHA_Findings_MeasurementCriteria"].ToString();
                OHAFindingsCategory = DataRow_Row["OHA_Findings_Category"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_OccupationalHealthAudit_Summary")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                OHAId = DataRow_Row["OHA_Id"].ToString();
                OHADate = DataRow_Row["OHA_Date"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_MonthlyHospitalStatistics")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                MHSPeriod = DataRow_Row["MHS_Period"].ToString();
                MHSFYPeriod = DataRow_Row["MHS_FYPeriod"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_MonthlyPharmacyStatistics")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                MPSPeriod = DataRow_Row["MPS_Period"].ToString();
                MPSFYPeriod = DataRow_Row["MPS_FYPeriod"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_Incident")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_Alert")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                AlertReportNumber = DataRow_Row["Alert_ReportNumber"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_Pharmacy_NewProduct")
              {
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                NewProductReportNumber = DataRow_Row["NewProduct_ReportNumber"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_CRM")
              {
                CRMId = DataRow_Row["CRM_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                CRMReportNumber = DataRow_Row["CRM_ReportNumber"].ToString();
                CRMTypeName = DataRow_Row["CRM_Type_Name"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_InfectionPrevention")
              {
                pkiInfectionFormID = DataRow_Row["pkiInfectionFormID"].ToString();
                FacilityId = DataRow_Row["Facility_Id"].ToString();  
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString(); 
                sPatientVisitNumber = DataRow_Row["sPatientVisitNumber"].ToString();
                sReportNumber = DataRow_Row["sReportNumber"].ToString(); 
              }
              else if (Request.QueryString["EmailPage"] == "Form_IPS")
              {
                IPSVisitInformationId = DataRow_Row["IPS_VisitInformation_Id"].ToString();
                IPSInfectionId = DataRow_Row["IPS_Infection_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                IPSInfectionReportNumber = DataRow_Row["IPS_Infection_ReportNumber"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_BundleCompliance")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                PatientVisitNumber = DataRow_Row["BC_Bundles_PatientVisitNumber"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_RehabBundleCompliance")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                PatientVisitNumber = DataRow_Row["RBC_Bundles_PatientVisitNumber"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_MedicationBundleCompliance")
              {
                MBCVisitInformationId = DataRow_Row["MBC_VisitInformation_Id"].ToString();
                MBCBundlesId = DataRow_Row["MBC_Bundles_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                MBCBundlesReportNumber = DataRow_Row["MBC_Bundles_ReportNumber"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_AntimicrobialStewardshipIntervention")
              {
                ASIVisitInformationId = DataRow_Row["ASI_VisitInformation_Id"].ToString();
                ASIInterventionId = DataRow_Row["ASI_Intervention_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                ASIInterventionReportNumber = DataRow_Row["ASI_Intervention_ReportNumber"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_HeadOfficeQualityAudit")
              {
                HQAFindingId = DataRow_Row["HQA_Finding_Id"].ToString();
                HQAFindingFunctionName = DataRow_Row["HQA_Finding_Function_Name"].ToString();
                HQAFindingFindingNo = DataRow_Row["HQA_Finding_FindingNo"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_TransparencyRegister")
              {
                TransparencyRegisterEmployeeNumber = DataRow_Row["TransparencyRegister_EmployeeNumber"].ToString();
                TransparencyRegisterFirstName = DataRow_Row["TransparencyRegister_FirstName"].ToString();
                TransparencyRegisterLastName = DataRow_Row["TransparencyRegister_LastName"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_SustainabilityManagement")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                MHSPeriod = DataRow_Row["SustainabilityManagement_Period"].ToString();
                MHSFYPeriod = DataRow_Row["SustainabilityManagement_FYPeriod"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_MonthlyOccupationalHealthStatistics")
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                MOHSPeriod = DataRow_Row["MOHS_Period"].ToString();
                MOHSFYPeriod = DataRow_Row["MOHS_FYPeriod"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_PharmacySurveys")
              {
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
                LoadedSurveysFY = DataRow_Row["LoadedSurveys_FY"].ToString();
                CreatedSurveysName = DataRow_Row["CreatedSurveys_Name"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_VTE")
              {
                VTEVisitInformationId = DataRow_Row["VTE_VisitInformation_Id"].ToString();
                VTEAssesmentsId = DataRow_Row["VTE_Assesments_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                VTEAssesmentsReportNumber = DataRow_Row["VTE_Assesments_ReportNumber"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_PharmacyClinicalMetrics_TherapeuticIntervention")
              {
                PCMTIId = DataRow_Row["PCM_TI_Id"].ToString();
                PCMInterventionId = DataRow_Row["PCM_Intervention_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                PCMTIReportNumber = DataRow_Row["PCM_TI_ReportNumber"].ToString();
                PCMInterventionInterventionName = DataRow_Row["PCM_Intervention_Intervention_Name"].ToString();
              }
              else if (Request.QueryString["EmailPage"] == "Form_PharmacyClinicalMetrics_PharmacistTime")
              {
                PCMPTId = DataRow_Row["PCM_PT_Id"].ToString();
                PCMInterventionId = DataRow_Row["PCM_Intervention_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                PCMPTReportNumber = DataRow_Row["PCM_PT_ReportNumber"].ToString();
                PCMInterventionInterventionName = DataRow_Row["PCM_Intervention_Intervention_Name"].ToString();
              }
              else
              {
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["FacilityDisplayName"].ToString();
                PatientVisitNumber = DataRow_Row["PatientVisitNumber"].ToString();
              }
            }
          }
          else
          {
            ClearSQLStringFormDetailValueVariables();
          }
        }
      }
    }

    // TODO : New Form : InfoQuest_Email : Add new SQLStringFormDetailValue() variables to ClearSQLStringFormDetailValueVariables()
    private void ClearSQLStringFormDetailValueVariables()
    {
      FacilityId = "";
      FacilityFacilityDisplayName = "";
      PatientVisitNumber = "";
      QMSReviewId = "";
      QMSReviewDate = "";
      QMSReviewFindingsSystem = "";
      QMSReviewFindingsMeasurementCriteria = "";
      QMSReviewFindingsCategory = "";
      CLAId = "";
      CLADate = "";
      CLAFindingsSystem = "";
      CLAFindingsMeasurementCriteria = "";
      CLAFindingsCategory = "";
      OHAId = "";
      OHADate = "";
      OHAFindingsSystem = "";
      OHAFindingsMeasurementCriteria = "";
      OHAFindingsCategory = "";
      MHSPeriod = "";
      MHSFYPeriod = "";
      MPSPeriod = "";
      MPSFYPeriod = "";
      IncidentReportNumber = "";
      AlertReportNumber = "";
      NewProductReportNumber = "";
      CRMId = "";
      CRMReportNumber = "";
      CRMTypeName = "";
      pkiInfectionFormID = "";
      sPatientVisitNumber = "";
      sReportNumber = "";
      IPSVisitInformationId = "";
      IPSInfectionId = "";
      IPSInfectionReportNumber = "";
      MBCVisitInformationId = "";
      MBCBundlesId = "";
      MBCBundlesReportNumber = "";
      ASIVisitInformationId = "";
      ASIInterventionId = "";
      ASIInterventionReportNumber = "";
      HQAFindingId = "";
      HQAFindingFunctionName = "";
      HQAFindingFindingNo = "";
      TransparencyRegisterEmployeeNumber = "";
      TransparencyRegisterFirstName = "";
      TransparencyRegisterLastName = "";
      SustainabilityManagementPeriod = "";
      SustainabilityManagementFYPeriod = "";
      MOHSPeriod = "";
      MOHSFYPeriod = "";
      LoadedSurveysName = "";
      LoadedSurveysFY = "";
      CreatedSurveysName = "";
      VTEVisitInformationId = "";
      VTEAssesmentsId = "";
      VTEAssesmentsReportNumber = "";
      PCMTIId = "";
      PCMInterventionId = "";
      PCMTIReportNumber = "";
      PCMInterventionInterventionName = "";
      PCMPTId = "";
      PCMPTReportNumber = "";
    }

    // TODO : New Form : InfoQuest_Email : Add new SQLStringFormDetailValue() variables to BodyStringReplace()
    private void BodyStringReplace()
    {
      BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + FirstNameSendTo + " " + LastNameSendTo + "");
      BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
      BodyString = BodyString.Replace(";replace;CurrentUserFullName;replace;", "" + FirstNameSendFrom + " " + LastNameSendFrom + "");
      BodyString = BodyString.Replace(";replace;Description;replace;", "" + TextBoxDescription + "");
      BodyString = BodyString.Replace(";replace;FacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
      BodyString = BodyString.Replace(";replace;PatientVisitNumber;replace;", "" + PatientVisitNumber + "");
      BodyString = BodyString.Replace(";replace;FacilitiesId;replace;", "" + FacilityId + "");
      BodyString = BodyString.Replace(";replace;EmailValue;replace;", "" + Request.QueryString["EmailValue"] + "");
      BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");

      BodyString = BodyString.Replace(";replace;QMSReviewId;replace;", "" + QMSReviewId + "");
      BodyString = BodyString.Replace(";replace;QMSReviewDate;replace;", "" + QMSReviewDate + "");
      BodyString = BodyString.Replace(";replace;QMSReviewFindingsSystem;replace;", "" + QMSReviewFindingsSystem + "");
      BodyString = BodyString.Replace(";replace;QMSReviewFindingsMeasurementCriteria;replace;", "" + QMSReviewFindingsMeasurementCriteria + "");
      BodyString = BodyString.Replace(";replace;QMSReviewFindingsCategory;replace;", "" + QMSReviewFindingsCategory + "");

      BodyString = BodyString.Replace(";replace;CLAId;replace;", "" + CLAId + "");
      BodyString = BodyString.Replace(";replace;CLADate;replace;", "" + CLADate + "");
      BodyString = BodyString.Replace(";replace;CLAFindingsSystem;replace;", "" + CLAFindingsSystem + "");
      BodyString = BodyString.Replace(";replace;CLAFindingsMeasurementCriteria;replace;", "" + CLAFindingsMeasurementCriteria + "");
      BodyString = BodyString.Replace(";replace;CLAFindingsCategory;replace;", "" + CLAFindingsCategory + "");

      BodyString = BodyString.Replace(";replace;OHAId;replace;", "" + OHAId + "");
      BodyString = BodyString.Replace(";replace;OHADate;replace;", "" + OHADate + "");
      BodyString = BodyString.Replace(";replace;OHAFindingsSystem;replace;", "" + OHAFindingsSystem + "");
      BodyString = BodyString.Replace(";replace;OHAFindingsMeasurementCriteria;replace;", "" + OHAFindingsMeasurementCriteria + "");
      BodyString = BodyString.Replace(";replace;OHAFindingsCategory;replace;", "" + OHAFindingsCategory + "");

      BodyString = BodyString.Replace(";replace;MHSPeriod;replace;", "" + MHSPeriod + "");
      BodyString = BodyString.Replace(";replace;MHSFYPeriod;replace;", "" + MHSFYPeriod + "");

      BodyString = BodyString.Replace(";replace;MPSPeriod;replace;", "" + MPSPeriod + "");
      BodyString = BodyString.Replace(";replace;MPSFYPeriod;replace;", "" + MPSFYPeriod + "");

      BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");

      BodyString = BodyString.Replace(";replace;AlertReportNumber;replace;", "" + AlertReportNumber + "");

      BodyString = BodyString.Replace(";replace;NewProductReportNumber;replace;", "" + NewProductReportNumber + "");

      BodyString = BodyString.Replace(";replace;CRMId;replace;", "" + CRMId + "");
      BodyString = BodyString.Replace(";replace;CRMReportNumber;replace;", "" + CRMReportNumber + "");
      BodyString = BodyString.Replace(";replace;CRMTypeName;replace;", "" + CRMTypeName + "");

      BodyString = BodyString.Replace(";replace;pkiInfectionFormID;replace;", "" + pkiInfectionFormID + "");
      BodyString = BodyString.Replace(";replace;sPatientVisitNumber;replace;", "" + sPatientVisitNumber + "");
      BodyString = BodyString.Replace(";replace;sReportNumber;replace;", "" + sReportNumber + "");

      BodyString = BodyString.Replace(";replace;IPSVisitInformationId;replace;", "" + IPSVisitInformationId + "");
      BodyString = BodyString.Replace(";replace;IPSInfectionId;replace;", "" + IPSInfectionId + "");
      BodyString = BodyString.Replace(";replace;IPSInfectionReportNumber;replace;", "" + IPSInfectionReportNumber + "");

      BodyString = BodyString.Replace(";replace;MBCVisitInformationId;replace;", "" + MBCVisitInformationId + "");
      BodyString = BodyString.Replace(";replace;MBCBundlesId;replace;", "" + MBCBundlesId + "");
      BodyString = BodyString.Replace(";replace;MBCBundlesReportNumber;replace;", "" + MBCBundlesReportNumber + "");

      BodyString = BodyString.Replace(";replace;ASIVisitInformationId;replace;", "" + ASIVisitInformationId + "");
      BodyString = BodyString.Replace(";replace;ASIInterventionId;replace;", "" + ASIInterventionId + "");
      BodyString = BodyString.Replace(";replace;ASIInterventionReportNumber;replace;", "" + ASIInterventionReportNumber + "");

      BodyString = BodyString.Replace(";replace;HQAFindingId;replace;", "" + HQAFindingId + "");
      BodyString = BodyString.Replace(";replace;HQAFindingFunctionName;replace;", "" + HQAFindingFunctionName + "");
      BodyString = BodyString.Replace(";replace;HQAFindingFindingNo;replace;", "" + HQAFindingFindingNo + "");

      BodyString = BodyString.Replace(";replace;TransparencyRegisterEmployeeNumber;replace;", "" + TransparencyRegisterEmployeeNumber + "");
      BodyString = BodyString.Replace(";replace;TransparencyRegisterFirstName;replace;", "" + TransparencyRegisterFirstName + "");
      BodyString = BodyString.Replace(";replace;TransparencyRegisterLastName;replace;", "" + TransparencyRegisterLastName + "");

      BodyString = BodyString.Replace(";replace;SustainabilityManagementPeriod;replace;", "" + SustainabilityManagementPeriod + "");
      BodyString = BodyString.Replace(";replace;SustainabilityManagementFYPeriod;replace;", "" + SustainabilityManagementFYPeriod + "");

      BodyString = BodyString.Replace(";replace;MOHSPeriod;replace;", "" + MOHSPeriod + "");
      BodyString = BodyString.Replace(";replace;MOHSFYPeriod;replace;", "" + MOHSFYPeriod + "");

      BodyString = BodyString.Replace(";replace;LoadedSurveysName;replace;", "" + LoadedSurveysName + "");
      BodyString = BodyString.Replace(";replace;LoadedSurveysFY;replace;", "" + LoadedSurveysFY + "");
      BodyString = BodyString.Replace(";replace;CreatedSurveysName;replace;", "" + CreatedSurveysName  + "");

      BodyString = BodyString.Replace(";replace;VTEVisitInformationId;replace;", "" + VTEVisitInformationId + "");
      BodyString = BodyString.Replace(";replace;VTEAssesmentsId;replace;", "" + VTEAssesmentsId + "");
      BodyString = BodyString.Replace(";replace;VTEAssesmentsReportNumber;replace;", "" + VTEAssesmentsReportNumber + "");

      BodyString = BodyString.Replace(";replace;PCMTIId;replace;", "" + PCMTIId + "");
      BodyString = BodyString.Replace(";replace;PCMInterventionId;replace;", "" + PCMInterventionId + "");
      BodyString = BodyString.Replace(";replace;PCMTIReportNumber;replace;", "" + PCMTIReportNumber + "");
      BodyString = BodyString.Replace(";replace;PCMInterventionInterventionName;replace;", "" + PCMInterventionInterventionName + "");
      BodyString = BodyString.Replace(";replace;PCMPTId;replace;", "" + PCMPTId + "");
      BodyString = BodyString.Replace(";replace;PCMPTReportNumber;replace;", "" + PCMPTReportNumber + "");
    }

    private string EmailAddressValidation(string EmailAddress)
    {
      string ValidEmailAddress = "No";

      if (!string.IsNullOrEmpty(EmailAddress))
      {
        InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
        string ValidRegexEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailAddress);

        if (ValidRegexEmailAddress == "No")
        {
          ValidEmailAddress = "No";
          EmailMessage = EmailMessage + "<strong style='color:#B0262e'>Email Address " + EmailAddress + " is not a valid Email address<strong><br />";
        }
        else
        {
          ValidEmailAddress = "Yes";
        }
      }
      else
      {
        ValidEmailAddress = "No";
      }

      return ValidEmailAddress;
    }

    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    private void SendFromDetail(out string FirstNameSendFromValue, out string LastNameSendFromValue)
    {
      string FirstName = "";
      string LastName = "";

      DataTable DataTable_EmailFrom;
      using (DataTable_EmailFrom = new DataTable())
      {
        DataTable_EmailFrom.Locale = CultureInfo.CurrentCulture;
        DataTable_EmailFrom = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(Request.ServerVariables["LOGON_USER"]).Copy();
        if (DataTable_EmailFrom.Columns.Count != 1)
        {
          if (DataTable_EmailFrom.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EmailFrom.Rows)
            {
              FirstName = DataRow_Row["FirstName"].ToString();
              LastName = DataRow_Row["LastName"].ToString();
            }
          }
        }
      }

      FirstNameSendFromValue = FirstName;
      LastNameSendFromValue = LastName;
    }

    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    private static void SendToDetail(string EmailAddress, out string FirstNameSendToValue, out string LastNameSendToValue)
    {
      string FirstName = "";
      string LastName = "";

      DataTable DataTable_EmailTo;
      using (DataTable_EmailTo = new DataTable())
      {
        DataTable_EmailTo.Locale = CultureInfo.CurrentCulture;
        DataTable_EmailTo = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindAll("", "", "", EmailAddress).Copy();
        if (DataTable_EmailTo.Columns.Count != 1)
        {
          if (DataTable_EmailTo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EmailTo.Rows)
            {
              FirstName = DataRow_Row["FirstName"].ToString();
              LastName = DataRow_Row["LastName"].ToString();
            }
          }
        }
      }

      FirstNameSendToValue = FirstName;
      LastNameSendToValue = LastName;
    }

    private string EmailSend(string EmailSend, string EmailSendTo)
    {
      if (EmailSend == "Yes")
      {
        Page.MaintainScrollPositionOnPostBack = false;
        EmailMessage = EmailMessage + Convert.ToString("<strong style='color:#77cf9c'>Email sent to " + EmailSendTo + "<strong><br />", CultureInfo.CurrentCulture);
      }
      else
      {
        Page.MaintainScrollPositionOnPostBack = false;
        EmailMessage = EmailMessage + Convert.ToString("<strong style='color:#B0262e'>Email could not be send to " + EmailSendTo + "<strong><br />", CultureInfo.CurrentCulture);
      }

      return EmailMessage;
    }
  }
}