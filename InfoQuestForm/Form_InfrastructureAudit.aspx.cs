using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_InfrastructureAudit : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("43").Replace(" Form", "")).ToString();
          Label_ReviewHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("43").Replace(" Form", "")).ToString();


          if (Request.QueryString["InfrastructureAudit_Id"] != null)
          {
            TableReviewForm.Visible = true;

            InfrastructureAudit();

            if (TableReviewForm.Visible == true)
            {
              SetFormVisibility();
            }
          }
          else
          {
            TableReviewForm.Visible = false;
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
        if (Request.QueryString["InfrastructureAudit_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('43')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["QMSReview_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("43");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_InfrastructureAudit.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infrastructure Audit", "17");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_InfrastructureAudit_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_InfrastructureAudit_Form.SelectCommand = "SELECT * FROM Form_InfrastructureAudit WHERE (InfrastructureAudit_Id = @InfrastructureAudit_Id)";
      SqlDataSource_InfrastructureAudit_Form.UpdateCommand = "UPDATE Form_InfrastructureAudit SET [InfrastructureAudit_Completed] = @InfrastructureAudit_Completed ,[InfrastructureAudit_CompletedDate] = @InfrastructureAudit_CompletedDate ,[InfrastructureAudit_ModifiedDate] = @InfrastructureAudit_ModifiedDate ,[InfrastructureAudit_ModifiedBy] = @InfrastructureAudit_ModifiedBy ,[InfrastructureAudit_History] = @InfrastructureAudit_History ,[InfrastructureAudit_IsActive] = @InfrastructureAudit_IsActive WHERE [InfrastructureAudit_Id] = @InfrastructureAudit_Id";
      SqlDataSource_InfrastructureAudit_Form.SelectParameters.Clear();
      SqlDataSource_InfrastructureAudit_Form.SelectParameters.Add("InfrastructureAudit_Id", TypeCode.Int32, Request.QueryString["InfrastructureAudit_Id"]);
      SqlDataSource_InfrastructureAudit_Form.UpdateParameters.Clear();
      SqlDataSource_InfrastructureAudit_Form.UpdateParameters.Add("InfrastructureAudit_Completed", TypeCode.Boolean, "");
      SqlDataSource_InfrastructureAudit_Form.UpdateParameters.Add("InfrastructureAudit_CompletedDate", TypeCode.DateTime, "");
      SqlDataSource_InfrastructureAudit_Form.UpdateParameters.Add("InfrastructureAudit_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_InfrastructureAudit_Form.UpdateParameters.Add("InfrastructureAudit_ModifiedBy", TypeCode.String, "");
      SqlDataSource_InfrastructureAudit_Form.UpdateParameters.Add("InfrastructureAudit_History", TypeCode.String, "");
      SqlDataSource_InfrastructureAudit_Form.UpdateParameters.Add("InfrastructureAudit_IsActive", TypeCode.Boolean, "");
      SqlDataSource_InfrastructureAudit_Form.UpdateParameters.Add("InfrastructureAudit_Id", TypeCode.Int32, "");
    }

    private void InfrastructureAudit()
    {
      Session["InfrastructureAuditId"] = "";
      string SQLStringInfrastructureAudit = "SELECT InfrastructureAudit_Id FROM Form_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id";
      using (SqlCommand SqlCommand_InfrastructureAudit = new SqlCommand(SQLStringInfrastructureAudit))
      {
        SqlCommand_InfrastructureAudit.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);
        DataTable DataTable_InfrastructureAudit;
        using (DataTable_InfrastructureAudit = new DataTable())
        {
          DataTable_InfrastructureAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_InfrastructureAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfrastructureAudit).Copy();
          if (DataTable_InfrastructureAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfrastructureAudit.Rows)
            {
              Session["InfrastructureAuditId"] = DataRow_Row["InfrastructureAudit_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["InfrastructureAuditId"].ToString()))
      {
        TableReviewForm.Visible = false;
      }
      else
      {
        TableReviewForm.Visible = true;
      }

      Session.Remove("InfrastructureAuditId");
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('43')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '173'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '174'");
            DataRow[] SecurityFacilityAdminCompletion = DataTable_FormMode.Select("SecurityRole_Id = '175'");

            Session["InfrastructureAuditId"] = "";
            Session["InfrastructureAuditIsActive"] = "";
            string SQLStringInfrastructureAudit = "SELECT InfrastructureAudit_Id , InfrastructureAudit_IsActive FROM (SELECT InfrastructureAudit_Id , InfrastructureAudit_IsActive , RANK() OVER (ORDER BY InfrastructureAudit_Date DESC) AS InfrastructureAudit_Rank FROM Form_InfrastructureAudit WHERE Facility_Id IN (SELECT Facility_Id FROM Form_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id) AND InfrastructureAudit_IsActive = 1) AS TempTable WHERE InfrastructureAudit_Rank = 1 AND InfrastructureAudit_Id = @InfrastructureAudit_Id";
            using (SqlCommand SqlCommand_InfrastructureAudit = new SqlCommand(SQLStringInfrastructureAudit))
            {
              SqlCommand_InfrastructureAudit.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);
              DataTable DataTable_InfrastructureAudit;
              using (DataTable_InfrastructureAudit = new DataTable())
              {
                DataTable_InfrastructureAudit.Locale = CultureInfo.CurrentCulture;
                DataTable_InfrastructureAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfrastructureAudit).Copy();
                if (DataTable_InfrastructureAudit.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_InfrastructureAudit.Rows)
                  {
                    Session["InfrastructureAuditId"] = DataRow_Row["InfrastructureAudit_Id"];
                    Session["InfrastructureAuditIsActive"] = DataRow_Row["InfrastructureAudit_IsActive"];
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["InfrastructureAuditId"].ToString()))
              {
                FormView_InfrastructureAudit_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                FormView_InfrastructureAudit_Form.ChangeMode(FormViewMode.Edit);
              }
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0))
            {
              Session["Security"] = "0";
              FormView_InfrastructureAudit_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminCompletion.Length > 0)
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["InfrastructureAuditId"].ToString()))
              {
                FormView_InfrastructureAudit_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                if (Session["InfrastructureAuditIsActive"].ToString() == "True")
                {
                  FormView_InfrastructureAudit_Form.ChangeMode(FormViewMode.Edit);
                }
                else if (Session["InfrastructureAuditIsActive"].ToString() == "False")
                {
                  FormView_InfrastructureAudit_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              FormView_InfrastructureAudit_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            Session.Remove("InfrastructureAuditId");
            Session.Remove("InfrastructureAuditIsActive");
          }
        }
      }
    }

    private void RedirectToList()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_InfrastructureAuditCompleted"];

      if (SearchField1 == null && SearchField2 == null)
      {
        FinalURL = "Form_InfrastructureAudit_List.aspx";
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_InfrastructureAudit_Completed=" + Request.QueryString["Search_InfrastructureAuditCompleted"] + "&";
        }

        string SearchURL = "Form_InfrastructureAudit_List.aspx?" + SearchField1 + SearchField2;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }


    //--START-- --TableForm--//
    protected void FormView_InfrastructureAudit_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDInfrastructureAuditModifiedDate"] = e.OldValues["InfrastructureAudit_ModifiedDate"];
        object OLDInfrastructureAuditModifiedDate = Session["OLDInfrastructureAuditModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDInfrastructureAuditModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareInfrastructureAudit = (DataView)SqlDataSource_InfrastructureAudit_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareInfrastructureAudit = DataView_CompareInfrastructureAudit[0];
        Session["DBInfrastructureAuditModifiedDate"] = Convert.ToString(DataRowView_CompareInfrastructureAudit["InfrastructureAudit_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBInfrastructureAuditModifiedBy"] = Convert.ToString(DataRowView_CompareInfrastructureAudit["InfrastructureAudit_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBInfrastructureAuditModifiedDate = Session["DBInfrastructureAuditModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBInfrastructureAuditModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_InfrastructureAudit_Form.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_InfrastructureAudit_Form.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBInfrastructureAuditModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_InvalidForm = "";

          string ValidDates = "Yes";

          if (ValidDates == "Yes")
          {
            e.Cancel = false;
          }
          else
          {
            e.Cancel = true;
          }

          if (e.Cancel == true)
          {
            ((Label)FormView_InfrastructureAudit_Form.FindControl("Label_InvalidForm")).Text = Label_InvalidForm;
          }
          else if (e.Cancel == false)
          {
            e.NewValues["InfrastructureAudit_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["InfrastructureAudit_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_InfrastructureAudit", "InfrastructureAudit_Id = " + Request.QueryString["InfrastructureAudit_Id"]);

            DataView DataView_InfrastructureAudit = (DataView)SqlDataSource_InfrastructureAudit_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_InfrastructureAudit = DataView_InfrastructureAudit[0];
            Session["InfrastructureAuditHistory"] = Convert.ToString(DataRowView_InfrastructureAudit["InfrastructureAudit_History"], CultureInfo.CurrentCulture);

            Session["InfrastructureAuditHistory"] = Session["History"].ToString() + Session["InfrastructureAuditHistory"].ToString();
            e.NewValues["InfrastructureAudit_History"] = Session["InfrastructureAuditHistory"].ToString();

            Session["InfrastructureAuditHistory"] = "";
            Session["History"] = "";



            CheckBox CheckBox_EditCompleted = (CheckBox)FormView_InfrastructureAudit_Form.FindControl("CheckBox_EditCompleted");

            if (CheckBox_EditCompleted.Checked == true)
            {
              if ((bool)e.OldValues["InfrastructureAudit_Completed"] == true)
              {
                e.NewValues["InfrastructureAudit_CompletedDate"] = e.OldValues["InfrastructureAudit_CompletedDate"];
              }
              else
              {
                e.NewValues["InfrastructureAudit_CompletedDate"] = DateTime.Now.ToString();
              }
            }
            else
            {
              e.NewValues["InfrastructureAudit_CompletedDate"] = "";
            }
          }
        }

        Session.Remove("OLDInfrastructureAuditModifiedDate");
        Session.Remove("DBInfrastructureAuditModifiedDate");
        Session.Remove("DBInfrastructureAuditModifiedBy");
      }
    }

    protected void SqlDataSource_InfrastructureAudit_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Request.QueryString["InfrastructureAudit_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }


    protected void FormView_InfrastructureAudit_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["InfrastructureAudit_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }

    protected void FormView_InfrastructureAudit_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_InfrastructureAudit_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["InfrastructureAudit_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_InfrastructureAudit_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["InfrastructureAudit_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void EditDataBound()
    {
      CheckBox CheckBox_EditCompleted = (CheckBox)FormView_InfrastructureAudit_Form.FindControl("CheckBox_EditCompleted");
      Label Label_EditCompleted = (Label)FormView_InfrastructureAudit_Form.FindControl("Label_EditCompleted");

      CheckBox CheckBox_EditIsActive = (CheckBox)FormView_InfrastructureAudit_Form.FindControl("CheckBox_EditIsActive");
      Label Label_EditIsActive = (Label)FormView_InfrastructureAudit_Form.FindControl("Label_EditIsActive");

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_UserName = @SecurityUser_UserName AND Form_Id IN ('-1','43') AND (Facility_Id IN (SELECT Facility_Id FROM Form_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '122'");
            DataRow[] SecurityFacilityAdminCompletion = DataTable_FormMode.Select("SecurityRole_Id = '126'");

            Session["InfrastructureAuditId"] = "";
            Session["InfrastructureAuditIsActive"] = "";
            string SQLStringInfrastructureAudit = "SELECT InfrastructureAudit_Id , InfrastructureAudit_IsActive FROM Form_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id";
            using (SqlCommand SqlCommand_InfrastructureAudit = new SqlCommand(SQLStringInfrastructureAudit))
            {
              SqlCommand_InfrastructureAudit.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);
              DataTable DataTable_InfrastructureAudit;
              using (DataTable_InfrastructureAudit = new DataTable())
              {
                DataTable_InfrastructureAudit.Locale = CultureInfo.CurrentCulture;
                DataTable_InfrastructureAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfrastructureAudit).Copy();
                if (DataTable_InfrastructureAudit.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_InfrastructureAudit.Rows)
                  {
                    Session["InfrastructureAuditId"] = DataRow_Row["InfrastructureAudit_Id"];
                    Session["InfrastructureAuditIsActive"] = DataRow_Row["InfrastructureAudit_IsActive"];
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";

              if (Session["InfrastructureAuditIsActive"].ToString() == "True")
              {
                CheckBox_EditCompleted.Visible = true;
                Label_EditCompleted.Visible = false;
              }
              else if (Session["InfrastructureAuditIsActive"].ToString() == "False")
              {
                CheckBox_EditCompleted.Visible = false;
                Label_EditCompleted.Visible = true;
              }

              CheckBox_EditIsActive.Visible = true;
              Label_EditIsActive.Visible = false;
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminCompletion.Length > 0)
            {
              Session["Security"] = "0";

              if (Session["InfrastructureAuditIsActive"].ToString() == "True")
              {
                CheckBox_EditCompleted.Visible = true;
                Label_EditCompleted.Visible = false;
              }
              else if (Session["InfrastructureAuditIsActive"].ToString() == "False")
              {
                CheckBox_EditCompleted.Visible = false;
                Label_EditCompleted.Visible = true;
              }

              CheckBox_EditIsActive.Visible = false;
              Label_EditIsActive.Visible = true;
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";

              CheckBox_EditCompleted.Visible = false;
              Label_EditCompleted.Visible = true;

              CheckBox_EditIsActive.Visible = false;
              Label_EditIsActive.Visible = true;
            }
          }
        }
      }


      Session["FacilityFacilityDisplayName"] = "";
      Session["InfrastructureAuditDate"] = "";
      string SQLStringInfrastructureAudit1 = "SELECT Facility_FacilityDisplayName , InfrastructureAudit_Date FROM vForm_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id";
      using (SqlCommand SqlCommand_InfrastructureAudit1 = new SqlCommand(SQLStringInfrastructureAudit1))
      {
        SqlCommand_InfrastructureAudit1.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);
        DataTable DataTable_InfrastructureAudit1;
        using (DataTable_InfrastructureAudit1 = new DataTable())
        {
          DataTable_InfrastructureAudit1.Locale = CultureInfo.CurrentCulture;
          DataTable_InfrastructureAudit1 = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfrastructureAudit1).Copy();
          if (DataTable_InfrastructureAudit1.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfrastructureAudit1.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["InfrastructureAuditDate"] = DataRow_Row["InfrastructureAudit_Date"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label Label_EditFacility = (Label)FormView_InfrastructureAudit_Form.FindControl("Label_EditFacility");
        Label_EditFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_EditDate = (Label)FormView_InfrastructureAudit_Form.FindControl("Label_EditDate");
        Label_EditDate.Text = Convert.ToDateTime(Session["InfrastructureAuditDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("InfrastructureAuditDate");
    }

    protected void ReadOnlyDataBound()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["InfrastructureAuditDate"] = "";
      string SQLStringInfrastructureAudit = "SELECT Facility_FacilityDisplayName , InfrastructureAudit_Date FROM vForm_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id";
      using (SqlCommand SqlCommand_InfrastructureAudit = new SqlCommand(SQLStringInfrastructureAudit))
      {
        SqlCommand_InfrastructureAudit.Parameters.AddWithValue("@InfrastructureAudit_Id", Request.QueryString["InfrastructureAudit_Id"]);
        DataTable DataTable_InfrastructureAudit;
        using (DataTable_InfrastructureAudit = new DataTable())
        {
          DataTable_InfrastructureAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_InfrastructureAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfrastructureAudit).Copy();
          if (DataTable_InfrastructureAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfrastructureAudit.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["InfrastructureAuditDate"] = DataRow_Row["InfrastructureAudit_Date"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label Label_ItemFacility = (Label)FormView_InfrastructureAudit_Form.FindControl("Label_ItemFacility");
        Label_ItemFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_ItemDate = (Label)FormView_InfrastructureAudit_Form.FindControl("Label_ItemDate");
        Label_ItemDate.Text = Convert.ToDateTime(Session["InfrastructureAuditDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("InfrastructureAuditDate");
    }

    protected void LinkButton_EditFile_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_EditFile = (LinkButton)sender;
      ScriptManager ScriptManager_EditFile = ScriptManager.GetCurrent(Page);
      ScriptManager_EditFile.RegisterPostBackControl(LinkButton_EditFile);
    }

    protected void LinkButton_ItemFile_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_ItemFile = (LinkButton)sender;
      ScriptManager ScriptManager_ItemFile = ScriptManager.GetCurrent(Page);
      ScriptManager_ItemFile.RegisterPostBackControl(LinkButton_ItemFile);
    }
    //---END--- --TableForm--//


    //--START-- --File--//
    public static string DatabaseFileName(object infrastructureAudit_ZipFileName)
    {
      string DatabaseFileName = "";
      if (infrastructureAudit_ZipFileName != null)
      {
        DatabaseFileName = "" + infrastructureAudit_ZipFileName.ToString() + "";
      }

      return DatabaseFileName;
    }

    protected void RetrieveDatabaseFile(object sender, EventArgs e)
    {
      LinkButton LinkButton_InfrastructureAuditFile = (LinkButton)sender;
      string FileId = LinkButton_InfrastructureAuditFile.CommandArgument.ToString();

      Session["InfrastructureAuditZipFileName"] = "";
      Session["InfrastructureAuditContentType"] = "";
      Session["InfrastructureAuditData"] = "";
      string SQLStringInfrastructureAuditFile = "SELECT InfrastructureAudit_ZipFileName ,InfrastructureAudit_ContentType ,InfrastructureAudit_Data FROM Form_InfrastructureAudit WHERE InfrastructureAudit_Id = @InfrastructureAudit_Id";
      using (SqlCommand SqlCommand_InfrastructureAuditFile = new SqlCommand(SQLStringInfrastructureAuditFile))
      {
        SqlCommand_InfrastructureAuditFile.Parameters.AddWithValue("@InfrastructureAudit_Id", FileId);
        DataTable DataTable_InfrastructureAuditFile;
        using (DataTable_InfrastructureAuditFile = new DataTable())
        {
          DataTable_InfrastructureAuditFile.Locale = CultureInfo.CurrentCulture;
          DataTable_InfrastructureAuditFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfrastructureAuditFile).Copy();
          if (DataTable_InfrastructureAuditFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfrastructureAuditFile.Rows)
            {
              Session["InfrastructureAuditZipFileName"] = DataRow_Row["InfrastructureAudit_ZipFileName"];
              Session["InfrastructureAuditContentType"] = DataRow_Row["InfrastructureAudit_ContentType"];
              Session["InfrastructureAuditData"] = DataRow_Row["InfrastructureAudit_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["InfrastructureAuditData"].ToString()))
      {
        Byte[] Byte_FileData = (Byte[])Session["InfrastructureAuditData"];

        //FileStream FileStream_File = new FileStream(@"c:\test\" + Session["InfrastructureAuditZipFileName"].ToString(), FileMode.Append);
        //FileStream_File.Write(Byte_FileData, 0, Byte_FileData.Length);
        //FileStream_File.Close();

        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = Session["InfrastructureAuditContentType"].ToString();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Session["InfrastructureAuditZipFileName"].ToString() + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      Session["InfrastructureAuditZipFileName"] = "";
      Session["InfrastructureAuditContentType"] = "";
      Session["InfrastructureAuditData"] = "";
    }
    //---END--- --File--//
  }
}