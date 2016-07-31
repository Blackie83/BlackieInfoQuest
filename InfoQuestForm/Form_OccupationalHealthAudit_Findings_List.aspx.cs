using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_OccupationalHealthAudit_Findings_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString() + " : Findings", CultureInfo.CurrentCulture);
          Label_ReviewHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString() + " Findings", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString() + " Findings", CultureInfo.CurrentCulture);


          if (string.IsNullOrEmpty(DropDownList_System.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_OHA_Findings_System"] == null)
            {
              DropDownList_System.SelectedValue = "";
            }
            else
            {
              DropDownList_System.SelectedValue = Request.QueryString["s_OHA_Findings_System"];
            }
          }

          if (string.IsNullOrEmpty(DropDownList_Category.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_OHA_Findings_Category"] == null)
            {
              DropDownList_Category.SelectedValue = "";
            }
            else
            {
              DropDownList_Category.SelectedValue = Request.QueryString["s_OHA_Findings_Category"];
            }
          }

          if (string.IsNullOrEmpty(DropDownList_Tracking.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_OHA_Findings_Tracking"] == null)
            {
              DropDownList_Tracking.SelectedValue = "";
            }
            else
            {
              DropDownList_Tracking.SelectedValue = Request.QueryString["s_OHA_Findings_Tracking"];
            }
          }


          if (Request.QueryString["OHA_Id"] != null)
          {
            TableReviewInfo.Visible = true;
            TableSearch.Visible = true;
            TableList.Visible = true;

            OccupationalHealthAuditFindings();
          }
          else
          {
            TableReviewInfo.Visible = false;
            TableSearch.Visible = false;
            TableList.Visible = false;
          }

          if (TableReviewInfo.Visible == true)
          {
            TableReviewInfoVisible();
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

        string SQLStringSecurity = "";
        if (Request.QueryString["OHA_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('48')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("48");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_OccupationalHealthAudit_Findings_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Occupational Health Audit", "25");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_OccupationalHealthAudit_Findings_System.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Findings_System.SelectCommand = "SELECT DISTINCT OHA_Findings_System FROM Form_OccupationalHealthAudit_Findings WHERE OHA_Id = @OHA_Id ORDER BY OHA_Findings_System";
      SqlDataSource_OccupationalHealthAudit_Findings_System.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_OccupationalHealthAudit_Findings_System.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Findings_System.SelectParameters.Add("OHA_Id", TypeCode.Int32, Request.QueryString["OHA_Id"]);

      SqlDataSource_OccupationalHealthAudit_Findings_Category.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Findings_Category.SelectCommand = "SELECT DISTINCT OHA_Findings_Category FROM Form_OccupationalHealthAudit_Findings WHERE OHA_Id = @OHA_Id ORDER BY OHA_Findings_Category";
      SqlDataSource_OccupationalHealthAudit_Findings_Category.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_OccupationalHealthAudit_Findings_Category.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Findings_Category.SelectParameters.Add("OHA_Id", TypeCode.String, Request.QueryString["OHA_Id"]);

      SqlDataSource_OccupationalHealthAudit_Findings_Tracking.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Findings_Tracking.SelectCommand = "SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 48 AND ListCategory_Id = 208 AND ListItem_Parent = -1 ORDER BY ListItem_Name";
      SqlDataSource_OccupationalHealthAudit_Findings_Tracking.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_OccupationalHealthAudit_Findings_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Findings_List.SelectCommand = "spForm_Get_OccupationalHealthAudit_Findings_List";
      SqlDataSource_OccupationalHealthAudit_Findings_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_OccupationalHealthAudit_Findings_List.CancelSelectOnNullParameter = false;
      SqlDataSource_OccupationalHealthAudit_Findings_List.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Findings_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_OccupationalHealthAudit_Findings_List.SelectParameters.Add("OHAId", TypeCode.String, Request.QueryString["OHA_Id"]);
      SqlDataSource_OccupationalHealthAudit_Findings_List.SelectParameters.Add("OHAFindingsSystem", TypeCode.String, Request.QueryString["s_OHA_Findings_System"]);
      SqlDataSource_OccupationalHealthAudit_Findings_List.SelectParameters.Add("OHAFindingsCategory", TypeCode.String, Request.QueryString["s_OHA_Findings_Category"]);
      SqlDataSource_OccupationalHealthAudit_Findings_List.SelectParameters.Add("OHAFindingsTracking", TypeCode.String, Request.QueryString["s_OHA_Findings_Tracking"]);
    }

    private void OccupationalHealthAuditFindings()
    {
      Session["OHAId"] = "";
      string SQLStringOccupationalHealthAudit = "SELECT OHA_Id FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id AND OHA_IsActive = 1";
      using (SqlCommand SqlCommand_OccupationalHealthAudit = new SqlCommand(SQLStringOccupationalHealthAudit))
      {
        SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
        DataTable DataTable_OccupationalHealthAudit;
        using (DataTable_OccupationalHealthAudit = new DataTable())
        {
          DataTable_OccupationalHealthAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_OccupationalHealthAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OccupationalHealthAudit).Copy();
          if (DataTable_OccupationalHealthAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OccupationalHealthAudit.Rows)
            {
              Session["OHAId"] = DataRow_Row["OHA_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["OHAId"].ToString()))
      {
        TableReviewInfo.Visible = false;
        TableSearch.Visible = false;
        TableList.Visible = false;
      }
      else
      {
        TableReviewInfo.Visible = true;
        TableSearch.Visible = true;
        TableList.Visible = true;
      }
    }

    private void TableReviewInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["UnitName"] = "";
      Session["OHADate"] = "";
      Session["OHACompleted"] = "";
      string SQLStringReviewInfo = "SELECT DISTINCT Facility_FacilityDisplayName , Unit_Name , OHA_Date , OHA_Completed FROM vForm_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id";
      using (SqlCommand SqlCommand_ReviewInfo = new SqlCommand(SQLStringReviewInfo))
      {
        SqlCommand_ReviewInfo.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
        DataTable DataTable_ReviewInfo;
        using (DataTable_ReviewInfo = new DataTable())
        {
          DataTable_ReviewInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_ReviewInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ReviewInfo).Copy();
          if (DataTable_ReviewInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ReviewInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["UnitName"] = DataRow_Row["Unit_Name"];
              Session["OHADate"] = DataRow_Row["OHA_Date"];
              Session["OHACompleted"] = DataRow_Row["OHA_Completed"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label_Facility.Text = Session["FacilityFacilityDisplayName"].ToString();
        Label_Unit.Text = Session["UnitName"].ToString();
        Label_Date.Text = Convert.ToDateTime(Session["OHADate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        if (Session["OHACompleted"].ToString() == "True")
        {
          Label_Completed.Text = Convert.ToString("Yes", CultureInfo.CurrentCulture);
        }
        else if (Session["OHACompleted"].ToString() == "False")
        {
          Label_Completed.Text = Convert.ToString("No", CultureInfo.CurrentCulture);
        }
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("UnitName");
      Session.Remove("OHADate");
      Session.Remove("OHACompleted");
    }


    //--START-- --Search--//
    protected void Button_Back_Click(object sender, EventArgs e)
    {
      if (Request.QueryString["OHA_Id"] != null)
      {
        string FinalURL = "";

        string SearchField1 = Request.QueryString["Search_FacilityId"];
        string SearchField2 = Request.QueryString["Search_UnitId"];
        string SearchField3 = Request.QueryString["Search_OHACompleted"];

        if (SearchField1 == null && SearchField2 == null && SearchField3 == null)
        {
          FinalURL = "Form_OccupationalHealthAudit_List.aspx";
        }
        else
        {
          if (SearchField1 == null)
          {
            SearchField1 = "";
          }
          else
          {
            SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
          }

          if (SearchField2 == null)
          {
            SearchField2 = "";
          }
          else
          {
            SearchField2 = "s_Unit_Id=" + Request.QueryString["Search_UnitId"] + "&";
          }

          if (SearchField3 == null)
          {
            SearchField3 = "";
          }
          else
          {
            SearchField3 = "s_OHA_Completed=" + Request.QueryString["Search_OHACompleted"] + "&";
          }

          string SearchURL = "Form_OccupationalHealthAudit_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
          SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

          FinalURL = SearchURL;
        }

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_OccupationalHealthAudit_Findings_List.aspx?OHA_Id=" + Request.QueryString["OHA_Id"] + "";
      Response.Redirect(FinalURL, false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_System.SelectedValue;
      string SearchField2 = DropDownList_Category.SelectedValue;
      string SearchField3 = DropDownList_Tracking.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1) && string.IsNullOrEmpty(SearchField2) && string.IsNullOrEmpty(SearchField3))
      {
        string FinalURL = "";
        FinalURL = "Form_OccupationalHealthAudit_Findings_List.aspx?OHA_Id=" + Request.QueryString["OHA_Id"] + "";
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
          SearchField1 = "s_OHA_Findings_System=" + DropDownList_System.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_OHA_Findings_Category=" + DropDownList_Category.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_OHA_Findings_Tracking=" + DropDownList_Tracking.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_OccupationalHealthAudit_Findings_List.aspx?OHA_Id=" + Request.QueryString["OHA_Id"] + "&" + SearchField1 + SearchField2 + SearchField3;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        Response.Redirect(FinalURL, false);
      }
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_OccupationalHealthAudit_Findings_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_OccupationalHealthAudit_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_OccupationalHealthAudit_Findings_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_OccupationalHealthAudit_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_OccupationalHealthAudit_Findings_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_OccupationalHealthAudit_Findings_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_OccupationalHealthAudit_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_OccupationalHealthAudit_Findings_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_OccupationalHealthAudit_Findings_List.PageSize > 20 && GridView_OccupationalHealthAudit_Findings_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_OccupationalHealthAudit_Findings_List.PageSize > 50 && GridView_OccupationalHealthAudit_Findings_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_OccupationalHealthAudit_Findings_List.Rows.Count; i++)
      {
        GridView_OccupationalHealthAudit_Findings_List.HeaderRow.Cells[6].Visible = false;

        if (GridView_OccupationalHealthAudit_Findings_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[6].Visible = false;

          if (GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[6].Text.ToString() == "6141")
          {
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[6].Text.ToString() == "6142")
          {
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#FFFF77");
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[6].Text.ToString() == "6143")
          {
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#77cf9c");
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[6].Text.ToString() == "6144")
          {
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#ffcc66");
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_OccupationalHealthAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_OccupationalHealthAudit_Findings_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_OccupationalHealthAudit_Findings_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_OccupationalHealthAudit_Findings_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_OccupationalHealthAudit_Findings_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_OccupationalHealthAudit_Findings_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object oha_Findings_Id, object oha_Completed)
    {
      string LinkURL = "";
      if (oha_Completed != null)
      {
        if (oha_Completed.ToString() == "True")
        {
          LinkURL = "<a href='Form_OccupationalHealthAudit_Findings.aspx?OHA_Id=" + Request.QueryString["OHA_Id"] + "&OHA_Findings_Id=" + oha_Findings_Id + "'>View</a>";
        }
        else
        {
          LinkURL = "<a href='Form_OccupationalHealthAudit_Findings.aspx?OHA_Id=" + Request.QueryString["OHA_Id"] + "&OHA_Findings_Id=" + oha_Findings_Id + "'>Update</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      string SearchField1 = Request.QueryString["s_OHA_Findings_System"];
      string SearchField2 = Request.QueryString["s_OHA_Findings_Category"];
      string SearchField3 = Request.QueryString["s_OHA_Findings_Tracking"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null)
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
          SearchField1 = "Search_OHAFindingsSystem=" + Request.QueryString["s_OHA_Findings_System"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "Search_OHAFindingsCategory=" + Request.QueryString["s_OHA_Findings_Category"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "Search_OHAFindingsTracking=" + Request.QueryString["s_OHA_Findings_Tracking"] + "&";
        }

        string SearchURL = "";
        SearchURL = SearchField1 + SearchField2 + SearchField3;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        if (oha_Completed != null)
        {
          if (oha_Completed.ToString() == "True")
          {
            FinalURL = CurrentURL.Replace("'>View</a>", "&" + SearchURL + "'>View</a>");
          }
          else
          {
            FinalURL = CurrentURL.Replace("'>Update</a>", "&" + SearchURL + "'>Update</a>");
          }
        }
      }

      return FinalURL;
    }
    //---END--- --List--//
  }
}