using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_MedicationBundleCompliance : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_MedicationBundleCompliance, this.GetType(), "UpdateProgress_Start", "Validation_Search();Validation_Form();Calculation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          PageTitle();

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_MBC_VisitInformation_VisitNumber"] != null)
          {
            form_MedicationBundleCompliance.DefaultButton = Button_Search.UniqueID;

            SqlDataSource_MBC_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
            SqlDataSource_MBC_Facility.SelectParameters["TableFROM"].DefaultValue = "Form_MedicationBundleCompliance_VisitInformation";
            SqlDataSource_MBC_Facility.SelectParameters["TableWHERE"].DefaultValue = "Facility_Id = " + Request.QueryString["s_Facility_Id"] + " AND MBC_VisitInformation_VisitNumber = " + Request.QueryString["s_MBC_VisitInformation_VisitNumber"] + " ";

            Label_InvalidSearchMessage.Text = "";
            TableVisitInfo.Visible = false;
            TableCurrentBundle.Visible = false;
            TableBundle.Visible = false;

            VisitData();
          }
          else
          {
            if (Request.QueryString["MBCVisitInformationId"] == null)
            {
              form_MedicationBundleCompliance.DefaultButton = Button_Search.UniqueID;

              Label_InvalidSearchMessage.Text = "";
              TableVisitInfo.Visible = false;
              TableCurrentBundle.Visible = false;
              TableBundle.Visible = false;
            }
            else
            {
              SqlDataSource_MBC_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
              SqlDataSource_MBC_Facility.SelectParameters["TableFROM"].DefaultValue = "Form_MedicationBundleCompliance_VisitInformation";
              SqlDataSource_MBC_Facility.SelectParameters["TableWHERE"].DefaultValue = "MBC_VisitInformation_Id = " + Request.QueryString["MBCVisitInformationId"] + " ";

              TableVisitInfo.Visible = true;
              TableCurrentBundle.Visible = true;
              TableBundle.Visible = true;

              SetCurrentBundleVisibility();

              if (string.IsNullOrEmpty(Request.QueryString["MBCBundlesId"]))
              {
                form_MedicationBundleCompliance.DefaultButton = Button_Search.UniqueID;

                if (TableCurrentBundle.Visible == true)
                {
                  FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
                  string FacilityId = FromDataBase_FacilityId_Current.FacilityId;

                  if (((HiddenField)FormView_MedicationBundleCompliance_Form.FindControl("HiddenField_Insert")) != null)
                  {
                    ((DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).Items.Clear();
                    SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectParameters["Facility_Id"].DefaultValue = FacilityId;
                    ((DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
                    ((DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).DataBind();

                    ((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_InsertDate")).Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
                  }
                }
              }
              else
              {
                form_MedicationBundleCompliance.DefaultButton = null;

                SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
                SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
                SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters["TableFROM"].DefaultValue = "Form_MedicationBundleCompliance_Bundles";
                SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters["TableWHERE"].DefaultValue = "MBC_Bundles_Id = " + Request.QueryString["MBCBundlesId"] + " ";
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
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('38'))";
        }
        else
        {
          if (Request.QueryString["s_Facility_Id"] != null)
          {
            SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('38')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
          }

          if (Request.QueryString["IPSVisitInformationId"] != null)
          {
            SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('38')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_MedicationBundleCompliance_VisitInformation WHERE MBC_VisitInformation_Id = @MBC_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
          }
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_Security.Parameters.AddWithValue("@MBC_VisitInformation_Id", Request.QueryString["MBCVisitInformationId"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("38");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_MedicationBundleCompliance.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Medication Bundle Compliance", "10");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_MBC_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MBC_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_MBC_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MBC_Facility.SelectParameters.Clear();
      SqlDataSource_MBC_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_MBC_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "38");
      SqlDataSource_MBC_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_MBC_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_MBC_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_MBC_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_MedicationBundleCompliance_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MedicationBundleCompliance_Form.InsertCommand="INSERT INTO Form_MedicationBundleCompliance_Bundles ( MBC_VisitInformation_Id , MBC_Bundles_ReportNumber , MBC_Bundles_Assessed_LMA , MBC_Bundles_Assessed_CMA , MBC_Bundles_Assessed_RMA , MBC_Bundles_Assessed_ESM , MBC_Bundles_Date , Unit_Id , MBC_Bundles_LMA_SelectAll , MBC_Bundles_LMA_1 , MBC_Bundles_LMA_2 , MBC_Bundles_LMA_3 , MBC_Bundles_LMA_4 , MBC_Bundles_LMA_Cal , MBC_Bundles_LMA_1_NA , MBC_Bundles_LMA_2_NA , MBC_Bundles_LMA_3_NA , MBC_Bundles_LMA_4_NA , MBC_Bundles_CMA_SelectAll , MBC_Bundles_CMA_1 , MBC_Bundles_CMA_2 , MBC_Bundles_CMA_3 , MBC_Bundles_CMA_4 , MBC_Bundles_CMA_5 , MBC_Bundles_CMA_Cal , MBC_Bundles_CMA_1_NA , MBC_Bundles_CMA_2_NA , MBC_Bundles_CMA_3_NA , MBC_Bundles_CMA_4_NA , MBC_Bundles_CMA_5_NA , MBC_Bundles_RMA_SelectAll , MBC_Bundles_RMA_1 , MBC_Bundles_RMA_2 , MBC_Bundles_RMA_3 , MBC_Bundles_RMA_Cal , MBC_Bundles_RMA_1_NA , MBC_Bundles_RMA_2_NA , MBC_Bundles_RMA_3_NA , MBC_Bundles_ESM_SelectAll , MBC_Bundles_ESM_1 , MBC_Bundles_ESM_2 , MBC_Bundles_ESM_3 , MBC_Bundles_ESM_4 , MBC_Bundles_ESM_5 , MBC_Bundles_ESM_Cal , MBC_Bundles_ESM_1_NA , MBC_Bundles_ESM_2_NA , MBC_Bundles_ESM_3_NA , MBC_Bundles_ESM_4_NA , MBC_Bundles_ESM_5_NA , MBC_Bundles_CreatedDate , MBC_Bundles_CreatedBy , MBC_Bundles_ModifiedDate , MBC_Bundles_ModifiedBy , MBC_Bundles_History , MBC_Bundles_IsActive ) VALUES ( @MBC_VisitInformation_Id , @MBC_Bundles_ReportNumber , @MBC_Bundles_Assessed_LMA , @MBC_Bundles_Assessed_CMA , @MBC_Bundles_Assessed_RMA , @MBC_Bundles_Assessed_ESM , @MBC_Bundles_Date , @Unit_Id , @MBC_Bundles_LMA_SelectAll , @MBC_Bundles_LMA_1 , @MBC_Bundles_LMA_2 , @MBC_Bundles_LMA_3 , @MBC_Bundles_LMA_4 , @MBC_Bundles_LMA_Cal , @MBC_Bundles_LMA_1_NA , @MBC_Bundles_LMA_2_NA , @MBC_Bundles_LMA_3_NA , @MBC_Bundles_LMA_4_NA , @MBC_Bundles_CMA_SelectAll , @MBC_Bundles_CMA_1 , @MBC_Bundles_CMA_2 , @MBC_Bundles_CMA_3 , @MBC_Bundles_CMA_4 , @MBC_Bundles_CMA_5 , @MBC_Bundles_CMA_Cal , @MBC_Bundles_CMA_1_NA , @MBC_Bundles_CMA_2_NA , @MBC_Bundles_CMA_3_NA , @MBC_Bundles_CMA_4_NA , @MBC_Bundles_CMA_5_NA , @MBC_Bundles_RMA_SelectAll , @MBC_Bundles_RMA_1 , @MBC_Bundles_RMA_2 , @MBC_Bundles_RMA_3 , @MBC_Bundles_RMA_Cal , @MBC_Bundles_RMA_1_NA , @MBC_Bundles_RMA_2_NA , @MBC_Bundles_RMA_3_NA , @MBC_Bundles_ESM_SelectAll , @MBC_Bundles_ESM_1 , @MBC_Bundles_ESM_2 , @MBC_Bundles_ESM_3 , @MBC_Bundles_ESM_4 , @MBC_Bundles_ESM_5 , @MBC_Bundles_ESM_Cal , @MBC_Bundles_ESM_1_NA , @MBC_Bundles_ESM_2_NA , @MBC_Bundles_ESM_3_NA , @MBC_Bundles_ESM_4_NA , @MBC_Bundles_ESM_5_NA , @MBC_Bundles_CreatedDate , @MBC_Bundles_CreatedBy , @MBC_Bundles_ModifiedDate , @MBC_Bundles_ModifiedBy , @MBC_Bundles_History , @MBC_Bundles_IsActive ); SELECT @MBC_Bundles_Id = SCOPE_IDENTITY()";
      SqlDataSource_MedicationBundleCompliance_Form.SelectCommand="SELECT * FROM Form_MedicationBundleCompliance_Bundles WHERE (MBC_Bundles_Id = @MBC_Bundles_Id)";
      SqlDataSource_MedicationBundleCompliance_Form.UpdateCommand="UPDATE Form_MedicationBundleCompliance_Bundles SET MBC_Bundles_Assessed_LMA = @MBC_Bundles_Assessed_LMA , MBC_Bundles_Assessed_CMA = @MBC_Bundles_Assessed_CMA , MBC_Bundles_Assessed_RMA = @MBC_Bundles_Assessed_RMA , MBC_Bundles_Assessed_ESM = @MBC_Bundles_Assessed_ESM , MBC_Bundles_Date = @MBC_Bundles_Date , Unit_Id = @Unit_Id , MBC_Bundles_LMA_SelectAll = @MBC_Bundles_LMA_SelectAll , MBC_Bundles_LMA_1 = @MBC_Bundles_LMA_1 , MBC_Bundles_LMA_2 = @MBC_Bundles_LMA_2 , MBC_Bundles_LMA_3 = @MBC_Bundles_LMA_3 , MBC_Bundles_LMA_4 = @MBC_Bundles_LMA_4 , MBC_Bundles_LMA_Cal = @MBC_Bundles_LMA_Cal , MBC_Bundles_LMA_1_NA = @MBC_Bundles_LMA_1_NA , MBC_Bundles_LMA_2_NA = @MBC_Bundles_LMA_2_NA , MBC_Bundles_LMA_3_NA = @MBC_Bundles_LMA_3_NA , MBC_Bundles_LMA_4_NA = @MBC_Bundles_LMA_4_NA , MBC_Bundles_CMA_SelectAll = @MBC_Bundles_CMA_SelectAll , MBC_Bundles_CMA_1 = @MBC_Bundles_CMA_1 , MBC_Bundles_CMA_2 = @MBC_Bundles_CMA_2 , MBC_Bundles_CMA_3 = @MBC_Bundles_CMA_3 , MBC_Bundles_CMA_4 = @MBC_Bundles_CMA_4 , MBC_Bundles_CMA_5 = @MBC_Bundles_CMA_5 , MBC_Bundles_CMA_Cal = @MBC_Bundles_CMA_Cal , MBC_Bundles_CMA_1_NA = @MBC_Bundles_CMA_1_NA , MBC_Bundles_CMA_2_NA = @MBC_Bundles_CMA_2_NA , MBC_Bundles_CMA_3_NA = @MBC_Bundles_CMA_3_NA , MBC_Bundles_CMA_4_NA = @MBC_Bundles_CMA_4_NA , MBC_Bundles_CMA_5_NA = @MBC_Bundles_CMA_5_NA , MBC_Bundles_RMA_SelectAll = @MBC_Bundles_RMA_SelectAll , MBC_Bundles_RMA_1 = @MBC_Bundles_RMA_1 , MBC_Bundles_RMA_2 = @MBC_Bundles_RMA_2 , MBC_Bundles_RMA_3 = @MBC_Bundles_RMA_3 , MBC_Bundles_RMA_Cal = @MBC_Bundles_RMA_Cal , MBC_Bundles_RMA_1_NA = @MBC_Bundles_RMA_1_NA , MBC_Bundles_RMA_2_NA = @MBC_Bundles_RMA_2_NA , MBC_Bundles_RMA_3_NA = @MBC_Bundles_RMA_3_NA , MBC_Bundles_ESM_SelectAll = @MBC_Bundles_ESM_SelectAll , MBC_Bundles_ESM_1 = @MBC_Bundles_ESM_1 , MBC_Bundles_ESM_2 = @MBC_Bundles_ESM_2 , MBC_Bundles_ESM_3 = @MBC_Bundles_ESM_3 , MBC_Bundles_ESM_4 = @MBC_Bundles_ESM_4 , MBC_Bundles_ESM_5 = @MBC_Bundles_ESM_5 , MBC_Bundles_ESM_Cal = @MBC_Bundles_ESM_Cal , MBC_Bundles_ESM_1_NA = @MBC_Bundles_ESM_1_NA , MBC_Bundles_ESM_2_NA = @MBC_Bundles_ESM_2_NA , MBC_Bundles_ESM_3_NA = @MBC_Bundles_ESM_3_NA , MBC_Bundles_ESM_4_NA = @MBC_Bundles_ESM_4_NA , MBC_Bundles_ESM_5_NA = @MBC_Bundles_ESM_5_NA , MBC_Bundles_ModifiedDate = @MBC_Bundles_ModifiedDate , MBC_Bundles_ModifiedBy = @MBC_Bundles_ModifiedBy , MBC_Bundles_History = @MBC_Bundles_History , MBC_Bundles_IsActive = @MBC_Bundles_IsActive WHERE MBC_Bundles_Id = @MBC_Bundles_Id";
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Clear();
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_Id", TypeCode.Int32, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_VisitInformation_Id", TypeCode.Int32, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ReportNumber", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_Assessed_LMA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_Assessed_CMA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_Assessed_RMA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_Assessed_ESM", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_Date", TypeCode.DateTime, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_1", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_2", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_3", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_4", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_Cal", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_1_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_2_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_3_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_LMA_4_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_1", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_2", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_3", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_4", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_5", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_Cal", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_1_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_2_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_3_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_4_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CMA_5_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_RMA_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_RMA_1", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_RMA_2", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_RMA_3", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_RMA_Cal", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_RMA_1_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_RMA_2_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_RMA_3_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_1", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_2", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_3", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_4", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_5", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_Cal", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_1_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_2_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_3_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_4_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ESM_5_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_CreatedBy", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_ModifiedBy", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_History", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_MedicationBundleCompliance_Form.InsertParameters.Add("MBC_Bundles_IsActive", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.SelectParameters.Clear();
      SqlDataSource_MedicationBundleCompliance_Form.SelectParameters.Add("MBC_Bundles_Id", TypeCode.Int32, Request.QueryString["MBCBundlesId"]);
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Clear();
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_Assessed_LMA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_Assessed_CMA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_Assessed_RMA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_Assessed_ESM", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_Date", TypeCode.DateTime, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_1", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_2", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_3", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_4", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_Cal", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_1_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_2_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_3_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_LMA_4_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_1", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_2", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_3", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_4", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_5", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_Cal", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_1_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_2_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_3_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_4_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_CMA_5_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_RMA_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_RMA_1", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_RMA_2", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_RMA_3", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_RMA_Cal", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_RMA_1_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_RMA_2_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_RMA_3_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_1", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_2", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_3", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_4", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_5", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_Cal", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_1_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_2_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_3_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_4_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ESM_5_NA", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_ModifiedBy", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_History", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_IsActive", TypeCode.Boolean, "");
      SqlDataSource_MedicationBundleCompliance_Form.UpdateParameters.Add("MBC_Bundles_Id", TypeCode.Int32, "");

      SqlDataSource_MedicationBundleCompliance_InsertUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectParameters.Clear();
      SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectParameters.Add("Form_Id", TypeCode.String, "38");
      SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_MedicationBundleCompliance_InsertUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_MedicationBundleCompliance_EditUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MedicationBundleCompliance_EditUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_MedicationBundleCompliance_EditUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters.Clear();
      SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters.Add("Form_Id", TypeCode.String, "38");
      SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_MedicationBundleCompliance_Bundles.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_MedicationBundleCompliance_Bundles.SelectCommand = "spForm_Get_MedicationBundleCompliance_Bundles";
      SqlDataSource_MedicationBundleCompliance_Bundles.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_MedicationBundleCompliance_Bundles.CancelSelectOnNullParameter = false;
      SqlDataSource_MedicationBundleCompliance_Bundles.SelectParameters.Clear();
      SqlDataSource_MedicationBundleCompliance_Bundles.SelectParameters.Add("MBC_VisitInformation_Id", TypeCode.String, Request.QueryString["MBCVisitInformationId"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("38")).ToString(), CultureInfo.CurrentCulture);
      Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("38").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
      Label_VisitInfoHeading.Text = Convert.ToString("Visit Information", CultureInfo.CurrentCulture);
      Label_CurrentBundleHeading.Text = Convert.ToString("Bundles", CultureInfo.CurrentCulture);
      Label_BundleHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("38").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
    }

    private void SetFormQueryString()
    {
      if (Request.QueryString["s_Facility_Id"] == null && Request.QueryString["s_MBC_VisitInformation_VisitNumber"] == null && Request.QueryString["MBCVisitInformationId"] == null)
      {
        DropDownList_Facility.SelectedValue = "";
        TextBox_PatientVisitNumber.Text = "";
      }
      else
      {
        if (Request.QueryString["MBCVisitInformationId"] == null)
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_MBC_VisitInformation_VisitNumber"];
        }
        else
        {
          Session["FacilityId"] = "";
          Session["MBCVisitInformationVisitNumber"] = "";
          string SQLStringVisitInfo = "SELECT Facility_Id , MBC_VisitInformation_VisitNumber FROM Form_MedicationBundleCompliance_VisitInformation WHERE MBC_VisitInformation_Id = @MBC_VisitInformation_Id";
          using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
          {
            SqlCommand_VisitInfo.Parameters.AddWithValue("@MBC_VisitInformation_Id", Request.QueryString["MBCVisitInformationId"]);
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
                  Session["MBCVisitInformationVisitNumber"] = DataRow_Row["MBC_VisitInformation_VisitNumber"];
                }
              }
            }
          }

          DropDownList_Facility.SelectedValue = Session["FacilityId"].ToString();
          TextBox_PatientVisitNumber.Text = Session["MBCVisitInformationVisitNumber"].ToString();

          Session.Remove("FacilityId");
          Session.Remove("MBCVisitInformationVisitNumber");
        }
      }
    }

    protected void TableVisible()
    {
      if (TableVisitInfo.Visible == true)
      {
        TableVisitInfoVisible();
      }

      if (TableCurrentBundle.Visible == true)
      {
        TableCurrentBundleVisible();
      }
    }


    private void VisitData()
    {
      DataTable DataTable_VisitData;
      using (DataTable_VisitData = new DataTable())
      {
        DataTable_VisitData.Locale = CultureInfo.CurrentCulture;
        //DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_MBC_VisitInformation_VisitNumber"]).Copy();
        DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_MBC_VisitInformation_VisitNumber"]).Copy();
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

            string MBCVisitInformationId = "";
            string SQLStringVisitInfo = "SELECT MBC_VisitInformation_Id FROM Form_MedicationBundleCompliance_VisitInformation WHERE Facility_Id = @Facility_Id AND MBC_VisitInformation_VisitNumber = @MBC_VisitInformation_VisitNumber";
            using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
            {
              SqlCommand_VisitInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
              SqlCommand_VisitInfo.Parameters.AddWithValue("@MBC_VisitInformation_VisitNumber", Request.QueryString["s_MBC_VisitInformation_VisitNumber"]);
              DataTable DataTable_VisitInfo;
              using (DataTable_VisitInfo = new DataTable())
              {
                DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
                DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
                if (DataTable_VisitInfo.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row1 in DataTable_VisitInfo.Rows)
                  {
                    MBCVisitInformationId = DataRow_Row1["MBC_VisitInformation_Id"].ToString();
                  }
                }
              }
            }

            if (string.IsNullOrEmpty(MBCVisitInformationId))
            {
              string SQLStringInsertVisitInformation = "INSERT INTO Form_MedicationBundleCompliance_VisitInformation ( Facility_Id , MBC_VisitInformation_VisitNumber , PatientInformation_Id , MBC_VisitInformation_PatientName , MBC_VisitInformation_PatientAge , MBC_VisitInformation_DateOfAdmission , MBC_VisitInformation_DateOfDischarge , MBC_VisitInformation_Archived ) VALUES ( @Facility_Id , @MBC_VisitInformation_VisitNumber , @PatientInformation_Id , @MBC_VisitInformation_PatientName , @MBC_VisitInformation_PatientAge , @MBC_VisitInformation_DateOfAdmission , @MBC_VisitInformation_DateOfDischarge , @MBC_VisitInformation_Archived ); SELECT SCOPE_IDENTITY()";
              using (SqlCommand SqlCommand_InsertVisitInformation = new SqlCommand(SQLStringInsertVisitInformation))
              {
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_VisitNumber", Request.QueryString["s_MBC_VisitInformation_VisitNumber"]);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", DBNull.Value);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_PatientName", PatientSurnameName);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_PatientAge", PatientAge);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_DateOfAdmission", DateOfAdmission);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_DateOfDischarge", DateOfDischarge);
                SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_Archived", 0);
                MBCVisitInformationId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertVisitInformation);
              }
            }
            else
            {
              string SQLStringUpdateVisitInformation = "UPDATE Form_MedicationBundleCompliance_VisitInformation SET PatientInformation_Id = @PatientInformation_Id , MBC_VisitInformation_PatientName = @MBC_VisitInformation_PatientName , MBC_VisitInformation_PatientAge  = @MBC_VisitInformation_PatientAge , MBC_VisitInformation_DateOfAdmission  = @MBC_VisitInformation_DateOfAdmission , MBC_VisitInformation_DateOfDischarge  = @MBC_VisitInformation_DateOfDischarge WHERE Facility_Id = @Facility_Id AND MBC_VisitInformation_VisitNumber = @MBC_VisitInformation_VisitNumber";
              using (SqlCommand SqlCommand_UpdateVisitInformation = new SqlCommand(SQLStringUpdateVisitInformation))
              {
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", DBNull.Value);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_PatientName", PatientSurnameName);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_PatientAge", PatientAge);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_DateOfAdmission", DateOfAdmission);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_DateOfDischarge", DateOfDischarge);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_VisitNumber", Request.QueryString["s_MBC_VisitInformation_VisitNumber"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateVisitInformation);
              }
            }

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_MedicationBundleCompliance", "Form_MedicationBundleCompliance.aspx?MBCVisitInformationId=" + MBCVisitInformationId), false);
          }
        }
      }

      ////String PatientInformationId = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PatientInformationId(Request.QueryString["s_Facility_Id"], Request.QueryString["s_MBC_VisitInformation_VisitNumber"]);
      //String PatientInformationId = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_PatientInformationId(Request.QueryString["s_Facility_Id"], Request.QueryString["s_MBC_VisitInformation_VisitNumber"]);
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
      //    //DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_MBC_VisitInformation_VisitNumber"]).Copy();
      //    DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_MBC_VisitInformation_VisitNumber"]).Copy();
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

      //        String MBCVisitInformationId = "";
      //        String SQLStringVisitInfo = "SELECT MBC_VisitInformation_Id FROM Form_MedicationBundleCompliance_VisitInformation WHERE Facility_Id = @Facility_Id AND MBC_VisitInformation_VisitNumber = @MBC_VisitInformation_VisitNumber";
      //        using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      //        {
      //          SqlCommand_VisitInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
      //          SqlCommand_VisitInfo.Parameters.AddWithValue("@MBC_VisitInformation_VisitNumber", Request.QueryString["s_MBC_VisitInformation_VisitNumber"]);
      //          DataTable DataTable_VisitInfo;
      //          using (DataTable_VisitInfo = new DataTable())
      //          {
      //            DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
      //            DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
      //            if (DataTable_VisitInfo.Rows.Count > 0)
      //            {
      //              foreach (DataRow DataRow_Row1 in DataTable_VisitInfo.Rows)
      //              {
      //                MBCVisitInformationId = DataRow_Row1["MBC_VisitInformation_Id"].ToString();
      //              }
      //            }
      //          }
      //        }

      //        if (String.IsNullOrEmpty(MBCVisitInformationId))
      //        {
      //          String SQLStringInsertVisitInformation = "INSERT INTO Form_MedicationBundleCompliance_VisitInformation ( Facility_Id , MBC_VisitInformation_VisitNumber , PatientInformation_Id , MBC_VisitInformation_PatientAge , MBC_VisitInformation_DateOfAdmission , MBC_VisitInformation_DateOfDischarge , MBC_VisitInformation_Archived ) VALUES ( @Facility_Id , @MBC_VisitInformation_VisitNumber , @PatientInformation_Id , @MBC_VisitInformation_PatientAge , @MBC_VisitInformation_DateOfAdmission , @MBC_VisitInformation_DateOfDischarge , @MBC_VisitInformation_Archived ); SELECT SCOPE_IDENTITY()";
      //          using (SqlCommand SqlCommand_InsertVisitInformation = new SqlCommand(SQLStringInsertVisitInformation))
      //          {
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_VisitNumber", Request.QueryString["s_MBC_VisitInformation_VisitNumber"]);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_PatientAge", PatientAge);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_DateOfAdmission", DateOfAdmission);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_DateOfDischarge", DateOfDischarge);
      //            SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_Archived", 0);
      //            MBCVisitInformationId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertVisitInformation);
      //          }
      //        }
      //        else
      //        {
      //          String SQLStringUpdateVisitInformation = "UPDATE Form_MedicationBundleCompliance_VisitInformation SET PatientInformation_Id = @PatientInformation_Id , MBC_VisitInformation_PatientAge  = @MBC_VisitInformation_PatientAge , MBC_VisitInformation_DateOfAdmission  = @MBC_VisitInformation_DateOfAdmission , MBC_VisitInformation_DateOfDischarge  = @MBC_VisitInformation_DateOfDischarge WHERE Facility_Id = @Facility_Id AND MBC_VisitInformation_VisitNumber = @MBC_VisitInformation_VisitNumber";
      //          using (SqlCommand SqlCommand_UpdateVisitInformation = new SqlCommand(SQLStringUpdateVisitInformation))
      //          {
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_PatientAge", PatientAge);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_DateOfAdmission", DateOfAdmission);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_DateOfDischarge", DateOfDischarge);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
      //            SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@MBC_VisitInformation_VisitNumber", Request.QueryString["s_MBC_VisitInformation_VisitNumber"]);
      //            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateVisitInformation);
      //          }
      //        }

      //        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_MedicationBundleCompliance", "Form_MedicationBundleCompliance.aspx?MBCVisitInformationId=" + MBCVisitInformationId), false);
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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('38')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_MedicationBundleCompliance_VisitInformation WHERE MBC_VisitInformation_Id = @MBC_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@MBC_VisitInformation_Id", Request.QueryString["MBCVisitInformationId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '156'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '157'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '158'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '159'");
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

      string SQLStringFacility = "SELECT Facility_Id FROM Form_MedicationBundleCompliance_VisitInformation WHERE MBC_VisitInformation_Id = @MBC_VisitInformation_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@MBC_VisitInformation_Id", Request.QueryString["MBCVisitInformationId"]);
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

    private class FromDataBase_FormViewUpdate
    {
      public string ViewUpdate { get; set; }
    }

    private FromDataBase_FormViewUpdate GetFormViewUpdate()
    {
      FromDataBase_FormViewUpdate FromDataBase_FormViewUpdate_New = new FromDataBase_FormViewUpdate();

      string SQLStringFormViewUpdate = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 38),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,MBC_Bundles_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 38),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,MBC_Bundles_Date)+1,0))) < GETDATE() THEN 'No' END AS ViewUpdate FROM Form_MedicationBundleCompliance_Bundles WHERE MBC_Bundles_Id = @MBC_Bundles_Id";
      using (SqlCommand SqlCommand_FormViewUpdate = new SqlCommand(SQLStringFormViewUpdate))
      {
        SqlCommand_FormViewUpdate.Parameters.AddWithValue("@MBC_Bundles_Id", Request.QueryString["MBCBundlesId"]);
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Form", "Form_MedicationBundleCompliance.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect("Form_MedicationBundleCompliance.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_MBC_VisitInformation_VisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "", false);
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
      string SearchField3 = Request.QueryString["Search_MedicationBundleCompliancePatientVisitNumber"];
      string SearchField4 = Request.QueryString["Search_MedicationBundleCompliancePatientName"];
      string SearchField5 = Request.QueryString["Search_MedicationBundleComplianceReportNumber"];

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
        SearchField3 = "s_MedicationBundleCompliance_PatientVisitNumber=" + Request.QueryString["Search_MedicationBundleCompliancePatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_MedicationBundleCompliance_PatientName=" + Request.QueryString["Search_MedicationBundleCompliancePatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_MedicationBundleCompliance_ReportNumber=" + Request.QueryString["Search_MedicationBundleComplianceReportNumber"] + "&";
      }

      string FinalURL = "Form_MedicationBundleCompliance_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --VisitInfo--//
    private void TableVisitInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["MBCVisitInformationVisitNumber"] = "";
      Session["MBCVisitInformationPatientName"] = "";
      Session["MBCVisitInformationPatientAge"] = "";
      Session["MBCVisitInformationDateOfAdmission"] = "";
      Session["MBCVisitInformationDateOfDischarge"] = "";
      string SQLStringVisitInfo = "SELECT Facility_FacilityDisplayName , MBC_VisitInformation_VisitNumber , MBC_VisitInformation_PatientName , MBC_VisitInformation_PatientAge , MBC_VisitInformation_DateOfAdmission , MBC_VisitInformation_DateOfDischarge FROM vForm_MedicationBundleCompliance_VisitInformation WHERE MBC_VisitInformation_Id = @MBC_VisitInformation_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@MBC_VisitInformation_Id", Request.QueryString["MBCVisitInformationId"]);
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
              Session["MBCVisitInformationVisitNumber"] = DataRow_Row["MBC_VisitInformation_VisitNumber"];
              Session["MBCVisitInformationPatientName"] = DataRow_Row["MBC_VisitInformation_PatientName"];
              Session["MBCVisitInformationPatientAge"] = DataRow_Row["MBC_VisitInformation_PatientAge"];
              Session["MBCVisitInformationDateOfAdmission"] = DataRow_Row["MBC_VisitInformation_DateOfAdmission"];
              Session["MBCVisitInformationDateOfDischarge"] = DataRow_Row["MBC_VisitInformation_DateOfDischarge"];
            }
          }
        }
      }

      Label_VIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_VIVisitNumber.Text = Session["MBCVisitInformationVisitNumber"].ToString();
      Label_VIName.Text = Session["MBCVisitInformationPatientName"].ToString();
      Label_VIAge.Text = Session["MBCVisitInformationPatientAge"].ToString();
      Label_VIDateAdmission.Text = Session["MBCVisitInformationDateOfAdmission"].ToString();
      Label_VIDateDischarge.Text = Session["MBCVisitInformationDateOfDischarge"].ToString();

      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("MBCVisitInformationVisitNumber");
      Session.Remove("MBCVisitInformationPatientName");
      Session.Remove("MBCVisitInformationPatientAge");
      Session.Remove("MBCVisitInformationDateOfAdmission");
      Session.Remove("MBCVisitInformationDateOfDischarge");
    }
    //---END--- --VisitInfo--//


    //--START-- --TableCurrentBundle--//
    protected void SetCurrentBundleVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["MBCBundlesId"]))
      {
        SetCurrentBundleVisibility_Insert();
      }
      else
      {
        SetCurrentBundleVisibility_Edit();
      }
    }

    protected void SetCurrentBundleVisibility_Insert()
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
        FormView_MedicationBundleCompliance_Form.ChangeMode(FormViewMode.Insert);
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_MedicationBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_MedicationBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetCurrentBundleVisibility_Edit()
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
        FormView_MedicationBundleCompliance_Form.ChangeMode(FormViewMode.Edit);
      }

      if (Security == "1" && (SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (ViewUpdate == "Yes")
        {
          FormView_MedicationBundleCompliance_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_MedicationBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_MedicationBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_MedicationBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentBundleVisible()
    {
      if (FormView_MedicationBundleCompliance_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedLMA")).Attributes.Add("OnClick", "Validation_Form('AssessedLMA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedCMA")).Attributes.Add("OnClick", "Validation_Form('AssessedCMA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedRMA")).Attributes.Add("OnClick", "Validation_Form('AssessedRMA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedESM")).Attributes.Add("OnClick", "Validation_Form('AssessedESM');Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMASelectAll")).Attributes.Add("OnClick", "Validation_Form('LMASelectAll');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA1")).Attributes.Add("OnClick", "Validation_Form('LMA1');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA1NA")).Attributes.Add("OnClick", "Validation_Form('LMA1NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA2")).Attributes.Add("OnClick", "Validation_Form('LMA2');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA2NA")).Attributes.Add("OnClick", "Validation_Form('LMA2NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA3")).Attributes.Add("OnClick", "Validation_Form('LMA3');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA3NA")).Attributes.Add("OnClick", "Validation_Form('LMA3NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA4")).Attributes.Add("OnClick", "Validation_Form('LMA4');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA4NA")).Attributes.Add("OnClick", "Validation_Form('LMA4NA');Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMASelectAll")).Attributes.Add("OnClick", "Validation_Form('CMASelectAll');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA1")).Attributes.Add("OnClick", "Validation_Form('CMA1');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA1NA")).Attributes.Add("OnClick", "Validation_Form('CMA1NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA2")).Attributes.Add("OnClick", "Validation_Form('CMA2');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA2NA")).Attributes.Add("OnClick", "Validation_Form('CMA2NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA3")).Attributes.Add("OnClick", "Validation_Form('CMA3');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA3NA")).Attributes.Add("OnClick", "Validation_Form('CMA3NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA4")).Attributes.Add("OnClick", "Validation_Form('CMA4');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA4NA")).Attributes.Add("OnClick", "Validation_Form('CMA4NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA5")).Attributes.Add("OnClick", "Validation_Form('CMA5');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA5NA")).Attributes.Add("OnClick", "Validation_Form('CMA5NA');Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertRMASelectAll")).Attributes.Add("OnClick", "Validation_Form('RMASelectAll');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertRMA1")).Attributes.Add("OnClick", "Validation_Form('RMA1');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertRMA1NA")).Attributes.Add("OnClick", "Validation_Form('RMA1NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertRMA2")).Attributes.Add("OnClick", "Validation_Form('RMA2');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertRMA2NA")).Attributes.Add("OnClick", "Validation_Form('RMA2NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertRMA3")).Attributes.Add("OnClick", "Validation_Form('RMA3');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertRMA3NA")).Attributes.Add("OnClick", "Validation_Form('RMA3NA');Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESMSelectAll")).Attributes.Add("OnClick", "Validation_Form('ESMSelectAll');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM1")).Attributes.Add("OnClick", "Validation_Form('ESM1');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM1NA")).Attributes.Add("OnClick", "Validation_Form('ESM1NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM2")).Attributes.Add("OnClick", "Validation_Form('ESM2');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM2NA")).Attributes.Add("OnClick", "Validation_Form('ESM2NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM3")).Attributes.Add("OnClick", "Validation_Form('ESM3');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM3NA")).Attributes.Add("OnClick", "Validation_Form('ESM3NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM4")).Attributes.Add("OnClick", "Validation_Form('ESM4');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM4NA")).Attributes.Add("OnClick", "Validation_Form('ESM4NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM5")).Attributes.Add("OnClick", "Validation_Form('ESM5');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM5NA")).Attributes.Add("OnClick", "Validation_Form('ESM5NA');Calculation_Form();");
      }

      if (FormView_MedicationBundleCompliance_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_EditUnit")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedLMA")).Attributes.Add("OnClick", "Validation_Form('AssessedLMA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedCMA")).Attributes.Add("OnClick", "Validation_Form('AssessedCMA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedRMA")).Attributes.Add("OnClick", "Validation_Form('AssessedRMA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedESM")).Attributes.Add("OnClick", "Validation_Form('AssessedESM');Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMASelectAll")).Attributes.Add("OnClick", "Validation_Form('LMASelectAll');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA1")).Attributes.Add("OnClick", "Validation_Form('LMA1');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA1NA")).Attributes.Add("OnClick", "Validation_Form('LMA1NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA2")).Attributes.Add("OnClick", "Validation_Form('LMA2');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA2NA")).Attributes.Add("OnClick", "Validation_Form('LMA2NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA3")).Attributes.Add("OnClick", "Validation_Form('LMA3');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA3NA")).Attributes.Add("OnClick", "Validation_Form('LMA3NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA4")).Attributes.Add("OnClick", "Validation_Form('LMA4');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA4NA")).Attributes.Add("OnClick", "Validation_Form('LMA4NA');Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMASelectAll")).Attributes.Add("OnClick", "Validation_Form('CMASelectAll');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA1")).Attributes.Add("OnClick", "Validation_Form('CMA1');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA1NA")).Attributes.Add("OnClick", "Validation_Form('CMA1NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA2")).Attributes.Add("OnClick", "Validation_Form('CMA2');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA2NA")).Attributes.Add("OnClick", "Validation_Form('CMA2NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA3")).Attributes.Add("OnClick", "Validation_Form('CMA3');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA3NA")).Attributes.Add("OnClick", "Validation_Form('CMA3NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA4")).Attributes.Add("OnClick", "Validation_Form('CMA4');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA4NA")).Attributes.Add("OnClick", "Validation_Form('CMA4NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA5")).Attributes.Add("OnClick", "Validation_Form('CMA5');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA5NA")).Attributes.Add("OnClick", "Validation_Form('CMA5NA');Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditRMASelectAll")).Attributes.Add("OnClick", "Validation_Form('RMASelectAll');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditRMA1")).Attributes.Add("OnClick", "Validation_Form('RMA1');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditRMA1NA")).Attributes.Add("OnClick", "Validation_Form('RMA1NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditRMA2")).Attributes.Add("OnClick", "Validation_Form('RMA2');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditRMA2NA")).Attributes.Add("OnClick", "Validation_Form('RMA2NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditRMA3")).Attributes.Add("OnClick", "Validation_Form('RMA3');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditRMA3NA")).Attributes.Add("OnClick", "Validation_Form('RMA3NA');Calculation_Form();");

        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESMSelectAll")).Attributes.Add("OnClick", "Validation_Form('ESMSelectAll');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM1")).Attributes.Add("OnClick", "Validation_Form('ESM1');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM1NA")).Attributes.Add("OnClick", "Validation_Form('ESM1NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM2")).Attributes.Add("OnClick", "Validation_Form('ESM2');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM2NA")).Attributes.Add("OnClick", "Validation_Form('ESM2NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM3")).Attributes.Add("OnClick", "Validation_Form('ESM3');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM3NA")).Attributes.Add("OnClick", "Validation_Form('ESM3NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM4")).Attributes.Add("OnClick", "Validation_Form('ESM4');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM4NA")).Attributes.Add("OnClick", "Validation_Form('ESM4NA');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM5")).Attributes.Add("OnClick", "Validation_Form('ESM5');Calculation_Form();");
        ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM5NA")).Attributes.Add("OnClick", "Validation_Form('ESM5NA');Calculation_Form();");
      }
    }


    protected void FormView_MedicationBundleCompliance_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ToolkitScriptManager_MedicationBundleCompliance.SetFocus(UpdatePanel_MedicationBundleCompliance);
          ((Label)FormView_MedicationBundleCompliance_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_MedicationBundleCompliance_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
          Session["FacilityId"] = FromDataBase_FacilityId_Current.FacilityId;

          Session["MBC_Bundles_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Session["FacilityId"].ToString(), "38");

          SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["Unit_Id"].DefaultValue = ((DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).SelectedValue;

          SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_VisitInformation_Id"].DefaultValue = Request.QueryString["MBCVisitInformationId"];
          SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_ReportNumber"].DefaultValue = Session["MBC_Bundles_ReportNumber"].ToString();
          SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_History"].DefaultValue = "";
          SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_IsActive"].DefaultValue = "true";


          if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedLMA")).Checked == false)
          {
            SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_LMA_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal LMA_Total;
            LMA_Total = 0;
            Decimal LMA_Selected;
            LMA_Selected = 0;

            for (int a = 1; a <= 4; a++)
            {
              if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA" + a + "")).Checked == true)
              {
                LMA_Total = LMA_Total + 1;
              }
              else
              {
                LMA_Total = LMA_Total + 0;
              }

              if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertLMA" + a + "NA")).Checked == false)
              {
                LMA_Selected = LMA_Selected + 1;
              }
              else
              {
                LMA_Selected = LMA_Selected + 0;
              }
            }

            Decimal LMA_Cal = (LMA_Total * 100 / LMA_Selected);
            SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_LMA_Cal"].DefaultValue = Decimal.Round(LMA_Cal, MidpointRounding.ToEven) + " %";
          }


          if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedCMA")).Checked == false)
          {
            SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_CMA_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal CMA_Total;
            CMA_Total = 0;
            Decimal CMA_Selected;
            CMA_Selected = 0;

            for (int a = 1; a <= 5; a++)
            {
              if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA" + a + "")).Checked == true)
              {
                CMA_Total = CMA_Total + 1;
              }
              else
              {
                CMA_Total = CMA_Total + 0;
              }

              if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertCMA" + a + "NA")).Checked == false)
              {
                CMA_Selected = CMA_Selected + 1;
              }
              else
              {
                CMA_Selected = CMA_Selected + 0;
              }
            }

            Decimal CMA_Cal = (CMA_Total * 100 / CMA_Selected);
            SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_CMA_Cal"].DefaultValue = Decimal.Round(CMA_Cal, MidpointRounding.ToEven) + " %";
          }


          if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedRMA")).Checked == false)
          {
            SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_RMA_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal RMA_Total;
            RMA_Total = 0;
            Decimal RMA_Selected;
            RMA_Selected = 0;

            for (int a = 1; a <= 3; a++)
            {
              if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertRMA" + a + "")).Checked == true)
              {
                RMA_Total = RMA_Total + 1;
              }
              else
              {
                RMA_Total = RMA_Total + 0;
              }

              if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertRMA" + a + "NA")).Checked == false)
              {
                RMA_Selected = RMA_Selected + 1;
              }
              else
              {
                RMA_Selected = RMA_Selected + 0;
              }
            }

            Decimal RMA_Cal = (RMA_Total * 100 / RMA_Selected);
            SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_RMA_Cal"].DefaultValue = Decimal.Round(RMA_Cal, MidpointRounding.ToEven) + " %";
          }


          if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedESM")).Checked == false)
          {
            SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_ESM_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal ESM_Total;
            ESM_Total = 0;
            Decimal ESM_Selected;
            ESM_Selected = 0;

            for (int a = 1; a <= 5; a++)
            {
              if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM" + a + "")).Checked == true)
              {
                ESM_Total = ESM_Total + 1;
              }
              else
              {
                ESM_Total = ESM_Total + 0;
              }

              if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertESM" + a + "NA")).Checked == false)
              {
                ESM_Selected = ESM_Selected + 1;
              }
              else
              {
                ESM_Selected = ESM_Selected + 0;
              }
            }

            Decimal ESM_Cal = (ESM_Total * 100 / ESM_Selected);
            SqlDataSource_MedicationBundleCompliance_Form.InsertParameters["MBC_Bundles_ESM_Cal"].DefaultValue = Decimal.Round(ESM_Cal, MidpointRounding.ToEven) + " %";
          }

          Session["MBC_Bundles_ReportNumber"] = "";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_InsertDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedLMA")).Checked == false && ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedCMA")).Checked == false && ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedRMA")).Checked == false && ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_InsertAssessedESM")).Checked == false)
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateDate = ((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_InsertDate")).Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Evaluation Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_InsertDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Evaluation dates allowed<br />";
          }
          else
          {
            Session["ValidCapture"] = "";
            Session["CutOffDay"] = "";
            string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 38),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@MBC_Bundles_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 38),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@MBC_Bundles_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 38) AS CutOffDay";
            using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
            {
              SqlCommand_ValidCapture.Parameters.AddWithValue("@MBC_Bundles_Date", PickedDate);
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
              InvalidFormMessage = InvalidFormMessage + "Evaluation date is not valid. Forms may be captured between the 1st of a calendar month until the " + Session["CutOffDay"].ToString() + "th of the following month <br />";
            }

            Session["ValidCapture"] = "";
            Session["CutOffDay"] = "";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_MedicationBundleCompliance_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["MBC_Bundles_Id"] = e.Command.Parameters["@MBC_Bundles_Id"].Value;
        Session["MBC_Bundles_ReportNumber"] = e.Command.Parameters["@MBC_Bundles_ReportNumber"].Value;
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_MedicationBundleCompliance&ReportNumber=" + Session["MBC_Bundles_ReportNumber"].ToString() + ""), false);
      }
    }


    protected void FormView_MedicationBundleCompliance_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDMBCBundlesModifiedDate"] = e.OldValues["MBC_Bundles_ModifiedDate"];
        object OLDMBCBundlesModifiedDate = Session["OLDMBCBundlesModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDMBCBundlesModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareMedicationBundleCompliance = (DataView)SqlDataSource_MedicationBundleCompliance_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareMedicationBundleCompliance = DataView_CompareMedicationBundleCompliance[0];
        Session["DBMBCBundlesModifiedDate"] = Convert.ToString(DataRowView_CompareMedicationBundleCompliance["MBC_Bundles_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBMBCBundlesModifiedBy"] = Convert.ToString(DataRowView_CompareMedicationBundleCompliance["MBC_Bundles_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBMBCBundlesModifiedDate = Session["DBMBCBundlesModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBMBCBundlesModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_MedicationBundleCompliance.SetFocus(UpdatePanel_MedicationBundleCompliance);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBMBCBundlesModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_MedicationBundleCompliance_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_MedicationBundleCompliance_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ToolkitScriptManager_MedicationBundleCompliance.SetFocus(UpdatePanel_MedicationBundleCompliance);
            ((Label)FormView_MedicationBundleCompliance_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_MedicationBundleCompliance_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["MBC_Bundles_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["MBC_Bundles_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];
            e.NewValues["Unit_Id"] = ((DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_EditUnit")).SelectedValue;

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_MedicationBundleCompliance_Bundles", "MBC_Bundles_Id = " + Request.QueryString["MBCBundlesId"]);

            DataView DataView_MedicationBundleCompliance = (DataView)SqlDataSource_MedicationBundleCompliance_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_MedicationBundleCompliance = DataView_MedicationBundleCompliance[0];
            Session["MBCBundlesHistory"] = Convert.ToString(DataRowView_MedicationBundleCompliance["MBC_Bundles_History"], CultureInfo.CurrentCulture);

            Session["MBCBundlesHistory"] = Session["History"].ToString() + Session["MBCBundlesHistory"].ToString();
            e.NewValues["MBC_Bundles_History"] = Session["MBCBundlesHistory"].ToString();

            Session["MBCBundlesHistory"] = "";
            Session["History"] = "";

            if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedLMA")).Checked == false)
            {
              e.NewValues["MBC_Bundles_LMA_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal LMA_Total;
              LMA_Total = 0;
              Decimal LMA_Selected;
              LMA_Selected = 0;

              for (int a = 1; a <= 4; a++)
              {
                if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA" + a + "")).Checked == true)
                {
                  LMA_Total = LMA_Total + 1;
                }
                else
                {
                  LMA_Total = LMA_Total + 0;
                }

                if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditLMA" + a + "NA")).Checked == false)
                {
                  LMA_Selected = LMA_Selected + 1;
                }
                else
                {
                  LMA_Selected = LMA_Selected + 0;
                }
              }

              Decimal LMA_Cal = (LMA_Total * 100 / LMA_Selected);
              e.NewValues["MBC_Bundles_LMA_Cal"] = Decimal.Round(LMA_Cal, MidpointRounding.ToEven) + " %";
            }


            if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedCMA")).Checked == false)
            {
              e.NewValues["MBC_Bundles_CMA_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal CMA_Total;
              CMA_Total = 0;
              Decimal CMA_Selected;
              CMA_Selected = 0;

              for (int a = 1; a <= 5; a++)
              {
                if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA" + a + "")).Checked == true)
                {
                  CMA_Total = CMA_Total + 1;
                }
                else
                {
                  CMA_Total = CMA_Total + 0;
                }

                if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditCMA" + a + "NA")).Checked == false)
                {
                  CMA_Selected = CMA_Selected + 1;
                }
                else
                {
                  CMA_Selected = CMA_Selected + 0;
                }
              }

              Decimal CMA_Cal = (CMA_Total * 100 / CMA_Selected);
              e.NewValues["MBC_Bundles_CMA_Cal"] = Decimal.Round(CMA_Cal, MidpointRounding.ToEven) + " %";
            }


            if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedRMA")).Checked == false)
            {
              e.NewValues["MBC_Bundles_RMA_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal RMA_Total;
              RMA_Total = 0;
              Decimal RMA_Selected;
              RMA_Selected = 0;

              for (int a = 1; a <= 3; a++)
              {
                if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditRMA" + a + "")).Checked == true)
                {
                  RMA_Total = RMA_Total + 1;
                }
                else
                {
                  RMA_Total = RMA_Total + 0;
                }

                if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditRMA" + a + "NA")).Checked == false)
                {
                  RMA_Selected = RMA_Selected + 1;
                }
                else
                {
                  RMA_Selected = RMA_Selected + 0;
                }
              }

              Decimal RMA_Cal = (RMA_Total * 100 / RMA_Selected);
              e.NewValues["MBC_Bundles_RMA_Cal"] = Decimal.Round(RMA_Cal, MidpointRounding.ToEven) + " %";
            }


            if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedESM")).Checked == false)
            {
              e.NewValues["MBC_Bundles_ESM_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal ESM_Total;
              ESM_Total = 0;
              Decimal ESM_Selected;
              ESM_Selected = 0;

              for (int a = 1; a <= 5; a++)
              {
                if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM" + a + "")).Checked == true)
                {
                  ESM_Total = ESM_Total + 1;
                }
                else
                {
                  ESM_Total = ESM_Total + 0;
                }

                if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditESM" + a + "NA")).Checked == false)
                {
                  ESM_Selected = ESM_Selected + 1;
                }
                else
                {
                  ESM_Selected = ESM_Selected + 0;
                }
              }

              Decimal ESM_Cal = (ESM_Total * 100 / ESM_Selected);
              e.NewValues["MBC_Bundles_ESM_Cal"] = Decimal.Round(ESM_Cal, MidpointRounding.ToEven) + " %";
            }
          }
        }

        Session["OLDMBCBundlesModifiedDate"] = "";
        Session["DBMBCBundlesModifiedDate"] = "";
        Session["DBMBCBundlesModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_EditUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_EditDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedLMA")).Checked == false && ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedCMA")).Checked == false && ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedRMA")).Checked == false && ((CheckBox)FormView_MedicationBundleCompliance_Form.FindControl("CheckBox_EditAssessedESM")).Checked == false)
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateDate = ((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_EditDate")).Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Evaluation Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_MedicationBundleCompliance_Form.FindControl("TextBox_EditDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Evaluation dates allowed<br />";
          }
          else
          {
            DateTime PreviousDate = Convert.ToDateTime(((HiddenField)FormView_MedicationBundleCompliance_Form.FindControl("HiddenField_EditDate")).Value, CultureInfo.CurrentCulture);

            if (PickedDate.CompareTo(PreviousDate) != 0)
            {
              Session["ValidCapture"] = "";
              Session["CutOffDay"] = "";
              string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 38),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@MBC_Bundles_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 38),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@MBC_Bundles_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 38) AS CutOffDay";
              using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
              {
                SqlCommand_ValidCapture.Parameters.AddWithValue("@MBC_Bundles_Date", PickedDate);
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
                InvalidFormMessage = InvalidFormMessage + "Evaluation date is not valid. Forms may be captured between the 1st of a calendar month until the " + Session["CutOffDay"].ToString() + "th of the following month <br />";
              }

              Session["ValidCapture"] = "";
              Session["CutOffDay"] = "";
            }
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_MedicationBundleCompliance_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Form", "Form_MedicationBundleCompliance.aspx?MBCVisitInformationId=" + Request.QueryString["MBCVisitInformationId"] + ""), false);
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_MedicationBundleCompliance, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Print", "InfoQuest_Print.aspx?PrintPage=Form_MedicationBundleCompliance&PrintValue=" + Request.QueryString["MBCBundlesId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_MedicationBundleCompliance, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_MedicationBundleCompliance, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Email", "InfoQuest_Email.aspx?EmailPage=Form_MedicationBundleCompliance&EmailValue=" + Request.QueryString["MBCBundlesId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_MedicationBundleCompliance, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }


    protected void FormView_MedicationBundleCompliance_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["MBCBundlesId"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Form", "Form_MedicationBundleCompliance.aspx?MBCVisitInformationId=" + Request.QueryString["MBCVisitInformationId"] + ""), false);
          }
        }
      }
    }

    protected void FormView_MedicationBundleCompliance_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_MedicationBundleCompliance_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_MedicationBundleCompliance_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      if (Request.QueryString["MBCBundlesId"] != null)
      {
        FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
        string FacilityId = FromDataBase_FacilityId_Current.FacilityId;

        DropDownList DropDownList_EditUnit = (DropDownList)FormView_MedicationBundleCompliance_Form.FindControl("DropDownList_EditUnit");
        DataView DataView_UnitId = (DataView)SqlDataSource_MedicationBundleCompliance_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_UnitId = DataView_UnitId[0];
        DropDownList_EditUnit.SelectedValue = Convert.ToString(DataRowView_UnitId["Unit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters["Facility_Id"].DefaultValue = FacilityId;
        SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
        SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters["TableFROM"].DefaultValue = "Form_MedicationBundleCompliance_Bundles LEFT JOIN Form_MedicationBundleCompliance_VisitInformation ON Form_MedicationBundleCompliance_Bundles.MBC_VisitInformation_Id = Form_MedicationBundleCompliance_VisitInformation.MBC_VisitInformation_Id";
        SqlDataSource_MedicationBundleCompliance_EditUnit.SelectParameters["TableWHERE"].DefaultValue = "MBC_Bundles_Id = " + Request.QueryString["MBCBundlesId"] + " ";


        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 38";
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
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["MBCBundlesId"] != null)
      {
        Session["UnitName"] = "";
        string SQLStringMedicationBundleCompliance = "SELECT Unit_Name FROM vForm_MedicationBundleCompliance_Bundles WHERE MBC_Bundles_Id = @MBC_Bundles_Id";
        using (SqlCommand SqlCommand_MedicationBundleCompliance = new SqlCommand(SQLStringMedicationBundleCompliance))
        {
          SqlCommand_MedicationBundleCompliance.Parameters.AddWithValue("@MBC_Bundles_Id", Request.QueryString["MBCBundlesId"]);
          DataTable DataTable_MedicationBundleCompliance;
          using (DataTable_MedicationBundleCompliance = new DataTable())
          {
            DataTable_MedicationBundleCompliance.Locale = CultureInfo.CurrentCulture;
            DataTable_MedicationBundleCompliance = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_MedicationBundleCompliance).Copy();
            if (DataTable_MedicationBundleCompliance.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_MedicationBundleCompliance.Rows)
              {
                Session["UnitName"] = DataRow_Row["Unit_Name"];
              }
            }
          }
        }

        ((Label)FormView_MedicationBundleCompliance_Form.FindControl("Label_ItemUnit")).Text = Session["UnitName"].ToString();

        Session["UnitName"] = "";

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 38";
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
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Print", "InfoQuest_Print.aspx?PrintPage=Form_MedicationBundleCompliance&PrintValue=" + Request.QueryString["MBCBundlesId"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_MedicationBundleCompliance_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Email", "InfoQuest_Email.aspx?EmailPage=Form_MedicationBundleCompliance&EmailValue=" + Request.QueryString["MBCBundlesId"] + "") + "')";
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
    //---END--- --TableCurrentBundle--//


    //--START-- --TableBundle--//
    protected void SqlDataSource_MedicationBundleCompliance_Bundles_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_MedicationBundleCompliance_Bundles.PageSize = Convert.ToInt32(((DropDownList)GridView_MedicationBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_MedicationBundleCompliance_Bundles.PageIndex = ((DropDownList)GridView_MedicationBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_MedicationBundleCompliance_Bundles_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_MedicationBundleCompliance_Bundles.PageSize <= 20)
        {
          ((DropDownList)GridView_MedicationBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_MedicationBundleCompliance_Bundles.PageSize > 20 && GridView_MedicationBundleCompliance_Bundles.PageSize <= 50)
        {
          ((DropDownList)GridView_MedicationBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_MedicationBundleCompliance_Bundles.PageSize > 50 && GridView_MedicationBundleCompliance_Bundles.PageSize <= 100)
        {
          ((DropDownList)GridView_MedicationBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_MedicationBundleCompliance_Bundles_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_MedicationBundleCompliance_Bundles.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_MedicationBundleCompliance_Bundles.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_MedicationBundleCompliance_Bundles.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_MedicationBundleCompliance_Bundles_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance New Form", "Form_MedicationBundleCompliance.aspx?MBCVisitInformationId=" + Request.QueryString["MBCVisitInformationId"] + ""), false);
    }

    public string GetLink(object mbc_Bundles_Id)
    {
      string LinkURL = "";
      if (mbc_Bundles_Id != null)
      {
        if (Request.QueryString["MBCBundlesId"] == mbc_Bundles_Id.ToString())
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Form", "Form_MedicationBundleCompliance.aspx?MBCVisitInformationId=" + Request.QueryString["MBCVisitInformationId"] + "&MBCBundlesId=" + mbc_Bundles_Id + "") + "'>Selected</a>";
        }
        else
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Medication Bundle Compliance Form", "Form_MedicationBundleCompliance.aspx?MBCVisitInformationId=" + Request.QueryString["MBCVisitInformationId"] + "&MBCBundlesId=" + mbc_Bundles_Id + "") + "'>Select</a>";
        }
      }

      string FinalURL = LinkURL;

      return FinalURL;
    }
    //---END--- --TableBundle--//
  }
}