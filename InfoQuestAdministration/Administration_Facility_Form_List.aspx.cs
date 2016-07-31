﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_Facility_Form_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_Form_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Facility_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_Facility_Form_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_Facility_Id.SelectParameters["Form_Id"].DefaultValue = "0";

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Facility_Form_List.FindControl("Label_UpdateProgress");
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

          DropDownList_FacilityId.Items.Clear();
          SqlDataSource_Facility_Id.SelectParameters["Form_Id"].DefaultValue = "0";
          DropDownList_FacilityId.Items.Insert(0, new ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
          DropDownList_FacilityId.DataBind();
        }
        else
        {
          DropDownList_FormId.SelectedValue = Request.QueryString["s_Form_Id"];

          DropDownList_FacilityId.Items.Clear();
          SqlDataSource_Facility_Id.SelectParameters["Form_Id"].DefaultValue = Request.QueryString["s_Form_Id"];
          DropDownList_FacilityId.Items.Insert(0, new ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
          DropDownList_FacilityId.DataBind();
        }
      }

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
    }


    //--START-- --Search--//
    protected void DropDownList_FormId_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_FacilityId.Items.Clear();
      SqlDataSource_Facility_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

      if (string.IsNullOrEmpty(DropDownList_FormId.SelectedValue))
      {
        SqlDataSource_Facility_Id.SelectParameters["Form_Id"].DefaultValue = "0";
      }
      else
      {
        SqlDataSource_Facility_Id.SelectParameters["Form_Id"].DefaultValue = DropDownList_FormId.SelectedValue.ToString();
      }

      DropDownList_FacilityId.Items.Insert(0, new ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
      DropDownList_FacilityId.DataBind();

      GridView_Facility_Form_List.DataBind();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_FormId.SelectedValue;
      string SearchField2 = DropDownList_FacilityId.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Form_Id=" + DropDownList_FormId.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Facility_Id=" + DropDownList_FacilityId.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Administration_Facility_Form_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Form List", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Form List", "Administration_Facility_Form_List.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_Facility_Form_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_Facility_Form_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_Facility_Form_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_Facility_Form_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_Facility_Form_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_Facility_Form_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Facility_Form_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Facility_Form_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Facility_Form_List.PageSize > 20 && GridView_Facility_Form_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Facility_Form_List.PageSize > 50 && GridView_Facility_Form_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_Facility_Form_List_DataBound(object sender, EventArgs e)
    {

      GridViewRow GridViewRow_List = GridView_Facility_Form_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Facility_Form_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Facility_Form_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }

    }

    protected void GridView_Facility_Form_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Form", "Administration_Facility_Form.aspx"), false);
    }

    public string GetLink(object form_Id)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Form", "Administration_Facility_Form.aspx?Form_Id=" + form_Id + "") + "'>Update</a>";

      string SearchField1 = Request.QueryString["s_Form_Id"];
      string SearchField2 = Request.QueryString["s_Facility_Id"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_FormId=" + Request.QueryString["s_Form_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
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