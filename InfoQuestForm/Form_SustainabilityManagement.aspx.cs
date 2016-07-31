using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;

namespace InfoQuestForm
{
  public partial class Form_SustainabilityManagement : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_SustainabilityManagement, this.GetType(), "UpdateProgress_Start", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("45").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_SustainabilityManagementInfoHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("45").Replace(" Form", "")).ToString() + " Information", CultureInfo.CurrentCulture);
          Label_GridItem.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("45").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridItemList.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("45").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          if (Request.QueryString["SustainabilityManagement_Id"] != null && Request.QueryString["ViewMode"] != null)
          {
            TableSustainabilityManagementInfo.Visible = true;
            TableLinks.Visible = true;
            TableItem.Visible = true;
            TableItemList.Visible = true;

            if (TableSustainabilityManagementInfo.Visible == true)
            {
              SetFormVisibility();
            }
          }
          else
          {
            TableSustainabilityManagementInfo.Visible = false;
            TableLinks.Visible = false;
            TableItem.Visible = false;
            TableItemList.Visible = false;

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management List", "Form_SustainabilityManagement_List.aspx"), true);
          }

          if (TableItem.Visible == true)
          {
            TableItemVisible();
          }

          if (TableSustainabilityManagementInfo.Visible == true)
          {
            TableSustainabilityManagementInfoVisible();
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
        if (Request.QueryString["SustainabilityManagement_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('45')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_SustainabilityManagement WHERE SustainabilityManagement_Id = @SustainabilityManagement_Id) OR (SecurityRole_Rank = 1))";
        }

        if (!string.IsNullOrEmpty(SQLStringSecurity))
        {
          using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
          {
            SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_Security.Parameters.AddWithValue("@SustainabilityManagement_Id", Request.QueryString["SustainabilityManagement_Id"]);

            SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
          }
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("45");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SustainabilityManagement.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Sustainability Management", "21");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_SustainabilityManagement_Item.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_SustainabilityManagement_Item.SelectCommand = "spForm_Get_SustainabilityManagement_Item";
      SqlDataSource_SustainabilityManagement_Item.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_SustainabilityManagement_Item.UpdateCommand = "UPDATE Form_SustainabilityManagement_Item SET SustainabilityManagement_Item_Value = @SustainabilityManagement_Item_Value , SustainabilityManagement_Item_ModifiedDate = @SustainabilityManagement_Item_ModifiedDate , SustainabilityManagement_Item_ModifiedBy = @SustainabilityManagement_Item_ModifiedBy , SustainabilityManagement_Item_History = @SustainabilityManagement_Item_History WHERE SustainabilityManagement_Item_Id = @SustainabilityManagement_Item_Id";
      SqlDataSource_SustainabilityManagement_Item.UpdateCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_SustainabilityManagement_Item.SelectParameters.Clear();
      SqlDataSource_SustainabilityManagement_Item.SelectParameters.Add("SecurityUser", Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_SustainabilityManagement_Item.SelectParameters.Add("SustainabilityManagement_Id", TypeCode.String, Request.QueryString["SustainabilityManagement_Id"]);
      SqlDataSource_SustainabilityManagement_Item.UpdateParameters.Clear();
      SqlDataSource_SustainabilityManagement_Item.UpdateParameters.Add("SustainabilityManagement_Item_Value", TypeCode.Decimal, "");
      SqlDataSource_SustainabilityManagement_Item.UpdateParameters.Add("SustainabilityManagement_Item_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_SustainabilityManagement_Item.UpdateParameters.Add("SustainabilityManagement_Item_ModifiedBy", TypeCode.String, "");
      SqlDataSource_SustainabilityManagement_Item.UpdateParameters.Add("SustainabilityManagement_Item_History", TypeCode.String, "");
      SqlDataSource_SustainabilityManagement_Item.UpdateParameters.Add("SustainabilityManagement_Item_Id", TypeCode.Int32, "");

      SqlDataSource_SustainabilityManagement_Item_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_SustainabilityManagement_Item_List.SelectCommand = "spForm_Get_SustainabilityManagement_Item";
      SqlDataSource_SustainabilityManagement_Item_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_SustainabilityManagement_Item_List.CancelSelectOnNullParameter = false;
      SqlDataSource_SustainabilityManagement_Item_List.SelectParameters.Clear();
      SqlDataSource_SustainabilityManagement_Item_List.SelectParameters.Add("SecurityUser", Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_SustainabilityManagement_Item_List.SelectParameters.Add("SustainabilityManagement_Id", TypeCode.String, Request.QueryString["SustainabilityManagement_Id"]);
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["ViewMode"] == "0")
      {
        SetFormVisibility_Item();
      }
      else if (Request.QueryString["ViewMode"] == "1")
      {
        SetFormVisibility_Edit();
      }
    }

    protected void SetFormVisibility_Edit()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      Session["Security"] = "1";
      Session["RedirectURL"] = "1";
      if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
      {
        Session["Security"] = "0";
        Session["RedirectURL"] = "0";
        BeingModifiedUpdate("Lock");

        TableItem.Visible = true;
        TableItemList.Visible = false;
      }

      if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Session["Security"] = "0";
        Session["RedirectURL"] = "1";
      }

      if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
      {
        Session["Security"] = "0";

        if (SustainabilityManagementBeingModified() == "1")
        {
          if (SustainabilityManagementCutOff() == "1")
          {
            Session["RedirectURL"] = "0";
            BeingModifiedUpdate("Lock");

            TableItem.Visible = true;
            TableItemList.Visible = false;
          }
          else
          {
            Session["RedirectURL"] = "1";
          }
        }
        else
        {
          Session["RedirectURL"] = "1";
        }
      }

      if (Session["RedirectURL"].ToString() == "1")
      {
        string CurrentURL = Request.Url.AbsoluteUri;
        string FinalURL = CurrentURL.Replace("ViewMode=1", "ViewMode=0");
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management Form", FinalURL);
        Response.Redirect(FinalURL, false);
      }

      Session["Security"] = "1";
      Session["RedirectURL"] = "1";
    }

    protected void SetFormVisibility_Item()
    {
      TableItem.Visible = false;
      TableItemList.Visible = true;
    }

    private void TableSustainabilityManagementInfoVisible()
    {
      string FacilityFacilityDisplayName = "";
      string SustainabilityManagementPeriod = "";
      string SustainabilityManagementFYPeriod = "";
      string SQLStringSustainabilityManagementInfo = "SELECT Facility_FacilityDisplayName , SustainabilityManagement_Period , SustainabilityManagement_FYPeriod FROM vForm_SustainabilityManagement WHERE SustainabilityManagement_Id = @SustainabilityManagement_Id";
      using (SqlCommand SqlCommand_SustainabilityManagementInfo = new SqlCommand(SQLStringSustainabilityManagementInfo))
      {
        SqlCommand_SustainabilityManagementInfo.Parameters.AddWithValue("@SustainabilityManagement_Id", Request.QueryString["SustainabilityManagement_Id"]);
        DataTable DataTable_SustainabilityManagementInfo;
        using (DataTable_SustainabilityManagementInfo = new DataTable())
        {
          DataTable_SustainabilityManagementInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_SustainabilityManagementInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SustainabilityManagementInfo).Copy();
          if (DataTable_SustainabilityManagementInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SustainabilityManagementInfo.Rows)
            {
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              SustainabilityManagementPeriod = DataRow_Row["SustainabilityManagement_Period"].ToString();
              SustainabilityManagementFYPeriod = DataRow_Row["SustainabilityManagement_FYPeriod"].ToString();
            }
          }
        }
      }

      Label_SustainabilityManagementFacility.Text = FacilityFacilityDisplayName;
      Label_SustainabilityManagementMonth.Text = SustainabilityManagementPeriod;
      Label_SustainabilityManagementFYPeriod.Text = SustainabilityManagementFYPeriod;

      FacilityFacilityDisplayName = "";
      SustainabilityManagementPeriod = "";
      SustainabilityManagementFYPeriod = "";
    }

    protected void TableItemVisible()
    {
      for (int a = 0; a < GridView_SustainabilityManagement_Item.Rows.Count; a++)
      {
        if (GridView_SustainabilityManagement_Item.Rows[a].RowType == DataControlRowType.DataRow)
        {
          ((TextBox)GridView_SustainabilityManagement_Item.Rows[a].FindControl("TextBox_EditValue")).Attributes.Add("OnChange", "Validation_Form('" + a + "');");
          ((TextBox)GridView_SustainabilityManagement_Item.Rows[a].FindControl("TextBox_EditValue")).Attributes.Add("OnInput", "Validation_Form('" + a + "');");
        }
      }
    }


    private class FromDataBase_SecurityRole
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
      public DataRow[] SecurityFacilityAdminUpdate { get; set; }
      public DataRow[] SecurityFacilityAdminView { get; set; }
    }

    private FromDataBase_SecurityRole GetSecurityRole()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_New = new FromDataBase_SecurityRole();

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('45')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_SustainabilityManagement WHERE SustainabilityManagement_Id = @SustainabilityManagement_Id) OR (SecurityRole_Rank = 1))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@SustainabilityManagement_Id", Request.QueryString["SustainabilityManagement_Id"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '178'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '179'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '180'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '181'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }


    private void BeingModifiedUpdate(string BeingModifiedStatus)
    {
      if (BeingModifiedStatus == "Lock")
      {
        string SQLStringUpdateSustainabilityManagement = "UPDATE Form_SustainabilityManagement SET SustainabilityManagement_BeingModified = @SustainabilityManagement_BeingModified, SustainabilityManagement_BeingModifiedDate = @SustainabilityManagement_BeingModifiedDate, SustainabilityManagement_BeingModifiedBy = @SustainabilityManagement_BeingModifiedBy WHERE SustainabilityManagement_Id = @SustainabilityManagement_Id";
        using (SqlCommand SqlCommand_UpdateSustainabilityManagement = new SqlCommand(SQLStringUpdateSustainabilityManagement))
        {
          SqlCommand_UpdateSustainabilityManagement.Parameters.AddWithValue("@SustainabilityManagement_BeingModified", true);
          SqlCommand_UpdateSustainabilityManagement.Parameters.AddWithValue("@SustainabilityManagement_BeingModifiedDate", DateTime.Now.ToString());
          SqlCommand_UpdateSustainabilityManagement.Parameters.AddWithValue("@SustainabilityManagement_BeingModifiedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_UpdateSustainabilityManagement.Parameters.AddWithValue("@SustainabilityManagement_Id", Request.QueryString["SustainabilityManagement_Id"]);
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateSustainabilityManagement);
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "LockedRecord", "<script language='javascript'>LockedRecord()</script>");
      }
      else if (BeingModifiedStatus == "Unlock")
      {
        if (SustainabilityManagementBeingModified() == "1")
        {
          string SQLStringUpdateSustainabilityManagement = "UPDATE Form_SustainabilityManagement SET SustainabilityManagement_BeingModified = @SustainabilityManagement_BeingModified, SustainabilityManagement_BeingModifiedDate = @SustainabilityManagement_BeingModifiedDate, SustainabilityManagement_BeingModifiedBy = @SustainabilityManagement_BeingModifiedBy WHERE SustainabilityManagement_Id = @SustainabilityManagement_Id";
          using (SqlCommand SqlCommand_UpdateSustainabilityManagement = new SqlCommand(SQLStringUpdateSustainabilityManagement))
          {
            SqlCommand_UpdateSustainabilityManagement.Parameters.AddWithValue("@SustainabilityManagement_BeingModified", false);
            SqlCommand_UpdateSustainabilityManagement.Parameters.AddWithValue("@SustainabilityManagement_BeingModifiedDate", DBNull.Value);
            SqlCommand_UpdateSustainabilityManagement.Parameters.AddWithValue("@SustainabilityManagement_BeingModifiedBy", DBNull.Value);
            SqlCommand_UpdateSustainabilityManagement.Parameters.AddWithValue("@SustainabilityManagement_Id", Request.QueryString["SustainabilityManagement_Id"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateSustainabilityManagement);
          }
        }
      }
    }

    private string SustainabilityManagementBeingModified()
    {
      string BeingModifiedAllow = "0";

      Session["SustainabilityManagementBeingModified"] = "";
      string SQLStringSustainabilityManagementBeingModified = "SELECT SustainabilityManagement_BeingModified FROM Form_SustainabilityManagement WHERE SustainabilityManagement_Id = @SustainabilityManagement_Id AND (SustainabilityManagement_BeingModifiedBy = @SustainabilityManagement_BeingModifiedBy OR SustainabilityManagement_BeingModifiedBy IS NULL)";
      using (SqlCommand SqlCommand_SustainabilityManagementBeingModified = new SqlCommand(SQLStringSustainabilityManagementBeingModified))
      {
        SqlCommand_SustainabilityManagementBeingModified.Parameters.AddWithValue("@SustainabilityManagement_Id", Request.QueryString["SustainabilityManagement_Id"]);
        SqlCommand_SustainabilityManagementBeingModified.Parameters.AddWithValue("@SustainabilityManagement_BeingModifiedBy", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_SustainabilityManagementBeingModified;
        using (DataTable_SustainabilityManagementBeingModified = new DataTable())
        {
          DataTable_SustainabilityManagementBeingModified.Locale = CultureInfo.CurrentCulture;
          DataTable_SustainabilityManagementBeingModified = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SustainabilityManagementBeingModified).Copy();
          if (DataTable_SustainabilityManagementBeingModified.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SustainabilityManagementBeingModified.Rows)
            {
              Session["SustainabilityManagementBeingModified"] = DataRow_Row["SustainabilityManagement_BeingModified"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["SustainabilityManagementBeingModified"].ToString()))
      {
        BeingModifiedAllow = "1";
      }
      else
      {
        BeingModifiedAllow = "0";
      }

      Session["SustainabilityManagementBeingModified"] = "";

      return BeingModifiedAllow;
    }

    private string SustainabilityManagementCutOff()
    {
      string CutOffAllow = "0";

      Session["SustainabilityManagementPeriodStart"] = "";
      Session["SustainabilityManagementPeriodEnd"] = "";
      string SQLStringSustainabilityManagementPeriod = "SELECT SustainabilityManagement_PeriodDate AS SustainabilityManagement_PeriodStart ,CAST(DATEADD(DAY, (SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,SustainabilityManagement_PeriodDate)+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 45) - 1, SustainabilityManagement_PeriodDate) AS DATETIME) AS SustainabilityManagement_PeriodEnd FROM (SELECT DATEADD(MONTH,1, CAST(LEFT(SustainabilityManagement_Period,7) + '/01' AS DATETIME)) AS SustainabilityManagement_PeriodDate FROM Form_SustainabilityManagement WHERE SustainabilityManagement_Id = @SustainabilityManagement_Id) AS TempTable";
      using (SqlCommand SqlCommand_SustainabilityManagementPeriod = new SqlCommand(SQLStringSustainabilityManagementPeriod))
      {
        SqlCommand_SustainabilityManagementPeriod.Parameters.AddWithValue("@SustainabilityManagement_Id", Request.QueryString["SustainabilityManagement_Id"]);
        DataTable DataTable_SustainabilityManagementPeriod;
        using (DataTable_SustainabilityManagementPeriod = new DataTable())
        {
          DataTable_SustainabilityManagementPeriod.Locale = CultureInfo.CurrentCulture;
          DataTable_SustainabilityManagementPeriod = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SustainabilityManagementPeriod).Copy();
          if (DataTable_SustainabilityManagementPeriod.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SustainabilityManagementPeriod.Rows)
            {
              Session["SustainabilityManagementPeriodStart"] = DataRow_Row["SustainabilityManagement_PeriodStart"];
              Session["SustainabilityManagementPeriodEnd"] = DataRow_Row["SustainabilityManagement_PeriodEnd"];
            }
          }
        }
      }


      if (string.IsNullOrEmpty(Session["SustainabilityManagementPeriodStart"].ToString()) || string.IsNullOrEmpty(Session["SustainabilityManagementPeriodEnd"].ToString()))
      {
        CutOffAllow = "0";
      }
      else
      {
        DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
        DateTime SustainabilityManagementPeriodStart = Convert.ToDateTime(Session["SustainabilityManagementPeriodStart"].ToString(), CultureInfo.CurrentCulture);
        DateTime SustainabilityManagementPeriodEnd = Convert.ToDateTime(Session["SustainabilityManagementPeriodEnd"].ToString(), CultureInfo.CurrentCulture);

        if ((CurrentDate.CompareTo(SustainabilityManagementPeriodStart) >= 0) && (CurrentDate.CompareTo(SustainabilityManagementPeriodEnd) <= 0))
        {
          CutOffAllow = "1";
        }
        else
        {
          CutOffAllow = "0";
        }
      }

      return CutOffAllow;
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_SustainabilityManagementPeriod"];
      string SearchField3 = Request.QueryString["Search_SustainabilityManagementFYPeriod"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SustainabilityManagement_Period=" + Request.QueryString["Search_SustainabilityManagementPeriod"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_SustainabilityManagement_FYPeriod=" + Request.QueryString["Search_SustainabilityManagementFYPeriod"] + "&";
      }

      string FinalURL = "Form_SustainabilityManagement_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management List", FinalURL);

      BeingModifiedUpdate("Unlock");

      Response.Redirect(FinalURL, false);
    }

    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }


    //--START-- --TableItem--//
    protected void SqlDataSource_SustainabilityManagement_Item_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecordsItem.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
        HiddenField_TotalRecordsItem.Value = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_SustainabilityManagement_Item_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }

      if (GridView_List.Rows.Count > 0)
      {
        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 45";
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

        if (Email == "False")
        {
          ((Button)GridViewRow_List.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)GridViewRow_List.FindControl("Button_EditEmail")).Visible = true;
        }

        if (Print == "False")
        {
          ((Button)GridViewRow_List.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)GridViewRow_List.FindControl("Button_EditPrint")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void GridView_SustainabilityManagement_Item_DataBound(object sender, EventArgs e)
    {
      for (int i = GridView_SustainabilityManagement_Item.Rows.Count - 1; i > 0; i--)
      {
        GridViewRow GridViewRow_Row = GridView_SustainabilityManagement_Item.Rows[i];
        GridViewRow GridViewRow_PreviousRow = GridView_SustainabilityManagement_Item.Rows[i - 1];
        //for (int j = 0; j < row.Cells.Count; j++)
        for (int j = 0; j < 1; j++)
        {
          if (GridViewRow_Row.Cells[j].Text == GridViewRow_PreviousRow.Cells[j].Text)
          {
            if (GridViewRow_PreviousRow.Cells[j].RowSpan == 0)
            {
              if (GridViewRow_Row.Cells[j].RowSpan == 0)
              {
                GridViewRow_PreviousRow.Cells[j].RowSpan += 2;
              }
              else
              {
                GridViewRow_PreviousRow.Cells[j].RowSpan = GridViewRow_Row.Cells[j].RowSpan + 1;
              }

              GridViewRow_Row.Cells[j].Visible = false;
            }
          }
        }

        GridViewRow_Row.Cells[0].BackColor = Color.FromName("#f7f7f7");
        GridViewRow_Row.Cells[0].ForeColor = Color.FromName("#000000");
      }


      for (int i = 0; i < GridView_SustainabilityManagement_Item.Rows.Count; i++)
      {
        if (GridView_SustainabilityManagement_Item.Rows[i].RowType == DataControlRowType.DataRow)
        {
          TextBox TextBox_EditValue = (TextBox)GridView_SustainabilityManagement_Item.Rows[i].FindControl("TextBox_EditValue");
          Label Label_EditValue = (Label)GridView_SustainabilityManagement_Item.Rows[i].FindControl("Label_EditValue");
          HiddenField HiddenField_EditCapture = (HiddenField)GridView_SustainabilityManagement_Item.Rows[i].FindControl("HiddenField_EditCapture");

          if (HiddenField_EditCapture.Value == "Yes")
          {
            TextBox_EditValue.Visible = true;
            Label_EditValue.Visible = false;
          }
          else
          {
            TextBox_EditValue.Visible = false;
            Label_EditValue.Visible = true;
          }
        }
      }
    }

    protected void Button_EditUpdate(object sender, EventArgs e)
    {
      string InvalidForm = "No";

      for (int a = 0; a < Convert.ToInt32(Label_TotalRecordsItem.Text, CultureInfo.CurrentCulture); a++)
      {
        TextBox TextBox_EditValue = (TextBox)GridView_SustainabilityManagement_Item.Rows[a].Cells[2].FindControl("TextBox_EditValue");

        string InvalidFormMessage = "";

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditValue.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Item Value is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (!string.IsNullOrEmpty(InvalidFormMessage))
        {
          ((Label)GridView_SustainabilityManagement_Item.Rows[a].Cells[2].FindControl("Label_EditInvalidForm")).Text = Convert.ToString(InvalidFormMessage, CultureInfo.CurrentCulture);
        }
        else
        {
          ((Label)GridView_SustainabilityManagement_Item.Rows[a].Cells[2].FindControl("Label_EditInvalidForm")).Text = "";
        }
      }


      if (InvalidForm == "No")
      {
        for (int a = 0; a < Convert.ToInt32(Label_TotalRecordsItem.Text, CultureInfo.CurrentCulture); a++)
        {
          TextBox TextBox_EditValue = (TextBox)GridView_SustainabilityManagement_Item.Rows[a].Cells[2].FindControl("TextBox_EditValue");
          HiddenField HiddenField_EditValue = (HiddenField)GridView_SustainabilityManagement_Item.Rows[a].Cells[2].FindControl("HiddenField_EditValue");
          HiddenField HiddenField_EditItemId = (HiddenField)GridView_SustainabilityManagement_Item.Rows[a].Cells[2].FindControl("HiddenField_EditItemId");

          if (TextBox_EditValue.Text != HiddenField_EditValue.Value)
          {
            SqlDataSource_SustainabilityManagement_Item.UpdateParameters["SustainabilityManagement_Item_Value"].DefaultValue = TextBox_EditValue.Text;
            SqlDataSource_SustainabilityManagement_Item.UpdateParameters["SustainabilityManagement_Item_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource_SustainabilityManagement_Item.UpdateParameters["SustainabilityManagement_Item_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_SustainabilityManagement_Item", "SustainabilityManagement_Item_Id = " + HiddenField_EditItemId.Value.ToString());
            DataView DataView_SustainabilityManagement_Item = (DataView)SqlDataSource_SustainabilityManagement_Item.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_SustainabilityManagement_Item = DataView_SustainabilityManagement_Item[a];
            Session["SustainabilityManagementItemHistory"] = Convert.ToString(DataRowView_SustainabilityManagement_Item["SustainabilityManagement_Item_History"], CultureInfo.CurrentCulture);
            Session["SustainabilityManagementItemHistory"] = Session["History"].ToString() + Session["SustainabilityManagementItemHistory"].ToString();
            SqlDataSource_SustainabilityManagement_Item.UpdateParameters["SustainabilityManagement_Item_History"].DefaultValue = Session["SustainabilityManagementItemHistory"].ToString();
            Session["SustainabilityManagementItemHistory"] = "";
            Session["History"] = "";

            SqlDataSource_SustainabilityManagement_Item.UpdateParameters["SustainabilityManagement_Item_Id"].DefaultValue = HiddenField_EditItemId.Value;

            SqlDataSource_SustainabilityManagement_Item.Update();
          }
        }

        SqlDataSource_SustainabilityManagement_Item.DataBind();
        GridView_SustainabilityManagement_Item.DataBind();

        RedirectToList();
      }
    }

    protected void Button_EditPrint_Click(object sender, EventArgs e)
    {
      ScriptManager.RegisterStartupScript(UpdatePanel_SustainabilityManagement, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management Print", "InfoQuest_Print.aspx?PrintPage=Form_SustainabilityManagement&PrintValue=" + Request.QueryString["SustainabilityManagement_Id"] + "") + "')", true);
      ScriptManager.RegisterStartupScript(UpdatePanel_SustainabilityManagement, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      ScriptManager.RegisterStartupScript(UpdatePanel_SustainabilityManagement, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management Email", "InfoQuest_Email.aspx?EmailPage=Form_SustainabilityManagement&EmailValue=" + Request.QueryString["SustainabilityManagement_Id"] + "") + "')", true);
      ScriptManager.RegisterStartupScript(UpdatePanel_SustainabilityManagement, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
    }

    protected void Button_EditGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }
    //---END--- --TableItem--//


    //--START-- --TableItemList--//
    protected void SqlDataSource_SustainabilityManagement_Item_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecordsItemList.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
        HiddenField_TotalRecordsItemList.Value = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_SustainabilityManagement_Item_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }

      if (GridView_List.Rows.Count > 0)
      {
        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 45";
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

        if (Email == "False")
        {
          ((Button)GridViewRow_List.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)GridViewRow_List.FindControl("Button_ItemEmail")).Visible = true;
        }

        if (Print == "False")
        {
          ((Button)GridViewRow_List.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)GridViewRow_List.FindControl("Button_ItemPrint")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void GridView_SustainabilityManagement_Item_List_DataBound(object sender, EventArgs e)
    {
      for (int i = GridView_SustainabilityManagement_Item_List.Rows.Count - 1; i > 0; i--)
      {
        GridViewRow GridViewRow_Row = GridView_SustainabilityManagement_Item_List.Rows[i];
        GridViewRow GridViewRow_PreviousRow = GridView_SustainabilityManagement_Item_List.Rows[i - 1];
        //for (int j = 0; j < row.Cells.Count; j++)
        for (int j = 0; j < 1; j++)
        {
          if (GridViewRow_Row.Cells[j].Text == GridViewRow_PreviousRow.Cells[j].Text)
          {
            if (GridViewRow_PreviousRow.Cells[j].RowSpan == 0)
            {
              if (GridViewRow_Row.Cells[j].RowSpan == 0)
              {
                GridViewRow_PreviousRow.Cells[j].RowSpan += 2;
              }
              else
              {
                GridViewRow_PreviousRow.Cells[j].RowSpan = GridViewRow_Row.Cells[j].RowSpan + 1;
              }

              GridViewRow_Row.Cells[j].Visible = false;
            }
          }

          GridViewRow_Row.Cells[j].BackColor = Color.FromName("#f7f7f7");
          GridViewRow_Row.Cells[j].ForeColor = Color.FromName("#000000");
        }
      }
    }

    protected void Button_ItemPrint_Click(object sender, EventArgs e)
    {
      ScriptManager.RegisterStartupScript(UpdatePanel_SustainabilityManagement, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management Print", "InfoQuest_Print.aspx?PrintPage=Form_SustainabilityManagement&PrintValue=" + Request.QueryString["SustainabilityManagement_Id"] + "") + "')", true);
      ScriptManager.RegisterStartupScript(UpdatePanel_SustainabilityManagement, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
    }

    protected void Button_ItemEmail_Click(object sender, EventArgs e)
    {
      ScriptManager.RegisterStartupScript(UpdatePanel_SustainabilityManagement, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Sustainability Management Email", "InfoQuest_Email.aspx?EmailPage=Form_SustainabilityManagement&EmailValue=" + Request.QueryString["SustainabilityManagement_Id"] + "") + "')", true);
      ScriptManager.RegisterStartupScript(UpdatePanel_SustainabilityManagement, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
    }

    protected void Button_ItemGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }
    //---END--- --TableItemList--//
  }
}