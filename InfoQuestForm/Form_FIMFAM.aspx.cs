using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_FIMFAM : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_FIMFAM, this.GetType(), "UpdateProgress_Start", "Validation_Search();Validation_Form();Calculation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          PageTitle();

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_FIMFAM_PatientVisitNumber"] != null)
          {
            SqlDataSource_FIMFAM_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
            SqlDataSource_FIMFAM_Facility.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_FIMFAM_Elements";
            SqlDataSource_FIMFAM_Facility.SelectParameters["TableWHERE"].DefaultValue = "Facility_Id = " + Request.QueryString["s_Facility_Id"] + " AND FIMFAM_Elements_PatientVisitNumber = '" + Request.QueryString["s_FIMFAM_PatientVisitNumber"] + "' ";

            TablePatientInfo.Visible = true;
            TableForm.Visible = true;
            TableList.Visible = true;

            PatientDataPI();

            if (TablePatientInfo.Visible == true)
            {
              SetFormVisibility();
            }
          }
          else
          {
            Label_InvalidSearchMessage.Text = "";
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            TableList.Visible = false;
          }


          if (TablePatientInfo.Visible == true)
          {
            TablePatientInfoVisible();
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
        if (Request.QueryString["s_Facility_Id"] == null)
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('25'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('25')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("25");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_FIMFAM.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("FIMFAM", "28");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_FIMFAM_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_FIMFAM_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_FIMFAM_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_FIMFAM_Facility.SelectParameters.Clear();
      SqlDataSource_FIMFAM_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_FIMFAM_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "25");
      SqlDataSource_FIMFAM_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_FIMFAM_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_FIMFAM_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_FIMFAM_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_FIMFAM_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_FIMFAM_Form.InsertCommand = "INSERT INTO InfoQuest_Form_FIMFAM_Elements ([Facility_Id], [FIMFAM_Elements_PatientVisitNumber], [FIMFAM_Elements_ReportNumber], [FIMFAM_Elements_ObservationDate], [FIMFAM_Elements_OnsetDate], [FIMFAM_Elements_Selfcare_Eating], [FIMFAM_Elements_Selfcare_Grooming], [FIMFAM_Elements_Selfcare_Bathing], [FIMFAM_Elements_Selfcare_DressingUpper], [FIMFAM_Elements_Selfcare_DressingLower], [FIMFAM_Elements_Selfcare_Toileting], [FIMFAM_Elements_Selfcare_Swallowing], [FIMFAM_Elements_Sphincter_Bladder1], [FIMFAM_Elements_Sphincter_Bladder2], [FIMFAM_Elements_Sphincter_Bowel1], [FIMFAM_Elements_Sphincter_Bowel2], [FIMFAM_Elements_Transfer_BCW], [FIMFAM_Elements_Transfer_Toilet], [FIMFAM_Elements_Transfer_TS], [FIMFAM_Elements_Transfer_CarTransfer], [FIMFAM_Elements_Locomotion_WW], [FIMFAM_Elements_Locomotion_Stairs], [FIMFAM_Elements_Locomotion_CommunityAccess], [FIMFAM_Elements_MotorSubScore], [FIMFAM_Elements_Communication_AV], [FIMFAM_Elements_Communication_VN], [FIMFAM_Elements_Communication_Reading], [FIMFAM_Elements_Communication_Writing], [FIMFAM_Elements_Communication_Speech], [FIMFAM_Elements_PSAdjust_SocialInteraction], [FIMFAM_Elements_PSAdjust_EmotionalStatus], [FIMFAM_Elements_PSAdjust_Adjustment], [FIMFAM_Elements_PSAdjust_Employability], [FIMFAM_Elements_CognitiveFunction_ProblemSolving], [FIMFAM_Elements_CognitiveFunction_Memory], [FIMFAM_Elements_CognitiveFunction_Orientation], [FIMFAM_Elements_CognitiveFunction_Attention], [FIMFAM_Elements_CognitiveFunction_SafetyJudgement], [FIMFAM_Elements_CognitiveSubScore], [FIMFAM_Elements_Total], [FIMFAM_Elements_CreatedDate], [FIMFAM_Elements_CreatedBy], [FIMFAM_Elements_ModifiedDate], [FIMFAM_Elements_ModifiedBy], [FIMFAM_Elements_History], [FIMFAM_Elements_IsActive]) VALUES (@Facility_Id, @FIMFAM_Elements_PatientVisitNumber, @FIMFAM_Elements_ReportNumber, @FIMFAM_Elements_ObservationDate, @FIMFAM_Elements_OnsetDate, @FIMFAM_Elements_Selfcare_Eating, @FIMFAM_Elements_Selfcare_Grooming, @FIMFAM_Elements_Selfcare_Bathing, @FIMFAM_Elements_Selfcare_DressingUpper, @FIMFAM_Elements_Selfcare_DressingLower, @FIMFAM_Elements_Selfcare_Toileting, @FIMFAM_Elements_Selfcare_Swallowing, @FIMFAM_Elements_Sphincter_Bladder1, @FIMFAM_Elements_Sphincter_Bladder2, @FIMFAM_Elements_Sphincter_Bowel1, @FIMFAM_Elements_Sphincter_Bowel2, @FIMFAM_Elements_Transfer_BCW, @FIMFAM_Elements_Transfer_Toilet, @FIMFAM_Elements_Transfer_TS, @FIMFAM_Elements_Transfer_CarTransfer, @FIMFAM_Elements_Locomotion_WW, @FIMFAM_Elements_Locomotion_Stairs, @FIMFAM_Elements_Locomotion_CommunityAccess, @FIMFAM_Elements_MotorSubScore, @FIMFAM_Elements_Communication_AV, @FIMFAM_Elements_Communication_VN, @FIMFAM_Elements_Communication_Reading, @FIMFAM_Elements_Communication_Writing, @FIMFAM_Elements_Communication_Speech, @FIMFAM_Elements_PSAdjust_SocialInteraction, @FIMFAM_Elements_PSAdjust_EmotionalStatus, @FIMFAM_Elements_PSAdjust_Adjustment, @FIMFAM_Elements_PSAdjust_Employability, @FIMFAM_Elements_CognitiveFunction_ProblemSolving, @FIMFAM_Elements_CognitiveFunction_Memory, @FIMFAM_Elements_CognitiveFunction_Orientation, @FIMFAM_Elements_CognitiveFunction_Attention, @FIMFAM_Elements_CognitiveFunction_SafetyJudgement, @FIMFAM_Elements_CognitiveSubScore, @FIMFAM_Elements_Total, @FIMFAM_Elements_CreatedDate, @FIMFAM_Elements_CreatedBy, @FIMFAM_Elements_ModifiedDate, @FIMFAM_Elements_ModifiedBy, @FIMFAM_Elements_History, @FIMFAM_Elements_IsActive); SELECT @FIMFAM_Elements_Id = SCOPE_IDENTITY()";
      SqlDataSource_FIMFAM_Form.SelectCommand = "SELECT * FROM InfoQuest_Form_FIMFAM_Elements WHERE (FIMFAM_Elements_Id = @FIMFAM_Elements_Id)";
      SqlDataSource_FIMFAM_Form.UpdateCommand = "UPDATE InfoQuest_Form_FIMFAM_Elements SET [FIMFAM_Elements_ObservationDate] = @FIMFAM_Elements_ObservationDate, [FIMFAM_Elements_OnsetDate] = @FIMFAM_Elements_OnsetDate, [FIMFAM_Elements_Selfcare_Eating] = @FIMFAM_Elements_Selfcare_Eating, [FIMFAM_Elements_Selfcare_Grooming] = @FIMFAM_Elements_Selfcare_Grooming, [FIMFAM_Elements_Selfcare_Bathing] = @FIMFAM_Elements_Selfcare_Bathing, [FIMFAM_Elements_Selfcare_DressingUpper] = @FIMFAM_Elements_Selfcare_DressingUpper, [FIMFAM_Elements_Selfcare_DressingLower] = @FIMFAM_Elements_Selfcare_DressingLower, [FIMFAM_Elements_Selfcare_Toileting] = @FIMFAM_Elements_Selfcare_Toileting, [FIMFAM_Elements_Selfcare_Swallowing] = @FIMFAM_Elements_Selfcare_Swallowing, [FIMFAM_Elements_Sphincter_Bladder1] = @FIMFAM_Elements_Sphincter_Bladder1, [FIMFAM_Elements_Sphincter_Bladder2] = @FIMFAM_Elements_Sphincter_Bladder2, [FIMFAM_Elements_Sphincter_Bowel1] = @FIMFAM_Elements_Sphincter_Bowel1, [FIMFAM_Elements_Sphincter_Bowel2] = @FIMFAM_Elements_Sphincter_Bowel2, [FIMFAM_Elements_Transfer_BCW] = @FIMFAM_Elements_Transfer_BCW, [FIMFAM_Elements_Transfer_Toilet] = @FIMFAM_Elements_Transfer_Toilet, [FIMFAM_Elements_Transfer_TS] = @FIMFAM_Elements_Transfer_TS, [FIMFAM_Elements_Transfer_CarTransfer] = @FIMFAM_Elements_Transfer_CarTransfer, [FIMFAM_Elements_Locomotion_WW] = @FIMFAM_Elements_Locomotion_WW, [FIMFAM_Elements_Locomotion_Stairs] = @FIMFAM_Elements_Locomotion_Stairs, [FIMFAM_Elements_Locomotion_CommunityAccess] = @FIMFAM_Elements_Locomotion_CommunityAccess, [FIMFAM_Elements_MotorSubScore] = @FIMFAM_Elements_MotorSubScore, [FIMFAM_Elements_Communication_AV] = @FIMFAM_Elements_Communication_AV, [FIMFAM_Elements_Communication_VN] = @FIMFAM_Elements_Communication_VN, [FIMFAM_Elements_Communication_Reading] = @FIMFAM_Elements_Communication_Reading, [FIMFAM_Elements_Communication_Writing] = @FIMFAM_Elements_Communication_Writing, [FIMFAM_Elements_Communication_Speech] = @FIMFAM_Elements_Communication_Speech, [FIMFAM_Elements_PSAdjust_SocialInteraction] = @FIMFAM_Elements_PSAdjust_SocialInteraction, [FIMFAM_Elements_PSAdjust_EmotionalStatus] = @FIMFAM_Elements_PSAdjust_EmotionalStatus, [FIMFAM_Elements_PSAdjust_Adjustment] = @FIMFAM_Elements_PSAdjust_Adjustment, [FIMFAM_Elements_PSAdjust_Employability] = @FIMFAM_Elements_PSAdjust_Employability, [FIMFAM_Elements_CognitiveFunction_ProblemSolving] = @FIMFAM_Elements_CognitiveFunction_ProblemSolving, [FIMFAM_Elements_CognitiveFunction_Memory] = @FIMFAM_Elements_CognitiveFunction_Memory, [FIMFAM_Elements_CognitiveFunction_Orientation] = @FIMFAM_Elements_CognitiveFunction_Orientation, [FIMFAM_Elements_CognitiveFunction_Attention] = @FIMFAM_Elements_CognitiveFunction_Attention, [FIMFAM_Elements_CognitiveFunction_SafetyJudgement] = @FIMFAM_Elements_CognitiveFunction_SafetyJudgement, [FIMFAM_Elements_CognitiveSubScore] = @FIMFAM_Elements_CognitiveSubScore, [FIMFAM_Elements_Total] = @FIMFAM_Elements_Total, [FIMFAM_Elements_ModifiedDate] = @FIMFAM_Elements_ModifiedDate, [FIMFAM_Elements_ModifiedBy] = @FIMFAM_Elements_ModifiedBy, [FIMFAM_Elements_History] = @FIMFAM_Elements_History, [FIMFAM_Elements_IsActive] = @FIMFAM_Elements_IsActive WHERE FIMFAM_Elements_Id = @FIMFAM_Elements_Id";
      SqlDataSource_FIMFAM_Form.InsertParameters.Clear();
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Id", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_PatientVisitNumber", TypeCode.String, Request.QueryString["s_FIMFAM_PatientVisitNumber"]);      
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_ReportNumber", TypeCode.String, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_ObservationDate", TypeCode.DateTime, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_OnsetDate", TypeCode.DateTime, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Selfcare_Eating", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Selfcare_Grooming", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Selfcare_Bathing", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Selfcare_DressingUpper", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Selfcare_DressingLower", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Selfcare_Toileting", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Selfcare_Swallowing", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Sphincter_Bladder1", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Sphincter_Bladder2", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Sphincter_Bowel1", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Sphincter_Bowel2", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Transfer_BCW", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Transfer_Toilet", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Transfer_TS", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Transfer_CarTransfer", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Locomotion_WW", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Locomotion_Stairs", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Locomotion_CommunityAccess", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_MotorSubScore", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Communication_AV", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Communication_VN", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Communication_Reading", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Communication_Writing", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Communication_Speech", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_PSAdjust_SocialInteraction", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_PSAdjust_EmotionalStatus", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_PSAdjust_Adjustment", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_PSAdjust_Employability", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_CognitiveFunction_ProblemSolving", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_CognitiveFunction_Memory", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_CognitiveFunction_Orientation", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_CognitiveFunction_Attention", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_CognitiveFunction_SafetyJudgement", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_CognitiveSubScore", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_Total", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_CreatedBy", TypeCode.String, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_ModifiedBy", TypeCode.String, "");
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_History", TypeCode.String, "");
      SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_FIMFAM_Form.InsertParameters.Add("FIMFAM_Elements_IsActive", TypeCode.Boolean, "");
      SqlDataSource_FIMFAM_Form.SelectParameters.Clear();
      SqlDataSource_FIMFAM_Form.SelectParameters.Add("FIMFAM_Elements_Id", TypeCode.Int32, Request.QueryString["FIMFAM_Elements_Id"]);
      SqlDataSource_FIMFAM_Form.UpdateParameters.Clear();
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_ObservationDate", TypeCode.DateTime, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_OnsetDate", TypeCode.DateTime, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Selfcare_Eating", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Selfcare_Grooming", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Selfcare_Bathing", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Selfcare_DressingUpper", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Selfcare_DressingLower", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Selfcare_Toileting", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Selfcare_Swallowing", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Sphincter_Bladder1", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Sphincter_Bladder2", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Sphincter_Bowel1", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Sphincter_Bowel2", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Transfer_BCW", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Transfer_Toilet", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Transfer_TS", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Transfer_CarTransfer", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Locomotion_WW", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Locomotion_Stairs", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Locomotion_CommunityAccess", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_MotorSubScore", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Communication_AV", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Communication_VN", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Communication_Reading", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Communication_Writing", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Communication_Speech", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_PSAdjust_SocialInteraction", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_PSAdjust_EmotionalStatus", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_PSAdjust_Adjustment", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_PSAdjust_Employability", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_CognitiveFunction_ProblemSolving", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_CognitiveFunction_Memory", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_CognitiveFunction_Orientation", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_CognitiveFunction_Attention", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_CognitiveFunction_SafetyJudgement", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_CognitiveSubScore", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Total", TypeCode.Int32, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_ModifiedBy", TypeCode.String, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_History", TypeCode.String, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_IsActive", TypeCode.Boolean, "");
      SqlDataSource_FIMFAM_Form.UpdateParameters.Add("FIMFAM_Elements_Id", TypeCode.Int32, "");

      SqlDataSource_FIMFAM_Elements.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_FIMFAM_Elements.SelectCommand = "spForm_Get_FIMFAM_Elements";
      SqlDataSource_FIMFAM_Elements.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_FIMFAM_Elements.CancelSelectOnNullParameter = false;
      SqlDataSource_FIMFAM_Elements.SelectParameters.Clear();
      SqlDataSource_FIMFAM_Elements.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_FIMFAM_Elements.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_FIMFAM_Elements.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("25")).ToString(), CultureInfo.CurrentCulture);
      Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("25").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
      Label_PatientInfoHeading.Text = Convert.ToString("Patient Information", CultureInfo.CurrentCulture);
      Label_ElementsHeading.Text = Convert.ToString("Elements", CultureInfo.CurrentCulture);
      Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("25").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Facility_Id"] == null)
        {
          DropDownList_Facility.SelectedValue = "";
        }
        else
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text.ToString()))
      {
        if (Request.QueryString["s_FIMFAM_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_FIMFAM_PatientVisitNumber"];
        }
      }
    }

    private void PatientDataPI()
    {
      DataTable DataTable_PatientDataPI;
      using (DataTable_PatientDataPI = new DataTable())
      {
        DataTable_PatientDataPI.Locale = CultureInfo.CurrentCulture;
        //DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_FIMFAM_PatientVisitNumber"]).Copy();
        DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_FIMFAM_PatientVisitNumber"]).Copy();

        if (DataTable_PatientDataPI.Columns.Count == 1)
        {
          string FIMFAMPIId = "";
          string SQLStringPatientInfo = "SELECT FIMFAM_PI_Id FROM InfoQuest_Form_FIMFAM_PatientInformation WHERE Facility_Id = @FacilityId AND FIMFAM_PI_PatientVisitNumber = @FIMFAMPIPatientVisitNumber";
          using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
          {
            SqlCommand_PatientInfo.Parameters.AddWithValue("@FacilityId", Request.QueryString["s_Facility_Id"]);
            SqlCommand_PatientInfo.Parameters.AddWithValue("@FIMFAMPIPatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
            DataTable DataTable_PatientInfo;
            using (DataTable_PatientInfo = new DataTable())
            {
              DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
              DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
              if (DataTable_PatientInfo.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                {
                  FIMFAMPIId = DataRow_Row1["FIMFAM_PI_Id"].ToString();
                }
              }
            }
          }

          if (string.IsNullOrEmpty(FIMFAMPIId))
          {
            string Error = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Error = DataRow_Row["Error"].ToString();
            }

            Label_InvalidSearchMessage.Text = Error;
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            TableList.Visible = false;
            Error = "";
          }
          else
          {
            string Error = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Error = DataRow_Row["Error"].ToString();
            }

            Label_InvalidSearchMessage.Text = Error + Convert.ToString("<br />No Patient related data could be updated but you can continue capturing new form(s) and updating and viewing previous form(s)", CultureInfo.CurrentCulture);
            Error = "";
          }

          FIMFAMPIId = "";
        }
        else if (DataTable_PatientDataPI.Columns.Count != 1)
        {
          PatientDataPI_PatientInformation(DataTable_PatientDataPI);

          PatientDataPI_PatientAilment();
        }
      }
    }

    private void PatientDataPI_PatientInformation(DataTable dataTable_PatientDataPI)
    {
      if (dataTable_PatientDataPI != null)
      {
        if (dataTable_PatientDataPI.Rows.Count == 0)
        {
          Label_InvalidSearchMessage.Text = Convert.ToString("Patient Visit Number " + Request.QueryString["s_FIMFAM_PatientVisitNumber"] + " does not Exist", CultureInfo.CurrentCulture);
          TablePatientInfo.Visible = false;
          TableForm.Visible = false;
          TableList.Visible = false;
        }
        else
        {
          foreach (DataRow DataRow_Row in dataTable_PatientDataPI.Rows)
          {
            string VisitNumber = DataRow_Row["VisitNumber"].ToString();
            string NameSurname = DataRow_Row["Surname"].ToString() + "," + DataRow_Row["Name"].ToString();
            string Age = DataRow_Row["PatientAge"].ToString();
            string AdmissionDate = DataRow_Row["DateOfAdmission"].ToString();
            string DischargeDate = DataRow_Row["DateOfDischarge"].ToString();

            string NameSurnamePI = NameSurname;
            NameSurnamePI = NameSurnamePI.Replace("'", "");
            NameSurname = NameSurnamePI;
            NameSurnamePI = "";

            string FIMFAMPIId = "";
            string SQLStringPatientInfo = "SELECT FIMFAM_PI_Id FROM InfoQuest_Form_FIMFAM_PatientInformation WHERE Facility_Id = @Facility_Id AND FIMFAM_PI_PatientVisitNumber = @FIMFAM_PI_PatientVisitNumber";
            using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
            {
              SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
              SqlCommand_PatientInfo.Parameters.AddWithValue("@FIMFAM_PI_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
              DataTable DataTable_PatientInfo;
              using (DataTable_PatientInfo = new DataTable())
              {
                DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
                DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
                if (DataTable_PatientInfo.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                  {
                    FIMFAMPIId = DataRow_Row1["FIMFAM_PI_Id"].ToString();
                  }
                }
              }
            }

            if (string.IsNullOrEmpty(FIMFAMPIId))
            {
              string SQLStringInsertFIMFAMPI = "INSERT INTO InfoQuest_Form_FIMFAM_PatientInformation ( Facility_Id , FIMFAM_PI_PatientVisitNumber , FIMFAM_PI_PatientName , FIMFAM_PI_PatientAge , FIMFAM_PI_PatientDateOfAdmission , FIMFAM_PI_PatientDateofDischarge , FIMFAM_PI_Archived ) VALUES  ( @Facility_Id , @FIMFAM_PI_PatientVisitNumber , @FIMFAM_PI_PatientName , @FIMFAM_PI_PatientAge , @FIMFAM_PI_PatientDateOfAdmission , @FIMFAM_PI_PatientDateofDischarge , @FIMFAM_PI_Archived )";
              using (SqlCommand SqlCommand_InsertFIMFAMPI = new SqlCommand(SQLStringInsertFIMFAMPI))
              {
                SqlCommand_InsertFIMFAMPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_InsertFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientVisitNumber", VisitNumber);
                SqlCommand_InsertFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientName", NameSurname);
                SqlCommand_InsertFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientAge", Age);
                SqlCommand_InsertFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientDateOfAdmission", AdmissionDate);
                SqlCommand_InsertFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientDateofDischarge", DischargeDate);
                SqlCommand_InsertFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_Archived", 0);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertFIMFAMPI);
              }
            }
            else
            {
              string SQLStringUpdateFIMFAMPI = "UPDATE InfoQuest_Form_FIMFAM_PatientInformation SET FIMFAM_PI_PatientName = @FIMFAM_PI_PatientName , FIMFAM_PI_PatientAge = @FIMFAM_PI_PatientAge , FIMFAM_PI_PatientDateOfAdmission = @FIMFAM_PI_PatientDateOfAdmission , FIMFAM_PI_PatientDateofDischarge = @FIMFAM_PI_PatientDateofDischarge WHERE Facility_Id = @Facility_Id AND FIMFAM_PI_PatientVisitNumber = @FIMFAM_PI_PatientVisitNumber ";
              using (SqlCommand SqlCommand_UpdateFIMFAMPI = new SqlCommand(SQLStringUpdateFIMFAMPI))
              {
                SqlCommand_UpdateFIMFAMPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_UpdateFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientVisitNumber", VisitNumber);
                SqlCommand_UpdateFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientName", NameSurname);
                SqlCommand_UpdateFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientAge", Age);
                SqlCommand_UpdateFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientDateOfAdmission", AdmissionDate);
                SqlCommand_UpdateFIMFAMPI.Parameters.AddWithValue("@FIMFAM_PI_PatientDateofDischarge", DischargeDate);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateFIMFAMPI);
              }
            }
            FIMFAMPIId = "";
          }
        }
      }
    }

    private void PatientDataPI_PatientAilment()
    {
      DataTable DataTable_PatientDataAilment;
      using (DataTable_PatientDataAilment = new DataTable())
      {
        DataTable_PatientDataAilment.Locale = CultureInfo.CurrentCulture;
        DataTable_PatientDataAilment = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_ImpairmentCoding(Request.QueryString["s_Facility_Id"], Request.QueryString["s_FIMFAM_PatientVisitNumber"]).Copy();

        if (DataTable_PatientDataAilment.Columns.Count == 1)
        {
          string FIMFAMPAId = "";
          string SQLStringPatientInfo = "SELECT FIMFAM_PA_Id FROM InfoQuest_Form_FIMFAM_PatientAilments WHERE Facility_Id = @Facility_Id AND FIMFAM_PA_PatientVisitNumber = @FIMFAM_PA_PatientVisitNumber";
          using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
          {
            SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
            SqlCommand_PatientInfo.Parameters.AddWithValue("@FIMFAM_PA_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
            DataTable DataTable_PatientInfo;
            using (DataTable_PatientInfo = new DataTable())
            {
              DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
              DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
              if (DataTable_PatientInfo.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                {
                  FIMFAMPAId = DataRow_Row1["FIMFAM_PA_Id"].ToString();
                }
              }
            }
          }

          if (string.IsNullOrEmpty(FIMFAMPAId))
          {
            string Error = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataAilment.Rows)
            {
              Error = DataRow_Row["Error"].ToString();
            }

            Label_InvalidSearchMessage.Text = Error;
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            TableList.Visible = false;
            Error = "";
          }

          FIMFAMPAId = "";
        }
        else if (DataTable_PatientDataAilment.Columns.Count != 1)
        {
          if (DataTable_PatientDataAilment.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PatientDataAilment.Rows)
            {
              string VisitNumber = DataRow_Row["VisitNumber"].ToString();
              string VisitAilment = DataRow_Row["ImpairmentClassificationDescription"].ToString();

              VisitAilment = VisitAilment.Replace("'", "");

              string FIMFAMPAId = "";
              string SQLStringPatientInfo = "SELECT FIMFAM_PA_Id FROM InfoQuest_Form_FIMFAM_PatientAilments WHERE Facility_Id = @Facility_Id AND FIMFAM_PA_PatientVisitNumber = @FIMFAM_PA_PatientVisitNumber AND FIMFAM_PA_Ailment = @FIMFAM_PA_Ailment";
              using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
              {
                SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_PatientInfo.Parameters.AddWithValue("@FIMFAM_PA_PatientVisitNumber", VisitNumber);
                SqlCommand_PatientInfo.Parameters.AddWithValue("@FIMFAM_PA_Ailment", VisitAilment);
                DataTable DataTable_PatientInfo;
                using (DataTable_PatientInfo = new DataTable())
                {
                  DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
                  DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
                  if (DataTable_PatientInfo.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                    {
                      FIMFAMPAId = DataRow_Row1["FIMFAM_PA_Id"].ToString();
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(FIMFAMPAId))
              {
                string SQLStringInsertFIMFAMAilment = "INSERT INTO InfoQuest_Form_FIMFAM_PatientAilments ( Facility_Id , FIMFAM_PA_PatientVisitNumber , FIMFAM_PA_Ailment , FIMFAM_PA_IsActive ) VALUES  ( @Facility_Id , @FIMFAM_PA_PatientVisitNumber , @FIMFAM_PA_Ailment , @FIMFAM_PA_IsActive )";
                using (SqlCommand SqlCommand_InsertFIMFAMAilment = new SqlCommand(SQLStringInsertFIMFAMAilment))
                {
                  SqlCommand_InsertFIMFAMAilment.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_InsertFIMFAMAilment.Parameters.AddWithValue("@FIMFAM_PA_PatientVisitNumber", VisitNumber);
                  SqlCommand_InsertFIMFAMAilment.Parameters.AddWithValue("@FIMFAM_PA_Ailment", VisitAilment);
                  SqlCommand_InsertFIMFAMAilment.Parameters.AddWithValue("@FIMFAM_PA_IsActive", 1);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertFIMFAMAilment);
                }
              }
              else
              {
                string SQLStringUpdateFIMFAMAilment = "UPDATE InfoQuest_Form_FIMFAM_PatientAilments SET FIMFAM_PA_Ailment = @FIMFAM_PA_Ailment , FIMFAM_PA_IsActive = @FIMFAM_PA_IsActive WHERE Facility_Id = @Facility_Id AND FIMFAM_PA_PatientVisitNumber = @FIMFAM_PA_PatientVisitNumber AND FIMFAM_PA_Ailment = @FIMFAM_PA_Ailment ";
                using (SqlCommand SqlCommand_UpdateFIMFAMAilment = new SqlCommand(SQLStringUpdateFIMFAMAilment))
                {
                  SqlCommand_UpdateFIMFAMAilment.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_UpdateFIMFAMAilment.Parameters.AddWithValue("@FIMFAM_PA_PatientVisitNumber", VisitNumber);
                  SqlCommand_UpdateFIMFAMAilment.Parameters.AddWithValue("@FIMFAM_PA_Ailment", VisitAilment);
                  SqlCommand_UpdateFIMFAMAilment.Parameters.AddWithValue("@FIMFAM_PA_IsActive", 1);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateFIMFAMAilment);
                }
              }
              FIMFAMPAId = "";

              PatientDataPI_PatientAilment_Inactive(DataTable_PatientDataAilment, VisitNumber);
            }
          }
        }
      }
    }

    private void PatientDataPI_PatientAilment_Inactive(DataTable dataTable_PatientDataAilment, string visitNumber)
    {
      //String FIMFAMPAId = "";
      string FIMFAMPAAilment = "";
      string FacilityId = "";
      string FIMFAMPAPatientVisitNumber = "";
      string SQLStringPatientAilment = "SELECT FIMFAM_PA_Id , Facility_Id , FIMFAM_PA_PatientVisitNumber , FIMFAM_PA_Ailment FROM InfoQuest_Form_FIMFAM_PatientAilments WHERE Facility_Id = @s_Facility_Id AND FIMFAM_PA_PatientVisitNumber = @VisitNumber";
      using (SqlCommand SqlCommand_PatientAilment = new SqlCommand(SQLStringPatientAilment))
      {
        SqlCommand_PatientAilment.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PatientAilment.Parameters.AddWithValue("@VisitNumber", visitNumber);
        DataTable DataTable_PatientAilment;
        using (DataTable_PatientAilment = new DataTable())
        {
          DataTable_PatientAilment.Locale = CultureInfo.CurrentCulture;
          DataTable_PatientAilment = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientAilment).Copy();
          if (DataTable_PatientAilment.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row2 in DataTable_PatientAilment.Rows)
            {
              //FIMFAMPAId = DataRow_Row2["FIMFAM_PA_Id"].ToString();
              FacilityId = DataRow_Row2["Facility_Id"].ToString();
              FIMFAMPAPatientVisitNumber = DataRow_Row2["FIMFAM_PA_PatientVisitNumber"].ToString();
              FIMFAMPAAilment = DataRow_Row2["FIMFAM_PA_Ailment"].ToString();

              DataRow[] a = dataTable_PatientDataAilment.Select("VisitNumber = '" + FIMFAMPAPatientVisitNumber + "' AND ImpairmentClassificationDescription = '" + FIMFAMPAAilment + "'");

              if (a.Length == 0)
              {
                string SQLStringUpdateFIMFAMAilment = "UPDATE InfoQuest_Form_FIMFAM_PatientAilments SET FIMFAM_PA_IsActive = @FIMFAM_PA_IsActive WHERE Facility_Id = @Facility_Id AND FIMFAM_PA_PatientVisitNumber = @FIMFAM_PA_PatientVisitNumber AND FIMFAM_PA_Ailment = @FIMFAM_PA_Ailment ";
                using (SqlCommand SqlCommand_UpdateFIMFAMAilment = new SqlCommand(SQLStringUpdateFIMFAMAilment))
                {
                  SqlCommand_UpdateFIMFAMAilment.Parameters.AddWithValue("@Facility_Id", FacilityId);
                  SqlCommand_UpdateFIMFAMAilment.Parameters.AddWithValue("@FIMFAM_PA_PatientVisitNumber", FIMFAMPAPatientVisitNumber);
                  SqlCommand_UpdateFIMFAMAilment.Parameters.AddWithValue("@FIMFAM_PA_Ailment", FIMFAMPAAilment);
                  SqlCommand_UpdateFIMFAMAilment.Parameters.AddWithValue("@FIMFAM_PA_IsActive", 0);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateFIMFAMAilment);
                }
              }
            }
          }
        }
      }
      //FIMFAMPAId = "";
      FIMFAMPAAilment = "";
      FacilityId = "";
      FIMFAMPAPatientVisitNumber = "";
    }


    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('25')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@LOGON_USER", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '81'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '82'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '83'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '84'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              if (Request.QueryString["FIMFAM_Elements_Id"] != null)
              {
                if (Request.QueryString["ViewMode"] == "1")
                {
                  FormView_FIMFAM_Form.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                  FormView_FIMFAM_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
              else
              {
                FormView_FIMFAM_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Session["Security"] = "0";
              FormView_FIMFAM_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              FormView_FIMFAM_Form.ChangeMode(FormViewMode.ReadOnly);
            }
            Session["Security"] = "1";
          }
        }
      }
    }

    private void TablePatientInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["FIMFAMPIPatientVisitNumber"] = "";
      Session["FIMFAMPIPatientName"] = "";
      Session["FIMFAMPIPatientAge"] = "";
      Session["FIMFAMPIPatientDateOfAdmission"] = "";
      Session["FIMFAMPIPatientDateofDischarge"] = "";

      string SQLStringPatientInfo = "SELECT DISTINCT Facility_FacilityDisplayName , FIMFAM_PI_PatientVisitNumber , FIMFAM_PI_PatientName , FIMFAM_PI_PatientAge , FIMFAM_PI_PatientDateOfAdmission , FIMFAM_PI_PatientDateofDischarge FROM vForm_FIMFAM_PatientInformation WHERE Facility_Id = @s_Facility_Id AND FIMFAM_PI_PatientVisitNumber = @s_FIMFAM_PatientVisitNumber";
      using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
      {
        SqlCommand_PatientInfo.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PatientInfo.Parameters.AddWithValue("@s_FIMFAM_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
        DataTable DataTable_PatientInfo;
        using (DataTable_PatientInfo = new DataTable())
        {
          DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
          if (DataTable_PatientInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PatientInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["FIMFAMPIPatientVisitNumber"] = DataRow_Row["FIMFAM_PI_PatientVisitNumber"];
              Session["FIMFAMPIPatientName"] = DataRow_Row["FIMFAM_PI_PatientName"];
              Session["FIMFAMPIPatientAge"] = DataRow_Row["FIMFAM_PI_PatientAge"];
              Session["FIMFAMPIPatientDateOfAdmission"] = DataRow_Row["FIMFAM_PI_PatientDateOfAdmission"];
              Session["FIMFAMPIPatientDateofDischarge"] = DataRow_Row["FIMFAM_PI_PatientDateofDischarge"];
            }
          }
        }
      }

      Label_PIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_PIVisitNumber.Text = Session["FIMFAMPIPatientVisitNumber"].ToString();
      Label_PIName.Text = Session["FIMFAMPIPatientName"].ToString();
      Label_PIAge.Text = Session["FIMFAMPIPatientAge"].ToString();
      Label_PIDateAdmission.Text = Session["FIMFAMPIPatientDateOfAdmission"].ToString();
      Label_PIDateDischarge.Text = Session["FIMFAMPIPatientDateofDischarge"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["FIMFAMPIPatientVisitNumber"] = "";
      Session["FIMFAMPIPatientName"] = "";
      Session["FIMFAMPIPatientAge"] = "";
      Session["FIMFAMPIPatientDateOfAdmission"] = "";
      Session["FIMFAMPIPatientDateofDischarge"] = "";


      Session["FIMFAMPAAilment"] = "";
      BulletedList_PAAilments.Items.Clear();

      string SQLStringPatientAilment = "SELECT DISTINCT FIMFAM_PA_Ailment FROM InfoQuest_Form_FIMFAM_PatientAilments WHERE Facility_Id = @s_Facility_Id AND FIMFAM_PA_PatientVisitNumber = @s_FIMFAM_PatientVisitNumber AND FIMFAM_PA_IsActive = @FIMFAM_PA_IsActive";
      using (SqlCommand SqlCommand_PatientAilment = new SqlCommand(SQLStringPatientAilment))
      {
        SqlCommand_PatientAilment.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PatientAilment.Parameters.AddWithValue("@s_FIMFAM_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
        SqlCommand_PatientAilment.Parameters.AddWithValue("@FIMFAM_PA_IsActive", 1);
        DataTable DataTable_PatientAilment;
        using (DataTable_PatientAilment = new DataTable())
        {
          DataTable_PatientAilment.Locale = CultureInfo.CurrentCulture;
          DataTable_PatientAilment = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientAilment).Copy();
          if (DataTable_PatientAilment.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PatientAilment.Rows)
            {
              Session["FIMFAMPAAilment"] = DataRow_Row["FIMFAM_PA_Ailment"];
              BulletedList_PAAilments.Items.Add(Session["FIMFAMPAAilment"].ToString());
            }
          }
          else
          {
            Session["FIMFAMPAAilment"] = "";
            BulletedList_PAAilments.Items.Clear();
            BulletedList_PAAilments.Items.Add("No Ailments");
          }
        }
      }

      Session["FIMFAMPAAilment"] = "";
    }

    private void TableFormVisible()
    {
      if (FormView_FIMFAM_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertObservationDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertObservationDate")).Attributes.Add("OnInput", "Validation_Form();");

        Session["FIMFAMElementsOnsetDate"] = "";
        string SQLStringOnsetDate = "SELECT DISTINCT FIMFAM_Elements_OnsetDate FROM InfoQuest_Form_FIMFAM_Elements WHERE Facility_Id = @Facility_Id AND FIMFAM_Elements_PatientVisitNumber = @FIMFAM_Elements_PatientVisitNumber AND FIMFAM_Elements_OnsetDate IS NOT NULL";
        using (SqlCommand SqlCommand_OnsetDate = new SqlCommand(SQLStringOnsetDate))
        {
          SqlCommand_OnsetDate.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_OnsetDate.Parameters.AddWithValue("@FIMFAM_Elements_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
          DataTable DataTable_OnsetDate;
          using (DataTable_OnsetDate = new DataTable())
          {
            DataTable_OnsetDate.Locale = CultureInfo.CurrentCulture;
            DataTable_OnsetDate = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OnsetDate).Copy();
            if (DataTable_OnsetDate.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_OnsetDate.Rows)
              {
                Session["FIMFAMElementsOnsetDate"] = DataRow_Row["FIMFAM_Elements_OnsetDate"];
              }
            }
          }
        }

        if (string.IsNullOrEmpty(Session["FIMFAMElementsOnsetDate"].ToString()))
        {
          ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertOnsetDate")).Attributes.Add("OnChange", "Validation_Form();");
          ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertOnsetDate")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertOnsetDate")).Visible = true;
          ((ImageButton)FormView_FIMFAM_Form.FindControl("ImageButton_InsertOnsetDate")).Visible = true;
          ((Label)FormView_FIMFAM_Form.FindControl("Label_InsertOnsetDate")).Visible = false;
        }
        else
        {
          ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertOnsetDate")).Visible = false;
          ((ImageButton)FormView_FIMFAM_Form.FindControl("ImageButton_InsertOnsetDate")).Visible = false;
          ((Label)FormView_FIMFAM_Form.FindControl("Label_InsertOnsetDate")).Visible = true;
          ((Label)FormView_FIMFAM_Form.FindControl("Label_InsertOnsetDate")).Text = Convert.ToDateTime(Session["FIMFAMElementsOnsetDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        }
        Session["FIMFAMElementsOnsetDate"] = "";

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareEating")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareGrooming")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareBathing")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareDressingUpper")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareDressingLower")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareToileting")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareSwallowing")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferBCW")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferToilet")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferTS")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferCarTransfer")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionWW")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionStairs")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionCommunityAccess")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationAV")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationVN")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationReading")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationWriting")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationSpeech")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustSocialInteraction")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustEmotionalStatus")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustAdjustment")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustEmployability")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionProblemSolving")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionMemory")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionOrientation")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionAttention")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionSafetyJudgement")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      }

      if (FormView_FIMFAM_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditObservationDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditObservationDate")).Attributes.Add("OnInput", "Validation_Form();");

        Session["FIMFAMElementsId"] = "";
        Session["FIMFAMElementsOnsetDate"] = "";
        string SQLStringOnsetDate = "SELECT TOP 1 FIMFAM_Elements_Id , FIMFAM_Elements_OnsetDate FROM InfoQuest_Form_FIMFAM_Elements WHERE 	Facility_Id = @Facility_Id AND FIMFAM_Elements_PatientVisitNumber = @FIMFAM_Elements_PatientVisitNumber AND FIMFAM_Elements_OnsetDate IS NOT NULL ORDER BY FIMFAM_Elements_Id";
        using (SqlCommand SqlCommand_OnsetDate = new SqlCommand(SQLStringOnsetDate))
        {
          SqlCommand_OnsetDate.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_OnsetDate.Parameters.AddWithValue("@FIMFAM_Elements_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
          DataTable DataTable_OnsetDate;
          using (DataTable_OnsetDate = new DataTable())
          {
            DataTable_OnsetDate.Locale = CultureInfo.CurrentCulture;
            DataTable_OnsetDate = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OnsetDate).Copy();
            if (DataTable_OnsetDate.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_OnsetDate.Rows)
              {
                Session["FIMFAMElementsId"] = DataRow_Row["FIMFAM_Elements_Id"];
                Session["FIMFAMElementsOnsetDate"] = DataRow_Row["FIMFAM_Elements_OnsetDate"];
              }
            }
          }
        }

        if (Session["FIMFAMElementsId"].ToString() == Request.QueryString["FIMFAM_Elements_Id"])
        {
          ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditOnsetDate")).Attributes.Add("OnChange", "Validation_Form();");
          ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditOnsetDate")).Attributes.Add("OnInput", "Validation_Form();");
          ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditOnsetDate")).Visible = true;
          ((ImageButton)FormView_FIMFAM_Form.FindControl("ImageButton_EditOnsetDate")).Visible = true;
          ((Label)FormView_FIMFAM_Form.FindControl("Label_EditOnsetDate")).Visible = false;
        }
        else
        {
          ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditOnsetDate")).Visible = false;
          ((ImageButton)FormView_FIMFAM_Form.FindControl("ImageButton_EditOnsetDate")).Visible = false;
          ((Label)FormView_FIMFAM_Form.FindControl("Label_EditOnsetDate")).Visible = true;
          ((Label)FormView_FIMFAM_Form.FindControl("Label_EditOnsetDate")).Text = Convert.ToDateTime(Session["FIMFAMElementsOnsetDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        }
        Session["FIMFAMElementsId"] = "";
        Session["FIMFAMElementsOnsetDate"] = "";

        ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditOnsetDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditOnsetDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareEating")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareGrooming")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareBathing")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareDressingUpper")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareDressingLower")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareToileting")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareSwallowing")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferBCW")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferToilet")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferTS")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferCarTransfer")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionWW")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionStairs")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionCommunityAccess")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationAV")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationVN")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationReading")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationWriting")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationSpeech")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustSocialInteraction")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustEmotionalStatus")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustAdjustment")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustEmployability")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionProblemSolving")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionMemory")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionOrientation")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionAttention")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionSafetyJudgement")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      }
    }


    //--START-- --Search--//
    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("FIMFAM Form", "Form_FIMFAM.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("FIMFAM Form", "Form_FIMFAM.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_FIMFAM_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + ""), false);
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

        if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text))
        {
          InvalidSearch = "Yes";
        }
      }

      if (InvalidSearch == "Yes")
      {
        InvalidSearchMessage = "All red fields are required";
      }

      if (InvalidSearch == "No" && string.IsNullOrEmpty(InvalidSearchMessage))
      {

      }

      return InvalidSearchMessage;
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_FIMFAMPatientVisitNumber"];
      string SearchField3 = Request.QueryString["Search_FIMFAMPatientName"];
      string SearchField4 = Request.QueryString["Search_FIMFAMReportNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_FIMFAM_PatientVisitNumber=" + Request.QueryString["Search_FIMFAMPatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_FIMFAM_PatientName=" + Request.QueryString["Search_FIMFAMPatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_FIMFAM_ReportNumber=" + Request.QueryString["Search_FIMFAMReportNumber"] + "&";
      }

      string FinalURL = "Form_FIMFAM_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("FIMFAM List", FinalURL);

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --TableForm--//
    protected void FormView_FIMFAM_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation();

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
          Page.MaintainScrollPositionOnPostBack = false;
          ((Label)FormView_FIMFAM_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_FIMFAM_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          Session["FIMFAM_Elements_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Request.QueryString["s_Facility_Id"], "25");

          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_ReportNumber"].DefaultValue = Session["FIMFAM_Elements_ReportNumber"].ToString();
          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_History"].DefaultValue = "";
          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_IsActive"].DefaultValue = "true";

          int MotorSubScore = 0;
          MotorSubScore = Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareEating")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareGrooming")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareBathing")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareDressingUpper")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareDressingLower")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareToileting")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareSwallowing")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferBCW")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferToilet")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferTS")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferCarTransfer")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionWW")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionStairs")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionCommunityAccess")).SelectedValue, CultureInfo.CurrentCulture);

          if (Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder1")).SelectedValue, CultureInfo.CurrentCulture) <= Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder2")).SelectedValue, CultureInfo.CurrentCulture))
          {
            MotorSubScore = MotorSubScore + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder1")).SelectedValue, CultureInfo.CurrentCulture);
          }
          else
          {
            MotorSubScore = MotorSubScore + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder2")).SelectedValue, CultureInfo.CurrentCulture);
          }

          if (Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel1")).SelectedValue, CultureInfo.CurrentCulture) <= Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel2")).SelectedValue, CultureInfo.CurrentCulture))
          {
            MotorSubScore = MotorSubScore + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel1")).SelectedValue, CultureInfo.CurrentCulture);
          }
          else
          {
            MotorSubScore = MotorSubScore + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel2")).SelectedValue, CultureInfo.CurrentCulture);
          }

          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_MotorSubScore"].DefaultValue = MotorSubScore.ToString(CultureInfo.CurrentCulture);

          int CognitiveSubScore = 0;
          CognitiveSubScore = Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationAV")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationVN")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationReading")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationWriting")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationSpeech")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustSocialInteraction")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustEmotionalStatus")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustAdjustment")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustEmployability")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionProblemSolving")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionMemory")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionOrientation")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionAttention")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionSafetyJudgement")).SelectedValue, CultureInfo.CurrentCulture);

          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_CognitiveSubScore"].DefaultValue = CognitiveSubScore.ToString(CultureInfo.CurrentCulture);

          int Total = 0;
          Total = Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareEating")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareGrooming")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareBathing")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareDressingUpper")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareDressingLower")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareToileting")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareSwallowing")).SelectedValue, CultureInfo.CurrentCulture) +

            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferBCW")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferToilet")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferTS")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferCarTransfer")).SelectedValue, CultureInfo.CurrentCulture) +

            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionWW")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionStairs")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionCommunityAccess")).SelectedValue, CultureInfo.CurrentCulture) +

            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationAV")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationVN")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationReading")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationWriting")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationSpeech")).SelectedValue, CultureInfo.CurrentCulture) +

            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustSocialInteraction")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustEmotionalStatus")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustAdjustment")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustEmployability")).SelectedValue, CultureInfo.CurrentCulture) +

            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionProblemSolving")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionMemory")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionOrientation")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionAttention")).SelectedValue, CultureInfo.CurrentCulture) +
            Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionSafetyJudgement")).SelectedValue, CultureInfo.CurrentCulture);

          if (Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder1")).SelectedValue, CultureInfo.CurrentCulture) <= Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder2")).SelectedValue, CultureInfo.CurrentCulture))
          {
            Total = Total + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder1")).SelectedValue, CultureInfo.CurrentCulture);
          }
          else
          {
            Total = Total + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder2")).SelectedValue, CultureInfo.CurrentCulture);
          }

          if (Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel1")).SelectedValue, CultureInfo.CurrentCulture) <= Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel2")).SelectedValue, CultureInfo.CurrentCulture))
          {
            Total = Total + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel1")).SelectedValue, CultureInfo.CurrentCulture);
          }
          else
          {
            Total = Total + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel2")).SelectedValue, CultureInfo.CurrentCulture);
          }

          SqlDataSource_FIMFAM_Form.InsertParameters["FIMFAM_Elements_Total"].DefaultValue = Total.ToString(CultureInfo.CurrentCulture);

          Session["FIMFAM_Elements_ReportNumber"] = "";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        InvalidForm = InsertValidation_Motor(InvalidForm);

        InvalidForm = InsertValidation_Cognitive(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        TextBox TextBox_InsertObservationDate = (TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertObservationDate");
        string DateToValidate_ObservationDate = TextBox_InsertObservationDate.Text.ToString();
        DateTime ValidatedDate_ObservationDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate_ObservationDate);

        if (ValidatedDate_ObservationDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid Observation date, date must be in the format yyyy/mm/dd<br />";
        }

        TextBox TextBox_InsertOnsetDate = (TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertOnsetDate");
        string DateToValidate_OnsetDate = TextBox_InsertOnsetDate.Text.ToString();
        DateTime ValidatedDate_OnsetDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate_OnsetDate);

        if (ValidatedDate_OnsetDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid Onset date, date must be in the format yyyy/mm/dd<br />";
        }


        if (string.IsNullOrEmpty(InvalidFormMessage))
        {
          DateTime PickedObservationDate = Convert.ToDateTime(((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertObservationDate")).Text, CultureInfo.CurrentCulture);
          DateTime PickedOnsetDate = Convert.ToDateTime(((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertOnsetDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedObservationDate.CompareTo(PickedOnsetDate) < 0)
          {
            InvalidFormMessage = InvalidFormMessage + "Observation date cannot be before Onset date<br />";
          }
          else
          {
            Session["CutOffDay"] = "";
            string SQLStringCutOffDay = "SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 25";
            using (SqlCommand SqlCommand_CutOffDay = new SqlCommand(SQLStringCutOffDay))
            {
              DataTable DataTable_CutOffDay;
              using (DataTable_CutOffDay = new DataTable())
              {
                DataTable_CutOffDay.Locale = CultureInfo.CurrentCulture;
                DataTable_CutOffDay = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CutOffDay).Copy();
                if (DataTable_CutOffDay.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_CutOffDay.Rows)
                  {
                    Session["CutOffDay"] = DataRow_Row["ValidCutOffDay"];
                  }
                }
              }
            }

            if (string.IsNullOrEmpty(Session["CutOffDay"].ToString()))
            {
              if (PickedObservationDate.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future Observation dates allowed<br />";
              }
            }
            else
            {
              int CutOffDay = Convert.ToInt32(Session["CutOffDay"].ToString(), CultureInfo.CurrentCulture);

              if (PickedObservationDate.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future Observation dates allowed<br />";
              }
              else
              {
                Session["CorrectDate"] = InsertValidation_CorrectDate(PickedObservationDate, CurrentDate, CutOffDay);

                if (Session["CorrectDate"].ToString() == "0")
                {
                  InvalidFormMessage = InvalidFormMessage + "Date selection is not valid. Forms may be captured between the 1st of a calendar month until the " + CutOffDay + "th of the following month <br />";
                }
                Session["CorrectDate"] = "";
              }
            }
            Session["CutOffDay"] = "";

            Session["FIMFAMElementsId"] = "";
            string SQLStringFIMFAM = "SELECT FIMFAM_Elements_Id FROM InfoQuest_Form_FIMFAM_Elements WHERE Facility_Id = @Facility_Id AND FIMFAM_Elements_PatientVisitNumber = @FIMFAM_Elements_PatientVisitNumber AND FIMFAM_Elements_ObservationDate = @FIMFAM_Elements_ObservationDate";
            using (SqlCommand SqlCommand_FIMFAM = new SqlCommand(SQLStringFIMFAM))
            {
              SqlCommand_FIMFAM.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
              SqlCommand_FIMFAM.Parameters.AddWithValue("@FIMFAM_Elements_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
              SqlCommand_FIMFAM.Parameters.AddWithValue("@FIMFAM_Elements_ObservationDate", PickedObservationDate);
              DataTable DataTable_FIMFAM;
              using (DataTable_FIMFAM = new DataTable())
              {
                DataTable_FIMFAM.Locale = CultureInfo.CurrentCulture;
                DataTable_FIMFAM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FIMFAM).Copy();
                if (DataTable_FIMFAM.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_FIMFAM.Rows)
                  {
                    Session["FIMFAMElementsId"] = DataRow_Row["FIMFAM_Elements_Id"];
                  }
                }
              }
            }

            if (!string.IsNullOrEmpty(Session["FIMFAMElementsId"].ToString()))
            {
              InvalidFormMessage = InvalidFormMessage + "A form has already been captured for this <br />Observation Date : " + PickedObservationDate.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture) + "<br />";
            }
            Session["FIMFAMElementsId"] = "";

            if (PickedOnsetDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future Onset dates allowed<br />";
            }
          }
        }
      }

      return InvalidFormMessage;
    }

    protected string InsertValidation_Motor(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareEating")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareGrooming")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareBathing")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareDressingUpper")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareDressingLower")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareToileting")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSelfcareSwallowing")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder1")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBladder2")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel1")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertSphincterBowel2")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferBCW")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferToilet")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferTS")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertTransferCarTransfer")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionWW")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionStairs")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertLocomotionCommunityAccess")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string InsertValidation_Cognitive(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationAV")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationVN")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationReading")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationWriting")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCommunicationSpeech")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustSocialInteraction")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustEmotionalStatus")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustAdjustment")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertPSAdjustEmployability")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionProblemSolving")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionMemory")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionOrientation")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionAttention")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_InsertCognitiveFunctionSafetyJudgement")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected static string InsertValidation_CorrectDate(DateTime pickedObservationDate, DateTime currentDate, int cutoffDay)
    {
      string CorrectDate = "";

      int PickedDateMonth = pickedObservationDate.Month;
      int PickedDateYear = pickedObservationDate.Year;

      int CurrentDateDay = currentDate.Day;
      int CurrentDateMonth = currentDate.Month;
      int CurrentDateYear = currentDate.Year;

      if ((PickedDateYear == CurrentDateYear) && (PickedDateMonth == CurrentDateMonth))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth + 1 == CurrentDateMonth) && (PickedDateYear == CurrentDateYear) && (CurrentDateDay < cutoffDay))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth + 1 == CurrentDateMonth) && (PickedDateYear == CurrentDateYear) && (CurrentDateDay > cutoffDay))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth + 1 < CurrentDateMonth) && (PickedDateYear == CurrentDateYear))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth == 12) && (CurrentDateMonth == 1) && (PickedDateYear + 1 == CurrentDateYear) && (CurrentDateDay < cutoffDay))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth == 12) && (CurrentDateMonth == 1) && (PickedDateYear + 1 == CurrentDateYear) && (CurrentDateDay > cutoffDay))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth <= 12) && (CurrentDateMonth > 1) && (PickedDateYear + 1 == CurrentDateYear))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth <= 12) && (CurrentDateMonth > 1) && (PickedDateYear + 1 < CurrentDateYear))
      {
        CorrectDate = "0";
      }

      return CorrectDate;
    }

    protected void SqlDataSource_FIMFAM_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["FIMFAM_Elements_Id"] = e.Command.Parameters["@FIMFAM_Elements_Id"].Value;
        Session["FIMFAM_Elements_ReportNumber"] = e.Command.Parameters["@FIMFAM_Elements_ReportNumber"].Value;
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_FIMFAM&ReportNumber=" + Session["FIMFAM_Elements_ReportNumber"].ToString() + ""), false);
      }
    }


    protected void FormView_FIMFAM_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDFIMFAMElementsModifiedDate"] = e.OldValues["FIMFAM_Elements_ModifiedDate"];
        object OLDFIMFAMElementsModifiedDate = Session["OLDFIMFAMElementsModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDFIMFAMElementsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareFIMFAM = (DataView)SqlDataSource_FIMFAM_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareFIMFAM = DataView_CompareFIMFAM[0];
        Session["DBFIMFAMElementsModifiedDate"] = Convert.ToString(DataRowView_CompareFIMFAM["FIMFAM_Elements_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBFIMFAMElementsModifiedBy"] = Convert.ToString(DataRowView_CompareFIMFAM["FIMFAM_Elements_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBFIMFAMElementsModifiedDate = Session["DBFIMFAMElementsModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBFIMFAMElementsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          Page.MaintainScrollPositionOnPostBack = false;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBFIMFAMElementsModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_FIMFAM_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_FIMFAM_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation(e.OldValues["FIMFAM_Elements_ObservationDate"], e.OldValues["FIMFAM_Elements_OnsetDate"]);

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
            Page.MaintainScrollPositionOnPostBack = false;
            ((Label)FormView_FIMFAM_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_FIMFAM_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["FIMFAM_Elements_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["FIMFAM_Elements_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_FIMFAM_Elements", "FIMFAM_Elements_Id = " + Request.QueryString["FIMFAM_Elements_Id"]);

            DataView DataView_FIMFAM = (DataView)SqlDataSource_FIMFAM_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_FIMFAM = DataView_FIMFAM[0];
            Session["FIMFAMElementsHistory"] = Convert.ToString(DataRowView_FIMFAM["FIMFAM_Elements_History"], CultureInfo.CurrentCulture);

            Session["FIMFAMElementsHistory"] = Session["History"].ToString() + Session["FIMFAMElementsHistory"].ToString();
            e.NewValues["FIMFAM_Elements_History"] = Session["FIMFAMElementsHistory"].ToString();

            Session["FIMFAMElementsHistory"] = "";
            Session["History"] = "";


            int MotorSubScore = 0;
            MotorSubScore = Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareEating")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareGrooming")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareBathing")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareDressingUpper")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareDressingLower")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareToileting")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareSwallowing")).SelectedValue, CultureInfo.CurrentCulture) +

                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferBCW")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferToilet")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferTS")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferCarTransfer")).SelectedValue, CultureInfo.CurrentCulture) +

                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionWW")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionStairs")).SelectedValue, CultureInfo.CurrentCulture) +
                Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionCommunityAccess")).SelectedValue, CultureInfo.CurrentCulture);

            if (Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder1")).SelectedValue, CultureInfo.CurrentCulture) <= Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder2")).SelectedValue, CultureInfo.CurrentCulture))
            {
              MotorSubScore = MotorSubScore + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder1")).SelectedValue, CultureInfo.CurrentCulture);
            }
            else
            {
              MotorSubScore = MotorSubScore + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder2")).SelectedValue, CultureInfo.CurrentCulture);
            }

            if (Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel1")).SelectedValue, CultureInfo.CurrentCulture) <= Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel2")).SelectedValue, CultureInfo.CurrentCulture))
            {
              MotorSubScore = MotorSubScore + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel1")).SelectedValue, CultureInfo.CurrentCulture);
            }
            else
            {
              MotorSubScore = MotorSubScore + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel2")).SelectedValue, CultureInfo.CurrentCulture);
            }

            e.NewValues["FIMFAM_Elements_MotorSubScore"] = MotorSubScore.ToString(CultureInfo.CurrentCulture);

            int CognitiveSubScore = 0;
            CognitiveSubScore = Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationAV")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationVN")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationReading")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationWriting")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationSpeech")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustSocialInteraction")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustEmotionalStatus")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustAdjustment")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustEmployability")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionProblemSolving")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionMemory")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionOrientation")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionAttention")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionSafetyJudgement")).SelectedValue, CultureInfo.CurrentCulture);

            e.NewValues["FIMFAM_Elements_CognitiveSubScore"] = CognitiveSubScore.ToString(CultureInfo.CurrentCulture);

            int Total = 0;
            Total = Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareEating")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareGrooming")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareBathing")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareDressingUpper")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareDressingLower")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareToileting")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareSwallowing")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferBCW")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferToilet")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferTS")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferCarTransfer")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionWW")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionStairs")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionCommunityAccess")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationAV")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationVN")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationReading")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationWriting")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationSpeech")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustSocialInteraction")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustEmotionalStatus")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustAdjustment")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustEmployability")).SelectedValue, CultureInfo.CurrentCulture) +

              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionProblemSolving")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionMemory")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionOrientation")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionAttention")).SelectedValue, CultureInfo.CurrentCulture) +
              Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionSafetyJudgement")).SelectedValue, CultureInfo.CurrentCulture);

            if (Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder1")).SelectedValue, CultureInfo.CurrentCulture) <= Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder2")).SelectedValue, CultureInfo.CurrentCulture))
            {
              Total = Total + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder1")).SelectedValue, CultureInfo.CurrentCulture);
            }
            else
            {
              Total = Total + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder2")).SelectedValue, CultureInfo.CurrentCulture);
            }

            if (Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel1")).SelectedValue, CultureInfo.CurrentCulture) <= Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel2")).SelectedValue, CultureInfo.CurrentCulture))
            {
              Total = Total + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel1")).SelectedValue, CultureInfo.CurrentCulture);
            }
            else
            {
              Total = Total + Convert.ToInt32(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel2")).SelectedValue, CultureInfo.CurrentCulture);
            }

            e.NewValues["FIMFAM_Elements_Total"] = Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDFIMFAMElementsModifiedDate"] = "";
        Session["DBFIMFAMElementsModifiedDate"] = "";
        Session["DBFIMFAMElementsModifiedBy"] = "";
      }
    }

    protected string EditValidation(object oldFIMFAM_Elements_ObservationDate, object oldFIMFAM_Elements_OnsetDate)
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        InvalidForm = EditValidation_Motor(InvalidForm);

        InvalidForm = EditValidation_Cognitive(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        TextBox TextBox_EditObservationDate = (TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditObservationDate");
        string DateToValidate_ObservationDate = TextBox_EditObservationDate.Text.ToString();
        DateTime ValidatedDate_ObservationDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate_ObservationDate);

        if (ValidatedDate_ObservationDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid Observation date, date must be in the format yyyy/mm/dd<br />";
        }

        TextBox TextBox_EditOnsetDate = (TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditOnsetDate");
        string DateToValidate_OnsetDate = TextBox_EditOnsetDate.Text.ToString();
        DateTime ValidatedDate_OnsetDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate_OnsetDate);

        if (ValidatedDate_OnsetDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid Onset date, date must be in the format yyyy/mm/dd<br />";
        }

        InvalidFormMessage = EditValidation_ControlValidation(InvalidFormMessage, oldFIMFAM_Elements_ObservationDate, oldFIMFAM_Elements_OnsetDate);
      }

      return InvalidFormMessage;
    }

    protected string EditValidation_Motor(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareEating")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareGrooming")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareBathing")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareDressingUpper")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareDressingLower")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareToileting")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSelfcareSwallowing")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder1")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBladder2")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel1")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditSphincterBowel2")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferBCW")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferToilet")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferTS")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditTransferCarTransfer")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionWW")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionStairs")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditLocomotionCommunityAccess")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string EditValidation_Cognitive(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationAV")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationVN")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationReading")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationWriting")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCommunicationSpeech")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustSocialInteraction")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustEmotionalStatus")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustAdjustment")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditPSAdjustEmployability")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionProblemSolving")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionMemory")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionOrientation")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionAttention")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_FIMFAM_Form.FindControl("RadioButtonList_EditCognitiveFunctionSafetyJudgement")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string EditValidation_ControlValidation(string invalidFormMessage, object oldFIMFAM_Elements_ObservationDate, object oldFIMFAM_Elements_OnsetDate)
    {
      string InvalidFormMessage = invalidFormMessage;

      if (string.IsNullOrEmpty(InvalidFormMessage))
      {
        string ObservationDate = Convert.ToDateTime(oldFIMFAM_Elements_ObservationDate, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        string OnsetDate = Convert.ToDateTime(oldFIMFAM_Elements_OnsetDate, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);

        DateTime DBObservationDate = Convert.ToDateTime(ObservationDate, CultureInfo.CurrentCulture);
        DateTime DBOnsetDate = Convert.ToDateTime(OnsetDate, CultureInfo.CurrentCulture);
        DateTime PickedObservationDate = Convert.ToDateTime(((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditObservationDate")).Text, CultureInfo.CurrentCulture);
        DateTime PickedOnsetDate = Convert.ToDateTime(((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditOnsetDate")).Text, CultureInfo.CurrentCulture);
        DateTime CurrentDate = DateTime.Now;

        if (PickedObservationDate.CompareTo(PickedOnsetDate) < 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Observation date cannot be before Onset date<br />";
        }
        else
        {
          if ((PickedObservationDate).CompareTo(DBObservationDate) != 0)
          {
            Session["CutOffDay"] = "";
            string SQLStringCutOffDay = "SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 25";
            using (SqlCommand SqlCommand_CutOffDay = new SqlCommand(SQLStringCutOffDay))
            {
              DataTable DataTable_CutOffDay;
              using (DataTable_CutOffDay = new DataTable())
              {
                DataTable_CutOffDay.Locale = CultureInfo.CurrentCulture;
                DataTable_CutOffDay = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CutOffDay).Copy();
                if (DataTable_CutOffDay.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_CutOffDay.Rows)
                  {
                    Session["CutOffDay"] = DataRow_Row["ValidCutOffDay"];
                  }
                }
              }
            }

            if (string.IsNullOrEmpty(Session["CutOffDay"].ToString()))
            {
              if (PickedObservationDate.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future Observation dates allowed<br />";
              }
            }
            else
            {
              int CutOffDay = Convert.ToInt32(Session["CutOffDay"].ToString(), CultureInfo.CurrentCulture);

              if (PickedObservationDate.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future Observation dates allowed<br />";
              }
              else
              {
                int PickedDateMonth = PickedObservationDate.Month;
                int PickedDateYear = PickedObservationDate.Year;

                Session["CorrectDate"] = EditValidation_CorrectDate(PickedObservationDate, CurrentDate, CutOffDay);

                if (Session["CorrectDate"].ToString() == "0")
                {
                  DateTime OldObserbationDate = Convert.ToDateTime(oldFIMFAM_Elements_ObservationDate, CultureInfo.CurrentCulture);

                  int OldPickedDateMonth = OldObserbationDate.Month;
                  int OldPickedDateYear = OldObserbationDate.Year;

                  if ((PickedDateMonth != OldPickedDateMonth) || (PickedDateYear != OldPickedDateYear))
                  {
                    InvalidFormMessage = InvalidFormMessage + "Date selection is not valid. Forms may be captured between the 1st of a calendar month until the " + CutOffDay + "th of the following month<br />";
                  }
                }
                Session["CorrectDate"] = "";
              }
            }
            Session["CutOffDay"] = "";

            Session["FIMFAMElementsId"] = "";
            string SQLStringFIMFAM = "SELECT FIMFAM_Elements_Id FROM InfoQuest_Form_FIMFAM_Elements WHERE Facility_Id = @Facility_Id AND FIMFAM_Elements_PatientVisitNumber = @FIMFAM_Elements_PatientVisitNumber AND FIMFAM_Elements_ObservationDate = @FIMFAM_Elements_ObservationDate";
            using (SqlCommand SqlCommand_FIMFAM = new SqlCommand(SQLStringFIMFAM))
            {
              SqlCommand_FIMFAM.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
              SqlCommand_FIMFAM.Parameters.AddWithValue("@FIMFAM_Elements_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
              SqlCommand_FIMFAM.Parameters.AddWithValue("@FIMFAM_Elements_ObservationDate", PickedObservationDate);
              DataTable DataTable_FIMFAM;
              using (DataTable_FIMFAM = new DataTable())
              {
                DataTable_FIMFAM.Locale = CultureInfo.CurrentCulture;
                DataTable_FIMFAM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FIMFAM).Copy();
                if (DataTable_FIMFAM.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_FIMFAM.Rows)
                  {
                    Session["FIMFAMElementsId"] = DataRow_Row["FIMFAM_Elements_Id"];
                  }
                }
              }
            }

            if (!string.IsNullOrEmpty(Session["FIMFAMElementsId"].ToString()))
            {
              InvalidFormMessage = InvalidFormMessage + "A form has already been captured for this <br />Observation Date : " + PickedObservationDate.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture) + "<br />";
            }
            Session["FIMFAMElementsId"] = "";
          }

          if ((PickedOnsetDate).CompareTo(DBOnsetDate) != 0)
          {
            if (PickedOnsetDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future Onset dates allowed<br />";
            }
          }
        }

        Session["ObservationDate"] = "";
        Session["OnsetDate"] = "";
      }

      return InvalidFormMessage;
    }

    protected static string EditValidation_CorrectDate(DateTime pickedObservationDate, DateTime currentDate, int cutoffDay)
    {
      string CorrectDate = "";

      int PickedDateMonth = pickedObservationDate.Month;
      int PickedDateYear = pickedObservationDate.Year;

      int CurrentDateDay = currentDate.Day;
      int CurrentDateMonth = currentDate.Month;
      int CurrentDateYear = currentDate.Year;

      if ((PickedDateYear == CurrentDateYear) && (PickedDateMonth == CurrentDateMonth))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth + 1 == CurrentDateMonth) && (PickedDateYear == CurrentDateYear) && (CurrentDateDay < cutoffDay))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth + 1 == CurrentDateMonth) && (PickedDateYear == CurrentDateYear) && (CurrentDateDay > cutoffDay))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth + 1 < CurrentDateMonth) && (PickedDateYear == CurrentDateYear))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth == 12) && (CurrentDateMonth == 1) && (PickedDateYear + 1 == CurrentDateYear) && (CurrentDateDay < cutoffDay))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth == 12) && (CurrentDateMonth == 1) && (PickedDateYear + 1 == CurrentDateYear) && (CurrentDateDay > cutoffDay))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth <= 12) && (CurrentDateMonth > 1) && (PickedDateYear + 1 == CurrentDateYear))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth <= 12) && (CurrentDateMonth > 1) && (PickedDateYear + 1 < CurrentDateYear))
      {
        CorrectDate = "0";
      }

      return CorrectDate;
    }

    protected void SqlDataSource_FIMFAM_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Session["FIMFAMElementsOnsetDate"] = "";
          string SQLStringOnsetDate = "SELECT TOP 1 FIMFAM_Elements_OnsetDate FROM InfoQuest_Form_FIMFAM_Elements WHERE Facility_Id = @Facility_Id AND FIMFAM_Elements_PatientVisitNumber = @FIMFAM_Elements_PatientVisitNumber AND FIMFAM_Elements_OnsetDate IS NOT NULL ORDER BY FIMFAM_Elements_Id";
          using (SqlCommand SqlCommand_OnsetDate = new SqlCommand(SQLStringOnsetDate))
          {
            SqlCommand_OnsetDate.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
            SqlCommand_OnsetDate.Parameters.AddWithValue("@FIMFAM_Elements_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
            DataTable DataTable_OnsetDate;
            using (DataTable_OnsetDate = new DataTable())
            {
              DataTable_OnsetDate.Locale = CultureInfo.CurrentCulture;
              DataTable_OnsetDate = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OnsetDate).Copy();
              if (DataTable_OnsetDate.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_OnsetDate.Rows)
                {
                  Session["FIMFAMElementsOnsetDate"] = DataRow_Row["FIMFAM_Elements_OnsetDate"];
                }
              }
            }
          }

          string SQLStringUpdateOnsetDate = "UPDATE InfoQuest_Form_FIMFAM_Elements SET FIMFAM_Elements_OnsetDate = @OnsetDate WHERE Facility_Id = @Facility_Id AND FIMFAM_Elements_PatientVisitNumber = @FIMFAM_Elements_PatientVisitNumber";
          using (SqlCommand SqlCommand_UpdateOnsetDate = new SqlCommand(SQLStringUpdateOnsetDate))
          {
            SqlCommand_UpdateOnsetDate.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
            SqlCommand_UpdateOnsetDate.Parameters.AddWithValue("@FIMFAM_Elements_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
            SqlCommand_UpdateOnsetDate.Parameters.AddWithValue("@OnsetDate", Convert.ToDateTime(Session["FIMFAMElementsOnsetDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture));
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateOnsetDate);
          }
          Session["FIMFAMElementsOnsetDate"] = "";

          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("FIMFAM Form", "Form_FIMFAM.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_FIMFAM_PatientVisitNumber=" + Request.QueryString["s_FIMFAM_PatientVisitNumber"] + ""), false);
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_FIMFAM, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("FIMFAM Print", "InfoQuest_Print.aspx?PrintPage=Form_FIMFAM&PrintValue=" + Request.QueryString["FIMFAM_Elements_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_FIMFAM, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_FIMFAM, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("FIMFAM Email", "InfoQuest_Email.aspx?EmailPage=Form_FIMFAM&EmailValue=" + Request.QueryString["FIMFAM_Elements_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_FIMFAM, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }


    protected void FormView_FIMFAM_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["FIMFAM_Elements_Id"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("FIMFAM Form", "Form_FIMFAM.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_FIMFAM_PatientVisitNumber=" + Request.QueryString["s_FIMFAM_PatientVisitNumber"] + ""), false);
          }
        }
      }
    }

    protected void FormView_FIMFAM_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_FIMFAM_Form.CurrentMode == FormViewMode.Insert)
      {
        InsertDataBound();
      }

      if (FormView_FIMFAM_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_FIMFAM_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void InsertDataBound()
    {
      Session["FIMFAMElementsOnsetDate"] = "";
      string SQLStringOnsetDate = "SELECT DISTINCT FIMFAM_Elements_OnsetDate FROM InfoQuest_Form_FIMFAM_Elements WHERE Facility_Id = @Facility_Id AND FIMFAM_Elements_PatientVisitNumber = @FIMFAM_Elements_PatientVisitNumber AND FIMFAM_Elements_OnsetDate IS NOT NULL";
      using (SqlCommand SqlCommand_OnsetDate = new SqlCommand(SQLStringOnsetDate))
      {
        SqlCommand_OnsetDate.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_OnsetDate.Parameters.AddWithValue("@FIMFAM_Elements_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
        DataTable DataTable_OnsetDate;
        using (DataTable_OnsetDate = new DataTable())
        {
          DataTable_OnsetDate.Locale = CultureInfo.CurrentCulture;
          DataTable_OnsetDate = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OnsetDate).Copy();
          if (DataTable_OnsetDate.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OnsetDate.Rows)
            {
              Session["FIMFAMElementsOnsetDate"] = DataRow_Row["FIMFAM_Elements_OnsetDate"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FIMFAMElementsOnsetDate"].ToString()))
      {
        ((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertOnsetDate")).Text = Convert.ToDateTime(Session["FIMFAMElementsOnsetDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }
      Session["FIMFAMElementsOnsetDate"] = "";
    }

    protected void EditDataBound()
    {
      if (Request.QueryString["FIMFAM_Elements_Id"] != null)
      {
        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 25";
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
          ((Button)FormView_FIMFAM_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_FIMFAM_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_FIMFAM_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_FIMFAM_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["FIMFAM_Elements_Id"] != null)
      {
        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 25";
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
          ((Button)FormView_FIMFAM_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_FIMFAM_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_FIMFAM_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("FIMFAM Print", "InfoQuest_Print.aspx?PrintPage=Form_FIMFAM&PrintValue=" + Request.QueryString["FIMFAM_Elements_Id"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_FIMFAM_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_FIMFAM_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_FIMFAM_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("FIMFAM Email", "InfoQuest_Email.aspx?EmailPage=Form_FIMFAM&EmailValue=" + Request.QueryString["FIMFAM_Elements_Id"] + "") + "')";
        }

        Email = "";
        Print = "";
      }
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

    protected void TextBox_InsertObservationDate_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertObservationDate = (TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertObservationDate");
      string DateToValidate = TextBox_InsertObservationDate.Text.ToString();
      DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

      string ValidDates = "Yes";
      if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        ((Label)FormView_FIMFAM_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Convert.ToString("Not a valid Observation date, date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
        ValidDates = "No";
      }

      if (ValidDates == "Yes")
      {
        DateTime PickedObservationDate = Convert.ToDateTime(((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_InsertObservationDate")).Text, CultureInfo.CurrentCulture);

        Session["FIMFAMElementsId"] = "";
        string SQLStringFIMFAM = "SELECT FIMFAM_Elements_Id FROM InfoQuest_Form_FIMFAM_Elements WHERE Facility_Id = @Facility_Id AND FIMFAM_Elements_PatientVisitNumber = @FIMFAM_Elements_PatientVisitNumber AND FIMFAM_Elements_ObservationDate = @FIMFAM_Elements_ObservationDate";
        using (SqlCommand SqlCommand_FIMFAM = new SqlCommand(SQLStringFIMFAM))
        {
          SqlCommand_FIMFAM.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_FIMFAM.Parameters.AddWithValue("@FIMFAM_Elements_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
          SqlCommand_FIMFAM.Parameters.AddWithValue("@FIMFAM_Elements_ObservationDate", PickedObservationDate);
          DataTable DataTable_FIMFAM;
          using (DataTable_FIMFAM = new DataTable())
          {
            DataTable_FIMFAM.Locale = CultureInfo.CurrentCulture;
            DataTable_FIMFAM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FIMFAM).Copy();
            if (DataTable_FIMFAM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FIMFAM.Rows)
              {
                Session["FIMFAMElementsId"] = DataRow_Row["FIMFAM_Elements_Id"];
              }
            }
            else
            {
              Session["FIMFAMElementsId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FIMFAMElementsId"].ToString()))
        {
          ((Label)FormView_FIMFAM_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Convert.ToString("A form has already been captured for this <br />Observation Date : " + PickedObservationDate.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture) + "", CultureInfo.CurrentCulture);
        }
        else
        {
          ((Label)FormView_FIMFAM_Form.FindControl("Label_InsertInvalidFormMessage")).Text = "";
        }
        Session["FIMFAMElementsId"] = "";
      }
    }

    protected void TextBox_EditObservationDate_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditObservationDate = (TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditObservationDate");
      string DateToValidate = TextBox_EditObservationDate.Text.ToString();
      DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

      string ValidDates = "Yes";
      if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        ((Label)FormView_FIMFAM_Form.FindControl("Label_EditInvalidFormMessage")).Text = Convert.ToString("Not a valid Observation date, date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
        ValidDates = "No";
      }

      if (ValidDates == "Yes")
      {
        DateTime PickedObservationDate = Convert.ToDateTime(((TextBox)FormView_FIMFAM_Form.FindControl("TextBox_EditObservationDate")).Text, CultureInfo.CurrentCulture);

        Session["FIMFAMElementsId"] = "";
        string SQLStringFIMFAM = "SELECT FIMFAM_Elements_Id FROM InfoQuest_Form_FIMFAM_Elements WHERE Facility_Id = @Facility_Id AND FIMFAM_Elements_PatientVisitNumber = @FIMFAM_Elements_PatientVisitNumber AND FIMFAM_Elements_ObservationDate = @FIMFAM_Elements_ObservationDate";
        using (SqlCommand SqlCommand_FIMFAM = new SqlCommand(SQLStringFIMFAM))
        {
          SqlCommand_FIMFAM.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_FIMFAM.Parameters.AddWithValue("@FIMFAM_Elements_PatientVisitNumber", Request.QueryString["s_FIMFAM_PatientVisitNumber"]);
          SqlCommand_FIMFAM.Parameters.AddWithValue("@FIMFAM_Elements_ObservationDate", PickedObservationDate);
          DataTable DataTable_FIMFAM;
          using (DataTable_FIMFAM = new DataTable())
          {
            DataTable_FIMFAM.Locale = CultureInfo.CurrentCulture;
            DataTable_FIMFAM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FIMFAM).Copy();
            if (DataTable_FIMFAM.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FIMFAM.Rows)
              {
                Session["FIMFAMElementsId"] = DataRow_Row["FIMFAM_Elements_Id"];
              }
            }
            else
            {
              Session["FIMFAMElementsId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FIMFAMElementsId"].ToString()))
        {
          if (Session["FIMFAMElementsId"].ToString() == Request.QueryString["FIMFAM_Elements_Id"])
          {
            ((Label)FormView_FIMFAM_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          }
          else
          {
            ((Label)FormView_FIMFAM_Form.FindControl("Label_EditInvalidFormMessage")).Text = Convert.ToString("A form has already been captured for this <br />Observation Date : " + PickedObservationDate.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture) + "", CultureInfo.CurrentCulture);
          }
        }
        else
        {
          ((Label)FormView_FIMFAM_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
        }
        Session["FIMFAMElementsId"] = "";
      }
    }
    //---END--- --TableForm--//


    //--START-- --TableList--//
    protected void SqlDataSource_FIMFAM_Elements_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_FIMFAM_Elements.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_FIMFAM_Elements.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_FIMFAM_Elements.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_FIMFAM_Elements.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_FIMFAM_Elements_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_FIMFAM_Elements.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_FIMFAM_Elements.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_FIMFAM_Elements.PageSize > 20 && GridView_FIMFAM_Elements.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_FIMFAM_Elements.PageSize > 50 && GridView_FIMFAM_Elements.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_FIMFAM_Elements_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_FIMFAM_Elements.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_FIMFAM_Elements.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_FIMFAM_Elements.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_FIMFAM_Elements_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_CaptureNew_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FIMFAM.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_FIMFAM_PatientVisitNumber=" + Request.QueryString["s_FIMFAM_PatientVisitNumber"] + "", false);
    }

    public string GetLink(object fimfam_Elements_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "" +
          "<a href='Form_FIMFAM.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_FIMFAM_PatientVisitNumber=" + Request.QueryString["s_FIMFAM_PatientVisitNumber"] + "&FIMFAM_Elements_Id=" + fimfam_Elements_Id + "&ViewMode=0'>View</a>&nbsp;/&nbsp;" +
          "<a href='Form_FIMFAM.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_FIMFAM_PatientVisitNumber=" + Request.QueryString["s_FIMFAM_PatientVisitNumber"] + "&FIMFAM_Elements_Id=" + fimfam_Elements_Id + "&ViewMode=1'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "" +
          "<a href='Form_FIMFAM.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_FIMFAM_PatientVisitNumber=" + Request.QueryString["s_FIMFAM_PatientVisitNumber"] + "&FIMFAM_Elements_Id=" + fimfam_Elements_Id + "&ViewMode=0'>View</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      FinalURL = CurrentURL;

      return FinalURL;
    }
    //--START-- --TableList--//
  }
}