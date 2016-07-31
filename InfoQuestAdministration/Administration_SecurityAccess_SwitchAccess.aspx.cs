using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SecurityAccess_SwitchAccess : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_SwitchAccess_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_UserName_A.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_UserName_B.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          string SecurityAccessSwitchAccessId = "";
          string SQLStringSwitchAccess = "SELECT SecurityAccess_SwitchAccess_Id FROM Administration_SecurityAccess_SwitchAccess";
          using (SqlCommand SqlCommand_SwitchAccess = new SqlCommand(SQLStringSwitchAccess))
          {
            DataTable DataTable_SwitchAccess;
            using (DataTable_SwitchAccess = new DataTable())
            {
              DataTable_SwitchAccess.Locale = CultureInfo.CurrentCulture;
              DataTable_SwitchAccess = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SwitchAccess).Copy();
              if (DataTable_SwitchAccess.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_SwitchAccess.Rows)
                {
                  SecurityAccessSwitchAccessId = DataRow_Row["SecurityAccess_SwitchAccess_Id"].ToString();
                }
              }
            }
          }

          if (string.IsNullOrEmpty(SecurityAccessSwitchAccessId))
          {
            SwitchAccess1.Visible = true;
            SwitchAccess2.Visible = true;
            SwitchAccess3.Visible = true;
            SwitchAccessBack1.Visible = false;
          }
          else
          {
            SwitchAccess1.Visible = false;
            SwitchAccess2.Visible = false;
            SwitchAccess3.Visible = false;
            SwitchAccessBack1.Visible = true;
          }
        }
      }
    }

    private string PageSecurity()
    {
      string SecurityAllow = "0";

      string SecurityAllowAdministration = "0";

      string SecurityUserUserName = "";
      string SQLStringSecurityUser = "SELECT	Administration_SecurityUser.SecurityUser_UserName " +
                                     " FROM		( " +
	                                   "   SELECT	Administration_SecurityAccess.SecurityUser_Id , " +
					                           "           SecurityUser_UserName " +
	                                   "   FROM		( " +
		                                 "     SELECT	Administration_SecurityUser_A.SecurityUser_Id , " +
						                         "             Administration_SecurityUser_B.SecurityUser_UserName " +
		                                 "     FROM		Administration_SecurityAccess_SwitchAccess " +
						                         "             LEFT JOIN Administration_SecurityUser AS Administration_SecurityUser_A ON Administration_SecurityAccess_SwitchAccess.SecurityAccess_SwitchAccess_UserA = Administration_SecurityUser_A.SecurityUser_Id " +
						                         "             LEFT JOIN Administration_SecurityUser AS Administration_SecurityUser_B ON Administration_SecurityAccess_SwitchAccess.SecurityAccess_SwitchAccess_UserB = Administration_SecurityUser_B.SecurityUser_Id " +
		                                 "     UNION " +
		                                 "     SELECT	Administration_SecurityUser_B.SecurityUser_Id , " +
						                         "             Administration_SecurityUser_A.SecurityUser_UserName " +
		                                 "     FROM		Administration_SecurityAccess_SwitchAccess " +
						                         "             LEFT JOIN Administration_SecurityUser AS Administration_SecurityUser_A ON Administration_SecurityAccess_SwitchAccess.SecurityAccess_SwitchAccess_UserA = Administration_SecurityUser_A.SecurityUser_Id " +
						                         "             LEFT JOIN Administration_SecurityUser AS Administration_SecurityUser_B ON Administration_SecurityAccess_SwitchAccess.SecurityAccess_SwitchAccess_UserB = Administration_SecurityUser_B.SecurityUser_Id " +
	                                   "   ) AS		TempTableA " +
					                           "           LEFT JOIN Administration_SecurityAccess ON TempTableA.SecurityUser_Id = Administration_SecurityAccess.SecurityUser_Id " +
	                                   "   WHERE		Administration_SecurityAccess.SecurityRole_Id = 1 " +
					                           "           AND SecurityUser_UserName = @SecurityUser_UserName " +
                                     " ) AS		TempTableB " +
				                             "         LEFT JOIN Administration_SecurityUser ON TempTableB.SecurityUser_Id = Administration_SecurityUser.SecurityUser_Id";
      using (SqlCommand SqlCommand_SecurityUser = new SqlCommand(SQLStringSecurityUser))
      {
        SqlCommand_SecurityUser.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_SecurityUser;
        using (DataTable_SecurityUser = new DataTable())
        {
          DataTable_SecurityUser.Locale = CultureInfo.CurrentCulture;
          DataTable_SecurityUser = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUser).Copy();
          if (DataTable_SecurityUser.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SecurityUser.Rows)
            {
              SecurityUserUserName = DataRow_Row["SecurityUser_UserName"].ToString();
            }
          }
        }
      }

      if (string.IsNullOrEmpty(SecurityUserUserName))
      {
        SecurityAllowAdministration = InfoQuestWCF.InfoQuest_Security.Security_Form_Administration(Request.ServerVariables["LOGON_USER"]);
      }
      else
      {        
        SecurityAllowAdministration = InfoQuestWCF.InfoQuest_Security.Security_Form_Administration(SecurityUserUserName);
      }
      

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SecurityAccess_SwitchAccess.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }


    //--START-- --List--//
    protected void SqlDataSource_SwitchAccess_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_SwitchAccess_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_SwitchAccess_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_SwitchAccess_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_SwitchAccess_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_SwitchAccess_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_SwitchAccess_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_SwitchAccess_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_SwitchAccess_List.PageSize > 20 && GridView_SwitchAccess_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_SwitchAccess_List.PageSize > 50 && GridView_SwitchAccess_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_SwitchAccess_List_DataBound(object sender, EventArgs e)
    {

      GridViewRow GridViewRow_List = GridView_SwitchAccess_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_SwitchAccess_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_SwitchAccess_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }

    }

    protected void GridView_SwitchAccess_List_RowCreated(object sender, GridViewRowEventArgs e)
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


    protected void Button_SwitchAccess_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(DropDownList_UserName_A.SelectedValue) || string.IsNullOrEmpty(DropDownList_UserName_B.SelectedValue))
      {
        Label_ErrorMessage.Text = Convert.ToString("User Name A and User Name B needs to be selected to Switch Access", CultureInfo.CurrentCulture);
      }
      else
      {
        string SQLStringSwitchAccess = "EXECUTE spAdministration_Execute_SecurityAccess_SwitchAccess @SecurityUser_UserName_A , @SecurityUser_UserName_B";
        using (SqlCommand SqlCommand_SwitchAccess = new SqlCommand(SQLStringSwitchAccess))
        {
          SqlCommand_SwitchAccess.Parameters.AddWithValue("@SecurityUser_UserName_A", DropDownList_UserName_A.SelectedValue);
          SqlCommand_SwitchAccess.Parameters.AddWithValue("@SecurityUser_UserName_B", DropDownList_UserName_B.SelectedValue);
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_SwitchAccess);
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security Access Switch Access", "Administration_SecurityAccess_SwitchAccess.aspx"), false);
      }
    }

    protected void Button_SwitchAccessBack_Click(object sender, EventArgs e)
    {
      string SQLStringSwitchAccess = "EXECUTE spAdministration_Execute_SecurityAccess_SwitchAccess '' , ''";
      using (SqlCommand SqlCommand_SwitchAccess = new SqlCommand(SQLStringSwitchAccess))
      {
        InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_SwitchAccess);
      }

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security Access Switch Access", "Administration_SecurityAccess_SwitchAccess.aspx"), false);
    }
  }
}