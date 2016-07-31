using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;

namespace InfoQuestForm
{
  public partial class Form_HeadOfficeQualityAudit_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("40").Replace(" Form", "")).ToString() + " : Captured Findings", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("40").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("40").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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
        string SecurityAllowForm = "0";

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('40'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("40");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_HeadOfficeQualityAudit_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Head Office Quality Audit", "14");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_HeadOfficeQualityAudit_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_HeadOfficeQualityAudit_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_Facility.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_HeadOfficeQualityAudit_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_HeadOfficeQualityAudit_Facility.SelectParameters.Add("TableFROM", TypeCode.String,  "0");
      SqlDataSource_HeadOfficeQualityAudit_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_Function.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_Function.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_Function.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_Function.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_Function.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_Function.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "175");
      SqlDataSource_HeadOfficeQualityAudit_Function.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_Function.SelectParameters.Add("TableSELECT", TypeCode.String, "HQA_Finding_Function_List");
      SqlDataSource_HeadOfficeQualityAudit_Function.SelectParameters.Add("TableFROM", TypeCode.String, "Form_HeadOfficeQualityAudit_Finding");
      SqlDataSource_HeadOfficeQualityAudit_Function.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_FinancialYear.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_FinancialYear.SelectCommand = "SELECT DISTINCT HQA_Finding_FinancialYear FROM Form_HeadOfficeQualityAudit_Finding ORDER BY HQA_Finding_FinancialYear DESC";

      SqlDataSource_HeadOfficeQualityAudit_Classification.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_Classification.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_Classification.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_Classification.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_Classification.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_Classification.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "177");
      SqlDataSource_HeadOfficeQualityAudit_Classification.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_Classification.SelectParameters.Add("TableSELECT", TypeCode.String, "HQA_Finding_Classification_List");
      SqlDataSource_HeadOfficeQualityAudit_Classification.SelectParameters.Add("TableFROM", TypeCode.String, "Form_HeadOfficeQualityAudit_Finding");
      SqlDataSource_HeadOfficeQualityAudit_Classification.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
      
      SqlDataSource_HeadOfficeQualityAudit_Tracking.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_Tracking.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_HeadOfficeQualityAudit_Tracking.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_Tracking.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_Tracking.SelectParameters.Add("Form_Id", TypeCode.String, "40");
      SqlDataSource_HeadOfficeQualityAudit_Tracking.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "173");
      SqlDataSource_HeadOfficeQualityAudit_Tracking.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_HeadOfficeQualityAudit_Tracking.SelectParameters.Add("TableSELECT", TypeCode.String, "HQA_Finding_Tracking_List");
      SqlDataSource_HeadOfficeQualityAudit_Tracking.SelectParameters.Add("TableFROM", TypeCode.String, "Form_HeadOfficeQualityAudit_Finding");
      SqlDataSource_HeadOfficeQualityAudit_Tracking.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_HeadOfficeQualityAudit_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_HeadOfficeQualityAudit_List.SelectCommand = "spForm_Get_HeadOfficeQualityAudit_List";
      SqlDataSource_HeadOfficeQualityAudit_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_HeadOfficeQualityAudit_List.CancelSelectOnNullParameter = false;
      SqlDataSource_HeadOfficeQualityAudit_List.SelectParameters.Clear();
      SqlDataSource_HeadOfficeQualityAudit_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_HeadOfficeQualityAudit_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_HeadOfficeQualityAudit_List.SelectParameters.Add("HQAFindingFunction", TypeCode.String, Request.QueryString["s_HQA_Finding_Function"]);
      SqlDataSource_HeadOfficeQualityAudit_List.SelectParameters.Add("HQAFindingFinancialYear", TypeCode.String, Request.QueryString["s_HQA_Finding_FinancialYear"]);
      SqlDataSource_HeadOfficeQualityAudit_List.SelectParameters.Add("HQAFindingClassification", TypeCode.String, Request.QueryString["s_HQA_Finding_Classification"]);
      SqlDataSource_HeadOfficeQualityAudit_List.SelectParameters.Add("HQAFindingTracking", TypeCode.String, Request.QueryString["s_HQA_Finding_Tracking"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Facility_Id"]))
        {
          DropDownList_Facility.SelectedValue = "";
        }
        else
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Function.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_HQA_Finding_Function"]))
        {
          DropDownList_Function.SelectedValue = "";
        }
        else
        {
          DropDownList_Function.SelectedValue = Request.QueryString["s_HQA_Finding_Function"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_FinancialYear.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_HQA_Finding_FinancialYear"]))
        {
          DropDownList_FinancialYear.SelectedValue = "";
        }
        else
        {
          DropDownList_FinancialYear.SelectedValue = Request.QueryString["s_HQA_Finding_FinancialYear"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Classification.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_HQA_Finding_Classification"]))
        {
          DropDownList_Classification.SelectedValue = "";
        }
        else
        {
          DropDownList_Classification.SelectedValue = Request.QueryString["s_HQA_Finding_Classification"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Tracking.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_HQA_Finding_Tracking"]))
        {
          DropDownList_Tracking.SelectedValue = "";
        }
        else
        {
          DropDownList_Tracking.SelectedValue = Request.QueryString["s_HQA_Finding_Tracking"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = DropDownList_Function.SelectedValue;
      string SearchField3 = DropDownList_FinancialYear.SelectedValue;
      string SearchField4 = DropDownList_Classification.SelectedValue;
      string SearchField5 = DropDownList_Tracking.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_HQA_Finding_Function=" + DropDownList_Function.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_HQA_Finding_FinancialYear=" + DropDownList_FinancialYear.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_HQA_Finding_Classification=" + DropDownList_Classification.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_HQA_Finding_Tracking=" + DropDownList_Tracking.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_HeadOfficeQualityAudit_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding Captured Findings", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding Captured Findings", "Form_HeadOfficeQualityAudit_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_HeadOfficeQualityAudit_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_HeadOfficeQualityAudit_List.PageSize = Convert.ToInt32(((DropDownList)GridView_HeadOfficeQualityAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_HeadOfficeQualityAudit_List.PageIndex = ((DropDownList)GridView_HeadOfficeQualityAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_HeadOfficeQualityAudit_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_HeadOfficeQualityAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_HeadOfficeQualityAudit_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_HeadOfficeQualityAudit_List.PageSize > 20 && GridView_HeadOfficeQualityAudit_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_HeadOfficeQualityAudit_List.PageSize > 50 && GridView_HeadOfficeQualityAudit_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_HeadOfficeQualityAudit_List.Rows.Count; i++)
      {
        GridView_HeadOfficeQualityAudit_List.HeaderRow.Cells[8].Visible = false;

        if (GridView_HeadOfficeQualityAudit_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[8].Visible = false;

          if (GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[8].Text.ToString() == "5396")
          {
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].BackColor = Color.FromName("#d46e6e");
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[8].Text.ToString() == "5397")
          {
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].BackColor = Color.FromName("#FFFF77");
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[8].Text.ToString() == "5399")
          {
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].BackColor = Color.FromName("#77cf9c");
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[8].Text.ToString() == "5398")
          {
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].BackColor = Color.FromName("#ffcc66");
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].BackColor = Color.FromName("#d46e6e");
            GridView_HeadOfficeQualityAudit_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }


          if (GridView_List.Rows[i].Cells[9].Text == "No")
          {
            GridView_List.Rows[i].Cells[9].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_List.Rows[i].Cells[9].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_HeadOfficeQualityAudit_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_HeadOfficeQualityAudit_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_HeadOfficeQualityAudit_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_HeadOfficeQualityAudit_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_HeadOfficeQualityAudit_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding New Form", "Form_HeadOfficeQualityAudit.aspx"), false);
    }

    public string GetLink(object hqa_Finding_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding New Form", "Form_HeadOfficeQualityAudit.aspx?HQAFindingId=" + hqa_Finding_Id + "") + "'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Head Office Quality Audit Finding New Form", "Form_HeadOfficeQualityAudit.aspx?HQAFindingId=" + hqa_Finding_Id + "") + "'>View</a>";
        }
      }

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_HQA_Finding_Function"];
      string SearchField3 = Request.QueryString["s_HQA_Finding_FinancialYear"];
      string SearchField4 = Request.QueryString["s_HQA_Finding_Classification"];
      string SearchField5 = Request.QueryString["s_HQA_Finding_Tracking"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_HQAFindingFunction=" + Request.QueryString["s_HQA_Finding_Function"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_HQAFindingFinancialYear=" + Request.QueryString["s_HQA_Finding_FinancialYear"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_HQAFindingClassification=" + Request.QueryString["s_HQA_Finding_Classification"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "Search_HQAFindingTracking=" + Request.QueryString["s_HQA_Finding_Tracking"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      string FinalURL = "";
      if (!string.IsNullOrEmpty(SearchURL))
      {
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        FinalURL = LinkURL.Replace("'>View</a>", "&" + SearchURL + "'>View</a>");
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