using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.ComponentModel;

namespace InfoQuestForm
{
  public partial class Form_PharmacySurveys_LoadedSurveys : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_PharmacySurveys_LoadedSurveys, this.GetType(), "UpdateProgress_Start", "Validation_Form_Surveys();Validation_Form_Sections();Validation_Form_Questions();Validation_Form_Answers();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("47").Replace(" Form", "")).ToString() + " : Loaded Surveys", CultureInfo.CurrentCulture);
          Label_HeadingGridSections.Text = Convert.ToString("List of Sections", CultureInfo.CurrentCulture);
          Label_HeadingGridQuestions.Text = Convert.ToString("List of Questions", CultureInfo.CurrentCulture);
          Label_HeadingGridAnswers.Text = Convert.ToString("List of Answers", CultureInfo.CurrentCulture);

          if (string.IsNullOrEmpty(Request.QueryString["Search_LoadedSurveysId"]) && string.IsNullOrEmpty(Request.QueryString["LoadedSurveys_Id"]) && string.IsNullOrEmpty(Request.QueryString["LoadedSections_Id"]) && string.IsNullOrEmpty(Request.QueryString["LoadedQuestions_Id"]) && string.IsNullOrEmpty(Request.QueryString["LoadedAnswers_Id"]))
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys List", "Form_PharmacySurveys_LoadedSurveys_List.aspx"), true);
          }

          SetVisibility();
          TableNavigationVisible();
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id IN ('186'))";
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

      InfoQuestWCF.InfoQuest_Exceptions.Exceptions(Exception_Error, Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"], "");
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
      InfoQuestWCF.InfoQuest_All.All_Maintenance("47");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_PharmacySurveys_LoadedSurveys.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Pharmacy Surveys", "23");
      }


    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertCommand = "INSERT INTO Form_PharmacySurveys_LoadedSurveys ( LoadedSurveys_Name , LoadedSurveys_FY , LoadedSurveys_CreatedDate , LoadedSurveys_CreatedBy , LoadedSurveys_ModifiedDate , LoadedSurveys_ModifiedBy , LoadedSurveys_History , LoadedSurveys_IsActive ) VALUES ( @LoadedSurveys_Name , @LoadedSurveys_FY , @LoadedSurveys_CreatedDate , @LoadedSurveys_CreatedBy , @LoadedSurveys_ModifiedDate , @LoadedSurveys_ModifiedBy , @LoadedSurveys_History , @LoadedSurveys_IsActive ); SELECT @LoadedSurveys_Id = SCOPE_IDENTITY()";
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.SelectCommand = "SELECT * FROM Form_PharmacySurveys_LoadedSurveys WHERE (LoadedSurveys_Id = @LoadedSurveys_Id)";
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.UpdateCommand = "UPDATE Form_PharmacySurveys_LoadedSurveys SET LoadedSurveys_Name = @LoadedSurveys_Name , LoadedSurveys_FY = @LoadedSurveys_FY , LoadedSurveys_ModifiedDate = @LoadedSurveys_ModifiedDate , LoadedSurveys_ModifiedBy = @LoadedSurveys_ModifiedBy , LoadedSurveys_History = @LoadedSurveys_History , LoadedSurveys_IsActive = @LoadedSurveys_IsActive WHERE LoadedSurveys_Id = @LoadedSurveys_Id";
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Add("LoadedSurveys_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters["LoadedSurveys_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Add("LoadedSurveys_Name", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Add("LoadedSurveys_FY", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Add("LoadedSurveys_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Add("LoadedSurveys_CreatedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Add("LoadedSurveys_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Add("LoadedSurveys_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Add("LoadedSurveys_History", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters["LoadedSurveys_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters.Add("LoadedSurveys_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.SelectParameters.Add("LoadedSurveys_Id", TypeCode.Int32, Request.QueryString["LoadedSurveys_Id"]);
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.UpdateParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.UpdateParameters.Add("LoadedSurveys_Name", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.UpdateParameters.Add("LoadedSurveys_FY", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.UpdateParameters.Add("LoadedSurveys_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.UpdateParameters.Add("LoadedSurveys_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.UpdateParameters.Add("LoadedSurveys_History", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.UpdateParameters.Add("LoadedSurveys_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacySurveys_LoadedSurveys_Form.UpdateParameters.Add("LoadedSurveys_Id", TypeCode.Int32, "");

      SqlDataSource_PharmacySurveys_LoadedSections_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedSections_List.SelectCommand = "spForm_Get_PharmacySurveys_LoadedSections_List";
      SqlDataSource_PharmacySurveys_LoadedSections_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacySurveys_LoadedSections_List.CancelSelectOnNullParameter = false;
      SqlDataSource_PharmacySurveys_LoadedSections_List.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedSections_List.SelectParameters.Add("LoadedSurveysId", TypeCode.String, Request.QueryString["LoadedSurveys_Id"]);

      SqlDataSource_PharmacySurveys_LoadedSections_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertCommand = "INSERT INTO Form_PharmacySurveys_LoadedSections ( LoadedSurveys_Id , LoadedSections_Name , LoadedSections_CreatedDate , LoadedSections_CreatedBy , LoadedSections_ModifiedDate , LoadedSections_ModifiedBy , LoadedSections_History , LoadedSections_IsActive ) VALUES ( @LoadedSurveys_Id , @LoadedSections_Name , @LoadedSections_CreatedDate , @LoadedSections_CreatedBy , @LoadedSections_ModifiedDate , @LoadedSections_ModifiedBy , @LoadedSections_History , @LoadedSections_IsActive ); SELECT @LoadedSections_Id = SCOPE_IDENTITY()";
      SqlDataSource_PharmacySurveys_LoadedSections_Form.SelectCommand = "SELECT * FROM Form_PharmacySurveys_LoadedSections WHERE (LoadedSections_Id = @LoadedSections_Id)";
      SqlDataSource_PharmacySurveys_LoadedSections_Form.UpdateCommand = "UPDATE Form_PharmacySurveys_LoadedSections SET LoadedSections_Name = @LoadedSections_Name , LoadedSections_ModifiedDate = @LoadedSections_ModifiedDate , LoadedSections_ModifiedBy = @LoadedSections_ModifiedBy , LoadedSections_History = @LoadedSections_History , LoadedSections_IsActive = @LoadedSections_IsActive WHERE LoadedSections_Id = @LoadedSections_Id";
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Add("LoadedSections_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters["LoadedSections_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Add("LoadedSurveys_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Add("LoadedSections_Name", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Add("LoadedSections_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Add("LoadedSections_CreatedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Add("LoadedSections_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Add("LoadedSections_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Add("LoadedSections_History", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters["LoadedSections_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters.Add("LoadedSections_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedSections_Form.SelectParameters.Add("LoadedSections_Id", TypeCode.Int32, Request.QueryString["LoadedSections_Id"]);
      SqlDataSource_PharmacySurveys_LoadedSections_Form.UpdateParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedSections_Form.UpdateParameters.Add("LoadedSections_Name", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.UpdateParameters.Add("LoadedSections_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.UpdateParameters.Add("LoadedSections_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.UpdateParameters.Add("LoadedSections_History", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.UpdateParameters.Add("LoadedSections_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacySurveys_LoadedSections_Form.UpdateParameters.Add("LoadedSections_Id", TypeCode.Int32, "");

      SqlDataSource_PharmacySurveys_LoadedQuestions_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedQuestions_List.SelectCommand = "spForm_Get_PharmacySurveys_LoadedQuestions_List";
      SqlDataSource_PharmacySurveys_LoadedQuestions_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacySurveys_LoadedQuestions_List.CancelSelectOnNullParameter = false;
      SqlDataSource_PharmacySurveys_LoadedQuestions_List.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_List.SelectParameters.Add("LoadedSectionsId", TypeCode.String, Request.QueryString["LoadedSections_Id"]);

      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Section.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Section.SelectCommand = @"SELECT		LoadedSections_Id , 
					                                                                                                                      LoadedSections_Name
                                                                                                                      FROM			Form_PharmacySurveys_LoadedSections
                                                                                                                      WHERE			LoadedSections_IsActive = 1
					                                                                                                                      AND LoadedSurveys_Id IN (
						                                                                                                                      SELECT	LoadedSurveys_Id
						                                                                                                                      FROM		Form_PharmacySurveys_LoadedSections
						                                                                                                                      WHERE		LoadedSections_Id = @LoadedSections_Id
					                                                                                                                      )
                                                                                                                      ORDER BY	LoadedSections_Name";
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Section.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Section.SelectParameters.Add("LoadedSections_Id", TypeCode.String, Request.QueryString["LoadedSections_Id"]);

      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Question.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Question.SelectCommand = @"SELECT		LoadedQuestions_Id ,
					                                                                                                                      LoadedQuestions_Name
                                                                                                                      FROM			Form_PharmacySurveys_LoadedQuestions
                                                                                                                      WHERE			LoadedQuestions_IsActive = 1
					                                                                                                                      AND LoadedSections_Id = @LoadedSections_Id
                                                                                                                      ORDER BY	LoadedQuestions_Name";
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Question.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Question.SelectParameters.Add("LoadedSections_Id", TypeCode.String, "");

      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId.SelectCommand = @"SELECT		LoadedAnswers_Id ,
					                                                                                                              LoadedAnswers_Name
                                                                                                              FROM			Form_PharmacySurveys_LoadedAnswers
                                                                                                              WHERE			LoadedAnswers_IsActive = 1
					                                                                                                              AND LoadedQuestions_Id = @LoadedQuestions_Id
                                                                                                              ORDER BY	LoadedAnswers_Name";
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId.SelectParameters.Add("LoadedQuestions_Id", TypeCode.String, "");

      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Section.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Section.SelectCommand = @"SELECT		LoadedSections_Id , 
					                                                                                                                    LoadedSections_Name + ' (' + 
					                                                                                                                    CASE LoadedSections_IsActive
						                                                                                                                    WHEN 1 THEN 'Yes'
						                                                                                                                    WHEN 0 THEN 'No'
					                                                                                                                    END + ')' AS LoadedSections_Name
                                                                                                                    FROM			Form_PharmacySurveys_LoadedSections
                                                                                                                    WHERE			LoadedSections_IsActive = 1
					                                                                                                                    AND LoadedSurveys_Id IN (
						                                                                                                                    SELECT LoadedSurveys_Id
						                                                                                                                    FROM Form_PharmacySurveys_LoadedSections
						                                                                                                                    WHERE LoadedSections_Id IN (
							                                                                                                                    SELECT	LoadedSections_Id
							                                                                                                                    FROM		Form_PharmacySurveys_LoadedQuestions
							                                                                                                                    WHERE		LoadedQuestions_Id = @LoadedQuestions_Id
						                                                                                                                    )
					                                                                                                                    )
                                                                                                                    UNION
                                                                                                                    SELECT		LoadedSections_Id , 
					                                                                                                                    LoadedSections_Name + ' (' + 
					                                                                                                                    CASE LoadedSections_IsActive
						                                                                                                                    WHEN 1 THEN 'Yes'
						                                                                                                                    WHEN 0 THEN 'No'
					                                                                                                                    END + ')' AS LoadedSections_Name
                                                                                                                    FROM			Form_PharmacySurveys_LoadedSections
                                                                                                                    WHERE			LoadedSections_Id IN (
						                                                                                                                    SELECT	LoadedSections_Id
						                                                                                                                    FROM		Form_PharmacySurveys_LoadedQuestions
						                                                                                                                    WHERE		LoadedQuestions_Id IN (
							                                                                                                                    SELECT	LoadedQuestions_Id
							                                                                                                                    FROM		Form_PharmacySurveys_LoadedAnswers
							                                                                                                                    WHERE		LoadedAnswers_Id IN (
								                                                                                                                    SELECT	LoadedQuestions_Dependency_ShowHide_LoadedAnswersId
								                                                                                                                    FROM		Form_PharmacySurveys_LoadedQuestions
								                                                                                                                    WHERE		LoadedQuestions_Id = @LoadedQuestions_Id
							                                                                                                                    )
						                                                                                                                    )
					                                                                                                                    )
                                                                                                                    ORDER BY	LoadedSections_Name";
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Section.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Section.SelectParameters.Add("LoadedQuestions_Id", TypeCode.String, Request.QueryString["LoadedQuestions_Id"]);

      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Question.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Question.SelectCommand = @"SELECT		LoadedQuestions_Id ,
					                                                                                                                    LoadedQuestions_Name + ' (' + 
					                                                                                                                    CASE LoadedQuestions_IsActive
						                                                                                                                    WHEN 1 THEN 'Yes'
						                                                                                                                    WHEN 0 THEN 'No'
					                                                                                                                    END + ')' AS LoadedQuestions_Name
                                                                                                                    FROM			Form_PharmacySurveys_LoadedQuestions
                                                                                                                    WHERE			LoadedQuestions_IsActive = 1
					                                                                                                                    AND LoadedSections_Id = @LoadedSections_Id
					                                                                                                                    AND LoadedQuestions_Id != @LoadedQuestions_Id
                                                                                                                    UNION
                                                                                                                    SELECT		LoadedQuestions_Id , 
					                                                                                                                    LoadedQuestions_Name + ' (' + 
					                                                                                                                    CASE LoadedQuestions_IsActive
						                                                                                                                    WHEN 1 THEN 'Yes'
						                                                                                                                    WHEN 0 THEN 'No'
					                                                                                                                    END + ')' AS LoadedQuestions_Name
                                                                                                                    FROM			Form_PharmacySurveys_LoadedQuestions
                                                                                                                    WHERE			LoadedQuestions_Id IN (
						                                                                                                                    SELECT	LoadedQuestions_Id
						                                                                                                                    FROM		Form_PharmacySurveys_LoadedAnswers
						                                                                                                                    WHERE		LoadedAnswers_Id IN (
							                                                                                                                    SELECT	LoadedQuestions_Dependency_ShowHide_LoadedAnswersId
							                                                                                                                    FROM		Form_PharmacySurveys_LoadedQuestions
							                                                                                                                    WHERE		LoadedQuestions_Id = @LoadedQuestions_Id
						                                                                                                                    )
					                                                                                                                    )
                                                                                                                    ORDER BY	LoadedQuestions_Name";
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Question.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Question.SelectParameters.Add("LoadedQuestions_Id", TypeCode.String, Request.QueryString["LoadedQuestions_Id"]);
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Question.SelectParameters.Add("LoadedSections_Id", TypeCode.String, "");

      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId.SelectCommand = @"SELECT		LoadedAnswers_Id ,
					                                                                                                            LoadedAnswers_Name + ' (' + 
					                                                                                                            CASE LoadedAnswers_IsActive
						                                                                                                            WHEN 1 THEN 'Yes'
						                                                                                                            WHEN 0 THEN 'No'
					                                                                                                            END + ')' AS LoadedAnswers_Name
                                                                                                            FROM			Form_PharmacySurveys_LoadedAnswers
                                                                                                            WHERE			LoadedAnswers_IsActive = 1
					                                                                                                            AND LoadedQuestions_Id = @LoadedQuestions_Id
                                                                                                            UNION
                                                                                                            SELECT		LoadedAnswers_Id , 
					                                                                                                            LoadedAnswers_Name + ' (' + 
					                                                                                                            CASE LoadedAnswers_IsActive
						                                                                                                            WHEN 1 THEN 'Yes'
						                                                                                                            WHEN 0 THEN 'No'
					                                                                                                            END + ')' AS LoadedAnswers_Name
                                                                                                            FROM			Form_PharmacySurveys_LoadedAnswers
                                                                                                            WHERE			LoadedAnswers_Id IN (
						                                                                                                            SELECT	LoadedQuestions_Dependency_ShowHide_LoadedAnswersId
						                                                                                                            FROM		Form_PharmacySurveys_LoadedQuestions
						                                                                                                            WHERE		LoadedQuestions_Id = @LoadedQuestionsId
					                                                                                                            )
                                                                                                            ORDER BY	LoadedAnswers_Name";
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId.SelectParameters.Add("LoadedQuestionsId", TypeCode.String, Request.QueryString["LoadedQuestions_Id"]);
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId.SelectParameters.Add("LoadedQuestions_Id", TypeCode.String, "");

      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertCommand = "INSERT INTO Form_PharmacySurveys_LoadedQuestions ( LoadedSections_Id , LoadedQuestions_Name , LoadedQuestions_Dependency_ShowHide_LoadedAnswersId , LoadedQuestions_CreatedDate , LoadedQuestions_CreatedBy , LoadedQuestions_ModifiedDate , LoadedQuestions_ModifiedBy , LoadedQuestions_History , LoadedQuestions_IsActive ) VALUES ( @LoadedSections_Id , @LoadedQuestions_Name , @LoadedQuestions_Dependency_ShowHide_LoadedAnswersId , @LoadedQuestions_CreatedDate , @LoadedQuestions_CreatedBy , @LoadedQuestions_ModifiedDate , @LoadedQuestions_ModifiedBy , @LoadedQuestions_History , @LoadedQuestions_IsActive ); SELECT @LoadedQuestions_Id = SCOPE_IDENTITY()";
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.SelectCommand = "SELECT Form_PharmacySurveys_LoadedQuestions.* , Questions.LoadedSections_Id AS DependencySection , Questions.LoadedQuestions_Id AS DependencyQuestion FROM Form_PharmacySurveys_LoadedQuestions LEFT JOIN Form_PharmacySurveys_LoadedAnswers AS Answers ON Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Dependency_ShowHide_LoadedAnswersId = Answers.LoadedAnswers_Id LEFT JOIN Form_PharmacySurveys_LoadedQuestions AS Questions ON Answers.LoadedQuestions_Id = Questions.LoadedQuestions_Id WHERE (Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id = @LoadedQuestions_Id)";
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateCommand = "UPDATE Form_PharmacySurveys_LoadedQuestions SET LoadedQuestions_Name = @LoadedQuestions_Name , LoadedQuestions_Dependency_ShowHide_LoadedAnswersId = @LoadedQuestions_Dependency_ShowHide_LoadedAnswersId , LoadedQuestions_ModifiedDate = @LoadedQuestions_ModifiedDate , LoadedQuestions_ModifiedBy = @LoadedQuestions_ModifiedBy , LoadedQuestions_History = @LoadedQuestions_History , LoadedQuestions_IsActive = @LoadedQuestions_IsActive WHERE LoadedQuestions_Id = @LoadedQuestions_Id";
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedQuestions_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedSections_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedQuestions_Name", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedQuestions_Dependency_ShowHide_LoadedAnswersId", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_Dependency_ShowHide_LoadedAnswersId"].ConvertEmptyStringToNull = true;
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedQuestions_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedQuestions_CreatedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedQuestions_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedQuestions_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedQuestions_History", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters.Add("LoadedQuestions_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.SelectParameters.Add("LoadedQuestions_Id", TypeCode.Int32, Request.QueryString["LoadedQuestions_Id"]);
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateParameters.Add("LoadedQuestions_Name", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateParameters.Add("LoadedQuestions_Dependency_ShowHide_LoadedAnswersId", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateParameters["LoadedQuestions_Dependency_ShowHide_LoadedAnswersId"].ConvertEmptyStringToNull = true;
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateParameters.Add("LoadedQuestions_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateParameters.Add("LoadedQuestions_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateParameters.Add("LoadedQuestions_History", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateParameters.Add("LoadedQuestions_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacySurveys_LoadedQuestions_Form.UpdateParameters.Add("LoadedQuestions_Id", TypeCode.Int32, "");

      SqlDataSource_PharmacySurveys_LoadedAnswers_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedAnswers_List.SelectCommand = "spForm_Get_PharmacySurveys_LoadedAnswers_List";
      SqlDataSource_PharmacySurveys_LoadedAnswers_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacySurveys_LoadedAnswers_List.CancelSelectOnNullParameter = false;
      SqlDataSource_PharmacySurveys_LoadedAnswers_List.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedAnswers_List.SelectParameters.Add("LoadedQuestionsId", TypeCode.String, Request.QueryString["LoadedQuestions_Id"]);

      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertCommand = "INSERT INTO Form_PharmacySurveys_LoadedAnswers ( LoadedQuestions_Id , LoadedAnswers_Name , LoadedAnswers_Score , LoadedAnswers_CreatedDate , LoadedAnswers_CreatedBy , LoadedAnswers_ModifiedDate , LoadedAnswers_ModifiedBy , LoadedAnswers_History , LoadedAnswers_IsActive ) VALUES ( @LoadedQuestions_Id , @LoadedAnswers_Name , @LoadedAnswers_Score , @LoadedAnswers_CreatedDate , @LoadedAnswers_CreatedBy , @LoadedAnswers_ModifiedDate , @LoadedAnswers_ModifiedBy , @LoadedAnswers_History , @LoadedAnswers_IsActive ); SELECT @LoadedAnswers_Id = SCOPE_IDENTITY()";
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.SelectCommand = "SELECT * FROM Form_PharmacySurveys_LoadedAnswers WHERE (LoadedAnswers_Id = @LoadedAnswers_Id)";
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.UpdateCommand = "UPDATE Form_PharmacySurveys_LoadedAnswers SET LoadedAnswers_Name = @LoadedAnswers_Name , LoadedAnswers_Score = @LoadedAnswers_Score , LoadedAnswers_ModifiedDate = @LoadedAnswers_ModifiedDate , LoadedAnswers_ModifiedBy = @LoadedAnswers_ModifiedBy , LoadedAnswers_History = @LoadedAnswers_History , LoadedAnswers_IsActive = @LoadedAnswers_IsActive WHERE LoadedAnswers_Id = @LoadedAnswers_Id";
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedAnswers_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters["LoadedAnswers_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedQuestions_Id", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedAnswers_Name", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedAnswers_Score", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedAnswers_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedAnswers_CreatedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedAnswers_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedAnswers_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedAnswers_History", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters["LoadedAnswers_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters.Add("LoadedAnswers_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.SelectParameters.Add("LoadedAnswers_Id", TypeCode.Int32, Request.QueryString["LoadedAnswers_Id"]);
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.UpdateParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.UpdateParameters.Add("LoadedAnswers_Name", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.UpdateParameters.Add("LoadedAnswers_Score", TypeCode.Int32, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.UpdateParameters.Add("LoadedAnswers_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.UpdateParameters.Add("LoadedAnswers_ModifiedBy", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.UpdateParameters.Add("LoadedAnswers_History", TypeCode.String, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.UpdateParameters.Add("LoadedAnswers_IsActive", TypeCode.Boolean, "");
      SqlDataSource_PharmacySurveys_LoadedAnswers_Form.UpdateParameters.Add("LoadedAnswers_Id", TypeCode.Int32, "");
    }


    private void SetVisibility()
    {
      TableNavigation.Visible = false;
      Button_AddSection.Visible = false;
      Button_AddQuestion.Visible = false;
      Button_AddAnswer.Visible = false;

      DivFormSurveys.Visible = false;
      TableFormSurveys.Visible = false;
      DivGridSections.Visible = false;
      TableGridSections.Visible = false;
      DivFormSections.Visible = false;
      TableFormSections.Visible = false;
      DivGridQuestions.Visible = false;
      TableGridQuestions.Visible = false;
      DivFormQuestions.Visible = false;
      TableFormQuestions.Visible = false;
      DivGridAnswers.Visible = false;
      TableGridAnswers.Visible = false;
      DivFormAnswers.Visible = false;
      TableFormAnswers.Visible = false;
      
      if (!string.IsNullOrEmpty(Request.QueryString["LoadedSurveys_Id"]) && string.IsNullOrEmpty(Request.QueryString["LoadedSections_Id"]))
      {
        TableNavigation.Visible = true;
        Hyperlink_NavigationSurveys.Visible = false;
        Hyperlink_NavigationSections.Visible = false;
        Hyperlink_NavigationQuestions.Visible = false;        


        DivFormSurveys.Visible = true;
        TableFormSurveys.Visible = true;

        if (Convert.ToInt32(Request.QueryString["LoadedSurveys_Id"], CultureInfo.CurrentCulture) == 0)
        {
          Label_HeadingFormSurveys.Text = Convert.ToString("Create Survey", CultureInfo.CurrentCulture);
          FormView_PharmacySurveys_LoadedSurveys_Form.ChangeMode(FormViewMode.Insert);
        }
        else if (Convert.ToInt32(Request.QueryString["LoadedSurveys_Id"], CultureInfo.CurrentCulture) > 0)
        {
          Button_AddSection.Visible = true;

          DivGridSections.Visible = true;
          TableGridSections.Visible = true;
          Label_HeadingFormSurveys.Text = Convert.ToString("Modify Survey", CultureInfo.CurrentCulture);
          FormView_PharmacySurveys_LoadedSurveys_Form.ChangeMode(FormViewMode.Edit);
        }
      }

      if (!string.IsNullOrEmpty(Request.QueryString["LoadedSections_Id"]) && string.IsNullOrEmpty(Request.QueryString["LoadedQuestions_Id"]))
      {
        TableNavigation.Visible = true;
        Hyperlink_NavigationSurveys.Visible = true;
        Hyperlink_NavigationSections.Visible = false;
        Hyperlink_NavigationQuestions.Visible = false;
        Button_AddSection.Visible = true;


        DivFormSections.Visible = true;
        TableFormSections.Visible = true;

        if (Convert.ToInt32(Request.QueryString["LoadedSections_Id"], CultureInfo.CurrentCulture) == 0)
        {
          DivGridSections.Visible = true;
          TableGridSections.Visible = true;
          Label_HeadingFormSections.Text = Convert.ToString("Create Section", CultureInfo.CurrentCulture);
          FormView_PharmacySurveys_LoadedSections_Form.ChangeMode(FormViewMode.Insert);
        }
        else if (Convert.ToInt32(Request.QueryString["LoadedSections_Id"], CultureInfo.CurrentCulture) > 0)
        {
          Button_AddQuestion.Visible = true;

          DivGridQuestions.Visible = true;
          TableGridQuestions.Visible = true;
          Label_HeadingFormSections.Text = Convert.ToString("Modify Section", CultureInfo.CurrentCulture);
          FormView_PharmacySurveys_LoadedSections_Form.ChangeMode(FormViewMode.Edit);
        }
      }

      if (!string.IsNullOrEmpty(Request.QueryString["LoadedQuestions_Id"]) && string.IsNullOrEmpty(Request.QueryString["LoadedAnswers_Id"]))
      {
        TableNavigation.Visible = true;
        Hyperlink_NavigationSurveys.Visible = true;
        Hyperlink_NavigationSections.Visible = true;
        Hyperlink_NavigationQuestions.Visible = false;
        Button_AddSection.Visible = true;
        Button_AddQuestion.Visible = true;


        DivFormQuestions.Visible = true;
        TableFormQuestions.Visible = true;

        if (Convert.ToInt32(Request.QueryString["LoadedQuestions_Id"], CultureInfo.CurrentCulture) == 0)
        {
          DivGridQuestions.Visible = true;
          TableGridQuestions.Visible = true;
          Label_HeadingFormQuestions.Text = Convert.ToString("Create Question", CultureInfo.CurrentCulture);
          FormView_PharmacySurveys_LoadedQuestions_Form.ChangeMode(FormViewMode.Insert);
        }
        else if (Convert.ToInt32(Request.QueryString["LoadedQuestions_Id"], CultureInfo.CurrentCulture) > 0)
        {
          Button_AddAnswer.Visible = true;

          DivGridAnswers.Visible = true;
          TableGridAnswers.Visible = true;
          Label_HeadingFormQuestions.Text = Convert.ToString("Modify Question", CultureInfo.CurrentCulture);
          FormView_PharmacySurveys_LoadedQuestions_Form.ChangeMode(FormViewMode.Edit);
        }
      }

      if (!string.IsNullOrEmpty(Request.QueryString["LoadedAnswers_Id"]))
      {
        TableNavigation.Visible = true;
        Hyperlink_NavigationSurveys.Visible = true;
        Hyperlink_NavigationSections.Visible = true;
        Hyperlink_NavigationQuestions.Visible = true;
        Button_AddSection.Visible = true;
        Button_AddQuestion.Visible = true;
        Button_AddAnswer.Visible = true;


        DivFormAnswers.Visible = true;
        TableFormAnswers.Visible = true;

        if (Convert.ToInt32(Request.QueryString["LoadedAnswers_Id"], CultureInfo.CurrentCulture) == 0)
        {
          DivGridAnswers.Visible = true;
          TableGridAnswers.Visible = true;
          Label_HeadingFormAnswers.Text = Convert.ToString("Create Answer", CultureInfo.CurrentCulture);
          FormView_PharmacySurveys_LoadedAnswers_Form.ChangeMode(FormViewMode.Insert);
        }
        else if (Convert.ToInt32(Request.QueryString["LoadedAnswers_Id"], CultureInfo.CurrentCulture) > 0)
        {
          Label_HeadingFormAnswers.Text = Convert.ToString("Modify Answer", CultureInfo.CurrentCulture);
          FormView_PharmacySurveys_LoadedAnswers_Form.ChangeMode(FormViewMode.Edit);
        }
      }
    }

    private void TableNavigationVisible()
    {
      FromDataBase_Navigation FromDataBase_Navigation_Current = GetNavigation();
      string LoadedSurveysId = FromDataBase_Navigation_Current.LoadedSurveysId;
      string LoadedSurveysName = FromDataBase_Navigation_Current.LoadedSurveysName;
      string LoadedSectionsId = FromDataBase_Navigation_Current.LoadedSectionsId;
      string LoadedSectionsName = FromDataBase_Navigation_Current.LoadedSectionsName;
      string LoadedQuestionsId = FromDataBase_Navigation_Current.LoadedQuestionsId;
      string LoadedQuestionsName = FromDataBase_Navigation_Current.LoadedQuestionsName;

      Hyperlink_NavigationSurveys.Text = Convert.ToString("<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSurveys_Id=" + LoadedSurveysId + "") + "'>" + LoadedSurveysName + "</a>", CultureInfo.CurrentCulture);
      Hyperlink_NavigationSections.Text = Convert.ToString("<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSections_Id=" + LoadedSectionsId + "") + "'>" + LoadedSectionsName + "</a>", CultureInfo.CurrentCulture);
      Hyperlink_NavigationQuestions.Text = Convert.ToString("<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedQuestions_Id=" + LoadedQuestionsId + "") + "'>" + LoadedQuestionsName + "</a>", CultureInfo.CurrentCulture);
    }

    protected void TableFormVisible()
    {
      if (TableFormSurveys.Visible == true)
      {
        if (FormView_PharmacySurveys_LoadedSurveys_Form.CurrentMode == FormViewMode.Insert)
        {
          ((TextBox)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnChange", "Validation_Form_Surveys();");
          ((TextBox)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnInput", "Validation_Form_Surveys();");
        }
        else if (FormView_PharmacySurveys_LoadedSurveys_Form.CurrentMode == FormViewMode.Insert)
        {
          ((TextBox)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("TextBox_EditName")).Attributes.Add("OnChange", "Validation_Form_Surveys();");
          ((TextBox)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("TextBox_EditName")).Attributes.Add("OnInput", "Validation_Form_Surveys();");
        }
      }

      if (TableFormSections.Visible == true)
      {
        if (FormView_PharmacySurveys_LoadedSections_Form.CurrentMode == FormViewMode.Insert)
        {
          ((TextBox)FormView_PharmacySurveys_LoadedSections_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnChange", "Validation_Form_Sections();");
          ((TextBox)FormView_PharmacySurveys_LoadedSections_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnInput", "Validation_Form_Sections();");
        }
        else if (FormView_PharmacySurveys_LoadedSections_Form.CurrentMode == FormViewMode.Insert)
        {
          ((TextBox)FormView_PharmacySurveys_LoadedSections_Form.FindControl("TextBox_EditName")).Attributes.Add("OnChange", "Validation_Form_Sections();");
          ((TextBox)FormView_PharmacySurveys_LoadedSections_Form.FindControl("TextBox_EditName")).Attributes.Add("OnInput", "Validation_Form_Sections();");
        }
      }

      if (TableFormQuestions.Visible == true)
      {
        if (FormView_PharmacySurveys_LoadedQuestions_Form.CurrentMode == FormViewMode.Insert)
        {
          ((TextBox)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnChange", "Validation_Form_Questions();");
          ((TextBox)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnInput", "Validation_Form_Questions();");
        }
        else if (FormView_PharmacySurveys_LoadedQuestions_Form.CurrentMode == FormViewMode.Insert)
        {
          ((TextBox)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("TextBox_EditName")).Attributes.Add("OnChange", "Validation_Form_Questions();");
          ((TextBox)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("TextBox_EditName")).Attributes.Add("OnInput", "Validation_Form_Questions();");
        }
      }

      if (TableFormAnswers.Visible == true)
      {
        if (FormView_PharmacySurveys_LoadedAnswers_Form.CurrentMode == FormViewMode.Insert)
        {
          ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnChange", "Validation_Form_Answers();");
          ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnInput", "Validation_Form_Answers();");
          ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_InsertScore")).Attributes.Add("OnChange", "Validation_Form_Answers();");
          ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_InsertScore")).Attributes.Add("OnInput", "Validation_Form_Answers();");
        }
        else if (FormView_PharmacySurveys_LoadedAnswers_Form.CurrentMode == FormViewMode.Insert)
        {
          ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_EditName")).Attributes.Add("OnChange", "Validation_Form_Answers();");
          ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_EditName")).Attributes.Add("OnInput", "Validation_Form_Answers();");
          ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_EditScore")).Attributes.Add("OnChange", "Validation_Form_Answers();");
          ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_EditScore")).Attributes.Add("OnInput", "Validation_Form_Answers();");
        }
      }
    }

    private class FromDataBase_Navigation
    {
      public string LoadedSurveysId { get; set; }
      public string LoadedSurveysName { get; set; }
      public string LoadedSectionsId { get; set; }
      public string LoadedSectionsName { get; set; }
      public string LoadedQuestionsId { get; set; }
      public string LoadedQuestionsName { get; set; }
    }

    private FromDataBase_Navigation GetNavigation()
    {
      FromDataBase_Navigation FromDataBase_Navigation_New = new FromDataBase_Navigation();

      if (TableNavigation.Visible == true)
      {
        string SQLStringPharmacySurveys = @"SELECT	TOP 1 
				                                            Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id ,
				                                            Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Name + ' (' + Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_FY + ')' AS LoadedSurveys_Name ,
				                                            Form_PharmacySurveys_LoadedSections.LoadedSections_Id ,
				                                            Form_PharmacySurveys_LoadedSections.LoadedSections_Name , 
				                                            Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id ,
				                                            Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Name
                                            FROM		Form_PharmacySurveys_LoadedSurveys
				                                            LEFT JOIN Form_PharmacySurveys_LoadedSections ON Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSections.LoadedSurveys_Id
				                                            LEFT JOIN Form_PharmacySurveys_LoadedQuestions ON Form_PharmacySurveys_LoadedSections.LoadedSections_Id = Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id
				                                            LEFT JOIN Form_PharmacySurveys_LoadedAnswers ON Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id = Form_PharmacySurveys_LoadedAnswers.LoadedQuestions_Id
                                            WHERE		(Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id = @LoadedSurveys_Id OR @LoadedSurveys_Id IS NULL OR @LoadedSurveys_Id = '0')
				                                            AND (Form_PharmacySurveys_LoadedSections.LoadedSections_Id = @LoadedSections_Id OR @LoadedSections_Id IS NULL OR @LoadedSections_Id = '0')
				                                            AND (Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id = @LoadedQuestions_Id OR @LoadedQuestions_Id IS NULL OR @LoadedQuestions_Id = '0')
				                                            AND (Form_PharmacySurveys_LoadedAnswers.LoadedAnswers_Id = @LoadedAnswers_Id OR @LoadedAnswers_Id IS NULL OR @LoadedAnswers_Id = '0')";
        using (SqlCommand SqlCommand_PharmacySurveys = new SqlCommand(SQLStringPharmacySurveys))
        {
          SqlCommand_PharmacySurveys.Parameters.AddWithValue("@LoadedSurveys_Id", Request.QueryString["LoadedSurveys_Id"]);
          SqlCommand_PharmacySurveys.Parameters.AddWithValue("@LoadedSections_Id", Request.QueryString["LoadedSections_Id"]);
          SqlCommand_PharmacySurveys.Parameters.AddWithValue("@LoadedQuestions_Id", Request.QueryString["LoadedQuestions_Id"]);
          SqlCommand_PharmacySurveys.Parameters.AddWithValue("@LoadedAnswers_Id", Request.QueryString["LoadedAnswers_Id"]);
          DataTable DataTable_PharmacySurveys;
          using (DataTable_PharmacySurveys = new DataTable())
          {
            DataTable_PharmacySurveys.Locale = CultureInfo.CurrentCulture;
            DataTable_PharmacySurveys = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PharmacySurveys).Copy();
            if (DataTable_PharmacySurveys.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PharmacySurveys.Rows)
              {
                FromDataBase_Navigation_New.LoadedSurveysId = DataRow_Row["LoadedSurveys_Id"].ToString();
                FromDataBase_Navigation_New.LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
                FromDataBase_Navigation_New.LoadedSectionsId = DataRow_Row["LoadedSections_Id"].ToString();
                FromDataBase_Navigation_New.LoadedSectionsName = DataRow_Row["LoadedSections_Name"].ToString();
                FromDataBase_Navigation_New.LoadedQuestionsId = DataRow_Row["LoadedQuestions_Id"].ToString();
                FromDataBase_Navigation_New.LoadedQuestionsName = DataRow_Row["LoadedQuestions_Name"].ToString();
              }
            }
          }
        }
      }

      return FromDataBase_Navigation_New;
    }


    //--START-- --Navigation--//
    protected void Button_CreateSurvey_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSurveys_Id=0"), false);
    }

    protected void Button_AddSection_Click(object sender, EventArgs e)
    {
      FromDataBase_Navigation FromDataBase_Navigation_Current = GetNavigation();
      string LoadedSurveysId = FromDataBase_Navigation_Current.LoadedSurveysId;

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSurveys_Id=" + LoadedSurveysId + "&LoadedSections_Id=0"), false);
    }

    protected void Button_AddQuestion_Click(object sender, EventArgs e)
    {
      FromDataBase_Navigation FromDataBase_Navigation_Current = GetNavigation();
      string LoadedSectionsId = FromDataBase_Navigation_Current.LoadedSectionsId;

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSections_Id=" + LoadedSectionsId + "&LoadedQuestions_Id=0"), false);
    }

    protected void Button_AddAnswer_Click(object sender, EventArgs e)
    {
      FromDataBase_Navigation FromDataBase_Navigation_Current = GetNavigation();
      string LoadedQuestionsId = FromDataBase_Navigation_Current.LoadedQuestionsId;

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedQuestions_Id=" + LoadedQuestionsId + "&LoadedAnswers_Id=0"), false);
    }

    protected void Button_GoToLoadedSurveys_Click(object sender, EventArgs e)
    {
      string SearchField1 = Request.QueryString["Search_LoadedSurveysId"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_LoadedSurveys_Id=" + Request.QueryString["Search_LoadedSurveysId"] + "&";
      }

      string FinalURL = "Form_PharmacySurveys_LoadedSurveys_List.aspx?" + SearchField1;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys List", FinalURL);

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Navigation--//


    //--START-- --LoadedSurveys_Form--//
    //--START-- --Insert--//
    protected void FormView_PharmacySurveys_LoadedSurveys_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_LoadedSurveys();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);

          ((Label)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters["LoadedSurveys_FY"].DefaultValue = ((HiddenField)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("HiddenField_InsertFY")).Value;
          SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters["LoadedSurveys_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters["LoadedSurveys_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters["LoadedSurveys_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters["LoadedSurveys_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters["LoadedSurveys_History"].DefaultValue = "";
          SqlDataSource_PharmacySurveys_LoadedSurveys_Form.InsertParameters["LoadedSurveys_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation_LoadedSurveys()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("TextBox_InsertName")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string LoadedSurveysName = "";
        string LoadedSurveysFY = "";
        string SQLStringLoadedSurveys = @"SELECT	LoadedSurveys_Name ,
				                                          LoadedSurveys_FY
                                          FROM		Form_PharmacySurveys_LoadedSurveys
                                          WHERE		LoadedSurveys_Name = @LoadedSurveys_Name
				                                          AND LoadedSurveys_FY = @LoadedSurveys_FY
				                                          AND LoadedSurveys_IsActive = 1";
        using (SqlCommand SqlCommand_LoadedSurveys = new SqlCommand(SQLStringLoadedSurveys))
        {
          SqlCommand_LoadedSurveys.Parameters.AddWithValue("@LoadedSurveys_Name", ((TextBox)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("TextBox_InsertName")).Text);
          SqlCommand_LoadedSurveys.Parameters.AddWithValue("@LoadedSurveys_FY", ((HiddenField)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("HiddenField_InsertFY")).Value);
          DataTable DataTable_LoadedSurveys;
          using (DataTable_LoadedSurveys = new DataTable())
          {
            DataTable_LoadedSurveys.Locale = CultureInfo.CurrentCulture;
            DataTable_LoadedSurveys = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LoadedSurveys).Copy();
            if (DataTable_LoadedSurveys.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_LoadedSurveys.Rows)
              {
                LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
                LoadedSurveysFY = DataRow_Row["LoadedSurveys_FY"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(LoadedSurveysName))
        {
          InvalidFormMessage = InvalidFormMessage + "A Survey with the Name '" + LoadedSurveysName + "' already exists for FY '" + LoadedSurveysFY + "'<br />";
        }

        LoadedSurveysName = "";
        LoadedSurveysFY = "";
      }
      
      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacySurveys_LoadedSurveys_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string LoadedSurveys_Id = e.Command.Parameters["@LoadedSurveys_Id"].Value.ToString();
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSurveys_Id=" + LoadedSurveys_Id + ""), false);
      }
    }
    //---END--- --Insert--//

    //--START-- --Edit--//
    protected void FormView_PharmacySurveys_LoadedSurveys_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDLoadedSurveysModifiedDate"] = e.OldValues["LoadedSurveys_ModifiedDate"];
        object OLDLoadedSurveysModifiedDate = Session["OLDLoadedSurveysModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDLoadedSurveysModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareLoadedSurveys = (DataView)SqlDataSource_PharmacySurveys_LoadedSurveys_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareLoadedSurveys = DataView_CompareLoadedSurveys[0];
        Session["DBLoadedSurveysModifiedDate"] = Convert.ToString(DataRowView_CompareLoadedSurveys["LoadedSurveys_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBLoadedSurveysModifiedBy"] = Convert.ToString(DataRowView_CompareLoadedSurveys["LoadedSurveys_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBLoadedSurveysModifiedDate = Session["DBLoadedSurveysModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBLoadedSurveysModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBLoadedSurveysModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_LoadedSurveys();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);
            ((Label)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
            e.NewValues["LoadedSurveys_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["LoadedSurveys_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_PharmacySurveys_LoadedSurveys", "LoadedSurveys_Id = " + Request.QueryString["LoadedSurveys_Id"]);

            DataView DataView_LoadedSurveys = (DataView)SqlDataSource_PharmacySurveys_LoadedSurveys_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_LoadedSurveys = DataView_LoadedSurveys[0];
            Session["LoadedSurveysHistory"] = Convert.ToString(DataRowView_LoadedSurveys["LoadedSurveys_History"], CultureInfo.CurrentCulture);

            Session["LoadedSurveysHistory"] = Session["History"].ToString() + Session["LoadedSurveysHistory"].ToString();
            e.NewValues["LoadedSurveys_History"] = Session["LoadedSurveysHistory"].ToString();

            Session["LoadedSurveysHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDLoadedSurveysModifiedDate"] = "";
        Session["DBLoadedSurveysModifiedDate"] = "";
        Session["DBLoadedSurveysModifiedBy"] = "";
      }
    }

    protected string EditValidation_LoadedSurveys()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("TextBox_EditName")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string LoadedSurveysId = "";
        string LoadedSurveysName = "";
        string LoadedSurveysFY = "";
        string SQLStringLoadedSurveys = @"SELECT	LoadedSurveys_Id ,
                                                  LoadedSurveys_Name ,
				                                          LoadedSurveys_FY
                                          FROM		Form_PharmacySurveys_LoadedSurveys
                                          WHERE		LoadedSurveys_Name = @LoadedSurveys_Name
				                                          AND LoadedSurveys_FY = @LoadedSurveys_FY
				                                          AND LoadedSurveys_IsActive = 1";
        using (SqlCommand SqlCommand_LoadedSurveys = new SqlCommand(SQLStringLoadedSurveys))
        {
          SqlCommand_LoadedSurveys.Parameters.AddWithValue("@LoadedSurveys_Name", ((TextBox)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("TextBox_EditName")).Text);
          SqlCommand_LoadedSurveys.Parameters.AddWithValue("@LoadedSurveys_FY", ((HiddenField)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("HiddenField_EditFY")).Value);
          DataTable DataTable_LoadedSurveys;
          using (DataTable_LoadedSurveys = new DataTable())
          {
            DataTable_LoadedSurveys.Locale = CultureInfo.CurrentCulture;
            DataTable_LoadedSurveys = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LoadedSurveys).Copy();
            if (DataTable_LoadedSurveys.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_LoadedSurveys.Rows)
              {
                LoadedSurveysId = DataRow_Row["LoadedSurveys_Id"].ToString();
                LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
                LoadedSurveysFY = DataRow_Row["LoadedSurveys_FY"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(LoadedSurveysName))
        {
          if (LoadedSurveysId != Request.QueryString["LoadedSurveys_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Survey with the Name '" + LoadedSurveysName + "' already exists for FY '" + LoadedSurveysFY + "'<br />";
          }
        }

        LoadedSurveysId = "";
        LoadedSurveysName = "";
        LoadedSurveysFY = "";
      }
      
      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacySurveys_LoadedSurveys_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            if (((CheckBox)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("CheckBox_EditIsActive")).Checked == false)
            {
              string SQLStringUpdateLoadedSections = @" UPDATE	Form_PharmacySurveys_LoadedSections 
                                                        SET			LoadedSections_IsActive = 0
                                                        WHERE		LoadedSurveys_Id = @LoadedSurveys_Id";
              using (SqlCommand SqlCommand_UpdateLoadedSections = new SqlCommand(SQLStringUpdateLoadedSections))
              {
                SqlCommand_UpdateLoadedSections.Parameters.AddWithValue("@LoadedSurveys_Id", Request.QueryString["LoadedSurveys_Id"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateLoadedSections);
              }


              string SQLStringUpdateLoadedQuestions = @"UPDATE	Form_PharmacySurveys_LoadedQuestions
                                                        SET			LoadedQuestions_IsActive = 0
                                                        WHERE		LoadedSections_Id IN (
					                                                        SELECT	LoadedSections_Id
					                                                        FROM		Form_PharmacySurveys_LoadedSections 
					                                                        WHERE		LoadedSurveys_Id = @LoadedSurveys_Id
				                                                        )";
              using (SqlCommand SqlCommand_UpdateLoadedQuestions = new SqlCommand(SQLStringUpdateLoadedQuestions))
              {
                SqlCommand_UpdateLoadedQuestions.Parameters.AddWithValue("@LoadedSurveys_Id", Request.QueryString["LoadedSurveys_Id"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateLoadedQuestions);
              }


              string SQLStringUpdateLoadedAnswers = @"UPDATE	Form_PharmacySurveys_LoadedAnswers
                                                      SET			LoadedAnswers_IsActive = 0
                                                      WHERE		LoadedQuestions_Id IN (
					                                                      SELECT	LoadedQuestions_Id 
					                                                      FROM		Form_PharmacySurveys_LoadedQuestions
					                                                      WHERE		LoadedSections_Id IN (
										                                                      SELECT	LoadedSections_Id
										                                                      FROM		Form_PharmacySurveys_LoadedSections 
										                                                      WHERE		LoadedSurveys_Id = @LoadedSurveys_Id
									                                                      )
				                                                      )";
              using (SqlCommand SqlCommand_UpdateLoadedAnswers = new SqlCommand(SQLStringUpdateLoadedAnswers))
              {
                SqlCommand_UpdateLoadedAnswers.Parameters.AddWithValue("@LoadedSurveys_Id", Request.QueryString["LoadedSurveys_Id"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateLoadedAnswers);
              }
            }

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSurveys_Id=" + Request.QueryString["LoadedSurveys_Id"] + ""), false);
          }
        }
      }
    }
    //---END--- --Edit--//

    protected void FormView_PharmacySurveys_LoadedSurveys_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_PharmacySurveys_LoadedSurveys_Form.CurrentMode == FormViewMode.Insert)
      {
        if (Convert.ToInt32(Request.QueryString["LoadedSurveys_Id"], CultureInfo.CurrentCulture) == 0)
        {
          string FY = "";
          string SQLStringFY = @"SELECT	CASE 
					                                WHEN MONTH(GETDATE()) IN ('1','2','3','4','5','6','7','8','9') THEN CAST((YEAR(GETDATE()) + 0) AS NVARCHAR(10))
                                          WHEN MONTH(GETDATE()) IN ('10','11','12') THEN CAST((YEAR(GETDATE()) + 1) AS NVARCHAR(10))
                                        END AS FY";
          using (SqlCommand SqlCommand_FY = new SqlCommand(SQLStringFY))
          {
            DataTable DataTable_FY;
            using (DataTable_FY = new DataTable())
            {
              DataTable_FY.Locale = CultureInfo.CurrentCulture;
              DataTable_FY = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FY).Copy();
              if (DataTable_FY.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_FY.Rows)
                {
                  FY = DataRow_Row["FY"].ToString();
                }
              }
            }
          }

          ((Label)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("Label_InsertFY")).Text = FY;
          ((HiddenField)FormView_PharmacySurveys_LoadedSurveys_Form.FindControl("HiddenField_InsertFY")).Value = FY;
        }
      }
    }

    //--START-- --Insert Controls--//
    protected void Button_InsertClear_Click_LoadedSurveys(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSurveys_Id=0"), false);
    }
    //---END--- --Insert Controls--//

    //--START-- --Edit Controls--//
    protected void Button_EditClear_Click_LoadedSurveys(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSurveys_Id=0"), false);
    }

    protected void Button_EditUpdate_Click_LoadedSurveys(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//
    //---END--- --LoadedSurveys_Form--//


    //--START-- --LoadedSections_List--//
    protected void SqlDataSource_PharmacySurveys_LoadedSections_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Sections.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_PharmacySurveys_LoadedSections_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }


      for (int i = 0; i < GridView_PharmacySurveys_LoadedSections_List.Rows.Count; i++)
      {
        if (GridView_PharmacySurveys_LoadedSections_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_List.Rows[i].Cells[2].Text == "Yes")
          {
            GridView_List.Rows[i].Cells[2].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[2].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_List.Rows[i].Cells[2].Text == "No")
          {
            GridView_List.Rows[i].Cells[2].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[2].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void Button_AddSections_Click(object sender, EventArgs e)
    {
      FromDataBase_Navigation FromDataBase_Navigation_Current = GetNavigation();
      string LoadedSurveysId = FromDataBase_Navigation_Current.LoadedSurveysId;

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSurveys_Id=" + LoadedSurveysId + "&LoadedSections_Id=0"), false);
    }

    public static string GetLink_Sections(object loadedSections_Id)
    {
      return "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSections_Id=" + loadedSections_Id + "") + "'>Update</a>";
    }
    //---END--- --LoadedSections_List--//


    //--START-- --LoadedSections_Form--//
    //--START-- --Insert--//
    protected void FormView_PharmacySurveys_LoadedSections_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_LoadedSections();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);

          ((Label)FormView_PharmacySurveys_LoadedSections_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_PharmacySurveys_LoadedSections_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters["LoadedSurveys_Id"].DefaultValue = Request.QueryString["LoadedSurveys_Id"];
          SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters["LoadedSections_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters["LoadedSections_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters["LoadedSections_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters["LoadedSections_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters["LoadedSections_History"].DefaultValue = "";
          SqlDataSource_PharmacySurveys_LoadedSections_Form.InsertParameters["LoadedSections_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation_LoadedSections()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedSections_Form.FindControl("TextBox_InsertName")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string LoadedSurveysName = "";
        string LoadedSectionsName = "";
        string SQLStringLoadedSections = @"SELECT	LoadedSurveys_Name + ' (' + LoadedSurveys_FY + ')' AS LoadedSurveys_Name , 
                                                  LoadedSections_Name
                                          FROM		Form_PharmacySurveys_LoadedSections
                                                  LEFT JOIN Form_PharmacySurveys_LoadedSurveys ON Form_PharmacySurveys_LoadedSections.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id
                                          WHERE		LoadedSections_Name = @LoadedSections_Name
                                                  AND Form_PharmacySurveys_LoadedSections.LoadedSurveys_Id = @LoadedSurveys_Id
                                                  AND LoadedSections_IsActive = 1";
        using (SqlCommand SqlCommand_LoadedSections = new SqlCommand(SQLStringLoadedSections))
        {
          SqlCommand_LoadedSections.Parameters.AddWithValue("@LoadedSections_Name", ((TextBox)FormView_PharmacySurveys_LoadedSections_Form.FindControl("TextBox_InsertName")).Text);
          SqlCommand_LoadedSections.Parameters.AddWithValue("@LoadedSurveys_Id", Request.QueryString["LoadedSurveys_Id"]);
          DataTable DataTable_LoadedSections;
          using (DataTable_LoadedSections = new DataTable())
          {
            DataTable_LoadedSections.Locale = CultureInfo.CurrentCulture;
            DataTable_LoadedSections = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LoadedSections).Copy();
            if (DataTable_LoadedSections.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_LoadedSections.Rows)
              {
                LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
                LoadedSectionsName = DataRow_Row["LoadedSections_Name"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(LoadedSectionsName))
        {
          InvalidFormMessage = InvalidFormMessage + "A Section with the Name '" + LoadedSectionsName + "' already exists for Survey '" + LoadedSurveysName + "'<br />";
        }

        LoadedSurveysName = "";
        LoadedSectionsName = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacySurveys_LoadedSections_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string LoadedSections_Id = e.Command.Parameters["@LoadedSections_Id"].Value.ToString();
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSections_Id=" + LoadedSections_Id + ""), false);
      }
    }
    //---END--- --Insert--//

    //--START-- --Edit--//
    protected void FormView_PharmacySurveys_LoadedSections_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDLoadedSectionsModifiedDate"] = e.OldValues["LoadedSections_ModifiedDate"];
        object OLDLoadedSectionsModifiedDate = Session["OLDLoadedSectionsModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDLoadedSectionsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareLoadedSections = (DataView)SqlDataSource_PharmacySurveys_LoadedSections_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareLoadedSections = DataView_CompareLoadedSections[0];
        Session["DBLoadedSectionsModifiedDate"] = Convert.ToString(DataRowView_CompareLoadedSections["LoadedSections_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBLoadedSectionsModifiedBy"] = Convert.ToString(DataRowView_CompareLoadedSections["LoadedSections_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBLoadedSectionsModifiedDate = Session["DBLoadedSectionsModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBLoadedSectionsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBLoadedSectionsModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_PharmacySurveys_LoadedSections_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_PharmacySurveys_LoadedSections_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_LoadedSections();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);
            ((Label)FormView_PharmacySurveys_LoadedSections_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_PharmacySurveys_LoadedSections_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
            e.NewValues["LoadedSections_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["LoadedSections_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_PharmacySurveys_LoadedSections", "LoadedSections_Id = " + Request.QueryString["LoadedSections_Id"]);

            DataView DataView_LoadedSections = (DataView)SqlDataSource_PharmacySurveys_LoadedSections_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_LoadedSections = DataView_LoadedSections[0];
            Session["LoadedSectionsHistory"] = Convert.ToString(DataRowView_LoadedSections["LoadedSections_History"], CultureInfo.CurrentCulture);

            Session["LoadedSectionsHistory"] = Session["History"].ToString() + Session["LoadedSectionsHistory"].ToString();
            e.NewValues["LoadedSections_History"] = Session["LoadedSectionsHistory"].ToString();

            Session["LoadedSectionsHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDLoadedSectionsModifiedDate"] = "";
        Session["DBLoadedSectionsModifiedDate"] = "";
        Session["DBLoadedSectionsModifiedBy"] = "";
      }
    }

    protected string EditValidation_LoadedSections()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedSections_Form.FindControl("TextBox_EditName")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string LoadedSurveysName = "";
        string LoadedSectionsId = "";
        string LoadedSectionsName = "";
        string SQLStringLoadedSections = @"SELECT	LoadedSurveys_Name + ' (' + LoadedSurveys_FY + ')' AS LoadedSurveys_Name , 
                                                  LoadedSections_Id ,
                                                  LoadedSections_Name
                                          FROM		Form_PharmacySurveys_LoadedSections
                                                  LEFT JOIN Form_PharmacySurveys_LoadedSurveys ON Form_PharmacySurveys_LoadedSections.LoadedSurveys_Id = Form_PharmacySurveys_LoadedSurveys.LoadedSurveys_Id
                                          WHERE		LoadedSections_Name = @LoadedSections_Name
                                                  AND Form_PharmacySurveys_LoadedSections.LoadedSurveys_Id IN (
					                                          SELECT	LoadedSurveys_Id
					                                          FROM		Form_PharmacySurveys_LoadedSections
					                                          WHERE		LoadedSections_Id = @LoadedSections_Id
                                                  )
                                                  AND LoadedSections_IsActive = 1";
        using (SqlCommand SqlCommand_LoadedSections = new SqlCommand(SQLStringLoadedSections))
        {
          SqlCommand_LoadedSections.Parameters.AddWithValue("@LoadedSections_Name", ((TextBox)FormView_PharmacySurveys_LoadedSections_Form.FindControl("TextBox_EditName")).Text);
          SqlCommand_LoadedSections.Parameters.AddWithValue("@LoadedSections_Id", Request.QueryString["LoadedSections_Id"]);
          DataTable DataTable_LoadedSections;
          using (DataTable_LoadedSections = new DataTable())
          {
            DataTable_LoadedSections.Locale = CultureInfo.CurrentCulture;
            DataTable_LoadedSections = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LoadedSections).Copy();
            if (DataTable_LoadedSections.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_LoadedSections.Rows)
              {
                LoadedSurveysName = DataRow_Row["LoadedSurveys_Name"].ToString();
                LoadedSectionsId = DataRow_Row["LoadedSections_Id"].ToString();
                LoadedSectionsName = DataRow_Row["LoadedSections_Name"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(LoadedSectionsName))
        {
          if (LoadedSectionsId != Request.QueryString["LoadedSections_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Section with the Name '" + LoadedSectionsName + "' already exists for Survey '" + LoadedSurveysName + "'<br />";
          }
        }

        LoadedSurveysName = "";
        LoadedSectionsId = "";
        LoadedSectionsName = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacySurveys_LoadedSections_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            if (((CheckBox)FormView_PharmacySurveys_LoadedSections_Form.FindControl("CheckBox_EditIsActive")).Checked == false)
            {
              string SQLStringUpdateLoadedQuestions = @"UPDATE	Form_PharmacySurveys_LoadedQuestions
                                                        SET			LoadedQuestions_IsActive = 0
                                                        WHERE		LoadedSections_Id = @LoadedSections";
              using (SqlCommand SqlCommand_UpdateLoadedQuestions = new SqlCommand(SQLStringUpdateLoadedQuestions))
              {
                SqlCommand_UpdateLoadedQuestions.Parameters.AddWithValue("@LoadedSections_Id", Request.QueryString["LoadedSections_Id"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateLoadedQuestions);
              }


              string SQLStringUpdateLoadedAnswers = @"UPDATE	Form_PharmacySurveys_LoadedAnswers
                                                      SET			LoadedAnswers_IsActive = 0
                                                      WHERE		LoadedQuestions_Id IN (
					                                                      SELECT	LoadedQuestions_Id 
					                                                      FROM		Form_PharmacySurveys_LoadedQuestions
					                                                      WHERE		LoadedSections_Id = @LoadedSections
				                                                      )";
              using (SqlCommand SqlCommand_UpdateLoadedAnswers = new SqlCommand(SQLStringUpdateLoadedAnswers))
              {
                SqlCommand_UpdateLoadedAnswers.Parameters.AddWithValue("@LoadedSections_Id", Request.QueryString["LoadedSections_Id"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateLoadedAnswers);
              }
            }

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSections_Id=" + Request.QueryString["LoadedSections_Id"] + ""), false);
          }
        }
      }
    }
    //---END--- --Edit--//

    //--START-- --Insert Controls--//
    protected void Button_InsertClear_Click_LoadedSections(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSections_Id=0"), false);
    }
    //---END--- --Insert Controls--//

    //--START-- --Edit Controls--//
    protected void Button_EditClear_Click_LoadedSections(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSections_Id=0"), false);
    }

    protected void Button_EditUpdate_Click_LoadedSections(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//
    //---END--- --LoadedSections_Form--//


    //--START-- --LoadedQuestions_List--//
    protected void SqlDataSource_PharmacySurveys_LoadedQuestions_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Questions.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_PharmacySurveys_LoadedQuestions_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }


      for (int i = 0; i < GridView_PharmacySurveys_LoadedQuestions_List.Rows.Count; i++)
      {
        if (GridView_PharmacySurveys_LoadedQuestions_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_List.Rows[i].Cells[2].Text == "Yes")
          {
            GridView_List.Rows[i].Cells[2].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[2].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_List.Rows[i].Cells[2].Text == "No")
          {
            GridView_List.Rows[i].Cells[2].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[2].ForeColor = Color.FromName("#333333");
          }

          if (GridView_List.Rows[i].Cells[4].Text == "Yes")
          {
            GridView_List.Rows[i].Cells[4].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[4].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_List.Rows[i].Cells[4].Text == "No")
          {
            GridView_List.Rows[i].Cells[4].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[4].ForeColor = Color.FromName("#333333");
          }

          if (GridView_List.Rows[i].Cells[6].Text == "Yes")
          {
            GridView_List.Rows[i].Cells[6].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_List.Rows[i].Cells[6].Text == "No")
          {
            GridView_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void Button_AddQuestions_Click(object sender, EventArgs e)
    {
      FromDataBase_Navigation FromDataBase_Navigation_Current = GetNavigation();
      string LoadedSectionsId = FromDataBase_Navigation_Current.LoadedSectionsId;

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSections_Id=" + LoadedSectionsId + "&LoadedQuestions_Id=0"), false);
    }

    public static string GetLink_Questions(object loadedQuestions_Id)
    {
      return "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedQuestions_Id=" + loadedQuestions_Id + "") + "'>Update</a>";
    }
    //---END--- --LoadedQuestions_List--//


    //--START-- --LoadedQuestions_Form--//
    //--START-- --Insert--//
    protected void FormView_PharmacySurveys_LoadedQuestions_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_LoadedQuestions();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);

          ((Label)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedSections_Id"].DefaultValue = Request.QueryString["LoadedSections_Id"];
          SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_Dependency_ShowHide_LoadedAnswersId"].DefaultValue = ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId")).SelectedValue;
          SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_History"].DefaultValue = "";
          SqlDataSource_PharmacySurveys_LoadedQuestions_Form.InsertParameters["LoadedQuestions_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation_LoadedQuestions()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("TextBox_InsertName")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string LoadedSectionsName = "";
        string LoadedQuestionsName = "";
        string SQLStringLoadedQuestions = @"SELECT	LoadedSections_Name , 
                                                    LoadedQuestions_Name
                                            FROM		Form_PharmacySurveys_LoadedQuestions
                                                    LEFT JOIN Form_PharmacySurveys_LoadedSections ON Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id = Form_PharmacySurveys_LoadedSections.LoadedSections_Id
                                            WHERE		LoadedQuestions_Name = @LoadedQuestions_Name
                                                    AND Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id = @LoadedSections_Id
                                                    AND LoadedQuestions_IsActive = 1";
        using (SqlCommand SqlCommand_LoadedQuestions = new SqlCommand(SQLStringLoadedQuestions))
        {
          SqlCommand_LoadedQuestions.Parameters.AddWithValue("@LoadedQuestions_Name", ((TextBox)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("TextBox_InsertName")).Text);
          SqlCommand_LoadedQuestions.Parameters.AddWithValue("@LoadedSections_Id", Request.QueryString["LoadedSections_Id"]);
          DataTable DataTable_LoadedQuestions;
          using (DataTable_LoadedQuestions = new DataTable())
          {
            DataTable_LoadedQuestions.Locale = CultureInfo.CurrentCulture;
            DataTable_LoadedQuestions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LoadedQuestions).Copy();
            if (DataTable_LoadedQuestions.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_LoadedQuestions.Rows)
              {
                LoadedSectionsName = DataRow_Row["LoadedSections_Name"].ToString();
                LoadedQuestionsName = DataRow_Row["LoadedQuestions_Name"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(LoadedQuestionsName))
        {
          InvalidFormMessage = InvalidFormMessage + "A Question with the Name '" + LoadedQuestionsName + "' already exists for Section '" + LoadedSectionsName + "'<br />";
        }

        LoadedSectionsName = "";
        LoadedQuestionsName = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacySurveys_LoadedQuestions_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string LoadedQuestions_Id = e.Command.Parameters["@LoadedQuestions_Id"].Value.ToString();
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedQuestions_Id=" + LoadedQuestions_Id + ""), false);
      }
    }
    //---END--- --Insert--//

    //--START-- --Edit--//
    protected void FormView_PharmacySurveys_LoadedQuestions_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDLoadedQuestionsModifiedDate"] = e.OldValues["LoadedQuestions_ModifiedDate"];
        object OLDLoadedQuestionsModifiedDate = Session["OLDLoadedQuestionsModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDLoadedQuestionsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareLoadedQuestions = (DataView)SqlDataSource_PharmacySurveys_LoadedQuestions_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareLoadedQuestions = DataView_CompareLoadedQuestions[0];
        Session["DBLoadedQuestionsModifiedDate"] = Convert.ToString(DataRowView_CompareLoadedQuestions["LoadedQuestions_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBLoadedQuestionsModifiedBy"] = Convert.ToString(DataRowView_CompareLoadedQuestions["LoadedQuestions_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBLoadedQuestionsModifiedDate = Session["DBLoadedQuestionsModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBLoadedQuestionsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBLoadedQuestionsModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_LoadedQuestions();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);
            ((Label)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
            e.NewValues["LoadedQuestions_Dependency_ShowHide_LoadedAnswersId"] = ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId")).SelectedValue;
            e.NewValues["LoadedQuestions_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["LoadedQuestions_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];            

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_PharmacySurveys_LoadedQuestions", "LoadedQuestions_Id = " + Request.QueryString["LoadedQuestions_Id"]);

            DataView DataView_LoadedQuestions = (DataView)SqlDataSource_PharmacySurveys_LoadedQuestions_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_LoadedQuestions = DataView_LoadedQuestions[0];
            Session["LoadedQuestionsHistory"] = Convert.ToString(DataRowView_LoadedQuestions["LoadedQuestions_History"], CultureInfo.CurrentCulture);

            Session["LoadedQuestionsHistory"] = Session["History"].ToString() + Session["LoadedQuestionsHistory"].ToString();
            e.NewValues["LoadedQuestions_History"] = Session["LoadedQuestionsHistory"].ToString();

            Session["LoadedQuestionsHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDLoadedQuestionsModifiedDate"] = "";
        Session["DBLoadedQuestionsModifiedDate"] = "";
        Session["DBLoadedQuestionsModifiedBy"] = "";
      }
    }

    protected string EditValidation_LoadedQuestions()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("TextBox_EditName")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string LoadedSectionsName = "";
        string LoadedQuestionsId = "";
        string LoadedQuestionsName = "";
        string SQLStringLoadedQuestions = @"SELECT	LoadedSections_Name , 
                                                  LoadedQuestions_Id ,
                                                  LoadedQuestions_Name
                                          FROM		Form_PharmacySurveys_LoadedQuestions
                                                  LEFT JOIN Form_PharmacySurveys_LoadedSections ON Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id = Form_PharmacySurveys_LoadedSections.LoadedSections_Id
                                          WHERE		LoadedQuestions_Name = @LoadedQuestions_Name
                                                  AND Form_PharmacySurveys_LoadedQuestions.LoadedSections_Id IN (
					                                          SELECT	LoadedSections_Id
					                                          FROM		Form_PharmacySurveys_LoadedQuestions
					                                          WHERE		LoadedQuestions_Id = @LoadedQuestions_Id
                                                  )
                                                  AND LoadedQuestions_IsActive = 1";
        using (SqlCommand SqlCommand_LoadedQuestions = new SqlCommand(SQLStringLoadedQuestions))
        {
          SqlCommand_LoadedQuestions.Parameters.AddWithValue("@LoadedQuestions_Name", ((TextBox)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("TextBox_EditName")).Text);
          SqlCommand_LoadedQuestions.Parameters.AddWithValue("@LoadedQuestions_Id", Request.QueryString["LoadedQuestions_Id"]);
          DataTable DataTable_LoadedQuestions;
          using (DataTable_LoadedQuestions = new DataTable())
          {
            DataTable_LoadedQuestions.Locale = CultureInfo.CurrentCulture;
            DataTable_LoadedQuestions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LoadedQuestions).Copy();
            if (DataTable_LoadedQuestions.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_LoadedQuestions.Rows)
              {
                LoadedSectionsName = DataRow_Row["LoadedSections_Name"].ToString();
                LoadedQuestionsId = DataRow_Row["LoadedQuestions_Id"].ToString();
                LoadedQuestionsName = DataRow_Row["LoadedQuestions_Name"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(LoadedQuestionsName))
        {
          if (LoadedQuestionsId != Request.QueryString["LoadedQuestions_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Question with the Name '" + LoadedQuestionsName + "' already exists for Section '" + LoadedSectionsName + "'<br />";
          }
        }

        LoadedSectionsName = "";
        LoadedQuestionsId = "";
        LoadedQuestionsName = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacySurveys_LoadedQuestions_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            if (((CheckBox)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("CheckBox_EditIsActive")).Checked == false)
            {
              string SQLStringUpdateLoadedAnswers = @"UPDATE	Form_PharmacySurveys_LoadedAnswers
                                                      SET			LoadedAnswers_IsActive = 0
                                                      WHERE		LoadedQuestions_Id = @LoadedQuestions";
              using (SqlCommand SqlCommand_UpdateLoadedAnswers = new SqlCommand(SQLStringUpdateLoadedAnswers))
              {
                SqlCommand_UpdateLoadedAnswers.Parameters.AddWithValue("@LoadedQuestions_Id", Request.QueryString["LoadedQuestions_Id"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateLoadedAnswers);
              }
            }

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedQuestions_Id=" + Request.QueryString["LoadedQuestions_Id"] + ""), false);
          }
        }
      }
    }
    //---END--- --Edit--//

    protected void FormView_PharmacySurveys_LoadedQuestions_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_PharmacySurveys_LoadedQuestions_Form.CurrentMode == FormViewMode.Edit)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["LoadedQuestions_Id"]))
        {
          DataView DataView_LoadedQuestionsDependency = (DataView)SqlDataSource_PharmacySurveys_LoadedQuestions_Form.Select(DataSourceSelectArguments.Empty);
          DataRowView DataRowView_LoadedQuestionsDependency = DataView_LoadedQuestionsDependency[0];
          ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId_Section")).SelectedValue = Convert.ToString(DataRowView_LoadedQuestionsDependency["DependencySection"], CultureInfo.CurrentCulture);
          ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId_Question")).SelectedValue = Convert.ToString(DataRowView_LoadedQuestionsDependency["DependencyQuestion"], CultureInfo.CurrentCulture);
          ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId")).SelectedValue = Convert.ToString(DataRowView_LoadedQuestionsDependency["LoadedQuestions_Dependency_ShowHide_LoadedAnswersId"], CultureInfo.CurrentCulture);
        }
      }
    }

    //--START-- --Insert Controls--//
    protected void DropDownList_InsertDependencyShowHideLoadedAnswersId_Section_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId_Question")).Items.Clear();
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId")).Items.Clear();

      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId_Question.SelectParameters["LoadedSections_Id"].DefaultValue = ((DropDownList)sender).SelectedValue;
      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId.SelectParameters["LoadedQuestions_Id"].DefaultValue = "";

      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId_Question")).Items.Insert(0, new ListItem(Convert.ToString("Select Question", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId")).Items.Insert(0, new ListItem(Convert.ToString("Select Answer", CultureInfo.CurrentCulture), ""));

      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId_Question")).DataBind();
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId")).DataBind();
    }

    protected void DropDownList_InsertDependencyShowHideLoadedAnswersId_Question_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId")).Items.Clear();

      SqlDataSource_PharmacySurveys_LoadedQuestions_InsertDependencyShowHideLoadedAnswersId.SelectParameters["LoadedQuestions_Id"].DefaultValue = ((DropDownList)sender).SelectedValue;

      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId")).Items.Insert(0, new ListItem(Convert.ToString("Select Answer", CultureInfo.CurrentCulture), ""));

      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_InsertDependencyShowHideLoadedAnswersId")).DataBind();
    }

    protected void Button_InsertClear_Click_LoadedQuestions(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedQuestions_Id=0"), false);
    }
    //---END--- --Insert Controls--//

    //--START-- --Edit Controls--//
    protected void DropDownList_EditDependencyShowHideLoadedAnswersId_Section_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId_Question")).Items.Clear();
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId")).Items.Clear();

      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Question.SelectParameters["LoadedSections_Id"].DefaultValue = ((DropDownList)sender).SelectedValue;
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId.SelectParameters["LoadedQuestions_Id"].DefaultValue = "";

      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId_Question")).Items.Insert(0, new ListItem(Convert.ToString("Select Question", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId")).Items.Insert(0, new ListItem(Convert.ToString("Select Answer", CultureInfo.CurrentCulture), ""));

      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId_Question")).DataBind();
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId")).DataBind();
    }

    protected void DropDownList_EditDependencyShowHideLoadedAnswersId_Section_DataBound(object sender, EventArgs e)
    {
      DataView DataView_LoadedQuestionsDependency = (DataView)SqlDataSource_PharmacySurveys_LoadedQuestions_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_LoadedQuestionsDependency = DataView_LoadedQuestionsDependency[0];
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId_Question.SelectParameters["LoadedSections_Id"].DefaultValue = Convert.ToString(DataRowView_LoadedQuestionsDependency["DependencySection"], CultureInfo.CurrentCulture);
    }

    protected void DropDownList_EditDependencyShowHideLoadedAnswersId_Question_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId")).Items.Clear();

      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId.SelectParameters["LoadedQuestions_Id"].DefaultValue = ((DropDownList)sender).SelectedValue;

      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId")).Items.Insert(0, new ListItem(Convert.ToString("Select Answer", CultureInfo.CurrentCulture), ""));

      ((DropDownList)FormView_PharmacySurveys_LoadedQuestions_Form.FindControl("DropDownList_EditDependencyShowHideLoadedAnswersId")).DataBind();
    }

    protected void DropDownList_EditDependencyShowHideLoadedAnswersId_Question_DataBound(object sender, EventArgs e)
    {
      DataView DataView_LoadedQuestionsDependency = (DataView)SqlDataSource_PharmacySurveys_LoadedQuestions_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_LoadedQuestionsDependency = DataView_LoadedQuestionsDependency[0];
      SqlDataSource_PharmacySurveys_LoadedQuestions_EditDependencyShowHideLoadedAnswersId.SelectParameters["LoadedQuestions_Id"].DefaultValue = Convert.ToString(DataRowView_LoadedQuestionsDependency["DependencyQuestion"], CultureInfo.CurrentCulture);
    }

    protected void Button_EditClear_Click_LoadedQuestions(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedQuestions_Id=0"), false);
    }

    protected void Button_EditUpdate_Click_LoadedQuestions(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//
    //---END--- --LoadedQuestions_Form--//


    //--START-- --LoadedAnswers_List--//
    protected void SqlDataSource_PharmacySurveys_LoadedAnswers_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Answers.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_PharmacySurveys_LoadedAnswers_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }


      for (int i = 0; i < GridView_PharmacySurveys_LoadedAnswers_List.Rows.Count; i++)
      {
        if (GridView_PharmacySurveys_LoadedAnswers_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_List.Rows[i].Cells[3].Text == "Yes")
          {
            GridView_List.Rows[i].Cells[3].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_List.Rows[i].Cells[3].Text == "No")
          {
            GridView_List.Rows[i].Cells[3].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void Button_AddAnswers_Click(object sender, EventArgs e)
    {
      FromDataBase_Navigation FromDataBase_Navigation_Current = GetNavigation();
      string LoadedQuestionsId = FromDataBase_Navigation_Current.LoadedQuestionsId;

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedQuestions_Id=" + LoadedQuestionsId + "&LoadedAnswers_Id=0"), false);
    }

    public static string GetLink_Answers(object loadedAnswers_Id)
    {
      return "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedAnswers_Id=" + loadedAnswers_Id + "") + "'>Update</a>";
    }
    //---END--- --LoadedAnswers_List--//


    //--START-- --LoadedAnswers_Form--//
    //--START-- --Insert--//
    protected void FormView_PharmacySurveys_LoadedAnswers_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_LoadedAnswers();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);

          ((Label)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters["LoadedQuestions_Id"].DefaultValue = Request.QueryString["LoadedQuestions_Id"];
          SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters["LoadedAnswers_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters["LoadedAnswers_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters["LoadedAnswers_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters["LoadedAnswers_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters["LoadedAnswers_History"].DefaultValue = "";
          SqlDataSource_PharmacySurveys_LoadedAnswers_Form.InsertParameters["LoadedAnswers_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation_LoadedAnswers()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_InsertName")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_InsertScore")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string LoadedQuestionsName = "";
        string LoadedAnswersName = "";
        string SQLStringLoadedAnswers = @"SELECT	LoadedQuestions_Name , 
                                                  LoadedAnswers_Name
                                          FROM		Form_PharmacySurveys_LoadedAnswers
                                                  LEFT JOIN Form_PharmacySurveys_LoadedQuestions ON Form_PharmacySurveys_LoadedAnswers.LoadedQuestions_Id = Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id
                                          WHERE		LoadedAnswers_Name = @LoadedAnswers_Name
                                                  AND Form_PharmacySurveys_LoadedAnswers.LoadedQuestions_Id = @LoadedQuestions_Id
                                                  AND LoadedAnswers_IsActive = 1";
        using (SqlCommand SqlCommand_LoadedAnswers = new SqlCommand(SQLStringLoadedAnswers))
        {
          SqlCommand_LoadedAnswers.Parameters.AddWithValue("@LoadedAnswers_Name", ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_InsertName")).Text);
          SqlCommand_LoadedAnswers.Parameters.AddWithValue("@LoadedQuestions_Id", Request.QueryString["LoadedQuestions_Id"]);
          DataTable DataTable_LoadedAnswers;
          using (DataTable_LoadedAnswers = new DataTable())
          {
            DataTable_LoadedAnswers.Locale = CultureInfo.CurrentCulture;
            DataTable_LoadedAnswers = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LoadedAnswers).Copy();
            if (DataTable_LoadedAnswers.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_LoadedAnswers.Rows)
              {
                LoadedQuestionsName = DataRow_Row["LoadedQuestions_Name"].ToString();
                LoadedAnswersName = DataRow_Row["LoadedAnswers_Name"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(LoadedAnswersName))
        {
          InvalidFormMessage = InvalidFormMessage + "A Answer with the Name '" + LoadedAnswersName + "' already exists for Question '" + LoadedQuestionsName + "'<br />";
        }

        LoadedQuestionsName = "";
        LoadedAnswersName = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacySurveys_LoadedAnswers_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string LoadedAnswers_Id = e.Command.Parameters["@LoadedAnswers_Id"].Value.ToString();
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedAnswers_Id=" + LoadedAnswers_Id + ""), false);
      }
    }
    //---END--- --Insert--//

    //--START-- --Edit--//
    protected void FormView_PharmacySurveys_LoadedAnswers_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDLoadedAnswersModifiedDate"] = e.OldValues["LoadedAnswers_ModifiedDate"];
        object OLDLoadedAnswersModifiedDate = Session["OLDLoadedAnswersModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDLoadedAnswersModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareLoadedAnswers = (DataView)SqlDataSource_PharmacySurveys_LoadedAnswers_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareLoadedAnswers = DataView_CompareLoadedAnswers[0];
        Session["DBLoadedAnswersModifiedDate"] = Convert.ToString(DataRowView_CompareLoadedAnswers["LoadedAnswers_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBLoadedAnswersModifiedBy"] = Convert.ToString(DataRowView_CompareLoadedAnswers["LoadedAnswers_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBLoadedAnswersModifiedDate = Session["DBLoadedAnswersModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBLoadedAnswersModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBLoadedAnswersModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_LoadedAnswers();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_PharmacySurveys_LoadedSurveys.SetFocus(UpdatePanel_PharmacySurveys_LoadedSurveys);
            ((Label)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
            e.NewValues["LoadedAnswers_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["LoadedAnswers_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_PharmacySurveys_LoadedAnswers", "LoadedAnswers_Id = " + Request.QueryString["LoadedAnswers_Id"]);

            DataView DataView_LoadedAnswers = (DataView)SqlDataSource_PharmacySurveys_LoadedAnswers_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_LoadedAnswers = DataView_LoadedAnswers[0];
            Session["LoadedAnswersHistory"] = Convert.ToString(DataRowView_LoadedAnswers["LoadedAnswers_History"], CultureInfo.CurrentCulture);

            Session["LoadedAnswersHistory"] = Session["History"].ToString() + Session["LoadedAnswersHistory"].ToString();
            e.NewValues["LoadedAnswers_History"] = Session["LoadedAnswersHistory"].ToString();

            Session["LoadedAnswersHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDLoadedAnswersModifiedDate"] = "";
        Session["DBLoadedAnswersModifiedDate"] = "";
        Session["DBLoadedAnswersModifiedBy"] = "";
      }
    }

    protected string EditValidation_LoadedAnswers()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_EditName")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_EditScore")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string LoadedQuestionsName = "";
        string LoadedAnswersId = "";
        string LoadedAnswersName = "";
        string SQLStringLoadedAnswers = @"SELECT	LoadedQuestions_Name , 
                                                  LoadedAnswers_Id ,
                                                  LoadedAnswers_Name
                                          FROM		Form_PharmacySurveys_LoadedAnswers
                                                  LEFT JOIN Form_PharmacySurveys_LoadedQuestions ON Form_PharmacySurveys_LoadedAnswers.LoadedQuestions_Id = Form_PharmacySurveys_LoadedQuestions.LoadedQuestions_Id
                                          WHERE		LoadedAnswers_Name = @LoadedAnswers_Name
                                                  AND Form_PharmacySurveys_LoadedAnswers.LoadedQuestions_Id IN (
					                                          SELECT	LoadedQuestions_Id
					                                          FROM		Form_PharmacySurveys_LoadedAnswers
					                                          WHERE		LoadedAnswers_Id = @LoadedAnswers_Id
                                                  )
                                                  AND LoadedAnswers_IsActive = 1";
        using (SqlCommand SqlCommand_LoadedAnswers = new SqlCommand(SQLStringLoadedAnswers))
        {
          SqlCommand_LoadedAnswers.Parameters.AddWithValue("@LoadedAnswers_Name", ((TextBox)FormView_PharmacySurveys_LoadedAnswers_Form.FindControl("TextBox_EditName")).Text);
          SqlCommand_LoadedAnswers.Parameters.AddWithValue("@LoadedAnswers_Id", Request.QueryString["LoadedAnswers_Id"]);
          DataTable DataTable_LoadedAnswers;
          using (DataTable_LoadedAnswers = new DataTable())
          {
            DataTable_LoadedAnswers.Locale = CultureInfo.CurrentCulture;
            DataTable_LoadedAnswers = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LoadedAnswers).Copy();
            if (DataTable_LoadedAnswers.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_LoadedAnswers.Rows)
              {
                LoadedQuestionsName = DataRow_Row["LoadedQuestions_Name"].ToString();
                LoadedAnswersId = DataRow_Row["LoadedAnswers_Id"].ToString();
                LoadedAnswersName = DataRow_Row["LoadedAnswers_Name"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(LoadedAnswersName))
        {
          if (LoadedAnswersId != Request.QueryString["LoadedAnswers_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Answer with the Name '" + LoadedAnswersName + "' already exists for Question '" + LoadedQuestionsName + "'<br />";
          }
        }

        LoadedQuestionsName = "";
        LoadedAnswersId = "";
        LoadedAnswersName = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_PharmacySurveys_LoadedAnswers_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedAnswers_Id=" + Request.QueryString["LoadedAnswers_Id"] + ""), false);
          }
        }
      } 
    }
    //---END--- --Edit--//

    //--START-- --Insert Controls--//
    protected void Button_InsertClear_Click_LoadedAnswers(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedAnswers_Id=0"), false);
    }
    //---END--- --Insert Controls--//

    //--START-- --Edit Controls--//
    protected void Button_EditClear_Click_LoadedAnswers(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedAnswers_Id=0"), false);
    }

    protected void Button_EditUpdate_Click_LoadedAnswers(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//
    //---END--- --LoadedAnswers_Form--//
  }
}