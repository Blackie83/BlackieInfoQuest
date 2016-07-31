using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_CRM_PXM_Upload : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_CRM_PXM_Upload, this.GetType(), "UpdateProgress_Start", "Validation_Search();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          TextBox_CurrentDateFrom.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_CurrentDateFrom.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_CurrentDateFrom.Attributes.Add("OnInput", "Validation_Search();");
          TextBox_CurrentDateTo.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_CurrentDateTo.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_CurrentDateTo.Attributes.Add("OnInput", "Validation_Search();");

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " : PXM Upload", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " : PXM Upload", CultureInfo.CurrentCulture);
          Label_GridHeading_Event.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " PXM Created Event", CultureInfo.CurrentCulture);
          Label_GridHeading_Escalation.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " PXM Upload Escalations", CultureInfo.CurrentCulture);
          Label_GridHeading_Report.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " PXM Upload Reports", CultureInfo.CurrentCulture);

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_CRM_PXM_Upload.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Customer Relationship Management", "4");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_CRM_Event_FileUploaded.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_Event_FileUploaded.SelectCommand = "spForm_Get_CRM_PXMCreatedEvent";
      SqlDataSource_CRM_Event_FileUploaded.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_Event_FileUploaded.SelectParameters.Clear();
      SqlDataSource_CRM_Event_FileUploaded.SelectParameters.Add("CurrentDateFrom", TypeCode.String, Request.QueryString["s_CurrentDateFrom"]);
      SqlDataSource_CRM_Event_FileUploaded.SelectParameters.Add("CurrentDateTo", TypeCode.String, Request.QueryString["s_CurrentDateTo"]);

      SqlDataSource_CRM_Escalation_FileUploaded.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_Escalation_FileUploaded.SelectCommand = "spForm_Get_CRM_PXMUploadEscalation";
      SqlDataSource_CRM_Escalation_FileUploaded.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_Escalation_FileUploaded.SelectParameters.Clear();
      SqlDataSource_CRM_Escalation_FileUploaded.SelectParameters.Add("CurrentDateFrom", TypeCode.String, Request.QueryString["s_CurrentDateFrom"]);
      SqlDataSource_CRM_Escalation_FileUploaded.SelectParameters.Add("CurrentDateTo", TypeCode.String, Request.QueryString["s_CurrentDateTo"]);

      SqlDataSource_CRM_Report_FileUploaded.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_Report_FileUploaded.SelectCommand = "spForm_Get_CRM_PXMUploadReport";
      SqlDataSource_CRM_Report_FileUploaded.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_Report_FileUploaded.SelectParameters.Clear();
      SqlDataSource_CRM_Report_FileUploaded.SelectParameters.Add("CurrentDateFrom", TypeCode.String, Request.QueryString["s_CurrentDateFrom"]);
      SqlDataSource_CRM_Report_FileUploaded.SelectParameters.Add("CurrentDateTo", TypeCode.String, Request.QueryString["s_CurrentDateTo"]);
    }

    private void SetFormQueryString()
    {
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
        DivEvent.Visible = false;
        TableEvent.Visible = false;
        DivEscalation.Visible = false;
        TableEscalation.Visible = false;
        DivReport.Visible = false;
        TableReport.Visible = false;
      }
      else
      {
        DivEvent.Visible = true;
        TableEvent.Visible = true;
        DivEscalation.Visible = true;
        TableEscalation.Visible = true;
        DivReport.Visible = true;
        TableReport.Visible = true;
      }
    }

    protected void RetrieveDatabaseFile_Event(object sender, EventArgs e)
    {
      LinkButton LinkButton_PXMEventFile = (LinkButton)sender;
      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_PXMEventFile.CommandArgument.ToString().Split(';');
      string FileId = CommandArgumentSplit[0];

      string PXMPDCHEventFileCreatedZipFileName = "";
      string PXMPDCHEventFileCreatedContentType = "";
      object PXMPDCHEventFileCreatedData = new object();
      string SQLStringPXMEventFile = "SELECT PXM_PDCH_Event_FileCreated_ZipFileName , PXM_PDCH_Event_FileCreated_ContentType , PXM_PDCH_Event_FileCreated_Data FROM Form_PXM_PDCH_Event_FileCreated WHERE PXM_PDCH_Event_FileCreated_Id = @PXM_PDCH_Event_FileCreated_Id";
      using (SqlCommand SqlCommand_PXMEventFile = new SqlCommand(SQLStringPXMEventFile))
      {
        SqlCommand_PXMEventFile.Parameters.AddWithValue("@PXM_PDCH_Event_FileCreated_Id", FileId);
        DataTable DataTable_PXMEventFile;
        using (DataTable_PXMEventFile = new DataTable())
        {
          DataTable_PXMEventFile.Locale = CultureInfo.CurrentCulture;
          DataTable_PXMEventFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXMEventFile).Copy();
          if (DataTable_PXMEventFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PXMEventFile.Rows)
            {
              PXMPDCHEventFileCreatedZipFileName = DataRow_Row["PXM_PDCH_Event_FileCreated_ZipFileName"].ToString();
              PXMPDCHEventFileCreatedContentType = DataRow_Row["PXM_PDCH_Event_FileCreated_ContentType"].ToString();
              PXMPDCHEventFileCreatedData = DataRow_Row["PXM_PDCH_Event_FileCreated_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(PXMPDCHEventFileCreatedData.ToString()))
      {
        Byte[] Byte_FileData = (Byte[])PXMPDCHEventFileCreatedData;
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = PXMPDCHEventFileCreatedContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + PXMPDCHEventFileCreatedZipFileName + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      PXMPDCHEventFileCreatedZipFileName = "";
      PXMPDCHEventFileCreatedContentType = "";
      PXMPDCHEventFileCreatedData = "";
    }

    protected void RetrieveDatabaseFile_Escalation(object sender, EventArgs e)
    {
      LinkButton LinkButton_PXMEscalationFile = (LinkButton)sender;
      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_PXMEscalationFile.CommandArgument.ToString().Split(';');
      string FileId = CommandArgumentSplit[0];

      string PXMPDCHEscalationFileUploadedZipFileName = "";
      string PXMPDCHEscalationFileUploadedContentType = "";
      object PXMPDCHEscalationFileUploadedData = new object();
      string SQLStringPXMEscalationFile = "SELECT PXM_PDCH_Escalation_FileUploaded_ZipFileName ,PXM_PDCH_Escalation_FileUploaded_ContentType ,PXM_PDCH_Escalation_FileUploaded_Data FROM Form_PXM_PDCH_Escalation_FileUploaded WHERE PXM_PDCH_Escalation_FileUploaded_Id = @PXM_PDCH_Escalation_FileUploaded_Id";
      using (SqlCommand SqlCommand_PXMEscalationFile = new SqlCommand(SQLStringPXMEscalationFile))
      {
        SqlCommand_PXMEscalationFile.Parameters.AddWithValue("@PXM_PDCH_Escalation_FileUploaded_Id", FileId);
        DataTable DataTable_PXMEscalationFile;
        using (DataTable_PXMEscalationFile = new DataTable())
        {
          DataTable_PXMEscalationFile.Locale = CultureInfo.CurrentCulture;
          DataTable_PXMEscalationFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXMEscalationFile).Copy();
          if (DataTable_PXMEscalationFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PXMEscalationFile.Rows)
            {
              PXMPDCHEscalationFileUploadedZipFileName = DataRow_Row["PXM_PDCH_Escalation_FileUploaded_ZipFileName"].ToString();
              PXMPDCHEscalationFileUploadedContentType = DataRow_Row["PXM_PDCH_Escalation_FileUploaded_ContentType"].ToString();
              PXMPDCHEscalationFileUploadedData = DataRow_Row["PXM_PDCH_Escalation_FileUploaded_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(PXMPDCHEscalationFileUploadedData.ToString()))
      {
        Byte[] Byte_FileData = (Byte[])PXMPDCHEscalationFileUploadedData;
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = PXMPDCHEscalationFileUploadedContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + PXMPDCHEscalationFileUploadedZipFileName + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      PXMPDCHEscalationFileUploadedZipFileName = "";
      PXMPDCHEscalationFileUploadedContentType = "";
      PXMPDCHEscalationFileUploadedData = "";
    }

    protected void RetrieveDatabaseFile_Report(object sender, EventArgs e)
    {
      LinkButton LinkButton_PXMReportFile = (LinkButton)sender;
      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_PXMReportFile.CommandArgument.ToString().Split(';');
      string FileId = CommandArgumentSplit[0];

      string PXMPDCHReportFileUploadedZipFileName = "";
      string PXMPDCHReportFileUploadedContentType = "";
      object PXMPDCHReportFileUploadedData = new object();
      string SQLStringPXMReportFile = "SELECT PXM_PDCH_Report_FileUploaded_ZipFileName ,PXM_PDCH_Report_FileUploaded_ContentType ,PXM_PDCH_Report_FileUploaded_Data FROM Form_PXM_PDCH_Report_FileUploaded WHERE PXM_PDCH_Report_FileUploaded_Id = @PXM_PDCH_Report_FileUploaded_Id";
      using (SqlCommand SqlCommand_PXMReportFile = new SqlCommand(SQLStringPXMReportFile))
      {
        SqlCommand_PXMReportFile.Parameters.AddWithValue("@PXM_PDCH_Report_FileUploaded_Id", FileId);
        DataTable DataTable_PXMReportFile;
        using (DataTable_PXMReportFile = new DataTable())
        {
          DataTable_PXMReportFile.Locale = CultureInfo.CurrentCulture;
          DataTable_PXMReportFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PXMReportFile).Copy();
          if (DataTable_PXMReportFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PXMReportFile.Rows)
            {
              PXMPDCHReportFileUploadedZipFileName = DataRow_Row["PXM_PDCH_Report_FileUploaded_ZipFileName"].ToString();
              PXMPDCHReportFileUploadedContentType = DataRow_Row["PXM_PDCH_Report_FileUploaded_ContentType"].ToString();
              PXMPDCHReportFileUploadedData = DataRow_Row["PXM_PDCH_Report_FileUploaded_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(PXMPDCHReportFileUploadedData.ToString()))
      {
        Byte[] Byte_FileData = (Byte[])PXMPDCHReportFileUploadedData;
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = PXMPDCHReportFileUploadedContentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + PXMPDCHReportFileUploadedZipFileName + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      PXMPDCHReportFileUploadedZipFileName = "";
      PXMPDCHReportFileUploadedContentType = "";
      PXMPDCHReportFileUploadedData = "";
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

        string SearchField1 = Server.HtmlEncode(TextBox_CurrentDateFrom.Text);
        string SearchField2 = Server.HtmlEncode(TextBox_CurrentDateTo.Text);

        if (!string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "s_CurrentDateFrom=" + Server.HtmlEncode(TextBox_CurrentDateFrom.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "s_CurrentDateTo=" + Server.HtmlEncode(TextBox_CurrentDateTo.Text.ToString()) + "&";
        }

        string FinalURL = "Form_CRM_PXM_Upload.aspx?" + SearchField1 + SearchField2;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("CRM PXM Upload", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("CRM PXM Upload", "Form_CRM_PXM_Upload.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --Event--//
    protected void SqlDataSource_CRM_Event_FileUploaded_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Event.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_Event_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_Event_FileUploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Event");
      GridView_CRM_Event_FileUploaded.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_Event_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_CRM_Event_FileUploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_Event");
      GridView_CRM_Event_FileUploaded.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_CRM_Event_FileUploaded_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_Event_FileUploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Event");
        if (GridView_CRM_Event_FileUploaded.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_CRM_Event_FileUploaded.PageSize > 20 && GridView_CRM_Event_FileUploaded.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_CRM_Event_FileUploaded.PageSize > 50 && GridView_CRM_Event_FileUploaded.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_CRM_Event_FileUploaded_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_CRM_Event_FileUploaded.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_Event");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_CRM_Event_FileUploaded.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_CRM_Event_FileUploaded.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_CRM_Event_FileUploaded_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void LinkButton_RetrieveDatabaseFile_Event_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_RetrieveDatabaseFile_Event = (LinkButton)sender;
      ScriptManager ScriptManager_RetrieveDatabaseFile_Event = ScriptManager.GetCurrent(Page);
      ScriptManager_RetrieveDatabaseFile_Event.RegisterPostBackControl(LinkButton_RetrieveDatabaseFile_Event);

      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_RetrieveDatabaseFile_Event.CommandArgument.ToString().Split(';');
      string FileExist = CommandArgumentSplit[1];

      if (FileExist == "No")
      {
        LinkButton_RetrieveDatabaseFile_Event.Visible = false;
      }
    }
    //---END--- --Event--//


    //--START-- --Escalation--//
    protected void SqlDataSource_CRM_Escalation_FileUploaded_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Escalation.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_Escalation_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_Escalation_FileUploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Escalation");
      GridView_CRM_Escalation_FileUploaded.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_Escalation_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_CRM_Escalation_FileUploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_Escalation");
      GridView_CRM_Escalation_FileUploaded.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_CRM_Escalation_FileUploaded_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_Escalation_FileUploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Escalation");
        if (GridView_CRM_Escalation_FileUploaded.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_CRM_Escalation_FileUploaded.PageSize > 20 && GridView_CRM_Escalation_FileUploaded.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_CRM_Escalation_FileUploaded.PageSize > 50 && GridView_CRM_Escalation_FileUploaded.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_CRM_Escalation_FileUploaded_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_CRM_Escalation_FileUploaded.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_Escalation");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_CRM_Escalation_FileUploaded.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_CRM_Escalation_FileUploaded.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_CRM_Escalation_FileUploaded_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void LinkButton_RetrieveDatabaseFile_Escalation_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_RetrieveDatabaseFile_Escalation = (LinkButton)sender;
      ScriptManager ScriptManager_RetrieveDatabaseFile_Escalation = ScriptManager.GetCurrent(Page);
      ScriptManager_RetrieveDatabaseFile_Escalation.RegisterPostBackControl(LinkButton_RetrieveDatabaseFile_Escalation);

      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_RetrieveDatabaseFile_Escalation.CommandArgument.ToString().Split(';');
      string FileExist = CommandArgumentSplit[1];

      if (FileExist == "No")
      {
        LinkButton_RetrieveDatabaseFile_Escalation.Visible = false;
      }
    }
    //---END--- --Escalation--//


    //--START-- --Report--//
    protected void SqlDataSource_CRM_Report_FileUploaded_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_Report.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_Report_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_Report_FileUploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Report");
      GridView_CRM_Report_FileUploaded.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_Report_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_CRM_Report_FileUploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_Report");
      GridView_CRM_Report_FileUploaded.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_CRM_Report_FileUploaded_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_Report_FileUploaded.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Report");
        if (GridView_CRM_Report_FileUploaded.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_CRM_Report_FileUploaded.PageSize > 20 && GridView_CRM_Report_FileUploaded.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_CRM_Report_FileUploaded.PageSize > 50 && GridView_CRM_Report_FileUploaded.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_CRM_Report_FileUploaded_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_CRM_Report_FileUploaded.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_Report");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_CRM_Report_FileUploaded.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_CRM_Report_FileUploaded.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_CRM_Report_FileUploaded_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void LinkButton_RetrieveDatabaseFile_Report_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_RetrieveDatabaseFile_Report = (LinkButton)sender;
      ScriptManager ScriptManager_RetrieveDatabaseFile_Report = ScriptManager.GetCurrent(Page);
      ScriptManager_RetrieveDatabaseFile_Report.RegisterPostBackControl(LinkButton_RetrieveDatabaseFile_Report);

      string[] CommandArgumentSplit;
      CommandArgumentSplit = LinkButton_RetrieveDatabaseFile_Report.CommandArgument.ToString().Split(';');
      string FileExist = CommandArgumentSplit[1];

      if (FileExist == "No")
      {
        LinkButton_RetrieveDatabaseFile_Report.Visible = false;
      }
    }
    //---END--- --Report--//
  }
}