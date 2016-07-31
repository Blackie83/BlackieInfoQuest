using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_RehabBundleCompliance : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_RehabBundleCompliance, this.GetType(), "UpdateProgress_Start", "Validation_Search();Validation_Form();Calculation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          PageTitle();

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] != null)
          {
            TablePatientInfo.Visible = true;
            TableForm.Visible = true;
            TableList.Visible = true;

            SetFormVisibility();

            SqlDataSource_RehabBundleCompliance_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
            SqlDataSource_RehabBundleCompliance_Facility.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_RehabBundleCompliance_Bundles";
            SqlDataSource_RehabBundleCompliance_Facility.SelectParameters["TableWHERE"].DefaultValue = "Facility_Id = " + Request.QueryString["s_Facility_Id"] + " AND RBC_Bundles_PatientVisitNumber = " + Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] + " ";

            if (string.IsNullOrEmpty(Request.QueryString["RBC_Bundles_Id"]))
            {
              form_RehabBundleCompliance.DefaultButton = Button_Search.UniqueID;

              if (((HiddenField)FormView_RehabBundleCompliance_Form.FindControl("HiddenField_Insert")) != null)
              {
                SqlDataSource_RehabBundleCompliance_InsertUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
                ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).Items.Clear();
                SqlDataSource_RehabBundleCompliance_InsertUnit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
                ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
                ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).DataBind();

                ((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_InsertDate")).Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
              }
            }
            else
            {
              form_RehabBundleCompliance.DefaultButton = null;

              if (Request.QueryString["ViewMode"] == "0")
              {
              }
              else if (Request.QueryString["ViewMode"] == "1")
              {
                SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
                SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
                SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_RehabBundleCompliance_Bundles";
                SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters["TableWHERE"].DefaultValue = "RBC_Bundles_Id = " + Request.QueryString["RBC_Bundles_Id"] + " ";

                SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters["TableSELECT"].DefaultValue = "RBC_Bundles_Assessed_List";
                SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_RehabBundleCompliance_Bundles";
                SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters["TableWHERE"].DefaultValue = "RBC_Bundles_Id = " + Request.QueryString["RBC_Bundles_Id"] + " ";

                SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters["TableSELECT"].DefaultValue = "RBC_Bundles_IUC_5_Catheter_List";
                SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_RehabBundleCompliance_Bundles";
                SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters["TableWHERE"].DefaultValue = "RBC_Bundles_Id = " + Request.QueryString["RBC_Bundles_Id"] + " ";

                SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters["TableSELECT"].DefaultValue = "RBC_Bundles_SPC_5_Catheter_List";
                SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_RehabBundleCompliance_Bundles";
                SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters["TableWHERE"].DefaultValue = "RBC_Bundles_Id = " + Request.QueryString["RBC_Bundles_Id"] + " ";
              }
            }
          }
          else
          {
            form_RehabBundleCompliance.DefaultButton = Button_Search.UniqueID;

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
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('35'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('35')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("35");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_RehabBundleCompliance.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Rehab Bundle Compliance", "8");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_RehabBundleCompliance_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_RehabBundleCompliance_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_InsertUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_InsertUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_RehabBundleCompliance_InsertUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_InsertUnit.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_InsertUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_RehabBundleCompliance_InsertUnit.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_InsertUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_InsertUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_InsertUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_InsertUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_InsertAssessedList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_InsertAssessedList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_RehabBundleCompliance_InsertAssessedList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_InsertAssessedList.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_InsertAssessedList.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_InsertAssessedList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "108");
      SqlDataSource_RehabBundleCompliance_InsertAssessedList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_RehabBundleCompliance_InsertAssessedList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_InsertAssessedList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_InsertAssessedList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "109");
      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_InsertIUC5CatheterList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "109");
      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_InsertSPC5CatheterList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_EditUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_EditUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_RehabBundleCompliance_EditUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters.Add("Facility_Id", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_EditAssessedList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "108");
      SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_EditAssessedList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "109");
      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_EditIUC5CatheterList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters.Add("Form_Id", TypeCode.String, "35");
      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "109");
      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_RehabBundleCompliance_EditSPC5CatheterList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_RehabBundleCompliance_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_Form.InsertCommand="INSERT INTO InfoQuest_Form_RehabBundleCompliance_Bundles (Facility_Id ,	RBC_Bundles_PatientVisitNumber ,  RBC_Bundles_ReportNumber ,	RBC_Bundles_Assessed_List ,	RBC_Bundles_Date ,	Unit_Id ,	RBC_Bundles_IUC_SelectAll ,	RBC_Bundles_IUC_1 ,	RBC_Bundles_IUC_2 ,	RBC_Bundles_IUC_3 ,	RBC_Bundles_IUC_4 ,	RBC_Bundles_IUC_5 , RBC_Bundles_IUC_5_Catheter_List ,	RBC_Bundles_IUC_Cal ,	RBC_Bundles_IUC_1_NA ,	RBC_Bundles_IUC_2_NA ,	RBC_Bundles_IUC_3_NA ,	RBC_Bundles_IUC_4_NA ,	RBC_Bundles_IUC_5_NA ,	RBC_Bundles_SPC_SelectAll ,	RBC_Bundles_SPC_1 ,	RBC_Bundles_SPC_2 ,	RBC_Bundles_SPC_3 ,	RBC_Bundles_SPC_4 ,	RBC_Bundles_SPC_5 , RBC_Bundles_SPC_5_Catheter_List ,	RBC_Bundles_SPC_Cal ,	RBC_Bundles_SPC_1_NA ,	RBC_Bundles_SPC_2_NA ,	RBC_Bundles_SPC_3_NA ,	RBC_Bundles_SPC_4_NA ,	RBC_Bundles_SPC_5_NA ,	RBC_Bundles_ICC_SelectAll ,	RBC_Bundles_ICC_1 ,	RBC_Bundles_ICC_2 ,	RBC_Bundles_ICC_3 ,	RBC_Bundles_ICC_4 ,	RBC_Bundles_ICC_Cal ,	RBC_Bundles_ICC_1_NA ,	RBC_Bundles_ICC_2_NA ,	RBC_Bundles_ICC_3_NA ,	RBC_Bundles_ICC_4_NA ,	RBC_Bundles_CreatedDate ,	RBC_Bundles_CreatedBy ,	RBC_Bundles_ModifiedDate ,	RBC_Bundles_ModifiedBy ,	RBC_Bundles_History ,	RBC_Bundles_IsActive) VALUES (@Facility_Id ,@RBC_Bundles_PatientVisitNumber ,@RBC_Bundles_ReportNumber ,@RBC_Bundles_Assessed_List ,@RBC_Bundles_Date ,@Unit_Id ,@RBC_Bundles_IUC_SelectAll ,@RBC_Bundles_IUC_1 ,@RBC_Bundles_IUC_2 ,@RBC_Bundles_IUC_3 ,@RBC_Bundles_IUC_4 ,@RBC_Bundles_IUC_5 ,@RBC_Bundles_IUC_5_Catheter_List ,@RBC_Bundles_IUC_Cal ,@RBC_Bundles_IUC_1_NA ,@RBC_Bundles_IUC_2_NA ,@RBC_Bundles_IUC_3_NA ,@RBC_Bundles_IUC_4_NA ,@RBC_Bundles_IUC_5_NA ,@RBC_Bundles_SPC_SelectAll ,@RBC_Bundles_SPC_1 ,@RBC_Bundles_SPC_2 ,@RBC_Bundles_SPC_3 ,@RBC_Bundles_SPC_4 ,@RBC_Bundles_SPC_5 ,@RBC_Bundles_SPC_5_Catheter_List ,@RBC_Bundles_SPC_Cal ,@RBC_Bundles_SPC_1_NA ,@RBC_Bundles_SPC_2_NA ,@RBC_Bundles_SPC_3_NA ,@RBC_Bundles_SPC_4_NA ,@RBC_Bundles_SPC_5_NA ,@RBC_Bundles_ICC_SelectAll ,@RBC_Bundles_ICC_1 ,@RBC_Bundles_ICC_2 ,@RBC_Bundles_ICC_3 ,@RBC_Bundles_ICC_4 ,@RBC_Bundles_ICC_Cal ,@RBC_Bundles_ICC_1_NA ,@RBC_Bundles_ICC_2_NA ,@RBC_Bundles_ICC_3_NA ,@RBC_Bundles_ICC_4_NA ,@RBC_Bundles_CreatedDate ,@RBC_Bundles_CreatedBy ,@RBC_Bundles_ModifiedDate ,@RBC_Bundles_ModifiedBy ,@RBC_Bundles_History ,@RBC_Bundles_IsActive); SELECT @RBC_Bundles_Id = SCOPE_IDENTITY()";
      SqlDataSource_RehabBundleCompliance_Form.SelectCommand="SELECT * FROM InfoQuest_Form_RehabBundleCompliance_Bundles WHERE (RBC_Bundles_Id = @RBC_Bundles_Id)";
      SqlDataSource_RehabBundleCompliance_Form.UpdateCommand="UPDATE InfoQuest_Form_RehabBundleCompliance_Bundles SET RBC_Bundles_Assessed_List = @RBC_Bundles_Assessed_List ,	RBC_Bundles_Date = @RBC_Bundles_Date ,	Unit_Id = @Unit_Id ,	RBC_Bundles_IUC_SelectAll = @RBC_Bundles_IUC_SelectAll ,	RBC_Bundles_IUC_1 = @RBC_Bundles_IUC_1 ,	RBC_Bundles_IUC_2 = @RBC_Bundles_IUC_2 ,	RBC_Bundles_IUC_3 = @RBC_Bundles_IUC_3 ,	RBC_Bundles_IUC_4 = @RBC_Bundles_IUC_4 ,	RBC_Bundles_IUC_5 = @RBC_Bundles_IUC_5 , RBC_Bundles_IUC_5_Catheter_List = @RBC_Bundles_IUC_5_Catheter_List ,	RBC_Bundles_IUC_Cal = @RBC_Bundles_IUC_Cal ,	RBC_Bundles_IUC_1_NA = @RBC_Bundles_IUC_1_NA ,	RBC_Bundles_IUC_2_NA = @RBC_Bundles_IUC_2_NA ,	RBC_Bundles_IUC_3_NA = @RBC_Bundles_IUC_3_NA ,	RBC_Bundles_IUC_4_NA = @RBC_Bundles_IUC_4_NA ,	RBC_Bundles_IUC_5_NA = @RBC_Bundles_IUC_5_NA ,	RBC_Bundles_SPC_SelectAll = @RBC_Bundles_SPC_SelectAll ,	RBC_Bundles_SPC_1 = @RBC_Bundles_SPC_1 ,	RBC_Bundles_SPC_2 = @RBC_Bundles_SPC_2 ,	RBC_Bundles_SPC_3 = @RBC_Bundles_SPC_3 ,	RBC_Bundles_SPC_4 = @RBC_Bundles_SPC_4 ,	RBC_Bundles_SPC_5 = @RBC_Bundles_SPC_5 , RBC_Bundles_SPC_5_Catheter_List = @RBC_Bundles_SPC_5_Catheter_List ,	RBC_Bundles_SPC_Cal = @RBC_Bundles_SPC_Cal ,	RBC_Bundles_SPC_1_NA = @RBC_Bundles_SPC_1_NA ,	RBC_Bundles_SPC_2_NA = @RBC_Bundles_SPC_2_NA ,	RBC_Bundles_SPC_3_NA = @RBC_Bundles_SPC_3_NA ,	RBC_Bundles_SPC_4_NA = @RBC_Bundles_SPC_4_NA ,	RBC_Bundles_SPC_5_NA = @RBC_Bundles_SPC_5_NA ,	RBC_Bundles_ICC_SelectAll = @RBC_Bundles_ICC_SelectAll ,	RBC_Bundles_ICC_1 = @RBC_Bundles_ICC_1 ,	RBC_Bundles_ICC_2 = @RBC_Bundles_ICC_2 ,	RBC_Bundles_ICC_3 = @RBC_Bundles_ICC_3 ,	RBC_Bundles_ICC_4 = @RBC_Bundles_ICC_4 ,	RBC_Bundles_ICC_Cal = @RBC_Bundles_ICC_Cal ,	RBC_Bundles_ICC_1_NA = @RBC_Bundles_ICC_1_NA ,	RBC_Bundles_ICC_2_NA = @RBC_Bundles_ICC_2_NA ,	RBC_Bundles_ICC_3_NA = @RBC_Bundles_ICC_3_NA ,	RBC_Bundles_ICC_4_NA = @RBC_Bundles_ICC_4_NA ,	RBC_Bundles_ModifiedDate = @RBC_Bundles_ModifiedDate ,	RBC_Bundles_ModifiedBy = @RBC_Bundles_ModifiedBy ,	RBC_Bundles_History = @RBC_Bundles_History ,	RBC_Bundles_IsActive = @RBC_Bundles_IsActive WHERE RBC_Bundles_Id = @RBC_Bundles_Id";
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Clear();
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_Id", TypeCode.Int32, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_PatientVisitNumber", TypeCode.Int32, Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"]);
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ReportNumber", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_Assessed_List", TypeCode.Int32, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_Date", TypeCode.DateTime, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_1", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_2", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_3", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_4", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_5", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_5_Catheter_List", TypeCode.Int32, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_Cal", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_1_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_2_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_3_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_4_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IUC_5_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_1", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_2", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_3", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_4", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_5", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_5_Catheter_List", TypeCode.Int32, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_Cal", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_1_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_2_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_3_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_4_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_SPC_5_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_1", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_2", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_3", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_4", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_Cal", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_1_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_2_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_3_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ICC_4_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_CreatedBy", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_ModifiedBy", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_History", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_RehabBundleCompliance_Form.InsertParameters.Add("RBC_Bundles_IsActive", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_Form.SelectParameters.Add("RBC_Bundles_Id", TypeCode.Int32, Request.QueryString["RBC_Bundles_Id"]);
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Clear();
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_Assessed_List", TypeCode.Int32, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_Date", TypeCode.DateTime, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_1", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_2", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_3", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_4", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_5", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_5_Catheter_List", TypeCode.Int32, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_Cal", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_1_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_2_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_3_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_4_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IUC_5_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_1", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_2", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_3", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_4", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_5", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_5_Catheter_List", TypeCode.Int32, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_Cal", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_1_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_2_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_3_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_4_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_SPC_5_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_SelectAll", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_1", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_2", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_3", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_4", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_Cal", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_1_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_2_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_3_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ICC_4_NA", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_ModifiedBy", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_History", TypeCode.String, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_IsActive", TypeCode.Boolean, "");
      SqlDataSource_RehabBundleCompliance_Form.UpdateParameters.Add("RBC_Bundles_Id", TypeCode.Int32, "");

      SqlDataSource_RehabBundleCompliance_Bundles.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_RehabBundleCompliance_Bundles.SelectCommand = "spForm_Get_RehabBundleCompliance_Bundles";
      SqlDataSource_RehabBundleCompliance_Bundles.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_RehabBundleCompliance_Bundles.CancelSelectOnNullParameter = false;
      SqlDataSource_RehabBundleCompliance_Bundles.SelectParameters.Clear();
      SqlDataSource_RehabBundleCompliance_Bundles.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_RehabBundleCompliance_Bundles.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_RehabBundleCompliance_Bundles.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("35")).ToString(), CultureInfo.CurrentCulture);
      Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("35").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
      Label_PatientInfoHeading.Text = Convert.ToString("Patient Information", CultureInfo.CurrentCulture);
      Label_InterventionsHeading.Text = Convert.ToString("Bundles", CultureInfo.CurrentCulture);
      Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("35").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
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
        if (Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"];
        }
      }
    }

    private void PatientDataPI()
    {
      DataTable DataTable_PatientDataPI;
      using (DataTable_PatientDataPI = new DataTable())
      {
        DataTable_PatientDataPI.Locale = CultureInfo.CurrentCulture;
        //DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"]).Copy();
        DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"]).Copy();

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
            Label_InvalidSearchMessage.Text = Convert.ToString("Patient Visit Number " + Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] + " does not Exist", CultureInfo.CurrentCulture);
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

              Session["RBCPIId"] = "";
              string SQLStringPatientInfo = "SELECT RBC_PI_Id FROM InfoQuest_Form_RehabBundleCompliance_PatientInformation WHERE Facility_Id = @Facility_Id AND RBC_PI_PatientVisitNumber = @RBC_PI_PatientVisitNumber";
              using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
              {
                SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_PatientInfo.Parameters.AddWithValue("@RBC_PI_PatientVisitNumber", Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"]);
                DataTable DataTable_PatientInfo;
                using (DataTable_PatientInfo = new DataTable())
                {
                  DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
                  DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
                  if (DataTable_PatientInfo.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                    {
                      Session["RBCPIId"] = DataRow_Row1["RBC_PI_Id"];
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(Session["RBCPIId"].ToString()))
              {
                string SQLStringInsertRBCPI = "INSERT INTO InfoQuest_Form_RehabBundleCompliance_PatientInformation ( Facility_Id , RBC_PI_PatientVisitNumber , RBC_PI_PatientName , RBC_PI_PatientAge , RBC_PI_PatientDateOfAdmission , RBC_PI_PatientDateofDischarge , RBC_PI_Archived ) VALUES  ( @Facility_Id , @RBC_PI_PatientVisitNumber , @RBC_PI_PatientName , @RBC_PI_PatientAge , @RBC_PI_PatientDateOfAdmission , @RBC_PI_PatientDateofDischarge , @RBC_PI_Archived )";
                using (SqlCommand SqlCommand_InsertRBCPI = new SqlCommand(SQLStringInsertRBCPI))
                {
                  SqlCommand_InsertRBCPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_InsertRBCPI.Parameters.AddWithValue("@RBC_PI_PatientVisitNumber", Session["PatientVisitNumber"].ToString());
                  SqlCommand_InsertRBCPI.Parameters.AddWithValue("@RBC_PI_PatientName", Session["PatientSurnameName"].ToString());
                  SqlCommand_InsertRBCPI.Parameters.AddWithValue("@RBC_PI_PatientAge", Session["PatientAge"].ToString());
                  SqlCommand_InsertRBCPI.Parameters.AddWithValue("@RBC_PI_PatientDateOfAdmission", Session["PatientDateOfAdmission"].ToString());
                  SqlCommand_InsertRBCPI.Parameters.AddWithValue("@RBC_PI_PatientDateofDischarge", Session["PatientDateOfDischarge"].ToString());
                  SqlCommand_InsertRBCPI.Parameters.AddWithValue("@RBC_PI_Archived", 0);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertRBCPI);
                }
              }
              else
              {
                string SQLStringUpdateRBCPI = "UPDATE InfoQuest_Form_RehabBundleCompliance_PatientInformation SET RBC_PI_PatientName = @RBC_PI_PatientName , RBC_PI_PatientAge = @RBC_PI_PatientAge , RBC_PI_PatientDateOfAdmission = @RBC_PI_PatientDateOfAdmission , RBC_PI_PatientDateofDischarge = @RBC_PI_PatientDateofDischarge WHERE Facility_Id = @Facility_Id AND RBC_PI_PatientVisitNumber = @RBC_PI_PatientVisitNumber ";
                using (SqlCommand SqlCommand_UpdateRBCPI = new SqlCommand(SQLStringUpdateRBCPI))
                {
                  SqlCommand_UpdateRBCPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_UpdateRBCPI.Parameters.AddWithValue("@RBC_PI_PatientVisitNumber", Session["PatientVisitNumber"].ToString());
                  SqlCommand_UpdateRBCPI.Parameters.AddWithValue("@RBC_PI_PatientName", Session["PatientSurnameName"].ToString());
                  SqlCommand_UpdateRBCPI.Parameters.AddWithValue("@RBC_PI_PatientAge", Session["PatientAge"].ToString());
                  SqlCommand_UpdateRBCPI.Parameters.AddWithValue("@RBC_PI_PatientDateOfAdmission", Session["PatientDateOfAdmission"].ToString());
                  SqlCommand_UpdateRBCPI.Parameters.AddWithValue("@RBC_PI_PatientDateofDischarge", Session["PatientDateOfDischarge"].ToString());
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateRBCPI);
                }
              }

              Session["RBCPIId"] = "";
            }
          }
        }
      }
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('35')) AND (Facility_Id IN (@Facility_Id) OR SecurityRole_Rank = 1)";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '142'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '143'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '144'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '145'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
            {
              Session["Security"] = "0";
              if (Request.QueryString["RBC_Bundles_Id"] != null)
              {
                if (Request.QueryString["ViewMode"] == "1")
                {
                  FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                  FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
              else
              {
                FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";
              if (Request.QueryString["RBC_Bundles_Id"] != null)
              {
                if (Request.QueryString["ViewMode"] == "1")
                {
                  FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                  FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
              else
              {
                FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFormAdminView.Length > 0)
            {
              Session["Security"] = "0";
              FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";
              if (Request.QueryString["RBC_Bundles_Id"] != null)
              {
                if (Request.QueryString["ViewMode"] == "1")
                {
                  FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                  FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
              else
              {
                FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminView.Length > 0)
            {
              Session["Security"] = "0";
              FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              FormView_RehabBundleCompliance_Form.ChangeMode(FormViewMode.ReadOnly);
            }
            Session["Security"] = "1";
          }
        }
      }
    }

    private void TablePatientInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["RBCPIPatientVisitNumber"] = "";
      Session["RBCPIPatientName"] = "";
      Session["RBCPIPatientAge"] = "";
      Session["RBCPIPatientDateOfAdmission"] = "";
      Session["RBCPIPatientDateofDischarge"] = "";

      string SQLStringPatientInfo = "SELECT DISTINCT Facility_FacilityDisplayName , RBC_PI_PatientVisitNumber , RBC_PI_PatientName , RBC_PI_PatientAge , RBC_PI_PatientDateOfAdmission , RBC_PI_PatientDateofDischarge FROM vForm_RehabBundleCompliance_PatientInformation WHERE Facility_Id = @Facility_Id AND RBC_PI_PatientVisitNumber = @RBC_PI_PatientVisitNumber";
      using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
      {
        SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PatientInfo.Parameters.AddWithValue("@RBC_PI_PatientVisitNumber", Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"]);
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
              Session["RBCPIPatientVisitNumber"] = DataRow_Row["RBC_PI_PatientVisitNumber"];
              Session["RBCPIPatientName"] = DataRow_Row["RBC_PI_PatientName"];
              Session["RBCPIPatientAge"] = DataRow_Row["RBC_PI_PatientAge"];
              Session["RBCPIPatientDateOfAdmission"] = DataRow_Row["RBC_PI_PatientDateOfAdmission"];
              Session["RBCPIPatientDateofDischarge"] = DataRow_Row["RBC_PI_PatientDateofDischarge"];
            }
          }
        }
      }

      Label_PIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_PIVisitNumber.Text = Session["RBCPIPatientVisitNumber"].ToString();
      Label_PIName.Text = Session["RBCPIPatientName"].ToString();
      Label_PIAge.Text = Session["RBCPIPatientAge"].ToString();
      Label_PIDateAdmission.Text = Session["RBCPIPatientDateOfAdmission"].ToString();
      Label_PIDateDischarge.Text = Session["RBCPIPatientDateofDischarge"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["RBCPIPatientVisitNumber"] = "";
      Session["RBCPIPatientName"] = "";
      Session["RBCPIPatientAge"] = "";
      Session["RBCPIPatientDateOfAdmission"] = "";
      Session["RBCPIPatientDateofDischarge"] = "";
    }

    private void TableFormVisible()
    {
      if (FormView_RehabBundleCompliance_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();");
        ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertAssessedList")).Attributes.Add("OnChange", "Validation_Form('AssessedList');Calculation_Form();ShowHide_Form();");

        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUCSelectAll")).Attributes.Add("OnClick", "Validation_Form('IUCSelectAll');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC1")).Attributes.Add("OnClick", "Validation_Form('IUC1');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC1NA")).Attributes.Add("OnClick", "Validation_Form('IUC1NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC2")).Attributes.Add("OnClick", "Validation_Form('IUC2');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC2NA")).Attributes.Add("OnClick", "Validation_Form('IUC2NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC3")).Attributes.Add("OnClick", "Validation_Form('IUC3');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC3NA")).Attributes.Add("OnClick", "Validation_Form('IUC3NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC4")).Attributes.Add("OnClick", "Validation_Form('IUC4');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC4NA")).Attributes.Add("OnClick", "Validation_Form('IUC4NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC5")).Attributes.Add("OnClick", "Validation_Form('IUC5');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC5NA")).Attributes.Add("OnClick", "Validation_Form('IUC5NA');Calculation_Form();ShowHide_Form();");
        ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertIUC5CatheterList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");

        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPCSelectAll")).Attributes.Add("OnClick", "Validation_Form('SPCSelectAll');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC1")).Attributes.Add("OnClick", "Validation_Form('SPC1');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC1NA")).Attributes.Add("OnClick", "Validation_Form('SPC1NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC2")).Attributes.Add("OnClick", "Validation_Form('SPC2');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC2NA")).Attributes.Add("OnClick", "Validation_Form('SPC2NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC3")).Attributes.Add("OnClick", "Validation_Form('SPC3');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC3NA")).Attributes.Add("OnClick", "Validation_Form('SPC3NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC4")).Attributes.Add("OnClick", "Validation_Form('SPC4');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC4NA")).Attributes.Add("OnClick", "Validation_Form('SPC4NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC5")).Attributes.Add("OnClick", "Validation_Form('SPC5');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC5NA")).Attributes.Add("OnClick", "Validation_Form('SPC5NA');Calculation_Form();ShowHide_Form();");
        ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertSPC5CatheterList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");

        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICCSelectAll")).Attributes.Add("OnClick", "Validation_Form('ICCSelectAll');Calculation_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC1")).Attributes.Add("OnClick", "Validation_Form('ICC1');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC1NA")).Attributes.Add("OnClick", "Validation_Form('ICC1NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC2")).Attributes.Add("OnClick", "Validation_Form('ICC2');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC2NA")).Attributes.Add("OnClick", "Validation_Form('ICC2NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC3")).Attributes.Add("OnClick", "Validation_Form('ICC3');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC3NA")).Attributes.Add("OnClick", "Validation_Form('ICC3NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC4")).Attributes.Add("OnClick", "Validation_Form('ICC4');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC4NA")).Attributes.Add("OnClick", "Validation_Form('ICC4NA');Calculation_Form();ShowHide_Form();");
      }

      if (FormView_RehabBundleCompliance_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditUnit")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();");
        ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditAssessedList")).Attributes.Add("OnChange", "Validation_Form('AssessedList');Calculation_Form();ShowHide_Form();");

        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUCSelectAll")).Attributes.Add("OnClick", "Validation_Form('IUCSelectAll');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC1")).Attributes.Add("OnClick", "Validation_Form('IUC1');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC1NA")).Attributes.Add("OnClick", "Validation_Form('IUC1NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC2")).Attributes.Add("OnClick", "Validation_Form('IUC2');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC2NA")).Attributes.Add("OnClick", "Validation_Form('IUC2NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC3")).Attributes.Add("OnClick", "Validation_Form('IUC3');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC3NA")).Attributes.Add("OnClick", "Validation_Form('IUC3NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC4")).Attributes.Add("OnClick", "Validation_Form('IUC4');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC4NA")).Attributes.Add("OnClick", "Validation_Form('IUC4NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC5")).Attributes.Add("OnClick", "Validation_Form('IUC5');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC5NA")).Attributes.Add("OnClick", "Validation_Form('IUC5NA');Calculation_Form();ShowHide_Form();");
        ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditIUC5CatheterList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");

        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPCSelectAll")).Attributes.Add("OnClick", "Validation_Form('SPCSelectAll');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC1")).Attributes.Add("OnClick", "Validation_Form('SPC1');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC1NA")).Attributes.Add("OnClick", "Validation_Form('SPC1NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC2")).Attributes.Add("OnClick", "Validation_Form('SPC2');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC2NA")).Attributes.Add("OnClick", "Validation_Form('SPC2NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC3")).Attributes.Add("OnClick", "Validation_Form('SPC3');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC3NA")).Attributes.Add("OnClick", "Validation_Form('SPC3NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC4")).Attributes.Add("OnClick", "Validation_Form('SPC4');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC4NA")).Attributes.Add("OnClick", "Validation_Form('SPC4NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC5")).Attributes.Add("OnClick", "Validation_Form('SPC5');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC5NA")).Attributes.Add("OnClick", "Validation_Form('SPC5NA');Calculation_Form();ShowHide_Form();");
        ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditSPC5CatheterList")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");

        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICCSelectAll")).Attributes.Add("OnClick", "Validation_Form('ICCSelectAll');Calculation_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC1")).Attributes.Add("OnClick", "Validation_Form('ICC1');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC1NA")).Attributes.Add("OnClick", "Validation_Form('ICC1NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC2")).Attributes.Add("OnClick", "Validation_Form('ICC2');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC2NA")).Attributes.Add("OnClick", "Validation_Form('ICC2NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC3")).Attributes.Add("OnClick", "Validation_Form('ICC3');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC3NA")).Attributes.Add("OnClick", "Validation_Form('ICC3NA');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC4")).Attributes.Add("OnClick", "Validation_Form('ICC4');Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC4NA")).Attributes.Add("OnClick", "Validation_Form('ICC4NA');Calculation_Form();ShowHide_Form();");
      }
    }


    //--START-- --Search--//
    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Form", "Form_RehabBundleCompliance.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Form", "Form_RehabBundleCompliance.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_RehabBundleCompliance_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + ""), false);
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
      string SearchField2 = Request.QueryString["Search_RehabBundleCompliancePatientVisitNumber"];
      string SearchField3 = Request.QueryString["Search_RehabBundleCompliancePatientName"];
      string SearchField4 = Request.QueryString["Search_RehabBundleComplianceReportNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_RehabBundleCompliance_PatientVisitNumber=" + Request.QueryString["Search_RehabBundleCompliancePatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_RehabBundleCompliance_PatientName=" + Request.QueryString["Search_RehabBundleCompliancePatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_RehabBundleCompliance_ReportNumber=" + Request.QueryString["Search_RehabBundleComplianceReportNumber"] + "&";
      }

      string FinalURL = "Form_RehabBundleCompliance_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance List", FinalURL);

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --TableForm--//
    protected void FormView_RehabBundleCompliance_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          Session["RBC_Bundles_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Request.QueryString["s_Facility_Id"], "35");

          SqlDataSource_RehabBundleCompliance_Form.InsertParameters["Unit_Id"].DefaultValue = ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).SelectedValue;

          SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_ReportNumber"].DefaultValue = Session["RBC_Bundles_ReportNumber"].ToString();
          SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_History"].DefaultValue = "";
          SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_IsActive"].DefaultValue = "true";

          if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertAssessedList")).SelectedValue != "4383")
          {
            SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_IUC_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal IUC_Total;
            IUC_Total = 0;
            Decimal IUC_Selected;
            IUC_Selected = 0;

            for (int a = 1; a <= 5; a++)
            {
              if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC" + a + "")).Checked == true)
              {
                IUC_Total = IUC_Total + 1;
              }
              else
              {
                IUC_Total = IUC_Total + 0;
              }

              if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC" + a + "NA")).Checked == false)
              {
                IUC_Selected = IUC_Selected + 1;
              }
              else
              {
                IUC_Selected = IUC_Selected + 0;
              }
            }

            Decimal IUC_Cal = (IUC_Total * 100 / IUC_Selected);
            SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_IUC_Cal"].DefaultValue = Decimal.Round(IUC_Cal, MidpointRounding.ToEven) + " %";
          }


          if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertAssessedList")).SelectedValue != "4384")
          {
            SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_SPC_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal SPC_Total;
            SPC_Total = 0;
            Decimal SPC_Selected;
            SPC_Selected = 0;

            for (int a = 1; a <= 5; a++)
            {
              if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC" + a + "")).Checked == true)
              {
                SPC_Total = SPC_Total + 1;
              }
              else
              {
                SPC_Total = SPC_Total + 0;
              }

              if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC" + a + "NA")).Checked == false)
              {
                SPC_Selected = SPC_Selected + 1;
              }
              else
              {
                SPC_Selected = SPC_Selected + 0;
              }
            }

            Decimal SPC_Cal = (SPC_Total * 100 / SPC_Selected);
            SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_SPC_Cal"].DefaultValue = Decimal.Round(SPC_Cal, MidpointRounding.ToEven) + " %";
          }


          if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertAssessedList")).SelectedValue != "4385" && ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertAssessedList")).SelectedValue != "4394")
          {
            SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_ICC_Cal"].DefaultValue = "Not Assessed";
          }
          else
          {
            Decimal ICC_Total;
            ICC_Total = 0;
            Decimal ICC_Selected;
            ICC_Selected = 0;

            for (int a = 1; a <= 4; a++)
            {
              if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC" + a + "")).Checked == true)
              {
                ICC_Total = ICC_Total + 1;
              }
              else
              {
                ICC_Total = ICC_Total + 0;
              }

              if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertICC" + a + "NA")).Checked == false)
              {
                ICC_Selected = ICC_Selected + 1;
              }
              else
              {
                ICC_Selected = ICC_Selected + 0;
              }
            }

            Decimal ICC_Cal = (ICC_Total * 100 / ICC_Selected);
            SqlDataSource_RehabBundleCompliance_Form.InsertParameters["RBC_Bundles_ICC_Cal"].DefaultValue = Decimal.Round(ICC_Cal, MidpointRounding.ToEven) + " %";
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
        if (string.IsNullOrEmpty(((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_InsertDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertAssessedList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertAssessedList")).SelectedValue == "4383")
        {
          if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertIUC5")).Checked == true)
          {
            if (string.IsNullOrEmpty(((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertIUC5CatheterList")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }

        if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertAssessedList")).SelectedValue == "4384")
        {
          if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_InsertSPC5")).Checked == true)
          {
            if (string.IsNullOrEmpty(((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_InsertSPC5CatheterList")).SelectedValue))
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
        string DateToValidateDate = ((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_InsertDate")).Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Observation Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_InsertDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Observation dates allowed<br />";
          }
          else
          {
            Session["ValidCapture"] = "";
            Session["CutOffDay"] = "";
            string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 35),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@RBC_Bundles_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 35),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@RBC_Bundles_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 35) AS CutOffDay";
            using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
            {
              SqlCommand_ValidCapture.Parameters.AddWithValue("@RBC_Bundles_Date", PickedDate);
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

    protected void SqlDataSource_RehabBundleCompliance_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["RBC_Bundles_Id"] = e.Command.Parameters["@RBC_Bundles_Id"].Value;
        Session["RBC_Bundles_ReportNumber"] = e.Command.Parameters["@RBC_Bundles_ReportNumber"].Value;
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_RehabBundleCompliance&ReportNumber=" + Session["RBC_Bundles_ReportNumber"].ToString() + ""), false);
      }
    }


    protected void FormView_RehabBundleCompliance_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDRBCBundlesModifiedDate"] = e.OldValues["RBC_Bundles_ModifiedDate"];
        object OLDRBCBundlesModifiedDate = Session["OLDRBCBundlesModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDRBCBundlesModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareRehabBundleCompliance = (DataView)SqlDataSource_RehabBundleCompliance_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareRehabBundleCompliance = DataView_CompareRehabBundleCompliance[0];
        Session["DBRBCBundlesModifiedDate"] = Convert.ToString(DataRowView_CompareRehabBundleCompliance["RBC_Bundles_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBRBCBundlesModifiedBy"] = Convert.ToString(DataRowView_CompareRehabBundleCompliance["RBC_Bundles_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBRBCBundlesModifiedDate = Session["DBRBCBundlesModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBRBCBundlesModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          Page.MaintainScrollPositionOnPostBack = false;

          string Label_EditConcurrencyUpdateMessage = "" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBRBCBundlesModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>";

          ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Convert.ToString(Label_EditConcurrencyUpdateMessage, CultureInfo.CurrentCulture);
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
            ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["RBC_Bundles_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["RBC_Bundles_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];
            e.NewValues["Unit_Id"] = ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditUnit")).SelectedValue;

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_RehabBundleCompliance", "RBC_Bundles_Id = " + Request.QueryString["RBC_Bundles_Id"]);

            DataView DataView_RehabBundleCompliance = (DataView)SqlDataSource_RehabBundleCompliance_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_RehabBundleCompliance = DataView_RehabBundleCompliance[0];
            Session["RBCBundlesHistory"] = Convert.ToString(DataRowView_RehabBundleCompliance["RBC_Bundles_History"], CultureInfo.CurrentCulture);

            Session["RBCBundlesHistory"] = Session["History"].ToString() + Session["RBCBundlesHistory"].ToString();

            e.NewValues["RBC_Bundles_History"] = Session["RBCBundlesHistory"].ToString();

            Session["RBCBundlesHistory"] = "";
            Session["History"] = "";

            if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditAssessedList")).SelectedValue != "4383")
            {
              e.NewValues["RBC_Bundles_IUC_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal IUC_Total;
              IUC_Total = 0;
              Decimal IUC_Selected;
              IUC_Selected = 0;

              for (int a = 1; a <= 5; a++)
              {
                if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC" + a + "")).Checked == true)
                {
                  IUC_Total = IUC_Total + 1;
                }
                else
                {
                  IUC_Total = IUC_Total + 0;
                }

                if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC" + a + "NA")).Checked == false)
                {
                  IUC_Selected = IUC_Selected + 1;
                }
                else
                {
                  IUC_Selected = IUC_Selected + 0;
                }
              }

              Decimal IUC_Cal = (IUC_Total * 100 / IUC_Selected);
              e.NewValues["RBC_Bundles_IUC_Cal"] = Decimal.Round(IUC_Cal, MidpointRounding.ToEven) + " %";
            }


            if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditAssessedList")).SelectedValue != "4384")
            {
              e.NewValues["RBC_Bundles_SPC_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal SPC_Total;
              SPC_Total = 0;
              Decimal SPC_Selected;
              SPC_Selected = 0;

              for (int a = 1; a <= 5; a++)
              {
                if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC" + a + "")).Checked == true)
                {
                  SPC_Total = SPC_Total + 1;
                }
                else
                {
                  SPC_Total = SPC_Total + 0;
                }

                if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC" + a + "NA")).Checked == false)
                {
                  SPC_Selected = SPC_Selected + 1;
                }
                else
                {
                  SPC_Selected = SPC_Selected + 0;
                }
              }

              Decimal SPC_Cal = (SPC_Total * 100 / SPC_Selected);
              e.NewValues["RBC_Bundles_SPC_Cal"] = Decimal.Round(SPC_Cal, MidpointRounding.ToEven) + " %";
            }


            if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditAssessedList")).SelectedValue != "4385" && ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditAssessedList")).SelectedValue != "4394")
            {
              e.NewValues["RBC_Bundles_ICC_Cal"] = "Not Assessed";
            }
            else
            {
              Decimal ICC_Total;
              ICC_Total = 0;
              Decimal ICC_Selected;
              ICC_Selected = 0;

              for (int a = 1; a <= 4; a++)
              {
                if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC" + a + "")).Checked == true)
                {
                  ICC_Total = ICC_Total + 1;
                }
                else
                {
                  ICC_Total = ICC_Total + 0;
                }

                if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditICC" + a + "NA")).Checked == false)
                {
                  ICC_Selected = ICC_Selected + 1;
                }
                else
                {
                  ICC_Selected = ICC_Selected + 0;
                }
              }

              Decimal ICC_Cal = (ICC_Total * 100 / ICC_Selected);
              e.NewValues["RBC_Bundles_ICC_Cal"] = Decimal.Round(ICC_Cal, MidpointRounding.ToEven) + " %";
            }
          }
        }

        Session["OLDRBCBundlesModifiedDate"] = "";
        Session["DBRBCBundlesModifiedDate"] = "";
        Session["DBRBCBundlesModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_EditDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditAssessedList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditAssessedList")).SelectedValue == "4383")
        {
          if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditIUC5")).Checked == true)
          {
            if (string.IsNullOrEmpty(((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditIUC5CatheterList")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }

        if (((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditAssessedList")).SelectedValue == "4384")
        {
          if (((CheckBox)FormView_RehabBundleCompliance_Form.FindControl("CheckBox_EditSPC5")).Checked == true)
          {
            if (string.IsNullOrEmpty(((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditSPC5CatheterList")).SelectedValue))
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
        string DateToValidateDate = ((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_EditDate")).Text.ToString();
        DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

        if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Observation Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_RehabBundleCompliance_Form.FindControl("TextBox_EditDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future Observation dates allowed<br />";
          }
          else
          {
            DateTime PreviousDate = Convert.ToDateTime(((HiddenField)FormView_RehabBundleCompliance_Form.FindControl("HiddenField_EditDate")).Value, CultureInfo.CurrentCulture);

            if (PickedDate.CompareTo(PreviousDate) != 0)
            {
              Session["ValidCapture"] = "";
              Session["CutOffDay"] = "";
              string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 35),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@RBC_Bundles_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 35),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@RBC_Bundles_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 35) AS CutOffDay";
              using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
              {
                SqlCommand_ValidCapture.Parameters.AddWithValue("@RBC_Bundles_Date", PickedDate);
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

    protected void SqlDataSource_RehabBundleCompliance_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Form", "Form_RehabBundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_RehabBundleCompliance_PatientVisitNumber=" + Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] + ""), false);
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_RehabBundleCompliance, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Print", "InfoQuest_Print.aspx?PrintPage=Form_RehabBundleCompliance&PrintValue=" + Request.QueryString["RBC_Bundles_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_RehabBundleCompliance, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);

          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_RehabBundleCompliance, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Email", "InfoQuest_Email.aspx?EmailPage=Form_RehabBundleCompliance&EmailValue=" + Request.QueryString["RBC_Bundles_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_RehabBundleCompliance, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }


    protected void FormView_RehabBundleCompliance_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["RBC_Bundles_Id"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Form", "Form_RehabBundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_RehabBundleCompliance_PatientVisitNumber=" + Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] + ""), false);
          }
        }
      }
    }

    protected void FormView_RehabBundleCompliance_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_RehabBundleCompliance_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_RehabBundleCompliance_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      if (Request.QueryString["RBC_Bundles_Id"] != null)
      {
        DataView DataView_UnitId = (DataView)SqlDataSource_RehabBundleCompliance_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_UnitId = DataView_UnitId[0];
        ((DropDownList)FormView_RehabBundleCompliance_Form.FindControl("DropDownList_EditUnit")).SelectedValue = Convert.ToString(DataRowView_UnitId["Unit_Id"], CultureInfo.CurrentCulture);
        SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_Facility_Id"];
        SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters["TableSELECT"].DefaultValue = "Unit_Id";
        SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_RehabBundleCompliance_Bundles";
        SqlDataSource_RehabBundleCompliance_EditUnit.SelectParameters["TableWHERE"].DefaultValue = "RBC_Bundles_Id = " + Request.QueryString["RBC_Bundles_Id"] + " ";

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 35";
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
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["RBC_Bundles_Id"] != null)
      {
        Session["UnitName"] = "";
        Session["RBCBundlesAssessedName"] = "";
        Session["RBCBundlesIUC5CatheterName"] = "";
        Session["RBCBundlesSPC5CatheterName"] = "";
        string SQLStringRehabBundleCompliance = "SELECT Unit_Name , RBC_Bundles_Assessed_Name , RBC_Bundles_IUC_5_Catheter_Name , RBC_Bundles_SPC_5_Catheter_Name FROM vForm_RehabBundleCompliance WHERE RBC_Bundles_Id = @RBC_Bundles_Id";
        using (SqlCommand SqlCommand_RehabBundleCompliance = new SqlCommand(SQLStringRehabBundleCompliance))
        {
          SqlCommand_RehabBundleCompliance.Parameters.AddWithValue("@RBC_Bundles_Id", Request.QueryString["RBC_Bundles_Id"]);
          DataTable DataTable_RehabBundleCompliance;
          using (DataTable_RehabBundleCompliance = new DataTable())
          {
            DataTable_RehabBundleCompliance.Locale = CultureInfo.CurrentCulture;
            DataTable_RehabBundleCompliance = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_RehabBundleCompliance).Copy();
            if (DataTable_RehabBundleCompliance.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_RehabBundleCompliance.Rows)
              {
                Session["UnitName"] = DataRow_Row["Unit_Name"];
                Session["RBCBundlesAssessedName"] = DataRow_Row["RBC_Bundles_Assessed_Name"];
                Session["RBCBundlesIUC5CatheterName"] = DataRow_Row["RBC_Bundles_IUC_5_Catheter_Name"];
                Session["RBCBundlesSPC5CatheterName"] = DataRow_Row["RBC_Bundles_SPC_5_Catheter_Name"];
              }
            }
          }
        }

        ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_ItemUnit")).Text = Session["UnitName"].ToString();
        ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_ItemAssessedList")).Text = Session["RBCBundlesAssessedName"].ToString();
        ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_ItemIUC5CatheterList")).Text = Session["RBCBundlesIUC5CatheterName"].ToString();
        ((Label)FormView_RehabBundleCompliance_Form.FindControl("Label_ItemSPC5CatheterList")).Text = Session["RBCBundlesSPC5CatheterName"].ToString();

        Session["UnitsName"] = "";
        Session["RBCBundlesAssessedName"] = "";
        Session["RBCBundlesIUC5CatheterName"] = "";
        Session["RBCBundlesSPC5CatheterName"] = "";

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 35";
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
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Print", "InfoQuest_Print.aspx?PrintPage=Form_RehabBundleCompliance&PrintValue=" + Request.QueryString["RBC_Bundles_Id"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_RehabBundleCompliance_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Email", "InfoQuest_Email.aspx?EmailPage=Form_RehabBundleCompliance&EmailValue=" + Request.QueryString["RBC_Bundles_Id"] + "") + "')";
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
    //---END--- --TableForm--//


    //--START-- --TableList--//
    protected void SqlDataSource_RehabBundleCompliance_Bundles_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_RehabBundleCompliance_Bundles.PageSize = Convert.ToInt32(((DropDownList)GridView_RehabBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      GridView_RehabBundleCompliance_Bundles.PageIndex = ((DropDownList)GridView_RehabBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_RehabBundleCompliance_Bundles_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_RehabBundleCompliance_Bundles.PageSize <= 20)
        {
          ((DropDownList)GridView_RehabBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_RehabBundleCompliance_Bundles.PageSize > 20 && GridView_RehabBundleCompliance_Bundles.PageSize <= 50)
        {
          ((DropDownList)GridView_RehabBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_RehabBundleCompliance_Bundles.PageSize > 50 && GridView_RehabBundleCompliance_Bundles.PageSize <= 100)
        {
          ((DropDownList)GridView_RehabBundleCompliance_Bundles.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_RehabBundleCompliance_Bundles_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_RehabBundleCompliance_Bundles.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_RehabBundleCompliance_Bundles.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_RehabBundleCompliance_Bundles.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_RehabBundleCompliance_Bundles_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance New Form", "Form_RehabBundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_RehabBundleCompliance_PatientVisitNumber=" + Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] + ""), false);
    }

    public string GetLink(object rbc_Bundles_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "" +
          "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Form", "Form_RehabBundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_RehabBundleCompliance_PatientVisitNumber=" + Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] + "&RBC_Bundles_Id=" + rbc_Bundles_Id + "&ViewMode=0") + "'>View</a>&nbsp;/&nbsp;" +
          "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Form", "Form_RehabBundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_RehabBundleCompliance_PatientVisitNumber=" + Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] + "&RBC_Bundles_Id=" + rbc_Bundles_Id + "&ViewMode=1") + "'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "" +
          "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Rehab Bundle Compliance Form", "Form_RehabBundleCompliance.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_RehabBundleCompliance_PatientVisitNumber=" + Request.QueryString["s_RehabBundleCompliance_PatientVisitNumber"] + "&RBC_Bundles_Id=" + rbc_Bundles_Id + "&ViewMode=0") + "'>View</a>";
        }
      }

      string FinalURL = LinkURL;

      return FinalURL;
    }
    //---END--- --TableList--//
  }
}