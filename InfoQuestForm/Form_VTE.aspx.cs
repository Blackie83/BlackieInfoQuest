using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_VTE : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_VTE, this.GetType(), "UpdateProgress_Start", "Validation_Search();Validation_Form();Calculation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          PageTitle();

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_VTE_VisitInformation_VisitNumber"] != null)
          {
            form_VTE.DefaultButton = Button_Search.UniqueID;

            SqlDataSource_VTE_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
            SqlDataSource_VTE_Facility.SelectParameters["TableFROM"].DefaultValue = "Form_VTE_VisitInformation";
            SqlDataSource_VTE_Facility.SelectParameters["TableWHERE"].DefaultValue = "Facility_Id = " + Request.QueryString["s_Facility_Id"] + " AND VTE_VisitInformation_VisitNumber = " + Request.QueryString["s_VTE_VisitInformation_VisitNumber"] + " ";

            Label_InvalidSearchMessage.Text = "";
            TableVisitInfo.Visible = false;
            TableCurrentAssesment.Visible = false;
            TableAssesment.Visible = false;

            VisitData();
          }
          else
          {
            if (Request.QueryString["VTEVisitInformationId"] == null)
            {
              form_VTE.DefaultButton = Button_Search.UniqueID;

              Label_InvalidSearchMessage.Text = "";
              TableVisitInfo.Visible = false;
              TableCurrentAssesment.Visible = false;
              TableAssesment.Visible = false;
            }
            else
            {
              SqlDataSource_VTE_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
              SqlDataSource_VTE_Facility.SelectParameters["TableFROM"].DefaultValue = "Form_VTE_VisitInformation";
              SqlDataSource_VTE_Facility.SelectParameters["TableWHERE"].DefaultValue = "VTE_VisitInformation_Id = " + Request.QueryString["VTEVisitInformationId"] + " ";

              TableVisitInfo.Visible = true;
              TableCurrentAssesment.Visible = true;
              TableAssesment.Visible = true;

              SetCurrentAssesmentVisibility();

              if (string.IsNullOrEmpty(Request.QueryString["VTEAssesmentsId"]))
              {
                form_VTE.DefaultButton = Button_Search.UniqueID;

                if (TableCurrentAssesment.Visible == true)
                {
                  FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
                  string FacilityId = FromDataBase_FacilityId_Current.FacilityId;
                  string VTEVisitInformationVisitNumber = FromDataBase_FacilityId_Current.VTEVisitInformationVisitNumber;

                  if (((HiddenField)FormView_VTE_Form.FindControl("HiddenField_Insert")) != null)
                  {
                    ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertUnit")).Items.Clear();
                    SqlDataSource_VTE_InsertUnit.SelectParameters["Facility_Id"].DefaultValue = FacilityId;
                    ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertUnit")).Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
                    ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertUnit")).DataBind();

                    ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDate")).Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);

                    DataTable DataTable_Doctor;
                    using (DataTable_Doctor = new DataTable())
                    {
                      DataTable_Doctor.Locale = CultureInfo.CurrentCulture;
                      DataTable_Doctor = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(FacilityId, VTEVisitInformationVisitNumber);

                      ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorDoctorNotified")).DataSource = DataTable_Doctor;
                      ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorDoctorNotified")).Items.Insert(1, new System.Web.UI.WebControls.ListItem(Convert.ToString("No", CultureInfo.CurrentCulture), "No"));
                      ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorDoctorNotified")).DataBind();
                    }
                  }
                }
              }
              else
              {
                form_VTE.DefaultButton = null;

                SqlDataSource_VTE_EditUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
                SqlDataSource_VTE_EditUnit.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
                SqlDataSource_VTE_EditUnit.SelectParameters["TableFROM"].DefaultValue = "Form_VTE_Assesments";
                SqlDataSource_VTE_EditUnit.SelectParameters["TableWHERE"].DefaultValue = "VTE_Assesments_Id = " + Request.QueryString["VTEAssesmentsId"] + " ";

                FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
                string FacilityId = FromDataBase_FacilityId_Current.FacilityId;
                string VTEVisitInformationVisitNumber = FromDataBase_FacilityId_Current.VTEVisitInformationVisitNumber;

                if (((HiddenField)FormView_VTE_Form.FindControl("HiddenField_Edit")) != null)
                {
                  string SQLStringDoctor = @"SELECT	Facility_FacilityCode AS FacilityCode , 
				                                            VTE_VisitInformation_VisitNumber AS VisitNumber , 
				                                            VTE_Assesments_Doctor_DoctorNotified AS Practitioner 
                                            FROM		vForm_VTE_Assesments
                                            WHERE		VTE_Assesments_Id = @VTE_Assesments_Id 
				                                            AND VTE_Assesments_Doctor_DoctorNotified IS NOT NULL
                                                    AND VTE_Assesments_Doctor_DoctorNotified NOT IN ('No')";
                  using (SqlCommand SqlCommand_Doctor = new SqlCommand(SQLStringDoctor))
                  {
                    SqlCommand_Doctor.Parameters.AddWithValue("@VTE_Assesments_Id", Request.QueryString["VTEAssesmentsId"]);
                    DataTable DataTable_Doctor;
                    using (DataTable_Doctor = new DataTable())
                    {
                      DataTable_Doctor.Locale = CultureInfo.CurrentCulture;
                      DataTable_Doctor.Merge(InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Doctor));
                      DataTable_Doctor.Merge(InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(FacilityId, VTEVisitInformationVisitNumber));

                      DataTable_Doctor.DefaultView.Sort = "Practitioner ASC";

                      ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorDoctorNotified")).DataSource = DataTable_Doctor.DefaultView.ToTable(true, "Practitioner");
                      ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorDoctorNotified")).Items.Insert(1, new System.Web.UI.WebControls.ListItem(Convert.ToString("No", CultureInfo.CurrentCulture), "No"));
                      ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorDoctorNotified")).DataBind();
                    }
                  }
                }
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
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('51'))";
        }
        else
        {
          if (Request.QueryString["s_Facility_Id"] != null)
          {
            SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('51')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
          }

          if (Request.QueryString["IPSVisitInformationId"] != null)
          {
            SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('51')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_VTE_VisitInformation WHERE VTE_VisitInformation_Id = @VTE_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
          }
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_Security.Parameters.AddWithValue("@VTE_VisitInformation_Id", Request.QueryString["VTEVisitInformationId"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("51");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_VTE.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("VTE", "27");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_VTE_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_VTE_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_VTE_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_VTE_Facility.SelectParameters.Clear();
      SqlDataSource_VTE_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_VTE_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "51");
      SqlDataSource_VTE_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_VTE_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_VTE_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_VTE_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_VTE_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_VTE_Form.InsertCommand = "INSERT INTO Form_VTE_Assesments ( VTE_VisitInformation_Id , VTE_Assesments_ReportNumber , VTE_Assesments_Date , VTE_Assesments_By , Unit_Id , VTE_Assesments_Weight , VTE_Assesments_Height , VTE_Assesments_BMI , VTE_Assesments_BRF_Medical_Stroke , VTE_Assesments_BRF_Medical_HeartAttack , VTE_Assesments_BRF_Medical_HeartFailure , VTE_Assesments_BRF_Medical_Infection , VTE_Assesments_BRF_Medical_Thrombolytic , VTE_Assesments_BRF_Medical_CVA , VTE_Assesments_BRF_Surgical_SurgeryOfPelvis , VTE_Assesments_BRF_Surgical_FractureOfPelvis , VTE_Assesments_BRF_Surgical_MultipleTrauma , VTE_Assesments_BRF_Surgical_SpinalCordInjury , VTE_Assesments_BRF_Surgical_Plaster , VTE_Assesments_BRF_Surgical_SurgeryAbove45Min , VTE_Assesments_BRF_Surgical_SurgeryBelow45Min , VTE_Assesments_BRF_Both_PatientInBed , VTE_Assesments_BRF_Score , VTE_Assesments_PRF_Morbidities_History , VTE_Assesments_PRF_Morbidities_Cancer , VTE_Assesments_PRF_Morbidities_Obesity , VTE_Assesments_PRF_Morbidities_VaricoseVeins , VTE_Assesments_PRF_Morbidities_InflammatoryBowel , VTE_Assesments_PRF_Age_Years , VTE_Assesments_PRF_Gender_OralContraceptive , VTE_Assesments_PRF_Gender_HormoneReplacementTherapy , VTE_Assesments_PRF_Gender_Pregnancy , VTE_Assesments_PRF_Score , VTE_Assesments_RFS_Score , VTE_Assesments_RFS_Level , VTE_Assesments_Relative_SystolicHypertension , VTE_Assesments_Relative_HaemorrhagicStroke , VTE_Assesments_Relative_ActiveBleeding , VTE_Assesments_Relative_AntiCoagulants , VTE_Assesments_Relative_LowPlatelets , VTE_Assesments_Relative_BleedingDisorder , VTE_Assesments_Doctor_DoctorNotified , VTE_Assesments_Doctor_ReasonNotNotified , VTE_Assesments_Doctor_TreatmentInitiated , VTE_Assesments_Doctor_ReasonNotInitiated , VTE_Assesments_CreatedDate , VTE_Assesments_CreatedBy , VTE_Assesments_ModifiedDate , VTE_Assesments_ModifiedBy , VTE_Assesments_History , VTE_Assesments_IsActive ) VALUES ( @VTE_VisitInformation_Id , @VTE_Assesments_ReportNumber , @VTE_Assesments_Date , @VTE_Assesments_By , @Unit_Id , @VTE_Assesments_Weight , @VTE_Assesments_Height , @VTE_Assesments_BMI , @VTE_Assesments_BRF_Medical_Stroke , @VTE_Assesments_BRF_Medical_HeartAttack , @VTE_Assesments_BRF_Medical_HeartFailure , @VTE_Assesments_BRF_Medical_Infection , @VTE_Assesments_BRF_Medical_Thrombolytic , @VTE_Assesments_BRF_Medical_CVA , @VTE_Assesments_BRF_Surgical_SurgeryOfPelvis , @VTE_Assesments_BRF_Surgical_FractureOfPelvis , @VTE_Assesments_BRF_Surgical_MultipleTrauma , @VTE_Assesments_BRF_Surgical_SpinalCordInjury , @VTE_Assesments_BRF_Surgical_Plaster , @VTE_Assesments_BRF_Surgical_SurgeryAbove45Min , @VTE_Assesments_BRF_Surgical_SurgeryBelow45Min , @VTE_Assesments_BRF_Both_PatientInBed , @VTE_Assesments_BRF_Score , @VTE_Assesments_PRF_Morbidities_History , @VTE_Assesments_PRF_Morbidities_Cancer , @VTE_Assesments_PRF_Morbidities_Obesity , @VTE_Assesments_PRF_Morbidities_VaricoseVeins , @VTE_Assesments_PRF_Morbidities_InflammatoryBowel , @VTE_Assesments_PRF_Age_Years , @VTE_Assesments_PRF_Gender_OralContraceptive , @VTE_Assesments_PRF_Gender_HormoneReplacementTherapy , @VTE_Assesments_PRF_Gender_Pregnancy , @VTE_Assesments_PRF_Score , @VTE_Assesments_RFS_Score , @VTE_Assesments_RFS_Level , @VTE_Assesments_Relative_SystolicHypertension , @VTE_Assesments_Relative_HaemorrhagicStroke , @VTE_Assesments_Relative_ActiveBleeding , @VTE_Assesments_Relative_AntiCoagulants , @VTE_Assesments_Relative_LowPlatelets , @VTE_Assesments_Relative_BleedingDisorder , @VTE_Assesments_Doctor_DoctorNotified , @VTE_Assesments_Doctor_ReasonNotNotified , @VTE_Assesments_Doctor_TreatmentInitiated , @VTE_Assesments_Doctor_ReasonNotInitiated , @VTE_Assesments_CreatedDate , @VTE_Assesments_CreatedBy , @VTE_Assesments_ModifiedDate , @VTE_Assesments_ModifiedBy , @VTE_Assesments_History , @VTE_Assesments_IsActive ); SELECT @VTE_Assesments_Id = SCOPE_IDENTITY()";
      SqlDataSource_VTE_Form.SelectCommand = "SELECT * FROM Form_VTE_Assesments WHERE (VTE_Assesments_Id = @VTE_Assesments_Id)";
      SqlDataSource_VTE_Form.UpdateCommand = "UPDATE Form_VTE_Assesments SET VTE_Assesments_Date = @VTE_Assesments_Date , VTE_Assesments_By = @VTE_Assesments_By , Unit_Id = @Unit_Id , VTE_Assesments_Weight = @VTE_Assesments_Weight , VTE_Assesments_Height = @VTE_Assesments_Height , VTE_Assesments_BMI = @VTE_Assesments_BMI , VTE_Assesments_BRF_Medical_Stroke = @VTE_Assesments_BRF_Medical_Stroke , VTE_Assesments_BRF_Medical_HeartAttack = @VTE_Assesments_BRF_Medical_HeartAttack , VTE_Assesments_BRF_Medical_HeartFailure = @VTE_Assesments_BRF_Medical_HeartFailure , VTE_Assesments_BRF_Medical_Infection = @VTE_Assesments_BRF_Medical_Infection , VTE_Assesments_BRF_Medical_Thrombolytic = @VTE_Assesments_BRF_Medical_Thrombolytic , VTE_Assesments_BRF_Medical_CVA = @VTE_Assesments_BRF_Medical_CVA , VTE_Assesments_BRF_Surgical_SurgeryOfPelvis = @VTE_Assesments_BRF_Surgical_SurgeryOfPelvis , VTE_Assesments_BRF_Surgical_FractureOfPelvis = @VTE_Assesments_BRF_Surgical_FractureOfPelvis , VTE_Assesments_BRF_Surgical_MultipleTrauma = @VTE_Assesments_BRF_Surgical_MultipleTrauma , VTE_Assesments_BRF_Surgical_SpinalCordInjury = @VTE_Assesments_BRF_Surgical_SpinalCordInjury , VTE_Assesments_BRF_Surgical_Plaster = @VTE_Assesments_BRF_Surgical_Plaster , VTE_Assesments_BRF_Surgical_SurgeryAbove45Min = @VTE_Assesments_BRF_Surgical_SurgeryAbove45Min , VTE_Assesments_BRF_Surgical_SurgeryBelow45Min = @VTE_Assesments_BRF_Surgical_SurgeryBelow45Min , VTE_Assesments_BRF_Both_PatientInBed = @VTE_Assesments_BRF_Both_PatientInBed , VTE_Assesments_BRF_Score = @VTE_Assesments_BRF_Score , VTE_Assesments_PRF_Morbidities_History = @VTE_Assesments_PRF_Morbidities_History , VTE_Assesments_PRF_Morbidities_Cancer = @VTE_Assesments_PRF_Morbidities_Cancer , VTE_Assesments_PRF_Morbidities_Obesity = @VTE_Assesments_PRF_Morbidities_Obesity , VTE_Assesments_PRF_Morbidities_VaricoseVeins = @VTE_Assesments_PRF_Morbidities_VaricoseVeins , VTE_Assesments_PRF_Morbidities_InflammatoryBowel = @VTE_Assesments_PRF_Morbidities_InflammatoryBowel , VTE_Assesments_PRF_Age_Years = @VTE_Assesments_PRF_Age_Years , VTE_Assesments_PRF_Gender_OralContraceptive = @VTE_Assesments_PRF_Gender_OralContraceptive , VTE_Assesments_PRF_Gender_HormoneReplacementTherapy = @VTE_Assesments_PRF_Gender_HormoneReplacementTherapy , VTE_Assesments_PRF_Gender_Pregnancy = @VTE_Assesments_PRF_Gender_Pregnancy , VTE_Assesments_PRF_Score = @VTE_Assesments_PRF_Score , VTE_Assesments_RFS_Score = @VTE_Assesments_RFS_Score , VTE_Assesments_RFS_Level = @VTE_Assesments_RFS_Level , VTE_Assesments_Relative_SystolicHypertension = @VTE_Assesments_Relative_SystolicHypertension , VTE_Assesments_Relative_HaemorrhagicStroke = @VTE_Assesments_Relative_HaemorrhagicStroke , VTE_Assesments_Relative_ActiveBleeding = @VTE_Assesments_Relative_ActiveBleeding , VTE_Assesments_Relative_AntiCoagulants = @VTE_Assesments_Relative_AntiCoagulants , VTE_Assesments_Relative_LowPlatelets = @VTE_Assesments_Relative_LowPlatelets , VTE_Assesments_Relative_BleedingDisorder = @VTE_Assesments_Relative_BleedingDisorder , VTE_Assesments_Doctor_DoctorNotified = @VTE_Assesments_Doctor_DoctorNotified , VTE_Assesments_Doctor_ReasonNotNotified = @VTE_Assesments_Doctor_ReasonNotNotified , VTE_Assesments_Doctor_TreatmentInitiated = @VTE_Assesments_Doctor_TreatmentInitiated , VTE_Assesments_Doctor_ReasonNotInitiated = @VTE_Assesments_Doctor_ReasonNotInitiated , VTE_Assesments_ModifiedDate = @VTE_Assesments_ModifiedDate , VTE_Assesments_ModifiedBy = @VTE_Assesments_ModifiedBy , VTE_Assesments_History = @VTE_Assesments_History , VTE_Assesments_IsActive = @VTE_Assesments_IsActive WHERE VTE_Assesments_Id = @VTE_Assesments_Id";      
      SqlDataSource_VTE_Form.InsertParameters.Clear();
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Id", TypeCode.Int32, "");
      SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_VisitInformation_Id", TypeCode.Int32, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_ReportNumber", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Date", TypeCode.DateTime, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_By", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Weight", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Height", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BMI", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Medical_Stroke", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Medical_HeartAttack", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Medical_HeartFailure", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Medical_Infection", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Medical_Thrombolytic", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Medical_CVA", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Surgical_SurgeryOfPelvis", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Surgical_FractureOfPelvis", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Surgical_MultipleTrauma", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Surgical_SpinalCordInjury", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Surgical_Plaster", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Surgical_SurgeryAbove45Min", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Surgical_SurgeryBelow45Min", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Both_PatientInBed", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_BRF_Score", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Morbidities_History", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Morbidities_Cancer", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Morbidities_Obesity", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Morbidities_VaricoseVeins", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Morbidities_InflammatoryBowel", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Age_Years", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Gender_OralContraceptive", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Gender_HormoneReplacementTherapy", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Gender_Pregnancy", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_PRF_Score", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_RFS_Score", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_RFS_Level", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Relative_SystolicHypertension", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Relative_HaemorrhagicStroke", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Relative_ActiveBleeding", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Relative_AntiCoagulants", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Relative_LowPlatelets", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Relative_BleedingDisorder", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Doctor_DoctorNotified", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Doctor_ReasonNotNotified", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Doctor_TreatmentInitiated", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_Doctor_ReasonNotInitiated", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_CreatedBy", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_ModifiedBy", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_History", TypeCode.String, "");
      SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_VTE_Form.InsertParameters.Add("VTE_Assesments_IsActive", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.SelectParameters.Clear();
      SqlDataSource_VTE_Form.SelectParameters.Add("VTE_Assesments_Id", TypeCode.Int32, Request.QueryString["VTEAssesmentsId"]);
      SqlDataSource_VTE_Form.UpdateParameters.Clear();
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Date", TypeCode.DateTime, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_By", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Weight", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Height", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BMI", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Medical_Stroke", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Medical_HeartAttack", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Medical_HeartFailure", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Medical_Infection", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Medical_Thrombolytic", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Medical_CVA", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Surgical_SurgeryOfPelvis", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Surgical_FractureOfPelvis", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Surgical_MultipleTrauma", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Surgical_SpinalCordInjury", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Surgical_Plaster", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Surgical_SurgeryAbove45Min", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Surgical_SurgeryBelow45Min", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Both_PatientInBed", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_BRF_Score", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Morbidities_History", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Morbidities_Cancer", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Morbidities_Obesity", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Morbidities_VaricoseVeins", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Morbidities_InflammatoryBowel", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Age_Years", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Gender_OralContraceptive", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Gender_HormoneReplacementTherapy", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Gender_Pregnancy", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_PRF_Score", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_RFS_Score", TypeCode.Decimal, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_RFS_Level", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Relative_SystolicHypertension", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Relative_HaemorrhagicStroke", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Relative_ActiveBleeding", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Relative_AntiCoagulants", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Relative_LowPlatelets", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Relative_BleedingDisorder", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Doctor_DoctorNotified", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Doctor_ReasonNotNotified", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Doctor_TreatmentInitiated", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Doctor_ReasonNotInitiated", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_ModifiedBy", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_History", TypeCode.String, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_IsActive", TypeCode.Boolean, "");
      SqlDataSource_VTE_Form.UpdateParameters.Add("VTE_Assesments_Id", TypeCode.Int32, "");

      SqlDataSource_VTE_InsertUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_VTE_InsertUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_VTE_InsertUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_VTE_InsertUnit.SelectParameters.Clear();
      SqlDataSource_VTE_InsertUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_VTE_InsertUnit.SelectParameters.Add("Form_Id", TypeCode.String, "51");
      SqlDataSource_VTE_InsertUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_VTE_InsertUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_VTE_InsertUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_VTE_InsertUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_VTE_EditUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_VTE_EditUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_VTE_EditUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_VTE_EditUnit.SelectParameters.Clear();
      SqlDataSource_VTE_EditUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, "");
      SqlDataSource_VTE_EditUnit.SelectParameters.Add("Form_Id", TypeCode.String, "51");
      SqlDataSource_VTE_EditUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_VTE_EditUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_VTE_EditUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_VTE_EditUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_VTE_Assesments.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_VTE_Assesments.SelectCommand = "spForm_Get_VTE_Assesments";
      SqlDataSource_VTE_Assesments.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_VTE_Assesments.CancelSelectOnNullParameter = false;
      SqlDataSource_VTE_Assesments.SelectParameters.Clear();
      SqlDataSource_VTE_Assesments.SelectParameters.Add("VTE_VisitInformation_Id", TypeCode.String, Request.QueryString["VTEVisitInformationId"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("51")).ToString(), CultureInfo.CurrentCulture);
      Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("51").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
      Label_VisitInfoHeading.Text = Convert.ToString("Visit Information", CultureInfo.CurrentCulture);
      Label_CurrentAssesmentHeading.Text = Convert.ToString("Assesments", CultureInfo.CurrentCulture);
      Label_AssesmentHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("51").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
    }

    private void SetFormQueryString()
    {
      if (Request.QueryString["s_Facility_Id"] == null && Request.QueryString["s_VTE_VisitInformation_VisitNumber"] == null && Request.QueryString["VTEVisitInformationId"] == null)
      {
        DropDownList_Facility.SelectedValue = "";
        TextBox_PatientVisitNumber.Text = "";
      }
      else
      {
        if (Request.QueryString["VTEVisitInformationId"] == null)
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_VTE_VisitInformation_VisitNumber"];
        }
        else
        {
          Session["FacilityId"] = "";
          Session["VTEVisitInformationVisitNumber"] = "";
          string SQLStringVisitInfo = "SELECT Facility_Id , VTE_VisitInformation_VisitNumber FROM Form_VTE_VisitInformation WHERE VTE_VisitInformation_Id = @VTE_VisitInformation_Id";
          using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
          {
            SqlCommand_VisitInfo.Parameters.AddWithValue("@VTE_VisitInformation_Id", Request.QueryString["VTEVisitInformationId"]);
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
                  Session["VTEVisitInformationVisitNumber"] = DataRow_Row["VTE_VisitInformation_VisitNumber"];
                }
              }
            }
          }

          DropDownList_Facility.SelectedValue = Session["FacilityId"].ToString();
          TextBox_PatientVisitNumber.Text = Session["VTEVisitInformationVisitNumber"].ToString();

          Session.Remove("FacilityId");
          Session.Remove("VTEVisitInformationVisitNumber");
        }
      }
    }

    protected void TableVisible()
    {
      if (TableVisitInfo.Visible == true)
      {
        TableVisitInfoVisible();
      }

      if (TableCurrentAssesment.Visible == true)
      {
        TableCurrentAssesmentVisible();
      }
    }


    private void VisitData()
    {
      DataTable DataTable_VisitData;
      using (DataTable_VisitData = new DataTable())
      {
        DataTable_VisitData.Locale = CultureInfo.CurrentCulture;
        //DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_VTE_VisitInformation_VisitNumber"]).Copy();
        DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_VTE_VisitInformation_VisitNumber"]).Copy();
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
            string PatientSurnameName = DataRow_Row["Surname"].ToString() + "," + DataRow_Row["Name"].ToString();
            string PatientAge = DataRow_Row["PatientAge"].ToString();

            string NameSurnamePI = PatientSurnameName;
            NameSurnamePI = NameSurnamePI.Replace("'", "");
            PatientSurnameName = NameSurnamePI;
            NameSurnamePI = "";

            string VTEVisitInformationId = "";
            string SQLStringVisitInfo = "SELECT VTE_VisitInformation_Id FROM Form_VTE_VisitInformation WHERE Facility_Id = @Facility_Id AND VTE_VisitInformation_VisitNumber = @VTE_VisitInformation_VisitNumber";
            using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
            {
              SqlCommand_VisitInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
              SqlCommand_VisitInfo.Parameters.AddWithValue("@VTE_VisitInformation_VisitNumber", Request.QueryString["s_VTE_VisitInformation_VisitNumber"]);
              DataTable DataTable_VisitInfo;
              using (DataTable_VisitInfo = new DataTable())
              {
                DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
                DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
                if (DataTable_VisitInfo.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row1 in DataTable_VisitInfo.Rows)
                  {
                    VTEVisitInformationId = DataRow_Row1["VTE_VisitInformation_Id"].ToString();
                  }
                }
              }
            }

            if (string.IsNullOrEmpty(VTEVisitInformationId))
            {
              string SQLStringInsertVisitInformation = "INSERT INTO Form_VTE_VisitInformation ( Facility_Id , VTE_VisitInformation_VisitNumber , PatientInformation_Id , VTE_VisitInformation_PatientName , VTE_VisitInformation_PatientAge , VTE_VisitInformation_DateOfAdmission , VTE_VisitInformation_DateOfDischarge , VTE_VisitInformation_Archived ) VALUES ( @Facility_Id , @VTE_VisitInformation_VisitNumber , @PatientInformation_Id , @VTE_VisitInformation_PatientName , @VTE_VisitInformation_PatientAge , @VTE_VisitInformation_DateOfAdmission , @VTE_VisitInformation_DateOfDischarge , @VTE_VisitInformation_Archived ); SELECT SCOPE_IDENTITY()";
              using (SqlCommand SqlCommand_InsertVisitInformation = new SqlCommand(SQLStringInsertVisitInformation))
              {
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_VisitNumber", Request.QueryString["s_VTE_VisitInformation_VisitNumber"]);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", DBNull.Value);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_PatientName", PatientSurnameName);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_PatientAge", PatientAge);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_DateOfAdmission", DateOfAdmission);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_DateOfDischarge", DateOfDischarge);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_Archived", 0);
                VTEVisitInformationId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertVisitInformation);
              }
            }
            else
            {
              string SQLStringUpdateVisitInformation = "UPDATE Form_VTE_VisitInformation SET PatientInformation_Id = @PatientInformation_Id , VTE_VisitInformation_PatientName = @VTE_VisitInformation_PatientName , VTE_VisitInformation_PatientAge  = @VTE_VisitInformation_PatientAge , VTE_VisitInformation_DateOfAdmission  = @VTE_VisitInformation_DateOfAdmission , VTE_VisitInformation_DateOfDischarge  = @VTE_VisitInformation_DateOfDischarge WHERE Facility_Id = @Facility_Id AND VTE_VisitInformation_VisitNumber = @VTE_VisitInformation_VisitNumber";
              using (SqlCommand SqlCommand_UpdateVisitInformation = new SqlCommand(SQLStringUpdateVisitInformation))
              {
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", DBNull.Value);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_PatientName", PatientSurnameName);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_PatientAge", PatientAge);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_DateOfAdmission", DateOfAdmission);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_DateOfDischarge", DateOfDischarge);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_VisitNumber", Request.QueryString["s_VTE_VisitInformation_VisitNumber"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateVisitInformation);
              }
            }

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_VTE", "Form_VTE.aspx?VTEVisitInformationId=" + VTEVisitInformationId), false);
          }
        }
      }

      ////String PatientInformationId = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PatientInformationId(Request.QueryString["s_Facility_Id"], Request.QueryString["s_VTE_VisitInformation_VisitNumber"]);
      //String PatientInformationId = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_PatientInformationId(Request.QueryString["s_Facility_Id"], Request.QueryString["s_VTE_VisitInformation_VisitNumber"]);
      //Int32 FindError = PatientInformationId.IndexOf("Error", StringComparison.CurrentCulture);

      //if (FindError > -1)
      //{
      //  Label_InvalidSearchMessage.Text = PatientInformationId;
      //  TableVisitInfo.Visible = false;
      //}
      //else
      //{
      //  DataTable DataTable_VisitData;
      //  using (DataTable_VisitData = new DataTable())
      //  {
      //    DataTable_VisitData.Locale = CultureInfo.CurrentCulture;
      //    //DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_VTE_VisitInformation_VisitNumber"]).Copy();
      //    DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_VTE_VisitInformation_VisitNumber"]).Copy();
      //    if (DataTable_VisitData.Columns.Count == 1)
      //    {
      //      String Error = "";
      //      foreach (DataRow DataRow_Row in DataTable_VisitData.Rows)
      //      {
      //        Error = DataRow_Row["Error"].ToString();
      //      }

      //      Label_InvalidSearchMessage.Text = Error;
      //      TableVisitInfo.Visible = false;
      //      Error = "";
      //    }
      //    else if (DataTable_VisitData.Columns.Count != 1)
      //    {
      //      foreach (DataRow DataRow_Row in DataTable_VisitData.Rows)
      //      {
      //        String DateOfAdmission = DataRow_Row["DateOfAdmission"].ToString();
      //        String DateOfDischarge = DataRow_Row["DateOfDischarge"].ToString();
      //        String PatientAge = DataRow_Row["PatientAge"].ToString();

      //        String VTEVisitInformationId = "";
      //        String SQLStringVisitInfo = "SELECT VTE_VisitInformation_Id FROM Form_VTE_VisitInformation WHERE Facility_Id = @Facility_Id AND VTE_VisitInformation_VisitNumber = @VTE_VisitInformation_VisitNumber";
      //        using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      //        {
      //          SqlCommand_VisitInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
      //          SqlCommand_VisitInfo.Parameters.AddWithValue("@VTE_VisitInformation_VisitNumber", Request.QueryString["s_VTE_VisitInformation_VisitNumber"]);
      //          DataTable DataTable_VisitInfo;
      //          using (DataTable_VisitInfo = new DataTable())
      //          {
      //            DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
      //            DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
      //            if (DataTable_VisitInfo.Rows.Count > 0)
      //            {
      //              foreach (DataRow DataRow_Row1 in DataTable_VisitInfo.Rows)
      //              {
      //                VTEVisitInformationId = DataRow_Row1["VTE_VisitInformation_Id"].ToString();
      //              }
      //            }
      //          }
      //        }

      //        if (String.IsNullOrEmpty(VTEVisitInformationId))
      //        {
      //          String SQLStringInsertVisitInformation = "INSERT INTO Form_VTE_VisitInformation ( Facility_Id , VTE_VisitInformation_VisitNumber , PatientInformation_Id , VTE_VisitInformation_PatientAge , VTE_VisitInformation_DateOfAdmission , VTE_VisitInformation_DateOfDischarge , VTE_VisitInformation_Archived ) VALUES ( @Facility_Id , @VTE_VisitInformation_VisitNumber , @PatientInformation_Id , @VTE_VisitInformation_PatientAge , @VTE_VisitInformation_DateOfAdmission , @VTE_VisitInformation_DateOfDischarge , @VTE_VisitInformation_Archived ); SELECT SCOPE_IDENTITY()";
      //          using (SqlCommand SqlCommand_InsertVisitInformation = new SqlCommand(SQLStringInsertVisitInformation))
      //          {
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_VisitNumber", Request.QueryString["s_VTE_VisitInformation_VisitNumber"]);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_PatientAge", PatientAge);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_DateOfAdmission", DateOfAdmission);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_DateOfDischarge", DateOfDischarge);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_Archived", 0);
      //            VTEVisitInformationId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertVisitInformation);
      //          }
      //        }
      //        else
      //        {
      //          String SQLStringUpdateVisitInformation = "UPDATE Form_VTE_VisitInformation SET PatientInformation_Id = @PatientInformation_Id , VTE_VisitInformation_PatientAge  = @VTE_VisitInformation_PatientAge , VTE_VisitInformation_DateOfAdmission  = @VTE_VisitInformation_DateOfAdmission , VTE_VisitInformation_DateOfDischarge  = @VTE_VisitInformation_DateOfDischarge WHERE Facility_Id = @Facility_Id AND VTE_VisitInformation_VisitNumber = @VTE_VisitInformation_VisitNumber";
      //          using (SqlCommand SqlCommand_UpdateVisitInformation = new SqlCommand(SQLStringUpdateVisitInformation))
      //          {
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_PatientAge", PatientAge);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_DateOfAdmission", DateOfAdmission);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_DateOfDischarge", DateOfDischarge);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@VTE_VisitInformation_VisitNumber", Request.QueryString["s_VTE_VisitInformation_VisitNumber"]);
      //            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateVisitInformation);
      //          }
      //        }

      //        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_VTE", "Form_VTE.aspx?VTEVisitInformationId=" + VTEVisitInformationId), false);
      //      }
      //    }
      //  }
      //}
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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('51')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_VTE_VisitInformation WHERE VTE_VisitInformation_Id = @VTE_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@VTE_VisitInformation_Id", Request.QueryString["VTEVisitInformationId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '202'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '203'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '204'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '205'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_FacilityId
    {
      public string FacilityId { get; set; }
      public string VTEVisitInformationVisitNumber { get; set; }
    }

    private FromDataBase_FacilityId GetFacilityId()
    {
      FromDataBase_FacilityId FromDataBase_FacilityId_New = new FromDataBase_FacilityId();

      string SQLStringFacility = "SELECT Facility_Id , VTE_VisitInformation_VisitNumber FROM Form_VTE_VisitInformation WHERE VTE_VisitInformation_Id = @VTE_VisitInformation_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@VTE_VisitInformation_Id", Request.QueryString["VTEVisitInformationId"]);
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
              FromDataBase_FacilityId_New.VTEVisitInformationVisitNumber = DataRow_Row["VTE_VisitInformation_VisitNumber"].ToString();
            }
          }
        }
      }

      return FromDataBase_FacilityId_New;
    }

    private class FromDataBase_FormViewUpdate
    {
      public string ViewUpdate { get; set; }
    }

    private FromDataBase_FormViewUpdate GetFormViewUpdate()
    {
      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_New = new FromDataBase_FormViewUpdate();

      string SQLStringFormViewUpdate = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 51),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,VTE_Assesments_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 51),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,VTE_Assesments_Date)+1,0))) < GETDATE() THEN 'No' END AS ViewUpdate FROM Form_VTE_Assesments WHERE VTE_Assesments_Id = @VTE_Assesments_Id";
      using (SqlCommand SqlCommand_FormViewUpdate = new SqlCommand(SQLStringFormViewUpdate))
      {
        SqlCommand_FormViewUpdate.Parameters.AddWithValue("@VTE_Assesments_Id", Request.QueryString["VTEAssesmentsId"]);
        DataTable DataTable_FormViewUpdate;
        using (DataTable_FormViewUpdate = new DataTable())
        {
          DataTable_FormViewUpdate.Locale = CultureInfo.CurrentCulture;
          DataTable_FormViewUpdate = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormViewUpdate).Copy();
          if (DataTable_FormViewUpdate.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormViewUpdate.Rows)
            {
              FromDataBase_FormViewUpdate_New.ViewUpdate = DataRow_Row["ViewUpdate"].ToString();
            }
          }
        }
      }

      return FromDataBase_FormViewUpdate_New;
    }


    //--START-- --Search--//
    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Form", "Form_VTE.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect("Form_VTE.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_VTE_VisitInformation_VisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "", false);
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
      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_UnitId"];
      string SearchField3 = Request.QueryString["Search_VTEPatientVisitNumber"];
      string SearchField4 = Request.QueryString["Search_VTEPatientName"];
      string SearchField5 = Request.QueryString["Search_VTEReportNumber"];

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
        SearchField3 = "s_VTE_PatientVisitNumber=" + Request.QueryString["Search_VTEPatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_VTE_PatientName=" + Request.QueryString["Search_VTEPatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_VTE_ReportNumber=" + Request.QueryString["Search_VTEReportNumber"] + "&";
      }

      string FinalURL = "Form_VTE_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --VisitInfo--//
    private void TableVisitInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["VTEVisitInformationVisitNumber"] = "";
      Session["VTEVisitInformationPatientName"] = "";
      Session["VTEVisitInformationPatientAge"] = "";
      Session["VTEVisitInformationDateOfAdmission"] = "";
      Session["VTEVisitInformationDateOfDischarge"] = "";
      string SQLStringVisitInfo = "SELECT Facility_FacilityDisplayName , VTE_VisitInformation_VisitNumber , VTE_VisitInformation_PatientName , VTE_VisitInformation_PatientAge , VTE_VisitInformation_DateOfAdmission , VTE_VisitInformation_DateOfDischarge FROM vForm_VTE_VisitInformation WHERE VTE_VisitInformation_Id = @VTE_VisitInformation_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@VTE_VisitInformation_Id", Request.QueryString["VTEVisitInformationId"]);
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
              Session["VTEVisitInformationVisitNumber"] = DataRow_Row["VTE_VisitInformation_VisitNumber"];
              Session["VTEVisitInformationPatientName"] = DataRow_Row["VTE_VisitInformation_PatientName"];
              Session["VTEVisitInformationPatientAge"] = DataRow_Row["VTE_VisitInformation_PatientAge"];
              Session["VTEVisitInformationDateOfAdmission"] = DataRow_Row["VTE_VisitInformation_DateOfAdmission"];
              Session["VTEVisitInformationDateOfDischarge"] = DataRow_Row["VTE_VisitInformation_DateOfDischarge"];
            }
          }
        }
      }

      Label_VIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_VIVisitNumber.Text = Session["VTEVisitInformationVisitNumber"].ToString();
      Label_VIName.Text = Session["VTEVisitInformationPatientName"].ToString();
      Label_VIAge.Text = Session["VTEVisitInformationPatientAge"].ToString();
      HiddenField_VIAge.Value = Session["VTEVisitInformationPatientAge"].ToString();
      Label_VIDateAdmission.Text = Session["VTEVisitInformationDateOfAdmission"].ToString();
      Label_VIDateDischarge.Text = Session["VTEVisitInformationDateOfDischarge"].ToString();

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("VTEVisitInformationVisitNumber");
      Session.Remove("VTEVisitInformationPatientName");
      Session.Remove("VTEVisitInformationPatientAge");
      Session.Remove("VTEVisitInformationDateOfAdmission");
      Session.Remove("VTEVisitInformationDateOfDischarge");
    }
    //---END--- --VisitInfo--//


    //--START-- --TableCurrentAssesment--//
    protected void SetCurrentAssesmentVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["VTEAssesmentsId"]))
      {
        SetCurrentAssesmentVisibility_Insert();
      }
      else
      {
        SetCurrentAssesmentVisibility_Edit();
      }
    }

    protected void SetCurrentAssesmentVisibility_Insert()
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
        FormView_VTE_Form.ChangeMode(FormViewMode.Insert);
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_VTE_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_VTE_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetCurrentAssesmentVisibility_Edit()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_Current = GetFormViewUpdate();
      string ViewUpdate = FromDataBase_FormViewUpdate_Current.ViewUpdate;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
      {
        Security = "0";
        FormView_VTE_Form.ChangeMode(FormViewMode.Edit);
      }

      if (Security == "1" && (SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (ViewUpdate == "Yes")
        {
          FormView_VTE_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_VTE_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_VTE_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_VTE_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentAssesmentVisible()
    {
      if (FormView_VTE_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertBy")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertBy")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertUnit")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertWeight")).Attributes.Add("OnKeyUp", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertWeight")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertHeight")).Attributes.Add("OnKeyUp", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertHeight")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();ShowHide_Form();");

        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalStroke")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalHeartAttack")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalHeartFailure")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalInfection")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalThrombolytic")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalCVA")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalSurgeryOfPelvis")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalFractureOfPelvis")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalMultipleTrauma")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalSpinalCordInjury")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalPlaster")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalSurgeryAbove45Min")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalSurgeryBelow45Min")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFBothPatientInBed")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");

        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFMorbiditiesHistory")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFMorbiditiesCancer")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFMorbiditiesVaricoseVeins")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFMorbiditiesInflammatoryBowel")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFGenderOralContraceptive")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFGenderHormoneReplacementTherapy")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFGenderPregnancy")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");

        ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorDoctorNotified")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDoctorReasonNotNotified")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDoctorReasonNotNotified")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorTreatmentInitiated")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDoctorReasonNotInitiated")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDoctorReasonNotInitiated")).Attributes.Add("OnInput", "Validation_Form();");
      }

      if (FormView_VTE_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditBy")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("Textbox_EditBy")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditUnit")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditWeight")).Attributes.Add("OnKeyUp", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditWeight")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditHeight")).Attributes.Add("OnKeyUp", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditHeight")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();ShowHide_Form();");

        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalStroke")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalHeartAttack")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalHeartFailure")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalInfection")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalThrombolytic")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalCVA")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalSurgeryOfPelvis")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalFractureOfPelvis")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalMultipleTrauma")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalSpinalCordInjury")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalPlaster")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalSurgeryAbove45Min")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalSurgeryBelow45Min")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFBothPatientInBed")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");

        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFMorbiditiesHistory")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFMorbiditiesCancer")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFMorbiditiesVaricoseVeins")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFMorbiditiesInflammatoryBowel")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFGenderOralContraceptive")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFGenderHormoneReplacementTherapy")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFGenderPregnancy")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");

        ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorDoctorNotified")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDoctorReasonNotNotified")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDoctorReasonNotNotified")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorTreatmentInitiated")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDoctorReasonNotInitiated")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDoctorReasonNotInitiated")).Attributes.Add("OnInput", "Validation_Form();");
      }
    }


    protected void FormView_VTE_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation();

        if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;
        }
        else
        {
          e.Cancel = true;
        }

        if (e.Cancel == true)
        {
          ToolkitScriptManager_VTE.SetFocus(UpdatePanel_VTE);
          ((Label)FormView_VTE_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_VTE_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
          Session["FacilityId"] = FromDataBase_FacilityId_Current.FacilityId;

          Session["VTE_Assesments_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Session["FacilityId"].ToString(), "51");

          SqlDataSource_VTE_Form.InsertParameters["Unit_Id"].DefaultValue = ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertUnit")).SelectedValue;

          SqlDataSource_VTE_Form.InsertParameters["VTE_VisitInformation_Id"].DefaultValue = Request.QueryString["VTEVisitInformationId"];
          SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_ReportNumber"].DefaultValue = Session["VTE_Assesments_ReportNumber"].ToString();
          SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_History"].DefaultValue = "";
          SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_IsActive"].DefaultValue = "true";

          Decimal Weight = Convert.ToDecimal(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertWeight")).Text, CultureInfo.CurrentCulture);
          Decimal Height = Convert.ToDecimal(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertHeight")).Text, CultureInfo.CurrentCulture);
          Decimal BMI = Convert.ToDecimal((Weight / (Height * Height)));
          SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_BMI"].DefaultValue = Decimal.Round(BMI, 2, MidpointRounding.ToEven).ToString(CultureInfo.CurrentCulture);

          SqlDataSource_VTE_Form.InsertParameters["VTE_Assesments_Doctor_DoctorNotified"].DefaultValue = ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorDoctorNotified")).SelectedValue;

          Session["VTE_Assesments_ReportNumber"] = "";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertBy")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertWeight")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertHeight")).Text))
        {
          InvalidForm = "Yes";
        }

        InvalidForm = InsertValidation_BRF(InvalidForm);

        InvalidForm = InsertValidation_PRF(InvalidForm);

        InvalidForm = InsertValidation_Doctor(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateDate = ((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDate")).Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Assesment Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Assesment dates allowed<br />";
          }
          else
          {
            Session["ValidCapture"] = "";
            Session["CutOffDay"] = "";
            string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 51),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@VTE_Assesments_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 51),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@VTE_Assesments_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 51) AS CutOffDay";
            using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
            {
              SqlCommand_ValidCapture.Parameters.AddWithValue("@VTE_Assesments_Date", PickedDate);
              DataTable DataTable_ValidCapture;
              using (DataTable_ValidCapture = new DataTable())
              {
                DataTable_ValidCapture.Locale = CultureInfo.CurrentCulture;
                DataTable_ValidCapture = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ValidCapture).Copy();
                if (DataTable_ValidCapture.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_ValidCapture.Rows)
                  {
                    Session["ValidCapture"] = DataRow_Row["ValidCapture"];
                    Session["CutOffDay"] = DataRow_Row["CutOffDay"];
                  }
                }
              }
            }

            if (Session["ValidCapture"].ToString() != "Yes")
            {
              InvalidFormMessage = InvalidFormMessage + "Assesment date is not valid. Forms may be captured between the 1st of a calendar month until the " + Session["CutOffDay"].ToString() + "th of the following month <br />";
            }

            Session["ValidCapture"] = "";
            Session["CutOffDay"] = "";
          }
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertWeight")).Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Weight is not in the correct format<br />";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertHeight")).Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Height is not in the correct format<br />";
        }
      }

      return InvalidFormMessage;
    }

    protected string InsertValidation_BRF(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalStroke")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalHeartAttack")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalHeartFailure")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalInfection")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalThrombolytic")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFMedicalCVA")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalSurgeryOfPelvis")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalFractureOfPelvis")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalMultipleTrauma")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalSpinalCordInjury")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalPlaster")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalSurgeryAbove45Min")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFSurgicalSurgeryBelow45Min")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertBRFBothPatientInBed")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string InsertValidation_PRF(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFMorbiditiesHistory")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFMorbiditiesCancer")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFMorbiditiesVaricoseVeins")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFMorbiditiesInflammatoryBowel")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFGenderOralContraceptive")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFGenderHormoneReplacementTherapy")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_InsertPRFGenderPregnancy")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string InsertValidation_Doctor(string invalidForm)
    {
      string InvalidForm = invalidForm;

      Int32 RFSScore = Convert.ToInt32(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertRFSScore")).Text, CultureInfo.CurrentCulture);

      if (RFSScore > 2)
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorDoctorNotified")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
        else
        {
          if (((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorDoctorNotified")).SelectedValue == "No")
          {
            if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDoctorReasonNotNotified")).Text))
            {
              InvalidForm = "Yes";
            }
          }
        }


        if (string.IsNullOrEmpty(((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorTreatmentInitiated")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
        else
        {
          if (((DropDownList)FormView_VTE_Form.FindControl("DropDownList_InsertDoctorTreatmentInitiated")).SelectedValue == "No")
          {
            if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_InsertDoctorReasonNotInitiated")).Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected void SqlDataSource_VTE_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["VTE_Assesments_Id"] = e.Command.Parameters["@VTE_Assesments_Id"].Value;
        Session["VTE_Assesments_ReportNumber"] = e.Command.Parameters["@VTE_Assesments_ReportNumber"].Value;
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_VTE&ReportNumber=" + Session["VTE_Assesments_ReportNumber"].ToString() + ""), false);
      }
    }


    protected void FormView_VTE_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDVTEAssesmentsModifiedDate"] = e.OldValues["VTE_Assesments_ModifiedDate"];
        object OLDVTEAssesmentsModifiedDate = Session["OLDVTEAssesmentsModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDVTEAssesmentsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareVTE = (DataView)SqlDataSource_VTE_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareVTE = DataView_CompareVTE[0];
        Session["DBVTEAssesmentsModifiedDate"] = Convert.ToString(DataRowView_CompareVTE["VTE_Assesments_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBVTEAssesmentsModifiedBy"] = Convert.ToString(DataRowView_CompareVTE["VTE_Assesments_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBVTEAssesmentsModifiedDate = Session["DBVTEAssesmentsModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBVTEAssesmentsModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_VTE.SetFocus(UpdatePanel_VTE);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBVTEAssesmentsModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_VTE_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_VTE_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation();

          if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
          }
          else
          {
            e.Cancel = true;
          }

          if (e.Cancel == true)
          {
            ToolkitScriptManager_VTE.SetFocus(UpdatePanel_VTE);
            ((Label)FormView_VTE_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_VTE_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["VTE_Assesments_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["VTE_Assesments_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];
            e.NewValues["Unit_Id"] = ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditUnit")).SelectedValue;

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_VTE_Assesments", "VTE_Assesments_Id = " + Request.QueryString["VTEAssesmentsId"]);

            DataView DataView_VTE = (DataView)SqlDataSource_VTE_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_VTE = DataView_VTE[0];
            Session["VTEAssesmentsHistory"] = Convert.ToString(DataRowView_VTE["VTE_Assesments_History"], CultureInfo.CurrentCulture);

            Session["VTEAssesmentsHistory"] = Session["History"].ToString() + Session["VTEAssesmentsHistory"].ToString();
            e.NewValues["VTE_Assesments_History"] = Session["VTEAssesmentsHistory"].ToString();

            Session["VTEAssesmentsHistory"] = "";
            Session["History"] = "";

            Decimal Weight = Convert.ToDecimal(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditWeight")).Text, CultureInfo.CurrentCulture);
            Decimal Height = Convert.ToDecimal(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditHeight")).Text, CultureInfo.CurrentCulture);
            Decimal BMI = Convert.ToDecimal((Weight / (Height * Height)));
            e.NewValues["VTE_Assesments_BMI"] = Decimal.Round(BMI, 2, MidpointRounding.ToEven).ToString(CultureInfo.CurrentCulture);

            e.NewValues["VTE_Assesments_Doctor_DoctorNotified"] = ((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorDoctorNotified")).SelectedValue;
          }
        }

        Session["OLDVTEAssesmentsModifiedDate"] = "";
        Session["DBVTEAssesmentsModifiedDate"] = "";
        Session["DBVTEAssesmentsModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditBy")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditWeight")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditHeight")).Text))
        {
          InvalidForm = "Yes";
        }

        InvalidForm = EditValidation_BRF(InvalidForm);

        InvalidForm = EditValidation_PRF(InvalidForm);

        InvalidForm = EditValidation_Doctor(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateDate = ((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDate")).Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Assesment Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Assesment dates allowed<br />";
          }
          else
          {
            DateTime PreviousDate = Convert.ToDateTime(((HiddenField)FormView_VTE_Form.FindControl("HiddenField_EditDate")).Value, CultureInfo.CurrentCulture);

            if (PickedDate.CompareTo(PreviousDate) != 0)
            {
              Session["ValidCapture"] = "";
              Session["CutOffDay"] = "";
              string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 51),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@VTE_Assesments_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 51),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@VTE_Assesments_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 51) AS CutOffDay";
              using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
              {
                SqlCommand_ValidCapture.Parameters.AddWithValue("@VTE_Assesments_Date", PickedDate);
                DataTable DataTable_ValidCapture;
                using (DataTable_ValidCapture = new DataTable())
                {
                  DataTable_ValidCapture.Locale = CultureInfo.CurrentCulture;
                  DataTable_ValidCapture = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ValidCapture).Copy();
                  if (DataTable_ValidCapture.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_ValidCapture.Rows)
                    {
                      Session["ValidCapture"] = DataRow_Row["ValidCapture"];
                      Session["CutOffDay"] = DataRow_Row["CutOffDay"];
                    }
                  }
                }
              }

              if (Session["ValidCapture"].ToString() != "Yes")
              {
                InvalidFormMessage = InvalidFormMessage + "Assesment date is not valid. Forms may be captured between the 1st of a calendar month until the " + Session["CutOffDay"].ToString() + "th of the following month <br />";
              }

              Session["ValidCapture"] = "";
              Session["CutOffDay"] = "";
            }
          }
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditWeight")).Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Weight is not in the correct format<br />";
        }

        if (InfoQuestWCF.InfoQuest_All.All_ValidateDecimal(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditHeight")).Text) == 0)
        {
          InvalidFormMessage = InvalidFormMessage + "Height is not in the correct format<br />";
        }
      }

      return InvalidFormMessage;
    }

    protected string EditValidation_BRF(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalStroke")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalHeartAttack")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalHeartFailure")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalInfection")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalThrombolytic")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFMedicalCVA")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalSurgeryOfPelvis")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalFractureOfPelvis")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalMultipleTrauma")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalSpinalCordInjury")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalPlaster")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalSurgeryAbove45Min")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFSurgicalSurgeryBelow45Min")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditBRFBothPatientInBed")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string EditValidation_PRF(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFMorbiditiesHistory")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFMorbiditiesCancer")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFMorbiditiesVaricoseVeins")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFMorbiditiesInflammatoryBowel")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFGenderOralContraceptive")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFGenderHormoneReplacementTherapy")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((RadioButtonList)FormView_VTE_Form.FindControl("RadioButtonList_EditPRFGenderPregnancy")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string EditValidation_Doctor(string invalidForm)
    {
      string InvalidForm = invalidForm;

      Int32 RFSScore = Convert.ToInt32(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditRFSScore")).Text, CultureInfo.CurrentCulture);

      if (RFSScore > 2)
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorDoctorNotified")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
        else
        {
          if (((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorDoctorNotified")).SelectedValue == "No")
          {
            if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDoctorReasonNotNotified")).Text))
            {
              InvalidForm = "Yes";
            }
          }
        }


        if (string.IsNullOrEmpty(((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorTreatmentInitiated")).SelectedValue))
        {
          InvalidForm = "Yes";
        }
        else
        {
          if (((DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorTreatmentInitiated")).SelectedValue == "No")
          {
            if (string.IsNullOrEmpty(((TextBox)FormView_VTE_Form.FindControl("TextBox_EditDoctorReasonNotInitiated")).Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected void SqlDataSource_VTE_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Form", "Form_VTE.aspx?VTEVisitInformationId=" + Request.QueryString["VTEVisitInformationId"] + ""), false);
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_VTE, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Print", "InfoQuest_Print.aspx?PrintPage=Form_VTE&PrintValue=" + Request.QueryString["VTEAssesmentsId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_VTE, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_VTE, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Email", "InfoQuest_Email.aspx?EmailPage=Form_VTE&EmailValue=" + Request.QueryString["VTEAssesmentsId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_VTE, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }


    protected void FormView_VTE_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["VTEAssesmentsId"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Form", "Form_VTE.aspx?VTEVisitInformationId=" + Request.QueryString["VTEVisitInformationId"] + ""), false);
          }
        }
      }
    }

    protected void FormView_VTE_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_VTE_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_VTE_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      if (Request.QueryString["VTEAssesmentsId"] != null)
      {
        FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
        string FacilityId = FromDataBase_FacilityId_Current.FacilityId;

        DropDownList DropDownList_EditUnit = (DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditUnit");
        DataView DataView_UnitId = (DataView)SqlDataSource_VTE_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_UnitId = DataView_UnitId[0];
        DropDownList_EditUnit.SelectedValue = Convert.ToString(DataRowView_UnitId["Unit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_VTE_EditUnit.SelectParameters["Facility_Id"].DefaultValue = FacilityId;
        SqlDataSource_VTE_EditUnit.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
        SqlDataSource_VTE_EditUnit.SelectParameters["TableFROM"].DefaultValue = "Form_VTE_Assesments LEFT JOIN Form_VTE_VisitInformation ON Form_VTE_Assesments.VTE_VisitInformation_Id = Form_VTE_VisitInformation.VTE_VisitInformation_Id";
        SqlDataSource_VTE_EditUnit.SelectParameters["TableWHERE"].DefaultValue = "VTE_Assesments_Id = " + Request.QueryString["VTEAssesmentsId"] + " ";

        DropDownList DropDownList_EditDoctorDoctorNotified = (DropDownList)FormView_VTE_Form.FindControl("DropDownList_EditDoctorDoctorNotified");
        DataView DataView_Doctor = (DataView)SqlDataSource_VTE_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_Doctor = DataView_Doctor[0];
        DropDownList_EditDoctorDoctorNotified.SelectedValue = Convert.ToString(DataRowView_Doctor["VTE_Assesments_Doctor_DoctorNotified"], CultureInfo.CurrentCulture);


        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 51";
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
          ((Button)FormView_VTE_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_VTE_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_VTE_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_VTE_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["VTEAssesmentsId"] != null)
      {
        Session["UnitName"] = "";
        string SQLStringVTE = "SELECT Unit_Name FROM vForm_VTE_Assesments WHERE VTE_Assesments_Id = @VTE_Assesments_Id";
        using (SqlCommand SqlCommand_VTE = new SqlCommand(SQLStringVTE))
        {
          SqlCommand_VTE.Parameters.AddWithValue("@VTE_Assesments_Id", Request.QueryString["VTEAssesmentsId"]);
          DataTable DataTable_VTE;
          using (DataTable_VTE = new DataTable())
          {
            DataTable_VTE.Locale = CultureInfo.CurrentCulture;
            DataTable_VTE = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VTE).Copy();
            if (DataTable_VTE.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_VTE.Rows)
              {
                Session["UnitName"] = DataRow_Row["Unit_Name"];
              }
            }
          }
        }

        ((Label)FormView_VTE_Form.FindControl("Label_ItemUnit")).Text = Session["UnitName"].ToString();

        Session["UnitName"] = "";

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 51";
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
          ((Button)FormView_VTE_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_VTE_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_VTE_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Print", "InfoQuest_Print.aspx?PrintPage=Form_VTE&PrintValue=" + Request.QueryString["VTEAssesmentsId"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_VTE_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_VTE_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_VTE_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Email", "InfoQuest_Email.aspx?EmailPage=Form_VTE&EmailValue=" + Request.QueryString["VTEAssesmentsId"] + "") + "')";
        }

        Email = "";
        Print = "";
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
    //---END--- --TableCurrentAssesment--//


    //--START-- --TableAssesment--//
    protected void SqlDataSource_VTE_Assesments_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_VTE_Assesments.PageSize = Convert.ToInt32(((DropDownList)GridView_VTE_Assesments.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_VTE_Assesments.PageIndex = ((DropDownList)GridView_VTE_Assesments.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_VTE_Assesments_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_VTE_Assesments.PageSize <= 20)
        {
          ((DropDownList)GridView_VTE_Assesments.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_VTE_Assesments.PageSize > 20 && GridView_VTE_Assesments.PageSize <= 50)
        {
          ((DropDownList)GridView_VTE_Assesments.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_VTE_Assesments.PageSize > 50 && GridView_VTE_Assesments.PageSize <= 100)
        {
          ((DropDownList)GridView_VTE_Assesments.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_VTE_Assesments_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_VTE_Assesments.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_VTE_Assesments.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_VTE_Assesments.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_VTE_Assesments_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_CaptureNew_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE New Form", "Form_VTE.aspx?VTEVisitInformationId=" + Request.QueryString["VTEVisitInformationId"] + ""), false);
    }

    public string GetLink(object vte_Assesments_Id)
    {
      string LinkURL = "";
      if (vte_Assesments_Id != null)
      {
        if (Request.QueryString["VTEAssesmentsId"] == vte_Assesments_Id.ToString())
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Form", "Form_VTE.aspx?VTEVisitInformationId=" + Request.QueryString["VTEVisitInformationId"] + "&VTEAssesmentsId=" + vte_Assesments_Id + "") + "'>Selected</a>";
        }
        else
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("VTE Form", "Form_VTE.aspx?VTEVisitInformationId=" + Request.QueryString["VTEVisitInformationId"] + "&VTEAssesmentsId=" + vte_Assesments_Id + "") + "'>Select</a>";
        }
      }

      string FinalURL = LinkURL;

      return FinalURL;
    }
    //---END--- --TableAssesment--//
  }
}