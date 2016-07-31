using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_QMSReview_Findings : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_QMSReview_Findings, this.GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);


          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString() + " : Findings", CultureInfo.CurrentCulture);
          Label_FindingsInfoHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_FindingsHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString() + " Findings", CultureInfo.CurrentCulture);


          if (Request.QueryString["QMSReview_Findings_Id"] != null && Request.QueryString["QMSReview_Id"] != null)
          {
            SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters["TableSELECT"].DefaultValue = "QMSReview_Findings_Tracking_List";
            SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_QMSReview_Findings";
            SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters["TableWHERE"].DefaultValue = "QMSReview_Findings_Id = " + Request.QueryString["QMSReview_Findings_Id"] + " ";

            SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters["TableSELECT"].DefaultValue = "QMSReview_Findings_LateClosingOutCNC_List";
            SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_QMSReview_Findings";
            SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters["TableWHERE"].DefaultValue = "QMSReview_Findings_Id = " + Request.QueryString["QMSReview_Findings_Id"] + " ";

            TableFindingsInfo.Visible = true;
            TableFindingsForm.Visible = true;

            QMSReviewFindings();

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
        if (Request.QueryString["QMSReview_Id"] == null || Request.QueryString["QMSReview_Findings_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('31')) AND (Facility_Id IN (SELECT Facility_Id FROM vForm_QMSReview_Findings WHERE QMSReview_Id = @QMSReview_Id AND QMSReview_Findings_Id = @QMSReview_Findings_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
          SqlCommand_Security.Parameters.AddWithValue("@QMSReview_Findings_Id", Request.QueryString["QMSReview_Findings_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("31");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_QMSReview_Findings.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Internal Quality Audit", "12");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_QMSReview_Findings_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_QMSReview_Findings_Form.SelectCommand="SELECT * FROM InfoQuest_Form_QMSReview_Findings WHERE ([QMSReview_Findings_Id] = @QMSReview_Findings_Id)";
      SqlDataSource_QMSReview_Findings_Form.UpdateCommand = "UPDATE InfoQuest_Form_QMSReview_Findings SET QMSReview_Findings_RootCause = @QMSReview_Findings_RootCause , QMSReview_Findings_Actions = @QMSReview_Findings_Actions ,[QMSReview_Findings_ResponsiblePerson] = @QMSReview_Findings_ResponsiblePerson ,[QMSReview_Findings_DueDate] = @QMSReview_Findings_DueDate ,[QMSReview_Findings_ActionsEffective] = @QMSReview_Findings_ActionsEffective ,[QMSReview_Findings_Tracking_List] = @QMSReview_Findings_Tracking_List ,[QMSReview_Findings_TrackingDate] = @QMSReview_Findings_TrackingDate , [QMSReview_Findings_LateClosingOutCNC_List] = @QMSReview_Findings_LateClosingOutCNC_List , [QMSReview_Findings_LateClosingOutCNC_List_Other] = @QMSReview_Findings_LateClosingOutCNC_List_Other ,[QMSReview_Findings_ModifiedDate] = @QMSReview_Findings_ModifiedDate ,[QMSReview_Findings_ModifiedBy] = @QMSReview_Findings_ModifiedBy ,[QMSReview_Findings_History] = @QMSReview_Findings_History WHERE [QMSReview_Findings_Id] = @QMSReview_Findings_Id";
      SqlDataSource_QMSReview_Findings_Form.SelectParameters.Clear();
      SqlDataSource_QMSReview_Findings_Form.SelectParameters.Add("QMSReview_Findings_Id", TypeCode.Int32, Request.QueryString["QMSReview_Findings_Id"]);
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Clear();
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_RootCause", TypeCode.String, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_Actions", TypeCode.String, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_ResponsiblePerson", TypeCode.String, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_DueDate", TypeCode.DateTime, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_ActionsEffective", TypeCode.String, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_Tracking_List", TypeCode.Int32, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_TrackingDate", TypeCode.DateTime, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_LateClosingOutCNC_List", TypeCode.Int32, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_LateClosingOutCNC_List_Other", TypeCode.String, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_ModifiedBy", TypeCode.String, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_History", TypeCode.String, "");
      SqlDataSource_QMSReview_Findings_Form.UpdateParameters.Add("QMSReview_Findings_Id", TypeCode.Int32, "");

      SqlDataSource_QMSReview_Findings_EditTrackingList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_QMSReview_Findings_EditTrackingList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_QMSReview_Findings_EditTrackingList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters.Clear();
      SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters.Add("Form_Id", TypeCode.String, "31");
      SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "94");
      SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_QMSReview_Findings_EditTrackingList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.SelectParameters.Clear();
      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.SelectParameters.Add("Form_Id", TypeCode.String, "31");
      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "97");
      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_QMSReview_Findings_EditLateClosingOutCNCList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
    }

    private void QMSReviewFindings()
    {
      Session["QMSReviewId"] = "";
      string SQLStringQMSReview = "SELECT QMSReview_Id FROM vForm_QMSReview_Findings WHERE QMSReview_Id = @QMSReview_Id AND QMSReview_Findings_Id = @QMSReview_Findings_Id AND QMSReview_IsActive = 1";
      using (SqlCommand SqlCommand_QMSReview = new SqlCommand(SQLStringQMSReview))
      {
        SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
        SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Findings_Id", Request.QueryString["QMSReview_Findings_Id"]);
        DataTable DataTable_QMSReview;
        using (DataTable_QMSReview = new DataTable())
        {
          DataTable_QMSReview.Locale = CultureInfo.CurrentCulture;
          DataTable_QMSReview = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QMSReview).Copy();
          if (DataTable_QMSReview.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_QMSReview.Rows)
            {
              Session["QMSReviewId"] = DataRow_Row["QMSReview_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["QMSReviewId"].ToString()))
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
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('31')) AND (Facility_Id IN (SELECT Facility_Id FROM vForm_QMSReview_Findings WHERE QMSReview_Id = @QMSReview_Id AND QMSReview_Findings_Id = @QMSReview_Findings_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@LOGON_USER", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@QMSReview_Findings_Id", Request.QueryString["QMSReview_Findings_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '122'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '123'");
            DataRow[] SecurityFacilityAdminCompletion = DataTable_FormMode.Select("SecurityRole_Id = '126'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '124'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '125'");

            Session["QMSReviewId"] = "";
            Session["QMSReviewFindingsTrackingList"] = "";
            string SQLStringQMSReview = "SELECT TempTable.QMSReview_Id ,InfoQuest_Form_QMSReview_Findings.QMSReview_Findings_Tracking_List FROM (SELECT QMSReview_Id , RANK() OVER (ORDER BY QMSReview_Date DESC) AS QMSReview_Rank FROM InfoQuest_Form_QMSReview WHERE Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id AND QMSReview_Completed = 0) AND QMSReview_IsActive = 1) AS TempTable , InfoQuest_Form_QMSReview_Findings WHERE TempTable.QMSReview_Id = InfoQuest_Form_QMSReview_Findings.QMSReview_Id AND InfoQuest_Form_QMSReview_Findings.QMSReview_Findings_Id = @QMSReview_Findings_Id AND TempTable.QMSReview_Rank = 1 AND TempTable.QMSReview_Id = @QMSReview_Id";
            using (SqlCommand SqlCommand_QMSReview = new SqlCommand(SQLStringQMSReview))
            {
              SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
              SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Findings_Id", Request.QueryString["QMSReview_Findings_Id"]);
              DataTable DataTable_QMSReview;
              using (DataTable_QMSReview = new DataTable())
              {
                DataTable_QMSReview.Locale = CultureInfo.CurrentCulture;
                DataTable_QMSReview = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QMSReview).Copy();
                if (DataTable_QMSReview.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_QMSReview.Rows)
                  {
                    Session["QMSReviewId"] = DataRow_Row["QMSReview_Id"];
                    Session["QMSReviewFindingsTrackingList"] = DataRow_Row["QMSReview_Findings_Tracking_List"];
                  }
                }
              }
            }


            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["QMSReviewId"].ToString()))
              {
                FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.Edit);
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAdminView.Length > 0)
            {
              Session["Security"] = "0";

              FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminCompletion.Length > 0)
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["QMSReviewId"].ToString()))
              {
                FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.Edit);
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["QMSReviewId"].ToString()))
              {
                FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                if (Session["QMSReviewFindingsTrackingList"].ToString() == "4316")
                {
                  FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
                }
                else
                {
                  FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.Edit);
                }
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminView.Length > 0)
            {
              Session["Security"] = "0";

              FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";

              FormView_QMSReview_Findings_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            Session.Remove("QMSReviewId");
            Session.Remove("QMSReviewFindingsTrackingList");
          }
        }
      }
    }

    private void TableReviewInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["QMSReviewDate"] = "";
      Session["QMSReviewCompleted"] = "";
      string SQLStringReviewInfo = "SELECT DISTINCT Facility_FacilityDisplayName , QMSReview_Date , QMSReview_Completed FROM vForm_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
      using (SqlCommand SqlCommand_ReviewInfo = new SqlCommand(SQLStringReviewInfo))
      {
        SqlCommand_ReviewInfo.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
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
              Session["QMSReviewDate"] = DataRow_Row["QMSReview_Date"];
              Session["QMSReviewCompleted"] = DataRow_Row["QMSReview_Completed"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label_Facility.Text = Session["FacilityFacilityDisplayName"].ToString();
        Label_Date.Text = Convert.ToDateTime(Session["QMSReviewDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        if (Session["QMSReviewCompleted"].ToString() == "True")
        {
          Label_Completed.Text = Convert.ToString("Yes", CultureInfo.CurrentCulture);
        }
        else if (Session["QMSReviewCompleted"].ToString() == "False")
        {
          Label_Completed.Text = Convert.ToString("No", CultureInfo.CurrentCulture);
        }
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("QMSReviewDate");
      Session.Remove("QMSReviewCompleted");
    }

    private void TableFormVisible()
    {
      if (FormView_QMSReview_Findings_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditRootCause")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditRootCause")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditActions")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditActions")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditResponsiblePerson")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditResponsiblePerson")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditDueDate")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditDueDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditDueDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditDueDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditActionsEffective")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditTrackingList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditLateClosingOutCNCList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditLateClosingOutCNCListOther")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditLateClosingOutCNCListOther")).Attributes.Add("OnInput", "Validation_Form();");
      }
    }

    private void RedirectToFindingsList()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_QMSReviewFindingsSystem"];
      string SearchField2 = Request.QueryString["Search_QMSReviewFindingsCategory"];
      string SearchField3 = Request.QueryString["Search_QMSReviewFindingsTracking"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null)
      {
        FinalURL = "Form_QMSReview_Findings_List.aspx?QMSReview_Id=" + Request.QueryString["QMSReview_Id"] + "";
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_QMSReview_Findings_System=" + Request.QueryString["Search_QMSReviewFindingsSystem"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_QMSReview_Findings_Category=" + Request.QueryString["Search_QMSReviewFindingsCategory"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_QMSReview_Findings_Tracking=" + Request.QueryString["Search_QMSReviewFindingsTracking"] + "&";
        }

        string SearchURL = "Form_QMSReview_Findings_List.aspx?QMSReview_Id=" + Request.QueryString["QMSReview_Id"] + "&" + SearchField1 + SearchField2 + SearchField3;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }

    
    //--START-- --TableForm--//
    protected void FormView_QMSReview_Findings_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDQMSReviewFindingsModifiedDate"] = e.OldValues["QMSReview_Findings_ModifiedDate"];
        object OLDQMSReviewFindingsModifiedDate = Session["OLDQMSReviewFindingsModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDQMSReviewFindingsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareQMSReviewFindings = (DataView)SqlDataSource_QMSReview_Findings_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareQMSReviewFindings = DataView_CompareQMSReviewFindings[0];
        Session["DBQMSReviewFindingsModifiedDate"] = Convert.ToString(DataRowView_CompareQMSReviewFindings["QMSReview_Findings_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBQMSReviewFindingsModifiedBy"] = Convert.ToString(DataRowView_CompareQMSReviewFindings["QMSReview_Findings_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBQMSReviewFindingsModifiedDate = Session["DBQMSReviewFindingsModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBQMSReviewFindingsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_QMSReview_Findings_Form.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_QMSReview_Findings_Form.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBQMSReviewFindingsModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_InvalidForm = "";

          string ValidDates = "Yes";
          TextBox TextBox_EditDueDateValidate = (TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditDueDate");
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
            DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditTrackingList");

            if (DropDownList_EditTrackingList.SelectedValue == "4316")
            {
              TextBox TextBox_EditRootCause = (TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditRootCause");
              TextBox TextBox_EditActions = (TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditActions");
              TextBox TextBox_EditResponsiblePerson = (TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditResponsiblePerson");
              TextBox TextBox_EditDueDate = (TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditDueDate");
              DropDownList DropDownList_EditActionsEffective = (DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditActionsEffective");
              DropDownList DropDownList_EditLateClosingOutCNCList = (DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditLateClosingOutCNCList");
              TextBox TextBox_EditLateClosingOutCNCListOther = (TextBox)FormView_QMSReview_Findings_Form.FindControl("TextBox_EditLateClosingOutCNCListOther");

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
                    if (DropDownList_EditLateClosingOutCNCList.SelectedValue == "4328")
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
            ((Label)FormView_QMSReview_Findings_Form.FindControl("Label_InvalidForm")).Text = Convert.ToString(Label_InvalidForm, CultureInfo.CurrentCulture);
          }
          else if (e.Cancel == false)
          {
            e.NewValues["QMSReview_Findings_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["QMSReview_Findings_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_QMSReview_Findings", "QMSReview_Findings_Id = " + Request.QueryString["QMSReview_Findings_Id"]);

            DataView DataView_QMSReview_Findings = (DataView)SqlDataSource_QMSReview_Findings_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_QMSReview_Findings = DataView_QMSReview_Findings[0];
            Session["QMSReviewFindingsHistory"] = Convert.ToString(DataRowView_QMSReview_Findings["QMSReview_Findings_History"], CultureInfo.CurrentCulture);

            Session["QMSReviewFindingsHistory"] = Session["History"].ToString() + Session["QMSReviewFindingsHistory"].ToString();
            e.NewValues["QMSReview_Findings_History"] = Session["QMSReviewFindingsHistory"].ToString();

            Session["QMSReviewFindingsHistory"] = "";
            Session["History"] = "";

            DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditTrackingList");

            if (!string.IsNullOrEmpty(DropDownList_EditTrackingList.SelectedValue))
            {
              if (e.OldValues["QMSReview_Findings_Tracking_List"].ToString() == DropDownList_EditTrackingList.SelectedValue)
              {
                e.NewValues["QMSReview_Findings_TrackingDate"] = e.OldValues["QMSReview_Findings_TrackingDate"];
              }
              else
              {
                e.NewValues["QMSReview_Findings_TrackingDate"] = DateTime.Now.ToString();
              }
            }
            else
            {
              e.NewValues["QMSReview_Findings_TrackingDate"] = "";
            }
          }
        }

        Session.Remove("OLDQMSReviewFindingsModifiedDate");
        Session.Remove("DBQMSReviewFindingsModifiedDate");
        Session.Remove("DBQMSReviewFindingsModifiedBy");
      }
    }
    
    protected void SqlDataSource_QMSReview_Findings_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Request.QueryString["QMSReview_Findings_Id"] != null && Request.QueryString["QMSReview_Id"] != null)
          {
            if (Button_EditUpdateClicked == true)
            {
              Button_EditUpdateClicked = false;
              RedirectToFindingsList();
            }

            if (Button_EditPrintClicked == true)
            {
              Button_EditPrintClicked = false;

              ScriptManager.RegisterStartupScript(UpdatePanel_QMSReview_Findings, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Internal Quality Audit Findings Print", "InfoQuest_Print.aspx?PrintPage=Form_QMSReview_Findings&PrintValue=" + Request.QueryString["QMSReview_Findings_Id"] + "") + "')", true);
              ScriptManager.RegisterStartupScript(UpdatePanel_QMSReview_Findings, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
            }

            if (Button_EditEmailClicked == true)
            {
              Button_EditEmailClicked = false;

              ScriptManager.RegisterStartupScript(UpdatePanel_QMSReview_Findings, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Internal Quality Audit Findings Email", "InfoQuest_Email.aspx?EmailPage=Form_QMSReview_Findings&EmailValue=" + Request.QueryString["QMSReview_Findings_Id"] + "") + "')", true);
              ScriptManager.RegisterStartupScript(UpdatePanel_QMSReview_Findings, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
            }
          }
        }
      }
    }


    protected void FormView_QMSReview_Findings_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["QMSReview_Findings_Id"] != null && Request.QueryString["QMSReview_Id"] != null)
          {
            RedirectToFindingsList();
          }
        }
      }
    }

    protected void FormView_QMSReview_Findings_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_QMSReview_Findings_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["QMSReview_Findings_Id"] != null && Request.QueryString["QMSReview_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_QMSReview_Findings_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["QMSReview_Findings_Id"] != null && Request.QueryString["QMSReview_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void EditDataBound()
    {
      HiddenField HiddenField_EditCategory = (HiddenField)FormView_QMSReview_Findings_Form.FindControl("HiddenField_EditCategory");
      DropDownList DropDownList_EditLateClosingOutCNCList = (DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditLateClosingOutCNCList");
      if (HiddenField_EditCategory.Value != "CNC")
      {
        DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
        FormView_QMSReview_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
      }
      else
      {
        DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditTrackingList");
        if (DropDownList_EditTrackingList.SelectedValue != "4316")
        {
          DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
          FormView_QMSReview_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
        }
        else
        {
          HiddenField HiddenField_EditTrackingDate = (HiddenField)FormView_QMSReview_Findings_Form.FindControl("HiddenField_EditTrackingDate");
          HiddenField HiddenField_EditCreatedDate = (HiddenField)FormView_QMSReview_Findings_Form.FindControl("HiddenField_EditCreatedDate");
          DateTime TrackingDate = Convert.ToDateTime(Convert.ToDateTime(HiddenField_EditTrackingDate.Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(HiddenField_EditCreatedDate.Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddMonths(1);

          if (CreatedDateAdded.CompareTo(TrackingDate) >= 0)
          {
            DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
            FormView_QMSReview_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
          }
          else
          {
            FormView_QMSReview_Findings_Form.FindControl("LateClosingOutCNCList").Visible = true;
          }
        }
      }


      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 31";
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
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_EditPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_EditPrint")).Visible = true;
      }

      if (Email == "False")
      {
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_EditEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_EditEmail")).Visible = true;
      }

      Email = "";
      Print = "";
    }

    protected void ReadOnlyDataBound()
    {
      Session["QMSReviewFindingsTrackingName"] = "";
      Session["QMSReviewFindingsLateClosingOutCNCName"] = "";
      string SQLStringQMSReview = "SELECT QMSReview_Findings_Tracking_Name , QMSReview_Findings_LateClosingOutCNC_Name FROM vForm_QMSReview_Findings WHERE QMSReview_Findings_Id = @QMSReview_Findings_Id";
      using (SqlCommand SqlCommand_QMSReview = new SqlCommand(SQLStringQMSReview))
      {
        SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Findings_Id", Request.QueryString["QMSReview_Findings_Id"]);
        DataTable DataTable_QMSReview;
        using (DataTable_QMSReview = new DataTable())
        {
          DataTable_QMSReview.Locale = CultureInfo.CurrentCulture;
          DataTable_QMSReview = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QMSReview).Copy();
          if (DataTable_QMSReview.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_QMSReview.Rows)
            {
              Session["QMSReviewFindingsTrackingName"] = DataRow_Row["QMSReview_Findings_Tracking_Name"];
              Session["QMSReviewFindingsLateClosingOutCNCName"] = DataRow_Row["QMSReview_Findings_LateClosingOutCNC_Name"];
            }
          }
        }
      }

      Label Label_ItemTrackingList = (Label)FormView_QMSReview_Findings_Form.FindControl("Label_ItemTrackingList");
      Label_ItemTrackingList.Text = Session["QMSReviewFindingsTrackingName"].ToString();

      Label Label_ItemLateClosingOutCNCList = (Label)FormView_QMSReview_Findings_Form.FindControl("Label_ItemLateClosingOutCNCList");
      Label_ItemLateClosingOutCNCList.Text = Session["QMSReviewFindingsLateClosingOutCNCName"].ToString();

      Session["QMSReviewFindingsTrackingName"] = "";


      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 31";
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
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_ItemPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_ItemPrint")).Visible = true;
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Print", "InfoQuest_Print.aspx?PrintPage=Form_QMSReview_Findings&PrintValue=" + Request.QueryString["QMSReview_Findings_Id"] + "") + "')";
      }

      if (Email == "False")
      {
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_ItemEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_ItemEmail")).Visible = true;
        ((Button)FormView_QMSReview_Findings_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Customer Relationship Management Print", "InfoQuest_Email.aspx?EmailPage=Form_QMSReview_Findings&EmailValue=" + Request.QueryString["QMSReview_Findings_Id"] + "") + "')";
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
      if (FormView_QMSReview_Findings_Form.CurrentMode == FormViewMode.Edit)
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('31')) AND (Facility_Id IN (SELECT Facility_Id FROM vForm_QMSReview_Findings WHERE QMSReview_Id = @QMSReview_Id AND QMSReview_Findings_Id = @QMSReview_Findings_Id) OR SecurityRole_Id IN ('1','122','123'))";
        using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
        {
          SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_FormMode.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
          SqlCommand_FormMode.Parameters.AddWithValue("@QMSReview_Findings_Id", Request.QueryString["QMSReview_Findings_Id"]);
          DataTable DataTable_FormMode;
          using (DataTable_FormMode = new DataTable())
          {
            DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
            DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
            if (DataTable_FormMode.Rows.Count > 0)
            {
              DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '122'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '123'");
              DataRow[] SecurityFacilityAdminCompletion = DataTable_FormMode.Select("SecurityRole_Id = '126'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '124'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '125'");

              DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditTrackingList");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFormAdminView.Length > 0 || SecurityFacilityAdminCompletion.Length > 0))
              {
                Session["Security"] = "0";
              }

              if (Session["Security"].ToString() == "1" && (SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityAdminView.Length > 0))
              {
                Session["Security"] = "0";

                ListItem ListItem_EditTrackingListItem4316 = DropDownList_EditTrackingList.Items.FindByValue("4316");
                if (ListItem_EditTrackingListItem4316 != null)
                {
                  DropDownList_EditTrackingList.Items.Remove(ListItem_EditTrackingListItem4316);
                }
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";

                ListItem ListItem_EditTrackingListItem4316 = DropDownList_EditTrackingList.Items.FindByValue("4316");
                if (ListItem_EditTrackingListItem4316 != null)
                {
                  DropDownList_EditTrackingList.Items.Remove(ListItem_EditTrackingListItem4316);
                }
              }
            }
          }
        }
      }
    }

    protected void DropDownList_EditTrackingList_SelectedIndexChanged(object sender, EventArgs e)
    {
      HiddenField HiddenField_EditCategory = (HiddenField)FormView_QMSReview_Findings_Form.FindControl("HiddenField_EditCategory");
      DropDownList DropDownList_EditLateClosingOutCNCList = (DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditLateClosingOutCNCList");
      if (HiddenField_EditCategory.Value != "CNC")
      {
        DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
        FormView_QMSReview_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
      }
      else
      {
        DropDownList DropDownList_EditTrackingList = (DropDownList)FormView_QMSReview_Findings_Form.FindControl("DropDownList_EditTrackingList");
        if (DropDownList_EditTrackingList.SelectedValue != "4316")
        {
          DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
          FormView_QMSReview_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
        }
        else
        {
          HiddenField HiddenField_EditCreatedDate = (HiddenField)FormView_QMSReview_Findings_Form.FindControl("HiddenField_EditCreatedDate");
          DateTime CreatedDate = Convert.ToDateTime(Convert.ToDateTime(HiddenField_EditCreatedDate.Value, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
          DateTime CreatedDateAdded = CreatedDate.AddMonths(1);

          if (CreatedDateAdded.CompareTo(CurrentDate) >= 0)
          {
            DropDownList_EditLateClosingOutCNCList.SelectedValue = "";
            FormView_QMSReview_Findings_Form.FindControl("LateClosingOutCNCList").Visible = false;
          }
          else
          {
            FormView_QMSReview_Findings_Form.FindControl("LateClosingOutCNCList").Visible = true;
          }
        }
      }
    }
    //---END--- --TableForm--//  
  }
}
