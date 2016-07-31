using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace InfoQuestForm
{
  public partial class Form_CRM_IncompleteOther : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " : Bulk Acknowledgement & Close Out", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_BulkApprovalHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("36").Replace(" Form", "")).ToString() + " : Bulk Approval", CultureInfo.CurrentCulture);

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_CRM_IncompleteOther.FindControl("Label_UpdateProgress");
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
      SqlDataSource_CRM_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_CRM_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_Type.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_Type.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_CRM_Type.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_Type.SelectParameters.Clear();
      SqlDataSource_CRM_Type.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_Type.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "110");
      SqlDataSource_CRM_Type.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_CRM_Type.SelectParameters.Add("TableSELECT", TypeCode.String, "CRM_Type_List");
      SqlDataSource_CRM_Type.SelectParameters.Add("TableFROM", TypeCode.String, "Form_CRM");
      SqlDataSource_CRM_Type.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_CRM_IncompleteOther.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_IncompleteOther.SelectCommand = "spForm_Get_CRM_IncompleteOther";
      SqlDataSource_CRM_IncompleteOther.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_IncompleteOther.UpdateCommand = "UPDATE Form_CRM SET CRM_Compliment_Unit_Id = @CRM_Compliment_Unit_Id ,CRM_Compliment_Acknowledge = @CRM_Compliment_Acknowledge ,CRM_Compliment_AcknowledgeDate = @CRM_Compliment_AcknowledgeDate ,CRM_Compliment_AcknowledgeBy = @CRM_Compliment_AcknowledgeBy ,CRM_Compliment_CloseOut = @CRM_Compliment_CloseOut ,CRM_Compliment_CloseOutDate = @CRM_Compliment_CloseOutDate ,CRM_Compliment_CloseOutBy = @CRM_Compliment_CloseOutBy ,CRM_Comment_Type_List = @CRM_Comment_Type_List ,CRM_Comment_Unit_Id = @CRM_Comment_Unit_Id ,CRM_Comment_Category_List = @CRM_Comment_Category_List, CRM_Comment_AdditionalType_List = @CRM_Comment_AdditionalType_List , CRM_Comment_AdditionalUnit_Id = @CRM_Comment_AdditionalUnit_Id ,CRM_Comment_AdditionalCategory_List = @CRM_Comment_AdditionalCategory_List ,CRM_Comment_Acknowledge = @CRM_Comment_Acknowledge ,CRM_Comment_AcknowledgeDate = @CRM_Comment_AcknowledgeDate ,CRM_Comment_AcknowledgeBy = @CRM_Comment_AcknowledgeBy ,CRM_Comment_CloseOut = @CRM_Comment_CloseOut ,CRM_Comment_CloseOutDate = @CRM_Comment_CloseOutDate ,CRM_Comment_CloseOutBy = @CRM_Comment_CloseOutBy ,CRM_Query_Unit_Id = @CRM_Query_Unit_Id ,CRM_Query_Acknowledge = @CRM_Query_Acknowledge ,CRM_Query_AcknowledgeDate = @CRM_Query_AcknowledgeDate ,CRM_Query_AcknowledgeBy = @CRM_Query_AcknowledgeBy ,CRM_Query_CloseOut = @CRM_Query_CloseOut ,CRM_Query_CloseOutDate = @CRM_Query_CloseOutDate ,CRM_Query_CloseOutBy = @CRM_Query_CloseOutBy ,CRM_Suggestion_Unit_Id = @CRM_Suggestion_Unit_Id ,CRM_Suggestion_Acknowledge = @CRM_Suggestion_Acknowledge ,CRM_Suggestion_AcknowledgeDate = @CRM_Suggestion_AcknowledgeDate , CRM_Suggestion_AcknowledgeBy = @CRM_Suggestion_AcknowledgeBy , CRM_Suggestion_CloseOut = @CRM_Suggestion_CloseOut ,CRM_Suggestion_CloseOutDate = @CRM_Suggestion_CloseOutDate ,CRM_Suggestion_CloseOutBy = @CRM_Suggestion_CloseOutBy ,CRM_ModifiedDate = @CRM_ModifiedDate ,CRM_ModifiedBy = @CRM_ModifiedBy ,CRM_History = @CRM_History WHERE CRM_Id = @CRM_Id";
      SqlDataSource_CRM_IncompleteOther.CancelSelectOnNullParameter = false;
      SqlDataSource_CRM_IncompleteOther.SelectParameters.Clear();
      SqlDataSource_CRM_IncompleteOther.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_IncompleteOther.SelectParameters.Add("FacilityType", TypeCode.String, Request.QueryString["s_Facility_Type"]);
      SqlDataSource_CRM_IncompleteOther.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_CRM_IncompleteOther.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_CRM_ReportNumber"]);
      SqlDataSource_CRM_IncompleteOther.SelectParameters.Add("TypeList", TypeCode.String, Request.QueryString["s_CRM_TypeList"]);
      SqlDataSource_CRM_IncompleteOther.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_CRM_PatientVisitNumber"]);
      SqlDataSource_CRM_IncompleteOther.SelectParameters.Add("Name", TypeCode.String, Request.QueryString["s_CRM_Name"]);
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Clear();
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Compliment_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Compliment_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Compliment_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Compliment_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Compliment_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Compliment_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Compliment_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_Type_List", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_Category_List", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_AdditionalType_List", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_AdditionalUnit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_AdditionalCategory_List", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Comment_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Query_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Query_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Query_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Query_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Query_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Query_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Query_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Suggestion_Unit_Id", TypeCode.Int32, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Suggestion_Acknowledge", TypeCode.Boolean, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Suggestion_AcknowledgeDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Suggestion_AcknowledgeBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Suggestion_CloseOut", TypeCode.Boolean, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Suggestion_CloseOutDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_Suggestion_CloseOutBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_ModifiedBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOther.UpdateParameters.Add("CRM_History", TypeCode.String, "");

      SqlDataSource_CRM_IncompleteOtherBulkApproval.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectCommand = "spForm_Get_CRM_IncompleteOtherBulkApproval";
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_IncompleteOtherBulkApproval.UpdateCommand = "UPDATE Form_CRM SET CRM_Status = @CRM_Status , CRM_StatusDate = @CRM_StatusDate , CRM_StatusRejectedReason = @CRM_StatusRejectedReason , CRM_ModifiedBy = @CRM_ModifiedBy , CRM_ModifiedDate = @CRM_ModifiedDate , CRM_History = @CRM_History WHERE CRM_Id = @CRM_Id";
      SqlDataSource_CRM_IncompleteOtherBulkApproval.CancelSelectOnNullParameter = false;
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectParameters.Clear();
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectParameters.Add("FacilityType", TypeCode.String, Request.QueryString["s_Facility_Type"]);
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectParameters.Add("ReportNumber", TypeCode.String, Request.QueryString["s_CRM_ReportNumber"]);
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectParameters.Add("TypeList", TypeCode.String, Request.QueryString["s_CRM_TypeList"]);
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_CRM_PatientVisitNumber"]);
      SqlDataSource_CRM_IncompleteOtherBulkApproval.SelectParameters.Add("Name", TypeCode.String, Request.QueryString["s_CRM_Name"]);
      SqlDataSource_CRM_IncompleteOtherBulkApproval.UpdateParameters.Clear();
      SqlDataSource_CRM_IncompleteOtherBulkApproval.UpdateParameters.Add("CRM_Status", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOtherBulkApproval.UpdateParameters.Add("CRM_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOtherBulkApproval.UpdateParameters.Add("CRM_StatusRejectedReason", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOtherBulkApproval.UpdateParameters.Add("CRM_ModifiedBy", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOtherBulkApproval.UpdateParameters.Add("CRM_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_CRM_IncompleteOtherBulkApproval.UpdateParameters.Add("CRM_History", TypeCode.String, "");
      SqlDataSource_CRM_IncompleteOtherBulkApproval.UpdateParameters.Add("CRM_Id", TypeCode.Int32, "");
    }

    protected void SetFormQueryString()
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

      if (string.IsNullOrEmpty(DropDownList_Type.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_CRM_TypeList"]))
        {
          DropDownList_Type.SelectedValue = "";
        }
        else
        {
          DropDownList_Type.SelectedValue = Request.QueryString["s_CRM_TypeList"];
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

    protected static void SqlCommand_IsNullOrEmpty(SqlCommand argumentValue, string argumentName)
    {
      if (argumentValue == null)
        throw new ArgumentException("The value can't be null or empty", argumentName);
    }

    protected static void Control_IsNullOrEmpty(Control argumentValue, string argumentName)
    {
      if (argumentValue == null)
        throw new ArgumentException("The value can't be null or empty", argumentName);
    }

    protected void PXM_PDCH_Results(string crmId)
    {
      if (!string.IsNullOrEmpty(crmId))
      {
        string Survey = "";
        string SQLStringSurvey = "SELECT SUBSTRING(CRM_UploadedFrom,0,CHARINDEX(' :',CRM_UploadedFrom)) AS Survey FROM Form_CRM WHERE CRM_Id = @CRM_Id";
        using (SqlCommand SqlCommand_Survey = new SqlCommand(SQLStringSurvey))
        {
          SqlCommand_Survey.Parameters.AddWithValue("@CRM_Id", crmId);
          DataTable DataTable_Survey;
          using (DataTable_Survey = new DataTable())
          {
            DataTable_Survey.Locale = CultureInfo.CurrentCulture;
            DataTable_Survey = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Survey).Copy();
            if (DataTable_Survey.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Survey.Rows)
              {
                Survey = DataRow_Row["Survey"].ToString();
              }
            }
          }
        }

        var HeaderFont = FontFactory.GetFont("Verdana, Geneva, sans-serif", 20, new BaseColor(0, 0, 0));
        var TableHeaderFont = FontFactory.GetFont("Verdana, Geneva, sans-serif", 16, new BaseColor(255, 255, 255));
        var TableCellFont = FontFactory.GetFont("Verdana, Geneva, sans-serif", 12, new BaseColor(0, 0, 0));

        Paragraph Paragraph_Space = new Paragraph(" ");
        Paragraph Paragraph_Heading;
        if (string.IsNullOrEmpty(Survey))
        {
          Paragraph_Heading = new Paragraph("Survey", HeaderFont);
        }
        else
        {
          Paragraph_Heading = new Paragraph(Survey, HeaderFont);
        }

        iTextSharp.text.Image Image_Logo = iTextSharp.text.Image.GetInstance(Server.MapPath("App_Images/Logos/Life Healthcare/14_logo_2_col_blue_red.jpg"));
        Image_Logo.ScalePercent(50f);

        PdfPTable PdfPTable_Survey = new PdfPTable(2);
        float[] Cell_Widths = new float[] { 8f, 2f };
        PdfPTable_Survey.SetWidths(Cell_Widths);
        PdfPTable_Survey.HorizontalAlignment = 0;

        PdfPCell PdfPCell_Question = new PdfPCell(new Phrase("Question", TableHeaderFont));
        PdfPCell_Question.BackgroundColor = new BaseColor(0, 55, 104);

        PdfPCell PdfPCell_Answer = new PdfPCell(new Phrase("Answer", TableHeaderFont));
        PdfPCell_Answer.BackgroundColor = new BaseColor(0, 55, 104);

        PdfPTable_Survey.AddCell(PdfPCell_Question);
        PdfPTable_Survey.AddCell(PdfPCell_Answer);

        string SQLStringExportDataToPDF = "SELECT CRM_PXM_PDCH_Result_Question AS Question , CRM_PXM_PDCH_Result_Answer AS Answer FROM Form_CRM_PXM_PDCH_Result WHERE CRM_Id = @CRM_Id";
        using (SqlCommand SqlCommand_ExportDataToPDF = new SqlCommand(SQLStringExportDataToPDF))
        {
          SqlCommand_ExportDataToPDF.Parameters.AddWithValue("@CRM_Id", crmId);
          DataTable DataTable_ExportDataToPDF;
          using (DataTable_ExportDataToPDF = new DataTable())
          {
            DataTable_ExportDataToPDF.Locale = CultureInfo.CurrentCulture;
            DataTable_ExportDataToPDF = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ExportDataToPDF).Copy();
            if (DataTable_ExportDataToPDF.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_ExportDataToPDF.Rows)
              {
                string Question = DataRow_Row["Question"].ToString();
                string Answer = DataRow_Row["Answer"].ToString();

                PdfPCell PdfPCell_Questions = new PdfPCell(new Phrase(Question, TableCellFont));
                PdfPCell PdfPCell_Answers = new PdfPCell(new Phrase(Answer, TableCellFont));

                PdfPTable_Survey.AddCell(PdfPCell_Questions);
                PdfPTable_Survey.AddCell(PdfPCell_Answers);
              }
            }
          }
        }

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=PostDischargeSurveyResults.pdf");

        Document Document_PXMPDCHResults;
        using (Document_PXMPDCHResults = new Document(PageSize.A4))
        {
          PdfWriter.GetInstance(Document_PXMPDCHResults, Response.OutputStream);
          Document_PXMPDCHResults.Open();
          Document_PXMPDCHResults.Add(Image_Logo);
          Document_PXMPDCHResults.Add(Paragraph_Space);
          Document_PXMPDCHResults.Add(Paragraph_Heading);
          Document_PXMPDCHResults.Add(Paragraph_Space);
          Document_PXMPDCHResults.Add(PdfPTable_Survey);
          //Document_PXMPDCHResults.Close();
        }

        Response.Write(Document_PXMPDCHResults);
        Response.Flush();
        Response.End();
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
      string SearchField4 = DropDownList_Type.SelectedValue;
      string SearchField5 = Server.HtmlEncode(TextBox_PatientVisitNumber.Text);
      string SearchField6 = Server.HtmlEncode(TextBox_Name.Text);

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
        SearchField4 = "s_CRM_TypeList=" + DropDownList_Type.SelectedValue.ToString() + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_CRM_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_CRM_Name=" + Server.HtmlEncode(TextBox_Name.Text.ToString()) + "&";
      }

      string FinalURL = "Form_CRM_IncompleteOther.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Incomplete Other", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Incomplete Other", "Form_CRM_IncompleteOther.aspx");
      Response.Redirect(FinalURL, false);
    }

    protected void DropDownList_Type_DataBound(object sender, EventArgs e)
    {
      System.Web.UI.WebControls.ListItem ListItem_TypeListItem4395 = DropDownList_Type.Items.FindByValue("4395");
      if (ListItem_TypeListItem4395 != null)
      {
        DropDownList_Type.Items.Remove(ListItem_TypeListItem4395);
      }
    }
    //---END--- --Search--//


    //--START-- --IncompleteOther--//
    protected void SqlDataSource_CRM_IncompleteOther_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_IncompleteOther.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_CRM_IncompleteOther.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_CRM_IncompleteOther.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_CRM_IncompleteOther.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_CRM_IncompleteOther_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_IncompleteOther.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_CRM_IncompleteOther.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_CRM_IncompleteOther.PageSize > 20 && GridView_CRM_IncompleteOther.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_CRM_IncompleteOther.PageSize > 50 && GridView_CRM_IncompleteOther.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_CRM_IncompleteOther_DataBound(object sender, EventArgs e)
    {
      DataRow[] SecurityAdmin = null;
      DataRow[] SecurityFormAdminUpdate = null;
      DataRow[] SecurityFacilityAdminHospitalManagerUpdate = null;
      DataRow[] SecurityFacilityAdminNSMUpdate = null;
      DataRow[] SecurityFacilityInvestigator = null;

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
            SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '146'");
            SecurityFacilityAdminHospitalManagerUpdate = DataTable_FormMode.Select("SecurityRole_Id = '150'");
            SecurityFacilityAdminNSMUpdate = DataTable_FormMode.Select("SecurityRole_Id = '148'");
            SecurityFacilityInvestigator = DataTable_FormMode.Select("SecurityRole_Id = '153'");
          }

          DataBound_BottomPagerRow(DataTable_FormMode);
        }
      }

      for (int i = 0; i < GridView_CRM_IncompleteOther.Rows.Count; i++)
      {
        HiddenField HiddenField_EditCRMId = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditCRMId");
        HiddenField HiddenField_EditCRMTypeList = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditCRMTypeList");

        Label Label_EditCommentTypeList = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentTypeList");
        DropDownList DropDownList_EditCommentTypeList = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentTypeList");
        Label Label_EditCommentTypeName = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentTypeName");

        DropDownList DropDownList_EditUnitId = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditUnitId");
        Label Label_EditUnitName = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditUnitName");

        Label Label_EditCommentCategoryList = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentCategoryList");
        DropDownList DropDownList_EditCommentCategoryList = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentCategoryList");
        Label Label_EditCommentCategoryName = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentCategoryName");

        Label Label_EditCommentAdditionalTypeList = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentAdditionalTypeList");
        DropDownList DropDownList_EditCommentAdditionalTypeList = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentAdditionalTypeList");
        Label Label_EditCommentAdditionalTypeName = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentAdditionalTypeName");

        Label Label_EditCommentAdditionalUnitId = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentAdditionalUnitId");
        DropDownList DropDownList_EditCommentAdditionalUnitId = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentAdditionalUnitId");
        Label Label_EditCommentAdditionalUnitName = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentAdditionalUnitName");

        Label Label_EditCommentAdditionalCategoryList = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentAdditionalCategoryList");
        DropDownList DropDownList_EditCommentAdditionalCategoryList = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentAdditionalCategoryList");
        Label Label_EditCommentAdditionalCategoryName = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCommentAdditionalCategoryName");

        CheckBox CheckBox_EditAcknowledge = (CheckBox)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("CheckBox_EditAcknowledge");
        Label Label_EditAcknowledge = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditAcknowledge");

        CheckBox CheckBox_EditCloseout = (CheckBox)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("CheckBox_EditCloseout");
        Label Label_EditCloseout = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditCloseout");

        HiddenField HiddenField_EditCRMUploadedFrom = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditCRMUploadedFrom");
        LinkButton LinkButton_EditCommentPXMPDCHResults = (LinkButton)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("LinkButton_EditCommentPXMPDCHResults");
        ScriptManager ScriptManager_Edit = ScriptManager.GetCurrent(Page);
        ScriptManager_Edit.RegisterPostBackControl(LinkButton_EditCommentPXMPDCHResults);

        string Security = "1";
        if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminHospitalManagerUpdate.Length > 0 || SecurityFacilityAdminNSMUpdate.Length > 0 || SecurityFacilityInvestigator.Length > 0))
        {
          Security = "0";

          DropDownList_EditUnitId.Visible = true;
          Label_EditUnitName.Visible = false;

          if (HiddenField_EditCRMTypeList.Value.ToString() == "4412")
          {
            Label_EditCommentTypeList.Visible = false;
            DropDownList_EditCommentTypeList.Visible = true;

            SqlDataSource SqlDataSource_CRM_EditCommentTypeList = (SqlDataSource)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("SqlDataSource_CRM_EditCommentTypeList");
            SqlDataSource_CRM_EditCommentTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
            SqlDataSource_CRM_EditCommentTypeList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_Type_List";
            SqlDataSource_CRM_EditCommentTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentTypeList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + HiddenField_EditCRMId.Value.ToString();
            SqlDataSource_CRM_EditCommentTypeList.DataBind();

            Label_EditCommentCategoryList.Visible = false;
            DropDownList_EditCommentCategoryList.Visible = true;

            SqlDataSource SqlDataSource_CRM_EditCommentCategoryList = (SqlDataSource)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("SqlDataSource_CRM_EditCommentCategoryList");
            SqlDataSource_CRM_EditCommentCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
            SqlDataSource_CRM_EditCommentCategoryList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_Category_List";
            SqlDataSource_CRM_EditCommentCategoryList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentCategoryList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + HiddenField_EditCRMId.Value.ToString();
            SqlDataSource_CRM_EditCommentCategoryList.DataBind();

            Label_EditCommentAdditionalTypeList.Visible = false;
            DropDownList_EditCommentAdditionalTypeList.Visible = true;

            SqlDataSource SqlDataSource_CRM_EditCommentAdditionalTypeList = (SqlDataSource)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("SqlDataSource_CRM_EditCommentAdditionalTypeList");
            SqlDataSource_CRM_EditCommentAdditionalTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
            SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_AdditionalType_List";
            SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + HiddenField_EditCRMId.Value.ToString();
            SqlDataSource_CRM_EditCommentAdditionalTypeList.DataBind();

            Label_EditCommentAdditionalUnitId.Visible = false;
            DropDownList_EditCommentAdditionalUnitId.Visible = true;
            Label_EditCommentAdditionalUnitName.Visible = false;

            Label_EditCommentAdditionalCategoryList.Visible = false;
            DropDownList_EditCommentAdditionalCategoryList.Visible = true;

            SqlDataSource SqlDataSource_CRM_EditCommentAdditionalCategoryList = (SqlDataSource)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("SqlDataSource_CRM_EditCommentAdditionalCategoryList");
            SqlDataSource_CRM_EditCommentAdditionalCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
            SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters["TableSELECT"].DefaultValue = "CRM_Comment_AdditionalCategory_List";
            SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters["TableFROM"].DefaultValue = "Form_CRM";
            SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters["TableWHERE"].DefaultValue = "CRM_Id = " + HiddenField_EditCRMId.Value.ToString();
            SqlDataSource_CRM_EditCommentAdditionalCategoryList.DataBind();

            if (HiddenField_EditCRMUploadedFrom.Value == "Post Discharge Survey")
            {
              LinkButton_EditCommentPXMPDCHResults.Visible = true;
            }
            else
            {
              LinkButton_EditCommentPXMPDCHResults.Visible = false;
            }
          }
          else
          {
            Label_EditCommentTypeList.Visible = true;
            DropDownList_EditCommentTypeList.Visible = false;

            Label_EditCommentCategoryList.Visible = true;
            DropDownList_EditCommentCategoryList.Visible = false;

            Label_EditCommentAdditionalTypeList.Visible = true;
            DropDownList_EditCommentAdditionalTypeList.Visible = false;

            Label_EditCommentAdditionalUnitId.Visible = true;
            DropDownList_EditCommentAdditionalUnitId.Visible = false;

            Label_EditCommentAdditionalCategoryList.Visible = true;
            DropDownList_EditCommentAdditionalCategoryList.Visible = false;

            LinkButton_EditCommentPXMPDCHResults.Visible = false;
          }
          Label_EditCommentTypeName.Visible = false;

          Label_EditCommentCategoryName.Visible = false;

          Label_EditCommentAdditionalTypeName.Visible = false;

          Label_EditCommentAdditionalCategoryName.Visible = false;

          CheckBox_EditAcknowledge.Visible = true;
          Label_EditAcknowledge.Visible = false;

          if (CheckBox_EditAcknowledge.Checked == false)
          {
            CheckBox_EditCloseout.Visible = false;
          }
          else
          {
            CheckBox_EditCloseout.Visible = true;
          }
          Label_EditCloseout.Visible = false;
        }

        if (Security == "1")
        {
          Security = "0";

          Label_EditCommentTypeList.Visible = false;
          DropDownList_EditCommentTypeList.Visible = false;
          Label_EditCommentTypeName.Visible = true;

          DropDownList_EditUnitId.Visible = false;
          Label_EditUnitName.Visible = true;

          Label_EditCommentCategoryList.Visible = false;
          DropDownList_EditCommentCategoryList.Visible = false;
          Label_EditCommentCategoryName.Visible = true;

          Label_EditCommentAdditionalTypeList.Visible = false;
          DropDownList_EditCommentAdditionalTypeList.Visible = false;
          Label_EditCommentAdditionalTypeName.Visible = true;

          Label_EditCommentAdditionalUnitId.Visible = false;
          DropDownList_EditCommentAdditionalUnitId.Visible = false;
          Label_EditCommentAdditionalUnitName.Visible = true;

          Label_EditCommentAdditionalCategoryList.Visible = false;
          DropDownList_EditCommentAdditionalCategoryList.Visible = false;
          Label_EditCommentAdditionalCategoryName.Visible = true;

          CheckBox_EditAcknowledge.Visible = false;
          Label_EditAcknowledge.Visible = true;

          CheckBox_EditCloseout.Visible = false;
          Label_EditCloseout.Visible = true;

          LinkButton_EditCommentPXMPDCHResults.Visible = false;
        }
      }
    }

    protected void DataBound_BottomPagerRow(DataTable dataTable_FormMode)
    {
      DataRow[] SecurityAdmin = null;
      DataRow[] SecurityFormAdminUpdate = null;
      DataRow[] SecurityFormAdminView = null;
      DataRow[] SecurityFacilityAdminHospitalManagerUpdate = null;
      DataRow[] SecurityFacilityAdminNSMUpdate = null;
      DataRow[] SecurityFacilityAdminView = null;
      DataRow[] SecurityFacilityInvestigator = null;
      DataRow[] SecurityFacilityApprover = null;
      DataRow[] SecurityFacilityCapturer = null;

      if (dataTable_FormMode != null)
      {
        if (dataTable_FormMode.Rows.Count > 0)
        {
          SecurityAdmin = dataTable_FormMode.Select("SecurityRole_Id = '1'");
          SecurityFormAdminUpdate = dataTable_FormMode.Select("SecurityRole_Id = '146'");
          SecurityFormAdminView = dataTable_FormMode.Select("SecurityRole_Id = '147'");
          SecurityFacilityAdminHospitalManagerUpdate = dataTable_FormMode.Select("SecurityRole_Id = '150'");
          SecurityFacilityAdminNSMUpdate = dataTable_FormMode.Select("SecurityRole_Id = '148'");
          SecurityFacilityAdminView = dataTable_FormMode.Select("SecurityRole_Id = '149'");
          SecurityFacilityInvestigator = dataTable_FormMode.Select("SecurityRole_Id = '153'");
          SecurityFacilityApprover = dataTable_FormMode.Select("SecurityRole_Id = '151'");
          SecurityFacilityCapturer = dataTable_FormMode.Select("SecurityRole_Id = '152'");
        }

        GridViewRow GridViewRow_List = GridView_CRM_IncompleteOther.BottomPagerRow;
        if (GridViewRow_List != null)
        {
          DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
          if (GridViewRow_List != null)
          {
            for (int i = 0; i < GridView_CRM_IncompleteOther.PageCount; i++)
            {
              int pageNumber = i + 1;
              System.Web.UI.WebControls.ListItem ListItem_Item = new System.Web.UI.WebControls.ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
              if (i == GridView_CRM_IncompleteOther.PageIndex)
              {
                ListItem_Item.Selected = true;
              }

              DropDownList_PageList.Items.Add(ListItem_Item);
            }
          }


          Button Button_AcknowledgeAll = (Button)GridViewRow_List.FindControl("Button_AcknowledgeAll");
          Button Button_AcknowledgeCloseoutAll = (Button)GridViewRow_List.FindControl("Button_AcknowledgeCloseoutAll");
          Button Button_Update = (Button)GridViewRow_List.FindControl("Button_Update");
          Button Button_Cancel = (Button)GridViewRow_List.FindControl("Button_Cancel");

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminHospitalManagerUpdate.Length > 0 || SecurityFacilityAdminNSMUpdate.Length > 0 || SecurityFacilityInvestigator.Length > 0))
          {
            Security = "0";

            Button_AcknowledgeAll.Visible = true;
            Button_AcknowledgeCloseoutAll.Visible = true;
            Button_Update.Visible = true;
            Button_Cancel.Visible = true;
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityApprover.Length > 0 || SecurityFacilityCapturer.Length > 0))
          {
            Security = "0";

            Button_AcknowledgeAll.Visible = false;
            Button_AcknowledgeCloseoutAll.Visible = false;
            Button_Update.Visible = false;
            Button_Cancel.Visible = false;
          }

          if (Security == "1")
          {
            Security = "0";

            Button_AcknowledgeAll.Visible = false;
            Button_AcknowledgeCloseoutAll.Visible = false;
            Button_Update.Visible = false;
            Button_Cancel.Visible = false;
          }
        }
      }
    }

    protected void GridView_CRM_IncompleteOther_RowCreated(object sender, GridViewRowEventArgs e)
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


    protected void DropDownList_EditCommentTypeList_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditCommentTypeList = (DropDownList)sender;
      GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)DropDownList_EditCommentTypeList.NamingContainer;
      HiddenField HiddenField_EditCRMId = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMId");
      HiddenField HiddenField_EditCRMTypeList = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMTypeList");
      Label Label_EditCommentTypeList = (Label)GridViewRow_CRM_IncompleteOther.FindControl("Label_EditCommentTypeList");

      if (HiddenField_EditCRMTypeList.Value.ToString() == "4412")
      {
        Label_EditCommentTypeList.Visible = false;
        DropDownList_EditCommentTypeList.Visible = true;

        SqlDataSource SqlDataSource_CRM_EditCommentTypeList = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentTypeList");
        SqlDataSource_CRM_EditCommentTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentTypeList.SelectCommand = "spAdministration_Execute_List";
        SqlDataSource_CRM_EditCommentTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "117");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "CRM_Comment_Type_List");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_CRM");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "CRM_Id = " + HiddenField_EditCRMId.Value.ToString() + "");
        SqlDataSource_CRM_EditCommentTypeList.DataBind();
      }
      else
      {
        Label_EditCommentTypeList.Visible = true;
        DropDownList_EditCommentTypeList.Visible = false;

        SqlDataSource SqlDataSource_CRM_EditCommentTypeList = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentTypeList");
        SqlDataSource_CRM_EditCommentTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentTypeList.SelectCommand = "spAdministration_Execute_List";
        SqlDataSource_CRM_EditCommentTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "117");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
        SqlDataSource_CRM_EditCommentTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
      }
    }

    protected void DropDownList_EditUnitId_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditUnitId = (DropDownList)sender;
      GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)DropDownList_EditUnitId.NamingContainer;
      HiddenField HiddenField_EditCRMId = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMId");
      HiddenField HiddenField_EditFacilityId = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditFacilityId");

      SqlDataSource SqlDataSource_CRM_EditUnitId = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditUnitId");
      SqlDataSource_CRM_EditUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CRM_EditUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_CRM_EditUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_CRM_EditUnitId.SelectParameters.Clear();
      SqlDataSource_CRM_EditUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_CRM_EditUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
      SqlDataSource_CRM_EditUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, HiddenField_EditFacilityId.Value.ToString());
      SqlDataSource_CRM_EditUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "CASE WHEN CRM_Type_List = '4406' THEN CRM_Compliment_Unit_Id WHEN CRM_Type_List = '4412' THEN CRM_Comment_Unit_Id WHEN CRM_Type_List = '4413' THEN CRM_Query_Unit_Id WHEN CRM_Type_List = '4414' THEN CRM_Suggestion_Unit_Id ELSE 0 END	AS CRM_Unit_Id");
      SqlDataSource_CRM_EditUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "vForm_CRM");
      SqlDataSource_CRM_EditUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "CRM_Id = " + HiddenField_EditCRMId.Value + " ");
      SqlDataSource_CRM_EditUnitId.DataBind();
    }

    protected void DropDownList_EditCommentCategoryList_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditCommentCategoryList = (DropDownList)sender;
      GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)DropDownList_EditCommentCategoryList.NamingContainer;
      HiddenField HiddenField_EditCRMId = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMId");
      HiddenField HiddenField_EditCRMTypeList = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMTypeList");
      Label Label_EditCommentCategoryList = (Label)GridViewRow_CRM_IncompleteOther.FindControl("Label_EditCommentCategoryList");

      if (HiddenField_EditCRMTypeList.Value.ToString() == "4412")
      {
        Label_EditCommentCategoryList.Visible = false;
        DropDownList_EditCommentCategoryList.Visible = true;

        SqlDataSource SqlDataSource_CRM_EditCommentCategoryList = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentCategoryList");
        SqlDataSource_CRM_EditCommentCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentCategoryList.SelectCommand = "spAdministration_Execute_List";
        SqlDataSource_CRM_EditCommentCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "124");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "CRM_Comment_Category_List");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_CRM");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "CRM_Id = " + HiddenField_EditCRMId.Value.ToString() + "");
        SqlDataSource_CRM_EditCommentCategoryList.DataBind();
      }
      else
      {
        Label_EditCommentCategoryList.Visible = true;
        DropDownList_EditCommentCategoryList.Visible = false;

        SqlDataSource SqlDataSource_CRM_EditCommentCategoryList = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentCategoryList");
        SqlDataSource_CRM_EditCommentCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentCategoryList.SelectCommand = "spAdministration_Execute_List";
        SqlDataSource_CRM_EditCommentCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "124");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
        SqlDataSource_CRM_EditCommentCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
      }
    }

    protected void DropDownList_EditCommentAdditionalTypeList_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditCommentAdditionalTypeList = (DropDownList)sender;
      GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)DropDownList_EditCommentAdditionalTypeList.NamingContainer;
      HiddenField HiddenField_EditCRMId = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMId");
      HiddenField HiddenField_EditCRMTypeList = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMTypeList");
      Label Label_EditCommentAdditionalTypeList = (Label)GridViewRow_CRM_IncompleteOther.FindControl("Label_EditCommentAdditionalTypeList");

      if (HiddenField_EditCRMTypeList.Value.ToString() == "4412")
      {
        Label_EditCommentAdditionalTypeList.Visible = false;
        DropDownList_EditCommentAdditionalTypeList.Visible = true;

        SqlDataSource SqlDataSource_CRM_EditCommentAdditionalTypeList = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentAdditionalTypeList");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectCommand = "spAdministration_Execute_List";
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "117");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "CRM_Comment_AdditionalType_List");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_CRM");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "CRM_Id = " + HiddenField_EditCRMId.Value.ToString() + "");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.DataBind();
      }
      else
      {
        Label_EditCommentAdditionalTypeList.Visible = true;
        DropDownList_EditCommentAdditionalTypeList.Visible = false;

        SqlDataSource SqlDataSource_CRM_EditCommentAdditionalTypeList = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentAdditionalTypeList");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectCommand = "spAdministration_Execute_List";
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "117");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
        SqlDataSource_CRM_EditCommentAdditionalTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
      }
    }

    protected void DropDownList_EditCommentAdditionalUnitId_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditCommentAdditionalUnitId = (DropDownList)sender;
      GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)DropDownList_EditCommentAdditionalUnitId.NamingContainer;
      HiddenField HiddenField_EditCRMId = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMId");
      HiddenField HiddenField_EditCRMTypeList = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMTypeList");
      HiddenField HiddenField_EditFacilityId = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditFacilityId");
      Label Label_EditCommentAdditionalUnitId = (Label)GridViewRow_CRM_IncompleteOther.FindControl("Label_EditCommentAdditionalUnitId");

      if (HiddenField_EditCRMTypeList.Value.ToString() == "4412")
      {
        Label_EditCommentAdditionalUnitId.Visible = false;
        DropDownList_EditCommentAdditionalUnitId.Visible = true;

        SqlDataSource SqlDataSource_CRM_EditCommentAdditionalUnitId = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentAdditionalUnitId");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, HiddenField_EditFacilityId.Value.ToString());
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "CASE WHEN CRM_Type_List = '4406' THEN 0 WHEN CRM_Type_List = '4412' THEN CAST(CRM_Comment_AdditionalUnit_Id AS NVARCHAR(MAX)) WHEN CRM_Type_List = '4413' THEN 0 WHEN CRM_Type_List = '4414' THEN 0 ELSE 0 END	AS CRM_Comment_AdditionalUnit_Id");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "vForm_CRM");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "CRM_Id = " + HiddenField_EditCRMId.Value + " ");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.DataBind();
      }
      else
      {
        Label_EditCommentAdditionalUnitId.Visible = true;
        DropDownList_EditCommentAdditionalUnitId.Visible = false;

        SqlDataSource SqlDataSource_CRM_EditCommentAdditionalUnitId = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentAdditionalUnitId");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, HiddenField_EditFacilityId.Value.ToString());
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "CASE WHEN CRM_Type_List = '4406' THEN 0 WHEN CRM_Type_List = '4412' THEN CAST(CRM_Comment_AdditionalUnit_Id AS NVARCHAR(MAX)) WHEN CRM_Type_List = '4413' THEN 0 WHEN CRM_Type_List = '4414' THEN 0 ELSE 0 END	AS CRM_Comment_AdditionalUnit_Id");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "vForm_CRM");
        SqlDataSource_CRM_EditCommentAdditionalUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "CRM_Id = " + HiddenField_EditCRMId.Value + " ");
      }
    }

    protected void DropDownList_EditCommentAdditionalCategoryList_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditCommentAdditionalCategoryList = (DropDownList)sender;
      GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)DropDownList_EditCommentAdditionalCategoryList.NamingContainer;
      HiddenField HiddenField_EditCRMId = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMId");
      HiddenField HiddenField_EditCRMTypeList = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMTypeList");
      Label Label_EditCommentAdditionalCategoryList = (Label)GridViewRow_CRM_IncompleteOther.FindControl("Label_EditCommentAdditionalCategoryList");

      if (HiddenField_EditCRMTypeList.Value.ToString() == "4412")
      {
        Label_EditCommentAdditionalCategoryList.Visible = false;
        DropDownList_EditCommentAdditionalCategoryList.Visible = true;

        SqlDataSource SqlDataSource_CRM_EditCommentAdditionalCategoryList = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentAdditionalCategoryList");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectCommand = "spAdministration_Execute_List";
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "124");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "CRM_Comment_AdditionalCategory_List");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "Form_CRM");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "CRM_Id = " + HiddenField_EditCRMId.Value.ToString() + "");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.DataBind();
      }
      else
      {
        Label_EditCommentAdditionalCategoryList.Visible = true;
        DropDownList_EditCommentAdditionalCategoryList.Visible = false;

        SqlDataSource SqlDataSource_CRM_EditCommentAdditionalCategoryList = (SqlDataSource)GridViewRow_CRM_IncompleteOther.FindControl("SqlDataSource_CRM_EditCommentAdditionalCategoryList");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectCommand = "spAdministration_Execute_List";
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Clear();
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "36");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "124");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
        SqlDataSource_CRM_EditCommentAdditionalCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
      }
    }

    protected void CheckBox_EditCloseout_DataBinding(object sender, EventArgs e)
    {
      CheckBox CheckBox_EditCloseout = (CheckBox)sender;
      GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)CheckBox_EditCloseout.NamingContainer;
      CheckBox CheckBox_EditAcknowledge = (CheckBox)GridViewRow_CRM_IncompleteOther.FindControl("CheckBox_EditAcknowledge");

      if (CheckBox_EditAcknowledge.Checked == true)
      {
        CheckBox_EditCloseout.Checked = false;
        CheckBox_EditCloseout.Visible = true;
      }
      else
      {
        CheckBox_EditCloseout.Checked = false;
        CheckBox_EditCloseout.Visible = false;
      }
    }


    protected void CheckBox_EditAcknowledge_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox CheckBox_EditAcknowledge = (CheckBox)sender;
      GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)CheckBox_EditAcknowledge.NamingContainer;
      CheckBox CheckBox_EditCloseout = (CheckBox)GridViewRow_CRM_IncompleteOther.FindControl("CheckBox_EditCloseout");

      if (CheckBox_EditAcknowledge.Checked == true)
      {
        CheckBox_EditCloseout.Checked = false;
        CheckBox_EditCloseout.Visible = true;
      }
      else
      {
        CheckBox_EditCloseout.Checked = false;
        CheckBox_EditCloseout.Visible = false;
      }
    }

    protected void CheckBox_EditCloseout_CheckedChanged(object sender, EventArgs e)
    {
      //CheckBox CheckBox_EditCloseout = (CheckBox)sender;
      //GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)CheckBox_EditCloseout.NamingContainer;
      //DropDownList DropDownList_EditUnitId = (DropDownList)GridViewRow_CRM_IncompleteOther.FindControl("DropDownList_EditUnitId");
      //HiddenField HiddenField_EditCRMTypeList = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMTypeList");
      //DropDownList DropDownList_EditCommentTypeList = (DropDownList)GridViewRow_CRM_IncompleteOther.FindControl("DropDownList_EditCommentTypeList");
      //DropDownList DropDownList_EditCommentCategoryList = (DropDownList)GridViewRow_CRM_IncompleteOther.FindControl("DropDownList_EditCommentCategoryList");
      //DropDownList DropDownList_EditCommentAdditionalTypeList = (DropDownList)GridViewRow_CRM_IncompleteOther.FindControl("DropDownList_EditCommentAdditionalTypeList");
      //DropDownList DropDownList_EditCommentAdditionalUnitId = (DropDownList)GridViewRow_CRM_IncompleteOther.FindControl("DropDownList_EditCommentAdditionalUnitId");
      //DropDownList DropDownList_EditCommentAdditionalCategoryList = (DropDownList)GridViewRow_CRM_IncompleteOther.FindControl("DropDownList_EditCommentAdditionalCategoryList");      
    }


    protected void Button_AcknowledgeAll_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_CRM_IncompleteOther.Rows.Count; i++)
      {
        CheckBox CheckBox_EditAcknowledge = (CheckBox)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("CheckBox_EditAcknowledge");

        CheckBox_EditAcknowledge.Checked = true;
        CheckBox_EditAcknowledge_CheckedChanged(CheckBox_EditAcknowledge, e);
      }

      Button_Update_Click(sender, e);
    }

    protected void Button_AcknowledgeCloseoutAll_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_CRM_IncompleteOther.Rows.Count; i++)
      {
        CheckBox CheckBox_EditAcknowledge = (CheckBox)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("CheckBox_EditAcknowledge");
        CheckBox CheckBox_EditCloseout = (CheckBox)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("CheckBox_EditCloseout");

        CheckBox_EditAcknowledge.Checked = true;
        CheckBox_EditAcknowledge_CheckedChanged(CheckBox_EditAcknowledge, e);
        CheckBox_EditCloseout.Checked = true;
        CheckBox_EditCloseout_CheckedChanged(CheckBox_EditCloseout, e);
      }

      Button_Update_Click(sender, e);
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_CRM_IncompleteOther.SetFocus(ImageButton_IncompleteOther);

      string Proceed = "Yes";

      for (int i = 0; i < GridView_CRM_IncompleteOther.Rows.Count; i++)
      {
        if (Proceed == "Yes")
        {
          HiddenField HiddenField_EditCRMId = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditCRMId");
          HiddenField HiddenField_EditCRMTypeList = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditCRMTypeList");
          DropDownList DropDownList_EditCommentTypeList = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentTypeList");
          DropDownList DropDownList_EditUnitId = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditUnitId");
          DropDownList DropDownList_EditCommentCategoryList = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentCategoryList");
          DropDownList DropDownList_EditCommentAdditionalTypeList = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentAdditionalTypeList");
          DropDownList DropDownList_EditCommentAdditionalUnitId = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentAdditionalUnitId");
          DropDownList DropDownList_EditCommentAdditionalCategoryList = (DropDownList)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("DropDownList_EditCommentAdditionalCategoryList");
          CheckBox CheckBox_EditAcknowledge = (CheckBox)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("CheckBox_EditAcknowledge");
          HiddenField HiddenField_EditAcknowledge = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditAcknowledge");
          HiddenField HiddenField_EditAcknowledgeDate = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditAcknowledgeDate");
          HiddenField HiddenField_EditAcknowledgeBy = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditAcknowledgeBy");
          CheckBox CheckBox_EditCloseout = (CheckBox)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("CheckBox_EditCloseout");
          HiddenField HiddenField_EditCloseout = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditCloseout");
          HiddenField HiddenField_EditCloseoutDate = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditCloseoutDate");
          HiddenField HiddenField_EditCloseoutBy = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditCloseoutBy");

          HiddenField HiddenField_EditModifiedDate = (HiddenField)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("HiddenField_EditModifiedDate");
          Label Label_EditInvalidFormMessage = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditInvalidFormMessage");
          Label Label_EditConcurrencyUpdateMessage = (Label)GridView_CRM_IncompleteOther.Rows[i].Cells[0].FindControl("Label_EditConcurrencyUpdateMessage");

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
            string Label_EditInvalidFormMessageText = EditValidation(CheckBox_EditCloseout, DropDownList_EditUnitId, HiddenField_EditCRMTypeList, DropDownList_EditCommentTypeList);

            if (!string.IsNullOrEmpty(Label_EditInvalidFormMessageText))
            {
              Proceed = "No";

              Label_EditInvalidFormMessage.Text = Label_EditInvalidFormMessageText;
              Label_EditConcurrencyUpdateMessage.Text = "";
            }
            else
            {
              string SQLStringEdit = "UPDATE Form_CRM SET CRM_Compliment_Unit_Id = @CRM_Compliment_Unit_Id ,CRM_Compliment_Acknowledge = @CRM_Compliment_Acknowledge ,CRM_Compliment_AcknowledgeDate = @CRM_Compliment_AcknowledgeDate ,CRM_Compliment_AcknowledgeBy = @CRM_Compliment_AcknowledgeBy ,CRM_Compliment_CloseOut = @CRM_Compliment_CloseOut ,CRM_Compliment_CloseOutDate = @CRM_Compliment_CloseOutDate ,CRM_Compliment_CloseOutBy = @CRM_Compliment_CloseOutBy ,CRM_Comment_Type_List = @CRM_Comment_Type_List ,CRM_Comment_Unit_Id = @CRM_Comment_Unit_Id ,CRM_Comment_Category_List = @CRM_Comment_Category_List, CRM_Comment_AdditionalType_List = @CRM_Comment_AdditionalType_List , CRM_Comment_AdditionalUnit_Id = @CRM_Comment_AdditionalUnit_Id ,CRM_Comment_AdditionalCategory_List = @CRM_Comment_AdditionalCategory_List ,CRM_Comment_Acknowledge = @CRM_Comment_Acknowledge ,CRM_Comment_AcknowledgeDate = @CRM_Comment_AcknowledgeDate ,CRM_Comment_AcknowledgeBy = @CRM_Comment_AcknowledgeBy ,CRM_Comment_CloseOut = @CRM_Comment_CloseOut ,CRM_Comment_CloseOutDate = @CRM_Comment_CloseOutDate ,CRM_Comment_CloseOutBy = @CRM_Comment_CloseOutBy ,CRM_Query_Unit_Id = @CRM_Query_Unit_Id ,CRM_Query_Acknowledge = @CRM_Query_Acknowledge ,CRM_Query_AcknowledgeDate = @CRM_Query_AcknowledgeDate ,CRM_Query_AcknowledgeBy = @CRM_Query_AcknowledgeBy ,CRM_Query_CloseOut = @CRM_Query_CloseOut ,CRM_Query_CloseOutDate = @CRM_Query_CloseOutDate ,CRM_Query_CloseOutBy = @CRM_Query_CloseOutBy ,CRM_Suggestion_Unit_Id = @CRM_Suggestion_Unit_Id ,CRM_Suggestion_Acknowledge = @CRM_Suggestion_Acknowledge ,CRM_Suggestion_AcknowledgeDate = @CRM_Suggestion_AcknowledgeDate ,CRM_Suggestion_AcknowledgeBy = @CRM_Suggestion_AcknowledgeBy ,CRM_Suggestion_CloseOut = @CRM_Suggestion_CloseOut ,CRM_Suggestion_CloseOutDate = @CRM_Suggestion_CloseOutDate ,CRM_Suggestion_CloseOutBy = @CRM_Suggestion_CloseOutBy ,CRM_ModifiedDate = @CRM_ModifiedDate ,CRM_ModifiedBy = @CRM_ModifiedBy ,CRM_History = @CRM_History WHERE CRM_Id = @CRM_Id";
              using (SqlCommand SqlCommand_Edit = new SqlCommand(SQLStringEdit))
              {
                Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_CRM", "CRM_Id = " + HiddenField_EditCRMId.Value.ToString());

                DataView DataView_CRM = (DataView)SqlDataSource_CRM_IncompleteOther.Select(DataSourceSelectArguments.Empty);
                if (DataView_CRM.Table.Rows.Count != 0)
                {
                  DataRowView DataRowView_CRM = DataView_CRM[0];
                  Session["CRMHistory"] = Convert.ToString(DataRowView_CRM["CRM_History"], CultureInfo.CurrentCulture);
                  Session["CRMHistory"] = Session["History"].ToString() + Session["CRMHistory"].ToString();

                  if (HiddenField_EditCRMTypeList.Value.ToString() == "4406")
                  {
                    EditSqlCommand_Compliment(SqlCommand_Edit, DropDownList_EditUnitId, HiddenField_EditAcknowledge, HiddenField_EditAcknowledgeDate, HiddenField_EditAcknowledgeBy, CheckBox_EditAcknowledge, HiddenField_EditCloseout, HiddenField_EditCloseoutDate, HiddenField_EditCloseoutBy, CheckBox_EditCloseout);
                  }
                  else if (HiddenField_EditCRMTypeList.Value.ToString() == "4412")
                  {
                    EditSqlCommand_Comment(SqlCommand_Edit, DropDownList_EditCommentTypeList, DropDownList_EditUnitId, DropDownList_EditCommentCategoryList, DropDownList_EditCommentAdditionalTypeList, DropDownList_EditCommentAdditionalUnitId, DropDownList_EditCommentAdditionalCategoryList, HiddenField_EditAcknowledge, HiddenField_EditAcknowledgeDate, HiddenField_EditAcknowledgeBy, CheckBox_EditAcknowledge, HiddenField_EditCloseout, HiddenField_EditCloseoutDate, HiddenField_EditCloseoutBy, CheckBox_EditCloseout);
                  }
                  else if (HiddenField_EditCRMTypeList.Value.ToString() == "4413")
                  {
                    EditSqlCommand_Query(SqlCommand_Edit, DropDownList_EditUnitId, HiddenField_EditAcknowledge, HiddenField_EditAcknowledgeDate, HiddenField_EditAcknowledgeBy, CheckBox_EditAcknowledge, HiddenField_EditCloseout, HiddenField_EditCloseoutDate, HiddenField_EditCloseoutBy, CheckBox_EditCloseout);
                  }
                  else if (HiddenField_EditCRMTypeList.Value.ToString() == "4414")
                  {
                    EditSqlCommand_Suggestions(SqlCommand_Edit, DropDownList_EditUnitId, HiddenField_EditAcknowledge, HiddenField_EditAcknowledgeDate, HiddenField_EditAcknowledgeBy, CheckBox_EditAcknowledge, HiddenField_EditCloseout, HiddenField_EditCloseoutDate, HiddenField_EditCloseoutBy, CheckBox_EditCloseout);
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

              GridView_CRM_IncompleteOther.Rows[i].Visible = false;
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
        GridView_CRM_IncompleteOther.DataBind();
        GridView_CRM_IncompleteOtherBulkApproval.DataBind();
      }
    }

    protected static string EditValidation(CheckBox checkBox_EditCloseout, ListControl dropDownList_EditUnitId, HiddenField hiddenField_EditTypeList, ListControl dropDownList_EditCommentTypeList)
    {
      string InvalidFormMessage = "";

      if (checkBox_EditCloseout != null && dropDownList_EditUnitId != null && hiddenField_EditTypeList != null && dropDownList_EditCommentTypeList != null)
      {
        if (checkBox_EditCloseout.Checked == true)
        {
          if (string.IsNullOrEmpty(dropDownList_EditUnitId.SelectedValue.ToString()))
          {
            InvalidFormMessage = InvalidFormMessage + "Unit is Required<br/>";
          }

          if (hiddenField_EditTypeList.Value == "4412")
          {
            if (string.IsNullOrEmpty(dropDownList_EditCommentTypeList.SelectedValue.ToString()))
            {
              InvalidFormMessage = InvalidFormMessage + "Comment Type is Required<br/>";
            }
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void EditSqlCommand_Compliment(SqlCommand sqlCommand_Edit, ListControl dropDownList_EditUnitId, HiddenField hiddenField_EditAcknowledge, HiddenField hiddenField_EditAcknowledgeDate, HiddenField hiddenField_EditAcknowledgeBy, CheckBox checkBox_EditAcknowledge, HiddenField hiddenField_EditCloseout, HiddenField hiddenField_EditCloseoutDate, HiddenField hiddenField_EditCloseoutBy, CheckBox checkBox_EditCloseout)
    {
      SqlCommand_IsNullOrEmpty(sqlCommand_Edit, "sqlCommand_Edit");
      Control_IsNullOrEmpty(dropDownList_EditUnitId, "dropDownList_EditUnitId");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledge, "hiddenField_EditAcknowledge");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledgeDate, "hiddenField_EditAcknowledgeDate");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledgeBy, "hiddenField_EditAcknowledgeBy");
      Control_IsNullOrEmpty(checkBox_EditAcknowledge, "checkBox_EditAcknowledge");
      Control_IsNullOrEmpty(hiddenField_EditCloseout, "hiddenField_EditCloseout");
      Control_IsNullOrEmpty(hiddenField_EditCloseoutDate, "hiddenField_EditCloseoutDate");
      Control_IsNullOrEmpty(hiddenField_EditCloseoutBy, "hiddenField_EditCloseoutBy");
      Control_IsNullOrEmpty(checkBox_EditCloseout, "checkBox_EditCloseout");

      if (sqlCommand_Edit != null && dropDownList_EditUnitId != null && hiddenField_EditAcknowledge != null && hiddenField_EditAcknowledgeDate != null && hiddenField_EditAcknowledgeBy != null && checkBox_EditAcknowledge != null && hiddenField_EditCloseout != null && hiddenField_EditCloseoutDate != null && hiddenField_EditCloseoutBy != null && checkBox_EditCloseout != null)
      {
        if (!string.IsNullOrEmpty(dropDownList_EditUnitId.SelectedValue.ToString()))
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", dropDownList_EditUnitId.SelectedValue);
        }
        else
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
        }

        if (hiddenField_EditAcknowledge.Value.ToString() == "False")
        {
          if (checkBox_EditAcknowledge.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", true);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeDate", DateTime.Now.ToString());
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeBy", Request.ServerVariables["LOGON_USER"]);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeBy", DBNull.Value);
          }
        }
        else
        {
          if (checkBox_EditAcknowledge.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", hiddenField_EditAcknowledge.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeDate", hiddenField_EditAcknowledgeDate.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeBy", hiddenField_EditAcknowledgeBy.Value);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeBy", DBNull.Value);
          }
        }

        if (hiddenField_EditCloseout.Value.ToString() == "False")
        {
          if (checkBox_EditAcknowledge.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOut", true);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutDate", DateTime.Now.ToString());
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutBy", Request.ServerVariables["LOGON_USER"]);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOut", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutBy", DBNull.Value);
          }
        }
        else
        {
          if (checkBox_EditCloseout.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOut", hiddenField_EditCloseout.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutDate", hiddenField_EditCloseoutDate.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutBy", hiddenField_EditCloseoutBy.Value);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOut", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutBy", DBNull.Value);
          }
        }

        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Category_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalType_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalUnit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalCategory_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);

        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutBy", DBNull.Value);

        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutBy", DBNull.Value);
      }
    }

    protected void EditSqlCommand_Comment(SqlCommand sqlCommand_Edit, ListControl dropDownList_EditCommentTypeList, ListControl dropDownList_EditUnitId, ListControl dropDownList_EditCommentCategoryList, ListControl dropDownList_EditCommentAdditionalTypeList, ListControl dropDownList_EditCommentAdditionalUnitId, ListControl dropDownList_EditCommentAdditionalCategoryList, HiddenField hiddenField_EditAcknowledge, HiddenField hiddenField_EditAcknowledgeDate, HiddenField hiddenField_EditAcknowledgeBy, CheckBox checkBox_EditAcknowledge, HiddenField hiddenField_EditCloseout, HiddenField hiddenField_EditCloseoutDate, HiddenField hiddenField_EditCloseoutBy, CheckBox checkBox_EditCloseout)
    {
      SqlCommand_IsNullOrEmpty(sqlCommand_Edit, "sqlCommand_Edit");
      Control_IsNullOrEmpty(dropDownList_EditCommentTypeList, "dropDownList_EditCommentTypeList");
      Control_IsNullOrEmpty(dropDownList_EditUnitId, "dropDownList_EditUnitId");
      Control_IsNullOrEmpty(dropDownList_EditCommentCategoryList, "dropDownList_EditCommentCategoryList");
      Control_IsNullOrEmpty(dropDownList_EditCommentAdditionalTypeList, "dropDownList_EditCommentAdditionalTypeList");
      Control_IsNullOrEmpty(dropDownList_EditCommentAdditionalUnitId, "dropDownList_EditCommentAdditionalUnitId");
      Control_IsNullOrEmpty(dropDownList_EditCommentAdditionalCategoryList, "dropDownList_EditCommentAdditionalCategoryList");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledge, "hiddenField_EditAcknowledge");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledgeDate, "hiddenField_EditAcknowledgeDate");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledgeBy, "hiddenField_EditAcknowledgeBy");
      Control_IsNullOrEmpty(checkBox_EditAcknowledge, "checkBox_EditAcknowledge");
      Control_IsNullOrEmpty(hiddenField_EditCloseout, "hiddenField_EditCloseout");
      Control_IsNullOrEmpty(hiddenField_EditCloseoutDate, "hiddenField_EditCloseoutDate");
      Control_IsNullOrEmpty(hiddenField_EditCloseoutBy, "hiddenField_EditCloseoutBy");
      Control_IsNullOrEmpty(checkBox_EditCloseout, "checkBox_EditCloseout");

      if (sqlCommand_Edit != null && dropDownList_EditCommentTypeList != null && dropDownList_EditUnitId != null && dropDownList_EditCommentCategoryList != null && dropDownList_EditCommentAdditionalTypeList != null && dropDownList_EditCommentAdditionalUnitId != null && dropDownList_EditCommentAdditionalCategoryList != null && hiddenField_EditAcknowledge != null && hiddenField_EditAcknowledgeDate != null && hiddenField_EditAcknowledgeBy != null && checkBox_EditAcknowledge != null && hiddenField_EditCloseout != null && hiddenField_EditCloseoutDate != null && hiddenField_EditCloseoutBy != null && checkBox_EditCloseout != null)
      {
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutBy", DBNull.Value);

        if (!string.IsNullOrEmpty(dropDownList_EditCommentTypeList.SelectedValue.ToString()))
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Type_List", dropDownList_EditCommentTypeList.SelectedValue);
        }
        else
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(dropDownList_EditUnitId.SelectedValue.ToString()))
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Unit_Id", dropDownList_EditUnitId.SelectedValue);
        }
        else
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(dropDownList_EditCommentCategoryList.SelectedValue.ToString()))
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Category_List", dropDownList_EditCommentCategoryList.SelectedValue);
        }
        else
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Category_List", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(dropDownList_EditCommentAdditionalTypeList.SelectedValue.ToString()))
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalType_List", dropDownList_EditCommentAdditionalTypeList.SelectedValue);
        }
        else
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalType_List", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(dropDownList_EditCommentAdditionalUnitId.SelectedValue.ToString()))
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalUnit_Id", dropDownList_EditCommentAdditionalUnitId.SelectedValue);
        }
        else
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalUnit_Id", DBNull.Value);
        }

        if (!string.IsNullOrEmpty(dropDownList_EditCommentAdditionalCategoryList.SelectedValue.ToString()))
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalCategory_List", dropDownList_EditCommentAdditionalCategoryList.SelectedValue);
        }
        else
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalCategory_List", DBNull.Value);
        }

        if (hiddenField_EditAcknowledge.Value.ToString() == "False")
        {
          if (checkBox_EditAcknowledge.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Acknowledge", true);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DateTime.Now.ToString());
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", Request.ServerVariables["LOGON_USER"]);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Acknowledge", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
          }
        }
        else
        {
          if (checkBox_EditAcknowledge.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Acknowledge", hiddenField_EditAcknowledge.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", hiddenField_EditAcknowledgeDate.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", hiddenField_EditAcknowledgeBy.Value);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Acknowledge", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
          }
        }

        if (hiddenField_EditCloseout.Value.ToString() == "False")
        {
          if (checkBox_EditCloseout.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOut", true);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DateTime.Now.ToString());
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", Request.ServerVariables["LOGON_USER"]);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOut", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
          }
        }
        else
        {
          if (checkBox_EditCloseout.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOut", hiddenField_EditCloseout.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", hiddenField_EditCloseoutDate.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", hiddenField_EditCloseoutBy.Value);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOut", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);
          }
        }

        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutBy", DBNull.Value);

        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutBy", DBNull.Value);
      }
    }

    protected void EditSqlCommand_Query(SqlCommand sqlCommand_Edit, ListControl dropDownList_EditUnitId, HiddenField hiddenField_EditAcknowledge, HiddenField hiddenField_EditAcknowledgeDate, HiddenField hiddenField_EditAcknowledgeBy, CheckBox checkBox_EditAcknowledge, HiddenField hiddenField_EditCloseout, HiddenField hiddenField_EditCloseoutDate, HiddenField hiddenField_EditCloseoutBy, CheckBox checkBox_EditCloseout)
    {
      SqlCommand_IsNullOrEmpty(sqlCommand_Edit, "sqlCommand_Edit");
      Control_IsNullOrEmpty(dropDownList_EditUnitId, "dropDownList_EditUnitId");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledge, "hiddenField_EditAcknowledge");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledgeDate, "hiddenField_EditAcknowledgeDate");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledgeBy, "hiddenField_EditAcknowledgeBy");
      Control_IsNullOrEmpty(checkBox_EditAcknowledge, "checkBox_EditAcknowledge");
      Control_IsNullOrEmpty(hiddenField_EditCloseout, "hiddenField_EditCloseout");
      Control_IsNullOrEmpty(hiddenField_EditCloseoutDate, "hiddenField_EditCloseoutDate");
      Control_IsNullOrEmpty(hiddenField_EditCloseoutBy, "hiddenField_EditCloseoutBy");
      Control_IsNullOrEmpty(checkBox_EditCloseout, "checkBox_EditCloseout");

      if (sqlCommand_Edit != null && dropDownList_EditUnitId != null && hiddenField_EditAcknowledge != null && hiddenField_EditAcknowledgeDate != null && hiddenField_EditAcknowledgeBy != null && checkBox_EditAcknowledge != null && hiddenField_EditCloseout != null && hiddenField_EditCloseoutDate != null && hiddenField_EditCloseoutBy != null && checkBox_EditCloseout != null)
      {
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutBy", DBNull.Value);

        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Category_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalType_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalUnit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalCategory_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);

        if (!string.IsNullOrEmpty(dropDownList_EditUnitId.SelectedValue.ToString()))
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Unit_Id", dropDownList_EditUnitId.SelectedValue);
        }
        else
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Unit_Id", DBNull.Value);
        }

        if (hiddenField_EditAcknowledge.Value.ToString() == "False")
        {
          if (checkBox_EditAcknowledge.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Acknowledge", true);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeDate", DateTime.Now.ToString());
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeBy", Request.ServerVariables["LOGON_USER"]);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Acknowledge", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeBy", DBNull.Value);
          }
        }
        else
        {
          if (checkBox_EditAcknowledge.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Acknowledge", hiddenField_EditAcknowledge.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeDate", hiddenField_EditAcknowledgeDate.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeBy", hiddenField_EditAcknowledgeBy.Value);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Acknowledge", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeBy", DBNull.Value);
          }
        }

        if (hiddenField_EditCloseout.Value.ToString() == "False")
        {
          if (checkBox_EditCloseout.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOut", true);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutDate", DateTime.Now.ToString());
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutBy", Request.ServerVariables["LOGON_USER"]);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOut", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutBy", DBNull.Value);
          }
        }
        else
        {
          if (checkBox_EditCloseout.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOut", hiddenField_EditCloseout.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutDate", hiddenField_EditCloseoutDate.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutBy", hiddenField_EditCloseoutBy.Value);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOut", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutBy", DBNull.Value);
          }
        }

        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutBy", DBNull.Value);
      }
    }

    protected void EditSqlCommand_Suggestions(SqlCommand sqlCommand_Edit, ListControl dropDownList_EditUnitId, HiddenField hiddenField_EditAcknowledge, HiddenField hiddenField_EditAcknowledgeDate, HiddenField hiddenField_EditAcknowledgeBy, CheckBox checkBox_EditAcknowledge, HiddenField hiddenField_EditCloseout, HiddenField hiddenField_EditCloseoutDate, HiddenField hiddenField_EditCloseoutBy, CheckBox checkBox_EditCloseout)
    {
      SqlCommand_IsNullOrEmpty(sqlCommand_Edit, "sqlCommand_Edit");
      Control_IsNullOrEmpty(dropDownList_EditUnitId, "dropDownList_EditUnitId");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledge, "hiddenField_EditAcknowledge");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledgeDate, "hiddenField_EditAcknowledgeDate");
      Control_IsNullOrEmpty(hiddenField_EditAcknowledgeBy, "hiddenField_EditAcknowledgeBy");
      Control_IsNullOrEmpty(checkBox_EditAcknowledge, "checkBox_EditAcknowledge");
      Control_IsNullOrEmpty(hiddenField_EditCloseout, "hiddenField_EditCloseout");
      Control_IsNullOrEmpty(hiddenField_EditCloseoutDate, "hiddenField_EditCloseoutDate");
      Control_IsNullOrEmpty(hiddenField_EditCloseoutBy, "hiddenField_EditCloseoutBy");
      Control_IsNullOrEmpty(checkBox_EditCloseout, "checkBox_EditCloseout");

      if (sqlCommand_Edit != null && dropDownList_EditUnitId != null && hiddenField_EditAcknowledge != null && hiddenField_EditAcknowledgeDate != null && hiddenField_EditAcknowledgeBy != null && checkBox_EditAcknowledge != null && hiddenField_EditCloseout != null && hiddenField_EditCloseoutDate != null && hiddenField_EditCloseoutBy != null && checkBox_EditCloseout != null)
      {
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Compliment_CloseOutBy", DBNull.Value);

        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Type_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Category_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalType_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalUnit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AdditionalCategory_List", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Comment_CloseOutBy", DBNull.Value);

        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Unit_Id", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_Acknowledge", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_AcknowledgeBy", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOut", false);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutDate", DBNull.Value);
        sqlCommand_Edit.Parameters.AddWithValue("@CRM_Query_CloseOutBy", DBNull.Value);

        if (!string.IsNullOrEmpty(dropDownList_EditUnitId.SelectedValue.ToString()))
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Unit_Id", dropDownList_EditUnitId.SelectedValue);
        }
        else
        {
          sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Unit_Id", DBNull.Value);
        }

        if (hiddenField_EditAcknowledge.Value.ToString() == "False")
        {
          if (checkBox_EditAcknowledge.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", true);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeDate", DateTime.Now.ToString());
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeBy", Request.ServerVariables["LOGON_USER"]);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeBy", DBNull.Value);
          }
        }
        else
        {
          if (checkBox_EditAcknowledge.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", hiddenField_EditAcknowledge.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeDate", hiddenField_EditAcknowledgeDate.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeBy", hiddenField_EditAcknowledgeBy.Value);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_Acknowledge", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_AcknowledgeBy", DBNull.Value);
          }
        }

        if (hiddenField_EditCloseout.Value.ToString() == "False")
        {
          if (checkBox_EditCloseout.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", true);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutDate", DateTime.Now.ToString());
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutBy", Request.ServerVariables["LOGON_USER"]);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutBy", DBNull.Value);
          }
        }
        else
        {
          if (checkBox_EditCloseout.Checked == true)
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", hiddenField_EditCloseout.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutDate", hiddenField_EditCloseoutDate.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutBy", hiddenField_EditCloseoutBy.Value);
          }
          else
          {
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOut", false);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutDate", DBNull.Value);
            sqlCommand_Edit.Parameters.AddWithValue("@CRM_Suggestion_CloseOutBy", DBNull.Value);
          }
        }
      }
    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_CRM_IncompleteOther.SetFocus(ImageButton_IncompleteOther);

      GridView_CRM_IncompleteOther.DataBind();
      GridView_CRM_IncompleteOtherBulkApproval.DataBind();
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
      string SearchField4 = Request.QueryString["s_CRM_TypeList"];
      string SearchField5 = Request.QueryString["s_CRM_PatientVisitNumber"];
      string SearchField6 = Request.QueryString["s_CRM_Name"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "SearchIO_FacilityType=" + Request.QueryString["s_Facility_Type"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "SearchIO_FacilityId=" + Request.QueryString["s_Facility_Id"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "SearchIO_CRMReportNumber=" + Request.QueryString["s_CRM_ReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "SearchIO_CRMTypeList=" + Request.QueryString["s_CRM_TypeList"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "SearchIO_CRMPatientVisitNumber=" + Request.QueryString["s_CRM_PatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "SearchIO_CRMName=" + Request.QueryString["s_CRM_Name"] + "&";
      }

      string SearchURL = SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + "Search_CRMForm=IncompleteOther&";
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

    protected void Button_AcknowledgeAll_DataBinding(object sender, EventArgs e)
    {
      Button Button_Cancel = (Button)sender;
      Button_Cancel.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Acknowledge All');");
    }

    protected void Button_AcknowledgeCloseoutAll_DataBinding(object sender, EventArgs e)
    {
      Button Button_Cancel = (Button)sender;
      Button_Cancel.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Acknowledge and Closeout All');");
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

    protected void LinkButton_EditCommentPXMPDCHResults_OnClick(object sender, EventArgs e)
    {
      LinkButton LinkButton_EditCommentPXMPDCHResults = (LinkButton)sender;
      GridViewRow GridViewRow_CRM_IncompleteOther = (GridViewRow)LinkButton_EditCommentPXMPDCHResults.NamingContainer;
      HiddenField HiddenField_EditCRMId = (HiddenField)GridViewRow_CRM_IncompleteOther.FindControl("HiddenField_EditCRMId");

      string CRM_Id = HiddenField_EditCRMId.Value;

      PXM_PDCH_Results(CRM_Id);
    }
    //---END--- --IncompleteOther--//


    //--START-- --IncompleteOtherBulkApproval--//
    protected void SqlDataSource_CRM_IncompleteOtherBulkApproval_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords_BulkApproval.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged_BulkApproval(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_IncompleteOtherBulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_CRM_IncompleteOtherBulkApproval.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged_BulkApproval(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_CRM_IncompleteOtherBulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_CRM_IncompleteOtherBulkApproval.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_CRM_IncompleteOtherBulkApproval_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_CRM_IncompleteOtherBulkApproval.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_CRM_IncompleteOtherBulkApproval.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_CRM_IncompleteOtherBulkApproval.PageSize > 20 && GridView_CRM_IncompleteOtherBulkApproval.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_CRM_IncompleteOtherBulkApproval.PageSize > 50 && GridView_CRM_IncompleteOtherBulkApproval.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_CRM_IncompleteOtherBulkApproval_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_CRM_IncompleteOtherBulkApproval.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_CRM_IncompleteOtherBulkApproval.PageCount; i++)
          {
            int pageNumber = i + 1;
            System.Web.UI.WebControls.ListItem ListItem_Item = new System.Web.UI.WebControls.ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_CRM_IncompleteOtherBulkApproval.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_CRM_IncompleteOtherBulkApproval_RowCreated(object sender, GridViewRowEventArgs e)
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
      GridViewRow GridViewRow_CRM_IncompleteOtherBulkApproval = (GridViewRow)DropDownList_EditStatus.NamingContainer;
      Label Label_UpdateStatusRejectedLabel = (Label)GridViewRow_CRM_IncompleteOtherBulkApproval.FindControl("Label_UpdateStatusRejectedLabel");
      TextBox TextBox_EditStatusRejectedReason = (TextBox)GridViewRow_CRM_IncompleteOtherBulkApproval.FindControl("TextBox_EditStatusRejectedReason");

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

    protected void Button_ApproveAll_BulkApproval_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_CRM_IncompleteOtherBulkApproval.Rows.Count; i++)
      {
        DropDownList DropDownList_EditStatus = (DropDownList)GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Cells[0].FindControl("DropDownList_EditStatus");

        DropDownList_EditStatus.SelectedValue = "Approved";
      }

      Button_Update_BulkApproval_Click(sender, e);
    }

    protected void Button_Update_BulkApproval_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_CRM_IncompleteOther.SetFocus(ImageButton_BulkApproval);

      string Proceed = "Yes";

      for (int i = 0; i < GridView_CRM_IncompleteOtherBulkApproval.Rows.Count; i++)
      {
        if (Proceed == "Yes")
        {
          HiddenField HiddenField_EditCRMId = (HiddenField)GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Cells[0].FindControl("HiddenField_EditCRMId");
          DropDownList DropDownList_EditStatus = (DropDownList)GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Cells[0].FindControl("DropDownList_EditStatus");
          HiddenField HiddenField_EditStatus = (HiddenField)GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Cells[0].FindControl("HiddenField_EditStatus");
          HiddenField HiddenField_EditStatusDate = (HiddenField)GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Cells[0].FindControl("HiddenField_EditStatusDate");
          TextBox TextBox_EditStatusRejectedReason = (TextBox)GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Cells[0].FindControl("TextBox_EditStatusRejectedReason");

          HiddenField HiddenField_EditModifiedDate = (HiddenField)GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Cells[0].FindControl("HiddenField_EditModifiedDate");
          Label Label_EditInvalidFormMessage = (Label)GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Cells[0].FindControl("Label_EditInvalidFormMessage");
          Label Label_EditConcurrencyUpdateMessage = (Label)GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Cells[0].FindControl("Label_EditConcurrencyUpdateMessage");

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
              string Label_EditInvalidFormMessageText = EditValidation_BulkApproval(DropDownList_EditStatus, TextBox_EditStatusRejectedReason);

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

                  DataView DataView_CRM = (DataView)SqlDataSource_CRM_IncompleteOtherBulkApproval.Select(DataSourceSelectArguments.Empty);
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

                GridView_CRM_IncompleteOtherBulkApproval.Rows[i].Visible = false;
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
        GridView_CRM_IncompleteOther.DataBind();
        GridView_CRM_IncompleteOtherBulkApproval.DataBind();
      }
    }

    protected static string EditValidation_BulkApproval(ListControl dropDownList_EditStatus, TextBox textBox_EditStatusRejectedReason)
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

    protected void Button_Cancel_BulkApproval_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_CRM_IncompleteOther.SetFocus(ImageButton_BulkApproval);

      GridView_CRM_IncompleteOther.DataBind();
      GridView_CRM_IncompleteOtherBulkApproval.DataBind();
    }

    protected void Button_ApproveAll_BulkApproval_DataBinding(object sender, EventArgs e)
    {
      Button Button_Cancel = (Button)sender;
      Button_Cancel.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Approve All');");
    }

    protected void Button_Update_BulkApproval_DataBinding(object sender, EventArgs e)
    {
      Button Button_Cancel = (Button)sender;
      Button_Cancel.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Update the changes');");
    }

    protected void Button_Cancel_BulkApproval_DataBinding(object sender, EventArgs e)
    {
      Button Button_Cancel = (Button)sender;
      Button_Cancel.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Cancel the changes');");
    }
    //---END--- --IncompleteOtherBulkApproval--//
  }
}