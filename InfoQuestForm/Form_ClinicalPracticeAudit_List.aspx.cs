using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_ClinicalPracticeAudit_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("19").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("19").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("19").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('19'))";
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
          Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("19");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_ClinicalPracticeAudit_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_CPA_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CPA_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_CPA_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CPA_Facility.SelectParameters.Clear();
      SqlDataSource_CPA_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CPA_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "19");
      SqlDataSource_CPA_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_CPA_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_CPA_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "InfoQuest_Form_ClinicalPracticeAudit_Elements");
      SqlDataSource_CPA_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CPA_Unit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CPA_Unit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CPA_Unit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CPA_Unit.SelectParameters.Clear();
      SqlDataSource_CPA_Unit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CPA_Unit.SelectParameters.Add("Form_Id", TypeCode.String, "19");
      SqlDataSource_CPA_Unit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_CPA_Unit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CPA_Unit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CPA_Unit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CPA_AuditType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CPA_AuditType.SelectCommand = "SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 19 AND ListCategory_Id = 44 AND ListItem_Parent = -1";

      SqlDataSource_ClinicalPracticeAudit_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ClinicalPracticeAudit_List.SelectCommand = "spInfoQuest_Get_Form_ClinicalPracticeAudit_List";
      SqlDataSource_ClinicalPracticeAudit_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_ClinicalPracticeAudit_List.CancelSelectOnNullParameter = false;
      SqlDataSource_ClinicalPracticeAudit_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_ClinicalPracticeAudit_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_ClinicalPracticeAudit_List.SelectParameters.Add("UnitId", TypeCode.String, Request.QueryString["s_Unit_Id"]);
      SqlDataSource_ClinicalPracticeAudit_List.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_CPA_PatientVisitNumber"]);
      SqlDataSource_ClinicalPracticeAudit_List.SelectParameters.Add("PatientName", TypeCode.String, Request.QueryString["s_CPA_PatientName"]);
      SqlDataSource_ClinicalPracticeAudit_List.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_CPA_ReportNumber"]);
      SqlDataSource_ClinicalPracticeAudit_List.SelectParameters.Add("AuditType", TypeCode.String, Request.QueryString["s_CPA_AuditType"]);
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

          DropDownList_Unit.Items.Clear();
          SqlDataSource_CPA_Unit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
          DropDownList_Unit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
          DropDownList_Unit.DataBind();
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Unit.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Unit_Id"] == null)
        {
          DropDownList_Unit.SelectedValue = "";
        }
        else
        {
          DropDownList_Unit.SelectedValue = Request.QueryString["s_Unit_Id"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text.ToString()))
      {
        if (Request.QueryString["s_CPA_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_CPA_PatientVisitNumber"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientName.Text.ToString()))
      {
        if (Request.QueryString["s_CPA_PatientName"] == null)
        {
          TextBox_PatientName.Text = "";
        }
        else
        {
          TextBox_PatientName.Text = Request.QueryString["s_CPA_PatientName"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
      {
        if (Request.QueryString["s_CPA_ReportNumber"] == null)
        {
          TextBox_ReportNumber.Text = "";
        }
        else
        {
          TextBox_ReportNumber.Text = Request.QueryString["s_CPA_ReportNumber"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_AuditType.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_CPA_AuditType"] == null)
        {
          DropDownList_AuditType.SelectedValue = "";
        }
        else
        {
          DropDownList_AuditType.SelectedValue = Request.QueryString["s_CPA_AuditType"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = DropDownList_Unit.SelectedValue;
      string SearchField3 = Server.HtmlEncode(TextBox_PatientVisitNumber.Text);
      string SearchField4 = Server.HtmlEncode(TextBox_PatientName.Text);
      string SearchField5 = Server.HtmlEncode(TextBox_ReportNumber.Text);
      string SearchField6 = DropDownList_AuditType.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1) && string.IsNullOrEmpty(SearchField2) && string.IsNullOrEmpty(SearchField3) && string.IsNullOrEmpty(SearchField4) && string.IsNullOrEmpty(SearchField5) && string.IsNullOrEmpty(SearchField6))
      {
        string FinalURL = "";
        FinalURL = "Form_ClinicalPracticeAudit_List.aspx";
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
          SearchField1 = "s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_Unit_Id=" + DropDownList_Unit.SelectedValue.ToString() + "&";
        }

        if (string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_CPA_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "&";
        }

        if (string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_CPA_PatientName=" + Server.HtmlEncode(TextBox_PatientName.Text.ToString()) + "&";
        }

        if (string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "";
        }
        else
        {
          SearchField5 = "s_CPA_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
        }

        if (string.IsNullOrEmpty(SearchField6))
        {
          SearchField6 = "";
        }
        else
        {
          SearchField6 = "s_CPA_AuditType=" + DropDownList_AuditType.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_ClinicalPracticeAudit_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_ClinicalPracticeAudit_List.aspx";
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_ClinicalPracticeAudit_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }    

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_ClinicalPracticeAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_ClinicalPracticeAudit_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_ClinicalPracticeAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_ClinicalPracticeAudit_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_ClinicalPracticeAudit_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_ClinicalPracticeAudit_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_ClinicalPracticeAudit_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_ClinicalPracticeAudit_List.PageSize > 20 && GridView_ClinicalPracticeAudit_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_ClinicalPracticeAudit_List.PageSize > 50 && GridView_ClinicalPracticeAudit_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_ClinicalPracticeAudit_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_ClinicalPracticeAudit_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_ClinicalPracticeAudit_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_ClinicalPracticeAudit_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_ClinicalPracticeAudit_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect("http://infoquest:5002/Form_ClinicalPracticeAudit.aspx", false);
    }

    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_Unit.Items.Clear();
      SqlDataSource_CPA_Unit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CPA_Unit.SelectParameters["Facility_Id"].DefaultValue = DropDownList_Facility.SelectedValue.ToString();
      DropDownList_Unit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_Unit.DataBind();
    }

    public string GetLink(object number, object facility_Id, object cpa_PI_PatientVisitNumber)
    {
      string LinkURL = "";
      if (number != null)
      {
        if (number.ToString() == "1")
        {
          LinkURL = "<a href='http://infoquest:5002/Form_ClinicalPracticeAudit.aspx?s_Facility_Id=" + facility_Id + "&s_CPA_PatientVisitNumber=" + cpa_PI_PatientVisitNumber + "&s_CPA_Number=Yes'>View</a>";
        }
        else if (number.ToString() == "2")
        {
          LinkURL = "<a href='http://infoquest:5002/Form_ClinicalPracticeAudit.aspx?s_Facility_Id=" + facility_Id + "&s_CPA_ReferenceNumber=" + cpa_PI_PatientVisitNumber + "'>View</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";
      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_Unit_Id"];
      string SearchField3 = Request.QueryString["s_CPA_PatientVisitNumber"];
      string SearchField4 = Request.QueryString["s_CPA_PatientName"];
      string SearchField5 = Request.QueryString["s_CPA_ReportNumber"];
      string SearchField6 = Request.QueryString["s_CPA_AuditType"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null && SearchField4 == null && SearchField5 == null && SearchField6 == null)
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
          //SearchField1 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
          SearchField1 = "s_FacilityDisplayName=" + Request.QueryString["s_Facility_Id"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          //SearchField2 = "Search_UnitId=" + Request.QueryString["s_Unit_Id"] + "&";
          SearchField2 = "s_Units_Name=" + Request.QueryString["s_Unit_Id"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          //SearchField3 = "Search_CPAPatientVisitNumber=" + Request.QueryString["s_CPA_PatientVisitNumber"] + "&";
          SearchField3 = "s_CPA_PI_PatientVisitNumber=" + Request.QueryString["s_CPA_PatientVisitNumber"] + "&";
        }

        if (SearchField4 == null)
        {
          SearchField4 = "";
        }
        else
        {
          //SearchField4 = "Search_CPAPatientName=" + Request.QueryString["s_CPA_PatientName"] + "&";
          SearchField4 = "s_CPA_PI_PatientName=" + Request.QueryString["s_CPA_PatientName"] + "&";
        }

        if (SearchField5 == null)
        {
          SearchField5 = "";
        }
        else
        {
          //SearchField5 = "Search_CPAReportNumber=" + Request.QueryString["s_CPA_ReportNumber"] + "&";
          SearchField5 = "s_CPA_Elements_ReportNumber=" + Request.QueryString["s_CPA_ReportNumber"] + "&";
        }

        if (SearchField6 == null)
        {
          SearchField6 = "";
        }
        else
        {
          //SearchField6 = "Search_CPAAuditType=" + Request.QueryString["s_CPA_AuditType"] + "&";
          SearchField6 = "s_CPA_Elements_AuditType_Name=" + Request.QueryString["s_CPA_AuditType"] + "&";
        }

        string SearchURL = "";
        SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = CurrentURL.Replace("'>View</a>", "&" + SearchURL + "'>View</a>");
      }

      return FinalURL;
    }
    //---END--- --List--//
  }
}