using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_MonthlyOccupationalHealthStatistics : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_MOHS, this.GetType(), "UpdateProgress_Start", "Calculation_CC_CCNReceivedPositive_Calculated();Calculation_CC_CCNReceivedSuggestions_Calculated();Calculation_CCNReceivedNegative_Calculated();Calculation_CC_ComplaintsReceived_Calculated();Calculate_CC_CCNReceivedTotal();Calculation_CC_TotalComplaints();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("46").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_MOHSInfoHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("46").Replace(" Form", "")).ToString() + " Information", CultureInfo.CurrentCulture);
          Label_MOHSHeading.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("46").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          if (Request.QueryString["MOHS_Id"] != null && Request.QueryString["ViewMode"] != null)
          {
            TableMOHSInfo.Visible = true;
            TableLinks.Visible = true;
            TableForm.Visible = true;

            if (TableMOHSInfo.Visible == true)
            {
              SetFormVisibility();
            }
          }
          else
          {
            TableMOHSInfo.Visible = false;
            TableLinks.Visible = false;
            TableForm.Visible = false;

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics List", "Form_MonthlyOccupationalHealthStatistics_List.aspx"), true);
          }

          if (TableMOHSInfo.Visible == true)
          {
            TableMOHSInfoVisible();
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
        if (Request.QueryString["MOHS_Id"] == null)
        {
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('46')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_MonthlyOccupationalHealthStatistics WHERE MOHS_Id = @MOHS_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@MOHS_Id", Request.QueryString["MOHS_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("46");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_MOHS.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Monthly Occupational Health Statistics", "22");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_MOHS_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MOHS_Form.SelectCommand = "SELECT * FROM Form_MonthlyOccupationalHealthStatistics WHERE (MOHS_Id = @MOHS_Id)";
      SqlDataSource_MOHS_Form.UpdateCommand = "UPDATE Form_MonthlyOccupationalHealthStatistics SET MOHS_OH_ClinicVisits = @MOHS_OH_ClinicVisits , MOHS_OH_LabourHours = @MOHS_OH_LabourHours , MOHS_OH_PatientSatisfactionScore = @MOHS_OH_PatientSatisfactionScore , MOHS_OH_PatientSatisfactionResponseRate = @MOHS_OH_PatientSatisfactionResponseRate , MOHS_CC_CCNReceivedPositive = @MOHS_CC_CCNReceivedPositive , MOHS_CC_CCNReceivedPositive_Published = @MOHS_CC_CCNReceivedPositive_Published , MOHS_CC_CCNReceivedPositive_Calculated = @MOHS_CC_CCNReceivedPositive_Calculated , MOHS_CC_CCNReceivedSuggestions = @MOHS_CC_CCNReceivedSuggestions , MOHS_CC_CCNReceivedSuggestions_Published = @MOHS_CC_CCNReceivedSuggestions_Published , MOHS_CC_CCNReceivedSuggestions_Calculated = @MOHS_CC_CCNReceivedSuggestions_Calculated , MOHS_CC_CCNReceivedNegative = @MOHS_CC_CCNReceivedNegative , MOHS_CC_CCNReceivedNegative_Published = @MOHS_CC_CCNReceivedNegative_Published , MOHS_CC_CCNReceivedNegative_Calculated = @MOHS_CC_CCNReceivedNegative_Calculated , MOHS_CC_CCNReceivedTotal = @MOHS_CC_CCNReceivedTotal , MOHS_CC_ComplaintsReceived = @MOHS_CC_ComplaintsReceived , MOHS_CC_ComplaintsReceived_Published = @MOHS_CC_ComplaintsReceived_Published , MOHS_CC_ComplaintsReceived_Calculated = @MOHS_CC_ComplaintsReceived_Calculated , MOHS_CC_TotalComplaints = @MOHS_CC_TotalComplaints , MOHS_Care_TotalStaffTrained = @MOHS_Care_TotalStaffTrained , MOHS_Care_TotalStaffEmployed = @MOHS_Care_TotalStaffEmployed, MOHS_ModifiedDate = @MOHS_ModifiedDate , MOHS_ModifiedBy = @MOHS_ModifiedBy , MOHS_History = @MOHS_History WHERE MOHS_Id = @MOHS_Id";
      SqlDataSource_MOHS_Form.SelectParameters.Clear();
      SqlDataSource_MOHS_Form.SelectParameters.Add("MOHS_Id", TypeCode.Int32, Request.QueryString["MOHS_Id"]);
      SqlDataSource_MOHS_Form.UpdateParameters.Clear();
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_OH_ClinicVisits", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_OH_LabourHours", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_OH_PatientSatisfactionScore", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_OH_PatientSatisfactionResponseRate", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedPositive", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedPositive_Published", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedPositive_Calculated", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedSuggestions", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedSuggestions_Published", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedSuggestions_Calculated", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedNegative", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedNegative_Published", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedNegative_Calculated", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_CCNReceivedTotal", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_ComplaintsReceived", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_ComplaintsReceived_Published", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_ComplaintsReceived_Calculated", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_CC_TotalComplaints", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_Care_TotalStaffTrained", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_Care_TotalStaffEmployed", TypeCode.Decimal, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_ModifiedBy", TypeCode.String, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_History", TypeCode.String, "");
      SqlDataSource_MOHS_Form.UpdateParameters.Add("MOHS_Id", TypeCode.Int32, "");
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
      FormView_MOHS_Form.ChangeMode(FormViewMode.Edit);

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('-1','46') AND SecurityUser_Username = @SecurityUser_Username AND (Facility_Id IN (SELECT Facility_Id FROM Form_MonthlyOccupationalHealthStatistics WHERE MOHS_Id = @MOHS_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@MOHS_Id", Request.QueryString["MOHS_Id"]);
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
        DataRow[] SecurityFormAdminUpdate = dataTable_FormMode.Select("SecurityRole_Id = '182'");
        DataRow[] SecurityFormAdminView = dataTable_FormMode.Select("SecurityRole_Id = '183'");
        DataRow[] SecurityFacilityAdminUpdate = dataTable_FormMode.Select("SecurityRole_Id = '184'");
        DataRow[] SecurityFacilityAdminView = dataTable_FormMode.Select("SecurityRole_Id = '185'");


        TextBox TextBox_EditOH_ClinicVisits = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_ClinicVisits");
        Label Label_EditOH_ClinicVisits = (Label)FormView_MOHS_Form.FindControl("Label_EditOH_ClinicVisits");
        TextBox TextBox_EditOH_LabourHours = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_LabourHours");
        Label Label_EditOH_LabourHours = (Label)FormView_MOHS_Form.FindControl("Label_EditOH_LabourHours");
        TextBox TextBox_EditOH_PatientSatisfactionScore = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_PatientSatisfactionScore");
        Label Label_EditOH_PatientSatisfactionScore = (Label)FormView_MOHS_Form.FindControl("Label_EditOH_PatientSatisfactionScore");
        TextBox TextBox_EditOH_PatientSatisfactionResponseRate = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_PatientSatisfactionResponseRate");
        Label Label_EditOH_PatientSatisfactionResponseRate = (Label)FormView_MOHS_Form.FindControl("Label_EditOH_PatientSatisfactionResponseRate");
        TextBox TextBox_EditCC_CCNReceivedPositive = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive");
        Label Label_EditCC_CCNReceivedPositive = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedPositive");
        TextBox TextBox_EditCC_CCNReceivedPositive_Published = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Published");
        Label Label_EditCC_CCNReceivedPositive_Published = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedPositive_Published");
        TextBox TextBox_EditCC_CCNReceivedPositive_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Calculated");
        Label Label_EditCC_CCNReceivedPositive_Calculated = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedPositive_Calculated");
        TextBox TextBox_EditCC_CCNReceivedSuggestions = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions");
        Label Label_EditCC_CCNReceivedSuggestions = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedSuggestions");
        TextBox TextBox_EditCC_CCNReceivedSuggestions_Published = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Published");
        Label Label_EditCC_CCNReceivedSuggestions_Published = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedSuggestions_Published");
        TextBox TextBox_EditCC_CCNReceivedSuggestions_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Calculated");
        Label Label_EditCC_CCNReceivedSuggestions_Calculated = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedSuggestions_Calculated");
        TextBox TextBox_EditCC_CCNReceivedNegative = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative");
        Label Label_EditCC_CCNReceivedNegative = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedNegative");
        TextBox TextBox_EditCC_CCNReceivedNegative_Published = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Published");
        Label Label_EditCC_CCNReceivedNegative_Published = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedNegative_Published");
        TextBox TextBox_EditCC_CCNReceivedNegative_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Calculated");
        Label Label_EditCC_CCNReceivedNegative_Calculated = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedNegative_Calculated");
        TextBox TextBox_EditCC_CCNReceivedTotal = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedTotal");
        Label Label_EditCC_CCNReceivedTotal = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_CCNReceivedTotal");
        TextBox TextBox_EditCC_ComplaintsReceived = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived");
        Label Label_EditCC_ComplaintsReceived = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_ComplaintsReceived");
        TextBox TextBox_EditCC_ComplaintsReceived_Published = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Published");
        Label Label_EditCC_ComplaintsReceived_Published = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_ComplaintsReceived_Published");
        TextBox TextBox_EditCC_ComplaintsReceived_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Calculated");
        Label Label_EditCC_ComplaintsReceived_Calculated = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_ComplaintsReceived_Calculated");
        TextBox TextBox_EditCC_TotalComplaints = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_TotalComplaints");
        Label Label_EditCC_TotalComplaints = (Label)FormView_MOHS_Form.FindControl("Label_EditCC_TotalComplaints");
        TextBox TextBox_EditCare_TotalStaffTrained = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCare_TotalStaffTrained");
        Label Label_EditCare_TotalStaffTrained = (Label)FormView_MOHS_Form.FindControl("Label_EditCare_TotalStaffTrained");
        TextBox TextBox_EditCare_TotalStaffEmployed = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCare_TotalStaffEmployed");
        Label Label_EditCare_TotalStaffEmployed = (Label)FormView_MOHS_Form.FindControl("Label_EditCare_TotalStaffEmployed");


        Session["Security"] = "1";
        Session["RedirectURL"] = "1";
        if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
        {
          Session["Security"] = "0";
          Session["RedirectURL"] = "0";
          BeingModifiedUpdate("Lock");
          FormView_MOHS_Form.ChangeMode(FormViewMode.Edit);

          TextBox_EditOH_ClinicVisits.Visible = true;
          Label_EditOH_ClinicVisits.Visible = false;
          TextBox_EditOH_LabourHours.Visible = true;
          Label_EditOH_LabourHours.Visible = false;
          TextBox_EditOH_PatientSatisfactionScore.Visible = true;
          Label_EditOH_PatientSatisfactionScore.Visible = false;
          TextBox_EditOH_PatientSatisfactionResponseRate.Visible = true;
          Label_EditOH_PatientSatisfactionResponseRate.Visible = false;
          TextBox_EditCC_CCNReceivedPositive.Visible = true;
          Label_EditCC_CCNReceivedPositive.Visible = false;
          TextBox_EditCC_CCNReceivedPositive_Published.Visible = true;
          Label_EditCC_CCNReceivedPositive_Published.Visible = false;
          TextBox_EditCC_CCNReceivedPositive_Calculated.Visible = true;
          Label_EditCC_CCNReceivedPositive_Calculated.Visible = false;
          TextBox_EditCC_CCNReceivedSuggestions.Visible = true;
          Label_EditCC_CCNReceivedSuggestions.Visible = false;
          TextBox_EditCC_CCNReceivedSuggestions_Published.Visible = true;
          Label_EditCC_CCNReceivedSuggestions_Published.Visible = false;
          TextBox_EditCC_CCNReceivedSuggestions_Calculated.Visible = true;
          Label_EditCC_CCNReceivedSuggestions_Calculated.Visible = false;
          TextBox_EditCC_CCNReceivedNegative.Visible = true;
          Label_EditCC_CCNReceivedNegative.Visible = false;
          TextBox_EditCC_CCNReceivedNegative_Published.Visible = true;
          Label_EditCC_CCNReceivedNegative_Published.Visible = false;
          TextBox_EditCC_CCNReceivedNegative_Calculated.Visible = true;
          Label_EditCC_CCNReceivedNegative_Calculated.Visible = false;
          TextBox_EditCC_CCNReceivedTotal.Visible = true;
          Label_EditCC_CCNReceivedTotal.Visible = false;
          TextBox_EditCC_ComplaintsReceived.Visible = true;
          Label_EditCC_ComplaintsReceived.Visible = false;
          TextBox_EditCC_ComplaintsReceived_Published.Visible = true;
          Label_EditCC_ComplaintsReceived_Published.Visible = false;
          TextBox_EditCC_ComplaintsReceived_Calculated.Visible = true;
          Label_EditCC_ComplaintsReceived_Calculated.Visible = false;
          TextBox_EditCC_TotalComplaints.Visible = true;
          Label_EditCC_TotalComplaints.Visible = false;
          TextBox_EditCare_TotalStaffTrained.Visible = true;
          Label_EditCare_TotalStaffTrained.Visible = false;
          TextBox_EditCare_TotalStaffEmployed.Visible = true;
          Label_EditCare_TotalStaffEmployed.Visible = false;
        }

        if (Session["Security"].ToString() == "1" && SecurityFormAdminUpdate.Length > 0)
        {
          Session["Security"] = "0";
          Session["RedirectURL"] = "0";
          BeingModifiedUpdate("Lock");
          FormView_MOHS_Form.ChangeMode(FormViewMode.Edit);

          TextBox_EditOH_ClinicVisits.Visible = true;
          Label_EditOH_ClinicVisits.Visible = false;
          TextBox_EditOH_LabourHours.Visible = true;
          Label_EditOH_LabourHours.Visible = false;
          TextBox_EditOH_PatientSatisfactionScore.Visible = true;
          Label_EditOH_PatientSatisfactionScore.Visible = false;
          TextBox_EditOH_PatientSatisfactionResponseRate.Visible = true;
          Label_EditOH_PatientSatisfactionResponseRate.Visible = false;
          TextBox_EditCC_CCNReceivedPositive.Visible = true;
          Label_EditCC_CCNReceivedPositive.Visible = false;
          TextBox_EditCC_CCNReceivedPositive_Published.Visible = true;
          Label_EditCC_CCNReceivedPositive_Published.Visible = false;
          TextBox_EditCC_CCNReceivedPositive_Calculated.Visible = true;
          Label_EditCC_CCNReceivedPositive_Calculated.Visible = false;
          TextBox_EditCC_CCNReceivedSuggestions.Visible = true;
          Label_EditCC_CCNReceivedSuggestions.Visible = false;
          TextBox_EditCC_CCNReceivedSuggestions_Published.Visible = true;
          Label_EditCC_CCNReceivedSuggestions_Published.Visible = false;
          TextBox_EditCC_CCNReceivedSuggestions_Calculated.Visible = true;
          Label_EditCC_CCNReceivedSuggestions_Calculated.Visible = false;
          TextBox_EditCC_CCNReceivedNegative.Visible = true;
          Label_EditCC_CCNReceivedNegative.Visible = false;
          TextBox_EditCC_CCNReceivedNegative_Published.Visible = true;
          Label_EditCC_CCNReceivedNegative_Published.Visible = false;
          TextBox_EditCC_CCNReceivedNegative_Calculated.Visible = true;
          Label_EditCC_CCNReceivedNegative_Calculated.Visible = false;
          TextBox_EditCC_CCNReceivedTotal.Visible = true;
          Label_EditCC_CCNReceivedTotal.Visible = false;
          TextBox_EditCC_ComplaintsReceived.Visible = true;
          Label_EditCC_ComplaintsReceived.Visible = false;
          TextBox_EditCC_ComplaintsReceived_Published.Visible = true;
          Label_EditCC_ComplaintsReceived_Published.Visible = false;
          TextBox_EditCC_ComplaintsReceived_Calculated.Visible = true;
          Label_EditCC_ComplaintsReceived_Calculated.Visible = false;
          TextBox_EditCC_TotalComplaints.Visible = true;
          Label_EditCC_TotalComplaints.Visible = false;
          TextBox_EditCare_TotalStaffTrained.Visible = true;
          Label_EditCare_TotalStaffTrained.Visible = false;
          TextBox_EditCare_TotalStaffEmployed.Visible = true;
          Label_EditCare_TotalStaffEmployed.Visible = false;
        }

        if (Session["Security"].ToString() == "1" && SecurityFormAdminView.Length > 0)
        {
          Session["Security"] = "0";
          Session["RedirectURL"] = "1";
        }

        if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
        {
          Session["Security"] = "0";

          if (MOHSBeingModified() == "1")
          {
            if (MOHSCutOff() == "1")
            {
              Session["RedirectURL"] = "0";
              BeingModifiedUpdate("Lock");
              FormView_MOHS_Form.ChangeMode(FormViewMode.Edit);

              TextBox_EditOH_ClinicVisits.Visible = true;
              Label_EditOH_ClinicVisits.Visible = false;
              TextBox_EditOH_LabourHours.Visible = true;
              Label_EditOH_LabourHours.Visible = false;
              TextBox_EditOH_PatientSatisfactionScore.Visible = true;
              Label_EditOH_PatientSatisfactionScore.Visible = false;
              TextBox_EditOH_PatientSatisfactionResponseRate.Visible = true;
              Label_EditOH_PatientSatisfactionResponseRate.Visible = false;
              TextBox_EditCC_CCNReceivedPositive.Visible = true;
              Label_EditCC_CCNReceivedPositive.Visible = false;
              TextBox_EditCC_CCNReceivedPositive_Published.Visible = true;
              Label_EditCC_CCNReceivedPositive_Published.Visible = false;
              TextBox_EditCC_CCNReceivedPositive_Calculated.Visible = true;
              Label_EditCC_CCNReceivedPositive_Calculated.Visible = false;
              TextBox_EditCC_CCNReceivedSuggestions.Visible = true;
              Label_EditCC_CCNReceivedSuggestions.Visible = false;
              TextBox_EditCC_CCNReceivedSuggestions_Published.Visible = true;
              Label_EditCC_CCNReceivedSuggestions_Published.Visible = false;
              TextBox_EditCC_CCNReceivedSuggestions_Calculated.Visible = true;
              Label_EditCC_CCNReceivedSuggestions_Calculated.Visible = false;
              TextBox_EditCC_CCNReceivedNegative.Visible = true;
              Label_EditCC_CCNReceivedNegative.Visible = false;
              TextBox_EditCC_CCNReceivedNegative_Published.Visible = true;
              Label_EditCC_CCNReceivedNegative_Published.Visible = false;
              TextBox_EditCC_CCNReceivedNegative_Calculated.Visible = true;
              Label_EditCC_CCNReceivedNegative_Calculated.Visible = false;
              TextBox_EditCC_CCNReceivedTotal.Visible = true;
              Label_EditCC_CCNReceivedTotal.Visible = false;
              TextBox_EditCC_ComplaintsReceived.Visible = true;
              Label_EditCC_ComplaintsReceived.Visible = false;
              TextBox_EditCC_ComplaintsReceived_Published.Visible = true;
              Label_EditCC_ComplaintsReceived_Published.Visible = false;
              TextBox_EditCC_ComplaintsReceived_Calculated.Visible = true;
              Label_EditCC_ComplaintsReceived_Calculated.Visible = false;
              TextBox_EditCC_TotalComplaints.Visible = true;
              Label_EditCC_TotalComplaints.Visible = false;
              TextBox_EditCare_TotalStaffTrained.Visible = true;
              Label_EditCare_TotalStaffTrained.Visible = false;
              TextBox_EditCare_TotalStaffEmployed.Visible = true;
              Label_EditCare_TotalStaffEmployed.Visible = false;
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
          FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics Form", FinalURL);
          Response.Redirect(FinalURL, false);
        }

        Session["Security"] = "1";
        Session["RedirectURL"] = "1";
      }
    }

    protected void SetFormVisibility_Item()
    {
      FormView_MOHS_Form.ChangeMode(FormViewMode.ReadOnly);
    }

    private void TableMOHSInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["UnitName"] = "";
      Session["MOHSPeriod"] = "";
      Session["MOHSFYPeriod"] = "";
      string SQLStringMOHSInfo = "SELECT Facility_FacilityDisplayName , Unit_Name , MOHS_Period , MOHS_FYPeriod FROM vForm_MonthlyOccupationalHealthStatistics WHERE MOHS_Id = @MOHS_Id";
      using (SqlCommand SqlCommand_MOHSInfo = new SqlCommand(SQLStringMOHSInfo))
      {
        SqlCommand_MOHSInfo.Parameters.AddWithValue("@MOHS_Id", Request.QueryString["MOHS_Id"]);
        DataTable DataTable_MOHSInfo;
        using (DataTable_MOHSInfo = new DataTable())
        {
          DataTable_MOHSInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_MOHSInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MOHSInfo).Copy();
          if (DataTable_MOHSInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MOHSInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["UnitName"] = DataRow_Row["Unit_Name"];
              Session["MOHSPeriod"] = DataRow_Row["MOHS_Period"];
              Session["MOHSFYPeriod"] = DataRow_Row["MOHS_FYPeriod"];
            }
          }
        }
      }

      Label_MOHSFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_MOHSUnit.Text = Session["UnitName"].ToString();
      Label_MOHSMonth.Text = Session["MOHSPeriod"].ToString();
      Label_MOHSFYPeriod.Text = Session["MOHSFYPeriod"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["UnitName"] = "";
      Session["MOHSPeriod"] = "";
      Session["MOHSFYPeriod"] = "";
    }

    private void TableFormVisible()
    {
      Session["ListItem_Id"] = "";
      Session["ListItem_Name"] = "";
      string SQLStringSecurity = "SELECT ListItem_Id , ListItem_Name FROM Administration_ListItem WHERE ListCategory_Id = 193";
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
                case 6067:
                  Label Label_OHClinicVisitsInfo = (Label)FormView_MOHS_Form.FindControl("Label_OHClinicVisitsInfo");
                  Label_OHClinicVisitsInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6068:
                  Label Label_OHLabourHoursInfo = (Label)FormView_MOHS_Form.FindControl("Label_OHLabourHoursInfo");
                  Label_OHLabourHoursInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6120:
                  Label Label_OHPatientSatisfactionScoreInfo = (Label)FormView_MOHS_Form.FindControl("Label_OHPatientSatisfactionScoreInfo");
                  Label_OHPatientSatisfactionScoreInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6121:
                  Label Label_OHPatientSatisfactionResponseRateInfo = (Label)FormView_MOHS_Form.FindControl("Label_OHPatientSatisfactionResponseRateInfo");
                  Label_OHPatientSatisfactionResponseRateInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6069:
                  Label Label_CCComplaintsReceivedInfo = (Label)FormView_MOHS_Form.FindControl("Label_CCComplaintsReceivedInfo");
                  Label_CCComplaintsReceivedInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6070:
                  Label Label_CCCCNReceivedPositiveInfo = (Label)FormView_MOHS_Form.FindControl("Label_CCCCNReceivedPositiveInfo");
                  Label_CCCCNReceivedPositiveInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6071:
                  Label Label_CCCCNReceivedSuggestionsInfo = (Label)FormView_MOHS_Form.FindControl("Label_CCCCNReceivedSuggestionsInfo");
                  Label_CCCCNReceivedSuggestionsInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6072:
                  Label Label_CCCCNReceivedNegativeInfo = (Label)FormView_MOHS_Form.FindControl("Label_CCCCNReceivedNegativeInfo");
                  Label_CCCCNReceivedNegativeInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6073:
                  Label Label_CCCCNReceivedTotalInfo = (Label)FormView_MOHS_Form.FindControl("Label_CCCCNReceivedTotalInfo");
                  Label_CCCCNReceivedTotalInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6074:
                  Label Label_CCTotalComplaintsInfo = (Label)FormView_MOHS_Form.FindControl("Label_CCTotalComplaintsInfo");
                  Label_CCTotalComplaintsInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6937:
                  Label Label_CareTotalStaffTrainedInfo = (Label)FormView_MOHS_Form.FindControl("Label_CareTotalStaffTrainedInfo");
                  Label_CareTotalStaffTrainedInfo.Text = Session["ListItem_Name"].ToString();
                  break;
                case 6938:
                  Label Label_CareTotalStaffEmployedInfo = (Label)FormView_MOHS_Form.FindControl("Label_CareTotalStaffEmployedInfo");
                  Label_CareTotalStaffEmployedInfo.Text = Session["ListItem_Name"].ToString();
                  break;
              }
            }
          }
        }
      }

      Session["ListItem_Id"] = "";
      Session["ListItem_Name"] = "";


      if (FormView_MOHS_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive")).Attributes.Add("OnKeyUp", "Calculation_CC_CCNReceivedPositive_Calculated();Calculate_CC_CCNReceivedTotal();");
        ((TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive")).Attributes.Add("OnInput", "Calculation_CC_CCNReceivedPositive_Calculated();Calculate_CC_CCNReceivedTotal();");
        ((TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions")).Attributes.Add("OnKeyUp", "Calculation_CC_CCNReceivedSuggestions_Calculated();Calculate_CC_CCNReceivedTotal();");
        ((TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions")).Attributes.Add("OnInput", "Calculation_CC_CCNReceivedSuggestions_Calculated();Calculate_CC_CCNReceivedTotal();");
        ((TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative")).Attributes.Add("OnKeyUp", "Calculation_CCNReceivedNegative_Calculated();Calculate_CC_CCNReceivedTotal();Calculation_CC_TotalComplaints();");
        ((TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative")).Attributes.Add("OnInput", "Calculation_CCNReceivedNegative_Calculated();Calculate_CC_CCNReceivedTotal();Calculation_CC_TotalComplaints();");
        ((TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived")).Attributes.Add("OnKeyUp", "Calculation_CC_ComplaintsReceived_Calculated();Calculation_CC_TotalComplaints();");
        ((TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived")).Attributes.Add("OnInput", "Calculation_CC_ComplaintsReceived_Calculated();Calculation_CC_TotalComplaints();");
      }
    }


    private void BeingModifiedUpdate(string BeingModifiedStatus)
    {
      if (BeingModifiedStatus == "Lock")
      {
        string SQLStringUpdateMOHS = "UPDATE Form_MonthlyOccupationalHealthStatistics SET MOHS_BeingModified = @MOHS_BeingModified, MOHS_BeingModifiedDate = @MOHS_BeingModifiedDate, MOHS_BeingModifiedBy = @MOHS_BeingModifiedBy WHERE MOHS_Id = @MOHS_Id";
        using (SqlCommand SqlCommand_UpdateMOHS = new SqlCommand(SQLStringUpdateMOHS))
        {
          SqlCommand_UpdateMOHS.Parameters.AddWithValue("@MOHS_BeingModified", true);
          SqlCommand_UpdateMOHS.Parameters.AddWithValue("@MOHS_BeingModifiedDate", DateTime.Now.ToString());
          SqlCommand_UpdateMOHS.Parameters.AddWithValue("@MOHS_BeingModifiedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_UpdateMOHS.Parameters.AddWithValue("@MOHS_Id", Request.QueryString["MOHS_Id"]);
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateMOHS);
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "LockedRecord", "<script language='javascript'>LockedRecord()</script>");
      }
      else if (BeingModifiedStatus == "Unlock")
      {
        if (MOHSBeingModified() == "1")
        {
          string SQLStringUpdateMOHS = "UPDATE Form_MonthlyOccupationalHealthStatistics SET MOHS_BeingModified = @MOHS_BeingModified, MOHS_BeingModifiedDate = @MOHS_BeingModifiedDate, MOHS_BeingModifiedBy = @MOHS_BeingModifiedBy WHERE MOHS_Id = @MOHS_Id";
          using (SqlCommand SqlCommand_UpdateMOHS = new SqlCommand(SQLStringUpdateMOHS))
          {
            SqlCommand_UpdateMOHS.Parameters.AddWithValue("@MOHS_BeingModified", false);
            SqlCommand_UpdateMOHS.Parameters.AddWithValue("@MOHS_BeingModifiedDate", DBNull.Value);
            SqlCommand_UpdateMOHS.Parameters.AddWithValue("@MOHS_BeingModifiedBy", DBNull.Value);
            SqlCommand_UpdateMOHS.Parameters.AddWithValue("@MOHS_Id", Request.QueryString["MOHS_Id"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateMOHS);
          }
        }
      }
    }

    private string MOHSBeingModified()
    {
      string BeingModifiedAllow = "0";

      Session["MOHSBeingModified"] = "";
      string SQLStringMOHSBeingModified = "SELECT MOHS_BeingModified FROM Form_MonthlyOccupationalHealthStatistics WHERE MOHS_Id = @MOHS_Id AND (MOHS_BeingModifiedBy = @MOHS_BeingModifiedBy OR MOHS_BeingModifiedBy IS NULL)";
      using (SqlCommand SqlCommand_MOHSBeingModified = new SqlCommand(SQLStringMOHSBeingModified))
      {
        SqlCommand_MOHSBeingModified.Parameters.AddWithValue("@MOHS_Id", Request.QueryString["MOHS_Id"]);
        SqlCommand_MOHSBeingModified.Parameters.AddWithValue("@MOHS_BeingModifiedBy", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_MOHSBeingModified;
        using (DataTable_MOHSBeingModified = new DataTable())
        {
          DataTable_MOHSBeingModified.Locale = CultureInfo.CurrentCulture;
          DataTable_MOHSBeingModified = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MOHSBeingModified).Copy();
          if (DataTable_MOHSBeingModified.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MOHSBeingModified.Rows)
            {
              Session["MOHSBeingModified"] = DataRow_Row["MOHS_BeingModified"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["MOHSBeingModified"].ToString()))
      {
        BeingModifiedAllow = "1";
      }
      else
      {
        BeingModifiedAllow = "0";
      }

      Session["MOHSBeingModified"] = "";

      return BeingModifiedAllow;
    }

    private string MOHSCutOff()
    {
      string CutOffAllow = "0";

      Session["MOHSPeriodStart"] = "";
      Session["MOHSPeriodEnd"] = "";
      string SQLStringMOHSPeriod = "SELECT MOHS_PeriodDate AS MOHS_PeriodStart ,CAST(DATEADD(DAY, (SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,MOHS_PeriodDate)+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 46) - 1, MOHS_PeriodDate) AS DATETIME) AS MOHS_PeriodEnd FROM (SELECT DATEADD(MONTH,1, CAST(LEFT(MOHS_Period,7) + '/01' AS DATETIME)) AS MOHS_PeriodDate FROM Form_MonthlyOccupationalHealthStatistics WHERE MOHS_Id = @MOHS_Id) AS TempTable";
      using (SqlCommand SqlCommand_MOHSPeriod = new SqlCommand(SQLStringMOHSPeriod))
      {
        SqlCommand_MOHSPeriod.Parameters.AddWithValue("@MOHS_Id", Request.QueryString["MOHS_Id"]);
        DataTable DataTable_MOHSPeriod;
        using (DataTable_MOHSPeriod = new DataTable())
        {
          DataTable_MOHSPeriod.Locale = CultureInfo.CurrentCulture;
          DataTable_MOHSPeriod = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MOHSPeriod).Copy();
          if (DataTable_MOHSPeriod.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_MOHSPeriod.Rows)
            {
              Session["MOHSPeriodStart"] = DataRow_Row["MOHS_PeriodStart"];
              Session["MOHSPeriodEnd"] = DataRow_Row["MOHS_PeriodEnd"];
            }
          }
        }
      }


      if (string.IsNullOrEmpty(Session["MOHSPeriodStart"].ToString()) || string.IsNullOrEmpty(Session["MOHSPeriodEnd"].ToString()))
      {
        CutOffAllow = "0";
      }
      else
      {
        DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
        DateTime MOHSPeriodStart = Convert.ToDateTime(Session["MOHSPeriodStart"].ToString(), CultureInfo.CurrentCulture);
        DateTime MOHSPeriodEnd = Convert.ToDateTime(Session["MOHSPeriodEnd"].ToString(), CultureInfo.CurrentCulture);

        if ((CurrentDate.CompareTo(MOHSPeriodStart) >= 0) && (CurrentDate.CompareTo(MOHSPeriodEnd) <= 0))
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
      string SearchField2 = Request.QueryString["Search_UnitId"];
      string SearchField3 = Request.QueryString["Search_MOHSPeriod"];
      string SearchField4 = Request.QueryString["Search_MOHSFYPeriod"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Unit_Id=" + Request.QueryString["Search_UnitId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_MOHS_Period=" + Request.QueryString["Search_MOHSPeriod"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_MOHS_FYPeriod=" + Request.QueryString["Search_MOHSFYPeriod"] + "&";
      }

      string FinalURL = "Form_MonthlyOccupationalHealthStatistics_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics List", FinalURL);

      BeingModifiedUpdate("Unlock");

      Response.Redirect(FinalURL, false);
    }

    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }


    //--START-- --TableForm--//
    protected void FormView_MOHS_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDMOHSModifiedDate"] = e.OldValues["MOHS_ModifiedDate"];
        object OLDMOHSModifiedDate = Session["OLDMOHSModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDMOHSModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareMOHS = (DataView)SqlDataSource_MOHS_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareMOHS = DataView_CompareMOHS[0];
        Session["DBMOHSModifiedDate"] = Convert.ToString(DataRowView_CompareMOHS["MOHS_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBMOHSModifiedBy"] = Convert.ToString(DataRowView_CompareMOHS["MOHS_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBMOHSModifiedDate = Session["DBMOHSModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBMOHSModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_ConcurrencyUpdate = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBMOHSModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_MOHS_Form.FindControl("Label_InvalidForm")).Text = "";
          ((Label)FormView_MOHS_Form.FindControl("Label_ConcurrencyUpdate")).Text = Label_ConcurrencyUpdate;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_InvalidForm = EditValidation();

          if (!string.IsNullOrEmpty(Label_InvalidForm))
          {
            e.Cancel = true;

            ((Label)FormView_MOHS_Form.FindControl("Label_InvalidForm")).Text = Label_InvalidForm;
            ((Label)FormView_MOHS_Form.FindControl("Label_ConcurrencyUpdate")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_InvalidForm))
          {
            e.Cancel = false;

            e.NewValues["MOHS_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["MOHS_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_MonthlyOccupationalHealthStatistics", "MOHS_Id = " + Request.QueryString["MOHS_Id"]);

            DataView DataView_MOHS = (DataView)SqlDataSource_MOHS_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_MOHS = DataView_MOHS[0];
            Session["MOHSHistory"] = Convert.ToString(DataRowView_MOHS["MOHS_History"], CultureInfo.CurrentCulture);

            Session["MOHSHistory"] = Session["History"].ToString() + Session["MOHSHistory"].ToString();
            e.NewValues["MOHS_History"] = Session["MOHSHistory"].ToString();

            Session["MOHSHistory"] = "";
            Session["History"] = "";


            TextBox TextBox_EditOH_ClinicVisits = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_ClinicVisits");
            TextBox TextBox_EditOH_LabourHours = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_LabourHours");
            TextBox TextBox_EditOH_PatientSatisfactionScore = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_PatientSatisfactionScore");
            TextBox TextBox_EditOH_PatientSatisfactionResponseRate = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_PatientSatisfactionResponseRate");

            e.NewValues["MOHS_OH_ClinicVisits"] = TextBox_EditOH_ClinicVisits.Text;
            e.NewValues["MOHS_OH_LabourHours"] = TextBox_EditOH_LabourHours.Text;
            e.NewValues["MOHS_OH_PatientSatisfactionScore"] = TextBox_EditOH_PatientSatisfactionScore.Text;
            e.NewValues["MOHS_OH_PatientSatisfactionResponseRate"] = TextBox_EditOH_PatientSatisfactionResponseRate.Text;


            decimal CCCCNReceivedPositive = 0;
            decimal CCCCNReceivedPositive_Published = 0;
            decimal CCCCNReceivedPositive_Calculated = 0;
            TextBox TextBox_EditCC_CCNReceivedPositive = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive");
            TextBox TextBox_EditCC_CCNReceivedPositive_Published = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Published");
            TextBox TextBox_EditCC_CCNReceivedPositive_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Calculated");
            if (!string.IsNullOrEmpty(TextBox_EditCC_CCNReceivedPositive.Text))
            {
              CCCCNReceivedPositive = Convert.ToDecimal(TextBox_EditCC_CCNReceivedPositive.Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(TextBox_EditCC_CCNReceivedPositive_Published.Text))
            {
              CCCCNReceivedPositive_Published = Convert.ToDecimal(TextBox_EditCC_CCNReceivedPositive_Published.Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(TextBox_EditCC_CCNReceivedPositive_Calculated.Text))
            {
              CCCCNReceivedPositive_Calculated = Convert.ToDecimal(TextBox_EditCC_CCNReceivedPositive_Calculated.Text, CultureInfo.CurrentCulture);
            }

            CCCCNReceivedPositive_Calculated = CCCCNReceivedPositive + CCCCNReceivedPositive_Published;

            e.NewValues["MOHS_CC_CCNReceivedPositive"] = TextBox_EditCC_CCNReceivedPositive.Text;
            e.NewValues["MOHS_CC_CCNReceivedPositive_Calculated"] = CCCCNReceivedPositive_Calculated.ToString(CultureInfo.CurrentCulture);


            decimal CCCCNReceivedSuggestions = 0;
            decimal CCCCNReceivedSuggestions_Published = 0;
            decimal CCCCNReceivedSuggestions_Calculated = 0;
            TextBox TextBox_EditCC_CCNReceivedSuggestions = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions");
            TextBox TextBox_EditCC_CCNReceivedSuggestions_Published = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Published");
            TextBox TextBox_EditCC_CCNReceivedSuggestions_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Calculated");
            if (!string.IsNullOrEmpty(TextBox_EditCC_CCNReceivedSuggestions.Text))
            {
              CCCCNReceivedSuggestions = Convert.ToDecimal(TextBox_EditCC_CCNReceivedSuggestions.Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(TextBox_EditCC_CCNReceivedSuggestions_Published.Text))
            {
              CCCCNReceivedSuggestions_Published = Convert.ToDecimal(TextBox_EditCC_CCNReceivedSuggestions_Published.Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(TextBox_EditCC_CCNReceivedSuggestions_Calculated.Text))
            {
              CCCCNReceivedSuggestions_Calculated = Convert.ToDecimal(TextBox_EditCC_CCNReceivedSuggestions_Calculated.Text, CultureInfo.CurrentCulture);
            }

            CCCCNReceivedSuggestions_Calculated = CCCCNReceivedSuggestions + CCCCNReceivedSuggestions_Published;

            e.NewValues["MOHS_CC_CCNReceivedSuggestions"] = TextBox_EditCC_CCNReceivedSuggestions.Text;
            e.NewValues["MOHS_CC_CCNReceivedSuggestions_Calculated"] = CCCCNReceivedSuggestions_Calculated.ToString(CultureInfo.CurrentCulture);


            decimal CCCCNReceivedNegative = 0;
            decimal CCCCNReceivedNegative_Published = 0;
            decimal CCCCNReceivedNegative_Calculated = 0;
            TextBox TextBox_EditCC_CCNReceivedNegative = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative");
            TextBox TextBox_EditCC_CCNReceivedNegative_Published = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Published");
            TextBox TextBox_EditCC_CCNReceivedNegative_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Calculated");
            if (!string.IsNullOrEmpty(TextBox_EditCC_CCNReceivedNegative.Text))
            {
              CCCCNReceivedNegative = Convert.ToDecimal(TextBox_EditCC_CCNReceivedNegative.Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(TextBox_EditCC_CCNReceivedNegative_Published.Text))
            {
              CCCCNReceivedNegative_Published = Convert.ToDecimal(TextBox_EditCC_CCNReceivedNegative_Published.Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(TextBox_EditCC_CCNReceivedNegative_Calculated.Text))
            {
              CCCCNReceivedNegative_Calculated = Convert.ToDecimal(TextBox_EditCC_CCNReceivedNegative_Calculated.Text, CultureInfo.CurrentCulture);
            }

            CCCCNReceivedNegative_Calculated = CCCCNReceivedNegative + CCCCNReceivedNegative_Published;

            e.NewValues["MOHS_CC_CCNReceivedNegative"] = TextBox_EditCC_CCNReceivedNegative.Text;
            e.NewValues["MOHS_CC_CCNReceivedNegative_Calculated"] = CCCCNReceivedNegative_Calculated.ToString(CultureInfo.CurrentCulture);


            decimal CCComplaintsReceived = 0;
            decimal CCComplaintsReceived_Published = 0;
            decimal CCComplaintsReceived_Calculated = 0;
            TextBox TextBox_EditCC_ComplaintsReceived = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived");
            TextBox TextBox_EditCC_ComplaintsReceived_Published = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Published");
            TextBox TextBox_EditCC_ComplaintsReceived_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Calculated");
            if (!string.IsNullOrEmpty(TextBox_EditCC_ComplaintsReceived.Text))
            {
              CCComplaintsReceived = Convert.ToDecimal(TextBox_EditCC_ComplaintsReceived.Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(TextBox_EditCC_ComplaintsReceived_Published.Text))
            {
              CCComplaintsReceived_Published = Convert.ToDecimal(TextBox_EditCC_ComplaintsReceived_Published.Text, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrEmpty(TextBox_EditCC_ComplaintsReceived_Calculated.Text))
            {
              CCComplaintsReceived_Calculated = Convert.ToDecimal(TextBox_EditCC_ComplaintsReceived_Calculated.Text, CultureInfo.CurrentCulture);
            }

            CCComplaintsReceived_Calculated = CCComplaintsReceived + CCComplaintsReceived_Published;

            e.NewValues["MOHS_CC_ComplaintsReceived"] = TextBox_EditCC_ComplaintsReceived.Text;
            e.NewValues["MOHS_CC_ComplaintsReceived_Calculated"] = CCComplaintsReceived_Calculated.ToString(CultureInfo.CurrentCulture);


            decimal CCCCNReceivedTotalTotal = 0;
            CCCCNReceivedTotalTotal = CCCCNReceivedPositive_Calculated + CCCCNReceivedSuggestions_Calculated + CCCCNReceivedNegative_Calculated;
            e.NewValues["MOHS_CC_CCNReceivedTotal"] = CCCCNReceivedTotalTotal.ToString(CultureInfo.CurrentCulture);


            decimal CCTotalComplaintsTotal = 0;
            CCTotalComplaintsTotal = CCCCNReceivedNegative_Calculated + CCComplaintsReceived_Calculated;
            e.NewValues["MOHS_CC_TotalComplaints"] = CCTotalComplaintsTotal.ToString(CultureInfo.CurrentCulture);


            TextBox TextBox_EditCare_TotalStaffTrained = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCare_TotalStaffTrained");
            TextBox TextBox_EditCare_TotalStaffEmployed = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCare_TotalStaffEmployed");

            e.NewValues["MOHS_Care_TotalStaffTrained"] = TextBox_EditCare_TotalStaffTrained.Text;
            e.NewValues["MOHS_Care_TotalStaffEmployed"] = TextBox_EditCare_TotalStaffEmployed.Text;
          }
        }

        Session["OLDMOHSModifiedDate"] = "";
        Session["DBMOHSModifiedDate"] = "";
        Session["DBMOHSModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditOH_ClinicVisits = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_ClinicVisits");
      TextBox TextBox_EditOH_LabourHours = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_LabourHours");
      TextBox TextBox_EditOH_PatientSatisfactionScore = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_PatientSatisfactionScore");
      TextBox TextBox_EditOH_PatientSatisfactionResponseRate = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditOH_PatientSatisfactionResponseRate");
      TextBox TextBox_EditCC_CCNReceivedPositive = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive");
      TextBox TextBox_EditCC_CCNReceivedPositive_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedPositive_Calculated");
      TextBox TextBox_EditCC_CCNReceivedSuggestions = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions");
      TextBox TextBox_EditCC_CCNReceivedSuggestions_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedSuggestions_Calculated");
      TextBox TextBox_EditCC_CCNReceivedNegative = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative");
      TextBox TextBox_EditCC_CCNReceivedNegative_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedNegative_Calculated");
      TextBox TextBox_EditCC_CCNReceivedTotal = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_CCNReceivedTotal");
      TextBox TextBox_EditCC_ComplaintsReceived = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived");
      TextBox TextBox_EditCC_ComplaintsReceived_Calculated = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_ComplaintsReceived_Calculated");
      TextBox TextBox_EditCC_TotalComplaints = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCC_TotalComplaints");
      TextBox TextBox_EditCare_TotalStaffTrained = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCare_TotalStaffTrained");
      TextBox TextBox_EditCare_TotalStaffEmployed = (TextBox)FormView_MOHS_Form.FindControl("TextBox_EditCare_TotalStaffEmployed");

      if (InvalidForm == "No")
      {
        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOH_ClinicVisits.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Total Clinic Visists is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOH_LabourHours.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Total Labour Hours is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOH_PatientSatisfactionScore.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Patient Satisfaction Score is not in the correct format<br />";
          InvalidForm = "Yes";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditOH_PatientSatisfactionResponseRate.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Patient Satisfaction Response Rate is not in the correct format<br />";
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

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(TextBox_EditCare_TotalStaffEmployed.Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Total number of employed staff for the month is not in the correct format<br />";
          InvalidForm = "Yes";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_MOHS_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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

            ScriptManager.RegisterStartupScript(UpdatePanel_MOHS, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics Print", "InfoQuest_Print.aspx?PrintPage=Form_MonthlyOccupationalHealthStatistics&PrintValue=" + Request.QueryString["MOHS_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_MOHS, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_MOHS, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Monthly Occupational Health Statistics Email", "InfoQuest_Email.aspx?EmailPage=Form_MonthlyOccupationalHealthStatistics&EmailValue=" + Request.QueryString["MOHS_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_MOHS, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }


    protected void FormView_MOHS_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["MOHS_Id"] != null)
          {
            RedirectToList();
          }
        }
      }
    }

    protected void FormView_MOHS_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_MOHS_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["MOHS_Id"] != null)
        {
          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 46";
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
            ((Button)FormView_MOHS_Form.FindControl("Button_EditEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_MOHS_Form.FindControl("Button_EditEmail")).Visible = true;
          }

          if (Print == "False")
          {
            ((Button)FormView_MOHS_Form.FindControl("Button_EditPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_MOHS_Form.FindControl("Button_EditPrint")).Visible = true;
          }

          Email = "";
          Print = "";
        }
      }

      if (FormView_MOHS_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["MOHS_Id"] != null)
        {
          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 46";
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
            ((Button)FormView_MOHS_Form.FindControl("Button_ItemEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_MOHS_Form.FindControl("Button_ItemEmail")).Visible = true;
            ((Button)FormView_MOHS_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('InfoQuest_Email.aspx?EmailPage=Form_MonthlyOccupationalHealthStatistics&EmailValue=" + Request.QueryString["MOHS_Id"] + "')";
          }

          if (Print == "False")
          {
            ((Button)FormView_MOHS_Form.FindControl("Button_ItemPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_MOHS_Form.FindControl("Button_ItemPrint")).Visible = true;
            ((Button)FormView_MOHS_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('InfoQuest_Print.aspx?PrintPage=Form_MonthlyOccupationalHealthStatistics&PrintValue=" + Request.QueryString["MOHS_Id"] + "')";
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