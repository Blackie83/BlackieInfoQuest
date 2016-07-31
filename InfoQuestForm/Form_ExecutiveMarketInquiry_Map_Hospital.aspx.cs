using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace InfoQuestForm
{
  public partial class Form_ExecutiveMarketInquiry_Map_Hospital : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        DataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_ExecutiveMarketInquiry_Map_Hospital, this.GetType(), "UpdateProgress_Start", "Validation_Search();ShowHide_Search();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("42").Replace(" Form", "")).ToString() + " : Map", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search Map", CultureInfo.CurrentCulture);
          Label_MapHeading.Text = Convert.ToString("Map", CultureInfo.CurrentCulture);
          Label_GridHeading_Hospital.Text = Convert.ToString("Hospital List", CultureInfo.CurrentCulture);
          Label_GridHeading_Population.Text = Convert.ToString("Population List", CultureInfo.CurrentCulture);
          Label_GridHeading_MedicalScheme.Text = Convert.ToString("Medical Scheme Beneficiaries List", CultureInfo.CurrentCulture);

          CheckBox_MapType_CheckAll.Attributes.Add("OnClick", "Validation_Search();CheckAllMapType();ShowHide_Search();");
          CheckBoxList_MapType.Attributes.Add("OnClick", "Validation_Search();ClearAllMapType();ShowHide_Search();");
          DropDownList_Province.Attributes.Add("OnChange", "Validation_Search();");
          CheckBox_Hospital_Organisation_CheckAll.Attributes.Add("OnClick", "CheckAllHospitalOrganisation();");
          CheckBoxList_Hospital_Organisation.Attributes.Add("OnClick", "ClearAllHospitalOrganisation();");

          if (Request.QueryString["s_MapType"] == null)
          {
            CheckBox_MapType_CheckAll.Checked = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Select All", "<script language='javascript'>CheckAllMapType();ShowHide_Search();</script>", false);
          }

          SetFormQueryString();

          LoadMap();
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('42'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("42");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_ExecutiveMarketInquiry_Map_Hospital.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Executive Market Inquiry", "16");
      }
    }

    private void DataSourceSetup()
    {
      ObjectDataSource_MapType.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalMapType";
      ObjectDataSource_MapType.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";

      ObjectDataSource_Country.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_List_AllCountry";
      ObjectDataSource_Country.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";

      ObjectDataSource_Province.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_List_AllProvince";
      ObjectDataSource_Province.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_Province.SelectParameters.Clear();
      ObjectDataSource_Province.SelectParameters.Add("country", TypeCode.String, (Request.QueryString["s_Country"] ?? DropDownList_Country.SelectedValue ?? "").ToString());
      ObjectDataSource_Province.SelectParameters["country"].ConvertEmptyStringToNull = false;

      DataSourceSetup_Hospital();

      DataSourceSetup_Population();
      DataSourceSetup_MedicalScheme();
    }

    private void DataSourceSetup_Hospital()
    {
      PageLoad PageLoad_Current = GetPageLoad();
      MapTypeValue MapTypeValue_Current = GetMapTypeValue();

      ObjectDataSource_Hospital_SubRegion.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalSubRegion";
      ObjectDataSource_Hospital_SubRegion.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_Hospital_SubRegion.SelectParameters.Clear();
      ObjectDataSource_Hospital_SubRegion.SelectParameters.Add("country", TypeCode.String, (Request.QueryString["s_Country"] ?? DropDownList_Country.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_SubRegion.SelectParameters["country"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_SubRegion.SelectParameters.Add("province", TypeCode.String, (PageLoad_Current.PageLoading == "Yes") ? (((string.IsNullOrEmpty(Request.QueryString["s_MapType"]) ? -1 : ((Request.QueryString["s_MapType"]).IndexOf("Hospital", StringComparison.CurrentCulture) >= 0) ? (Request.QueryString["s_MapType"]).IndexOf("Hospital", StringComparison.CurrentCulture) : -1) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "") : "") : (((MapTypeValue_Current.HospitalAvailable) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "").ToString() : ""));
      ObjectDataSource_Hospital_SubRegion.SelectParameters["province"].ConvertEmptyStringToNull = false;

      ObjectDataSource_Hospital_Town.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalTown";
      ObjectDataSource_Hospital_Town.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_Hospital_Town.SelectParameters.Clear();
      ObjectDataSource_Hospital_Town.SelectParameters.Add("country", TypeCode.String, (Request.QueryString["s_Country"] ?? DropDownList_Country.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_Town.SelectParameters["country"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_Town.SelectParameters.Add("province", TypeCode.String, (PageLoad_Current.PageLoading == "Yes") ? (((string.IsNullOrEmpty(Request.QueryString["s_MapType"]) ? -1 : ((Request.QueryString["s_MapType"]).IndexOf("Hospital", StringComparison.CurrentCulture) >= 0) ? (Request.QueryString["s_MapType"]).IndexOf("Hospital", StringComparison.CurrentCulture) : -1) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "") : "") : (((MapTypeValue_Current.HospitalAvailable) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "").ToString() : ""));
      ObjectDataSource_Hospital_Town.SelectParameters["province"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_Town.SelectParameters.Add("subRegion", TypeCode.String, (Request.QueryString["s_HSubRegion"] ?? DropDownList_Hospital_SubRegion.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_Town.SelectParameters["subRegion"].ConvertEmptyStringToNull = false;

      ObjectDataSource_Hospital_Organisation.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalOrganisation";
      ObjectDataSource_Hospital_Organisation.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_Hospital_Organisation.SelectParameters.Clear();
      ObjectDataSource_Hospital_Organisation.SelectParameters.Add("country", TypeCode.String, (Request.QueryString["s_Country"] ?? DropDownList_Country.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_Organisation.SelectParameters["country"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_Organisation.SelectParameters.Add("province", TypeCode.String, (PageLoad_Current.PageLoading == "Yes") ? (((string.IsNullOrEmpty(Request.QueryString["s_MapType"]) ? -1 : (Request.QueryString["s_MapType"]).IndexOf("Hospital", StringComparison.CurrentCulture)) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "") : "") : (((MapTypeValue_Current.HospitalAvailable) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "").ToString() : ""));
      ObjectDataSource_Hospital_Organisation.SelectParameters["province"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_Organisation.SelectParameters.Add("subRegion", TypeCode.String, (Request.QueryString["s_HSubRegion"] ?? DropDownList_Hospital_SubRegion.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_Organisation.SelectParameters["subRegion"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_Organisation.SelectParameters.Add("town", TypeCode.String, (Request.QueryString["s_HTown"] ?? DropDownList_Hospital_Town.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_Organisation.SelectParameters["town"].ConvertEmptyStringToNull = false;

      ObjectDataSource_Hospital_Type.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalType";
      ObjectDataSource_Hospital_Type.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_Hospital_Type.SelectParameters.Clear();
      ObjectDataSource_Hospital_Type.SelectParameters.Add("country", TypeCode.String, (Request.QueryString["s_Country"] ?? DropDownList_Country.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_Type.SelectParameters["country"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_Type.SelectParameters.Add("province", TypeCode.String, (PageLoad_Current.PageLoading == "Yes") ? (((string.IsNullOrEmpty(Request.QueryString["s_MapType"]) ? -1 : (Request.QueryString["s_MapType"]).IndexOf("Hospital", StringComparison.CurrentCulture)) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "") : "") : (((MapTypeValue_Current.HospitalAvailable) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "").ToString() : ""));
      ObjectDataSource_Hospital_Type.SelectParameters["province"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_Type.SelectParameters.Add("subRegion", TypeCode.String, (Request.QueryString["s_HSubRegion"] ?? DropDownList_Hospital_SubRegion.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_Type.SelectParameters["subRegion"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_Type.SelectParameters.Add("town", TypeCode.String, (Request.QueryString["s_HTown"] ?? DropDownList_Hospital_Town.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_Type.SelectParameters["town"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_Type.SelectParameters.Add("organisation", TypeCode.String, (Request.QueryString["s_HOrganisation"] ?? TextBox_Hospital_Organisation.Text ?? "").ToString());
      ObjectDataSource_Hospital_Type.SelectParameters["organisation"].ConvertEmptyStringToNull = false;

      ObjectDataSource_Hospital_NamedropDown.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_List_HospitalName";
      ObjectDataSource_Hospital_NamedropDown.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters.Clear();
      ObjectDataSource_Hospital_NamedropDown.SelectParameters.Add("country", TypeCode.String, (Request.QueryString["s_Country"] ?? DropDownList_Country.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["country"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_NamedropDown.SelectParameters.Add("province", TypeCode.String, (PageLoad_Current.PageLoading == "Yes") ? (((string.IsNullOrEmpty(Request.QueryString["s_MapType"]) ? -1 : (Request.QueryString["s_MapType"]).IndexOf("Hospital", StringComparison.CurrentCulture)) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "") : "") : (((MapTypeValue_Current.HospitalAvailable) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "").ToString() : ""));
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["province"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_NamedropDown.SelectParameters.Add("subRegion", TypeCode.String, (Request.QueryString["s_HSubRegion"] ?? DropDownList_Hospital_SubRegion.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["subRegion"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_NamedropDown.SelectParameters.Add("town", TypeCode.String, (Request.QueryString["s_HTown"] ?? DropDownList_Hospital_Town.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["town"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_NamedropDown.SelectParameters.Add("organisation", TypeCode.String, (Request.QueryString["s_HOrganisation"] ?? TextBox_Hospital_Organisation.Text ?? "").ToString());
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["organisation"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Hospital_NamedropDown.SelectParameters.Add("type", TypeCode.String, (Request.QueryString["s_HType"] ?? DropDownList_Hospital_Type.SelectedValue ?? "").ToString());
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["type"].ConvertEmptyStringToNull = false;

      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_Hospital";
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectParameters.Clear();
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectParameters.Add("organisation", TypeCode.String, Request.QueryString["s_HOrganisation"]);
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectParameters.Add("facilityNamedropDown", TypeCode.String, Request.QueryString["s_HNameDropDown"]);
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectParameters.Add("facilityNameTextBox", TypeCode.String, Request.QueryString["s_HNameTextBox"]);
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectParameters.Add("type", TypeCode.String, Request.QueryString["s_HType"]);
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectParameters.Add("country", TypeCode.String, Request.QueryString["s_Country"]);
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectParameters.Add("province", TypeCode.String, Request.QueryString["s_Province"]);
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectParameters.Add("subRegion", TypeCode.String, Request.QueryString["s_HSubRegion"]);
      ObjectDataSource_ExecutiveMarketInquiry_Hospital_List.SelectParameters.Add("town", TypeCode.String, Request.QueryString["s_HTown"]);
    }

    private void DataSourceSetup_Population()
    {
      PageLoad PageLoad_Current = GetPageLoad();
      MapTypeValue MapTypeValue_Current = GetMapTypeValue();

      ObjectDataSource_Population_Municipality.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_List_PopulationMunicipality";
      ObjectDataSource_Population_Municipality.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_Population_Municipality.SelectParameters.Clear();
      ObjectDataSource_Population_Municipality.SelectParameters.Add("country", TypeCode.String, (Request.QueryString["s_Country"] ?? DropDownList_Country.SelectedValue ?? "").ToString());
      ObjectDataSource_Population_Municipality.SelectParameters["country"].ConvertEmptyStringToNull = false;
      ObjectDataSource_Population_Municipality.SelectParameters.Add("province", TypeCode.String, (PageLoad_Current.PageLoading == "Yes") ? (((string.IsNullOrEmpty(Request.QueryString["s_MapType"]) ? -1 : (Request.QueryString["s_MapType"]).IndexOf("Population", StringComparison.CurrentCulture)) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "") : "") : (((MapTypeValue_Current.PopulationAvailable) >= 0) ? (Request.QueryString["s_Province"] ?? DropDownList_Province.SelectedValue ?? "").ToString() : ""));
      ObjectDataSource_Population_Municipality.SelectParameters["province"].ConvertEmptyStringToNull = false;

      ObjectDataSource_ExecutiveMarketInquiry_Population_List.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_Population";
      ObjectDataSource_ExecutiveMarketInquiry_Population_List.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_ExecutiveMarketInquiry_Population_List.SelectParameters.Clear();
      ObjectDataSource_ExecutiveMarketInquiry_Population_List.SelectParameters.Add("country", TypeCode.String, Request.QueryString["s_Country"]);
      ObjectDataSource_ExecutiveMarketInquiry_Population_List.SelectParameters.Add("province", TypeCode.String, Request.QueryString["s_Province"]);
      ObjectDataSource_ExecutiveMarketInquiry_Population_List.SelectParameters.Add("municipality", TypeCode.String, Request.QueryString["s_PMunicipality"]);
    }

    private void DataSourceSetup_MedicalScheme()
    {
      ObjectDataSource_ExecutiveMarketInquiry_MedicalScheme_List.SelectMethod = "DataPatient_ODS_ExecutiveMarketInquiry_MedicalScheme";
      ObjectDataSource_ExecutiveMarketInquiry_MedicalScheme_List.TypeName = "InfoQuestWCF.InfoQuest_DataPatient";
      ObjectDataSource_ExecutiveMarketInquiry_MedicalScheme_List.SelectParameters.Clear();
      ObjectDataSource_ExecutiveMarketInquiry_MedicalScheme_List.SelectParameters.Add("country", TypeCode.String, Request.QueryString["s_Country"]);
      ObjectDataSource_ExecutiveMarketInquiry_MedicalScheme_List.SelectParameters.Add("province", TypeCode.String, Request.QueryString["s_Province"]);
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(TextBox_MapType.Text.ToString()))
      {
        if (Request.QueryString["s_MapType"] == null)
        {
          TextBox_MapType.Text = "";
        }
        else
        {
          TextBox_MapType.Text = Request.QueryString["s_MapType"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Country.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Country"] == null)
        {
          DropDownList_Country.SelectedValue = "";
        }
        else
        {
          DropDownList_Country.SelectedValue = Request.QueryString["s_Country"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Province.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_Province"] == null)
        {
          DropDownList_Province.SelectedValue = "";
        }
        else
        {
          DropDownList_Province.SelectedValue = Request.QueryString["s_Province"];
        }
      }

      SetFormQueryString_Hospital();

      SetFormQueryString_Population();
    }

    private void SetFormQueryString_Hospital()
    {
      if (string.IsNullOrEmpty(DropDownList_Hospital_SubRegion.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_HSubRegion"] == null)
        {
          DropDownList_Hospital_SubRegion.SelectedValue = "";
        }
        else
        {
          DropDownList_Hospital_SubRegion.SelectedValue = Request.QueryString["s_HSubRegion"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Hospital_Town.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_HTown"] == null)
        {
          DropDownList_Hospital_Town.SelectedValue = "";
        }
        else
        {
          DropDownList_Hospital_Town.SelectedValue = Request.QueryString["s_HTown"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_Hospital_Organisation.Text.ToString()))
      {
        if (Request.QueryString["s_HOrganisation"] == null)
        {
          TextBox_Hospital_Organisation.Text = "";
        }
        else
        {
          TextBox_Hospital_Organisation.Text = Request.QueryString["s_HOrganisation"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Hospital_Type.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_HType"] == null)
        {
          DropDownList_Hospital_Type.SelectedValue = "";
        }
        else
        {
          DropDownList_Hospital_Type.SelectedValue = Request.QueryString["s_HType"];
        }
      }

      if (string.IsNullOrEmpty(DropDownList_Hospital_NamedropDown.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_HNameDropDown"] == null)
        {
          DropDownList_Hospital_NamedropDown.SelectedValue = "";
        }
        else
        {
          DropDownList_Hospital_NamedropDown.SelectedValue = Request.QueryString["s_HNameDropDown"];
        }
      }

      if (string.IsNullOrEmpty(TextBox_Hospital_NameTextBox.Text.ToString()))
      {
        if (Request.QueryString["s_HNameTextBox"] == null)
        {
          TextBox_Hospital_NameTextBox.Text = "";
        }
        else
        {
          TextBox_Hospital_NameTextBox.Text = Request.QueryString["s_HNameTextBox"];
        }
      }
    }

    private void SetFormQueryString_Population()
    {
      if (string.IsNullOrEmpty(DropDownList_Population_Municipality.SelectedValue.ToString()))
      {
        if (Request.QueryString["s_PMunicipality"] == null)
        {
          DropDownList_Population_Municipality.SelectedValue = "";
        }
        else
        {
          DropDownList_Population_Municipality.SelectedValue = Request.QueryString["s_PMunicipality"];
        }
      }
    }

    private class PageLoad
    {
      [DefaultValue("Yes")]
      public string PageLoading { get; set; }
    }

    private PageLoad GetPageLoad()
    {
      PageLoad PageLoad_New = new PageLoad();

      if (!IsPostBack)
      {
        PageLoad_New.PageLoading = "Yes";
      }
      else
      {
        PageLoad_New.PageLoading = "No";
      }

      return PageLoad_New;
    }


    string ShowHospital = "Yes";
    string ShowPopulation = "Yes";
    string ShowMedicalScheme = "Yes";

    private void LoadMap()
    {
      LoadMap_ShowHideGrid();


      //--START-- --Hospital--//
      //[ 0:Latitude , 1:Longitude , 2:Title , 3:Content , 4:Icon ]
      string Hospital = "";
      Int32 HospitalCount = 0;
      DataTable DataTable_ExecutiveMarketInquiry_Hospital;
      using (DataTable_ExecutiveMarketInquiry_Hospital = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_Hospital.Locale = CultureInfo.CurrentCulture;

        if (ShowHospital == "Yes")
        {
          MapType MapType_Hospital = GetMapType_Hospital();
          Hospital = MapType_Hospital.MapTypeMarkers;
          HospitalCount = MapType_Hospital.MapTypeCount;
          DataTable_ExecutiveMarketInquiry_Hospital = MapType_Hospital.MapTypeDataTable;
        }
      }
      //---END--- --Hospital--//


      //--START-- --Population--//
      //[ 0:Latitude , 1:Longitude , 2:Title , 3:Content , 4:Icon , 5:RadiusValue , 6:RadiusColor ]
      string Population = "";
      Int32 PopulationCount = 0;
      DataTable DataTable_ExecutiveMarketInquiry_Population;
      using (DataTable_ExecutiveMarketInquiry_Population = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_Population.Locale = CultureInfo.CurrentCulture;

        if (ShowPopulation == "Yes")
        {
          MapType MapType_Population = GetMapType_Population();
          Population = MapType_Population.MapTypeMarkers;
          PopulationCount = MapType_Population.MapTypeCount;
          DataTable_ExecutiveMarketInquiry_Population = MapType_Population.MapTypeDataTable;
        }
      }
      //---END--- --Population--//


      //--START-- --MedicalScheme--//
      //[ 0:Latitude , 1:Longitude , 2:Title , 3:Content , 4:Icon , 5:RadiusValue , 6:RadiusColor ]
      string MedicalScheme = "";
      Int32 MedicalSchemeCount = 0;
      DataTable DataTable_ExecutiveMarketInquiry_MedicalScheme;
      using (DataTable_ExecutiveMarketInquiry_MedicalScheme = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_MedicalScheme.Locale = CultureInfo.CurrentCulture;

        if (ShowMedicalScheme == "Yes")
        {
          MapType MapType_MedicalScheme = GetMapType_MedicalScheme();
          MedicalScheme = MapType_MedicalScheme.MapTypeMarkers;
          MedicalSchemeCount = MapType_MedicalScheme.MapTypeCount;
          DataTable_ExecutiveMarketInquiry_MedicalScheme = MapType_MedicalScheme.MapTypeDataTable;
        }
      }
      //---END--- --MedicalScheme--//


      //--START-- --MapLegend--//
      LoadMap_MapLegend(DataTable_ExecutiveMarketInquiry_Hospital, DataTable_ExecutiveMarketInquiry_Population, DataTable_ExecutiveMarketInquiry_MedicalScheme);
      //---END--- --MapLegend--//


      //--START-- --MarkerCount--//
      Label_TotalHospital_Map.Text = HospitalCount.ToString(CultureInfo.CurrentCulture);
      Label_TotalPopulation_Map.Text = PopulationCount.ToString(CultureInfo.CurrentCulture);
      Label_TotalMedicalScheme_Map.Text = MedicalSchemeCount.ToString(CultureInfo.CurrentCulture);

      Int32 MarkerCount = HospitalCount + PopulationCount + MedicalSchemeCount;
      //---END--- --MarkerCount--//


      //--START-- --GoogleMapAPIKey--//
      string GoogleMapAPIKey = GetGoogleMapAPIKey();
      //---END--- --GoogleMapAPIKey--//


      Literal Literal_LiteralJavaScript = (Literal)FindControl("Literal_JavaScript");
      if (Literal_LiteralJavaScript != null)
      {
        Literal_LiteralJavaScript.Text = Convert.ToString(@"
          <script src='http://maps.googleapis.com/maps/api/js?key=" + GoogleMapAPIKey + @"&sensor=false'></script>
          <script>
            //[ 0:Latitude , 1:Longitude , 2:Title , 3:Content , 4:Icon ]
            var HospitalLocationData = [
              " + Hospital + @"
            ];

            //[ 0:Latitude , 1:Longitude , 2:Title , 3:Content , 4:Icon , 5:RadiusValue , 6:RadiusColor ]
            var PopulationLocationData = [
              " + Population + @"
            ];

            //[ 0:Latitude , 1:Longitude , 2:Title , 3:Content , 4:Icon , 5:RadiusValue , 6:RadiusColor ]
            var MedicalSchemeLocationData = [
              " + MedicalScheme + @"
            ];

            var Map;
            var MapMarker = [];

            function GoogleMarkerBounce(BounceIcon)
            {
              for (var i = 0; i < MapMarker.length; i++)
              {
                if (MapMarker[i].icon.url == BounceIcon)
                {
                  if (MapMarker[i].getAnimation() != null)
                  {
                    MapMarker[i].setAnimation(null);
                  }
                  else
                  {
                    MapMarker[i].setAnimation(google.maps.Animation.BOUNCE);
                  }
                }
                else
                {
                  MapMarker[i].setAnimation(null);
                }
              }
            }

            function GoogleMapInitialize()
            {
              //----------------------------------------------------------------------------------------------------
              //--START-- --Map--//
              var MapOptions = {
                panControl: true,
                zoomControl: true,
                mapTypeControl: true,
                scaleControl: true,
                streetViewControl: true,
                overviewMapControl: true,
                rotateControl: true,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                scrollwheel: false,
                zoom: 6,
                center: new google.maps.LatLng(-30.559482, 22.937506) 
              };

              Map = new google.maps.Map(document.getElementById('googleMap'), MapOptions);
              var MapBounds = new google.maps.LatLngBounds();
              var MarkerInfowindow = new google.maps.InfoWindow();


              //--START-- --ContextMenu--//
              var directionsRendererOptions={};
	            directionsRendererOptions.draggable=false;
	            directionsRendererOptions.hideRouteList=true;
	            directionsRendererOptions.suppressMarkers=false;
	            directionsRendererOptions.preserveViewport=false;
	            var directionsRenderer=new google.maps.DirectionsRenderer(directionsRendererOptions);
	            var directionsService=new google.maps.DirectionsService();
	
	            var contextMenuOptions={};
	            contextMenuOptions.classNames={menu:'context_menu', menuSeparator:'context_menu_separator'};
	
	            var menuItems=[];
	            //menuItems.push({className:'context_menu_item', eventName:'directions_origin_click', id:'directionsOriginItem', label:'Directions from here'});
	            //menuItems.push({className:'context_menu_item', eventName:'directions_destination_click', id:'directionsDestinationItem', label:'Directions to here'});
	            //menuItems.push({className:'context_menu_item', eventName:'clear_directions_click', id:'clearDirectionsItem', label:'Clear directions'});
	            //menuItems.push({className:'context_menu_item', eventName:'get_directions_click', id:'getDirectionsItem', label:'Get directions'});
	            //menuItems.push({});
	            menuItems.push({className:'context_menu_item', eventName:'zoom_in_click', label:'Zoom in'});
	            menuItems.push({className:'context_menu_item', eventName:'zoom_out_click', label:'Zoom out'});
	            menuItems.push({});
	            menuItems.push({className:'context_menu_item', eventName:'center_map_click', label:'Center map here'});
	            contextMenuOptions.menuItems=menuItems;
	
	            var contextMenu=new ContextMenu(Map, contextMenuOptions);
	
	            google.maps.event.addListener(Map, 'rightclick', function(mouseEvent){
		            contextMenu.show(mouseEvent.latLng);
	            });
	
	            var markerOptions={};
	            markerOptions.icon='App_Images\Icons\markerA.png';
	            markerOptions.map=null;
	            markerOptions.position=new google.maps.LatLng(0, 0);
	            markerOptions.title='Directions origin';
	
	            var originMarker=new google.maps.Marker(markerOptions);
	
	            markerOptions.icon='App_Images\Icons\markerB.png';
	            markerOptions.title='Directions destination';
	            var destinationMarker=new google.maps.Marker(markerOptions);
	
	            google.maps.event.addListener(contextMenu, 'menu_item_selected', function(latLng, eventName){
		            switch(eventName){
			            case 'directions_origin_click':
				            originMarker.setPosition(latLng);
				            if(!originMarker.getMap()){
					            originMarker.setMap(Map);
				            }
				            break;
			            case 'directions_destination_click':
				            destinationMarker.setPosition(latLng);
				            if(!destinationMarker.getMap()){
					            destinationMarker.setMap(Map);
				            }
				            break;
			            case 'clear_directions_click':
				            directionsRenderer.setMap(null);
				            document.getElementById('clearDirectionsItem').style.display='';
				            document.getElementById('directionsDestinationItem').style.display='';
				            document.getElementById('directionsOriginItem').style.display='';
				            document.getElementById('getDirectionsItem').style.display='';
				            break;
			            case 'get_directions_click':
				            var directionsRequest={};
				            directionsRequest.destination=destinationMarker.getPosition();
				            directionsRequest.origin=originMarker.getPosition();
				            directionsRequest.travelMode=google.maps.TravelMode.DRIVING;
				
				            directionsService.route(directionsRequest, function(result, status){
					            if(status===google.maps.DirectionsStatus.OK){
						            originMarker.setMap(null);
						            destinationMarker.setMap(null);
						            directionsRenderer.setDirections(result);
						            directionsRenderer.setMap(Map);
						            document.getElementById('clearDirectionsItem').style.display='block';
						            document.getElementById('directionsDestinationItem').style.display='none';
						            document.getElementById('directionsOriginItem').style.display='none';
						            document.getElementById('getDirectionsItem').style.display='none';
					            } else {
						            alert('Sorry, the map was unable to obtain directions.\n\nThe request failed with the message: '+status);
					            }
				            });
				            break;
			            case 'zoom_in_click':
				            Map.setZoom(Map.getZoom()+1);
				            break;
			            case 'zoom_out_click':
				            Map.setZoom(Map.getZoom()-1);
				            break;
			            case 'center_map_click':
				            Map.panTo(latLng);
				            break;
		            }
		            if(originMarker.getMap() && destinationMarker.getMap() && document.getElementById('getDirectionsItem').style.display===''){
			            document.getElementById('getDirectionsItem').style.display='block';
		            }
	            });
              //---END--- --ContextMenu--//


              var OverlappingMarker = new OverlappingMarkerSpiderfier(Map,
                {
                  markersWontMove: true,
                  markersWontHide: true
                });

              OverlappingMarker.addListener('click', function (MapMarker)
              {
                MarkerInfowindow.setContent(MapMarker.content);
                MarkerInfowindow.open(Map, MapMarker);
              });

              OverlappingMarker.addListener('spiderfy', function (MapMarker)
              {
                //MarkerInfowindow.close();
              });

              OverlappingMarker.addListener('unspiderfy', function (MapMarker)
              {

              });
              //---END--- --Map--//
              //----------------------------------------------------------------------------------------------------


              //----------------------------------------------------------------------------------------------------
              //--START-- --MedicalScheme--//
              //[ 0:Latitude , 1:Longitude , 2:Title , 3:Content , 4:Icon , 5:RadiusValue , 6:RadiusColor ]
              var MapMarker_MedicalScheme;
              for (var b in MedicalSchemeLocationData)
              {
                var MedicalScheme = MedicalSchemeLocationData[b]
                var LatLong = new google.maps.LatLng(MedicalScheme[0], MedicalScheme[1]);
                var ExtendBounds = new google.maps.LatLngBounds();
                ExtendBounds.extend(LatLong);
                MapBounds.union(ExtendBounds);
                var IconImage = {
                  url: MedicalScheme[4],
                  scaledSize: new google.maps.Size(25, 30)
                }

                MapMarker_MedicalScheme = new google.maps.Marker({
                  position: LatLong,
                  map: Map,
                  title: MedicalScheme[2],
                  content: MedicalScheme[3],
                  icon: IconImage
                });

                OverlappingMarker.addMarker(MapMarker_MedicalScheme);
                //google.maps.event.addListener(MapMarker_MedicalScheme, 'click', function ()
                //{
                //  MarkerInfowindow.setContent(this.content);
                //  MarkerInfowindow.open(Map, this);
                //});

                var MedicalSchemeCircle = new google.maps.Circle({
                  strokeColor: MedicalScheme[6],
                  strokeOpacity: 0.8,
                  strokeWeight: 2,
                  fillColor: MedicalScheme[6],
                  fillOpacity: 0.1,
                  map: Map,
                  center: LatLong,
                  clickable: false,
                  radius: Math.sqrt(MedicalScheme[5]) * 100
                });

                MedicalSchemeCircle.bindTo('center', MapMarker_MedicalScheme, 'position');
                MapMarker.push(MapMarker_MedicalScheme);
              }
              //---END--- --MedicalScheme--//
              //----------------------------------------------------------------------------------------------------


              //----------------------------------------------------------------------------------------------------
              //--START-- --Population--//
              //[ 0:Latitude , 1:Longitude , 2:Title , 3:Content , 4:Icon , 5:RadiusValue , 6:RadiusColor ]
              var MapMarker_Population;
              for (var b in PopulationLocationData)
              {
                var Population = PopulationLocationData[b]
                var LatLong = new google.maps.LatLng(Population[0], Population[1]);
                var ExtendBounds = new google.maps.LatLngBounds();
                ExtendBounds.extend(LatLong);
                MapBounds.union(ExtendBounds);
                var IconImage = {
                  url: Population[4],
                  scaledSize: new google.maps.Size(25, 30)
                }

                MapMarker_Population = new google.maps.Marker({
                  position: LatLong,
                  map: Map,
                  title: Population[2],
                  content: Population[3],
                  icon: IconImage
                });

                OverlappingMarker.addMarker(MapMarker_Population);
                //google.maps.event.addListener(MapMarker_Population, 'click', function ()
                //{
                //  MarkerInfowindow.setContent(this.content);
                //  MarkerInfowindow.open(Map, this);
                //});

                var PopulationCircle = new google.maps.Circle({
                  strokeColor: Population[6],
                  strokeOpacity: 0.8,
                  strokeWeight: 2,
                  fillColor: Population[6],
                  fillOpacity: 0.1,
                  map: Map,
                  center: LatLong,
                  clickable: false,
                  radius: Math.sqrt(Population[5]) * 100
                });

                PopulationCircle.bindTo('center', MapMarker_Population, 'position');
                MapMarker.push(MapMarker_Population);
              }
              //---END--- --Population--//
              //----------------------------------------------------------------------------------------------------


              //----------------------------------------------------------------------------------------------------
              //--START-- --Hospital--//
              //[ 0:Latitude , 1:Longitude , 2:Title , 3:Content , 4:Icon ]
              var MapMarker_Hospital;
              for (var a in HospitalLocationData)
              {
                var Hospital = HospitalLocationData[a];
                var LatLong = new google.maps.LatLng(Hospital[0], Hospital[1]);
                var ExtendBounds = new google.maps.LatLngBounds();
                ExtendBounds.extend(LatLong);
                MapBounds.union(ExtendBounds);
                var IconImage = {
                  url: Hospital[4],
                  scaledSize: new google.maps.Size(25, 30)
                }

                MapMarker_Hospital = new google.maps.Marker({
                  position: LatLong,
                  map: Map,
                  title: Hospital[2],
                  content: Hospital[3],
                  icon: IconImage
                });

                OverlappingMarker.addMarker(MapMarker_Hospital);
                //google.maps.event.addListener(MapMarker_Hospital, 'click', function ()
                //{
                //  MarkerInfowindow.setContent(this.content);
                //  MarkerInfowindow.open(Map, this);
                //});

                MapMarker.push(MapMarker_Hospital);
              }
              //---END--- --Hospital--//
              //----------------------------------------------------------------------------------------------------


              //----------------------------------------------------------------------------------------------------
              //--START-- --MarkerCount--//
              var MarkerCount = " + MarkerCount + @";

              if (MarkerCount >= 2)
              {
                Map.fitBounds(MapBounds);
                var listener = google.maps.event.addListener(Map, 'idle', function ()
                {
                  Map.setZoom(Map.getZoom() + 0);

                  google.maps.event.removeListener(listener);
                });
              }
              else if (MarkerCount == 1)
              {
                Map.setCenter(MapBounds.getCenter());
                Map.setZoom(14);
              }
              //---END--- --MarkerCount--//
              //----------------------------------------------------------------------------------------------------
            }

            google.maps.event.addDomListener(window, 'load', GoogleMapInitialize);
          </script>", CultureInfo.CurrentCulture);
      }
    }

    private void LoadMap_ShowHideGrid()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["s_MapType"]))
      {
        if (Request.QueryString["s_MapType"].IndexOf("Hospital", StringComparison.CurrentCulture) == -1)
        {
          ShowHospital = "No";
          DivHospital.Visible = false;
          TableHospital.Visible = false;
        }

        if (Request.QueryString["s_MapType"].IndexOf("Population", StringComparison.CurrentCulture) == -1)
        {
          ShowPopulation = "No";
          //DivPopulation.Visible = false;
          //TablePopulation.Visible = false;
        }

        if (Request.QueryString["s_MapType"].IndexOf("Medical Scheme", StringComparison.CurrentCulture) == -1)
        {
          ShowMedicalScheme = "No";
          //DivMedicalScheme.Visible = false;
          //TableMedicalScheme.Visible = false;
        }
      }
      else if (string.IsNullOrEmpty(Request.QueryString["s_MapType"]))
      {
        ShowHospital = "No";
        DivHospital.Visible = false;
        TableHospital.Visible = false;

        ShowPopulation = "No";
        DivPopulation.Visible = false;
        TablePopulation.Visible = false;

        ShowMedicalScheme = "No";
        DivMedicalScheme.Visible = false;
        TableMedicalScheme.Visible = false;
      }
    }

    private void LoadMap_MapLegend(DataTable dataTableExecutiveMarketInquiryHospital, DataTable dataTableExecutiveMarketInquiryPopulation, DataTable dataTableExecutiveMarketInquiryMedicalScheme)
    {
      DataTable DataTable_ExecutiveMarketInquiry_Map_HospitalLegend;
      using (DataTable_ExecutiveMarketInquiry_Map_HospitalLegend = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.Clear();
        DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.Columns.Add("IconDescription");
        DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.Columns.Add("Icon");
        DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.Columns.Add("IconCount");
        DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.Locale = CultureInfo.CurrentCulture;

        if (dataTableExecutiveMarketInquiryHospital.Rows.Count > 0)
        {
          dataTableExecutiveMarketInquiryHospital.DefaultView.Sort = "Hospital_IconDescription ASC";
          foreach (DataRow DataRow_Row in dataTableExecutiveMarketInquiryHospital.Rows)
          {
            DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.Rows.Add(DataRow_Row["Hospital_IconDescription"].ToString(), DataRow_Row["Hospital_Icon"].ToString());
          }
        }

        if (dataTableExecutiveMarketInquiryPopulation.Rows.Count > 0)
        {
          dataTableExecutiveMarketInquiryPopulation.DefaultView.Sort = "Population_IconDescription ASC";
          foreach (DataRow DataRow_Row in dataTableExecutiveMarketInquiryPopulation.Rows)
          {
            DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.Rows.Add(DataRow_Row["Population_IconDescription"].ToString(), DataRow_Row["Population_Icon"].ToString());
          }
        }

        if (dataTableExecutiveMarketInquiryMedicalScheme.Rows.Count > 0)
        {
          dataTableExecutiveMarketInquiryMedicalScheme.DefaultView.Sort = "MedicalScheme_IconDescription ASC";
          foreach (DataRow DataRow_Row in dataTableExecutiveMarketInquiryMedicalScheme.Rows)
          {
            DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.Rows.Add(DataRow_Row["MedicalScheme_IconDescription"].ToString(), DataRow_Row["MedicalScheme_Icon"].ToString());
          }
        }


        var Query_IconCount = from DataTableRow in DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.AsEnumerable()
                              group DataTableRow by DataTableRow.Field<string>("Icon") into DataTableCount
                              orderby DataTableCount.Key
                              select new
                              {
                                Icon = DataTableCount.Key,
                                IconCount = DataTableCount.Count()
                              };

        DataTable_ExecutiveMarketInquiry_Map_HospitalLegend = DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.DefaultView.ToTable(true, "IconDescription", "Icon", "IconCount").Copy();

        foreach (var Icons in Query_IconCount)
        {
          DataRow[] DataRow_Rows = DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.Select("Icon = '" + Icons.Icon + "'");

          for (int i = 0; i < DataRow_Rows.Length; i++)
          {
            DataRow_Rows[i]["IconCount"] = Icons.IconCount;
          }
        }

        DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.DefaultView.Sort = "IconDescription ASC";
        DataList_ExecutiveMarketInquiry_Map_HospitalLegend_List.DataSource = DataTable_ExecutiveMarketInquiry_Map_HospitalLegend.DefaultView.ToTable();
        DataList_ExecutiveMarketInquiry_Map_HospitalLegend_List.DataBind();
      }
    }

    private class MapType
    {
      public string MapTypeMarkers { get; set; }
      public Int32 MapTypeCount { get; set; }
      public DataTable MapTypeDataTable { get; set; }
    }

    private MapType GetMapType_Hospital()
    {
      MapType MapType_New = new MapType();

      MapType_New.MapTypeMarkers = "";
      MapType_New.MapTypeCount = 0;
      DataTable DataTable_ExecutiveMarketInquiry_Hospital;
      using (DataTable_ExecutiveMarketInquiry_Hospital = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_Hospital.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_Hospital = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_ExecutiveMarketInquiry_Hospital(Request.QueryString["s_HOrganisation"], Request.QueryString["s_HNameDropDown"], Request.QueryString["s_HNameTextBox"], Request.QueryString["s_HType"], Request.QueryString["s_Country"], Request.QueryString["s_Province"], Request.QueryString["s_HSubRegion"], Request.QueryString["s_HTown"]).Copy();
        if (DataTable_ExecutiveMarketInquiry_Hospital.Rows.Count > 0)
        {
          MapType_New.MapTypeCount = DataTable_ExecutiveMarketInquiry_Hospital.Rows.Count;

          foreach (DataRow DataRow_Row in DataTable_ExecutiveMarketInquiry_Hospital.Rows)
          {
            string Hospital_PhysicalAddress = DataRow_Row["Hospital_PhysicalAddress"].ToString();
            string Hospital_Latitude = DataRow_Row["Hospital_Latitude"].ToString();
            string Hospital_Longitude = DataRow_Row["Hospital_Longitude"].ToString();
            string Hospital_Title = DataRow_Row["Hospital_Title"].ToString();
            string Hospital_Content = DataRow_Row["Hospital_Content"].ToString();
            string Hospital_Icon = DataRow_Row["Hospital_Icon"].ToString();
            string StatusMessage = "";

            if (string.IsNullOrEmpty(Hospital_Latitude) || string.IsNullOrEmpty(Hospital_Longitude))
            {
              MapCoordinates MapCoordinates_Current = GetMapCoordinates(Hospital_PhysicalAddress);
              Hospital_Latitude = MapCoordinates_Current.Latitude.ToString(CultureInfo.CurrentCulture);
              Hospital_Longitude = MapCoordinates_Current.Longitude.ToString(CultureInfo.CurrentCulture);
              StatusMessage = MapCoordinates_Current.StatusMessage;
            }

            if (StatusMessage == "Ok" || string.IsNullOrEmpty(StatusMessage) || !string.IsNullOrEmpty(StatusMessage))
            {
              MapType_New.MapTypeMarkers += Environment.NewLine + @"[" + Hospital_Latitude + " , " + Hospital_Longitude + " , '" + Hospital_Title + "' , '" + Hospital_Content + "' , '" + Hospital_Icon + "'],";
            }
          }

          if (!string.IsNullOrEmpty(MapType_New.MapTypeMarkers))
          {
            MapType_New.MapTypeMarkers = MapType_New.MapTypeMarkers.Remove(MapType_New.MapTypeMarkers.Length - 1, 1);
          }
        }
      }

      MapType_New.MapTypeDataTable = DataTable_ExecutiveMarketInquiry_Hospital.Copy();

      return MapType_New;
    }

    private MapType GetMapType_Population()
    {
      MapType MapType_New = new MapType();

      MapType_New.MapTypeMarkers = "";
      MapType_New.MapTypeCount = 0;
      DataTable DataTable_ExecutiveMarketInquiry_Population;
      using (DataTable_ExecutiveMarketInquiry_Population = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_Population.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_Population = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_ExecutiveMarketInquiry_Population(Request.QueryString["s_Country"], Request.QueryString["s_Province"], Request.QueryString["s_PMunicipality"]).Copy();
        if (DataTable_ExecutiveMarketInquiry_Population.Rows.Count > 0)
        {
          MapType_New.MapTypeCount = DataTable_ExecutiveMarketInquiry_Population.Rows.Count;

          foreach (DataRow DataRow_Row in DataTable_ExecutiveMarketInquiry_Population.Rows)
          {
            string Population_Address = DataRow_Row["Population_Address"].ToString();
            string Population_Latitude = DataRow_Row["Population_Latitude"].ToString();
            string Population_Longitude = DataRow_Row["Population_Longitude"].ToString();
            string Population_Title = DataRow_Row["Population_Title"].ToString();
            string Population_Content = DataRow_Row["Population_Content"].ToString();
            string Population_Icon = DataRow_Row["Population_Icon"].ToString();
            string Population_RadiusValue = DataRow_Row["Population_RadiusValue"].ToString();
            string Population_RadiusColor = DataRow_Row["Population_RadiusColor"].ToString();
            string StatusMessage = "";

            if (string.IsNullOrEmpty(Population_Latitude) || string.IsNullOrEmpty(Population_Longitude))
            {
              MapCoordinates MapCoordinates_Current = GetMapCoordinates(Population_Address);
              Population_Latitude = MapCoordinates_Current.Latitude.ToString(CultureInfo.CurrentCulture);
              Population_Longitude = MapCoordinates_Current.Longitude.ToString(CultureInfo.CurrentCulture);
              StatusMessage = MapCoordinates_Current.StatusMessage;
            }

            if (StatusMessage == "Ok" || string.IsNullOrEmpty(StatusMessage) || !string.IsNullOrEmpty(StatusMessage))
            {
              MapType_New.MapTypeMarkers += Environment.NewLine + @"[" + Population_Latitude + " , " + Population_Longitude + " , '" + Population_Title + "' , '" + Population_Content + "' , '" + Population_Icon + "' , '" + Population_RadiusValue + "' , '" + Population_RadiusColor + "'],";
            }
          }

          if (!string.IsNullOrEmpty(MapType_New.MapTypeMarkers))
          {
            MapType_New.MapTypeMarkers = MapType_New.MapTypeMarkers.Remove(MapType_New.MapTypeMarkers.Length - 1, 1);
          }
        }
      }

      MapType_New.MapTypeDataTable = DataTable_ExecutiveMarketInquiry_Population.Copy();

      return MapType_New;
    }

    private MapType GetMapType_MedicalScheme()
    {
      MapType MapType_New = new MapType();

      MapType_New.MapTypeMarkers = "";
      MapType_New.MapTypeCount = 0;
      DataTable DataTable_ExecutiveMarketInquiry_MedicalScheme;
      using (DataTable_ExecutiveMarketInquiry_MedicalScheme = new DataTable())
      {
        DataTable_ExecutiveMarketInquiry_MedicalScheme.Locale = CultureInfo.CurrentCulture;
        DataTable_ExecutiveMarketInquiry_MedicalScheme = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_ExecutiveMarketInquiry_MedicalScheme(Request.QueryString["s_Country"], Request.QueryString["s_Province"]).Copy();
        if (DataTable_ExecutiveMarketInquiry_MedicalScheme.Rows.Count > 0)
        {
          MapType_New.MapTypeCount = DataTable_ExecutiveMarketInquiry_MedicalScheme.Rows.Count;

          foreach (DataRow DataRow_Row in DataTable_ExecutiveMarketInquiry_MedicalScheme.Rows)
          {
            string MedicalScheme_Address = DataRow_Row["MedicalScheme_Address"].ToString();
            string MedicalScheme_Latitude = DataRow_Row["MedicalScheme_Latitude"].ToString();
            string MedicalScheme_Longitude = DataRow_Row["MedicalScheme_Longitude"].ToString();
            string MedicalScheme_Title = DataRow_Row["MedicalScheme_Title"].ToString();
            string MedicalScheme_Content = DataRow_Row["MedicalScheme_Content"].ToString();
            string MedicalScheme_Icon = DataRow_Row["MedicalScheme_Icon"].ToString();
            string MedicalScheme_RadiusValue = DataRow_Row["MedicalScheme_RadiusValue"].ToString();
            string MedicalScheme_RadiusColor = DataRow_Row["MedicalScheme_RadiusColor"].ToString();
            string StatusMessage = "";

            if (string.IsNullOrEmpty(MedicalScheme_Latitude) || string.IsNullOrEmpty(MedicalScheme_Longitude))
            {
              MapCoordinates MapCoordinates_Current = GetMapCoordinates(MedicalScheme_Address);
              MedicalScheme_Latitude = MapCoordinates_Current.Latitude.ToString(CultureInfo.CurrentCulture);
              MedicalScheme_Longitude = MapCoordinates_Current.Longitude.ToString(CultureInfo.CurrentCulture);
              StatusMessage = MapCoordinates_Current.StatusMessage;
            }

            if (StatusMessage == "Ok" || string.IsNullOrEmpty(StatusMessage) || !string.IsNullOrEmpty(StatusMessage))
            {
              MapType_New.MapTypeMarkers += Environment.NewLine + @"[" + MedicalScheme_Latitude + " , " + MedicalScheme_Longitude + " , '" + MedicalScheme_Title + "' , '" + MedicalScheme_Content + "' , '" + MedicalScheme_Icon + "' , '" + MedicalScheme_RadiusValue + "' , '" + MedicalScheme_RadiusColor + "'],";
            }
          }

          if (!string.IsNullOrEmpty(MapType_New.MapTypeMarkers))
          {
            MapType_New.MapTypeMarkers = MapType_New.MapTypeMarkers.Remove(MapType_New.MapTypeMarkers.Length - 1, 1);
          }
        }
      }

      MapType_New.MapTypeDataTable = DataTable_ExecutiveMarketInquiry_MedicalScheme.Copy();

      return MapType_New;
    }

    private class MapCoordinates
    {
      public Double Latitude { get; set; }
      public Double Longitude { get; set; }
      public string StatusMessage { get; set; }
    }

    private static MapCoordinates GetMapCoordinates(string address)
    {
      MapCoordinates MapCoordinates_New = new MapCoordinates();

      if (string.IsNullOrEmpty(address))
      {
        MapCoordinates_New.Latitude = 0;
        MapCoordinates_New.Longitude = 0;
        MapCoordinates_New.StatusMessage = "";
      }

      //String RequestUriString = String.Format(CultureInfo.CurrentCulture, "https://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));
      //HttpWebRequest HttpWebRequest_Request = (HttpWebRequest)WebRequest.Create(RequestUriString);

      //try
      //{
      //  HttpWebResponse HttpWebResponse_Response = (HttpWebResponse)HttpWebRequest_Request.GetResponse();
      //  XDocument XDocument_File = XDocument.Load(HttpWebResponse_Response.GetResponseStream());
      //  String SearchStatus = XDocument_File.Element("GeocodeResponse").Element("status").Value;

      //  String StatusMessage = "";
      //  if (SearchStatus == "ZERO_RESULTS")
      //  {
      //    StatusMessage = "No coordinates found for address: " + address;
      //  }
      //  else if (SearchStatus == "OVER_QUERY_LIMIT")
      //  {
      //    StatusMessage = "Query limit has been reached for the day";
      //  }
      //  else if (SearchStatus == "OK")
      //  {
      //    StatusMessage = "Ok";
      //  }
      //  else
      //  {
      //    StatusMessage = SearchStatus;
      //  }

      //  XElement XElement_Location = XDocument_File.Element("GeocodeResponse").Element("result").Element("geometry").Element("location");
      //  MapCoordinates_New.Latitude = (Double)XElement_Location.Element("lat");
      //  MapCoordinates_New.Longitude = (Double)XElement_Location.Element("lng");
      //  MapCoordinates_New.StatusMessage = StatusMessage;
      //}
      //catch (WebException WebException_Exception)
      //{
      //  MapCoordinates_New.Latitude = 0;
      //  MapCoordinates_New.Longitude = 0;
      //  MapCoordinates_New.StatusMessage = WebException_Exception.ToString();
      //}

      return MapCoordinates_New;
    }

    private static string GetGoogleMapAPIKey()
    {
      string GoogleMapAPIKey = "";
      string SQLStringGoogleMapAPIKey = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListCategory_Id = 180 AND ListItem_IsActive = 1";
      using (SqlCommand SqlCommand_GoogleMapAPIKey = new SqlCommand(SQLStringGoogleMapAPIKey))
      {
        DataTable DataTable_GoogleMapAPIKey;
        using (DataTable_GoogleMapAPIKey = new DataTable())
        {
          DataTable_GoogleMapAPIKey.Locale = CultureInfo.CurrentCulture;
          DataTable_GoogleMapAPIKey = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_GoogleMapAPIKey).Copy();
          if (DataTable_GoogleMapAPIKey.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_GoogleMapAPIKey.Rows)
            {
              GoogleMapAPIKey = DataRow_Row["ListItem_Name"].ToString();
            }
          }
        }
      }

      return GoogleMapAPIKey;
    }


    //--START-- --Search--//
    private string GetPostBackControlName()
    {
      Control Control_Page = null;
      string ControlName = Page.Request.Params["__EVENTTARGET"];

      if (!string.IsNullOrEmpty(ControlName))
      {
        Control_Page = Page.FindControl(ControlName);
      }
      else
      {
        string ControlId = string.Empty;
        Control Control_Found = null;

        foreach (string Controls in Page.Request.Form)
        {
          if (Controls.EndsWith(".x", StringComparison.CurrentCulture) || Controls.EndsWith(".y", StringComparison.CurrentCulture))
          {
            ControlId = Controls.Substring(0, Controls.Length - 2);
            Control_Found = Page.FindControl(ControlId);
          }
          else
          {
            Control_Found = Page.FindControl(Controls);
          }

          if (Control_Found is Button || Control_Found is ImageButton)
          {
            Control_Page = Control_Found;
            break;
          }
        }
      }

      return Control_Page == null ? string.Empty : Control_Page.ID;
    }

    private class MapTypeValue
    {
      public Int32 HospitalAvailable { get; set; }
      public Int32 PopulationAvailable { get; set; }
      public Int32 MedicalSchemeAvailable { get; set; }
    }

    private MapTypeValue GetMapTypeValue()
    {
      MapTypeValue MapTypeValue_New = new MapTypeValue();

      string MapTypeValue = "";

      for (int i = 0; i < CheckBoxList_MapType.Items.Count; i++)
      {
        if (CheckBoxList_MapType.Items[i].Selected)
        {
          MapTypeValue += CheckBoxList_MapType.Items[i].Text + ",";
        }
      }

      MapTypeValue_New.HospitalAvailable = MapTypeValue.IndexOf("Hospital", StringComparison.CurrentCulture);
      MapTypeValue_New.PopulationAvailable = MapTypeValue.IndexOf("Population", StringComparison.CurrentCulture);
      MapTypeValue_New.MedicalSchemeAvailable = MapTypeValue.IndexOf("Medical Scheme", StringComparison.CurrentCulture);

      return MapTypeValue_New;
    }

    protected void CheckBoxList_MapType_SelectedIndexChanged(object sender, EventArgs e)
    {
      string MapTypeSelected = "";

      for (int i = 0; i < CheckBoxList_MapType.Items.Count; i++)
      {
        if (CheckBoxList_MapType.Items[i].Selected)
        {
          MapTypeSelected += CheckBoxList_MapType.Items[i].Text + ",";
        }
      }

      if (MapTypeSelected.EndsWith(",", StringComparison.CurrentCulture))
      {
        MapTypeSelected = MapTypeSelected.Substring(0, MapTypeSelected.LastIndexOf(",", StringComparison.CurrentCulture));
      }

      TextBox_MapType.Text = Convert.ToString(MapTypeSelected, CultureInfo.CurrentCulture);

      if (GetPostBackControlName() == "CheckBoxList_MapType")
      {
        MapTypeValue MapTypeValue_Current = GetMapTypeValue();

        string DropDownListCountryValue = DropDownList_Country.SelectedValue;
        DropDownList_Country.Items.Clear();
        DropDownList_Country.Items.Insert(0, new ListItem(Convert.ToString("Select Country", CultureInfo.CurrentCulture), ""));
        DropDownList_Country.DataBind();
        DropDownList_Country.SelectedValue = DropDownListCountryValue;

        string DropDownListProvinceValue = DropDownList_Province.SelectedValue;
        DropDownList_Province.Items.Clear();
        ObjectDataSource_Province.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : (MapTypeValue_Current.PopulationAvailable >= 0) ? DropDownList_Country.SelectedValue : (MapTypeValue_Current.MedicalSchemeAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
        DropDownList_Province.Items.Insert(0, new ListItem(Convert.ToString("Select Province", CultureInfo.CurrentCulture), ""));
        DropDownList_Province.DataBind();
        DropDownList_Province.SelectedValue = (DropDownList_Province.Items.FindByValue(DropDownListProvinceValue) != null) ? DropDownListProvinceValue : "";

        string DropDownListHospitalSubRegionValue = DropDownList_Hospital_SubRegion.SelectedValue;
        DropDownList_Hospital_SubRegion.Items.Clear();
        ObjectDataSource_Hospital_SubRegion.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
        ObjectDataSource_Hospital_SubRegion.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
        DropDownList_Hospital_SubRegion.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Region", CultureInfo.CurrentCulture), ""));
        DropDownList_Hospital_SubRegion.DataBind();
        DropDownList_Hospital_SubRegion.SelectedValue = (DropDownList_Hospital_SubRegion.Items.FindByValue(DropDownListHospitalSubRegionValue) != null) ? DropDownListHospitalSubRegionValue : "";

        string DropDownListHospitalTownValue = DropDownList_Hospital_Town.SelectedValue;
        DropDownList_Hospital_Town.Items.Clear();
        ObjectDataSource_Hospital_Town.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
        ObjectDataSource_Hospital_Town.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
        ObjectDataSource_Hospital_Town.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
        DropDownList_Hospital_Town.Items.Insert(0, new ListItem(Convert.ToString("Select Town", CultureInfo.CurrentCulture), ""));
        DropDownList_Hospital_Town.DataBind();
        DropDownList_Hospital_Town.SelectedValue = (DropDownList_Hospital_Town.Items.FindByValue(DropDownListHospitalTownValue) != null) ? DropDownListHospitalTownValue : "";

        string TextBoxHospitalOrganisationValue = TextBox_Hospital_Organisation.Text;
        CheckBoxList_Hospital_Organisation.Items.Clear();
        ObjectDataSource_Hospital_Organisation.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
        ObjectDataSource_Hospital_Organisation.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
        ObjectDataSource_Hospital_Organisation.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
        ObjectDataSource_Hospital_Organisation.SelectParameters["Town"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Town.SelectedValue : "";
        CheckBoxList_Hospital_Organisation.DataBind();
        TextBox_Hospital_Organisation.Text = (MapTypeValue_Current.HospitalAvailable >= 0) ? TextBoxHospitalOrganisationValue : "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Select All Hospital", "<script language='javascript'>CheckAllHospitalOrganisation();</script>", false);

        string DropDownListHospitalTypeValue = DropDownList_Hospital_Type.SelectedValue;
        DropDownList_Hospital_Type.Items.Clear();
        ObjectDataSource_Hospital_Type.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
        ObjectDataSource_Hospital_Type.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
        ObjectDataSource_Hospital_Type.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
        ObjectDataSource_Hospital_Type.SelectParameters["Town"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Town.SelectedValue : "";
        ObjectDataSource_Hospital_Type.SelectParameters["Organisation"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? TextBox_Hospital_Organisation.Text : "";
        DropDownList_Hospital_Type.Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
        DropDownList_Hospital_Type.DataBind();
        DropDownList_Hospital_Type.SelectedValue = (DropDownList_Hospital_Type.Items.FindByValue(DropDownListHospitalTypeValue) != null) ? DropDownListHospitalTypeValue : "";

        string DropDownListHospitalNamedropDownValue = DropDownList_Hospital_NamedropDown.SelectedValue;
        DropDownList_Hospital_NamedropDown.Items.Clear();
        ObjectDataSource_Hospital_NamedropDown.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
        ObjectDataSource_Hospital_NamedropDown.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
        ObjectDataSource_Hospital_NamedropDown.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
        ObjectDataSource_Hospital_NamedropDown.SelectParameters["Town"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Town.SelectedValue : "";
        ObjectDataSource_Hospital_NamedropDown.SelectParameters["Organisation"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? TextBox_Hospital_Organisation.Text : "";
        ObjectDataSource_Hospital_NamedropDown.SelectParameters["Type"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Type.SelectedValue : "";
        DropDownList_Hospital_NamedropDown.Items.Insert(0, new ListItem(Convert.ToString("Select Hospital", CultureInfo.CurrentCulture), ""));
        DropDownList_Hospital_NamedropDown.DataBind();
        DropDownList_Hospital_NamedropDown.SelectedValue = (DropDownList_Hospital_NamedropDown.Items.FindByValue(DropDownListHospitalNamedropDownValue) != null) ? DropDownListHospitalNamedropDownValue : "";

        string TextBoxHospitalNameTextBoxValue = TextBox_Hospital_NameTextBox.Text;
        TextBox_Hospital_NameTextBox.Text = (MapTypeValue_Current.HospitalAvailable >= 0) ? TextBoxHospitalNameTextBoxValue : "";

        string DropDownListPopulationMunicipalityValue = DropDownList_Population_Municipality.SelectedValue;
        DropDownList_Population_Municipality.Items.Clear();
        ObjectDataSource_Population_Municipality.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.PopulationAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
        ObjectDataSource_Population_Municipality.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.PopulationAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
        DropDownList_Population_Municipality.Items.Insert(0, new ListItem(Convert.ToString("Select Municipality", CultureInfo.CurrentCulture), ""));
        DropDownList_Population_Municipality.DataBind();
        DropDownList_Population_Municipality.SelectedValue = (DropDownList_Population_Municipality.Items.FindByValue(DropDownListPopulationMunicipalityValue) != null) ? DropDownListPopulationMunicipalityValue : "";
      }
    }

    protected void CheckBoxList_MapType_DataBound(object sender, EventArgs e)
    {
      string MapTypeValue = TextBox_MapType.Text;
      List<string> MapTypeValueList = new List<string>();
      MapTypeValueList = MapTypeValue.Split(',').ToList();
      Int32 MapTypeSelectedCount = 0;
      for (int a = 0; a < MapTypeValueList.Count; a++)
      {
        for (int b = 0; b < CheckBoxList_MapType.Items.Count; b++)
        {
          if (CheckBoxList_MapType.Items[b].Text == MapTypeValueList[a])
          {
            MapTypeSelectedCount = MapTypeSelectedCount + 1;
            CheckBoxList_MapType.Items[b].Selected = true;
          }
        }
      }

      if (CheckBoxList_MapType.Items.Count == MapTypeSelectedCount && CheckBoxList_MapType.Items.Count > 0)
      {
        CheckBox_MapType_CheckAll.Checked = true;
      }
    }

    protected void DropDownList_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
      MapTypeValue MapTypeValue_Current = GetMapTypeValue();

      DropDownList_Province.Items.Clear();
      ObjectDataSource_Province.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : (MapTypeValue_Current.PopulationAvailable >= 0) ? DropDownList_Country.SelectedValue : (MapTypeValue_Current.MedicalSchemeAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      DropDownList_Province.Items.Insert(0, new ListItem(Convert.ToString("Select Province", CultureInfo.CurrentCulture), ""));
      DropDownList_Province.DataBind();

      DropDownList_Hospital_SubRegion.Items.Clear();
      ObjectDataSource_Hospital_SubRegion.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_SubRegion.SelectParameters["Province"].DefaultValue = "";
      DropDownList_Hospital_SubRegion.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Region", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_SubRegion.DataBind();

      DropDownList_Hospital_Town.Items.Clear();
      ObjectDataSource_Hospital_Town.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Town.SelectParameters["Province"].DefaultValue = "";
      ObjectDataSource_Hospital_Town.SelectParameters["SubRegion"].DefaultValue = "";
      DropDownList_Hospital_Town.Items.Insert(0, new ListItem(Convert.ToString("Select Town", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_Town.DataBind();

      CheckBoxList_Hospital_Organisation.Items.Clear();
      ObjectDataSource_Hospital_Organisation.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["Province"].DefaultValue = "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["SubRegion"].DefaultValue = "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["Town"].DefaultValue = "";
      CheckBoxList_Hospital_Organisation.DataBind();

      DropDownList_Hospital_Type.Items.Clear();
      ObjectDataSource_Hospital_Type.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Province"].DefaultValue = "";
      ObjectDataSource_Hospital_Type.SelectParameters["SubRegion"].DefaultValue = "";
      ObjectDataSource_Hospital_Type.SelectParameters["Town"].DefaultValue = "";
      ObjectDataSource_Hospital_Type.SelectParameters["Organisation"].DefaultValue = "";
      DropDownList_Hospital_Type.Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_Type.DataBind();

      DropDownList_Hospital_NamedropDown.Items.Clear();
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Province"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["SubRegion"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Town"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Organisation"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Type"].DefaultValue = "";
      DropDownList_Hospital_NamedropDown.Items.Insert(0, new ListItem(Convert.ToString("Select Hospital", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_NamedropDown.DataBind();

      DropDownList_Population_Municipality.Items.Clear();
      ObjectDataSource_Population_Municipality.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.PopulationAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Population_Municipality.SelectParameters["Province"].DefaultValue = "";
      DropDownList_Population_Municipality.Items.Insert(0, new ListItem(Convert.ToString("Select Municipality", CultureInfo.CurrentCulture), ""));
      DropDownList_Population_Municipality.DataBind();
    }

    protected void DropDownList_Province_SelectedIndexChanged(object sender, EventArgs e)
    {
      MapTypeValue MapTypeValue_Current = GetMapTypeValue();

      DropDownList_Hospital_SubRegion.Items.Clear();
      ObjectDataSource_Hospital_SubRegion.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_SubRegion.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      DropDownList_Hospital_SubRegion.Items.Insert(0, new ListItem(Convert.ToString("Select Sub Region", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_SubRegion.DataBind();

      DropDownList_Hospital_Town.Items.Clear();
      ObjectDataSource_Hospital_Town.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Town.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_Town.SelectParameters["SubRegion"].DefaultValue = "";
      DropDownList_Hospital_Town.Items.Insert(0, new ListItem(Convert.ToString("Select Town", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_Town.DataBind();

      CheckBoxList_Hospital_Organisation.Items.Clear();
      ObjectDataSource_Hospital_Organisation.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["SubRegion"].DefaultValue = "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["Town"].DefaultValue = "";
      CheckBoxList_Hospital_Organisation.DataBind();

      DropDownList_Hospital_Type.Items.Clear();
      ObjectDataSource_Hospital_Type.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["SubRegion"].DefaultValue = "";
      ObjectDataSource_Hospital_Type.SelectParameters["Town"].DefaultValue = "";
      ObjectDataSource_Hospital_Type.SelectParameters["Organisation"].DefaultValue = "";
      DropDownList_Hospital_Type.Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_Type.DataBind();

      DropDownList_Hospital_NamedropDown.Items.Clear();
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["SubRegion"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Town"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Organisation"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Type"].DefaultValue = "";
      DropDownList_Hospital_NamedropDown.Items.Insert(0, new ListItem(Convert.ToString("Select Hospital", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_NamedropDown.DataBind();

      DropDownList_Population_Municipality.Items.Clear();
      ObjectDataSource_Population_Municipality.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.PopulationAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Population_Municipality.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.PopulationAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      DropDownList_Population_Municipality.Items.Insert(0, new ListItem(Convert.ToString("Select Municipality", CultureInfo.CurrentCulture), ""));
      DropDownList_Population_Municipality.DataBind();
    }

    protected void DropDownList_Hospital_SubRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
      MapTypeValue MapTypeValue_Current = GetMapTypeValue();

      DropDownList_Hospital_Town.Items.Clear();
      ObjectDataSource_Hospital_Town.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Town.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_Town.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      DropDownList_Hospital_Town.Items.Insert(0, new ListItem(Convert.ToString("Select Town", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_Town.DataBind();

      CheckBoxList_Hospital_Organisation.Items.Clear();
      ObjectDataSource_Hospital_Organisation.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["Town"].DefaultValue = "";
      CheckBoxList_Hospital_Organisation.DataBind();

      DropDownList_Hospital_Type.Items.Clear();
      ObjectDataSource_Hospital_Type.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Town"].DefaultValue = "";
      ObjectDataSource_Hospital_Type.SelectParameters["Organisation"].DefaultValue = "";
      DropDownList_Hospital_Type.Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_Type.DataBind();

      DropDownList_Hospital_NamedropDown.Items.Clear();
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Town"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Organisation"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Type"].DefaultValue = "";
      DropDownList_Hospital_NamedropDown.Items.Insert(0, new ListItem(Convert.ToString("Select Hospital", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_NamedropDown.DataBind();
    }

    protected void DropDownList_Hospital_Town_SelectedIndexChanged(object sender, EventArgs e)
    {
      MapTypeValue MapTypeValue_Current = GetMapTypeValue();

      CheckBoxList_Hospital_Organisation.Items.Clear();
      ObjectDataSource_Hospital_Organisation.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      ObjectDataSource_Hospital_Organisation.SelectParameters["Town"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Town.SelectedValue : "";
      CheckBoxList_Hospital_Organisation.DataBind();

      DropDownList_Hospital_Type.Items.Clear();
      ObjectDataSource_Hospital_Type.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Town"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Town.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Organisation"].DefaultValue = "";
      DropDownList_Hospital_Type.Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_Type.DataBind();

      DropDownList_Hospital_NamedropDown.Items.Clear();
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Town"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Town.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Organisation"].DefaultValue = "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Type"].DefaultValue = "";
      DropDownList_Hospital_NamedropDown.Items.Insert(0, new ListItem(Convert.ToString("Select Hospital", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_NamedropDown.DataBind();
    }

    protected void CheckBoxList_Hospital_Organisation_SelectedIndexChanged(object sender, EventArgs e)
    {
      string OrganisationSelected = "";

      for (int i = 0; i < CheckBoxList_Hospital_Organisation.Items.Count; i++)
      {
        if (CheckBoxList_Hospital_Organisation.Items[i].Selected)
        {
          OrganisationSelected += CheckBoxList_Hospital_Organisation.Items[i].Text + ",";
        }
      }

      if (OrganisationSelected.EndsWith(",", StringComparison.CurrentCulture))
      {
        OrganisationSelected = OrganisationSelected.Substring(0, OrganisationSelected.LastIndexOf(",", StringComparison.CurrentCulture));
      }

      TextBox_Hospital_Organisation.Text = Convert.ToString(OrganisationSelected, CultureInfo.CurrentCulture);

      MapTypeValue MapTypeValue_Current = GetMapTypeValue();

      DropDownList_Hospital_Type.Items.Clear();
      ObjectDataSource_Hospital_Type.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Town"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Town.SelectedValue : "";
      ObjectDataSource_Hospital_Type.SelectParameters["Organisation"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? TextBox_Hospital_Organisation.Text : "";
      DropDownList_Hospital_Type.Items.Insert(0, new ListItem(Convert.ToString("Select Type", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_Type.DataBind();

      DropDownList_Hospital_NamedropDown.Items.Clear();
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Town"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Town.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Organisation"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? TextBox_Hospital_Organisation.Text : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Type"].DefaultValue = "";
      DropDownList_Hospital_NamedropDown.Items.Insert(0, new ListItem(Convert.ToString("Select Hospital", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_NamedropDown.DataBind();
    }

    protected void CheckBoxList_Hospital_Organisation_DataBound(object sender, EventArgs e)
    {
      string HospitalOrganisationValue = TextBox_Hospital_Organisation.Text;
      List<string> HospitalOrganisationValueList = new List<string>();
      HospitalOrganisationValueList = HospitalOrganisationValue.Split(',').ToList();
      Int32 HospitalOrganisationSelectedCount = 0;
      for (int a = 0; a < HospitalOrganisationValueList.Count; a++)
      {
        for (int b = 0; b < CheckBoxList_Hospital_Organisation.Items.Count; b++)
        {
          if (CheckBoxList_Hospital_Organisation.Items[b].Text == HospitalOrganisationValueList[a])
          {
            HospitalOrganisationSelectedCount = HospitalOrganisationSelectedCount + 1;
            CheckBoxList_Hospital_Organisation.Items[b].Selected = true;
          }
        }
      }

      if (CheckBoxList_Hospital_Organisation.Items.Count == HospitalOrganisationSelectedCount && CheckBoxList_Hospital_Organisation.Items.Count > 0)
      {
        CheckBox_Hospital_Organisation_CheckAll.Checked = true;
      }
    }

    protected void DropDownList_Hospital_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
      MapTypeValue MapTypeValue_Current = GetMapTypeValue();

      DropDownList_Hospital_NamedropDown.Items.Clear();
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Country"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Country.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Province"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Province.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["SubRegion"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_SubRegion.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Town"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Town.SelectedValue : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Organisation"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? TextBox_Hospital_Organisation.Text : "";
      ObjectDataSource_Hospital_NamedropDown.SelectParameters["Type"].DefaultValue = (MapTypeValue_Current.HospitalAvailable >= 0) ? DropDownList_Hospital_Type.SelectedValue : "";
      DropDownList_Hospital_NamedropDown.Items.Insert(0, new ListItem(Convert.ToString("Select Hospital", CultureInfo.CurrentCulture), ""));
      DropDownList_Hospital_NamedropDown.DataBind();
    }

    protected void Button_Search_OnClick(object sender, EventArgs e)
    {
      string SearchField1 = Server.HtmlEncode(TextBox_MapType.Text);
      string SearchField2 = DropDownList_Country.SelectedValue;
      string SearchField3 = DropDownList_Province.SelectedValue;
      string SearchField4 = DropDownList_Hospital_SubRegion.SelectedValue;
      string SearchField5 = DropDownList_Hospital_Town.SelectedValue;
      string SearchField6 = Server.HtmlEncode(TextBox_Hospital_Organisation.Text);
      string SearchField7 = DropDownList_Hospital_Type.SelectedValue;
      string SearchField8 = DropDownList_Hospital_NamedropDown.SelectedValue;
      string SearchField9 = Server.HtmlEncode(TextBox_Hospital_NameTextBox.Text);
      string SearchField10 = DropDownList_Population_Municipality.SelectedValue;

      if (string.IsNullOrEmpty(SearchField1))
      {
        Label_InvalidSearchMessage.Text = Convert.ToString("Maps and Province is required", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_InvalidSearchMessage.Text = "";
        if (!string.IsNullOrEmpty(SearchField1) || string.IsNullOrEmpty(SearchField1))
        {
          SearchField1 = "s_MapType=" + Server.HtmlEncode(TextBox_MapType.Text).ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField2))
        {
          SearchField2 = "s_Country=" + DropDownList_Country.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField3))
        {
          SearchField3 = "s_Province=" + DropDownList_Province.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField4))
        {
          SearchField4 = "s_HSubRegion=" + DropDownList_Hospital_SubRegion.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField5))
        {
          SearchField5 = "s_HTown=" + DropDownList_Hospital_Town.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField6))
        {
          SearchField6 = "s_HOrganisation=" + Server.HtmlEncode(TextBox_Hospital_Organisation.Text).ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField7))
        {
          SearchField7 = "s_HType=" + DropDownList_Hospital_Type.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField8))
        {
          SearchField8 = "s_HNameDropDown=" + DropDownList_Hospital_NamedropDown.SelectedValue.ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField9))
        {
          SearchField9 = "s_HNameTextBox=" + Server.HtmlEncode(TextBox_Hospital_NameTextBox.Text).ToString() + "&";
        }

        if (!string.IsNullOrEmpty(SearchField10))
        {
          SearchField10 = "s_PMunicipality=" + DropDownList_Population_Municipality.SelectedValue.ToString() + "&";
        }

        string FinalURL = "Form_ExecutiveMarketInquiry_Map_Hospital.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5 + SearchField6 + SearchField7 + SearchField8 + SearchField9 + SearchField10;
        FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
        FinalURL = FinalURL + "#MapSearch";
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Executive Market Inquiry - Map", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }

    protected void Button_Clear_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Executive Market Inquiry Map", "Form_ExecutiveMarketInquiry_Map_Hospital.aspx"), false);
    }
    //---END--- --Search--//


    //--START-- --Hospital List--//
    protected void ObjectDataSource_ExecutiveMarketInquiry_Hospital_List_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalHospital_List.Text = ((DataTable)e.ReturnValue).Rows.Count.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_Hospital_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_ExecutiveMarketInquiry_Hospital_List.PageSize = Convert.ToInt32(((DropDownList)GridView_ExecutiveMarketInquiry_Hospital_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Hospital")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_Hospital_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_ExecutiveMarketInquiry_Hospital_List.PageIndex = ((DropDownList)GridView_ExecutiveMarketInquiry_Hospital_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_Hospital")).SelectedIndex;
    }

    protected void GridView_ExecutiveMarketInquiry_Hospital_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_ExecutiveMarketInquiry_Hospital_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Hospital");
        if (GridView_ExecutiveMarketInquiry_Hospital_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_ExecutiveMarketInquiry_Hospital_List.PageSize > 20 && GridView_ExecutiveMarketInquiry_Hospital_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_ExecutiveMarketInquiry_Hospital_List.PageSize > 50 && GridView_ExecutiveMarketInquiry_Hospital_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_ExecutiveMarketInquiry_Hospital_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_ExecutiveMarketInquiry_Hospital_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_Hospital");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_ExecutiveMarketInquiry_Hospital_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_ExecutiveMarketInquiry_Hospital_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_ExecutiveMarketInquiry_Hospital_List_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --Hospital List--//


    //--START-- --Population List--//
    private double PopulationTotal = 0;

    protected void ObjectDataSource_ExecutiveMarketInquiry_Population_List_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalPopulation_List.Text = ((DataTable)e.ReturnValue).Rows.Count.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_Population_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_ExecutiveMarketInquiry_Population_List.PageSize = Convert.ToInt32(((DropDownList)GridView_ExecutiveMarketInquiry_Population_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Population")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_Population_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_ExecutiveMarketInquiry_Population_List.PageIndex = ((DropDownList)GridView_ExecutiveMarketInquiry_Population_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_Population")).SelectedIndex;
    }

    protected void GridView_ExecutiveMarketInquiry_Population_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_ExecutiveMarketInquiry_Population_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_Population");
        if (GridView_ExecutiveMarketInquiry_Population_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_ExecutiveMarketInquiry_Population_List.PageSize > 20 && GridView_ExecutiveMarketInquiry_Population_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_ExecutiveMarketInquiry_Population_List.PageSize > 50 && GridView_ExecutiveMarketInquiry_Population_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }


      GridView_ExecutiveMarketInquiry_Population_List_Count();
    }

    protected void GridView_ExecutiveMarketInquiry_Population_List_Count()
    {
      string PopulationLevel1 = "No";
      string PopulationLevel2 = "No";
      string PopulationLevel3 = "No";
      for (int i = 0; i < GridView_ExecutiveMarketInquiry_Population_List.Rows.Count; i++)
      {
        if (GridView_ExecutiveMarketInquiry_Population_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          Label Label_Population_Value = (Label)GridView_ExecutiveMarketInquiry_Population_List.Rows[i].FindControl("Label_Population_Value");
          HiddenField HiddenField_Population_Level = (HiddenField)GridView_ExecutiveMarketInquiry_Population_List.Rows[i].FindControl("HiddenField_Population_Level");
          if (HiddenField_Population_Level.Value == "1")
          {
            PopulationLevel1 = "Yes";
          }
          else if (HiddenField_Population_Level.Value == "2")
          {
            PopulationLevel2 = "Yes";
          }
          else if (HiddenField_Population_Level.Value == "3")
          {
            PopulationLevel3 = "Yes";
          }

          if (Label_Population_Value.Visible == true)
          {
            if (!string.IsNullOrEmpty(Label_Population_Value.Text))
            {
              if (PopulationLevel1 == "Yes" && i == 0)
              {
                PopulationTotal = PopulationTotal + Convert.ToDouble(Label_Population_Value.Text, CultureInfo.CurrentCulture);
                break;
              }

              if (PopulationLevel1 == "No" && PopulationLevel2 == "Yes" && HiddenField_Population_Level.Value == "2")
              {
                PopulationTotal = PopulationTotal + Convert.ToDouble(Label_Population_Value.Text, CultureInfo.CurrentCulture);
              }

              if (PopulationLevel1 == "No" && PopulationLevel2 == "No" && PopulationLevel3 == "Yes" && HiddenField_Population_Level.Value == "3")
              {
                PopulationTotal = PopulationTotal + Convert.ToDouble(Label_Population_Value.Text, CultureInfo.CurrentCulture);
              }
            }
          }
        }
      }

      if (GridView_ExecutiveMarketInquiry_Population_List.Rows.Count > 0)
      {
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[0].Text = Convert.ToString("Total", CultureInfo.CurrentCulture);
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[0].Font.Bold = true;

        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[1].Text = PopulationTotal.ToString("#,##0", CultureInfo.CurrentCulture);
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[1].Font.Bold = true;


        string PopulationSearch = Convert.ToString(Request.QueryString["s_Country"] + ", " + Request.QueryString["s_Province"] + ", " + Request.QueryString["s_PMunicipality"], CultureInfo.CurrentCulture);
        PopulationSearch = PopulationSearch.Replace(", , ", "");
        if (PopulationSearch.IndexOf(", ", StringComparison.CurrentCulture) == 0)
        {
          PopulationSearch = PopulationSearch.Remove(0, 2);
        }

        if (PopulationSearch.LastIndexOf(", ", StringComparison.CurrentCulture) == PopulationSearch.Length - 2)
        {
          PopulationSearch = PopulationSearch.Remove(PopulationSearch.Length - 2, 2);
        }

        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[2].ColumnSpan = 5;
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[2].Text = PopulationSearch;
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[2].Font.Bold = true;

        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[3].Visible = false;
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[4].Visible = false;
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[5].Visible = false;
        GridView_ExecutiveMarketInquiry_Population_List.FooterRow.Cells[6].Visible = false;
      }
    }

    protected void GridView_ExecutiveMarketInquiry_Population_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_ExecutiveMarketInquiry_Population_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_Population");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_ExecutiveMarketInquiry_Population_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_ExecutiveMarketInquiry_Population_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }
    //--START-- --Population List--//


    //--START-- --MedicalScheme List--//
    private double MedicalSchemeTotal = 0;

    protected void ObjectDataSource_ExecutiveMarketInquiry_MedicalScheme_List_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalMedicalScheme_List.Text = ((DataTable)e.ReturnValue).Rows.Count.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_MedicalScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageSize = Convert.ToInt32(((DropDownList)GridView_ExecutiveMarketInquiry_MedicalScheme_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_MedicalScheme")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_MedicalScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageIndex = ((DropDownList)GridView_ExecutiveMarketInquiry_MedicalScheme_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page_MedicalScheme")).SelectedIndex;
    }

    protected void GridView_ExecutiveMarketInquiry_MedicalScheme_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_ExecutiveMarketInquiry_MedicalScheme_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize_MedicalScheme");
        if (GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageSize > 20 && GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageSize > 50 && GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }


      GridView_ExecutiveMarketInquiry_MedicalScheme_List_Count();
    }

    protected void GridView_ExecutiveMarketInquiry_MedicalScheme_List_Count()
    {
      string MedicalSchemeLevel1 = "No";
      string MedicalSchemeLevel2 = "No";
      for (int i = 0; i < GridView_ExecutiveMarketInquiry_MedicalScheme_List.Rows.Count; i++)
      {
        if (GridView_ExecutiveMarketInquiry_MedicalScheme_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          Label Label_MedicalScheme_Value = (Label)GridView_ExecutiveMarketInquiry_MedicalScheme_List.Rows[i].FindControl("Label_MedicalScheme_Value");
          HiddenField HiddenField_MedicalScheme_Level = (HiddenField)GridView_ExecutiveMarketInquiry_MedicalScheme_List.Rows[i].FindControl("HiddenField_MedicalScheme_Level");
          if (HiddenField_MedicalScheme_Level.Value == "1")
          {
            MedicalSchemeLevel1 = "Yes";
          }
          else if (HiddenField_MedicalScheme_Level.Value == "2")
          {
            MedicalSchemeLevel2 = "Yes";
          }

          if (Label_MedicalScheme_Value.Visible == true)
          {
            if (!string.IsNullOrEmpty(Label_MedicalScheme_Value.Text))
            {
              if (MedicalSchemeLevel1 == "Yes" && i == 0)
              {
                MedicalSchemeTotal = MedicalSchemeTotal + Convert.ToDouble(Label_MedicalScheme_Value.Text, CultureInfo.CurrentCulture);
                break;
              }

              if (MedicalSchemeLevel1 == "No" && MedicalSchemeLevel2 == "Yes" && HiddenField_MedicalScheme_Level.Value == "2")
              {
                MedicalSchemeTotal = MedicalSchemeTotal + Convert.ToDouble(Label_MedicalScheme_Value.Text, CultureInfo.CurrentCulture);
              }
            }
          }
        }
      }

      if (GridView_ExecutiveMarketInquiry_MedicalScheme_List.Rows.Count > 0)
      {
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[0].Text = Convert.ToString("Total", CultureInfo.CurrentCulture);
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[0].Font.Bold = true;

        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[1].Text = MedicalSchemeTotal.ToString("#,##0", CultureInfo.CurrentCulture);
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[1].Font.Bold = true;


        string MedicalSchemeSearch = Convert.ToString(Request.QueryString["s_Country"] + ", " + Request.QueryString["s_Province"], CultureInfo.CurrentCulture);
        if (MedicalSchemeSearch.IndexOf(", ", StringComparison.CurrentCulture) == 0)
        {
          MedicalSchemeSearch = MedicalSchemeSearch.Remove(0, 2);
        }

        if (MedicalSchemeSearch.LastIndexOf(", ", StringComparison.CurrentCulture) == MedicalSchemeSearch.Length - 2)
        {
          MedicalSchemeSearch = MedicalSchemeSearch.Remove(MedicalSchemeSearch.Length - 2, 2);
        }

        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[2].ColumnSpan = 5;
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[2].Text = MedicalSchemeSearch;
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[2].Font.Bold = true;

        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[3].Visible = false;
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[4].Visible = false;
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[5].Visible = false;
        GridView_ExecutiveMarketInquiry_MedicalScheme_List.FooterRow.Cells[6].Visible = false;
      }
    }

    protected void GridView_ExecutiveMarketInquiry_MedicalScheme_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_ExecutiveMarketInquiry_MedicalScheme_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page_MedicalScheme");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_ExecutiveMarketInquiry_MedicalScheme_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }
    //--START-- --MedicalScheme List--//
  }
}