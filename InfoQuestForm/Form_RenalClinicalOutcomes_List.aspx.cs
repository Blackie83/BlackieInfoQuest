using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_RenalClinicalOutcomes_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_RenalClinicalOutcomes_List, this.GetType(), "UpdateProgress_Start", "Validation_Search();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          TextBox_CurrentDateFrom.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_CurrentDateFrom.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_CurrentDateFrom.Attributes.Add("OnInput", "Validation_Search();");
          TextBox_CurrentDateTo.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_CurrentDateTo.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_CurrentDateTo.Attributes.Add("OnInput", "Validation_Search();");

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("50").Replace(" Form", "")).ToString() + " : Uploaded", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("50").Replace(" Form", "")).ToString() + " : Uploaded", CultureInfo.CurrentCulture);
          Label_GridHeading_List.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("50").Replace(" Form", "")).ToString() + " Uploaded Records", CultureInfo.CurrentCulture);
          Label_GridHeading_Upload.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("50").Replace(" Form", "")).ToString() + " Uploaded Files", CultureInfo.CurrentCulture);

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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id IN ('200'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("50");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_RenalClinicalOutcomes_List.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Renal Clinical Outcomes", "26");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_RenalClinicalOutcomes_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RenalClinicalOutcomes_Facility.SelectCommand = @"SELECT		DISTINCT 
					                                                                    RenalClinicalOutcomes_Facility AS Facility
                                                                    FROM			Form_RenalClinicalOutcomes
                                                                    ORDER BY	RenalClinicalOutcomes_Facility";
      SqlDataSource_RenalClinicalOutcomes_Facility.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_RenalClinicalOutcomes_Facility.SelectParameters.Clear();

      SqlDataSource_RenalClinicalOutcomes_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RenalClinicalOutcomes_List.SelectCommand = "spForm_Get_RenalClinicalOutcomes_List";
      SqlDataSource_RenalClinicalOutcomes_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RenalClinicalOutcomes_List.CancelSelectOnNullParameter = false;
      SqlDataSource_RenalClinicalOutcomes_List.SelectParameters.Clear();
      SqlDataSource_RenalClinicalOutcomes_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_RenalClinicalOutcomes_List.SelectParameters.Add("FacilityName", TypeCode.String, Request.QueryString["s_FacilityName"]);
      SqlDataSource_RenalClinicalOutcomes_List.SelectParameters.Add("CurrentDateFrom", TypeCode.String, Request.QueryString["s_CurrentDateFrom"]);
      SqlDataSource_RenalClinicalOutcomes_List.SelectParameters.Add("CurrentDateTo", TypeCode.String, Request.QueryString["s_CurrentDateTo"]);

      SqlDataSource_RenalClinicalOutcomes_Upload.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RenalClinicalOutcomes_Upload.SelectCommand = "spForm_Get_RenalClinicalOutcomes_FileUpload_List";
      SqlDataSource_RenalClinicalOutcomes_Upload.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RenalClinicalOutcomes_Upload.SelectParameters.Clear();
      SqlDataSource_RenalClinicalOutcomes_Upload.SelectParameters.Add("CurrentDateFrom", TypeCode.String, Request.QueryString["s_CurrentDateFrom"]);
      SqlDataSource_RenalClinicalOutcomes_Upload.SelectParameters.Add("CurrentDateTo", TypeCode.String, Request.QueryString["s_CurrentDateTo"]);
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
        DivUpload.Visible = false;
        TableUpload.Visible = false;
      }
      else
      {
        DivList.Visible = true;
        TableList.Visible = true;
        DivUpload.Visible = true;
        TableUpload.Visible = true;
      }
    }

    protected void RetrieveDatabaseFile_Upload(object sender, EventArgs e)
    {
      LinkButton LinkButton_RenalClinicalOutcomesFile = (LinkButton)sender;
      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_RenalClinicalOutcomesFile.CommandArgument.ToString().Split(';');
      string FileId = CommandArgumentSplit[0];

      string RenalClinicalOutcomesFileUploadZipFileName = "";
      string RenalClinicalOutcomesFileUploadContentType = "";
      Object RenalClinicalOutcomesFileUploadData = new Object();
      string SQLStringRenalClinicalOutcomesFile = "SELECT RenalClinicalOutcomes_FileUpload_ZipFileName , RenalClinicalOutcomes_FileUpload_ContentType , RenalClinicalOutcomes_FileUpload_Data FROM Form_RenalClinicalOutcomes_FileUpload WHERE RenalClinicalOutcomes_FileUpload_Id = @RenalClinicalOutcomes_FileUpload_Id";
      using (SqlCommand SqlCommand_RenalClinicalOutcomesFile = new SqlCommand(SQLStringRenalClinicalOutcomesFile))
      {
        SqlCommand_RenalClinicalOutcomesFile.Parameters.AddWithValue("@RenalClinicalOutcomes_FileUpload_Id", FileId);
        DataTable DataTable_RenalClinicalOutcomesFile;
        using (DataTable_RenalClinicalOutcomesFile = new DataTable())
        {
          DataTable_RenalClinicalOutcomesFile.Locale = CultureInfo.CurrentCulture;
          DataTable_RenalClinicalOutcomesFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_RenalClinicalOutcomesFile).Copy();
          if (DataTable_RenalClinicalOutcomesFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_RenalClinicalOutcomesFile.Rows)
            {
              RenalClinicalOutcomesFileUploadZipFileName = DataRow_Row["RenalClinicalOutcomes_FileUpload_ZipFileName"].ToString();
              RenalClinicalOutcomesFileUploadContentType = DataRow_Row["RenalClinicalOutcomes_FileUpload_ContentType"].ToString();
              RenalClinicalOutcomesFileUploadData = DataRow_Row["RenalClinicalOutcomes_FileUpload_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(RenalClinicalOutcomesFileUploadData.ToString()))
      {
        Byte[] Byte_FileData = (Byte[])RenalClinicalOutcomesFileUploadData;
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = RenalClinicalOutcomesFileUploadContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + RenalClinicalOutcomesFileUploadZipFileName + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      RenalClinicalOutcomesFileUploadZipFileName = "";
      RenalClinicalOutcomesFileUploadContentType = "";
      RenalClinicalOutcomesFileUploadData = "";
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

        string FinalURL = "Form_RenalClinicalOutcomes_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Renal Clinical Outcomes List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Renal Clinical Outcomes List", "Form_RenalClinicalOutcomes_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_RenalClinicalOutcomes_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_List.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_RenalClinicalOutcomes_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_RenalClinicalOutcomes_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_RenalClinicalOutcomes_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_RenalClinicalOutcomes_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_RenalClinicalOutcomes_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_RenalClinicalOutcomes_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_RenalClinicalOutcomes_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_RenalClinicalOutcomes_List.PageSize > 20 && GridView_RenalClinicalOutcomes_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_RenalClinicalOutcomes_List.PageSize > 50 && GridView_RenalClinicalOutcomes_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_RenalClinicalOutcomes_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_RenalClinicalOutcomes_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_RenalClinicalOutcomes_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_RenalClinicalOutcomes_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_RenalClinicalOutcomes_List_RowCreated(object sender, GridViewRowEventArgs e)
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


    //--START-- --Upload--//
    protected void SqlDataSource_RenalClinicalOutcomes_Upload_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Upload.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_Upload_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_RenalClinicalOutcomes_Upload.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Upload");
      GridView_RenalClinicalOutcomes_Upload.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_Upload_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_RenalClinicalOutcomes_Upload.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_Upload");
      GridView_RenalClinicalOutcomes_Upload.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_RenalClinicalOutcomes_Upload_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_RenalClinicalOutcomes_Upload.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Upload");
        if (GridView_RenalClinicalOutcomes_Upload.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_RenalClinicalOutcomes_Upload.PageSize > 20 && GridView_RenalClinicalOutcomes_Upload.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_RenalClinicalOutcomes_Upload.PageSize > 50 && GridView_RenalClinicalOutcomes_Upload.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_RenalClinicalOutcomes_Upload_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_RenalClinicalOutcomes_Upload.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_Upload");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_RenalClinicalOutcomes_Upload.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_RenalClinicalOutcomes_Upload.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_RenalClinicalOutcomes_Upload_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void LinkButton_RetrieveDatabaseFile_Upload_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_RetrieveDatabaseFile_Upload = (LinkButton)sender;
      ScriptManager ScriptManager_RetrieveDatabaseFile_Upload = ScriptManager.GetCurrent(Page);
      ScriptManager_RetrieveDatabaseFile_Upload.RegisterPostBackControl(LinkButton_RetrieveDatabaseFile_Upload);

      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_RetrieveDatabaseFile_Upload.CommandArgument.ToString().Split(';');
      string FileExist = CommandArgumentSplit[1];

      if (FileExist == "No")
      {
        LinkButton_RetrieveDatabaseFile_Upload.Visible = false;
      }
    }
    //---END--- --Upload--//
  }
}