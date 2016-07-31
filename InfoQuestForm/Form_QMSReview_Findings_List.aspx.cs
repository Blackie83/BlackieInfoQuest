using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_QMSReview_Findings_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString() + " : Findings", CultureInfo.CurrentCulture);
          Label_ReviewHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString() + " Findings", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString() + " Findings", CultureInfo.CurrentCulture);


          if (string.IsNullOrEmpty(DropDownList_System.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_QMSReview_Findings_System"] == null)
            {
              DropDownList_System.SelectedValue = "";
            }
            else
            {
              DropDownList_System.SelectedValue = Request.QueryString["s_QMSReview_Findings_System"];
            }
          }

          if (string.IsNullOrEmpty(DropDownList_Category.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_QMSReview_Findings_Category"] == null)
            {
              DropDownList_Category.SelectedValue = "";
            }
            else
            {
              DropDownList_Category.SelectedValue = Request.QueryString["s_QMSReview_Findings_Category"];
            }
          }

          if (string.IsNullOrEmpty(DropDownList_Tracking.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_QMSReview_Findings_Tracking"] == null)
            {
              DropDownList_Tracking.SelectedValue = "";
            }
            else
            {
              DropDownList_Tracking.SelectedValue = Request.QueryString["s_QMSReview_Findings_Tracking"];
            }
          }


          if (Request.QueryString["QMSReview_Id"] != null)
          {
            TableReviewInfo.Visible = true;
            TableSearch.Visible = true;
            TableList.Visible = true;

            QMSReviewFindings();
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
        if (Request.QueryString["QMSReview_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('31')) AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("31");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_QMSReview_Findings_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Internal Quality Audit", "12");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_QMSReview_Findings_System.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_QMSReview_Findings_System.SelectCommand = "SELECT DISTINCT QMSReview_Findings_System FROM InfoQuest_Form_QMSReview_Findings WHERE QMSReview_Id = @QMSReview_Id ORDER BY QMSReview_Findings_System";
      SqlDataSource_QMSReview_Findings_System.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_QMSReview_Findings_System.SelectParameters.Clear();
      SqlDataSource_QMSReview_Findings_System.SelectParameters.Add("QMSReview_Id", TypeCode.Int32, Request.QueryString["QMSReview_Id"]);

      SqlDataSource_QMSReview_Findings_Category.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_QMSReview_Findings_Category.SelectCommand = "SELECT DISTINCT QMSReview_Findings_Category FROM InfoQuest_Form_QMSReview_Findings WHERE QMSReview_Id = @QMSReview_Id ORDER BY QMSReview_Findings_Category";
      SqlDataSource_QMSReview_Findings_Category.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_QMSReview_Findings_Category.SelectParameters.Clear();
      SqlDataSource_QMSReview_Findings_Category.SelectParameters.Add("QMSReview_Id", TypeCode.String, Request.QueryString["QMSReview_Id"]);

      SqlDataSource_QMSReview_Findings_Tracking.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_QMSReview_Findings_Tracking.SelectCommand = "SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 31 AND ListCategory_Id = 94 AND ListItem_Parent = -1 ORDER BY ListItem_Name";
      SqlDataSource_QMSReview_Findings_Tracking.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_QMSReview_Findings_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_QMSReview_Findings_List.SelectCommand = "spForm_Get_QMSReview_Findings_List";
      SqlDataSource_QMSReview_Findings_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_QMSReview_Findings_List.CancelSelectOnNullParameter = false;
      SqlDataSource_QMSReview_Findings_List.SelectParameters.Clear();
      SqlDataSource_QMSReview_Findings_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_QMSReview_Findings_List.SelectParameters.Add("QMSReviewId", TypeCode.String, Request.QueryString["QMSReview_Id"]);
      SqlDataSource_QMSReview_Findings_List.SelectParameters.Add("QMSReviewFindingsSystem", TypeCode.String, Request.QueryString["s_QMSReview_Findings_System"]);
      SqlDataSource_QMSReview_Findings_List.SelectParameters.Add("QMSReviewFindingsCategory", TypeCode.String, Request.QueryString["s_QMSReview_Findings_Category"]);
      SqlDataSource_QMSReview_Findings_List.SelectParameters.Add("QMSReviewFindingsTracking", TypeCode.String, Request.QueryString["s_QMSReview_Findings_Tracking"]);
    }

    private void QMSReviewFindings()
    {
      Session["QMSReviewId"] = "";
      string SQLStringQMSReview = "SELECT QMSReview_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id AND QMSReview_IsActive = 1";
      using (SqlCommand SqlCommand_QMSReview = new SqlCommand(SQLStringQMSReview))
      {
        SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
        DataTable DataTable_QMSReview;
        using (DataTable_QMSReview = new DataTable())
        {
          DataTable_QMSReview.Locale = CultureInfo.CurrentCulture;
          DataTable_QMSReview = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QMSReview).Copy();
          if (DataTable_QMSReview.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_QMSReview.Rows)
            {
              Session["QMSReviewId"] = DataRow_Row["QMSReview_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["QMSReviewId"].ToString()))
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
      Session["QMSReviewDate"] = "";
      Session["QMSReviewCompleted"] = "";
      string SQLStringReviewInfo = "SELECT DISTINCT Facility_FacilityDisplayName , QMSReview_Date , QMSReview_Completed FROM vForm_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
      using (SqlCommand SqlCommand_ReviewInfo = new SqlCommand(SQLStringReviewInfo))
      {
        SqlCommand_ReviewInfo.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
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
              Session["QMSReviewDate"] = DataRow_Row["QMSReview_Date"];
              Session["QMSReviewCompleted"] = DataRow_Row["QMSReview_Completed"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label_Facility.Text = Session["FacilityFacilityDisplayName"].ToString();
        Label_Date.Text = Convert.ToDateTime(Session["QMSReviewDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        if (Session["QMSReviewCompleted"].ToString() == "True")
        {
          Label_Completed.Text = Convert.ToString("Yes", CultureInfo.CurrentCulture);
        }
        else if (Session["QMSReviewCompleted"].ToString() == "False")
        {
          Label_Completed.Text = Convert.ToString("No", CultureInfo.CurrentCulture);
        }
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("QMSReviewDate");
      Session.Remove("QMSReviewCompleted");
    }


    //--START-- --Search--//
    protected void Button_Back_Click(object sender, EventArgs e)
    {
      if (Request.QueryString["QMSReview_Id"] != null)
      {
        string FinalURL = "";

        string SearchField1 = Request.QueryString["Search_FacilityId"];
        string SearchField2 = Request.QueryString["Search_QMSReviewCompleted"];

        if (SearchField1 == null && SearchField2 == null)
        {
          FinalURL = "Form_QMSReview_List.aspx";
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
            SearchField2 = "s_QMSReview_Completed=" + Request.QueryString["Search_QMSReviewCompleted"] + "&";
          }

          string SearchURL = "Form_QMSReview_List.aspx?" + SearchField1 + SearchField2;
          SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

          FinalURL = SearchURL;
        }

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_QMSReview_Findings_List.aspx?QMSReview_Id=" + Request.QueryString["QMSReview_Id"] + "";
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
        FinalURL = "Form_QMSReview_Findings_List.aspx?QMSReview_Id=" + Request.QueryString["QMSReview_Id"] + "";
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
          SearchField1 = "s_QMSReview_Findings_System=" + DropDownList_System.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_QMSReview_Findings_Category=" + DropDownList_Category.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_QMSReview_Findings_Tracking=" + DropDownList_Tracking.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_QMSReview_Findings_List.aspx?QMSReview_Id=" + Request.QueryString["QMSReview_Id"] + "&" + SearchField1 + SearchField2 + SearchField3;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        Response.Redirect(FinalURL, false);
      }
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_QMSReview_Findings_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_QMSReview_Findings_List.PageSize = Convert.ToInt32(((DropDownList)GridView_QMSReview_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);

      Session["GridViewQMSReviewFindingsList_DropDownListPageSize"] = Convert.ToInt32(((DropDownList)GridView_QMSReview_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_PageSize_DataBinding(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewQMSReviewFindingsList_DropDownListPageSize"] != null)
        {
          GridView_QMSReview_Findings_List.PageSize = Convert.ToInt32(Session["GridViewQMSReviewFindingsList_DropDownListPageSize"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_QMSReview_Findings_List.PageIndex = ((DropDownList)GridView_QMSReview_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;

      Session["GridViewQMSReviewFindingsList_DropDownListPage"] = ((DropDownList)GridView_QMSReview_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void DropDownList_Page_DataBinding(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewQMSReviewFindingsList_DropDownListPage"] != null)
        {
          GridView_QMSReview_Findings_List.PageIndex = Convert.ToInt32(Session["GridViewQMSReviewFindingsList_DropDownListPage"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void ImageButton_First_Unload(object sender, EventArgs e)
    {
      Session["GridViewQMSReviewFindingsList_DropDownListPage"] = ((DropDownList)GridView_QMSReview_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Prev_Unload(object sender, EventArgs e)
    {
      Session["GridViewQMSReviewFindingsList_DropDownListPage"] = ((DropDownList)GridView_QMSReview_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Next_Unload(object sender, EventArgs e)
    {
      Session["GridViewQMSReviewFindingsList_DropDownListPage"] = ((DropDownList)GridView_QMSReview_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Last_Unload(object sender, EventArgs e)
    {
      Session["GridViewQMSReviewFindingsList_DropDownListPage"] = ((DropDownList)GridView_QMSReview_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_QMSReview_Findings_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_QMSReview_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_QMSReview_Findings_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_QMSReview_Findings_List.PageSize > 20 && GridView_QMSReview_Findings_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_QMSReview_Findings_List.PageSize > 50 && GridView_QMSReview_Findings_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_QMSReview_Findings_List.Rows.Count; i++)
      {
        GridView_QMSReview_Findings_List.HeaderRow.Cells[6].Visible = false;

        if (GridView_QMSReview_Findings_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          GridView_QMSReview_Findings_List.Rows[i].Cells[6].Visible = false;

          if (GridView_QMSReview_Findings_List.Rows[i].Cells[6].Text.ToString() == "4314")
          {
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_QMSReview_Findings_List.Rows[i].Cells[6].Text.ToString() == "4315")
          {
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#FFFF77");
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_QMSReview_Findings_List.Rows[i].Cells[6].Text.ToString() == "4316")
          {
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#77cf9c");
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_QMSReview_Findings_List.Rows[i].Cells[6].Text.ToString() == "4323")
          {
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#ffcc66");
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_QMSReview_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_QMSReview_Findings_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_QMSReview_Findings_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_QMSReview_Findings_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_QMSReview_Findings_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_QMSReview_Findings_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object qmsReview_Findings_Id, object qmsReview_Completed)
    {
      string LinkURL = "";
      if (qmsReview_Completed != null)
      {
        if (qmsReview_Completed.ToString() == "True")
        {
          LinkURL = "<a href='Form_QMSReview_Findings.aspx?QMSReview_Id=" + Request.QueryString["QMSReview_Id"] + "&QMSReview_Findings_Id=" + qmsReview_Findings_Id + "'>View</a>";
        }
        else
        {
          LinkURL = "<a href='Form_QMSReview_Findings.aspx?QMSReview_Id=" + Request.QueryString["QMSReview_Id"] + "&QMSReview_Findings_Id=" + qmsReview_Findings_Id + "'>Update</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      string SearchField1 = Request.QueryString["s_QMSReview_Findings_System"];
      string SearchField2 = Request.QueryString["s_QMSReview_Findings_Category"];
      string SearchField3 = Request.QueryString["s_QMSReview_Findings_Tracking"];

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
          SearchField1 = "Search_QMSReviewFindingsSystem=" + Request.QueryString["s_QMSReview_Findings_System"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "Search_QMSReviewFindingsCategory=" + Request.QueryString["s_QMSReview_Findings_Category"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "Search_QMSReviewFindingsTracking=" + Request.QueryString["s_QMSReview_Findings_Tracking"] + "&";
        }

        string SearchURL = "";
        SearchURL = SearchField1 + SearchField2 + SearchField3;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        if (qmsReview_Completed != null)
        {
          if (qmsReview_Completed.ToString() == "True")
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