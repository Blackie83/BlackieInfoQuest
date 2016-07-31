using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Isidima_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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
            if (Request.QueryString["s_Isidima_PatientVisitNumber"] == null)
            {
              TextBox_PatientVisitNumber.Text = "";
            }
            else
            {
              TextBox_PatientVisitNumber.Text = Request.QueryString["s_Isidima_PatientVisitNumber"];
            }
          }

          if (string.IsNullOrEmpty(TextBox_PatientName.Text.ToString()))
          {
            if (Request.QueryString["s_Isidima_PatientName"] == null)
            {
              TextBox_PatientName.Text = "";
            }
            else
            {
              TextBox_PatientName.Text = Request.QueryString["s_Isidima_PatientName"];
            }
          }

          if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
          {
            if (Request.QueryString["s_Isidima_ReportNumber"] == null)
            {
              TextBox_ReportNumber.Text = "";
            }
            else
            {
              TextBox_ReportNumber.Text = Request.QueryString["s_Isidima_ReportNumber"];
            }
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('27'))";
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
      if (PageSecurity() == "1")
      {

      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Isidima_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Isidima_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Isidima_Facility.SelectParameters.Clear();
      SqlDataSource_Isidima_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Isidima_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "27");
      SqlDataSource_Isidima_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Isidima_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_Isidima_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "InfoQuest_Form_Isidima_Category");
      SqlDataSource_Isidima_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Isidima_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_List.SelectCommand = "spForm_Get_Isidima_List";
      SqlDataSource_Isidima_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Isidima_List.CancelSelectOnNullParameter = false;
      SqlDataSource_Isidima_List.SelectParameters.Clear();
      SqlDataSource_Isidima_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Isidima_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_List.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_Isidima_PatientVisitNumber"]);
      SqlDataSource_Isidima_List.SelectParameters.Add("PatientName", TypeCode.String, Request.QueryString["s_Isidima_PatientName"]);
      SqlDataSource_Isidima_List.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_Isidima_ReportNumber"]);
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = Server.HtmlEncode(TextBox_PatientVisitNumber.Text);
      string SearchField3 = Server.HtmlEncode(TextBox_PatientName.Text);
      string SearchField4 = Server.HtmlEncode(TextBox_ReportNumber.Text);

      if (string.IsNullOrEmpty(SearchField1) && string.IsNullOrEmpty(SearchField2) && string.IsNullOrEmpty(SearchField3) && string.IsNullOrEmpty(SearchField4))
      {
        string FinalURL = "";
        FinalURL = "Form_Isidima_List.aspx";
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
          SearchField2 = "s_Isidima_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "&";
        }

        if (string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_Isidima_PatientName=" + Server.HtmlEncode(TextBox_PatientName.Text.ToString()) + "&";
        }

        if (string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_Isidima_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
        }

        string FinalURL = "Form_Isidima_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_Isidima_List.aspx";
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void Button_CaptureNew_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx", false);
    }

    protected void SqlDataSource_Isidima_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_Isidima_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_Isidima_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_Isidima_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_Isidima_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_Isidima_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Isidima_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Isidima_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Isidima_List.PageSize > 20 && GridView_Isidima_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Isidima_List.PageSize > 50 && GridView_Isidima_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_Isidima_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_Isidima_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Isidima_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Isidima_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_Isidima_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object facilityId, object patientVisitNumber)
    {
      string LinkURL = "";
      LinkURL = "<a href='Form_Isidima.aspx?s_Facility_Id=" + facilityId + "&s_Isidima_PatientVisitNumber=" + patientVisitNumber + "'>View</a>";

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_Isidima_PatientVisitNumber"];
      string SearchField3 = Request.QueryString["s_Isidima_PatientName"];
      string SearchField4 = Request.QueryString["s_Isidima_ReportNumber"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null && SearchField4 == null)
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
          SearchField1 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "Search_IsidimaPatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "Search_IsidimaPatientName=" + Request.QueryString["s_Isidima_PatientName"] + "&";
        }

        if (SearchField4 == null)
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "Search_IsidimaReportNumber=" + Request.QueryString["s_Isidima_ReportNumber"] + "&";
        }

        string SearchURL = "";
        SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = CurrentURL.Replace("'>View</a>", "&" + SearchURL + "'>View</a>");
      }

      return FinalURL;
    }
    //---END--- --List--//
  }
}