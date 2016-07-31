using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_PharmacySurveys : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          PageTitle();
        }

        if (Request.QueryString["CreatedSurveysId"] != null)
        {
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

        string SQLStringSecurity = @" SELECT SecurityUser_UserName
                                      FROM vAdministration_SecurityAccess_Active
                                      WHERE (SecurityUser_UserName = @SecurityUser_UserName) 
                                      AND (Form_Id IN ('47')) 
                                      AND (Facility_Id IN (SELECT Facility_Id FROM Form_PharmacySurveys_CreatedSurveys WHERE CreatedSurveys_Id = @CreatedSurveys_Id) OR (SecurityRole_Rank = 1))
                                      UNION
                                      SELECT CreatedSurveys_UserName 
                                      FROM Form_PharmacySurveys_CreatedSurveys 
                                      WHERE CreatedSurveys_Id = @CreatedSurveys_Id 
                                      AND CreatedSurveys_UserName = @SecurityUser_UserName";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("47");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_PharmacySurveys.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Pharmacy Surveys", "23");
      }
    }

    private void SqlDataSourceSetup()
    {
      string FacilityId = "";
      string SQLStringFacility = "SELECT Facility_Id FROM Form_PharmacySurveys_CreatedSurveys WHERE CreatedSurveys_Id = @CreatedSurveys_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
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


      SqlDataSource_PharmacySurveys_EditUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_EditUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_PharmacySurveys_EditUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacySurveys_EditUnit.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_EditUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_PharmacySurveys_EditUnit.SelectParameters.Add("Form_Id", TypeCode.String, "47");
      SqlDataSource_PharmacySurveys_EditUnit.SelectParameters.Add("Facility_Id", TypeCode.String, FacilityId);
      SqlDataSource_PharmacySurveys_EditUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "Unit_Id");
      SqlDataSource_PharmacySurveys_EditUnit.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacySurveys_CreatedSurveys");
      SqlDataSource_PharmacySurveys_EditUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "CreatedSurveys_Id = " + Request.QueryString["CreatedSurveysId"]);

      FacilityId = "";

      SqlDataSource_PharmacySurveys_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_Form.SelectCommand = "SELECT * FROM Form_PharmacySurveys_CreatedSurveys WHERE (CreatedSurveys_Id = @CreatedSurveys_Id)";
      SqlDataSource_PharmacySurveys_Form.UpdateCommand = "UPDATE Form_PharmacySurveys_CreatedSurveys SET Unit_Id = @Unit_Id , CreatedSurveys_Designation = @CreatedSurveys_Designation , CreatedSurveys_Comments = @CreatedSurveys_Comments , CreatedSurveys_Compliment = @CreatedSurveys_Compliment , CreatedSurveys_ModifiedDate = @CreatedSurveys_ModifiedDate , CreatedSurveys_ModifiedBy = @CreatedSurveys_ModifiedBy , CreatedSurveys_History = @CreatedSurveys_History WHERE CreatedSurveys_Id = @CreatedSurveys_Id";
      SqlDataSource_PharmacySurveys_Form.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_Form.SelectParameters.Add("CreatedSurveys_Id", TypeCode.Int32, Request.QueryString["CreatedSurveysId"]);
      SqlDataSource_PharmacySurveys_Form.UpdateParameters.Clear();
      SqlDataSource_PharmacySurveys_Form.UpdateParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_Form.UpdateParameters.Add("CreatedSurveys_Designation", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_Form.UpdateParameters.Add("CreatedSurveys_Comments", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_Form.UpdateParameters.Add("CreatedSurveys_Compliment", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_Form.UpdateParameters.Add("CreatedSurveys_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_Form.UpdateParameters.Add("CreatedSurveys_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_Form.UpdateParameters.Add("CreatedSurveys_History", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_Form.UpdateParameters.Add("CreatedSurveys_Id", TypeCode.Int32, "");
    }

    protected void PageTitle()
    {
      string LoadedSurveysName = "";
      string SQLStringSurveysName = "SELECT LoadedSurveys_Name FROM Form_PharmacySurveys_LoadedSurveys WHERE LoadedSurveys_Id IN ( SELECT LoadedSurveys_Id FROM Form_PharmacySurveys_CreatedSurveys WHERE CreatedSurveys_Id = @CreatedSurveys_Id )";
      using (SqlCommand SqlCommand_SurveysName = new SqlCommand(SQLStringSurveysName))
      {
        SqlCommand_SurveysName.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
        DataTable DataTable_SurveysName;
        using (DataTable_SurveysName = new DataTable())
        {
          DataTable_SurveysName.Locale = CultureInfo.CurrentCulture;
          DataTable_SurveysName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SurveysName).Copy();
          if (DataTable_SurveysName.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SurveysName.Rows)
            {
              LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
            }
          }
        }
      }

      Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("47")).ToString();
      Label_FormHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("47")).ToString() + ": " + LoadedSurveysName, CultureInfo.CurrentCulture); ;
    }

    private void SetFormVisibility()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminSurveyCreator = FromDataBase_SecurityRole_Current.SecurityFacilityAdminSurveyCreator;
      DataRow[] SecurityFacilityAdminSurveyAccess = FromDataBase_SecurityRole_Current.SecurityFacilityAdminSurveyAccess;

      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_Current = GetFormViewUpdate();
      string ViewUpdate = FromDataBase_FormViewUpdate_Current.ViewUpdate;
      string CreatedSurveysIsActive = FromDataBase_FormViewUpdate_Current.CreatedSurveys_IsActive;
      string LoadedSurveysIsActive = FromDataBase_FormViewUpdate_Current.LoadedSurveys_IsActive;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFormAdminView.Length > 0 || SecurityFacilityAdminSurveyCreator.Length > 0 || SecurityFacilityAdminSurveyAccess.Length > 0))
      {
        Security = "0";
        if (ViewUpdate == "Yes")
        {
          if (CreatedSurveysIsActive == "Yes")
          {
            if (LoadedSurveysIsActive == "Yes")
            {
              FormView_PharmacySurveys_Form.ChangeMode(FormViewMode.Edit);
            }
            else
            {
              FormView_PharmacySurveys_Form.ChangeMode(FormViewMode.ReadOnly);
            }
          }
          else
          {
            FormView_PharmacySurveys_Form.ChangeMode(FormViewMode.ReadOnly);
          }
        }
        else
        {
          FormView_PharmacySurveys_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_PharmacySurveys_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_PharmacySurveys_Form.CurrentMode == FormViewMode.Edit)
      {
        TableFormVisible_Edit();
      }

      if (FormView_PharmacySurveys_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        TableFormVisible_ReadOnly();
      }
    }

    private void TableFormVisible_Edit()
    {
      ((PlaceHolder)FormView_PharmacySurveys_Form.FindControl("PlaceHolder_EditPharmacySurveys")).Controls.Clear();
      using (Table Table_SurveyTable = new Table())
      {
        Table_SurveyTable.Attributes.Add("class", "FormView_TableBody");

        DataTable DataTable_Survey;
        string SQLStringSurvey = @"SELECT		Form_PharmacySurveys_LoadedSections.LoadedSections_Name AS SurveySection ,
					                                  Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id AS SurveyQuestionId ,
                                            Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Name AS SurveyQuestion ,
                                            DependencyShowHide.LoadedQuestions_Id AS SurveyQuestion_ShowHideDependency_QuestionId , 
                                            Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Dependency_ShowHide_LoadedAnswersId AS SurveyQuestion_ShowHideDependency_AnswerId , 
                                            Form_PharmacySurveys_LoadedAnswers.LoadedAnswers_Id AS SurveyAnswerId ,
                                            Form_PharmacySurveys_LoadedAnswers.LoadedAnswers_Name AS SurveyAnswer
                                  FROM			Form_PharmacySurveys_CreatedSurveys
                                            LEFT JOIN Form_PharmacySurveys_CreatedSurveyAnswers ON Form_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedSurveys ON Form_PharmacySurveys_CreatedSurveys.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedSections ON Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSections.LoadedSurveys_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedQuestions ON Form_PharmacySurveys_LoadedSections.LoadedSections_Id = Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedAnswers ON Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id = Form_PharmacySurveys_LoadedAnswers.LoadedQuestions_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedAnswers AS DependencyShowHide ON Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Dependency_ShowHide_LoadedAnswersId = DependencyShowHide.LoadedAnswers_Id
                                  WHERE			Form_PharmacySurveys_CreatedSurveys.CreatedSurveys_UserName = @CreatedSurveys_UserName
                                            AND Form_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = @CreatedSurveys_Id
                                            AND Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id IS NULL
                                            AND Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_IsActive = 1
                                            AND Form_PharmacySurveys_LoadedSections.LoadedSections_IsActive = 1
                                            AND Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_IsActive = 1
                                            AND Form_PharmacySurveys_LoadedAnswers.LoadedAnswers_IsActive = 1
                                  ORDER BY	Form_PharmacySurveys_LoadedSections.LoadedSections_Name , 
                                            Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Name , 
                                            Form_PharmacySurveys_LoadedAnswers.LoadedAnswers_Name";
        using (SqlCommand SqlCommand_Survey = new SqlCommand(SQLStringSurvey))
        {
          SqlCommand_Survey.Parameters.AddWithValue("@CreatedSurveys_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Survey.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
          using (DataTable_Survey = new DataTable())
          {
            DataTable_Survey.Locale = CultureInfo.CurrentCulture;
            DataTable_Survey = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Survey).Copy();
          }
        }

        Int32 SurveyQuestionCount = 1;
        if (DataTable_Survey.Rows.Count > 0)
        {
          string LastSurveyQuestion = "";
          using (DataView DataView_Survey = new DataView(DataTable_Survey))
          {
            DataTable DistinctSurveySection = DataView_Survey.ToTable(true, "SurveySection");
            if (DistinctSurveySection.Rows.Count > 0)
            {
              string SurveySection = "";
              foreach (DataRow DataRow_SurveySection in DistinctSurveySection.Rows)
              {
                SurveySection = DataRow_SurveySection["SurveySection"].ToString();

                using (TableRow TableRow_SurveySection = new TableRow())
                {
                  TableRow_SurveySection.VerticalAlign = VerticalAlign.Top;
                  Table_SurveyTable.Controls.Add(TableRow_SurveySection);

                  using (TableCell TableCell_SurveySection = new TableCell())
                  {
                    TableCell_SurveySection.ColumnSpan = 3;
                    using (Label Label_SurveySection = new Label())
                    {
                      Label_SurveySection.Text = SurveySection;

                      TableCell_SurveySection.Controls.Add(Label_SurveySection);
                      TableRow_SurveySection.Controls.Add(TableCell_SurveySection);

                      string SurveyQuestion = "";
                      DataRow[] DataRow_SurveyQuestion = DataTable_Survey.Select("SurveySection = '" + SurveySection + "'");
                      foreach (DataRow DataRow_Question in DataRow_SurveyQuestion)
                      {
                        SurveyQuestion = DataRow_Question["SurveyQuestion"].ToString();

                        if (SurveyQuestion != LastSurveyQuestion)
                        {
                          using (TableRow TableRow_SurveyQuestion = new TableRow())
                          {
                            TableRow_SurveyQuestion.ID = "ShowHideSurveyQuestion_" + SurveyQuestionCount;
                            TableRow_SurveyQuestion.VerticalAlign = VerticalAlign.Top;
                            Table_SurveyTable.Controls.Add(TableRow_SurveyQuestion);

                            for (int a = 1; a <= 3; a++)
                            {
                              using (TableCell TableCell_SurveyQuestion = new TableCell())
                              {
                                if (a == 1)
                                {
                                  Label Label_SurveyQuestionSpace = new Label();
                                  Label_SurveyQuestionSpace.Text = Convert.ToString("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", CultureInfo.CurrentCulture);

                                  TableCell_SurveyQuestion.Wrap = false;
                                  TableCell_SurveyQuestion.Controls.Add(Label_SurveyQuestionSpace);
                                }

                                if (a == 2)
                                {
                                  Label Label_SurveyQuestion = new Label();
                                  Label_SurveyQuestion.Text = SurveyQuestion;

                                  TableCell_SurveyQuestion.Wrap = true;
                                  TableCell_SurveyQuestion.ID = "FormSurveyQuestion_" + SurveyQuestionCount;
                                  TableCell_SurveyQuestion.Controls.Add(Label_SurveyQuestion);
                                }

                                if (a == 3)
                                {
                                  using (RadioButtonList RadioButtonList_SurveyAnswer = new RadioButtonList())
                                  {
                                    RadioButtonList_SurveyAnswer.ID = "RadioButtonList_EditSurveyQuestion_" + SurveyQuestionCount;
                                    RadioButtonList_SurveyAnswer.Attributes.Add("OnClick", "Validation_Form('" + SurveyQuestionCount + "'); ShowHide_Form('" + SurveyQuestionCount + "');");
                                    RadioButtonList_SurveyAnswer.RepeatDirection = RepeatDirection.Horizontal;
                                    RadioButtonList_SurveyAnswer.RepeatLayout = RepeatLayout.Flow;

                                    string SurveyQuestionId = "";
                                    string SurveyQuestionShowHideDependencyQuestionId = "";
                                    string SurveyQuestionShowHideDependencyAnswerId = "";
                                    string SurveyAnswer = "";
                                    string SurveyAnswerId = "";
                                    DataRow[] DataRow_SurveyAnswer = DataTable_Survey.Select("SurveySection = '" + SurveySection + "' AND SurveyQuestion = '" + SurveyQuestion + "'");
                                    foreach (DataRow DataRow_Answer in DataRow_SurveyAnswer)
                                    {
                                      SurveyQuestionId = DataRow_Answer["SurveyQuestionId"].ToString();
                                      SurveyQuestionShowHideDependencyQuestionId = DataRow_Answer["SurveyQuestion_ShowHideDependency_QuestionId"].ToString();
                                      SurveyQuestionShowHideDependencyAnswerId = DataRow_Answer["SurveyQuestion_ShowHideDependency_AnswerId"].ToString();
                                      SurveyAnswer = DataRow_Answer["SurveyAnswer"].ToString();
                                      SurveyAnswerId = DataRow_Answer["SurveyAnswerId"].ToString();

                                      RadioButtonList_SurveyAnswer.Items.Add(new ListItem(SurveyAnswer, SurveyAnswerId));
                                      RadioButtonList_SurveyAnswer.Attributes.Add("SurveyQuestionId", SurveyQuestionId);
                                      RadioButtonList_SurveyAnswer.Attributes.Add("SurveyQuestionShowHideDependencyQuestionId", SurveyQuestionShowHideDependencyQuestionId);
                                      RadioButtonList_SurveyAnswer.Attributes.Add("SurveyQuestionShowHideDependencyAnswerId", SurveyQuestionShowHideDependencyAnswerId);

                                      TableCell_SurveyQuestion.Wrap = false;
                                      TableCell_SurveyQuestion.Controls.Add(RadioButtonList_SurveyAnswer);
                                    }

                                    SurveyQuestionCount = SurveyQuestionCount + 1;
                                  }
                                }

                                TableRow_SurveyQuestion.Controls.Add(TableCell_SurveyQuestion);
                              }
                            }
                          }

                          LastSurveyQuestion = SurveyQuestion;
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }

        ((HiddenField)FormView_PharmacySurveys_Form.FindControl("HiddenField_EditControlCount")).Value = SurveyQuestionCount.ToString(CultureInfo.CurrentCulture);

        ((PlaceHolder)FormView_PharmacySurveys_Form.FindControl("PlaceHolder_EditPharmacySurveys")).Controls.Add(Table_SurveyTable);
      }

      ScriptManager.RegisterStartupScript(UpdatePanel_PharmacySurveys, this.GetType(), "UpdateProgress_Start", "Validation_Form(); ShowHide_Form();", true);

      ((DropDownList)FormView_PharmacySurveys_Form.FindControl("DropDownList_EditUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_PharmacySurveys_Form.FindControl("TextBox_EditDesignation")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_PharmacySurveys_Form.FindControl("TextBox_EditDesignation")).Attributes.Add("OnInput", "Validation_Form();");
    }

    private void TableFormVisible_ReadOnly()
    {
      FromDataBase_FormIsActive FromDataBase_FormIsActive_Current = GetFormIsActive();
      string CreatedSurveysName = FromDataBase_FormIsActive_Current.CreatedSurveys_Name;
      string CreatedBy = FromDataBase_FormIsActive_Current.CreatedBy;
      string ModifiedBy = FromDataBase_FormIsActive_Current.ModifiedBy;
      string CreatedSurveysIsActive = FromDataBase_FormIsActive_Current.CreatedSurveys_IsActive;
      string LoadedSurveysIsActive = FromDataBase_FormIsActive_Current.LoadedSurveys_IsActive;

      ((PlaceHolder)FormView_PharmacySurveys_Form.FindControl("PlaceHolder_ItemPharmacySurveys")).Controls.Clear();

      if (LoadedSurveysIsActive == "No")
      {
        using (Label Label_IsActive = new Label())
        {
          Label_IsActive.Text = Convert.ToString("The Survey created by " + CreatedBy + " for " + CreatedSurveysName + " has been cancelled", CultureInfo.CurrentCulture);
          Label_IsActive.CssClass = "Controls_Validation";

          ((PlaceHolder)FormView_PharmacySurveys_Form.FindControl("PlaceHolder_ItemPharmacySurveys")).Controls.Add(Label_IsActive);
        }
      }
      else
      {
        if (CreatedSurveysIsActive == "No")
        {
          using (Label Label_IsActive = new Label())
          {
            Label_IsActive.Text = Convert.ToString("The Survey created by " + CreatedBy + " for " + CreatedSurveysName + " has been cancelled by " + ModifiedBy, CultureInfo.CurrentCulture);
            Label_IsActive.CssClass = "Controls_Validation";

            ((PlaceHolder)FormView_PharmacySurveys_Form.FindControl("PlaceHolder_ItemPharmacySurveys")).Controls.Add(Label_IsActive);
          }
        }
        else
        {
          using (Table Table_SurveyTable = new Table())
          {
            Table_SurveyTable.Attributes.Add("class", "FormView_TableBody");

            DataTable DataTable_Survey;
            string SQLStringSurvey = @"SELECT		Form_PharmacySurveys_LoadedSections.LoadedSections_Name AS SurveySection ,
					                                  Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id AS SurveyQuestionId ,
                                            Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Name AS SurveyQuestion , 
                                            DependencyShowHide.LoadedQuestions_Id AS SurveyQuestion_ShowHideDependency_QuestionId , 
                                            Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Dependency_ShowHide_LoadedAnswersId AS SurveyQuestion_ShowHideDependency_AnswerId , 
                                            NULL AS SurveyAnswerId ,
                                            NULL AS SurveyAnswer
                                  FROM			Form_PharmacySurveys_CreatedSurveys
                                            LEFT JOIN Form_PharmacySurveys_CreatedSurveyAnswers ON Form_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedSurveys ON Form_PharmacySurveys_CreatedSurveys.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedSections ON Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSections.LoadedSurveys_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedQuestions ON Form_PharmacySurveys_LoadedSections.LoadedSections_Id = Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedAnswers AS DependencyShowHide ON Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Dependency_ShowHide_LoadedAnswersId = DependencyShowHide.LoadedAnswers_Id
                                  WHERE			Form_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = @CreatedSurveys_Id
                                            AND Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id IS NULL
                                            AND Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_IsActive = 1
                                            AND Form_PharmacySurveys_LoadedSections.LoadedSections_IsActive = 1
                                            AND Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_IsActive = 1
                                  UNION
                                  SELECT		Form_PharmacySurveys_LoadedSections.LoadedSections_Name AS SurveySection ,
					                                  Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id AS SurveyQuestionId ,
                                            Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Name AS SurveyQuestion , 
                                            DependencyShowHide.LoadedQuestions_Id AS SurveyQuestion_ShowHideDependency_QuestionId , 
                                            Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Dependency_ShowHide_LoadedAnswersId AS SurveyQuestion_ShowHideDependency_AnswerId , 
                                            Form_PharmacySurveys_CreatedSurveyAnswers.LoadedAnswers_Id AS SurveyAnswerId ,
                                            Form_PharmacySurveys_LoadedAnswers.LoadedAnswers_Name AS SurveyAnswer
                                  FROM			Form_PharmacySurveys_CreatedSurveyAnswers
                                            LEFT JOIN Form_PharmacySurveys_LoadedAnswers ON Form_PharmacySurveys_CreatedSurveyAnswers.LoadedAnswers_Id = Form_PharmacySurveys_LoadedAnswers.LoadedAnswers_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedQuestions ON Form_PharmacySurveys_LoadedAnswers.LoadedQuestions_Id = Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedSections ON Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id = Form_PharmacySurveys_LoadedSections.LoadedSections_Id
                                            LEFT JOIN Form_PharmacySurveys_LoadedAnswers AS DependencyShowHide ON Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Dependency_ShowHide_LoadedAnswersId = DependencyShowHide.LoadedAnswers_Id
                                  WHERE			CreatedSurveys_Id = @CreatedSurveys_Id
                                  ORDER BY	LoadedSections_Name , 
                                            LoadedQuestions_Name";
            using (SqlCommand SqlCommand_Survey = new SqlCommand(SQLStringSurvey))
            {
              SqlCommand_Survey.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
              using (DataTable_Survey = new DataTable())
              {
                DataTable_Survey.Locale = CultureInfo.CurrentCulture;
                DataTable_Survey = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Survey).Copy();
              }
            }

            Int32 SurveyQuestionCount = 1;
            if (DataTable_Survey.Rows.Count > 0)
            {
              string LastSurveyQuestion = "";
              using (DataView DataView_Survey = new DataView(DataTable_Survey))
              {
                DataTable DistinctSurveySection = DataView_Survey.ToTable(true, "SurveySection");
                if (DistinctSurveySection.Rows.Count > 0)
                {
                  string SurveySection = "";
                  foreach (DataRow DataRow_SurveySection in DistinctSurveySection.Rows)
                  {
                    SurveySection = DataRow_SurveySection["SurveySection"].ToString();

                    using (TableRow TableRow_SurveySection = new TableRow())
                    {
                      TableRow_SurveySection.VerticalAlign = VerticalAlign.Top;
                      Table_SurveyTable.Controls.Add(TableRow_SurveySection);

                      using (TableCell TableCell_SurveySection = new TableCell())
                      {
                        TableCell_SurveySection.ColumnSpan = 3;
                        using (Label Label_SurveySection = new Label())
                        {
                          Label_SurveySection.Text = SurveySection;

                          TableCell_SurveySection.Controls.Add(Label_SurveySection);
                          TableRow_SurveySection.Controls.Add(TableCell_SurveySection);

                          string SurveyQuestion = "";
                          string SurveyQuestionId = "";
                          string SurveyQuestionShowHideDependencyQuestionId = "";
                          string SurveyQuestionShowHideDependencyAnswerId = "";
                          string SurveyAnswer = "";
                          string SurveyAnswerId = "";
                          DataRow[] DataRow_SurveyQuestion = DataTable_Survey.Select("SurveySection = '" + SurveySection + "'");
                          foreach (DataRow DataRow_Question in DataRow_SurveyQuestion)
                          {
                            SurveyQuestionId = DataRow_Question["SurveyQuestionId"].ToString();
                            SurveyQuestion = DataRow_Question["SurveyQuestion"].ToString();
                            SurveyQuestionShowHideDependencyQuestionId = DataRow_Question["SurveyQuestion_ShowHideDependency_QuestionId"].ToString();
                            SurveyQuestionShowHideDependencyAnswerId = DataRow_Question["SurveyQuestion_ShowHideDependency_AnswerId"].ToString();
                            SurveyAnswerId = DataRow_Question["SurveyAnswerId"].ToString();
                            SurveyAnswer = DataRow_Question["SurveyAnswer"].ToString();

                            if (SurveyQuestion != LastSurveyQuestion)
                            {
                              using (TableRow TableRow_SurveyQuestion = new TableRow())
                              {
                                TableRow_SurveyQuestion.ID = "ShowHideSurveyQuestion_" + SurveyQuestionCount;
                                TableRow_SurveyQuestion.VerticalAlign = VerticalAlign.Top;
                                Table_SurveyTable.Controls.Add(TableRow_SurveyQuestion);

                                for (int a = 1; a <= 3; a++)
                                {
                                  using (TableCell TableCell_SurveyQuestion = new TableCell())
                                  {
                                    if (a == 1)
                                    {
                                      Label Label_SurveyQuestionSpace = new Label();
                                      Label_SurveyQuestionSpace.Text = Convert.ToString("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", CultureInfo.CurrentCulture);

                                      TableCell_SurveyQuestion.Wrap = false;
                                      TableCell_SurveyQuestion.Controls.Add(Label_SurveyQuestionSpace);
                                    }

                                    if (a == 2)
                                    {
                                      Label Label_SurveyQuestion = new Label();
                                      Label_SurveyQuestion.Text = SurveyQuestion;

                                      TableCell_SurveyQuestion.Wrap = true;
                                      TableCell_SurveyQuestion.ID = "FormSurveyQuestion_" + SurveyQuestionCount;
                                      TableCell_SurveyQuestion.Controls.Add(Label_SurveyQuestion);
                                    }

                                    if (a == 3)
                                    {
                                      Label Label_SurveyAnswer = new Label();
                                      Label_SurveyAnswer.ID = "Label_ItemSurveyQuestion_" + SurveyQuestionCount;
                                      Label_SurveyAnswer.Text = SurveyAnswer;
                                      Label_SurveyAnswer.Attributes.Add("SurveyQuestionId", SurveyQuestionId);
                                      Label_SurveyAnswer.Attributes.Add("SurveyQuestionShowHideDependencyQuestionId", SurveyQuestionShowHideDependencyQuestionId);
                                      Label_SurveyAnswer.Attributes.Add("SurveyQuestionShowHideDependencyAnswerId", SurveyQuestionShowHideDependencyAnswerId);
                                      Label_SurveyAnswer.Attributes.Add("SurveyAnswerId", SurveyAnswerId);

                                      TableCell_SurveyQuestion.Wrap = false;
                                      TableCell_SurveyQuestion.Controls.Add(Label_SurveyAnswer);

                                      SurveyQuestionCount = SurveyQuestionCount + 1;
                                    }

                                    TableRow_SurveyQuestion.Controls.Add(TableCell_SurveyQuestion);
                                  }
                                }
                              }

                              LastSurveyQuestion = SurveyQuestion;
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }

            ((HiddenField)FormView_PharmacySurveys_Form.FindControl("HiddenField_ItemControlCount")).Value = SurveyQuestionCount.ToString(CultureInfo.CurrentCulture);

            ((PlaceHolder)FormView_PharmacySurveys_Form.FindControl("PlaceHolder_ItemPharmacySurveys")).Controls.Add(Table_SurveyTable);
          }

          ScriptManager.RegisterStartupScript(UpdatePanel_PharmacySurveys, this.GetType(), "UpdateProgress_Start", "ShowHide_Form();", true);
        }
      }
    }


    private class FromDataBase_SecurityRole
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
      public DataRow[] SecurityFacilityAdminSurveyCreator { get; set; }
      public DataRow[] SecurityFacilityAdminSurveyAccess { get; set; }
    }

    private FromDataBase_SecurityRole GetSecurityRole()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_New = new FromDataBase_SecurityRole();

      string SQLStringFormMode = @" SELECT	SecurityRole_Id 
                                    FROM		vAdministration_SecurityAccess_Active 
                                    WHERE		(SecurityUser_UserName = @SecurityUser_UserName) 
				                                    AND (SecurityRole_Id = '1' OR Form_Id IN ('47')) 
				                                    AND (Facility_Id IN (SELECT Facility_Id FROM Form_PharmacySurveys_CreatedSurveys WHERE CreatedSurveys_Id = @CreatedSurveys_Id) OR (SecurityRole_Rank = 1))
                                    UNION
                                    SELECT	0 
                                    FROM		Form_PharmacySurveys_CreatedSurveys 
                                    WHERE		CreatedSurveys_Id = @CreatedSurveys_Id 
				                                    AND CreatedSurveys_UserName = @SecurityUser_UserName";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '186'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '187'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminSurveyCreator = DataTable_FormMode.Select("SecurityRole_Id = '188'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminSurveyAccess = DataTable_FormMode.Select("SecurityRole_Id = '0'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_FormViewUpdate
    {
      public string ViewUpdate { get; set; }
      public string CreatedSurveys_IsActive { get; set; }
      public string LoadedSurveys_IsActive { get; set; }
    }

    private FromDataBase_FormViewUpdate GetFormViewUpdate()
    {
      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_New = new FromDataBase_FormViewUpdate();

      string SQLStringFormViewUpdate = @" SELECT	'Yes' AS ViewUpdate , 
				                                          CASE WHEN CreatedSurveys_IsActive = 1 THEN 'Yes' ELSE 'No' END AS CreatedSurveys_IsActive ,
                                                  CASE WHEN LoadedSurveys_IsActive = 1 THEN 'Yes' ELSE 'No' END AS LoadedSurveys_IsActive
                                          FROM		vForm_PharmacySurveys_CreatedSurveys 
				                                          LEFT JOIN Form_PharmacySurveys_CreatedSurveyAnswers ON vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id
                                          WHERE		vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = @CreatedSurveys_Id 
				                                          AND vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_UserName = @CreatedSurveys_UserName
				                                          AND Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id IS NULL";
      using (SqlCommand SqlCommand_FormViewUpdate = new SqlCommand(SQLStringFormViewUpdate))
      {
        SqlCommand_FormViewUpdate.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
        SqlCommand_FormViewUpdate.Parameters.AddWithValue("@CreatedSurveys_UserName", Request.ServerVariables["LOGON_USER"]);
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
              FromDataBase_FormViewUpdate_New.CreatedSurveys_IsActive = DataRow_Row["CreatedSurveys_IsActive"].ToString();
              FromDataBase_FormViewUpdate_New.LoadedSurveys_IsActive = DataRow_Row["LoadedSurveys_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_FormViewUpdate_New;
    }

    private class FromDataBase_FormIsActive
    {
      public string CreatedSurveys_Name { get; set; }
      public string CreatedBy { get; set; }
      public string ModifiedBy { get; set; }
      public string CreatedSurveys_IsActive { get; set; }
      public string LoadedSurveys_IsActive { get; set; }
    }

    private FromDataBase_FormIsActive GetFormIsActive()
    {
      FromDataBase_FormIsActive FromDataBase_FormIsActive_New = new FromDataBase_FormIsActive();

      string SQLStringFormIsActive = @"SELECT	CreatedSurveys_Name ,
                                              A.SecurityUser_DisplayName AS CreatedBy ,
                                              B.SecurityUser_DisplayName AS ModifiedBy ,
                                              CASE WHEN CreatedSurveys_IsActive = 1 THEN 'Yes' ELSE 'No' END AS CreatedSurveys_IsActive ,
                                              CASE WHEN LoadedSurveys_IsActive = 1 THEN 'Yes' ELSE 'No' END AS LoadedSurveys_IsActive
                                      FROM		vForm_PharmacySurveys_CreatedSurveys 
                                              LEFT JOIN Administration_SecurityUser A ON vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_CreatedBy = A.SecurityUser_UserName
                                              LEFT JOIN Administration_SecurityUser B ON vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_ModifiedBy = B.SecurityUser_UserName
                                      WHERE		vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = @CreatedSurveys_Id";
      using (SqlCommand SqlCommand_FormIsActive = new SqlCommand(SQLStringFormIsActive))
      {
        SqlCommand_FormIsActive.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
        DataTable DataTable_FormIsActive;
        using (DataTable_FormIsActive = new DataTable())
        {
          DataTable_FormIsActive.Locale = CultureInfo.CurrentCulture;
          DataTable_FormIsActive = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormIsActive).Copy();
          if (DataTable_FormIsActive.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormIsActive.Rows)
            {
              FromDataBase_FormIsActive_New.CreatedSurveys_Name = DataRow_Row["CreatedSurveys_Name"].ToString();
              FromDataBase_FormIsActive_New.CreatedBy = DataRow_Row["CreatedBy"].ToString();
              FromDataBase_FormIsActive_New.ModifiedBy = DataRow_Row["ModifiedBy"].ToString();
              FromDataBase_FormIsActive_New.CreatedSurveys_IsActive = DataRow_Row["CreatedSurveys_IsActive"].ToString();
              FromDataBase_FormIsActive_New.LoadedSurveys_IsActive = DataRow_Row["LoadedSurveys_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_FormIsActive_New;
    }


    //--START-- --TablePharmacySurveys--//
    //--START-- --Edit--//
    protected void FormView_PharmacySurveys_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDCreatedSurveysModifiedDate"] = e.OldValues["CreatedSurveys_ModifiedDate"];
        object OLDCreatedSurveysModifiedDate = Session["OLDCreatedSurveysModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDCreatedSurveysModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_ComparePharmacySurveys = (DataView)SqlDataSource_PharmacySurveys_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_ComparePharmacySurveys = DataView_ComparePharmacySurveys[0];
        Session["DBCreatedSurveysModifiedDate"] = Convert.ToString(DataRowView_ComparePharmacySurveys["CreatedSurveys_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBCreatedSurveysModifiedBy"] = Convert.ToString(DataRowView_ComparePharmacySurveys["CreatedSurveys_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBCreatedSurveysModifiedDate = Session["DBCreatedSurveysModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBCreatedSurveysModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacySurveys.SetFocus(UpdatePanel_PharmacySurveys);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBCreatedSurveysModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_PharmacySurveys_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_PharmacySurveys_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ToolkitScriptManager_PharmacySurveys.SetFocus(UpdatePanel_PharmacySurveys);
            ((Label)FormView_PharmacySurveys_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_PharmacySurveys_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["CreatedSurveys_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["CreatedSurveys_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];
            e.NewValues["Unit_Id"] = ((DropDownList)FormView_PharmacySurveys_Form.FindControl("DropDownList_EditUnit")).SelectedValue;

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_PharmacySurveys_CreatedSurveys", "CreatedSurveys_Id = " + Request.QueryString["CreatedSurveysId"]);

            DataView DataView_PharmacySurveys = (DataView)SqlDataSource_PharmacySurveys_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_PharmacySurveys = DataView_PharmacySurveys[0];
            Session["CreatedSurveysHistory"] = Convert.ToString(DataRowView_PharmacySurveys["CreatedSurveys_History"], CultureInfo.CurrentCulture);

            Session["CreatedSurveysHistory"] = Session["History"].ToString() + Session["CreatedSurveysHistory"].ToString();
            e.NewValues["CreatedSurveys_History"] = Session["CreatedSurveysHistory"].ToString();

            Session["CreatedSurveysHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDCreatedSurveysModifiedDate"] = "";
        Session["DBCreatedSurveysModifiedDate"] = "";
        Session["DBCreatedSurveysModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_PharmacySurveys_Form.FindControl("DropDownList_EditUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_Form.FindControl("TextBox_EditDesignation")).Text))
        {
          InvalidForm = "Yes";
        }

        Int32 ControlCount = Convert.ToInt32(((HiddenField)FormView_PharmacySurveys_Form.FindControl("HiddenField_EditControlCount")).Value, CultureInfo.CurrentCulture);
        for (int a = 1; a < ControlCount; a++)
        {
          string SurveyQuestionShowHideDependencyQuestionId = ((RadioButtonList)FormView_PharmacySurveys_Form.FindControl("RadioButtonList_EditSurveyQuestion_" + a)).Attributes["SurveyQuestionShowHideDependencyQuestionId"];
          string SurveyQuestionShowHideDependencyAnswerId = ((RadioButtonList)FormView_PharmacySurveys_Form.FindControl("RadioButtonList_EditSurveyQuestion_" + a)).Attributes["SurveyQuestionShowHideDependencyAnswerId"];
          string Find_SurveyAnswerId = "";
          if (string.IsNullOrEmpty(SurveyQuestionShowHideDependencyQuestionId))
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_PharmacySurveys_Form.FindControl("RadioButtonList_EditSurveyQuestion_" + a)).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
          else
          {
            for (int b = 1; b < ControlCount; b++)
            {
              string Find_SurveyQuestionId = ((RadioButtonList)FormView_PharmacySurveys_Form.FindControl("RadioButtonList_EditSurveyQuestion_" + b)).Attributes["SurveyQuestionId"];

              if (SurveyQuestionShowHideDependencyQuestionId == Find_SurveyQuestionId)
              {
                Find_SurveyAnswerId = ((RadioButtonList)FormView_PharmacySurveys_Form.FindControl("RadioButtonList_EditSurveyQuestion_" + b)).SelectedValue;
                break;
              }
            }

            if (SurveyQuestionShowHideDependencyAnswerId == Find_SurveyAnswerId)
            {
              if (string.IsNullOrEmpty(((RadioButtonList)FormView_PharmacySurveys_Form.FindControl("RadioButtonList_EditSurveyQuestion_" + a)).SelectedValue))
              {
                InvalidForm = "Yes";
              }
            }
          }
        }
      }
      
      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
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

      string ValidUpdate = "";
      string LoadedSurveysFY = "";
      string SQLStringValidUpdate = @"SELECT	LoadedSurveys_FY , 
                                              CASE 
					                                      WHEN LoadedSurveys_FY = 
						                                      CASE 
							                                      WHEN MONTH(GETDATE()) IN ('1','2','3','4','5','6','7','8','9') THEN CAST((YEAR(GETDATE()) + 0) AS NVARCHAR(10))
							                                      WHEN MONTH(GETDATE()) IN ('10','11','12') THEN CAST((YEAR(GETDATE()) + 1) AS NVARCHAR(10))
						                                      END
					                                      THEN 'Yes'
					                                      ELSE 'No' 
				                                      END AS ValidUpdate
                                      FROM		Form_PharmacySurveys_CreatedSurveys
				                                      LEFT JOIN Form_PharmacySurveys_LoadedSurveys ON Form_PharmacySurveys_CreatedSurveys.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id
                                      WHERE		CreatedSurveys_Id = @CreatedSurveys_Id";
      using (SqlCommand SqlCommand_ValidUpdate = new SqlCommand(SQLStringValidUpdate))
      {
        SqlCommand_ValidUpdate.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
        DataTable DataTable_ValidUpdate;
        using (DataTable_ValidUpdate = new DataTable())
        {
          DataTable_ValidUpdate.Locale = CultureInfo.CurrentCulture;
          DataTable_ValidUpdate = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ValidUpdate).Copy();
          if (DataTable_ValidUpdate.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ValidUpdate.Rows)
            {
              ValidUpdate = DataRow_Row["ValidUpdate"].ToString();
              LoadedSurveysFY = DataRow_Row["LoadedSurveys_FY"].ToString();
            }
          }
        }
      }

      if (ValidUpdate != "Yes")
      {
        InvalidFormMessage = InvalidFormMessage + "Survey was for Financial Year " + LoadedSurveysFY + " and can only be completed in Financial Year " + LoadedSurveysFY + "<br />";
      }

      ValidUpdate = "";
      LoadedSurveysFY = "";

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacySurveys_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            Int32 Question_Count = Convert.ToInt32(((HiddenField)FormView_PharmacySurveys_Form.FindControl("HiddenField_EditControlCount")).Value, CultureInfo.CurrentCulture);
            for (Int32 a = 1; a < Question_Count; a++)
            {
              if (!string.IsNullOrEmpty(((RadioButtonList)FormView_PharmacySurveys_Form.FindControl("RadioButtonList_EditSurveyQuestion_" + a)).SelectedValue.ToString()))
              {
                string SQLStringInsertSurveyQuestion = "INSERT INTO Form_PharmacySurveys_CreatedSurveyAnswers ( CreatedSurveys_Id , LoadedAnswers_Id ) VALUES ( @CreatedSurveys_Id , @LoadedAnswers_Id )";
                using (SqlCommand SqlCommand_InsertSurveyQuestion = new SqlCommand(SQLStringInsertSurveyQuestion))
                {
                  SqlCommand_InsertSurveyQuestion.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
                  SqlCommand_InsertSurveyQuestion.Parameters.AddWithValue("@LoadedAnswers_Id", ((RadioButtonList)FormView_PharmacySurveys_Form.FindControl("RadioButtonList_EditSurveyQuestion_" + a)).SelectedValue.ToString());
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertSurveyQuestion);
                }
              }
            }


            //--START-- --Email--//
            string SecurityUserEmail = "";
            string SecurityUserDisplayName = "";
            string CreatedSurveysId = "";
            string FacilityFacilityDisplayName = "";
            string LoadedSurveysName = "";
            string LoadedSurveysFY = "";
            string CreatedSurveysCreatedBy = "";
            string CreatedSurveysName = "";
            string CreatedSurveysEmail = "";
            string CreatedSurveysUserName = "";
            string SQLStringCompletedSurvey = @"SELECT	vAdministration_SecurityAccess_Active.SecurityUser_Email ,
                                                        vAdministration_SecurityAccess_Active.SecurityUser_DisplayName ,
                                                        vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id ,
                                                        vForm_PharmacySurveys_CreatedSurveys.Facility_FacilityDisplayName ,
                                                        LoadedSurveys_Name ,
                                                        LoadedSurveys_FY ,
                                                        CreatedSurveys_CreatedBy ,
                                                        CreatedSurveys_Name ,
                                                        CreatedSurveys_Email ,
                                                        CreatedSurveys_UserName
                                                FROM		vForm_PharmacySurveys_CreatedSurveys 
                                                        OUTER APPLY 
                                                        ( 
                                                          SELECT	TOP 1 * 
                                                          FROM		Form_PharmacySurveys_CreatedSurveyAnswers 
                                                          WHERE		Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id = vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id
                                                        ) TempTable
                                                        LEFT JOIN vAdministration_SecurityAccess_Active ON vForm_PharmacySurveys_CreatedSurveys.Facility_Id = vAdministration_SecurityAccess_Active.Facility_Id
                                                WHERE		vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = @CreatedSurveys_Id
                                                        AND CreatedSurveyAnswers_Id IS NOT NULL
                                                        AND vAdministration_SecurityAccess_Active.SecurityRole_Id = 188
                                                UNION 
                                                SELECT	vAdministration_SecurityAccess_Active.SecurityUser_Email ,
                                                        vAdministration_SecurityAccess_Active.SecurityUser_DisplayName ,
                                                        vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id ,
                                                        vForm_PharmacySurveys_CreatedSurveys.Facility_FacilityDisplayName ,
                                                        LoadedSurveys_Name ,
                                                        LoadedSurveys_FY ,
                                                        CreatedSurveys_CreatedBy ,
                                                        CreatedSurveys_Name ,
                                                        CreatedSurveys_Email ,
                                                        CreatedSurveys_UserName
                                                FROM		vForm_PharmacySurveys_CreatedSurveys
				                                                OUTER APPLY 
                                                        ( 
                                                          SELECT	TOP 1 * 
                                                          FROM		Form_PharmacySurveys_CreatedSurveyAnswers 
                                                          WHERE		Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id = vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id
                                                        ) TempTable
				                                                LEFT JOIN vAdministration_SecurityAccess_Active ON vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_CreatedBy = vAdministration_SecurityAccess_Active.SecurityUser_UserName
                                                WHERE		vForm_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id = @CreatedSurveys_Id
                                                        AND CreatedSurveyAnswers_Id IS NOT NULL";
            using (SqlCommand SqlCommand_CompletedSurvey = new SqlCommand(SQLStringCompletedSurvey))
            {
              SqlCommand_CompletedSurvey.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
              DataTable DataTable_CompletedSurvey;
              using (DataTable_CompletedSurvey = new DataTable())
              {
                DataTable_CompletedSurvey.Locale = CultureInfo.CurrentCulture;
                DataTable_CompletedSurvey = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CompletedSurvey).Copy();
                if (DataTable_CompletedSurvey.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_CompletedSurvey.Rows)
                  {
                    SecurityUserEmail = DataRow_Row["SecurityUser_Email"].ToString();
                    SecurityUserDisplayName = DataRow_Row["SecurityUser_DisplayName"].ToString();
                    CreatedSurveysId = DataRow_Row["CreatedSurveys_Id"].ToString();
                    FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                    LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
                    LoadedSurveysFY = DataRow_Row["LoadedSurveys_FY"].ToString();
                    CreatedSurveysCreatedBy = DataRow_Row["CreatedSurveys_CreatedBy"].ToString();
                    CreatedSurveysName = DataRow_Row["CreatedSurveys_Name"].ToString();
                    CreatedSurveysEmail = DataRow_Row["CreatedSurveys_Email"].ToString();
                    CreatedSurveysUserName = DataRow_Row["CreatedSurveys_UserName"].ToString();
                  }
                }
              }
            }

            string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("87");
            string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
            string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("47");
            string BodyString = EmailTemplate;

            BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + SecurityUserDisplayName + "");
            BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
            BodyString = BodyString.Replace(";replace;FacilityName;replace;", "" + FacilityFacilityDisplayName + "");
            BodyString = BodyString.Replace(";replace;SurveyName;replace;", "" + LoadedSurveysName + "");
            BodyString = BodyString.Replace(";replace;SurveyFY;replace;", "" + LoadedSurveysFY + "");
            BodyString = BodyString.Replace(";replace;SurveyCreatedBy;replace;", "" + CreatedSurveysCreatedBy + "");
            BodyString = BodyString.Replace(";replace;EmployeeName;replace;", "" + CreatedSurveysName + "");
            BodyString = BodyString.Replace(";replace;EmployeeEmail;replace;", "" + CreatedSurveysEmail + "");
            BodyString = BodyString.Replace(";replace;EmployeeUserName;replace;", "" + CreatedSurveysUserName + "");
            BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
            BodyString = BodyString.Replace(";replace;CreatedSurveysId;replace;", "" + CreatedSurveysId + "");

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

            SecurityUserEmail = "";
            SecurityUserDisplayName = "";
            CreatedSurveysId = "";
            FacilityFacilityDisplayName = "";
            LoadedSurveysName = "";
            LoadedSurveysFY = "";
            CreatedSurveysCreatedBy = "";
            CreatedSurveysName = "";
            CreatedSurveysEmail = "";
            CreatedSurveysUserName = "";
            //---END--- --Email--//


            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Updated", "InfoQuest_Updated.aspx?UpdatedPage=Form_PharmacySurveys&UpdatedNumber=" + Request.QueryString["CreatedSurveysId"] + ""), false);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacySurveys, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Email", "InfoQuest_Email.aspx?EmailPage=Form_PharmacySurveys&EmailValue=" + Request.QueryString["CreatedSurveysId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_PharmacySurveys, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_PharmacySurveys_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["CreatedSurveysId"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Form", "Form_PharmacySurveys.aspx?CreatedSurveysId=" + Request.QueryString["CreatedSurveysId"] + ""), false);
          }
        }
      }
    }

    protected void FormView_PharmacySurveys_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_PharmacySurveys_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_PharmacySurveys_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      if (Request.QueryString["CreatedSurveysId"] != null)
      {
        string FacilityFacilityDisplayName = "";
        string LoadedSurveysName = "";
        string LoadedSurveysFY = "";
        string SQLStringPharmacySurveys = "SELECT Facility_FacilityDisplayName , LoadedSurveys_Name , LoadedSurveys_FY FROM vForm_PharmacySurveys_CreatedSurveys WHERE CreatedSurveys_Id = @CreatedSurveys_Id";
        using (SqlCommand SqlCommand_PharmacySurveys = new SqlCommand(SQLStringPharmacySurveys))
        {
          SqlCommand_PharmacySurveys.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
          DataTable DataTable_PharmacySurveys;
          using (DataTable_PharmacySurveys = new DataTable())
          {
            DataTable_PharmacySurveys.Locale = CultureInfo.CurrentCulture;
            DataTable_PharmacySurveys = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PharmacySurveys).Copy();
            if (DataTable_PharmacySurveys.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PharmacySurveys.Rows)
              {
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
                LoadedSurveysFY = DataRow_Row["LoadedSurveys_FY"].ToString();
              }
            }
          }
        }

        ((Label)FormView_PharmacySurveys_Form.FindControl("Label_EditFacility")).Text = FacilityFacilityDisplayName;
        ((Label)FormView_PharmacySurveys_Form.FindControl("Label_EditSurvey")).Text = LoadedSurveysName;
        ((Label)FormView_PharmacySurveys_Form.FindControl("Label_EditFY")).Text = LoadedSurveysFY;

        FacilityFacilityDisplayName = "";
        LoadedSurveysName = "";
        LoadedSurveysFY = "";


        string Email = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 47";
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
              }
            }
          }
        }

        if (Email == "False")
        {
          ((Button)FormView_PharmacySurveys_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacySurveys_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["CreatedSurveysId"] != null)
      {
        string FacilityFacilityDisplayName = "";
        string LoadedSurveysName = "";
        string LoadedSurveysFY = "";
        string UnitName = "";
        string SQLStringPharmacySurveys = "SELECT Facility_FacilityDisplayName , LoadedSurveys_Name , LoadedSurveys_FY , Unit_Name FROM vForm_PharmacySurveys_CreatedSurveys WHERE CreatedSurveys_Id = @CreatedSurveys_Id";
        using (SqlCommand SqlCommand_PharmacySurveys = new SqlCommand(SQLStringPharmacySurveys))
        {
          SqlCommand_PharmacySurveys.Parameters.AddWithValue("@CreatedSurveys_Id", Request.QueryString["CreatedSurveysId"]);
          DataTable DataTable_PharmacySurveys;
          using (DataTable_PharmacySurveys = new DataTable())
          {
            DataTable_PharmacySurveys.Locale = CultureInfo.CurrentCulture;
            DataTable_PharmacySurveys = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PharmacySurveys).Copy();
            if (DataTable_PharmacySurveys.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PharmacySurveys.Rows)
              {
                FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
                LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
                LoadedSurveysFY = DataRow_Row["LoadedSurveys_FY"].ToString();
                UnitName = DataRow_Row["Unit_Name"].ToString();
              }
            }
          }
        }

        ((Label)FormView_PharmacySurveys_Form.FindControl("Label_ItemFacility")).Text = FacilityFacilityDisplayName;
        ((Label)FormView_PharmacySurveys_Form.FindControl("Label_ItemSurvey")).Text = LoadedSurveysName;
        ((Label)FormView_PharmacySurveys_Form.FindControl("Label_ItemFY")).Text = LoadedSurveysFY;
        ((Label)FormView_PharmacySurveys_Form.FindControl("Label_ItemUnit")).Text = UnitName;

        FacilityFacilityDisplayName = "";
        LoadedSurveysName = "";
        LoadedSurveysFY = "";
        UnitName = "";


        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 47";
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
          ((Button)FormView_PharmacySurveys_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacySurveys_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_PharmacySurveys_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Print", "InfoQuest_Print.aspx?PrintPage=Form_PharmacySurveys&PrintValue=" + Request.QueryString["CreatedSurveysId"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_PharmacySurveys_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_PharmacySurveys_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_PharmacySurveys_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Email", "InfoQuest_Email.aspx?EmailPage=Form_PharmacySurveys&EmailValue=" + Request.QueryString["CreatedSurveysId"] + "") + "')";
        }

        Email = "";
        Print = "";
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_PharmacySurveysLoadedSurveysName"];
      string SearchField3 = Request.QueryString["Search_PharmacySurveysLoadedSurveysFY"];
      string SearchField4 = Request.QueryString["Search_PharmacySurveysCreatedSurveysName"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_PharmacySurveys_LoadedSurveysName=" + Request.QueryString["Search_PharmacySurveysLoadedSurveysName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_PharmacySurveys_LoadedSurveysFY=" + Request.QueryString["Search_PharmacySurveysLoadedSurveysFY"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_PharmacySurveys_CreatedSurveysName=" + Request.QueryString["Search_PharmacySurveysCreatedSurveysName"] + "&";
      }

      string FinalURL = "Form_PharmacySurveys_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Created Surveys", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Edit Controls--//
    protected void Button_EditCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_PharmacySurveys.aspx?CreatedSurveysId=" + Request.QueryString["CreatedSurveysId"] + "", false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }

    protected void Button_EditGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      Button_EditEmailClicked = true;
    }
    //---END--- --Edit Controls--//

    //--START-- --Item Controls--//
    protected void Button_ItemGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }
    //---END--- --Item Controls--//
    //---END--- --TablePharmacySurveys--//
  }
}