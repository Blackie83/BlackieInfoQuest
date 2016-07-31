using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Alert_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("2").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("2").Replace(" Form", "")).ToString() + "s", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("2").Replace(" Form", "")).ToString() + "s", CultureInfo.CurrentCulture);
          Label_BulkApprovalHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("2").Replace(" Form", "")).ToString() + " : Bulk Approval", CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('2'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("2");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_Alert_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Alert", "11");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Alert_FacilityType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_FacilityType.SelectCommand = "SELECT Facility_Type_Lookup_Id , Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup ORDER BY Facility_Type_Lookup_Name";

      SqlDataSource_Alert_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Alert_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_Facility.SelectParameters.Clear();
      SqlDataSource_Alert_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Alert_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Alert_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_Alert_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "InfoQuest_Form_Alert");
      SqlDataSource_Alert_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_UnitToUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_UnitToUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Alert_UnitToUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_UnitToUnit.SelectParameters.Clear();
      SqlDataSource_Alert_UnitToUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Alert_UnitToUnit.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_UnitToUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_Alert_UnitToUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_UnitToUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_UnitToUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_List.SelectCommand = "spForm_Get_Alert_List";
      SqlDataSource_Alert_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_List.CancelSelectOnNullParameter = false;
      SqlDataSource_Alert_List.SelectParameters.Clear();
      SqlDataSource_Alert_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Alert_List.SelectParameters.Add("FacilityType", TypeCode.String, Request.QueryString["s_Facility_Type"]);
      SqlDataSource_Alert_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Alert_List.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_Alert_ReportNumber"]);
      SqlDataSource_Alert_List.SelectParameters.Add("UnitToUnit", TypeCode.String, Request.QueryString["s_Alert_UnitToUnit"]);
      SqlDataSource_Alert_List.SelectParameters.Add("Status", TypeCode.String, Request.QueryString["s_Alert_Status"]);
      SqlDataSource_Alert_List.SelectParameters.Add("StatusDateFrom", TypeCode.String, Request.QueryString["s_Alert_StatusDateFrom"]);
      SqlDataSource_Alert_List.SelectParameters.Add("StatusDateTo", TypeCode.String, Request.QueryString["s_Alert_StatusDateTo"]);

      SqlDataSource_Alert_BulkApproval.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_BulkApproval.SelectCommand = "spForm_Get_Alert_BulkApproval";
      SqlDataSource_Alert_BulkApproval.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_BulkApproval.UpdateCommand = "UPDATE InfoQuest_Form_Alert SET Alert_Status = @Alert_Status , Alert_StatusDate = @Alert_StatusDate , Alert_StatusRejectedReason = @Alert_StatusRejectedReason , Alert_ModifiedBy = @Alert_ModifiedBy , Alert_ModifiedDate = @Alert_ModifiedDate , Alert_History = @Alert_History WHERE Alert_Id = @Alert_Id";
      SqlDataSource_Alert_BulkApproval.CancelSelectOnNullParameter = false;
      SqlDataSource_Alert_BulkApproval.SelectParameters.Clear();
      SqlDataSource_Alert_BulkApproval.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Alert_BulkApproval.SelectParameters.Add("FacilityType", TypeCode.String, Request.QueryString["s_Facility_Type"]);
      SqlDataSource_Alert_BulkApproval.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Alert_BulkApproval.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_Alert_ReportNumber"]);
      SqlDataSource_Alert_BulkApproval.SelectParameters.Add("UnitToUnit", TypeCode.String, Request.QueryString["s_Alert_UnitToUnit"]);
      SqlDataSource_Alert_BulkApproval.SelectParameters.Add("Status", TypeCode.String, Request.QueryString["s_Alert_Status"]);
      SqlDataSource_Alert_BulkApproval.SelectParameters.Add("StatusDateFrom", TypeCode.String, Request.QueryString["s_Alert_StatusDateFrom"]);
      SqlDataSource_Alert_BulkApproval.SelectParameters.Add("StatusDateTo", TypeCode.String, Request.QueryString["s_Alert_StatusDateTo"]);
      SqlDataSource_Alert_BulkApproval.UpdateParameters.Clear();
      SqlDataSource_Alert_BulkApproval.UpdateParameters.Add("Alert_Status", TypeCode.String, "");
      SqlDataSource_Alert_BulkApproval.UpdateParameters.Add("Alert_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_Alert_BulkApproval.UpdateParameters.Add("Alert_StatusRejectedReason", TypeCode.String, "");
      SqlDataSource_Alert_BulkApproval.UpdateParameters.Add("Alert_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Alert_BulkApproval.UpdateParameters.Add("Alert_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Alert_BulkApproval.UpdateParameters.Add("Alert_History", TypeCode.String, "");
      SqlDataSource_Alert_BulkApproval.UpdateParameters.Add("Alert_Id", TypeCode.Int32, "");
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
          SqlDataSource_Alert_Facility.SelectParameters["Facility_Type"].DefaultValue = Request.QueryString["s_Facility_Type"];
          DropDownList_Facility.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
          DropDownList_Facility.DataBind();

          DropDownList_UnitToUnit.Items.Clear();
          SqlDataSource_Alert_UnitToUnit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
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
          SqlDataSource_Alert_UnitToUnit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
          DropDownList_UnitToUnit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
          DropDownList_UnitToUnit.DataBind();
        }
      }

      if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
      {
        if (Request.QueryString["s_Alert_ReportNumber"] == null)
        {
          TextBox_ReportNumber.Text = "";
        }
        else
        {
          TextBox_ReportNumber.Text = Request.QueryString["s_Alert_ReportNumber"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_UnitToUnit.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Alert_UnitToUnit"] == null)
        {
          DropDownList_UnitToUnit.SelectedValue = "";
        }
        else
        {
          DropDownList_UnitToUnit.SelectedValue = Request.QueryString["s_Alert_UnitToUnit"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Status.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Alert_Status"] == null)
        {
          DropDownList_Status.SelectedValue = "";
        }
        else
        {
          DropDownList_Status.SelectedValue = Request.QueryString["s_Alert_Status"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_StatusDateFrom.Text.ToString()))
      {
        if (Request.QueryString["s_Alert_StatusDateFrom"] == null)
        {
          TextBox_StatusDateFrom.Text = "";
        }
        else
        {
          TextBox_StatusDateFrom.Text = Request.QueryString["s_Alert_StatusDateFrom"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_StatusDateTo.Text.ToString()))
      {
        if (Request.QueryString["s_Alert_StatusDateTo"] == null)
        {
          TextBox_StatusDateTo.Text = "";
        }
        else
        {
          TextBox_StatusDateTo.Text = Request.QueryString["s_Alert_StatusDateTo"];
        }
      }
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('2'))";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '21'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '133'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '6'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '134'");
            DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '5'");
            DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '69'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityApprover.Length > 0))
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
        SqlDataSource_Alert_Facility.SelectParameters["Facility_Type"].DefaultValue = "0";
        DropDownList_Facility.Items.Insert(0, new ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
        DropDownList_Facility.DataBind();
      }
      else
      {
        DropDownList_Facility.Items.Clear();
        SqlDataSource_Alert_Facility.SelectParameters["Facility_Type"].DefaultValue = DropDownList_FacilityType.SelectedValue;
        DropDownList_Facility.Items.Insert(0, new ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
        DropDownList_Facility.DataBind();
      }

      DropDownList_UnitToUnit.Items.Clear();
      SqlDataSource_Alert_UnitToUnit.SelectParameters["Facility_Id"].DefaultValue = "";
      DropDownList_UnitToUnit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
      DropDownList_UnitToUnit.DataBind();
    }

    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList_UnitToUnit.Items.Clear();
      SqlDataSource_Alert_UnitToUnit.SelectParameters["Facility_Id"].DefaultValue = DropDownList_Facility.SelectedValue;
      DropDownList_UnitToUnit.Items.Insert(0, new ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
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
        string SearchField6 = Server.HtmlEncode(TextBox_StatusDateFrom.Text);
        string SearchField7 = Server.HtmlEncode(TextBox_StatusDateTo.Text);

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
          SearchField3 = "s_Alert_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "s_Alert_UnitToUnit=" + DropDownList_UnitToUnit.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "s_Alert_Status=" + DropDownList_Status.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField6))
        {
          SearchField6 = "s_Alert_StatusDateFrom=" + Server.HtmlEncode(TextBox_StatusDateFrom.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField7))
        {
          SearchField7 = "s_Alert_StatusDateTo=" + Server.HtmlEncode(TextBox_StatusDateTo.Text.ToString()) + "&";
        }

        string FinalURL = "Form_Alert_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert Captured Forms", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert List", "Form_Alert_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_Alert_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_Alert_List.PageSize = Convert.ToInt32(((DropDownList)GridView_Alert_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);

      Session["GridViewAlertList_DropDownListPageSize"] = Convert.ToInt32(((DropDownList)GridView_Alert_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_PageSize_DataBinding(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewAlertList_DropDownListPageSize"] != null)
        {
          GridView_Alert_List.PageSize = Convert.ToInt32(Session["GridViewAlertList_DropDownListPageSize"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_Alert_List.PageIndex = ((DropDownList)GridView_Alert_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;

      Session["GridViewAlertList_DropDownListPage"] = ((DropDownList)GridView_Alert_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void DropDownList_Page_DataBinding(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewAlertList_DropDownListPage"] != null)
        {
          GridView_Alert_List.PageIndex = Convert.ToInt32(Session["GridViewAlertList_DropDownListPage"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void ImageButton_First_Unload(object sender, EventArgs e)
    {
      Session["GridViewAlertList_DropDownListPage"] = ((DropDownList)GridView_Alert_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Prev_Unload(object sender, EventArgs e)
    {
      Session["GridViewAlertList_DropDownListPage"] = ((DropDownList)GridView_Alert_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Next_Unload(object sender, EventArgs e)
    {
      Session["GridViewAlertList_DropDownListPage"] = ((DropDownList)GridView_Alert_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Last_Unload(object sender, EventArgs e)
    {
      Session["GridViewAlertList_DropDownListPage"] = ((DropDownList)GridView_Alert_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_Alert_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Alert_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Alert_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Alert_List.PageSize > 20 && GridView_Alert_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Alert_List.PageSize > 50 && GridView_Alert_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_Alert_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_Alert_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Alert_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Alert_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_Alert_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert New Form", "Form_Alert.aspx"), false);
    }

    public string GetLink(object alert_Id, object facility_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert New Form", "Form_Alert.aspx?s_Facility_Id=" + facility_Id + "&Alert_Id=" + alert_Id + "") + "'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert New Form", "Form_Alert.aspx?s_Facility_Id=" + facility_Id + "&Alert_Id=" + alert_Id + "") + "'>View</a>";
        }
      }

      string SearchField1 = Request.QueryString["s_Facility_Type"];
      string SearchField2 = Request.QueryString["s_Facility_Id"];
      string SearchField3 = Request.QueryString["s_Alert_ReportNumber"];
      string SearchField4 = Request.QueryString["s_Alert_UnitToUnit"];
      string SearchField5 = Request.QueryString["s_Alert_Status"];
      string SearchField6 = Request.QueryString["s_Alert_StatusDateFrom"];
      string SearchField7 = Request.QueryString["s_Alert_StatusDateTo"];

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
        SearchField3 = "Search_AlertReportNumber=" + Request.QueryString["s_Alert_ReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_AlertUnitToUnit=" + Request.QueryString["s_Alert_UnitToUnit"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "Search_AlertStatus=" + Request.QueryString["s_Alert_Status"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "Search_AlertStatusDateFrom=" + Request.QueryString["s_Alert_StatusDateFrom"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "Search_AlertStatusDateTo=" + Request.QueryString["s_Alert_StatusDateTo"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7;
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
    protected void SqlDataSource_Alert_BulkApproval_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_BulkApproval.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged_BulkApproval(object sender, EventArgs e)
    {
      GridView_Alert_BulkApproval.PageSize = Convert.ToInt32(((DropDownList)GridView_Alert_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);

      Session["GridViewAlertBulkApproval_DropDownListPageSize"] = Convert.ToInt32(((DropDownList)GridView_Alert_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_PageSize_DataBinding_BulkApproval(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewAlertBulkApproval_DropDownListPageSize"] != null)
        {
          GridView_Alert_BulkApproval.PageSize = Convert.ToInt32(Session["GridViewAlertBulkApproval_DropDownListPageSize"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DropDownList_Page_SelectedIndexChanged_BulkApproval(object sender, EventArgs e)
    {
      GridView_Alert_BulkApproval.PageIndex = ((DropDownList)GridView_Alert_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;

      Session["GridViewAlertBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Alert_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void DropDownList_Page_DataBinding_BulkApproval(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (Session["GridViewAlertBulkApproval_DropDownListPage"] != null)
        {
          GridView_Alert_BulkApproval.PageIndex = Convert.ToInt32(Session["GridViewAlertBulkApproval_DropDownListPage"], CultureInfo.CurrentCulture);
        }
      }
    }

    protected void ImageButton_First_Unload_BulkApproval(object sender, EventArgs e)
    {
      Session["GridViewAlertBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Alert_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Prev_Unload_BulkApproval(object sender, EventArgs e)
    {
      Session["GridViewAlertBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Alert_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Next_Unload_BulkApproval(object sender, EventArgs e)
    {
      Session["GridViewAlertBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Alert_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void ImageButton_Last_Unload_BulkApproval(object sender, EventArgs e)
    {
      Session["GridViewAlertBulkApproval_DropDownListPage"] = ((DropDownList)GridView_Alert_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_Alert_BulkApproval_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Alert_BulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Alert_BulkApproval.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Alert_BulkApproval.PageSize > 20 && GridView_Alert_BulkApproval.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Alert_BulkApproval.PageSize > 50 && GridView_Alert_BulkApproval.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_Alert_BulkApproval_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_Alert_BulkApproval.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Alert_BulkApproval.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Alert_BulkApproval.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_Alert_BulkApproval_RowCreated(object sender, GridViewRowEventArgs e)
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
      for (int i = 0; i < GridView_Alert_BulkApproval.Rows.Count; i++)
      {
        DropDownList DropDownList_UpdateStatus = (DropDownList)GridView_Alert_BulkApproval.Rows[i].Cells[0].FindControl("DropDownList_UpdateStatus");

        DropDownList_UpdateStatus.SelectedValue = "Approved";
      }

      Button_Update_Click(sender, e);
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_Alert_List.SetFocus(ImageButton_BulkApproval);

      string Proceed = "Yes";

      for (int i = 0; i < GridView_Alert_BulkApproval.Rows.Count; i++)
      {
        if (Proceed == "Yes")
        {
          HiddenField HiddenField_UpdateId = (HiddenField)GridView_Alert_BulkApproval.Rows[i].Cells[0].FindControl("HiddenField_UpdateId");
          DropDownList DropDownList_UpdateStatus = (DropDownList)GridView_Alert_BulkApproval.Rows[i].Cells[0].FindControl("DropDownList_UpdateStatus");
          TextBox TextBox_UpdateStatusRejectedReason = (TextBox)GridView_Alert_BulkApproval.Rows[i].Cells[0].FindControl("TextBox_UpdateStatusRejectedReason");
          Label Label_UpdateStatusRejectedMessage = (Label)GridView_Alert_BulkApproval.Rows[i].Cells[0].FindControl("Label_UpdateStatusRejectedMessage");

          if (DropDownList_UpdateStatus.SelectedValue == "Approved")
          {
            string SQLStringUpdateApproved = "UPDATE InfoQuest_Form_Alert SET Alert_Status = @Alert_Status , Alert_StatusDate = @Alert_StatusDate , Alert_ModifiedBy = @Alert_ModifiedBy , Alert_ModifiedDate = @Alert_ModifiedDate , Alert_History = @Alert_History WHERE Alert_Id = @Alert_Id";
            using (SqlCommand SqlCommand_UpdateApproved = new SqlCommand(SQLStringUpdateApproved))
            {
              Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Alert", "Alert_Id = " + HiddenField_UpdateId.Value.ToString());

              DataView DataView_Alert = (DataView)SqlDataSource_Alert_BulkApproval.Select(DataSourceSelectArguments.Empty);
              if (DataView_Alert.Table.Rows.Count != 0)
              {
                DataRowView DataRowView_Alert = DataView_Alert[0];
                Session["AlertHistory"] = Convert.ToString(DataRowView_Alert["Alert_History"], CultureInfo.CurrentCulture);

                Session["AlertHistory"] = Session["History"].ToString() + Session["AlertHistory"].ToString();

                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_Status", DropDownList_UpdateStatus.SelectedValue.ToString());
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_StatusDate", DateTime.Now);
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_ModifiedDate", DateTime.Now);
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_History", Session["AlertHistory"].ToString());
                SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_Id", HiddenField_UpdateId.Value.ToString());

                Session["AlertHistory"] = "";
                Session["History"] = "";

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateApproved);
              }

              Session["AlertHistory"] = "";
              Session["History"] = "";
            }
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

              string SQLStringUpdateApproved = "UPDATE InfoQuest_Form_Alert SET Alert_Status = @Alert_Status , Alert_StatusDate = @Alert_StatusDate , Alert_StatusRejectedReason = @Alert_StatusRejectedReason , Alert_ModifiedBy = @Alert_ModifiedBy , Alert_ModifiedDate = @Alert_ModifiedDate , Alert_History = @Alert_History WHERE Alert_Id = @Alert_Id";
              using (SqlCommand SqlCommand_UpdateApproved = new SqlCommand(SQLStringUpdateApproved))
              {
                Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Alert", "Alert_Id = " + HiddenField_UpdateId.Value.ToString());

                DataView DataView_Alert = (DataView)SqlDataSource_Alert_BulkApproval.Select(DataSourceSelectArguments.Empty);
                if (DataView_Alert.Table.Rows.Count != 0)
                {
                  DataRowView DataRowView_Alert = DataView_Alert[0];
                  Session["AlertHistory"] = Convert.ToString(DataRowView_Alert["Alert_History"], CultureInfo.CurrentCulture);

                  Session["AlertHistory"] = Session["History"].ToString() + Session["AlertHistory"].ToString();

                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_Status", DropDownList_UpdateStatus.SelectedValue.ToString());
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_StatusDate", DateTime.Now);
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_StatusRejectedReason", TextBox_UpdateStatusRejectedReason.Text.ToString());
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_ModifiedDate", DateTime.Now);
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_History", Session["AlertHistory"].ToString());
                  SqlCommand_UpdateApproved.Parameters.AddWithValue("@Alert_Id", HiddenField_UpdateId.Value.ToString());

                  Session["AlertHistory"] = "";
                  Session["History"] = "";

                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateApproved);
                }

                Session["AlertHistory"] = "";
                Session["History"] = "";
              }

              GridView_Alert_BulkApproval.Rows[i].Visible = false;
            }
          }
        }
      }

      if (Proceed == "Yes")
      {
        GridView_Alert_BulkApproval.DataBind();
        GridView_Alert_List.DataBind();
      }
    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
      GridView_Alert_BulkApproval.DataBind();
      GridView_Alert_List.DataBind();
    }
    //---END--- --Bulk Approval--//
  }
}