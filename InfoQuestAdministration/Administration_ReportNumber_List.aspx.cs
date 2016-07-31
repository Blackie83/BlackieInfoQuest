using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_ReportNumber_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_Facility_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Form_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ReportNumber_FinancialYear.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_ReportNumber_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SetFormQueryString();

          if (string.IsNullOrEmpty(Request.QueryString["s_Facility_Id"]) && string.IsNullOrEmpty(Request.QueryString["s_Form_Id"]) && string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_FinancialYear"]) && string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_GeneratedNumber"]) && string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_GeneratedBy"]) && string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_GeneratedDateFrom"]) && string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_GeneratedDateTo"]) && string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_IsActive"]))
          {
            TableSearch.Visible = false;
          }
          else
          {
            TableSearch.Visible = true;
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_ReportNumber_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_FacilityId.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Facility_Id"]))
        {
          DropDownList_FacilityId.SelectedValue = "";
        }
        else
        {
          DropDownList_FacilityId.SelectedValue = Request.QueryString["s_Facility_Id"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_FormId.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Form_Id"]))
        {
          DropDownList_FormId.SelectedValue = "";
        }
        else
        {
          DropDownList_FormId.SelectedValue = Request.QueryString["s_Form_Id"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_FinancialYear.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_FinancialYear"]))
        {
          DropDownList_FinancialYear.SelectedValue = "";
        }
        else
        {
          DropDownList_FinancialYear.SelectedValue = Request.QueryString["s_ReportNumber_FinancialYear"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_GeneratedNumber.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_GeneratedNumber"]))
        {
          TextBox_GeneratedNumber.Text = "";
        }
        else
        {
          TextBox_GeneratedNumber.Text = Request.QueryString["s_ReportNumber_GeneratedNumber"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_GeneratedBy.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_GeneratedBy"]))
        {
          TextBox_GeneratedBy.Text = "";
        }
        else
        {
          TextBox_GeneratedBy.Text = Request.QueryString["s_ReportNumber_GeneratedBy"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_GeneratedDateFrom.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_GeneratedDateFrom"]))
        {
          TextBox_GeneratedDateFrom.Text = "";
        }
        else
        {
          TextBox_GeneratedDateFrom.Text = Request.QueryString["s_ReportNumber_GeneratedDateFrom"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_GeneratedDateTo.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_GeneratedDateTo"]))
        {
          TextBox_GeneratedDateTo.Text = "";
        }
        else
        {
          TextBox_GeneratedDateTo.Text = Request.QueryString["s_ReportNumber_GeneratedDateTo"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_IsActive.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ReportNumber_IsActive"]))
        {
          DropDownList_IsActive.SelectedValue = "";
        }
        else
        {
          DropDownList_IsActive.SelectedValue = Request.QueryString["s_ReportNumber_IsActive"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchErrorMessage = "";
      string ValidSearch = "Yes";

      if (!string.IsNullOrEmpty(TextBox_GeneratedDateFrom.Text.ToString()))
      {
        string DateToValidate = TextBox_GeneratedDateFrom.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          SearchErrorMessage = SearchErrorMessage + Convert.ToString("Generated Date From is not in the correct format,<br />date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
          ValidSearch = "No";
        }
      }

      if (!string.IsNullOrEmpty(TextBox_GeneratedDateTo.Text.ToString()))
      {
        string DateToValidate = TextBox_GeneratedDateTo.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          SearchErrorMessage = SearchErrorMessage + Convert.ToString("Generated Date To is not in the correct format,<br />date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
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

        string SearchField1 = DropDownList_FacilityId.SelectedValue;
        string SearchField2 = DropDownList_FormId.SelectedValue;
        string SearchField3 = DropDownList_FinancialYear.SelectedValue;
        string SearchField4 = Server.HtmlEncode(TextBox_GeneratedNumber.Text);
        string SearchField5 = Server.HtmlEncode(TextBox_GeneratedBy.Text);
        string SearchField6 = Server.HtmlEncode(TextBox_GeneratedDateFrom.Text);
        string SearchField7 = Server.HtmlEncode(TextBox_GeneratedDateTo.Text);
        string SearchField8 = DropDownList_IsActive.SelectedValue;

        if (!string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "s_Facility_Id=" + DropDownList_FacilityId.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "s_Form_Id=" + DropDownList_FormId.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "s_ReportNumber_FinancialYear=" + DropDownList_FinancialYear.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "s_ReportNumber_GeneratedNumber=" + Server.HtmlEncode(TextBox_GeneratedNumber.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "s_ReportNumber_GeneratedBy=" + Server.HtmlEncode(TextBox_GeneratedBy.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField6))
        {
          SearchField6 = "s_ReportNumber_GeneratedDateFrom=" + Server.HtmlEncode(TextBox_GeneratedDateFrom.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField7))
        {
          SearchField7 = "s_ReportNumber_GeneratedDateTo=" + Server.HtmlEncode(TextBox_GeneratedDateTo.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField8))
        {
          SearchField8 = "s_ReportNumber_IsActive=" + DropDownList_IsActive.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Administration_ReportNumber_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7 + SearchField8;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number List", "Administration_ReportNumber_List.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_ReportNumber_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_ReportNumber_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_ReportNumber_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_ReportNumber_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_ReportNumber_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_ReportNumber_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_ReportNumber_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_ReportNumber_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_ReportNumber_List.PageSize > 20 && GridView_ReportNumber_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_ReportNumber_List.PageSize > 50 && GridView_ReportNumber_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_ReportNumber_List.Rows.Count; i++)
      {
        if (GridView_ReportNumber_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_ReportNumber_List.Rows[i].Cells[7].Text.ToString() == "Yes")
          {
            GridView_ReportNumber_List.Rows[i].Cells[7].BackColor = Color.FromName("#77cf9c");
            GridView_ReportNumber_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_ReportNumber_List.Rows[i].Cells[7].Text.ToString() == "No")
          {
            GridView_ReportNumber_List.Rows[i].Cells[7].BackColor = Color.FromName("#d46e6e");
            GridView_ReportNumber_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_ReportNumber_List.Rows[i].Cells[7].BackColor = Color.FromName("#d46e6e");
            GridView_ReportNumber_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_ReportNumber_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_ReportNumber_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_ReportNumber_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_ReportNumber_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_ReportNumber_List_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --List--//
  }
}