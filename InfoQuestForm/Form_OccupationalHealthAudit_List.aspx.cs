using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_OccupationalHealthAudit_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_Facility_Id"] == null)
            {
              DropDownList_Facility.SelectedValue = "";
            }
            else
            {
              DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];

              DropDownList_Unit.Items.Clear();
              SqlDataSource_OccupationalHealthAudit_Unit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
              DropDownList_Unit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
              DropDownList_Unit.DataBind();
            }
          }

          if (string.IsNullOrEmpty(DropDownList_Unit.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_Unit_Id"] == null)
            {
              DropDownList_Unit.SelectedValue = "";
            }
            else
            {
              DropDownList_Unit.SelectedValue = Request.QueryString["s_Unit_Id"];
            }
          }

          if (string.IsNullOrEmpty(DropDownList_Completed.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_OHA_Completed"] == null)
            {
              DropDownList_Completed.SelectedValue = "";
            }
            else
            {
              DropDownList_Completed.SelectedValue = Request.QueryString["s_OHA_Completed"];
            }
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('48'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("48");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_OccupationalHealthAudit_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Occupational Health Audit", "25");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_OccupationalHealthAudit_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_OccupationalHealthAudit_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_OccupationalHealthAudit_Facility.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_OccupationalHealthAudit_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "48");
      SqlDataSource_OccupationalHealthAudit_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_OccupationalHealthAudit_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_OccupationalHealthAudit_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "Form_OccupationalHealthAudit");
      SqlDataSource_OccupationalHealthAudit_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_OccupationalHealthAudit_Unit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Unit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_OccupationalHealthAudit_Unit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_OccupationalHealthAudit_Unit.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Unit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_OccupationalHealthAudit_Unit.SelectParameters.Add("Form_Id", TypeCode.String, "48");
      SqlDataSource_OccupationalHealthAudit_Unit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Unit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_OccupationalHealthAudit_Unit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_OccupationalHealthAudit_Unit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_OccupationalHealthAudit_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_List.SelectCommand = "spForm_Get_OccupationalHealthAudit_List";
      SqlDataSource_OccupationalHealthAudit_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_OccupationalHealthAudit_List.CancelSelectOnNullParameter = false;
      SqlDataSource_OccupationalHealthAudit_List.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_OccupationalHealthAudit_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_OccupationalHealthAudit_List.SelectParameters.Add("UnitId", TypeCode.String, Request.QueryString["s_Unit_Id"]);
      SqlDataSource_OccupationalHealthAudit_List.SelectParameters.Add("Completed", TypeCode.String, Request.QueryString["s_OHA_Completed"]);
    }


    //--START-- --Search--//
    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_Unit.Items.Clear();
      SqlDataSource_OccupationalHealthAudit_Unit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Unit.SelectParameters["Facility_Id"].DefaultValue = DropDownList_Facility.SelectedValue.ToString();
      DropDownList_Unit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_Unit.DataBind();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_OccupationalHealthAudit_List.aspx";
      Response.Redirect(FinalURL, false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = DropDownList_Unit.SelectedValue;
      string SearchField3 = DropDownList_Completed.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1) && string.IsNullOrEmpty(SearchField2) && string.IsNullOrEmpty(SearchField3))
      {
        string FinalURL = "";
        FinalURL = "Form_OccupationalHealthAudit_List.aspx";
        Response.Redirect(FinalURL, false);
      }
      else
      {
        if (string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_Unit_Id=" + DropDownList_Unit.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_OHA_Completed=" + DropDownList_Completed.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_OccupationalHealthAudit_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        Response.Redirect(FinalURL, false);
      }
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_OccupationalHealthAudit_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_OccupationalHealthAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_OccupationalHealthAudit_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_OccupationalHealthAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_OccupationalHealthAudit_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_OccupationalHealthAudit_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_OccupationalHealthAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_OccupationalHealthAudit_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_OccupationalHealthAudit_List.PageSize > 20 && GridView_OccupationalHealthAudit_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_OccupationalHealthAudit_List.PageSize > 50 && GridView_OccupationalHealthAudit_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_OccupationalHealthAudit_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_OccupationalHealthAudit_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_OccupationalHealthAudit_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_OccupationalHealthAudit_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_OccupationalHealthAudit_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object oha_Id, object oha_IsActive)
    {
      string LinkURL = "";
      if (oha_IsActive != null)
      {
        if (oha_IsActive.ToString() == "Yes")
        {
          LinkURL = "" +
          "<a href='Form_OccupationalHealthAudit_Summary.aspx?OHA_Id=" + oha_Id + "'>Summary</a>&nbsp;/&nbsp;" +
          "<a href='Form_OccupationalHealthAudit_Findings_List.aspx?OHA_Id=" + oha_Id + "'>Findings</a>&nbsp;/&nbsp;" +
          "<a href='Form_OccupationalHealthAudit.aspx?OHA_Id=" + oha_Id + "'>Completion</a>";
        }
        else if (oha_IsActive.ToString() == "No")
        {
          LinkURL = "" +
          "<a href='Form_OccupationalHealthAudit.aspx?OHA_Id=" + oha_Id + "'>Completion</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_Unit_Id"];
      string SearchField3 = Request.QueryString["s_OHA_Completed"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null)
      {
        FinalURL = CurrentURL;
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "Search_UnitId=" + Request.QueryString["s_Unit_Id"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "Search_OHACompleted=" + Request.QueryString["s_OHA_Completed"] + "&";
        }

        string SearchURL = "";
        SearchURL = SearchField1 + SearchField2 + SearchField3;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = CurrentURL;

        FinalURL = FinalURL.Replace("'>Summary</a>", "&" + SearchURL + "'>Summary</a>");
        FinalURL = FinalURL.Replace("'>Findings</a>", "&" + SearchURL + "'>Findings</a>");
        FinalURL = FinalURL.Replace("'>Completion</a>", "&" + SearchURL + "'>Completion</a>");
      }

      return FinalURL;
    }
    //---END--- --List--//
  }
}