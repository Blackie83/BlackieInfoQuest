using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Data;
using System.Web.UI;

namespace InfoQuestForm
{
  public partial class Form_PharmacySurveys_LoadedSurveys_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_PharmacySurveys_LoadedSurveys_List, this.GetType(), "UpdateProgress_Start", "Validation_Form_DuplicateSurveys();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("47").Replace(" Form", "")).ToString() + " : Loaded Surveys", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("47").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_DuplicateSurveyHeading.Text = Convert.ToString("Create a Duplicate Survey of current selected Survey", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("47").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          if (string.IsNullOrEmpty(Request.QueryString["s_LoadedSurveys_Id"]))
          {
            DivLoadedSurveys1.Visible = false;
            TableDuplicateSurveys.Visible = false;
            DivLoadedSurveys2.Visible = false;
            TableLoadedSurveys.Visible = false;
          }
          else
          {
            DivLoadedSurveys1.Visible = true;
            TableDuplicateSurveys.Visible = true;
            DivLoadedSurveys2.Visible = true;
            TableLoadedSurveys.Visible = true;

            SetFormQueryString();
          }

          if (TableDuplicateSurveys.Visible == true)
          {
            TableDuplicateSurveysVisible();
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_PharmacySurveys_LoadedSurveys_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Pharmacy Surveys", "23");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_LoadedSurveys.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_LoadedSurveys.SelectCommand = @"SELECT		LoadedSurveys_Id ,
					                                                    LoadedSurveys_Name + ' (' + LoadedSurveys_FY + ')' AS LoadedSurveys_Name
                                                    FROM			Form_PharmacySurveys_LoadedSurveys
                                                    ORDER BY	LoadedSurveys_FY DESC ,
					                                                    LoadedSurveys_Name";

      SqlDataSource_DuplicateFY.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_DuplicateFY.SelectCommand = @"SELECT	CASE 
                                                            WHEN MONTH(GETDATE()) IN ('1','2','3','4','5','6','7','8','9') THEN CAST((YEAR(GETDATE()) + 0) AS NVARCHAR(10))
                                                            WHEN MONTH(GETDATE()) IN ('10','11','12') THEN CAST((YEAR(GETDATE()) + 1) AS NVARCHAR(10))
                                                          END AS FY
                                                  UNION
                                                  SELECT	CASE 
                                                            WHEN MONTH(GETDATE()) IN ('1','2','3','4','5','6','7','8','9') THEN CAST((YEAR(GETDATE()) + 1) AS NVARCHAR(10))
                                                            WHEN MONTH(GETDATE()) IN ('10','11','12') THEN CAST((YEAR(GETDATE()) + 2) AS NVARCHAR(10))
                                                          END AS FY";

      SqlDataSource_PharmacySurveys_LoadedSurveys_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedSurveys_List.SelectCommand = "spForm_Get_PharmacySurveys_LoadedSurveys_List";
      SqlDataSource_PharmacySurveys_LoadedSurveys_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacySurveys_LoadedSurveys_List.CancelSelectOnNullParameter = false;
      SqlDataSource_PharmacySurveys_LoadedSurveys_List.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_LoadedSurveys_List.SelectParameters.Add("LoadedSurveysId", TypeCode.String, Request.QueryString["s_LoadedSurveys_Id"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_LoadedSurveys.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_LoadedSurveys_Id"] == null)
        {
          DropDownList_LoadedSurveys.SelectedValue = "";
        }
        else
        {
          DropDownList_LoadedSurveys.SelectedValue = Request.QueryString["s_LoadedSurveys_Id"];
        }
      }
    }

    protected void TableDuplicateSurveysVisible()
    {
      TextBox_DuplicateName.Attributes.Add("OnChange", "Validation_Form_DuplicateSurveys();");
      TextBox_DuplicateName.Attributes.Add("OnInput", "Validation_Form_DuplicateSurveys();");
      DropDownList_DuplicateFY.Attributes.Add("OnChange", "Validation_Form_DuplicateSurveys();");
    }


    //--START-- --Search--//
    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys List", "Form_PharmacySurveys_LoadedSurveys_List.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_LoadedSurveys.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys List", "Form_PharmacySurveys_LoadedSurveys_List.aspx"), false);
      }
      else
      {
        if (string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_LoadedSurveys_Id=" + DropDownList_LoadedSurveys.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_PharmacySurveys_LoadedSurveys_List.aspx?" + SearchField1;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys List", FinalURL);
        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_CreateSurvey_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?LoadedSurveys_Id=0"), false);
    }
    //---END--- --Search--//


    //--START-- --Duplicate Surveys--//
    protected void Button_ClearDuplicateSurvey_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys List", "Form_PharmacySurveys_LoadedSurveys_List.aspx?s_LoadedSurveys_Id=" + Request.QueryString["s_LoadedSurveys_Id"]), false);
    }

