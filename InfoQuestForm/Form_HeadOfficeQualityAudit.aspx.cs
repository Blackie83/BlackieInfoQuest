using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_HeadOfficeQualityAudit : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected bool Button_EditUpdateClicked = false;
    protected bool Button_EditPrintClicked = false;
    protected bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_HeadOfficeQualityAudit, GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("40")).ToString(), CultureInfo.CurrentCulture);
          Label_FormHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("40")).ToString();

          if (string.IsNullOrEmpty(Request.QueryString["HQAFindingId"]))
          {
            SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters["TableSELECT"].DefaultValue = "HQA_Finding_Criteria_List";
            SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters["TableFROM"].DefaultValue = "Form_HeadOfficeQualityAudit_Finding";
            SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters["TableWHERE"].DefaultValue = "HQA_Finding_Id = " + Request.QueryString["HQAFindingId"] + " ";

            SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters["TableSELECT"].DefaultValue = "HQA_Finding_SubCriteria_List";
            SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters["TableFROM"].DefaultValue = "Form_HeadOfficeQualityAudit_Finding";
            SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters["TableWHERE"].DefaultValue = "HQA_Finding_Id = " + Request.QueryString["HQAFindingId"] + " ";

            SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters["TableSELECT"].DefaultValue = "HQA_Finding_Classification_List";
            SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters["TableFROM"].DefaultValue = "Form_HeadOfficeQualityAudit_Finding";
            SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters["TableWHERE"].DefaultValue = "HQA_Finding_Id = " + Request.QueryString["HQAFindingId"] + " ";

            SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters["TableSELECT"].DefaultValue = "HQA_Finding_Tracking_List";
            SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters["TableFROM"].DefaultValue = "Form_HeadOfficeQualityAudit_Finding";
            SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters["TableWHERE"].DefaultValue = "HQA_Finding_Id = " + Request.QueryString["HQAFindingId"] + " ";

            SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters["TableSELECT"].DefaultValue = "HQA_Finding_LateCloseOut_List";
            SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters["TableFROM"].DefaultValue = "Form_HeadOfficeQualityAudit_Finding";
            SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters["TableWHERE"].DefaultValue = "HQA_Finding_Id = " + Request.QueryString["HQAFindingId"] + " ";
          }

          SetFormVisibility();

          TableFormVisible();
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
        if (string.IsNullOrEmpty(Request.QueryString["HQAFindingId"]))
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('40'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('40')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @HQA_Finding_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@HQA_Finding_Id", Request.QueryString["HQAFindingId"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("40");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_HeadOfficeQualityAudit.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Head Office Quality Audit", "14");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertFacility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "175");
      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertFunctionList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "176");
      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "178");
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "177");
      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertClassificationList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "173");
      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_InsertTrackingList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "176");
      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "178");
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "177");
      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditClassificationList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "173");
      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditTrackingList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "174");
      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_EditLateCloseoutList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");


      SqlDataSource_HeadOfficeQualityAudit_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertCommand = "INSERT INTO Form_HeadOfficeQualityAudit_Finding ( Facility_Id , HQA_Finding_Function_List , HQA_Finding_FindingNo , HQA_Finding_FindingDate , HQA_Finding_FinancialYear , HQA_Finding_Auditor , HQA_Finding_Criteria_List , HQA_Finding_SubCriteria_List , HQA_Finding_Classification_List , HQA_Finding_Description , HQA_Finding_ImmediateAction , HQA_Finding_RootCause , HQA_Finding_CorrectiveAction , HQA_Finding_Evaluation , HQA_Finding_Tracking_List , HQA_Finding_TrackingDate , HQA_Finding_LateCloseOut_List , HQA_Finding_LateCloseOut_List_Other , HQA_Finding_CreatedDate , HQA_Finding_CreatedBy , HQA_Finding_ModifiedDate , HQA_Finding_ModifiedBy , HQA_Finding_History , HQA_Finding_IsActive , HQA_Finding_Archived ) VALUES ( @Facility_Id , @HQA_Finding_Function_List , @HQA_Finding_FindingNo , @HQA_Finding_FindingDate , @HQA_Finding_FinancialYear , @HQA_Finding_Auditor , @HQA_Finding_Criteria_List , @HQA_Finding_SubCriteria_List , @HQA_Finding_Classification_List , @HQA_Finding_Description , @HQA_Finding_ImmediateAction , @HQA_Finding_RootCause , @HQA_Finding_CorrectiveAction , @HQA_Finding_Evaluation , @HQA_Finding_Tracking_List , @HQA_Finding_TrackingDate , @HQA_Finding_LateCloseOut_List , @HQA_Finding_LateCloseOut_List_Other , @HQA_Finding_CreatedDate , @HQA_Finding_CreatedBy , @HQA_Finding_ModifiedDate , @HQA_Finding_ModifiedBy , @HQA_Finding_History , @HQA_Finding_IsActive , @HQA_Finding_Archived ); SELECT @HQA_Finding_Id = SCOPE_IDENTITY()";
      SqlDataSource_HeadOfficeQualityAudit_Form.SelectCommand = "SELECT * FROM Form_HeadOfficeQualityAudit_Finding WHERE ( HQA_Finding_Id = @HQA_Finding_Id )";
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateCommand = "UPDATE Form_HeadOfficeQualityAudit_Finding SET HQA_Finding_FindingDate = @HQA_Finding_FindingDate , HQA_Finding_FinancialYear = @HQA_Finding_FinancialYear , HQA_Finding_Auditor = @HQA_Finding_Auditor , HQA_Finding_Criteria_List = @HQA_Finding_Criteria_List , HQA_Finding_SubCriteria_List = @HQA_Finding_SubCriteria_List , HQA_Finding_Classification_List = @HQA_Finding_Classification_List , HQA_Finding_Description = @HQA_Finding_Description , HQA_Finding_ImmediateAction = @HQA_Finding_ImmediateAction , HQA_Finding_RootCause = @HQA_Finding_RootCause , HQA_Finding_CorrectiveAction = @HQA_Finding_CorrectiveAction , HQA_Finding_Evaluation = @HQA_Finding_Evaluation , HQA_Finding_Tracking_List = @HQA_Finding_Tracking_List , HQA_Finding_TrackingDate = @HQA_Finding_TrackingDate , HQA_Finding_LateCloseOut_List = @HQA_Finding_LateCloseOut_List , HQA_Finding_LateCloseOut_List_Other = @HQA_Finding_LateCloseOut_List_Other , HQA_Finding_ModifiedDate = @HQA_Finding_ModifiedDate , HQA_Finding_ModifiedBy = @HQA_Finding_ModifiedBy , HQA_Finding_History = @HQA_Finding_History , HQA_Finding_IsActive = @HQA_Finding_IsActive WHERE HQA_Finding_Id = @HQA_Finding_Id";
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_Id", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("Facility_Id", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_Function_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_FindingNo", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_FindingDate", TypeCode.DateTime, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_FinancialYear", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_Auditor", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_Criteria_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_SubCriteria_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_Classification_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_Description", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_ImmediateAction", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_RootCause", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_CorrectiveAction", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_Evaluation", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_Tracking_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_TrackingDate", TypeCode.DateTime, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_LateCloseOut_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_LateCloseOut_List_Other", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_CreatedBy", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_ModifiedBy", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_History", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_IsActive", TypeCode.Boolean, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters.Add("HQA_Finding_Archived", TypeCode.Boolean, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_Form.SelectParameters.Add("HQA_Finding_Id", TypeCode.Int32, Request.QueryString["HQAFindingId"]);
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_FindingDate", TypeCode.DateTime, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_FinancialYear", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_Auditor", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_Criteria_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_SubCriteria_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_Classification_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_Description", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_ImmediateAction", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_RootCause", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_CorrectiveAction", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_Evaluation", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_Tracking_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_TrackingDate", TypeCode.DateTime, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_LateCloseOut_List", TypeCode.Int32, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_LateCloseOut_List_Other", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_ModifiedBy", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_History", TypeCode.String, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_IsActive", TypeCode.Boolean, "");
      SqlDataSource_HeadOfficeQualityAudit_Form.UpdateParameters.Add("HQA_Finding_Id", TypeCode.Int32, "");
    }

    protected void SetFormVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["HQAFindingId"]))
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
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminCapture = FromDataBase_SecurityRole_Current.SecurityFormAdminCapture;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFormAdminCapture.Length > 0))
      {
        Security = "0";
        TableFormNoAccess.Visible = false;
        FormView_HeadOfficeQualityAudit_Form.Visible = true;
        FormView_HeadOfficeQualityAudit_Form.ChangeMode(FormViewMode.Insert);
      }

      if (Security == "1")
      {
        Security = "0";
        TableFormNoAccess.Visible = true;
        FormView_HeadOfficeQualityAudit_Form.Visible = false;
      }
    }

    protected void SetFormVisibility_Edit()
    {
      FromDataBase_SecurityRole_Edit FromDataBase_SecurityRole_Edit_Current = GetSecurityRoleEdit();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Edit_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminCapture = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminCapture;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminHospitalManagerCompletion = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminHospitalManagerCompletion;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminUpdate;

      FromDataBase_Tracking FromDataBase_Tracking_Current = GetTracking();
      string HQAFindingTrackingList = FromDataBase_Tracking_Current.HQAFindingTrackingList;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
      {
        Security = "0";
        TableFormNoAccess.Visible = false;
        FormView_HeadOfficeQualityAudit_Form.Visible = true;
        FormView_HeadOfficeQualityAudit_Form.ChangeMode(FormViewMode.Edit);
      }

      if (Security == "1" && (SecurityFormAdminCapture.Length > 0 || SecurityFormAdminView.Length > 0))
      {
        Security = "0";
        TableFormNoAccess.Visible = false;
        FormView_HeadOfficeQualityAudit_Form.Visible = true;
        FormView_HeadOfficeQualityAudit_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1" && (SecurityFacilityAdminHospitalManagerCompletion.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        TableFormNoAccess.Visible = false;
        FormView_HeadOfficeQualityAudit_Form.Visible = true;

        if (HQAFindingTrackingList != "5399")
        {
          FormView_HeadOfficeQualityAudit_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_HeadOfficeQualityAudit_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1")
      {
        Security = "0";
        TableFormNoAccess.Visible = false;
        FormView_HeadOfficeQualityAudit_Form.Visible = true;
        FormView_HeadOfficeQualityAudit_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableFormVisible()
    {
      if (FormView_HeadOfficeQualityAudit_Form.Visible == true)
      {
        if (FormView_HeadOfficeQualityAudit_Form.CurrentMode == FormViewMode.Insert)
        {
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertCriteriaList")).Items.Clear();
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertCriteriaList")).Items.Insert(0, new ListItem(Convert.ToString("Select Criteria", CultureInfo.CurrentCulture), ""));

          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).Items.Clear();
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).Items.Insert(0, new ListItem(Convert.ToString("Select Sub Criteria", CultureInfo.CurrentCulture), ""));

          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertTrackingList")).SelectedValue = "5396";

          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertFacility")).Attributes.Add("OnChange", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertFunctionList")).Attributes.Add("OnChange", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertFindingDate")).Attributes.Add("OnChange", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertFindingDate")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertAuditor")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertAuditor")).Attributes.Add("OnInput", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertCriteriaList")).Attributes.Add("OnChange", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).Attributes.Add("OnChange", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertClassificationList")).Attributes.Add("OnChange", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertImmediateAction")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertImmediateAction")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertRootCause")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertRootCause")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertCorrectiveAction")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertCorrectiveAction")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertEvaluation")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertEvaluation")).Attributes.Add("OnInput", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertTrackingList")).Attributes.Add("OnChange", "Validation_Form();");
        }

        if (FormView_HeadOfficeQualityAudit_Form.CurrentMode == FormViewMode.Edit)
        {
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditFindingDate")).Attributes.Add("OnChange", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditFindingDate")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditAuditor")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditAuditor")).Attributes.Add("OnInput", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditCriteriaList")).Attributes.Add("OnChange", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).Attributes.Add("OnChange", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).Attributes.Add("OnChange", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditImmediateAction")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditImmediateAction")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditRootCause")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditRootCause")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditCorrectiveAction")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditCorrectiveAction")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditEvaluation")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditEvaluation")).Attributes.Add("OnInput", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).Attributes.Add("OnChange", "Validation_Form();");
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditLateCloseOutListOther")).Attributes.Add("OnKeyUp", "Validation_Form();");
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditLateCloseOutListOther")).Attributes.Add("OnInput", "Validation_Form();");
        }
      }
    }

    protected void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_HQAFindingFunction"];
      string SearchField3 = Request.QueryString["Search_HQAFindingFinancialYear"];
      string SearchField4 = Request.QueryString["Search_HQAFindingClassification"];
      string SearchField5 = Request.QueryString["Search_HQAFindingTracking"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_HQA_Finding_Function=" + Request.QueryString["Search_HQAFindingFunction"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_HQA_Finding_FinancialYear=" + Request.QueryString["Search_HQAFindingFinancialYear"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_HQA_Finding_Classification=" + Request.QueryString["Search_HQAFindingClassification"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_HQA_Finding_Tracking=" + Request.QueryString["Search_HQAFindingTracking"] + "&";
      }

      string FinalURL = "Form_HeadOfficeQualityAudit_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding Captured Findings", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    private class FromDataBase_SecurityRole
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminCapture { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
      public DataRow[] SecurityFacilityAdminHospitalManagerCompletion { get; set; }
      public DataRow[] SecurityFacilityAdminUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminView { get; set; }
    }

    private FromDataBase_SecurityRole GetSecurityRole()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_New = new FromDataBase_SecurityRole();

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('40'))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
          FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '164'");
          FromDataBase_SecurityRole_New.SecurityFormAdminCapture = DataTable_FormMode.Select("SecurityRole_Id = '169'");
          FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '165'");
          FromDataBase_SecurityRole_New.SecurityFacilityAdminHospitalManagerCompletion = DataTable_FormMode.Select("SecurityRole_Id = '166'");
          FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '167'");
          FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '168'");
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_SecurityRole_Edit
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminCapture { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
      public DataRow[] SecurityFacilityAdminHospitalManagerCompletion { get; set; }
      public DataRow[] SecurityFacilityAdminUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminView { get; set; }
    }

    private FromDataBase_SecurityRole_Edit GetSecurityRoleEdit()
    {
      FromDataBase_SecurityRole_Edit FromDataBase_SecurityRole_Edit_New = new FromDataBase_SecurityRole_Edit();

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('40')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @HQA_Finding_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@HQA_Finding_Id", Request.QueryString["HQAFindingId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          FromDataBase_SecurityRole_Edit_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
          FromDataBase_SecurityRole_Edit_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '164'");
          FromDataBase_SecurityRole_Edit_New.SecurityFormAdminCapture = DataTable_FormMode.Select("SecurityRole_Id = '169'");
          FromDataBase_SecurityRole_Edit_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '165'");
          FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminHospitalManagerCompletion = DataTable_FormMode.Select("SecurityRole_Id = '166'");
          FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '167'");
          FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '168'");
        }
      }

      return FromDataBase_SecurityRole_Edit_New;
    }

    private class FromDataBase_Tracking
    {
      public string HQAFindingTrackingList { get; set; }
    }

    private FromDataBase_Tracking GetTracking()
    {
      FromDataBase_Tracking FromDataBase_Tracking_New = new FromDataBase_Tracking();

      string SQLStringFormMode = "SELECT HQA_Finding_Tracking_List FROM Form_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @HQA_Finding_Id";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@HQA_Finding_Id", Request.QueryString["HQAFindingId"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormMode.Rows)
            {
              FromDataBase_Tracking_New.HQAFindingTrackingList = DataRow_Row["HQA_Finding_Tracking_List"].ToString();
            }
          }
        }
      }

      return FromDataBase_Tracking_New;
    }


    //--START-- --TableFormNoAccess--//
    protected void Button_NoAccessCaptured_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding Captured Findings", "Form_HeadOfficeQualityAudit_List.aspx"), false);
    }
    //---END--- --TableFormNoAccess--//


    //--START-- --TableForm--//
    //--START-- --Insert--//
    protected void FormView_HeadOfficeQualityAudit_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_HeadOfficeQualityAudit.SetFocus(UpdatePanel_HeadOfficeQualityAudit);

          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_History"].DefaultValue = "";
          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_IsActive"].DefaultValue = "true";
          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_Archived"].DefaultValue = "false";


          string FindingNo = "";
          string FinancialYear = "";
          string SQLStringFindingNo = "SELECT ISNULL(MAX(CAST(HQA_Finding_FindingNo AS INT)),0) + 1 AS NextFindingNo , CASE WHEN MONTH(@FindingDate) IN ('1','2','3','4','5','6','7','8','9') THEN CAST((YEAR(@FindingDate) + 0) AS NVARCHAR(10)) WHEN MONTH(@FindingDate) IN ('10','11','12') THEN CAST((YEAR(@FindingDate) + 1) AS NVARCHAR(10)) END AS FinancialYear FROM ( SELECT HQA_Finding_FindingNo FROM Form_HeadOfficeQualityAudit_Finding WHERE Facility_Id = @FacilityId AND HQA_Finding_Function_List = @Function AND HQA_Finding_FinancialYear = CASE WHEN MONTH(@FindingDate) IN ('1','2','3','4','5','6','7','8','9') THEN CAST((YEAR(@FindingDate) + 0) AS NVARCHAR(10)) WHEN MONTH(@FindingDate) IN ('10','11','12') THEN CAST((YEAR(@FindingDate) + 1) AS NVARCHAR(10)) END ) AS TempTableA";
          using (SqlCommand SqlCommand_FindingNo = new SqlCommand(SQLStringFindingNo))
          {
            SqlCommand_FindingNo.Parameters.AddWithValue("@FacilityId", ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertFacility")).SelectedValue);
            SqlCommand_FindingNo.Parameters.AddWithValue("@Function", ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertFunctionList")).SelectedValue);
            SqlCommand_FindingNo.Parameters.AddWithValue("@FindingDate", ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertFindingDate")).Text);
            DataTable DataTable_FindingNo;
            using (DataTable_FindingNo = new DataTable())
            {
              DataTable_FindingNo.Locale = CultureInfo.CurrentCulture;
              DataTable_FindingNo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FindingNo).Copy();
              if (DataTable_FindingNo.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_FindingNo.Rows)
                {
                  FindingNo = DataRow_Row["NextFindingNo"].ToString();
                  FinancialYear = DataRow_Row["FinancialYear"].ToString();
                }
              }
            }
          }


          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_FindingNo"].DefaultValue = FindingNo;
          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_FinancialYear"].DefaultValue = FinancialYear;
          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_Criteria_List"].DefaultValue = ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertCriteriaList")).SelectedValue;
          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_SubCriteria_List"].DefaultValue = ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).SelectedValue;

          if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertTrackingList")).Visible == false)
          {
            SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_Tracking_List"].DefaultValue = "5396";
          }

          SqlDataSource_HeadOfficeQualityAudit_Form.InsertParameters["HQA_Finding_TrackingDate"].DefaultValue = DateTime.Now.ToString();
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertFacility")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertFunctionList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertFindingDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertAuditor")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertCriteriaList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).Items.Count > 1)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertClassificationList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertDescription")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertTrackingList")).Visible == true)
        {
          if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertTrackingList")).SelectedValue == "5399")
          {
            if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertImmediateAction")).Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertRootCause")).Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertCorrectiveAction")).Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertEvaluation")).Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = InsertFieldValidation(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string InsertFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      DateTime CurrentDate = DateTime.Now;
      DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_InsertFindingDate")).Text);
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

      return InvalidFormMessage;
    }

    protected void SqlDataSource_HeadOfficeQualityAudit_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string HQA_Finding_Id = e.Command.Parameters["@HQA_Finding_Id"].Value.ToString();

        if (!string.IsNullOrEmpty(HQA_Finding_Id))
        {
          string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("70");
          string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
          string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("40");

          string HQAFindingId = "";
          string FacilityFacilityDisplayName = "";
          string HQAFindingFunctionName = "";
          string HQAFindingFindingNo = "";
          string SQLStringHQAFinding = "SELECT HQA_Finding_Id , Facility_FacilityDisplayName , HQA_Finding_Function_Name , HQA_Finding_FindingNo FROM vForm_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @HQA_Finding_Id";
          using (SqlCommand SqlCommand_HQAFinding = new SqlCommand(SQLStringHQAFinding))
          {
            SqlCommand_HQAFinding.Parameters.AddWithValue("@HQA_Finding_Id", HQA_Finding_Id);
            DataTable DataTable_HQAFinding;
            using (DataTable_HQAFinding = new DataTable())
            {
              DataTable_HQAFinding.Locale = CultureInfo.CurrentCulture;
              DataTable_HQAFinding = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HQAFinding).Copy();
              if (DataTable_HQAFinding.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_HQAFinding.Rows)
                {
                  HQAFindingId = DataRow_Row["HQA_Finding_Id"].ToString();
                  FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                  HQAFindingFunctionName = DataRow_Row["HQA_Finding_Function_Name"].ToString();
                  HQAFindingFindingNo = DataRow_Row["HQA_Finding_FindingNo"].ToString();
                }
              }
            }
          }

          string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();

          string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();

          string SecurityUserDisplayName = "";
          string SecurityUserEmail = "";
          string SQLStringEmailTo = "SELECT ISNULL(SecurityUser_DisplayName,'') AS SecurityUser_DisplayName, ISNULL(SecurityUser_Email,'') AS SecurityUser_Email FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('40') AND SecurityRole_Id IN ('166') AND Facility_Id IN (SELECT Facility_Id FROM Form_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @HQA_Finding_Id) AND SecurityUser_Email IS NOT NULL";
          using (SqlCommand SqlCommand_EmailTo = new SqlCommand(SQLStringEmailTo))
          {
            SqlCommand_EmailTo.Parameters.AddWithValue("@HQA_Finding_Id", HQA_Finding_Id);
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

                  string BodyString = EmailTemplate;

                  BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + SecurityUserDisplayName + "");
                  BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
                  BodyString = BodyString.Replace(";replace;FacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
                  BodyString = BodyString.Replace(";replace;HQAFindingId;replace;", "" + HQAFindingId + "");
                  BodyString = BodyString.Replace(";replace;HQAFindingFunctionName;replace;", "" + HQAFindingFunctionName + "");
                  BodyString = BodyString.Replace(";replace;HQAFindingFindingNo;replace;", "" + HQAFindingFindingNo + "");
                  BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");

                  string EmailBody = HeaderString + BodyString + FooterString;

                  string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", SecurityUserEmail, FormName, EmailBody);

                  if (EmailSend == "Yes")
                  {
                    EmailBody = "";
                  }
                  else
                  {
                    EmailBody = "";
                  }

                  EmailSend = "";
                  SecurityUserDisplayName = "";
                  SecurityUserEmail = "";
                }
              }
              else
              {
                SecurityUserDisplayName = "";
                SecurityUserEmail = "";
              }
            }
          }

          SecurityUserDisplayName = "";
          SecurityUserEmail = "";

          EmailTemplate = "";
          URLAuthority = "";
          FormName = "";
          HQAFindingId = "";
          FacilityFacilityDisplayName = "";
          HQAFindingFunctionName = "";
          HQAFindingFindingNo = "";
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Captured", "InfoQuest_Captured.aspx?CapturedPage=Form_HeadOfficeQualityAudit&CapturedNumber=" + HQA_Finding_Id + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    protected void FormView_HeadOfficeQualityAudit_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDHQAFindingModifiedDate"] = e.OldValues["HQA_Finding_ModifiedDate"];
        object OLDHQAFindingModifiedDate = Session["OLDHQAFindingModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDHQAFindingModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareHeadOfficeQualityAudit = (DataView)SqlDataSource_HeadOfficeQualityAudit_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareHeadOfficeQualityAudit = DataView_CompareHeadOfficeQualityAudit[0];
        Session["DBHQAFindingModifiedDate"] = Convert.ToString(DataRowView_CompareHeadOfficeQualityAudit["HQA_Finding_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBHQAFindingModifiedBy"] = Convert.ToString(DataRowView_CompareHeadOfficeQualityAudit["HQA_Finding_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBHQAFindingModifiedDate = Session["DBHQAFindingModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBHQAFindingModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_HeadOfficeQualityAudit.SetFocus(UpdatePanel_HeadOfficeQualityAudit);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBHQAFindingModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation();
          
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
            ToolkitScriptManager_HeadOfficeQualityAudit.SetFocus(UpdatePanel_HeadOfficeQualityAudit);
            ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["HQA_Finding_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["HQA_Finding_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_HeadOfficeQualityAudit_Finding", "HQA_Finding_Id = " + Request.QueryString["HQAFindingId"]);

            DataView DataView_HeadOfficeQualityAudit = (DataView)SqlDataSource_HeadOfficeQualityAudit_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_HeadOfficeQualityAudit = DataView_HeadOfficeQualityAudit[0];
            Session["HQAFindingHistory"] = Convert.ToString(DataRowView_HeadOfficeQualityAudit["HQA_Finding_History"], CultureInfo.CurrentCulture);

            Session["HQAFindingHistory"] = Session["History"].ToString() + Session["HQAFindingHistory"].ToString();
            e.NewValues["HQA_Finding_History"] = Session["HQAFindingHistory"].ToString();

            Session["HQAFindingHistory"] = "";
            Session["History"] = "";


            string FinancialYear = "";
            string SQLStringFinancialYear = "SELECT CASE WHEN MONTH(@FindingDate) IN ('1','2','3','4','5','6','7','8','9') THEN CAST((YEAR(@FindingDate) + 0) AS NVARCHAR(10)) WHEN MONTH(@FindingDate) IN ('10','11','12') THEN CAST((YEAR(@FindingDate) + 1) AS NVARCHAR(10)) END AS FinancialYear ";
            using (SqlCommand SqlCommand_FinancialYear = new SqlCommand(SQLStringFinancialYear))
            {
              SqlCommand_FinancialYear.Parameters.AddWithValue("@FindingDate", ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditFindingDate")).Text);
              DataTable DataTable_FinancialYear;
              using (DataTable_FinancialYear = new DataTable())
              {
                DataTable_FinancialYear.Locale = CultureInfo.CurrentCulture;
                DataTable_FinancialYear = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FinancialYear).Copy();
                if (DataTable_FinancialYear.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_FinancialYear.Rows)
                  {
                    FinancialYear = DataRow_Row["FinancialYear"].ToString();
                  }
                }
              }
            }

            e.NewValues["HQA_Finding_FinancialYear"] = FinancialYear;

            if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditCriteriaList")).Visible == true)
            {
              e.NewValues["HQA_Finding_Criteria_List"] = ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditCriteriaList")).SelectedValue;
            }
            else
            {
              e.NewValues["HQA_Finding_Criteria_List"] = ((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditCriteriaList")).Value;
            }

            if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).Visible == true)
            {
              e.NewValues["HQA_Finding_SubCriteria_List"] = ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).SelectedValue;
            }
            else
            {
              e.NewValues["HQA_Finding_SubCriteria_List"] = ((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditSubCriteriaList")).Value;
            }


            if (!string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).SelectedValue))
            {
              if (e.OldValues["HQA_Finding_Tracking_List"].ToString() == ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).SelectedValue)
              {
                e.NewValues["HQA_Finding_TrackingDate"] = e.OldValues["HQA_Finding_TrackingDate"];
              }
              else
              {
                e.NewValues["HQA_Finding_TrackingDate"] = DateTime.Now.ToString();
              }
            }
          }
        }

        Session["OLDHQAFindingModifiedDate"] = "";
        Session["DBHQAFindingModifiedDate"] = "";
        Session["DBHQAFindingModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditFindingDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditAuditor")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditCriteriaList")).Visible == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditCriteriaList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).Visible == true)
        {
          if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).Items.Count > 1)
          {
            if (string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditDescription")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).Visible == true)
        {
          if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).SelectedValue == "5399")
          {
            if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditImmediateAction")).Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditRootCause")).Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditCorrectiveAction")).Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditEvaluation")).Text))
            {
              InvalidForm = "Yes";
            }
          }
        }

        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).Visible == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else
          {
            if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue == "5406")
            {
              if (string.IsNullOrEmpty(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditLateCloseOutListOther")).Text))
              {
                InvalidForm = "Yes";
              }
            }
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = EditFieldValidation(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      DateTime CurrentDate = DateTime.Now;
      DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditFindingDate")).Text);
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

      return InvalidFormMessage;
    }

    protected void SqlDataSource_HeadOfficeQualityAudit_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            RedirectToList();
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_HeadOfficeQualityAudit, GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding Print", "InfoQuest_Print.aspx?PrintPage=Form_HeadOfficeQualityAudit&PrintValue=" + Request.QueryString["HQAFindingId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_HeadOfficeQualityAudit, GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_HeadOfficeQualityAudit, GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding Email", "InfoQuest_Email.aspx?EmailPage=Form_HeadOfficeQualityAudit&EmailValue=" + Request.QueryString["HQAFindingId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_HeadOfficeQualityAudit, GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_HeadOfficeQualityAudit_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["HQAFindingId"] != null)
          {
            RedirectToList();
          }
        }
      }
    }

    protected void FormView_HeadOfficeQualityAudit_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_HeadOfficeQualityAudit_Form.CurrentMode == FormViewMode.Insert)
      {
        if (string.IsNullOrEmpty(Request.QueryString["HQAFindingId"]))
        {
          InsertDataBound();
        }
      }

      if (FormView_HeadOfficeQualityAudit_Form.CurrentMode == FormViewMode.Edit)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["HQAFindingId"]))
        {
          EditDataBound();
        }
      }

      if (FormView_HeadOfficeQualityAudit_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["HQAFindingId"]))
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void InsertDataBound()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminCapture = FromDataBase_SecurityRole_Current.SecurityFormAdminCapture;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminHospitalManagerCompletion = FromDataBase_SecurityRole_Current.SecurityFacilityAdminHospitalManagerCompletion;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
      {
        Security = "0";
        FormView_HeadOfficeQualityAudit_Form.FindControl("ImmediateAction").Visible = true;
        FormView_HeadOfficeQualityAudit_Form.FindControl("RootCause").Visible = true;
        FormView_HeadOfficeQualityAudit_Form.FindControl("CorrectiveAction").Visible = true;
        FormView_HeadOfficeQualityAudit_Form.FindControl("Evaluation").Visible = true;
        ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertTrackingList")).Visible = true;
        ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_InsertTrackingList")).Visible = false;
      }

      if (Security == "1" && (SecurityFormAdminCapture.Length > 0 || SecurityFormAdminView.Length > 0 || SecurityFacilityAdminHospitalManagerCompletion.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_HeadOfficeQualityAudit_Form.FindControl("ImmediateAction").Visible = false;
        FormView_HeadOfficeQualityAudit_Form.FindControl("RootCause").Visible = false;
        FormView_HeadOfficeQualityAudit_Form.FindControl("CorrectiveAction").Visible = false;
        FormView_HeadOfficeQualityAudit_Form.FindControl("Evaluation").Visible = false;
        ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertTrackingList")).Visible = false;
        ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_InsertTrackingList")).Visible = true;
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_HeadOfficeQualityAudit_Form.FindControl("ImmediateAction").Visible = false;
        FormView_HeadOfficeQualityAudit_Form.FindControl("RootCause").Visible = false;
        FormView_HeadOfficeQualityAudit_Form.FindControl("CorrectiveAction").Visible = false;
        FormView_HeadOfficeQualityAudit_Form.FindControl("Evaluation").Visible = false;
        ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertTrackingList")).Visible = false;
        ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_InsertTrackingList")).Visible = true;
      }
    }

    protected void EditDataBound()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["HQAFindingId"]))
      {
        SqlDataSource_HeadOfficeQualityAudit_EditCriteriaList.SelectParameters["ListItem_Parent"].DefaultValue = ((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditFunctionList")).Value;

        DataView DataView_HeadOfficeQualityAudit = (DataView)SqlDataSource_HeadOfficeQualityAudit_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_HeadOfficeQualityAudit = DataView_HeadOfficeQualityAudit[0];
        ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditCriteriaList")).SelectedValue = Convert.ToString(DataRowView_HeadOfficeQualityAudit["HQA_Finding_Criteria_List"], CultureInfo.CurrentCulture);
        ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).SelectedValue = Convert.ToString(DataRowView_HeadOfficeQualityAudit["HQA_Finding_SubCriteria_List"], CultureInfo.CurrentCulture);

        EditDataBound_LateCloseout();


        string FacilityFacilityDisplayName = "";
        string HQAFindingFunctionName = "";
        string HQAFindingFindingNo = "";
        string HQAFindingCriteriaName = "";
        string HQAFindingSubCriteriaName = "";
        string HQAFindingClassificationName = "";
        string SQLStringHeadOfficeQualityAudit = "SELECT Facility_FacilityDisplayName , HQA_Finding_Function_Name , HQA_Finding_FindingNo , HQA_Finding_Criteria_Name , HQA_Finding_SubCriteria_Name , HQA_Finding_Classification_Name FROM vForm_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @HQA_Finding_Id";
        using (SqlCommand SqlCommand_HeadOfficeQualityAudit = new SqlCommand(SQLStringHeadOfficeQualityAudit))
        {
          SqlCommand_HeadOfficeQualityAudit.Parameters.AddWithValue("@HQA_Finding_Id", Request.QueryString["HQAFindingId"]);
          DataTable DataTable_HeadOfficeQualityAudit;
          using (DataTable_HeadOfficeQualityAudit = new DataTable())
          {
            DataTable_HeadOfficeQualityAudit.Locale = CultureInfo.CurrentCulture;
            DataTable_HeadOfficeQualityAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HeadOfficeQualityAudit).Copy();
            if (DataTable_HeadOfficeQualityAudit.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_HeadOfficeQualityAudit.Rows)
              {
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                HQAFindingFunctionName = DataRow_Row["HQA_Finding_Function_Name"].ToString();
                HQAFindingFindingNo = DataRow_Row["HQA_Finding_FindingNo"].ToString();
                HQAFindingCriteriaName = DataRow_Row["HQA_Finding_Criteria_Name"].ToString();
                HQAFindingSubCriteriaName = DataRow_Row["HQA_Finding_SubCriteria_Name"].ToString();
                HQAFindingClassificationName = DataRow_Row["HQA_Finding_Classification_Name"].ToString();
              }
            }
          }
        }

        ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditFacility")).Text = FacilityFacilityDisplayName;
        ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditFunctionList")).Text = HQAFindingFunctionName;
        ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditFindingNo")).Text = Convert.ToString(HQAFindingFunctionName + "." + HQAFindingFindingNo, CultureInfo.CurrentCulture);
        ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditCriteriaList")).Text = HQAFindingCriteriaName;
        ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditSubCriteriaList")).Text = HQAFindingSubCriteriaName;
        ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditClassificationList")).Text = HQAFindingClassificationName;

        FacilityFacilityDisplayName = "";
        HQAFindingFunctionName = "";
        HQAFindingFindingNo = "";
        HQAFindingCriteriaName = "";
        HQAFindingSubCriteriaName = "";
        HQAFindingClassificationName = "";


        FromDataBase_SecurityRole_Edit FromDataBase_SecurityRole_Edit_Current = GetSecurityRoleEdit();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Edit_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFacilityAdminHospitalManagerCompletion = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminHospitalManagerCompletion;
        DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminUpdate;

        string Security = "1";
        if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
        {
          Security = "0";
          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditFindingDate")).Visible = false;
          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditAuditor")).Visible = false;
          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditCriteriaList")).Visible = false;
          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditSubCriteriaList")).Visible = false;
          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditClassificationList")).Visible = false;
          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditDescription")).Visible = false;
          ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_EditIsActive")).Visible = false;
        }

        if (Security == "1" && (SecurityFacilityAdminHospitalManagerCompletion.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
        {
          Security = "0";
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditFindingDate")).Visible = false;
          ((ImageButton)FormView_HeadOfficeQualityAudit_Form.FindControl("ImageButton_EditFindingDate")).Visible = false;
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditAuditor")).Visible = false;
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditCriteriaList")).Visible = false;
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).Visible = false;
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).Visible = false;
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditDescription")).Visible = false;
          ((CheckBox)FormView_HeadOfficeQualityAudit_Form.FindControl("CheckBox_EditIsActive")).Visible = false;
        }

        if (Security == "1")
        {
          Security = "0";
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditFindingDate")).Visible = false;
          ((ImageButton)FormView_HeadOfficeQualityAudit_Form.FindControl("ImageButton_EditFindingDate")).Visible = false;
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditAuditor")).Visible = false;
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditCriteriaList")).Visible = false;
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).Visible = false;
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).Visible = false;
          ((TextBox)FormView_HeadOfficeQualityAudit_Form.FindControl("TextBox_EditDescription")).Visible = false;
          ((CheckBox)FormView_HeadOfficeQualityAudit_Form.FindControl("CheckBox_EditIsActive")).Visible = false;
        }


        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 40";
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
          ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void EditDataBound_LateCloseout()
    {
      if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).SelectedValue == "5400") //CNC
      {
        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).SelectedValue != "5399")
        {
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
          FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
        }
        else
        {
          DateTime TrackingDate = Convert.ToDateTime(Convert.ToDateTime(((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditTrackingDate")).Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditCreatedDate")).Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddDays(30);

          if (CreatedDateAdded.CompareTo(TrackingDate) >= 0)
          {
            ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
          }
          else
          {
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = true;
          }
        }
      }
      else if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).SelectedValue == "5401") //NC
      {
        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).SelectedValue != "5399")
        {
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
          FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
        }
        else
        {
          DateTime TrackingDate = Convert.ToDateTime(Convert.ToDateTime(((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditTrackingDate")).Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditCreatedDate")).Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddDays(90);

          if (CreatedDateAdded.CompareTo(TrackingDate) >= 0)
          {
            ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
          }
          else
          {
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = true;
          }
        }
      }
      else
      {
        ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
        FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
      }
    }

    protected void ReadOnlyDataBound()
    {
      string FacilityFacilityDisplayName = "";
      string HQAFindingFunctionName = "";
      string HQAFindingFindingNo = "";
      string HQAFindingCriteriaName = "";
      string HQAFindingSubCriteriaName = "";
      string HQAFindingClassificationName = "";
      string HQAFindingTrackingName = "";
      string HQAFindingLateCloseOutName = "";
      string SQLStringHeadOfficeQualityAudit = "SELECT Facility_FacilityDisplayName , HQA_Finding_Function_Name , HQA_Finding_FindingNo , HQA_Finding_Criteria_Name , HQA_Finding_SubCriteria_Name , HQA_Finding_Classification_Name , HQA_Finding_Tracking_Name , HQA_Finding_LateCloseOut_Name FROM vForm_HeadOfficeQualityAudit_Finding WHERE HQA_Finding_Id = @HQA_Finding_Id";
      using (SqlCommand SqlCommand_HeadOfficeQualityAudit = new SqlCommand(SQLStringHeadOfficeQualityAudit))
      {
        SqlCommand_HeadOfficeQualityAudit.Parameters.AddWithValue("@HQA_Finding_Id", Request.QueryString["HQAFindingId"]);
        DataTable DataTable_HeadOfficeQualityAudit;
        using (DataTable_HeadOfficeQualityAudit = new DataTable())
        {
          DataTable_HeadOfficeQualityAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_HeadOfficeQualityAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HeadOfficeQualityAudit).Copy();
          if (DataTable_HeadOfficeQualityAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HeadOfficeQualityAudit.Rows)
            {
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              HQAFindingFunctionName = DataRow_Row["HQA_Finding_Function_Name"].ToString();
              HQAFindingFindingNo = DataRow_Row["HQA_Finding_FindingNo"].ToString();
              HQAFindingCriteriaName = DataRow_Row["HQA_Finding_Criteria_Name"].ToString();
              HQAFindingSubCriteriaName = DataRow_Row["HQA_Finding_SubCriteria_Name"].ToString();
              HQAFindingClassificationName = DataRow_Row["HQA_Finding_Classification_Name"].ToString();
              HQAFindingTrackingName = DataRow_Row["HQA_Finding_Tracking_Name"].ToString();
              HQAFindingLateCloseOutName = DataRow_Row["HQA_Finding_LateCloseOut_Name"].ToString();
            }
          }
        }
      }

      ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_ItemFacility")).Text = FacilityFacilityDisplayName;
      ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_ItemFunctionList")).Text = HQAFindingFunctionName;
      ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_ItemFindingNo")).Text = Convert.ToString(HQAFindingFunctionName + "." + HQAFindingFindingNo, CultureInfo.CurrentCulture);
      ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_ItemCriteriaList")).Text = HQAFindingCriteriaName;
      ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_ItemSubCriteriaList")).Text = HQAFindingSubCriteriaName;
      ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_ItemClassificationList")).Text = HQAFindingClassificationName;
      ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_ItemTrackingList")).Text = HQAFindingTrackingName;
      ((Label)FormView_HeadOfficeQualityAudit_Form.FindControl("Label_ItemLateCloseOutList")).Text = HQAFindingLateCloseOutName;

      FacilityFacilityDisplayName = "";
      HQAFindingFunctionName = "";
      HQAFindingFindingNo = "";
      HQAFindingCriteriaName = "";
      HQAFindingSubCriteriaName = "";
      HQAFindingClassificationName = "";
      HQAFindingTrackingName = "";
      HQAFindingLateCloseOutName = "";


      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 40";
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
        ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_ItemPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_ItemPrint")).Visible = true;
        ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding Print", "InfoQuest_Print.aspx?PrintPage=Form_HeadOfficeQualityAudit&PrintValue=" + Request.QueryString["HQAFindingId"] + "") + "')";
      }

      if (Email == "False")
      {
        ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_ItemEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_ItemEmail")).Visible = true;
        ((Button)FormView_HeadOfficeQualityAudit_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding Print", "InfoQuest_Email.aspx?EmailPage=Form_HeadOfficeQualityAudit&EmailValue=" + Request.QueryString["HQAFindingId"] + "") + "')";
      }

      Email = "";
      Print = "";
    }


    //--START-- --Insert Controls--//
    protected void DropDownList_InsertFunctionList_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertCriteriaList")).Items.Clear();
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).Items.Clear();

      SqlDataSource_HeadOfficeQualityAudit_InsertCriteriaList.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectParameters["ListItem_Parent"].DefaultValue = "";

      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertCriteriaList")).Items.Insert(0, new ListItem(Convert.ToString("Select Criteria", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).Items.Insert(0, new ListItem(Convert.ToString("Select Sub Criteria", CultureInfo.CurrentCulture), ""));

      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertCriteriaList")).DataBind();
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).DataBind();
    }

    protected void DropDownList_InsertCriteriaList_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).Items.Clear();
      SqlDataSource_HeadOfficeQualityAudit_InsertSubCriteriaList.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).Items.Insert(0, new ListItem(Convert.ToString("Select Sub Criteria", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_InsertSubCriteriaList")).DataBind();
    }

    protected void Button_InsertCaptured_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_InsertClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding", "Form_HeadOfficeQualityAudit.aspx"), false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void DropDownList_EditCriteriaList_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).Items.Clear();
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).Items.Insert(0, new ListItem(Convert.ToString("Select Sub Criteria", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditSubCriteriaList")).DataBind();
    }

    protected void DropDownList_EditCriteriaList_DataBound(object sender, EventArgs e)
    {
      SqlDataSource_HeadOfficeQualityAudit_EditSubCriteriaList.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;      
    }

    protected void DropDownList_EditClassificationList_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).SelectedValue == "5400") //CNC
      {
        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).SelectedValue != "5399")
        {
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
          FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
        }
        else
        {
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditCreatedDate")).Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddDays(30);

          if (CreatedDateAdded.CompareTo(CurrentDate) >= 0)
          {
            ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
          }
          else
          {
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = true;
          }
        }
      }
      else if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).SelectedValue == "5401") //NC
      {
        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).SelectedValue != "5399")
        {
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
          FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
        }
        else
        {
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditCreatedDate")).Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddDays(90);

          if (CreatedDateAdded.CompareTo(CurrentDate) >= 0)
          {
            ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
          }
          else
          {
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = true;
          }
        }
      }
      else
      {
        ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
        FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
      }
    }
    
    protected void DropDownList_EditTrackingList_DataBound(object sender, EventArgs e)
    {
      if (FormView_HeadOfficeQualityAudit_Form.CurrentMode == FormViewMode.Edit)
      {
        FromDataBase_SecurityRole_Edit FromDataBase_SecurityRole_Edit_Current = GetSecurityRoleEdit();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Edit_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFormAdminCapture = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminCapture;
        DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminView;
        DataRow[] SecurityFacilityAdminHospitalManagerCompletion = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminHospitalManagerCompletion;
        DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminUpdate;
        DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminView;

        DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList");

        Session["Security"] = "1";
        if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFormAdminCapture.Length > 0 || SecurityFormAdminView.Length > 0 || SecurityFacilityAdminHospitalManagerCompletion.Length > 0))
        {
          Session["Security"] = "0";
        }

        if (Session["Security"].ToString() == "1" && (SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityAdminView.Length > 0))
        {
          Session["Security"] = "0";

          ListItem ListItem_EditTrackingListItem = DropDownList_EditTrackingList.Items.FindByValue("5399");
          if (ListItem_EditTrackingListItem != null)
          {
            DropDownList_EditTrackingList.Items.Remove(ListItem_EditTrackingListItem);
          }
        }

        if (Session["Security"].ToString() == "1")
        {
          Session["Security"] = "0";

          ListItem ListItem_EditTrackingListItem = DropDownList_EditTrackingList.Items.FindByValue("5399");
          if (ListItem_EditTrackingListItem != null)
          {
            DropDownList_EditTrackingList.Items.Remove(ListItem_EditTrackingListItem);
          }
        }
      }
    }

    protected void DropDownList_EditTrackingList_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).SelectedValue == "5400") //CNC
      {
        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).SelectedValue != "5399")
        {
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
          FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
        }
        else
        {
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditCreatedDate")).Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddDays(30);

          if (CreatedDateAdded.CompareTo(CurrentDate) >= 0)
          {
            ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
          }
          else
          {
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = true;
          }
        }
      }
      else if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditClassificationList")).SelectedValue == "5401") //NC
      {
        if (((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditTrackingList")).SelectedValue != "5399")
        {
          ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
          FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
        }
        else
        {
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(((HiddenField)FormView_HeadOfficeQualityAudit_Form.FindControl("HiddenField_EditCreatedDate")).Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddDays(90);

          if (CreatedDateAdded.CompareTo(CurrentDate) >= 0)
          {
            ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
          }
          else
          {
            FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = true;
          }
        }
      }
      else
      {
        ((DropDownList)FormView_HeadOfficeQualityAudit_Form.FindControl("DropDownList_EditLateCloseOutList")).SelectedValue = "";
        FormView_HeadOfficeQualityAudit_Form.FindControl("LateCloseOutList").Visible = false;
      }
    }

    protected void Button_EditPrint_Click(object sender, EventArgs e)
    {
      Button_EditPrintClicked = true;
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      Button_EditEmailClicked = true;
    }

    protected void Button_EditCaptured_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding", "Form_HeadOfficeQualityAudit.aspx"), false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    protected void Button_ItemCaptured_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding", "Form_HeadOfficeQualityAudit.aspx"), false);
    }
    //---END--- --Item Controls--//
    //---END--- --TableForm--//
  }
}