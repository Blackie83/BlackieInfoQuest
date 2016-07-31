using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_MonthlyPharmacyStatistics : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("32").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_MPSInfoHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("32").Replace(" Form", "")).ToString() + " Information", CultureInfo.CurrentCulture);
          Label_MPSHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("32").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          if (Request.QueryString["MPS_Id"] != null && Request.QueryString["ViewMode"] != null)
          {
            TableMPSInfo.Visible = true;
            TableLinks.Visible = true;
            TableForm.Visible = true;

            if (TableMPSInfo.Visible == true)
            {
              SetFormVisibility();
            }
          }
          else
          {
            TableMPSInfo.Visible = false;
            TableLinks.Visible = false;
            TableForm.Visible = false;

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Pharmacy Statistics List", "Form_MonthlyPharmacyStatistics_List.aspx"), true);
          }

          if (TableMPSInfo.Visible == true)
          {
            TableMPSInfoVisible();
          }

          if (TableForm.Visible == true)
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
        if (Request.QueryString["MPS_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('32')) AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_MonthlyPharmacyStatistics WHERE MPS_Id = @MPS_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@MPS_Id", Request.QueryString["MPS_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("32");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_MPS.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Monthly Pharmacy Statistics", "20");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_MPS_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MPS_Form.SelectCommand="SELECT * FROM [InfoQuest_Form_MonthlyPharmacyStatistics] WHERE ([MPS_Id] = @MPS_Id)";
      SqlDataSource_MPS_Form.UpdateCommand="UPDATE [InfoQuest_Form_MonthlyPharmacyStatistics] SET [MPS_Pharmacy_NegativeStock] = @MPS_Pharmacy_NegativeStock ,[MPS_Pharmacy_CostReductionOpportunitiesRealized] = @MPS_Pharmacy_CostReductionOpportunitiesRealized ,[MPS_ModifiedDate] = @MPS_ModifiedDate ,[MPS_ModifiedBy] = @MPS_ModifiedBy ,[MPS_History] = @MPS_History WHERE [MPS_Id] = @MPS_Id";
      SqlDataSource_MPS_Form.SelectParameters.Clear();
      SqlDataSource_MPS_Form.SelectParameters.Add("MPS_Id", TypeCode.Int32, Request.QueryString["MPS_Id"]);
      SqlDataSource_MPS_Form.UpdateParameters.Clear();
      SqlDataSource_MPS_Form.UpdateParameters.Add("MPS_Pharmacy_NegativeStock", TypeCode.Decimal, "");
      SqlDataSource_MPS_Form.UpdateParameters.Add("MPS_Pharmacy_CostReductionOpportunitiesRealized", TypeCode.Decimal, "");
      SqlDataSource_MPS_Form.UpdateParameters.Add("MPS_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_MPS_Form.UpdateParameters.Add("MPS_ModifiedBy", TypeCode.String, "");
      SqlDataSource_MPS_Form.UpdateParameters.Add("MPS_History", TypeCode.String, "");
      SqlDataSource_MPS_Form.UpdateParameters.Add("MPS_Id", TypeCode.Int32, "");
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
      FormView_MPS_Form.ChangeMode(FormViewMode.Edit);

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('-1','32') AND SecurityUser_Username = @SecurityUser_Username AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_MonthlyPharmacyStatistics WHERE MPS_Id = @MPS_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@MPS_Id", Request.QueryString["MPS_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            SetFormVisibility_Edit_DataTable(DataTable_FormMode);
          }
        }
      }
    }

    protected void SetFormVisibility_Edit_DataTable(DataTable dataTable_FormMode)
    {
      if (dataTable_FormMode != null)
      {
        DataRow[] SecurityAdmin = dataTable_FormMode.Select("SecurityRole_Id = '1'");
        DataRow[] SecurityFormAdminUpdate = dataTable_FormMode.Select("SecurityRole_Id = '127'");
        DataRow[] SecurityFormAdminView = dataTable_FormMode.Select("SecurityRole_Id = '128'");
        DataRow[] SecurityFacilityAdminUpdate = dataTable_FormMode.Select("SecurityRole_Id = '129'");
        DataRow[] SecurityFacilityAdminView = dataTable_FormMode.Select("SecurityRole_Id = '130'");


        TextBox TextBox_EditPharmacy_NegativeStock = (TextBox)FormView_MPS_Form.FindControl("TextBox_EditPharmacy_NegativeStock");
        Label Label_EditPharmacy_NegativeStock = (Label)FormView_MPS_Form.FindControl("Label_EditPharmacy_NegativeStock");
        TextBox TextBox_EditPharmacy_CostReductionOpportunitiesRealized = (TextBox)FormView_MPS_Form.FindControl("TextBox_EditPharmacy_CostReductionOpportunitiesRealized");
        Label Label_EditPharmacy_CostReductionOpportunitiesRealized = (Label)FormView_MPS_Form.FindControl("Label_EditPharmacy_CostReductionOpportunitiesRealized");


        Session["Security"] = "1";
        Session["RedirectURL"] = "1";
        if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
        {
          Session["Security"] = "0";
          Session["RedirectURL"] = "0";
          BeingModifiedUpdate("Lock");
          FormView_MPS_Form.ChangeMode(FormViewMode.Edit);

          TextBox_EditPharmacy_NegativeStock.Visible = true;
          Label_EditPharmacy_NegativeStock.Visible = false;
          TextBox_EditPharmacy_CostReductionOpportunitiesRealized.Visible = true;
          Label_EditPharmacy_CostReductionOpportunitiesRealized.Visible = false;
        }

        if (Session["Security"].ToString() == "1" && SecurityFormAdminUpdate.Length > 0)
        {
          Session["Security"] = "0";
          Session["RedirectURL"] = "0";
          BeingModifiedUpdate("Lock");
          FormView_MPS_Form.ChangeMode(FormViewMode.Edit);

          TextBox_EditPharmacy_NegativeStock.Visible = true;
          Label_EditPharmacy_NegativeStock.Visible = false;
          TextBox_EditPharmacy_CostReductionOpportunitiesRealized.Visible = true;
          Label_EditPharmacy_CostReductionOpportunitiesRealized.Visible = false;
        }

        if (Session["Security"].ToString() == "1" && SecurityFormAdminView.Length > 0)
        {
          Session["Security"] = "0";
          Session["RedirectURL"] = "1";
        }

        if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
        {
          Session["Security"] = "0";

          if (MPSBeingModified() == "1")
          {
            if (MPSCutOff() == "1")
            {
              Session["RedirectURL"] = "0";
              BeingModifiedUpdate("Lock");
              FormView_MPS_Form.ChangeMode(FormViewMode.Edit);

              TextBox_EditPharmacy_NegativeStock.Visible = true;
              Label_EditPharmacy_NegativeStock.Visible = false;
              TextBox_EditPharmacy_CostReductionOpportunitiesRealized.Visible = true;
              Label_EditPharmacy_CostReductionOpportunitiesRealized.Visible = false;
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

        if (Session["Security"].ToString() == "1" && SecurityFacilityAdminView.Length > 0)
        {
          Session["Security"] = "0";
          Session["RedirectURL"] = "1";
        }


        if (Session["RedirectURL"].ToString() == "1")
        {
          string CurrentURL = Request.Url.AbsoluteUri;
          string FinalURL = CurrentURL.Replace("ViewMode=1", "ViewMode=0");
          FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Pharmacy Statistics Form", FinalURL);
          Response.Redirect(FinalURL, false);
        }

        Session["Security"] = "1";
        Session["RedirectURL"] = "1";
      }
    }

    protected void SetFormVisibility_Item()
    {
      FormView_MPS_Form.ChangeMode(FormViewMode.ReadOnly);
    }

    private void TableMPSInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["MPSPeriod"] = "";
      Session["MPSFYPeriod"] = "";
      string SQLStringMPSInfo = "SELECT Facility_FacilityDisplayName , MPS_Period , MPS_FYPeriod FROM vForm_MonthlyPharmacyStatistics WHERE MPS_Id = @MPS_Id";
      using (SqlCommand SqlCommand_MPSInfo = new SqlCommand(SQLStringMPSInfo))
      {
        SqlCommand_MPSInfo.Parameters.AddWithValue("@MPS_Id", Request.QueryString["MPS_Id"]);
        DataTable DataTable_MPSInfo;
        using (DataTable_MPSInfo = new DataTable())
        {
          DataTable_MPSInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_MPSInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MPSInfo).Copy();
          if (DataTable_MPSInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MPSInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["MPSPeriod"] = DataRow_Row["MPS_Period"];
              Session["MPSFYPeriod"] = DataRow_Row["MPS_FYPeriod"];
            }
          }
        }
      }

      Label_MPSFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_MPSMonth.Text = Session["MPSPeriod"].ToString();
      Label_MPSFYPeriod.Text = Session["MPSFYPeriod"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["MPSPeriod"] = "";
      Session["MPSFYPeriod"] = "";
    }

    private void TableFormVisible()
    {
      Session["ListItem_Id"] = "";
      Session["ListItem_Name"] = "";
      string SQLStringSecurity = "SELECT ListItem_Id , ListItem_Name FROM Administration_ListItem WHERE ListCategory_Id = 95";
      using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
      {
        DataTable DataTable_Security;
        using (DataTable_Security = new DataTable())
        {
          DataTable_Security.Locale = CultureInfo.CurrentCulture;
          DataTable_Security = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Security).Copy();
          if (DataTable_Security.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Security.Rows)
            {
              Session["ListItem_Id"] = DataRow_Row["ListItem_Id"];
              Session["ListItem_Name"] = DataRow_Row["ListItem_Name"];

              int Id = int.Parse(Session["ListItem_Id"].ToString(), CultureInfo.CurrentCulture);
              switch (Id)
              {
                case 2724:
                  Label Label_PharmacyRevenueActualInfo = (Label)FormView_MPS_Form.FindControl("Label_PharmacyRevenueActualInfo");
                  Label_PharmacyRevenueActualInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 2723:
                  Label Label_PharmacyRevenueBudgetInfo = (Label)FormView_MPS_Form.FindControl("Label_PharmacyRevenueBudgetInfo");
                  Label_PharmacyRevenueBudgetInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 2722:
                  Label Label_PharmacyCOSDrugsEthicalInfo = (Label)FormView_MPS_Form.FindControl("Label_PharmacyCOSDrugsEthicalInfo");
                  Label_PharmacyCOSDrugsEthicalInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 2721:
                  Label Label_PharmacyCOSDrugsSurgicalInfo = (Label)FormView_MPS_Form.FindControl("Label_PharmacyCOSDrugsSurgicalInfo");
                  Label_PharmacyCOSDrugsSurgicalInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 2720:
                  Label Label_PharmacyNegativeStockInfo = (Label)FormView_MPS_Form.FindControl("Label_PharmacyNegativeStockInfo");
                  Label_PharmacyNegativeStockInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 2719:
                  Label Label_PharmacyCostReductionOpportunitiesRealizedInfo = (Label)FormView_MPS_Form.FindControl("Label_PharmacyCostReductionOpportunitiesRealizedInfo");
                  Label_PharmacyCostReductionOpportunitiesRealizedInfo.Text = Session["ListItem_Name"].ToString();
                  break;

              }
            }
          }
          else
          {
            Session["ListItem_Id"] = "";
            Session["ListItem_Name"] = "";
          }
        }
      }

      Session["ListItem_Id"] = "";
      Session["ListItem_Name"] = "";
    }
    
    
    private void BeingModifiedUpdate(string BeingModifiedStatus)
    {
      if (BeingModifiedStatus == "Lock")
      {
        string SQLStringUpdateMPS = "UPDATE InfoQuest_Form_MonthlyPharmacyStatistics SET MPS_BeingModified = @MPS_BeingModified, MPS_BeingModifiedDate = @MPS_BeingModifiedDate, MPS_BeingModifiedBy = @MPS_BeingModifiedBy WHERE MPS_Id = @MPS_Id";
        using (SqlCommand SqlCommand_UpdateMPS = new SqlCommand(SQLStringUpdateMPS))
        {
          SqlCommand_UpdateMPS.Parameters.AddWithValue("@MPS_BeingModified", true);
          SqlCommand_UpdateMPS.Parameters.AddWithValue("@MPS_BeingModifiedDate", DateTime.Now.ToString());
          SqlCommand_UpdateMPS.Parameters.AddWithValue("@MPS_BeingModifiedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_UpdateMPS.Parameters.AddWithValue("@MPS_Id", Request.QueryString["MPS_Id"]);
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateMPS);
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "LockedRecord", "<script language='javascript'>LockedRecord()</script>");
      }
      else if (BeingModifiedStatus == "Unlock")
      {
        if (MPSBeingModified() == "1")
        {
          string SQLStringUpdateMPS = "UPDATE InfoQuest_Form_MonthlyPharmacyStatistics SET MPS_BeingModified = @MPS_BeingModified, MPS_BeingModifiedDate = @MPS_BeingModifiedDate, MPS_BeingModifiedBy = @MPS_BeingModifiedBy WHERE MPS_Id = @MPS_Id";
          using (SqlCommand SqlCommand_UpdateMPS = new SqlCommand(SQLStringUpdateMPS))
          {
            SqlCommand_UpdateMPS.Parameters.AddWithValue("@MPS_BeingModified", false);
            SqlCommand_UpdateMPS.Parameters.AddWithValue("@MPS_BeingModifiedDate", DBNull.Value);
            SqlCommand_UpdateMPS.Parameters.AddWithValue("@MPS_BeingModifiedBy", DBNull.Value);
            SqlCommand_UpdateMPS.Parameters.AddWithValue("@MPS_Id", Request.QueryString["MPS_Id"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateMPS);
          }
        }
      }
    }

    private string MPSBeingModified()
    {
      string BeingModifiedAllow = "0";

      Session["MPSBeingModified"] = "";
      string SQLStringMPSBeingModified = "SELECT MPS_BeingModified FROM InfoQuest_Form_MonthlyPharmacyStatistics WHERE MPS_Id = @MPS_Id AND (MPS_BeingModifiedBy = @MPS_BeingModifiedBy OR MPS_BeingModifiedBy IS NULL)";
      using (SqlCommand SqlCommand_MPSBeingModified = new SqlCommand(SQLStringMPSBeingModified))
      {
        SqlCommand_MPSBeingModified.Parameters.AddWithValue("@MPS_Id", Request.QueryString["MPS_Id"]);
        SqlCommand_MPSBeingModified.Parameters.AddWithValue("@MPS_BeingModifiedBy", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_MPSBeingModified;
        using (DataTable_MPSBeingModified = new DataTable())
        {
          DataTable_MPSBeingModified.Locale = CultureInfo.CurrentCulture;
          DataTable_MPSBeingModified = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MPSBeingModified).Copy();
          if (DataTable_MPSBeingModified.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MPSBeingModified.Rows)
            {
              Session["MPSBeingModified"] = DataRow_Row["MPS_BeingModified"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["MPSBeingModified"].ToString()))
      {
        BeingModifiedAllow = "1";
      }
      else
      {
        BeingModifiedAllow = "0";
      }

      Session["MPSBeingModified"] = "";

      return BeingModifiedAllow;
    }

    private string MPSCutOff()
    {
      string CutOffAllow = "0";

      Session["MPSPeriodStart"] = "";
      Session["MPSPeriodEnd"] = "";
      string SQLStringMPSPeriod = "SELECT MPS_PeriodDate AS MPS_PeriodStart ,CAST(DATEADD(DAY, (SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,MPS_PeriodDate)+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 32) - 1, MPS_PeriodDate) AS DATETIME) AS MPS_PeriodEnd FROM (SELECT DATEADD(MONTH,1, CAST(LEFT(MPS_Period,7) + '/01' AS DATETIME)) AS MPS_PeriodDate FROM InfoQuest_Form_MonthlyPharmacyStatistics WHERE MPS_Id = @MPS_Id) AS TempTable";
      using (SqlCommand SqlCommand_MPSPeriod = new SqlCommand(SQLStringMPSPeriod))
      {
        SqlCommand_MPSPeriod.Parameters.AddWithValue("@MPS_Id", Request.QueryString["MPS_Id"]);
        DataTable DataTable_MPSPeriod;
        using (DataTable_MPSPeriod = new DataTable())
        {
          DataTable_MPSPeriod.Locale = CultureInfo.CurrentCulture;
          DataTable_MPSPeriod = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MPSPeriod).Copy();
          if (DataTable_MPSPeriod.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MPSPeriod.Rows)
            {
              Session["MPSPeriodStart"] = DataRow_Row["MPS_PeriodStart"];
              Session["MPSPeriodEnd"] = DataRow_Row["MPS_PeriodEnd"];
            }
          }
        }
      }


      if (string.IsNullOrEmpty(Session["MPSPeriodStart"].ToString()) || string.IsNullOrEmpty(Session["MPSPeriodEnd"].ToString()))
      {
        CutOffAllow = "0";
      }
      else
      {
        DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
        DateTime MPSPeriodStart = Convert.ToDateTime(Session["MPSPeriodStart"].ToString(), CultureInfo.CurrentCulture);
        DateTime MPSPeriodEnd = Convert.ToDateTime(Session["MPSPeriodEnd"].ToString(), CultureInfo.CurrentCulture);

        if ((CurrentDate.CompareTo(MPSPeriodStart) >= 0) && (CurrentDate.CompareTo(MPSPeriodEnd) <= 0))
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
      string SearchField2 = Request.QueryString["Search_MPSPeriod"];
      string SearchField3 = Request.QueryString["Search_MPSFYPeriod"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_MPS_Period=" + Request.QueryString["Search_MPSPeriod"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_MPS_FYPeriod=" + Request.QueryString["Search_MPSFYPeriod"] + "&";
      }

      string FinalURL = "Form_MonthlyPharmacyStatistics_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Pharmacy Statistics List", FinalURL);

      BeingModifiedUpdate("Unlock");

      Response.Redirect(FinalURL, false);
    }

    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }


    //--START-- --TableForm--//
    protected void FormView_MPS_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDMPSModifiedDate"] = e.OldValues["MPS_ModifiedDate"];
        object OLDMPSModifiedDate = Session["OLDMPSModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDMPSModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareMPS = (DataView)SqlDataSource_MPS_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareMPS = DataView_CompareMPS[0];
        Session["DBMPSModifiedDate"] = Convert.ToString(DataRowView_CompareMPS["MPS_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBMPSModifiedBy"] = Convert.ToString(DataRowView_CompareMPS["MPS_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBMPSModifiedDate = Session["DBMPSModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBMPSModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_ConcurrencyUpdate = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBMPSModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_MPS_Form.FindControl("Label_InvalidForm")).Text = "";
          ((Label)FormView_MPS_Form.FindControl("Label_ConcurrencyUpdate")).Text = Label_ConcurrencyUpdate;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_InvalidForm = EditValidation();

          if (!string.IsNullOrEmpty(Label_InvalidForm))
          {
            e.Cancel = true;

            ((Label)FormView_MPS_Form.FindControl("Label_InvalidForm")).Text = Label_InvalidForm;
            ((Label)FormView_MPS_Form.FindControl("Label_ConcurrencyUpdate")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_InvalidForm))
          {
            e.Cancel = false;

            e.NewValues["MPS_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["MPS_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_MonthlyPharmacyStatistics", "MPS_Id = " + Request.QueryString["MPS_Id"]);

            DataView DataView_MPS = (DataView)SqlDataSource_MPS_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_MPS = DataView_MPS[0];
            Session["MPSHistory"] = Convert.ToString(DataRowView_MPS["MPS_History"], CultureInfo.CurrentCulture);

            Session["MPSHistory"] = Session["History"].ToString() + Session["MPSHistory"].ToString();
            e.NewValues["MPS_History"] = Session["MPSHistory"].ToString();

            Session["MPSHistory"] = "";
            Session["History"] = "";


            TextBox TextBox_EditPharmacy_NegativeStock = (TextBox)FormView_MPS_Form.FindControl("TextBox_EditPharmacy_NegativeStock");
            TextBox TextBox_EditPharmacy_CostReductionOpportunitiesRealized = (TextBox)FormView_MPS_Form.FindControl("TextBox_EditPharmacy_CostReductionOpportunitiesRealized");

            e.NewValues["MPS_Pharmacy_NegativeStock"] = TextBox_EditPharmacy_NegativeStock.Text;
            e.NewValues["MPS_Pharmacy_CostReductionOpportunitiesRealized"] = TextBox_EditPharmacy_CostReductionOpportunitiesRealized.Text;
          }
        }

        Session["OLDMPSModifiedDate"] = "";
        Session["DBMPSModifiedDate"] = "";
        Session["DBMPSModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditPharmacy_NegativeStock = (TextBox)FormView_MPS_Form.FindControl("TextBox_EditPharmacy_NegativeStock");
      TextBox TextBox_EditPharmacy_CostReductionOpportunitiesRealized = (TextBox)FormView_MPS_Form.FindControl("TextBox_EditPharmacy_CostReductionOpportunitiesRealized");

      if (InvalidForm == "No")
      {

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditPharmacy_NegativeStock.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Negative Stock is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditPharmacy_CostReductionOpportunitiesRealized.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Cost Reduction Opportunities Realized is not in the correct format<br />";
          InvalidForm = "Yes";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_MPS_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            RedirectToList();
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_MPS, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Pharmacy Statistics Print", "InfoQuest_Print.aspx?PrintPage=Form_MonthlyPharmacyStatistics&PrintValue=" + Request.QueryString["MPS_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_MPS, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_MPS, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Pharmacy Statistics Email", "InfoQuest_Email.aspx?EmailPage=Form_MonthlyPharmacyStatistics&EmailValue=" + Request.QueryString["MPS_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_MPS, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }


    protected void FormView_MPS_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["MPS_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }

    protected void FormView_MPS_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_MPS_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["MPS_Id"] != null)
        {
          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 32";
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
            ((Button)FormView_MPS_Form.FindControl("Button_EditEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_MPS_Form.FindControl("Button_EditEmail")).Visible = true;
          }

          if (Print == "False")
          {
            ((Button)FormView_MPS_Form.FindControl("Button_EditPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_MPS_Form.FindControl("Button_EditPrint")).Visible = true;
          }

          Email = "";
          Print = "";
        }
      }

      if (FormView_MPS_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["MPS_Id"] != null)
        {
          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 32";
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
            ((Button)FormView_MPS_Form.FindControl("Button_ItemEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_MPS_Form.FindControl("Button_ItemEmail")).Visible = true;
            ((Button)FormView_MPS_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('InfoQuest_Email.aspx?EmailPage=Form_MonthlyPharmacyStatistics&EmailValue=" + Request.QueryString["MPS_Id"] + "')";
          }

          if (Print == "False")
          {
            ((Button)FormView_MPS_Form.FindControl("Button_ItemPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_MPS_Form.FindControl("Button_ItemPrint")).Visible = true;
            ((Button)FormView_MPS_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('InfoQuest_Print.aspx?PrintPage=Form_MonthlyPharmacyStatistics&PrintValue=" + Request.QueryString["MPS_Id"] + "')";
          }

          Email = "";
          Print = "";
        }
      }
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
    //---END--- --TableForm--//
  }
}