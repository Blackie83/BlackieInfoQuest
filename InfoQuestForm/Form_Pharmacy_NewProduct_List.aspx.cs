using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Pharmacy_NewProduct_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("33").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("33").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("33").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('33'))";
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
      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Pharmacy_NewProduct_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Pharmacy: New Product Code Request", "6");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Pharmacy_NewProduct_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Pharmacy_NewProduct_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Pharmacy_NewProduct_Facility.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Pharmacy_NewProduct_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "33");
      SqlDataSource_Pharmacy_NewProduct_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Pharmacy_NewProduct_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "Facility_Id");
      SqlDataSource_Pharmacy_NewProduct_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "InfoQuest_Form_Pharmacy_NewProduct");
      SqlDataSource_Pharmacy_NewProduct_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Pharmacy_NewProduct_CreatedBy.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_CreatedBy.SelectCommand = "SELECT DISTINCT NewProduct_CreatedBy FROM InfoQuest_Form_Pharmacy_NewProduct ORDER BY NewProduct_CreatedBy";
      SqlDataSource_Pharmacy_NewProduct_CreatedBy.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_ModifiedBy.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_ModifiedBy.SelectCommand = "SELECT DISTINCT NewProduct_ModifiedBy FROM InfoQuest_Form_Pharmacy_NewProduct ORDER BY NewProduct_ModifiedBy";
      SqlDataSource_Pharmacy_NewProduct_ModifiedBy.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_Feedback_ProgressStatus.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_Feedback_ProgressStatus.SelectCommand = "SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 33 AND ListCategory_Id = 99 AND ListItem_Parent = -1";
      SqlDataSource_Pharmacy_NewProduct_Feedback_ProgressStatus.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_Manufacturer.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_Manufacturer.SelectCommand = "SELECT Pharmacy_Supplier_Lookup_Id , Pharmacy_Supplier_Lookup_Description + ' (' + ISNULL(Pharmacy_Supplier_Lookup_Code,'') + ')' AS Pharmacy_Supplier_Lookup_Description FROM Form_Pharmacy_Supplier_Lookup WHERE Pharmacy_Supplier_Lookup_IsActive = 1 UNION SELECT InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Manufacturer_List , Pharmacy_Supplier_Lookup_Description + ' (' + ISNULL(Pharmacy_Supplier_Lookup_Code,'') + ')' AS Pharmacy_Supplier_Lookup_Description FROM InfoQuest_Form_Pharmacy_NewProduct LEFT JOIN Form_Pharmacy_Supplier_Lookup ON InfoQuest_Form_Pharmacy_NewProduct.NewProduct_Manufacturer_List = Form_Pharmacy_Supplier_Lookup.Pharmacy_Supplier_Lookup_Id ORDER BY Pharmacy_Supplier_Lookup_Description";
      SqlDataSource_Pharmacy_NewProduct_Manufacturer.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Pharmacy_NewProduct_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Pharmacy_NewProduct_List.SelectCommand = "spForm_Get_Pharmacy_NewProduct_List";
      SqlDataSource_Pharmacy_NewProduct_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Pharmacy_NewProduct_List.CancelSelectOnNullParameter = false;
      SqlDataSource_Pharmacy_NewProduct_List.SelectParameters.Clear();
      SqlDataSource_Pharmacy_NewProduct_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Pharmacy_NewProduct_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Pharmacy_NewProduct_List.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_NewProduct_ReportNumber"]);
      SqlDataSource_Pharmacy_NewProduct_List.SelectParameters.Add("ManufacturerList", TypeCode.String, Request.QueryString["s_NewProduct_Manufacturer_List"]);
      SqlDataSource_Pharmacy_NewProduct_List.SelectParameters.Add("CreatedBy", TypeCode.String, Request.QueryString["s_NewProduct_CreatedBy"]);
      SqlDataSource_Pharmacy_NewProduct_List.SelectParameters.Add("ModifiedBy", TypeCode.String, Request.QueryString["s_NewProduct_ModifiedBy"]);
      SqlDataSource_Pharmacy_NewProduct_List.SelectParameters.Add("IsActive", TypeCode.String, Request.QueryString["s_NewProduct_IsActive"]);
      SqlDataSource_Pharmacy_NewProduct_List.SelectParameters.Add("FeedbackProgressStatusList", TypeCode.String, Request.QueryString["s_NewProduct_Feedback_ProgressStatus_List"]);
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

      if (string.IsNullOrEmpty(TextBox_ReportNumber.Text.ToString()))
      {
        if (Request.QueryString["s_NewProduct_ReportNumber"] == null)
        {
          TextBox_ReportNumber.Text = "";
        }
        else
        {
          TextBox_ReportNumber.Text = Request.QueryString["s_NewProduct_ReportNumber"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Manufacturer.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_NewProduct_Manufacturer_List"] == null)
        {
          DropDownList_Manufacturer.SelectedValue = "";
        }
        else
        {
          DropDownList_Manufacturer.SelectedValue = Request.QueryString["s_NewProduct_Manufacturer_List"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_CreatedBy.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_NewProduct_CreatedBy"] == null)
        {
          DropDownList_CreatedBy.SelectedValue = "";
        }
        else
        {
          DropDownList_CreatedBy.SelectedValue = Request.QueryString["s_NewProduct_CreatedBy"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_ModifiedBy.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_NewProduct_ModifiedBy"] == null)
        {
          DropDownList_ModifiedBy.SelectedValue = "";
        }
        else
        {
          DropDownList_ModifiedBy.SelectedValue = Request.QueryString["s_NewProduct_ModifiedBy"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_IsActive.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_NewProduct_IsActive"] == null)
        {
          DropDownList_IsActive.SelectedValue = "";
        }
        else
        {
          DropDownList_IsActive.SelectedValue = Request.QueryString["s_NewProduct_IsActive"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_FeedbackProgressStatus.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_NewProduct_Feedback_ProgressStatus_List"] == null)
        {
          DropDownList_FeedbackProgressStatus.SelectedValue = "";
        }
        else
        {
          DropDownList_FeedbackProgressStatus.SelectedValue = Request.QueryString["s_NewProduct_Feedback_ProgressStatus_List"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Facility.SelectedValue;
      string SearchField2 = Server.HtmlEncode(TextBox_ReportNumber.Text);
      string SearchField3 = DropDownList_Manufacturer.SelectedValue;
      string SearchField4 = DropDownList_CreatedBy.SelectedValue;
      string SearchField5 = DropDownList_ModifiedBy.SelectedValue;
      string SearchField6 = DropDownList_IsActive.SelectedValue;
      string SearchField7 = DropDownList_FeedbackProgressStatus.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_NewProduct_ReportNumber=" + Server.HtmlEncode(TextBox_ReportNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_NewProduct_Manufacturer_List=" + DropDownList_Manufacturer.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_NewProduct_CreatedBy=" + DropDownList_CreatedBy.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_NewProduct_ModifiedBy=" + DropDownList_ModifiedBy.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_NewProduct_IsActive=" + DropDownList_IsActive.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "s_NewProduct_Feedback_ProgressStatus_List=" + DropDownList_FeedbackProgressStatus.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_Pharmacy_NewProduct_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request Captured Forms", "Form_Pharmacy_NewProduct_List.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_Pharmacy_NewProduct_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_Pharmacy_NewProduct_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_Pharmacy_NewProduct_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_Pharmacy_NewProduct_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_Pharmacy_NewProduct_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_Pharmacy_NewProduct_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Pharmacy_NewProduct_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Pharmacy_NewProduct_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Pharmacy_NewProduct_List.PageSize > 20 && GridView_Pharmacy_NewProduct_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Pharmacy_NewProduct_List.PageSize > 50 && GridView_Pharmacy_NewProduct_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_Pharmacy_NewProduct_List.Rows.Count; i++)
      {
        GridView_Pharmacy_NewProduct_List.HeaderRow.Cells[11].Visible = false;

        if (GridView_Pharmacy_NewProduct_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          GridView_Pharmacy_NewProduct_List.Rows[i].Cells[11].Visible = false;

          if (GridView_Pharmacy_NewProduct_List.Rows[i].Cells[11].Text.ToString() == "4333") //Declined
          {
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].BackColor = Color.FromName("#d46e6e"); //Red
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_Pharmacy_NewProduct_List.Rows[i].Cells[11].Text.ToString() == "4332") //Approved
          {
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].BackColor = Color.FromName("#77cf9c"); //Green
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_Pharmacy_NewProduct_List.Rows[i].Cells[11].Text.ToString() == "4411") //Captured
          {
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].BackColor = Color.FromName("#ffcc66"); //Orange
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_Pharmacy_NewProduct_List.Rows[i].Cells[11].Text.ToString() == "4334") //Pending
          {
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].BackColor = Color.FromName("#68c0ff"); //Light Blue
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_Pharmacy_NewProduct_List.Rows[i].Cells[11].Text.ToString() == "4409") //Information Required
          {
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].BackColor = Color.FromName("#eed029"); //Yellow
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_Pharmacy_NewProduct_List.Rows[i].Cells[11].Text.ToString() == "4938") //Completed
          {
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].BackColor = Color.FromName("#c3c3c3"); //Grey
            GridView_Pharmacy_NewProduct_List.Rows[i].Cells[10].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_Pharmacy_NewProduct_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_Pharmacy_NewProduct_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Pharmacy_NewProduct_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Pharmacy_NewProduct_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_Pharmacy_NewProduct_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request New Form", "Form_Pharmacy_NewProduct.aspx"), false);
    }

    //protected void Button_GoToPF_OnClick(object sender, EventArgs e)
    //{
    //  Session["CRMId"] = "";
    //  Session["CRMTypeList"] = "";
    //  String SQLStringSurveyResults = "SELECT Form_CRM_PXM_PDCH_Result.CRM_Id , CRM_Type_List FROM Form_CRM_PXM_PDCH_Result LEFT	JOIN Form_CRM ON Form_CRM_PXM_PDCH_Result.CRM_Id = Form_CRM.CRM_Id WHERE Form_CRM_PXM_PDCH_Result.CRM_Id = @CRM_Id";
    //  using (SqlCommand SqlCommand_SurveyResults = new SqlCommand(SQLStringSurveyResults))
    //  {
    //    SqlCommand_SurveyResults.Parameters.AddWithValue("@CRM_Id", Request.QueryString["CRM_Id"]);
    //    DataTable DataTable_SurveyResults;
    //    using (DataTable_SurveyResults = new DataTable())
    //    {
    //      DataTable_SurveyResults.Locale = CultureInfo.CurrentCulture;
    //      DataTable_SurveyResults = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SurveyResults).Copy();
    //      if (DataTable_SurveyResults.Rows.Count > 0)
    //      {
    //        foreach (DataRow DataRow_Row in DataTable_SurveyResults.Rows)
    //        {
    //          Session["CRMId"] = DataRow_Row["CRM_Id"].ToString();
    //          Session["CRMTypeList"] = DataRow_Row["CRM_Type_List"].ToString();
    //        }
    //      }
    //    }
    //  }



    //  Response.Write("<script>");
    //  Response.Write("window.open('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Products and Formularies", "https://www.google.com/") + "','_blank')");
    //  Response.Write("</script>");
    //}

    public string GetLink(object newProduct_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request Form", "Form_Pharmacy_NewProduct.aspx?NewProduct_Id=" + newProduct_Id + "") + "'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("New Product Code Request Form", "Form_Pharmacy_NewProduct.aspx?NewProduct_Id=" + newProduct_Id + "") + "'>View</a>";
        }
      }

      string SearchField1 = Request.QueryString["s_Facility_Id"];
      string SearchField2 = Request.QueryString["s_NewProduct_ReportNumber"];
      string SearchField3 = Request.QueryString["s_NewProduct_Manufacturer_List"];
      string SearchField4 = Request.QueryString["s_NewProduct_CreatedBy"];
      string SearchField5 = Request.QueryString["s_NewProduct_ModifiedBy"];
      string SearchField6 = Request.QueryString["s_NewProduct_IsActive"];
      string SearchField7 = Request.QueryString["s_NewProduct_Feedback_ProgressStatus_List"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_NewProductReportNumber=" + Request.QueryString["s_NewProduct_ReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_NewProductManufacturerList=" + Request.QueryString["s_NewProduct_Manufacturer_List"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_NewProductCreatedBy=" + Request.QueryString["s_NewProduct_CreatedBy"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "Search_NewProductModifiedBy=" + Request.QueryString["s_NewProduct_ModifiedBy"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "Search_NewProductIsActive=" + Request.QueryString["s_NewProduct_IsActive"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "Search_NewProductFeedbackProgressStatusList=" + Request.QueryString["s_NewProduct_Feedback_ProgressStatus_List"] + "&";
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
  }
}