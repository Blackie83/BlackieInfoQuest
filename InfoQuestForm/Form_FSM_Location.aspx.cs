using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_FSM_Location : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_FSM_Location, this.GetType(), "UpdateProgress_Start", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          PageTitle();

          SetLocationVisibility();

          TableLocationVisible();
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
        ((Label)PageUpdateProgress_FSM_Location.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Facility Structure Maintenance", "15");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_FSM_Location_InsertCountry.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_InsertCountry.SelectCommand = "SELECT CountryKey , CountryName FROM Geographic.Country ORDER BY CountryName";

      SqlDataSource_FSM_Location_InsertProvinceKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_InsertProvinceKey.SelectCommand = "SELECT ProvinceKey , ProvinceName FROM Geographic.Province WHERE CountryKey = @CountryKey ORDER BY ProvinceName";
      SqlDataSource_FSM_Location_InsertProvinceKey.SelectParameters.Clear();
      SqlDataSource_FSM_Location_InsertProvinceKey.SelectParameters.Add("CountryKey", TypeCode.String, "");

      SqlDataSource_FSM_Location_EditCountry.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_EditCountry.SelectCommand = "SELECT CountryKey , CountryName FROM Geographic.Country ORDER BY CountryName";

      SqlDataSource_FSM_Location_EditProvinceKey.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_EditProvinceKey.SelectCommand = "SELECT ProvinceKey , ProvinceName FROM Geographic.Province WHERE CountryKey = @CountryKey ORDER BY ProvinceName";
      SqlDataSource_FSM_Location_EditProvinceKey.SelectParameters.Clear();
      SqlDataSource_FSM_Location_EditProvinceKey.SelectParameters.Add("CountryKey", TypeCode.String, "");

      SqlDataSource_FSM_Location_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailFacilityStructure");
      SqlDataSource_FSM_Location_Form.InsertCommand = "INSERT INTO BusinessUnit.Location ( LocationName , LocationAddress , Longitude , Latitude , ProvinceKey , CreatedDate , CreatedBy , ModifiedDate , ModifiedBy , IsActive ) VALUES ( @LocationName , @LocationAddress , @Longitude , @Latitude , @ProvinceKey , @CreatedDate , @CreatedBy , @ModifiedDate , @ModifiedBy , @IsActive ); SELECT @LocationKey = SCOPE_IDENTITY()";
      SqlDataSource_FSM_Location_Form.SelectCommand = "SELECT * FROM BusinessUnit.Location WHERE (LocationKey = @LocationKey)";
      SqlDataSource_FSM_Location_Form.UpdateCommand = "UPDATE BusinessUnit.Location SET LocationName = @LocationName , LocationAddress = @LocationAddress , Longitude = @Longitude , Latitude = @Latitude , ProvinceKey = @ProvinceKey , CreatedDate = @CreatedDate , CreatedBy = @CreatedBy , ModifiedDate = @ModifiedDate , ModifiedBy = @ModifiedBy , IsActive = @IsActive WHERE LocationKey = @LocationKey";
      SqlDataSource_FSM_Location_Form.InsertParameters.Clear();
      SqlDataSource_FSM_Location_Form.InsertParameters.Add("LocationKey", TypeCode.Int32, "");
      SqlDataSource_FSM_Location_Form.InsertParameters["LocationKey"].Direction = ParameterDirection.Output;
      SqlDataSource_FSM_Location_Form.InsertParameters.Add("LocationName", TypeCode.String, "");
			SqlDataSource_FSM_Location_Form.InsertParameters.Add("LocationAddress", TypeCode.String, "");
			SqlDataSource_FSM_Location_Form.InsertParameters.Add("Longitude", TypeCode.Decimal, "");
			SqlDataSource_FSM_Location_Form.InsertParameters.Add("Latitude", TypeCode.Decimal, "");
			SqlDataSource_FSM_Location_Form.InsertParameters.Add("ProvinceKey", TypeCode.Int32, "");
			SqlDataSource_FSM_Location_Form.InsertParameters.Add("CreatedDate", TypeCode.DateTime, "");
			SqlDataSource_FSM_Location_Form.InsertParameters.Add("CreatedBy", TypeCode.String, "");
			SqlDataSource_FSM_Location_Form.InsertParameters.Add("ModifiedDate", TypeCode.DateTime, "");
			SqlDataSource_FSM_Location_Form.InsertParameters.Add("ModifiedBy", TypeCode.String, "");
			SqlDataSource_FSM_Location_Form.InsertParameters.Add("IsActive", TypeCode.Boolean, "");
      SqlDataSource_FSM_Location_Form.SelectParameters.Clear();
      SqlDataSource_FSM_Location_Form.SelectParameters.Add("LocationKey", TypeCode.Int32, Request.QueryString["LocationKey"]);
      SqlDataSource_FSM_Location_Form.UpdateParameters.Clear();
      SqlDataSource_FSM_Location_Form.UpdateParameters.Add("LocationName", TypeCode.String, "");
      SqlDataSource_FSM_Location_Form.UpdateParameters.Add("LocationAddress", TypeCode.String, "");
      SqlDataSource_FSM_Location_Form.UpdateParameters.Add("Longitude", TypeCode.Decimal, "");
      SqlDataSource_FSM_Location_Form.UpdateParameters.Add("Latitude", TypeCode.Decimal, "");
      SqlDataSource_FSM_Location_Form.UpdateParameters.Add("ProvinceKey", TypeCode.Int32, "");
      SqlDataSource_FSM_Location_Form.UpdateParameters.Add("ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_FSM_Location_Form.UpdateParameters.Add("ModifiedBy", TypeCode.String, "");
      SqlDataSource_FSM_Location_Form.UpdateParameters.Add("IsActive", TypeCode.Boolean, "");
      SqlDataSource_FSM_Location_Form.UpdateParameters.Add("LocationKey", TypeCode.Int32, "");
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("41").Replace(" Form", "")).ToString() + " : Location", CultureInfo.CurrentCulture);
      Label_LocationHeading.Text = Convert.ToString("Location", CultureInfo.CurrentCulture);
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_Location"];
      string SearchField2 = Request.QueryString["Search_Country"];
      string SearchField3 = Request.QueryString["Search_Province"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Location=" + Request.QueryString["Search_Location"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Country=" + Request.QueryString["Search_Country"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_Province=" + Request.QueryString["Search_Province"] + "&";
      }

      string FinalURL = "Form_FSM_Location_List.aspx?" + SearchField1 + SearchField2 + SearchField3;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - Captured Locations", FinalURL);

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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('41'))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '170'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '171'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }


    //--START-- --TableLocation--//
    protected void SetLocationVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["LocationKey"]))
      {
        FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
        DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
        DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
        DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;

        string Security = "1";
        if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
        {
          Security = "0";
          FormView_FSM_Location_Form.ChangeMode(FormViewMode.Insert);
        }

        if (Security == "1" && (SecurityFormAdminView.Length > 0))
        {
          Security = "0";
          FormView_FSM_Location_Form.ChangeMode(FormViewMode.ReadOnly);
        }

        if (Security == "1")
        {
          Security = "0";
          FormView_FSM_Location_Form.ChangeMode(FormViewMode.ReadOnly);
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
          FormView_FSM_Location_Form.ChangeMode(FormViewMode.Edit);
        }

        if (Security == "1" && (SecurityFormAdminView.Length > 0))
        {
          Security = "0";
          FormView_FSM_Location_Form.ChangeMode(FormViewMode.ReadOnly);
        }

        if (Security == "1")
        {
          Security = "0";
          FormView_FSM_Location_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }
    }

    protected void TableLocationVisible()
    {
      if (FormView_FSM_Location_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLocationName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLocationName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLocationAddress")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLocationAddress")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_InsertCountry")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_InsertProvinceKey")).Attributes.Add("OnChange", "Validation_Form();");
      }

      if (FormView_FSM_Location_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLocationName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLocationName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLocationAddress")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLocationAddress")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditCountry")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditProvinceKey")).Attributes.Add("OnChange", "Validation_Form();");
      }
    }


    //--START-- --Insert--//
    protected void FormView_FSM_Location_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ToolkitScriptManager_FSM_Location.SetFocus(UpdatePanel_FSM_Location);
          ((Label)FormView_FSM_Location_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_FSM_Location_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_FSM_Location_Form.InsertParameters["CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_FSM_Location_Form.InsertParameters["CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_FSM_Location_Form.InsertParameters["ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_FSM_Location_Form.InsertParameters["ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_FSM_Location_Form.InsertParameters["IsActive"].DefaultValue = "true";

          SqlDataSource_FSM_Location_Form.InsertParameters["ProvinceKey"].DefaultValue = ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_InsertProvinceKey")).SelectedValue;
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLocationName")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLocationAddress")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_InsertCountry")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_InsertProvinceKey")).SelectedValue))
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
        InvalidFormMessage = InsertFieldValidation(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string InsertFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      string LocationName = "";
      string SQLStringName = "SELECT LocationName FROM BusinessUnit.Location WHERE LocationName = @LocationName";
      using (SqlCommand SqlCommand_Name = new SqlCommand(SQLStringName))
      {
        SqlCommand_Name.Parameters.AddWithValue("@LocationName", ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLocationName")).Text.ToString());
        DataTable DataTable_Name;
        using (DataTable_Name = new DataTable())
        {
          DataTable_Name.Locale = CultureInfo.CurrentCulture;
          DataTable_Name = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Name, "PatientDetailFacilityStructure").Copy();
          if (DataTable_Name.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Name.Rows)
            {
              LocationName = DataRow_Row["LocationName"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(LocationName))
      {
        InvalidFormMessage = InvalidFormMessage + "A Location with the Name '" + LocationName + "' already exists<br />";
      }

      LocationName = "";


      if (!string.IsNullOrEmpty(((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLongitude")).Text))
      {
        string ValidLongitude = InfoQuestWCF.InfoQuest_Regex.Regex_Longitude((((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLongitude")).Text).ToString());

        if (ValidLongitude == "No")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid Longitude, Longitude must be in the format DDD.DDDDD°, Decimal Degrees<br />";
        }
      }

      if (!string.IsNullOrEmpty(((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLatitude")).Text))
      {
        string ValidLatitude = InfoQuestWCF.InfoQuest_Regex.Regex_Latitude((((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLatitude")).Text).ToString());

        if (ValidLatitude == "No")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid Latitude, Latitude must be in the format DDD.DDDDD°, Decimal Degrees<br />";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_FSM_Location_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string LocationKey = e.Command.Parameters["@LocationKey"].Value.ToString();

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Captured", "InfoQuest_Captured.aspx?CapturedPage=Form_FSM_Location&CapturedNumber=" + LocationKey + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    protected void FormView_FSM_Location_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDModifiedDate"] = e.OldValues["ModifiedDate"];
        object OLDModifiedDate = Session["OLDModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareFSM_Location = (DataView)SqlDataSource_FSM_Location_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareFSM_Location = DataView_CompareFSM_Location[0];
        Session["DBModifiedDate"] = Convert.ToString(DataRowView_CompareFSM_Location["ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBModifiedBy"] = Convert.ToString(DataRowView_CompareFSM_Location["ModifiedBy"], CultureInfo.CurrentCulture);
        object DBModifiedDate = Session["DBModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_FSM_Location.SetFocus(UpdatePanel_FSM_Location);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_FSM_Location_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_FSM_Location_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ToolkitScriptManager_FSM_Location.SetFocus(UpdatePanel_FSM_Location);
            ((Label)FormView_FSM_Location_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_FSM_Location_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            DropDownList DropDownList_EditProvinceKey = (DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditProvinceKey");

            e.NewValues["ProvinceKey"] = DropDownList_EditProvinceKey.SelectedValue;
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
        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLocationName")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLocationAddress")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditCountry")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditProvinceKey")).SelectedValue))
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
        InvalidFormMessage = EditFieldValidation(InvalidFormMessage);
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      string LocationKey = "";
      string LocationName = "";
      string SQLStringName = "SELECT LocationKey , LocationName FROM BusinessUnit.Location WHERE LocationName = @LocationName";
      using (SqlCommand SqlCommand_Name = new SqlCommand(SQLStringName))
      {
        SqlCommand_Name.Parameters.AddWithValue("@LocationName", ((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_InsertLocationName")).Text.ToString());
        DataTable DataTable_Name;
        using (DataTable_Name = new DataTable())
        {
          DataTable_Name.Locale = CultureInfo.CurrentCulture;
          DataTable_Name = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_Name, "PatientDetailFacilityStructure").Copy();
          if (DataTable_Name.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Name.Rows)
            {
              LocationKey = DataRow_Row["LocationKey"].ToString();
              LocationName = DataRow_Row["LocationName"].ToString();
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(LocationName))
      {
        if (LocationKey != Request.QueryString["LocationKey"])
        {
          InvalidFormMessage = InvalidFormMessage + "A Location with the Name '" + LocationName + "' already exists<br />";
        }
      }

      LocationKey = "";
      LocationName = "";


      if (!string.IsNullOrEmpty(((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLongitude")).Text))
      {
        string ValidLongitude = InfoQuestWCF.InfoQuest_Regex.Regex_Longitude((((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLongitude")).Text).ToString());

        if (ValidLongitude == "No")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid Longitude, Longitude must be in the format DDD.DDDDD°, Decimal Degrees<br />";
        }
      }

      if (!string.IsNullOrEmpty(((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLatitude")).Text))
      {
        string ValidLatitude = InfoQuestWCF.InfoQuest_Regex.Regex_Latitude((((TextBox)FormView_FSM_Location_Form.FindControl("TextBox_EditLatitude")).Text).ToString());

        if (ValidLatitude == "No")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid Latitude, Latitude must be in the format DDD.DDDDD°, Decimal Degrees<br />";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_FSM_Location_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_FSM_Location_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["LocationKey"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Facility Structure Maintenance - New Location", "Form_FSM_Location.aspx"), false);
          }
        }
      }
    }

    protected void FormView_FSM_Location_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_FSM_Location_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_FSM_Location_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["LocationKey"]))
      {
        DataView DataView_FSM_Location = (DataView)SqlDataSource_FSM_Location_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_FSM_Location = DataView_FSM_Location[0];
        ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditProvinceKey")).SelectedValue = Convert.ToString(DataRowView_FSM_Location["ProvinceKey"], CultureInfo.CurrentCulture);
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["LocationKey"]))
      {
        string CountryName = "";
        string ProvinceName = "";
        string SQLStringFSMLocation = "SELECT Country.CountryName , Province.ProvinceName FROM Geographic.Province LEFT JOIN Geographic.Country ON Province.CountryKey = Country.CountryKey WHERE Province.ProvinceKey IN ( SELECT ProvinceKey FROM BusinessUnit.Location WHERE LocationKey = @LocationKey )";
        using (SqlCommand SqlCommand_FSMLocation = new SqlCommand(SQLStringFSMLocation))
        {
          SqlCommand_FSMLocation.Parameters.AddWithValue("@LocationKey", Request.QueryString["LocationKey"]);
          DataTable DataTable_FSMLocation;
          using (DataTable_FSMLocation = new DataTable())
          {
            DataTable_FSMLocation.Locale = CultureInfo.CurrentCulture;
            DataTable_FSMLocation = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_FSMLocation, "PatientDetailFacilityStructure").Copy();
            if (DataTable_FSMLocation.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FSMLocation.Rows)
              {
                CountryName = DataRow_Row["CountryName"].ToString();
                ProvinceName = DataRow_Row["ProvinceName"].ToString();
              }
            }
          }
        }

        ((Label)FormView_FSM_Location_Form.FindControl("Label_EditCountry")).Text = CountryName;
        ((Label)FormView_FSM_Location_Form.FindControl("Label_EditProvinceKey")).Text = ProvinceName;

        CountryName = "";
        ProvinceName = "";
      }
    }


    //--START-- --Insert Controls--//
    protected void DropDownList_InsertCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_InsertProvinceKey")).Items.Clear();
      SqlDataSource_FSM_Location_InsertProvinceKey.SelectParameters["CountryKey"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_InsertProvinceKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Province", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_InsertProvinceKey")).DataBind();
    }

    protected void Button_InsertGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_InsertCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FSM_Location.aspx", false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void DropDownList_EditCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
      ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditProvinceKey")).Items.Clear();
      SqlDataSource_FSM_Location_EditProvinceKey.SelectParameters["CountryKey"].DefaultValue = ((DropDownList)sender).SelectedValue;
      ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditProvinceKey")).Items.Insert(0, new ListItem(Convert.ToString("Select Province", CultureInfo.CurrentCulture), ""));
      ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditProvinceKey")).DataBind();
    }

    protected void DropDownList_EditCountry_DataBound(object sender, EventArgs e)
    {
      DataView DataView_FSM_Location = (DataView)SqlDataSource_FSM_Location_Form.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_FSM_Location = DataView_FSM_Location[0];

      string CountryKey = "";
      string SQLStringCountryKey = "SELECT CountryKey FROM Geographic.Province WHERE ProvinceKey IN ( SELECT ProvinceKey FROM BusinessUnit.Location WHERE LocationKey = @LocationKey )";
      using (SqlCommand SqlCommand_CountryKey = new SqlCommand(SQLStringCountryKey))
      {
        SqlCommand_CountryKey.Parameters.AddWithValue("@LocationKey", Request.QueryString["LocationKey"]);
        DataTable DataTable_CountryKey;
        using (DataTable_CountryKey = new DataTable())
        {
          DataTable_CountryKey.Locale = CultureInfo.CurrentCulture;
          DataTable_CountryKey = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData_Other(SqlCommand_CountryKey, "PatientDetailFacilityStructure").Copy();
          if (DataTable_CountryKey.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_CountryKey.Rows)
            {
              CountryKey = DataRow_Row["CountryKey"].ToString();
            }
          }
        }
      }

      ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditCountry")).SelectedValue = CountryKey;
      SqlDataSource_FSM_Location_EditProvinceKey.SelectParameters["CountryKey"].DefaultValue = CountryKey;
      ((DropDownList)FormView_FSM_Location_Form.FindControl("DropDownList_EditProvinceKey")).SelectedValue = Convert.ToString(DataRowView_FSM_Location["ProvinceKey"], CultureInfo.CurrentCulture);
    }

    protected void Button_EditGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FSM_Location.aspx", false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    protected void Button_ItemGoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_FSM_Location.aspx", false);
    }
    //---END--- --Item Controls--//
    //---END--- --TableLocation--//
  }
}