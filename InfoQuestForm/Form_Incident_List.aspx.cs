using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Incident_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("1").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("1").Replace(" Form", "")).ToString() + "s", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("1").Replace(" Form", "")).ToString() + "s", CultureInfo.CurrentCulture);
          Label_BulkApprovalHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("1").Replace(" Form", "")).ToString() + " : Bulk Approval", CultureInfo.CurrentCulture);

          SetFormQueryString();

          SetFormVisibility();
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('1'))";
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
          SecurityAllow = "1";
          //SecurityAllow = "0";
          //Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
          //Response.End();
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("1");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_Incident_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Incident", "7");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Incident_FacilityType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_FacilityType.SelectCommand = "SELECT Facility_Type_Lookup_Id , Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup ORDER BY Facility_Type_Lookup_Name";

      SqlDataSource_Incident_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Incident_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_Facility.SelectParameters.Clear();
      SqlDataSource_Incident_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Incident_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_Incident_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "Form_Incident");
      SqlDataSource_Incident_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_UnitToUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_UnitToUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Incident_UnitToUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_UnitToUnit.SelectParameters.Clear();
      SqlDataSource_Incident_UnitToUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_UnitToUnit.SelectParameters.Add("Form_Id", TypeCode.String, "1");
      SqlDataSource_Incident_UnitToUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_Incident_UnitToUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Incident_UnitToUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Incident_UnitToUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Incident_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_List.SelectCommand = "spForm_Get_Incident_List";
      SqlDataSource_Incident_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_List.CancelSelectOnNullParameter = false;
      SqlDataSource_Incident_List.SelectParameters.Clear();
      SqlDataSource_Incident_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_List.SelectParameters.Add("FacilityType", TypeCode.String, Request.QueryString["s_Facility_Type"]);
      SqlDataSource_Incident_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Incident_List.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_Incident_ReportNumber"]);
      SqlDataSource_Incident_List.SelectParameters.Add("UnitToUnit", TypeCode.String, Request.QueryString["s_Incident_UnitToUnit"]);
      SqlDataSource_Incident_List.SelectParameters.Add("Status", TypeCode.String, Request.QueryString["s_Incident_Status"]);
      SqlDataSource_Incident_List.SelectParameters.Add("InvestigationCompleted", TypeCode.String, Request.QueryString["s_Incident_InvestigationCompleted"]);
      SqlDataSource_Incident_List.SelectParameters.Add("StatusDateFrom", TypeCode.String, Request.QueryString["s_Incident_StatusDateFrom"]);
      SqlDataSource_Incident_List.SelectParameters.Add("StatusDateTo", TypeCode.String, Request.QueryString["s_Incident_StatusDateTo"]);

      SqlDataSource_Incident_BulkApproval.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Incident_BulkApproval.SelectCommand = "spForm_Get_Incident_BulkApproval";
      SqlDataSource_Incident_BulkApproval.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Incident_BulkApproval.UpdateCommand = "UPDATE Form_Incident SET Incident_Status = @Incident_Status , Incident_StatusDate = @Incident_StatusDate , Incident_StatusRejectedReason = @Incident_StatusRejectedReason , Incident_ModifiedBy = @Incident_ModifiedBy , Incident_ModifiedDate = @Incident_ModifiedDate , Incident_History = @Incident_History WHERE Incident_Id = @Incident_Id";
      SqlDataSource_Incident_BulkApproval.CancelSelectOnNullParameter = false;
      SqlDataSource_Incident_BulkApproval.SelectParameters.Clear();
      SqlDataSource_Incident_BulkApproval.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Incident_BulkApproval.SelectParameters.Add("FacilityType", TypeCode.String, Request.QueryString["s_Facility_Type"]);
      SqlDataSource_Incident_BulkApproval.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Incident_BulkApproval.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_Incident_ReportNumber"]);
      SqlDataSource_Incident_BulkApproval.SelectParameters.Add("UnitToUnit", TypeCode.String, Request.QueryString["s_Incident_UnitToUnit"]);
      SqlDataSource_Incident_BulkApproval.SelectParameters.Add("Status", TypeCode.String, Request.QueryString["s_Incident_Status"]);
      SqlDataSource_Incident_BulkApproval.SelectParameters.Add("InvestigationCompleted", TypeCode.String, Request.QueryString["s_Incident_InvestigationCompleted"]);
      SqlDataSource_Incident_BulkApproval.SelectParameters.Add("StatusDateFrom", TypeCode.String, Request.QueryString["s_Incident_StatusDateFrom"]);
      SqlDataSource_Incident_BulkApproval.SelectParameters.Add("StatusDateTo", TypeCode.String, Request.QueryString["s_Incident_StatusDateTo"]);
      SqlDataSource_Incident_BulkApproval.UpdateParameters.Clear();
      SqlDataSource_Incident_BulkApproval.UpdateParameters.Add("Incident_Status", TypeCode.String, "");
      SqlDataSource_Incident_BulkApproval.UpdateParameters.Add("Incident_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_Incident_BulkApproval.UpdateParameters.Add("Incident_StatusRejectedReason", TypeCode.String, "");
      SqlDataSource_Incident_BulkApproval.UpdateParameters.Add("Incident_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Incident_BulkApproval.UpdateParameters.Add("Incident_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Incident_BulkApproval.UpdateParameters.Add("Incident_History", TypeCode.String, "");
      SqlDataSource_Incident_BulkApproval.UpdateParameters.Add("Incident_Id", TypeCode.Int32, "");
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_FacilityType.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Facility_Type"] == null)
        {
          DropDownList_FacilityType.SelectedValue = "";
        }
        else
        {
          DropDownList_FacilityType.SelectedValue = Request.QueryString["s_Facility_Type"];

          DropDownList_Facility.Items.Clear();
          SqlDataSource_Incident_Facility.SelectParameters["Facility_Type"].DefaultValue = Request.QueryString["s_Facility_Type"];
          DropDownList_Facility.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
          DropDownList_Facility.DataBind();

          DropDownList_UnitToUnit.Items.Clear();
          SqlDataSource_Incident_UnitToUnit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
          DropDownList_UnitToUnit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
          DropDownList_UnitToUnit.DataBind();
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Facility_Id"] == null)
        {
          DropDownList_Facility.SelectedValue = "";
        }
        else
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];

          DropDownList_UnitToUnit.Items.Clear();
          SqlDataSource_Incident_UnitToUnit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
          DropDownList_UnitToUnit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
          DropDownList_UnitToUnit.DataBind();
        }
      }

      if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Incident_ReportNumber"]))
        {
          TextBox_ReportNumber.Text = "";
        }
        else
        {
          TextBox_ReportNumber.Text = Request.QueryString["s_Incident_ReportNumber"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_UnitToUnit.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Incident_UnitToUnit"]))
        {
          DropDownList_UnitToUnit.SelectedValue = "";
        }
        else
        {
          DropDownList_UnitToUnit.SelectedValue = Request.QueryString["s_Incident_UnitToUnit"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Status.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Incident_Status"]))
        {
          DropDownList_Status.SelectedValue = "";
        }
        else
        {
          DropDownList_Status.SelectedValue = Request.QueryString["s_Incident_Status"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_InvestigationCompleted.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Incident_InvestigationCompleted"]))
        {
          DropDownList_InvestigationCompleted.SelectedValue = "";
        }
        else
        {
          DropDownList_InvestigationCompleted.SelectedValue = Request.QueryString["s_Incident_InvestigationCompleted"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_StatusDateFrom.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Incident_StatusDateFrom"]))
        {
          TextBox_StatusDateFrom.Text = "";
        }
        else
        {
          TextBox_StatusDateFrom.Text = Request.QueryString["s_Incident_StatusDateFrom"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_StatusDateTo.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Incident_StatusDateTo"]))
        {
          TextBox_StatusDateTo.Text = "";
        }
        else
        {
          TextBox_StatusDateTo.Text = Request.QueryString["s_Incident_StatusDateTo"];
        }
      }
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('1'))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '20'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '131'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '3'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '132'");
            DataRow[] SecurityFacilityPharmacyManager = DataTable_FormMode.Select("SecurityRole_Id = '189'");
            DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '2'");
            DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '70'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityPharmacyManager.Length > 0 || SecurityFacilityApprover.Length > 0))
            {
              Session["Security"] = "0";

              TableBulkApproval.Visible = true;
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityCapturer.Length > 0))
            {
              Session["Security"] = "0";

              TableBulkApproval.Visible = false;
            }
            Session["Security"] = "1";
          }
          else
          {
            TableBulkApproval.Visible = false;
          }
        }
      }
    }


    //--START-- --Search--//
    protected void DropDownList_FacilityType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(DropDownList_FacilityType.SelectedValue))
      {
        DropDownList_Facility.Items.Clear();
        SqlDataSource_Incident_Facility.SelectParameters["Facility_Type"].DefaultValue = "0";
        DropDownList_Facility.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
        DropDownList_Facility.DataBind();
      }
      else
      {
        DropDownList_Facility.Items.Clear();
        SqlDataSource_Incident_Facility.SelectParameters["Facility_Type"].DefaultValue = DropDownList_FacilityType.SelectedValue;
        DropDownList_Facility.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
        DropDownList_Facility.DataBind();
      }

      DropDownList_UnitToUnit.Items.Clear();
      SqlDataSource_Incident_UnitToUnit.SelectParameters["Facility_Id"].DefaultValue = "";
      DropDownList_UnitToUnit.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_UnitToUnit.DataBind();
    }

    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_UnitToUnit.Items.Clear();
      SqlDataSource_Incident_UnitToUnit.SelectParameters["Facility_Id"].DefaultValue = DropDownList_Facility.SelectedValue;
      DropDownList_UnitToUnit.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_UnitToUnit.DataBind();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchErrorMessage = "";
      string ValidSearch = "Yes";

      if (!string.IsNullOrEmpty(TextBox_StatusDateFrom.Text.ToString()))
      {
        string DateToValidate = TextBox_StatusDateFrom.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          SearchErrorMessage = SearchErrorMessage + "Form Status Date From is not in the correct format,<br />date must be in the format yyyy/mm/dd<br />";
          ValidSearch = "No";
        }
      }

      if (!string.IsNullOrEmpty(TextBox_StatusDateTo.Text.ToString()))
      {
        string DateToValidate = TextBox_StatusDateTo.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          SearchErrorMessage = SearchErrorMessage + "Form Status Date To is not in the correct format,<br />date must be in the format yyyy/mm/dd<br />";
          ValidSearch = "No";
        }
      }

      if (ValidSearch == "No")
      {
        Label_SearchErrorMessage.Text = Convert.ToString(SearchErrorMessage, CultureInfo.CurrentCulture);
      }
      else
      {
        Label_SearchErrorMessage.Text = "";

        string SearchField1 = DropDownList_FacilityType.SelectedValue;
        string SearchField2 = DropDownList_Facility.SelectedValue;
        string SearchField3 = Server.HtmlEncode(TextBox_ReportNumber.Text);
        string SearchField4 = DropDownList_UnitToUnit.SelectedValue;
        string SearchField5 = DropDownList_Status.SelectedValue;
        string SearchField6 = DropDownList_InvestigationCompleted.SelectedValue;
        string SearchField7 = Server.HtmlEncode(TextBox_StatusDateFrom.Text);
        string SearchField8 = Server.HtmlEncode(TextBox_StatusDateTo.Text);

        if (!string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "s_Facility_Type=" + DropDownList_FacilityType.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "s_Incident_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "s_Incident_UnitToUnit=" + DropDownList_UnitToUnit.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "s_Incident_Status=" + DropDownList_Status.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField6))
        {
          SearchField6 = "s_Incident_InvestigationCompleted=" + DropDownList_InvestigationCompleted.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField7))
        {
          SearchField7 = "s_Incident_StatusDateFrom=" + Server.HtmlEncode(TextBox_StatusDateFrom.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField8))
        {
          SearchField8 = "s_Incident_StatusDateTo=" + Server.HtmlEncode(TextBox_StatusDateTo.Text.ToString()) + "&";
        }


        string FinalURL = "Form_Incident_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7 + SearchField8;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident Captured Forms", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident List", "Form_Incident_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_Incident_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_Incident_List.PageSize = Convert.ToInt32(((DropDownList)GridView_Incident_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);

      Session["GridViewIncidentList_DropDownListPageSize"] = Convert.ToInt32(((DropDownList)GridView_Incident_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_PageSize_DataBinding(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewIncidentList_DropDownListPageSize"] != null)
        {
          GridView_Incident_List.PageSize = Convert.ToInt32(Session["GridViewIncidentList_DropDownListPageSize"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_Incident_List.PageIndex = ((DropDownList)GridView_Incident_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;

      Session["GridViewIncidentList_DropDownListPage"] = ((DropDownList)GridView_Incident_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void DropDownList_Page_DataBinding(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewIncidentList_DropDownListPage"] != null)
        {
          GridView_Incident_List.PageIndex = Convert.ToInt32(Session["GridViewIncidentList_DropDownListPage"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void ImageButton_First_Unload(object sender, EventArgs e)
    {
      Session["GridViewIncidentList_DropDownListPage"] = ((DropDownList)GridView_Incident_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Prev_Unload(object sender, EventArgs e)
    {
      Session["GridViewIncidentList_DropDownListPage"] = ((DropDownList)GridView_Incident_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Next_Unload(object sender, EventArgs e)
    {
      Session["GridViewIncidentList_DropDownListPage"] = ((DropDownList)GridView_Incident_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Last_Unload(object sender, EventArgs e)
    {
      Session["GridViewIncidentList_DropDownListPage"] = ((DropDownList)GridView_Incident_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_Incident_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Incident_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Incident_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Incident_List.PageSize > 20 && GridView_Incident_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Incident_List.PageSize > 50 && GridView_Incident_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_Incident_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_Incident_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Incident_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Incident_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_Incident_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident New Form", "Form_Incident.aspx"), false);
    }

    public string GetLink(object incident_Id, object facility_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident New Form", "Form_Incident.aspx?s_Facility_Id=" + facility_Id + "&Incident_Id=" + incident_Id + "") + "'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Incident New Form", "Form_Incident.aspx?s_Facility_Id=" + facility_Id + "&Incident_Id=" + incident_Id + "") + "'>View</a>";
        }
      }

      string SearchField1 = Request.QueryString["s_Facility_Type"];
      string SearchField2 = Request.QueryString["s_Facility_Id"];
      string SearchField3 = Request.QueryString["s_Incident_ReportNumber"];
      string SearchField4 = Request.QueryString["s_Incident_UnitToUnit"];
      string SearchField5 = Request.QueryString["s_Incident_Status"];
      string SearchField6 = Request.QueryString["s_Incident_InvestigationCompleted"];
      string SearchField7 = Request.QueryString["s_Incident_StatusDateFrom"];
      string SearchField8 = Request.QueryString["s_Incident_StatusDateTo"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_FacilityType=" + Request.QueryString["s_Facility_Type"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_IncidentReportNumber=" + Request.QueryString["s_Incident_ReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_IncidentUnitToUnit=" + Request.QueryString["s_Incident_UnitToUnit"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "Search_IncidentStatus=" + Request.QueryString["s_Incident_Status"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "Search_IncidentInvestigationCompleted=" + Request.QueryString["s_Incident_InvestigationCompleted"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "Search_IncidentStatusDateFrom=" + Request.QueryString["s_Incident_StatusDateFrom"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField8))
      {
        SearchField8 = "Search_IncidentStatusDateTo=" + Request.QueryString["s_Incident_StatusDateTo"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7 + SearchField8;
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


    //--START-- --Bulk Approval--//
    protected void SqlDataSource_Incident_BulkApproval_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_BulkApproval.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged_BulkApproval(Object sender, EventArgs e)
    {
      GridView_Incident_BulkApproval.PageSize = Convert.ToInt32(((DropDownList)GridView_Incident_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);

      Session["GridViewIncidentBulkApproval_DropDownListPageSize"] = Convert.ToInt32(((DropDownList)GridView_Incident_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_PageSize_DataBinding_BulkApproval(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewIncidentBulkApproval_DropDownListPageSize"] != null)
        {
          GridView_Incident_BulkApproval.PageSize = Convert.ToInt32(Session["GridViewIncidentBulkApproval_DropDownListPageSize"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DropDownList_Page_SelectedIndexChanged_BulkApproval(Object sender, EventArgs e)
    {
      GridView_Incident_BulkApproval.PageIndex = ((DropDownList)GridView_Incident_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;

      Session["GridViewIncidentBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Incident_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void DropDownList_Page_DataBinding_BulkApproval(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewIncidentBulkApproval_DropDownListPage"] != null)
        {
          GridView_Incident_BulkApproval.PageIndex = Convert.ToInt32(Session["GridViewIncidentBulkApproval_DropDownListPage"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void ImageButton_First_Unload_BulkApproval(object sender, EventArgs e)
    {
      Session["GridViewIncidentBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Incident_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Prev_Unload_BulkApproval(object sender, EventArgs e)
    {
      Session["GridViewIncidentBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Incident_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Next_Unload_BulkApproval(object sender, EventArgs e)
    {
      Session["GridViewIncidentBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Incident_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Last_Unload_BulkApproval(object sender, EventArgs e)
    {
      Session["GridViewIncidentBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Incident_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_Incident_BulkApproval_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Incident_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Incident_BulkApproval.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Incident_BulkApproval.PageSize > 20 && GridView_Incident_BulkApproval.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Incident_BulkApproval.PageSize > 50 && GridView_Incident_BulkApproval.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_Incident_BulkApproval_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_Incident_BulkApproval.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Incident_BulkApproval.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Incident_BulkApproval.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_Incident_BulkApproval_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void DropDownList_UpdateStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_UpdateStatus = (DropDownList)sender;
      GridViewRow GridViewRow_Status = (GridViewRow)DropDownList_UpdateStatus.NamingContainer;
      TextBox TextBox_UpdateStatusRejectedReason = (TextBox)GridViewRow_Status.FindControl("TextBox_UpdateStatusRejectedReason");
      Label Label_UpdateStatusRejectedLabel = (Label)GridViewRow_Status.FindControl("Label_UpdateStatusRejectedLabel");
      Label Label_UpdateStatusRejectedMessage = (Label)GridViewRow_Status.FindControl("Label_UpdateStatusRejectedMessage");

      if (DropDownList_UpdateStatus.SelectedValue == "Rejected")
      {
        Label_UpdateStatusRejectedLabel.Visible = true;
        TextBox_UpdateStatusRejectedReason.Visible = true;
        Label_UpdateStatusRejectedMessage.Visible = true;
      }
      else
      {
        Label_UpdateStatusRejectedLabel.Visible = false;
        TextBox_UpdateStatusRejectedReason.Visible = false;
        Label_UpdateStatusRejectedMessage.Visible = false;
      }
    }

    protected void Button_ApproveAll_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_Incident_BulkApproval.Rows.Count; i++)
      {
        DropDownList DropDownList_UpdateStatus = (DropDownList)GridView_Incident_BulkApproval.Rows[i].Cells[0].FindControl("DropDownList_UpdateStatus");

        DropDownList_UpdateStatus.SelectedValue = "Approved";
      }

      Button_Update_Click(sender, e);
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_Incident_List.SetFocus(ImageButton_BulkApproval);

      string Proceed = "Yes";

      for (int i = 0; i < GridView_Incident_BulkApproval.Rows.Count; i++)
      {
        if (Proceed == "Yes")
        {
          HiddenField HiddenField_UpdateId = (HiddenField)GridView_Incident_BulkApproval.Rows[i].Cells[0].FindControl("HiddenField_UpdateId");
          DropDownList DropDownList_UpdateStatus = (DropDownList)GridView_Incident_BulkApproval.Rows[i].Cells[0].FindControl("DropDownList_UpdateStatus");
          TextBox TextBox_UpdateStatusRejectedReason = (TextBox)GridView_Incident_BulkApproval.Rows[i].Cells[0].FindControl("TextBox_UpdateStatusRejectedReason");
          Label Label_UpdateStatusRejectedMessage = (Label)GridView_Incident_BulkApproval.Rows[i].Cells[0].FindControl("Label_UpdateStatusRejectedMessage");

          if (DropDownList_UpdateStatus.SelectedValue == "Approved")
          {
            string SQLStringUpdateApproved = "UPDATE Form_Incident SET Incident_Status = @Incident_Status , Incident_StatusDate = @Incident_StatusDate , Incident_ModifiedBy = @Incident_ModifiedBy , Incident_ModifiedDate = @Incident_ModifiedDate , Incident_History = @Incident_History WHERE Incident_Id = @Incident_Id";
            using (SqlCommand SqlCommand_UpdateApproved = new SqlCommand(SQLStringUpdateApproved))
            {
              Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Incident", "Incident_Id = " + HiddenField_UpdateId.Value.ToString());

              DataView DataView_Incident = (DataView)SqlDataSource_Incident_BulkApproval.Select(DataSourceSelectArguments.Empty);
              if (DataView_Incident.Table.Rows.Count != 0)
              {
                DataRowView DataRowView_Incident = DataView_Incident[0];
                Session["IncidentHistory"] = Convert.ToString(DataRowView_Incident["Incident_History"], CultureInfo.CurrentCulture);

                Session["IncidentHistory"] = Session["History"].ToString() + Session["IncidentHistory"].ToString();

                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_Status", DropDownList_UpdateStatus.SelectedValue.ToString());
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_StatusDate", DateTime.Now);
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_ModifiedDate", DateTime.Now);
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_History", Session["IncidentHistory"].ToString());
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_Id", HiddenField_UpdateId.Value.ToString());

                Session["IncidentHistory"] = "";
                Session["History"] = "";

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateApproved);
              }

              Session["IncidentHistory"] = "";
              Session["History"] = "";
            }

            GridView_Incident_BulkApproval.Rows[i].Visible = false;
          }
          else if (DropDownList_UpdateStatus.SelectedValue == "Rejected")
          {
            if (string.IsNullOrEmpty(TextBox_UpdateStatusRejectedReason.Text))
            {
              Proceed = "No";
              Label_UpdateStatusRejectedMessage.Visible = true;
            }
            else
            {
              Label_UpdateStatusRejectedMessage.Visible = false;

              string SQLStringUpdateApproved = "UPDATE Form_Incident SET Incident_Status = @Incident_Status , Incident_StatusDate = @Incident_StatusDate , Incident_StatusRejectedReason = @Incident_StatusRejectedReason , Incident_ModifiedBy = @Incident_ModifiedBy , Incident_ModifiedDate = @Incident_ModifiedDate , Incident_History = @Incident_History WHERE Incident_Id = @Incident_Id";
              using (SqlCommand SqlCommand_UpdateApproved = new SqlCommand(SQLStringUpdateApproved))
              {
                Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Incident", "Incident_Id = " + HiddenField_UpdateId.Value.ToString());

                DataView DataView_Incident = (DataView)SqlDataSource_Incident_BulkApproval.Select(DataSourceSelectArguments.Empty);
                if (DataView_Incident.Table.Rows.Count != 0)
                {
                  DataRowView DataRowView_Incident = DataView_Incident[0];
                  Session["IncidentHistory"] = Convert.ToString(DataRowView_Incident["Incident_History"], CultureInfo.CurrentCulture);

                  Session["IncidentHistory"] = Session["History"].ToString() + Session["IncidentHistory"].ToString();

                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_Status", DropDownList_UpdateStatus.SelectedValue.ToString());
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_StatusDate", DateTime.Now);
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_StatusRejectedReason", TextBox_UpdateStatusRejectedReason.Text.ToString());
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_ModifiedDate", DateTime.Now);
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_History", Session["IncidentHistory"].ToString());
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Incident_Id", HiddenField_UpdateId.Value.ToString());

                  Session["IncidentHistory"] = "";
                  Session["History"] = "";

                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateApproved);
                }

                Session["IncidentHistory"] = "";
                Session["History"] = "";
              }

              GridView_Incident_BulkApproval.Rows[i].Visible = false;
            }
          }
        }
      }

      if (Proceed == "Yes")
      {
        GridView_Incident_BulkApproval.DataBind();
        GridView_Incident_List.DataBind();
      }      
    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
      GridView_Incident_BulkApproval.DataBind();
      GridView_Incident_List.DataBind();
    }
    //---END--- --Bulk Approval--//
  }
}