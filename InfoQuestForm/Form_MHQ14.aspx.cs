using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_MHQ14 : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_MHQ14, this.GetType(), "UpdateProgress_Start", "Validation_Search();Validation_Form();Calculation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("34")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("34").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_PatientInfoHeading.Text = Convert.ToString("Patient Information", CultureInfo.CurrentCulture);
          Label_QuestionnaireHeading.Text = Convert.ToString("Questionnaire", CultureInfo.CurrentCulture);

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_MHQ14_PatientVisitNumber"] != null)
          {
            if (Request.QueryString["MHQ14_Questionnaire_Id"] == null)
            {
              Session["MHQ14QuestionnaireId"] = "";
              string SQLStringMHQ14QuestionnaireId = "SELECT MHQ14_Questionnaire_Id FROM InfoQuest_Form_MHQ14_Questionnaire WHERE Facility_Id = @Facility_Id AND MHQ14_Questionnaire_PatientVisitNumber = @MHQ14_Questionnaire_PatientVisitNumber";
              using (SqlCommand SqlCommand_MHQ14QuestionnaireId = new SqlCommand(SQLStringMHQ14QuestionnaireId))
              {
                SqlCommand_MHQ14QuestionnaireId.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_MHQ14QuestionnaireId.Parameters.AddWithValue("@MHQ14_Questionnaire_PatientVisitNumber", Request.QueryString["s_MHQ14_PatientVisitNumber"]);
                DataTable DataTable_MHQ14QuestionnaireId;
                using (DataTable_MHQ14QuestionnaireId = new DataTable())
                {
                  DataTable_MHQ14QuestionnaireId.Locale = CultureInfo.CurrentCulture;
                  DataTable_MHQ14QuestionnaireId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHQ14QuestionnaireId).Copy();
                  if (DataTable_MHQ14QuestionnaireId.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_MHQ14QuestionnaireId.Rows)
                    {
                      Session["MHQ14QuestionnaireId"] = DataRow_Row["MHQ14_Questionnaire_Id"];
                    }
                  }
                }
              }

              if (!string.IsNullOrEmpty(Session["MHQ14QuestionnaireId"].ToString()))
              {
                Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("MHQ14", "Form_MHQ14.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_MHQ14_PatientVisitNumber=" + Request.QueryString["s_MHQ14_PatientVisitNumber"] + "&MHQ14_Questionnaire_Id=" + Session["MHQ14QuestionnaireId"].ToString() + ""), false);
                Response.End();
              }
            }
            else
            {
              SqlDataSource_MHQ14_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
              SqlDataSource_MHQ14_Facility.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_MHQ14_Questionnaire";
              SqlDataSource_MHQ14_Facility.SelectParameters["TableWHERE"].DefaultValue = "MHQ14_Questionnaire_Id = " + Request.QueryString["MHQ14_Questionnaire_Id"] + "";
            }

            TablePatientInfo.Visible = true;
            TableForm.Visible = true;

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
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('34'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('34')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("34"); 

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_MHQ14.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_MHQ14_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHQ14_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_MHQ14_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MHQ14_Facility.SelectParameters.Clear();
      SqlDataSource_MHQ14_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_MHQ14_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "34");
      SqlDataSource_MHQ14_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_MHQ14_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_MHQ14_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_MHQ14_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_MHQ14_InsertQuestionnaireADMSDiagnosisQ4List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHQ14_InsertQuestionnaireADMSDiagnosisQ4List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 34 AND ListCategory_Id = 107 AND ListItem_Parent = -1 ) AS TempTableAll ORDER BY CASE WHEN LEFT(ListItem_Name,5) = 'Other' THEN 'ZZZ' + ListItem_Name ELSE ListItem_Name END";
      SqlDataSource_MHQ14_InsertQuestionnaireADMSDiagnosisQ4List.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_MHQ14_EditQuestionnaireADMSDiagnosisQ4List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHQ14_EditQuestionnaireADMSDiagnosisQ4List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 34 AND ListCategory_Id = 107 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_MHQ14_Questionnaire.MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List , Administration_ListItem.ListItem_Name FROM InfoQuest_Form_MHQ14_Questionnaire , Administration_ListItem WHERE InfoQuest_Form_MHQ14_Questionnaire.MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_MHQ14_Questionnaire.MHQ14_Questionnaire_Id = @MHQ14_Questionnaire_Id ) AS TempTableAll ORDER BY CASE WHEN LEFT(ListItem_Name,5) = 'Other' THEN 'ZZZ' + ListItem_Name ELSE ListItem_Name END";
      SqlDataSource_MHQ14_EditQuestionnaireADMSDiagnosisQ4List.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_MHQ14_EditQuestionnaireADMSDiagnosisQ4List.SelectParameters.Clear();
      SqlDataSource_MHQ14_EditQuestionnaireADMSDiagnosisQ4List.SelectParameters.Add("MHQ14_Questionnaire_Id", TypeCode.String, Request.QueryString["MHQ14_Questionnaire_Id"]);

      SqlDataSource_MHQ14_EditQuestionnaireDISCHDiagnosisQ4List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHQ14_EditQuestionnaireDISCHDiagnosisQ4List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 34 AND ListCategory_Id = 107 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_MHQ14_Questionnaire.MHQ14_Questionnaire_DISCH_Diagnosis_Q4_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_MHQ14_Questionnaire , Administration_ListItem WHERE InfoQuest_Form_MHQ14_Questionnaire.MHQ14_Questionnaire_DISCH_Diagnosis_Q4_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_MHQ14_Questionnaire.MHQ14_Questionnaire_Id = @MHQ14_Questionnaire_Id ) AS TempTableAll ORDER BY CASE WHEN LEFT(ListItem_Name,5) = 'Other' THEN 'ZZZ' + ListItem_Name ELSE ListItem_Name END";
      SqlDataSource_MHQ14_EditQuestionnaireDISCHDiagnosisQ4List.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_MHQ14_EditQuestionnaireDISCHDiagnosisQ4List.SelectParameters.Clear();
      SqlDataSource_MHQ14_EditQuestionnaireDISCHDiagnosisQ4List.SelectParameters.Add("MHQ14_Questionnaire_Id", TypeCode.String, Request.QueryString["MHQ14_Questionnaire_Id"]);

      SqlDataSource_MHQ14_EditQuestionnaireDISCHNoDischargeReasonList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHQ14_EditQuestionnaireDISCHNoDischargeReasonList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 34 AND ListCategory_Id = 106 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_MHQ14_Questionnaire.MHQ14_Questionnaire_DISCH_NoDischargeReason_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_MHQ14_Questionnaire , Administration_ListItem WHERE InfoQuest_Form_MHQ14_Questionnaire.MHQ14_Questionnaire_DISCH_NoDischargeReason_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_MHQ14_Questionnaire.MHQ14_Questionnaire_Id = @MHQ14_Questionnaire_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_MHQ14_EditQuestionnaireDISCHNoDischargeReasonList.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_MHQ14_EditQuestionnaireDISCHNoDischargeReasonList.SelectParameters.Clear();
      SqlDataSource_MHQ14_EditQuestionnaireDISCHNoDischargeReasonList.SelectParameters.Add("MHQ14_Questionnaire_Id", TypeCode.String, Request.QueryString["MHQ14_Questionnaire_Id"]);

      SqlDataSource_MHQ14_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHQ14_Form.InsertCommand="INSERT INTO [InfoQuest_Form_MHQ14_Questionnaire] (Facility_Id ,	MHQ14_Questionnaire_PatientVisitNumber ,	MHQ14_Questionnaire_ReportNumber ,	MHQ14_Questionnaire_ADMS_Date ,	MHQ14_Questionnaire_ADMS_Diagnosis_Q1 ,	MHQ14_Questionnaire_ADMS_Diagnosis_Q2 ,	MHQ14_Questionnaire_ADMS_Diagnosis_Q3 ,	MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List ,	MHQ14_Questionnaire_ADMS_Q1A ,	MHQ14_Questionnaire_ADMS_Q1B ,	MHQ14_Questionnaire_ADMS_Q1C ,	MHQ14_Questionnaire_ADMS_Q1D ,	MHQ14_Questionnaire_ADMS_Q1E , MHQ14_Questionnaire_ADMS_Section1Score ,	MHQ14_Questionnaire_ADMS_Q2A ,	MHQ14_Questionnaire_ADMS_Q2B ,	MHQ14_Questionnaire_ADMS_Q2C ,	MHQ14_Questionnaire_ADMS_Q2D , MHQ14_Questionnaire_ADMS_Section2Score ,	MHQ14_Questionnaire_ADMS_Q3A ,	MHQ14_Questionnaire_ADMS_Q3B , MHQ14_Questionnaire_ADMS_Section3Score ,	MHQ14_Questionnaire_ADMS_Q4A ,	MHQ14_Questionnaire_ADMS_Q4B ,	MHQ14_Questionnaire_ADMS_Q4C , MHQ14_Questionnaire_ADMS_Section4Score ,	MHQ14_Questionnaire_ADMS_Score ,	MHQ14_Questionnaire_CreatedDate ,	MHQ14_Questionnaire_CreatedBy ,	MHQ14_Questionnaire_ModifiedDate ,	MHQ14_Questionnaire_ModifiedBy ,	MHQ14_Questionnaire_History ,	MHQ14_Questionnaire_IsActive) VALUES (@Facility_Id ,	@MHQ14_Questionnaire_PatientVisitNumber ,	@MHQ14_Questionnaire_ReportNumber ,	@MHQ14_Questionnaire_ADMS_Date , @MHQ14_Questionnaire_ADMS_Diagnosis_Q1 ,	@MHQ14_Questionnaire_ADMS_Diagnosis_Q2 ,	@MHQ14_Questionnaire_ADMS_Diagnosis_Q3 ,	@MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List ,	@MHQ14_Questionnaire_ADMS_Q1A ,	@MHQ14_Questionnaire_ADMS_Q1B ,	@MHQ14_Questionnaire_ADMS_Q1C ,	@MHQ14_Questionnaire_ADMS_Q1D ,	@MHQ14_Questionnaire_ADMS_Q1E , @MHQ14_Questionnaire_ADMS_Section1Score ,	@MHQ14_Questionnaire_ADMS_Q2A ,	@MHQ14_Questionnaire_ADMS_Q2B ,	@MHQ14_Questionnaire_ADMS_Q2C ,	@MHQ14_Questionnaire_ADMS_Q2D , @MHQ14_Questionnaire_ADMS_Section2Score ,	@MHQ14_Questionnaire_ADMS_Q3A ,	@MHQ14_Questionnaire_ADMS_Q3B , @MHQ14_Questionnaire_ADMS_Section3Score ,	@MHQ14_Questionnaire_ADMS_Q4A ,	@MHQ14_Questionnaire_ADMS_Q4B ,	@MHQ14_Questionnaire_ADMS_Q4C , @MHQ14_Questionnaire_ADMS_Section4Score ,	@MHQ14_Questionnaire_ADMS_Score ,	@MHQ14_Questionnaire_CreatedDate ,	@MHQ14_Questionnaire_CreatedBy ,	@MHQ14_Questionnaire_ModifiedDate ,	@MHQ14_Questionnaire_ModifiedBy ,	@MHQ14_Questionnaire_History ,	@MHQ14_Questionnaire_IsActive); SELECT @MHQ14_Questionnaire_Id = SCOPE_IDENTITY()";
      SqlDataSource_MHQ14_Form.SelectCommand="SELECT * FROM [InfoQuest_Form_MHQ14_Questionnaire] WHERE ([MHQ14_Questionnaire_Id] = @MHQ14_Questionnaire_Id)";
      SqlDataSource_MHQ14_Form.UpdateCommand="UPDATE [InfoQuest_Form_MHQ14_Questionnaire] SET	MHQ14_Questionnaire_ADMS_Date = @MHQ14_Questionnaire_ADMS_Date ,	MHQ14_Questionnaire_ADMS_Diagnosis_Q1 = @MHQ14_Questionnaire_ADMS_Diagnosis_Q1 ,	MHQ14_Questionnaire_ADMS_Diagnosis_Q2 = @MHQ14_Questionnaire_ADMS_Diagnosis_Q2 ,	MHQ14_Questionnaire_ADMS_Diagnosis_Q3 = @MHQ14_Questionnaire_ADMS_Diagnosis_Q3 ,	MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List = @MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List ,	MHQ14_Questionnaire_ADMS_Q1A = @MHQ14_Questionnaire_ADMS_Q1A ,	MHQ14_Questionnaire_ADMS_Q1B = @MHQ14_Questionnaire_ADMS_Q1B ,	MHQ14_Questionnaire_ADMS_Q1C = @MHQ14_Questionnaire_ADMS_Q1C ,	MHQ14_Questionnaire_ADMS_Q1D = @MHQ14_Questionnaire_ADMS_Q1D ,	MHQ14_Questionnaire_ADMS_Q1E = @MHQ14_Questionnaire_ADMS_Q1E , MHQ14_Questionnaire_ADMS_Section1Score = @MHQ14_Questionnaire_ADMS_Section1Score ,	MHQ14_Questionnaire_ADMS_Q2A = @MHQ14_Questionnaire_ADMS_Q2A ,	MHQ14_Questionnaire_ADMS_Q2B = @MHQ14_Questionnaire_ADMS_Q2B ,	MHQ14_Questionnaire_ADMS_Q2C = @MHQ14_Questionnaire_ADMS_Q2C ,	MHQ14_Questionnaire_ADMS_Q2D = @MHQ14_Questionnaire_ADMS_Q2D , MHQ14_Questionnaire_ADMS_Section2Score = @MHQ14_Questionnaire_ADMS_Section2Score ,	MHQ14_Questionnaire_ADMS_Q3A = @MHQ14_Questionnaire_ADMS_Q3A ,	MHQ14_Questionnaire_ADMS_Q3B = @MHQ14_Questionnaire_ADMS_Q3B , MHQ14_Questionnaire_ADMS_Section3Score = @MHQ14_Questionnaire_ADMS_Section3Score  ,	MHQ14_Questionnaire_ADMS_Q4A = @MHQ14_Questionnaire_ADMS_Q4A ,	MHQ14_Questionnaire_ADMS_Q4B = @MHQ14_Questionnaire_ADMS_Q4B ,	MHQ14_Questionnaire_ADMS_Q4C = @MHQ14_Questionnaire_ADMS_Q4C , MHQ14_Questionnaire_ADMS_Section4Score = @MHQ14_Questionnaire_ADMS_Section4Score ,	MHQ14_Questionnaire_ADMS_Score = @MHQ14_Questionnaire_ADMS_Score ,	MHQ14_Questionnaire_DISCH_CompleteDischarge = @MHQ14_Questionnaire_DISCH_CompleteDischarge ,	MHQ14_Questionnaire_DISCH_NoDischargeReason_List = @MHQ14_Questionnaire_DISCH_NoDischargeReason_List ,	MHQ14_Questionnaire_DISCH_Date = @MHQ14_Questionnaire_DISCH_Date , MHQ14_Questionnaire_DISCH_Diagnosis_Q1 = @MHQ14_Questionnaire_DISCH_Diagnosis_Q1 ,	MHQ14_Questionnaire_DISCH_Diagnosis_Q2 = @MHQ14_Questionnaire_DISCH_Diagnosis_Q2 ,	MHQ14_Questionnaire_DISCH_Diagnosis_Q3 = @MHQ14_Questionnaire_DISCH_Diagnosis_Q3 ,	MHQ14_Questionnaire_DISCH_Diagnosis_Q4_List = @MHQ14_Questionnaire_DISCH_Diagnosis_Q4_List ,	MHQ14_Questionnaire_DISCH_Q1A = @MHQ14_Questionnaire_DISCH_Q1A ,	MHQ14_Questionnaire_DISCH_Q1B = @MHQ14_Questionnaire_DISCH_Q1B ,	MHQ14_Questionnaire_DISCH_Q1C = @MHQ14_Questionnaire_DISCH_Q1C ,	MHQ14_Questionnaire_DISCH_Q1D = @MHQ14_Questionnaire_DISCH_Q1D ,	MHQ14_Questionnaire_DISCH_Q1E = @MHQ14_Questionnaire_DISCH_Q1E , MHQ14_Questionnaire_DISCH_Section1Score = @MHQ14_Questionnaire_DISCH_Section1Score ,	MHQ14_Questionnaire_DISCH_Q2A = @MHQ14_Questionnaire_DISCH_Q2A ,	MHQ14_Questionnaire_DISCH_Q2B = @MHQ14_Questionnaire_DISCH_Q2B ,	MHQ14_Questionnaire_DISCH_Q2C = @MHQ14_Questionnaire_DISCH_Q2C ,	MHQ14_Questionnaire_DISCH_Q2D = @MHQ14_Questionnaire_DISCH_Q2D , MHQ14_Questionnaire_DISCH_Section2Score = @MHQ14_Questionnaire_DISCH_Section2Score ,	MHQ14_Questionnaire_DISCH_Q3A = @MHQ14_Questionnaire_DISCH_Q3A ,	MHQ14_Questionnaire_DISCH_Q3B = @MHQ14_Questionnaire_DISCH_Q3B , MHQ14_Questionnaire_DISCH_Section3Score = @MHQ14_Questionnaire_DISCH_Section3Score ,	MHQ14_Questionnaire_DISCH_Q4A = @MHQ14_Questionnaire_DISCH_Q4A ,	MHQ14_Questionnaire_DISCH_Q4B = @MHQ14_Questionnaire_DISCH_Q4B ,	MHQ14_Questionnaire_DISCH_Q4C = @MHQ14_Questionnaire_DISCH_Q4C , MHQ14_Questionnaire_DISCH_Section4Score = @MHQ14_Questionnaire_DISCH_Section4Score ,	MHQ14_Questionnaire_DISCH_Score = @MHQ14_Questionnaire_DISCH_Score ,	MHQ14_Questionnaire_DISCH_Difference = @MHQ14_Questionnaire_DISCH_Difference ,	MHQ14_Questionnaire_ModifiedDate = @MHQ14_Questionnaire_ModifiedDate ,	MHQ14_Questionnaire_ModifiedBy = @MHQ14_Questionnaire_ModifiedBy ,	MHQ14_Questionnaire_History = @MHQ14_Questionnaire_History ,	MHQ14_Questionnaire_IsActive = @MHQ14_Questionnaire_IsActive WHERE [MHQ14_Questionnaire_Id] = @MHQ14_Questionnaire_Id";
      SqlDataSource_MHQ14_Form.InsertParameters.Clear();
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_Id", TypeCode.Int32, "");
      SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_MHQ14_Form.InsertParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_PatientVisitNumber", TypeCode.String, Request.QueryString["s_MHQ14_PatientVisitNumber"]);
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ReportNumber", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Date", TypeCode.DateTime, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Q1", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Q2", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Q3", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List", TypeCode.Int32, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Psychotic", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Mood", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Stress", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Anxiety", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Alcohol", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Eating", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Personality", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q1A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q1B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q1C", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q1D", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q1E", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Section1Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q2A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q2B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q2C", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q2D", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Section2Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q3A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q3B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Section3Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q4A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q4B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Q4C", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Section4Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ADMS_Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_CreatedBy", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_ModifiedBy", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_History", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_MHQ14_Form.InsertParameters.Add("MHQ14_Questionnaire_IsActive", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.SelectParameters.Clear();
      SqlDataSource_MHQ14_Form.SelectParameters.Add("MHQ14_Questionnaire_Id", TypeCode.Int32, Request.QueryString["MHQ14_Questionnaire_Id"]);
      SqlDataSource_MHQ14_Form.UpdateParameters.Clear();
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Date", TypeCode.DateTime, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Q1", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Q2", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Q3", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Diagnosis_Q4_List", TypeCode.Int32, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q1A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q1B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q1C", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q1D", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q1E", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Section1Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q2A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q2B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q2C", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q2D", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Section2Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q3A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q3B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Section3Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q4A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q4B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Q4C", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Section4Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ADMS_Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_CompleteDischarge", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_NoDischargeReason_List", TypeCode.Int32, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Date", TypeCode.DateTime, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Diagnosis_Q1", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Diagnosis_Q2", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Diagnosis_Q3", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Diagnosis_Q4_List", TypeCode.Int32, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q1A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q1B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q1C", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q1D", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q1E", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Section1Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q2A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q2B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q2C", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q2D", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Section2Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q3A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q3B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Section3Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q4A", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q4B", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Q4C", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Section4Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Score", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_DISCH_Difference", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_ModifiedBy", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_History", TypeCode.String, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_IsActive", TypeCode.Boolean, "");
      SqlDataSource_MHQ14_Form.UpdateParameters.Add("MHQ14_Questionnaire_Id", TypeCode.Int32, "");
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
        if (Request.QueryString["s_MHQ14_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_MHQ14_PatientVisitNumber"];
        }
      }
    }

    private void PatientDataPI()
    {
      DataTable DataTable_PatientDataPI;
      using (DataTable_PatientDataPI = new DataTable())
      {
        DataTable_PatientDataPI.Locale = CultureInfo.CurrentCulture;
        //DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_MHQ14_PatientVisitNumber"]).Copy();
        DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_MHQ14_PatientVisitNumber"]).Copy();

        if (DataTable_PatientDataPI.Columns.Count == 1)
        {
          Session["MHQ14PIId"] = "";
          string SQLStringPatientInfo = "SELECT MHQ14_PI_Id FROM InfoQuest_Form_MHQ14_PatientInformation WHERE Facility_Id = @Facility_Id AND MHQ14_PI_PatientVisitNumber = @MHQ14_PI_PatientVisitNumber";
          using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
          {
            SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
            SqlCommand_PatientInfo.Parameters.AddWithValue("@MHQ14_PI_PatientVisitNumber", Request.QueryString["s_MHQ14_PatientVisitNumber"]);
            DataTable DataTable_PatientInfo;
            using (DataTable_PatientInfo = new DataTable())
            {
              DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
              DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
              if (DataTable_PatientInfo.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                {
                  Session["MHQ14PIId"] = DataRow_Row1["MHQ14_PI_Id"];
                }
              }
              else
              {
                Session["MHQ14PIId"] = "";
              }
            }
          }

          if (string.IsNullOrEmpty(Session["MHQ14PIId"].ToString()))
          {
            Session["Error"] = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["Error"] = DataRow_Row["Error"];
            }

            Label_InvalidSearchMessage.Text = Session["Error"].ToString();
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            Session["Error"] = "";
          }
          else
          {
            Session["Error"] = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["Error"] = DataRow_Row["Error"];
            }

            Label_InvalidSearchMessage.Text = Session["Error"].ToString() + Convert.ToString("<br />No Patient related data could be updated but you can continue capturing new form(s) and updating and viewing previous form(s)", CultureInfo.CurrentCulture);
            Session["Error"] = "";
          }

          Session["MHQ14PIId"] = "";
        }
        else if (DataTable_PatientDataPI.Columns.Count != 1)
        {
          if (DataTable_PatientDataPI.Rows.Count == 0)
          {
            Label_InvalidSearchMessage.Text = Convert.ToString("Patient Visit Number " + Request.QueryString["s_MHQ14_PatientVisitNumber"] + " does not Exist", CultureInfo.CurrentCulture);
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
          }
          else
          {
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["VisitNumber"] = DataRow_Row["VisitNumber"];
              Session["NameSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];
              Session["Age"] = DataRow_Row["PatientAge"];
              Session["AdmissionDate"] = DataRow_Row["DateOfAdmission"];
              Session["DischargeDate"] = DataRow_Row["DateOfDischarge"];

              string NameSurnamePI = Session["NameSurname"].ToString();
              NameSurnamePI = NameSurnamePI.Replace("'", "");
              Session["NameSurname"] = NameSurnamePI;
              NameSurnamePI = "";

              Session["MHQ14PIId"] = "";
              string SQLStringPatientInfo = "SELECT MHQ14_PI_Id FROM InfoQuest_Form_MHQ14_PatientInformation WHERE Facility_Id = @Facility_Id AND MHQ14_PI_PatientVisitNumber = @MHQ14_PI_PatientVisitNumber";
              using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
              {
                SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_PatientInfo.Parameters.AddWithValue("@MHQ14_PI_PatientVisitNumber", Request.QueryString["s_MHQ14_PatientVisitNumber"]);
                DataTable DataTable_PatientInfo;
                using (DataTable_PatientInfo = new DataTable())
                {
                  DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
                  DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
                  if (DataTable_PatientInfo.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                    {
                      Session["MHQ14PIId"] = DataRow_Row1["MHQ14_PI_Id"];
                    }
                  }
                  else
                  {
                    Session["MHQ14PIId"] = "";
                  }
                }
              }

              if (string.IsNullOrEmpty(Session["MHQ14PIId"].ToString()))
              {
                string SQLStringInsertMHQ14PI = "INSERT INTO InfoQuest_Form_MHQ14_PatientInformation ( Facility_Id , MHQ14_PI_PatientVisitNumber , MHQ14_PI_PatientName , MHQ14_PI_PatientAge , MHQ14_PI_PatientDateOfAdmission , MHQ14_PI_PatientDateofDischarge , MHQ14_PI_Archived ) VALUES  ( @Facility_Id , @MHQ14_PI_PatientVisitNumber , @MHQ14_PI_PatientName , @MHQ14_PI_PatientAge , @MHQ14_PI_PatientDateOfAdmission , @MHQ14_PI_PatientDateofDischarge , @MHQ14_PI_Archived )";
                using (SqlCommand SqlCommand_InsertMHQ14PI = new SqlCommand(SQLStringInsertMHQ14PI))
                {
                  SqlCommand_InsertMHQ14PI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_InsertMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientVisitNumber", Session["VisitNumber"].ToString());
                  SqlCommand_InsertMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientName", Session["NameSurname"].ToString());
                  SqlCommand_InsertMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientAge", Session["Age"].ToString());
                  SqlCommand_InsertMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientDateOfAdmission", Session["AdmissionDate"].ToString());
                  SqlCommand_InsertMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientDateofDischarge", Session["DischargeDate"].ToString());
                  SqlCommand_InsertMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_Archived", 0);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertMHQ14PI);
                }
              }
              else
              {
                string SQLStringUpdateMHQ14PI = "UPDATE InfoQuest_Form_MHQ14_PatientInformation SET MHQ14_PI_PatientName = @MHQ14_PI_PatientName , MHQ14_PI_PatientAge = @MHQ14_PI_PatientAge , MHQ14_PI_PatientDateOfAdmission = @MHQ14_PI_PatientDateOfAdmission , MHQ14_PI_PatientDateofDischarge = @MHQ14_PI_PatientDateofDischarge WHERE Facility_Id = @Facility_Id AND MHQ14_PI_PatientVisitNumber = @MHQ14_PI_PatientVisitNumber ";
                using (SqlCommand SqlCommand_UpdateMHQ14PI = new SqlCommand(SQLStringUpdateMHQ14PI))
                {
                  SqlCommand_UpdateMHQ14PI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_UpdateMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientVisitNumber", Session["VisitNumber"].ToString());
                  SqlCommand_UpdateMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientName", Session["NameSurname"].ToString());
                  SqlCommand_UpdateMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientAge", Session["Age"].ToString());
                  SqlCommand_UpdateMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientDateOfAdmission", Session["AdmissionDate"].ToString());
                  SqlCommand_UpdateMHQ14PI.Parameters.AddWithValue("@MHQ14_PI_PatientDateofDischarge", Session["DischargeDate"].ToString());
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateMHQ14PI);
                }
              }
              Session["MHQ14PIId"] = "";
            }
          }
        }
      }
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('34')) AND (Facility_Id IN (@Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '138'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '139'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '140'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '141'");

            if (Request.QueryString["MHQ14_Questionnaire_Id"] != null)
            {
              Session["ViewUpdate"] = "";
              string SQLStringMHQ14 = @"SELECT	CASE 
					                                        WHEN Facility_Id = 119 AND MHQ14_Questionnaire_CreatedDate >= '2014/10/01' THEN 'Yes' --REMOVE ME LATER
					                                        WHEN MHQ14_Questionnaire_DISCH_Date IS NULL THEN 'Yes' 
					                                        WHEN DATEADD(DAY	,0 - (DAY(DATEADD(MONTH,1,MHQ14_Questionnaire_DISCH_Date))) + (
						                                        SELECT	CASE 
											                                        WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,DATEADD(MONTH,1,MHQ14_Questionnaire_DISCH_Date))+1,0))) 
											                                        ELSE Form_CutOffDay 
										                                        END AS ValidCutOffDay 
						                                        FROM		Administration_Form 
						                                        WHERE		Form_Id = 34),DATEADD(MONTH,1,MHQ14_Questionnaire_DISCH_Date)) >= GETDATE() THEN 'Yes' 
					                                        ELSE 'No' 
				                                        END AS ViewUpdate 
                                        FROM		InfoQuest_Form_MHQ14_Questionnaire 
                                        WHERE		MHQ14_Questionnaire_Id = @MHQ14_Questionnaire_Id";
              using (SqlCommand SqlCommand_MHQ14 = new SqlCommand(SQLStringMHQ14))
              {
                SqlCommand_MHQ14.Parameters.AddWithValue("@MHQ14_Questionnaire_Id", Request.QueryString["MHQ14_Questionnaire_Id"]);
                DataTable DataTable_MHQ14;
                using (DataTable_MHQ14 = new DataTable())
                {
                  DataTable_MHQ14.Locale = CultureInfo.CurrentCulture;
                  DataTable_MHQ14 = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHQ14).Copy();
                  if (DataTable_MHQ14.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_MHQ14.Rows)
                    {
                      Session["ViewUpdate"] = DataRow_Row["ViewUpdate"];
                    }
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              if (Request.QueryString["MHQ14_Questionnaire_Id"] != null)
              {
                FormView_MHQ14_Form.ChangeMode(FormViewMode.Edit);
              }
              else
              {
                FormView_MHQ14_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Session["Security"] = "0";
              FormView_MHQ14_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";
              if (Request.QueryString["MHQ14_Questionnaire_Id"] != null)
              {
                if (Session["ViewUpdate"].ToString() == "Yes")
                {
                  FormView_MHQ14_Form.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                  FormView_MHQ14_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
              else
              {
                FormView_MHQ14_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              FormView_MHQ14_Form.ChangeMode(FormViewMode.ReadOnly);
            }
            Session["Security"] = "1";
          }

          Session["ViewUpdate"] = "";
        }
      }
    }

    private void TablePatientInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["MHQ14PIPatientVisitNumber"] = "";
      Session["MHQ14PIPatientName"] = "";
      Session["MHQ14PIPatientAge"] = "";
      Session["MHQ14PIPatientDateOfAdmission"] = "";
      Session["MHQ14PIPatientDateofDischarge"] = "";

      string SQLStringPatientInfo = "SELECT DISTINCT Facility_FacilityDisplayName , MHQ14_PI_PatientVisitNumber , MHQ14_PI_PatientName , MHQ14_PI_PatientAge , MHQ14_PI_PatientDateOfAdmission , MHQ14_PI_PatientDateofDischarge FROM vForm_MHQ14_PatientInformation WHERE Facility_Id = @Facility_Id AND MHQ14_PI_PatientVisitNumber = @MHQ14_PI_PatientVisitNumber";
      using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
      {
        SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PatientInfo.Parameters.AddWithValue("@MHQ14_PI_PatientVisitNumber", Request.QueryString["s_MHQ14_PatientVisitNumber"]);
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
              Session["MHQ14PIPatientVisitNumber"] = DataRow_Row["MHQ14_PI_PatientVisitNumber"];
              Session["MHQ14PIPatientName"] = DataRow_Row["MHQ14_PI_PatientName"];
              Session["MHQ14PIPatientAge"] = DataRow_Row["MHQ14_PI_PatientAge"];
              Session["MHQ14PIPatientDateOfAdmission"] = DataRow_Row["MHQ14_PI_PatientDateOfAdmission"];
              Session["MHQ14PIPatientDateofDischarge"] = DataRow_Row["MHQ14_PI_PatientDateofDischarge"];
            }
          }
        }
      }

      Label_PIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_PIVisitNumber.Text = Session["MHQ14PIPatientVisitNumber"].ToString();
      Label_PIName.Text = Session["MHQ14PIPatientName"].ToString();
      Label_PIAge.Text = Session["MHQ14PIPatientAge"].ToString();
      Label_PIDateAdmission.Text = Session["MHQ14PIPatientDateOfAdmission"].ToString();
      Label_PIDateDischarge.Text = Session["MHQ14PIPatientDateofDischarge"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["MHQ14PIPatientVisitNumber"] = "";
      Session["MHQ14PIPatientName"] = "";
      Session["MHQ14PIPatientAge"] = "";
      Session["MHQ14PIPatientDateOfAdmission"] = "";
      Session["MHQ14PIPatientDateofDischarge"] = "";
    }

    private void TableFormVisible()
    {
      if (FormView_MHQ14_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_MHQ14_Form.FindControl("TextBox_InsertADMSDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_MHQ14_Form.FindControl("TextBox_InsertADMSDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSDiagnosisQ1")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSDiagnosisQ2")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSDiagnosisQ3")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSDiagnosisQ4List")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1C")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1D")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1E")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2C")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2D")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ3A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ3B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ4A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ4B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ4C")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      }

      if (FormView_MHQ14_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_MHQ14_Form.FindControl("TextBox_EditADMSDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_MHQ14_Form.FindControl("TextBox_EditADMSDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSDiagnosisQ1")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSDiagnosisQ2")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSDiagnosisQ3")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSDiagnosisQ4List")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1C")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1D")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1E")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2C")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2D")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ3A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ3B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ4A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ4B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ4C")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((DropDownList)FormView_MHQ14_Form.FindControl("DropDownList_EditDISCHCompleteDischarge")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((TextBox)FormView_MHQ14_Form.FindControl("TextBox_EditDISCHDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_MHQ14_Form.FindControl("TextBox_EditDISCHDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_MHQ14_Form.FindControl("DropDownList_EditDISCHNoDischargeReasonList")).Attributes.Add("OnChange", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ1")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ2")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ3")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ4List")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1C")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1D")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1E")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2C")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2D")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ3A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ3B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4A")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4C")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        RadioButtonList RadioButtonList_EditDISCHDiagnosisQ1 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ1");
        RadioButtonList_EditDISCHDiagnosisQ1.Items.RemoveAt(RadioButtonList_EditDISCHDiagnosisQ1.Items.IndexOf(RadioButtonList_EditDISCHDiagnosisQ1.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHDiagnosisQ2 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ2");
        RadioButtonList_EditDISCHDiagnosisQ2.Items.RemoveAt(RadioButtonList_EditDISCHDiagnosisQ2.Items.IndexOf(RadioButtonList_EditDISCHDiagnosisQ2.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHDiagnosisQ3 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ3");
        RadioButtonList_EditDISCHDiagnosisQ3.Items.RemoveAt(RadioButtonList_EditDISCHDiagnosisQ3.Items.IndexOf(RadioButtonList_EditDISCHDiagnosisQ3.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHDiagnosisQ4List = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ4List");
        RadioButtonList_EditDISCHDiagnosisQ4List.Items.RemoveAt(RadioButtonList_EditDISCHDiagnosisQ4List.Items.IndexOf(RadioButtonList_EditDISCHDiagnosisQ4List.Items.FindByValue("")));

        RadioButtonList RadioButtonList_EditDISCHQ1A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1A");
        RadioButtonList_EditDISCHQ1A.Items.RemoveAt(RadioButtonList_EditDISCHQ1A.Items.IndexOf(RadioButtonList_EditDISCHQ1A.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ1B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1B");
        RadioButtonList_EditDISCHQ1B.Items.RemoveAt(RadioButtonList_EditDISCHQ1B.Items.IndexOf(RadioButtonList_EditDISCHQ1B.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ1C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1C");
        RadioButtonList_EditDISCHQ1C.Items.RemoveAt(RadioButtonList_EditDISCHQ1C.Items.IndexOf(RadioButtonList_EditDISCHQ1C.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ1D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1D");
        RadioButtonList_EditDISCHQ1D.Items.RemoveAt(RadioButtonList_EditDISCHQ1D.Items.IndexOf(RadioButtonList_EditDISCHQ1D.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ1E = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1E");
        RadioButtonList_EditDISCHQ1E.Items.RemoveAt(RadioButtonList_EditDISCHQ1E.Items.IndexOf(RadioButtonList_EditDISCHQ1E.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ2A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2A");
        RadioButtonList_EditDISCHQ2A.Items.RemoveAt(RadioButtonList_EditDISCHQ2A.Items.IndexOf(RadioButtonList_EditDISCHQ2A.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ2B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2B");
        RadioButtonList_EditDISCHQ2B.Items.RemoveAt(RadioButtonList_EditDISCHQ2B.Items.IndexOf(RadioButtonList_EditDISCHQ2B.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ2C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2C");
        RadioButtonList_EditDISCHQ2C.Items.RemoveAt(RadioButtonList_EditDISCHQ2C.Items.IndexOf(RadioButtonList_EditDISCHQ2C.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ2D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2D");
        RadioButtonList_EditDISCHQ2D.Items.RemoveAt(RadioButtonList_EditDISCHQ2D.Items.IndexOf(RadioButtonList_EditDISCHQ2D.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ3A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ3A");
        RadioButtonList_EditDISCHQ3A.Items.RemoveAt(RadioButtonList_EditDISCHQ3A.Items.IndexOf(RadioButtonList_EditDISCHQ3A.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ3B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ3B");
        RadioButtonList_EditDISCHQ3B.Items.RemoveAt(RadioButtonList_EditDISCHQ3B.Items.IndexOf(RadioButtonList_EditDISCHQ3B.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ4A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4A");
        RadioButtonList_EditDISCHQ4A.Items.RemoveAt(RadioButtonList_EditDISCHQ4A.Items.IndexOf(RadioButtonList_EditDISCHQ4A.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ4B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4B");
        RadioButtonList_EditDISCHQ4B.Items.RemoveAt(RadioButtonList_EditDISCHQ4B.Items.IndexOf(RadioButtonList_EditDISCHQ4B.Items.FindByValue("")));
        RadioButtonList RadioButtonList_EditDISCHQ4C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4C");
        RadioButtonList_EditDISCHQ4C.Items.RemoveAt(RadioButtonList_EditDISCHQ4C.Items.IndexOf(RadioButtonList_EditDISCHQ4C.Items.FindByValue("")));
      }
    }


    //--START-- --Search--//
    protected void Button_GoToRequired_Click(object sender, EventArgs e)
    {
      RedirectToRequired();
    }

    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_MHQ14.aspx";
      Response.Redirect(FinalURL, false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect("Form_MHQ14.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_MHQ14_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "", false);
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

    private void RedirectToRequired()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["SearchR_FacilityId"];
      string SearchField2 = Request.QueryString["SearchR_MHQ14PatientVisitNumber"];
      string SearchField3 = Request.QueryString["SearchR_MHQ14PatientName"];
      string SearchField4 = Request.QueryString["SearchR_MHQ14ReportNumber"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null && SearchField4 == null)
      {
        FinalURL = "Form_MHQ14_Required.aspx";
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_Facility_Id=" + Request.QueryString["SearchR_FacilityId"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_MHQ14_PatientVisitNumber=" + Request.QueryString["SearchR_MHQ14PatientVisitNumber"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_MHQ14_PatientName=" + Request.QueryString["SearchR_MHQ14PatientName"] + "&";
        }

        if (SearchField4 == null)
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_MHQ14_ReportNumber=" + Request.QueryString["SearchR_MHQ14ReportNumber"] + "&";
        }

        string SearchURL = "Form_MHQ14_Required.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }

    private void RedirectToList()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_MHQ14PatientVisitNumber"];
      string SearchField3 = Request.QueryString["Search_MHQ14PatientName"];
      string SearchField4 = Request.QueryString["Search_MHQ14ReportNumber"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null && SearchField4 == null)
      {
        FinalURL = "Form_MHQ14_List.aspx";
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
          SearchField2 = "s_MHQ14_PatientVisitNumber=" + Request.QueryString["Search_MHQ14PatientVisitNumber"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_MHQ14_PatientName=" + Request.QueryString["Search_MHQ14PatientName"] + "&";
        }

        if (SearchField4 == null)
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_MHQ14_ReportNumber=" + Request.QueryString["Search_MHQ14ReportNumber"] + "&";
        }

        string SearchURL = "Form_MHQ14_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --Form--//
    protected void FormView_MHQ14_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["MHQ14QuestionnaireId"] = "";
        Session["MHQ14QuestionnaireCreatedDate"] = "";
        string SQLStringMHQ14QuestionnaireId = "SELECT MHQ14_Questionnaire_Id , MHQ14_Questionnaire_CreatedDate FROM InfoQuest_Form_MHQ14_Questionnaire WHERE Facility_Id = @Facility_Id AND MHQ14_Questionnaire_PatientVisitNumber = @MHQ14_Questionnaire_PatientVisitNumber";
        using (SqlCommand SqlCommand_MHQ14QuestionnaireId = new SqlCommand(SQLStringMHQ14QuestionnaireId))
        {
          SqlCommand_MHQ14QuestionnaireId.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_MHQ14QuestionnaireId.Parameters.AddWithValue("@MHQ14_Questionnaire_PatientVisitNumber", Request.QueryString["s_MHQ14_PatientVisitNumber"]);
          DataTable DataTable_MHQ14QuestionnaireId;
          using (DataTable_MHQ14QuestionnaireId = new DataTable())
          {
            DataTable_MHQ14QuestionnaireId.Locale = CultureInfo.CurrentCulture;
            DataTable_MHQ14QuestionnaireId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHQ14QuestionnaireId).Copy();
            if (DataTable_MHQ14QuestionnaireId.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_MHQ14QuestionnaireId.Rows)
              {
                Session["MHQ14QuestionnaireId"] = DataRow_Row["MHQ14_Questionnaire_Id"];
                Session["MHQ14QuestionnaireCreatedDate"] = DataRow_Row["MHQ14_Questionnaire_CreatedDate"];
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["MHQ14QuestionnaireId"].ToString()))
        {
          e.Cancel = true;
          Page.MaintainScrollPositionOnPostBack = false;

          string Label_InsertConcurrencyInsertMessage = "" +
            "Questionnaire could not be added<br/>" +
            "A Questionnaire has already been captured for this Visit Number at " + Session["MHQ14QuestionnaireCreatedDate"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view Questionnaire<br/><br/>";

          ((Label)FormView_MHQ14_Form.FindControl("Label_InsertInvalidFormMessage")).Text = "";
          ((Label)FormView_MHQ14_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = Convert.ToString(Label_InsertConcurrencyInsertMessage, CultureInfo.CurrentCulture);
        }
        else
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
            ((Label)FormView_MHQ14_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
            ((Label)FormView_MHQ14_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            Session["MHQ14_Questionnaire_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Request.QueryString["s_Facility_Id"], "34");

            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_ReportNumber"].DefaultValue = Session["MHQ14_Questionnaire_ReportNumber"].ToString();
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_CreatedDate"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_History"].DefaultValue = "";
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_IsActive"].DefaultValue = "true";


            decimal ADMSSection1Score = ItemInserting_ADMSSection1Score();
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_ADMS_Section1Score"].DefaultValue = ADMSSection1Score.ToString("##0.00", CultureInfo.CurrentCulture);

            decimal ADMSSection2Score = ItemInserting_ADMSSection2Score();
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_ADMS_Section2Score"].DefaultValue = ADMSSection2Score.ToString("##0.00", CultureInfo.CurrentCulture);

            decimal ADMSSection3Score = ItemInserting_ADMSSection3Score();
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_ADMS_Section3Score"].DefaultValue = ADMSSection3Score.ToString("##0.00", CultureInfo.CurrentCulture);

            decimal ADMSSection4Score = ItemInserting_ADMSSection4Score();
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_ADMS_Section4Score"].DefaultValue = ADMSSection4Score.ToString("##0.00", CultureInfo.CurrentCulture);

            decimal ADMSScore = 0;
            string StringADMSSection1Score = ADMSSection1Score.ToString("##0.00", CultureInfo.CurrentCulture);
            string StringADMSSection2Score = ADMSSection2Score.ToString("##0.00", CultureInfo.CurrentCulture);
            string StringADMSSection3Score = ADMSSection3Score.ToString("##0.00", CultureInfo.CurrentCulture);
            string StringADMSSection4Score = ADMSSection4Score.ToString("##0.00", CultureInfo.CurrentCulture);

            ADMSScore = ((Convert.ToDecimal(StringADMSSection1Score, CultureInfo.CurrentCulture) + Convert.ToDecimal(StringADMSSection2Score, CultureInfo.CurrentCulture) + Convert.ToDecimal(StringADMSSection3Score, CultureInfo.CurrentCulture) + Convert.ToDecimal(StringADMSSection4Score, CultureInfo.CurrentCulture)) / 4);
            SqlDataSource_MHQ14_Form.InsertParameters["MHQ14_Questionnaire_ADMS_Score"].DefaultValue = ADMSScore.ToString("##0.00", CultureInfo.CurrentCulture);

            Session["MHQ14_Questionnaire_ReportNumber"] = "";
          }
        }

        Session["MHQ14QuestionnaireId"] = "";
        Session["MHQ14QuestionnaireCreatedDate"] = "";
      }
    }

    protected decimal ItemInserting_ADMSSection1Score()
    {
      decimal ADMSSection1Score = 0;
      decimal ADMSSection1Numerator = 0;
      decimal ADMSSection1Denominator = 0;
      RadioButtonList RadioButtonList_InsertADMSQ1A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1A");
      RadioButtonList RadioButtonList_InsertADMSQ1B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1B");
      RadioButtonList RadioButtonList_InsertADMSQ1C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1C");
      RadioButtonList RadioButtonList_InsertADMSQ1D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1D");
      RadioButtonList RadioButtonList_InsertADMSQ1E = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1E");

      if (RadioButtonList_InsertADMSQ1A.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ1A.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ1B.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ1B.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ1C.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ1C.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ1D.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ1D.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ1E.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ1E.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (ADMSSection1Denominator == 0)
      {
        ADMSSection1Score = 0;
      }
      else
      {
        ADMSSection1Score = ADMSSection1Numerator / ADMSSection1Denominator;
      }

      return ADMSSection1Score;
    }

    protected decimal ItemInserting_ADMSSection2Score()
    {
      decimal ADMSSection2Score = 0;
      decimal ADMSSection2Numerator = 0;
      decimal ADMSSection2Denominator = 0;
      RadioButtonList RadioButtonList_InsertADMSQ2A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2A");
      RadioButtonList RadioButtonList_InsertADMSQ2B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2B");
      RadioButtonList RadioButtonList_InsertADMSQ2C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2C");
      RadioButtonList RadioButtonList_InsertADMSQ2D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2D");

      if (RadioButtonList_InsertADMSQ2A.SelectedValue != "N/A")
      {
        ADMSSection2Numerator = ADMSSection2Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ2A.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection2Denominator = ADMSSection2Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ2B.SelectedValue != "N/A")
      {
        ADMSSection2Numerator = ADMSSection2Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ2B.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection2Denominator = ADMSSection2Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ2C.SelectedValue != "N/A")
      {
        ADMSSection2Numerator = ADMSSection2Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ2C.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection2Denominator = ADMSSection2Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ2D.SelectedValue != "N/A")
      {
        ADMSSection2Numerator = ADMSSection2Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ2D.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection2Denominator = ADMSSection2Denominator + 1;
      }

      if (ADMSSection2Denominator == 0)
      {
        ADMSSection2Score = 0;
      }
      else
      {
        ADMSSection2Score = ADMSSection2Numerator / ADMSSection2Denominator;
      }

      return ADMSSection2Score;
    }

    protected decimal ItemInserting_ADMSSection3Score()
    {
      decimal ADMSSection3Score = 0;
      decimal ADMSSection3Numerator = 0;
      decimal ADMSSection3Denominator = 0;
      RadioButtonList RadioButtonList_InsertADMSQ3A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ3A");
      RadioButtonList RadioButtonList_InsertADMSQ3B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ3B");

      if (RadioButtonList_InsertADMSQ3A.SelectedValue != "N/A")
      {
        ADMSSection3Numerator = ADMSSection3Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ3A.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection3Denominator = ADMSSection3Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ3B.SelectedValue != "N/A")
      {
        ADMSSection3Numerator = ADMSSection3Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ3B.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection3Denominator = ADMSSection3Denominator + 1;
      }

      if (ADMSSection3Denominator == 0)
      {
        ADMSSection3Score = 0;
      }
      else
      {
        ADMSSection3Score = ADMSSection3Numerator / ADMSSection3Denominator;
      }

      return ADMSSection3Score;
    }

    protected decimal ItemInserting_ADMSSection4Score()
    {
      decimal ADMSSection4Score = 0;
      decimal ADMSSection4Numerator = 0;
      decimal ADMSSection4Denominator = 0;
      RadioButtonList RadioButtonList_InsertADMSQ4A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ4A");
      RadioButtonList RadioButtonList_InsertADMSQ4B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ4B");
      RadioButtonList RadioButtonList_InsertADMSQ4C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ4C");

      if (RadioButtonList_InsertADMSQ4A.SelectedValue != "N/A")
      {
        ADMSSection4Numerator = ADMSSection4Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ4A.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection4Denominator = ADMSSection4Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ4B.SelectedValue != "N/A")
      {
        ADMSSection4Numerator = ADMSSection4Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ4B.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection4Denominator = ADMSSection4Denominator + 1;
      }

      if (RadioButtonList_InsertADMSQ4C.SelectedValue != "N/A")
      {
        ADMSSection4Numerator = ADMSSection4Numerator + Convert.ToDecimal(RadioButtonList_InsertADMSQ4C.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection4Denominator = ADMSSection4Denominator + 1;
      }

      if (ADMSSection4Denominator == 0)
      {
        ADMSSection4Score = 0;
      }
      else
      {
        ADMSSection4Score = ADMSSection4Numerator / ADMSSection4Denominator;
      }

      return ADMSSection4Score;
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertADMSDate = (TextBox)FormView_MHQ14_Form.FindControl("TextBox_InsertADMSDate");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertADMSDate.Text))
        {
          InvalidForm = "Yes";
        }

        InvalidForm = InsertRequiredFields_ADMSDiagnosisQ();
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateADMSDate = TextBox_InsertADMSDate.Text.ToString();
        DateTime ValidatedDateADMSDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateADMSDate);

        if (ValidatedDateADMSDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Assessment Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_MHQ14_Form.FindControl("TextBox_InsertADMSDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Assessment dates allowed<br />";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected string InsertRequiredFields_ADMSDiagnosisQ()
    {
      string InvalidForm = "No";

      RadioButtonList RadioButtonList_InsertADMSDiagnosisQ1 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSDiagnosisQ1");
      RadioButtonList RadioButtonList_InsertADMSDiagnosisQ2 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSDiagnosisQ2");
      RadioButtonList RadioButtonList_InsertADMSDiagnosisQ3 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSDiagnosisQ3");
      RadioButtonList RadioButtonList_InsertADMSDiagnosisQ4List = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSDiagnosisQ4List");
      RadioButtonList RadioButtonList_InsertADMSQ1A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1A");
      RadioButtonList RadioButtonList_InsertADMSQ1B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1B");
      RadioButtonList RadioButtonList_InsertADMSQ1C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1C");
      RadioButtonList RadioButtonList_InsertADMSQ1D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1D");
      RadioButtonList RadioButtonList_InsertADMSQ1E = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ1E");
      RadioButtonList RadioButtonList_InsertADMSQ2A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2A");
      RadioButtonList RadioButtonList_InsertADMSQ2B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2B");
      RadioButtonList RadioButtonList_InsertADMSQ2C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2C");
      RadioButtonList RadioButtonList_InsertADMSQ2D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ2D");
      RadioButtonList RadioButtonList_InsertADMSQ3A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ3A");
      RadioButtonList RadioButtonList_InsertADMSQ3B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ3B");
      RadioButtonList RadioButtonList_InsertADMSQ4A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ4A");
      RadioButtonList RadioButtonList_InsertADMSQ4B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ4B");
      RadioButtonList RadioButtonList_InsertADMSQ4C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_InsertADMSQ4C");

      if (string.IsNullOrEmpty(RadioButtonList_InsertADMSDiagnosisQ1.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSDiagnosisQ2.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSDiagnosisQ3.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSDiagnosisQ4List.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_InsertADMSQ1A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ1B.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ1C.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ1D.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ1E.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_InsertADMSQ2A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ2B.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ2C.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ2D.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_InsertADMSQ3A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ3B.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_InsertADMSQ4A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ4B.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_InsertADMSQ4C.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected void SqlDataSource_MHQ14_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["MHQ14_Questionnaire_Id"] = e.Command.Parameters["@MHQ14_Questionnaire_Id"].Value;
        Session["MHQ14_Questionnaire_ReportNumber"] = e.Command.Parameters["@MHQ14_Questionnaire_ReportNumber"].Value;
        Response.Redirect("InfoQuest_ReportNumber.aspx?ReportPage=Form_MHQ14&ReportNumber=" + Session["MHQ14_Questionnaire_ReportNumber"].ToString() + "", false);
      }
    }


    protected void FormView_MHQ14_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDMHQ14QuestionnaireModifiedDate"] = e.OldValues["MHQ14_Questionnaire_ModifiedDate"];
        object OLDMHQ14QuestionnaireModifiedDate = Session["OLDMHQ14QuestionnaireModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDMHQ14QuestionnaireModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareMHQ14 = (DataView)SqlDataSource_MHQ14_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareMHQ14 = DataView_CompareMHQ14[0];
        Session["DBMHQ14QuestionnaireModifiedDate"] = Convert.ToString(DataRowView_CompareMHQ14["MHQ14_Questionnaire_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBMHQ14QuestionnaireModifiedBy"] = Convert.ToString(DataRowView_CompareMHQ14["MHQ14_Questionnaire_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBMHQ14QuestionnaireModifiedDate = Session["DBMHQ14QuestionnaireModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBMHQ14QuestionnaireModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          Page.MaintainScrollPositionOnPostBack = false;

          string Label_EditConcurrencyUpdateMessage = "" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBMHQ14QuestionnaireModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>";

          ((Label)FormView_MHQ14_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_MHQ14_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Convert.ToString(Label_EditConcurrencyUpdateMessage, CultureInfo.CurrentCulture);
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
            Page.MaintainScrollPositionOnPostBack = false;
            ((Label)FormView_MHQ14_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_MHQ14_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["MHQ14_Questionnaire_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["MHQ14_Questionnaire_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_MHQ14_Questionnaire", "MHQ14_Questionnaire_Id = " + Request.QueryString["MHQ14_Questionnaire_Id"]);

            DataView DataView_MHQ14 = (DataView)SqlDataSource_MHQ14_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_MHQ14 = DataView_MHQ14[0];
            Session["MHQ14QuestionnaireHistory"] = Convert.ToString(DataRowView_MHQ14["MHQ14_Questionnaire_History"], CultureInfo.CurrentCulture);

            Session["MHQ14QuestionnaireHistory"] = Session["History"].ToString() + Session["MHQ14QuestionnaireHistory"].ToString();
            e.NewValues["MHQ14_Questionnaire_History"] = Session["MHQ14QuestionnaireHistory"].ToString();

            Session["MHQ14QuestionnaireHistory"] = "";
            Session["History"] = "";

            decimal ADMSSection1Score = ItemUpdating_ADMSSection1Score();
            e.NewValues["MHQ14_Questionnaire_ADMS_Section1Score"] = ADMSSection1Score.ToString("##0.00", CultureInfo.CurrentCulture);

            decimal ADMSSection2Score = ItemUpdating_ADMSSection2Score();
            e.NewValues["MHQ14_Questionnaire_ADMS_Section2Score"] = ADMSSection2Score.ToString("##0.00", CultureInfo.CurrentCulture);

            decimal ADMSSection3Score = ItemUpdating_ADMSSection3Score();
            e.NewValues["MHQ14_Questionnaire_ADMS_Section3Score"] = ADMSSection3Score.ToString("##0.00", CultureInfo.CurrentCulture);

            decimal ADMSSection4Score = ItemUpdating_ADMSSection4Score();
            e.NewValues["MHQ14_Questionnaire_ADMS_Section4Score"] = ADMSSection4Score.ToString("##0.00", CultureInfo.CurrentCulture);

            decimal ADMSScore = 0;
            string StringADMSSection1Score = ADMSSection1Score.ToString("##0.00", CultureInfo.CurrentCulture);
            string StringADMSSection2Score = ADMSSection2Score.ToString("##0.00", CultureInfo.CurrentCulture);
            string StringADMSSection3Score = ADMSSection3Score.ToString("##0.00", CultureInfo.CurrentCulture);
            string StringADMSSection4Score = ADMSSection4Score.ToString("##0.00", CultureInfo.CurrentCulture);

            ADMSScore = ((Convert.ToDecimal(StringADMSSection1Score, CultureInfo.CurrentCulture) + Convert.ToDecimal(StringADMSSection2Score, CultureInfo.CurrentCulture) + Convert.ToDecimal(StringADMSSection3Score, CultureInfo.CurrentCulture) + Convert.ToDecimal(StringADMSSection4Score, CultureInfo.CurrentCulture)) / 4);
            e.NewValues["MHQ14_Questionnaire_ADMS_Score"] = ADMSScore.ToString("##0.00", CultureInfo.CurrentCulture);


            DropDownList DropDownList_EditDISCHCompleteDischarge = (DropDownList)FormView_MHQ14_Form.FindControl("DropDownList_EditDISCHCompleteDischarge");
            if (DropDownList_EditDISCHCompleteDischarge.SelectedValue != "Yes")
            {
              e.NewValues["MHQ14_Questionnaire_DISCH_Section1Score"] = DBNull.Value.ToString();
              e.NewValues["MHQ14_Questionnaire_DISCH_Section2Score"] = DBNull.Value.ToString();
              e.NewValues["MHQ14_Questionnaire_DISCH_Section3Score"] = DBNull.Value.ToString();
              e.NewValues["MHQ14_Questionnaire_DISCH_Section4Score"] = DBNull.Value.ToString();
              e.NewValues["MHQ14_Questionnaire_DISCH_Score"] = DBNull.Value.ToString();
              e.NewValues["MHQ14_Questionnaire_DISCH_Difference"] = DBNull.Value.ToString();
            }
            else
            {
              decimal DISCHSection1Score = ItemUpdating_DISCHSection1Score();
              e.NewValues["MHQ14_Questionnaire_DISCH_Section1Score"] = DISCHSection1Score.ToString("##0.00", CultureInfo.CurrentCulture);

              decimal DISCHSection2Score = ItemUpdating_DISCHSection2Score();
              e.NewValues["MHQ14_Questionnaire_DISCH_Section2Score"] = DISCHSection2Score.ToString("##0.00", CultureInfo.CurrentCulture);

              decimal DISCHSection3Score = ItemUpdating_DISCHSection3Score();
              e.NewValues["MHQ14_Questionnaire_DISCH_Section3Score"] = DISCHSection3Score.ToString("##0.00", CultureInfo.CurrentCulture);

              decimal DISCHSection4Score = ItemUpdating_DISCHSection4Score();
              e.NewValues["MHQ14_Questionnaire_DISCH_Section4Score"] = DISCHSection4Score.ToString("##0.00", CultureInfo.CurrentCulture);

              decimal DISCHScore = 0;
              string StringDISCHSection1Score = DISCHSection1Score.ToString("##0.00", CultureInfo.CurrentCulture);
              string StringDISCHSection2Score = DISCHSection2Score.ToString("##0.00", CultureInfo.CurrentCulture);
              string StringDISCHSection3Score = DISCHSection3Score.ToString("##0.00", CultureInfo.CurrentCulture);
              string StringDISCHSection4Score = DISCHSection4Score.ToString("##0.00", CultureInfo.CurrentCulture);
              DISCHScore = ((Convert.ToDecimal(StringDISCHSection1Score, CultureInfo.CurrentCulture) + Convert.ToDecimal(StringDISCHSection2Score, CultureInfo.CurrentCulture) + Convert.ToDecimal(StringDISCHSection3Score, CultureInfo.CurrentCulture) + Convert.ToDecimal(StringDISCHSection4Score, CultureInfo.CurrentCulture)) / 4);
              e.NewValues["MHQ14_Questionnaire_DISCH_Score"] = DISCHScore.ToString("##0.00", CultureInfo.CurrentCulture);

              decimal DISCHDifference = 0;
              string StringDISCHScore = DISCHScore.ToString("##0.00", CultureInfo.CurrentCulture);
              string StringADMSScore = ADMSScore.ToString("##0.00", CultureInfo.CurrentCulture);
              DISCHDifference = (Convert.ToDecimal(StringDISCHScore, CultureInfo.CurrentCulture) - Convert.ToDecimal(StringADMSScore, CultureInfo.CurrentCulture));
              e.NewValues["MHQ14_Questionnaire_DISCH_Difference"] = DISCHDifference.ToString("##0.00", CultureInfo.CurrentCulture);
            }
          }
        }

        Session["OLDMHQ14QuestionnaireModifiedDate"] = "";
        Session["DBMHQ14QuestionnaireModifiedDate"] = "";
        Session["DBMHQ14QuestionnaireModifiedBy"] = "";
      }
    }

    protected decimal ItemUpdating_ADMSSection1Score()
    {
      decimal ADMSSection1Score = 0;
      decimal ADMSSection1Numerator = 0;
      decimal ADMSSection1Denominator = 0;
      RadioButtonList RadioButtonList_EditADMSQ1A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1A");
      RadioButtonList RadioButtonList_EditADMSQ1B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1B");
      RadioButtonList RadioButtonList_EditADMSQ1C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1C");
      RadioButtonList RadioButtonList_EditADMSQ1D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1D");
      RadioButtonList RadioButtonList_EditADMSQ1E = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1E");

      if (RadioButtonList_EditADMSQ1A.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ1A.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ1B.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ1B.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ1C.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ1C.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ1D.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ1D.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ1E.SelectedValue != "N/A")
      {
        ADMSSection1Numerator = ADMSSection1Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ1E.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection1Denominator = ADMSSection1Denominator + 1;
      }

      if (ADMSSection1Denominator == 0)
      {
        ADMSSection1Score = 0;
      }
      else
      {
        ADMSSection1Score = ADMSSection1Numerator / ADMSSection1Denominator;
      }

      return ADMSSection1Score;
    }

    protected decimal ItemUpdating_ADMSSection2Score()
    {
      decimal ADMSSection2Score = 0;
      decimal ADMSSection2Numerator = 0;
      decimal ADMSSection2Denominator = 0;
      RadioButtonList RadioButtonList_EditADMSQ2A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2A");
      RadioButtonList RadioButtonList_EditADMSQ2B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2B");
      RadioButtonList RadioButtonList_EditADMSQ2C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2C");
      RadioButtonList RadioButtonList_EditADMSQ2D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2D");

      if (RadioButtonList_EditADMSQ2A.SelectedValue != "N/A")
      {
        ADMSSection2Numerator = ADMSSection2Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ2A.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection2Denominator = ADMSSection2Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ2B.SelectedValue != "N/A")
      {
        ADMSSection2Numerator = ADMSSection2Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ2B.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection2Denominator = ADMSSection2Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ2C.SelectedValue != "N/A")
      {
        ADMSSection2Numerator = ADMSSection2Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ2C.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection2Denominator = ADMSSection2Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ2D.SelectedValue != "N/A")
      {
        ADMSSection2Numerator = ADMSSection2Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ2D.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection2Denominator = ADMSSection2Denominator + 1;
      }

      if (ADMSSection2Denominator == 0)
      {
        ADMSSection2Score = 0;
      }
      else
      {
        ADMSSection2Score = ADMSSection2Numerator / ADMSSection2Denominator;
      }

      return ADMSSection2Score;
    }

    protected decimal ItemUpdating_ADMSSection3Score()
    {
      decimal ADMSSection3Score = 0;
      decimal ADMSSection3Numerator = 0;
      decimal ADMSSection3Denominator = 0;
      RadioButtonList RadioButtonList_EditADMSQ3A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ3A");
      RadioButtonList RadioButtonList_EditADMSQ3B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ3B");

      if (RadioButtonList_EditADMSQ3A.SelectedValue != "N/A")
      {
        ADMSSection3Numerator = ADMSSection3Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ3A.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection3Denominator = ADMSSection3Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ3B.SelectedValue != "N/A")
      {
        ADMSSection3Numerator = ADMSSection3Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ3B.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection3Denominator = ADMSSection3Denominator + 1;
      }

      if (ADMSSection3Denominator == 0)
      {
        ADMSSection3Score = 0;
      }
      else
      {
        ADMSSection3Score = ADMSSection3Numerator / ADMSSection3Denominator;
      }

      return ADMSSection3Score;
    }

    protected decimal ItemUpdating_ADMSSection4Score()
    {
      decimal ADMSSection4Score = 0;
      decimal ADMSSection4Numerator = 0;
      decimal ADMSSection4Denominator = 0;
      RadioButtonList RadioButtonList_EditADMSQ4A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ4A");
      RadioButtonList RadioButtonList_EditADMSQ4B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ4B");
      RadioButtonList RadioButtonList_EditADMSQ4C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ4C");

      if (RadioButtonList_EditADMSQ4A.SelectedValue != "N/A")
      {
        ADMSSection4Numerator = ADMSSection4Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ4A.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection4Denominator = ADMSSection4Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ4B.SelectedValue != "N/A")
      {
        ADMSSection4Numerator = ADMSSection4Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ4B.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection4Denominator = ADMSSection4Denominator + 1;
      }

      if (RadioButtonList_EditADMSQ4C.SelectedValue != "N/A")
      {
        ADMSSection4Numerator = ADMSSection4Numerator + Convert.ToDecimal(RadioButtonList_EditADMSQ4C.SelectedValue, CultureInfo.CurrentCulture);
        ADMSSection4Denominator = ADMSSection4Denominator + 1;
      }

      if (ADMSSection4Denominator == 0)
      {
        ADMSSection4Score = 0;
      }
      else
      {
        ADMSSection4Score = ADMSSection4Numerator / ADMSSection4Denominator;
      }

      return ADMSSection4Score;
    }

    protected decimal ItemUpdating_DISCHSection1Score()
    {
      decimal DISCHSection1Score = 0;
      decimal DISCHSection1Numerator = 0;
      decimal DISCHSection1Denominator = 0;
      RadioButtonList RadioButtonList_EditDISCHQ1A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1A");
      RadioButtonList RadioButtonList_EditDISCHQ1B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1B");
      RadioButtonList RadioButtonList_EditDISCHQ1C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1C");
      RadioButtonList RadioButtonList_EditDISCHQ1D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1D");
      RadioButtonList RadioButtonList_EditDISCHQ1E = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1E");

      if (RadioButtonList_EditDISCHQ1A.SelectedValue != "N/A")
      {
        DISCHSection1Numerator = DISCHSection1Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ1A.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection1Denominator = DISCHSection1Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ1B.SelectedValue != "N/A")
      {
        DISCHSection1Numerator = DISCHSection1Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ1B.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection1Denominator = DISCHSection1Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ1C.SelectedValue != "N/A")
      {
        DISCHSection1Numerator = DISCHSection1Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ1C.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection1Denominator = DISCHSection1Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ1D.SelectedValue != "N/A")
      {
        DISCHSection1Numerator = DISCHSection1Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ1D.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection1Denominator = DISCHSection1Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ1E.SelectedValue != "N/A")
      {
        DISCHSection1Numerator = DISCHSection1Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ1E.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection1Denominator = DISCHSection1Denominator + 1;
      }

      if (DISCHSection1Denominator == 0)
      {
        DISCHSection1Score = 0;
      }
      else
      {
        DISCHSection1Score = DISCHSection1Numerator / DISCHSection1Denominator;
      }

      return DISCHSection1Score;
    }

    protected decimal ItemUpdating_DISCHSection2Score()
    {
      decimal DISCHSection2Score = 0;
      decimal DISCHSection2Numerator = 0;
      decimal DISCHSection2Denominator = 0;
      RadioButtonList RadioButtonList_EditDISCHQ2A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2A");
      RadioButtonList RadioButtonList_EditDISCHQ2B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2B");
      RadioButtonList RadioButtonList_EditDISCHQ2C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2C");
      RadioButtonList RadioButtonList_EditDISCHQ2D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2D");

      if (RadioButtonList_EditDISCHQ2A.SelectedValue != "N/A")
      {
        DISCHSection2Numerator = DISCHSection2Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ2A.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection2Denominator = DISCHSection2Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ2B.SelectedValue != "N/A")
      {
        DISCHSection2Numerator = DISCHSection2Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ2B.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection2Denominator = DISCHSection2Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ2C.SelectedValue != "N/A")
      {
        DISCHSection2Numerator = DISCHSection2Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ2C.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection2Denominator = DISCHSection2Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ2D.SelectedValue != "N/A")
      {
        DISCHSection2Numerator = DISCHSection2Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ2D.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection2Denominator = DISCHSection2Denominator + 1;
      }

      if (DISCHSection2Denominator == 0)
      {
        DISCHSection2Score = 0;
      }
      else
      {
        DISCHSection2Score = DISCHSection2Numerator / DISCHSection2Denominator;
      }

      return DISCHSection2Score;
    }

    protected decimal ItemUpdating_DISCHSection3Score()
    {
      decimal DISCHSection3Score = 0;
      decimal DISCHSection3Numerator = 0;
      decimal DISCHSection3Denominator = 0;
      RadioButtonList RadioButtonList_EditDISCHQ3A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ3A");
      RadioButtonList RadioButtonList_EditDISCHQ3B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ3B");

      if (RadioButtonList_EditDISCHQ3A.SelectedValue != "N/A")
      {
        DISCHSection3Numerator = DISCHSection3Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ3A.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection3Denominator = DISCHSection3Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ3B.SelectedValue != "N/A")
      {
        DISCHSection3Numerator = DISCHSection3Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ3B.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection3Denominator = DISCHSection3Denominator + 1;
      }

      if (DISCHSection3Denominator == 0)
      {
        DISCHSection3Score = 0;
      }
      else
      {
        DISCHSection3Score = DISCHSection3Numerator / DISCHSection3Denominator;
      }

      return DISCHSection3Score;
    }

    protected decimal ItemUpdating_DISCHSection4Score()
    {
      decimal DISCHSection4Score = 0;
      decimal DISCHSection4Numerator = 0;
      decimal DISCHSection4Denominator = 0;
      RadioButtonList RadioButtonList_EditDISCHQ4A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4A");
      RadioButtonList RadioButtonList_EditDISCHQ4B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4B");
      RadioButtonList RadioButtonList_EditDISCHQ4C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4C");

      if (RadioButtonList_EditDISCHQ4A.SelectedValue != "N/A")
      {
        DISCHSection4Numerator = DISCHSection4Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ4A.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection4Denominator = DISCHSection4Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ4B.SelectedValue != "N/A")
      {
        DISCHSection4Numerator = DISCHSection4Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ4B.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection4Denominator = DISCHSection4Denominator + 1;
      }

      if (RadioButtonList_EditDISCHQ4C.SelectedValue != "N/A")
      {
        DISCHSection4Numerator = DISCHSection4Numerator + Convert.ToDecimal(RadioButtonList_EditDISCHQ4C.SelectedValue, CultureInfo.CurrentCulture);
        DISCHSection4Denominator = DISCHSection4Denominator + 1;
      }

      if (DISCHSection4Denominator == 0)
      {
        DISCHSection4Score = 0;
      }
      else
      {
        DISCHSection4Score = DISCHSection4Numerator / DISCHSection4Denominator;
      }

      return DISCHSection4Score;
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditADMSDate = (TextBox)FormView_MHQ14_Form.FindControl("TextBox_EditADMSDate");

      DropDownList DropDownList_EditDISCHCompleteDischarge = (DropDownList)FormView_MHQ14_Form.FindControl("DropDownList_EditDISCHCompleteDischarge");
      TextBox TextBox_EditDISCHDate = (TextBox)FormView_MHQ14_Form.FindControl("TextBox_EditDISCHDate");
      DropDownList DropDownList_EditDISCHNoDischargeReasonList = (DropDownList)FormView_MHQ14_Form.FindControl("DropDownList_EditDISCHNoDischargeReasonList");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditADMSDate.Text))
        {
          InvalidForm = "Yes";
        }

        InvalidForm = EditRequiredFields_ADMSDiagnosisQ();

        if (DropDownList_EditDISCHCompleteDischarge.SelectedValue == "No")
        {
          if (string.IsNullOrEmpty(TextBox_EditDISCHDate.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_EditDISCHNoDischargeReasonList.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else if (DropDownList_EditDISCHCompleteDischarge.SelectedValue == "Yes")
        {
          if (string.IsNullOrEmpty(TextBox_EditDISCHDate.Text))
          {
            InvalidForm = "Yes";
          }

          InvalidForm = EditRequiredFields_DISCHDiagnosisQ();
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateADMSDate = TextBox_EditADMSDate.Text.ToString();
        DateTime ValidatedDateADMSDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateADMSDate);

        if (ValidatedDateADMSDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Assessment Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_MHQ14_Form.FindControl("TextBox_EditADMSDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Assessment dates allowed<br />";
          }
        }

        if (!string.IsNullOrEmpty(DropDownList_EditDISCHCompleteDischarge.SelectedValue))
        {
          string DateToValidateDISCHDate = TextBox_EditDISCHDate.Text.ToString();
          DateTime ValidatedDateDISCHDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDISCHDate);

          if (ValidatedDateDISCHDate.ToString() == "0001/01/01 12:00:00 AM")
          {
            InvalidFormMessage = InvalidFormMessage + "Assessment Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
          }
          else
          {
            DateTime PickedADMSDate = Convert.ToDateTime(((TextBox)FormView_MHQ14_Form.FindControl("TextBox_EditADMSDate")).Text, CultureInfo.CurrentCulture);
            DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_MHQ14_Form.FindControl("TextBox_EditDISCHDate")).Text, CultureInfo.CurrentCulture);
            DateTime CurrentDate = DateTime.Now;

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future Assessment dates allowed<br />";
            }

            if (PickedADMSDate.CompareTo(PickedDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "Discharge Assessment Date cannot be before Admission Assessment Date<br />";
            }
          }
        }
      }

      return InvalidFormMessage;
    }

    protected string EditRequiredFields_ADMSDiagnosisQ()
    {
      string InvalidForm = "No";

      RadioButtonList RadioButtonList_EditADMSDiagnosisQ1 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSDiagnosisQ1");
      RadioButtonList RadioButtonList_EditADMSDiagnosisQ2 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSDiagnosisQ2");
      RadioButtonList RadioButtonList_EditADMSDiagnosisQ3 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSDiagnosisQ3");
      RadioButtonList RadioButtonList_EditADMSDiagnosisQ4List = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSDiagnosisQ4List");
      RadioButtonList RadioButtonList_EditADMSQ1A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1A");
      RadioButtonList RadioButtonList_EditADMSQ1B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1B");
      RadioButtonList RadioButtonList_EditADMSQ1C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1C");
      RadioButtonList RadioButtonList_EditADMSQ1D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1D");
      RadioButtonList RadioButtonList_EditADMSQ1E = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ1E");
      RadioButtonList RadioButtonList_EditADMSQ2A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2A");
      RadioButtonList RadioButtonList_EditADMSQ2B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2B");
      RadioButtonList RadioButtonList_EditADMSQ2C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2C");
      RadioButtonList RadioButtonList_EditADMSQ2D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ2D");
      RadioButtonList RadioButtonList_EditADMSQ3A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ3A");
      RadioButtonList RadioButtonList_EditADMSQ3B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ3B");
      RadioButtonList RadioButtonList_EditADMSQ4A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ4A");
      RadioButtonList RadioButtonList_EditADMSQ4B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ4B");
      RadioButtonList RadioButtonList_EditADMSQ4C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditADMSQ4C");

      if (string.IsNullOrEmpty(RadioButtonList_EditADMSDiagnosisQ1.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSDiagnosisQ2.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSDiagnosisQ3.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSDiagnosisQ4List.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_EditADMSQ1A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ1B.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ1C.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ1D.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ1E.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_EditADMSQ2A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ2B.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ2C.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ2D.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_EditADMSQ3A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ3B.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_EditADMSQ4A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ4B.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditADMSQ4C.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string EditRequiredFields_DISCHDiagnosisQ()
    {
      string InvalidForm = "No";

      RadioButtonList RadioButtonList_EditDISCHDiagnosisQ1 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ1");
      RadioButtonList RadioButtonList_EditDISCHDiagnosisQ2 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ2");
      RadioButtonList RadioButtonList_EditDISCHDiagnosisQ3 = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ3");
      RadioButtonList RadioButtonList_EditDISCHDiagnosisQ4List = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHDiagnosisQ4List");
      RadioButtonList RadioButtonList_EditDISCHQ1A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1A");
      RadioButtonList RadioButtonList_EditDISCHQ1B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1B");
      RadioButtonList RadioButtonList_EditDISCHQ1C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1C");
      RadioButtonList RadioButtonList_EditDISCHQ1D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1D");
      RadioButtonList RadioButtonList_EditDISCHQ1E = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ1E");
      RadioButtonList RadioButtonList_EditDISCHQ2A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2A");
      RadioButtonList RadioButtonList_EditDISCHQ2B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2B");
      RadioButtonList RadioButtonList_EditDISCHQ2C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2C");
      RadioButtonList RadioButtonList_EditDISCHQ2D = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ2D");
      RadioButtonList RadioButtonList_EditDISCHQ3A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ3A");
      RadioButtonList RadioButtonList_EditDISCHQ3B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ3B");
      RadioButtonList RadioButtonList_EditDISCHQ4A = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4A");
      RadioButtonList RadioButtonList_EditDISCHQ4B = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4B");
      RadioButtonList RadioButtonList_EditDISCHQ4C = (RadioButtonList)FormView_MHQ14_Form.FindControl("RadioButtonList_EditDISCHQ4C");

      if (string.IsNullOrEmpty(RadioButtonList_EditDISCHDiagnosisQ1.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHDiagnosisQ2.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHDiagnosisQ3.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHDiagnosisQ4List.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_EditDISCHQ1A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ1B.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ1C.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ1D.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ1E.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_EditDISCHQ2A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ2B.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ2C.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ2D.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_EditDISCHQ3A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ3B.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(RadioButtonList_EditDISCHQ4A.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ4B.SelectedValue) || string.IsNullOrEmpty(RadioButtonList_EditDISCHQ4C.SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected void SqlDataSource_MHQ14_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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

            ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'>FormPrint('InfoQuest_Print.aspx?PrintPage=Form_MHQ14&PrintValue=" + Request.QueryString["MHQ14_Questionnaire_Id"] + "')</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "Reload", "<script language='javascript'>window.location.href='" + Request.Url.AbsoluteUri + "'</script>");
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ClientScript.RegisterStartupScript(this.GetType(), "Email", "<script language='javascript'>FormEmail('InfoQuest_Email.aspx?EmailPage=Form_MHQ14&EmailValue=" + Request.QueryString["MHQ14_Questionnaire_Id"] + "')</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "Reload", "<script language='javascript'>window.location.href='" + Request.Url.AbsoluteUri + "'</script>");
          }
        }
      }
    }


    protected void FormView_MHQ14_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["MHQ14_Questionnaire_Id"] != null)
          {
            Response.Redirect("Form_MHQ14.aspx", false);
          }
        }
      }
    }

    protected void FormView_MHQ14_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_MHQ14_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["MHQ14_Questionnaire_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_MHQ14_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["MHQ14_Questionnaire_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void EditDataBound()
    {
      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 34";
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
        ((Button)FormView_MHQ14_Form.FindControl("Button_EditPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_MHQ14_Form.FindControl("Button_EditPrint")).Visible = true;
      }

      if (Email == "False")
      {
        ((Button)FormView_MHQ14_Form.FindControl("Button_EditEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_MHQ14_Form.FindControl("Button_EditEmail")).Visible = true;
      }

      Email = "";
      Print = "";
    }

    protected void ReadOnlyDataBound()
    {
      Session["MHQ14QuestionnaireADMSDiagnosisQ1"] = "";
      Session["MHQ14QuestionnaireADMSDiagnosisQ2"] = "";
      Session["MHQ14QuestionnaireADMSDiagnosisQ3"] = "";
      Session["MHQ14QuestionnaireADMSDiagnosisQ4Name"] = "";
      Session["MHQ14QuestionnaireADMSQ1A"] = "";
      Session["MHQ14QuestionnaireADMSQ1B"] = "";
      Session["MHQ14QuestionnaireADMSQ1C"] = "";
      Session["MHQ14QuestionnaireADMSQ1D"] = "";
      Session["MHQ14QuestionnaireADMSQ1E"] = "";
      Session["MHQ14QuestionnaireADMSQ2A"] = "";
      Session["MHQ14QuestionnaireADMSQ2B"] = "";
      Session["MHQ14QuestionnaireADMSQ2C"] = "";
      Session["MHQ14QuestionnaireADMSQ2D"] = "";
      Session["MHQ14QuestionnaireADMSQ3A"] = "";
      Session["MHQ14QuestionnaireADMSQ3B"] = "";
      Session["MHQ14QuestionnaireADMSQ4A"] = "";
      Session["MHQ14QuestionnaireADMSQ4B"] = "";
      Session["MHQ14QuestionnaireADMSQ4C"] = "";
      Session["MHQ14QuestionnaireDISCHDiagnosisQ1"] = "";
      Session["MHQ14QuestionnaireDISCHDiagnosisQ2"] = "";
      Session["MHQ14QuestionnaireDISCHDiagnosisQ3"] = "";
      Session["MHQ14QuestionnaireDISCHDiagnosisQ4Name"] = "";
      Session["MHQ14QuestionnaireDISCHNoDischargeReasonName"] = "";
      Session["MHQ14QuestionnaireDISCHQ1A"] = "";
      Session["MHQ14QuestionnaireDISCHQ1B"] = "";
      Session["MHQ14QuestionnaireDISCHQ1C"] = "";
      Session["MHQ14QuestionnaireDISCHQ1D"] = "";
      Session["MHQ14QuestionnaireDISCHQ1E"] = "";
      Session["MHQ14QuestionnaireDISCHQ2A"] = "";
      Session["MHQ14QuestionnaireDISCHQ2B"] = "";
      Session["MHQ14QuestionnaireDISCHQ2C"] = "";
      Session["MHQ14QuestionnaireDISCHQ2D"] = "";
      Session["MHQ14QuestionnaireDISCHQ3A"] = "";
      Session["MHQ14QuestionnaireDISCHQ3B"] = "";
      Session["MHQ14QuestionnaireDISCHQ4A"] = "";
      Session["MHQ14QuestionnaireDISCHQ4B"] = "";
      Session["MHQ14QuestionnaireDISCHQ4C"] = "";
      string SQLStringMHQ14Questionnaire = "SELECT CASE MHQ14_Questionnaire_ADMS_Diagnosis_Q1 WHEN '1' THEN 'Yes' WHEN '0' THEN 'No' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Diagnosis_Q1 ,CASE MHQ14_Questionnaire_ADMS_Diagnosis_Q2 WHEN '1' THEN 'Yes' WHEN '0' THEN 'No' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Diagnosis_Q2 ,CASE MHQ14_Questionnaire_ADMS_Diagnosis_Q3 WHEN '1' THEN 'Yes' WHEN '0' THEN 'No' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Diagnosis_Q3 ,MHQ14_Questionnaire_ADMS_Diagnosis_Q4_Name ,CASE MHQ14_Questionnaire_ADMS_Q1A WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1A ,	CASE MHQ14_Questionnaire_ADMS_Q1B WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1B ,	CASE MHQ14_Questionnaire_ADMS_Q1C WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1C ,	CASE MHQ14_Questionnaire_ADMS_Q1D WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1D ,	CASE MHQ14_Questionnaire_ADMS_Q1E WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q1E ,		CASE MHQ14_Questionnaire_ADMS_Q2A WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q2A ,	CASE MHQ14_Questionnaire_ADMS_Q2B WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q2B ,	CASE MHQ14_Questionnaire_ADMS_Q2C WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q2C ,	CASE MHQ14_Questionnaire_ADMS_Q2D WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q2D ,	CASE MHQ14_Questionnaire_ADMS_Q3A WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q3A ,	CASE MHQ14_Questionnaire_ADMS_Q3B WHEN '100' THEN 'Not at all' WHEN '75' THEN 'Slightly' WHEN '50' THEN 'Moderately' WHEN '25' THEN 'Quite a bit' WHEN '0' THEN 'Extremely' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q3B ,		CASE MHQ14_Questionnaire_ADMS_Q4A WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q4A ,	CASE MHQ14_Questionnaire_ADMS_Q4B WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q4B ,	CASE MHQ14_Questionnaire_ADMS_Q4C WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_ADMS_Q4C , CASE MHQ14_Questionnaire_DISCH_Diagnosis_Q1 WHEN '1' THEN 'Yes' WHEN '0' THEN 'No' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Diagnosis_Q1 ,CASE MHQ14_Questionnaire_DISCH_Diagnosis_Q2 WHEN '1' THEN 'Yes' WHEN '0' THEN 'No' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Diagnosis_Q2 , CASE MHQ14_Questionnaire_DISCH_Diagnosis_Q3 WHEN '1' THEN 'Yes' WHEN '0' THEN 'No' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Diagnosis_Q3 ,MHQ14_Questionnaire_DISCH_Diagnosis_Q4_Name ,MHQ14_Questionnaire_DISCH_NoDischargeReason_Name ,	CASE MHQ14_Questionnaire_DISCH_Q1A WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1A ,	CASE MHQ14_Questionnaire_DISCH_Q1B WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1B ,	CASE MHQ14_Questionnaire_DISCH_Q1C WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1C ,	CASE MHQ14_Questionnaire_DISCH_Q1D WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1D ,	CASE MHQ14_Questionnaire_DISCH_Q1E WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q1E ,	CASE MHQ14_Questionnaire_DISCH_Q2A WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q2A ,	CASE MHQ14_Questionnaire_DISCH_Q2B WHEN '100' THEN 'All of the time' WHEN '80' THEN 'Most of the time' WHEN '60' THEN 'A good bit of the time' WHEN '40' THEN 'Some of the time' WHEN '20' THEN 'A little of the time' WHEN '0' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q2B ,	CASE MHQ14_Questionnaire_DISCH_Q2C WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q2C ,	CASE MHQ14_Questionnaire_DISCH_Q2D WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q2D ,	CASE MHQ14_Questionnaire_DISCH_Q3A WHEN '0' THEN 'All of the time' WHEN '20' THEN 'Most of the time' WHEN '40' THEN 'A good bit of the time' WHEN '60' THEN 'Some of the time' WHEN '80' THEN 'A little of the time' WHEN '100' THEN 'None of the time' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q3A ,	CASE MHQ14_Questionnaire_DISCH_Q3B WHEN '100' THEN 'Not at all' WHEN '75' THEN 'Slightly' WHEN '50' THEN 'Moderately' WHEN '25' THEN 'Quite a bit' WHEN '0' THEN 'Extremely' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q3B ,		CASE MHQ14_Questionnaire_DISCH_Q4A WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q4A ,	CASE MHQ14_Questionnaire_DISCH_Q4B WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q4B ,	CASE MHQ14_Questionnaire_DISCH_Q4C WHEN '0' THEN 'Yes' WHEN '100' THEN 'No' WHEN 'N/A' THEN 'N/A' ELSE NULL END AS MHQ14_Questionnaire_DISCH_Q4C FROM vForm_MHQ14 WHERE MHQ14_Questionnaire_Id = @MHQ14_Questionnaire_Id";
      using (SqlCommand SqlCommand_MHQ14Questionnaire = new SqlCommand(SQLStringMHQ14Questionnaire))
      {
        SqlCommand_MHQ14Questionnaire.Parameters.AddWithValue("@MHQ14_Questionnaire_Id", Request.QueryString["MHQ14_Questionnaire_Id"]);
        DataTable DataTable_MHQ14Questionnaire;
        using (DataTable_MHQ14Questionnaire = new DataTable())
        {
          DataTable_MHQ14Questionnaire.Locale = CultureInfo.CurrentCulture;
          DataTable_MHQ14Questionnaire = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHQ14Questionnaire).Copy();
          if (DataTable_MHQ14Questionnaire.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MHQ14Questionnaire.Rows)
            {
              Session["MHQ14QuestionnaireADMSDiagnosisQ1"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Diagnosis_Q1"];
              Session["MHQ14QuestionnaireADMSDiagnosisQ2"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Diagnosis_Q2"];
              Session["MHQ14QuestionnaireADMSDiagnosisQ3"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Diagnosis_Q3"];
              Session["MHQ14QuestionnaireADMSDiagnosisQ4Name"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Diagnosis_Q4_Name"];
              Session["MHQ14QuestionnaireADMSQ1A"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q1A"];
              Session["MHQ14QuestionnaireADMSQ1B"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q1B"];
              Session["MHQ14QuestionnaireADMSQ1C"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q1C"];
              Session["MHQ14QuestionnaireADMSQ1D"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q1D"];
              Session["MHQ14QuestionnaireADMSQ1E"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q1E"];
              Session["MHQ14QuestionnaireADMSQ2A"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q2A"];
              Session["MHQ14QuestionnaireADMSQ2B"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q2B"];
              Session["MHQ14QuestionnaireADMSQ2C"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q2C"];
              Session["MHQ14QuestionnaireADMSQ2D"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q2D"];
              Session["MHQ14QuestionnaireADMSQ3A"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q3A"];
              Session["MHQ14QuestionnaireADMSQ3B"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q3B"];
              Session["MHQ14QuestionnaireADMSQ4A"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q4A"];
              Session["MHQ14QuestionnaireADMSQ4B"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q4B"];
              Session["MHQ14QuestionnaireADMSQ4C"] = DataRow_Row["MHQ14_Questionnaire_ADMS_Q4C"];
              Session["MHQ14QuestionnaireDISCHDiagnosisQ1"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Diagnosis_Q1"];
              Session["MHQ14QuestionnaireDISCHDiagnosisQ2"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Diagnosis_Q2"];
              Session["MHQ14QuestionnaireDISCHDiagnosisQ3"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Diagnosis_Q3"];
              Session["MHQ14QuestionnaireDISCHDiagnosisQ4Name"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Diagnosis_Q4_Name"];
              Session["MHQ14QuestionnaireDISCHNoDischargeReasonName"] = DataRow_Row["MHQ14_Questionnaire_DISCH_NoDischargeReason_Name"];
              Session["MHQ14QuestionnaireDISCHQ1A"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q1A"];
              Session["MHQ14QuestionnaireDISCHQ1B"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q1B"];
              Session["MHQ14QuestionnaireDISCHQ1C"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q1C"];
              Session["MHQ14QuestionnaireDISCHQ1D"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q1D"];
              Session["MHQ14QuestionnaireDISCHQ1E"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q1E"];
              Session["MHQ14QuestionnaireDISCHQ2A"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q2A"];
              Session["MHQ14QuestionnaireDISCHQ2B"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q2B"];
              Session["MHQ14QuestionnaireDISCHQ2C"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q2C"];
              Session["MHQ14QuestionnaireDISCHQ2D"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q2D"];
              Session["MHQ14QuestionnaireDISCHQ3A"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q3A"];
              Session["MHQ14QuestionnaireDISCHQ3B"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q3B"];
              Session["MHQ14QuestionnaireDISCHQ4A"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q4A"];
              Session["MHQ14QuestionnaireDISCHQ4B"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q4B"];
              Session["MHQ14QuestionnaireDISCHQ4C"] = DataRow_Row["MHQ14_Questionnaire_DISCH_Q4C"];
            }
          }
        }
      }

      Label Label_ItemADMSDiagnosisQ1 = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSDiagnosisQ1");
      Label_ItemADMSDiagnosisQ1.Text = Session["MHQ14QuestionnaireADMSDiagnosisQ1"].ToString();

      Label Label_ItemADMSDiagnosisQ2 = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSDiagnosisQ2");
      Label_ItemADMSDiagnosisQ2.Text = Session["MHQ14QuestionnaireADMSDiagnosisQ2"].ToString();

      Label Label_ItemADMSDiagnosisQ3 = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSDiagnosisQ3");
      Label_ItemADMSDiagnosisQ3.Text = Session["MHQ14QuestionnaireADMSDiagnosisQ3"].ToString();

      Label Label_ItemADMSDiagnosisQ4List = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSDiagnosisQ4List");
      Label_ItemADMSDiagnosisQ4List.Text = Session["MHQ14QuestionnaireADMSDiagnosisQ4Name"].ToString();

      Label Label_ItemADMSQ1A = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ1A");
      Label_ItemADMSQ1A.Text = Session["MHQ14QuestionnaireADMSQ1A"].ToString();

      Label Label_ItemADMSQ1B = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ1B");
      Label_ItemADMSQ1B.Text = Session["MHQ14QuestionnaireADMSQ1B"].ToString();

      Label Label_ItemADMSQ1C = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ1C");
      Label_ItemADMSQ1C.Text = Session["MHQ14QuestionnaireADMSQ1C"].ToString();

      Label Label_ItemADMSQ1D = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ1D");
      Label_ItemADMSQ1D.Text = Session["MHQ14QuestionnaireADMSQ1D"].ToString();

      Label Label_ItemADMSQ1E = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ1E");
      Label_ItemADMSQ1E.Text = Session["MHQ14QuestionnaireADMSQ1E"].ToString();

      Label Label_ItemADMSQ2A = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ2A");
      Label_ItemADMSQ2A.Text = Session["MHQ14QuestionnaireADMSQ2A"].ToString();

      Label Label_ItemADMSQ2B = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ2B");
      Label_ItemADMSQ2B.Text = Session["MHQ14QuestionnaireADMSQ2B"].ToString();

      Label Label_ItemADMSQ2C = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ2C");
      Label_ItemADMSQ2C.Text = Session["MHQ14QuestionnaireADMSQ2C"].ToString();

      Label Label_ItemADMSQ2D = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ2D");
      Label_ItemADMSQ2D.Text = Session["MHQ14QuestionnaireADMSQ2D"].ToString();

      Label Label_ItemADMSQ3A = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ3A");
      Label_ItemADMSQ3A.Text = Session["MHQ14QuestionnaireADMSQ3A"].ToString();

      Label Label_ItemADMSQ3B = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ3B");
      Label_ItemADMSQ3B.Text = Session["MHQ14QuestionnaireADMSQ3B"].ToString();

      Label Label_ItemADMSQ4A = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ4A");
      Label_ItemADMSQ4A.Text = Session["MHQ14QuestionnaireADMSQ4A"].ToString();

      Label Label_ItemADMSQ4B = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ4B");
      Label_ItemADMSQ4B.Text = Session["MHQ14QuestionnaireADMSQ4B"].ToString();

      Label Label_ItemADMSQ4C = (Label)FormView_MHQ14_Form.FindControl("Label_ItemADMSQ4C");
      Label_ItemADMSQ4C.Text = Session["MHQ14QuestionnaireADMSQ4C"].ToString();

      Label Label_ItemDISCHDiagnosisQ1 = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHDiagnosisQ1");
      Label_ItemDISCHDiagnosisQ1.Text = Session["MHQ14QuestionnaireDISCHDiagnosisQ1"].ToString();

      Label Label_ItemDISCHDiagnosisQ2 = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHDiagnosisQ2");
      Label_ItemDISCHDiagnosisQ2.Text = Session["MHQ14QuestionnaireDISCHDiagnosisQ2"].ToString();

      Label Label_ItemDISCHDiagnosisQ3 = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHDiagnosisQ3");
      Label_ItemDISCHDiagnosisQ3.Text = Session["MHQ14QuestionnaireDISCHDiagnosisQ3"].ToString();

      Label Label_ItemDISCHDiagnosisQ4List = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHDiagnosisQ4List");
      Label_ItemDISCHDiagnosisQ4List.Text = Session["MHQ14QuestionnaireDISCHDiagnosisQ4Name"].ToString();

      Label Label_ItemDISCHNoDischargeReasonList = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHNoDischargeReasonList");
      Label_ItemDISCHNoDischargeReasonList.Text = Session["MHQ14QuestionnaireDISCHNoDischargeReasonName"].ToString();

      Label Label_ItemDISCHQ1A = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ1A");
      Label_ItemDISCHQ1A.Text = Session["MHQ14QuestionnaireDISCHQ1A"].ToString();

      Label Label_ItemDISCHQ1B = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ1B");
      Label_ItemDISCHQ1B.Text = Session["MHQ14QuestionnaireDISCHQ1B"].ToString();

      Label Label_ItemDISCHQ1C = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ1C");
      Label_ItemDISCHQ1C.Text = Session["MHQ14QuestionnaireDISCHQ1C"].ToString();

      Label Label_ItemDISCHQ1D = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ1D");
      Label_ItemDISCHQ1D.Text = Session["MHQ14QuestionnaireDISCHQ1D"].ToString();

      Label Label_ItemDISCHQ1E = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ1E");
      Label_ItemDISCHQ1E.Text = Session["MHQ14QuestionnaireDISCHQ1E"].ToString();

      Label Label_ItemDISCHQ2A = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ2A");
      Label_ItemDISCHQ2A.Text = Session["MHQ14QuestionnaireDISCHQ2A"].ToString();

      Label Label_ItemDISCHQ2B = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ2B");
      Label_ItemDISCHQ2B.Text = Session["MHQ14QuestionnaireDISCHQ2B"].ToString();

      Label Label_ItemDISCHQ2C = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ2C");
      Label_ItemDISCHQ2C.Text = Session["MHQ14QuestionnaireDISCHQ2C"].ToString();

      Label Label_ItemDISCHQ2D = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ2D");
      Label_ItemDISCHQ2D.Text = Session["MHQ14QuestionnaireDISCHQ2D"].ToString();

      Label Label_ItemDISCHQ3A = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ3A");
      Label_ItemDISCHQ3A.Text = Session["MHQ14QuestionnaireDISCHQ3A"].ToString();

      Label Label_ItemDISCHQ3B = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ3B");
      Label_ItemDISCHQ3B.Text = Session["MHQ14QuestionnaireDISCHQ3B"].ToString();

      Label Label_ItemDISCHQ4A = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ4A");
      Label_ItemDISCHQ4A.Text = Session["MHQ14QuestionnaireDISCHQ4A"].ToString();

      Label Label_ItemDISCHQ4B = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ4B");
      Label_ItemDISCHQ4B.Text = Session["MHQ14QuestionnaireDISCHQ4B"].ToString();

      Label Label_ItemDISCHQ4C = (Label)FormView_MHQ14_Form.FindControl("Label_ItemDISCHQ4C");
      Label_ItemDISCHQ4C.Text = Session["MHQ14QuestionnaireDISCHQ4C"].ToString();

      Session["MHQ14QuestionnaireADMSDiagnosisQ1"] = "";
      Session["MHQ14QuestionnaireADMSDiagnosisQ2"] = "";
      Session["MHQ14QuestionnaireADMSDiagnosisQ3"] = "";
      Session["MHQ14QuestionnaireADMSDiagnosisQ4Name"] = "";
      Session["MHQ14QuestionnaireADMSQ1A"] = "";
      Session["MHQ14QuestionnaireADMSQ1B"] = "";
      Session["MHQ14QuestionnaireADMSQ1C"] = "";
      Session["MHQ14QuestionnaireADMSQ1D"] = "";
      Session["MHQ14QuestionnaireADMSQ1E"] = "";
      Session["MHQ14QuestionnaireADMSQ2A"] = "";
      Session["MHQ14QuestionnaireADMSQ2B"] = "";
      Session["MHQ14QuestionnaireADMSQ2C"] = "";
      Session["MHQ14QuestionnaireADMSQ2D"] = "";
      Session["MHQ14QuestionnaireADMSQ3A"] = "";
      Session["MHQ14QuestionnaireADMSQ3B"] = "";
      Session["MHQ14QuestionnaireADMSQ4A"] = "";
      Session["MHQ14QuestionnaireADMSQ4B"] = "";
      Session["MHQ14QuestionnaireADMSQ4C"] = "";
      Session["MHQ14QuestionnaireDISCHDiagnosisQ1"] = "";
      Session["MHQ14QuestionnaireDISCHDiagnosisQ2"] = "";
      Session["MHQ14QuestionnaireDISCHDiagnosisQ3"] = "";
      Session["MHQ14QuestionnaireDISCHDiagnosisQ4Name"] = "";
      Session["MHQ14QuestionnaireDISCHNoDischargeReasonName"] = "";
      Session["MHQ14QuestionnaireDISCHQ1A"] = "";
      Session["MHQ14QuestionnaireDISCHQ1B"] = "";
      Session["MHQ14QuestionnaireDISCHQ1C"] = "";
      Session["MHQ14QuestionnaireDISCHQ1D"] = "";
      Session["MHQ14QuestionnaireDISCHQ1E"] = "";
      Session["MHQ14QuestionnaireDISCHQ2A"] = "";
      Session["MHQ14QuestionnaireDISCHQ2B"] = "";
      Session["MHQ14QuestionnaireDISCHQ2C"] = "";
      Session["MHQ14QuestionnaireDISCHQ2D"] = "";
      Session["MHQ14QuestionnaireDISCHQ3A"] = "";
      Session["MHQ14QuestionnaireDISCHQ3B"] = "";
      Session["MHQ14QuestionnaireDISCHQ4A"] = "";
      Session["MHQ14QuestionnaireDISCHQ4B"] = "";
      Session["MHQ14QuestionnaireDISCHQ4C"] = "";


      ReadOnlyDataBound_FormInfo();
    }

    protected void ReadOnlyDataBound_FormInfo()
    {
      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 34";
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
        ((Button)FormView_MHQ14_Form.FindControl("Button_ItemPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_MHQ14_Form.FindControl("Button_ItemPrint")).Visible = true;
        ((Button)FormView_MHQ14_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('InfoQuest_Print.aspx?PrintPage=Form_MHQ14&PrintValue=" + Request.QueryString["MHQ14_Questionnaire_Id"] + "')";
      }

      if (Email == "False")
      {
        ((Button)FormView_MHQ14_Form.FindControl("Button_ItemEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_MHQ14_Form.FindControl("Button_ItemEmail")).Visible = true;
        ((Button)FormView_MHQ14_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('InfoQuest_Email.aspx?EmailPage=Form_MHQ14&EmailValue=" + Request.QueryString["MHQ14_Questionnaire_Id"] + "')";
      }

      Email = "";
      Print = "";
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
    //---END--- --Form--//
  }
}