using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_PXM_PDCH_DoctorQuestion_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_PXM_PDCH_DoctorQuestion_List, this.GetType(), "UpdateProgress_Start", "Validation_Search();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          TextBox_CurrentDateFrom.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_CurrentDateFrom.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_CurrentDateFrom.Attributes.Add("OnInput", "Validation_Search();");
          TextBox_CurrentDateTo.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_CurrentDateTo.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_CurrentDateTo.Attributes.Add("OnInput", "Validation_Search();");

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " : Uploaded", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " : Uploaded", CultureInfo.CurrentCulture);
          Label_GridHeading_List.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " Uploaded Records", CultureInfo.CurrentCulture);
          Label_GridHeading_Uploaded.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " Uploaded Files", CultureInfo.CurrentCulture);

          SetFormQueryString();

          SetFormVisibility();
        }
      }
    }

    protected string PageSecurity()
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id IN ('146'))";
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_PXM_PDCH_DoctorQuestion_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Customer Relationship Management", "4");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_PXM_PDCH_DoctorQuestion_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PXM_PDCH_DoctorQuestion_Facility.SelectCommand = @"SELECT		DISTINCT 
					                                                                    PXM_PDCH_DoctorQuestion_BusinessUnitName AS Facility
                                                                    FROM			Form_PXM_PDCH_DoctorQuestion
                                                                    ORDER BY	PXM_PDCH_DoctorQuestion_BusinessUnitName";
      SqlDataSource_PXM_PDCH_DoctorQuestion_Facility.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_PXM_PDCH_DoctorQuestion_Facility.SelectParameters.Clear();

      SqlDataSource_PXM_PDCH_DoctorQuestion_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PXM_PDCH_DoctorQuestion_List.SelectCommand = "spForm_Get_PXM_PDCH_DoctorQuestion_List";
      SqlDataSource_PXM_PDCH_DoctorQuestion_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PXM_PDCH_DoctorQuestion_List.CancelSelectOnNullParameter = false;
      SqlDataSource_PXM_PDCH_DoctorQuestion_List.SelectParameters.Clear();
      SqlDataSource_PXM_PDCH_DoctorQuestion_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_PXM_PDCH_DoctorQuestion_List.SelectParameters.Add("FacilityName", TypeCode.String, Request.QueryString["s_FacilityName"]);
      SqlDataSource_PXM_PDCH_DoctorQuestion_List.SelectParameters.Add("CurrentDateFrom", TypeCode.String, Request.QueryString["s_CurrentDateFrom"]);
      SqlDataSource_PXM_PDCH_DoctorQuestion_List.SelectParameters.Add("CurrentDateTo", TypeCode.String, Request.QueryString["s_CurrentDateTo"]);

      SqlDataSource_PXM_PDCH_DoctorQuestion_Uploaded.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_PXM_PDCH_DoctorQuestion_Uploaded.SelectCommand = "spForm_Get_PXM_PDCH_DoctorQuestion_FileUploaded_List";
      SqlDataSource_PXM_PDCH_DoctorQuestion_Uploaded.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_PXM_PDCH_DoctorQuestion_Uploaded.SelectParameters.Clear();
      SqlDataSource_PXM_PDCH_DoctorQuestion_Uploaded.SelectParameters.Add("CurrentDateFrom", TypeCode.String, Request.QueryString["s_CurrentDateFrom"]);
      SqlDataSource_PXM_PDCH_DoctorQuestion_Uploaded.SelectParameters.Add("CurrentDateTo", TypeCode.String, Request.QueryString["s_CurrentDateTo"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_FacilityName"] == null)
        {
          DropDownList_Facility.SelectedValue = "";
        }
        else
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_FacilityName"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_CurrentDateFrom.Text.ToString()))
      {
        if (Request.QueryString["s_CurrentDateFrom"] == null)
        {
          TextBox_CurrentDateFrom.Text = "";
        }
        else
        {
          TextBox_CurrentDateFrom.Text = Request.QueryString["s_CurrentDateFrom"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_CurrentDateTo.Text.ToString()))
      {
        if (Request.QueryString["s_CurrentDateTo"] == null)
        {
          TextBox_CurrentDateTo.Text = "";
        }
        else
        {
          TextBox_CurrentDateTo.Text = Request.QueryString["s_CurrentDateTo"];
        }
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["s_CurrentDateFrom"] == null || Request.QueryString["s_CurrentDateTo"] == null)
      {
        DivList.Visible = false;
        TableList.Visible = false;
        DivUploaded.Visible = false;
        TableUploaded.Visible = false;
      }
      else
      {
        DivList.Visible = true;
        TableList.Visible = true;
        DivUploaded.Visible = true;
        TableUploaded.Visible = true;
      }
    }

    protected void RetrieveDatabaseFile_Uploaded(object sender, EventArgs e)
    {
      LinkButton LinkButton_PXM_PDCH_DoctorQuestionFile = (LinkButton)sender;
      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_PXM_PDCH_DoctorQuestionFile.CommandArgument.ToString().Split(';');
      string FileId = CommandArgumentSplit[0];

      string PXM_PDCH_DoctorQuestionFileUploadedZipFileName = "";
      string PXM_PDCH_DoctorQuestionFileUploadedContentType = "";
      Object PXM_PDCH_DoctorQuestionFileUploadedData = new Object();
      string SQLStringPXM_PDCH_DoctorQuestionFile = "SELECT PXM_PDCH_DoctorQuestion_FileUploaded_ZipFileName , PXM_PDCH_DoctorQuestion_FileUploaded_ContentType , PXM_PDCH_DoctorQuestion_FileUploaded_Data FROM Form_PXM_PDCH_DoctorQuestion_FileUploaded WHERE PXM_PDCH_DoctorQuestion_FileUploaded_Id = @PXM_PDCH_DoctorQuestion_FileUploaded_Id";
      using (SqlCommand SqlCommand_PXM_PDCH_DoctorQuestionFile = new SqlCommand(SQLStringPXM_PDCH_DoctorQuestionFile))
      {
        SqlCommand_PXM_PDCH_DoctorQuestionFile.Parameters.AddWithValue("@PXM_PDCH_DoctorQuestion_FileUploaded_Id", FileId);
        DataTable DataTable_PXM_PDCH_DoctorQuestionFile;
        using (DataTable_PXM_PDCH_DoctorQuestionFile = new DataTable())
        {
          DataTable_PXM_PDCH_DoctorQuestionFile.Locale = CultureInfo.CurrentCulture;
          DataTable_PXM_PDCH_DoctorQuestionFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXM_PDCH_DoctorQuestionFile).Copy();
          if (DataTable_PXM_PDCH_DoctorQuestionFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PXM_PDCH_DoctorQuestionFile.Rows)
            {
              PXM_PDCH_DoctorQuestionFileUploadedZipFileName = DataRow_Row["PXM_PDCH_DoctorQuestion_FileUploaded_ZipFileName"].ToString();
              PXM_PDCH_DoctorQuestionFileUploadedContentType = DataRow_Row["PXM_PDCH_DoctorQuestion_FileUploaded_ContentType"].ToString();
              PXM_PDCH_DoctorQuestionFileUploadedData = DataRow_Row["PXM_PDCH_DoctorQuestion_FileUploaded_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(PXM_PDCH_DoctorQuestionFileUploadedData.ToString()))
      {
        Byte[] Byte_FileData = (Byte[])PXM_PDCH_DoctorQuestionFileUploadedData;
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = PXM_PDCH_DoctorQuestionFileUploadedContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + PXM_PDCH_DoctorQuestionFileUploadedZipFileName + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      PXM_PDCH_DoctorQuestionFileUploadedZipFileName = "";
      PXM_PDCH_DoctorQuestionFileUploadedContentType = "";
      PXM_PDCH_DoctorQuestionFileUploadedData = "";
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchErrorMessage = "";
      string ValidSearch = "Yes";

      if (!string.IsNullOrEmpty(TextBox_CurrentDateFrom.Text.ToString()))
      {
        string DateToValidate = TextBox_CurrentDateFrom.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          SearchErrorMessage = SearchErrorMessage + "Current Date From is not in the correct format,<br />date must be in the format yyyy/mm/dd<br />";
          ValidSearch = "No";
        }
      }
      else
      {
        SearchErrorMessage = SearchErrorMessage + "Current Date From is required<br />";
        ValidSearch = "No";
      }

      if (!string.IsNullOrEmpty(TextBox_CurrentDateTo.Text.ToString()))
      {
        string DateToValidate = TextBox_CurrentDateTo.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          SearchErrorMessage = SearchErrorMessage + "Current Date To is not in the correct format,<br />date must be in the format yyyy/mm/dd<br />";
          ValidSearch = "No";
        }
      }
      else
      {
        SearchErrorMessage = SearchErrorMessage + "Current Date To is required<br />";
        ValidSearch = "No";
      }

      if (ValidSearch == "No")
      {
        Label_SearchErrorMessage.Text = Convert.ToString(SearchErrorMessage, CultureInfo.CurrentCulture);
      }
      else
      {
        Label_SearchErrorMessage.Text = "";

        string SearchField1 = DropDownList_Facility.SelectedValue.ToString();
        string SearchField2 = Server.HtmlEncode(TextBox_CurrentDateFrom.Text);
        string SearchField3 = Server.HtmlEncode(TextBox_CurrentDateTo.Text);

        if (!string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "s_FacilityName=" + DropDownList_Facility.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "s_CurrentDateFrom=" + Server.HtmlEncode(TextBox_CurrentDateFrom.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "s_CurrentDateTo=" + Server.HtmlEncode(TextBox_CurrentDateTo.Text.ToString()) + "&";
        }

        string FinalURL = "Form_PXM_PDCH_DoctorQuestion_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("PXM PDCH Doctor Question List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("PXM PDCH Doctor Question List", "Form_PXM_PDCH_DoctorQuestion_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_PXM_PDCH_DoctorQuestion_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_List.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_PXM_PDCH_DoctorQuestion_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_PXM_PDCH_DoctorQuestion_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_PXM_PDCH_DoctorQuestion_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_PXM_PDCH_DoctorQuestion_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_PXM_PDCH_DoctorQuestion_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_PXM_PDCH_DoctorQuestion_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_PXM_PDCH_DoctorQuestion_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_PXM_PDCH_DoctorQuestion_List.PageSize > 20 && GridView_PXM_PDCH_DoctorQuestion_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_PXM_PDCH_DoctorQuestion_List.PageSize > 50 && GridView_PXM_PDCH_DoctorQuestion_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_PXM_PDCH_DoctorQuestion_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_PXM_PDCH_DoctorQuestion_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_PXM_PDCH_DoctorQuestion_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_PXM_PDCH_DoctorQuestion_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_PXM_PDCH_DoctorQuestion_List_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --List--//


    //--START-- --Uploaded--//
    protected void SqlDataSource_PXM_PDCH_DoctorQuestion_Uploaded_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Uploaded.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_Uploaded_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_PXM_PDCH_DoctorQuestion_Uploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Uploaded");
      GridView_PXM_PDCH_DoctorQuestion_Uploaded.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_Uploaded_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_PXM_PDCH_DoctorQuestion_Uploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_Uploaded");
      GridView_PXM_PDCH_DoctorQuestion_Uploaded.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_PXM_PDCH_DoctorQuestion_Uploaded_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_PXM_PDCH_DoctorQuestion_Uploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Uploaded");
        if (GridView_PXM_PDCH_DoctorQuestion_Uploaded.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_PXM_PDCH_DoctorQuestion_Uploaded.PageSize > 20 && GridView_PXM_PDCH_DoctorQuestion_Uploaded.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_PXM_PDCH_DoctorQuestion_Uploaded.PageSize > 50 && GridView_PXM_PDCH_DoctorQuestion_Uploaded.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_PXM_PDCH_DoctorQuestion_Uploaded_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_PXM_PDCH_DoctorQuestion_Uploaded.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_Uploaded");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_PXM_PDCH_DoctorQuestion_Uploaded.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_PXM_PDCH_DoctorQuestion_Uploaded.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_PXM_PDCH_DoctorQuestion_Uploaded_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void LinkButton_RetrieveDatabaseFile_Uploaded_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_RetrieveDatabaseFile_Uploaded = (LinkButton)sender;
      ScriptManager ScriptManager_RetrieveDatabaseFile_Uploaded = ScriptManager.GetCurrent(Page);
      ScriptManager_RetrieveDatabaseFile_Uploaded.RegisterPostBackControl(LinkButton_RetrieveDatabaseFile_Uploaded);

      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_RetrieveDatabaseFile_Uploaded.CommandArgument.ToString().Split(';');
      string FileExist = CommandArgumentSplit[1];

      if (FileExist == "No")
      {
        LinkButton_RetrieveDatabaseFile_Uploaded.Visible = false;
      }
    }
    //---END--- --Uploaded--//
  }
}