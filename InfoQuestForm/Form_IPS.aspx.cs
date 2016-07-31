using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace InfoQuestForm
{
  public partial class Form_IPS : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected bool Button_EditUpdateClicked = false;
    protected bool Button_EditPrintClicked = false;
    protected bool Button_EditEmailClicked = false;

    protected Dictionary<string, string> FileContentTypeHandler = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_IPS, this.GetType(), "UpdateProgress_Start", "Validation_Search();Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          PageTitle();

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_IPS_VisitInformation_VisitNumber"] != null)
          {
            SqlDataSource_IPS_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
            SqlDataSource_IPS_Facility.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_VisitInformation";
            SqlDataSource_IPS_Facility.SelectParameters["TableWHERE"].DefaultValue = "Facility_Id = " + Request.QueryString["s_Facility_Id"] + " AND IPS_VisitInformation_VisitNumber = " + Request.QueryString["s_IPS_VisitInformation_VisitNumber"] + " ";

            Label_InvalidSearchMessage.Text = "";
            TableVisitInfo.Visible = false;
            TableInfection.Visible = false;
            TableCurrentInfection.Visible = false; DivCurrentInfection.Visible = false;
            TableTheatre.Visible = false; DivTheatre.Visible = false;
            TableVisitDiagnosis.Visible = false; DivVisitDiagnosis.Visible = false;
            TableBedHistory.Visible = false; DivBedHistory.Visible = false;
            TableSpecimen.Visible = false; DivSpecimen.Visible = false;
            TableCurrentInfectionComplete.Visible = false; DivCurrentInfectionComplete.Visible = false;
            TableFile.Visible = false; DivFile.Visible = false;
            TableFileList.Visible = false;

            VisitData();
          }
          else
          {
            if (Request.QueryString["IPSVisitInformationId"] == null)
            {
              form_IPS.DefaultButton = Button_Search.UniqueID;

              Label_InvalidSearchMessage.Text = "";
              TableVisitInfo.Visible = false;
              TableInfection.Visible = false;
              TableCurrentInfection.Visible = false; DivCurrentInfection.Visible = false;
              TableTheatre.Visible = false; DivTheatre.Visible = false;
              TableVisitDiagnosis.Visible = false; DivVisitDiagnosis.Visible = false;
              TableBedHistory.Visible = false; DivBedHistory.Visible = false;
              TableSpecimen.Visible = false; DivSpecimen.Visible = false;
              TableCurrentInfectionComplete.Visible = false; DivCurrentInfectionComplete.Visible = false;
              TableFile.Visible = false; DivFile.Visible = false;
              TableFileList.Visible = false;
            }
            else
            {
              SqlDataSource_IPS_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
              SqlDataSource_IPS_Facility.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_VisitInformation";
              SqlDataSource_IPS_Facility.SelectParameters["TableWHERE"].DefaultValue = "IPS_VisitInformation_Id = " + Request.QueryString["IPSVisitInformationId"] + " ";

              TableVisitInfo.Visible = true;
              TableInfection.Visible = true;

              if (Request.QueryString["IPSInfectionId"] == null)
              {
                FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
                DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
                DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
                DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
                DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
                DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

                string Security = "1";
                if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
                {
                  Security = "0";
                  TableCurrentInfection.Visible = true; DivCurrentInfection.Visible = true;
                  TableTheatre.Visible = false; DivTheatre.Visible = false;
                  TableVisitDiagnosis.Visible = false; DivVisitDiagnosis.Visible = false;
                  TableBedHistory.Visible = false; DivBedHistory.Visible = false;
                  TableSpecimen.Visible = false; DivSpecimen.Visible = false;
                  TableCurrentInfectionComplete.Visible = false; DivCurrentInfectionComplete.Visible = false;
                  TableFile.Visible = false; DivFile.Visible = false; 
                  TableFileList.Visible = false;
                }

                if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
                {
                  Security = "0";
                  TableCurrentInfection.Visible = false; DivCurrentInfection.Visible = false;
                  TableTheatre.Visible = false; DivTheatre.Visible = false;
                  TableVisitDiagnosis.Visible = false; DivVisitDiagnosis.Visible = false;
                  TableBedHistory.Visible = false; DivBedHistory.Visible = false;
                  TableSpecimen.Visible = false; DivSpecimen.Visible = false;
                  TableCurrentInfectionComplete.Visible = false; DivCurrentInfectionComplete.Visible = false;
                  TableFile.Visible = false; DivFile.Visible = false;
                  TableFileList.Visible = false;
                }

                if (Security == "1")
                {
                  Security = "0";
                  TableCurrentInfection.Visible = false; DivCurrentInfection.Visible = false;
                  TableTheatre.Visible = false; DivTheatre.Visible = false;
                  TableVisitDiagnosis.Visible = false; DivVisitDiagnosis.Visible = false;
                  TableBedHistory.Visible = false; DivBedHistory.Visible = false;
                  TableSpecimen.Visible = false; DivSpecimen.Visible = false;
                  TableCurrentInfectionComplete.Visible = false; DivCurrentInfectionComplete.Visible = false;
                  TableFile.Visible = false; DivFile.Visible = false;
                  TableFileList.Visible = false;
                }

                if (TableCurrentInfection.Visible == true)
                {
                  DropDownList DropDownList_InsertInfectionUnitId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionUnitId");

                  FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
                  string FacilityId = FromDataBase_FacilityId_Current.FacilityId;

                  DropDownList_InsertInfectionUnitId.Items.Clear();
                  SqlDataSource_IPS_InsertInfectionUnitId.SelectParameters["Facility_Id"].DefaultValue = FacilityId;
                  DropDownList_InsertInfectionUnitId.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
                  DropDownList_InsertInfectionUnitId.DataBind();
                }
              }
              else
              {
                TableCurrentInfection.Visible = true; DivCurrentInfection.Visible = true;

                SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Infection_Category_List";
                SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection";
                SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";

                SqlDataSource_IPS_EditInfectionTypeList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Infection_Type_List";
                SqlDataSource_IPS_EditInfectionTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection";
                SqlDataSource_IPS_EditInfectionTypeList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";

                SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Infection_SubType_List";
                SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection";
                SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";

                SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Infection_SubSubType_List";
                SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection";
                SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";

                SqlDataSource_IPS_EditInfectionSiteList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Infection_Site_List";
                SqlDataSource_IPS_EditInfectionSiteList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection";
                SqlDataSource_IPS_EditInfectionSiteList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";

                SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Infection_ScreeningReason_List";
                SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection";
                SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";

                SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
                SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
                SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection";
                SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";

                SetCurrentInfectionVisibility();

                TableCurrentInfectionComplete.Visible = true; DivCurrentInfectionComplete.Visible = true;

                TableTheatre.Visible = true; DivTheatre.Visible = true;
                TableVisitDiagnosis.Visible = true; DivVisitDiagnosis.Visible = true;
                TableBedHistory.Visible = true; DivBedHistory.Visible = true;
                TableSpecimen.Visible = true; DivSpecimen.Visible = true;
                TableFile.Visible = true; DivFile.Visible = true;

                FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
                string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
                string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

                FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
                string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

                FromDataBase_HAICompleted FromDataBase_HAICompleted_Current = GetHAICompleted();
                string IPSHAICompleted = FromDataBase_HAICompleted_Current.IPSHAICompleted;

                if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True" && IPSInfectionSiteInfectionIsActive == "True")
                {
                  DivFile.Visible = true;
                  TableFile.Visible = true; 
                  TableFileList.Visible = false;

                  AntibioticData();
                  TheatreData();
                  VisitDiagnosisData();
                  BedHistoryData();
                }
                else
                {
                  if (IPSInfectionIsActive == "True")
                  {
                    if (IPSHAICompleted == "False")
                    {
                      DivFile.Visible = true;
                      TableFile.Visible = true;
                      TableFileList.Visible = false;
                    }
                    else
                    {
                      DivFile.Visible = true;
                      TableFile.Visible = false;
                      TableFileList.Visible = true;
                    }
                  }
                  else
                  {
                    DivFile.Visible = true;
                    TableFile.Visible = false;
                    TableFileList.Visible = true;
                  }
                }

                FileRegisterPostBackControl();
              }
            }
          }

          TableVisible();
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
        if (Request.QueryString["s_Facility_Id"] == null && Request.QueryString["IPSVisitInformationId"] == null)
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37'))";
        }
        else
        {
          if (Request.QueryString["s_Facility_Id"] != null)
          {
            SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
          }

          if (Request.QueryString["IPSVisitInformationId"] != null)
          {
            SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
          }
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_Security.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          SecurityAllow = "0";
          Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("37");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_IPS.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infection Prevention Surveillance", "5");
      }
    }
    
    private void SqlDataSourceSetup()
    {
      SqlDataSource_IPS_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_IPS_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Facility.SelectParameters.Clear();
      SqlDataSource_IPS_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_IPS_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_IPS_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_Infection_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Infection_List.SelectCommand = "spForm_Get_IPS_Infection_List";
      SqlDataSource_IPS_Infection_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Infection_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_Infection_List.SelectParameters.Clear();
      SqlDataSource_IPS_Infection_List.SelectParameters.Add("IPS_VisitInformation_Id", TypeCode.String, Request.QueryString["IPSVisitInformationId"]);

      SqlDataSource_IPS_Theatre_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Theatre_List.SelectCommand = "spForm_Get_IPS_Theatre_List";
      SqlDataSource_IPS_Theatre_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Theatre_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_Theatre_List.SelectParameters.Clear();
      SqlDataSource_IPS_Theatre_List.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSource_IPS_VisitDiagnosis_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_VisitDiagnosis_List.SelectCommand = "spForm_Get_IPS_VisitDiagnosis_List";
      SqlDataSource_IPS_VisitDiagnosis_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_VisitDiagnosis_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_VisitDiagnosis_List.SelectParameters.Clear();
      SqlDataSource_IPS_VisitDiagnosis_List.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSource_IPS_BedHistory_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_BedHistory_List.SelectCommand = "spForm_Get_IPS_BedHistory_List";
      SqlDataSource_IPS_BedHistory_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_BedHistory_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_BedHistory_List.SelectParameters.Clear();
      SqlDataSource_IPS_BedHistory_List.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSource_IPS_Specimen_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Specimen_List.SelectCommand = "spForm_Get_IPS_Specimen_List";
      SqlDataSource_IPS_Specimen_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Specimen_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_Specimen_List.SelectParameters.Clear();
      SqlDataSource_IPS_Specimen_List.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSource_IPS_File.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_File.SelectCommand = "SELECT IPS_File_Id , IPS_File_Name , IPS_File_CreatedDate , IPS_File_CreatedBy FROM Form_IPS_File WHERE IPS_Infection_Id = @IPS_Infection_Id ORDER BY IPS_File_Name";
      SqlDataSource_IPS_File.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_File.SelectParameters.Clear();
      SqlDataSource_IPS_File.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSource_IPS_File_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_File_List.SelectCommand = "SELECT IPS_File_Id , IPS_File_Name , IPS_File_CreatedDate , IPS_File_CreatedBy FROM Form_IPS_File WHERE IPS_Infection_Id = @IPS_Infection_Id ORDER BY IPS_File_Name";
      SqlDataSource_IPS_File_List.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_File_List.SelectParameters.Clear();
      SqlDataSource_IPS_File_List.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSourceSetup_Form();
    }

    private void SqlDataSourceSetup_Form()
    {
      SqlDataSource_IPS_Infection_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Infection_Form.InsertCommand = "INSERT INTO Form_IPS_Infection ( IPS_VisitInformation_Id , IPS_Infection_ReportNumber , IPS_Infection_Category_List , IPS_Infection_Type_List , IPS_Infection_SubType_List , IPS_Infection_SubSubType_List , IPS_Infection_Site_List , IPS_Infection_Site_Infection_Id , IPS_Infection_Screening , IPS_Infection_ScreeningReason_List , Unit_Id , IPS_Infection_Summary , IPS_Infection_Completed , IPS_Infection_CompletedDate , IPS_Infection_CreatedDate , IPS_Infection_CreatedBy , IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History , IPS_Infection_IsActive ) VALUES ( @IPS_VisitInformation_Id , @IPS_Infection_ReportNumber , @IPS_Infection_Category_List , @IPS_Infection_Type_List , @IPS_Infection_SubType_List , @IPS_Infection_SubSubType_List , @IPS_Infection_Site_List , @IPS_Infection_Site_Infection_Id , @IPS_Infection_Screening , @IPS_Infection_ScreeningReason_List , @Unit_Id , @IPS_Infection_Summary , @IPS_Infection_Completed , @IPS_Infection_CompletedDate , @IPS_Infection_CreatedDate , @IPS_Infection_CreatedBy , @IPS_Infection_ModifiedDate , @IPS_Infection_ModifiedBy , @IPS_Infection_History , @IPS_Infection_IsActive ); SELECT @IPS_Infection_Id = SCOPE_IDENTITY()";
      SqlDataSource_IPS_Infection_Form.SelectCommand = "SELECT * FROM Form_IPS_Infection WHERE (IPS_Infection_Id = @IPS_Infection_Id)";
      SqlDataSource_IPS_Infection_Form.UpdateCommand = "UPDATE Form_IPS_Infection SET IPS_Infection_Category_List = @IPS_Infection_Category_List , IPS_Infection_Type_List = @IPS_Infection_Type_List , IPS_Infection_SubType_List = @IPS_Infection_SubType_List , IPS_Infection_SubSubType_List = @IPS_Infection_SubSubType_List , IPS_Infection_Site_List = @IPS_Infection_Site_List , IPS_Infection_Site_Infection_Id = @IPS_Infection_Site_Infection_Id , IPS_Infection_Screening = @IPS_Infection_Screening , IPS_Infection_ScreeningReason_List = @IPS_Infection_ScreeningReason_List , Unit_Id = @Unit_Id , IPS_Infection_Summary = @IPS_Infection_Summary , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History , IPS_Infection_IsActive = @IPS_Infection_IsActive , IPS_Infection_RejectedReason = @IPS_Infection_RejectedReason WHERE [IPS_Infection_Id] = @IPS_Infection_Id";
      SqlDataSource_IPS_Infection_Form.InsertParameters.Clear();
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_VisitInformation_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_ReportNumber", TypeCode.String, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_Category_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_Type_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_SubType_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_SubSubType_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_Site_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_Site_Infection_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_Screening", TypeCode.Boolean, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_ScreeningReason_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_Summary", TypeCode.String, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_Completed", TypeCode.Boolean, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_CompletedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_CompletedDate"].ConvertEmptyStringToNull = true;
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_CreatedBy", TypeCode.String, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_History", TypeCode.String, "");
      SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_IPS_Infection_Form.InsertParameters.Add("IPS_Infection_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_Infection_Form.SelectParameters.Clear();
      SqlDataSource_IPS_Infection_Form.SelectParameters.Add("IPS_Infection_Id", TypeCode.Int32, Request.QueryString["IPSInfectionId"]);
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Clear();
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_Category_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_Type_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_SubType_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_SubSubType_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_Site_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_Site_Infection_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_Screening", TypeCode.Boolean, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_ScreeningReason_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_Summary", TypeCode.String, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_History", TypeCode.String, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_RejectedReason", TypeCode.String, "");
      SqlDataSource_IPS_Infection_Form.UpdateParameters.Add("IPS_Infection_Id", TypeCode.Int32, "");

      SqlDataSource_IPS_InsertInfectionCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertInfectionCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertInfectionCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertInfectionCategoryList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertInfectionCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertInfectionCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "119");
      SqlDataSource_IPS_InsertInfectionCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertInfectionCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertInfectionTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertInfectionTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertInfectionTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertInfectionTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertInfectionTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertInfectionTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "120");
      SqlDataSource_IPS_InsertInfectionTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertInfectionTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertInfectionSubTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "121");
      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertInfectionSubSubTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "122");
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertInfectionSiteList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertInfectionSiteList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertInfectionSiteList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertInfectionSiteList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertInfectionSiteList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertInfectionSiteList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "137");
      SqlDataSource_IPS_InsertInfectionSiteList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertInfectionSiteList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionSiteList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionSiteList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertInfectionSiteInfectionId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertInfectionSiteInfectionId.SelectCommand = "SELECT IPS_Infection_Id , IPS_Infection_ReportNumber FROM Form_IPS_Infection WHERE IPS_Infection_IsActive = 1 AND IPS_VisitInformation_Id = @IPS_VisitInformation_Id AND IPS_Infection_Site_List = CASE @IPS_Infection_Site_List WHEN '' THEN '' WHEN '4996' THEN '' WHEN '4997' THEN '4996' WHEN '4998' THEN '4997' END	AND IPS_Infection_Category_List IN (4799)";
      SqlDataSource_IPS_InsertInfectionSiteInfectionId.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_InsertInfectionSiteInfectionId.SelectParameters.Clear();
      SqlDataSource_IPS_InsertInfectionSiteInfectionId.SelectParameters.Add("IPS_VisitInformation_Id", TypeCode.String, Request.QueryString["IPSVisitInformationId"]);
      SqlDataSource_IPS_InsertInfectionSiteInfectionId.SelectParameters.Add("IPS_Infection_Site_List", TypeCode.String, "");

      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "140");
      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionScreeningTypeTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertInfectionScreeningReasonList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertInfectionScreeningReasonList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertInfectionScreeningReasonList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertInfectionScreeningReasonList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertInfectionScreeningReasonList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertInfectionScreeningReasonList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "141");
      SqlDataSource_IPS_InsertInfectionScreeningReasonList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertInfectionScreeningReasonList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionScreeningReasonList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionScreeningReasonList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertInfectionUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertInfectionUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_IPS_InsertInfectionUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertInfectionUnitId.SelectParameters.Clear();
      SqlDataSource_IPS_InsertInfectionUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_IPS_InsertInfectionUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertInfectionUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_IPS_InsertInfectionUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertInfectionUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditInfectionCategoryList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionCategoryList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditInfectionCategoryList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "119");
      SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionCategoryList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditInfectionTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditInfectionTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditInfectionTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditInfectionTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "120");
      SqlDataSource_IPS_EditInfectionTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditInfectionTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditInfectionSubTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionSubTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditInfectionSubTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "121");
      SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditInfectionSubSubTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "122");
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditInfectionSiteList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionSiteList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditInfectionSiteList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditInfectionSiteList.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionSiteList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditInfectionSiteList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "137");
      SqlDataSource_IPS_EditInfectionSiteList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditInfectionSiteList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionSiteList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionSiteList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditInfectionSiteInfectionId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionSiteInfectionId.SelectCommand = "SELECT IPS_Infection_Id , IPS_Infection_ReportNumber FROM Form_IPS_Infection WHERE IPS_Infection_IsActive = 1 AND IPS_VisitInformation_Id = @IPS_VisitInformation_Id AND IPS_Infection_Id != @IPS_Infection_Id AND IPS_Infection_Site_List = CASE @IPS_Infection_Site_List WHEN '' THEN '' WHEN '4996' THEN '' WHEN '4997' THEN '4996' WHEN '4998' THEN '4997' END AND IPS_Infection_Category_List IN (4799) UNION SELECT IPS_Infection_Site_Infection_Id , 'Linked Site Required' FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      SqlDataSource_IPS_EditInfectionSiteInfectionId.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_EditInfectionSiteInfectionId.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionSiteInfectionId.SelectParameters.Add("IPS_VisitInformation_Id", TypeCode.String, Request.QueryString["IPSVisitInformationId"]);
      SqlDataSource_IPS_EditInfectionSiteInfectionId.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);
      SqlDataSource_IPS_EditInfectionSiteInfectionId.SelectParameters.Add("IPS_Infection_Site_List", TypeCode.String, "");

      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "140");
      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionScreeningTypeTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditInfectionScreeningReasonList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "141");
      SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionScreeningReasonList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditInfectionUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditInfectionUnitId.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_IPS_EditInfectionUnitId.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Clear();
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_ItemInfectionScreeningType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_ItemInfectionScreeningType.SelectCommand = "SELECT DISTINCT IPS_ScreeningType_Type_Name FROM vForm_IPS_Infection_ScreeningType WHERE IPS_Infection_Id = @IPS_Infection_Id ORDER BY IPS_ScreeningType_Type_Name";
      SqlDataSource_IPS_ItemInfectionScreeningType.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_ItemInfectionScreeningType.SelectParameters.Clear();
      SqlDataSource_IPS_ItemInfectionScreeningType.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName("37"), CultureInfo.CurrentCulture);
      Label_SearchHeading.Text = Convert.ToString("Search " + InfoQuestWCF.InfoQuest_All.All_FormName("37").Replace(" Form", ""), CultureInfo.CurrentCulture);
      Label_VisitInfoHeading.Text = Convert.ToString("Visit Information", CultureInfo.CurrentCulture);
      Label_InfectionHeading.Text = Convert.ToString("Infections", CultureInfo.CurrentCulture);

      if (Request.QueryString["IPSInfectionId"] == null)
      {
        Label_CurrentInfectionHeading.Text = Convert.ToString("New Infection", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_CurrentInfectionHeading.Text = Convert.ToString("Selected Infection", CultureInfo.CurrentCulture);
      }

      Label_TheatreHeading.Text = Convert.ToString("Visit History (Not Compulsory)", CultureInfo.CurrentCulture);
      Label_VisitDiagnosisHeading.Text = Convert.ToString("Visit Diagnosis (Not Compulsory)", CultureInfo.CurrentCulture);
      Label_BedHistoryHeading.Text = Convert.ToString("Bed History (Not Compulsory)", CultureInfo.CurrentCulture);
      Label_SpecimenHeading.Text = Convert.ToString("Specimen (Compulsory)", CultureInfo.CurrentCulture);

      if (Request.QueryString["IPSInfectionId"] == null)
      {
        Label_CurrentInfectionCompleteHeading.Text = Convert.ToString("Selected Infection Completed", CultureInfo.CurrentCulture);
      }
      else
      {
        FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Request.QueryString["IPSInfectionId"]);
        string IsHAI = FromDataBase_IsHAI_Current.IsHAI;

        if (IsHAI == "True")
        {
          Label_CurrentInfectionCompleteHeading.Text = Convert.ToString("Selected Infection Completed and HAI Investigation", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_CurrentInfectionCompleteHeading.Text = Convert.ToString("Selected Infection Completed", CultureInfo.CurrentCulture);
        }        
      }
      
      Label_FileHeading.Text = Convert.ToString("Files", CultureInfo.CurrentCulture);
      Label_FileListHeading.Text = Convert.ToString("Files", CultureInfo.CurrentCulture);
    }

    private void SetFormQueryString()
    {
      if (Request.QueryString["s_Facility_Id"] == null && Request.QueryString["s_IPS_VisitInformation_VisitNumber"] == null && Request.QueryString["IPSVisitInformationId"] == null)
      {
        DropDownList_Facility.SelectedValue = "";
        TextBox_PatientVisitNumber.Text = "";
      }
      else
      {
        if (Request.QueryString["IPSVisitInformationId"] == null)
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_IPS_VisitInformation_VisitNumber"];
        }
        else
        {
          Session["FacilityId"] = "";
          Session["IPSVisitInformationVisitNumber"] = "";
          string SQLStringVisitInfo = "SELECT Facility_Id , IPS_VisitInformation_VisitNumber FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id";
          using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
          {
            SqlCommand_VisitInfo.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);
            DataTable DataTable_VisitInfo;
            using (DataTable_VisitInfo = new DataTable())
            {
              DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
              DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
              if (DataTable_VisitInfo.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
                {
                  Session["FacilityId"] = DataRow_Row["Facility_Id"];
                  Session["IPSVisitInformationVisitNumber"] = DataRow_Row["IPS_VisitInformation_VisitNumber"];
                }
              }
            }
          }

          DropDownList_Facility.SelectedValue = Session["FacilityId"].ToString();
          TextBox_PatientVisitNumber.Text = Session["IPSVisitInformationVisitNumber"].ToString();

          Session.Remove("FacilityId");
          Session.Remove("IPSVisitInformationVisitNumber");
        }
      }
    }

    protected void TableVisible()
    {
      if (TableVisitInfo.Visible == true)
      {
        TableVisitInfoVisible();
      }

      if (TableCurrentInfection.Visible == true)
      {
        TableCurrentInfectionVisible();
      }

      if (TableCurrentInfectionComplete.Visible == true)
      {
        TableCurrentInfectionCompleteVisible();
      }
    }


    private class FromDataBase_VisitInfo
    {
      public string FacilityId { get; set; }
      public string IPSVisitInformationVisitNumber { get; set; }
    }

    private FromDataBase_VisitInfo GetVisitInfo()
    {
      FromDataBase_VisitInfo DataBase_VisitInfo_New = new FromDataBase_VisitInfo();

      string SQLStringVisitInfo = "SELECT Facility_Id , IPS_VisitInformation_VisitNumber FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);
        DataTable DataTable_VisitInfo;
        using (DataTable_VisitInfo = new DataTable())
        {
          DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
          if (DataTable_VisitInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
            {
              DataBase_VisitInfo_New.FacilityId = DataRow_Row["Facility_Id"].ToString();
              DataBase_VisitInfo_New.IPSVisitInformationVisitNumber = DataRow_Row["IPS_VisitInformation_VisitNumber"].ToString();
            }
          }
        }
      }

      return DataBase_VisitInfo_New;
    }

    private void VisitData()
    {
      //String PatientInformationId = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PatientInformationId(Request.QueryString["s_Facility_Id"], Request.QueryString["s_IPS_VisitInformation_VisitNumber"]);
      string PatientInformationId = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_PatientInformationId(Request.QueryString["s_Facility_Id"], Request.QueryString["s_IPS_VisitInformation_VisitNumber"]);
      Int32 FindError = PatientInformationId.IndexOf("Error", StringComparison.CurrentCulture);

      if (FindError > -1)
      {
        Label_InvalidSearchMessage.Text = PatientInformationId;
        TableVisitInfo.Visible = false;
      }
      else
      {
        DataTable DataTable_VisitData;
        using (DataTable_VisitData = new DataTable())
        {
          DataTable_VisitData.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_IPS_VisitInformation_VisitNumber"]).Copy();
          if (DataTable_VisitData.Columns.Count == 1)
          {
            string Error = "";
            foreach (DataRow DataRow_Row in DataTable_VisitData.Rows)
            {
              Error = DataRow_Row["Error"].ToString();
            }

            Label_InvalidSearchMessage.Text = Error;
            TableVisitInfo.Visible = false;
            Error = "";
          }
          else if (DataTable_VisitData.Columns.Count != 1)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitData.Rows)
            {
              string DateOfAdmission = DataRow_Row["DateOfAdmission"].ToString();
              string DateOfDischarge = DataRow_Row["DateOfDischarge"].ToString();
              string PatientAge = DataRow_Row["PatientAge"].ToString();
              string Deceased = DataRow_Row["Deceased"].ToString();
              string Ward = "Ward: " + DataRow_Row["Ward"].ToString() + "; Room: " + DataRow_Row["Room"].ToString() + "; Bed: " + DataRow_Row["Bed"].ToString() + ";";

              if (Deceased == "True")
              {
                Deceased = "Yes";
              }
              else if (Deceased == "False")
              {
                Deceased = "No";
              }

              Ward = Ward.Replace("'", "");

              string IPSVisitInformationId = "";
              string SQLStringVisitInfo = "SELECT IPS_VisitInformation_Id FROM Form_IPS_VisitInformation WHERE Facility_Id = @Facility_Id AND IPS_VisitInformation_VisitNumber = @IPS_VisitInformation_VisitNumber";
              using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
              {
                SqlCommand_VisitInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_VisitInfo.Parameters.AddWithValue("@IPS_VisitInformation_VisitNumber", Request.QueryString["s_IPS_VisitInformation_VisitNumber"]);
                DataTable DataTable_VisitInfo;
                using (DataTable_VisitInfo = new DataTable())
                {
                  DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
                  DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
                  if (DataTable_VisitInfo.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row1 in DataTable_VisitInfo.Rows)
                    {
                      IPSVisitInformationId = DataRow_Row1["IPS_VisitInformation_Id"].ToString();
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(IPSVisitInformationId))
              {
                string SQLStringInsertVisitInformation = "INSERT INTO Form_IPS_VisitInformation ( Facility_Id , IPS_VisitInformation_VisitNumber , PatientInformation_Id , IPS_VisitInformation_PatientAge , IPS_VisitInformation_DateOfAdmission , IPS_VisitInformation_DateOfDischarge , IPS_VisitInformation_Deceased , IPS_VisitInformation_Ward , IPS_VisitInformation_Archived ) VALUES ( @Facility_Id , @IPS_VisitInformation_VisitNumber , @PatientInformation_Id , @IPS_VisitInformation_PatientAge , @IPS_VisitInformation_DateOfAdmission , @IPS_VisitInformation_DateOfDischarge , @IPS_VisitInformation_Deceased , @IPS_VisitInformation_Ward , @IPS_VisitInformation_Archived ); SELECT SCOPE_IDENTITY()";
                using (SqlCommand SqlCommand_InsertVisitInformation = new SqlCommand(SQLStringInsertVisitInformation))
                {
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_VisitNumber", Request.QueryString["s_IPS_VisitInformation_VisitNumber"]);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_PatientAge", PatientAge);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_DateOfAdmission", DateOfAdmission);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_DateOfDischarge", DateOfDischarge);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_Deceased", Deceased);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_Ward", Ward);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_Archived", 0);
                  IPSVisitInformationId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertVisitInformation);
                }
              }
              else
              {
                string SQLStringUpdateVisitInformation = "UPDATE Form_IPS_VisitInformation SET PatientInformation_Id = @PatientInformation_Id , IPS_VisitInformation_PatientAge  = @IPS_VisitInformation_PatientAge , IPS_VisitInformation_DateOfAdmission  = @IPS_VisitInformation_DateOfAdmission , IPS_VisitInformation_DateOfDischarge  = @IPS_VisitInformation_DateOfDischarge , IPS_VisitInformation_Deceased  = @IPS_VisitInformation_Deceased , IPS_VisitInformation_Ward  = @IPS_VisitInformation_Ward WHERE Facility_Id = @Facility_Id AND IPS_VisitInformation_VisitNumber = @IPS_VisitInformation_VisitNumber";
                using (SqlCommand SqlCommand_UpdateVisitInformation = new SqlCommand(SQLStringUpdateVisitInformation))
                {
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_PatientAge", PatientAge);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_DateOfAdmission", DateOfAdmission);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_DateOfDischarge", DateOfDischarge);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_Deceased", Deceased);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_Ward", Ward);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_VisitNumber", Request.QueryString["s_IPS_VisitInformation_VisitNumber"]);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateVisitInformation);
                }
              }

              Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + IPSVisitInformationId), false);
            }
          }
        }
      }
    }

    private void AntibioticData()
    {
      FromDataBase_VisitInfo FromDataBase_VisitInfo_Current = GetVisitInfo();
      Session["FacilityId"] = FromDataBase_VisitInfo_Current.FacilityId;
      Session["IPSVisitInformationVisitNumber"] = FromDataBase_VisitInfo_Current.IPSVisitInformationVisitNumber;

      string IPSVisitInformationId = Request.QueryString["IPSVisitInformationId"];

      DataTable DataTable_AntibioticInformation;
      using (DataTable_AntibioticInformation = new DataTable())
      {
        DataTable_AntibioticInformation.Locale = CultureInfo.CurrentCulture;
        DataTable_AntibioticInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_AntibioticInformation(Session["FacilityId"].ToString(), Session["IPSVisitInformationVisitNumber"].ToString()).Copy();
        if (DataTable_AntibioticInformation.Columns.Count == 1)
        {
          string Error = "";
          foreach (DataRow DataRow_Row in DataTable_AntibioticInformation.Rows)
          {
            Error = DataRow_Row["Error"].ToString();
          }

          Error = Error + Convert.ToString(" : Antibiotic", CultureInfo.CurrentCulture);
          Label Label_EditInvalidFormMessage = (Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage");
          if (Label_EditInvalidFormMessage != null)
          {
            Label_EditInvalidFormMessage.Text = Error;
          }
          Label_InvalidSearchMessage.Text = Error;
          Error = "";
        }
        else if (DataTable_AntibioticInformation.Columns.Count != 1)
        {
          foreach (DataRow DataRow_Row in DataTable_AntibioticInformation.Rows)
          {
            string Description = DataRow_Row["Description"].ToString();
            Description = Description.Replace("'", "");

            string IPSAntibioticPrescriptionId = "";
            string SQLStringAntibioticPrescription = "SELECT IPS_AntibioticPrescription_Id FROM Form_IPS_AntibioticPrescription WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id AND IPS_AntibioticPrescription_Description = @IPS_AntibioticPrescription_Description";
            using (SqlCommand SqlCommand_AntibioticPrescription = new SqlCommand(SQLStringAntibioticPrescription))
            {
              SqlCommand_AntibioticPrescription.Parameters.AddWithValue("@IPS_VisitInformation_Id", IPSVisitInformationId);
              SqlCommand_AntibioticPrescription.Parameters.AddWithValue("@IPS_AntibioticPrescription_Description", Description);
              DataTable DataTable_AntibioticPrescription;
              using (DataTable_AntibioticPrescription = new DataTable())
              {
                DataTable_AntibioticPrescription.Locale = CultureInfo.CurrentCulture;
                DataTable_AntibioticPrescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntibioticPrescription).Copy();
                if (DataTable_AntibioticPrescription.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row1 in DataTable_AntibioticPrescription.Rows)
                  {
                    IPSAntibioticPrescriptionId = DataRow_Row1["IPS_AntibioticPrescription_Id"].ToString();
                  }
                }
              }
            }

            if (string.IsNullOrEmpty(IPSAntibioticPrescriptionId))
            {
              string SQLStringInsertVisitInformation = "INSERT INTO Form_IPS_AntibioticPrescription ( IPS_VisitInformation_Id , IPS_AntibioticPrescription_Description ) VALUES ( @IPS_VisitInformation_Id , @IPS_AntibioticPrescription_Description )";
              using (SqlCommand SqlCommand_InsertVisitInformation = new SqlCommand(SQLStringInsertVisitInformation))
              {
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@IPS_VisitInformation_Id", IPSVisitInformationId);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@IPS_AntibioticPrescription_Description", Description);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertVisitInformation);
              }
            }
          }


          string IPSAntibioticPrescriptionIdCompare = "";
          string IPSAntibioticPrescriptionDescriptionCompare = "";
          string SQLStringAntibioticPrescriptionCompare = "SELECT IPS_AntibioticPrescription_Id , REPLACE(IPS_AntibioticPrescription_Description , '''','''''') AS IPS_AntibioticPrescription_Description FROM Form_IPS_AntibioticPrescription WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id";
          using (SqlCommand SqlCommand_AntibioticPrescriptionCompare = new SqlCommand(SQLStringAntibioticPrescriptionCompare))
          {
            SqlCommand_AntibioticPrescriptionCompare.Parameters.AddWithValue("@IPS_VisitInformation_Id", IPSVisitInformationId);
            DataTable DataTable_AntibioticPrescriptionCompare;
            using (DataTable_AntibioticPrescriptionCompare = new DataTable())
            {
              DataTable_AntibioticPrescriptionCompare.Locale = CultureInfo.CurrentCulture;
              DataTable_AntibioticPrescriptionCompare = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntibioticPrescriptionCompare).Copy();
              if (DataTable_AntibioticPrescriptionCompare.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_AntibioticPrescriptionCompare.Rows)
                {
                  IPSAntibioticPrescriptionIdCompare = DataRow_Row["IPS_AntibioticPrescription_Id"].ToString();
                  IPSAntibioticPrescriptionDescriptionCompare = DataRow_Row["IPS_AntibioticPrescription_Description"].ToString();

                  DataRow[] DataRow_AntibioticInformation = DataTable_AntibioticInformation.Select("Description = '" + IPSAntibioticPrescriptionDescriptionCompare + "'");

                  if (DataRow_AntibioticInformation.Length == 0)
                  {
                    string SQLStringInsertVisitInformationDelete = "DELETE FROM Form_IPS_AntibioticPrescription WHERE IPS_AntibioticPrescription_Id = @IPS_AntibioticPrescription_Id";
                    using (SqlCommand SqlCommand_InsertVisitInformationDelete = new SqlCommand(SQLStringInsertVisitInformationDelete))
                    {
                      SqlCommand_InsertVisitInformationDelete.Parameters.AddWithValue("@IPS_AntibioticPrescription_Id", IPSAntibioticPrescriptionIdCompare);
                      InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertVisitInformationDelete);
                    }
                  }
                }
              }
            }
          }

          IPSAntibioticPrescriptionIdCompare = "";
          IPSAntibioticPrescriptionDescriptionCompare = "";
        }
      }

      Session.Remove("FacilityId");
      Session.Remove("IPSVisitInformationVisitNumber");
    }

    private void TheatreData()
    {
      string IPSInfectionId = Request.QueryString["IPSInfectionId"];

      FromDataBase_VisitInfo FromDataBase_VisitInfo_Current = GetVisitInfo();
      Session["FacilityId"] = FromDataBase_VisitInfo_Current.FacilityId;
      Session["IPSVisitInformationVisitNumber"] = FromDataBase_VisitInfo_Current.IPSVisitInformationVisitNumber;

      DataTable DataTable_Theatre;
      using (DataTable_Theatre = new DataTable())
      {
        DataTable_Theatre.Locale = CultureInfo.CurrentCulture;
        DataTable_Theatre = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_TheatreInformation(Session["FacilityId"].ToString(), Session["IPSVisitInformationVisitNumber"].ToString()).Copy();
        if (DataTable_Theatre.Columns.Count == 1)
        {
          string Error = "";
          foreach (DataRow DataRow_Row in DataTable_Theatre.Rows)
          {
            Error = DataRow_Row["Error"].ToString();
          }

          Error = Error + Convert.ToString(" : Visit History", CultureInfo.CurrentCulture);
          Label Label_EditInvalidFormMessage = (Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage");
          if (Label_EditInvalidFormMessage != null)
          {
            Label_EditInvalidFormMessage.Text = Error;
          }
          Label_InvalidSearchMessage.Text = Error;
          Error = "";
        }
        else if (DataTable_Theatre.Columns.Count != 1)
        {
          Session["IPSTheatreId"] = "";
          Session["IPSTheatreFacility"] = "";
          Session["IPSTheatreDateOfAdmission"] = "";
          Session["IPSTheatreDateOfDischarge"] = "";
          Session["IPSTheatreFinalDiagnosisCode"] = "";
          Session["IPSTheatreFinalDiagnosisDescription"] = "";
          Session["IPSTheatreVisitNumber"] = "";
          Session["IPSTheatreTheatre"] = "";
          Session["IPSTheatreTheatreTime"] = "";
          Session["IPSTheatreProcedureDate"] = "";
          Session["IPSTheatreProcedureCode"] = "";
          Session["IPSTheatreProcedureDescription"] = "";
          Session["IPSTheatreTheatreInvoice"] = "";
          Session["IPSTheatreSurgeon"] = "";
          Session["IPSTheatreAnaesthetist"] = "";
          Session["IPSTheatreAssistant"] = "";
          Session["IPSTheatreWoundCategory"] = "";
          Session["IPSTheatreScrubNurse"] = "";
          Session["IPSTheatreServiceCategory"] = "";

          string SQLStringTheatreCompare = "SELECT IPS_Theatre_Id , IPS_Theatre_Facility , IPS_Theatre_DateOfAdmission , IPS_Theatre_DateOfDischarge , REPLACE(IPS_Theatre_FinalDiagnosisCode , '''','''''') AS IPS_Theatre_FinalDiagnosisCode , REPLACE(IPS_Theatre_FinalDiagnosisDescription , '''','''''') AS IPS_Theatre_FinalDiagnosisDescription , IPS_Theatre_VisitNumber , REPLACE(IPS_Theatre_Theatre , '''','''''') AS IPS_Theatre_Theatre , IPS_Theatre_TheatreTime , IPS_Theatre_ProcedureDate , REPLACE(IPS_Theatre_ProcedureCode , '''','''''') AS IPS_Theatre_ProcedureCode , REPLACE(IPS_Theatre_ProcedureDescription , '''','''''') AS IPS_Theatre_ProcedureDescription , REPLACE(IPS_Theatre_TheatreInvoice , '''','''''') AS IPS_Theatre_TheatreInvoice , REPLACE(IPS_Theatre_Surgeon , '''','''''') AS IPS_Theatre_Surgeon , REPLACE(IPS_Theatre_Anaesthetist , '''','''''') AS IPS_Theatre_Anaesthetist ,  REPLACE(IPS_Theatre_Assistant , '''','''''') AS IPS_Theatre_Assistant , REPLACE(IPS_Theatre_WoundCategory , '''','''''') AS IPS_Theatre_WoundCategory , REPLACE(IPS_Theatre_ScrubNurse , '''','''''') AS IPS_Theatre_ScrubNurse , REPLACE(IPS_Theatre_ServiceCategory , '''','''''') AS IPS_Theatre_ServiceCategory FROM Form_IPS_Theatre WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_TheatreCompare = new SqlCommand(SQLStringTheatreCompare))
          {
            SqlCommand_TheatreCompare.Parameters.AddWithValue("@IPS_Infection_Id", IPSInfectionId);
            DataTable DataTable_TheatreCompare;
            using (DataTable_TheatreCompare = new DataTable())
            {
              DataTable_TheatreCompare.Locale = CultureInfo.CurrentCulture;
              DataTable_TheatreCompare = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TheatreCompare).Copy();
              if (DataTable_TheatreCompare.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_TheatreCompare.Rows)
                {
                  Session["IPSTheatreId"] = DataRow_Row["IPS_Theatre_Id"];
                  Session["IPSTheatreFacility"] = DataRow_Row["IPS_Theatre_Facility"];
                  Session["IPSTheatreDateOfAdmission"] = DataRow_Row["IPS_Theatre_DateOfAdmission"];
                  Session["IPSTheatreDateOfDischarge"] = DataRow_Row["IPS_Theatre_DateOfDischarge"];
                  Session["IPSTheatreFinalDiagnosisCode"] = DataRow_Row["IPS_Theatre_FinalDiagnosisCode"];
                  Session["IPSTheatreFinalDiagnosisDescription"] = DataRow_Row["IPS_Theatre_FinalDiagnosisDescription"];
                  Session["IPSTheatreVisitNumber"] = DataRow_Row["IPS_Theatre_VisitNumber"];
                  Session["IPSTheatreTheatre"] = DataRow_Row["IPS_Theatre_Theatre"];
                  Session["IPSTheatreTheatreTime"] = DataRow_Row["IPS_Theatre_TheatreTime"];
                  Session["IPSTheatreProcedureDate"] = DataRow_Row["IPS_Theatre_ProcedureDate"];
                  Session["IPSTheatreProcedureCode"] = DataRow_Row["IPS_Theatre_ProcedureCode"];
                  Session["IPSTheatreProcedureDescription"] = DataRow_Row["IPS_Theatre_ProcedureDescription"];
                  Session["IPSTheatreTheatreInvoice"] = DataRow_Row["IPS_Theatre_TheatreInvoice"];
                  Session["IPSTheatreSurgeon"] = DataRow_Row["IPS_Theatre_Surgeon"];
                  Session["IPSTheatreAnaesthetist"] = DataRow_Row["IPS_Theatre_Anaesthetist"];
                  Session["IPSTheatreAssistant"] = DataRow_Row["IPS_Theatre_Assistant"];
                  Session["IPSTheatreWoundCategory"] = DataRow_Row["IPS_Theatre_WoundCategory"];
                  Session["IPSTheatreScrubNurse"] = DataRow_Row["IPS_Theatre_ScrubNurse"];
                  Session["IPSTheatreServiceCategory"] = DataRow_Row["IPS_Theatre_ServiceCategory"];

                  string DataTableSelect = "FacilityCode = '" + Session["IPSTheatreFacility"].ToString() + "' AND VisitNumber = '" + Session["IPSTheatreVisitNumber"].ToString() + "' AND FinalDiagnosisCode = '" + Session["IPSTheatreFinalDiagnosisCode"].ToString() + "' AND FinalDiagnosisDescription = '" + Session["IPSTheatreFinalDiagnosisDescription"].ToString() + "' AND AdmissionDate = '" + Session["IPSTheatreDateOfAdmission"].ToString() + "' AND DischargeDate = '" + Session["IPSTheatreDateOfDischarge"].ToString() + "' AND ProcedureDate = '" + Session["IPSTheatreProcedureDate"].ToString() + "' AND TheatreInvoice = '" + Session["IPSTheatreTheatreInvoice"].ToString() + "' AND Theatre = '" + Session["IPSTheatreTheatre"].ToString() + "' AND TheatreTime = '" + Session["IPSTheatreTheatreTime"].ToString() + "' AND Surgeon = '" + Session["IPSTheatreSurgeon"].ToString() + "' AND Anaesthetist = '" + Session["IPSTheatreAnaesthetist"].ToString() + "' AND Assistant = '" + Session["IPSTheatreAssistant"].ToString() + "' AND ProcedureCode = '" + Session["IPSTheatreProcedureCode"].ToString() + "' AND ProcedureDescription = '" + Session["IPSTheatreProcedureDescription"].ToString() + "' AND ScrubNurse = '" + Session["IPSTheatreScrubNurse"].ToString() + "' AND WoundCategory = '" + Session["IPSTheatreWoundCategory"].ToString() + "' AND ServiceCategory  + ' (' + VisitType + ')' = '" + Session["IPSTheatreServiceCategory"].ToString() + "'";
                  DataTableSelect = DataTableSelect.Replace(" = '' ", " IS NULL ");

                  DataRow[] DataRow_Theatre = DataTable_Theatre.Select(DataTableSelect);

                  if (DataRow_Theatre.Length == 0)
                  {
                    string SQLStringDeleteTheatre = "DELETE FROM Form_IPS_Theatre WHERE IPS_Theatre_Id = @IPS_Theatre_Id";
                    using (SqlCommand SqlCommand_DeleteTheatre = new SqlCommand(SQLStringDeleteTheatre))
                    {
                      SqlCommand_DeleteTheatre.Parameters.AddWithValue("@IPS_Theatre_Id", Session["IPSTheatreId"].ToString());
                      InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteTheatre);
                    }
                  }
                }
              }
            }
          }

          Session.Remove("IPSTheatreId");
          Session.Remove("IPSTheatreFacility");
          Session.Remove("IPSTheatreDateOfAdmission");
          Session.Remove("IPSTheatreDateOfDischarge");
          Session.Remove("IPSTheatreFinalDiagnosisCode");
          Session.Remove("IPSTheatreFinalDiagnosisDescription");
          Session.Remove("IPSTheatreVisitNumber");
          Session.Remove("IPSTheatreTheatre");
          Session.Remove("IPSTheatreTheatreTime");
          Session.Remove("IPSTheatreProcedureDate");
          Session.Remove("IPSTheatreProcedureCode");
          Session.Remove("IPSTheatreProcedureDescription");
          Session.Remove("IPSTheatreTheatreInvoice");
          Session.Remove("IPSTheatreSurgeon");
          Session.Remove("IPSTheatreAnaesthetist");
          Session.Remove("IPSTheatreAssistant");
          Session.Remove("IPSTheatreWoundCategory");
          Session.Remove("IPSTheatreScrubNurse");
          Session.Remove("IPSTheatreServiceCategory");
        }
      }

      Session.Remove("FacilityId");
      Session.Remove("IPSVisitInformationVisitNumber");
    }

    private void VisitDiagnosisData()
    {
      string IPSInfectionId = Request.QueryString["IPSInfectionId"];

      FromDataBase_VisitInfo FromDataBase_VisitInfo_Current = GetVisitInfo();
      Session["FacilityId"] = FromDataBase_VisitInfo_Current.FacilityId;
      Session["IPSVisitInformationVisitNumber"] = FromDataBase_VisitInfo_Current.IPSVisitInformationVisitNumber;

      DataTable DataTable_VisitDiagnosis;
      using (DataTable_VisitDiagnosis = new DataTable())
      {
        DataTable_VisitDiagnosis.Locale = CultureInfo.CurrentCulture;
        DataTable_VisitDiagnosis = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_CodingInformation(Session["FacilityId"].ToString(), Session["IPSVisitInformationVisitNumber"].ToString()).Copy();
        if (DataTable_VisitDiagnosis.Columns.Count == 1)
        {
          string Error = "";
          foreach (DataRow DataRow_Row in DataTable_VisitDiagnosis.Rows)
          {
            Error = DataRow_Row["Error"].ToString();
          }

          Error = Error + Convert.ToString(" : Visit Diagnosis", CultureInfo.CurrentCulture);
          Label Label_EditInvalidFormMessage = (Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage");
          if (Label_EditInvalidFormMessage != null)
          {
            Label_EditInvalidFormMessage.Text = Error;
          }
          Label_InvalidSearchMessage.Text = Error;
          Error = "";
        }
        else if (DataTable_VisitDiagnosis.Columns.Count != 1)
        {
          Session["IPSVisitDiagnosisId"] = "";
          Session["IPSVisitDiagnosisCodeType"] = "";
          Session["IPSVisitDiagnosisCode"] = "";
          Session["IPSVisitDiagnosisDescription"] = "";
          string SQLStringVisitDiagnosisCompare = "SELECT IPS_VisitDiagnosis_Id , REPLACE(IPS_VisitDiagnosis_CodeType , '''','''''') AS IPS_VisitDiagnosis_CodeType , REPLACE(IPS_VisitDiagnosis_Code , '''','''''') AS IPS_VisitDiagnosis_Code , REPLACE(IPS_VisitDiagnosis_Description , '''','''''') AS IPS_VisitDiagnosis_Description FROM Form_IPS_VisitDiagnosis WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_VisitDiagnosisCompare = new SqlCommand(SQLStringVisitDiagnosisCompare))
          {
            SqlCommand_VisitDiagnosisCompare.Parameters.AddWithValue("@IPS_Infection_Id", IPSInfectionId);
            DataTable DataTable_VisitDiagnosisCompare;
            using (DataTable_VisitDiagnosisCompare = new DataTable())
            {
              DataTable_VisitDiagnosisCompare.Locale = CultureInfo.CurrentCulture;
              DataTable_VisitDiagnosisCompare = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitDiagnosisCompare).Copy();
              if (DataTable_VisitDiagnosisCompare.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_VisitDiagnosisCompare.Rows)
                {
                  Session["IPSVisitDiagnosisId"] = DataRow_Row["IPS_VisitDiagnosis_Id"];
                  Session["IPSVisitDiagnosisCodeType"] = DataRow_Row["IPS_VisitDiagnosis_CodeType"];
                  Session["IPSVisitDiagnosisCode"] = DataRow_Row["IPS_VisitDiagnosis_Code"];
                  Session["IPSVisitDiagnosisDescription"] = DataRow_Row["IPS_VisitDiagnosis_Description"];

                  string DataTableSelect = "CodeType = '" + Session["IPSVisitDiagnosisCodeType"].ToString() + "' AND Code = '" + Session["IPSVisitDiagnosisCode"].ToString() + "' AND CodeDescription = '" + Session["IPSVisitDiagnosisDescription"].ToString() + "'";
                  DataTableSelect = DataTableSelect.Replace(" = '' ", " IS NULL ");

                  DataRow[] DataRow_VisitDiagnosis = DataTable_VisitDiagnosis.Select(DataTableSelect);

                  if (DataRow_VisitDiagnosis.Length == 0)
                  {
                    string SQLStringDeleteVisitDiagnosis = "DELETE FROM Form_IPS_VisitDiagnosis WHERE IPS_VisitDiagnosis_Id = @IPS_VisitDiagnosis_Id";
                    using (SqlCommand SqlCommand_DeleteVisitDiagnosis = new SqlCommand(SQLStringDeleteVisitDiagnosis))
                    {
                      SqlCommand_DeleteVisitDiagnosis.Parameters.AddWithValue("@IPS_VisitDiagnosis_Id", Session["IPSVisitDiagnosisId"]);
                      InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteVisitDiagnosis);
                    }
                  }
                }
              }
            }
          }

          Session.Remove("IPSVisitDiagnosisId");
          Session.Remove("IPSVisitDiagnosisCodeType");
          Session.Remove("IPSVisitDiagnosisCode");
          Session.Remove("IPSVisitDiagnosisDescription");
        }
      }

      Session.Remove("FacilityId");
      Session.Remove("IPSVisitInformationVisitNumber");
    }

    private void BedHistoryData()
    {
      string IPSInfectionId = Request.QueryString["IPSInfectionId"];

      FromDataBase_VisitInfo FromDataBase_VisitInfo_Current = GetVisitInfo();
      Session["FacilityId"] = FromDataBase_VisitInfo_Current.FacilityId;
      Session["IPSVisitInformationVisitNumber"] = FromDataBase_VisitInfo_Current.IPSVisitInformationVisitNumber;

      DataTable DataTable_BedHistory;
      using (DataTable_BedHistory = new DataTable())
      {
        DataTable_BedHistory.Locale = CultureInfo.CurrentCulture;
        DataTable_BedHistory = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_AccommodationInformation(Session["FacilityId"].ToString(), Session["IPSVisitInformationVisitNumber"].ToString()).Copy();
        if (DataTable_BedHistory.Columns.Count == 1)
        {
          string Error = "";
          foreach (DataRow DataRow_Row in DataTable_BedHistory.Rows)
          {
            Error = DataRow_Row["Error"].ToString();
          }

          Error = Error + Convert.ToString(" : Bed History", CultureInfo.CurrentCulture);
          Label Label_EditInvalidFormMessage = (Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage");
          if (Label_EditInvalidFormMessage != null)
          {
            Label_EditInvalidFormMessage.Text = Error;
          }
          Label_InvalidSearchMessage.Text = Error;
          Error = "";
        }
        else if (DataTable_BedHistory.Columns.Count != 1)
        {
          Session["IPSBedHistoryId"] = "";
          Session["IPSBedHistoryDepartment"] = "";
          Session["IPSBedHistoryRoom"] = "";
          Session["IPSBedHistoryBed"] = "";
          Session["IPSBedHistoryDate"] = "";
          string SQLStringBedHistoryCompare = "SELECT IPS_BedHistory_Id , REPLACE(IPS_BedHistory_Department , '''','''''') AS IPS_BedHistory_Department , REPLACE(IPS_BedHistory_Room , '''','''''') AS IPS_BedHistory_Room , REPLACE(IPS_BedHistory_Bed , '''','''''') AS IPS_BedHistory_Bed , IPS_BedHistory_Date FROM Form_IPS_BedHistory WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_BedHistoryCompare = new SqlCommand(SQLStringBedHistoryCompare))
          {
            SqlCommand_BedHistoryCompare.Parameters.AddWithValue("@IPS_Infection_Id", IPSInfectionId);
            DataTable DataTable_BedHistoryCompare;
            using (DataTable_BedHistoryCompare = new DataTable())
            {
              DataTable_BedHistoryCompare.Locale = CultureInfo.CurrentCulture;
              DataTable_BedHistoryCompare = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_BedHistoryCompare).Copy();
              if (DataTable_BedHistoryCompare.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_BedHistoryCompare.Rows)
                {
                  Session["IPSBedHistoryId"] = DataRow_Row["IPS_BedHistory_Id"];
                  Session["IPSBedHistoryDepartment"] = DataRow_Row["IPS_BedHistory_Department"].ToString();
                  Session["IPSBedHistoryRoom"] = DataRow_Row["IPS_BedHistory_Room"].ToString();
                  Session["IPSBedHistoryBed"] = DataRow_Row["IPS_BedHistory_Bed"].ToString();
                  Session["IPSBedHistoryDate"] = DataRow_Row["IPS_BedHistory_Date"].ToString();

                  string DataTableSelect = "Department = '" + Session["IPSBedHistoryDepartment"].ToString() + "' AND Room = '" + Session["IPSBedHistoryRoom"].ToString() + "' AND Bed = '" + Session["IPSBedHistoryBed"].ToString() + "' AND Date = '" + Session["IPSBedHistoryDate"].ToString() + "'";
                  DataTableSelect = DataTableSelect.Replace(" = '' ", " IS NULL ");

                  DataRow[] DataRow_BedHistory = DataTable_BedHistory.Select(DataTableSelect);

                  if (DataRow_BedHistory.Length == 0)
                  {
                    string SQLStringDeleteBedHistory = "UPDATE Form_IPS_Specimen SET IPS_Specimen_BedHistory = NULL WHERE IPS_Specimen_BedHistory = @IPS_Specimen_BedHistory ; DELETE FROM Form_IPS_BedHistory WHERE IPS_BedHistory_Id = @IPS_BedHistory_Id";
                    using (SqlCommand SqlCommand_DeleteBedHistory = new SqlCommand(SQLStringDeleteBedHistory))
                    {
                      SqlCommand_DeleteBedHistory.Parameters.AddWithValue("@IPS_Specimen_BedHistory", Session["IPSBedHistoryId"]);
                      SqlCommand_DeleteBedHistory.Parameters.AddWithValue("@IPS_BedHistory_Id", Session["IPSBedHistoryId"]);
                      InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteBedHistory);
                    }
                  }
                }
              }
            }
          }

          Session.Remove("IPSBedHistoryId");
          Session.Remove("IPSBedHistoryDepartment");
          Session.Remove("IPSBedHistoryRoom");
          Session.Remove("IPSBedHistoryBed");
          Session.Remove("IPSBedHistoryDate");
        }
      }

      Session.Remove("FacilityId");
      Session.Remove("IPSVisitInformationVisitNumber");
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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('37')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '22'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '155'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '11'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '10'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_FacilityId
    {
      public string FacilityId { get; set; }
    }

    private FromDataBase_FacilityId GetFacilityId()
    {
      FromDataBase_FacilityId FromDataBase_FacilityId_New = new FromDataBase_FacilityId();

      string SQLStringFacility = "SELECT Facility_Id FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);
        DataTable DataTable_Facility;
        using (DataTable_Facility = new DataTable())
        {
          DataTable_Facility.Locale = CultureInfo.CurrentCulture;
          DataTable_Facility = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Facility).Copy();
          if (DataTable_Facility.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Facility.Rows)
            {
              FromDataBase_FacilityId_New.FacilityId = DataRow_Row["Facility_Id"].ToString();
            }
          }
        }
      }

      return FromDataBase_FacilityId_New;
    }

    private class FromDataBase_InfectionCompleted
    {
      public string IPSInfectionCompleted { get; set; }
      public string IPSInfectionIsActive { get; set; }
    }

    private FromDataBase_InfectionCompleted GetInfectionCompleted()
    {
      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_New = new FromDataBase_InfectionCompleted();

      string SQLStringInfection = "SELECT IPS_Infection_Completed , IPS_Infection_IsActive FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              FromDataBase_InfectionCompleted_New.IPSInfectionCompleted = DataRow_Row["IPS_Infection_Completed"].ToString();
              FromDataBase_InfectionCompleted_New.IPSInfectionIsActive = DataRow_Row["IPS_Infection_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_InfectionCompleted_New;
    }

    private class FromDataBase_SpecimenCompleted
    {
      public string Specimen { get; set; }
    }

    private FromDataBase_SpecimenCompleted GetSpecimenCompleted()
    {
      FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_New = new FromDataBase_SpecimenCompleted();

      string SQLStringSpecimen = "EXECUTE spForm_Get_IPS_SpecimenCompleted @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Specimen = new SqlCommand(SQLStringSpecimen))
      {
        SqlCommand_Specimen.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Specimen;
        using (DataTable_Specimen = new DataTable())
        {
          DataTable_Specimen.Locale = CultureInfo.CurrentCulture;
          DataTable_Specimen = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Specimen).Copy();
          if (DataTable_Specimen.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Specimen.Rows)
            {
              FromDataBase_SpecimenCompleted_New.Specimen = DataRow_Row["Specimen"].ToString();
            }
          }
        }
      }

      return FromDataBase_SpecimenCompleted_New;
    }

    private class FromDataBase_IsHAI
    {
      public string IsHAI { get; set; }
    }

    private static FromDataBase_IsHAI GetIsHAI(string ipsInfectionId)
    {
      FromDataBase_IsHAI FromDataBase_IsHAI_New = new FromDataBase_IsHAI();

      string SQLStringIPSInfectionHAI = "SELECT CASE WHEN IPS_Infection_Category_List IN (4799) THEN 'True' ELSE 'False' END AS IsHAI FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_IPSInfectionHAI = new SqlCommand(SQLStringIPSInfectionHAI))
      {
        SqlCommand_IPSInfectionHAI.Parameters.AddWithValue("@IPS_Infection_Id", ipsInfectionId);
        DataTable DataTable_IPSInfectionHAI;
        using (DataTable_IPSInfectionHAI = new DataTable())
        {
          DataTable_IPSInfectionHAI.Locale = CultureInfo.CurrentCulture;
          DataTable_IPSInfectionHAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IPSInfectionHAI).Copy();
          if (DataTable_IPSInfectionHAI.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_IPSInfectionHAI.Rows)
            {
              FromDataBase_IsHAI_New.IsHAI = DataRow_Row["IsHAI"].ToString();
            }
          }
        }
      }

      return FromDataBase_IsHAI_New;
    }

    private class FromDataBase_HAICompleted
    {
      public string IPSHAICompleted { get; set; }
    }

    private FromDataBase_HAICompleted GetHAICompleted()
    {
      FromDataBase_HAICompleted FromDataBase_HAICompleted_New = new FromDataBase_HAICompleted();

      string SQLStringHAI = "SELECT IPS_HAI_Investigation_Completed FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_HAI = new SqlCommand(SQLStringHAI))
      {
        SqlCommand_HAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_HAI;
        using (DataTable_HAI = new DataTable())
        {
          DataTable_HAI.Locale = CultureInfo.CurrentCulture;
          DataTable_HAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAI).Copy();
          if (DataTable_HAI.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HAI.Rows)
            {
              FromDataBase_HAICompleted_New.IPSHAICompleted = DataRow_Row["IPS_HAI_Investigation_Completed"].ToString();
            }
          }
        }
      }

      return FromDataBase_HAICompleted_New;
    }

    private class FromDataBase_InfectionSite
    {
      public string IPSInfectionSiteInfectionIsActive { get; set; }
    }

    private FromDataBase_InfectionSite GetInfectionSite()
    {
      FromDataBase_InfectionSite FromDataBase_InfectionSite_New = new FromDataBase_InfectionSite();

      string SQLStringInfection = "SELECT CASE WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4996 THEN 'True' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'False' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4996') THEN 'False' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'False' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4997') THEN 'False' ELSE 'True' END	AS IPS_Infection_Site_Infection_IsActive FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              FromDataBase_InfectionSite_New.IPSInfectionSiteInfectionIsActive = DataRow_Row["IPS_Infection_Site_Infection_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_InfectionSite_New;
    }


    //--START-- --Search--//
    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance Form", "Form_IPS.aspx");
      Response.Redirect(FinalURL, false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect("Form_IPS.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_IPS_VisitInformation_VisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "", false);
      }
      else
      {
        Label_InvalidSearchMessage.Text = Label_InvalidSearchMessageText;
      }
    }

    protected string SearchValidation()
    {
      string InvalidSearch = "No";
      string InvalidSearchMessage = "";

      if (InvalidSearch == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue))
        {
          InvalidSearch = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text))
        {
          InvalidSearch = "Yes";
        }
      }

      if (InvalidSearch == "Yes")
      {
        InvalidSearchMessage = "All red fields are required";
      }

      if (InvalidSearch == "No" && string.IsNullOrEmpty(InvalidSearchMessage))
      {

      }

      return InvalidSearchMessage;
    }

    private void RedirectToList()
    {
      string FinalURL = "Form_IPS_List.aspx";
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --VisitInfo--//
    private void TableVisitInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["IPSVisitInformationVisitNumber"] = "";
      Session["PatientInformationName"] = "";
      Session["PatientInformationSurname"] = "";
      Session["PatientInformationGender"] = "";
      Session["IPSVisitInformationPatientAge"] = "";
      Session["PatientInformationDateOfBirth"] = "";
      Session["IPSVisitInformationDateOfAdmission"] = "";
      Session["IPSVisitInformationDateOfDischarge"] = "";
      Session["IPSVisitInformationDeceased"] = "";
      Session["IPSVisitInformationWard"] = "";
      string SQLStringVisitInfo = "SELECT Facility_FacilityDisplayName , IPS_VisitInformation_VisitNumber , PatientInformation_Name , PatientInformation_Surname , PatientInformation_Gender , IPS_VisitInformation_PatientAge , PatientInformation_DateOfBirth , IPS_VisitInformation_DateOfAdmission , IPS_VisitInformation_DateOfDischarge , IPS_VisitInformation_Deceased , IPS_VisitInformation_Ward FROM vForm_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);
        DataTable DataTable_VisitInfo;
        using (DataTable_VisitInfo = new DataTable())
        {
          DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
          if (DataTable_VisitInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["IPSVisitInformationVisitNumber"] = DataRow_Row["IPS_VisitInformation_VisitNumber"];
              Session["PatientInformationName"] = DataRow_Row["PatientInformation_Name"];
              Session["PatientInformationSurname"] = DataRow_Row["PatientInformation_Surname"];
              Session["PatientInformationGender"] = DataRow_Row["PatientInformation_Gender"];
              Session["IPSVisitInformationPatientAge"] = DataRow_Row["IPS_VisitInformation_PatientAge"];
              Session["PatientInformationDateOfBirth"] = DataRow_Row["PatientInformation_DateOfBirth"];
              Session["IPSVisitInformationDateOfAdmission"] = DataRow_Row["IPS_VisitInformation_DateOfAdmission"];
              Session["IPSVisitInformationDateOfDischarge"] = DataRow_Row["IPS_VisitInformation_DateOfDischarge"];
              Session["IPSVisitInformationDeceased"] = DataRow_Row["IPS_VisitInformation_Deceased"];
              Session["IPSVisitInformationWard"] = DataRow_Row["IPS_VisitInformation_Ward"];
            }
          }
        }
      }

      Label_VIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_VIVisitNumber.Text = Session["IPSVisitInformationVisitNumber"].ToString();
      Label_VIName.Text = Session["PatientInformationSurname"].ToString() + Convert.ToString(", ", CultureInfo.CurrentCulture) + Session["PatientInformationName"].ToString();
      Label_VIGender.Text = Session["PatientInformationGender"].ToString();
      Label_VIAge.Text = Session["IPSVisitInformationPatientAge"].ToString();
      Label_VIDateOfBirth.Text = Session["PatientInformationDateOfBirth"].ToString().Replace(" 12:00:00 AM","");
      Label_VIDateAdmission.Text = Session["IPSVisitInformationDateOfAdmission"].ToString();
      Label_VIDateDischarge.Text = Session["IPSVisitInformationDateOfDischarge"].ToString();
      Label_VIDeceased.Text = Session["IPSVisitInformationDeceased"].ToString();
      Label_VIWard.Text = Session["IPSVisitInformationWard"].ToString();

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("IPSVisitInformationVisitNumber");
      Session.Remove("PatientInformationName");
      Session.Remove("PatientInformationSurname");
      Session.Remove("PatientInformationGender");
      Session.Remove("IPSVisitInformationPatientAge");
      Session.Remove("PatientInformationDateOfBirth");
      Session.Remove("IPSVisitInformationDateOfAdmission");
      Session.Remove("IPSVisitInformationDateOfDischarge");
      Session.Remove("IPSVisitInformationDeceased");
      Session.Remove("IPSVisitInformationWard");
    }
    //---END--- --VisitInfo--//


    //--START-- --Infection--//
    protected void SqlDataSource_IPS_Infection_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenInfectionTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_Infection_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }

      for (int i = 0; i < GridView_IPS_Infection_List.Rows.Count; i++)
      {
        if (GridView_IPS_Infection_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          Label Label_IsActive = (Label)GridView_IPS_Infection_List.Rows[i].FindControl("Label_IsActive");
          HtmlTableCell InfectionIsActive = (HtmlTableCell)GridView_IPS_Infection_List.Rows[i].FindControl("InfectionIsActive");

          if (Label_IsActive.Text == "Yes")
          {
            Label_IsActive.ForeColor = Color.FromName("#333333");

            InfectionIsActive.Attributes.Add("Style", "color: #333333");
            InfectionIsActive.Attributes.Add("Style", "background-color: #77cf9c");
          }
          else if (Label_IsActive.Text == "No")
          {
            Label_IsActive.ForeColor = Color.FromName("#333333");

            InfectionIsActive.Attributes.Add("Style", "color: #333333");
            InfectionIsActive.Attributes.Add("Style", "background-color: #d46e6e");
          }


          Label Label_Completed = (Label)GridView_IPS_Infection_List.Rows[i].FindControl("Label_Completed");
          HtmlTableCell InfectionCompleted = (HtmlTableCell)GridView_IPS_Infection_List.Rows[i].FindControl("InfectionCompleted");

          if (Label_IsActive.Text == "Yes")
          {
            if (Label_Completed.Text == "Complete")
            {
              Label_Completed.ForeColor = Color.FromName("#333333");

              InfectionCompleted.Attributes.Add("Style", "color: #333333");
              InfectionCompleted.Attributes.Add("Style", "background-color: #77cf9c");
            }
            else if (Label_Completed.Text == "Incomplete")
            {
              Label_Completed.ForeColor = Color.FromName("#333333");

              InfectionCompleted.Attributes.Add("Style", "color: #333333");
              InfectionCompleted.Attributes.Add("Style", "background-color: #d46e6e");
            }
          }
          else if (Label_IsActive.Text == "No")
          {
            Label_Completed.ForeColor = Color.FromName("#333333");

            InfectionCompleted.Attributes.Add("Style", "color: #333333");
            InfectionCompleted.Attributes.Add("Style", "background-color: #77cf9c");
          }


          Label Label_Specimen = (Label)GridView_IPS_Infection_List.Rows[i].FindControl("Label_Specimen");
          HtmlTableCell InfectionSpecimen = (HtmlTableCell)GridView_IPS_Infection_List.Rows[i].FindControl("InfectionSpecimen");

          if (Label_IsActive.Text == "Yes")
          {
            if (Label_Specimen.Text == "Complete")
            {
              Label_Specimen.ForeColor = Color.FromName("#333333");

              InfectionSpecimen.Attributes.Add("Style", "color: #333333");
              InfectionSpecimen.Attributes.Add("Style", "background-color: #77cf9c");
            }
            else if (Label_Specimen.Text == "Incomplete")
            {
              Label_Specimen.ForeColor = Color.FromName("#333333");

              InfectionSpecimen.Attributes.Add("Style", "color: #333333");
              InfectionSpecimen.Attributes.Add("Style", "background-color: #d46e6e");
            }
          }
          else if (Label_IsActive.Text == "No")
          {
            Label_Specimen.ForeColor = Color.FromName("#333333");

            InfectionSpecimen.Attributes.Add("Style", "color: #333333");
            InfectionSpecimen.Attributes.Add("Style", "background-color: #77cf9c");
          }


          Label Label_HAI = (Label)GridView_IPS_Infection_List.Rows[i].FindControl("Label_HAI");
          HtmlTableCell InfectionHAI = (HtmlTableCell)GridView_IPS_Infection_List.Rows[i].FindControl("InfectionHAI");

          if (Label_IsActive.Text == "Yes")
          {
            if (Label_HAI.Text == "Not Required")
            {
              Label_HAI.ForeColor = Color.FromName("#333333");

              InfectionHAI.Attributes.Add("Style", "color: #333333");
              InfectionHAI.Attributes.Add("Style", "background-color: #77cf9c");
            }
            else if (Label_HAI.Text == "Complete")
            {
              Label_HAI.ForeColor = Color.FromName("#333333");

              InfectionHAI.Attributes.Add("Style", "color: #333333");
              InfectionHAI.Attributes.Add("Style", "background-color: #77cf9c");
            }
            else if (Label_HAI.Text == "Incomplete")
            {
              Label_HAI.ForeColor = Color.FromName("#333333");

              InfectionHAI.Attributes.Add("Style", "color: #333333");
              InfectionHAI.Attributes.Add("Style", "background-color: #d46e6e");
            }
          }
          else if (Label_IsActive.Text == "No")
          {
            Label_HAI.ForeColor = Color.FromName("#333333");

            InfectionHAI.Attributes.Add("Style", "color: #333333");
            InfectionHAI.Attributes.Add("Style", "background-color: #77cf9c");
          }


          Label Label_Site = (Label)GridView_IPS_Infection_List.Rows[i].FindControl("Label_Site");
          HtmlTableCell InfectionHAISite = (HtmlTableCell)GridView_IPS_Infection_List.Rows[i].FindControl("InfectionHAISite");

          if (Label_IsActive.Text == "Yes")
          {
            if (Label_Site.Text == "Linked Site Required")
            {
              Label_Site.ForeColor = Color.FromName("#333333");

              InfectionHAISite.Attributes.Add("Style", "color: #333333");
              InfectionHAISite.Attributes.Add("Style", "background-color: #d46e6e");
            }
          }
        }
      }
    }

    protected void GridView_IPS_Infection_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_InfectionTotalRecords = (Label)e.Row.FindControl("Label_InfectionTotalRecords");
          Label_InfectionTotalRecords.Text = Label_HiddenInfectionTotalRecords.Text;          
        }


        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_CaptureNewInfection = (Button)e.Row.FindControl("Button_CaptureNewInfection");

          FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
          DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
          DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
          DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
          DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
          DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
          {
            Security = "0";
            Button_CaptureNewInfection.Visible = true;
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
          {
            Security = "0";
            Button_CaptureNewInfection.Visible = false;
          }

          if (Security == "1")
          {
            Security = "0";
            Button_CaptureNewInfection.Visible = false;
          }
        }
      }
    }

    protected void Button_CaptureNewInfection_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "#CurrentInfection"), false);
    }

    public string GetSpecimenLink(object ips_Infection_Id, object specimen)
    {
      string FinalURL = "";
      if (ips_Infection_Id != null && specimen != null)
      {
        FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + ips_Infection_Id.ToString() + "") + "' Class='Controls_HyperLink_ColorBackground'>" + specimen.ToString() + "</a>";
      }

      return FinalURL;
    }

    public string GetInfectionLink(object ips_Infection_Id, object ips_Infection_Completed)
    {
      string FinalURL = "";
      if (ips_Infection_Id != null && ips_Infection_Completed != null)
      {
        FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + ips_Infection_Id.ToString() + "#CurrentInfection") + "' Class='Controls_HyperLink_ColorBackground'>" + ips_Infection_Completed.ToString() + "</a>";
      }

      return FinalURL;
    }

    public string GetHAILink(object ips_Infection_Id, object ips_Infection_Completed, object hai, object ips_HAI_Id)
    {
      string FinalURL = "";
      if (ips_Infection_Id != null && ips_Infection_Completed != null && hai != null && ips_HAI_Id != null)
      {
        if (ips_Infection_Completed.ToString() == "Incomplete")
        {
          FinalURL = hai.ToString();
        }
        else
        {
          if (hai.ToString() == "Not Required")
          {
            FinalURL = hai.ToString();
          }
          else
          {
            FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + ips_Infection_Id.ToString() + "&IPSHAIId=" + ips_HAI_Id.ToString() + "#CurrentHAI") + "' Class='Controls_HyperLink_ColorBackground'>" + hai.ToString() + "</a>";
          }
        }
      }

      return FinalURL;
    }
    //---END--- --Infection--//


    //--START-- --CurrentInfection--//
    protected void SetCurrentInfectionVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["IPSInfectionId"]))
      {
        SetCurrentInfectionVisibility_Insert();
      }
      else
      {
        SetCurrentInfectionVisibility_Edit();
      }
    }

    protected void SetCurrentInfectionVisibility_Insert()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        FormView_IPS_Infection_Form.ChangeMode(FormViewMode.Insert);
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_IPS_Infection_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_IPS_Infection_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetCurrentInfectionVisibility_Edit()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
      {
        Security = "0";
        if (IPSInfectionCompleted == "False")
        {
          FormView_IPS_Infection_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_IPS_Infection_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
        {
          FormView_IPS_Infection_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_IPS_Infection_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_IPS_Infection_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_IPS_Infection_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentInfectionVisible()
    {
      if (FormView_IPS_Infection_Form.CurrentMode == FormViewMode.Insert)
      {
        DropDownList DropDownList_InsertInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubTypeList");
        DropDownList_InsertInfectionSubTypeList.Items.Clear();
        DropDownList_InsertInfectionSubTypeList.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Type", CultureInfo.CurrentCulture), ""));

        DropDownList DropDownList_InsertInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubSubTypeList");
        DropDownList_InsertInfectionSubSubTypeList.Items.Clear();
        DropDownList_InsertInfectionSubSubTypeList.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Sub Type", CultureInfo.CurrentCulture), ""));

        DropDownList DropDownList_InsertInfectionSiteInfectionId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSiteInfectionId");
        DropDownList_InsertInfectionSiteInfectionId.Items.Clear();
        DropDownList_InsertInfectionSiteInfectionId.Items.Insert(0, new ListItem(Convert.ToString("Select Linked Site", CultureInfo.CurrentCulture), ""));

        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionTypeList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubTypeList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubSubTypeList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSiteList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSiteInfectionId")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBox)FormView_IPS_Infection_Form.FindControl("CheckBox_InsertInfectionScreening")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBoxList)FormView_IPS_Infection_Form.FindControl("CheckBoxList_InsertScreeningTypeTypeList")).Attributes.Add("OnClick", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionScreeningReasonList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_InsertInfectionSummary")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_InsertInfectionSummary")).Attributes.Add("OnInput", "Validation_Form();");

        Button Button_InsertAdd = (Button)FormView_IPS_Infection_Form.FindControl("Button_InsertAdd");
        if (Button_InsertAdd != null)
        {
          form_IPS.DefaultButton = Button_InsertAdd.UniqueID;
        }
      }

      if (FormView_IPS_Infection_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionCategoryList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionTypeList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubTypeList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubSubTypeList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteInfectionId")).Attributes.Add("OnChange", "Validation_Form();");
        ((CheckBox)FormView_IPS_Infection_Form.FindControl("CheckBox_EditInfectionScreening")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((CheckBoxList)FormView_IPS_Infection_Form.FindControl("CheckBoxList_EditScreeningTypeTypeList")).Attributes.Add("OnClick", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionScreeningReasonList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_EditInfectionSummary")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_EditInfectionSummary")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_IPS_Infection_Form.FindControl("CheckBox_EditIsActive")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_EditRejectedReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_EditRejectedReason")).Attributes.Add("OnInput", "Validation_Form();");

        Button Button_EditUpdate = (Button)FormView_IPS_Infection_Form.FindControl("Button_EditUpdate");
        if (Button_EditUpdate != null)
        {
          form_IPS.DefaultButton = Button_EditUpdate.UniqueID;
        }
      }
    }


    protected void FormView_IPS_Infection_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_Infection();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfection);

          ((Label)FormView_IPS_Infection_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_IPS_Infection_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";

          InsertRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
          Session["FacilityId"] = FromDataBase_FacilityId_Current.FacilityId;

          Session["IPS_Infection_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Session["FacilityId"].ToString(), "37");

          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_VisitInformation_Id"].DefaultValue = Request.QueryString["IPSVisitInformationId"];
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_ReportNumber"].DefaultValue = Session["IPS_Infection_ReportNumber"].ToString();

          DropDownList DropDownList_InsertInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubTypeList");
          DropDownList DropDownList_InsertInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubSubTypeList");
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_SubType_List"].DefaultValue = DropDownList_InsertInfectionSubTypeList.SelectedValue;
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_SubSubType_List"].DefaultValue = DropDownList_InsertInfectionSubSubTypeList.SelectedValue;

          DropDownList DropDownList_InsertInfectionSiteInfectionId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSiteInfectionId");
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_Site_Infection_Id"].DefaultValue = DropDownList_InsertInfectionSiteInfectionId.SelectedValue;

          DropDownList DropDownList_InsertInfectionUnitId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionUnitId");
          SqlDataSource_IPS_Infection_Form.InsertParameters["Unit_Id"].DefaultValue = DropDownList_InsertInfectionUnitId.SelectedValue;

          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_Completed"].DefaultValue = "false";
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_CompletedDate"].DefaultValue = "";

          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_History"].DefaultValue = "";
          SqlDataSource_IPS_Infection_Form.InsertParameters["IPS_Infection_IsActive"].DefaultValue = "true";

          Session.Remove("FacilityId");
          Session.Remove("IPS_Infection_ReportNumber");
        }
      }
    }

    protected string InsertValidation_Infection()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_InsertInfectionCategoryList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionCategoryList");
      DropDownList DropDownList_InsertInfectionTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionTypeList");
      DropDownList DropDownList_InsertInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubTypeList");
      DropDownList DropDownList_InsertInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubSubTypeList");
      DropDownList DropDownList_InsertInfectionSiteList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSiteList");
      DropDownList DropDownList_InsertInfectionSiteInfectionId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSiteInfectionId");
      CheckBox CheckBox_InsertInfectionScreening = (CheckBox)FormView_IPS_Infection_Form.FindControl("CheckBox_InsertInfectionScreening");
      CheckBoxList CheckBoxList_InsertScreeningTypeTypeList = (CheckBoxList)FormView_IPS_Infection_Form.FindControl("CheckBoxList_InsertScreeningTypeTypeList");
      DropDownList DropDownList_InsertInfectionScreeningReasonList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionScreeningReasonList");
      DropDownList DropDownList_InsertInfectionUnitId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionUnitId");
      TextBox TextBox_InsertInfectionSummary = (TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_InsertInfectionSummary");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertInfectionCategoryList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (DropDownList_InsertInfectionCategoryList.SelectedValue == "4799")
        {
          if (string.IsNullOrEmpty(DropDownList_InsertInfectionTypeList.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_InsertInfectionSubTypeList.Items.Count > 1)
          {
            if (string.IsNullOrEmpty(DropDownList_InsertInfectionSubTypeList.SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }

          if (DropDownList_InsertInfectionSubSubTypeList.Items.Count > 1)
          {
            if (string.IsNullOrEmpty(DropDownList_InsertInfectionSubSubTypeList.SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }

          if (string.IsNullOrEmpty(DropDownList_InsertInfectionSiteList.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_InsertInfectionSiteList.SelectedValue != "4996")
          {
            if (string.IsNullOrEmpty(DropDownList_InsertInfectionSiteInfectionId.SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }

          if (string.IsNullOrEmpty(TextBox_InsertInfectionSummary.Text))
          {
            InvalidForm = "Yes";
          }
        }

        if (CheckBox_InsertInfectionScreening.Checked == true)
        {
          string ScreeningTypeCompleted = "No";
          foreach (System.Web.UI.WebControls.ListItem ListItem_ScreeningTypeTypeList in CheckBoxList_InsertScreeningTypeTypeList.Items)
          {
            if (ListItem_ScreeningTypeTypeList.Selected == true)
            {
              ScreeningTypeCompleted = "Yes";
              break;
            }
            else if (ListItem_ScreeningTypeTypeList.Selected == false)
            {
              ScreeningTypeCompleted = "No";
            }
          }

          if (ScreeningTypeCompleted == "No")
          {
            InvalidForm = "Yes";
          }
          

          if (string.IsNullOrEmpty(DropDownList_InsertInfectionScreeningReasonList.SelectedValue))
          {
            InvalidForm = "Yes";
          }          
        }

        if (string.IsNullOrEmpty(DropDownList_InsertInfectionUnitId.SelectedValue))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_Infection_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["IPS_Infection_Id"] = e.Command.Parameters["@IPS_Infection_Id"].Value;

        if (!string.IsNullOrEmpty(Session["IPS_Infection_Id"].ToString()))
        {
          FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Session["IPS_Infection_Id"].ToString());
          string IsHAI = FromDataBase_IsHAI_Current.IsHAI;

          if (IsHAI == "True")
          {
            string SQLStringInsertHAI = "INSERT INTO Form_IPS_HAI ( IPS_Infection_Id , IPS_HAI_BundleCompliance_TPN , IPS_HAI_BundleCompliance_EnteralFeeding , IPS_HAI_Investigation_Completed , IPS_HAI_Investigation_CompletedDate , IPS_HAI_CreatedDate , IPS_HAI_CreatedBy , IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy , IPS_HAI_History ) VALUES ( @IPS_Infection_Id , @IPS_HAI_BundleCompliance_TPN , @IPS_HAI_BundleCompliance_EnteralFeeding , @IPS_HAI_Investigation_Completed , @IPS_HAI_Investigation_CompletedDate , @IPS_HAI_CreatedDate , @IPS_HAI_CreatedBy , @IPS_HAI_ModifiedDate , @IPS_HAI_ModifiedBy , @IPS_HAI_History )";
            using (SqlCommand SqlCommand_InsertHAI = new SqlCommand(SQLStringInsertHAI))
            {
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_Infection_Id", Session["IPS_Infection_Id"].ToString());
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_BundleCompliance_TPN", false);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_BundleCompliance_EnteralFeeding", false);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_Completed", false);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_CompletedDate", DBNull.Value);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_CreatedDate", DateTime.Now);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_CreatedBy", Request.ServerVariables["LOGON_USER"].ToString());
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedDate", DateTime.Now);
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedBy", Request.ServerVariables["LOGON_USER"].ToString());
              SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_History", DBNull.Value);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertHAI);
            }
          }


          CheckBoxList CheckBoxList_InsertScreeningTypeTypeList = (CheckBoxList)FormView_IPS_Infection_Form.FindControl("CheckBoxList_InsertScreeningTypeTypeList");
          foreach (System.Web.UI.WebControls.ListItem ListItem_ScreeningTypeTypeList in CheckBoxList_InsertScreeningTypeTypeList.Items)
          {
            if (ListItem_ScreeningTypeTypeList.Selected)
            {
              string SQLStringInsertScreeningType = "INSERT INTO Form_IPS_Infection_ScreeningType ( IPS_Infection_Id , IPS_ScreeningType_Type_List , IPS_ScreeningType_CreatedDate , IPS_ScreeningType_CreatedBy ) VALUES ( @IPS_Infection_Id , @IPS_ScreeningType_Type_List , @IPS_ScreeningType_CreatedDate , @IPS_ScreeningType_CreatedBy )";
              using (SqlCommand SqlCommand_InsertScreeningType = new SqlCommand(SQLStringInsertScreeningType))
              {
                SqlCommand_InsertScreeningType.Parameters.AddWithValue("@IPS_Infection_Id", Session["IPS_Infection_Id"].ToString());
                SqlCommand_InsertScreeningType.Parameters.AddWithValue("@IPS_ScreeningType_Type_List", ListItem_ScreeningTypeTypeList.Value.ToString());
                SqlCommand_InsertScreeningType.Parameters.AddWithValue("@IPS_ScreeningType_CreatedDate", DateTime.Now);
                SqlCommand_InsertScreeningType.Parameters.AddWithValue("@IPS_ScreeningType_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertScreeningType);
              }
            }
          }
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Session["IPS_Infection_Id"] + "#CurrentInfection"), false);
      }
    }


    protected void FormView_IPS_Infection_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        HiddenField HiddenField_EditModifiedDate = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditModifiedDate");
        Session["OLDIPSInfectionModifiedDate"] = HiddenField_EditModifiedDate.Value;
        object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareIPSInfection = (DataView)SqlDataSource_IPS_Infection_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareIPSInfection = DataView_CompareIPSInfection[0];
        Session["DBIPSInfectionModifiedDate"] = Convert.ToString(DataRowView_CompareIPSInfection["IPS_Infection_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIPSInfectionModifiedBy"] = Convert.ToString(DataRowView_CompareIPSInfection["IPS_Infection_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfection);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_IPS_Infection_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;

          EditRegisterPostBackControl();
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_Infection();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfection);
            ((Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_IPS_Infection_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";

            EditRegisterPostBackControl();
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
            e.NewValues["IPS_Infection_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["IPS_Infection_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

            DataView DataView_IPSInfection = (DataView)SqlDataSource_IPS_Infection_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_IPSInfection = DataView_IPSInfection[0];
            Session["IPSInfectionHistory"] = Convert.ToString(DataRowView_IPSInfection["IPS_Infection_History"], CultureInfo.CurrentCulture);

            Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();
            e.NewValues["IPS_Infection_History"] = Session["IPSInfectionHistory"].ToString();

            Session.Remove("IPSInfectionHistory");
            Session.Remove("History");


            DropDownList DropDownList_EditInfectionTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionTypeList");
            DropDownList DropDownList_EditInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubTypeList");
            DropDownList DropDownList_EditInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubSubTypeList");

            e.NewValues["IPS_Infection_Type_List"] = DropDownList_EditInfectionTypeList.SelectedValue;
            e.NewValues["IPS_Infection_SubType_List"] = DropDownList_EditInfectionSubTypeList.SelectedValue;
            e.NewValues["IPS_Infection_SubSubType_List"] = DropDownList_EditInfectionSubSubTypeList.SelectedValue;


            DropDownList DropDownList_EditInfectionSiteInfectionId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteInfectionId");
            e.NewValues["IPS_Infection_Site_Infection_Id"] = DropDownList_EditInfectionSiteInfectionId.SelectedValue;


            DropDownList DropDownList_EditInfectionUnitId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionUnitId");
            e.NewValues["Unit_Id"] = DropDownList_EditInfectionUnitId.SelectedValue;
          }
        }
      }
    }

    protected string EditValidation_Infection()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditInfectionCategoryList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionCategoryList");
      DropDownList DropDownList_EditInfectionTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionTypeList");
      DropDownList DropDownList_EditInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubTypeList");
      DropDownList DropDownList_EditInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubSubTypeList");
      DropDownList DropDownList_EditInfectionSiteList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteList");
      DropDownList DropDownList_EditInfectionSiteInfectionId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteInfectionId");
      CheckBox CheckBox_EditInfectionScreening = (CheckBox)FormView_IPS_Infection_Form.FindControl("CheckBox_EditInfectionScreening");
      CheckBoxList CheckBoxList_EditScreeningTypeTypeList = (CheckBoxList)FormView_IPS_Infection_Form.FindControl("CheckBoxList_EditScreeningTypeTypeList");
      DropDownList DropDownList_EditInfectionScreeningReasonList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionScreeningReasonList");
      DropDownList DropDownList_EditInfectionUnitId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionUnitId");
      TextBox TextBox_EditInfectionSummary = (TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_EditInfectionSummary");
      CheckBox CheckBox_EditIsActive = (CheckBox)FormView_IPS_Infection_Form.FindControl("CheckBox_EditIsActive");
      TextBox TextBox_EditRejectedReason = (TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_EditRejectedReason");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_EditInfectionCategoryList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (DropDownList_EditInfectionCategoryList.SelectedValue == "4799")
        {
          if (string.IsNullOrEmpty(DropDownList_EditInfectionTypeList.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_EditInfectionSubTypeList.Items.Count > 1)
          {
            if (string.IsNullOrEmpty(DropDownList_EditInfectionSubTypeList.SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }

          if (DropDownList_EditInfectionSubSubTypeList.Items.Count > 1)
          {
            if (string.IsNullOrEmpty(DropDownList_EditInfectionSubSubTypeList.SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }

          if (string.IsNullOrEmpty(DropDownList_EditInfectionSiteList.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_EditInfectionSiteList.SelectedValue != "4996")
          {
            if (string.IsNullOrEmpty(DropDownList_EditInfectionSiteInfectionId.SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }

          if (string.IsNullOrEmpty(TextBox_EditInfectionSummary.Text))
          {
            InvalidForm = "Yes";
          }          
        }

        if (CheckBox_EditInfectionScreening.Checked == true)
        {
          string ScreeningTypeCompleted = "No";
          foreach (ListItem ListItem_ScreeningTypeTypeList in CheckBoxList_EditScreeningTypeTypeList.Items)
          {
            if (ListItem_ScreeningTypeTypeList.Selected == true)
            {
              ScreeningTypeCompleted = "Yes";
              break;
            }
            else if (ListItem_ScreeningTypeTypeList.Selected == false)
            {
              ScreeningTypeCompleted = "No";
            }
          }

          if (ScreeningTypeCompleted == "No")
          {
            InvalidForm = "Yes";
          }


          if (string.IsNullOrEmpty(DropDownList_EditInfectionScreeningReasonList.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(DropDownList_EditInfectionUnitId.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (CheckBox_EditIsActive.Checked == false)
        {
          if (string.IsNullOrEmpty(TextBox_EditRejectedReason.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_Infection_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Request.QueryString["IPSInfectionId"]);
            string IsHAI = FromDataBase_IsHAI_Current.IsHAI;

            if (IsHAI == "True")
            {
              string IPSHAIId = "";
              string SQLStringIPSInfectionHAI = "SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
              using (SqlCommand SqlCommand_IPSInfectionHAI = new SqlCommand(SQLStringIPSInfectionHAI))
              {
                SqlCommand_IPSInfectionHAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                DataTable DataTable_IPSInfectionHAI;
                using (DataTable_IPSInfectionHAI = new DataTable())
                {
                  DataTable_IPSInfectionHAI.Locale = CultureInfo.CurrentCulture;
                  DataTable_IPSInfectionHAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IPSInfectionHAI).Copy();
                  if (DataTable_IPSInfectionHAI.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_IPSInfectionHAI.Rows)
                    {
                      IPSHAIId = DataRow_Row["IPS_HAI_Id"].ToString();
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(IPSHAIId))
              {
                string SQLStringInsertHAI = "INSERT INTO Form_IPS_HAI ( IPS_Infection_Id , IPS_HAI_BundleCompliance_TPN , IPS_HAI_BundleCompliance_EnteralFeeding , IPS_HAI_Investigation_Completed , IPS_HAI_Investigation_CompletedDate , IPS_HAI_CreatedDate , IPS_HAI_CreatedBy , IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy , IPS_HAI_History ) VALUES ( @IPS_Infection_Id , @IPS_HAI_BundleCompliance_TPN , @IPS_HAI_BundleCompliance_EnteralFeeding , @IPS_HAI_Investigation_Completed , @IPS_HAI_Investigation_CompletedDate , @IPS_HAI_CreatedDate , @IPS_HAI_CreatedBy , @IPS_HAI_ModifiedDate , @IPS_HAI_ModifiedBy , @IPS_HAI_History )";
                using (SqlCommand SqlCommand_InsertHAI = new SqlCommand(SQLStringInsertHAI))
                {
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_BundleCompliance_TPN", false);
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_BundleCompliance_EnteralFeeding", false);
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_Completed", false);
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_CompletedDate", DBNull.Value);
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_CreatedDate", DateTime.Now);
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_CreatedBy", Request.ServerVariables["LOGON_USER"].ToString());
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedDate", DateTime.Now);
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedBy", Request.ServerVariables["LOGON_USER"].ToString());
                  SqlCommand_InsertHAI.Parameters.AddWithValue("@IPS_HAI_History", DBNull.Value);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertHAI);
                }
              }


              string SQLStringDeleteBundleComplianceQA = "DELETE FROM Form_IPS_HAI_BundleComplianceQA WHERE IPS_HAI_Id IN ( SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id )";
              using (SqlCommand SqlCommand_DeleteBundleComplianceQA = new SqlCommand(SQLStringDeleteBundleComplianceQA))
              {
                SqlCommand_DeleteBundleComplianceQA.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteBundleComplianceQA);
              }
            }
            else
            {
              string SQLStringDeleteHAI_BundleComplianceQA = "DELETE FROM Form_IPS_HAI_BundleComplianceQA WHERE IPS_HAI_Id IN (SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id)";
              using (SqlCommand SqlCommand_DeleteHAI_BundleComplianceQA = new SqlCommand(SQLStringDeleteHAI_BundleComplianceQA))
              {
                SqlCommand_DeleteHAI_BundleComplianceQA.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteHAI_BundleComplianceQA);
              }

              string SQLStringDeleteHAI_LabReports = "DELETE FROM Form_IPS_HAI_LabReports WHERE IPS_HAI_Id IN (SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id)";
              using (SqlCommand SqlCommand_DeleteHAI_LabReports = new SqlCommand(SQLStringDeleteHAI_LabReports))
              {
                SqlCommand_DeleteHAI_LabReports.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteHAI_LabReports);
              }

              string SQLStringDeleteHAI_PredisposingCondition = "DELETE FROM Form_IPS_HAI_PredisposingCondition WHERE IPS_HAI_Id IN (SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id)";
              using (SqlCommand SqlCommand_DeleteHAI_PredisposingCondition = new SqlCommand(SQLStringDeleteHAI_PredisposingCondition))
              {
                SqlCommand_DeleteHAI_PredisposingCondition.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteHAI_PredisposingCondition);
              }

              string SQLStringDeleteHAI_Measures = "DELETE FROM Form_IPS_HAI_Measures WHERE IPS_HAI_Id IN (SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id)";
              using (SqlCommand SqlCommand_DeleteHAI_Measures = new SqlCommand(SQLStringDeleteHAI_Measures))
              {
                SqlCommand_DeleteHAI_Measures.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteHAI_Measures);
              }

              string SQLStringDeleteHAI = "DELETE FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
              using (SqlCommand SqlCommand_DeleteHAI = new SqlCommand(SQLStringDeleteHAI))
              {
                SqlCommand_DeleteHAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteHAI);
              }
            }


            string SQLStringIPSInfectionScreeningType = "DELETE FROM Form_IPS_Infection_ScreeningType WHERE IPS_Infection_Id = @IPS_Infection_Id";
            using (SqlCommand SqlCommand_IPSInfectionScreeningType = new SqlCommand(SQLStringIPSInfectionScreeningType))
            {
              SqlCommand_IPSInfectionScreeningType.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);

              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_IPSInfectionScreeningType);
            }

            CheckBoxList CheckBoxList_EditScreeningTypeTypeList = (CheckBoxList)FormView_IPS_Infection_Form.FindControl("CheckBoxList_EditScreeningTypeTypeList");
            foreach (System.Web.UI.WebControls.ListItem ListItem_ScreeningTypeTypeList in CheckBoxList_EditScreeningTypeTypeList.Items)
            {
              if (ListItem_ScreeningTypeTypeList.Selected)
              {
                string SQLStringInsertScreeningType = "INSERT INTO Form_IPS_Infection_ScreeningType ( IPS_Infection_Id , IPS_ScreeningType_Type_List , IPS_ScreeningType_CreatedDate , IPS_ScreeningType_CreatedBy ) VALUES ( @IPS_Infection_Id , @IPS_ScreeningType_Type_List , @IPS_ScreeningType_CreatedDate , @IPS_ScreeningType_CreatedBy )";
                using (SqlCommand SqlCommand_InsertScreeningType = new SqlCommand(SQLStringInsertScreeningType))
                {
                  SqlCommand_InsertScreeningType.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                  SqlCommand_InsertScreeningType.Parameters.AddWithValue("@IPS_ScreeningType_Type_List", ListItem_ScreeningTypeTypeList.Value.ToString());
                  SqlCommand_InsertScreeningType.Parameters.AddWithValue("@IPS_ScreeningType_CreatedDate", DateTime.Now);
                  SqlCommand_InsertScreeningType.Parameters.AddWithValue("@IPS_ScreeningType_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertScreeningType);
                }
              }
            }


            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentInfection"), false);
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;
            form_IPS.DefaultButton = null;
            ScriptManager.RegisterStartupScript(UpdatePanel_IPS, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance Print", "InfoQuest_Print.aspx?PrintPage=Form_IPS&PrintValue=" + Request.QueryString["IPSInfectionId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_IPS, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;
            form_IPS.DefaultButton = null;
            ScriptManager.RegisterStartupScript(UpdatePanel_IPS, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance Email", "InfoQuest_Email.aspx?EmailPage=Form_IPS&EmailValue=" + Request.QueryString["IPSInfectionId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_IPS, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }
    

    protected void FormView_IPS_Infection_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_IPS_Infection_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_IPS_Infection_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      DropDownList DropDownList_EditInfectionTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionTypeList");
      DropDownList DropDownList_EditInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubTypeList");
      DropDownList DropDownList_EditInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubSubTypeList");

      DataView DataView_InfectionType = (DataView)SqlDataSource_IPS_Infection_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_InfectionType = DataView_InfectionType[0];
      DropDownList_EditInfectionTypeList.SelectedValue = Convert.ToString(DataRowView_InfectionType["IPS_Infection_Type_List"], CultureInfo.CurrentCulture);
      DropDownList_EditInfectionSubTypeList.SelectedValue = Convert.ToString(DataRowView_InfectionType["IPS_Infection_SubType_List"], CultureInfo.CurrentCulture);
      DropDownList_EditInfectionSubSubTypeList.SelectedValue = Convert.ToString(DataRowView_InfectionType["IPS_Infection_SubSubType_List"], CultureInfo.CurrentCulture);


      FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
      string FacilityId = FromDataBase_FacilityId_Current.FacilityId;

      DropDownList DropDownList_EditInfectionUnitId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionUnitId");
      DataView DataView_InfectionUnitId = (DataView)SqlDataSource_IPS_Infection_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_InfectionUnitId = DataView_InfectionUnitId[0];
      DropDownList_EditInfectionUnitId.SelectedValue = Convert.ToString(DataRowView_InfectionUnitId["Unit_Id"], CultureInfo.CurrentCulture);
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["Facility_Id"].DefaultValue = FacilityId;
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Infection LEFT JOIN Form_IPS_VisitInformation ON Form_IPS_Infection.IPS_VisitInformation_Id = Form_IPS_VisitInformation.IPS_VisitInformation_Id";
      SqlDataSource_IPS_EditInfectionUnitId.SelectParameters["TableWHERE"].DefaultValue = "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"] + " ";


      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 37";
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
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_EditPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_EditPrint")).Visible = true;
      }

      if (Email == "False")
      {
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_EditEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_EditEmail")).Visible = true;
      }

      Email = "";
      Print = "";
    }

    protected void ReadOnlyDataBound()
    {
      Session["IPSInfectionCategoryName"] = "";
      Session["IPSInfectionTypeName"] = "";
      Session["IPSInfectionSubTypeName"] = "";
      Session["IPSInfectionSubSubTypeName"] = "";
      Session["IPSInfectionSiteName"] = "";
      Session["IPSInfectionSiteInfectionReportNumber"] = "";
      Session["IPSInfectionScreeningReasonName"] = "";
      Session["UnitName"] = "";
      string SQLStringInfection = "SELECT IPS_Infection_Category_Name , IPS_Infection_Type_Name , IPS_Infection_SubType_Name , IPS_Infection_SubSubType_Name , IPS_Infection_Site_Name , IPS_Infection_Site_Infection_ReportNumber , IPS_Infection_ScreeningReason_Name , Unit_Name FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["IPSInfectionCategoryName"] = DataRow_Row["IPS_Infection_Category_Name"];
              Session["IPSInfectionTypeName"] = DataRow_Row["IPS_Infection_Type_Name"];
              Session["IPSInfectionSubTypeName"] = DataRow_Row["IPS_Infection_SubType_Name"];
              Session["IPSInfectionSubSubTypeName"] = DataRow_Row["IPS_Infection_SubSubType_Name"];
              Session["IPSInfectionSiteName"] = DataRow_Row["IPS_Infection_Site_Name"];
              Session["IPSInfectionSiteInfectionReportNumber"] = DataRow_Row["IPS_Infection_Site_Infection_ReportNumber"];
              Session["IPSInfectionScreeningReasonName"] = DataRow_Row["IPS_Infection_ScreeningReason_Name"];
              Session["UnitName"] = DataRow_Row["Unit_Name"];
            }
          }
        }
      }


      Label Label_ItemInfectionCategoryList = (Label)FormView_IPS_Infection_Form.FindControl("Label_ItemInfectionCategoryList");
      Label_ItemInfectionCategoryList.Text = Session["IPSInfectionCategoryName"].ToString();

      Label Label_ItemInfectionTypeList = (Label)FormView_IPS_Infection_Form.FindControl("Label_ItemInfectionTypeList");
      Label_ItemInfectionTypeList.Text = Session["IPSInfectionTypeName"].ToString();

      Label Label_ItemInfectionSubTypeList = (Label)FormView_IPS_Infection_Form.FindControl("Label_ItemInfectionSubTypeList");
      Label_ItemInfectionSubTypeList.Text = Session["IPSInfectionSubTypeName"].ToString();

      Label Label_ItemInfectionSubSubTypeList = (Label)FormView_IPS_Infection_Form.FindControl("Label_ItemInfectionSubSubTypeList");
      Label_ItemInfectionSubSubTypeList.Text = Session["IPSInfectionSubSubTypeName"].ToString();

      Label Label_ItemInfectionSiteList = (Label)FormView_IPS_Infection_Form.FindControl("Label_ItemInfectionSiteList");
      Label_ItemInfectionSiteList.Text = Session["IPSInfectionSiteName"].ToString();

      Label Label_ItemInfectionSiteInfectionId = (Label)FormView_IPS_Infection_Form.FindControl("Label_ItemInfectionSiteInfectionId");
      Label_ItemInfectionSiteInfectionId.Text = Session["IPSInfectionSiteInfectionReportNumber"].ToString();

      Label Label_ItemInfectionScreeningReasonList = (Label)FormView_IPS_Infection_Form.FindControl("Label_ItemInfectionScreeningReasonList");
      Label_ItemInfectionScreeningReasonList.Text = Session["IPSInfectionScreeningReasonName"].ToString();

      Label Label_ItemInfectionUnit = (Label)FormView_IPS_Infection_Form.FindControl("Label_ItemInfectionUnit");
      Label_ItemInfectionUnit.Text = Session["UnitName"].ToString();

      Session.Remove("IPSInfectionCategoryName");
      Session.Remove("IPSInfectionTypeName");
      Session.Remove("IPSInfectionSubTypeName");
      Session.Remove("IPSInfectionSubSubTypeName");
      Session.Remove("IPSInfectionSiteName");
      Session.Remove("IPSInfectionSiteInfectionReportNumber");
      Session.Remove("IPSInfectionScreeningReasonName");
      Session.Remove("UnitName");


      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 37";
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
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_ItemPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_ItemPrint")).Visible = true;
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance Print", "InfoQuest_Print.aspx?PrintPage=Form_IPS&PrintValue=" + Request.QueryString["IPSInfectionId"] + "") + "')";
      }

      if (Email == "False")
      {
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_ItemEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_ItemEmail")).Visible = true;
        ((Button)FormView_IPS_Infection_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Surveillance Email", "InfoQuest_Email.aspx?EmailPage=Form_IPS&EmailValue=" + Request.QueryString["IPSInfectionId"] + "") + "')";
      }

      Email = "";
      Print = "";
    }

    
    protected void InsertRegisterPostBackControl()
    {
      DropDownList DropDownList_InsertInfectionTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionTypeList");
      DropDownList DropDownList_InsertInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubTypeList");
      DropDownList DropDownList_InsertInfectionSiteList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSiteList");

      ScriptManager ScriptManager_Insert = ScriptManager.GetCurrent(Page);

      ScriptManager_Insert.RegisterPostBackControl(DropDownList_InsertInfectionTypeList);
      ScriptManager_Insert.RegisterPostBackControl(DropDownList_InsertInfectionSubTypeList);
      ScriptManager_Insert.RegisterPostBackControl(DropDownList_InsertInfectionSiteList);

      FileRegisterPostBackControl();
    }

    protected void DropDownList_InsertInfectionTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertInfectionTypeList = (DropDownList)sender;
      DropDownList DropDownList_InsertInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubTypeList");
      DropDownList DropDownList_InsertInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubSubTypeList");

      ToolkitScriptManager_IPS.SetFocus(DropDownList_InsertInfectionTypeList);

      DropDownList_InsertInfectionSubTypeList.Items.Clear();
      DropDownList_InsertInfectionSubSubTypeList.Items.Clear();

      SqlDataSource_IPS_InsertInfectionSubTypeList.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_InsertInfectionTypeList.SelectedValue;
      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectParameters["ListItem_Parent"].DefaultValue = "";

      DropDownList_InsertInfectionSubTypeList.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Type", CultureInfo.CurrentCulture), ""));
      DropDownList_InsertInfectionSubSubTypeList.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Sub Type", CultureInfo.CurrentCulture), ""));

      DropDownList_InsertInfectionSubTypeList.DataBind();
      DropDownList_InsertInfectionSubSubTypeList.DataBind();
    }

    protected void DropDownList_InsertInfectionSubTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertInfectionSubTypeList = (DropDownList)sender;
      DropDownList DropDownList_InsertInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSubSubTypeList");

      ToolkitScriptManager_IPS.SetFocus(DropDownList_InsertInfectionSubTypeList);

      DropDownList_InsertInfectionSubSubTypeList.Items.Clear();

      SqlDataSource_IPS_InsertInfectionSubSubTypeList.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_InsertInfectionSubTypeList.SelectedValue;

      DropDownList_InsertInfectionSubSubTypeList.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Sub Type", CultureInfo.CurrentCulture), ""));

      DropDownList_InsertInfectionSubSubTypeList.DataBind();
    }

    protected void DropDownList_InsertInfectionSiteList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertInfectionSiteList = (DropDownList)sender;
      DropDownList DropDownList_InsertInfectionSiteInfectionId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_InsertInfectionSiteInfectionId");

      ToolkitScriptManager_IPS.SetFocus(DropDownList_InsertInfectionSiteList);

      DropDownList_InsertInfectionSiteInfectionId.Items.Clear();

      SqlDataSource_IPS_InsertInfectionSiteInfectionId.SelectParameters["IPS_Infection_Site_List"].DefaultValue = DropDownList_InsertInfectionSiteList.SelectedValue;

      DropDownList_InsertInfectionSiteInfectionId.Items.Insert(0, new ListItem(Convert.ToString("Select Linked Site", CultureInfo.CurrentCulture), ""));

      DropDownList_InsertInfectionSiteInfectionId.DataBind();
    }

    protected void DropDownList_InsertInfectionSiteList_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertInfectionSiteList = (DropDownList)sender;

      Session["IPSInfectionId"] = "";
      string SQLStringInfection = "SELECT IPS_Infection_Id FROM Form_IPS_Infection WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id AND IPS_Infection_Category_List IN (4799)";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["IPSInfectionId"] = DataRow_Row["IPS_Infection_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSInfectionId"].ToString()))
      {
        DropDownList_InsertInfectionSiteList.Items.Remove(DropDownList_InsertInfectionSiteList.Items.FindByValue("4997"));
        DropDownList_InsertInfectionSiteList.Items.Remove(DropDownList_InsertInfectionSiteList.Items.FindByValue("4998"));
      }

      Session.Remove("IPSInfectionId");
    }

    protected void HiddenField_InsertScreeningTypeTypeListTotal_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_InsertScreeningTypeTypeListTotal = (HiddenField)sender;
      CheckBoxList CheckBoxList_InsertScreeningTypeTypeList = (CheckBoxList)FormView_IPS_Infection_Form.FindControl("CheckBoxList_InsertScreeningTypeTypeList");
      HiddenField_InsertScreeningTypeTypeListTotal.Value = CheckBoxList_InsertScreeningTypeTypeList.Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void Button_InsertInfectionHome_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }
    

    protected void EditRegisterPostBackControl()
    {
      DropDownList DropDownList_EditInfectionTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionTypeList");
      DropDownList DropDownList_EditInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubTypeList");
      DropDownList DropDownList_EditInfectionSiteList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteList");

      ScriptManager ScriptManager_Edit = ScriptManager.GetCurrent(Page);

      ScriptManager_Edit.RegisterPostBackControl(DropDownList_EditInfectionTypeList);
      ScriptManager_Edit.RegisterPostBackControl(DropDownList_EditInfectionSubTypeList);
      ScriptManager_Edit.RegisterPostBackControl(DropDownList_EditInfectionSiteList);

      FileRegisterPostBackControl();
    }

    protected void DropDownList_EditInfectionTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditInfectionTypeList = (DropDownList)sender;
      DropDownList DropDownList_EditInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubTypeList");
      DropDownList DropDownList_EditInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubSubTypeList");

      ToolkitScriptManager_IPS.SetFocus(DropDownList_EditInfectionTypeList);

      DropDownList_EditInfectionSubTypeList.Items.Clear();
      DropDownList_EditInfectionSubSubTypeList.Items.Clear();

      SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_EditInfectionTypeList.SelectedValue;
      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters["ListItem_Parent"].DefaultValue = "";

      DropDownList_EditInfectionSubTypeList.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Type", CultureInfo.CurrentCulture), ""));
      DropDownList_EditInfectionSubSubTypeList.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Sub Type", CultureInfo.CurrentCulture), ""));

      DropDownList_EditInfectionSubTypeList.DataBind();
      DropDownList_EditInfectionSubSubTypeList.DataBind();
    }

    protected void DropDownList_EditInfectionTypeList_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditInfectionTypeList = (DropDownList)sender;

      SqlDataSource_IPS_EditInfectionSubTypeList.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_EditInfectionTypeList.SelectedValue;

      DropDownList DropDownList_EditInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubTypeList");
      DropDownList DropDownList_EditInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubSubTypeList");

      DataView DataView_IPSType = (DataView)SqlDataSource_IPS_Infection_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_IPSType = DataView_IPSType[0];
      DropDownList_EditInfectionSubTypeList.SelectedValue = Convert.ToString(DataRowView_IPSType["IPS_Infection_SubType_List"], CultureInfo.CurrentCulture);
      DropDownList_EditInfectionSubSubTypeList.SelectedValue = Convert.ToString(DataRowView_IPSType["IPS_Infection_SubSubType_List"], CultureInfo.CurrentCulture);
    }

    protected void DropDownList_EditInfectionSubTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditInfectionSubTypeList = (DropDownList)sender;
      DropDownList DropDownList_EditInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubSubTypeList");

      ToolkitScriptManager_IPS.SetFocus(DropDownList_EditInfectionSubTypeList);

      DropDownList_EditInfectionSubSubTypeList.Items.Clear();

      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_EditInfectionSubTypeList.SelectedValue;

      DropDownList_EditInfectionSubSubTypeList.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Sub Type", CultureInfo.CurrentCulture), ""));

      DropDownList_EditInfectionSubSubTypeList.DataBind();
    }

    protected void DropDownList_EditInfectionSubTypeList_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditInfectionSubTypeList = (DropDownList)sender;

      SqlDataSource_IPS_EditInfectionSubSubTypeList.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_EditInfectionSubTypeList.SelectedValue;
    }

    protected void DropDownList_EditInfectionSiteList_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditInfectionSiteList = (DropDownList)sender;
      DropDownList DropDownList_EditInfectionSiteInfectionId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteInfectionId");

      ToolkitScriptManager_IPS.SetFocus(DropDownList_EditInfectionSiteList);

      DropDownList_EditInfectionSiteInfectionId.Items.Clear();

      SqlDataSource_IPS_EditInfectionSiteInfectionId.SelectParameters["IPS_Infection_Site_List"].DefaultValue = DropDownList_EditInfectionSiteList.SelectedValue;

      DropDownList_EditInfectionSiteInfectionId.Items.Insert(0, new ListItem(Convert.ToString("Select Linked Site", CultureInfo.CurrentCulture), ""));

      DropDownList_EditInfectionSiteInfectionId.DataBind();
    }

    protected void DropDownList_EditInfectionSiteList_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditInfectionSiteList = (DropDownList)sender;

      Session["IPSInfectionId"] = "";
      string SQLStringInfection = "SELECT IPS_Infection_Id FROM Form_IPS_Infection WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id AND IPS_Infection_Id != @IPS_Infection_Id AND IPS_Infection_Category_List IN (4799)";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["IPSInfectionId"] = DataRow_Row["IPS_Infection_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSInfectionId"].ToString()))
      {
        DropDownList_EditInfectionSiteList.Items.Remove(DropDownList_EditInfectionSiteList.Items.FindByValue("4997"));
        DropDownList_EditInfectionSiteList.Items.Remove(DropDownList_EditInfectionSiteList.Items.FindByValue("4998"));
      }

      Session.Remove("IPSInfectionId");


      SqlDataSource_IPS_EditInfectionSiteInfectionId.SelectParameters["IPS_Infection_Site_List"].DefaultValue = DropDownList_EditInfectionSiteList.SelectedValue;

      DropDownList DropDownList_EditInfectionSiteInfectionId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteInfectionId");
      DataView DataView_IPSSiteInfectionId = (DataView)SqlDataSource_IPS_Infection_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_IPSSiteInfectionId = DataView_IPSSiteInfectionId[0];

      Session["IPSInfectionIdActive"] = "";
      string SQLStringInfectionActive = "SELECT IPS_Infection_Id FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id AND IPS_Infection_IsActive = 1";
      using (SqlCommand SqlCommand_InfectionActive = new SqlCommand(SQLStringInfectionActive))
      {
        SqlCommand_InfectionActive.Parameters.AddWithValue("@IPS_Infection_Id", Convert.ToString(DataRowView_IPSSiteInfectionId["IPS_Infection_Site_Infection_Id"], CultureInfo.CurrentCulture));
        DataTable DataTable_InfectionActive;
        using (DataTable_InfectionActive = new DataTable())
        {
          DataTable_InfectionActive.Locale = CultureInfo.CurrentCulture;
          DataTable_InfectionActive = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionActive).Copy();
          if (DataTable_InfectionActive.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfectionActive.Rows)
            {
              Session["IPSInfectionIdActive"] = DataRow_Row["IPS_Infection_Id"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["IPSInfectionIdActive"].ToString()))
      {
        DropDownList_EditInfectionSiteInfectionId.SelectedValue = Convert.ToString(DataRowView_IPSSiteInfectionId["IPS_Infection_Site_Infection_Id"], CultureInfo.CurrentCulture);
      }

      Session.Remove("IPSInfectionIdActive");
    }

    protected void DropDownList_EditInfectionSiteInfectionId_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditInfectionSiteInfectionId = (DropDownList)sender;

      DropDownList_EditInfectionSiteInfectionId.Items.Remove(DropDownList_EditInfectionSiteInfectionId.Items.FindByText(Convert.ToString("Linked Site Required", CultureInfo.CurrentCulture)));
    }

    protected void CheckBoxList_EditScreeningTypeTypeList_DataBound(object sender, EventArgs e)
    {
      CheckBoxList CheckBoxList_EditScreeningTypeTypeList = (CheckBoxList)sender;

      for (int i = 0; i < CheckBoxList_EditScreeningTypeTypeList.Items.Count; i++)
      {
        Session["IPSScreeningTypeTypeList"] = "";
        string SQLStringIPSScreeningTypeTypeList = "SELECT DISTINCT IPS_ScreeningType_Type_List FROM Form_IPS_Infection_ScreeningType WHERE IPS_Infection_Id = @IPS_Infection_Id AND IPS_ScreeningType_Type_List = @IPS_ScreeningType_Type_List";
        using (SqlCommand SqlCommand_IPSScreeningTypeTypeList = new SqlCommand(SQLStringIPSScreeningTypeTypeList))
        {
          SqlCommand_IPSScreeningTypeTypeList.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
          SqlCommand_IPSScreeningTypeTypeList.Parameters.AddWithValue("@IPS_ScreeningType_Type_List", CheckBoxList_EditScreeningTypeTypeList.Items[i].Value);
          DataTable DataTable_IPSScreeningTypeTypeList;
          using (DataTable_IPSScreeningTypeTypeList = new DataTable())
          {
            DataTable_IPSScreeningTypeTypeList.Locale = CultureInfo.CurrentCulture;
            DataTable_IPSScreeningTypeTypeList = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IPSScreeningTypeTypeList).Copy();
            if (DataTable_IPSScreeningTypeTypeList.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_IPSScreeningTypeTypeList.Rows)
              {
                Session["IPSScreeningTypeTypeList"] = DataRow_Row["IPS_ScreeningType_Type_List"];
                CheckBoxList_EditScreeningTypeTypeList.Items[i].Selected = true;
              }
            }
          }
        }

        Session["IPSScreeningTypeTypeList"] = "";
      }
    }

    protected void HiddenField_EditScreeningTypeTypeListTotal_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_EditScreeningTypeTypeListTotal = (HiddenField)sender;
      CheckBoxList CheckBoxList_EditScreeningTypeTypeList = (CheckBoxList)FormView_IPS_Infection_Form.FindControl("CheckBoxList_EditScreeningTypeTypeList");
      HiddenField_EditScreeningTypeTypeListTotal.Value = CheckBoxList_EditScreeningTypeTypeList.Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void Button_EditPrint_Click(object sender, EventArgs e)
    {
      Button_EditPrintClicked = true;
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      Button_EditEmailClicked = true;
    }

    protected void Button_EditInfectionHome_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }


    protected void SqlDataSource_IPS_ItemInfectionScreeningType_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        GridView GridView_ItemInfectionScreeningType = (GridView)FormView_IPS_Infection_Form.FindControl("GridView_ItemInfectionScreeningType");

        if (TotalRecords > 0)
        {
          GridView_ItemInfectionScreeningType.Visible = true;
        }
        else
        {
          GridView_ItemInfectionScreeningType.Visible = false;
        }
      }
    }

    protected void GridView_ItemInfectionScreeningType_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemInfectionScreeningType_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          int m = e.Row.Cells.Count;

          for (int i = m - 1; i >= 1; i += -1)
          {
            e.Row.Cells.RemoveAt(i);
          }

          e.Row.Cells[0].ColumnSpan = m;
          e.Row.Cells[0].Text = Convert.ToString("&nbsp;", CultureInfo.CurrentCulture);
        }
      }
    }

    protected void Button_ItemInfectionHome_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }
    //---END--- --CurrentInfection--//


    protected string EditValidation_Infection_BeforeRedirect()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditInfectionCategoryList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionCategoryList");
      DropDownList DropDownList_EditInfectionTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionTypeList");
      DropDownList DropDownList_EditInfectionSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubTypeList");
      DropDownList DropDownList_EditInfectionSubSubTypeList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSubSubTypeList");
      DropDownList DropDownList_EditInfectionSiteList = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteList");
      DropDownList DropDownList_EditInfectionSiteInfectionId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionSiteInfectionId");
      CheckBox CheckBox_EditInfectionScreening = (CheckBox)FormView_IPS_Infection_Form.FindControl("CheckBox_EditInfectionScreening");
      DropDownList DropDownList_EditInfectionUnitId = (DropDownList)FormView_IPS_Infection_Form.FindControl("DropDownList_EditInfectionUnitId");
      TextBox TextBox_EditInfectionSummary = (TextBox)FormView_IPS_Infection_Form.FindControl("TextBox_EditInfectionSummary");
      CheckBox CheckBox_EditIsActive = (CheckBox)FormView_IPS_Infection_Form.FindControl("CheckBox_EditIsActive");

      HiddenField HiddenField_EditInfectionCategoryList = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditInfectionCategoryList");
      HiddenField HiddenField_EditInfectionTypeList = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditInfectionTypeList");
      HiddenField HiddenField_EditInfectionSubTypeList = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditInfectionSubTypeList");
      HiddenField HiddenField_EditInfectionSubSubTypeList = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditInfectionSubSubTypeList");
      HiddenField HiddenField_EditInfectionSiteList = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditInfectionSiteList");
      HiddenField HiddenField_EditInfectionSiteInfectionId = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditInfectionSiteInfectionId");
      HiddenField HiddenField_EditInfectionScreening = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditInfectionScreening");
      HiddenField HiddenField_EditInfectionUnitId = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditInfectionUnitId");
      HiddenField HiddenField_EditInfectionSummary = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditInfectionSummary");
      HiddenField HiddenField_EditIsActive = (HiddenField)FormView_IPS_Infection_Form.FindControl("HiddenField_EditIsActive");

      if (InvalidForm == "No")
      {
        if (DropDownList_EditInfectionCategoryList != null)
        {
          if (DropDownList_EditInfectionCategoryList.SelectedValue != HiddenField_EditInfectionCategoryList.Value)
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_EditInfectionTypeList.SelectedValue != HiddenField_EditInfectionTypeList.Value)
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_EditInfectionSubTypeList.SelectedValue != HiddenField_EditInfectionSubTypeList.Value)
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_EditInfectionSubSubTypeList.SelectedValue != HiddenField_EditInfectionSubSubTypeList.Value)
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_EditInfectionSiteList.SelectedValue != HiddenField_EditInfectionSiteList.Value)
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_EditInfectionSiteInfectionId.SelectedValue != HiddenField_EditInfectionSiteInfectionId.Value)
          {
            InvalidForm = "Yes";
          }


          string ValueCheckBox_EditInfectionScreening = "";
          if (CheckBox_EditInfectionScreening.Checked == true)
          {
            ValueCheckBox_EditInfectionScreening = "True";
          }
          else
          {
            ValueCheckBox_EditInfectionScreening = "False";
          }

          if (ValueCheckBox_EditInfectionScreening != HiddenField_EditInfectionScreening.Value)
          {
            InvalidForm = "Yes";
          }


          if (DropDownList_EditInfectionUnitId.SelectedValue != HiddenField_EditInfectionUnitId.Value)
          {
            InvalidForm = "Yes";
          }

          if (TextBox_EditInfectionSummary.Text != HiddenField_EditInfectionSummary.Value)
          {
            InvalidForm = "Yes";
          }


          string ValueCheckBox_EditIsActive = "";
          if (CheckBox_EditIsActive.Checked == true)
          {
            ValueCheckBox_EditIsActive = "True";
          }
          else
          {
            ValueCheckBox_EditIsActive = "False";
          }

          if (ValueCheckBox_EditIsActive != HiddenField_EditIsActive.Value)
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("Infection has changed, save changes before leaving page", CultureInfo.CurrentCulture);
      }

      return InvalidFormMessage;
    }


    //--START-- --Theatre--//
    protected void SqlDataSource_IPS_Theatre_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenTheatreTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_Theatre_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }

      for (int i = 0; i < GridView_IPS_Theatre_List.Rows.Count; i++)
      {
        if (GridView_IPS_Theatre_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          Label Label_Type = (Label)GridView_IPS_Theatre_List.Rows[i].FindControl("Label_Type");
          HtmlTableRow SurgicalRow1 = (HtmlTableRow)GridView_IPS_Theatre_List.Rows[i].FindControl("SurgicalRow1");
          HtmlTableRow SurgicalRow2 = (HtmlTableRow)GridView_IPS_Theatre_List.Rows[i].FindControl("SurgicalRow2");
          HtmlTableRow SurgicalRow3 = (HtmlTableRow)GridView_IPS_Theatre_List.Rows[i].FindControl("SurgicalRow3");
          HtmlTableRow SurgicalRow4 = (HtmlTableRow)GridView_IPS_Theatre_List.Rows[i].FindControl("SurgicalRow4");

          if (Label_Type.Text.Contains("SURGICAL") == true)
          {
            SurgicalRow1.Visible = true;
            SurgicalRow2.Visible = true;
            SurgicalRow3.Visible = true;
            SurgicalRow4.Visible = true;
          }
          else
          {
            SurgicalRow1.Visible = false;
            SurgicalRow2.Visible = true;
            SurgicalRow3.Visible = false;
            SurgicalRow4.Visible = false;
          }
        }
      }
    }

    protected void GridView_IPS_Theatre_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_TheatreTotalRecords = (Label)e.Row.FindControl("Label_TheatreTotalRecords");
          Label_TheatreTotalRecords.Text = Label_HiddenTheatreTotalRecords.Text;
        }


        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_TheatreSelect = (Button)e.Row.FindControl("Button_TheatreSelect");

          FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
          DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
          DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
          DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
          DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
          DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
          string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
          {
            Security = "0";
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True" && IPSInfectionSiteInfectionIsActive == "True")
            {
              Button_TheatreSelect.Enabled = true;
              Button_TheatreSelect.Text = Convert.ToString("Select Visit History", CultureInfo.CurrentCulture);
            }
            else
            {
              if (IPSInfectionIsActive == "True")
              {
                if (IPSInfectionSiteInfectionIsActive == "True")
                {
                  Button_TheatreSelect.Enabled = false;
                  Button_TheatreSelect.Text = Convert.ToString("Infection Completed", CultureInfo.CurrentCulture);
                }
                else
                {
                  Button_TheatreSelect.Enabled = false;
                  Button_TheatreSelect.Text = Convert.ToString("Linked Site Required", CultureInfo.CurrentCulture);
                }
              }
              else
              {
                Button_TheatreSelect.Enabled = false;
                Button_TheatreSelect.Text = Convert.ToString("Infection Cancelled", CultureInfo.CurrentCulture);
              }
            }
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
          {
            Security = "0";
            Button_TheatreSelect.Enabled = false;
            Button_TheatreSelect.Text = Convert.ToString("Infection Completed", CultureInfo.CurrentCulture);
          }

          if (Security == "1")
          {
            Security = "0";
            Button_TheatreSelect.Enabled = false;
            Button_TheatreSelect.Text = Convert.ToString("Infection Completed", CultureInfo.CurrentCulture);
          }
        }
      }
    }

    protected void Button_TheatreInfectionHome_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }

    protected void Button_TheatreSelect_Click(object sender, EventArgs e)
    {
      string Label_EditInvalidFormMessage = EditValidation_Infection_BeforeRedirect();

      if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
      {
        ((Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;

        TableCurrentInfection.Focus();
        AjaxControlToolkit.Utility.SetFocusOnLoad(TableCurrentInfection);
      }
      else
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Theatre", "Form_IPS_Theatre.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"]), false);
      }
    }

    public static string GetFinalDiagnosis(object ips_Theatre_FinalDiagnosisCode, object ips_Theatre_FinalDiagnosisDescription)
    {
      string FinalDiagnosis = "";

      if (ips_Theatre_FinalDiagnosisDescription != null)
      {
        if (ips_Theatre_FinalDiagnosisCode == null || string.IsNullOrEmpty(ips_Theatre_FinalDiagnosisCode.ToString()))
        {
          FinalDiagnosis = ips_Theatre_FinalDiagnosisDescription.ToString();
        }
        else
        {
          FinalDiagnosis = ips_Theatre_FinalDiagnosisCode.ToString() + " : " + ips_Theatre_FinalDiagnosisDescription.ToString();
        }
      }

      return FinalDiagnosis;
    }

    public static string GetProcedure(object ips_Theatre_ProcedureCode, object ips_Theatre_ProcedureDescription)
    {
      string Procedure = "";

      if (ips_Theatre_ProcedureDescription != null)
      {
        if (ips_Theatre_ProcedureCode == null || string.IsNullOrEmpty(ips_Theatre_ProcedureCode.ToString()))
        {
          Procedure = ips_Theatre_ProcedureDescription.ToString();
        }
        else
        {
          Procedure = ips_Theatre_ProcedureCode.ToString() + " : " + ips_Theatre_ProcedureDescription.ToString();
        }
      }

      return Procedure;
    }
    //---END--- --Theatre--//


    //--START-- --VisitDiagnosis--//
    protected void SqlDataSource_IPS_VisitDiagnosis_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenVisitDiagnosisTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_VisitDiagnosis_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_VisitDiagnosis_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_VisitDiagnosisTotalRecords = (Label)e.Row.FindControl("Label_VisitDiagnosisTotalRecords");
          Label_VisitDiagnosisTotalRecords.Text = Label_HiddenVisitDiagnosisTotalRecords.Text;
        }


        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_VisitDiagnosisSelect = (Button)e.Row.FindControl("Button_VisitDiagnosisSelect");

          FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
          DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
          DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
          DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
          DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
          DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
          string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
          {
            Security = "0";
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True" && IPSInfectionSiteInfectionIsActive == "True")
            {
              Button_VisitDiagnosisSelect.Enabled = true;
              Button_VisitDiagnosisSelect.Text = Convert.ToString("Select Visit Diagnosis", CultureInfo.CurrentCulture);
            }
            else
            {
              if (IPSInfectionIsActive == "True")
              {
                if (IPSInfectionSiteInfectionIsActive == "True")
                {
                  Button_VisitDiagnosisSelect.Enabled = false;
                  Button_VisitDiagnosisSelect.Text = Convert.ToString("Infection Completed", CultureInfo.CurrentCulture);
                }
                else
                {
                  Button_VisitDiagnosisSelect.Enabled = false;
                  Button_VisitDiagnosisSelect.Text = Convert.ToString("Linked Site Required", CultureInfo.CurrentCulture);
                }
              }
              else
              {
                Button_VisitDiagnosisSelect.Enabled = false;
                Button_VisitDiagnosisSelect.Text = Convert.ToString("Infection Cancelled", CultureInfo.CurrentCulture);
              }
            }
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
          {
            Security = "0";
            Button_VisitDiagnosisSelect.Enabled = false;
            Button_VisitDiagnosisSelect.Text = Convert.ToString("Infection Completed", CultureInfo.CurrentCulture);
          }

          if (Security == "1")
          {
            Security = "0";
            Button_VisitDiagnosisSelect.Enabled = false;
            Button_VisitDiagnosisSelect.Text = Convert.ToString("Infection Completed", CultureInfo.CurrentCulture);
          }
        }
      }
    }

    protected void Button_VisitDiagnosisInfectionHome_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }

    protected void Button_VisitDiagnosisSelect_Click(object sender, EventArgs e)
    {
      string Label_EditInvalidFormMessage = EditValidation_Infection_BeforeRedirect();
      
      if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
      {
        ((Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;

        TableCurrentInfection.Focus();
        AjaxControlToolkit.Utility.SetFocusOnLoad(TableCurrentInfection);
      }
      else
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_VisitDiagnosis", "Form_IPS_VisitDiagnosis.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"]), false);
      }
    }
    //---END--- --VisitDiagnosis--//


    //--START-- --BedHistory--//
    protected void SqlDataSource_IPS_BedHistory_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenBedHistoryTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_BedHistory_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_BedHistory_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_BedHistoryTotalRecords = (Label)e.Row.FindControl("Label_BedHistoryTotalRecords");
          Label_BedHistoryTotalRecords.Text = Label_HiddenBedHistoryTotalRecords.Text;
        }


        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_BedHistorySelect = (Button)e.Row.FindControl("Button_BedHistorySelect");

          FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
          DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
          DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
          DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
          DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
          DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
          string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
          {
            Security = "0";
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True" && IPSInfectionSiteInfectionIsActive == "True")
            {
              Button_BedHistorySelect.Enabled = true;
              Button_BedHistorySelect.Text = Convert.ToString("Select Bed History", CultureInfo.CurrentCulture);
            }
            else
            {
              if (IPSInfectionIsActive == "True")
              {
                if (IPSInfectionSiteInfectionIsActive == "True")
                {
                  Button_BedHistorySelect.Enabled = false;
                  Button_BedHistorySelect.Text = Convert.ToString("Infection Completed", CultureInfo.CurrentCulture);
                }
                else
                {
                  Button_BedHistorySelect.Enabled = false;
                  Button_BedHistorySelect.Text = Convert.ToString("Linked Site Required", CultureInfo.CurrentCulture);
                }
              }
              else
              {
                Button_BedHistorySelect.Enabled = false;
                Button_BedHistorySelect.Text = Convert.ToString("Infection Cancelled", CultureInfo.CurrentCulture);
              }
            }
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
          {
            Security = "0";
            Button_BedHistorySelect.Enabled = false;
            Button_BedHistorySelect.Text = Convert.ToString("Infection Completed", CultureInfo.CurrentCulture);
          }

          if (Security == "1")
          {
            Security = "0";
            Button_BedHistorySelect.Enabled = false;
            Button_BedHistorySelect.Text = Convert.ToString("Infection Completed", CultureInfo.CurrentCulture);
          }
        }
      }
    }

    protected void Button_BedHistoryInfectionHome_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }

    protected void Button_BedHistorySelect_Click(object sender, EventArgs e)
    {
      string Label_EditInvalidFormMessage = EditValidation_Infection_BeforeRedirect();

      if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
      {
        ((Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;

        TableCurrentInfection.Focus();
        AjaxControlToolkit.Utility.SetFocusOnLoad(TableCurrentInfection);
      }
      else
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_BedHistory", "Form_IPS_BedHistory.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"]), false);
      }
    }
    //---END--- --BedHistory--//


    //--START-- --Specimen--//
    protected void SqlDataSource_IPS_Specimen_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenSpecimenTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_Specimen_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }


      for (int i = 0; i < GridView_IPS_Specimen_List.Rows.Count; i++)
      {
        if (GridView_IPS_Specimen_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_IPS_Specimen_List.Rows[i].Cells[5].Text == "No")
          {
            GridView_IPS_Specimen_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_Specimen_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_IPS_Specimen_List.Rows[i].Cells[5].Text == "Yes")
          {
            GridView_IPS_Specimen_List.Rows[i].Cells[5].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_Specimen_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }


          if (GridView_IPS_Specimen_List.Rows[i].Cells[5].Text == "Yes")
          {
            if (GridView_IPS_Specimen_List.Rows[i].Cells[6].Text == "Incomplete")
            {
              GridView_IPS_Specimen_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
              GridView_IPS_Specimen_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
            }
            else
            {
              GridView_IPS_Specimen_List.Rows[i].Cells[6].BackColor = Color.FromName("#77cf9c");
              GridView_IPS_Specimen_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
            }
          }
          else if (GridView_IPS_Specimen_List.Rows[i].Cells[5].Text == "No")
          {
            GridView_IPS_Specimen_List.Rows[i].Cells[6].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_Specimen_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_IPS_Specimen_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_SpecimenTotalRecords = (Label)e.Row.FindControl("Label_SpecimenTotalRecords");
          Label_SpecimenTotalRecords.Text = Label_HiddenSpecimenTotalRecords.Text;
        }


        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_SpecimenCapture = (Button)e.Row.FindControl("Button_SpecimenCapture");

          FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
          DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
          DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
          DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
          DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
          DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
          string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
          {
            Security = "0";
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True" && IPSInfectionSiteInfectionIsActive == "True")
            {
              Button_SpecimenCapture.Enabled = true;
              Button_SpecimenCapture.Text = Convert.ToString("Capture New Specimen", CultureInfo.CurrentCulture);
            }
            else
            {
              Button_SpecimenCapture.Enabled = true;
              Button_SpecimenCapture.Text = Convert.ToString("View Captured Specimens", CultureInfo.CurrentCulture);
            }
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
          {
            Security = "0";
            Button_SpecimenCapture.Enabled = true;
            Button_SpecimenCapture.Text = Convert.ToString("View Captured Specimens", CultureInfo.CurrentCulture);
          }

          if (Security == "1")
          {
            Security = "0";
            Button_SpecimenCapture.Enabled = true;
            Button_SpecimenCapture.Text = Convert.ToString("View Captured Specimens", CultureInfo.CurrentCulture);
          }
        }
      }
    }

    protected void Button_SpecimenInfectionHome_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }

    protected void Button_SpecimenCapture_Click(object sender, EventArgs e)
    {
      string Label_EditInvalidFormMessage = EditValidation_Infection_BeforeRedirect();

      if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
      {
        ((Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;

        TableCurrentInfection.Focus();
        AjaxControlToolkit.Utility.SetFocusOnLoad(TableCurrentInfection);
      }
      else
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentSpecimen"), false);
      }
    }

    protected void Button_SpecimenAll_Click(object sender, EventArgs e)
    {
      string Label_EditInvalidFormMessage = EditValidation_Infection_BeforeRedirect();

      if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
      {
        ((Label)FormView_IPS_Infection_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;

        TableCurrentInfection.Focus();
        AjaxControlToolkit.Utility.SetFocusOnLoad(TableCurrentInfection);
      }
      else
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_SpecimenAll.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"]), false);
      }
    }

    public string GetSpecimenLink(object ips_Specimen_Id)
    {
      string FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + ips_Specimen_Id + "#CurrentSpecimen") + "'>Select</a>";

      return FinalURL;
    }
    //---END--- --Specimen--//


    //--START-- --CurrentInfectionComplete--//
    private void TableCurrentInfectionCompleteVisible()
    {
      TableCurrentInfectionCompleteVisible_Controls();


      FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Request.QueryString["IPSInfectionId"]);
      string IsHAI = FromDataBase_IsHAI_Current.IsHAI;


      if (IsHAI == "True")
      {
        TableCurrentInfectionCompleteVisible_ButtonsHAIYes();
      }
      else
      {
        TableCurrentInfectionCompleteVisible_ButtonsHAINo();
      }
    }

    private void TableCurrentInfectionCompleteVisible_Controls()
    {
      Session["IPSInfectionId"] = "";
      Session["FacilityFacilityDisplayName"] = "";
      Session["IPSVisitInformationVisitNumber"] = "";
      Session["PatientInformationName"] = "";
      Session["PatientInformationSurname"] = "";
      Session["IPSInfectionReportNumber"] = "";
      Session["IPSInfectionCategoryName"] = "";
      Session["IPSInfectionTypeName"] = "";
      Session["IPSInfectionCompleted"] = "";
      Session["IPSInfectionModifiedDate"] = "";
      Session["IPSInfectionModifiedBy"] = "";
      Session["IPSInfectionHistory"] = "";
      Session["IPSInfectionIsActive"] = "";
      Session["IPSHAIId"] = "";
      Session["IPSHAIModifiedDate"] = "";
      Session["Specimen"] = "";
      Session["Infection"] = "";
      Session["HAI"] = "";
      string SQLStringVisitInfo = "EXECUTE spForm_Get_IPS_InfectionInformation @IPS_Infection_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_VisitInfo;
        using (DataTable_VisitInfo = new DataTable())
        {
          DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
          if (DataTable_VisitInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
            {
              Session["IPSInfectionId"] = DataRow_Row["IPS_Infection_Id"];
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["IPSVisitInformationVisitNumber"] = DataRow_Row["IPS_VisitInformation_VisitNumber"];
              Session["PatientInformationName"] = DataRow_Row["PatientInformation_Name"];
              Session["PatientInformationSurname"] = DataRow_Row["PatientInformation_Surname"];
              Session["IPSInfectionReportNumber"] = DataRow_Row["IPS_Infection_ReportNumber"];
              Session["IPSInfectionCategoryName"] = DataRow_Row["IPS_Infection_Category_Name"];
              Session["IPSInfectionTypeName"] = DataRow_Row["IPS_Infection_Type_Name"];
              Session["IPSInfectionCompleted"] = DataRow_Row["IPS_Infection_Completed"];
              Session["IPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["IPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSInfectionIsActive"] = DataRow_Row["IPS_Infection_IsActive"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
              Session["IPSHAIModifiedDate"] = DataRow_Row["IPS_HAI_ModifiedDate"];
              Session["Specimen"] = DataRow_Row["Specimen"];
              Session["Infection"] = DataRow_Row["Infection"];
              Session["HAI"] = DataRow_Row["HAI"];
            }
          }
        }
      }

      Label_CurrentInfectionCompleteInfection.Text = Session["Infection"].ToString();
      Label_CurrentInfectionCompleteHAIInvestigation.Text = Session["HAI"].ToString();

      if (Session["Infection"].ToString() == "Incomplete")
      {
        Hyperlink_CurrentInfectionCompleteHAIInvestigation.Text = Session["HAI"].ToString();
      }
      else
      {
        if (Session["HAI"].ToString() == "Not Required")
        {
          Hyperlink_CurrentInfectionCompleteHAIInvestigation.Text = Session["HAI"].ToString();
        }
        else
        {
          Hyperlink_CurrentInfectionCompleteHAIInvestigation.Text = Convert.ToString("<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Session["IPSInfectionId"].ToString() + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI") + "' Class='Controls_HyperLink_ColorBackground'>" + Session["HAI"].ToString() + "</a>", CultureInfo.CurrentCulture);
        }
      }

      HiddenField_CurrentInfectionCompleteHAIId.Value = Session["IPSHAIId"].ToString();
      Label_CurrentInfectionCompleteSpecimen.Text = Session["Specimen"].ToString();
      Hyperlink_CurrentInfectionCompleteSpecimen.Text = Convert.ToString("<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Session["IPSInfectionId"].ToString() + "") + "' Class='Controls_HyperLink_ColorBackground'>" + Session["Specimen"].ToString() + "</a>", CultureInfo.CurrentCulture);
      HiddenField_CurrentInfectionComplete_ModifiedDate.Value = Session["IPSInfectionModifiedDate"].ToString();
      HiddenField_CurrentInfectionComplete_ModifiedBy.Value = Session["IPSInfectionModifiedBy"].ToString();
      HiddenField_CurrentInfectionComplete_History.Value = Session["IPSInfectionHistory"].ToString();
      HiddenField_CurrentInfectionComplete_HAIModifiedDate.Value = Session["IPSHAIModifiedDate"].ToString();
      Label_CurrentInfectionCompleteIsActive.Text = Session["IPSInfectionIsActive"].ToString();

      if (Session["IPSInfectionIsActive"].ToString() == "Yes")
      {
        if (Session["Infection"].ToString() == "Incomplete")
        {
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "background-color: #d46e6e");
        }
        else
        {
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "background-color: #77cf9c");
        }

        if (Session["HAI"].ToString() == "Incomplete")
        {
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "background-color: #d46e6e");
        }
        else
        {
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "background-color: #77cf9c");
        }

        if (Session["Specimen"].ToString() == "Incomplete")
        {
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "background-color: #d46e6e");
        }
        else
        {
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "background-color: #77cf9c");
        }

        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "background-color: #77cf9c");
      }
      else
      {
        CurrentInfectionCompleteInfection.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteInfection.Attributes.Add("Style", "background-color: #77cf9c");
        CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "background-color: #77cf9c");
        CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "background-color: #77cf9c");
        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "background-color: #d46e6e");
      }

      Session.Remove("IPSInfectionId");
      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("IPSVisitInformationVisitNumber");
      Session.Remove("PatientInformationName");
      Session.Remove("PatientInformationSurname");
      Session.Remove("IPSInfectionReportNumber");
      Session.Remove("IPSInfectionCategoryName");
      Session.Remove("IPSInfectionTypeName");
      Session.Remove("IPSInfectionCompleted");
      Session.Remove("IPSInfectionModifiedDate");
      Session.Remove("IPSInfectionModifiedBy");
      Session.Remove("IPSInfectionHistory");
      Session.Remove("IPSInfectionIsActive");
      Session.Remove("IPSHAIId");
      Session.Remove("IPSHAIModifiedDate");
      Session.Remove("Specimen");
      Session.Remove("Infection");
      Session.Remove("HAI");
    }

    private void TableCurrentInfectionCompleteVisible_ButtonsHAIYes()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
      string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

      FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_Current = GetSpecimenCompleted();
      string Specimen = FromDataBase_SpecimenCompleted_Current.Specimen;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      FromDataBase_HAICompleted FromDataBase_HAICompleted_Current = GetHAICompleted();
      string IPSHAICompleted = FromDataBase_HAICompleted_Current.IPSHAICompleted;


      Button_HAINo_SpecimenIncomplete.Visible = false;
      Button_HAINo_InfectionCanceled.Visible = false;
      Button_HAINo_CompleteInfection.Visible = false;
      Button_HAINo_OpenInfection.Visible = false;
      Button_HAINo_CaptureNewInfection.Visible = false;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (IPSInfectionSiteInfectionIsActive == "False")
        {
          if (IPSInfectionIsActive == "True")
          {
            Button_HAIYes_LinkedSiteRequired.Visible = true;
            Button_HAIYes_SpecimenIncomplete.Visible = false;
            Button_HAIYes_InfectionCanceled.Visible = false;
            Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenInfection.Visible = false;
            Button_HAIYes_GoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenHAIInvestigation.Visible = false;
            Button_HAIYes_CaptureNewInfection.Visible = false;
          }
          else
          {
            Button_HAIYes_LinkedSiteRequired.Visible = false;
            Button_HAIYes_SpecimenIncomplete.Visible = false;
            Button_HAIYes_InfectionCanceled.Visible = true;
            Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenInfection.Visible = false;
            Button_HAIYes_GoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenHAIInvestigation.Visible = false;
            Button_HAIYes_CaptureNewInfection.Visible = true;
          }
        }
        else
        {
          if (Specimen == "Incomplete" || string.IsNullOrEmpty(Specimen))
          {
            if (IPSInfectionIsActive == "True")
            {
              Button_HAIYes_LinkedSiteRequired.Visible = false;
              Button_HAIYes_SpecimenIncomplete.Visible = true;
              Button_HAIYes_InfectionCanceled.Visible = false;
              Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenInfection.Visible = false;
              Button_HAIYes_GoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenHAIInvestigation.Visible = false;
              Button_HAIYes_CaptureNewInfection.Visible = false;
            }
            else
            {
              Button_HAIYes_LinkedSiteRequired.Visible = false;
              Button_HAIYes_SpecimenIncomplete.Visible = false;
              Button_HAIYes_InfectionCanceled.Visible = true;
              Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenInfection.Visible = false;
              Button_HAIYes_GoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenHAIInvestigation.Visible = false;
              Button_HAIYes_CaptureNewInfection.Visible = true;
            }
          }
          else
          {
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
            {
              Button_HAIYes_LinkedSiteRequired.Visible = false;
              Button_HAIYes_SpecimenIncomplete.Visible = false;
              Button_HAIYes_InfectionCanceled.Visible = false;
              Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = true;
              Button_HAIYes_OpenInfection.Visible = false;
              Button_HAIYes_GoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenHAIInvestigation.Visible = false;
              Button_HAIYes_CaptureNewInfection.Visible = false;
            }
            else
            {
              if (IPSInfectionIsActive == "True")
              {
                if (IPSHAICompleted == "False")
                {
                  Button_HAIYes_LinkedSiteRequired.Visible = false;
                  Button_HAIYes_SpecimenIncomplete.Visible = false;
                  Button_HAIYes_InfectionCanceled.Visible = false;
                  Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
                  Button_HAIYes_OpenInfection.Visible = true;
                  Button_HAIYes_GoToHAIInvestigation.Visible = true;
                  Button_HAIYes_OpenHAIInvestigation.Visible = false;
                  Button_HAIYes_CaptureNewInfection.Visible = false;
                }
                else
                {
                  Button_HAIYes_LinkedSiteRequired.Visible = false;
                  Button_HAIYes_SpecimenIncomplete.Visible = false;
                  Button_HAIYes_InfectionCanceled.Visible = false;
                  Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
                  Button_HAIYes_OpenInfection.Visible = true;
                  Button_HAIYes_GoToHAIInvestigation.Visible = false;
                  Button_HAIYes_OpenHAIInvestigation.Visible = true;
                  Button_HAIYes_CaptureNewInfection.Visible = true;
                }
              }
              else
              {
                Button_HAIYes_LinkedSiteRequired.Visible = false;
                Button_HAIYes_SpecimenIncomplete.Visible = false;
                Button_HAIYes_InfectionCanceled.Visible = true;
                Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
                Button_HAIYes_OpenInfection.Visible = false;
                Button_HAIYes_GoToHAIInvestigation.Visible = false;
                Button_HAIYes_OpenHAIInvestigation.Visible = false;
                Button_HAIYes_CaptureNewInfection.Visible = true;
              }
            }
          }
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";

        Button_HAIYes_LinkedSiteRequired.Visible = false;
        Button_HAIYes_SpecimenIncomplete.Visible = false;
        Button_HAIYes_InfectionCanceled.Visible = false;
        Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenInfection.Visible = false;
        Button_HAIYes_GoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenHAIInvestigation.Visible = false;
        Button_HAIYes_CaptureNewInfection.Visible = false;
      }

      if (Security == "1")
      {
        Security = "0";

        Button_HAIYes_LinkedSiteRequired.Visible = false;
        Button_HAIYes_SpecimenIncomplete.Visible = false;
        Button_HAIYes_InfectionCanceled.Visible = false;
        Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenInfection.Visible = false;
        Button_HAIYes_GoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenHAIInvestigation.Visible = false;
        Button_HAIYes_CaptureNewInfection.Visible = false;
      }
    }

    private void TableCurrentInfectionCompleteVisible_ButtonsHAINo()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_Current = GetSpecimenCompleted();
      string Specimen = FromDataBase_SpecimenCompleted_Current.Specimen;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;


      Button_HAIYes_LinkedSiteRequired.Visible = false;
      Button_HAIYes_SpecimenIncomplete.Visible = false;
      Button_HAIYes_InfectionCanceled.Visible = false;
      Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
      Button_HAIYes_OpenInfection.Visible = false;
      Button_HAIYes_GoToHAIInvestigation.Visible = false;
      Button_HAIYes_OpenHAIInvestigation.Visible = false;
      Button_HAIYes_CaptureNewInfection.Visible = false;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (Specimen == "Incomplete" || string.IsNullOrEmpty(Specimen))
        {
          if (IPSInfectionIsActive == "True")
          {
            Button_HAINo_SpecimenIncomplete.Visible = true;
            Button_HAINo_InfectionCanceled.Visible = false;
            Button_HAINo_CompleteInfection.Visible = false;
            Button_HAINo_OpenInfection.Visible = false;
            Button_HAINo_CaptureNewInfection.Visible = false;
          }
          else
          {
            Button_HAINo_SpecimenIncomplete.Visible = false;
            Button_HAINo_InfectionCanceled.Visible = true;
            Button_HAINo_CompleteInfection.Visible = false;
            Button_HAINo_OpenInfection.Visible = false;
            Button_HAINo_CaptureNewInfection.Visible = true;
          }
        }
        else
        {
          if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
          {
            Button_HAINo_SpecimenIncomplete.Visible = false;
            Button_HAINo_InfectionCanceled.Visible = false;
            Button_HAINo_CompleteInfection.Visible = true;
            Button_HAINo_OpenInfection.Visible = false;
            Button_HAINo_CaptureNewInfection.Visible = false;
          }
          else
          {
            if (IPSInfectionIsActive == "True")
            {
              Button_HAINo_SpecimenIncomplete.Visible = false;
              Button_HAINo_InfectionCanceled.Visible = false;
              Button_HAINo_CompleteInfection.Visible = false;
              Button_HAINo_OpenInfection.Visible = true;
              Button_HAINo_CaptureNewInfection.Visible = true;
            }
            else
            {
              Button_HAINo_SpecimenIncomplete.Visible = false;
              Button_HAINo_InfectionCanceled.Visible = true;
              Button_HAINo_CompleteInfection.Visible = false;
              Button_HAINo_OpenInfection.Visible = false;
              Button_HAINo_CaptureNewInfection.Visible = true;
            }
          }
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";

        Button_HAINo_SpecimenIncomplete.Visible = false;
        Button_HAINo_InfectionCanceled.Visible = false;
        Button_HAINo_CompleteInfection.Visible = false;
        Button_HAINo_OpenInfection.Visible = false;
        Button_HAINo_CaptureNewInfection.Visible = false;
      }

      if (Security == "1")
      {
        Security = "0";

        Button_HAINo_SpecimenIncomplete.Visible = false;
        Button_HAINo_InfectionCanceled.Visible = false;
        Button_HAINo_CompleteInfection.Visible = false;
        Button_HAINo_OpenInfection.Visible = false;
        Button_HAINo_CaptureNewInfection.Visible = false;
      }
    }


    protected void Button_InfectionInfectionHome_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void Button_HAIYes_LinkedSiteRequired_Click(object sender, EventArgs e)
    {
    }

    protected void Button_HAIYes_SpecimenIncomplete_Click(object sender, EventArgs e)
    {
    }

    protected void Button_HAIYes_InfectionCanceled_Click(object sender, EventArgs e)
    {
    }

    protected void Button_HAIYes_CompleteInfectionGoToHAIInvestigation_Click(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History , IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 1);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI"), false);
        }
      }
    }

    protected void Button_HAIYes_OpenInfection_Click(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History , IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id ; UPDATE Form_IPS_HAI SET IPS_HAI_Investigation_Completed = 0 , IPS_HAI_Investigation_CompletedDate = NULL WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 0);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DBNull.Value);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentInfection"), false);
        }
      }
    }

    protected void Button_HAIYes_GoToHAIInvestigation_Click(object sender, EventArgs e)
    {
      string SQLStringHAIId = "SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_HAIId = new SqlCommand(SQLStringHAIId))
      {
        SqlCommand_HAIId.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_HAIId;
        using (DataTable_HAIId = new DataTable())
        {
          DataTable_HAIId.Locale = CultureInfo.CurrentCulture;
          DataTable_HAIId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAIId).Copy();
          if (DataTable_HAIId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HAIId.Rows)
            {
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI"), false);
    }

    protected void Button_HAIYes_OpenHAIInvestigation_Click(object sender, EventArgs e)
    {
      Session["OLDIPSHAIModifiedDate"] = HiddenField_CurrentInfectionComplete_HAIModifiedDate.Value.ToString();
      object OLDIPSHAIModifiedDate = Session["OLDIPSHAIModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSHAIModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringHAI = "SELECT IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy , IPS_HAI_History , IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_HAI = new SqlCommand(SQLStringHAI))
      {
        SqlCommand_HAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_HAI;
        using (DataTable_HAI = new DataTable())
        {
          DataTable_HAI.Locale = CultureInfo.CurrentCulture;
          DataTable_HAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAI).Copy();
          if (DataTable_HAI.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HAI.Rows)
            {
              Session["DBIPSHAIModifiedDate"] = DataRow_Row["IPS_HAI_ModifiedDate"];
              Session["DBIPSHAIModifiedBy"] = DataRow_Row["IPS_HAI_ModifiedBy"];
              Session["IPSHAIHistory"] = DataRow_Row["IPS_HAI_History"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      object DBIPSHAIModifiedDate = Session["DBIPSHAIModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSHAIModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSHAIModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_HAI", "IPS_HAI_Id = " + Session["IPSHAIId"].ToString());

          Session["IPSHAIHistory"] = Session["History"].ToString() + Session["IPSHAIHistory"].ToString();

          string SQLStringUpdateHAI = "UPDATE Form_IPS_HAI SET IPS_HAI_Investigation_Completed = @IPS_HAI_Investigation_Completed , IPS_HAI_Investigation_CompletedDate = @IPS_HAI_Investigation_CompletedDate , IPS_HAI_ModifiedDate = @IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy = @IPS_HAI_ModifiedBy , IPS_HAI_History = @IPS_HAI_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateHAI = new SqlCommand(SQLStringUpdateHAI))
          {
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_Completed", 0);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_CompletedDate", DBNull.Value);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_History", Session["IPSHAIHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateHAI);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI"), false);
        }
      }
    }

    protected void Button_HAIYes_CaptureNewInfection_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx"), false);
    }


    protected void Button_HAINo_SpecimenIncomplete_Click(object sender, EventArgs e)
    {
    }

    protected void Button_HAINo_InfectionCanceled_Click(object sender, EventArgs e)
    {
    }

    protected void Button_HAINo_CompleteInfection_Click(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 1);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
        }
      }
    }

    protected void Button_HAINo_OpenInfection_Click(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;

        FileRegisterPostBackControl();
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";

          FileRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 0);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DBNull.Value);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentInfection"), false);
        }
      }
    }

    protected void Button_HAINo_CaptureNewInfection_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx"), false);
    }    
    //---END--- --CurrentInfectionComplete--//


    //--START-- --File--//
    protected void FileRegisterPostBackControl()
    {
      ScriptManager ScriptManager_File = ScriptManager.GetCurrent(Page);

      if (Button_UploadFile != null)
      {
        ScriptManager_File.RegisterPostBackControl(Button_UploadFile);
      }
    }

    public static string DatabaseFileName(object ips_File_Name)
    {
      string DatabaseFileName = "";
      if (ips_File_Name != null)
      {
        DatabaseFileName = "" + ips_File_Name.ToString() + "";
      }

      return DatabaseFileName;
    }

    protected void RetrieveDatabaseFile(object sender, EventArgs e)
    {
      LinkButton LinkButton_IPSFile = (LinkButton)sender;
      string FileId = LinkButton_IPSFile.CommandArgument.ToString();

      Session["IPSFileName"] = "";
      Session["IPSFileContentType"] = "";
      Session["IPSFileData"] = "";
      string SQLStringIPSFile = "SELECT IPS_File_Name ,IPS_File_ContentType ,IPS_File_Data FROM Form_IPS_File WHERE IPS_File_Id = @IPS_File_Id";
      using (SqlCommand SqlCommand_IPSFile = new SqlCommand(SQLStringIPSFile))
      {
        SqlCommand_IPSFile.Parameters.AddWithValue("@IPS_File_Id", FileId);
        DataTable DataTable_IPSFile;
        using (DataTable_IPSFile = new DataTable())
        {
          DataTable_IPSFile.Locale = CultureInfo.CurrentCulture;
          DataTable_IPSFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IPSFile).Copy();
          if (DataTable_IPSFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_IPSFile.Rows)
            {
              Session["IPSFileName"] = DataRow_Row["IPS_File_Name"];
              Session["IPSFileContentType"] = DataRow_Row["IPS_File_ContentType"];
              Session["IPSFileData"] = DataRow_Row["IPS_File_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["IPSFileData"].ToString()))
      {
        Byte[] Byte_FileData = (Byte[])Session["IPSFileData"];
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = Session["IPSFileContentType"].ToString();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Session["IPSFileName"].ToString() + "\"");
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      Session.Remove("IPSFileName");
      Session.Remove("IPSFileContentType");
      Session.Remove("IPSFileData");
    }

    protected void FileContentTypeHandlers()
    {
      FileContentTypeHandler.Add(".doc", "application/vnd.ms-word");
      FileContentTypeHandler.Add(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
      FileContentTypeHandler.Add(".xls", "application/vnd.ms-excel");
      FileContentTypeHandler.Add(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
      FileContentTypeHandler.Add(".pdf", "application/pdf");
      FileContentTypeHandler.Add(".tif", "image/tiff");
      FileContentTypeHandler.Add(".tiff", "image/tiff");
      FileContentTypeHandler.Add(".txt", "text/plain");
      FileContentTypeHandler.Add(".msg", "application/vnd.ms-outlook");
      FileContentTypeHandler.Add(".jpeg", "image/jpeg");
      FileContentTypeHandler.Add(".jpg", "image/jpeg");
      FileContentTypeHandler.Add(".gif", "image/gif");
      FileContentTypeHandler.Add(".png", "image/png");
    }

    protected string FileContentType(string fileExtension)
    {
      if (string.IsNullOrEmpty(fileExtension))
      {
        return "";
      }
      else
      {
        FileContentTypeHandlers();

        if (FileContentTypeHandler.ContainsKey(fileExtension))
        {
          string FileContentTypeValue = FileContentTypeHandler[fileExtension];
          FileContentTypeHandler.Clear();
          return FileContentTypeValue;
        }
        else
        {
          FileContentTypeHandler.Clear();
          return "";
        }
      }
    }


    protected void SqlDataSource_IPS_File_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        HiddenField_File.Value = TotalRecords.ToString(CultureInfo.CurrentCulture);

        if (TotalRecords > 0)
        {
          GridView_IPS_File.Visible = true;
        }
        else
        {
          GridView_IPS_File.Visible = false;
        }
      }
    }

    protected void GridView_IPS_File_PreRender(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = (GridViewRow)GridView_IPS_File.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_File_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          int m = e.Row.Cells.Count;

          for (int i = m - 1; i >= 1; i += -1)
          {
            e.Row.Cells.RemoveAt(i);
          }

          e.Row.Cells[0].ColumnSpan = m;
          e.Row.Cells[0].Text = Convert.ToString("&nbsp;", CultureInfo.CurrentCulture);
        }
      }
    }

    protected void Button_UploadFile_Click(object sender, EventArgs e)
    {
      string UploadMessage = "";

      if (!FileUpload_File.HasFiles)
      {
        UploadMessage = UploadMessage + Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>No files chosen</span>", CultureInfo.CurrentCulture);
      }
      else
      {
        foreach (HttpPostedFile HttpPostedFile_File in FileUpload_File.PostedFiles)
        {
          string FileName = Path.GetFileName(HttpPostedFile_File.FileName);
          string FileExtension = System.IO.Path.GetExtension(FileName);
          FileExtension = FileExtension.ToLower(CultureInfo.CurrentCulture);
          string FileContentTypeValue = FileContentType(FileExtension);
          decimal FileSize = HttpPostedFile_File.ContentLength;
          decimal FileSizeMB = (FileSize / 1024 / 1024);
          string FileSizeMBString = FileSizeMB.ToString("N2", CultureInfo.CurrentCulture);

          if ((!string.IsNullOrEmpty(FileContentTypeValue)) && (FileSize < 5242880))
          {
            Session["IPSFileId"] = "";
            string SQLStringExistingFile = "SELECT IPS_File_Id FROM Form_IPS_File WHERE IPS_File_CreatedBy = @IPS_File_CreatedBy AND IPS_Infection_Id = @IPS_Infection_Id AND IPS_File_Name = @IPS_File_Name";
            using (SqlCommand SqlCommand_ExistingFile = new SqlCommand(SQLStringExistingFile))
            {
              SqlCommand_ExistingFile.Parameters.AddWithValue("@IPS_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@IPS_File_Name", FileName);
              DataTable DataTable_ExistingFile;
              using (DataTable_ExistingFile = new DataTable())
              {
                DataTable_ExistingFile.Locale = CultureInfo.CurrentCulture;
                DataTable_ExistingFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ExistingFile).Copy();
                if (DataTable_ExistingFile.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_ExistingFile.Rows)
                  {
                    Session["IPSFileId"] = DataRow_Row["IPS_File_Id"];
                  }
                }
              }
            }

            if (!string.IsNullOrEmpty(Session["IPSFileId"].ToString()))
            {
              UploadMessage = Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span>", CultureInfo.CurrentCulture);
            }
            else
            {
              Stream Stream_File = HttpPostedFile_File.InputStream;
              BinaryReader BinaryReader_File = new BinaryReader(Stream_File);
              Byte[] Byte_File = BinaryReader_File.ReadBytes((Int32)Stream_File.Length);

              string SQLStringFile = "INSERT INTO Form_IPS_File ( IPS_Infection_Id , IPS_File_Name , IPS_File_ContentType , IPS_File_Data , IPS_File_CreatedDate , IPS_File_CreatedBy ) VALUES ( @IPS_Infection_Id , @IPS_File_Name , @IPS_File_ContentType , @IPS_File_Data , @IPS_File_CreatedDate , @IPS_File_CreatedBy )";
              using (SqlCommand SqlCommand_File = new SqlCommand(SQLStringFile))
              {
                SqlCommand_File.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_Name", FileName);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_ContentType", FileContentTypeValue);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_Data", Byte_File);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_CreatedDate", DateTime.Now);
                SqlCommand_File.Parameters.AddWithValue("@IPS_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_File);
              }

              GridView_IPS_File.DataBind();
            }

            Session.Remove("IPSFileId");
          }
          else
          {
            if (string.IsNullOrEmpty(FileContentTypeValue))
            {
              UploadMessage = UploadMessage + Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>Only doc, docx, xls, xlsx, pdf, tif, tiff, txt, msg, jpeg, jpg, gif and png files can be uploaded<br/>File Name: " + FileName + "</span>", CultureInfo.CurrentCulture);
            }

            if (FileSize > 5242880)
            {
              UploadMessage = UploadMessage + Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>Only files smaller then 5 MB can be uploaded<br/>File Name: " + FileName + "<br/>File Size: " + FileSizeMBString + " MB</span>", CultureInfo.CurrentCulture);
            }
          }
        }
      }

      FileRegisterPostBackControl();
      Label_MessageFile.Text = UploadMessage;
      ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentFile);
    }

    protected void Button_DeleteFile_Click(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      for (int i = 0; i < GridView_IPS_File.Rows.Count; i++)
      {
        CheckBox CheckBox_File = (CheckBox)GridView_IPS_File.Rows[i].Cells[0].FindControl("CheckBox_File");
        Int32 FileId = 0;

        if (CheckBox_File.Checked == true)
        {
          FileId = Convert.ToInt32(CheckBox_File.CssClass, CultureInfo.CurrentCulture);

          string SQLStringIPSFile = "DELETE FROM Form_IPS_File WHERE IPS_File_Id = @IPS_File_Id";
          using (SqlCommand SqlCommand_IPSFile = new SqlCommand(SQLStringIPSFile))
          {
            SqlCommand_IPSFile.Parameters.AddWithValue("@IPS_File_Id", FileId);

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_IPSFile);

            DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
          }
        }
      }

      FileRegisterPostBackControl();
      Label_MessageFile.Text = DeleteMessage;
      GridView_IPS_File.DataBind();
      ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentFile);
    }

    protected void Button_DeleteAllFile_Click(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      for (int i = 0; i < GridView_IPS_File.Rows.Count; i++)
      {
        CheckBox CheckBox_File = (CheckBox)GridView_IPS_File.Rows[i].Cells[0].FindControl("CheckBox_File");
        Int32 FileId = 0;

        FileId = Convert.ToInt32(CheckBox_File.CssClass, CultureInfo.CurrentCulture);

        string SQLStringIPSFile = "DELETE FROM Form_IPS_File WHERE IPS_File_Id = @IPS_File_Id";
        using (SqlCommand SqlCommand_IPSFile = new SqlCommand(SQLStringIPSFile))
        {
          SqlCommand_IPSFile.Parameters.AddWithValue("@IPS_File_Id", FileId);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_IPSFile);

          DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
        }
      }

      FileRegisterPostBackControl();
      Label_MessageFile.Text = DeleteMessage;
      GridView_IPS_File.DataBind();
      ToolkitScriptManager_IPS.SetFocus(LinkButton_CurrentFile);
    }

    protected void Button_UploadFile_DataBinding(object sender, EventArgs e)
    {
      ScriptManager ScriptManager_UploadFile = ScriptManager.GetCurrent(Page);
      ScriptManager_UploadFile.RegisterPostBackControl(Button_UploadFile);
    }

    protected void Button_DeleteFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_DeleteFile = (Button)sender;
      ScriptManager ScriptManager_DeleteFile = ScriptManager.GetCurrent(Page);
      ScriptManager_DeleteFile.RegisterPostBackControl(Button_DeleteFile);
    }

    protected void Button_DeleteAllFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_DeleteAllFile = (Button)sender;
      ScriptManager ScriptManager_DeleteAllFile = ScriptManager.GetCurrent(Page);
      ScriptManager_DeleteAllFile.RegisterPostBackControl(Button_DeleteAllFile);
    }

    protected void LinkButton_File_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_File = (LinkButton)sender;
      ScriptManager ScriptManager_File = ScriptManager.GetCurrent(Page);
      ScriptManager_File.RegisterPostBackControl(LinkButton_File);
    }
    //---END--- --File--//


    //--START-- --FileList--//
    protected void SqlDataSource_IPS_File_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenFileListTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_File_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_File_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_FileListTotalRecords = (Label)e.Row.FindControl("Label_FileListTotalRecords");
          Label_FileListTotalRecords.Text = Label_HiddenFileListTotalRecords.Text;
        }
      }
    }
    //---END--- --FileList--//
  }
}