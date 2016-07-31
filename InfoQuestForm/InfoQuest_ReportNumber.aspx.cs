using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class InfoQuest_ReportNumber : InfoQuestWCF.Override_SystemWebUIPage
  {
    private Dictionary<string, Action> FormHandler = new Dictionary<string, Action>();
    private Dictionary<string, string> FormIdHandler = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

        if (Request.QueryString["ReportPage"] == null || Request.QueryString["ReportNumber"] == null)
        {
          Label_FormName.Text = "";
          Label_ReportNumber.Text = "";
          Hyperlink_View.NavigateUrl = "";
          Hyperlink_Captured.NavigateUrl = "";
        }
        else
        {
          Form(Request.QueryString["ReportPage"]);
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
          SqlCommand_Security.Parameters.AddWithValue("@Form_Id", FormId(Request.QueryString["ReportPage"]));

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          string FormIdValue = FormId(Request.QueryString["ReportPage"]);

          if (FormIdValue == "1" || FormIdValue == "2" || FormIdValue == "44")
          {
            SecurityAllow = "1";
          }
          else
          {
            SecurityAllow = "0";
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
            //Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
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
        ((Label)PageUpdateProgress_ReportNumber.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }


    // TODO : New Form : InfoQuest_ReportNumber : Add new Form to FormHandlers()
    protected void FormHandlers()
    {
      FormHandler.Add("Form_FIMFAM", new Action(Form_FIMFAM));
      FormHandler.Add("Form_Isidima", new Action(Form_Isidima));
      FormHandler.Add("Form_AMSPI", new Action(Form_AMSPI));
      FormHandler.Add("Form_PROMS", new Action(Form_PROMS));
      FormHandler.Add("Form_Incident", new Action(Form_Incident));
      FormHandler.Add("Form_Alert", new Action(Form_Alert));
      FormHandler.Add("Form_Pharmacy_NewProduct", new Action(Form_Pharmacy_NewProduct));
      FormHandler.Add("Form_MHQ14", new Action(Form_MHQ14));
      FormHandler.Add("Form_BundleCompliance", new Action(Form_BundleCompliance));
      FormHandler.Add("Form_RehabBundleCompliance", new Action(Form_RehabBundleCompliance));
      FormHandler.Add("Form_CRM", new Action(Form_CRM));
      FormHandler.Add("Form_HAI", new Action(Form_HAI));
      FormHandler.Add("Form_MedicationBundleCompliance", new Action(Form_MedicationBundleCompliance));
      FormHandler.Add("Form_AntimicrobialStewardshipIntervention", new Action(Form_AntimicrobialStewardshipIntervention));
      FormHandler.Add("Form_VTE", new Action(Form_VTE));
      FormHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention", new Action(Form_PharmacyClinicalMetrics_TherapeuticIntervention));
      FormHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime", new Action(Form_PharmacyClinicalMetrics_PharmacistTime));
    }

    // TODO : New Form : InfoQuest_ReportNumber : Add new Form to FormIdHandlers()
    protected void FormIdHandlers()
    {
      FormIdHandler.Add("Form_Incident", "1");
      FormIdHandler.Add("Form_Alert", "2");
      FormIdHandler.Add("Form_BundleCompliance", "17");
      FormIdHandler.Add("Form_FIMFAM", "25");
      FormIdHandler.Add("Form_Isidima", "27");
      FormIdHandler.Add("Form_AMSPI", "29");
      FormIdHandler.Add("Form_PROMS", "30");
      FormIdHandler.Add("Form_Pharmacy_NewProduct", "33");
      FormIdHandler.Add("Form_MHQ14", "34");
      FormIdHandler.Add("Form_RehabBundleCompliance", "35");
      FormIdHandler.Add("Form_CRM", "36");
      FormIdHandler.Add("Form_HAI", "37");
      FormIdHandler.Add("Form_MedicationBundleCompliance", "38");
      FormIdHandler.Add("Form_AntimicrobialStewardshipIntervention", "39");
      FormIdHandler.Add("Form_VTE", "51");
      FormIdHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention", "52");
      FormIdHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime", "52");
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


    // TODO : New Form : InfoQuest_ReportNumber : Add new Procedure for Form
    private void Form_FIMFAM()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string FIMFAMElementsId = "";
      string FacilityId = "";
      string FIMFAMElementsPatientVisitNumber = "";
      string SQLStringFIMFAMFormDetail = "SELECT FIMFAM_Elements_Id , Facility_Id , FIMFAM_Elements_PatientVisitNumber FROM InfoQuest_Form_FIMFAM_Elements WHERE FIMFAM_Elements_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_FIMFAMFormDetail = new SqlCommand(SQLStringFIMFAMFormDetail))
      {
        SqlCommand_FIMFAMFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_FIMFAMFormDetail;
        using (DataTable_FIMFAMFormDetail = new DataTable())
        {
          DataTable_FIMFAMFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_FIMFAMFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FIMFAMFormDetail).Copy();
          if (DataTable_FIMFAMFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FIMFAMFormDetail.Rows)
            {
              FIMFAMElementsId = DataRow_Row["FIMFAM_Elements_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
              FIMFAMElementsPatientVisitNumber = DataRow_Row["FIMFAM_Elements_PatientVisitNumber"].ToString();
            }
          }
          else
          {
            FIMFAMElementsId = "";
            FacilityId = "";
            FIMFAMElementsPatientVisitNumber = "";
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_FIMFAM.aspx?s_Facility_Id=" + FacilityId + "&s_FIMFAM_PatientVisitNumber=" + FIMFAMElementsPatientVisitNumber + "&FIMFAM_Elements_Id=" + FIMFAMElementsId + "&ViewMode=0";
      Hyperlink_Captured.NavigateUrl = "Form_FIMFAM.aspx";

      FIMFAMElementsId = "";
      FacilityId = "";
      FIMFAMElementsPatientVisitNumber = "";
    }

    private void Form_Isidima()
    {
      Label_FormName.Text = "";
      Label_ReportNumber.Text = "";
      Hyperlink_View.NavigateUrl = "";
      Hyperlink_Captured.NavigateUrl = "";
    }

    private void Form_AMSPI()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string AMSPIInterventionId = "";
      string FacilityId = "";
      string AMSPIInterventionPatientVisitNumber = "";
      string SQLStringAMSPIFormDetail = "SELECT AMSPI_Intervention_Id , Facility_Id , AMSPI_Intervention_PatientVisitNumber FROM InfoQuest_Form_AMSPI_Intervention WHERE AMSPI_Intervention_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_AMSPIFormDetail = new SqlCommand(SQLStringAMSPIFormDetail))
      {
        SqlCommand_AMSPIFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_AMSPIFormDetail;
        using (DataTable_AMSPIFormDetail = new DataTable())
        {
          DataTable_AMSPIFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_AMSPIFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AMSPIFormDetail).Copy();
          if (DataTable_AMSPIFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AMSPIFormDetail.Rows)
            {
              AMSPIInterventionId = DataRow_Row["AMSPI_Intervention_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
              AMSPIInterventionPatientVisitNumber = DataRow_Row["AMSPI_Intervention_PatientVisitNumber"].ToString();
            }
          }
          else
          {
            AMSPIInterventionId = "";
            FacilityId = "";
            AMSPIInterventionPatientVisitNumber = "";
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_AMSPI.aspx?s_Facility_Id=" + FacilityId + "&s_AMSPI_PatientVisitNumber=" + AMSPIInterventionPatientVisitNumber + "&AMSPI_Intervention_Id=" + AMSPIInterventionId + "&ViewMode=0";
      Hyperlink_Captured.NavigateUrl = "Form_AMSPI.aspx";

      AMSPIInterventionId = "";
      FacilityId = "";
      AMSPIInterventionPatientVisitNumber = "";
    }

    private void Form_PROMS()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string PROMSQuestionnaireId = "";
      string FacilityId = "";
      string PROMSQuestionnairePatientVisitNumber = "";
      string SQLStringPROMSFormDetail = "SELECT PROMS_Questionnaire_Id , Facility_Id , PROMS_Questionnaire_PatientVisitNumber FROM InfoQuest_Form_PROMS_Questionnaire WHERE PROMS_Questionnaire_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_PROMSFormDetail = new SqlCommand(SQLStringPROMSFormDetail))
      {
        SqlCommand_PROMSFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_PROMSFormDetail;
        using (DataTable_PROMSFormDetail = new DataTable())
        {
          DataTable_PROMSFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_PROMSFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSFormDetail).Copy();
          if (DataTable_PROMSFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PROMSFormDetail.Rows)
            {
              PROMSQuestionnaireId = DataRow_Row["PROMS_Questionnaire_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
              PROMSQuestionnairePatientVisitNumber = DataRow_Row["PROMS_Questionnaire_PatientVisitNumber"].ToString();
            }
          }
          else
          {
            PROMSQuestionnaireId = "";
            FacilityId = "";
            PROMSQuestionnairePatientVisitNumber = "";
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_PROMS.aspx?s_Facility_Id=" + FacilityId + "&s_PROMS_PatientVisitNumber=" + PROMSQuestionnairePatientVisitNumber + "&PROMS_Questionnaire_Id=" + PROMSQuestionnaireId + "&ViewModeQ=0&FollowUp=0";
      Hyperlink_Captured.NavigateUrl = "Form_PROMS.aspx";

      PROMSQuestionnaireId = "";
      FacilityId = "";
      PROMSQuestionnairePatientVisitNumber = "";
    }

    private void Form_Incident()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string IncidentId = "";
      string FacilityId = "";
      string SQLStringIncidentFormDetail = "SELECT Incident_Id , Facility_Id FROM Form_Incident WHERE Incident_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_IncidentFormDetail = new SqlCommand(SQLStringIncidentFormDetail))
      {
        SqlCommand_IncidentFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_IncidentFormDetail;
        using (DataTable_IncidentFormDetail = new DataTable())
        {
          DataTable_IncidentFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_IncidentFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IncidentFormDetail).Copy();
          if (DataTable_IncidentFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_IncidentFormDetail.Rows)
            {
              IncidentId = DataRow_Row["Incident_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
            }
          }
          else
          {
            IncidentId = "";
            FacilityId = "";
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_Incident.aspx?s_Facility_Id=" + FacilityId + "&Incident_Id=" + IncidentId + "";
      Hyperlink_Captured.NavigateUrl = "Form_Incident.aspx?s_Facility_Id=" + FacilityId + "";

      IncidentId = "";
      FacilityId = "";
    }

    private void Form_Alert()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string AlertId = "";
      string FacilityId = "";
      string SQLStringAlertFormDetail = "SELECT Alert_Id , Facility_Id FROM InfoQuest_Form_Alert WHERE Alert_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_AlertFormDetail = new SqlCommand(SQLStringAlertFormDetail))
      {
        SqlCommand_AlertFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_AlertFormDetail;
        using (DataTable_AlertFormDetail = new DataTable())
        {
          DataTable_AlertFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_AlertFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AlertFormDetail).Copy();
          if (DataTable_AlertFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AlertFormDetail.Rows)
            {
              AlertId = DataRow_Row["Alert_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
            }
          }
          else
          {
            AlertId = "";
            FacilityId = "";
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_Alert.aspx?s_Facility_Id=" + FacilityId + "&Alert_Id=" + AlertId + "";
      Hyperlink_Captured.NavigateUrl = "Form_Alert.aspx?s_Facility_Id=" + FacilityId + "";

      AlertId = "";
      FacilityId = "";
    }

    private void Form_Pharmacy_NewProduct()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string NewProductId = "";
      string SQLStringPharmacyNewProductFormDetail = "SELECT NewProduct_Id FROM InfoQuest_Form_Pharmacy_NewProduct WHERE NewProduct_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_PharmacyNewProductFormDetail = new SqlCommand(SQLStringPharmacyNewProductFormDetail))
      {
        SqlCommand_PharmacyNewProductFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_PharmacyNewProductFormDetail;
        using (DataTable_PharmacyNewProductFormDetail = new DataTable())
        {
          DataTable_PharmacyNewProductFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_PharmacyNewProductFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PharmacyNewProductFormDetail).Copy();
          if (DataTable_PharmacyNewProductFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PharmacyNewProductFormDetail.Rows)
            {
              NewProductId = DataRow_Row["NewProduct_Id"].ToString();
            }
          }
          else
          {
            NewProductId = "";
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_Pharmacy_NewProduct.aspx?NewProduct_Id=" + NewProductId + "";
      Hyperlink_Captured.NavigateUrl = "Form_Pharmacy_NewProduct.aspx";

      NewProductId = "";
    }

    private void Form_MHQ14()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string MHQ14QuestionnaireId = "";
      string FacilityId = "";
      string MHQ14QuestionnairePatientVisitNumber = "";
      string SQLStringMHQ14FormDetail = "SELECT MHQ14_Questionnaire_Id , Facility_Id , MHQ14_Questionnaire_PatientVisitNumber FROM InfoQuest_Form_MHQ14_Questionnaire WHERE MHQ14_Questionnaire_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_MHQ14FormDetail = new SqlCommand(SQLStringMHQ14FormDetail))
      {
        SqlCommand_MHQ14FormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_MHQ14FormDetail;
        using (DataTable_MHQ14FormDetail = new DataTable())
        {
          DataTable_MHQ14FormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_MHQ14FormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHQ14FormDetail).Copy();
          if (DataTable_MHQ14FormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MHQ14FormDetail.Rows)
            {
              MHQ14QuestionnaireId = DataRow_Row["MHQ14_Questionnaire_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
              MHQ14QuestionnairePatientVisitNumber = DataRow_Row["MHQ14_Questionnaire_PatientVisitNumber"].ToString();
            }
          }
          else
          {
            MHQ14QuestionnaireId = "";
            FacilityId = "";
            MHQ14QuestionnairePatientVisitNumber = "";
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_MHQ14.aspx?s_Facility_Id=" + FacilityId + "&s_MHQ14_PatientVisitNumber=" + MHQ14QuestionnairePatientVisitNumber + "&MHQ14_Questionnaire_Id=" + MHQ14QuestionnaireId + "";
      Hyperlink_Captured.NavigateUrl = "Form_MHQ14.aspx";

      MHQ14QuestionnaireId = "";
      FacilityId = "";
      MHQ14QuestionnairePatientVisitNumber = "";
    }

    private void Form_BundleCompliance()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string BCBundlesId = "";
      string FacilityId = "";
      string BCBundlesPatientVisitNumber = "";
      string SQLStringBundleComplianceFormDetail = "SELECT BC_Bundles_Id , Facility_Id , BC_Bundles_PatientVisitNumber FROM InfoQuest_Form_BundleCompliance_Bundles WHERE BC_Bundles_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_BundleComplianceFormDetail = new SqlCommand(SQLStringBundleComplianceFormDetail))
      {
        SqlCommand_BundleComplianceFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_BundleComplianceFormDetail;
        using (DataTable_BundleComplianceFormDetail = new DataTable())
        {
          DataTable_BundleComplianceFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_BundleComplianceFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_BundleComplianceFormDetail).Copy();
          if (DataTable_BundleComplianceFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_BundleComplianceFormDetail.Rows)
            {
              BCBundlesId = DataRow_Row["BC_Bundles_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
              BCBundlesPatientVisitNumber = DataRow_Row["BC_Bundles_PatientVisitNumber"].ToString();
            }
          }
          else
          {
            BCBundlesId = "";
            FacilityId = "";
            BCBundlesPatientVisitNumber = "";
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_BundleCompliance.aspx?s_Facility_Id=" + FacilityId + "&s_BundleCompliance_PatientVisitNumber=" + BCBundlesPatientVisitNumber + "&BC_Bundles_Id=" + BCBundlesId + "";
      Hyperlink_Captured.NavigateUrl = "Form_BundleCompliance.aspx";

      BCBundlesId = "";
      FacilityId = "";
      BCBundlesPatientVisitNumber = "";
    }

    private void Form_RehabBundleCompliance()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string RBCBundlesId = "";
      string FacilityId = "";
      string RBCBundlesPatientVisitNumber = "";
      string SQLStringRehabBundleComplianceFormDetail = "SELECT RBC_Bundles_Id , Facility_Id , RBC_Bundles_PatientVisitNumber FROM InfoQuest_Form_RehabBundleCompliance_Bundles WHERE RBC_Bundles_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_RehabBundleComplianceFormDetail = new SqlCommand(SQLStringRehabBundleComplianceFormDetail))
      {
        SqlCommand_RehabBundleComplianceFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_RehabBundleComplianceFormDetail;
        using (DataTable_RehabBundleComplianceFormDetail = new DataTable())
        {
          DataTable_RehabBundleComplianceFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_RehabBundleComplianceFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_RehabBundleComplianceFormDetail).Copy();
          if (DataTable_RehabBundleComplianceFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_RehabBundleComplianceFormDetail.Rows)
            {
              RBCBundlesId = DataRow_Row["RBC_Bundles_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
              RBCBundlesPatientVisitNumber = DataRow_Row["RBC_Bundles_PatientVisitNumber"].ToString();
            }
          }
          else
          {
            RBCBundlesId = "";
            FacilityId = "";
            RBCBundlesPatientVisitNumber = "";
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_RehabBundleCompliance.aspx?s_Facility_Id=" + FacilityId + "&s_RehabBundleCompliance_PatientVisitNumber=" + RBCBundlesPatientVisitNumber + "&RBC_Bundles_Id=" + RBCBundlesId + "";
      Hyperlink_Captured.NavigateUrl = "Form_RehabBundleCompliance.aspx";

      RBCBundlesId = "";
      FacilityId = "";
      RBCBundlesPatientVisitNumber = "";
    }

    private void Form_CRM()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string CRMId = "";
      string SQLStringCRMFormDetail = "SELECT CRM_Id FROM Form_CRM WHERE CRM_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_CRMFormDetail = new SqlCommand(SQLStringCRMFormDetail))
      {
        SqlCommand_CRMFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_CRMFormDetail;
        using (DataTable_CRMFormDetail = new DataTable())
        {
          DataTable_CRMFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_CRMFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMFormDetail).Copy();
          if (DataTable_CRMFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CRMFormDetail.Rows)
            {
              CRMId = DataRow_Row["CRM_Id"].ToString();
            }
          }
        }
      }

      Hyperlink_View.NavigateUrl = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Form", "Form_CRM.aspx?CRM_Id=" + CRMId + "");
      //Hyperlink_View.NavigateUrl = "Form_CRM.aspx?CRM_Id=" + CRMId + "";
      Hyperlink_Captured.NavigateUrl = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Form", "Form_CRM.aspx");
      //Hyperlink_Captured.NavigateUrl = "Form_CRM.aspx";

      CRMId = "";
    }

    private void Form_HAI()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string pkiInfectionFormID = "";
      string fkiFacilityID = "";
      string sPatientVisitNumber = "";
      string SQLStringHAIFormDetail = "SELECT pkiInfectionFormID , fkiFacilityID , sPatientVisitNumber FROM tblInfectionPrevention WHERE sReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_HAIFormDetail = new SqlCommand(SQLStringHAIFormDetail))
      {
        SqlCommand_HAIFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_HAIFormDetail;
        using (DataTable_HAIFormDetail = new DataTable())
        {
          DataTable_HAIFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_HAIFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAIFormDetail).Copy();
          if (DataTable_HAIFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HAIFormDetail.Rows)
            {
              pkiInfectionFormID = DataRow_Row["pkiInfectionFormID"].ToString();
              fkiFacilityID = DataRow_Row["fkiFacilityID"].ToString();
              sPatientVisitNumber = DataRow_Row["sPatientVisitNumber"].ToString();
            }
          }
        }
      }

      Hyperlink_View.NavigateUrl = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention New Form", "Form_InfectionPrevention.aspx?InfectionFormID=" + pkiInfectionFormID + "&s_FacilityId=" + fkiFacilityID + "&s_PatientVisitNumber=" + sPatientVisitNumber + "");
      Hyperlink_Captured.NavigateUrl = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention New Form", "Form_InfectionPrevention.aspx");

      pkiInfectionFormID = "";
      fkiFacilityID = "";
      sPatientVisitNumber = "";
    }

    private void Form_MedicationBundleCompliance()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string MBCBundlesId = "";
      string MBCVisitInformationId = "";
      string SQLStringMedicationBundleComplianceFormDetail = "SELECT MBC_Bundles_Id , MBC_VisitInformation_Id FROM Form_MedicationBundleCompliance_Bundles WHERE MBC_Bundles_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_MedicationBundleComplianceFormDetail = new SqlCommand(SQLStringMedicationBundleComplianceFormDetail))
      {
        SqlCommand_MedicationBundleComplianceFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_MedicationBundleComplianceFormDetail;
        using (DataTable_MedicationBundleComplianceFormDetail = new DataTable())
        {
          DataTable_MedicationBundleComplianceFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_MedicationBundleComplianceFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MedicationBundleComplianceFormDetail).Copy();
          if (DataTable_MedicationBundleComplianceFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MedicationBundleComplianceFormDetail.Rows)
            {
              MBCBundlesId = DataRow_Row["MBC_Bundles_Id"].ToString();
              MBCVisitInformationId = DataRow_Row["MBC_VisitInformation_Id"].ToString();
            }
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_MedicationBundleCompliance.aspx?MBCVisitInformationId=" + MBCVisitInformationId + "&MBCBundlesId=" + MBCBundlesId + "";
      Hyperlink_Captured.NavigateUrl = "Form_MedicationBundleCompliance.aspx";

      MBCBundlesId = "";
      MBCVisitInformationId = "";
    }

    private void Form_AntimicrobialStewardshipIntervention()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string ASIInterventionId = "";
      string ASIVisitInformationId = "";
      string SQLStringAntimicrobialStewardshipInterventionFormDetail = "SELECT ASI_Intervention_Id , ASI_VisitInformation_Id FROM Form_AntimicrobialStewardshipIntervention_Intervention WHERE ASI_Intervention_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_AntimicrobialStewardshipInterventionFormDetail = new SqlCommand(SQLStringAntimicrobialStewardshipInterventionFormDetail))
      {
        SqlCommand_AntimicrobialStewardshipInterventionFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_AntimicrobialStewardshipInterventionFormDetail;
        using (DataTable_AntimicrobialStewardshipInterventionFormDetail = new DataTable())
        {
          DataTable_AntimicrobialStewardshipInterventionFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_AntimicrobialStewardshipInterventionFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntimicrobialStewardshipInterventionFormDetail).Copy();
          if (DataTable_AntimicrobialStewardshipInterventionFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AntimicrobialStewardshipInterventionFormDetail.Rows)
            {
              ASIInterventionId = DataRow_Row["ASI_Intervention_Id"].ToString();
              ASIVisitInformationId = DataRow_Row["ASI_VisitInformation_Id"].ToString();
            }
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + ASIVisitInformationId + "&ASIInterventionId=" + ASIInterventionId + "";
      Hyperlink_Captured.NavigateUrl = "Form_AntimicrobialStewardshipIntervention.aspx";

      ASIInterventionId = "";
      ASIVisitInformationId = "";
    }

    private void Form_VTE()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string VTEAssesmentsId = "";
      string VTEVisitInformationId = "";
      string SQLStringVTEFormDetail = "SELECT VTE_Assesments_Id , VTE_VisitInformation_Id FROM Form_VTE_Assesments WHERE VTE_Assesments_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_VTEFormDetail = new SqlCommand(SQLStringVTEFormDetail))
      {
        SqlCommand_VTEFormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_VTEFormDetail;
        using (DataTable_VTEFormDetail = new DataTable())
        {
          DataTable_VTEFormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_VTEFormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VTEFormDetail).Copy();
          if (DataTable_VTEFormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VTEFormDetail.Rows)
            {
              VTEAssesmentsId = DataRow_Row["VTE_Assesments_Id"].ToString();
              VTEVisitInformationId = DataRow_Row["VTE_VisitInformation_Id"].ToString();
            }
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_VTE.aspx?VTEVisitInformationId=" + VTEVisitInformationId + "&VTEAssesmentsId=" + VTEAssesmentsId + "";
      Hyperlink_Captured.NavigateUrl = "Form_VTE.aspx";

      VTEAssesmentsId = "";
      VTEVisitInformationId = "";
    }

    private void Form_PharmacyClinicalMetrics_TherapeuticIntervention()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string PCMTIId = "";
      string PCMInterventionId = "";
      string SQLStringFormDetail = "SELECT PCM_TI_Id , PCM_Intervention_Id FROM Form_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_TI_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_FormDetail = new SqlCommand(SQLStringFormDetail))
      {
        SqlCommand_FormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_FormDetail;
        using (DataTable_FormDetail = new DataTable())
        {
          DataTable_FormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_FormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormDetail).Copy();
          if (DataTable_FormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormDetail.Rows)
            {
              PCMTIId = DataRow_Row["PCM_TI_Id"].ToString();
              PCMInterventionId = DataRow_Row["PCM_Intervention_Id"].ToString();
            }
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + PCMInterventionId + "&PCMTIId=" + PCMTIId + "";
      Hyperlink_Captured.NavigateUrl = "Form_PharmacyClinicalMetrics.aspx";

      PCMTIId = "";
      PCMInterventionId = "";
    }

    private void Form_PharmacyClinicalMetrics_PharmacistTime()
    {
      Label_FormName.Text = InfoQuestWCF.InfoQuest_All.All_FormName(FormId(Request.QueryString["ReportPage"]));
      Label_ReportNumber.Text = Request.QueryString["ReportNumber"];

      string PCMPTId = "";
      string PCMInterventionId = "";
      string SQLStringFormDetail = "SELECT PCM_PT_Id , PCM_Intervention_Id FROM Form_PharmacyClinicalMetrics_PharmacistTime WHERE PCM_PT_ReportNumber = @ReportNumber";
      using (SqlCommand SqlCommand_FormDetail = new SqlCommand(SQLStringFormDetail))
      {
        SqlCommand_FormDetail.Parameters.AddWithValue("@ReportNumber", Request.QueryString["ReportNumber"]);
        DataTable DataTable_FormDetail;
        using (DataTable_FormDetail = new DataTable())
        {
          DataTable_FormDetail.Locale = CultureInfo.CurrentCulture;
          DataTable_FormDetail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormDetail).Copy();
          if (DataTable_FormDetail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormDetail.Rows)
            {
              PCMPTId = DataRow_Row["PCM_PT_Id"].ToString();
              PCMInterventionId = DataRow_Row["PCM_Intervention_Id"].ToString();
            }
          }
        }
      }

      Hyperlink_View.NavigateUrl = "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + PCMInterventionId + "&PCMPTId=" + PCMPTId + "";
      Hyperlink_Captured.NavigateUrl = "Form_PharmacyClinicalMetrics.aspx";

      PCMPTId = "";
      PCMInterventionId = "";
    }
  }
}