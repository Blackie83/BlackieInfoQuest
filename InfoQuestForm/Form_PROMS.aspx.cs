using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_PROMS : InfoQuestWCF.Override_SystemWebUIPage
  {
    private int TotalQuestions = 12;
    private bool Button_EditForm0UpdateClicked = false;
    private bool Button_EditForm1UpdateClicked = false;
    private bool Button_EditForm0PrintClicked = false;
    private bool Button_EditForm0EmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          TablePatientInfo.Visible = false;
          TableForm.Visible = false;
          TableForm0.Visible = false;
          TableForm1.Visible = false;
          TableList.Visible = false;


          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("30")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("30").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_PatientInfoHeading.Text = Convert.ToString("Patient Information", CultureInfo.CurrentCulture);
          Label_QuestionnaireHeading.Text = Convert.ToString("Questionnaire", CultureInfo.CurrentCulture);
          Label_FollowUpHeading.Text = Convert.ToString("Follow Up Questionnaire", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("30").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_PROMS_PatientVisitNumber"] != null)
          {
            if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
            {
              SqlDataSource_PROMS_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
              SqlDataSource_PROMS_Facility.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_PROMS_Questionnaire";
              SqlDataSource_PROMS_Facility.SelectParameters["TableWHERE"].DefaultValue = "PROMS_Questionnaire_Id = " + Request.QueryString["PROMS_Questionnaire_Id"] + "";
            }

            TablePatientInfo.Visible = true;
            TableForm.Visible = true;
            TableForm0.Visible = true;
            TableForm1.Visible = true;
            TableList.Visible = true;

            PatientDataPI();

            if (TablePatientInfo.Visible == true)
            {
              SetFormVisibility();
            }
          }
          else
          {
            Label_InvalidSearch.Text = "";
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            TableForm0.Visible = false;
            TableForm1.Visible = false;
            TableList.Visible = false;
          }


          if (TablePatientInfo.Visible == true)
          {
            TablePatientInfoVisible();
          }

          if (TableForm0.Visible == true)
          {
            TableForm0Visible();
          }

          if (TableForm1.Visible == true)
          {
            TableForm1Visible();
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
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('30'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('30')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
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
      if (PageSecurity() == "1")
      {

      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_PROMS_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_PROMS_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PROMS_Facility.SelectParameters.Clear();
      SqlDataSource_PROMS_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_PROMS_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "30");
      SqlDataSource_PROMS_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_PROMS_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PROMS_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PROMS_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PROMS_InsertQuestionnaireList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_InsertQuestionnaireList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 30 AND ListCategory_Id = 79 AND ListItem_Parent = -1 ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_PROMS_InsertQuestionnaireList.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_PROMS_InsertLanguageList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_InsertLanguageList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 30 AND ListCategory_Id = 104 AND ListItem_Parent = -1 ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_PROMS_InsertLanguageList.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_PROMS_EditQuestionnaireList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_EditQuestionnaireList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 30 AND ListCategory_Id = 79 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_PROMS_Questionnaire.PROMS_Questionnaire_Questionnaire_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_PROMS_Questionnaire , Administration_ListItem WHERE InfoQuest_Form_PROMS_Questionnaire.PROMS_Questionnaire_Questionnaire_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_PROMS_Questionnaire.PROMS_Questionnaire_Id = @PROMS_Questionnaire_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_PROMS_EditQuestionnaireList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_PROMS_EditQuestionnaireList.SelectParameters.Clear();
      SqlDataSource_PROMS_EditQuestionnaireList.SelectParameters.Add("PROMS_Questionnaire_Id", TypeCode.String, Request.QueryString["PROMS_Questionnaire_Id"]);

      SqlDataSource_PROMS_EditLanguageList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_EditLanguageList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 30 AND ListCategory_Id = 104 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_PROMS_Questionnaire.PROMS_Questionnaire_Language_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_PROMS_Questionnaire , Administration_ListItem WHERE InfoQuest_Form_PROMS_Questionnaire.PROMS_Questionnaire_Language_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_PROMS_Questionnaire.PROMS_Questionnaire_Id = @PROMS_Questionnaire_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_PROMS_EditLanguageList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_PROMS_EditLanguageList.SelectParameters.Clear();
      SqlDataSource_PROMS_EditLanguageList.SelectParameters.Add("PROMS_Questionnaire_Id", TypeCode.String, Request.QueryString["PROMS_Questionnaire_Id"]);

      SqlDataSource_PROMS_Questionnaire_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_Questionnaire_Form.InsertCommand="INSERT INTO [InfoQuest_Form_PROMS_Questionnaire] ( Facility_Id ,PROMS_Questionnaire_PatientVisitNumber ,PROMS_Questionnaire_ReportNumber ,PROMS_Questionnaire_Questionnaire_List ,PROMS_Questionnaire_AdmissionDate ,PROMS_Questionnaire_Q1 ,PROMS_Questionnaire_Q2 ,PROMS_Questionnaire_Q3 ,PROMS_Questionnaire_Q4 ,PROMS_Questionnaire_Q5 ,PROMS_Questionnaire_Q6 ,PROMS_Questionnaire_Q7 ,PROMS_Questionnaire_Q8 ,PROMS_Questionnaire_Q9 ,PROMS_Questionnaire_Q10 ,PROMS_Questionnaire_Q11 ,PROMS_Questionnaire_Q12 ,PROMS_Questionnaire_Score ,PROMS_Questionnaire_ContactPatient ,PROMS_Questionnaire_ContactNumber ,PROMS_Questionnaire_Language_List ,PROMS_Questionnaire_EmailSend ,PROMS_Questionnaire_EmailDate ,PROMS_Questionnaire_CreatedDate ,PROMS_Questionnaire_CreatedBy ,PROMS_Questionnaire_ModifiedDate ,PROMS_Questionnaire_ModifiedBy ,PROMS_Questionnaire_History ,PROMS_Questionnaire_IsActive ) VALUES ( @Facility_Id ,@PROMS_Questionnaire_PatientVisitNumber ,@PROMS_Questionnaire_ReportNumber ,@PROMS_Questionnaire_Questionnaire_List ,@PROMS_Questionnaire_AdmissionDate ,@PROMS_Questionnaire_Q1 ,@PROMS_Questionnaire_Q2 ,@PROMS_Questionnaire_Q3 ,@PROMS_Questionnaire_Q4 ,@PROMS_Questionnaire_Q5 ,@PROMS_Questionnaire_Q6 ,@PROMS_Questionnaire_Q7 ,@PROMS_Questionnaire_Q8 ,@PROMS_Questionnaire_Q9 ,@PROMS_Questionnaire_Q10 ,@PROMS_Questionnaire_Q11 ,@PROMS_Questionnaire_Q12 ,@PROMS_Questionnaire_Score ,@PROMS_Questionnaire_ContactPatient ,@PROMS_Questionnaire_ContactNumber ,@PROMS_Questionnaire_Language_List ,@PROMS_Questionnaire_EmailSend ,@PROMS_Questionnaire_EmailDate ,@PROMS_Questionnaire_CreatedDate ,@PROMS_Questionnaire_CreatedBy ,@PROMS_Questionnaire_ModifiedDate ,@PROMS_Questionnaire_ModifiedBy ,@PROMS_Questionnaire_History ,@PROMS_Questionnaire_IsActive ); SELECT @PROMS_Questionnaire_Id = SCOPE_IDENTITY()";
      SqlDataSource_PROMS_Questionnaire_Form.SelectCommand="SELECT * FROM [InfoQuest_Form_PROMS_Questionnaire] WHERE ([PROMS_Questionnaire_Id] = @PROMS_Questionnaire_Id)";
      SqlDataSource_PROMS_Questionnaire_Form.UpdateCommand="UPDATE [InfoQuest_Form_PROMS_Questionnaire] SET PROMS_Questionnaire_Questionnaire_List = @PROMS_Questionnaire_Questionnaire_List ,PROMS_Questionnaire_AdmissionDate = @PROMS_Questionnaire_AdmissionDate ,PROMS_Questionnaire_Q1 = @PROMS_Questionnaire_Q1 ,PROMS_Questionnaire_Q2 = @PROMS_Questionnaire_Q2 ,PROMS_Questionnaire_Q3 = @PROMS_Questionnaire_Q3 ,PROMS_Questionnaire_Q4 = @PROMS_Questionnaire_Q4 ,PROMS_Questionnaire_Q5 = @PROMS_Questionnaire_Q5 ,PROMS_Questionnaire_Q6 = @PROMS_Questionnaire_Q6 ,PROMS_Questionnaire_Q7 = @PROMS_Questionnaire_Q7 ,PROMS_Questionnaire_Q8 = @PROMS_Questionnaire_Q8 ,PROMS_Questionnaire_Q9 = @PROMS_Questionnaire_Q9 ,PROMS_Questionnaire_Q10 = @PROMS_Questionnaire_Q10 ,PROMS_Questionnaire_Q11 = @PROMS_Questionnaire_Q11 ,PROMS_Questionnaire_Q12 = @PROMS_Questionnaire_Q12 ,PROMS_Questionnaire_Score = @PROMS_Questionnaire_Score ,PROMS_Questionnaire_ContactPatient = @PROMS_Questionnaire_ContactPatient ,PROMS_Questionnaire_ContactNumber = @PROMS_Questionnaire_ContactNumber , PROMS_Questionnaire_Language_List = @PROMS_Questionnaire_Language_List ,PROMS_Questionnaire_EmailSend = @PROMS_Questionnaire_EmailSend ,PROMS_Questionnaire_EmailDate = @PROMS_Questionnaire_EmailDate ,PROMS_Questionnaire_ModifiedDate = @PROMS_Questionnaire_ModifiedDate ,PROMS_Questionnaire_ModifiedBy = @PROMS_Questionnaire_ModifiedBy ,PROMS_Questionnaire_History = @PROMS_Questionnaire_History ,PROMS_Questionnaire_IsActive = @PROMS_Questionnaire_IsActive WHERE [PROMS_Questionnaire_Id] = @PROMS_Questionnaire_Id";
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Clear();
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Id", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_PatientVisitNumber", TypeCode.Int32, Request.QueryString["s_PROMS_PatientVisitNumber"]);
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_ReportNumber", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Questionnaire_List", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_AdmissionDate", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q1", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q2", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q3", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q4", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q5", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q6", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q7", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q8", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q9", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q10", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q11", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Q12", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Score", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_ContactPatient", TypeCode.Boolean, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_ContactNumber", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_Language_List", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_EmailSend", TypeCode.Boolean, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_EmailDate", TypeCode.DateTime, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_CreatedBy", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_History", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_PROMS_Questionnaire_Form.InsertParameters.Add("PROMS_Questionnaire_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PROMS_Questionnaire_Form.SelectParameters.Clear();
      SqlDataSource_PROMS_Questionnaire_Form.SelectParameters.Add("PROMS_Questionnaire_Id", TypeCode.Int32, Request.QueryString["PROMS_Questionnaire_Id"]);
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Clear();
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Questionnaire_List", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_AdmissionDate", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q1", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q2", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q3", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q4", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q5", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q6", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q7", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q8", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q9", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q10", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q11", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Q12", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Score", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_ContactPatient", TypeCode.Boolean, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_ContactNumber", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Language_List", TypeCode.Int32, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_EmailSend", TypeCode.Boolean, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_EmailDate", TypeCode.DateTime, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_History", TypeCode.String, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PROMS_Questionnaire_Form.UpdateParameters.Add("PROMS_Questionnaire_Id", TypeCode.Int32, "");

      SqlDataSource_PROMS_EditCanceledList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_EditCanceledList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 30 AND ListCategory_Id = 93 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_PROMS_FollowUp.PROMS_FollowUp_Cancelled_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_PROMS_FollowUp , Administration_ListItem WHERE InfoQuest_Form_PROMS_FollowUp.PROMS_FollowUp_Cancelled_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_PROMS_FollowUp.PROMS_FollowUp_Id = @PROMS_FollowUp_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_PROMS_EditCanceledList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_PROMS_EditCanceledList.SelectParameters.Clear();
      SqlDataSource_PROMS_EditCanceledList.SelectParameters.Add("PROMS_FollowUp_Id", TypeCode.String, Request.QueryString["PROMS_FollowUp_Id"]);


      SqlDataSource_PROMS_InsertCanceledList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_InsertCanceledList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 30 AND ListCategory_Id = 93 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_PROMS_FollowUp.PROMS_FollowUp_Cancelled_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_PROMS_FollowUp , Administration_ListItem WHERE InfoQuest_Form_PROMS_FollowUp.PROMS_FollowUp_Cancelled_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_PROMS_FollowUp.PROMS_FollowUp_Id = @PROMS_FollowUp_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_PROMS_InsertCanceledList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_PROMS_InsertCanceledList.CancelSelectOnNullParameter = false;
      SqlDataSource_PROMS_InsertCanceledList.SelectParameters.Clear();
      SqlDataSource_PROMS_InsertCanceledList.SelectParameters.Add("PROMS_FollowUp_Id", TypeCode.String, Request.QueryString["PROMS_FollowUp_Id"]);

      SqlDataSource_PROMS_FollowUp_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_FollowUp_Form.InsertCommand="INSERT INTO [InfoQuest_Form_PROMS_FollowUp] (PROMS_Questionnaire_Id ,PROMS_FollowUp_CompletionDate ,PROMS_FollowUp_Q1 ,PROMS_FollowUp_Q2 ,PROMS_FollowUp_Q3 ,PROMS_FollowUp_Q4 ,PROMS_FollowUp_Q5 ,PROMS_FollowUp_Q6 ,PROMS_FollowUp_Q7 ,PROMS_FollowUp_Q8 ,PROMS_FollowUp_Q9 ,PROMS_FollowUp_Q10 ,PROMS_FollowUp_Q11 ,PROMS_FollowUp_Q12 ,PROMS_FollowUp_Score ,PROMS_FollowUp_Cancelled ,PROMS_FollowUp_Cancelled_List ,PROMS_FollowUp_Notes ,PROMS_FollowUp_CreatedDate ,PROMS_FollowUp_CreatedBy ,PROMS_FollowUp_ModifiedDate ,PROMS_FollowUp_ModifiedBy ,PROMS_FollowUp_History) VALUES (@PROMS_Questionnaire_Id ,@PROMS_FollowUp_CompletionDate ,@PROMS_FollowUp_Q1 ,@PROMS_FollowUp_Q2 ,@PROMS_FollowUp_Q3 ,@PROMS_FollowUp_Q4 ,@PROMS_FollowUp_Q5 ,@PROMS_FollowUp_Q6 ,@PROMS_FollowUp_Q7 ,@PROMS_FollowUp_Q8 ,@PROMS_FollowUp_Q9 ,@PROMS_FollowUp_Q10 ,@PROMS_FollowUp_Q11 ,@PROMS_FollowUp_Q12 ,@PROMS_FollowUp_Score ,@PROMS_FollowUp_Cancelled ,@PROMS_FollowUp_Cancelled_List ,@PROMS_FollowUp_Notes ,@PROMS_FollowUp_CreatedDate ,@PROMS_FollowUp_CreatedBy ,@PROMS_FollowUp_ModifiedDate ,@PROMS_FollowUp_ModifiedBy ,@PROMS_FollowUp_History)";
      SqlDataSource_PROMS_FollowUp_Form.SelectCommand="SELECT * FROM [InfoQuest_Form_PROMS_FollowUp] WHERE ([PROMS_FollowUp_Id] = @PROMS_FollowUp_Id)";
      SqlDataSource_PROMS_FollowUp_Form.UpdateCommand="UPDATE [InfoQuest_Form_PROMS_FollowUp] SET PROMS_FollowUp_Q1 = @PROMS_FollowUp_Q1 ,PROMS_FollowUp_Q2 = @PROMS_FollowUp_Q2 ,PROMS_FollowUp_Q3 = @PROMS_FollowUp_Q3 ,PROMS_FollowUp_Q4 = @PROMS_FollowUp_Q4 ,PROMS_FollowUp_Q5 = @PROMS_FollowUp_Q5 ,PROMS_FollowUp_Q6 = @PROMS_FollowUp_Q6 ,PROMS_FollowUp_Q7 = @PROMS_FollowUp_Q7 ,PROMS_FollowUp_Q8 = @PROMS_FollowUp_Q8 ,PROMS_FollowUp_Q9 = @PROMS_FollowUp_Q9 ,PROMS_FollowUp_Q10 = @PROMS_FollowUp_Q10 ,PROMS_FollowUp_Q11 = @PROMS_FollowUp_Q11 ,PROMS_FollowUp_Q12 = @PROMS_FollowUp_Q12 ,PROMS_FollowUp_Score = @PROMS_FollowUp_Score ,PROMS_FollowUp_Cancelled = @PROMS_FollowUp_Cancelled ,PROMS_FollowUp_Cancelled_List = @PROMS_FollowUp_Cancelled_List ,PROMS_FollowUp_Notes = @PROMS_FollowUp_Notes ,PROMS_FollowUp_ModifiedDate = @PROMS_FollowUp_ModifiedDate ,PROMS_FollowUp_ModifiedBy = @PROMS_FollowUp_ModifiedBy ,PROMS_FollowUp_History = @PROMS_FollowUp_History WHERE [PROMS_FollowUp_Id] = @PROMS_FollowUp_Id";
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Clear();
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_Questionnaire_Id", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_CompletionDate", TypeCode.DateTime, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q1", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q2", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q3", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q4", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q5", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q6", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q7", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q8", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q9", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q10", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q11", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Q12", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Score", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_Score"].ConvertEmptyStringToNull = true;
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Cancelled", TypeCode.Boolean, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Cancelled_List", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_Notes", TypeCode.String, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_CreatedBy", TypeCode.String, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters.Add("PROMS_FollowUp_History", TypeCode.String, "");
      SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_PROMS_FollowUp_Form.SelectParameters.Clear();
      SqlDataSource_PROMS_FollowUp_Form.SelectParameters.Add("PROMS_FollowUp_Id", TypeCode.Int32, Request.QueryString["PROMS_FollowUp_Id"]);
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Clear();
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q1", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q2", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q3", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q4", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q5", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q6", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q7", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q8", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q9", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q10", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q11", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Q12", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Score", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters["PROMS_FollowUp_Score"].ConvertEmptyStringToNull = true;
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Cancelled", TypeCode.Boolean, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Cancelled_List", TypeCode.Int32, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Notes", TypeCode.String, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_History", TypeCode.String, "");
      SqlDataSource_PROMS_FollowUp_Form.UpdateParameters.Add("PROMS_FollowUp_Id", TypeCode.Int32, "");
      
      SqlDataSource_PROMS_Questionnaire.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PROMS_Questionnaire.SelectCommand = "spForm_Get_PROMS_Questionnaire";
      SqlDataSource_PROMS_Questionnaire.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PROMS_Questionnaire.CancelSelectOnNullParameter = false;
      SqlDataSource_PROMS_Questionnaire.SelectParameters.Clear();
      SqlDataSource_PROMS_Questionnaire.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_PROMS_Questionnaire.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_PROMS_Questionnaire.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_PROMS_PatientVisitNumber"]);
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
        if (Request.QueryString["s_PROMS_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_PROMS_PatientVisitNumber"];
        }
      }
    }

    private void PatientDataPI()
    {
      DataTable DataTable_PatientDataPI;
      using (DataTable_PatientDataPI = new DataTable())
      {
        DataTable_PatientDataPI.Locale = CultureInfo.CurrentCulture;
        //DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_PROMS_PatientVisitNumber"]).Copy();
        DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_PROMS_PatientVisitNumber"]).Copy();

        if (DataTable_PatientDataPI.Columns.Count == 1)
        {
          Session["PROMSPIId"] = "";
          string SQLStringPatientInfo = "SELECT PROMS_PI_Id FROM InfoQuest_Form_PROMS_PatientInformation WHERE Facility_Id = @FacilityId AND PROMS_PI_PatientVisitNumber = @PROMSPIPatientVisitNumber";
          using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
          {
            SqlCommand_PatientInfo.Parameters.AddWithValue("@FacilityId", Request.QueryString["s_Facility_Id"]);
            SqlCommand_PatientInfo.Parameters.AddWithValue("@PROMSPIPatientVisitNumber", Request.QueryString["s_PROMS_PatientVisitNumber"]);
            DataTable DataTable_PatientInfo;
            using (DataTable_PatientInfo = new DataTable())
            {
              DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
              DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
              if (DataTable_PatientInfo.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                {
                  Session["PROMSPIId"] = DataRow_Row1["PROMS_PI_Id"];
                }
              }
            }
          }

          if (string.IsNullOrEmpty(Session["PROMSPIId"].ToString()))
          {
            Session["Error"] = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["Error"] = DataRow_Row["Error"];
            }

            Label_InvalidSearch.Text = Session["Error"].ToString();
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            TableForm0.Visible = false;
            TableForm1.Visible = false;
            TableList.Visible = false;
            Session["Error"] = "";
          }
          else
          {
            Session["Error"] = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["Error"] = DataRow_Row["Error"];
            }

            Label_InvalidSearch.Text = Session["Error"].ToString() + Convert.ToString("<br />No Patient related data could be updated but you can continue capturing new form(s) and updating and viewing previous form(s)", CultureInfo.CurrentCulture);
            Session["Error"] = "";
          }

          Session["PROMSPIId"] = "";
        }
        else if (DataTable_PatientDataPI.Columns.Count != 1)
        {
          PatientDataPI_PatientInformation(DataTable_PatientDataPI);
        }
      }
    }

    private void PatientDataPI_PatientInformation(DataTable dataTable_PatientDataPI)
    {
      if (dataTable_PatientDataPI != null)
      {
        if (dataTable_PatientDataPI.Rows.Count == 0)
        {
          Label_InvalidSearch.Text = Convert.ToString("Patient Visit Number " + Request.QueryString["s_PROMS_PatientVisitNumber"] + " does not Exist", CultureInfo.CurrentCulture);
          TablePatientInfo.Visible = false;
          TableForm.Visible = false;
          TableForm0.Visible = false;
          TableForm1.Visible = false;
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
            string ContactNumber = DataRow_Row["ContactNumber"].ToString();

            string NameSurnamePI = NameSurname;
            NameSurnamePI = NameSurnamePI.Replace("'", "");
            NameSurname = NameSurnamePI;
            NameSurnamePI = "";

            string PROMSPIId = "";
            string SQLStringPatientInfo = "SELECT PROMS_PI_Id FROM InfoQuest_Form_PROMS_PatientInformation WHERE Facility_Id = @FacilityId AND PROMS_PI_PatientVisitNumber = @PROMSPIPatientVisitNumber";
            using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
            {
              SqlCommand_PatientInfo.Parameters.AddWithValue("@FacilityId", Request.QueryString["s_Facility_Id"]);
              SqlCommand_PatientInfo.Parameters.AddWithValue("@PROMSPIPatientVisitNumber", Request.QueryString["s_PROMS_PatientVisitNumber"]);
              DataTable DataTable_PatientInfo;
              using (DataTable_PatientInfo = new DataTable())
              {
                DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
                DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
                if (DataTable_PatientInfo.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                  {
                    PROMSPIId = DataRow_Row1["PROMS_PI_Id"].ToString();
                  }
                }
              }
            }

            if (string.IsNullOrEmpty(PROMSPIId))
            {
              string SQLStringInsertPROMSPI = "INSERT INTO InfoQuest_Form_PROMS_PatientInformation ( Facility_Id , PROMS_PI_PatientVisitNumber , PROMS_PI_PatientName , PROMS_PI_PatientAge , PROMS_PI_PatientDateOfAdmission , PROMS_PI_PatientDateofDischarge , PROMS_PI_PatientContactNumber , PROMS_PI_Archived ) VALUES  ( @Facility_Id , @PROMS_PI_PatientVisitNumber , @PROMS_PI_PatientName , @PROMS_PI_PatientAge , @PROMS_PI_PatientDateOfAdmission , @PROMS_PI_PatientDateofDischarge , @PROMS_PI_PatientContactNumber , @PROMS_PI_Archived )";
              using (SqlCommand SqlCommand_InsertPROMSPI = new SqlCommand(SQLStringInsertPROMSPI))
              {
                SqlCommand_InsertPROMSPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_InsertPROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientVisitNumber", VisitNumber);
                SqlCommand_InsertPROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientName", NameSurname);
                SqlCommand_InsertPROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientAge", Age);
                SqlCommand_InsertPROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientDateOfAdmission", AdmissionDate);
                SqlCommand_InsertPROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientDateofDischarge", DischargeDate);
                SqlCommand_InsertPROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientContactNumber", ContactNumber);
                SqlCommand_InsertPROMSPI.Parameters.AddWithValue("@PROMS_PI_Archived", 0);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertPROMSPI);
              }
            }
            else
            {
              string SQLStringUpdatePROMSPI = "UPDATE InfoQuest_Form_PROMS_PatientInformation SET PROMS_PI_PatientName = @PROMS_PI_PatientName , PROMS_PI_PatientAge = @PROMS_PI_PatientAge , PROMS_PI_PatientDateOfAdmission = @PROMS_PI_PatientDateOfAdmission , PROMS_PI_PatientDateofDischarge = @PROMS_PI_PatientDateofDischarge , PROMS_PI_PatientContactNumber = @PROMS_PI_PatientContactNumber WHERE Facility_Id = @Facility_Id AND PROMS_PI_PatientVisitNumber = @PROMS_PI_PatientVisitNumber ";
              using (SqlCommand SqlCommand_UpdatePROMSPI = new SqlCommand(SQLStringUpdatePROMSPI))
              {
                SqlCommand_UpdatePROMSPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_UpdatePROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientVisitNumber", VisitNumber);
                SqlCommand_UpdatePROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientName", NameSurname);
                SqlCommand_UpdatePROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientAge", Age);
                SqlCommand_UpdatePROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientDateOfAdmission", AdmissionDate);
                SqlCommand_UpdatePROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientDateofDischarge", DischargeDate);
                SqlCommand_UpdatePROMSPI.Parameters.AddWithValue("@PROMS_PI_PatientContactNumber", ContactNumber);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdatePROMSPI);
              }


              string SQLStringUpdateQuestionnaire = "UPDATE InfoQuest_Form_PROMS_Questionnaire SET PROMS_Questionnaire_AdmissionDate = @PROMS_Questionnaire_AdmissionDate WHERE Facility_Id = @Facility_Id AND PROMS_Questionnaire_PatientVisitNumber = @PROMS_Questionnaire_PatientVisitNumber ";
              using (SqlCommand SqlCommand_UpdateQuestionnaire = new SqlCommand(SQLStringUpdateQuestionnaire))
              {
                SqlCommand_UpdateQuestionnaire.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_UpdateQuestionnaire.Parameters.AddWithValue("@PROMS_Questionnaire_PatientVisitNumber", VisitNumber);
                SqlCommand_UpdateQuestionnaire.Parameters.AddWithValue("@PROMS_Questionnaire_AdmissionDate", AdmissionDate);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateQuestionnaire);
              }
            }
            PROMSPIId = "";
          }
        }
      }
    }


    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('30')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '118'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '119'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '120'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '121'");

            if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
            {
              Session["PROMSFollowUpId"] = "";
              string SQLStringPROMSFollowUpId = "SELECT PROMS_FollowUp_Id FROM InfoQuest_Form_PROMS_FollowUp WHERE PROMS_Questionnaire_Id = @PROMS_Questionnaire_Id";
              using (SqlCommand SqlCommand_PROMSFollowUpId = new SqlCommand(SQLStringPROMSFollowUpId))
              {
                SqlCommand_PROMSFollowUpId.Parameters.AddWithValue("@PROMS_Questionnaire_Id", Request.QueryString["PROMS_Questionnaire_Id"]);
                DataTable DataTable_PROMSFollowUpId;
                using (DataTable_PROMSFollowUpId = new DataTable())
                {
                  DataTable_PROMSFollowUpId.Locale = CultureInfo.CurrentCulture;
                  DataTable_PROMSFollowUpId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSFollowUpId).Copy();
                  if (DataTable_PROMSFollowUpId.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_PROMSFollowUpId.Rows)
                    {
                      Session["PROMSFollowUpId"] = DataRow_Row["PROMS_FollowUp_Id"];
                    }
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              SetFormVisibility_Admin();
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Session["Security"] = "0";
              SetFormVisibility_View();
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              SetFormVisibility_Other();
            }

            Session["Security"] = "1";
          }
        }
      }
    }

    private void SetFormVisibility_Admin()
    {
      if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
      {
        if (Request.QueryString["ViewModeQ"] == "1")
        {
          FormView_PROMS_Questionnaire_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_PROMS_Questionnaire_Form.ChangeMode(FormViewMode.ReadOnly);
        }


        if (Request.QueryString["FollowUp"] == "0")
        {
          TableForm1.Visible = false;
        }
        else if (Request.QueryString["FollowUp"] == "1")
        {
          if (Request.QueryString["PROMS_FollowUp_Id"] != null)
          {
            if (Request.QueryString["ViewModeF"] == "1")
            {
              FormView_PROMS_FollowUp_Form.ChangeMode(FormViewMode.Edit);
            }
            else
            {
              FormView_PROMS_FollowUp_Form.ChangeMode(FormViewMode.ReadOnly);
            }
          }
          else
          {
            if (string.IsNullOrEmpty(Session["PROMSFollowUpId"].ToString()))
            {
              FormView_PROMS_FollowUp_Form.ChangeMode(FormViewMode.Insert);
            }
            else
            {
              FormView_PROMS_FollowUp_Form.ChangeMode(FormViewMode.ReadOnly);
            }
          }
        }
        else
        {
          TableForm1.Visible = false;
        }
      }
      else
      {
        FormView_PROMS_Questionnaire_Form.ChangeMode(FormViewMode.Insert);
        TableForm1.Visible = false;
      }
    }

    private void SetFormVisibility_View()
    {
      if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
      {
        FormView_PROMS_Questionnaire_Form.ChangeMode(FormViewMode.ReadOnly);

        if (Request.QueryString["FollowUp"] == "1")
        {
          FormView_PROMS_FollowUp_Form.ChangeMode(FormViewMode.ReadOnly);
        }
        else
        {
          TableForm1.Visible = false;
        }
      }
      else
      {
        FormView_PROMS_Questionnaire_Form.ChangeMode(FormViewMode.ReadOnly);
        TableForm1.Visible = false;
      }
    }

    private void SetFormVisibility_Other()
    {
      if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
      {
        FormView_PROMS_Questionnaire_Form.ChangeMode(FormViewMode.ReadOnly);

        if (Request.QueryString["FollowUp"] == "1")
        {
          FormView_PROMS_FollowUp_Form.ChangeMode(FormViewMode.ReadOnly);
        }
        else
        {
          TableForm1.Visible = false;
        }
      }
      else
      {
        FormView_PROMS_Questionnaire_Form.ChangeMode(FormViewMode.ReadOnly);
        TableForm1.Visible = false;
      }
    }


    private void TablePatientInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["PROMSPIPatientVisitNumber"] = "";
      Session["PROMSPIPatientName"] = "";
      Session["PROMSPIPatientAge"] = "";
      Session["PROMSPIPatientDateOfAdmission"] = "";
      Session["PROMSPIPatientDateofDischarge"] = "";
      Session["PROMSPIPatientContactNumber"] = "";

      string SQLStringPatientInfo = "SELECT DISTINCT Facility_FacilityDisplayName , PROMS_PI_PatientVisitNumber , PROMS_PI_PatientName , PROMS_PI_PatientAge , PROMS_PI_PatientDateOfAdmission , PROMS_PI_PatientDateofDischarge , PROMS_PI_PatientContactNumber FROM vForm_PROMS_PatientInformation WHERE Facility_Id = @s_Facility_Id AND PROMS_PI_PatientVisitNumber = @s_PROMS_PatientVisitNumber";
      using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
      {
        SqlCommand_PatientInfo.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PatientInfo.Parameters.AddWithValue("@s_PROMS_PatientVisitNumber", Request.QueryString["s_PROMS_PatientVisitNumber"]);
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
              Session["PROMSPIPatientVisitNumber"] = DataRow_Row["PROMS_PI_PatientVisitNumber"];
              Session["PROMSPIPatientName"] = DataRow_Row["PROMS_PI_PatientName"];
              Session["PROMSPIPatientAge"] = DataRow_Row["PROMS_PI_PatientAge"];
              Session["PROMSPIPatientDateOfAdmission"] = DataRow_Row["PROMS_PI_PatientDateOfAdmission"];
              Session["PROMSPIPatientDateofDischarge"] = DataRow_Row["PROMS_PI_PatientDateofDischarge"];
              Session["PROMSPIPatientContactNumber"] = DataRow_Row["PROMS_PI_PatientContactNumber"];
            }
          }
        }
      }

      Label_PIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_PIVisitNumber.Text = Session["PROMSPIPatientVisitNumber"].ToString();
      Label_PIName.Text = Session["PROMSPIPatientName"].ToString();
      Label_PIAge.Text = Session["PROMSPIPatientAge"].ToString();
      Label_PIDateAdmission.Text = Session["PROMSPIPatientDateOfAdmission"].ToString();
      Label_PIDateDischarge.Text = Session["PROMSPIPatientDateofDischarge"].ToString();
      Label_PIContactNumber.Text = Session["PROMSPIPatientContactNumber"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["PROMSPIPatientVisitNumber"] = "";
      Session["PROMSPIPatientName"] = "";
      Session["PROMSPIPatientAge"] = "";
      Session["PROMSPIPatientDateOfAdmission"] = "";
      Session["PROMSPIPatientDateofDischarge"] = "";
      Session["PROMSPIPatientContactNumber"] = "";
    }

    private void TableForm0Visible()
    {
      if (FormView_PROMS_Questionnaire_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertQuestionnaireList")).Attributes.Add("OnChange", "Validation_Form0();ShowHide_Form0();");

        for (int a = 1; a <= TotalQuestions; a++)
        {
          ((RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertQ" + a + "")).Attributes.Add("OnClick", "Validation_Form0();Calculation_Form0();");
        }

        ((RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertContactPatient")).Attributes.Add("OnClick", "Validation_Form0();ShowHide_Form0();");
        ((TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_InsertContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form0();");
        ((TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_InsertContactNumber")).Attributes.Add("OnInput", "Validation_Form0();");
        ((DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertLanguageList")).Attributes.Add("OnChange", "Validation_Form0();ShowHide_Form0();");

        RadioButtonList RadioButtonList_InsertContactPatient = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertContactPatient");
        RadioButtonList_InsertContactPatient.SelectedValue = "True";

        Session["ListItemsName"] = "";
        string SQLStringListItems = "SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 105";
        using (SqlCommand SqlCommand_ListItems = new SqlCommand(SQLStringListItems))
        {
          DataTable DataTable_ListItems;
          using (DataTable_ListItems = new DataTable())
          {
            DataTable_ListItems.Locale = CultureInfo.CurrentCulture;
            DataTable_ListItems = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItems).Copy();
            if (DataTable_ListItems.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_ListItems.Rows)
              {
                Session["ListItemsName"] = DataRow_Row["ListItem_Name"];
              }
            }
            else
            {
              Session["ListItemsName"] = "";
            }
          }
        }

        DropDownList DropDownList_InsertLanguageList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertLanguageList");
        DropDownList_InsertLanguageList.SelectedValue = Session["ListItemsName"].ToString();

        Session["ListItemsName"] = "";

        string AdmissionDate = Label_PIDateAdmission.Text;
        Label Label_InsertAdmissionDate = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertAdmissionDate");
        Label_InsertAdmissionDate.Text = AdmissionDate;

        string ContactNumber = Label_PIContactNumber.Text;
        TextBox TextBox_InsertContactNumber = (TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_InsertContactNumber");
        TextBox_InsertContactNumber.Text = ContactNumber;
      }

      if (FormView_PROMS_Questionnaire_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_EditQuestionnaireList")).Attributes.Add("OnChange", "Validation_Form0();ShowHide_Form0();");

        for (int a = 1; a <= TotalQuestions; a++)
        {
          ((RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditQ" + a + "")).Attributes.Add("OnClick", "Validation_Form0();Calculation_Form0();");
        }

        ((RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditContactPatient")).Attributes.Add("OnClick", "Validation_Form0();ShowHide_Form0();");
        ((TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_EditContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form0();");
        ((TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_EditContactNumber")).Attributes.Add("OnInput", "Validation_Form0();");
        ((DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_EditLanguageList")).Attributes.Add("OnChange", "Validation_Form0();ShowHide_Form0();");
      }
    }

    private void TableForm1Visible()
    {
      if (FormView_PROMS_FollowUp_Form.CurrentMode == FormViewMode.Insert)
      {
        ((CheckBox)FormView_PROMS_FollowUp_Form.FindControl("CheckBox_InsertCancelled")).Attributes.Add("OnClick", "Validation_Form1();Calculation_Form1();ShowHide_Form1();");
        ((DropDownList)FormView_PROMS_FollowUp_Form.FindControl("DropDownList_InsertCancelledList")).Attributes.Add("OnChange", "Validation_Form1();ShowHide_Form1();");

        for (int a = 1; a <= TotalQuestions; a++)
        {
          ((RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ" + a + "")).Attributes.Add("OnClick", "Validation_Form1();Calculation_Form1();");
        }
      }

      if (FormView_PROMS_FollowUp_Form.CurrentMode == FormViewMode.Edit)
      {
        ((CheckBox)FormView_PROMS_FollowUp_Form.FindControl("CheckBox_EditCancelled")).Attributes.Add("OnClick", "Validation_Form1();Calculation_Form1();ShowHide_Form1();");
        ((DropDownList)FormView_PROMS_FollowUp_Form.FindControl("DropDownList_EditCancelledList")).Attributes.Add("OnChange", "Validation_Form1();ShowHide_Form1();");

        for (int a = 1; a <= TotalQuestions; a++)
        {
          ((RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ" + a + "")).Attributes.Add("OnClick", "Validation_Form1();Calculation_Form1();");

          RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ" + a + "");
          RadioButtonList_EditQ.Items.RemoveAt(5);
        }
      }
    }


    //--START-- --Search--//
    protected void Button_GoToNext_Click(object sender, EventArgs e)
    {
      RedirectToNext();
    }

    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_PROMS.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_PROMS_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "", false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_PROMS.aspx";
      Response.Redirect(FinalURL, false);
    }

    private void RedirectToList()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_PROMSPatientVisitNumber"];
      string SearchField3 = Request.QueryString["Search_PROMSPatientName"];
      string SearchField4 = Request.QueryString["Search_PROMSReportNumber"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null && SearchField4 == null)
      {
        FinalURL = "Form_PROMS_List.aspx";
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_PROMS_PatientVisitNumber=" + Request.QueryString["Search_PROMSPatientVisitNumber"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_PROMS_PatientName=" + Request.QueryString["Search_PROMSPatientName"] + "&";
        }

        if (SearchField4 == null)
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_PROMS_ReportNumber=" + Request.QueryString["Search_PROMSReportNumber"] + "&";
        }

        string SearchURL = "Form_PROMS_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }

    private void RedirectToNext()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["SearchN_FacilityId"];
      string SearchField2 = Request.QueryString["SearchN_PROMSPatientVisitNumber"];
      string SearchField3 = Request.QueryString["SearchN_PROMSReportNumber"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null)
      {
        FinalURL = "Form_PROMS_Next.aspx";
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_Facility_Id=" + Request.QueryString["SearchN_FacilityId"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_PROMS_PatientVisitNumber=" + Request.QueryString["SearchN_PROMSPatientVisitNumber"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_PROMS_ReportNumber=" + Request.QueryString["SearchN_PROMSReportNumber"] + "&";
        }

        string SearchURL = "Form_PROMS_Next.aspx?" + SearchField1 + SearchField2 + SearchField3;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --TableList--//
    protected void SqlDataSource_PROMS_Questionnaire_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_PROMS_Questionnaire.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_PROMS_Questionnaire.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_PROMS_Questionnaire.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_PROMS_Questionnaire.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_PROMS_Questionnaire_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_PROMS_Questionnaire.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_PROMS_Questionnaire.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_PROMS_Questionnaire.PageSize > 20 && GridView_PROMS_Questionnaire.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_PROMS_Questionnaire.PageSize > 50 && GridView_PROMS_Questionnaire.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_PROMS_Questionnaire_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_PROMS_Questionnaire.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_PROMS_Questionnaire.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_PROMS_Questionnaire.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_PROMS_Questionnaire_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect("Form_PROMS.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_PROMS_PatientVisitNumber=" + Request.QueryString["s_PROMS_PatientVisitNumber"] + "", false);
    }
    //--START-- --TableList--//


    //--START-- --TableForm--//
    //--START-- --Table_PROMS_Questionnaire--//
    protected void FormView_PROMS_Questionnaire_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        DropDownList DropDownList_InsertQuestionnaireList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertQuestionnaireList");

        Session["PROMSQuestionnaireId"] = "";
        Session["PROMSQuestionnaireQuestionnaireName"] = "";
        Session["PROMSQuestionnaireCreatedDate"] = "";
        string SQLStringPROMSQuestionnaireId = "SELECT PROMS_Questionnaire_Id , PROMS_Questionnaire_Questionnaire_Name , PROMS_Questionnaire_CreatedDate FROM vForm_PROMS WHERE Facility_Id = @Facility_Id AND PROMS_Questionnaire_PatientVisitNumber = @PROMS_Questionnaire_PatientVisitNumber AND PROMS_Questionnaire_Questionnaire_List = @PROMS_Questionnaire_Questionnaire_List";
        using (SqlCommand SqlCommand_PROMSQuestionnaireId = new SqlCommand(SQLStringPROMSQuestionnaireId))
        {
          SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@PROMS_Questionnaire_PatientVisitNumber", Request.QueryString["s_PROMS_PatientVisitNumber"]);
          SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@PROMS_Questionnaire_Questionnaire_List", DropDownList_InsertQuestionnaireList.SelectedValue.ToString());
          DataTable DataTable_PROMSQuestionnaireId;
          using (DataTable_PROMSQuestionnaireId = new DataTable())
          {
            DataTable_PROMSQuestionnaireId.Locale = CultureInfo.CurrentCulture;
            DataTable_PROMSQuestionnaireId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSQuestionnaireId).Copy();
            if (DataTable_PROMSQuestionnaireId.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PROMSQuestionnaireId.Rows)
              {
                Session["PROMSQuestionnaireId"] = DataRow_Row["PROMS_Questionnaire_Id"];
                Session["PROMSQuestionnaireQuestionnaireName"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_Name"];
                Session["PROMSQuestionnaireCreatedDate"] = DataRow_Row["PROMS_Questionnaire_CreatedDate"];
              }
            }
            else
            {
              Session["PROMSQuestionnaireId"] = "";
              Session["PROMSQuestionnaireQuestionnaireName"] = "";
              Session["PROMSQuestionnaireCreatedDate"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["PROMSQuestionnaireId"].ToString()))
        {
          e.Cancel = true;
          Page.MaintainScrollPositionOnPostBack = false;

          string Label_InsertConcurrencyInsertMessage = "" +
            "Questionnaire could not be added<br/>" +
            "A Questionnaire for " + Session["PROMSQuestionnaireQuestionnaireName"].ToString() + " has already been captured for this Visit Number at " + Session["PROMSQuestionnaireCreatedDate"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "&PROMS_Questionnaire_Id=" + Session["PROMSQuestionnaireId"].ToString() + "&ViewModeQ=0&FollowUp=0#LinkPatientInfo' style='color:#b0262e;'>Reload Page</a> to view Questionnaire<br/><br/>";

          ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertInvalidFormMessage")).Text = "";
          ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = Convert.ToString(Label_InsertConcurrencyInsertMessage, CultureInfo.CurrentCulture);
        }
        else
        {
          string Label_InsertInvalidFormMessage = InsertValidationQuestionnaire();

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
            ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
            ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            Session["PROMS_Questionnaire_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Request.QueryString["s_Facility_Id"], "30");

            SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_EmailSend"].DefaultValue = "false";
            SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_ReportNumber"].DefaultValue = Session["PROMS_Questionnaire_ReportNumber"].ToString();
            SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_CreatedDate"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_History"].DefaultValue = "";
            SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_IsActive"].DefaultValue = "true";

            int QuestionnaireScore = 0;
            for (int a = 1; a <= TotalQuestions; a++)
            {
              int SelectedIndex = Convert.ToInt32(((RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertQ" + a + "")).SelectedIndex);
              int SelectedValue = 4 - SelectedIndex;
              QuestionnaireScore = QuestionnaireScore + SelectedValue;
            }

            SqlDataSource_PROMS_Questionnaire_Form.InsertParameters["PROMS_Questionnaire_Score"].DefaultValue = QuestionnaireScore.ToString(CultureInfo.CurrentCulture);

            Session["PROMS_Questionnaire_ReportNumber"] = "";
          }
        }

        Session["PROMSQuestionnaireId"] = "";
        Session["PROMSQuestionnaireQuestionnaireName"] = "";
        Session["PROMSQuestionnaireCreatedDate"] = "";
      }
    }

    protected string InsertValidationQuestionnaire()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_InsertQuestionnaireList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertQuestionnaireList");
      RadioButtonList RadioButtonList_InsertContactPatient = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertContactPatient");
      TextBox TextBox_InsertContactNumber = (TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_InsertContactNumber");
      DropDownList DropDownList_InsertLanguageList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertLanguageList");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertQuestionnaireList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        for (int a = 1; a <= TotalQuestions; a++)
        {
          RadioButtonList RadioButtonList_InsertQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertQ" + a + "");

          if (string.IsNullOrEmpty(RadioButtonList_InsertQ.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(RadioButtonList_InsertContactPatient.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (RadioButtonList_InsertContactPatient.SelectedValue == "True")
        {
          if (string.IsNullOrEmpty(TextBox_InsertContactNumber.Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(DropDownList_InsertLanguageList.SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {

      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PROMS_Questionnaire_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["PROMS_Questionnaire_Id"] = e.Command.Parameters["@PROMS_Questionnaire_Id"].Value;
        Session["PROMS_Questionnaire_ReportNumber"] = e.Command.Parameters["@PROMS_Questionnaire_ReportNumber"].Value;
        Response.Redirect("InfoQuest_ReportNumber.aspx?ReportPage=Form_PROMS&ReportNumber=" + Session["PROMS_Questionnaire_ReportNumber"].ToString() + "", false);
      }
    }


    protected void FormView_PROMS_Questionnaire_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDPROMSQuestionnaireModifiedDate"] = e.OldValues["PROMS_Questionnaire_ModifiedDate"];
        object OLDPROMSQuestionnaireModifiedDate = Session["OLDPROMSQuestionnaireModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDPROMSQuestionnaireModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_ComparePROMS = (DataView)SqlDataSource_PROMS_Questionnaire_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ComparePROMS = DataView_ComparePROMS[0];
        Session["DBPROMSQuestionnaireModifiedDate"] = Convert.ToString(DataRowView_ComparePROMS["PROMS_Questionnaire_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBPROMSQuestionnaireModifiedBy"] = Convert.ToString(DataRowView_ComparePROMS["PROMS_Questionnaire_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBPROMSQuestionnaireModifiedDate = Session["DBPROMSQuestionnaireModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBPROMSQuestionnaireModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Visible = true;

          ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBPROMSQuestionnaireModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          DropDownList DropDownList_EditQuestionnaireList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_EditQuestionnaireList");

          Session["PROMSQuestionnaireId"] = "";
          Session["PROMSQuestionnaireQuestionnaireName"] = "";
          Session["PROMSQuestionnaireCreatedDate"] = "";
          string SQLStringPROMSQuestionnaireId = "SELECT PROMS_Questionnaire_Id , PROMS_Questionnaire_Questionnaire_Name , PROMS_Questionnaire_CreatedDate FROM vForm_PROMS WHERE Facility_Id = @Facility_Id AND PROMS_Questionnaire_PatientVisitNumber = @PROMS_Questionnaire_PatientVisitNumber AND PROMS_Questionnaire_Questionnaire_List = @PROMS_Questionnaire_Questionnaire_List";
          using (SqlCommand SqlCommand_PROMSQuestionnaireId = new SqlCommand(SQLStringPROMSQuestionnaireId))
          {
            SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
            SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@PROMS_Questionnaire_PatientVisitNumber", Request.QueryString["s_PROMS_PatientVisitNumber"]);
            SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@PROMS_Questionnaire_Questionnaire_List", DropDownList_EditQuestionnaireList.SelectedValue.ToString());
            DataTable DataTable_PROMSQuestionnaireId;
            using (DataTable_PROMSQuestionnaireId = new DataTable())
            {
              DataTable_PROMSQuestionnaireId.Locale = CultureInfo.CurrentCulture;
              DataTable_PROMSQuestionnaireId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSQuestionnaireId).Copy();
              if (DataTable_PROMSQuestionnaireId.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_PROMSQuestionnaireId.Rows)
                {
                  Session["PROMSQuestionnaireId"] = DataRow_Row["PROMS_Questionnaire_Id"];
                  Session["PROMSQuestionnaireQuestionnaireName"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_Name"];
                  Session["PROMSQuestionnaireCreatedDate"] = DataRow_Row["PROMS_Questionnaire_CreatedDate"];
                }
              }
            }
          }

          if ((!string.IsNullOrEmpty(Session["PROMSQuestionnaireId"].ToString())) && (Session["PROMSQuestionnaireId"].ToString() != Request.QueryString["PROMS_Questionnaire_Id"]))
          {
            e.Cancel = true;

            Page.MaintainScrollPositionOnPostBack = false;

            string Label_EditConcurrencyUpdateMessage = "" +
              "Questionnaire could not be updated<br/>" +
              "A Questionnaire for " + Session["PROMSQuestionnaireQuestionnaireName"].ToString() + " has already been captured for this Visit Number at " + Session["PROMSQuestionnaireCreatedDate"].ToString() + "<br/>" +
              "<a href='Form_PROMS.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_PROMS_PatientVisitNumber=" + Request.QueryString["s_PROMS_PatientVisitNumber"] + "&PROMS_Questionnaire_Id=" + Session["PROMSQuestionnaireId"].ToString() + "&ViewModeQ=0&FollowUp=0#LinkPatientInfo' style='color:#b0262e;'>Reload Page</a> to view Questionnaire<br/><br/>";

            ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
            ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Convert.ToString(Label_EditConcurrencyUpdateMessage, CultureInfo.CurrentCulture);

            for (int a = 1; a <= TotalQuestions; a++)
            {
              Label Label_EditQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditHeadingQ" + a + "");
              Label_EditQ.Text = "";

              RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditQ" + a + "");
              RadioButtonList_EditQ.Items.Clear();
            }
          }
          else
          {
            string Label_EditInvalidFormMessage = EditValidationQuestionnaire();

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
              ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
              ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
            }
            else if (e.Cancel == false)
            {
              e.NewValues["PROMS_Questionnaire_ModifiedDate"] = DateTime.Now.ToString();
              e.NewValues["PROMS_Questionnaire_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];
              e.NewValues["PROMS_Questionnaire_EmailSend"] = "false";

              Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_PROMS_Questionnaire", "PROMS_Questionnaire_Id = " + Request.QueryString["PROMS_Questionnaire_Id"]);

              DataView DataView_PROMS = (DataView)SqlDataSource_PROMS_Questionnaire_Form.Select(DataSourceSelectArguments.Empty);
              DataRowView DataRowView_PROMS = DataView_PROMS[0];
              Session["PROMSQuestionnaireHistory"] = Convert.ToString(DataRowView_PROMS["PROMS_Questionnaire_History"], CultureInfo.CurrentCulture);

              Session["PROMSQuestionnaireHistory"] = Session["History"].ToString() + Session["PROMSQuestionnaireHistory"].ToString();
              e.NewValues["PROMS_Questionnaire_History"] = Session["PROMSQuestionnaireHistory"].ToString();

              Session["PROMSQuestionnaireHistory"] = "";
              Session["History"] = "";


              int QuestionnaireScore = 0;
              for (int a = 1; a <= TotalQuestions; a++)
              {
                int SelectedIndex = Convert.ToInt32(((RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditQ" + a + "")).SelectedIndex);
                int SelectedValue = 4 - SelectedIndex;
                QuestionnaireScore = QuestionnaireScore + SelectedValue;
              }

              e.NewValues["PROMS_Questionnaire_Score"] = QuestionnaireScore.ToString(CultureInfo.CurrentCulture);
            }
          }
        }

        Session["OLDPROMSQuestionnaireModifiedDate"] = "";
        Session["DBPROMSQuestionnaireModifiedDate"] = "";
        Session["DBPROMSQuestionnaireModifiedBy"] = "";
      }
    }

    protected string EditValidationQuestionnaire()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditQuestionnaireList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_EditQuestionnaireList");
      RadioButtonList RadioButtonList_EditContactPatient = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditContactPatient");
      TextBox TextBox_EditContactNumber = (TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_EditContactNumber");
      DropDownList DropDownList_EditLanguageList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_EditLanguageList");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_EditQuestionnaireList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        for (int a = 1; a <= TotalQuestions; a++)
        {
          RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditQ" + a + "");

          if (string.IsNullOrEmpty(RadioButtonList_EditQ.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(RadioButtonList_EditContactPatient.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (RadioButtonList_EditContactPatient.SelectedValue == "True")
        {
          if (string.IsNullOrEmpty(TextBox_EditContactNumber.Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(DropDownList_EditLanguageList.SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {

      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PROMS_Questionnaire_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditForm0UpdateClicked == true)
          {
            Button_EditForm0UpdateClicked = false;
            Response.Redirect("Form_PROMS.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_PROMS_PatientVisitNumber=" + Request.QueryString["s_PROMS_PatientVisitNumber"] + "", false);
          }

          if (Button_EditForm0PrintClicked == true)
          {
            Button_EditForm0PrintClicked = false;

            ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'>FormPrint('InfoQuest_Print.aspx?PrintPage=Form_PROMS&PrintValue=" + Request.QueryString["PROMS_Questionnaire_Id"] + "')</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "Reload", "<script language='javascript'>window.location.href='" + Request.Url.AbsoluteUri + "'</script>");
          }

          if (Button_EditForm0EmailClicked == true)
          {
            Button_EditForm0EmailClicked = false;

            ClientScript.RegisterStartupScript(this.GetType(), "Email", "<script language='javascript'>FormEmail('InfoQuest_Email.aspx?EmailPage=Form_PROMS&EmailValue=" + Request.QueryString["PROMS_Questionnaire_Id"] + "')</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "Reload", "<script language='javascript'>window.location.href='" + Request.Url.AbsoluteUri + "'</script>");
          }
        }
      }
    }


    protected void FormView_PROMS_Questionnaire_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
          {
            Response.Redirect("Form_PROMS.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_PROMS_PatientVisitNumber=" + Request.QueryString["s_PROMS_PatientVisitNumber"] + "", false);
          }
        }
      }
    }

    protected void FormView_PROMS_Questionnaire_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_PROMS_Questionnaire_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
        {
          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 30";
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
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_EditPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_EditPrint")).Visible = true;
          }

          if (Email == "False")
          {
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_EditEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_EditEmail")).Visible = true;
          }

          Email = "";
          Print = "";
        }
      }

      if (FormView_PROMS_Questionnaire_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
        {
          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 30";
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
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_ItemPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_ItemPrint")).Visible = true;
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('InfoQuest_Print.aspx?PrintPage=Form_PROMS&PrintValue=" + Request.QueryString["PROMS_Questionnaire_Id"] + "')";
          }

          if (Email == "False")
          {
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_ItemEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_ItemEmail")).Visible = true;
            ((Button)FormView_PROMS_Questionnaire_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('InfoQuest_Email.aspx?EmailPage=Form_PROMS&EmailValue=" + Request.QueryString["PROMS_Questionnaire_Id"] + "')";
          }

          Email = "";
          Print = "";
        }
      }
    }


    protected void HiddenField_InsertQuestionnaireTotalQuestions_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_InsertQuestionnaireTotalQuestions = (HiddenField)sender;
      HiddenField_InsertQuestionnaireTotalQuestions.Value = TotalQuestions.ToString(CultureInfo.CurrentCulture);
    }

    protected void HiddenField_EditQuestionnaireTotalQuestions_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_EditQuestionnaireTotalQuestions = (HiddenField)sender;
      HiddenField_EditQuestionnaireTotalQuestions.Value = TotalQuestions.ToString(CultureInfo.CurrentCulture);

      if (FormView_PROMS_Questionnaire_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
        {
          Session["QuestionnaireList"] = "";
          string SQLStringQuestionnaireList = "SELECT PROMS_Questionnaire_Questionnaire_List FROM InfoQuest_Form_PROMS_Questionnaire WHERE PROMS_Questionnaire_Id = @PROMS_Questionnaire_Id";
          using (SqlCommand SqlCommand_QuestionnaireList = new SqlCommand(SQLStringQuestionnaireList))
          {
            SqlCommand_QuestionnaireList.Parameters.AddWithValue("@PROMS_Questionnaire_Id", Request.QueryString["PROMS_Questionnaire_Id"]);
            DataTable DataTable_QuestionnaireList;
            using (DataTable_QuestionnaireList = new DataTable())
            {
              DataTable_QuestionnaireList.Locale = CultureInfo.CurrentCulture;
              DataTable_QuestionnaireList = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QuestionnaireList).Copy();
              if (DataTable_QuestionnaireList.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_QuestionnaireList.Rows)
                {
                  Session["QuestionnaireList"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_List"];
                }
              }
              else
              {
                Session["QuestionnaireList"] = "";
              }
            }
          }

          Session["QuestionParent"] = "";
          Session["QuestionId"] = "";
          Session["Question"] = "";
          Session["RowCount"] = "";
          string SQLStringQuestionnaire = "SELECT DISTINCT B.ListItem_Parent AS QuestionParent ,B.ListItem_Id AS QuestionId ,B.ListItem_Name AS Question ,RANK() OVER (ORDER BY CAST(LEFT(B.ListItem_Name,CHARINDEX('.',B.ListItem_Name,0) - 1) AS INT)) AS [RowCount] FROM Administration_ListItem AS B RIGHT OUTER JOIN Administration_ListItem AS A ON B.ListItem_Parent = A.ListItem_Id WHERE A.ListCategory_Id IN (79) AND B.ListCategory_Id IN (90) AND B.ListItem_Parent = @Id";
          using (SqlCommand SqlCommand_Questionnaire = new SqlCommand(SQLStringQuestionnaire))
          {
            SqlCommand_Questionnaire.Parameters.AddWithValue("@Id", Session["QuestionnaireList"].ToString());
            DataTable DataTable_Questionnaire;
            using (DataTable_Questionnaire = new DataTable())
            {
              DataTable_Questionnaire.Locale = CultureInfo.CurrentCulture;
              DataTable_Questionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questionnaire).Copy();
              if (DataTable_Questionnaire.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Questionnaire.Rows)
                {
                  Session["QuestionParent"] = DataRow_Row["QuestionParent"];
                  Session["QuestionId"] = DataRow_Row["QuestionId"];
                  Session["Question"] = DataRow_Row["Question"];
                  Session["RowCount"] = DataRow_Row["RowCount"];

                  Label Label_EditQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditHeadingQ" + Session["RowCount"].ToString() + "");
                  Label_EditQ.Text = Session["Question"].ToString();

                  RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditQ" + Session["RowCount"].ToString() + "");
                  RadioButtonList_EditQ.Items.Clear();

                  Session["AnswerParent"] = "";
                  Session["AnswerId"] = "";
                  Session["Answer"] = "";
                  Session["Score"] = "";
                  string SQLStringQuestions = "SELECT DISTINCT C.ListItem_Parent AS AnswerParent ,C.ListItem_Id AS AnswerId ,C.ListItem_Name AS Answer ,D.ListItem_Name AS Score FROM Administration_ListItem AS D RIGHT OUTER JOIN Administration_ListItem AS C ON D.ListItem_Parent = C.ListItem_Id WHERE D.ListCategory_Id IN (92) AND C.ListItem_Parent = @Id ORDER BY D.ListItem_Name DESC";
                  using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
                  {
                    SqlCommand_Questions.Parameters.AddWithValue("@Id", Session["QuestionId"].ToString());
                    DataTable DataTable_Questions;
                    using (DataTable_Questions = new DataTable())
                    {
                      DataTable_Questions.Locale = CultureInfo.CurrentCulture;
                      DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
                      if (DataTable_Questions.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_RowQuestions in DataTable_Questions.Rows)
                        {
                          Session["AnswerParent"] = DataRow_RowQuestions["AnswerParent"];
                          Session["AnswerId"] = DataRow_RowQuestions["AnswerId"];
                          Session["Answer"] = DataRow_RowQuestions["Answer"];
                          Session["Score"] = DataRow_RowQuestions["Score"];

                          ListItem EditQ = new ListItem();
                          EditQ.Text = Session["Answer"].ToString();
                          EditQ.Value = Session["AnswerId"].ToString();

                          RadioButtonList_EditQ.Items.Add(EditQ);
                        }
                      }
                      else
                      {
                        Session["AnswerParent"] = "";
                        Session["AnswerId"] = "";
                        Session["Answer"] = "";
                        Session["Score"] = "";
                      }
                    }
                  }
                }
              }
              else
              {
                Session["QuestionParent"] = "";
                Session["QuestionId"] = "";
                Session["Question"] = "";
                Session["RowCount"] = "";

                for (int a = 1; a <= TotalQuestions; a++)
                {
                  Label Label_EditQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditHeadingQ" + a + "");
                  Label_EditQ.Text = "";

                  RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditQ" + a + "");
                  RadioButtonList_EditQ.Items.Clear();
                }
              }
            }
          }

          Session["QuestionnaireList"] = "";
        }
      }
    }

    protected void HiddenField_ItemQuestionnaireTotalQuestions_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_ItemQuestionnaireTotalQuestions = (HiddenField)sender;
      HiddenField_ItemQuestionnaireTotalQuestions.Value = TotalQuestions.ToString(CultureInfo.CurrentCulture);

      if (FormView_PROMS_Questionnaire_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
        {
          Session["PROMSQuestionnaireQuestionnaireList"] = "";
          Session["PROMSQuestionnaireQuestionnaireName"] = "";
          Session["PROMSQuestionnaireQ1Name"] = "";
          Session["PROMSQuestionnaireQ2Name"] = "";
          Session["PROMSQuestionnaireQ3Name"] = "";
          Session["PROMSQuestionnaireQ4Name"] = "";
          Session["PROMSQuestionnaireQ5Name"] = "";
          Session["PROMSQuestionnaireQ6Name"] = "";
          Session["PROMSQuestionnaireQ7Name"] = "";
          Session["PROMSQuestionnaireQ8Name"] = "";
          Session["PROMSQuestionnaireQ9Name"] = "";
          Session["PROMSQuestionnaireQ10Name"] = "";
          Session["PROMSQuestionnaireQ11Name"] = "";
          Session["PROMSQuestionnaireQ12Name"] = "";
          Session["PROMSQuestionnaireLanguageName"] = "";
          string SQLStringPROMSQuestionnaire = "SELECT PROMS_Questionnaire_Questionnaire_List , PROMS_Questionnaire_Questionnaire_Name , PROMS_Questionnaire_Q1_Name ,PROMS_Questionnaire_Q2_Name , PROMS_Questionnaire_Q3_Name ,PROMS_Questionnaire_Q4_Name ,PROMS_Questionnaire_Q5_Name ,PROMS_Questionnaire_Q6_Name ,PROMS_Questionnaire_Q7_Name ,PROMS_Questionnaire_Q8_Name ,PROMS_Questionnaire_Q9_Name ,PROMS_Questionnaire_Q10_Name ,PROMS_Questionnaire_Q11_Name ,PROMS_Questionnaire_Q12_Name ,PROMS_Questionnaire_Language_Name FROM vForm_PROMS_Questionnaire WHERE PROMS_Questionnaire_Id = @PROMS_Questionnaire_Id";
          using (SqlCommand SqlCommand_PROMSQuestionnaire = new SqlCommand(SQLStringPROMSQuestionnaire))
          {
            SqlCommand_PROMSQuestionnaire.Parameters.AddWithValue("@PROMS_Questionnaire_Id", Request.QueryString["PROMS_Questionnaire_Id"]);
            DataTable DataTable_PROMSQuestionnaire;
            using (DataTable_PROMSQuestionnaire = new DataTable())
            {
              DataTable_PROMSQuestionnaire.Locale = CultureInfo.CurrentCulture;
              DataTable_PROMSQuestionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSQuestionnaire).Copy();
              if (DataTable_PROMSQuestionnaire.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_PROMSQuestionnaire.Rows)
                {
                  Session["PROMSQuestionnaireQuestionnaireList"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_List"];
                  Session["PROMSQuestionnaireQuestionnaireName"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_Name"];
                  Session["PROMSQuestionnaireQ1Name"] = DataRow_Row["PROMS_Questionnaire_Q1_Name"];
                  Session["PROMSQuestionnaireQ2Name"] = DataRow_Row["PROMS_Questionnaire_Q2_Name"];
                  Session["PROMSQuestionnaireQ3Name"] = DataRow_Row["PROMS_Questionnaire_Q3_Name"];
                  Session["PROMSQuestionnaireQ4Name"] = DataRow_Row["PROMS_Questionnaire_Q4_Name"];
                  Session["PROMSQuestionnaireQ5Name"] = DataRow_Row["PROMS_Questionnaire_Q5_Name"];
                  Session["PROMSQuestionnaireQ6Name"] = DataRow_Row["PROMS_Questionnaire_Q6_Name"];
                  Session["PROMSQuestionnaireQ7Name"] = DataRow_Row["PROMS_Questionnaire_Q7_Name"];
                  Session["PROMSQuestionnaireQ8Name"] = DataRow_Row["PROMS_Questionnaire_Q8_Name"];
                  Session["PROMSQuestionnaireQ9Name"] = DataRow_Row["PROMS_Questionnaire_Q9_Name"];
                  Session["PROMSQuestionnaireQ10Name"] = DataRow_Row["PROMS_Questionnaire_Q10_Name"];
                  Session["PROMSQuestionnaireQ11Name"] = DataRow_Row["PROMS_Questionnaire_Q11_Name"];
                  Session["PROMSQuestionnaireQ12Name"] = DataRow_Row["PROMS_Questionnaire_Q12_Name"];
                  Session["PROMSQuestionnaireLanguageName"] = DataRow_Row["PROMS_Questionnaire_Language_Name"];
                }
              }
            }
          }

          Label Label_ItemQuestionnaireList = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_ItemQuestionnaireList");
          Label_ItemQuestionnaireList.Text = Session["PROMSQuestionnaireQuestionnaireName"].ToString();
          Label Label_ItemLanguageList = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_ItemLanguageList");
          Label_ItemLanguageList.Text = Session["PROMSQuestionnaireLanguageName"].ToString();

          for (int a = 1; a <= TotalQuestions; a++)
          {
            Label Label_ItemQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_ItemQ" + a + "");
            Label_ItemQ.Text = Session["PROMSQuestionnaireQ" + a + "Name"].ToString();
          }

          Session["QuestionParent"] = "";
          Session["QuestionId"] = "";
          Session["Question"] = "";
          Session["RowCount"] = "";
          string SQLStringQuestionnaire = "SELECT DISTINCT B.ListItem_Parent AS QuestionParent ,B.ListItem_Id AS QuestionId ,B.ListItem_Name AS Question ,RANK() OVER (ORDER BY CAST(LEFT(B.ListItem_Name,CHARINDEX('.',B.ListItem_Name,0) - 1) AS INT)) AS [RowCount] FROM Administration_ListItem AS B RIGHT OUTER JOIN Administration_ListItem AS A ON B.ListItem_Parent = A.ListItem_Id WHERE A.ListCategory_Id IN (79) AND B.ListCategory_Id IN (90) AND B.ListItem_Parent = @Id";
          using (SqlCommand SqlCommand_Questionnaire = new SqlCommand(SQLStringQuestionnaire))
          {
            SqlCommand_Questionnaire.Parameters.AddWithValue("@Id", Session["PROMSQuestionnaireQuestionnaireList"].ToString());
            DataTable DataTable_Questionnaire;
            using (DataTable_Questionnaire = new DataTable())
            {
              DataTable_Questionnaire.Locale = CultureInfo.CurrentCulture;
              DataTable_Questionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questionnaire).Copy();
              if (DataTable_Questionnaire.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Questionnaire.Rows)
                {
                  Session["QuestionParent"] = DataRow_Row["QuestionParent"];
                  Session["QuestionId"] = DataRow_Row["QuestionId"];
                  Session["Question"] = DataRow_Row["Question"];
                  Session["RowCount"] = DataRow_Row["RowCount"];

                  Label Label_InsertQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_ItemHeadingQ" + Session["RowCount"].ToString() + "");
                  Label_InsertQ.Text = Session["Question"].ToString();
                }
              }
              else
              {
                Session["QuestionParent"] = "";
                Session["QuestionId"] = "";
                Session["Question"] = "";
                Session["RowCount"] = "";
              }
            }
          }

          Session["PROMSQuestionnaireQuestionnaireList"] = "";
          Session["PROMSQuestionnaireQuestionnaireName"] = "";
          Session["PROMSQuestionnaireQ1Name"] = "";
          Session["PROMSQuestionnaireQ2Name"] = "";
          Session["PROMSQuestionnaireQ3Name"] = "";
          Session["PROMSQuestionnaireQ4Name"] = "";
          Session["PROMSQuestionnaireQ5Name"] = "";
          Session["PROMSQuestionnaireQ6Name"] = "";
          Session["PROMSQuestionnaireQ7Name"] = "";
          Session["PROMSQuestionnaireQ8Name"] = "";
          Session["PROMSQuestionnaireQ9Name"] = "";
          Session["PROMSQuestionnaireQ10Name"] = "";
          Session["PROMSQuestionnaireQ11Name"] = "";
          Session["PROMSQuestionnaireQ12Name"] = "";
          Session["PROMSQuestionnaireLanguageName"] = "";
        }
      }
    }

    protected void DropDownList_InsertQuestionnaireList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertQuestionnaireList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertQuestionnaireList");

      Session["PROMSQuestionnaireId"] = "";
      Session["PROMSQuestionnaireQuestionnaireName"] = "";
      Session["PROMSQuestionnaireCreatedDate"] = "";
      string SQLStringPROMSQuestionnaireId = "SELECT PROMS_Questionnaire_Id , PROMS_Questionnaire_Questionnaire_Name , PROMS_Questionnaire_CreatedDate FROM vForm_PROMS WHERE Facility_Id = @Facility_Id AND PROMS_Questionnaire_PatientVisitNumber = @PROMS_Questionnaire_PatientVisitNumber AND PROMS_Questionnaire_Questionnaire_List = @PROMS_Questionnaire_Questionnaire_List";
      using (SqlCommand SqlCommand_PROMSQuestionnaireId = new SqlCommand(SQLStringPROMSQuestionnaireId))
      {
        SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@PROMS_Questionnaire_PatientVisitNumber", Request.QueryString["s_PROMS_PatientVisitNumber"]);
        SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@PROMS_Questionnaire_Questionnaire_List", DropDownList_InsertQuestionnaireList.SelectedValue.ToString());
        DataTable DataTable_PROMSQuestionnaireId;
        using (DataTable_PROMSQuestionnaireId = new DataTable())
        {
          DataTable_PROMSQuestionnaireId.Locale = CultureInfo.CurrentCulture;
          DataTable_PROMSQuestionnaireId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSQuestionnaireId).Copy();
          if (DataTable_PROMSQuestionnaireId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PROMSQuestionnaireId.Rows)
            {
              Session["PROMSQuestionnaireId"] = DataRow_Row["PROMS_Questionnaire_Id"];
              Session["PROMSQuestionnaireQuestionnaireName"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_Name"];
              Session["PROMSQuestionnaireCreatedDate"] = DataRow_Row["PROMS_Questionnaire_CreatedDate"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["PROMSQuestionnaireId"].ToString()))
      {
        Page.MaintainScrollPositionOnPostBack = false;

        string Label_InsertConcurrencyInsertMessage = "" +
          "Questionnaire could not be loaded<br/>" +
          "A Questionnaire for " + Session["PROMSQuestionnaireQuestionnaireName"].ToString() + " has already been captured for this Visit Number at " + Session["PROMSQuestionnaireCreatedDate"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "&PROMS_Questionnaire_Id=" + Session["PROMSQuestionnaireId"].ToString() + "&ViewModeQ=0&FollowUp=0#LinkPatientInfo' style='color:#b0262e;'>Reload Page</a> to view Questionnaire<br/><br/>";

        ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertInvalidFormMessage")).Text = "";
        ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = Convert.ToString(Label_InsertConcurrencyInsertMessage, CultureInfo.CurrentCulture);

        for (int a = 1; a <= TotalQuestions; a++)
        {
          Label Label_InsertQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertHeadingQ" + a + "");
          Label_InsertQ.Text = "";

          RadioButtonList RadioButtonList_InsertQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertQ" + a + "");
          RadioButtonList_InsertQ.Items.Clear();
        }
      }
      else
      {
        ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertInvalidFormMessage")).Text = "";
        ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";

        Session["QuestionParent"] = "";
        Session["QuestionId"] = "";
        Session["Question"] = "";
        Session["RowCount"] = "";
        string SQLStringQuestionnaire = "SELECT DISTINCT B.ListItem_Parent AS QuestionParent ,B.ListItem_Id AS QuestionId ,B.ListItem_Name AS Question ,RANK() OVER (ORDER BY CAST(LEFT(B.ListItem_Name,CHARINDEX('.',B.ListItem_Name,0) - 1) AS INT)) AS [RowCount] FROM Administration_ListItem AS B RIGHT OUTER JOIN Administration_ListItem AS A ON B.ListItem_Parent = A.ListItem_Id WHERE A.ListCategory_Id IN (79) AND B.ListCategory_Id IN (90) AND B.ListItem_Parent = @Id";
        using (SqlCommand SqlCommand_Questionnaire = new SqlCommand(SQLStringQuestionnaire))
        {
          SqlCommand_Questionnaire.Parameters.AddWithValue("@Id", DropDownList_InsertQuestionnaireList.SelectedValue);
          DataTable DataTable_Questionnaire;
          using (DataTable_Questionnaire = new DataTable())
          {
            DataTable_Questionnaire.Locale = CultureInfo.CurrentCulture;
            DataTable_Questionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questionnaire).Copy();
            if (DataTable_Questionnaire.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Questionnaire.Rows)
              {
                Session["QuestionParent"] = DataRow_Row["QuestionParent"];
                Session["QuestionId"] = DataRow_Row["QuestionId"];
                Session["Question"] = DataRow_Row["Question"];
                Session["RowCount"] = DataRow_Row["RowCount"];

                Label Label_InsertQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertHeadingQ" + Session["RowCount"].ToString() + "");
                Label_InsertQ.Text = Session["Question"].ToString();

                RadioButtonList RadioButtonList_InsertQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertQ" + Session["RowCount"].ToString() + "");
                RadioButtonList_InsertQ.Items.Clear();

                Session["AnswerParent"] = "";
                Session["AnswerId"] = "";
                Session["Answer"] = "";
                Session["Score"] = "";
                string SQLStringQuestions = "SELECT DISTINCT C.ListItem_Parent AS AnswerParent ,C.ListItem_Id AS AnswerId ,C.ListItem_Name AS Answer ,D.ListItem_Name AS Score FROM Administration_ListItem AS D RIGHT OUTER JOIN Administration_ListItem AS C ON D.ListItem_Parent = C.ListItem_Id WHERE D.ListCategory_Id IN (92) AND C.ListItem_Parent = @Id ORDER BY D.ListItem_Name DESC";
                using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
                {
                  SqlCommand_Questions.Parameters.AddWithValue("@Id", Session["QuestionId"].ToString());
                  DataTable DataTable_Questions;
                  using (DataTable_Questions = new DataTable())
                  {
                    DataTable_Questions.Locale = CultureInfo.CurrentCulture;
                    DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
                    if (DataTable_Questions.Rows.Count > 0)
                    {
                      foreach (DataRow DataRow_RowQuestions in DataTable_Questions.Rows)
                      {
                        Session["AnswerParent"] = DataRow_RowQuestions["AnswerParent"];
                        Session["AnswerId"] = DataRow_RowQuestions["AnswerId"];
                        Session["Answer"] = DataRow_RowQuestions["Answer"];
                        Session["Score"] = DataRow_RowQuestions["Score"];

                        ListItem InsertQ = new ListItem();
                        InsertQ.Text = Session["Answer"].ToString();
                        InsertQ.Value = Session["AnswerId"].ToString();

                        RadioButtonList_InsertQ.Items.Add(InsertQ);
                      }
                    }
                  }
                }
              }
            }
            else
            {
              Session["QuestionParent"] = "";
              Session["QuestionId"] = "";
              Session["Question"] = "";
              Session["RowCount"] = "";

              for (int a = 1; a <= TotalQuestions; a++)
              {
                Label Label_InsertQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertHeadingQ" + a + "");
                Label_InsertQ.Text = "";

                RadioButtonList RadioButtonList_InsertQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertQ" + a + "");
                RadioButtonList_InsertQ.Items.Clear();
              }
            }
          }
        }
      }

      ((DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertQuestionnaireList")).Attributes.Add("OnChange", "Validation_Form0();ShowHide_Form0();");

      for (int a = 1; a <= TotalQuestions; a++)
      {
        ((RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertQ" + a + "")).Attributes.Add("OnClick", "Validation_Form0();Calculation_Form0();");
      }

      ((RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertContactPatient")).Attributes.Add("OnClick", "Validation_Form0();ShowHide_Form0();");
      ((TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_InsertContactNumber")).Attributes.Add("OnKeyUp", "Validation_Form0();");
      ((TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_InsertContactNumber")).Attributes.Add("OnInput", "Validation_Form0();");
      ((DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertLanguageList")).Attributes.Add("OnChange", "Validation_Form0();ShowHide_Form0();");

      RadioButtonList RadioButtonList_InsertContactPatient = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_InsertContactPatient");
      RadioButtonList_InsertContactPatient.SelectedValue = "True";

      Session["ListItemsName"] = "";
      string SQLStringListItems = "SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 105";
      using (SqlCommand SqlCommand_ListItems = new SqlCommand(SQLStringListItems))
      {
        DataTable DataTable_ListItems;
        using (DataTable_ListItems = new DataTable())
        {
          DataTable_ListItems.Locale = CultureInfo.CurrentCulture;
          DataTable_ListItems = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ListItems).Copy();
          if (DataTable_ListItems.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ListItems.Rows)
            {
              Session["ListItemsName"] = DataRow_Row["ListItem_Name"];
            }
          }
        }
      }

      DropDownList DropDownList_InsertLanguageList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_InsertLanguageList");
      DropDownList_InsertLanguageList.SelectedValue = Session["ListItemsName"].ToString();

      Session["ListItemsName"] = "";

      string AdmissionDate = Label_PIDateAdmission.Text;
      Label Label_InsertAdmissionDate = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_InsertAdmissionDate");
      Label_InsertAdmissionDate.Text = AdmissionDate;

      string ContactNumber = Label_PIContactNumber.Text;
      TextBox TextBox_InsertContactNumber = (TextBox)FormView_PROMS_Questionnaire_Form.FindControl("TextBox_InsertContactNumber");
      TextBox_InsertContactNumber.Text = ContactNumber;

      GridView_PROMS_Questionnaire.DataBind();
    }

    protected void DropDownList_EditQuestionnaireList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditQuestionnaireList = (DropDownList)FormView_PROMS_Questionnaire_Form.FindControl("DropDownList_EditQuestionnaireList");

      Session["PROMSQuestionnaireId"] = "";
      Session["PROMSQuestionnaireQuestionnaireName"] = "";
      Session["PROMSQuestionnaireCreatedDate"] = "";
      string SQLStringPROMSQuestionnaireId = "SELECT PROMS_Questionnaire_Id , PROMS_Questionnaire_Questionnaire_Name , PROMS_Questionnaire_CreatedDate FROM vForm_PROMS WHERE Facility_Id = @Facility_Id AND PROMS_Questionnaire_PatientVisitNumber = @PROMS_Questionnaire_PatientVisitNumber AND PROMS_Questionnaire_Questionnaire_List = @PROMS_Questionnaire_Questionnaire_List";
      using (SqlCommand SqlCommand_PROMSQuestionnaireId = new SqlCommand(SQLStringPROMSQuestionnaireId))
      {
        SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@PROMS_Questionnaire_PatientVisitNumber", Request.QueryString["s_PROMS_PatientVisitNumber"]);
        SqlCommand_PROMSQuestionnaireId.Parameters.AddWithValue("@PROMS_Questionnaire_Questionnaire_List", DropDownList_EditQuestionnaireList.SelectedValue.ToString());
        DataTable DataTable_PROMSQuestionnaireId;
        using (DataTable_PROMSQuestionnaireId = new DataTable())
        {
          DataTable_PROMSQuestionnaireId.Locale = CultureInfo.CurrentCulture;
          DataTable_PROMSQuestionnaireId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSQuestionnaireId).Copy();
          if (DataTable_PROMSQuestionnaireId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PROMSQuestionnaireId.Rows)
            {
              Session["PROMSQuestionnaireId"] = DataRow_Row["PROMS_Questionnaire_Id"];
              Session["PROMSQuestionnaireQuestionnaireName"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_Name"];
              Session["PROMSQuestionnaireCreatedDate"] = DataRow_Row["PROMS_Questionnaire_CreatedDate"];
            }
          }
        }
      }

      if ((!string.IsNullOrEmpty(Session["PROMSQuestionnaireId"].ToString())) && (Session["PROMSQuestionnaireId"].ToString() != Request.QueryString["PROMS_Questionnaire_Id"]))
      {
        Page.MaintainScrollPositionOnPostBack = false;

        string Label_EditConcurrencyUpdateMessage = "" +
          "Questionnaire could not be loaded<br/>" +
          "A Questionnaire for " + Session["PROMSQuestionnaireQuestionnaireName"].ToString() + " has already been captured for this Visit Number at " + Session["PROMSQuestionnaireCreatedDate"].ToString() + "<br/>" +
          "<a href='Form_PROMS.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_PROMS_PatientVisitNumber=" + Request.QueryString["s_PROMS_PatientVisitNumber"] + "&PROMS_Questionnaire_Id=" + Session["PROMSQuestionnaireId"].ToString() + "&ViewModeQ=0&FollowUp=0#LinkPatientInfo' style='color:#b0262e;'>Reload Page</a> to view Questionnaire<br/><br/>";

        ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
        ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Convert.ToString(Label_EditConcurrencyUpdateMessage, CultureInfo.CurrentCulture);

        for (int a = 1; a <= TotalQuestions; a++)
        {
          Label Label_EditQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditHeadingQ" + a + "");
          Label_EditQ.Text = "";

          RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditQ" + a + "");
          RadioButtonList_EditQ.Items.Clear();
        }
      }
      else
      {
        ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
        ((Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";

        Session["QuestionParent"] = "";
        Session["QuestionId"] = "";
        Session["Question"] = "";
        Session["RowCount"] = "";
        string SQLStringQuestionnaire = "SELECT DISTINCT B.ListItem_Parent AS QuestionParent ,B.ListItem_Id AS QuestionId ,B.ListItem_Name AS Question ,RANK() OVER (ORDER BY CAST(LEFT(B.ListItem_Name,CHARINDEX('.',B.ListItem_Name,0) - 1) AS INT)) AS [RowCount] FROM Administration_ListItem AS B RIGHT OUTER JOIN Administration_ListItem AS A ON B.ListItem_Parent = A.ListItem_Id WHERE A.ListCategory_Id IN (79) AND B.ListCategory_Id IN (90) AND B.ListItem_Parent = @Id";
        using (SqlCommand SqlCommand_Questionnaire = new SqlCommand(SQLStringQuestionnaire))
        {
          SqlCommand_Questionnaire.Parameters.AddWithValue("@Id", DropDownList_EditQuestionnaireList.SelectedValue);
          DataTable DataTable_Questionnaire;
          using (DataTable_Questionnaire = new DataTable())
          {
            DataTable_Questionnaire.Locale = CultureInfo.CurrentCulture;
            DataTable_Questionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questionnaire).Copy();
            if (DataTable_Questionnaire.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Questionnaire.Rows)
              {
                Session["QuestionParent"] = DataRow_Row["QuestionParent"];
                Session["QuestionId"] = DataRow_Row["QuestionId"];
                Session["Question"] = DataRow_Row["Question"];
                Session["RowCount"] = DataRow_Row["RowCount"];

                Label Label_EditQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditHeadingQ" + Session["RowCount"].ToString() + "");
                Label_EditQ.Text = Session["Question"].ToString();

                RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditQ" + Session["RowCount"].ToString() + "");
                RadioButtonList_EditQ.Items.Clear();

                Session["AnswerParent"] = "";
                Session["AnswerId"] = "";
                Session["Answer"] = "";
                Session["Score"] = "";
                string SQLStringQuestions = "SELECT DISTINCT C.ListItem_Parent AS AnswerParent ,C.ListItem_Id AS AnswerId ,C.ListItem_Name AS Answer ,D.ListItem_Name AS Score FROM Administration_ListItem AS D RIGHT OUTER JOIN Administration_ListItem AS C ON D.ListItem_Parent = C.ListItem_Id WHERE D.ListCategory_Id IN (92) AND C.ListItem_Parent = @Id ORDER BY D.ListItem_Name DESC";
                using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
                {
                  SqlCommand_Questions.Parameters.AddWithValue("@Id", Session["QuestionId"].ToString());
                  DataTable DataTable_Questions;
                  using (DataTable_Questions = new DataTable())
                  {
                    DataTable_Questions.Locale = CultureInfo.CurrentCulture;
                    DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
                    if (DataTable_Questions.Rows.Count > 0)
                    {
                      foreach (DataRow DataRow_RowQuestions in DataTable_Questions.Rows)
                      {
                        Session["AnswerParent"] = DataRow_RowQuestions["AnswerParent"];
                        Session["AnswerId"] = DataRow_RowQuestions["AnswerId"];
                        Session["Answer"] = DataRow_RowQuestions["Answer"];
                        Session["Score"] = DataRow_RowQuestions["Score"];

                        ListItem EditQ = new ListItem();
                        EditQ.Text = Session["Answer"].ToString();
                        EditQ.Value = Session["AnswerId"].ToString();

                        RadioButtonList_EditQ.Items.Add(EditQ);
                      }
                    }
                  }
                }
              }
            }
            else
            {
              Session["QuestionParent"] = "";
              Session["QuestionId"] = "";
              Session["Question"] = "";
              Session["RowCount"] = "";

              for (int a = 1; a <= TotalQuestions; a++)
              {
                Label Label_EditQ = (Label)FormView_PROMS_Questionnaire_Form.FindControl("Label_EditHeadingQ" + a + "");
                Label_EditQ.Text = "";

                RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_Questionnaire_Form.FindControl("RadioButtonList_EditQ" + a + "");
                RadioButtonList_EditQ.Items.Clear();
              }
            }
          }
        }
      }
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditForm0UpdateClicked = true;
      Button_EditForm1UpdateClicked = true;
    }

    protected void Button_EditPrint_Click(object sender, EventArgs e)
    {
      Button_EditForm0PrintClicked = true;
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      Button_EditForm0EmailClicked = true;
    }
    //---END--- --Table_PROMS_Questionnaire--//

    //--START-- --Table_PROMS_FollowUp--//
    protected void FormView_PROMS_FollowUp_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidationFollowUp();

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
          ((Label)FormView_PROMS_FollowUp_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_PROMS_FollowUp_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_Questionnaire_Id"].DefaultValue = Request.QueryString["PROMS_Questionnaire_Id"];
          SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_CompletionDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_History"].DefaultValue = "";

          CheckBox CheckBox_InsertCancelled = (CheckBox)FormView_PROMS_FollowUp_Form.FindControl("CheckBox_InsertCancelled");
          if (CheckBox_InsertCancelled.Checked == false)
          {
            int FollowUpScore = 0;
            for (int a = 1; a <= TotalQuestions; a++)
            {
              int SelectedIndex = Convert.ToInt32(((RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ" + a + "")).SelectedIndex);
              int SelectedValue = 4 - SelectedIndex;
              FollowUpScore = FollowUpScore + SelectedValue;
            }

            SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_Score"].DefaultValue = FollowUpScore.ToString(CultureInfo.CurrentCulture);
          }
          else
          {
            SqlDataSource_PROMS_FollowUp_Form.InsertParameters["PROMS_FollowUp_Score"].DefaultValue = "";
          }
        }
      }
    }

    protected string InsertValidationFollowUp()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      CheckBox CheckBox_InsertCancelled = (CheckBox)FormView_PROMS_FollowUp_Form.FindControl("CheckBox_InsertCancelled");
      DropDownList DropDownList_InsertCancelledList = (DropDownList)FormView_PROMS_FollowUp_Form.FindControl("DropDownList_InsertCancelledList");
      RadioButtonList RadioButtonList_InsertQ1 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ1");
      RadioButtonList RadioButtonList_InsertQ2 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ2");
      RadioButtonList RadioButtonList_InsertQ3 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ3");
      RadioButtonList RadioButtonList_InsertQ4 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ4");
      RadioButtonList RadioButtonList_InsertQ5 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ5");
      RadioButtonList RadioButtonList_InsertQ6 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ6");
      RadioButtonList RadioButtonList_InsertQ7 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ7");
      RadioButtonList RadioButtonList_InsertQ8 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ8");
      RadioButtonList RadioButtonList_InsertQ9 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ9");
      RadioButtonList RadioButtonList_InsertQ10 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ10");
      RadioButtonList RadioButtonList_InsertQ11 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ11");
      RadioButtonList RadioButtonList_InsertQ12 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ12");

      if (InvalidForm == "No")
      {
        if (CheckBox_InsertCancelled.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertCancelledList.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else if (CheckBox_InsertCancelled.Checked == false)
        {
          if (string.IsNullOrEmpty(RadioButtonList_InsertQ1.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ2.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ3.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ4.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ5.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ6.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ7.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ8.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ9.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ10.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ11.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertQ12.SelectedValue))
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

      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PROMS_FollowUp_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_PROMS.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_PROMS_PatientVisitNumber=" + Request.QueryString["s_PROMS_PatientVisitNumber"] + "", false);
    }


    protected void FormView_PROMS_FollowUp_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDPROMSFollowUpModifiedDate"] = e.OldValues["PROMS_FollowUp_ModifiedDate"];
        object OLDPROMSFollowUpModifiedDate = Session["OLDPROMSFollowUpModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDPROMSFollowUpModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_ComparePROMS = (DataView)SqlDataSource_PROMS_FollowUp_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ComparePROMS = DataView_ComparePROMS[0];
        Session["DBPROMSFollowUpModifiedDate"] = Convert.ToString(DataRowView_ComparePROMS["PROMS_FollowUp_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBPROMSFollowUpModifiedBy"] = Convert.ToString(DataRowView_ComparePROMS["PROMS_FollowUp_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBPROMSFollowUpModifiedDate = Session["DBPROMSFollowUpModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBPROMSFollowUpModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_PROMS_FollowUp_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Visible = true;

          ((Label)FormView_PROMS_FollowUp_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBPROMSFollowUpModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidationFollowUp();

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
            ((Label)FormView_PROMS_FollowUp_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_PROMS_FollowUp_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["PROMS_FollowUp_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["PROMS_FollowUp_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_PROMS_FollowUp", "PROMS_FollowUp_Id = " + Request.QueryString["PROMS_FollowUp_Id"]);

            DataView DataView_PROMS = (DataView)SqlDataSource_PROMS_FollowUp_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_PROMS = DataView_PROMS[0];
            Session["PROMSFollowUpHistory"] = Convert.ToString(DataRowView_PROMS["PROMS_FollowUp_History"], CultureInfo.CurrentCulture);

            Session["PROMSFollowUpHistory"] = Session["History"].ToString() + Session["PROMSFollowUpHistory"].ToString();
            e.NewValues["PROMS_FollowUp_History"] = Session["PROMSFollowUpHistory"].ToString();

            Session["PROMSFollowUpHistory"] = "";
            Session["History"] = "";


            CheckBox CheckBox_EditCancelled = (CheckBox)FormView_PROMS_FollowUp_Form.FindControl("CheckBox_EditCancelled");
            if (CheckBox_EditCancelled.Checked == false)
            {
              int FollowUpScore = 0;
              for (int a = 1; a <= TotalQuestions; a++)
              {
                int SelectedIndex = Convert.ToInt32(((RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ" + a + "")).SelectedIndex);
                int SelectedValue = 4 - SelectedIndex;
                FollowUpScore = FollowUpScore + SelectedValue;
              }

              e.NewValues["PROMS_FollowUp_Score"] = FollowUpScore.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
              e.NewValues["PROMS_FollowUp_Score"] = "";
            }
          }
        }

        Session["OLDPROMSFollowUpModifiedDate"] = "";
        Session["DBPROMSFollowUpModifiedDate"] = "";
        Session["DBPROMSFollowUpModifiedBy"] = "";
      }
    }

    protected string EditValidationFollowUp()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      CheckBox CheckBox_EditCancelled = (CheckBox)FormView_PROMS_FollowUp_Form.FindControl("CheckBox_EditCancelled");
      DropDownList DropDownList_EditCancelledList = (DropDownList)FormView_PROMS_FollowUp_Form.FindControl("DropDownList_EditCancelledList");
      RadioButtonList RadioButtonList_EditQ1 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ1");
      RadioButtonList RadioButtonList_EditQ2 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ2");
      RadioButtonList RadioButtonList_EditQ3 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ3");
      RadioButtonList RadioButtonList_EditQ4 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ4");
      RadioButtonList RadioButtonList_EditQ5 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ5");
      RadioButtonList RadioButtonList_EditQ6 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ6");
      RadioButtonList RadioButtonList_EditQ7 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ7");
      RadioButtonList RadioButtonList_EditQ8 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ8");
      RadioButtonList RadioButtonList_EditQ9 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ9");
      RadioButtonList RadioButtonList_EditQ10 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ10");
      RadioButtonList RadioButtonList_EditQ11 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ11");
      RadioButtonList RadioButtonList_EditQ12 = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ12");

      if (InvalidForm == "No")
      {
        if (CheckBox_EditCancelled.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditCancelledList.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else if (CheckBox_EditCancelled.Checked == false)
        {
          if (string.IsNullOrEmpty(RadioButtonList_EditQ1.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ2.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ3.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ4.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ5.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ6.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ7.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ8.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ9.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ10.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ11.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditQ12.SelectedValue))
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

      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PROMS_FollowUp_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditForm1UpdateClicked == true)
          {
            Button_EditForm1UpdateClicked = false;
            Response.Redirect("Form_PROMS.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_PROMS_PatientVisitNumber=" + Request.QueryString["s_PROMS_PatientVisitNumber"] + "", false);
          }
        }
      }
    }


    protected void FormView_PROMS_FollowUp_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
          {
            Response.Redirect("Form_PROMS.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_PROMS_PatientVisitNumber=" + Request.QueryString["s_PROMS_PatientVisitNumber"] + "", false);
          }
        }
      }
    }

    protected void FormView_PROMS_FollowUp_Form_DataBound(object sender, EventArgs e)
    {
      if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
      {
        Session["PROMSQuestionnaireReportNumber"] = "";
        Session["PROMSQuestionnaireQuestionnaireName"] = "";
        string SQLStringPROMSQuestionnaire = "SELECT PROMS_Questionnaire_ReportNumber , PROMS_Questionnaire_Questionnaire_Name FROM vForm_PROMS_Questionnaire WHERE PROMS_Questionnaire_Id = @PROMS_Questionnaire_Id";
        using (SqlCommand SqlCommand_PROMSQuestionnaire = new SqlCommand(SQLStringPROMSQuestionnaire))
        {
          SqlCommand_PROMSQuestionnaire.Parameters.AddWithValue("@PROMS_Questionnaire_Id", Request.QueryString["PROMS_Questionnaire_Id"]);
          DataTable DataTable_PROMSQuestionnaire;
          using (DataTable_PROMSQuestionnaire = new DataTable())
          {
            DataTable_PROMSQuestionnaire.Locale = CultureInfo.CurrentCulture;
            DataTable_PROMSQuestionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSQuestionnaire).Copy();
            if (DataTable_PROMSQuestionnaire.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PROMSQuestionnaire.Rows)
              {
                Session["PROMSQuestionnaireReportNumber"] = DataRow_Row["PROMS_Questionnaire_ReportNumber"];
                Session["PROMSQuestionnaireQuestionnaireName"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_Name"];
              }
            }
          }
        }

      }

      if (FormView_PROMS_FollowUp_Form.CurrentMode == FormViewMode.Insert)
      {
        Label Label_InsertQuestionnaireList = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_InsertQuestionnaireList");
        Label_InsertQuestionnaireList.Text = Session["PROMSQuestionnaireQuestionnaireName"].ToString();
      }

      if (FormView_PROMS_FollowUp_Form.CurrentMode == FormViewMode.Edit)
      {
        Label Label_EditQuestionnaireList = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_EditQuestionnaireList");
        Label_EditQuestionnaireList.Text = Session["PROMSQuestionnaireQuestionnaireName"].ToString();
      }

      if (FormView_PROMS_FollowUp_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        Label Label_ItemQuestionnaireList = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_ItemQuestionnaireList");
        if (Label_ItemQuestionnaireList != null)
        {
          Label_ItemQuestionnaireList.Text = Session["PROMSQuestionnaireQuestionnaireName"].ToString();
        }
      }

      Session["PROMSQuestionnaireReportNumber"] = "";
      Session["PROMSQuestionnaireQuestionnaireName"] = "";
    }


    protected void HiddenField_InsertFollowUpTotalQuestions_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_InsertFollowUpTotalQuestions = (HiddenField)sender;
      HiddenField_InsertFollowUpTotalQuestions.Value = TotalQuestions.ToString(CultureInfo.CurrentCulture);

      if (FormView_PROMS_FollowUp_Form.CurrentMode == FormViewMode.Insert)
      {
        if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
        {
          Session["QuestionnaireList"] = "";
          string SQLStringPROMSQuestionnaire = "SELECT PROMS_Questionnaire_Questionnaire_List FROM vForm_PROMS_Questionnaire WHERE PROMS_Questionnaire_Id = @PROMS_Questionnaire_Id";
          using (SqlCommand SqlCommand_PROMSQuestionnaire = new SqlCommand(SQLStringPROMSQuestionnaire))
          {
            SqlCommand_PROMSQuestionnaire.Parameters.AddWithValue("@PROMS_Questionnaire_Id", Request.QueryString["PROMS_Questionnaire_Id"]);
            DataTable DataTable_PROMSQuestionnaire;
            using (DataTable_PROMSQuestionnaire = new DataTable())
            {
              DataTable_PROMSQuestionnaire.Locale = CultureInfo.CurrentCulture;
              DataTable_PROMSQuestionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSQuestionnaire).Copy();
              if (DataTable_PROMSQuestionnaire.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_PROMSQuestionnaire.Rows)
                {
                  Session["QuestionnaireList"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_List"];
                }
              }
              else
              {
                Session["QuestionnaireList"] = "";
              }
            }
          }

          Session["QuestionParent"] = "";
          Session["QuestionId"] = "";
          Session["Question"] = "";
          Session["RowCount"] = "";
          string SQLStringQuestionnaire = "SELECT DISTINCT B.ListItem_Parent AS QuestionParent ,B.ListItem_Id AS QuestionId ,B.ListItem_Name AS Question ,RANK() OVER (ORDER BY CAST(LEFT(B.ListItem_Name,CHARINDEX('.',B.ListItem_Name,0) - 1) AS INT)) AS [RowCount] FROM Administration_ListItem AS B RIGHT OUTER JOIN Administration_ListItem AS A ON B.ListItem_Parent = A.ListItem_Id WHERE A.ListCategory_Id IN (79) AND B.ListCategory_Id IN (90) AND B.ListItem_Parent = @Id";
          using (SqlCommand SqlCommand_Questionnaire = new SqlCommand(SQLStringQuestionnaire))
          {
            SqlCommand_Questionnaire.Parameters.AddWithValue("@Id", Session["QuestionnaireList"].ToString());
            DataTable DataTable_Questionnaire;
            using (DataTable_Questionnaire = new DataTable())
            {
              DataTable_Questionnaire.Locale = CultureInfo.CurrentCulture;
              DataTable_Questionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questionnaire).Copy();
              if (DataTable_Questionnaire.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Questionnaire.Rows)
                {
                  Session["QuestionParent"] = DataRow_Row["QuestionParent"];
                  Session["QuestionId"] = DataRow_Row["QuestionId"];
                  Session["Question"] = DataRow_Row["Question"];
                  Session["RowCount"] = DataRow_Row["RowCount"];

                  Label Label_InsertQ = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_InsertHeadingQ" + Session["RowCount"].ToString() + "");
                  Label_InsertQ.Text = Session["Question"].ToString();

                  RadioButtonList RadioButtonList_InsertQ = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ" + Session["RowCount"].ToString() + "");
                  RadioButtonList_InsertQ.Items.Clear();

                  Session["AnswerParent"] = "";
                  Session["AnswerId"] = "";
                  Session["Answer"] = "";
                  Session["Score"] = "";
                  string SQLStringQuestions = "SELECT DISTINCT C.ListItem_Parent AS AnswerParent ,C.ListItem_Id AS AnswerId ,C.ListItem_Name AS Answer ,D.ListItem_Name AS Score FROM Administration_ListItem AS D RIGHT OUTER JOIN Administration_ListItem AS C ON D.ListItem_Parent = C.ListItem_Id WHERE D.ListCategory_Id IN (92) AND C.ListItem_Parent = @Id ORDER BY D.ListItem_Name DESC";
                  using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
                  {
                    SqlCommand_Questions.Parameters.AddWithValue("@Id", Session["QuestionId"].ToString());
                    DataTable DataTable_Questions;
                    using (DataTable_Questions = new DataTable())
                    {
                      DataTable_Questions.Locale = CultureInfo.CurrentCulture;
                      DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
                      if (DataTable_Questions.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_RowQuestions in DataTable_Questions.Rows)
                        {
                          Session["AnswerParent"] = DataRow_RowQuestions["AnswerParent"];
                          Session["AnswerId"] = DataRow_RowQuestions["AnswerId"];
                          Session["Answer"] = DataRow_RowQuestions["Answer"];
                          Session["Score"] = DataRow_RowQuestions["Score"];

                          ListItem EditQ = new ListItem();
                          EditQ.Text = Session["Answer"].ToString();
                          EditQ.Value = Session["AnswerId"].ToString();

                          RadioButtonList_InsertQ.Items.Add(EditQ);
                        }
                      }
                    }
                  }
                }
              }
              else
              {
                Session["QuestionParent"] = "";
                Session["QuestionId"] = "";
                Session["Question"] = "";
                Session["RowCount"] = "";

                for (int a = 1; a <= TotalQuestions; a++)
                {
                  Label Label_InsertQ = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_InsertHeadingQ" + a + "");
                  Label_InsertQ.Text = "";

                  RadioButtonList RadioButtonList_InsertQ = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_InsertQ" + a + "");
                  RadioButtonList_InsertQ.Items.Clear();
                }
              }
            }
          }

          Session["QuestionnaireList"] = "";
        }
      }
    }

    protected void HiddenField_EditFollowUpTotalQuestions_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_EditFollowUpTotalQuestions = (HiddenField)sender;
      HiddenField_EditFollowUpTotalQuestions.Value = TotalQuestions.ToString(CultureInfo.CurrentCulture);

      if (FormView_PROMS_FollowUp_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["PROMS_Questionnaire_Id"] != null)
        {
          Session["QuestionnaireList"] = "";
          string SQLStringQuestionnaireList = "SELECT PROMS_Questionnaire_Questionnaire_List FROM InfoQuest_Form_PROMS_Questionnaire WHERE PROMS_Questionnaire_Id = @PROMS_Questionnaire_Id";
          using (SqlCommand SqlCommand_QuestionnaireList = new SqlCommand(SQLStringQuestionnaireList))
          {
            SqlCommand_QuestionnaireList.Parameters.AddWithValue("@PROMS_Questionnaire_Id", Request.QueryString["PROMS_Questionnaire_Id"]);
            DataTable DataTable_QuestionnaireList;
            using (DataTable_QuestionnaireList = new DataTable())
            {
              DataTable_QuestionnaireList.Locale = CultureInfo.CurrentCulture;
              DataTable_QuestionnaireList = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QuestionnaireList).Copy();
              if (DataTable_QuestionnaireList.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_QuestionnaireList.Rows)
                {
                  Session["QuestionnaireList"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_List"];
                }
              }
              else
              {
                Session["QuestionnaireList"] = "";
              }
            }
          }

          Session["QuestionParent"] = "";
          Session["QuestionId"] = "";
          Session["Question"] = "";
          Session["RowCount"] = "";
          string SQLStringQuestionnaire = "SELECT DISTINCT B.ListItem_Parent AS QuestionParent ,B.ListItem_Id AS QuestionId ,B.ListItem_Name AS Question ,RANK() OVER (ORDER BY CAST(LEFT(B.ListItem_Name,CHARINDEX('.',B.ListItem_Name,0) - 1) AS INT)) AS [RowCount] FROM Administration_ListItem AS B RIGHT OUTER JOIN Administration_ListItem AS A ON B.ListItem_Parent = A.ListItem_Id WHERE A.ListCategory_Id IN (79) AND B.ListCategory_Id IN (90) AND B.ListItem_Parent = @Id";
          using (SqlCommand SqlCommand_Questionnaire = new SqlCommand(SQLStringQuestionnaire))
          {
            SqlCommand_Questionnaire.Parameters.AddWithValue("@Id", Session["QuestionnaireList"].ToString());
            DataTable DataTable_Questionnaire;
            using (DataTable_Questionnaire = new DataTable())
            {
              DataTable_Questionnaire.Locale = CultureInfo.CurrentCulture;
              DataTable_Questionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questionnaire).Copy();
              if (DataTable_Questionnaire.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Questionnaire.Rows)
                {
                  Session["QuestionParent"] = DataRow_Row["QuestionParent"];
                  Session["QuestionId"] = DataRow_Row["QuestionId"];
                  Session["Question"] = DataRow_Row["Question"];
                  Session["RowCount"] = DataRow_Row["RowCount"];

                  Label Label_EditQ = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_EditHeadingQ" + Session["RowCount"].ToString() + "");
                  Label_EditQ.Text = Session["Question"].ToString();

                  RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ" + Session["RowCount"].ToString() + "");
                  RadioButtonList_EditQ.Items.Clear();

                  Session["AnswerParent"] = "";
                  Session["AnswerId"] = "";
                  Session["Answer"] = "";
                  Session["Score"] = "";

                  string SQLStringQuestions = "SELECT DISTINCT C.ListItem_Parent AS AnswerParent ,C.ListItem_Id AS AnswerId ,C.ListItem_Name AS Answer ,D.ListItem_Name AS Score FROM Administration_ListItem AS D RIGHT OUTER JOIN Administration_ListItem AS C ON D.ListItem_Parent = C.ListItem_Id WHERE D.ListCategory_Id IN (92) AND C.ListItem_Parent = @Id ORDER BY D.ListItem_Name DESC";
                  using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
                  {
                    SqlCommand_Questions.Parameters.AddWithValue("@Id", Session["QuestionId"].ToString());
                    DataTable DataTable_Questions;
                    using (DataTable_Questions = new DataTable())
                    {
                      DataTable_Questions.Locale = CultureInfo.CurrentCulture;
                      DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
                      if (DataTable_Questions.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_RowQuestions in DataTable_Questions.Rows)
                        {
                          Session["AnswerParent"] = DataRow_RowQuestions["AnswerParent"];
                          Session["AnswerId"] = DataRow_RowQuestions["AnswerId"];
                          Session["Answer"] = DataRow_RowQuestions["Answer"];
                          Session["Score"] = DataRow_RowQuestions["Score"];

                          ListItem EditQ = new ListItem();
                          EditQ.Text = Session["Answer"].ToString();
                          EditQ.Value = Session["AnswerId"].ToString();

                          RadioButtonList_EditQ.Items.Add(EditQ);
                        }
                      }
                    }
                  }

                  ListItem EditQNull = new ListItem();
                  EditQNull.Text = Convert.ToString("null", CultureInfo.CurrentCulture);
                  EditQNull.Value = "";
                  RadioButtonList_EditQ.Items.Add(EditQNull);

                }
              }
              else
              {
                Session["QuestionParent"] = "";
                Session["QuestionId"] = "";
                Session["Question"] = "";
                Session["RowCount"] = "";

                for (int a = 1; a <= TotalQuestions; a++)
                {
                  Label Label_EditQ = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_EditHeadingQ" + a + "");
                  Label_EditQ.Text = "";

                  RadioButtonList RadioButtonList_EditQ = (RadioButtonList)FormView_PROMS_FollowUp_Form.FindControl("RadioButtonList_EditQ" + a + "");
                  RadioButtonList_EditQ.Items.Clear();
                }
              }
            }
          }

          Session["QuestionnaireList"] = "";
        }
      }
    }

    protected void HiddenField_ItemFollowUpTotalQuestions_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_ItemFollowUpTotalQuestions = (HiddenField)sender;
      HiddenField_ItemFollowUpTotalQuestions.Value = TotalQuestions.ToString(CultureInfo.CurrentCulture);

      if (FormView_PROMS_FollowUp_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["PROMS_FollowUp_Id"] != null)
        {
          Session["PROMSFollowUpQ1Name"] = "";
          Session["PROMSFollowUpQ2Name"] = "";
          Session["PROMSFollowUpQ3Name"] = "";
          Session["PROMSFollowUpQ4Name"] = "";
          Session["PROMSFollowUpQ5Name"] = "";
          Session["PROMSFollowUpQ6Name"] = "";
          Session["PROMSFollowUpQ7Name"] = "";
          Session["PROMSFollowUpQ8Name"] = "";
          Session["PROMSFollowUpQ9Name"] = "";
          Session["PROMSFollowUpQ10Name"] = "";
          Session["PROMSFollowUpQ11Name"] = "";
          Session["PROMSFollowUpQ12Name"] = "";
          Session["PROMSFollowUpCancelledName"] = "";
          string SQLStringPROMSFollowUp = "SELECT PROMS_FollowUp_Q1_Name ,PROMS_FollowUp_Q2_Name , PROMS_FollowUp_Q3_Name ,PROMS_FollowUp_Q4_Name ,PROMS_FollowUp_Q5_Name ,PROMS_FollowUp_Q6_Name ,PROMS_FollowUp_Q7_Name ,PROMS_FollowUp_Q8_Name ,PROMS_FollowUp_Q9_Name ,PROMS_FollowUp_Q10_Name ,PROMS_FollowUp_Q11_Name ,PROMS_FollowUp_Q12_Name ,PROMS_FollowUp_Cancelled_Name FROM vForm_PROMS_FollowUp WHERE PROMS_FollowUp_Id = @PROMS_FollowUp_Id";
          using (SqlCommand SqlCommand_PROMSFollowUp = new SqlCommand(SQLStringPROMSFollowUp))
          {
            SqlCommand_PROMSFollowUp.Parameters.AddWithValue("@PROMS_FollowUp_Id", Request.QueryString["PROMS_FollowUp_Id"]);
            DataTable DataTable_PROMSFollowUp;
            using (DataTable_PROMSFollowUp = new DataTable())
            {
              DataTable_PROMSFollowUp.Locale = CultureInfo.CurrentCulture;
              DataTable_PROMSFollowUp = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSFollowUp).Copy();
              if (DataTable_PROMSFollowUp.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_PROMSFollowUp.Rows)
                {
                  Session["PROMSFollowUpQ1Name"] = DataRow_Row["PROMS_FollowUp_Q1_Name"];
                  Session["PROMSFollowUpQ2Name"] = DataRow_Row["PROMS_FollowUp_Q2_Name"];
                  Session["PROMSFollowUpQ3Name"] = DataRow_Row["PROMS_FollowUp_Q3_Name"];
                  Session["PROMSFollowUpQ4Name"] = DataRow_Row["PROMS_FollowUp_Q4_Name"];
                  Session["PROMSFollowUpQ5Name"] = DataRow_Row["PROMS_FollowUp_Q5_Name"];
                  Session["PROMSFollowUpQ6Name"] = DataRow_Row["PROMS_FollowUp_Q6_Name"];
                  Session["PROMSFollowUpQ7Name"] = DataRow_Row["PROMS_FollowUp_Q7_Name"];
                  Session["PROMSFollowUpQ8Name"] = DataRow_Row["PROMS_FollowUp_Q8_Name"];
                  Session["PROMSFollowUpQ9Name"] = DataRow_Row["PROMS_FollowUp_Q9_Name"];
                  Session["PROMSFollowUpQ10Name"] = DataRow_Row["PROMS_FollowUp_Q10_Name"];
                  Session["PROMSFollowUpQ11Name"] = DataRow_Row["PROMS_FollowUp_Q11_Name"];
                  Session["PROMSFollowUpQ12Name"] = DataRow_Row["PROMS_FollowUp_Q12_Name"];
                  Session["PROMSFollowUpCancelledName"] = DataRow_Row["PROMS_FollowUp_Cancelled_Name"];
                }
              }
            }
          }

          for (int a = 1; a <= TotalQuestions; a++)
          {
            Label Label_ItemQ = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_ItemQ" + a + "");
            Label_ItemQ.Text = Session["PROMSFollowUpQ" + a + "Name"].ToString();
          }

          Label Label_ItemCancelledList = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_ItemCancelledList");
          Label_ItemCancelledList.Text = Session["PROMSFollowUpCancelledName"].ToString();

          Session["PROMSQuestionnaireQuestionnaireList"] = "";
          string SQLStringPROMSQuestionnaire = "SELECT PROMS_Questionnaire_Questionnaire_List FROM vForm_PROMS_Questionnaire WHERE PROMS_Questionnaire_Id = @PROMS_Questionnaire_Id";
          using (SqlCommand SqlCommand_PROMSQuestionnaire = new SqlCommand(SQLStringPROMSQuestionnaire))
          {
            SqlCommand_PROMSQuestionnaire.Parameters.AddWithValue("@PROMS_Questionnaire_Id", Request.QueryString["PROMS_Questionnaire_Id"]);
            DataTable DataTable_PROMSQuestionnaire;
            using (DataTable_PROMSQuestionnaire = new DataTable())
            {
              DataTable_PROMSQuestionnaire.Locale = CultureInfo.CurrentCulture;
              DataTable_PROMSQuestionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PROMSQuestionnaire).Copy();
              if (DataTable_PROMSQuestionnaire.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_PROMSQuestionnaire.Rows)
                {
                  Session["PROMSQuestionnaireQuestionnaireList"] = DataRow_Row["PROMS_Questionnaire_Questionnaire_List"];
                }
              }
              else
              {
                Session["PROMSQuestionnaireQuestionnaireList"] = "";
              }
            }
          }

          Session["QuestionParent"] = "";
          Session["QuestionId"] = "";
          Session["Question"] = "";
          Session["RowCount"] = "";
          string SQLStringQuestionnaire = "SELECT DISTINCT B.ListItem_Parent AS QuestionParent ,B.ListItem_Id AS QuestionId ,B.ListItem_Name AS Question ,RANK() OVER (ORDER BY CAST(LEFT(B.ListItem_Name,CHARINDEX('.',B.ListItem_Name,0) - 1) AS INT)) AS [RowCount] FROM Administration_ListItem AS B RIGHT OUTER JOIN Administration_ListItem AS A ON B.ListItem_Parent = A.ListItem_Id WHERE A.ListCategory_Id IN (79) AND B.ListCategory_Id IN (90) AND B.ListItem_Parent = @Id";
          using (SqlCommand SqlCommand_Questionnaire = new SqlCommand(SQLStringQuestionnaire))
          {
            SqlCommand_Questionnaire.Parameters.AddWithValue("@Id", Session["PROMSQuestionnaireQuestionnaireList"].ToString());
            DataTable DataTable_Questionnaire;
            using (DataTable_Questionnaire = new DataTable())
            {
              DataTable_Questionnaire.Locale = CultureInfo.CurrentCulture;
              DataTable_Questionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questionnaire).Copy();
              if (DataTable_Questionnaire.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Questionnaire.Rows)
                {
                  Session["QuestionParent"] = DataRow_Row["QuestionParent"];
                  Session["QuestionId"] = DataRow_Row["QuestionId"];
                  Session["Question"] = DataRow_Row["Question"];
                  Session["RowCount"] = DataRow_Row["RowCount"];

                  Label Label_InsertQ = (Label)FormView_PROMS_FollowUp_Form.FindControl("Label_ItemHeadingQ" + Session["RowCount"].ToString() + "");
                  Label_InsertQ.Text = Session["Question"].ToString();
                }
              }
            }
          }

          Session["PROMSQuestionnaireQuestionnaireList"] = "";
          Session["PROMSFollowUpQ1Name"] = "";
          Session["PROMSFollowUpQ2Name"] = "";
          Session["PROMSFollowUpQ3Name"] = "";
          Session["PROMSFollowUpQ4Name"] = "";
          Session["PROMSFollowUpQ5Name"] = "";
          Session["PROMSFollowUpQ6Name"] = "";
          Session["PROMSFollowUpQ7Name"] = "";
          Session["PROMSFollowUpQ8Name"] = "";
          Session["PROMSFollowUpQ9Name"] = "";
          Session["PROMSFollowUpQ10Name"] = "";
          Session["PROMSFollowUpQ11Name"] = "";
          Session["PROMSFollowUpQ12Name"] = "";
          Session["PROMSFollowUpCancelledName"] = "";
        }
      }
    }
    //---END--- --Table_PROMS_FollowUp--//
    //---END--- --TableForm--// 
  }
}
