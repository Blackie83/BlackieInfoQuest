using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_Exceptions_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_Exceptions_Page.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Exceptions_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Exceptions_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Page.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Exceptions_Page"]))
        {
          DropDownList_Page.SelectedValue = "";
        }
        else
        {
          DropDownList_Page.SelectedValue = Request.QueryString["s_Exceptions_Page"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_Other.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Exceptions_Other"]))
        {
          TextBox_Other.Text = "";
        }
        else
        {
          TextBox_Other.Text = Request.QueryString["s_Exceptions_Other"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Completed.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Exceptions_Completed"]))
        {
          DropDownList_Completed.SelectedValue = "";
        }
        else
        {
          DropDownList_Completed.SelectedValue = Request.QueryString["s_Exceptions_Completed"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_DateFrom.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Exceptions_DateFrom"]))
        {
          TextBox_DateFrom.Text = "";
        }
        else
        {
          TextBox_DateFrom.Text = Request.QueryString["s_Exceptions_DateFrom"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_DateTo.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Exceptions_DateTo"]))
        {
          TextBox_DateTo.Text = "";
        }
        else
        {
          TextBox_DateTo.Text = Request.QueryString["s_Exceptions_DateTo"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchErrorMessage = "";
      string ValidSearch = "Yes";

      if (!string.IsNullOrEmpty(TextBox_DateFrom.Text.ToString()))
      {
        string DateToValidate = TextBox_DateFrom.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          SearchErrorMessage = SearchErrorMessage + Convert.ToString("Date From is not in the correct format,<br />date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
          ValidSearch = "No";
        }
      }

      if (!string.IsNullOrEmpty(TextBox_DateTo.Text.ToString()))
      {
        string DateToValidate = TextBox_DateTo.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          SearchErrorMessage = SearchErrorMessage + Convert.ToString("Date To is not in the correct format,<br />date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
          ValidSearch = "No";
        }
      }

      if (ValidSearch == "No")
      {
        Label_SearchErrorMessage.Text = SearchErrorMessage;
      }
      else
      {
        Label_SearchErrorMessage.Text = "";

        string SearchField1 = DropDownList_Page.SelectedValue;
        string SearchField2 = Server.HtmlEncode(TextBox_Other.Text);
        string SearchField3 = DropDownList_Completed.SelectedValue;
        string SearchField4 = Server.HtmlEncode(TextBox_DateFrom.Text);
        string SearchField5 = Server.HtmlEncode(TextBox_DateTo.Text);

        if (!string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "s_Exceptions_Page=" + DropDownList_Page.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "s_Exceptions_Other=" + Server.HtmlEncode(TextBox_Other.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "s_Exceptions_Completed=" + DropDownList_Completed.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "s_Exceptions_DateFrom=" + Server.HtmlEncode(TextBox_DateFrom.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "s_Exceptions_DateTo=" + Server.HtmlEncode(TextBox_DateTo.Text.ToString()) + "&";
        }

        string FinalURL = "Administration_Exceptions_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Exceptions List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Exceptions List", "Administration_Exceptions_List.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_Exceptions_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_Exceptions_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_Exceptions_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {

      DropDownList DropDownList_PageList = (DropDownList)GridView_Exceptions_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_Exceptions_List.PageIndex = DropDownList_PageList.SelectedIndex;

    }

    protected void GridView_Exceptions_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Exceptions_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Exceptions_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Exceptions_List.PageSize > 20 && GridView_Exceptions_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Exceptions_List.PageSize > 50 && GridView_Exceptions_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_Exceptions_List.Rows.Count; i++)
      {
        if (GridView_Exceptions_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_Exceptions_List.Rows[i].Cells[6].Text.ToString() == "Yes")
          {
            GridView_Exceptions_List.Rows[i].Cells[6].BackColor = Color.FromName("#77cf9c");
            GridView_Exceptions_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_Exceptions_List.Rows[i].Cells[6].Text.ToString() == "No")
          {
            GridView_Exceptions_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
            GridView_Exceptions_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_Exceptions_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
            GridView_Exceptions_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_Exceptions_List_DataBound(object sender, EventArgs e)
    {

      GridViewRow GridViewRow_List = GridView_Exceptions_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Exceptions_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Exceptions_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }

    }

    protected void GridView_Exceptions_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object exceptions_Id)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Exceptions", "Administration_Exceptions.aspx?Exceptions_Id=" + exceptions_Id + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_Exceptions_Page"];
      string SearchField2 = Request.QueryString["s_Exceptions_Other"];
      string SearchField3 = Request.QueryString["s_Exceptions_Completed"];
      string SearchField4 = Request.QueryString["s_Exceptions_DateFrom"];
      string SearchField5 = Request.QueryString["s_Exceptions_DateTo"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_ExceptionsPage=" + Request.QueryString["s_Exceptions_Page"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_ExceptionsOther=" + Request.QueryString["s_Exceptions_Other"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_ExceptionsCompleted=" + Request.QueryString["s_Exceptions_Completed"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_ExceptionsDateFrom=" + Request.QueryString["s_Exceptions_DateFrom"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "Search_ExceptionsDateTo=" + Request.QueryString["s_Exceptions_DateTo"] + "&";
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