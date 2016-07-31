using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_CollegeLearningAudit : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("49").Replace(" Form", "")).ToString();
          Label_ReviewHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("49").Replace(" Form", "")).ToString();


          if (Request.QueryString["CLA_Id"] != null)
          {
            TableReviewForm.Visible = true;

            CollegeLearningAudit();

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
        if (Request.QueryString["CLA_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('49')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("49");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_CollegeLearningAudit.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("College Learning Audit", "24");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_CollegeLearningAudit_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_CollegeLearningAudit_Form.SelectCommand = "SELECT * FROM Form_CollegeLearningAudit WHERE ([CLA_Id] = @CLA_Id)";
      SqlDataSource_CollegeLearningAudit_Form.UpdateCommand = "UPDATE Form_CollegeLearningAudit SET [CLA_Completed] = @CLA_Completed ,[CLA_CompletedDate] = @CLA_CompletedDate ,[CLA_ModifiedDate] = @CLA_ModifiedDate ,[CLA_ModifiedBy] = @CLA_ModifiedBy ,[CLA_History] = @CLA_History ,[CLA_IsActive] = @CLA_IsActive WHERE [CLA_Id] = @CLA_Id";
      SqlDataSource_CollegeLearningAudit_Form.SelectParameters.Clear();
      SqlDataSource_CollegeLearningAudit_Form.SelectParameters.Add("CLA_Id", TypeCode.Int32, Request.QueryString["CLA_Id"]);
      SqlDataSource_CollegeLearningAudit_Form.UpdateParameters.Clear();
      SqlDataSource_CollegeLearningAudit_Form.UpdateParameters.Add("CLA_Completed", TypeCode.Boolean, "");
      SqlDataSource_CollegeLearningAudit_Form.UpdateParameters.Add("CLA_CompletedDate", TypeCode.DateTime, "");
      SqlDataSource_CollegeLearningAudit_Form.UpdateParameters.Add("CLA_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_CollegeLearningAudit_Form.UpdateParameters.Add("CLA_ModifiedBy", TypeCode.String, "");
      SqlDataSource_CollegeLearningAudit_Form.UpdateParameters.Add("CLA_History", TypeCode.String, "");
      SqlDataSource_CollegeLearningAudit_Form.UpdateParameters.Add("CLA_IsActive", TypeCode.Boolean, "");
      SqlDataSource_CollegeLearningAudit_Form.UpdateParameters.Add("CLA_Id", TypeCode.Int32, "");
    }

    private void CollegeLearningAudit()
    {
      Session["CLAId"] = "";
      string SQLStringCollegeLearningAudit = "SELECT CLA_Id FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id";
      using (SqlCommand SqlCommand_CollegeLearningAudit = new SqlCommand(SQLStringCollegeLearningAudit))
      {
        SqlCommand_CollegeLearningAudit.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
        DataTable DataTable_CollegeLearningAudit;
        using (DataTable_CollegeLearningAudit = new DataTable())
        {
          DataTable_CollegeLearningAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_CollegeLearningAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CollegeLearningAudit).Copy();
          if (DataTable_CollegeLearningAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CollegeLearningAudit.Rows)
            {
              Session["CLAId"] = DataRow_Row["CLA_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["CLAId"].ToString()))
      {
        TableReviewForm.Visible = false;
      }
      else
      {
        TableReviewForm.Visible = true;
      }

      Session.Remove("CLAId");
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('49')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '190'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '191'");
            DataRow[] SecurityFacilityAdminCompletion = DataTable_FormMode.Select("SecurityRole_Id = '192'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '193'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '194'");

            Session["CLAId"] = "";
            Session["CLAIsActive"] = "";
            string SQLStringCollegeLearningAudit = "SELECT CLA_Id , CLA_IsActive FROM (SELECT CLA_Id , CLA_IsActive , RANK() OVER (ORDER BY CLA_CreatedDate DESC) AS CLA_Rank FROM Form_CollegeLearningAudit WHERE Facility_Id IN (SELECT Facility_Id FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id) ) AS TempTable WHERE CLA_Rank = 1 AND CLA_Id = @CLA_Id";
            using (SqlCommand SqlCommand_CollegeLearningAudit = new SqlCommand(SQLStringCollegeLearningAudit))
            {
              SqlCommand_CollegeLearningAudit.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
              DataTable DataTable_CollegeLearningAudit;
              using (DataTable_CollegeLearningAudit = new DataTable())
              {
                DataTable_CollegeLearningAudit.Locale = CultureInfo.CurrentCulture;
                DataTable_CollegeLearningAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CollegeLearningAudit).Copy();
                if (DataTable_CollegeLearningAudit.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_CollegeLearningAudit.Rows)
                  {
                    Session["CLAId"] = DataRow_Row["CLA_Id"];
                    Session["CLAIsActive"] = DataRow_Row["CLA_IsActive"];
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["CLAId"].ToString()))
              {
                FormView_CollegeLearningAudit_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                FormView_CollegeLearningAudit_Form.ChangeMode(FormViewMode.Edit);
              }
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Session["Security"] = "0";
              FormView_CollegeLearningAudit_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminCompletion.Length > 0)
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["CLAId"].ToString()))
              {
                FormView_CollegeLearningAudit_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                if (Session["CLAIsActive"].ToString() == "True")
                {
                  FormView_CollegeLearningAudit_Form.ChangeMode(FormViewMode.Edit);
                }
                else if (Session["CLAIsActive"].ToString() == "False")
                {
                  FormView_CollegeLearningAudit_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              FormView_CollegeLearningAudit_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            Session.Remove("CLAId");
            Session.Remove("CLAIsActive");
          }
        }
      }
    }

    private void RedirectToList()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_CLACompleted"];

      if (SearchField1 == null && SearchField2 == null)
      {
        FinalURL = "Form_CollegeLearningAudit_List.aspx";
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
          SearchField2 = "s_CLA_Completed=" + Request.QueryString["Search_CLACompleted"] + "&";
        }

        string SearchURL = "Form_CollegeLearningAudit_List.aspx?" + SearchField1 + SearchField2;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }


    //--START-- --TableForm--//
    protected void FormView_CollegeLearningAudit_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDCLAModifiedDate"] = e.OldValues["CLA_ModifiedDate"];
        object OLDCLAModifiedDate = Session["OLDCLAModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDCLAModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareCollegeLearningAudit = (DataView)SqlDataSource_CollegeLearningAudit_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareCollegeLearningAudit = DataView_CompareCollegeLearningAudit[0];
        Session["DBCLAModifiedDate"] = Convert.ToString(DataRowView_CompareCollegeLearningAudit["CLA_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBCLAModifiedBy"] = Convert.ToString(DataRowView_CompareCollegeLearningAudit["CLA_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBCLAModifiedDate = Session["DBCLAModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBCLAModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_CollegeLearningAudit_Form.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_CollegeLearningAudit_Form.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBCLAModifiedBy"].ToString() + "<br/>" +
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
            ((Label)FormView_CollegeLearningAudit_Form.FindControl("Label_InvalidForm")).Text = Label_InvalidForm;
          }
          else if (e.Cancel == false)
          {
            e.NewValues["CLA_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["CLA_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_CollegeLearningAudit", "CLA_Id = " + Request.QueryString["CLA_Id"]);

            DataView DataView_CollegeLearningAudit = (DataView)SqlDataSource_CollegeLearningAudit_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_CollegeLearningAudit = DataView_CollegeLearningAudit[0];
            Session["CLAHistory"] = Convert.ToString(DataRowView_CollegeLearningAudit["CLA_History"], CultureInfo.CurrentCulture);

            Session["CLAHistory"] = Session["History"].ToString() + Session["CLAHistory"].ToString();
            e.NewValues["CLA_History"] = Session["CLAHistory"].ToString();

            Session["CLAHistory"] = "";
            Session["History"] = "";



            CheckBox CheckBox_EditCompleted = (CheckBox)FormView_CollegeLearningAudit_Form.FindControl("CheckBox_EditCompleted");

            if (CheckBox_EditCompleted.Checked == true)
            {
              if ((bool)e.OldValues["CLA_Completed"] == true)
              {
                e.NewValues["CLA_CompletedDate"] = e.OldValues["CLA_CompletedDate"];
              }
              else
              {
                e.NewValues["CLA_CompletedDate"] = DateTime.Now.ToString();
              }
            }
            else
            {
              e.NewValues["CLA_CompletedDate"] = "";
            }
          }
        }

        Session.Remove("OLDCLAModifiedDate");
        Session.Remove("DBCLAModifiedDate");
        Session.Remove("DBCLAModifiedBy");
      }
    }

    protected void SqlDataSource_CollegeLearningAudit_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Request.QueryString["CLA_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }


    protected void FormView_CollegeLearningAudit_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["CLA_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }

    protected void FormView_CollegeLearningAudit_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_CollegeLearningAudit_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["CLA_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_CollegeLearningAudit_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["CLA_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void EditDataBound()
    {
      CheckBox CheckBox_EditCompleted = (CheckBox)FormView_CollegeLearningAudit_Form.FindControl("CheckBox_EditCompleted");
      Label Label_EditCompleted = (Label)FormView_CollegeLearningAudit_Form.FindControl("Label_EditCompleted");

      CheckBox CheckBox_EditIsActive = (CheckBox)FormView_CollegeLearningAudit_Form.FindControl("CheckBox_EditIsActive");
      Label Label_EditIsActive = (Label)FormView_CollegeLearningAudit_Form.FindControl("Label_EditIsActive");

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_UserName = @SecurityUser_UserName AND Form_Id IN ('-1','49') AND (Facility_Id IN (SELECT Facility_Id FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '190'");
            DataRow[] SecurityFacilityAdminCompletion = DataTable_FormMode.Select("SecurityRole_Id = '192'");

            Session["CLAId"] = "";
            Session["CLAIsActive"] = "";
            string SQLStringCollegeLearningAudit = "SELECT CLA_Id , CLA_IsActive FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id";
            using (SqlCommand SqlCommand_CollegeLearningAudit = new SqlCommand(SQLStringCollegeLearningAudit))
            {
              SqlCommand_CollegeLearningAudit.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
              DataTable DataTable_CollegeLearningAudit;
              using (DataTable_CollegeLearningAudit = new DataTable())
              {
                DataTable_CollegeLearningAudit.Locale = CultureInfo.CurrentCulture;
                DataTable_CollegeLearningAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CollegeLearningAudit).Copy();
                if (DataTable_CollegeLearningAudit.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_CollegeLearningAudit.Rows)
                  {
                    Session["CLAId"] = DataRow_Row["CLA_Id"];
                    Session["CLAIsActive"] = DataRow_Row["CLA_IsActive"];
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";

              if (Session["CLAIsActive"].ToString() == "True")
              {
                CheckBox_EditCompleted.Visible = true;
                Label_EditCompleted.Visible = false;
              }
              else if (Session["CLAIsActive"].ToString() == "False")
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

              if (Session["CLAIsActive"].ToString() == "True")
              {
                CheckBox_EditCompleted.Visible = true;
                Label_EditCompleted.Visible = false;
              }
              else if (Session["CLAIsActive"].ToString() == "False")
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
      Session["CLADate"] = "";
      string SQLStringCollegeLearningAudit1 = "SELECT Facility_FacilityDisplayName , CLA_Date FROM vForm_CollegeLearningAudit WHERE CLA_Id = @CLA_Id";
      using (SqlCommand SqlCommand_CollegeLearningAudit1 = new SqlCommand(SQLStringCollegeLearningAudit1))
      {
        SqlCommand_CollegeLearningAudit1.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
        DataTable DataTable_CollegeLearningAudit1;
        using (DataTable_CollegeLearningAudit1 = new DataTable())
        {
          DataTable_CollegeLearningAudit1.Locale = CultureInfo.CurrentCulture;
          DataTable_CollegeLearningAudit1 = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CollegeLearningAudit1).Copy();
          if (DataTable_CollegeLearningAudit1.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CollegeLearningAudit1.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["CLADate"] = DataRow_Row["CLA_Date"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label Label_EditFacility = (Label)FormView_CollegeLearningAudit_Form.FindControl("Label_EditFacility");
        Label_EditFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_EditDate = (Label)FormView_CollegeLearningAudit_Form.FindControl("Label_EditDate");
        Label_EditDate.Text = Convert.ToDateTime(Session["CLADate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("CLADate");
    }

    protected void ReadOnlyDataBound()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["CLADate"] = "";
      string SQLStringCollegeLearningAudit = "SELECT Facility_FacilityDisplayName , CLA_Date FROM vForm_CollegeLearningAudit WHERE CLA_Id = @CLA_Id";
      using (SqlCommand SqlCommand_CollegeLearningAudit = new SqlCommand(SQLStringCollegeLearningAudit))
      {
        SqlCommand_CollegeLearningAudit.Parameters.AddWithValue("@CLA_Id", Request.QueryString["CLA_Id"]);
        DataTable DataTable_CollegeLearningAudit;
        using (DataTable_CollegeLearningAudit = new DataTable())
        {
          DataTable_CollegeLearningAudit.Locale = CultureInfo.CurrentCulture;
          DataTable_CollegeLearningAudit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CollegeLearningAudit).Copy();
          if (DataTable_CollegeLearningAudit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CollegeLearningAudit.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["CLADate"] = DataRow_Row["CLA_Date"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label Label_ItemFacility = (Label)FormView_CollegeLearningAudit_Form.FindControl("Label_ItemFacility");
        Label_ItemFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_ItemDate = (Label)FormView_CollegeLearningAudit_Form.FindControl("Label_ItemDate");
        Label_ItemDate.Text = Convert.ToDateTime(Session["CLADate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("CLADate");
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
    public static string DatabaseFileName(object cla_ZipFileName)
    {
      string DatabaseFileName = "";
      if (cla_ZipFileName != null)
      {
        DatabaseFileName = "" + cla_ZipFileName.ToString() + "";
      }

      return DatabaseFileName;
    }

    protected void RetrieveDatabaseFile(object sender, EventArgs e)
    {
      LinkButton LinkButton_CollegeLearningAuditFile = (LinkButton)sender;
      string FileId = LinkButton_CollegeLearningAuditFile.CommandArgument.ToString();

      Session["CLAZipFileName"] = "";
      Session["CLAContentType"] = "";
      Session["CLAData"] = "";
      string SQLStringCollegeLearningAuditFile = "SELECT CLA_ZipFileName ,CLA_ContentType ,CLA_Data FROM Form_CollegeLearningAudit WHERE CLA_Id = @CLA_Id";
      using (SqlCommand SqlCommand_CollegeLearningAuditFile = new SqlCommand(SQLStringCollegeLearningAuditFile))
      {
        SqlCommand_CollegeLearningAuditFile.Parameters.AddWithValue("@CLA_Id", FileId);
        DataTable DataTable_CollegeLearningAuditFile;
        using (DataTable_CollegeLearningAuditFile = new DataTable())
        {
          DataTable_CollegeLearningAuditFile.Locale = CultureInfo.CurrentCulture;
          DataTable_CollegeLearningAuditFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CollegeLearningAuditFile).Copy();
          if (DataTable_CollegeLearningAuditFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CollegeLearningAuditFile.Rows)
            {
              Session["CLAZipFileName"] = DataRow_Row["CLA_ZipFileName"];
              Session["CLAContentType"] = DataRow_Row["CLA_ContentType"];
              Session["CLAData"] = DataRow_Row["CLA_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["CLAData"].ToString()))
      {
        Byte[] Byte_FileData = (Byte[])Session["CLAData"];
        //FileStream FileStream_File = new FileStream(Server.MapPath("App_Files/Form_CRM_DiscoveryComment_Upload/") + Session["CRMFileName"].ToString(), FileMode.Append);
        //FileStream_File.Write(Byte_FileData, 0, Byte_FileData.Length);
        //FileStream_File.Close();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = Session["CLAContentType"].ToString();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Session["CLAZipFileName"].ToString() + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      Session["CLAZipFileName"] = "";
      Session["CLAContentType"] = "";
      Session["CLAData"] = "";
    }
    //---END--- --File--//
  }
}