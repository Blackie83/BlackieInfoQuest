using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Drawing;

namespace InfoQuestForm
{
  public partial class Form_CRM : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected bool Button_EditUpdateClicked = false;
    protected bool Button_EditPrintClicked = false;
    protected bool Button_EditEmailClicked = false;

    protected Dictionary<string, string> FileContentTypeHandler = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_CRM, this.GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();", true);

        HiddenField HiddenField_InsertCRMIdTemp;
        using (HiddenField_InsertCRMIdTemp = new HiddenField())
        {
          if (string.IsNullOrEmpty(Request.QueryString["CRM_Id"]))
          {
            HiddenField_InsertCRMIdTemp = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertCRMIdTemp");
            if (string.IsNullOrEmpty(HiddenField_InsertCRMIdTemp.Value))
            {
              HiddenField_InsertCRMIdTemp.Value = "TEMP_ID:USER:" + Request.ServerVariables["LOGON_USER"].ToUpper(CultureInfo.CurrentCulture) + ":DATE:" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss", CultureInfo.CurrentCulture).ToUpper(CultureInfo.CurrentCulture) + "";
            }
          }
        }

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_CRM_File_InsertFile.SelectParameters["CRM_File_Temp_CRM_Id"].DefaultValue = HiddenField_InsertCRMIdTemp.Value;

