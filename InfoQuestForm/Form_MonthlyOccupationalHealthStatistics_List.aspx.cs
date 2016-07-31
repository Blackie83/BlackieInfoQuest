using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_MonthlyOccupationalHealthStatistics_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("46").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("46").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("46").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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
              SqlDataSource_MOHS_Unit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
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

          if (string.IsNullOrEmpty(DropDownList_Period.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_MOHS_Period"] == null)
            {
              DropDownList_Period.SelectedValue = "";
            }
            else
            {
              DropDownList_Period.SelectedValue = Request.QueryString["s_MOHS_Period"];
            }
          }

          if (string.IsNullOrEmpty(DropDownList_FYPeriod.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_MOHS_FYPeriod"] == null)
            {
              DropDownList_FYPeriod.SelectedValue = "";
            }
            else
            {
              DropDownList_FYPeriod.SelectedValue = Request.QueryString["s_MOHS_FYPeriod"];
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('46'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("46");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_MOHS_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Monthly Occupational Health Statistics", "22");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_MOHS_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MOHS_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_MOHS_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MOHS_Facility.SelectParameters.Clear();
      SqlDataSource_MOHS_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_MOHS_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "46");
      SqlDataSource_MOHS_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_MOHS_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_MOHS_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "Form_MonthlyOccupationalHealthStatistics");
      SqlDataSource_MOHS_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_MOHS_Unit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MOHS_Unit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_MOHS_Unit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MOHS_Unit.SelectParameters.Clear();
      SqlDataSource_MOHS_Unit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_MOHS_Unit.SelectParameters.Add("Form_Id", TypeCode.String, "46");
      SqlDataSource_MOHS_Unit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_MOHS_Unit.SelectParameters.Add("TableSELECT", TypeCode.String, "Unit_Id");
      SqlDataSource_MOHS_Unit.SelectParameters.Add("TableFROM", TypeCode.String, "Form_MonthlyOccupationalHealthStatistics");
      SqlDataSource_MOHS_Unit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_MOHS_Period.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MOHS_Period.SelectCommand = "SELECT DISTINCT MOHS_Period FROM Form_MonthlyOccupationalHealthStatistics ORDER BY MOHS_Period DESC";
      SqlDataSource_MOHS_Period.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_MOHS_FYPeriod.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MOHS_FYPeriod.SelectCommand = "SELECT DISTINCT MOHS_FYPeriod FROM Form_MonthlyOccupationalHealthStatistics ORDER BY MOHS_FYPeriod DESC";
      SqlDataSource_MOHS_FYPeriod.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_MOHS_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MOHS_List.SelectCommand = "spForm_Get_MonthlyOccupationalHealthStatistics_List";
      SqlDataSource_MOHS_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MOHS_List.CancelSelectOnNullParameter = false;
      SqlDataSource_MOHS_List.SelectParameters.Clear();
      SqlDataSource_MOHS_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_MOHS_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_MOHS_List.SelectParameters.Add("UnitId", TypeCode.String, Request.QueryString["s_Unit_Id"]);
      SqlDataSource_MOHS_List.SelectParameters.Add("Period", TypeCode.String, Request.QueryString["s_MOHS_Period"]);
      SqlDataSource_MOHS_List.SelectParameters.Add("FYPeriod", TypeCode.String, Request.QueryString["s_MOHS_FYPeriod"]);
    }


    //--START-- --Search--//
    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_Unit.Items.Clear();
      SqlDataSource_MOHS_Unit.SelectParameters["Facility_Id"].DefaultValue = DropDownList_Facility.SelectedValue;
      DropDownList_Unit.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_Unit.DataBind();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics List", "Form_MonthlyOccupationalHealthStatistics_List.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = DropDownList_Unit.SelectedValue;
      string SearchField3 = DropDownList_Period.SelectedValue;
      string SearchField4 = DropDownList_FYPeriod.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1) && string.IsNullOrEmpty(SearchField2) && string.IsNullOrEmpty(SearchField3) && string.IsNullOrEmpty(SearchField4))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics List", "Form_MonthlyOccupationalHealthStatistics_List.aspx"), false);
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
          SearchField3 = "s_MOHS_Period=" + DropDownList_Period.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_MOHS_FYPeriod=" + DropDownList_FYPeriod.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_MonthlyOccupationalHealthStatistics_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics List", FinalURL);
        Response.Redirect(FinalURL, false);
      }
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_MOHS_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_MOHS_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_MOHS_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_MOHS_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_MOHS_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_MOHS_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_MOHS_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_MOHS_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_MOHS_List.PageSize > 20 && GridView_MOHS_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_MOHS_List.PageSize > 50 && GridView_MOHS_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_MOHS_List.Rows.Count; i++)
      {
        if (GridView_MOHS_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_MOHS_List.Rows[i].Cells[5].Text == "No")
          {
            GridView_MOHS_List.Rows[i].Cells[5].BackColor = Color.FromName("#77cf9c");
            GridView_MOHS_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_MOHS_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_MOHS_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }

          if (GridView_MOHS_List.Rows[i].Cells[8].Text == "No")
          {
            GridView_MOHS_List.Rows[i].Cells[8].BackColor = Color.FromName("#77cf9c");
            GridView_MOHS_List.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_MOHS_List.Rows[i].Cells[8].BackColor = Color.FromName("#d46e6e");
            GridView_MOHS_List.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");

            GridView_MOHS_List.Rows[i].Cells[9].BackColor = Color.FromName("#d46e6e");
            GridView_MOHS_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");

            GridView_MOHS_List.Rows[i].Cells[10].BackColor = Color.FromName("#d46e6e");
            GridView_MOHS_List.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_MOHS_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_MOHS_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_MOHS_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_MOHS_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_MOHS_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object mohs_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "" +
          "<a href='Form_MonthlyOccupationalHealthStatistics.aspx?MOHS_Id=" + mohs_Id + "&ViewMode=0'>View</a>&nbsp;/&nbsp;" +
          "<a href='Form_MonthlyOccupationalHealthStatistics.aspx?MOHS_Id=" + mohs_Id + "&ViewMode=1'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "" +
          "<a href='Form_MonthlyOccupationalHealthStatistics.aspx?MOHS_Id=" + mohs_Id + "&ViewMode=0'>View</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_Unit_Id"];
      string SearchField3 = Request.QueryString["s_MOHS_Period"];
      string SearchField4 = Request.QueryString["s_MOHS_FYPeriod"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null && SearchField4 == null)
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
          SearchField3 = "Search_MOHSPeriod=" + Request.QueryString["s_MOHS_Period"] + "&";
        }

        if (SearchField4 == null)
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "Search_MOHSFYPeriod=" + Request.QueryString["s_MOHS_FYPeriod"] + "&";
        }

        string SearchURL = "";
        SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = CurrentURL;

        FinalURL = FinalURL.Replace("'>View</a>", "&" + SearchURL + "'>View</a>");
        FinalURL = FinalURL.Replace("'>Update</a>", "&" + SearchURL + "'>Update</a>");
      }

      return FinalURL;
    }
    //---END--- --List--//
  }
}