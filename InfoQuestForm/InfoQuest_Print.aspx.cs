using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace InfoQuestForm
{
  public partial class InfoQuest_Print : InfoQuestWCF.Override_SystemWebUIPage
  {
    private Dictionary<string, Action> FormHandler = new Dictionary<string, Action>();
    private Dictionary<string, string> FormIdHandler = new Dictionary<string, string>();
    private Dictionary<string, HtmlTable> NavigationHandler = new Dictionary<string, HtmlTable>();
    private Dictionary<string, HtmlTable> ParameterHandler = new Dictionary<string, HtmlTable>();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["PrintPage"] != null && Request.QueryString["PrintValue"] != null)
          {
            TablePrint.Visible = true;
            ReportViewer_Print.Visible = true;
            ReportViewer_Print.Reset();
            ReportViewer_Print.ProcessingMode = ProcessingMode.Local;

            //ReportViewer_RemoveExportButton(ReportViewer_Print, "Excel");
            //ReportViewer_RemoveExportButton(ReportViewer_Print, "Word");

            Form(Request.QueryString["PrintPage"]);

            ReportViewer_Print.LocalReport.Refresh();
          }
          else
          {
            TablePrint.Visible = false;
            ReportViewer_Print.Visible = false;
          }

          if (TablePrint.Visible == true)
          {
            Navigation(Request.QueryString["PrintPage"]);
            Parameter(Request.QueryString["PrintPage"]);
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
          SqlCommand_Security.Parameters.AddWithValue("@Form_Id", FormId(Request.QueryString["PrintPage"]));

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          string FormIdValue = FormId(Request.QueryString["PrintPage"]);

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Print.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Parameter_Form_HAI_Site.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Parameter_Form_HAI_Site.SelectCommand = "SELECT DISTINCT iSiteNumber FROM tblInfectionPrevention_Site WHERE (fkiInfectionFormID = @PrintValue) ORDER BY iSiteNumber";
      SqlDataSource_Parameter_Form_HAI_Site.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Parameter_Form_HAI_Site.SelectParameters.Clear();
      SqlDataSource_Parameter_Form_HAI_Site.SelectParameters.Add("PrintValue", TypeCode.String, Request.QueryString["PrintValue"]);

      SqlDataSource_Parameter_Form_IPS_Infection.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Parameter_Form_IPS_Infection.SelectCommand = "SELECT IPS_Infection_Id , IPS_Infection_ReportNumber FROM Form_IPS_Infection WHERE IPS_VisitInformation_Id IN ( SELECT IPS_VisitInformation_Id FROM Form_IPS_Infection WHERE IPS_Infection_Id = @PrintValue )";
      SqlDataSource_Parameter_Form_IPS_Infection.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Parameter_Form_IPS_Infection.SelectParameters.Clear();
      SqlDataSource_Parameter_Form_IPS_Infection.SelectParameters.Add("PrintValue", TypeCode.String, Request.QueryString["PrintValue"]);
    }

    #region ReportViewer_RemoveExportButton
    //private static void ReportViewer_RemoveExportButton(ReportViewer reportViewer, String optionToRemove)
    //{
    //  var ExportList = reportViewer.LocalReport.ListRenderingExtensions();
    //  foreach (var ExportItem in ExportList)
    //  {
    //    if (ExportItem.Name.Trim().ToUpper(CultureInfo.CurrentCulture) == optionToRemove.Trim().ToUpper(CultureInfo.CurrentCulture)) // Hide the option
    //    {
    //      ExportItem.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(ExportItem, false);
    //    }
    //  }
    //}
    #endregion



    // TODO : New Form : InfoQuest_Print : Add new Form to FormHandlers()
    protected void FormHandlers()
    {
      FormHandler.Add("Form_FIMFAM", new Action(Form_FIMFAM));
      FormHandler.Add("Form_Isidima", new Action(Form_Isidima));
      FormHandler.Add("Form_AMSPI", new Action(Form_AMSPI));
      FormHandler.Add("Form_PROMS", new Action(Form_PROMS));
      FormHandler.Add("Form_QMSReview_Findings", new Action(Form_QMSReview_Findings));
      FormHandler.Add("Form_CollegeLearningAudit_Findings", new Action(Form_CollegeLearningAudit_Findings));
      FormHandler.Add("Form_OccupationalHealthAudit_Findings", new Action(Form_OccupationalHealthAudit_Findings));
      FormHandler.Add("Form_MonthlyHospitalStatistics", new Action(Form_MonthlyHospitalStatistics));
      FormHandler.Add("Form_MonthlyPharmacyStatistics", new Action(Form_MonthlyPharmacyStatistics));
      FormHandler.Add("Form_Incident", new Action(Form_Incident));
      FormHandler.Add("Form_Alert", new Action(Form_Alert));
      FormHandler.Add("Form_Pharmacy_NewProduct", new Action(Form_Pharmacy_NewProduct));
      FormHandler.Add("Form_Pharmacy_DeadStock", new Action(Form_Pharmacy_DeadStock));
      FormHandler.Add("Form_Pharmacy_Formulary", new Action(Form_Pharmacy_Formulary));
      FormHandler.Add("Form_Pharmacy_Hotline", new Action(Form_Pharmacy_Hotline));
      FormHandler.Add("Form_Pharmacy_Product", new Action(Form_Pharmacy_Product));
      FormHandler.Add("Form_Pharmacy_ProductComplaint", new Action(Form_Pharmacy_ProductComplaint));
      FormHandler.Add("Form_Pharmacy_UPD", new Action(Form_Pharmacy_UPD));
      FormHandler.Add("Form_Pharmacy_Vendor", new Action(Form_Pharmacy_Vendor));
      FormHandler.Add("Form_ECM", new Action(Form_ECM));
      FormHandler.Add("Form_EquipmentClassifiedSystem_Request", new Action(Form_EquipmentClassifiedSystem_Request));
      FormHandler.Add("Form_EquipmentClassifiedSystem_Available", new Action(Form_EquipmentClassifiedSystem_Available));
      FormHandler.Add("Form_BundleCompliance", new Action(Form_BundleCompliance));
      FormHandler.Add("Form_BundleCompliance_All", new Action(Form_BundleCompliance_All));
      FormHandler.Add("Form_RehabBundleCompliance", new Action(Form_RehabBundleCompliance));
      FormHandler.Add("Form_RehabBundleCompliance_All", new Action(Form_RehabBundleCompliance_All));
      FormHandler.Add("Form_ClinicalPracticeAudit", new Action(Form_ClinicalPracticeAudit));
      FormHandler.Add("Form_ClinicalPracticeAudit_All", new Action(Form_ClinicalPracticeAudit_All));
      FormHandler.Add("Form_HAI", new Action(Form_HAI));
      FormHandler.Add("Form_HAI_Patient", new Action(Form_HAI_Patient));
      FormHandler.Add("Form_HAI_Patient_Extra", new Action(Form_HAI_Patient_Extra));
      FormHandler.Add("Form_HAI_Site", new Action(Form_HAI_Site));
      FormHandler.Add("Form_HAI_Investigation", new Action(Form_HAI_Investigation));
      FormHandler.Add("Form_MHQ14", new Action(Form_MHQ14));
      FormHandler.Add("Form_CRM", new Action(Form_CRM));
      FormHandler.Add("Form_IPS", new Action(Form_IPS));
      FormHandler.Add("Form_IPS_InfectionDetail", new Action(Form_IPS_InfectionDetail));
      FormHandler.Add("Form_IPS_InfectionDetail_Extra", new Action(Form_IPS_InfectionDetail_Extra));
      FormHandler.Add("Form_IPS_Investigation", new Action(Form_IPS_Investigation));
      FormHandler.Add("Form_MedicationBundleCompliance", new Action(Form_MedicationBundleCompliance));
      FormHandler.Add("Form_MedicationBundleCompliance_All", new Action(Form_MedicationBundleCompliance_All));
      FormHandler.Add("Form_AntimicrobialStewardshipIntervention", new Action(Form_AntimicrobialStewardshipIntervention));
      FormHandler.Add("Form_AntimicrobialStewardshipIntervention_All", new Action(Form_AntimicrobialStewardshipIntervention_All));
      FormHandler.Add("Form_HeadOfficeQualityAudit", new Action(Form_HeadOfficeQualityAudit));
      FormHandler.Add("Form_TransparencyRegister", new Action(Form_TransparencyRegister));
      FormHandler.Add("Form_SustainabilityManagement", new Action(Form_SustainabilityManagement));
      FormHandler.Add("Form_MonthlyOccupationalHealthStatistics", new Action(Form_MonthlyOccupationalHealthStatistics));
      FormHandler.Add("Form_PharmacySurveys", new Action(Form_PharmacySurveys));
      FormHandler.Add("Form_VTE", new Action(Form_VTE));
      FormHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention", new Action(Form_PharmacyClinicalMetrics_TherapeuticIntervention));
      FormHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention_All", new Action(Form_PharmacyClinicalMetrics_TherapeuticIntervention_All));
      FormHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime", new Action(Form_PharmacyClinicalMetrics_PharmacistTime));
      FormHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime_All", new Action(Form_PharmacyClinicalMetrics_PharmacistTime_All));
    }

    // TODO : New Form : InfoQuest_Print : Add new Form to FormIdHandlers()
    protected void FormIdHandlers()
    {
      FormIdHandler.Add("Form_Incident", "1");
      FormIdHandler.Add("Form_Alert", "2");
      FormIdHandler.Add("Form_HAI", "37");
      FormIdHandler.Add("Form_HAI_Patient", "37");
      FormIdHandler.Add("Form_HAI_Patient_Extra", "37");
      FormIdHandler.Add("Form_HAI_Site", "37");
      FormIdHandler.Add("Form_HAI_Investigation", "37");
      FormIdHandler.Add("Form_MonthlyHospitalStatistics", "5");
      FormIdHandler.Add("Form_Pharmacy_Hotline", "6");
      FormIdHandler.Add("Form_Pharmacy_Formulary", "12");
      FormIdHandler.Add("Form_Pharmacy_ProductComplaint", "13");
      FormIdHandler.Add("Form_Pharmacy_Vendor", "14");
      FormIdHandler.Add("Form_Pharmacy_UPD", "16");
      FormIdHandler.Add("Form_BundleCompliance", "17");
      FormIdHandler.Add("Form_BundleCompliance_All", "17");
      FormIdHandler.Add("Form_Pharmacy_Product", "18");
      FormIdHandler.Add("Form_ClinicalPracticeAudit", "19");
      FormIdHandler.Add("Form_ClinicalPracticeAudit_All", "19");
      FormIdHandler.Add("Form_Pharmacy_DeadStock", "21");
      FormIdHandler.Add("Form_ECM", "22");
      FormIdHandler.Add("Form_EquipmentClassifiedSystem_Request", "24");
      FormIdHandler.Add("Form_EquipmentClassifiedSystem_Available", "24");
      FormIdHandler.Add("Form_FIMFAM", "25");
      FormIdHandler.Add("Form_Isidima", "27");
      FormIdHandler.Add("Form_AMSPI", "29");
      FormIdHandler.Add("Form_PROMS", "30");
      FormIdHandler.Add("Form_QMSReview_Findings", "31");
      FormIdHandler.Add("Form_CollegeLearningAudit_Findings", "49");
      FormIdHandler.Add("Form_OccupationalHealthAudit_Findings", "48");
      FormIdHandler.Add("Form_MonthlyPharmacyStatistics", "32");
      FormIdHandler.Add("Form_Pharmacy_NewProduct", "33");
      FormIdHandler.Add("Form_MHQ14", "34");
      FormIdHandler.Add("Form_RehabBundleCompliance", "35");
      FormIdHandler.Add("Form_RehabBundleCompliance_All", "35");
      FormIdHandler.Add("Form_CRM", "36");
      FormIdHandler.Add("Form_IPS", "37");
      FormIdHandler.Add("Form_IPS_InfectionDetail", "37");
      FormIdHandler.Add("Form_IPS_InfectionDetail_Extra", "37");
      FormIdHandler.Add("Form_IPS_Investigation", "37");
      FormIdHandler.Add("Form_MedicationBundleCompliance", "38");
      FormIdHandler.Add("Form_MedicationBundleCompliance_All", "38");
      FormIdHandler.Add("Form_AntimicrobialStewardshipIntervention", "39");
      FormIdHandler.Add("Form_AntimicrobialStewardshipIntervention_All", "39");
      FormIdHandler.Add("Form_HeadOfficeQualityAudit", "40");
      FormIdHandler.Add("Form_TransparencyRegister", "44");
      FormIdHandler.Add("Form_SustainabilityManagement", "45");
      FormIdHandler.Add("Form_MonthlyOccupationalHealthStatistics", "46");
      FormIdHandler.Add("Form_PharmacySurveys", "47");
      FormIdHandler.Add("Form_VTE", "51");
      FormIdHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention", "52");
      FormIdHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention_All", "52");
      FormIdHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime", "52");
      FormIdHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime_All", "52");
    }

    // TODO : New Form : InfoQuest_Print : Add new Form to NavigationHandlers()
    protected void NavigationHandlers()
    {
      NavigationHandler.Add("Form_HAI", TableNavigation_Form_HAI);
      NavigationHandler.Add("Form_HAI_Patient", TableNavigation_Form_HAI);
      NavigationHandler.Add("Form_HAI_Patient_Extra", TableNavigation_Form_HAI);
      NavigationHandler.Add("Form_HAI_Site", TableNavigation_Form_HAI);
      NavigationHandler.Add("Form_HAI_Investigation", TableNavigation_Form_HAI);
      NavigationHandler.Add("Form_BundleCompliance", TableNavigation_Form_BundleCompliance);
      NavigationHandler.Add("Form_BundleCompliance_All", TableNavigation_Form_BundleCompliance);
      NavigationHandler.Add("Form_ClinicalPracticeAudit", TableNavigation_Form_ClinicalPracticeAudit);
      NavigationHandler.Add("Form_ClinicalPracticeAudit_All", TableNavigation_Form_ClinicalPracticeAudit);
      NavigationHandler.Add("Form_RehabBundleCompliance", TableNavigation_Form_RehabBundleCompliance);
      NavigationHandler.Add("Form_RehabBundleCompliance_All", TableNavigation_Form_RehabBundleCompliance);
      NavigationHandler.Add("Form_IPS", TableNavigation_Form_IPS);
      NavigationHandler.Add("Form_IPS_InfectionDetail", TableNavigation_Form_IPS);
      NavigationHandler.Add("Form_IPS_InfectionDetail_Extra", TableNavigation_Form_IPS);
      NavigationHandler.Add("Form_IPS_Investigation", TableNavigation_Form_IPS);
      NavigationHandler.Add("Form_MedicationBundleCompliance", TableNavigation_Form_MedicationBundleCompliance);
      NavigationHandler.Add("Form_MedicationBundleCompliance_All", TableNavigation_Form_MedicationBundleCompliance);
      NavigationHandler.Add("Form_AntimicrobialStewardshipIntervention", TableNavigation_Form_AntimicrobialStewardshipIntervention);
      NavigationHandler.Add("Form_AntimicrobialStewardshipIntervention_All", TableNavigation_Form_AntimicrobialStewardshipIntervention);
      NavigationHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention", TableNavigation_Form_PharmacyClinicalMetrics_TherapeuticIntervention);
      NavigationHandler.Add("Form_PharmacyClinicalMetrics_TherapeuticIntervention_All", TableNavigation_Form_PharmacyClinicalMetrics_TherapeuticIntervention);
      NavigationHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime", TableNavigation_Form_PharmacyClinicalMetrics_PharmacistTime);
      NavigationHandler.Add("Form_PharmacyClinicalMetrics_PharmacistTime_All", TableNavigation_Form_PharmacyClinicalMetrics_PharmacistTime);

    }

    // TODO : New Form : InfoQuest_Print : Add new Form to ParameterHandlers()
    protected void ParameterHandlers()
    {
      ParameterHandler.Add("Form_HAI", TableParameters_Form_HAI);
      ParameterHandler.Add("Form_HAI_Site", TableParameters_Form_HAI);
      ParameterHandler.Add("Form_IPS", TableParameters_Form_IPS);
      ParameterHandler.Add("Form_IPS_InfectionDetail", TableParameters_Form_IPS);
      ParameterHandler.Add("Form_IPS_InfectionDetail_Extra", TableParameters_Form_IPS);
      ParameterHandler.Add("Form_IPS_Investigation", TableParameters_Form_IPS);
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

    protected void Navigation(string formName)
    {
      if (formName != null)
      {
        NavigationHandlers();

        if (NavigationHandler.ContainsKey(formName))
        {
          HtmlTable NavigationValue = NavigationHandler[formName];
          NavigationHandler.Clear();
          NavigationValue.Visible = true;
        }
      }
    }

    protected void Parameter(string formName)
    {
      if (formName != null)
      {
        ParameterHandlers();

        if (ParameterHandler.ContainsKey(formName))
        {
          HtmlTable ParameterValue = ParameterHandler[formName];
          ParameterHandler.Clear();
          ParameterValue.Visible = true;
        }
      }
    }

    
    //--START-- --Navigation--//
    protected void LinkButton_Navigation_Form_ClinicalPracticeAudit_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_ClinicalPracticeAudit&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_ClinicalPracticeAudit_All_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_ClinicalPracticeAudit_All&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }


    protected void LinkButton_Navigation_Form_BundleCompliance_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_BundleCompliance&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_BundleCompliance_All_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_BundleCompliance_All&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }


    protected void LinkButton_Navigation_Form_RehabBundleCompliance_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_RehabBundleCompliance&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_RehabBundleCompliance_All_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_RehabBundleCompliance_All&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }


    protected void LinkButton_Navigation_Form_MedicationBundleCompliance_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_MedicationBundleCompliance&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_MedicationBundleCompliance_All_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_MedicationBundleCompliance_All&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }


    protected void LinkButton_Navigation_Form_AntimicrobialStewardshipIntervention_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_AntimicrobialStewardshipIntervention&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_AntimicrobialStewardshipIntervention_All_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_AntimicrobialStewardshipIntervention_All&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }


    protected void LinkButton_Navigation_Form_PharmacyClinicalMetrics_TherapeuticIntervention_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_PharmacyClinicalMetrics_TherapeuticIntervention&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_PharmacyClinicalMetrics_TherapeuticIntervention_All_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_PharmacyClinicalMetrics_TherapeuticIntervention_All&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }


    protected void LinkButton_Navigation_Form_PharmacyClinicalMetrics_PharmacistTime_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_PharmacyClinicalMetrics_PharmacistTime&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_PharmacyClinicalMetrics_PharmacistTime_All_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_PharmacyClinicalMetrics_PharmacistTime_All&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }


    protected void LinkButton_Navigation_Form_HAI_All_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_HAI&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_HAI_PatientSection_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_HAI_Patient&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_HAI_PatientSection_Extra_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_HAI_Patient_Extra&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_HAI_PatientSite_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_HAI_Site&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_HAI_InvestigationSection_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_HAI_Investigation&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }


    protected void LinkButton_Navigation_Form_IPS_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_IPS&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_IPS_InfectionDetail_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_IPS_InfectionDetail&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_IPS_InfectionDetail_Extra_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_IPS_InfectionDetail_Extra&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }

    protected void LinkButton_Navigation_Form_IPS_Investigation_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=Form_IPS_Investigation&PrintValue=" + Request.QueryString["PrintValue"]), false);
    }
    //---END--- --Navigation--//


    //--START-- --Parameter--//
    protected void Button_Parameter_Form_HAI_Site_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=" + Request.QueryString["PrintPage"] + "&PrintValue=" + Request.QueryString["PrintValue"] + "&HAI_Site=" + DropDownList_Parameter_Form_HAI_Site.SelectedValue.ToString()), false);
    }

    protected void Button_Parameter_Form_IPS_Infection_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("InfoQuest_Print", "InfoQuest_Print.aspx?PrintPage=" + Request.QueryString["PrintPage"] + "&PrintValue=" + DropDownList_Parameter_Form_IPS_Infection.SelectedValue.ToString()), false);
    }

    protected void DropDownList_Parameter_Form_IPS_Infection_DataBound(object sender, EventArgs e)
    {
      DropDownList_Parameter_Form_IPS_Infection.SelectedValue = Request.QueryString["PrintValue"];
    }
    //---END--- --Parameter--//


    // TODO : New Form : InfoQuest_Print : Add new Procedure for Form
    private void Form_FIMFAM()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM ( SELECT TOP 6 * FROM vForm_FIMFAM TableA WHERE EXISTS ( SELECT Facility_Id , FIMFAM_Elements_PatientVisitNumber FROM vForm_FIMFAM TableB WHERE FIMFAM_Elements_Id = @PrintValue AND TableA.Facility_Id = TableB.Facility_Id AND TableA.FIMFAM_Elements_PatientVisitNumber = TableB.FIMFAM_Elements_PatientVisitNumber ) AND FIMFAM_Elements_IsActive = 1 ORDER BY FIMFAM_Elements_ObservationDate DESC , FIMFAM_Elements_CreatedDate DESC ) AS TempTable1 UNION SELECT * FROM ( SELECT TOP 1 * FROM vForm_FIMFAM TableA WHERE EXISTS (SELECT Facility_Id , FIMFAM_Elements_PatientVisitNumber FROM vForm_FIMFAM TableB WHERE FIMFAM_Elements_Id = @PrintValue AND TableA.Facility_Id = TableB.Facility_Id AND TableA.FIMFAM_Elements_PatientVisitNumber = TableB.FIMFAM_Elements_PatientVisitNumber ) AND FIMFAM_Elements_IsActive = 1 ORDER BY FIMFAM_Elements_ObservationDate , FIMFAM_Elements_CreatedDate ) AS TempTable2 ORDER BY FIMFAM_Elements_ObservationDate DESC , FIMFAM_Elements_CreatedDate DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT A.[LengthOfStay] , A.[FIMLatestScore] , A.[FAMLatestScore] , A.[TotalLatestScore] , B.[FIMAdmissionScore] , B.[FAMAdmissionScore] , B.[TotalAdmissionScore] , CASE WHEN A.[LengthOfStay] = 0 THEN 0 ELSE CAST(((A.[FIMLatestScore] - B.[FIMAdmissionScore]) / A.[LengthOfStay]) AS DECIMAL(18,2)) END AS [FIMEfficiencyLOS] , CASE WHEN A.[LengthOfStay] = 0 THEN 0 ELSE CAST(((A.[FAMLatestScore] - B.[FAMAdmissionScore]) / A.[LengthOfStay]) AS DECIMAL(18,2)) END AS [FAMEfficiencyLOS] , CASE WHEN A.[LengthOfStay] = 0 THEN 0 ELSE CAST(((A.[TotalLatestScore] - B.[TotalAdmissionScore]) / A.[LengthOfStay]) AS DECIMAL(18,2)) END AS [FIMFAMEfficiencyLOS] FROM ( SELECT TOP 1 FIMFAM_Elements_Id , FIMFAM_Elements_PatientVisitNumber , FIMFAM_PI_PatientDateOfAdmission , FIMFAM_Elements_ObservationDate , FIMFAM_Elements_CreatedDate , DATEDIFF(DAY , FIMFAM_PI_PatientDateOfAdmission , FIMFAM_Elements_ObservationDate) + 1 AS [LengthOfStay] , FIMFAM_Elements_Selfcare_Eating + FIMFAM_Elements_Selfcare_Grooming + FIMFAM_Elements_Selfcare_Bathing + FIMFAM_Elements_Selfcare_DressingUpper + FIMFAM_Elements_Selfcare_DressingLower + FIMFAM_Elements_Selfcare_Toileting + (CASE WHEN FIMFAM_Elements_Sphincter_Bladder1 <= FIMFAM_Elements_Sphincter_Bladder2 THEN FIMFAM_Elements_Sphincter_Bladder1 ELSE FIMFAM_Elements_Sphincter_Bladder2 END) + (CASE WHEN FIMFAM_Elements_Sphincter_Bowel1 <= FIMFAM_Elements_Sphincter_Bowel2 THEN FIMFAM_Elements_Sphincter_Bowel1 ELSE FIMFAM_Elements_Sphincter_Bowel2 END) + FIMFAM_Elements_Transfer_BCW + FIMFAM_Elements_Transfer_Toilet + FIMFAM_Elements_Transfer_TS + FIMFAM_Elements_Locomotion_WW + FIMFAM_Elements_Locomotion_Stairs + FIMFAM_Elements_Communication_AV + FIMFAM_Elements_Communication_VN + FIMFAM_Elements_PSAdjust_SocialInteraction + FIMFAM_Elements_CognitiveFunction_ProblemSolving + FIMFAM_Elements_CognitiveFunction_Memory AS [FIMLatestScore] , FIMFAM_Elements_Selfcare_Swallowing + FIMFAM_Elements_Transfer_CarTransfer + FIMFAM_Elements_Locomotion_CommunityAccess + FIMFAM_Elements_Communication_Reading + FIMFAM_Elements_Communication_Writing + FIMFAM_Elements_Communication_Speech + FIMFAM_Elements_PSAdjust_EmotionalStatus + FIMFAM_Elements_PSAdjust_Adjustment + FIMFAM_Elements_PSAdjust_Employability + FIMFAM_Elements_CognitiveFunction_Orientation + FIMFAM_Elements_CognitiveFunction_Attention + FIMFAM_Elements_CognitiveFunction_SafetyJudgement AS [FAMLatestScore] , FIMFAM_Elements_Total AS [TotalLatestScore] FROM vForm_FIMFAM TableA WHERE EXISTS ( SELECT Facility_Id , FIMFAM_Elements_PatientVisitNumber FROM vForm_FIMFAM TableB WHERE FIMFAM_Elements_Id = @PrintValue AND TableA.Facility_Id = TableB.Facility_Id AND TableA.FIMFAM_Elements_PatientVisitNumber = TableB.FIMFAM_Elements_PatientVisitNumber ) AND FIMFAM_Elements_IsActive = 1 ORDER BY FIMFAM_Elements_ObservationDate DESC , FIMFAM_Elements_CreatedDate DESC ) A INNER JOIN ( SELECT TOP 1 FIMFAM_Elements_Id , FIMFAM_Elements_PatientVisitNumber , FIMFAM_PI_PatientDateOfAdmission , FIMFAM_Elements_ObservationDate , FIMFAM_Elements_CreatedDate , DATEDIFF(DAY , FIMFAM_PI_PatientDateOfAdmission , FIMFAM_Elements_ObservationDate) + 1 AS [LengthOfStay] , FIMFAM_Elements_Selfcare_Eating + FIMFAM_Elements_Selfcare_Grooming + FIMFAM_Elements_Selfcare_Bathing + FIMFAM_Elements_Selfcare_DressingUpper + FIMFAM_Elements_Selfcare_DressingLower + FIMFAM_Elements_Selfcare_Toileting + (CASE WHEN FIMFAM_Elements_Sphincter_Bladder1 <= FIMFAM_Elements_Sphincter_Bladder2 THEN FIMFAM_Elements_Sphincter_Bladder1 ELSE FIMFAM_Elements_Sphincter_Bladder2 END) + (CASE WHEN FIMFAM_Elements_Sphincter_Bowel1 <= FIMFAM_Elements_Sphincter_Bowel2 THEN FIMFAM_Elements_Sphincter_Bowel1 ELSE FIMFAM_Elements_Sphincter_Bowel2 END) + FIMFAM_Elements_Transfer_BCW + FIMFAM_Elements_Transfer_Toilet + FIMFAM_Elements_Transfer_TS + FIMFAM_Elements_Locomotion_WW + FIMFAM_Elements_Locomotion_Stairs + FIMFAM_Elements_Communication_AV + FIMFAM_Elements_Communication_VN + FIMFAM_Elements_PSAdjust_SocialInteraction + FIMFAM_Elements_CognitiveFunction_ProblemSolving + FIMFAM_Elements_CognitiveFunction_Memory AS [FIMAdmissionScore] , FIMFAM_Elements_Selfcare_Swallowing + FIMFAM_Elements_Transfer_CarTransfer + FIMFAM_Elements_Locomotion_CommunityAccess + FIMFAM_Elements_Communication_Reading + FIMFAM_Elements_Communication_Writing + FIMFAM_Elements_Communication_Speech + FIMFAM_Elements_PSAdjust_EmotionalStatus + FIMFAM_Elements_PSAdjust_Adjustment + FIMFAM_Elements_PSAdjust_Employability + FIMFAM_Elements_CognitiveFunction_Orientation + FIMFAM_Elements_CognitiveFunction_Attention + FIMFAM_Elements_CognitiveFunction_SafetyJudgement AS [FAMAdmissionScore] , FIMFAM_Elements_Total AS [TotalAdmissionScore] FROM vForm_FIMFAM TableA WHERE EXISTS ( SELECT Facility_Id , FIMFAM_Elements_PatientVisitNumber FROM vForm_FIMFAM TableB WHERE FIMFAM_Elements_Id = @PrintValue AND TableA.Facility_Id = TableB.Facility_Id AND TableA.FIMFAM_Elements_PatientVisitNumber = TableB.FIMFAM_Elements_PatientVisitNumber ) AND FIMFAM_Elements_IsActive = 1 ORDER BY FIMFAM_Elements_ObservationDate , FIMFAM_Elements_CreatedDate ) B ON A.FIMFAM_Elements_PatientVisitNumber = B.FIMFAM_Elements_PatientVisitNumber";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_FIMFAM.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_FIMFAM", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_FIMFAM_Efficiency", SqlDataSource_Print2));
    }

    private void Form_Isidima()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "EXECUTE spForm_Get_Isidima_Print @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT Facility_FacilityDisplayName , Isidima_PI_PatientVisitNumber , Isidima_PI_PatientName FROM vForm_Isidima WHERE Isidima_Category_Id = @PrintValue";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Isidima.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Isidima", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Isidima_Info", SqlDataSource_Print2));
    }

    private void Form_AMSPI()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_AMSPI TableA WHERE EXISTS ( SELECT Facility_Id , AMSPI_Intervention_PatientVisitNumber FROM vForm_AMSPI TableB WHERE AMSPI_Intervention_Id = @PrintValue AND TableA.Facility_Id = TableB.Facility_Id AND TableA.AMSPI_Intervention_PatientVisitNumber = TableB.AMSPI_Intervention_PatientVisitNumber ) AND AMSPI_Intervention_IsActive = 1 ORDER BY AMSPI_Intervention_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_AMSPI.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_AMSPI", SqlDataSource_Print1));
    }

    private void Form_PROMS()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "EXECUTE spForm_Get_PROMS_Print @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT * FROM vForm_PROMS WHERE PROMS_Questionnaire_Id = @PrintValue AND PROMS_Questionnaire_IsActive = 1 ORDER BY PROMS_FollowUp_CompletionDate DESC";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_PROMS.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_PROMS", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_PROMS_FollowUp", SqlDataSource_Print2));
    }

    private void Form_QMSReview_Findings()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_QMSReview_Findings WHERE QMSReview_Findings_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_QMSReview_Findings.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_QMSReview_Findings", SqlDataSource_Print1));
    }

    private void Form_CollegeLearningAudit_Findings()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_CollegeLearningAudit_Findings WHERE CLA_Findings_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_CollegeLearningAudit_Findings.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_CollegeLearningAudit_Findings", SqlDataSource_Print1));
    }

    private void Form_OccupationalHealthAudit_Findings()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_OccupationalHealthAudit_Findings WHERE OHA_Findings_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_OccupationalHealthAudit_Findings.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_OccupationalHealthAudit_Findings", SqlDataSource_Print1));
    }

    private void Form_MonthlyHospitalStatistics()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_MonthlyHospitalStatistics WHERE MHS_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT * FROM vForm_MonthlyHospitalStatistics_Organisms WHERE MHS_Id = @PrintValue ORDER BY MHS_Organisms_Description";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print3.SelectCommand = "SELECT * FROM vForm_MonthlyHospitalStatistics_Waste WHERE MHS_Id = @PrintValue ORDER BY MHS_Waste_Identifier_Name";
        SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_MonthlyHospitalStatistics.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_MonthlyHospitalStatistics", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_MonthlyHospitalStatistics_Organisms", SqlDataSource_Print2));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_MonthlyHospitalStatistics_Waste", SqlDataSource_Print3));
    }

    private void Form_MonthlyPharmacyStatistics()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_MonthlyPharmacyStatistics WHERE MPS_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_MonthlyPharmacyStatistics.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_MonthlyPharmacyStatistics", SqlDataSource_Print1));
    }

    private void Form_Incident()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_Incident WHERE Incident_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Incident.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Incident", SqlDataSource_Print1));
    }

    private void Form_Alert()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_Alert WHERE Alert_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Alert.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Alert", SqlDataSource_Print1));
    }

    private void Form_Pharmacy_NewProduct()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_Pharmacy_NewProduct WHERE NewProduct_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT NewProduct_ProductClassification_List , NewProduct_ProductRequest_List , NewProduct_File_Id , vForm_Pharmacy_NewProduct_File.NewProduct_Id , NewProduct_File_Field_Name , NewProduct_File_Name , NewProduct_File_CreatedDate , NewProduct_File_CreatedBy FROM vForm_Pharmacy_NewProduct_File , vForm_Pharmacy_NewProduct WHERE vForm_Pharmacy_NewProduct_File.NewProduct_Id = vForm_Pharmacy_NewProduct.NewProduct_Id AND vForm_Pharmacy_NewProduct_File.NewProduct_Id = @PrintValue";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Pharmacy_NewProduct.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Pharmacy_NewProduct", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Pharmacy_NewProduct_File", SqlDataSource_Print2));
    }

    private void Form_Pharmacy_DeadStock()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_Pharmacy_DeadStock WHERE DeadStock_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Pharmacy_DeadStock.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Pharmacy_DeadStock", SqlDataSource_Print1));
    }

    private void Form_Pharmacy_Formulary()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_Pharmacy_Formulary WHERE Formulary_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Pharmacy_Formulary.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Pharmacy_Formulary", SqlDataSource_Print1));
    }

    private void Form_Pharmacy_Hotline()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_Pharmacy_Hotline WHERE Hotline_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Pharmacy_Hotline.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Pharmacy_Hotline", SqlDataSource_Print1));
    }

    private void Form_Pharmacy_Product()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_Pharmacy_Product WHERE Product_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Pharmacy_Product.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Pharmacy_Product", SqlDataSource_Print1));
    }

    private void Form_Pharmacy_ProductComplaint()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_Pharmacy_ProductComplaint WHERE ProductComplaint_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Pharmacy_ProductComplaint.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Pharmacy_ProductComplaint", SqlDataSource_Print1));
    }

    private void Form_Pharmacy_UPD()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_Pharmacy_UPD WHERE UPD_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Pharmacy_UPD.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Pharmacy_UPD", SqlDataSource_Print1));
    }

    private void Form_Pharmacy_Vendor()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_Pharmacy_Vendor WHERE Vendor_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_Pharmacy_Vendor.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_Pharmacy_Vendor", SqlDataSource_Print1));
    }

    private void Form_ECM()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_ECM WHERE ECM_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT * FROM vInfoQuest_Form_ECM_Diesel WHERE ECM_Id = @PrintValue AND ECM_Diesel_IsActive = 1";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print3.SelectCommand = "SELECT * FROM vInfoQuest_Form_ECM_Electricity WHERE ECM_Id = @PrintValue AND ECM_Electricity_IsActive = 1";
        SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print4;
      using (SqlDataSource_Print4 = new SqlDataSource())
      {
        SqlDataSource_Print4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print4.SelectCommand = "SELECT * FROM vInfoQuest_Form_ECM_Purchased WHERE ECM_Id = @PrintValue AND ECM_Purchased_IsActive = 1";
        SqlDataSource_Print4.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print5;
      using (SqlDataSource_Print5 = new SqlDataSource())
      {
        SqlDataSource_Print5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print5.SelectCommand = "SELECT * FROM vInfoQuest_Form_ECM_Water WHERE ECM_Id = @PrintValue AND ECM_Water_IsActive = 1";
        SqlDataSource_Print5.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }


      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_ECM.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_ECM", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_ECM_Diesel", SqlDataSource_Print2));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_ECM_Electricity", SqlDataSource_Print3));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_ECM_Purchased", SqlDataSource_Print4));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_ECM_Water", SqlDataSource_Print5));
    }

    private void Form_EquipmentClassifiedSystem_Request()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request WHERE ECS_Request_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT * FROM (SELECT TempTable1.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable1, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '1' AND vAdministration_SecurityAccess_Active.Form_Id = '-1' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable2.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable2, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '77' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable3.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable3, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '78' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable4.* FROM (SELECT * FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available WHERE Available_Facility_Id IN (SELECT Facility_Id FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_Username  = @SecurityUser_Username AND Form_Id = 24 AND SecurityRole_Id = 79)) AS TempTable4) AS TempTable4, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '79' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable5.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable5, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '80' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable6.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable6, Administration_Form WHERE Administration_Form.Form_Id = '24') AS TempTableAll WHERE ECS_Request_Id = @PrintValue";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
        SqlDataSource_Print2.SelectParameters.Add("SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print3.SelectCommand = "SELECT COUNT(*) AS Count_Request_Available FROM (SELECT TempTable1.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable1, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '1' AND vAdministration_SecurityAccess_Active.Form_Id = '-1' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable2.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable2, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '77' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable3.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable3, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '78' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable4.* FROM (SELECT * FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available WHERE Available_Facility_Id IN (SELECT Facility_Id FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_Username  = @SecurityUser_Username AND Form_Id = 24 AND SecurityRole_Id = 79)) AS TempTable4) AS TempTable4, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '79' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable5.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable5, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '80' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable6.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Request_Available) AS TempTable6, Administration_Form WHERE Administration_Form.Form_Id = '24') AS TempTableAll WHERE ECS_Request_Id = @PrintValue";
        SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
        SqlDataSource_Print3.SelectParameters.Add("SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_EquipmentClassifiedSystem_Request.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_EquipmentClassifiedSystem_Request", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_EquipmentClassifiedSystem_Request_Available", SqlDataSource_Print2));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Count_Request_Available", SqlDataSource_Print3));
    }

    private void Form_EquipmentClassifiedSystem_Available()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available WHERE ECS_Available_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT * FROM (SELECT TempTable1.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable1, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '1' AND vAdministration_SecurityAccess_Active.Form_Id = '-1' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable2.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable2, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '77' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable3.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable3, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '78' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable4.* FROM (SELECT * FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved WHERE Reserved_Facility_Id IN (SELECT Facility_Id FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_Username = @SecurityUser_Username AND Form_Id = 24 AND SecurityRole_Id = 79)) AS TempTable4) AS TempTable4, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '79' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable5.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable5, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '80' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable6.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable6, Administration_Form WHERE Administration_Form.Form_Id = '24') AS TempTableAll WHERE ECS_Available_Id = @PrintValue";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
        SqlDataSource_Print2.SelectParameters.Add("SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print3.SelectCommand = "SELECT COUNT(*) AS Count_Available_Reserved FROM (SELECT TempTable1.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable1, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '1' AND vAdministration_SecurityAccess_Active.Form_Id = '-1' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable2.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable2, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '77' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable3.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable3, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '78' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable4.* FROM (SELECT * FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved WHERE Reserved_Facility_Id IN (SELECT Facility_Id FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_Username = @SecurityUser_Username AND Form_Id = 24 AND SecurityRole_Id = 79)) AS TempTable4) AS TempTable4, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '79' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable5.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable5, vAdministration_SecurityAccess_Active WHERE vAdministration_SecurityAccess_Active.SecurityRole_Id = '80' AND vAdministration_SecurityAccess_Active.Form_Id = '24' AND vAdministration_SecurityAccess_Active.SecurityUser_Username = @SecurityUser_Username UNION SELECT TempTable6.* FROM (SELECT * FROM vInfoQuest_Form_EquipmentClassifiedSystem_Available_Reserved) AS TempTable6, Administration_Form WHERE Administration_Form.Form_Id = '24') AS TempTableAll WHERE ECS_Available_Id = @PrintValue";
        SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
        SqlDataSource_Print3.SelectParameters.Add("SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_EquipmentClassifiedSystem_Available.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_EquipmentClassifiedSystem_Available", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_EquipmentClassifiedSystem_Available_Reserved", SqlDataSource_Print2));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Count_Available_Reserved", SqlDataSource_Print3));
    }

    private void Form_BundleCompliance()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_BundleCompliance WHERE BC_Bundles_Id = @PrintValue ORDER BY vForm_BundleCompliance.BC_Bundles_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_BundleCompliance.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_BundleCompliance", SqlDataSource_Print1));
    }

    private void Form_BundleCompliance_All()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_BundleCompliance WHERE BC_Bundles_PatientVisitNumber IN (SELECT BC_Bundles_PatientVisitNumber FROM vForm_BundleCompliance WHERE BC_Bundles_Id = @PrintValue) AND Facility_Id IN (SELECT Facility_Id FROM vForm_BundleCompliance WHERE BC_Bundles_Id = @PrintValue) ORDER BY vForm_BundleCompliance.BC_Bundles_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_BundleCompliance_All.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_BundleCompliance_All", SqlDataSource_Print1));
    }

    private void Form_RehabBundleCompliance()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_RehabBundleCompliance WHERE RBC_Bundles_Id = @PrintValue ORDER BY vForm_RehabBundleCompliance.RBC_Bundles_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_RehabBundleCompliance.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_RehabBundleCompliance", SqlDataSource_Print1));
    }

    private void Form_RehabBundleCompliance_All()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_RehabBundleCompliance WHERE RBC_Bundles_PatientVisitNumber IN (SELECT RBC_Bundles_PatientVisitNumber FROM vForm_RehabBundleCompliance WHERE RBC_Bundles_Id = @PrintValue) AND Facility_Id IN (SELECT Facility_Id FROM vForm_RehabBundleCompliance WHERE RBC_Bundles_Id = @PrintValue) ORDER BY vForm_RehabBundleCompliance.RBC_Bundles_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_RehabBundleCompliance_All.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_RehabBundleCompliance_All", SqlDataSource_Print1));
    }

    private void Form_ClinicalPracticeAudit()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_ClinicalPracticeAudit WHERE CPA_Elements_Id = @PrintValue ORDER BY vInfoQuest_Form_ClinicalPracticeAudit.CPA_Elements_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_ClinicalPracticeAudit.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_ClinicalPracticeAudit", SqlDataSource_Print1));
    }

    private void Form_ClinicalPracticeAudit_All()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vInfoQuest_Form_ClinicalPracticeAudit WHERE CPA_Elements_PatientVisitNumber IN (SELECT CPA_Elements_PatientVisitNumber FROM vInfoQuest_Form_ClinicalPracticeAudit WHERE CPA_Elements_Id = @PrintValue) AND Facility_Id IN (SELECT Facility_Id FROM vInfoQuest_Form_ClinicalPracticeAudit WHERE CPA_Elements_Id = @PrintValue) ORDER BY vInfoQuest_Form_ClinicalPracticeAudit.CPA_Elements_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_ClinicalPracticeAudit_All.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_ClinicalPracticeAudit_All", SqlDataSource_Print1));
    }
    
    private void Form_HAI()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT DISTINCT vAdministration_Facility_All.Facility_FacilityDisplayName AS FacilityDisplayName , tblInfectionPrevention.sReportNumber FROM tblInfectionPrevention , vAdministration_Facility_All WHERE tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id AND (pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print2.SelectCommand = "SELECT DISTINCT iSiteNumber FROM tblInfectionPrevention_Site WHERE (fkiInfectionFormID = @PrintValue) AND ( iSiteNumber = @HAI_Site)";
          SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print2.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print2.SelectCommand = "SELECT DISTINCT iSiteNumber FROM tblInfectionPrevention_Site WHERE (fkiInfectionFormID = @PrintValue) AND ( iSiteNumber = @HAI_Site)";
          SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print2.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      ReportViewer_Print.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubReport_Form_HAI_Patient);
      ReportViewer_Print.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubReport_Form_HAI_Site);
      ReportViewer_Print.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubReport_Form_HAI_Investigation);

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_HAI.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_Header", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_Site", SqlDataSource_Print2));
    }    

    void SubReport_Form_HAI_Patient(object sender, SubreportProcessingEventArgs e)
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT DISTINCT vAdministration_Facility_All.Facility_FacilityDisplayName AS FacilityDisplayName , tblInfectionPrevention.sReportNumber FROM tblInfectionPrevention , vAdministration_Facility_All WHERE tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id AND (pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT * FROM (SELECT DISTINCT vAdministration_Facility_Form_All.Facility_FacilityDisplayName AS FacilityDisplayName, tblInfectionPrevention.sReportNumber, tblInfectionPrevention.sPatientVisitNumber, tblInfectionPrevention.sPatientName, tblInfectionPrevention.sPatientAge, CONVERT(DATETIME,tblInfectionPrevention.sPatientDateOfAdmission) AS sPatientDateOfAdmission, CASE WHEN tblInfectionPrevention_VisitDiagnosis.bSelected = 0 THEN '' ELSE tblInfectionPrevention_VisitDiagnosis.sCode + ' - ' + tblInfectionPrevention_VisitDiagnosis.sDescription END AS VisitDiagnosis FROM vAdministration_Facility_Form_All RIGHT OUTER JOIN tblInfectionPrevention LEFT OUTER JOIN tblInfectionPrevention_VisitDiagnosis ON tblInfectionPrevention.pkiInfectionFormID = tblInfectionPrevention_VisitDiagnosis.fkiInfectionFormID ON vAdministration_Facility_Form_All.Facility_Id = tblInfectionPrevention.fkiFacilityID WHERE (pkiInfectionFormID = @PrintValue)) AS Temp ORDER BY CASE WHEN LEFT(VisitDiagnosis,1) = '' THEN 'ZZZZZZZZZZ' + VisitDiagnosis ELSE VisitDiagnosis END";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      e.DataSources.Add(new ReportDataSource("Form_HAI_Header", SqlDataSource_Print1));
      e.DataSources.Add(new ReportDataSource("Form_HAI_Patient", SqlDataSource_Print2));
    }

    void SubReport_Form_HAI_Site(object sender, SubreportProcessingEventArgs e)
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT DISTINCT vAdministration_Facility_All.Facility_FacilityDisplayName AS FacilityDisplayName , tblInfectionPrevention.sReportNumber FROM tblInfectionPrevention , vAdministration_Facility_All WHERE tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id AND (pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print2.SelectCommand = "SELECT DISTINCT iSiteNumber FROM tblInfectionPrevention_Site WHERE (fkiInfectionFormID = @PrintValue) AND ( iSiteNumber = @HAI_Site)";
          SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print2.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print2.SelectCommand = "SELECT DISTINCT iSiteNumber FROM tblInfectionPrevention_Site WHERE (fkiInfectionFormID = @PrintValue) AND ( iSiteNumber = @HAI_Site)";
          SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print2.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print3.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site.fkiFacilityUnitID, Administration_Unit.Unit_Name, tblInfectionPrevention_Site.dtReported, tblInfectionPrevention_Site.fkiInfectionTypeID, vAdministration_ListItem_Active_1.ListItem_Name AS InfectionTypeName, tblInfectionPrevention_Site.fkiInfectionSubTypeID, vAdministration_ListItem_Active.ListItem_Name AS InfectionSubTypeName, tblInfectionPrevention_Site.fkiSeverityTypeID, tblSeverityType.sDescription, tblInfectionPrevention_Site.sDescription AS InfectionPrevention_SiteDescription FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblSeverityType ON tblInfectionPrevention_Site.fkiSeverityTypeID = tblSeverityType.pkiSeverityTypeID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active ON tblInfectionPrevention_Site.fkiInfectionSubTypeID = vAdministration_ListItem_Active.ListItem_Id LEFT OUTER JOIN Administration_Unit ON tblInfectionPrevention_Site.fkiFacilityUnitID = Administration_Unit.Unit_Id WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print3.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print3.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site.fkiFacilityUnitID, Administration_Unit.Unit_Name, tblInfectionPrevention_Site.dtReported, tblInfectionPrevention_Site.fkiInfectionTypeID, vAdministration_ListItem_Active_1.ListItem_Name AS InfectionTypeName, tblInfectionPrevention_Site.fkiInfectionSubTypeID, vAdministration_ListItem_Active.ListItem_Name AS InfectionSubTypeName, tblInfectionPrevention_Site.fkiSeverityTypeID, tblSeverityType.sDescription, tblInfectionPrevention_Site.sDescription AS InfectionPrevention_SiteDescription FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblSeverityType ON tblInfectionPrevention_Site.fkiSeverityTypeID = tblSeverityType.pkiSeverityTypeID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active ON tblInfectionPrevention_Site.fkiInfectionSubTypeID = vAdministration_ListItem_Active.ListItem_Id LEFT OUTER JOIN Administration_Unit ON tblInfectionPrevention_Site.fkiFacilityUnitID = Administration_Unit.Unit_Id WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print3.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print4;
      using (SqlDataSource_Print4 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print4.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, CASE WHEN tblInfectionPrevention_Site_Surgery.sFacility = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sFacility <> '' THEN tblInfectionPrevention_Site_Surgery.sFacility END AS sFacility, CASE WHEN tblInfectionPrevention_Site_Surgery.sVisitNumber = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sVisitNumber <> '' THEN tblInfectionPrevention_Site_Surgery.sVisitNumber END AS sVisitNumber, CASE WHEN tblInfectionPrevention_Site_Surgery.sProcedure = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sProcedure <> '' THEN tblInfectionPrevention_Site_Surgery.sProcedure END AS sProcedure, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDate <> '' THEN CAST(tblInfectionPrevention_Site_Surgery.sSurgeryDate AS DATETIME) END AS sSurgeryDate, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration <> '' THEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration END AS sSurgeryDuration, CASE WHEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice <> '' THEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice END AS sTheatreInvoice, CASE WHEN tblInfectionPrevention_Site_Surgery.sTheatre = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sTheatre <> '' THEN tblInfectionPrevention_Site_Surgery.sTheatre END AS sTheatre, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeon = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeon <> '' THEN tblInfectionPrevention_Site_Surgery.sSurgeon END AS sSurgeon, CASE WHEN tblInfectionPrevention_Site_Surgery.sAssistant = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAssistant <> '' THEN tblInfectionPrevention_Site_Surgery.sAssistant END AS sAssistant, CASE WHEN tblInfectionPrevention_Site_Surgery.sScrubNurse = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sScrubNurse <> '' THEN tblInfectionPrevention_Site_Surgery.sScrubNurse END AS sScrubNurse, CASE WHEN tblInfectionPrevention_Site_Surgery.sAnaesthesist = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAnaesthesist <> '' THEN tblInfectionPrevention_Site_Surgery.sAnaesthesist END AS sAnaesthesist, CASE WHEN tblInfectionPrevention_Site_Surgery.sWound = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sWound <> '' THEN tblInfectionPrevention_Site_Surgery.sWound END AS sWound, CASE WHEN tblInfectionPrevention_Site_Surgery.sCategory = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sCategory <> '' THEN tblInfectionPrevention_Site_Surgery.sCategory END AS sCategory, CASE WHEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis <> '' THEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis END AS sFinalDiagnosis, CASE WHEN tblInfectionPrevention_Site_Surgery.sAdmissionDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAdmissionDate <> '' THEN tblInfectionPrevention_Site_Surgery.sAdmissionDate END AS sAdmissionDate, CASE WHEN tblInfectionPrevention_Site_Surgery.sDischargeDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sDischargeDate <> '' THEN tblInfectionPrevention_Site_Surgery.sDischargeDate END AS sDischargeDate, tblInfectionPrevention_Site_Surgery.bSelected FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_Surgery ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_Surgery.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site_Surgery.bSelected = 1) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print4.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print4.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print4.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, CASE WHEN tblInfectionPrevention_Site_Surgery.sFacility = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sFacility <> '' THEN tblInfectionPrevention_Site_Surgery.sFacility END AS sFacility, CASE WHEN tblInfectionPrevention_Site_Surgery.sVisitNumber = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sVisitNumber <> '' THEN tblInfectionPrevention_Site_Surgery.sVisitNumber END AS sVisitNumber, CASE WHEN tblInfectionPrevention_Site_Surgery.sProcedure = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sProcedure <> '' THEN tblInfectionPrevention_Site_Surgery.sProcedure END AS sProcedure, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDate <> '' THEN CAST(tblInfectionPrevention_Site_Surgery.sSurgeryDate AS DATETIME) END AS sSurgeryDate, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration <> '' THEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration END AS sSurgeryDuration, CASE WHEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice <> '' THEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice END AS sTheatreInvoice, CASE WHEN tblInfectionPrevention_Site_Surgery.sTheatre = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sTheatre <> '' THEN tblInfectionPrevention_Site_Surgery.sTheatre END AS sTheatre, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeon = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeon <> '' THEN tblInfectionPrevention_Site_Surgery.sSurgeon END AS sSurgeon, CASE WHEN tblInfectionPrevention_Site_Surgery.sAssistant = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAssistant <> '' THEN tblInfectionPrevention_Site_Surgery.sAssistant END AS sAssistant, CASE WHEN tblInfectionPrevention_Site_Surgery.sScrubNurse = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sScrubNurse <> '' THEN tblInfectionPrevention_Site_Surgery.sScrubNurse END AS sScrubNurse, CASE WHEN tblInfectionPrevention_Site_Surgery.sAnaesthesist = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAnaesthesist <> '' THEN tblInfectionPrevention_Site_Surgery.sAnaesthesist END AS sAnaesthesist, CASE WHEN tblInfectionPrevention_Site_Surgery.sWound = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sWound <> '' THEN tblInfectionPrevention_Site_Surgery.sWound END AS sWound, CASE WHEN tblInfectionPrevention_Site_Surgery.sCategory = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sCategory <> '' THEN tblInfectionPrevention_Site_Surgery.sCategory END AS sCategory, CASE WHEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis <> '' THEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis END AS sFinalDiagnosis, CASE WHEN tblInfectionPrevention_Site_Surgery.sAdmissionDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAdmissionDate <> '' THEN tblInfectionPrevention_Site_Surgery.sAdmissionDate END AS sAdmissionDate, CASE WHEN tblInfectionPrevention_Site_Surgery.sDischargeDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sDischargeDate <> '' THEN tblInfectionPrevention_Site_Surgery.sDischargeDate END AS sDischargeDate, tblInfectionPrevention_Site_Surgery.bSelected FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_Surgery ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_Surgery.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site_Surgery.bSelected = 1) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print4.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print4.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print5;
      using (SqlDataSource_Print5 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print5.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID, vAdministration_ListItem_Active.ListItem_Name AS PredisposingConditonName, tblInfectionPrevention_Site_PredisposingCondition.sDescription AS PredisposingConditionsDescription, tblInfectionPrevention_Site_PredisposingCondition.bSelected FROM tblInfectionPrevention_Site LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_PredisposingCondition ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_PredisposingCondition.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_PredisposingCondition.bSelected = 1)";
          SqlDataSource_Print5.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print5.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print5.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID, vAdministration_ListItem_Active.ListItem_Name AS PredisposingConditonName, tblInfectionPrevention_Site_PredisposingCondition.sDescription AS PredisposingConditionsDescription, tblInfectionPrevention_Site_PredisposingCondition.bSelected FROM tblInfectionPrevention_Site LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_PredisposingCondition ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_PredisposingCondition.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_PredisposingCondition.bSelected = 1)";
          SqlDataSource_Print5.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print5.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print6;
      using (SqlDataSource_Print6 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print6.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print6.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_BedHistory.sFromUnit, tblInfectionPrevention_Site_BedHistory.sToUnit, CAST(tblInfectionPrevention_Site_BedHistory.sDateTransferred AS DATETIME) AS sDateTransferred FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_BedHistory ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_BedHistory.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND  (tblInfectionPrevention_Site_BedHistory.bSelected = 1)";
          SqlDataSource_Print6.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print6.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print6.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print6.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_BedHistory.sFromUnit, tblInfectionPrevention_Site_BedHistory.sToUnit, CAST(tblInfectionPrevention_Site_BedHistory.sDateTransferred AS DATETIME) AS sDateTransferred FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_BedHistory ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_BedHistory.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND  (tblInfectionPrevention_Site_BedHistory.bSelected = 1)";
          SqlDataSource_Print6.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print6.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print7;
      using (SqlDataSource_Print7 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print7.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print7.SelectCommand = "SELECT fkiInfectionFormID, iSiteNumber sRelatedHighRiskProcedures, CASE WHEN (CHARINDEX('TPN',sRelatedHighRiskProcedures,0)) > 0 THEN 'Yes' WHEN (CHARINDEX('TPN',sRelatedHighRiskProcedures,0)) = 0 THEN 'No' END AS TPN, CASE WHEN (CHARINDEX('Enteral Feeding',sRelatedHighRiskProcedures,0)) > 0 THEN'Yes' WHEN (CHARINDEX('Enteral Feeding',sRelatedHighRiskProcedures,0)) = 0 THEN 'No' END AS ENT FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblSeverityType ON tblInfectionPrevention_Site.fkiSeverityTypeID = tblSeverityType.pkiSeverityTypeID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active ON tblInfectionPrevention_Site.fkiInfectionSubTypeID = vAdministration_ListItem_Active.ListItem_Id LEFT OUTER JOIN Administration_Unit ON tblInfectionPrevention_Site.fkiFacilityUnitID = Administration_Unit.Unit_Id WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print7.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print7.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print7.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print7.SelectCommand = "SELECT fkiInfectionFormID, iSiteNumber sRelatedHighRiskProcedures, CASE WHEN (CHARINDEX('TPN',sRelatedHighRiskProcedures,0)) > 0 THEN 'Yes' WHEN (CHARINDEX('TPN',sRelatedHighRiskProcedures,0)) = 0 THEN 'No' END AS TPN, CASE WHEN (CHARINDEX('Enteral Feeding',sRelatedHighRiskProcedures,0)) > 0 THEN'Yes' WHEN (CHARINDEX('Enteral Feeding',sRelatedHighRiskProcedures,0)) = 0 THEN 'No' END AS ENT FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblSeverityType ON tblInfectionPrevention_Site.fkiSeverityTypeID = tblSeverityType.pkiSeverityTypeID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active ON tblInfectionPrevention_Site.fkiInfectionSubTypeID = vAdministration_ListItem_Active.ListItem_Id LEFT OUTER JOIN Administration_Unit ON tblInfectionPrevention_Site.fkiFacilityUnitID = Administration_Unit.Unit_Id WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print7.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print7.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print8;
      using (SqlDataSource_Print8 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print8.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print8.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_ReportableDisease.sReportedToDepartment, vAdministration_ListItem_Active.ListItem_Name AS DiseaseName, CAST(tblInfectionPrevention_Site_ReportableDisease.sDateReported AS DATETIME) AS sDateReported, tblInfectionPrevention_Site_ReportableDisease.sReferenceNumber FROM tblInfectionPrevention_Site LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_ReportableDisease ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_ReportableDisease.fkiDiseaseID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_ReportableDisease.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_ReportableDisease.bSelected = 1)";
          SqlDataSource_Print8.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print8.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print8.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print8.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_ReportableDisease.sReportedToDepartment, vAdministration_ListItem_Active.ListItem_Name AS DiseaseName, CAST(tblInfectionPrevention_Site_ReportableDisease.sDateReported AS DATETIME) AS sDateReported, tblInfectionPrevention_Site_ReportableDisease.sReferenceNumber FROM tblInfectionPrevention_Site LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_ReportableDisease ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_ReportableDisease.fkiDiseaseID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_ReportableDisease.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_ReportableDisease.bSelected = 1)";
          SqlDataSource_Print8.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print8.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print9;
      using (SqlDataSource_Print9 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print9.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print9.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, CAST(tblInfectionPrevention_Site_LabReport.sLabDate AS DATETIME) AS sLabDate, tblInfectionPrevention_Site_LabReport.sSpecimen, tblInfectionPrevention_Site_LabReport.sOrganism, tblInfectionPrevention_Site_LabReport.sLabNumber FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_LabReport ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_LabReport.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_LabReport.bSelected = 1)";
          SqlDataSource_Print9.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print9.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print9.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print9.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, CAST(tblInfectionPrevention_Site_LabReport.sLabDate AS DATETIME) AS sLabDate, tblInfectionPrevention_Site_LabReport.sSpecimen, tblInfectionPrevention_Site_LabReport.sOrganism, tblInfectionPrevention_Site_LabReport.sLabNumber FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_LabReport ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_LabReport.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_LabReport.bSelected = 1)";
          SqlDataSource_Print9.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print9.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print10;
      using (SqlDataSource_Print10 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print10.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print10.SelectCommand = "SELECT DISTINCT  sDescription FROM tblInfectionPrevention_Antibiotic WHERE fkiInfectionFormID = @PrintValue ORDER BY sDescription";
          SqlDataSource_Print10.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print10.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print10.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print10.SelectCommand = "SELECT DISTINCT  sDescription FROM tblInfectionPrevention_Antibiotic WHERE fkiInfectionFormID = @PrintValue ORDER BY sDescription";
          SqlDataSource_Print10.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print10.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print11;
      using (SqlDataSource_Print11 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print11.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print11.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site.sRelatedHighRiskProcedures, tblInfectionPrevention_Site.fkiInfectionTypeID, vAdministration_ListItem_Active_1.ListItem_Name AS InfectionTypeName, tblInfectionPrevention_Site.fkiBundleComplianceID, tblOveralBundleCompliance.sDescription, tblInfectionPrevention_Site.sInfectionDays, tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID, vAdministration_ListItem_Active.ListItem_Name AS BundleItemTypeName, tblInfectionPrevention_Site_BundleComplianceItem.bSelected FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblOveralBundleCompliance ON tblInfectionPrevention_Site.fkiBundleComplianceID = tblOveralBundleCompliance.pkiBundleComplianceID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_BundleComplianceItem ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_BundleComplianceItem.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print11.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print11.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print11.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print11.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site.sRelatedHighRiskProcedures, tblInfectionPrevention_Site.fkiInfectionTypeID, vAdministration_ListItem_Active_1.ListItem_Name AS InfectionTypeName, tblInfectionPrevention_Site.fkiBundleComplianceID, tblOveralBundleCompliance.sDescription, tblInfectionPrevention_Site.sInfectionDays, tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID, vAdministration_ListItem_Active.ListItem_Name AS BundleItemTypeName, tblInfectionPrevention_Site_BundleComplianceItem.bSelected FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblOveralBundleCompliance ON tblInfectionPrevention_Site.fkiBundleComplianceID = tblOveralBundleCompliance.pkiBundleComplianceID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_BundleComplianceItem ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_BundleComplianceItem.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print11.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print11.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      e.DataSources.Add(new ReportDataSource("Form_HAI_Header", SqlDataSource_Print1));
      e.DataSources.Add(new ReportDataSource("Form_HAI_Site", SqlDataSource_Print2));
      e.DataSources.Add(new ReportDataSource("Form_HAI_InfectionDetail", SqlDataSource_Print3));
      e.DataSources.Add(new ReportDataSource("Form_HAI_SurgeryDetails", SqlDataSource_Print4));
      e.DataSources.Add(new ReportDataSource("Form_HAI_PredisposingCondition", SqlDataSource_Print5));
      e.DataSources.Add(new ReportDataSource("Form_HAI_BedHistory", SqlDataSource_Print6));
      e.DataSources.Add(new ReportDataSource("Form_HAI_TPN_ENT", SqlDataSource_Print7));
      e.DataSources.Add(new ReportDataSource("Form_HAI_ReportableDiseases", SqlDataSource_Print8));
      e.DataSources.Add(new ReportDataSource("Form_HAI_LabReports", SqlDataSource_Print9));
      e.DataSources.Add(new ReportDataSource("Form_HAI_Antibiotic", SqlDataSource_Print10));
      e.DataSources.Add(new ReportDataSource("Form_HAI_BundleCompliance", SqlDataSource_Print11));
    }

    void SubReport_Form_HAI_Investigation(object sender, SubreportProcessingEventArgs e)
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT DISTINCT vAdministration_Facility_All.Facility_FacilityDisplayName AS FacilityDisplayName , tblInfectionPrevention.sReportNumber FROM tblInfectionPrevention , vAdministration_Facility_All WHERE tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id AND (pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT DISTINCT tblInfectionPrevention.dtDateOfInvestigation, tblInfectionPrevention.sInvestigatorName, tblInfectionPrevention.sInvestigatorDesignation, tblInfectionPrevention.sIPCSName, tblInfectionPrevention.sTeamMembers, vAdministration_ListItem_Active.ListItem_Name AS PrecautionaryMeasureName, tblInfectionPrevention_PrecautionaryMeasure.bSelected, tblInfectionPrevention.bInvestigationCompleted, tblInfectionPrevention.dtInvestigationCompleted FROM vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_PrecautionaryMeasure ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_PrecautionaryMeasure.fkiPrecautionaryMeasureID RIGHT OUTER JOIN tblInfectionPrevention ON tblInfectionPrevention_PrecautionaryMeasure.fkiInfectionFormID = tblInfectionPrevention.pkiInfectionFormID WHERE (tblInfectionPrevention.pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      e.DataSources.Add(new ReportDataSource("Form_HAI_Header", SqlDataSource_Print1));
      e.DataSources.Add(new ReportDataSource("Form_HAI_InvestigationSection", SqlDataSource_Print2));
    }

    private void Form_HAI_Patient()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT DISTINCT vAdministration_Facility_All.Facility_FacilityDisplayName AS FacilityDisplayName , tblInfectionPrevention.sReportNumber FROM tblInfectionPrevention , vAdministration_Facility_All WHERE tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id AND (pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT * FROM (SELECT DISTINCT vAdministration_Facility_Form_All.Facility_FacilityDisplayName AS FacilityDisplayName, tblInfectionPrevention.sReportNumber, tblInfectionPrevention.sPatientVisitNumber, tblInfectionPrevention.sPatientName, tblInfectionPrevention.sPatientAge, CONVERT(DATETIME,tblInfectionPrevention.sPatientDateOfAdmission) AS sPatientDateOfAdmission, CASE WHEN tblInfectionPrevention_VisitDiagnosis.bSelected = 0 THEN '' ELSE tblInfectionPrevention_VisitDiagnosis.sCode + ' - ' + tblInfectionPrevention_VisitDiagnosis.sDescription END AS VisitDiagnosis FROM vAdministration_Facility_Form_All RIGHT OUTER JOIN tblInfectionPrevention LEFT OUTER JOIN tblInfectionPrevention_VisitDiagnosis ON tblInfectionPrevention.pkiInfectionFormID = tblInfectionPrevention_VisitDiagnosis.fkiInfectionFormID ON vAdministration_Facility_Form_All.Facility_Id = tblInfectionPrevention.fkiFacilityID WHERE (pkiInfectionFormID = @PrintValue)) AS Temp ORDER BY CASE WHEN LEFT(VisitDiagnosis,1) = '' THEN 'ZZZZZZZZZZ' + VisitDiagnosis ELSE VisitDiagnosis END";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_HAI_Patient.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_Header", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_Patient", SqlDataSource_Print2));
    }

    private void Form_HAI_Patient_Extra()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT DISTINCT vAdministration_Facility_All.Facility_FacilityDisplayName AS FacilityDisplayName , tblInfectionPrevention.sReportNumber FROM tblInfectionPrevention , vAdministration_Facility_All WHERE tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id AND (pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_HAI_Patient_Extra.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_Header", SqlDataSource_Print1));
    }

    private void Form_HAI_Site()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT DISTINCT vAdministration_Facility_All.Facility_FacilityDisplayName AS FacilityDisplayName , tblInfectionPrevention.sReportNumber FROM tblInfectionPrevention , vAdministration_Facility_All WHERE tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id AND (pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print2.SelectCommand = "SELECT DISTINCT iSiteNumber FROM tblInfectionPrevention_Site WHERE (fkiInfectionFormID = @PrintValue) AND ( iSiteNumber = @HAI_Site)";
          SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print2.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print2.SelectCommand = "SELECT DISTINCT iSiteNumber FROM tblInfectionPrevention_Site WHERE (fkiInfectionFormID = @PrintValue) AND ( iSiteNumber = @HAI_Site)";
          SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print2.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print3.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site.fkiFacilityUnitID, Administration_Unit.Unit_Name, tblInfectionPrevention_Site.dtReported, tblInfectionPrevention_Site.fkiInfectionTypeID, vAdministration_ListItem_Active_1.ListItem_Name AS InfectionTypeName, tblInfectionPrevention_Site.fkiInfectionSubTypeID, vAdministration_ListItem_Active.ListItem_Name AS InfectionSubTypeName, tblInfectionPrevention_Site.fkiSeverityTypeID, tblSeverityType.sDescription, tblInfectionPrevention_Site.sDescription AS InfectionPrevention_SiteDescription FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblSeverityType ON tblInfectionPrevention_Site.fkiSeverityTypeID = tblSeverityType.pkiSeverityTypeID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active ON tblInfectionPrevention_Site.fkiInfectionSubTypeID = vAdministration_ListItem_Active.ListItem_Id LEFT OUTER JOIN Administration_Unit ON tblInfectionPrevention_Site.fkiFacilityUnitID = Administration_Unit.Unit_Id WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print3.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print3.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site.fkiFacilityUnitID, Administration_Unit.Unit_Name, tblInfectionPrevention_Site.dtReported, tblInfectionPrevention_Site.fkiInfectionTypeID, vAdministration_ListItem_Active_1.ListItem_Name AS InfectionTypeName, tblInfectionPrevention_Site.fkiInfectionSubTypeID, vAdministration_ListItem_Active.ListItem_Name AS InfectionSubTypeName, tblInfectionPrevention_Site.fkiSeverityTypeID, tblSeverityType.sDescription, tblInfectionPrevention_Site.sDescription AS InfectionPrevention_SiteDescription FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblSeverityType ON tblInfectionPrevention_Site.fkiSeverityTypeID = tblSeverityType.pkiSeverityTypeID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active ON tblInfectionPrevention_Site.fkiInfectionSubTypeID = vAdministration_ListItem_Active.ListItem_Id LEFT OUTER JOIN Administration_Unit ON tblInfectionPrevention_Site.fkiFacilityUnitID = Administration_Unit.Unit_Id WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print3.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print4;
      using (SqlDataSource_Print4 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print4.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, CASE WHEN tblInfectionPrevention_Site_Surgery.sFacility = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sFacility <> '' THEN tblInfectionPrevention_Site_Surgery.sFacility END AS sFacility, CASE WHEN tblInfectionPrevention_Site_Surgery.sVisitNumber = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sVisitNumber <> '' THEN tblInfectionPrevention_Site_Surgery.sVisitNumber END AS sVisitNumber, CASE WHEN tblInfectionPrevention_Site_Surgery.sProcedure = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sProcedure <> '' THEN tblInfectionPrevention_Site_Surgery.sProcedure END AS sProcedure, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDate <> '' THEN CAST(tblInfectionPrevention_Site_Surgery.sSurgeryDate AS DATETIME) END AS sSurgeryDate, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration <> '' THEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration END AS sSurgeryDuration, CASE WHEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice <> '' THEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice END AS sTheatreInvoice, CASE WHEN tblInfectionPrevention_Site_Surgery.sTheatre = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sTheatre <> '' THEN tblInfectionPrevention_Site_Surgery.sTheatre END AS sTheatre, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeon = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeon <> '' THEN tblInfectionPrevention_Site_Surgery.sSurgeon END AS sSurgeon, CASE WHEN tblInfectionPrevention_Site_Surgery.sAssistant = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAssistant <> '' THEN tblInfectionPrevention_Site_Surgery.sAssistant END AS sAssistant, CASE WHEN tblInfectionPrevention_Site_Surgery.sScrubNurse = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sScrubNurse <> '' THEN tblInfectionPrevention_Site_Surgery.sScrubNurse END AS sScrubNurse, CASE WHEN tblInfectionPrevention_Site_Surgery.sAnaesthesist = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAnaesthesist <> '' THEN tblInfectionPrevention_Site_Surgery.sAnaesthesist END AS sAnaesthesist, CASE WHEN tblInfectionPrevention_Site_Surgery.sWound = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sWound <> '' THEN tblInfectionPrevention_Site_Surgery.sWound END AS sWound, CASE WHEN tblInfectionPrevention_Site_Surgery.sCategory = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sCategory <> '' THEN tblInfectionPrevention_Site_Surgery.sCategory END AS sCategory, CASE WHEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis <> '' THEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis END AS sFinalDiagnosis, CASE WHEN tblInfectionPrevention_Site_Surgery.sAdmissionDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAdmissionDate <> '' THEN tblInfectionPrevention_Site_Surgery.sAdmissionDate END AS sAdmissionDate, CASE WHEN tblInfectionPrevention_Site_Surgery.sDischargeDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sDischargeDate <> '' THEN tblInfectionPrevention_Site_Surgery.sDischargeDate END AS sDischargeDate, tblInfectionPrevention_Site_Surgery.bSelected FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_Surgery ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_Surgery.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site_Surgery.bSelected = 1) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print4.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print4.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print4.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, CASE WHEN tblInfectionPrevention_Site_Surgery.sFacility = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sFacility <> '' THEN tblInfectionPrevention_Site_Surgery.sFacility END AS sFacility, CASE WHEN tblInfectionPrevention_Site_Surgery.sVisitNumber = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sVisitNumber <> '' THEN tblInfectionPrevention_Site_Surgery.sVisitNumber END AS sVisitNumber, CASE WHEN tblInfectionPrevention_Site_Surgery.sProcedure = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sProcedure <> '' THEN tblInfectionPrevention_Site_Surgery.sProcedure END AS sProcedure, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDate <> '' THEN CAST(tblInfectionPrevention_Site_Surgery.sSurgeryDate AS DATETIME) END AS sSurgeryDate, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration <> '' THEN tblInfectionPrevention_Site_Surgery.sSurgeryDuration END AS sSurgeryDuration, CASE WHEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice <> '' THEN tblInfectionPrevention_Site_Surgery.sTheatreInvoice END AS sTheatreInvoice, CASE WHEN tblInfectionPrevention_Site_Surgery.sTheatre = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sTheatre <> '' THEN tblInfectionPrevention_Site_Surgery.sTheatre END AS sTheatre, CASE WHEN tblInfectionPrevention_Site_Surgery.sSurgeon = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sSurgeon <> '' THEN tblInfectionPrevention_Site_Surgery.sSurgeon END AS sSurgeon, CASE WHEN tblInfectionPrevention_Site_Surgery.sAssistant = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAssistant <> '' THEN tblInfectionPrevention_Site_Surgery.sAssistant END AS sAssistant, CASE WHEN tblInfectionPrevention_Site_Surgery.sScrubNurse = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sScrubNurse <> '' THEN tblInfectionPrevention_Site_Surgery.sScrubNurse END AS sScrubNurse, CASE WHEN tblInfectionPrevention_Site_Surgery.sAnaesthesist = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAnaesthesist <> '' THEN tblInfectionPrevention_Site_Surgery.sAnaesthesist END AS sAnaesthesist, CASE WHEN tblInfectionPrevention_Site_Surgery.sWound = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sWound <> '' THEN tblInfectionPrevention_Site_Surgery.sWound END AS sWound, CASE WHEN tblInfectionPrevention_Site_Surgery.sCategory = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sCategory <> '' THEN tblInfectionPrevention_Site_Surgery.sCategory END AS sCategory, CASE WHEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis <> '' THEN tblInfectionPrevention_Site_Surgery.sFinalDiagnosis END AS sFinalDiagnosis, CASE WHEN tblInfectionPrevention_Site_Surgery.sAdmissionDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sAdmissionDate <> '' THEN tblInfectionPrevention_Site_Surgery.sAdmissionDate END AS sAdmissionDate, CASE WHEN tblInfectionPrevention_Site_Surgery.sDischargeDate = '' THEN 'No Information' WHEN tblInfectionPrevention_Site_Surgery.sDischargeDate <> '' THEN tblInfectionPrevention_Site_Surgery.sDischargeDate END AS sDischargeDate, tblInfectionPrevention_Site_Surgery.bSelected FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_Surgery ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_Surgery.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site_Surgery.bSelected = 1) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print4.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print4.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print5;
      using (SqlDataSource_Print5 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print5.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID, vAdministration_ListItem_Active.ListItem_Name AS PredisposingConditonName, tblInfectionPrevention_Site_PredisposingCondition.sDescription AS PredisposingConditionsDescription, tblInfectionPrevention_Site_PredisposingCondition.bSelected FROM tblInfectionPrevention_Site LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_PredisposingCondition ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_PredisposingCondition.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_PredisposingCondition.bSelected = 1)";
          SqlDataSource_Print5.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print5.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print5.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID, vAdministration_ListItem_Active.ListItem_Name AS PredisposingConditonName, tblInfectionPrevention_Site_PredisposingCondition.sDescription AS PredisposingConditionsDescription, tblInfectionPrevention_Site_PredisposingCondition.bSelected FROM tblInfectionPrevention_Site LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_PredisposingCondition ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_PredisposingCondition.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_PredisposingCondition.bSelected = 1)";
          SqlDataSource_Print5.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print5.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print6;
      using (SqlDataSource_Print6 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print6.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print6.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_BedHistory.sFromUnit, tblInfectionPrevention_Site_BedHistory.sToUnit, CAST(tblInfectionPrevention_Site_BedHistory.sDateTransferred AS DATETIME) AS sDateTransferred FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_BedHistory ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_BedHistory.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND  (tblInfectionPrevention_Site_BedHistory.bSelected = 1)";
          SqlDataSource_Print6.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print6.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print6.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print6.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_BedHistory.sFromUnit, tblInfectionPrevention_Site_BedHistory.sToUnit, CAST(tblInfectionPrevention_Site_BedHistory.sDateTransferred AS DATETIME) AS sDateTransferred FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_BedHistory ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_BedHistory.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND  (tblInfectionPrevention_Site_BedHistory.bSelected = 1)";
          SqlDataSource_Print6.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print6.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print7;
      using (SqlDataSource_Print7 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print7.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print7.SelectCommand = "SELECT fkiInfectionFormID, iSiteNumber sRelatedHighRiskProcedures, CASE WHEN (CHARINDEX('TPN',sRelatedHighRiskProcedures,0)) > 0 THEN 'Yes' WHEN (CHARINDEX('TPN',sRelatedHighRiskProcedures,0)) = 0 THEN 'No' END AS TPN, CASE WHEN (CHARINDEX('Enteral Feeding',sRelatedHighRiskProcedures,0)) > 0 THEN'Yes' WHEN (CHARINDEX('Enteral Feeding',sRelatedHighRiskProcedures,0)) = 0 THEN 'No' END AS ENT FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblSeverityType ON tblInfectionPrevention_Site.fkiSeverityTypeID = tblSeverityType.pkiSeverityTypeID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active ON tblInfectionPrevention_Site.fkiInfectionSubTypeID = vAdministration_ListItem_Active.ListItem_Id LEFT OUTER JOIN Administration_Unit ON tblInfectionPrevention_Site.fkiFacilityUnitID = Administration_Unit.Unit_Id WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print7.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print7.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print7.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print7.SelectCommand = "SELECT fkiInfectionFormID, iSiteNumber sRelatedHighRiskProcedures, CASE WHEN (CHARINDEX('TPN',sRelatedHighRiskProcedures,0)) > 0 THEN 'Yes' WHEN (CHARINDEX('TPN',sRelatedHighRiskProcedures,0)) = 0 THEN 'No' END AS TPN, CASE WHEN (CHARINDEX('Enteral Feeding',sRelatedHighRiskProcedures,0)) > 0 THEN'Yes' WHEN (CHARINDEX('Enteral Feeding',sRelatedHighRiskProcedures,0)) = 0 THEN 'No' END AS ENT FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblSeverityType ON tblInfectionPrevention_Site.fkiSeverityTypeID = tblSeverityType.pkiSeverityTypeID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active ON tblInfectionPrevention_Site.fkiInfectionSubTypeID = vAdministration_ListItem_Active.ListItem_Id LEFT OUTER JOIN Administration_Unit ON tblInfectionPrevention_Site.fkiFacilityUnitID = Administration_Unit.Unit_Id WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print7.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print7.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print8;
      using (SqlDataSource_Print8 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print8.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print8.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_ReportableDisease.sReportedToDepartment, vAdministration_ListItem_Active.ListItem_Name AS DiseaseName, CAST(tblInfectionPrevention_Site_ReportableDisease.sDateReported AS DATETIME) AS sDateReported, tblInfectionPrevention_Site_ReportableDisease.sReferenceNumber FROM tblInfectionPrevention_Site LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_ReportableDisease ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_ReportableDisease.fkiDiseaseID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_ReportableDisease.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_ReportableDisease.bSelected = 1)";
          SqlDataSource_Print8.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print8.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print8.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print8.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site_ReportableDisease.sReportedToDepartment, vAdministration_ListItem_Active.ListItem_Name AS DiseaseName, CAST(tblInfectionPrevention_Site_ReportableDisease.sDateReported AS DATETIME) AS sDateReported, tblInfectionPrevention_Site_ReportableDisease.sReferenceNumber FROM tblInfectionPrevention_Site LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_ReportableDisease ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_ReportableDisease.fkiDiseaseID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_ReportableDisease.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_ReportableDisease.bSelected = 1)";
          SqlDataSource_Print8.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print8.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print9;
      using (SqlDataSource_Print9 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print9.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print9.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, CAST(tblInfectionPrevention_Site_LabReport.sLabDate AS DATETIME) AS sLabDate, tblInfectionPrevention_Site_LabReport.sSpecimen, tblInfectionPrevention_Site_LabReport.sOrganism, tblInfectionPrevention_Site_LabReport.sLabNumber FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_LabReport ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_LabReport.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_LabReport.bSelected = 1)";
          SqlDataSource_Print9.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print9.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print9.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print9.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, CAST(tblInfectionPrevention_Site_LabReport.sLabDate AS DATETIME) AS sLabDate, tblInfectionPrevention_Site_LabReport.sSpecimen, tblInfectionPrevention_Site_LabReport.sOrganism, tblInfectionPrevention_Site_LabReport.sLabNumber FROM tblInfectionPrevention_Site LEFT OUTER JOIN tblInfectionPrevention_Site_LabReport ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_LabReport.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site) AND (tblInfectionPrevention_Site_LabReport.bSelected = 1)";
          SqlDataSource_Print9.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print9.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print10;
      using (SqlDataSource_Print10 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print10.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print10.SelectCommand = "SELECT DISTINCT  sDescription FROM tblInfectionPrevention_Antibiotic WHERE fkiInfectionFormID = @PrintValue ORDER BY sDescription";
          SqlDataSource_Print10.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print10.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print10.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print10.SelectCommand = "SELECT DISTINCT  sDescription FROM tblInfectionPrevention_Antibiotic WHERE fkiInfectionFormID = @PrintValue ORDER BY sDescription";
          SqlDataSource_Print10.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print10.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      SqlDataSource SqlDataSource_Print11;
      using (SqlDataSource_Print11 = new SqlDataSource())
      {
        if (Request.QueryString["HAI_Site"] == null)
        {
          SqlDataSource_Print11.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print11.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site.sRelatedHighRiskProcedures, tblInfectionPrevention_Site.fkiInfectionTypeID, vAdministration_ListItem_Active_1.ListItem_Name AS InfectionTypeName, tblInfectionPrevention_Site.fkiBundleComplianceID, tblOveralBundleCompliance.sDescription, tblInfectionPrevention_Site.sInfectionDays, tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID, vAdministration_ListItem_Active.ListItem_Name AS BundleItemTypeName, tblInfectionPrevention_Site_BundleComplianceItem.bSelected FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblOveralBundleCompliance ON tblInfectionPrevention_Site.fkiBundleComplianceID = tblOveralBundleCompliance.pkiBundleComplianceID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_BundleComplianceItem ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_BundleComplianceItem.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print11.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print11.SelectParameters.Add("HAI_Site", "1");
        }
        else
        {
          SqlDataSource_Print11.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          SqlDataSource_Print11.SelectCommand = "SELECT DISTINCT tblInfectionPrevention_Site.fkiInfectionFormID, tblInfectionPrevention_Site.iSiteNumber, tblInfectionPrevention_Site.sRelatedHighRiskProcedures, tblInfectionPrevention_Site.fkiInfectionTypeID, vAdministration_ListItem_Active_1.ListItem_Name AS InfectionTypeName, tblInfectionPrevention_Site.fkiBundleComplianceID, tblOveralBundleCompliance.sDescription, tblInfectionPrevention_Site.sInfectionDays, tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID, vAdministration_ListItem_Active.ListItem_Name AS BundleItemTypeName, tblInfectionPrevention_Site_BundleComplianceItem.bSelected FROM vAdministration_ListItem_Active AS vAdministration_ListItem_Active_1 RIGHT OUTER JOIN tblInfectionPrevention_Site ON vAdministration_ListItem_Active_1.ListItem_Id = tblInfectionPrevention_Site.fkiInfectionTypeID LEFT OUTER JOIN tblOveralBundleCompliance ON tblInfectionPrevention_Site.fkiBundleComplianceID = tblOveralBundleCompliance.pkiBundleComplianceID LEFT OUTER JOIN vAdministration_ListItem_Active AS vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_Site_BundleComplianceItem ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID ON tblInfectionPrevention_Site.pkiSiteID = tblInfectionPrevention_Site_BundleComplianceItem.fkiSiteID WHERE (tblInfectionPrevention_Site.fkiInfectionFormID = @PrintValue) AND (tblInfectionPrevention_Site.iSiteNumber = @HAI_Site)";
          SqlDataSource_Print11.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
          SqlDataSource_Print11.SelectParameters.Add("HAI_Site", Request.QueryString["HAI_Site"]);
        }
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_HAI_Site.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_Header", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_Site", SqlDataSource_Print2));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_InfectionDetail", SqlDataSource_Print3));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_SurgeryDetails", SqlDataSource_Print4));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_PredisposingCondition", SqlDataSource_Print5));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_BedHistory", SqlDataSource_Print6));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_TPN_ENT", SqlDataSource_Print7));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_ReportableDiseases", SqlDataSource_Print8));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_LabReports", SqlDataSource_Print9));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_Antibiotic", SqlDataSource_Print10));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_BundleCompliance", SqlDataSource_Print11));
    }

    private void Form_HAI_Investigation()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT DISTINCT vAdministration_Facility_All.Facility_FacilityDisplayName AS FacilityDisplayName , tblInfectionPrevention.sReportNumber FROM tblInfectionPrevention , vAdministration_Facility_All WHERE tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id AND (pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT DISTINCT tblInfectionPrevention.dtDateOfInvestigation, tblInfectionPrevention.sInvestigatorName, tblInfectionPrevention.sInvestigatorDesignation, tblInfectionPrevention.sIPCSName, tblInfectionPrevention.sTeamMembers, vAdministration_ListItem_Active.ListItem_Name AS PrecautionaryMeasureName, tblInfectionPrevention_PrecautionaryMeasure.bSelected, tblInfectionPrevention.bInvestigationCompleted, tblInfectionPrevention.dtInvestigationCompleted FROM vAdministration_ListItem_Active RIGHT OUTER JOIN tblInfectionPrevention_PrecautionaryMeasure ON vAdministration_ListItem_Active.ListItem_Id = tblInfectionPrevention_PrecautionaryMeasure.fkiPrecautionaryMeasureID RIGHT OUTER JOIN tblInfectionPrevention ON tblInfectionPrevention_PrecautionaryMeasure.fkiInfectionFormID = tblInfectionPrevention.pkiInfectionFormID WHERE (tblInfectionPrevention.pkiInfectionFormID = @PrintValue)";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_HAI_Investigation.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_Header", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HAI_InvestigationSection", SqlDataSource_Print2));
    }
    
    private void Form_MHQ14()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT MHQ14_Questionnaire_Id ,Facility_Id ,Facility_FacilityName ,Facility_FacilityCode ,Facility_FacilityDisplayName ,MHQ14_PI_PatientVisitNumber ,MHQ14_PI_PatientName ,MHQ14_PI_PatientAge ,MHQ14_PI_PatientDateOfAdmission ,MHQ14_PI_PatientDateOfDischarge ,MHQ14_PI_Archived ,MHQ14_Questionnaire_PatientVisitNumber ,MHQ14_Questionnaire_ReportNumber ,MHQ14_Questionnaire_ADMS_Date ,MHQ14_Questionnaire_ADMS_Diagnosis_Q1 ,MHQ14_Questionnaire_ADMS_Diagnosis_Q2 ,MHQ14_Questionnaire_ADMS_Diagnosis_Q3 ,MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List ,MHQ14_Questionnaire_ADMS_Diagnosis_Q4_Name ,CASE MHQ14_Questionnaire_ADMS_Q1A WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1A ,CASE MHQ14_Questionnaire_ADMS_Q1B WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1B ,CASE MHQ14_Questionnaire_ADMS_Q1C WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1C ,CASE MHQ14_Questionnaire_ADMS_Q1D WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1D ,CASE MHQ14_Questionnaire_ADMS_Q1E WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1E ,MHQ14_Questionnaire_ADMS_Section1Score ,CASE MHQ14_Questionnaire_ADMS_Q2A WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q2A ,CASE MHQ14_Questionnaire_ADMS_Q2B WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q2B ,CASE MHQ14_Questionnaire_ADMS_Q2C WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q2C ,CASE MHQ14_Questionnaire_ADMS_Q2D WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q2D ,MHQ14_Questionnaire_ADMS_Section2Score ,CASE MHQ14_Questionnaire_ADMS_Q3A WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q3A ,CASE MHQ14_Questionnaire_ADMS_Q3B WHEN '100' THEN 'Not at all' WHEN '75' THEN 'Slightly' WHEN '50' THEN 'Moderately' WHEN '25' THEN 'Quite a bit' WHEN '0' THEN 'Extremely' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q3B ,MHQ14_Questionnaire_ADMS_Section3Score ,CASE MHQ14_Questionnaire_ADMS_Q4A WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q4A ,CASE MHQ14_Questionnaire_ADMS_Q4B WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q4B ,CASE MHQ14_Questionnaire_ADMS_Q4C WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q4C ,MHQ14_Questionnaire_ADMS_Section4Score ,MHQ14_Questionnaire_ADMS_Score ,MHQ14_Questionnaire_DISCH_CompleteDischarge ,MHQ14_Questionnaire_DISCH_NoDischargeReason_List ,MHQ14_Questionnaire_DISCH_NoDischargeReason_Name ,MHQ14_Questionnaire_DISCH_Date ,MHQ14_Questionnaire_DISCH_Diagnosis_Q1 ,MHQ14_Questionnaire_DISCH_Diagnosis_Q2 ,MHQ14_Questionnaire_DISCH_Diagnosis_Q3 ,MHQ14_Questionnaire_DISCH_Diagnosis_Q4_List ,MHQ14_Questionnaire_DISCH_Diagnosis_Q4_Name ,CASE MHQ14_Questionnaire_DISCH_Q1A WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1A ,CASE MHQ14_Questionnaire_DISCH_Q1B WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1B ,CASE MHQ14_Questionnaire_DISCH_Q1C WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1C ,CASE MHQ14_Questionnaire_DISCH_Q1D WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1D ,CASE MHQ14_Questionnaire_DISCH_Q1E WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1E ,MHQ14_Questionnaire_DISCH_Section1Score ,CASE MHQ14_Questionnaire_DISCH_Q2A WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q2A ,CASE MHQ14_Questionnaire_DISCH_Q2B WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q2B ,CASE MHQ14_Questionnaire_DISCH_Q2C WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q2C ,CASE MHQ14_Questionnaire_DISCH_Q2D WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q2D ,MHQ14_Questionnaire_DISCH_Section2Score ,CASE MHQ14_Questionnaire_DISCH_Q3A WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q3A ,CASE MHQ14_Questionnaire_DISCH_Q3B WHEN '100' THEN 'Not at all' WHEN '75' THEN 'Slightly' WHEN '50' THEN 'Moderately' WHEN '25' THEN 'Quite a bit' WHEN '0' THEN 'Extremely' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q3B ,MHQ14_Questionnaire_DISCH_Section3Score ,CASE MHQ14_Questionnaire_DISCH_Q4A WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q4A ,CASE MHQ14_Questionnaire_DISCH_Q4B WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q4B ,CASE MHQ14_Questionnaire_DISCH_Q4C WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q4C ,MHQ14_Questionnaire_DISCH_Section4Score ,MHQ14_Questionnaire_DISCH_Score ,MHQ14_Questionnaire_DISCH_Difference ,MHQ14_Questionnaire_CreatedDate ,MHQ14_Questionnaire_CreatedBy ,MHQ14_Questionnaire_ModifiedDate ,MHQ14_Questionnaire_ModifiedBy ,MHQ14_Questionnaire_History ,MHQ14_Questionnaire_IsActive FROM vForm_MHQ14 WHERE MHQ14_Questionnaire_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_MHQ14.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_MHQ14", SqlDataSource_Print1));
    }

    private void Form_CRM()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_CRM LEFT JOIN vForm_CRM_Complaint_Category ON vForm_CRM.CRM_Id = vForm_CRM_Complaint_Category.CRM_Id WHERE vForm_CRM.CRM_Id = @PrintValue ORDER BY CRM_Complaint_Category_Item_Name";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT * FROM Form_CRM_File WHERE CRM_Id = @PrintValue ORDER BY CRM_File_Name";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print3.SelectCommand = "SELECT * FROM Form_CRM_PXM_PDCH_Result WHERE CRM_Id = @PrintValue";
        SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_CRM.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_CRM", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_CRM_File", SqlDataSource_Print2));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_CRM_PXM_PDCH_Result", SqlDataSource_Print3));
    }

    private void Form_IPS()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT Facility_FacilityDisplayName , IPS_Infection_ReportNumber FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT Form_IPS_HAI.IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubReport_Form_IPS_InfectionDetail);
      ReportViewer_Print.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubReport_Form_IPS_Investigation);

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_IPS.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_Infection_Header", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_HAI_Investigation", SqlDataSource_Print2));
    }

    void SubReport_Form_IPS_InfectionDetail(object sender, SubreportProcessingEventArgs e)
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT Facility_FacilityDisplayName , IPS_Infection_ReportNumber FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT Form_IPS_HAI.IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print3.SelectCommand = "SELECT IPS_VisitInformation_VisitNumber , PatientInformation_Name , PatientInformation_Surname , PatientInformation_Gender , IPS_VisitInformation_PatientAge , PatientInformation_DateOfBirth , IPS_VisitInformation_DateOfAdmission , IPS_VisitInformation_DateOfDischarge , IPS_VisitInformation_Deceased , IPS_VisitInformation_Ward FROM vForm_IPS_VisitInformation WHERE IPS_VisitInformation_Id IN ( SELECT IPS_VisitInformation_Id FROM Form_IPS_Infection WHERE IPS_Infection_Id = @PrintValue )";
        SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print4;
      using (SqlDataSource_Print4 = new SqlDataSource())
      {
        SqlDataSource_Print4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print4.SelectCommand = "SELECT IPS_Infection_Category_List , IPS_Infection_Category_Name , IPS_Infection_Type_List , IPS_Infection_Type_Name , IPS_Infection_SubType_Name , IPS_Infection_SubSubType_Name , CASE WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4996 THEN vForm_IPS_Infection.IPS_Infection_Site_Name WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4996') THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4997') THEN 'Linked Site Required' ELSE vForm_IPS_Infection.IPS_Infection_Site_Name + ' (' + vForm_IPS_Infection.IPS_Infection_Site_Infection_ReportNumber + ')' END	AS IPS_Infection_Site_Name , CASE WHEN IPS_Infection_Screening = 1 THEN 'Yes' WHEN IPS_Infection_Screening = 0 THEN 'No' END AS IPS_Infection_Screening , IPS_ScreeningType_Type_Name , IPS_Infection_ScreeningReason_Name , Unit_Name , IPS_Infection_Summary FROM vForm_IPS_Infection LEFT JOIN vForm_IPS_Infection_ScreeningType ON vForm_IPS_Infection.IPS_Infection_Id = vForm_IPS_Infection_ScreeningType.IPS_Infection_Id WHERE vForm_IPS_Infection.IPS_Infection_Id = @PrintValue ORDER BY IPS_ScreeningType_Type_Name";
        SqlDataSource_Print4.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print5;
      using (SqlDataSource_Print5 = new SqlDataSource())
      {
        SqlDataSource_Print5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print5.SelectCommand = "SELECT IPS_Theatre_FacilityDisplayName , IPS_Theatre_DateOfAdmission , IPS_Theatre_DateOfDischarge , IPS_Theatre_FinalDiagnosisCode , IPS_Theatre_FinalDiagnosisDescription , IPS_Theatre_VisitNumber , IPS_Theatre_ServiceCategory , IPS_Theatre_Theatre , IPS_Theatre_TheatreTime , IPS_Theatre_ProcedureDate , IPS_Theatre_ProcedureCode , IPS_Theatre_ProcedureDescription , IPS_Theatre_TheatreInvoice , IPS_Theatre_Surgeon , IPS_Theatre_Anaesthetist , IPS_Theatre_Assistant , IPS_Theatre_WoundCategory , IPS_Theatre_ScrubNurse FROM vForm_IPS_Theatre WHERE IPS_Infection_Id = @PrintValue ORDER BY IPS_Theatre_DateOfAdmission DESC , IPS_Theatre_ProcedureDate DESC";
        SqlDataSource_Print5.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print6;
      using (SqlDataSource_Print6 = new SqlDataSource())
      {
        SqlDataSource_Print6.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print6.SelectCommand = "SELECT IPS_VisitDiagnosis_CodeType , IPS_VisitDiagnosis_Code , IPS_VisitDiagnosis_Description FROM vForm_IPS_VisitDiagnosis WHERE IPS_Infection_Id = @PrintValue ORDER BY IPS_VisitDiagnosis_Id";
        SqlDataSource_Print6.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print7;
      using (SqlDataSource_Print7 = new SqlDataSource())
      {
        SqlDataSource_Print7.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print7.SelectCommand = "SELECT IPS_BedHistory_Department , IPS_BedHistory_Room , IPS_BedHistory_Bed , IPS_BedHistory_Date FROM vForm_IPS_BedHistory WHERE IPS_Infection_Id = @PrintValue ORDER BY IPS_BedHistory_Id";
        SqlDataSource_Print7.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print8;
      using (SqlDataSource_Print8 = new SqlDataSource())
      {
        SqlDataSource_Print8.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print8.SelectCommand = "SELECT IPS_AntibioticPrescription_Description FROM Form_IPS_AntibioticPrescription WHERE IPS_VisitInformation_Id IN ( SELECT IPS_VisitInformation_Id FROM Form_IPS_Infection WHERE IPS_Infection_Id = @PrintValue ) ORDER BY IPS_AntibioticPrescription_Description";
        SqlDataSource_Print8.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print9;
      using (SqlDataSource_Print9 = new SqlDataSource())
      {
        SqlDataSource_Print9.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print9.SelectCommand = "SELECT 'Department of Health' AS Authority , IPS_Organism_Lookup_Description + ' (' + IPS_Organism_Lookup_Code + ')' AS IPS_Organism_Lookup , IPS_Organism_Notifiable_DepartmentOfHealth_Date , IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber FROM vForm_IPS_Organism WHERE IPS_Organism_Lookup_Notifiable = 1 AND IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id IN ( SELECT IPS_Specimen_Id FROM Form_IPS_Specimen WHERE IPS_Infection_Id = @PrintValue ) ) ORDER BY IPS_Organism_Lookup_Description";
        SqlDataSource_Print9.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      e.DataSources.Add(new ReportDataSource("Form_IPS_Infection_Header", SqlDataSource_Print1));
      e.DataSources.Add(new ReportDataSource("Form_IPS_HAI_Investigation", SqlDataSource_Print2));
      e.DataSources.Add(new ReportDataSource("Form_IPS_VisitInformation", SqlDataSource_Print3));
      e.DataSources.Add(new ReportDataSource("Form_IPS_Infection", SqlDataSource_Print4));
      e.DataSources.Add(new ReportDataSource("Form_IPS_Theatre", SqlDataSource_Print5));
      e.DataSources.Add(new ReportDataSource("Form_IPS_VisitDiagnosis", SqlDataSource_Print6));
      e.DataSources.Add(new ReportDataSource("Form_IPS_BedHistory", SqlDataSource_Print7));
      e.DataSources.Add(new ReportDataSource("Form_IPS_AntibioticPrescription", SqlDataSource_Print8));
      e.DataSources.Add(new ReportDataSource("Form_IPS_Organism", SqlDataSource_Print9));
    }

    void SubReport_Form_IPS_Investigation(object sender, SubreportProcessingEventArgs e)
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT Facility_FacilityDisplayName , IPS_Infection_ReportNumber FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT Form_IPS_HAI.IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print3.SelectCommand = "SELECT IPS_HAI_PredisposingCondition_Condition_Name , IPS_HAI_PredisposingCondition_Description FROM vForm_IPS_HAI_PredisposingCondition WHERE IPS_HAI_Id IN ( SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue ) ORDER BY IPS_HAI_PredisposingCondition_Condition_Name";
        SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print4;
      using (SqlDataSource_Print4 = new SqlDataSource())
      {
        SqlDataSource_Print4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print4.SelectCommand = "SELECT SpecimenStatus , SpecimenDate , SpecimenTime , SpecimenType , SpecimenResultLabNumber , Organism , OrganismResistance , OrganismResistanceName , ( SELECT Antibiogram + '; ' FROM ( SELECT * FROM ( SELECT vForm_IPS_Specimen.IPS_Specimen_Status_Name AS SpecimenStatus , CONVERT(VARCHAR(10),vForm_IPS_Specimen.IPS_Specimen_Date,111) AS SpecimenDate , vForm_IPS_Specimen.IPS_Specimen_TimeHours + ':' + vForm_IPS_Specimen.IPS_Specimen_TimeMinutes AS SpecimenTime , vForm_IPS_Specimen.IPS_Specimen_Type_Name AS SpecimenType , Form_IPS_SpecimenResult.IPS_SpecimenResult_LabNumber AS SpecimenResultLabNumber , vForm_IPS_Organism.IPS_Organism_Lookup_Description + ' (' + vForm_IPS_Organism.IPS_Organism_Lookup_Code + ')' AS Organism , CASE WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 1 THEN 'Yes' WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 0 THEN 'No' END AS OrganismResistance , vForm_IPS_Organism.IPS_Organism_Resistance_Name AS OrganismResistanceName , vForm_IPS_Antibiogram.IPS_Antibiogram_Lookup_Description + ' (' + vForm_IPS_Antibiogram.IPS_Antibiogram_Lookup_Code + ') -- ' + vForm_IPS_Antibiogram.IPS_Antibiogram_SRI_Name AS Antibiogram FROM vForm_IPS_Specimen LEFT JOIN Form_IPS_SpecimenResult ON vForm_IPS_Specimen.IPS_Specimen_Id = Form_IPS_SpecimenResult.IPS_Specimen_Id LEFT JOIN vForm_IPS_Organism ON Form_IPS_SpecimenResult.IPS_SpecimenResult_Id = vForm_IPS_Organism.IPS_SpecimenResult_Id LEFT JOIN vForm_IPS_Antibiogram ON vForm_IPS_Organism.IPS_Organism_Id = vForm_IPS_Antibiogram.IPS_Organism_Id WHERE vForm_IPS_Specimen.IPS_Infection_Id = @PrintValue AND (vForm_IPS_Specimen.IPS_Specimen_IsActive = 1 OR vForm_IPS_Specimen.IPS_Specimen_IsActive IS NULL) AND (Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive = 1 OR Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive IS NULL) AND (vForm_IPS_Organism.IPS_Organism_IsActive = 1 OR vForm_IPS_Organism.IPS_Organism_IsActive IS NULL) AND (vForm_IPS_Antibiogram.IPS_Antibiogram_IsActive = 1 OR vForm_IPS_Antibiogram.IPS_Antibiogram_IsActive IS NULL) ) AS TempTableA WHERE EXISTS ( SELECT * FROM ( SELECT IPS_HAI_LabReports_SpecimenStatus , IPS_HAI_LabReports_SpecimenDate , IPS_HAI_LabReports_SpecimenTime , IPS_HAI_LabReports_SpecimenType , IPS_HAI_LabReports_SpecimenResultLabNumber , IPS_HAI_LabReports_Organism , IPS_HAI_LabReports_OrganismResistance , IPS_HAI_LabReports_OrganismResistanceName FROM Form_IPS_HAI_LabReports WHERE IPS_HAI_Id IN ( SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue ) ) AS TempTableB WHERE	TempTableB.IPS_HAI_LabReports_SpecimenStatus = ISNULL(TempTableA.SpecimenStatus,'') AND TempTableB.IPS_HAI_LabReports_SpecimenDate = ISNULL(TempTableA.SpecimenDate,'') AND TempTableB.IPS_HAI_LabReports_SpecimenTime = ISNULL(TempTableA.SpecimenTime,'') AND TempTableB.IPS_HAI_LabReports_SpecimenType = ISNULL(TempTableA.SpecimenType,'') AND TempTableB.IPS_HAI_LabReports_SpecimenResultLabNumber = ISNULL(TempTableA.SpecimenResultLabNumber,'') AND TempTableB.IPS_HAI_LabReports_Organism = ISNULL(TempTableA.Organism,'') AND TempTableB.IPS_HAI_LabReports_OrganismResistance = ISNULL(TempTableA.OrganismResistance,'') AND TempTableB.IPS_HAI_LabReports_OrganismResistanceName = ISNULL(TempTableA.OrganismResistanceName,'') ) ) AS TempTableC WHERE TempTableC.Organism = TempTableD.Organism FOR XML PATH('') ) AS Antibiogram FROM ( SELECT * FROM ( SELECT vForm_IPS_Specimen.IPS_Specimen_Status_Name AS SpecimenStatus , CONVERT(VARCHAR(10),vForm_IPS_Specimen.IPS_Specimen_Date,111) AS SpecimenDate , vForm_IPS_Specimen.IPS_Specimen_TimeHours + ':' + vForm_IPS_Specimen.IPS_Specimen_TimeMinutes AS SpecimenTime , vForm_IPS_Specimen.IPS_Specimen_Type_Name AS SpecimenType , Form_IPS_SpecimenResult.IPS_SpecimenResult_LabNumber AS SpecimenResultLabNumber , vForm_IPS_Organism.IPS_Organism_Lookup_Description + ' (' + vForm_IPS_Organism.IPS_Organism_Lookup_Code + ')' AS Organism , CASE WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 1 THEN 'Yes' WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 0 THEN 'No' END AS OrganismResistance , vForm_IPS_Organism.IPS_Organism_Resistance_Name AS OrganismResistanceName , vForm_IPS_Antibiogram.IPS_Antibiogram_Lookup_Description + ' (' + vForm_IPS_Antibiogram.IPS_Antibiogram_Lookup_Code + ') -- ' + vForm_IPS_Antibiogram.IPS_Antibiogram_SRI_Name AS Antibiogram FROM vForm_IPS_Specimen LEFT JOIN Form_IPS_SpecimenResult ON vForm_IPS_Specimen.IPS_Specimen_Id = Form_IPS_SpecimenResult.IPS_Specimen_Id LEFT JOIN vForm_IPS_Organism ON Form_IPS_SpecimenResult.IPS_SpecimenResult_Id = vForm_IPS_Organism.IPS_SpecimenResult_Id LEFT JOIN vForm_IPS_Antibiogram ON vForm_IPS_Organism.IPS_Organism_Id = vForm_IPS_Antibiogram.IPS_Organism_Id WHERE vForm_IPS_Specimen.IPS_Infection_Id = @PrintValue AND (vForm_IPS_Specimen.IPS_Specimen_IsActive = 1 OR vForm_IPS_Specimen.IPS_Specimen_IsActive IS NULL) AND (Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive = 1 OR Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive IS NULL) AND (vForm_IPS_Organism.IPS_Organism_IsActive = 1 OR vForm_IPS_Organism.IPS_Organism_IsActive IS NULL) AND (vForm_IPS_Antibiogram.IPS_Antibiogram_IsActive = 1 OR vForm_IPS_Antibiogram.IPS_Antibiogram_IsActive IS NULL) ) AS TempTableA WHERE EXISTS ( SELECT * FROM ( SELECT IPS_HAI_LabReports_SpecimenStatus , IPS_HAI_LabReports_SpecimenDate , IPS_HAI_LabReports_SpecimenTime , IPS_HAI_LabReports_SpecimenType , IPS_HAI_LabReports_SpecimenResultLabNumber , IPS_HAI_LabReports_Organism , IPS_HAI_LabReports_OrganismResistance , IPS_HAI_LabReports_OrganismResistanceName FROM Form_IPS_HAI_LabReports WHERE IPS_HAI_Id IN ( SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue ) ) AS TempTableB WHERE	TempTableB.IPS_HAI_LabReports_SpecimenStatus = ISNULL(TempTableA.SpecimenStatus,'') AND TempTableB.IPS_HAI_LabReports_SpecimenDate = ISNULL(TempTableA.SpecimenDate,'') AND TempTableB.IPS_HAI_LabReports_SpecimenTime = ISNULL(TempTableA.SpecimenTime,'') AND TempTableB.IPS_HAI_LabReports_SpecimenType = ISNULL(TempTableA.SpecimenType,'') AND TempTableB.IPS_HAI_LabReports_SpecimenResultLabNumber = ISNULL(TempTableA.SpecimenResultLabNumber,'') AND TempTableB.IPS_HAI_LabReports_Organism = ISNULL(TempTableA.Organism,'') AND TempTableB.IPS_HAI_LabReports_OrganismResistance = ISNULL(TempTableA.OrganismResistance,'') AND TempTableB.IPS_HAI_LabReports_OrganismResistanceName = ISNULL(TempTableA.OrganismResistanceName,'') ) ) AS TempTableD GROUP BY SpecimenStatus , SpecimenDate , SpecimenTime , SpecimenType , SpecimenResultLabNumber , Organism , OrganismResistance , OrganismResistanceName ORDER BY TempTableD.SpecimenDate , TempTableD.SpecimenTime";
        SqlDataSource_Print4.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print5;
      using (SqlDataSource_Print5 = new SqlDataSource())
      {
        SqlDataSource_Print5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print5.SelectCommand = "SELECT vForm_IPS_Infection.IPS_Infection_Type_List , vForm_IPS_Infection.IPS_Infection_Type_Name , Form_IPS_HAI.IPS_HAI_BundleCompliance_Days , Administration_ListItem.ListItem_Name , CASE WHEN Form_IPS_HAI_BundleComplianceQA.IPS_HAI_BundleComplianceQA_Answer = 1 THEN 'Yes' WHEN Form_IPS_HAI_BundleComplianceQA.IPS_HAI_BundleComplianceQA_Answer = 0 THEN 'No' END AS IPS_HAI_BundleComplianceQA_Answer , CASE WHEN Form_IPS_HAI.IPS_HAI_BundleCompliance_TPN = 1 THEN 'Yes' WHEN Form_IPS_HAI.IPS_HAI_BundleCompliance_TPN = 0 THEN 'No' END AS IPS_HAI_BundleCompliance_TPN , CASE WHEN Form_IPS_HAI.IPS_HAI_BundleCompliance_EnteralFeeding = 1 THEN 'Yes' WHEN Form_IPS_HAI.IPS_HAI_BundleCompliance_EnteralFeeding = 0 THEN 'No' END AS IPS_HAI_BundleCompliance_EnteralFeeding FROM vForm_IPS_Infection LEFT JOIN Form_IPS_HAI ON vForm_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id LEFT JOIN Form_IPS_HAI_BundleComplianceQA ON Form_IPS_HAI.IPS_HAI_Id = Form_IPS_HAI_BundleComplianceQA.IPS_HAI_Id LEFT JOIN Administration_ListItem ON Form_IPS_HAI_BundleComplianceQA.IPS_HAI_BundleComplianceQA_Question_List = Administration_ListItem.ListItem_Id WHERE vForm_IPS_Infection.IPS_Infection_Id = @PrintValue ORDER BY Administration_ListItem.ListItem_Name";
        SqlDataSource_Print5.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print6;
      using (SqlDataSource_Print6 = new SqlDataSource())
      {
        SqlDataSource_Print6.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print6.SelectCommand = "SELECT CASE WHEN IPS_HAI_BundleCompliance_TPN = 1 THEN 'Yes' WHEN IPS_HAI_BundleCompliance_TPN = 0 THEN 'No' END AS IPS_HAI_BundleCompliance_TPN , CASE WHEN IPS_HAI_BundleCompliance_EnteralFeeding = 1 THEN 'Yes' WHEN IPS_HAI_BundleCompliance_EnteralFeeding = 0 THEN 'No' END AS IPS_HAI_BundleCompliance_EnteralFeeding FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print6.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print7;
      using (SqlDataSource_Print7 = new SqlDataSource())
      {
        SqlDataSource_Print7.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print7.SelectCommand = "SELECT IPS_HAI_Investigation_Date , IPS_HAI_Investigation_InvestigatorName , IPS_HAI_Investigation_InvestigatorDesignation , IPS_HAI_Investigation_IPCName , IPS_HAI_Investigation_TeamMembers FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print7.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print8;
      using (SqlDataSource_Print8 = new SqlDataSource())
      {
        SqlDataSource_Print8.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print8.SelectCommand = "SELECT IPS_HAI_Measures_Measure_Name FROM vForm_IPS_HAI_Measures WHERE IPS_HAI_Id IN (SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue) ORDER BY IPS_HAI_Measures_Measure_Name";
        SqlDataSource_Print8.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print9;
      using (SqlDataSource_Print9 = new SqlDataSource())
      {
        SqlDataSource_Print9.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print9.SelectCommand = "SELECT IPS_VisitInformation_VisitNumber , PatientInformation_Name , PatientInformation_Surname , PatientInformation_Gender , IPS_VisitInformation_PatientAge , PatientInformation_DateOfBirth , IPS_VisitInformation_DateOfAdmission , IPS_VisitInformation_DateOfDischarge , IPS_VisitInformation_Deceased , IPS_VisitInformation_Ward FROM vForm_IPS_VisitInformation WHERE IPS_VisitInformation_Id IN ( SELECT IPS_VisitInformation_Id FROM Form_IPS_Infection WHERE IPS_Infection_Id = @PrintValue )";
        SqlDataSource_Print9.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print10;
      using (SqlDataSource_Print10 = new SqlDataSource())
      {
        SqlDataSource_Print10.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print10.SelectCommand = "SELECT IPS_Infection_Category_List , IPS_Infection_Category_Name , IPS_Infection_Type_List , IPS_Infection_Type_Name , IPS_Infection_SubType_Name , IPS_Infection_SubSubType_Name , CASE WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4996 THEN vForm_IPS_Infection.IPS_Infection_Site_Name WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4996') THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4997') THEN 'Linked Site Required' ELSE vForm_IPS_Infection.IPS_Infection_Site_Name + ' (' + vForm_IPS_Infection.IPS_Infection_Site_Infection_ReportNumber + ')' END	AS IPS_Infection_Site_Name , CASE WHEN IPS_Infection_Screening = 1 THEN 'Yes' WHEN IPS_Infection_Screening = 0 THEN 'No' END AS IPS_Infection_Screening , IPS_ScreeningType_Type_Name , IPS_Infection_ScreeningReason_Name , Unit_Name , IPS_Infection_Summary FROM vForm_IPS_Infection LEFT JOIN vForm_IPS_Infection_ScreeningType ON vForm_IPS_Infection.IPS_Infection_Id = vForm_IPS_Infection_ScreeningType.IPS_Infection_Id WHERE vForm_IPS_Infection.IPS_Infection_Id = @PrintValue ORDER BY IPS_ScreeningType_Type_Name";
        SqlDataSource_Print10.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print11;
      using (SqlDataSource_Print11 = new SqlDataSource())
      {
        SqlDataSource_Print11.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print11.SelectCommand = "SELECT CASE @PrintPage WHEN 'Form_IPS_Investigation' THEN 'Yes' ELSE 'No' END AS PrintPage_Investigation";
        SqlDataSource_Print11.SelectParameters.Add("PrintPage", Request.QueryString["PrintPage"]);
      }

      e.DataSources.Add(new ReportDataSource("Form_IPS_Infection_Header", SqlDataSource_Print1));
      e.DataSources.Add(new ReportDataSource("Form_IPS_HAI_Investigation", SqlDataSource_Print2));
      e.DataSources.Add(new ReportDataSource("Form_IPS_HAI_PredisposingCondition", SqlDataSource_Print3));
      e.DataSources.Add(new ReportDataSource("Form_IPS_HAI_LabReports", SqlDataSource_Print4));
      e.DataSources.Add(new ReportDataSource("Form_IPS_HAI_BundleCompliance", SqlDataSource_Print5));
      e.DataSources.Add(new ReportDataSource("Form_IPS_HAI_HighRiskProcedures", SqlDataSource_Print6));
      e.DataSources.Add(new ReportDataSource("Form_IPS_HAI", SqlDataSource_Print7));
      e.DataSources.Add(new ReportDataSource("Form_IPS_HAI_Measures", SqlDataSource_Print8));
      e.DataSources.Add(new ReportDataSource("Form_IPS_VisitInformation", SqlDataSource_Print9));
      e.DataSources.Add(new ReportDataSource("Form_IPS_Infection", SqlDataSource_Print10));
      e.DataSources.Add(new ReportDataSource("Form_IPS_PrintPage", SqlDataSource_Print11));
    }

    private void Form_IPS_InfectionDetail()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT Facility_FacilityDisplayName , IPS_Infection_ReportNumber FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT Form_IPS_HAI.IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print3.SelectCommand = "SELECT IPS_VisitInformation_VisitNumber , PatientInformation_Name , PatientInformation_Surname , PatientInformation_Gender , IPS_VisitInformation_PatientAge , PatientInformation_DateOfBirth , IPS_VisitInformation_DateOfAdmission , IPS_VisitInformation_DateOfDischarge , IPS_VisitInformation_Deceased , IPS_VisitInformation_Ward FROM vForm_IPS_VisitInformation WHERE IPS_VisitInformation_Id IN ( SELECT IPS_VisitInformation_Id FROM Form_IPS_Infection WHERE IPS_Infection_Id = @PrintValue )";
        SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print4;
      using (SqlDataSource_Print4 = new SqlDataSource())
      {
        SqlDataSource_Print4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print4.SelectCommand = "SELECT IPS_Infection_Category_List , IPS_Infection_Category_Name , IPS_Infection_Type_List , IPS_Infection_Type_Name , IPS_Infection_SubType_Name , IPS_Infection_SubSubType_Name , CASE WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4996 THEN vForm_IPS_Infection.IPS_Infection_Site_Name WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4996') THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4997') THEN 'Linked Site Required' ELSE vForm_IPS_Infection.IPS_Infection_Site_Name + ' (' + vForm_IPS_Infection.IPS_Infection_Site_Infection_ReportNumber + ')' END	AS IPS_Infection_Site_Name , CASE WHEN IPS_Infection_Screening = 1 THEN 'Yes' WHEN IPS_Infection_Screening = 0 THEN 'No' END AS IPS_Infection_Screening , IPS_ScreeningType_Type_Name , IPS_Infection_ScreeningReason_Name , Unit_Name , IPS_Infection_Summary FROM vForm_IPS_Infection LEFT JOIN vForm_IPS_Infection_ScreeningType ON vForm_IPS_Infection.IPS_Infection_Id = vForm_IPS_Infection_ScreeningType.IPS_Infection_Id WHERE vForm_IPS_Infection.IPS_Infection_Id = @PrintValue ORDER BY IPS_ScreeningType_Type_Name";
        SqlDataSource_Print4.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print5;
      using (SqlDataSource_Print5 = new SqlDataSource())
      {
        SqlDataSource_Print5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print5.SelectCommand = "SELECT IPS_Theatre_FacilityDisplayName , IPS_Theatre_DateOfAdmission , IPS_Theatre_DateOfDischarge , IPS_Theatre_FinalDiagnosisCode , IPS_Theatre_FinalDiagnosisDescription , IPS_Theatre_VisitNumber , IPS_Theatre_ServiceCategory , IPS_Theatre_Theatre , IPS_Theatre_TheatreTime , IPS_Theatre_ProcedureDate , IPS_Theatre_ProcedureCode , IPS_Theatre_ProcedureDescription , IPS_Theatre_TheatreInvoice , IPS_Theatre_Surgeon , IPS_Theatre_Anaesthetist , IPS_Theatre_Assistant , IPS_Theatre_WoundCategory , IPS_Theatre_ScrubNurse FROM vForm_IPS_Theatre WHERE IPS_Infection_Id = @PrintValue ORDER BY IPS_Theatre_DateOfAdmission DESC , IPS_Theatre_ProcedureDate DESC";
        SqlDataSource_Print5.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print6;
      using (SqlDataSource_Print6 = new SqlDataSource())
      {
        SqlDataSource_Print6.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print6.SelectCommand = "SELECT IPS_VisitDiagnosis_CodeType , IPS_VisitDiagnosis_Code , IPS_VisitDiagnosis_Description FROM vForm_IPS_VisitDiagnosis WHERE IPS_Infection_Id = @PrintValue ORDER BY IPS_VisitDiagnosis_Id";
        SqlDataSource_Print6.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print7;
      using (SqlDataSource_Print7 = new SqlDataSource())
      {
        SqlDataSource_Print7.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print7.SelectCommand = "SELECT IPS_BedHistory_Department , IPS_BedHistory_Room , IPS_BedHistory_Bed , IPS_BedHistory_Date FROM vForm_IPS_BedHistory WHERE IPS_Infection_Id = @PrintValue ORDER BY IPS_BedHistory_Id";
        SqlDataSource_Print7.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print8;
      using (SqlDataSource_Print8 = new SqlDataSource())
      {
        SqlDataSource_Print8.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print8.SelectCommand = "SELECT IPS_AntibioticPrescription_Description FROM Form_IPS_AntibioticPrescription WHERE IPS_VisitInformation_Id IN ( SELECT IPS_VisitInformation_Id FROM Form_IPS_Infection WHERE IPS_Infection_Id = @PrintValue ) ORDER BY IPS_AntibioticPrescription_Description";
        SqlDataSource_Print8.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print9;
      using (SqlDataSource_Print9 = new SqlDataSource())
      {
        SqlDataSource_Print9.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print9.SelectCommand = "SELECT 'Department of Health' AS Authority , IPS_Organism_Lookup_Description + ' (' + IPS_Organism_Lookup_Code + ')' AS IPS_Organism_Lookup , IPS_Organism_Notifiable_DepartmentOfHealth_Date , IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber FROM vForm_IPS_Organism WHERE IPS_Organism_Lookup_Notifiable = 1 AND IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id IN ( SELECT IPS_Specimen_Id FROM Form_IPS_Specimen WHERE IPS_Infection_Id = @PrintValue ) ) ORDER BY IPS_Organism_Lookup_Description";
        SqlDataSource_Print9.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_IPS_InfectionDetail.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_Infection_Header", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_HAI_Investigation", SqlDataSource_Print2));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_VisitInformation", SqlDataSource_Print3));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_Infection", SqlDataSource_Print4));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_Theatre", SqlDataSource_Print5));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_VisitDiagnosis", SqlDataSource_Print6));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_BedHistory", SqlDataSource_Print7));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_AntibioticPrescription", SqlDataSource_Print8));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_Organism", SqlDataSource_Print9));
    }

    private void Form_IPS_InfectionDetail_Extra()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT Facility_FacilityDisplayName , IPS_Infection_ReportNumber FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_IPS_InfectionDetail_Extra.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_Infection_Header", SqlDataSource_Print1));
    }

    private void Form_IPS_Investigation()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT Facility_FacilityDisplayName , IPS_Infection_ReportNumber FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print2;
      using (SqlDataSource_Print2 = new SqlDataSource())
      {
        SqlDataSource_Print2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print2.SelectCommand = "SELECT Form_IPS_HAI.IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print2.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print3;
      using (SqlDataSource_Print3 = new SqlDataSource())
      {
        SqlDataSource_Print3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print3.SelectCommand = "SELECT IPS_HAI_PredisposingCondition_Condition_Name , IPS_HAI_PredisposingCondition_Description FROM vForm_IPS_HAI_PredisposingCondition WHERE IPS_HAI_Id IN ( SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue ) ORDER BY IPS_HAI_PredisposingCondition_Condition_Name";
        SqlDataSource_Print3.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print4;
      using (SqlDataSource_Print4 = new SqlDataSource())
      {
        SqlDataSource_Print4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print4.SelectCommand = "SELECT SpecimenStatus , SpecimenDate , SpecimenTime , SpecimenType , SpecimenResultLabNumber , Organism , OrganismResistance , OrganismResistanceName , ( SELECT Antibiogram + '; ' FROM ( SELECT * FROM ( SELECT vForm_IPS_Specimen.IPS_Specimen_Status_Name AS SpecimenStatus , CONVERT(VARCHAR(10),vForm_IPS_Specimen.IPS_Specimen_Date,111) AS SpecimenDate , vForm_IPS_Specimen.IPS_Specimen_TimeHours + ':' + vForm_IPS_Specimen.IPS_Specimen_TimeMinutes AS SpecimenTime , vForm_IPS_Specimen.IPS_Specimen_Type_Name AS SpecimenType , Form_IPS_SpecimenResult.IPS_SpecimenResult_LabNumber AS SpecimenResultLabNumber , vForm_IPS_Organism.IPS_Organism_Lookup_Description + ' (' + vForm_IPS_Organism.IPS_Organism_Lookup_Code + ')' AS Organism , CASE WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 1 THEN 'Yes' WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 0 THEN 'No' END AS OrganismResistance , vForm_IPS_Organism.IPS_Organism_Resistance_Name AS OrganismResistanceName , vForm_IPS_Antibiogram.IPS_Antibiogram_Lookup_Description + ' (' + vForm_IPS_Antibiogram.IPS_Antibiogram_Lookup_Code + ') -- ' + vForm_IPS_Antibiogram.IPS_Antibiogram_SRI_Name AS Antibiogram FROM vForm_IPS_Specimen LEFT JOIN Form_IPS_SpecimenResult ON vForm_IPS_Specimen.IPS_Specimen_Id = Form_IPS_SpecimenResult.IPS_Specimen_Id LEFT JOIN vForm_IPS_Organism ON Form_IPS_SpecimenResult.IPS_SpecimenResult_Id = vForm_IPS_Organism.IPS_SpecimenResult_Id LEFT JOIN vForm_IPS_Antibiogram ON vForm_IPS_Organism.IPS_Organism_Id = vForm_IPS_Antibiogram.IPS_Organism_Id WHERE vForm_IPS_Specimen.IPS_Infection_Id = @PrintValue AND (vForm_IPS_Specimen.IPS_Specimen_IsActive = 1 OR vForm_IPS_Specimen.IPS_Specimen_IsActive IS NULL) AND (Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive = 1 OR Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive IS NULL) AND (vForm_IPS_Organism.IPS_Organism_IsActive = 1 OR vForm_IPS_Organism.IPS_Organism_IsActive IS NULL) AND (vForm_IPS_Antibiogram.IPS_Antibiogram_IsActive = 1 OR vForm_IPS_Antibiogram.IPS_Antibiogram_IsActive IS NULL) ) AS TempTableA WHERE EXISTS ( SELECT * FROM ( SELECT IPS_HAI_LabReports_SpecimenStatus , IPS_HAI_LabReports_SpecimenDate , IPS_HAI_LabReports_SpecimenTime , IPS_HAI_LabReports_SpecimenType , IPS_HAI_LabReports_SpecimenResultLabNumber , IPS_HAI_LabReports_Organism , IPS_HAI_LabReports_OrganismResistance , IPS_HAI_LabReports_OrganismResistanceName FROM Form_IPS_HAI_LabReports WHERE IPS_HAI_Id IN ( SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue ) ) AS TempTableB WHERE	TempTableB.IPS_HAI_LabReports_SpecimenStatus = ISNULL(TempTableA.SpecimenStatus,'') AND TempTableB.IPS_HAI_LabReports_SpecimenDate = ISNULL(TempTableA.SpecimenDate,'') AND TempTableB.IPS_HAI_LabReports_SpecimenTime = ISNULL(TempTableA.SpecimenTime,'') AND TempTableB.IPS_HAI_LabReports_SpecimenType = ISNULL(TempTableA.SpecimenType,'') AND TempTableB.IPS_HAI_LabReports_SpecimenResultLabNumber = ISNULL(TempTableA.SpecimenResultLabNumber,'') AND TempTableB.IPS_HAI_LabReports_Organism = ISNULL(TempTableA.Organism,'') AND TempTableB.IPS_HAI_LabReports_OrganismResistance = ISNULL(TempTableA.OrganismResistance,'') AND TempTableB.IPS_HAI_LabReports_OrganismResistanceName = ISNULL(TempTableA.OrganismResistanceName,'') ) ) AS TempTableC WHERE TempTableC.Organism = TempTableD.Organism FOR XML PATH('') ) AS Antibiogram FROM ( SELECT * FROM ( SELECT vForm_IPS_Specimen.IPS_Specimen_Status_Name AS SpecimenStatus , CONVERT(VARCHAR(10),vForm_IPS_Specimen.IPS_Specimen_Date,111) AS SpecimenDate , vForm_IPS_Specimen.IPS_Specimen_TimeHours + ':' + vForm_IPS_Specimen.IPS_Specimen_TimeMinutes AS SpecimenTime , vForm_IPS_Specimen.IPS_Specimen_Type_Name AS SpecimenType , Form_IPS_SpecimenResult.IPS_SpecimenResult_LabNumber AS SpecimenResultLabNumber , vForm_IPS_Organism.IPS_Organism_Lookup_Description + ' (' + vForm_IPS_Organism.IPS_Organism_Lookup_Code + ')' AS Organism , CASE WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 1 THEN 'Yes' WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 0 THEN 'No' END AS OrganismResistance , vForm_IPS_Organism.IPS_Organism_Resistance_Name AS OrganismResistanceName , vForm_IPS_Antibiogram.IPS_Antibiogram_Lookup_Description + ' (' + vForm_IPS_Antibiogram.IPS_Antibiogram_Lookup_Code + ') -- ' + vForm_IPS_Antibiogram.IPS_Antibiogram_SRI_Name AS Antibiogram FROM vForm_IPS_Specimen LEFT JOIN Form_IPS_SpecimenResult ON vForm_IPS_Specimen.IPS_Specimen_Id = Form_IPS_SpecimenResult.IPS_Specimen_Id LEFT JOIN vForm_IPS_Organism ON Form_IPS_SpecimenResult.IPS_SpecimenResult_Id = vForm_IPS_Organism.IPS_SpecimenResult_Id LEFT JOIN vForm_IPS_Antibiogram ON vForm_IPS_Organism.IPS_Organism_Id = vForm_IPS_Antibiogram.IPS_Organism_Id WHERE vForm_IPS_Specimen.IPS_Infection_Id = @PrintValue AND (vForm_IPS_Specimen.IPS_Specimen_IsActive = 1 OR vForm_IPS_Specimen.IPS_Specimen_IsActive IS NULL) AND (Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive = 1 OR Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive IS NULL) AND (vForm_IPS_Organism.IPS_Organism_IsActive = 1 OR vForm_IPS_Organism.IPS_Organism_IsActive IS NULL) AND (vForm_IPS_Antibiogram.IPS_Antibiogram_IsActive = 1 OR vForm_IPS_Antibiogram.IPS_Antibiogram_IsActive IS NULL) ) AS TempTableA WHERE EXISTS ( SELECT * FROM ( SELECT IPS_HAI_LabReports_SpecimenStatus , IPS_HAI_LabReports_SpecimenDate , IPS_HAI_LabReports_SpecimenTime , IPS_HAI_LabReports_SpecimenType , IPS_HAI_LabReports_SpecimenResultLabNumber , IPS_HAI_LabReports_Organism , IPS_HAI_LabReports_OrganismResistance , IPS_HAI_LabReports_OrganismResistanceName FROM Form_IPS_HAI_LabReports WHERE IPS_HAI_Id IN ( SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue ) ) AS TempTableB WHERE	TempTableB.IPS_HAI_LabReports_SpecimenStatus = ISNULL(TempTableA.SpecimenStatus,'') AND TempTableB.IPS_HAI_LabReports_SpecimenDate = ISNULL(TempTableA.SpecimenDate,'') AND TempTableB.IPS_HAI_LabReports_SpecimenTime = ISNULL(TempTableA.SpecimenTime,'') AND TempTableB.IPS_HAI_LabReports_SpecimenType = ISNULL(TempTableA.SpecimenType,'') AND TempTableB.IPS_HAI_LabReports_SpecimenResultLabNumber = ISNULL(TempTableA.SpecimenResultLabNumber,'') AND TempTableB.IPS_HAI_LabReports_Organism = ISNULL(TempTableA.Organism,'') AND TempTableB.IPS_HAI_LabReports_OrganismResistance = ISNULL(TempTableA.OrganismResistance,'') AND TempTableB.IPS_HAI_LabReports_OrganismResistanceName = ISNULL(TempTableA.OrganismResistanceName,'') ) ) AS TempTableD GROUP BY SpecimenStatus , SpecimenDate , SpecimenTime , SpecimenType , SpecimenResultLabNumber , Organism , OrganismResistance , OrganismResistanceName ORDER BY TempTableD.SpecimenDate , TempTableD.SpecimenTime";
        SqlDataSource_Print4.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print5;
      using (SqlDataSource_Print5 = new SqlDataSource())
      {
        SqlDataSource_Print5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print5.SelectCommand = "SELECT vForm_IPS_Infection.IPS_Infection_Type_List , vForm_IPS_Infection.IPS_Infection_Type_Name , Form_IPS_HAI.IPS_HAI_BundleCompliance_Days , Administration_ListItem.ListItem_Name , CASE WHEN Form_IPS_HAI_BundleComplianceQA.IPS_HAI_BundleComplianceQA_Answer = 1 THEN 'Yes' WHEN Form_IPS_HAI_BundleComplianceQA.IPS_HAI_BundleComplianceQA_Answer = 0 THEN 'No' END AS IPS_HAI_BundleComplianceQA_Answer , CASE WHEN Form_IPS_HAI.IPS_HAI_BundleCompliance_TPN = 1 THEN 'Yes' WHEN Form_IPS_HAI.IPS_HAI_BundleCompliance_TPN = 0 THEN 'No' END AS IPS_HAI_BundleCompliance_TPN , CASE WHEN Form_IPS_HAI.IPS_HAI_BundleCompliance_EnteralFeeding = 1 THEN 'Yes' WHEN Form_IPS_HAI.IPS_HAI_BundleCompliance_EnteralFeeding = 0 THEN 'No' END AS IPS_HAI_BundleCompliance_EnteralFeeding FROM vForm_IPS_Infection LEFT JOIN Form_IPS_HAI ON vForm_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id LEFT JOIN Form_IPS_HAI_BundleComplianceQA ON Form_IPS_HAI.IPS_HAI_Id = Form_IPS_HAI_BundleComplianceQA.IPS_HAI_Id LEFT JOIN Administration_ListItem ON Form_IPS_HAI_BundleComplianceQA.IPS_HAI_BundleComplianceQA_Question_List = Administration_ListItem.ListItem_Id WHERE vForm_IPS_Infection.IPS_Infection_Id = @PrintValue ORDER BY Administration_ListItem.ListItem_Name";
        SqlDataSource_Print5.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print6;
      using (SqlDataSource_Print6 = new SqlDataSource())
      {
        SqlDataSource_Print6.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print6.SelectCommand = "SELECT CASE WHEN IPS_HAI_BundleCompliance_TPN = 1 THEN 'Yes' WHEN IPS_HAI_BundleCompliance_TPN = 0 THEN 'No' END AS IPS_HAI_BundleCompliance_TPN , CASE WHEN IPS_HAI_BundleCompliance_EnteralFeeding = 1 THEN 'Yes' WHEN IPS_HAI_BundleCompliance_EnteralFeeding = 0 THEN 'No' END AS IPS_HAI_BundleCompliance_EnteralFeeding FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print6.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print7;
      using (SqlDataSource_Print7 = new SqlDataSource())
      {
        SqlDataSource_Print7.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print7.SelectCommand = "SELECT IPS_HAI_Investigation_Date , IPS_HAI_Investigation_InvestigatorName , IPS_HAI_Investigation_InvestigatorDesignation , IPS_HAI_Investigation_IPCName , IPS_HAI_Investigation_TeamMembers FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue";
        SqlDataSource_Print7.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print8;
      using (SqlDataSource_Print8 = new SqlDataSource())
      {
        SqlDataSource_Print8.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print8.SelectCommand = "SELECT IPS_HAI_Measures_Measure_Name FROM vForm_IPS_HAI_Measures WHERE IPS_HAI_Id IN (SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @PrintValue) ORDER BY IPS_HAI_Measures_Measure_Name";
        SqlDataSource_Print8.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print9;
      using (SqlDataSource_Print9 = new SqlDataSource())
      {
        SqlDataSource_Print9.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print9.SelectCommand = "SELECT IPS_VisitInformation_VisitNumber , PatientInformation_Name , PatientInformation_Surname , PatientInformation_Gender , IPS_VisitInformation_PatientAge , PatientInformation_DateOfBirth , IPS_VisitInformation_DateOfAdmission , IPS_VisitInformation_DateOfDischarge , IPS_VisitInformation_Deceased , IPS_VisitInformation_Ward FROM vForm_IPS_VisitInformation WHERE IPS_VisitInformation_Id IN ( SELECT IPS_VisitInformation_Id FROM Form_IPS_Infection WHERE IPS_Infection_Id = @PrintValue )";
        SqlDataSource_Print9.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print10;
      using (SqlDataSource_Print10 = new SqlDataSource())
      {
        SqlDataSource_Print10.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print10.SelectCommand = "SELECT IPS_Infection_Category_List , IPS_Infection_Category_Name , IPS_Infection_Type_List , IPS_Infection_Type_Name , IPS_Infection_SubType_Name , IPS_Infection_SubSubType_Name , CASE WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4996 THEN vForm_IPS_Infection.IPS_Infection_Site_Name WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4996') THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'Linked Site Required' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4997') THEN 'Linked Site Required' ELSE vForm_IPS_Infection.IPS_Infection_Site_Name + ' (' + vForm_IPS_Infection.IPS_Infection_Site_Infection_ReportNumber + ')' END	AS IPS_Infection_Site_Name , CASE WHEN IPS_Infection_Screening = 1 THEN 'Yes' WHEN IPS_Infection_Screening = 0 THEN 'No' END AS IPS_Infection_Screening , IPS_ScreeningType_Type_Name , IPS_Infection_ScreeningReason_Name , Unit_Name , IPS_Infection_Summary FROM vForm_IPS_Infection LEFT JOIN vForm_IPS_Infection_ScreeningType ON vForm_IPS_Infection.IPS_Infection_Id = vForm_IPS_Infection_ScreeningType.IPS_Infection_Id WHERE vForm_IPS_Infection.IPS_Infection_Id = @PrintValue ORDER BY IPS_ScreeningType_Type_Name";
        SqlDataSource_Print10.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      SqlDataSource SqlDataSource_Print11;
      using (SqlDataSource_Print11 = new SqlDataSource())
      {
        SqlDataSource_Print11.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print11.SelectCommand = "SELECT CASE @PrintPage WHEN 'Form_IPS_Investigation' THEN 'Yes' ELSE 'No' END AS PrintPage_Investigation";
        SqlDataSource_Print11.SelectParameters.Add("PrintPage", Request.QueryString["PrintPage"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_IPS_Investigation.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_Infection_Header", SqlDataSource_Print1));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_HAI_Investigation", SqlDataSource_Print2));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_HAI_PredisposingCondition", SqlDataSource_Print3));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_HAI_LabReports", SqlDataSource_Print4));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_HAI_BundleCompliance", SqlDataSource_Print5));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_HAI_HighRiskProcedures", SqlDataSource_Print6));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_HAI", SqlDataSource_Print7));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_HAI_Measures", SqlDataSource_Print8));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_VisitInformation", SqlDataSource_Print9));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_Infection", SqlDataSource_Print10));
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_IPS_PrintPage", SqlDataSource_Print11));
    }
    
    private void Form_MedicationBundleCompliance()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_MedicationBundleCompliance_Bundles WHERE MBC_Bundles_Id = @PrintValue ORDER BY vForm_MedicationBundleCompliance_Bundles.MBC_Bundles_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_MedicationBundleCompliance.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_MedicationBundleCompliance", SqlDataSource_Print1));
    }

    private void Form_MedicationBundleCompliance_All()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_MedicationBundleCompliance_Bundles WHERE MBC_VisitInformation_Id IN (SELECT MBC_VisitInformation_Id FROM vForm_MedicationBundleCompliance_Bundles WHERE MBC_Bundles_Id = @PrintValue) ORDER BY vForm_MedicationBundleCompliance_Bundles.MBC_Bundles_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_MedicationBundleCompliance_All.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_MedicationBundleCompliance_All", SqlDataSource_Print1));
    }

    private void Form_AntimicrobialStewardshipIntervention()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT vForm_AntimicrobialStewardshipIntervention_Antibiotic.* , vForm_AntimicrobialStewardshipIntervention_Nursing5B.ASI_Nursing5B_Item_Name FROM vForm_AntimicrobialStewardshipIntervention_Antibiotic LEFT JOIN vForm_AntimicrobialStewardshipIntervention_Nursing5B ON vForm_AntimicrobialStewardshipIntervention_Antibiotic.ASI_Intervention_Id = vForm_AntimicrobialStewardshipIntervention_Nursing5B.ASI_Intervention_Id WHERE vForm_AntimicrobialStewardshipIntervention_Antibiotic.ASI_Intervention_Id = @PrintValue ORDER BY vForm_AntimicrobialStewardshipIntervention_Antibiotic.ASI_Intervention_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_AntimicrobialStewardshipIntervention.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_AntimicrobialStewardshipIntervention_Antibiotic", SqlDataSource_Print1));
    }

    private void Form_AntimicrobialStewardshipIntervention_All()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT vForm_AntimicrobialStewardshipIntervention_Antibiotic.* , vForm_AntimicrobialStewardshipIntervention_Nursing5B.ASI_Nursing5B_Item_Name FROM vForm_AntimicrobialStewardshipIntervention_Antibiotic LEFT JOIN vForm_AntimicrobialStewardshipIntervention_Nursing5B ON vForm_AntimicrobialStewardshipIntervention_Antibiotic.ASI_Intervention_Id = vForm_AntimicrobialStewardshipIntervention_Nursing5B.ASI_Intervention_Id WHERE vForm_AntimicrobialStewardshipIntervention_Antibiotic.ASI_VisitInformation_Id IN ( SELECT ASI_VisitInformation_Id FROM vForm_AntimicrobialStewardshipIntervention_Intervention WHERE ASI_Intervention_Id = @PrintValue ) ORDER BY vForm_AntimicrobialStewardshipIntervention_Antibiotic.ASI_Intervention_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_AntimicrobialStewardshipIntervention_All.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_AntimicrobialStewardshipIntervention_Antibiotic", SqlDataSource_Print1));
    }

    private void Form_HeadOfficeQualityAudit()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_HeadOfficeQualityAudit.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_HeadOfficeQualityAudit", SqlDataSource_Print1));
    }

    private void Form_TransparencyRegister()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_TransparencyRegister_Classification WHERE TransparencyRegister_Id = @PrintValue ORDER BY TransparencyRegister_Classification_Item_Name";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_TransparencyRegister.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_TransparencyRegister_Classification", SqlDataSource_Print1));
    }

    private void Form_SustainabilityManagement()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "EXECUTE spForm_Get_SustainabilityManagement_Item @SecurityUser , @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("SecurityUser", Request.ServerVariables["LOGON_USER"]);
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_SustainabilityManagement.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_SustainabilityManagement", SqlDataSource_Print1));
    }

    private void Form_MonthlyOccupationalHealthStatistics()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_MonthlyOccupationalHealthStatistics WHERE MOHS_Id = @PrintValue";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_MonthlyOccupationalHealthStatistics.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_MonthlyOccupationalHealthStatistics", SqlDataSource_Print1));
    }

    private void Form_PharmacySurveys()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = @"SELECT		Facility_FacilityDisplayName ,
					                                              vForm_PharmacySurveys_CreatedSurveys.LoadedSurveys_Name ,
					                                              vForm_PharmacySurveys_CreatedSurveys.LoadedSurveys_FY ,
					                                              CreatedSurveys_Name , 
					                                              Unit_Name , 
					                                              CreatedSurveys_Designation ,
					                                              LoadedSections_Name ,
					                                              LoadedQuestions_Name , 
					                                              LoadedAnswers_Name ,
					                                              CreatedSurveys_Comments , 
					                                              CreatedSurveys_Compliment
                                              FROM			vForm_PharmacySurveys_CreatedSurveys
					                                              LEFT JOIN Form_PharmacySurveys_CreatedSurveyAnswers ON vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id
					                                              LEFT JOIN Form_PharmacySurveys_LoadedSurveys ON vForm_PharmacySurveys_CreatedSurveys.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id
					                                              LEFT JOIN Form_PharmacySurveys_LoadedSections ON Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSections.LoadedSurveys_Id
					                                              LEFT JOIN Form_PharmacySurveys_LoadedQuestions ON Form_PharmacySurveys_LoadedSections.LoadedSections_Id = Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id
					                                              LEFT JOIN Form_PharmacySurveys_LoadedAnswers AS DependencyShowHide ON Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Dependency_ShowHide_LoadedAnswersId = DependencyShowHide.LoadedAnswers_Id
                                              WHERE			vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = @PrintValue
					                                              AND Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id IS NULL
					                                              AND Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_IsActive = 1
					                                              AND Form_PharmacySurveys_LoadedSections.LoadedSections_IsActive = 1
					                                              AND Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_IsActive = 1
                                              UNION
                                              SELECT		Facility_FacilityDisplayName , 
					                                              LoadedSurveys_Name , 
					                                              LoadedSurveys_FY ,
					                                              CreatedSurveys_Name , 
					                                              Unit_Name , 
					                                              CreatedSurveys_Designation ,
					                                              LoadedSections_Name , 
					                                              LoadedQuestions_Name , 
					                                              LoadedAnswers_Name ,
					                                              CreatedSurveys_Comments , 
					                                              CreatedSurveys_Compliment
                                              FROM			vForm_PharmacySurveys_CreatedSurveys
					                                              LEFT JOIN Form_PharmacySurveys_CreatedSurveyAnswers ON vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id
					                                              LEFT JOIN Form_PharmacySurveys_LoadedAnswers ON Form_PharmacySurveys_CreatedSurveyAnswers.LoadedAnswers_Id = Form_PharmacySurveys_LoadedAnswers.LoadedAnswers_Id
					                                              LEFT JOIN Form_PharmacySurveys_LoadedQuestions ON Form_PharmacySurveys_LoadedAnswers.LoadedQuestions_Id = Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id
					                                              LEFT JOIN Form_PharmacySurveys_LoadedSections ON Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id = Form_PharmacySurveys_LoadedSections.LoadedSections_Id
                                              WHERE			vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = @PrintValue
                                              ORDER BY	LoadedSections_Name , 
					                                              LoadedQuestions_Name";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_PharmacySurveys.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_PharmacySurveys", SqlDataSource_Print1));
    }

    private void Form_VTE()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_VTE_Assesments WHERE VTE_Assesments_Id = @PrintValue ORDER BY vForm_VTE_Assesments.VTE_Assesments_Date DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_VTE.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_VTE", SqlDataSource_Print1));
    }

    private void Form_PharmacyClinicalMetrics_TherapeuticIntervention()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_TI_Id = @PrintValue ORDER BY PCM_TI_ReportNumber DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_PharmacyClinicalMetrics_TherapeuticIntervention.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_PharmacyClinicalMetrics_TherapeuticIntervention", SqlDataSource_Print1));
    }

    private void Form_PharmacyClinicalMetrics_TherapeuticIntervention_All()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_VisitInformation_Id IN ( SELECT PCM_VisitInformation_Id FROM vForm_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_TI_Id = @PrintValue ) ORDER BY PCM_TI_ReportNumber DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_PharmacyClinicalMetrics_TherapeuticIntervention_All.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_PharmacyClinicalMetrics_TherapeuticIntervention", SqlDataSource_Print1));
    }

    private void Form_PharmacyClinicalMetrics_PharmacistTime()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_PharmacyClinicalMetrics_PharmacistTime WHERE PCM_PT_Id = @PrintValue ORDER BY PCM_PT_ReportNumber DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_PharmacyClinicalMetrics_PharmacistTime.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_PharmacyClinicalMetrics_PharmacistTime", SqlDataSource_Print1));
    }

    private void Form_PharmacyClinicalMetrics_PharmacistTime_All()
    {
      SqlDataSource SqlDataSource_Print1;
      using (SqlDataSource_Print1 = new SqlDataSource())
      {
        SqlDataSource_Print1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Print1.SelectCommand = "SELECT * FROM vForm_PharmacyClinicalMetrics_PharmacistTime WHERE PCM_Intervention_Id IN ( SELECT PCM_Intervention_Id FROM vForm_PharmacyClinicalMetrics_PharmacistTime WHERE PCM_PT_Id = @PrintValue ) ORDER BY PCM_PT_ReportNumber DESC";
        SqlDataSource_Print1.SelectParameters.Add("PrintValue", Request.QueryString["PrintValue"]);
      }

      ReportViewer_Print.LocalReport.ReportPath = "App_Print\\Form_PharmacyClinicalMetrics_PharmacistTime_All.rdlc";
      ReportViewer_Print.LocalReport.DataSources.Clear();
      ReportViewer_Print.LocalReport.DataSources.Add(new ReportDataSource("Form_PharmacyClinicalMetrics_PharmacistTime", SqlDataSource_Print1));
    }
  }
}
