using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_InfectionPrevention_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_InfectionPrevention_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_InfectionType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_InfectionPrevention_Facility.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_InfectionPrevention_List.SelectParameters["SecurityUser"].DefaultValue = Request.ServerVariables["LOGON_USER"];

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("4").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("4").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("4").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          SqlDataSource_InfectionPrevention_InfectionType.SelectParameters["TableSELECT"].DefaultValue = "fkiInfectionTypeID";
          SqlDataSource_InfectionPrevention_InfectionType.SelectParameters["TableFROM"].DefaultValue = "tblInfectionPrevention_Site";

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("37");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_InfectionPrevention_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Facility"]))
        {
          DropDownList_Facility.SelectedValue = "";
        }
        else
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_ReportNumber"]))
        {
          TextBox_ReportNumber.Text = "";
        }
        else
        {
          TextBox_ReportNumber.Text = Request.QueryString["s_ReportNumber"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientName.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_PatientName"]))
        {
          TextBox_PatientName.Text = "";
        }
        else
        {
          TextBox_PatientName.Text = Request.QueryString["s_PatientName"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_PatientVisitNumber"]))
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_PatientVisitNumber"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_InfectionType.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_InfectionType"]))
        {
          DropDownList_InfectionType.SelectedValue = "";
        }
        else
        {
          DropDownList_InfectionType.SelectedValue = Request.QueryString["s_InfectionType"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = Server.HtmlEncode(TextBox_ReportNumber.Text);
      string SearchField3 = Server.HtmlEncode(TextBox_PatientName.Text);
      string SearchField4 = Server.HtmlEncode(TextBox_PatientVisitNumber.Text);
      string SearchField5 = DropDownList_InfectionType.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility=" + DropDownList_Facility.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_PatientName=" + Server.HtmlEncode(TextBox_PatientName.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_InfectionType=" + DropDownList_InfectionType.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_InfectionPrevention_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Captured Forms", "Form_InfectionPrevention_List.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_InfectionPrevention_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_InfectionPrevention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_InfectionPrevention_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_InfectionPrevention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_InfectionPrevention_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_InfectionPrevention_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_InfectionPrevention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_InfectionPrevention_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_InfectionPrevention_List.PageSize > 20 && GridView_InfectionPrevention_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_InfectionPrevention_List.PageSize > 50 && GridView_InfectionPrevention_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_InfectionPrevention_List.Rows.Count; i++)
      {
        if (GridView_InfectionPrevention_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_InfectionPrevention_List.Rows[i].Cells[7].Text == "Pending")
          {
            if (GridView_InfectionPrevention_List.Rows[i].Cells[5].Text == "Rejected")
            {
              GridView_InfectionPrevention_List.Rows[i].Cells[7].BackColor = Color.FromName("#77cf9c");
              GridView_InfectionPrevention_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
            }
            else
            {
              GridView_InfectionPrevention_List.Rows[i].Cells[7].BackColor = Color.FromName("#d46e6e");
              GridView_InfectionPrevention_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
            }
          }
          else
          {
            GridView_InfectionPrevention_List.Rows[i].Cells[7].BackColor = Color.FromName("#77cf9c");
            GridView_InfectionPrevention_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_InfectionPrevention_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_InfectionPrevention_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_InfectionPrevention_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_InfectionPrevention_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_InfectionPrevention_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention New Form", "Form_InfectionPrevention.aspx"), false);
    }

    public string GetLink(object infectionFormId, object facilityId, object patientVisitNumber, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention New Form", "Form_InfectionPrevention.aspx?InfectionFormID=" + infectionFormId + "&s_FacilityId=" + facilityId + "&s_PatientVisitNumber=" + patientVisitNumber + "") + "'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention New Form", "Form_InfectionPrevention.aspx?InfectionFormID=" + infectionFormId + "&s_FacilityId=" + facilityId + "&s_PatientVisitNumber=" + patientVisitNumber + "") + "'>View</a>";
        }
      }

      string SearchField1 = Request.QueryString["s_Facility"];
      string SearchField2 = Request.QueryString["s_ReportNumber"];
      string SearchField3 = Request.QueryString["s_PatientName"];
      string SearchField4 = Request.QueryString["s_PatientVisitNumber"];
      string SearchField5 = Request.QueryString["s_InfectionType"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_Facility=" + Request.QueryString["s_Facility"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_ReportNumber=" + Request.QueryString["s_ReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_PatientName=" + Request.QueryString["s_PatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_PatientVisitNumber=" + Request.QueryString["s_PatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "Search_InfectionType=" + Request.QueryString["s_InfectionType"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      string FinalURL = "";
      if (!string.IsNullOrEmpty(SearchURL))
      {
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);
        if (viewUpdate != null)
        {
          if (viewUpdate.ToString() == "Yes")
          {
            FinalURL = LinkURL.Replace("'>Update</a>", "&" + SearchURL + "'>Update</a>");
          }
          else if (viewUpdate.ToString() == "No")
          {
            FinalURL = LinkURL.Replace("'>View</a>", "&" + SearchURL + "'>View</a>");
          }
        }
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