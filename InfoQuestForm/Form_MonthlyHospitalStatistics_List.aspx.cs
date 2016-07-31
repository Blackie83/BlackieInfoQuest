using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_MonthlyHospitalStatistics_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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

          if (string.IsNullOrEmpty(DropDownList_Period.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_MHS_Period"] == null)
            {
              DropDownList_Period.SelectedValue = "";
            }
            else
            {
              DropDownList_Period.SelectedValue = Request.QueryString["s_MHS_Period"];
            }
          }

          if (string.IsNullOrEmpty(DropDownList_FYPeriod.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_MHS_FYPeriod"] == null)
            {
              DropDownList_FYPeriod.SelectedValue = "";
            }
            else
            {
              DropDownList_FYPeriod.SelectedValue = Request.QueryString["s_MHS_FYPeriod"];
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('5'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("5");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_MHS_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Monthly Hospital Statistics", "19");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_MHS_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHS_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_MHS_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MHS_Facility.SelectParameters.Clear();
      SqlDataSource_MHS_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_MHS_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "5");
      SqlDataSource_MHS_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_MHS_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_MHS_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "InfoQuest_Form_MonthlyHospitalStatistics");
      SqlDataSource_MHS_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_MHS_Period.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHS_Period.SelectCommand = "SELECT DISTINCT MHS_Period FROM InfoQuest_Form_MonthlyHospitalStatistics ORDER BY MHS_Period DESC";
      SqlDataSource_MHS_Period.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_MHS_FYPeriod.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHS_FYPeriod.SelectCommand = "SELECT DISTINCT MHS_FYPeriod FROM InfoQuest_Form_MonthlyHospitalStatistics ORDER BY MHS_FYPeriod DESC";
      SqlDataSource_MHS_FYPeriod.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_MHS_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHS_List.SelectCommand = "spForm_Get_MonthlyHospitalStatistics_List";
      SqlDataSource_MHS_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MHS_List.CancelSelectOnNullParameter = false;
      SqlDataSource_MHS_List.SelectParameters.Clear();
      SqlDataSource_MHS_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_MHS_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_MHS_List.SelectParameters.Add("Period", TypeCode.String, Request.QueryString["s_MHS_Period"]);
      SqlDataSource_MHS_List.SelectParameters.Add("FYPeriod", TypeCode.String, Request.QueryString["s_MHS_FYPeriod"]);
    }


    //--START-- --Search--//
    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Hospital Statistics List", "Form_MonthlyHospitalStatistics_List.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = DropDownList_Period.SelectedValue;
      string SearchField3 = DropDownList_FYPeriod.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1) && string.IsNullOrEmpty(SearchField2) && string.IsNullOrEmpty(SearchField3))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Hospital Statistics List", "Form_MonthlyHospitalStatistics_List.aspx"), false);
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
          SearchField2 = "s_MHS_Period=" + DropDownList_Period.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_MHS_FYPeriod=" + DropDownList_FYPeriod.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_MonthlyHospitalStatistics_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Hospital Statistics List", FinalURL);
        Response.Redirect(FinalURL, false);
      }
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_MHS_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_MHS_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_MHS_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_MHS_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_MHS_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_MHS_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_MHS_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_MHS_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_MHS_List.PageSize > 20 && GridView_MHS_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_MHS_List.PageSize > 50 && GridView_MHS_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_MHS_List.Rows.Count; i++)
      {
        if (GridView_MHS_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_MHS_List.Rows[i].Cells[4].Text == "No")
          {
            GridView_MHS_List.Rows[i].Cells[4].BackColor = Color.FromName("#77cf9c");
            GridView_MHS_List.Rows[i].Cells[4].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_MHS_List.Rows[i].Cells[4].BackColor = Color.FromName("#d46e6e");
            GridView_MHS_List.Rows[i].Cells[4].ForeColor = Color.FromName("#333333");
          }

          if (GridView_MHS_List.Rows[i].Cells[7].Text == "No")
          {
            GridView_MHS_List.Rows[i].Cells[7].BackColor = Color.FromName("#77cf9c");
            GridView_MHS_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_MHS_List.Rows[i].Cells[7].BackColor = Color.FromName("#d46e6e");
            GridView_MHS_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");

            GridView_MHS_List.Rows[i].Cells[8].BackColor = Color.FromName("#d46e6e");
            GridView_MHS_List.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");

            GridView_MHS_List.Rows[i].Cells[9].BackColor = Color.FromName("#d46e6e");
            GridView_MHS_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_MHS_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_MHS_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_MHS_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_MHS_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_MHS_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object mhs_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "" +
          "<a href='Form_MonthlyHospitalStatistics.aspx?MHS_Id=" + mhs_Id + "&ViewMode=0'>View</a>&nbsp;/&nbsp;" +
          "<a href='Form_MonthlyHospitalStatistics.aspx?MHS_Id=" + mhs_Id + "&ViewMode=1'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "" +
          "<a href='Form_MonthlyHospitalStatistics.aspx?MHS_Id=" + mhs_Id + "&ViewMode=0'>View</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_MHS_Period"];
      string SearchField3 = Request.QueryString["s_MHS_FYPeriod"];

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
          SearchField2 = "Search_MHSPeriod=" + Request.QueryString["s_MHS_Period"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "Search_MHSFYPeriod=" + Request.QueryString["s_MHS_FYPeriod"] + "&";
        }

        string SearchURL = "";
        SearchURL = SearchField1 + SearchField2 + SearchField3;
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