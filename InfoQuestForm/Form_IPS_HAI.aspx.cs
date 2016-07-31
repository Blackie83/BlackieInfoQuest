using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace InfoQuestForm
{
  public partial class Form_IPS_HAI : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected Dictionary<string, string> FileContentTypeHandler = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_IPS_HAI, this.GetType(), "UpdateProgress_Start", "Validation_HAIForm();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          PageTitle();

          if (!string.IsNullOrEmpty(Request.QueryString["IPSVisitInformationId"]) && !string.IsNullOrEmpty(Request.QueryString["IPSInfectionId"]) && !string.IsNullOrEmpty(Request.QueryString["IPSHAIId"]))
          {
            FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Request.QueryString["IPSInfectionId"]);
            string IsHAI = FromDataBase_IsHAI_Current.IsHAI;

            if (IsHAI == "True")
            {
              TableInfo.Visible = true;
              TableCurrentHAI.Visible = true;
              DivCurrentInfectionComplete.Visible = true;
              TableCurrentInfectionComplete.Visible = true;
              DivFile.Visible = true;
              TableFile.Visible = true;

              FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
              string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
              string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

              FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
              string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

              FromDataBase_HAICompleted FromDataBase_HAICompleted_Current = GetHAICompleted();
              string IPSHAICompleted = FromDataBase_HAICompleted_Current.IPSHAICompleted;

              if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True" && IPSInfectionSiteInfectionIsActive == "True")
              {
                DivFile.Visible = true;
                TableFile.Visible = true;
                TableFileList.Visible = false;
              }
              else
              {
                if (IPSInfectionIsActive == "True")
                {
                  if (IPSHAICompleted == "False")
                  {
                    DivFile.Visible = true;
                    TableFile.Visible = true;
                    TableFileList.Visible = false;
                  }
                  else
                  {
                    DivFile.Visible = true;
                    TableFile.Visible = false;
                    TableFileList.Visible = true;
                  }
                }
                else
                {
                  DivFile.Visible = true;
                  TableFile.Visible = false;
                  TableFileList.Visible = true;
                }
              }

              FileRegisterPostBackControl();

              SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
              SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
              SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection";
              SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";

              SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters["TableSELECT"].DefaultValue = "IPS_HAI_PredisposingCondition_Condition_List";
              SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_HAI_PredisposingCondition";
              SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters["TableWHERE"].DefaultValue = "IPS_HAI_Id = " + Request.QueryString["IPSHAIId"] + " ";

              SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters["TableSELECT"].DefaultValue = "IPS_HAI_Measures_Measure_List";
              SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_HAI_Measures";
              SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters["TableWHERE"].DefaultValue = "IPS_HAI_Id = " + Request.QueryString["IPSHAIId"] + " ";

              SetCurrentHAIVisibility();
            }
            else
            {
              TableInfo.Visible = false;
              TableCurrentHAI.Visible = false;
              DivCurrentInfectionComplete.Visible = false;
              TableCurrentInfectionComplete.Visible = false;
              DivFile.Visible = false;
              TableFile.Visible = false;
              TableFileList.Visible = false;
            }
          }
          else
          {
            CreateHAIForm();

            TableInfo.Visible = false;
            TableCurrentHAI.Visible = false;
            DivCurrentInfectionComplete.Visible = false;
            TableCurrentInfectionComplete.Visible = false;
            DivFile.Visible = false;
            TableFile.Visible = false;
            TableFileList.Visible = false;
          }

          if (TableInfo.Visible == true)
          {
            TableInfoVisible();
          }

          if (TableCurrentHAI.Visible == true)
          {
            TableCurrentHAIVisible();
          }

          if (TableCurrentInfectionComplete.Visible == true)
          {
            TableCurrentInfectionCompleteVisible();
          }
        }
        else
        {
          PlaceHolder PlaceHolder_EditBundleComplianceQA = (PlaceHolder)FormView_IPS_HAI_Form.FindControl("PlaceHolder_EditBundleComplianceQA");

          if (PlaceHolder_EditBundleComplianceQA != null)
          {
            BundleComplianceQA_EditControls();
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_IPS_HAI.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infection Prevention Surveillance", "5");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_IPS_HAI_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_HAI_Form.SelectCommand="SELECT * FROM Form_IPS_HAI WHERE (IPS_HAI_Id = @IPS_HAI_Id)";
      SqlDataSource_IPS_HAI_Form.UpdateCommand="UPDATE Form_IPS_HAI SET IPS_HAI_BundleCompliance_Days = @IPS_HAI_BundleCompliance_Days , IPS_HAI_BundleCompliance_TPN = @IPS_HAI_BundleCompliance_TPN , IPS_HAI_BundleCompliance_EnteralFeeding = @IPS_HAI_BundleCompliance_EnteralFeeding  , IPS_HAI_Investigation_Date = @IPS_HAI_Investigation_Date , IPS_HAI_Investigation_InvestigatorName = @IPS_HAI_Investigation_InvestigatorName , IPS_HAI_Investigation_InvestigatorDesignation = @IPS_HAI_Investigation_InvestigatorDesignation , IPS_HAI_Investigation_IPCName = @IPS_HAI_Investigation_IPCName , IPS_HAI_Investigation_TeamMembers = @IPS_HAI_Investigation_TeamMembers , IPS_HAI_Investigation_Completed = @IPS_HAI_Investigation_Completed , IPS_HAI_Investigation_CompletedDate = @IPS_HAI_Investigation_CompletedDate , IPS_HAI_ModifiedDate = @IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy = @IPS_HAI_ModifiedBy , IPS_HAI_History = @IPS_HAI_History WHERE [IPS_HAI_Id] = @IPS_HAI_Id";
      SqlDataSource_IPS_HAI_Form.SelectParameters.Clear();
      SqlDataSource_IPS_HAI_Form.SelectParameters.Add("IPS_HAI_Id", TypeCode.Int32, Request.QueryString["IPSHAIId"]);
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Clear();
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_BundleCompliance_Days", TypeCode.Int32, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_BundleCompliance_TPN", TypeCode.Boolean, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_BundleCompliance_EnteralFeeding", TypeCode.Boolean, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_Investigation_Date", TypeCode.DateTime, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_Investigation_InvestigatorName", TypeCode.String, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_Investigation_InvestigatorDesignation", TypeCode.String, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_Investigation_IPCName", TypeCode.String, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_Investigation_TeamMembers", TypeCode.String, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_Investigation_Completed", TypeCode.Boolean, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_Investigation_CompletedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_History", TypeCode.String, "");
      SqlDataSource_IPS_HAI_Form.UpdateParameters.Add("IPS_HAI_Id", TypeCode.Int32, "");

      SqlDataSource_IPS_EditInfectionUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_IPS_EditInfectionUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_HAI_EditPredisposingConditionList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters.Clear();
      SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "15");
      SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_HAI_EditPredisposingConditionList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_HAI_EditLabReportsList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_HAI_EditLabReportsList.SelectCommand = "SELECT vForm_IPS_Specimen.IPS_Specimen_Status_Name AS SpecimenStatus , CONVERT(VARCHAR(10),vForm_IPS_Specimen.IPS_Specimen_Date,111) AS SpecimenDate , vForm_IPS_Specimen.IPS_Specimen_TimeHours + ':' + vForm_IPS_Specimen.IPS_Specimen_TimeMinutes AS SpecimenTime , vForm_IPS_Specimen.IPS_Specimen_Type_Name AS SpecimenType , Form_IPS_SpecimenResult.IPS_SpecimenResult_LabNumber AS SpecimenResultLabNumber , vForm_IPS_Organism.IPS_Organism_Lookup_Description + ' (' + vForm_IPS_Organism.IPS_Organism_Lookup_Code + ')' AS Organism , CASE WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 1 THEN 'Yes' WHEN vForm_IPS_Organism.IPS_Organism_Lookup_Resistance = 0 THEN 'No' END AS OrganismResistance , vForm_IPS_Organism.IPS_Organism_Resistance_Name AS OrganismResistanceName FROM vForm_IPS_Specimen LEFT JOIN Form_IPS_SpecimenResult ON vForm_IPS_Specimen.IPS_Specimen_Id = Form_IPS_SpecimenResult.IPS_Specimen_Id LEFT JOIN vForm_IPS_Organism ON Form_IPS_SpecimenResult.IPS_SpecimenResult_Id = vForm_IPS_Organism.IPS_SpecimenResult_Id WHERE vForm_IPS_Specimen.IPS_Infection_Id = @IPS_Infection_Id AND (vForm_IPS_Specimen.IPS_Specimen_IsActive = 1 OR vForm_IPS_Specimen.IPS_Specimen_IsActive IS NULL) AND (Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive = 1 OR Form_IPS_SpecimenResult.IPS_SpecimenResult_IsActive IS NULL) AND (vForm_IPS_Organism.IPS_Organism_IsActive = 1 OR vForm_IPS_Organism.IPS_Organism_IsActive IS NULL) ORDER BY vForm_IPS_Specimen.IPS_Specimen_Date , vForm_IPS_Specimen.IPS_Specimen_TimeHours + ':' + vForm_IPS_Specimen.IPS_Specimen_TimeMinutes";
      SqlDataSource_IPS_HAI_EditLabReportsList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_HAI_EditLabReportsList.SelectParameters.Clear();
      SqlDataSource_IPS_HAI_EditLabReportsList.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSource_IPS_HAI_EditMeasureList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_HAI_EditMeasureList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_HAI_EditMeasureList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters.Clear();
      SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "35");
      SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_HAI_EditMeasureList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_HAI_ItemPredisposingConditionList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_HAI_ItemPredisposingConditionList.SelectCommand = "SELECT ListItem_Name , IPS_HAI_PredisposingCondition_Description FROM Form_IPS_HAI_PredisposingCondition LEFT JOIN Administration_ListItem ON Form_IPS_HAI_PredisposingCondition.IPS_HAI_PredisposingCondition_Condition_List = Administration_ListItem.ListItem_Id WHERE IPS_HAI_Id = @IPS_HAI_Id ORDER BY ListItem_Name";
      SqlDataSource_IPS_HAI_ItemPredisposingConditionList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_HAI_ItemPredisposingConditionList.SelectParameters.Clear();
      SqlDataSource_IPS_HAI_ItemPredisposingConditionList.SelectParameters.Add("IPS_HAI_Id", TypeCode.String, Request.QueryString["IPSHAIId"]);

      SqlDataSource_IPS_HAI_ItemLabReportsList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_HAI_ItemLabReportsList.SelectCommand = "SELECT IPS_HAI_LabReports_SpecimenStatus , IPS_HAI_LabReports_SpecimenDate , IPS_HAI_LabReports_SpecimenTime , IPS_HAI_LabReports_SpecimenType , IPS_HAI_LabReports_SpecimenResultLabNumber , IPS_HAI_LabReports_Organism , IPS_HAI_LabReports_OrganismResistance , IPS_HAI_LabReports_OrganismResistanceName FROM Form_IPS_HAI_LabReports WHERE IPS_HAI_Id = @IPS_HAI_Id ORDER BY IPS_HAI_LabReports_SpecimenDate , IPS_HAI_LabReports_SpecimenTime";
      SqlDataSource_IPS_HAI_ItemLabReportsList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_HAI_ItemLabReportsList.SelectParameters.Clear();
      SqlDataSource_IPS_HAI_ItemLabReportsList.SelectParameters.Add("IPS_HAI_Id", TypeCode.String, Request.QueryString["IPSHAIId"]);

      SqlDataSource_IPS_HAI_ItemBundleComplianceQAList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_HAI_ItemBundleComplianceQAList.SelectCommand = "SELECT CASE WHEN IPS_HAI_BundleComplianceQA_Answer = 1 THEN 'Yes' WHEN IPS_HAI_BundleComplianceQA_Answer = 0 THEN 'No' END AS Answer , ListItem_Name AS Question FROM Form_IPS_HAI_BundleComplianceQA LEFT JOIN Administration_ListItem ON Form_IPS_HAI_BundleComplianceQA.IPS_HAI_BundleComplianceQA_Question_List = Administration_ListItem.ListItem_Id WHERE IPS_HAI_Id = @IPS_HAI_Id ORDER BY ListItem_Name";
      SqlDataSource_IPS_HAI_ItemBundleComplianceQAList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_HAI_ItemBundleComplianceQAList.SelectParameters.Clear();
      SqlDataSource_IPS_HAI_ItemBundleComplianceQAList.SelectParameters.Add("IPS_HAI_Id", TypeCode.String, Request.QueryString["IPSHAIId"]);

      SqlDataSource_IPS_HAI_ItemMeasureList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_HAI_ItemMeasureList.SelectCommand = "SELECT ListItem_Name FROM Form_IPS_HAI_Measures LEFT JOIN Administration_ListItem ON Form_IPS_HAI_Measures.IPS_HAI_Measures_Measure_List = Administration_ListItem.ListItem_Id WHERE IPS_HAI_Id = @IPS_HAI_Id ORDER BY ListItem_Name";
      SqlDataSource_IPS_HAI_ItemMeasureList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_HAI_ItemMeasureList.SelectParameters.Clear();
      SqlDataSource_IPS_HAI_ItemMeasureList.SelectParameters.Add("IPS_HAI_Id", TypeCode.String, Request.QueryString["IPSHAIId"]);

      SqlDataSource_IPS_File.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_File.SelectCommand = "SELECT IPS_File_Id , IPS_File_Name , IPS_File_CreatedDate , IPS_File_CreatedBy FROM Form_IPS_File WHERE IPS_Infection_Id = @IPS_Infection_Id ORDER BY IPS_File_Name";
      SqlDataSource_IPS_File.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_File.SelectParameters.Clear();
      SqlDataSource_IPS_File.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSource_IPS_File_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_File_List.SelectCommand = "SELECT IPS_File_Id , IPS_File_Name , IPS_File_CreatedDate , IPS_File_CreatedBy FROM Form_IPS_File WHERE IPS_Infection_Id = @IPS_Infection_Id ORDER BY IPS_File_Name";
      SqlDataSource_IPS_File_List.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_File_List.SelectParameters.Clear();
      SqlDataSource_IPS_File_List.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName("37") + " : HAI Investigation", CultureInfo.CurrentCulture);
      Label_InfoHeading.Text = Convert.ToString("Visit and Infection Information", CultureInfo.CurrentCulture);
      Label_CurrentHAIHeading.Text = Convert.ToString("HAI Investigation", CultureInfo.CurrentCulture);

      if (Request.QueryString["IPSInfectionId"] == null)
      {
        Label_CurrentInfectionCompleteHeading.Text = Convert.ToString("Selected Infection Completed", CultureInfo.CurrentCulture);
      }
      else
      {
        FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Request.QueryString["IPSInfectionId"]);
        string IsHAI = FromDataBase_IsHAI_Current.IsHAI;

        if (IsHAI == "True")
        {
          Label_CurrentInfectionCompleteHeading.Text = Convert.ToString("Selected Infection Completed and HAI Investigation", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_CurrentInfectionCompleteHeading.Text = Convert.ToString("Selected Infection Completed", CultureInfo.CurrentCulture);
        }
      }

      Label_FileHeading.Text = Convert.ToString("Files", CultureInfo.CurrentCulture);
      Label_FileListHeading.Text = Convert.ToString("Files", CultureInfo.CurrentCulture);
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

    private class FromDataBase_SpecimenCompleted
    {
      public string Specimen { get; set; }
    }

    private FromDataBase_SpecimenCompleted GetSpecimenCompleted()
    {
      FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_New = new FromDataBase_SpecimenCompleted();

      string SQLStringSpecimen = "EXECUTE spForm_Get_IPS_SpecimenCompleted @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Specimen = new SqlCommand(SQLStringSpecimen))
      {
        SqlCommand_Specimen.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Specimen;
        using (DataTable_Specimen = new DataTable())
        {
          DataTable_Specimen.Locale = CultureInfo.CurrentCulture;
          DataTable_Specimen = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Specimen).Copy();
          if (DataTable_Specimen.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Specimen.Rows)
            {
              FromDataBase_SpecimenCompleted_New.Specimen = DataRow_Row["Specimen"].ToString();
            }
          }
        }
      }

      return FromDataBase_SpecimenCompleted_New;
    }

    private class FromDataBase_IsHAI
    {
      public string IsHAI { get; set; }
    }

    private static FromDataBase_IsHAI GetIsHAI(string ipsInfectionId)
    {
      FromDataBase_IsHAI FromDataBase_IsHAI_New = new FromDataBase_IsHAI();

      string SQLStringIPSInfectionHAI = "SELECT CASE WHEN IPS_Infection_Category_List IN (4799) THEN 'True' ELSE 'False' END AS IsHAI FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_IPSInfectionHAI = new SqlCommand(SQLStringIPSInfectionHAI))
      {
        SqlCommand_IPSInfectionHAI.Parameters.AddWithValue("@IPS_Infection_Id", ipsInfectionId);
        DataTable DataTable_IPSInfectionHAI;
        using (DataTable_IPSInfectionHAI = new DataTable())
        {
          DataTable_IPSInfectionHAI.Locale = CultureInfo.CurrentCulture;
          DataTable_IPSInfectionHAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IPSInfectionHAI).Copy();
          if (DataTable_IPSInfectionHAI.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_IPSInfectionHAI.Rows)
            {
              FromDataBase_IsHAI_New.IsHAI = DataRow_Row["IsHAI"].ToString();
            }
          }
        }
      }

      return FromDataBase_IsHAI_New;
    }

    private class FromDataBase_HAICompleted
    {
      public string IPSHAICompleted { get; set; }
    }

    private FromDataBase_HAICompleted GetHAICompleted()
    {
      FromDataBase_HAICompleted FromDataBase_HAICompleted_New = new FromDataBase_HAICompleted();

      string SQLStringHAI = "SELECT IPS_HAI_Investigation_Completed FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_HAI = new SqlCommand(SQLStringHAI))
      {
        SqlCommand_HAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_HAI;
        using (DataTable_HAI = new DataTable())
        {
          DataTable_HAI.Locale = CultureInfo.CurrentCulture;
          DataTable_HAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAI).Copy();
          if (DataTable_HAI.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HAI.Rows)
            {
              FromDataBase_HAICompleted_New.IPSHAICompleted = DataRow_Row["IPS_HAI_Investigation_Completed"].ToString();
            }
          }
        }
      }

      return FromDataBase_HAICompleted_New;
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


      RowInfoVisible.Visible = false;
    }

    protected void Button_InfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }

    protected void SetCurrentHAIVisibility()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["IPSHAIId"]))
      {
        SetCurrentHAIVisibility_Edit();
      }
    }

    protected void SetCurrentHAIVisibility_Edit()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      FromDataBase_HAICompleted FromDataBase_HAICompleted_Current = GetHAICompleted();
      string IPSHAICompleted = FromDataBase_HAICompleted_Current.IPSHAICompleted;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
        {
          FormView_IPS_HAI_Form.ChangeMode(FormViewMode.ReadOnly);
        }
        else
        {
          if (IPSInfectionIsActive == "True")
          {
            if (IPSHAICompleted == "False")
            {
              FormView_IPS_HAI_Form.ChangeMode(FormViewMode.Edit);
              //FormView_IPS_HAI_Form.ChangeMode(FormViewMode.ReadOnly);
            }
            else
            {
              FormView_IPS_HAI_Form.ChangeMode(FormViewMode.ReadOnly);
            }
          }
          else
          {
            FormView_IPS_HAI_Form.ChangeMode(FormViewMode.ReadOnly);
          }
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_IPS_HAI_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_IPS_HAI_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentHAIVisible()
    {
      if (FormView_IPS_HAI_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_IPS_HAI_Form.FindControl("DropDownList_EditInfectionUnitId")).Attributes.Add("OnChange", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInfectionSummary")).Attributes.Add("OnKeyUp", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInfectionSummary")).Attributes.Add("OnInput", "Validation_HAIForm();");

        GridView GridView_IPS_HAI_EditPredisposingConditionList = (GridView)FormView_IPS_HAI_Form.FindControl("GridView_IPS_HAI_EditPredisposingConditionList");
        for (int i = 0; i < GridView_IPS_HAI_EditPredisposingConditionList.Rows.Count; i++)
        {
          ((CheckBox)GridView_IPS_HAI_EditPredisposingConditionList.Rows[i].Cells[0].FindControl("CheckBox_Selected")).Attributes.Add("OnClick", "Validation_HAIForm();");
          ((TextBox)GridView_IPS_HAI_EditPredisposingConditionList.Rows[i].Cells[0].FindControl("TextBox_Description")).Attributes.Add("OnKeyUp", "Validation_HAIForm();");
          ((TextBox)GridView_IPS_HAI_EditPredisposingConditionList.Rows[i].Cells[0].FindControl("TextBox_Description")).Attributes.Add("OnInput", "Validation_HAIForm();");
        }

        ((CheckBox)FormView_IPS_HAI_Form.FindControl("CheckBox_EditInvestigationCompleted")).Attributes.Add("OnClick", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditBundleComplianceDays")).Attributes.Add("OnKeyUp", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditBundleComplianceDays")).Attributes.Add("OnInput", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationDate")).Attributes.Add("OnChange", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationDate")).Attributes.Add("OnInput", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationInvestigatorName")).Attributes.Add("OnKeyUp", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationInvestigatorName")).Attributes.Add("OnInput", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationInvestigatorDesignation")).Attributes.Add("OnKeyUp", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationInvestigatorDesignation")).Attributes.Add("OnInput", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationIPCName")).Attributes.Add("OnKeyUp", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationIPCName")).Attributes.Add("OnInput", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationTeamMembers")).Attributes.Add("OnKeyUp", "Validation_HAIForm();");
        ((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationTeamMembers")).Attributes.Add("OnInput", "Validation_HAIForm();");

        TextBox TextBox_EditBundleComplianceDays = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditBundleComplianceDays");

        HtmlTableRow BundleComplianceQA1 = (HtmlTableRow)FormView_IPS_HAI_Form.FindControl("BundleComplianceQA1");
        HtmlTableRow BundleComplianceQA2 = (HtmlTableRow)FormView_IPS_HAI_Form.FindControl("BundleComplianceQA2");
        HtmlTableRow BundleComplianceQA3 = (HtmlTableRow)FormView_IPS_HAI_Form.FindControl("BundleComplianceQA3");
        HtmlTableRow BundleComplianceQA4 = (HtmlTableRow)FormView_IPS_HAI_Form.FindControl("BundleComplianceQA4");

        string ListItemId = "";
        string SQLStringBundleComplianceQA = "SELECT TOP 1 ListItem_Id FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 143 AND ListItem_Parent IN ( SELECT ListItem_Parent FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 144 AND ListItem_Name IN ( SELECT IPS_Infection_Type_List FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id ) )";
        using (SqlCommand SqlCommand_BundleComplianceQA = new SqlCommand(SQLStringBundleComplianceQA))
        {
          SqlCommand_BundleComplianceQA.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
          DataTable DataTable_BundleComplianceQA;
          using (DataTable_BundleComplianceQA = new DataTable())
          {
            DataTable_BundleComplianceQA.Locale = CultureInfo.CurrentCulture;
            DataTable_BundleComplianceQA = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_BundleComplianceQA).Copy();
            if (DataTable_BundleComplianceQA.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_BundleComplianceQA.Rows)
              {
                ListItemId = DataRow_Row["ListItem_Id"].ToString();
              }
            }
          }
        }

        PlaceHolder PlaceHolder_EditBundleComplianceQA = (PlaceHolder)FormView_IPS_HAI_Form.FindControl("PlaceHolder_EditBundleComplianceQA");

        if (string.IsNullOrEmpty(ListItemId))
        {
          TextBox_EditBundleComplianceDays.Text = "";
          BundleComplianceQA1.Visible = false;
          BundleComplianceQA2.Visible = false;
          BundleComplianceQA3.Visible = false;
          BundleComplianceQA4.Visible = false;
          PlaceHolder_EditBundleComplianceQA.Visible = false;
        }
        else
        {
          BundleComplianceQA1.Visible = true;
          BundleComplianceQA2.Visible = true;
          BundleComplianceQA3.Visible = true;
          BundleComplianceQA4.Visible = true;
          PlaceHolder_EditBundleComplianceQA.Visible = true;

          BundleComplianceQA_EditControls();
        }
      }

      if (FormView_IPS_HAI_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        HtmlTableRow BundleComplianceQA1 = (HtmlTableRow)FormView_IPS_HAI_Form.FindControl("BundleComplianceQA1");
        HtmlTableRow BundleComplianceQA2 = (HtmlTableRow)FormView_IPS_HAI_Form.FindControl("BundleComplianceQA2");
        HtmlTableRow BundleComplianceQA3 = (HtmlTableRow)FormView_IPS_HAI_Form.FindControl("BundleComplianceQA3");
        HtmlTableRow BundleComplianceQA4 = (HtmlTableRow)FormView_IPS_HAI_Form.FindControl("BundleComplianceQA4");

        string ListItemId = "";
        string SQLStringBundleComplianceQA = "SELECT TOP 1 ListItem_Id FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 143 AND ListItem_Parent IN ( SELECT ListItem_Parent FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 144 AND ListItem_Name IN ( SELECT IPS_Infection_Type_List FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id ) )";
        using (SqlCommand SqlCommand_BundleComplianceQA = new SqlCommand(SQLStringBundleComplianceQA))
        {
          SqlCommand_BundleComplianceQA.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
          DataTable DataTable_BundleComplianceQA;
          using (DataTable_BundleComplianceQA = new DataTable())
          {
            DataTable_BundleComplianceQA.Locale = CultureInfo.CurrentCulture;
            DataTable_BundleComplianceQA = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_BundleComplianceQA).Copy();
            if (DataTable_BundleComplianceQA.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_BundleComplianceQA.Rows)
              {
                ListItemId = DataRow_Row["ListItem_Id"].ToString();
              }
            }
          }
        }

        if (string.IsNullOrEmpty(ListItemId))
        {
          BundleComplianceQA1.Visible = false;
          BundleComplianceQA2.Visible = false;
          BundleComplianceQA3.Visible = false;
          BundleComplianceQA4.Visible = false;
        }
        else
        {
          BundleComplianceQA1.Visible = true;
          BundleComplianceQA2.Visible = true;
          BundleComplianceQA3.Visible = true;
          BundleComplianceQA4.Visible = true;
        }
      }
    }

    protected void CreateHAIForm()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["IPSVisitInformationId"]) && !string.IsNullOrEmpty(Request.QueryString["IPSInfectionId"]) && string.IsNullOrEmpty(Request.QueryString["IPSHAIId"]))
      {
        FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Request.QueryString["IPSInfectionId"]);
        string IsHAI = FromDataBase_IsHAI_Current.IsHAI;

        if (IsHAI == "True")
        {
          string IPSHAIId = "";
          string SQLStringIPSInfectionHAI = "SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_IPSInfectionHAI = new SqlCommand(SQLStringIPSInfectionHAI))
          {
            SqlCommand_IPSInfectionHAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            DataTable DataTable_IPSInfectionHAI;
            using (DataTable_IPSInfectionHAI = new DataTable())
            {
              DataTable_IPSInfectionHAI.Locale = CultureInfo.CurrentCulture;
              DataTable_IPSInfectionHAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IPSInfectionHAI).Copy();
              if (DataTable_IPSInfectionHAI.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_IPSInfectionHAI.Rows)
                {
                  IPSHAIId = DataRow_Row["IPS_HAI_Id"].ToString();
                }
              }
            }
          }

          if (string.IsNullOrEmpty(IPSHAIId))
          {
            string SQLStringInsertHAI = "INSERT INTO Form_IPS_HAI ( IPS_Infection_Id , IPS_HAI_BundleCompliance_TPN , IPS_HAI_BundleCompliance_EnteralFeeding , IPS_HAI_Investigation_Completed , IPS_HAI_Investigation_CompletedDate , IPS_HAI_CreatedDate , IPS_HAI_CreatedBy , IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy , IPS_HAI_History ) VALUES ( @IPS_Infection_Id , @IPS_HAI_BundleCompliance_TPN , @IPS_HAI_BundleCompliance_EnteralFeeding , @IPS_HAI_Investigation_Completed , @IPS_HAI_Investigation_CompletedDate , @IPS_HAI_CreatedDate , @IPS_HAI_CreatedBy , @IPS_HAI_ModifiedDate , @IPS_HAI_ModifiedBy , @IPS_HAI_History ); SELECT SCOPE_IDENTITY()";
            using (SqlCommand SqlCommand_InsertHAI = new SqlCommand(SQLStringInsertHAI))
            {
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_BundleCompliance_TPN", false);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_BundleCompliance_EnteralFeeding", false);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_Completed", false);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_CompletedDate", DBNull.Value);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_CreatedDate", DateTime.Now);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_CreatedBy", Request.ServerVariables["LOGON_USER"].ToString());
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedDate", DateTime.Now);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedBy", Request.ServerVariables["LOGON_USER"].ToString());
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_History", DBNull.Value);
              IPSHAIId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertHAI);
            }

            if (!string.IsNullOrEmpty(IPSHAIId))
            {
              Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSHAIId=" + IPSHAIId + "#CurrentHAI"), false);
              Response.End();
            }
          }
        }
      }
    }


    //--START-- --CurrentHAI--//
    protected void FormView_IPS_HAI_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIPSHAIModifiedDate"] = e.OldValues["IPS_HAI_ModifiedDate"];
        object OLDIPSHAIModifiedDate = Session["OLDIPSHAIModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSHAIModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareIPSHAI = (DataView)SqlDataSource_IPS_HAI_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareIPSHAI = DataView_CompareIPSHAI[0];
        Session["DBIPSHAIModifiedDate"] = Convert.ToString(DataRowView_CompareIPSHAI["IPS_HAI_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIPSHAIModifiedBy"] = Convert.ToString(DataRowView_CompareIPSHAI["IPS_HAI_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIPSHAIModifiedDate = Session["DBIPSHAIModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIPSHAIModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentHAI);

          string Label_EditConcurrencyUpdateMessageText = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSHAIModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_IPS_HAI_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_IPS_HAI_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessageText;

          EditRegisterPostBackControl();
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessageText = EditValidation_HAI();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessageText))
          {
            e.Cancel = true;
            ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentHAI);

            ((Label)FormView_IPS_HAI_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessageText;
            ((Label)FormView_IPS_HAI_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";

            EditRegisterPostBackControl();
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessageText))
          {
            e.Cancel = false;
            e.NewValues["IPS_HAI_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["IPS_HAI_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_HAI", "IPS_HAI_Id = " + Request.QueryString["IPSHAIId"]);

            DataView DataView_IPSHAI = (DataView)SqlDataSource_IPS_HAI_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_IPSHAI = DataView_IPSHAI[0];
            Session["IPSHAIHistory"] = Convert.ToString(DataRowView_IPSHAI["IPS_HAI_History"], CultureInfo.CurrentCulture);

            Session["IPSHAIHistory"] = Session["History"].ToString() + Session["IPSHAIHistory"].ToString();
            e.NewValues["IPS_HAI_History"] = Session["IPSHAIHistory"].ToString();

            Session.Remove("IPSHAIHistory");
            Session.Remove("History");




            if (e.OldValues["IPS_HAI_Investigation_Completed"].ToString() == "False")
            {
              CheckBox CheckBox_EditInvestigationCompleted = (CheckBox)FormView_IPS_HAI_Form.FindControl("CheckBox_EditInvestigationCompleted");
              if (CheckBox_EditInvestigationCompleted.Checked == true)
              {
                e.NewValues["IPS_HAI_Investigation_CompletedDate"] = DateTime.Now.ToString();
              }
              else
              {
                e.NewValues["IPS_HAI_Investigation_CompletedDate"] = DBNull.Value.ToString();
              }
            }
            else
            {
              CheckBox CheckBox_EditInvestigationCompleted = (CheckBox)FormView_IPS_HAI_Form.FindControl("CheckBox_EditInvestigationCompleted");
              if (CheckBox_EditInvestigationCompleted.Checked == false)
              {
                e.NewValues["IPS_HAI_Investigation_CompletedDate"] = DBNull.Value.ToString();
              }
            }
          }
        }
      }
    }

    protected string EditValidation_HAI()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditInfectionUnitId = (DropDownList)FormView_IPS_HAI_Form.FindControl("DropDownList_EditInfectionUnitId");
      TextBox TextBox_EditInfectionSummary = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInfectionSummary");

      //GridView GridView_IPS_HAI_EditPredisposingConditionList = (GridView)FormView_IPS_HAI_Form.FindControl("GridView_IPS_HAI_EditPredisposingConditionList");

      CheckBox CheckBox_EditInvestigationCompleted = (CheckBox)FormView_IPS_HAI_Form.FindControl("CheckBox_EditInvestigationCompleted");

      TextBox TextBox_EditBundleComplianceDays = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditBundleComplianceDays");
      HtmlTableRow BundleComplianceQA1 = (HtmlTableRow)FormView_IPS_HAI_Form.FindControl("BundleComplianceQA1");

      TextBox TextBox_EditInvestigationDate = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationDate");
      TextBox TextBox_EditInvestigationInvestigatorName = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationInvestigatorName");
      TextBox TextBox_EditInvestigationInvestigatorDesignation = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationInvestigatorDesignation");
      TextBox TextBox_EditInvestigationIPCName = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationIPCName");
      TextBox TextBox_EditInvestigationTeamMembers = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationTeamMembers");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_EditInfectionUnitId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditInfectionSummary.Text))
        {
          InvalidForm = "Yes";
        }

        if (CheckBox_EditInvestigationCompleted.Checked == true)
        {
          //for (int i = 0; i < GridView_IPS_HAI_EditPredisposingConditionList.Rows.Count; i++)
          //{
          //  CheckBox CheckBox_Selected = (CheckBox)GridView_IPS_HAI_EditPredisposingConditionList.Rows[i].Cells[0].FindControl("CheckBox_Selected");
          //  Label Label_ConditionName = (Label)GridView_IPS_HAI_EditPredisposingConditionList.Rows[i].Cells[0].FindControl("Label_ConditionName");
          //  TextBox TextBox_Description = (TextBox)GridView_IPS_HAI_EditPredisposingConditionList.Rows[i].Cells[0].FindControl("TextBox_Description");

          //  if (CheckBox_Selected.Checked == true)
          //  {
          //    if (String.IsNullOrEmpty(TextBox_Description.Text))
          //    {
          //      InvalidForm = "Yes";
          //      InvalidFormMessage = InvalidFormMessage + Convert.ToString("Predisposing Condition: " + Label_ConditionName.Text + " is missing a description<br/>", CultureInfo.CurrentCulture);
          //    }
          //  }
          //}

          if (BundleComplianceQA1.Visible == true)
          {
            if (string.IsNullOrEmpty(TextBox_EditBundleComplianceDays.Text))
            {
              InvalidForm = "Yes";
            }
          }

          if (string.IsNullOrEmpty(TextBox_EditInvestigationDate.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(TextBox_EditInvestigationInvestigatorName.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(TextBox_EditInvestigationInvestigatorDesignation.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(TextBox_EditInvestigationIPCName.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(TextBox_EditInvestigationTeamMembers.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = InvalidFormMessage + Convert.ToString("All red fields are required<br/>", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = InvalidFormMessage + EditFieldValidation();
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation()
    {
      string InvalidFormMessage = "";

      TextBox TextBox_EditInvestigationDate = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationDate");

      if (!string.IsNullOrEmpty(TextBox_EditInvestigationDate.Text))
      {
        string DateToValidateInvestigationDate = TextBox_EditInvestigationDate.Text.ToString();
        DateTime ValidatedDateInvestigationDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateInvestigationDate);

        if (ValidatedDateInvestigationDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Investigation Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInvestigationDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_HAI_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          DropDownList DropDownList_EditInfectionUnitId = (DropDownList)FormView_IPS_HAI_Form.FindControl("DropDownList_EditInfectionUnitId");
          TextBox TextBox_EditInfectionSummary = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInfectionSummary");

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET Unit_Id = @Unit_Id , IPS_Infection_Summary = @IPS_Infection_Summary WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@Unit_Id", DropDownList_EditInfectionUnitId.SelectedValue);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Summary", TextBox_EditInfectionSummary.Text);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }


          //--START-- --Form_IPS_HAI_PredisposingCondition--//
          string SQLStringDeletePredisposingCondition = "DELETE FROM Form_IPS_HAI_PredisposingCondition WHERE IPS_HAI_Id = @IPS_HAI_Id";
          using (SqlCommand SqlCommand_DeletePredisposingCondition = new SqlCommand(SQLStringDeletePredisposingCondition))
          {
            SqlCommand_DeletePredisposingCondition.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeletePredisposingCondition);
          }

          GridView GridView_IPS_HAI_EditPredisposingConditionList = (GridView)FormView_IPS_HAI_Form.FindControl("GridView_IPS_HAI_EditPredisposingConditionList");

          for (int i = 0; i < GridView_IPS_HAI_EditPredisposingConditionList.Rows.Count; i++)
          {
            CheckBox CheckBox_Selected = (CheckBox)GridView_IPS_HAI_EditPredisposingConditionList.Rows[i].Cells[0].FindControl("CheckBox_Selected");
            HiddenField HiddenField_ConditionList = (HiddenField)GridView_IPS_HAI_EditPredisposingConditionList.Rows[i].Cells[0].FindControl("HiddenField_ConditionList");
            TextBox TextBox_Description = (TextBox)GridView_IPS_HAI_EditPredisposingConditionList.Rows[i].Cells[0].FindControl("TextBox_Description");

            if (CheckBox_Selected.Checked == true)
            {
              string SQLStringHAIPredisposingCondition = "INSERT INTO Form_IPS_HAI_PredisposingCondition ( IPS_HAI_Id ,IPS_HAI_PredisposingCondition_Condition_List ,IPS_HAI_PredisposingCondition_Description ,IPS_HAI_PredisposingCondition_CreatedDate ,IPS_HAI_PredisposingCondition_CreatedBy ) VALUES ( @IPS_HAI_Id ,@IPS_HAI_PredisposingCondition_Condition_List ,@IPS_HAI_PredisposingCondition_Description ,@IPS_HAI_PredisposingCondition_CreatedDate ,@IPS_HAI_PredisposingCondition_CreatedBy )";
              using (SqlCommand SqlCommand_HAIPredisposingCondition = new SqlCommand(SQLStringHAIPredisposingCondition))
              {
                SqlCommand_HAIPredisposingCondition.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
                SqlCommand_HAIPredisposingCondition.Parameters.AddWithValue("@IPS_HAI_PredisposingCondition_Condition_List", HiddenField_ConditionList.Value);
                SqlCommand_HAIPredisposingCondition.Parameters.AddWithValue("@IPS_HAI_PredisposingCondition_Description", TextBox_Description.Text);
                SqlCommand_HAIPredisposingCondition.Parameters.AddWithValue("@IPS_HAI_PredisposingCondition_CreatedDate", DateTime.Now);
                SqlCommand_HAIPredisposingCondition.Parameters.AddWithValue("@IPS_HAI_PredisposingCondition_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_HAIPredisposingCondition);
              }
            }
          }
          //---END--- --Form_IPS_HAI_PredisposingCondition--//


          //--START-- --Form_IPS_HAI_LabReports--//
          string SQLStringDeleteLabReports = "DELETE FROM Form_IPS_HAI_LabReports WHERE IPS_HAI_Id = @IPS_HAI_Id";
          using (SqlCommand SqlCommand_DeleteLabReports = new SqlCommand(SQLStringDeleteLabReports))
          {
            SqlCommand_DeleteLabReports.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteLabReports);
          }

          GridView GridView_IPS_HAI_EditLabReportsList = (GridView)FormView_IPS_HAI_Form.FindControl("GridView_IPS_HAI_EditLabReportsList");

          for (int i = 0; i < GridView_IPS_HAI_EditLabReportsList.Rows.Count; i++)
          {
            CheckBox CheckBox_Selected = (CheckBox)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("CheckBox_Selected");
            //HiddenField HiddenField_ConditionList = (HiddenField)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("HiddenField_ConditionList");
            Label Label_SpecimenStatus = (Label)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("Label_SpecimenStatus");
            Label Label_SpecimenDate = (Label)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("Label_SpecimenDate");
            Label Label_SpecimenTime = (Label)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("Label_SpecimenTime");
            Label Label_SpecimenType = (Label)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("Label_SpecimenType");
            Label Label_SpecimenResultLabNumber = (Label)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("Label_SpecimenResultLabNumber");
            Label Label_Organism = (Label)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("Label_Organism");
            Label Label_OrganismResistance = (Label)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("Label_OrganismResistance");
            Label Label_OrganismResistanceName = (Label)GridView_IPS_HAI_EditLabReportsList.Rows[i].Cells[0].FindControl("Label_OrganismResistanceName");

            if (CheckBox_Selected.Checked == true)
            {
              string SQLStringHAILabReports = "INSERT INTO Form_IPS_HAI_LabReports ( IPS_HAI_Id , IPS_HAI_LabReports_SpecimenStatus , IPS_HAI_LabReports_SpecimenDate , IPS_HAI_LabReports_SpecimenTime , IPS_HAI_LabReports_SpecimenType , IPS_HAI_LabReports_SpecimenResultLabNumber , IPS_HAI_LabReports_Organism , IPS_HAI_LabReports_OrganismResistance , IPS_HAI_LabReports_OrganismResistanceName , IPS_HAI_LabReports_CreatedDate , IPS_HAI_LabReports_CreatedBy ) VALUES ( @IPS_HAI_Id , @IPS_HAI_LabReports_SpecimenStatus , @IPS_HAI_LabReports_SpecimenDate , @IPS_HAI_LabReports_SpecimenTime , @IPS_HAI_LabReports_SpecimenType , @IPS_HAI_LabReports_SpecimenResultLabNumber , @IPS_HAI_LabReports_Organism , @IPS_HAI_LabReports_OrganismResistance , @IPS_HAI_LabReports_OrganismResistanceName , @IPS_HAI_LabReports_CreatedDate , @IPS_HAI_LabReports_CreatedBy )";
              using (SqlCommand SqlCommand_HAILabReports = new SqlCommand(SQLStringHAILabReports))
              {
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenStatus", Label_SpecimenStatus.Text);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenDate", Label_SpecimenDate.Text);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenTime", Label_SpecimenTime.Text);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenType", Label_SpecimenType.Text);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenResultLabNumber", Label_SpecimenResultLabNumber.Text);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_Organism", Label_Organism.Text);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_OrganismResistance", Label_OrganismResistance.Text);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_OrganismResistanceName", Label_OrganismResistanceName.Text);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_CreatedDate", DateTime.Now);
                SqlCommand_HAILabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_HAILabReports);
              }
            }
          }
          //---END--- --Form_IPS_HAI_LabReports--//


          //--START-- --Form_IPS_HAI_BundleComplianceQA--//
          string SQLStringDeleteBundleComplianceQA = "DELETE FROM Form_IPS_HAI_BundleComplianceQA WHERE IPS_HAI_Id = @IPS_HAI_Id";
          using (SqlCommand SqlCommand_DeleteBundleComplianceQA = new SqlCommand(SQLStringDeleteBundleComplianceQA))
          {
            SqlCommand_DeleteBundleComplianceQA.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteBundleComplianceQA);
          }

          PlaceHolder PlaceHolder_EditBundleComplianceQA = (PlaceHolder)FormView_IPS_HAI_Form.FindControl("PlaceHolder_EditBundleComplianceQA");
          Int32 TotalQuestions = Convert.ToInt32(((HiddenField)(PlaceHolder_EditBundleComplianceQA.FindControl("HiddenField_EditBundleComplianceQA_TotalQuestions"))).Value, CultureInfo.CurrentCulture);

          for (int a = 1; a <= TotalQuestions; a++)
          {
            CheckBox CheckBox_EditBundleComplianceQA = (CheckBox)PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_" + a);

            string QuestionId = CheckBox_EditBundleComplianceQA.Attributes["Question"].ToString();

            string SQLStringHAIBundleComplianceQA = "INSERT INTO Form_IPS_HAI_BundleComplianceQA ( IPS_HAI_Id , IPS_HAI_BundleComplianceQA_Question_List , IPS_HAI_BundleComplianceQA_Answer , IPS_HAI_BundleComplianceQA_CreatedDate , IPS_HAI_BundleComplianceQA_CreatedBy ) VALUES ( @IPS_HAI_Id , @IPS_HAI_BundleComplianceQA_Question_List , @IPS_HAI_BundleComplianceQA_Answer , @IPS_HAI_BundleComplianceQA_CreatedDate , @IPS_HAI_BundleComplianceQA_CreatedBy )";
            using (SqlCommand SqlCommand_HAIBundleComplianceQA = new SqlCommand(SQLStringHAIBundleComplianceQA))
            {
              SqlCommand_HAIBundleComplianceQA.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
              SqlCommand_HAIBundleComplianceQA.Parameters.AddWithValue("@IPS_HAI_BundleComplianceQA_Question_List", QuestionId);
              SqlCommand_HAIBundleComplianceQA.Parameters.AddWithValue("@IPS_HAI_BundleComplianceQA_Answer", CheckBox_EditBundleComplianceQA.Checked);
              SqlCommand_HAIBundleComplianceQA.Parameters.AddWithValue("@IPS_HAI_BundleComplianceQA_CreatedDate", DateTime.Now);
              SqlCommand_HAIBundleComplianceQA.Parameters.AddWithValue("@IPS_HAI_BundleComplianceQA_CreatedBy", Request.ServerVariables["LOGON_USER"]);

              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_HAIBundleComplianceQA);
            }
          }
          //---END--- --Form_IPS_HAI_BundleComplianceQA--//


          //--START-- --Form_IPS_HAI_Measures--//
          string SQLStringDeleteMeasures = "DELETE FROM Form_IPS_HAI_Measures WHERE IPS_HAI_Id = @IPS_HAI_Id";
          using (SqlCommand SqlCommand_DeleteMeasures = new SqlCommand(SQLStringDeleteMeasures))
          {
            SqlCommand_DeleteMeasures.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteMeasures);
          }

          CheckBoxList CheckBoxList_EditMeasureList = (CheckBoxList)FormView_IPS_HAI_Form.FindControl("CheckBoxList_EditMeasureList");

          foreach (System.Web.UI.WebControls.ListItem ListItem_MeasureList in CheckBoxList_EditMeasureList.Items)
          {
            if (ListItem_MeasureList.Selected)
            {
              string SQLStringInsertMeasure = "INSERT INTO Form_IPS_HAI_Measures ( IPS_HAI_Id , IPS_HAI_Measures_Measure_List , IPS_HAI_Measures_CreatedDate , IPS_HAI_Measures_CreatedBy ) VALUES ( @IPS_HAI_Id , @IPS_HAI_Measures_Measure_List , @IPS_HAI_Measures_CreatedDate , @IPS_HAI_Measures_CreatedBy )";
              using (SqlCommand SqlCommand_InsertMeasure = new SqlCommand(SQLStringInsertMeasure))
              {
                SqlCommand_InsertMeasure.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
                SqlCommand_InsertMeasure.Parameters.AddWithValue("@IPS_HAI_Measures_Measure_List", ListItem_MeasureList.Value.ToString());
                SqlCommand_InsertMeasure.Parameters.AddWithValue("@IPS_HAI_Measures_CreatedDate", DateTime.Now);
                SqlCommand_InsertMeasure.Parameters.AddWithValue("@IPS_HAI_Measures_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertMeasure);
              }
            }
          }
          //---END--- --Form_IPS_HAI_Measures--//


          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
        }
      }
    }


    protected void FormView_IPS_HAI_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_IPS_HAI_Form.CurrentMode == FormViewMode.Edit)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["IPSHAIId"]))
        {
          EditDataBound_CurrentHAI();
        }
      }

      if (FormView_IPS_HAI_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["IPSHAIId"]))
        {
          ReadOnlyDataBound_CurrentHAI();
        }
      }
    }

    protected void EditDataBound_CurrentHAI()
    {
      string IPSInfectionTypeName = "";
      string UnitId = "";
      string IPSInfectionSummary = "";
      string SQLStringInfection = "SELECT IPS_Infection_Type_Name , Unit_Id , IPS_Infection_Summary FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
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
              IPSInfectionTypeName = DataRow_Row["IPS_Infection_Type_Name"].ToString();
              UnitId = DataRow_Row["Unit_Id"].ToString();
              IPSInfectionSummary = DataRow_Row["IPS_Infection_Summary"].ToString();
            }
          }
        }
      }

      Label Label_EditInfectionTypeName = (Label)FormView_IPS_HAI_Form.FindControl("Label_EditInfectionTypeName");
      Label_EditInfectionTypeName.Text = IPSInfectionTypeName;

      TextBox TextBox_EditInfectionSummary = (TextBox)FormView_IPS_HAI_Form.FindControl("TextBox_EditInfectionSummary");
      TextBox_EditInfectionSummary.Text = IPSInfectionSummary;


      string FacilityId = "";
      string SQLStringFacility = "SELECT Facility_Id FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);
        DataTable DataTable_Facility;
        using (DataTable_Facility = new DataTable())
        {
          DataTable_Facility.Locale = CultureInfo.CurrentCulture;
          DataTable_Facility = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
          if (DataTable_Facility.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Facility.Rows)
            {
              FacilityId = DataRow_Row["Facility_Id"].ToString();
            }
          }
        }
      }

      DropDownList DropDownList_EditInfectionUnitId = (DropDownList)FormView_IPS_HAI_Form.FindControl("DropDownList_EditInfectionUnitId");
      DropDownList_EditInfectionUnitId.SelectedValue = UnitId;
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["Facility_Id"].DefaultValue = FacilityId;
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection LEFT JOIN Form_IPS_VisitInformation ON Form_IPS_Infection.IPS_VisitInformation_Id = Form_IPS_VisitInformation.IPS_VisitInformation_Id";
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";
    }

    protected void ReadOnlyDataBound_CurrentHAI()
    {
      string IPSInfectionTypeName = "";
      string UnitName = "";
      string IPSInfectionSummary = "";
      string SQLStringInfection = "SELECT IPS_Infection_Type_Name , Unit_Name , IPS_Infection_Summary FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
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
              IPSInfectionTypeName = DataRow_Row["IPS_Infection_Type_Name"].ToString();
              UnitName = DataRow_Row["Unit_Name"].ToString();
              IPSInfectionSummary = DataRow_Row["IPS_Infection_Summary"].ToString();
            }
          }
        }
      }

      Label Label_ItemInfectionTypeName = (Label)FormView_IPS_HAI_Form.FindControl("Label_ItemInfectionTypeName");
      Label_ItemInfectionTypeName.Text = IPSInfectionTypeName;

      Label Label_ItemInfectionUnit = (Label)FormView_IPS_HAI_Form.FindControl("Label_ItemInfectionUnit");
      Label_ItemInfectionUnit.Text = UnitName;

      Label Label_ItemInfectionSummary = (Label)FormView_IPS_HAI_Form.FindControl("Label_ItemInfectionSummary");
      Label_ItemInfectionSummary.Text = IPSInfectionSummary;
    }


    protected void EditRegisterPostBackControl()
    {
      FileRegisterPostBackControl();
    }

    protected void SqlDataSource_IPS_HAI_EditPredisposingConditionList_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        HiddenField HiddenField_EditPredisposingConditionTotalRecords = (HiddenField)FormView_IPS_HAI_Form.FindControl("HiddenField_EditPredisposingConditionTotalRecords");
        HiddenField_EditPredisposingConditionTotalRecords.Value = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_HAI_EditPredisposingConditionList_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_HAI_EditPredisposingConditionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          HiddenField HiddenField_ConditionList = (HiddenField)e.Row.FindControl("HiddenField_ConditionList");

          Session["IPSHAIPredisposingConditionId"] = "";
          Session["IPSHAIPredisposingConditionDescription"] = "";
          string SQLStringPredisposingCondition = "SELECT IPS_HAI_PredisposingCondition_Id , IPS_HAI_PredisposingCondition_Description FROM Form_IPS_HAI_PredisposingCondition WHERE IPS_HAI_Id = @IPS_HAI_Id AND IPS_HAI_PredisposingCondition_Condition_List = @IPS_HAI_PredisposingCondition_Condition_List";
          using (SqlCommand SqlCommand_PredisposingCondition = new SqlCommand(SQLStringPredisposingCondition))
          {
            SqlCommand_PredisposingCondition.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
            SqlCommand_PredisposingCondition.Parameters.AddWithValue("@IPS_HAI_PredisposingCondition_Condition_List", HiddenField_ConditionList.Value);

            DataTable DataTable_PredisposingCondition;
            using (DataTable_PredisposingCondition = new DataTable())
            {
              DataTable_PredisposingCondition.Locale = CultureInfo.CurrentCulture;
              DataTable_PredisposingCondition = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PredisposingCondition).Copy();
              if (DataTable_PredisposingCondition.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_PredisposingCondition.Rows)
                {
                  Session["IPSHAIPredisposingConditionId"] = DataRow_Row["IPS_HAI_PredisposingCondition_Id"];
                  Session["IPSHAIPredisposingConditionDescription"] = DataRow_Row["IPS_HAI_PredisposingCondition_Description"];
                }
              }
            }
          }

          CheckBox CheckBox_Selected = (CheckBox)e.Row.FindControl("CheckBox_Selected");
          TextBox TextBox_Description = (TextBox)e.Row.FindControl("TextBox_Description");

          if (!string.IsNullOrEmpty(Session["IPSHAIPredisposingConditionId"].ToString()))
          {
            CheckBox_Selected.Checked = true;
            TextBox_Description.Text = Session["IPSHAIPredisposingConditionDescription"].ToString();
          }
          else
          {
            CheckBox_Selected.Checked = false;
            TextBox_Description.Text = "";
          }

          Session.Remove("IPSHAIPredisposingConditionId");
          Session.Remove("IPSHAIPredisposingConditionDescription");
        }
      }
    }

    protected void SqlDataSource_IPS_HAI_EditLabReportsList_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        HiddenField HiddenField_EditLabReportsTotalRecords = (HiddenField)FormView_IPS_HAI_Form.FindControl("HiddenField_EditLabReportsTotalRecords");
        HiddenField_EditLabReportsTotalRecords.Value = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_HAI_EditLabReportsList_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_HAI_EditLabReportsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          Label Label_SpecimenStatus = (Label)e.Row.FindControl("Label_SpecimenStatus");
          Label Label_SpecimenDate = (Label)e.Row.FindControl("Label_SpecimenDate");
          Label Label_SpecimenTime = (Label)e.Row.FindControl("Label_SpecimenTime");
          Label Label_SpecimenType = (Label)e.Row.FindControl("Label_SpecimenType");
          Label Label_SpecimenResultLabNumber = (Label)e.Row.FindControl("Label_SpecimenResultLabNumber");
          Label Label_Organism = (Label)e.Row.FindControl("Label_Organism");
          Label Label_OrganismResistance = (Label)e.Row.FindControl("Label_OrganismResistance");
          Label Label_OrganismResistanceName = (Label)e.Row.FindControl("Label_OrganismResistanceName");

          string IPSHAILabReportsId = "";
          string SQLStringLabReports = "SELECT IPS_HAI_LabReports_Id FROM Form_IPS_HAI_LabReports WHERE IPS_HAI_Id = @IPS_HAI_Id AND IPS_HAI_LabReports_SpecimenStatus = @IPS_HAI_LabReports_SpecimenStatus AND IPS_HAI_LabReports_SpecimenDate = @IPS_HAI_LabReports_SpecimenDate AND IPS_HAI_LabReports_SpecimenTime = @IPS_HAI_LabReports_SpecimenTime AND IPS_HAI_LabReports_SpecimenType = @IPS_HAI_LabReports_SpecimenType AND IPS_HAI_LabReports_SpecimenResultLabNumber = @IPS_HAI_LabReports_SpecimenResultLabNumber AND IPS_HAI_LabReports_Organism = @IPS_HAI_LabReports_Organism AND IPS_HAI_LabReports_OrganismResistance = @IPS_HAI_LabReports_OrganismResistance AND IPS_HAI_LabReports_OrganismResistanceName = @IPS_HAI_LabReports_OrganismResistanceName";
          using (SqlCommand SqlCommand_LabReports = new SqlCommand(SQLStringLabReports))
          {
            SqlCommand_LabReports.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
            SqlCommand_LabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenStatus", Label_SpecimenStatus.Text);
            SqlCommand_LabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenDate", Label_SpecimenDate.Text);
            SqlCommand_LabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenTime", Label_SpecimenTime.Text);
            SqlCommand_LabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenType", Label_SpecimenType.Text);
            SqlCommand_LabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_SpecimenResultLabNumber", Label_SpecimenResultLabNumber.Text);
            SqlCommand_LabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_Organism", Label_Organism.Text);
            SqlCommand_LabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_OrganismResistance", Label_OrganismResistance.Text);
            SqlCommand_LabReports.Parameters.AddWithValue("@IPS_HAI_LabReports_OrganismResistanceName", Label_OrganismResistanceName.Text);

            DataTable DataTable_LabReports;
            using (DataTable_LabReports = new DataTable())
            {
              DataTable_LabReports.Locale = CultureInfo.CurrentCulture;
              DataTable_LabReports = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LabReports).Copy();
              if (DataTable_LabReports.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_LabReports.Rows)
                {
                  IPSHAILabReportsId = DataRow_Row["IPS_HAI_LabReports_Id"].ToString();
                }
              }
            }
          }

          CheckBox CheckBox_Selected = (CheckBox)e.Row.FindControl("CheckBox_Selected");

          if (!string.IsNullOrEmpty(IPSHAILabReportsId))
          {
            CheckBox_Selected.Checked = true;
          }
          else
          {
            CheckBox_Selected.Checked = false;
          }

          IPSHAILabReportsId = "";
        }
      }
    }

    protected void CheckBoxList_EditMeasureList_DataBound(object sender, EventArgs e)
    {
      CheckBoxList CheckBoxList_EditMeasureList = (CheckBoxList)sender;

      for (int i = 0; i < CheckBoxList_EditMeasureList.Items.Count; i++)
      {
        Session["IPSHAIMeasuresMeasureList"] = "";
        string SQLStringHAIMeasuresMeasureList = "SELECT DISTINCT IPS_HAI_Measures_Measure_List FROM Form_IPS_HAI_Measures WHERE IPS_HAI_Id = @IPS_HAI_Id AND IPS_HAI_Measures_Measure_List = @IPS_HAI_Measures_Measure_List";
        using (SqlCommand SqlCommand_HAIMeasuresMeasureList = new SqlCommand(SQLStringHAIMeasuresMeasureList))
        {
          SqlCommand_HAIMeasuresMeasureList.Parameters.AddWithValue("@IPS_HAI_Id", Request.QueryString["IPSHAIId"]);
          SqlCommand_HAIMeasuresMeasureList.Parameters.AddWithValue("@IPS_HAI_Measures_Measure_List", CheckBoxList_EditMeasureList.Items[i].Value);
          DataTable DataTable_HAIMeasuresMeasureList;
          using (DataTable_HAIMeasuresMeasureList = new DataTable())
          {
            DataTable_HAIMeasuresMeasureList.Locale = CultureInfo.CurrentCulture;
            DataTable_HAIMeasuresMeasureList = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAIMeasuresMeasureList).Copy();
            if (DataTable_HAIMeasuresMeasureList.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_HAIMeasuresMeasureList.Rows)
              {
                Session["IPSHAIMeasuresMeasureList"] = DataRow_Row["IPS_HAI_Measures_Measure_List"].ToString();
                CheckBoxList_EditMeasureList.Items[i].Selected = true;
              }
            }
          }
        }

        Session.Remove("IPSHAIMeasuresMeasureList");
      }
    }

    protected void Button_EditHAIInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void BundleComplianceQA_EditControls()
    {
      HtmlTable HtmlTable_EditBundleComplianceQA_Questions = BundleComplianceQA_EditControls_Questions();
      Int32 TotalQuestions = HtmlTable_EditBundleComplianceQA_Questions.Rows.Count;

      HtmlTable HtmlTable_EditBundleComplianceQA_SelectAll = BundleComplianceQA_EditControls_SelectAll();

      HiddenField HiddenField_EditBundleComplianceQA_TotalQuestions = BundleComplianceQA_EditControls_TotalQuestions(TotalQuestions);

      PlaceHolder PlaceHolder_EditBundleComplianceQA = (PlaceHolder)FormView_IPS_HAI_Form.FindControl("PlaceHolder_EditBundleComplianceQA");
      PlaceHolder_EditBundleComplianceQA.Controls.Add(HtmlTable_EditBundleComplianceQA_SelectAll);
      PlaceHolder_EditBundleComplianceQA.Controls.Add(HtmlTable_EditBundleComplianceQA_Questions);
      PlaceHolder_EditBundleComplianceQA.Controls.Add(HiddenField_EditBundleComplianceQA_TotalQuestions);


      string QuestionAllSelected = "Yes";
      for (int a = 1; a <= TotalQuestions; a++)
      {
        if (((CheckBox)(PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_" + a))).Checked == false)
        {
          QuestionAllSelected = "No";
          ((CheckBox)(PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_SelectAll"))).Checked = false;
        }
      }

      if (QuestionAllSelected == "Yes")
      {
        ((CheckBox)(PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_SelectAll"))).Checked = true;
      }
    }

    protected HtmlTable BundleComplianceQA_EditControls_Questions()
    {
      HtmlTable HtmlTable_EditBundleComplianceQA_Questions;
      using (HtmlTable_EditBundleComplianceQA_Questions = new HtmlTable())
      {
        HtmlTable_EditBundleComplianceQA_Questions.Width = "100%";
        HtmlTable_EditBundleComplianceQA_Questions.Border = 0;
        HtmlTable_EditBundleComplianceQA_Questions.Attributes.Add("Class", "Table_Body");

        Int32 TotalQuestions = 1;

        string QuestionId = "";
        string Question = "";
        Boolean Answer = false;
        string SQLStringQuestions = "SELECT * FROM ( " +
	                                  "  SELECT * , ROW_NUMBER() OVER( PARTITION BY QuestionId ORDER BY Answer DESC ) AS RowNumber FROM ( " +
		                                "    SELECT	DISTINCT " +
						                        "            vAdministration_ListItem_Active.ListItem_Id AS QuestionId , " +
						                        "            vAdministration_ListItem_Active.ListItem_Name AS Question , " +
						                        "            CASE " +
							                      "              WHEN Form_IPS_HAI.IPS_Infection_Id = @IPS_Infection_Id THEN Form_IPS_HAI_BundleComplianceQA.IPS_HAI_BundleComplianceQA_Answer " +
							                      "              ELSE 0 " +
						                        "            END Answer " +
		                                "    FROM		vAdministration_ListItem_Active " +
						                        "            LEFT JOIN Form_IPS_HAI_BundleComplianceQA ON vAdministration_ListItem_Active.ListItem_Id = Form_IPS_HAI_BundleComplianceQA.IPS_HAI_BundleComplianceQA_Question_List " +
						                        "            LEFT JOIN Form_IPS_HAI ON Form_IPS_HAI.IPS_HAI_Id = Form_IPS_HAI_BundleComplianceQA.IPS_HAI_Id " +
		                                "    WHERE		ListCategory_Id = 143 " +
						                        "            AND ListItem_Parent IN ( " +
							                      "              SELECT	ListItem_Parent " +
							                      "              FROM		vAdministration_ListItem_Active " +
							                      "              WHERE		ListCategory_Id = 144 " +
											              "                      AND ListItem_Name IN ( " +
												            "                        SELECT	IPS_Infection_Type_List " +
												            "                        FROM		Form_IPS_Infection " +
												            "                        WHERE		IPS_Infection_Id = @IPS_Infection_Id " +
											              "                      ) " +
						                        "            ) " +
	                                  "  ) AS TempTableA " +
                                    ") AS TempTableB " +
                                    "WHERE TempTableB.RowNumber = 1 " +
                                    "ORDER BY TempTableB.Question";
        using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
        {
          SqlCommand_Questions.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
          DataTable DataTable_Questions;
          using (DataTable_Questions = new DataTable())
          {
            DataTable_Questions.Locale = CultureInfo.CurrentCulture;
            DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
            if (DataTable_Questions.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Questions.Rows)
              {
                QuestionId = DataRow_Row["QuestionId"].ToString();
                Question = DataRow_Row["Question"].ToString();
                Answer = Convert.ToBoolean(DataRow_Row["Answer"], CultureInfo.CurrentCulture);

                using (HtmlTableRow HtmlTableRow_EditBundleComplianceQA_Questions = new HtmlTableRow())
                {
                  for (int a = 1; a <= 2; a++)
                  {
                    using (HtmlTableCell HtmlTableCell_EditBundleComplianceQA_Questions = new HtmlTableCell())
                    {
                      HtmlTableCell_EditBundleComplianceQA_Questions.Align = HorizontalAlign.Left.ToString();
                      HtmlTableCell_EditBundleComplianceQA_Questions.VAlign = VerticalAlign.Top.ToString();

                      if (a == 1)
                      {
                        CheckBox CheckBox_EditBundleComplianceQA;
                        using (CheckBox_EditBundleComplianceQA = new CheckBox())
                        {
                          CheckBox_EditBundleComplianceQA.ID = "CheckBox_EditBundleComplianceQA_" + TotalQuestions;
                          CheckBox_EditBundleComplianceQA.Text = "";
                          CheckBox_EditBundleComplianceQA.Checked = Answer;
                          CheckBox_EditBundleComplianceQA.Attributes.Add("Question", QuestionId);
                          CheckBox_EditBundleComplianceQA.AutoPostBack = true;
                        }

                        CheckBox_EditBundleComplianceQA.CheckedChanged += new EventHandler(CheckBox_EditBundleComplianceQA_CheckedChanged);

                        HtmlTableCell_EditBundleComplianceQA_Questions.Style.Add("padding", "0px");
                        HtmlTableCell_EditBundleComplianceQA_Questions.Style.Add("width", "10px");
                        HtmlTableCell_EditBundleComplianceQA_Questions.Controls.Add(CheckBox_EditBundleComplianceQA);
                      }

                      if (a == 2)
                      {
                        using (Label Label_EditBundleComplianceQA = new Label())
                        {
                          Label_EditBundleComplianceQA.ID = "Label_EditBundleComplianceQA_" + TotalQuestions;
                          Label_EditBundleComplianceQA.Text = Question;

                          HtmlTableCell_EditBundleComplianceQA_Questions.Style.Add("padding", "3px");
                          HtmlTableCell_EditBundleComplianceQA_Questions.Controls.Add(Label_EditBundleComplianceQA);
                        }
                      }

                      HtmlTableRow_EditBundleComplianceQA_Questions.Cells.Add(HtmlTableCell_EditBundleComplianceQA_Questions);
                    }
                  }

                  HtmlTable_EditBundleComplianceQA_Questions.Rows.Add(HtmlTableRow_EditBundleComplianceQA_Questions);
                }

                TotalQuestions = TotalQuestions + 1;
              }
            }
          }
        }
      }

      return HtmlTable_EditBundleComplianceQA_Questions;
    }

    protected HtmlTable BundleComplianceQA_EditControls_SelectAll()
    {
      HtmlTable HtmlTable_EditBundleComplianceQA_SelectAll;
      using (HtmlTable_EditBundleComplianceQA_SelectAll = new HtmlTable())
      {
        HtmlTable_EditBundleComplianceQA_SelectAll.Width = "100%";
        HtmlTable_EditBundleComplianceQA_SelectAll.Border = 0;
        HtmlTable_EditBundleComplianceQA_SelectAll.Attributes.Add("Class", "Table_Body");

        using (HtmlTableRow HtmlTableRow_EditBundleComplianceQA_SelectAll = new HtmlTableRow())
        {
          for (int a = 1; a <= 2; a++)
          {
            using (HtmlTableCell HtmlTableCell_EditBundleComplianceQA_SelectAll = new HtmlTableCell())
            {
              HtmlTableCell_EditBundleComplianceQA_SelectAll.Align = HorizontalAlign.Left.ToString();
              HtmlTableCell_EditBundleComplianceQA_SelectAll.VAlign = VerticalAlign.Top.ToString();

              if (a == 1)
              {
                CheckBox CheckBox_EditBundleComplianceQA_SelectAll;
                using (CheckBox_EditBundleComplianceQA_SelectAll = new CheckBox())
                {
                  CheckBox_EditBundleComplianceQA_SelectAll.ID = "CheckBox_EditBundleComplianceQA_SelectAll";
                  CheckBox_EditBundleComplianceQA_SelectAll.Text = "";
                  CheckBox_EditBundleComplianceQA_SelectAll.AutoPostBack = true;
                }

                CheckBox_EditBundleComplianceQA_SelectAll.CheckedChanged += new EventHandler(CheckBox_EditBundleComplianceQA_CheckedChanged);                

                HtmlTableCell_EditBundleComplianceQA_SelectAll.Style.Add("padding", "0px");
                HtmlTableCell_EditBundleComplianceQA_SelectAll.Style.Add("width", "10px");
                HtmlTableCell_EditBundleComplianceQA_SelectAll.Controls.Add(CheckBox_EditBundleComplianceQA_SelectAll);
              }

              if (a == 2)
              {
                using (Label Label_EditBundleComplianceQA_SelectAll = new Label())
                {
                  Label_EditBundleComplianceQA_SelectAll.ID = "Label_EditBundleComplianceQA_SelectAll";
                  Label_EditBundleComplianceQA_SelectAll.Text = Convert.ToString("All", CultureInfo.CurrentCulture);

                  HtmlTableCell_EditBundleComplianceQA_SelectAll.Style.Add("padding", "3px");
                  HtmlTableCell_EditBundleComplianceQA_SelectAll.Controls.Add(Label_EditBundleComplianceQA_SelectAll);
                }
              }

              HtmlTableRow_EditBundleComplianceQA_SelectAll.Cells.Add(HtmlTableCell_EditBundleComplianceQA_SelectAll);
            }
          }

          HtmlTable_EditBundleComplianceQA_SelectAll.Rows.Add(HtmlTableRow_EditBundleComplianceQA_SelectAll);
        }
      }

      return HtmlTable_EditBundleComplianceQA_SelectAll;
    }

    protected static HiddenField BundleComplianceQA_EditControls_TotalQuestions(Int32 editTotalQuestions)
    {
      HiddenField HiddenField_EditBundleComplianceQA_TotalQuestions;
      using (HiddenField_EditBundleComplianceQA_TotalQuestions = new HiddenField())
      {
        HiddenField_EditBundleComplianceQA_TotalQuestions.ID = "HiddenField_EditBundleComplianceQA_TotalQuestions";
        HiddenField_EditBundleComplianceQA_TotalQuestions.Value = editTotalQuestions.ToString(CultureInfo.CurrentCulture);
      }

      return HiddenField_EditBundleComplianceQA_TotalQuestions;
    }

    protected void CheckBox_EditBundleComplianceQA_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox CurrentCheckBox = (CheckBox)sender;
      PlaceHolder PlaceHolder_EditBundleComplianceQA = (PlaceHolder)FormView_IPS_HAI_Form.FindControl("PlaceHolder_EditBundleComplianceQA");
      Int32 TotalQuestions = Convert.ToInt32(((HiddenField)(PlaceHolder_EditBundleComplianceQA.FindControl("HiddenField_EditBundleComplianceQA_TotalQuestions"))).Value, CultureInfo.CurrentCulture);

      if (CurrentCheckBox.ID == "CheckBox_EditBundleComplianceQA_SelectAll")
      {
        for (int a = 1; a <= TotalQuestions; a++)
        {
          if (((CheckBox)(PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_SelectAll"))).Checked == true)
          {
            ((CheckBox)(PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_" + a))).Checked = true;
          }
          else
          {
            ((CheckBox)(PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_" + a))).Checked = false;
          }
        }
      }
      else
      {
        string QuestionAllSelected = "Yes";
        for (int a = 1; a <= TotalQuestions; a++)
        {
          if (((CheckBox)(PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_" + a))).Checked == false)
          {
            QuestionAllSelected = "No";
            ((CheckBox)(PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_SelectAll"))).Checked = false;
          }
        }

        if (QuestionAllSelected == "Yes")
        {
          ((CheckBox)(PlaceHolder_EditBundleComplianceQA.FindControl("CheckBox_EditBundleComplianceQA_SelectAll"))).Checked = true;
        }
      }
    }


    protected void GridView_IPS_HAI_ItemPredisposingConditionList_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_HAI_ItemLabReportsList_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_HAI_ItemBundleComplianceQAList_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_HAI_ItemMeasureList_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void Button_ItemHAIInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }
    //---END--- --CurrentHAI--//


    //--START-- --CurrentInfectionComplete--//
    private void TableCurrentInfectionCompleteVisible()
    {
      TableCurrentInfectionCompleteVisible_Controls();


      FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Request.QueryString["IPSInfectionId"]);
      string IsHAI = FromDataBase_IsHAI_Current.IsHAI;


      if (IsHAI == "True")
      {
        TableCurrentInfectionCompleteVisible_ButtonsHAIYes();
      }
      else
      {
        TableCurrentInfectionCompleteVisible_ButtonsHAINo();
      }
    }

    private void TableCurrentInfectionCompleteVisible_Controls()
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

      Label_CurrentInfectionCompleteInfection.Text = Session["Infection"].ToString();
      Label_CurrentInfectionCompleteHAIInvestigation.Text = Session["HAI"].ToString();

      if (Session["Infection"].ToString() == "Incomplete")
      {
        Hyperlink_CurrentInfectionCompleteHAIInvestigation.Text = Session["HAI"].ToString();
      }
      else
      {
        if (Session["HAI"].ToString() == "Not Required")
        {
          Hyperlink_CurrentInfectionCompleteHAIInvestigation.Text = Session["HAI"].ToString();
        }
        else
        {
          Hyperlink_CurrentInfectionCompleteHAIInvestigation.Text = Convert.ToString("<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Session["IPSInfectionId"].ToString() + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI") + "' Class='Controls_HyperLink_ColorBackground'>" + Session["HAI"].ToString() + "</a>", CultureInfo.CurrentCulture);
        }
      }

      HiddenField_CurrentInfectionCompleteHAIId.Value = Session["IPSHAIId"].ToString();
      Label_CurrentInfectionCompleteSpecimen.Text = Session["Specimen"].ToString();
      Hyperlink_CurrentInfectionCompleteSpecimen.Text = Convert.ToString("<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Session["IPSInfectionId"].ToString() + "") + "' Class='Controls_HyperLink_ColorBackground'>" + Session["Specimen"].ToString() + "</a>", CultureInfo.CurrentCulture);
      HiddenField_CurrentInfectionComplete_ModifiedDate.Value = Session["IPSInfectionModifiedDate"].ToString();
      HiddenField_CurrentInfectionComplete_ModifiedBy.Value = Session["IPSInfectionModifiedBy"].ToString();
      HiddenField_CurrentInfectionComplete_History.Value = Session["IPSInfectionHistory"].ToString();
      HiddenField_CurrentInfectionComplete_HAIModifiedDate.Value = Session["IPSHAIModifiedDate"].ToString();
      Label_CurrentInfectionCompleteIsActive.Text = Session["IPSInfectionIsActive"].ToString();

      if (Session["IPSInfectionIsActive"].ToString() == "Yes")
      {
        if (Session["Infection"].ToString() == "Incomplete")
        {
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "background-color: #d46e6e");
        }
        else
        {
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "background-color: #77cf9c");
        }

        if (Session["HAI"].ToString() == "Incomplete")
        {
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "background-color: #d46e6e");
        }
        else
        {
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "background-color: #77cf9c");
        }

        if (Session["Specimen"].ToString() == "Incomplete")
        {
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "background-color: #d46e6e");
        }
        else
        {
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "background-color: #77cf9c");
        }

        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "background-color: #77cf9c");
      }
      else
      {
        CurrentInfectionCompleteInfection.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteInfection.Attributes.Add("Style", "background-color: #77cf9c");
        CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "background-color: #77cf9c");
        CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "background-color: #77cf9c");
        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "background-color: #d46e6e");
      }

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

    private void TableCurrentInfectionCompleteVisible_ButtonsHAIYes()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
      string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

      FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_Current = GetSpecimenCompleted();
      string Specimen = FromDataBase_SpecimenCompleted_Current.Specimen;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      FromDataBase_HAICompleted FromDataBase_HAICompleted_Current = GetHAICompleted();
      string IPSHAICompleted = FromDataBase_HAICompleted_Current.IPSHAICompleted;


      Button_HAINo_SpecimenIncomplete.Visible = false;
      Button_HAINo_InfectionCanceled.Visible = false;
      Button_HAINo_CompleteInfection.Visible = false;
      Button_HAINo_OpenInfection.Visible = false;
      Button_HAINo_CaptureNewInfection.Visible = false;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (IPSInfectionSiteInfectionIsActive == "False")
        {
          if (IPSInfectionIsActive == "True")
          {
            Button_HAIYes_LinkedSiteRequired.Visible = true;
            Button_HAIYes_SpecimenIncomplete.Visible = false;
            Button_HAIYes_InfectionCanceled.Visible = false;
            Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenInfection.Visible = false;
            Button_HAIYes_CompleteHAIInvestigation.Visible = false;
            Button_HAIYes_OpenHAIInvestigation.Visible = false;
            Button_HAIYes_CaptureNewInfection.Visible = false;
          }
          else
          {
            Button_HAIYes_LinkedSiteRequired.Visible = false;
            Button_HAIYes_SpecimenIncomplete.Visible = false;
            Button_HAIYes_InfectionCanceled.Visible = true;
            Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenInfection.Visible = false;
            Button_HAIYes_CompleteHAIInvestigation.Visible = false;
            Button_HAIYes_OpenHAIInvestigation.Visible = false;
            Button_HAIYes_CaptureNewInfection.Visible = true;
          }
        }
        else
        {
          if (Specimen == "Incomplete")
          {
            if (IPSInfectionIsActive == "True")
            {
              Button_HAIYes_LinkedSiteRequired.Visible = false;
              Button_HAIYes_SpecimenIncomplete.Visible = true;
              Button_HAIYes_InfectionCanceled.Visible = false;
              Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenInfection.Visible = false;
              Button_HAIYes_CompleteHAIInvestigation.Visible = false;
              Button_HAIYes_OpenHAIInvestigation.Visible = false;
              Button_HAIYes_CaptureNewInfection.Visible = false;
            }
            else
            {
              Button_HAIYes_LinkedSiteRequired.Visible = false;
              Button_HAIYes_SpecimenIncomplete.Visible = false;
              Button_HAIYes_InfectionCanceled.Visible = true;
              Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenInfection.Visible = false;
              Button_HAIYes_CompleteHAIInvestigation.Visible = false;
              Button_HAIYes_OpenHAIInvestigation.Visible = false;
              Button_HAIYes_CaptureNewInfection.Visible = true;
            }
          }
          else
          {
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
            {
              Button_HAIYes_LinkedSiteRequired.Visible = false;
              Button_HAIYes_SpecimenIncomplete.Visible = false;
              Button_HAIYes_InfectionCanceled.Visible = false;
              Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = true;
              Button_HAIYes_OpenInfection.Visible = false;
              Button_HAIYes_CompleteHAIInvestigation.Visible = false;
              Button_HAIYes_OpenHAIInvestigation.Visible = false;
              Button_HAIYes_CaptureNewInfection.Visible = false;
            }
            else
            {
              if (IPSInfectionIsActive == "True")
              {
                if (IPSHAICompleted == "False")
                {
                  Button_HAIYes_LinkedSiteRequired.Visible = false;
                  Button_HAIYes_SpecimenIncomplete.Visible = false;
                  Button_HAIYes_InfectionCanceled.Visible = false;
                  Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
                  Button_HAIYes_OpenInfection.Visible = true;
                  Button_HAIYes_CompleteHAIInvestigation.Visible = true;
                  Button_HAIYes_OpenHAIInvestigation.Visible = false;
                  Button_HAIYes_CaptureNewInfection.Visible = false;
                }
                else
                {
                  Button_HAIYes_LinkedSiteRequired.Visible = false;
                  Button_HAIYes_SpecimenIncomplete.Visible = false;
                  Button_HAIYes_InfectionCanceled.Visible = false;
                  Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
                  Button_HAIYes_OpenInfection.Visible = true;
                  Button_HAIYes_CompleteHAIInvestigation.Visible = false;
                  Button_HAIYes_OpenHAIInvestigation.Visible = true;
                  Button_HAIYes_CaptureNewInfection.Visible = true;
                }
              }
              else
              {
                Button_HAIYes_LinkedSiteRequired.Visible = false;
                Button_HAIYes_SpecimenIncomplete.Visible = false;
                Button_HAIYes_InfectionCanceled.Visible = true;
                Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
                Button_HAIYes_OpenInfection.Visible = false;
                Button_HAIYes_CompleteHAIInvestigation.Visible = false;
                Button_HAIYes_OpenHAIInvestigation.Visible = false;
                Button_HAIYes_CaptureNewInfection.Visible = true;
              }
            }
          }
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";

        Button_HAIYes_LinkedSiteRequired.Visible = false;
        Button_HAIYes_SpecimenIncomplete.Visible = false;
        Button_HAIYes_InfectionCanceled.Visible = false;
        Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenInfection.Visible = false;
        Button_HAIYes_CompleteHAIInvestigation.Visible = false;
        Button_HAIYes_OpenHAIInvestigation.Visible = false;
        Button_HAIYes_CaptureNewInfection.Visible = false;
      }

      if (Security == "1")
      {
        Security = "0";

        Button_HAIYes_LinkedSiteRequired.Visible = false;
        Button_HAIYes_SpecimenIncomplete.Visible = false;
        Button_HAIYes_InfectionCanceled.Visible = false;
        Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenInfection.Visible = false;
        Button_HAIYes_CompleteHAIInvestigation.Visible = false;
        Button_HAIYes_OpenHAIInvestigation.Visible = false;
        Button_HAIYes_CaptureNewInfection.Visible = false;
      }
    }

    private void TableCurrentInfectionCompleteVisible_ButtonsHAINo()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_Current = GetSpecimenCompleted();
      string Specimen = FromDataBase_SpecimenCompleted_Current.Specimen;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;


      Button_HAIYes_LinkedSiteRequired.Visible = false;
      Button_HAIYes_SpecimenIncomplete.Visible = false;
      Button_HAIYes_InfectionCanceled.Visible = false;
      Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
      Button_HAIYes_OpenInfection.Visible = false;
      Button_HAIYes_CompleteHAIInvestigation.Visible = false;
      Button_HAIYes_OpenHAIInvestigation.Visible = false;
      Button_HAIYes_CaptureNewInfection.Visible = false;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (Specimen == "Incomplete")
        {
          if (IPSInfectionIsActive == "True")
          {
            Button_HAINo_SpecimenIncomplete.Visible = true;
            Button_HAINo_InfectionCanceled.Visible = false;
            Button_HAINo_CompleteInfection.Visible = false;
            Button_HAINo_OpenInfection.Visible = false;
            Button_HAINo_CaptureNewInfection.Visible = false;
          }
          else
          {
            Button_HAINo_SpecimenIncomplete.Visible = false;
            Button_HAINo_InfectionCanceled.Visible = true;
            Button_HAINo_CompleteInfection.Visible = false;
            Button_HAINo_OpenInfection.Visible = false;
            Button_HAINo_CaptureNewInfection.Visible = true;
          }
        }
        else
        {
          if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
          {
            Button_HAINo_SpecimenIncomplete.Visible = false;
            Button_HAINo_InfectionCanceled.Visible = false;
            Button_HAINo_CompleteInfection.Visible = true;
            Button_HAINo_OpenInfection.Visible = false;
            Button_HAINo_CaptureNewInfection.Visible = false;
          }
          else
          {
            if (IPSInfectionIsActive == "True")
            {
              Button_HAINo_SpecimenIncomplete.Visible = false;
              Button_HAINo_InfectionCanceled.Visible = false;
              Button_HAINo_CompleteInfection.Visible = false;
              Button_HAINo_OpenInfection.Visible = true;
              Button_HAINo_CaptureNewInfection.Visible = true;
            }
            else
            {
              Button_HAINo_SpecimenIncomplete.Visible = false;
              Button_HAINo_InfectionCanceled.Visible = true;
              Button_HAINo_CompleteInfection.Visible = false;
              Button_HAINo_OpenInfection.Visible = false;
              Button_HAINo_CaptureNewInfection.Visible = true;
            }
          }
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";

        Button_HAINo_SpecimenIncomplete.Visible = false;
        Button_HAINo_InfectionCanceled.Visible = false;
        Button_HAINo_CompleteInfection.Visible = false;
        Button_HAINo_OpenInfection.Visible = false;
        Button_HAINo_CaptureNewInfection.Visible = false;
      }

      if (Security == "1")
      {
        Security = "0";

        Button_HAINo_SpecimenIncomplete.Visible = false;
        Button_HAINo_InfectionCanceled.Visible = false;
        Button_HAINo_CompleteInfection.Visible = false;
        Button_HAINo_OpenInfection.Visible = false;
        Button_HAINo_CaptureNewInfection.Visible = false;
      }
    }


    protected void Button_InfectionInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void Button_HAIYes_LinkedSiteRequired_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAIYes_SpecimenIncomplete_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAIYes_InfectionCanceled_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAIYes_CompleteInfectionGoToHAIInvestigation_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History , IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @IPS_Infection_Id";
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
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 1);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI"), false);
        }
      }
    }

    protected void Button_HAIYes_OpenInfection_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History , IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @IPS_Infection_Id";
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
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id ; UPDATE Form_IPS_HAI SET IPS_HAI_Investigation_Completed = 0 , IPS_HAI_Investigation_CompletedDate = NULL WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 0);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DBNull.Value);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentInfection"), false);
        }
      }
    }

    protected void Button_HAIYes_CompleteHAIInvestigation_OnClick(object sender, EventArgs e)
    {
      CheckBox CheckBox_EditInvestigationCompleted = (CheckBox)FormView_IPS_HAI_Form.FindControl("CheckBox_EditInvestigationCompleted");
      CheckBox_EditInvestigationCompleted.Checked = true;

      FormView_IPS_HAI_Form.UpdateItem(true);
    }

    protected void Button_HAIYes_OpenHAIInvestigation_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSHAIModifiedDate"] = HiddenField_CurrentInfectionComplete_HAIModifiedDate.Value.ToString();
      object OLDIPSHAIModifiedDate = Session["OLDIPSHAIModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSHAIModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringHAI = "SELECT IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy , IPS_HAI_History , IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_HAI = new SqlCommand(SQLStringHAI))
      {
        SqlCommand_HAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_HAI;
        using (DataTable_HAI = new DataTable())
        {
          DataTable_HAI.Locale = CultureInfo.CurrentCulture;
          DataTable_HAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAI).Copy();
          if (DataTable_HAI.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HAI.Rows)
            {
              Session["DBIPSHAIModifiedDate"] = DataRow_Row["IPS_HAI_ModifiedDate"];
              Session["DBIPSHAIModifiedBy"] = DataRow_Row["IPS_HAI_ModifiedBy"];
              Session["IPSHAIHistory"] = DataRow_Row["IPS_HAI_History"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      object DBIPSHAIModifiedDate = Session["DBIPSHAIModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSHAIModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSHAIModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_HAI", "IPS_HAI_Id = " + Session["IPSHAIId"].ToString());

          Session["IPSHAIHistory"] = Session["History"].ToString() + Session["IPSHAIHistory"].ToString();

          string SQLStringUpdateHAI = "UPDATE Form_IPS_HAI SET IPS_HAI_Investigation_Completed = @IPS_HAI_Investigation_Completed , IPS_HAI_Investigation_CompletedDate = @IPS_HAI_Investigation_CompletedDate , IPS_HAI_ModifiedDate = @IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy = @IPS_HAI_ModifiedBy , IPS_HAI_History = @IPS_HAI_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateHAI = new SqlCommand(SQLStringUpdateHAI))
          {
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_Completed", 0);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_CompletedDate", DBNull.Value);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_History", Session["IPSHAIHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateHAI);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI"), false);
        }
      }
    }

    protected void Button_HAIYes_CaptureNewInfection_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx"), false);
    }


    protected void Button_HAINo_SpecimenIncomplete_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAINo_InfectionCanceled_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAINo_CompleteInfection_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
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
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 1);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
        }
      }
    }

    protected void Button_HAINo_OpenInfection_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
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
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 0);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DBNull.Value);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentInfection"), false);
        }
      }
    }

    protected void Button_HAINo_CaptureNewInfection_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx"), false);
    }
    //---END--- --CurrentInfectionComplete--//


    //--START-- --File--//
    protected void FileRegisterPostBackControl()
    {
      ScriptManager ScriptManager_File = ScriptManager.GetCurrent(Page);

      if (Button_UploadFile != null)
      {
        ScriptManager_File.RegisterPostBackControl(Button_UploadFile);
      }
    }

    public static string DatabaseFileName(object ips_File_Name)
    {
      string DatabaseFileName = "";
      if (ips_File_Name != null)
      {
        DatabaseFileName = "" + ips_File_Name.ToString() + "";
      }

      return DatabaseFileName;
    }

    protected void RetrieveDatabaseFile(object sender, EventArgs e)
    {
      LinkButton LinkButton_IPSFile = (LinkButton)sender;
      string FileId = LinkButton_IPSFile.CommandArgument.ToString();

      Session["IPSFileName"] = "";
      Session["IPSFileContentType"] = "";
      Session["IPSFileData"] = "";
      string SQLStringIPSFile = "SELECT IPS_File_Name ,IPS_File_ContentType ,IPS_File_Data FROM Form_IPS_File WHERE IPS_File_Id = @IPS_File_Id";
      using (SqlCommand SqlCommand_IPSFile = new SqlCommand(SQLStringIPSFile))
      {
        SqlCommand_IPSFile.Parameters.AddWithValue("@IPS_File_Id", FileId);
        DataTable DataTable_IPSFile;
        using (DataTable_IPSFile = new DataTable())
        {
          DataTable_IPSFile.Locale = CultureInfo.CurrentCulture;
          DataTable_IPSFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IPSFile).Copy();
          if (DataTable_IPSFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_IPSFile.Rows)
            {
              Session["IPSFileName"] = DataRow_Row["IPS_File_Name"];
              Session["IPSFileContentType"] = DataRow_Row["IPS_File_ContentType"];
              Session["IPSFileData"] = DataRow_Row["IPS_File_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["IPSFileData"].ToString()))
      {
        Byte[] Byte_FileData = (Byte[])Session["IPSFileData"];
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = Session["IPSFileContentType"].ToString();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Session["IPSFileName"].ToString() + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      Session.Remove("IPSFileName");
      Session.Remove("IPSFileContentType");
      Session.Remove("IPSFileData");
    }

    protected void FileContentTypeHandlers()
    {
      FileContentTypeHandler.Add(".doc", "application/vnd.ms-word");
      FileContentTypeHandler.Add(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
      FileContentTypeHandler.Add(".xls", "application/vnd.ms-excel");
      FileContentTypeHandler.Add(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
      FileContentTypeHandler.Add(".pdf", "application/pdf");
      FileContentTypeHandler.Add(".tif", "image/tiff");
      FileContentTypeHandler.Add(".tiff", "image/tiff");
      FileContentTypeHandler.Add(".txt", "text/plain");
      FileContentTypeHandler.Add(".msg", "application/vnd.ms-outlook");
      FileContentTypeHandler.Add(".jpeg", "image/jpeg");
      FileContentTypeHandler.Add(".jpg", "image/jpeg");
      FileContentTypeHandler.Add(".gif", "image/gif");
      FileContentTypeHandler.Add(".png", "image/png");
    }

    protected string FileContentType(string fileExtension)
    {
      if (string.IsNullOrEmpty(fileExtension))
      {
        return "";
      }
      else
      {
        FileContentTypeHandlers();

        if (FileContentTypeHandler.ContainsKey(fileExtension))
        {
          string FileContentTypeValue = FileContentTypeHandler[fileExtension];
          FileContentTypeHandler.Clear();
          return FileContentTypeValue;
        }
        else
        {
          FileContentTypeHandler.Clear();
          return "";
        }
      }
    }


    protected void SqlDataSource_IPS_File_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        HiddenField_File.Value = TotalRecords.ToString(CultureInfo.CurrentCulture);

        if (TotalRecords > 0)
        {
          GridView_IPS_File.Visible = true;
        }
        else
        {
          GridView_IPS_File.Visible = false;
        }
      }
    }

    protected void GridView_IPS_File_PreRender(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = (GridViewRow)GridView_IPS_File.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_File_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_UploadFile_OnClick(object sender, EventArgs e)
    {
      string UploadMessage = "";

      if (!FileUpload_File.HasFiles)
      {
        UploadMessage = UploadMessage + Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>No file chosen</span>", CultureInfo.CurrentCulture);
      }
      else
      {
        foreach (HttpPostedFile HttpPostedFile_File in FileUpload_File.PostedFiles)
        {
          string FileName = Path.GetFileName(HttpPostedFile_File.FileName);
          string FileExtension = System.IO.Path.GetExtension(FileName);
          FileExtension = FileExtension.ToLower(CultureInfo.CurrentCulture);
          string FileContentTypeValue = FileContentType(FileExtension);
          decimal FileSize = HttpPostedFile_File.ContentLength;
          decimal FileSizeMB = (FileSize / 1024 / 1024);
          string FileSizeMBString = FileSizeMB.ToString("N2", CultureInfo.CurrentCulture);

          if ((!string.IsNullOrEmpty(FileContentTypeValue)) && (FileSize < 5242880))
          {
            Session["IPSFileId"] = "";
            string SQLStringExistingFile = "SELECT IPS_File_Id FROM Form_IPS_File WHERE IPS_File_CreatedBy = @IPS_File_CreatedBy AND IPS_Infection_Id = @IPS_Infection_Id AND IPS_File_Name = @IPS_File_Name";
            using (SqlCommand SqlCommand_ExistingFile = new SqlCommand(SQLStringExistingFile))
            {
              SqlCommand_ExistingFile.Parameters.AddWithValue("@IPS_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@IPS_File_Name", FileName);
              DataTable DataTable_ExistingFile;
              using (DataTable_ExistingFile = new DataTable())
              {
                DataTable_ExistingFile.Locale = CultureInfo.CurrentCulture;
                DataTable_ExistingFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ExistingFile).Copy();
                if (DataTable_ExistingFile.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_ExistingFile.Rows)
                  {
                    Session["IPSFileId"] = DataRow_Row["IPS_File_Id"];
                  }
                }
              }
            }

            if (!string.IsNullOrEmpty(Session["IPSFileId"].ToString()))
            {
              UploadMessage = Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span>", CultureInfo.CurrentCulture);
            }
            else
            {
              Stream Stream_File = HttpPostedFile_File.InputStream;
              BinaryReader BinaryReader_File = new BinaryReader(Stream_File);
              Byte[] Byte_File = BinaryReader_File.ReadBytes((Int32)Stream_File.Length);

              string SQLStringFile = "INSERT INTO Form_IPS_File ( IPS_Infection_Id , IPS_File_Name , IPS_File_ContentType , IPS_File_Data , IPS_File_CreatedDate , IPS_File_CreatedBy ) VALUES ( @IPS_Infection_Id , @IPS_File_Name , @IPS_File_ContentType , @IPS_File_Data , @IPS_File_CreatedDate , @IPS_File_CreatedBy )";
              using (SqlCommand SqlCommand_File = new SqlCommand(SQLStringFile))
              {
                SqlCommand_File.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_Name", FileName);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_ContentType", FileContentTypeValue);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_Data", Byte_File);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_CreatedDate", DateTime.Now);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_File);
              }

              GridView_IPS_File.DataBind();
            }

            Session.Remove("IPSFileId");
          }
          else
          {
            if (string.IsNullOrEmpty(FileContentTypeValue))
            {
              UploadMessage = UploadMessage + Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>Only doc, docx, xls, xlsx, pdf, tif, tiff, txt, msg, jpeg, jpg, gif and png files can be uploaded<br/>File Name: " + FileName + "</span>", CultureInfo.CurrentCulture);
            }

            if (FileSize > 5242880)
            {
              UploadMessage = UploadMessage + Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>Only files smaller then 5 MB can be uploaded<br/>File Name: " + FileName + "<br/>File Size: " + FileSizeMBString + " MB</span>", CultureInfo.CurrentCulture);
            }
          }
        }
      }

      FileRegisterPostBackControl();
      Label_MessageFile.Text = UploadMessage;
      ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentFile);
    }

    protected void Button_DeleteFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      for (int i = 0; i < GridView_IPS_File.Rows.Count; i++)
      {
        CheckBox CheckBox_File = (CheckBox)GridView_IPS_File.Rows[i].Cells[0].FindControl("CheckBox_File");
        Int32 FileId = 0;

        if (CheckBox_File.Checked == true)
        {
          FileId = Convert.ToInt32(CheckBox_File.CssClass, CultureInfo.CurrentCulture);

          string SQLStringIPSFile = "DELETE FROM Form_IPS_File WHERE IPS_File_Id = @IPS_File_Id";
          using (SqlCommand SqlCommand_IPSFile = new SqlCommand(SQLStringIPSFile))
          {
            SqlCommand_IPSFile.Parameters.AddWithValue("@IPS_File_Id", FileId);

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_IPSFile);

            DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
          }
        }
      }

      FileRegisterPostBackControl();
      Label_MessageFile.Text = DeleteMessage;
      GridView_IPS_File.DataBind();
      ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentFile);
    }

    protected void Button_DeleteAllFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      for (int i = 0; i < GridView_IPS_File.Rows.Count; i++)
      {
        CheckBox CheckBox_File = (CheckBox)GridView_IPS_File.Rows[i].Cells[0].FindControl("CheckBox_File");
        Int32 FileId = 0;

        FileId = Convert.ToInt32(CheckBox_File.CssClass, CultureInfo.CurrentCulture);

        string SQLStringIPSFile = "DELETE FROM Form_IPS_File WHERE IPS_File_Id = @IPS_File_Id";
        using (SqlCommand SqlCommand_IPSFile = new SqlCommand(SQLStringIPSFile))
        {
          SqlCommand_IPSFile.Parameters.AddWithValue("@IPS_File_Id", FileId);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_IPSFile);

          DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
        }
      }

      FileRegisterPostBackControl();
      Label_MessageFile.Text = DeleteMessage;
      GridView_IPS_File.DataBind();
      ToolkitScriptManager_IPS_HAI.SetFocus(LinkButton_CurrentFile);
    }

    protected void Button_UploadFile_DataBinding(object sender, EventArgs e)
    {
      ScriptManager ScriptManager_UploadFile = ScriptManager.GetCurrent(Page);
      ScriptManager_UploadFile.RegisterPostBackControl(Button_UploadFile);
    }

    protected void Button_DeleteFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_DeleteFile = (Button)sender;
      ScriptManager ScriptManager_DeleteFile = ScriptManager.GetCurrent(Page);
      ScriptManager_DeleteFile.RegisterPostBackControl(Button_DeleteFile);
    }

    protected void Button_DeleteAllFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_DeleteAllFile = (Button)sender;
      ScriptManager ScriptManager_DeleteAllFile = ScriptManager.GetCurrent(Page);
      ScriptManager_DeleteAllFile.RegisterPostBackControl(Button_DeleteAllFile);
    }

    protected void LinkButton_File_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_File = (LinkButton)sender;
      ScriptManager ScriptManager_File = ScriptManager.GetCurrent(Page);
      ScriptManager_File.RegisterPostBackControl(LinkButton_File);
    }
    //---END--- --File--//


    //--START-- --FileList--//
    protected void SqlDataSource_IPS_File_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenFileListTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_File_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_File_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_FileListTotalRecords = (Label)e.Row.FindControl("Label_FileListTotalRecords");
          Label_FileListTotalRecords.Text = Label_HiddenFileListTotalRecords.Text;
        }
      }
    }
    //---END--- --FileList--//
  }
}