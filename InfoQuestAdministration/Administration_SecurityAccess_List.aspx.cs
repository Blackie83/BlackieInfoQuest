using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SecurityAccess_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_Facility_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Form_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_SecurityRole_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_SecurityAccess_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_Form_Id.SelectParameters["Facility_Id"].DefaultValue = "0";
          SqlDataSource_SecurityRole_Id.SelectParameters["Form_Id"].DefaultValue = "";

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
        SecurityAllow = "0";
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
        Response.End();
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("0");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SecurityAccess_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(TextBox_SecurityUserUserName.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SecurityUserUserName"]))
        {
          TextBox_SecurityUserUserName.Text = "";
        }
        else
        {
          TextBox_SecurityUserUserName.Text = Request.QueryString["s_SecurityUserUserName"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_SecurityUserDisplayName.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SecurityUserDisplayName"]))
        {
          TextBox_SecurityUserDisplayName.Text = "";
        }
        else
        {
          TextBox_SecurityUserDisplayName.Text = Request.QueryString["s_SecurityUserDisplayName"];
        }
      }


      if (string.IsNullOrEmpty(DropDownList_FacilityId.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Facility_Id"]))
        {
          DropDownList_FacilityId.SelectedValue = "";

          DropDownList_FormId.Items.Clear();
          SqlDataSource_Form_Id.SelectParameters["Facility_Id"].DefaultValue = "0";
          DropDownList_FormId.Items.Insert(0, new ListItem(Convert.ToString("Select Form", CultureInfo.CurrentCulture), ""));
          DropDownList_FormId.DataBind();

          DropDownList_SecurityRoleId.Items.Clear();
          SqlDataSource_SecurityRole_Id.SelectParameters["Form_Id"].DefaultValue = "";
          DropDownList_SecurityRoleId.Items.Insert(0, new ListItem(Convert.ToString("Select Role", CultureInfo.CurrentCulture), ""));
          DropDownList_SecurityRoleId.DataBind();
        }
        else
        {
          DropDownList_FacilityId.SelectedValue = Request.QueryString["s_Facility_Id"];

          DropDownList_FormId.Items.Clear();
          SqlDataSource_Form_Id.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
          DropDownList_FormId.Items.Insert(0, new ListItem(Convert.ToString("Select Form", CultureInfo.CurrentCulture), ""));
          if (DropDownList_FacilityId.SelectedValue == "0")
          {
            DropDownList_FormId.Items.Insert(1, new ListItem(Convert.ToString("InfoQuest", CultureInfo.CurrentCulture), "-1"));
          }
          DropDownList_FormId.DataBind();

          DropDownList_SecurityRoleId.Items.Clear();
          SqlDataSource_SecurityRole_Id.SelectParameters["Form_Id"].DefaultValue = "";
          DropDownList_SecurityRoleId.Items.Insert(0, new ListItem(Convert.ToString("Select Role", CultureInfo.CurrentCulture), ""));
          DropDownList_SecurityRoleId.DataBind();
        }
      }

      if (string.IsNullOrEmpty(DropDownList_FormId.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Form_Id"]))
        {
          DropDownList_FormId.SelectedValue = "";

          DropDownList_SecurityRoleId.Items.Clear();
          SqlDataSource_SecurityRole_Id.SelectParameters["Form_Id"].DefaultValue = "";
          DropDownList_SecurityRoleId.Items.Insert(0, new ListItem(Convert.ToString("Select Role", CultureInfo.CurrentCulture), ""));
          DropDownList_SecurityRoleId.DataBind();
        }
        else
        {
          DropDownList_FormId.SelectedValue = Request.QueryString["s_Form_Id"];

          DropDownList_SecurityRoleId.Items.Clear();
          SqlDataSource_SecurityRole_Id.SelectParameters["Form_Id"].DefaultValue = Request.QueryString["s_Form_Id"];
          DropDownList_SecurityRoleId.Items.Insert(0, new ListItem(Convert.ToString("Select Role", CultureInfo.CurrentCulture), ""));
          DropDownList_SecurityRoleId.DataBind();
        }
      }

      if (string.IsNullOrEmpty(DropDownList_SecurityRoleId.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SecurityRole_Id"]))
        {
          DropDownList_SecurityRoleId.SelectedValue = "";
        }
        else
        {
          DropDownList_SecurityRoleId.SelectedValue = Request.QueryString["s_SecurityRole_Id"];
        }
      }
    }


    //--START-- --Search--//
    protected void DropDownList_FacilityId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_FormId.Items.Clear();
      SqlDataSource_Form_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      if (string.IsNullOrEmpty(DropDownList_FacilityId.SelectedValue))
      {
        SqlDataSource_Form_Id.SelectParameters["Facility_Id"].DefaultValue = "0";
      }
      else
      {
        SqlDataSource_Form_Id.SelectParameters["Facility_Id"].DefaultValue = DropDownList_FacilityId.SelectedValue.ToString();
      }

      DropDownList_FormId.Items.Insert(0, new ListItem(Convert.ToString("Select Form", CultureInfo.CurrentCulture), ""));
      if (DropDownList_FacilityId.SelectedValue == "0")
      {
        DropDownList_FormId.Items.Insert(1, new ListItem(Convert.ToString("InfoQuest", CultureInfo.CurrentCulture), "-1"));
      }
      DropDownList_FormId.DataBind();

      DropDownList_SecurityRoleId.Items.Clear();
      SqlDataSource_SecurityRole_Id.SelectParameters["Form_Id"].DefaultValue = "";
      DropDownList_SecurityRoleId.Items.Insert(0, new ListItem(Convert.ToString("Select Role", CultureInfo.CurrentCulture), ""));
      DropDownList_SecurityRoleId.DataBind();
    }

    protected void DropDownList_FormId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_SecurityRoleId.Items.Clear();
      SqlDataSource_SecurityRole_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      if (string.IsNullOrEmpty(DropDownList_FormId.SelectedValue))
      {
        SqlDataSource_SecurityRole_Id.SelectParameters["Form_Id"].DefaultValue = "";
      }
      else
      {
        SqlDataSource_SecurityRole_Id.SelectParameters["Form_Id"].DefaultValue = DropDownList_FormId.SelectedValue.ToString();
      }

      DropDownList_SecurityRoleId.Items.Insert(0, new ListItem(Convert.ToString("Select Role", CultureInfo.CurrentCulture), ""));
      DropDownList_SecurityRoleId.DataBind();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = Server.HtmlEncode(TextBox_SecurityUserUserName.Text);
      string SearchField2 = Server.HtmlEncode(TextBox_SecurityUserDisplayName.Text);
      string SearchField3 = DropDownList_FacilityId.SelectedValue;
      string SearchField4 = DropDownList_FormId.SelectedValue;
      string SearchField5 = DropDownList_SecurityRoleId.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SecurityUserUserName=" + Server.HtmlEncode(TextBox_SecurityUserUserName.Text).ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SecurityUserDisplayName=" + Server.HtmlEncode(TextBox_SecurityUserDisplayName.Text).ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Facility_Id=" + DropDownList_FacilityId.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_Form_Id=" + DropDownList_FormId.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_SecurityRole_Id=" + DropDownList_SecurityRoleId.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Administration_SecurityAccess_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security Access List", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security Access List", "Administration_SecurityAccess_List.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_SecurityAccess_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_SecurityAccess_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_SecurityAccess_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_SecurityAccess_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_SecurityAccess_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_SecurityAccess_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_SecurityAccess_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_SecurityAccess_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_SecurityAccess_List.PageSize > 20 && GridView_SecurityAccess_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_SecurityAccess_List.PageSize > 50 && GridView_SecurityAccess_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_SecurityAccess_List_DataBound(object sender, EventArgs e)
    {

      GridViewRow GridViewRow_List = GridView_SecurityAccess_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_SecurityAccess_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_SecurityAccess_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }

    }

    protected void GridView_SecurityAccess_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_Update_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security Access", "Administration_SecurityAccess.aspx"), false);
    }

    public string GetLink(object securityUser_Id, object securityUser_UserName)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security Access", "Administration_SecurityAccess.aspx?SecurityUser_Id=" + securityUser_Id + "&s_SecurityAccess_UserName=" + securityUser_UserName + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_SecurityUserUserName"];
      string SearchField2 = Request.QueryString["s_SecurityUserDisplayName"];
      string SearchField3 = Request.QueryString["s_Facility_Id"];
      string SearchField4 = Request.QueryString["s_Form_Id"];
      string SearchField5 = Request.QueryString["s_SecurityRole_Id"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_SecurityUserUserName=" + Request.QueryString["s_SecurityUserUserName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_SecurityUserDisplayName=" + Request.QueryString["s_SecurityUserDisplayName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_FormId=" + Request.QueryString["s_Form_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "Search_SecurityRoleId=" + Request.QueryString["s_SecurityRole_Id"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      string FinalURL = "";
      if (!string.IsNullOrEmpty(SearchURL))
      {
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        FinalURL = LinkURL.Replace("'>Update</a>", "&" + SearchURL + "'>Update</a>");
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