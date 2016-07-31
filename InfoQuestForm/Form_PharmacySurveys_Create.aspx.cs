using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_PharmacySurveys_Create : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_PharmacySurveys_Create, this.GetType(), "UpdateProgress_Start", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          PageTitle();

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Form();");
          DropDownList_LoadedSurveysName.Attributes.Add("OnChange", "Validation_Form();");
          DropDownList_LoadedSurveysName.Attributes.Add("OnInput", "Validation_Form();");

          GetCheckBoxListCreatedSurveysName();
          GetCheckBoxListCreatedSurveysNameCancelled();
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
                                      AND (SecurityRole_Id IN ('186','188'))";
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
        ((Label)PageUpdateProgress_PharmacySurveys_Create.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Pharmacy Surveys", "23");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_PharmacySurveys_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_PharmacySurveys_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "47");
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacySurveys_LoadedSurveysName.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedSurveysName.SelectCommand = @"SELECT		LoadedSurveys_Id ,
					                                                                        LoadedSurveys_Name + ' (' + LoadedSurveys_FY + ')' AS LoadedSurveys_Name
                                                                        FROM			Form_PharmacySurveys_LoadedSurveys
                                                                        WHERE			LoadedSurveys_FY = 
					                                                                        CASE 
						                                                                        WHEN MONTH(GETDATE()) IN ('1','2','3','4','5','6','7','8','9') THEN CAST((YEAR(GETDATE()) + 0) AS NVARCHAR(10))
						                                                                        WHEN MONTH(GETDATE()) IN ('10','11','12') THEN CAST((YEAR(GETDATE()) + 1) AS NVARCHAR(10))
					                                                                        END
                                                                        ORDER BY	LoadedSurveys_Name";
    }

    protected void PageTitle()
    {
      Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("47")).ToString();
      Label_FormHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("47").Replace(" Form", "")).ToString() + ": Create / Modify Survey", CultureInfo.CurrentCulture);
    }

    private void GetCheckBoxListCreatedSurveysName()
    {
      if (!string.IsNullOrEmpty(DropDownList_Facility.SelectedValue) && !string.IsNullOrEmpty(DropDownList_LoadedSurveysName.SelectedValue))
      {
        CheckBoxList_CreatedSurveysName.Items.Clear();

        string CreatedSurveysUserName = "";
        string CreatedSurveysEmail = "";
        string CreatedSurveysName = "";
        string CreatedSurveysComplete = "";
        Boolean CreatedSurveysEnabled = false;
        string SQLStringCreatedSurveys = @"SELECT		CreatedSurveys_UserName , 
					                                          CreatedSurveys_Email , 
					                                          CreatedSurveys_Name ,
					                                          CASE 
						                                          WHEN CreatedSurveyAnswers_Id IS NOT NULL THEN 'Yes'
						                                          ELSE 'No'
					                                          END AS CreatedSurveys_Complete ,
					                                          CASE 
						                                          WHEN CreatedSurveyAnswers_Id IS NOT NULL THEN 'False'
						                                          ELSE 'True'
					                                          END AS CreatedSurveys_Enabled
                                          FROM			Form_PharmacySurveys_CreatedSurveys 
					                                          OUTER APPLY 
					                                          ( 
						                                          SELECT	TOP 1 * 
						                                          FROM		Form_PharmacySurveys_CreatedSurveyAnswers 
						                                          WHERE		Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id = Form_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id
					                                          ) TempTable
                                          WHERE			Facility_Id = @Facility_Id 
					                                          AND LoadedSurveys_Id = @LoadedSurveys_Id
                                                    AND CreatedSurveys_IsActive = 1
                                          ORDER BY	CreatedSurveys_Name";
        using (SqlCommand SqlCommand_CreatedSurveys = new SqlCommand(SQLStringCreatedSurveys))
        {
          SqlCommand_CreatedSurveys.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
          SqlCommand_CreatedSurveys.Parameters.AddWithValue("@LoadedSurveys_Id", DropDownList_LoadedSurveysName.SelectedValue);
          DataTable DataTable_CreatedSurveys;
          using (DataTable_CreatedSurveys = new DataTable())
          {
            DataTable_CreatedSurveys.Locale = CultureInfo.CurrentCulture;
            DataTable_CreatedSurveys = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CreatedSurveys).Copy();
            if (DataTable_CreatedSurveys.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_CreatedSurveys.Rows)
              {
                CreatedSurveysUserName = DataRow_Row["CreatedSurveys_UserName"].ToString();
                CreatedSurveysEmail = DataRow_Row["CreatedSurveys_Email"].ToString();
                CreatedSurveysName = DataRow_Row["CreatedSurveys_Name"].ToString();
                CreatedSurveysComplete = DataRow_Row["CreatedSurveys_Complete"].ToString();
                CreatedSurveysEnabled = Convert.ToBoolean(DataRow_Row["CreatedSurveys_Enabled"], CultureInfo.CurrentCulture);

                ListItem ListItem_Employee = new ListItem(Convert.ToString(CreatedSurveysName + " (" + CreatedSurveysUserName + ") (" + CreatedSurveysEmail + ") (" + CreatedSurveysComplete + ")", CultureInfo.CurrentCulture), Convert.ToString(CreatedSurveysName + " (" + CreatedSurveysUserName + ") (" + CreatedSurveysEmail + ")", CultureInfo.CurrentCulture), CreatedSurveysEnabled);
                ListItem_Employee.Attributes.Add("CreatedSurveysUserName", CreatedSurveysUserName);
                ListItem_Employee.Attributes.Add("CreatedSurveysEmail", CreatedSurveysEmail);
                ListItem_Employee.Attributes.Add("CreatedSurveysName", CreatedSurveysName);
                ListItem_Employee.Attributes.Add("CreatedSurveysComplete", CreatedSurveysComplete);
                CheckBoxList_CreatedSurveysName.Items.Add(ListItem_Employee);
              }
            }
          }
        }
      }

      SetButtonVisibility();
      SetCheckBoxListCreatedSurveysNameCancelledVisibility();
      SetCheckBoxListSearchNameVisibility();
    }

    private void GetCheckBoxListCreatedSurveysNameCancelled()
    {
      if (!string.IsNullOrEmpty(DropDownList_Facility.SelectedValue) && !string.IsNullOrEmpty(DropDownList_LoadedSurveysName.SelectedValue))
      {
        CheckBoxList_CreatedSurveysNameCanceled.Items.Clear();

        string CreatedSurveysUserName = "";
        string CreatedSurveysEmail = "";
        string CreatedSurveysName = "";
        string CreatedSurveysComplete = "";
        Boolean CreatedSurveysEnabled = false;
        string SQLStringCreatedSurveys = @"SELECT		CreatedSurveys_UserName , 
					                                          CreatedSurveys_Email , 
					                                          CreatedSurveys_Name ,
					                                          CASE 
						                                          WHEN CreatedSurveyAnswers_Id IS NOT NULL THEN 'Yes'
						                                          ELSE 'No'
					                                          END AS CreatedSurveys_Complete ,
					                                          CASE 
						                                          WHEN CreatedSurveyAnswers_Id IS NOT NULL THEN 'False'
						                                          ELSE 'True'
					                                          END AS CreatedSurveys_Enabled
                                          FROM			Form_PharmacySurveys_CreatedSurveys 
					                                          OUTER APPLY 
					                                          ( 
						                                          SELECT	TOP 1 * 
						                                          FROM		Form_PharmacySurveys_CreatedSurveyAnswers 
						                                          WHERE		Form_PharmacySurveys_CreatedSurveyAnswers.CreatedSurveys_Id = Form_PharmacySurveys_CreatedSurveys.CreatedSurveys_Id
					                                          ) TempTable
                                          WHERE			Facility_Id = @Facility_Id 
					                                          AND LoadedSurveys_Id = @LoadedSurveys_Id
                                                    AND CreatedSurveys_IsActive = 0
                                          ORDER BY	CreatedSurveys_Name";
        using (SqlCommand SqlCommand_CreatedSurveys = new SqlCommand(SQLStringCreatedSurveys))
        {
          SqlCommand_CreatedSurveys.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
          SqlCommand_CreatedSurveys.Parameters.AddWithValue("@LoadedSurveys_Id", DropDownList_LoadedSurveysName.SelectedValue);
          DataTable DataTable_CreatedSurveys;
          using (DataTable_CreatedSurveys = new DataTable())
          {
            DataTable_CreatedSurveys.Locale = CultureInfo.CurrentCulture;
            DataTable_CreatedSurveys = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CreatedSurveys).Copy();
            if (DataTable_CreatedSurveys.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_CreatedSurveys.Rows)
              {
                CreatedSurveysUserName = DataRow_Row["CreatedSurveys_UserName"].ToString();
                CreatedSurveysEmail = DataRow_Row["CreatedSurveys_Email"].ToString();
                CreatedSurveysName = DataRow_Row["CreatedSurveys_Name"].ToString();
                CreatedSurveysComplete = DataRow_Row["CreatedSurveys_Complete"].ToString();
                CreatedSurveysEnabled = Convert.ToBoolean(DataRow_Row["CreatedSurveys_Enabled"], CultureInfo.CurrentCulture);

                ListItem ListItem_Employee = new ListItem(Convert.ToString(CreatedSurveysName + " (" + CreatedSurveysUserName + ") (" + CreatedSurveysEmail + ")", CultureInfo.CurrentCulture), Convert.ToString(CreatedSurveysName + " (" + CreatedSurveysUserName + ") (" + CreatedSurveysEmail + ")", CultureInfo.CurrentCulture), CreatedSurveysEnabled);
                ListItem_Employee.Attributes.Add("CreatedSurveysUserName", CreatedSurveysUserName);
                ListItem_Employee.Attributes.Add("CreatedSurveysEmail", CreatedSurveysEmail);
                ListItem_Employee.Attributes.Add("CreatedSurveysName", CreatedSurveysName);
                ListItem_Employee.Attributes.Add("CreatedSurveysComplete", CreatedSurveysComplete);
                CheckBoxList_CreatedSurveysNameCanceled.Items.Add(ListItem_Employee);
              }
            }
          }
        }
      }

      SetButtonVisibility();
      SetCheckBoxListCreatedSurveysNameCancelledVisibility();
      SetCheckBoxListSearchNameVisibility();
    }

    private void GetPreviousSurvey()
    {
      if (string.IsNullOrEmpty(DropDownList_LoadedSurveysName.SelectedValue) || string.IsNullOrEmpty(DropDownList_LoadedSurveysName.SelectedValue))
      {
        Label_PreviousSurvey.Text = "";
        HiddenField_PreviousSurvey.Value = "No";
      }
      else
      {
        string CreatedSurveysId = "";
        string SQLStringCreatedSurveys = "SELECT TOP 1 CreatedSurveys_Id FROM Form_PharmacySurveys_CreatedSurveys WHERE Facility_Id = @Facility_Id AND LoadedSurveys_Id = @LoadedSurveys_Id AND CreatedSurveys_IsActive = 1";
        using (SqlCommand SqlCommand_CreatedSurveys = new SqlCommand(SQLStringCreatedSurveys))
        {
          SqlCommand_CreatedSurveys.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
          SqlCommand_CreatedSurveys.Parameters.AddWithValue("@LoadedSurveys_Id", DropDownList_LoadedSurveysName.SelectedValue);
          DataTable DataTable_CreatedSurveys;
          using (DataTable_CreatedSurveys = new DataTable())
          {
            DataTable_CreatedSurveys.Locale = CultureInfo.CurrentCulture;
            DataTable_CreatedSurveys = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CreatedSurveys).Copy();
            if (DataTable_CreatedSurveys.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_CreatedSurveys.Rows)
              {
                CreatedSurveysId = DataRow_Row["CreatedSurveys_Id"].ToString();
              }
            }
          }
        }


        string SurveyActive = "";
        string LoadedSurveysIsActive = "";
        string SQLStringSurveysIsActive = "SELECT CASE LoadedSurveys_IsActive WHEN 1 THEN 'Yes' ELSE 'No' END AS LoadedSurveys_IsActive FROM Form_PharmacySurveys_LoadedSurveys WHERE LoadedSurveys_Id = @LoadedSurveys_Id";
        using (SqlCommand SqlCommand_SurveysIsActive = new SqlCommand(SQLStringSurveysIsActive))
        {
          SqlCommand_SurveysIsActive.Parameters.AddWithValue("@LoadedSurveys_Id", DropDownList_LoadedSurveysName.SelectedValue);
          DataTable DataTable_SurveysIsActive;
          using (DataTable_SurveysIsActive = new DataTable())
          {
            DataTable_SurveysIsActive.Locale = CultureInfo.CurrentCulture;
            DataTable_SurveysIsActive = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SurveysIsActive).Copy();
            if (DataTable_SurveysIsActive.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SurveysIsActive.Rows)
              {
                LoadedSurveysIsActive = DataRow_Row["LoadedSurveys_IsActive"].ToString();
              }
            }
          }
        }

        if (LoadedSurveysIsActive == "Yes")
        {
          SurveyActive = "<span style='color:#008000;'>The current survey is Active, surveys can be created / modified</span>";
          HiddenField_SurveyActive.Value = "Yes";
        }
        else if (LoadedSurveysIsActive == "No")
        {
          SurveyActive = "<span style='color:#b0262e;'>The current survey is Not Active, surveys can't be created / modified</span>";
          HiddenField_SurveyActive.Value = "No";
        }


        if (string.IsNullOrEmpty(CreatedSurveysId))
        {
          Label_PreviousSurvey.Text = Convert.ToString("No " + DropDownList_LoadedSurveysName.SelectedItem.Text + " Survey has been created for " + DropDownList_Facility.SelectedItem.Text + " previously<br />" + SurveyActive, CultureInfo.CurrentCulture);
          HiddenField_PreviousSurvey.Value = "No";
        }
        else
        {
          Label_PreviousSurvey.Text = Convert.ToString("A " + DropDownList_LoadedSurveysName.SelectedItem.Text + " Survey has been created for " + DropDownList_Facility.SelectedItem.Text + " previously<br />" + SurveyActive, CultureInfo.CurrentCulture);
          HiddenField_PreviousSurvey.Value = "Yes";
        }
      }
    }

    private void SetButtonVisibility()
    {
      SetButtonSearchVisibility();
      SetButtonRemoveVisibility();
      SetButtonReAddVisibility();
      SetButtonAddVisibility();
      SetButtonCreateVisibility();
    }

    private void SetButtonSearchVisibility()
    {
      if (HiddenField_SurveyActive.Value == "Yes")
      {
        Button_SearchClear.Enabled = true;
        Button_Search.Enabled = true;
      }
      else
      {
        Button_SearchClear.Enabled = false;
        Button_Search.Enabled = false;
      }
    }

    private void SetButtonRemoveVisibility()
    {
      if (HiddenField_SurveyActive.Value == "Yes")
      {
        if (CheckBoxList_CreatedSurveysName.Items.Count > 0)
        {
          string CreatedSurveysName_Enabled = "No";
          for (int a = 0; a < CheckBoxList_CreatedSurveysName.Items.Count; a++)
          {
            if (CheckBoxList_CreatedSurveysName.Items[a].Enabled == true)
            {
              CreatedSurveysName_Enabled = "Yes";
              break;
            }
          }

          if (CreatedSurveysName_Enabled == "Yes")
          {
            Button_RemoveSelected.Enabled = true;
            Button_RemoveAll.Enabled = true;
          }
          else
          {
            Button_RemoveSelected.Enabled = false;
            Button_RemoveAll.Enabled = false;
          }
        }
        else
        {
          Button_RemoveSelected.Enabled = false;
          Button_RemoveAll.Enabled = false;
        }
      }
      else
      {
        Button_RemoveSelected.Enabled = false;
        Button_RemoveAll.Enabled = false;
      }
    }

    private void SetButtonReAddVisibility()
    {
      if (HiddenField_SurveyActive.Value == "Yes")
      {
        if (CheckBoxList_CreatedSurveysNameCanceled.Items.Count > 0)
        {
          Button_ReAddSelected.Enabled = true;
          Button_ReAddAll.Enabled = true;
        }
        else
        {
          Button_ReAddSelected.Enabled = false;
          Button_ReAddAll.Enabled = false;
        }
      }
      else
      {
        Button_ReAddSelected.Enabled = false;
        Button_ReAddAll.Enabled = false;
      }
    }

    private void SetButtonAddVisibility()
    {
      if (HiddenField_SurveyActive.Value == "Yes")
      {
        if (CheckBoxList_SearchName.Items.Count > 0)
        {
          Button_AddSelected.Enabled = true;
          Button_AddAll.Enabled = true;
        }
        else
        {
          Button_AddSelected.Enabled = false;
          Button_AddAll.Enabled = false;
        }
      }
      else
      {
        Button_AddSelected.Enabled = false;
        Button_AddAll.Enabled = false;
      }
    }

    private void SetButtonCreateVisibility()
    {
      if (HiddenField_SurveyActive.Value == "Yes")
      {
        string CreatedSurveysName_Enabled = "No";
        for (int a = 0; a < CheckBoxList_CreatedSurveysName.Items.Count; a++)
        {
          if (CheckBoxList_CreatedSurveysName.Items[a].Enabled == true && string.IsNullOrEmpty(CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysComplete"]))
          {
            CreatedSurveysName_Enabled = "Yes";
            break;
          }
        }

        if (CreatedSurveysName_Enabled == "Yes")
        {
          Button_Create.Enabled = true;
        }
        else
        {
          if (HiddenField_PreviousSurvey.Value == "Yes")
          {
            Button_Create.Enabled = true;
          }
          else
          {
            Button_Create.Enabled = false;
          }
        }
      }
      else
      {
        Button_Create.Enabled = false;
      }
    }

    private void SetCheckBoxListCreatedSurveysNameCancelledVisibility()
    {
      if (CheckBoxList_CreatedSurveysNameCanceled.Items.Count > 0)
      {
        for (int a = 0; a < CheckBoxList_CreatedSurveysNameCanceled.Items.Count; a++)
        {
          if (CheckBoxList_CreatedSurveysName.Items.Count > 0)
          {
            for (int b = 0; b < CheckBoxList_CreatedSurveysName.Items.Count; b++)
            {
              if (CheckBoxList_CreatedSurveysName.Items[b].Value.ToLower(CultureInfo.CurrentCulture) == CheckBoxList_CreatedSurveysNameCanceled.Items[a].Value.ToLower(CultureInfo.CurrentCulture))
              {
                CheckBoxList_CreatedSurveysNameCanceled.Items[a].Enabled = false;
                break;
              }
              else
              {
                CheckBoxList_CreatedSurveysNameCanceled.Items[a].Enabled = true;
              }
            }
          }
          else
          {
            CheckBoxList_CreatedSurveysNameCanceled.Items[a].Enabled = true;
          }
        }
      }
    }

    private void SetCheckBoxListSearchNameVisibility()
    {
      if (CheckBoxList_SearchName.Items.Count > 0)
      {
        for (int a = 0; a < CheckBoxList_SearchName.Items.Count; a++)
        {
          if (CheckBoxList_CreatedSurveysName.Items.Count > 0)
          {
            for (int b = 0; b < CheckBoxList_CreatedSurveysName.Items.Count; b++)
            {
              if (CheckBoxList_CreatedSurveysName.Items[b].Value.ToLower(CultureInfo.CurrentCulture) == CheckBoxList_SearchName.Items[a].Value.ToLower(CultureInfo.CurrentCulture))
              {
                CheckBoxList_SearchName.Items[a].Enabled = false;
                break;
              }
              else
              {
                CheckBoxList_SearchName.Items[a].Enabled = true;
              }
            }
          }
          else
          {
            CheckBoxList_SearchName.Items[a].Enabled = true;
          }
        }
      }
    }


    //--START-- --Search--//
    protected void Button_SearchClear_Click(object sender, EventArgs e)
    {
      TextBox_SearchUserName.Text = "";
      TextBox_SearchName.Text = "";
      TextBox_SearchEmployeeNumber.Text = "";
      Label_SearchMessage.Text = Convert.ToString("", CultureInfo.CurrentCulture);
      CheckBoxList_SearchName.Items.Clear();

      SetButtonVisibility();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      CheckBoxList_SearchName.Items.Clear();

      if ((!string.IsNullOrEmpty(TextBox_SearchUserName.Text)) || (!string.IsNullOrEmpty(TextBox_SearchName.Text)) || (!string.IsNullOrEmpty(TextBox_SearchEmployeeNumber.Text)))
      {
        string AD_UserName = "";
        string AD_DisplayName = "";
        string AD_Email = "";
        string AD_Error = "";

        DataTable DataTable_AD;
        using (DataTable_AD = new DataTable())
        {
          DataTable_AD.Locale = CultureInfo.CurrentCulture;
          DataTable_AD = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindAll(TextBox_SearchUserName.Text.ToString(), TextBox_SearchName.Text.ToString(), TextBox_SearchEmployeeNumber.Text.ToString(), "").Copy();
          if (DataTable_AD.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_AD.Rows)
            {
              AD_Error = DataRow_Row["Error"].ToString();
            }
          }
          else if (DataTable_AD.Columns.Count != 1)
          {
            if (DataTable_AD.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_AD.Rows)
              {
                AD_UserName = DataRow_Row["UserName"].ToString();
                AD_DisplayName = DataRow_Row["DisplayName"].ToString();
                AD_Email = DataRow_Row["Email"].ToString();

                if (!string.IsNullOrEmpty(AD_UserName))
                {
                  AD_UserName = "LHC\\" + AD_UserName;
                }

                ListItem ListItem_Employee = new ListItem(Convert.ToString(AD_DisplayName + " (" + AD_UserName + ") (" + AD_Email + ")", CultureInfo.CurrentCulture), Convert.ToString(AD_DisplayName + " (" + AD_UserName + ") (" + AD_Email + ")", CultureInfo.CurrentCulture), true);
                ListItem_Employee.Attributes.Add("CreatedSurveysUserName", AD_UserName);
                ListItem_Employee.Attributes.Add("CreatedSurveysEmail", AD_Email);
                ListItem_Employee.Attributes.Add("CreatedSurveysName", AD_DisplayName);
                ListItem_Employee.Attributes.Add("CreatedSurveysComplete", "");
                CheckBoxList_SearchName.Items.Add(ListItem_Employee);
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(AD_Error))
        {
          Label_SearchMessage.Text = AD_Error;
        }
        else
        {
          Label_SearchMessage.Text = "";
        }

        AD_UserName = "";
        AD_DisplayName = "";
        AD_Email = "";
        AD_Error = "";
      }
      else
      {
        Label_SearchMessage.Text = Convert.ToString("Username, Name or Surname, Employee Number needs to be provided for a Employee search ", CultureInfo.CurrentCulture);
      }


      SetButtonVisibility();
      SetCheckBoxListCreatedSurveysNameCancelledVisibility();
      SetCheckBoxListSearchNameVisibility();
    }
    //---END--- --Search--//


    //--START-- --Form--//
    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      GetPreviousSurvey();
      GetCheckBoxListCreatedSurveysName();
      GetCheckBoxListCreatedSurveysNameCancelled();
    }

    protected void DropDownList_LoadedSurveysName_SelectedIndexChanged(object sender, EventArgs e)
    {
      GetPreviousSurvey();
      GetCheckBoxListCreatedSurveysName();
      GetCheckBoxListCreatedSurveysNameCancelled();
    }

    protected void Button_RemoveSelected_Click(object sender, EventArgs e)
    {
      for (int a = CheckBoxList_CreatedSurveysName.Items.Count - 1; a >= 0; a--)
      {
        if (CheckBoxList_CreatedSurveysName.Items[a].Selected == true)
        {
          CheckBoxList_CreatedSurveysName.Items.RemoveAt(a);
        }
      }

      SetButtonVisibility();
      SetCheckBoxListCreatedSurveysNameCancelledVisibility();
      SetCheckBoxListSearchNameVisibility();
    }

    protected void Button_RemoveAll_Click(object sender, EventArgs e)
    {
      for (int a = CheckBoxList_CreatedSurveysName.Items.Count - 1; a >= 0; a--)
      {
        if (CheckBoxList_CreatedSurveysName.Items[a].Enabled == true)
        {
          CheckBoxList_CreatedSurveysName.Items.RemoveAt(a);
        }
      }

      SetButtonVisibility();
      SetCheckBoxListCreatedSurveysNameCancelledVisibility();
      SetCheckBoxListSearchNameVisibility();
    }

    protected void Button_ReAddSelected_Click(object sender, EventArgs e)
    {
      if (CheckBoxList_CreatedSurveysNameCanceled.Items.Count > 0)
      {
        for (int a = 0; a < CheckBoxList_CreatedSurveysNameCanceled.Items.Count; a++)
        {
          if (CheckBoxList_CreatedSurveysNameCanceled.Items[a].Selected == true)
          {
            string AddItem = "Yes";
            for (int b = 0; b < CheckBoxList_CreatedSurveysName.Items.Count; b++)
            {
              if (CheckBoxList_CreatedSurveysName.Items[b].Value == CheckBoxList_CreatedSurveysNameCanceled.Items[a].Value)
              {
                AddItem = "No";
              }
            }

            if (AddItem == "Yes")
            {
              ListItem ListItem_Employee = new ListItem(CheckBoxList_CreatedSurveysNameCanceled.Items[a].Text, CheckBoxList_CreatedSurveysNameCanceled.Items[a].Value, true);
              ListItem_Employee.Attributes.Add("CreatedSurveysUserName", CheckBoxList_CreatedSurveysNameCanceled.Items[a].Attributes["CreatedSurveysUserName"]);
              ListItem_Employee.Attributes.Add("CreatedSurveysEmail", CheckBoxList_CreatedSurveysNameCanceled.Items[a].Attributes["CreatedSurveysEmail"]);
              ListItem_Employee.Attributes.Add("CreatedSurveysName", CheckBoxList_CreatedSurveysNameCanceled.Items[a].Attributes["CreatedSurveysName"]);
              ListItem_Employee.Attributes.Add("CreatedSurveysComplete", CheckBoxList_CreatedSurveysNameCanceled.Items[a].Attributes["CreatedSurveysComplete"]);
              CheckBoxList_CreatedSurveysName.Items.Add(ListItem_Employee);
            }

            CheckBoxList_CreatedSurveysNameCanceled.Items[a].Selected = false;
          }
        }
      }

      SetButtonVisibility();
      SetCheckBoxListCreatedSurveysNameCancelledVisibility();
      SetCheckBoxListSearchNameVisibility();
    }

    protected void Button_ReAddAll_Click(object sender, EventArgs e)
    {
      if (CheckBoxList_CreatedSurveysNameCanceled.Items.Count > 0)
      {
        for (int a = 0; a < CheckBoxList_CreatedSurveysNameCanceled.Items.Count; a++)
        {
          if (CheckBoxList_CreatedSurveysNameCanceled.Items[a].Enabled == true)
          {
            ListItem ListItem_Employee = new ListItem(CheckBoxList_CreatedSurveysNameCanceled.Items[a].Text, CheckBoxList_CreatedSurveysNameCanceled.Items[a].Value, true);
            ListItem_Employee.Attributes.Add("CreatedSurveysUserName", CheckBoxList_CreatedSurveysNameCanceled.Items[a].Attributes["CreatedSurveysUserName"]);
            ListItem_Employee.Attributes.Add("CreatedSurveysEmail", CheckBoxList_CreatedSurveysNameCanceled.Items[a].Attributes["CreatedSurveysEmail"]);
            ListItem_Employee.Attributes.Add("CreatedSurveysName", CheckBoxList_CreatedSurveysNameCanceled.Items[a].Attributes["CreatedSurveysName"]);
            ListItem_Employee.Attributes.Add("CreatedSurveysComplete", CheckBoxList_CreatedSurveysNameCanceled.Items[a].Attributes["CreatedSurveysComplete"]);
            CheckBoxList_CreatedSurveysName.Items.Add(ListItem_Employee);

            CheckBoxList_CreatedSurveysNameCanceled.Items[a].Selected = false;
          }
        }
      }

      SetButtonVisibility();
      SetCheckBoxListCreatedSurveysNameCancelledVisibility();
      SetCheckBoxListSearchNameVisibility();
    }

    protected void Button_AddSelected_Click(object sender, EventArgs e)
    {
      if (CheckBoxList_SearchName.Items.Count > 0)
      {
        for (int a = 0; a < CheckBoxList_SearchName.Items.Count; a++)
        {
          if (CheckBoxList_SearchName.Items[a].Selected == true)
          {
            string AddItem = "Yes";
            for (int b = 0; b < CheckBoxList_CreatedSurveysName.Items.Count; b++)
            {
              if (CheckBoxList_CreatedSurveysName.Items[b].Value == CheckBoxList_SearchName.Items[a].Value)
              {
                AddItem = "No";
              }
            }

            if (AddItem == "Yes")
            {
              ListItem ListItem_Employee = new ListItem(CheckBoxList_SearchName.Items[a].Text, CheckBoxList_SearchName.Items[a].Value, true);
              ListItem_Employee.Attributes.Add("CreatedSurveysUserName", CheckBoxList_SearchName.Items[a].Attributes["CreatedSurveysUserName"]);
              ListItem_Employee.Attributes.Add("CreatedSurveysEmail", CheckBoxList_SearchName.Items[a].Attributes["CreatedSurveysEmail"]);
              ListItem_Employee.Attributes.Add("CreatedSurveysName", CheckBoxList_SearchName.Items[a].Attributes["CreatedSurveysName"]);
              ListItem_Employee.Attributes.Add("CreatedSurveysComplete", CheckBoxList_SearchName.Items[a].Attributes["CreatedSurveysComplete"]);
              CheckBoxList_CreatedSurveysName.Items.Add(ListItem_Employee);
            }

            CheckBoxList_SearchName.Items[a].Selected = false;
          }
        }
      }

      SetButtonVisibility();
      SetCheckBoxListCreatedSurveysNameCancelledVisibility();
      SetCheckBoxListSearchNameVisibility();
    }

    protected void Button_AddAll_Click(object sender, EventArgs e)
    {
      if (CheckBoxList_SearchName.Items.Count > 0)
      {
        for (int a = 0; a < CheckBoxList_SearchName.Items.Count; a++)
        {
          if (CheckBoxList_SearchName.Items[a].Enabled == true)
          {
            ListItem ListItem_Employee = new ListItem(CheckBoxList_SearchName.Items[a].Text, CheckBoxList_SearchName.Items[a].Value, true);
            ListItem_Employee.Attributes.Add("CreatedSurveysUserName", CheckBoxList_SearchName.Items[a].Attributes["CreatedSurveysUserName"]);
            ListItem_Employee.Attributes.Add("CreatedSurveysEmail", CheckBoxList_SearchName.Items[a].Attributes["CreatedSurveysEmail"]);
            ListItem_Employee.Attributes.Add("CreatedSurveysName", CheckBoxList_SearchName.Items[a].Attributes["CreatedSurveysName"]);
            ListItem_Employee.Attributes.Add("CreatedSurveysComplete", CheckBoxList_SearchName.Items[a].Attributes["CreatedSurveysComplete"]);
            CheckBoxList_CreatedSurveysName.Items.Add(ListItem_Employee);

            CheckBoxList_SearchName.Items[a].Selected = false;
          }
        }
      }

      SetButtonVisibility();
      SetCheckBoxListCreatedSurveysNameCancelledVisibility();
      SetCheckBoxListSearchNameVisibility();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_PharmacySurveys_Create.aspx", false);
    }

    protected void Button_Create_Click(object sender, EventArgs e)
    {
      string InvalidFormMessage = "";
      string FacilityId = DropDownList_Facility.SelectedValue;
      string FacilityName = DropDownList_Facility.SelectedItem.Text;
      string LoadedSurveysName = (DropDownList_LoadedSurveysName.SelectedItem.Text).Substring(0, (DropDownList_LoadedSurveysName.SelectedItem.Text).IndexOf(" (", StringComparison.CurrentCulture));
      string LoadedSurveysFY = (DropDownList_LoadedSurveysName.SelectedItem.Text).Substring((DropDownList_LoadedSurveysName.SelectedItem.Text).IndexOf(" (", StringComparison.CurrentCulture) + 2, (DropDownList_LoadedSurveysName.SelectedItem.Text).IndexOf(")", StringComparison.CurrentCulture) - (DropDownList_LoadedSurveysName.SelectedItem.Text).IndexOf(" (", StringComparison.CurrentCulture) - 2);

      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue) || string.IsNullOrEmpty(DropDownList_LoadedSurveysName.SelectedValue))
      {
        InvalidFormMessage = "All red fields are required";
      }
      else
      {
        string SQLStringCreatedSurveys = @"SELECT	CreatedSurveys_Id , 
				                                          CreatedSurveys_UserName ,
				                                          CreatedSurveys_Email ,
				                                          CreatedSurveys_Name ,
				                                          CASE 
					                                          WHEN CreatedSurveys_IsActive = 1 THEN 'Yes'
					                                          ELSE 'No'
				                                          END AS CreatedSurveys_IsActive
                                          FROM		Form_PharmacySurveys_CreatedSurveys
                                          WHERE		Facility_Id = @Facility_Id
				                                          AND LoadedSurveys_Id = @LoadedSurveys_Id";
        using (SqlCommand SqlCommand_CreatedSurveys = new SqlCommand(SQLStringCreatedSurveys))
        {
          SqlCommand_CreatedSurveys.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
          SqlCommand_CreatedSurveys.Parameters.AddWithValue("@LoadedSurveys_Id", DropDownList_LoadedSurveysName.SelectedValue);
          DataTable DataTable_CreatedSurveys;
          using (DataTable_CreatedSurveys = new DataTable())
          {
            DataTable_CreatedSurveys.Locale = CultureInfo.CurrentCulture;
            DataTable_CreatedSurveys = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CreatedSurveys).Copy();

            Create_Update(DataTable_CreatedSurveys);

            Create_Insert(DataTable_CreatedSurveys, FacilityName, LoadedSurveysName, LoadedSurveysFY);
          }
        }
      }

      if (string.IsNullOrEmpty(InvalidFormMessage))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Created Surveys", "Form_PharmacySurveys_List.aspx?s_Facility_Id=" + FacilityId + "&s_PharmacySurveys_LoadedSurveysName=" + LoadedSurveysName + "&s_PharmacySurveys_LoadedSurveysFY=" + LoadedSurveysFY), false);
      }
      else
      {
        Label_InvalidFormMessage.Text = Convert.ToString(InvalidFormMessage, CultureInfo.CurrentCulture);
      }
    }

    private void Create_Update(DataTable dataTable_CreatedSurveys)
    {
      if (dataTable_CreatedSurveys.Rows.Count > 0)
      {
        foreach (DataRow DataRow_Row in dataTable_CreatedSurveys.Rows)
        {
          string CreatedSurveysId = DataRow_Row["CreatedSurveys_Id"].ToString().ToLower(CultureInfo.CurrentCulture);
          string CreatedSurveysUserName = DataRow_Row["CreatedSurveys_UserName"].ToString().ToLower(CultureInfo.CurrentCulture);
          string CreatedSurveysEmail = DataRow_Row["CreatedSurveys_Email"].ToString().ToLower(CultureInfo.CurrentCulture);
          string CreatedSurveysName = DataRow_Row["CreatedSurveys_Name"].ToString().ToLower(CultureInfo.CurrentCulture);
          string CreatedSurveysIsActive = DataRow_Row["CreatedSurveys_IsActive"].ToString();

          string CheckBoxListFound = "No";
          for (int a = 0; a < CheckBoxList_CreatedSurveysName.Items.Count; a++)
          {
            if (CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysUserName"].ToLower(CultureInfo.CurrentCulture) == CreatedSurveysUserName && CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysEmail"].ToLower(CultureInfo.CurrentCulture) == CreatedSurveysEmail && CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysName"].ToLower(CultureInfo.CurrentCulture) == CreatedSurveysName)
            {
              CheckBoxListFound = "Yes";

              if (CreatedSurveysIsActive == "No")
              {
                string SQLStringUpdateCreatedSurveys = "UPDATE Form_PharmacySurveys_CreatedSurveys SET CreatedSurveys_IsActive = 1 WHERE CreatedSurveys_Id = @CreatedSurveys_Id";
                using (SqlCommand SqlCommand_UpdateCreatedSurveys = new SqlCommand(SQLStringUpdateCreatedSurveys))
                {
                  SqlCommand_UpdateCreatedSurveys.Parameters.AddWithValue("@CreatedSurveys_Id", CreatedSurveysId);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateCreatedSurveys);
                }
              }

              break;
            }
          }

          if (CheckBoxListFound == "No")
          {
            string SQLStringUpdateCreatedSurveys = "UPDATE Form_PharmacySurveys_CreatedSurveys SET CreatedSurveys_IsActive = 0 WHERE CreatedSurveys_Id = @CreatedSurveys_Id";
            using (SqlCommand SqlCommand_UpdateCreatedSurveys = new SqlCommand(SQLStringUpdateCreatedSurveys))
            {
              SqlCommand_UpdateCreatedSurveys.Parameters.AddWithValue("@CreatedSurveys_Id", CreatedSurveysId);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateCreatedSurveys);
            }
          }
        }
      }
    }

    private void Create_Insert(DataTable dataTable_CreatedSurveys, string facilityName, string loadedSurveysName, string loadedSurveysFY)
    {
      for (int a = 0; a < CheckBoxList_CreatedSurveysName.Items.Count; a++)
      {
        DataRow[] CreatedSurveys_Id = dataTable_CreatedSurveys.Select("CreatedSurveys_UserName = '" + CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysUserName"] + "' AND CreatedSurveys_Email = '" + CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysEmail"] + "' AND CreatedSurveys_Name = '" + CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysName"] + "'");

        if (CreatedSurveys_Id.Length == 0)
        {
          string CreatedSurveysId = "";
          string SQLStringInsertCreatedSurveys = @" INSERT INTO Form_PharmacySurveys_CreatedSurveys
                                                      ( Facility_Id , LoadedSurveys_Id , CreatedSurveys_UserName , CreatedSurveys_Email , CreatedSurveys_Name , CreatedSurveys_CreatedDate , CreatedSurveys_CreatedBy , CreatedSurveys_ModifiedDate , CreatedSurveys_ModifiedBy , CreatedSurveys_History , CreatedSurveys_IsActive , CreatedSurveys_Archived )
                                                      VALUES
                                                      ( @Facility_Id , @LoadedSurveys_Id , @CreatedSurveys_UserName , @CreatedSurveys_Email , @CreatedSurveys_Name , GETDATE() , @CreatedSurveys_CreatedBy , GETDATE() , @CreatedSurveys_ModifiedBy , NULL , 1 , 0 ); 
                                                      SELECT SCOPE_IDENTITY()";
          using (SqlCommand SqlCommand_InsertCreatedSurveys = new SqlCommand(SQLStringInsertCreatedSurveys))
          {
            SqlCommand_InsertCreatedSurveys.Parameters.AddWithValue("@Facility_Id", DropDownList_Facility.SelectedValue);
            SqlCommand_InsertCreatedSurveys.Parameters.AddWithValue("@LoadedSurveys_Id", DropDownList_LoadedSurveysName.SelectedValue);
            SqlCommand_InsertCreatedSurveys.Parameters.AddWithValue("@CreatedSurveys_UserName", CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysUserName"]);
            SqlCommand_InsertCreatedSurveys.Parameters.AddWithValue("@CreatedSurveys_Email", CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysEmail"]);
            SqlCommand_InsertCreatedSurveys.Parameters.AddWithValue("@CreatedSurveys_Name", CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysName"]);
            SqlCommand_InsertCreatedSurveys.Parameters.AddWithValue("@CreatedSurveys_CreatedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_InsertCreatedSurveys.Parameters.AddWithValue("@CreatedSurveys_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            CreatedSurveysId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertCreatedSurveys);
          }

          //--START-- --Email--//
          string SecurityUserDisplayName = "";
          string SQLStringSecurityUser = "SELECT SecurityUser_DisplayName FROM Administration_SecurityUser WHERE SecurityUser_UserName = @SecurityUser_UserName";
          using (SqlCommand SqlCommand_SecurityUser = new SqlCommand(SQLStringSecurityUser))
          {
            SqlCommand_SecurityUser.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
            DataTable DataTable_SecurityUser;
            using (DataTable_SecurityUser = new DataTable())
            {
              DataTable_SecurityUser.Locale = CultureInfo.CurrentCulture;
              DataTable_SecurityUser = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUser).Copy();
              if (DataTable_SecurityUser.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_SecurityUser.Rows)
                {
                  SecurityUserDisplayName = DataRow_Row["SecurityUser_DisplayName"].ToString();
                }
              }
            }
          }

          string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("79");
          string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
          string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("47");
          string BodyString = EmailTemplate;

          BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysName"] + "");
          BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
          BodyString = BodyString.Replace(";replace;FacilityName;replace;", "" + facilityName + "");
          BodyString = BodyString.Replace(";replace;SurveyName;replace;", "" + loadedSurveysName + "");
          BodyString = BodyString.Replace(";replace;SurveyFY;replace;", "" + loadedSurveysFY + "");
          BodyString = BodyString.Replace(";replace;SurveyCreatedBy;replace;", "" + SecurityUserDisplayName + "");
          BodyString = BodyString.Replace(";replace;EmployeeName;replace;", "" + CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysName"] + "");
          BodyString = BodyString.Replace(";replace;EmployeeEmail;replace;", "" + CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysEmail"] + "");
          BodyString = BodyString.Replace(";replace;EmployeeUserName;replace;", "" + CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysUserName"] + "");
          BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
          BodyString = BodyString.Replace(";replace;CreatedSurveysId;replace;", "" + CreatedSurveysId + "");

          string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
          string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
          string EmailBody = HeaderString + BodyString + FooterString;

          string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", CheckBoxList_CreatedSurveysName.Items[a].Attributes["CreatedSurveysEmail"], FormName, EmailBody);

          if (!string.IsNullOrEmpty(EmailSend))
          {
            EmailSend = "";
          }

          EmailBody = "";
          EmailTemplate = "";
          URLAuthority = "";
          FormName = "";
          //---END--- --Email--//

          CreatedSurveysId = "";
        }
      }
    }
    //---END--- --Form--//
  }
}