          if (!string.IsNullOrEmpty(Request.QueryString["CRM_Id"]))
          {
            SqlDataSource_CRM_EditOriginatedAtList.SelectParameters["TableSELECT"].DefaultValue = "CRM_OriginatedAt_List";
            SqlDataSource_CRM_EditOriginatedAtList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditOriginatedAtList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditTypeList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Type_List";
            SqlDataSource_CRM_EditTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditTypeList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditReceivedViaList.SelectParameters["TableSELECT"].DefaultValue = "CRM_ReceivedVia_List";
            SqlDataSource_CRM_EditReceivedViaList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditReceivedViaList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditReceivedViaList.SelectParameters["TableSELECT"].DefaultValue = "CRM_ReceivedFrom_List";
            SqlDataSource_CRM_EditReceivedViaList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditReceivedViaList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Complaint_Priority_List";
            SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Complaint_Category_Item_List";
            SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM_Complaint_Category";
            SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Complaint_Within24HoursMethod_List";
            SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditCommentTypeList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_Type_List";
            SqlDataSource_CRM_EditCommentTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentTypeList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_AdditionalType_List";
            SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditCommentCategoryList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_Category_List";
            SqlDataSource_CRM_EditCommentCategoryList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentCategoryList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_AdditionalCategory_List";
            SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditComplaintUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_CRM_EditComplaintUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Complaint_Unit_Id";
            SqlDataSource_CRM_EditComplaintUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditComplaintUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditComplimentUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_CRM_EditComplimentUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Compliment_Unit_Id";
            SqlDataSource_CRM_EditComplimentUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditComplimentUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditCommentUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_CRM_EditCommentUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_Unit_Id";
            SqlDataSource_CRM_EditCommentUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_AdditionalUnit_Id";
            SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditQueryUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_CRM_EditQueryUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Query_Unit_Id";
            SqlDataSource_CRM_EditQueryUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditQueryUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

            SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Suggestion_Unit_Id";
            SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";
          }

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("36")).ToString();
          Label_FormHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("36")).ToString();

          TableForm.Visible = true;

          SetFormVisibility();

          TableFormVisible();

          FileCleanUp();
          ComplaintCategoryCleanUp();
        }
      }
    }

    protected string PageSecurity()
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('36'))";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

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

      try
      {
        InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
      }
      catch (Exception Exception_ErrorNext)
      {
        if (!string.IsNullOrEmpty(Exception_ErrorNext.ToString()))
        {
          InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, "Page Title Not Available", Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
        }
        else
        {
          throw;
        }
      }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      InfoQuestWCF.InfoQuest_All.All_Maintenance("36");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_CRM.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Customer Relationship Management", "4");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSourceSetup_Insert();
      SqlDataSourceSetup_Edit();
      SqlDataSourceSetup_Item();

      SqlDataSource_CRM_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_Form.InsertCommand = "INSERT INTO [Form_CRM] (Facility_Id ,	CRM_ReportNumber ,CRM_DateReceived ,CRM_DateForwarded ,CRM_OriginatedAt_List ,CRM_Type_List ,CRM_ReceivedVia_List ,CRM_ReceivedFrom_List ,CRM_EscalatedForm ,CRM_EscalatedReportNumber ,CRM_CustomerName ,CRM_CustomerEmail ,CRM_CustomerContactNumber ,CRM_PatientVisitNumber ,CRM_PatientName ,CRM_PatientDateOfAdmission ,CRM_PatientEmail ,CRM_PatientContactNumber ,CRM_Complaint_Description ,CRM_Complaint_ContactPatient ,CRM_Complaint_Unit_Id ,CRM_Complaint_DateOccurred ,CRM_Complaint_TimeOccuredHours ,CRM_Complaint_TimeOccuredMinutes ,CRM_Complaint_Priority_List ,CRM_Complaint_Within24Hours ,CRM_Complaint_Within24HoursReason , CRM_Complaint_Within24HoursMethod_List ,CRM_Complaint_Within5Days ,CRM_Complaint_Within5DaysReason ,CRM_Complaint_Within10Days ,CRM_Complaint_Within10DaysReason ,CRM_Complaint_Within3Days ,CRM_Complaint_Within3DaysReason ,CRM_Complaint_CustomerSatisfied ,CRM_Complaint_CustomerSatisfiedReason ,CRM_Complaint_InvestigatorName ,CRM_Complaint_InvestigatorDesignation ,CRM_Complaint_RootCause , CRM_Complaint_Action ,CRM_Complaint_CloseOut ,CRM_Complaint_CloseOutDate ,CRM_Complaint_CloseOutBy ,CRM_Compliment_Description ,CRM_Compliment_ContactPatient ,CRM_Compliment_Unit_Id ,CRM_Compliment_Acknowledge ,CRM_Compliment_AcknowledgeDate ,CRM_Compliment_AcknowledgeBy ,CRM_Compliment_CloseOut ,CRM_Compliment_CloseOutDate ,CRM_Compliment_CloseOutBy ,CRM_Comment_Description ,CRM_Comment_ContactPatient ,CRM_Comment_Unit_Id ,CRM_Comment_Type_List , CRM_Comment_Category_List , CRM_Comment_AdditionalUnit_Id , CRM_Comment_AdditionalType_List , CRM_Comment_AdditionalCategory_List ,CRM_Comment_Acknowledge ,CRM_Comment_AcknowledgeDate ,CRM_Comment_AcknowledgeBy ,CRM_Comment_CloseOut ,CRM_Comment_CloseOutDate ,CRM_Comment_CloseOutBy ,CRM_Query_Description ,CRM_Query_ContactPatient ,CRM_Query_Unit_Id ,CRM_Query_Acknowledge ,CRM_Query_AcknowledgeDate ,CRM_Query_AcknowledgeBy ,CRM_Query_CloseOut ,CRM_Query_CloseOutDate ,CRM_Query_CloseOutBy ,CRM_Suggestion_Description ,CRM_Suggestion_ContactPatient ,CRM_Suggestion_Unit_Id ,CRM_Suggestion_Acknowledge ,CRM_Suggestion_AcknowledgeDate ,CRM_Suggestion_AcknowledgeBy ,CRM_Suggestion_CloseOut ,CRM_Suggestion_CloseOutDate ,CRM_Suggestion_CloseOutBy ,CRM_Status ,CRM_StatusDate ,CRM_CreatedDate,	CRM_CreatedBy ,CRM_ModifiedDate ,CRM_ModifiedBy ,CRM_History , CRM_Archived ) VALUES ( @Facility_Id ,@CRM_ReportNumber ,@CRM_DateReceived ,@CRM_DateForwarded ,@CRM_OriginatedAt_List ,@CRM_Type_List ,@CRM_ReceivedVia_List ,@CRM_ReceivedFrom_List ,@CRM_EscalatedForm ,@CRM_EscalatedReportNumber ,@CRM_CustomerName ,@CRM_CustomerEmail ,@CRM_CustomerContactNumber ,@CRM_PatientVisitNumber ,@CRM_PatientName ,@CRM_PatientDateOfAdmission ,@CRM_PatientEmail ,@CRM_PatientContactNumber ,@CRM_Complaint_Description ,@CRM_Complaint_ContactPatient ,@CRM_Complaint_Unit_Id ,@CRM_Complaint_DateOccurred ,@CRM_Complaint_TimeOccuredHours ,@CRM_Complaint_TimeOccuredMinutes ,@CRM_Complaint_Priority_List ,@CRM_Complaint_Within24Hours ,@CRM_Complaint_Within24HoursReason , @CRM_Complaint_Within24HoursMethod_List ,@CRM_Complaint_Within5Days ,@CRM_Complaint_Within5DaysReason ,@CRM_Complaint_Within10Days ,@CRM_Complaint_Within10DaysReason ,@CRM_Complaint_Within3Days ,@CRM_Complaint_Within3DaysReason ,@CRM_Complaint_CustomerSatisfied ,@CRM_Complaint_CustomerSatisfiedReason ,@CRM_Complaint_InvestigatorName ,@CRM_Complaint_InvestigatorDesignation ,@CRM_Complaint_RootCause , @CRM_Complaint_Action , @CRM_Complaint_CloseOut ,@CRM_Complaint_CloseOutDate ,@CRM_Complaint_CloseOutBy ,@CRM_Compliment_Description ,@CRM_Compliment_ContactPatient ,@CRM_Compliment_Unit_Id ,@CRM_Compliment_Acknowledge ,@CRM_Compliment_AcknowledgeDate ,@CRM_Compliment_AcknowledgeBy ,@CRM_Compliment_CloseOut ,@CRM_Compliment_CloseOutDate ,@CRM_Compliment_CloseOutBy ,@CRM_Comment_Description ,@CRM_Comment_ContactPatient ,@CRM_Comment_Unit_Id ,@CRM_Comment_Type_List , @CRM_Comment_Category_List , @CRM_Comment_AdditionalUnit_Id , @CRM_Comment_AdditionalType_List , @CRM_Comment_AdditionalCategory_List ,@CRM_Comment_Acknowledge ,@CRM_Comment_AcknowledgeDate ,@CRM_Comment_AcknowledgeBy ,@CRM_Comment_CloseOut ,@CRM_Comment_CloseOutDate ,@CRM_Comment_CloseOutBy ,@CRM_Query_Description ,@CRM_Query_ContactPatient ,@CRM_Query_Unit_Id ,@CRM_Query_Acknowledge ,@CRM_Query_AcknowledgeDate ,@CRM_Query_AcknowledgeBy ,@CRM_Query_CloseOut ,@CRM_Query_CloseOutDate ,@CRM_Query_CloseOutBy ,@CRM_Suggestion_Description ,@CRM_Suggestion_ContactPatient ,@CRM_Suggestion_Unit_Id ,@CRM_Suggestion_Acknowledge ,@CRM_Suggestion_AcknowledgeDate ,@CRM_Suggestion_AcknowledgeBy ,@CRM_Suggestion_CloseOut ,@CRM_Suggestion_CloseOutDate ,@CRM_Suggestion_CloseOutBy ,@CRM_Status ,@CRM_StatusDate ,@CRM_CreatedDate ,@CRM_CreatedBy ,@CRM_ModifiedDate ,@CRM_ModifiedBy ,@CRM_History ,@CRM_Archived ); SELECT @CRM_Id = SCOPE_IDENTITY()";
      SqlDataSource_CRM_Form.SelectCommand = "SELECT * FROM [Form_CRM] WHERE ([CRM_Id] = @CRM_Id)";
      SqlDataSource_CRM_Form.UpdateCommand = "UPDATE [Form_CRM] SET CRM_DateReceived = @CRM_DateReceived, CRM_OriginatedAt_List = @CRM_OriginatedAt_List, CRM_Type_List = @CRM_Type_List, CRM_ReceivedVia_List = @CRM_ReceivedVia_List, CRM_ReceivedFrom_List = @CRM_ReceivedFrom_List, CRM_EscalatedForm = @CRM_EscalatedForm, CRM_EscalatedReportNumber = @CRM_EscalatedReportNumber, CRM_CustomerName = @CRM_CustomerName, CRM_CustomerEmail = @CRM_CustomerEmail, CRM_CustomerContactNumber = @CRM_CustomerContactNumber, CRM_PatientVisitNumber = @CRM_PatientVisitNumber, CRM_PatientName = @CRM_PatientName, CRM_PatientDateOfAdmission = @CRM_PatientDateOfAdmission, CRM_PatientEmail = @CRM_PatientEmail, CRM_PatientContactNumber = @CRM_PatientContactNumber, CRM_Complaint_Description = @CRM_Complaint_Description, CRM_Complaint_ContactPatient = @CRM_Complaint_ContactPatient , CRM_Complaint_Unit_Id = @CRM_Complaint_Unit_Id, CRM_Complaint_DateOccurred = @CRM_Complaint_DateOccurred, CRM_Complaint_TimeOccuredHours = @CRM_Complaint_TimeOccuredHours, CRM_Complaint_TimeOccuredMinutes = @CRM_Complaint_TimeOccuredMinutes, CRM_Complaint_Priority_List = @CRM_Complaint_Priority_List, CRM_Complaint_Within24Hours = @CRM_Complaint_Within24Hours, CRM_Complaint_Within24HoursReason = @CRM_Complaint_Within24HoursReason , CRM_Complaint_Within24HoursMethod_List = @CRM_Complaint_Within24HoursMethod_List , CRM_Complaint_Within5Days = @CRM_Complaint_Within5Days, CRM_Complaint_Within5DaysReason = @CRM_Complaint_Within5DaysReason, CRM_Complaint_Within10Days = @CRM_Complaint_Within10Days, CRM_Complaint_Within10DaysReason = @CRM_Complaint_Within10DaysReason, CRM_Complaint_Within3Days = @CRM_Complaint_Within3Days, CRM_Complaint_Within3DaysReason = @CRM_Complaint_Within3DaysReason, CRM_Complaint_CustomerSatisfied = @CRM_Complaint_CustomerSatisfied, CRM_Complaint_CustomerSatisfiedReason = @CRM_Complaint_CustomerSatisfiedReason, CRM_Complaint_InvestigatorName = @CRM_Complaint_InvestigatorName, CRM_Complaint_InvestigatorDesignation = @CRM_Complaint_InvestigatorDesignation, CRM_Complaint_RootCause = @CRM_Complaint_RootCause , CRM_Complaint_Action = @CRM_Complaint_Action , CRM_Complaint_CloseOut = @CRM_Complaint_CloseOut, CRM_Complaint_CloseOutDate = @CRM_Complaint_CloseOutDate, CRM_Complaint_CloseOutBy = @CRM_Complaint_CloseOutBy, CRM_Compliment_Description = @CRM_Compliment_Description, CRM_Compliment_ContactPatient = @CRM_Compliment_ContactPatient, CRM_Compliment_Unit_Id = @CRM_Compliment_Unit_Id, CRM_Compliment_Acknowledge = @CRM_Compliment_Acknowledge, CRM_Compliment_AcknowledgeDate = @CRM_Compliment_AcknowledgeDate, CRM_Compliment_AcknowledgeBy = @CRM_Compliment_AcknowledgeBy, CRM_Compliment_CloseOut = @CRM_Compliment_CloseOut, CRM_Compliment_CloseOutDate = @CRM_Compliment_CloseOutDate, CRM_Compliment_CloseOutBy = @CRM_Compliment_CloseOutBy, CRM_Comment_Description = @CRM_Comment_Description, CRM_Comment_ContactPatient = @CRM_Comment_ContactPatient, CRM_Comment_Unit_Id = @CRM_Comment_Unit_Id, CRM_Comment_Type_List = @CRM_Comment_Type_List , CRM_Comment_Category_List = @CRM_Comment_Category_List , CRM_Comment_AdditionalUnit_Id = @CRM_Comment_AdditionalUnit_Id , CRM_Comment_AdditionalType_List = @CRM_Comment_AdditionalType_List , CRM_Comment_AdditionalCategory_List = @CRM_Comment_AdditionalCategory_List , CRM_Comment_Acknowledge = @CRM_Comment_Acknowledge, CRM_Comment_AcknowledgeDate = @CRM_Comment_AcknowledgeDate, CRM_Comment_AcknowledgeBy = @CRM_Comment_AcknowledgeBy, CRM_Comment_CloseOut = @CRM_Comment_CloseOut, CRM_Comment_CloseOutDate = @CRM_Comment_CloseOutDate, CRM_Comment_CloseOutBy = @CRM_Comment_CloseOutBy, CRM_Query_Description = @CRM_Query_Description, CRM_Query_ContactPatient = @CRM_Query_ContactPatient, CRM_Query_Unit_Id = @CRM_Query_Unit_Id, CRM_Query_Acknowledge = @CRM_Query_Acknowledge, CRM_Query_AcknowledgeDate = @CRM_Query_AcknowledgeDate, CRM_Query_AcknowledgeBy = @CRM_Query_AcknowledgeBy, CRM_Query_CloseOut = @CRM_Query_CloseOut, CRM_Query_CloseOutDate = @CRM_Query_CloseOutDate, CRM_Query_CloseOutBy = @CRM_Query_CloseOutBy, CRM_Suggestion_Description = @CRM_Suggestion_Description, CRM_Suggestion_ContactPatient = @CRM_Suggestion_ContactPatient, CRM_Suggestion_Unit_Id = @CRM_Suggestion_Unit_Id, CRM_Suggestion_Acknowledge = @CRM_Suggestion_Acknowledge, CRM_Suggestion_AcknowledgeDate = @CRM_Suggestion_AcknowledgeDate, CRM_Suggestion_AcknowledgeBy = @CRM_Suggestion_AcknowledgeBy, CRM_Suggestion_CloseOut = @CRM_Suggestion_CloseOut, CRM_Suggestion_CloseOutDate = @CRM_Suggestion_CloseOutDate, CRM_Suggestion_CloseOutBy = @CRM_Suggestion_CloseOutBy, CRM_Status = @CRM_Status , CRM_StatusDate = @CRM_StatusDate ,CRM_StatusRejectedReason = @CRM_StatusRejectedReason ,CRM_ModifiedDate = @CRM_ModifiedDate ,CRM_ModifiedBy = @CRM_ModifiedBy ,CRM_History = @CRM_History WHERE [CRM_Id] = @CRM_Id";
      SqlDataSource_CRM_Form.InsertParameters.Clear();
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters["CRM_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_CRM_Form.InsertParameters.Add("Facility_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_ReportNumber", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_DateReceived", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_DateForwarded", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_OriginatedAt_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Type_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_ReceivedVia_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_ReceivedFrom_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_EscalatedForm", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_EscalatedReportNumber", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_CustomerName", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_CustomerEmail", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_CustomerContactNumber", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_PatientVisitNumber", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_PatientName", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_PatientDateOfAdmission", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_PatientEmail", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_PatientContactNumber", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_DateOccurred", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_TimeOccuredHours", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_TimeOccuredMinutes", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Priority_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Within24Hours", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Within24HoursReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Within24HoursMethod_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Within5Days", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Within5DaysReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Within10Days", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Within10DaysReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Within3Days", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Within3DaysReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_CustomerSatisfied", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_CustomerSatisfiedReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_InvestigatorName", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_InvestigatorDesignation", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_RootCause", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_Action", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Complaint_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Compliment_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Compliment_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Compliment_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Compliment_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Compliment_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Compliment_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Compliment_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Compliment_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Compliment_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_Type_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_Category_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_AdditionalUnit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_AdditionalType_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_AdditionalCategory_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Comment_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Query_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Query_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Query_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Query_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Query_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Query_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Query_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Query_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Query_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Suggestion_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Suggestion_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Suggestion_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Suggestion_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Suggestion_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Suggestion_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Suggestion_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Suggestion_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Suggestion_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Status", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_CreatedBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_ModifiedBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_History", TypeCode.String, "");
      SqlDataSource_CRM_Form.InsertParameters["CRM_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_CRM_Form.InsertParameters.Add("CRM_Archived", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.SelectParameters.Clear();
      SqlDataSource_CRM_Form.SelectParameters.Add("CRM_Id", TypeCode.Int32, Request.QueryString["CRM_Id"]);
      SqlDataSource_CRM_Form.UpdateParameters.Clear();
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_DateReceived", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_OriginatedAt_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Type_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_ReceivedVia_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_ReceivedFrom_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_EscalatedForm", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_EscalatedReportNumber", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_CustomerName", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_CustomerEmail", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_CustomerContactNumber", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_PatientVisitNumber", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_PatientName", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_PatientDateOfAdmission", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_PatientEmail", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_PatientContactNumber", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_DateOccurred", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_TimeOccuredHours", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_TimeOccuredMinutes", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Priority_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Within24Hours", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Within24HoursReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Within24HoursMethod_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Within5Days", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Within5DaysReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Within10Days", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Within10DaysReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Within3Days", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Within3DaysReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_CustomerSatisfied", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_CustomerSatisfiedReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_InvestigatorName", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_InvestigatorDesignation", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_RootCause", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_Action", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Complaint_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Compliment_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Compliment_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Compliment_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Compliment_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Compliment_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Compliment_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Compliment_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Compliment_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Compliment_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_Type_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_Category_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_AdditionalUnit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_AdditionalType_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_AdditionalCategory_List", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Comment_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Query_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Query_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Query_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Query_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Query_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Query_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Query_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Query_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Query_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Suggestion_Description", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Suggestion_ContactPatient", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Suggestion_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Suggestion_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Suggestion_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Suggestion_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Suggestion_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Suggestion_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Suggestion_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Status", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_StatusRejectedReason", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_ModifiedBy", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_History", TypeCode.String, "");
      SqlDataSource_CRM_Form.UpdateParameters.Add("CRM_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Insert()
    {
      SqlDataSource_CRM_InsertFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertFacility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_CRM_InsertFacility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertFacility.SelectParameters.Clear();
      SqlDataSource_CRM_InsertFacility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_InsertFacility.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertFacility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_CRM_InsertFacility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertFacility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertFacility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertOriginatedAtList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertOriginatedAtList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertOriginatedAtList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertOriginatedAtList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertOriginatedAtList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertOriginatedAtList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "115");
      SqlDataSource_CRM_InsertOriginatedAtList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertOriginatedAtList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertOriginatedAtList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertOriginatedAtList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertTypeList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "110");
      SqlDataSource_CRM_InsertTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertReceivedViaList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertReceivedViaList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertReceivedViaList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertReceivedViaList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertReceivedViaList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertReceivedViaList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "111");
      SqlDataSource_CRM_InsertReceivedViaList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertReceivedViaList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertReceivedViaList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertReceivedViaList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertReceivedFromList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertReceivedFromList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertReceivedFromList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertReceivedFromList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertReceivedFromList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertReceivedFromList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "112");
      SqlDataSource_CRM_InsertReceivedFromList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertReceivedFromList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertReceivedFromList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertReceivedFromList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertComplaintUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertComplaintUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_InsertComplaintUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertComplaintUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_InsertComplaintUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_InsertComplaintUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertComplaintUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_InsertComplaintUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplaintUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplaintUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertComplaintPriorityList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertComplaintPriorityList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertComplaintPriorityList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertComplaintPriorityList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertComplaintPriorityList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertComplaintPriorityList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "113");
      SqlDataSource_CRM_InsertComplaintPriorityList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertComplaintPriorityList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplaintPriorityList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplaintPriorityList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertComplaintCategoryItemList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertComplaintCategoryItemList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertComplaintCategoryItemList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertComplaintCategoryItemList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertComplaintCategoryItemList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertComplaintCategoryItemList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "114");
      SqlDataSource_CRM_InsertComplaintCategoryItemList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertComplaintCategoryItemList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplaintCategoryItemList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplaintCategoryItemList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "145");
      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplaintWithin24HoursMethodList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertComplimentUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertComplimentUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_InsertComplimentUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertComplimentUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_InsertComplimentUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_InsertComplimentUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertComplimentUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_InsertComplimentUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplimentUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertComplimentUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertCommentUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertCommentUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_InsertCommentUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertCommentUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_InsertCommentUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_InsertCommentUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertCommentUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_InsertCommentUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertCommentAdditionalUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertCommentTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertCommentTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertCommentTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertCommentTypeList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertCommentTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertCommentTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "117");
      SqlDataSource_CRM_InsertCommentTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertCommentTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertCommentAdditionalTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertCommentAdditionalTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertCommentAdditionalTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertCommentAdditionalTypeList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertCommentAdditionalTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertCommentAdditionalTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "117");
      SqlDataSource_CRM_InsertCommentAdditionalTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertCommentAdditionalTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentAdditionalTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentAdditionalTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertCommentCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertCommentCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertCommentCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertCommentCategoryList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertCommentCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertCommentCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "124");
      SqlDataSource_CRM_InsertCommentCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertCommentCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.SelectParameters.Clear();
      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "124");
      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertCommentAdditionalCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertQueryUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertQueryUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_InsertQueryUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertQueryUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_InsertQueryUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_InsertQueryUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertQueryUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_InsertQueryUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertQueryUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertQueryUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_InsertSuggestionUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_File_InsertFile.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_File_InsertFile.SelectCommand = "SELECT CRM_File_Id , CRM_File_Name , CRM_File_CreatedDate , CRM_File_CreatedBy FROM Form_CRM_File WHERE CRM_File_CreatedBy = @CRM_File_CreatedBy AND CRM_File_Temp_CRM_Id = @CRM_File_Temp_CRM_Id ORDER BY CRM_File_Name";
      SqlDataSource_CRM_File_InsertFile.SelectParameters.Clear();
      SqlDataSource_CRM_File_InsertFile.SelectParameters.Add("CRM_File_CreatedBy", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_File_InsertFile.SelectParameters.Add("CRM_File_Temp_CRM_Id", TypeCode.String, "");
    }

    private void SqlDataSourceSetup_Edit()
    {
      SqlDataSource_CRM_EditOriginatedAtList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditOriginatedAtList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditOriginatedAtList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditOriginatedAtList.SelectParameters.Clear();
      SqlDataSource_CRM_EditOriginatedAtList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditOriginatedAtList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "115");
      SqlDataSource_CRM_EditOriginatedAtList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditOriginatedAtList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditOriginatedAtList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditOriginatedAtList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditTypeList.SelectParameters.Clear();
      SqlDataSource_CRM_EditTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "110");
      SqlDataSource_CRM_EditTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditReceivedViaList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditReceivedViaList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditReceivedViaList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditReceivedViaList.SelectParameters.Clear();
      SqlDataSource_CRM_EditReceivedViaList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditReceivedViaList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "111");
      SqlDataSource_CRM_EditReceivedViaList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditReceivedViaList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditReceivedViaList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditReceivedViaList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditReceivedFromList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditReceivedFromList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditReceivedFromList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditReceivedFromList.SelectParameters.Clear();
      SqlDataSource_CRM_EditReceivedFromList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditReceivedFromList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "112");
      SqlDataSource_CRM_EditReceivedFromList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditReceivedFromList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditReceivedFromList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditReceivedFromList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditComplaintUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditComplaintUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_EditComplaintUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditComplaintUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_EditComplaintUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_EditComplaintUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditComplaintUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_EditComplaintUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplaintUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplaintUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditComplaintPriorityList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditComplaintPriorityList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditComplaintPriorityList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters.Clear();
      SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "113");
      SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplaintPriorityList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditComplaintCategoryItemList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditComplaintCategoryItemList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditComplaintCategoryItemList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters.Clear();
      SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "114");
      SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplaintCategoryItemList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters.Clear();
      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "145");
      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplaintWithin24HoursMethodList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditComplimentUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditComplimentUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_EditComplimentUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditComplimentUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_EditComplimentUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_EditComplimentUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditComplimentUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_EditComplimentUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplimentUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditComplimentUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditCommentUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditCommentUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_EditCommentUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditCommentUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_EditCommentUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_EditCommentUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditCommentUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_EditCommentUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditCommentAdditionalUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditCommentTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditCommentTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditCommentTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Clear();
      SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "117");
      SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditCommentAdditionalTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Clear();
      SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "117");
      SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditCommentCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditCommentCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditCommentCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Clear();
      SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "124");
      SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditCommentAdditionalCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Clear();
      SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "124");
      SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditQueryUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditQueryUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_EditQueryUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditQueryUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_EditQueryUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_EditQueryUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditQueryUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_EditQueryUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditQueryUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditQueryUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditSuggestionUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditSuggestionUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_EditSuggestionUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_EditRouteFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditRouteFacility.SelectCommand = "SELECT Facility_Id , Facility_FacilityDisplayName FROM ( SELECT TempTableA.* , ( SELECT CASE vForm_CRM.Facility_Id WHEN '120' THEN 'Yes' ELSE 'No' END FROM vForm_CRM WHERE CRM_Id = @CRM_Id ) AS FacilityShowAll FROM (	 SELECT Facility_Id , Facility_FacilityDisplayName FROM vAdministration_Facility_Form_Active WHERE Form_Id = 36 ) AS TempTableA WHERE Facility_Id NOT IN ( SELECT Facility_Id FROM Form_CRM WHERE CRM_Id = @CRM_Id AND Facility_Id NOT IN (120) ) ) AS TempTableB WHERE Facility_Id = ( CASE FacilityShowAll WHEN 'No' THEN '120' ELSE Facility_Id END ) ORDER BY TempTableB.Facility_FacilityDisplayName";
      SqlDataSource_CRM_EditRouteFacility.SelectParameters.Clear();
      SqlDataSource_CRM_EditRouteFacility.SelectParameters.Add("CRM_Id", TypeCode.String, Request.QueryString["CRM_Id"]);

      SqlDataSource_CRM_EditRouteUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditRouteUnit.SelectCommand = "SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 164 AND ListItem_ParentName = @ListItem_ParentName ORDER BY ListItem_Name";
      SqlDataSource_CRM_EditRouteUnit.SelectParameters.Clear();
      SqlDataSource_CRM_EditRouteUnit.SelectParameters.Add("CRM_Id", TypeCode.String, Request.QueryString["CRM_Id"]);
      SqlDataSource_CRM_EditRouteUnit.SelectParameters.Add("ListItem_ParentName", TypeCode.String, "");

      SqlDataSource_CRM_EditRouteList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditRouteList.SelectCommand = "SELECT Facility_FacilityDisplayName , CRM_Route_ToUnit_Name , CRM_Route_Comment , CASE CRM_Route_Complete WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END AS CRM_Route_Complete , CRM_Route_CompleteDate , CRM_Route_CreatedDate FROM vForm_CRM_Route WHERE CRM_Id = @CRM_Id ORDER BY CRM_Route_CreatedDate DESC";
      SqlDataSource_CRM_EditRouteList.SelectParameters.Clear();
      SqlDataSource_CRM_EditRouteList.SelectParameters.Add("CRM_Id", TypeCode.String, Request.QueryString["CRM_Id"]);

      SqlDataSource_CRM_File_EditFile.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_File_EditFile.SelectCommand = "SELECT CRM_File_Id , CRM_File_Name , CRM_File_CreatedDate , CRM_File_CreatedBy FROM Form_CRM_File WHERE CRM_Id = @CRM_Id ORDER BY CRM_File_Name";
      SqlDataSource_CRM_File_EditFile.SelectParameters.Clear();
      SqlDataSource_CRM_File_EditFile.SelectParameters.Add("CRM_Id", TypeCode.String, Request.QueryString["CRM_Id"]);
    }

    private void SqlDataSourceSetup_Item()
    {
      SqlDataSource_CRM_ItemComplaintCategory.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_ItemComplaintCategory.SelectCommand = "SELECT DISTINCT CRM_Complaint_Category_Item_Name FROM vForm_CRM_Complaint_Category WHERE CRM_Id = @CRM_Id ORDER BY CRM_Complaint_Category_Item_Name";
      SqlDataSource_CRM_ItemComplaintCategory.SelectParameters.Clear();
      SqlDataSource_CRM_ItemComplaintCategory.SelectParameters.Add("CRM_Id", TypeCode.String, Request.QueryString["CRM_Id"]);

      SqlDataSource_CRM_ItemRouteList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_ItemRouteList.SelectCommand = "SELECT Facility_FacilityDisplayName , CRM_Route_ToUnit_Name , CRM_Route_Comment , CASE CRM_Route_Complete WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END AS CRM_Route_Complete , CRM_Route_CompleteDate , CRM_Route_CreatedDate FROM vForm_CRM_Route WHERE CRM_Id = @CRM_Id ORDER BY CRM_Route_CreatedDate  DESC";
      SqlDataSource_CRM_ItemRouteList.SelectParameters.Clear();
      SqlDataSource_CRM_ItemRouteList.SelectParameters.Add("CRM_Id", TypeCode.String, Request.QueryString["CRM_Id"]);

      SqlDataSource_CRM_File_ItemFile.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_File_ItemFile.SelectCommand = "SELECT CRM_File_Id , CRM_File_Name , CRM_File_CreatedDate , CRM_File_CreatedBy FROM Form_CRM_File WHERE CRM_Id = @CRM_Id ORDER BY CRM_File_Name";
      SqlDataSource_CRM_File_ItemFile.SelectParameters.Clear();
      SqlDataSource_CRM_File_ItemFile.SelectParameters.Add("CRM_Id", TypeCode.String, Request.QueryString["CRM_Id"]);
    }

    protected void SetFormVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["CRM_Id"]))
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
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminHospitalManagerUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminHospitalManagerUpdate;
      DataRow[] SecurityFacilityAdminNSMUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminNSMUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;
      DataRow[] SecurityFacilityInvestigator = FromDataBase_SecurityRole_Current.SecurityFacilityInvestigator;
      DataRow[] SecurityFacilityApprover = FromDataBase_SecurityRole_Current.SecurityFacilityApprover;
      DataRow[] SecurityFacilityCapturer = FromDataBase_SecurityRole_Current.SecurityFacilityCapturer;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminHospitalManagerUpdate.Length > 0 || SecurityFacilityAdminNSMUpdate.Length > 0 || SecurityFacilityInvestigator.Length > 0 || SecurityFacilityApprover.Length > 0 || SecurityFacilityCapturer.Length > 0))
      {
        Security = "0";
        FormView_CRM_Form.ChangeMode(FormViewMode.Insert);
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_CRM_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_CRM_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetFormVisibility_Edit()
    {
      FromDataBase_IsRouteComplete FromDataBase_IsRouteComplete_Current = GetIsRouteComplete();
      string IsRouteComplete = FromDataBase_IsRouteComplete_Current.IsRouteComplete;

      string CRMStatus = "";
      string ViewUpdate = "";
      string SQLStringCRM = "SELECT CRM_Status , CASE WHEN CRM_Status = 'Rejected' THEN 'No' WHEN CRM_Type_List = 4395 AND CRM_Complaint_CloseOut = 0 THEN 'Yes' WHEN CRM_Type_List = 4395 AND CRM_Complaint_CloseOut = 1 THEN 'No' WHEN CRM_Type_List = 4406 AND CRM_Compliment_CloseOut = 0 THEN 'Yes' WHEN CRM_Type_List = 4406 AND CRM_Compliment_CloseOut = 1 THEN 'No' WHEN CRM_Type_List = 4412 AND CRM_Comment_CloseOut = 0 THEN 'Yes' WHEN CRM_Type_List = 4412 AND CRM_Comment_CloseOut = 1 THEN 'No' WHEN CRM_Type_List = 4413 AND CRM_Query_CloseOut = 0 THEN 'Yes' WHEN CRM_Type_List = 4413 AND CRM_Query_CloseOut = 1 THEN 'No' WHEN CRM_Type_List = 4414 AND CRM_Suggestion_CloseOut = 0 THEN 'Yes' WHEN CRM_Type_List = 4414 AND CRM_Suggestion_CloseOut = 1 THEN 'No' END AS ViewUpdate FROM Form_CRM WHERE CRM_Id = @CRM_Id";
      using (SqlCommand SqlCommand_CRM = new SqlCommand(SQLStringCRM))
      {
        SqlCommand_CRM.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
        DataTable DataTable_CRM;
        using (DataTable_CRM = new DataTable())
        {
          DataTable_CRM.Locale = CultureInfo.CurrentCulture;
          DataTable_CRM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRM).Copy();
          if (DataTable_CRM.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CRM.Rows)
            {
              CRMStatus = DataRow_Row["CRM_Status"].ToString();
              ViewUpdate = DataRow_Row["ViewUpdate"].ToString();
            }
          }
        }
      }

      if (IsRouteComplete == "Yes")
      {
        SetFormVisibility_Edit_RouteCompleteYes(CRMStatus, ViewUpdate);
      }
      else
      {
        SetFormVisibility_Edit_RouteCompleteNo();
      }

      CRMStatus = "";
      ViewUpdate = "";
    }

    protected void SetFormVisibility_Edit_RouteCompleteYes(string crmStatus, string viewUpdate)
    {
      FromDataBase_SecurityRole_Edit FromDataBase_SecurityRole_Edit_Current = GetSecurityRoleEdit();
      DataRow[] SecurityAdmin_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminHospitalManagerUpdate_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminHospitalManagerUpdate;
      DataRow[] SecurityFacilityAdminNSMUpdate_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminNSMUpdate;
      DataRow[] SecurityFacilityAdminView_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminView;
      DataRow[] SecurityFacilityInvestigator_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityInvestigator;
      DataRow[] SecurityFacilityApprover_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityApprover;
      DataRow[] SecurityFacilityCapturer_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityCapturer;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin_Edit.Length > 0 || SecurityFormAdminUpdate_Edit.Length > 0))
      {
        Security = "0";
        FormView_CRM_Form.ChangeMode(FormViewMode.Edit);
      }

      if (Security == "1" && (SecurityFacilityAdminHospitalManagerUpdate_Edit.Length > 0 || SecurityFacilityAdminNSMUpdate_Edit.Length > 0 || SecurityFacilityInvestigator_Edit.Length > 0))
      {
        Security = "0";
        if (viewUpdate == "Yes")
        {
          FormView_CRM_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_CRM_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && SecurityFacilityApprover_Edit.Length > 0)
      {
        Security = "0";
        if (crmStatus == "Pending Approval")
        {
          FormView_CRM_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_CRM_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView_Edit.Length > 0 || SecurityFacilityAdminView_Edit.Length > 0 || SecurityFacilityCapturer_Edit.Length > 0))
      {
        Security = "0";
        FormView_CRM_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Form", "Form_CRM.aspx"), false);
      }
    }

    protected void SetFormVisibility_Edit_RouteCompleteNo()
    {
      FromDataBase_SecurityRole_Edit FromDataBase_SecurityRole_Edit_Current = GetSecurityRoleEdit();
      DataRow[] SecurityAdmin_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminHospitalManagerUpdate_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminHospitalManagerUpdate;
      DataRow[] SecurityFacilityAdminNSMUpdate_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminNSMUpdate;
      DataRow[] SecurityFacilityAdminView_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityAdminView;
      DataRow[] SecurityFacilityInvestigator_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityInvestigator;
      DataRow[] SecurityFacilityApprover_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityApprover;
      DataRow[] SecurityFacilityCapturer_Edit = FromDataBase_SecurityRole_Edit_Current.SecurityFacilityCapturer;

      FromDataBase_SecurityRole_EditRoute FromDataBase_SecurityRole_EditRoute_Current = GetSecurityRoleEditRoute();
      DataRow[] SecurityAdmin_EditRoute = FromDataBase_SecurityRole_EditRoute_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate_EditRoute = FromDataBase_SecurityRole_EditRoute_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView_EditRoute = FromDataBase_SecurityRole_EditRoute_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminHospitalManagerUpdate_EditRoute = FromDataBase_SecurityRole_EditRoute_Current.SecurityFacilityAdminHospitalManagerUpdate;
      DataRow[] SecurityFacilityAdminNSMUpdate_EditRoute = FromDataBase_SecurityRole_EditRoute_Current.SecurityFacilityAdminNSMUpdate;
      DataRow[] SecurityFacilityAdminView_EditRoute = FromDataBase_SecurityRole_EditRoute_Current.SecurityFacilityAdminView;
      DataRow[] SecurityFacilityInvestigator_EditRoute = FromDataBase_SecurityRole_EditRoute_Current.SecurityFacilityInvestigator;
      DataRow[] SecurityFacilityApprover_EditRoute = FromDataBase_SecurityRole_EditRoute_Current.SecurityFacilityApprover;
      DataRow[] SecurityFacilityCapturer_EditRoute = FromDataBase_SecurityRole_EditRoute_Current.SecurityFacilityCapturer;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin_EditRoute.Length > 0 || SecurityFormAdminUpdate_EditRoute.Length > 0 || SecurityAdmin_Edit.Length > 0 || SecurityFormAdminUpdate_Edit.Length > 0))
      {
        Security = "0";
        FormView_CRM_Form.ChangeMode(FormViewMode.Edit);
      }

      if (Security == "1" && (SecurityFacilityAdminHospitalManagerUpdate_EditRoute.Length > 0 || SecurityFacilityAdminNSMUpdate_EditRoute.Length > 0 || SecurityFacilityInvestigator_EditRoute.Length > 0))
      {
        Security = "0";
        FormView_CRM_Form.ChangeMode(FormViewMode.Edit);
      }

      if (Security == "1" && (SecurityFormAdminView_EditRoute.Length > 0 || SecurityFacilityAdminView_EditRoute.Length > 0 || SecurityFacilityApprover_EditRoute.Length > 0 || SecurityFacilityCapturer_EditRoute.Length > 0 || SecurityFacilityAdminHospitalManagerUpdate_Edit.Length > 0 || SecurityFacilityAdminNSMUpdate_Edit.Length > 0 || SecurityFacilityInvestigator_Edit.Length > 0 || SecurityFacilityApprover_Edit.Length > 0 || SecurityFormAdminView_Edit.Length > 0 || SecurityFacilityAdminView_Edit.Length > 0 || SecurityFacilityCapturer_Edit.Length > 0))
      {
        Security = "0";
        FormView_CRM_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Form", "Form_CRM.aspx"), false);
      }
    }

    protected void TableFormVisible()
    {
      if (FormView_CRM_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertFacility")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertDateReceived")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertDateReceived")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertOriginatedAtList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertTypeList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertReceivedViaList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertReceivedFromList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertEscalatedForm")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerEmail")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerEmail")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientVisitNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientVisitNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientDateOfAdmission")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientDateOfAdmission")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientEmail")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientEmail")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientContactNumber")).Attributes.Add("OnInput", "Validation_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintDateOccurred")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintDateOccurred")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintTimeOccuredHours")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintTimeOccuredMinutes")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintPriorityList")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBoxList)FormView_CRM_Form.FindControl("CheckBoxList_InsertComplaintCategoryItemList")).Attributes.Add("OnClick", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintWithin24Hours")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin24HoursReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin24HoursReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintWithin5Days")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin5DaysReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin5DaysReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintWithin10Days")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin10DaysReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin10DaysReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintWithin3Days")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin3DaysReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin3DaysReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintCustomerSatisfied")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintCustomerSatisfiedReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintCustomerSatisfiedReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintInvestigatorName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintInvestigatorName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintInvestigatorDesignation")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintInvestigatorDesignation")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintRootCause")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintRootCause")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplaintCloseOut")).Attributes.Add("OnClick", "Validation_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplimentDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplimentDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplimentUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplimentAcknowledge")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplimentCloseOut")).Attributes.Add("OnClick", "Validation_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCommentDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCommentDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentAdditionalUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentTypeList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentAdditionalTypeList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentAdditionalCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertCommentAcknowledge")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertCommentCloseOut")).Attributes.Add("OnClick", "Validation_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertQueryDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertQueryDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertQueryUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertQueryAcknowledge")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertQueryCloseOut")).Attributes.Add("OnClick", "Validation_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertSuggestionDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertSuggestionDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertSuggestionUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertSuggestionAcknowledge")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertSuggestionCloseOut")).Attributes.Add("OnClick", "Validation_Form();");

        TextBox TextBox_InsertDateReceived = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertDateReceived");
        if (string.IsNullOrEmpty(TextBox_InsertDateReceived.Text))
        {
          TextBox_InsertDateReceived.Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        }
      }

      if (FormView_CRM_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditDateReceived")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditDateReceived")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditTypeList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditReceivedViaList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditReceivedFromList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditEscalatedForm")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerEmail")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerEmail")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerContactNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientVisitNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientVisitNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientDateOfAdmission")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientDateOfAdmission")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientEmail")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientEmail")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientContactNumber")).Attributes.Add("OnInput", "Validation_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintDateOccurred")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintDateOccurred")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintTimeOccuredHours")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintTimeOccuredMinutes")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintPriorityList")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBoxList)FormView_CRM_Form.FindControl("CheckBoxList_EditComplaintCategoryItemList")).Attributes.Add("OnClick", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintWithin24Hours")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin24HoursReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin24HoursReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintWithin5Days")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin5DaysReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin5DaysReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintWithin10Days")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin10DaysReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin10DaysReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintWithin3Days")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin3DaysReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin3DaysReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintCustomerSatisfied")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintCustomerSatisfiedReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintCustomerSatisfiedReason")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintInvestigatorName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintInvestigatorName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintInvestigatorDesignation")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintInvestigatorDesignation")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintRootCause")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintRootCause")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplaintCloseOut")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplimentDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplimentDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplimentUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentAcknowledge")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentCloseOut")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditCommentDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditCommentDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentAdditionalUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentTypeList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentAdditionalTypeList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentAdditionalCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentAcknowledge")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentCloseOut")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditQueryDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditQueryDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditQueryUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryAcknowledge")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryCloseOut")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");

        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditSuggestionDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditSuggestionDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditSuggestionUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionAcknowledge")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionCloseOut")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");

        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteRoute")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditRouteFacility")).Attributes.Add("OnChange", "Validation_Form();");
        ((RadioButtonList)FormView_CRM_Form.FindControl("RadioButtonList_EditRouteUnit")).Attributes.Add("OnClick", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditRouteComment")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditRouteComment")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteComplete")).Attributes.Add("OnClick", "Validation_Form();");

        ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditStatus")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditStatusRejectedReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_CRM_Form.FindControl("TextBox_EditStatusRejectedReason")).Attributes.Add("OnInput", "Validation_Form();");

        RouteForm();
      }
    }

    protected void RedirectToIncompleteOther()
    {
      string SearchField1 = Request.QueryString["SearchIO_FacilityType"];
      string SearchField2 = Request.QueryString["SearchIO_FacilityId"];
      string SearchField3 = Request.QueryString["SearchIO_CRMReportNumber"];
      string SearchField4 = Request.QueryString["SearchIO_CRMTypeList"];
      string SearchField5 = Request.QueryString["SearchIO_CRMPatientVisitNumber"];
      string SearchField6 = Request.QueryString["SearchIO_CRMName"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Type=" + Request.QueryString["SearchIO_FacilityType"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Facility_Id=" + Request.QueryString["SearchIO_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_CRM_ReportNumber=" + Request.QueryString["SearchIO_CRMReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_CRM_TypeList=" + Request.QueryString["SearchIO_CRMTypeList"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_CRM_PatientVisitNumber=" + Request.QueryString["SearchIO_CRMPatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_CRM_Name=" + Request.QueryString["SearchIO_CRMName"] + "&";
      }

      string FinalURL = "Form_CRM_IncompleteOther.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Incomplete Other", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void RedirectToIncompleteComplaints()
    {
      string SearchField1 = Request.QueryString["SearchIC_FacilityType"];
      string SearchField2 = Request.QueryString["SearchIC_FacilityId"];
      string SearchField3 = Request.QueryString["SearchIC_CRMReportNumber"];
      string SearchField4 = Request.QueryString["SearchIC_CRMPatientVisitNumber"];
      string SearchField5 = Request.QueryString["SearchIC_CRMName"];
      string SearchField6 = Request.QueryString["SearchIC_CRMEscalatedForm"];
      string SearchField7 = Request.QueryString["SearchIC_CRMRoute"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Type=" + Request.QueryString["SearchIC_FacilityType"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Facility_Id=" + Request.QueryString["SearchIC_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_CRM_ReportNumber=" + Request.QueryString["SearchIC_CRMReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_CRM_PatientVisitNumber=" + Request.QueryString["SearchIC_CRMPatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_CRM_Name=" + Request.QueryString["SearchIC_CRMName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_CRM_EscalatedForm=" + Request.QueryString["SearchIC_CRMEscalatedForm"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "s_CRM_Route=" + Request.QueryString["SearchIC_CRMRoute"] + "&";
      }

      string FinalURL = "Form_CRM_IncompleteComplaints.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Incomplete Complaints", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityType"];
      string SearchField2 = Request.QueryString["Search_FacilityId"];
      string SearchField3 = Request.QueryString["Search_CRMReportNumber"];
      string SearchField4 = Request.QueryString["Search_CRMTypeList"];
      string SearchField5 = Request.QueryString["Search_CRMOriginatedAtList"];
      string SearchField6 = Request.QueryString["Search_CRMReceivedFromList"];
      string SearchField7 = Request.QueryString["Search_CRMPatientVisitNumber"];
      string SearchField8 = Request.QueryString["Search_CRMName"];
      string SearchField9 = Request.QueryString["Search_CRMStatus"];
      string SearchField10 = Request.QueryString["Search_CRMStatusDateFrom"];
      string SearchField11 = Request.QueryString["Search_CRMStatusDateTo"];
      string SearchField12 = Request.QueryString["Search_CRMCloseOut"];
      string SearchField13 = Request.QueryString["Search_CRMRoute"];


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
        SearchField3 = "s_CRM_ReportNumber=" + Request.QueryString["Search_CRMReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_CRM_TypeList=" + Request.QueryString["Search_CRMTypeList"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_CRM_OriginatedAtList=" + Request.QueryString["Search_CRMOriginatedAtList"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_CRM_ReceivedFromList=" + Request.QueryString["Search_CRMReceivedFromList"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "s_CRM_PatientVisitNumber=" + Request.QueryString["Search_CRMPatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField8))
      {
        SearchField8 = "s_CRM_Name=" + Request.QueryString["Search_CRMName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField9))
      {
        SearchField9 = "s_CRM_Status=" + Request.QueryString["Search_CRMStatus"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField10))
      {
        SearchField10 = "s_CRM_StatusDateFrom=" + Request.QueryString["Search_CRMStatusDateFrom"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField11))
      {
        SearchField11 = "s_CRM_StatusDateTo=" + Request.QueryString["Search_CRMStatusDateTo"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField12))
      {
        SearchField12 = "s_CRM_CloseOut=" + Request.QueryString["Search_CRMCloseOut"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField13))
      {
        SearchField13 = "s_CRM_Route=" + Request.QueryString["Search_CRMRoute"] + "&";
      }

      string FinalURL = "Form_CRM_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7 + SearchField8 + SearchField9 + SearchField10 + SearchField11 + SearchField12 + SearchField13;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void FileCleanUp()
    {
      if (string.IsNullOrEmpty(Request.QueryString["CRM_Id"]))
      {
        HiddenField HiddenField_InsertCRMIdTemp = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertCRMIdTemp");

        string SQLStringCRMFile = "DELETE FROM Form_CRM_File WHERE CRM_Id IS NULL AND CRM_File_CreatedBy = @CRM_File_CreatedBy AND CRM_File_Temp_CRM_Id <> @CRM_File_Temp_CRM_Id";
        using (SqlCommand SqlCommand_CRMFile = new SqlCommand(SQLStringCRMFile))
        {
          SqlCommand_CRMFile.Parameters.AddWithValue("@CRM_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_CRMFile.Parameters.AddWithValue("@CRM_File_Temp_CRM_Id", HiddenField_InsertCRMIdTemp.Value);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_CRMFile);
        }
      }
    }

    protected void ComplaintCategoryCleanUp()
    {
      if (string.IsNullOrEmpty(Request.QueryString["CRM_Id"]))
      {
        HiddenField HiddenField_InsertCRMIdTemp = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertCRMIdTemp");

        string SQLStringCRMComplaintCategory = "DELETE FROM Form_CRM_Complaint_Category WHERE CRM_Id IS NULL AND CRM_Complaint_Category_CreatedBy = @CRM_Complaint_Category_CreatedBy AND CRM_Complaint_Category_Temp_CRM_Id <> @CRM_Complaint_Category_Temp_CRM_Id";
        using (SqlCommand SqlCommand_CRMComplaintCategory = new SqlCommand(SQLStringCRMComplaintCategory))
        {
          SqlCommand_CRMComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_CreatedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_CRMComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_Temp_CRM_Id", HiddenField_InsertCRMIdTemp.Value);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_CRMComplaintCategory);
        }
      }
    }

    protected void PXM_PDCH_Results()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["CRM_Id"]))
      {
        string Survey = "";
        string SQLStringSurvey = "SELECT SUBSTRING(CRM_UploadedFrom,0,CHARINDEX(' :',CRM_UploadedFrom)) AS Survey FROM Form_CRM WHERE CRM_Id = @CRM_Id";
        using (SqlCommand SqlCommand_Survey = new SqlCommand(SQLStringSurvey))
        {
          SqlCommand_Survey.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
          DataTable DataTable_Survey;
          using (DataTable_Survey = new DataTable())
          {
            DataTable_Survey.Locale = CultureInfo.CurrentCulture;
            DataTable_Survey = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Survey).Copy();
            if (DataTable_Survey.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Survey.Rows)
              {
                Survey = DataRow_Row["Survey"].ToString();
              }
            }
          }
        }

        var HeaderFont = FontFactory.GetFont("Verdana, Geneva, sans-serif", 20, new BaseColor(0, 0, 0));
        var TableHeaderFont = FontFactory.GetFont("Verdana, Geneva, sans-serif", 16, new BaseColor(255, 255, 255));
        var TableCellFont = FontFactory.GetFont("Verdana, Geneva, sans-serif", 12, new BaseColor(0, 0, 0));

        Paragraph Paragraph_Space = new Paragraph(" ");
        Paragraph Paragraph_Heading;
        if (string.IsNullOrEmpty(Survey))
        {
          Paragraph_Heading = new Paragraph("Survey", HeaderFont);
        }
        else
        {
          Paragraph_Heading = new Paragraph(Survey, HeaderFont);
        }

        iTextSharp.text.Image Image_Logo = iTextSharp.text.Image.GetInstance(Server.MapPath("App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg"));
        Image_Logo.ScalePercent(50f);

        PdfPTable PdfPTable_Survey = new PdfPTable(2);
        float[] Cell_Widths = new float[] { 8f, 2f };
        PdfPTable_Survey.SetWidths(Cell_Widths);
        PdfPTable_Survey.HorizontalAlignment = 0;

        PdfPCell PdfPCell_Question = new PdfPCell(new Phrase("Question", TableHeaderFont));
        PdfPCell_Question.BackgroundColor = new BaseColor(0, 55, 104);

        PdfPCell PdfPCell_Answer = new PdfPCell(new Phrase("Answer", TableHeaderFont));
        PdfPCell_Answer.BackgroundColor = new BaseColor(0, 55, 104);

        PdfPTable_Survey.AddCell(PdfPCell_Question);
        PdfPTable_Survey.AddCell(PdfPCell_Answer);

        string SQLStringExportDataToPDF = "SELECT CRM_PXM_PDCH_Result_Question AS Question , CRM_PXM_PDCH_Result_Answer AS Answer FROM Form_CRM_PXM_PDCH_Result WHERE CRM_Id = @CRM_Id";
        using (SqlCommand SqlCommand_ExportDataToPDF = new SqlCommand(SQLStringExportDataToPDF))
        {
          SqlCommand_ExportDataToPDF.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
          DataTable DataTable_ExportDataToPDF;
          using (DataTable_ExportDataToPDF = new DataTable())
          {
            DataTable_ExportDataToPDF.Locale = CultureInfo.CurrentCulture;
            DataTable_ExportDataToPDF = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ExportDataToPDF).Copy();
            if (DataTable_ExportDataToPDF.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_ExportDataToPDF.Rows)
              {
                string Question = DataRow_Row["Question"].ToString();
                string Answer = DataRow_Row["Answer"].ToString();

                PdfPCell PdfPCell_Questions = new PdfPCell(new Phrase(Question, TableCellFont));
                PdfPCell PdfPCell_Answers = new PdfPCell(new Phrase(Answer, TableCellFont));

                PdfPTable_Survey.AddCell(PdfPCell_Questions);
                PdfPTable_Survey.AddCell(PdfPCell_Answers);
              }
            }
          }
        }

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=PostDischargeSurveyResults.pdf");

        Document Document_PXMPDCHResults;
        using (Document_PXMPDCHResults = new Document(PageSize.A4))
        {
          PdfWriter.GetInstance(Document_PXMPDCHResults, Response.OutputStream);
          Document_PXMPDCHResults.Open();
          Document_PXMPDCHResults.Add(Image_Logo);
          Document_PXMPDCHResults.Add(Paragraph_Space);
          Document_PXMPDCHResults.Add(Paragraph_Heading);
          Document_PXMPDCHResults.Add(Paragraph_Space);
          Document_PXMPDCHResults.Add(PdfPTable_Survey);
        }

        Response.Write(Document_PXMPDCHResults);
        Response.Flush();
        Response.End();
      }
    }

    protected void RouteForm()
    {
      CheckBox CheckBox_EditRouteRoute = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteRoute");
      Label Label_EditRouteRoute = (Label)FormView_CRM_Form.FindControl("Label_EditRouteRoute");
      DropDownList DropDownList_EditRouteFacility = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditRouteFacility");
      Label Label_EditRouteFacility = (Label)FormView_CRM_Form.FindControl("Label_EditRouteFacility");
      RadioButtonList RadioButtonList_EditRouteUnit = (RadioButtonList)FormView_CRM_Form.FindControl("RadioButtonList_EditRouteUnit");
      Label Label_EditRouteUnit = (Label)FormView_CRM_Form.FindControl("Label_EditRouteUnit");
      TextBox TextBox_EditRouteComment = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditRouteComment");
      CheckBox CheckBox_EditRouteComplete = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteComplete");
      Label Label_EditRouteComplete = (Label)FormView_CRM_Form.FindControl("Label_EditRouteComplete");
      Label Label_EditRouteCompleteDate = (Label)FormView_CRM_Form.FindControl("Label_EditRouteCompleteDate");

      FromDataBase_IsRouteComplete FromDataBase_IsRouteComplete_Current = GetIsRouteComplete();
      string IsRouteComplete = FromDataBase_IsRouteComplete_Current.IsRouteComplete;

      FromDataBase_CRMRouteValues FromDataBase_CRMRouteValues_Current = GetCRMRouteValues();
      string CRMRouteRoute = FromDataBase_CRMRouteValues_Current.CRMRouteRoute;
      string CRMRouteFacilityFacilityDisplayName = FromDataBase_CRMRouteValues_Current.FacilityFacilityDisplayName;
      string CRMRouteToUnitName = FromDataBase_CRMRouteValues_Current.CRMRouteToUnitName;
      string CRMRouteComment = FromDataBase_CRMRouteValues_Current.CRMRouteComment;

      if (IsRouteComplete == "Yes")
      {
        CheckBox_EditRouteRoute.Visible = true;
        Label_EditRouteRoute.Visible = false;

        DropDownList_EditRouteFacility.Visible = true;
        Label_EditRouteFacility.Visible = false;

        RadioButtonList_EditRouteUnit.Visible = true;
        Label_EditRouteUnit.Visible = false;

        TextBox_EditRouteComment.Visible = true;

        CheckBox_EditRouteComplete.Visible = false;
        Label_EditRouteComplete.Visible = true;
        Label_EditRouteCompleteDate.Visible = true;
      }
      else
      {
        CheckBox_EditRouteRoute.Visible = false;
        Label_EditRouteRoute.Visible = true;

        DropDownList_EditRouteFacility.Visible = false;
        Label_EditRouteFacility.Visible = true;

        RadioButtonList_EditRouteUnit.Visible = false;
        Label_EditRouteUnit.Visible = true;

        TextBox_EditRouteComment.Visible = true;

        CheckBox_EditRouteComplete.Visible = true;
        Label_EditRouteComplete.Visible = false;
        Label_EditRouteCompleteDate.Visible = true;

        Label_EditRouteRoute.Text = CRMRouteRoute;
        Label_EditRouteFacility.Text = CRMRouteFacilityFacilityDisplayName;
        Label_EditRouteUnit.Text = CRMRouteToUnitName;
        TextBox_EditRouteComment.Text = CRMRouteComment;
        CheckBox_EditRouteComplete.Checked = false;
      }
    }


    private class FromDataBase_SecurityRole
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
      public DataRow[] SecurityFacilityAdminHospitalManagerUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminNSMUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminView { get; set; }
      public DataRow[] SecurityFacilityInvestigator { get; set; }
      public DataRow[] SecurityFacilityApprover { get; set; }
      public DataRow[] SecurityFacilityCapturer { get; set; }
    }

    private FromDataBase_SecurityRole GetSecurityRole()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_New = new FromDataBase_SecurityRole();

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('36'))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
          FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '146'");
          FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '147'");
          FromDataBase_SecurityRole_New.SecurityFacilityAdminHospitalManagerUpdate = DataTable_FormMode.Select("SecurityRole_Id = '150'");
          FromDataBase_SecurityRole_New.SecurityFacilityAdminNSMUpdate = DataTable_FormMode.Select("SecurityRole_Id = '148'");
          FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '149'");
          FromDataBase_SecurityRole_New.SecurityFacilityInvestigator = DataTable_FormMode.Select("SecurityRole_Id = '153'");
          FromDataBase_SecurityRole_New.SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '151'");
          FromDataBase_SecurityRole_New.SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '152'");

          //if (DataTable_FormMode.Rows.Count > 0)
          //{
          //  FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
          //  FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '146'");
          //  FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '147'");
          //  FromDataBase_SecurityRole_New.SecurityFacilityAdminHospitalManagerUpdate = DataTable_FormMode.Select("SecurityRole_Id = '150'");
          //  FromDataBase_SecurityRole_New.SecurityFacilityAdminNSMUpdate = DataTable_FormMode.Select("SecurityRole_Id = '148'");
          //  FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '149'");
          //  FromDataBase_SecurityRole_New.SecurityFacilityInvestigator = DataTable_FormMode.Select("SecurityRole_Id = '153'");
          //  FromDataBase_SecurityRole_New.SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '151'");
          //  FromDataBase_SecurityRole_New.SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '152'");
          //}
          //else
          //{
          //  FromDataBase_SecurityRole_New.SecurityAdmin = null;
          //  FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = null;
          //  FromDataBase_SecurityRole_New.SecurityFormAdminView = null;
          //  FromDataBase_SecurityRole_New.SecurityFacilityAdminHospitalManagerUpdate = null;
          //  FromDataBase_SecurityRole_New.SecurityFacilityAdminNSMUpdate = null;
          //  FromDataBase_SecurityRole_New.SecurityFacilityAdminView = null;
          //  FromDataBase_SecurityRole_New.SecurityFacilityInvestigator = null;
          //  FromDataBase_SecurityRole_New.SecurityFacilityApprover = null;
          //  FromDataBase_SecurityRole_New.SecurityFacilityCapturer = null;
          //}
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_SecurityRole_Edit
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
      public DataRow[] SecurityFacilityAdminHospitalManagerUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminNSMUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminView { get; set; }
      public DataRow[] SecurityFacilityInvestigator { get; set; }
      public DataRow[] SecurityFacilityApprover { get; set; }
      public DataRow[] SecurityFacilityCapturer { get; set; }
    }

    private FromDataBase_SecurityRole_Edit GetSecurityRoleEdit()
    {
      FromDataBase_SecurityRole_Edit FromDataBase_SecurityRole_Edit_New = new FromDataBase_SecurityRole_Edit();

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('36')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_CRM WHERE CRM_Id = @CRM_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          FromDataBase_SecurityRole_Edit_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
          FromDataBase_SecurityRole_Edit_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '146'");
          FromDataBase_SecurityRole_Edit_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '147'");
          FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminHospitalManagerUpdate = DataTable_FormMode.Select("SecurityRole_Id = '150'");
          FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminNSMUpdate = DataTable_FormMode.Select("SecurityRole_Id = '148'");
          FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '149'");
          FromDataBase_SecurityRole_Edit_New.SecurityFacilityInvestigator = DataTable_FormMode.Select("SecurityRole_Id = '153'");
          FromDataBase_SecurityRole_Edit_New.SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '151'");
          FromDataBase_SecurityRole_Edit_New.SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '152'");

          //if (DataTable_FormMode.Rows.Count > 0)
          //{
          //  FromDataBase_SecurityRole_Edit_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
          //  FromDataBase_SecurityRole_Edit_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '146'");
          //  FromDataBase_SecurityRole_Edit_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '147'");
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminHospitalManagerUpdate = DataTable_FormMode.Select("SecurityRole_Id = '150'");
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminNSMUpdate = DataTable_FormMode.Select("SecurityRole_Id = '148'");
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '149'");
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityInvestigator = DataTable_FormMode.Select("SecurityRole_Id = '153'");
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '151'");
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '152'");
          //}
          //else
          //{
          //  FromDataBase_SecurityRole_Edit_New.SecurityAdmin = null;
          //  FromDataBase_SecurityRole_Edit_New.SecurityFormAdminUpdate = null;
          //  FromDataBase_SecurityRole_Edit_New.SecurityFormAdminView = null;
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminHospitalManagerUpdate = null;
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminNSMUpdate = null;
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityAdminView = null;
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityInvestigator = null;
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityApprover = null;
          //  FromDataBase_SecurityRole_Edit_New.SecurityFacilityCapturer = null;
          //}
        }
      }

      return FromDataBase_SecurityRole_Edit_New;
    }

    private class FromDataBase_SecurityRole_EditRoute
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
      public DataRow[] SecurityFacilityAdminHospitalManagerUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminNSMUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminView { get; set; }
      public DataRow[] SecurityFacilityInvestigator { get; set; }
      public DataRow[] SecurityFacilityApprover { get; set; }
      public DataRow[] SecurityFacilityCapturer { get; set; }
    }

    private FromDataBase_SecurityRole_EditRoute GetSecurityRoleEditRoute()
    {
      FromDataBase_SecurityRole_EditRoute FromDataBase_SecurityRole_EditRoute_New = new FromDataBase_SecurityRole_EditRoute();

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('36')) AND (Facility_Id IN (SELECT TOP 1 CASE WHEN ISNULL(vForm_CRM_Route.CRM_Route_Complete,'1') = 1 THEN NULL ELSE vForm_CRM_Route.Facility_Id END AS RouteFacility FROM Form_CRM LEFT JOIN vForm_CRM_Route ON Form_CRM.CRM_Id = vForm_CRM_Route.CRM_Id WHERE Form_CRM.CRM_Id = @CRM_Id ORDER BY CRM_Route_CreatedDate DESC) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          FromDataBase_SecurityRole_EditRoute_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
          FromDataBase_SecurityRole_EditRoute_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '146'");
          FromDataBase_SecurityRole_EditRoute_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '147'");
          FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityAdminHospitalManagerUpdate = DataTable_FormMode.Select("SecurityRole_Id = '150'");
          FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityAdminNSMUpdate = DataTable_FormMode.Select("SecurityRole_Id = '148'");
          FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '149'");
          FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityInvestigator = DataTable_FormMode.Select("SecurityRole_Id = '153'");
          FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '151'");
          FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '152'");

          //if (DataTable_FormMode.Rows.Count > 0)
          //{
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '146'");
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '147'");
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityAdminHospitalManagerUpdate = DataTable_FormMode.Select("SecurityRole_Id = '150'");
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityAdminNSMUpdate = DataTable_FormMode.Select("SecurityRole_Id = '148'");
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '149'");
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityInvestigator = DataTable_FormMode.Select("SecurityRole_Id = '153'");
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '151'");
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '152'");
          //}
          //else
          //{
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityAdmin = null;
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFormAdminUpdate = null;
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFormAdminView = null;
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityAdminHospitalManagerUpdate = null;
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityAdminNSMUpdate = null;
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityAdminView = null;
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityInvestigator = null;
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityApprover = null;
          //  FromDataBase_SecurityRole_EditRoute_New.SecurityFacilityCapturer = null;
          //}
        }
      }

      return FromDataBase_SecurityRole_EditRoute_New;
    }

    private class FromDataBase_IsRouteComplete
    {
      public string IsRouteComplete { get; set; }
    }

    private FromDataBase_IsRouteComplete GetIsRouteComplete()
    {
      FromDataBase_IsRouteComplete FromDataBase_IsRouteComplete_New = new FromDataBase_IsRouteComplete();

      string SQLStringCRMRouteComplete = "SELECT TOP 1 CASE ISNULL(vForm_CRM_Route.CRM_Route_Complete,'1') WHEN '1' THEN 'Yes' ELSE 'No' END AS IsRouteComplete FROM Form_CRM LEFT JOIN vForm_CRM_Route ON Form_CRM.CRM_Id = vForm_CRM_Route.CRM_Id WHERE Form_CRM.CRM_Id = @CRM_Id ORDER BY CRM_Route_CreatedDate DESC";
      using (SqlCommand SqlCommand_CRMRouteComplete = new SqlCommand(SQLStringCRMRouteComplete))
      {
        SqlCommand_CRMRouteComplete.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
        DataTable DataTable_CRMRouteComplete;
        using (DataTable_CRMRouteComplete = new DataTable())
        {
          DataTable_CRMRouteComplete.Locale = CultureInfo.CurrentCulture;
          DataTable_CRMRouteComplete = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMRouteComplete).Copy();
          if (DataTable_CRMRouteComplete.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CRMRouteComplete.Rows)
            {
              FromDataBase_IsRouteComplete_New.IsRouteComplete = DataRow_Row["IsRouteComplete"].ToString();
            }
          }
        }
      }

      return FromDataBase_IsRouteComplete_New;
    }

    private class FromDataBase_CRMValues
    {
      public string CRMId { get; set; }
      //public String FacilityId { get; set; }
      public string FacilityFacilityDisplayName { get; set; }
      public string CRMReportNumber { get; set; }
    }

    private FromDataBase_CRMValues GetCRMValues()
    {
      FromDataBase_CRMValues FromDataBase_CRMValues_New = new FromDataBase_CRMValues();

      string SQLStringCRM = "SELECT CRM_Id , Facility_Id , Facility_FacilityDisplayName , CRM_ReportNumber FROM vForm_CRM WHERE CRM_Id = @CRM_Id";
      using (SqlCommand SqlCommand_CRM = new SqlCommand(SQLStringCRM))
      {
        SqlCommand_CRM.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
        DataTable DataTable_CRM;
        using (DataTable_CRM = new DataTable())
        {
          DataTable_CRM.Locale = CultureInfo.CurrentCulture;
          DataTable_CRM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRM).Copy();
          if (DataTable_CRM.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CRM.Rows)
            {
              FromDataBase_CRMValues_New.CRMId = DataRow_Row["CRM_Id"].ToString();
              //FromDataBase_CRMValues_New.FacilityId = DataRow_Row["Facility_Id"].ToString();
              FromDataBase_CRMValues_New.FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              FromDataBase_CRMValues_New.CRMReportNumber = DataRow_Row["CRM_ReportNumber"].ToString();
            }
          }
        }
      }

      return FromDataBase_CRMValues_New;
    }

    private class FromDataBase_CRMRouteValues
    {
      public string CRMRouteId { get; set; }
      public string CRMRouteRoute { get; set; }
      //public String FacilityId { get; set; }
      public string FacilityFacilityDisplayName { get; set; }
      //public String CRMRouteToUnitList { get; set; }
      public string CRMRouteToUnitName { get; set; }
      public string CRMRouteComment { get; set; }
      //public String CRMRouteComplete { get; set; }
      //public String CRMRouteCompleteDate { get; set; }
      //public String CRMRouteCreatedDate { get; set; }
      //public String CRMRouteCreatedBy { get; set; }
      //public String CRMRouteModifiedDate { get; set; }
      //public String CRMRouteModifiedBy { get; set; }
      public string CRMRouteHistory { get; set; }
    }

    private FromDataBase_CRMRouteValues GetCRMRouteValues()
    {
      FromDataBase_CRMRouteValues FromDataBase_CRMRouteValues_New = new FromDataBase_CRMRouteValues();

      string SQLStringCRMRouteComplete = "SELECT TOP 1 CRM_Route_Id , CASE CRM_Route_Route WHEN '1' THEN 'Yes' WHEN '0' THEN 'No' END AS CRM_Route_Route , vForm_CRM_Route.Facility_Id , Facility_FacilityDisplayName , CRM_Route_ToUnit_List , CRM_Route_ToUnit_Name , CRM_Route_Comment , CASE CRM_Route_Complete WHEN '1' THEN 'Yes' WHEN '0' THEN 'No' END AS CRM_Route_Complete , CRM_Route_CompleteDate , CRM_Route_CreatedDate , CRM_Route_CreatedBy , CRM_Route_ModifiedDate , CRM_Route_ModifiedBy , CRM_Route_History , CASE ISNULL(vForm_CRM_Route.CRM_Route_Complete,'1') WHEN '1' THEN 'Yes' ELSE 'No' END AS IsRouteComplete FROM Form_CRM LEFT JOIN vForm_CRM_Route ON Form_CRM.CRM_Id = vForm_CRM_Route.CRM_Id WHERE Form_CRM.CRM_Id = @CRM_Id ORDER BY CRM_Route_CreatedDate DESC";
      using (SqlCommand SqlCommand_CRMRouteComplete = new SqlCommand(SQLStringCRMRouteComplete))
      {
        SqlCommand_CRMRouteComplete.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
        DataTable DataTable_CRMRouteComplete;
        using (DataTable_CRMRouteComplete = new DataTable())
        {
          DataTable_CRMRouteComplete.Locale = CultureInfo.CurrentCulture;
          DataTable_CRMRouteComplete = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMRouteComplete).Copy();
          if (DataTable_CRMRouteComplete.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CRMRouteComplete.Rows)
            {
              FromDataBase_CRMRouteValues_New.CRMRouteId = DataRow_Row["CRM_Route_Id"].ToString();
              FromDataBase_CRMRouteValues_New.CRMRouteRoute = DataRow_Row["CRM_Route_Route"].ToString();
              //FromDataBase_CRMRouteValues_New.FacilityId = DataRow_Row["Facility_Id"].ToString();
              FromDataBase_CRMRouteValues_New.FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              //FromDataBase_CRMRouteValues_New.CRMRouteToUnitList = DataRow_Row["CRM_Route_ToUnit_List"].ToString();
              FromDataBase_CRMRouteValues_New.CRMRouteToUnitName = DataRow_Row["CRM_Route_ToUnit_Name"].ToString();
              FromDataBase_CRMRouteValues_New.CRMRouteComment = DataRow_Row["CRM_Route_Comment"].ToString();
              //FromDataBase_CRMRouteValues_New.CRMRouteComplete = DataRow_Row["CRM_Route_Complete"].ToString();
              //FromDataBase_CRMRouteValues_New.CRMRouteCompleteDate = DataRow_Row["CRM_Route_CompleteDate"].ToString();
              //FromDataBase_CRMRouteValues_New.CRMRouteCreatedDate = DataRow_Row["CRM_Route_CreatedDate"].ToString();
              //FromDataBase_CRMRouteValues_New.CRMRouteCreatedBy = DataRow_Row["CRM_Route_CreatedBy"].ToString();
              //FromDataBase_CRMRouteValues_New.CRMRouteModifiedDate = DataRow_Row["CRM_Route_ModifiedDate"].ToString();
              //FromDataBase_CRMRouteValues_New.CRMRouteModifiedBy = DataRow_Row["CRM_Route_ModifiedBy"].ToString();
              FromDataBase_CRMRouteValues_New.CRMRouteHistory = DataRow_Row["CRM_Route_History"].ToString();
            }
          }
        }
      }

      return FromDataBase_CRMRouteValues_New;
    }


    //--START-- --TableForm--//
    //--START-- --Insert--//
    protected void FormView_CRM_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_CRM.SetFocus(UpdatePanel_CRM);

          ((Label)FormView_CRM_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_CRM_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";

          InsertRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;
          DropDownList DropDownList_InsertFacility = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertFacility");
          HiddenField HiddenField_InsertCRMIdTemp = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertCRMIdTemp");

          Session["CRM_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], DropDownList_InsertFacility.SelectedValue.ToString(), "36");

          SqlDataSource_CRM_Form.InsertParameters["Facility_Id"].DefaultValue = DropDownList_InsertFacility.SelectedValue.ToString();
          SqlDataSource_CRM_Form.InsertParameters["CRM_ReportNumber"].DefaultValue = Session["CRM_ReportNumber"].ToString();

          DropDownList DropDownList_InsertOriginatedAtList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertOriginatedAtList");
          if (DropDownList_InsertOriginatedAtList.Visible == false)
          {
            SqlDataSource_CRM_Form.InsertParameters["CRM_OriginatedAt_List"].DefaultValue = "4408";
          }
          else
          {
            SqlDataSource_CRM_Form.InsertParameters["CRM_OriginatedAt_List"].DefaultValue = DropDownList_InsertOriginatedAtList.SelectedValue;
          }

          SqlDataSource_CRM_Form.InsertParameters["CRM_DateForwarded"].DefaultValue = DateTime.Now.ToString();

          DropDownList DropDownList_InsertComplaintUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintUnitId");
          DropDownList DropDownList_InsertComplimentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplimentUnitId");
          DropDownList DropDownList_InsertCommentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentUnitId");
          DropDownList DropDownList_InsertCommentAdditionalUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentAdditionalUnitId");
          DropDownList DropDownList_InsertQueryUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertQueryUnitId");
          DropDownList DropDownList_InsertSuggestionUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertSuggestionUnitId");
          SqlDataSource_CRM_Form.InsertParameters["CRM_Complaint_Unit_Id"].DefaultValue = DropDownList_InsertComplaintUnitId.SelectedValue.ToString();
          SqlDataSource_CRM_Form.InsertParameters["CRM_Compliment_Unit_Id"].DefaultValue = DropDownList_InsertComplimentUnitId.SelectedValue.ToString();
          SqlDataSource_CRM_Form.InsertParameters["CRM_Comment_Unit_Id"].DefaultValue = DropDownList_InsertCommentUnitId.SelectedValue.ToString();
          SqlDataSource_CRM_Form.InsertParameters["CRM_Comment_AdditionalUnit_Id"].DefaultValue = DropDownList_InsertCommentAdditionalUnitId.SelectedValue.ToString();
          SqlDataSource_CRM_Form.InsertParameters["CRM_Query_Unit_Id"].DefaultValue = DropDownList_InsertQueryUnitId.SelectedValue.ToString();
          SqlDataSource_CRM_Form.InsertParameters["CRM_Suggestion_Unit_Id"].DefaultValue = DropDownList_InsertSuggestionUnitId.SelectedValue.ToString();
          SqlDataSource_CRM_Form.InsertParameters["CRM_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_CRM_Form.InsertParameters["CRM_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_CRM_Form.InsertParameters["CRM_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_CRM_Form.InsertParameters["CRM_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_CRM_Form.InsertParameters["CRM_History"].DefaultValue = "";
          SqlDataSource_CRM_Form.InsertParameters["CRM_Archived"].DefaultValue = "false";

          Insert_Type(SqlDataSource_CRM_Form);

          Insert_Status(SqlDataSource_CRM_Form);

          CheckBoxList CheckBoxList_InsertComplaintCategoryItemList = (CheckBoxList)FormView_CRM_Form.FindControl("CheckBoxList_InsertComplaintCategoryItemList");
          foreach (System.Web.UI.WebControls.ListItem ListItem_ComplaintCategoryItemList in CheckBoxList_InsertComplaintCategoryItemList.Items)
          {
            if (ListItem_ComplaintCategoryItemList.Selected)
            {
              string SQLStringInsertComplaintCategory = "INSERT INTO Form_CRM_Complaint_Category ( CRM_Id ,CRM_Complaint_Category_Temp_CRM_Id ,CRM_Complaint_Category_Item_List ,CRM_Complaint_Category_CreatedDate ,CRM_Complaint_Category_CreatedBy ) VALUES ( @CRM_Id ,@CRM_Complaint_Category_Temp_CRM_Id ,@CRM_Complaint_Category_Item_List ,@CRM_Complaint_Category_CreatedDate ,@CRM_Complaint_Category_CreatedBy )";
              using (SqlCommand SqlCommand_InsertComplaintCategory = new SqlCommand(SQLStringInsertComplaintCategory))
              {
                SqlCommand_InsertComplaintCategory.Parameters.AddWithValue("@CRM_Id", DBNull.Value);
                SqlCommand_InsertComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_Temp_CRM_Id", HiddenField_InsertCRMIdTemp.Value);
                SqlCommand_InsertComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_Item_List", ListItem_ComplaintCategoryItemList.Value.ToString());
                SqlCommand_InsertComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_CreatedDate", DateTime.Now);
                SqlCommand_InsertComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertComplaintCategory);
              }
            }
          }

          Session["CRM_ReportNumber"] = "";
        }
      }
    }

    protected void Insert_Type(SqlDataSource sqlDataSource_CRM_Form)
    {
      if (sqlDataSource_CRM_Form != null)
      {
        DropDownList DropDownList_InsertTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertTypeList");
        if (DropDownList_InsertTypeList.SelectedValue.ToString() == "4395")
        {
          CheckBox CheckBox_InsertComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplaintCloseOut");
          if (CheckBox_InsertComplaintCloseOut.Checked == true)
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Complaint_CloseOutDate"].DefaultValue = DateTime.Now.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Complaint_CloseOutBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Complaint_CloseOutDate"].DefaultValue = DBNull.Value.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Complaint_CloseOutBy"].DefaultValue = DBNull.Value.ToString();
          }
        }
        else if (DropDownList_InsertTypeList.SelectedValue.ToString() == "4406")
        {
          CheckBox CheckBox_InsertComplimentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplimentAcknowledge");
          if (CheckBox_InsertComplimentAcknowledge.Checked == true)
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Compliment_AcknowledgeDate"].DefaultValue = DateTime.Now.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Compliment_AcknowledgeBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Compliment_AcknowledgeDate"].DefaultValue = DBNull.Value.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Compliment_AcknowledgeBy"].DefaultValue = DBNull.Value.ToString();
          }

          CheckBox CheckBox_InsertComplimentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplimentCloseOut");
          if (CheckBox_InsertComplimentCloseOut.Checked == true)
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Compliment_CloseOutDate"].DefaultValue = DateTime.Now.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Compliment_CloseOutBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Compliment_CloseOutDate"].DefaultValue = DBNull.Value.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Compliment_CloseOutBy"].DefaultValue = DBNull.Value.ToString();
          }
        }
        else if (DropDownList_InsertTypeList.SelectedValue.ToString() == "4412")
        {
          CheckBox CheckBox_InsertCommentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertCommentAcknowledge");
          if (CheckBox_InsertCommentAcknowledge.Checked == true)
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Comment_AcknowledgeDate"].DefaultValue = DateTime.Now.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Comment_AcknowledgeBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Comment_AcknowledgeDate"].DefaultValue = DBNull.Value.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Comment_AcknowledgeBy"].DefaultValue = DBNull.Value.ToString();
          }

          CheckBox CheckBox_InsertCommentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertCommentCloseOut");
          if (CheckBox_InsertCommentCloseOut.Checked == true)
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Comment_CloseOutDate"].DefaultValue = DateTime.Now.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Comment_CloseOutBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Comment_CloseOutDate"].DefaultValue = DBNull.Value.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Comment_CloseOutBy"].DefaultValue = DBNull.Value.ToString();
          }
        }
        else if (DropDownList_InsertTypeList.SelectedValue.ToString() == "4413")
        {
          CheckBox CheckBox_InsertQueryAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertQueryAcknowledge");
          if (CheckBox_InsertQueryAcknowledge.Checked == true)
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Query_AcknowledgeDate"].DefaultValue = DateTime.Now.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Query_AcknowledgeBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Query_AcknowledgeDate"].DefaultValue = DBNull.Value.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Query_AcknowledgeBy"].DefaultValue = DBNull.Value.ToString();
          }

          CheckBox CheckBox_InsertQueryCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertQueryCloseOut");
          if (CheckBox_InsertQueryCloseOut.Checked == true)
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Query_CloseOutDate"].DefaultValue = DateTime.Now.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Query_CloseOutBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Query_CloseOutDate"].DefaultValue = DBNull.Value.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Query_CloseOutBy"].DefaultValue = DBNull.Value.ToString();
          }
        }
        else if (DropDownList_InsertTypeList.SelectedValue.ToString() == "4414")
        {
          CheckBox CheckBox_InsertSuggestionAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertSuggestionAcknowledge");
          if (CheckBox_InsertSuggestionAcknowledge.Checked == true)
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Suggestion_AcknowledgeDate"].DefaultValue = DateTime.Now.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Suggestion_AcknowledgeBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Suggestion_AcknowledgeDate"].DefaultValue = DBNull.Value.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Suggestion_AcknowledgeBy"].DefaultValue = DBNull.Value.ToString();
          }

          CheckBox CheckBox_InsertSuggestionCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertSuggestionCloseOut");
          if (CheckBox_InsertSuggestionCloseOut.Checked == true)
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Suggestion_CloseOutDate"].DefaultValue = DateTime.Now.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Suggestion_CloseOutBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            sqlDataSource_CRM_Form.InsertParameters["CRM_Suggestion_CloseOutDate"].DefaultValue = DBNull.Value.ToString();
            sqlDataSource_CRM_Form.InsertParameters["CRM_Suggestion_CloseOutBy"].DefaultValue = DBNull.Value.ToString();
          }
        }
      }
    }

    protected void Insert_Status(SqlDataSource sqlDataSource_CRM_Form)
    {
      if (sqlDataSource_CRM_Form != null)
      {
        string CRM_Status = "Pending Approval";
        string SecurityRole = "";
        HiddenField HiddenField_InsertSecurityRole = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertSecurityRole");
        SecurityRole = HiddenField_InsertSecurityRole.Value;

        if (SecurityRole == "1" || SecurityRole == "146" || SecurityRole == "150" || SecurityRole == "148" || SecurityRole == "153" || SecurityRole == "151")
        {
          CRM_Status = "Approved";
        }

        SqlDataSource_CRM_Form.InsertParameters["CRM_Status"].DefaultValue = CRM_Status;
        SqlDataSource_CRM_Form.InsertParameters["CRM_StatusDate"].DefaultValue = DateTime.Now.ToString();
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_InsertTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertTypeList");

      if (InvalidForm == "No")
      {
        InvalidForm = InsertRequiredFields(InvalidForm);

        if (DropDownList_InsertTypeList.SelectedValue == "4395")
        {
          InvalidForm = InsertRequiredFields_Complaint(InvalidForm);
        }
        else if (DropDownList_InsertTypeList.SelectedValue == "4406")
        {
          InvalidForm = InsertRequiredFields_Compliment(InvalidForm);
        }
        else if (DropDownList_InsertTypeList.SelectedValue == "4412")
        {
          InvalidForm = InsertRequiredFields_Comment(InvalidForm);
        }
        else if (DropDownList_InsertTypeList.SelectedValue == "4413")
        {
          InvalidForm = InsertRequiredFields_Query(InvalidForm);
        }
        else if (DropDownList_InsertTypeList.SelectedValue == "4414")
        {
          InvalidForm = InsertRequiredFields_Suggestions(InvalidForm);
        }
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

    protected string InsertRequiredFields(string invalidForm)
    {
      string InvalidForm = invalidForm;

      HiddenField HiddenField_InsertAdmin = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertAdmin");
      DropDownList DropDownList_InsertFacility = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertFacility");
      TextBox TextBox_InsertDateReceived = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertDateReceived");
      DropDownList DropDownList_InsertTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertTypeList");
      DropDownList DropDownList_InsertReceivedViaList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertReceivedViaList");
      DropDownList DropDownList_InsertReceivedFromList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertReceivedFromList");

      TextBox TextBox_InsertCustomerName = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerName");
      TextBox TextBox_InsertCustomerEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerEmail");
      TextBox TextBox_InsertCustomerContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerContactNumber");

      if (string.IsNullOrEmpty(DropDownList_InsertFacility.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(TextBox_InsertDateReceived.Text))
      {
        InvalidForm = "Yes";
      }

      if (HiddenField_InsertAdmin.Value == "Yes")
      {
        DropDownList DropDownList_InsertOriginatedAtList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertOriginatedAtList");
        if (string.IsNullOrEmpty(DropDownList_InsertOriginatedAtList.SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      if (string.IsNullOrEmpty(DropDownList_InsertTypeList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_InsertReceivedViaList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_InsertReceivedFromList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (DropDownList_InsertReceivedFromList.SelectedValue.ToString() != "4396" && DropDownList_InsertReceivedFromList.SelectedValue.ToString() != "4415" && DropDownList_InsertReceivedFromList.SelectedValue.ToString() != "4798" && DropDownList_InsertReceivedFromList.SelectedValue.ToString() != "5387")
      {
        if (string.IsNullOrEmpty(TextBox_InsertCustomerName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertCustomerEmail.Text) && string.IsNullOrEmpty(TextBox_InsertCustomerContactNumber.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertRequiredFields_ReceivedFrom_Patient(string invalidForm)
    {
      string InvalidForm = invalidForm;

      DropDownList DropDownList_InsertReceivedFromList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertReceivedFromList");

      TextBox TextBox_InsertPatientVisitNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientVisitNumber");
      TextBox TextBox_InsertPatientName = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientName");
      TextBox TextBox_InsertPatientDateOfAdmission = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientDateOfAdmission");
      TextBox TextBox_InsertPatientEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientEmail");
      TextBox TextBox_InsertPatientContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientContactNumber");

      if (DropDownList_InsertReceivedFromList.SelectedValue.ToString() == "4396")
      {
        if (string.IsNullOrEmpty(TextBox_InsertPatientVisitNumber.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertPatientName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertPatientDateOfAdmission.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertPatientEmail.Text) && string.IsNullOrEmpty(TextBox_InsertPatientContactNumber.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertRequiredFields_Complaint(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_InsertComplaintDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintDescription");
      DropDownList DropDownList_InsertComplaintUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintUnitId");
      TextBox TextBox_InsertComplaintDateOccurred = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintDateOccurred");
      DropDownList DropDownList_InsertComplaintTimeOccuredHours = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintTimeOccuredHours");
      DropDownList DropDownList_InsertComplaintTimeOccuredMinutes = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintTimeOccuredMinutes");
      DropDownList DropDownList_InsertComplaintPriorityList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintPriorityList");
      CheckBoxList CheckBoxList_InsertComplaintCategoryItemList = (CheckBoxList)FormView_CRM_Form.FindControl("CheckBoxList_InsertComplaintCategoryItemList");
      DropDownList DropDownList_InsertComplaintCustomerSatisfied = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintCustomerSatisfied");
      TextBox TextBox_InsertComplaintInvestigatorName = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintInvestigatorName");
      TextBox TextBox_InsertComplaintInvestigatorDesignation = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintInvestigatorDesignation");
      TextBox TextBox_InsertComplaintRootCause = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintRootCause");
      CheckBox CheckBox_InsertComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplaintCloseOut");

      if (CheckBox_InsertComplaintCloseOut.Checked == true)
      {
        InvalidForm = InsertRequiredFields_ReceivedFrom_Patient(InvalidForm);

        if (string.IsNullOrEmpty(TextBox_InsertComplaintDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertComplaintUnitId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertComplaintDateOccurred.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertComplaintTimeOccuredHours.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertComplaintTimeOccuredMinutes.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertComplaintPriorityList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        string ComplaintCategoryCompleted = "No";
        foreach (System.Web.UI.WebControls.ListItem ListItem_ComplaintCategoryItemList in CheckBoxList_InsertComplaintCategoryItemList.Items)
        {
          if (ListItem_ComplaintCategoryItemList.Selected == true)
          {
            ComplaintCategoryCompleted = "Yes";
            break;
          }
          else if (ListItem_ComplaintCategoryItemList.Selected == false)
          {
            ComplaintCategoryCompleted = "No";
          }
        }

        if (ComplaintCategoryCompleted == "No")
        {
          InvalidForm = "Yes";
        }

        InvalidForm = InsertRequiredFields_Complaint_CompletedWithin(InvalidForm);

        if (string.IsNullOrEmpty(DropDownList_InsertComplaintCustomerSatisfied.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertComplaintInvestigatorName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertComplaintInvestigatorDesignation.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertComplaintRootCause.Text))
        {
          InvalidForm = "Yes";
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_InsertComplaintDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertRequiredFields_Complaint_CompletedWithin(string invalidForm)
    {
      string InvalidForm = invalidForm;

      CheckBox CheckBox_InsertEscalatedForm = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertEscalatedForm");

      DropDownList DropDownList_InsertComplaintWithin24Hours = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintWithin24Hours");
      DropDownList DropDownList_InsertComplaintWithin5Days = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintWithin5Days");
      DropDownList DropDownList_InsertComplaintWithin10Days = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintWithin10Days");
      TextBox TextBox_InsertComplaintWithin10DaysReason = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin10DaysReason");
      DropDownList DropDownList_InsertComplaintWithin3Days = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintWithin3Days");
      TextBox TextBox_InsertComplaintWithin3DaysReason = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintWithin3DaysReason");

      if (string.IsNullOrEmpty(DropDownList_InsertComplaintWithin24Hours.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (CheckBox_InsertEscalatedForm.Checked == true)
      {
        if (string.IsNullOrEmpty(DropDownList_InsertComplaintWithin3Days.SelectedValue))
        {
          InvalidForm = "Yes";
        }
        else
        {
          if (DropDownList_InsertComplaintWithin3Days.SelectedValue.ToString() == "No")
          {
            if (string.IsNullOrEmpty(TextBox_InsertComplaintWithin3DaysReason.Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(DropDownList_InsertComplaintWithin5Days.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertComplaintWithin10Days.SelectedValue))
        {
          InvalidForm = "Yes";
        }
        else
        {
          if (DropDownList_InsertComplaintWithin10Days.SelectedValue.ToString() == "No")
          {
            if (string.IsNullOrEmpty(TextBox_InsertComplaintWithin10DaysReason.Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string InsertRequiredFields_Compliment(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_InsertComplimentDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplimentDescription");
      DropDownList DropDownList_InsertComplimentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplimentUnitId");
      CheckBox CheckBox_InsertComplimentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplimentAcknowledge");
      CheckBox CheckBox_InsertComplimentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplimentCloseOut");

      if (CheckBox_InsertComplimentAcknowledge.Checked == true)
      {
        if (CheckBox_InsertComplimentCloseOut.Checked == true)
        {
          InvalidForm = InsertRequiredFields_ReceivedFrom_Patient(InvalidForm);

          if (string.IsNullOrEmpty(TextBox_InsertComplimentDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_InsertComplimentUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else
        {
          if (string.IsNullOrEmpty(TextBox_InsertComplimentDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_InsertComplimentDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertRequiredFields_Comment(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_InsertCommentDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCommentDescription");
      DropDownList DropDownList_InsertCommentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentUnitId");
      DropDownList DropDownList_InsertCommentTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentTypeList");
      CheckBox CheckBox_InsertCommentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertCommentAcknowledge");
      CheckBox CheckBox_InsertCommentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertCommentCloseOut");

      if (CheckBox_InsertCommentAcknowledge.Checked == true)
      {
        if (CheckBox_InsertCommentCloseOut.Checked == true)
        {
          InvalidForm = InsertRequiredFields_ReceivedFrom_Patient(InvalidForm);

          if (string.IsNullOrEmpty(TextBox_InsertCommentDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_InsertCommentUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_InsertCommentTypeList.Text))
          {
            InvalidForm = "Yes";
          }
        }
        else
        {
          if (string.IsNullOrEmpty(TextBox_InsertCommentDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_InsertCommentDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertRequiredFields_Query(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_InsertQueryDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertQueryDescription");
      DropDownList DropDownList_InsertQueryUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertQueryUnitId");
      CheckBox CheckBox_InsertQueryAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertQueryAcknowledge");
      CheckBox CheckBox_InsertQueryCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertQueryCloseOut");

      if (CheckBox_InsertQueryAcknowledge.Checked == true)
      {
        if (CheckBox_InsertQueryCloseOut.Checked == true)
        {
          InvalidForm = InsertRequiredFields_ReceivedFrom_Patient(InvalidForm);

          if (string.IsNullOrEmpty(TextBox_InsertQueryDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_InsertQueryUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else
        {
          if (string.IsNullOrEmpty(TextBox_InsertQueryDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_InsertQueryDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertRequiredFields_Suggestions(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_InsertSuggestionDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertSuggestionDescription");
      DropDownList DropDownList_InsertSuggestionUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertSuggestionUnitId");
      CheckBox CheckBox_InsertSuggestionAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertSuggestionAcknowledge");
      CheckBox CheckBox_InsertSuggestionCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertSuggestionCloseOut");

      if (CheckBox_InsertSuggestionAcknowledge.Checked == true)
      {
        if (CheckBox_InsertSuggestionCloseOut.Checked == true)
        {
          InvalidForm = InsertRequiredFields_ReceivedFrom_Patient(InvalidForm);

          if (string.IsNullOrEmpty(TextBox_InsertSuggestionDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_InsertSuggestionUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else
        {
          if (string.IsNullOrEmpty(TextBox_InsertSuggestionDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_InsertSuggestionDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string InsertFieldValidation()
    {
      string InvalidFormMessage = "";

      TextBox TextBox_InsertDateReceived = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertDateReceived");
      DropDownList DropDownList_InsertTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertTypeList");
      TextBox TextBox_InsertComplaintDateOccurred = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintDateOccurred");
      DropDownList DropDownList_InsertComplaintTimeOccuredHours = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintTimeOccuredHours");
      DropDownList DropDownList_InsertComplaintTimeOccuredMinutes = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintTimeOccuredMinutes");

      string DateToValidateDateReceived = TextBox_InsertDateReceived.Text.ToString();
      DateTime ValidatedDateDateReceived = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDateReceived);

      if (ValidatedDateDateReceived.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + "Date Received is not in the correct format, date must be in the format yyyy/mm/dd<br />";
      }
      else
      {
        DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertDateReceived")).Text, CultureInfo.CurrentCulture);
        DateTime CurrentDate = DateTime.Now;

        if (PickedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
        }
      }

      if (DropDownList_InsertTypeList.SelectedValue == "4395")
      {
        if (!string.IsNullOrEmpty(TextBox_InsertComplaintDateOccurred.Text))
        {
          string DateToValidateComplaintDateOccurred = TextBox_InsertComplaintDateOccurred.Text.ToString();
          DateTime ValidatedDateComplaintDateOccurred = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateComplaintDateOccurred);

          if (ValidatedDateComplaintDateOccurred.ToString() == "0001/01/01 12:00:00 AM")
          {
            InvalidFormMessage = InvalidFormMessage + "Date Incident Occurred is not in the correct format, date must be in the format yyyy/mm/dd<br />";
          }
          else
          {
            DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_CRM_Form.FindControl("TextBox_InsertDateReceived")).Text, CultureInfo.CurrentCulture);
            DateTime CurrentDate = DateTime.Now;

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
            }
          }
        }

        if (!string.IsNullOrEmpty(DropDownList_InsertComplaintTimeOccuredHours.SelectedValue))
        {
          Int32 Hours = Convert.ToInt32(DropDownList_InsertComplaintTimeOccuredHours.SelectedValue, CultureInfo.CurrentCulture);

          if (Hours < 0 || Hours > 23)
          {
            InvalidFormMessage = InvalidFormMessage + "Hours is not in the correct format, Hours must be between 0 and 23<br />";
          }
        }

        if (!string.IsNullOrEmpty(DropDownList_InsertComplaintTimeOccuredMinutes.SelectedValue))
        {
          Int32 Minutes = Convert.ToInt32(DropDownList_InsertComplaintTimeOccuredMinutes.SelectedValue, CultureInfo.CurrentCulture);

          if (Minutes < 0 || Minutes > 59)
          {
            InvalidFormMessage = InvalidFormMessage + "Minutes is not in the correct format, Minutes must be between 0 and 59<br />";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_CRM_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["CRM_Id"] = e.Command.Parameters["@CRM_Id"].Value;
        Session["CRM_ReportNumber"] = e.Command.Parameters["@CRM_ReportNumber"].Value;

        HiddenField HiddenField_InsertCRMIdTemp = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertCRMIdTemp");

        string SQLStringUpdateFile = "UPDATE Form_CRM_File SET CRM_Id = @CRM_Id, CRM_File_Temp_CRM_Id = NULL WHERE CRM_File_Temp_CRM_Id = @CRM_File_Temp_CRM_Id";
        using (SqlCommand SqlCommand_UpdateFile = new SqlCommand(SQLStringUpdateFile))
        {
          SqlCommand_UpdateFile.Parameters.AddWithValue("@CRM_Id", Session["CRM_Id"].ToString());
          SqlCommand_UpdateFile.Parameters.AddWithValue("@CRM_File_Temp_CRM_Id", HiddenField_InsertCRMIdTemp.Value.ToString());

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateFile);
        }

        string SQLStringUpdateComplaintCategory = "UPDATE Form_CRM_Complaint_Category SET CRM_Id = @CRM_Id, CRM_Complaint_Category_Temp_CRM_Id = NULL WHERE CRM_Complaint_Category_Temp_CRM_Id = @CRM_Complaint_Category_Temp_CRM_Id";
        using (SqlCommand SqlCommand_UpdateComplaintCategory = new SqlCommand(SQLStringUpdateComplaintCategory))
        {
          SqlCommand_UpdateComplaintCategory.Parameters.AddWithValue("@CRM_Id", Session["CRM_Id"].ToString());
          SqlCommand_UpdateComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_Temp_CRM_Id", HiddenField_InsertCRMIdTemp.Value.ToString());

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateComplaintCategory);
        }

        if (!string.IsNullOrEmpty(Session["CRM_Id"].ToString()))
        {
          HiddenField HiddenField_InsertAdmin = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertAdmin");
          if (HiddenField_InsertAdmin.Value == "Yes")
          {
            Session["EmailTemplate"] = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("25");

            Session["URLAuthority"] = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();

            Session["FormName"] = InfoQuestWCF.InfoQuest_All.All_FormName("36");

            Session["CRMId"] = "";
            Session["FacilityFacilityDisplayName"] = "";
            Session["CRMReportNumber"] = "";
            Session["CRMTypeName"] = "";
            string SQLStringCRM = "SELECT CRM_Id, Facility_FacilityDisplayName, CRM_ReportNumber , CRM_Type_Name FROM vForm_CRM WHERE CRM_Id = @CRM_Id";
            using (SqlCommand SqlCommand_CRM = new SqlCommand(SQLStringCRM))
            {
              SqlCommand_CRM.Parameters.AddWithValue("@CRM_Id", Session["CRM_Id"]);
              DataTable DataTable_CRM;
              using (DataTable_CRM = new DataTable())
              {
                DataTable_CRM.Locale = CultureInfo.CurrentCulture;
                DataTable_CRM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRM).Copy();
                if (DataTable_CRM.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_CRM.Rows)
                  {
                    Session["CRMId"] = DataRow_Row["CRM_Id"];
                    Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
                    Session["CRMReportNumber"] = DataRow_Row["CRM_ReportNumber"];
                    Session["CRMTypeName"] = DataRow_Row["CRM_Type_Name"];
                  }
                }
              }
            }

            string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();

            string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();

            Session["SecurityUserDisplayName"] = "";
            Session["SecurityUserEmail"] = "";
            string SQLStringEmailTo = "SELECT ISNULL(SecurityUser_DisplayName,'') AS SecurityUser_DisplayName, ISNULL(SecurityUser_Email,'') AS SecurityUser_Email FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('36') AND SecurityRole_Id IN ('148') AND Facility_Id IN (SELECT Facility_Id FROM Form_CRM WHERE CRM_Id = @CRM_Id) AND SecurityUser_Email IS NOT NULL";
            using (SqlCommand SqlCommand_EmailTo = new SqlCommand(SQLStringEmailTo))
            {
              SqlCommand_EmailTo.Parameters.AddWithValue("@CRM_Id", Session["CRM_Id"]);
              DataTable DataTable_EmailTo;
              using (DataTable_EmailTo = new DataTable())
              {
                DataTable_EmailTo.Locale = CultureInfo.CurrentCulture;
                DataTable_EmailTo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailTo).Copy();
                if (DataTable_EmailTo.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_EmailTo.Rows)
                  {
                    Session["SecurityUserDisplayName"] = DataRow_Row["SecurityUser_DisplayName"];
                    Session["SecurityUserEmail"] = DataRow_Row["SecurityUser_Email"];

                    string BodyString = Session["EmailTemplate"].ToString();

                    BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + Session["SecurityUserDisplayName"].ToString() + "");
                    BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + Session["FormName"].ToString() + "");
                    BodyString = BodyString.Replace(";replace;FacilityDisplayName;replace;", "" + Session["FacilityFacilityDisplayName"].ToString() + "");
                    BodyString = BodyString.Replace(";replace;CRMId;replace;", "" + Session["CRMId"].ToString() + "");
                    BodyString = BodyString.Replace(";replace;CRMReportNumber;replace;", "" + Session["CRMReportNumber"].ToString() + "");
                    BodyString = BodyString.Replace(";replace;CRMTypeName;replace;", "" + Session["CRMTypeName"].ToString() + "");
                    BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + Session["URLAuthority"].ToString() + "");

                    Session["EmailBody"] = HeaderString + BodyString + FooterString;

                    Session["EmailSend"] = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", Session["SecurityUserEmail"].ToString(), Session["FormName"].ToString(), Session["EmailBody"].ToString());

                    if (Session["EmailSend"].ToString() == "Yes")
                    {
                      Session["EmailBody"] = "";
                    }
                    else
                    {
                      Session["EmailBody"] = "";
                    }

                    Session["EmailSend"] = "";
                    Session["SecurityUserDisplayName"] = "";
                    Session["SecurityUserEmail"] = "";
                  }
                }
                else
                {
                  Session["SecurityUserDisplayName"] = "";
                  Session["SecurityUserEmail"] = "";
                }
              }
            }

            Session["SecurityUserDisplayName"] = "";
            Session["SecurityUserEmail"] = "";

            Session["EmailTemplate"] = "";
            Session["URLAuthority"] = "";
            Session["FormName"] = "";
            Session["CRMId"] = "";
            Session["FacilityFacilityDisplayName"] = "";
            Session["CRMReportNumber"] = "";
            Session["CRMTypeName"] = "";
          }
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_CRM&ReportNumber=" + Session["CRM_ReportNumber"].ToString() + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    protected void FormView_CRM_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDCRMModifiedDate"] = e.OldValues["CRM_ModifiedDate"];
        object OLDCRMModifiedDate = Session["OLDCRMModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDCRMModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareCRM = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareCRM = DataView_CompareCRM[0];
        Session["DBCRMModifiedDate"] = Convert.ToString(DataRowView_CompareCRM["CRM_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBCRMModifiedBy"] = Convert.ToString(DataRowView_CompareCRM["CRM_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBCRMModifiedDate = Session["DBCRMModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBCRMModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_CRM.SetFocus(UpdatePanel_CRM);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBCRMModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_CRM_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_CRM_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;

          EditRegisterPostBackControl();
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_CRM.SetFocus(UpdatePanel_CRM);
            ((Label)FormView_CRM_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_CRM_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";

            EditRegisterPostBackControl();
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
            e.NewValues["CRM_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_CRM", "CRM_Id = " + Request.QueryString["CRM_Id"]);

            DataView DataView_CRM = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_CRM = DataView_CRM[0];
            Session["CRMHistory"] = Convert.ToString(DataRowView_CRM["CRM_History"], CultureInfo.CurrentCulture);

            Session["CRMHistory"] = Session["History"].ToString() + Session["CRMHistory"].ToString();
            e.NewValues["CRM_History"] = Session["CRMHistory"].ToString();

            Session["CRMHistory"] = "";
            Session["History"] = "";


            DropDownList DropDownList_EditOriginatedAtList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditOriginatedAtList");
            if (DropDownList_EditOriginatedAtList.Visible == false)
            {
              e.NewValues["CRM_OriginatedAt_List"] = e.OldValues["CRM_OriginatedAt_List"];
            }
            else
            {
              e.NewValues["CRM_OriginatedAt_List"] = DropDownList_EditOriginatedAtList.SelectedValue;
            }


            DropDownList DropDownList_EditComplaintUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintUnitId");
            DropDownList DropDownList_EditComplimentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplimentUnitId");
            DropDownList DropDownList_EditCommentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentUnitId");
            DropDownList DropDownList_EditCommentAdditionalUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentAdditionalUnitId");
            DropDownList DropDownList_EditQueryUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditQueryUnitId");
            DropDownList DropDownList_EditSuggestionUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditSuggestionUnitId");
            e.NewValues["CRM_Complaint_Unit_Id"] = DropDownList_EditComplaintUnitId.SelectedValue;
            e.NewValues["CRM_Compliment_Unit_Id"] = DropDownList_EditComplimentUnitId.SelectedValue;
            e.NewValues["CRM_Comment_Unit_Id"] = DropDownList_EditCommentUnitId.SelectedValue;
            e.NewValues["CRM_Comment_AdditionalUnit_Id"] = DropDownList_EditCommentAdditionalUnitId.SelectedValue;
            e.NewValues["CRM_Query_Unit_Id"] = DropDownList_EditQueryUnitId.SelectedValue;
            e.NewValues["CRM_Suggestion_Unit_Id"] = DropDownList_EditSuggestionUnitId.SelectedValue;


            DropDownList DropDownList_EditTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditTypeList");
            if (DropDownList_EditTypeList.SelectedValue.ToString() == "4395")
            {
              EditType_Complaint(e);
            }
            else if (DropDownList_EditTypeList.SelectedValue.ToString() == "4406")
            {
              EditType_Compliment(e);
            }
            else if (DropDownList_EditTypeList.SelectedValue.ToString() == "4412")
            {
              EditType_Comment(e);
            }
            else if (DropDownList_EditTypeList.SelectedValue.ToString() == "4413")
            {
              EditType_Query(e);
            }
            else if (DropDownList_EditTypeList.SelectedValue.ToString() == "4414")
            {
              EditType_Suggestions(e);
            }


            string DBStatus = e.OldValues["CRM_Status"].ToString();
            DropDownList DropDownList_EditStatus = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditStatus");
            e.NewValues["CRM_Status"] = DropDownList_EditStatus.SelectedValue;

            if (DBStatus != DropDownList_EditStatus.SelectedValue)
            {
              if (DBStatus == "Pending Approval")
              {
                e.NewValues["CRM_StatusDate"] = DateTime.Now.ToString();
              }
            }


            string SQLStringCRMComplaintCategory = "DELETE FROM Form_CRM_Complaint_Category WHERE CRM_Id = @CRM_Id";
            using (SqlCommand SqlCommand_CRMComplaintCategory = new SqlCommand(SQLStringCRMComplaintCategory))
            {
              SqlCommand_CRMComplaintCategory.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);

              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_CRMComplaintCategory);
            }


            if (DropDownList_EditTypeList.SelectedValue.ToString() == "4395")
            {
              CheckBoxList CheckBoxList_EditComplaintCategoryItemList = (CheckBoxList)FormView_CRM_Form.FindControl("CheckBoxList_EditComplaintCategoryItemList");
              foreach (System.Web.UI.WebControls.ListItem ListItem_ComplaintCategoryItemList in CheckBoxList_EditComplaintCategoryItemList.Items)
              {
                if (ListItem_ComplaintCategoryItemList.Selected)
                {
                  string SQLStringInsertCRMComplaintCategory = "INSERT INTO Form_CRM_Complaint_Category ( CRM_Id ,CRM_Complaint_Category_Temp_CRM_Id ,CRM_Complaint_Category_Item_List ,CRM_Complaint_Category_CreatedDate ,CRM_Complaint_Category_CreatedBy ) VALUES ( @CRM_Id ,@CRM_Complaint_Category_Temp_CRM_Id ,@CRM_Complaint_Category_Item_List ,@CRM_Complaint_Category_CreatedDate ,@CRM_Complaint_Category_CreatedBy )";
                  using (SqlCommand SqlCommand_InsertCRMComplaintCategory = new SqlCommand(SQLStringInsertCRMComplaintCategory))
                  {
                    SqlCommand_InsertCRMComplaintCategory.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
                    SqlCommand_InsertCRMComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_Temp_CRM_Id", DBNull.Value);
                    SqlCommand_InsertCRMComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_Item_List", ListItem_ComplaintCategoryItemList.Value.ToString());
                    SqlCommand_InsertCRMComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_CreatedDate", DateTime.Now);
                    SqlCommand_InsertCRMComplaintCategory.Parameters.AddWithValue("@CRM_Complaint_Category_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMComplaintCategory);
                  }
                }
              }
            }


            EditRoute();
          }
        }
      }
    }

    protected void EditType_Complaint(FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        if (e.OldValues["CRM_Complaint_CloseOut"].ToString() == "False")
        {
          CheckBox CheckBox_EditComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplaintCloseOut");
          if (CheckBox_EditComplaintCloseOut.Checked == true)
          {
            e.NewValues["CRM_Complaint_CloseOutDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_Complaint_CloseOutBy"] = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            e.NewValues["CRM_Complaint_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Complaint_CloseOutBy"] = DBNull.Value.ToString();
          }
        }
        else
        {
          CheckBox CheckBox_EditComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplaintCloseOut");
          if (CheckBox_EditComplaintCloseOut.Checked == false)
          {
            e.NewValues["CRM_Complaint_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Complaint_CloseOutBy"] = DBNull.Value.ToString();
          }
        }

        e.NewValues["CRM_Compliment_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_CloseOutBy"] = DBNull.Value.ToString();
      }
    }

    protected void EditType_Compliment(FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        e.NewValues["CRM_Complaint_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Complaint_CloseOutBy"] = DBNull.Value.ToString();

        if (e.OldValues["CRM_Compliment_Acknowledge"].ToString() == "False")
        {
          CheckBox CheckBox_EditComplimentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentAcknowledge");
          if (CheckBox_EditComplimentAcknowledge.Checked == true)
          {
            e.NewValues["CRM_Compliment_AcknowledgeDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_Compliment_AcknowledgeBy"] = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            e.NewValues["CRM_Compliment_AcknowledgeDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Compliment_AcknowledgeBy"] = DBNull.Value.ToString();
          }
        }
        else
        {
          CheckBox CheckBox_EditComplimentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentAcknowledge");
          if (CheckBox_EditComplimentAcknowledge.Checked == false)
          {
            e.NewValues["CRM_Compliment_AcknowledgeDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Compliment_AcknowledgeBy"] = DBNull.Value.ToString();
          }
        }

        if (e.OldValues["CRM_Compliment_CloseOut"].ToString() == "False")
        {
          CheckBox CheckBox_EditComplimentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentCloseOut");
          if (CheckBox_EditComplimentCloseOut.Checked == true)
          {
            e.NewValues["CRM_Compliment_CloseOutDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_Compliment_CloseOutBy"] = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            e.NewValues["CRM_Compliment_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Compliment_CloseOutBy"] = DBNull.Value.ToString();
          }
        }
        else
        {
          CheckBox CheckBox_EditComplimentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentCloseOut");
          if (CheckBox_EditComplimentCloseOut.Checked == false)
          {
            e.NewValues["CRM_Compliment_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Compliment_CloseOutBy"] = DBNull.Value.ToString();
          }
        }

        e.NewValues["CRM_Comment_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_CloseOutBy"] = DBNull.Value.ToString();
      }
    }

    protected void EditType_Comment(FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        e.NewValues["CRM_Complaint_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Complaint_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_CloseOutBy"] = DBNull.Value.ToString();

        if (e.OldValues["CRM_Comment_Acknowledge"].ToString() == "False")
        {
          CheckBox CheckBox_EditCommentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentAcknowledge");
          if (CheckBox_EditCommentAcknowledge.Checked == true)
          {
            e.NewValues["CRM_Comment_AcknowledgeDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_Comment_AcknowledgeBy"] = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            e.NewValues["CRM_Comment_AcknowledgeDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Comment_AcknowledgeBy"] = DBNull.Value.ToString();
          }
        }
        else
        {
          CheckBox CheckBox_EditCommentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentAcknowledge");
          if (CheckBox_EditCommentAcknowledge.Checked == false)
          {
            e.NewValues["CRM_Comment_AcknowledgeDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Comment_AcknowledgeBy"] = DBNull.Value.ToString();
          }
        }

        if (e.OldValues["CRM_Comment_CloseOut"].ToString() == "False")
        {
          CheckBox CheckBox_EditCommentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentCloseOut");
          if (CheckBox_EditCommentCloseOut.Checked == true)
          {
            e.NewValues["CRM_Comment_CloseOutDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_Comment_CloseOutBy"] = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            e.NewValues["CRM_Comment_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Comment_CloseOutBy"] = DBNull.Value.ToString();
          }
        }
        else
        {
          CheckBox CheckBox_EditCommentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentCloseOut");
          if (CheckBox_EditCommentCloseOut.Checked == false)
          {
            e.NewValues["CRM_Comment_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Comment_CloseOutBy"] = DBNull.Value.ToString();
          }
        }

        e.NewValues["CRM_Query_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_CloseOutBy"] = DBNull.Value.ToString();
      }
    }

    protected void EditType_Query(FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        e.NewValues["CRM_Complaint_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Complaint_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_CloseOutBy"] = DBNull.Value.ToString();

        if (e.OldValues["CRM_Query_Acknowledge"].ToString() == "False")
        {
          CheckBox CheckBox_EditQueryAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryAcknowledge");
          if (CheckBox_EditQueryAcknowledge.Checked == true)
          {
            e.NewValues["CRM_Query_AcknowledgeDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_Query_AcknowledgeBy"] = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            e.NewValues["CRM_Query_AcknowledgeDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Query_AcknowledgeBy"] = DBNull.Value.ToString();
          }
        }
        else
        {
          CheckBox CheckBox_EditQueryAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryAcknowledge");
          if (CheckBox_EditQueryAcknowledge.Checked == false)
          {
            e.NewValues["CRM_Query_AcknowledgeDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Query_AcknowledgeBy"] = DBNull.Value.ToString();
          }
        }

        if (e.OldValues["CRM_Query_CloseOut"].ToString() == "False")
        {
          CheckBox CheckBox_EditQueryCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryCloseOut");
          if (CheckBox_EditQueryCloseOut.Checked == true)
          {
            e.NewValues["CRM_Query_CloseOutDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_Query_CloseOutBy"] = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            e.NewValues["CRM_Query_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Query_CloseOutBy"] = DBNull.Value.ToString();
          }
        }
        else
        {
          CheckBox CheckBox_EditQueryCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryCloseOut");
          if (CheckBox_EditQueryCloseOut.Checked == false)
          {
            e.NewValues["CRM_Query_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Query_CloseOutBy"] = DBNull.Value.ToString();
          }
        }

        e.NewValues["CRM_Suggestion_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Suggestion_CloseOutBy"] = DBNull.Value.ToString();
      }
    }

    protected void EditType_Suggestions(FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        e.NewValues["CRM_Complaint_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Complaint_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Compliment_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Comment_CloseOutBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_AcknowledgeDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_AcknowledgeBy"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_CloseOutDate"] = DBNull.Value.ToString();
        e.NewValues["CRM_Query_CloseOutBy"] = DBNull.Value.ToString();

        if (e.OldValues["CRM_Suggestion_Acknowledge"].ToString() == "False")
        {
          CheckBox CheckBox_EditSuggestionAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionAcknowledge");
          if (CheckBox_EditSuggestionAcknowledge.Checked == true)
          {
            e.NewValues["CRM_Suggestion_AcknowledgeDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_Suggestion_AcknowledgeBy"] = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            e.NewValues["CRM_Suggestion_AcknowledgeDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Suggestion_AcknowledgeBy"] = DBNull.Value.ToString();
          }
        }
        else
        {
          CheckBox CheckBox_EditSuggestionAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionAcknowledge");
          if (CheckBox_EditSuggestionAcknowledge.Checked == false)
          {
            e.NewValues["CRM_Suggestion_AcknowledgeDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Suggestion_AcknowledgeBy"] = DBNull.Value.ToString();
          }
        }

        if (e.OldValues["CRM_Suggestion_CloseOut"].ToString() == "False")
        {
          CheckBox CheckBox_EditSuggestionCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionCloseOut");
          if (CheckBox_EditSuggestionCloseOut.Checked == true)
          {
            e.NewValues["CRM_Suggestion_CloseOutDate"] = DateTime.Now.ToString();
            e.NewValues["CRM_Suggestion_CloseOutBy"] = Request.ServerVariables["LOGON_USER"];
          }
          else
          {
            e.NewValues["CRM_Suggestion_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Suggestion_CloseOutBy"] = DBNull.Value.ToString();
          }
        }
        else
        {
          CheckBox CheckBox_EditSuggestionCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionCloseOut");
          if (CheckBox_EditSuggestionCloseOut.Checked == false)
          {
            e.NewValues["CRM_Suggestion_CloseOutDate"] = DBNull.Value.ToString();
            e.NewValues["CRM_Suggestion_CloseOutBy"] = DBNull.Value.ToString();
          }
        }
      }
    }

    protected void EditRoute()
    {
      CheckBox CheckBox_EditRouteRoute = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteRoute");
      DropDownList DropDownList_EditRouteFacility = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditRouteFacility");
      RadioButtonList RadioButtonList_EditRouteUnit = (RadioButtonList)FormView_CRM_Form.FindControl("RadioButtonList_EditRouteUnit");
      TextBox TextBox_EditRouteComment = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditRouteComment");
      CheckBox CheckBox_EditRouteComplete = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteComplete");

      FromDataBase_IsRouteComplete FromDataBase_IsRouteComplete_Current = GetIsRouteComplete();
      string IsRouteComplete = FromDataBase_IsRouteComplete_Current.IsRouteComplete;

      FromDataBase_CRMRouteValues FromDataBase_CRMRouteValues_Current = GetCRMRouteValues();
      string CRMRouteId = FromDataBase_CRMRouteValues_Current.CRMRouteId;
      string CRMRouteHistory = FromDataBase_CRMRouteValues_Current.CRMRouteHistory;

      if (IsRouteComplete == "Yes")
      {
        if (CheckBox_EditRouteRoute.Checked == true)
        {
          string SQLStringInsertCRMRoute = "INSERT INTO Form_CRM_Route ( CRM_Id , CRM_Route_Route , Facility_Id , CRM_Route_ToUnit_List , CRM_Route_Comment , CRM_Route_Complete , CRM_Route_CompleteDate , CRM_Route_CreatedDate , CRM_Route_CreatedBy , CRM_Route_ModifiedDate , CRM_Route_ModifiedBy , CRM_Route_History ) VALUES ( @CRM_Id , @CRM_Route_Route , @Facility_Id , @CRM_Route_ToUnit_List , @CRM_Route_Comment , @CRM_Route_Complete , @CRM_Route_CompleteDate , @CRM_Route_CreatedDate , @CRM_Route_CreatedBy , @CRM_Route_ModifiedDate , @CRM_Route_ModifiedBy , @CRM_Route_History )";
          using (SqlCommand SqlCommand_InsertCRMRoute = new SqlCommand(SQLStringInsertCRMRoute))
          {
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_Route", CheckBox_EditRouteRoute.Checked);
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@Facility_Id", DropDownList_EditRouteFacility.SelectedValue);
            if (string.IsNullOrEmpty(RadioButtonList_EditRouteUnit.SelectedValue.ToString()))
            {
              SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_ToUnit_List", DBNull.Value);
            }
            else
            {
              SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_ToUnit_List", RadioButtonList_EditRouteUnit.SelectedValue);
            }
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_Comment", TextBox_EditRouteComment.Text);
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_Complete", false);
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_CompleteDate", DBNull.Value);
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_CreatedDate", DateTime.Now);
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_CreatedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_ModifiedDate", DateTime.Now);
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_InsertCRMRoute.Parameters.AddWithValue("@CRM_Route_History", DBNull.Value);

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertCRMRoute);
          }
        }
      }
      else
      {
        string SQLStringUpdateCRMRoute = "UPDATE Form_CRM_Route SET CRM_Route_Comment = @CRM_Route_Comment , CRM_Route_Complete = @CRM_Route_Complete , CRM_Route_CompleteDate = @CRM_Route_CompleteDate , CRM_Route_ModifiedDate = @CRM_Route_ModifiedDate , CRM_Route_ModifiedBy = @CRM_Route_ModifiedBy , CRM_Route_History = @CRM_Route_History WHERE CRM_Route_Id = @CRM_Route_Id";
        using (SqlCommand SqlCommand_UpdateCRMRoute = new SqlCommand(SQLStringUpdateCRMRoute))
        {

          SqlCommand_UpdateCRMRoute.Parameters.AddWithValue("@CRM_Route_Comment", TextBox_EditRouteComment.Text);
          SqlCommand_UpdateCRMRoute.Parameters.AddWithValue("@CRM_Route_Complete", CheckBox_EditRouteComplete.Checked);

          if (CheckBox_EditRouteComplete.Checked == true)
          {
            SqlCommand_UpdateCRMRoute.Parameters.AddWithValue("@CRM_Route_CompleteDate", DateTime.Now);
          }
          else
          {
            SqlCommand_UpdateCRMRoute.Parameters.AddWithValue("@CRM_Route_CompleteDate", DBNull.Value);
          }

          SqlCommand_UpdateCRMRoute.Parameters.AddWithValue("@CRM_Route_ModifiedDate", DateTime.Now);
          SqlCommand_UpdateCRMRoute.Parameters.AddWithValue("@CRM_Route_ModifiedBy", Request.ServerVariables["LOGON_USER"]);

          string History = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_CRM_Route", "CRM_Route_Id = " + CRMRouteId);
          string CRMHistory = CRMRouteHistory.ToString();
          CRMHistory = History + CRMHistory;
          SqlCommand_UpdateCRMRoute.Parameters.AddWithValue("@CRM_Route_History", CRMHistory);

          SqlCommand_UpdateCRMRoute.Parameters.AddWithValue("@CRM_Route_Id", CRMRouteId);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateCRMRoute);
        }
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditTypeList");

      if (InvalidForm == "No")
      {
        InvalidForm = EditRequiredFields(InvalidForm);

        if (DropDownList_EditTypeList.SelectedValue == "4395")
        {
          InvalidForm = EditRequiredFields_Complaint(InvalidForm);
        }
        else if (DropDownList_EditTypeList.SelectedValue == "4406")
        {
          InvalidForm = EditRequiredFields_Compliment(InvalidForm);
        }
        else if (DropDownList_EditTypeList.SelectedValue == "4412")
        {
          InvalidForm = EditRequiredFields_Comment(InvalidForm);
        }
        else if (DropDownList_EditTypeList.SelectedValue == "4413")
        {
          InvalidForm = EditRequiredFields_Query(InvalidForm);
        }
        else if (DropDownList_EditTypeList.SelectedValue == "4414")
        {
          InvalidForm = EditRequiredFields_Suggestions(InvalidForm);
        }

        InvalidForm = EditRequiredFields_Route(InvalidForm);
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

    protected string EditRequiredFields(string invalidForm)
    {
      string InvalidForm = invalidForm;

      HiddenField HiddenField_EditAdmin = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_EditAdmin");
      TextBox TextBox_EditDateReceived = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditDateReceived");
      DropDownList DropDownList_EditTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditTypeList");
      DropDownList DropDownList_EditReceivedViaList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditReceivedViaList");
      DropDownList DropDownList_EditReceivedFromList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditReceivedFromList");

      TextBox TextBox_EditCustomerName = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerName");
      TextBox TextBox_EditCustomerEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerEmail");
      TextBox TextBox_EditCustomerContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerContactNumber");

      if (string.IsNullOrEmpty(TextBox_EditDateReceived.Text))
      {
        InvalidForm = "Yes";
      }

      if (HiddenField_EditAdmin.Value == "Yes")
      {
        DropDownList DropDownList_EditOriginatedAtList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditOriginatedAtList");
        if (string.IsNullOrEmpty(DropDownList_EditOriginatedAtList.SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      if (string.IsNullOrEmpty(DropDownList_EditTypeList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_EditReceivedViaList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(DropDownList_EditReceivedFromList.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (DropDownList_EditReceivedFromList.SelectedValue.ToString() != "4396" && DropDownList_EditReceivedFromList.SelectedValue.ToString() != "4415" && DropDownList_EditReceivedFromList.SelectedValue.ToString() != "4798" && DropDownList_EditReceivedFromList.SelectedValue.ToString() != "5387")
      {
        if (string.IsNullOrEmpty(TextBox_EditCustomerName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditCustomerEmail.Text) && string.IsNullOrEmpty(TextBox_EditCustomerContactNumber.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_ReceivedFrom_Patient(string invalidForm)
    {
      string InvalidForm = invalidForm;

      DropDownList DropDownList_EditReceivedFromList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditReceivedFromList");

      TextBox TextBox_EditPatientVisitNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientVisitNumber");
      TextBox TextBox_EditPatientName = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientName");
      TextBox TextBox_EditPatientDateOfAdmission = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientDateOfAdmission");
      TextBox TextBox_EditPatientEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientEmail");
      TextBox TextBox_EditPatientContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientContactNumber");

      if (DropDownList_EditReceivedFromList.SelectedValue.ToString() == "4396")
      {
        if (string.IsNullOrEmpty(TextBox_EditPatientVisitNumber.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditPatientName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditPatientDateOfAdmission.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditPatientEmail.Text) && string.IsNullOrEmpty(TextBox_EditPatientContactNumber.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_Complaint(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_EditComplaintDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintDescription");
      DropDownList DropDownList_EditComplaintUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintUnitId");
      TextBox TextBox_EditComplaintDateOccurred = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintDateOccurred");
      DropDownList DropDownList_EditComplaintTimeOccuredHours = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintTimeOccuredHours");
      DropDownList DropDownList_EditComplaintTimeOccuredMinutes = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintTimeOccuredMinutes");
      DropDownList DropDownList_EditComplaintPriorityList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintPriorityList");
      CheckBoxList CheckBoxList_EditComplaintCategoryItemList = (CheckBoxList)FormView_CRM_Form.FindControl("CheckBoxList_EditComplaintCategoryItemList");
      DropDownList DropDownList_EditComplaintCustomerSatisfied = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintCustomerSatisfied");
      TextBox TextBox_EditComplaintInvestigatorName = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintInvestigatorName");
      TextBox TextBox_EditComplaintInvestigatorDesignation = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintInvestigatorDesignation");
      TextBox TextBox_EditComplaintRootCause = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintRootCause");
      CheckBox CheckBox_EditComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplaintCloseOut");

      if (CheckBox_EditComplaintCloseOut.Checked == true)
      {
        InvalidForm = EditRequiredFields_ReceivedFrom_Patient(InvalidForm);

        if (string.IsNullOrEmpty(TextBox_EditComplaintDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditComplaintUnitId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditComplaintDateOccurred.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditComplaintTimeOccuredHours.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditComplaintTimeOccuredMinutes.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditComplaintPriorityList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        string ComplaintCategoryCompleted = "No";
        foreach (System.Web.UI.WebControls.ListItem ListItem_ComplaintCategoryItemList in CheckBoxList_EditComplaintCategoryItemList.Items)
        {
          if (ListItem_ComplaintCategoryItemList.Selected == true)
          {
            ComplaintCategoryCompleted = "Yes";
            break;
          }
          else if (ListItem_ComplaintCategoryItemList.Selected == false)
          {
            ComplaintCategoryCompleted = "No";
          }
        }

        if (ComplaintCategoryCompleted == "No")
        {
          InvalidForm = "Yes";
        }

        InvalidForm = EditRequiredFields_Complaint_CompletedWithin(InvalidForm);

        if (string.IsNullOrEmpty(DropDownList_EditComplaintCustomerSatisfied.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditComplaintInvestigatorName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditComplaintInvestigatorDesignation.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditComplaintRootCause.Text))
        {
          InvalidForm = "Yes";
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_EditComplaintDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_Complaint_CompletedWithin(string invalidForm)
    {
      string InvalidForm = invalidForm;

      CheckBox CheckBox_EditEscalatedForm = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditEscalatedForm");

      DropDownList DropDownList_EditComplaintWithin24Hours = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintWithin24Hours");
      DropDownList DropDownList_EditComplaintWithin5Days = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintWithin5Days");
      DropDownList DropDownList_EditComplaintWithin10Days = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintWithin10Days");
      TextBox TextBox_EditComplaintWithin10DaysReason = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin10DaysReason");
      DropDownList DropDownList_EditComplaintWithin3Days = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintWithin3Days");
      TextBox TextBox_EditComplaintWithin3DaysReason = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintWithin3DaysReason");

      if (string.IsNullOrEmpty(DropDownList_EditComplaintWithin24Hours.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (CheckBox_EditEscalatedForm.Checked == true)
      {
        if (string.IsNullOrEmpty(DropDownList_EditComplaintWithin3Days.SelectedValue))
        {
          InvalidForm = "Yes";
        }
        else
        {
          if (DropDownList_EditComplaintWithin3Days.SelectedValue.ToString() == "No")
          {
            if (string.IsNullOrEmpty(TextBox_EditComplaintWithin3DaysReason.Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(DropDownList_EditComplaintWithin5Days.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditComplaintWithin10Days.SelectedValue))
        {
          InvalidForm = "Yes";
        }
        else
        {
          if (DropDownList_EditComplaintWithin10Days.SelectedValue.ToString() == "No")
          {
            if (string.IsNullOrEmpty(TextBox_EditComplaintWithin10DaysReason.Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_Compliment(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_EditComplimentDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplimentDescription");
      DropDownList DropDownList_EditComplimentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplimentUnitId");
      CheckBox CheckBox_EditComplimentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentAcknowledge");
      CheckBox CheckBox_EditComplimentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentCloseOut");

      if (CheckBox_EditComplimentAcknowledge.Checked == true)
      {
        if (CheckBox_EditComplimentCloseOut.Checked == true)
        {
          InvalidForm = EditRequiredFields_ReceivedFrom_Patient(InvalidForm);

          if (string.IsNullOrEmpty(TextBox_EditComplimentDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_EditComplimentUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else
        {
          if (string.IsNullOrEmpty(TextBox_EditComplimentDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_EditComplimentDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_Comment(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_EditCommentDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditCommentDescription");
      DropDownList DropDownList_EditCommentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentUnitId");
      DropDownList DropDownList_EditCommentTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentTypeList");
      CheckBox CheckBox_EditCommentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentAcknowledge");
      CheckBox CheckBox_EditCommentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentCloseOut");

      if (CheckBox_EditCommentAcknowledge.Checked == true)
      {
        if (CheckBox_EditCommentCloseOut.Checked == true)
        {
          InvalidForm = EditRequiredFields_ReceivedFrom_Patient(InvalidForm);

          if (string.IsNullOrEmpty(TextBox_EditCommentDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_EditCommentUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_EditCommentTypeList.Text))
          {
            InvalidForm = "Yes";
          }
        }
        else
        {
          if (string.IsNullOrEmpty(TextBox_EditCommentDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_EditCommentDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_Query(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_EditQueryDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditQueryDescription");
      DropDownList DropDownList_EditQueryUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditQueryUnitId");
      CheckBox CheckBox_EditQueryAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryAcknowledge");
      CheckBox CheckBox_EditQueryCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryCloseOut");

      if (CheckBox_EditQueryAcknowledge.Checked == true)
      {
        if (CheckBox_EditQueryCloseOut.Checked == true)
        {
          InvalidForm = EditRequiredFields_ReceivedFrom_Patient(InvalidForm);

          if (string.IsNullOrEmpty(TextBox_EditQueryDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_EditQueryUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else
        {
          if (string.IsNullOrEmpty(TextBox_EditQueryDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_EditQueryDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_Suggestions(string invalidForm)
    {
      string InvalidForm = invalidForm;

      TextBox TextBox_EditSuggestionDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditSuggestionDescription");
      DropDownList DropDownList_EditSuggestionUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditSuggestionUnitId");
      CheckBox CheckBox_EditSuggestionAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionAcknowledge");
      CheckBox CheckBox_EditSuggestionCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionCloseOut");

      if (CheckBox_EditSuggestionAcknowledge.Checked == true)
      {
        if (CheckBox_EditSuggestionCloseOut.Checked == true)
        {
          InvalidForm = EditRequiredFields_ReceivedFrom_Patient(InvalidForm);

          if (string.IsNullOrEmpty(TextBox_EditSuggestionDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_EditSuggestionUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else
        {
          if (string.IsNullOrEmpty(TextBox_EditSuggestionDescription.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_EditSuggestionDescription.Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_Route(string invalidForm)
    {
      string InvalidForm = invalidForm;

      CheckBox CheckBox_EditRouteRoute = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteRoute");
      DropDownList DropDownList_EditRouteFacility = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditRouteFacility");
      RadioButtonList RadioButtonList_EditRouteUnit = (RadioButtonList)FormView_CRM_Form.FindControl("RadioButtonList_EditRouteUnit");
      TextBox TextBox_EditRouteComment = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditRouteComment");
      DropDownList DropDownList_EditStatus = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditStatus");

      if (DropDownList_EditStatus.SelectedValue == "Approved")
      {
        if (CheckBox_EditRouteRoute.Visible == true)
        {
          if (CheckBox_EditRouteRoute.Checked == true)
          {
            if (string.IsNullOrEmpty(DropDownList_EditRouteFacility.SelectedValue))
            {
              InvalidForm = "Yes";
            }

            if (RadioButtonList_EditRouteUnit.Items.Count > 0)
            {
              if (string.IsNullOrEmpty(RadioButtonList_EditRouteUnit.SelectedValue))
              {
                InvalidForm = "Yes";
              }
            }

            if (string.IsNullOrEmpty(TextBox_EditRouteComment.Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
        else
        {
          if (string.IsNullOrEmpty(TextBox_EditRouteComment.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      return InvalidForm;
    }

    protected string EditFieldValidation()
    {
      string InvalidFormMessage = "";

      TextBox TextBox_EditDateReceived = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditDateReceived");
      DropDownList DropDownList_EditTypeList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditTypeList");
      TextBox TextBox_EditComplaintDateOccurred = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintDateOccurred");
      DropDownList DropDownList_EditComplaintTimeOccuredHours = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintTimeOccuredHours");
      DropDownList DropDownList_EditComplaintTimeOccuredMinutes = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintTimeOccuredMinutes");

      string DateToValidateDate = TextBox_EditDateReceived.Text.ToString();
      DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

      if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + Convert.ToString("Date Received is not in the correct format, date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
      }
      else
      {
        DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_CRM_Form.FindControl("TextBox_EditDateReceived")).Text, CultureInfo.CurrentCulture);
        DateTime CurrentDate = DateTime.Now;

        if (PickedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + Convert.ToString("No future dates allowed<br />", CultureInfo.CurrentCulture);
        }
      }

      if (DropDownList_EditTypeList.SelectedValue == "4395")
      {
        if (!string.IsNullOrEmpty(TextBox_EditComplaintDateOccurred.Text))
        {
          string DateToValidateComplaintDateOccurred = TextBox_EditComplaintDateOccurred.Text.ToString();
          DateTime ValidatedDateComplaintDateOccurred = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateComplaintDateOccurred);

          if (ValidatedDateComplaintDateOccurred.ToString() == "0001/01/01 12:00:00 AM")
          {
            InvalidFormMessage = InvalidFormMessage + Convert.ToString("Date Incident Occurred is not in the correct format, date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
          }
          else
          {
            DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_CRM_Form.FindControl("TextBox_EditDateReceived")).Text, CultureInfo.CurrentCulture);
            DateTime CurrentDate = DateTime.Now;

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + Convert.ToString("No future dates allowed<br />", CultureInfo.CurrentCulture);
            }
          }
        }

        if (!string.IsNullOrEmpty(DropDownList_EditComplaintTimeOccuredHours.SelectedValue))
        {
          Int32 Hours = Convert.ToInt32(DropDownList_EditComplaintTimeOccuredHours.SelectedValue, CultureInfo.CurrentCulture);

          if (Hours < 0 || Hours > 23)
          {
            InvalidFormMessage = InvalidFormMessage + Convert.ToString("Hours is not in the correct format, Hours must be between 0 and 23<br />", CultureInfo.CurrentCulture);
          }
        }

        if (!string.IsNullOrEmpty(DropDownList_EditComplaintTimeOccuredMinutes.SelectedValue))
        {
          Int32 Minutes = Convert.ToInt32(DropDownList_EditComplaintTimeOccuredMinutes.SelectedValue, CultureInfo.CurrentCulture);

          if (Minutes < 0 || Minutes > 59)
          {
            InvalidFormMessage = InvalidFormMessage + Convert.ToString("Minutes is not in the correct format, Minutes must be between 0 and 59<br />", CultureInfo.CurrentCulture);
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_CRM_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            CheckBox CheckBox_EditRouteRoute = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteRoute");
            CheckBox CheckBox_EditRouteComplete = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteComplete");

            if (CheckBox_EditRouteRoute.Visible == true)
            {
              if (CheckBox_EditRouteRoute.Checked == true)
              {
                Edit_EmailRouteTo();
              }
            }
            else
            {
              if (CheckBox_EditRouteComplete.Checked == true)
              {
                Edit_EmailRouteComplete();
              }
            }

            if (Request.QueryString["Search_CRMForm"] == "List")
            {
              RedirectToList();
            }
            else if (Request.QueryString["Search_CRMForm"] == "IncompleteComplaints")
            {
              RedirectToIncompleteComplaints();
            }
            else if (Request.QueryString["Search_CRMForm"] == "IncompleteOther")
            {
              RedirectToIncompleteOther();
            }
            else
            {
              RedirectToList();
            }
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_CRM, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Print", "InfoQuest_Print.aspx?PrintPage=Form_CRM&PrintValue=" + Request.QueryString["CRM_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_CRM, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_CRM, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Email", "InfoQuest_Email.aspx?EmailPage=Form_CRM&EmailValue=" + Request.QueryString["CRM_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_CRM, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_CRM_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["CRM_Id"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Form", "Form_CRM.aspx?CRM_Id=" + Request.QueryString["CRM_Id"] + ""), false);
          }
        }
      }
    }

    protected void FormView_CRM_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_CRM_Form.CurrentMode == FormViewMode.Insert)
      {
        InsertDataBound();
      }

      if (FormView_CRM_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_CRM_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();

        ItemRegisterPostBackControl();
      }
    }

    protected void InsertDataBound()
    {
      DropDownList DropDownList_InsertOriginatedAtList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertOriginatedAtList");
      Label Label_InsertOriginatedAtList = (Label)FormView_CRM_Form.FindControl("Label_InsertOriginatedAtList");
      Label Label_InsertStatus = (Label)FormView_CRM_Form.FindControl("Label_InsertStatus");

      CheckBox CheckBox_InsertComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplaintCloseOut");
      CheckBox CheckBox_InsertComplimentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplimentAcknowledge");
      CheckBox CheckBox_InsertComplimentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplimentCloseOut");
      CheckBox CheckBox_InsertCommentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertCommentAcknowledge");
      CheckBox CheckBox_InsertCommentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertCommentCloseOut");
      CheckBox CheckBox_InsertQueryAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertQueryAcknowledge");
      CheckBox CheckBox_InsertQueryCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertQueryCloseOut");
      CheckBox CheckBox_InsertSuggestionAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertSuggestionAcknowledge");
      CheckBox CheckBox_InsertSuggestionCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertSuggestionCloseOut");

      CheckBox_InsertComplaintCloseOut.Checked = false;
      CheckBox_InsertComplaintCloseOut.Visible = false;
      CheckBox_InsertComplimentAcknowledge.Checked = false;
      CheckBox_InsertComplimentAcknowledge.Visible = false;
      CheckBox_InsertComplimentCloseOut.Checked = false;
      CheckBox_InsertComplimentCloseOut.Visible = false;
      CheckBox_InsertCommentAcknowledge.Checked = false;
      CheckBox_InsertCommentAcknowledge.Visible = false;
      CheckBox_InsertCommentCloseOut.Checked = false;
      CheckBox_InsertCommentCloseOut.Visible = false;
      CheckBox_InsertQueryAcknowledge.Checked = false;
      CheckBox_InsertQueryAcknowledge.Visible = false;
      CheckBox_InsertQueryCloseOut.Checked = false;
      CheckBox_InsertQueryCloseOut.Visible = false;
      CheckBox_InsertSuggestionAcknowledge.Checked = false;
      CheckBox_InsertSuggestionAcknowledge.Visible = false;
      CheckBox_InsertSuggestionCloseOut.Checked = false;
      CheckBox_InsertSuggestionCloseOut.Visible = false;

      string SecurityRole = "";
      HiddenField HiddenField_InsertSecurityRole = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertSecurityRole");
      SecurityRole = HiddenField_InsertSecurityRole.Value;

      string Security = "1";
      if (Security == "1" && (SecurityRole == "1" || SecurityRole == "146"))
      {
        Security = "0";

        DropDownList_InsertOriginatedAtList.Visible = true;
        Label_InsertOriginatedAtList.Text = "";
        Label_InsertOriginatedAtList.Visible = false;

        CheckBox_InsertComplaintCloseOut.Checked = false;
        CheckBox_InsertComplaintCloseOut.Visible = false;
        CheckBox_InsertComplimentAcknowledge.Visible = true;
        CheckBox_InsertComplimentCloseOut.Visible = true;
        CheckBox_InsertCommentAcknowledge.Visible = true;
        CheckBox_InsertCommentCloseOut.Visible = true;
        CheckBox_InsertQueryAcknowledge.Visible = true;
        CheckBox_InsertQueryCloseOut.Visible = true;
        CheckBox_InsertSuggestionAcknowledge.Visible = true;
        CheckBox_InsertSuggestionCloseOut.Visible = true;

        Label_InsertStatus.Text = Convert.ToString("Approved", CultureInfo.CurrentCulture);
      }

      if (Security == "1" && (SecurityRole == "150" || SecurityRole == "148" || SecurityRole == "153"))
      {
        Security = "0";

        DropDownList_InsertOriginatedAtList.SelectedValue = "";
        DropDownList_InsertOriginatedAtList.Visible = false;
        Label_InsertOriginatedAtList.Text = Convert.ToString("Facility", CultureInfo.CurrentCulture);
        Label_InsertOriginatedAtList.Visible = true;

        CheckBox_InsertComplaintCloseOut.Checked = false;
        CheckBox_InsertComplaintCloseOut.Visible = false;
        CheckBox_InsertComplimentAcknowledge.Visible = true;
        CheckBox_InsertComplimentCloseOut.Visible = true;
        CheckBox_InsertCommentAcknowledge.Visible = true;
        CheckBox_InsertCommentCloseOut.Visible = true;
        CheckBox_InsertQueryAcknowledge.Visible = true;
        CheckBox_InsertQueryCloseOut.Visible = true;
        CheckBox_InsertSuggestionAcknowledge.Visible = true;
        CheckBox_InsertSuggestionCloseOut.Visible = true;

        Label_InsertStatus.Text = Convert.ToString("Approved", CultureInfo.CurrentCulture);
      }

      if (Security == "1" && SecurityRole == "151")
      {
        Security = "0";

        DropDownList_InsertOriginatedAtList.SelectedValue = "";
        DropDownList_InsertOriginatedAtList.Visible = false;
        Label_InsertOriginatedAtList.Text = Convert.ToString("Facility", CultureInfo.CurrentCulture);
        Label_InsertOriginatedAtList.Visible = true;
        Label_InsertStatus.Text = Convert.ToString("Approved", CultureInfo.CurrentCulture);
      }

      if (Security == "1")
      {
        DropDownList_InsertOriginatedAtList.SelectedValue = "";
        DropDownList_InsertOriginatedAtList.Visible = false;
        Label_InsertOriginatedAtList.Text = Convert.ToString("Facility", CultureInfo.CurrentCulture);
        Label_InsertOriginatedAtList.Visible = true;
        Label_InsertStatus.Text = Convert.ToString("Pending Approval", CultureInfo.CurrentCulture);
      }
    }

    protected void EditDataBound()
    {
      if (Request.QueryString["CRM_Id"] != null)
      {
        Session["CRMId"] = "";
        string SQLStringSurveyResults = "SELECT DISTINCT CRM_Id FROM Form_CRM_PXM_PDCH_Result WHERE CRM_Id = @CRM_Id";
        using (SqlCommand SqlCommand_SurveyResults = new SqlCommand(SQLStringSurveyResults))
        {
          SqlCommand_SurveyResults.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
          DataTable DataTable_SurveyResults;
          using (DataTable_SurveyResults = new DataTable())
          {
            DataTable_SurveyResults.Locale = CultureInfo.CurrentCulture;
            DataTable_SurveyResults = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SurveyResults).Copy();
            if (DataTable_SurveyResults.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SurveyResults.Rows)
              {
                Session["CRMId"] = DataRow_Row["CRM_Id"].ToString();
              }
            }
          }
        }

        if (string.IsNullOrEmpty(Session["CRMId"].ToString()))
        {
          FormView_CRM_Form.FindControl("SurveyResults1").Visible = false;
          FormView_CRM_Form.FindControl("SurveyResults2").Visible = false;
          FormView_CRM_Form.FindControl("SurveyResults3").Visible = false;
        }
        else
        {
          FormView_CRM_Form.FindControl("SurveyResults1").Visible = true;
          FormView_CRM_Form.FindControl("SurveyResults2").Visible = true;
          FormView_CRM_Form.FindControl("SurveyResults3").Visible = true;
        }

        Session.Remove("CRMId");


        EditDataBound_ShowHideControls();

        DropDownList DropDownList_EditComplaintUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintUnitId");
        DataView DataView_ComplaintUnitId = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ComplaintUnitId = DataView_ComplaintUnitId[0];
        DropDownList_EditComplaintUnitId.SelectedValue = Convert.ToString(DataRowView_ComplaintUnitId["CRM_Complaint_Unit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditComplaintUnitId.SelectParameters["Facility_Id"].DefaultValue = Convert.ToString(DataRowView_ComplaintUnitId["Facility_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditComplaintUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Complaint_Unit_Id";
        SqlDataSource_CRM_EditComplaintUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
        SqlDataSource_CRM_EditComplaintUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

        DropDownList DropDownList_EditComplimentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplimentUnitId");
        DataView DataView_ComplimentUnitId = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ComplimentUnitId = DataView_ComplimentUnitId[0];
        DropDownList_EditComplimentUnitId.SelectedValue = Convert.ToString(DataRowView_ComplimentUnitId["CRM_Compliment_Unit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditComplimentUnitId.SelectParameters["Facility_Id"].DefaultValue = Convert.ToString(DataRowView_ComplimentUnitId["Facility_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditComplimentUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Compliment_Unit_Id";
        SqlDataSource_CRM_EditComplimentUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
        SqlDataSource_CRM_EditComplimentUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

        DropDownList DropDownList_EditCommentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentUnitId");
        DataView DataView_CommentUnitId = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CommentUnitId = DataView_CommentUnitId[0];
        DropDownList_EditCommentUnitId.SelectedValue = Convert.ToString(DataRowView_CommentUnitId["CRM_Comment_Unit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditCommentUnitId.SelectParameters["Facility_Id"].DefaultValue = Convert.ToString(DataRowView_CommentUnitId["Facility_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditCommentUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_Unit_Id";
        SqlDataSource_CRM_EditCommentUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
        SqlDataSource_CRM_EditCommentUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

        DropDownList DropDownList_EditCommentAdditionalUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditCommentAdditionalUnitId");
        DataView DataView_CommentAdditionalUnitId = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CommentAdditionalUnitId = DataView_CommentAdditionalUnitId[0];
        DropDownList_EditCommentAdditionalUnitId.SelectedValue = Convert.ToString(DataRowView_CommentAdditionalUnitId["CRM_Comment_AdditionalUnit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters["Facility_Id"].DefaultValue = Convert.ToString(DataRowView_CommentUnitId["Facility_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_AdditionalUnit_Id";
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

        DropDownList DropDownList_EditQueryUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditQueryUnitId");
        DataView DataView_QueryUnitId = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_QueryUnitId = DataView_QueryUnitId[0];
        DropDownList_EditQueryUnitId.SelectedValue = Convert.ToString(DataRowView_QueryUnitId["CRM_Query_Unit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditQueryUnitId.SelectParameters["Facility_Id"].DefaultValue = Convert.ToString(DataRowView_QueryUnitId["Facility_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditQueryUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Query_Unit_Id";
        SqlDataSource_CRM_EditQueryUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
        SqlDataSource_CRM_EditQueryUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";

        DropDownList DropDownList_EditSuggestionUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditSuggestionUnitId");
        DataView DataView_SuggestionUnitId = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_SuggestionUnitId = DataView_SuggestionUnitId[0];
        DropDownList_EditSuggestionUnitId.SelectedValue = Convert.ToString(DataRowView_SuggestionUnitId["CRM_Suggestion_Unit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters["Facility_Id"].DefaultValue = Convert.ToString(DataRowView_SuggestionUnitId["Facility_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters["TableSELECT"].DefaultValue = "CRM_Suggestion_Unit_Id";
        SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
        SqlDataSource_CRM_EditSuggestionUnitId.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + Request.QueryString["CRM_Id"] + " ";


        Session["Edit_FacilityId"] = Convert.ToString(DataRowView_ComplaintUnitId["Facility_Id"], CultureInfo.CurrentCulture);
        Session["Edit_CRMId"] = Request.QueryString["CRM_Id"];


        Session["FacilityFacilityDisplayName"] = "";
        Session["CRMOriginatedAtName"] = "";
        string SQLStringCRM = "SELECT Facility_FacilityDisplayName , CRM_OriginatedAt_Name FROM vForm_CRM WHERE CRM_Id = @CRM_Id";
        using (SqlCommand SqlCommand_CRM = new SqlCommand(SQLStringCRM))
        {
          SqlCommand_CRM.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
          DataTable DataTable_CRM;
          using (DataTable_CRM = new DataTable())
          {
            DataTable_CRM.Locale = CultureInfo.CurrentCulture;
            DataTable_CRM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRM).Copy();
            if (DataTable_CRM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_CRM.Rows)
              {
                Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
                Session["CRMOriginatedAtName"] = DataRow_Row["CRM_OriginatedAt_Name"];
              }
            }
          }
        }

        Label Label_EditFacility = (Label)FormView_CRM_Form.FindControl("Label_EditFacility");
        Label_EditFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_EditOriginatedAtList = (Label)FormView_CRM_Form.FindControl("Label_EditOriginatedAtList");
        Label_EditOriginatedAtList.Text = Session["CRMOriginatedAtName"].ToString();

        Session["FacilityFacilityDisplayName"] = "";
        Session["CRMOriginatedAtName"] = "";



        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 36";
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
          ((Button)FormView_CRM_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_CRM_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_CRM_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_CRM_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void EditDataBound_ShowHideControls()
    {
      DropDownList DropDownList_EditOriginatedAtList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditOriginatedAtList");
      Label Label_EditOriginatedAtList = (Label)FormView_CRM_Form.FindControl("Label_EditOriginatedAtList");

      CheckBox CheckBox_EditComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplaintCloseOut");
      CheckBox CheckBox_EditComplimentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentAcknowledge");
      CheckBox CheckBox_EditComplimentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentCloseOut");
      CheckBox CheckBox_EditCommentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentAcknowledge");
      CheckBox CheckBox_EditCommentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentCloseOut");
      CheckBox CheckBox_EditQueryAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryAcknowledge");
      CheckBox CheckBox_EditQueryCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryCloseOut");
      CheckBox CheckBox_EditSuggestionAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionAcknowledge");
      CheckBox CheckBox_EditSuggestionCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionCloseOut");

      Label Label_EditComplaintCloseOut = (Label)FormView_CRM_Form.FindControl("Label_EditComplaintCloseOut");
      Label Label_EditComplimentAcknowledge = (Label)FormView_CRM_Form.FindControl("Label_EditComplimentAcknowledge");
      Label Label_EditCommentAcknowledge = (Label)FormView_CRM_Form.FindControl("Label_EditCommentAcknowledge");
      Label Label_EditQueryAcknowledge = (Label)FormView_CRM_Form.FindControl("Label_EditQueryAcknowledge");
      Label Label_EditSuggestionAcknowledge = (Label)FormView_CRM_Form.FindControl("Label_EditSuggestionAcknowledge");

      CheckBox CheckBox_EditRouteRoute = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteRoute");

      DropDownList DropDownList_EditComplaintPriorityList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintPriorityList");
      DropDownList DropDownList_EditStatus = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditStatus");

      CheckBox_EditComplaintCloseOut.Visible = false;
      CheckBox_EditComplimentAcknowledge.Visible = false;
      CheckBox_EditComplimentCloseOut.Visible = false;
      CheckBox_EditCommentAcknowledge.Visible = false;
      CheckBox_EditCommentCloseOut.Visible = false;
      CheckBox_EditQueryAcknowledge.Visible = false;
      CheckBox_EditQueryCloseOut.Visible = false;
      CheckBox_EditSuggestionAcknowledge.Visible = false;
      CheckBox_EditSuggestionCloseOut.Visible = false;

      Label_EditComplaintCloseOut.Visible = true;
      Label_EditComplimentAcknowledge.Visible = true;
      Label_EditCommentAcknowledge.Visible = true;
      Label_EditQueryAcknowledge.Visible = true;
      Label_EditSuggestionAcknowledge.Visible = true;

      CheckBox_EditRouteRoute.Visible = false;

      FromDataBase_IsRouteComplete FromDataBase_IsRouteComplete_Current = GetIsRouteComplete();
      string IsRouteComplete = FromDataBase_IsRouteComplete_Current.IsRouteComplete;

      string SecurityRole = "";
      HiddenField HiddenField_EditSecurityRole = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_EditSecurityRole");
      SecurityRole = HiddenField_EditSecurityRole.Value;

      string Security = "1";
      if (Security == "1" && (SecurityRole == "1" || SecurityRole == "146"))
      {
        Security = "0";
        DropDownList_EditOriginatedAtList.Visible = true;
        Label_EditOriginatedAtList.Text = "";
        Label_EditOriginatedAtList.Visible = false;

        if (DropDownList_EditStatus.SelectedValue == "Approved")
        {
          if (IsRouteComplete == "Yes")
          {
            if (!string.IsNullOrEmpty(DropDownList_EditComplaintPriorityList.SelectedValue))
            {
              CheckBox_EditComplaintCloseOut.Visible = true;
              Label_EditComplaintCloseOut.Visible = false;
            }

            CheckBox_EditComplimentAcknowledge.Visible = true;
            CheckBox_EditComplimentCloseOut.Visible = true;
            CheckBox_EditCommentAcknowledge.Visible = true;
            CheckBox_EditCommentCloseOut.Visible = true;
            CheckBox_EditQueryAcknowledge.Visible = true;
            CheckBox_EditQueryCloseOut.Visible = true;
            CheckBox_EditSuggestionAcknowledge.Visible = true;
            CheckBox_EditSuggestionCloseOut.Visible = true;

            Label_EditComplimentAcknowledge.Visible = false;
            Label_EditCommentAcknowledge.Visible = false;
            Label_EditQueryAcknowledge.Visible = false;
            Label_EditSuggestionAcknowledge.Visible = false;
          }

          CheckBox_EditRouteRoute.Visible = true;
        }
      }

      if (Security == "1" && (SecurityRole == "150" || SecurityRole == "148" || SecurityRole == "153"))
      {
        Security = "0";

        DropDownList_EditOriginatedAtList.SelectedValue = "";
        DropDownList_EditOriginatedAtList.Visible = false;
        Label_EditOriginatedAtList.Text = Convert.ToString("Facility", CultureInfo.CurrentCulture);
        Label_EditOriginatedAtList.Visible = true;

        if (DropDownList_EditStatus.SelectedValue == "Approved")
        {
          if (IsRouteComplete == "Yes")
          {
            if (DropDownList_EditComplaintPriorityList.SelectedValue == "4400")
            {
              if (HiddenField_EditSecurityRole.Value == "150")
              {
                CheckBox_EditComplaintCloseOut.Visible = true;
                Label_EditComplaintCloseOut.Visible = false;
              }
            }
            else if (DropDownList_EditComplaintPriorityList.SelectedValue == "4401")
            {
              if (HiddenField_EditSecurityRole.Value == "148")
              {
                CheckBox_EditComplaintCloseOut.Visible = true;
                Label_EditComplaintCloseOut.Visible = false;
              }
            }

            CheckBox_EditComplimentAcknowledge.Visible = true;
            CheckBox_EditComplimentCloseOut.Visible = true;
            CheckBox_EditCommentAcknowledge.Visible = true;
            CheckBox_EditCommentCloseOut.Visible = true;
            CheckBox_EditQueryAcknowledge.Visible = true;
            CheckBox_EditQueryCloseOut.Visible = true;
            CheckBox_EditSuggestionAcknowledge.Visible = true;
            CheckBox_EditSuggestionCloseOut.Visible = true;

            Label_EditComplimentAcknowledge.Visible = false;
            Label_EditCommentAcknowledge.Visible = false;
            Label_EditQueryAcknowledge.Visible = false;
            Label_EditSuggestionAcknowledge.Visible = false;
          }

          CheckBox_EditRouteRoute.Visible = true;
        }
      }

      if (Security == "1")
      {
        DropDownList_EditOriginatedAtList.SelectedValue = "";
        DropDownList_EditOriginatedAtList.Visible = false;
        Label_EditOriginatedAtList.Text = Convert.ToString("Facility", CultureInfo.CurrentCulture);
        Label_EditOriginatedAtList.Visible = true;
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["CRM_Id"] != null)
      {
        Session["CRMId"] = "";
        string SQLStringSurveyResults = "SELECT DISTINCT CRM_Id FROM Form_CRM_PXM_PDCH_Result WHERE CRM_Id = @CRM_Id";
        using (SqlCommand SqlCommand_SurveyResults = new SqlCommand(SQLStringSurveyResults))
        {
          SqlCommand_SurveyResults.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
          DataTable DataTable_SurveyResults;
          using (DataTable_SurveyResults = new DataTable())
          {
            DataTable_SurveyResults.Locale = CultureInfo.CurrentCulture;
            DataTable_SurveyResults = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SurveyResults).Copy();
            if (DataTable_SurveyResults.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SurveyResults.Rows)
              {
                Session["CRMId"] = DataRow_Row["CRM_Id"].ToString();
              }
            }
          }
        }

        if (string.IsNullOrEmpty(Session["CRMId"].ToString()))
        {
          FormView_CRM_Form.FindControl("SurveyResults1").Visible = false;
          FormView_CRM_Form.FindControl("SurveyResults2").Visible = false;
          FormView_CRM_Form.FindControl("SurveyResults3").Visible = false;
        }
        else
        {
          FormView_CRM_Form.FindControl("SurveyResults1").Visible = true;
          FormView_CRM_Form.FindControl("SurveyResults2").Visible = true;
          FormView_CRM_Form.FindControl("SurveyResults3").Visible = true;
        }

        Session.Remove("CRMId");


        Session["FacilityFacilityDisplayName"] = "";
        Session["CRMOriginatedAtName"] = "";
        Session["CRMTypeName"] = "";
        Session["CRMReceivedViaName"] = "";
        Session["CRMReceivedFromName"] = "";
        Session["CRMComplaintUnitName"] = "";
        Session["CRMComplaintPriorityName"] = "";
        Session["CRMComplaintWithin24HoursMethodName"] = "";
        Session["CRMComplimentUnitName"] = "";
        Session["CRMCommentUnitName"] = "";
        Session["CRMCommentTypeName"] = "";
        Session["CRMCommentCategoryName"] = "";
        Session["CRMCommentAdditionalUnitName"] = "";
        Session["CRMCommentAdditionalTypeName"] = "";
        Session["CRMCommentAdditionalCategoryName"] = "";
        Session["CRMQueryUnitName"] = "";
        Session["CRMSuggestionUnitName"] = "";
        string SQLStringCRM = "SELECT Facility_FacilityDisplayName , CRM_OriginatedAt_Name , CRM_Type_Name , CRM_ReceivedVia_Name , CRM_ReceivedFrom_Name , CRM_Complaint_Unit_Name , CRM_Complaint_Priority_Name , CRM_Complaint_Within24HoursMethod_Name , CRM_Compliment_Unit_Name , CRM_Comment_Unit_Name , CRM_Comment_Type_Name , CRM_Comment_Category_Name , CRM_Comment_AdditionalUnit_Name , CRM_Comment_AdditionalType_Name , CRM_Comment_AdditionalCategory_Name , CRM_Query_Unit_Name , CRM_Suggestion_Unit_Name FROM vForm_CRM WHERE CRM_Id = @CRM_Id";
        using (SqlCommand SqlCommand_CRM = new SqlCommand(SQLStringCRM))
        {
          SqlCommand_CRM.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
          DataTable DataTable_CRM;
          using (DataTable_CRM = new DataTable())
          {
            DataTable_CRM.Locale = CultureInfo.CurrentCulture;
            DataTable_CRM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRM).Copy();
            if (DataTable_CRM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_CRM.Rows)
              {
                Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
                Session["CRMOriginatedAtName"] = DataRow_Row["CRM_OriginatedAt_Name"];
                Session["CRMTypeName"] = DataRow_Row["CRM_Type_Name"];
                Session["CRMReceivedViaName"] = DataRow_Row["CRM_ReceivedVia_Name"];
                Session["CRMReceivedFromName"] = DataRow_Row["CRM_ReceivedFrom_Name"];
                Session["CRMComplaintUnitName"] = DataRow_Row["CRM_Complaint_Unit_Name"];
                Session["CRMComplaintPriorityName"] = DataRow_Row["CRM_Complaint_Priority_Name"];
                Session["CRMComplaintWithin24HoursMethodName"] = DataRow_Row["CRM_Complaint_Within24HoursMethod_Name"];
                Session["CRMComplimentUnitName"] = DataRow_Row["CRM_Compliment_Unit_Name"];
                Session["CRMCommentUnitName"] = DataRow_Row["CRM_Comment_Unit_Name"];
                Session["CRMCommentTypeName"] = DataRow_Row["CRM_Comment_Type_Name"];
                Session["CRMCommentCategoryName"] = DataRow_Row["CRM_Comment_Category_Name"];
                Session["CRMCommentAdditionalUnitName"] = DataRow_Row["CRM_Comment_AdditionalUnit_Name"];
                Session["CRMCommentAdditionalTypeName"] = DataRow_Row["CRM_Comment_AdditionalType_Name"];
                Session["CRMCommentAdditionalCategoryName"] = DataRow_Row["CRM_Comment_AdditionalCategory_Name"];
                Session["CRMQueryUnitName"] = DataRow_Row["CRM_Query_Unit_Name"];
                Session["CRMSuggestionUnitName"] = DataRow_Row["CRM_Suggestion_Unit_Name"];
              }
            }
          }
        }


        Label Label_ItemFacility = (Label)FormView_CRM_Form.FindControl("Label_ItemFacility");
        Label_ItemFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_ItemOriginatedAtList = (Label)FormView_CRM_Form.FindControl("Label_ItemOriginatedAtList");
        Label_ItemOriginatedAtList.Text = Session["CRMOriginatedAtName"].ToString();

        Label Label_ItemTypeList = (Label)FormView_CRM_Form.FindControl("Label_ItemTypeList");
        Label_ItemTypeList.Text = Session["CRMTypeName"].ToString();

        Label Label_ItemReceivedViaList = (Label)FormView_CRM_Form.FindControl("Label_ItemReceivedViaList");
        Label_ItemReceivedViaList.Text = Session["CRMReceivedViaName"].ToString();

        Label Label_ItemReceivedFromList = (Label)FormView_CRM_Form.FindControl("Label_ItemReceivedFromList");
        Label_ItemReceivedFromList.Text = Session["CRMReceivedFromName"].ToString();

        Label Label_ItemComplaintUnitId = (Label)FormView_CRM_Form.FindControl("Label_ItemComplaintUnitId");
        Label_ItemComplaintUnitId.Text = Session["CRMComplaintUnitName"].ToString();

        Label Label_ItemComplaintPriorityList = (Label)FormView_CRM_Form.FindControl("Label_ItemComplaintPriorityList");
        Label_ItemComplaintPriorityList.Text = Session["CRMComplaintPriorityName"].ToString();

        Label Label_ItemComplaintWithin24HoursMethodList = (Label)FormView_CRM_Form.FindControl("Label_ItemComplaintWithin24HoursMethodList");
        Label_ItemComplaintWithin24HoursMethodList.Text = Session["CRMComplaintWithin24HoursMethodName"].ToString();

        Label Label_ItemComplimentUnitId = (Label)FormView_CRM_Form.FindControl("Label_ItemComplimentUnitId");
        Label_ItemComplimentUnitId.Text = Session["CRMComplimentUnitName"].ToString();

        Label Label_ItemCommentUnitId = (Label)FormView_CRM_Form.FindControl("Label_ItemCommentUnitId");
        Label_ItemCommentUnitId.Text = Session["CRMCommentUnitName"].ToString();

        Label Label_ItemCommentTypeList = (Label)FormView_CRM_Form.FindControl("Label_ItemCommentTypeList");
        Label_ItemCommentTypeList.Text = Session["CRMCommentTypeName"].ToString();

        Label Label_ItemCommentCategoryList = (Label)FormView_CRM_Form.FindControl("Label_ItemCommentCategoryList");
        Label_ItemCommentCategoryList.Text = Session["CRMCommentCategoryName"].ToString();

        Label Label_ItemCommentAdditionalUnitId = (Label)FormView_CRM_Form.FindControl("Label_ItemCommentAdditionalUnitId");
        Label_ItemCommentAdditionalUnitId.Text = Session["CRMCommentAdditionalUnitName"].ToString();

        Label Label_ItemCommentAdditionalTypeList = (Label)FormView_CRM_Form.FindControl("Label_ItemCommentAdditionalTypeList");
        Label_ItemCommentAdditionalTypeList.Text = Session["CRMCommentAdditionalTypeName"].ToString();

        Label Label_ItemCommentAdditionalCategoryList = (Label)FormView_CRM_Form.FindControl("Label_ItemCommentAdditionalCategoryList");
        Label_ItemCommentAdditionalCategoryList.Text = Session["CRMCommentAdditionalCategoryName"].ToString();

        Label Label_ItemQueryUnitId = (Label)FormView_CRM_Form.FindControl("Label_ItemQueryUnitId");
        Label_ItemQueryUnitId.Text = Session["CRMQueryUnitName"].ToString();

        Label Label_ItemSuggestionUnitId = (Label)FormView_CRM_Form.FindControl("Label_ItemSuggestionUnitId");
        Label_ItemSuggestionUnitId.Text = Session["CRMSuggestionUnitName"].ToString();

        Session["FacilityFacilityDisplayName"] = "";
        Session["CRMOriginatedAtName"] = "";
        Session["CRMTypeName"] = "";
        Session["CRMReceivedViaName"] = "";
        Session["CRMReceivedFromName"] = "";
        Session["CRMComplaintUnitName"] = "";
        Session["CRMComplaintPriorityName"] = "";
        Session["CRMComplaintWithin24HoursMethodName"] = "";
        Session["CRMComplimentUnitName"] = "";
        Session["CRMCommentUnitName"] = "";
        Session["CRMCommentTypeName"] = "";
        Session["CRMCommentCategoryName"] = "";
        Session["CRMCommentAdditionalUnitName"] = "";
        Session["CRMCommentAdditionalTypeName"] = "";
        Session["CRMCommentAdditionalCategoryName"] = "";
        Session["CRMQueryUnitName"] = "";
        Session["CRMSuggestionUnitName"] = "";


        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 36";
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
          ((Button)FormView_CRM_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_CRM_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_CRM_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Print", "InfoQuest_Print.aspx?PrintPage=Form_CRM&PrintValue=" + Request.QueryString["CRM_Id"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_CRM_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_CRM_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_CRM_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Email", "InfoQuest_Email.aspx?EmailPage=Form_CRM&EmailValue=" + Request.QueryString["CRM_Id"] + "") + "')";
        }

        Email = "";
        Print = "";
      }
    }

    protected void Edit_EmailRouteTo()
    {
      DropDownList DropDownList_EditRouteFacility = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditRouteFacility");
      RadioButtonList RadioButtonList_EditRouteUnit = (RadioButtonList)FormView_CRM_Form.FindControl("RadioButtonList_EditRouteUnit");
      TextBox TextBox_EditRouteComment = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditRouteComment");

      FromDataBase_CRMRouteValues FromDataBase_CRMRouteValues_Current = GetCRMRouteValues();
      string CRMRouteFacilityFacilityDisplayName = FromDataBase_CRMRouteValues_Current.FacilityFacilityDisplayName;

      FromDataBase_CRMValues FromDataBase_CRMValues_Current = GetCRMValues();
      string CRMId = FromDataBase_CRMValues_Current.CRMId;
      string CRMReportNumber = FromDataBase_CRMValues_Current.CRMReportNumber;
      string FacilityFacilityDisplayName = FromDataBase_CRMValues_Current.FacilityFacilityDisplayName;

      string DisplayName = "";
      string Email = "";
      string SQLStringEmailRouteTo = "SELECT DISTINCT DisplayName , Email , EmailOrder FROM ( " +
                                     "   SELECT * , RANK() OVER (ORDER BY EmailOrder) AS EmailRank FROM ( " +
                                     "     SELECT	ListItem_Name AS DisplayName , " +
                                     "             ListItem_Name AS Email , " +
                                     "             '1' AS EmailOrder " +
                                     "     FROM		vAdministration_ListItem_Active " +
                                     "     WHERE		ListCategory_Id = 165 " +
                                     "             AND ListItem_Parent = @RouteToUnit " +
                                     "            AND ListItem_Parent IN ( " +
                                     "              SELECT	ListItem_Id " +
                                     "              FROM		vAdministration_ListItem_Active " +
                                     "              WHERE		ListCategory_Id = 164 " +
                                     "                      AND ListItem_Parent IN ( " +
                                     "                        SELECT	ListItem_Id " +
                                     "                        FROM		vAdministration_ListItem_Active " +
                                     "                        WHERE		ListCategory_Id = 163 " +
                                     "                                AND ListItem_Name = @RouteToFacility " +
                                     "                      ) " +
                                     "            ) " +
                                     "     UNION " +
                                     "     SELECT	ListItem_Name AS DisplayName , " +
                                     "             ListItem_Name AS Email , " +
                                     "             '2' AS EmailOrder " +
                                     "     FROM		vAdministration_ListItem_Active " +
                                     "     WHERE		ListCategory_Id = 167 " +
                                     "             AND ListItem_Parent IN ( " +
                                     "               SELECT	ListItem_Id " +
                                     "               FROM		vAdministration_ListItem_Active " +
                                     "               WHERE		ListCategory_Id = 166 " +
                                     "                       AND ListItem_Name = @RouteToFacility " +
                                     "             ) " +
                                     "     UNION " +
                                     "     SELECT	ISNULL(SecurityUser_DisplayName,'') AS DisplayName , " +
                                     "             ISNULL(SecurityUser_Email,'') AS Email , " +
                                     "             '3' AS EmailOrder " +
                                     "     FROM		vAdministration_SecurityAccess_Active " +
                                     "     WHERE		Form_Id IN ('36') " +
                                     "             AND SecurityRole_Id IN ('148') " +
                                     "             AND Facility_Id = @RouteToFacility " +
                                     "             AND SecurityUser_Email IS NOT NULL " +
                                     "   ) AS TempTableA " +
                                     " ) AS TempTableB " +
                                     " WHERE EmailRank = 1 " +
                                     " ORDER BY EmailOrder";
      using (SqlCommand SqlCommand_EmailRouteTo = new SqlCommand(SQLStringEmailRouteTo))
      {
        SqlCommand_EmailRouteTo.Parameters.AddWithValue("@RouteToUnit", RadioButtonList_EditRouteUnit.SelectedValue);
        SqlCommand_EmailRouteTo.Parameters.AddWithValue("@RouteToFacility", DropDownList_EditRouteFacility.SelectedValue);
        SqlCommand_EmailRouteTo.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
        DataTable DataTable_EmailRouteTo;
        using (DataTable_EmailRouteTo = new DataTable())
        {
          DataTable_EmailRouteTo.Locale = CultureInfo.CurrentCulture;
          DataTable_EmailRouteTo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailRouteTo).Copy();
          if (DataTable_EmailRouteTo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EmailRouteTo.Rows)
            {
              DisplayName = DataRow_Row["DisplayName"].ToString();
              Email = DataRow_Row["Email"].ToString();

              string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("62");
              string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
              string FormsName = InfoQuestWCF.InfoQuest_All.All_FormName("36");
              string BodyString = EmailTemplate;

              BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + DisplayName + "");
              BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormsName + "");
              BodyString = BodyString.Replace(";replace;OriginalFacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
              BodyString = BodyString.Replace(";replace;RoutedFacilityFacilityDisplayName;replace;", "" + CRMRouteFacilityFacilityDisplayName + "");
              BodyString = BodyString.Replace(";replace;CRMReportNumber;replace;", "" + CRMReportNumber + "");
              BodyString = BodyString.Replace(";replace;RouteComment;replace;", "" + TextBox_EditRouteComment.Text + "");
              BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
              BodyString = BodyString.Replace(";replace;CRMId;replace;", "" + CRMId + "");

              string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
              string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
              string EmailBody = HeaderString + BodyString + FooterString;

              string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", Email, FormsName, EmailBody);

              if (!string.IsNullOrEmpty(EmailSend))
              {
                EmailSend = "";
              }

              EmailBody = "";
              EmailTemplate = "";
              URLAuthority = "";
              FormsName = "";
            }
          }
        }
      }

      CRMId = "";
      FacilityFacilityDisplayName = "";
      CRMReportNumber = "";

      DisplayName = "";
      Email = "";
    }

    protected void Edit_EmailRouteComplete()
    {
      Label Label_EditRouteFacility = (Label)FormView_CRM_Form.FindControl("Label_EditRouteFacility");
      TextBox TextBox_EditRouteComment = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditRouteComment");

      FromDataBase_CRMValues FromDataBase_CRMValues_Current = GetCRMValues();
      string CRMId = FromDataBase_CRMValues_Current.CRMId;
      string CRMReportNumber = FromDataBase_CRMValues_Current.CRMReportNumber;
      string FacilityFacilityDisplayName = FromDataBase_CRMValues_Current.FacilityFacilityDisplayName;

      string DisplayName = "";
      string Email = "";
      string SQLStringEmailRouteComplete = "SELECT DISTINCT DisplayName , Email , EmailOrder FROM ( " +
                                           "   SELECT * , RANK() OVER (ORDER BY EmailOrder) AS EmailRank FROM ( " +
                                           "     SELECT	ListItem_Name AS DisplayName , " +
                                           "             ListItem_Name AS Email , " +
                                           "             '1' AS Id , " +
                                           "             '1' AS EmailOrder " +
                                           "     FROM		vAdministration_ListItem_Active " +
                                           "     WHERE		ListCategory_Id = 167 " +
                                           "             AND ListItem_Parent IN ( " +
                                           "               SELECT	ListItem_Id " +
                                           "               FROM		vAdministration_ListItem_Active " +
                                           "               WHERE		ListCategory_Id = 166 " +
                                           "                       AND ListItem_Name IN ( " +
                                           "                         SELECT	Facility_Id " +
                                           "                         FROM		Form_CRM " +
                                           "                         WHERE		CRM_Id = @CRM_Id " +
                                           "                       ) " +
                                           "             ) " +
                                           "     UNION " +
                                           "     SELECT	ISNULL(SecurityUser_DisplayName,'') AS DisplayName , " +
                                           "             ISNULL(SecurityUser_Email,'') AS Email , " +
                                           "             '1' AS Id , " +
                                           "             '2' AS EmailOrder " +
                                           "     FROM		vAdministration_SecurityAccess_Active " +
                                           "     WHERE		Form_Id IN ('36') " +
                                           "             AND SecurityRole_Id IN ('148') " +
                                           "             AND Facility_Id IN ( " +
                                           "               SELECT	Facility_Id " +
                                           "               FROM		Form_CRM " +
                                           "               WHERE		CRM_Id = @CRM_Id " +
                                           "             ) " +
                                           "             AND SecurityUser_Email IS NOT NULL " +
                                           "   ) AS TempTableA " +
                                           " ) AS TempTableB " +
                                           " WHERE EmailRank = 1 " +
                                           " ORDER BY EmailOrder";
      using (SqlCommand SqlCommand_EmailRouteComplete = new SqlCommand(SQLStringEmailRouteComplete))
      {
        SqlCommand_EmailRouteComplete.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
        DataTable DataTable_EmailRouteComplete;
        using (DataTable_EmailRouteComplete = new DataTable())
        {
          DataTable_EmailRouteComplete.Locale = CultureInfo.CurrentCulture;
          DataTable_EmailRouteComplete = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailRouteComplete).Copy();
          if (DataTable_EmailRouteComplete.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EmailRouteComplete.Rows)
            {
              DisplayName = DataRow_Row["DisplayName"].ToString();
              Email = DataRow_Row["Email"].ToString();

              string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("63");
              string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
              string FormsName = InfoQuestWCF.InfoQuest_All.All_FormName("36");
              string BodyString = EmailTemplate;

              BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + DisplayName + "");
              BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormsName + "");
              BodyString = BodyString.Replace(";replace;OriginalFacilityFacilityDisplayName;replace;", "" + FacilityFacilityDisplayName + "");
              BodyString = BodyString.Replace(";replace;RoutedFacilityFacilityDisplayName;replace;", "" + Label_EditRouteFacility.Text + "");
              BodyString = BodyString.Replace(";replace;CRMReportNumber;replace;", "" + CRMReportNumber + "");
              BodyString = BodyString.Replace(";replace;RouteComment;replace;", "" + TextBox_EditRouteComment.Text + "");
              BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
              BodyString = BodyString.Replace(";replace;CRMId;replace;", "" + CRMId + "");

              string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
              string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
              string EmailBody = HeaderString + BodyString + FooterString;

              string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", Email, FormsName, EmailBody);

              if (!string.IsNullOrEmpty(EmailSend))
              {
                EmailSend = "";
              }

              EmailBody = "";
              EmailTemplate = "";
              URLAuthority = "";
              FormsName = "";
            }
          }
        }
      }

      CRMId = "";
      CRMReportNumber = "";
      FacilityFacilityDisplayName = "";

      DisplayName = "";
      Email = "";
    }


    //--START-- --Insert Controls--//
    protected void InsertRegisterPostBackControl()
    {
      TextBox TextBox_InsertEscalatedReportNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertEscalatedReportNumber");
      TextBox TextBox_InsertCustomerEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerEmail");
      TextBox TextBox_InsertPatientName = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientName");
      TextBox TextBox_InsertPatientEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientEmail");
      DropDownList DropDownList_InsertComplaintPriorityList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintPriorityList");
      Button Button_InsertUploadFile = (Button)FormView_CRM_Form.FindControl("Button_InsertUploadFile");

      ScriptManager ScriptManager_Insert = ScriptManager.GetCurrent(Page);

      ScriptManager_Insert.RegisterPostBackControl(TextBox_InsertEscalatedReportNumber);
      ScriptManager_Insert.RegisterPostBackControl(TextBox_InsertCustomerEmail);
      ScriptManager_Insert.RegisterPostBackControl(TextBox_InsertPatientName);
      ScriptManager_Insert.RegisterPostBackControl(TextBox_InsertPatientEmail);
      ScriptManager_Insert.RegisterPostBackControl(DropDownList_InsertComplaintPriorityList);
      ScriptManager_Insert.RegisterPostBackControl(Button_InsertUploadFile);
    }

    protected void HiddenField_InsertSecurityRole_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_InsertSecurityRole = (HiddenField)sender;
      string SecurityRole = "";

      if (string.IsNullOrEmpty(Request.QueryString["CRM_Id"]))
      {
        FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
        DataRow[] SecurityFacilityAdminHospitalManagerUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminHospitalManagerUpdate;
        DataRow[] SecurityFacilityAdminNSMUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminNSMUpdate;
        DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;
        DataRow[] SecurityFacilityInvestigator = FromDataBase_SecurityRole_Current.SecurityFacilityInvestigator;
        DataRow[] SecurityFacilityApprover = FromDataBase_SecurityRole_Current.SecurityFacilityApprover;
        DataRow[] SecurityFacilityCapturer = FromDataBase_SecurityRole_Current.SecurityFacilityCapturer;

        string Security = "1";
        if (Security == "1" && SecurityAdmin.Length > 0)
        {
          Security = "0";
          SecurityRole = "1";
        }

        if (Security == "1" && SecurityFormAdminUpdate.Length > 0)
        {
          Security = "0";
          SecurityRole = "146";
        }

        if (Security == "1" && SecurityFormAdminView.Length > 0)
        {
          Security = "0";
          SecurityRole = "147";
        }

        if (Security == "1" && SecurityFacilityAdminHospitalManagerUpdate.Length > 0)
        {
          Security = "0";
          SecurityRole = "150";
        }

        if (Security == "1" && SecurityFacilityAdminNSMUpdate.Length > 0)
        {
          Security = "0";
          SecurityRole = "148";
        }

        if (Security == "1" && SecurityFacilityAdminView.Length > 0)
        {
          Security = "0";
          SecurityRole = "149";
        }

        if (Security == "1" && SecurityFacilityInvestigator.Length > 0)
        {
          Security = "0";
          SecurityRole = "153";
        }

        if (Security == "1" && SecurityFacilityApprover.Length > 0)
        {
          Security = "0";
          SecurityRole = "151";
        }

        if (Security == "1" && SecurityFacilityCapturer.Length > 0)
        {
          Security = "0";
          SecurityRole = "152";
        }
      }

      HiddenField_InsertSecurityRole.Value = SecurityRole;
    }

    protected void HiddenField_InsertAdmin_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_InsertAdmin = (HiddenField)sender;
      string Admin = "No";

      if (string.IsNullOrEmpty(Request.QueryString["CRM_Id"]))
      {
        string SecurityRole = "";
        HiddenField HiddenField_InsertSecurityRole = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertSecurityRole");
        SecurityRole = HiddenField_InsertSecurityRole.Value;

        if (SecurityRole == "1" || SecurityRole == "146")
        {
          Admin = "Yes";
        }
      }

      HiddenField_InsertAdmin.Value = Admin;
    }

    protected void DropDownList_InsertFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertFacility = (DropDownList)sender;
      DropDownList DropDownList_InsertComplaintUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplaintUnitId");
      DropDownList DropDownList_InsertComplimentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertComplimentUnitId");
      DropDownList DropDownList_InsertCommentUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentUnitId");
      DropDownList DropDownList_InsertCommentAdditionalUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertCommentAdditionalUnitId");
      DropDownList DropDownList_InsertQueryUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertQueryUnitId");
      DropDownList DropDownList_InsertSuggestionUnitId = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertSuggestionUnitId");


      Session["Insert_FacilityId"] = DropDownList_InsertFacility.SelectedValue;


      DropDownList_InsertComplaintUnitId.Items.Clear();
      SqlDataSource_CRM_InsertComplaintUnitId.SelectParameters["Facility_Id"].DefaultValue = DropDownList_InsertFacility.SelectedValue;
      DropDownList_InsertComplaintUnitId.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertComplaintUnitId.DataBind();

      DropDownList_InsertComplimentUnitId.Items.Clear();
      SqlDataSource_CRM_InsertComplimentUnitId.SelectParameters["Facility_Id"].DefaultValue = DropDownList_InsertFacility.SelectedValue;
      DropDownList_InsertComplimentUnitId.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertComplimentUnitId.DataBind();

      DropDownList_InsertCommentUnitId.Items.Clear();
      SqlDataSource_CRM_InsertCommentUnitId.SelectParameters["Facility_Id"].DefaultValue = DropDownList_InsertFacility.SelectedValue;
      DropDownList_InsertCommentUnitId.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertCommentUnitId.DataBind();

      DropDownList_InsertCommentAdditionalUnitId.Items.Clear();
      SqlDataSource_CRM_InsertCommentAdditionalUnitId.SelectParameters["Facility_Id"].DefaultValue = DropDownList_InsertFacility.SelectedValue;
      DropDownList_InsertCommentAdditionalUnitId.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertCommentAdditionalUnitId.DataBind();

      DropDownList_InsertQueryUnitId.Items.Clear();
      SqlDataSource_CRM_InsertQueryUnitId.SelectParameters["Facility_Id"].DefaultValue = DropDownList_InsertFacility.SelectedValue;
      DropDownList_InsertQueryUnitId.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertQueryUnitId.DataBind();

      DropDownList_InsertSuggestionUnitId.Items.Clear();
      SqlDataSource_CRM_InsertSuggestionUnitId.SelectParameters["Facility_Id"].DefaultValue = DropDownList_InsertFacility.SelectedValue;
      DropDownList_InsertSuggestionUnitId.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertSuggestionUnitId.DataBind();

      TextBox TextBox_InsertPatientVisitNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientVisitNumber");
      TextBox TextBox_InsertPatientName = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientName");
      TextBox TextBox_InsertPatientDateOfAdmission = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientDateOfAdmission");
      TextBox TextBox_InsertPatientEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientEmail");
      TextBox TextBox_InsertPatientContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientContactNumber");

      TextBox_InsertPatientVisitNumber.Text = "";
      TextBox_InsertPatientName.Text = "";
      TextBox_InsertPatientDateOfAdmission.Text = "";
      TextBox_InsertPatientEmail.Text = "";
      TextBox_InsertPatientContactNumber.Text = "";


      InsertRegisterPostBackControl();
    }

    protected void TextBox_InsertEscalatedReportNumber_TextChanged(object sender, EventArgs e)
    {
      string EscalatedReportNumberErrorMessage = "";
      TextBox TextBox_InsertEscalatedReportNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertEscalatedReportNumber");
      DropDownList DropDownList_InsertFacility = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertFacility");
      TextBox TextBox_InsertComplaintDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertComplaintDescription");
      Label Label_InsertEscalatedReportNumberError = (Label)FormView_CRM_Form.FindControl("Label_InsertEscalatedReportNumberError");

      if (string.IsNullOrEmpty(DropDownList_InsertFacility.SelectedValue))
      {
        ToolkitScriptManager_CRM.SetFocus(DropDownList_InsertFacility);
        EscalatedReportNumberErrorMessage = EscalatedReportNumberErrorMessage + Convert.ToString("No Facility has been selected", CultureInfo.CurrentCulture);
      }
      else
      {
        if (string.IsNullOrEmpty(TextBox_InsertEscalatedReportNumber.Text.ToString()))
        {
          ToolkitScriptManager_CRM.SetFocus(TextBox_InsertEscalatedReportNumber);
          EscalatedReportNumberErrorMessage = "";
        }
        else
        {
          ToolkitScriptManager_CRM.SetFocus(TextBox_InsertComplaintDescription);

          Session["CRMReportNumber"] = "";
          string SQLStringCRMReportNumber = "SELECT DISTINCT CRM_ReportNumber FROM Form_CRM WHERE CRM_ReportNumber IS NOT NULL AND CRM_ReportNumber = @CRM_ReportNumber AND CRM_Status = 'Approved' AND Facility_Id = @Facility_Id";
          using (SqlCommand SqlCommand_CRMReportNumber = new SqlCommand(SQLStringCRMReportNumber))
          {
            SqlCommand_CRMReportNumber.Parameters.AddWithValue("@CRM_ReportNumber", TextBox_InsertEscalatedReportNumber.Text.ToString());
            SqlCommand_CRMReportNumber.Parameters.AddWithValue("@Facility_Id", DropDownList_InsertFacility.SelectedValue.ToString());
            DataTable DataTable_CRMReportNumber;
            using (DataTable_CRMReportNumber = new DataTable())
            {
              DataTable_CRMReportNumber.Locale = CultureInfo.CurrentCulture;
              DataTable_CRMReportNumber = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMReportNumber).Copy();
              if (DataTable_CRMReportNumber.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_CRMReportNumber.Rows)
                {
                  Session["CRMReportNumber"] = DataRow_Row["CRM_ReportNumber"];
                }
              }
            }
          }

          if (string.IsNullOrEmpty(Session["CRMReportNumber"].ToString()))
          {
            ToolkitScriptManager_CRM.SetFocus(TextBox_InsertEscalatedReportNumber);
            EscalatedReportNumberErrorMessage = EscalatedReportNumberErrorMessage + Convert.ToString("Report Number " + TextBox_InsertEscalatedReportNumber.Text.ToString() + " does not exist for this facility", CultureInfo.CurrentCulture);
          }
        }
      }

      Label_InsertEscalatedReportNumberError.Text = EscalatedReportNumberErrorMessage;

      InsertRegisterPostBackControl();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] ServiceMethod_InsertEscalatedReportNumber(string prefixText, int count, string contextKey)
    {
      if (string.IsNullOrEmpty(contextKey))
      {
        List<string> List_Items = new List<string>(count);

        DataTable DataTable_EscalatedReportNumber;
        using (DataTable_EscalatedReportNumber = new DataTable())
        {
          DataTable_EscalatedReportNumber.Locale = CultureInfo.CurrentCulture;

          string SQLStringReportNumber = "SELECT DISTINCT CRM_ReportNumber FROM Form_CRM WHERE CRM_Type_List = 4395 AND CRM_ReportNumber IS NOT NULL AND CRM_ReportNumber LIKE @CRM_ReportNumber + '%' AND CRM_Status = 'Approved' AND Facility_Id = @Facility_Id ORDER BY CRM_ReportNumber DESC";
          string SQLConnectionReportNumber = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

          DataTable_EscalatedReportNumber.Reset();
          DataTable_EscalatedReportNumber.Columns.Add("CRM_ReportNumber", typeof(string));

          using (SqlCommand SqlCommand_Form_CRM_GetEscalatedReportNumber = new SqlCommand(SQLStringReportNumber))
          {
            SqlCommand_Form_CRM_GetEscalatedReportNumber.Parameters.AddWithValue("@CRM_ReportNumber", prefixText);
            SqlCommand_Form_CRM_GetEscalatedReportNumber.Parameters.AddWithValue("@Facility_Id", HttpContext.Current.Session["Insert_FacilityId"]);

            using (SqlConnection SQLConnection_Form_CRM_GetEscalatedReportNumber = new SqlConnection(SQLConnectionReportNumber))
            {
              using (SqlDataAdapter SqlDataAdapter_Form_CRM_GetEscalatedReportNumber = new SqlDataAdapter())
              {
                foreach (SqlParameter SqlParameter_Value in SqlCommand_Form_CRM_GetEscalatedReportNumber.Parameters)
                {
                  if (SqlParameter_Value.Value == null)
                  {
                    SqlParameter_Value.Value = DBNull.Value;
                  }
                }

                SqlCommand_Form_CRM_GetEscalatedReportNumber.CommandType = CommandType.Text;
                SqlCommand_Form_CRM_GetEscalatedReportNumber.Connection = SQLConnection_Form_CRM_GetEscalatedReportNumber;
                SQLConnection_Form_CRM_GetEscalatedReportNumber.Open();
                SqlDataAdapter_Form_CRM_GetEscalatedReportNumber.SelectCommand = SqlCommand_Form_CRM_GetEscalatedReportNumber;
                SqlDataAdapter_Form_CRM_GetEscalatedReportNumber.Fill(DataTable_EscalatedReportNumber);
              }
            }
          }

          if (DataTable_EscalatedReportNumber.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EscalatedReportNumber.Rows)
            {
              string ReportNumber = DataRow_Row["CRM_ReportNumber"].ToString();
              List_Items.Add(ReportNumber);
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

    protected void TextBox_InsertCustomerEmail_TextChanged(object sender, EventArgs e)
    {
      string EmailErrorMessage = "";
      TextBox TextBox_InsertCustomerEmail = (TextBox)sender;
      Label Label_InsertCustomerEmailError = (Label)FormView_CRM_Form.FindControl("Label_InsertCustomerEmailError");
      TextBox TextBox_InsertCustomerContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertCustomerContactNumber");

      if (string.IsNullOrEmpty(TextBox_InsertCustomerEmail.Text.ToString()))
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_InsertCustomerEmail);
        EmailErrorMessage = "";
      }
      else
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_InsertCustomerContactNumber);

        string EmailTextBox = TextBox_InsertCustomerEmail.Text.ToString();
        EmailTextBox = EmailTextBox.Replace(";", Convert.ToString(",", CultureInfo.CurrentCulture));
        EmailTextBox = EmailTextBox.Replace(":", Convert.ToString(",", CultureInfo.CurrentCulture));

        string EmailTextBoxSplit = EmailTextBox;
        string[] EmailTextBoxSplitEmails = EmailTextBoxSplit.Split(',');

        foreach (string EmailTextBoxSplitEmail in EmailTextBoxSplitEmails)
        {
          InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
          string ValidEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailTextBoxSplitEmail);

          if (ValidEmailAddress == "No")
          {
            ToolkitScriptManager_CRM.SetFocus(TextBox_InsertCustomerEmail);
            EmailErrorMessage = EmailErrorMessage + Convert.ToString("Email Address " + EmailTextBoxSplitEmail + " is not a valid Email address<br />", CultureInfo.CurrentCulture);
          }
        }

        TextBox_InsertCustomerEmail.Text = EmailTextBox;
      }

      Label_InsertCustomerEmailError.Text = EmailErrorMessage;

      InsertRegisterPostBackControl();
    }

    protected void Button_InsertFindPatient_OnClick(object sender, EventArgs e)
    {
      string PatientErrorMessage = "";
      DropDownList DropDownList_InsertFacility = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_InsertFacility");
      TextBox TextBox_InsertPatientVisitNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientVisitNumber");
      TextBox TextBox_InsertPatientName = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientName");
      TextBox TextBox_InsertPatientDateOfAdmission = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientDateOfAdmission");
      TextBox TextBox_InsertPatientEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientEmail");
      TextBox TextBox_InsertPatientContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientContactNumber");
      Label Label_InsertPatientError = (Label)FormView_CRM_Form.FindControl("Label_InsertPatientError");

      if (string.IsNullOrEmpty(TextBox_InsertPatientVisitNumber.Text))
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_InsertPatientVisitNumber);
        PatientErrorMessage = PatientErrorMessage + Convert.ToString("Visit Number not provided to find Patient Information<br />", CultureInfo.CurrentCulture);
      }
      else
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFacility.SelectedValue))
        {
          ToolkitScriptManager_CRM.SetFocus(TextBox_InsertPatientVisitNumber);
          PatientErrorMessage = PatientErrorMessage + Convert.ToString("Facility not Selected to find Patient Information<br />", CultureInfo.CurrentCulture);
        }
        else
        {
          Session["NameSurname"] = "";
          Session["DateofAdmission"] = "";
          Session["Email"] = "";
          Session["ContactNumber"] = "";
          DataTable DataTable_DataPatient;
          using (DataTable_DataPatient = new DataTable())
          {
            DataTable_DataPatient.Locale = CultureInfo.CurrentCulture;
            //DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(DropDownList_InsertFacility.SelectedValue.ToString(), TextBox_InsertPatientVisitNumber.Text).Copy();
            DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(DropDownList_InsertFacility.SelectedValue.ToString(), TextBox_InsertPatientVisitNumber.Text).Copy();

            if (DataTable_DataPatient.Columns.Count == 1)
            {
              foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
              {
                PatientErrorMessage = PatientErrorMessage + DataRow_Row["Error"];
              }
            }
            else if (DataTable_DataPatient.Columns.Count != 1)
            {
              if (DataTable_DataPatient.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
                {
                  Session["NameSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];
                  Session["DateofAdmission"] = DataRow_Row["DateOfAdmission"];
                  Session["Email"] = DataRow_Row["Email"];
                  Session["ContactNumber"] = DataRow_Row["ContactNumber"];

                  string NameSurname = Session["NameSurname"].ToString();
                  NameSurname = NameSurname.Replace("'", "");
                  Session["NameSurname"] = NameSurname;
                  NameSurname = "";

                  string Email = Session["Email"].ToString();
                  Email = Email.Replace("'", "");
                  Session["Email"] = Email;
                  Email = "";

                  PatientErrorMessage = "";

                  TextBox_InsertPatientName.Text = Session["NameSurname"].ToString();
                  TextBox_InsertPatientDateOfAdmission.Text = Session["DateofAdmission"].ToString();
                  TextBox_InsertPatientEmail.Text = Session["Email"].ToString();
                  TextBox_InsertPatientContactNumber.Text = Session["ContactNumber"].ToString();
                }
              }
              else
              {
                PatientErrorMessage = PatientErrorMessage + Convert.ToString("Patient Information not found for specific Patient Visit Number,<br />Please type in the Patient Information<br />", CultureInfo.CurrentCulture);
              }
            }
          }

          Session["NameSurname"] = "";
          Session["DateofAdmission"] = "";
          Session["Email"] = "";
          Session["ContactNumber"] = "";
        }
      }

      Label_InsertPatientError.Text = PatientErrorMessage;

      InsertRegisterPostBackControl();
    }

    protected void TextBox_InsertPatientName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertPatientName = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientName");
      TextBox TextBox_InsertPatientDateOfAdmission = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientDateOfAdmission");
      Label Label_InsertPatientError = (Label)FormView_CRM_Form.FindControl("Label_InsertPatientError");

      if (!string.IsNullOrEmpty(TextBox_InsertPatientName.Text))
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_InsertPatientDateOfAdmission);
        Label_InsertPatientError.Text = "";
      }

      InsertRegisterPostBackControl();
    }

    protected void TextBox_InsertPatientEmail_TextChanged(object sender, EventArgs e)
    {
      string PatientEmailErrorMessage = "";
      TextBox TextBox_InsertPatientEmail = (TextBox)sender;
      Label Label_InsertPatientEmailError = (Label)FormView_CRM_Form.FindControl("Label_InsertPatientEmailError");
      TextBox TextBox_InsertPatientContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_InsertPatientContactNumber");

      if (string.IsNullOrEmpty(TextBox_InsertPatientEmail.Text.ToString()))
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_InsertPatientEmail);
        PatientEmailErrorMessage = "";
      }
      else
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_InsertPatientContactNumber);

        string EmailTextBox = TextBox_InsertPatientEmail.Text.ToString();
        EmailTextBox = EmailTextBox.Replace(";", Convert.ToString(",", CultureInfo.CurrentCulture));
        EmailTextBox = EmailTextBox.Replace(":", Convert.ToString(",", CultureInfo.CurrentCulture));

        string EmailTextBoxSplit = EmailTextBox;
        string[] EmailTextBoxSplitEmails = EmailTextBoxSplit.Split(',');

        foreach (string EmailTextBoxSplitEmail in EmailTextBoxSplitEmails)
        {
          InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
          string ValidEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailTextBoxSplitEmail);

          if (ValidEmailAddress == "No")
          {
            ToolkitScriptManager_CRM.SetFocus(TextBox_InsertPatientEmail);
            PatientEmailErrorMessage = PatientEmailErrorMessage + Convert.ToString("Email Address " + EmailTextBoxSplitEmail + " is not a valid Email address<br />", CultureInfo.CurrentCulture);
          }
        }

        TextBox_InsertPatientEmail.Text = EmailTextBox;
      }

      Label_InsertPatientEmailError.Text = PatientEmailErrorMessage;

      InsertRegisterPostBackControl();
    }

    protected void DropDownList_InsertComplaintPriorityList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertComplaintPriorityList = (DropDownList)sender;
      HiddenField HiddenField_InsertSecurityRole = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertSecurityRole");
      CheckBox CheckBox_InsertComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_InsertComplaintCloseOut");

      if (string.IsNullOrEmpty(DropDownList_InsertComplaintPriorityList.SelectedValue))
      {
        CheckBox_InsertComplaintCloseOut.Checked = false;
        CheckBox_InsertComplaintCloseOut.Visible = false;
      }
      else if (DropDownList_InsertComplaintPriorityList.SelectedValue == "4400")
      {
        if (HiddenField_InsertSecurityRole.Value == "1" || HiddenField_InsertSecurityRole.Value == "146" || HiddenField_InsertSecurityRole.Value == "150")
        {
          CheckBox_InsertComplaintCloseOut.Visible = true;
        }
        else
        {
          CheckBox_InsertComplaintCloseOut.Checked = false;
          CheckBox_InsertComplaintCloseOut.Visible = false;
        }
      }
      else if (DropDownList_InsertComplaintPriorityList.SelectedValue == "4401")
      {
        if (HiddenField_InsertSecurityRole.Value == "1" || HiddenField_InsertSecurityRole.Value == "146" || HiddenField_InsertSecurityRole.Value == "148")
        {
          CheckBox_InsertComplaintCloseOut.Visible = true;
        }
        else
        {
          CheckBox_InsertComplaintCloseOut.Checked = false;
          CheckBox_InsertComplaintCloseOut.Visible = false;
        }
      }
      else
      {
        CheckBox_InsertComplaintCloseOut.Checked = false;
        CheckBox_InsertComplaintCloseOut.Visible = false;
      }

      ToolkitScriptManager_CRM.SetFocus(DropDownList_InsertComplaintPriorityList);

      InsertRegisterPostBackControl();
    }

    protected void HiddenField_InsertComplaintCategoryItemListTotal_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_InsertComplaintCategoryItemListTotal = (HiddenField)sender;
      CheckBoxList CheckBoxList_InsertComplaintCategoryItemList = (CheckBoxList)FormView_CRM_Form.FindControl("CheckBoxList_InsertComplaintCategoryItemList");
      HiddenField_InsertComplaintCategoryItemListTotal.Value = CheckBoxList_InsertComplaintCategoryItemList.Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void Button_InsertIncompleteOther_Click(object sender, EventArgs e)
    {
      FileCleanUp();
      ComplaintCategoryCleanUp();
      RedirectToIncompleteOther();
    }

    protected void Button_InsertIncompleteComplaints_Click(object sender, EventArgs e)
    {
      FileCleanUp();
      ComplaintCategoryCleanUp();
      RedirectToIncompleteComplaints();
    }

    protected void Button_InsertCaptured_Click(object sender, EventArgs e)
    {
      FileCleanUp();
      ComplaintCategoryCleanUp();
      RedirectToList();
    }

    protected void Button_InsertClear_Click(object sender, EventArgs e)
    {
      FileCleanUp();
      ComplaintCategoryCleanUp();
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Form", "Form_CRM.aspx"), false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void EditRegisterPostBackControl()
    {
      TextBox TextBox_EditEscalatedReportNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditEscalatedReportNumber");
      TextBox TextBox_EditCustomerEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerEmail");
      TextBox TextBox_EditPatientName = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientName");
      TextBox TextBox_EditPatientEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientEmail");
      DropDownList DropDownList_EditComplaintPriorityList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintPriorityList");
      DropDownList DropDownList_EditStatus = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditStatus");
      Button Button_EditUploadFile = (Button)FormView_CRM_Form.FindControl("Button_EditUploadFile");
      Button Button_EditConvertToComplaint = (Button)FormView_CRM_Form.FindControl("Button_EditConvertToComplaint");
      Button Button_EditSurveyResults = (Button)FormView_CRM_Form.FindControl("Button_EditSurveyResults");
      DropDownList DropDownList_EditRouteFacility = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditRouteFacility");

      ScriptManager ScriptManager_Edit = ScriptManager.GetCurrent(Page);

      ScriptManager_Edit.RegisterPostBackControl(TextBox_EditEscalatedReportNumber);
      ScriptManager_Edit.RegisterPostBackControl(TextBox_EditCustomerEmail);
      ScriptManager_Edit.RegisterPostBackControl(TextBox_EditPatientName);
      ScriptManager_Edit.RegisterPostBackControl(TextBox_EditPatientEmail);
      ScriptManager_Edit.RegisterPostBackControl(DropDownList_EditComplaintPriorityList);
      ScriptManager_Edit.RegisterPostBackControl(DropDownList_EditStatus);
      ScriptManager_Edit.RegisterPostBackControl(Button_EditUploadFile);
      ScriptManager_Edit.RegisterPostBackControl(Button_EditConvertToComplaint);

      if (Button_EditSurveyResults != null)
      {
        ScriptManager_Edit.RegisterPostBackControl(Button_EditSurveyResults);
      }

      if (DropDownList_EditRouteFacility != null)
      {
        ScriptManager_Edit.RegisterPostBackControl(DropDownList_EditRouteFacility);
      }
    }

    protected void HiddenField_EditSecurityRole_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_EditSecurityRole = (HiddenField)sender;
      string SecurityRole = "";

      if (Request.QueryString["CRM_Id"] != null)
      {
        FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
        DataRow[] SecurityFacilityAdminHospitalManagerUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminHospitalManagerUpdate;
        DataRow[] SecurityFacilityAdminNSMUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminNSMUpdate;
        DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;
        DataRow[] SecurityFacilityInvestigator = FromDataBase_SecurityRole_Current.SecurityFacilityInvestigator;
        DataRow[] SecurityFacilityApprover = FromDataBase_SecurityRole_Current.SecurityFacilityApprover;
        DataRow[] SecurityFacilityCapturer = FromDataBase_SecurityRole_Current.SecurityFacilityCapturer;

        string Security = "1";
        if (Security == "1" && SecurityAdmin.Length > 0)
        {
          Security = "0";
          SecurityRole = "1";
        }

        if (Security == "1" && SecurityFormAdminUpdate.Length > 0)
        {
          Security = "0";
          SecurityRole = "146";
        }

        if (Security == "1" && SecurityFormAdminView.Length > 0)
        {
          Security = "0";
          SecurityRole = "147";
        }

        if (Security == "1" && SecurityFacilityAdminHospitalManagerUpdate.Length > 0)
        {
          Security = "0";
          SecurityRole = "150";
        }

        if (Security == "1" && SecurityFacilityAdminNSMUpdate.Length > 0)
        {
          Security = "0";
          SecurityRole = "148";
        }

        if (Security == "1" && SecurityFacilityAdminView.Length > 0)
        {
          Security = "0";
          SecurityRole = "149";
        }

        if (Security == "1" && SecurityFacilityInvestigator.Length > 0)
        {
          Security = "0";
          SecurityRole = "153";
        }

        if (Security == "1" && SecurityFacilityApprover.Length > 0)
        {
          Security = "0";
          SecurityRole = "151";
        }

        if (Security == "1" && SecurityFacilityCapturer.Length > 0)
        {
          Security = "0";
          SecurityRole = "152";
        }
      }

      HiddenField_EditSecurityRole.Value = SecurityRole;
    }

    protected void HiddenField_EditAdmin_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_EditAdmin = (HiddenField)sender;
      string Admin = "No";

      if (Request.QueryString["CRM_Id"] != null)
      {
        string SecurityRole = "";
        HiddenField HiddenField_EditSecurityRole = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_EditSecurityRole");
        SecurityRole = HiddenField_EditSecurityRole.Value;

        if (SecurityRole == "1" || SecurityRole == "146")
        {
          Admin = "Yes";
        }
      }

      HiddenField_EditAdmin.Value = Admin;
    }

    protected void TextBox_EditEscalatedReportNumber_TextChanged(object sender, EventArgs e)
    {
      string EscalatedReportNumberErrorMessage = "";
      TextBox TextBox_EditEscalatedReportNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditEscalatedReportNumber");
      TextBox TextBox_EditComplaintDescription = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditComplaintDescription");
      Label Label_EditEscalatedReportNumberError = (Label)FormView_CRM_Form.FindControl("Label_EditEscalatedReportNumberError");

      if (string.IsNullOrEmpty(TextBox_EditEscalatedReportNumber.Text.ToString()))
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_EditEscalatedReportNumber);
        EscalatedReportNumberErrorMessage = "";
      }
      else
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_EditComplaintDescription);

        Session["CRMReportNumber"] = "";
        string SQLStringCRMReportNumber = "SELECT DISTINCT CRM_ReportNumber FROM Form_CRM WHERE CRM_ReportNumber IS NOT NULL AND CRM_ReportNumber = @CRM_ReportNumber AND CRM_Status = 'Approved' AND CRM_Id != @CRM_Id AND Facility_Id = (SELECT Facility_Id FROM Form_CRM WHERE CRM_Id = @CRM_Id)";
        using (SqlCommand SqlCommand_CRMReportNumber = new SqlCommand(SQLStringCRMReportNumber))
        {
          SqlCommand_CRMReportNumber.Parameters.AddWithValue("@CRM_ReportNumber", TextBox_EditEscalatedReportNumber.Text.ToString());
          SqlCommand_CRMReportNumber.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
          DataTable DataTable_CRMReportNumber;
          using (DataTable_CRMReportNumber = new DataTable())
          {
            DataTable_CRMReportNumber.Locale = CultureInfo.CurrentCulture;
            DataTable_CRMReportNumber = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMReportNumber).Copy();
            if (DataTable_CRMReportNumber.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_CRMReportNumber.Rows)
              {
                Session["CRMReportNumber"] = DataRow_Row["CRM_ReportNumber"];
              }
            }
          }
        }

        if (string.IsNullOrEmpty(Session["CRMReportNumber"].ToString()))
        {
          ToolkitScriptManager_CRM.SetFocus(TextBox_EditEscalatedReportNumber);
          EscalatedReportNumberErrorMessage = EscalatedReportNumberErrorMessage + Convert.ToString("Report Number " + TextBox_EditEscalatedReportNumber.Text.ToString() + " does not exist for this facility", CultureInfo.CurrentCulture);
        }
      }

      Label_EditEscalatedReportNumberError.Text = EscalatedReportNumberErrorMessage;

      EditRegisterPostBackControl();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] ServiceMethod_EditEscalatedReportNumber(string prefixText, int count, string contextKey)
    {
      if (string.IsNullOrEmpty(contextKey))
      {
        List<string> List_Items = new List<string>(count);

        DataTable DataTable_EscalatedReportNumber;
        using (DataTable_EscalatedReportNumber = new DataTable())
        {
          DataTable_EscalatedReportNumber.Locale = CultureInfo.CurrentCulture;

          string SQLStringReportNumber = "SELECT DISTINCT CRM_ReportNumber FROM Form_CRM WHERE CRM_Type_List = 4395 AND CRM_ReportNumber IS NOT NULL AND CRM_ReportNumber LIKE @CRM_ReportNumber + '%' AND CRM_Status = 'Approved' AND Facility_Id = @Facility_Id AND CRM_Id != @CRM_Id ORDER BY CRM_ReportNumber DESC";
          string SQLConnectionReportNumber = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

          DataTable_EscalatedReportNumber.Reset();
          DataTable_EscalatedReportNumber.Columns.Add("CRM_ReportNumber", typeof(string));

          using (SqlCommand SqlCommand_Form_CRM_GetEscalatedReportNumber = new SqlCommand(SQLStringReportNumber))
          {
            SqlCommand_Form_CRM_GetEscalatedReportNumber.Parameters.AddWithValue("@CRM_ReportNumber", prefixText);
            SqlCommand_Form_CRM_GetEscalatedReportNumber.Parameters.AddWithValue("@Facility_Id", HttpContext.Current.Session["Edit_FacilityId"]);
            SqlCommand_Form_CRM_GetEscalatedReportNumber.Parameters.AddWithValue("@CRM_Id", HttpContext.Current.Session["Edit_CRMId"]);

            using (SqlConnection SQLConnection_Form_CRM_GetEscalatedReportNumber = new SqlConnection(SQLConnectionReportNumber))
            {
              using (SqlDataAdapter SqlDataAdapter_Form_CRM_GetEscalatedReportNumber = new SqlDataAdapter())
              {
                foreach (SqlParameter SqlParameter_Value in SqlCommand_Form_CRM_GetEscalatedReportNumber.Parameters)
                {
                  if (SqlParameter_Value.Value == null)
                  {
                    SqlParameter_Value.Value = DBNull.Value;
                  }
                }

                SqlCommand_Form_CRM_GetEscalatedReportNumber.CommandType = CommandType.Text;
                SqlCommand_Form_CRM_GetEscalatedReportNumber.Connection = SQLConnection_Form_CRM_GetEscalatedReportNumber;
                SQLConnection_Form_CRM_GetEscalatedReportNumber.Open();
                SqlDataAdapter_Form_CRM_GetEscalatedReportNumber.SelectCommand = SqlCommand_Form_CRM_GetEscalatedReportNumber;
                SqlDataAdapter_Form_CRM_GetEscalatedReportNumber.Fill(DataTable_EscalatedReportNumber);
              }
            }
          }

          if (DataTable_EscalatedReportNumber.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EscalatedReportNumber.Rows)
            {
              string ReportNumber = DataRow_Row["CRM_ReportNumber"].ToString();
              List_Items.Add(ReportNumber);
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

    protected void TextBox_EditCustomerEmail_TextChanged(object sender, EventArgs e)
    {
      string EmailAddressErrorMessage = "";
      TextBox TextBox_EditCustomerEmail = (TextBox)sender;
      Label Label_EditCustomerEmailError = (Label)FormView_CRM_Form.FindControl("Label_EditCustomerEmailError");
      TextBox TextBox_EditCustomerContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditCustomerContactNumber");

      if (string.IsNullOrEmpty(TextBox_EditCustomerEmail.Text.ToString()))
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_EditCustomerEmail);
        EmailAddressErrorMessage = "";
      }
      else
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_EditCustomerContactNumber);

        string EmailTextBox = TextBox_EditCustomerEmail.Text.ToString();
        EmailTextBox = EmailTextBox.Replace(";", Convert.ToString(",", CultureInfo.CurrentCulture));
        EmailTextBox = EmailTextBox.Replace(":", Convert.ToString(",", CultureInfo.CurrentCulture));

        string EmailTextBoxSplit = EmailTextBox;
        string[] EmailTextBoxSplitEmails = EmailTextBoxSplit.Split(',');

        foreach (string EmailTextBoxSplitEmail in EmailTextBoxSplitEmails)
        {
          InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
          string ValidEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailTextBoxSplitEmail);

          if (ValidEmailAddress == "No")
          {
            ToolkitScriptManager_CRM.SetFocus(TextBox_EditCustomerEmail);
            EmailAddressErrorMessage = EmailAddressErrorMessage + Convert.ToString("Email Address " + EmailTextBoxSplitEmail + " is not a valid Email address<br />", CultureInfo.CurrentCulture);
          }
        }

        TextBox_EditCustomerEmail.Text = EmailTextBox;
      }

      Label_EditCustomerEmailError.Text = EmailAddressErrorMessage;


      EditRegisterPostBackControl();
    }

    protected void Button_EditFindPatient_OnClick(object sender, EventArgs e)
    {
      string PatientErrorMessage = "";

      DataView DataView_Facility = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_Facility = DataView_Facility[0];
      string EditFacility = Convert.ToString(DataRowView_Facility["Facility_Id"], CultureInfo.CurrentCulture);

      TextBox TextBox_EditPatientVisitNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientVisitNumber");
      TextBox TextBox_EditPatientName = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientName");
      TextBox TextBox_EditPatientDateOfAdmission = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientDateOfAdmission");
      TextBox TextBox_EditPatientEmail = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientEmail");
      TextBox TextBox_EditPatientContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientContactNumber");
      Label Label_EditPatientError = (Label)FormView_CRM_Form.FindControl("Label_EditPatientError");

      if (string.IsNullOrEmpty(TextBox_EditPatientVisitNumber.Text))
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_EditPatientVisitNumber);
        PatientErrorMessage = PatientErrorMessage + Convert.ToString("Visit Number not provided to find Patient Information<br />", CultureInfo.CurrentCulture);
      }
      else
      {
        if (string.IsNullOrEmpty(EditFacility))
        {
          ToolkitScriptManager_CRM.SetFocus(TextBox_EditPatientVisitNumber);
          PatientErrorMessage = PatientErrorMessage + Convert.ToString("Facility not Selected to find Patient Information<br />", CultureInfo.CurrentCulture);
        }
        else
        {
          Session["NameSurname"] = "";
          Session["DateofAdmission"] = "";
          Session["Email"] = "";
          Session["ContactNumber"] = "";
          DataTable DataTable_DataPatient;
          using (DataTable_DataPatient = new DataTable())
          {
            DataTable_DataPatient.Locale = CultureInfo.CurrentCulture;
            //DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(EditFacility, TextBox_EditPatientVisitNumber.Text).Copy();
            DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(EditFacility, TextBox_EditPatientVisitNumber.Text).Copy();

            if (DataTable_DataPatient.Columns.Count == 1)
            {
              foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
              {
                PatientErrorMessage = PatientErrorMessage + DataRow_Row["Error"];
              }
            }
            else if (DataTable_DataPatient.Columns.Count != 1)
            {
              if (DataTable_DataPatient.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
                {
                  Session["NameSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];
                  Session["DateofAdmission"] = DataRow_Row["DateOfAdmission"];
                  Session["Email"] = DataRow_Row["Email"];
                  Session["ContactNumber"] = DataRow_Row["ContactNumber"];

                  string NameSurname = Session["NameSurname"].ToString();
                  NameSurname = NameSurname.Replace("'", "");
                  Session["NameSurname"] = NameSurname;
                  NameSurname = "";

                  string Email = Session["Email"].ToString();
                  Email = Email.Replace("'", "");
                  Session["Email"] = Email;
                  Email = "";

                  PatientErrorMessage = "";

                  TextBox_EditPatientName.Text = Session["NameSurname"].ToString();
                  TextBox_EditPatientDateOfAdmission.Text = Session["DateofAdmission"].ToString();
                  TextBox_EditPatientEmail.Text = Session["Email"].ToString();
                  TextBox_EditPatientContactNumber.Text = Session["ContactNumber"].ToString();
                }
              }
              else
              {
                PatientErrorMessage = PatientErrorMessage + Convert.ToString("Patient Information not found for specific Patient Visit Number,<br />Please type in the Patient Information<br />", CultureInfo.CurrentCulture);
              }
            }
          }

          Session["NameSurname"] = "";
          Session["DateofAdmission"] = "";
          Session["Email"] = "";
          Session["ContactNumber"] = "";
        }
      }

      Label_EditPatientError.Text = PatientErrorMessage;


      EditRegisterPostBackControl();
    }

    protected void TextBox_EditPatientName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditPatientName = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientName");
      TextBox TextBox_EditPatientDateOfAdmission = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientDateOfAdmission");
      Label Label_EditPatientError = (Label)FormView_CRM_Form.FindControl("Label_EditPatientError");

      if (!string.IsNullOrEmpty(TextBox_EditPatientName.Text))
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_EditPatientDateOfAdmission);
        Label_EditPatientError.Text = "";
      }


      EditRegisterPostBackControl();
    }

    protected void TextBox_EditPatientEmail_TextChanged(object sender, EventArgs e)
    {
      string PatientEmailErrorMessage = "";
      TextBox TextBox_EditPatientEmail = (TextBox)sender;
      Label Label_EditPatientEmailError = (Label)FormView_CRM_Form.FindControl("Label_EditPatientEmailError");
      TextBox TextBox_EditPatientContactNumber = (TextBox)FormView_CRM_Form.FindControl("TextBox_EditPatientContactNumber");

      if (string.IsNullOrEmpty(TextBox_EditPatientEmail.Text.ToString()))
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_EditPatientEmail);
        PatientEmailErrorMessage = "";
      }
      else
      {
        ToolkitScriptManager_CRM.SetFocus(TextBox_EditPatientContactNumber);

        string EmailTextBox = TextBox_EditPatientEmail.Text.ToString();
        EmailTextBox = EmailTextBox.Replace(";", Convert.ToString(",", CultureInfo.CurrentCulture));
        EmailTextBox = EmailTextBox.Replace(":", Convert.ToString(",", CultureInfo.CurrentCulture));

        string EmailTextBoxSplit = EmailTextBox;
        string[] EmailTextBoxSplitEmails = EmailTextBoxSplit.Split(',');

        foreach (string EmailTextBoxSplitEmail in EmailTextBoxSplitEmails)
        {
          InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
          string ValidEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailTextBoxSplitEmail);

          if (ValidEmailAddress == "No")
          {
            ToolkitScriptManager_CRM.SetFocus(TextBox_EditPatientEmail);
            PatientEmailErrorMessage = PatientEmailErrorMessage + Convert.ToString("Email Address " + EmailTextBoxSplitEmail + " is not a valid Email address<br />", CultureInfo.CurrentCulture);
          }
        }

        TextBox_EditPatientEmail.Text = EmailTextBox;
      }

      Label_EditPatientEmailError.Text = PatientEmailErrorMessage;


      EditRegisterPostBackControl();
    }

    protected void DropDownList_EditComplaintPriorityList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditComplaintPriorityList = (DropDownList)sender;
      HiddenField HiddenField_EditSecurityRole = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_EditSecurityRole");
      CheckBox CheckBox_EditComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplaintCloseOut");
      DropDownList DropDownList_EditStatus = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditStatus");
      Label Label_EditComplaintCloseOut = (Label)FormView_CRM_Form.FindControl("Label_EditComplaintCloseOut");

      if (DropDownList_EditStatus.SelectedValue == "Approved")
      {
        if (string.IsNullOrEmpty(DropDownList_EditComplaintPriorityList.SelectedValue))
        {
          CheckBox_EditComplaintCloseOut.Checked = false;
          CheckBox_EditComplaintCloseOut.Visible = false;
          Label_EditComplaintCloseOut.Visible = true;
        }
        else if (DropDownList_EditComplaintPriorityList.SelectedValue == "4400")
        {
          if (HiddenField_EditSecurityRole.Value == "1" || HiddenField_EditSecurityRole.Value == "146" || HiddenField_EditSecurityRole.Value == "150")
          {
            CheckBox_EditComplaintCloseOut.Visible = true;
            Label_EditComplaintCloseOut.Visible = false;
          }
          else
          {
            CheckBox_EditComplaintCloseOut.Checked = false;
            CheckBox_EditComplaintCloseOut.Visible = false;
            Label_EditComplaintCloseOut.Visible = true;
          }
        }
        else if (DropDownList_EditComplaintPriorityList.SelectedValue == "4401")
        {
          if (HiddenField_EditSecurityRole.Value == "1" || HiddenField_EditSecurityRole.Value == "146" || HiddenField_EditSecurityRole.Value == "148")
          {
            CheckBox_EditComplaintCloseOut.Visible = true;
            Label_EditComplaintCloseOut.Visible = false;
          }
          else
          {
            CheckBox_EditComplaintCloseOut.Checked = false;
            CheckBox_EditComplaintCloseOut.Visible = false;
            Label_EditComplaintCloseOut.Visible = true;
          }
        }
        else
        {
          CheckBox_EditComplaintCloseOut.Checked = false;
          CheckBox_EditComplaintCloseOut.Visible = false;
          Label_EditComplaintCloseOut.Visible = true;
        }
      }
      else
      {
        CheckBox_EditComplaintCloseOut.Checked = false;
        CheckBox_EditComplaintCloseOut.Visible = false;
        Label_EditComplaintCloseOut.Visible = true;
      }

      ToolkitScriptManager_CRM.SetFocus(DropDownList_EditComplaintPriorityList);

      EditRegisterPostBackControl();
    }

    protected void CheckBoxList_EditComplaintCategoryItemList_DataBound(object sender, EventArgs e)
    {
      CheckBoxList CheckBoxList_EditComplaintCategoryItemList = (CheckBoxList)sender;

      for (int i = 0; i < CheckBoxList_EditComplaintCategoryItemList.Items.Count; i++)
      {
        Session["CRMComplaintCategoryItemList"] = "";
        string SQLStringCRMComplaintCategoryItemList = "SELECT DISTINCT CRM_Complaint_Category_Item_List FROM Form_CRM_Complaint_Category WHERE CRM_Id = @CRM_Id AND CRM_Complaint_Category_Item_List = @CRM_Complaint_Category_Item_List";
        using (SqlCommand SqlCommand_CRMComplaintCategoryItemList = new SqlCommand(SQLStringCRMComplaintCategoryItemList))
        {
          SqlCommand_CRMComplaintCategoryItemList.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
          SqlCommand_CRMComplaintCategoryItemList.Parameters.AddWithValue("@CRM_Complaint_Category_Item_List", CheckBoxList_EditComplaintCategoryItemList.Items[i].Value);
          DataTable DataTable_CRMComplaintCategoryItemList;
          using (DataTable_CRMComplaintCategoryItemList = new DataTable())
          {
            DataTable_CRMComplaintCategoryItemList.Locale = CultureInfo.CurrentCulture;
            DataTable_CRMComplaintCategoryItemList = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMComplaintCategoryItemList).Copy();
            if (DataTable_CRMComplaintCategoryItemList.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_CRMComplaintCategoryItemList.Rows)
              {
                Session["CRMComplaintCategoryItemList"] = DataRow_Row["CRM_Complaint_Category_Item_List"];
                CheckBoxList_EditComplaintCategoryItemList.Items[i].Selected = true;
              }
            }
          }
        }

        Session.Remove("CRMComplaintCategoryItemList");
      }
    }

    protected void HiddenField_EditComplaintCategoryItemListTotal_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_EditComplaintCategoryItemListTotal = (HiddenField)sender;
      CheckBoxList CheckBoxList_EditComplaintCategoryItemList = (CheckBoxList)FormView_CRM_Form.FindControl("CheckBoxList_EditComplaintCategoryItemList");
      HiddenField_EditComplaintCategoryItemListTotal.Value = CheckBoxList_EditComplaintCategoryItemList.Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void Button_EditConvertToComplaint_DataBinding(object sender, EventArgs e)
    {
      Button Button_EditConvertToComplaint = (Button)sender;
      Button_EditConvertToComplaint.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Convert the Comment to a Complaint');");
    }

    protected void Button_EditConvertToComplaint_OnClick(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(Request.QueryString["CRM_Id"]))
      {
        Label Label_EditModifiedDate = (Label)FormView_CRM_Form.FindControl("Label_EditModifiedDate");
        Session["OLDCRMModifiedDate"] = Label_EditModifiedDate.Text;
        object OLDCRMModifiedDate = Session["OLDCRMModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDCRMModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareCRM = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareCRM = DataView_CompareCRM[0];
        Session["DBCRMModifiedDate"] = Convert.ToString(DataRowView_CompareCRM["CRM_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBCRMModifiedBy"] = Convert.ToString(DataRowView_CompareCRM["CRM_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBCRMModifiedDate = Session["DBCRMModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBCRMModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          ToolkitScriptManager_CRM.SetFocus(UpdatePanel_CRM);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBCRMModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_CRM_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_CRM_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;

          EditRegisterPostBackControl();
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_CRM", "CRM_Id = " + Request.QueryString["CRM_Id"]);

          DataView DataView_CRM = (DataView)SqlDataSource_CRM_Form.Select(DataSourceSelectArguments.Empty);
          DataRowView DataRowView_CRM = DataView_CRM[0];
          Session["CRMHistory"] = Convert.ToString(DataRowView_CRM["CRM_History"], CultureInfo.CurrentCulture);

          Session["CRMHistory"] = Session["History"].ToString() + Session["CRMHistory"].ToString();

          string SQLStringConvertCRM = "UPDATE Form_CRM SET CRM_Type_List = 4395 , CRM_Complaint_Description = CRM_Comment_Description , CRM_Comment_Description = NULL , CRM_Complaint_ContactPatient = CRM_Comment_ContactPatient , CRM_Comment_ContactPatient = NULL , CRM_Complaint_Unit_Id = CRM_Comment_Unit_Id , CRM_Comment_Unit_Id = NULL , CRM_Comment_Type_List = NULL , CRM_Comment_Category_List = NULL , CRM_Comment_AdditionalUnit_Id = NULL , CRM_Comment_AdditionalType_List = NULL , CRM_Comment_AdditionalCategory_List = NULL , CRM_Comment_Acknowledge = 0 , CRM_Comment_AcknowledgeDate = NULL , CRM_Comment_AcknowledgeBy = NULL , CRM_Comment_CloseOut = 0 , CRM_Comment_CloseOutDate = NULL , CRM_Comment_CloseOutBy = NULL , CRM_ModifiedDate =  @CRM_ModifiedDate , CRM_ModifiedBy = @CRM_ModifiedBy , CRM_History = @CRM_History WHERE CRM_Id = @CRM_Id";
          using (SqlCommand SqlCommand_ConvertCRM = new SqlCommand(SQLStringConvertCRM))
          {
            SqlCommand_ConvertCRM.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_ConvertCRM.Parameters.AddWithValue("@CRM_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_ConvertCRM.Parameters.AddWithValue("@CRM_History", Session["CRMHistory"].ToString());
            SqlCommand_ConvertCRM.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_ConvertCRM);
          }

          Session.Remove("CRMHistory");
          Session.Remove("History");

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Form", "" + Request.RawUrl + ""), true);
        }
      }
    }

    protected void Button_EditSurveyResults_OnClick(object sender, EventArgs e)
    {
      PXM_PDCH_Results();
    }

    protected void DropDownList_EditRouteFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((RadioButtonList)FormView_CRM_Form.FindControl("RadioButtonList_EditRouteUnit")).Items.Clear();

      SqlDataSource_CRM_EditRouteUnit.SelectParameters["ListItem_ParentName"].DefaultValue = ((DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditRouteFacility")).SelectedValue;

      ((RadioButtonList)FormView_CRM_Form.FindControl("RadioButtonList_EditRouteUnit")).DataBind();

      ((HiddenField)FormView_CRM_Form.FindControl("HiddenField_EditRouteUnitTotal")).Value = ((RadioButtonList)FormView_CRM_Form.FindControl("RadioButtonList_EditRouteUnit")).Items.Count.ToString(CultureInfo.CurrentCulture);

      ToolkitScriptManager_CRM.SetFocus(((RadioButtonList)FormView_CRM_Form.FindControl("RadioButtonList_EditRouteUnit")));

      EditRegisterPostBackControl();
    }

    protected void GridView_EditRouteList_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }

      for (int i = 0; i < GridView_List.Rows.Count; i++)
      {
        if (GridView_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_List.Rows[i].Cells[4].Text == "No")
          {
            GridView_List.Rows[i].Cells[4].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[4].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_List.Rows[i].Cells[4].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[4].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_EditRouteList_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void DropDownList_EditStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditStatus = (DropDownList)sender;

      CheckBox CheckBox_EditComplaintCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplaintCloseOut");
      CheckBox CheckBox_EditComplimentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentAcknowledge");
      CheckBox CheckBox_EditComplimentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditComplimentCloseOut");
      CheckBox CheckBox_EditCommentAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentAcknowledge");
      CheckBox CheckBox_EditCommentCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditCommentCloseOut");
      CheckBox CheckBox_EditQueryAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryAcknowledge");
      CheckBox CheckBox_EditQueryCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditQueryCloseOut");
      CheckBox CheckBox_EditSuggestionAcknowledge = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionAcknowledge");
      CheckBox CheckBox_EditSuggestionCloseOut = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditSuggestionCloseOut");

      Label Label_EditComplaintCloseOut = (Label)FormView_CRM_Form.FindControl("Label_EditComplaintCloseOut");
      Label Label_EditComplimentAcknowledge = (Label)FormView_CRM_Form.FindControl("Label_EditComplimentAcknowledge");
      Label Label_EditCommentAcknowledge = (Label)FormView_CRM_Form.FindControl("Label_EditCommentAcknowledge");
      Label Label_EditQueryAcknowledge = (Label)FormView_CRM_Form.FindControl("Label_EditQueryAcknowledge");
      Label Label_EditSuggestionAcknowledge = (Label)FormView_CRM_Form.FindControl("Label_EditSuggestionAcknowledge");

      CheckBox CheckBox_EditRouteRoute = (CheckBox)FormView_CRM_Form.FindControl("CheckBox_EditRouteRoute");

      CheckBox_EditComplaintCloseOut.Checked = false;
      CheckBox_EditComplaintCloseOut.Visible = false;
      CheckBox_EditComplimentAcknowledge.Checked = false;
      CheckBox_EditComplimentAcknowledge.Visible = false;
      CheckBox_EditComplimentCloseOut.Checked = false;
      CheckBox_EditComplimentCloseOut.Visible = false;
      CheckBox_EditCommentAcknowledge.Checked = false;
      CheckBox_EditCommentAcknowledge.Visible = false;
      CheckBox_EditCommentCloseOut.Checked = false;
      CheckBox_EditCommentCloseOut.Visible = false;
      CheckBox_EditQueryAcknowledge.Checked = false;
      CheckBox_EditQueryAcknowledge.Visible = false;
      CheckBox_EditQueryCloseOut.Checked = false;
      CheckBox_EditQueryCloseOut.Visible = false;
      CheckBox_EditSuggestionAcknowledge.Checked = false;
      CheckBox_EditSuggestionAcknowledge.Visible = false;
      CheckBox_EditSuggestionCloseOut.Checked = false;
      CheckBox_EditSuggestionCloseOut.Visible = false;

      Label_EditComplaintCloseOut.Visible = true;
      Label_EditComplimentAcknowledge.Visible = true;
      Label_EditCommentAcknowledge.Visible = true;
      Label_EditQueryAcknowledge.Visible = true;
      Label_EditSuggestionAcknowledge.Visible = true;

      CheckBox_EditRouteRoute.Checked = false;
      CheckBox_EditRouteRoute.Visible = false;

      FromDataBase_IsRouteComplete FromDataBase_IsRouteComplete_Current = GetIsRouteComplete();
      string IsRouteComplete = FromDataBase_IsRouteComplete_Current.IsRouteComplete;

      string SecurityRole = "";
      HiddenField HiddenField_EditSecurityRole = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_EditSecurityRole");
      SecurityRole = HiddenField_EditSecurityRole.Value;

      if (SecurityRole == "1" || SecurityRole == "146" || SecurityRole == "150" || SecurityRole == "148" || SecurityRole == "153")
      {
        if (DropDownList_EditStatus.SelectedValue == "Approved")
        {
          if (IsRouteComplete == "Yes")
          {
            CheckBox_EditComplaintCloseOut.Checked = false;
            CheckBox_EditComplaintCloseOut.Visible = false;
            CheckBox_EditComplimentAcknowledge.Visible = true;
            CheckBox_EditComplimentCloseOut.Visible = true;
            CheckBox_EditCommentAcknowledge.Visible = true;
            CheckBox_EditCommentCloseOut.Visible = true;
            CheckBox_EditQueryAcknowledge.Visible = true;
            CheckBox_EditQueryCloseOut.Visible = true;
            CheckBox_EditSuggestionAcknowledge.Visible = true;
            CheckBox_EditSuggestionCloseOut.Visible = true;

            Label_EditComplaintCloseOut.Visible = true;
            Label_EditComplimentAcknowledge.Visible = false;
            Label_EditCommentAcknowledge.Visible = false;
            Label_EditQueryAcknowledge.Visible = false;
            Label_EditSuggestionAcknowledge.Visible = false;
          }

          CheckBox_EditRouteRoute.Visible = true;
        }
      }

      DropDownList DropDownList_EditComplaintPriorityList = (DropDownList)FormView_CRM_Form.FindControl("DropDownList_EditComplaintPriorityList");
      DropDownList_EditComplaintPriorityList_SelectedIndexChanged(DropDownList_EditComplaintPriorityList, e);

      ToolkitScriptManager_CRM.SetFocus(DropDownList_EditStatus);
    }

    protected void Button_EditPrint_Click(object sender, EventArgs e)
    {
      Button_EditPrintClicked = true;
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      Button_EditEmailClicked = true;
    }

    protected void Button_EditIncompleteOther_Click(object sender, EventArgs e)
    {
      RedirectToIncompleteOther();
    }

    protected void Button_EditIncompleteComplaints_Click(object sender, EventArgs e)
    {
      RedirectToIncompleteComplaints();
    }

    protected void Button_EditCaptured_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Form", "Form_CRM.aspx"), false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    protected void ItemRegisterPostBackControl()
    {
      Button Button_ItemSurveyResults = (Button)FormView_CRM_Form.FindControl("Button_ItemSurveyResults");

      ScriptManager ScriptManager_Item = ScriptManager.GetCurrent(Page);

      if (Button_ItemSurveyResults != null)
      {
        ScriptManager_Item.RegisterPostBackControl(Button_ItemSurveyResults);
      }
    }

    protected void HiddenField_ItemSecurityRole_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_ItemSecurityRole = (HiddenField)sender;
      string SecurityRole = "";

      if (Request.QueryString["CRM_Id"] != null)
      {
        FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
        DataRow[] SecurityFacilityAdminHospitalManagerUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminHospitalManagerUpdate;
        DataRow[] SecurityFacilityAdminNSMUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminNSMUpdate;
        DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;
        DataRow[] SecurityFacilityInvestigator = FromDataBase_SecurityRole_Current.SecurityFacilityInvestigator;
        DataRow[] SecurityFacilityApprover = FromDataBase_SecurityRole_Current.SecurityFacilityApprover;
        DataRow[] SecurityFacilityCapturer = FromDataBase_SecurityRole_Current.SecurityFacilityCapturer;

        string Security = "1";
        if (Security == "1" && SecurityAdmin.Length > 0)
        {
          Security = "0";
          SecurityRole = "1";
        }

        if (Security == "1" && SecurityFormAdminUpdate.Length > 0)
        {
          Security = "0";
          SecurityRole = "146";
        }

        if (Security == "1" && SecurityFormAdminView.Length > 0)
        {
          Security = "0";
          SecurityRole = "147";
        }

        if (Security == "1" && SecurityFacilityAdminHospitalManagerUpdate.Length > 0)
        {
          Security = "0";
          SecurityRole = "150";
        }

        if (Security == "1" && SecurityFacilityAdminNSMUpdate.Length > 0)
        {
          Security = "0";
          SecurityRole = "148";
        }

        if (Security == "1" && SecurityFacilityAdminView.Length > 0)
        {
          Security = "0";
          SecurityRole = "149";
        }

        if (Security == "1" && SecurityFacilityInvestigator.Length > 0)
        {
          Security = "0";
          SecurityRole = "153";
        }

        if (Security == "1" && SecurityFacilityApprover.Length > 0)
        {
          Security = "0";
          SecurityRole = "151";
        }

        if (Security == "1" && SecurityFacilityCapturer.Length > 0)
        {
          Security = "0";
          SecurityRole = "152";
        }
      }

      HiddenField_ItemSecurityRole.Value = SecurityRole;
    }

    protected void HiddenField_ItemAdmin_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_ItemAdmin = (HiddenField)sender;
      string Admin = "No";

      if (Request.QueryString["CRM_Id"] != null)
      {
        string SecurityRole = "";
        HiddenField HiddenField_ItemSecurityRole = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_ItemSecurityRole");
        SecurityRole = HiddenField_ItemSecurityRole.Value;

        if (SecurityRole == "1" || SecurityRole == "146")
        {
          Admin = "Yes";
        }
      }

      HiddenField_ItemAdmin.Value = Admin;
    }

    protected void Button_ItemSurveyResults_OnClick(object sender, EventArgs e)
    {
      PXM_PDCH_Results();
    }

    protected void SqlDataSource_CRM_ItemComplaintCategory_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        GridView GridView_ItemComplaintCategory = (GridView)FormView_CRM_Form.FindControl("GridView_ItemComplaintCategory");

        if (TotalRecords > 0)
        {
          GridView_ItemComplaintCategory.Visible = true;
        }
        else
        {
          GridView_ItemComplaintCategory.Visible = false;
        }
      }
    }

    protected void GridView_ItemComplaintCategory_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemComplaintCategory_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_ItemRouteList_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }

      for (int i = 0; i < GridView_List.Rows.Count; i++)
      {
        if (GridView_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_List.Rows[i].Cells[4].Text == "No")
          {
            GridView_List.Rows[i].Cells[4].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[4].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_List.Rows[i].Cells[4].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[4].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_ItemRouteList_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_ItemIncompleteOther_Click(object sender, EventArgs e)
    {
      RedirectToIncompleteOther();
    }

    protected void Button_ItemIncompleteComplaints_Click(object sender, EventArgs e)
    {
      RedirectToIncompleteComplaints();
    }

    protected void Button_ItemCaptured_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Form", "Form_CRM.aspx"), false);
    }
    //---END--- --Item Controls--//


    //--START-- --File--//
    public static string DatabaseFileName(object crm_File_Name)
    {
      string DatabaseFileName = "";
      if (crm_File_Name != null)
      {
        DatabaseFileName = "" + crm_File_Name.ToString() + "";
      }

      return DatabaseFileName;
    }

    protected void RetrieveDatabaseFile(object sender, EventArgs e)
    {
      LinkButton LinkButton_CRMFile = (LinkButton)sender;
      string FileId = LinkButton_CRMFile.CommandArgument.ToString();

      Session["CRMFileName"] = "";
      Session["CRMFileContentType"] = "";
      Session["CRMFileData"] = "";
      string SQLStringCRMFile = "SELECT CRM_File_Name ,CRM_File_ContentType ,CRM_File_Data FROM Form_CRM_File WHERE CRM_File_Id = @CRM_File_Id";
      using (SqlCommand SqlCommand_CRMFile = new SqlCommand(SQLStringCRMFile))
      {
        SqlCommand_CRMFile.Parameters.AddWithValue("@CRM_File_Id", FileId);
        DataTable DataTable_CRMFile;
        using (DataTable_CRMFile = new DataTable())
        {
          DataTable_CRMFile.Locale = CultureInfo.CurrentCulture;
          DataTable_CRMFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMFile).Copy();
          if (DataTable_CRMFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CRMFile.Rows)
            {
              Session["CRMFileName"] = DataRow_Row["CRM_File_Name"];
              Session["CRMFileContentType"] = DataRow_Row["CRM_File_ContentType"];
              Session["CRMFileData"] = DataRow_Row["CRM_File_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["CRMFileData"].ToString()))
      {
        Byte[] Byte_FileData = (Byte[])Session["CRMFileData"];
        //FileStream FileStream_File = new FileStream(Server.MapPath("App_Files/Form_CRM_DiscoveryComment_Upload/") + Session["CRMFileName"].ToString(), FileMode.Append);
        //FileStream_File.Write(Byte_FileData, 0, Byte_FileData.Length);
        //FileStream_File.Close();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = Session["CRMFileContentType"].ToString();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Session["CRMFileName"].ToString() + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      Session["CRMFileName"] = "";
      Session["CRMFileContentType"] = "";
      Session["CRMFileData"] = "";
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
    //---END--- --File--//


    //--START-- --File Insert--//
    protected void SqlDataSource_CRM_InsertFile_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        HiddenField HiddenField_InsertFile = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertFile");
        HiddenField_InsertFile.Value = TotalRecords.ToString(CultureInfo.CurrentCulture);

        GridView GridView_InsertFile = (GridView)FormView_CRM_Form.FindControl("GridView_InsertFile");

        if (TotalRecords > 0)
        {
          GridView_InsertFile.Visible = true;
        }
        else
        {
          GridView_InsertFile.Visible = false;
        }
      }
    }

    protected void GridView_InsertFile_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_InsertFile_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_InsertUploadFile_OnClick(object sender, EventArgs e)
    {
      string UploadMessage = "";

      Label Label_InsertMessageFile = (Label)FormView_CRM_Form.FindControl("Label_InsertMessageFile");
      FileUpload FileUpload_InsertFile = (FileUpload)FormView_CRM_Form.FindControl("FileUpload_InsertFile");

      if (!FileUpload_InsertFile.HasFiles)
      {
        UploadMessage = UploadMessage + Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>No file chosen</span>", CultureInfo.CurrentCulture);
      }
      else
      {
        foreach (HttpPostedFile HttpPostedFile_File in FileUpload_InsertFile.PostedFiles)
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
            HiddenField HiddenField_InsertCRMIdTemp = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_InsertCRMIdTemp");

            Session["CRMFileId"] = "";
            string SQLStringExistingFile = "SELECT CRM_File_Id FROM Form_CRM_File WHERE CRM_File_CreatedBy = @CRM_File_CreatedBy AND CRM_File_Temp_CRM_Id = @CRM_File_Temp_CRM_Id AND CRM_File_Name = @CRM_File_Name";
            using (SqlCommand SqlCommand_ExistingFile = new SqlCommand(SQLStringExistingFile))
            {
              SqlCommand_ExistingFile.Parameters.AddWithValue("@CRM_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@CRM_File_Temp_CRM_Id", HiddenField_InsertCRMIdTemp.Value);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@CRM_File_Name", FileName);
              DataTable DataTable_ExistingFile;
              using (DataTable_ExistingFile = new DataTable())
              {
                DataTable_ExistingFile.Locale = CultureInfo.CurrentCulture;
                DataTable_ExistingFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ExistingFile).Copy();
                if (DataTable_ExistingFile.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_ExistingFile.Rows)
                  {
                    Session["CRMFileId"] = DataRow_Row["CRM_File_Id"];
                  }
                }
              }
            }

            if (!string.IsNullOrEmpty(Session["CRMFileId"].ToString()))
            {
              UploadMessage = Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span>", CultureInfo.CurrentCulture);
            }
            else
            {
              Stream Stream_File = HttpPostedFile_File.InputStream;
              BinaryReader BinaryReader_File = new BinaryReader(Stream_File);
              Byte[] Byte_File = BinaryReader_File.ReadBytes((Int32)Stream_File.Length);

              string SQLStringInsertFile = "INSERT INTO Form_CRM_File ( CRM_Id ,CRM_File_Temp_CRM_Id ,CRM_File_Name ,CRM_File_ContentType ,CRM_File_Data ,CRM_File_CreatedDate ,CRM_File_CreatedBy ) VALUES ( @CRM_Id ,@CRM_File_Temp_CRM_Id ,@CRM_File_Name ,@CRM_File_ContentType ,@CRM_File_Data ,@CRM_File_CreatedDate ,@CRM_File_CreatedBy )";
              using (SqlCommand SqlCommand_InsertFile = new SqlCommand(SQLStringInsertFile))
              {
                SqlCommand_InsertFile.Parameters.AddWithValue("@CRM_Id", DBNull.Value);
                SqlCommand_InsertFile.Parameters.AddWithValue("@CRM_File_Temp_CRM_Id", HiddenField_InsertCRMIdTemp.Value);
                SqlCommand_InsertFile.Parameters.AddWithValue("@CRM_File_Name", FileName);
                SqlCommand_InsertFile.Parameters.AddWithValue("@CRM_File_ContentType", FileContentTypeValue);
                SqlCommand_InsertFile.Parameters.AddWithValue("@CRM_File_Data", Byte_File);
                SqlCommand_InsertFile.Parameters.AddWithValue("@CRM_File_CreatedDate", DateTime.Now);
                SqlCommand_InsertFile.Parameters.AddWithValue("@CRM_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertFile);
              }

              GridView GridView_InsertFile = (GridView)FormView_CRM_Form.FindControl("GridView_InsertFile");
              SqlDataSource_CRM_File_InsertFile.SelectParameters["CRM_File_Temp_CRM_Id"].DefaultValue = HiddenField_InsertCRMIdTemp.Value;
              GridView_InsertFile.DataBind();
            }
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

      Button Button_InsertAdd = (Button)FormView_CRM_Form.FindControl("Button_InsertAdd");

      ToolkitScriptManager_CRM.SetFocus(Button_InsertAdd);
      InsertRegisterPostBackControl();
      Label_InsertMessageFile.Text = UploadMessage;
    }

    protected void Button_InsertDeleteFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      GridView GridView_InsertFile = (GridView)FormView_CRM_Form.FindControl("GridView_InsertFile");
      Label Label_InsertMessageFile = (Label)FormView_CRM_Form.FindControl("Label_InsertMessageFile");

      for (int i = 0; i < GridView_InsertFile.Rows.Count; i++)
      {
        CheckBox CheckBox_InsertFile = (CheckBox)GridView_InsertFile.Rows[i].Cells[0].FindControl("CheckBox_InsertFile");
        Int32 FileId = 0;

        if (CheckBox_InsertFile.Checked == true)
        {
          FileId = Convert.ToInt32(CheckBox_InsertFile.CssClass, CultureInfo.CurrentCulture);

          string SQLStringCRMFile = "DELETE FROM Form_CRM_File WHERE CRM_File_Id = @CRM_File_Id";
          using (SqlCommand SqlCommand_CRMFile = new SqlCommand(SQLStringCRMFile))
          {
            SqlCommand_CRMFile.Parameters.AddWithValue("@CRM_File_Id", FileId);

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_CRMFile);

            DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
          }
        }
      }

      Button Button_InsertAdd = (Button)FormView_CRM_Form.FindControl("Button_InsertAdd");

      ToolkitScriptManager_CRM.SetFocus(Button_InsertAdd);
      InsertRegisterPostBackControl();
      Label_InsertMessageFile.Text = DeleteMessage;
      GridView_InsertFile.DataBind();
    }

    protected void Button_InsertDeleteAllFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      GridView GridView_InsertFile = (GridView)FormView_CRM_Form.FindControl("GridView_InsertFile");
      Label Label_InsertMessageFile = (Label)FormView_CRM_Form.FindControl("Label_InsertMessageFile");

      for (int i = 0; i < GridView_InsertFile.Rows.Count; i++)
      {
        CheckBox CheckBox_InsertFile = (CheckBox)GridView_InsertFile.Rows[i].Cells[0].FindControl("CheckBox_InsertFile");
        Int32 FileId = 0;

        FileId = Convert.ToInt32(CheckBox_InsertFile.CssClass, CultureInfo.CurrentCulture);

        string SQLStringCRMFile = "DELETE FROM Form_CRM_File WHERE CRM_File_Id = @CRM_File_Id";
        using (SqlCommand SqlCommand_CRMFile = new SqlCommand(SQLStringCRMFile))
        {
          SqlCommand_CRMFile.Parameters.AddWithValue("@CRM_File_Id", FileId);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_CRMFile);

          DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
        }
      }

      Button Button_InsertAdd = (Button)FormView_CRM_Form.FindControl("Button_InsertAdd");

      ToolkitScriptManager_CRM.SetFocus(Button_InsertAdd);
      InsertRegisterPostBackControl();
      Label_InsertMessageFile.Text = DeleteMessage;
      GridView_InsertFile.DataBind();
    }

    protected void Button_InsertUploadFile_DataBinding(object sender, EventArgs e)
    {
      InsertRegisterPostBackControl();
    }

    protected void Button_InsertDeleteFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_InsertDeleteFile = (Button)sender;
      ScriptManager ScriptManager_InsertDeleteFile = ScriptManager.GetCurrent(Page);
      ScriptManager_InsertDeleteFile.RegisterPostBackControl(Button_InsertDeleteFile);
    }

    protected void Button_InsertDeleteAllFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_InsertDeleteAllFile = (Button)sender;
      ScriptManager ScriptManager_InsertDeleteAllFile = ScriptManager.GetCurrent(Page);
      ScriptManager_InsertDeleteAllFile.RegisterPostBackControl(Button_InsertDeleteAllFile);
    }

    protected void LinkButton_InsertFile_DataBinding(object sender, EventArgs e)
    {
      if (e != null)
      {
        LinkButton LinkButton_InsertFile = (LinkButton)sender;
        ScriptManager ScriptManager_InsertFile = ScriptManager.GetCurrent(Page);
        ScriptManager_InsertFile.RegisterPostBackControl(LinkButton_InsertFile);
      }
    }
    //---END--- --File Insert--//


    //--START-- --File Edit--//
    protected void SqlDataSource_CRM_EditFile_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        HiddenField HiddenField_EditFile = (HiddenField)FormView_CRM_Form.FindControl("HiddenField_EditFile");
        HiddenField_EditFile.Value = TotalRecords.ToString(CultureInfo.CurrentCulture);

        GridView GridView_EditFile = (GridView)FormView_CRM_Form.FindControl("GridView_EditFile");

        if (TotalRecords > 0)
        {
          GridView_EditFile.Visible = true;
        }
        else
        {
          GridView_EditFile.Visible = false;
        }
      }
    }

    protected void GridView_EditFile_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_EditFile_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_EditUploadFile_OnClick(object sender, EventArgs e)
    {
      string UploadMessage = "";

      Label Label_EditMessageFile = (Label)FormView_CRM_Form.FindControl("Label_EditMessageFile");
      FileUpload FileUpload_EditFile = (FileUpload)FormView_CRM_Form.FindControl("FileUpload_EditFile");

      if (!FileUpload_EditFile.HasFiles)
      {
        UploadMessage = UploadMessage + Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>No file chosen</span>", CultureInfo.CurrentCulture);
      }
      else
      {
        foreach (HttpPostedFile HttpPostedFile_File in FileUpload_EditFile.PostedFiles)
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
            Session["CRMFileId"] = "";
            string SQLStringExistingFile = "SELECT CRM_File_Id FROM Form_CRM_File WHERE CRM_File_CreatedBy = @CRM_File_CreatedBy AND CRM_Id = @CRM_Id AND CRM_File_Name = @CRM_File_Name";
            using (SqlCommand SqlCommand_ExistingFile = new SqlCommand(SQLStringExistingFile))
            {
              SqlCommand_ExistingFile.Parameters.AddWithValue("@CRM_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@CRM_File_Name", FileName);
              DataTable DataTable_ExistingFile;
              using (DataTable_ExistingFile = new DataTable())
              {
                DataTable_ExistingFile.Locale = CultureInfo.CurrentCulture;
                DataTable_ExistingFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ExistingFile).Copy();
                if (DataTable_ExistingFile.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_ExistingFile.Rows)
                  {
                    Session["CRMFileId"] = DataRow_Row["CRM_File_Id"];
                  }
                }
              }
            }

            if (!string.IsNullOrEmpty(Session["CRMFileId"].ToString()))
            {
              UploadMessage = Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span>", CultureInfo.CurrentCulture);
            }
            else
            {
              Stream Stream_File = HttpPostedFile_File.InputStream;
              BinaryReader BinaryReader_File = new BinaryReader(Stream_File);
              Byte[] Byte_File = BinaryReader_File.ReadBytes((Int32)Stream_File.Length);

              string SQLStringEditFile = "INSERT INTO Form_CRM_File ( CRM_Id ,CRM_File_Temp_CRM_Id ,CRM_File_Name ,CRM_File_ContentType ,CRM_File_Data ,CRM_File_CreatedDate ,CRM_File_CreatedBy ) VALUES ( @CRM_Id ,@CRM_File_Temp_CRM_Id ,@CRM_File_Name ,@CRM_File_ContentType ,@CRM_File_Data ,@CRM_File_CreatedDate ,@CRM_File_CreatedBy )";
              using (SqlCommand SqlCommand_EditFile = new SqlCommand(SQLStringEditFile))
              {
                SqlCommand_EditFile.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
                SqlCommand_EditFile.Parameters.AddWithValue("@CRM_File_Temp_CRM_Id", DBNull.Value);
                SqlCommand_EditFile.Parameters.AddWithValue("@CRM_File_Name", FileName);
                SqlCommand_EditFile.Parameters.AddWithValue("@CRM_File_ContentType", FileContentTypeValue);
                SqlCommand_EditFile.Parameters.AddWithValue("@CRM_File_Data", Byte_File);
                SqlCommand_EditFile.Parameters.AddWithValue("@CRM_File_CreatedDate", DateTime.Now);
                SqlCommand_EditFile.Parameters.AddWithValue("@CRM_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditFile);
              }

              GridView GridView_EditCRMFile = (GridView)FormView_CRM_Form.FindControl("GridView_EditFile");
              GridView_EditCRMFile.DataBind();
            }
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

      Button Button_EditUpdate = (Button)FormView_CRM_Form.FindControl("Button_EditUpdate");

      ToolkitScriptManager_CRM.SetFocus(Button_EditUpdate);
      EditRegisterPostBackControl();
      Label_EditMessageFile.Text = UploadMessage;
    }

    protected void Button_EditDeleteFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      GridView GridView_EditFile = (GridView)FormView_CRM_Form.FindControl("GridView_EditFile");
      Label Label_EditMessageFile = (Label)FormView_CRM_Form.FindControl("Label_EditMessageFile");

      for (int i = 0; i < GridView_EditFile.Rows.Count; i++)
      {
        CheckBox CheckBox_EditFile = (CheckBox)GridView_EditFile.Rows[i].Cells[0].FindControl("CheckBox_EditFile");
        Int32 FileId = 0;

        if (CheckBox_EditFile.Checked == true)
        {
          FileId = Convert.ToInt32(CheckBox_EditFile.CssClass, CultureInfo.CurrentCulture);

          string SQLStringCRMFile = "DELETE FROM Form_CRM_File WHERE CRM_File_Id = @CRM_File_Id";
          using (SqlCommand SqlCommand_CRMFile = new SqlCommand(SQLStringCRMFile))
          {
            SqlCommand_CRMFile.Parameters.AddWithValue("@CRM_File_Id", FileId);

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_CRMFile);

            DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
          }
        }
      }

      Button Button_EditUpdate = (Button)FormView_CRM_Form.FindControl("Button_EditUpdate");

      ToolkitScriptManager_CRM.SetFocus(Button_EditUpdate);
      EditRegisterPostBackControl();
      Label_EditMessageFile.Text = DeleteMessage;
      GridView_EditFile.DataBind();
    }

    protected void Button_EditDeleteAllFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      GridView GridView_EditFile = (GridView)FormView_CRM_Form.FindControl("GridView_EditFile");
      Label Label_EditMessageFile = (Label)FormView_CRM_Form.FindControl("Label_EditMessageFile");

      for (int i = 0; i < GridView_EditFile.Rows.Count; i++)
      {
        CheckBox CheckBox_EditFile = (CheckBox)GridView_EditFile.Rows[i].Cells[0].FindControl("CheckBox_EditFile");
        Int32 FileId = 0;

        FileId = Convert.ToInt32(CheckBox_EditFile.CssClass, CultureInfo.CurrentCulture);

        string SQLStringCRMFile = "DELETE FROM Form_CRM_File WHERE CRM_File_Id = @CRM_File_Id";
        using (SqlCommand SqlCommand_CRMFile = new SqlCommand(SQLStringCRMFile))
        {
          SqlCommand_CRMFile.Parameters.AddWithValue("@CRM_File_Id", FileId);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_CRMFile);

          DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
        }
      }

      Button Button_EditUpdate = (Button)FormView_CRM_Form.FindControl("Button_EditUpdate");

      ToolkitScriptManager_CRM.SetFocus(Button_EditUpdate);
      EditRegisterPostBackControl();
      Label_EditMessageFile.Text = DeleteMessage;
      GridView_EditFile.DataBind();
    }

    protected void Button_EditUploadFile_DataBinding(object sender, EventArgs e)
    {
      EditRegisterPostBackControl();
    }

    protected void Button_EditDeleteFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_EditDeleteFile = (Button)sender;
      ScriptManager ScriptManager_EditDeleteFile = ScriptManager.GetCurrent(Page);
      ScriptManager_EditDeleteFile.RegisterPostBackControl(Button_EditDeleteFile);
    }

    protected void Button_EditDeleteAllFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_EditDeleteAllFile = (Button)sender;
      ScriptManager ScriptManager_EditDeleteAllFile = ScriptManager.GetCurrent(Page);
      ScriptManager_EditDeleteAllFile.RegisterPostBackControl(Button_EditDeleteAllFile);
    }

    protected void LinkButton_EditFile_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_EditFile = (LinkButton)sender;
      ScriptManager ScriptManager_EditFile = ScriptManager.GetCurrent(Page);
      ScriptManager_EditFile.RegisterPostBackControl(LinkButton_EditFile);
    }
    //---END--- --File Edit--//


    //--START-- --File Item--//
    protected void SqlDataSource_CRM_ItemFile_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        GridView GridView_ItemFile = (GridView)FormView_CRM_Form.FindControl("GridView_ItemFile");

        if (TotalRecords > 0)
        {
          GridView_ItemFile.Visible = true;
        }
        else
        {
          GridView_ItemFile.Visible = false;
        }
      }
    }

    protected void GridView_ItemFile_PreRender(object sender, EventArgs e)
    {
      if (e != null)
      {
        GridView GridView_List = (GridView)sender;
        GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
        if (GridViewRow_List != null)
        {
          GridViewRow_List.Visible = true;
        }
      }
    }

    protected void GridView_ItemFile_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void LinkButton_ItemFile_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_ItemFile = (LinkButton)sender;
      ScriptManager ScriptManager_ItemFile = ScriptManager.GetCurrent(Page);
      ScriptManager_ItemFile.RegisterPostBackControl(LinkButton_ItemFile);
    }
    //---END--- --File Item--//
    //---END--- --TableForm--//
  }
}