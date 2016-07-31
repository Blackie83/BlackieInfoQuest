using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_MonthlyHospitalStatistics : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected Dictionary<string, string> IconInfoHandler = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_MHSInfoHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString() + " Information", CultureInfo.CurrentCulture);
          Label_MHSHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("5").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_GridOrganisms.Text = Convert.ToString("List of Significant Organisms", CultureInfo.CurrentCulture);
          //Label_GridWaste.Text = Convert.ToString("List of Waste", CultureInfo.CurrentCulture);
          //Label_GridWasteList.Text = Convert.ToString("List of Waste", CultureInfo.CurrentCulture);

          if (Request.QueryString["MHS_Id"] != null && Request.QueryString["ViewMode"] != null)
          {
            TableMHSInfo.Visible = true;
            TableLinks.Visible = true;
            TableForm.Visible = true;
            TableOrganisms.Visible = true;
            //TableWaste.Visible = true;
            //TableWasteList.Visible = true;

            if (TableMHSInfo.Visible == true)
            {
              SetFormVisibility();
            }
          }
          else
          {
            TableMHSInfo.Visible = false;
            TableLinks.Visible = false;
            TableForm.Visible = false;
            TableOrganisms.Visible = false;
            //TableWaste.Visible = false;
            //TableWasteList.Visible = false;

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Hospital Statistics List", "Form_MonthlyHospitalStatistics_List.aspx"), true);
          }


          if (TableMHSInfo.Visible == true)
          {
            TableMHSInfoVisible();
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
        if (Request.QueryString["MHS_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('5')) AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_MonthlyHospitalStatistics WHERE MHS_Id = @MHS_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@MHS_Id", Request.QueryString["MHS_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("5");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_MHS.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Monthly Hospital Statistics", "19");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_MHS_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHS_Form.SelectCommand="SELECT * FROM [InfoQuest_Form_MonthlyHospitalStatistics] WHERE ([MHS_Id] = @MHS_Id)";
      SqlDataSource_MHS_Form.UpdateCommand = "UPDATE [InfoQuest_Form_MonthlyHospitalStatistics] SET [MHS_OS_LabourHours] = @MHS_OS_LabourHours ,[MHS_OS_CentralLineDays] = @MHS_OS_CentralLineDays ,[MHS_OS_CatheterDays] = @MHS_OS_CatheterDays , MHS_OS_EsidimeniHAITotal = @MHS_OS_EsidimeniHAITotal , [MHS_OS_VAPDays] = @MHS_OS_VAPDays , MHS_OS_SpotlightOnCleaning = @MHS_OS_SpotlightOnCleaning ,[MHS_HABSI_MSRA] = @MHS_HABSI_MSRA ,[MHS_HABSI_MRSA] = @MHS_HABSI_MRSA ,[MHS_HABSI_Percentage] = @MHS_HABSI_Percentage ,[MHS_Other_TBStaff_Cases_Clinical] = @MHS_Other_TBStaff_Cases_Clinical ,[MHS_Other_TBStaff_Cases_MDR] = @MHS_Other_TBStaff_Cases_MDR ,[MHS_Other_TBStaff_Cases_XDR] = @MHS_Other_TBStaff_Cases_XDR ,[MHS_CC_CCNReceivedPositive] = @MHS_CC_CCNReceivedPositive , MHS_CC_CCNReceivedPositive_Calculated = @MHS_CC_CCNReceivedPositive_Calculated ,[MHS_CC_CCNReceivedSuggestions] = @MHS_CC_CCNReceivedSuggestions , MHS_CC_CCNReceivedSuggestions_Calculated = @MHS_CC_CCNReceivedSuggestions_Calculated ,[MHS_CC_CCNReceivedNegative] = @MHS_CC_CCNReceivedNegative , MHS_CC_CCNReceivedNegative_Calculated = @MHS_CC_CCNReceivedNegative_Calculated ,[MHS_CC_CCNReceivedTotal] = @MHS_CC_CCNReceivedTotal ,[MHS_CC_ComplaintsReceived] = @MHS_CC_ComplaintsReceived , MHS_CC_ComplaintsReceived_Calculated = @MHS_CC_ComplaintsReceived_Calculated ,[MHS_CC_TotalComplaints] = @MHS_CC_TotalComplaints , MHS_Care_TotalStaffTrained = @MHS_Care_TotalStaffTrained ,[MHS_ModifiedDate] = @MHS_ModifiedDate ,[MHS_ModifiedBy] = @MHS_ModifiedBy ,[MHS_History] = @MHS_History WHERE [MHS_Id] = @MHS_Id";
      SqlDataSource_MHS_Form.SelectParameters.Clear();
      SqlDataSource_MHS_Form.SelectParameters.Add("MHS_Id", TypeCode.String, Request.QueryString["MHS_Id"]);
      SqlDataSource_MHS_Form.UpdateParameters.Clear();
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_OS_LabourHours", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_OS_CentralLineDays", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_OS_CatheterDays", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_OS_EsidimeniHAITotal", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_OS_VAPDays", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_OS_SpotlightOnCleaning", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_HABSI_MSRA", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_HABSI_MRSA", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_HABSI_Percentage", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_Other_TBStaff_Cases_Clinical", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_Other_TBStaff_Cases_MDR", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_Other_TBStaff_Cases_XDR", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_CCNReceivedPositive", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_CCNReceivedPositive_Calculated", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_CCNReceivedSuggestions", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_CCNReceivedSuggestions_Calculated", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_CCNReceivedNegative", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_CCNReceivedNegative_Calculated", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_CCNReceivedTotal", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_ComplaintsReceived", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_ComplaintsReceived_Calculated", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_CC_TotalComplaints", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_Care_TotalStaffTrained", TypeCode.Decimal, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_ModifiedBy", TypeCode.String, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_History", TypeCode.String, "");
      SqlDataSource_MHS_Form.UpdateParameters.Add("MHS_Id", TypeCode.Int32, "");

      SqlDataSource_MHS_Organisms.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MHS_Organisms.SelectCommand = "spForm_Get_MonthlyHospitalStatistics_Organisms";
      SqlDataSource_MHS_Organisms.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MHS_Organisms.CancelSelectOnNullParameter = false;
      SqlDataSource_MHS_Organisms.SelectParameters.Clear();
      SqlDataSource_MHS_Organisms.SelectParameters.Add("MHS_Id", TypeCode.String, Request.QueryString["MHS_Id"]);

      //SqlDataSource_MHS_Waste.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      //SqlDataSource_MHS_Waste.SelectCommand = "spForm_Get_MonthlyHospitalStatistics_Waste";
      //SqlDataSource_MHS_Waste.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      //SqlDataSource_MHS_Waste.UpdateCommand = "UPDATE InfoQuest_Form_MonthlyHospitalStatistics_Waste SET MHS_Waste_Value = @MHS_Waste_Value , MHS_Waste_PPD = @MHS_Waste_PPD , MHS_Waste_ModifiedDate = @MHS_Waste_ModifiedDate , MHS_Waste_ModifiedBy = @MHS_Waste_ModifiedBy , MHS_Waste_History = @MHS_Waste_History WHERE MHS_Waste_Id = @MHS_Waste_Id";
      //SqlDataSource_MHS_Waste.UpdateCommandType = SqlDataSourceCommandType.Text;
      //SqlDataSource_MHS_Waste.SelectParameters.Clear();
      //SqlDataSource_MHS_Waste.SelectParameters.Add("MHS_Id", TypeCode.String, Request.QueryString["MHS_Id"]);
      //SqlDataSource_MHS_Waste.UpdateParameters.Clear();
      //SqlDataSource_MHS_Waste.UpdateParameters.Add("MHS_Waste_Value", TypeCode.Decimal, "");
      //SqlDataSource_MHS_Waste.UpdateParameters.Add("MHS_Waste_PPD", TypeCode.Decimal, "");
      //SqlDataSource_MHS_Waste.UpdateParameters.Add("MHS_Waste_ModifiedDate", TypeCode.DateTime, "");
      //SqlDataSource_MHS_Waste.UpdateParameters.Add("MHS_Waste_ModifiedBy", TypeCode.String, "");
      //SqlDataSource_MHS_Waste.UpdateParameters.Add("MHS_Waste_History", TypeCode.String, "");
      //SqlDataSource_MHS_Waste.UpdateParameters.Add("MHS_Waste_Id", TypeCode.Int32, "");

      //SqlDataSource_MHS_Waste_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      //SqlDataSource_MHS_Waste_List.SelectCommand = "spForm_Get_MonthlyHospitalStatistics_Waste";
      //SqlDataSource_MHS_Waste_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      //SqlDataSource_MHS_Waste_List.CancelSelectOnNullParameter = false;
      //SqlDataSource_MHS_Waste_List.SelectParameters.Clear();
      //SqlDataSource_MHS_Waste_List.SelectParameters.Add("MHS_Id", TypeCode.String, Request.QueryString["MHS_Id"]);
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

      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_LabourHours")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_CentralLineDays")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_CatheterDays")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_EsidimeniHAITotal")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_VAPDays")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_SpotlightOnCleaning")).Visible = false;

      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MSRA")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MRSA")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_Percentage")).Visible = false;

      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_Clinical")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_MDR")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_XDR")).Visible = false;

      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Published")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Calculated")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Published")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Calculated")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Published")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Calculated")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedTotal")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Published")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Calculated")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_TotalComplaints")).Visible = false;

      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCare_TotalStaffTrained")).Visible = false;

      Session["Security"] = "1";
      Session["RedirectURL"] = "1";
      if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
      {
        Session["Security"] = "0";
        Session["RedirectURL"] = "0";
        BeingModifiedUpdate("Lock");
        FormView_MHS_Form.ChangeMode(FormViewMode.Edit);

        SetFormVisibility_Edit_Security();

        //TableWaste.Visible = true;
        //TableWasteList.Visible = false;
      }

      if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Session["Security"] = "0";
        Session["RedirectURL"] = "1";
      }

      if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
      {
        Session["Security"] = "0";

        if (MHSBeingModified() == "1")
        {
          if (MHSCutOff() == "1")
          {
            Session["RedirectURL"] = "0";
            BeingModifiedUpdate("Lock");
            FormView_MHS_Form.ChangeMode(FormViewMode.Edit);

            SetFormVisibility_Edit_Security();

            //TableWaste.Visible = true;
            //TableWasteList.Visible = false;
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
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Hospital Statistics Form", FinalURL);
        Response.Redirect(FinalURL, false);
      }

      Session["Security"] = "1";
      Session["RedirectURL"] = "1";
    }

    protected void SetFormVisibility_Edit_Security()
    {
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_LabourHours")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditOS_LabourHours")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_CentralLineDays")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditOS_CentralLineDays")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_CatheterDays")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditOS_CatheterDays")).Visible = false;


      FromDataBase_Esidimeni FromDataBase_Esidimeni_Current = GetEsidimeni();
      string FacilityTypeLookup = FromDataBase_Esidimeni_Current.FacilityTypeLookup;
      if (FacilityTypeLookup == "3")
      {
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_EsidimeniHAITotal")).Visible = true;
        ((Label)FormView_MHS_Form.FindControl("Label_EditOS_EsidimeniHAITotal")).Visible = false;
      }


      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_VAPDays")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditOS_VAPDays")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_SpotlightOnCleaning")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditOS_SpotlightOnCleaning")).Visible = false;

      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MSRA")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditHABSI_MSRA")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MRSA")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditHABSI_MRSA")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_Percentage")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditHABSI_Percentage")).Visible = false;

      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_Clinical")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditOther_TBStaff_Cases_Clinical")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_MDR")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditOther_TBStaff_Cases_MDR")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_XDR")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditOther_TBStaff_Cases_XDR")).Visible = false;

      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedPositive")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Published")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedPositive_Published")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Calculated")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedPositive_Calculated")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedSuggestions")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Published")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedSuggestions_Published")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Calculated")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedSuggestions_Calculated")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedNegative")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Published")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedNegative_Published")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Calculated")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedNegative_Calculated")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedTotal")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_CCNReceivedTotal")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_ComplaintsReceived")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Published")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_ComplaintsReceived_Published")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Calculated")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_ComplaintsReceived_Calculated")).Visible = false;
      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_TotalComplaints")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCC_TotalComplaints")).Visible = false;

      ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCare_TotalStaffTrained")).Visible = true;
      ((Label)FormView_MHS_Form.FindControl("Label_EditCare_TotalStaffTrained")).Visible = false;
    }

    protected void SetFormVisibility_Item()
    {
      FormView_MHS_Form.ChangeMode(FormViewMode.ReadOnly);
      //TableWaste.Visible = false;
      //TableWasteList.Visible = true;
    }

    private void TableMHSInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["MHSPeriod"] = "";
      Session["MHSFYPeriod"] = "";
      string SQLStringMHSInfo = "SELECT Facility_FacilityDisplayName , MHS_Period , MHS_FYPeriod FROM vForm_MonthlyHospitalStatistics WHERE MHS_Id = @MHS_Id";
      using (SqlCommand SqlCommand_MHSInfo = new SqlCommand(SQLStringMHSInfo))
      {
        SqlCommand_MHSInfo.Parameters.AddWithValue("@MHS_Id", Request.QueryString["MHS_Id"]);
        DataTable DataTable_MHSInfo;
        using (DataTable_MHSInfo = new DataTable())
        {
          DataTable_MHSInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_MHSInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHSInfo).Copy();
          if (DataTable_MHSInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MHSInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["MHSPeriod"] = DataRow_Row["MHS_Period"];
              Session["MHSFYPeriod"] = DataRow_Row["MHS_FYPeriod"];
            }
          }
        }
      }

      Label_MHSFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_MHSMonth.Text = Session["MHSPeriod"].ToString();
      Label_MHSFYPeriod.Text = Session["MHSFYPeriod"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["MHSPeriod"] = "";
      Session["MHSFYPeriod"] = "";
    }

    private void TableFormVisible()
    {
      IconInfoHandler.Clear();

      string ListItem_Id = "";
      string ListItem_Name = "";
      string SQLStringSecurity = "SELECT ListItem_Id , ListItem_Name FROM Administration_ListItem WHERE ListCategory_Id = 40";
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
              ListItem_Id = DataRow_Row["ListItem_Id"].ToString();
              ListItem_Name = DataRow_Row["ListItem_Name"].ToString();

              IconInfoHandler.Add(ListItem_Id, ListItem_Name);
            }
          }
        }
      }

      ListItem_Id = "";
      ListItem_Name = "";

      Label Label_OSTotalAdmissionsInfo = (Label)FormView_MHS_Form.FindControl("Label_OSTotalAdmissionsInfo");
      Label_OSTotalAdmissionsInfo.Text = IconInfo("2674");
      Label Label_OSInPatientsInfo = (Label)FormView_MHS_Form.FindControl("Label_OSInPatientsInfo");
      Label_OSInPatientsInfo.Text = IconInfo("2675");
      Label Label_OSOutPatientsInfo = (Label)FormView_MHS_Form.FindControl("Label_OSOutPatientsInfo");
      Label_OSOutPatientsInfo.Text = IconInfo("2676");
      Label Label_OSAEInfo = (Label)FormView_MHS_Form.FindControl("Label_OSAEInfo");
      Label_OSAEInfo.Text = IconInfo("2737");
      Label Label_OSBirthsNormalInfo = (Label)FormView_MHS_Form.FindControl("Label_OSBirthsNormalInfo");
      Label_OSBirthsNormalInfo.Text = IconInfo("2727");
      Label Label_OSBirthsCaesareanInfo = (Label)FormView_MHS_Form.FindControl("Label_OSBirthsCaesareanInfo");
      Label_OSBirthsCaesareanInfo.Text = IconInfo("2728");
      Label Label_OSTheatreTimeInfo = (Label)FormView_MHS_Form.FindControl("Label_OSTheatreTimeInfo");
      Label_OSTheatreTimeInfo.Text = IconInfo("2902");
      Label Label_OSTotalTheatreCasesInfo = (Label)FormView_MHS_Form.FindControl("Label_OSTotalTheatreCasesInfo");
      Label_OSTotalTheatreCasesInfo.Text = IconInfo("2689");
      Label Label_OSMajorTheatreCasesInfo = (Label)FormView_MHS_Form.FindControl("Label_OSMajorTheatreCasesInfo");
      Label_OSMajorTheatreCasesInfo.Text = IconInfo("2690");
      Label Label_OSMinorTheatreCasesInfo = (Label)FormView_MHS_Form.FindControl("Label_OSMinorTheatreCasesInfo");
      Label_OSMinorTheatreCasesInfo.Text = IconInfo("2691");
      Label Label_OSTheatreCasesSSIInfo = (Label)FormView_MHS_Form.FindControl("Label_OSTheatreCasesSSIInfo");
      Label_OSTheatreCasesSSIInfo.Text = IconInfo("2678");
      Label Label_OSLabourHoursInfo = (Label)FormView_MHS_Form.FindControl("Label_OSLabourHoursInfo");
      Label_OSLabourHoursInfo.Text = IconInfo("2692");
      Label Label_OSBedsActiveInfo = (Label)FormView_MHS_Form.FindControl("Label_OSBedsActiveInfo");
      Label_OSBedsActiveInfo.Text = IconInfo("2863");
      Label Label_OSBedsRegisteredInfo = (Label)FormView_MHS_Form.FindControl("Label_OSBedsRegisteredInfo");
      Label_OSBedsRegisteredInfo.Text = IconInfo("2864");
      Label Label_OSTotalHospitalPPDInfo = (Label)FormView_MHS_Form.FindControl("Label_OSTotalHospitalPPDInfo");
      Label_OSTotalHospitalPPDInfo.Text = IconInfo("2679");
      Label Label_OSICUHCPPDInfo = (Label)FormView_MHS_Form.FindControl("Label_OSICUHCPPDInfo");
      Label_OSICUHCPPDInfo.Text = IconInfo("2680");
      Label Label_OSNNICUPPDInfo = (Label)FormView_MHS_Form.FindControl("Label_OSNNICUPPDInfo");
      Label_OSNNICUPPDInfo.Text = IconInfo("2681");
      Label Label_OSCentralLineDaysInfo = (Label)FormView_MHS_Form.FindControl("Label_OSCentralLineDaysInfo");
      Label_OSCentralLineDaysInfo.Text = IconInfo("2693");
      Label Label_OSCatheterDaysInfo = (Label)FormView_MHS_Form.FindControl("Label_OSCatheterDaysInfo");
      Label_OSCatheterDaysInfo.Text = IconInfo("2694");
      Label Label_OSEsidimeniHAITotalInfo = (Label)FormView_MHS_Form.FindControl("Label_OSEsidimeniHAITotalInfo");
      Label_OSEsidimeniHAITotalInfo.Text = IconInfo("6939");
      Label Label_OSVAPDaysInfo = (Label)FormView_MHS_Form.FindControl("Label_OSVAPDaysInfo");
      Label_OSVAPDaysInfo.Text = IconInfo("2695");
      Label Label_OSHAIRateInfo = (Label)FormView_MHS_Form.FindControl("Label_OSHAIRateInfo");
      Label_OSHAIRateInfo.Text = IconInfo("2730");
      Label Label_OSVAPRateInfo = (Label)FormView_MHS_Form.FindControl("Label_OSVAPRateInfo");
      Label_OSVAPRateInfo.Text = IconInfo("2731");
      Label Label_OSSSIRateInfo = (Label)FormView_MHS_Form.FindControl("Label_OSSSIRateInfo");
      Label_OSSSIRateInfo.Text = IconInfo("2732");
      Label Label_OSCAUTIRateInfo = (Label)FormView_MHS_Form.FindControl("Label_OSCAUTIRateInfo");
      Label_OSCAUTIRateInfo.Text = IconInfo("2733");
      Label Label_OSCLABSIRateInfo = (Label)FormView_MHS_Form.FindControl("Label_OSCLABSIRateInfo");
      Label_OSCLABSIRateInfo.Text = IconInfo("2734");
      Label Label_OSSpotlightOnCleaningInfo = (Label)FormView_MHS_Form.FindControl("Label_OSSpotlightOnCleaningInfo");
      Label_OSSpotlightOnCleaningInfo.Text = IconInfo("6940");
      Label Label_OSDSOInternalInfo = (Label)FormView_MHS_Form.FindControl("Label_OSDSOInternalInfo");
      Label_OSDSOInternalInfo.Text = IconInfo("4317");
      Label Label_OSDSOExternalInfo = (Label)FormView_MHS_Form.FindControl("Label_OSDSOExternalInfo");
      Label_OSDSOExternalInfo.Text = IconInfo("4321");
      Label Label_OSDSOExternalExcludingCOIDInfo = (Label)FormView_MHS_Form.FindControl("Label_OSDSOExternalExcludingCOIDInfo");
      Label_OSDSOExternalExcludingCOIDInfo.Text = IconInfo("4322");
      Label Label_HABSIMSRAInfo = (Label)FormView_MHS_Form.FindControl("Label_HABSIMSRAInfo");
      Label_HABSIMSRAInfo.Text = IconInfo("2696");
      Label Label_HABSIMRSAInfo = (Label)FormView_MHS_Form.FindControl("Label_HABSIMRSAInfo");
      Label_HABSIMRSAInfo.Text = IconInfo("2697");
      Label Label_HABSIPercentageInfo = (Label)FormView_MHS_Form.FindControl("Label_HABSIPercentageInfo");
      Label_HABSIPercentageInfo.Text = IconInfo("2698");
      Label Label_OtherColonisationsInfo = (Label)FormView_MHS_Form.FindControl("Label_OtherColonisationsInfo");
      Label_OtherColonisationsInfo.Text = IconInfo("2699");
      Label Label_OtherTBStaffCasesXDRInfo = (Label)FormView_MHS_Form.FindControl("Label_OtherTBStaffCasesXDRInfo");
      Label_OtherTBStaffCasesXDRInfo.Text = IconInfo("2705");
      Label Label_OtherTBStaffCasesMDRInfo = (Label)FormView_MHS_Form.FindControl("Label_OtherTBStaffCasesMDRInfo");
      Label_OtherTBStaffCasesMDRInfo.Text = IconInfo("2704");
      Label Label_OtherTBStaffCasesClinicalInfo = (Label)FormView_MHS_Form.FindControl("Label_OtherTBStaffCasesClinicalInfo");
      Label_OtherTBStaffCasesClinicalInfo.Text = IconInfo("2703");
      Label Label_OtherTBPatentsCasesXDRInfo = (Label)FormView_MHS_Form.FindControl("Label_OtherTBPatentsCasesXDRInfo");
      Label_OtherTBPatentsCasesXDRInfo.Text = IconInfo("2702");
      Label Label_OtherTBPatentsCasesMDRInfo = (Label)FormView_MHS_Form.FindControl("Label_OtherTBPatentsCasesMDRInfo");
      Label_OtherTBPatentsCasesMDRInfo.Text = IconInfo("2701");
      Label Label_OtherTBPatentsCasesClinicalInfo = (Label)FormView_MHS_Form.FindControl("Label_OtherTBPatentsCasesClinicalInfo");
      Label_OtherTBPatentsCasesClinicalInfo.Text = IconInfo("2700");
      Label Label_CCTotalComplaintsInfo = (Label)FormView_MHS_Form.FindControl("Label_CCTotalComplaintsInfo");
      Label_CCTotalComplaintsInfo.Text = IconInfo("2726");
      Label Label_CCComplaintsReceivedInfo = (Label)FormView_MHS_Form.FindControl("Label_CCComplaintsReceivedInfo");
      Label_CCComplaintsReceivedInfo.Text = IconInfo("2714");
      Label Label_CCCCNReceivedTotalInfo = (Label)FormView_MHS_Form.FindControl("Label_CCCCNReceivedTotalInfo");
      Label_CCCCNReceivedTotalInfo.Text = IconInfo("2718");
      Label Label_CCCCNReceivedNegativeInfo = (Label)FormView_MHS_Form.FindControl("Label_CCCCNReceivedNegativeInfo");
      Label_CCCCNReceivedNegativeInfo.Text = IconInfo("2717");
      Label Label_CCCCNReceivedPositiveInfo = (Label)FormView_MHS_Form.FindControl("Label_CCCCNReceivedPositiveInfo");
      Label_CCCCNReceivedPositiveInfo.Text = IconInfo("2715");
      Label Label_CCCCNReceivedSuggestionsInfo = (Label)FormView_MHS_Form.FindControl("Label_CCCCNReceivedSuggestionsInfo");
      Label_CCCCNReceivedSuggestionsInfo.Text = IconInfo("2716");
      Label Label_CareTotalStaffTrainedInfo = (Label)FormView_MHS_Form.FindControl("Label_CareTotalStaffTrainedInfo");
      Label_CareTotalStaffTrainedInfo.Text = IconInfo("6941");

      IconInfoHandler.Clear();


      if (FormView_MHS_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MSRA")).Attributes.Add("OnKeyUp", "Calculation_HABSI_Percentage();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MSRA")).Attributes.Add("OnInput", "Calculation_HABSI_Percentage();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MRSA")).Attributes.Add("OnKeyUp", "Calculation_HABSI_Percentage();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MRSA")).Attributes.Add("OnInput", "Calculation_HABSI_Percentage();");

        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive")).Attributes.Add("OnKeyUp", "Calculation_CC_CCNReceivedPositive_Calculated();Calculate_CC_CCNReceivedTotal();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive")).Attributes.Add("OnInput", "Calculation_CC_CCNReceivedPositive_Calculated();Calculate_CC_CCNReceivedTotal();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions")).Attributes.Add("OnKeyUp", "Calculation_CC_CCNReceivedSuggestions_Calculated();Calculate_CC_CCNReceivedTotal();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions")).Attributes.Add("OnInput", "Calculation_CC_CCNReceivedSuggestions_Calculated();Calculate_CC_CCNReceivedTotal();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative")).Attributes.Add("OnKeyUp", "Calculation_CCNReceivedNegative_Calculated();Calculate_CC_CCNReceivedTotal();Calculation_CC_TotalComplaints();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative")).Attributes.Add("OnInput", "Calculation_CCNReceivedNegative_Calculated();Calculate_CC_CCNReceivedTotal();Calculation_CC_TotalComplaints();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived")).Attributes.Add("OnKeyUp", "Calculation_CC_ComplaintsReceived_Calculated();Calculation_CC_TotalComplaints();");
        ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived")).Attributes.Add("OnInput", "Calculation_CC_ComplaintsReceived_Calculated();Calculation_CC_TotalComplaints();");
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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('5')) AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_MonthlyHospitalStatistics WHERE MHS_Id = @MHS_Id) OR (SecurityRole_Rank = 1))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@MHS_Id", Request.QueryString["MHS_Id"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '55'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '54'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '53'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '52'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_Esidimeni
    {
      public string FacilityTypeLookup { get; set; }
    }

    private FromDataBase_Esidimeni GetEsidimeni()
    {
      FromDataBase_Esidimeni FromDataBase_Esidimeni_New = new FromDataBase_Esidimeni();

      string SQLStringEsidimeni = "SELECT Facility_Type_Lookup FROM vForm_MonthlyHospitalStatistics WHERE MHS_Id = @MHS_Id";
      using (SqlCommand SqlCommand_Esidimeni = new SqlCommand(SQLStringEsidimeni))
      {
        SqlCommand_Esidimeni.Parameters.AddWithValue("@MHS_Id", Request.QueryString["MHS_Id"]);

        DataTable DataTable_Esidimeni;
        using (DataTable_Esidimeni = new DataTable())
        {
          DataTable_Esidimeni.Locale = CultureInfo.CurrentCulture;
          DataTable_Esidimeni = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Esidimeni).Copy();
          if (DataTable_Esidimeni.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Esidimeni.Rows)
            {
              FromDataBase_Esidimeni_New.FacilityTypeLookup = DataRow_Row["Facility_Type_Lookup"].ToString();
            }
          }
        }
      }

      return FromDataBase_Esidimeni_New;
    }


    private void BeingModifiedUpdate(string BeingModifiedStatus)
    {
      if (BeingModifiedStatus == "Lock")
      {
        string SQLStringUpdateMHS = "UPDATE InfoQuest_Form_MonthlyHospitalStatistics SET MHS_BeingModified = @MHS_BeingModified, MHS_BeingModifiedDate = @MHS_BeingModifiedDate, MHS_BeingModifiedBy = @MHS_BeingModifiedBy WHERE MHS_Id = @MHS_Id";
        using (SqlCommand SqlCommand_UpdateMHS = new SqlCommand(SQLStringUpdateMHS))
        {
          SqlCommand_UpdateMHS.Parameters.AddWithValue("@MHS_BeingModified", true);
          SqlCommand_UpdateMHS.Parameters.AddWithValue("@MHS_BeingModifiedDate", DateTime.Now.ToString());
          SqlCommand_UpdateMHS.Parameters.AddWithValue("@MHS_BeingModifiedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_UpdateMHS.Parameters.AddWithValue("@MHS_Id", Request.QueryString["MHS_Id"]);
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateMHS);
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "LockedRecord", "<script language='javascript'>LockedRecord()</script>");
      }
      else if (BeingModifiedStatus == "Unlock")
      {
        if (MHSBeingModified() == "1")
        {
          string SQLStringUpdateMHS = "UPDATE InfoQuest_Form_MonthlyHospitalStatistics SET MHS_BeingModified = @MHS_BeingModified, MHS_BeingModifiedDate = @MHS_BeingModifiedDate, MHS_BeingModifiedBy = @MHS_BeingModifiedBy WHERE MHS_Id = @MHS_Id";
          using (SqlCommand SqlCommand_UpdateMHS = new SqlCommand(SQLStringUpdateMHS))
          {
            SqlCommand_UpdateMHS.Parameters.AddWithValue("@MHS_BeingModified", false);
            SqlCommand_UpdateMHS.Parameters.AddWithValue("@MHS_BeingModifiedDate", DBNull.Value);
            SqlCommand_UpdateMHS.Parameters.AddWithValue("@MHS_BeingModifiedBy", DBNull.Value);
            SqlCommand_UpdateMHS.Parameters.AddWithValue("@MHS_Id", Request.QueryString["MHS_Id"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateMHS);
          }
        }
      }
    }

    private string MHSBeingModified()
    {
      string BeingModifiedAllow = "0";

      Session["MHSBeingModified"] = "";
      string SQLStringMHSBeingModified = "SELECT MHS_BeingModified FROM InfoQuest_Form_MonthlyHospitalStatistics WHERE MHS_Id = @MHS_Id AND (MHS_BeingModifiedBy = @MHS_BeingModifiedBy OR MHS_BeingModifiedBy IS NULL)";
      using (SqlCommand SqlCommand_MHSBeingModified = new SqlCommand(SQLStringMHSBeingModified))
      {
        SqlCommand_MHSBeingModified.Parameters.AddWithValue("@MHS_Id", Request.QueryString["MHS_Id"]);
        SqlCommand_MHSBeingModified.Parameters.AddWithValue("@MHS_BeingModifiedBy", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_MHSBeingModified;
        using (DataTable_MHSBeingModified = new DataTable())
        {
          DataTable_MHSBeingModified.Locale = CultureInfo.CurrentCulture;
          DataTable_MHSBeingModified = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHSBeingModified).Copy();
          if (DataTable_MHSBeingModified.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MHSBeingModified.Rows)
            {
              Session["MHSBeingModified"] = DataRow_Row["MHS_BeingModified"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["MHSBeingModified"].ToString()))
      {
        BeingModifiedAllow = "1";
      }
      else
      {
        BeingModifiedAllow = "0";
      }

      Session["MHSBeingModified"] = "";

      return BeingModifiedAllow;
    }

    private string MHSCutOff()
    {
      string CutOffAllow = "0";

      Session["MHSPeriodStart"] = "";
      Session["MHSPeriodEnd"] = "";
      string SQLStringMHSPeriod = "SELECT MHS_PeriodDate AS MHS_PeriodStart ,CAST(DATEADD(DAY, (SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,MHS_PeriodDate)+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 5) - 1, MHS_PeriodDate) AS DATETIME) AS MHS_PeriodEnd FROM (SELECT DATEADD(MONTH,1, CAST(LEFT(MHS_Period,7) + '/01' AS DATETIME)) AS MHS_PeriodDate FROM InfoQuest_Form_MonthlyHospitalStatistics WHERE MHS_Id = @MHS_Id) AS TempTable";
      using (SqlCommand SqlCommand_MHSPeriod = new SqlCommand(SQLStringMHSPeriod))
      {
        SqlCommand_MHSPeriod.Parameters.AddWithValue("@MHS_Id", Request.QueryString["MHS_Id"]);
        DataTable DataTable_MHSPeriod;
        using (DataTable_MHSPeriod = new DataTable())
        {
          DataTable_MHSPeriod.Locale = CultureInfo.CurrentCulture;
          DataTable_MHSPeriod = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MHSPeriod).Copy();
          if (DataTable_MHSPeriod.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MHSPeriod.Rows)
            {
              Session["MHSPeriodStart"] = DataRow_Row["MHS_PeriodStart"];
              Session["MHSPeriodEnd"] = DataRow_Row["MHS_PeriodEnd"];
            }
          }
        }
      }


      if (string.IsNullOrEmpty(Session["MHSPeriodStart"].ToString()) || string.IsNullOrEmpty(Session["MHSPeriodEnd"].ToString()))
      {
        CutOffAllow = "0";
      }
      else
      {
        DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
        DateTime MHSPeriodStart = Convert.ToDateTime(Session["MHSPeriodStart"].ToString(), CultureInfo.CurrentCulture);
        DateTime MHSPeriodEnd = Convert.ToDateTime(Session["MHSPeriodEnd"].ToString(), CultureInfo.CurrentCulture);

        if ((CurrentDate.CompareTo(MHSPeriodStart) >= 0) && (CurrentDate.CompareTo(MHSPeriodEnd) <= 0))
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

    protected string IconInfo(string icon)
    {
      if (string.IsNullOrEmpty(icon))
      {
        return "";
      }
      else
      {
        if (IconInfoHandler.ContainsKey(icon))
        {
          string IconInfoValue = IconInfoHandler[icon];
          return IconInfoValue;
        }
        else
        {
          return "";
        }
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_MHSPeriod"];
      string SearchField3 = Request.QueryString["Search_MHSFYPeriod"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_MHS_Period=" + Request.QueryString["Search_MHSPeriod"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_MHS_FYPeriod=" + Request.QueryString["Search_MHSFYPeriod"] + "&";
      }

      string FinalURL = "Form_MonthlyHospitalStatistics_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Hospital Statistics List", FinalURL);

      BeingModifiedUpdate("Unlock");

      Response.Redirect(FinalURL, false);
    }

    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }


    //--START-- --TableForm--//
    protected void FormView_MHS_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDMHSModifiedDate"] = e.OldValues["MHS_ModifiedDate"];
        object OLDMHSModifiedDate = Session["OLDMHSModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDMHSModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareMHS = (DataView)SqlDataSource_MHS_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareMHS = DataView_CompareMHS[0];
        Session["DBMHSModifiedDate"] = Convert.ToString(DataRowView_CompareMHS["MHS_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBMHSModifiedBy"] = Convert.ToString(DataRowView_CompareMHS["MHS_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBMHSModifiedDate = Session["DBMHSModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBMHSModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_ConcurrencyUpdate = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBMHSModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_MHS_Form.FindControl("Label_InvalidForm")).Text = "";
          ((Label)FormView_MHS_Form.FindControl("Label_ConcurrencyUpdate")).Text = Label_ConcurrencyUpdate;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_InvalidForm = EditValidation();

          if (!string.IsNullOrEmpty(Label_InvalidForm))
          {
            e.Cancel = true;

            ((Label)FormView_MHS_Form.FindControl("Label_InvalidForm")).Text = Label_InvalidForm;
            ((Label)FormView_MHS_Form.FindControl("Label_ConcurrencyUpdate")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_InvalidForm))
          {
            e.Cancel = false;

            e.NewValues["MHS_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["MHS_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_MonthlyHospitalStatistics", "MHS_Id = " + Request.QueryString["MHS_Id"]);

            DataView DataView_MHS = (DataView)SqlDataSource_MHS_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_MHS = DataView_MHS[0];
            Session["MHSHistory"] = Convert.ToString(DataRowView_MHS["MHS_History"], CultureInfo.CurrentCulture);

            Session["MHSHistory"] = Session["History"].ToString() + Session["MHSHistory"].ToString();
            e.NewValues["MHS_History"] = Session["MHSHistory"].ToString();

            Session["MHSHistory"] = "";
            Session["History"] = "";


            e.NewValues["MHS_OS_LabourHours"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_LabourHours")).Text;
            e.NewValues["MHS_OS_CentralLineDays"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_CentralLineDays")).Text;
            e.NewValues["MHS_OS_CatheterDays"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_CatheterDays")).Text;
            e.NewValues["MHS_OS_EsidimeniHAITotal"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_EsidimeniHAITotal")).Text;
            e.NewValues["MHS_OS_VAPDays"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_VAPDays")).Text;
            e.NewValues["MHS_OS_SpotlightOnCleaning"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_SpotlightOnCleaning")).Text;


            decimal HABSIPercentageTotal = 0;
            decimal MSRA = 0;
            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MSRA")).Text))
            {
              MSRA = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MSRA")).Text, CultureInfo.CurrentCulture);
            }

            decimal MRSA = 0;
            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MRSA")).Text))
            {
              MRSA = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MRSA")).Text, CultureInfo.CurrentCulture);
            }

            if ((MSRA == 0) || (MRSA == 0))
            {
              HABSIPercentageTotal = 0;
            }
            else if ((MSRA != 0) && (MRSA != 0))
            {
              HABSIPercentageTotal = ((MRSA / MSRA) * 100);
            }

            e.NewValues["MHS_HABSI_MSRA"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MSRA")).Text;
            e.NewValues["MHS_HABSI_MRSA"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MRSA")).Text;
            e.NewValues["MHS_HABSI_Percentage"] = HABSIPercentageTotal.ToString("###0.00", CultureInfo.CurrentCulture);


            e.NewValues["MHS_Other_TBStaff_Cases_Clinical"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_Clinical")).Text;
            e.NewValues["MHS_Other_TBStaff_Cases_MDR"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_MDR")).Text;
            e.NewValues["MHS_Other_TBStaff_Cases_XDR"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_XDR")).Text;


            decimal CCCCNReceivedPositive = 0;
            decimal CCCCNReceivedPositive_Published = 0;
            decimal CCCCNReceivedPositive_Calculated = 0;
            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive")).Text))
            {
              CCCCNReceivedPositive = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive")).Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Published")).Text))
            {
              CCCCNReceivedPositive_Published = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Published")).Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Calculated")).Text))
            {
              CCCCNReceivedPositive_Calculated = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Calculated")).Text, CultureInfo.CurrentCulture);
            }

            CCCCNReceivedPositive_Calculated = CCCCNReceivedPositive + CCCCNReceivedPositive_Published;

            e.NewValues["MHS_CC_CCNReceivedPositive"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive")).Text;
            e.NewValues["MHS_CC_CCNReceivedPositive_Calculated"] = CCCCNReceivedPositive_Calculated.ToString(CultureInfo.CurrentCulture);


            decimal CCCCNReceivedSuggestions = 0;
            decimal CCCCNReceivedSuggestions_Published = 0;
            decimal CCCCNReceivedSuggestions_Calculated = 0;
            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions")).Text))
            {
              CCCCNReceivedSuggestions = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions")).Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Published")).Text))
            {
              CCCCNReceivedSuggestions_Published = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Published")).Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Calculated")).Text))
            {
              CCCCNReceivedSuggestions_Calculated = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Calculated")).Text, CultureInfo.CurrentCulture);
            }

            CCCCNReceivedSuggestions_Calculated = CCCCNReceivedSuggestions + CCCCNReceivedSuggestions_Published;

            e.NewValues["MHS_CC_CCNReceivedSuggestions"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions")).Text;
            e.NewValues["MHS_CC_CCNReceivedSuggestions_Calculated"] = CCCCNReceivedSuggestions_Calculated.ToString(CultureInfo.CurrentCulture);


            decimal CCCCNReceivedNegative = 0;
            decimal CCCCNReceivedNegative_Published = 0;
            decimal CCCCNReceivedNegative_Calculated = 0;
            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative")).Text))
            {
              CCCCNReceivedNegative = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative")).Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Published")).Text))
            {
              CCCCNReceivedNegative_Published = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Published")).Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Calculated")).Text))
            {
              CCCCNReceivedNegative_Calculated = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Calculated")).Text, CultureInfo.CurrentCulture);
            }

            CCCCNReceivedNegative_Calculated = CCCCNReceivedNegative + CCCCNReceivedNegative_Published;

            e.NewValues["MHS_CC_CCNReceivedNegative"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative")).Text;
            e.NewValues["MHS_CC_CCNReceivedNegative_Calculated"] = CCCCNReceivedNegative_Calculated.ToString(CultureInfo.CurrentCulture);


            decimal CCComplaintsReceived = 0;
            decimal CCComplaintsReceived_Published = 0;
            decimal CCComplaintsReceived_Calculated = 0;
            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived")).Text))
            {
              CCComplaintsReceived = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived")).Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Published")).Text))
            {
              CCComplaintsReceived_Published = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Published")).Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Calculated")).Text))
            {
              CCComplaintsReceived_Calculated = Convert.ToDecimal(((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Calculated")).Text, CultureInfo.CurrentCulture);
            }

            CCComplaintsReceived_Calculated = CCComplaintsReceived + CCComplaintsReceived_Published;

            e.NewValues["MHS_CC_ComplaintsReceived"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived")).Text;
            e.NewValues["MHS_CC_ComplaintsReceived_Calculated"] = CCComplaintsReceived_Calculated.ToString(CultureInfo.CurrentCulture);


            decimal CCCCNReceivedTotalTotal = 0;
            CCCCNReceivedTotalTotal = CCCCNReceivedPositive_Calculated + CCCCNReceivedSuggestions_Calculated + CCCCNReceivedNegative_Calculated;
            e.NewValues["MHS_CC_CCNReceivedTotal"] = CCCCNReceivedTotalTotal.ToString(CultureInfo.CurrentCulture);


            decimal CCTotalComplaintsTotal = 0;
            CCTotalComplaintsTotal = CCCCNReceivedNegative_Calculated + CCComplaintsReceived_Calculated;
            e.NewValues["MHS_CC_TotalComplaints"] = CCTotalComplaintsTotal.ToString(CultureInfo.CurrentCulture);


            e.NewValues["MHS_Care_TotalStaffTrained"] = ((TextBox)FormView_MHS_Form.FindControl("TextBox_EditCare_TotalStaffTrained")).Text;
          }
        }

        Session["OLDMHSModifiedDate"] = "";
        Session["DBMHSModifiedDate"] = "";
        Session["DBMHSModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditOS_LabourHours = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_LabourHours");
      TextBox TextBox_EditOS_CentralLineDays = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_CentralLineDays");
      TextBox TextBox_EditOS_CatheterDays = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_CatheterDays");
      TextBox TextBox_EditOS_EsidimeniHAITotal = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_EsidimeniHAITotal");
      TextBox TextBox_EditOS_VAPDays = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_VAPDays");
      TextBox TextBox_EditOS_SpotlightOnCleaning = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditOS_SpotlightOnCleaning");
      TextBox TextBox_EditHABSI_MSRA = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MSRA");
      TextBox TextBox_EditHABSI_MRSA = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_MRSA");
      TextBox TextBox_EditHABSI_Percentage = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditHABSI_Percentage");
      TextBox TextBox_EditOther_TBStaff_Cases_Clinical = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_Clinical");
      TextBox TextBox_EditOther_TBStaff_Cases_MDR = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_MDR");
      TextBox TextBox_EditOther_TBStaff_Cases_XDR = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditOther_TBStaff_Cases_XDR");
      TextBox TextBox_EditCC_CCNReceivedPositive = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive");
      TextBox TextBox_EditCC_CCNReceivedPositive_Calculated = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Calculated");
      TextBox TextBox_EditCC_CCNReceivedSuggestions = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions");
      TextBox TextBox_EditCC_CCNReceivedSuggestions_Calculated = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Calculated");
      TextBox TextBox_EditCC_CCNReceivedNegative = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative");
      TextBox TextBox_EditCC_CCNReceivedNegative_Calculated = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Calculated");
      TextBox TextBox_EditCC_CCNReceivedTotal = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_CCNReceivedTotal");
      TextBox TextBox_EditCC_ComplaintsReceived = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived");
      TextBox TextBox_EditCC_ComplaintsReceived_Calculated = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Calculated");
      TextBox TextBox_EditCC_TotalComplaints = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCC_TotalComplaints");
      TextBox TextBox_EditCare_TotalStaffTrained = (TextBox)FormView_MHS_Form.FindControl("TextBox_EditCare_TotalStaffTrained");

      if (InvalidForm == "No")
      {
        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOS_LabourHours.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Total Labour Hours is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOS_CentralLineDays.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Central Line Days is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOS_CatheterDays.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Uretheral Catheter Days is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOS_EsidimeniHAITotal.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Number of HAI's is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOS_VAPDays.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "VAP Days is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOS_SpotlightOnCleaning.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Spotlight on cleaning is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditHABSI_MSRA.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Total number of HA BSI cultured with any Staphylococcus species is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditHABSI_MRSA.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Number of HA BSI Cultured with MRSA is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditHABSI_Percentage.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "MRSA as a % of Total HA BSI is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOther_TBStaff_Cases_Clinical.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "TB Staff Cases Total is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOther_TBStaff_Cases_MDR.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "TB Staff Cases (MDR) is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOther_TBStaff_Cases_XDR.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "TB Staff Cases (XDR) is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_CCNReceivedPositive.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Comment Cards-Number Received Positive is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_CCNReceivedPositive_Calculated.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Comment Cards-Number Received Positive Calculated is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_CCNReceivedSuggestions.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Comment Cards-Number Received Suggestions is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_CCNReceivedSuggestions_Calculated.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Comment Cards-Number Received Suggestions Calculated is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_CCNReceivedNegative.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Comment Cards-Number Received Negative (P3) is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_CCNReceivedNegative_Calculated.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Comment Cards-Number Received Negative (P3) Calculated is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_CCNReceivedTotal.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Comment Cards-Number Received Total is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_ComplaintsReceived.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Complaints Received at Hospital level P1 and P2 is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_ComplaintsReceived_Calculated.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Complaints Received at Hospital level P1 and P2 Calculated is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCC_TotalComplaints.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Total Complaints is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCare_TotalStaffTrained.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Total number of staff trained for the month is not in the correct format<br />";
          InvalidForm = "Yes";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_MHS_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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

            ScriptManager.RegisterStartupScript(UpdatePanel_MHS, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Hospital Statistics Print", "InfoQuest_Print.aspx?PrintPage=Form_MonthlyHospitalStatistics&PrintValue=" + Request.QueryString["MHS_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_MHS, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_MHS, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Hospital Statistics Email", "InfoQuest_Email.aspx?EmailPage=Form_MonthlyHospitalStatistics&EmailValue=" + Request.QueryString["MHS_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_MHS, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }


    protected void FormView_MHS_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["MHS_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }

    protected void FormView_MHS_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_MHS_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["MHS_Id"] != null)
        {
          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 5";
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
            ((Button)FormView_MHS_Form.FindControl("Button_EditPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_MHS_Form.FindControl("Button_EditPrint")).Visible = true;
          }

          if (Email == "False")
          {
            ((Button)FormView_MHS_Form.FindControl("Button_EditEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_MHS_Form.FindControl("Button_EditEmail")).Visible = true;
          }

          Email = "";
          Print = "";
        }
      }

      if (FormView_MHS_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["MHS_Id"] != null)
        {
          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 5";
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
            ((Button)FormView_MHS_Form.FindControl("Button_ItemPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_MHS_Form.FindControl("Button_ItemPrint")).Visible = true;
            ((Button)FormView_MHS_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('InfoQuest_Print.aspx?PrintPage=Form_MonthlyHospitalStatistics&PrintValue=" + Request.QueryString["MHS_Id"] + "')";
          }

          if (Email == "False")
          {
            ((Button)FormView_MHS_Form.FindControl("Button_ItemEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_MHS_Form.FindControl("Button_ItemEmail")).Visible = true;
            ((Button)FormView_MHS_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('InfoQuest_Email.aspx?EmailPage=Form_MonthlyHospitalStatistics&EmailValue=" + Request.QueryString["MHS_Id"] + "')";
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


    //--START-- --TableOrganisms--//
    private double OrganismsTotal = 0;

    protected void SqlDataSource_MHS_Organisms_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecordsOrganisms.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_MHS_Organisms_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }

      for (int i = 0; i < GridView_MHS_Organisms.Rows.Count; i++)
      {
        if (GridView_MHS_Organisms.Rows[i].RowType == DataControlRowType.DataRow)
        {
          GridView_MHS_Organisms.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Right;
          double OrganismsValue = Convert.ToDouble(GridView_MHS_Organisms.Rows[i].Cells[1].Text, CultureInfo.CurrentCulture);
          GridView_MHS_Organisms.Rows[i].Cells[1].Text = OrganismsValue.ToString("#,##0", CultureInfo.CurrentCulture);
          OrganismsTotal = OrganismsTotal + Convert.ToDouble(GridView_MHS_Organisms.Rows[i].Cells[1].Text, CultureInfo.CurrentCulture);
        }
      }

      if (GridView_MHS_Organisms.Rows.Count > 0)
      {
        GridView_MHS_Organisms.FooterRow.Cells[0].Text = Convert.ToString("Total", CultureInfo.CurrentCulture);
        GridView_MHS_Organisms.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        GridView_MHS_Organisms.FooterRow.Cells[0].Font.Bold = true;

        GridView_MHS_Organisms.FooterRow.Cells[1].Text = OrganismsTotal.ToString("#,##0", CultureInfo.CurrentCulture);
        GridView_MHS_Organisms.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        GridView_MHS_Organisms.FooterRow.Cells[1].Font.Bold = true;
      }
    }
    //---END--- --TableOrganisms--//


    ////--START-- --TableWaste--//
    //private double WasteTotal = 0;

    //protected void SqlDataSource_MHS_Waste_Selected(object sender, SqlDataSourceStatusEventArgs e)
    //{
    //  if (e != null)
    //  {
    //    Label_TotalRecordsWaste.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
    //  }
    //}

    //protected void GridView_MHS_Waste_PreRender(object sender, EventArgs e)
    //{
    //  GridView GridView_List = (GridView)sender;
    //  GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
    //  if (GridViewRow_List != null)
    //  {
    //    GridViewRow_List.Visible = true;
    //  }

    //  for (int i = 0; i < GridView_MHS_Waste.Rows.Count; i++)
    //  {
    //    if (GridView_MHS_Waste.Rows[i].RowType == DataControlRowType.DataRow)
    //    {
    //      TextBox TextBox_WasteValue = (TextBox)GridView_MHS_Waste.Rows[i].FindControl("TextBox_WasteValue");
    //      Label Label_WasteValue = (Label)GridView_MHS_Waste.Rows[i].FindControl("Label_WasteValue");

    //      if (TextBox_WasteValue.Visible == true)
    //      {
    //        if (!String.IsNullOrEmpty(TextBox_WasteValue.Text) && InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_WasteValue.Text) != 0)
    //        {
    //          WasteTotal = WasteTotal + Convert.ToDouble(TextBox_WasteValue.Text, CultureInfo.CurrentCulture);
    //        }
    //      }

    //      if (Label_WasteValue.Visible == true)
    //      {
    //        if (!String.IsNullOrEmpty(Label_WasteValue.Text))
    //        {
    //          WasteTotal = WasteTotal + Convert.ToDouble(Label_WasteValue.Text, CultureInfo.CurrentCulture);
    //        }
    //      }
    //    }
    //  }

    //  if (GridView_MHS_Waste.Rows.Count > 0)
    //  {
    //    GridView_MHS_Waste.FooterRow.Cells[1].Text = Convert.ToString("Total", CultureInfo.CurrentCulture);
    //    GridView_MHS_Waste.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
    //    GridView_MHS_Waste.FooterRow.Cells[1].Font.Bold = true;

    //    GridView_MHS_Waste.FooterRow.Cells[2].Text = WasteTotal.ToString("#,##0.00", CultureInfo.CurrentCulture) + Convert.ToString(" kg", CultureInfo.CurrentCulture);
    //    GridView_MHS_Waste.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
    //    GridView_MHS_Waste.FooterRow.Cells[2].Font.Bold = true;
    //  }
    //}

    //protected void GridView_MHS_Waste_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //  if (e != null)
    //  {
    //    FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
    //    DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
    //    DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
    //    DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //      TextBox TextBox_WasteValue = (TextBox)e.Row.FindControl("TextBox_WasteValue");
    //      Label Label_WasteValue = (Label)e.Row.FindControl("Label_WasteValue");

    //      String Security = "1";
    //      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
    //      {
    //        Security = "0";
    //        TextBox_WasteValue.Visible = true;
    //        Label_WasteValue.Visible = false;
    //      }

    //      if (Security == "1")
    //      {
    //        Security = "0";
    //        TextBox_WasteValue.Visible = false;
    //        Label_WasteValue.Visible = true;
    //      }
    //    }

    //    if (e.Row.RowType == DataControlRowType.Pager)
    //    {
    //      Button Button_UpdateWaste = (Button)e.Row.FindControl("Button_UpdateWaste");

    //      String Security = "1";
    //      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
    //      {
    //        Security = "0";
    //        Button_UpdateWaste.Visible = true;
    //      }

    //      if (Security == "1")
    //      {
    //        Security = "0";
    //        Button_UpdateWaste.Visible = false;
    //      }
    //    }
    //  }
    //}

    //protected void Button_UpdateWaste(object sender, EventArgs e)
    //{
    //  String InvalidForm = "No";

    //  for (int a = 0; a < Convert.ToInt32(Label_TotalRecordsWaste.Text, CultureInfo.CurrentCulture); a++)
    //  {
    //    TextBox TextBox_WasteValue = (TextBox)GridView_MHS_Waste.Rows[a].Cells[2].FindControl("TextBox_WasteValue");

    //    String InvalidFormMessage = "";

    //    if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_WasteValue.Text) == 0)
    //    {
    //      InvalidFormMessage = InvalidFormMessage + "Waste Value is not in the correct format<br />";
    //      InvalidForm = "Yes";
    //    }

    //    if (!String.IsNullOrEmpty(InvalidFormMessage))
    //    {
    //      ((Label)GridView_MHS_Waste.Rows[a].Cells[2].FindControl("Label_InvalidForm")).Text = Convert.ToString(InvalidFormMessage, CultureInfo.CurrentCulture);
    //    }
    //    else
    //    {
    //      ((Label)GridView_MHS_Waste.Rows[a].Cells[2].FindControl("Label_InvalidForm")).Text = "";
    //    }
    //  }


    //  if (InvalidForm == "No")
    //  {
    //    for (int a = 0; a < Convert.ToInt32(Label_TotalRecordsWaste.Text, CultureInfo.CurrentCulture); a++)
    //    {
    //      TextBox TextBox_WasteValue = (TextBox)GridView_MHS_Waste.Rows[a].Cells[2].FindControl("TextBox_WasteValue");
    //      HiddenField HiddenField_EditWasteId = (HiddenField)GridView_MHS_Waste.Rows[a].Cells[2].FindControl("HiddenField_EditWasteId");

    //      SqlDataSource_MHS_Waste.UpdateParameters["MHS_Waste_Value"].DefaultValue = TextBox_WasteValue.Text;
    //      SqlDataSource_MHS_Waste.UpdateParameters["MHS_Waste_PPD"].DefaultValue = DBNull.Value.ToString();
    //      SqlDataSource_MHS_Waste.UpdateParameters["MHS_Waste_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
    //      SqlDataSource_MHS_Waste.UpdateParameters["MHS_Waste_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];

    //      Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("InfoQuest_Form_MonthlyHospitalStatistics_Waste", "MHS_Waste_Id = " + HiddenField_EditWasteId.Value.ToString());
    //      DataView DataView_MHS_Waste = (DataView)SqlDataSource_MHS_Waste.Select(DataSourceSelectArguments.Empty);
    //      DataRowView DataRowView_MHS_Waste = DataView_MHS_Waste[a];
    //      Session["MHSWasteHistory"] = Convert.ToString(DataRowView_MHS_Waste["MHS_Waste_History"], CultureInfo.CurrentCulture);
    //      Session["MHSWasteHistory"] = Session["History"].ToString() + Session["MHSWasteHistory"].ToString();
    //      SqlDataSource_MHS_Waste.UpdateParameters["MHS_Waste_History"].DefaultValue = Session["MHSWasteHistory"].ToString();
    //      Session["MHSWasteHistory"] = "";
    //      Session["History"] = "";

    //      SqlDataSource_MHS_Waste.UpdateParameters["MHS_Waste_Id"].DefaultValue = HiddenField_EditWasteId.Value;

    //      SqlDataSource_MHS_Waste.Update();
    //    }

    //    SqlDataSource_MHS_Waste.DataBind();
    //    GridView_MHS_Waste.DataBind();

    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", "<script language='javascript'>alert('Waste has been updated');</script>", false);
    //  }
    //}
    ////---END--- --TableWaste--//


    ////--START-- --TableWasteList--//
    //private double WasteListTotal = 0;

    //protected void SqlDataSource_MHS_Waste_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    //{
    //  if (e != null)
    //  {
    //    Label_TotalRecordsWasteList.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
    //  }
    //}

    //protected void GridView_MHS_Waste_List_PreRender(object sender, EventArgs e)
    //{
    //  GridView GridView_List = (GridView)sender;
    //  GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
    //  if (GridViewRow_List != null)
    //  {
    //    GridViewRow_List.Visible = true;
    //  }

    //  for (int i = 0; i < GridView_MHS_Waste_List.Rows.Count; i++)
    //  {
    //    if (GridView_MHS_Waste_List.Rows[i].RowType == DataControlRowType.DataRow)
    //    {
    //      Label Label_WasteValue = (Label)GridView_MHS_Waste_List.Rows[i].FindControl("Label_WasteValue");

    //      if (Label_WasteValue.Visible == true)
    //      {
    //        if (!String.IsNullOrEmpty(Label_WasteValue.Text))
    //        {
    //          WasteListTotal = WasteListTotal + Convert.ToDouble(Label_WasteValue.Text, CultureInfo.CurrentCulture);
    //        }
    //      }
    //    }
    //  }

    //  if (GridView_MHS_Waste_List.Rows.Count > 0)
    //  {
    //    GridView_MHS_Waste_List.FooterRow.Cells[1].Text = Convert.ToString("Total", CultureInfo.CurrentCulture);
    //    GridView_MHS_Waste_List.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
    //    GridView_MHS_Waste_List.FooterRow.Cells[1].Font.Bold = true;

    //    GridView_MHS_Waste_List.FooterRow.Cells[2].Text = WasteListTotal.ToString("#,##0.00", CultureInfo.CurrentCulture) + Convert.ToString(" kg", CultureInfo.CurrentCulture);
    //    GridView_MHS_Waste_List.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
    //    GridView_MHS_Waste_List.FooterRow.Cells[2].Font.Bold = true;
    //  }
    //}
    ////---END--- --TableWasteList--//
  }
}