using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_FSM_BusinessUnit : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_FSM_BusinessUnit, this.GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          PageTitle();

          SetBusinessUnitVisibility();

          TableBusinessUnitVisible();

          if (string.IsNullOrEmpty(Request.QueryString["BusinessUnitKey"]))
          {
            if (((HiddenField)FormView_FSM_BusinessUnit_Form.FindControl("HiddenField_Insert")) != null)
            {
              string SQLStringSourceSystem = "SELECT SourceSystemKey , SourceSystemName FROM Mapping.SourceSystem WHERE MappingTypeKey IN ( SELECT MappingTypeKey FROM Mapping.MappingType WHERE LookupTable = 'BusinessUnit.BusinessUnitType' ) ORDER BY SourceSystemName";
              using (SqlCommand SqlCommand_SourceSystem = new SqlCommand(SQLStringSourceSystem))
              {
                DataTable DataTable_SourceSystem;
                using (DataTable_SourceSystem = new DataTable())
                {
                  DataTable_SourceSystem.Locale = CultureInfo.CurrentCulture;
                  DataTable_SourceSystem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_SourceSystem, "PatientDetailFacilityStructure").Copy();
                  if (DataTable_SourceSystem.Rows.Count > 0)
                  {
                    ((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_InsertMappingBusinessUnit")).DataSource = DataTable_SourceSystem;
                    ((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_InsertMappingBusinessUnit")).DataBind();
                  }
                  else
                  {
                    ((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_InsertMappingBusinessUnit")).DataSource = null;
                    ((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_InsertMappingBusinessUnit")).DataBind();
                  }
                }
              }
            }            
          }
          else
          {
            if (((HiddenField)FormView_FSM_BusinessUnit_Form.FindControl("HiddenField_Edit")) != null)
            {
              string SQLStringMappingSourceSystem = @"SELECT BusinessUnit.SourceSystemKey , SourceSystem.SourceSystemName FROM Mapping.BusinessUnit LEFT JOIN Mapping.SourceSystem ON BusinessUnit.SourceSystemKey = SourceSystem.SourceSystemKey WHERE BusinessUnit.BusinessUnitKey = @BusinessUnitKey";
              using (SqlCommand SqlCommand_MappingSourceSystem = new SqlCommand(SQLStringMappingSourceSystem))
              {
                SqlCommand_MappingSourceSystem.Parameters.AddWithValue("@BusinessUnitKey", Request.QueryString["BusinessUnitKey"]);
                DataTable DataTable_MappingSourceSystem;
                using (DataTable_MappingSourceSystem = new DataTable())
                {
                  DataTable_MappingSourceSystem.Locale = CultureInfo.CurrentCulture;
                  DataTable_MappingSourceSystem.Merge(InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_MappingSourceSystem, "PatientDetailFacilityStructure"));

                  string SQLStringSourceSystem = "SELECT SourceSystemKey , SourceSystemName FROM Mapping.SourceSystem WHERE MappingTypeKey IN ( SELECT MappingTypeKey FROM Mapping.MappingType WHERE LookupTable = 'BusinessUnit.BusinessUnitType' ) ORDER BY SourceSystemName";
                  using (SqlCommand SqlCommand_SourceSystem = new SqlCommand(SQLStringSourceSystem))
                  {
                    DataTable DataTable_SourceSystem;
                    using (DataTable_SourceSystem = new DataTable())
                    {
                      DataTable_SourceSystem.Locale = CultureInfo.CurrentCulture;
                      DataTable_SourceSystem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_SourceSystem, "PatientDetailFacilityStructure").Copy();

                      if (DataTable_SourceSystem.Rows.Count > 0)
                      {
                        DataTable_MappingSourceSystem.Merge(DataTable_SourceSystem);
                      }
                    }
                  }

                  DataTable_MappingSourceSystem.DefaultView.Sort = "SourceSystemName ASC";

                  ((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_EditMappingBusinessUnit")).DataSource = DataTable_MappingSourceSystem.DefaultView.ToTable(true, "SourceSystemKey", "SourceSystemName");
                  ((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_EditMappingBusinessUnit")).DataBind();
                }
              }
            }
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('41'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("41");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_FSM_BusinessUnit.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Facility Structure Maintenance", "15");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitTypeKey.SelectCommand = "SELECT BusinessUnitTypeKey , BusinessUnitTypeName FROM BusinessUnit.BusinessUnitType ORDER BY BusinessUnitTypeName";

      SqlDataSource_FSM_BusinessUnit_InsertLocationKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_InsertLocationKey.SelectCommand = "SELECT LocationKey , LocationName FROM BusinessUnit.Location WHERE IsActive = 1 ORDER BY LocationName";

      SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitReportingGroupKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitReportingGroupKey.SelectCommand = "SELECT BusinessUnitReportingGroupKey , BusinessUnitReportingGroupName FROM BusinessUnit.BusinessUnitReportingGroup WHERE BusinessUnitTypeKey = @BusinessUnitTypeKey OR BusinessUnitReportingGroupKey = 0 ORDER BY BusinessUnitReportingGroupName";
      SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitReportingGroupKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitReportingGroupKey.SelectParameters.Add("BusinessUnitTypeKey", TypeCode.String, "");

      SqlDataSource_FSM_BusinessUnit_InsertHospitalTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_InsertHospitalTypeKey.SelectCommand = "SELECT HospitalTypeKey , HospitalTypeName FROM Hospital.HospitalType ORDER BY HospitalTypeName";

      SqlDataSource_FSM_BusinessUnit_EditBusinessUnitTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_EditBusinessUnitTypeKey.SelectCommand = "SELECT BusinessUnitTypeKey , BusinessUnitTypeName FROM BusinessUnit.BusinessUnitType ORDER BY BusinessUnitTypeName";

      SqlDataSource_FSM_BusinessUnit_EditLocationKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_EditLocationKey.SelectCommand = "SELECT LocationKey , LocationName FROM BusinessUnit.Location WHERE IsActive = 1 UNION SELECT BusinessUnit.LocationKey , Location.LocationName FROM BusinessUnit.BusinessUnit LEFT JOIN BusinessUnit.Location ON BusinessUnit.LocationKey = Location.LocationKey WHERE BusinessUnitKey = @BusinessUnitKey ORDER BY LocationName";
      SqlDataSource_FSM_BusinessUnit_EditLocationKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_EditLocationKey.SelectParameters.Add("BusinessUnitKey", TypeCode.String, Request.QueryString["BusinessUnitKey"]);

      SqlDataSource_FSM_BusinessUnit_EditBusinessUnitReportingGroupKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_EditBusinessUnitReportingGroupKey.SelectCommand = "SELECT BusinessUnitReportingGroupKey , BusinessUnitReportingGroupName FROM BusinessUnit.BusinessUnitReportingGroup WHERE BusinessUnitTypeKey = @BusinessUnitTypeKey OR BusinessUnitReportingGroupKey = 0 ORDER BY BusinessUnitReportingGroupName";
      SqlDataSource_FSM_BusinessUnit_EditBusinessUnitReportingGroupKey.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_EditBusinessUnitReportingGroupKey.SelectParameters.Add("BusinessUnitTypeKey", TypeCode.String, "");

      SqlDataSource_FSM_BusinessUnit_EditHospitalTypeKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_EditHospitalTypeKey.SelectCommand = "SELECT HospitalTypeKey , HospitalTypeName FROM Hospital.HospitalType ORDER BY HospitalTypeName";

      SqlDataSource_FSM_BusinessUnit_ItemMappingBusinessUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_ItemMappingBusinessUnit.SelectCommand = "SELECT SourceSystem.SourceSystemName , BusinessUnit.SourceSystemValue FROM Mapping.BusinessUnit LEFT JOIN Mapping.SourceSystem ON BusinessUnit.SourceSystemKey = SourceSystem.SourceSystemKey WHERE BusinessUnit.BusinessUnitKey = @BusinessUnitKey ORDER BY SourceSystem.SourceSystemName";
      SqlDataSource_FSM_BusinessUnit_ItemMappingBusinessUnit.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_ItemMappingBusinessUnit.SelectParameters.Add("BusinessUnitKey", TypeCode.String, Request.QueryString["BusinessUnitKey"]);

      SqlDataSource_FSM_BusinessUnit_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_BusinessUnit_Form.InsertCommand = @"INSERT INTO BusinessUnit.BusinessUnit ( BusinessUnitName , BusinessUnitTypeKey , LocationKey , BusinessUnitReportingGroupKey , BusinessUnitDefaultEntity , CreatedDate , CreatedBy , ModifiedDate , ModifiedBy , IsActive ) VALUES ( @BusinessUnitName , @BusinessUnitTypeKey , @LocationKey , @BusinessUnitReportingGroupKey , @BusinessUnitDefaultEntity , @CreatedDate , @CreatedBy , @ModifiedDate , @ModifiedBy , @IsActive ); 
                                                            SELECT @BusinessUnitKey = SCOPE_IDENTITY();
                                                            INSERT INTO Hospital.Hospital ( BusinessUnitKey , RegisteredName , ShortName , PracticeNumber , HospitalTypeKey ) SELECT @BusinessUnitKey , @RegisteredName , @ShortName , @PracticeNumber , @HospitalTypeKey WHERE @BusinessUnitTypeKey = 1";
      SqlDataSource_FSM_BusinessUnit_Form.SelectCommand = @"SELECT * FROM BusinessUnit.BusinessUnit LEFT JOIN Hospital.Hospital ON BusinessUnit.BusinessUnitKey = Hospital.BusinessUnitKey WHERE BusinessUnit.BusinessUnitKey = @BusinessUnitKey";
      SqlDataSource_FSM_BusinessUnit_Form.UpdateCommand = @"UPDATE BusinessUnit.BusinessUnit SET BusinessUnitName = @BusinessUnitName , BusinessUnitTypeKey = @BusinessUnitTypeKey , LocationKey = @LocationKey , BusinessUnitReportingGroupKey = @BusinessUnitReportingGroupKey , BusinessUnitDefaultEntity = @BusinessUnitDefaultEntity , CreatedDate = @CreatedDate , CreatedBy = @CreatedBy , ModifiedDate = @ModifiedDate , ModifiedBy = @ModifiedBy , IsActive = @IsActive WHERE BusinessUnitKey = @BusinessUnitKey;
                                                            MERGE Hospital.Hospital AS [Target]
                                                            USING (SELECT BusinessUnitKey , BusinessUnitTypeKey FROM BusinessUnit.BusinessUnit WHERE BusinessUnitKey = @BusinessUnitKey) AS [Source] (BusinessUnitKey , BusinessUnitTypeKey)
                                                            ON ([Target].BusinessUnitKey = [Source].BusinessUnitKey) 
                                                            WHEN NOT MATCHED AND BusinessUnitTypeKey = 1
		                                                          THEN INSERT ( BusinessUnitKey , RegisteredName , ShortName , PracticeNumber , HospitalTypeKey ) VALUES ( @BusinessUnitKey , @RegisteredName , @ShortName , @PracticeNumber , @HospitalTypeKey )
                                                            WHEN MATCHED AND BusinessUnitTypeKey = 1
                                                              THEN UPDATE SET [Target].BusinessUnitKey = @BusinessUnitKey , [Target].RegisteredName = @RegisteredName , [Target].ShortName = @ShortName , [Target].PracticeNumber = @PracticeNumber , [Target].HospitalTypeKey = @HospitalTypeKey
                                                            WHEN MATCHED AND BusinessUnitTypeKey != 1
                                                              THEN DELETE ;";
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("BusinessUnitKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters["BusinessUnitKey"].Direction = ParameterDirection.Output;
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("BusinessUnitName", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("BusinessUnitTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("LocationKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("BusinessUnitReportingGroupKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("BusinessUnitDefaultEntity", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("CreatedBy", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("ModifiedBy", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("IsActive", TypeCode.Boolean, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("RegisteredName", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters["RegisteredName"].ConvertEmptyStringToNull = true;
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("ShortName", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("PracticeNumber", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.InsertParameters.Add("HospitalTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnit_Form.SelectParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_Form.SelectParameters.Add("BusinessUnitKey", TypeCode.Int32, Request.QueryString["BusinessUnitKey"]);
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Clear();
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("BusinessUnitName", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("BusinessUnitTypeKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("LocationKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("BusinessUnitReportingGroupKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("BusinessUnitDefaultEntity", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("ModifiedBy", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("IsActive", TypeCode.Boolean, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("BusinessUnitKey", TypeCode.Int32, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("RegisteredName", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("ShortName", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("PracticeNumber", TypeCode.String, "");
      SqlDataSource_FSM_BusinessUnit_Form.UpdateParameters.Add("HospitalTypeKey", TypeCode.Int32, "");
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("41").Replace(" Form", "")).ToString() + " : Business Unit", CultureInfo.CurrentCulture);
      Label_BusinessUnitHeading.Text = Convert.ToString("Business Unit", CultureInfo.CurrentCulture);
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_BusinessUnit"];
      string SearchField2 = Request.QueryString["Search_BusinessUnitType"];
      string SearchField3 = Request.QueryString["Search_BusinessUnitDefaultEntity"];
      string SearchField4 = Request.QueryString["Search_BusinessUnitReportingGroup"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_BusinessUnit=" + Request.QueryString["Search_BusinessUnit"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_BusinessUnitType=" + Request.QueryString["Search_BusinessUnitType"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_BusinessUnitDefaultEntity=" + Request.QueryString["Search_BusinessUnitDefaultEntity"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_BusinessUnitReportingGroup=" + Request.QueryString["Search_BusinessUnitReportingGroup"] + "&";
      }

      string FinalURL = "Form_FSM_BusinessUnit_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Captured Business Units", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    private class FromDataBase_SecurityRole
    {
      public DataRow[] SecurityAdmin { get; set; }
      public DataRow[] SecurityFormAdminUpdate { get; set; }
      public DataRow[] SecurityFormAdminView { get; set; }
    }

    private FromDataBase_SecurityRole GetSecurityRole()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_New = new FromDataBase_SecurityRole();

      string SQLStringSecurityRole = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('41'))";
      using (SqlCommand SqlCommand_SecurityRole = new SqlCommand(SQLStringSecurityRole))
      {
        SqlCommand_SecurityRole.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

        DataTable DataTable_SecurityRole;
        using (DataTable_SecurityRole = new DataTable())
        {
          DataTable_SecurityRole.Locale = CultureInfo.CurrentCulture;
          DataTable_SecurityRole = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityRole).Copy();

          if (DataTable_SecurityRole.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_SecurityRole.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_SecurityRole.Select("SecurityRole_Id = '170'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_SecurityRole.Select("SecurityRole_Id = '171'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }


    //--START-- --TableBusinessUnit--//
    protected void SetBusinessUnitVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["BusinessUnitKey"]))
      {
        FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;

        string Security = "1";
        if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
        {
          Security = "0";
          FormView_FSM_BusinessUnit_Form.ChangeMode(FormViewMode.Insert);
        }

        if (Security == "1" && (SecurityFormAdminView.Length > 0))
        {
          Security = "0";
          FormView_FSM_BusinessUnit_Form.ChangeMode(FormViewMode.ReadOnly);
        }

        if (Security == "1")
        {
          Security = "0";
          FormView_FSM_BusinessUnit_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }
      else
      {
        FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;

        string Security = "1";
        if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
        {
          Security = "0";
          FormView_FSM_BusinessUnit_Form.ChangeMode(FormViewMode.Edit);
        }

        if (Security == "1" && (SecurityFormAdminView.Length > 0))
        {
          Security = "0";
          FormView_FSM_BusinessUnit_Form.ChangeMode(FormViewMode.ReadOnly);
        }

        if (Security == "1")
        {
          Security = "0";
          FormView_FSM_BusinessUnit_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }
    }

    protected void TableBusinessUnitVisible()
    {
      if (FormView_FSM_BusinessUnit_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitName")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitTypeKey")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertLocationKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitReportingGroupKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitDefaultEntity")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitDefaultEntity")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertRegisteredName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertRegisteredName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertShortName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertShortName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertPracticeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertPracticeNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertHospitalTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
      }

      if (FormView_FSM_BusinessUnit_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditBusinessUnitName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditBusinessUnitName")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitTypeKey")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditLocationKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitReportingGroupKey")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditBusinessUnitDefaultEntity")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditBusinessUnitDefaultEntity")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditRegisteredName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditRegisteredName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditShortName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditShortName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditPracticeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditPracticeNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditHospitalTypeKey")).Attributes.Add("OnChange", "Validation_Form();");
      }
    }


    //--START-- --Insert--//
    protected void FormView_FSM_BusinessUnit_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ToolkitScriptManager_FSM_BusinessUnit.SetFocus(UpdatePanel_FSM_BusinessUnit);
          ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_FSM_BusinessUnit_Form.InsertParameters["CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_FSM_BusinessUnit_Form.InsertParameters["CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_FSM_BusinessUnit_Form.InsertParameters["ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_FSM_BusinessUnit_Form.InsertParameters["ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_FSM_BusinessUnit_Form.InsertParameters["IsActive"].DefaultValue = "true";

          SqlDataSource_FSM_BusinessUnit_Form.InsertParameters["BusinessUnitTypeKey"].DefaultValue = ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitTypeKey")).SelectedValue;
          SqlDataSource_FSM_BusinessUnit_Form.InsertParameters["BusinessUnitReportingGroupKey"].DefaultValue = ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitReportingGroupKey")).SelectedValue;
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitName")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertLocationKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitReportingGroupKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitDefaultEntity")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitTypeKey")).SelectedValue == "1")
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertRegisteredName")).Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertShortName")).Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertPracticeNumber")).Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertHospitalTypeKey")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = InsertFieldValidation(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string InsertFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      string BusinessUnitName = "";
      string SQLStringName = "SELECT BusinessUnitName FROM BusinessUnit.BusinessUnit WHERE BusinessUnitName = @BusinessUnitName";
      using (SqlCommand SqlCommand_Name = new SqlCommand(SQLStringName))
      {
        SqlCommand_Name.Parameters.AddWithValue("@BusinessUnitName", ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitName")).Text.ToString());
        DataTable DataTable_Name;
        using (DataTable_Name = new DataTable())
        {
          DataTable_Name.Locale = CultureInfo.CurrentCulture;
          DataTable_Name = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Name, "PatientDetailFacilityStructure").Copy();
          if (DataTable_Name.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Name.Rows)
            {
              BusinessUnitName = DataRow_Row["BusinessUnitName"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(BusinessUnitName))
      {
        InvalidFormMessage = InvalidFormMessage + "A Business Unit with the Name '" + BusinessUnitName + "' already exists<br />";
      }

      BusinessUnitName = "";


      string ValidEntity = "";
      string SQLStringEntity = "SELECT BusinessUnit.ValidateEntityFn ( @EntityCode ) AS ValidEntity";
      using (SqlCommand SqlCommand_Entity = new SqlCommand(SQLStringEntity))
      {
        SqlCommand_Entity.Parameters.AddWithValue("@EntityCode", ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitDefaultEntity")).Text.ToString());
        DataTable DataTable_Entity;
        using (DataTable_Entity = new DataTable())
        {
          DataTable_Entity.Locale = CultureInfo.CurrentCulture;
          DataTable_Entity = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Entity, "PatientDetailFacilityStructure").Copy();
          if (DataTable_Entity.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Entity.Rows)
            {
              ValidEntity = DataRow_Row["ValidEntity"].ToString();
            }
          }
        }
      }

      if (ValidEntity == "False")
      {
        InvalidFormMessage = InvalidFormMessage + "Entity Code is not valid<br />";
      }

      ValidEntity = "";


      return InvalidFormMessage;
    }

    protected void SqlDataSource_FSM_BusinessUnit_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string BusinessUnitKey = e.Command.Parameters["@BusinessUnitKey"].Value.ToString();

        if (!string.IsNullOrEmpty(BusinessUnitKey))
        {
          if (((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitTypeKey")).SelectedValue == "1")
          {
            if (((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_InsertMappingBusinessUnit")).Rows.Count > 0)
            {
              foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_InsertMappingBusinessUnit")).Rows)
              {
                HiddenField HiddenField_InsertSourceSystemKey = (HiddenField)GridViewRow_Row.FindControl("HiddenField_InsertSourceSystemKey");
                TextBox TextBox_InsertSourceSystemValue = (TextBox)GridViewRow_Row.FindControl("TextBox_InsertSourceSystemValue");

                if (!string.IsNullOrEmpty(TextBox_InsertSourceSystemValue.Text))
                {
                  string SQLStringInsertSourceSystemMapping = "INSERT INTO Mapping.BusinessUnit ( BusinessUnitKey , SourceSystemKey , SourceSystemValue ) VALUES ( @BusinessUnitKey , @SourceSystemKey , @SourceSystemValue )";
                  using (SqlCommand SqlCommand_InsertSourceSystemMapping = new SqlCommand(SQLStringInsertSourceSystemMapping))
                  {
                    SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@BusinessUnitKey", BusinessUnitKey);
                    SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@SourceSystemKey", HiddenField_InsertSourceSystemKey.Value);
                    SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@SourceSystemValue", TextBox_InsertSourceSystemValue.Text);

                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_Other(SqlCommand_InsertSourceSystemMapping, "PatientDetailFacilityStructure");
                  }
                }
              }
            }
          }
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Captured", "InfoQuest_Captured.aspx?CapturedPage=Form_FSM_BusinessUnit&CapturedNumber=" + BusinessUnitKey + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    protected void FormView_FSM_BusinessUnit_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDModifiedDate"] = e.OldValues["ModifiedDate"];
        object OLDModifiedDate = Session["OLDModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareFSM_BusinessUnit = (DataView)SqlDataSource_FSM_BusinessUnit_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareFSM_BusinessUnit = DataView_CompareFSM_BusinessUnit[0];
        Session["DBModifiedDate"] = Convert.ToString(DataRowView_CompareFSM_BusinessUnit["ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBModifiedBy"] = Convert.ToString(DataRowView_CompareFSM_BusinessUnit["ModifiedBy"], CultureInfo.CurrentCulture);
        object DBModifiedDate = Session["DBModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_FSM_BusinessUnit.SetFocus(UpdatePanel_FSM_BusinessUnit);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ToolkitScriptManager_FSM_BusinessUnit.SetFocus(UpdatePanel_FSM_BusinessUnit);
            ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            e.NewValues["BusinessUnitTypeKey"] = ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitTypeKey")).SelectedValue;
            e.NewValues["BusinessUnitReportingGroupKey"] = ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitReportingGroupKey")).SelectedValue;
          }
        }

        Session["OLDModifiedDate"] = "";
        Session["DBModifiedDate"] = "";
        Session["DBModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditBusinessUnitName")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitTypeKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditLocationKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitReportingGroupKey")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditBusinessUnitDefaultEntity")).Text))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitTypeKey")).SelectedValue == "1")
        {
          if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditRegisteredName")).Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditShortName")).Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditPracticeNumber")).Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditHospitalTypeKey")).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "All red fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = EditFieldValidation(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      string BusinessUnitKey = "";
      string BusinessUnitName = "";
      string SQLStringBusinessUnit = "SELECT BusinessUnitKey , BusinessUnitName FROM BusinessUnit.BusinessUnit WHERE BusinessUnitName = @BusinessUnitName";
      using (SqlCommand SqlCommand_BusinessUnit = new SqlCommand(SQLStringBusinessUnit))
      {
        SqlCommand_BusinessUnit.Parameters.AddWithValue("@BusinessUnitName", ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditBusinessUnitName")).Text.ToString());
        DataTable DataTable_BusinessUnit;
        using (DataTable_BusinessUnit = new DataTable())
        {
          DataTable_BusinessUnit.Locale = CultureInfo.CurrentCulture;
          DataTable_BusinessUnit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_BusinessUnit, "PatientDetailFacilityStructure").Copy();
          if (DataTable_BusinessUnit.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_BusinessUnit.Rows)
            {
              BusinessUnitKey = DataRow_Row["BusinessUnitKey"].ToString();
              BusinessUnitName = DataRow_Row["BusinessUnitName"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(BusinessUnitName))
      {
        if (BusinessUnitKey != Request.QueryString["BusinessUnitKey"])
        {
          InvalidFormMessage = InvalidFormMessage + "A Business Unit with the Name '" + BusinessUnitName + "' already exists<br />";
        }
      }

      BusinessUnitKey = "";
      BusinessUnitName = "";


      string ValidEntity = "";
      string SQLStringEntity = "SELECT BusinessUnit.ValidateEntityFn ( @EntityCode ) AS ValidEntity";
      using (SqlCommand SqlCommand_Entity = new SqlCommand(SQLStringEntity))
      {
        SqlCommand_Entity.Parameters.AddWithValue("@EntityCode", ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitDefaultEntity")).Text.ToString());
        DataTable DataTable_Entity;
        using (DataTable_Entity = new DataTable())
        {
          DataTable_Entity.Locale = CultureInfo.CurrentCulture;
          DataTable_Entity = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Entity, "PatientDetailFacilityStructure").Copy();
          if (DataTable_Entity.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Entity.Rows)
            {
              ValidEntity = DataRow_Row["ValidEntity"].ToString();
            }
          }
        }
      }

      if (ValidEntity == "False")
      {
        InvalidFormMessage = InvalidFormMessage + "Entity Code is not valid<br />";
      }

      ValidEntity = "";


      return InvalidFormMessage;
    }

    protected void SqlDataSource_FSM_BusinessUnit_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows >= 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            if (!string.IsNullOrEmpty(Request.QueryString["BusinessUnitKey"]))
            {
              if (((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_EditMappingBusinessUnit")).Rows.Count > 0)
              {
                string SQLStringDeleteSourceSystemMapping = "DELETE FROM Mapping.BusinessUnit WHERE BusinessUnitKey = @BusinessUnitKey";
                using (SqlCommand SqlCommand_DeleteSourceSystemMapping = new SqlCommand(SQLStringDeleteSourceSystemMapping))
                {
                  SqlCommand_DeleteSourceSystemMapping.Parameters.AddWithValue("@BusinessUnitKey", Request.QueryString["BusinessUnitKey"]);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_Other(SqlCommand_DeleteSourceSystemMapping, "PatientDetailFacilityStructure");
                }

                foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_EditMappingBusinessUnit")).Rows)
                {
                  HiddenField HiddenField_EditSourceSystemKey = (HiddenField)GridViewRow_Row.FindControl("HiddenField_EditSourceSystemKey");
                  TextBox TextBox_EditSourceSystemValue = (TextBox)GridViewRow_Row.FindControl("TextBox_EditSourceSystemValue");

                  if (!string.IsNullOrEmpty(TextBox_EditSourceSystemValue.Text))
                  {
                    string SQLStringInsertSourceSystemMapping = "INSERT INTO Mapping.BusinessUnit ( BusinessUnitKey , SourceSystemKey , SourceSystemValue ) VALUES ( @BusinessUnitKey , @SourceSystemKey , @SourceSystemValue )";
                    using (SqlCommand SqlCommand_InsertSourceSystemMapping = new SqlCommand(SQLStringInsertSourceSystemMapping))
                    {
                      SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@BusinessUnitKey", Request.QueryString["BusinessUnitKey"]);
                      SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@SourceSystemKey", HiddenField_EditSourceSystemKey.Value);
                      SqlCommand_InsertSourceSystemMapping.Parameters.AddWithValue("@SourceSystemValue", TextBox_EditSourceSystemValue.Text);

                      InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute_Other(SqlCommand_InsertSourceSystemMapping, "PatientDetailFacilityStructure");
                    }
                  }
                }
              }
            }

            RedirectToList();
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_FSM_BusinessUnit_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["BusinessUnitKey"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - New Business Unit", "Form_FSM_BusinessUnit.aspx"), false);
          }
        }
      }
    }

    protected void FormView_FSM_BusinessUnit_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_FSM_BusinessUnit_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_FSM_BusinessUnit_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["BusinessUnitKey"]))
      {
        DataView DataView_FSM_BusinessUnit = (DataView)SqlDataSource_FSM_BusinessUnit_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_FSM_BusinessUnit = DataView_FSM_BusinessUnit[0];
        ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitReportingGroupKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnit["BusinessUnitReportingGroupKey"], CultureInfo.CurrentCulture);
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["BusinessUnitKey"]))
      {
        string BusinessUnitTypeName = "";
        string HospitalTypeName = "";
        string LocationName = "";
        string BusinessUnitReportingGroupName = "";
        string SQLStringFSMBusinessUnit = "SELECT BusinessUnitType.BusinessUnitTypeName , HospitalType.HospitalTypeName , Location.LocationName , BusinessUnitReportingGroup.BusinessUnitReportingGroupName FROM BusinessUnit.BusinessUnit LEFT JOIN Hospital.Hospital ON BusinessUnit.BusinessUnitKey = Hospital.BusinessUnitKey LEFT JOIN BusinessUnit.BusinessUnitType ON BusinessUnit.BusinessUnitTypeKey = BusinessUnitType.BusinessUnitTypeKey LEFT JOIN Hospital.HospitalType ON Hospital.HospitalTypeKey = HospitalType.HospitalTypeKey LEFT JOIN BusinessUnit.Location ON BusinessUnit.LocationKey = Location.LocationKey LEFT JOIN BusinessUnit.BusinessUnitReportingGroup ON BusinessUnit.BusinessUnitReportingGroupKey = BusinessUnitReportingGroup.BusinessUnitReportingGroupKey WHERE BusinessUnit.BusinessUnitKey = @BusinessUnitKey";
        using (SqlCommand SqlCommand_FSMBusinessUnit = new SqlCommand(SQLStringFSMBusinessUnit))
        {
          SqlCommand_FSMBusinessUnit.Parameters.AddWithValue("@BusinessUnitKey", Request.QueryString["BusinessUnitKey"]);
          DataTable DataTable_FSMBusinessUnit;
          using (DataTable_FSMBusinessUnit = new DataTable())
          {
            DataTable_FSMBusinessUnit.Locale = CultureInfo.CurrentCulture;
            DataTable_FSMBusinessUnit = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_FSMBusinessUnit, "PatientDetailFacilityStructure").Copy();
            if (DataTable_FSMBusinessUnit.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FSMBusinessUnit.Rows)
              {
                BusinessUnitTypeName = DataRow_Row["BusinessUnitTypeName"].ToString();
                HospitalTypeName = DataRow_Row["HospitalTypeName"].ToString();
                LocationName = DataRow_Row["LocationName"].ToString();
                BusinessUnitReportingGroupName = DataRow_Row["BusinessUnitReportingGroupName"].ToString();
              }
            }
          }
        }

        ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_ItemBusinessUnitTypeKey")).Text = BusinessUnitTypeName;
        ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_ItemHospitalTypeKey")).Text = HospitalTypeName;
        ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_ItemLocationKey")).Text = LocationName;
        ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_ItemBusinessUnitReportingGroupKey")).Text = BusinessUnitReportingGroupName;

        BusinessUnitTypeName = "";
        HospitalTypeName = "";
        LocationName = "";
        BusinessUnitReportingGroupName = "";
      }
    }


    //--START-- --Insert Controls--//
    protected void DropDownList_InsertBusinessUnitTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitReportingGroupKey")).Items.Clear();
      SqlDataSource_FSM_BusinessUnit_InsertBusinessUnitReportingGroupKey.SelectParameters["BusinessUnitTypeKey"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitReportingGroupKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Reporting Group", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_InsertBusinessUnitReportingGroupKey")).DataBind();
    }

    protected void TextBox_InsertBusinessUnitDefaultEntity_TextChanged(object sender, EventArgs e)
    {
      string Description = "No Description";
      string SQLStringEntityLookup = "SELECT Description FROM BusinessUnit.EntityLookup WHERE Entity = @Entity";
      using (SqlCommand SqlCommand_EntityLookup = new SqlCommand(SQLStringEntityLookup))
      {
        SqlCommand_EntityLookup.Parameters.AddWithValue("@Entity", ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_InsertBusinessUnitDefaultEntity")).Text);
        DataTable DataTable_EntityLookup;
        using (DataTable_EntityLookup = new DataTable())
        {
          DataTable_EntityLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_EntityLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_EntityLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_EntityLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EntityLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_InsertBusinessUnitDefaultEntityLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    protected void GridView_InsertMappingBusinessUnit_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_InsertMappingBusinessUnit_DataBound(object sender, EventArgs e)
    {
      GridView GridView_InsertMappingBusinessUnit = (GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_InsertMappingBusinessUnit");

      ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_InsertTotalRecords")).Text = GridView_InsertMappingBusinessUnit.Rows.Count.ToString(CultureInfo.CurrentCulture);
      ((HiddenField)FormView_FSM_BusinessUnit_Form.FindControl("HiddenField_InsertTotalRecords")).Value = GridView_InsertMappingBusinessUnit.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void GridView_InsertMappingBusinessUnit_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_InsertGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_InsertCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FSM_BusinessUnit.aspx", false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void DropDownList_EditBusinessUnitTypeKey_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitReportingGroupKey")).Items.Clear();
      SqlDataSource_FSM_BusinessUnit_EditBusinessUnitReportingGroupKey.SelectParameters["BusinessUnitTypeKey"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitReportingGroupKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Reporting Group", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitReportingGroupKey")).DataBind();
    }

    protected void DropDownList_EditBusinessUnitTypeKey_DataBound(object sender, EventArgs e)
    {
      DataView DataView_FSM_BusinessUnit = (DataView)SqlDataSource_FSM_BusinessUnit_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_FSM_BusinessUnit = DataView_FSM_BusinessUnit[0];
      ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitTypeKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnit["BusinessUnitTypeKey"], CultureInfo.CurrentCulture);
      SqlDataSource_FSM_BusinessUnit_EditBusinessUnitReportingGroupKey.SelectParameters["BusinessUnitTypeKey"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_FSM_BusinessUnit_Form.FindControl("DropDownList_EditBusinessUnitReportingGroupKey")).SelectedValue = Convert.ToString(DataRowView_FSM_BusinessUnit["BusinessUnitReportingGroupKey"], CultureInfo.CurrentCulture);
    }

    protected void TextBox_EditBusinessUnitDefaultEntity_TextChanged(object sender, EventArgs e)
    {
      string Description = "No Description";
      string SQLStringEntityLookup = "SELECT Description FROM BusinessUnit.EntityLookup WHERE Entity = @Entity";
      using (SqlCommand SqlCommand_EntityLookup = new SqlCommand(SQLStringEntityLookup))
      {
        SqlCommand_EntityLookup.Parameters.AddWithValue("@Entity", ((TextBox)FormView_FSM_BusinessUnit_Form.FindControl("TextBox_EditBusinessUnitDefaultEntity")).Text);
        DataTable DataTable_EntityLookup;
        using (DataTable_EntityLookup = new DataTable())
        {
          DataTable_EntityLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_EntityLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_EntityLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_EntityLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EntityLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_EditBusinessUnitDefaultEntityLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    protected void TextBox_EditBusinessUnitDefaultEntity_DataBinding(object sender, EventArgs e)
    {
      string Description = "No Description";
      string SQLStringEntityLookup = "SELECT Description FROM BusinessUnit.EntityLookup WHERE Entity = @Entity";
      using (SqlCommand SqlCommand_EntityLookup = new SqlCommand(SQLStringEntityLookup))
      {
        SqlCommand_EntityLookup.Parameters.AddWithValue("@Entity", ((TextBox)sender).Text);
        DataTable DataTable_EntityLookup;
        using (DataTable_EntityLookup = new DataTable())
        {
          DataTable_EntityLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_EntityLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_EntityLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_EntityLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EntityLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_EditBusinessUnitDefaultEntityLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    protected void GridView_EditMappingBusinessUnit_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_EditMappingBusinessUnit_DataBound(object sender, EventArgs e)
    {
      GridView GridView_EditMappingBusinessUnit = (GridView)FormView_FSM_BusinessUnit_Form.FindControl("GridView_EditMappingBusinessUnit");

      ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_EditTotalRecords")).Text = GridView_EditMappingBusinessUnit.Rows.Count.ToString(CultureInfo.CurrentCulture);
      ((HiddenField)FormView_FSM_BusinessUnit_Form.FindControl("HiddenField_EditTotalRecords")).Value = GridView_EditMappingBusinessUnit.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void GridView_EditMappingBusinessUnit_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_EditMappingBusinessUnit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          HiddenField HiddenField_EditSourceSystemKey = (HiddenField)e.Row.FindControl("HiddenField_EditSourceSystemKey");

          string SourceSystemKey = "";
          string SourceSystemValue = "";
          string SQLStringSourceSystem = "SELECT SourceSystemKey , SourceSystemValue FROM Mapping.BusinessUnit WHERE BusinessUnitKey = @BusinessUnitKey AND SourceSystemKey = @SourceSystemKey";
          using (SqlCommand SqlCommand_SourceSystem = new SqlCommand(SQLStringSourceSystem))
          {
            SqlCommand_SourceSystem.Parameters.AddWithValue("@BusinessUnitKey", Request.QueryString["BusinessUnitKey"]);
            SqlCommand_SourceSystem.Parameters.AddWithValue("@SourceSystemKey", HiddenField_EditSourceSystemKey.Value);
            DataTable DataTable_SourceSystem;
            using (DataTable_SourceSystem = new DataTable())
            {
              DataTable_SourceSystem.Locale = CultureInfo.CurrentCulture;
              DataTable_SourceSystem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_SourceSystem, "PatientDetailFacilityStructure").Copy();
              if (DataTable_SourceSystem.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_SourceSystem.Rows)
                {
                  SourceSystemKey = DataRow_Row["SourceSystemKey"].ToString();
                  SourceSystemValue = DataRow_Row["SourceSystemValue"].ToString();
                }
              }
            }
          }

          TextBox TextBox_EditSourceSystemValue = (TextBox)e.Row.FindControl("TextBox_EditSourceSystemValue");

          if (!string.IsNullOrEmpty(SourceSystemKey))
          {
            TextBox_EditSourceSystemValue.Text = SourceSystemValue;
          }
          else
          {
            TextBox_EditSourceSystemValue.Text = "";
          }

          SourceSystemKey = "";
          SourceSystemValue = "";
        }
      }
    }

    protected void Button_EditGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FSM_BusinessUnit.aspx", false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    protected void Label_ItemBusinessUnitDefaultEntity_DataBinding(object sender, EventArgs e)
    {
      string LabelItemBusinessUnitDefaultEntityText = ((Label)sender).Text;

      string Description = "No Description";
      string SQLStringEntityLookup = "SELECT Description FROM BusinessUnit.EntityLookup WHERE Entity = @Entity";
      using (SqlCommand SqlCommand_EntityLookup = new SqlCommand(SQLStringEntityLookup))
      {
        SqlCommand_EntityLookup.Parameters.AddWithValue("@Entity", LabelItemBusinessUnitDefaultEntityText);
        DataTable DataTable_EntityLookup;
        using (DataTable_EntityLookup = new DataTable())
        {
          DataTable_EntityLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_EntityLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_EntityLookup, "PatientDetailFacilityStructure").Copy();
          if (DataTable_EntityLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_EntityLookup.Rows)
            {
              Description = DataRow_Row["Description"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(LabelItemBusinessUnitDefaultEntityText))
      {
        Description = " - " + Description;
      }

      ((Label)FormView_FSM_BusinessUnit_Form.FindControl("Label_ItemBusinessUnitDefaultEntityLookup")).Text = Convert.ToString(Description, CultureInfo.CurrentCulture);

      Description = "";
    }

    protected void GridView_ItemMappingBusinessUnit_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemMappingBusinessUnit_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_ItemGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FSM_BusinessUnit.aspx", false);
    }
    //---END--- --Item Controls--//
    //---END--- --TableBusinessUnit--//
  }
}