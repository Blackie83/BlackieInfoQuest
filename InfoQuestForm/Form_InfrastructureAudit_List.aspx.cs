using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_InfrastructureAudit_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("43").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("43").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("43").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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

          if (string.IsNullOrEmpty(DropDownList_Completed.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_InfrastructureAudit_Completed"] == null)
            {
              DropDownList_Completed.SelectedValue = "";
            }
            else
            {
              DropDownList_Completed.SelectedValue = Request.QueryString["s_InfrastructureAudit_Completed"];
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('43'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("43");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_InfrastructureAudit_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infrastructure Audit", "17");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_InfrastructureAudit_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_InfrastructureAudit_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_InfrastructureAudit_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_InfrastructureAudit_Facility.SelectParameters.Clear();
      SqlDataSource_InfrastructureAudit_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_InfrastructureAudit_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "43");
      SqlDataSource_InfrastructureAudit_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_InfrastructureAudit_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_InfrastructureAudit_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_InfrastructureAudit_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_InfrastructureAudit_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_InfrastructureAudit_List.SelectCommand = "spForm_Get_InfrastructureAudit_List";
      SqlDataSource_InfrastructureAudit_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_InfrastructureAudit_List.CancelSelectOnNullParameter = false;
      SqlDataSource_InfrastructureAudit_List.SelectParameters.Clear();
      SqlDataSource_InfrastructureAudit_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_InfrastructureAudit_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_InfrastructureAudit_List.SelectParameters.Add("Completed", TypeCode.String, Request.QueryString["s_InfrastructureAudit_Completed"]);
    }


    //--START-- --Search--//
    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_InfrastructureAudit_List.aspx";
      Response.Redirect(FinalURL, false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = DropDownList_Completed.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1) && string.IsNullOrEmpty(SearchField2))
      {
        string FinalURL = "";
        FinalURL = "Form_InfrastructureAudit_List.aspx";
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
          SearchField2 = "s_InfrastructureAudit_Completed=" + DropDownList_Completed.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_InfrastructureAudit_List.aspx?" + SearchField1 + SearchField2;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        Response.Redirect(FinalURL, false);
      }
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_InfrastructureAudit_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_InfrastructureAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_InfrastructureAudit_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_InfrastructureAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_InfrastructureAudit_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_InfrastructureAudit_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_InfrastructureAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_InfrastructureAudit_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_InfrastructureAudit_List.PageSize > 20 && GridView_InfrastructureAudit_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_InfrastructureAudit_List.PageSize > 50 && GridView_InfrastructureAudit_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_InfrastructureAudit_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_InfrastructureAudit_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_InfrastructureAudit_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_InfrastructureAudit_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_InfrastructureAudit_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object infrastructureAudit_Id, object infrastructureAudit_IsActive)
    {
      string LinkURL = "";
      if (infrastructureAudit_IsActive != null)
      {
        if (infrastructureAudit_IsActive.ToString() == "Yes")
        {
          LinkURL = "" +
          "<a href='Form_InfrastructureAudit_Summary.aspx?InfrastructureAudit_Id=" + infrastructureAudit_Id + "'>Summary</a>&nbsp;/&nbsp;" +
          "<a href='Form_InfrastructureAudit.aspx?InfrastructureAudit_Id=" + infrastructureAudit_Id + "'>Completion</a>";
        }
        else if (infrastructureAudit_IsActive.ToString() == "No")
        {
          LinkURL = "" +
          "<a href='Form_InfrastructureAudit.aspx?InfrastructureAudit_Id=" + infrastructureAudit_Id + "'>Completion</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_InfrastructureAudit_Completed"];

      if (SearchField1 == null && SearchField2 == null)
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
          SearchField2 = "Search_InfrastructureAuditCompleted=" + Request.QueryString["s_InfrastructureAudit_Completed"] + "&";
        }

        string SearchURL = "";
        SearchURL = SearchField1 + SearchField2;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = CurrentURL;

        FinalURL = FinalURL.Replace("'>Summary</a>", "&" + SearchURL + "'>Summary</a>");
        FinalURL = FinalURL.Replace("'>Completion</a>", "&" + SearchURL + "'>Completion</a>");
      }

      return FinalURL;
    }
    //---END--- --List--//
  }
}