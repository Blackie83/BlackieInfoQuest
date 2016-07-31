using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_OccupationalHealthAudit : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString();
          Label_ReviewHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("48").Replace(" Form", "")).ToString();


          if (Request.QueryString["OHA_Id"] != null)
          {
            TableReviewForm.Visible = true;

            OccupationalHealthAudit();

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
        if (Request.QueryString["OHA_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('48')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_OccupationalHealthAudit.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Occupational Health Audit", "25");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_OccupationalHealthAudit_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_OccupationalHealthAudit_Form.SelectCommand = "SELECT * FROM Form_OccupationalHealthAudit WHERE ([OHA_Id] = @OHA_Id)";
      SqlDataSource_OccupationalHealthAudit_Form.UpdateCommand = "UPDATE Form_OccupationalHealthAudit SET [OHA_Completed] = @OHA_Completed ,[OHA_CompletedDate] = @OHA_CompletedDate ,[OHA_ModifiedDate] = @OHA_ModifiedDate ,[OHA_ModifiedBy] = @OHA_ModifiedBy ,[OHA_History] = @OHA_History ,[OHA_IsActive] = @OHA_IsActive WHERE [OHA_Id] = @OHA_Id";
      SqlDataSource_OccupationalHealthAudit_Form.SelectParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Form.SelectParameters.Add("OHA_Id", TypeCode.Int32, Request.QueryString["OHA_Id"]);
      SqlDataSource_OccupationalHealthAudit_Form.UpdateParameters.Clear();
      SqlDataSource_OccupationalHealthAudit_Form.UpdateParameters.Add("OHA_Completed", TypeCode.Boolean, "");
      SqlDataSource_OccupationalHealthAudit_Form.UpdateParameters.Add("OHA_CompletedDate", TypeCode.DateTime, "");
      SqlDataSource_OccupationalHealthAudit_Form.UpdateParameters.Add("OHA_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_OccupationalHealthAudit_Form.UpdateParameters.Add("OHA_ModifiedBy", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Form.UpdateParameters.Add("OHA_History", TypeCode.String, "");
      SqlDataSource_OccupationalHealthAudit_Form.UpdateParameters.Add("OHA_IsActive", TypeCode.Boolean, "");
      SqlDataSource_OccupationalHealthAudit_Form.UpdateParameters.Add("OHA_Id", TypeCode.Int32, "");
    }

    private void OccupationalHealthAudit()
    {
      Session["OHAId"] = "";
      string SQLStringOccupationalHealthAudit = "SELECT OHA_Id FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id";
      using (SqlCommand SqlCommand_OccupationalHealthAudit = new SqlCommand(SQLStringOccupationalHealthAudit))
      {
        SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
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
        TableReviewForm.Visible = false;
      }
      else
      {
        TableReviewForm.Visible = true;
      }

      Session.Remove("OHAId");
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('48')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
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
            Session["OHAIsActive"] = "";
            string SQLStringOccupationalHealthAudit = @"SELECT OHA_Id , OHA_IsActive 
                                                        FROM (
	                                                        SELECT OHA_Id , OHA_IsActive , RANK() OVER (ORDER BY OHA_CreatedDate DESC) AS OHA_Rank 
	                                                        FROM Form_OccupationalHealthAudit 
	                                                        WHERE Facility_Id IN (
		                                                        SELECT Facility_Id 
		                                                        FROM Form_OccupationalHealthAudit 
		                                                        WHERE OHA_Id = @OHA_Id
	                                                        ) AND Unit_Id IN ( 
		                                                        SELECT Unit_Id 
		                                                        FROM Form_OccupationalHealthAudit 
		                                                        WHERE OHA_Id = @OHA_Id
	                                                        )
                                                        ) AS TempTable 
                                                        WHERE OHA_Rank = 1 AND OHA_Id = @OHA_Id";
            using (SqlCommand SqlCommand_OccupationalHealthAudit = new SqlCommand(SQLStringOccupationalHealthAudit))
            {
              SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
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
                    Session["OHAIsActive"] = DataRow_Row["OHA_IsActive"];
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
                FormView_OccupationalHealthAudit_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                FormView_OccupationalHealthAudit_Form.ChangeMode(FormViewMode.Edit);
              }
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Session["Security"] = "0";
              FormView_OccupationalHealthAudit_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminCompletion.Length > 0)
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["OHAId"].ToString()))
              {
                FormView_OccupationalHealthAudit_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                if (Session["OHAIsActive"].ToString() == "True")
                {
                  FormView_OccupationalHealthAudit_Form.ChangeMode(FormViewMode.Edit);
                }
                else if (Session["OHAIsActive"].ToString() == "False")
                {
                  FormView_OccupationalHealthAudit_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              FormView_OccupationalHealthAudit_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            Session.Remove("OHAId");
            Session.Remove("OHAIsActive");
          }
        }
      }
    }

    private void RedirectToList()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_UnitId"];
      string SearchField3 = Request.QueryString["Search_OHACompleted"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null)
      {
        FinalURL = "Form_OccupationalHealthAudit_List.aspx";
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
          SearchField2 = "s_Unit_Id=" + Request.QueryString["Search_UnitId"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_OHA_Completed=" + Request.QueryString["Search_OHACompleted"] + "&";
        }

        string SearchURL = "Form_OccupationalHealthAudit_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }


    //--START-- --TableForm--//
    protected void FormView_OccupationalHealthAudit_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDOHAModifiedDate"] = e.OldValues["OHA_ModifiedDate"];
        object OLDOHAModifiedDate = Session["OLDOHAModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDOHAModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareOccupationalHealthAudit = (DataView)SqlDataSource_OccupationalHealthAudit_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareOccupationalHealthAudit = DataView_CompareOccupationalHealthAudit[0];
        Session["DBOHAModifiedDate"] = Convert.ToString(DataRowView_CompareOccupationalHealthAudit["OHA_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBOHAModifiedBy"] = Convert.ToString(DataRowView_CompareOccupationalHealthAudit["OHA_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBOHAModifiedDate = Session["DBOHAModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBOHAModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBOHAModifiedBy"].ToString() + "<br/>" +
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
            ((Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_InvalidForm")).Text = Label_InvalidForm;
          }
          else if (e.Cancel == false)
          {
            e.NewValues["OHA_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["OHA_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_OccupationalHealthAudit", "OHA_Id = " + Request.QueryString["OHA_Id"]);

            DataView DataView_OccupationalHealthAudit = (DataView)SqlDataSource_OccupationalHealthAudit_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_OccupationalHealthAudit = DataView_OccupationalHealthAudit[0];
            Session["OHAHistory"] = Convert.ToString(DataRowView_OccupationalHealthAudit["OHA_History"], CultureInfo.CurrentCulture);

            Session["OHAHistory"] = Session["History"].ToString() + Session["OHAHistory"].ToString();
            e.NewValues["OHA_History"] = Session["OHAHistory"].ToString();

            Session["OHAHistory"] = "";
            Session["History"] = "";



            CheckBox CheckBox_EditCompleted = (CheckBox)FormView_OccupationalHealthAudit_Form.FindControl("CheckBox_EditCompleted");

            if (CheckBox_EditCompleted.Checked == true)
            {
              if ((bool)e.OldValues["OHA_Completed"] == true)
              {
                e.NewValues["OHA_CompletedDate"] = e.OldValues["OHA_CompletedDate"];
              }
              else
              {
                e.NewValues["OHA_CompletedDate"] = DateTime.Now.ToString();
              }
            }
            else
            {
              e.NewValues["OHA_CompletedDate"] = "";
            }
          }
        }

        Session.Remove("OLDOHAModifiedDate");
        Session.Remove("DBOHAModifiedDate");
        Session.Remove("DBOHAModifiedBy");
      }
    }

    protected void SqlDataSource_OccupationalHealthAudit_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Request.QueryString["OHA_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }


    protected void FormView_OccupationalHealthAudit_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["OHA_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }

    protected void FormView_OccupationalHealthAudit_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_OccupationalHealthAudit_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["OHA_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_OccupationalHealthAudit_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["OHA_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void EditDataBound()
    {
      CheckBox CheckBox_EditCompleted = (CheckBox)FormView_OccupationalHealthAudit_Form.FindControl("CheckBox_EditCompleted");
      Label Label_EditCompleted = (Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_EditCompleted");

      CheckBox CheckBox_EditIsActive = (CheckBox)FormView_OccupationalHealthAudit_Form.FindControl("CheckBox_EditIsActive");
      Label Label_EditIsActive = (Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_EditIsActive");

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_UserName = @SecurityUser_UserName AND Form_Id IN ('-1','48') AND (Facility_Id IN (SELECT Facility_Id FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '195'");
            DataRow[] SecurityFacilityAdminCompletion = DataTable_FormMode.Select("SecurityRole_Id = '197'");

            Session["OHAId"] = "";
            Session["OHAIsActive"] = "";
            string SQLStringOccupationalHealthAudit = "SELECT OHA_Id , OHA_IsActive FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id";
            using (SqlCommand SqlCommand_OccupationalHealthAudit = new SqlCommand(SQLStringOccupationalHealthAudit))
            {
              SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
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
                    Session["OHAIsActive"] = DataRow_Row["OHA_IsActive"];
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";

              if (Session["OHAIsActive"].ToString() == "True")
              {
                CheckBox_EditCompleted.Visible = true;
                Label_EditCompleted.Visible = false;
              }
              else if (Session["OHAIsActive"].ToString() == "False")
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

              if (Session["OHAIsActive"].ToString() == "True")
              {
                CheckBox_EditCompleted.Visible = true;
                Label_EditCompleted.Visible = false;
              }
              else if (Session["OHAIsActive"].ToString() == "False")
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
      Session["UnitName"] = "";
      Session["OHADate"] = "";
      string SQLStringOccupationalHealthAudit1 = "SELECT Facility_FacilityDisplayName , Unit_Name , OHA_Date FROM vForm_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id";
      using (SqlCommand SqlCommand_OccupationalHealthAudit1 = new SqlCommand(SQLStringOccupationalHealthAudit1))
      {
        SqlCommand_OccupationalHealthAudit1.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
        DataTable DataTable_OccupationalHealthAudit1;
        using (DataTable_OccupationalHealthAudit1 = new DataTable())
        {
          DataTable_OccupationalHealthAudit1.Locale = CultureInfo.CurrentCulture;
          DataTable_OccupationalHealthAudit1 = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OccupationalHealthAudit1).Copy();
          if (DataTable_OccupationalHealthAudit1.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OccupationalHealthAudit1.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["UnitName"] = DataRow_Row["Unit_Name"];
              Session["OHADate"] = DataRow_Row["OHA_Date"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label Label_EditFacility = (Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_EditFacility");
        Label_EditFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_EditUnit = (Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_EditUnit");
        Label_EditUnit.Text = Session["UnitName"].ToString();

        Label Label_EditDate = (Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_EditDate");
        Label_EditDate.Text = Convert.ToDateTime(Session["OHADate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("UnitName");
      Session.Remove("OHADate");
    }

    protected void ReadOnlyDataBound()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["UnitName"] = "";
      Session["OHADate"] = "";
      string SQLStringOccupationalHealthAudit = "SELECT Facility_FacilityDisplayName , Unit_Name , OHA_Date FROM vForm_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id";
      using (SqlCommand SqlCommand_OccupationalHealthAudit = new SqlCommand(SQLStringOccupationalHealthAudit))
      {
        SqlCommand_OccupationalHealthAudit.Parameters.AddWithValue("@OHA_Id", Request.QueryString["OHA_Id"]);
        DataTable DataTable_OccupationalHealthAudit;
        using (DataTable_OccupationalHealthAudit = new DataTable())
        {
          DataTable_OccupationalHealthAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_OccupationalHealthAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OccupationalHealthAudit).Copy();
          if (DataTable_OccupationalHealthAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OccupationalHealthAudit.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["UnitName"] = DataRow_Row["Unit_Name"];
              Session["OHADate"] = DataRow_Row["OHA_Date"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label Label_ItemFacility = (Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_ItemFacility");
        Label_ItemFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_ItemUnit = (Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_ItemUnit");
        Label_ItemUnit.Text = Session["UnitName"].ToString();

        Label Label_ItemDate = (Label)FormView_OccupationalHealthAudit_Form.FindControl("Label_ItemDate");
        Label_ItemDate.Text = Convert.ToDateTime(Session["OHADate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("UnitName");
      Session.Remove("OHADate");
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
    public static string DatabaseFileName(object oha_ZipFileName)
    {
      string DatabaseFileName = "";
      if (oha_ZipFileName != null)
      {
        DatabaseFileName = "" + oha_ZipFileName.ToString() + "";
      }

      return DatabaseFileName;
    }

    protected void RetrieveDatabaseFile(object sender, EventArgs e)
    {
      LinkButton LinkButton_OccupationalHealthAuditFile = (LinkButton)sender;
      string FileId = LinkButton_OccupationalHealthAuditFile.CommandArgument.ToString();

      Session["OHAZipFileName"] = "";
      Session["OHAContentType"] = "";
      Session["OHAData"] = "";
      string SQLStringOccupationalHealthAuditFile = "SELECT OHA_ZipFileName ,OHA_ContentType ,OHA_Data FROM Form_OccupationalHealthAudit WHERE OHA_Id = @OHA_Id";
      using (SqlCommand SqlCommand_OccupationalHealthAuditFile = new SqlCommand(SQLStringOccupationalHealthAuditFile))
      {
        SqlCommand_OccupationalHealthAuditFile.Parameters.AddWithValue("@OHA_Id", FileId);
        DataTable DataTable_OccupationalHealthAuditFile;
        using (DataTable_OccupationalHealthAuditFile = new DataTable())
        {
          DataTable_OccupationalHealthAuditFile.Locale = CultureInfo.CurrentCulture;
          DataTable_OccupationalHealthAuditFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OccupationalHealthAuditFile).Copy();
          if (DataTable_OccupationalHealthAuditFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OccupationalHealthAuditFile.Rows)
            {
              Session["OHAZipFileName"] = DataRow_Row["OHA_ZipFileName"];
              Session["OHAContentType"] = DataRow_Row["OHA_ContentType"];
              Session["OHAData"] = DataRow_Row["OHA_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["OHAData"].ToString()))
      {
        Byte[] Byte_FileData = (Byte[])Session["OHAData"];
        //FileStream FileStream_File = new FileStream(Server.MapPath("App_Files/Form_CRM_DiscoveryComment_Upload/") + Session["CRMFileName"].ToString(), FileMode.Append);
        //FileStream_File.Write(Byte_FileData, 0, Byte_FileData.Length);
        //FileStream_File.Close();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = Session["OHAContentType"].ToString();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Session["OHAZipFileName"].ToString() + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      Session["OHAZipFileName"] = "";
      Session["OHAContentType"] = "";
      Session["OHAData"] = "";
    }
    //---END--- --File--//
  }
}