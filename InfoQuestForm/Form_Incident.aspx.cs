using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace InfoQuestForm
{
  public partial class Form_Incident : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_Incident, this.GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("1")).ToString();
          Label_FacilityHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("1")).ToString();
          Label_IncidentHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("1")).ToString();

          if (Request.QueryString["s_Facility_Id"] != null)
          {
            TableFacility.Visible = false;
            TableForm.Visible = true;

            if (Request.QueryString["Incident_Id"] == null)
            {
              ((HtmlTableRow)FormView_Incident_Form.FindControl("IncidentReportableTriggerLevel")).Visible = false;
            }
            else
            {
              SqlDataSource_Incident_EditFacilityFrom.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
              SqlDataSource_Incident_EditFacilityFrom.SelectParameters["TableSELECT"].DefaultValue = "Incident_FacilityFrom_Facility";
              SqlDataSource_Incident_EditFacilityFrom.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditFacilityFrom.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditUnitFromUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
              SqlDataSource_Incident_EditUnitFromUnit.SelectParameters["TableSELECT"].DefaultValue = "Incident_UnitFrom_Unit";
              SqlDataSource_Incident_EditUnitFromUnit.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditUnitFromUnit.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditUnitToUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
              SqlDataSource_Incident_EditUnitToUnit.SelectParameters["TableSELECT"].DefaultValue = "Incident_UnitTo_Unit";
              SqlDataSource_Incident_EditUnitToUnit.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditUnitToUnit.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters["TableSELECT"].DefaultValue = "Incident_IncidentCategory_List";
              SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditDisciplineList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Discipline_List";
              SqlDataSource_Incident_EditDisciplineList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditDisciplineList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
              SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters["TableSELECT"].DefaultValue = "Incident_E_EmployeeUnit_Unit";
              SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters["TableSELECT"].DefaultValue = "Incident_E_EmployeeStatus_List";
              SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters["TableSELECT"].DefaultValue = "Incident_E_StaffCategory_List";
              SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters["TableSELECT"].DefaultValue = "Incident_E_BodyPartAffected_List";
              SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters["TableSELECT"].DefaultValue = "Incident_E_TreatmentRequired_List";
              SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditEMainContributorList.SelectParameters["TableSELECT"].DefaultValue = "Incident_E_MainContributor_List";
              SqlDataSource_Incident_EditEMainContributorList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditEMainContributorList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters["TableSELECT"].DefaultValue = "Incident_E_MainContributor_Staff_List";
              SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters["TableSELECT"].DefaultValue = "Incident_MMDT_Discipline_List";
              SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPMainContributorList.SelectParameters["TableSELECT"].DefaultValue = "Incident_P_MainContributor_List";
              SqlDataSource_Incident_EditPMainContributorList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPMainContributorList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters["TableSELECT"].DefaultValue = "Incident_P_MainContributor_Staff_List";
              SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPropMainContributorList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Prop_MainContributor_List";
              SqlDataSource_Incident_EditPropMainContributorList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPropMainContributorList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Prop_MainContributor_Staff_List";
              SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditLevel1List.SelectParameters["TableSELECT"].DefaultValue = "Incident_Level1_List";
              SqlDataSource_Incident_EditLevel1List.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditLevel1List.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditLevel2List.SelectParameters["TableSELECT"].DefaultValue = "Incident_Level2_List";
              SqlDataSource_Incident_EditLevel2List.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditLevel2List.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditLevel3List.SelectParameters["TableSELECT"].DefaultValue = "Incident_Level3_List";
              SqlDataSource_Incident_EditLevel3List.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditLevel3List.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditSeverityList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Severity_List";
              SqlDataSource_Incident_EditSeverityList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditSeverityList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters["TableSELECT"].DefaultValue = "Incident_PatientFalling_WhereFallOccur_List";
              SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters["TableSELECT"].DefaultValue = "Incident_DegreeOfHarm_Impact_Impact_List";
              SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident_DegreeOfHarm_Impact";
              SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Pharmacy_StaffInvolved_List";
              SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Pharmacy_Checking_List";
              SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Pharmacy_StaffOnDuty_List";
              SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Pharmacy_TypeOfPrescription_List";
              SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Pharmacy_Factors_List";
              SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Pharmacy_SystemRelatedIssues_List";
              SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Pharmacy_ErgonomicProblems_List";
              SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters["TableSELECT"].DefaultValue = "Incident_Pharmacy_Location_List";
              SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";

              SqlDataSource_Incident_EditRootCategoryList.SelectParameters["TableSELECT"].DefaultValue = "Incident_RootCategory_List";
              SqlDataSource_Incident_EditRootCategoryList.SelectParameters["TableFROM"].DefaultValue = "Form_Incident";
              SqlDataSource_Incident_EditRootCategoryList.SelectParameters["TableWHERE"].DefaultValue = "Incident_Id = " + Request.QueryString["Incident_Id"] + " ";
            }

            SetFormVisibility();
          }
          else
          {
            TableFacility.Visible = true;
            TableForm.Visible = false;
          }

          if (TableForm.Visible == true)
          {
            TableFormVisible();
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
        if (Request.QueryString["Incident_Id"] == null)
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('1'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('1')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_Incident WHERE Incident_Id = @Incident_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          SecurityAllow = "1";
          //SecurityAllow = "0";
          //Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
          //Response.End();
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("1");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_Incident.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Incident", "7");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Incident_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Incident_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_Facility.SelectParameters.Clear();
      SqlDataSource_Incident_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Incident_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");


      SqlDataSourceSetup_Insert();
      SqlDataSourceSetup_Edit();
      SqlDataSourceSetup_Item();


      SqlDataSource_Incident_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_Form.InsertCommand = "INSERT INTO Form_Incident ( Facility_Id ,Incident_ReportNumber , Incident_FacilityFrom_Facility , Incident_Date ,Incident_Time_Hours ,Incident_Time_Min ,Incident_ReportingPerson ,Incident_UnitFrom_Unit ,Incident_UnitTo_Unit ,Incident_IncidentCategory_List ,Incident_CVR_Name ,Incident_CVR_ContactNumber ,Incident_E_EmployeeNumber ,Incident_E_EmployeeName ,Incident_E_EmployeeUnit_Unit ,Incident_E_EmployeeStatus_List ,Incident_E_StaffCategory_List ,Incident_E_BodyPartAffected_List , Incident_E_TreatmentRequired_List , Incident_E_DaysOff , Incident_E_MainContributor_List , Incident_E_MainContributor_Staff_List ,Incident_MMDT_Name ,Incident_MMDT_ContactNumber ,Incident_MMDT_Discipline_List ,Incident_P_VisitNumber ,Incident_P_Name , Incident_P_MainContributor_List , Incident_P_MainContributor_Staff_List , Incident_Prop_MainContributor_List , Incident_Prop_MainContributor_Staff_List , Incident_SS_Name ,Incident_SS_ContactNumber ,Incident_Description ,Incident_Level1_List ,Incident_Level2_List ,Incident_Level3_List ,Incident_Severity_List ,Incident_Reportable ,Incident_Report_COID ,Incident_Report_COID_Date ,Incident_Report_COID_Number ,Incident_Report_DEAT ,Incident_Report_DEAT_Date ,Incident_Report_DEAT_Number ,Incident_Report_DepartmentOfHealth ,Incident_Report_DepartmentOfHealth_Date ,Incident_Report_DepartmentOfHealth_Number ,Incident_Report_DepartmentOfLabour ,Incident_Report_DepartmentOfLabour_Date ,Incident_Report_DepartmentOfLabour_Number ,Incident_Report_HospitalManager ,Incident_Report_HospitalManager_Date ,Incident_Report_HospitalManager_Number ,Incident_Report_HPCSA ,Incident_Report_HPCSA_Date ,Incident_Report_HPCSA_Number ,Incident_Report_LegalDepartment ,Incident_Report_LegalDepartment_Date ,Incident_Report_LegalDepartment_Number ,Incident_Report_CEO ,Incident_Report_CEO_Date ,Incident_Report_CEO_Number ,Incident_Report_PharmacyCouncil ,Incident_Report_PharmacyCouncil_Date ,Incident_Report_PharmacyCouncil_Number ,Incident_Report_Quality ,Incident_Report_Quality_Date ,Incident_Report_Quality_Number ,Incident_Report_RM ,Incident_Report_RM_Date ,Incident_Report_RM_Number ,Incident_Report_SANC ,Incident_Report_SANC_Date ,Incident_Report_SANC_Number ,Incident_Report_SAPS ,Incident_Report_SAPS_Date ,Incident_Report_SAPS_Number ,Incident_Report_InternalAudit ,Incident_Report_InternalAudit_Date ,Incident_Report_InternalAudit_Number , Incident_PatientFalling_WhereFallOccur_List , Incident_PatientDetail_VisitNumber , Incident_PatientDetail_Name , Incident_Pharmacy_Initials , Incident_Pharmacy_StaffInvolved_List , Incident_Pharmacy_Checking_List , Incident_Pharmacy_LocumOrPermanent , Incident_Pharmacy_StaffOnDuty_List , Incident_Pharmacy_ChangeInWorkProcedure , Incident_Pharmacy_TypeOfPrescription_List , Incident_Pharmacy_NumberOfRxOnDay , Incident_Pharmacy_NumberOfItemsDispensedOnDay , Incident_Pharmacy_NumberOfRxDayBefore , Incident_Pharmacy_NumberOfItemsDispensedDayBefore , Incident_Pharmacy_DrugPrescribed , Incident_Pharmacy_DrugDispensed , Incident_Pharmacy_DrugPacked , Incident_Pharmacy_StrengthDrugPrescribed , Incident_Pharmacy_StrengthDrugDispensed , Incident_Pharmacy_DrugPrescribedNewOnMarket , Incident_Pharmacy_LegislativeInformationOnPrescription , Incident_Pharmacy_LegislativeInformationNotOnPrescription , Incident_Pharmacy_DoctorName , Incident_Pharmacy_Factors_List , Incident_Pharmacy_SystemRelatedIssues_List , Incident_Pharmacy_ErgonomicProblems_List , Incident_Pharmacy_PatientCounselled , Incident_Pharmacy_SimilarIncident , Incident_Pharmacy_Location_List , Incident_Pharmacy_PatientOutcomeAffected , Incident_Reportable_CEO_AcknowledgedHM , Incident_Reportable_CEO_DoctorRelated , Incident_Reportable_CEO_CEONotifiedWithin24Hours , Incident_Reportable_CEO_ProgressUpdateSent , Incident_Reportable_CEO_ActionsTakenHM ,Incident_Reportable_CEO_Date ,Incident_Reportable_CEO_ActionsAgainstEmployee ,Incident_Reportable_CEO_EmployeeNumber ,Incident_Reportable_CEO_EmployeeName , Incident_Reportable_CEO_Outcome ,Incident_Reportable_CEO_FileScanned , Incident_Reportable_CEO_CloseOffHM , Incident_Reportable_CEO_CloseOutEmailSend , Incident_Reportable_SAPS_PoliceStation , Incident_Reportable_SAPS_InvestigationOfficersName , Incident_Reportable_SAPS_TelephoneNumber , Incident_Reportable_SAPS_CaseNumber , Incident_Reportable_InternalAudit_DateDetected , Incident_Reportable_InternalAudit_ByWhom , Incident_Reportable_InternalAudit_TotalLossValue , Incident_Reportable_InternalAudit_TotalRecovery , Incident_Reportable_InternalAudit_RecoveryPlan , Incident_Reportable_InternalAudit_StatusOfInvestigation , Incident_Reportable_InternalAudit_SAPSNotReported , Incident_DegreeOfHarm_DegreeOfHarm_List , Incident_DegreeOfHarm_Cost , Incident_DegreeOfHarm_Implications ,Incident_RootCategory_List ,Incident_RootDescription ,Incident_Discipline_List ,Incident_Investigator ,Incident_InvestigatorContactNumber ,Incident_InvestigatorDesignation ,Incident_InvestigationCompleted ,Incident_InvestigationCompletedDate ,Incident_Status ,Incident_StatusDate ,Incident_CreatedDate ,Incident_CreatedBy ,Incident_ModifiedDate ,Incident_ModifiedBy ,Incident_History ,Incident_Archived ) VALUES ( @Facility_Id ,@Incident_ReportNumber , @Incident_FacilityFrom_Facility , @Incident_Date ,@Incident_Time_Hours ,@Incident_Time_Min ,@Incident_ReportingPerson ,@Incident_UnitFrom_Unit ,@Incident_UnitTo_Unit ,@Incident_IncidentCategory_List ,@Incident_CVR_Name ,@Incident_CVR_ContactNumber ,@Incident_E_EmployeeNumber ,@Incident_E_EmployeeName ,@Incident_E_EmployeeUnit_Unit ,@Incident_E_EmployeeStatus_List ,@Incident_E_StaffCategory_List ,@Incident_E_BodyPartAffected_List , @Incident_E_TreatmentRequired_List , @Incident_E_DaysOff , @Incident_E_MainContributor_List , @Incident_E_MainContributor_Staff_List ,@Incident_MMDT_Name ,@Incident_MMDT_ContactNumber ,@Incident_MMDT_Discipline_List ,@Incident_P_VisitNumber ,@Incident_P_Name , @Incident_P_MainContributor_List , @Incident_P_MainContributor_Staff_List , @Incident_Prop_MainContributor_List , @Incident_Prop_MainContributor_Staff_List , @Incident_SS_Name ,@Incident_SS_ContactNumber ,@Incident_Description ,@Incident_Level1_List ,@Incident_Level2_List ,@Incident_Level3_List ,@Incident_Severity_List ,@Incident_Reportable ,@Incident_Report_COID ,@Incident_Report_COID_Date ,@Incident_Report_COID_Number ,@Incident_Report_DEAT ,@Incident_Report_DEAT_Date ,@Incident_Report_DEAT_Number ,@Incident_Report_DepartmentOfHealth ,@Incident_Report_DepartmentOfHealth_Date ,@Incident_Report_DepartmentOfHealth_Number ,@Incident_Report_DepartmentOfLabour ,@Incident_Report_DepartmentOfLabour_Date ,@Incident_Report_DepartmentOfLabour_Number ,@Incident_Report_HospitalManager ,@Incident_Report_HospitalManager_Date ,@Incident_Report_HospitalManager_Number ,@Incident_Report_HPCSA ,@Incident_Report_HPCSA_Date ,@Incident_Report_HPCSA_Number ,@Incident_Report_LegalDepartment ,@Incident_Report_LegalDepartment_Date ,@Incident_Report_LegalDepartment_Number ,@Incident_Report_CEO ,@Incident_Report_CEO_Date ,@Incident_Report_CEO_Number ,@Incident_Report_PharmacyCouncil ,@Incident_Report_PharmacyCouncil_Date ,@Incident_Report_PharmacyCouncil_Number ,@Incident_Report_Quality ,@Incident_Report_Quality_Date ,@Incident_Report_Quality_Number ,@Incident_Report_RM ,@Incident_Report_RM_Date ,@Incident_Report_RM_Number ,@Incident_Report_SANC ,@Incident_Report_SANC_Date ,@Incident_Report_SANC_Number ,@Incident_Report_SAPS ,@Incident_Report_SAPS_Date ,@Incident_Report_SAPS_Number ,@Incident_Report_InternalAudit ,@Incident_Report_InternalAudit_Date ,@Incident_Report_InternalAudit_Number , @Incident_PatientFalling_WhereFallOccur_List , @Incident_PatientDetail_VisitNumber , @Incident_PatientDetail_Name , @Incident_Pharmacy_Initials , @Incident_Pharmacy_StaffInvolved_List , @Incident_Pharmacy_Checking_List , @Incident_Pharmacy_LocumOrPermanent , @Incident_Pharmacy_StaffOnDuty_List , @Incident_Pharmacy_ChangeInWorkProcedure , @Incident_Pharmacy_TypeOfPrescription_List , @Incident_Pharmacy_NumberOfRxOnDay , @Incident_Pharmacy_NumberOfItemsDispensedOnDay , @Incident_Pharmacy_NumberOfRxDayBefore , @Incident_Pharmacy_NumberOfItemsDispensedDayBefore , @Incident_Pharmacy_DrugPrescribed , @Incident_Pharmacy_DrugDispensed , @Incident_Pharmacy_DrugPacked , @Incident_Pharmacy_StrengthDrugPrescribed , @Incident_Pharmacy_StrengthDrugDispensed , @Incident_Pharmacy_DrugPrescribedNewOnMarket , @Incident_Pharmacy_LegislativeInformationOnPrescription , @Incident_Pharmacy_LegislativeInformationNotOnPrescription , @Incident_Pharmacy_DoctorName , @Incident_Pharmacy_Factors_List , @Incident_Pharmacy_SystemRelatedIssues_List , @Incident_Pharmacy_ErgonomicProblems_List , @Incident_Pharmacy_PatientCounselled , @Incident_Pharmacy_SimilarIncident , @Incident_Pharmacy_Location_List , @Incident_Pharmacy_PatientOutcomeAffected , @Incident_Reportable_CEO_AcknowledgedHM , @Incident_Reportable_CEO_DoctorRelated , @Incident_Reportable_CEO_CEONotifiedWithin24Hours , @Incident_Reportable_CEO_ProgressUpdateSent , @Incident_Reportable_CEO_ActionsTakenHM ,@Incident_Reportable_CEO_Date ,@Incident_Reportable_CEO_ActionsAgainstEmployee ,@Incident_Reportable_CEO_EmployeeNumber ,@Incident_Reportable_CEO_EmployeeName , @Incident_Reportable_CEO_Outcome , @Incident_Reportable_CEO_FileScanned , @Incident_Reportable_CEO_CloseOffHM , @Incident_Reportable_CEO_CloseOutEmailSend , @Incident_Reportable_SAPS_PoliceStation , @Incident_Reportable_SAPS_InvestigationOfficersName , @Incident_Reportable_SAPS_TelephoneNumber , @Incident_Reportable_SAPS_CaseNumber , @Incident_Reportable_InternalAudit_DateDetected , @Incident_Reportable_InternalAudit_ByWhom , @Incident_Reportable_InternalAudit_TotalLossValue , @Incident_Reportable_InternalAudit_TotalRecovery , @Incident_Reportable_InternalAudit_RecoveryPlan , @Incident_Reportable_InternalAudit_StatusOfInvestigation , @Incident_Reportable_InternalAudit_SAPSNotReported , @Incident_DegreeOfHarm_DegreeOfHarm_List , @Incident_DegreeOfHarm_Cost , @Incident_DegreeOfHarm_Implications , @Incident_RootCategory_List ,@Incident_RootDescription ,@Incident_Discipline_List ,@Incident_Investigator ,@Incident_InvestigatorContactNumber ,@Incident_InvestigatorDesignation ,@Incident_InvestigationCompleted ,@Incident_InvestigationCompletedDate ,@Incident_Status ,@Incident_StatusDate ,@Incident_CreatedDate ,@Incident_CreatedBy ,@Incident_ModifiedDate ,@Incident_ModifiedBy ,@Incident_History , @Incident_Archived ); SELECT @Incident_Id = SCOPE_IDENTITY()";
      SqlDataSource_Incident_Form.SelectCommand = "SELECT * FROM [Form_Incident] WHERE ([Incident_Id] = @Incident_Id)";
      SqlDataSource_Incident_Form.UpdateCommand = "UPDATE [Form_Incident] SET Incident_FacilityFrom_Facility = @Incident_FacilityFrom_Facility , Incident_Date = @Incident_Date ,Incident_Time_Hours = @Incident_Time_Hours ,Incident_Time_Min = @Incident_Time_Min ,Incident_ReportingPerson = @Incident_ReportingPerson ,Incident_UnitFrom_Unit = @Incident_UnitFrom_Unit ,Incident_UnitTo_Unit = @Incident_UnitTo_Unit ,Incident_IncidentCategory_List = @Incident_IncidentCategory_List ,Incident_CVR_Name = @Incident_CVR_Name ,Incident_CVR_ContactNumber = @Incident_CVR_ContactNumber ,Incident_E_EmployeeNumber = @Incident_E_EmployeeNumber ,Incident_E_EmployeeName = @Incident_E_EmployeeName ,Incident_E_EmployeeUnit_Unit = @Incident_E_EmployeeUnit_Unit ,Incident_E_EmployeeStatus_List = @Incident_E_EmployeeStatus_List ,Incident_E_StaffCategory_List = @Incident_E_StaffCategory_List ,Incident_E_BodyPartAffected_List = @Incident_E_BodyPartAffected_List , Incident_E_TreatmentRequired_List = @Incident_E_TreatmentRequired_List ,Incident_E_DaysOff = @Incident_E_DaysOff , Incident_E_MainContributor_List = @Incident_E_MainContributor_List , Incident_E_MainContributor_Staff_List = @Incident_E_MainContributor_Staff_List ,Incident_MMDT_Name = @Incident_MMDT_Name ,Incident_MMDT_ContactNumber = @Incident_MMDT_ContactNumber ,Incident_MMDT_Discipline_List = @Incident_MMDT_Discipline_List ,Incident_P_VisitNumber = @Incident_P_VisitNumber ,Incident_P_Name = @Incident_P_Name , Incident_P_MainContributor_List = @Incident_P_MainContributor_List , Incident_P_MainContributor_Staff_List = @Incident_P_MainContributor_Staff_List , Incident_Prop_MainContributor_List = @Incident_Prop_MainContributor_List , Incident_Prop_MainContributor_Staff_List = @Incident_Prop_MainContributor_Staff_List , Incident_SS_Name = @Incident_SS_Name ,Incident_SS_ContactNumber = @Incident_SS_ContactNumber ,Incident_Description = @Incident_Description ,Incident_Level1_List = @Incident_Level1_List ,Incident_Level2_List = @Incident_Level2_List ,Incident_Level3_List = @Incident_Level3_List ,Incident_Severity_List = @Incident_Severity_List ,Incident_Reportable = @Incident_Reportable ,Incident_Report_COID = @Incident_Report_COID ,Incident_Report_COID_Date = @Incident_Report_COID_Date ,Incident_Report_COID_Number = @Incident_Report_COID_Number ,Incident_Report_DEAT = @Incident_Report_DEAT ,Incident_Report_DEAT_Date = @Incident_Report_DEAT_Date ,Incident_Report_DEAT_Number = @Incident_Report_DEAT_Number ,Incident_Report_DepartmentOfHealth = @Incident_Report_DepartmentOfHealth ,Incident_Report_DepartmentOfHealth_Date = @Incident_Report_DepartmentOfHealth_Date ,Incident_Report_DepartmentOfHealth_Number = @Incident_Report_DepartmentOfHealth_Number ,Incident_Report_DepartmentOfLabour = @Incident_Report_DepartmentOfLabour ,Incident_Report_DepartmentOfLabour_Date = @Incident_Report_DepartmentOfLabour_Date ,Incident_Report_DepartmentOfLabour_Number = @Incident_Report_DepartmentOfLabour_Number ,Incident_Report_HospitalManager = @Incident_Report_HospitalManager ,Incident_Report_HospitalManager_Date = @Incident_Report_HospitalManager_Date ,Incident_Report_HospitalManager_Number = @Incident_Report_HospitalManager_Number ,Incident_Report_HPCSA = @Incident_Report_HPCSA ,Incident_Report_HPCSA_Date = @Incident_Report_HPCSA_Date ,Incident_Report_HPCSA_Number = @Incident_Report_HPCSA_Number ,Incident_Report_LegalDepartment = @Incident_Report_LegalDepartment ,Incident_Report_LegalDepartment_Date = @Incident_Report_LegalDepartment_Date ,Incident_Report_LegalDepartment_Number = @Incident_Report_LegalDepartment_Number ,Incident_Report_CEO = @Incident_Report_CEO ,Incident_Report_CEO_Date = @Incident_Report_CEO_Date ,Incident_Report_CEO_Number = @Incident_Report_CEO_Number ,Incident_Report_PharmacyCouncil = @Incident_Report_PharmacyCouncil ,Incident_Report_PharmacyCouncil_Date = @Incident_Report_PharmacyCouncil_Date ,Incident_Report_PharmacyCouncil_Number = @Incident_Report_PharmacyCouncil_Number ,Incident_Report_Quality = @Incident_Report_Quality ,Incident_Report_Quality_Date = @Incident_Report_Quality_Date ,Incident_Report_Quality_Number = @Incident_Report_Quality_Number ,Incident_Report_RM = @Incident_Report_RM ,Incident_Report_RM_Date = @Incident_Report_RM_Date ,Incident_Report_RM_Number = @Incident_Report_RM_Number ,Incident_Report_SANC = @Incident_Report_SANC ,Incident_Report_SANC_Date = @Incident_Report_SANC_Date ,Incident_Report_SANC_Number = @Incident_Report_SANC_Number ,Incident_Report_SAPS = @Incident_Report_SAPS ,Incident_Report_SAPS_Date = @Incident_Report_SAPS_Date ,Incident_Report_SAPS_Number = @Incident_Report_SAPS_Number ,Incident_Report_InternalAudit = @Incident_Report_InternalAudit ,Incident_Report_InternalAudit_Date = @Incident_Report_InternalAudit_Date ,Incident_Report_InternalAudit_Number = @Incident_Report_InternalAudit_Number , Incident_PatientFalling_WhereFallOccur_List = @Incident_PatientFalling_WhereFallOccur_List , Incident_PatientDetail_VisitNumber = @Incident_PatientDetail_VisitNumber , Incident_PatientDetail_Name = @Incident_PatientDetail_Name , Incident_Pharmacy_Initials = @Incident_Pharmacy_Initials , Incident_Pharmacy_StaffInvolved_List = @Incident_Pharmacy_StaffInvolved_List , Incident_Pharmacy_Checking_List = @Incident_Pharmacy_Checking_List , Incident_Pharmacy_LocumOrPermanent = @Incident_Pharmacy_LocumOrPermanent , Incident_Pharmacy_StaffOnDuty_List = @Incident_Pharmacy_StaffOnDuty_List , Incident_Pharmacy_ChangeInWorkProcedure = @Incident_Pharmacy_ChangeInWorkProcedure , Incident_Pharmacy_TypeOfPrescription_List = @Incident_Pharmacy_TypeOfPrescription_List , Incident_Pharmacy_NumberOfRxOnDay = @Incident_Pharmacy_NumberOfRxOnDay , Incident_Pharmacy_NumberOfItemsDispensedOnDay = @Incident_Pharmacy_NumberOfItemsDispensedOnDay , Incident_Pharmacy_NumberOfRxDayBefore = @Incident_Pharmacy_NumberOfRxDayBefore , Incident_Pharmacy_NumberOfItemsDispensedDayBefore = @Incident_Pharmacy_NumberOfItemsDispensedDayBefore , Incident_Pharmacy_DrugPrescribed = @Incident_Pharmacy_DrugPrescribed , Incident_Pharmacy_DrugDispensed = @Incident_Pharmacy_DrugDispensed , Incident_Pharmacy_DrugPacked = @Incident_Pharmacy_DrugPacked , Incident_Pharmacy_StrengthDrugPrescribed = @Incident_Pharmacy_StrengthDrugPrescribed , Incident_Pharmacy_StrengthDrugDispensed = @Incident_Pharmacy_StrengthDrugDispensed , Incident_Pharmacy_DrugPrescribedNewOnMarket = @Incident_Pharmacy_DrugPrescribedNewOnMarket , Incident_Pharmacy_LegislativeInformationOnPrescription = @Incident_Pharmacy_LegislativeInformationOnPrescription , Incident_Pharmacy_LegislativeInformationNotOnPrescription = @Incident_Pharmacy_LegislativeInformationNotOnPrescription , Incident_Pharmacy_DoctorName = @Incident_Pharmacy_DoctorName , Incident_Pharmacy_Factors_List = @Incident_Pharmacy_Factors_List , Incident_Pharmacy_SystemRelatedIssues_List = @Incident_Pharmacy_SystemRelatedIssues_List , Incident_Pharmacy_ErgonomicProblems_List = @Incident_Pharmacy_ErgonomicProblems_List , Incident_Pharmacy_PatientCounselled = @Incident_Pharmacy_PatientCounselled , Incident_Pharmacy_SimilarIncident = @Incident_Pharmacy_SimilarIncident , Incident_Pharmacy_Location_List = @Incident_Pharmacy_Location_List , Incident_Pharmacy_PatientOutcomeAffected = @Incident_Pharmacy_PatientOutcomeAffected , Incident_Reportable_CEO_AcknowledgedHM = @Incident_Reportable_CEO_AcknowledgedHM , Incident_Reportable_CEO_DoctorRelated = @Incident_Reportable_CEO_DoctorRelated , Incident_Reportable_CEO_CEONotifiedWithin24Hours = @Incident_Reportable_CEO_CEONotifiedWithin24Hours , Incident_Reportable_CEO_ProgressUpdateSent = @Incident_Reportable_CEO_ProgressUpdateSent ,Incident_Reportable_CEO_ActionsTakenHM = @Incident_Reportable_CEO_ActionsTakenHM ,Incident_Reportable_CEO_Date = @Incident_Reportable_CEO_Date ,Incident_Reportable_CEO_ActionsAgainstEmployee = @Incident_Reportable_CEO_ActionsAgainstEmployee ,Incident_Reportable_CEO_EmployeeNumber = @Incident_Reportable_CEO_EmployeeNumber ,Incident_Reportable_CEO_EmployeeName = @Incident_Reportable_CEO_EmployeeName , Incident_Reportable_CEO_Outcome = @Incident_Reportable_CEO_Outcome ,Incident_Reportable_CEO_FileScanned = @Incident_Reportable_CEO_FileScanned , Incident_Reportable_CEO_CloseOffHM = @Incident_Reportable_CEO_CloseOffHM , Incident_Reportable_CEO_CloseOutEmailSend = @Incident_Reportable_CEO_CloseOutEmailSend ,Incident_Reportable_SAPS_PoliceStation = @Incident_Reportable_SAPS_PoliceStation ,Incident_Reportable_SAPS_InvestigationOfficersName = @Incident_Reportable_SAPS_InvestigationOfficersName ,Incident_Reportable_SAPS_TelephoneNumber = @Incident_Reportable_SAPS_TelephoneNumber ,Incident_Reportable_SAPS_CaseNumber = @Incident_Reportable_SAPS_CaseNumber ,Incident_Reportable_InternalAudit_DateDetected = @Incident_Reportable_InternalAudit_DateDetected ,Incident_Reportable_InternalAudit_ByWhom = @Incident_Reportable_InternalAudit_ByWhom ,Incident_Reportable_InternalAudit_TotalLossValue = @Incident_Reportable_InternalAudit_TotalLossValue ,Incident_Reportable_InternalAudit_TotalRecovery = @Incident_Reportable_InternalAudit_TotalRecovery ,Incident_Reportable_InternalAudit_RecoveryPlan = @Incident_Reportable_InternalAudit_RecoveryPlan ,Incident_Reportable_InternalAudit_StatusOfInvestigation = @Incident_Reportable_InternalAudit_StatusOfInvestigation ,Incident_Reportable_InternalAudit_SAPSNotReported = @Incident_Reportable_InternalAudit_SAPSNotReported , Incident_DegreeOfHarm_DegreeOfHarm_List = @Incident_DegreeOfHarm_DegreeOfHarm_List , Incident_DegreeOfHarm_Cost = @Incident_DegreeOfHarm_Cost , Incident_DegreeOfHarm_Implications = @Incident_DegreeOfHarm_Implications , Incident_RootCategory_List = @Incident_RootCategory_List ,Incident_RootDescription = @Incident_RootDescription ,Incident_Discipline_List = @Incident_Discipline_List ,Incident_Investigator = @Incident_Investigator ,Incident_InvestigatorContactNumber = @Incident_InvestigatorContactNumber ,Incident_InvestigatorDesignation = @Incident_InvestigatorDesignation ,Incident_InvestigationCompleted = @Incident_InvestigationCompleted ,Incident_InvestigationCompletedDate = @Incident_InvestigationCompletedDate ,Incident_Status = @Incident_Status ,Incident_StatusDate = @Incident_StatusDate ,Incident_StatusRejectedReason = @Incident_StatusRejectedReason ,Incident_ModifiedDate = @Incident_ModifiedDate ,Incident_ModifiedBy = @Incident_ModifiedBy ,Incident_History = @Incident_History WHERE [Incident_Id] = @Incident_Id";
      SqlDataSource_Incident_Form.InsertParameters.Clear();
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Id", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters["Incident_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_Incident_Form.InsertParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_ReportNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_FacilityFrom_Facility", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Time_Hours", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Time_Min", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_ReportingPerson", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_UnitFrom_Unit", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_UnitTo_Unit", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_IncidentCategory_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_CVR_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_CVR_ContactNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_EmployeeNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_EmployeeName", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_EmployeeUnit_Unit", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_EmployeeStatus_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_StaffCategory_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_BodyPartAffected_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_TreatmentRequired_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_DaysOff", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_MainContributor_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_E_MainContributor_Staff_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_MMDT_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_MMDT_ContactNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_MMDT_Discipline_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_P_VisitNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_P_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_P_MainContributor_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_P_MainContributor_Staff_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Prop_MainContributor_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Prop_MainContributor_Staff_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_SS_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_SS_ContactNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Description", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Level1_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Level2_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Level3_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Severity_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_COID", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_COID_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_COID_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_DEAT", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_DEAT_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_DEAT_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_DepartmentOfHealth", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_DepartmentOfHealth_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_DepartmentOfHealth_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_DepartmentOfLabour", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_DepartmentOfLabour_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_DepartmentOfLabour_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_HospitalManager", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_HospitalManager_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_HospitalManager_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_HPCSA", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_HPCSA_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_HPCSA_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_LegalDepartment", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_LegalDepartment_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_LegalDepartment_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_CEO", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_CEO_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_CEO_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_PharmacyCouncil", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_PharmacyCouncil_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_PharmacyCouncil_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_Quality", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_Quality_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_Quality_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_RM", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_RM_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_RM_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_SANC", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_SANC_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_SANC_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_SAPS", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_SAPS_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_SAPS_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_InternalAudit", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_InternalAudit_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Report_InternalAudit_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_PatientFalling_WhereFallOccur_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_PatientDetail_VisitNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_PatientDetail_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_Initials", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_StaffInvolved_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_Checking_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_LocumOrPermanent", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_StaffOnDuty_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_ChangeInWorkProcedure", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_TypeOfPrescription_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_NumberOfRxOnDay", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_NumberOfItemsDispensedOnDay", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_NumberOfRxDayBefore", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_NumberOfItemsDispensedDayBefore", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_DrugPrescribed", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_DrugDispensed", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_DrugPacked", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_StrengthDrugPrescribed", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_StrengthDrugDispensed", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_DrugPrescribedNewOnMarket", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_LegislativeInformationOnPrescription", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_LegislativeInformationNotOnPrescription", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_DoctorName", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_Factors_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_SystemRelatedIssues_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_ErgonomicProblems_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_PatientCounselled", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_SimilarIncident", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_Location_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Pharmacy_PatientOutcomeAffected", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_AcknowledgedHM", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_DoctorRelated", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_CEONotifiedWithin24Hours", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_ProgressUpdateSent", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_ActionsTakenHM", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_ActionsAgainstEmployee", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_EmployeeNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_EmployeeName", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_Outcome", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_FileScanned", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_CloseOffHM", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_CEO_CloseOutEmailSend", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_SAPS_PoliceStation", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_SAPS_InvestigationOfficersName", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_SAPS_TelephoneNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_SAPS_CaseNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_InternalAudit_DateDetected", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_InternalAudit_ByWhom", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_InternalAudit_TotalLossValue", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_InternalAudit_TotalRecovery", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_InternalAudit_RecoveryPlan", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_InternalAudit_StatusOfInvestigation", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Reportable_InternalAudit_SAPSNotReported", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_DegreeOfHarm_DegreeOfHarm_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_DegreeOfHarm_Cost", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_DegreeOfHarm_Implications", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_RootCategory_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_RootDescription", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Discipline_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Investigator", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_InvestigatorContactNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_InvestigatorDesignation", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_InvestigationCompleted", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_InvestigationCompletedDate", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Status", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_CreatedBy", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_History", TypeCode.String, "");
      SqlDataSource_Incident_Form.InsertParameters["Incident_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Incident_Form.InsertParameters.Add("Incident_Archived", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.SelectParameters.Clear();
      SqlDataSource_Incident_Form.SelectParameters.Add("Incident_Id", TypeCode.Int32, Request.QueryString["Incident_Id"]);
      SqlDataSource_Incident_Form.UpdateParameters.Clear();
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_FacilityFrom_Facility", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Time_Hours", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Time_Min", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_ReportingPerson", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_UnitFrom_Unit", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_UnitTo_Unit", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_IncidentCategory_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_CVR_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_CVR_ContactNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_EmployeeNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_EmployeeName", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_EmployeeUnit_Unit", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_EmployeeStatus_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_StaffCategory_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_BodyPartAffected_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_TreatmentRequired_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_DaysOff", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_MainContributor_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_E_MainContributor_Staff_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_MMDT_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_MMDT_ContactNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_MMDT_Discipline_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_P_VisitNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_P_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_P_MainContributor_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_P_MainContributor_Staff_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Prop_MainContributor_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Prop_MainContributor_Staff_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_SS_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_SS_ContactNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Description", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Level1_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Level2_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Level3_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Severity_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_COID", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_COID_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_COID_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_DEAT", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_DEAT_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_DEAT_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_DepartmentOfHealth", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_DepartmentOfHealth_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_DepartmentOfHealth_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_DepartmentOfLabour", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_DepartmentOfLabour_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_DepartmentOfLabour_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_HospitalManager", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_HospitalManager_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_HospitalManager_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_HPCSA", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_HPCSA_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_HPCSA_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_LegalDepartment", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_LegalDepartment_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_LegalDepartment_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_CEO", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_CEO_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_CEO_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_PharmacyCouncil", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_PharmacyCouncil_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_PharmacyCouncil_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_Quality", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_Quality_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_Quality_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_RM", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_RM_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_RM_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_SANC", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_SANC_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_SANC_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_SAPS", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_SAPS_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_SAPS_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_InternalAudit", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_InternalAudit_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Report_InternalAudit_Number", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_PatientFalling_WhereFallOccur_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_PatientDetail_VisitNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_PatientDetail_Name", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_Initials", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_StaffInvolved_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_Checking_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_LocumOrPermanent", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_StaffOnDuty_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_ChangeInWorkProcedure", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_TypeOfPrescription_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_NumberOfRxOnDay", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_NumberOfItemsDispensedOnDay", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_NumberOfRxDayBefore", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_NumberOfItemsDispensedDayBefore", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_DrugPrescribed", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_DrugDispensed", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_DrugPacked", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_StrengthDrugPrescribed", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_StrengthDrugDispensed", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_DrugPrescribedNewOnMarket", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_LegislativeInformationOnPrescription", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_LegislativeInformationNotOnPrescription", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_DoctorName", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_Factors_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_SystemRelatedIssues_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_ErgonomicProblems_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_PatientCounselled", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_SimilarIncident", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_Location_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Pharmacy_PatientOutcomeAffected", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_AcknowledgedHM", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_DoctorRelated", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_CEONotifiedWithin24Hours", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_ProgressUpdateSent", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_ActionsTakenHM", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_Date", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_ActionsAgainstEmployee", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_EmployeeNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_EmployeeName", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_Outcome", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_FileScanned", TypeCode.Boolean, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_CloseOffHM", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_CEO_CloseOutEmailSend", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_SAPS_PoliceStation", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_SAPS_InvestigationOfficersName", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_SAPS_TelephoneNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_SAPS_CaseNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_InternalAudit_DateDetected", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_InternalAudit_ByWhom", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_InternalAudit_TotalLossValue", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_InternalAudit_TotalRecovery", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_InternalAudit_RecoveryPlan", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_InternalAudit_StatusOfInvestigation", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Reportable_InternalAudit_SAPSNotReported", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_DegreeOfHarm_DegreeOfHarm_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_DegreeOfHarm_Cost", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_DegreeOfHarm_Implications", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_RootCategory_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_RootDescription", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Discipline_List", TypeCode.Int32, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Investigator", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_InvestigatorContactNumber", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_InvestigatorDesignation", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_InvestigationCompleted", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_InvestigationCompletedDate", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Status", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_StatusRejectedReason", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_History", TypeCode.String, "");
      SqlDataSource_Incident_Form.UpdateParameters.Add("Incident_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Insert()
    {
      SqlDataSource_Incident_InsertFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertFacility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Incident_InsertFacility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertFacility.SelectParameters.Clear();
      SqlDataSource_Incident_InsertFacility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_InsertFacility.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertFacility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Incident_InsertFacility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertFacility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertFacility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertFacilityFrom.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertFacilityFrom.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Incident_InsertFacilityFrom.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertFacilityFrom.SelectParameters.Clear();
      SqlDataSource_Incident_InsertFacilityFrom.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_InsertFacilityFrom.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertFacilityFrom.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Incident_InsertFacilityFrom.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertFacilityFrom.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertFacilityFrom.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertUnitFromUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertUnitFromUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Incident_InsertUnitFromUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertUnitFromUnit.SelectParameters.Clear();
      SqlDataSource_Incident_InsertUnitFromUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_InsertUnitFromUnit.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertUnitFromUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Incident_InsertUnitFromUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertUnitFromUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertUnitFromUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertUnitToUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertUnitToUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Incident_InsertUnitToUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertUnitToUnit.SelectParameters.Clear();
      SqlDataSource_Incident_InsertUnitToUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_InsertUnitToUnit.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertUnitToUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Incident_InsertUnitToUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertUnitToUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertUnitToUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertIncidentCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertIncidentCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertIncidentCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertIncidentCategoryList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertIncidentCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertIncidentCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "4");
      SqlDataSource_Incident_InsertIncidentCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertIncidentCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertIncidentCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertIncidentCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSourceSetup_Insert_IncidentCategory();

      SqlDataSource_Incident_InsertLevel1List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertLevel1List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertLevel1List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertLevel1List.SelectParameters.Clear();
      SqlDataSource_Incident_InsertLevel1List.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertLevel1List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "5");
      SqlDataSource_Incident_InsertLevel1List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertLevel1List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertLevel1List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertLevel1List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertLevel2List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertLevel2List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertLevel2List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertLevel2List.SelectParameters.Clear();
      SqlDataSource_Incident_InsertLevel2List.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertLevel2List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "6");
      SqlDataSource_Incident_InsertLevel2List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertLevel2List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertLevel2List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertLevel2List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertLevel3List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertLevel3List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertLevel3List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertLevel3List.SelectParameters.Clear();
      SqlDataSource_Incident_InsertLevel3List.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertLevel3List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "7");
      SqlDataSource_Incident_InsertLevel3List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertLevel3List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertLevel3List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertLevel3List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertSeverityList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertSeverityList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertSeverityList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertSeverityList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertSeverityList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertSeverityList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "60");
      SqlDataSource_Incident_InsertSeverityList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertSeverityList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertSeverityList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertSeverityList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "231");
      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPatientFallingWhereFallOccurList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "160");
      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertDegreeOfHarmImpactItemList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSourceSetup_Insert_Pharmacy();

      SqlDataSource_Incident_InsertRootCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertRootCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertRootCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertRootCategoryList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertRootCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertRootCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "11");
      SqlDataSource_Incident_InsertRootCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertRootCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertRootCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertRootCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertDisciplineList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertDisciplineList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertDisciplineList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertDisciplineList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertDisciplineList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertDisciplineList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "118");
      SqlDataSource_Incident_InsertDisciplineList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertDisciplineList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertDisciplineList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertDisciplineList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
    }

    private void SqlDataSourceSetup_Insert_IncidentCategory()
    {
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.SelectParameters.Clear();
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEEmployeeUnitUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertEEmployeeStatusList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertEEmployeeStatusList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertEEmployeeStatusList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertEEmployeeStatusList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertEEmployeeStatusList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertEEmployeeStatusList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "59");
      SqlDataSource_Incident_InsertEEmployeeStatusList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertEEmployeeStatusList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEEmployeeStatusList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEEmployeeStatusList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertEStaffCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertEStaffCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertEStaffCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertEStaffCategoryList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertEStaffCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertEStaffCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "8");
      SqlDataSource_Incident_InsertEStaffCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertEStaffCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEStaffCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEStaffCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertEBodyPartAffectedList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertEBodyPartAffectedList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertEBodyPartAffectedList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertEBodyPartAffectedList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertEBodyPartAffectedList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertEBodyPartAffectedList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "9");
      SqlDataSource_Incident_InsertEBodyPartAffectedList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertEBodyPartAffectedList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEBodyPartAffectedList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEBodyPartAffectedList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertETreatmentRequiredList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertETreatmentRequiredList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertETreatmentRequiredList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertETreatmentRequiredList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertETreatmentRequiredList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertETreatmentRequiredList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "192");
      SqlDataSource_Incident_InsertETreatmentRequiredList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertETreatmentRequiredList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertETreatmentRequiredList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertETreatmentRequiredList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertEMainContributorList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertEMainContributorList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertEMainContributorList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertEMainContributorList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertEMainContributorList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertEMainContributorList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "225");
      SqlDataSource_Incident_InsertEMainContributorList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertEMainContributorList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEMainContributorList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEMainContributorList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertEMainContributorStaffList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertEMainContributorStaffList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertEMainContributorStaffList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertEMainContributorStaffList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertEMainContributorStaffList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertEMainContributorStaffList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "226");
      SqlDataSource_Incident_InsertEMainContributorStaffList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertEMainContributorStaffList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEMainContributorStaffList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertEMainContributorStaffList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertMMDTDisciplineList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertMMDTDisciplineList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertMMDTDisciplineList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertMMDTDisciplineList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertMMDTDisciplineList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertMMDTDisciplineList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "10");
      SqlDataSource_Incident_InsertMMDTDisciplineList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertMMDTDisciplineList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertMMDTDisciplineList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertMMDTDisciplineList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPMainContributorList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPMainContributorList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPMainContributorList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPMainContributorList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPMainContributorList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPMainContributorList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "225");
      SqlDataSource_Incident_InsertPMainContributorList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPMainContributorList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPMainContributorList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPMainContributorList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPMainContributorStaffList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPMainContributorStaffList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPMainContributorStaffList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPMainContributorStaffList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPMainContributorStaffList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPMainContributorStaffList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "226");
      SqlDataSource_Incident_InsertPMainContributorStaffList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPMainContributorStaffList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPMainContributorStaffList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPMainContributorStaffList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPropMainContributorList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPropMainContributorList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPropMainContributorList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPropMainContributorList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPropMainContributorList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPropMainContributorList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "225");
      SqlDataSource_Incident_InsertPropMainContributorList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPropMainContributorList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPropMainContributorList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPropMainContributorList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPropMainContributorStaffList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPropMainContributorStaffList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPropMainContributorStaffList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPropMainContributorStaffList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPropMainContributorStaffList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPropMainContributorStaffList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "226");
      SqlDataSource_Incident_InsertPropMainContributorStaffList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPropMainContributorStaffList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPropMainContributorStaffList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPropMainContributorStaffList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
    }

    private void SqlDataSourceSetup_Insert_Pharmacy()
    {
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "198");
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyStaffInvolvedList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPharmacyCheckingList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPharmacyCheckingList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPharmacyCheckingList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPharmacyCheckingList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPharmacyCheckingList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPharmacyCheckingList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "199");
      SqlDataSource_Incident_InsertPharmacyCheckingList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPharmacyCheckingList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyCheckingList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyCheckingList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "200");
      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyStaffOnDutyList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "201");
      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyTypeOfPrescriptionList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPharmacyFactorsList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPharmacyFactorsList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPharmacyFactorsList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPharmacyFactorsList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPharmacyFactorsList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPharmacyFactorsList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "202");
      SqlDataSource_Incident_InsertPharmacyFactorsList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPharmacyFactorsList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyFactorsList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyFactorsList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "203");
      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacySystemRelatedIssuesList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "204");
      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyErgonomicProblemsList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_InsertPharmacyLocationList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_InsertPharmacyLocationList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_InsertPharmacyLocationList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_InsertPharmacyLocationList.SelectParameters.Clear();
      SqlDataSource_Incident_InsertPharmacyLocationList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_InsertPharmacyLocationList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "205");
      SqlDataSource_Incident_InsertPharmacyLocationList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_InsertPharmacyLocationList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyLocationList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_InsertPharmacyLocationList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
    }

    private void SqlDataSourceSetup_Edit()
    {
      SqlDataSource_Incident_EditFacilityFrom.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditFacilityFrom.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Incident_EditFacilityFrom.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditFacilityFrom.SelectParameters.Clear();
      SqlDataSource_Incident_EditFacilityFrom.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, "");
      SqlDataSource_Incident_EditFacilityFrom.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditFacilityFrom.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Incident_EditFacilityFrom.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditFacilityFrom.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditFacilityFrom.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditUnitFromUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditUnitFromUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Incident_EditUnitFromUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditUnitFromUnit.SelectParameters.Clear();
      SqlDataSource_Incident_EditUnitFromUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_EditUnitFromUnit.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditUnitFromUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Incident_EditUnitFromUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditUnitFromUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditUnitFromUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditUnitToUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditUnitToUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Incident_EditUnitToUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditUnitToUnit.SelectParameters.Clear();
      SqlDataSource_Incident_EditUnitToUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_EditUnitToUnit.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditUnitToUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Incident_EditUnitToUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditUnitToUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditUnitToUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditIncidentCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditIncidentCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditIncidentCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters.Clear();
      SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "4");
      SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditIncidentCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSourceSetup_Edit_IncidentCategory();

      SqlDataSource_Incident_EditLevel1List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditLevel1List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditLevel1List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditLevel1List.SelectParameters.Clear();
      SqlDataSource_Incident_EditLevel1List.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditLevel1List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "5");
      SqlDataSource_Incident_EditLevel1List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditLevel1List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditLevel1List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditLevel1List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditLevel2List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditLevel2List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditLevel2List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditLevel2List.SelectParameters.Clear();
      SqlDataSource_Incident_EditLevel2List.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditLevel2List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "6");
      SqlDataSource_Incident_EditLevel2List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditLevel2List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditLevel2List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditLevel2List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditLevel3List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditLevel3List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditLevel3List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditLevel3List.SelectParameters.Clear();
      SqlDataSource_Incident_EditLevel3List.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditLevel3List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "7");
      SqlDataSource_Incident_EditLevel3List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditLevel3List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditLevel3List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditLevel3List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditSeverityList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditSeverityList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditSeverityList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditSeverityList.SelectParameters.Clear();
      SqlDataSource_Incident_EditSeverityList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditSeverityList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "60");
      SqlDataSource_Incident_EditSeverityList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditSeverityList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditSeverityList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditSeverityList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "231");
      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPatientFallingWhereFallOccurList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters.Clear();
      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "160");
      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditDegreeOfHarmImpactItemList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditDegreeOfHarmImpact.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditDegreeOfHarmImpact.SelectCommand = "SELECT DISTINCT Incident_DegreeOfHarm_Impact_Impact_Name FROM vForm_Incident_DegreeOfHarm_Impact WHERE Incident_Id = @Incident_Id ORDER BY Incident_DegreeOfHarm_Impact_Impact_Name";
      SqlDataSource_Incident_EditDegreeOfHarmImpact.SelectParameters.Clear();
      SqlDataSource_Incident_EditDegreeOfHarmImpact.SelectParameters.Add("Incident_Id", Request.QueryString["Incident_Id"]);

      SqlDataSourceSetup_Edit_Pharmacy();

      SqlDataSource_Incident_EditRootCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditRootCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditRootCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditRootCategoryList.SelectParameters.Clear();
      SqlDataSource_Incident_EditRootCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditRootCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "11");
      SqlDataSource_Incident_EditRootCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditRootCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditRootCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditRootCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditDisciplineList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditDisciplineList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditDisciplineList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditDisciplineList.SelectParameters.Clear();
      SqlDataSource_Incident_EditDisciplineList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditDisciplineList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "118");
      SqlDataSource_Incident_EditDisciplineList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditDisciplineList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditDisciplineList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditDisciplineList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
    }

    private void SqlDataSourceSetup_Edit_IncidentCategory()
    {
      SqlDataSource_Incident_EditEEmployeeUnitUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters.Clear();
      SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditEEmployeeUnitUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditEEmployeeStatusList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditEEmployeeStatusList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditEEmployeeStatusList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters.Clear();
      SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "59");
      SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditEEmployeeStatusList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditEStaffCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditEStaffCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditEStaffCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters.Clear();
      SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "8");
      SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditEStaffCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditEBodyPartAffectedList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditEBodyPartAffectedList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditEBodyPartAffectedList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters.Clear();
      SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "9");
      SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditEBodyPartAffectedList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditETreatmentRequiredList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditETreatmentRequiredList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditETreatmentRequiredList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters.Clear();
      SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "192");
      SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditETreatmentRequiredList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditEMainContributorList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditEMainContributorList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditEMainContributorList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditEMainContributorList.SelectParameters.Clear();
      SqlDataSource_Incident_EditEMainContributorList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditEMainContributorList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "225");
      SqlDataSource_Incident_EditEMainContributorList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditEMainContributorList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditEMainContributorList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditEMainContributorList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditEMainContributorStaffList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditEMainContributorStaffList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditEMainContributorStaffList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters.Clear();
      SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "226");
      SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditEMainContributorStaffList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditMMDTDisciplineList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditMMDTDisciplineList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditMMDTDisciplineList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters.Clear();
      SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "10");
      SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditMMDTDisciplineList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPMainContributorList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPMainContributorList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPMainContributorList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPMainContributorList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPMainContributorList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPMainContributorList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "225");
      SqlDataSource_Incident_EditPMainContributorList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPMainContributorList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPMainContributorList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPMainContributorList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPMainContributorStaffList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPMainContributorStaffList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPMainContributorStaffList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "226");
      SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPMainContributorStaffList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPropMainContributorList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPropMainContributorList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPropMainContributorList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPropMainContributorList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPropMainContributorList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPropMainContributorList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "225");
      SqlDataSource_Incident_EditPropMainContributorList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPropMainContributorList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPropMainContributorList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPropMainContributorList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPropMainContributorStaffList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPropMainContributorStaffList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPropMainContributorStaffList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "226");
      SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPropMainContributorStaffList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
    }

    private void SqlDataSourceSetup_Edit_Pharmacy()
    {
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "198");
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyStaffInvolvedList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPharmacyCheckingList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPharmacyCheckingList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPharmacyCheckingList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "199");
      SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyCheckingList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "200");
      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyStaffOnDutyList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "201");
      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyTypeOfPrescriptionList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPharmacyFactorsList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPharmacyFactorsList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPharmacyFactorsList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "202");
      SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyFactorsList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "203");
      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacySystemRelatedIssuesList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "204");
      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyErgonomicProblemsList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_EditPharmacyLocationList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_EditPharmacyLocationList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Incident_EditPharmacyLocationList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters.Clear();
      SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "205");
      SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_EditPharmacyLocationList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
    }

    private void SqlDataSourceSetup_Item()
    {
      SqlDataSource_Incident_ItemDegreeOfHarmImpact.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_ItemDegreeOfHarmImpact.SelectCommand = "SELECT DISTINCT Incident_DegreeOfHarm_Impact_Impact_Name FROM vForm_Incident_DegreeOfHarm_Impact WHERE Incident_Id = @Incident_Id ORDER BY Incident_DegreeOfHarm_Impact_Impact_Name";
      SqlDataSource_Incident_ItemDegreeOfHarmImpact.SelectParameters.Clear();
      SqlDataSource_Incident_ItemDegreeOfHarmImpact.SelectParameters.Add("Incident_Id", Request.QueryString["Incident_Id"]);
    }

    private void SetFormVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["Incident_Id"]))
      {
        SetFormVisibility_Insert();
      }
      else
      {
        SetFormVisibility_Edit();
      }
    }

    protected void SetFormVisibility_Insert()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('1')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '20'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '131'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '3'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '132'");
            DataRow[] SecurityFacilityPharmacyManager = DataTable_FormMode.Select("SecurityRole_Id = '189'");
            DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '2'");
            DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '70'");

            string Security = "1";
            if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityPharmacyManager.Length > 0 || SecurityFacilityApprover.Length > 0 || SecurityFacilityCapturer.Length > 0))
            {
              Security = "0";
              FormView_Incident_Form.ChangeMode(FormViewMode.Insert);
            }

            if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Security = "0";
              FormView_Incident_Form.ChangeMode(FormViewMode.Insert);
            }

            if (Security == "1")
            {
              Security = "0";
              FormView_Incident_Form.ChangeMode(FormViewMode.Insert);
            }
          }
          else
          {
            FormView_Incident_Form.ChangeMode(FormViewMode.Insert);
          }
        }
      }
    }

    protected void SetFormVisibility_Edit()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('1')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '20'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '131'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '3'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '132'");
            DataRow[] SecurityFacilityPharmacyManager = DataTable_FormMode.Select("SecurityRole_Id = '189'");
            DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '2'");
            DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '70'");

            Session["IncidentStatus"] = "";
            Session["IncidentInvestigationCompleted"] = "";
            string SQLStringIncident = "SELECT Incident_Status, Incident_InvestigationCompleted FROM Form_Incident WHERE Incident_Id = @Incident_Id";
            using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
            {
              SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
              DataTable DataTable_Incident;
              using (DataTable_Incident = new DataTable())
              {
                DataTable_Incident.Locale = CultureInfo.CurrentCulture;
                DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
                if (DataTable_Incident.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
                  {
                    Session["IncidentStatus"] = DataRow_Row["Incident_Status"];
                    Session["IncidentInvestigationCompleted"] = DataRow_Row["Incident_InvestigationCompleted"];
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              FormView_Incident_Form.ChangeMode(FormViewMode.Edit);
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityCapturer.Length > 0))
            {
              Session["Security"] = "0";
              FormView_Incident_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && (SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityPharmacyManager.Length > 0))
            {
              Session["Security"] = "0";
              if (Session["IncidentInvestigationCompleted"].ToString() == "False")
              {
                if (Session["IncidentStatus"].ToString() == "Rejected")
                {
                  FormView_Incident_Form.ChangeMode(FormViewMode.ReadOnly);
                }
                else
                {
                  if (SecurityFacilityAdminUpdate.Length > 0)
                  {
                    FormView_Incident_Form.ChangeMode(FormViewMode.Edit);
                  }
                  else if (SecurityFacilityPharmacyManager.Length > 0)
                  {
                    string TriggerValue = Pharmacy_TriggerLevel();

                    if (string.IsNullOrEmpty(TriggerValue))
                    {
                      FormView_Incident_Form.ChangeMode(FormViewMode.ReadOnly);
                    }
                    else
                    {
                      FormView_Incident_Form.ChangeMode(FormViewMode.Edit);
                    }
                  }
                }
              }
              else
              {
                FormView_Incident_Form.ChangeMode(FormViewMode.ReadOnly);
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityApprover.Length > 0)
            {
              Session["Security"] = "0";
              if (Session["IncidentStatus"].ToString() == "Pending Approval")
              {
                FormView_Incident_Form.ChangeMode(FormViewMode.Edit);
              }
              else
              {
                FormView_Incident_Form.ChangeMode(FormViewMode.ReadOnly);
              }
            }

            Session["Security"] = "1";

            Session.Remove("IncidentStatus");
            Session.Remove("IncidentInvestigationCompleted");
          }
          else
          {
            FormView_Incident_Form.ChangeMode(FormViewMode.ReadOnly);
          }
        }
      }
    }

    private void TableFormVisible()
    {
      Session["ListItem_Id"] = "";
      Session["ListItem_Name"] = "";
      string SQLStringIconInfo = "SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 96";
      using (SqlCommand SqlCommand_IconInfo = new SqlCommand(SQLStringIconInfo))
      {
        DataTable DataTable_IconInfo;
        using (DataTable_IconInfo = new DataTable())
        {
          DataTable_IconInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_IconInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IconInfo).Copy();
          if (DataTable_IconInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_IconInfo.Rows)
            {
              Session["ListItem_Id"] = DataRow_Row["ListItem_Id"];
              Session["ListItem_Name"] = DataRow_Row["ListItem_Name"];

              int Id = int.Parse(Session["ListItem_Id"].ToString(), CultureInfo.CurrentCulture);
              switch (Id)
              {
                case 4320:
                  ((Label)FormView_Incident_Form.FindControl("Label_ReportableInfo")).Text = Session["ListItem_Name"].ToString();
                  break;
              }
            }
          }
        }
      }
      Session["ListItem_Id"] = "";
      Session["ListItem_Name"] = "";

      if (FormView_Incident_Form.CurrentMode == FormViewMode.Insert)
      {
        string Incident_Status = "";
        string SQLStringFormStatus = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('1')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
        using (SqlCommand SqlCommand_FormStatus = new SqlCommand(SQLStringFormStatus))
        {
          SqlCommand_FormStatus.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_FormStatus.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
          DataTable DataTable_FormStatus;
          using (DataTable_FormStatus = new DataTable())
          {
            DataTable_FormStatus.Locale = CultureInfo.CurrentCulture;
            DataTable_FormStatus = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormStatus).Copy();
            if (DataTable_FormStatus.Rows.Count > 0)
            {
              DataRow[] SecurityAdmin = DataTable_FormStatus.Select("SecurityRole_Id = '1'");
              DataRow[] SecurityFormAdminUpdate = DataTable_FormStatus.Select("SecurityRole_Id = '20'");
              DataRow[] SecurityFormAdminView = DataTable_FormStatus.Select("SecurityRole_Id = '131'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormStatus.Select("SecurityRole_Id = '3'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormStatus.Select("SecurityRole_Id = '132'");
              DataRow[] SecurityFacilityPharmacyManager = DataTable_FormStatus.Select("SecurityRole_Id = '189'");
              DataRow[] SecurityFacilityApprover = DataTable_FormStatus.Select("SecurityRole_Id = '2'");
              DataRow[] SecurityFacilityCapturer = DataTable_FormStatus.Select("SecurityRole_Id = '70'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityApprover.Length > 0))
              {
                Session["Security"] = "0";
                Incident_Status = "Approved";
              }

              if (Session["Security"].ToString() == "1" && SecurityFacilityPharmacyManager.Length > 0)
              {
                Session["Security"] = "0";
                string TriggerValue = Pharmacy_TriggerLevel("Insert");

                if (string.IsNullOrEmpty(TriggerValue))
                {
                  Incident_Status = "Pending Approval";
                }
                else
                {
                  Incident_Status = "Approved";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityCapturer.Length > 0))
              {
                Session["Security"] = "0";
                Incident_Status = "Pending Approval";
              }

              Session["Security"] = "1";
            }
            else
            {
              Incident_Status = "Pending Approval";
            }
          }
        }

        ((Label)FormView_Incident_Form.FindControl("Label_InsertStatus")).Text = Convert.ToString(Incident_Status, CultureInfo.CurrentCulture);

        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertFacility")).SelectedValue = Request.QueryString["s_Facility_Id"];
        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertFacilityFrom")).SelectedValue = Request.QueryString["s_Facility_Id"];

        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel1List")).Items.Clear();
        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel1List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 1", CultureInfo.CurrentCulture), ""));

        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).Items.Clear();
        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 2", CultureInfo.CurrentCulture), ""));

        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Items.Clear();
        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 3", CultureInfo.CurrentCulture), ""));


        TableFormVisible_Insert();
      }

      if (FormView_Incident_Form.CurrentMode == FormViewMode.Edit)
      {
        TableFormVisible_Edit();
      }
    }

    private void TableFormVisible_Insert()
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertFacilityFrom")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertTimeHours")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertTimeMin")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportingPerson")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportingPerson")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertUnitFromUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertUnitToUnit")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertIncidentCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertCVRName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertCVRName")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertCVRContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertCVRContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEEmployeeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEEmployeeNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((Button)FormView_Incident_Form.FindControl("Button_InsertFindEEmployeeName")).Attributes.Add("OnClick", "Search_EEmployeeName();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEEmployeeName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEEmployeeName")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEEmployeeUnitUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEEmployeeStatusList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEStaffCategoryList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEBodyPartAffectedList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertETreatmentRequiredList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEDaysOff")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEDaysOff")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEMainContributorList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEMainContributorStaffList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertMMDTName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertMMDTName")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertMMDTContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertMMDTContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertMMDTDisciplineList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPVisitNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPVisitNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((Button)FormView_Incident_Form.FindControl("Button_InsertFindPName")).Attributes.Add("OnClick", "Search_PName();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPName")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPMainContributorList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPMainContributorStaffList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPropMainContributorList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPropMainContributorStaffList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertSSName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertSSName")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertSSContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertSSContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel1List")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertSeverityList")).Attributes.Add("OnChange", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportable")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");

      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportCOID")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportCOID');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCOIDDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCOIDDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCOIDNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCOIDNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportDEAT")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportDEAT');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDEATDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDEATDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDEATNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDEATNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportDepartmentOfHealth")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportDepartmentOfHealth');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfHealthDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfHealthDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfHealthNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfHealthNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportDepartmentOfLabour")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportDepartmentOfLabour');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfLabourDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfLabourDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfLabourNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfLabourNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportHospitalManager")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportHospitalManager');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHospitalManagerDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHospitalManagerDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHospitalManagerNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHospitalManagerNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportHPCSA")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportHPCSA');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHPCSADate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHPCSADate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHPCSANumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHPCSANumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportLegalDepartment")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportLegalDepartment');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportLegalDepartmentDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportLegalDepartmentDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportLegalDepartmentNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportLegalDepartmentNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportCEO")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportCEO');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCEODate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCEODate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCEONumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCEONumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportPharmacyCouncil")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportPharmacyCouncil');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportPharmacyCouncilDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportPharmacyCouncilDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportPharmacyCouncilNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportPharmacyCouncilNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportQuality")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportQuality');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportQualityDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportQualityDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportQualityNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportQualityNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportRM")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportRM');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportRMDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportRMDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportRMNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportRMNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportSANC")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportSANC');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSANCDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSANCDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSANCNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSANCNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportSAPS")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportSAPS');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSAPSDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSAPSDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSAPSNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSAPSNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportInternalAudit")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportInternalAudit');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportInternalAuditDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportInternalAuditDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportInternalAuditNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportInternalAuditNumber")).Attributes.Add("OnInput", "Validation_Form();");

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPatientFallingWhereFallOccurList")).Attributes.Add("OnChange", "Validation_Form();");

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailVisitNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailVisitNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((Button)FormView_Incident_Form.FindControl("Button_InsertFindPatientDetailName")).Attributes.Add("OnClick", "Search_PatientDetailName();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailName")).Attributes.Add("OnInput", "Validation_Form();");

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyInitials")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyInitials")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyStaffInvolvedList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyCheckingList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyLocumOrPermanent")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyStaffOnDutyList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyChangeInWorkProcedure")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyTypeOfPrescriptionList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfRxOnDay")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfRxOnDay")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfItemsDispensedOnDay")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfItemsDispensedOnDay")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfRxDayBefore")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfRxDayBefore")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfItemsDispensedDayBefore")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfItemsDispensedDayBefore")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyDrugPrescribed")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyDrugPrescribed")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyDrugDispensed")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyDrugDispensed")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyDrugPacked")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyDrugPacked")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyStrengthDrugPrescribed")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyStrengthDrugPrescribed")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyStrengthDrugDispensed")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyStrengthDrugDispensed")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyDrugPrescribedNewOnMarket")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyLegislativeInformationOnPrescription")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyLegislativeInformationNotOnPrescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyLegislativeInformationNotOnPrescription")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyDoctorName")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyFactorsList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacySystemRelatedIssuesList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyErgonomicProblemsList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyPatientCounselled")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacySimilarIncident")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyLocationList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyPatientOutcomeAffected")).Attributes.Add("OnChange", "Validation_Form();");

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOAcknowledgedHM")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEODoctorRelated")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOCEONotifiedWithin24Hours")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOProgressUpdateSent")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOActionsTakenHM")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOActionsTakenHM")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEODate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEODate")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportableCEOActionsAgainstEmployee")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOEmployeeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOEmployeeNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((Button)FormView_Incident_Form.FindControl("Button_InsertFindReportableCEOEmployeeName")).Attributes.Add("OnClick", "Search_ReportableCEOEmployeeName();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOEmployeeName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOEmployeeName")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOOutcome")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOOutcome")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOCloseOffHM")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOCloseOutEmailSend")).Attributes.Add("OnChange", "Validation_Form();");

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableSAPSPoliceStation")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableSAPSPoliceStation")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableSAPSCaseNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableSAPSCaseNumber")).Attributes.Add("OnInput", "Validation_Form();");

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditDateDetected")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditDateDetected")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditByWhom")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditByWhom")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditRecoveryPlan")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditRecoveryPlan")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditStatusOfInvestigation")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditStatusOfInvestigation")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditSAPSNotReported")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditSAPSNotReported")).Attributes.Add("OnInput", "Validation_Form();");

      ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Attributes.Add("OnClick", "Validation_Form();");
      ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Attributes.Add("OnClick", "Validation_Form();");

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertRootCategoryList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertRootDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertRootDescription")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertDisciplineList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertInvestigator")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertInvestigator")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertInvestigatorContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertInvestigatorContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertInvestigatorDesignation")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertInvestigatorDesignation")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
    }

    private void TableFormVisible_Edit()
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditFacilityFrom")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditTimeHours")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditTimeMin")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportingPerson")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportingPerson")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditUnitFromUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditUnitToUnit")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditIncidentCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditCVRName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditCVRName")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditCVRContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditCVRContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEEmployeeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEEmployeeNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((Button)FormView_Incident_Form.FindControl("Button_EditFindEEmployeeName")).Attributes.Add("OnClick", "Search_EEmployeeName();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEEmployeeName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEEmployeeName")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEEmployeeUnitUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEEmployeeStatusList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEStaffCategoryList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEBodyPartAffectedList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditETreatmentRequiredList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEDaysOff")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEDaysOff")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEMainContributorList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEMainContributorStaffList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditMMDTName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditMMDTName")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditMMDTContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditMMDTContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditMMDTDisciplineList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPVisitNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPVisitNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((Button)FormView_Incident_Form.FindControl("Button_EditFindPName")).Attributes.Add("OnClick", "Search_PName();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPName")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPMainContributorList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPMainContributorStaffList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPropMainContributorList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPropMainContributorStaffList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditSSName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditSSName")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditSSContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditSSContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel1List")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditSeverityList")).Attributes.Add("OnChange", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportable")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");

      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportCOID")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportCOID');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCOIDDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCOIDDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCOIDNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCOIDNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportDEAT")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportDEAT');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDEATDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDEATDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDEATNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDEATNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportDepartmentOfHealth")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportDepartmentOfHealth');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfHealthDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfHealthDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfHealthNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfHealthNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportDepartmentOfLabour")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportDepartmentOfLabour');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfLabourDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfLabourDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfLabourNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfLabourNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportHospitalManager")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportHospitalManager');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHospitalManagerDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHospitalManagerDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHospitalManagerNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHospitalManagerNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportHPCSA")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportHPCSA');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHPCSADate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHPCSADate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHPCSANumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHPCSANumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportLegalDepartment")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportLegalDepartment');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportLegalDepartmentDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportLegalDepartmentDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportLegalDepartmentNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportLegalDepartmentNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportCEO")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportCEO');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCEODate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCEODate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCEONumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCEONumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportPharmacyCouncil")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportPharmacyCouncil');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportPharmacyCouncilDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportPharmacyCouncilDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportPharmacyCouncilNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportPharmacyCouncilNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportQuality")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportQuality');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportQualityDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportQualityDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportQualityNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportQualityNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportRM")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportRM');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportRMDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportRMDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportRMNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportRMNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportSANC")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportSANC');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSANCDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSANCDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSANCNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSANCNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportSAPS")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportSAPS');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSAPSDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSAPSDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSAPSNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSAPSNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportInternalAudit")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form('ReportInternalAudit');");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportInternalAuditDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportInternalAuditDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportInternalAuditNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportInternalAuditNumber")).Attributes.Add("OnInput", "Validation_Form();");

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPatientFallingWhereFallOccurList")).Attributes.Add("OnChange", "Validation_Form();");

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailVisitNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailVisitNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((Button)FormView_Incident_Form.FindControl("Button_EditFindPatientDetailName")).Attributes.Add("OnClick", "Search_PatientDetailName();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailName")).Attributes.Add("OnInput", "Validation_Form();");

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyInitials")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyInitials")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyStaffInvolvedList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyCheckingList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyLocumOrPermanent")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyStaffOnDutyList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyChangeInWorkProcedure")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyTypeOfPrescriptionList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfRxOnDay")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfRxOnDay")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfItemsDispensedOnDay")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfItemsDispensedOnDay")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfRxDayBefore")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfRxDayBefore")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfItemsDispensedDayBefore")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfItemsDispensedDayBefore")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyDrugPrescribed")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyDrugPrescribed")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyDrugDispensed")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyDrugDispensed")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyDrugPacked")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyDrugPacked")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyStrengthDrugPrescribed")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyStrengthDrugPrescribed")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyStrengthDrugDispensed")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyStrengthDrugDispensed")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDrugPrescribedNewOnMarket")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyLegislativeInformationOnPrescription")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyLegislativeInformationNotOnPrescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyLegislativeInformationNotOnPrescription")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyFactorsList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacySystemRelatedIssuesList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyErgonomicProblemsList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyPatientCounselled")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacySimilarIncident")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyLocationList")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyPatientOutcomeAffected")).Attributes.Add("OnChange", "Validation_Form();");

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOAcknowledgedHM")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEODoctorRelated")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOCEONotifiedWithin24Hours")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOProgressUpdateSent")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOActionsTakenHM")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOActionsTakenHM")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEODate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEODate")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportableCEOActionsAgainstEmployee")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOEmployeeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOEmployeeNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((Button)FormView_Incident_Form.FindControl("Button_EditFindReportableCEOEmployeeName")).Attributes.Add("OnClick", "Search_ReportableCEOEmployeeName();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOEmployeeName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOEmployeeName")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOOutcome")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOOutcome")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOCloseOffHM")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOCloseOutEmailSend")).Attributes.Add("OnChange", "Validation_Form();");

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableSAPSPoliceStation")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableSAPSPoliceStation")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableSAPSCaseNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableSAPSCaseNumber")).Attributes.Add("OnInput", "Validation_Form();");

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditDateDetected")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditDateDetected")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditByWhom")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditByWhom")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditRecoveryPlan")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditRecoveryPlan")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditStatusOfInvestigation")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditStatusOfInvestigation")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditSAPSNotReported")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditSAPSNotReported")).Attributes.Add("OnInput", "Validation_Form();");

      ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Attributes.Add("OnClick", "Validation_Form();");
      ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Attributes.Add("OnClick", "Validation_Form();");

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditRootCategoryList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditRootDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditRootDescription")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditDisciplineList")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditInvestigator")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditInvestigator")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditInvestigatorContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditInvestigatorContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditInvestigatorDesignation")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditInvestigatorDesignation")).Attributes.Add("OnInput", "Validation_Form();");
      ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditStatus")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditStatusRejectedReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditStatusRejectedReason")).Attributes.Add("OnInput", "Validation_Form();");
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityType"];
      string SearchField2 = Request.QueryString["Search_FacilityId"];
      string SearchField3 = Request.QueryString["Search_IncidentReportNumber"];
      string SearchField4 = Request.QueryString["Search_IncidentUnitToUnit"];
      string SearchField5 = Request.QueryString["Search_IncidentStatus"];
      string SearchField6 = Request.QueryString["Search_IncidentInvestigationCompleted"];
      string SearchField7 = Request.QueryString["Search_IncidentStatusDateFrom"];
      string SearchField8 = Request.QueryString["Search_IncidentStatusDateTo"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Type=" + Request.QueryString["Search_FacilityType"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Incident_ReportNumber=" + Request.QueryString["Search_IncidentReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_Incident_UnitToUnit=" + Request.QueryString["Search_IncidentUnitToUnit"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_Incident_Status=" + Request.QueryString["Search_IncidentStatus"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_Incident_InvestigationCompleted=" + Request.QueryString["Search_IncidentInvestigationCompleted"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "s_Incident_StatusDateFrom=" + Request.QueryString["Search_IncidentStatusDateFrom"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField8))
      {
        SearchField8 = "s_Incident_StatusDateTo=" + Request.QueryString["Search_IncidentStatusDateTo"] + "&";
      }

      string FinalURL = "Form_Incident_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7 + SearchField8;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --TableFacility--//
    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident New Form", "Form_Incident.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue + ""), false);
    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }
    //---END--- --TableFacility--//


    //--START-- --TableForm--//
    //--START-- --Insert--//
    protected void FormView_Incident_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_Incident.SetFocus(UpdatePanel_Incident);

          ((Label)FormView_Incident_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_Incident_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          Session["Incident_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Request.QueryString["s_Facility_Id"], "1");

          SqlDataSource_Incident_Form.InsertParameters["Incident_ReportNumber"].DefaultValue = Session["Incident_ReportNumber"].ToString();
          SqlDataSource_Incident_Form.InsertParameters["Incident_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Incident_Form.InsertParameters["Incident_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Incident_Form.InsertParameters["Incident_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Incident_Form.InsertParameters["Incident_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Incident_Form.InsertParameters["Incident_History"].DefaultValue = "";
          SqlDataSource_Incident_Form.InsertParameters["Incident_Archived"].DefaultValue = "false";

          SqlDataSource_Incident_Form.InsertParameters["Incident_Level1_List"].DefaultValue = ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel1List")).SelectedValue;
          SqlDataSource_Incident_Form.InsertParameters["Incident_Level2_List"].DefaultValue = ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).SelectedValue;
          SqlDataSource_Incident_Form.InsertParameters["Incident_Level3_List"].DefaultValue = ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).SelectedValue;
          SqlDataSource_Incident_Form.InsertParameters["Incident_Pharmacy_DoctorName"].DefaultValue = ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyDoctorName")).SelectedValue;

          if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Checked == true)
          {
            SqlDataSource_Incident_Form.InsertParameters["Incident_InvestigationCompletedDate"].DefaultValue = DateTime.Now.ToString();
          }
          else
          {
            SqlDataSource_Incident_Form.InsertParameters["Incident_InvestigationCompletedDate"].DefaultValue = "";
          }

          string Incident_Status = "";
          string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('1')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
          using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
          {
            SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
            DataTable DataTable_FormMode;
            using (DataTable_FormMode = new DataTable())
            {
              DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
              DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
              if (DataTable_FormMode.Rows.Count > 0)
              {
                DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
                DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '20'");
                DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '131'");
                DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '3'");
                DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '132'");
                DataRow[] SecurityFacilityPharmacyManager = DataTable_FormMode.Select("SecurityRole_Id = '189'");
                DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '2'");
                DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '70'");

                Session["Security"] = "1";
                if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityApprover.Length > 0))
                {
                  Session["Security"] = "0";
                  Incident_Status = "Approved";
                }

                if (Session["Security"].ToString() == "1" && SecurityFacilityPharmacyManager.Length > 0)
                {
                  Session["Security"] = "0";
                  string TriggerValue = Pharmacy_TriggerLevel("Insert");

                  if (string.IsNullOrEmpty(TriggerValue))
                  {
                    Incident_Status = "Pending Approval";
                  }
                  else
                  {
                    Incident_Status = "Approved";
                  }
                }

                if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityCapturer.Length > 0))
                {
                  Session["Security"] = "0";
                  Incident_Status = "Pending Approval";
                }

                if (Session["Security"].ToString() == "1")
                {
                  Incident_Status = "Pending Approval";
                }
                Session["Security"] = "1";
              }
              else
              {
                Incident_Status = "Pending Approval";
              }
            }
          }

          SqlDataSource_Incident_Form.InsertParameters["Incident_Status"].DefaultValue = Incident_Status;
          SqlDataSource_Incident_Form.InsertParameters["Incident_StatusDate"].DefaultValue = DateTime.Now.ToString();

          Session["Incident_ReportNumber"] = "";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        InvalidForm = InsertValidation_Incident(InvalidForm);

        InvalidForm = InsertValidation_IncidentCategoryList(InvalidForm);

        InvalidForm = InsertValidation_IncidentDetail(InvalidForm);

        InvalidForm = InsertValidation_Reportable(InvalidForm);

        InvalidForm = InsertValidation_ReportableToCEO(InvalidForm);

        InvalidForm = InsertValidation_ReportableToSAPS(InvalidForm);

        InvalidForm = InsertValidation_ReportableToInternalAudit(InvalidForm);

        InvalidForm = InsertValidation_DegreeOfHarm(InvalidForm);

        InvalidForm = InsertValidation_Investigation(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = InsertFieldValidation();
      }

      return InvalidFormMessage;
    }

    protected string InsertValidation_Incident(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertFacility")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertFacilityFrom")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDate")).Text.Trim()))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertTimeHours")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertTimeMin")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportingPerson")).Text.Trim()))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertUnitFromUnit")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertUnitToUnit")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertDisciplineList")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertIncidentCategoryList")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      InsertValidation_PatientFalling(InvalidForm);

      InsertValidation_PatientDetail(InvalidForm);

      InsertValidation_Pharmacy(InvalidForm);

      return InvalidForm;
    }

    protected string InsertValidation_IncidentCategoryList(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertIncidentCategoryList")).SelectedValue == "266")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertCVRName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertCVRContactNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertIncidentCategoryList")).SelectedValue == "267")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEEmployeeNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEEmployeeName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEEmployeeUnitUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEEmployeeStatusList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEStaffCategoryList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEBodyPartAffectedList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertETreatmentRequiredList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertETreatmentRequiredList")).SelectedValue == "6058")
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEDaysOff")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEMainContributorList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEMainContributorList")).SelectedValue == "6261")
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertEMainContributorStaffList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertIncidentCategoryList")).SelectedValue == "268")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertMMDTName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertMMDTContactNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertMMDTDisciplineList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertIncidentCategoryList")).SelectedValue == "269")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPVisitNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPMainContributorList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPMainContributorList")).SelectedValue == "6261")
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPMainContributorStaffList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertIncidentCategoryList")).SelectedValue == "270")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPropMainContributorList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPropMainContributorList")).SelectedValue == "6261")
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPropMainContributorStaffList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertIncidentCategoryList")).SelectedValue == "271")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertSSName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertSSContactNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_IncidentDetail(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDescription")).Text.Trim()))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel1List")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Items.Count > 1)
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertSeverityList")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportable")).Checked == true)
      {
        InvalidForm = InsertValidation_Reportable_COID(InvalidForm);

        InvalidForm = InsertValidation_Reportable_DEAT(InvalidForm);

        InvalidForm = InsertValidation_Reportable_DepartmentOfHealth(InvalidForm);

        InvalidForm = InsertValidation_Reportable_DepartmentOfLabour(InvalidForm);

        InvalidForm = InsertValidation_Reportable_HospitalManager(InvalidForm);

        InvalidForm = InsertValidation_Reportable_HPCSA(InvalidForm);

        InvalidForm = InsertValidation_Reportable_LegalDepartment(InvalidForm);

        InvalidForm = InsertValidation_Reportable_CEO(InvalidForm);

        InvalidForm = InsertValidation_Reportable_PharmacyCouncil(InvalidForm);

        InvalidForm = InsertValidation_Reportable_Quality(InvalidForm);

        InvalidForm = InsertValidation_Reportable_RM(InvalidForm);

        InvalidForm = InsertValidation_Reportable_SANC(InvalidForm);

        InvalidForm = InsertValidation_Reportable_SAPS(InvalidForm);

        InvalidForm = InsertValidation_Reportable_InternalAudit(InvalidForm);
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_COID(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportCOID")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCOIDDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCOIDNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_DEAT(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportDEAT")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDEATDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDEATNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_DepartmentOfHealth(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportDepartmentOfHealth")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfHealthDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfHealthNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_DepartmentOfLabour(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportDepartmentOfLabour")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfLabourDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportDepartmentOfLabourNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_HospitalManager(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportHospitalManager")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHospitalManagerDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHospitalManagerNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_HPCSA(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportHPCSA")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHPCSADate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportHPCSANumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_LegalDepartment(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportLegalDepartment")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportLegalDepartmentDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportLegalDepartmentNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_CEO(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportCEO")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCEODate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportCEONumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_PharmacyCouncil(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportPharmacyCouncil")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportPharmacyCouncilDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportPharmacyCouncilNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_Quality(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportQuality")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportQualityDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportQualityNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_RM(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportRM")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportRMDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportRMNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_SANC(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportSANC")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSANCDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSANCNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_SAPS(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportSAPS")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSAPSDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportSAPSNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Reportable_InternalAudit(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportInternalAudit")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportInternalAuditDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportInternalAuditNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_PatientFalling(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (!string.IsNullOrEmpty(((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientFalling_TriggerLevel")).Value))
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPatientFallingWhereFallOccurList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_PatientDetail(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (!string.IsNullOrEmpty(((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientDetail_TriggerLevel")).Value))
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailVisitNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Pharmacy(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (!string.IsNullOrEmpty(((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPharmacy_TriggerLevel")).Value))
      {
        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyInitials")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyStaffInvolvedList")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyCheckingList")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyLocumOrPermanent")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyStaffOnDutyList")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyChangeInWorkProcedure")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyTypeOfPrescriptionList")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfRxOnDay")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfItemsDispensedOnDay")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfRxDayBefore")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyNumberOfItemsDispensedDayBefore")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyDrugPrescribed")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyDrugDispensed")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyDrugPacked")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyStrengthDrugPrescribed")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyStrengthDrugDispensed")).Text.Trim()))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyDrugPrescribedNewOnMarket")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyLegislativeInformationOnPrescription")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyLegislativeInformationOnPrescription")).Text == "No")
        //{
        //  if (String.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPharmacyLegislativeInformationNotOnPrescription")).Text.Trim()))
        //  {
        //    InvalidForm = "Yes";
        //  }
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyDoctorName")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyFactorsList")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacySystemRelatedIssuesList")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyErgonomicProblemsList")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyTypeOfPrescriptionList")).SelectedValue != "6094")
        //{
        //  if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyPatientCounselled")).SelectedValue))
        //  {
        //    InvalidForm = "Yes";
        //  }
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacySimilarIncident")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyLocationList")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}

        //if (String.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyPatientOutcomeAffected")).SelectedValue))
        //{
        //  InvalidForm = "Yes";
        //}
      }

      return InvalidForm;
    }

    protected string InsertValidation_ReportableToCEO(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportable")).Checked == true)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportCEO")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEODoctorRelated")).SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")) != null)
          {
            if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Checked == true)
            {
              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOAcknowledgedHM")).SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOCEONotifiedWithin24Hours")).SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOProgressUpdateSent")).SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOActionsTakenHM")).Text.Trim()))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEODate")).Text.Trim()))
              {
                InvalidForm = "Yes";
              }

              if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportableCEOActionsAgainstEmployee")).Checked == true)
              {
                if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOEmployeeNumber")).Text.Trim()))
                {
                  InvalidForm = "Yes";
                }

                if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOEmployeeName")).Text.Trim()))
                {
                  InvalidForm = "Yes";
                }
              }

              if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOOutcome")).Text.Trim()))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOCloseOffHM")).SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEOCloseOutEmailSend")).SelectedValue))
              {
                InvalidForm = "Yes";
              }
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_ReportableToSAPS(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportable")).Checked == true)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportSAPS")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableSAPSPoliceStation")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableSAPSCaseNumber")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_ReportableToInternalAudit(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportable")).Checked == true)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportInternalAudit")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditDateDetected")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditByWhom")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditRecoveryPlan")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditStatusOfInvestigation")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportSAPS")).Checked == false)
          {
            if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditSAPSNotReported")).Text.Trim()))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_DegreeOfHarm(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")) != null)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Checked == true)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }

          string ImpactItemCompleted = "No";
          foreach (System.Web.UI.WebControls.ListItem ListItem_DegreeOfHarmImpactImpactList in ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Items)
          {
            if (ListItem_DegreeOfHarmImpactImpactList.Selected == true)
            {
              ImpactItemCompleted = "Yes";
              break;
            }
            else if (ListItem_DegreeOfHarmImpactImpactList.Selected == false)
            {
              ImpactItemCompleted = "No";
            }
          }

          if (ImpactItemCompleted == "No")
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Investigation(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")) != null)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertRootCategoryList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertRootDescription")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertInvestigator")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertInvestigatorContactNumber")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertInvestigatorDesignation")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected string InsertFieldValidation()
    {
      string InvalidFormMessage = "";

      DateTime CurrentDate = DateTime.Now;
      DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDate")).Text);
      if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + "Not a valid date, date must be in the format yyyy/mm/dd<br />";
      }
      else
      {
        if (ValidatedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
        }
      }

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportable")).Checked == true)
      {
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("COID", "COID");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("DEAT", "DEAT");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("DepartmentOfHealth", "Department Of Health");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("DepartmentOfLabour", "Department Of Labour");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("HospitalManager", "Hospital Manager");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("HPCSA", "HPCSA");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("LegalDepartment", "Legal Department");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("CEO", "CEO");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("PharmacyCouncil", "Pharmacy Council");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("Quality", "Quality");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("RM", "RM");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("SANC", "SANC");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("SAPS", "SAPS");
        InvalidFormMessage = InvalidFormMessage + InsertFieldValidation_ReportableDate("InternalAudit", "Internal Audit");


        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportCEO")).Checked == true)
        {
          if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEODate")).Text.Trim().ToString()))
          {
            DateTime ValidatedReportableCEODate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEODate")).Text);
            if (ValidatedReportableCEODate.ToString() == "0001/01/01 12:00:00 AM")
            {
              InvalidFormMessage = InvalidFormMessage + "Not a valid Reportable to CEO date, date must be in the format yyyy/mm/dd<br />";
            }
            else
            {
              if (ValidatedReportableCEODate.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
              }
            }
          }
        }


        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportInternalAudit")).Checked == true)
        {
          if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditDateDetected")).Text.Trim().ToString()))
          {
            DateTime ValidatedReportableReportableInternalAuditDateDetected = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableInternalAuditDateDetected")).Text);
            if (ValidatedReportableReportableInternalAuditDateDetected.ToString() == "0001/01/01 12:00:00 AM")
            {
              InvalidFormMessage = InvalidFormMessage + "Not a valid Reportable to Internal Audit Date Detected date, date must be in the format yyyy/mm/dd<br />";
            }
            else
            {
              if (ValidatedReportableReportableInternalAuditDateDetected.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
              }
            }
          }
        }
      }

      return InvalidFormMessage;
    }

    protected string InsertFieldValidation_ReportableDate(string reportableControl, string reportableControlText)
    {
      string InvalidFormMessage = "";

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReport" + reportableControl + "")).Checked == true)
      {
        DateTime CurrentDate = DateTime.Now;
        DateTime ValidatedDateReportDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReport" + reportableControl + "Date")).Text);
        if (ValidatedDateReportDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid " + reportableControlText + " date, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          if (ValidatedDateReportDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future " + reportableControlText + " dates allowed<br />";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Incident_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["Incident_Id"] = e.Command.Parameters["@Incident_Id"].Value;
        Session["Incident_ReportNumber"] = e.Command.Parameters["@Incident_ReportNumber"].Value;

        if (!string.IsNullOrEmpty(Session["Incident_Id"].ToString()))
        {
          if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")) != null)
          {
            foreach (ListItem ListItem_DegreeOfHarmImpactItemList in ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Items)
            {
              if (ListItem_DegreeOfHarmImpactItemList.Selected)
              {
                string SQLStringInsertDegreeOfHarmImpact = "INSERT INTO Form_Incident_DegreeOfHarm_Impact ( Incident_Id , Incident_DegreeOfHarm_Impact_Impact_List , Incident_DegreeOfHarm_Impact_CreatedDate , Incident_DegreeOfHarm_Impact_CreatedBy ) VALUES ( @Incident_Id , @Incident_DegreeOfHarm_Impact_Impact_List , @Incident_DegreeOfHarm_Impact_CreatedDate , @Incident_DegreeOfHarm_Impact_CreatedBy )";
                using (SqlCommand SqlCommand_InsertDegreeOfHarmImpact = new SqlCommand(SQLStringInsertDegreeOfHarmImpact))
                {
                  SqlCommand_InsertDegreeOfHarmImpact.Parameters.AddWithValue("@Incident_Id", Session["Incident_Id"].ToString());
                  SqlCommand_InsertDegreeOfHarmImpact.Parameters.AddWithValue("@Incident_DegreeOfHarm_Impact_Impact_List", ListItem_DegreeOfHarmImpactItemList.Value.ToString());
                  SqlCommand_InsertDegreeOfHarmImpact.Parameters.AddWithValue("@Incident_DegreeOfHarm_Impact_CreatedDate", DateTime.Now);
                  SqlCommand_InsertDegreeOfHarmImpact.Parameters.AddWithValue("@Incident_DegreeOfHarm_Impact_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertDegreeOfHarmImpact);
                }
              }
            }
          }
        }

        EmailTriggerLevel_Insert(Session["Incident_Id"].ToString());
        EmailTriggerUnit_Insert(Session["Incident_Id"].ToString());
        Email_Pharmacy_TriggerLevel_Insert(Session["Incident_Id"].ToString());
        Email_ReportableCEO_TriggerDoctorRelated_Insert(Session["Incident_Id"].ToString());
        Email_ReportableCEO_Insert(Session["Incident_Id"].ToString());

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_Incident&ReportNumber=" + Session["Incident_ReportNumber"].ToString() + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    string TriggerUnit_EditChanged = "No";
    string TriggerLevel_EditChanged = "No";
    string ReportableCEO_TriggerDoctorRelated_EditChanged = "No";
    string ReportableCEO_EditChanged = "No";

    protected void FormView_Incident_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIncidentModifiedDate"] = e.OldValues["Incident_ModifiedDate"];
        object OLDIncidentModifiedDate = Session["OLDIncidentModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIncidentModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareIncident = (DataView)SqlDataSource_Incident_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareIncident = DataView_CompareIncident[0];
        Session["DBIncidentModifiedDate"] = Convert.ToString(DataRowView_CompareIncident["Incident_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIncidentModifiedBy"] = Convert.ToString(DataRowView_CompareIncident["Incident_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIncidentModifiedDate = Session["DBIncidentModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIncidentModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_Incident.SetFocus(UpdatePanel_Incident);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBIncidentModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_Incident_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Incident_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_Incident.SetFocus(UpdatePanel_Incident);
            ((Label)FormView_Incident_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Incident_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
            e.NewValues["Incident_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Incident_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Incident", "Incident_Id = " + Request.QueryString["Incident_Id"]);

            DataView DataView_Incident = (DataView)SqlDataSource_Incident_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Incident = DataView_Incident[0];
            Session["IncidentHistory"] = Convert.ToString(DataRowView_Incident["Incident_History"], CultureInfo.CurrentCulture);

            Session["IncidentHistory"] = Session["History"].ToString() + Session["IncidentHistory"].ToString();
            e.NewValues["Incident_History"] = Session["IncidentHistory"].ToString();

            Session["IncidentHistory"] = "";
            Session["History"] = "";

            e.NewValues["Incident_Level1_List"] = ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel1List")).SelectedValue;
            e.NewValues["Incident_Level2_List"] = ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).SelectedValue;
            e.NewValues["Incident_Level3_List"] = ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).SelectedValue;
            e.NewValues["Incident_Pharmacy_DoctorName"] = ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).SelectedValue;


            string DBInvestigationCompleted = e.OldValues["Incident_InvestigationCompleted"].ToString();
            string DBStatus = e.OldValues["Incident_Status"].ToString();

            if (DBInvestigationCompleted != ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Checked.ToString())
            {
              if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Checked == true)
              {
                e.NewValues["Incident_InvestigationCompletedDate"] = DateTime.Now.ToString();
              }
              else
              {
                e.NewValues["Incident_InvestigationCompletedDate"] = "";
              }
            }

            if (DBStatus != ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditStatus")).SelectedValue)
            {
              if (DBStatus == "Pending Approval")
              {
                e.NewValues["Incident_StatusDate"] = DateTime.Now.ToString();
              }
            }


            if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditUnitToUnit")).SelectedValue != e.OldValues["Incident_UnitTo_Unit"].ToString())
            {
              TriggerUnit_EditChanged = "Yes";
            }

            if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel1List")).SelectedValue != Convert.ToString(DataRowView_CompareIncident["Incident_Level1_List"], CultureInfo.CurrentCulture) || ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).SelectedValue != Convert.ToString(DataRowView_CompareIncident["Incident_Level2_List"], CultureInfo.CurrentCulture) || ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).SelectedValue != Convert.ToString(DataRowView_CompareIncident["Incident_Level3_List"], CultureInfo.CurrentCulture))
            {
              TriggerLevel_EditChanged = "Yes";
            }

            if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEODoctorRelated")).SelectedValue == "Yes")
            {
              if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEODoctorRelated")).SelectedValue != e.OldValues["Incident_Reportable_CEO_DoctorRelated"].ToString())
              {
                ReportableCEO_TriggerDoctorRelated_EditChanged = "Yes";
              }
            }

            if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportCEO")).Checked == true)
            {
              if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportCEO")).Checked.ToString() != e.OldValues["Incident_Report_CEO"].ToString())
              {
                ReportableCEO_EditChanged = "Yes";
              }
            }
          }
        }

        Session["OLDIncidentModifiedDate"] = "";
        Session["DBIncidentModifiedDate"] = "";
        Session["DBIncidentModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        InvalidForm = EditValidation_Incident(InvalidForm);

        InvalidForm = EditValidation_IncidentCategoryList(InvalidForm);

        InvalidForm = EditValidation_IncidentDetail(InvalidForm);

        InvalidForm = EditValidation_Reportable(InvalidForm);

        InvalidForm = EditValidation_ReportableToCEO(InvalidForm);

        InvalidForm = EditValidation_ReportableToSAPS(InvalidForm);

        InvalidForm = EditValidation_ReportableToInternalAudit(InvalidForm);

        InvalidForm = EditValidation_DegreeOfHarm(InvalidForm);

        InvalidForm = EditValidation_Investigation(InvalidForm);

        InvalidForm = EditValidation_Status(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = EditFieldValidation();
      }

      return InvalidFormMessage;
    }

    protected string EditValidation_Incident(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditFacilityFrom")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDate")).Text.Trim()))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditTimeHours")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditTimeMin")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportingPerson")).Text.Trim()))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditUnitFromUnit")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditUnitToUnit")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditDisciplineList")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditIncidentCategoryList")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      EditValidation_PatientFalling(InvalidForm);

      EditValidation_PatientDetail(InvalidForm);

      EditValidation_Pharmacy(InvalidForm);

      return InvalidForm;
    }

    protected string EditValidation_IncidentCategoryList(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditIncidentCategoryList")).SelectedValue == "266")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditCVRName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditCVRContactNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditIncidentCategoryList")).SelectedValue == "267")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEEmployeeNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEEmployeeName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEEmployeeUnitUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEEmployeeStatusList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEStaffCategoryList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEBodyPartAffectedList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditETreatmentRequiredList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditETreatmentRequiredList")).SelectedValue == "6058")
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEDaysOff")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEMainContributorList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEMainContributorList")).SelectedValue == "6261")
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditEMainContributorStaffList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditIncidentCategoryList")).SelectedValue == "268")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditMMDTName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditMMDTContactNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditMMDTDisciplineList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditIncidentCategoryList")).SelectedValue == "269")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPVisitNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPMainContributorList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPMainContributorList")).SelectedValue == "6261")
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPMainContributorStaffList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditIncidentCategoryList")).SelectedValue == "270")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPropMainContributorList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPropMainContributorList")).SelectedValue == "6261")
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPropMainContributorStaffList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditIncidentCategoryList")).SelectedValue == "271")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditSSName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditSSContactNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_IncidentDetail(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDescription")).Text.Trim()))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel1List")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).Items.Count > 1)
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditSeverityList")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportable")).Checked == true)
      {
        InvalidForm = EditValidation_Reportable_COID(InvalidForm);

        InvalidForm = EditValidation_Reportable_DEAT(InvalidForm);

        InvalidForm = EditValidation_Reportable_DepartmentOfHealth(InvalidForm);

        InvalidForm = EditValidation_Reportable_DepartmentOfLabour(InvalidForm);

        InvalidForm = EditValidation_Reportable_HospitalManager(InvalidForm);

        InvalidForm = EditValidation_Reportable_HPCSA(InvalidForm);

        InvalidForm = EditValidation_Reportable_LegalDepartment(InvalidForm);

        InvalidForm = EditValidation_Reportable_CEO(InvalidForm);

        InvalidForm = EditValidation_Reportable_PharmacyCouncil(InvalidForm);

        InvalidForm = EditValidation_Reportable_Quality(InvalidForm);

        InvalidForm = EditValidation_Reportable_RM(InvalidForm);

        InvalidForm = EditValidation_Reportable_SANC(InvalidForm);

        InvalidForm = EditValidation_Reportable_SAPS(InvalidForm);

        InvalidForm = EditValidation_Reportable_InternalAudit(InvalidForm);
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_COID(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportCOID")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCOIDDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCOIDNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_DEAT(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportDEAT")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDEATDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDEATNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_DepartmentOfHealth(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportDepartmentOfHealth")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfHealthDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfHealthNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_DepartmentOfLabour(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportDepartmentOfLabour")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfLabourDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportDepartmentOfLabourNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_HospitalManager(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportHospitalManager")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHospitalManagerDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHospitalManagerNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_HPCSA(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportHPCSA")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHPCSADate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportHPCSANumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_LegalDepartment(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportLegalDepartment")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportLegalDepartmentDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportLegalDepartmentNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_CEO(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportCEO")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCEODate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportCEONumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_PharmacyCouncil(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportPharmacyCouncil")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportPharmacyCouncilDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportPharmacyCouncilNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_Quality(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportQuality")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportQualityDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportQualityNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_RM(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportRM")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportRMDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportRMNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_SANC(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportSANC")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSANCDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSANCNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_SAPS(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportSAPS")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSAPSDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportSAPSNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Reportable_InternalAudit(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportInternalAudit")).Checked == true)
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportInternalAuditDate")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportInternalAuditNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_PatientFalling(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (!string.IsNullOrEmpty(((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientFalling_TriggerLevel")).Value))
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPatientFallingWhereFallOccurList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_PatientDetail(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (!string.IsNullOrEmpty(((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientDetail_TriggerLevel")).Value))
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailVisitNumber")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailName")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Pharmacy(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (!string.IsNullOrEmpty(((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPharmacy_TriggerLevel")).Value))
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyInitials")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyStaffInvolvedList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyCheckingList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyLocumOrPermanent")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyStaffOnDutyList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyChangeInWorkProcedure")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyTypeOfPrescriptionList")).SelectedValue != "6094")
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyTypeOfPrescriptionList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfRxOnDay")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfItemsDispensedOnDay")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfRxDayBefore")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyNumberOfItemsDispensedDayBefore")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyDrugPrescribed")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyDrugDispensed")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyDrugPacked")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyStrengthDrugPrescribed")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyStrengthDrugDispensed")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDrugPrescribedNewOnMarket")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyLegislativeInformationOnPrescription")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyLegislativeInformationOnPrescription")).Text == "No")
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPharmacyLegislativeInformationNotOnPrescription")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyFactorsList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacySystemRelatedIssuesList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyErgonomicProblemsList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyPatientCounselled")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacySimilarIncident")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyLocationList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyPatientOutcomeAffected")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_ReportableToCEO(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportable")).Checked == true)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportCEO")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEODoctorRelated")).SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")) != null)
          {
            if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Checked == true)
            {
              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOAcknowledgedHM")).SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOCEONotifiedWithin24Hours")).SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOProgressUpdateSent")).SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOActionsTakenHM")).Text.Trim()))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEODate")).Text.Trim()))
              {
                InvalidForm = "Yes";
              }

              if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportableCEOActionsAgainstEmployee")).Checked == true)
              {
                if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOEmployeeNumber")).Text.Trim()))
                {
                  InvalidForm = "Yes";
                }

                if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOEmployeeName")).Text.Trim()))
                {
                  InvalidForm = "Yes";
                }
              }

              if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOOutcome")).Text.Trim()))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOCloseOffHM")).SelectedValue))
              {
                InvalidForm = "Yes";
              }

              if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditReportableCEOCloseOutEmailSend")).SelectedValue))
              {
                InvalidForm = "Yes";
              }
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_ReportableToSAPS(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportable")).Checked == true)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportSAPS")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableSAPSPoliceStation")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableSAPSCaseNumber")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_ReportableToInternalAudit(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportable")).Checked == true)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportInternalAudit")).Checked == true)
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditDateDetected")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditByWhom")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditRecoveryPlan")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditStatusOfInvestigation")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportSAPS")).Checked == false)
          {
            if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditSAPSNotReported")).Text.Trim()))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_DegreeOfHarm(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")) != null)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Checked == true)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }

          string ImpactItemCompleted = "No";
          foreach (System.Web.UI.WebControls.ListItem ListItem_DegreeOfHarmImpactImpactList in ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Items)
          {
            if (ListItem_DegreeOfHarmImpactImpactList.Selected == true)
            {
              ImpactItemCompleted = "Yes";
              break;
            }
            else if (ListItem_DegreeOfHarmImpactImpactList.Selected == false)
            {
              ImpactItemCompleted = "No";
            }
          }

          if (ImpactItemCompleted == "No")
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Investigation(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")) != null)
      {
        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditRootCategoryList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditRootDescription")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditInvestigator")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditInvestigatorContactNumber")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditInvestigatorDesignation")).Text.Trim()))
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Status(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditStatus")).SelectedValue == "Rejected")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditStatusRejectedReason")).Text.Trim()))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditFieldValidation()
    {
      string InvalidFormMessage = "";

      DateTime CurrentDate = DateTime.Now;
      DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDate")).Text);
      if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + "Not a valid date, date must be in the format yyyy/mm/dd<br />";
      }
      else
      {
        if (ValidatedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
        }
      }

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportable")).Checked == true)
      {
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("COID", "COID");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("DEAT", "DEAT");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("DepartmentOfHealth", "Department Of Health");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("DepartmentOfLabour", "Department Of Labour");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("HospitalManager", "Hospital Manager");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("HPCSA", "HPCSA");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("LegalDepartment", "Legal Department");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("CEO", "CEO");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("PharmacyCouncil", "Pharmacy Council");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("Quality", "Quality");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("RM", "RM");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("SANC", "SANC");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("SAPS", "SAPS");
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation_ReportableDate("InternalAudit", "Internal Audit");


        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportCEO")).Checked == true)
        {
          if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEODate")).Text.Trim().ToString()))
          {
            DateTime ValidatedReportableCEODate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEODate")).Text);
            if (ValidatedReportableCEODate.ToString() == "0001/01/01 12:00:00 AM")
            {
              InvalidFormMessage = InvalidFormMessage + "Not a valid Reportable to CEO date, date must be in the format yyyy/mm/dd<br />";
            }
            else
            {
              if (ValidatedReportableCEODate.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
              }
            }
          }
        }


        if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportInternalAudit")).Checked == true)
        {
          if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditDateDetected")).Text.Trim().ToString()))
          {
            DateTime ValidatedReportableReportableInternalAuditDateDetected = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableInternalAuditDateDetected")).Text);
            if (ValidatedReportableReportableInternalAuditDateDetected.ToString() == "0001/01/01 12:00:00 AM")
            {
              InvalidFormMessage = InvalidFormMessage + "Not a valid Reportable to Internal Audit Date Detected date, date must be in the format yyyy/mm/dd<br />";
            }
            else
            {
              if (ValidatedReportableReportableInternalAuditDateDetected.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
              }
            }
          }
        }
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation_ReportableDate(string reportableControl, string reportableControlText)
    {
      string InvalidFormMessage = "";

      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReport" + reportableControl + "")).Checked == true)
      {
        DateTime CurrentDate = DateTime.Now;
        DateTime ValidatedDateReportDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReport" + reportableControl + "Date")).Text);
        if (ValidatedDateReportDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid " + reportableControlText + " date, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          if (ValidatedDateReportDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future " + reportableControlText + " dates allowed<br />";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Incident_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (!string.IsNullOrEmpty(Request.QueryString["Incident_Id"]))
          {
            if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")) != null)
            {
              string SQLStringIncidentDegreeOfHarmImpact = "DELETE FROM Form_Incident_DegreeOfHarm_Impact WHERE Incident_Id = @Incident_Id";
              using (SqlCommand SqlCommand_IncidentDegreeOfHarmImpact = new SqlCommand(SQLStringIncidentDegreeOfHarmImpact))
              {
                SqlCommand_IncidentDegreeOfHarmImpact.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_IncidentDegreeOfHarmImpact);
              }

              foreach (ListItem ListItem_DegreeOfHarmImpactItemList in ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Items)
              {
                if (ListItem_DegreeOfHarmImpactItemList.Selected)
                {
                  string SQLStringInsertDegreeOfHarmImpact = "INSERT INTO Form_Incident_DegreeOfHarm_Impact ( Incident_Id , Incident_DegreeOfHarm_Impact_Impact_List , Incident_DegreeOfHarm_Impact_CreatedDate , Incident_DegreeOfHarm_Impact_CreatedBy ) VALUES ( @Incident_Id , @Incident_DegreeOfHarm_Impact_Impact_List , @Incident_DegreeOfHarm_Impact_CreatedDate , @Incident_DegreeOfHarm_Impact_CreatedBy )";
                  using (SqlCommand SqlCommand_InsertDegreeOfHarmImpact = new SqlCommand(SQLStringInsertDegreeOfHarmImpact))
                  {
                    SqlCommand_InsertDegreeOfHarmImpact.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
                    SqlCommand_InsertDegreeOfHarmImpact.Parameters.AddWithValue("@Incident_DegreeOfHarm_Impact_Impact_List", ListItem_DegreeOfHarmImpactItemList.Value.ToString());
                    SqlCommand_InsertDegreeOfHarmImpact.Parameters.AddWithValue("@Incident_DegreeOfHarm_Impact_CreatedDate", DateTime.Now);
                    SqlCommand_InsertDegreeOfHarmImpact.Parameters.AddWithValue("@Incident_DegreeOfHarm_Impact_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertDegreeOfHarmImpact);
                  }
                }
              }
            }

            EmailTriggerLevel_Edit();
            EmailTriggerUnit_Edit();
            Email_ReportableCEO_TriggerDoctorRelated_Edit();
            Email_ReportableCEO_Edit();
          }


          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            RedirectToList();
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_Incident, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident Print", "InfoQuest_Print.aspx?PrintPage=Form_Incident&PrintValue=" + Request.QueryString["Incident_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_Incident, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_Incident, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident Email", "InfoQuest_Email.aspx?EmailPage=Form_Incident&EmailValue=" + Request.QueryString["Incident_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_Incident, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_Incident_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["Incident_Id"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident Form", "Form_Incident.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&Incident_Id=" + Request.QueryString["Incident_Id"] + ""), false);
          }
        }
      }
    }

    protected void FormView_Incident_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_Incident_Form.CurrentMode == FormViewMode.Insert)
      {
        if (Request.QueryString["s_Facility_Id"] != null)
        {
          InsertDataBound();
        }
      }

      if (FormView_Incident_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["Incident_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_Incident_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["Incident_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void InsertDataBound()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('1')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '20'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '131'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '3'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '132'");
            DataRow[] SecurityFacilityPharmacyManager = DataTable_FormMode.Select("SecurityRole_Id = '189'");
            DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '2'");
            DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '70'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
            {
              Session["Security"] = "0";
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Visible = true;
              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Visible = true;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Visible = true;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Visible = true;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmImpactImpactListTotal")).Visible = true;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmCost")).Visible = true;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmImplications")).Visible = true;
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Visible = true;
              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Visible = true;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Visible = true;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Visible = true;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmImpactImpactListTotal")).Visible = true;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmCost")).Visible = true;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmImplications")).Visible = true;
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAdminView.Length > 0)
            {
              Session["Security"] = "0";
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Visible = false;
              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Visible = false;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmImpactImpactListTotal")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmCost")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmImplications")).Visible = false;
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Visible = true;
              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Visible = true;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Visible = true;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Visible = true;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmImpactImpactListTotal")).Visible = true;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmCost")).Visible = true;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmImplications")).Visible = true;
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminView.Length > 0)
            {
              Session["Security"] = "0";
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Visible = false;
              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Visible = false;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmImpactImpactListTotal")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmCost")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmImplications")).Visible = false;
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityPharmacyManager.Length > 0)
            {
              Session["Security"] = "0";
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Visible = false;
              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Visible = false;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmImpactImpactListTotal")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmCost")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmImplications")).Visible = false;
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityApprover.Length > 0)
            {
              Session["Security"] = "0";
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Visible = false;
              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Visible = false;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmImpactImpactListTotal")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmCost")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmImplications")).Visible = false;
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityCapturer.Length > 0)
            {
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Visible = false;
              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Visible = false;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmImpactImpactListTotal")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmCost")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmImplications")).Visible = false;
            }
            Session["Security"] = "1";
          }
          else
          {
            ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Visible = false;
            ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Visible = false;
            ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Visible = false;
            ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Visible = false;
            ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmImpactImpactListTotal")).Visible = false;
            ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmCost")).Visible = false;
            ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertDegreeOfHarmImplications")).Visible = false;
          }
        }
      }
    }

    protected void EditDataBound()
    {
      DataView DataView_IncidentLevels = (DataView)SqlDataSource_Incident_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_IncidentLevels = DataView_IncidentLevels[0];
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel1List")).SelectedValue = Convert.ToString(DataRowView_IncidentLevels["Incident_Level1_List"], CultureInfo.CurrentCulture);
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).SelectedValue = Convert.ToString(DataRowView_IncidentLevels["Incident_Level2_List"], CultureInfo.CurrentCulture);
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).SelectedValue = Convert.ToString(DataRowView_IncidentLevels["Incident_Level3_List"], CultureInfo.CurrentCulture);

      EditDataBound_InvestigationCompleted();

      string FacilityFacilityDisplayName = "";
      string IncidentDegreeOfHarmDegreeOfHarmName = "";
      string SQLStringIncident = "SELECT Facility_FacilityDisplayName , Incident_DegreeOfHarm_DegreeOfHarm_Name FROM vForm_Incident WHERE Incident_Id = @Incident_Id";
      using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
      {
        SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
        DataTable DataTable_Incident;
        using (DataTable_Incident = new DataTable())
        {
          DataTable_Incident.Locale = CultureInfo.CurrentCulture;
          DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
          if (DataTable_Incident.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
            {
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              IncidentDegreeOfHarmDegreeOfHarmName = DataRow_Row["Incident_DegreeOfHarm_DegreeOfHarm_Name"].ToString();
            }
          }
        }
      }

      ((Label)FormView_Incident_Form.FindControl("Label_EditFacility")).Text = FacilityFacilityDisplayName;
      ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Text = IncidentDegreeOfHarmDegreeOfHarmName;

      FacilityFacilityDisplayName = "";
      IncidentDegreeOfHarmDegreeOfHarmName = "";


      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).Items.Clear();

      string SQLStringDoctor = "SELECT Facility_FacilityCode AS FacilityCode , Incident_P_VisitNumber AS VisitNumber , Incident_Pharmacy_DoctorName AS Practitioner FROM vForm_Incident WHERE Incident_Id = @Incident_Id AND Incident_Pharmacy_DoctorName IS NOT NULL";
      using (SqlCommand SqlCommand_Doctor = new SqlCommand(SQLStringDoctor))
      {
        SqlCommand_Doctor.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
        DataTable DataTable_Doctor;
        using (DataTable_Doctor = new DataTable())
        {
          DataTable_Doctor.Locale = CultureInfo.CurrentCulture;
          DataTable_Doctor.Merge(InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Doctor));
          DataTable_Doctor.Merge(InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPVisitNumber")).Text));

          DataTable_Doctor.DefaultView.Sort = "Practitioner ASC";

          ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).DataSource = DataTable_Doctor.DefaultView.ToTable(true, "Practitioner");
          ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).Items.Insert(0, new ListItem(Convert.ToString("Select Doctor", CultureInfo.CurrentCulture), ""));
          ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).DataBind();
        }
      }

      DropDownList DropDownList_EditPharmacyDoctorName = (DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName");
      DataView DataView_Doctor = (DataView)SqlDataSource_Incident_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_Doctor = DataView_Doctor[0];
      DropDownList_EditPharmacyDoctorName.SelectedValue = Convert.ToString(DataRowView_Doctor["Incident_Pharmacy_DoctorName"], CultureInfo.CurrentCulture);


      if (Request.QueryString["Incident_Id"] != null)
      {
        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 1";
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
          ((Button)FormView_Incident_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_Incident_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_Incident_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_Incident_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void EditDataBound_InvestigationCompleted()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('1')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '20'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '3'");
            DataRow[] SecurityFacilityPharmacyManager = DataTable_FormMode.Select("SecurityRole_Id = '189'");
            DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '2'");

            if (Request.QueryString["Incident_Id"] != null)
            {
              Session["IncidentStatus"] = "";
              string SQLStringIncidentStatus = "SELECT Incident_Status FROM Form_Incident WHERE Incident_Id = @Incident_Id";
              using (SqlCommand SqlCommand_IncidentStatus = new SqlCommand(SQLStringIncidentStatus))
              {
                SqlCommand_IncidentStatus.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
                DataTable DataTable_IncidentStatus;
                using (DataTable_IncidentStatus = new DataTable())
                {
                  DataTable_IncidentStatus.Locale = CultureInfo.CurrentCulture;
                  DataTable_IncidentStatus = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IncidentStatus).Copy();
                  if (DataTable_IncidentStatus.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_IncidentStatus.Rows)
                    {
                      Session["IncidentStatus"] = DataRow_Row["Incident_Status"];
                    }
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              if (Session["IncidentStatus"].ToString() == "Pending Approval")
              {
                ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Checked = false;
                ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = false;

                ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = false;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = false;
                ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = false;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = false;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = false;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = false;

                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = true;
                ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = true;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = true;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = true;
              }
              else
              {
                ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = true;

                ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = true;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = true;
                ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = true;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = true;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = true;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = true;

                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = false;
                ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = false;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = false;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = false;
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityPharmacyManager.Length > 0)
            {
              Session["Security"] = "0";
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = false;

              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = false;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = false;

              ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = true;
              ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = true;
              ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = true;
              ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = true;
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityApprover.Length > 0)
            {
              Session["Security"] = "0";
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = false;

              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = false;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = false;

              ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = true;
              ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = true;
              ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = true;
              ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = true;
            }
            Session["Security"] = "1";

            Session["IncidentStatus"] = "";
          }
          else
          {
            ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = false;

            ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = false;
            ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = false;
            ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = false;
            ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = false;
            ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = false;
            ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = false;

            ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = true;
            ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = true;
            ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = true;
            ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = true;
          }
        }
      }
    }

    protected void ReadOnlyDataBound()
    {
      string FacilityFacilityDisplayName = "";
      string IncidentFacilityFromFacilityFacilityDisplayName = "";
      string IncidentUnitFromName = "";
      string IncidentUnitToName = "";
      string IncidentIncidentCategoryName = "";
      string IncidentEEmployeeUnitName = "";
      string IncidentEEmployeeStatusName = "";
      string IncidentEStaffCategoryName = "";
      string IncidentEBodyPartAffectedName = "";
      string IncidentETreatmentRequiredName = "";
      string IncidentEMainContributorName = "";
      string IncidentEMainContributorStaffName = "";
      string IncidentMMDTDisciplineName = "";
      string IncidentPMainContributorName = "";
      string IncidentPMainContributorStaffName = "";
      string IncidentPropMainContributorName = "";
      string IncidentPropMainContributorStaffName = "";
      string IncidentLevel1Name = "";
      string IncidentLevel2Name = "";
      string IncidentLevel3Name = "";
      string IncidentSeverityName = "";
      string IncidentPatientFallingWhereFallOccurName = "";
      string IncidentDegreeOfHarmDegreeOfHarmName = "";
      string IncidentPharmacyStaffInvolvedName = "";
      string IncidentPharmacyCheckingName = "";
      string IncidentPharmacyStaffOnDutyName = "";
      string IncidentPharmacyTypeOfPrescriptionName = "";
      string IncidentPharmacyFactorsName = "";
      string IncidentPharmacySystemRelatedIssuesName = "";
      string IncidentPharmacyErgonomicProblemsName = "";
      string IncidentPharmacyLocationName = "";
      string IncidentRootCategoryName = "";
      string IncidentDisciplineName = "";
      string SQLStringIncident = "SELECT Facility_FacilityDisplayName , Incident_FacilityFrom_Facility_FacilityDisplayName , Incident_UnitFrom_Name , Incident_UnitTo_Name , Incident_IncidentCategory_Name , Incident_E_EmployeeUnit_Name , Incident_E_EmployeeStatus_Name , Incident_E_StaffCategory_Name , Incident_E_BodyPartAffected_Name , Incident_E_TreatmentRequired_Name , Incident_E_MainContributor_Name , Incident_E_MainContributor_Staff_Name , Incident_MMDT_Discipline_Name , Incident_P_MainContributor_Name , Incident_P_MainContributor_Staff_Name , Incident_Prop_MainContributor_Name , Incident_Prop_MainContributor_Staff_Name , Incident_Level1_Name , Incident_Level2_Name , Incident_Level3_Name , Incident_Severity_Name , Incident_PatientFalling_WhereFallOccur_Name , Incident_DegreeOfHarm_DegreeOfHarm_Name , Incident_Pharmacy_StaffInvolved_Name , Incident_Pharmacy_Checking_Name , Incident_Pharmacy_StaffOnDuty_Name , Incident_Pharmacy_TypeOfPrescription_Name , Incident_Pharmacy_Factors_Name , Incident_Pharmacy_SystemRelatedIssues_Name , Incident_Pharmacy_ErgonomicProblems_Name , Incident_Pharmacy_Location_Name , Incident_RootCategory_Name , Incident_Discipline_Name FROM vForm_Incident WHERE Incident_Id = @Incident_Id";
      using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
      {
        SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
        DataTable DataTable_Incident;
        using (DataTable_Incident = new DataTable())
        {
          DataTable_Incident.Locale = CultureInfo.CurrentCulture;
          DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
          if (DataTable_Incident.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
            {
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              IncidentFacilityFromFacilityFacilityDisplayName = DataRow_Row["Incident_FacilityFrom_Facility_FacilityDisplayName"].ToString();
              IncidentUnitFromName = DataRow_Row["Incident_UnitFrom_Name"].ToString();
              IncidentUnitToName = DataRow_Row["Incident_UnitTo_Name"].ToString();
              IncidentIncidentCategoryName = DataRow_Row["Incident_IncidentCategory_Name"].ToString();
              IncidentEEmployeeUnitName = DataRow_Row["Incident_E_EmployeeUnit_Name"].ToString();
              IncidentEEmployeeStatusName = DataRow_Row["Incident_E_EmployeeStatus_Name"].ToString();
              IncidentEStaffCategoryName = DataRow_Row["Incident_E_StaffCategory_Name"].ToString();
              IncidentEBodyPartAffectedName = DataRow_Row["Incident_E_BodyPartAffected_Name"].ToString();
              IncidentETreatmentRequiredName = DataRow_Row["Incident_E_TreatmentRequired_Name"].ToString();
              IncidentEMainContributorName = DataRow_Row["Incident_E_MainContributor_Name"].ToString();
              IncidentEMainContributorStaffName = DataRow_Row["Incident_E_MainContributor_Staff_Name"].ToString();
              IncidentMMDTDisciplineName = DataRow_Row["Incident_MMDT_Discipline_Name"].ToString();
              IncidentPMainContributorName = DataRow_Row["Incident_P_MainContributor_Name"].ToString();
              IncidentPMainContributorStaffName = DataRow_Row["Incident_P_MainContributor_Staff_Name"].ToString();
              IncidentPropMainContributorName = DataRow_Row["Incident_Prop_MainContributor_Name"].ToString();
              IncidentPropMainContributorStaffName = DataRow_Row["Incident_Prop_MainContributor_Staff_Name"].ToString();
              IncidentLevel1Name = DataRow_Row["Incident_Level1_Name"].ToString();
              IncidentLevel2Name = DataRow_Row["Incident_Level2_Name"].ToString();
              IncidentLevel3Name = DataRow_Row["Incident_Level3_Name"].ToString();
              IncidentSeverityName = DataRow_Row["Incident_Severity_Name"].ToString();
              IncidentPatientFallingWhereFallOccurName = DataRow_Row["Incident_PatientFalling_WhereFallOccur_Name"].ToString();
              IncidentDegreeOfHarmDegreeOfHarmName = DataRow_Row["Incident_DegreeOfHarm_DegreeOfHarm_Name"].ToString();
              IncidentPharmacyStaffInvolvedName = DataRow_Row["Incident_Pharmacy_StaffInvolved_Name"].ToString();
              IncidentPharmacyCheckingName = DataRow_Row["Incident_Pharmacy_Checking_Name"].ToString();
              IncidentPharmacyStaffOnDutyName = DataRow_Row["Incident_Pharmacy_StaffOnDuty_Name"].ToString();
              IncidentPharmacyTypeOfPrescriptionName = DataRow_Row["Incident_Pharmacy_TypeOfPrescription_Name"].ToString();
              IncidentPharmacyFactorsName = DataRow_Row["Incident_Pharmacy_Factors_Name"].ToString();
              IncidentPharmacySystemRelatedIssuesName = DataRow_Row["Incident_Pharmacy_SystemRelatedIssues_Name"].ToString();
              IncidentPharmacyErgonomicProblemsName = DataRow_Row["Incident_Pharmacy_ErgonomicProblems_Name"].ToString();
              IncidentPharmacyLocationName = DataRow_Row["Incident_Pharmacy_Location_Name"].ToString();
              IncidentRootCategoryName = DataRow_Row["Incident_RootCategory_Name"].ToString();
              IncidentDisciplineName = DataRow_Row["Incident_Discipline_Name"].ToString();
            }
          }
        }
      }


      ((Label)FormView_Incident_Form.FindControl("Label_ItemFacility")).Text = FacilityFacilityDisplayName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemFacilityFrom")).Text = IncidentFacilityFromFacilityFacilityDisplayName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemUnitFromUnit")).Text = IncidentUnitFromName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemUnitToUnit")).Text = IncidentUnitToName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemIncidentCategoryList")).Text = IncidentIncidentCategoryName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemEEmployeeUnitUnit")).Text = IncidentEEmployeeUnitName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemEEmployeeStatusList")).Text = IncidentEEmployeeStatusName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemEStaffCategoryList")).Text = IncidentEStaffCategoryName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemEBodyPartAffectedList")).Text = IncidentEBodyPartAffectedName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemETreatmentRequiredList")).Text = IncidentETreatmentRequiredName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemEMainContributorList")).Text = IncidentEMainContributorName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemEMainContributorStaffList")).Text = IncidentEMainContributorStaffName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemMMDTDisciplineList")).Text = IncidentMMDTDisciplineName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPMainContributorList")).Text = IncidentPMainContributorName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPMainContributorStaffList")).Text = IncidentPMainContributorStaffName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPropMainContributorList")).Text = IncidentPropMainContributorName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPropMainContributorStaffList")).Text = IncidentPropMainContributorStaffName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemLevel1List")).Text = IncidentLevel1Name;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemLevel2List")).Text = IncidentLevel2Name;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemLevel3List")).Text = IncidentLevel3Name;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemSeverityList")).Text = IncidentSeverityName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPatientFallingWhereFallOccurList")).Text = IncidentPatientFallingWhereFallOccurName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemDegreeOfHarmList")).Text = IncidentDegreeOfHarmDegreeOfHarmName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPharmacyStaffInvolvedList")).Text = IncidentPharmacyStaffInvolvedName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPharmacyCheckingList")).Text = IncidentPharmacyCheckingName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPharmacyStaffOnDutyList")).Text = IncidentPharmacyStaffOnDutyName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPharmacyTypeOfPrescriptionList")).Text = IncidentPharmacyTypeOfPrescriptionName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPharmacyFactorsList")).Text = IncidentPharmacyFactorsName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPharmacySystemRelatedIssuesList")).Text = IncidentPharmacySystemRelatedIssuesName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPharmacyErgonomicProblemsList")).Text = IncidentPharmacyErgonomicProblemsName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemPharmacyLocationList")).Text = IncidentPharmacyLocationName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemRootCategoryList")).Text = IncidentRootCategoryName;
      ((Label)FormView_Incident_Form.FindControl("Label_ItemDisciplineList")).Text = IncidentDisciplineName;

      FacilityFacilityDisplayName = "";
      IncidentFacilityFromFacilityFacilityDisplayName = "";
      IncidentUnitFromName = "";
      IncidentUnitToName = "";
      IncidentIncidentCategoryName = "";
      IncidentEEmployeeUnitName = "";
      IncidentEEmployeeStatusName = "";
      IncidentEStaffCategoryName = "";
      IncidentEBodyPartAffectedName = "";
      IncidentETreatmentRequiredName = "";
      IncidentEMainContributorName = "";
      IncidentEMainContributorStaffName = "";
      IncidentMMDTDisciplineName = "";
      IncidentPMainContributorName = "";
      IncidentPMainContributorStaffName = "";
      IncidentPropMainContributorName = "";
      IncidentPropMainContributorStaffName = "";
      IncidentLevel1Name = "";
      IncidentLevel2Name = "";
      IncidentLevel3Name = "";
      IncidentSeverityName = "";
      IncidentPatientFallingWhereFallOccurName = "";
      IncidentDegreeOfHarmDegreeOfHarmName = "";
      IncidentPharmacyStaffInvolvedName = "";
      IncidentPharmacyCheckingName = "";
      IncidentPharmacyStaffOnDutyName = "";
      IncidentPharmacyTypeOfPrescriptionName = "";
      IncidentPharmacyFactorsName = "";
      IncidentPharmacySystemRelatedIssuesName = "";
      IncidentPharmacyErgonomicProblemsName = "";
      IncidentPharmacyLocationName = "";
      IncidentRootCategoryName = "";
      IncidentDisciplineName = "";


      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 1";
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
        ((Button)FormView_Incident_Form.FindControl("Button_ItemPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_Incident_Form.FindControl("Button_ItemPrint")).Visible = true;
        ((Button)FormView_Incident_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident Print", "InfoQuest_Print.aspx?PrintPage=Form_Incident&PrintValue=" + Request.QueryString["Incident_Id"] + "") + "')";
      }

      if (Email == "False")
      {
        ((Button)FormView_Incident_Form.FindControl("Button_ItemEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_Incident_Form.FindControl("Button_ItemEmail")).Visible = true;
        ((Button)FormView_Incident_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident Email", "InfoQuest_Email.aspx?EmailPage=Form_Incident&EmailValue=" + Request.QueryString["Incident_Id"] + "") + "')";
      }

      Email = "";
      Print = "";
    }

    protected string IncidentReportable_TriggerUnit(string formViewMode)
    {
      string TriggerValue = "";
      string SQLStringTriggerUnit = "SELECT DISTINCT ListItem_Parent FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 157 AND ListItem_Name = @ListItem_Name";
      using (SqlCommand SqlCommand_TriggerUnit = new SqlCommand(SQLStringTriggerUnit))
      {
        SqlCommand_TriggerUnit.Parameters.AddWithValue("@ListItem_Name", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "UnitToUnit")).SelectedValue);
        DataTable DataTable_TriggerUnit;
        using (DataTable_TriggerUnit = new DataTable())
        {
          DataTable_TriggerUnit.Locale = CultureInfo.CurrentCulture;
          DataTable_TriggerUnit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerUnit).Copy();
          if (DataTable_TriggerUnit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TriggerUnit.Rows)
            {
              if (TriggerValue.IndexOf(DataRow_Row["ListItem_Parent"].ToString(), StringComparison.CurrentCulture) == -1)
              {
                if (!string.IsNullOrEmpty(TriggerValue))
                {
                  TriggerValue = TriggerValue + ":";
                }

                TriggerValue = TriggerValue + DataRow_Row["ListItem_Parent"].ToString();
              }
            }
          }
        }
      }

      return TriggerValue;
    }

    protected string IncidentReportable_TriggerLevel(string formViewMode)
    {
      string TriggerValue = "";
      string SQLStringTriggerLevel = @"SELECT	DISTINCT 
                                              CASE 
					                                      WHEN TempTableB.ListItem_Name IS NULL THEN TempTableA.ListItem_Name + ';Yes' 
					                                      ELSE TempTableA.ListItem_Name + ';No' 
				                                      END AS ListItem_Name 
                                      FROM		( 
                                                SELECT		ListItem_Name , 
                                                          ListItem_Parent , 
                                                          ListCategory_Id 
                                                FROM			vAdministration_ListItem_Active 
                                                WHERE			ListCategory_Id = 153 
                                              ) TempTableA 
                                              LEFT JOIN 
                                              ( 
                                                SELECT		ListItem_Name , 
                                                          ListCategory_Id 
                                                FROM			vAdministration_ListItem_Active 
                                                WHERE			ListCategory_Id = 162 
                                              ) TempTableB 
                                              ON TempTableA.ListItem_Name = TempTableB.ListItem_Name 
                                      WHERE		TempTableA.ListItem_Parent IN ( 
                                                SELECT	ListItem_Parent 
                                                FROM		vAdministration_ListItem_Active 
                                                WHERE		ListCategory_Id = 148	
                                                        AND ListItem_Name = @Level1 
                                                UNION 
                                                SELECT	ListItem_Parent 
                                                FROM		vAdministration_ListItem_Active 
                                                WHERE		ListCategory_Id = 149 
                                                        AND ListItem_Name = @Level2 
                                                UNION 
                                                SELECT	ListItem_Parent 
                                                FROM		vAdministration_ListItem_Active 
                                                WHERE		ListCategory_Id = 150 
                                                        AND ListItem_Name = @Level3 
                                              )";
      using (SqlCommand SqlCommand_TriggerLevel = new SqlCommand(SQLStringTriggerLevel))
      {
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level1", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level1List")).SelectedValue);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level2", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level2List")).SelectedValue);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level3", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level3List")).SelectedValue);
        DataTable DataTable_TriggerLevel;
        using (DataTable_TriggerLevel = new DataTable())
        {
          DataTable_TriggerLevel.Locale = CultureInfo.CurrentCulture;
          DataTable_TriggerLevel = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerLevel).Copy();
          if (DataTable_TriggerLevel.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TriggerLevel.Rows)
            {
              if (TriggerValue.IndexOf(DataRow_Row["ListItem_Name"].ToString(), StringComparison.CurrentCulture) == -1)
              {
                if (!string.IsNullOrEmpty(TriggerValue))
                {
                  TriggerValue = TriggerValue + ":";
                }

                TriggerValue = TriggerValue + DataRow_Row["ListItem_Name"].ToString();
              }
            }
          }
        }
      }

      if (string.IsNullOrEmpty(TriggerValue))
      {
        ((HtmlTableRow)FormView_Incident_Form.FindControl("IncidentReportableTriggerLevel")).Visible = false;
        ((Label)FormView_Incident_Form.FindControl("Label_" + formViewMode + "IncidentReportableTriggerLevel")).Text = "";
      }
      else
      {
        ((HtmlTableRow)FormView_Incident_Form.FindControl("IncidentReportableTriggerLevel")).Visible = true;
        ((Label)FormView_Incident_Form.FindControl("Label_" + formViewMode + "IncidentReportableTriggerLevel")).Text = Convert.ToString("Incident Reportable fields are compulsary for Type of Incident Level 1, Level 2 or Level 3 selected", CultureInfo.CurrentCulture);
      }

      return TriggerValue;
    }

    protected string PatientFalling_TriggerLevel(string formViewMode)
    {
      string TriggerValue = "";
      string SQLStringTriggerLevel = @" SELECT	DISTINCT ListItem_Name
                                        FROM		vAdministration_ListItem_Active 
                                        WHERE		ListItem_Id IN ( 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		ListCategory_Id = 228	
                                                          AND ListItem_Name = @Level1 
                                                  UNION 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		ListCategory_Id = 229 
                                                          AND ListItem_Name = @Level2 
                                                  UNION 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		ListCategory_Id = 230 
                                                          AND ListItem_Name = @Level3 
                                                )";
      using (SqlCommand SqlCommand_TriggerLevel = new SqlCommand(SQLStringTriggerLevel))
      {
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level1", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level1List")).SelectedValue);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level2", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level2List")).SelectedValue);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level3", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level3List")).SelectedValue);
        DataTable DataTable_TriggerLevel;
        using (DataTable_TriggerLevel = new DataTable())
        {
          DataTable_TriggerLevel.Locale = CultureInfo.CurrentCulture;
          DataTable_TriggerLevel = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerLevel).Copy();
          if (DataTable_TriggerLevel.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TriggerLevel.Rows)
            {
              if (TriggerValue.IndexOf(DataRow_Row["ListItem_Name"].ToString(), StringComparison.CurrentCulture) == -1)
              {
                if (!string.IsNullOrEmpty(TriggerValue))
                {
                  TriggerValue = TriggerValue + ":";
                }

                TriggerValue = TriggerValue + DataRow_Row["ListItem_Name"].ToString();
              }
            }
          }
        }
      }

      return TriggerValue;
    }

    protected string PatientDetail_TriggerLevel(string formViewMode)
    {
      string TriggerValue = "";
      string SQLStringTriggerLevel = @" SELECT	DISTINCT ListItem_Name
                                        FROM		vAdministration_ListItem_Active 
                                        WHERE		ListItem_Id IN ( 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		ListCategory_Id = 182	
                                                          AND ListItem_Name = @Level1 
                                                  UNION 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		ListCategory_Id = 183 
                                                          AND ListItem_Name = @Level2 
                                                  UNION 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		ListCategory_Id = 184 
                                                          AND ListItem_Name = @Level3 
                                                )";
      using (SqlCommand SqlCommand_TriggerLevel = new SqlCommand(SQLStringTriggerLevel))
      {
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level1", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level1List")).SelectedValue);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level2", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level2List")).SelectedValue);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level3", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level3List")).SelectedValue);
        DataTable DataTable_TriggerLevel;
        using (DataTable_TriggerLevel = new DataTable())
        {
          DataTable_TriggerLevel.Locale = CultureInfo.CurrentCulture;
          DataTable_TriggerLevel = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerLevel).Copy();
          if (DataTable_TriggerLevel.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TriggerLevel.Rows)
            {
              if (TriggerValue.IndexOf(DataRow_Row["ListItem_Name"].ToString(), StringComparison.CurrentCulture) == -1)
              {
                if (!string.IsNullOrEmpty(TriggerValue))
                {
                  TriggerValue = TriggerValue + ":";
                }

                TriggerValue = TriggerValue + DataRow_Row["ListItem_Name"].ToString();
              }
            }
          }
        }
      }

      return TriggerValue;
    }

    protected string Pharmacy_TriggerLevel()
    {
      string TriggerValue = "";
      string SQLStringTriggerLevel = @" SELECT	Incident_Id
                                        FROM		Form_Incident
                                        WHERE		Incident_Id = @Incident_Id
				                                        AND (
					                                        Incident_Level1_List IN ( SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 195 )
					                                        OR Incident_Level2_List IN ( SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 196 )
					                                        OR Incident_Level3_List IN ( SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 197 )
				                                        )";
      using (SqlCommand SqlCommand_TriggerLevel = new SqlCommand(SQLStringTriggerLevel))
      {
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
        DataTable DataTable_TriggerLevel;
        using (DataTable_TriggerLevel = new DataTable())
        {
          DataTable_TriggerLevel.Locale = CultureInfo.CurrentCulture;
          DataTable_TriggerLevel = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerLevel).Copy();
          if (DataTable_TriggerLevel.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TriggerLevel.Rows)
            {
              if (TriggerValue.IndexOf(DataRow_Row["Incident_Id"].ToString(), StringComparison.CurrentCulture) == -1)
              {
                if (!string.IsNullOrEmpty(TriggerValue))
                {
                  TriggerValue = TriggerValue + ":";
                }

                TriggerValue = TriggerValue + DataRow_Row["Incident_Id"].ToString();
              }
            }
          }
        }
      }

      return TriggerValue;
    }

    protected string Pharmacy_TriggerLevel(string formViewMode)
    {
      string TriggerValue = "";
      string SQLStringTriggerLevel = @" SELECT	DISTINCT ListItem_Name
                                        FROM		vAdministration_ListItem_Active 
                                        WHERE		ListItem_Id IN ( 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		ListCategory_Id = 195	
                                                          AND ListItem_Name = @Level1 
                                                  UNION 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		ListCategory_Id = 196 
                                                          AND ListItem_Name = @Level2 
                                                  UNION 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		ListCategory_Id = 197 
                                                          AND ListItem_Name = @Level3 
                                                )";
      using (SqlCommand SqlCommand_TriggerLevel = new SqlCommand(SQLStringTriggerLevel))
      {
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level1", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level1List")).SelectedValue);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level2", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level2List")).SelectedValue);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level3", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_" + formViewMode + "Level3List")).SelectedValue);
        DataTable DataTable_TriggerLevel;
        using (DataTable_TriggerLevel = new DataTable())
        {
          DataTable_TriggerLevel.Locale = CultureInfo.CurrentCulture;
          DataTable_TriggerLevel = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerLevel).Copy();
          if (DataTable_TriggerLevel.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TriggerLevel.Rows)
            {
              if (TriggerValue.IndexOf(DataRow_Row["ListItem_Name"].ToString(), StringComparison.CurrentCulture) == -1)
              {
                if (!string.IsNullOrEmpty(TriggerValue))
                {
                  TriggerValue = TriggerValue + ":";
                }

                TriggerValue = TriggerValue + DataRow_Row["ListItem_Name"].ToString();
              }
            }
          }
        }
      }

      return TriggerValue;
    }

    protected void DegreeOfHarm_InsertDropDownList()
    {
      ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Items.Clear();

      string SQLStringDegreeOfHarm = "SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id IN (158) AND ListItem_Parent = @ListItem_Parent ORDER BY ListItem_Name";
      using (SqlCommand SqlCommand_DegreeOfHarm = new SqlCommand(SQLStringDegreeOfHarm))
      {
        SqlCommand_DegreeOfHarm.Parameters.AddWithValue("@ListItem_Parent", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertIncidentCategoryList")).SelectedValue);
        DataTable DataTable_DegreeOfHarm;
        using (DataTable_DegreeOfHarm = new DataTable())
        {
          DataTable_DegreeOfHarm.Locale = CultureInfo.CurrentCulture;
          DataTable_DegreeOfHarm = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_DegreeOfHarm).Copy();
          if (DataTable_DegreeOfHarm.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DegreeOfHarm.Rows)
            {
              string DegreeOfHarm_ListItemId = DataRow_Row["ListItem_Id"].ToString();
              string DegreeOfHarm_ListItemName = DataRow_Row["ListItem_Name"].ToString();
              string DegreeOfHarmDescription_ListItemName = "";

              string SQLStringDegreeOfHarmDescription = "SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id IN (159) AND ListItem_Parent = @ListItem_Parent ORDER BY ListItem_Name";
              using (SqlCommand SqlCommand_DegreeOfHarmDescription = new SqlCommand(SQLStringDegreeOfHarmDescription))
              {
                SqlCommand_DegreeOfHarmDescription.Parameters.AddWithValue("@ListItem_Parent", DegreeOfHarm_ListItemId);
                DataTable DataTable_DegreeOfHarmDescription;
                using (DataTable_DegreeOfHarmDescription = new DataTable())
                {
                  DataTable_DegreeOfHarmDescription.Locale = CultureInfo.CurrentCulture;
                  DataTable_DegreeOfHarmDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_DegreeOfHarmDescription).Copy();
                  if (DataTable_DegreeOfHarmDescription.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_RowDescription in DataTable_DegreeOfHarmDescription.Rows)
                    {
                      if (DataTable_DegreeOfHarmDescription.Rows.Count == 1)
                      {
                        DegreeOfHarmDescription_ListItemName = DataRow_RowDescription["ListItem_Name"].ToString();
                      }
                      else
                      {
                        DegreeOfHarmDescription_ListItemName = DegreeOfHarmDescription_ListItemName + "<br />" + DataRow_RowDescription["ListItem_Name"].ToString();
                      }
                    }
                  }
                }
              }

              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Items.Add(new ListItem(Convert.ToString(DegreeOfHarm_ListItemName + "&nbsp;&nbsp;&nbsp;<a href=\"#\" class=\"tt\"><img height=\"11\" alt=\"\" src=\"App_Images/Information_16x16.png\" style=\"border: 0px\"><span class=\"tooltip\"><span class=\"middle\">" + DegreeOfHarmDescription_ListItemName + "</span></span></a>", CultureInfo.CurrentCulture), DegreeOfHarm_ListItemId));
            }

            ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertDegreeOfHarmListTotal")).Value = DataTable_DegreeOfHarm.Rows.Count.ToString(CultureInfo.CurrentCulture);
          }
        }
      }
    }

    protected void DegreeOfHarm_EditDropDownList()
    {
      ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Items.Clear();

      string SQLStringDegreeOfHarm = @"SELECT ListItem_Id , ListItem_Name 
                                     FROM vAdministration_ListItem_Active 
                                     WHERE ListCategory_Id IN (158) AND ListItem_Parent = @ListItem_Parent 
                                     UNION 
                                     SELECT Incident_DegreeOfHarm_DegreeOfHarm_List , Incident_DegreeOfHarm_DegreeOfHarm_Name 
                                     FROM vForm_Incident 
                                     WHERE Incident_Id = @Incident_Id 
                                     ORDER BY ListItem_Name";
      using (SqlCommand SqlCommand_DegreeOfHarm = new SqlCommand(SQLStringDegreeOfHarm))
      {
        SqlCommand_DegreeOfHarm.Parameters.AddWithValue("@ListItem_Parent", ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditIncidentCategoryList")).SelectedValue);
        if (string.IsNullOrEmpty(Request.QueryString["Incident_Id"]))
        {
          SqlCommand_DegreeOfHarm.Parameters.AddWithValue("@Incident_Id", "");
        }
        else
        {
          SqlCommand_DegreeOfHarm.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
        }
        DataTable DataTable_DegreeOfHarm;
        using (DataTable_DegreeOfHarm = new DataTable())
        {
          DataTable_DegreeOfHarm.Locale = CultureInfo.CurrentCulture;
          DataTable_DegreeOfHarm = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_DegreeOfHarm).Copy();
          if (DataTable_DegreeOfHarm.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DegreeOfHarm.Rows)
            {
              string DegreeOfHarm_ListItemId = DataRow_Row["ListItem_Id"].ToString();
              string DegreeOfHarm_ListItemName = DataRow_Row["ListItem_Name"].ToString();
              string DegreeOfHarmDescription_ListItemName = "";

              string SQLStringDegreeOfHarmDescription = "SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id IN (159) AND ListItem_Parent = @ListItem_Parent ORDER BY ListItem_Name";
              using (SqlCommand SqlCommand_DegreeOfHarmDescription = new SqlCommand(SQLStringDegreeOfHarmDescription))
              {
                SqlCommand_DegreeOfHarmDescription.Parameters.AddWithValue("@ListItem_Parent", DegreeOfHarm_ListItemId);
                DataTable DataTable_DegreeOfHarmDescription;
                using (DataTable_DegreeOfHarmDescription = new DataTable())
                {
                  DataTable_DegreeOfHarmDescription.Locale = CultureInfo.CurrentCulture;
                  DataTable_DegreeOfHarmDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_DegreeOfHarmDescription).Copy();
                  if (DataTable_DegreeOfHarmDescription.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_RowDescription in DataTable_DegreeOfHarmDescription.Rows)
                    {
                      if (DataTable_DegreeOfHarmDescription.Rows.Count == 1)
                      {
                        DegreeOfHarmDescription_ListItemName = DataRow_RowDescription["ListItem_Name"].ToString();
                      }
                      else
                      {
                        DegreeOfHarmDescription_ListItemName = DegreeOfHarmDescription_ListItemName + "<br />" + DataRow_RowDescription["ListItem_Name"].ToString();
                      }
                    }
                  }
                }
              }

              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Items.Add(new ListItem(Convert.ToString(DegreeOfHarm_ListItemName + "&nbsp;&nbsp;&nbsp;<a href=\"#\" class=\"tt\"><img height=\"11\" alt=\"\" src=\"App_Images/Information_16x16.png\" style=\"border: 0px\"><span class=\"tooltip\"><span class=\"middle\">" + DegreeOfHarmDescription_ListItemName + "</span></span></a>", CultureInfo.CurrentCulture), DegreeOfHarm_ListItemId));
            }

            ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Value = DataTable_DegreeOfHarm.Rows.Count.ToString(CultureInfo.CurrentCulture);
          }
        }
      }
    }


    protected static string GetIncidentCreatedBySurnameName(string incidentCreatedBy)
    {
      string IncidentCreatedBySurnameName = "";
      string SQLStringIncidentCreatedBySurnameName = "SELECT SecurityUser_DisplayName FROM Administration_SecurityUser WHERE SecurityUser_UserName = @SecurityUser_UserName";
      using (SqlCommand SqlCommand_IncidentCreatedBySurnameName = new SqlCommand(SQLStringIncidentCreatedBySurnameName))
      {
        SqlCommand_IncidentCreatedBySurnameName.Parameters.AddWithValue("@SecurityUser_UserName", incidentCreatedBy);
        DataTable DataTable_IncidentCreatedBySurnameName;
        using (DataTable_IncidentCreatedBySurnameName = new DataTable())
        {
          DataTable_IncidentCreatedBySurnameName.Locale = CultureInfo.CurrentCulture;
          DataTable_IncidentCreatedBySurnameName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IncidentCreatedBySurnameName).Copy();
          if (DataTable_IncidentCreatedBySurnameName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_IncidentCreatedBySurnameName.Rows)
            {
              IncidentCreatedBySurnameName = DataRow_Row["SecurityUser_DisplayName"].ToString();
            }
          }
        }
      }

      return IncidentCreatedBySurnameName;
    }

    protected static void EmailTriggerLevel_Insert(string incident_Id)
    {
      string IncidentId = "";
      string FacilityId = "";
      string FacilityFacilityDisplayName = "";
      string IncidentReportNumber = "";
      string IncidentDescription = "";
      string IncidentLevel1List = "";
      string IncidentLevel1Name = "";
      string IncidentLevel2List = "";
      string IncidentLevel2Name = "";
      string IncidentLevel3List = "";
      string IncidentLevel3Name = "";
      string IncidentCreatedBy = "";
      string SQLStringIncident = @"SELECT	Incident_Id , 
                                          Facility_Id , 
                                          Facility_FacilityDisplayName , 
                                          Incident_ReportNumber , 
                                          Incident_Description , 
                                          Incident_Level1_List , 
                                          Incident_Level1_Name , 
                                          Incident_Level2_List , 
                                          Incident_Level2_Name , 
                                          Incident_Level3_List , 
                                          Incident_Level3_Name , 
                                          Incident_CreatedBy 
                                  FROM		vForm_Incident 
                                  WHERE		Incident_Id = @Incident_Id";
      using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
      {
        SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", incident_Id);
        DataTable DataTable_Incident;
        using (DataTable_Incident = new DataTable())
        {
          DataTable_Incident.Locale = CultureInfo.CurrentCulture;
          DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
          if (DataTable_Incident.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
            {
              IncidentId = DataRow_Row["Incident_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
              IncidentDescription = DataRow_Row["Incident_Description"].ToString();
              IncidentLevel1List = DataRow_Row["Incident_Level1_List"].ToString();
              IncidentLevel1Name = DataRow_Row["Incident_Level1_Name"].ToString();
              IncidentLevel2List = DataRow_Row["Incident_Level2_List"].ToString();
              IncidentLevel2Name = DataRow_Row["Incident_Level2_Name"].ToString();
              IncidentLevel3List = DataRow_Row["Incident_Level3_List"].ToString();
              IncidentLevel3Name = DataRow_Row["Incident_Level3_Name"].ToString();
              IncidentCreatedBy = DataRow_Row["Incident_CreatedBy"].ToString();
            }
          }
        }
      }

      string IncidentCreatedBySurnameName = GetIncidentCreatedBySurnameName(IncidentCreatedBy);

      string TriggerLevelEmailAddress = "";
      string TriggerLevelEmailTemplate = "";
      string SQLStringTriggerLevel = @"SELECT	DISTINCT 
                                              TempTableA.ListItem_Name AS TriggerLevel_EmailAddress , 
                                              TempTableB.ListItem_Name AS TriggerLevel_EmailTemplate 
                                      FROM		vAdministration_ListItem_Active AS TempTableA 
                                              LEFT JOIN vAdministration_ListItem_Active AS TempTableB ON TempTableA.ListItem_Parent = TempTableB.ListItem_Parent 
                                      WHERE		TempTableA.ListCategory_Id = 147 
                                              AND TempTableB.ListCategory_Id = 154 
                                              AND TempTableA.ListItem_Parent IN ( 
                                                SELECT	ListItem_Parent 
                                                FROM		vAdministration_ListItem_Active 
                                                WHERE		(ListCategory_Id = 148 AND ListItem_Name = @Level1) 
                                                        OR (ListCategory_Id = 149 AND ListItem_Name = @Level2) 
                                                        OR (ListCategory_Id = 150 AND ListItem_Name = @Level3) 
                                              )";
      using (SqlCommand SqlCommand_TriggerLevel = new SqlCommand(SQLStringTriggerLevel))
      {
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level1", IncidentLevel1List);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level2", IncidentLevel2List);
        SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level3", IncidentLevel3List);
        DataTable DataTable_TriggerLevel;
        using (DataTable_TriggerLevel = new DataTable())
        {
          DataTable_TriggerLevel.Locale = CultureInfo.CurrentCulture;
          DataTable_TriggerLevel = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerLevel).Copy();
          if (DataTable_TriggerLevel.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TriggerLevel.Rows)
            {
              TriggerLevelEmailAddress = DataRow_Row["TriggerLevel_EmailAddress"].ToString();
              TriggerLevelEmailTemplate = DataRow_Row["TriggerLevel_EmailTemplate"].ToString();

              if (!string.IsNullOrEmpty(TriggerLevelEmailAddress) && !string.IsNullOrEmpty(TriggerLevelEmailTemplate))
              {
                string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate(TriggerLevelEmailTemplate);
                string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
                string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("1");
                string BodyString = EmailTemplate;

                BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + TriggerLevelEmailAddress + "");
                BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");
                BodyString = BodyString.Replace(";replace;IncidentDescription;replace;", "" + IncidentDescription + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel1Name;replace;", "" + IncidentLevel1Name + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel2Name;replace;", "" + IncidentLevel2Name + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel3Name;replace;", "" + IncidentLevel3Name + "");
                BodyString = BodyString.Replace(";replace;IncidentCreatedBy;replace;", "" + IncidentCreatedBySurnameName + " (" + IncidentCreatedBy + ")");
                BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
                BodyString = BodyString.Replace(";replace;FacilityId;replace;", "" + FacilityId + "");
                BodyString = BodyString.Replace(";replace;IncidentId;replace;", "" + IncidentId + "");

                string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                string EmailBody = HeaderString + BodyString + FooterString;

                string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", TriggerLevelEmailAddress, FormName, EmailBody);

                if (!string.IsNullOrEmpty(EmailSend))
                {
                  EmailSend = "";
                }

                EmailBody = "";
                EmailTemplate = "";
                URLAuthority = "";
                FormName = "";
              }
            }
          }
        }
      }

      IncidentId = "";
      FacilityId = "";
      FacilityFacilityDisplayName = "";
      IncidentReportNumber = "";
      IncidentDescription = "";
      IncidentLevel1List = "";
      IncidentLevel1Name = "";
      IncidentLevel2List = "";
      IncidentLevel2Name = "";
      IncidentLevel3List = "";
      IncidentLevel3Name = "";
      IncidentCreatedBy = "";
      IncidentCreatedBySurnameName = "";

      TriggerLevelEmailAddress = "";
      TriggerLevelEmailTemplate = "";
    }

    protected static void EmailTriggerUnit_Insert(string incident_Id)
    {
      string IncidentId = "";
      string FacilityId = "";
      string FacilityFacilityDisplayName = "";
      string IncidentReportNumber = "";
      string IncidentDescription = "";
      string IncidentUnitToUnit = "";
      string IncidentUnitToName = "";
      string IncidentCreatedBy = "";
      string SQLStringIncident = @"SELECT	Incident_Id , 
                                          Facility_Id , 
                                          Facility_FacilityDisplayName , 
                                          Incident_ReportNumber , 
                                          Incident_Description , 
                                          Incident_UnitTo_Unit , 
                                          Incident_UnitTo_Name , 
                                          Incident_CreatedBy 
                                  FROM		vForm_Incident 
                                  WHERE		Incident_Id = @Incident_Id";
      using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
      {
        SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", incident_Id);
        DataTable DataTable_Incident;
        using (DataTable_Incident = new DataTable())
        {
          DataTable_Incident.Locale = CultureInfo.CurrentCulture;
          DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
          if (DataTable_Incident.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
            {
              IncidentId = DataRow_Row["Incident_Id"].ToString();
              FacilityId = DataRow_Row["Facility_Id"].ToString();
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
              IncidentDescription = DataRow_Row["Incident_Description"].ToString();
              IncidentUnitToUnit = DataRow_Row["Incident_UnitTo_Unit"].ToString();
              IncidentUnitToName = DataRow_Row["Incident_UnitTo_Name"].ToString();
              IncidentCreatedBy = DataRow_Row["Incident_CreatedBy"].ToString();
            }
          }
        }
      }

      string IncidentCreatedBySurnameName = GetIncidentCreatedBySurnameName(IncidentCreatedBy);

      string TriggerUnitEmailAddress = "";
      string TriggerUnitEmailTemplate = "";
      string SQLStringTriggerUnit = @"SELECT	DISTINCT 
                                            TempTableA.ListItem_Name AS TriggerUnit_EmailAddress , 
                                            TempTableB.ListItem_Name AS TriggerUnit_EmailTemplate 
                                    FROM		vAdministration_ListItem_Active AS TempTableA 
                                            LEFT JOIN vAdministration_ListItem_Active AS TempTableB ON TempTableA.ListItem_Parent = TempTableB.ListItem_Parent 
                                    WHERE		TempTableA.ListCategory_Id = 156 
                                            AND TempTableB.ListCategory_Id = 155 
                                            AND TempTableA.ListItem_Parent IN ( 
                                              SELECT	ListItem_Parent 
                                              FROM		vAdministration_ListItem_Active 
                                              WHERE		ListCategory_Id = 157 
                                                      AND ListItem_Name = @Unit 
                                            )";
      using (SqlCommand SqlCommand_TriggerUnit = new SqlCommand(SQLStringTriggerUnit))
      {
        SqlCommand_TriggerUnit.Parameters.AddWithValue("@Unit", IncidentUnitToUnit);
        DataTable DataTable_TriggerUnit;
        using (DataTable_TriggerUnit = new DataTable())
        {
          DataTable_TriggerUnit.Locale = CultureInfo.CurrentCulture;
          DataTable_TriggerUnit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerUnit).Copy();
          if (DataTable_TriggerUnit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TriggerUnit.Rows)
            {
              TriggerUnitEmailAddress = DataRow_Row["TriggerUnit_EmailAddress"].ToString();
              TriggerUnitEmailTemplate = DataRow_Row["TriggerUnit_EmailTemplate"].ToString();

              if (!string.IsNullOrEmpty(TriggerUnitEmailAddress) && !string.IsNullOrEmpty(TriggerUnitEmailTemplate))
              {
                string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate(TriggerUnitEmailTemplate);
                string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
                string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("1");
                string BodyString = EmailTemplate;

                BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + TriggerUnitEmailAddress + "");
                BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");
                BodyString = BodyString.Replace(";replace;IncidentDescription;replace;", "" + IncidentDescription + "");
                BodyString = BodyString.Replace(";replace;IncidentUnitToName;replace;", "" + IncidentUnitToName + "");
                BodyString = BodyString.Replace(";replace;IncidentCreatedBy;replace;", "" + IncidentCreatedBySurnameName + " (" + IncidentCreatedBy + ")");
                BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
                BodyString = BodyString.Replace(";replace;FacilityId;replace;", "" + FacilityId + "");
                BodyString = BodyString.Replace(";replace;IncidentId;replace;", "" + IncidentId + "");

                string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                string EmailBody = HeaderString + BodyString + FooterString;

                string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", TriggerUnitEmailAddress, FormName, EmailBody);

                if (!string.IsNullOrEmpty(EmailSend))
                {
                  EmailSend = "";
                }

                EmailBody = "";
                EmailTemplate = "";
                URLAuthority = "";
                FormName = "";
              }
            }
          }
        }
      }

      IncidentId = "";
      FacilityId = "";
      FacilityFacilityDisplayName = "";
      IncidentReportNumber = "";
      IncidentDescription = "";
      IncidentUnitToUnit = "";
      IncidentUnitToName = "";
      IncidentCreatedBy = "";
      IncidentCreatedBySurnameName = "";

      TriggerUnitEmailAddress = "";
      TriggerUnitEmailTemplate = "";
    }

    protected void Email_Pharmacy_TriggerLevel_Insert(string incident_Id)
    {
      string TriggerValue = Pharmacy_TriggerLevel("Insert");

      if (!string.IsNullOrEmpty(TriggerValue))
      {
        string IncidentId = "";
        string FacilityId = "";
        string FacilityFacilityDisplayName = "";
        string IncidentReportNumber = "";
        string IncidentDescription = "";
        string IncidentLevel1Name = "";
        string IncidentLevel2Name = "";
        string IncidentLevel3Name = "";
        string IncidentCreatedBy = "";
        string SQLStringIncident = @"SELECT	Incident_Id , 
                                            Facility_Id , 
                                            Facility_FacilityDisplayName , 
                                            Incident_ReportNumber , 
                                            Incident_Description , 
                                            Incident_Level1_Name , 
                                            Incident_Level2_Name , 
                                            Incident_Level3_Name , 
                                            Incident_CreatedBy 
                                    FROM		vForm_Incident 
                                    WHERE		Incident_Id = @Incident_Id";
        using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
        {
          SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", incident_Id);
          DataTable DataTable_Incident;
          using (DataTable_Incident = new DataTable())
          {
            DataTable_Incident.Locale = CultureInfo.CurrentCulture;
            DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
            if (DataTable_Incident.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
              {
                IncidentId = DataRow_Row["Incident_Id"].ToString();
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
                IncidentDescription = DataRow_Row["Incident_Description"].ToString();
                IncidentLevel1Name = DataRow_Row["Incident_Level1_Name"].ToString();
                IncidentLevel2Name = DataRow_Row["Incident_Level2_Name"].ToString();
                IncidentLevel3Name = DataRow_Row["Incident_Level3_Name"].ToString();
                IncidentCreatedBy = DataRow_Row["Incident_CreatedBy"].ToString();
              }
            }
          }
        }

        string IncidentCreatedBySurnameName = GetIncidentCreatedBySurnameName(IncidentCreatedBy);

        string SecurityUserDisplayName = "";
        string SecurityUserEmail = "";
        string SQLStringEmailTo = "SELECT ISNULL(SecurityUser_DisplayName,'') AS SecurityUser_DisplayName, ISNULL(SecurityUser_Email,'') AS SecurityUser_Email FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('1') AND SecurityRole_Id IN ('189') AND Facility_Id IN (SELECT Facility_Id FROM Form_Incident WHERE Incident_Id = @Incident_Id) AND SecurityUser_Email IS NOT NULL";
        using (SqlCommand SqlCommand_EmailTo = new SqlCommand(SQLStringEmailTo))
        {
          SqlCommand_EmailTo.Parameters.AddWithValue("@Incident_Id", incident_Id);
          DataTable DataTable_EmailTo;
          using (DataTable_EmailTo = new DataTable())
          {
            DataTable_EmailTo.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailTo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailTo).Copy();
            if (DataTable_EmailTo.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_EmailTo.Rows)
              {
                SecurityUserDisplayName = DataRow_Row["SecurityUser_DisplayName"].ToString();
                SecurityUserEmail = DataRow_Row["SecurityUser_Email"].ToString();

                string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("80");
                string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
                string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("1");
                string BodyString = EmailTemplate;

                BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + SecurityUserDisplayName + "");
                BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");
                BodyString = BodyString.Replace(";replace;IncidentDescription;replace;", "" + IncidentDescription + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel1Name;replace;", "" + IncidentLevel1Name + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel2Name;replace;", "" + IncidentLevel2Name + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel3Name;replace;", "" + IncidentLevel3Name + "");
                BodyString = BodyString.Replace(";replace;IncidentCreatedBy;replace;", "" + IncidentCreatedBySurnameName + " (" + IncidentCreatedBy + ")");
                BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
                BodyString = BodyString.Replace(";replace;FacilityId;replace;", "" + FacilityId + "");
                BodyString = BodyString.Replace(";replace;IncidentId;replace;", "" + IncidentId + "");

                string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                string EmailBody = HeaderString + BodyString + FooterString;

                string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", SecurityUserEmail, FormName, EmailBody);

                if (!string.IsNullOrEmpty(EmailSend))
                {
                  EmailSend = "";
                }

                EmailBody = "";
                EmailTemplate = "";
                URLAuthority = "";
                FormName = "";
              }
            }
          }
        }

        IncidentId = "";
        FacilityId = "";
        FacilityFacilityDisplayName = "";
        IncidentReportNumber = "";
        IncidentDescription = "";
        IncidentLevel1Name = "";
        IncidentLevel2Name = "";
        IncidentLevel3Name = "";
        IncidentCreatedBy = "";
        IncidentCreatedBySurnameName = "";

        SecurityUserDisplayName = "";
        SecurityUserEmail = "";
      }
    }

    protected void Email_ReportableCEO_TriggerDoctorRelated_Insert(string incident_Id)
    {
      if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertReportableCEODoctorRelated")).SelectedValue == "Yes")
      {
        string IncidentId = "";
        string FacilityId = "";
        string FacilityFacilityDisplayName = "";
        string IncidentReportNumber = "";
        string IncidentDescription = "";
        string IncidentLevel1Name = "";
        string IncidentLevel2Name = "";
        string IncidentLevel3Name = "";
        string IncidentCreatedBy = "";
        string SQLStringIncident = @"SELECT	Incident_Id , 
                                          Facility_Id , 
                                          Facility_FacilityDisplayName , 
                                          Incident_ReportNumber , 
                                          Incident_Description , 
                                          Incident_Level1_Name , 
                                          Incident_Level2_Name , 
                                          Incident_Level3_Name , 
                                          Incident_CreatedBy 
                                  FROM		vForm_Incident 
                                  WHERE		Incident_Id = @Incident_Id";
        using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
        {
          SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", incident_Id);
          DataTable DataTable_Incident;
          using (DataTable_Incident = new DataTable())
          {
            DataTable_Incident.Locale = CultureInfo.CurrentCulture;
            DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
            if (DataTable_Incident.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
              {
                IncidentId = DataRow_Row["Incident_Id"].ToString();
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
                IncidentDescription = DataRow_Row["Incident_Description"].ToString();
                IncidentLevel1Name = DataRow_Row["Incident_Level1_Name"].ToString();
                IncidentLevel2Name = DataRow_Row["Incident_Level2_Name"].ToString();
                IncidentLevel3Name = DataRow_Row["Incident_Level3_Name"].ToString();
                IncidentCreatedBy = DataRow_Row["Incident_CreatedBy"].ToString();
              }
            }
          }
        }

        string IncidentCreatedBySurnameName = GetIncidentCreatedBySurnameName(IncidentCreatedBy);

        string EmailAddress = "";
        string SQLStringEmailAddress = @"SELECT	ListItem_Name AS EmailAddress
                                        FROM		Administration_ListItem
                                        WHERE		ListCategory_Id = 211";
        using (SqlCommand SqlCommand_EmailAddress = new SqlCommand(SQLStringEmailAddress))
        {
          DataTable DataTable_EmailAddress;
          using (DataTable_EmailAddress = new DataTable())
          {
            DataTable_EmailAddress.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailAddress = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailAddress).Copy();
            if (DataTable_EmailAddress.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_EmailAddress.Rows)
              {
                EmailAddress = DataRow_Row["EmailAddress"].ToString();

                if (!string.IsNullOrEmpty(EmailAddress))
                {
                  string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("88");
                  string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
                  string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("1");
                  string BodyString = EmailTemplate;

                  BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + EmailAddress + "");
                  BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                  BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                  BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");
                  BodyString = BodyString.Replace(";replace;IncidentDescription;replace;", "" + IncidentDescription + "");
                  BodyString = BodyString.Replace(";replace;IncidentLevel1Name;replace;", "" + IncidentLevel1Name + "");
                  BodyString = BodyString.Replace(";replace;IncidentLevel2Name;replace;", "" + IncidentLevel2Name + "");
                  BodyString = BodyString.Replace(";replace;IncidentLevel3Name;replace;", "" + IncidentLevel3Name + "");
                  BodyString = BodyString.Replace(";replace;IncidentCreatedBy;replace;", "" + IncidentCreatedBySurnameName + " (" + IncidentCreatedBy + ")");
                  BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
                  BodyString = BodyString.Replace(";replace;FacilityId;replace;", "" + FacilityId + "");
                  BodyString = BodyString.Replace(";replace;IncidentId;replace;", "" + IncidentId + "");

                  string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                  string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                  string EmailBody = HeaderString + BodyString + FooterString;

                  string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailAddress, FormName, EmailBody);

                  if (!string.IsNullOrEmpty(EmailSend))
                  {
                    EmailSend = "";
                  }

                  EmailBody = "";
                  EmailTemplate = "";
                  URLAuthority = "";
                  FormName = "";
                }
              }
            }
          }
        }

        IncidentId = "";
        FacilityId = "";
        FacilityFacilityDisplayName = "";
        IncidentReportNumber = "";
        IncidentDescription = "";
        IncidentLevel1Name = "";
        IncidentLevel2Name = "";
        IncidentLevel3Name = "";
        IncidentCreatedBy = "";
        IncidentCreatedBySurnameName = "";

        EmailAddress = "";
      }
    }

    protected void Email_ReportableCEO_Insert(string incident_Id)
    {
      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_InsertReportCEO")).Checked == true)
      {
        string IncidentId = "";
        string FacilityId = "";
        string FacilityFacilityDisplayName = "";
        string IncidentReportNumber = "";
        string IncidentDescription = "";
        string IncidentLevel1Name = "";
        string IncidentLevel2Name = "";
        string IncidentLevel3Name = "";
        string IncidentCreatedBy = "";
        string SQLStringIncident = @"SELECT	Incident_Id , 
                                          Facility_Id , 
                                          Facility_FacilityDisplayName , 
                                          Incident_ReportNumber , 
                                          Incident_Description , 
                                          Incident_Level1_Name , 
                                          Incident_Level2_Name , 
                                          Incident_Level3_Name , 
                                          Incident_CreatedBy 
                                  FROM		vForm_Incident 
                                  WHERE		Incident_Id = @Incident_Id";
        using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
        {
          SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", incident_Id);
          DataTable DataTable_Incident;
          using (DataTable_Incident = new DataTable())
          {
            DataTable_Incident.Locale = CultureInfo.CurrentCulture;
            DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
            if (DataTable_Incident.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
              {
                IncidentId = DataRow_Row["Incident_Id"].ToString();
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
                IncidentDescription = DataRow_Row["Incident_Description"].ToString();
                IncidentLevel1Name = DataRow_Row["Incident_Level1_Name"].ToString();
                IncidentLevel2Name = DataRow_Row["Incident_Level2_Name"].ToString();
                IncidentLevel3Name = DataRow_Row["Incident_Level3_Name"].ToString();
                IncidentCreatedBy = DataRow_Row["Incident_CreatedBy"].ToString();
              }
            }
          }
        }

        string IncidentCreatedBySurnameName = GetIncidentCreatedBySurnameName(IncidentCreatedBy);

        string EmailAddress = "";
        string SQLStringEmailAddress = @"SELECT	ListItem_Name AS EmailAddress
                                        FROM		Administration_ListItem
                                        WHERE		ListCategory_Id = 232";
        using (SqlCommand SqlCommand_EmailAddress = new SqlCommand(SQLStringEmailAddress))
        {
          DataTable DataTable_EmailAddress;
          using (DataTable_EmailAddress = new DataTable())
          {
            DataTable_EmailAddress.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailAddress = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailAddress).Copy();
            if (DataTable_EmailAddress.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_EmailAddress.Rows)
              {
                EmailAddress = DataRow_Row["EmailAddress"].ToString();

                if (!string.IsNullOrEmpty(EmailAddress))
                {
                  string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("92");
                  string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
                  string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("1");
                  string BodyString = EmailTemplate;

                  BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + EmailAddress + "");
                  BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                  BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                  BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");
                  BodyString = BodyString.Replace(";replace;IncidentDescription;replace;", "" + IncidentDescription + "");
                  BodyString = BodyString.Replace(";replace;IncidentLevel1Name;replace;", "" + IncidentLevel1Name + "");
                  BodyString = BodyString.Replace(";replace;IncidentLevel2Name;replace;", "" + IncidentLevel2Name + "");
                  BodyString = BodyString.Replace(";replace;IncidentLevel3Name;replace;", "" + IncidentLevel3Name + "");
                  BodyString = BodyString.Replace(";replace;IncidentCreatedBy;replace;", "" + IncidentCreatedBySurnameName + " (" + IncidentCreatedBy + ")");
                  BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
                  BodyString = BodyString.Replace(";replace;FacilityId;replace;", "" + FacilityId + "");
                  BodyString = BodyString.Replace(";replace;IncidentId;replace;", "" + IncidentId + "");

                  string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                  string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                  string EmailBody = HeaderString + BodyString + FooterString;

                  string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailAddress, FormName, EmailBody);

                  if (!string.IsNullOrEmpty(EmailSend))
                  {
                    EmailSend = "";
                  }

                  EmailBody = "";
                  EmailTemplate = "";
                  URLAuthority = "";
                  FormName = "";
                }
              }
            }
          }
        }

        IncidentId = "";
        FacilityId = "";
        FacilityFacilityDisplayName = "";
        IncidentReportNumber = "";
        IncidentDescription = "";
        IncidentLevel1Name = "";
        IncidentLevel2Name = "";
        IncidentLevel3Name = "";
        IncidentCreatedBy = "";
        IncidentCreatedBySurnameName = "";

        EmailAddress = "";
      }
    }

    protected void EmailTriggerLevel_Edit()
    {
      if (TriggerLevel_EditChanged == "Yes")
      {
        string IncidentId = "";
        string FacilityId = "";
        string FacilityFacilityDisplayName = "";
        string IncidentReportNumber = "";
        string IncidentDescription = "";
        string IncidentLevel1List = "";
        string IncidentLevel1Name = "";
        string IncidentLevel2List = "";
        string IncidentLevel2Name = "";
        string IncidentLevel3List = "";
        string IncidentLevel3Name = "";
        string IncidentCreatedBy = "";
        string SQLStringIncident = @"SELECT	Incident_Id , 
                                            Facility_Id , 
                                            Facility_FacilityDisplayName , 
                                            Incident_ReportNumber , 
                                            Incident_Description , 
                                            Incident_Level1_List , 
                                            Incident_Level1_Name , 
                                            Incident_Level2_List , 
                                            Incident_Level2_Name , 
                                            Incident_Level3_List , 
                                            Incident_Level3_Name , 
                                            Incident_CreatedBy 
                                    FROM		vForm_Incident 
                                    WHERE		Incident_Id = @Incident_Id";
        using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
        {
          SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
          DataTable DataTable_Incident;
          using (DataTable_Incident = new DataTable())
          {
            DataTable_Incident.Locale = CultureInfo.CurrentCulture;
            DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
            if (DataTable_Incident.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
              {
                IncidentId = DataRow_Row["Incident_Id"].ToString();
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
                IncidentDescription = DataRow_Row["Incident_Description"].ToString();
                IncidentLevel1List = DataRow_Row["Incident_Level1_List"].ToString();
                IncidentLevel1Name = DataRow_Row["Incident_Level1_Name"].ToString();
                IncidentLevel2List = DataRow_Row["Incident_Level2_List"].ToString();
                IncidentLevel2Name = DataRow_Row["Incident_Level2_Name"].ToString();
                IncidentLevel3List = DataRow_Row["Incident_Level3_List"].ToString();
                IncidentLevel3Name = DataRow_Row["Incident_Level3_Name"].ToString();
                IncidentCreatedBy = DataRow_Row["Incident_CreatedBy"].ToString();
              }
            }
          }
        }

        string IncidentCreatedBySurnameName = GetIncidentCreatedBySurnameName(IncidentCreatedBy);

        string TriggerLevelEmailAddress = "";
        string TriggerLevelEmailTemplate = "";
        string SQLStringTriggerLevel = @"SELECT	DISTINCT 
                                                TempTableA.ListItem_Name AS TriggerLevel_EmailAddress , 
                                                TempTableB.ListItem_Name AS TriggerLevel_EmailTemplate 
                                        FROM		vAdministration_ListItem_Active AS TempTableA 
                                                LEFT JOIN vAdministration_ListItem_Active AS TempTableB ON TempTableA.ListItem_Parent = TempTableB.ListItem_Parent 
                                        WHERE		TempTableA.ListCategory_Id = 147 
                                                AND TempTableB.ListCategory_Id = 154 
                                                AND TempTableA.ListItem_Parent IN ( 
                                                  SELECT	ListItem_Parent 
                                                  FROM		vAdministration_ListItem_Active 
                                                  WHERE		(ListCategory_Id = 148 AND ListItem_Name = @Level1) 
                                                          OR (ListCategory_Id = 149 AND ListItem_Name = @Level2) 
                                                          OR (ListCategory_Id = 150 AND ListItem_Name = @Level3) 
                                                )";
        using (SqlCommand SqlCommand_TriggerLevel = new SqlCommand(SQLStringTriggerLevel))
        {
          SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level1", IncidentLevel1List);
          SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level2", IncidentLevel2List);
          SqlCommand_TriggerLevel.Parameters.AddWithValue("@Level3", IncidentLevel3List);
          DataTable DataTable_TriggerLevel;
          using (DataTable_TriggerLevel = new DataTable())
          {
            DataTable_TriggerLevel.Locale = CultureInfo.CurrentCulture;
            DataTable_TriggerLevel = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerLevel).Copy();
            if (DataTable_TriggerLevel.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_TriggerLevel.Rows)
              {
                TriggerLevelEmailAddress = DataRow_Row["TriggerLevel_EmailAddress"].ToString();
                TriggerLevelEmailTemplate = DataRow_Row["TriggerLevel_EmailTemplate"].ToString();

                string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate(TriggerLevelEmailTemplate);
                string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
                string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("1");
                string BodyString = EmailTemplate;

                BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + TriggerLevelEmailAddress + "");
                BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");
                BodyString = BodyString.Replace(";replace;IncidentDescription;replace;", "" + IncidentDescription + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel1Name;replace;", "" + IncidentLevel1Name + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel2Name;replace;", "" + IncidentLevel2Name + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel3Name;replace;", "" + IncidentLevel3Name + "");
                BodyString = BodyString.Replace(";replace;IncidentCreatedBy;replace;", "" + IncidentCreatedBySurnameName + " (" + IncidentCreatedBy + ")");
                BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
                BodyString = BodyString.Replace(";replace;FacilityId;replace;", "" + FacilityId + "");
                BodyString = BodyString.Replace(";replace;IncidentId;replace;", "" + IncidentId + "");

                string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                string EmailBody = HeaderString + BodyString + FooterString;

                string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", TriggerLevelEmailAddress, FormName, EmailBody);

                if (!string.IsNullOrEmpty(EmailSend))
                {
                  EmailSend = "";
                }

                EmailBody = "";
                EmailTemplate = "";
                URLAuthority = "";
                FormName = "";
              }
            }
          }
        }

        IncidentId = "";
        FacilityId = "";
        FacilityFacilityDisplayName = "";
        IncidentReportNumber = "";
        IncidentDescription = "";
        IncidentLevel1List = "";
        IncidentLevel1Name = "";
        IncidentLevel2List = "";
        IncidentLevel2Name = "";
        IncidentLevel3List = "";
        IncidentLevel3Name = "";
        IncidentCreatedBy = "";
        IncidentCreatedBySurnameName = "";

        TriggerLevelEmailAddress = "";
        TriggerLevelEmailTemplate = "";
      }
    }

    protected void EmailTriggerUnit_Edit()
    {
      if (TriggerUnit_EditChanged == "Yes")
      {
        string IncidentId = "";
        string FacilityId = "";
        string FacilityFacilityDisplayName = "";
        string IncidentReportNumber = "";
        string IncidentDescription = "";
        string IncidentUnitToUnit = "";
        string IncidentUnitToName = "";
        string IncidentCreatedBy = "";
        string SQLStringIncident = @"SELECT	Incident_Id , 
                                            Facility_Id , 
                                            Facility_FacilityDisplayName , 
                                            Incident_ReportNumber , 
                                            Incident_Description , 
                                            Incident_UnitTo_Unit , 
                                            Incident_UnitTo_Name , 
                                            Incident_CreatedBy 
                                    FROM		vForm_Incident 
                                    WHERE		Incident_Id = @Incident_Id";
        using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
        {
          SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
          DataTable DataTable_Incident;
          using (DataTable_Incident = new DataTable())
          {
            DataTable_Incident.Locale = CultureInfo.CurrentCulture;
            DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
            if (DataTable_Incident.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
              {
                IncidentId = DataRow_Row["Incident_Id"].ToString();
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
                IncidentDescription = DataRow_Row["Incident_Description"].ToString();
                IncidentUnitToUnit = DataRow_Row["Incident_UnitTo_Unit"].ToString();
                IncidentUnitToName = DataRow_Row["Incident_UnitTo_Name"].ToString();
                IncidentCreatedBy = DataRow_Row["Incident_CreatedBy"].ToString();
              }
            }
          }
        }

        string IncidentCreatedBySurnameName = GetIncidentCreatedBySurnameName(IncidentCreatedBy);

        string TriggerUnitEmailAddress = "";
        string TriggerUnitEmailTemplate = "";
        string SQLStringTriggerUnit = @"SELECT	DISTINCT 
                                              TempTableA.ListItem_Name AS TriggerUnit_EmailAddress , 
                                              TempTableB.ListItem_Name AS TriggerUnit_EmailTemplate 
                                      FROM		vAdministration_ListItem_Active AS TempTableA 
                                              LEFT JOIN vAdministration_ListItem_Active AS TempTableB ON TempTableA.ListItem_Parent = TempTableB.ListItem_Parent 
                                      WHERE		TempTableA.ListCategory_Id = 156 
                                              AND TempTableB.ListCategory_Id = 155 
                                              AND TempTableA.ListItem_Parent IN ( 
                                                SELECT	ListItem_Parent 
                                                FROM		vAdministration_ListItem_Active 
                                                WHERE		ListCategory_Id = 157 
                                                        AND ListItem_Name = @Unit 
                                              )";
        using (SqlCommand SqlCommand_TriggerUnit = new SqlCommand(SQLStringTriggerUnit))
        {
          SqlCommand_TriggerUnit.Parameters.AddWithValue("@Unit", IncidentUnitToUnit);
          DataTable DataTable_TriggerUnit;
          using (DataTable_TriggerUnit = new DataTable())
          {
            DataTable_TriggerUnit.Locale = CultureInfo.CurrentCulture;
            DataTable_TriggerUnit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TriggerUnit).Copy();
            if (DataTable_TriggerUnit.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_TriggerUnit.Rows)
              {
                TriggerUnitEmailAddress = DataRow_Row["TriggerUnit_EmailAddress"].ToString();
                TriggerUnitEmailTemplate = DataRow_Row["TriggerUnit_EmailTemplate"].ToString();

                if (!string.IsNullOrEmpty(TriggerUnitEmailAddress) && !string.IsNullOrEmpty(TriggerUnitEmailTemplate))
                {
                  string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate(TriggerUnitEmailTemplate);
                  string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
                  string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("1");
                  string BodyString = EmailTemplate;

                  BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + TriggerUnitEmailAddress + "");
                  BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                  BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                  BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");
                  BodyString = BodyString.Replace(";replace;IncidentDescription;replace;", "" + IncidentDescription + "");
                  BodyString = BodyString.Replace(";replace;IncidentUnitToName;replace;", "" + IncidentUnitToName + "");
                  BodyString = BodyString.Replace(";replace;IncidentCreatedBy;replace;", "" + IncidentCreatedBySurnameName + " (" + IncidentCreatedBy + ")");
                  BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
                  BodyString = BodyString.Replace(";replace;FacilityId;replace;", "" + FacilityId + "");
                  BodyString = BodyString.Replace(";replace;IncidentId;replace;", "" + IncidentId + "");

                  string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                  string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                  string EmailBody = HeaderString + BodyString + FooterString;

                  string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", TriggerUnitEmailAddress, FormName, EmailBody);

                  if (!string.IsNullOrEmpty(EmailSend))
                  {
                    EmailSend = "";
                  }

                  EmailBody = "";
                  EmailTemplate = "";
                  URLAuthority = "";
                  FormName = "";
                }
              }
            }
          }
        }

        IncidentId = "";
        FacilityId = "";
        FacilityFacilityDisplayName = "";
        IncidentReportNumber = "";
        IncidentDescription = "";
        IncidentUnitToUnit = "";
        IncidentUnitToName = "";
        IncidentCreatedBy = "";
        IncidentCreatedBySurnameName = "";

        TriggerUnitEmailAddress = "";
        TriggerUnitEmailTemplate = "";
      }
    }

    protected void Email_ReportableCEO_TriggerDoctorRelated_Edit()
    {
      if (ReportableCEO_TriggerDoctorRelated_EditChanged == "Yes")
      {
        string IncidentId = "";
        string FacilityId = "";
        string FacilityFacilityDisplayName = "";
        string IncidentReportNumber = "";
        string IncidentDescription = "";
        string IncidentLevel1Name = "";
        string IncidentLevel2Name = "";
        string IncidentLevel3Name = "";
        string IncidentCreatedBy = "";
        string SQLStringIncident = @"SELECT	Incident_Id , 
                                            Facility_Id , 
                                            Facility_FacilityDisplayName , 
                                            Incident_ReportNumber , 
                                            Incident_Description , 
                                            Incident_Level1_Name , 
                                            Incident_Level2_Name , 
                                            Incident_Level3_Name , 
                                            Incident_CreatedBy 
                                    FROM		vForm_Incident 
                                    WHERE		Incident_Id = @Incident_Id";
        using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
        {
          SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
          DataTable DataTable_Incident;
          using (DataTable_Incident = new DataTable())
          {
            DataTable_Incident.Locale = CultureInfo.CurrentCulture;
            DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
            if (DataTable_Incident.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
              {
                IncidentId = DataRow_Row["Incident_Id"].ToString();
                FacilityId = DataRow_Row["Facility_Id"].ToString();
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
                IncidentDescription = DataRow_Row["Incident_Description"].ToString();
                IncidentLevel1Name = DataRow_Row["Incident_Level1_Name"].ToString();
                IncidentLevel2Name = DataRow_Row["Incident_Level2_Name"].ToString();
                IncidentLevel3Name = DataRow_Row["Incident_Level3_Name"].ToString();
                IncidentCreatedBy = DataRow_Row["Incident_CreatedBy"].ToString();
              }
            }
          }
        }

        string IncidentCreatedBySurnameName = GetIncidentCreatedBySurnameName(IncidentCreatedBy);

        string EmailAddress = "";
        string SQLStringEmailAddress = @"SELECT	ListItem_Name AS EmailAddress
                                        FROM		Administration_ListItem
                                        WHERE		ListCategory_Id = 211";
        using (SqlCommand SqlCommand_EmailAddress = new SqlCommand(SQLStringEmailAddress))
        {
          DataTable DataTable_EmailAddress;
          using (DataTable_EmailAddress = new DataTable())
          {
            DataTable_EmailAddress.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailAddress = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailAddress).Copy();
            if (DataTable_EmailAddress.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_EmailAddress.Rows)
              {
                EmailAddress = DataRow_Row["EmailAddress"].ToString();

                string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("88");
                string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
                string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("1");
                string BodyString = EmailTemplate;

                BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + EmailAddress + "");
                BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");
                BodyString = BodyString.Replace(";replace;IncidentDescription;replace;", "" + IncidentDescription + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel1Name;replace;", "" + IncidentLevel1Name + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel2Name;replace;", "" + IncidentLevel2Name + "");
                BodyString = BodyString.Replace(";replace;IncidentLevel3Name;replace;", "" + IncidentLevel3Name + "");
                BodyString = BodyString.Replace(";replace;IncidentCreatedBy;replace;", "" + IncidentCreatedBySurnameName + " (" + IncidentCreatedBy + ")");
                BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
                BodyString = BodyString.Replace(";replace;FacilityId;replace;", "" + FacilityId + "");
                BodyString = BodyString.Replace(";replace;IncidentId;replace;", "" + IncidentId + "");

                string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                string EmailBody = HeaderString + BodyString + FooterString;

                string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailAddress, FormName, EmailBody);

                if (!string.IsNullOrEmpty(EmailSend))
                {
                  EmailSend = "";
                }

                EmailBody = "";
                EmailTemplate = "";
                URLAuthority = "";
                FormName = "";
              }
            }
          }
        }

        IncidentId = "";
        FacilityId = "";
        FacilityFacilityDisplayName = "";
        IncidentReportNumber = "";
        IncidentDescription = "";
        IncidentLevel1Name = "";
        IncidentLevel2Name = "";
        IncidentLevel3Name = "";
        IncidentCreatedBy = "";
        IncidentCreatedBySurnameName = "";

        EmailAddress = "";
      }
    }

    protected void Email_ReportableCEO_Edit()
    {
      if (((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditReportCEO")).Checked == true)
      {
        if (ReportableCEO_EditChanged == "Yes")
        {
          string IncidentId = "";
          string FacilityId = "";
          string FacilityFacilityDisplayName = "";
          string IncidentReportNumber = "";
          string IncidentDescription = "";
          string IncidentLevel1Name = "";
          string IncidentLevel2Name = "";
          string IncidentLevel3Name = "";
          string IncidentCreatedBy = "";
          string SQLStringIncident = @"SELECT	Incident_Id , 
                                            Facility_Id , 
                                            Facility_FacilityDisplayName , 
                                            Incident_ReportNumber , 
                                            Incident_Description , 
                                            Incident_Level1_Name , 
                                            Incident_Level2_Name , 
                                            Incident_Level3_Name , 
                                            Incident_CreatedBy 
                                    FROM		vForm_Incident 
                                    WHERE		Incident_Id = @Incident_Id";
          using (SqlCommand SqlCommand_Incident = new SqlCommand(SQLStringIncident))
          {
            SqlCommand_Incident.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
            DataTable DataTable_Incident;
            using (DataTable_Incident = new DataTable())
            {
              DataTable_Incident.Locale = CultureInfo.CurrentCulture;
              DataTable_Incident = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Incident).Copy();
              if (DataTable_Incident.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Incident.Rows)
                {
                  IncidentId = DataRow_Row["Incident_Id"].ToString();
                  FacilityId = DataRow_Row["Facility_Id"].ToString();
                  FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                  IncidentReportNumber = DataRow_Row["Incident_ReportNumber"].ToString();
                  IncidentDescription = DataRow_Row["Incident_Description"].ToString();
                  IncidentLevel1Name = DataRow_Row["Incident_Level1_Name"].ToString();
                  IncidentLevel2Name = DataRow_Row["Incident_Level2_Name"].ToString();
                  IncidentLevel3Name = DataRow_Row["Incident_Level3_Name"].ToString();
                  IncidentCreatedBy = DataRow_Row["Incident_CreatedBy"].ToString();
                }
              }
            }
          }

          string IncidentCreatedBySurnameName = GetIncidentCreatedBySurnameName(IncidentCreatedBy);

          string EmailAddress = "";
          string SQLStringEmailAddress = @"SELECT	ListItem_Name AS EmailAddress
                                        FROM		Administration_ListItem
                                        WHERE		ListCategory_Id = 232";
          using (SqlCommand SqlCommand_EmailAddress = new SqlCommand(SQLStringEmailAddress))
          {
            DataTable DataTable_EmailAddress;
            using (DataTable_EmailAddress = new DataTable())
            {
              DataTable_EmailAddress.Locale = CultureInfo.CurrentCulture;
              DataTable_EmailAddress = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailAddress).Copy();
              if (DataTable_EmailAddress.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_EmailAddress.Rows)
                {
                  EmailAddress = DataRow_Row["EmailAddress"].ToString();

                  string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("92");
                  string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
                  string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("1");
                  string BodyString = EmailTemplate;

                  BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + EmailAddress + "");
                  BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                  BodyString = BodyString.Replace(";replace;FacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                  BodyString = BodyString.Replace(";replace;IncidentReportNumber;replace;", "" + IncidentReportNumber + "");
                  BodyString = BodyString.Replace(";replace;IncidentDescription;replace;", "" + IncidentDescription + "");
                  BodyString = BodyString.Replace(";replace;IncidentLevel1Name;replace;", "" + IncidentLevel1Name + "");
                  BodyString = BodyString.Replace(";replace;IncidentLevel2Name;replace;", "" + IncidentLevel2Name + "");
                  BodyString = BodyString.Replace(";replace;IncidentLevel3Name;replace;", "" + IncidentLevel3Name + "");
                  BodyString = BodyString.Replace(";replace;IncidentCreatedBy;replace;", "" + IncidentCreatedBySurnameName + " (" + IncidentCreatedBy + ")");
                  BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
                  BodyString = BodyString.Replace(";replace;FacilityId;replace;", "" + FacilityId + "");
                  BodyString = BodyString.Replace(";replace;IncidentId;replace;", "" + IncidentId + "");

                  string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                  string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                  string EmailBody = HeaderString + BodyString + FooterString;

                  string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailAddress, FormName, EmailBody);

                  if (!string.IsNullOrEmpty(EmailSend))
                  {
                    EmailSend = "";
                  }

                  EmailBody = "";
                  EmailTemplate = "";
                  URLAuthority = "";
                  FormName = "";
                }
              }
            }
          }

          IncidentId = "";
          FacilityId = "";
          FacilityFacilityDisplayName = "";
          IncidentReportNumber = "";
          IncidentDescription = "";
          IncidentLevel1Name = "";
          IncidentLevel2Name = "";
          IncidentLevel3Name = "";
          IncidentCreatedBy = "";
          IncidentCreatedBySurnameName = "";

          EmailAddress = "";
        }
      }
    }


    //--START-- --Insert Controls--//
    protected void DropDownList_InsertFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertFacility = (DropDownList)sender;

      if (string.IsNullOrEmpty(DropDownList_InsertFacility.SelectedValue))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident New Form", "Form_Incident.aspx"), false);
      }
      else
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident New Form", "Form_Incident.aspx?s_Facility_Id=" + DropDownList_InsertFacility.SelectedValue + ""), false);
      }
    }

    protected void DropDownList_InsertUnitToUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertIncidentReportable_TriggerUnit")).Value = IncidentReportable_TriggerUnit("Insert");
    }

    protected void DropDownList_InsertIncidentCategoryList_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel1List")).Items.Clear();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).Items.Clear();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Items.Clear();

      SqlDataSource_Incident_InsertLevel1List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      SqlDataSource_Incident_InsertLevel2List.SelectParameters["ListItem_Parent"].DefaultValue = "";
      SqlDataSource_Incident_InsertLevel3List.SelectParameters["ListItem_Parent"].DefaultValue = "";

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel1List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 1", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 2", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 3", CultureInfo.CurrentCulture), ""));

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel1List")).DataBind();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).DataBind();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).DataBind();

      ((Label)FormView_Incident_Form.FindControl("Label_InsertEEmployeeNameError")).Text = "";
      ((Label)FormView_Incident_Form.FindControl("Label_InsertPNameError")).Text = "";

      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Insert");

      DegreeOfHarm_InsertDropDownList();
    }

    protected void DropDownList_InsertLevel1List_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).Items.Clear();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Items.Clear();

      SqlDataSource_Incident_InsertLevel2List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      SqlDataSource_Incident_InsertLevel3List.SelectParameters["ListItem_Parent"].DefaultValue = "";

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 2", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 3", CultureInfo.CurrentCulture), ""));

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel2List")).DataBind();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).DataBind();


      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertIncidentReportable_TriggerLevel")).Value = IncidentReportable_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Insert");

      InsertReportCompulsoryReset();
    }

    protected void DropDownList_InsertLevel2List_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Items.Clear();
      SqlDataSource_Incident_InsertLevel3List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 3", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertLevel3List")).DataBind();


      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertIncidentReportable_TriggerLevel")).Value = IncidentReportable_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Insert");

      InsertReportCompulsoryReset();
    }

    protected void DropDownList_InsertLevel3List_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertIncidentReportable_TriggerLevel")).Value = IncidentReportable_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Insert");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Insert");

      InsertReportCompulsoryReset();
    }

    protected void InsertReportCompulsoryReset()
    {
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportCOIDCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportDEATCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportDepartmentOfHealthCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportDepartmentOfLabourCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportHospitalManagerCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportHPCSACompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportLegalDepartmentCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportCEOCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportPharmacyCouncilCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportQualityCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportRMCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportSANCCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportSAPSCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_InsertReportInternalAuditCompulsory")).Value = "No";
    }

    protected void Button_InsertFindEEmployeeName_OnClick(object sender, EventArgs e)
    {
      Session["DisplayName"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataEmployee;
      using (DataTable_DataEmployee = new DataTable())
      {
        DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
        DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_FindDisplayName_SearchEmployeeNumber(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEEmployeeNumber")).Text).Copy();
        if (DataTable_DataEmployee.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["DisplayName"] = "";
        }
        else if (DataTable_DataEmployee.Columns.Count != 1)
        {
          if (DataTable_DataEmployee.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Session["DisplayName"] = DataRow_Row["DisplayName"];
              Session["Error"] = "";
            }
          }
          else
          {
            Session["DisplayName"] = "";
            Session["Error"] = "Employee Name not found for specific Employee Number,<br/>Please type in the Employee Name";
          }
        }
      }

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEEmployeeName")).Text = Session["DisplayName"].ToString();
      if (!string.IsNullOrEmpty(Session["Error"].ToString()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertEEmployeeNameError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertEEmployeeNameError")).Text = "";
      }
      Session["DisplayName"] = "";
      Session["Error"] = "";
    }

    protected void TextBox_InsertEEmployeeName_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertEEmployeeName")).Text.Trim()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertEEmployeeNameError")).Text = "";
      }
    }

    protected void TextBox_InsertPVisitNumber_TextChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyDoctorName")).Items.Clear();

      DataTable DataTable_Doctor;
      using (DataTable_Doctor = new DataTable())
      {
        DataTable_Doctor.Locale = CultureInfo.CurrentCulture;
        DataTable_Doctor = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPVisitNumber")).Text);

        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyDoctorName")).DataSource = DataTable_Doctor;
        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyDoctorName")).Items.Insert(0, new ListItem(Convert.ToString("Select Doctor", CultureInfo.CurrentCulture), ""));
        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_InsertPharmacyDoctorName")).DataBind();
      }
    }

    protected void Button_InsertFindPName_OnClick(object sender, EventArgs e)
    {
      Session["NameSurname"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataPatient;
      using (DataTable_DataPatient = new DataTable())
      {
        DataTable_DataPatient.Locale = CultureInfo.CurrentCulture;
        //DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPVisitNumber")).Text).Copy();
        DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPVisitNumber")).Text).Copy();

        if (DataTable_DataPatient.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["NameSurname"] = "";
        }
        else if (DataTable_DataPatient.Columns.Count != 1)
        {
          if (DataTable_DataPatient.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
            {
              Session["NameSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];

              string NameSurnamePI = Session["NameSurname"].ToString();
              NameSurnamePI = NameSurnamePI.Replace("'", "");
              Session["NameSurname"] = NameSurnamePI;
              NameSurnamePI = "";

              Session["Error"] = "";
            }
          }
          else
          {
            Session["NameSurname"] = "";
            Session["Error"] = "Patient Name not found for specific Patient Visit Number,<br/>Please type in the Patient Name";
          }
        }
      }

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPName")).Text = Session["NameSurname"].ToString();
      if (!string.IsNullOrEmpty(Session["Error"].ToString()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertPNameError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertPNameError")).Text = "";
      }

      Session["NameSurname"] = "";
      Session["Error"] = "";
    }

    protected void TextBox_InsertPName_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPName")).Text.Trim()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertPNameError")).Text = "";
      }
    }

    protected void Button_InsertFindPatientDetailName_OnClick(object sender, EventArgs e)
    {
      Session["NameSurname"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataPatient;
      using (DataTable_DataPatient = new DataTable())
      {
        DataTable_DataPatient.Locale = CultureInfo.CurrentCulture;
        //DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailVisitNumber")).Text).Copy();
        DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailVisitNumber")).Text).Copy();

        if (DataTable_DataPatient.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["NameSurname"] = "";
        }
        else if (DataTable_DataPatient.Columns.Count != 1)
        {
          if (DataTable_DataPatient.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
            {
              Session["NameSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];

              string NameSurnamePI = Session["NameSurname"].ToString();
              NameSurnamePI = NameSurnamePI.Replace("'", "");
              Session["NameSurname"] = NameSurnamePI;
              NameSurnamePI = "";

              Session["Error"] = "";
            }
          }
          else
          {
            Session["NameSurname"] = "";
            Session["Error"] = "Patient Name not found for specific Patient Visit Number,<br/>Please type in the Patient Name";
          }
        }
      }

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailName")).Text = Session["NameSurname"].ToString();
      if (!string.IsNullOrEmpty(Session["Error"].ToString()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertPatientDetailNameError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertPatientDetailNameError")).Text = "";
      }

      Session["NameSurname"] = "";
      Session["Error"] = "";
    }

    protected void TextBox_InsertPatientDetailName_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertPatientDetailName")).Text.Trim()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertPatientDetailNameError")).Text = "";
      }
    }

    protected void Button_InsertFindReportableCEOEmployeeName_OnClick(object sender, EventArgs e)
    {
      Session["DisplayName"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataEmployee;
      using (DataTable_DataEmployee = new DataTable())
      {
        DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
        DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_FindDisplayName_SearchEmployeeNumber(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOEmployeeNumber")).Text).Copy();
        if (DataTable_DataEmployee.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["DisplayName"] = "";
        }
        else if (DataTable_DataEmployee.Columns.Count != 1)
        {
          if (DataTable_DataEmployee.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Session["DisplayName"] = DataRow_Row["DisplayName"];
              Session["Error"] = "";
            }
          }
          else
          {
            Session["DisplayName"] = "";
            Session["Error"] = "Employee Name not found for specific Employee Number,<br/>Please type in the Employee Name";
          }
        }
      }

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOEmployeeName")).Text = Session["DisplayName"].ToString();
      if (!string.IsNullOrEmpty(Session["Error"].ToString()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertReportableCEOEmployeeNameError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertReportableCEOEmployeeNameError")).Text = "";
      }
      Session["DisplayName"] = "";
      Session["Error"] = "";
    }

    protected void TextBox_InsertReportableCEOEmployeeName_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_InsertReportableCEOEmployeeName")).Text.Trim()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_InsertReportableCEOEmployeeNameError")).Text = "";
      }
    }

    protected void HiddenField_InsertDegreeOfHarmListTotal_DataBinding(object sender, EventArgs e)
    {
      ((HiddenField)sender).Value = ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_InsertDegreeOfHarmList")).Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void HiddenField_InsertDegreeOfHarmImpactImpactListTotal_DataBinding(object sender, EventArgs e)
    {
      ((HiddenField)sender).Value = ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_InsertDegreeOfHarmImpactImpactList")).Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void Button_InsertCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_InsertClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident New Form", "Form_Incident.aspx"), false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void DropDownList_EditIncidentCategoryList_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel1List")).Items.Clear();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).Items.Clear();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).Items.Clear();

      SqlDataSource_Incident_EditLevel1List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      SqlDataSource_Incident_EditLevel2List.SelectParameters["ListItem_Parent"].DefaultValue = "";
      SqlDataSource_Incident_EditLevel3List.SelectParameters["ListItem_Parent"].DefaultValue = "";

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel1List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 1", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 2", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 3", CultureInfo.CurrentCulture), ""));

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel1List")).DataBind();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).DataBind();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).DataBind();

      ((Label)FormView_Incident_Form.FindControl("Label_EditEEmployeeNameError")).Text = "";
      ((Label)FormView_Incident_Form.FindControl("Label_EditPNameError")).Text = "";

      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Edit");

      DegreeOfHarm_EditDropDownList();
    }

    protected void DropDownList_EditIncidentCategoryList_DataBound(object sender, EventArgs e)
    {
      SqlDataSource_Incident_EditLevel1List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;

      DataView DataView_IncidentLevels = (DataView)SqlDataSource_Incident_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_IncidentLevels = DataView_IncidentLevels[0];
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel1List")).SelectedValue = Convert.ToString(DataRowView_IncidentLevels["Incident_Level1_List"], CultureInfo.CurrentCulture);
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).SelectedValue = Convert.ToString(DataRowView_IncidentLevels["Incident_Level2_List"], CultureInfo.CurrentCulture);
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).SelectedValue = Convert.ToString(DataRowView_IncidentLevels["Incident_Level3_List"], CultureInfo.CurrentCulture);

      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Edit");

      DegreeOfHarm_EditDropDownList();
    }

    protected void DropDownList_EditUnitToUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditIncidentReportable_TriggerUnit")).Value = IncidentReportable_TriggerUnit("Edit");
    }

    protected void DropDownList_EditUnitToUnit_DataBound(object sender, EventArgs e)
    {
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditIncidentReportable_TriggerUnit")).Value = IncidentReportable_TriggerUnit("Edit");
    }

    protected void DropDownList_EditLevel1List_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).Items.Clear();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).Items.Clear();

      SqlDataSource_Incident_EditLevel2List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      SqlDataSource_Incident_EditLevel3List.SelectParameters["ListItem_Parent"].DefaultValue = "";

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 2", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 3", CultureInfo.CurrentCulture), ""));

      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel2List")).DataBind();
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).DataBind();


      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditIncidentReportable_TriggerLevel")).Value = IncidentReportable_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Edit");

      EditReportCompulsoryReset();
    }

    protected void DropDownList_EditLevel1List_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditLevel1List = (DropDownList)sender;
      SqlDataSource_Incident_EditLevel2List.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_EditLevel1List.SelectedValue;


      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditIncidentReportable_TriggerLevel")).Value = IncidentReportable_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Edit");
    }

    protected void DropDownList_EditLevel2List_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).Items.Clear();
      SqlDataSource_Incident_EditLevel3List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 3", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditLevel3List")).DataBind();


      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditIncidentReportable_TriggerLevel")).Value = IncidentReportable_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Edit");

      EditReportCompulsoryReset();
    }

    protected void DropDownList_EditLevel2List_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditLevel2List = (DropDownList)sender;
      SqlDataSource_Incident_EditLevel3List.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_EditLevel2List.SelectedValue;


      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditIncidentReportable_TriggerLevel")).Value = IncidentReportable_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Edit");
    }

    protected void DropDownList_EditLevel3List_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditIncidentReportable_TriggerLevel")).Value = IncidentReportable_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Edit");

      EditReportCompulsoryReset();
    }

    protected void DropDownList_EditLevel3List_DataBound(object sender, EventArgs e)
    {
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditIncidentReportable_TriggerLevel")).Value = IncidentReportable_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientFalling_TriggerLevel")).Value = PatientFalling_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPatientDetail_TriggerLevel")).Value = PatientDetail_TriggerLevel("Edit");
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditPharmacy_TriggerLevel")).Value = Pharmacy_TriggerLevel("Edit");
    }

    protected void EditReportCompulsoryReset()
    {
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportCOIDCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportDEATCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportDepartmentOfHealthCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportDepartmentOfLabourCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportHospitalManagerCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportHPCSACompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportLegalDepartmentCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportCEOCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportPharmacyCouncilCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportQualityCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportRMCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportSANCCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportSAPSCompulsory")).Value = "No";
      ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditReportInternalAuditCompulsory")).Value = "No";
    }

    protected void DropDownList_EditStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditStatus")).SelectedValue == "Pending Approval")
      {
        ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Checked = false;
        ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = false;

        ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = false;
        ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = false;
        ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = false;
        ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = false;
        ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = false;
        ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = false;

        ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = true;
        ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = true;
        ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = true;
        ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = true;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('1')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
        using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
        {
          SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
          DataTable DataTable_FormMode;
          using (DataTable_FormMode = new DataTable())
          {
            DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
            DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
            if (DataTable_FormMode.Rows.Count > 0)
            {
              DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '20'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '3'");
              DataRow[] SecurityFacilityPharmacyManager = DataTable_FormMode.Select("SecurityRole_Id = '189'");
              DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '2'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
              {
                Session["Security"] = "0";
                ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = true;

                ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = true;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = true;
                ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = true;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = true;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = true;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = true;

                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = false;
                ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = false;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = false;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = false;
              }

              if (Session["Security"].ToString() == "1" && SecurityFormAdminUpdate.Length > 0)
              {
                Session["Security"] = "0";
                ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = true;

                ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = true;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = true;
                ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = true;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = true;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = true;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = true;

                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = false;
                ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = false;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = false;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = false;
              }

              if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
              {
                Session["Security"] = "0";
                ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = true;

                ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = true;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = true;
                ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = true;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = true;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = true;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = true;

                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = false;
                ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = false;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = false;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = false;
              }

              if (Session["Security"].ToString() == "1" && SecurityFacilityPharmacyManager.Length > 0)
              {
                Session["Security"] = "0";
                ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = false;

                ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = false;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = false;
                ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = false;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = false;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = false;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = false;

                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = true;
                ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = true;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = true;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = true;
              }

              if (Session["Security"].ToString() == "1" && SecurityFacilityApprover.Length > 0)
              {
                Session["Security"] = "0";
                ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = false;

                ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = false;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = false;
                ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = false;
                ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = false;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = false;
                ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = false;

                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = true;
                ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = true;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = true;
                ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = true;
              }
              Session["Security"] = "1";
            }
            else
            {
              ((CheckBox)FormView_Incident_Form.FindControl("CheckBox_EditInvestigationCompleted")).Visible = false;

              ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmListTotal")).Visible = false;
              ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Visible = false;
              ((HiddenField)FormView_Incident_Form.FindControl("HiddenField_EditDegreeOfHarmImpactImpactListTotal")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmCost")).Visible = false;
              ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditDegreeOfHarmImplications")).Visible = false;

              ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmList")).Visible = true;
              ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = true;
              ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmCost")).Visible = true;
              ((Label)FormView_Incident_Form.FindControl("Label_EditDegreeOfHarmImplications")).Visible = true;
            }
          }
        }

        Session["Security"] = "";
      }
    }

    protected void Button_EditFindEEmployeeName_OnClick(object sender, EventArgs e)
    {
      Session["DisplayName"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataEmployee;
      using (DataTable_DataEmployee = new DataTable())
      {
        DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
        DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_FindDisplayName_SearchEmployeeNumber(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEEmployeeNumber")).Text).Copy();
        if (DataTable_DataEmployee.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["DisplayName"] = "";
        }
        else if (DataTable_DataEmployee.Columns.Count != 1)
        {
          if (DataTable_DataEmployee.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Session["DisplayName"] = DataRow_Row["DisplayName"];
              Session["Error"] = "";
            }
          }
          else
          {
            Session["DisplayName"] = "";
            Session["Error"] = "Employee Name not found for specific Employee Number,<br/>Please type in the Employee Name";
          }
        }
      }

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEEmployeeName")).Text = Session["DisplayName"].ToString();
      if (!string.IsNullOrEmpty(Session["Error"].ToString()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditEEmployeeNameError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditEEmployeeNameError")).Text = "";
      }
      Session["DisplayName"] = "";
      Session["Error"] = "";
    }

    protected void TextBox_EditEEmployeeName_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditEEmployeeName")).Text.Trim()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditEEmployeeNameError")).Text = "";
      }
    }

    protected void TextBox_EditPVisitNumber_TextChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).Items.Clear();

      DataTable DataTable_Doctor;
      using (DataTable_Doctor = new DataTable())
      {
        DataTable_Doctor.Locale = CultureInfo.CurrentCulture;
        DataTable_Doctor = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPVisitNumber")).Text);

        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).DataSource = DataTable_Doctor;
        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).Items.Insert(0, new ListItem(Convert.ToString("Select Doctor", CultureInfo.CurrentCulture), ""));
        ((DropDownList)FormView_Incident_Form.FindControl("DropDownList_EditPharmacyDoctorName")).DataBind();
      }
    }

    protected void Button_EditFindPName_OnClick(object sender, EventArgs e)
    {
      Session["NameSurname"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataPatient;
      using (DataTable_DataPatient = new DataTable())
      {
        DataTable_DataPatient.Locale = CultureInfo.CurrentCulture;
        //DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPVisitNumber")).Text).Copy();
        DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPVisitNumber")).Text).Copy();

        if (DataTable_DataPatient.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["NameSurname"] = "";
        }
        else if (DataTable_DataPatient.Columns.Count != 1)
        {
          if (DataTable_DataPatient.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
            {
              Session["NameSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];

              string NameSurnamePI = Session["NameSurname"].ToString();
              NameSurnamePI = NameSurnamePI.Replace("'", "");
              Session["NameSurname"] = NameSurnamePI;
              NameSurnamePI = "";

              Session["Error"] = "";
            }
          }
          else
          {
            Session["NameSurname"] = "";
            Session["Error"] = "Patient Name not found for specific Patient Visit Number,<br/>Please type in the Patient Name";
          }
        }
      }

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPName")).Text = Session["NameSurname"].ToString();
      if (!string.IsNullOrEmpty(Session["Error"].ToString()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditPNameError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditPNameError")).Text = "";
      }
      Session["NameSurname"] = "";
      Session["Error"] = "";
    }

    protected void TextBox_EditPName_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPName")).Text.Trim()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditPNameError")).Text = "";
      }
    }

    protected void Button_EditFindPatientDetailName_OnClick(object sender, EventArgs e)
    {
      Session["NameSurname"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataPatient;
      using (DataTable_DataPatient = new DataTable())
      {
        DataTable_DataPatient.Locale = CultureInfo.CurrentCulture;
        //DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailVisitNumber")).Text).Copy();
        DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailVisitNumber")).Text).Copy();

        if (DataTable_DataPatient.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["NameSurname"] = "";
        }
        else if (DataTable_DataPatient.Columns.Count != 1)
        {
          if (DataTable_DataPatient.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
            {
              Session["NameSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];

              string NameSurnamePI = Session["NameSurname"].ToString();
              NameSurnamePI = NameSurnamePI.Replace("'", "");
              Session["NameSurname"] = NameSurnamePI;
              NameSurnamePI = "";

              Session["Error"] = "";
            }
          }
          else
          {
            Session["NameSurname"] = "";
            Session["Error"] = "Patient Name not found for specific Patient Visit Number,<br/>Please type in the Patient Name";
          }
        }
      }

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailName")).Text = Session["NameSurname"].ToString();
      if (!string.IsNullOrEmpty(Session["Error"].ToString()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditPatientDetailNameError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditPatientDetailNameError")).Text = "";
      }
      Session["NameSurname"] = "";
      Session["Error"] = "";
    }

    protected void TextBox_EditPatientDetailName_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditPatientDetailName")).Text.Trim()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditPatientDetailNameError")).Text = "";
      }
    }

    protected void Button_EditFindReportableCEOEmployeeName_OnClick(object sender, EventArgs e)
    {
      Session["DisplayName"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataEmployee;
      using (DataTable_DataEmployee = new DataTable())
      {
        DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
        DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_FindDisplayName_SearchEmployeeNumber(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOEmployeeNumber")).Text).Copy();
        if (DataTable_DataEmployee.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["DisplayName"] = "";
        }
        else if (DataTable_DataEmployee.Columns.Count != 1)
        {
          if (DataTable_DataEmployee.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Session["DisplayName"] = DataRow_Row["DisplayName"];
              Session["Error"] = "";
            }
          }
          else
          {
            Session["DisplayName"] = "";
            Session["Error"] = "Employee Name not found for specific Employee Number,<br/>Please type in the Employee Name";
          }
        }
      }

      ((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOEmployeeName")).Text = Session["DisplayName"].ToString();
      if (!string.IsNullOrEmpty(Session["Error"].ToString()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditReportableCEOEmployeeNameError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditReportableCEOEmployeeNameError")).Text = "";
      }
      Session["DisplayName"] = "";
      Session["Error"] = "";
    }

    protected void TextBox_EditReportableCEOEmployeeName_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(((TextBox)FormView_Incident_Form.FindControl("TextBox_EditReportableCEOEmployeeName")).Text.Trim()))
      {
        ((Label)FormView_Incident_Form.FindControl("Label_EditReportableCEOEmployeeNameError")).Text = "";
      }
    }

    protected void RadioButtonList_EditDegreeOfHarmList_DataBound(object sender, EventArgs e)
    {
      RadioButtonList RadioButtonList_EditDegreeOfHarmList = (RadioButtonList)sender;

      RadioButtonList_EditDegreeOfHarmList.Items.Remove(RadioButtonList_EditDegreeOfHarmList.Items.FindByValue(""));
    }

    protected void CheckBoxList_EditDegreeOfHarmImpactImpactList_DataBound(object sender, EventArgs e)
    {
      CheckBoxList CheckBoxList_EditDegreeOfHarmImpactImpactList = (CheckBoxList)sender;

      for (int i = 0; i < CheckBoxList_EditDegreeOfHarmImpactImpactList.Items.Count; i++)
      {
        Session["IncidentDegreeOfHarmImpactImpactList"] = "";
        string SQLStringIncidentDegreeOfHarmImpactImpactList = "SELECT DISTINCT Incident_DegreeOfHarm_Impact_Impact_List FROM Form_Incident_DegreeOfHarm_Impact WHERE Incident_Id = @Incident_Id AND Incident_DegreeOfHarm_Impact_Impact_List = @Incident_DegreeOfHarm_Impact_Impact_List";
        using (SqlCommand SqlCommand_IncidentDegreeOfHarmImpactImpactList = new SqlCommand(SQLStringIncidentDegreeOfHarmImpactImpactList))
        {
          SqlCommand_IncidentDegreeOfHarmImpactImpactList.Parameters.AddWithValue("@Incident_Id", Request.QueryString["Incident_Id"]);
          SqlCommand_IncidentDegreeOfHarmImpactImpactList.Parameters.AddWithValue("@Incident_DegreeOfHarm_Impact_Impact_List", CheckBoxList_EditDegreeOfHarmImpactImpactList.Items[i].Value);
          DataTable DataTable_IncidentDegreeOfHarmImpactImpactList;
          using (DataTable_IncidentDegreeOfHarmImpactImpactList = new DataTable())
          {
            DataTable_IncidentDegreeOfHarmImpactImpactList.Locale = CultureInfo.CurrentCulture;
            DataTable_IncidentDegreeOfHarmImpactImpactList = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IncidentDegreeOfHarmImpactImpactList).Copy();
            if (DataTable_IncidentDegreeOfHarmImpactImpactList.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_IncidentDegreeOfHarmImpactImpactList.Rows)
              {
                Session["IncidentDegreeOfHarmImpactImpactList"] = DataRow_Row["Incident_DegreeOfHarm_Impact_Impact_List"];
                CheckBoxList_EditDegreeOfHarmImpactImpactList.Items[i].Selected = true;
              }
            }
          }
        }

        Session["IncidentDegreeOfHarmImpactImpactList"] = "";
      }
    }

    protected void HiddenField_EditDegreeOfHarmListTotal_DataBinding(object sender, EventArgs e)
    {
      ((HiddenField)sender).Value = ((RadioButtonList)FormView_Incident_Form.FindControl("RadioButtonList_EditDegreeOfHarmList")).Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void HiddenField_EditDegreeOfHarmImpactImpactListTotal_DataBinding(object sender, EventArgs e)
    {
      ((HiddenField)sender).Value = ((CheckBoxList)FormView_Incident_Form.FindControl("CheckBoxList_EditDegreeOfHarmImpactImpactList")).Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void SqlDataSource_Incident_EditDegreeOfHarmImpact_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;

        if (TotalRecords > 0)
        {
          ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = true;
        }
        else
        {
          ((GridView)FormView_Incident_Form.FindControl("GridView_EditDegreeOfHarmImpact")).Visible = false;
        }
      }
    }

    protected void GridView_EditDegreeOfHarmImpact_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_EditDegreeOfHarmImpact_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_EditCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident New Form", "Form_Incident.aspx"), false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }

    protected void Button_EditPrint_Click(object sender, EventArgs e)
    {
      Button_EditPrintClicked = true;
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      Button_EditEmailClicked = true;
    }
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    protected void SqlDataSource_Incident_ItemDegreeOfHarmImpact_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;

        if (TotalRecords > 0)
        {
          ((GridView)FormView_Incident_Form.FindControl("GridView_ItemDegreeOfHarmImpact")).Visible = true;
        }
        else
        {
          ((GridView)FormView_Incident_Form.FindControl("GridView_ItemDegreeOfHarmImpact")).Visible = false;
        }
      }
    }

    protected void GridView_ItemDegreeOfHarmImpact_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemDegreeOfHarmImpact_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_ItemCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident New Form", "Form_Incident.aspx"), false);
    }
    //---END--- --Item Controls--//
    //---END--- --TableForm--// 
  }
}