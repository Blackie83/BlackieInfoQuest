using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_QMSReview : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString();
          Label_ReviewHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("31").Replace(" Form", "")).ToString();


          if (Request.QueryString["QMSReview_Id"] != null)
          {
            TableReviewForm.Visible = true;

            QMSReview();

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
        if (Request.QueryString["QMSReview_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('31')) AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);

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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_QMSReview.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Internal Quality Audit", "12");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_QMSReview_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_QMSReview_Form.SelectCommand="SELECT * FROM [InfoQuest_Form_QMSReview] WHERE ([QMSReview_Id] = @QMSReview_Id)";
      SqlDataSource_QMSReview_Form.UpdateCommand="UPDATE [InfoQuest_Form_QMSReview] SET [QMSReview_Completed] = @QMSReview_Completed ,[QMSReview_CompletedDate] = @QMSReview_CompletedDate ,[QMSReview_ModifiedDate] = @QMSReview_ModifiedDate ,[QMSReview_ModifiedBy] = @QMSReview_ModifiedBy ,[QMSReview_History] = @QMSReview_History ,[QMSReview_IsActive] = @QMSReview_IsActive WHERE [QMSReview_Id] = @QMSReview_Id";
      SqlDataSource_QMSReview_Form.SelectParameters.Clear();
      SqlDataSource_QMSReview_Form.SelectParameters.Add("QMSReview_Id", TypeCode.Int32, Request.QueryString["QMSReview_Id"]);
      SqlDataSource_QMSReview_Form.UpdateParameters.Clear();
      SqlDataSource_QMSReview_Form.UpdateParameters.Add("QMSReview_Completed", TypeCode.Boolean, "");
      SqlDataSource_QMSReview_Form.UpdateParameters.Add("QMSReview_CompletedDate", TypeCode.DateTime, "");
      SqlDataSource_QMSReview_Form.UpdateParameters.Add("QMSReview_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_QMSReview_Form.UpdateParameters.Add("QMSReview_ModifiedBy", TypeCode.String, "");
      SqlDataSource_QMSReview_Form.UpdateParameters.Add("QMSReview_History", TypeCode.String, "");
      SqlDataSource_QMSReview_Form.UpdateParameters.Add("QMSReview_IsActive", TypeCode.Boolean, "");
      SqlDataSource_QMSReview_Form.UpdateParameters.Add("QMSReview_Id", TypeCode.Int32, "");
    }

    private void QMSReview()
    {
      Session["QMSReviewId"] = "";
      string SQLStringQMSReview = "SELECT QMSReview_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
      using (SqlCommand SqlCommand_QMSReview = new SqlCommand(SQLStringQMSReview))
      {
        SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
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
        TableReviewForm.Visible = false;
      }
      else
      {
        TableReviewForm.Visible = true;
      }

      Session.Remove("QMSReviewId");
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('31')) AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
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
            Session["QMSReviewIsActive"] = "";
            string SQLStringQMSReview = "SELECT QMSReview_Id , QMSReview_IsActive FROM (SELECT QMSReview_Id , QMSReview_IsActive , RANK() OVER (ORDER BY QMSReview_CreatedDate DESC) AS QMSReview_Rank FROM InfoQuest_Form_QMSReview WHERE Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id) ) AS TempTable WHERE QMSReview_Rank = 1 AND QMSReview_Id = @QMSReview_Id";
            using (SqlCommand SqlCommand_QMSReview = new SqlCommand(SQLStringQMSReview))
            {
              SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
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
                    Session["QMSReviewIsActive"] = DataRow_Row["QMSReview_IsActive"];
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
                FormView_QMSReview_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                FormView_QMSReview_Form.ChangeMode(FormViewMode.Edit);
              }
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Session["Security"] = "0";
              FormView_QMSReview_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminCompletion.Length > 0)
            {
              Session["Security"] = "0";

              if (string.IsNullOrEmpty(Session["QMSReviewId"].ToString()))
              {
                FormView_QMSReview_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                if (Session["QMSReviewIsActive"].ToString() == "True")
                {
                  FormView_QMSReview_Form.ChangeMode(FormViewMode.Edit);
                }
                else if (Session["QMSReviewIsActive"].ToString() == "False")
                {
                  FormView_QMSReview_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              FormView_QMSReview_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            Session.Remove("QMSReviewId");
            Session.Remove("QMSReviewIsActive");
          }
        }
      }
    }

    private void RedirectToList()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_QMSReviewCompleted"];

      if (SearchField1 == null && SearchField2 == null)
      {
        FinalURL = "Form_QMSReview_List.aspx";
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
          SearchField2 = "s_QMSReview_Completed=" + Request.QueryString["Search_QMSReviewCompleted"] + "&";
        }

        string SearchURL = "Form_QMSReview_List.aspx?" + SearchField1 + SearchField2;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }


    //--START-- --TableForm--//
    protected void FormView_QMSReview_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDQMSReviewModifiedDate"] = e.OldValues["QMSReview_ModifiedDate"];
        object OLDQMSReviewModifiedDate = Session["OLDQMSReviewModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDQMSReviewModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareQMSReview = (DataView)SqlDataSource_QMSReview_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareQMSReview = DataView_CompareQMSReview[0];
        Session["DBQMSReviewModifiedDate"] = Convert.ToString(DataRowView_CompareQMSReview["QMSReview_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBQMSReviewModifiedBy"] = Convert.ToString(DataRowView_CompareQMSReview["QMSReview_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBQMSReviewModifiedDate = Session["DBQMSReviewModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBQMSReviewModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_QMSReview_Form.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_QMSReview_Form.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBQMSReviewModifiedBy"].ToString() + "<br/>" +
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
            ((Label)FormView_QMSReview_Form.FindControl("Label_InvalidForm")).Text = Label_InvalidForm;
          }
          else if (e.Cancel == false)
          {
            e.NewValues["QMSReview_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["QMSReview_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_QMSReview", "QMSReview_Id = " + Request.QueryString["QMSReview_Id"]);

            DataView DataView_QMSReview = (DataView)SqlDataSource_QMSReview_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_QMSReview = DataView_QMSReview[0];
            Session["QMSReviewHistory"] = Convert.ToString(DataRowView_QMSReview["QMSReview_History"], CultureInfo.CurrentCulture);

            Session["QMSReviewHistory"] = Session["History"].ToString() + Session["QMSReviewHistory"].ToString();
            e.NewValues["QMSReview_History"] = Session["QMSReviewHistory"].ToString();

            Session["QMSReviewHistory"] = "";
            Session["History"] = "";



            CheckBox CheckBox_EditCompleted = (CheckBox)FormView_QMSReview_Form.FindControl("CheckBox_EditCompleted");

            if (CheckBox_EditCompleted.Checked == true)
            {
              if ((bool)e.OldValues["QMSReview_Completed"] == true)
              {
                e.NewValues["QMSReview_CompletedDate"] = e.OldValues["QMSReview_CompletedDate"];
              }
              else
              {
                e.NewValues["QMSReview_CompletedDate"] = DateTime.Now.ToString();
              }
            }
            else
            {
              e.NewValues["QMSReview_CompletedDate"] = "";
            }
          }
        }

        Session.Remove("OLDQMSReviewModifiedDate");
        Session.Remove("DBQMSReviewModifiedDate");
        Session.Remove("DBQMSReviewModifiedBy");
      }
    }
    
    protected void SqlDataSource_QMSReview_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Request.QueryString["QMSReview_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }


    protected void FormView_QMSReview_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["QMSReview_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }

    protected void FormView_QMSReview_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_QMSReview_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["QMSReview_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_QMSReview_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["QMSReview_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void EditDataBound()
    {
      CheckBox CheckBox_EditCompleted = (CheckBox)FormView_QMSReview_Form.FindControl("CheckBox_EditCompleted");
      Label Label_EditCompleted = (Label)FormView_QMSReview_Form.FindControl("Label_EditCompleted");

      CheckBox CheckBox_EditIsActive = (CheckBox)FormView_QMSReview_Form.FindControl("CheckBox_EditIsActive");
      Label Label_EditIsActive = (Label)FormView_QMSReview_Form.FindControl("Label_EditIsActive");

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE SecurityUser_UserName = @SecurityUser_UserName AND Form_Id IN ('-1','31') AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
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

            Session["QMSReviewId"] = "";
            Session["QMSReviewIsActive"] = "";
            string SQLStringQMSReview = "SELECT QMSReview_Id , QMSReview_IsActive FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
            using (SqlCommand SqlCommand_QMSReview = new SqlCommand(SQLStringQMSReview))
            {
              SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
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
                    Session["QMSReviewIsActive"] = DataRow_Row["QMSReview_IsActive"];
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";

              if (Session["QMSReviewIsActive"].ToString() == "True")
              {
                CheckBox_EditCompleted.Visible = true;
                Label_EditCompleted.Visible = false;
              }
              else if (Session["QMSReviewIsActive"].ToString() == "False")
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

              if (Session["QMSReviewIsActive"].ToString() == "True")
              {
                CheckBox_EditCompleted.Visible = true;
                Label_EditCompleted.Visible = false;
              }
              else if (Session["QMSReviewIsActive"].ToString() == "False")
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
      Session["QMSReviewDate"] = "";
      string SQLStringQMSReview1 = "SELECT Facility_FacilityDisplayName , QMSReview_Date FROM vForm_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
      using (SqlCommand SqlCommand_QMSReview1 = new SqlCommand(SQLStringQMSReview1))
      {
        SqlCommand_QMSReview1.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
        DataTable DataTable_QMSReview1;
        using (DataTable_QMSReview1 = new DataTable())
        {
          DataTable_QMSReview1.Locale = CultureInfo.CurrentCulture;
          DataTable_QMSReview1 = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QMSReview1).Copy();
          if (DataTable_QMSReview1.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_QMSReview1.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["QMSReviewDate"] = DataRow_Row["QMSReview_Date"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label Label_EditFacility = (Label)FormView_QMSReview_Form.FindControl("Label_EditFacility");
        Label_EditFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_EditDate = (Label)FormView_QMSReview_Form.FindControl("Label_EditDate");
        Label_EditDate.Text = Convert.ToDateTime(Session["QMSReviewDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("QMSReviewDate");
    }

    protected void ReadOnlyDataBound()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["QMSReviewDate"] = "";
      string SQLStringQMSReview = "SELECT Facility_FacilityDisplayName , QMSReview_Date FROM vForm_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
      using (SqlCommand SqlCommand_QMSReview = new SqlCommand(SQLStringQMSReview))
      {
        SqlCommand_QMSReview.Parameters.AddWithValue("@QMSReview_Id", Request.QueryString["QMSReview_Id"]);
        DataTable DataTable_QMSReview;
        using (DataTable_QMSReview = new DataTable())
        {
          DataTable_QMSReview.Locale = CultureInfo.CurrentCulture;
          DataTable_QMSReview = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QMSReview).Copy();
          if (DataTable_QMSReview.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_QMSReview.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["QMSReviewDate"] = DataRow_Row["QMSReview_Date"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["FacilityFacilityDisplayName"].ToString()))
      {
        Label Label_ItemFacility = (Label)FormView_QMSReview_Form.FindControl("Label_ItemFacility");
        Label_ItemFacility.Text = Session["FacilityFacilityDisplayName"].ToString();

        Label Label_ItemDate = (Label)FormView_QMSReview_Form.FindControl("Label_ItemDate");
        Label_ItemDate.Text = Convert.ToDateTime(Session["QMSReviewDate"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("QMSReviewDate");
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
    public static string DatabaseFileName(object qmsReview_ZipFileName)
    {
      string DatabaseFileName = "";
      if (qmsReview_ZipFileName != null)
      {
        DatabaseFileName = "" + qmsReview_ZipFileName.ToString() + "";
      }

      return DatabaseFileName;
    }

    protected void RetrieveDatabaseFile(object sender, EventArgs e)
    {
      LinkButton LinkButton_QMSReviewFile = (LinkButton)sender;
      string FileId = LinkButton_QMSReviewFile.CommandArgument.ToString();

      Session["QMSReviewZipFileName"] = "";
      Session["QMSReviewContentType"] = "";
      Session["QMSReviewData"] = "";
      string SQLStringQMSReviewFile = "SELECT QMSReview_ZipFileName ,QMSReview_ContentType ,QMSReview_Data FROM InfoQuest_Form_QMSReview WHERE QMSReview_Id = @QMSReview_Id";
      using (SqlCommand SqlCommand_QMSReviewFile = new SqlCommand(SQLStringQMSReviewFile))
      {
        SqlCommand_QMSReviewFile.Parameters.AddWithValue("@QMSReview_Id", FileId);
        DataTable DataTable_QMSReviewFile;
        using (DataTable_QMSReviewFile = new DataTable())
        {
          DataTable_QMSReviewFile.Locale = CultureInfo.CurrentCulture;
          DataTable_QMSReviewFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_QMSReviewFile).Copy();
          if (DataTable_QMSReviewFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_QMSReviewFile.Rows)
            {
              Session["QMSReviewZipFileName"] = DataRow_Row["QMSReview_ZipFileName"];
              Session["QMSReviewContentType"] = DataRow_Row["QMSReview_ContentType"];
              Session["QMSReviewData"] = DataRow_Row["QMSReview_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["QMSReviewData"].ToString()))
      {
        Byte[] Byte_FileData = (Byte[])Session["QMSReviewData"];
        //FileStream FileStream_File = new FileStream(Server.MapPath("App_Files/Form_CRM_DiscoveryComment_Upload/") + Session["CRMFileName"].ToString(), FileMode.Append);
        //FileStream_File.Write(Byte_FileData, 0, Byte_FileData.Length);
        //FileStream_File.Close();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = Session["QMSReviewContentType"].ToString();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Session["QMSReviewZipFileName"].ToString() + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      Session["QMSReviewZipFileName"] = "";
      Session["QMSReviewContentType"] = "";
      Session["QMSReviewData"] = "";
    }
    //---END--- --File--//
  }
}