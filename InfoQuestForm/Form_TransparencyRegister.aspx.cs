using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace InfoQuestForm
{
  public partial class Form_TransparencyRegister : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_TransparencyRegister, this.GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();Calculation_Form();", true);

        HiddenField HiddenField_InsertTransparencyRegisterIdTemp;
        using (HiddenField_InsertTransparencyRegisterIdTemp = new HiddenField())
        {
          if (string.IsNullOrEmpty(Request.QueryString["TransparencyRegister_Id"]))
          {
            HiddenField_InsertTransparencyRegisterIdTemp = (HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertTransparencyRegisterIdTemp");
            if (string.IsNullOrEmpty(HiddenField_InsertTransparencyRegisterIdTemp.Value))
            {
              HiddenField_InsertTransparencyRegisterIdTemp.Value = "TEMP_ID:USER:" + Request.ServerVariables["LOGON_USER"].ToUpper(CultureInfo.CurrentCulture) + ":DATE:" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss", CultureInfo.CurrentCulture).ToUpper(CultureInfo.CurrentCulture) + "";
            }
          }
        }

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_TransparencyRegister_File_InsertFile.SelectParameters["TransparencyRegister_File_Temp_TransparencyRegister_Id"].DefaultValue = HiddenField_InsertTransparencyRegisterIdTemp.Value;

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("44").Replace(" Form", "")).ToString();
          Label_TransparencyRegisterHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("44").Replace(" Form", "")).ToString();

          SetFormVisibility();

          TableFormVisible();

          FileCleanUp();
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('44'))";
        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("44");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_TransparencyRegister.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Transparency Register", "18");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_TransparencyRegister_InsertClassification.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_TransparencyRegister_InsertClassification.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_TransparencyRegister_InsertClassification.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_TransparencyRegister_InsertClassification.SelectParameters.Clear();
      SqlDataSource_TransparencyRegister_InsertClassification.SelectParameters.Add("Form_Id", TypeCode.String, "44");
      SqlDataSource_TransparencyRegister_InsertClassification.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "186");
      SqlDataSource_TransparencyRegister_InsertClassification.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_TransparencyRegister_InsertClassification.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_TransparencyRegister_InsertClassification.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_TransparencyRegister_InsertClassification.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_TransparencyRegister_File_InsertFile.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_TransparencyRegister_File_InsertFile.SelectCommand = "SELECT TransparencyRegister_File_Id , TransparencyRegister_File_FileName , TransparencyRegister_File_CreatedDate , TransparencyRegister_File_CreatedBy FROM Form_TransparencyRegister_File WHERE TransparencyRegister_File_CreatedBy = @TransparencyRegister_File_CreatedBy AND TransparencyRegister_File_Temp_TransparencyRegister_Id = @TransparencyRegister_File_Temp_TransparencyRegister_Id ORDER BY TransparencyRegister_File_FileName";
      SqlDataSource_TransparencyRegister_File_InsertFile.SelectParameters.Clear();
      SqlDataSource_TransparencyRegister_File_InsertFile.SelectParameters.Add("TransparencyRegister_File_CreatedBy", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_TransparencyRegister_File_InsertFile.SelectParameters.Add("TransparencyRegister_File_Temp_TransparencyRegister_Id", TypeCode.String, "");

      SqlDataSource_TransparencyRegister_EditClassification.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_TransparencyRegister_EditClassification.SelectCommand = "SELECT ListItem_Name , TransparencyRegister_Classification_Value FROM Form_TransparencyRegister_Classification LEFT JOIN Administration_ListItem ON TransparencyRegister_Classification_Item_List = ListItem_Id WHERE TransparencyRegister_Id = @TransparencyRegister_Id";
      SqlDataSource_TransparencyRegister_EditClassification.SelectParameters.Clear();
      SqlDataSource_TransparencyRegister_EditClassification.SelectParameters.Add("TransparencyRegister_Id", TypeCode.String, Request.QueryString["TransparencyRegister_Id"]);

      SqlDataSource_TransparencyRegister_File_EditFile.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_TransparencyRegister_File_EditFile.SelectCommand = "SELECT TransparencyRegister_File_Id , TransparencyRegister_File_FileName , TransparencyRegister_File_CreatedDate , TransparencyRegister_File_CreatedBy FROM Form_TransparencyRegister_File WHERE TransparencyRegister_Id = @TransparencyRegister_Id ORDER BY TransparencyRegister_File_FileName";
      SqlDataSource_TransparencyRegister_File_EditFile.SelectParameters.Clear();
      SqlDataSource_TransparencyRegister_File_EditFile.SelectParameters.Add("TransparencyRegister_Id", TypeCode.String, Request.QueryString["TransparencyRegister_Id"]);

      SqlDataSource_TransparencyRegister_ItemClassification.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_TransparencyRegister_ItemClassification.SelectCommand = "SELECT ListItem_Name , TransparencyRegister_Classification_Value FROM Form_TransparencyRegister_Classification LEFT JOIN Administration_ListItem ON TransparencyRegister_Classification_Item_List = ListItem_Id WHERE TransparencyRegister_Id = @TransparencyRegister_Id";
      SqlDataSource_TransparencyRegister_ItemClassification.SelectParameters.Clear();
      SqlDataSource_TransparencyRegister_ItemClassification.SelectParameters.Add("TransparencyRegister_Id", TypeCode.String, Request.QueryString["TransparencyRegister_Id"]);

      SqlDataSource_TransparencyRegister_File_ItemFile.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_TransparencyRegister_File_ItemFile.SelectCommand = "SELECT TransparencyRegister_File_Id , TransparencyRegister_File_FileName , TransparencyRegister_File_CreatedDate , TransparencyRegister_File_CreatedBy FROM Form_TransparencyRegister_File WHERE TransparencyRegister_Id = @TransparencyRegister_Id ORDER BY TransparencyRegister_File_FileName";
      SqlDataSource_TransparencyRegister_File_ItemFile.SelectParameters.Clear();
      SqlDataSource_TransparencyRegister_File_ItemFile.SelectParameters.Add("TransparencyRegister_Id", TypeCode.String, Request.QueryString["TransparencyRegister_Id"]);

      SqlDataSource_TransparencyRegister_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_TransparencyRegister_Form.InsertCommand = "INSERT INTO Form_TransparencyRegister ( TransparencyRegister_EmployeeNumber , TransparencyRegister_FirstName , TransparencyRegister_LastName , TransparencyRegister_JobTitle , TransparencyRegister_Department , TransparencyRegister_ManagerEmployeeNumber , TransparencyRegister_ManagerFirstName , TransparencyRegister_ManagerLastName , TransparencyRegister_DeclarationDate , TransparencyRegister_Description , TransparencyRegister_Value , TransparencyRegister_Purpose , TransparencyRegister_PersonOrganisation , TransparencyRegister_Relationship , TransparencyRegister_Status , TransparencyRegister_StatusDate , TransparencyRegister_StatusApprovedRejectedBy , TransparencyRegister_StatusMessage , TransparencyRegister_CreatedDate , TransparencyRegister_CreatedBy , TransparencyRegister_ModifiedDate , TransparencyRegister_ModifiedBy , TransparencyRegister_History , TransparencyRegister_Archived ) VALUES ( @TransparencyRegister_EmployeeNumber , @TransparencyRegister_FirstName , @TransparencyRegister_LastName , @TransparencyRegister_JobTitle , @TransparencyRegister_Department , @TransparencyRegister_ManagerEmployeeNumber , @TransparencyRegister_ManagerFirstName , @TransparencyRegister_ManagerLastName , @TransparencyRegister_DeclarationDate , @TransparencyRegister_Description , @TransparencyRegister_Value , @TransparencyRegister_Purpose , @TransparencyRegister_PersonOrganisation , @TransparencyRegister_Relationship , @TransparencyRegister_Status , @TransparencyRegister_StatusDate , @TransparencyRegister_StatusApprovedRejectedBy , @TransparencyRegister_StatusMessage , @TransparencyRegister_CreatedDate , @TransparencyRegister_CreatedBy , @TransparencyRegister_ModifiedDate , @TransparencyRegister_ModifiedBy , @TransparencyRegister_History , @TransparencyRegister_Archived ); SELECT @TransparencyRegister_Id = SCOPE_IDENTITY()";
      SqlDataSource_TransparencyRegister_Form.SelectCommand = "SELECT * FROM Form_TransparencyRegister WHERE TransparencyRegister_Id = @TransparencyRegister_Id";
      SqlDataSource_TransparencyRegister_Form.UpdateCommand = "UPDATE Form_TransparencyRegister SET TransparencyRegister_Status = @TransparencyRegister_Status  , TransparencyRegister_StatusDate = @TransparencyRegister_StatusDate  , TransparencyRegister_StatusApprovedRejectedBy = @TransparencyRegister_StatusApprovedRejectedBy , TransparencyRegister_StatusMessage = @TransparencyRegister_StatusMessage , TransparencyRegister_ModifiedDate = @TransparencyRegister_ModifiedDate  , TransparencyRegister_ModifiedBy = @TransparencyRegister_ModifiedBy  , TransparencyRegister_History = @TransparencyRegister_History WHERE TransparencyRegister_Id = @TransparencyRegister_Id";
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Clear();
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_Id", TypeCode.Int32, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_EmployeeNumber", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_FirstName", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_LastName", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_JobTitle", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_Department", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_ManagerEmployeeNumber", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_ManagerFirstName", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_ManagerLastName", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_DeclarationDate", TypeCode.DateTime, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_Description", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_Value", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_Purpose", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_PersonOrganisation", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_Relationship", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_Status", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_StatusApprovedRejectedBy", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_StatusMessage", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_CreatedBy", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_ModifiedBy", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_History", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_TransparencyRegister_Form.InsertParameters.Add("TransparencyRegister_Archived", TypeCode.Boolean, "");
      SqlDataSource_TransparencyRegister_Form.SelectParameters.Clear();
      SqlDataSource_TransparencyRegister_Form.SelectParameters.Add("TransparencyRegister_Id", TypeCode.Int32, Request.QueryString["TransparencyRegister_Id"]);
      SqlDataSource_TransparencyRegister_Form.UpdateParameters.Clear();
      SqlDataSource_TransparencyRegister_Form.UpdateParameters.Add("TransparencyRegister_Status", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.UpdateParameters.Add("TransparencyRegister_StatusDate", TypeCode.DateTime, "");
      SqlDataSource_TransparencyRegister_Form.UpdateParameters.Add("TransparencyRegister_StatusApprovedRejectedBy", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.UpdateParameters.Add("TransparencyRegister_StatusMessage", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.UpdateParameters.Add("TransparencyRegister_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_TransparencyRegister_Form.UpdateParameters.Add("TransparencyRegister_ModifiedBy", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.UpdateParameters.Add("TransparencyRegister_History", TypeCode.String, "");
      SqlDataSource_TransparencyRegister_Form.UpdateParameters.Add("TransparencyRegister_Id", TypeCode.Int32, "");
    }

    private void SetFormVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["TransparencyRegister_Id"]))
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
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('44'))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '176'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '177'");

            string Security = "1";
            if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFormAdminView.Length > 0))
            {
              Security = "0";
              FormView_TransparencyRegister_Form.ChangeMode(FormViewMode.Insert);
            }

            if (Security == "1")
            {
              Security = "0";
              FormView_TransparencyRegister_Form.ChangeMode(FormViewMode.Insert);
            }
          }
          else
          {
            FormView_TransparencyRegister_Form.ChangeMode(FormViewMode.Insert);
          }
        }
      }
    }

    protected void SetFormVisibility_Edit()
    {
      string TransparencyRegisterEmployeeNumber = "";
      string TransparencyRegisterManagerEmployeeNumber = "";
      string TransparencyRegisterStatus = "";
      string SQLStringTransparencyRegister = "SELECT TransparencyRegister_EmployeeNumber , TransparencyRegister_ManagerEmployeeNumber , TransparencyRegister_Status FROM Form_TransparencyRegister WHERE TransparencyRegister_Id = @TransparencyRegister_Id";
      using (SqlCommand SqlCommand_TransparencyRegister = new SqlCommand(SQLStringTransparencyRegister))
      {
        SqlCommand_TransparencyRegister.Parameters.AddWithValue("@TransparencyRegister_Id", Request.QueryString["TransparencyRegister_Id"]);
        DataTable DataTable_TransparencyRegister;
        using (DataTable_TransparencyRegister = new DataTable())
        {
          DataTable_TransparencyRegister.Locale = CultureInfo.CurrentCulture;
          DataTable_TransparencyRegister = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TransparencyRegister).Copy();
          if (DataTable_TransparencyRegister.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TransparencyRegister.Rows)
            {
              TransparencyRegisterEmployeeNumber = DataRow_Row["TransparencyRegister_EmployeeNumber"].ToString();
              TransparencyRegisterManagerEmployeeNumber = DataRow_Row["TransparencyRegister_ManagerEmployeeNumber"].ToString();
              TransparencyRegisterStatus = DataRow_Row["TransparencyRegister_Status"].ToString();
            }
          }
        }
      }


      string CurrentEmployeeNumber = "";
      DataTable DataTable_CurrentEmployeeNumber = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(Request.ServerVariables["LOGON_USER"]);
      if (DataTable_CurrentEmployeeNumber.Columns.Count != 1)
      {
        if (DataTable_CurrentEmployeeNumber.Rows.Count > 0)
        {
          foreach (DataRow DataRow_CurrentEmployeeNumber in DataTable_CurrentEmployeeNumber.Rows)
          {
            CurrentEmployeeNumber = DataRow_CurrentEmployeeNumber["EmployeeNumber"].ToString();
          }
        }
      }


      string EmployeeManager = "";
      DataTable DataTable_EmployeeManager;
      using (DataTable_EmployeeManager = new DataTable())
      {
        DataTable_EmployeeManager.Locale = CultureInfo.CurrentCulture;
        DataTable_EmployeeManager = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_TransparencyRegister_EmployeeManager(TransparencyRegisterEmployeeNumber, TransparencyRegisterManagerEmployeeNumber, CurrentEmployeeNumber).Copy();
        if (DataTable_EmployeeManager.Columns.Contains("EmployeeManager") == true)
        {
          if (DataTable_EmployeeManager.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EmployeeManager.Rows)
            {
              EmployeeManager = DataRow_Row["EmployeeManager"].ToString();
            }
          }
        }
      }

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR SecurityRole_Id IN ('176'))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '176'");

            string Security = "1";
            if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Security = "0";
              if (TransparencyRegisterEmployeeNumber == CurrentEmployeeNumber)
              {
                FormView_TransparencyRegister_Form.ChangeMode(FormViewMode.ReadOnly);
              }
              else
              {
                FormView_TransparencyRegister_Form.ChangeMode(FormViewMode.Edit);
              }
            }
          }
          else
          {
            if (string.IsNullOrEmpty(EmployeeManager))
            {
              FormView_TransparencyRegister_Form.ChangeMode(FormViewMode.ReadOnly);
            }
            else
            {
              if (TransparencyRegisterStatus == "Pending Approval")
              {
                FormView_TransparencyRegister_Form.ChangeMode(FormViewMode.Edit);
              }
              else
              {
                FormView_TransparencyRegister_Form.ChangeMode(FormViewMode.ReadOnly);
              }
            }
          }
        }
      }
    }

    private void TableFormVisible()
    {
      if (FormView_TransparencyRegister_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertEmployeeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertEmployeeNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertDeclarationDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertDeclarationDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertValue")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertValue")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertPurpose")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertPurpose")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertPersonOrganisation")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertPersonOrganisation")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertRelationship")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertRelationship")).Attributes.Add("OnInput", "Validation_Form();");

        foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_TransparencyRegister_Form.FindControl("GridView_InsertTransparencyRegister_Classification")).Rows)
        {
          ((CheckBox)GridViewRow_Row.FindControl("CheckBox_InsertName")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();Calculation_Form();");
          ((TextBox)GridViewRow_Row.FindControl("TextBox_InsertValue")).Attributes.Add("OnKeyUp", "Validation_Form();Calculation_Form();");
          ((TextBox)GridViewRow_Row.FindControl("TextBox_InsertValue")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();");
        }

        TableFormVisible_Insert_LoadCurrentEmployee();

        if (string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertDeclarationDate")).Text))
        {
          ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertDeclarationDate")).Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
        }
      }

      if (FormView_TransparencyRegister_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_TransparencyRegister_Form.FindControl("DropDownList_EditStatus")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_EditStatusMessage")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_EditStatusMessage")).Attributes.Add("OnInput", "Validation_Form();");
      }
    }

    private void TableFormVisible_Insert_LoadCurrentEmployee()
    {
      string CurrentEmployeeNumber = "";
      string Error = "";
      DataTable DataTable_CurrentEmployeeNumber = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(Request.ServerVariables["LOGON_USER"]);
      if (DataTable_CurrentEmployeeNumber.Columns.Count == 1)
      {
        foreach (DataRow DataRow_Row in DataTable_CurrentEmployeeNumber.Rows)
        {
          Error = DataRow_Row["Error"].ToString();
          if (Error == "No Employee Data")
          {
            Error = "Employee number not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Active Directory";
          }
        }
      }
      else if (DataTable_CurrentEmployeeNumber.Columns.Count != 1)
      {
        if (DataTable_CurrentEmployeeNumber.Rows.Count > 0)
        {
          foreach (DataRow DataRow_Row in DataTable_CurrentEmployeeNumber.Rows)
          {
            CurrentEmployeeNumber = DataRow_Row["EmployeeNumber"].ToString();
          }
        }
        else
        {
          Error = "Employee number not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Active Directory";
        }
      }

      if (string.IsNullOrEmpty(Error))
      {
        string FirstName = "";
        string LastName = "";
        string JobTitle = "";
        string Department = "";
        string ReportsTo = "";
        string ManagerEmployeeNumber = "";
        DataTable DataTable_DataEmployee;
        using (DataTable_DataEmployee = new DataTable())
        {
          DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
          DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_FindEmployeeInfo_SearchEmployeeNumber(CurrentEmployeeNumber).Copy();
          if (DataTable_DataEmployee.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Error = DataRow_Row["Error"].ToString();
              if (Error == "No Employee Data")
              {
                Error = "Employee detail not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Vision";
              }
            }
          }
          else if (DataTable_DataEmployee.Columns.Count != 1)
          {
            if (DataTable_DataEmployee.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
              {
                FirstName = DataRow_Row["FirstName"].ToString();
                LastName = DataRow_Row["LastName"].ToString();
                JobTitle = DataRow_Row["JobTitle"].ToString();
                Department = DataRow_Row["Department"].ToString();
                ReportsTo = DataRow_Row["ReportsTo"].ToString();
                ManagerEmployeeNumber = DataRow_Row["ManagerEmployeeNumber"].ToString();
              }
            }
            else
            {
              Error = "Employee detail not found for current logged in employee<br/>Please log a call with Contact Centre for employee number to be updated on Vision";
            }
          }
        }

        ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertEmployeeNumber")).Text = CurrentEmployeeNumber;
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertFirstName")).Text = FirstName;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertFirstName")).Value = FirstName;
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertLastName")).Text = LastName;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertLastName")).Value = LastName;
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertJobTitle")).Text = JobTitle;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertJobTitle")).Value = JobTitle;
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertDepartment")).Text = Department;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertDepartment")).Value = Department;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmployeeNumber")).Value = ManagerEmployeeNumber;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertReportsTo")).Value = ReportsTo;

        FirstName = "";
        LastName = "";
        JobTitle = "";
        Department = "";
        ReportsTo = "";
        ManagerEmployeeNumber = "";


        if (string.IsNullOrEmpty(Error))
        {
          TableFormVisible_Insert_LoadCurrentEmployee_CEOApprover();

          Error = TableFormVisible_Insert_LoadCurrentEmployee_CurrentManager();
        }
      }

      if (!string.IsNullOrEmpty(Error))
      {
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertEmployeeError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Error + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertEmployeeError")).Text = "";
      }

      CurrentEmployeeNumber = "";
      Error = "";
    }

    private void TableFormVisible_Insert_LoadCurrentEmployee_CEOApprover()
    {
      if (((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertReportsTo")).Value == "00000001")
      {
        string CEOManagerEmployeeNumber = "";
        string SQLStringCEOManagerEmployeeNumber = "SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 185";
        using (SqlCommand SqlCommand_CEOManagerEmployeeNumber = new SqlCommand(SQLStringCEOManagerEmployeeNumber))
        {
          DataTable DataTable_CEOManagerEmployeeNumber;
          using (DataTable_CEOManagerEmployeeNumber = new DataTable())
          {
            DataTable_CEOManagerEmployeeNumber.Locale = CultureInfo.CurrentCulture;
            DataTable_CEOManagerEmployeeNumber = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CEOManagerEmployeeNumber).Copy();
            if (DataTable_CEOManagerEmployeeNumber.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_CEOManagerEmployeeNumber.Rows)
              {
                CEOManagerEmployeeNumber = DataRow_Row["ListItem_Name"].ToString();
              }
            }
          }
        }

        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmployeeNumber")).Value = CEOManagerEmployeeNumber;
      }
    }

    private string TableFormVisible_Insert_LoadCurrentEmployee_CurrentManager()
    {
      string Error = "";

      string CurrentManagerName = "";
      string CurrentManagerFirstName = "";
      string CurrentManagerLastName = "";
      string CurrentManagerUserName = "";
      string CurrentManagerEmail = "";
      DataTable DataTable_CurrentManagerUserName = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_EmployeeNumber(((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmployeeNumber")).Value);
      if (DataTable_CurrentManagerUserName.Columns.Count == 1)
      {
        foreach (DataRow DataRow_Row in DataTable_CurrentManagerUserName.Rows)
        {
          Error = DataRow_Row["Error"].ToString();
          if (Error == "No Employee Data")
          {
            Error = "Managers username not found for current logged in employee<br/>Please log a call with Contact Centre for employees manager to be updated on Active Directory";
          }
        }
      }
      else if (DataTable_CurrentManagerUserName.Columns.Count != 1)
      {
        if (DataTable_CurrentManagerUserName.Rows.Count > 0)
        {
          foreach (DataRow DataRow_CurrentManagerUserName in DataTable_CurrentManagerUserName.Rows)
          {
            CurrentManagerName = Convert.ToString(DataRow_CurrentManagerUserName["FirstName"].ToString() + " " + DataRow_CurrentManagerUserName["LastName"].ToString(), CultureInfo.CurrentCulture);
            CurrentManagerFirstName = DataRow_CurrentManagerUserName["FirstName"].ToString();
            CurrentManagerLastName = DataRow_CurrentManagerUserName["LastName"].ToString();
            CurrentManagerUserName = DataRow_CurrentManagerUserName["UserName"].ToString();
            CurrentManagerEmail = DataRow_CurrentManagerUserName["Email"].ToString();
          }
        }
        else
        {
          Error = "Managers username not found for current logged in employee<br/>Please log a call with Contact Centre for employees manager to be updated on Active Directory";
        }
      }

      ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertManagerName")).Text = CurrentManagerName;
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerFirstName")).Value = CurrentManagerFirstName;
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerLastName")).Value = CurrentManagerLastName;
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerUserName")).Value = CurrentManagerUserName;
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmail")).Value = CurrentManagerEmail;

      CurrentManagerName = "";
      CurrentManagerFirstName = "";
      CurrentManagerLastName = "";
      CurrentManagerUserName = "";
      CurrentManagerEmail = "";

      return Error;
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_TransparencyRegisterName"];
      string SearchField2 = Request.QueryString["Search_TransparencyRegisterEmployeeNumber"];
      string SearchField3 = Request.QueryString["Search_TransparencyRegisterStatus"];
      string SearchField4 = Request.QueryString["Search_TransparencyRegisterShowAll"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_TransparencyRegister_Name=" + Request.QueryString["Search_TransparencyRegisterName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_TransparencyRegister_EmployeeNumber=" + Request.QueryString["Search_TransparencyRegisterEmployeeNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_TransparencyRegister_Status=" + Request.QueryString["Search_TransparencyRegisterStatus"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_TransparencyRegister_ShowAll=" + Request.QueryString["Search_TransparencyRegisterShowAll"] + "&";
      }

      string FinalURL = "Form_TransparencyRegister_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void FileCleanUp()
    {
      if (string.IsNullOrEmpty(Request.QueryString["TransparencyRegister_Id"]))
      {
        HiddenField HiddenField_InsertTransparencyRegisterIdTemp = (HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertTransparencyRegisterIdTemp");

        string SQLStringTransparencyRegisterFile = "DELETE FROM Form_TransparencyRegister_File WHERE TransparencyRegister_Id IS NULL AND TransparencyRegister_File_CreatedBy = @TransparencyRegister_File_CreatedBy AND TransparencyRegister_File_Temp_TransparencyRegister_Id <> @TransparencyRegister_File_Temp_TransparencyRegister_Id";
        using (SqlCommand SqlCommand_TransparencyRegisterFile = new SqlCommand(SQLStringTransparencyRegisterFile))
        {
          SqlCommand_TransparencyRegisterFile.Parameters.AddWithValue("@TransparencyRegister_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_TransparencyRegisterFile.Parameters.AddWithValue("@TransparencyRegister_File_Temp_TransparencyRegister_Id", HiddenField_InsertTransparencyRegisterIdTemp.Value);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_TransparencyRegisterFile);
        }
      }
    }


    //--START-- --TableForm--//
    //--START-- --Insert--//
    protected void FormView_TransparencyRegister_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_TransparencyRegister.SetFocus(UpdatePanel_TransparencyRegister);

          ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";

          InsertRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_History"].DefaultValue = "";
          SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_Archived"].DefaultValue = "false";

          SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_Status"].DefaultValue = "Pending Approval";
          SqlDataSource_TransparencyRegister_Form.InsertParameters["TransparencyRegister_StatusDate"].DefaultValue = DateTime.Now.ToString();
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertEmployeeNumber")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertDeclarationDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((GridView)FormView_TransparencyRegister_Form.FindControl("GridView_InsertTransparencyRegister_Classification")).Rows.Count > 0)
        {
          string ValidClassification = "";
          foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_TransparencyRegister_Form.FindControl("GridView_InsertTransparencyRegister_Classification")).Rows)
          {
            if (((CheckBox)GridViewRow_Row.FindControl("CheckBox_InsertName")).Checked == true && !string.IsNullOrEmpty(((TextBox)GridViewRow_Row.FindControl("TextBox_InsertValue")).Text))
            {
              if (ValidClassification != "No")
              {
                ValidClassification = "Yes";
              }
            }
            else if (((CheckBox)GridViewRow_Row.FindControl("CheckBox_InsertName")).Checked == true && string.IsNullOrEmpty(((TextBox)GridViewRow_Row.FindControl("TextBox_InsertValue")).Text))
            {
              ValidClassification = "No";
            }
          }

          if (ValidClassification == "No")
          {
            InvalidForm = "Yes";
          }
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertDescription")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertValue")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertPurpose")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertPersonOrganisation")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertRelationship")).Text))
        {
          InvalidForm = "Yes";
        }

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

    protected string InsertFieldValidation()
    {
      string InvalidFormMessage = "";

      if (string.IsNullOrEmpty(((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertFirstName")).Value))
      {
        InvalidFormMessage = InvalidFormMessage + "Employee detail not found for specific employee number, Please log a call with Contact Centre for employee number to be updated on Vision<br />";
      }

      if (string.IsNullOrEmpty(((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmployeeNumber")).Value) && ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertReportsTo")).Value != "00000001")
      {
        InvalidFormMessage = InvalidFormMessage + "Managers employee number not found for specific employee number, Please log a call with Contact Centre for employees manager to be updated on Vision<br />";
      }

      if (string.IsNullOrEmpty(((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerUserName")).Value))
      {
        InvalidFormMessage = InvalidFormMessage + "Managers username not found for specific employee number, Please log a call with Contact Centre for employees manager to be updated on Active Directory<br />";
      }

      DateTime CurrentDate = DateTime.Now;
      DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertDeclarationDate")).Text);
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

      if (!string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertValue")).Text))
      {
        string ValidCurrency = InfoQuestWCF.InfoQuest_Regex.Regex_Currency((((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertValue")).Text).ToString());

        if (ValidCurrency == "No")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid Value, Please enter only numbers like 1000 or 1000.00<br />";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_TransparencyRegister_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string TransparencyRegister_Id = e.Command.Parameters["@TransparencyRegister_Id"].Value.ToString();
        string TransparencyRegister_EmployeeNumber = e.Command.Parameters["@TransparencyRegister_EmployeeNumber"].Value.ToString();
        string TransparencyRegister_FirstName = e.Command.Parameters["@TransparencyRegister_FirstName"].Value.ToString();
        string TransparencyRegister_LastName = e.Command.Parameters["@TransparencyRegister_LastName"].Value.ToString();
        string TransparencyRegister_ManagerFirstName = e.Command.Parameters["@TransparencyRegister_ManagerFirstName"].Value.ToString();
        string TransparencyRegister_ManagerLastName = e.Command.Parameters["@TransparencyRegister_ManagerLastName"].Value.ToString();
        string TransparencyRegister_CreatedBy = e.Command.Parameters["@TransparencyRegister_CreatedBy"].Value.ToString();

        HiddenField HiddenField_InsertTransparencyRegisterIdTemp = (HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertTransparencyRegisterIdTemp");

        if (!string.IsNullOrEmpty(TransparencyRegister_Id))
        {
          string SQLStringUpdateFile = "UPDATE Form_TransparencyRegister_File SET TransparencyRegister_Id = @TransparencyRegister_Id, TransparencyRegister_File_Temp_TransparencyRegister_Id = NULL WHERE TransparencyRegister_File_Temp_TransparencyRegister_Id = @TransparencyRegister_File_Temp_TransparencyRegister_Id";
          using (SqlCommand SqlCommand_UpdateFile = new SqlCommand(SQLStringUpdateFile))
          {
            SqlCommand_UpdateFile.Parameters.AddWithValue("@TransparencyRegister_Id", TransparencyRegister_Id);
            SqlCommand_UpdateFile.Parameters.AddWithValue("@TransparencyRegister_File_Temp_TransparencyRegister_Id", HiddenField_InsertTransparencyRegisterIdTemp.Value.ToString());

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateFile);
          }


          if (((GridView)FormView_TransparencyRegister_Form.FindControl("GridView_InsertTransparencyRegister_Classification")).Rows.Count > 0)
          {
            foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_TransparencyRegister_Form.FindControl("GridView_InsertTransparencyRegister_Classification")).Rows)
            {
              if (((CheckBox)GridViewRow_Row.FindControl("CheckBox_InsertName")).Checked == true)
              {
                string SQLStringInsertClassification = "INSERT INTO Form_TransparencyRegister_Classification ( TransparencyRegister_Id , TransparencyRegister_Classification_Item_List , TransparencyRegister_Classification_Value ) VALUES ( @TransparencyRegister_Id , @TransparencyRegister_Classification_Item_List , @TransparencyRegister_Classification_Value )";
                using (SqlCommand SqlCommand_InsertClassification = new SqlCommand(SQLStringInsertClassification))
                {
                  SqlCommand_InsertClassification.Parameters.AddWithValue("@TransparencyRegister_Id", TransparencyRegister_Id);
                  SqlCommand_InsertClassification.Parameters.AddWithValue("@TransparencyRegister_Classification_Item_List", ((HiddenField)GridViewRow_Row.FindControl("HiddenField_InsertId")).Value);
                  SqlCommand_InsertClassification.Parameters.AddWithValue("@TransparencyRegister_Classification_Value", ((TextBox)GridViewRow_Row.FindControl("TextBox_InsertValue")).Text);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertClassification);
                }
              }
            }
          }
        }


        string TransparencyRegisterCreatedByNameSurname = "";
        DataTable DataTable_TransparencyRegisterCreatedByNameSurname = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(TransparencyRegister_CreatedBy);
        if (DataTable_TransparencyRegisterCreatedByNameSurname.Columns.Count != 1)
        {
          if (DataTable_TransparencyRegisterCreatedByNameSurname.Rows.Count > 0)
          {
            foreach (DataRow DataRow_TransparencyRegisterCreatedByNameSurname in DataTable_TransparencyRegisterCreatedByNameSurname.Rows)
            {
              TransparencyRegisterCreatedByNameSurname = DataRow_TransparencyRegisterCreatedByNameSurname["FirstName"].ToString() + " " + DataRow_TransparencyRegisterCreatedByNameSurname["LastName"].ToString();
            }
          }
        }


        string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("74");
        string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
        string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("44");
        string BodyString = EmailTemplate;

        BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + TransparencyRegister_ManagerFirstName + " " + TransparencyRegister_ManagerLastName + "");
        BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
        BodyString = BodyString.Replace(";replace;TransparencyRegisterEmployeeNumber;replace;", "" + TransparencyRegister_EmployeeNumber + "");
        BodyString = BodyString.Replace(";replace;TransparencyRegisterFirstName;replace;", "" + TransparencyRegister_FirstName + "");
        BodyString = BodyString.Replace(";replace;TransparencyRegisterLastName;replace;", "" + TransparencyRegister_LastName + "");
        BodyString = BodyString.Replace(";replace;TransparencyRegisterCreatedBy;replace;", "" + TransparencyRegisterCreatedByNameSurname + " (" + TransparencyRegister_CreatedBy + ")");
        BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
        BodyString = BodyString.Replace(";replace;TransparencyRegisterId;replace;", "" + TransparencyRegister_Id + "");

        string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
        string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
        string EmailBody = HeaderString + BodyString + FooterString;

        string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmail")).Value, FormName, EmailBody);

        if (!string.IsNullOrEmpty(EmailSend))
        {
          EmailSend = "";
        }

        EmailBody = "";
        EmailTemplate = "";
        URLAuthority = "";
        FormName = "";

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Captured", "InfoQuest_Captured.aspx?CapturedPage=Form_TransparencyRegister&CapturedNumber=" + TransparencyRegister_Id + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    private bool Edit_EmailEmployee = false;

    protected void FormView_TransparencyRegister_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDTransparencyRegisterModifiedDate"] = e.OldValues["TransparencyRegister_ModifiedDate"];
        object OLDTransparencyRegisterModifiedDate = Session["OLDTransparencyRegisterModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDTransparencyRegisterModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareTransparencyRegister = (DataView)SqlDataSource_TransparencyRegister_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareTransparencyRegister = DataView_CompareTransparencyRegister[0];
        Session["DBTransparencyRegisterModifiedDate"] = Convert.ToString(DataRowView_CompareTransparencyRegister["TransparencyRegister_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBTransparencyRegisterModifiedBy"] = Convert.ToString(DataRowView_CompareTransparencyRegister["TransparencyRegister_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBTransparencyRegisterModifiedDate = Session["DBTransparencyRegisterModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBTransparencyRegisterModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_TransparencyRegister.SetFocus(UpdatePanel_TransparencyRegister);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBTransparencyRegisterModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_TransparencyRegister_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_TransparencyRegister_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_TransparencyRegister.SetFocus(UpdatePanel_TransparencyRegister);
            ((Label)FormView_TransparencyRegister_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_TransparencyRegister_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;
            e.NewValues["TransparencyRegister_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["TransparencyRegister_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_TransparencyRegister", "TransparencyRegister_Id = " + Request.QueryString["TransparencyRegister_Id"]);

            DataView DataView_TransparencyRegister = (DataView)SqlDataSource_TransparencyRegister_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_TransparencyRegister = DataView_TransparencyRegister[0];
            Session["TransparencyRegisterHistory"] = Convert.ToString(DataRowView_TransparencyRegister["TransparencyRegister_History"], CultureInfo.CurrentCulture);

            Session["TransparencyRegisterHistory"] = Session["History"].ToString() + Session["TransparencyRegisterHistory"].ToString();
            e.NewValues["TransparencyRegister_History"] = Session["TransparencyRegisterHistory"].ToString();

            Session["TransparencyRegisterHistory"] = "";
            Session["History"] = "";


            string DBStatus = e.OldValues["TransparencyRegister_Status"].ToString();

            if (DBStatus != ((DropDownList)FormView_TransparencyRegister_Form.FindControl("DropDownList_EditStatus")).SelectedValue)
            {
              if (DBStatus == "Pending Approval")
              {
                Edit_EmailEmployee = true;
                e.NewValues["TransparencyRegister_StatusDate"] = DateTime.Now.ToString();
                e.NewValues["TransparencyRegister_StatusApprovedRejectedBy"] = Request.ServerVariables["LOGON_USER"];
              }
              else if (((DropDownList)FormView_TransparencyRegister_Form.FindControl("DropDownList_EditStatus")).SelectedValue == "Pending Approval")
              {
                e.NewValues["TransparencyRegister_StatusDate"] = DateTime.Now.ToString();
                e.NewValues["TransparencyRegister_StatusApprovedRejectedBy"] = DBNull.Value;
              }
              else if (DBStatus != "Pending Approval" && ((DropDownList)FormView_TransparencyRegister_Form.FindControl("DropDownList_EditStatus")).SelectedValue != "Pending Approval")
              {
                Edit_EmailEmployee = true;
                e.NewValues["TransparencyRegister_StatusDate"] = DateTime.Now.ToString();
                e.NewValues["TransparencyRegister_StatusApprovedRejectedBy"] = Request.ServerVariables["LOGON_USER"];
              }
            }
          }
        }

        Session["OLDTransparencyRegisterModifiedDate"] = "";
        Session["DBTransparencyRegisterModifiedDate"] = "";
        Session["DBTransparencyRegisterModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (((DropDownList)FormView_TransparencyRegister_Form.FindControl("DropDownList_EditStatus")).SelectedValue == "Rejected")
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_EditStatusMessage")).Text.Trim()))
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

    protected void SqlDataSource_TransparencyRegister_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            if (Edit_EmailEmployee == true)
            {
              string TransparencyRegister_Id = e.Command.Parameters["@TransparencyRegister_Id"].Value.ToString();
              string TransparencyRegister_EmployeeNumber = e.Command.Parameters["@TransparencyRegister_EmployeeNumber"].Value.ToString();
              string TransparencyRegister_FirstName = e.Command.Parameters["@TransparencyRegister_FirstName"].Value.ToString();
              string TransparencyRegister_LastName = e.Command.Parameters["@TransparencyRegister_LastName"].Value.ToString();
              string TransparencyRegister_CreatedBy = e.Command.Parameters["@TransparencyRegister_CreatedBy"].Value.ToString();

              string TransparencyRegister_Status = e.Command.Parameters["@TransparencyRegister_Status"].Value.ToString();
              string TransparencyRegister_StatusDate = e.Command.Parameters["@TransparencyRegister_StatusDate"].Value.ToString();
              string TransparencyRegister_StatusApprovedRejectedBy = e.Command.Parameters["@TransparencyRegister_StatusApprovedRejectedBy"].Value.ToString();
              string TransparencyRegister_StatusMessage = e.Command.Parameters["@TransparencyRegister_StatusMessage"].Value.ToString();

              string TransparencyRegisterCreatedByNameSurname = SqlDataSource_TransparencyRegister_Form_Updated_TransparencyRegisterCreatedByNameSurname(TransparencyRegister_CreatedBy);

              string TransparencyRegisterStatusApprovedRejectedByNameSurname = SqlDataSource_TransparencyRegister_Form_Updated_TransparencyRegisterStatusApprovedRejectedByNameSurname(TransparencyRegister_StatusApprovedRejectedBy);

              string TransparencyRegisterEmployeeNumberEmail = SqlDataSource_TransparencyRegister_Form_Updated_TransparencyRegisterEmployeeNumberEmail(TransparencyRegister_EmployeeNumber);

              string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("75");
              string URLAuthority = InfoQuestWCF.InfoQuest_All.All_LinkAuthority();
              string FormName = InfoQuestWCF.InfoQuest_All.All_FormName("44");
              string BodyString = EmailTemplate;

              BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + TransparencyRegister_FirstName + " " + TransparencyRegister_LastName + "");
              BodyString = BodyString.Replace(";replace;FormsName;replace;", "" + FormName + "");
              BodyString = BodyString.Replace(";replace;TransparencyRegisterEmployeeNumber;replace;", "" + TransparencyRegister_EmployeeNumber + "");
              BodyString = BodyString.Replace(";replace;TransparencyRegisterFirstName;replace;", "" + TransparencyRegister_FirstName + "");
              BodyString = BodyString.Replace(";replace;TransparencyRegisterLastName;replace;", "" + TransparencyRegister_LastName + "");
              BodyString = BodyString.Replace(";replace;TransparencyRegisterCreatedBy;replace;", "" + TransparencyRegisterCreatedByNameSurname + " (" + TransparencyRegister_CreatedBy + ")");
              BodyString = BodyString.Replace(";replace;TransparencyRegisterStatus;replace;", "" + TransparencyRegister_Status + "");
              BodyString = BodyString.Replace(";replace;TransparencyRegisterStatusDate;replace;", "" + TransparencyRegister_StatusDate + "");
              BodyString = BodyString.Replace(";replace;TransparencyRegisterStatusApprovedRejectedBy;replace;", "" + TransparencyRegisterStatusApprovedRejectedByNameSurname + " (" + TransparencyRegister_StatusApprovedRejectedBy + ")");
              BodyString = BodyString.Replace(";replace;TransparencyRegisterStatusMessage;replace;", "" + TransparencyRegister_StatusMessage + "");
              BodyString = BodyString.Replace(";replace;URLAuthority;replace;", "" + URLAuthority + "");
              BodyString = BodyString.Replace(";replace;TransparencyRegisterId;replace;", "" + TransparencyRegister_Id + "");

              string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
              string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
              string EmailBody = HeaderString + BodyString + FooterString;

              string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", TransparencyRegisterEmployeeNumberEmail, FormName, EmailBody);

              if (!string.IsNullOrEmpty(EmailSend))
              {
                EmailSend = "";
              }

              EmailBody = "";
              EmailTemplate = "";
              URLAuthority = "";
              FormName = "";
            }

            RedirectToList();
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_TransparencyRegister, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register Print", "InfoQuest_Print.aspx?PrintPage=Form_TransparencyRegister&PrintValue=" + Request.QueryString["TransparencyRegister_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_TransparencyRegister, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_TransparencyRegister, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register Email", "InfoQuest_Email.aspx?EmailPage=Form_TransparencyRegister&EmailValue=" + Request.QueryString["TransparencyRegister_Id"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_TransparencyRegister, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }

    protected static string SqlDataSource_TransparencyRegister_Form_Updated_TransparencyRegisterCreatedByNameSurname(string transparencyRegister_CreatedBy)
    {
      string TransparencyRegisterCreatedByNameSurname = "";

      DataTable DataTable_TransparencyRegisterCreatedByNameSurname = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(transparencyRegister_CreatedBy);
      if (DataTable_TransparencyRegisterCreatedByNameSurname.Columns.Count != 1)
      {
        if (DataTable_TransparencyRegisterCreatedByNameSurname.Rows.Count > 0)
        {
          foreach (DataRow DataRow_TransparencyRegisterCreatedByNameSurname in DataTable_TransparencyRegisterCreatedByNameSurname.Rows)
          {
            TransparencyRegisterCreatedByNameSurname = DataRow_TransparencyRegisterCreatedByNameSurname["FirstName"].ToString() + " " + DataRow_TransparencyRegisterCreatedByNameSurname["LastName"].ToString();
          }
        }
      }

      return TransparencyRegisterCreatedByNameSurname;
    }

    protected static string SqlDataSource_TransparencyRegister_Form_Updated_TransparencyRegisterStatusApprovedRejectedByNameSurname(string transparencyRegister_StatusApprovedRejectedBy)
    {
      string TransparencyRegisterStatusApprovedRejectedByNameSurname = "";

      DataTable DataTable_TransparencyRegisterStatusApprovedRejectedByNameSurname = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(transparencyRegister_StatusApprovedRejectedBy);
      if (DataTable_TransparencyRegisterStatusApprovedRejectedByNameSurname.Columns.Count != 1)
      {
        if (DataTable_TransparencyRegisterStatusApprovedRejectedByNameSurname.Rows.Count > 0)
        {
          foreach (DataRow DataRow_TransparencyRegisterStatusApprovedRejectedByNameSurname in DataTable_TransparencyRegisterStatusApprovedRejectedByNameSurname.Rows)
          {
            TransparencyRegisterStatusApprovedRejectedByNameSurname = DataRow_TransparencyRegisterStatusApprovedRejectedByNameSurname["FirstName"].ToString() + " " + DataRow_TransparencyRegisterStatusApprovedRejectedByNameSurname["LastName"].ToString();
          }
        }
      }

      return TransparencyRegisterStatusApprovedRejectedByNameSurname;
    }

    protected static string SqlDataSource_TransparencyRegister_Form_Updated_TransparencyRegisterEmployeeNumberEmail(string transparencyRegister_EmployeeNumber)
    {
      string TransparencyRegisterEmployeeNumberEmail = "";

      DataTable DataTable_TransparencyRegisterEmployeeNumberEmail = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_EmployeeNumber(transparencyRegister_EmployeeNumber);
      if (DataTable_TransparencyRegisterEmployeeNumberEmail.Columns.Count != 1)
      {
        if (DataTable_TransparencyRegisterEmployeeNumberEmail.Rows.Count > 0)
        {
          foreach (DataRow DataRow_TransparencyRegisterEmployeeNumberEmail in DataTable_TransparencyRegisterEmployeeNumberEmail.Rows)
          {
            TransparencyRegisterEmployeeNumberEmail = DataRow_TransparencyRegisterEmployeeNumberEmail["Email"].ToString();
          }
        }
      }

      return TransparencyRegisterEmployeeNumberEmail;
    }
    //---END--- --Edit--//


    protected void FormView_TransparencyRegister_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["TransparencyRegister_Id"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register Form", "Form_TransparencyRegister.aspx?TransparencyRegister_Id=" + Request.QueryString["TransparencyRegister_Id"] + ""), false);
          }
        }
      }
    }

    protected void FormView_TransparencyRegister_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_TransparencyRegister_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["TransparencyRegister_Id"] != null)
        {
          EditDataBound();
        }
      }

      if (FormView_TransparencyRegister_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["TransparencyRegister_Id"] != null)
        {
          ReadOnlyDataBound();
        }
      }
    }

    protected void EditDataBound()
    {
      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 44";
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
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_EditPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_EditPrint")).Visible = true;
      }

      if (Email == "False")
      {
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_EditEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_EditEmail")).Visible = true;
      }

      Email = "";
      Print = "";
    }

    protected void ReadOnlyDataBound()
    {
      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 44";
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
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_ItemPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_ItemPrint")).Visible = true;
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register Print", "InfoQuest_Print.aspx?PrintPage=Form_TransparencyRegister&PrintValue=" + Request.QueryString["TransparencyRegister_Id"] + "") + "')";
      }

      if (Email == "False")
      {
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_ItemEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_ItemEmail")).Visible = true;
        ((Button)FormView_TransparencyRegister_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register Email", "InfoQuest_Email.aspx?EmailPage=Form_TransparencyRegister&EmailValue=" + Request.QueryString["TransparencyRegister_Id"] + "") + "')";
      }

      Email = "";
      Print = "";
    }


    //--START-- --Insert Controls--//
    protected void InsertRegisterPostBackControl()
    {
      Button Button_InsertUploadFile = (Button)FormView_TransparencyRegister_Form.FindControl("Button_InsertUploadFile");

      ScriptManager ScriptManager_Insert = ScriptManager.GetCurrent(Page);

      ScriptManager_Insert.RegisterPostBackControl(Button_InsertUploadFile);
    }

    protected void Button_InsertFindEmployee_Click(object sender, EventArgs e)
    {
      string CurrentEmployeeNumber = ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertEmployeeNumber")).Text;
      string Error = "";

      if (string.IsNullOrEmpty(CurrentEmployeeNumber))
      {
        Error = "Employee number is required";
      }
      else
      {
        string FirstName = "";
        string LastName = "";
        string JobTitle = "";
        string Department = "";
        string ReportsTo = "";
        string ManagerEmployeeNumber = "";
        DataTable DataTable_DataEmployee;
        using (DataTable_DataEmployee = new DataTable())
        {
          DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
          DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_FindEmployeeInfo_SearchEmployeeNumber(((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertEmployeeNumber")).Text).Copy();
          if (DataTable_DataEmployee.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Error = DataRow_Row["Error"].ToString();
              if (Error == "No Employee Data")
              {
                Error = "Employee detail not found for specific employee number<br/>Please log a call with Contact Centre for employee number to be updated on Vision";
              }
            }
          }
          else if (DataTable_DataEmployee.Columns.Count != 1)
          {
            if (DataTable_DataEmployee.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
              {
                FirstName = DataRow_Row["FirstName"].ToString();
                LastName = DataRow_Row["LastName"].ToString();
                JobTitle = DataRow_Row["JobTitle"].ToString();
                Department = DataRow_Row["Department"].ToString();
                ReportsTo = DataRow_Row["ReportsTo"].ToString();
                ManagerEmployeeNumber = DataRow_Row["ManagerEmployeeNumber"].ToString();
              }
            }
            else
            {
              Error = "Employee detail not found for specific employee number<br/>Please log a call with Contact Centre for employee number to be updated on Vision";
            }
          }
        }

        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertFirstName")).Text = FirstName;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertFirstName")).Value = FirstName;
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertLastName")).Text = LastName;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertLastName")).Value = LastName;
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertJobTitle")).Text = JobTitle;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertJobTitle")).Value = JobTitle;
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertDepartment")).Text = Department;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertDepartment")).Value = Department;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmployeeNumber")).Value = ManagerEmployeeNumber;
        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertReportsTo")).Value = ReportsTo;

        FirstName = "";
        LastName = "";
        JobTitle = "";
        Department = "";
        ReportsTo = "";
        ManagerEmployeeNumber = "";


        if (string.IsNullOrEmpty(Error))
        {
          Button_InsertFindEmployee_CEOApprover();

          Error = Button_InsertFindEmployee_CurrentManager();
        }
      }

      if (!string.IsNullOrEmpty(Error))
      {
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertEmployeeError")).Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Error + "</div>", CultureInfo.CurrentCulture);
      }
      else
      {
        ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertEmployeeError")).Text = "";
      }

      CurrentEmployeeNumber = "";
      Error = "";

      InsertRegisterPostBackControl();
    }

    private void Button_InsertFindEmployee_CEOApprover()
    {
      if (((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertReportsTo")).Value == "00000001")
      {
        string CEOManagerEmployeeNumber = "";
        string SQLStringCEOManagerEmployeeNumber = "SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE ListCategory_Id = 185";
        using (SqlCommand SqlCommand_CEOManagerEmployeeNumber = new SqlCommand(SQLStringCEOManagerEmployeeNumber))
        {
          DataTable DataTable_CEOManagerEmployeeNumber;
          using (DataTable_CEOManagerEmployeeNumber = new DataTable())
          {
            DataTable_CEOManagerEmployeeNumber.Locale = CultureInfo.CurrentCulture;
            DataTable_CEOManagerEmployeeNumber = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CEOManagerEmployeeNumber).Copy();
            if (DataTable_CEOManagerEmployeeNumber.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_CEOManagerEmployeeNumber.Rows)
              {
                CEOManagerEmployeeNumber = DataRow_Row["ListItem_Name"].ToString();
              }
            }
          }
        }

        ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmployeeNumber")).Value = CEOManagerEmployeeNumber;
      }
    }

    private string Button_InsertFindEmployee_CurrentManager()
    {
      string Error = "";

      string CurrentManagerName = "";
      string CurrentManagerFirstName = "";
      string CurrentManagerLastName = "";
      string CurrentManagerUserName = "";
      string CurrentManagerEmail = "";
      DataTable DataTable_CurrentManagerUserName = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_EmployeeNumber(((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmployeeNumber")).Value);
      if (DataTable_CurrentManagerUserName.Columns.Count == 1)
      {
        foreach (DataRow DataRow_Row in DataTable_CurrentManagerUserName.Rows)
        {
          Error = DataRow_Row["Error"].ToString();
          if (Error == "No Employee Data")
          {
            Error = "Managers username not found for specific employee number<br/>Please log a call with Contact Centre for employees manager to be updated on Active Directory";
          }
        }
      }
      else if (DataTable_CurrentManagerUserName.Columns.Count != 1)
      {
        if (DataTable_CurrentManagerUserName.Rows.Count > 0)
        {
          foreach (DataRow DataRow_CurrentManagerUserName in DataTable_CurrentManagerUserName.Rows)
          {
            CurrentManagerName = Convert.ToString(DataRow_CurrentManagerUserName["FirstName"].ToString() + " " + DataRow_CurrentManagerUserName["LastName"].ToString(), CultureInfo.CurrentCulture);
            CurrentManagerFirstName = DataRow_CurrentManagerUserName["FirstName"].ToString();
            CurrentManagerLastName = DataRow_CurrentManagerUserName["LastName"].ToString();
            CurrentManagerUserName = DataRow_CurrentManagerUserName["UserName"].ToString();
            CurrentManagerEmail = DataRow_CurrentManagerUserName["Email"].ToString();
          }
        }
        else
        {
          Error = "Managers username not found for specific employee number<br/>Please log a call with Contact Centre for Employees Manager to be updated on Active Directory";
        }
      }

      ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertManagerName")).Text = CurrentManagerName;
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerFirstName")).Value = CurrentManagerFirstName;
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerLastName")).Value = CurrentManagerLastName;
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerUserName")).Value = CurrentManagerUserName;
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmail")).Value = CurrentManagerEmail;

      CurrentManagerName = "";
      CurrentManagerFirstName = "";
      CurrentManagerLastName = "";
      CurrentManagerUserName = "";
      CurrentManagerEmail = "";

      return Error;
    }

    protected void Button_InsertClearEmployee_Click(object sender, EventArgs e)
    {
      ((TextBox)FormView_TransparencyRegister_Form.FindControl("TextBox_InsertEmployeeNumber")).Text = "";
      ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertFirstName")).Text = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertFirstName")).Value = "";
      ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertLastName")).Text = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertLastName")).Value = "";
      ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertJobTitle")).Text = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertJobTitle")).Value = "";
      ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertDepartment")).Text = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertDepartment")).Value = "";
      ((Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertManagerName")).Text = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmployeeNumber")).Value = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerFirstName")).Value = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerLastName")).Value = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerUserName")).Value = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertManagerEmail")).Value = "";
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertReportsTo")).Value = "";

      FileCleanUp();
      InsertRegisterPostBackControl();
    }

    protected void GridView_InsertTransparencyRegister_Classification_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_InsertTransparencyRegister_Classification_DataBound(object sender, EventArgs e)
    {
      ((HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertTotalRecords")).Value = ((GridView)FormView_TransparencyRegister_Form.FindControl("GridView_InsertTransparencyRegister_Classification")).Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void GridView_InsertTransparencyRegister_Classification_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_InsertCancel_Click(object sender, EventArgs e)
    {
      FileCleanUp();
      RedirectToList();
    }

    protected void Button_InsertClear_Click(object sender, EventArgs e)
    {
      FileCleanUp();
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register New Form", "Form_TransparencyRegister.aspx"), false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void GridView_EditTransparencyRegister_Classification_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_EditTransparencyRegister_Classification_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_EditCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register New Form", "Form_TransparencyRegister.aspx"), false);
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
    protected void GridView_ItemTransparencyRegister_Classification_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemTransparencyRegister_Classification_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_ItemCancel_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Transparency Register New Form", "Form_TransparencyRegister.aspx"), false);
    }
    //---END--- --Item Controls--//


    //--START-- --File--//
    protected Dictionary<string, string> FileContentTypeHandler = new Dictionary<string, string>();

    public static string DatabaseFileName(object transparencyRegister_File_FileName)
    {
      string DatabaseFileName = "";
      if (transparencyRegister_File_FileName != null)
      {
        DatabaseFileName = "" + transparencyRegister_File_FileName.ToString() + "";
      }

      return DatabaseFileName;
    }

    protected void RetrieveDatabaseFile(object sender, EventArgs e)
    {
      LinkButton LinkButton_TransparencyRegisterFile = (LinkButton)sender;
      string FileId = LinkButton_TransparencyRegisterFile.CommandArgument.ToString();

      Session["TransparencyRegisterFileFileName"] = "";
      Session["TransparencyRegisterFileFileContentType"] = "";
      Session["TransparencyRegisterFileZipFileName"] = "";
      Session["TransparencyRegisterFileZipFileContentType"] = "";
      Session["TransparencyRegisterFileData"] = "";
      string SQLStringTransparencyRegisterFile = "SELECT TransparencyRegister_File_FileName , TransparencyRegister_File_FileContentType , TransparencyRegister_File_ZipFileName , TransparencyRegister_File_ZipFileContentType , TransparencyRegister_File_Data FROM Form_TransparencyRegister_File WHERE TransparencyRegister_File_Id = @TransparencyRegister_File_Id";
      using (SqlCommand SqlCommand_TransparencyRegisterFile = new SqlCommand(SQLStringTransparencyRegisterFile))
      {
        SqlCommand_TransparencyRegisterFile.Parameters.AddWithValue("@TransparencyRegister_File_Id", FileId);
        DataTable DataTable_TransparencyRegisterFile;
        using (DataTable_TransparencyRegisterFile = new DataTable())
        {
          DataTable_TransparencyRegisterFile.Locale = CultureInfo.CurrentCulture;
          DataTable_TransparencyRegisterFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_TransparencyRegisterFile).Copy();
          if (DataTable_TransparencyRegisterFile.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_TransparencyRegisterFile.Rows)
            {
              Session["TransparencyRegisterFileFileName"] = DataRow_Row["TransparencyRegister_File_FileName"];
              Session["TransparencyRegisterFileFileContentType"] = DataRow_Row["TransparencyRegister_File_FileContentType"];
              Session["TransparencyRegisterFileZipFileName"] = DataRow_Row["TransparencyRegister_File_ZipFileName"];
              Session["TransparencyRegisterFileZipFileContentType"] = DataRow_Row["TransparencyRegister_File_ZipFileContentType"];
              Session["TransparencyRegisterFileData"] = DataRow_Row["TransparencyRegister_File_Data"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["TransparencyRegisterFileData"].ToString()))
      {
        Byte[] Byte_FileData = (Byte[])Session["TransparencyRegisterFileData"];

        //Stream Stream_ZipFile = new MemoryStream(Byte_FileData);
        //ZipArchive ZipArchive_ZipFile = new ZipArchive(Stream_ZipFile, ZipArchiveMode.Read);
        //ZipArchiveEntry ZipArchiveEntry_ZipFile = ZipArchive_ZipFile.GetEntry(Session["TransparencyRegisterFileFileName"].ToString());
        //FileStream FileStream_ZipFile = ZipArchiveEntry_ZipFile.Open() as FileStream;
        //BinaryReader BinaryReader_ZipFile = new BinaryReader(ZipArchiveEntry_ZipFile.Open());
        //Byte[] Byte_ZipFile = BinaryReader_ZipFile.ReadBytes((Int32)ZipArchiveEntry_ZipFile.Length);

        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = Session["TransparencyRegisterFileFileContentType"].ToString();
        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Session["TransparencyRegisterFileFileName"].ToString() + "\"");
        //Response.BinaryWrite(Byte_ZipFile);
        Response.BinaryWrite(Byte_FileData);
        Response.Flush();
        Response.End();
      }

      Session.Remove("TransparencyRegisterFileFileName");
      Session.Remove("TransparencyRegisterFileFileContentType");
      Session.Remove("TransparencyRegisterFileZipFileName");
      Session.Remove("TransparencyRegisterFileZipFileContentType");
      Session.Remove("TransparencyRegisterFileData");
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

    #region Create and Upload Zip Files --Future Expansion--
    //private String UploadPath()
    //{
    //  String UploadPath = Server.MapPath("App_Files/InfoQuest_Zip/");

    //  return UploadPath;
    //}

    //private String UploadUserFolder()
    //{
    //  String UserFolder = Request.ServerVariables["LOGON_USER"];
    //  UserFolder = UserFolder.Substring(UserFolder.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
    //  UserFolder = "Form_TransparencyRegister_" + UserFolder;
    //  UserFolder = UserFolder.ToLower(CultureInfo.CurrentCulture);

    //  return UserFolder;
    //}

    //private void DirectoryCleanUp()
    //{
    //  if (Directory.Exists(UploadPath() + UploadUserFolder()))
    //  {
    //    string[] UploadedFiles = Directory.GetFiles(UploadPath() + UploadUserFolder(), "*.*", SearchOption.AllDirectories);

    //    if (UploadedFiles.Length == 0)
    //    {
    //      Directory.Delete(UploadPath() + UploadUserFolder(), true);
    //    }
    //  }
    //}
    #endregion
    //---END--- --File--//


    //--START-- --File Insert--//
    protected void SqlDataSource_TransparencyRegister_InsertFile_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        HiddenField HiddenField_InsertFile = (HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertFile");
        HiddenField_InsertFile.Value = TotalRecords.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_InsertFile_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_InsertFile_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_InsertUploadFile_OnClick(object sender, EventArgs e)
    {
      string UploadMessage = "";

      Label Label_InsertMessageFile = (Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertMessageFile");
      FileUpload FileUpload_InsertFile = (FileUpload)FormView_TransparencyRegister_Form.FindControl("FileUpload_InsertFile");

      if (!FileUpload_InsertFile.HasFiles)
      {
        UploadMessage = UploadMessage + Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>No file chosen</span>", CultureInfo.CurrentCulture);
      }
      else
      {
        foreach (HttpPostedFile HttpPostedFile_File in FileUpload_InsertFile.PostedFiles)
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
            HiddenField HiddenField_InsertTransparencyRegisterIdTemp = (HiddenField)FormView_TransparencyRegister_Form.FindControl("HiddenField_InsertTransparencyRegisterIdTemp");

            Session["TransparencyRegisterFileId"] = "";
            string SQLStringExistingFile = "SELECT TransparencyRegister_File_Id FROM Form_TransparencyRegister_File WHERE TransparencyRegister_File_CreatedBy = @TransparencyRegister_File_CreatedBy AND TransparencyRegister_File_Temp_TransparencyRegister_Id = @TransparencyRegister_File_Temp_TransparencyRegister_Id AND TransparencyRegister_File_FileName = @TransparencyRegister_File_FileName";
            using (SqlCommand SqlCommand_ExistingFile = new SqlCommand(SQLStringExistingFile))
            {
              SqlCommand_ExistingFile.Parameters.AddWithValue("@TransparencyRegister_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@TransparencyRegister_File_Temp_TransparencyRegister_Id", HiddenField_InsertTransparencyRegisterIdTemp.Value);
              SqlCommand_ExistingFile.Parameters.AddWithValue("@TransparencyRegister_File_FileName", FileName);
              DataTable DataTable_ExistingFile;
              using (DataTable_ExistingFile = new DataTable())
              {
                DataTable_ExistingFile.Locale = CultureInfo.CurrentCulture;
                DataTable_ExistingFile = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ExistingFile).Copy();
                if (DataTable_ExistingFile.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_ExistingFile.Rows)
                  {
                    Session["TransparencyRegisterFileId"] = DataRow_Row["TransparencyRegister_File_Id"];
                  }
                }
              }
            }

            if (!string.IsNullOrEmpty(Session["TransparencyRegisterFileId"].ToString()))
            {
              UploadMessage = Convert.ToString("<span style='color:#d46e6e;'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span>", CultureInfo.CurrentCulture);
            }
            else
            {
              string ZipFileName = FileName.Substring(0, FileName.LastIndexOf(".", StringComparison.CurrentCulture)) + ".zip";
              string ZipFileContentType = "application/zip";

              Stream Stream_File = HttpPostedFile_File.InputStream;
              BinaryReader BinaryReader_File = new BinaryReader(Stream_File);
              Byte[] Byte_File = BinaryReader_File.ReadBytes((Int32)Stream_File.Length);


              //Stream Stream_File = HttpPostedFile_File.InputStream;
              //Stream Stream_ZipFile = new MemoryStream();
              //Stream_File.CopyTo(Stream_ZipFile);
              //Stream_ZipFile.Position = 0;
              //ZipArchive ZipArchive_ZipFile = new ZipArchive(Stream_ZipFile, ZipArchiveMode.Create, true);
              //ZipArchive_ZipFile.Dispose();
              //BinaryReader BinaryReader_ZipFile = new BinaryReader(Stream_ZipFile);
              //Byte[] Byte_ZipFile = BinaryReader_ZipFile.ReadBytes((Int32)Stream_ZipFile.Length);


              //if (!Directory.Exists(UploadPath() + UploadUserFolder()))
              //{
              //  Directory.CreateDirectory(UploadPath() + UploadUserFolder());
              //}

              //File.Delete(UploadPath() + UploadUserFolder() + "\\" + FileName);
              //File.Delete(UploadPath() + UploadUserFolder() + "\\" + ZipFileName);

              //HttpPostedFile_File.SaveAs(UploadPath() + UploadUserFolder() + "\\" + FileName);

              //using (ZipArchive ZipArchive_PathAndName = ZipFile.Open(UploadPath() + UploadUserFolder() + "\\" + ZipFileName, ZipArchiveMode.Update))
              //{
              //  ZipArchive_PathAndName.CreateEntryFromFile(UploadPath() + UploadUserFolder() + "\\" + FileName, FileName);
              //}

              //using (FileStream FileStream_ZipFile = new FileStream(UploadPath() + UploadUserFolder() + "\\" + ZipFileName, FileMode.Open, FileAccess.Read))
              //{
              //  BinaryReader BinaryReader_ZipFile = new BinaryReader(FileStream_ZipFile);
              //  Byte[] Byte_ZipFile = BinaryReader_ZipFile.ReadBytes((Int32)FileStream_ZipFile.Length);

              string SQLStringInsertFile = @"INSERT INTO Form_TransparencyRegister_File ( TransparencyRegister_Id , TransparencyRegister_File_Temp_TransparencyRegister_Id , TransparencyRegister_File_FileName , TransparencyRegister_File_FileContentType , TransparencyRegister_File_ZipFileName , TransparencyRegister_File_ZipFileContentType , TransparencyRegister_File_Data , TransparencyRegister_File_CreatedDate , TransparencyRegister_File_CreatedBy ) VALUES ( @TransparencyRegister_Id , @TransparencyRegister_File_Temp_TransparencyRegister_Id , @TransparencyRegister_File_FileName , @TransparencyRegister_File_FileContentType , @TransparencyRegister_File_ZipFileName , @TransparencyRegister_File_ZipFileContentType , @TransparencyRegister_File_Data , @TransparencyRegister_File_CreatedDate , @TransparencyRegister_File_CreatedBy )";
              using (SqlCommand SqlCommand_InsertFile = new SqlCommand(SQLStringInsertFile))
              {
                SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_Id", DBNull.Value);
                SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_File_Temp_TransparencyRegister_Id", HiddenField_InsertTransparencyRegisterIdTemp.Value);
                SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_File_FileName", FileName);
                SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_File_FileContentType", FileContentTypeValue);
                SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_File_ZipFileName", ZipFileName);
                SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_File_ZipFileContentType", ZipFileContentType);
                //SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_File_Data", Byte_ZipFile);
                SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_File_Data", Byte_File);
                SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_File_CreatedDate", DateTime.Now);
                SqlCommand_InsertFile.Parameters.AddWithValue("@TransparencyRegister_File_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertFile);
              }
              //}

              //File.Delete(UploadPath() + UploadUserFolder() + "\\" + FileName);
              //File.Delete(UploadPath() + UploadUserFolder() + "\\" + ZipFileName);
              //DirectoryCleanUp();

              GridView GridView_InsertFile = (GridView)FormView_TransparencyRegister_Form.FindControl("GridView_InsertFile");
              SqlDataSource_TransparencyRegister_File_InsertFile.SelectParameters["TransparencyRegister_File_Temp_TransparencyRegister_Id"].DefaultValue = HiddenField_InsertTransparencyRegisterIdTemp.Value;
              GridView_InsertFile.DataBind();
            }
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

      Button Button_InsertAdd = (Button)FormView_TransparencyRegister_Form.FindControl("Button_InsertAdd");

      ToolkitScriptManager_TransparencyRegister.SetFocus(Button_InsertAdd);
      InsertRegisterPostBackControl();
      Label_InsertMessageFile.Text = UploadMessage;
    }

    protected void Button_InsertDeleteFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      GridView GridView_InsertFile = (GridView)FormView_TransparencyRegister_Form.FindControl("GridView_InsertFile");
      Label Label_InsertMessageFile = (Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertMessageFile");

      for (int i = 0; i < GridView_InsertFile.Rows.Count; i++)
      {
        CheckBox CheckBox_InsertFile = (CheckBox)GridView_InsertFile.Rows[i].Cells[0].FindControl("CheckBox_InsertFile");
        Int32 FileId = 0;

        if (CheckBox_InsertFile.Checked == true)
        {
          FileId = Convert.ToInt32(CheckBox_InsertFile.CssClass, CultureInfo.CurrentCulture);

          string SQLStringTransparencyRegisterFile = "DELETE FROM Form_TransparencyRegister_File WHERE TransparencyRegister_File_Id = @TransparencyRegister_File_Id";
          using (SqlCommand SqlCommand_TransparencyRegisterFile = new SqlCommand(SQLStringTransparencyRegisterFile))
          {
            SqlCommand_TransparencyRegisterFile.Parameters.AddWithValue("@TransparencyRegister_File_Id", FileId);

            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_TransparencyRegisterFile);

            DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
          }
        }
      }

      Button Button_InsertAdd = (Button)FormView_TransparencyRegister_Form.FindControl("Button_InsertAdd");

      ToolkitScriptManager_TransparencyRegister.SetFocus(Button_InsertAdd);
      InsertRegisterPostBackControl();
      Label_InsertMessageFile.Text = DeleteMessage;
      GridView_InsertFile.DataBind();
    }

    protected void Button_InsertDeleteAllFile_OnClick(object sender, EventArgs e)
    {
      string DeleteMessage = "";

      GridView GridView_InsertFile = (GridView)FormView_TransparencyRegister_Form.FindControl("GridView_InsertFile");
      Label Label_InsertMessageFile = (Label)FormView_TransparencyRegister_Form.FindControl("Label_InsertMessageFile");

      for (int i = 0; i < GridView_InsertFile.Rows.Count; i++)
      {
        CheckBox CheckBox_InsertFile = (CheckBox)GridView_InsertFile.Rows[i].Cells[0].FindControl("CheckBox_InsertFile");
        Int32 FileId = 0;

        FileId = Convert.ToInt32(CheckBox_InsertFile.CssClass, CultureInfo.CurrentCulture);

        string SQLStringTransparencyRegisterFile = "DELETE FROM Form_TransparencyRegister_File WHERE TransparencyRegister_File_Id = @TransparencyRegister_File_Id";
        using (SqlCommand SqlCommand_TransparencyRegisterFile = new SqlCommand(SQLStringTransparencyRegisterFile))
        {
          SqlCommand_TransparencyRegisterFile.Parameters.AddWithValue("@TransparencyRegister_File_Id", FileId);

          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_TransparencyRegisterFile);

          DeleteMessage = "<span style='color:#77cf9c;'>File Deletion Successful</span>";
        }
      }

      Button Button_InsertAdd = (Button)FormView_TransparencyRegister_Form.FindControl("Button_InsertAdd");

      ToolkitScriptManager_TransparencyRegister.SetFocus(Button_InsertAdd);
      InsertRegisterPostBackControl();
      Label_InsertMessageFile.Text = DeleteMessage;
      GridView_InsertFile.DataBind();
    }

    protected void Button_InsertUploadFile_DataBinding(object sender, EventArgs e)
    {
      InsertRegisterPostBackControl();
    }

    protected void Button_InsertDeleteFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_InsertDeleteFile = (Button)sender;
      ScriptManager ScriptManager_InsertDeleteFile = ScriptManager.GetCurrent(Page);
      ScriptManager_InsertDeleteFile.RegisterPostBackControl(Button_InsertDeleteFile);
    }

    protected void Button_InsertDeleteAllFile_DataBinding(object sender, EventArgs e)
    {
      Button Button_InsertDeleteAllFile = (Button)sender;
      ScriptManager ScriptManager_InsertDeleteAllFile = ScriptManager.GetCurrent(Page);
      ScriptManager_InsertDeleteAllFile.RegisterPostBackControl(Button_InsertDeleteAllFile);
    }

    protected void LinkButton_InsertFile_DataBinding(object sender, EventArgs e)
    {
      if (e != null)
      {
        LinkButton LinkButton_InsertFile = (LinkButton)sender;
        ScriptManager ScriptManager_InsertFile = ScriptManager.GetCurrent(Page);
        ScriptManager_InsertFile.RegisterPostBackControl(LinkButton_InsertFile);
      }
    }
    //---END--- --File Insert--//


    //--START-- --File Edit--//
    protected void GridView_EditFile_PreRender(object sender, EventArgs e)
    {
      if (e != null)
      {
        GridView GridView_List = (GridView)sender;
        GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
        if (GridViewRow_List != null)
        {
          GridViewRow_List.Visible = true;
        }
      }
    }

    protected void GridView_EditFile_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void LinkButton_EditFile_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_EditFile = (LinkButton)sender;
      ScriptManager ScriptManager_EditFile = ScriptManager.GetCurrent(Page);
      ScriptManager_EditFile.RegisterPostBackControl(LinkButton_EditFile);
    }
    //---END--- --File Edit--//


    //--START-- --File Item--//
    protected void GridView_ItemFile_PreRender(object sender, EventArgs e)
    {
      if (e != null)
      {
        GridView GridView_List = (GridView)sender;
        GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
        if (GridViewRow_List != null)
        {
          GridViewRow_List.Visible = true;
        }
      }
    }

    protected void GridView_ItemFile_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void LinkButton_ItemFile_DataBinding(object sender, EventArgs e)
    {
      LinkButton LinkButton_ItemFile = (LinkButton)sender;
      ScriptManager ScriptManager_ItemFile = ScriptManager.GetCurrent(Page);
      ScriptManager_ItemFile.RegisterPostBackControl(LinkButton_ItemFile);
    }
    //---END--- --File Item--//
    //---END--- --TableForm--// 
  }
}