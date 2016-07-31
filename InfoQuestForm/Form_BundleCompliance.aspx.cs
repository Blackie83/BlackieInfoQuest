using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_BundleCompliance : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_BundleCompliance, this.GetType(), "UpdateProgress_Start", "Validation_Search();Validation_Form();Calculation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          PageTitle();

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_BundleCompliance_PatientVisitNumber"] != null)
          {
            TablePatientInfo.Visible = true;
            TableForm.Visible = true;
            TableList.Visible = true;

            SetFormVisibility();

            SqlDataSource_BundleCompliance_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
            SqlDataSource_BundleCompliance_Facility.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_BundleCompliance_Bundles";
            SqlDataSource_BundleCompliance_Facility.SelectParameters["TableWHERE"].DefaultValue = "Facility_Id = " + Request.QueryString["s_Facility_Id"] + " AND BC_Bundles_PatientVisitNumber = " + Request.QueryString["s_BundleCompliance_PatientVisitNumber"] + " ";

            if (string.IsNullOrEmpty(Request.QueryString["BC_Bundles_Id"]))
            {
              form_BundleCompliance.DefaultButton = Button_Search.UniqueID;

              if (((HiddenField)FormView_BundleCompliance_Form.FindControl("HiddenField_Insert")) != null)
              {
                SqlDataSource_BundleCompliance_InsertUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
                ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_InsertUnit")).Items.Clear();
                SqlDataSource_BundleCompliance_InsertUnit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
                ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_InsertUnit")).Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
                ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_InsertUnit")).DataBind();

                ((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_InsertDate")).Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
              }
            }
            else
            {
              form_BundleCompliance.DefaultButton = null;

              if (Request.QueryString["ViewMode"] == "0")
              {
              }
              else if (Request.QueryString["ViewMode"] == "1")
              {
                SqlDataSource_BundleCompliance_EditUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
                SqlDataSource_BundleCompliance_EditUnit.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
                SqlDataSource_BundleCompliance_EditUnit.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_BundleCompliance_Bundles";
                SqlDataSource_BundleCompliance_EditUnit.SelectParameters["TableWHERE"].DefaultValue = "BC_Bundles_Id = " + Request.QueryString["BC_Bundles_Id"] + " ";
              }
            }
          }
          else
          {
            form_BundleCompliance.DefaultButton = Button_Search.UniqueID;

            Label_InvalidSearchMessage.Text = "";
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            TableList.Visible = false;
          }


          if (TablePatientInfo.Visible == true)
          {
            PatientDataPI();

            TablePatientInfoVisible();
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
        if (Request.QueryString["s_Facility_Id"] == null)
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('17'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('17')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("17");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_BundleCompliance.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Bundle Compliance", "9");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_BundleCompliance_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_BundleCompliance_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_BundleCompliance_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_BundleCompliance_Facility.SelectParameters.Clear();
      SqlDataSource_BundleCompliance_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_BundleCompliance_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "17");
      SqlDataSource_BundleCompliance_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_BundleCompliance_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_BundleCompliance_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_BundleCompliance_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_BundleCompliance_InsertUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_BundleCompliance_InsertUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_BundleCompliance_InsertUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_BundleCompliance_InsertUnit.SelectParameters.Clear();
      SqlDataSource_BundleCompliance_InsertUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_BundleCompliance_InsertUnit.SelectParameters.Add("Form_Id", TypeCode.String, "17");
      SqlDataSource_BundleCompliance_InsertUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_BundleCompliance_InsertUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_BundleCompliance_InsertUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_BundleCompliance_InsertUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_BundleCompliance_EditUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_BundleCompliance_EditUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_BundleCompliance_EditUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_BundleCompliance_EditUnit.SelectParameters.Clear();
      SqlDataSource_BundleCompliance_EditUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_BundleCompliance_EditUnit.SelectParameters.Add("Form_Id", TypeCode.String, "17");
      SqlDataSource_BundleCompliance_EditUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_BundleCompliance_EditUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_BundleCompliance_EditUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_BundleCompliance_EditUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_BundleCompliance_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_BundleCompliance_Form.InsertCommand="INSERT INTO InfoQuest_Form_BundleCompliance_Bundles (Facility_Id ,	BC_Bundles_PatientVisitNumber ,  BC_Bundles_ReportNumber ,	BC_Bundles_Assessed_SSI ,	BC_Bundles_Assessed_CLABSI ,	BC_Bundles_Assessed_VAP ,	BC_Bundles_Assessed_CAUTI ,	BC_Bundles_Date ,	Unit_Id ,	BC_Bundles_SSI_TheatreProcedure ,	BC_Bundles_SSI_SelectAll ,	BC_Bundles_SSI_1 ,	BC_Bundles_SSI_2 ,	BC_Bundles_SSI_3 ,	BC_Bundles_SSI_4 ,	BC_Bundles_SSI_Cal ,	BC_Bundles_SSI_1_NA ,	BC_Bundles_SSI_2_NA ,	BC_Bundles_SSI_3_NA ,	BC_Bundles_SSI_4_NA ,	BC_Bundles_CLABSI_SelectAll ,	BC_Bundles_CLABSI_1 ,	BC_Bundles_CLABSI_2 ,	BC_Bundles_CLABSI_3 ,	BC_Bundles_CLABSI_4 ,	BC_Bundles_CLABSI_5 ,	BC_Bundles_CLABSI_6 ,	BC_Bundles_CLABSI_7 ,	BC_Bundles_CLABSI_Cal ,	BC_Bundles_CLABSI_1_NA ,	BC_Bundles_CLABSI_2_NA ,	BC_Bundles_CLABSI_3_NA ,	BC_Bundles_CLABSI_4_NA ,	BC_Bundles_CLABSI_5_NA ,	BC_Bundles_CLABSI_6_NA ,	BC_Bundles_CLABSI_7_NA ,	BC_Bundles_VAP_SelectAll ,	BC_Bundles_VAP_1 ,	BC_Bundles_VAP_2 ,	BC_Bundles_VAP_3 ,	BC_Bundles_VAP_4 ,	BC_Bundles_VAP_5 ,	BC_Bundles_VAP_Cal ,	BC_Bundles_VAP_1_NA ,	BC_Bundles_VAP_2_NA ,	BC_Bundles_VAP_3_NA ,	BC_Bundles_VAP_4_NA ,	BC_Bundles_VAP_5_NA ,	BC_Bundles_CAUTI_SelectAll ,	BC_Bundles_CAUTI_1 ,	BC_Bundles_CAUTI_2 ,	BC_Bundles_CAUTI_3 ,	BC_Bundles_CAUTI_4 ,	BC_Bundles_CAUTI_Cal ,	BC_Bundles_CAUTI_1_NA ,	BC_Bundles_CAUTI_2_NA ,	BC_Bundles_CAUTI_3_NA ,	BC_Bundles_CAUTI_4_NA ,	BC_Bundles_CreatedDate ,	BC_Bundles_CreatedBy ,	BC_Bundles_ModifiedDate ,	BC_Bundles_ModifiedBy ,	BC_Bundles_History ,	BC_Bundles_IsActive) VALUES (@Facility_Id ,@BC_Bundles_PatientVisitNumber ,@BC_Bundles_ReportNumber ,@BC_Bundles_Assessed_SSI ,@BC_Bundles_Assessed_CLABSI ,@BC_Bundles_Assessed_VAP ,@BC_Bundles_Assessed_CAUTI ,@BC_Bundles_Date ,@Unit_Id ,@BC_Bundles_SSI_TheatreProcedure ,@BC_Bundles_SSI_SelectAll ,@BC_Bundles_SSI_1 ,@BC_Bundles_SSI_2 ,@BC_Bundles_SSI_3 ,@BC_Bundles_SSI_4 ,@BC_Bundles_SSI_Cal ,@BC_Bundles_SSI_1_NA ,@BC_Bundles_SSI_2_NA ,@BC_Bundles_SSI_3_NA ,@BC_Bundles_SSI_4_NA ,@BC_Bundles_CLABSI_SelectAll ,@BC_Bundles_CLABSI_1 ,@BC_Bundles_CLABSI_2 ,@BC_Bundles_CLABSI_3 ,@BC_Bundles_CLABSI_4 ,@BC_Bundles_CLABSI_5 ,@BC_Bundles_CLABSI_6 ,@BC_Bundles_CLABSI_7 ,@BC_Bundles_CLABSI_Cal ,@BC_Bundles_CLABSI_1_NA ,@BC_Bundles_CLABSI_2_NA ,@BC_Bundles_CLABSI_3_NA ,@BC_Bundles_CLABSI_4_NA ,@BC_Bundles_CLABSI_5_NA ,@BC_Bundles_CLABSI_6_NA ,@BC_Bundles_CLABSI_7_NA ,@BC_Bundles_VAP_SelectAll ,@BC_Bundles_VAP_1 ,@BC_Bundles_VAP_2 ,@BC_Bundles_VAP_3 ,@BC_Bundles_VAP_4 ,@BC_Bundles_VAP_5 ,@BC_Bundles_VAP_Cal ,@BC_Bundles_VAP_1_NA ,@BC_Bundles_VAP_2_NA ,@BC_Bundles_VAP_3_NA ,@BC_Bundles_VAP_4_NA ,@BC_Bundles_VAP_5_NA ,@BC_Bundles_CAUTI_SelectAll ,@BC_Bundles_CAUTI_1 ,@BC_Bundles_CAUTI_2 ,@BC_Bundles_CAUTI_3 ,@BC_Bundles_CAUTI_4 ,@BC_Bundles_CAUTI_Cal ,@BC_Bundles_CAUTI_1_NA ,@BC_Bundles_CAUTI_2_NA ,@BC_Bundles_CAUTI_3_NA ,@BC_Bundles_CAUTI_4_NA ,@BC_Bundles_CreatedDate ,@BC_Bundles_CreatedBy ,@BC_Bundles_ModifiedDate ,@BC_Bundles_ModifiedBy ,@BC_Bundles_History ,@BC_Bundles_IsActive); SELECT @BC_Bundles_Id = SCOPE_IDENTITY()";
      SqlDataSource_BundleCompliance_Form.SelectCommand="SELECT * FROM InfoQuest_Form_BundleCompliance_Bundles WHERE (BC_Bundles_Id = @BC_Bundles_Id)";
      SqlDataSource_BundleCompliance_Form.UpdateCommand="UPDATE InfoQuest_Form_BundleCompliance_Bundles SET BC_Bundles_Assessed_SSI = @BC_Bundles_Assessed_SSI ,	BC_Bundles_Assessed_CLABSI = @BC_Bundles_Assessed_CLABSI ,	BC_Bundles_Assessed_VAP = @BC_Bundles_Assessed_VAP ,	BC_Bundles_Assessed_CAUTI = @BC_Bundles_Assessed_CAUTI ,	BC_Bundles_Date = @BC_Bundles_Date ,	Unit_Id = @Unit_Id ,	BC_Bundles_SSI_TheatreProcedure = @BC_Bundles_SSI_TheatreProcedure ,	BC_Bundles_SSI_SelectAll = @BC_Bundles_SSI_SelectAll ,	BC_Bundles_SSI_1 = @BC_Bundles_SSI_1 ,	BC_Bundles_SSI_2 = @BC_Bundles_SSI_2 ,	BC_Bundles_SSI_3 = @BC_Bundles_SSI_3 ,	BC_Bundles_SSI_4 = @BC_Bundles_SSI_4 ,	BC_Bundles_SSI_Cal = @BC_Bundles_SSI_Cal ,	BC_Bundles_SSI_1_NA = @BC_Bundles_SSI_1_NA ,	BC_Bundles_SSI_2_NA = @BC_Bundles_SSI_2_NA ,	BC_Bundles_SSI_3_NA = @BC_Bundles_SSI_3_NA ,	BC_Bundles_SSI_4_NA = @BC_Bundles_SSI_4_NA ,	BC_Bundles_CLABSI_SelectAll = @BC_Bundles_CLABSI_SelectAll ,	BC_Bundles_CLABSI_1 = @BC_Bundles_CLABSI_1 ,	BC_Bundles_CLABSI_2 = @BC_Bundles_CLABSI_2 ,	BC_Bundles_CLABSI_3 = @BC_Bundles_CLABSI_3 ,	BC_Bundles_CLABSI_4 = @BC_Bundles_CLABSI_4 ,	BC_Bundles_CLABSI_5 = @BC_Bundles_CLABSI_5 ,	BC_Bundles_CLABSI_6 = @BC_Bundles_CLABSI_6 ,	BC_Bundles_CLABSI_7 = @BC_Bundles_CLABSI_7 ,	BC_Bundles_CLABSI_Cal = @BC_Bundles_CLABSI_Cal ,	BC_Bundles_CLABSI_1_NA = @BC_Bundles_CLABSI_1_NA ,	BC_Bundles_CLABSI_2_NA = @BC_Bundles_CLABSI_2_NA ,	BC_Bundles_CLABSI_3_NA = @BC_Bundles_CLABSI_3_NA ,	BC_Bundles_CLABSI_4_NA = @BC_Bundles_CLABSI_4_NA ,	BC_Bundles_CLABSI_5_NA = @BC_Bundles_CLABSI_5_NA ,	BC_Bundles_CLABSI_6_NA = @BC_Bundles_CLABSI_6_NA ,	BC_Bundles_CLABSI_7_NA = @BC_Bundles_CLABSI_7_NA ,	BC_Bundles_VAP_SelectAll = @BC_Bundles_VAP_SelectAll ,	BC_Bundles_VAP_1 = @BC_Bundles_VAP_1 ,	BC_Bundles_VAP_2 = @BC_Bundles_VAP_2 ,	BC_Bundles_VAP_3 = @BC_Bundles_VAP_3 ,	BC_Bundles_VAP_4 = @BC_Bundles_VAP_4 ,	BC_Bundles_VAP_5 = @BC_Bundles_VAP_5 ,	BC_Bundles_VAP_Cal = @BC_Bundles_VAP_Cal ,	BC_Bundles_VAP_1_NA = @BC_Bundles_VAP_1_NA ,	BC_Bundles_VAP_2_NA = @BC_Bundles_VAP_2_NA ,	BC_Bundles_VAP_3_NA = @BC_Bundles_VAP_3_NA ,	BC_Bundles_VAP_4_NA = @BC_Bundles_VAP_4_NA ,	BC_Bundles_VAP_5_NA = @BC_Bundles_VAP_5_NA ,	BC_Bundles_CAUTI_SelectAll = @BC_Bundles_CAUTI_SelectAll ,	BC_Bundles_CAUTI_1 = @BC_Bundles_CAUTI_1 ,	BC_Bundles_CAUTI_2 = @BC_Bundles_CAUTI_2 ,	BC_Bundles_CAUTI_3 = @BC_Bundles_CAUTI_3 ,	BC_Bundles_CAUTI_4 = @BC_Bundles_CAUTI_4 ,	BC_Bundles_CAUTI_Cal = @BC_Bundles_CAUTI_Cal ,	BC_Bundles_CAUTI_1_NA = @BC_Bundles_CAUTI_1_NA ,	BC_Bundles_CAUTI_2_NA = @BC_Bundles_CAUTI_2_NA ,	BC_Bundles_CAUTI_3_NA = @BC_Bundles_CAUTI_3_NA ,	BC_Bundles_CAUTI_4_NA = @BC_Bundles_CAUTI_4_NA ,	BC_Bundles_ModifiedDate = @BC_Bundles_ModifiedDate ,	BC_Bundles_ModifiedBy = @BC_Bundles_ModifiedBy ,	BC_Bundles_History = @BC_Bundles_History ,	BC_Bundles_IsActive = @BC_Bundles_IsActive WHERE BC_Bundles_Id = @BC_Bundles_Id";
      SqlDataSource_BundleCompliance_Form.InsertParameters.Clear();
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_Id", TypeCode.Int32, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_PatientVisitNumber", TypeCode.String, Request.QueryString["s_BundleCompliance_PatientVisitNumber"]);
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_ReportNumber", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_Assessed_SSI", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_Assessed_CLABSI", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_Assessed_VAP", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_Assessed_CAUTI", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_Date", TypeCode.DateTime, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_TheatreProcedure", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_1", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_2", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_3", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_4", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_Cal", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_1_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_2_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_3_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_SSI_4_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_1", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_2", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_3", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_4", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_5", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_6", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_7", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_Cal", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_1_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_2_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_3_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_4_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_5_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_6_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CLABSI_7_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_1", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_2", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_3", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_4", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_5", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_Cal", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_1_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_2_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_3_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_4_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_VAP_5_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_1", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_2", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_3", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_4", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_Cal", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_1_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_2_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_3_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CAUTI_4_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_CreatedBy", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_ModifiedBy", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_History", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_BundleCompliance_Form.InsertParameters.Add("BC_Bundles_IsActive", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.SelectParameters.Clear();
      SqlDataSource_BundleCompliance_Form.SelectParameters.Add("BC_Bundles_Id", TypeCode.Int32, Request.QueryString["BC_Bundles_Id"]);
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Clear();
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_Assessed_SSI", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_Assessed_CLABSI", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_Assessed_VAP", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_Assessed_CAUTI", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_Date", TypeCode.DateTime, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_TheatreProcedure", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_1", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_2", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_3", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_4", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_Cal", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_1_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_2_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_3_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_SSI_4_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_1", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_2", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_3", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_4", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_5", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_6", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_7", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_Cal", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_1_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_2_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_3_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_4_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_5_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_6_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CLABSI_7_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_1", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_2", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_3", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_4", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_5", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_Cal", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_1_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_2_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_3_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_4_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_VAP_5_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_1", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_2", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_3", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_4", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_Cal", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_1_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_2_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_3_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_CAUTI_4_NA", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_ModifiedBy", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_History", TypeCode.String, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_IsActive", TypeCode.Boolean, "");
      SqlDataSource_BundleCompliance_Form.UpdateParameters.Add("BC_Bundles_Id", TypeCode.Int32, "");

      SqlDataSource_BundleCompliance_Bundles.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_BundleCompliance_Bundles.SelectCommand = "spForm_Get_BundleCompliance_Bundles";
      SqlDataSource_BundleCompliance_Bundles.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_BundleCompliance_Bundles.CancelSelectOnNullParameter = false;
      SqlDataSource_BundleCompliance_Bundles.SelectParameters.Clear();
      SqlDataSource_BundleCompliance_Bundles.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_BundleCompliance_Bundles.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_BundleCompliance_Bundles.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_BundleCompliance_PatientVisitNumber"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("17")).ToString(), CultureInfo.CurrentCulture);
      Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("17").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
      Label_PatientInfoHeading.Text = Convert.ToString("Patient Information", CultureInfo.CurrentCulture);
      Label_InterventionsHeading.Text = Convert.ToString("Bundles", CultureInfo.CurrentCulture);
      Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("17").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Facility.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Facility_Id"] == null)
        {
          DropDownList_Facility.SelectedValue = "";
        }
        else
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_PatientVisitNumber.Text.ToString()))
      {
        if (Request.QueryString["s_BundleCompliance_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_BundleCompliance_PatientVisitNumber"];
        }
      }
    }

    private void PatientDataPI()
    {
      DataTable DataTable_PatientDataPI;
      using (DataTable_PatientDataPI = new DataTable())
      {
        DataTable_PatientDataPI.Locale = CultureInfo.CurrentCulture;
        DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_BundleCompliance_PatientVisitNumber"]).Copy();
        //DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_BundleCompliance_PatientVisitNumber"]).Copy();

        if (DataTable_PatientDataPI.Columns.Count == 1)
        {
          Session["Error"] = "";
          foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Label_InvalidSearchMessage.Text = Session["Error"].ToString();
          TablePatientInfo.Visible = false;
          TableForm.Visible = false;
          TableList.Visible = false;
          Session["Error"] = "";
        }
        else
        {
          if (DataTable_PatientDataPI.Rows.Count == 0)
          {
            Label_InvalidSearchMessage.Text = Convert.ToString("Patient Visit Number " + Request.QueryString["s_BundleCompliance_PatientVisitNumber"] + " does not Exist", CultureInfo.CurrentCulture);
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            TableList.Visible = false;
          }
          else
          {
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["PatientVisitNumber"] = DataRow_Row["VisitNumber"];
              Session["PatientSurnameName"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];
              Session["PatientAge"] = DataRow_Row["PatientAge"];
              Session["PatientDateOfAdmission"] = DataRow_Row["DateOfAdmission"];
              Session["PatientDateOfDischarge"] = DataRow_Row["DateOfDischarge"];

              string NameSurnamePI = Session["PatientSurnameName"].ToString();
              NameSurnamePI = NameSurnamePI.Replace("'", "");
              Session["PatientSurnameName"] = NameSurnamePI;
              NameSurnamePI = "";

              Session["BCPIId"] = "";
              string SQLStringPatientInfo = "SELECT BC_PI_Id FROM InfoQuest_Form_BundleCompliance_PatientInformation WHERE Facility_Id = @Facility_Id AND BC_PI_PatientVisitNumber = @BC_PI_PatientVisitNumber";
              using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
              {
                SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_PatientInfo.Parameters.AddWithValue("@BC_PI_PatientVisitNumber", Request.QueryString["s_BundleCompliance_PatientVisitNumber"]);
                DataTable DataTable_PatientInfo;
                using (DataTable_PatientInfo = new DataTable())
                {
                  DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
                  DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
                  if (DataTable_PatientInfo.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                    {
                      Session["BCPIId"] = DataRow_Row1["BC_PI_Id"];
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(Session["BCPIId"].ToString()))
              {
                string SQLStringInsertBCPI = "INSERT INTO InfoQuest_Form_BundleCompliance_PatientInformation ( Facility_Id , BC_PI_PatientVisitNumber , BC_PI_PatientName , BC_PI_PatientAge , BC_PI_PatientDateOfAdmission , BC_PI_PatientDateofDischarge , BC_PI_Archived ) VALUES  ( @Facility_Id , @BC_PI_PatientVisitNumber , @BC_PI_PatientName , @BC_PI_PatientAge , @BC_PI_PatientDateOfAdmission , @BC_PI_PatientDateofDischarge , @BC_PI_Archived )";
                using (SqlCommand SqlCommand_InsertBCPI = new SqlCommand(SQLStringInsertBCPI))
                {
                  SqlCommand_InsertBCPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_InsertBCPI.Parameters.AddWithValue("@BC_PI_PatientVisitNumber", Session["PatientVisitNumber"].ToString());
                  SqlCommand_InsertBCPI.Parameters.AddWithValue("@BC_PI_PatientName", Session["PatientSurnameName"].ToString());
                  SqlCommand_InsertBCPI.Parameters.AddWithValue("@BC_PI_PatientAge", Session["PatientAge"].ToString());
                  SqlCommand_InsertBCPI.Parameters.AddWithValue("@BC_PI_PatientDateOfAdmission", Session["PatientDateOfAdmission"].ToString());
                  SqlCommand_InsertBCPI.Parameters.AddWithValue("@BC_PI_PatientDateofDischarge", Session["PatientDateOfDischarge"].ToString());
                  SqlCommand_InsertBCPI.Parameters.AddWithValue("@BC_PI_Archived", 0);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertBCPI);
                }
              }
              else
              {
                string SQLStringUpdateBCPI = "UPDATE InfoQuest_Form_BundleCompliance_PatientInformation SET BC_PI_PatientName = @BC_PI_PatientName , BC_PI_PatientAge = @BC_PI_PatientAge , BC_PI_PatientDateOfAdmission = @BC_PI_PatientDateOfAdmission , BC_PI_PatientDateofDischarge = @BC_PI_PatientDateofDischarge WHERE Facility_Id = @Facility_Id AND BC_PI_PatientVisitNumber = @BC_PI_PatientVisitNumber ";
                using (SqlCommand SqlCommand_UpdateBCPI = new SqlCommand(SQLStringUpdateBCPI))
                {
                  SqlCommand_UpdateBCPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_UpdateBCPI.Parameters.AddWithValue("@BC_PI_PatientVisitNumber", Session["PatientVisitNumber"].ToString());
                  SqlCommand_UpdateBCPI.Parameters.AddWithValue("@BC_PI_PatientName", Session["PatientSurnameName"].ToString());
                  SqlCommand_UpdateBCPI.Parameters.AddWithValue("@BC_PI_PatientAge", Session["PatientAge"].ToString());
                  SqlCommand_UpdateBCPI.Parameters.AddWithValue("@BC_PI_PatientDateOfAdmission", Session["PatientDateOfAdmission"].ToString());
                  SqlCommand_UpdateBCPI.Parameters.AddWithValue("@BC_PI_PatientDateofDischarge", Session["PatientDateOfDischarge"].ToString());
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateBCPI);
                }
              }
              Session["BCPIId"] = "";
            }
          }
        }
      }
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('17')) AND (Facility_Id IN (@Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '40'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '41'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '56'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '57'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              if (Request.QueryString["BC_Bundles_Id"] != null)
              {
                if (Request.QueryString["ViewMode"] == "1")
                {
                  FormView_BundleCompliance_Form.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                  FormView_BundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
              else
              {
                FormView_BundleCompliance_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Session["Security"] = "0";
              FormView_BundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";
              if (Request.QueryString["BC_Bundles_Id"] != null)
              {
                if (Request.QueryString["ViewMode"] == "1")
                {
                  FormView_BundleCompliance_Form.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                  FormView_BundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
              else
              {
                FormView_BundleCompliance_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              FormView_BundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
            }
            Session["Security"] = "1";
          }
        }
      }
    }

    private void TablePatientInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["BCPIPatientVisitNumber"] = "";
      Session["BCPIPatientName"] = "";
      Session["BCPIPatientAge"] = "";
      Session["BCPIPatientDateOfAdmission"] = "";
      Session["BCPIPatientDateofDischarge"] = "";

      string SQLStringPatientInfo = "SELECT DISTINCT Facility_FacilityDisplayName , BC_PI_PatientVisitNumber , BC_PI_PatientName , BC_PI_PatientAge , BC_PI_PatientDateOfAdmission , BC_PI_PatientDateofDischarge FROM vForm_BundleCompliance_PatientInformation WHERE Facility_Id = @Facility_Id AND BC_PI_PatientVisitNumber = @BC_PI_PatientVisitNumber";
      using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
      {
        SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PatientInfo.Parameters.AddWithValue("@BC_PI_PatientVisitNumber", Request.QueryString["s_BundleCompliance_PatientVisitNumber"]);
        DataTable DataTable_PatientInfo;
        using (DataTable_PatientInfo = new DataTable())
        {
          DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
          if (DataTable_PatientInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PatientInfo.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["BCPIPatientVisitNumber"] = DataRow_Row["BC_PI_PatientVisitNumber"];
              Session["BCPIPatientName"] = DataRow_Row["BC_PI_PatientName"];
              Session["BCPIPatientAge"] = DataRow_Row["BC_PI_PatientAge"];
              Session["BCPIPatientDateOfAdmission"] = DataRow_Row["BC_PI_PatientDateOfAdmission"];
              Session["BCPIPatientDateofDischarge"] = DataRow_Row["BC_PI_PatientDateofDischarge"];
            }
          }
        }
      }

      Label_PIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_PIVisitNumber.Text = Session["BCPIPatientVisitNumber"].ToString();
      Label_PIName.Text = Session["BCPIPatientName"].ToString();
      Label_PIAge.Text = Session["BCPIPatientAge"].ToString();
      Label_PIDateAdmission.Text = Session["BCPIPatientDateOfAdmission"].ToString();
      Label_PIDateDischarge.Text = Session["BCPIPatientDateofDischarge"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["BCPIPatientVisitNumber"] = "";
      Session["BCPIPatientName"] = "";
      Session["BCPIPatientAge"] = "";
      Session["BCPIPatientDateOfAdmission"] = "";
      Session["BCPIPatientDateofDischarge"] = "";
    }

    private void TableFormVisible()
    {
      if (FormView_BundleCompliance_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_InsertUnit")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedSSI")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedCLABSI")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedVAP")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedCAUTI")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_InsertSSITheatreProcedure")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSISelectAll")).Attributes.Add("OnClick", "Validation_Form('SSISelectAll');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI1")).Attributes.Add("OnClick", "Validation_Form('SSI1');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI1NA")).Attributes.Add("OnClick", "Validation_Form('SSI1NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI2")).Attributes.Add("OnClick", "Validation_Form('SSI2');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI2NA")).Attributes.Add("OnClick", "Validation_Form('SSI2NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI3")).Attributes.Add("OnClick", "Validation_Form('SSI3');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI3NA")).Attributes.Add("OnClick", "Validation_Form('SSI3NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI4")).Attributes.Add("OnClick", "Validation_Form('SSI4');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI4NA")).Attributes.Add("OnClick", "Validation_Form('SSI4NA');Calculation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSISelectAll")).Attributes.Add("OnClick", "Validation_Form('CLABSISelectAll');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI1")).Attributes.Add("OnClick", "Validation_Form('CLABSI1');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI1NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI1NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI2")).Attributes.Add("OnClick", "Validation_Form('CLABSI2');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI2NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI2NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI3")).Attributes.Add("OnClick", "Validation_Form('CLABSI3');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI3NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI3NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI4")).Attributes.Add("OnClick", "Validation_Form('CLABSI4');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI4NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI4NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI5")).Attributes.Add("OnClick", "Validation_Form('CLABSI5');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI5NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI5NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI6")).Attributes.Add("OnClick", "Validation_Form('CLABSI6');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI6NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI6NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI7")).Attributes.Add("OnClick", "Validation_Form('CLABSI7');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI7NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI7NA');Calculation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAPSelectAll")).Attributes.Add("OnClick", "Validation_Form('VAPSelectAll');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP1")).Attributes.Add("OnClick", "Validation_Form('VAP1');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP1NA")).Attributes.Add("OnClick", "Validation_Form('VAP1NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP2")).Attributes.Add("OnClick", "Validation_Form('VAP2');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP2NA")).Attributes.Add("OnClick", "Validation_Form('VAP2NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP3")).Attributes.Add("OnClick", "Validation_Form('VAP3');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP3NA")).Attributes.Add("OnClick", "Validation_Form('VAP3NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP4")).Attributes.Add("OnClick", "Validation_Form('VAP4');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP4NA")).Attributes.Add("OnClick", "Validation_Form('VAP4NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP5")).Attributes.Add("OnClick", "Validation_Form('VAP5');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP5NA")).Attributes.Add("OnClick", "Validation_Form('VAP5NA');Calculation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTISelectAll")).Attributes.Add("OnClick", "Validation_Form('CAUTISelectAll');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI1")).Attributes.Add("OnClick", "Validation_Form('CAUTI1');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI1NA")).Attributes.Add("OnClick", "Validation_Form('CAUTI1NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI2")).Attributes.Add("OnClick", "Validation_Form('CAUTI2');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI2NA")).Attributes.Add("OnClick", "Validation_Form('CAUTI2NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI3")).Attributes.Add("OnClick", "Validation_Form('CAUTI3');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI3NA")).Attributes.Add("OnClick", "Validation_Form('CAUTI3NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI4")).Attributes.Add("OnClick", "Validation_Form('CAUTI4');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI4NA")).Attributes.Add("OnClick", "Validation_Form('CAUTI4NA');Calculation_Form();");
      }

      if (FormView_BundleCompliance_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_EditUnit")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedSSI")).Attributes.Add("OnClick", "Validation_Form('AssessedSSI');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedCLABSI")).Attributes.Add("OnClick", "Validation_Form('AssessedCLABSI');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedVAP")).Attributes.Add("OnClick", "Validation_Form('AssessedVAP');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedCAUTI")).Attributes.Add("OnClick", "Validation_Form('AssessedCAUTI');Calculation_Form();");
        ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_EditSSITheatreProcedure")).Attributes.Add("OnChange", "Validation_Form('SSITheatreProcedure');Calculation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSISelectAll")).Attributes.Add("OnClick", "Validation_Form('SSISelectAll');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI1")).Attributes.Add("OnClick", "Validation_Form('SSI1');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI1NA")).Attributes.Add("OnClick", "Validation_Form('SSI1NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI2")).Attributes.Add("OnClick", "Validation_Form('SSI2');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI2NA")).Attributes.Add("OnClick", "Validation_Form('SSI2NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI3")).Attributes.Add("OnClick", "Validation_Form('SSI3');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI3NA")).Attributes.Add("OnClick", "Validation_Form('SSI3NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI4")).Attributes.Add("OnClick", "Validation_Form('SSI4');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI4NA")).Attributes.Add("OnClick", "Validation_Form('SSI4NA');Calculation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSISelectAll")).Attributes.Add("OnClick", "Validation_Form('CLABSISelectAll');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI1")).Attributes.Add("OnClick", "Validation_Form('CLABSI1');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI1NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI1NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI2")).Attributes.Add("OnClick", "Validation_Form('CLABSI2');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI2NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI2NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI3")).Attributes.Add("OnClick", "Validation_Form('CLABSI3');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI3NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI3NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI4")).Attributes.Add("OnClick", "Validation_Form('CLABSI4');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI4NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI4NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI5")).Attributes.Add("OnClick", "Validation_Form('CLABSI5');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI5NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI5NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI6")).Attributes.Add("OnClick", "Validation_Form('CLABSI6');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI6NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI6NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI7")).Attributes.Add("OnClick", "Validation_Form('CLABSI7');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI7NA")).Attributes.Add("OnClick", "Validation_Form('CLABSI7NA');Calculation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAPSelectAll")).Attributes.Add("OnClick", "Validation_Form('VAPSelectAll');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP1")).Attributes.Add("OnClick", "Validation_Form('VAP1');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP1NA")).Attributes.Add("OnClick", "Validation_Form('VAP1NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP2")).Attributes.Add("OnClick", "Validation_Form('VAP2');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP2NA")).Attributes.Add("OnClick", "Validation_Form('VAP2NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP3")).Attributes.Add("OnClick", "Validation_Form('VAP3');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP3NA")).Attributes.Add("OnClick", "Validation_Form('VAP3NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP4")).Attributes.Add("OnClick", "Validation_Form('VAP4');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP4NA")).Attributes.Add("OnClick", "Validation_Form('VAP4NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP5")).Attributes.Add("OnClick", "Validation_Form('VAP5');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP5NA")).Attributes.Add("OnClick", "Validation_Form('VAP5NA');Calculation_Form();");

        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTISelectAll")).Attributes.Add("OnClick", "Validation_Form('CAUTISelectAll');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI1")).Attributes.Add("OnClick", "Validation_Form('CAUTI1');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI1NA")).Attributes.Add("OnClick", "Validation_Form('CAUTI1NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI2")).Attributes.Add("OnClick", "Validation_Form('CAUTI2');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI2NA")).Attributes.Add("OnClick", "Validation_Form('CAUTI2NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI3")).Attributes.Add("OnClick", "Validation_Form('CAUTI3');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI3NA")).Attributes.Add("OnClick", "Validation_Form('CAUTI3NA');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI4")).Attributes.Add("OnClick", "Validation_Form('CAUTI4');Calculation_Form();");
        ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI4NA")).Attributes.Add("OnClick", "Validation_Form('CAUTI4NA');Calculation_Form();");
      }
    }


    //--START-- --Search--//
    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Form", "Form_BundleCompliance.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Form", "Form_BundleCompliance.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_BundleCompliance_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + ""), false);
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
      string SearchField2 = Request.QueryString["Search_BundleCompliancePatientVisitNumber"];
      string SearchField3 = Request.QueryString["Search_BundleCompliancePatientName"];
      string SearchField4 = Request.QueryString["Search_BundleComplianceReportNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_BundleCompliance_PatientVisitNumber=" + Request.QueryString["Search_BundleCompliancePatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_BundleCompliance_PatientName=" + Request.QueryString["Search_BundleCompliancePatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_BundleCompliance_ReportNumber=" + Request.QueryString["Search_BundleComplianceReportNumber"] + "&";
      }

      string FinalURL = "Form_BundleCompliance_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance List", FinalURL);

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --TableForm--//
    protected void FormView_BundleCompliance_Form_ItemInserting(object sender, CancelEventArgs e)
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
          Page.MaintainScrollPositionOnPostBack = false;
          ((Label)FormView_BundleCompliance_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_BundleCompliance_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          Session["BC_Bundles_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Request.QueryString["s_Facility_Id"], "17");

          SqlDataSource_BundleCompliance_Form.InsertParameters["Unit_Id"].DefaultValue = ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_InsertUnit")).SelectedValue;

          SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_ReportNumber"].DefaultValue = Session["BC_Bundles_ReportNumber"].ToString();
          SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_History"].DefaultValue = "";
          SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_IsActive"].DefaultValue = "true";


          if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedSSI")).Checked == false)
          {
            SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_SSI_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal SSI_Total;
            SSI_Total = 0;
            Decimal SSI_Selected;
            SSI_Selected = 0;

            for (int a = 1; a <= 4; a++)
            {
              if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI" + a + "")).Checked == true)
              {
                SSI_Total = SSI_Total + 1;
              }
              else
              {
                SSI_Total = SSI_Total + 0;
              }

              if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertSSI" + a + "NA")).Checked == false)
              {
                SSI_Selected = SSI_Selected + 1;
              }
              else
              {
                SSI_Selected = SSI_Selected + 0;
              }
            }

            Decimal SSI_Cal = (SSI_Total * 100 / SSI_Selected);
            SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_SSI_Cal"].DefaultValue = Decimal.Round(SSI_Cal, MidpointRounding.ToEven) + " %";
          }


          if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedCLABSI")).Checked == false)
          {
            SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_CLABSI_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal CLABSI_Total;
            CLABSI_Total = 0;
            Decimal CLABSI_Selected;
            CLABSI_Selected = 0;

            for (int a = 1; a <= 7; a++)
            {
              if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI" + a + "")).Checked == true)
              {
                CLABSI_Total = CLABSI_Total + 1;
              }
              else
              {
                CLABSI_Total = CLABSI_Total + 0;
              }

              if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCLABSI" + a + "NA")).Checked == false)
              {
                CLABSI_Selected = CLABSI_Selected + 1;
              }
              else
              {
                CLABSI_Selected = CLABSI_Selected + 0;
              }
            }

            Decimal CLABSI_Cal = (CLABSI_Total * 100 / CLABSI_Selected);
            SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_CLABSI_Cal"].DefaultValue = Decimal.Round(CLABSI_Cal, MidpointRounding.ToEven) + " %";
          }


          if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedVAP")).Checked == false)
          {
            SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_VAP_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal VAP_Total;
            VAP_Total = 0;
            Decimal VAP_Selected;
            VAP_Selected = 0;

            for (int a = 1; a <= 5; a++)
            {
              if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP" + a + "")).Checked == true)
              {
                VAP_Total = VAP_Total + 1;
              }
              else
              {
                VAP_Total = VAP_Total + 0;
              }

              if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertVAP" + a + "NA")).Checked == false)
              {
                VAP_Selected = VAP_Selected + 1;
              }
              else
              {
                VAP_Selected = VAP_Selected + 0;
              }
            }

            Decimal VAP_Cal = (VAP_Total * 100 / VAP_Selected);
            SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_VAP_Cal"].DefaultValue = Decimal.Round(VAP_Cal, MidpointRounding.ToEven) + " %";
          }


          if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedCAUTI")).Checked == false)
          {
            SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_CAUTI_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal CAUTI_Total;
            CAUTI_Total = 0;
            Decimal CAUTI_Selected;
            CAUTI_Selected = 0;

            for (int a = 1; a <= 4; a++)
            {
              if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI" + a + "")).Checked == true)
              {
                CAUTI_Total = CAUTI_Total + 1;
              }
              else
              {
                CAUTI_Total = CAUTI_Total + 0;
              }

              if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertCAUTI" + a + "NA")).Checked == false)
              {
                CAUTI_Selected = CAUTI_Selected + 1;
              }
              else
              {
                CAUTI_Selected = CAUTI_Selected + 0;
              }
            }

            Decimal CAUTI_Cal = (CAUTI_Total * 100 / CAUTI_Selected);
            SqlDataSource_BundleCompliance_Form.InsertParameters["BC_Bundles_CAUTI_Cal"].DefaultValue = Decimal.Round(CAUTI_Cal, MidpointRounding.ToEven) + " %";
          }

          Session["BC_Bundles_ReportNumber"] = "";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_InsertUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_InsertDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedSSI")).Checked == false && ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedCLABSI")).Checked == false && ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedVAP")).Checked == false && ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedCAUTI")).Checked == false)
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_InsertAssessedSSI")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_InsertSSITheatreProcedure")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else
          {
            string TheatreProcedureText = ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_InsertSSITheatreProcedure")).SelectedItem.Text.ToString();
            int TheatreProcedureIndex = TheatreProcedureText.IndexOf("NO : ", StringComparison.CurrentCulture);

            if (TheatreProcedureIndex == -1)
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateDate = ((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_InsertDate")).Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Observation Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_InsertDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Observation dates allowed<br />";
          }
          else
          {
            Session["ValidCapture"] = "";
            Session["CutOffDay"] = "";
            string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 17),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@BC_Bundles_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 17),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@BC_Bundles_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 17) AS CutOffDay";
            using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
            {
              SqlCommand_ValidCapture.Parameters.AddWithValue("@BC_Bundles_Date", PickedDate);
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
              InvalidFormMessage = InvalidFormMessage + "Observation date is not valid. Forms may be captured between the 1st of a calendar month until the " + Session["CutOffDay"].ToString() + "th of the following month <br />";
            }

            Session["ValidCapture"] = "";
            Session["CutOffDay"] = "";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_BundleCompliance_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["BC_Bundles_Id"] = e.Command.Parameters["@BC_Bundles_Id"].Value;
        Session["BC_Bundles_ReportNumber"] = e.Command.Parameters["@BC_Bundles_ReportNumber"].Value;
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_BundleCompliance&ReportNumber=" + Session["BC_Bundles_ReportNumber"].ToString() + ""), false);
      }
    }


    protected void FormView_BundleCompliance_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDBCBundlesModifiedDate"] = e.OldValues["BC_Bundles_ModifiedDate"];
        object OLDBCBundlesModifiedDate = Session["OLDBCBundlesModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDBCBundlesModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareBundleCompliance = (DataView)SqlDataSource_BundleCompliance_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareBundleCompliance = DataView_CompareBundleCompliance[0];
        Session["DBBCBundlesModifiedDate"] = Convert.ToString(DataRowView_CompareBundleCompliance["BC_Bundles_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBBCBundlesModifiedBy"] = Convert.ToString(DataRowView_CompareBundleCompliance["BC_Bundles_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBBCBundlesModifiedDate = Session["DBBCBundlesModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBBCBundlesModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          Page.MaintainScrollPositionOnPostBack = false;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBBCBundlesModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_BundleCompliance_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_BundleCompliance_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            Page.MaintainScrollPositionOnPostBack = false;
            ((Label)FormView_BundleCompliance_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_BundleCompliance_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["BC_Bundles_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["BC_Bundles_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];
            e.NewValues["Unit_Id"] = ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_EditUnit")).SelectedValue;

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_BundleCompliance", "BC_Bundles_Id = " + Request.QueryString["BC_Bundles_Id"]);

            DataView DataView_BundleCompliance = (DataView)SqlDataSource_BundleCompliance_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_BundleCompliance = DataView_BundleCompliance[0];
            Session["BCBundlesHistory"] = Convert.ToString(DataRowView_BundleCompliance["BC_Bundles_History"], CultureInfo.CurrentCulture);

            Session["BCBundlesHistory"] = Session["History"].ToString() + Session["BCBundlesHistory"].ToString();
            e.NewValues["BC_Bundles_History"] = Session["BCBundlesHistory"].ToString();

            Session["BCBundlesHistory"] = "";
            Session["History"] = "";

            if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedSSI")).Checked == false)
            {
              e.NewValues["BC_Bundles_SSI_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal SSI_Total;
              SSI_Total = 0;
              Decimal SSI_Selected;
              SSI_Selected = 0;

              for (int a = 1; a <= 4; a++)
              {
                if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI" + a + "")).Checked == true)
                {
                  SSI_Total = SSI_Total + 1;
                }
                else
                {
                  SSI_Total = SSI_Total + 0;
                }

                if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditSSI" + a + "NA")).Checked == false)
                {
                  SSI_Selected = SSI_Selected + 1;
                }
                else
                {
                  SSI_Selected = SSI_Selected + 0;
                }
              }

              Decimal SSI_Cal = (SSI_Total * 100 / SSI_Selected);
              e.NewValues["BC_Bundles_SSI_Cal"] = Decimal.Round(SSI_Cal, MidpointRounding.ToEven) + " %";
            }


            if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedCLABSI")).Checked == false)
            {
              e.NewValues["BC_Bundles_CLABSI_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal CLABSI_Total;
              CLABSI_Total = 0;
              Decimal CLABSI_Selected;
              CLABSI_Selected = 0;

              for (int a = 1; a <= 7; a++)
              {
                if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI" + a + "")).Checked == true)
                {
                  CLABSI_Total = CLABSI_Total + 1;
                }
                else
                {
                  CLABSI_Total = CLABSI_Total + 0;
                }

                if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCLABSI" + a + "NA")).Checked == false)
                {
                  CLABSI_Selected = CLABSI_Selected + 1;
                }
                else
                {
                  CLABSI_Selected = CLABSI_Selected + 0;
                }
              }

              Decimal CLABSI_Cal = (CLABSI_Total * 100 / CLABSI_Selected);
              e.NewValues["BC_Bundles_CLABSI_Cal"] = Decimal.Round(CLABSI_Cal, MidpointRounding.ToEven) + " %";
            }


            if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedVAP")).Checked == false)
            {
              e.NewValues["BC_Bundles_VAP_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal VAP_Total;
              VAP_Total = 0;
              Decimal VAP_Selected;
              VAP_Selected = 0;

              for (int a = 1; a <= 5; a++)
              {
                if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP" + a + "")).Checked == true)
                {
                  VAP_Total = VAP_Total + 1;
                }
                else
                {
                  VAP_Total = VAP_Total + 0;
                }

                if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditVAP" + a + "NA")).Checked == false)
                {
                  VAP_Selected = VAP_Selected + 1;
                }
                else
                {
                  VAP_Selected = VAP_Selected + 0;
                }
              }

              Decimal VAP_Cal = (VAP_Total * 100 / VAP_Selected);
              e.NewValues["BC_Bundles_VAP_Cal"] = Decimal.Round(VAP_Cal, MidpointRounding.ToEven) + " %";
            }


            if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedCAUTI")).Checked == false)
            {
              e.NewValues["BC_Bundles_CAUTI_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal CAUTI_Total;
              CAUTI_Total = 0;
              Decimal CAUTI_Selected;
              CAUTI_Selected = 0;

              for (int a = 1; a <= 4; a++)
              {
                if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI" + a + "")).Checked == true)
                {
                  CAUTI_Total = CAUTI_Total + 1;
                }
                else
                {
                  CAUTI_Total = CAUTI_Total + 0;
                }

                if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditCAUTI" + a + "NA")).Checked == false)
                {
                  CAUTI_Selected = CAUTI_Selected + 1;
                }
                else
                {
                  CAUTI_Selected = CAUTI_Selected + 0;
                }
              }

              Decimal CAUTI_Cal = (CAUTI_Total * 100 / CAUTI_Selected);
              e.NewValues["BC_Bundles_CAUTI_Cal"] = Decimal.Round(CAUTI_Cal, MidpointRounding.ToEven) + " %";
            }
          }
        }

        Session["OLDBCBundlesModifiedDate"] = "";
        Session["DBBCBundlesModifiedDate"] = "";
        Session["DBBCBundlesModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_EditUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_EditDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedSSI")).Checked == false && ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedCLABSI")).Checked == false && ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedVAP")).Checked == false && ((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedCAUTI")).Checked == false)
        {
          InvalidForm = "Yes";
        }

        if (((CheckBox)FormView_BundleCompliance_Form.FindControl("CheckBox_EditAssessedSSI")).Checked == true)
        {
          if (string.IsNullOrEmpty(((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_EditSSITheatreProcedure")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else
          {
            string TheatreProcedureText = ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_EditSSITheatreProcedure")).SelectedItem.Text.ToString();
            int TheatreProcedureIndex = TheatreProcedureText.IndexOf("NO : ", StringComparison.CurrentCulture);

            if (TheatreProcedureIndex == -1)
            {
              if (((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_EditSSITheatreProcedure")).SelectedValue.ToString() != ((HiddenField)FormView_BundleCompliance_Form.FindControl("HiddenField_EditSSITheatreProcedure")).Value.ToString())
              {
                InvalidForm = "Yes";
              }
            }
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        string DateToValidateDate = ((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_EditDate")).Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Observation Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_BundleCompliance_Form.FindControl("TextBox_EditDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Observation dates allowed<br />";
          }
          else
          {
            DateTime PreviousDate = Convert.ToDateTime(((HiddenField)FormView_BundleCompliance_Form.FindControl("HiddenField_EditDate")).Value, CultureInfo.CurrentCulture);

            if (PickedDate.CompareTo(PreviousDate) != 0)
            {
              Session["ValidCapture"] = "";
              Session["CutOffDay"] = "";
              string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 17),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@BC_Bundles_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 17),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@BC_Bundles_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 17) AS CutOffDay";
              using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
              {
                SqlCommand_ValidCapture.Parameters.AddWithValue("@BC_Bundles_Date", PickedDate);
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
                InvalidFormMessage = InvalidFormMessage + "Observation date is not valid. Forms may be captured between the 1st of a calendar month until the " + Session["CutOffDay"].ToString() + "th of the following month <br />";
              }

              Session["ValidCapture"] = "";
              Session["CutOffDay"] = "";
            }
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_BundleCompliance_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Form", "Form_BundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_BundleCompliance_PatientVisitNumber=" + Request.QueryString["s_BundleCompliance_PatientVisitNumber"] + ""), false);
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_BundleCompliance, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Print", "InfoQuest_Print.aspx?PrintPage=Form_BundleCompliance&PrintValue=" + Request.QueryString["BC_Bundles_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_BundleCompliance, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_BundleCompliance, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Email", "InfoQuest_Email.aspx?EmailPage=Form_BundleCompliance&EmailValue=" + Request.QueryString["BC_Bundles_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_BundleCompliance, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }


    protected void FormView_BundleCompliance_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["BC_Bundles_Id"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Form", "Form_BundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_BundleCompliance_PatientVisitNumber=" + Request.QueryString["s_BundleCompliance_PatientVisitNumber"] + ""), false);
          }
        }
      }
    }

    protected void FormView_BundleCompliance_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_BundleCompliance_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_BundleCompliance_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      if (Request.QueryString["BC_Bundles_Id"] != null)
      {
        DataView DataView_UnitId = (DataView)SqlDataSource_BundleCompliance_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_UnitId = DataView_UnitId[0];
        ((DropDownList)FormView_BundleCompliance_Form.FindControl("DropDownList_EditUnit")).SelectedValue = Convert.ToString(DataRowView_UnitId["Unit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_BundleCompliance_EditUnit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
        SqlDataSource_BundleCompliance_EditUnit.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
        SqlDataSource_BundleCompliance_EditUnit.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_BundleCompliance_Bundles";
        SqlDataSource_BundleCompliance_EditUnit.SelectParameters["TableWHERE"].DefaultValue = "BC_Bundles_Id = " + Request.QueryString["BC_Bundles_Id"] + " ";

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 17";
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
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["BC_Bundles_Id"] != null)
      {
        Session["UnitName"] = "";
        string SQLStringBundleCompliance = "SELECT Unit_Name FROM vForm_BundleCompliance WHERE BC_Bundles_Id = @BC_Bundles_Id";
        using (SqlCommand SqlCommand_BundleCompliance = new SqlCommand(SQLStringBundleCompliance))
        {
          SqlCommand_BundleCompliance.Parameters.AddWithValue("@BC_Bundles_Id", Request.QueryString["BC_Bundles_Id"]);
          DataTable DataTable_BundleCompliance;
          using (DataTable_BundleCompliance = new DataTable())
          {
            DataTable_BundleCompliance.Locale = CultureInfo.CurrentCulture;
            DataTable_BundleCompliance = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_BundleCompliance).Copy();
            if (DataTable_BundleCompliance.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_BundleCompliance.Rows)
              {
                Session["UnitName"] = DataRow_Row["Unit_Name"];
              }
            }
          }
        }

        ((Label)FormView_BundleCompliance_Form.FindControl("Label_ItemUnit")).Text = Session["UnitName"].ToString();

        Session["UnitName"] = "";

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 17";
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
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Print", "InfoQuest_Print.aspx?PrintPage=Form_BundleCompliance&PrintValue=" + Request.QueryString["BC_Bundles_Id"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_BundleCompliance_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Email", "InfoQuest_Email.aspx?EmailPage=Form_BundleCompliance&EmailValue=" + Request.QueryString["BC_Bundles_Id"] + "") + "')";
        }

        Email = "";
        Print = "";
      }
    }


    protected void DropDownList_InsertSSITheatreProcedure_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertSSITheatreProcedure = (DropDownList)sender;

      DropDownList_InsertSSITheatreProcedure.Items.Clear();
      DropDownList_InsertSSITheatreProcedure.Items.Insert(0, new ListItem(Convert.ToString("Select Theatre Event", CultureInfo.CurrentCulture), ""));

      Session["Theatre"] = "";
      Session["TheatreDate"] = "";
      Session["TheatreProcedure"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataPatient;
      using (DataTable_DataPatient = new DataTable())
      {
        DataTable_DataPatient.Locale = CultureInfo.CurrentCulture;
        DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_TheatreInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_BundleCompliance_PatientVisitNumber"]).Copy();

        if (DataTable_DataPatient.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["Theatre"] = "";
          Session["TheatreDate"] = "";
          Session["TheatreProcedure"] = "";
        }
        else if (DataTable_DataPatient.Columns.Count != 1)
        {
          if (DataTable_DataPatient.Rows.Count > 0)
          {
            int a = 1;
            foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
            {
              Session["TheatreDate"] = DataRow_Row["Theatre_Date"];
              Session["TheatreProcedure"] = DataRow_Row["Theatre_Procedure_Description"];

              if (!string.IsNullOrEmpty(Session["TheatreDate"].ToString()) && !string.IsNullOrEmpty(Session["TheatreProcedure"].ToString()))
              {
                string TheatreProcedure = Session["TheatreProcedure"].ToString();
                TheatreProcedure = TheatreProcedure.Replace("'", "");
                if (TheatreProcedure.Length > 55)
                {
                  TheatreProcedure = TheatreProcedure.Remove(55, (Int32)TheatreProcedure.Length - 55);
                }

                Session["TheatreProcedure"] = TheatreProcedure;
                TheatreProcedure = "";
                Session["Theatre"] = Session["TheatreDate"].ToString() + " : " + Session["TheatreProcedure"].ToString();

                Session["BCBundlesId"] = "";
                string SQLStringSSITheatreProcedure = "SELECT BC_Bundles_Id FROM InfoQuest_Form_BundleCompliance_Bundles WHERE Facility_Id = @Facility_Id AND BC_Bundles_PatientVisitNumber = @BC_Bundles_PatientVisitNumber AND BC_Bundles_Assessed_SSI = 1 AND BC_Bundles_IsActive = 1 AND BC_Bundles_SSI_TheatreProcedure = @BC_Bundles_SSI_TheatreProcedure";
                using (SqlCommand SqlCommand_SSITheatreProcedure = new SqlCommand(SQLStringSSITheatreProcedure))
                {
                  SqlCommand_SSITheatreProcedure.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_SSITheatreProcedure.Parameters.AddWithValue("@BC_Bundles_PatientVisitNumber", Request.QueryString["s_BundleCompliance_PatientVisitNumber"]);
                  SqlCommand_SSITheatreProcedure.Parameters.AddWithValue("@BC_Bundles_SSI_TheatreProcedure", Session["Theatre"].ToString());
                  DataTable DataTable_SSITheatreProcedure;
                  using (DataTable_SSITheatreProcedure = new DataTable())
                  {
                    DataTable_SSITheatreProcedure.Locale = CultureInfo.CurrentCulture;
                    DataTable_SSITheatreProcedure = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SSITheatreProcedure).Copy();
                    if (DataTable_SSITheatreProcedure.Rows.Count > 0)
                    {
                      foreach (DataRow DataRow_Row1 in DataTable_SSITheatreProcedure.Rows)
                      {
                        Session["BCBundlesId"] = DataRow_Row1["BC_Bundles_Id"];
                      }
                    }
                  }
                }


                if (string.IsNullOrEmpty(Session["BCBundlesId"].ToString()))
                {
                  DropDownList_InsertSSITheatreProcedure.Items.Insert(a, new ListItem(Convert.ToString("NO : " + Session["Theatre"].ToString(), CultureInfo.CurrentCulture), Session["Theatre"].ToString()));
                }
                else
                {
                  DropDownList_InsertSSITheatreProcedure.Items.Insert(a, new ListItem(Convert.ToString("YES : " + Session["Theatre"].ToString(), CultureInfo.CurrentCulture), Session["Theatre"].ToString()));
                }

                Session["BCBundlesId"] = "";
              }
              a = a + 1;
            }
          }
        }
      }

      Session["Theatre"] = "";
      Session["TheatreDate"] = "";
      Session["TheatreProcedure"] = "";
      Session["Error"] = "";
    }

    protected void DropDownList_EditSSITheatreProcedure_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditSSITheatreProcedure = (DropDownList)sender;
      HiddenField HiddenField_EditSSITheatreProcedure = (HiddenField)FormView_BundleCompliance_Form.FindControl("HiddenField_EditSSITheatreProcedure");

      int a = 1;

      DropDownList_EditSSITheatreProcedure.Items.Clear();
      DropDownList_EditSSITheatreProcedure.Items.Insert(0, new ListItem(Convert.ToString("Select Theatre Event", CultureInfo.CurrentCulture), ""));
      if (!string.IsNullOrEmpty(HiddenField_EditSSITheatreProcedure.Value.ToString()))
      {
        DropDownList_EditSSITheatreProcedure.Items.Insert(a, new ListItem(Convert.ToString("YES : " + HiddenField_EditSSITheatreProcedure.Value.ToString(), CultureInfo.CurrentCulture), HiddenField_EditSSITheatreProcedure.Value.ToString()));
        a = a + 1;
      }

      Session["Theatre"] = "";
      Session["TheatreDate"] = "";
      Session["TheatreProcedure"] = "";
      Session["Error"] = "";
      DataTable DataTable_DataPatient;
      using (DataTable_DataPatient = new DataTable())
      {
        DataTable_DataPatient.Locale = CultureInfo.CurrentCulture;
        DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_TheatreInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_BundleCompliance_PatientVisitNumber"]).Copy();
        if (DataTable_DataPatient.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }

          Session["Theatre"] = "";
          Session["TheatreDate"] = "";
          Session["TheatreProcedure"] = "";
        }
        else if (DataTable_DataPatient.Columns.Count != 1)
        {
          if (DataTable_DataPatient.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
            {
              Session["TheatreDate"] = DataRow_Row["Theatre_Date"];
              Session["TheatreProcedure"] = DataRow_Row["Theatre_Procedure_Description"];

              if (!string.IsNullOrEmpty(Session["TheatreDate"].ToString()) && !string.IsNullOrEmpty(Session["TheatreProcedure"].ToString()))
              {
                string TheatreProcedure = Session["TheatreProcedure"].ToString();
                TheatreProcedure = TheatreProcedure.Replace("'", "");
                if (TheatreProcedure.Length > 55)
                {
                  TheatreProcedure = TheatreProcedure.Remove(55, (Int32)TheatreProcedure.Length - 55);
                }

                Session["TheatreProcedure"] = TheatreProcedure;
                TheatreProcedure = "";
                Session["Theatre"] = Session["TheatreDate"].ToString() + " : " + Session["TheatreProcedure"].ToString();

                Session["BCBundlesId"] = "";
                string SQLStringSSITheatreProcedure = "SELECT BC_Bundles_Id FROM InfoQuest_Form_BundleCompliance_Bundles WHERE Facility_Id = @Facility_Id AND BC_Bundles_PatientVisitNumber = @BC_Bundles_PatientVisitNumber AND BC_Bundles_Assessed_SSI = 1 AND BC_Bundles_IsActive = 1 AND BC_Bundles_SSI_TheatreProcedure = @BC_Bundles_SSI_TheatreProcedure";
                using (SqlCommand SqlCommand_SSITheatreProcedure = new SqlCommand(SQLStringSSITheatreProcedure))
                {
                  SqlCommand_SSITheatreProcedure.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_SSITheatreProcedure.Parameters.AddWithValue("@BC_Bundles_PatientVisitNumber", Request.QueryString["s_BundleCompliance_PatientVisitNumber"]);
                  SqlCommand_SSITheatreProcedure.Parameters.AddWithValue("@BC_Bundles_SSI_TheatreProcedure", Session["Theatre"].ToString());
                  DataTable DataTable_SSITheatreProcedure;
                  using (DataTable_SSITheatreProcedure = new DataTable())
                  {
                    DataTable_SSITheatreProcedure.Locale = CultureInfo.CurrentCulture;
                    DataTable_SSITheatreProcedure = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SSITheatreProcedure).Copy();
                    if (DataTable_SSITheatreProcedure.Rows.Count > 0)
                    {
                      foreach (DataRow DataRow_Row1 in DataTable_SSITheatreProcedure.Rows)
                      {
                        Session["BCBundlesId"] = DataRow_Row1["BC_Bundles_Id"];
                      }
                    }
                  }
                }


                if (string.IsNullOrEmpty(Session["BCBundlesId"].ToString()))
                {
                  DropDownList_EditSSITheatreProcedure.Items.Insert(a, new ListItem(Convert.ToString("NO : " + Session["Theatre"].ToString(), CultureInfo.CurrentCulture), Session["Theatre"].ToString()));
                  a = a + 1;
                }
                else
                {
                  if (Session["Theatre"].ToString() != HiddenField_EditSSITheatreProcedure.Value.ToString())
                  {
                    DropDownList_EditSSITheatreProcedure.Items.Insert(a, new ListItem(Convert.ToString("YES : " + Session["Theatre"].ToString(), CultureInfo.CurrentCulture), Session["Theatre"].ToString()));
                    a = a + 1;
                  }
                }

                Session["BCBundlesId"] = "";
              }
            }
          }
        }
      }

      Session["Theatre"] = "";
      Session["TheatreDate"] = "";
      Session["TheatreProcedure"] = "";
      Session["Error"] = "";
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


    //--START-- --TableList--//
    protected void SqlDataSource_BundleCompliance_Bundles_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_BundleCompliance_Bundles.PageSize = Convert.ToInt32(((DropDownList)GridView_BundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_BundleCompliance_Bundles.PageIndex = ((DropDownList)GridView_BundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_BundleCompliance_Bundles_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_BundleCompliance_Bundles.PageSize <= 20)
        {
          ((DropDownList)GridView_BundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_BundleCompliance_Bundles.PageSize > 20 && GridView_BundleCompliance_Bundles.PageSize <= 50)
        {
          ((DropDownList)GridView_BundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_BundleCompliance_Bundles.PageSize > 50 && GridView_BundleCompliance_Bundles.PageSize <= 100)
        {
          ((DropDownList)GridView_BundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_BundleCompliance_Bundles_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_BundleCompliance_Bundles.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_BundleCompliance_Bundles.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_BundleCompliance_Bundles.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_BundleCompliance_Bundles_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance New Form", "Form_BundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_BundleCompliance_PatientVisitNumber=" + Request.QueryString["s_BundleCompliance_PatientVisitNumber"] + ""), false);
    }

    public string GetLink(object bc_Bundles_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "" +
          "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Form", "Form_BundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_BundleCompliance_PatientVisitNumber=" + Request.QueryString["s_BundleCompliance_PatientVisitNumber"] + "&BC_Bundles_Id=" + bc_Bundles_Id + "&ViewMode=0") + "'>View</a>&nbsp;/&nbsp;" +
          "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Form", "Form_BundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_BundleCompliance_PatientVisitNumber=" + Request.QueryString["s_BundleCompliance_PatientVisitNumber"] + "&BC_Bundles_Id=" + bc_Bundles_Id + "&ViewMode=1") + "'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "" +
          "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Bundle Compliance Form", "Form_BundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_BundleCompliance_PatientVisitNumber=" + Request.QueryString["s_BundleCompliance_PatientVisitNumber"] + "&BC_Bundles_Id=" + bc_Bundles_Id + "&ViewMode=0") + "'>View</a>";
        }
      }

      string FinalURL = LinkURL;

      return FinalURL;
    }
    //---END--- --TableList--//
  }
}