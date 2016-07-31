using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_AntimicrobialStewardshipIntervention : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_AntimicrobialStewardshipIntervention, this.GetType(), "UpdateProgress_Start", "Validation_Search();Validation_Form();Calculation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          SqlDataSource_ASI_Facility.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];

          PageTitle();

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_ASI_VisitInformation_VisitNumber"] != null)
          {
            form_AntimicrobialStewardshipIntervention.DefaultButton = Button_Search.UniqueID;

            SqlDataSource_ASI_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
            SqlDataSource_ASI_Facility.SelectParameters["TableFROM"].DefaultValue = "Form_AntimicrobialStewardshipIntervention_VisitInformation";
            SqlDataSource_ASI_Facility.SelectParameters["TableWHERE"].DefaultValue = "Facility_Id = " + Request.QueryString["s_Facility_Id"] + " AND ASI_VisitInformation_VisitNumber = " + Request.QueryString["s_ASI_VisitInformation_VisitNumber"] + " ";

            Label_InvalidSearchMessage.Text = "";
            TableVisitInfo.Visible = false;
            TableCurrentIntervention.Visible = false;
            TableIntervention.Visible = false;

            VisitData();
          }
          else
          {
            if (Request.QueryString["ASIVisitInformationId"] == null)
            {
              form_AntimicrobialStewardshipIntervention.DefaultButton = Button_Search.UniqueID;

              Label_InvalidSearchMessage.Text = "";
              TableVisitInfo.Visible = false;
              TableCurrentIntervention.Visible = false;
              TableIntervention.Visible = false;
            }
            else
            {
              SqlDataSource_ASI_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
              SqlDataSource_ASI_Facility.SelectParameters["TableFROM"].DefaultValue = "Form_AntimicrobialStewardshipIntervention_VisitInformation";
              SqlDataSource_ASI_Facility.SelectParameters["TableWHERE"].DefaultValue = "ASI_VisitInformation_Id = " + Request.QueryString["ASIVisitInformationId"] + " ";

              TableVisitInfo.Visible = true;
              TableCurrentIntervention.Visible = true;
              TableIntervention.Visible = true;

              SetCurrentInterventionVisibility();

              if (string.IsNullOrEmpty(Request.QueryString["ASIInterventionId"]))
              {
                PageLoad_Insert();
              }
              else
              {
                PageLoad_Edit();
              }
            }
          }

          TableVisible();
        }
      }
    }

    private void PageLoad_Insert()
    {
      form_AntimicrobialStewardshipIntervention.DefaultButton = Button_Search.UniqueID;

      if (TableCurrentIntervention.Visible == true)
      {
        if (((HiddenField)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("HiddenField_Insert")) != null)
        {
          FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
          string FacilityId = FromDataBase_FacilityId_Current.FacilityId;
          string ASIVisitInformationVisitNumber = FromDataBase_FacilityId_Current.ASIVisitInformationVisitNumber;


          ((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_InsertDate")).Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);


          DataTable DataTable_Unit;
          using (DataTable_Unit = new DataTable())
          {
            DataTable_Unit.Locale = CultureInfo.CurrentCulture;
            DataTable_Unit = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_AccommodationInformation(FacilityId, ASIVisitInformationVisitNumber);

            ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertUnit")).DataSource = DataTable_Unit.DefaultView.ToTable(true, "Ward");
            ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertUnit")).DataBind();
          }


          DataTable DataTable_Doctor;
          using (DataTable_Doctor = new DataTable())
          {
            DataTable_Doctor.Locale = CultureInfo.CurrentCulture;
            DataTable_Doctor = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(FacilityId, ASIVisitInformationVisitNumber);

            ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertDoctor")).DataSource = DataTable_Doctor;
            ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertDoctor")).DataBind();
          }


          DataTable DataTable_Antibiotic;
          using (DataTable_Antibiotic = new DataTable())
          {
            DataTable_Antibiotic.Locale = CultureInfo.CurrentCulture;
            DataTable_Antibiotic = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_AntibioticInformation(FacilityId, ASIVisitInformationVisitNumber);

            if (DataTable_Antibiotic.Columns.Count == 1)
            {
              string Error = "";
              foreach (DataRow DataRow_Row in DataTable_Antibiotic.Rows)
              {
                Error = DataRow_Row["Error"].ToString();
              }

              Error = Error + Convert.ToString("<br />Antibiotics could not be retrieved", CultureInfo.CurrentCulture);

              ToolkitScriptManager_AntimicrobialStewardshipIntervention.SetFocus(UpdatePanel_AntimicrobialStewardshipIntervention);
              ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Error;
              ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";

              ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic")).DataSource = null;
              ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic")).DataBind();
            }
            else if (DataTable_Antibiotic.Columns.Count != 1)
            {
              ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic")).DataSource = DataTable_Antibiotic;
              ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic")).DataBind();
            }
          }
        }
      }
    }

    private void PageLoad_Edit()
    {
      form_AntimicrobialStewardshipIntervention.DefaultButton = null;

      FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
      string FacilityId = FromDataBase_FacilityId_Current.FacilityId;
      string ASIVisitInformationVisitNumber = FromDataBase_FacilityId_Current.ASIVisitInformationVisitNumber;

      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters["TableSELECT"].DefaultValue = "ASI_Intervention_By_List";
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters["TableFROM"].DefaultValue = "Form_AntimicrobialStewardshipIntervention_Intervention";
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters["TableWHERE"].DefaultValue = "ASI_Intervention_Id = " + Request.QueryString["ASIInterventionId"] + " ";

      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters["TableSELECT"].DefaultValue = "ASI_Nursing5B_Item_List";
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters["TableFROM"].DefaultValue = "Form_AntimicrobialStewardshipIntervention_Nursing5B";
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters["TableWHERE"].DefaultValue = "ASI_Intervention_Id = " + Request.QueryString["ASIInterventionId"] + " ";

      if (((HiddenField)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("HiddenField_Edit")) != null)
      {
        string SQLStringUnit = "SELECT Facility_FacilityCode AS FacilityCode , ASI_VisitInformation_VisitNumber AS VisitNumber , '' AS SequenceNumber , ASI_Intervention_Unit AS Ward , '' AS Room , '' AS Bed , CAST('' AS DateTime) AS Date FROM vForm_AntimicrobialStewardshipIntervention_Intervention WHERE ASI_Intervention_Id = @ASI_Intervention_Id AND ASI_Intervention_Unit IS NOT NULL";
        using (SqlCommand SqlCommand_Unit = new SqlCommand(SQLStringUnit))
        {
          SqlCommand_Unit.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
          DataTable DataTable_Unit;
          using (DataTable_Unit = new DataTable())
          {
            DataTable_Unit.Locale = CultureInfo.CurrentCulture;

            DataTable_Unit.Merge(InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Unit));
            DataTable_Unit.Merge(InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_AccommodationInformation(FacilityId, ASIVisitInformationVisitNumber));

            DataTable_Unit.DefaultView.Sort = "Ward ASC";

            ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditUnit")).DataSource = DataTable_Unit.DefaultView.ToTable(true, "Ward");
            ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditUnit")).DataBind();


            ListItem ListItem_Pharmacy = new ListItem();
            ListItem_Pharmacy.Text = Convert.ToString("Pharmacy", CultureInfo.CurrentCulture);
            ListItem_Pharmacy.Value = Convert.ToString("Pharmacy", CultureInfo.CurrentCulture);

            Int32 IndexOfPharmacy = ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditUnit")).Items.IndexOf(ListItem_Pharmacy);

            if (IndexOfPharmacy == -1)
            {
              ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditUnit")).Items.Insert(1, ListItem_Pharmacy);
            }
          }
        }


        string SQLStringDoctor = "SELECT Facility_FacilityCode AS FacilityCode , ASI_VisitInformation_VisitNumber AS VisitNumber , ASI_Intervention_Doctor AS Practitioner FROM vForm_AntimicrobialStewardshipIntervention_Intervention WHERE ASI_Intervention_Id = @ASI_Intervention_Id AND ASI_Intervention_Doctor IS NOT NULL";
        using (SqlCommand SqlCommand_Doctor = new SqlCommand(SQLStringDoctor))
        {
          SqlCommand_Doctor.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
          DataTable DataTable_Doctor;
          using (DataTable_Doctor = new DataTable())
          {
            DataTable_Doctor.Locale = CultureInfo.CurrentCulture;
            DataTable_Doctor.Merge(InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Doctor));
            DataTable_Doctor.Merge(InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(FacilityId, ASIVisitInformationVisitNumber));

            DataTable_Doctor.DefaultView.Sort = "Practitioner ASC";

            ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditDoctor")).DataSource = DataTable_Doctor.DefaultView.ToTable(true, "Practitioner");
            ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditDoctor")).DataBind();
          }
        }

        string SQLStringAntibiotic = "SELECT vForm_AntimicrobialStewardshipIntervention_Intervention.Facility_FacilityCode AS FacilityCode , CAST(vForm_AntimicrobialStewardshipIntervention_Intervention.ASI_VisitInformation_VisitNumber AS INT) AS VisitNumber , Form_AntimicrobialStewardshipIntervention_Antibiotic.ASI_Antibiotic_Description AS Description FROM Form_AntimicrobialStewardshipIntervention_Antibiotic LEFT JOIN vForm_AntimicrobialStewardshipIntervention_Intervention ON Form_AntimicrobialStewardshipIntervention_Antibiotic.ASI_Intervention_Id = vForm_AntimicrobialStewardshipIntervention_Intervention.ASI_Intervention_Id WHERE Form_AntimicrobialStewardshipIntervention_Antibiotic.ASI_Intervention_Id = @ASI_Intervention_Id";
        using (SqlCommand SqlCommand_Antibiotic = new SqlCommand(SQLStringAntibiotic))
        {
          SqlCommand_Antibiotic.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
          DataTable DataTable_Antibiotic;
          using (DataTable_Antibiotic = new DataTable())
          {
            DataTable_Antibiotic.Locale = CultureInfo.CurrentCulture;
            DataTable_Antibiotic.Merge(InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Antibiotic));

            DataTable DataTable_EDWAntibiotic;
            using (DataTable_EDWAntibiotic = new DataTable())
            {
              DataTable_EDWAntibiotic.Locale = CultureInfo.CurrentCulture;
              DataTable_EDWAntibiotic = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_AntibioticInformation(FacilityId, ASIVisitInformationVisitNumber);

              if (DataTable_EDWAntibiotic.Columns.Count == 1)
              {
                string Error = "";
                foreach (DataRow DataRow_Row in DataTable_EDWAntibiotic.Rows)
                {
                  Error = DataRow_Row["Error"].ToString();
                }

                Error = Error + Convert.ToString("<br />Antibiotics could not be retrieved", CultureInfo.CurrentCulture);

                ToolkitScriptManager_AntimicrobialStewardshipIntervention.SetFocus(UpdatePanel_AntimicrobialStewardshipIntervention);
                ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_EditInvalidFormMessage")).Text = Error;
                ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
              }
              else if (DataTable_EDWAntibiotic.Columns.Count != 1)
              {
                DataTable_Antibiotic.Merge(DataTable_EDWAntibiotic);
              }
            }

            DataTable_Antibiotic.DefaultView.Sort = "Description ASC";

            ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_EditAntimicrobialStewardshipIntervention_Antibiotic")).DataSource = DataTable_Antibiotic.DefaultView.ToTable(true, "Description");
            ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_EditAntimicrobialStewardshipIntervention_Antibiotic")).DataBind();
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
        if (Request.QueryString["s_Facility_Id"] == null && Request.QueryString["IPSVisitInformationId"] == null)
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('39'))";
        }
        else
        {
          if (Request.QueryString["s_Facility_Id"] != null)
          {
            SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('39')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
          }

          if (Request.QueryString["IPSVisitInformationId"] != null)
          {
            SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('39')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_AntimicrobialStewardshipIntervention_VisitInformation WHERE ASI_VisitInformation_Id = @ASI_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
          }
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_Security.Parameters.AddWithValue("@ASI_VisitInformation_Id", Request.QueryString["ASIVisitInformationId"]);

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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("39");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_AntimicrobialStewardshipIntervention.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Antimicrobial Stewardship", "13");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_ASI_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_ASI_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_ASI_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_ASI_Facility.SelectParameters.Clear();
      SqlDataSource_ASI_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_ASI_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "39");
      SqlDataSource_ASI_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_ASI_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_ASI_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_ASI_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.SelectParameters.Add("Form_Id", TypeCode.String, "39");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "172");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertByList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.SelectParameters.Add("Form_Id", TypeCode.String, "39");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "179");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_AntimicrobialStewardshipIntervention_InsertNursing5BReason.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters.Add("Form_Id", TypeCode.String, "39");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "172");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditByList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters.Add("Form_Id", TypeCode.String, "39");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "179");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_AntimicrobialStewardshipIntervention_EditNursing5BReason.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_AntimicrobialStewardshipIntervention_ItemNursing5BReason.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_ItemNursing5BReason.SelectCommand = "SELECT ListItem_Name FROM Form_AntimicrobialStewardshipIntervention_Nursing5B LEFT JOIN Administration_ListItem ON ASI_Nursing5B_Item_List = ListItem_Id WHERE ASI_Intervention_Id = @ASI_Intervention_Id";
      SqlDataSource_AntimicrobialStewardshipIntervention_ItemNursing5BReason.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_ItemNursing5BReason.SelectParameters.Add("ASI_Intervention_Id", TypeCode.String, Request.QueryString["ASIInterventionId"]);

      SqlDataSource_AntimicrobialStewardshipIntervention_ItemAntibiotic.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_ItemAntibiotic.SelectCommand = "SELECT ASI_Antibiotic_Description , ASI_Antibiotic_Information FROM Form_AntimicrobialStewardshipIntervention_Antibiotic WHERE ASI_Intervention_Id = @ASI_Intervention_Id ORDER BY ASI_Antibiotic_Description";
      SqlDataSource_AntimicrobialStewardshipIntervention_ItemAntibiotic.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_ItemAntibiotic.SelectParameters.Add("ASI_Intervention_Id", TypeCode.String, Request.QueryString["ASIInterventionId"]);

      SqlDataSource_AntimicrobialStewardshipIntervention_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertCommand = "INSERT INTO Form_AntimicrobialStewardshipIntervention_Intervention ( ASI_VisitInformation_Id , ASI_Intervention_ReportNumber , ASI_Intervention_Date , ASI_Intervention_By_List , ASI_Intervention_Unit , ASI_Intervention_Doctor , ASI_Intervention_Nursing_1 , ASI_Intervention_Nursing_1_B , ASI_Intervention_Nursing_2 , ASI_Intervention_Nursing_2_B , ASI_Intervention_Nursing_3 , ASI_Intervention_Nursing_3_B , ASI_Intervention_Nursing_4 , ASI_Intervention_Nursing_5 , ASI_Intervention_Nursing_5_B , ASI_Intervention_Nursing_6 , ASI_Intervention_Nursing_6_B , ASI_Intervention_Nursing_Score , ASI_Intervention_Pharmacy_1 , ASI_Intervention_Pharmacy_1_B , ASI_Intervention_Pharmacy_2 , ASI_Intervention_Pharmacy_2_B , ASI_Intervention_Pharmacy_3 , ASI_Intervention_Pharmacy_3_B , ASI_Intervention_Pharmacy_4 , ASI_Intervention_Pharmacy_4_B , ASI_Intervention_Pharmacy_5 , ASI_Intervention_Pharmacy_5_B , ASI_Intervention_Pharmacy_6 , ASI_Intervention_Pharmacy_6_B , ASI_Intervention_Pharmacy_7 , ASI_Intervention_Pharmacy_7_B , ASI_Intervention_Pharmacy_8 , ASI_Intervention_Pharmacy_Score , ASI_Intervention_CreatedDate , ASI_Intervention_CreatedBy , ASI_Intervention_ModifiedDate , ASI_Intervention_ModifiedBy , ASI_Intervention_History , ASI_Intervention_IsActive ) VALUES ( @ASI_VisitInformation_Id , @ASI_Intervention_ReportNumber , @ASI_Intervention_Date , @ASI_Intervention_By_List , @ASI_Intervention_Unit , @ASI_Intervention_Doctor , @ASI_Intervention_Nursing_1 , @ASI_Intervention_Nursing_1_B , @ASI_Intervention_Nursing_2 , @ASI_Intervention_Nursing_2_B , @ASI_Intervention_Nursing_3 , @ASI_Intervention_Nursing_3_B , @ASI_Intervention_Nursing_4 , @ASI_Intervention_Nursing_5 , @ASI_Intervention_Nursing_5_B , @ASI_Intervention_Nursing_6 , @ASI_Intervention_Nursing_6_B , @ASI_Intervention_Nursing_Score , @ASI_Intervention_Pharmacy_1 , @ASI_Intervention_Pharmacy_1_B , @ASI_Intervention_Pharmacy_2 , @ASI_Intervention_Pharmacy_2_B , @ASI_Intervention_Pharmacy_3 , @ASI_Intervention_Pharmacy_3_B , @ASI_Intervention_Pharmacy_4 , @ASI_Intervention_Pharmacy_4_B , @ASI_Intervention_Pharmacy_5 , @ASI_Intervention_Pharmacy_5_B , @ASI_Intervention_Pharmacy_6 , @ASI_Intervention_Pharmacy_6_B , @ASI_Intervention_Pharmacy_7 , @ASI_Intervention_Pharmacy_7_B , @ASI_Intervention_Pharmacy_8 , @ASI_Intervention_Pharmacy_Score , @ASI_Intervention_CreatedDate , @ASI_Intervention_CreatedBy , @ASI_Intervention_ModifiedDate , @ASI_Intervention_ModifiedBy , @ASI_Intervention_History , @ASI_Intervention_IsActive ); SELECT @ASI_Intervention_Id = SCOPE_IDENTITY()";
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.SelectCommand = "SELECT * FROM Form_AntimicrobialStewardshipIntervention_Intervention WHERE (ASI_Intervention_Id = @ASI_Intervention_Id)";
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateCommand = "UPDATE Form_AntimicrobialStewardshipIntervention_Intervention SET ASI_Intervention_Date = @ASI_Intervention_Date , ASI_Intervention_By_List = @ASI_Intervention_By_List , ASI_Intervention_Unit = @ASI_Intervention_Unit , ASI_Intervention_Doctor = @ASI_Intervention_Doctor , ASI_Intervention_Nursing_1 = @ASI_Intervention_Nursing_1 , ASI_Intervention_Nursing_1_B = @ASI_Intervention_Nursing_1_B , ASI_Intervention_Nursing_2 = @ASI_Intervention_Nursing_2 , ASI_Intervention_Nursing_2_B = @ASI_Intervention_Nursing_2_B , ASI_Intervention_Nursing_3 = @ASI_Intervention_Nursing_3 , ASI_Intervention_Nursing_3_B = @ASI_Intervention_Nursing_3_B , ASI_Intervention_Nursing_4 = @ASI_Intervention_Nursing_4 , ASI_Intervention_Nursing_5 = @ASI_Intervention_Nursing_5 , ASI_Intervention_Nursing_5_B = @ASI_Intervention_Nursing_5_B , ASI_Intervention_Nursing_6 = @ASI_Intervention_Nursing_6 , ASI_Intervention_Nursing_6_B = @ASI_Intervention_Nursing_6_B , ASI_Intervention_Nursing_Score = @ASI_Intervention_Nursing_Score , ASI_Intervention_Pharmacy_1 = @ASI_Intervention_Pharmacy_1 , ASI_Intervention_Pharmacy_1_B = @ASI_Intervention_Pharmacy_1_B , ASI_Intervention_Pharmacy_2 = @ASI_Intervention_Pharmacy_2 , ASI_Intervention_Pharmacy_2_B = @ASI_Intervention_Pharmacy_2_B , ASI_Intervention_Pharmacy_3 = @ASI_Intervention_Pharmacy_3 , ASI_Intervention_Pharmacy_3_B = @ASI_Intervention_Pharmacy_3_B , ASI_Intervention_Pharmacy_4 = @ASI_Intervention_Pharmacy_4 , ASI_Intervention_Pharmacy_4_B = @ASI_Intervention_Pharmacy_4_B , ASI_Intervention_Pharmacy_5 = @ASI_Intervention_Pharmacy_5 , ASI_Intervention_Pharmacy_5_B = @ASI_Intervention_Pharmacy_5_B , ASI_Intervention_Pharmacy_6 = @ASI_Intervention_Pharmacy_6 , ASI_Intervention_Pharmacy_6_B = @ASI_Intervention_Pharmacy_6_B , ASI_Intervention_Pharmacy_7 = @ASI_Intervention_Pharmacy_7 , ASI_Intervention_Pharmacy_7_B = @ASI_Intervention_Pharmacy_7_B , ASI_Intervention_Pharmacy_8 = @ASI_Intervention_Pharmacy_8 , ASI_Intervention_Pharmacy_Score = @ASI_Intervention_Pharmacy_Score , ASI_Intervention_ModifiedDate = @ASI_Intervention_ModifiedDate , ASI_Intervention_ModifiedBy = @ASI_Intervention_ModifiedBy , ASI_Intervention_History = @ASI_Intervention_History , ASI_Intervention_IsActive = @ASI_Intervention_IsActive WHERE ASI_Intervention_Id = @ASI_Intervention_Id";
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Id", TypeCode.Int32, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_VisitInformation_Id", TypeCode.Int32, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_ReportNumber", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Date", TypeCode.DateTime, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_By_List", TypeCode.Int32, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Unit", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Doctor", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_1", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_1_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_2", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_2_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_3", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_3_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_4", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_5", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_5_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_6", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_6_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Nursing_Score", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_1", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_1_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_2", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_2_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_3", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_3_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_4", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_4_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_5", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_5_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_6", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_6_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_7", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_7_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_8", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_Pharmacy_Score", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_CreatedBy", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_ModifiedBy", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_History", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters.Add("ASI_Intervention_IsActive", TypeCode.Boolean, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.SelectParameters.Add("ASI_Intervention_Id", TypeCode.Int32, Request.QueryString["ASIInterventionId"]);
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Date", TypeCode.DateTime, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_By_List", TypeCode.Int32, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Unit", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Doctor", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_1", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_1_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_2", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_2_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_3", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_3_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_4", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_5", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_5_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_6", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_6_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Nursing_Score", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_1", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_1_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_2", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_2_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_3", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_3_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_4", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_4_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_5", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_5_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_6", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_6_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_7", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_7_B", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_8", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Pharmacy_Score", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_ModifiedBy", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_History", TypeCode.String, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_IsActive", TypeCode.Boolean, "");
      SqlDataSource_AntimicrobialStewardshipIntervention_Form.UpdateParameters.Add("ASI_Intervention_Id", TypeCode.Int32, "");

      SqlDataSource_AntimicrobialStewardshipIntervention_Intervention.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AntimicrobialStewardshipIntervention_Intervention.SelectCommand = "spForm_Get_AntimicrobialStewardshipIntervention_Intervention";
      SqlDataSource_AntimicrobialStewardshipIntervention_Intervention.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AntimicrobialStewardshipIntervention_Intervention.CancelSelectOnNullParameter = false;
      SqlDataSource_AntimicrobialStewardshipIntervention_Intervention.SelectParameters.Clear();
      SqlDataSource_AntimicrobialStewardshipIntervention_Intervention.SelectParameters.Add("ASI_VisitInformation_Id", TypeCode.String, Request.QueryString["ASIVisitInformationId"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("39")).ToString(), CultureInfo.CurrentCulture);
      Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("39").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
      Label_VisitInfoHeading.Text = Convert.ToString("Visit Information", CultureInfo.CurrentCulture);
      Label_CurrentInterventionHeading.Text = Convert.ToString("Assessment", CultureInfo.CurrentCulture);
      Label_InterventionHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("39").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
    }

    private void SetFormQueryString()
    {
      if (Request.QueryString["s_Facility_Id"] == null && Request.QueryString["s_ASI_VisitInformation_VisitNumber"] == null && Request.QueryString["ASIVisitInformationId"] == null)
      {
        DropDownList_Facility.SelectedValue = "";
        TextBox_PatientVisitNumber.Text = "";
      }
      else
      {
        if (Request.QueryString["ASIVisitInformationId"] == null)
        {
          DropDownList_Facility.SelectedValue = Request.QueryString["s_Facility_Id"];
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_ASI_VisitInformation_VisitNumber"];
        }
        else
        {
          string FacilityId = "";
          string ASIVisitInformationVisitNumber = "";
          string SQLStringVisitInfo = "SELECT Facility_Id , ASI_VisitInformation_VisitNumber FROM Form_AntimicrobialStewardshipIntervention_VisitInformation WHERE ASI_VisitInformation_Id = @ASI_VisitInformation_Id";
          using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
          {
            SqlCommand_VisitInfo.Parameters.AddWithValue("@ASI_VisitInformation_Id", Request.QueryString["ASIVisitInformationId"]);
            DataTable DataTable_VisitInfo;
            using (DataTable_VisitInfo = new DataTable())
            {
              DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
              DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
              if (DataTable_VisitInfo.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
                {
                  FacilityId = DataRow_Row["Facility_Id"].ToString();
                  ASIVisitInformationVisitNumber = DataRow_Row["ASI_VisitInformation_VisitNumber"].ToString();
                }
              }
            }
          }

          DropDownList_Facility.SelectedValue = FacilityId;
          TextBox_PatientVisitNumber.Text = ASIVisitInformationVisitNumber;

          FacilityId = "";
          ASIVisitInformationVisitNumber = "";
        }
      }
    }

    protected void TableVisible()
    {
      if (TableVisitInfo.Visible == true)
      {
        TableVisitInfoVisible();
      }

      if (TableCurrentIntervention.Visible == true)
      {
        TableCurrentInterventionVisible();
      }
    }


    private void VisitData()
    {
      string PatientInformationId = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PatientInformationId(Request.QueryString["s_Facility_Id"], Request.QueryString["s_ASI_VisitInformation_VisitNumber"]);
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
          DataTable_VisitData = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_ASI_VisitInformation_VisitNumber"]).Copy();
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

              string ASIVisitInformationId = "";
              string SQLStringVisitInfo = "SELECT ASI_VisitInformation_Id FROM Form_AntimicrobialStewardshipIntervention_VisitInformation WHERE Facility_Id = @Facility_Id AND ASI_VisitInformation_VisitNumber = @ASI_VisitInformation_VisitNumber";
              using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
              {
                SqlCommand_VisitInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_VisitInfo.Parameters.AddWithValue("@ASI_VisitInformation_VisitNumber", Request.QueryString["s_ASI_VisitInformation_VisitNumber"]);
                DataTable DataTable_VisitInfo;
                using (DataTable_VisitInfo = new DataTable())
                {
                  DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
                  DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
                  if (DataTable_VisitInfo.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row1 in DataTable_VisitInfo.Rows)
                    {
                      ASIVisitInformationId = DataRow_Row1["ASI_VisitInformation_Id"].ToString();
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(ASIVisitInformationId))
              {
                string SQLStringInsertVisitInformation = "INSERT INTO Form_AntimicrobialStewardshipIntervention_VisitInformation ( Facility_Id , ASI_VisitInformation_VisitNumber , PatientInformation_Id , ASI_VisitInformation_PatientAge , ASI_VisitInformation_DateOfAdmission , ASI_VisitInformation_DateOfDischarge , ASI_VisitInformation_Archived ) VALUES ( @Facility_Id , @ASI_VisitInformation_VisitNumber , @PatientInformation_Id , @ASI_VisitInformation_PatientAge , @ASI_VisitInformation_DateOfAdmission , @ASI_VisitInformation_DateOfDischarge , @ASI_VisitInformation_Archived ); SELECT SCOPE_IDENTITY()";
                using (SqlCommand SqlCommand_InsertVisitInformation = new SqlCommand(SQLStringInsertVisitInformation))
                {
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@ASI_VisitInformation_VisitNumber", Request.QueryString["s_ASI_VisitInformation_VisitNumber"]);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@ASI_VisitInformation_PatientAge", PatientAge);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@ASI_VisitInformation_DateOfAdmission", DateOfAdmission);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@ASI_VisitInformation_DateOfDischarge", DateOfDischarge);
                  SqlCommand_InsertVisitInformation.Parameters.AddWithValue("@ASI_VisitInformation_Archived", 0);
                  ASIVisitInformationId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertVisitInformation);
                }
              }
              else
              {
                string SQLStringUpdateVisitInformation = "UPDATE Form_AntimicrobialStewardshipIntervention_VisitInformation SET PatientInformation_Id = @PatientInformation_Id , ASI_VisitInformation_PatientAge  = @ASI_VisitInformation_PatientAge , ASI_VisitInformation_DateOfAdmission  = @ASI_VisitInformation_DateOfAdmission , ASI_VisitInformation_DateOfDischarge  = @ASI_VisitInformation_DateOfDischarge WHERE Facility_Id = @Facility_Id AND ASI_VisitInformation_VisitNumber = @ASI_VisitInformation_VisitNumber";
                using (SqlCommand SqlCommand_UpdateVisitInformation = new SqlCommand(SQLStringUpdateVisitInformation))
                {
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@PatientInformation_Id", PatientInformationId);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@ASI_VisitInformation_PatientAge", PatientAge);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@ASI_VisitInformation_DateOfAdmission", DateOfAdmission);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@ASI_VisitInformation_DateOfDischarge", DateOfDischarge);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_UpdateVisitInformation.Parameters.AddWithValue("@ASI_VisitInformation_VisitNumber", Request.QueryString["s_ASI_VisitInformation_VisitNumber"]);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateVisitInformation);
                }
              }

              Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_AntimicrobialStewardshipIntervention", "Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + ASIVisitInformationId), false);
            }
          }
        }
      }
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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('39')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_AntimicrobialStewardshipIntervention_VisitInformation WHERE ASI_VisitInformation_Id = @ASI_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@ASI_VisitInformation_Id", Request.QueryString["ASIVisitInformationId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '160'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '161'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '162'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '163'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_FacilityId
    {
      public string FacilityId { get; set; }
      public string ASIVisitInformationVisitNumber { get; set; }
    }

    private FromDataBase_FacilityId GetFacilityId()
    {
      FromDataBase_FacilityId FromDataBase_FacilityId_New = new FromDataBase_FacilityId();

      string SQLStringFacility = "SELECT Facility_Id , ASI_VisitInformation_VisitNumber FROM Form_AntimicrobialStewardshipIntervention_VisitInformation WHERE ASI_VisitInformation_Id = @ASI_VisitInformation_Id";
      using (SqlCommand SqlCommand_Facility = new SqlCommand(SQLStringFacility))
      {
        SqlCommand_Facility.Parameters.AddWithValue("@ASI_VisitInformation_Id", Request.QueryString["ASIVisitInformationId"]);
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
              FromDataBase_FacilityId_New.ASIVisitInformationVisitNumber = DataRow_Row["ASI_VisitInformation_VisitNumber"].ToString();
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

      string SQLStringFormViewUpdate = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 39),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,ASI_Intervention_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 39),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,ASI_Intervention_Date)+1,0))) < GETDATE() THEN 'No' END AS ViewUpdate FROM Form_AntimicrobialStewardshipIntervention_Intervention WHERE ASI_Intervention_Id = @ASI_Intervention_Id";
      using (SqlCommand SqlCommand_FormViewUpdate = new SqlCommand(SQLStringFormViewUpdate))
      {
        SqlCommand_FormViewUpdate.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Form", "Form_AntimicrobialStewardshipIntervention.aspx"), false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect("Form_AntimicrobialStewardshipIntervention.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_ASI_VisitInformation_VisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "", false);
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
      string SearchField2 = Request.QueryString["Search_AntimicrobialStewardshipInterventionPatientVisitNumber"];
      string SearchField3 = Request.QueryString["Search_AntimicrobialStewardshipInterventionPatientName"];
      string SearchField4 = Request.QueryString["Search_AntimicrobialStewardshipInterventionReportNumber"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_AntimicrobialStewardshipIntervention_PatientVisitNumber=" + Request.QueryString["Search_AntimicrobialStewardshipInterventionPatientVisitNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_AntimicrobialStewardshipIntervention_PatientName=" + Request.QueryString["Search_AntimicrobialStewardshipInterventionPatientName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_AntimicrobialStewardshipIntervention_ReportNumber=" + Request.QueryString["Search_AntimicrobialStewardshipInterventionReportNumber"] + "&";
      }

      string FinalURL = "Form_AntimicrobialStewardshipIntervention_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Captured Forms", FinalURL);

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --VisitInfo--//
    private void TableVisitInfoVisible()
    {
      string FacilityFacilityDisplayName = "";
      string ASIVisitInformationVisitNumber = "";
      string PatientInformationName = "";
      string PatientInformationSurname = "";
      string ASIVisitInformationPatientAge = "";
      string ASIVisitInformationDateOfAdmission = "";
      string ASIVisitInformationDateOfDischarge = "";
      string SQLStringVisitInfo = "SELECT Facility_FacilityDisplayName , ASI_VisitInformation_VisitNumber , PatientInformation_Name , PatientInformation_Surname , ASI_VisitInformation_PatientAge , ASI_VisitInformation_DateOfAdmission , ASI_VisitInformation_DateOfDischarge FROM vForm_AntimicrobialStewardshipIntervention_VisitInformation WHERE ASI_VisitInformation_Id = @ASI_VisitInformation_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@ASI_VisitInformation_Id", Request.QueryString["ASIVisitInformationId"]);
        DataTable DataTable_VisitInfo;
        using (DataTable_VisitInfo = new DataTable())
        {
          DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
          if (DataTable_VisitInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
            {
              FacilityFacilityDisplayName = DataRow_Row["Facility_FacilityDisplayName"].ToString();
              ASIVisitInformationVisitNumber = DataRow_Row["ASI_VisitInformation_VisitNumber"].ToString();
              PatientInformationName = DataRow_Row["PatientInformation_Name"].ToString();
              PatientInformationSurname = DataRow_Row["PatientInformation_Surname"].ToString();
              ASIVisitInformationPatientAge = DataRow_Row["ASI_VisitInformation_PatientAge"].ToString();
              ASIVisitInformationDateOfAdmission = DataRow_Row["ASI_VisitInformation_DateOfAdmission"].ToString();
              ASIVisitInformationDateOfDischarge = DataRow_Row["ASI_VisitInformation_DateOfDischarge"].ToString();
            }
          }
        }
      }

      Label_VIFacility.Text = FacilityFacilityDisplayName;
      Label_VIVisitNumber.Text = ASIVisitInformationVisitNumber;
      Label_VIName.Text = PatientInformationSurname + Convert.ToString(", ", CultureInfo.CurrentCulture) + PatientInformationName;
      Label_VIAge.Text = ASIVisitInformationPatientAge;
      Label_VIDateAdmission.Text = ASIVisitInformationDateOfAdmission;
      Label_VIDateDischarge.Text = ASIVisitInformationDateOfDischarge;

      FacilityFacilityDisplayName = "";
      ASIVisitInformationVisitNumber = "";
      PatientInformationName = "";
      PatientInformationSurname = "";
      ASIVisitInformationPatientAge = "";
      ASIVisitInformationDateOfAdmission = "";
      ASIVisitInformationDateOfDischarge = "";
    }
    //---END--- --VisitInfo--//


    //--START-- --TableCurrentIntervention--//
    protected void SetCurrentInterventionVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["ASIInterventionId"]))
      {
        SetCurrentInterventionVisibility_Insert();
      }
      else
      {
        SetCurrentInterventionVisibility_Edit();
      }
    }

    protected void SetCurrentInterventionVisibility_Insert()
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
        FormView_AntimicrobialStewardshipIntervention_Form.ChangeMode(FormViewMode.Insert);
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_AntimicrobialStewardshipIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_AntimicrobialStewardshipIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetCurrentInterventionVisibility_Edit()
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
        FormView_AntimicrobialStewardshipIntervention_Form.ChangeMode(FormViewMode.Edit);
      }

      if (Security == "1" && (SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (ViewUpdate == "Yes")
        {
          FormView_AntimicrobialStewardshipIntervention_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_AntimicrobialStewardshipIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_AntimicrobialStewardshipIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_AntimicrobialStewardshipIntervention_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentInterventionVisible()
    {
      if (FormView_AntimicrobialStewardshipIntervention_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();");
        ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertByList")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertUnit")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertDoctor")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");

        ((CheckBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBox_InsertNursingSelectAllYes")).Attributes.Add("OnClick", "NursingSelectAllYes_Form();Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing1B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing2B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing3")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing3B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing4")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing5")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing5B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing6")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing6B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((CheckBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBox_InsertPharmacySelectAllYes")).Attributes.Add("OnClick", "PharmacySelectAllYes_Form();Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy1B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy2B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy3")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy3B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy4")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy4B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy5")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy5B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy6")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy6B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy7")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy7B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy8")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");

        ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_InsertNursing5BReason")).Attributes.Add("OnClick", "Validation_Form();");

        foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic")).Rows)
        {
          CheckBox CheckBox_InsertAntibiotic = (CheckBox)GridViewRow_Row.FindControl("CheckBox_InsertAntibiotic");
          CheckBox_InsertAntibiotic.Attributes.Add("OnClick", "Validation_Form();");
        }
      }

      if (FormView_AntimicrobialStewardshipIntervention_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();Calculation_Form();");
        ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditByList")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditUnit")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");
        ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditDoctor")).Attributes.Add("OnChange", "Validation_Form();Calculation_Form();");

        ((CheckBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBox_EditNursingSelectAllYes")).Attributes.Add("OnClick", "NursingSelectAllYes_Form();Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing1B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing2B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing3")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing3B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing4")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing5")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing5B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing6")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing6B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

        ((CheckBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBox_EditPharmacySelectAllYes")).Attributes.Add("OnClick", "PharmacySelectAllYes_Form();Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy1B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy2B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy3")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy3B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy4")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy4B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy5")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy5B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy6")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy6B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy7")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy7B")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy8")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");

        ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Attributes.Add("OnClick", "Validation_Form();");

        foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_EditAntimicrobialStewardshipIntervention_Antibiotic")).Rows)
        {
          CheckBox CheckBox_EditAntibiotic = (CheckBox)GridViewRow_Row.FindControl("CheckBox_EditAntibiotic");
          CheckBox_EditAntibiotic.Attributes.Add("OnClick", "Validation_Form();");
        }
      }
    }


    //--START-- --Insert--//
    protected void FormView_AntimicrobialStewardshipIntervention_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ToolkitScriptManager_AntimicrobialStewardshipIntervention.SetFocus(UpdatePanel_AntimicrobialStewardshipIntervention);
          ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (e.Cancel == false)
        {
          FromDataBase_FacilityId FromDataBase_FacilityId_Current = GetFacilityId();
          string FacilityId = FromDataBase_FacilityId_Current.FacilityId;

          string ASI_Intervention_ReportNumber = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], FacilityId, "39");

          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_VisitInformation_Id"].DefaultValue = Request.QueryString["ASIVisitInformationId"];
          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_ReportNumber"].DefaultValue = ASI_Intervention_ReportNumber;
          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_History"].DefaultValue = "";
          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_IsActive"].DefaultValue = "true";

          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_Unit"].DefaultValue = ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertUnit")).SelectedValue;
          SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_Doctor"].DefaultValue = ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertDoctor")).SelectedValue;

          Int32 Nursing_Total = 0;
          Int32 Nursing_Selected = 0;

          for (Int32 NursingA = 1; NursingA <= 6; NursingA++)
          {
            if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing" + NursingA)).SelectedIndex == 0)
            {
              Nursing_Total = Nursing_Total + 1;
              Nursing_Selected = Nursing_Selected + 1;
            }
            else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing" + NursingA)).SelectedIndex == 1)
            {
              Nursing_Selected = Nursing_Selected + 1;
            }
          }

          if (Nursing_Selected != 0)
          {
            Decimal Nursing_Score = (Nursing_Total * 100 / Nursing_Selected);
            SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_Nursing_Score"].DefaultValue = Nursing_Total + " / " + Nursing_Selected + " (" + Decimal.Round(Nursing_Score, MidpointRounding.ToEven) + " %)";
          }
          else
          {
            SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_Nursing_Score"].DefaultValue = DBNull.Value.ToString();
          }


          Int32 Pharmacy_Total = 0;
          Int32 Pharmacy_Selected = 0;

          for (Int32 PharmacyA = 1; PharmacyA <= 8; PharmacyA++)
          {
            if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy" + PharmacyA)).SelectedIndex == 0)
            {
              Pharmacy_Total = Pharmacy_Total + 1;
              Pharmacy_Selected = Pharmacy_Selected + 1;
            }
            else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy" + PharmacyA)).SelectedIndex == 1)
            {
              Pharmacy_Selected = Pharmacy_Selected + 1;
            }
          }

          if (Pharmacy_Selected != 0)
          {
            Decimal Pharmacy_Score = (Pharmacy_Total * 100 / Pharmacy_Selected);
            SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_Pharmacy_Score"].DefaultValue = Pharmacy_Total + " / " + Pharmacy_Selected + " (" + Decimal.Round(Pharmacy_Score, MidpointRounding.ToEven) + " %)";
          }
          else
          {
            SqlDataSource_AntimicrobialStewardshipIntervention_Form.InsertParameters["ASI_Intervention_Pharmacy_Score"].DefaultValue = DBNull.Value.ToString();
          }


          ASI_Intervention_ReportNumber = "";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_InsertDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertByList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertDoctor")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertByList")).SelectedValue == "5390")
        {
          InvalidForm = InsertValidation_Nursing(InvalidForm);
        }
        else if (((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_InsertByList")).SelectedValue == "5389")
        {
          InvalidForm = InsertValidation_Pharmacy(InvalidForm);
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

    protected string InsertValidation_Nursing(string invalidForm)
    {
      string InvalidForm = invalidForm;      

      for (Int32 NursingA = 1; NursingA <= 6; NursingA++)
      {
        if (NursingA == 4)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing" + NursingA)).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else if (NursingA == 5)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing" + NursingA)).SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing" + NursingA)).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing" + NursingA + "B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
            else
            {
              if (((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_InsertNursing5BReason")).Items.Count > 0)
              {
                string Nursing5BReasonSelected = "No";
                for (int i = 0; i < ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_InsertNursing5BReason")).Items.Count; i++)
                {
                  if (((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_InsertNursing5BReason")).Items[i].Selected == true)
                  {
                    Nursing5BReasonSelected = "Yes";
                  }
                }

                if (Nursing5BReasonSelected == "No")
                {
                  InvalidForm = "Yes";
                }
              }
            }
          }
        }
        else
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing" + NursingA)).SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing" + NursingA)).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing" + NursingA + "B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string InsertValidation_Pharmacy(string invalidForm)
    {
      string InvalidForm = invalidForm;

      for (Int32 PharmacyA = 1; PharmacyA <= 8; PharmacyA++)
      {
        if (PharmacyA == 1)
        {
          if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy1")).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy1B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }
        else if (PharmacyA == 7)
        {
          if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy7")).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy7B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }
        else if (PharmacyA == 8)
        {
        }
        else
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy" + PharmacyA)).SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy" + PharmacyA)).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertPharmacy" + PharmacyA + "B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string InsertFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      string DateToValidateDate = ((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_InsertDate")).Text.ToString();
      DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

      if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + "Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
      }
      else
      {
        DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_InsertDate")).Text, CultureInfo.CurrentCulture);
        DateTime CurrentDate = DateTime.Now;

        if (PickedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
        }
        else
        {
          string ValidCapture = "";
          string CutOffDay = "";
          string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 39),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@ASI_Intervention_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 39),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@ASI_Intervention_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 39) AS CutOffDay";
          using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
          {
            SqlCommand_ValidCapture.Parameters.AddWithValue("@ASI_Intervention_Date", PickedDate);
            DataTable DataTable_ValidCapture;
            using (DataTable_ValidCapture = new DataTable())
            {
              DataTable_ValidCapture.Locale = CultureInfo.CurrentCulture;
              DataTable_ValidCapture = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ValidCapture).Copy();
              if (DataTable_ValidCapture.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_ValidCapture.Rows)
                {
                  ValidCapture = DataRow_Row["ValidCapture"].ToString();
                  CutOffDay = DataRow_Row["CutOffDay"].ToString();
                }
              }
            }
          }

          if (ValidCapture != "Yes")
          {
            InvalidFormMessage = InvalidFormMessage + "Date is not valid. Forms may be captured between the 1st of a calendar month until the " + CutOffDay + "th of the following month <br />";
          }

          ValidCapture = "";
          CutOffDay = "";
        }
      }


      if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing1")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing2")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing3")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing4")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing5")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_InsertNursing6")).SelectedIndex == 2)
      {
        InvalidFormMessage = InvalidFormMessage + "An Assessment can't be captured because all the answers are Not Applicable<br />";
      }


      //if (((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic")).Rows.Count == 0)
      //{
      //  InvalidFormMessage = InvalidFormMessage + "An Assessment can't be captured for this visit, no Antibiotics prescribed";
      //}
      //else
      //{
      //  String ValidAntibiotic = "No";

      //  foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic")).Rows)
      //  {
      //    CheckBox CheckBox_InsertAntibiotic = (CheckBox)GridViewRow_Row.FindControl("CheckBox_InsertAntibiotic");
      //    if (CheckBox_InsertAntibiotic.Checked == true)
      //    {
      //      ValidAntibiotic = "Yes";
      //    }
      //  }

      //  if (ValidAntibiotic == "No")
      //  {
      //    InvalidFormMessage = InvalidFormMessage + "An Antibiotic needs to selected for which the Assessment is being captured";
      //  }
      //}

      return InvalidFormMessage;
    }

    protected void SqlDataSource_AntimicrobialStewardshipIntervention_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        string ASI_Intervention_Id = e.Command.Parameters["@ASI_Intervention_Id"].Value.ToString();
        string ASI_Intervention_ReportNumber = e.Command.Parameters["@ASI_Intervention_ReportNumber"].Value.ToString();

        if (!string.IsNullOrEmpty(ASI_Intervention_Id))
        {
          if (((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_InsertNursing5BReason")).Items.Count > 0)
          {
            for (int i = 0; i < ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_InsertNursing5BReason")).Items.Count; i++)
            {
              if (((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_InsertNursing5BReason")).Items[i].Selected == true)
              {
                string SQLStringInsertNursing5B = "INSERT INTO Form_AntimicrobialStewardshipIntervention_Nursing5B ( ASI_Intervention_Id , ASI_Nursing5B_Item_List ) VALUES ( @ASI_Intervention_Id , @ASI_Nursing5B_Item_List )";
                using (SqlCommand SqlCommand_InsertNursing5B = new SqlCommand(SQLStringInsertNursing5B))
                {
                  SqlCommand_InsertNursing5B.Parameters.AddWithValue("@ASI_Intervention_Id", ASI_Intervention_Id);
                  SqlCommand_InsertNursing5B.Parameters.AddWithValue("@ASI_Nursing5B_Item_List", ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_InsertNursing5BReason")).Items[i].Value.ToString());
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertNursing5B);
                }
              }
            }
          }

          if (((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic")).Rows.Count > 0)
          {
            foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic")).Rows)
            {
              CheckBox CheckBox_InsertAntibiotic = (CheckBox)GridViewRow_Row.FindControl("CheckBox_InsertAntibiotic");
              Label Label_InsertAntibioticDescription = (Label)GridViewRow_Row.FindControl("Label_InsertAntibioticDescription");
              TextBox TextBox_InsertAntibiotic_Information = (TextBox)GridViewRow_Row.FindControl("TextBox_InsertAntibiotic_Information");

              if (CheckBox_InsertAntibiotic.Checked == true)
              {
                string SQLStringInsertAntibiotic = "INSERT INTO Form_AntimicrobialStewardshipIntervention_Antibiotic ( ASI_Intervention_Id , ASI_Antibiotic_Description , ASI_Antibiotic_Information ) VALUES ( @ASI_Intervention_Id , @ASI_Antibiotic_Description , @ASI_Antibiotic_Information )";
                using (SqlCommand SqlCommand_InsertAntibiotic = new SqlCommand(SQLStringInsertAntibiotic))
                {
                  SqlCommand_InsertAntibiotic.Parameters.AddWithValue("@ASI_Intervention_Id", ASI_Intervention_Id);
                  SqlCommand_InsertAntibiotic.Parameters.AddWithValue("@ASI_Antibiotic_Description", Label_InsertAntibioticDescription.Text);
                  SqlCommand_InsertAntibiotic.Parameters.AddWithValue("@ASI_Antibiotic_Information", TextBox_InsertAntibiotic_Information.Text);

                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertAntibiotic);
                }
              }
            }
          }
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_AntimicrobialStewardshipIntervention&ReportNumber=" + ASI_Intervention_ReportNumber + ""), false);
      }
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    protected void FormView_AntimicrobialStewardshipIntervention_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDASIInterventionModifiedDate"] = e.OldValues["ASI_Intervention_ModifiedDate"];
        object OLDASIInterventionModifiedDate = Session["OLDASIInterventionModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDASIInterventionModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareAntimicrobialStewardshipIntervention = (DataView)SqlDataSource_AntimicrobialStewardshipIntervention_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareAntimicrobialStewardshipIntervention = DataView_CompareAntimicrobialStewardshipIntervention[0];
        Session["DBASIInterventionModifiedDate"] = Convert.ToString(DataRowView_CompareAntimicrobialStewardshipIntervention["ASI_Intervention_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBASIInterventionModifiedBy"] = Convert.ToString(DataRowView_CompareAntimicrobialStewardshipIntervention["ASI_Intervention_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBASIInterventionModifiedDate = Session["DBASIInterventionModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBASIInterventionModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_AntimicrobialStewardshipIntervention.SetFocus(UpdatePanel_AntimicrobialStewardshipIntervention);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBASIInterventionModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ToolkitScriptManager_AntimicrobialStewardshipIntervention.SetFocus(UpdatePanel_AntimicrobialStewardshipIntervention);
            ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["ASI_Intervention_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["ASI_Intervention_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];
            e.NewValues["Unit_Id"] = ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditUnit")).SelectedValue;

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_AntimicrobialStewardshipIntervention_Intervention", "ASI_Intervention_Id = " + Request.QueryString["ASIInterventionId"]);

            DataView DataView_AntimicrobialStewardshipIntervention = (DataView)SqlDataSource_AntimicrobialStewardshipIntervention_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_AntimicrobialStewardshipIntervention = DataView_AntimicrobialStewardshipIntervention[0];
            Session["ASIInterventionHistory"] = Convert.ToString(DataRowView_AntimicrobialStewardshipIntervention["ASI_Intervention_History"], CultureInfo.CurrentCulture);

            Session["ASIInterventionHistory"] = Session["History"].ToString() + Session["ASIInterventionHistory"].ToString();
            e.NewValues["ASI_Intervention_History"] = Session["ASIInterventionHistory"].ToString();

            Session["ASIInterventionHistory"] = "";
            Session["History"] = "";


            e.NewValues["ASI_Intervention_Unit"] = ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditUnit")).SelectedValue;
            e.NewValues["ASI_Intervention_Doctor"] = ((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditDoctor")).SelectedValue;

            Int32 Nursing_Total = 0;
            Int32 Nursing_Selected = 0;

            for (Int32 NursingA = 1; NursingA <= 6; NursingA++)
            {
              if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing" + NursingA)).SelectedIndex == 0)
              {
                Nursing_Total = Nursing_Total + 1;
                Nursing_Selected = Nursing_Selected + 1;
              }
              else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing" + NursingA)).SelectedIndex == 1)
              {
                Nursing_Selected = Nursing_Selected + 1;
              }
            }

            if (Nursing_Selected != 0)
            {
              Decimal Nursing_Score = (Nursing_Total * 100 / Nursing_Selected);
              e.NewValues["ASI_Intervention_Nursing_Score"] = Nursing_Total + " / " + Nursing_Selected + " (" + Decimal.Round(Nursing_Score, MidpointRounding.ToEven) + " %)";
            }
            else
            {
              e.NewValues["ASI_Intervention_Nursing_Score"] = DBNull.Value.ToString();
            }


            Int32 Pharmacy_Total = 0;
            Int32 Pharmacy_Selected = 0;

            for (Int32 PharmacyA = 1; PharmacyA <= 8; PharmacyA++)
            {
              if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy" + PharmacyA)).SelectedIndex == 0)
              {
                Pharmacy_Total = Pharmacy_Total + 1;
                Pharmacy_Selected = Pharmacy_Selected + 1;
              }
              else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy" + PharmacyA)).SelectedIndex == 1)
              {
                Pharmacy_Selected = Pharmacy_Selected + 1;
              }
            }

            if (Pharmacy_Selected != 0)
            {
              Decimal Pharmacy_Score = (Pharmacy_Total * 100 / Pharmacy_Selected);
              e.NewValues["ASI_Intervention_Pharmacy_Score"] = Pharmacy_Total + " / " + Pharmacy_Selected + " (" + Decimal.Round(Pharmacy_Score, MidpointRounding.ToEven) + " %)";
            }
            else
            {
              e.NewValues["ASI_Intervention_Pharmacy_Score"] = DBNull.Value.ToString();
            }
          }
        }

        Session["OLDASIInterventionModifiedDate"] = "";
        Session["DBASIInterventionModifiedDate"] = "";
        Session["DBASIInterventionModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_EditDate")).Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditByList")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditUnit")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditDoctor")).SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditByList")).SelectedValue == "5390")
        {
          InvalidForm = EditValidation_Nursing(InvalidForm);
        }
        else if (((DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditByList")).SelectedValue == "5389")
        {
          InvalidForm = EditValidation_Pharmacy(InvalidForm);
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

    protected string EditValidation_Nursing(string invalidForm)
    {
      string InvalidForm = invalidForm;

      for (Int32 NursingA = 1; NursingA <= 6; NursingA++)
      {
        if (NursingA == 4)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing" + NursingA)).SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }
        else if (NursingA == 5)
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing" + NursingA)).SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing" + NursingA)).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing" + NursingA + "B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
            else
            {
              if (((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items.Count > 0)
              {
                string Nursing5BReasonSelected = "No";
                for (int i = 0; i < ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items.Count; i++)
                {
                  if (((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items[i].Selected == true)
                  {
                    Nursing5BReasonSelected = "Yes";
                  }
                }

                if (Nursing5BReasonSelected == "No")
                {
                  InvalidForm = "Yes";
                }
              }
            }
          }
        }
        else
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing" + NursingA)).SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing" + NursingA)).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing" + NursingA + "B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string EditValidation_Pharmacy(string invalidForm)
    {
      string InvalidForm = invalidForm;

      for (Int32 PharmacyA = 1; PharmacyA <= 8; PharmacyA++)
      {
        if (PharmacyA == 1)
        {
          if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy1")).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy1B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }
        else if (PharmacyA == 7)
        {
          if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy7")).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy7B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }
        else if (PharmacyA == 8)
        {
        }
        else
        {
          if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy" + PharmacyA)).SelectedValue))
          {
            InvalidForm = "Yes";
          }
          else if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy" + PharmacyA)).SelectedIndex == 1)
          {
            if (string.IsNullOrEmpty(((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy" + PharmacyA + "B")).SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      return InvalidForm;
    }

    protected string EditFieldValidation(string invalidFormMessage)
    {
      string InvalidFormMessage = invalidFormMessage;

      string DateToValidateDate = ((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_EditDate")).Text.ToString();
      DateTime ValidatedDateDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDate);

      if (ValidatedDateDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + "Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
      }
      else
      {
        DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("TextBox_EditDate")).Text, CultureInfo.CurrentCulture);
        DateTime CurrentDate = DateTime.Now;

        if (PickedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
        }
        else
        {
          DateTime PreviousDate = Convert.ToDateTime(((HiddenField)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("HiddenField_EditDate")).Value, CultureInfo.CurrentCulture);

          if (PickedDate.CompareTo(PreviousDate) != 0)
          {
            string ValidCapture = "";
            string CutOffDay = "";
            string SQLStringValidCapture = "SELECT CASE WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 39),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@ASI_Intervention_Date)+1,0))) >= GETDATE() THEN 'Yes' WHEN DATEADD(DAY,(SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 39),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@ASI_Intervention_Date)+1,0))) < GETDATE() THEN 'No' END AS ValidCapture , (SELECT Form_CutOffDay FROM Administration_Form WHERE Form_Id = 39) AS CutOffDay";
            using (SqlCommand SqlCommand_ValidCapture = new SqlCommand(SQLStringValidCapture))
            {
              SqlCommand_ValidCapture.Parameters.AddWithValue("@ASI_Intervention_Date", PickedDate);
              DataTable DataTable_ValidCapture;
              using (DataTable_ValidCapture = new DataTable())
              {
                DataTable_ValidCapture.Locale = CultureInfo.CurrentCulture;
                DataTable_ValidCapture = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_ValidCapture).Copy();
                if (DataTable_ValidCapture.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_ValidCapture.Rows)
                  {
                    ValidCapture = DataRow_Row["ValidCapture"].ToString();
                    CutOffDay = DataRow_Row["CutOffDay"].ToString();
                  }
                }
              }
            }

            if (ValidCapture != "Yes")
            {
              InvalidFormMessage = InvalidFormMessage + "date is not valid. Forms may be captured between the 1st of a calendar month until the " + CutOffDay + "th of the following month <br />";
            }

            ValidCapture = "";
            CutOffDay = "";
          }
        }
      }


      if (((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing1")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing2")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing3")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing4")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing5")).SelectedIndex == 2 && ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing6")).SelectedIndex == 2)
      {
        InvalidFormMessage = InvalidFormMessage + "An Assessment can't be captured because all the answers are Not Applicable<br />";
      }


      //if (((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_EditAntimicrobialStewardshipIntervention_Antibiotic")).Rows.Count == 0)
      //{
      //  InvalidFormMessage = InvalidFormMessage + "An Assessment can't be captured for this visit, no Antibiotics prescribed";
      //}
      //else
      //{
      //  String ValidAntibiotic = "No";

      //  foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_EditAntimicrobialStewardshipIntervention_Antibiotic")).Rows)
      //  {
      //    CheckBox CheckBox_EditAntibiotic = (CheckBox)GridViewRow_Row.FindControl("CheckBox_EditAntibiotic");
      //    if (CheckBox_EditAntibiotic.Checked == true)
      //    {
      //      ValidAntibiotic = "Yes";
      //    }
      //  }

      //  if (ValidAntibiotic == "No")
      //  {
      //    InvalidFormMessage = InvalidFormMessage + "An Antibiotic needs to selected for which the Assessment is being captured";
      //  }
      //}

      return InvalidFormMessage;
    }

    protected void SqlDataSource_AntimicrobialStewardshipIntervention_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            if (!string.IsNullOrEmpty(Request.QueryString["ASIInterventionId"]))
            {
              if (((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items.Count > 0)
              {
                string SQLStringDeleteNursing5B = "DELETE FROM Form_AntimicrobialStewardshipIntervention_Nursing5B WHERE ASI_Intervention_Id = @ASI_Intervention_Id";
                using (SqlCommand SqlCommand_DeleteNursing5B = new SqlCommand(SQLStringDeleteNursing5B))
                {
                  SqlCommand_DeleteNursing5B.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteNursing5B);
                }

                for (int i = 0; i < ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items.Count; i++)
                {
                  if (((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items[i].Selected == true)
                  {
                    string SQLStringInsertNursing5B = "INSERT INTO Form_AntimicrobialStewardshipIntervention_Nursing5B ( ASI_Intervention_Id , ASI_Nursing5B_Item_List ) VALUES ( @ASI_Intervention_Id , @ASI_Nursing5B_Item_List )";
                    using (SqlCommand SqlCommand_InsertNursing5B = new SqlCommand(SQLStringInsertNursing5B))
                    {
                      SqlCommand_InsertNursing5B.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
                      SqlCommand_InsertNursing5B.Parameters.AddWithValue("@ASI_Nursing5B_Item_List", ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items[i].Value.ToString());
                      InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertNursing5B);
                    }
                  }
                }
              }

              if (((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_EditAntimicrobialStewardshipIntervention_Antibiotic")).Rows.Count > 0)
              {
                string SQLStringDeleteAntibiotic = "DELETE FROM Form_AntimicrobialStewardshipIntervention_Antibiotic WHERE ASI_Intervention_Id = @ASI_Intervention_Id";
                using (SqlCommand SqlCommand_DeleteAntibiotic = new SqlCommand(SQLStringDeleteAntibiotic))
                {
                  SqlCommand_DeleteAntibiotic.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteAntibiotic);
                }

                foreach (GridViewRow GridViewRow_Row in ((GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_EditAntimicrobialStewardshipIntervention_Antibiotic")).Rows)
                {
                  CheckBox CheckBox_EditAntibiotic = (CheckBox)GridViewRow_Row.FindControl("CheckBox_EditAntibiotic");
                  Label Label_EditAntibioticDescription = (Label)GridViewRow_Row.FindControl("Label_EditAntibioticDescription");
                  TextBox TextBox_EditAntibiotic_Information = (TextBox)GridViewRow_Row.FindControl("TextBox_EditAntibiotic_Information");

                  if (CheckBox_EditAntibiotic.Checked == true)
                  {
                    string SQLStringEditAntibiotic = "INSERT INTO Form_AntimicrobialStewardshipIntervention_Antibiotic ( ASI_Intervention_Id , ASI_Antibiotic_Description , ASI_Antibiotic_Information ) VALUES ( @ASI_Intervention_Id , @ASI_Antibiotic_Description , @ASI_Antibiotic_Information )";
                    using (SqlCommand SqlCommand_EditAntibiotic = new SqlCommand(SQLStringEditAntibiotic))
                    {
                      SqlCommand_EditAntibiotic.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
                      SqlCommand_EditAntibiotic.Parameters.AddWithValue("@ASI_Antibiotic_Description", Label_EditAntibioticDescription.Text);
                      SqlCommand_EditAntibiotic.Parameters.AddWithValue("@ASI_Antibiotic_Information", TextBox_EditAntibiotic_Information.Text);

                      InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditAntibiotic);
                    }
                  }
                }
              }
            }

            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Assessment Form", "Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + Request.QueryString["ASIVisitInformationId"] + ""), false);
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_AntimicrobialStewardshipIntervention, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Print", "InfoQuest_Print.aspx?PrintPage=Form_AntimicrobialStewardshipIntervention&PrintValue=" + Request.QueryString["ASIInterventionId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_AntimicrobialStewardshipIntervention, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;
            ScriptManager.RegisterStartupScript(UpdatePanel_AntimicrobialStewardshipIntervention, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Email", "InfoQuest_Email.aspx?EmailPage=Form_AntimicrobialStewardshipIntervention&EmailValue=" + Request.QueryString["ASIInterventionId"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_AntimicrobialStewardshipIntervention, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }
      }
    }
    //---END--- --Edit--//


    protected void FormView_AntimicrobialStewardshipIntervention_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["ASIInterventionId"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Form", "Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + Request.QueryString["ASIVisitInformationId"] + ""), false);
          }
        }
      }
    }

    protected void FormView_AntimicrobialStewardshipIntervention_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_AntimicrobialStewardshipIntervention_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_AntimicrobialStewardshipIntervention_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected void EditDataBound()
    {
      if (Request.QueryString["ASIInterventionId"] != null)
      {
        ListItem ListItem_Remove = new ListItem();
        ListItem_Remove.Text = Convert.ToString("Empty", CultureInfo.CurrentCulture);
        ListItem_Remove.Value = Convert.ToString("", CultureInfo.CurrentCulture);

        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing1")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing1B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing2")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing2B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing3")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing3B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing4")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing5")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing5B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing6")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditNursing6B")).Items.Remove(ListItem_Remove);

        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy1")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy1B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy2")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy2B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy3")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy3B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy4")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy4B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy5")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy5B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy6")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy6B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy7")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy7B")).Items.Remove(ListItem_Remove);
        ((RadioButtonList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("RadioButtonList_EditPharmacy8")).Items.Remove(ListItem_Remove);


        DropDownList DropDownList_EditUnit = (DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditUnit");
        DataView DataView_Unit = (DataView)SqlDataSource_AntimicrobialStewardshipIntervention_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_Unit = DataView_Unit[0];
        DropDownList_EditUnit.SelectedValue = Convert.ToString(DataRowView_Unit["ASI_Intervention_Unit"], CultureInfo.CurrentCulture);


        DropDownList DropDownList_EditDoctor = (DropDownList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("DropDownList_EditDoctor");
        DataView DataView_Doctor = (DataView)SqlDataSource_AntimicrobialStewardshipIntervention_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_Doctor = DataView_Doctor[0];
        DropDownList_EditDoctor.SelectedValue = Convert.ToString(DataRowView_Doctor["ASI_Intervention_Doctor"], CultureInfo.CurrentCulture);


        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 39";
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
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_EditPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_EditPrint")).Visible = true;
        }

        if (Email == "False")
        {
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_EditEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_EditEmail")).Visible = true;
        }

        Email = "";
        Print = "";
      }
    }

    protected void ReadOnlyDataBound()
    {
      if (Request.QueryString["ASIInterventionId"] != null)
      {
        string ASIInterventionByName = "";
        string SQLStringAntimicrobialStewardshipIntervention = "SELECT ASI_Intervention_By_Name FROM vForm_AntimicrobialStewardshipIntervention_Intervention WHERE ASI_Intervention_Id = @ASI_Intervention_Id";
        using (SqlCommand SqlCommand_AntimicrobialStewardshipIntervention = new SqlCommand(SQLStringAntimicrobialStewardshipIntervention))
        {
          SqlCommand_AntimicrobialStewardshipIntervention.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
          DataTable DataTable_AntimicrobialStewardshipIntervention;
          using (DataTable_AntimicrobialStewardshipIntervention = new DataTable())
          {
            DataTable_AntimicrobialStewardshipIntervention.Locale = CultureInfo.CurrentCulture;
            DataTable_AntimicrobialStewardshipIntervention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntimicrobialStewardshipIntervention).Copy();
            if (DataTable_AntimicrobialStewardshipIntervention.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_AntimicrobialStewardshipIntervention.Rows)
              {
                ASIInterventionByName = DataRow_Row["ASI_Intervention_By_Name"].ToString();
              }
            }
          }
        }

        ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_ItemByList")).Text = ASIInterventionByName;

        ASIInterventionByName = "";

        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 39";
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
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Print", "InfoQuest_Print.aspx?PrintPage=Form_AntimicrobialStewardshipIntervention&PrintValue=" + Request.QueryString["ASIInterventionId"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Email", "InfoQuest_Email.aspx?EmailPage=Form_AntimicrobialStewardshipIntervention&EmailValue=" + Request.QueryString["ASIInterventionId"] + "") + "')";
        }

        Email = "";
        Print = "";
      }
    }


    //--START-- --Insert Controls--//
    protected void GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic_DataBound(object sender, EventArgs e)
    {
      GridView GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic = (GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic");

      ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_InsertTotalRecords")).Text = GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic.Rows.Count.ToString(CultureInfo.CurrentCulture);
      ((HiddenField)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("HiddenField_InsertTotalRecords")).Value = GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void GridView_InsertAntimicrobialStewardshipIntervention_Antibiotic_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect("Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + Request.QueryString["ASIVisitInformationId"] + "", false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void CheckBoxList_EditNursing5BReason_DataBound(object sender, EventArgs e)
    {
      if (Request.QueryString["ASIInterventionId"] != null)
      {
        for (int i = 0; i < ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items.Count; i++)
        {
          string SQLStringNursing5BId = "SELECT DISTINCT ASI_Nursing5B_Item_List FROM Form_AntimicrobialStewardshipIntervention_Nursing5B WHERE ASI_Intervention_Id = @ASI_Intervention_Id AND ASI_Nursing5B_Item_List = @ASI_Nursing5B_Item_List";
          using (SqlCommand SqlCommand_Nursing5BId = new SqlCommand(SQLStringNursing5BId))
          {
            SqlCommand_Nursing5BId.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
            SqlCommand_Nursing5BId.Parameters.AddWithValue("@ASI_Nursing5B_Item_List", ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items[i].Value);
            DataTable DataTable_Nursing5BId;
            using (DataTable_Nursing5BId = new DataTable())
            {
              DataTable_Nursing5BId.Locale = CultureInfo.CurrentCulture;
              DataTable_Nursing5BId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Nursing5BId).Copy();
              if (DataTable_Nursing5BId.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Nursing5BId.Rows)
                {
                  string ASINursing5BItemList = DataRow_Row["ASI_Nursing5B_Item_List"].ToString();
                  if (!string.IsNullOrEmpty(ASINursing5BItemList))
                  {
                    ((CheckBoxList)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("CheckBoxList_EditNursing5BReason")).Items[i].Selected = true;
                  }
                }
              }
            }
          }
        }
      }
    }

    protected void GridView_EditAntimicrobialStewardshipIntervention_Antibiotic_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_EditAntimicrobialStewardshipIntervention_Antibiotic_DataBound(object sender, EventArgs e)
    {
      GridView GridView_EditAntimicrobialStewardshipIntervention_Antibiotic = (GridView)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("GridView_EditAntimicrobialStewardshipIntervention_Antibiotic");

      ((Label)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("Label_EditTotalRecords")).Text = GridView_EditAntimicrobialStewardshipIntervention_Antibiotic.Rows.Count.ToString(CultureInfo.CurrentCulture);
      ((HiddenField)FormView_AntimicrobialStewardshipIntervention_Form.FindControl("HiddenField_EditTotalRecords")).Value = GridView_EditAntimicrobialStewardshipIntervention_Antibiotic.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void GridView_EditAntimicrobialStewardshipIntervention_Antibiotic_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_EditAntimicrobialStewardshipIntervention_Antibiotic_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          HiddenField HiddenField_EditAntibioticDescription = (HiddenField)e.Row.FindControl("HiddenField_EditAntibioticDescription");

          string ASIAntibioticId = "";
          string ASIAntibioticInformation = "";
          string SQLStringAntibiotic = "SELECT ASI_Antibiotic_Id , ASI_Antibiotic_Information FROM Form_AntimicrobialStewardshipIntervention_Antibiotic WHERE ASI_Intervention_Id = @ASI_Intervention_Id AND ASI_Antibiotic_Description = @ASI_Antibiotic_Description";
          using (SqlCommand SqlCommand_Antibiotic = new SqlCommand(SQLStringAntibiotic))
          {
            SqlCommand_Antibiotic.Parameters.AddWithValue("@ASI_Intervention_Id", Request.QueryString["ASIInterventionId"]);
            SqlCommand_Antibiotic.Parameters.AddWithValue("@ASI_Antibiotic_Description", HiddenField_EditAntibioticDescription.Value);
            DataTable DataTable_Antibiotic;
            using (DataTable_Antibiotic = new DataTable())
            {
              DataTable_Antibiotic.Locale = CultureInfo.CurrentCulture;
              DataTable_Antibiotic = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Antibiotic).Copy();
              if (DataTable_Antibiotic.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Antibiotic.Rows)
                {
                  ASIAntibioticId = DataRow_Row["ASI_Antibiotic_Id"].ToString();
                  ASIAntibioticInformation = DataRow_Row["ASI_Antibiotic_Information"].ToString();
                }
              }
            }
          }

          CheckBox CheckBox_EditAntibiotic = (CheckBox)e.Row.FindControl("CheckBox_EditAntibiotic");
          TextBox TextBox_EditAntibiotic_Information = (TextBox)e.Row.FindControl("TextBox_EditAntibiotic_Information");

          if (!string.IsNullOrEmpty(ASIAntibioticId))
          {
            CheckBox_EditAntibiotic.Checked = true;
            TextBox_EditAntibiotic_Information.Text = ASIAntibioticInformation;
          }
          else
          {
            CheckBox_EditAntibiotic.Checked = false;
            TextBox_EditAntibiotic_Information.Text = "";
          }

          Session.Remove("ASIAntibioticId");
          Session.Remove("ASIAntibioticInformation");
        }
      }
    }

    protected void Button_EditCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + Request.QueryString["ASIVisitInformationId"] + "", false);
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
    protected void GridView_ItemAntimicrobialStewardshipIntervention_Nursing5BReason_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemAntimicrobialStewardshipIntervention_Nursing5BReason_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_ItemAntimicrobialStewardshipIntervention_Antibiotic_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemAntimicrobialStewardshipIntervention_Antibiotic_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect("Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + Request.QueryString["ASIVisitInformationId"] + "", false);
    }
    //---END--- --Item Controls--//
    //---END--- --TableCurrentIntervention--//


    //--START-- --TableIntervention--//
    protected void SqlDataSource_AntimicrobialStewardshipIntervention_Intervention_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_AntimicrobialStewardshipIntervention_Intervention.PageSize = Convert.ToInt32(((DropDownList)GridView_AntimicrobialStewardshipIntervention_Intervention.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridView_AntimicrobialStewardshipIntervention_Intervention.PageIndex = ((DropDownList)GridView_AntimicrobialStewardshipIntervention_Intervention.BottomPagerRow.Cells[0].FindControl("DropDownList_Page")).SelectedIndex;
    }

    protected void GridView_AntimicrobialStewardshipIntervention_Intervention_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        if (GridView_AntimicrobialStewardshipIntervention_Intervention.PageSize <= 20)
        {
          ((DropDownList)GridView_AntimicrobialStewardshipIntervention_Intervention.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "20";
        }
        else if (GridView_AntimicrobialStewardshipIntervention_Intervention.PageSize > 20 && GridView_AntimicrobialStewardshipIntervention_Intervention.PageSize <= 50)
        {
          ((DropDownList)GridView_AntimicrobialStewardshipIntervention_Intervention.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "50";
        }
        else if (GridView_AntimicrobialStewardshipIntervention_Intervention.PageSize > 50 && GridView_AntimicrobialStewardshipIntervention_Intervention.PageSize <= 100)
        {
          ((DropDownList)GridView_AntimicrobialStewardshipIntervention_Intervention.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize")).SelectedValue = "100";
        }
      }
    }

    protected void GridView_AntimicrobialStewardshipIntervention_Intervention_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_AntimicrobialStewardshipIntervention_Intervention.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_AntimicrobialStewardshipIntervention_Intervention.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_AntimicrobialStewardshipIntervention_Intervention.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            ((DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page")).Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_AntimicrobialStewardshipIntervention_Intervention_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship New Form", "Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + Request.QueryString["ASIVisitInformationId"] + ""), false);
    }

    public string GetLink(object asi_Intervention_Id)
    {
      string FinalURL = "";

      if (asi_Intervention_Id != null)
      {
        string LinkURL = "";
      
        if (Request.QueryString["ASIInterventionId"] == asi_Intervention_Id.ToString())
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Form", "Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + Request.QueryString["ASIVisitInformationId"] + "&ASIInterventionId=" + asi_Intervention_Id + "") + "'>Selected</a>";
        }
        else
        {
          LinkURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Antimicrobial Stewardship Form", "Form_AntimicrobialStewardshipIntervention.aspx?ASIVisitInformationId=" + Request.QueryString["ASIVisitInformationId"] + "&ASIInterventionId=" + asi_Intervention_Id + "") + "'>Select</a>";
        }

        FinalURL = LinkURL;  
      }

      return FinalURL;
    }
    //---END--- --TableIntervention--//
  }
}