    protected void Button_CreateDuplicateSurvey_Click(object sender, EventArgs e)
    {
      string InvalidFormMessage = InsertValidation_CreateDuplicateSurvey();

      if (!string.IsNullOrEmpty(InvalidFormMessage))
      {
        Label_InsertInvalidFormMessage.Text = InvalidFormMessage;
      }
      else if (string.IsNullOrEmpty(InvalidFormMessage))
      {
        string LoadedSurveys_Id = "";

        string SQLStringDuplicateSurvey = "EXECUTE spForm_Execute_PharmacySurveys_DuplicateSurvey @LoadedSurveys_OldSurveyId , @LoadedSurveys_NewSurveyName , @LoadedSurveys_NewSurveyFY , @LoadedSurveys_NewSurveyUserName";
        using (SqlCommand SqlCommand_DuplicateSurvey = new SqlCommand(SQLStringDuplicateSurvey))
        {
          SqlCommand_DuplicateSurvey.Parameters.AddWithValue("@LoadedSurveys_OldSurveyId", Request.QueryString["s_LoadedSurveys_Id"]);
          SqlCommand_DuplicateSurvey.Parameters.AddWithValue("@LoadedSurveys_NewSurveyName", TextBox_DuplicateName.Text);
          SqlCommand_DuplicateSurvey.Parameters.AddWithValue("@LoadedSurveys_NewSurveyFY", DropDownList_DuplicateFY.SelectedValue);
          SqlCommand_DuplicateSurvey.Parameters.AddWithValue("@LoadedSurveys_NewSurveyUserName", Request.ServerVariables["LOGON_USER"]);
          LoadedSurveys_Id = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_DuplicateSurvey);
        }

        if (!string.IsNullOrEmpty(LoadedSurveys_Id))
        {
          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys List", "Form_PharmacySurveys_LoadedSurveys_List.aspx?s_LoadedSurveys_Id=" + LoadedSurveys_Id), false);
        }
        else
        {
          Label_InsertInvalidFormMessage.Text = Convert.ToString("Survey could not be created", CultureInfo.CurrentCulture);
        }
      }

