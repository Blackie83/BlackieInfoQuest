using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;

namespace InfoQuestForm
{
  public partial class Form_PharmacySurveys_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("47").Replace(" Form", "")).ToString() + " : Created Surveys", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("47").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("47").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          SetFormQueryString();
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

        string SQLStringSecurity = @" SELECT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('47'))
                                      UNION
                                      SELECT CreatedSurveys_UserName FROM Form_PharmacySurveys_CreatedSurveys WHERE CreatedSurveys_UserName = @SecurityUser_UserName";
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
        ((Label)PageUpdateProgress_PharmacySurveys_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

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
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "Form_PharmacySurveys_CreatedSurveys");
      SqlDataSource_PharmacySurveys_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PharmacySurveys_LoadedSurveysName.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedSurveysName.SelectCommand = "SELECT DISTINCT LoadedSurveys_Name FROM Form_PharmacySurveys_LoadedSurveys ORDER BY LoadedSurveys_Name";

      SqlDataSource_PharmacySurveys_LoadedSurveysFY.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_LoadedSurveysFY.SelectCommand = "SELECT DISTINCT LoadedSurveys_FY FROM Form_PharmacySurveys_LoadedSurveys ORDER BY LoadedSurveys_FY DESC";

      SqlDataSource_PharmacySurveys_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PharmacySurveys_List.SelectCommand = "spForm_Get_PharmacySurveys_CreatedSurveys_List";
      SqlDataSource_PharmacySurveys_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PharmacySurveys_List.CancelSelectOnNullParameter = false;
      SqlDataSource_PharmacySurveys_List.SelectParameters.Clear();
      SqlDataSource_PharmacySurveys_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_PharmacySurveys_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_PharmacySurveys_List.SelectParameters.Add("LoadedSurveysName", TypeCode.String, Request.QueryString["s_PharmacySurveys_LoadedSurveysName"]);
      SqlDataSource_PharmacySurveys_List.SelectParameters.Add("LoadedSurveysFY", TypeCode.String, Request.QueryString["s_PharmacySurveys_LoadedSurveysFY"]);
      SqlDataSource_PharmacySurveys_List.SelectParameters.Add("CreatedSurveysName", TypeCode.String, Request.QueryString["s_PharmacySurveys_CreatedSurveysName"]);
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

      if (string.IsNullOrEmpty(DropDownList_LoadedSurveysName.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_PharmacySurveys_LoadedSurveysName"] == null)
        {
          DropDownList_LoadedSurveysName.SelectedValue = "";
        }
        else
        {
          DropDownList_LoadedSurveysName.SelectedValue = Request.QueryString["s_PharmacySurveys_LoadedSurveysName"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_LoadedSurveysFY.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_PharmacySurveys_LoadedSurveysFY"] == null)
        {
          DropDownList_LoadedSurveysFY.SelectedValue = "";
        }
        else
        {
          DropDownList_LoadedSurveysFY.SelectedValue = Request.QueryString["s_PharmacySurveys_LoadedSurveysFY"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_CreatedSurveysName.Text.ToString()))
      {
        if (Request.QueryString["s_PharmacySurveys_CreatedSurveysName"] == null)
        {
          TextBox_CreatedSurveysName.Text = "";
        }
        else
        {
          TextBox_CreatedSurveysName.Text = Request.QueryString["s_PharmacySurveys_CreatedSurveysName"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = DropDownList_LoadedSurveysName.SelectedValue;
      string SearchField3 = DropDownList_LoadedSurveysFY.SelectedValue;
      string SearchField4 = Server.HtmlEncode(TextBox_CreatedSurveysName.Text);

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_PharmacySurveys_LoadedSurveysName=" + DropDownList_LoadedSurveysName.SelectedValue + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_PharmacySurveys_LoadedSurveysFY=" + DropDownList_LoadedSurveysFY.SelectedValue + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_PharmacySurveys_CreatedSurveysName=" + Server.HtmlEncode(TextBox_CreatedSurveysName.Text.ToString()) + "&";
      }

      string FinalURL = "Form_PharmacySurveys_List.aspx?" + SearchField1 +SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys Created Surveys", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys List", "Form_PharmacySurveys_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_PharmacySurveys_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_PharmacySurveys_List.PageSize = Convert.ToInt32(((DropDownList)GridView_PharmacySurveys_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_PharmacySurveys_List.PageIndex = ((DropDownList)GridView_PharmacySurveys_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_PharmacySurveys_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_PharmacySurveys_List.PageSize <= 20)
        {
          ((DropDownList)GridView_PharmacySurveys_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_PharmacySurveys_List.PageSize > 20 && GridView_PharmacySurveys_List.PageSize <= 50)
        {
          ((DropDownList)GridView_PharmacySurveys_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_PharmacySurveys_List.PageSize > 50 && GridView_PharmacySurveys_List.PageSize <= 100)
        {
          ((DropDownList)GridView_PharmacySurveys_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_PharmacySurveys_List.Rows.Count; i++)
      {
        if (GridView_PharmacySurveys_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_List.Rows[i].Cells[5].Text == "Yes")
          {
            GridView_List.Rows[i].Cells[5].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_List.Rows[i].Cells[5].Text == "Cancelled")
          {
            GridView_List.Rows[i].Cells[5].BackColor = Color.FromName("#ffcc66");
            GridView_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_PharmacySurveys_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_PharmacySurveys_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_PharmacySurveys_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_PharmacySurveys_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_PharmacySurveys_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys New Survey", "Form_PharmacySurveys_Create.aspx"), false);
    }

    public string GetLink(object createdSurveys_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys New Survey", "Form_PharmacySurveys.aspx?CreatedSurveysId=" + createdSurveys_Id + "") + "'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Pharmacy Surveys New Survey", "Form_PharmacySurveys.aspx?CreatedSurveysId=" + createdSurveys_Id + "") + "'>View</a>";
        }
      }

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_PharmacySurveys_LoadedSurveysName"];
      string SearchField3 = Request.QueryString["s_PharmacySurveys_LoadedSurveysFY"];
      string SearchField4 = Request.QueryString["s_PharmacySurveys_CreatedSurveysName"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_PharmacySurveysLoadedSurveysName=" + Request.QueryString["s_PharmacySurveys_LoadedSurveysName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_PharmacySurveysLoadedSurveysFY=" + Request.QueryString["s_PharmacySurveys_LoadedSurveysFY"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_PharmacySurveysCreatedSurveysName=" + Request.QueryString["s_PharmacySurveys_CreatedSurveysName"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4;
      string FinalURL = "";
      if (!string.IsNullOrEmpty(SearchURL))
      {
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        FinalURL = LinkURL.Replace("'>View</a>", "&" + SearchURL + "'>View</a>");
        FinalURL = FinalURL.Replace("'>Update</a>", "&" + SearchURL + "'>Update</a>");
      }
      else
      {
        FinalURL = LinkURL;
      }

      return FinalURL;
    }
    //---END--- --List--//
  }
}