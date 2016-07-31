using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_ListItem_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_Form_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListCategory_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_Parent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ListItem_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_ListCategory_Id.SelectParameters["Form_Id"].DefaultValue = "0";
          SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = "0";
          SqlDataSource_ListItem_Parent.SelectParameters["ListCategory_Id"].DefaultValue = "0";

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_ListItem_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_FormId.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Form_Id"]))
        {
          DropDownList_FormId.SelectedValue = "";

          DropDownList_ListCategoryId.Items.Clear();
          SqlDataSource_ListCategory_Id.SelectParameters["Form_Id"].DefaultValue = "0";
          DropDownList_ListCategoryId.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
          DropDownList_ListCategoryId.DataBind();

          DropDownList_Parent.Items.Clear();
          SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = "0";
          SqlDataSource_ListItem_Parent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
          DropDownList_Parent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
          DropDownList_Parent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
          DropDownList_Parent.DataBind();
        }
        else
        {
          DropDownList_FormId.SelectedValue = Request.QueryString["s_Form_Id"];

          DropDownList_ListCategoryId.Items.Clear();
          SqlDataSource_ListCategory_Id.SelectParameters["Form_Id"].DefaultValue = Request.QueryString["s_Form_Id"];
          DropDownList_ListCategoryId.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
          DropDownList_ListCategoryId.DataBind();

          DropDownList_Parent.Items.Clear();
          SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = Request.QueryString["s_Form_Id"];
          SqlDataSource_ListItem_Parent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
          DropDownList_Parent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
          DropDownList_Parent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
          DropDownList_Parent.DataBind();
        }
      }

      if (string.IsNullOrEmpty(DropDownList_ListCategoryId.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ListCategory_Id"]))
        {
          DropDownList_ListCategoryId.SelectedValue = "";

          DropDownList_Parent.Items.Clear();

          if (string.IsNullOrEmpty(Request.QueryString["s_Form_Id"]))
          {
            SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = "0";
          }
          else
          {
            SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = Request.QueryString["s_Form_Id"];
          }
          SqlDataSource_ListItem_Parent.SelectParameters["ListCategory_Id"].DefaultValue = "0";

          DropDownList_Parent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
          DropDownList_Parent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
          DropDownList_Parent.DataBind();
        }
        else
        {
          DropDownList_ListCategoryId.SelectedValue = Request.QueryString["s_ListCategory_Id"];

          DropDownList_Parent.Items.Clear();

          if (string.IsNullOrEmpty(Request.QueryString["s_Form_Id"]))
          {
            SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = "0";
          }
          else
          {
            SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = Request.QueryString["s_Form_Id"];
          }
          SqlDataSource_ListItem_Parent.SelectParameters["ListCategory_Id"].DefaultValue = Request.QueryString["s_ListCategory_Id"];

          DropDownList_Parent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
          DropDownList_Parent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
          DropDownList_Parent.DataBind();
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Parent.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ListItem_Parent"]))
        {
          DropDownList_Parent.SelectedValue = "";
        }
        else
        {
          DropDownList_Parent.SelectedValue = Request.QueryString["s_ListItem_Parent"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_Name.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ListItem_Name"]))
        {
          TextBox_Name.Text = "";
        }
        else
        {
          TextBox_Name.Text = Request.QueryString["s_ListItem_Name"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_IsActive.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ListItem_IsActive"]))
        {
          DropDownList_IsActive.SelectedValue = "";
        }
        else
        {
          DropDownList_IsActive.SelectedValue = Request.QueryString["s_ListItem_IsActive"];
        }
      }
    }


    //--START-- --Search--//
    protected void DropDownList_FormId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_ListCategoryId.Items.Clear();
      SqlDataSource_ListCategory_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      DropDownList_Parent.Items.Clear();
      SqlDataSource_ListItem_Parent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      if (string.IsNullOrEmpty(DropDownList_FormId.SelectedValue))
      {
        SqlDataSource_ListCategory_Id.SelectParameters["Form_Id"].DefaultValue = "0";
        SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = "0";
        SqlDataSource_ListItem_Parent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
      }
      else
      {
        SqlDataSource_ListCategory_Id.SelectParameters["Form_Id"].DefaultValue = DropDownList_FormId.SelectedValue.ToString();
        SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = DropDownList_FormId.SelectedValue.ToString();
        SqlDataSource_ListItem_Parent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
      }

      DropDownList_ListCategoryId.Items.Insert(0, new ListItem(Convert.ToString("Select Category", CultureInfo.CurrentCulture), ""));
      DropDownList_ListCategoryId.DataBind();

      DropDownList_Parent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
      DropDownList_Parent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
      DropDownList_Parent.DataBind();

      GridView_ListItem_List.DataBind();
    }

    protected void DropDownList_ListCategoryId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_Parent.Items.Clear();
      SqlDataSource_ListItem_Parent.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      if (string.IsNullOrEmpty(DropDownList_ListCategoryId.SelectedValue))
      {
        if (string.IsNullOrEmpty(DropDownList_FormId.SelectedValue))
        {
          SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = "0";
        }
        else
        {
          SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = DropDownList_FormId.SelectedValue.ToString();
        }
        SqlDataSource_ListItem_Parent.SelectParameters["ListCategory_Id"].DefaultValue = "0";
      }
      else
      {
        if (string.IsNullOrEmpty(DropDownList_FormId.SelectedValue))
        {
          SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = "0";
        }
        else
        {
          SqlDataSource_ListItem_Parent.SelectParameters["Form_Id"].DefaultValue = DropDownList_FormId.SelectedValue.ToString();
        }
        SqlDataSource_ListItem_Parent.SelectParameters["ListCategory_Id"].DefaultValue = DropDownList_ListCategoryId.SelectedValue.ToString();
      }

      DropDownList_Parent.Items.Insert(0, new ListItem(Convert.ToString("Select Parent", CultureInfo.CurrentCulture), ""));
      DropDownList_Parent.Items.Insert(1, new ListItem(Convert.ToString("No Parent", CultureInfo.CurrentCulture), "-1"));
      DropDownList_Parent.DataBind();

      GridView_ListItem_List.DataBind();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_FormId.SelectedValue;
      string SearchField2 = DropDownList_ListCategoryId.SelectedValue;
      string SearchField3 = DropDownList_Parent.SelectedValue;
      string SearchField4 = Server.HtmlEncode(TextBox_Name.Text);
      string SearchField5 = DropDownList_IsActive.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Form_Id=" + DropDownList_FormId.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_ListCategory_Id=" + DropDownList_ListCategoryId.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_ListItem_Parent=" + DropDownList_Parent.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_ListItem_Name=" + Server.HtmlEncode(TextBox_Name.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_ListItem_IsActive=" + DropDownList_IsActive.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Administration_ListItem_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("List Item List", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Administration_ListItem_List.aspx";
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_ListItem_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_ListItem_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_ListItem_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {

      DropDownList DropDownList_PageList = (DropDownList)GridView_ListItem_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_ListItem_List.PageIndex = DropDownList_PageList.SelectedIndex;

    }

    protected void GridView_ListItem_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_ListItem_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_ListItem_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_ListItem_List.PageSize > 20 && GridView_ListItem_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_ListItem_List.PageSize > 50 && GridView_ListItem_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_ListItem_List.Rows.Count; i++)
      {
        if (GridView_ListItem_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_ListItem_List.Rows[i].Cells[6].Text.ToString() == "Yes")
          {
            GridView_ListItem_List.Rows[i].Cells[6].BackColor = Color.FromName("#77cf9c");
            GridView_ListItem_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_ListItem_List.Rows[i].Cells[6].Text.ToString() == "No")
          {
            GridView_ListItem_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
            GridView_ListItem_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_ListItem_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
            GridView_ListItem_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_ListItem_List_DataBound(object sender, EventArgs e)
    {

      GridViewRow GridViewRow_List = GridView_ListItem_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_ListItem_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_ListItem_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }

    }

    protected void GridView_ListItem_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect("Administration_ListItem.aspx", false);
    }

    public string GetLink(object listItem_Id)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Unit", "Administration_ListItem.aspx?ListItem_Id=" + listItem_Id + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_Form_Id"];
      string SearchField2 = Request.QueryString["s_ListCategory_Id"];
      string SearchField3 = Request.QueryString["s_ListItem_Parent"];
      string SearchField4 = Request.QueryString["s_ListItem_Name"];
      string SearchField5 = Request.QueryString["s_ListItem_IsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_FormId=" + Request.QueryString["s_Form_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_ListCategoryId=" + Request.QueryString["s_ListCategory_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_ListItemParent=" + Request.QueryString["s_ListItem_Parent"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_ListItemName=" + Request.QueryString["s_ListItem_Name"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "Search_ListItemIsActive=" + Request.QueryString["s_ListItem_IsActive"] + "&";
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