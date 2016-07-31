using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SystemAccount_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_SystemAccount_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_SystemAccount_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SystemAccount_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Id.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SystemAccount_Id"]))
        {
          DropDownList_Id.SelectedValue = "";
        }
        else
        {
          DropDownList_Id.SelectedValue = Request.QueryString["s_SystemAccount_Id"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_IsActive.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SystemAccount_IsActive"]))
        {
          DropDownList_IsActive.SelectedValue = "";
        }
        else
        {
          DropDownList_IsActive.SelectedValue = Request.QueryString["s_SystemAccount_IsActive"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Id.SelectedValue;
      string SearchField2 = DropDownList_IsActive.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SystemAccount_Id=" + DropDownList_Id.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SystemAccount_IsActive=" + DropDownList_IsActive.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Administration_SystemAccount_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Account List", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Account List", "Administration_SystemAccount_List.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_SystemAccount_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_SystemAccount_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_SystemAccount_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_SystemAccount_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_SystemAccount_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_SystemAccount_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_SystemAccount_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_SystemAccount_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_SystemAccount_List.PageSize > 20 && GridView_SystemAccount_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_SystemAccount_List.PageSize > 50 && GridView_SystemAccount_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_SystemAccount_List.Rows.Count; i++)
      {
        if (GridView_SystemAccount_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_SystemAccount_List.Rows[i].Cells[3].Text.ToString() == "Yes")
          {
            GridView_SystemAccount_List.Rows[i].Cells[3].BackColor = Color.FromName("#77cf9c");
            GridView_SystemAccount_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_SystemAccount_List.Rows[i].Cells[3].Text.ToString() == "No")
          {
            GridView_SystemAccount_List.Rows[i].Cells[3].BackColor = Color.FromName("#d46e6e");
            GridView_SystemAccount_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_SystemAccount_List.Rows[i].Cells[3].BackColor = Color.FromName("#d46e6e");
            GridView_SystemAccount_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_SystemAccount_List_DataBound(object sender, EventArgs e)
    {

      GridViewRow GridViewRow_List = GridView_SystemAccount_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_SystemAccount_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_SystemAccount_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }

    }

    protected void GridView_SystemAccount_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Account", "Administration_SystemAccount.aspx"), false);
    }

    public string GetLink(object systemAccount_Id)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Account", "Administration_SystemAccount.aspx?SystemAccount_Id=" + systemAccount_Id + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_SystemAccount_Id"];
      string SearchField2 = Request.QueryString["s_SystemAccount_IsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_SystemAccountId=" + Request.QueryString["s_SystemAccount_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_SystemAccountIsActive=" + Request.QueryString["s_SystemAccount_IsActive"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2;
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