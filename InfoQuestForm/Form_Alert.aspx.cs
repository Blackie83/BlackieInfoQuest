using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Alert : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_Alert, GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("2")).ToString();
          Label_FacilityHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("2")).ToString();
          Label_AlertHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("2")).ToString();

          if (Request.QueryString["s_Facility_Id"] != null)
          {
            TableFacility.Visible = false;
            TableForm.Visible = true;

            if (Request.QueryString["Alert_Id"] != null)
            {
              SqlDataSource_Alert_EditFacilityFrom.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
              SqlDataSource_Alert_EditFacilityFrom.SelectParameters["TableSELECT"].DefaultValue = "Alert_FacilityFrom_Facility";
              SqlDataSource_Alert_EditFacilityFrom.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_Alert";
              SqlDataSource_Alert_EditFacilityFrom.SelectParameters["TableWHERE"].DefaultValue = "Alert_Id = " + Request.QueryString["Alert_Id"] + " ";

              SqlDataSource_Alert_EditUnitFromUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
              SqlDataSource_Alert_EditUnitFromUnit.SelectParameters["TableSELECT"].DefaultValue = "Alert_UnitFrom_Unit";
              SqlDataSource_Alert_EditUnitFromUnit.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_Alert";
              SqlDataSource_Alert_EditUnitFromUnit.SelectParameters["TableWHERE"].DefaultValue = "Alert_Id = " + Request.QueryString["Alert_Id"] + " ";

              SqlDataSource_Alert_EditUnitToUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
              SqlDataSource_Alert_EditUnitToUnit.SelectParameters["TableSELECT"].DefaultValue = "Alert_UnitTo_Unit";
              SqlDataSource_Alert_EditUnitToUnit.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_Alert";
              SqlDataSource_Alert_EditUnitToUnit.SelectParameters["TableWHERE"].DefaultValue = "Alert_Id = " + Request.QueryString["Alert_Id"] + " ";

              SqlDataSource_Alert_EditLevel1List.SelectParameters["TableSELECT"].DefaultValue = "Alert_Level1_List";
              SqlDataSource_Alert_EditLevel1List.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_Alert";
              SqlDataSource_Alert_EditLevel1List.SelectParameters["TableWHERE"].DefaultValue = "Alert_Id = " + Request.QueryString["Alert_Id"] + " ";

              SqlDataSource_Alert_EditLevel2List.SelectParameters["TableSELECT"].DefaultValue = "Alert_Level2_List";
              SqlDataSource_Alert_EditLevel2List.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_Alert";
              SqlDataSource_Alert_EditLevel2List.SelectParameters["TableWHERE"].DefaultValue = "Alert_Id = " + Request.QueryString["Alert_Id"] + " ";
            }

            SetFormVisibility();
          }
          else
          {
            TableFacility.Visible = true;
            TableForm.Visible = false;
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
        if (Request.QueryString["Alert_Id"] == null)
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('2'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('2')) AND (Facility_Id IN (SELECT Facility_Id FROM InfoQuest_Form_Alert WHERE Alert_Id = @Alert_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Alert_Id", Request.QueryString["Alert_Id"]);

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          SecurityAllow = "1";
          //SecurityAllow = "0";
          //Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
          //Response.End();
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("2");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_Alert.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Alert", "11");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Alert_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Alert_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_Facility.SelectParameters.Clear();
      SqlDataSource_Alert_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Alert_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Alert_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_InsertFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_InsertFacility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Alert_InsertFacility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_InsertFacility.SelectParameters.Clear();
      SqlDataSource_Alert_InsertFacility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Alert_InsertFacility.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_InsertFacility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Alert_InsertFacility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_InsertFacility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_InsertFacility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_InsertFacilityFrom.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_InsertFacilityFrom.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Alert_InsertFacilityFrom.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_InsertFacilityFrom.SelectParameters.Clear();
      SqlDataSource_Alert_InsertFacilityFrom.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Alert_InsertFacilityFrom.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_InsertFacilityFrom.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Alert_InsertFacilityFrom.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_InsertFacilityFrom.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_InsertFacilityFrom.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_InsertUnitFromUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_InsertUnitFromUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Alert_InsertUnitFromUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_InsertUnitFromUnit.SelectParameters.Clear();
      SqlDataSource_Alert_InsertUnitFromUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Alert_InsertUnitFromUnit.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_InsertUnitFromUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Alert_InsertUnitFromUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_InsertUnitFromUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_InsertUnitFromUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_InsertUnitToUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_InsertUnitToUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Alert_InsertUnitToUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_InsertUnitToUnit.SelectParameters.Clear();
      SqlDataSource_Alert_InsertUnitToUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Alert_InsertUnitToUnit.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_InsertUnitToUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Alert_InsertUnitToUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_InsertUnitToUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_InsertUnitToUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_InsertLevel1List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_InsertLevel1List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Alert_InsertLevel1List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_InsertLevel1List.SelectParameters.Clear();
      SqlDataSource_Alert_InsertLevel1List.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_InsertLevel1List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "1");
      SqlDataSource_Alert_InsertLevel1List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Alert_InsertLevel1List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_InsertLevel1List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_InsertLevel1List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_InsertLevel2List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_InsertLevel2List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Alert_InsertLevel2List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_InsertLevel2List.SelectParameters.Clear();
      SqlDataSource_Alert_InsertLevel2List.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_InsertLevel2List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "2");
      SqlDataSource_Alert_InsertLevel2List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Alert_InsertLevel2List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_InsertLevel2List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_InsertLevel2List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_EditFacilityFrom.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_EditFacilityFrom.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Alert_EditFacilityFrom.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_EditFacilityFrom.SelectParameters.Clear();
      SqlDataSource_Alert_EditFacilityFrom.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, "");
      SqlDataSource_Alert_EditFacilityFrom.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_EditFacilityFrom.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Alert_EditFacilityFrom.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_EditFacilityFrom.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_EditFacilityFrom.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_EditUnitFromUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_EditUnitFromUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Alert_EditUnitFromUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_EditUnitFromUnit.SelectParameters.Clear();
      SqlDataSource_Alert_EditUnitFromUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, "");
      SqlDataSource_Alert_EditUnitFromUnit.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_EditUnitFromUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Alert_EditUnitFromUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_EditUnitFromUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_EditUnitFromUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_EditUnitToUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_EditUnitToUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_Alert_EditUnitToUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_EditUnitToUnit.SelectParameters.Clear();
      SqlDataSource_Alert_EditUnitToUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, "");
      SqlDataSource_Alert_EditUnitToUnit.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_EditUnitToUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Alert_EditUnitToUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_EditUnitToUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_EditUnitToUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_EditLevel1List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_EditLevel1List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Alert_EditLevel1List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_EditLevel1List.SelectParameters.Clear();
      SqlDataSource_Alert_EditLevel1List.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_EditLevel1List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "1");
      SqlDataSource_Alert_EditLevel1List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Alert_EditLevel1List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_EditLevel1List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_EditLevel1List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Alert_EditLevel2List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_EditLevel2List.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Alert_EditLevel2List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Alert_EditLevel2List.SelectParameters.Clear();
      SqlDataSource_Alert_EditLevel2List.SelectParameters.Add("Form_Id", TypeCode.String, "2");
      SqlDataSource_Alert_EditLevel2List.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "2");
      SqlDataSource_Alert_EditLevel2List.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Alert_EditLevel2List.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Alert_EditLevel2List.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Alert_EditLevel2List.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
      
      SqlDataSource_Alert_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Alert_Form.InsertCommand = "INSERT INTO [InfoQuest_Form_Alert] (Facility_Id , Alert_FacilityFrom_Facility , Alert_ReportNumber, Alert_Date ,Alert_Originator ,Alert_UnitFrom_Unit ,Alert_UnitTo_Unit ,Alert_Description ,Alert_ActionTaken ,Alert_Level1_List ,Alert_Level2_List ,Alert_NumberOfInstances ,Alert_Status ,Alert_StatusDate ,Alert_CreatedDate ,Alert_CreatedBy ,Alert_ModifiedDate ,Alert_ModifiedBy ,Alert_History ,Alert_Archived) VALUES (@Facility_Id , @Alert_FacilityFrom_Facility , @Alert_ReportNumber, @Alert_Date ,@Alert_Originator ,@Alert_UnitFrom_Unit ,@Alert_UnitTo_Unit ,@Alert_Description ,@Alert_ActionTaken ,@Alert_Level1_List ,@Alert_Level2_List ,@Alert_NumberOfInstances ,@Alert_Status ,@Alert_StatusDate ,@Alert_CreatedDate ,@Alert_CreatedBy ,@Alert_ModifiedDate ,@Alert_ModifiedBy ,@Alert_History ,@Alert_Archived); SELECT @Alert_Id = SCOPE_IDENTITY()";
      SqlDataSource_Alert_Form.SelectCommand="SELECT * FROM [InfoQuest_Form_Alert] WHERE ([Alert_Id] = @Alert_Id)";
      SqlDataSource_Alert_Form.UpdateCommand = "UPDATE [InfoQuest_Form_Alert] SET Alert_FacilityFrom_Facility = @Alert_FacilityFrom_Facility , Alert_Date = @Alert_Date ,Alert_Originator = @Alert_Originator ,Alert_UnitFrom_Unit = @Alert_UnitFrom_Unit ,Alert_UnitTo_Unit = @Alert_UnitTo_Unit ,Alert_Description = @Alert_Description ,Alert_ActionTaken = @Alert_ActionTaken ,Alert_Level1_List = @Alert_Level1_List ,Alert_Level2_List = @Alert_Level2_List ,Alert_NumberOfInstances = @Alert_NumberOfInstances ,Alert_Status = @Alert_Status ,Alert_StatusDate = @Alert_StatusDate ,Alert_StatusRejectedReason = @Alert_StatusRejectedReason ,Alert_ModifiedDate = @Alert_ModifiedDate ,Alert_ModifiedBy = @Alert_ModifiedBy ,Alert_History = @Alert_History WHERE [Alert_Id] = @Alert_Id";
      SqlDataSource_Alert_Form.InsertParameters.Clear();
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_Id", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.InsertParameters["Alert_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_Alert_Form.InsertParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_FacilityFrom_Facility", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_ReportNumber", TypeCode.String, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_Date", TypeCode.DateTime, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_Originator", TypeCode.String, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_UnitFrom_Unit", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_UnitTo_Unit", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_Description", TypeCode.String, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_ActionTaken", TypeCode.String, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_Level1_List", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_Level2_List", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_NumberOfInstances", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_Status", TypeCode.String, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_CreatedBy", TypeCode.String, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_History", TypeCode.String, "");
      SqlDataSource_Alert_Form.InsertParameters["Alert_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Alert_Form.InsertParameters.Add("Alert_Archived", TypeCode.Boolean, "");
      SqlDataSource_Alert_Form.SelectParameters.Clear();
      SqlDataSource_Alert_Form.SelectParameters.Add("Alert_Id", TypeCode.Int32, Request.QueryString["Alert_Id"]);
      SqlDataSource_Alert_Form.UpdateParameters.Clear();
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_FacilityFrom_Facility", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_Date", TypeCode.DateTime, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_Originator", TypeCode.String, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_UnitFrom_Unit", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_UnitTo_Unit", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_Description", TypeCode.String, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_ActionTaken", TypeCode.String, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_Level1_List", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_Level2_List", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_NumberOfInstances", TypeCode.Int32, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_Status", TypeCode.String, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_StatusRejectedReason", TypeCode.String, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_History", TypeCode.String, "");
      SqlDataSource_Alert_Form.UpdateParameters.Add("Alert_Id", TypeCode.Int32, "");
    }

    private void SetFormVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["Alert_Id"]))
      {
        SetFormVisibility_Insert();
      }
      else
      {
        SetFormVisibility_Edit();
      }
    }

    protected void SetFormVisibility_Insert()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('2')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '21'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '133'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '6'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '134'");
            DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '5'");
            DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '69'");

            string Security = "1";
            if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityApprover.Length > 0 || SecurityFacilityCapturer.Length > 0))
            {
              Security = "0";
              FormView_Alert_Form.ChangeMode(FormViewMode.Insert);
            }

            if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Security = "0";
              FormView_Alert_Form.ChangeMode(FormViewMode.Insert);
            }

            if (Security == "1")
            {
              Security = "0";
              FormView_Alert_Form.ChangeMode(FormViewMode.Insert);
            }
          }
          else
          {
            FormView_Alert_Form.ChangeMode(FormViewMode.Insert);
          }
        }
      }
    }

    protected void SetFormVisibility_Edit()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('2')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '21'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '133'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '6'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '134'");
            DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '5'");
            DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '69'");

            Session["AlertStatus"] = "";
            Session["ViewUpdate"] = "";
            string SQLStringAlert = "SELECT Alert_Status , CASE WHEN DATEADD(DAY	,0 - (DAY(DATEADD(MONTH,1,Alert_StatusDate))) + (SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,DATEADD(MONTH,1,Alert_StatusDate))+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 2),DATEADD(MONTH,1,Alert_StatusDate)) >= GETDATE() OR Alert_Status = 'Pending Approval' THEN 'Yes' ELSE 'No' END AS ViewUpdate FROM InfoQuest_Form_Alert WHERE Alert_Id = @Alert_Id";
            using (SqlCommand SqlCommand_Alert = new SqlCommand(SQLStringAlert))
            {
              SqlCommand_Alert.Parameters.AddWithValue("@Alert_Id", Request.QueryString["Alert_Id"]);
              DataTable DataTable_Alert;
              using (DataTable_Alert = new DataTable())
              {
                DataTable_Alert.Locale = CultureInfo.CurrentCulture;
                DataTable_Alert = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Alert).Copy();
                if (DataTable_Alert.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_Alert.Rows)
                  {
                    Session["AlertStatus"] = DataRow_Row["Alert_Status"];
                    Session["ViewUpdate"] = DataRow_Row["ViewUpdate"];
                  }
                }
              }
            }

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              FormView_Alert_Form.ChangeMode(FormViewMode.Edit);
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityCapturer.Length > 0))
            {
              Session["Security"] = "0";
              FormView_Alert_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";
              if (Session["ViewUpdate"].ToString() == "Yes")
              {
                if (Session["AlertStatus"].ToString() == "Rejected")
                {
                  FormView_Alert_Form.ChangeMode(FormViewMode.ReadOnly);
                }
                else
                {
                  FormView_Alert_Form.ChangeMode(FormViewMode.Edit);
                }
              }
              else
              {
                FormView_Alert_Form.ChangeMode(FormViewMode.ReadOnly);
              }
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityApprover.Length > 0)
            {
              Session["Security"] = "0";
              if (Session["AlertStatus"].ToString() == "Pending Approval")
              {
                FormView_Alert_Form.ChangeMode(FormViewMode.Edit);
              }
              else
              {
                FormView_Alert_Form.ChangeMode(FormViewMode.ReadOnly);
              }
            }

            Session["Security"] = "1";

            Session["AlertStatus"] = "";
            Session["ViewUpdate"] = "";
          }
          else
          {
            FormView_Alert_Form.ChangeMode(FormViewMode.ReadOnly);
          }
        }
      }
    }

    private void TableFormVisible()
    {
      if (FormView_Alert_Form.CurrentMode == FormViewMode.Insert)
      {
        string Alert_Status = "";
        string SQLStringFormStatus = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('2')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
        using (SqlCommand SqlCommand_FormStatus = new SqlCommand(SQLStringFormStatus))
        {
          SqlCommand_FormStatus.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_FormStatus.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
          DataTable DataTable_FormStatus;
          using (DataTable_FormStatus = new DataTable())
          {
            DataTable_FormStatus.Locale = CultureInfo.CurrentCulture;
            DataTable_FormStatus = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormStatus).Copy();
            if (DataTable_FormStatus.Rows.Count > 0)
            {
              DataRow[] SecurityAdmin = DataTable_FormStatus.Select("SecurityRole_Id = '1'");
              DataRow[] SecurityFormAdminUpdate = DataTable_FormStatus.Select("SecurityRole_Id = '21'");
              DataRow[] SecurityFormAdminView = DataTable_FormStatus.Select("SecurityRole_Id = '133'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormStatus.Select("SecurityRole_Id = '6'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormStatus.Select("SecurityRole_Id = '134'");
              DataRow[] SecurityFacilityApprover = DataTable_FormStatus.Select("SecurityRole_Id = '5'");
              DataRow[] SecurityFacilityCapturer = DataTable_FormStatus.Select("SecurityRole_Id = '69'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
              {
                Session["Security"] = "0";
                Alert_Status = "Approved";
              }

              if (Session["Security"].ToString() == "1" && SecurityFormAdminUpdate.Length > 0)
              {
                Session["Security"] = "0";
                Alert_Status = "Approved";
              }

              if (Session["Security"].ToString() == "1" && SecurityFormAdminView.Length > 0)
              {
                Session["Security"] = "0";
                Alert_Status = "Pending Approval";
              }

              if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
              {
                Session["Security"] = "0";
                Alert_Status = "Approved";
              }

              if (Session["Security"].ToString() == "1" && SecurityFacilityAdminView.Length > 0)
              {
                Session["Security"] = "0";
                Alert_Status = "Pending Approval";
              }

              if (Session["Security"].ToString() == "1" && SecurityFacilityApprover.Length > 0)
              {
                Session["Security"] = "0";
                Alert_Status = "Approved";
              }

              if (Session["Security"].ToString() == "1" && SecurityFacilityCapturer.Length > 0)
              {
                Alert_Status = "Pending Approval";
              }
              Session["Security"] = "1";
            }
            else
            {
              Alert_Status = "Pending Approval";
            }
          }
        }

        ((Label)FormView_Alert_Form.FindControl("Label_InsertStatus")).Text = Convert.ToString(Alert_Status, CultureInfo.CurrentCulture);

        ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertFacility")).SelectedValue = Request.QueryString["s_Facility_Id"];
        ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertFacilityFrom")).SelectedValue = Request.QueryString["s_Facility_Id"];

        ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel2List")).Items.Clear();
        ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel2List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 2", CultureInfo.CurrentCulture), ""));

        TableFormVisible_Insert();
      }

      if (FormView_Alert_Form.CurrentMode == FormViewMode.Edit)
      {
        TableFormVisible_Edit();
      }
    }

    private void TableFormVisible_Insert()
    {
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertFacilityFrom")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertOriginator")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertOriginator")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertUnitFromUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertUnitToUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel1List")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel2List")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertNumberOfInstances")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertNumberOfInstances")).Attributes.Add("OnInput", "Validation_Form();");
    }

    private void TableFormVisible_Edit()
    {
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditFacilityFrom")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditOriginator")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditOriginator")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditUnitFromUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditUnitToUnit")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel1List")).Attributes.Add("OnChange", "Validation_Form();");
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel2List")).Attributes.Add("OnChange", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditNumberOfInstances")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditNumberOfInstances")).Attributes.Add("OnInput", "Validation_Form();");
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditStatus")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditStatusRejectedReason")).Attributes.Add("OnKeyUp", "Validation_Form();");
      ((TextBox)FormView_Alert_Form.FindControl("TextBox_EditStatusRejectedReason")).Attributes.Add("OnInput", "Validation_Form();");
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FacilityType"];
      string SearchField2 = Request.QueryString["Search_FacilityId"];
      string SearchField3 = Request.QueryString["Search_AlertReportNumber"];
      string SearchField4 = Request.QueryString["Search_AlertUnitToUnit"];
      string SearchField5 = Request.QueryString["Search_AlertStatus"];
      string SearchField6 = Request.QueryString["Search_AlertStatusDateFrom"];
      string SearchField7 = Request.QueryString["Search_AlertStatusDateTo"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Type=" + Request.QueryString["Search_FacilityType"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Alert_ReportNumber=" + Request.QueryString["Search_AlertReportNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_Alert_UnitToUnit=" + Request.QueryString["Search_AlertUnitToUnit"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_Alert_Status=" + Request.QueryString["Search_AlertStatus"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField6))
      {
        SearchField6 = "s_Alert_StatusDateFrom=" + Request.QueryString["Search_AlertStatusDateFrom"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField7))
      {
        SearchField7 = "s_Alert_StatusDateTo=" + Request.QueryString["Search_AlertStatusDateTo"] + "&";
      }

      string FinalURL = "Form_Alert_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    
    //--START-- --TableFacility--//
    protected void DropDownList_Facility_SelectedIndexChanged(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert New Form", "Form_Alert.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue + ""), false);
    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }
    //---END--- --TableFacility--//


    //--START-- --TableForm--//
    //--START-- --Insert--//
    protected void FormView_Alert_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_Alert.SetFocus(UpdatePanel_Alert);

          ((Label)FormView_Alert_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_Alert_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          Session["Alert_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Request.QueryString["s_Facility_Id"], "2");

          SqlDataSource_Alert_Form.InsertParameters["Alert_ReportNumber"].DefaultValue = Session["Alert_ReportNumber"].ToString();
          SqlDataSource_Alert_Form.InsertParameters["Alert_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Alert_Form.InsertParameters["Alert_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Alert_Form.InsertParameters["Alert_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Alert_Form.InsertParameters["Alert_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Alert_Form.InsertParameters["Alert_History"].DefaultValue = "";
          SqlDataSource_Alert_Form.InsertParameters["Alert_Archived"].DefaultValue = "false";

          SqlDataSource_Alert_Form.InsertParameters["Alert_Level2_List"].DefaultValue = ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel2List")).SelectedValue;

          string Alert_Status = "";
          string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('2')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
          using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
          {
            SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
            DataTable DataTable_FormMode;
            using (DataTable_FormMode = new DataTable())
            {
              DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
              DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
              if (DataTable_FormMode.Rows.Count > 0)
              {
                DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
                DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '21'");
                DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '133'");
                DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '6'");
                DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '134'");
                DataRow[] SecurityFacilityApprover = DataTable_FormMode.Select("SecurityRole_Id = '5'");
                DataRow[] SecurityFacilityCapturer = DataTable_FormMode.Select("SecurityRole_Id = '69'");

                Session["Security"] = "1";
                if (Session["Security"].ToString() == "1" && SecurityAdmin.Length > 0)
                {
                  Session["Security"] = "0";
                  Alert_Status = "Approved";
                }

                if (Session["Security"].ToString() == "1" && SecurityFormAdminUpdate.Length > 0)
                {
                  Session["Security"] = "0";
                  Alert_Status = "Approved";
                }

                if (Session["Security"].ToString() == "1" && SecurityFormAdminView.Length > 0)
                {
                  Session["Security"] = "0";
                  Alert_Status = "Pending Approval";
                }

                if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
                {
                  Session["Security"] = "0";
                  Alert_Status = "Approved";
                }

                if (Session["Security"].ToString() == "1" && SecurityFacilityAdminView.Length > 0)
                {
                  Session["Security"] = "0";
                  Alert_Status = "Pending Approval";
                }

                if (Session["Security"].ToString() == "1" && SecurityFacilityApprover.Length > 0)
                {
                  Session["Security"] = "0";
                  Alert_Status = "Approved";
                }

                if (Session["Security"].ToString() == "1" && SecurityFacilityCapturer.Length > 0)
                {
                  Alert_Status = "Pending Approval";
                }
                Session["Security"] = "1";
              }
              else
              {
                Alert_Status = "Pending Approval";
              }
            }
          }

          SqlDataSource_Alert_Form.InsertParameters["Alert_Status"].DefaultValue = Alert_Status;
          SqlDataSource_Alert_Form.InsertParameters["Alert_StatusDate"].DefaultValue = DateTime.Now.ToString();

          Session["Alert_ReportNumber"] = "";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        InvalidForm = InsertValidation_Alert(InvalidForm);

        InvalidForm = InsertValidation_AlertDetail(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = InsertFieldValidation();
      }

      return InvalidFormMessage;
    }

    protected string InsertValidation_Alert(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertFacility")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertFacilityFrom")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertDate")).Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertOriginator")).Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertUnitFromUnit")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertUnitToUnit")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string InsertValidation_AlertDetail(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertDescription")).Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel1List")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel2List")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string InsertFieldValidation()
    {
      string InvalidFormMessage = "";

      DateTime CurrentDate = DateTime.Now;
      DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Alert_Form.FindControl("TextBox_InsertDate")).Text);
      if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + "Not a valid date, date must be in the format yyyy/mm/dd<br />";
      }
      else
      {
        if (ValidatedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Alert_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["Alert_Id"] = e.Command.Parameters["@Alert_Id"].Value;
        Session["Alert_ReportNumber"] = e.Command.Parameters["@Alert_ReportNumber"].Value;
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_Alert&ReportNumber=" + Session["Alert_ReportNumber"].ToString() + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    protected void FormView_Alert_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDAlertModifiedDate"] = e.OldValues["Alert_ModifiedDate"];
        object OLDAlertModifiedDate = Session["OLDAlertModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDAlertModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareAlert = (DataView)SqlDataSource_Alert_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareAlert = DataView_CompareAlert[0];
        Session["DBAlertModifiedDate"] = Convert.ToString(DataRowView_CompareAlert["Alert_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBAlertModifiedBy"] = Convert.ToString(DataRowView_CompareAlert["Alert_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBAlertModifiedDate = Session["DBAlertModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBAlertModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_Alert.SetFocus(UpdatePanel_Alert);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBAlertModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_Alert_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Alert_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_Alert.SetFocus(UpdatePanel_Alert);
            ((Label)FormView_Alert_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Alert_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
            e.NewValues["Alert_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Alert_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Alert", "Alert_Id = " + Request.QueryString["Alert_Id"]);

            DataView DataView_Alert = (DataView)SqlDataSource_Alert_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Alert = DataView_Alert[0];
            Session["AlertHistory"] = Convert.ToString(DataRowView_Alert["Alert_History"], CultureInfo.CurrentCulture);

            Session["AlertHistory"] = Session["History"].ToString() + Session["AlertHistory"].ToString();
            e.NewValues["Alert_History"] = Session["AlertHistory"].ToString();

            Session["AlertHistory"] = "";
            Session["History"] = "";


            e.NewValues["Alert_Level1_List"] = ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel1List")).SelectedValue;
            e.NewValues["Alert_Level2_List"] = ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel2List")).SelectedValue;


            string DBStatus = e.OldValues["Alert_Status"].ToString();
            DropDownList DropDownList_EditStatus = (DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditStatus");

            if (DBStatus != DropDownList_EditStatus.SelectedValue)
            {
              if (DBStatus == "Pending Approval")
              {
                e.NewValues["Alert_StatusDate"] = DateTime.Now.ToString();
              }
            }
          }
        }

        Session["OLDAlertModifiedDate"] = "";
        Session["DBAlertModifiedDate"] = "";
        Session["DBAlertModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        InvalidForm = EditValidation_Alert(InvalidForm);

        InvalidForm = EditValidation_AlertDetail(InvalidForm);

        InvalidForm = EditValidation_Status(InvalidForm);
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = EditFieldValidation();
      }

      return InvalidFormMessage;
    }

    protected string EditValidation_Alert(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditFacilityFrom")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((TextBox)FormView_Alert_Form.FindControl("TextBox_EditDate")).Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((TextBox)FormView_Alert_Form.FindControl("TextBox_EditOriginator")).Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditUnitFromUnit")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditUnitToUnit")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string EditValidation_AlertDetail(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (string.IsNullOrEmpty(((TextBox)FormView_Alert_Form.FindControl("TextBox_EditDescription")).Text))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel1List")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      if (string.IsNullOrEmpty(((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel2List")).SelectedValue))
      {
        InvalidForm = "Yes";
      }

      return InvalidForm;
    }

    protected string EditValidation_Status(string invalidForm)
    {
      string InvalidForm = invalidForm;

      if (((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditStatus")).SelectedValue == "Rejected")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_Alert_Form.FindControl("TextBox_EditStatusRejectedReason")).Text))
        {
          InvalidForm = "Yes";
        }
      }

      return InvalidForm;
    }

    protected string EditFieldValidation()
    {
      string InvalidFormMessage = "";

      DateTime CurrentDate = DateTime.Now;
      DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_Alert_Form.FindControl("TextBox_EditDate")).Text);
      if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + "Not a valid date, date must be in the format yyyy/mm/dd<br />";
      }
      else
      {
        if (ValidatedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Alert_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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

            ScriptManager.RegisterStartupScript(UpdatePanel_Alert, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert Print", "InfoQuest_Print.aspx?PrintPage=Form_Alert&PrintValue=" + Request.QueryString["Alert_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_Alert, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_Alert, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert Email", "InfoQuest_Email.aspx?EmailPage=Form_Alert&EmailValue=" + Request.QueryString["Alert_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_Alert, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_Alert_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["Alert_Id"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert Form", "Form_Alert.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&Alert_Id=" + Request.QueryString["Alert_Id"] + ""), false);
          }
        }
      }
    }

    protected void FormView_Alert_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_Alert_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["Alert_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_Alert_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["Alert_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void EditDataBound()
    {
      DataView DataView_AlertLevels = (DataView)SqlDataSource_Alert_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_AlertLevels = DataView_AlertLevels[0];
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel2List")).SelectedValue = Convert.ToString(DataRowView_AlertLevels["Alert_Level2_List"], CultureInfo.CurrentCulture);

      Session["FacilityFacilityDisplayName"] = "";
      string SQLStringAlert = "SELECT Facility_FacilityDisplayName FROM vForm_Alert WHERE Alert_Id = @Alert_Id";
      using (SqlCommand SqlCommand_Alert = new SqlCommand(SQLStringAlert))
      {
        SqlCommand_Alert.Parameters.AddWithValue("@Alert_Id", Request.QueryString["Alert_Id"]);
        DataTable DataTable_Alert;
        using (DataTable_Alert = new DataTable())
        {
          DataTable_Alert.Locale = CultureInfo.CurrentCulture;
          DataTable_Alert = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Alert).Copy();
          if (DataTable_Alert.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Alert.Rows)
            {
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
            }
          }
        }
      }

      ((Label)FormView_Alert_Form.FindControl("Label_EditFacility")).Text = Session["FacilityFacilityDisplayName"].ToString();

      Session["FacilityFacilityDisplayName"] = "";


      if (Request.QueryString["Alert_Id"] != null)
      {
        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 2";
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
          ((Button)FormView_Alert_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_Alert_Form.FindControl("Button_EditPrint")).Visible = true;
        }
        
        if (Email == "False")
        {
          ((Button)FormView_Alert_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_Alert_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["Alert_Id"] != null)
      {
        Session["FacilityFacilityDisplayName"] = "";
        Session["AlertFacilityFromFacilityFacilityDisplayName"] = "";
        Session["AlertUnitFromName"] = "";
        Session["AlertUnitToName"] = "";
        Session["AlertLevel1Name"] = "";
        Session["AlertLevel2Name"] = "";
        string SQLStringAlert = "SELECT Facility_FacilityDisplayName , Alert_FacilityFrom_Facility_FacilityDisplayName , Alert_UnitFrom_Name , Alert_UnitTo_Name , Alert_Level1_Name , Alert_Level2_Name FROM vForm_Alert WHERE Alert_Id = @Alert_Id";
        using (SqlCommand SqlCommand_Alert = new SqlCommand(SQLStringAlert))
        {
          SqlCommand_Alert.Parameters.AddWithValue("@Alert_Id", Request.QueryString["Alert_Id"]);
          DataTable DataTable_Alert;
          using (DataTable_Alert = new DataTable())
          {
            DataTable_Alert.Locale = CultureInfo.CurrentCulture;
            DataTable_Alert = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Alert).Copy();
            if (DataTable_Alert.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_Alert.Rows)
              {
                Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
                Session["AlertFacilityFromFacilityFacilityDisplayName"] = DataRow_Row["Alert_FacilityFrom_Facility_FacilityDisplayName"];
                Session["AlertUnitFromName"] = DataRow_Row["Alert_UnitFrom_Name"];
                Session["AlertUnitToName"] = DataRow_Row["Alert_UnitTo_Name"];
                Session["AlertLevel1Name"] = DataRow_Row["Alert_Level1_Name"];
                Session["AlertLevel2Name"] = DataRow_Row["Alert_Level2_Name"];
              }
            }
          }
        }


        ((Label)FormView_Alert_Form.FindControl("Label_ItemFacility")).Text = Session["FacilityFacilityDisplayName"].ToString();
        ((Label)FormView_Alert_Form.FindControl("Label_ItemFacilityFrom")).Text = Session["AlertFacilityFromFacilityFacilityDisplayName"].ToString();
        ((Label)FormView_Alert_Form.FindControl("Label_ItemUnitFromUnit")).Text = Session["AlertUnitFromName"].ToString();
        ((Label)FormView_Alert_Form.FindControl("Label_ItemUnitToUnit")).Text = Session["AlertUnitToName"].ToString();
        ((Label)FormView_Alert_Form.FindControl("Label_ItemLevel1List")).Text = Session["AlertLevel1Name"].ToString();
        ((Label)FormView_Alert_Form.FindControl("Label_ItemLevel2List")).Text = Session["AlertLevel2Name"].ToString();

        Session["FacilityFacilityDisplayName"] = "";
        Session["AlertFacilityFromFacilityFacilityDisplayName"] = "";
        Session["AlertUnitFromName"] = "";
        Session["AlertUnitToName"] = "";
        Session["AlertLevel1Name"] = "";
        Session["AlertLevel2Name"] = "";

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 2";
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
          ((Button)FormView_Alert_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_Alert_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_Alert_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert Print", "InfoQuest_Print.aspx?PrintPage=Form_Alert&PrintValue=" + Request.QueryString["Alert_Id"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_Alert_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_Alert_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_Alert_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert Email", "InfoQuest_Email.aspx?EmailPage=Form_Alert&EmailValue=" + Request.QueryString["Alert_Id"] + "") + "')";
        }

        Email = "";
        Print = "";
      }
    }


    //--START-- --Insert Controls--//
    protected void DropDownList_InsertFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertFacility = (DropDownList)sender;

      if (string.IsNullOrEmpty(DropDownList_InsertFacility.SelectedValue))
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert New Form", "Form_Alert.aspx"), false);
      }
      else
      {
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert New Form", "Form_Alert.aspx?s_Facility_Id=" + DropDownList_InsertFacility.SelectedValue + ""), false);
      }
    }

    protected void DropDownList_InsertLevel1List_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel2List")).Items.Clear();
      SqlDataSource_Alert_InsertLevel2List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel2List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 2", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_InsertLevel2List")).DataBind();
    }

    protected void Button_InsertCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_InsertClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert New Form", "Form_Alert.aspx"), false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void DropDownList_EditLevel1List_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel2List")).Items.Clear();
      SqlDataSource_Alert_EditLevel2List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel2List")).Items.Insert(0, new ListItem(Convert.ToString("Select Level 2", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel2List")).DataBind();
    }

    protected void DropDownList_EditLevel1List_DataBound(object sender, EventArgs e)
    {
      SqlDataSource_Alert_EditLevel2List.SelectParameters["ListItem_Parent"].DefaultValue = ((DropDownList)sender).SelectedValue;
      DataView DataView_AlertLevels = (DataView)SqlDataSource_Alert_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_AlertLevels = DataView_AlertLevels[0];
      ((DropDownList)FormView_Alert_Form.FindControl("DropDownList_EditLevel2List")).SelectedValue = Convert.ToString(DataRowView_AlertLevels["Alert_Level2_List"], CultureInfo.CurrentCulture);
    }

    protected void Button_EditCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert New Form", "Form_Alert.aspx"), false);
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
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    protected void Button_ItemCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Alert New Form", "Form_Alert.aspx"), false);
    }
    //---END--- --Item Controls--//
    //---END--- --TableForm--// 
  }
}