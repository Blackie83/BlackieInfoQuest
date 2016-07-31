﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SendEmail_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_SendEmail_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SetFormQueryString();

          if (string.IsNullOrEmpty(Request.QueryString["s_SendEmail_From"]) && string.IsNullOrEmpty(Request.QueryString["s_SendEmail_To"]) && string.IsNullOrEmpty(Request.QueryString["s_SendEmail_Subject"]) && string.IsNullOrEmpty(Request.QueryString["s_SendEmail_Body"]) && string.IsNullOrEmpty(Request.QueryString["s_SendEmail_DateFrom"]) && string.IsNullOrEmpty(Request.QueryString["s_SendEmail_DateTo"]))
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SendEmail_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(TextBox_From.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SendEmail_From"]))
        {
          TextBox_From.Text = "";
        }
        else
        {
          TextBox_From.Text = Request.QueryString["s_SendEmail_From"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_To.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SendEmail_To"]))
        {
          TextBox_To.Text = "";
        }
        else
        {
          TextBox_To.Text = Request.QueryString["s_SendEmail_To"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_Subject.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SendEmail_Subject"]))
        {
          TextBox_Subject.Text = "";
        }
        else
        {
          TextBox_Subject.Text = Request.QueryString["s_SendEmail_Subject"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_Body.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SendEmail_Body"]))
        {
          TextBox_Body.Text = "";
        }
        else
        {
          TextBox_Body.Text = Request.QueryString["s_SendEmail_Body"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_DateFrom.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SendEmail_DateFrom"]))
        {
          TextBox_DateFrom.Text = "";
        }
        else
        {
          TextBox_DateFrom.Text = Request.QueryString["s_SendEmail_DateFrom"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_DateTo.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_SendEmail_DateTo"]))
        {
          TextBox_DateTo.Text = "";
        }
        else
        {
          TextBox_DateTo.Text = Request.QueryString["s_SendEmail_DateTo"];
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

        string SearchField1 = Server.HtmlEncode(TextBox_From.Text);
        string SearchField2 = Server.HtmlEncode(TextBox_To.Text);
        string SearchField3 = Server.HtmlEncode(TextBox_Subject.Text);
        string SearchField4 = Server.HtmlEncode(TextBox_Body.Text);
        string SearchField5 = Server.HtmlEncode(TextBox_DateFrom.Text);
        string SearchField6 = Server.HtmlEncode(TextBox_DateTo.Text);

        if (!string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "s_SendEmail_From=" + Server.HtmlEncode(TextBox_From.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "s_SendEmail_To=" + Server.HtmlEncode(TextBox_To.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "s_SendEmail_Subject=" + Server.HtmlEncode(TextBox_Subject.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "s_SendEmail_Body=" + Server.HtmlEncode(TextBox_Body.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "s_SendEmail_DateFrom=" + Server.HtmlEncode(TextBox_DateFrom.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField6))
        {
          SearchField6 = "s_SendEmail_DateTo=" + Server.HtmlEncode(TextBox_DateTo.Text.ToString()) + "&";
        }

        string FinalURL = "Administration_SendEmail_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Send Email List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Send Email List", "Administration_SendEmail_List.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_SendEmail_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_SendEmail_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_SendEmail_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_SendEmail_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_SendEmail_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_SendEmail_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_SendEmail_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_SendEmail_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_SendEmail_List.PageSize > 20 && GridView_SendEmail_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_SendEmail_List.PageSize > 50 && GridView_SendEmail_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_SendEmail_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_SendEmail_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_SendEmail_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_SendEmail_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_SendEmail_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_EmailBody_Click(object sender, EventArgs e)
    {
      Button Button_EmailBody = (Button)sender;
      GridViewRow GridViewRow_SendEmail_List = (GridViewRow)Button_EmailBody.NamingContainer;
      HiddenField HiddenField_SendEmailId = (HiddenField)GridViewRow_SendEmail_List.FindControl("HiddenField_SendEmailId");

      ScriptManager.RegisterStartupScript(UpdatePanel_SendEmail_List, this.GetType(), "EmailBody", "EmailBody('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Send Email", "Administration_SendEmail.aspx?SendEmail_Id=" + HiddenField_SendEmailId.Value + "") + "')", true);
    }
    //---END--- --List--//
  }
}