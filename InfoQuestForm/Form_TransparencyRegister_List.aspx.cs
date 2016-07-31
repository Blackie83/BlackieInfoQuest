#define INCLUDE_WEB_FUNCTIONS

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Text;
using System.IO;

namespace InfoQuestForm
{
  public partial class Form_TransparencyRegister_List : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("44").Replace(" Form", "")).ToString() + " : Captured Forms", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("44").Replace(" Form", "")).ToString() + "s", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("44").Replace(" Form", "")).ToString() + "s", CultureInfo.CurrentCulture);

          SetFormQueryString();

          SetFormVisibility();

          RegisterPostBackControl();
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('44'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("44");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_TransparencyRegister_List.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Transparency Register", "18");
      }
    }

    private void SqlDataSourceSetup()
    {
      Session["DataTable_EmployeeManager"] = "";

      string CurrentEmployeeNumber = "";
      string Error = "";
      DataTable DataTable_CurrentEmployeeNumber = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(Request.ServerVariables["LOGON_USER"]);
      if (DataTable_CurrentEmployeeNumber.Columns.Count == 1)
      {
        foreach (DataRow DataRow_Row in DataTable_CurrentEmployeeNumber.Rows)
        {
          Error = DataRow_Row["Error"].ToString();
          if (Error == "No Employee Data")
          {
            Error = "Employee number not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Active Directory";
          }
        }
      }
      else if (DataTable_CurrentEmployeeNumber.Columns.Count != 1)
      {
        if (DataTable_CurrentEmployeeNumber.Rows.Count > 0)
        {
          foreach (DataRow DataRow_CurrentEmployeeNumber in DataTable_CurrentEmployeeNumber.Rows)
          {
            CurrentEmployeeNumber = DataRow_CurrentEmployeeNumber["EmployeeNumber"].ToString();
          }
        }
        else
        {
          Error = "Employee number not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Active Directory";
        }
      }

      if (string.IsNullOrEmpty(Error))
      {
        DataTable DataTable_DataEmployee;
        using (DataTable_DataEmployee = new DataTable())
        {
          DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
          DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_FindEmployeeInfo_SearchEmployeeNumber(CurrentEmployeeNumber).Copy();
          if (DataTable_DataEmployee.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Error = DataRow_Row["Error"].ToString();
              if (Error == "No Employee Data")
              {
                Error = "Employee detail not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Vision";
              }
            }
          }
          else if (DataTable_DataEmployee.Columns.Count != 1)
          {
            if (DataTable_DataEmployee.Rows.Count == 0)
            {
              Error = "Employee detail not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Vision";
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Error))
      {
        Label_SearchErrorMessage.Text = Convert.ToString("<div style='color:#B0262E;'>" + Error + "</div>", CultureInfo.CurrentCulture);

        DataTable DataTable_EmployeeManager;
        using (DataTable_EmployeeManager = new DataTable())
        {
          DataTable_EmployeeManager.Locale = CultureInfo.CurrentCulture;
          DataTable_EmployeeManager.Reset();
          DataTable_EmployeeManager.Columns.Add("EmployeeNo", typeof(string));
          DataTable_EmployeeManager.Columns.Add("EmployeeManager", typeof(string));
          Session["DataTable_EmployeeManager"] = DataTable_EmployeeManager;
        }
      }
      else
      {
        Label_SearchErrorMessage.Text = "";

        DataTable DataTable_EmployeeManager;
        using (DataTable_EmployeeManager = new DataTable())
        {
          DataTable_EmployeeManager.Locale = CultureInfo.CurrentCulture;
          DataTable_EmployeeManager = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_TransparencyRegister_List_EmployeeManager(CurrentEmployeeNumber).Copy();
          Session["DataTable_EmployeeManager"] = DataTable_EmployeeManager;
        }
      }

      CurrentEmployeeNumber = "";
      Error = "";          

      SessionParameter EmployeeManager = new SessionParameter();
      EmployeeManager.Name = "EmployeeManager";
      EmployeeManager.SessionField = "DataTable_EmployeeManager";

      SqlDataSource_TransparencyRegister_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_TransparencyRegister_List.SelectCommand = "spForm_Get_TransparencyRegister_List";
      SqlDataSource_TransparencyRegister_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_TransparencyRegister_List.CancelSelectOnNullParameter = false;
      SqlDataSource_TransparencyRegister_List.SelectParameters.Clear();
      SqlDataSource_TransparencyRegister_List.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_TransparencyRegister_List.SelectParameters.Add(EmployeeManager);
      SqlDataSource_TransparencyRegister_List.SelectParameters.Add("Name", TypeCode.String, Request.QueryString["s_TransparencyRegister_Name"]);
      SqlDataSource_TransparencyRegister_List.SelectParameters.Add("EmployeeNumber", TypeCode.String, Request.QueryString["s_TransparencyRegister_EmployeeNumber"]);
      SqlDataSource_TransparencyRegister_List.SelectParameters.Add("Status", TypeCode.String, Request.QueryString["s_TransparencyRegister_Status"]);
      SqlDataSource_TransparencyRegister_List.SelectParameters.Add("ShowAll", TypeCode.String, Request.QueryString["s_TransparencyRegister_ShowAll"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(TextBox_Name.Text.ToString()))
      {
        if (Request.QueryString["s_TransparencyRegister_Name"] == null)
        {
          TextBox_Name.Text = "";
        }
        else
        {
          TextBox_Name.Text = Request.QueryString["s_TransparencyRegister_Name"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_EmployeeNumber.Text.ToString()))
      {
        if (Request.QueryString["s_TransparencyRegister_EmployeeNumber"] == null)
        {
          TextBox_EmployeeNumber.Text = "";
        }
        else
        {
          TextBox_EmployeeNumber.Text = Request.QueryString["s_TransparencyRegister_EmployeeNumber"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Status.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_TransparencyRegister_Status"] == null)
        {
          DropDownList_Status.SelectedValue = "";
        }
        else
        {
          DropDownList_Status.SelectedValue = Request.QueryString["s_TransparencyRegister_Status"];
        }
      }

      if (ShowAll.Visible == true)
      {
        if (Request.QueryString["s_TransparencyRegister_ShowAll"] == null || Request.QueryString["s_TransparencyRegister_ShowAll"] == "No")
        {
          CheckBox_ShowAll.Checked = false;
        }
        else
        {
          CheckBox_ShowAll.Checked = true;
        }
      }
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('44'))";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '176'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '177'");

            string Security = "1";
            if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFormAdminView.Length > 0))
            {
              Security = "0";

              ShowAll.Visible = true;
            }

            if (Security == "1")
            {
              Security = "0";

              ShowAll.Visible = false;
            }

            Security = "1";
          }
          else
          {
            ShowAll.Visible = false;
          }
        }
      }
    }

    protected void RegisterPostBackControl()
    {
      if (GridView_TransparencyRegister_List.Rows.Count > 0)
      {
        Button Button_Export = (Button)GridView_TransparencyRegister_List.BottomPagerRow.Cells[0].FindControl("Button_Export");

        ScriptManager ScriptManager_Page = ScriptManager.GetCurrent(Page);

        ScriptManager_Page.RegisterPostBackControl(Button_Export);
      }
    }


    private DataSet GetExcelExportData()
    {
      Session["DataTable_EmployeeManager"] = "";

      string CurrentEmployeeNumber = "";
      string Error = "";
      DataTable DataTable_CurrentEmployeeNumber = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(Request.ServerVariables["LOGON_USER"]);
      if (DataTable_CurrentEmployeeNumber.Columns.Count == 1)
      {
        foreach (DataRow DataRow_Row in DataTable_CurrentEmployeeNumber.Rows)
        {
          Error = DataRow_Row["Error"].ToString();
          if (Error == "No Employee Data")
          {
            Error = "Employee number not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Active Directory";
          }
        }
      }
      else if (DataTable_CurrentEmployeeNumber.Columns.Count != 1)
      {
        if (DataTable_CurrentEmployeeNumber.Rows.Count > 0)
        {
          foreach (DataRow DataRow_CurrentEmployeeNumber in DataTable_CurrentEmployeeNumber.Rows)
          {
            CurrentEmployeeNumber = DataRow_CurrentEmployeeNumber["EmployeeNumber"].ToString();
          }
        }
        else
        {
          Error = "Employee number not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Active Directory";
        }
      }

      if (string.IsNullOrEmpty(Error))
      {
        DataTable DataTable_DataEmployee;
        using (DataTable_DataEmployee = new DataTable())
        {
          DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
          DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_FindEmployeeInfo_SearchEmployeeNumber(CurrentEmployeeNumber).Copy();
          if (DataTable_DataEmployee.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Error = DataRow_Row["Error"].ToString();
              if (Error == "No Employee Data")
              {
                Error = "Employee detail not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Vision";
              }
            }
          }
          else if (DataTable_DataEmployee.Columns.Count != 1)
          {
            if (DataTable_DataEmployee.Rows.Count == 0)
            {
              Error = "Employee detail not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Vision";
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Error))
      {
        Label_SearchErrorMessage.Text = Convert.ToString("<div style='color:#B0262E;'>" + Error + "</div>", CultureInfo.CurrentCulture);

        DataTable DataTable_EmployeeManager;
        using (DataTable_EmployeeManager = new DataTable())
        {
          DataTable_EmployeeManager.Locale = CultureInfo.CurrentCulture;
          DataTable_EmployeeManager.Reset();
          DataTable_EmployeeManager.Columns.Add("EmployeeNo", typeof(string));
          DataTable_EmployeeManager.Columns.Add("EmployeeManager", typeof(string));
          Session["DataTable_EmployeeManager"] = DataTable_EmployeeManager;
        }
      }
      else
      {
        Label_SearchErrorMessage.Text = "";

        DataTable DataTable_EmployeeManager;
        using (DataTable_EmployeeManager = new DataTable())
        {
          DataTable_EmployeeManager.Locale = CultureInfo.CurrentCulture;
          DataTable_EmployeeManager = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_TransparencyRegister_List_EmployeeManager(CurrentEmployeeNumber).Copy();
          Session["DataTable_EmployeeManager"] = DataTable_EmployeeManager;
        }
      }

      CurrentEmployeeNumber = "";
      Error = "";

      DataSet DataSet_ReturnData;
      using (DataSet_ReturnData = new DataSet())
      {
        DataSet_ReturnData.Locale = CultureInfo.CurrentCulture;
        string SQLStringTransparencyRegisterExcelExport = "spForm_Get_TransparencyRegister_ExcelExport @SecurityUser , @EmployeeManager , @Name , @EmployeeNumber , @Status , @ShowAll";
        using (SqlCommand SqlCommand_TransparencyRegisterExcelExport = new SqlCommand(SQLStringTransparencyRegisterExcelExport))
        {
          SqlCommand_TransparencyRegisterExcelExport.CommandType = CommandType.StoredProcedure;
          SqlCommand_TransparencyRegisterExcelExport.Parameters.AddWithValue("@SecurityUser", Request.ServerVariables["LOGON_USER"]);
          SqlParameter SqlParameter_TransparencyRegisterExcelExport = SqlCommand_TransparencyRegisterExcelExport.Parameters.AddWithValue("@EmployeeManager", Session["DataTable_EmployeeManager"]);
          SqlParameter_TransparencyRegisterExcelExport.SqlDbType = SqlDbType.Structured;
          SqlParameter_TransparencyRegisterExcelExport.TypeName = "tForm_TransparencyRegister_EmployeeManager";
          SqlCommand_TransparencyRegisterExcelExport.Parameters.AddWithValue("@Name", Request.QueryString["s_TransparencyRegister_Name"]);
          SqlCommand_TransparencyRegisterExcelExport.Parameters.AddWithValue("@EmployeeNumber", Request.QueryString["s_TransparencyRegister_EmployeeNumber"]);
          SqlCommand_TransparencyRegisterExcelExport.Parameters.AddWithValue("@Status", Request.QueryString["s_TransparencyRegister_Status"]);
          SqlCommand_TransparencyRegisterExcelExport.Parameters.AddWithValue("@ShowAll", Request.QueryString["s_TransparencyRegister_ShowAll"]);
          DataTable DataTable_TransparencyRegisterExcelExport;
          using (DataTable_TransparencyRegisterExcelExport = new DataTable())
          {
            DataTable_TransparencyRegisterExcelExport.Locale = CultureInfo.CurrentCulture;
            DataTable_TransparencyRegisterExcelExport = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TransparencyRegisterExcelExport).Copy();

            DataSet_ReturnData.Tables.Add(DataTable_TransparencyRegisterExcelExport);
          }
        }
      }

      return DataSet_ReturnData;
    }

    private static void CreateExcelFile(DataSet dataSet_Export, SpreadsheetDocument spreadsheetDocument_Export)
    {
      spreadsheetDocument_Export.AddWorkbookPart();
      spreadsheetDocument_Export.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
      spreadsheetDocument_Export.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

      WorkbookStylesPart WorkbookStylesPart_Export = spreadsheetDocument_Export.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
      Stylesheet Stylesheet_Export = new Stylesheet();
      WorkbookStylesPart_Export.Stylesheet = Stylesheet_Export;

      uint worksheetNumber = 1;
      Sheets Sheets_Export = spreadsheetDocument_Export.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
      foreach (DataTable DataTable_Export in dataSet_Export.Tables)
      {
        string worksheetName = DataTable_Export.TableName;

        WorksheetPart WorksheetPart_Export = spreadsheetDocument_Export.WorkbookPart.AddNewPart<WorksheetPart>();
        Sheet Sheet_Export = new Sheet() { Id = spreadsheetDocument_Export.WorkbookPart.GetIdOfPart(WorksheetPart_Export), SheetId = worksheetNumber, Name = worksheetName };
        Sheets_Export.Append(Sheet_Export);
        AddExcelFileData(DataTable_Export, WorksheetPart_Export);
        worksheetNumber++;
      }

      spreadsheetDocument_Export.WorkbookPart.Workbook.Save();
    }

    private static void AddExcelFileData(DataTable dataTable_Export, WorksheetPart worksheetPart_Export)
    {
      OpenXmlWriter OpenXmlWriter_Export;
      using (OpenXmlWriter_Export = OpenXmlWriter.Create(worksheetPart_Export, Encoding.ASCII))
      {
        OpenXmlWriter_Export.WriteStartElement(new Worksheet());
        OpenXmlWriter_Export.WriteStartElement(new SheetData());

        int numberOfColumns = dataTable_Export.Columns.Count;
        bool[] IsNumericColumn = new bool[numberOfColumns];
        bool[] IsDateColumn = new bool[numberOfColumns];
        string[] excelColumnNames = new string[numberOfColumns];
        for (int n = 0; n < numberOfColumns; n++)
        {
          excelColumnNames[n] = GetExcelColumnName(n);
        }

        uint rowIndex = 1;
        OpenXmlWriter_Export.WriteStartElement(new Row { RowIndex = rowIndex });
        for (int colInx = 0; colInx < numberOfColumns; colInx++)
        {
          DataColumn DataColumn_Export = dataTable_Export.Columns[colInx];
          OpenXmlWriter_Export.WriteElement(new Cell { CellValue = new CellValue(DataColumn_Export.ColumnName), CellReference = excelColumnNames[colInx] + "1", DataType = CellValues.String });
          IsNumericColumn[colInx] = (DataColumn_Export.DataType.FullName == "System.Decimal") || (DataColumn_Export.DataType.FullName == "System.Int32") || (DataColumn_Export.DataType.FullName == "System.Double") || (DataColumn_Export.DataType.FullName == "System.Single");
          IsDateColumn[colInx] = (DataColumn_Export.DataType.FullName == "System.DateTime");
        }

        OpenXmlWriter_Export.WriteEndElement();
        foreach (DataRow DataRow_Export in dataTable_Export.Rows)
        {
          ++rowIndex;
          OpenXmlWriter_Export.WriteStartElement(new Row { RowIndex = rowIndex });
          for (int colInx = 0; colInx < numberOfColumns; colInx++)
          {
            OpenXmlWriter_Export.WriteElement(new Cell { CellValue = new CellValue(DataRow_Export.ItemArray[colInx].ToString()), CellReference = excelColumnNames[colInx] + rowIndex.ToString(CultureInfo.CurrentCulture), DataType = CellValues.String });
          }

          OpenXmlWriter_Export.WriteEndElement();
        }

        OpenXmlWriter_Export.WriteEndElement();
        OpenXmlWriter_Export.WriteEndElement();
      }
    }

    private static string GetExcelColumnName(int columnIndex)
    {
      if (columnIndex < 26)
      {
        return ((char)('A' + columnIndex)).ToString();
      }

      char firstChar = (char)('A' + (columnIndex / 26) - 1);
      char secondChar = (char)('A' + (columnIndex % 26));

      return string.Format(CultureInfo.CurrentCulture, "{0}{1}", firstChar, secondChar);
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchErrorMessage = "";
      string ValidSearch = "Yes";

      if (ValidSearch == "No")
      {
        Label_SearchErrorMessage.Text = Convert.ToString(SearchErrorMessage, CultureInfo.CurrentCulture);
      }
      else
      {
        Label_SearchErrorMessage.Text = "";

        string SearchField1 = Server.HtmlEncode(TextBox_Name.Text);
        string SearchField2 = Server.HtmlEncode(TextBox_EmployeeNumber.Text);
        string SearchField3 = DropDownList_Status.SelectedValue;
        string SearchField4 = "";
        if (ShowAll.Visible == true)
        {
          SearchField4 = CheckBox_ShowAll.Checked.ToString() == "True" ? "Yes" : "No";
        }

        if (!string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "s_TransparencyRegister_Name=" + Server.HtmlEncode(TextBox_Name.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "s_TransparencyRegister_EmployeeNumber=" + Server.HtmlEncode(TextBox_EmployeeNumber.Text.ToString()) + "&";
        }

        if (!string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "s_TransparencyRegister_Status=" + DropDownList_Status.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "s_TransparencyRegister_ShowAll=" + SearchField4 + "&";
        }

        string FinalURL = "Form_TransparencyRegister_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register Captured Forms", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register List", "Form_TransparencyRegister_List.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_TransparencyRegister_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_TransparencyRegister_List.PageSize = Convert.ToInt32(((DropDownList)GridView_TransparencyRegister_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_TransparencyRegister_List.PageIndex = ((DropDownList)GridView_TransparencyRegister_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_TransparencyRegister_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_TransparencyRegister_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_TransparencyRegister_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_TransparencyRegister_List.PageSize > 20 && GridView_TransparencyRegister_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_TransparencyRegister_List.PageSize > 50 && GridView_TransparencyRegister_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }

      for (int i = 0; i < GridView_TransparencyRegister_List.Rows.Count; i++)
      {
        if (GridView_TransparencyRegister_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_List.Rows[i].Cells[5].Text == "Pending Approval")
          {
            GridView_TransparencyRegister_List.Rows[i].Cells[5].BackColor = System.Drawing.Color.FromName("#ffcc66");
            GridView_TransparencyRegister_List.Rows[i].Cells[5].ForeColor = System.Drawing.Color.FromName("#333333");
          }
          else if (GridView_List.Rows[i].Cells[5].Text == "Approved")
          {
            GridView_TransparencyRegister_List.Rows[i].Cells[5].BackColor = System.Drawing.Color.FromName("#77cf9c");
            GridView_TransparencyRegister_List.Rows[i].Cells[5].ForeColor = System.Drawing.Color.FromName("#333333");
          }
          else if (GridView_List.Rows[i].Cells[5].Text == "Rejected")
          {
            GridView_TransparencyRegister_List.Rows[i].Cells[5].BackColor = System.Drawing.Color.FromName("#d46e6e");
            GridView_TransparencyRegister_List.Rows[i].Cells[5].ForeColor = System.Drawing.Color.FromName("#333333");
          }
          else
          {
            GridView_TransparencyRegister_List.Rows[i].Cells[5].BackColor = System.Drawing.Color.FromName("#ffcc66");
            GridView_TransparencyRegister_List.Rows[i].Cells[5].ForeColor = System.Drawing.Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_TransparencyRegister_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_TransparencyRegister_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_TransparencyRegister_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_TransparencyRegister_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_TransparencyRegister_List_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register New Form", "Form_TransparencyRegister.aspx"), false);
    }

    protected void Button_Export_Click(object sender, EventArgs e)
    {
      string Filename = "TransparencyRegister_Export_" + DateTime.Now.ToString("yyyyMMdd_hhmmss", CultureInfo.CurrentCulture) + ".xlsx";
      DataSet DataSet_Export = GetExcelExportData();

      MemoryStream MemoryStream_Export = new MemoryStream();
      using (SpreadsheetDocument SpreadsheetDocument_Export = SpreadsheetDocument.Create(MemoryStream_Export, SpreadsheetDocumentType.Workbook, true))
      {
        CreateExcelFile(DataSet_Export, SpreadsheetDocument_Export);
      }

      MemoryStream_Export.Flush();
      MemoryStream_Export.Position = 0;
      Byte[] Byte_Export = new Byte[MemoryStream_Export.Length];
      MemoryStream_Export.Read(Byte_Export, 0, Byte_Export.Length);
      MemoryStream_Export.Close();

      Response.ClearContent();
      Response.Clear();
      Response.Buffer = true;
      Response.Charset = "";
      Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
      Response.AddHeader("content-disposition", "attachment; filename=" + Filename);
      Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";      
      Response.BinaryWrite(Byte_Export);
      Response.Flush();
      Response.End();
    }

    public string GetLink(object transparencyRegister_Id, object transparencyRegister_Status, object employeeManager)
    {
      string LinkURL = "";
      if (transparencyRegister_Status != null && employeeManager != null)
      {
        if (employeeManager.ToString() == "1" && transparencyRegister_Status.ToString() == "Pending Approval")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register New Form", "Form_TransparencyRegister.aspx?TransparencyRegister_Id=" + transparencyRegister_Id + "") + "'>Update</a>";
        }
        else if (employeeManager.ToString() == "3")
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register New Form", "Form_TransparencyRegister.aspx?TransparencyRegister_Id=" + transparencyRegister_Id + "") + "'>View</a>";
        }
        else
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register New Form", "Form_TransparencyRegister.aspx?TransparencyRegister_Id=" + transparencyRegister_Id + "") + "'>View</a>";
        }
      }

      string SearchField1 = Request.QueryString["s_TransparencyRegister_Name"];
      string SearchField2 = Request.QueryString["s_TransparencyRegister_EmployeeNumber"];
      string SearchField3 = Request.QueryString["s_TransparencyRegister_Status"];
      string SearchField4 = Request.QueryString["s_TransparencyRegister_ShowAll"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "Search_TransparencyRegisterName=" + Request.QueryString["s_TransparencyRegister_Name"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "Search_TransparencyRegisterEmployeeNumber=" + Request.QueryString["s_TransparencyRegister_EmployeeNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "Search_TransparencyRegisterStatus=" + Request.QueryString["s_TransparencyRegister_Status"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "Search_TransparencyRegisterShowAll=" + Request.QueryString["s_TransparencyRegister_ShowAll"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4;
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