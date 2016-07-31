using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_CollegeLearningAudit_Findings_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("49").Replace(" Form", "")).ToString() + " : Findings", CultureInfo.CurrentCulture);
          Label_ReviewHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("49").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("49").Replace(" Form", "")).ToString() + " Findings", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("49").Replace(" Form", "")).ToString() + " Findings", CultureInfo.CurrentCulture);


          if (string.IsNullOrEmpty(DropDownList_System.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_CLA_Findings_System"] == null)
            {
              DropDownList_System.SelectedValue = "";
            }
            else
            {
              DropDownList_System.SelectedValue = Request.QueryString["s_CLA_Findings_System"];
            }
          }

          if (string.IsNullOrEmpty(DropDownList_Category.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_CLA_Findings_Category"] == null)
            {
              DropDownList_Category.SelectedValue = "";
            }
            else
            {
              DropDownList_Category.SelectedValue = Request.QueryString["s_CLA_Findings_Category"];
            }
          }

          if (string.IsNullOrEmpty(DropDownList_Tracking.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_CLA_Findings_Tracking"] == null)
            {
              DropDownList_Tracking.SelectedValue = "";
            }
            else
            {
              DropDownList_Tracking.SelectedValue = Request.QueryString["s_CLA_Findings_Tracking"];
            }
          }


          if (Request.QueryString["CLA_Id"] != null)
          {
            TableReviewInfo.Visible = true;
            TableSearch.Visible = true;
            TableList.Visible = true;

            CollegeLearningAuditFindings();
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
        if (Request.QueryString["CLA_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('49')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("49");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_CollegeLearningAudit_Findings_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("College Learning Audit", "24");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_CollegeLearningAudit_Findings_System.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CollegeLearningAudit_Findings_System.SelectCommand = "SELECT DISTINCT CLA_Findings_System FROM Form_CollegeLearningAudit_Findings WHERE CLA_Id = @CLA_Id ORDER BY CLA_Findings_System";
      SqlDataSource_CollegeLearningAudit_Findings_System.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_CollegeLearningAudit_Findings_System.SelectParameters.Clear();
      SqlDataSource_CollegeLearningAudit_Findings_System.SelectParameters.Add("CLA_Id", TypeCode.Int32, Request.QueryString["CLA_Id"]);

      SqlDataSource_CollegeLearningAudit_Findings_Category.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CollegeLearningAudit_Findings_Category.SelectCommand = "SELECT DISTINCT CLA_Findings_Category FROM Form_CollegeLearningAudit_Findings WHERE CLA_Id = @CLA_Id ORDER BY CLA_Findings_Category";
      SqlDataSource_CollegeLearningAudit_Findings_Category.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_CollegeLearningAudit_Findings_Category.SelectParameters.Clear();
      SqlDataSource_CollegeLearningAudit_Findings_Category.SelectParameters.Add("CLA_Id", TypeCode.String, Request.QueryString["CLA_Id"]);

      SqlDataSource_CollegeLearningAudit_Findings_Tracking.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CollegeLearningAudit_Findings_Tracking.SelectCommand = "SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 49 AND ListCategory_Id = 206 AND ListItem_Parent = -1 ORDER BY ListItem_Name";
      SqlDataSource_CollegeLearningAudit_Findings_Tracking.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_CollegeLearningAudit_Findings_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CollegeLearningAudit_Findings_List.SelectCommand = "spForm_Get_CollegeLearningAudit_Findings_List";
      SqlDataSource_CollegeLearningAudit_Findings_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CollegeLearningAudit_Findings_List.CancelSelectOnNullParameter = false;
      SqlDataSource_CollegeLearningAudit_Findings_List.SelectParameters.Clear();
      SqlDataSource_CollegeLearningAudit_Findings_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CollegeLearningAudit_Findings_List.SelectParameters.Add("CLAId", TypeCode.String, Request.QueryString["CLA_Id"]);
      SqlDataSource_CollegeLearningAudit_Findings_List.SelectParameters.Add("CLAFindingsSystem", TypeCode.String, Request.QueryString["s_CLA_Findings_System"]);
      SqlDataSource_CollegeLearningAudit_Findings_List.SelectParameters.Add("CLAFindingsCategory", TypeCode.String, Request.QueryString["s_CLA_Findings_Category"]);
      SqlDataSource_CollegeLearningAudit_Findings_List.SelectParameters.Add("CLAFindingsTracking", TypeCode.String, Request.QueryString["s_CLA_Findings_Tracking"]);
    }

    private void CollegeLearningAuditFindings()
    {
      Session["CLAId"] = "";
      string SQLStringCollegeLearningAudit = "SELECT CLA_Id FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id AND CLA_IsActive = 1";
      using (SqlCommand SqlCommand_CollegeLearningAudit = new SqlCommand(SQLStringCollegeLearningAudit))
      {
        SqlCommand_CollegeLearningAudit.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
        DataTable DataTable_CollegeLearningAudit;
        using (DataTable_CollegeLearningAudit = new DataTable())
        {
          DataTable_CollegeLearningAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_CollegeLearningAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CollegeLearningAudit).Copy();
          if (DataTable_CollegeLearningAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CollegeLearningAudit.Rows)
            {
              Session["CLAId"] = DataRow_Row["CLA_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["CLAId"].ToString()))
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
      Session["CLADate"] = "";
      Session["CLACompleted"] = "";
      string SQLStringReviewInfo = "SELECT DISTINCT Facility_FacilityDisplayName , CLA_Date , CLA_Completed FROM vForm_CollegeLearningAudit WHERE CLA_Id = @CLA_Id";
      using (SqlCommand SqlCommand_ReviewInfo = new SqlCommand(SQLStringReviewInfo))
      {
        SqlCommand_ReviewInfo.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
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
              Session["CLADate"] = DataRow_Row["CLA_Date"];
              Session["CLACompleted"] = DataRow_Row["CLA_Completed"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label_Facility.Text = Session["FacilityFacilityDisplayName"].ToString();
        Label_Date.Text = Convert.ToDateTime(Session["CLADate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        if (Session["CLACompleted"].ToString() == "True")
        {
          Label_Completed.Text = Convert.ToString("Yes", CultureInfo.CurrentCulture);
        }
        else if (Session["CLACompleted"].ToString() == "False")
        {
          Label_Completed.Text = Convert.ToString("No", CultureInfo.CurrentCulture);
        }
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("CLADate");
      Session.Remove("CLACompleted");
    }


    //--START-- --Search--//
    protected void Button_Back_Click(object sender, EventArgs e)
    {
      if (Request.QueryString["CLA_Id"] != null)
      {
        string FinalURL = "";

        string SearchField1 = Request.QueryString["Search_FacilityId"];
        string SearchField2 = Request.QueryString["Search_CLACompleted"];

        if (SearchField1 == null && SearchField2 == null)
        {
          FinalURL = "Form_CollegeLearningAudit_List.aspx";
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
            SearchField2 = "s_CLA_Completed=" + Request.QueryString["Search_CLACompleted"] + "&";
          }

          string SearchURL = "Form_CollegeLearningAudit_List.aspx?" + SearchField1 + SearchField2;
          SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

          FinalURL = SearchURL;
        }

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_CollegeLearningAudit_Findings_List.aspx?CLA_Id=" + Request.QueryString["CLA_Id"] + "";
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
        FinalURL = "Form_CollegeLearningAudit_Findings_List.aspx?CLA_Id=" + Request.QueryString["CLA_Id"] + "";
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
          SearchField1 = "s_CLA_Findings_System=" + DropDownList_System.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_CLA_Findings_Category=" + DropDownList_Category.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_CLA_Findings_Tracking=" + DropDownList_Tracking.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_CollegeLearningAudit_Findings_List.aspx?CLA_Id=" + Request.QueryString["CLA_Id"] + "&" + SearchField1 + SearchField2 + SearchField3;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        Response.Redirect(FinalURL, false);
      }
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_CollegeLearningAudit_Findings_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_CollegeLearningAudit_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_CollegeLearningAudit_Findings_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_CollegeLearningAudit_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_CollegeLearningAudit_Findings_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_CollegeLearningAudit_Findings_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_CollegeLearningAudit_Findings_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_CollegeLearningAudit_Findings_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_CollegeLearningAudit_Findings_List.PageSize > 20 && GridView_CollegeLearningAudit_Findings_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_CollegeLearningAudit_Findings_List.PageSize > 50 && GridView_CollegeLearningAudit_Findings_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_CollegeLearningAudit_Findings_List.Rows.Count; i++)
      {
        GridView_CollegeLearningAudit_Findings_List.HeaderRow.Cells[6].Visible = false;

        if (GridView_CollegeLearningAudit_Findings_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[6].Visible = false;

          if (GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[6].Text.ToString() == "6132")
          {
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[6].Text.ToString() == "6133")
          {
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#FFFF77");
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[6].Text.ToString() == "6134")
          {
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#77cf9c");
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[6].Text.ToString() == "6135")
          {
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#ffcc66");
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_CollegeLearningAudit_Findings_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_CollegeLearningAudit_Findings_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_CollegeLearningAudit_Findings_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_CollegeLearningAudit_Findings_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_CollegeLearningAudit_Findings_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_CollegeLearningAudit_Findings_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object cla_Findings_Id, object cla_Completed)
    {
      string LinkURL = "";
      if (cla_Completed != null)
      {
        if (cla_Completed.ToString() == "True")
        {
          LinkURL = "<a href='Form_CollegeLearningAudit_Findings.aspx?CLA_Id=" + Request.QueryString["CLA_Id"] + "&CLA_Findings_Id=" + cla_Findings_Id + "'>View</a>";
        }
        else
        {
          LinkURL = "<a href='Form_CollegeLearningAudit_Findings.aspx?CLA_Id=" + Request.QueryString["CLA_Id"] + "&CLA_Findings_Id=" + cla_Findings_Id + "'>Update</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      string SearchField1 = Request.QueryString["s_CLA_Findings_System"];
      string SearchField2 = Request.QueryString["s_CLA_Findings_Category"];
      string SearchField3 = Request.QueryString["s_CLA_Findings_Tracking"];

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
          SearchField1 = "Search_CLAFindingsSystem=" + Request.QueryString["s_CLA_Findings_System"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "Search_CLAFindingsCategory=" + Request.QueryString["s_CLA_Findings_Category"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "Search_CLAFindingsTracking=" + Request.QueryString["s_CLA_Findings_Tracking"] + "&";
        }

        string SearchURL = "";
        SearchURL = SearchField1 + SearchField2 + SearchField3;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        if (cla_Completed != null)
        {
          if (cla_Completed.ToString() == "True")
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