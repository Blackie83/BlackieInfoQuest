using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_PharmacyClinicalMetrics : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateTherapeuticInterventionClicked = false;
    private bool Button_EditPrintTherapeuticInterventionClicked = false;
    private bool Button_EditEmailTherapeuticInterventionClicked = false;

    private bool Button_EditUpdatePharmacistTimeClicked = false;
    private bool Button_EditPrintPharmacistTimeClicked = false;
    private bool Button_EditEmailPharmacistTimeClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_PharmacyClinicalMetrics, this.GetType(), "UpdateProgress_Start", "Validation_Search();ShowHide_Search();Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_Date.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_Date.Attributes.Add("OnInput", "Validation_Search();");
          DropDownList_Intervention.Attributes.Add("OnChange", "Validation_Search();ShowHide_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          Label_InterventionIcon.Text = GetListItemName("6232").ListItemName;

          HideTable();

          PageTitle();

          if (string.IsNullOrEmpty(Request.QueryString["PCMInterventionId"]))
          {
            SqlDataSource_Facility.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];

            if (string.IsNullOrEmpty(TextBox_Date.Text))
            {
              TextBox_Date.Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
            }
          }
          else
          {
            SqlDataSource_Facility.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
            SqlDataSource_Facility.SelectParameters["TableFROM"].DefaultValue = "Form_PharmacyClinicalMetrics_Intervention";
            SqlDataSource_Facility.SelectParameters["TableWHERE"].DefaultValue = "PCM_Intervention_Id = " + Request.QueryString["PCMInterventionId"] + " ";

            SqlDataSource_Intervention.SelectParameters["TableSELECT"].DefaultValue = "PCM_Intervention_Intervention_List";
            SqlDataSource_Intervention.SelectParameters["TableFROM"].DefaultValue = "Form_PharmacyClinicalMetrics_Intervention";
            SqlDataSource_Intervention.SelectParameters["TableWHERE"].DefaultValue = "PCM_Intervention_Id = " + Request.QueryString["PCMInterventionId"] + " ";

            SetSearchValues();

            FromDataBase_Intervention FromDataBase_Intervention_Current = GetIntervention();
            string PCMInterventionInterventionList = FromDataBase_Intervention_Current.PCMInterventionInterventionList;

            if (PCMInterventionInterventionList == "6217")
            {
              DivTherapeuticInterventionPatientInfo.Visible = true;
              TableTherapeuticInterventionPatientInfo.Visible = true;
              DivTherapeuticInterventionList.Visible = true;
              TableTherapeuticInterventionList.Visible = true;
              DivTherapeuticIntervention.Visible = true;
              TableTherapeuticIntervention.Visible = true;

              TableTherapeuticInterventionPatientInfoVisible();

              SetCurrentTherapeuticInterventionVisibility();

              TableCurrentTherapeuticInterventionVisible();
            }
            else if (PCMInterventionInterventionList == "6218")
            {
              DivPharmacistTimeList.Visible = true;
              TablePharmacistTimeList.Visible = true;
              DivPharmacistTime.Visible = true;
              TablePharmacistTime.Visible = true;

              if (Request.QueryString["PCMPTId"] != null)
              {
                SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
                SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters["TableSELECT"].DefaultValue = "PCM_PT_Unit";
                SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters["TableFROM"].DefaultValue = "vForm_PharmacyClinicalMetrics_PharmacistTime";
                SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters["TableWHERE"].DefaultValue = "PCM_PT_Id = " + Request.QueryString["PCMPTId"] + " ";
              }

              SetCurrentPharmacistTimeVisibility();

              TableCurrentPharmacistTimeVisible();
            }
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
        if (string.IsNullOrEmpty(Request.QueryString["PCMInterventionId"]))
        {
          SQLStringSecurity = @"SELECT DISTINCT SecurityUser_UserName 
                                FROM vAdministration_SecurityAccess_Active 
                                WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('52'))";
        }
        else
        {
          SQLStringSecurity = @"SELECT  DISTINCT SecurityUser_UserName 
                                FROM    vAdministration_SecurityAccess_Active 
                                WHERE   (SecurityUser_UserName = @SecurityUser_UserName) 
                                        AND (Form_Id IN ('52')) 
                                        AND (Facility_Id IN (
                                          SELECT Facility_Id 
                                          FROM Form_PharmacyClinicalMetrics_Intervention 
                                          WHERE PCM_Intervention_Id = @PCM_Intervention_Id
                                        ) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@PCM_Intervention_Id", Request.QueryString["PCMInterventionId"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("52");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_PharmacyClinicalMetrics.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Pharmacy Clinical Metrics", "29");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Facility.SelectParameters.Clear();
      SqlDataSource_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Intervention.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Intervention.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Intervention.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Intervention.SelectParameters.Clear();
      SqlDataSource_Intervention.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_Intervention.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "216");
      SqlDataSource_Intervention.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Intervention.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Intervention.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Intervention.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSourceSetup_TherapeuticIntervention();
      SqlDataSourceSetup_PharmacistTime();
    }

    private void SqlDataSourceSetup_TherapeuticIntervention()
    {
      SqlDataSourceSetup_TherapeuticIntervention_Insert();
      SqlDataSourceSetup_TherapeuticIntervention_Edit();

      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertCommand = "INSERT INTO Form_PharmacyClinicalMetrics_TherapeuticIntervention ( PCM_Intervention_Id , PCM_TI_ReportNumber , PCM_TI_InterventionBy , PCM_TI_Unit , PCM_TI_Doctor , PCM_TI_DoctorOther , PCM_TI_Time , PCM_TI_Indication_NoIndication , PCM_TI_Indication_NoIndication_IR_List , PCM_TI_Indication_NoIndication_CS_List , PCM_TI_Indication_NoIndication_Comment , PCM_TI_Indication_Duplication , PCM_TI_Indication_Duplication_IR_List , PCM_TI_Indication_Duplication_CS_List , PCM_TI_Indication_Duplication_Comment , PCM_TI_Indication_Untreated , PCM_TI_Indication_Untreated_IR_List , PCM_TI_Indication_Untreated_CS_List , PCM_TI_Indication_Untreated_Comment , PCM_TI_Dose_Dose , PCM_TI_Dose_Dose_List , PCM_TI_Dose_Dose_IR_List , PCM_TI_Dose_Dose_CS_List , PCM_TI_Dose_Dose_Comment , PCM_TI_Dose_Interval , PCM_TI_Dose_Interval_List , PCM_TI_Dose_Interval_IR_List , PCM_TI_Dose_Interval_CS_List , PCM_TI_Dose_Interval_Comment , PCM_TI_Efficacy_Change , PCM_TI_Efficacy_Change_IR_List , PCM_TI_Efficacy_Change_CS_List , PCM_TI_Efficacy_Change_Comment , PCM_TI_Safety_Allergic , PCM_TI_Safety_Allergic_IR_List , PCM_TI_Safety_Allergic_CS_List , PCM_TI_Safety_Allergic_Comment , PCM_TI_Safety_Unwanted , PCM_TI_Safety_Unwanted_IR_List , PCM_TI_Safety_Unwanted_CS_List , PCM_TI_Safety_Unwanted_Comment , PCM_TI_Safety_DrugDrug , PCM_TI_Safety_DrugDrug_IR_List , PCM_TI_Safety_DrugDrug_CS_List , PCM_TI_Safety_DrugDrug_Comment , PCM_TI_Safety_DrugDiluent , PCM_TI_Safety_DrugDiluent_IR_List , PCM_TI_Safety_DrugDiluent_CS_List , PCM_TI_Safety_DrugDiluent_Comment , PCM_TI_Safety_DrugLab , PCM_TI_Safety_DrugLab_IR_List , PCM_TI_Safety_DrugLab_CS_List , PCM_TI_Safety_DrugLab_Comment , PCM_TI_Safety_DrugDisease , PCM_TI_Safety_DrugDisease_IR_List , PCM_TI_Safety_DrugDisease_CS_List , PCM_TI_Safety_DrugDisease_Comment , PCM_TI_MedicationError_Missed , PCM_TI_MedicationError_Missed_Comment , PCM_TI_MedicationError_IncorrectDrug , PCM_TI_MedicationError_IncorrectDrug_Comment , PCM_TI_MedicationError_Incorrect , PCM_TI_MedicationError_Incorrect_Comment , PCM_TI_MedicationError_Prescribed , PCM_TI_MedicationError_Prescribed_Comment , PCM_TI_MedicationError_Administered , PCM_TI_MedicationError_Administered_Comment , PCM_TI_MedicationError_Medication , PCM_TI_MedicationError_Medication_Comment , PCM_TI_Cost_Generic , PCM_TI_Cost_Generic_Comment , PCM_TI_Cost_Substitution , PCM_TI_Cost_Substitution_Comment , PCM_TI_Cost_Decrease , PCM_TI_Cost_Decrease_Comment , PCM_TI_Cost_Increase , PCM_TI_Cost_Increase_Comment , PCM_TI_CreatedDate , PCM_TI_CreatedBy , PCM_TI_ModifiedDate , PCM_TI_ModifiedBy , PCM_TI_History , PCM_TI_IsActive ) VALUES ( @PCM_Intervention_Id , @PCM_TI_ReportNumber , @PCM_TI_InterventionBy , @PCM_TI_Unit , @PCM_TI_Doctor , @PCM_TI_DoctorOther , @PCM_TI_Time , @PCM_TI_Indication_NoIndication , @PCM_TI_Indication_NoIndication_IR_List , @PCM_TI_Indication_NoIndication_CS_List , @PCM_TI_Indication_NoIndication_Comment , @PCM_TI_Indication_Duplication , @PCM_TI_Indication_Duplication_IR_List , @PCM_TI_Indication_Duplication_CS_List , @PCM_TI_Indication_Duplication_Comment , @PCM_TI_Indication_Untreated , @PCM_TI_Indication_Untreated_IR_List , @PCM_TI_Indication_Untreated_CS_List , @PCM_TI_Indication_Untreated_Comment , @PCM_TI_Dose_Dose , @PCM_TI_Dose_Dose_List , @PCM_TI_Dose_Dose_IR_List , @PCM_TI_Dose_Dose_CS_List , @PCM_TI_Dose_Dose_Comment , @PCM_TI_Dose_Interval , @PCM_TI_Dose_Interval_List , @PCM_TI_Dose_Interval_IR_List , @PCM_TI_Dose_Interval_CS_List , @PCM_TI_Dose_Interval_Comment , @PCM_TI_Efficacy_Change , @PCM_TI_Efficacy_Change_IR_List , @PCM_TI_Efficacy_Change_CS_List , @PCM_TI_Efficacy_Change_Comment , @PCM_TI_Safety_Allergic , @PCM_TI_Safety_Allergic_IR_List , @PCM_TI_Safety_Allergic_CS_List , @PCM_TI_Safety_Allergic_Comment , @PCM_TI_Safety_Unwanted , @PCM_TI_Safety_Unwanted_IR_List , @PCM_TI_Safety_Unwanted_CS_List , @PCM_TI_Safety_Unwanted_Comment , @PCM_TI_Safety_DrugDrug , @PCM_TI_Safety_DrugDrug_IR_List , @PCM_TI_Safety_DrugDrug_CS_List , @PCM_TI_Safety_DrugDrug_Comment , @PCM_TI_Safety_DrugDiluent , @PCM_TI_Safety_DrugDiluent_IR_List , @PCM_TI_Safety_DrugDiluent_CS_List , @PCM_TI_Safety_DrugDiluent_Comment , @PCM_TI_Safety_DrugLab , @PCM_TI_Safety_DrugLab_IR_List , @PCM_TI_Safety_DrugLab_CS_List , @PCM_TI_Safety_DrugLab_Comment , @PCM_TI_Safety_DrugDisease , @PCM_TI_Safety_DrugDisease_IR_List , @PCM_TI_Safety_DrugDisease_CS_List , @PCM_TI_Safety_DrugDisease_Comment , @PCM_TI_MedicationError_Missed , @PCM_TI_MedicationError_Missed_Comment , @PCM_TI_MedicationError_IncorrectDrug , @PCM_TI_MedicationError_IncorrectDrug_Comment , @PCM_TI_MedicationError_Incorrect , @PCM_TI_MedicationError_Incorrect_Comment , @PCM_TI_MedicationError_Prescribed , @PCM_TI_MedicationError_Prescribed_Comment , @PCM_TI_MedicationError_Administered , @PCM_TI_MedicationError_Administered_Comment , @PCM_TI_MedicationError_Medication , @PCM_TI_MedicationError_Medication_Comment , @PCM_TI_Cost_Generic , @PCM_TI_Cost_Generic_Comment , @PCM_TI_Cost_Substitution , @PCM_TI_Cost_Substitution_Comment , @PCM_TI_Cost_Decrease , @PCM_TI_Cost_Decrease_Comment , @PCM_TI_Cost_Increase , @PCM_TI_Cost_Increase_Comment , @PCM_TI_CreatedDate , @PCM_TI_CreatedBy , @PCM_TI_ModifiedDate , @PCM_TI_ModifiedBy , @PCM_TI_History , @PCM_TI_IsActive ); SELECT @PCM_TI_Id = SCOPE_IDENTITY()";
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.SelectCommand = "SELECT * FROM Form_PharmacyClinicalMetrics_TherapeuticIntervention WHERE (PCM_TI_Id = @PCM_TI_Id)";
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateCommand = "UPDATE Form_PharmacyClinicalMetrics_TherapeuticIntervention SET PCM_TI_InterventionBy = @PCM_TI_InterventionBy , PCM_TI_Unit = @PCM_TI_Unit , PCM_TI_Doctor = @PCM_TI_Doctor , PCM_TI_DoctorOther = @PCM_TI_DoctorOther , PCM_TI_Time = @PCM_TI_Time , PCM_TI_Indication_NoIndication = @PCM_TI_Indication_NoIndication , PCM_TI_Indication_NoIndication_IR_List = @PCM_TI_Indication_NoIndication_IR_List , PCM_TI_Indication_NoIndication_CS_List = @PCM_TI_Indication_NoIndication_CS_List , PCM_TI_Indication_NoIndication_Comment = @PCM_TI_Indication_NoIndication_Comment , PCM_TI_Indication_Duplication = @PCM_TI_Indication_Duplication , PCM_TI_Indication_Duplication_IR_List = @PCM_TI_Indication_Duplication_IR_List , PCM_TI_Indication_Duplication_CS_List = @PCM_TI_Indication_Duplication_CS_List , PCM_TI_Indication_Duplication_Comment = @PCM_TI_Indication_Duplication_Comment , PCM_TI_Indication_Untreated = @PCM_TI_Indication_Untreated , PCM_TI_Indication_Untreated_IR_List = @PCM_TI_Indication_Untreated_IR_List , PCM_TI_Indication_Untreated_CS_List = @PCM_TI_Indication_Untreated_CS_List , PCM_TI_Indication_Untreated_Comment = @PCM_TI_Indication_Untreated_Comment , PCM_TI_Dose_Dose = @PCM_TI_Dose_Dose , PCM_TI_Dose_Dose_List = @PCM_TI_Dose_Dose_List , PCM_TI_Dose_Dose_IR_List = @PCM_TI_Dose_Dose_IR_List , PCM_TI_Dose_Dose_CS_List = @PCM_TI_Dose_Dose_CS_List , PCM_TI_Dose_Dose_Comment = @PCM_TI_Dose_Dose_Comment , PCM_TI_Dose_Interval = @PCM_TI_Dose_Interval , PCM_TI_Dose_Interval_List = @PCM_TI_Dose_Interval_List , PCM_TI_Dose_Interval_IR_List = @PCM_TI_Dose_Interval_IR_List , PCM_TI_Dose_Interval_CS_List = @PCM_TI_Dose_Interval_CS_List , PCM_TI_Dose_Interval_Comment = @PCM_TI_Dose_Interval_Comment , PCM_TI_Efficacy_Change = @PCM_TI_Efficacy_Change , PCM_TI_Efficacy_Change_IR_List = @PCM_TI_Efficacy_Change_IR_List , PCM_TI_Efficacy_Change_CS_List = @PCM_TI_Efficacy_Change_CS_List , PCM_TI_Efficacy_Change_Comment = @PCM_TI_Efficacy_Change_Comment , PCM_TI_Safety_Allergic = @PCM_TI_Safety_Allergic , PCM_TI_Safety_Allergic_IR_List = @PCM_TI_Safety_Allergic_IR_List , PCM_TI_Safety_Allergic_CS_List = @PCM_TI_Safety_Allergic_CS_List , PCM_TI_Safety_Allergic_Comment = @PCM_TI_Safety_Allergic_Comment , PCM_TI_Safety_Unwanted = @PCM_TI_Safety_Unwanted , PCM_TI_Safety_Unwanted_IR_List = @PCM_TI_Safety_Unwanted_IR_List , PCM_TI_Safety_Unwanted_CS_List = @PCM_TI_Safety_Unwanted_CS_List , PCM_TI_Safety_Unwanted_Comment = @PCM_TI_Safety_Unwanted_Comment , PCM_TI_Safety_DrugDrug = @PCM_TI_Safety_DrugDrug , PCM_TI_Safety_DrugDrug_IR_List = @PCM_TI_Safety_DrugDrug_IR_List , PCM_TI_Safety_DrugDrug_CS_List = @PCM_TI_Safety_DrugDrug_CS_List , PCM_TI_Safety_DrugDrug_Comment = @PCM_TI_Safety_DrugDrug_Comment , PCM_TI_Safety_DrugDiluent = @PCM_TI_Safety_DrugDiluent , PCM_TI_Safety_DrugDiluent_IR_List = @PCM_TI_Safety_DrugDiluent_IR_List , PCM_TI_Safety_DrugDiluent_CS_List = @PCM_TI_Safety_DrugDiluent_CS_List , PCM_TI_Safety_DrugDiluent_Comment = @PCM_TI_Safety_DrugDiluent_Comment , PCM_TI_Safety_DrugLab = @PCM_TI_Safety_DrugLab , PCM_TI_Safety_DrugLab_IR_List = @PCM_TI_Safety_DrugLab_IR_List , PCM_TI_Safety_DrugLab_CS_List = @PCM_TI_Safety_DrugLab_CS_List , PCM_TI_Safety_DrugLab_Comment = @PCM_TI_Safety_DrugLab_Comment , PCM_TI_Safety_DrugDisease = @PCM_TI_Safety_DrugDisease , PCM_TI_Safety_DrugDisease_IR_List = @PCM_TI_Safety_DrugDisease_IR_List , PCM_TI_Safety_DrugDisease_CS_List = @PCM_TI_Safety_DrugDisease_CS_List , PCM_TI_Safety_DrugDisease_Comment = @PCM_TI_Safety_DrugDisease_Comment , PCM_TI_MedicationError_Missed = @PCM_TI_MedicationError_Missed , PCM_TI_MedicationError_Missed_Comment = @PCM_TI_MedicationError_Missed_Comment , PCM_TI_MedicationError_IncorrectDrug = @PCM_TI_MedicationError_IncorrectDrug , PCM_TI_MedicationError_IncorrectDrug_Comment = @PCM_TI_MedicationError_IncorrectDrug_Comment , PCM_TI_MedicationError_Incorrect = @PCM_TI_MedicationError_Incorrect , PCM_TI_MedicationError_Incorrect_Comment = @PCM_TI_MedicationError_Incorrect_Comment , PCM_TI_MedicationError_Prescribed = @PCM_TI_MedicationError_Prescribed , PCM_TI_MedicationError_Prescribed_Comment = @PCM_TI_MedicationError_Prescribed_Comment , PCM_TI_MedicationError_Administered = @PCM_TI_MedicationError_Administered , PCM_TI_MedicationError_Administered_Comment = @PCM_TI_MedicationError_Administered_Comment , PCM_TI_MedicationError_Medication = @PCM_TI_MedicationError_Medication , PCM_TI_MedicationError_Medication_Comment = @PCM_TI_MedicationError_Medication_Comment , PCM_TI_Cost_Generic = @PCM_TI_Cost_Generic , PCM_TI_Cost_Generic_Comment = @PCM_TI_Cost_Generic_Comment , PCM_TI_Cost_Substitution = @PCM_TI_Cost_Substitution , PCM_TI_Cost_Substitution_Comment = @PCM_TI_Cost_Substitution_Comment , PCM_TI_Cost_Decrease = @PCM_TI_Cost_Decrease , PCM_TI_Cost_Decrease_Comment = @PCM_TI_Cost_Decrease_Comment , PCM_TI_Cost_Increase = @PCM_TI_Cost_Increase , PCM_TI_Cost_Increase_Comment = @PCM_TI_Cost_Increase_Comment , PCM_TI_ModifiedDate = @PCM_TI_ModifiedDate , PCM_TI_ModifiedBy = @PCM_TI_ModifiedBy , PCM_TI_History = @PCM_TI_History , PCM_TI_IsActive = @PCM_TI_IsActive WHERE PCM_TI_Id = @PCM_TI_Id";
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_Intervention_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_ReportNumber", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_InterventionBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Unit", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Doctor", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_DoctorOther", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_NoIndication", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_NoIndication_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_NoIndication_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_NoIndication_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_Duplication", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_Duplication_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_Duplication_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_Duplication_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_Untreated", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_Untreated_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_Untreated_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Indication_Untreated_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Dose", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Dose_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Dose_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Dose_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Dose_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Interval", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Interval_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Interval_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Interval_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Dose_Interval_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Efficacy_Change", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Efficacy_Change_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Efficacy_Change_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Efficacy_Change_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_Allergic", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_Allergic_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_Allergic_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_Allergic_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_Unwanted", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_Unwanted_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_Unwanted_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_Unwanted_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDrug", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDrug_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDrug_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDrug_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDiluent", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDiluent_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDiluent_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDiluent_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugLab", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugLab_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugLab_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugLab_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDisease", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDisease_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDisease_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Safety_DrugDisease_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Missed", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Missed_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_IncorrectDrug", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_IncorrectDrug_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Incorrect", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Incorrect_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Prescribed", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Prescribed_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Administered", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Administered_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Medication", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_MedicationError_Medication_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Cost_Generic", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Cost_Generic_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Cost_Substitution", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Cost_Substitution_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Cost_Decrease", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Cost_Decrease_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Cost_Increase", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_Cost_Increase_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_CreatedBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_History", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters.Add("PCM_TI_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.SelectParameters.Add("PCM_TI_Id", TypeCode.Int32, Request.QueryString["PCMTIId"]);
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_InterventionBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Unit", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Doctor", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_DoctorOther", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_NoIndication", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_NoIndication_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_NoIndication_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_NoIndication_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_Duplication", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_Duplication_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_Duplication_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_Duplication_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_Untreated", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_Untreated_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_Untreated_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Indication_Untreated_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Dose", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Dose_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Dose_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Dose_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Dose_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Interval", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Interval_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Interval_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Interval_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Dose_Interval_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Efficacy_Change", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Efficacy_Change_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Efficacy_Change_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Efficacy_Change_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_Allergic", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_Allergic_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_Allergic_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_Allergic_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_Unwanted", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_Unwanted_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_Unwanted_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_Unwanted_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDrug", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDrug_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDrug_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDrug_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDiluent", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDiluent_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDiluent_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDiluent_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugLab", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugLab_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugLab_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugLab_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDisease", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDisease_IR_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDisease_CS_List", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Safety_DrugDisease_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Missed", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Missed_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_IncorrectDrug", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_IncorrectDrug_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Incorrect", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Incorrect_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Prescribed", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Prescribed_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Administered", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Administered_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Medication", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_MedicationError_Medication_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Cost_Generic", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Cost_Generic_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Cost_Substitution", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Cost_Substitution_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Cost_Decrease", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Cost_Decrease_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Cost_Increase", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Cost_Increase_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_History", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.UpdateParameters.Add("PCM_TI_Id", TypeCode.Int32, "");

      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List.SelectCommand = "spForm_Get_PharmacyClinicalMetrics_TherapeuticIntervention_List";
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List.CancelSelectOnNullParameter = false;
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List.SelectParameters.Add("PCM_Intervention_Id", TypeCode.String, Request.QueryString["PCMInterventionId"]);
    }

    private void SqlDataSourceSetup_TherapeuticIntervention_Insert()
    {
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationNoIndicationCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationDuplicationCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertIndicationUntreatedCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "219");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseDoseCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "220");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertDoseIntervalCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertEfficacyChangeCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyAllergicCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
      
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyUnwantedCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDrugCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiluentCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugLabCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_InsertSafetyDrugDiseaseCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
    }

    private void SqlDataSourceSetup_TherapeuticIntervention_Edit()
    {
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Indication_NoIndication_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Indication_NoIndication_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationNoIndicationCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Indication_Duplication_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Indication_Duplication_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationDuplicationCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Indication_Untreated_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Indication_Untreated_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditIndicationUntreatedCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "219");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Dose_Dose_List");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Dose_Dose_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Dose_Dose_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseDoseCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "220");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Dose_Interval_List");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Dose_Interval_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Dose_Interval_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditDoseIntervalCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Efficacy_Change_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Efficacy_Change_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditEfficacyChangeCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_Allergic_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_Allergic_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyAllergicCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_Unwanted_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_Unwanted_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyUnwantedCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_DrugDrug_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_DrugDrug_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDrugCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_DrugDiluent_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_DrugDiluent_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiluentCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_DrugLab_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_DrugLab_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugLabCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "217");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_DrugDisease_IR_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseIRList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");

      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "218");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.SelectParameters.Add("TableSELECT", TypeCode.String, "PCM_TI_Safety_DrugDisease_CS_List");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacyClinicalMetrics_TherapeuticIntervention");
      SqlDataSource_PharmacyClinicalMetrics_EditSafetyDrugDiseaseCSList.SelectParameters.Add("TableWHERE", TypeCode.String, "PCM_TI_Id = " + Request.QueryString["PCMTIId"] + " ");
    }

    private void SqlDataSourceSetup_PharmacistTime()
    {
      FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
      string FacilityId = FromDataBase_FacilityId_Current.FacilityId;

      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.SelectParameters.Add("Facility_Id", TypeCode.String, FacilityId);
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_InsertUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters.Add("Form_Id", TypeCode.String, "52");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters.Add("Facility_Id", TypeCode.String, FacilityId);
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_EditUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertCommand = "INSERT INTO Form_PharmacyClinicalMetrics_PharmacistTime ( PCM_Intervention_Id , PCM_PT_ReportNumber , PCM_PT_InterventionBy , PCM_PT_Unit , PCM_PT_Patient_File , PCM_PT_Patient_LabResults , PCM_PT_Patient_Prescription , PCM_PT_Patient_TotalTime , PCM_PT_Patient_Amount , PCM_PT_Patient_Comments , PCM_PT_Medication , PCM_PT_Medication_Time , PCM_PT_Medication_Comment , PCM_PT_Research , PCM_PT_Research_Time , PCM_PT_Research_Comment , PCM_PT_Rounds , PCM_PT_Rounds_Time , PCM_PT_Rounds_Comment , PCM_PT_Counselling , PCM_PT_Counselling_Time , PCM_PT_Counselling_Comment , PCM_PT_Training , PCM_PT_Training_Time , PCM_PT_Training_Comment , PCM_PT_Reporting , PCM_PT_Reporting_Time , PCM_PT_Reporting_Comment , PCM_PT_Calculations , PCM_PT_Calculations_Time , PCM_PT_Calculations_Comment , PCM_PT_AdviceDoctor , PCM_PT_AdviceDoctor_Time , PCM_PT_AdviceDoctor_Comment , PCM_PT_AdviceNurse , PCM_PT_AdviceNurse_Time , PCM_PT_AdviceNurse_Comment , PCM_PT_MedicalHistory , PCM_PT_MedicalHistory_Time , PCM_PT_MedicalHistory_Comment , PCM_PT_Statistics , PCM_PT_Statistics_Time , PCM_PT_Statistics_Comment , PCM_PT_CreatedDate , PCM_PT_CreatedBy , PCM_PT_ModifiedDate , PCM_PT_ModifiedBy ,	PCM_PT_History , PCM_PT_IsActive ) VALUES ( @PCM_Intervention_Id , @PCM_PT_ReportNumber , @PCM_PT_InterventionBy , @PCM_PT_Unit , @PCM_PT_Patient_File , @PCM_PT_Patient_LabResults , @PCM_PT_Patient_Prescription , @PCM_PT_Patient_TotalTime , @PCM_PT_Patient_Amount , @PCM_PT_Patient_Comments , @PCM_PT_Medication , @PCM_PT_Medication_Time , @PCM_PT_Medication_Comment , @PCM_PT_Research , @PCM_PT_Research_Time , @PCM_PT_Research_Comment , @PCM_PT_Rounds , @PCM_PT_Rounds_Time , @PCM_PT_Rounds_Comment , @PCM_PT_Counselling , @PCM_PT_Counselling_Time , @PCM_PT_Counselling_Comment , @PCM_PT_Training , @PCM_PT_Training_Time , @PCM_PT_Training_Comment , @PCM_PT_Reporting , @PCM_PT_Reporting_Time , @PCM_PT_Reporting_Comment , @PCM_PT_Calculations , @PCM_PT_Calculations_Time , @PCM_PT_Calculations_Comment , @PCM_PT_AdviceDoctor , @PCM_PT_AdviceDoctor_Time , @PCM_PT_AdviceDoctor_Comment , @PCM_PT_AdviceNurse , @PCM_PT_AdviceNurse_Time , @PCM_PT_AdviceNurse_Comment , @PCM_PT_MedicalHistory , @PCM_PT_MedicalHistory_Time , @PCM_PT_MedicalHistory_Comment , @PCM_PT_Statistics , @PCM_PT_Statistics_Time , @PCM_PT_Statistics_Comment , @PCM_PT_CreatedDate , @PCM_PT_CreatedBy , @PCM_PT_ModifiedDate , @PCM_PT_ModifiedBy ,	@PCM_PT_History , @PCM_PT_IsActive ); SELECT @PCM_PT_Id = SCOPE_IDENTITY()";
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.SelectCommand = "SELECT * FROM Form_PharmacyClinicalMetrics_PharmacistTime WHERE (PCM_PT_Id = @PCM_PT_Id)";
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateCommand = "UPDATE Form_PharmacyClinicalMetrics_PharmacistTime SET PCM_PT_InterventionBy = @PCM_PT_InterventionBy , PCM_PT_Unit = @PCM_PT_Unit , PCM_PT_Patient_File = @PCM_PT_Patient_File , PCM_PT_Patient_LabResults = @PCM_PT_Patient_LabResults , PCM_PT_Patient_Prescription = @PCM_PT_Patient_Prescription , PCM_PT_Patient_TotalTime = @PCM_PT_Patient_TotalTime , PCM_PT_Patient_Amount = @PCM_PT_Patient_Amount , PCM_PT_Patient_Comments = @PCM_PT_Patient_Comments , PCM_PT_Medication = @PCM_PT_Medication , PCM_PT_Medication_Time = @PCM_PT_Medication_Time , PCM_PT_Medication_Comment = @PCM_PT_Medication_Comment , PCM_PT_Research = @PCM_PT_Research , PCM_PT_Research_Time = @PCM_PT_Research_Time , PCM_PT_Research_Comment = @PCM_PT_Research_Comment , PCM_PT_Rounds = @PCM_PT_Rounds , PCM_PT_Rounds_Time = @PCM_PT_Rounds_Time , PCM_PT_Rounds_Comment = @PCM_PT_Rounds_Comment , PCM_PT_Counselling = @PCM_PT_Counselling , PCM_PT_Counselling_Time = @PCM_PT_Counselling_Time , PCM_PT_Counselling_Comment = @PCM_PT_Counselling_Comment , PCM_PT_Training = @PCM_PT_Training , PCM_PT_Training_Time = @PCM_PT_Training_Time , PCM_PT_Training_Comment = @PCM_PT_Training_Comment , PCM_PT_Reporting = @PCM_PT_Reporting , PCM_PT_Reporting_Time = @PCM_PT_Reporting_Time , PCM_PT_Reporting_Comment = @PCM_PT_Reporting_Comment , PCM_PT_Calculations = @PCM_PT_Calculations , PCM_PT_Calculations_Time = @PCM_PT_Calculations_Time , PCM_PT_Calculations_Comment = @PCM_PT_Calculations_Comment , PCM_PT_AdviceDoctor = @PCM_PT_AdviceDoctor , PCM_PT_AdviceDoctor_Time = @PCM_PT_AdviceDoctor_Time , PCM_PT_AdviceDoctor_Comment = @PCM_PT_AdviceDoctor_Comment , PCM_PT_AdviceNurse = @PCM_PT_AdviceNurse , PCM_PT_AdviceNurse_Time = @PCM_PT_AdviceNurse_Time , PCM_PT_AdviceNurse_Comment = @PCM_PT_AdviceNurse_Comment , PCM_PT_MedicalHistory = @PCM_PT_MedicalHistory , PCM_PT_MedicalHistory_Time = @PCM_PT_MedicalHistory_Time , PCM_PT_MedicalHistory_Comment = @PCM_PT_MedicalHistory_Comment , PCM_PT_Statistics = @PCM_PT_Statistics , PCM_PT_Statistics_Time = @PCM_PT_Statistics_Time , PCM_PT_Statistics_Comment = @PCM_PT_Statistics_Comment , PCM_PT_ModifiedDate = @PCM_PT_ModifiedDate , PCM_PT_ModifiedBy = @PCM_PT_ModifiedBy ,	PCM_PT_History = @PCM_PT_History , PCM_PT_IsActive = @PCM_PT_IsActive WHERE PCM_PT_Id = @PCM_PT_Id";
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_PT_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_Intervention_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_ReportNumber", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_InterventionBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Unit", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Patient_File", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Patient_LabResults", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Patient_Prescription", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Patient_TotalTime", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Patient_Amount", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Patient_Comments", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Medication", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Medication_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Medication_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Research", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Research_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Research_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Rounds", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Rounds_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Rounds_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Counselling", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Counselling_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Counselling_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Training", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Training_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Training_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Reporting", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Reporting_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Reporting_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Calculations", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Calculations_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Calculations_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_AdviceDoctor", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_AdviceDoctor_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_AdviceDoctor_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_AdviceNurse", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_AdviceNurse_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_AdviceNurse_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_MedicalHistory", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_MedicalHistory_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_MedicalHistory_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Statistics", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Statistics_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_Statistics_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_CreatedBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_History", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_PT_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters.Add("PCM_PT_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.SelectParameters.Add("PCM_PT_Id", TypeCode.Int32, Request.QueryString["PCMPTId"]);
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_InterventionBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Unit", TypeCode.Int32, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Patient_File", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Patient_LabResults", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Patient_Prescription", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Patient_TotalTime", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Patient_Amount", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Patient_Comments", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Medication", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Medication_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Medication_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Research", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Research_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Research_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Rounds", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Rounds_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Rounds_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Counselling", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Counselling_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Counselling_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Training", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Training_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Training_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Reporting", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Reporting_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Reporting_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Calculations", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Calculations_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Calculations_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_AdviceDoctor", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_AdviceDoctor_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_AdviceDoctor_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_AdviceNurse", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_AdviceNurse_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_AdviceNurse_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_MedicalHistory", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_MedicalHistory_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_MedicalHistory_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Statistics", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Statistics_Time", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Statistics_Comment", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_History", TypeCode.String, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.UpdateParameters.Add("PCM_PT_Id", TypeCode.Int32, "");

      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List.SelectCommand = "spForm_Get_PharmacyClinicalMetrics_PharmacistTime_List";
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List.CancelSelectOnNullParameter = false;
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List.SelectParameters.Clear();
      SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List.SelectParameters.Add("PCM_Intervention_Id", TypeCode.String, Request.QueryString["PCMInterventionId"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("52")).ToString(), CultureInfo.CurrentCulture);
      Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("52").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
      Label_TherapeuticInterventionPatientInfoHeading.Text = Convert.ToString("Therapeutic Intervention Patient Information", CultureInfo.CurrentCulture);
      Label_TherapeuticInterventionListHeading.Text = Convert.ToString("Therapeutic Intervention List", CultureInfo.CurrentCulture);
      Label_TherapeuticInterventionHeading.Text = Convert.ToString("Therapeutic Intervention", CultureInfo.CurrentCulture);
      Label_PharmacistTimeListHeading.Text = Convert.ToString("Pharmacist Time List", CultureInfo.CurrentCulture);
      Label_PharmacistTimeHeading.Text = Convert.ToString("Pharmacist Time", CultureInfo.CurrentCulture);
    }

    private void HideTable()
    {
      DivTherapeuticInterventionPatientInfo.Visible = false;
      TableTherapeuticInterventionPatientInfo.Visible = false;
      DivTherapeuticInterventionList.Visible = false;
      TableTherapeuticInterventionList.Visible = false;
      DivTherapeuticIntervention.Visible = false;
      TableTherapeuticIntervention.Visible = false;
      DivPharmacistTimeList.Visible = false;
      TablePharmacistTimeList.Visible = false;
      DivPharmacistTime.Visible = false;
      TablePharmacistTime.Visible = false;
    }

    private void SetSearchValues()
    {
      string FacilityId = "";
      string PCMInterventionDate = "";
      string PCMInterventionInterventionList = "";
      string PCMInterventionVisitNumber = "";
      string SQLStringIntervention = "SELECT Facility_Id , PCM_Intervention_Date , PCM_Intervention_Intervention_List , PCM_Intervention_VisitNumber FROM Form_PharmacyClinicalMetrics_Intervention WHERE PCM_Intervention_Id = @PCM_Intervention_Id";
      using (SqlCommand SqlCommand_Intervention = new SqlCommand(SQLStringIntervention))
      {
        SqlCommand_Intervention.Parameters.AddWithValue("@PCM_Intervention_Id", Request.QueryString["PCMInterventionId"]);
        DataTable DataTable_Intervention;
        using (DataTable_Intervention = new DataTable())
        {
          DataTable_Intervention.Locale = CultureInfo.CurrentCulture;
          DataTable_Intervention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Intervention).Copy();
          if (DataTable_Intervention.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Intervention.Rows)
            {
              FacilityId = DataRow_Row["Facility_Id"].ToString();
              PCMInterventionDate = DataRow_Row["PCM_Intervention_Date"].ToString();
              DateTime ParsePCMInterventionDate = DateTime.Parse(PCMInterventionDate.ToString(), CultureInfo.CurrentCulture);
              PCMInterventionDate = ParsePCMInterventionDate.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
              PCMInterventionInterventionList = DataRow_Row["PCM_Intervention_Intervention_List"].ToString();
              PCMInterventionVisitNumber = DataRow_Row["PCM_Intervention_VisitNumber"].ToString();
            }
          }
        }
      }

      DropDownList_Facility.SelectedValue = FacilityId;
      TextBox_Date.Text = PCMInterventionDate;
      DropDownList_Intervention.SelectedValue = PCMInterventionInterventionList;
      TextBox_PatientVisitNumber.Text = PCMInterventionVisitNumber;

      FacilityId = "";
      PCMInterventionDate = "";
      PCMInterventionInterventionList = "";
      PCMInterventionVisitNumber = "";
    }

    private void TableTherapeuticInterventionPatientInfoVisible()
    {
      string FacilityFacilityDisplayName = "";
      string PCMInterventionVisitNumber = "";
      string PatientInformationName = "";
      string PatientInformationSurname = "";
      string PCMVisitInformationPatientAge = "";
      string PCMVisitInformationDateOfAdmission = "";
      string PCMVisitInformationDateOfDischarge = "";
      string SQLStringVisitInfo = @"SELECT Facility_FacilityDisplayName , PCM_Intervention_VisitNumber , PatientInformation_Name , PatientInformation_Surname , PCM_VisitInformation_PatientAge , PCM_VisitInformation_DateOfAdmission , PCM_VisitInformation_DateOfDischarge FROM vForm_PharmacyClinicalMetrics_Intervention WHERE PCM_Intervention_Id = @PCM_Intervention_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@PCM_Intervention_Id", Request.QueryString["PCMInterventionId"]);
        DataTable DataTable_VisitInfo;
        using (DataTable_VisitInfo = new DataTable())
        {
          DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
          if (DataTable_VisitInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
            {
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              PCMInterventionVisitNumber = DataRow_Row["PCM_Intervention_VisitNumber"].ToString();
              PatientInformationName = DataRow_Row["PatientInformation_Name"].ToString();
              PatientInformationSurname = DataRow_Row["PatientInformation_Surname"].ToString();
              PCMVisitInformationPatientAge = DataRow_Row["PCM_VisitInformation_PatientAge"].ToString();
              PCMVisitInformationDateOfAdmission = DataRow_Row["PCM_VisitInformation_DateOfAdmission"].ToString();
              PCMVisitInformationDateOfDischarge = DataRow_Row["PCM_VisitInformation_DateOfDischarge"].ToString();
            }
          }
        }
      }

      Label_PIFacility.Text = FacilityFacilityDisplayName;
      Label_PIVisitNumber.Text = PCMInterventionVisitNumber;
      Label_PIName.Text = PatientInformationSurname + Convert.ToString(", ", CultureInfo.CurrentCulture) + PatientInformationName;
      Label_PIAge.Text = PCMVisitInformationPatientAge;
      Label_PIDateAdmission.Text = PCMVisitInformationDateOfAdmission;
      Label_PIDateDischarge.Text = PCMVisitInformationDateOfDischarge;

      FacilityFacilityDisplayName = "";
      PCMInterventionVisitNumber = "";
      PatientInformationName = "";
      PatientInformationSurname = "";
      PCMVisitInformationPatientAge = "";
      PCMVisitInformationDateOfAdmission = "";
      PCMVisitInformationDateOfDischarge = "";
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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('52')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_PharmacyClinicalMetrics_Intervention WHERE PCM_Intervention_Id = @PCM_Intervention_Id) OR (SecurityRole_Rank = 1))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@PCM_Intervention_Id", Request.QueryString["PCMInterventionId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '206'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '207'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '208'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '209'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_Intervention
    {
      public string PCMInterventionInterventionList { get; set; }
    }

    private FromDataBase_Intervention GetIntervention()
    {
      FromDataBase_Intervention FromDataBase_Intervention_New = new FromDataBase_Intervention();

      string SQLStringIntervention = "SELECT PCM_Intervention_Intervention_List FROM Form_PharmacyClinicalMetrics_Intervention WHERE PCM_Intervention_Id = @PCM_Intervention_Id";
      using (SqlCommand SqlCommand_Intervention = new SqlCommand(SQLStringIntervention))
      {
        SqlCommand_Intervention.Parameters.AddWithValue("@PCM_Intervention_Id", Request.QueryString["PCMInterventionId"]);
        DataTable DataTable_Intervention;
        using (DataTable_Intervention = new DataTable())
        {
          DataTable_Intervention.Locale = CultureInfo.CurrentCulture;
          DataTable_Intervention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Intervention).Copy();
          if (DataTable_Intervention.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Intervention.Rows)
            {
              FromDataBase_Intervention_New.PCMInterventionInterventionList = DataRow_Row["PCM_Intervention_Intervention_List"].ToString();
            }
          }
        }
      }

      return FromDataBase_Intervention_New;
    }

    private class FromDataBase_FormViewUpdate
    {
      public string ViewUpdate { get; set; }
    }

    private FromDataBase_FormViewUpdate GetFormViewUpdate()
    {
      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_New = new FromDataBase_FormViewUpdate();

      string SQLStringFormViewUpdate = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 52),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0, PCM_Intervention_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 52),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,PCM_Intervention_Date)+1,0))) < GETDATE() THEN 'No' END AS ViewUpdate FROM Form_PharmacyClinicalMetrics_Intervention WHERE PCM_Intervention_Id = @PCM_Intervention_Id";
      using (SqlCommand SqlCommand_FormViewUpdate = new SqlCommand(SQLStringFormViewUpdate))
      {
        SqlCommand_FormViewUpdate.Parameters.AddWithValue("@PCM_Intervention_Id", Request.QueryString["PCMInterventionId"]);
        DataTable DataTable_FormViewUpdate;
        using (DataTable_FormViewUpdate = new DataTable())
        {
          DataTable_FormViewUpdate.Locale = CultureInfo.CurrentCulture;
          DataTable_FormViewUpdate = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormViewUpdate).Copy();
          if (DataTable_FormViewUpdate.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormViewUpdate.Rows)
            {
              FromDataBase_FormViewUpdate_New.ViewUpdate = DataRow_Row["ViewUpdate"].ToString();
            }
          }
        }
      }

      return FromDataBase_FormViewUpdate_New;
    }

    private class FromDataBase_FacilityId
    {
      public string FacilityId { get; set; }
      public string PCMInterventionVisitNumber { get; set; }
    }

    private FromDataBase_FacilityId GetFacilityId()
    {
      FromDataBase_FacilityId FromDataBase_FacilityId_New = new FromDataBase_FacilityId();

      string SQLStringFacility = "SELECT Facility_Id , PCM_Intervention_VisitNumber FROM Form_PharmacyClinicalMetrics_Intervention WHERE PCM_Intervention_Id = @PCM_Intervention_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@PCM_Intervention_Id", Request.QueryString["PCMInterventionId"]);
        DataTable DataTable_Facility;
        using (DataTable_Facility = new DataTable())
        {
          DataTable_Facility.Locale = CultureInfo.CurrentCulture;
          DataTable_Facility = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
          if (DataTable_Facility.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Facility.Rows)
            {
              FromDataBase_FacilityId_New.FacilityId = DataRow_Row["Facility_Id"].ToString();
              FromDataBase_FacilityId_New.PCMInterventionVisitNumber = DataRow_Row["PCM_Intervention_VisitNumber"].ToString();
            }
          }
        }
      }

      return FromDataBase_FacilityId_New;
    }

    private class FromDataBase_ListItemName
    {
      public string ListItemName { get; set; }
    }

    private static FromDataBase_ListItemName GetListItemName(string listItemId)
    {
      FromDataBase_ListItemName FromDataBase_ListItemName_New = new FromDataBase_ListItemName();

      string SQLStringListItemName = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Id = @ListItem_Id";
      using (SqlCommand SqlCommand_ListItemName = new SqlCommand(SQLStringListItemName))
      {
        SqlCommand_ListItemName.Parameters.AddWithValue("@ListItem_Id", listItemId);
        DataTable DataTable_ListItemName;
        using (DataTable_ListItemName = new DataTable())
        {
          DataTable_ListItemName.Locale = CultureInfo.CurrentCulture;
          DataTable_ListItemName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItemName).Copy();
          if (DataTable_ListItemName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ListItemName.Rows)
            {
              FromDataBase_ListItemName_New.ListItemName = DataRow_Row["ListItem_Name"].ToString();
            }
          }
        }
      }

      return FromDataBase_ListItemName_New;
    }


    //--START-- --Search--//
    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Form", "Form_PharmacyClinicalMetrics.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        if (!string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text))
        {
          SearchIntervention_TherapeuticIntervention();
        }
        else
        {
          SearchIntervention_PharmacistTime();
        }
      }
      else
      {
        Label_InvalidSearchMessage.Text = Label_InvalidSearchMessageText;
      }
    }

    protected string SearchValidation()
    {
      string InvalidSearch = "No";
      string InvalidSearchMessage = "";

      if (InvalidSearch == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue))
        {
          InvalidSearch = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_Date.Text))
        {
          InvalidSearch = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_Intervention.SelectedValue))
        {
          InvalidSearch = "Yes";
        }

        if (DropDownList_Intervention.SelectedValue == "6217")
        {
          if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text))
          {
            InvalidSearch = "Yes";
          }
        }
      }

      if (InvalidSearch == "Yes")
      {
        InvalidSearchMessage = "All red fields are required";
      }

      if (InvalidSearch == "No" && string.IsNullOrEmpty(InvalidSearchMessage))
      {
        string DateToValidateDate = TextBox_Date.Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidSearchMessage = InvalidSearchMessage + "Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(TextBox_Date.Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidSearchMessage = InvalidSearchMessage + "No future dates allowed<br />";
          }
        }
      }

      return InvalidSearchMessage;
    }

    protected void SearchIntervention_TherapeuticIntervention()
    {
      string PCMVisitInformationId = "";
      string PatientInformationId = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PatientInformationId(DropDownList_Facility.SelectedValue, TextBox_PatientVisitNumber.Text);
      Int32 FindError = PatientInformationId.IndexOf("Error", StringComparison.CurrentCulture);

      if (FindError > -1)
      {
        Label_InvalidSearchMessage.Text = PatientInformationId;
        HideTable();
      }
      else
      {
        DataTable DataTable_VisitData;
        using (DataTable_VisitData = new DataTable())
        {
          DataTable_VisitData.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(DropDownList_Facility.SelectedValue, TextBox_PatientVisitNumber.Text).Copy();
          if (DataTable_VisitData.Columns.Count == 1)
          {
            string Error = "";
            foreach (DataRow DataRow_Row in DataTable_VisitData.Rows)
            {
              Error = DataRow_Row["Error"].ToString();
            }

            Label_InvalidSearchMessage.Text = Error;
            HideTable();
            Error = "";
          }
          else if (DataTable_VisitData.Columns.Count != 1)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitData.Rows)
            {
              string DateOfAdmission = DataRow_Row["DateOfAdmission"].ToString();
              string DateOfDischarge = DataRow_Row["DateOfDischarge"].ToString();
              string PatientAge = DataRow_Row["PatientAge"].ToString();

              string SQLStringVisitInfo = "SELECT PCM_VisitInformation_Id FROM vForm_PharmacyClinicalMetrics_Intervention WHERE Facility_Id = @Facility_Id AND PCM_Intervention_VisitNumber = @PCM_Intervention_VisitNumber";
              using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
              {
                SqlCommand_VisitInfo.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
                SqlCommand_VisitInfo.Parameters.AddWithValue("@PCM_Intervention_VisitNumber", TextBox_PatientVisitNumber.Text);
                DataTable DataTable_VisitInfo;
                using (DataTable_VisitInfo = new DataTable())
                {
                  DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
                  DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
                  if (DataTable_VisitInfo.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row1 in DataTable_VisitInfo.Rows)
                    {
                      PCMVisitInformationId = DataRow_Row1["PCM_VisitInformation_Id"].ToString();
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(PCMVisitInformationId))
              {
                string SQLStringInsertVisitInformation = "INSERT INTO Form_PharmacyClinicalMetrics_VisitInformation ( PatientInformation_Id , PCM_VisitInformation_PatientAge , PCM_VisitInformation_DateOfAdmission , PCM_VisitInformation_DateOfDischarge ) VALUES ( @PatientInformation_Id , @PCM_VisitInformation_PatientAge , @PCM_VisitInformation_DateOfAdmission , @PCM_VisitInformation_DateOfDischarge ); SELECT SCOPE_IDENTITY()";
                using (SqlCommand SqlCommand_InsertVisitInformation = new SqlCommand(SQLStringInsertVisitInformation))
                {
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PCM_VisitInformation_PatientAge", PatientAge);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PCM_VisitInformation_DateOfAdmission", DateOfAdmission);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PCM_VisitInformation_DateOfDischarge", DateOfDischarge);
                  PCMVisitInformationId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertVisitInformation);
                }
              }
              else
              {
                string SQLStringUpdateVisitInformation = "UPDATE Form_PharmacyClinicalMetrics_VisitInformation SET PatientInformation_Id = @PatientInformation_Id , PCM_VisitInformation_PatientAge  = @PCM_VisitInformation_PatientAge , PCM_VisitInformation_DateOfAdmission  = @PCM_VisitInformation_DateOfAdmission , PCM_VisitInformation_DateOfDischarge  = @PCM_VisitInformation_DateOfDischarge WHERE PCM_VisitInformation_Id = @PCM_VisitInformation_Id";
                using (SqlCommand SqlCommand_UpdateVisitInformation = new SqlCommand(SQLStringUpdateVisitInformation))
                {
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PCM_VisitInformation_PatientAge", PatientAge);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PCM_VisitInformation_DateOfAdmission", DateOfAdmission);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PCM_VisitInformation_DateOfDischarge", DateOfDischarge);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PCM_VisitInformation_Id", PCMVisitInformationId);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateVisitInformation);
                }
              }
            }
          }
        }


        string PCMInterventionId = "";
        string SQLStringIntervention = @" SELECT  PCM_Intervention_Id 
                                            FROM    Form_PharmacyClinicalMetrics_Intervention 
                                            WHERE   Facility_Id = @Facility_Id 
                                                    AND PCM_Intervention_Date = @PCM_Intervention_Date 
                                                    AND PCM_Intervention_Intervention_List = @PCM_Intervention_Intervention_List 
                                                    AND PCM_Intervention_VisitNumber = @PCM_Intervention_VisitNumber 
                                                    AND PCM_VisitInformation_Id = @PCM_VisitInformation_Id";
        using (SqlCommand SqlCommand_Intervention = new SqlCommand(SQLStringIntervention))
        {
          SqlCommand_Intervention.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
          SqlCommand_Intervention.Parameters.AddWithValue("@PCM_Intervention_Date", TextBox_Date.Text);
          SqlCommand_Intervention.Parameters.AddWithValue("@PCM_Intervention_Intervention_List", DropDownList_Intervention.SelectedValue);
          SqlCommand_Intervention.Parameters.AddWithValue("@PCM_Intervention_VisitNumber", TextBox_PatientVisitNumber.Text);
          SqlCommand_Intervention.Parameters.AddWithValue("@PCM_VisitInformation_Id", PCMVisitInformationId);
          DataTable DataTable_Intervention;
          using (DataTable_Intervention = new DataTable())
          {
            DataTable_Intervention.Locale = CultureInfo.CurrentCulture;
            DataTable_Intervention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Intervention).Copy();
            if (DataTable_Intervention.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Intervention.Rows)
              {
                PCMInterventionId = DataRow_Row["PCM_Intervention_Id"].ToString();
              }
            }
          }
        }

        if (string.IsNullOrEmpty(PCMInterventionId))
        {
          string SQLStringInsertIntervention = "INSERT INTO Form_PharmacyClinicalMetrics_Intervention ( Facility_Id , PCM_Intervention_Date , PCM_Intervention_Intervention_List , PCM_Intervention_VisitNumber , PCM_VisitInformation_Id , PCM_Intervention_Archived ) VALUES ( @Facility_Id , @PCM_Intervention_Date , @PCM_Intervention_Intervention_List , @PCM_Intervention_VisitNumber , @PCM_VisitInformation_Id , @PCM_Intervention_Archived ); SELECT SCOPE_IDENTITY()";
          using (SqlCommand SqlCommand_InsertIntervention = new SqlCommand(SQLStringInsertIntervention))
          {
            SqlCommand_InsertIntervention.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
            SqlCommand_InsertIntervention.Parameters.AddWithValue("@PCM_Intervention_Date", TextBox_Date.Text);
            SqlCommand_InsertIntervention.Parameters.AddWithValue("@PCM_Intervention_Intervention_List", DropDownList_Intervention.SelectedValue);
            SqlCommand_InsertIntervention.Parameters.AddWithValue("@PCM_Intervention_VisitNumber", TextBox_PatientVisitNumber.Text);
            SqlCommand_InsertIntervention.Parameters.AddWithValue("@PCM_VisitInformation_Id", PCMVisitInformationId);
            SqlCommand_InsertIntervention.Parameters.AddWithValue("@PCM_Intervention_Archived", 0);
            PCMInterventionId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertIntervention);
          }
        }

        Response.Redirect("Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + PCMInterventionId, false);
      }
    }

    protected void SearchIntervention_PharmacistTime()
    {
      string PCMInterventionId = "";
      string SQLStringIntervention = @" SELECT  PCM_Intervention_Id 
                                            FROM    Form_PharmacyClinicalMetrics_Intervention 
                                            WHERE   Facility_Id = @Facility_Id 
                                                    AND PCM_Intervention_Date = @PCM_Intervention_Date 
                                                    AND PCM_Intervention_Intervention_List = @PCM_Intervention_Intervention_List";
      using (SqlCommand SqlCommand_Intervention = new SqlCommand(SQLStringIntervention))
      {
        SqlCommand_Intervention.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
        SqlCommand_Intervention.Parameters.AddWithValue("@PCM_Intervention_Date", TextBox_Date.Text);
        SqlCommand_Intervention.Parameters.AddWithValue("@PCM_Intervention_Intervention_List", DropDownList_Intervention.SelectedValue);
        DataTable DataTable_Intervention;
        using (DataTable_Intervention = new DataTable())
        {
          DataTable_Intervention.Locale = CultureInfo.CurrentCulture;
          DataTable_Intervention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Intervention).Copy();
          if (DataTable_Intervention.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Intervention.Rows)
            {
              PCMInterventionId = DataRow_Row["PCM_Intervention_Id"].ToString();
            }
          }
        }
      }

      if (string.IsNullOrEmpty(PCMInterventionId))
      {
        string SQLStringInsertIntervention = "INSERT INTO Form_PharmacyClinicalMetrics_Intervention ( Facility_Id , PCM_Intervention_Date , PCM_Intervention_Intervention_List , PCM_Intervention_Archived ) VALUES ( @Facility_Id , @PCM_Intervention_Date , @PCM_Intervention_Intervention_List , @PCM_Intervention_Archived ); SELECT SCOPE_IDENTITY()";
        using (SqlCommand SqlCommand_InsertIntervention = new SqlCommand(SQLStringInsertIntervention))
        {
          SqlCommand_InsertIntervention.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
          SqlCommand_InsertIntervention.Parameters.AddWithValue("@PCM_Intervention_Date", TextBox_Date.Text);
          SqlCommand_InsertIntervention.Parameters.AddWithValue("@PCM_Intervention_Intervention_List", DropDownList_Intervention.SelectedValue);
          SqlCommand_InsertIntervention.Parameters.AddWithValue("@PCM_Intervention_Archived", 0);
          PCMInterventionId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertIntervention);
        }
      }

      Response.Redirect("Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + PCMInterventionId, false);
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_InterventionList"];
      string SearchField3 = Request.QueryString["Search_PatientVisitNumber"];
      string SearchField4 = Request.QueryString["Search_PatientName"];
      string SearchField5 = Request.QueryString["Search_ReportNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_FacilityId=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_InterventionList=" + Request.QueryString["Search_InterventionList"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_PatientVisitNumber=" + Request.QueryString["Search_PatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_PatientName=" + Request.QueryString["Search_PatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_ReportNumber=" + Request.QueryString["Search_ReportNumber"] + "&";
      }

      string FinalURL = "Form_PharmacyClinicalMetrics_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --TableTherapeuticIntervention--//
    protected void SetCurrentTherapeuticInterventionVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["PCMTIId"]))
      {
        SetCurrentTherapeuticInterventionVisibility_Insert();
      }
      else
      {
        SetCurrentTherapeuticInterventionVisibility_Edit();
      }
    }

    protected void SetCurrentTherapeuticInterventionVisibility_Insert()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_Current = GetFormViewUpdate();
      string ViewUpdate = FromDataBase_FormViewUpdate_Current.ViewUpdate;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (ViewUpdate == "Yes")
        {
          FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ChangeMode(FormViewMode.Insert);
        }
        else
        {
          FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetCurrentTherapeuticInterventionVisibility_Edit()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_Current = GetFormViewUpdate();
      string ViewUpdate = FromDataBase_FormViewUpdate_Current.ViewUpdate;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ChangeMode(FormViewMode.Edit);
      }

      if (Security == "1" && (SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (ViewUpdate == "Yes")
        {
          FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentTherapeuticInterventionVisible()
    {
      if (FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.CurrentMode == FormViewMode.Insert)
      {
        ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_InsertSafetyAllergicURL")).NavigateUrl = GetListItemName("6237").ListItemName;
        ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_InsertSafetyUnwantedURL")).NavigateUrl = GetListItemName("6237").ListItemName;
        ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_InsertMedicationErrorURL_Alert")).NavigateUrl = GetListItemName("6238").ListItemName;
        ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_InsertMedicationErrorURL_Incident")).NavigateUrl = GetListItemName("6239").ListItemName;

        TableCurrentTherapeuticInterventionVisible_Insert();
      }

      if (FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.CurrentMode == FormViewMode.Edit)
      {
        ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_EditSafetyAllergicURL")).NavigateUrl = GetListItemName("6237").ListItemName;
        ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_EditSafetyUnwantedURL")).NavigateUrl = GetListItemName("6237").ListItemName;
        ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_EditMedicationErrorURL_Alert")).NavigateUrl = GetListItemName("6238").ListItemName;
        ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_EditMedicationErrorURL_Incident")).NavigateUrl = GetListItemName("6239").ListItemName;

        TableCurrentTherapeuticInterventionVisible_Edit();
      }

      if (FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["PCMTIId"]))
        {
          ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_ItemSafetyAllergicURL")).NavigateUrl = GetListItemName("6237").ListItemName;
          ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_ItemSafetyUnwantedURL")).NavigateUrl = GetListItemName("6237").ListItemName;
          ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_ItemMedicationErrorURL_Alert")).NavigateUrl = GetListItemName("6238").ListItemName;
          ((HyperLink)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("HyperLink_ItemMedicationErrorURL_Incident")).NavigateUrl = GetListItemName("6239").ListItemName;

          TableCurrentTherapeuticInterventionVisible_Item();
        }
      }
    }

    protected void TableCurrentTherapeuticInterventionVisible_Insert()
    {
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertIndicationNoIndicationCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertIndicationDuplicationCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertIndicationUntreatedCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertDoseDoseCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertDoseIntervalCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertEfficacyChangeCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertSafetyAllergicCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertSafetyUnwantedCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertSafetyDrugDrugCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertSafetyDrugDiluentCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertSafetyDrugLabCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertSafetyDrugDiseaseCSListIcon")).Text = GetListItemName("6233").ListItemName;


      FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
      string FacilityId = FromDataBase_FacilityId_Current.FacilityId;
      string PCMInterventionVisitNumber = FromDataBase_FacilityId_Current.PCMInterventionVisitNumber;

      DataTable DataTable_Unit;
      using (DataTable_Unit = new DataTable())
      {
        DataTable_Unit.Locale = CultureInfo.CurrentCulture;
        DataTable_Unit = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_AccommodationInformation(FacilityId, PCMInterventionVisitNumber);

        ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertUnit")).DataSource = DataTable_Unit.DefaultView.ToTable(true, "Ward");
        ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertUnit")).DataBind();
      }


      DataTable DataTable_Doctor;
      using (DataTable_Doctor = new DataTable())
      {
        DataTable_Doctor.Locale = CultureInfo.CurrentCulture;
        DataTable_Doctor = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(FacilityId, PCMInterventionVisitNumber);

        ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoctor")).DataSource = DataTable_Doctor;
        ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoctor")).DataBind();
      }


      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertInterventionBy")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertInterventionBy")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoctor")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertDoctorOther")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertDoctorOther")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertTime")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertIndicationNoIndication")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationNoIndicationIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationNoIndicationCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertIndicationNoIndicationComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertIndicationNoIndicationComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertIndicationDuplication")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationDuplicationIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationDuplicationCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertIndicationDuplicationComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertIndicationDuplicationComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertIndicationUntreated")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationUntreatedIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationUntreatedCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertIndicationUntreatedComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertIndicationUntreatedComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertDoseDose")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((RadioButtonList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("RadioButtonList_InsertDoseDoseList")).Attributes.Add("OnClick", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoseDoseIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoseDoseCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertDoseDoseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertDoseDoseComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertDoseInterval")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((RadioButtonList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("RadioButtonList_InsertDoseIntervalList")).Attributes.Add("OnClick", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoseIntervalIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoseIntervalCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertDoseIntervalComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertDoseIntervalComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertEfficacyChange")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertEfficacyChangeIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertEfficacyChangeCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertEfficacyChangeComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertEfficacyChangeComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyAllergic")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyAllergicIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyAllergicCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyAllergicComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyAllergicComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyUnwanted")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyUnwantedIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyUnwantedCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyUnwantedComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyUnwantedComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugDrug")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDrugIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDrugCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugDrugComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugDrugComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugDiluent")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDiluentIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDiluentCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugDiluentComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugDiluentComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugLab")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugLabIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugLabCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugLabComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugLabComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugDisease")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDiseaseIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDiseaseCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugDiseaseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugDiseaseComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorMissed")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorMissedComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorMissedComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorIncorrectDrug")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorIncorrectDrugComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorIncorrectDrugComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorIncorrect")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorIncorrectComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorIncorrectComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorPrescribed")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorPrescribedComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorPrescribedComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorAdministered")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorAdministeredComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorAdministeredComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorMedication")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorMedicationComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorMedicationComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostGeneric")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostGenericComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostGenericComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostSubstitution")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostSubstitutionComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostSubstitutionComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostDecrease")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostDecreaseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostDecreaseComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostIncrease")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostIncreaseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostIncreaseComment")).Attributes.Add("OnInput", "Validation_Form();");
    }

    protected void TableCurrentTherapeuticInterventionVisible_Edit()
    {
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditIndicationNoIndicationCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditIndicationDuplicationCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditIndicationUntreatedCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditDoseDoseCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditDoseIntervalCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditEfficacyChangeCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditSafetyAllergicCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditSafetyUnwantedCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditSafetyDrugDrugCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditSafetyDrugDiluentCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditSafetyDrugLabCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditSafetyDrugDiseaseCSListIcon")).Text = GetListItemName("6233").ListItemName;


      FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
      string FacilityId = FromDataBase_FacilityId_Current.FacilityId;
      string PCMInterventionVisitNumber = FromDataBase_FacilityId_Current.PCMInterventionVisitNumber;

      string SQLStringUnit = "SELECT Facility_FacilityCode AS FacilityCode , PCM_Intervention_VisitNumber AS VisitNumber , '' AS SequenceNumber , PCM_TI_Unit AS Ward , '' AS Room , '' AS Bed , CAST('' AS DateTime) AS Date FROM vForm_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_TI_Id = @PCM_TI_Id AND PCM_TI_Unit IS NOT NULL";
      using (SqlCommand SqlCommand_Unit = new SqlCommand(SQLStringUnit))
      {
        SqlCommand_Unit.Parameters.AddWithValue("@PCM_TI_Id", Request.QueryString["PCMTIId"]);
        DataTable DataTable_Unit;
        using (DataTable_Unit = new DataTable())
        {
          DataTable_Unit.Locale = CultureInfo.CurrentCulture;

          DataTable_Unit.Merge(InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Unit));
          DataTable_Unit.Merge(InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_AccommodationInformation(FacilityId, PCMInterventionVisitNumber));

          DataTable_Unit.DefaultView.Sort = "Ward ASC";

          ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditUnit")).DataSource = DataTable_Unit.DefaultView.ToTable(true, "Ward");
          ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditUnit")).DataBind();


          ListItem ListItem_Pharmacy = new ListItem();
          ListItem_Pharmacy.Text = Convert.ToString("Pharmacy", CultureInfo.CurrentCulture);
          ListItem_Pharmacy.Value = Convert.ToString("Pharmacy", CultureInfo.CurrentCulture);

          Int32 IndexOfPharmacy = ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditUnit")).Items.IndexOf(ListItem_Pharmacy);

          if (IndexOfPharmacy == -1)
          {
            ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditUnit")).Items.Insert(1, ListItem_Pharmacy);
          }
        }
      }


      string SQLStringDoctor = "SELECT Facility_FacilityCode AS FacilityCode , PCM_Intervention_VisitNumber AS VisitNumber , PCM_TI_Doctor AS Practitioner FROM vForm_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_TI_Id = @PCM_TI_Id AND PCM_TI_Doctor IS NOT NULL";
      using (SqlCommand SqlCommand_Doctor = new SqlCommand(SQLStringDoctor))
      {
        SqlCommand_Doctor.Parameters.AddWithValue("@PCM_TI_Id", Request.QueryString["PCMTIId"]);
        DataTable DataTable_Doctor;
        using (DataTable_Doctor = new DataTable())
        {
          DataTable_Doctor.Locale = CultureInfo.CurrentCulture;
          DataTable_Doctor.Merge(InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Doctor));
          DataTable_Doctor.Merge(InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(FacilityId, PCMInterventionVisitNumber));

          DataTable_Doctor.DefaultView.Sort = "Practitioner ASC";

          ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoctor")).DataSource = DataTable_Doctor.DefaultView.ToTable(true, "Practitioner");
          ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoctor")).DataBind();


          ListItem ListItem_Doctor = new ListItem();
          ListItem_Doctor.Text = Convert.ToString("Other", CultureInfo.CurrentCulture);
          ListItem_Doctor.Value = Convert.ToString("Other", CultureInfo.CurrentCulture);

          Int32 IndexOfDoctor = ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoctor")).Items.IndexOf(ListItem_Doctor);

          if (IndexOfDoctor == -1)
          {
            ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoctor")).Items.Insert(1, ListItem_Doctor);
          }
        }
      }


      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditInterventionBy")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditInterventionBy")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoctor")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditDoctorOther")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditDoctorOther")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditTime")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditIndicationNoIndication")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationNoIndicationIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationNoIndicationCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditIndicationNoIndicationComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditIndicationNoIndicationComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditIndicationDuplication")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationDuplicationIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationDuplicationCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditIndicationDuplicationComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditIndicationDuplicationComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditIndicationUntreated")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationUntreatedIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationUntreatedCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditIndicationUntreatedComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditIndicationUntreatedComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditDoseDose")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((RadioButtonList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("RadioButtonList_EditDoseDoseList")).Attributes.Add("OnClick", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoseDoseIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoseDoseCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditDoseDoseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditDoseDoseComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditDoseInterval")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((RadioButtonList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("RadioButtonList_EditDoseIntervalList")).Attributes.Add("OnClick", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoseIntervalIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoseIntervalCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditDoseIntervalComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditDoseIntervalComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditEfficacyChange")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditEfficacyChangeIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditEfficacyChangeCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditEfficacyChangeComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditEfficacyChangeComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyAllergic")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyAllergicIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyAllergicCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyAllergicComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyAllergicComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyUnwanted")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyUnwantedIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyUnwantedCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyUnwantedComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyUnwantedComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugDrug")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDrugIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDrugCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugDrugComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugDrugComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugDiluent")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDiluentIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDiluentCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugDiluentComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugDiluentComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugLab")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugLabIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugLabCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugLabComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugLabComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugDisease")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDiseaseIRList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDiseaseCSList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugDiseaseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugDiseaseComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorMissed")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorMissedComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorMissedComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorIncorrectDrug")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorIncorrectDrugComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorIncorrectDrugComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorIncorrect")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorIncorrectComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorIncorrectComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorPrescribed")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorPrescribedComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorPrescribedComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorAdministered")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorAdministeredComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorAdministeredComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorMedication")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorMedicationComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorMedicationComment")).Attributes.Add("OnInput", "Validation_Form();");

      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostGeneric")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostGenericComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostGenericComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostSubstitution")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostSubstitutionComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostSubstitutionComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostDecrease")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostDecreaseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostDecreaseComment")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostIncrease")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostIncreaseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostIncreaseComment")).Attributes.Add("OnInput", "Validation_Form();");
    }

    protected void TableCurrentTherapeuticInterventionVisible_Item()
    {
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemIndicationNoIndicationCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemIndicationDuplicationCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemIndicationUntreatedCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemDoseDoseCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemDoseIntervalCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemEfficacyChangeCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyAllergicCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyUnwantedCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugDrugCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugDiluentCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugLabCSListIcon")).Text = GetListItemName("6233").ListItemName;
      ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugDiseaseCSListIcon")).Text = GetListItemName("6233").ListItemName;
    }


    //--START-- --Insert--//
    protected void FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_TherapeuticIntervention();

        if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;
        }
        else
        {
          e.Cancel = true;
        }

        if (e.Cancel == true)
        {
          ToolkitScriptManager_PharmacyClinicalMetrics.SetFocus(UpdatePanel_PharmacyClinicalMetrics);
          ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
          string FacilityId = FromDataBase_FacilityId_Current.FacilityId;

          string PCM_TI_ReportNumber = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], FacilityId, "52");

          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_Intervention_Id"].DefaultValue = Request.QueryString["PCMInterventionId"];
          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_ReportNumber"].DefaultValue = PCM_TI_ReportNumber;
          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_History"].DefaultValue = "";
          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_IsActive"].DefaultValue = "true";

          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_Unit"].DefaultValue = ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertUnit")).SelectedValue;
          SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.InsertParameters["PCM_TI_Doctor"].DefaultValue = ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoctor")).SelectedValue;
        }
      }
    }

    protected string InsertValidation_TherapeuticIntervention()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertInterventionBy")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoctor")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoctor")).SelectedValue == "Other")
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertDoctorOther")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertTime")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertIndicationNoIndication")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationNoIndicationIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationNoIndicationCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertIndicationNoIndicationComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertIndicationDuplication")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationDuplicationIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationDuplicationCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertIndicationDuplicationComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertIndicationUntreated")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationUntreatedIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertIndicationUntreatedCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertIndicationUntreatedComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertDoseDose")).Checked == true)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("RadioButtonList_InsertDoseDoseList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoseDoseIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoseDoseCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertDoseDoseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertDoseInterval")).Checked == true)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("RadioButtonList_InsertDoseIntervalList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoseIntervalIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertDoseIntervalCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertDoseIntervalComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertEfficacyChange")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertEfficacyChangeIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertEfficacyChangeCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertEfficacyChangeComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyAllergic")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyAllergicIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyAllergicCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyAllergicComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyUnwanted")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyUnwantedIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyUnwantedCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyUnwantedComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugDrug")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDrugIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDrugCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugDrugComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugDiluent")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDiluentIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDiluentCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugDiluentComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugLab")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugLabIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugLabCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugLabComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugDisease")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDiseaseIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_InsertSafetyDrugDiseaseCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertSafetyDrugDiseaseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorMissed")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorMissedComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorIncorrectDrug")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorIncorrectDrugComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorIncorrect")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorIncorrectComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorPrescribed")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorPrescribedComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorAdministered")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorAdministeredComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorMedication")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertMedicationErrorMedicationComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }


        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostGeneric")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostGenericComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostSubstitution")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostSubstitutionComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostDecrease")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostDecreaseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostIncrease")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_InsertCostIncreaseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = InsertFieldValidation_TherapeuticIntervention(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string InsertFieldValidation_TherapeuticIntervention(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertIndicationNoIndication")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertIndicationDuplication")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertIndicationUntreated")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertDoseDose")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertDoseInterval")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertEfficacyChange")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyAllergic")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyUnwanted")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugDrug")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugDiluent")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugLab")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertSafetyDrugDisease")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorMissed")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorIncorrectDrug")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorIncorrect")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorPrescribed")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorAdministered")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertMedicationErrorMedication")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostGeneric")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostSubstitution")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostDecrease")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_InsertCostIncrease")).Checked == false)
      {
        InvalidFormMessage = InvalidFormMessage + "At least one of the Interventions needs to be selected<br />";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string PCM_TI_ReportNumber = e.Command.Parameters["@PCM_TI_ReportNumber"].Value.ToString();

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_PharmacyClinicalMetrics_TherapeuticIntervention&ReportNumber=" + PCM_TI_ReportNumber + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    protected void FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDPCMTIModifiedDate"] = e.OldValues["PCM_TI_ModifiedDate"];
        object OLDPCMTIModifiedDate = Session["OLDPCMTIModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDPCMTIModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_ComparePharmacyClinicalMetrics_TherapeuticIntervention = (DataView)SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ComparePharmacyClinicalMetrics_TherapeuticIntervention = DataView_ComparePharmacyClinicalMetrics_TherapeuticIntervention[0];
        Session["DBPCMTIModifiedDate"] = Convert.ToString(DataRowView_ComparePharmacyClinicalMetrics_TherapeuticIntervention["PCM_TI_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBPCMTIModifiedBy"] = Convert.ToString(DataRowView_ComparePharmacyClinicalMetrics_TherapeuticIntervention["PCM_TI_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBPCMTIModifiedDate = Session["DBPCMTIModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBPCMTIModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacyClinicalMetrics.SetFocus(UpdatePanel_PharmacyClinicalMetrics);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBPCMTIModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_TherapeuticIntervention();

          if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
          }
          else
          {
            e.Cancel = true;
          }

          if (e.Cancel == true)
          {
            ToolkitScriptManager_PharmacyClinicalMetrics.SetFocus(UpdatePanel_PharmacyClinicalMetrics);
            ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["PCM_TI_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["PCM_TI_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_PharmacyClinicalMetrics_TherapeuticIntervention", "PCM_TI_Id = " + Request.QueryString["PCMTIId"]);

            DataView DataView_PharmacyClinicalMetrics_TherapeuticIntervention = (DataView)SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_PharmacyClinicalMetrics_TherapeuticIntervention = DataView_PharmacyClinicalMetrics_TherapeuticIntervention[0];
            Session["PCMTIHistory"] = Convert.ToString(DataRowView_PharmacyClinicalMetrics_TherapeuticIntervention["PCM_TI_History"], CultureInfo.CurrentCulture);

            Session["PCMTIHistory"] = Session["History"].ToString() + Session["PCMTIHistory"].ToString();
            e.NewValues["PCM_TI_History"] = Session["PCMTIHistory"].ToString();

            Session["PCMTIHistory"] = "";
            Session["History"] = "";

            e.NewValues["PCM_TI_Unit"] = ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditUnit")).SelectedValue;
            e.NewValues["PCM_TI_Doctor"] = ((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoctor")).SelectedValue;
          }
        }

        Session["OLDPCMTIModifiedDate"] = "";
        Session["DBPCMTIModifiedDate"] = "";
        Session["DBPCMTIModifiedBy"] = "";
      }
    }

    protected string EditValidation_TherapeuticIntervention()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditInterventionBy")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoctor")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoctor")).SelectedValue == "Other")
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditDoctorOther")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditTime")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditIndicationNoIndication")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationNoIndicationIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationNoIndicationCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditIndicationNoIndicationComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditIndicationDuplication")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationDuplicationIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationDuplicationCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditIndicationDuplicationComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditIndicationUntreated")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationUntreatedIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditIndicationUntreatedCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditIndicationUntreatedComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditDoseDose")).Checked == true)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("RadioButtonList_EditDoseDoseList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoseDoseIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoseDoseCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditDoseDoseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditDoseInterval")).Checked == true)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("RadioButtonList_EditDoseIntervalList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoseIntervalIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoseIntervalCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditDoseIntervalComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditEfficacyChange")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditEfficacyChangeIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditEfficacyChangeCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditEfficacyChangeComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyAllergic")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyAllergicIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyAllergicCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyAllergicComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyUnwanted")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyUnwantedIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyUnwantedCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyUnwantedComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugDrug")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDrugIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDrugCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugDrugComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugDiluent")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDiluentIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDiluentCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugDiluentComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugLab")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugLabIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugLabCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugLabComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugDisease")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDiseaseIRList")).SelectedValue) || string.IsNullOrEmpty(((DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditSafetyDrugDiseaseCSList")).SelectedValue) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditSafetyDrugDiseaseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorMissed")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorMissedComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorIncorrectDrug")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorIncorrectDrugComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorIncorrect")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorIncorrectComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorPrescribed")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorPrescribedComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorAdministered")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorAdministeredComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorMedication")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditMedicationErrorMedicationComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }


        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostGeneric")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostGenericComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostSubstitution")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostSubstitutionComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostDecrease")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostDecreaseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostIncrease")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("TextBox_EditCostIncreaseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = EditFieldValidation_TherapeuticIntervention(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation_TherapeuticIntervention(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      if (((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditIndicationNoIndication")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditIndicationDuplication")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditIndicationUntreated")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditDoseDose")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditDoseInterval")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditEfficacyChange")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyAllergic")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyUnwanted")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugDrug")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugDiluent")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugLab")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditSafetyDrugDisease")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorMissed")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorIncorrectDrug")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorIncorrect")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorPrescribed")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorAdministered")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditMedicationErrorMedication")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostGeneric")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostSubstitution")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostDecrease")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("CheckBox_EditCostIncrease")).Checked == false)
      {
        InvalidFormMessage = InvalidFormMessage + "At least one of the Interventions needs to be selected<br />";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateTherapeuticInterventionClicked == true)
          {
            Button_EditUpdateTherapeuticInterventionClicked = false;

            if (!string.IsNullOrEmpty(Request.QueryString["PCMTIId"]))
            {
              Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + ""), false);
            }
          }

          if (Button_EditPrintTherapeuticInterventionClicked == true)
          {
            Button_EditPrintTherapeuticInterventionClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacyClinicalMetrics, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Print", "InfoQuest_Print.aspx?PrintPage=Form_PharmacyClinicalMetrics_TherapeuticIntervention&PrintValue=" + Request.QueryString["PCMTIId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacyClinicalMetrics, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailTherapeuticInterventionClicked == true)
          {
            Button_EditEmailTherapeuticInterventionClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacyClinicalMetrics, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Email", "InfoQuest_Email.aspx?EmailPage=Form_PharmacyClinicalMetrics_TherapeuticIntervention&EmailValue=" + Request.QueryString["PCMTIId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacyClinicalMetrics, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["PCMTIId"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + ""), false);
          }
        }
      }
    }

    protected void FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.CurrentMode == FormViewMode.Insert)
      {
        InsertDataBound_PharmacyClinicalMetrics_TherapeuticIntervention();
      }

      if (FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound_PharmacyClinicalMetrics_TherapeuticIntervention();
      }

      if (FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound_PharmacyClinicalMetrics_TherapeuticIntervention();
      }
    }

    protected void InsertDataBound_PharmacyClinicalMetrics_TherapeuticIntervention()
    {
      if (Request.QueryString["PCMTIId"] == null)
      {
        string PCMInterventionDate = "";
        string SQLStringTherapeuticIntervention = "SELECT CONVERT(NVARCHAR(MAX),PCM_Intervention_Date,111) AS PCM_Intervention_Date FROM vForm_PharmacyClinicalMetrics_Intervention WHERE PCM_Intervention_Id = @PCM_Intervention_Id";
        using (SqlCommand SqlCommand_TherapeuticIntervention = new SqlCommand(SQLStringTherapeuticIntervention))
        {
          SqlCommand_TherapeuticIntervention.Parameters.AddWithValue("@PCM_Intervention_Id", Request.QueryString["PCMInterventionId"]);
          DataTable DataTable_TherapeuticIntervention;
          using (DataTable_TherapeuticIntervention = new DataTable())
          {
            DataTable_TherapeuticIntervention.Locale = CultureInfo.CurrentCulture;
            DataTable_TherapeuticIntervention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TherapeuticIntervention).Copy();
            if (DataTable_TherapeuticIntervention.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_TherapeuticIntervention.Rows)
              {
                PCMInterventionDate = DataRow_Row["PCM_Intervention_Date"].ToString();
              }
            }
          }
        }

        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_InsertDate")).Text = PCMInterventionDate;

        PCMInterventionDate = "";
      }
    }

    protected void EditDataBound_PharmacyClinicalMetrics_TherapeuticIntervention()
    {
      if (Request.QueryString["PCMTIId"] != null)
      {
        string PCMInterventionDate = "";
        string SQLStringTherapeuticIntervention = "SELECT CONVERT(NVARCHAR(MAX),PCM_Intervention_Date,111) AS PCM_Intervention_Date FROM vForm_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_TI_Id = @PCM_TI_Id";
        using (SqlCommand SqlCommand_TherapeuticIntervention = new SqlCommand(SQLStringTherapeuticIntervention))
        {
          SqlCommand_TherapeuticIntervention.Parameters.AddWithValue("@PCM_TI_Id", Request.QueryString["PCMTIId"]);
          DataTable DataTable_TherapeuticIntervention;
          using (DataTable_TherapeuticIntervention = new DataTable())
          {
            DataTable_TherapeuticIntervention.Locale = CultureInfo.CurrentCulture;
            DataTable_TherapeuticIntervention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TherapeuticIntervention).Copy();
            if (DataTable_TherapeuticIntervention.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_TherapeuticIntervention.Rows)
              {
                PCMInterventionDate = DataRow_Row["PCM_Intervention_Date"].ToString();
              }
            }
          }
        }

        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_EditDate")).Text = PCMInterventionDate;

        PCMInterventionDate = "";


        DropDownList DropDownList_EditUnit = (DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditUnit");
        DataView DataView_Unit = (DataView)SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_Unit = DataView_Unit[0];
        DropDownList_EditUnit.SelectedValue = Convert.ToString(DataRowView_Unit["PCM_TI_Unit"], CultureInfo.CurrentCulture);


        DropDownList DropDownList_EditDoctor = (DropDownList)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("DropDownList_EditDoctor");
        DataView DataView_Doctor = (DataView)SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_Doctor = DataView_Doctor[0];
        DropDownList_EditDoctor.SelectedValue = Convert.ToString(DataRowView_Doctor["PCM_TI_Doctor"], CultureInfo.CurrentCulture);

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 52";
        using (SqlCommand SqlCommand_Email = new SqlCommand(SQLStringEmail))
        {
          DataTable DataTable_Email;
          using (DataTable_Email = new DataTable())
          {
            DataTable_Email.Locale = CultureInfo.CurrentCulture;
            DataTable_Email = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Email).Copy();
            if (DataTable_Email.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Email.Rows)
              {
                Email = DataRow_Row["Form_Email"].ToString();
                Print = DataRow_Row["Form_Print"].ToString();
              }
            }
          }
        }

        if (Print == "False")
        {
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_EditPrint_TherapeuticIntervention")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_EditPrint_TherapeuticIntervention")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_EditEmail_TherapeuticIntervention")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_EditEmail_TherapeuticIntervention")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound_PharmacyClinicalMetrics_TherapeuticIntervention()
    {
      if (Request.QueryString["PCMTIId"] != null)
      {
        string PCMInterventionDate = "";
        string PCMTIIndicationNoIndicationIRName = "";
        string PCMTIIndicationNoIndicationCSName = "";
        string PCMTIIndicationDuplicationIRName = "";
        string PCMTIIndicationDuplicationCSName = "";
        string PCMTIIndicationUntreatedIRName = "";
        string PCMTIIndicationUntreatedCSName = "";
        string PCMTIDoseDoseName = "";
        string PCMTIDoseDoseIRName = "";
        string PCMTIDoseDoseCSName = "";
        string PCMTIDoseIntervalName = "";
        string PCMTIDoseIntervalIRName = "";
        string PCMTIDoseIntervalCSName = "";
        string PCMTIEfficacyChangeIRName = "";
        string PCMTIEfficacyChangeCSName = "";
        string PCMTISafetyAllergicIRName = "";
        string PCMTISafetyAllergicCSName = "";
        string PCMTISafetyUnwantedIRName = "";
        string PCMTISafetyUnwantedCSName = "";
        string PCMTISafetyDrugDrugIRName = "";
        string PCMTISafetyDrugDrugCSName = "";
        string PCMTISafetyDrugDiluentIRName = "";
        string PCMTISafetyDrugDiluentCSName = "";
        string PCMTISafetyDrugLabIRName = "";
        string PCMTISafetyDrugLabCSName = "";
        string PCMTISafetyDrugDiseaseIRName = "";
        string PCMTISafetyDrugDiseaseCSName = "";
        string SQLStringTherapeuticIntervention = "SELECT CONVERT(NVARCHAR(MAX),PCM_Intervention_Date,111) AS PCM_Intervention_Date , PCM_TI_Indication_NoIndication_IR_Name , PCM_TI_Indication_NoIndication_CS_Name , PCM_TI_Indication_Duplication_IR_Name , PCM_TI_Indication_Duplication_CS_Name , PCM_TI_Indication_Untreated_IR_Name , PCM_TI_Indication_Untreated_CS_Name , PCM_TI_Dose_Dose_Name , PCM_TI_Dose_Dose_IR_Name , PCM_TI_Dose_Dose_CS_Name , PCM_TI_Dose_Interval_Name , PCM_TI_Dose_Interval_IR_Name , PCM_TI_Dose_Interval_CS_Name , PCM_TI_Efficacy_Change_IR_Name , PCM_TI_Efficacy_Change_CS_Name , PCM_TI_Safety_Allergic_IR_Name , PCM_TI_Safety_Allergic_CS_Name , PCM_TI_Safety_Unwanted_IR_Name , PCM_TI_Safety_Unwanted_CS_Name , PCM_TI_Safety_DrugDrug_IR_Name , PCM_TI_Safety_DrugDrug_CS_Name , PCM_TI_Safety_DrugDiluent_IR_Name , PCM_TI_Safety_DrugDiluent_CS_Name , PCM_TI_Safety_DrugLab_IR_Name , PCM_TI_Safety_DrugLab_CS_Name , PCM_TI_Safety_DrugDisease_IR_Name , PCM_TI_Safety_DrugDisease_CS_Name FROM vForm_PharmacyClinicalMetrics_TherapeuticIntervention WHERE PCM_TI_Id = @PCM_TI_Id";
        using (SqlCommand SqlCommand_TherapeuticIntervention = new SqlCommand(SQLStringTherapeuticIntervention))
        {
          SqlCommand_TherapeuticIntervention.Parameters.AddWithValue("@PCM_TI_Id", Request.QueryString["PCMTIId"]);
          DataTable DataTable_TherapeuticIntervention;
          using (DataTable_TherapeuticIntervention = new DataTable())
          {
            DataTable_TherapeuticIntervention.Locale = CultureInfo.CurrentCulture;
            DataTable_TherapeuticIntervention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TherapeuticIntervention).Copy();
            if (DataTable_TherapeuticIntervention.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_TherapeuticIntervention.Rows)
              {
                PCMInterventionDate = DataRow_Row["PCM_Intervention_Date"].ToString();
                PCMTIIndicationNoIndicationIRName = DataRow_Row["PCM_TI_Indication_NoIndication_IR_Name"].ToString();
                PCMTIIndicationNoIndicationCSName = DataRow_Row["PCM_TI_Indication_NoIndication_CS_Name"].ToString();
                PCMTIIndicationDuplicationIRName = DataRow_Row["PCM_TI_Indication_Duplication_IR_Name"].ToString();
                PCMTIIndicationDuplicationCSName = DataRow_Row["PCM_TI_Indication_Duplication_CS_Name"].ToString();
                PCMTIIndicationUntreatedIRName = DataRow_Row["PCM_TI_Indication_Untreated_IR_Name"].ToString();
                PCMTIIndicationUntreatedCSName = DataRow_Row["PCM_TI_Indication_Untreated_CS_Name"].ToString();
                PCMTIDoseDoseName = DataRow_Row["PCM_TI_Dose_Dose_Name"].ToString();
                PCMTIDoseDoseIRName = DataRow_Row["PCM_TI_Dose_Dose_IR_Name"].ToString();
                PCMTIDoseDoseCSName = DataRow_Row["PCM_TI_Dose_Dose_CS_Name"].ToString();
                PCMTIDoseIntervalName = DataRow_Row["PCM_TI_Dose_Interval_Name"].ToString();
                PCMTIDoseIntervalIRName = DataRow_Row["PCM_TI_Dose_Interval_IR_Name"].ToString();
                PCMTIDoseIntervalCSName = DataRow_Row["PCM_TI_Dose_Interval_CS_Name"].ToString();
                PCMTIEfficacyChangeIRName = DataRow_Row["PCM_TI_Efficacy_Change_IR_Name"].ToString();
                PCMTIEfficacyChangeCSName = DataRow_Row["PCM_TI_Efficacy_Change_CS_Name"].ToString();
                PCMTISafetyAllergicIRName = DataRow_Row["PCM_TI_Safety_Allergic_IR_Name"].ToString();
                PCMTISafetyAllergicCSName = DataRow_Row["PCM_TI_Safety_Allergic_CS_Name"].ToString();
                PCMTISafetyUnwantedIRName = DataRow_Row["PCM_TI_Safety_Unwanted_IR_Name"].ToString();
                PCMTISafetyUnwantedCSName = DataRow_Row["PCM_TI_Safety_Unwanted_CS_Name"].ToString();
                PCMTISafetyDrugDrugIRName = DataRow_Row["PCM_TI_Safety_DrugDrug_IR_Name"].ToString();
                PCMTISafetyDrugDrugCSName = DataRow_Row["PCM_TI_Safety_DrugDrug_CS_Name"].ToString();
                PCMTISafetyDrugDiluentIRName = DataRow_Row["PCM_TI_Safety_DrugDiluent_IR_Name"].ToString();
                PCMTISafetyDrugDiluentCSName = DataRow_Row["PCM_TI_Safety_DrugDiluent_CS_Name"].ToString();
                PCMTISafetyDrugLabIRName = DataRow_Row["PCM_TI_Safety_DrugLab_IR_Name"].ToString();
                PCMTISafetyDrugLabCSName = DataRow_Row["PCM_TI_Safety_DrugLab_CS_Name"].ToString();
                PCMTISafetyDrugDiseaseIRName = DataRow_Row["PCM_TI_Safety_DrugDisease_IR_Name"].ToString();
                PCMTISafetyDrugDiseaseCSName = DataRow_Row["PCM_TI_Safety_DrugDisease_CS_Name"].ToString();
              }
            }
          }
        }


        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemDate")).Text = PCMInterventionDate;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemIndicationNoIndicationIRList")).Text = PCMTIIndicationNoIndicationIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemIndicationNoIndicationCSList")).Text = PCMTIIndicationNoIndicationCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemIndicationDuplicationIRList")).Text = PCMTIIndicationDuplicationIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemIndicationDuplicationCSList")).Text = PCMTIIndicationDuplicationCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemIndicationUntreatedIRList")).Text = PCMTIIndicationUntreatedIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemIndicationUntreatedCSList")).Text = PCMTIIndicationUntreatedCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemDoseDoseList")).Text = PCMTIDoseDoseName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemDoseDoseIRList")).Text = PCMTIDoseDoseIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemDoseDoseCSList")).Text = PCMTIDoseDoseCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemDoseIntervalList")).Text = PCMTIDoseIntervalName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemDoseIntervalIRList")).Text = PCMTIDoseIntervalIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemDoseIntervalCSList")).Text = PCMTIDoseIntervalCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemEfficacyChangeIRList")).Text = PCMTIEfficacyChangeIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemEfficacyChangeCSList")).Text = PCMTIEfficacyChangeCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyAllergicIRList")).Text = PCMTISafetyAllergicIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyAllergicCSList")).Text = PCMTISafetyAllergicCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyUnwantedIRList")).Text = PCMTISafetyUnwantedIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyUnwantedCSList")).Text = PCMTISafetyUnwantedCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugDrugIRList")).Text = PCMTISafetyDrugDrugIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugDrugCSList")).Text = PCMTISafetyDrugDrugCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugDiluentIRList")).Text = PCMTISafetyDrugDiluentIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugDiluentCSList")).Text = PCMTISafetyDrugDiluentCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugLabIRList")).Text = PCMTISafetyDrugLabIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugLabCSList")).Text = PCMTISafetyDrugLabCSName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugDiseaseIRList")).Text = PCMTISafetyDrugDiseaseIRName;
        ((Label)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Label_ItemSafetyDrugDiseaseCSList")).Text = PCMTISafetyDrugDiseaseCSName;

        PCMInterventionDate = "";
        PCMTIIndicationNoIndicationIRName = "";
        PCMTIIndicationNoIndicationCSName = "";
        PCMTIIndicationDuplicationIRName = "";
        PCMTIIndicationDuplicationCSName = "";
        PCMTIIndicationUntreatedIRName = "";
        PCMTIIndicationUntreatedCSName = "";
        PCMTIDoseDoseName = "";
        PCMTIDoseDoseIRName = "";
        PCMTIDoseDoseCSName = "";
        PCMTIDoseIntervalName = "";
        PCMTIDoseIntervalIRName = "";
        PCMTIDoseIntervalCSName = "";
        PCMTIEfficacyChangeIRName = "";
        PCMTIEfficacyChangeCSName = "";
        PCMTISafetyAllergicIRName = "";
        PCMTISafetyAllergicCSName = "";
        PCMTISafetyUnwantedIRName = "";
        PCMTISafetyUnwantedCSName = "";
        PCMTISafetyDrugDrugIRName = "";
        PCMTISafetyDrugDrugCSName = "";
        PCMTISafetyDrugDiluentIRName = "";
        PCMTISafetyDrugDiluentCSName = "";
        PCMTISafetyDrugLabIRName = "";
        PCMTISafetyDrugLabCSName = "";
        PCMTISafetyDrugDiseaseIRName = "";
        PCMTISafetyDrugDiseaseCSName = "";


        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 52";
        using (SqlCommand SqlCommand_Email = new SqlCommand(SQLStringEmail))
        {
          DataTable DataTable_Email;
          using (DataTable_Email = new DataTable())
          {
            DataTable_Email.Locale = CultureInfo.CurrentCulture;
            DataTable_Email = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Email).Copy();
            if (DataTable_Email.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Email.Rows)
              {
                Email = DataRow_Row["Form_Email"].ToString();
                Print = DataRow_Row["Form_Print"].ToString();
              }
            }
          }
        }

        if (Print == "False")
        {
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_ItemPrint_TherapeuticIntervention")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_ItemPrint_TherapeuticIntervention")).Visible = true;
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_ItemPrint_TherapeuticIntervention")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Print", "InfoQuest_Print.aspx?PrintPage=Form_PharmacyClinicalMetrics_TherapeuticIntervention&PrintValue=" + Request.QueryString["PCMTIId"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_ItemEmail_TherapeuticIntervention")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_ItemEmail_TherapeuticIntervention")).Visible = true;
          ((Button)FormView_PharmacyClinicalMetrics_TherapeuticIntervention_Form.FindControl("Button_ItemEmail_TherapeuticIntervention")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Email", "InfoQuest_Email.aspx?EmailPage=Form_PharmacyClinicalMetrics_TherapeuticIntervention&EmailValue=" + Request.QueryString["PCMTIId"] + "") + "')";
        }

        Email = "";
        Print = "";
      }
    }


    //--START-- --Insert Controls--//
    protected void Button_InsertCancel_TherapeuticIntervention_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + "", false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void RadioButtonList_EditDoseDoseList_DataBound(object sender, EventArgs e)
    {
      RadioButtonList RadioButtonList_EditDoseDoseList = (RadioButtonList)sender;

      RadioButtonList_EditDoseDoseList.Items.Remove(RadioButtonList_EditDoseDoseList.Items.FindByValue(""));
    }

    protected void RadioButtonList_EditDoseIntervalList_DataBound(object sender, EventArgs e)
    {
      RadioButtonList RadioButtonList_EditDoseIntervalList = (RadioButtonList)sender;

      RadioButtonList_EditDoseIntervalList.Items.Remove(RadioButtonList_EditDoseIntervalList.Items.FindByValue(""));
    }

    protected void Button_EditCancel_TherapeuticIntervention_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + "", false);
    }

    protected void Button_EditUpdate_TherapeuticIntervention_Click(object sender, EventArgs e)
    {
      Button_EditUpdateTherapeuticInterventionClicked = true;
    }

    protected void Button_EditPrint_TherapeuticIntervention_Click(object sender, EventArgs e)
    {
      Button_EditPrintTherapeuticInterventionClicked = true;
    }

    protected void Button_EditEmail_TherapeuticIntervention_Click(object sender, EventArgs e)
    {
      Button_EditEmailTherapeuticInterventionClicked = true;
    }
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    protected void Button_ItemCancel_TherapeuticIntervention_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + "", false);
    }
    //---END--- --Item Controls--//
    //---END--- --TableTherapeuticIntervention--//


    //--START-- --TableTherapeuticInterventionList--//
    protected void SqlDataSource_PharmacyClinicalMetrics_TherapeuticIntervention_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_TherapeuticIntervention.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_TherapeuticIntervention_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageSize = Convert.ToInt32(((DropDownList)GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_TherapeuticIntervention")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_TherapeuticIntervention_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageIndex = ((DropDownList)GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_TherapeuticIntervention")).SelectedIndex;
    }

    protected void GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageSize <= 20)
        {
          ((DropDownList)GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_TherapeuticIntervention")).SelectedValue = "20";
        }
        else if (GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageSize > 20 && GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageSize <= 50)
        {
          ((DropDownList)GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_TherapeuticIntervention")).SelectedValue = "50";
        }
        else if (GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageSize > 50 && GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageSize <= 100)
        {
          ((DropDownList)GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_TherapeuticIntervention")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_TherapeuticIntervention")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_PharmacyClinicalMetrics_TherapeuticIntervention_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          int m = e.Row.Cells.Count;

          for (int i = m - 1; i >= 1; i += -1)
          {
            e.Row.Cells.RemoveAt(i);
          }

          e.Row.Cells[0].ColumnSpan = m;
          e.Row.Cells[0].Text = Convert.ToString("&nbsp;", CultureInfo.CurrentCulture);
        }
      }
    }

    protected void Button_CaptureNew_TherapeuticIntervention_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics New Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId" + Request.QueryString["PCMInterventionId"] + ""), false);
    }

    public string GetLink_TherapeuticIntervention(object pcm_Intervention_Id, object pcm_TI_Id)
    {
      string FinalURL = "";

      if (pcm_TI_Id != null && pcm_Intervention_Id != null)
      {
        string LinkURL = "";

        if (Request.QueryString["PCMTIId"] == pcm_TI_Id.ToString())
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics New Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + pcm_Intervention_Id + "&PCMTIId=" + pcm_TI_Id + "") + "'>Selected</a>";
        }
        else
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics New Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + pcm_Intervention_Id + "&PCMTIId=" + pcm_TI_Id + "") + "'>Select</a>";
        }

        FinalURL = LinkURL;
      }

      return FinalURL;
    }
    //---END--- --TableTherapeuticInterventionList--//


    //--START-- --TablePharmacistTime--//
    protected void SetCurrentPharmacistTimeVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["PCMPTId"]))
      {
        SetCurrentPharmacistTimeVisibility_Insert();
      }
      else
      {
        SetCurrentPharmacistTimeVisibility_Edit();
      }
    }

    protected void SetCurrentPharmacistTimeVisibility_Insert()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_Current = GetFormViewUpdate();
      string ViewUpdate = FromDataBase_FormViewUpdate_Current.ViewUpdate;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (ViewUpdate == "Yes")
        {
          FormView_PharmacyClinicalMetrics_PharmacistTime_Form.ChangeMode(FormViewMode.Insert);
        }
        else
        {
          FormView_PharmacyClinicalMetrics_PharmacistTime_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_PharmacistTime_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_PharmacistTime_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetCurrentPharmacistTimeVisibility_Edit()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_Current = GetFormViewUpdate();
      string ViewUpdate = FromDataBase_FormViewUpdate_Current.ViewUpdate;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_PharmacistTime_Form.ChangeMode(FormViewMode.Edit);
      }

      if (Security == "1" && (SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (ViewUpdate == "Yes")
        {
          FormView_PharmacyClinicalMetrics_PharmacistTime_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_PharmacyClinicalMetrics_PharmacistTime_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_PharmacistTime_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_PharmacyClinicalMetrics_PharmacistTime_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentPharmacistTimeVisible()
    {
      if (FormView_PharmacyClinicalMetrics_PharmacistTime_Form.CurrentMode == FormViewMode.Insert)
      {
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertReviewPatientInformationIcon")).Text = GetListItemName("6256").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertReviewedlabresultsIcon")).Text = GetListItemName("6257").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertConductedresearchIcon")).Text = GetListItemName("6252").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertWardroundswithdoctorIcon")).Text = GetListItemName("6259").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertPatientcounsellingeducationIcon")).Text = GetListItemName("6253").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertTrainingofwardpharmacystaffIcon")).Text = GetListItemName("6258").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertReportingofanadversedrugreactionIcon")).Text = GetListItemName("6255").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertPharmacokineticcalculationsIcon")).Text = GetListItemName("6254").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertAdvicetodoctorIcon")).Text = GetListItemName("6250").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertAdvicetonurseIcon")).Text = GetListItemName("6251").ListItemName;

        ((HyperLink)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("HyperLink_InsertReportingURL")).NavigateUrl = GetListItemName("6237").ListItemName;


        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertInterventionBy")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertInterventionBy")).Attributes.Add("OnInput", "Validation_Form();");

        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertPatientFile")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertPatientLabResults")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertPatientPrescription")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertPatientTotalTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertPatientTotalTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertPatientAmount")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertPatientAmount")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertPatientComments")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertPatientComments")).Attributes.Add("OnInput", "Validation_Form();");

        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertMedication")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicationTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicationTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicationComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicationComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertResearch")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertResearchTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertResearchTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertResearchComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertResearchComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertRounds")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertRoundsTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertRoundsTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertRoundsComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertRoundsComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertCounselling")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCounsellingTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCounsellingTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCounsellingComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCounsellingComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertTraining")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertTrainingTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertTrainingTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertTrainingComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertTrainingComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertReporting")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertReportingTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertReportingTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertReportingComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertReportingComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertCalculations")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCalculationsTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCalculationsTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCalculationsComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCalculationsComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertAdviceDoctor")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceDoctorTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceDoctorTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceDoctorComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceDoctorComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertAdviceNurse")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceNurseTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceNurseTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceNurseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceNurseComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertMedicalHistory")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicalHistoryTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicalHistoryTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicalHistoryComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicalHistoryComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertStatistics")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertStatisticsTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertStatisticsTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertStatisticsComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertStatisticsComment")).Attributes.Add("OnInput", "Validation_Form();");
      }

      if (FormView_PharmacyClinicalMetrics_PharmacistTime_Form.CurrentMode == FormViewMode.Edit)
      {
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditReviewPatientInformationIcon")).Text = GetListItemName("6256").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditReviewedlabresultsIcon")).Text = GetListItemName("6257").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditConductedresearchIcon")).Text = GetListItemName("6252").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditWardroundswithdoctorIcon")).Text = GetListItemName("6259").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditPatientcounsellingeducationIcon")).Text = GetListItemName("6253").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditTrainingofwardpharmacystaffIcon")).Text = GetListItemName("6258").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditReportingofanadversedrugreactionIcon")).Text = GetListItemName("6255").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditPharmacokineticcalculationsIcon")).Text = GetListItemName("6254").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditAdvicetodoctorIcon")).Text = GetListItemName("6250").ListItemName;
        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditAdvicetonurseIcon")).Text = GetListItemName("6251").ListItemName;

        ((HyperLink)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("HyperLink_EditReportingURL")).NavigateUrl = GetListItemName("6237").ListItemName;

        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditInterventionBy")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditInterventionBy")).Attributes.Add("OnInput", "Validation_Form();");

        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditPatientFile")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditPatientLabResults")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditPatientPrescription")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditPatientTotalTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditPatientTotalTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditPatientAmount")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditPatientAmount")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditPatientComments")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditPatientComments")).Attributes.Add("OnInput", "Validation_Form();");

        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditMedication")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicationTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicationTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicationComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicationComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditResearch")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditResearchTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditResearchTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditResearchComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditResearchComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditRounds")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditRoundsTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditRoundsTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditRoundsComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditRoundsComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditCounselling")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCounsellingTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCounsellingTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCounsellingComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCounsellingComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditTraining")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditTrainingTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditTrainingTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditTrainingComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditTrainingComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditReporting")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditReportingTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditReportingTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditReportingComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditReportingComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditCalculations")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCalculationsTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCalculationsTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCalculationsComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCalculationsComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditAdviceDoctor")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceDoctorTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceDoctorTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceDoctorComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceDoctorComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditAdviceNurse")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceNurseTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceNurseTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceNurseComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceNurseComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditMedicalHistory")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicalHistoryTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicalHistoryTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicalHistoryComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicalHistoryComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditStatistics")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditStatisticsTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditStatisticsTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditStatisticsComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditStatisticsComment")).Attributes.Add("OnInput", "Validation_Form();");
      }

      if (FormView_PharmacyClinicalMetrics_PharmacistTime_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["PCMPTId"]))
        {
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemReviewPatientInformationIcon")).Text = GetListItemName("6256").ListItemName;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemReviewedlabresultsIcon")).Text = GetListItemName("6257").ListItemName;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemConductedresearchIcon")).Text = GetListItemName("6252").ListItemName;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemWardroundswithdoctorIcon")).Text = GetListItemName("6259").ListItemName;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemPatientcounsellingeducationIcon")).Text = GetListItemName("6253").ListItemName;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemTrainingofwardpharmacystaffIcon")).Text = GetListItemName("6258").ListItemName;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemReportingofanadversedrugreactionIcon")).Text = GetListItemName("6255").ListItemName;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemPharmacokineticcalculationsIcon")).Text = GetListItemName("6254").ListItemName;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemAdvicetodoctorIcon")).Text = GetListItemName("6250").ListItemName;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemAdvicetonurseIcon")).Text = GetListItemName("6251").ListItemName;

          ((HyperLink)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("HyperLink_ItemReportingURL")).NavigateUrl = GetListItemName("6237").ListItemName;
        }
      }
    }


    //--START-- --Insert--//
    protected void FormView_PharmacyClinicalMetrics_PharmacistTime_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_PharmacistTime();

        if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;
        }
        else
        {
          e.Cancel = true;
        }

        if (e.Cancel == true)
        {
          ToolkitScriptManager_PharmacyClinicalMetrics.SetFocus(UpdatePanel_PharmacyClinicalMetrics);
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
          string FacilityId = FromDataBase_FacilityId_Current.FacilityId;

          string PCM_PT_ReportNumber = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], FacilityId, "52");

          SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_Intervention_Id"].DefaultValue = Request.QueryString["PCMInterventionId"];
          SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_PT_ReportNumber"].DefaultValue = PCM_PT_ReportNumber;
          SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_PT_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_PT_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_PT_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_PT_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_PT_History"].DefaultValue = "";
          SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.InsertParameters["PCM_PT_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation_PharmacistTime()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertInterventionBy")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertPatientFile")).Checked == true || ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertPatientLabResults")).Checked == true || ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertPatientPrescription")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertPatientTotalTime")).Text) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertPatientAmount")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertPatientComments")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertMedication")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicationTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicationComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertResearch")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertResearchTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertResearchComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertRounds")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertRoundsTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertRoundsComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertCounselling")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCounsellingTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCounsellingComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertTraining")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertTrainingTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertTrainingComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertReporting")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertReportingTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertReportingComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertCalculations")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCalculationsTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertCalculationsComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertAdviceDoctor")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceDoctorTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceDoctorComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertAdviceNurse")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceNurseTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertAdviceNurseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertMedicalHistory")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicalHistoryTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertMedicalHistoryComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertStatistics")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertStatisticsTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_InsertStatisticsComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = InsertFieldValidation_PharmacistTime(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string InsertFieldValidation_PharmacistTime(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertPatientFile")).Checked == false && 
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertPatientLabResults")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertPatientPrescription")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertMedication")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertResearch")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertRounds")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertCounselling")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertTraining")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertReporting")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertCalculations")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertAdviceDoctor")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertAdviceNurse")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertMedicalHistory")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_InsertStatistics")).Checked == false)
      {
        InvalidFormMessage = InvalidFormMessage + "At least one of the Interventions needs to be selected<br />";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string PCM_PT_ReportNumber = e.Command.Parameters["@PCM_PT_ReportNumber"].Value.ToString();

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_PharmacyClinicalMetrics_PharmacistTime&ReportNumber=" + PCM_PT_ReportNumber + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    protected void FormView_PharmacyClinicalMetrics_PharmacistTime_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDPCMPTModifiedDate"] = e.OldValues["PCM_PT_ModifiedDate"];
        object OLDPCMPTModifiedDate = Session["OLDPCMPTModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDPCMPTModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_ComparePharmacyClinicalMetrics_PharmacistTime = (DataView)SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ComparePharmacyClinicalMetrics_PharmacistTime = DataView_ComparePharmacyClinicalMetrics_PharmacistTime[0];
        Session["DBPCMPTModifiedDate"] = Convert.ToString(DataRowView_ComparePharmacyClinicalMetrics_PharmacistTime["PCM_PT_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBPCMPTModifiedBy"] = Convert.ToString(DataRowView_ComparePharmacyClinicalMetrics_PharmacistTime["PCM_PT_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBPCMPTModifiedDate = Session["DBPCMPTModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBPCMPTModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacyClinicalMetrics.SetFocus(UpdatePanel_PharmacyClinicalMetrics);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBPCMPTModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_PharmacistTime();

          if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
          }
          else
          {
            e.Cancel = true;
          }

          if (e.Cancel == true)
          {
            ToolkitScriptManager_PharmacyClinicalMetrics.SetFocus(UpdatePanel_PharmacyClinicalMetrics);
            ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["PCM_PT_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["PCM_PT_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_PharmacyClinicalMetrics_PharmacistTime", "PCM_PT_Id = " + Request.QueryString["PCMPTId"]);

            DataView DataView_PharmacyClinicalMetrics_PharmacistTime = (DataView)SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_PharmacyClinicalMetrics_PharmacistTime = DataView_PharmacyClinicalMetrics_PharmacistTime[0];
            Session["PCMPTHistory"] = Convert.ToString(DataRowView_PharmacyClinicalMetrics_PharmacistTime["PCM_PT_History"], CultureInfo.CurrentCulture);

            Session["PCMPTHistory"] = Session["History"].ToString() + Session["PCMPTHistory"].ToString();
            e.NewValues["PCM_PT_History"] = Session["PCMPTHistory"].ToString();

            Session["PCMPTHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDPCMPTModifiedDate"] = "";
        Session["DBPCMPTModifiedDate"] = "";
        Session["DBPCMPTModifiedBy"] = "";
      }
    }

    protected string EditValidation_PharmacistTime()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditInterventionBy")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditPatientFile")).Checked == true || ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditPatientLabResults")).Checked == true || ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditPatientPrescription")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditPatientTotalTime")).Text) || string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditPatientAmount")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditPatientComments")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditMedication")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicationTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicationComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditResearch")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditResearchTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditResearchComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditRounds")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditRoundsTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditRoundsComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditCounselling")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCounsellingTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCounsellingComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditTraining")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditTrainingTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditTrainingComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditReporting")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditReportingTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditReportingComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditCalculations")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCalculationsTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditCalculationsComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditAdviceDoctor")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceDoctorTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceDoctorComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditAdviceNurse")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceNurseTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditAdviceNurseComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditMedicalHistory")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicalHistoryTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditMedicalHistoryComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditStatistics")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditStatisticsTime")).Text))// || String.IsNullOrEmpty(((TextBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("TextBox_EditStatisticsComment")).Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = EditFieldValidation_PharmacistTime(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation_PharmacistTime(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      if (((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditPatientFile")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditPatientLabResults")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditPatientPrescription")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditMedication")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditResearch")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditRounds")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditCounselling")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditTraining")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditReporting")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditCalculations")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditAdviceDoctor")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditAdviceNurse")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditMedicalHistory")).Checked == false &&
          ((CheckBox)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("CheckBox_EditStatistics")).Checked == false)
      {
        InvalidFormMessage = InvalidFormMessage + "At least one of the Interventions needs to be selected<br />";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdatePharmacistTimeClicked == true)
          {
            Button_EditUpdatePharmacistTimeClicked = false;

            if (!string.IsNullOrEmpty(Request.QueryString["PCMPTId"]))
            {
              Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + ""), false);
            }
          }

          if (Button_EditPrintPharmacistTimeClicked == true)
          {
            Button_EditPrintPharmacistTimeClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacyClinicalMetrics, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Print", "InfoQuest_Print.aspx?PrintPage=Form_PharmacyClinicalMetrics_PharmacistTime&PrintValue=" + Request.QueryString["PCMPTId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacyClinicalMetrics, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailPharmacistTimeClicked == true)
          {
            Button_EditEmailPharmacistTimeClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacyClinicalMetrics, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Email", "InfoQuest_Email.aspx?EmailPage=Form_PharmacyClinicalMetrics_PharmacistTime&EmailValue=" + Request.QueryString["PCMPTId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacyClinicalMetrics, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_PharmacyClinicalMetrics_PharmacistTime_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["PCMPTId"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + ""), false);
          }
        }
      }
    }

    protected void FormView_PharmacyClinicalMetrics_PharmacistTime_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_PharmacyClinicalMetrics_PharmacistTime_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound_PharmacyClinicalMetrics_PharmacistTime();
      }

      if (FormView_PharmacyClinicalMetrics_PharmacistTime_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound_PharmacyClinicalMetrics_PharmacistTime();
      }
    }

    protected void EditDataBound_PharmacyClinicalMetrics_PharmacistTime()
    {
      if (Request.QueryString["PCMPTId"] != null)
      {
        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 52";
        using (SqlCommand SqlCommand_Email = new SqlCommand(SQLStringEmail))
        {
          DataTable DataTable_Email;
          using (DataTable_Email = new DataTable())
          {
            DataTable_Email.Locale = CultureInfo.CurrentCulture;
            DataTable_Email = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Email).Copy();
            if (DataTable_Email.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Email.Rows)
              {
                Email = DataRow_Row["Form_Email"].ToString();
                Print = DataRow_Row["Form_Print"].ToString();
              }
            }
          }
        }

        if (Print == "False")
        {
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_EditPrint_PharmacistTime")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_EditPrint_PharmacistTime")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_EditEmail_PharmacistTime")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_EditEmail_PharmacistTime")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound_PharmacyClinicalMetrics_PharmacistTime()
    {
      if (Request.QueryString["PCMPTId"] != null)
      {
        Session["UnitName"] = "";
        string SQLStringPharmacistTime = "SELECT Unit_Name FROM vForm_PharmacyClinicalMetrics_PharmacistTime WHERE PCM_PT_Id = @PCM_PT_Id";
        using (SqlCommand SqlCommand_PharmacistTime = new SqlCommand(SQLStringPharmacistTime))
        {
          SqlCommand_PharmacistTime.Parameters.AddWithValue("@PCM_PT_Id", Request.QueryString["PCMPTId"]);
          DataTable DataTable_PharmacistTime;
          using (DataTable_PharmacistTime = new DataTable())
          {
            DataTable_PharmacistTime.Locale = CultureInfo.CurrentCulture;
            DataTable_PharmacistTime = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PharmacistTime).Copy();
            if (DataTable_PharmacistTime.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PharmacistTime.Rows)
              {
                Session["UnitName"] = DataRow_Row["Unit_Name"];
              }
            }
          }
        }


        ((Label)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Label_ItemUnit")).Text = Session["UnitName"].ToString();

        Session["UnitName"] = "";


        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 52";
        using (SqlCommand SqlCommand_Email = new SqlCommand(SQLStringEmail))
        {
          DataTable DataTable_Email;
          using (DataTable_Email = new DataTable())
          {
            DataTable_Email.Locale = CultureInfo.CurrentCulture;
            DataTable_Email = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Email).Copy();
            if (DataTable_Email.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Email.Rows)
              {
                Email = DataRow_Row["Form_Email"].ToString();
                Print = DataRow_Row["Form_Print"].ToString();
              }
            }
          }
        }

        if (Print == "False")
        {
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_ItemPrint_PharmacistTime")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_ItemPrint_PharmacistTime")).Visible = true;
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_ItemPrint_PharmacistTime")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Print", "InfoQuest_Print.aspx?PrintPage=Form_PharmacyClinicalMetrics_PharmacistTime&PrintValue=" + Request.QueryString["PCMPTId"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_ItemEmail_PharmacistTime")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_ItemEmail_PharmacistTime")).Visible = true;
          ((Button)FormView_PharmacyClinicalMetrics_PharmacistTime_Form.FindControl("Button_ItemEmail_PharmacistTime")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics Email", "InfoQuest_Email.aspx?EmailPage=Form_PharmacyClinicalMetrics_PharmacistTime&EmailValue=" + Request.QueryString["PCMPTId"] + "") + "')";
        }

        Email = "";
        Print = "";
      }
    }


    //--START-- --Insert Controls--//
    protected void Button_InsertCancel_PharmacistTime_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + "", false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void Button_EditCancel_PharmacistTime_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + "", false);
    }

    protected void Button_EditUpdate_PharmacistTime_Click(object sender, EventArgs e)
    {
      Button_EditUpdatePharmacistTimeClicked = true;
    }

    protected void Button_EditPrint_PharmacistTime_Click(object sender, EventArgs e)
    {
      Button_EditPrintPharmacistTimeClicked = true;
    }

    protected void Button_EditEmail_PharmacistTime_Click(object sender, EventArgs e)
    {
      Button_EditEmailPharmacistTimeClicked = true;
    }
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    protected void Button_ItemCancel_PharmacistTime_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + "", false);
    }
    //---END--- --Item Controls--//
    //---END--- --TablePharmacistTime--//


    //--START-- --TablePharmacistTimeList--//
    protected void SqlDataSource_PharmacyClinicalMetrics_PharmacistTime_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_PharmacistTime.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_PharmacistTime_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageSize = Convert.ToInt32(((DropDownList)GridView_PharmacyClinicalMetrics_PharmacistTime_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_PharmacistTime")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_PharmacistTime_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageIndex = ((DropDownList)GridView_PharmacyClinicalMetrics_PharmacistTime_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_PharmacistTime")).SelectedIndex;
    }

    protected void GridView_PharmacyClinicalMetrics_PharmacistTime_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageSize <= 20)
        {
          ((DropDownList)GridView_PharmacyClinicalMetrics_PharmacistTime_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_PharmacistTime")).SelectedValue = "20";
        }
        else if (GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageSize > 20 && GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageSize <= 50)
        {
          ((DropDownList)GridView_PharmacyClinicalMetrics_PharmacistTime_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_PharmacistTime")).SelectedValue = "50";
        }
        else if (GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageSize > 50 && GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageSize <= 100)
        {
          ((DropDownList)GridView_PharmacyClinicalMetrics_PharmacistTime_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_PharmacistTime")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_PharmacyClinicalMetrics_PharmacistTime_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_PharmacyClinicalMetrics_PharmacistTime_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_PharmacyClinicalMetrics_PharmacistTime_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_PharmacistTime")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_PharmacyClinicalMetrics_PharmacistTime_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          int m = e.Row.Cells.Count;

          for (int i = m - 1; i >= 1; i += -1)
          {
            e.Row.Cells.RemoveAt(i);
          }

          e.Row.Cells[0].ColumnSpan = m;
          e.Row.Cells[0].Text = Convert.ToString("&nbsp;", CultureInfo.CurrentCulture);
        }
      }
    }

    protected void Button_CaptureNew_PharmacistTime_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics New Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + Request.QueryString["PCMInterventionId"] + ""), false);
    }

    public string GetLink_PharmacistTime(object pcm_Intervention_Id, object pcm_PT_Id)
    {
      string FinalURL = "";

      if (pcm_PT_Id != null && pcm_Intervention_Id != null)
      {
        string LinkURL = "";

        if (Request.QueryString["PCMPTId"] == pcm_PT_Id.ToString())
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics New Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + pcm_Intervention_Id + "&PCMPTId=" + pcm_PT_Id + "") + "'>Selected</a>";
        }
        else
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Clinical Metrics New Form", "Form_PharmacyClinicalMetrics.aspx?PCMInterventionId=" + pcm_Intervention_Id + "&PCMPTId=" + pcm_PT_Id + "") + "'>Select</a>";
        }

        FinalURL = LinkURL;
      }

      return FinalURL;
    }
    //---END--- --TablePharmacistTimeList--//
  }
}