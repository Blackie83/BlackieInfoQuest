using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_AntimicrobialStewardshipIntervention_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("39").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("39").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("39").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('39'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("39");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_AntimicrobialStewardshipIntervention_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Antimicrobial Stewardship", "13");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "39");
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "Form_AntimicrobialStewardshipIntervention_VisitInformation");
      SqlDataSource_AntimicrobialStewardshipIntervention_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
      
      SqlDataSource_AntimicrobialStewardshipIntervention_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_List.SelectCommand = "spForm_Get_AntimicrobialStewardshipIntervention_List";
      SqlDataSource_AntimicrobialStewardshipIntervention_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AntimicrobialStewardshipIntervention_List.CancelSelectOnNullParameter = false;
      SqlDataSource_AntimicrobialStewardshipIntervention_List.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_AntimicrobialStewardshipIntervention_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_AntimicrobialStewardshipIntervention_List.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientVisitNumber"]);
      SqlDataSource_AntimicrobialStewardshipIntervention_List.SelectParameters.Add("PatientName", TypeCode.String, Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientName"]);
      SqlDataSource_AntimicrobialStewardshipIntervention_List.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_AntimicrobialStewardshipIntervention_ReportNumber"]);
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
        if (Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientVisitNumber"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientName.Text.ToString()))
      {
        if (Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientName"] == null)
        {
          TextBox_PatientName.Text = "";
        }
        else
        {
          TextBox_PatientName.Text = Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientName"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
      {
        if (Request.QueryString["s_AntimicrobialStewardshipIntervention_ReportNumber"] == null)
        {
          TextBox_ReportNumber.Text = "";
        }
        else
        {
          TextBox_ReportNumber.Text = Request.QueryString["s_AntimicrobialStewardshipIntervention_ReportNumber"];
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
        SearchField2 = "s_AntimicrobialStewardshipIntervention_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_AntimicrobialStewardshipIntervention_PatientName=" + Server.HtmlEncode(TextBox_PatientName.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_AntimicrobialStewardshipIntervention_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
      }

      string FinalURL = "Form_AntimicrobialStewardshipIntervention_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship List", "Form_AntimicrobialStewardshipIntervention_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_AntimicrobialStewardshipIntervention_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_AntimicrobialStewardshipIntervention_List.PageSize = Convert.ToInt32(((DropDownList)GridView_AntimicrobialStewardshipIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_AntimicrobialStewardshipIntervention_List.PageIndex = ((DropDownList)GridView_AntimicrobialStewardshipIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_AntimicrobialStewardshipIntervention_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_AntimicrobialStewardshipIntervention_List.PageSize <= 20)
        {
          ((DropDownList)GridView_AntimicrobialStewardshipIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_AntimicrobialStewardshipIntervention_List.PageSize > 20 && GridView_AntimicrobialStewardshipIntervention_List.PageSize <= 50)
        {
          ((DropDownList)GridView_AntimicrobialStewardshipIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_AntimicrobialStewardshipIntervention_List.PageSize > 50 && GridView_AntimicrobialStewardshipIntervention_List.PageSize <= 100)
        {
          ((DropDownList)GridView_AntimicrobialStewardshipIntervention_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_AntimicrobialStewardshipIntervention_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_AntimicrobialStewardshipIntervention_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_AntimicrobialStewardshipIntervention_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_AntimicrobialStewardshipIntervention_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_AntimicrobialStewardshipIntervention_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship New Form", "Form_AntimicrobialStewardshipIntervention.aspx"), false);
    }

    public string GetLink(object asi_VisitInformation_Id)
    {
      string LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship New Form", "Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + asi_VisitInformation_Id + "") + "'>View</a>";

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientVisitNumber"];
      string SearchField3 = Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientName"];
      string SearchField4 = Request.QueryString["s_AntimicrobialStewardshipIntervention_ReportNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_AntimicrobialStewardshipInterventionPatientVisitNumber=" + Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_AntimicrobialStewardshipInterventionPatientName=" + Request.QueryString["s_AntimicrobialStewardshipIntervention_PatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_AntimicrobialStewardshipInterventionReportNumber=" + Request.QueryString["s_AntimicrobialStewardshipIntervention_ReportNumber"] + "&";
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