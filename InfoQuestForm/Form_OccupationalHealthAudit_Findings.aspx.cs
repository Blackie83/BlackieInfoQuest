using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_OccupationalHealthAudit_Findings : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_OccupationalHealthAudit_Findings, this.GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);


          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString() + " : Findings", CultureInfo.CurrentCulture);
          Label_FindingsInfoHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_FindingsHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString() + " Findings", CultureInfo.CurrentCulture);


          if (Request.QueryString["OHA_Findings_Id"] != null && Request.QueryString["OHA_Id"] != null)
          {
            SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters["TableSELECT"].DefaultValue = "OHA_Findings_Tracking_List";
            SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters["TableFROM"].DefaultValue = "Form_OccupationalHealthAudit_Findings";
            SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters["TableWHERE"].DefaultValue = "OHA_Findings_Id = " + Request.QueryString["OHA_Findings_Id"] + " ";

            SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters["TableSELECT"].DefaultValue = "OHA_Findings_LateClosingOutCNC_List";
            SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters["TableFROM"].DefaultValue = "Form_OccupationalHealthAudit_Findings";
            SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters["TableWHERE"].DefaultValue = "OHA_Findings_Id = " + Request.QueryString["OHA_Findings_Id"] + " ";

            TableFindingsInfo.Visible = true;
            TableFindingsForm.Visible = true;

            OccupationalHealthAuditFindings();

            if (TableFindingsForm.Visible == true)
            {
              SetFormVisibility();
            }
          }
          else
          {
            TableFindingsInfo.Visible = false;
            TableFindingsForm.Visible = false;
          }

          if (TableFindingsInfo.Visible == true)
          {
            TableReviewInfoVisible();
          }

          if (TableFindingsForm.Visible == true)
          {
            TableFormVisible();
          }
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

        string SQLStringSecurity = "";
        if (Request.QueryString["OHA_Id"] == null || Request.QueryString["OHA_Findings_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('48')) AND (Facility_Id IN (SELECT Facility_Id FROM vForm_OccupationalHealthAudit_Findings WHERE OHA_Id = @OHA_Id AND OHA_Findings_Id = @OHA_Findings_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
          SqlCommand_Security.Parameters.AddWithValue("@OHA_Findings_Id", Request.QueryString["OHA_Findings_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("48");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_OccupationalHealthAudit_Findings.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Occupational Health Audit", "25");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_OccupationalHealthAudit_Findings_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.SelectCommand = "SELECT * FROM Form_OccupationalHealthAudit_Findings WHERE (OHA_Findings_Id = @OHA_Findings_Id)";
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateCommand = "UPDATE Form_OccupationalHealthAudit_Findings SET OHA_Findings_RootCause = @OHA_Findings_RootCause , OHA_Findings_Actions = @OHA_Findings_Actions , OHA_Findings_ResponsiblePerson = @OHA_Findings_ResponsiblePerson ,[OHA_Findings_DueDate] = @OHA_Findings_DueDate ,[OHA_Findings_ActionsEffective] = @OHA_Findings_ActionsEffective ,[OHA_Findings_Tracking_List] = @OHA_Findings_Tracking_List ,[OHA_Findings_TrackingDate] = @OHA_Findings_TrackingDate , [OHA_Findings_LateClosingOutCNC_List] = @OHA_Findings_LateClosingOutCNC_List , [OHA_Findings_LateClosingOutCNC_List_Other] = @OHA_Findings_LateClosingOutCNC_List_Other ,[OHA_Findings_ModifiedDate] = @OHA_Findings_ModifiedDate ,[OHA_Findings_ModifiedBy] = @OHA_Findings_ModifiedBy ,[OHA_Findings_History] = @OHA_Findings_History WHERE [OHA_Findings_Id] = @OHA_Findings_Id";
      SqlDataSource_OccupationalHealthAudit_Findings_Form.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Findings_Form.SelectParameters.Add("OHA_Findings_Id", TypeCode.Int32, Request.QueryString["OHA_Findings_Id"]);
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_RootCause", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_Actions", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_ResponsiblePerson", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_DueDate", TypeCode.DateTime, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_ActionsEffective", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_Tracking_List", TypeCode.Int32, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_TrackingDate", TypeCode.DateTime, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_LateClosingOutCNC_List", TypeCode.Int32, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_LateClosingOutCNC_List_Other", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_ModifiedBy", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_History", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Findings_Form.UpdateParameters.Add("OHA_Findings_Id", TypeCode.Int32, "");

      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters.Add("Form_Id", TypeCode.String, "48");
      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "208");
      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_OccupationalHealthAudit_Findings_EditTrackingList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.SelectParameters.Add("Form_Id", TypeCode.String, "48");
      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "209");
      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_OccupationalHealthAudit_Findings_EditLateClosingOutCNCList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
    }

    private void OccupationalHealthAuditFindings()
    {
      Session["OHAId"] = "";
      string SQLStringOccupationalHealthAudit = "SELECT OHA_Id FROM vForm_OccupationalHealthAudit_Findings WHERE OHA_Id = @OHA_Id AND OHA_Findings_Id = @OHA_Findings_Id AND OHA_IsActive = 1";
      using (SqlCommand SqlCommand_OccupationalHealthAudit = new SqlCommand(SQLStringOccupationalHealthAudit))
      {
        SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
        SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Findings_Id", Request.QueryString["OHA_Findings_Id"]);
        DataTable DataTable_OccupationalHealthAudit;
        using (DataTable_OccupationalHealthAudit = new DataTable())
        {
          DataTable_OccupationalHealthAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_OccupationalHealthAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OccupationalHealthAudit).Copy();
          if (DataTable_OccupationalHealthAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OccupationalHealthAudit.Rows)
            {
              Session["OHAId"] = DataRow_Row["OHA_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["OHAId"].ToString()))
      {
        TableFindingsInfo.Visible = false;
        TableFindingsForm.Visible = false;
      }
      else
      {
        TableFindingsInfo.Visible = true;
        TableFindingsForm.Visible = true;
      }
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('48')) AND (Facility_Id IN (SELECT Facility_Id FROM vForm_OccupationalHealthAudit_Findings WHERE OHA_Id = @OHA_Id AND OHA_Findings_Id = @OHA_Findings_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@LOGON_USER", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@OHA_Findings_Id", Request.QueryString["OHA_Findings_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '195'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '196'");
            DataRow[] SecurityFacilityAdminCompletion = DataTable_FormMode.Select("SecurityRole_Id = '197'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '198'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '199'");

            Session["OHAId"] = "";
            Session["OHAFindingsTrackingList"] = "";
            string SQLStringOccupationalHealthAudit = "SELECT TempTable.OHA_Id , Form_OccupationalHealthAudit_Findings.OHA_Findings_Tracking_List FROM (SELECT OHA_Id , RANK() OVER (ORDER BY OHA_Date DESC) AS OHA_Rank FROM Form_OccupationalHealthAudit WHERE Facility_Id IN (SELECT Facility_Id FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id AND OHA_Completed = 0) AND Unit_Id IN ( SELECT Unit_Id FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id AND OHA_Completed = 0 ) AND OHA_IsActive = 1) AS TempTable , Form_OccupationalHealthAudit_Findings WHERE TempTable.OHA_Id = Form_OccupationalHealthAudit_Findings.OHA_Id AND Form_OccupationalHealthAudit_Findings.OHA_Findings_Id = @OHA_Findings_Id AND TempTable.OHA_Rank = 1 AND TempTable.OHA_Id = @OHA_Id";
            using (SqlCommand SqlCommand_OccupationalHealthAudit = new SqlCommand(SQLStringOccupationalHealthAudit))
            {
              SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
              SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Findings_Id", Request.QueryString["OHA_Findings_Id"]);
              DataTable DataTable_OccupationalHealthAudit;
              using (DataTable_OccupationalHealthAudit = new DataTable())
              {
                DataTable_OccupationalHealthAudit.Locale = CultureInfo.CurrentCulture;
                DataTable_OccupationalHealthAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OccupationalHealthAudit).Copy();
                if (DataTable_OccupationalHealthAudit.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_OccupationalHealthAudit.Rows)
                  {
                    Session["OHAId"] = DataRow_Row["OHA_Id"];
                    Session["OHAFindingsTrackingList"] = DataRow_Row["OHA_Findings_Tracking_List"];
                  }
                }
              }
            }


            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["OHAId"].ToString()))
              {
                FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.Edit);
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAdminView.Length > 0)
            {
              Session["Security"] = "0";

              FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminCompletion.Length > 0)
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["OHAId"].ToString()))
              {
                FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.Edit);
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["OHAId"].ToString()))
              {
                FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                if (Session["OHAFindingsTrackingList"].ToString() == "6143")
                {
                  FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
                }
                else
                {
                  FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.Edit);
                }
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminView.Length > 0)
            {
              Session["Security"] = "0";

              FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";

              FormView_OccupationalHealthAudit_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            Session.Remove("OHAId");
            Session.Remove("OHAFindingsTrackingList");
          }
        }
      }
    }

    private void TableReviewInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["UnitName"] = "";
      Session["OHADate"] = "";
      Session["OHACompleted"] = "";
      string SQLStringReviewInfo = "SELECT DISTINCT Facility_FacilityDisplayName , Unit_Name , OHA_Date , OHA_Completed FROM vForm_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id";
      using (SqlCommand SqlCommand_ReviewInfo = new SqlCommand(SQLStringReviewInfo))
      {
        SqlCommand_ReviewInfo.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
        DataTable DataTable_ReviewInfo;
        using (DataTable_ReviewInfo = new DataTable())
        {
          DataTable_ReviewInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_ReviewInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ReviewInfo).Copy();
          if (DataTable_ReviewInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_ReviewInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["UnitName"] = DataRow_Row["Unit_Name"];
              Session["OHADate"] = DataRow_Row["OHA_Date"];
              Session["OHACompleted"] = DataRow_Row["OHA_Completed"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label_Facility.Text = Session["FacilityFacilityDisplayName"].ToString();
        Label_Unit.Text = Session["UnitName"].ToString();
        Label_Date.Text = Convert.ToDateTime(Session["OHADate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        if (Session["OHACompleted"].ToString() == "True")
        {
          Label_Completed.Text = Convert.ToString("Yes", CultureInfo.CurrentCulture);
        }
        else if (Session["OHACompleted"].ToString() == "False")
        {
          Label_Completed.Text = Convert.ToString("No", CultureInfo.CurrentCulture);
        }
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("UnitName");
      Session.Remove("OHADate");
      Session.Remove("OHACompleted");
    }

    private void TableFormVisible()
    {
      if (FormView_OccupationalHealthAudit_Findings_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditRootCause")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditRootCause")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditActions")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditActions")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditResponsiblePerson")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditResponsiblePerson")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditDueDate")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditDueDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditDueDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditDueDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditActionsEffective")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditTrackingList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditLateClosingOutCNCList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditLateClosingOutCNCListOther")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditLateClosingOutCNCListOther")).Attributes.Add("OnInput", "Validation_Form();");
      }
    }

    private void RedirectToFindingsList()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_OHAFindingsSystem"];
      string SearchField2 = Request.QueryString["Search_OHAFindingsCategory"];
      string SearchField3 = Request.QueryString["Search_OHAFindingsTracking"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null)
      {
        FinalURL = "Form_OccupationalHealthAudit_Findings_List.aspx?OHA_Id=" + Request.QueryString["OHA_Id"] + "";
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_OHA_Findings_System=" + Request.QueryString["Search_OHAFindingsSystem"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_OHA_Findings_Category=" + Request.QueryString["Search_OHAFindingsCategory"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_OHA_Findings_Tracking=" + Request.QueryString["Search_OHAFindingsTracking"] + "&";
        }

        string SearchURL = "Form_OccupationalHealthAudit_Findings_List.aspx?OHA_Id=" + Request.QueryString["OHA_Id"] + "&" + SearchField1 + SearchField2 + SearchField3;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }


    //--START-- --TableForm--//
    protected void FormView_OccupationalHealthAudit_Findings_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDOHAFindingsModifiedDate"] = e.OldValues["OHA_Findings_ModifiedDate"];
        object OLDOHAFindingsModifiedDate = Session["OLDOHAFindingsModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDOHAFindingsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareOccupationalHealthAuditFindings = (DataView)SqlDataSource_OccupationalHealthAudit_Findings_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareOccupationalHealthAuditFindings = DataView_CompareOccupationalHealthAuditFindings[0];
        Session["DBOHAFindingsModifiedDate"] = Convert.ToString(DataRowView_CompareOccupationalHealthAuditFindings["OHA_Findings_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBOHAFindingsModifiedBy"] = Convert.ToString(DataRowView_CompareOccupationalHealthAuditFindings["OHA_Findings_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBOHAFindingsModifiedDate = Session["DBOHAFindingsModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBOHAFindingsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBOHAFindingsModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_InvalidForm = "";

          string ValidDates = "Yes";
          TextBox TextBox_EditDueDateValidate = (TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditDueDate");
          if (!string.IsNullOrEmpty(TextBox_EditDueDateValidate.Text))
          {
            string DateToValidate = TextBox_EditDueDateValidate.Text.ToString();
            DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

            if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
            {
              Label_InvalidForm = Label_InvalidForm + "Not a valid Due date, date must be in the format yyyy/mm/dd<br />";
              ValidDates = "No";
            }
          }

          if (ValidDates == "Yes")
          {
            DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditTrackingList");

            if (DropDownList_EditTrackingList.SelectedValue == "6143")
            {
              TextBox TextBox_EditRootCause = (TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditRootCause");
              TextBox TextBox_EditActions = (TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditActions");
              TextBox TextBox_EditResponsiblePerson = (TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditResponsiblePerson");
              TextBox TextBox_EditDueDate = (TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditDueDate");
              DropDownList DropDownList_EditActionsEffective = (DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditActionsEffective");
              DropDownList DropDownList_EditLateClosingOutCNCList = (DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditLateClosingOutCNCList");
              TextBox TextBox_EditLateClosingOutCNCListOther = (TextBox)FormView_OccupationalHealthAudit_Findings_Form.FindControl("TextBox_EditLateClosingOutCNCListOther");

              if (string.IsNullOrEmpty(TextBox_EditRootCause.Text) || string.IsNullOrEmpty(TextBox_EditActions.Text) || string.IsNullOrEmpty(TextBox_EditResponsiblePerson.Text) || string.IsNullOrEmpty(TextBox_EditDueDate.Text) || string.IsNullOrEmpty(DropDownList_EditActionsEffective.SelectedValue))
              {
                Label_InvalidForm = Label_InvalidForm + "All red fields are required<br />";
                e.Cancel = true;
              }
              else
              {
                if (DropDownList_EditLateClosingOutCNCList.Visible == true)
                {
                  if (string.IsNullOrEmpty(DropDownList_EditLateClosingOutCNCList.SelectedValue))
                  {
                    Label_InvalidForm = Label_InvalidForm + "All red fields are required<br />";
                    e.Cancel = true;
                  }
                  else
                  {
                    if (DropDownList_EditLateClosingOutCNCList.SelectedValue == "6149")
                    {
                      if (string.IsNullOrEmpty(TextBox_EditLateClosingOutCNCListOther.Text))
                      {
                        Label_InvalidForm = Label_InvalidForm + "All red fields are required<br />";
                        e.Cancel = true;
                      }
                      else
                      {
                        e.Cancel = false;
                      }
                    }
                    else
                    {
                      e.Cancel = false;
                    }
                  }
                }
                else
                {
                  e.Cancel = false;
                }
              }
            }
            else
            {
              e.Cancel = false;
            }
          }
          else
          {
            e.Cancel = true;
          }

          if (e.Cancel == true)
          {
            ((Label)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Label_InvalidForm")).Text = Convert.ToString(Label_InvalidForm, CultureInfo.CurrentCulture);
          }
          else if (e.Cancel == false)
          {
            e.NewValues["OHA_Findings_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["OHA_Findings_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_OccupationalHealthAudit_Findings", "OHA_Findings_Id = " + Request.QueryString["OHA_Findings_Id"]);

            DataView DataView_OccupationalHealthAudit_Findings = (DataView)SqlDataSource_OccupationalHealthAudit_Findings_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_OccupationalHealthAudit_Findings = DataView_OccupationalHealthAudit_Findings[0];
            Session["OHAFindingsHistory"] = Convert.ToString(DataRowView_OccupationalHealthAudit_Findings["OHA_Findings_History"], CultureInfo.CurrentCulture);

            Session["OHAFindingsHistory"] = Session["History"].ToString() + Session["OHAFindingsHistory"].ToString();
            e.NewValues["OHA_Findings_History"] = Session["OHAFindingsHistory"].ToString();

            Session["OHAFindingsHistory"] = "";
            Session["History"] = "";

            DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditTrackingList");

            if (!string.IsNullOrEmpty(DropDownList_EditTrackingList.SelectedValue))
            {
              if (e.OldValues["OHA_Findings_Tracking_List"].ToString() == DropDownList_EditTrackingList.SelectedValue)
              {
                e.NewValues["OHA_Findings_TrackingDate"] = e.OldValues["OHA_Findings_TrackingDate"];
              }
              else
              {
                e.NewValues["OHA_Findings_TrackingDate"] = DateTime.Now.ToString();
              }
            }
            else
            {
              e.NewValues["OHA_Findings_TrackingDate"] = "";
            }
          }
        }

        Session.Remove("OLDOHAFindingsModifiedDate");
        Session.Remove("DBOHAFindingsModifiedDate");
        Session.Remove("DBOHAFindingsModifiedBy");
      }
    }

    protected void SqlDataSource_OccupationalHealthAudit_Findings_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Request.QueryString["OHA_Findings_Id"] != null && Request.QueryString["OHA_Id"] != null)
          {
            if (Button_EditUpdateClicked == true)
            {
              Button_EditUpdateClicked = false;
              RedirectToFindingsList();
            }

            if (Button_EditPrintClicked == true)
            {
              Button_EditPrintClicked = false;

              ScriptManager.RegisterStartupScript(UpdatePanel_OccupationalHealthAudit_Findings, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Internal Quality Audit Findings Print", "InfoQuest_Print.aspx?PrintPage=Form_OccupationalHealthAudit_Findings&PrintValue=" + Request.QueryString["OHA_Findings_Id"] + "") + "')", true);
              ScriptManager.RegisterStartupScript(UpdatePanel_OccupationalHealthAudit_Findings, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
            }

            if (Button_EditEmailClicked == true)
            {
              Button_EditEmailClicked = false;

              ScriptManager.RegisterStartupScript(UpdatePanel_OccupationalHealthAudit_Findings, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Internal Quality Audit Findings Email", "InfoQuest_Email.aspx?EmailPage=Form_OccupationalHealthAudit_Findings&EmailValue=" + Request.QueryString["OHA_Findings_Id"] + "") + "')", true);
              ScriptManager.RegisterStartupScript(UpdatePanel_OccupationalHealthAudit_Findings, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
            }
          }
        }
      }
    }


    protected void FormView_OccupationalHealthAudit_Findings_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["OHA_Findings_Id"] != null && Request.QueryString["OHA_Id"] != null)
          {
            RedirectToFindingsList();
          }
        }
      }
    }

    protected void FormView_OccupationalHealthAudit_Findings_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_OccupationalHealthAudit_Findings_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["OHA_Findings_Id"] != null && Request.QueryString["OHA_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_OccupationalHealthAudit_Findings_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["OHA_Findings_Id"] != null && Request.QueryString["OHA_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void EditDataBound()
    {
      HiddenField HiddenField_EditCategory = (HiddenField)FormView_OccupationalHealthAudit_Findings_Form.FindControl("HiddenField_EditCategory");
      DropDownList DropDownList_EditLateClosingOutCNCList = (DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditLateClosingOutCNCList");
      if (HiddenField_EditCategory.Value != "CNC")
      {
        DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
        FormView_OccupationalHealthAudit_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
      }
      else
      {
        DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditTrackingList");
        if (DropDownList_EditTrackingList.SelectedValue != "6143")
        {
          DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
          FormView_OccupationalHealthAudit_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
        }
        else
        {
          HiddenField HiddenField_EditTrackingDate = (HiddenField)FormView_OccupationalHealthAudit_Findings_Form.FindControl("HiddenField_EditTrackingDate");
          HiddenField HiddenField_EditCreatedDate = (HiddenField)FormView_OccupationalHealthAudit_Findings_Form.FindControl("HiddenField_EditCreatedDate");
          DateTime TrackingDate = Convert.ToDateTime(Convert.ToDateTime(HiddenField_EditTrackingDate.Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(HiddenField_EditCreatedDate.Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddMonths(1);

          if (CreatedDateAdded.CompareTo(TrackingDate) >= 0)
          {
            DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
            FormView_OccupationalHealthAudit_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
          }
          else
          {
            FormView_OccupationalHealthAudit_Findings_Form.FindControl("LateClosingOutCNCList").Visible = true;
          }
        }
      }


      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 48";
      using (SqlCommand SqlCommand_Email = new SqlCommand(SQLStringEmail))
      {
        DataTable DataTable_Email;
        using (DataTable_Email = new DataTable())
        {
          DataTable_Email.Locale = CultureInfo.CurrentCulture;
          DataTable_Email = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Email).Copy();
          if (DataTable_Email.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Email.Rows)
            {
              Email = DataRow_Row["Form_Email"].ToString();
              Print = DataRow_Row["Form_Print"].ToString();
            }
          }
        }
      }

      if (Print == "False")
      {
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_EditPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_EditPrint")).Visible = true;
      }

      if (Email == "False")
      {
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_EditEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_EditEmail")).Visible = true;
      }

      Email = "";
      Print = "";
    }

    protected void ReadOnlyDataBound()
    {
      Session["OHAFindingsTrackingName"] = "";
      Session["OHAFindingsLateClosingOutCNCName"] = "";
      string SQLStringOccupationalHealthAudit = "SELECT OHA_Findings_Tracking_Name , OHA_Findings_LateClosingOutCNC_Name FROM vForm_OccupationalHealthAudit_Findings WHERE OHA_Findings_Id = @OHA_Findings_Id";
      using (SqlCommand SqlCommand_OccupationalHealthAudit = new SqlCommand(SQLStringOccupationalHealthAudit))
      {
        SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Findings_Id", Request.QueryString["OHA_Findings_Id"]);
        DataTable DataTable_OccupationalHealthAudit;
        using (DataTable_OccupationalHealthAudit = new DataTable())
        {
          DataTable_OccupationalHealthAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_OccupationalHealthAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OccupationalHealthAudit).Copy();
          if (DataTable_OccupationalHealthAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OccupationalHealthAudit.Rows)
            {
              Session["OHAFindingsTrackingName"] = DataRow_Row["OHA_Findings_Tracking_Name"];
              Session["OHAFindingsLateClosingOutCNCName"] = DataRow_Row["OHA_Findings_LateClosingOutCNC_Name"];
            }
          }
        }
      }

      Label Label_ItemTrackingList = (Label)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Label_ItemTrackingList");
      Label_ItemTrackingList.Text = Session["OHAFindingsTrackingName"].ToString();

      Label Label_ItemLateClosingOutCNCList = (Label)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Label_ItemLateClosingOutCNCList");
      Label_ItemLateClosingOutCNCList.Text = Session["OHAFindingsLateClosingOutCNCName"].ToString();

      Session["OHAFindingsTrackingName"] = "";


      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 48";
      using (SqlCommand SqlCommand_Email = new SqlCommand(SQLStringEmail))
      {
        DataTable DataTable_Email;
        using (DataTable_Email = new DataTable())
        {
          DataTable_Email.Locale = CultureInfo.CurrentCulture;
          DataTable_Email = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Email).Copy();
          if (DataTable_Email.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Email.Rows)
            {
              Email = DataRow_Row["Form_Email"].ToString();
              Print = DataRow_Row["Form_Print"].ToString();
            }
          }
        }
      }

      if (Print == "False")
      {
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_ItemPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_ItemPrint")).Visible = true;
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Print", "InfoQuest_Print.aspx?PrintPage=Form_OccupationalHealthAudit_Findings&PrintValue=" + Request.QueryString["OHA_Findings_Id"] + "") + "')";
      }

      if (Email == "False")
      {
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_ItemEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_ItemEmail")).Visible = true;
        ((Button)FormView_OccupationalHealthAudit_Findings_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Print", "InfoQuest_Email.aspx?EmailPage=Form_OccupationalHealthAudit_Findings&EmailValue=" + Request.QueryString["OHA_Findings_Id"] + "") + "')";
      }

      Email = "";
      Print = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }

    protected void Button_EditPrint_Click(object sender, EventArgs e)
    {
      Button_EditPrintClicked = true;
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      Button_EditEmailClicked = true;
    }

    protected void DropDownList_EditTrackingList_DataBound(object sender, EventArgs e)
    {
      if (FormView_OccupationalHealthAudit_Findings_Form.CurrentMode == FormViewMode.Edit)
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('48')) AND (Facility_Id IN (SELECT Facility_Id FROM vForm_OccupationalHealthAudit_Findings WHERE OHA_Id = @OHA_Id AND OHA_Findings_Id = @OHA_Findings_Id) OR SecurityRole_Id IN ('1','122','123'))";
        using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
        {
          SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_FormMode.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
          SqlCommand_FormMode.Parameters.AddWithValue("@OHA_Findings_Id", Request.QueryString["OHA_Findings_Id"]);
          DataTable DataTable_FormMode;
          using (DataTable_FormMode = new DataTable())
          {
            DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
            DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
            if (DataTable_FormMode.Rows.Count > 0)
            {
              DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '195'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '196'");
              DataRow[] SecurityFacilityAdminCompletion = DataTable_FormMode.Select("SecurityRole_Id = '197'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '198'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '199'");

              DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditTrackingList");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFormAdminView.Length > 0 || SecurityFacilityAdminCompletion.Length > 0))
              {
                Session["Security"] = "0";
              }

              if (Session["Security"].ToString() == "1" && (SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityAdminView.Length > 0))
              {
                Session["Security"] = "0";

                ListItem ListItem_EditTrackingListItem6143 = DropDownList_EditTrackingList.Items.FindByValue("6143");
                if (ListItem_EditTrackingListItem6143 != null)
                {
                  DropDownList_EditTrackingList.Items.Remove(ListItem_EditTrackingListItem6143);
                }
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";

                ListItem ListItem_EditTrackingListItem6143 = DropDownList_EditTrackingList.Items.FindByValue("6143");
                if (ListItem_EditTrackingListItem6143 != null)
                {
                  DropDownList_EditTrackingList.Items.Remove(ListItem_EditTrackingListItem6143);
                }
              }
            }
          }
        }
      }
    }

    protected void DropDownList_EditTrackingList_SelectedIndexChanged(object sender, EventArgs e)
    {
      HiddenField HiddenField_EditCategory = (HiddenField)FormView_OccupationalHealthAudit_Findings_Form.FindControl("HiddenField_EditCategory");
      DropDownList DropDownList_EditLateClosingOutCNCList = (DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditLateClosingOutCNCList");
      if (HiddenField_EditCategory.Value != "CNC")
      {
        DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
        FormView_OccupationalHealthAudit_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
      }
      else
      {
        DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_OccupationalHealthAudit_Findings_Form.FindControl("DropDownList_EditTrackingList");
        if (DropDownList_EditTrackingList.SelectedValue != "6143")
        {
          DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
          FormView_OccupationalHealthAudit_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
        }
        else
        {
          HiddenField HiddenField_EditCreatedDate = (HiddenField)FormView_OccupationalHealthAudit_Findings_Form.FindControl("HiddenField_EditCreatedDate");
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(HiddenField_EditCreatedDate.Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddMonths(1);

          if (CreatedDateAdded.CompareTo(CurrentDate) >= 0)
          {
            DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
            FormView_OccupationalHealthAudit_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
          }
          else
          {
            FormView_OccupationalHealthAudit_Findings_Form.FindControl("LateClosingOutCNCList").Visible = true;
          }
        }
      }
    }
    //---END--- --TableForm--//  
  }
}