      GridView_PharmacySurveys_LoadedSurveys_List.DataBind();
    }

    protected string InsertValidation_CreateDuplicateSurvey()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_DuplicateName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_DuplicateFY.SelectedValue))
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
          SqlCommand_LoadedSurveys.Parameters.AddWithValue("@LoadedSurveys_Name", TextBox_DuplicateName.Text);
          SqlCommand_LoadedSurveys.Parameters.AddWithValue("@LoadedSurveys_FY", DropDownList_DuplicateFY.SelectedValue);
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
    //---END--- --Duplicate Surveys--//


    //--START-- --List PharmacySurveys_LoadedSurveys_List--//
    protected void SqlDataSource_PharmacySurveys_LoadedSurveys_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_PharmacySurveys_LoadedSurveys_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_PharmacySurveys_LoadedSurveys_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_PharmacySurveys_LoadedSurveys_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_PharmacySurveys_LoadedSurveys_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_PharmacySurveys_LoadedSurveys_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_PharmacySurveys_LoadedSurveys_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_PharmacySurveys_LoadedSurveys_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_PharmacySurveys_LoadedSurveys_List.PageSize > 20 && GridView_PharmacySurveys_LoadedSurveys_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_PharmacySurveys_LoadedSurveys_List.PageSize > 50 && GridView_PharmacySurveys_LoadedSurveys_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_PharmacySurveys_LoadedSurveys_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_PharmacySurveys_LoadedSurveys_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_PharmacySurveys_LoadedSurveys_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_PharmacySurveys_LoadedSurveys_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }


      for (int i = GridView_PharmacySurveys_LoadedSurveys_List.Rows.Count - 1; i > 0; i--)
      {
        GridViewRow GridViewRow_Row = GridView_PharmacySurveys_LoadedSurveys_List.Rows[i];
        GridViewRow GridViewRow_PreviousRow = GridView_PharmacySurveys_LoadedSurveys_List.Rows[i - 1];
        for (int j = 0; j < 3; j++)
        {
          HyperLink HyperLink_LoadedSurveys = (HyperLink)GridViewRow_Row.Cells[j].FindControl("HyperLink_LoadedSurveys");
          HyperLink HyperLink_LoadedSections = (HyperLink)GridViewRow_Row.Cells[j].FindControl("HyperLink_LoadedSections");
          HyperLink HyperLink_LoadedQuestions = (HyperLink)GridViewRow_Row.Cells[j].FindControl("HyperLink_LoadedQuestions");

          HyperLink HyperLink_LoadedSurveys_PreviousRow = (HyperLink)GridViewRow_PreviousRow.Cells[j].FindControl("HyperLink_LoadedSurveys");
          HyperLink HyperLink_LoadedSections_PreviousRow = (HyperLink)GridViewRow_PreviousRow.Cells[j].FindControl("HyperLink_LoadedSections");
          HyperLink HyperLink_LoadedQuestions_PreviousRow = (HyperLink)GridViewRow_PreviousRow.Cells[j].FindControl("HyperLink_LoadedQuestions");

          string LoadedName = "";
          string LoadedName_PreviousRow = "";

          if (j == 0)
          {
            LoadedName = HyperLink_LoadedSurveys.Text;
            LoadedName_PreviousRow = HyperLink_LoadedSurveys_PreviousRow.Text;
          }
          else if (j == 1)
          {
            LoadedName = HyperLink_LoadedSections.Text;
            LoadedName_PreviousRow = HyperLink_LoadedSections_PreviousRow.Text;
          }
          else if (j == 2)
          {
            LoadedName = HyperLink_LoadedQuestions.Text;
            LoadedName_PreviousRow = HyperLink_LoadedQuestions_PreviousRow.Text;
          }
          else
          {
            LoadedName = GridViewRow_Row.Cells[j].Text;
            LoadedName_PreviousRow = GridViewRow_PreviousRow.Cells[j].Text;
          }

          if (LoadedName == LoadedName_PreviousRow)
          {
            if (GridViewRow_PreviousRow.Cells[j].RowSpan == 0)
            {
              if (GridViewRow_Row.Cells[j].RowSpan == 0)
              {
                GridViewRow_PreviousRow.Cells[j].RowSpan += 2;
              }
              else
              {
                GridViewRow_PreviousRow.Cells[j].RowSpan = GridViewRow_Row.Cells[j].RowSpan + 1;
              }

              GridViewRow_Row.Cells[j].Visible = false;
            }
          }

          GridViewRow_Row.Cells[j].BackColor = Color.FromName("#f7f7f7");
          GridViewRow_Row.Cells[j].ForeColor = Color.FromName("#000000");
        }
      }


      for (int i = 0; i < GridView_PharmacySurveys_LoadedSurveys_List.Rows.Count; i++)
      {
        if (GridView_PharmacySurveys_LoadedSurveys_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          GridViewRow GridViewRow_Row = GridView_PharmacySurveys_LoadedSurveys_List.Rows[i];

          for (int j = 0; j < 4; j++)
          {
            HyperLink HyperLink_LoadedSurveys = (HyperLink)GridViewRow_Row.Cells[j].FindControl("HyperLink_LoadedSurveys");
            HyperLink HyperLink_LoadedSections = (HyperLink)GridViewRow_Row.Cells[j].FindControl("HyperLink_LoadedSections");
            HyperLink HyperLink_LoadedQuestions = (HyperLink)GridViewRow_Row.Cells[j].FindControl("HyperLink_LoadedQuestions");
            HyperLink HyperLink_LoadedAnswers = (HyperLink)GridViewRow_Row.Cells[j].FindControl("HyperLink_LoadedAnswers");

            string LoadedName = "";

            if (j == 0)
            {
              LoadedName = HyperLink_LoadedSurveys.Text;
            }
            else if (j == 1)
            {
              LoadedName = HyperLink_LoadedSections.Text;
            }
            else if (j == 2)
            {
              LoadedName = HyperLink_LoadedQuestions.Text;
            }
            else if (j == 3)
            {
              LoadedName = HyperLink_LoadedAnswers.Text;
            }

            if (!string.IsNullOrEmpty(LoadedName))
            {
              string IsActive = LoadedName.Substring(LoadedName.LastIndexOf("(", StringComparison.CurrentCulture) + 1, LoadedName.Length - LoadedName.LastIndexOf("(", StringComparison.CurrentCulture) - 6);

              if (IsActive == "No")
              {
                GridView_PharmacySurveys_LoadedSurveys_List.Rows[i].Cells[j].BackColor = Color.FromName("#d46e6e");
                GridView_PharmacySurveys_LoadedSurveys_List.Rows[i].Cells[j].ForeColor = Color.FromName("#333333");
              }
              else if (IsActive == "Yes")
              {
                GridView_PharmacySurveys_LoadedSurveys_List.Rows[i].Cells[j].BackColor = Color.FromName("#77cf9c");
                GridView_PharmacySurveys_LoadedSurveys_List.Rows[i].Cells[j].ForeColor = Color.FromName("#333333");
              }
            }
          }
        }
      }
    }

    protected void GridView_PharmacySurveys_LoadedSurveys_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink_UpdateSurvey(object loadedSurveys_Id, object loadedSurveys_Name)
    {
      return "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?Search_LoadedSurveysId=" + Request.QueryString["s_LoadedSurveys_Id"] + "&LoadedSurveys_Id=" + loadedSurveys_Id + "") + "'>" + loadedSurveys_Name + "</a>";
    }

    public string GetLink_UpdateSections(object loadedSections_Id, object loadedSections_Name)
    {
      return "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?Search_LoadedSurveysId=" + Request.QueryString["s_LoadedSurveys_Id"] + "&LoadedSections_Id=" + loadedSections_Id + "") + "'>" + loadedSections_Name + "</a>";
    }

    public string GetLink_UpdateQuestions(object loadedQuestions_Id, object loadedQuestions_Name)
    {
      return "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?Search_LoadedSurveysId=" + Request.QueryString["s_LoadedSurveys_Id"] + "&LoadedQuestions_Id=" + loadedQuestions_Id + "") + "'>" + loadedQuestions_Name + "</a>";
    }

    public string GetLink_UpdateAnswers(object loadedAnswers_Id, object loadedAnswers_Name)
    {
      return "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Loaded Surveys", "Form_PharmacySurveys_LoadedSurveys.aspx?Search_LoadedSurveysId=" + Request.QueryString["s_LoadedSurveys_Id"] + "&LoadedAnswers_Id=" + loadedAnswers_Id + "") + "'>" + loadedAnswers_Name + "</a>";
    }
    //---END--- --List PharmacySurveys_LoadedSurveys_List--//
  }
}