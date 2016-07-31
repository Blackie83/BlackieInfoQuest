using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_RehabBundleCompliance_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("35").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("35").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("35").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('35'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("35");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_RehabBundleCompliance_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Rehab Bundle Compliance", "8");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_RehabBundleCompliance_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_RehabBundleCompliance_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_List.SelectCommand = "spForm_Get_RehabBundleCompliance_List";
      SqlDataSource_RehabBundleCompliance_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_List.CancelSelectOnNullParameter = false;
      SqlDataSource_RehabBundleCompliance_List.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_RehabBundleCompliance_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_RehabBundleCompliance_List.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"]);
      SqlDataSource_RehabBundleCompliance_List.SelectParameters.Add("PatientName", TypeCode.String, Request.QueryString["s_RehabBundleCompliance_PatientName"]);
      SqlDataSource_RehabBundleCompliance_List.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_RehabBundleCompliance_ReportNumber"]);
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

      if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text.ToString()))
      {
        if (Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientName.Text.ToString()))
      {
        if (Request.QueryString["s_RehabBundleCompliance_PatientName"] == null)
        {
          TextBox_PatientName.Text = "";
        }
        else
        {
          TextBox_PatientName.Text = Request.QueryString["s_RehabBundleCompliance_PatientName"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
      {
        if (Request.QueryString["s_RehabBundleCompliance_ReportNumber"] == null)
        {
          TextBox_ReportNumber.Text = "";
        }
        else
        {
          TextBox_ReportNumber.Text = Request.QueryString["s_RehabBundleCompliance_ReportNumber"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = Server.HtmlEncode(TextBox_PatientVisitNumber.Text);
      string SearchField3 = Server.HtmlEncode(TextBox_PatientName.Text);
      string SearchField4 = Server.HtmlEncode(TextBox_ReportNumber.Text);

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_RehabBundleCompliance_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_RehabBundleCompliance_PatientName=" + Server.HtmlEncode(TextBox_PatientName.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_RehabBundleCompliance_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
      }

      string FinalURL = "Form_RehabBundleCompliance_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance List", "Form_RehabBundleCompliance_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_RehabBundleCompliance_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_RehabBundleCompliance_List.PageSize = Convert.ToInt32(((DropDownList)GridView_RehabBundleCompliance_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_RehabBundleCompliance_List.PageIndex = ((DropDownList)GridView_RehabBundleCompliance_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_RehabBundleCompliance_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_RehabBundleCompliance_List.PageSize <= 20)
        {
          ((DropDownList)GridView_RehabBundleCompliance_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_RehabBundleCompliance_List.PageSize > 20 && GridView_RehabBundleCompliance_List.PageSize <= 50)
        {
          ((DropDownList)GridView_RehabBundleCompliance_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_RehabBundleCompliance_List.PageSize > 50 && GridView_RehabBundleCompliance_List.PageSize <= 100)
        {
          ((DropDownList)GridView_RehabBundleCompliance_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_RehabBundleCompliance_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_RehabBundleCompliance_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_RehabBundleCompliance_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_RehabBundleCompliance_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_RehabBundleCompliance_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance New Form", "Form_RehabBundleCompliance.aspx"), false);
    }

    public string GetLink(object facility_Id, object bc_PI_PatientVisitNumber)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance New Form", "Form_RehabBundleCompliance.aspx?s_Facility_Id=" + facility_Id + "&s_RehabBundleCompliance_PatientVisitNumber=" + bc_PI_PatientVisitNumber + "") + "'>View</a>";

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"];
      string SearchField3 = Request.QueryString["s_RehabBundleCompliance_PatientName"];
      string SearchField4 = Request.QueryString["s_RehabBundleCompliance_ReportNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_RehabBundleCompliancePatientVisitNumber=" + Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_RehabBundleCompliancePatientName=" + Request.QueryString["s_RehabBundleCompliance_PatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_RehabBundleComplianceReportNumber=" + Request.QueryString["s_RehabBundleCompliance_ReportNumber"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4;
      string FinalURL = "";
      if (!string.IsNullOrEmpty(SearchURL))
      {
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        FinalURL = LinkURL.Replace("'>View</a>", "&" + SearchURL + "'>View</a>");
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