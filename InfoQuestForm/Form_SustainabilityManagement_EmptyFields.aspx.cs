﻿using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_SustainabilityManagement_EmptyFields : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("45").Replace(" Form", "")).ToString() + " : Empty Fields", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("45").Replace(" Form", "")).ToString() + " Empty Fields", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("45").Replace(" Form", "")).ToString() + " Empty Fields", CultureInfo.CurrentCulture);

          if (Request.QueryString["s_Facility_Id"] == null && Request.QueryString["s_PeriodFrom"] == null && Request.QueryString["s_PeriodTo"] == null && Request.QueryString["s_FYPeriodFrom"] == null && Request.QueryString["s_FYPeriodTo"] == null && Request.QueryString["s_EmptyField"] == null)
          {
            DivEmptyFields1.Visible = false;
            DivEmptyFields2.Visible = false;
            TableEmptyFields.Visible = false;
          }
          else
          {
            DivEmptyFields1.Visible = true;
            DivEmptyFields2.Visible = true;
            TableEmptyFields.Visible = true;

            SetFormQueryString();
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id IN ('178','179'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("45");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SustainabilityManagement_EmptyFields.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Sustainability Management", "21");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Facility.SelectParameters.Clear();
      SqlDataSource_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "45");
      SqlDataSource_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "Form_SustainabilityManagement");
      SqlDataSource_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_PeriodFrom.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PeriodFrom.SelectCommand = "SELECT DISTINCT SustainabilityManagement_Period FROM Form_SustainabilityManagement ORDER BY SustainabilityManagement_Period DESC";

      SqlDataSource_PeriodTo.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PeriodTo.SelectCommand = "SELECT DISTINCT SustainabilityManagement_Period FROM Form_SustainabilityManagement ORDER BY SustainabilityManagement_Period DESC";

      SqlDataSource_FYPeriodFrom.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_FYPeriodFrom.SelectCommand = "SELECT DISTINCT SustainabilityManagement_FYPeriod FROM Form_SustainabilityManagement ORDER BY SustainabilityManagement_FYPeriod DESC";

      SqlDataSource_FYPeriodTo.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_FYPeriodTo.SelectCommand = "SELECT DISTINCT SustainabilityManagement_FYPeriod FROM Form_SustainabilityManagement ORDER BY SustainabilityManagement_FYPeriod DESC";

      SqlDataSource_EmptyField.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_EmptyField.SelectCommand = "SELECT b.ListItem_Name + ' - ' + a.ListItem_Name AS ListItem_ListItemDisplayName , a.ListItem_Name FROM Administration_ListItem a LEFT JOIN Administration_ListItem b ON a.ListItem_Parent = b.ListItem_Id WHERE a.ListCategory_Id = 188 AND b.ListCategory_Id = 187 ORDER BY b.ListItem_Name , a.ListItem_Name";

      SqlDataSource_SustainabilityManagement_EmptyFields.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_SustainabilityManagement_EmptyFields.SelectCommand = "spForm_Get_SustainabilityManagement_EmptyFields";
      SqlDataSource_SustainabilityManagement_EmptyFields.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_SustainabilityManagement_EmptyFields.CancelSelectOnNullParameter = false;
      SqlDataSource_SustainabilityManagement_EmptyFields.SelectParameters.Clear();
      SqlDataSource_SustainabilityManagement_EmptyFields.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_SustainabilityManagement_EmptyFields.SelectParameters.Add("PeriodFrom", TypeCode.String, Request.QueryString["s_PeriodFrom"]);
      SqlDataSource_SustainabilityManagement_EmptyFields.SelectParameters.Add("PeriodTo", TypeCode.String, Request.QueryString["s_PeriodTo"]);
      SqlDataSource_SustainabilityManagement_EmptyFields.SelectParameters.Add("FYPeriodFrom", TypeCode.String, Request.QueryString["s_FYPeriodFrom"]);
      SqlDataSource_SustainabilityManagement_EmptyFields.SelectParameters.Add("FYPeriodTo", TypeCode.String, Request.QueryString["s_FYPeriodTo"]);
      SqlDataSource_SustainabilityManagement_EmptyFields.SelectParameters.Add("EmptyField", TypeCode.String, Request.QueryString["s_EmptyField"]);
    }

    private void SetFormQueryString()
    {
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

      if (string.IsNullOrEmpty(DropDownList_PeriodFrom.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_PeriodFrom"] == null)
        {
          DropDownList_PeriodFrom.SelectedValue = "";
        }
        else
        {
          DropDownList_PeriodFrom.SelectedValue = Request.QueryString["s_PeriodFrom"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_PeriodTo.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_PeriodTo"] == null)
        {
          DropDownList_PeriodTo.SelectedValue = "";
        }
        else
        {
          DropDownList_PeriodTo.SelectedValue = Request.QueryString["s_PeriodTo"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_FYPeriodFrom.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_FYPeriodFrom"] == null)
        {
          DropDownList_FYPeriodFrom.SelectedValue = "";
        }
        else
        {
          DropDownList_FYPeriodFrom.SelectedValue = Request.QueryString["s_FYPeriodFrom"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_FYPeriodTo.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_FYPeriodTo"] == null)
        {
          DropDownList_FYPeriodTo.SelectedValue = "";
        }
        else
        {
          DropDownList_FYPeriodTo.SelectedValue = Request.QueryString["s_FYPeriodTo"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_EmptyField.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_EmptyField"] == null)
        {
          DropDownList_EmptyField.SelectedValue = "";
        }
        else
        {
          DropDownList_EmptyField.SelectedValue = Request.QueryString["s_EmptyField"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management Empty Fields", "Form_SustainabilityManagement_EmptyFields.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = DropDownList_PeriodFrom.SelectedValue;
      string SearchField3 = DropDownList_PeriodTo.SelectedValue;
      string SearchField4 = DropDownList_FYPeriodFrom.SelectedValue;
      string SearchField5 = DropDownList_FYPeriodTo.SelectedValue;
      string SearchField6 = DropDownList_EmptyField.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1) && string.IsNullOrEmpty(SearchField2) && string.IsNullOrEmpty(SearchField3) && string.IsNullOrEmpty(SearchField4) && string.IsNullOrEmpty(SearchField5) && string.IsNullOrEmpty(SearchField6))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management Empty Fields", "Form_SustainabilityManagement_EmptyFields.aspx"), false);
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
          SearchField2 = "s_PeriodFrom=" + DropDownList_PeriodFrom.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_PeriodTo=" + DropDownList_PeriodTo.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_FYPeriodFrom=" + DropDownList_FYPeriodFrom.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "";
        }
        else
        {
          SearchField5 = "s_FYPeriodTo=" + DropDownList_FYPeriodTo.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField6))
        {
          SearchField6 = "";
        }
        else
        {
          SearchField6 = "s_EmptyField=" + DropDownList_EmptyField.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_SustainabilityManagement_EmptyFields.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management Empty Fields", FinalURL);
        Response.Redirect(FinalURL, false);
      }
    }
    //---END--- --Search--//


    //--START-- --List SustainabilityManagement_EmptyFields--//
    protected void SqlDataSource_SustainabilityManagement_EmptyFields_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_SustainabilityManagement_EmptyFields.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_SustainabilityManagement_EmptyFields.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_SustainabilityManagement_EmptyFields.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_SustainabilityManagement_EmptyFields.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_SustainabilityManagement_EmptyFields_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_SustainabilityManagement_EmptyFields.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_SustainabilityManagement_EmptyFields.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_SustainabilityManagement_EmptyFields.PageSize > 20 && GridView_SustainabilityManagement_EmptyFields.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_SustainabilityManagement_EmptyFields.PageSize > 50 && GridView_SustainabilityManagement_EmptyFields.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_SustainabilityManagement_EmptyFields.Rows.Count; i++)
      {
        if (GridView_SustainabilityManagement_EmptyFields.Rows[i].RowType == DataControlRowType.DataRow)
        {
          for (int a = 5; a <= 5; a++)
          {
            if (GridView_SustainabilityManagement_EmptyFields.Rows[i].Cells[a].Text == "Required")
            {
              GridView_SustainabilityManagement_EmptyFields.Rows[i].Cells[a].BackColor = Color.FromName("#d46e6e");
              GridView_SustainabilityManagement_EmptyFields.Rows[i].Cells[a].ForeColor = Color.FromName("#333333");
            }
            else
            {
              GridView_SustainabilityManagement_EmptyFields.Rows[i].Cells[a].BackColor = Color.FromName("#77cf9c");
              GridView_SustainabilityManagement_EmptyFields.Rows[i].Cells[a].ForeColor = Color.FromName("#333333");
            }
          }
        }
      }
    }

    protected void GridView_SustainabilityManagement_EmptyFields_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_SustainabilityManagement_EmptyFields.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_SustainabilityManagement_EmptyFields.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_SustainabilityManagement_EmptyFields.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }


      for (int i = GridView_SustainabilityManagement_EmptyFields.Rows.Count - 1; i > 0; i--)
      {
        GridViewRow GridViewRow_Row = GridView_SustainabilityManagement_EmptyFields.Rows[i];
        GridViewRow GridViewRow_PreviousRow = GridView_SustainabilityManagement_EmptyFields.Rows[i - 1];
        for (int j = 0; j <= 3; j++)
        {
          if (GridViewRow_Row.Cells[0].Text + GridViewRow_Row.Cells[j].Text == GridViewRow_PreviousRow.Cells[0].Text + GridViewRow_PreviousRow.Cells[j].Text)
          {
            if (GridViewRow_PreviousRow.Cells[j].RowSpan == 0)
            {
              if (GridViewRow_Row.Cells[j].RowSpan == 0)
              {
                GridViewRow_PreviousRow.Cells[j].RowSpan += 2;
              }
              else
              {
                GridViewRow_PreviousRow.Cells[j].RowSpan = GridViewRow_Row.Cells[j].RowSpan + 1;
              }

              GridViewRow_Row.Cells[j].Visible = false;
            }
          }

          GridViewRow_Row.Cells[j].BackColor = Color.FromName("#f7f7f7");
          GridViewRow_Row.Cells[j].ForeColor = Color.FromName("#000000");
        }
      }
    }

    protected void GridView_SustainabilityManagement_EmptyFields_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --List SustainabilityManagement_EmptyFields--//
  }
}