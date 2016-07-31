using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_CRM_IncompleteComplaints : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " : Incomplete Complaints", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_BulkApprovalHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " : Bulk Approval", CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('36'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("36");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_CRM_IncompleteComplaints.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Customer Relationship Management", "4");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_CRM_FacilityType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_FacilityType.SelectCommand = "SELECT Facility_Type_Lookup_Id , Facility_Type_Lookup_Name FROM Administration_Facility_Type_Lookup ORDER BY Facility_Type_Lookup_Name";
      
      SqlDataSource_CRM_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_CRM_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_Facility.SelectParameters.Clear();
      SqlDataSource_CRM_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_CRM_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_CRM_Facility.SelectParameters.Add("TableFROM", TypeCode.String,  "0");
      SqlDataSource_CRM_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_IncompleteComplaints.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_IncompleteComplaints.SelectCommand = "spForm_Get_CRM_IncompleteComplaints";
      SqlDataSource_CRM_IncompleteComplaints.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_IncompleteComplaints.CancelSelectOnNullParameter = false;
      SqlDataSource_CRM_IncompleteComplaints.SelectParameters.Clear();
      SqlDataSource_CRM_IncompleteComplaints.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_IncompleteComplaints.SelectParameters.Add("FacilityType", TypeCode.String, Request.QueryString["s_Facility_Type"]);
      SqlDataSource_CRM_IncompleteComplaints.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_CRM_IncompleteComplaints.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_CRM_ReportNumber"]);
      SqlDataSource_CRM_IncompleteComplaints.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_CRM_PatientVisitNumber"]);
      SqlDataSource_CRM_IncompleteComplaints.SelectParameters.Add("Name", TypeCode.String, Request.QueryString["s_CRM_Name"]);
      SqlDataSource_CRM_IncompleteComplaints.SelectParameters.Add("EscalatedForm", TypeCode.String, Request.QueryString["s_CRM_EscalatedForm"]);
      SqlDataSource_CRM_IncompleteComplaints.SelectParameters.Add("Route", TypeCode.String, Request.QueryString["s_CRM_Route"]);

      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectCommand = "spForm_Get_CRM_IncompleteComplaintsBulkApproval";
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.UpdateCommand = "UPDATE Form_CRM SET CRM_Status = @CRM_Status , CRM_StatusDate = @CRM_StatusDate , CRM_StatusRejectedReason = @CRM_StatusRejectedReason , CRM_ModifiedBy = @CRM_ModifiedBy , CRM_ModifiedDate = @CRM_ModifiedDate , CRM_History = @CRM_History WHERE CRM_Id = @CRM_Id";
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.CancelSelectOnNullParameter = false;
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectParameters.Clear();
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectParameters.Add("FacilityType", TypeCode.String, Request.QueryString["s_Facility_Type"]);
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_CRM_ReportNumber"]);
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_CRM_PatientVisitNumber"]);
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectParameters.Add("Name", TypeCode.String, Request.QueryString["s_CRM_Name"]);
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.SelectParameters.Add("EscalatedForm", TypeCode.String, Request.QueryString["s_CRM_EscalatedForm"]);
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.UpdateParameters.Clear();
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.UpdateParameters.Add("CRM_Status", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.UpdateParameters.Add("CRM_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.UpdateParameters.Add("CRM_StatusRejectedReason", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.UpdateParameters.Add("CRM_ModifiedBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.UpdateParameters.Add("CRM_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.UpdateParameters.Add("CRM_History", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteComplaintsBulkApproval.UpdateParameters.Add("CRM_Id", TypeCode.Int32, "");
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

      if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_CRM_ReportNumber"]))
        {
          TextBox_ReportNumber.Text = "";
        }
        else
        {
          TextBox_ReportNumber.Text = Request.QueryString["s_CRM_ReportNumber"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_CRM_PatientVisitNumber"]))
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_CRM_PatientVisitNumber"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_Name.Text.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_CRM_Name"]))
        {
          TextBox_Name.Text = "";
        }
        else
        {
          TextBox_Name.Text = Request.QueryString["s_CRM_Name"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_EscalatedForm.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_CRM_EscalatedForm"]))
        {
          DropDownList_EscalatedForm.SelectedValue = "";
        }
        else
        {
          DropDownList_EscalatedForm.SelectedValue = Request.QueryString["s_CRM_EscalatedForm"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Route.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_CRM_Route"]))
        {
          DropDownList_Route.SelectedValue = "";
        }
        else
        {
          DropDownList_Route.SelectedValue = Request.QueryString["s_CRM_Route"];
        }
      }
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('36'))";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '146'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '147'");
            DataRow[] SecurityFacilityAdminHospitalManagerUpdate = DataTable_FormMode.Select("SecurityRole_Id = '150'");
            DataRow[] SecurityFacilityAdminNSMUpdate = DataTable_FormMode.Select("SecurityRole_Id = '148'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '149'");
            DataRow[] SecurityFacilityInvestigator = DataTable_FormMode.Select("SecurityRole_Id = '153'");
            DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '151'");
            DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '152'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminHospitalManagerUpdate.Length > 0 || SecurityFacilityAdminNSMUpdate.Length > 0 || SecurityFacilityInvestigator.Length > 0 || SecurityFacilityApprover.Length > 0))
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
        SqlDataSource_CRM_Facility.SelectParameters["Facility_Type"].DefaultValue = "0";
        DropDownList_Facility.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
        DropDownList_Facility.DataBind();
      }
      else
      {
        DropDownList_Facility.Items.Clear();
        SqlDataSource_CRM_Facility.SelectParameters["Facility_Type"].DefaultValue = DropDownList_FacilityType.SelectedValue;
        DropDownList_Facility.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Facility", CultureInfo.CurrentCulture), ""));
        DropDownList_Facility.DataBind();
      }
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_FacilityType.SelectedValue;
      string SearchField2 = DropDownList_Facility.SelectedValue;
      string SearchField3 = Server.HtmlEncode(TextBox_ReportNumber.Text);
      string SearchField4 = Server.HtmlEncode(TextBox_PatientVisitNumber.Text);
      string SearchField5 = Server.HtmlEncode(TextBox_Name.Text);
      string SearchField6 = DropDownList_EscalatedForm.SelectedValue;
      string SearchField7 = DropDownList_Route.SelectedValue;

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
        SearchField3 = "s_CRM_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_CRM_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_CRM_Name=" + Server.HtmlEncode(TextBox_Name.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_CRM_EscalatedForm=" + DropDownList_EscalatedForm.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "s_CRM_Route=" + DropDownList_Route.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_CRM_IncompleteComplaints.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Incomplete Complaints", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Incomplete Complaints", "Form_CRM_IncompleteComplaints.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_CRM_IncompleteComplaints_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_IncompleteComplaints.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_CRM_IncompleteComplaints.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_CRM_IncompleteComplaints.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_CRM_IncompleteComplaints.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_CRM_IncompleteComplaints_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_IncompleteComplaints.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_CRM_IncompleteComplaints.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_CRM_IncompleteComplaints.PageSize > 20 && GridView_CRM_IncompleteComplaints.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_CRM_IncompleteComplaints.PageSize > 50 && GridView_CRM_IncompleteComplaints.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_CRM_IncompleteComplaints.Rows.Count; i++)
      {
        if (GridView_CRM_IncompleteComplaints.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_List.Rows[i].Cells[7].Text == "No")
          {
            GridView_List.Rows[i].Cells[7].BackColor = Color.FromName("#77cf9c");
            GridView_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_List.Rows[i].Cells[7].BackColor = Color.FromName("#d46e6e");
            GridView_List.Rows[i].Cells[7].ForeColor = Color.FromName("#333333");
          }

          if (GridView_CRM_IncompleteComplaints.Rows[i].Cells[8].Text == "No")
          {
            GridView_CRM_IncompleteComplaints.Rows[i].Cells[8].BackColor = Color.FromName("#77cf9c");
            GridView_CRM_IncompleteComplaints.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_CRM_IncompleteComplaints.Rows[i].Cells[8].BackColor = Color.FromName("#d46e6e");
            GridView_CRM_IncompleteComplaints.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");
          }

          GridView_CRM_IncompleteComplaints.Rows[i].Cells[9].BackColor = Color.FromName("#d46e6e");
          GridView_CRM_IncompleteComplaints.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");

          if (Convert.ToInt32(GridView_CRM_IncompleteComplaints.Rows[i].Cells[10].Text.Replace(" : Overdue", ""), CultureInfo.CurrentCulture) >= 0)
          {
            GridView_CRM_IncompleteComplaints.Rows[i].Cells[10].BackColor = Color.FromName("#77cf9c");
            GridView_CRM_IncompleteComplaints.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
          else
          {
            string Days = GridView_CRM_IncompleteComplaints.Rows[i].Cells[10].Text.Replace(" : Overdue", "");
            GridView_CRM_IncompleteComplaints.Rows[i].Cells[10].Text = Days + Convert.ToString(" : Overdue", CultureInfo.CurrentCulture);
            GridView_CRM_IncompleteComplaints.Rows[i].Cells[10].BackColor = Color.FromName("#d46e6e");
            GridView_CRM_IncompleteComplaints.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_CRM_IncompleteComplaints_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_CRM_IncompleteComplaints.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_CRM_IncompleteComplaints.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_CRM_IncompleteComplaints.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_CRM_IncompleteComplaints_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management New Form", "Form_CRM.aspx"), false);
    }

    public string GetLink(object crm_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management New Form", "Form_CRM.aspx?CRM_Id=" + crm_Id + "") + "'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management New Form", "Form_CRM.aspx?CRM_Id=" + crm_Id + "") + "'>View</a>";
        }
      }

      string SearchField1 = Request.QueryString["s_Facility_Type"];
      string SearchField2 = Request.QueryString["s_Facility_Id"];
      string SearchField3 = Request.QueryString["s_CRM_ReportNumber"];
      string SearchField4 = Request.QueryString["s_CRM_PatientVisitNumber"];
      string SearchField5 = Request.QueryString["s_CRM_Name"];
      string SearchField6 = Request.QueryString["s_CRM_EscalatedForm"];
      string SearchField7 = Request.QueryString["s_CRM_Route"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "SearchIC_FacilityType=" + Request.QueryString["s_Facility_Type"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "SearchIC_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "SearchIC_CRMReportNumber=" + Request.QueryString["s_CRM_ReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "SearchIC_CRMPatientVisitNumber=" + Request.QueryString["s_CRM_PatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "SearchIC_CRMName=" + Request.QueryString["s_CRM_Name"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "SearchIC_CRMEscalatedForm=" + Request.QueryString["s_CRM_EscalatedForm"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "SearchIC_CRMRoute=" + Request.QueryString["s_CRM_Route"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7 + "Search_CRMForm=IncompleteComplaints&";
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
    protected void SqlDataSource_CRM_IncompleteComplaintsBulkApproval_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_BulkApproval.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged_BulkApproval(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_IncompleteComplaintsBulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_CRM_IncompleteComplaintsBulkApproval.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged_BulkApproval(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_CRM_IncompleteComplaintsBulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_CRM_IncompleteComplaintsBulkApproval.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_CRM_IncompleteComplaintsBulkApproval_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_IncompleteComplaintsBulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_CRM_IncompleteComplaintsBulkApproval.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_CRM_IncompleteComplaintsBulkApproval.PageSize > 20 && GridView_CRM_IncompleteComplaintsBulkApproval.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_CRM_IncompleteComplaintsBulkApproval.PageSize > 50 && GridView_CRM_IncompleteComplaintsBulkApproval.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_CRM_IncompleteComplaintsBulkApproval_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_CRM_IncompleteComplaintsBulkApproval.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_CRM_IncompleteComplaintsBulkApproval.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_CRM_IncompleteComplaintsBulkApproval.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_CRM_IncompleteComplaintsBulkApproval_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void DropDownList_EditStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditStatus = (DropDownList)sender;
      GridViewRow GridViewRow_CRM_IncompleteComplaintsBulkApproval = (GridViewRow)DropDownList_EditStatus.NamingContainer;
      Label Label_UpdateStatusRejectedLabel = (Label)GridViewRow_CRM_IncompleteComplaintsBulkApproval.FindControl("Label_UpdateStatusRejectedLabel");
      TextBox TextBox_EditStatusRejectedReason = (TextBox)GridViewRow_CRM_IncompleteComplaintsBulkApproval.FindControl("TextBox_EditStatusRejectedReason");

      if (DropDownList_EditStatus.SelectedValue == "Rejected")
      {
        Label_UpdateStatusRejectedLabel.Visible = true;
        TextBox_EditStatusRejectedReason.Visible = true;
      }
      else
      {
        Label_UpdateStatusRejectedLabel.Visible = false;
        TextBox_EditStatusRejectedReason.Text = "";
        TextBox_EditStatusRejectedReason.Visible = false;
      }
    }

    protected void Button_ApproveAll_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_CRM_IncompleteComplaintsBulkApproval.Rows.Count; i++)
      {
        DropDownList DropDownList_EditStatus = (DropDownList)GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Cells[0].FindControl("DropDownList_EditStatus");

        DropDownList_EditStatus.SelectedValue = "Approved";
      }

      Button_Update_Click(sender, e);
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_CRM_IncompleteComplaints.SetFocus(ImageButton_BulkApproval);

      string Proceed = "Yes";

      for (int i = 0; i < GridView_CRM_IncompleteComplaintsBulkApproval.Rows.Count; i++)
      {
        if (Proceed == "Yes")
        {
          HiddenField HiddenField_EditCRMId = (HiddenField)GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Cells[0].FindControl("HiddenField_EditCRMId");
          DropDownList DropDownList_EditStatus = (DropDownList)GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Cells[0].FindControl("DropDownList_EditStatus");
          HiddenField HiddenField_EditStatus = (HiddenField)GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Cells[0].FindControl("HiddenField_EditStatus");
          HiddenField HiddenField_EditStatusDate = (HiddenField)GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Cells[0].FindControl("HiddenField_EditStatusDate");
          TextBox TextBox_EditStatusRejectedReason = (TextBox)GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Cells[0].FindControl("TextBox_EditStatusRejectedReason");

          HiddenField HiddenField_EditModifiedDate = (HiddenField)GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Cells[0].FindControl("HiddenField_EditModifiedDate");
          Label Label_EditInvalidFormMessage = (Label)GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Cells[0].FindControl("Label_EditInvalidFormMessage");
          Label Label_EditConcurrencyUpdateMessage = (Label)GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Cells[0].FindControl("Label_EditConcurrencyUpdateMessage");

          string CRMStatus = "";
          string SQLStringCRMStatus = "SELECT CRM_Status FROM Form_CRM WHERE CRM_Id = @CRM_Id";
          using (SqlCommand SqlCommand_CRMStatus = new SqlCommand(SQLStringCRMStatus))
          {
            SqlCommand_CRMStatus.Parameters.AddWithValue("@CRM_Id", HiddenField_EditCRMId.Value.ToString());
            DataTable DataTable_CRMStatus;
            using (DataTable_CRMStatus = new DataTable())
            {
              DataTable_CRMStatus.Locale = CultureInfo.CurrentCulture;
              DataTable_CRMStatus = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRMStatus).Copy();
              if (DataTable_CRMStatus.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_CRMStatus.Rows)
                {
                  CRMStatus = DataRow_Row["CRM_Status"].ToString();
                }
              }
            }
          }

          if (CRMStatus == "Pending Approval" && DropDownList_EditStatus.SelectedValue != "Pending Approval")
          {
            Session["OLDCRMModifiedDate"] = HiddenField_EditModifiedDate.Value;
            object OLDCRMModifiedDate = Session["OLDCRMModifiedDate"].ToString();
            DateTime OLDModifiedDate1 = DateTime.Parse(OLDCRMModifiedDate.ToString(), CultureInfo.CurrentCulture);
            string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

            Session["DBCRMModifiedDate"] = "";
            Session["DBCRMModifiedBy"] = "";
            string SQLStringCRM = "SELECT CRM_ModifiedDate , CRM_ModifiedBy FROM Form_CRM WHERE CRM_Id = @CRM_Id";
            using (SqlCommand SqlCommand_CRM = new SqlCommand(SQLStringCRM))
            {
              SqlCommand_CRM.Parameters.AddWithValue("@CRM_Id", HiddenField_EditCRMId.Value);
              DataTable DataTable_CRM;
              using (DataTable_CRM = new DataTable())
              {
                DataTable_CRM.Locale = CultureInfo.CurrentCulture;
                DataTable_CRM = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CRM).Copy();
                if (DataTable_CRM.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_CRM.Rows)
                  {
                    Session["DBCRMModifiedDate"] = DataRow_Row["CRM_ModifiedDate"];
                    Session["DBCRMModifiedBy"] = DataRow_Row["CRM_ModifiedBy"];
                  }
                }
              }
            }

            object DBCRMModifiedDate = Session["DBCRMModifiedDate"].ToString();
            DateTime DBModifiedDate1 = DateTime.Parse(DBCRMModifiedDate.ToString(), CultureInfo.CurrentCulture);
            string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

            if (OLDModifiedDateNew != DBModifiedDateNew)
            {
              Proceed = "No";

              string Label_EditConcurrencyUpdateMessageText = Convert.ToString("" +
                "Record could not be updated<br/>" +
                "It was updated at " + DBModifiedDateNew + " by " + Session["DBCRMModifiedBy"].ToString() + "<br/>" +
                "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

              Label_EditInvalidFormMessage.Text = "";
              Label_EditConcurrencyUpdateMessage.Text = Label_EditConcurrencyUpdateMessageText;
            }
            else if (OLDModifiedDateNew == DBModifiedDateNew)
            {
              string Label_EditInvalidFormMessageText = EditValidation(DropDownList_EditStatus, TextBox_EditStatusRejectedReason);

              if (!string.IsNullOrEmpty(Label_EditInvalidFormMessageText))
              {
                Proceed = "No";

                Label_EditInvalidFormMessage.Text = Label_EditInvalidFormMessageText;
                Label_EditConcurrencyUpdateMessage.Text = "";
              }
              else
              {
                string SQLStringEdit = "UPDATE Form_CRM SET CRM_Status = @CRM_Status ,CRM_StatusDate = @CRM_StatusDate ,CRM_StatusRejectedReason = @CRM_StatusRejectedReason , CRM_ModifiedDate = @CRM_ModifiedDate ,CRM_ModifiedBy = @CRM_ModifiedBy ,CRM_History = @CRM_History WHERE CRM_Id = @CRM_Id";
                using (SqlCommand SqlCommand_Edit = new SqlCommand(SQLStringEdit))
                {
                  Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_CRM", "CRM_Id = " + HiddenField_EditCRMId.Value.ToString());

                  DataView DataView_CRM = (DataView)SqlDataSource_CRM_IncompleteComplaintsBulkApproval.Select(DataSourceSelectArguments.Empty);
                  if (DataView_CRM.Table.Rows.Count != 0)
                  {
                    DataRowView DataRowView_CRM = DataView_CRM[0];
                    Session["CRMHistory"] = Convert.ToString(DataRowView_CRM["CRM_History"], CultureInfo.CurrentCulture);
                    Session["CRMHistory"] = Session["History"].ToString() + Session["CRMHistory"].ToString();

                    SqlCommand_Edit.Parameters.AddWithValue("@CRM_Status", DropDownList_EditStatus.SelectedValue.ToString());

                    string DBStatus = HiddenField_EditStatus.Value.ToString();
                    if (DBStatus != DropDownList_EditStatus.SelectedValue)
                    {
                      if (DBStatus == "Pending Approval")
                      {
                        SqlCommand_Edit.Parameters.AddWithValue("@CRM_StatusDate", DateTime.Now.ToString());
                      }
                    }
                    else
                    {
                      SqlCommand_Edit.Parameters.AddWithValue("@CRM_StatusDate", Convert.ToDateTime(HiddenField_EditStatusDate.Value, CultureInfo.CurrentCulture));
                    }

                    if (DropDownList_EditStatus.SelectedValue == "Rejected")
                    {
                      SqlCommand_Edit.Parameters.AddWithValue("@CRM_StatusRejectedReason", TextBox_EditStatusRejectedReason.Text.ToString());
                    }
                    else
                    {
                      SqlCommand_Edit.Parameters.AddWithValue("@CRM_StatusRejectedReason", DBNull.Value);
                    }

                    SqlCommand_Edit.Parameters.AddWithValue("@CRM_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                    SqlCommand_Edit.Parameters.AddWithValue("@CRM_ModifiedDate", DateTime.Now);
                    SqlCommand_Edit.Parameters.AddWithValue("@CRM_History", Session["CRMHistory"].ToString());
                    SqlCommand_Edit.Parameters.AddWithValue("@CRM_Id", HiddenField_EditCRMId.Value.ToString());

                    Session["CRMHistory"] = "";
                    Session["History"] = "";

                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_Edit);
                  }

                  Session["CRMHistory"] = "";
                  Session["History"] = "";
                }

                GridView_CRM_IncompleteComplaintsBulkApproval.Rows[i].Visible = false;
              }
            }
          }
          else
          {
            Label_EditInvalidFormMessage.Text = "";
            Label_EditConcurrencyUpdateMessage.Text = "";
          }
        }
      }

      if (Proceed == "Yes")
      {
        GridView_CRM_IncompleteComplaints.DataBind();
        GridView_CRM_IncompleteComplaintsBulkApproval.DataBind();
      }
    }

    protected static string EditValidation(ListControl dropDownList_EditStatus, TextBox textBox_EditStatusRejectedReason)
    {
      string InvalidFormMessage = "";

      if (dropDownList_EditStatus != null && textBox_EditStatusRejectedReason != null)
      {
        if (dropDownList_EditStatus.SelectedValue == "Rejected")
        {
          if (string.IsNullOrEmpty(textBox_EditStatusRejectedReason.Text.ToString()))
          {
            InvalidFormMessage = "Rejection Reason is Required";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_CRM_IncompleteComplaints.SetFocus(ImageButton_BulkApproval);

      GridView_CRM_IncompleteComplaints.DataBind();
      GridView_CRM_IncompleteComplaintsBulkApproval.DataBind();
    }

    protected void Button_ApproveAll_DataBinding(object sender, EventArgs e)
    {
      Button Button_Cancel = (Button)sender;
      Button_Cancel.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Approve All');");
    }

    protected void Button_Update_DataBinding(object sender, EventArgs e)
    {
      Button Button_Cancel = (Button)sender;
      Button_Cancel.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Update the changes');");
    }

    protected void Button_Cancel_DataBinding(object sender, EventArgs e)
    {
      Button Button_Cancel = (Button)sender;
      Button_Cancel.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Cancel the changes');");
    }
    //---END--- --Bulk Approval--//
  }
}