using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_IPS_Specimen : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        ScriptManager.RegisterStartupScript(UpdatePanel_IPS_Specimen, this.GetType(), "UpdateProgress_Start", "Validation_SpecimenForm(); Validation_SpecimenResultForm(); Validation_OrganismForm();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          PageTitle();

          if (Request.QueryString["IPSVisitInformationId"] != null && Request.QueryString["IPSInfectionId"] != null)
          {
            SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Specimen_Status_List";
            SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Specimen";
            SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Specimen_Id = " + Request.QueryString["IPSSpecimenId"] + " ";

            SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Specimen_Type_List";
            SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Specimen";
            SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Specimen_Id = " + Request.QueryString["IPSSpecimenId"] + " ";

            SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters["TableSELECT"].DefaultValue = "IPS_Organism_Resistance_List";
            SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Organism";
            SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Organism_Id = " + Request.QueryString["IPSOrganismId"] + " ";

            SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters["TableSELECT"].DefaultValue = "IPS_RM_Mechanism_Item_List";
            SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters["TableFROM"].DefaultValue = "Form_IPS_Organism_ResistanceMechanism";
            SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters["TableWHERE"].DefaultValue = "IPS_Organism_Id = " + Request.QueryString["IPSOrganismId"] + " AND IPS_RM_Mechanism_Item_List != 5018 ";

            FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
            string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
            string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

            if (Request.QueryString["IPSSpecimenId"] != null)
            {
              PageSpecimenTable();

              if (Request.QueryString["IPSSpecimenResultId"] != null)
              {
                PageSpecimenResultTable();

                if (Request.QueryString["IPSOrganismId"] != null)
                {
                  PageOrganismTable();
                }
                else
                {
                  TableAntibiogram.Visible = false; DivAntibiogram.Visible = false;
                  TableAntibiogramList.Visible = false;
                }
              }
              else
              {
                TableOrganism.Visible = false; DivOrganism.Visible = false;
                TableCurrentOrganism.Visible = false; DivCurrentOrganism.Visible = false;
                TableAntibiogram.Visible = false; DivAntibiogram.Visible = false;
                TableAntibiogramList.Visible = false;
              }
            }
            else
            {
              if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
              {
                TableSpecimen.Visible = true;
                TableCurrentSpecimen.Visible = true;
              }
              else
              {
                TableSpecimen.Visible = true;
                TableCurrentSpecimen.Visible = false;
              }

              TableSpecimenResult.Visible = false; DivSpecimenResult.Visible = false;
              TableCurrentSpecimenResult.Visible = false; DivCurrentSpecimenResult.Visible = false;
              TableOrganism.Visible = false; DivOrganism.Visible = false;
              TableCurrentOrganism.Visible = false; DivCurrentOrganism.Visible = false;
              TableAntibiogram.Visible = false; DivAntibiogram.Visible = false;
              TableAntibiogramList.Visible = false;
            }

            FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_Current = GetSpecimenCompleted();
            string Specimen = FromDataBase_SpecimenCompleted_Current.Specimen;

            if (Specimen == "Incomplete")
            {
              TableCurrentInfectionComplete.Visible = false; DivCurrentInfectionComplete.Visible = false;
            }
            else
            {
              TableCurrentInfectionComplete.Visible = true; DivCurrentInfectionComplete.Visible = true;
            }

            TableInfo.Visible = true;
          }
          else
          {
            TableInfo.Visible = false;
            TableSpecimen.Visible = false;
            TableCurrentSpecimen.Visible = false;
            TableSpecimenResult.Visible = false; DivSpecimenResult.Visible = false;
            TableCurrentSpecimenResult.Visible = false; DivCurrentSpecimenResult.Visible = false;
            TableOrganism.Visible = false; DivOrganism.Visible = false;
            TableCurrentOrganism.Visible = false; DivCurrentOrganism.Visible = false;
            TableAntibiogram.Visible = false; DivAntibiogram.Visible = false;
            TableAntibiogramList.Visible = false;
            TableCurrentInfectionComplete.Visible = false; DivCurrentInfectionComplete.Visible = false;
          }

          if (TableInfo.Visible == true)
          {
            TableInfoVisible();
          }

          if (TableCurrentSpecimen.Visible == true)
          {
            SetTableCurrentSpecimenVisibility();

            TableCurrentSpecimenVisible();
          }

          if (TableCurrentInfectionComplete.Visible == true)
          {
            TableCurrentInfectionCompleteVisible();
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
        if (Request.QueryString["IPSVisitInformationId"] == null)
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
        }

        using (SqlCommand SqlCommand_Security = new SqlCommand(SQLStringSecurity))
        {
          SqlCommand_Security.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_Security.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);

          SecurityAllowForm = InfoQuestWCF.InfoQuest_Security.Security_Form_User(SqlCommand_Security);
        }

        if (SecurityAllowForm == "1")
        {
          SecurityAllow = "1";
        }
        else
        {
          SecurityAllow = "0";
          Response.Redirect("InfoQuest_PageText.aspx?PageTextValue=5", false);
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("37");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_IPS_Specimen.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Infection Prevention Surveillance", "5");
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSourceSetup_Specimen();
      SqlDataSourceSetup_SpecimenResult();
      SqlDataSourceSetup_Organism();
      SqlDataSourceSetup_Antibiogram();
    }

    private void SqlDataSourceSetup_Specimen()
    {
      SqlDataSource_IPS_Specimen_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Specimen_List.SelectCommand = "spForm_Get_IPS_Specimen_List";
      SqlDataSource_IPS_Specimen_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Specimen_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_Specimen_List.SelectParameters.Clear();
      SqlDataSource_IPS_Specimen_List.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSource_IPS_Specimen_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Specimen_Form.InsertCommand = "INSERT INTO Form_IPS_Specimen ( IPS_Infection_Id ,IPS_Specimen_Status_List ,IPS_Specimen_Date ,IPS_Specimen_TimeHours ,IPS_Specimen_TimeMinutes ,IPS_Specimen_Type_List ,IPS_Specimen_BedHistory ,IPS_Specimen_CreatedDate ,IPS_Specimen_CreatedBy ,IPS_Specimen_ModifiedDate ,IPS_Specimen_ModifiedBy ,IPS_Specimen_History ,IPS_Specimen_IsActive ) VALUES ( @IPS_Infection_Id ,@IPS_Specimen_Status_List ,@IPS_Specimen_Date ,@IPS_Specimen_TimeHours ,@IPS_Specimen_TimeMinutes ,@IPS_Specimen_Type_List ,@IPS_Specimen_BedHistory ,@IPS_Specimen_CreatedDate ,@IPS_Specimen_CreatedBy ,@IPS_Specimen_ModifiedDate ,@IPS_Specimen_ModifiedBy ,@IPS_Specimen_History ,@IPS_Specimen_IsActive ); SELECT @IPS_Specimen_Id = SCOPE_IDENTITY()";
      SqlDataSource_IPS_Specimen_Form.SelectCommand = "SELECT * FROM Form_IPS_Specimen WHERE (IPS_Specimen_Id = @IPS_Specimen_Id)";
      SqlDataSource_IPS_Specimen_Form.UpdateCommand = "UPDATE Form_IPS_Specimen SET IPS_Specimen_Status_List = @IPS_Specimen_Status_List ,IPS_Specimen_Date = @IPS_Specimen_Date ,IPS_Specimen_TimeHours = @IPS_Specimen_TimeHours ,IPS_Specimen_TimeMinutes = @IPS_Specimen_TimeMinutes ,IPS_Specimen_Type_List = @IPS_Specimen_Type_List , IPS_Specimen_BedHistory = @IPS_Specimen_BedHistory ,IPS_Specimen_ModifiedDate = @IPS_Specimen_ModifiedDate ,IPS_Specimen_ModifiedBy = @IPS_Specimen_ModifiedBy ,IPS_Specimen_History = @IPS_Specimen_History ,IPS_Specimen_IsActive = @IPS_Specimen_IsActive WHERE IPS_Specimen_Id = @IPS_Specimen_Id";
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Clear();
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters["IPS_Specimen_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Infection_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_Status_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_Date", TypeCode.DateTime, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_TimeHours", TypeCode.String, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_TimeMinutes", TypeCode.String, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_Type_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_BedHistory", TypeCode.Int32, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_CreatedBy", TypeCode.String, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_History", TypeCode.String, "");
      SqlDataSource_IPS_Specimen_Form.InsertParameters["IPS_Specimen_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_IPS_Specimen_Form.InsertParameters.Add("IPS_Specimen_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_Specimen_Form.SelectParameters.Clear();
      SqlDataSource_IPS_Specimen_Form.SelectParameters.Add("IPS_Specimen_Id", TypeCode.Int32, Request.QueryString["IPSSpecimenId"]);
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Clear();
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_Status_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_Date", TypeCode.DateTime, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_TimeHours", TypeCode.String, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_TimeMinutes", TypeCode.String, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_Type_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_BedHistory", TypeCode.Int32, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_History", TypeCode.String, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_Specimen_Form.UpdateParameters.Add("IPS_Specimen_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_SpecimenResult()
    {
      SqlDataSource_IPS_SpecimenResult_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_SpecimenResult_List.SelectCommand = "spForm_Get_IPS_SpecimenResult_List";
      SqlDataSource_IPS_SpecimenResult_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_SpecimenResult_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_SpecimenResult_List.SelectParameters.Clear();
      SqlDataSource_IPS_SpecimenResult_List.SelectParameters.Add("IPS_Specimen_Id", TypeCode.String, Request.QueryString["IPSSpecimenId"]);

      SqlDataSource_IPS_SpecimenResult_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_SpecimenResult_Form.InsertCommand = "INSERT INTO Form_IPS_SpecimenResult ( IPS_Specimen_Id ,IPS_SpecimenResult_LabNumber ,IPS_SpecimenResult_CreatedDate ,IPS_SpecimenResult_CreatedBy ,IPS_SpecimenResult_ModifiedDate ,IPS_SpecimenResult_ModifiedBy ,IPS_SpecimenResult_History ,IPS_SpecimenResult_IsActive ) VALUES ( @IPS_Specimen_Id ,@IPS_SpecimenResult_LabNumber ,@IPS_SpecimenResult_CreatedDate ,@IPS_SpecimenResult_CreatedBy ,@IPS_SpecimenResult_ModifiedDate ,@IPS_SpecimenResult_ModifiedBy ,@IPS_SpecimenResult_History ,@IPS_SpecimenResult_IsActive ); SELECT @IPS_SpecimenResult_Id = SCOPE_IDENTITY()";
      SqlDataSource_IPS_SpecimenResult_Form.SelectCommand = "SELECT * FROM Form_IPS_SpecimenResult WHERE (IPS_SpecimenResult_Id = @IPS_SpecimenResult_Id)";
      SqlDataSource_IPS_SpecimenResult_Form.UpdateCommand = "UPDATE Form_IPS_SpecimenResult SET IPS_SpecimenResult_LabNumber = @IPS_SpecimenResult_LabNumber ,IPS_SpecimenResult_ModifiedDate = @IPS_SpecimenResult_ModifiedDate , IPS_SpecimenResult_ModifiedBy = @IPS_SpecimenResult_ModifiedBy , IPS_SpecimenResult_History = @IPS_SpecimenResult_History , IPS_SpecimenResult_IsActive = @IPS_SpecimenResult_IsActive WHERE IPS_SpecimenResult_Id = @IPS_SpecimenResult_Id";
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Clear();
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Add("IPS_SpecimenResult_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters["IPS_SpecimenResult_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Add("IPS_Specimen_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Add("IPS_SpecimenResult_LabNumber", TypeCode.String, "");
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Add("IPS_SpecimenResult_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Add("IPS_SpecimenResult_CreatedBy", TypeCode.String, "");
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Add("IPS_SpecimenResult_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Add("IPS_SpecimenResult_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Add("IPS_SpecimenResult_History", TypeCode.String, "");
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters["IPS_SpecimenResult_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_IPS_SpecimenResult_Form.InsertParameters.Add("IPS_SpecimenResult_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_SpecimenResult_Form.SelectParameters.Clear();
      SqlDataSource_IPS_SpecimenResult_Form.SelectParameters.Add("IPS_SpecimenResult_Id", TypeCode.Int32, Request.QueryString["IPSSpecimenResultId"]);
      SqlDataSource_IPS_SpecimenResult_Form.UpdateParameters.Clear();
      SqlDataSource_IPS_SpecimenResult_Form.UpdateParameters.Add("IPS_SpecimenResult_LabNumber", TypeCode.String, "");
      SqlDataSource_IPS_SpecimenResult_Form.UpdateParameters.Add("IPS_SpecimenResult_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_SpecimenResult_Form.UpdateParameters.Add("IPS_SpecimenResult_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_SpecimenResult_Form.UpdateParameters.Add("IPS_SpecimenResult_History", TypeCode.String, "");
      SqlDataSource_IPS_SpecimenResult_Form.UpdateParameters.Add("IPS_SpecimenResult_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_SpecimenResult_Form.UpdateParameters.Add("IPS_SpecimenResult_Id", TypeCode.Int32, "");

      SqlDataSource_IPS_InsertSpecimenStatusList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertSpecimenStatusList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertSpecimenStatusList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertSpecimenStatusList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertSpecimenStatusList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertSpecimenStatusList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "134");
      SqlDataSource_IPS_InsertSpecimenStatusList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertSpecimenStatusList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertSpecimenStatusList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertSpecimenStatusList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertSpecimenTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertSpecimenTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertSpecimenTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertSpecimenTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertSpecimenTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertSpecimenTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "135");
      SqlDataSource_IPS_InsertSpecimenTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertSpecimenTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertSpecimenTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertSpecimenTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertSpecimenBedHistory.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertSpecimenBedHistory.SelectCommand = "SELECT IPS_BedHistory_Id , 'Ward: ' + ISNULL(IPS_BedHistory_Department,'Not Specified') + '; Room: ' + ISNULL(IPS_BedHistory_Room,'Not Specified') + '; Bed: ' + ISNULL(IPS_BedHistory_Bed,'Not Specified') + '; Date: ' + ISNULL(IPS_BedHistory_Date,'Not Specified') + ';' AS BedHistory FROM Form_IPS_BedHistory WHERE IPS_Infection_Id = @IPS_Infection_Id ORDER BY IPS_BedHistory_Id";
      SqlDataSource_IPS_InsertSpecimenBedHistory.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_InsertSpecimenBedHistory.SelectParameters.Clear();
      SqlDataSource_IPS_InsertSpecimenBedHistory.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);

      SqlDataSource_IPS_EditSpecimenStatusList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditSpecimenStatusList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditSpecimenStatusList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters.Clear();
      SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "134");
      SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditSpecimenStatusList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditSpecimenTypeList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditSpecimenTypeList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditSpecimenTypeList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters.Clear();
      SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "135");
      SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditSpecimenTypeList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditSpecimenBedHistory.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditSpecimenBedHistory.SelectCommand = "SELECT IPS_BedHistory_Id , 'Ward: ' + ISNULL(IPS_BedHistory_Department,'Not Specified') + '; Room: ' + ISNULL(IPS_BedHistory_Room,'Not Specified') + '; Bed: ' + ISNULL(IPS_BedHistory_Bed,'Not Specified') + '; Date: ' + ISNULL(IPS_BedHistory_Date,'Not Specified') + ';' AS BedHistory FROM Form_IPS_BedHistory WHERE IPS_Infection_Id = @IPS_Infection_Id ORDER BY IPS_BedHistory_Id";
      SqlDataSource_IPS_EditSpecimenBedHistory.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_EditSpecimenBedHistory.SelectParameters.Clear();
      SqlDataSource_IPS_EditSpecimenBedHistory.SelectParameters.Add("IPS_Infection_Id", TypeCode.String, Request.QueryString["IPSInfectionId"]);
    }

    private void SqlDataSourceSetup_Organism()
    {
      SqlDataSource_IPS_Organism_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Organism_List.SelectCommand = "spForm_Get_IPS_Organism_List";
      SqlDataSource_IPS_Organism_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Organism_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_Organism_List.SelectParameters.Clear();
      SqlDataSource_IPS_Organism_List.SelectParameters.Add("IPS_SpecimenResult_Id", TypeCode.String, Request.QueryString["IPSSpecimenResultId"]);

      SqlDataSource_IPS_Organism_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Organism_Form.InsertCommand = "INSERT INTO Form_IPS_Organism ( IPS_SpecimenResult_Id ,IPS_Organism_Name_Lookup ,IPS_Organism_Resistance_List , IPS_Organism_AntibiogramNotRequired ,IPS_Organism_Notifiable_DepartmentOfHealth ,IPS_Organism_Notifiable_DepartmentOfHealth_Date ,IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber ,IPS_Organism_CreatedDate ,IPS_Organism_CreatedBy ,IPS_Organism_ModifiedDate ,IPS_Organism_ModifiedBy ,IPS_Organism_History ,IPS_Organism_IsActive ) VALUES ( @IPS_SpecimenResult_Id ,@IPS_Organism_Name_Lookup , @IPS_Organism_Resistance_List , @IPS_Organism_AntibiogramNotRequired ,@IPS_Organism_Notifiable_DepartmentOfHealth ,@IPS_Organism_Notifiable_DepartmentOfHealth_Date ,@IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber ,@IPS_Organism_CreatedDate ,@IPS_Organism_CreatedBy ,@IPS_Organism_ModifiedDate ,@IPS_Organism_ModifiedBy ,@IPS_Organism_History ,@IPS_Organism_IsActive ); SELECT @IPS_Organism_Id = SCOPE_IDENTITY()";
      SqlDataSource_IPS_Organism_Form.SelectCommand = "SELECT * FROM Form_IPS_Organism WHERE (IPS_Organism_Id = @IPS_Organism_Id)";
      SqlDataSource_IPS_Organism_Form.UpdateCommand = "UPDATE Form_IPS_Organism SET IPS_Organism_Name_Lookup = @IPS_Organism_Name_Lookup , IPS_Organism_Resistance_List = @IPS_Organism_Resistance_List , IPS_Organism_AntibiogramNotRequired = @IPS_Organism_AntibiogramNotRequired , IPS_Organism_Notifiable_DepartmentOfHealth = @IPS_Organism_Notifiable_DepartmentOfHealth ,IPS_Organism_Notifiable_DepartmentOfHealth_Date = @IPS_Organism_Notifiable_DepartmentOfHealth_Date ,IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber = @IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber ,IPS_Organism_ModifiedDate = @IPS_Organism_ModifiedDate ,IPS_Organism_ModifiedBy = @IPS_Organism_ModifiedBy ,IPS_Organism_History = @IPS_Organism_History ,IPS_Organism_IsActive = @IPS_Organism_IsActive WHERE IPS_Organism_Id = @IPS_Organism_Id";
      SqlDataSource_IPS_Organism_Form.InsertParameters.Clear();
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters["IPS_Organism_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_SpecimenResult_Id", TypeCode.Int32, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_Name_Lookup", TypeCode.Int32, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_Resistance_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_AntibiogramNotRequired", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_Notifiable_DepartmentOfHealth", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_Notifiable_DepartmentOfHealth_Date", TypeCode.DateTime, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_CreatedBy", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_History", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Form.InsertParameters["IPS_Organism_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_IPS_Organism_Form.InsertParameters.Add("IPS_Organism_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Form.SelectParameters.Clear();
      SqlDataSource_IPS_Organism_Form.SelectParameters.Add("IPS_Organism_Id", TypeCode.Int32, Request.QueryString["IPSOrganismId"]);
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Clear();
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_Name_Lookup", TypeCode.Int32, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_Resistance_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_AntibiogramNotRequired", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_Notifiable_DepartmentOfHealth", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_Notifiable_DepartmentOfHealth_Date", TypeCode.DateTime, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_Notifiable_DepartmentOfHealth_ReferenceNumber", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_History", TypeCode.String, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_Organism_Form.UpdateParameters.Add("IPS_Organism_Id", TypeCode.Int32, "");

      SqlDataSource_IPS_InsertOrganismNameLookup.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertOrganismNameLookup.SelectCommand = "SELECT IPS_Organism_Lookup_Id ,IPS_Organism_Lookup_Description + ' (' + IPS_Organism_Lookup_Code + ')' AS IPS_Organism_Lookup FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_IsActive = 1 ORDER BY IPS_Organism_Lookup_Description";

      SqlDataSource_IPS_InsertOrganismResistanceList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertOrganismResistanceList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertOrganismResistanceList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertOrganismResistanceList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertOrganismResistanceList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertOrganismResistanceList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "138");
      SqlDataSource_IPS_InsertOrganismResistanceList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertOrganismResistanceList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertOrganismResistanceList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertOrganismResistanceList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.SelectParameters.Clear();
      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "139");
      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_InsertOrganismResistanceMechanismItemList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditOrganismNameLookup.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditOrganismNameLookup.SelectCommand = "SELECT IPS_Organism_Lookup_Id , IPS_Organism_Lookup_Description , IPS_Organism_Lookup_Description + ' (' + IPS_Organism_Lookup_Code + ')' AS IPS_Organism_Lookup FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_IsActive = 1 UNION SELECT IPS_Organism_Name_Lookup , IPS_Organism_Lookup_Description , IPS_Organism_Lookup_Description + ' (' + IPS_Organism_Lookup_Code + ')' AS IPS_Organism_Lookup FROM Form_IPS_Organism LEFT JOIN Form_IPS_Organism_Lookup ON Form_IPS_Organism.IPS_Organism_Name_Lookup = Form_IPS_Organism_Lookup.IPS_Organism_Lookup_Id WHERE IPS_Organism_Id = @IPS_Organism_Id ORDER BY IPS_Organism_Lookup_Description";
      SqlDataSource_IPS_EditOrganismNameLookup.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_EditOrganismNameLookup.SelectParameters.Clear();
      SqlDataSource_IPS_EditOrganismNameLookup.SelectParameters.Add("IPS_Organism_Id", TypeCode.String, Request.QueryString["IPSOrganismId"]);

      SqlDataSource_IPS_EditOrganismResistanceList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditOrganismResistanceList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditOrganismResistanceList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters.Clear();
      SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "138");
      SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditOrganismResistanceList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters.Clear();
      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters.Add("Form_Id", TypeCode.String, "37");
      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "139");
      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_IPS_EditOrganismResistanceMechanismItemList.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_IPS_ItemOrganismResistanceMechanism.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_ItemOrganismResistanceMechanism.SelectCommand = "SELECT DISTINCT IPS_RM_Mechanism_Item_Name FROM vForm_IPS_Organism_ResistanceMechanism WHERE IPS_Organism_Id = @IPS_Organism_Id ORDER BY IPS_RM_Mechanism_Item_Name";
      SqlDataSource_IPS_ItemOrganismResistanceMechanism.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_IPS_ItemOrganismResistanceMechanism.SelectParameters.Clear();
      SqlDataSource_IPS_ItemOrganismResistanceMechanism.SelectParameters.Add("IPS_Organism_Id", TypeCode.String, Request.QueryString["IPSOrganismId"]);
    }

    private void SqlDataSourceSetup_Antibiogram()
    {
      SqlDataSource_IPS_Antibiogram.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Antibiogram.SelectCommand = "SELECT * FROM Form_IPS_Antibiogram WHERE IPS_Organism_Id = @IPS_Organism_Id ORDER BY IPS_Antibiogram_Id";
      SqlDataSource_IPS_Antibiogram.UpdateCommand = "UPDATE Form_IPS_Antibiogram SET IPS_Antibiogram_Name_Lookup = @IPS_Antibiogram_Name_Lookup ,IPS_Antibiogram_SRI_List = @IPS_Antibiogram_SRI_List ,IPS_Antibiogram_ModifiedDate = @IPS_Antibiogram_ModifiedDate ,IPS_Antibiogram_ModifiedBy = @IPS_Antibiogram_ModifiedBy ,IPS_Antibiogram_History = @IPS_Antibiogram_History ,IPS_Antibiogram_IsActive = @IPS_Antibiogram_IsActive WHERE IPS_Antibiogram_Id = @IPS_Antibiogram_Id";
      SqlDataSource_IPS_Antibiogram.SelectParameters.Clear();
      SqlDataSource_IPS_Antibiogram.SelectParameters.Add("IPS_Organism_Id", TypeCode.Int32, Request.QueryString["IPSOrganismId"]);
      SqlDataSource_IPS_Antibiogram.UpdateParameters.Clear();
      SqlDataSource_IPS_Antibiogram.UpdateParameters.Add("IPS_Antibiogram_Name_Lookup", TypeCode.Int32, "");
      SqlDataSource_IPS_Antibiogram.UpdateParameters.Add("IPS_Antibiogram_SRI_List", TypeCode.Int32, "");
      SqlDataSource_IPS_Antibiogram.UpdateParameters.Add("IPS_Antibiogram_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_IPS_Antibiogram.UpdateParameters.Add("IPS_Antibiogram_ModifiedBy", TypeCode.String, "");
      SqlDataSource_IPS_Antibiogram.UpdateParameters.Add("IPS_Antibiogram_History", TypeCode.String, "");
      SqlDataSource_IPS_Antibiogram.UpdateParameters.Add("IPS_Antibiogram_IsActive", TypeCode.Boolean, "");
      SqlDataSource_IPS_Antibiogram.UpdateParameters.Add("IPS_Antibiogram_Id", TypeCode.Int32, "");

      SqlDataSource_IPS_Antibiogram_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_Antibiogram_List.SelectCommand = "spForm_Get_IPS_Antibiogram_List";
      SqlDataSource_IPS_Antibiogram_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_IPS_Antibiogram_List.CancelSelectOnNullParameter = false;
      SqlDataSource_IPS_Antibiogram_List.SelectParameters.Clear();
      SqlDataSource_IPS_Antibiogram_List.SelectParameters.Add("IPS_Organism_Id", TypeCode.String, Request.QueryString["IPSOrganismId"]);
    }

    protected void PageTitle()
    {
      Label_Title.Text = Convert.ToString(InfoQuestWCF.InfoQuest_All.All_FormName("37") + " : Specimen", CultureInfo.CurrentCulture);
      Label_InfoHeading.Text = Convert.ToString("Visit and Infection Information", CultureInfo.CurrentCulture);
      Label_SpecimenHeading.Text = Convert.ToString("Specimens", CultureInfo.CurrentCulture);

      if (Request.QueryString["IPSSpecimenId"] == null)
      {
        Label_CurrentSpecimenHeading.Text = Convert.ToString("New Specimen", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_CurrentSpecimenHeading.Text = Convert.ToString("Selected Specimen", CultureInfo.CurrentCulture);
      }

      Label_SpecimenResultHeading.Text = Convert.ToString("Laboratory Results", CultureInfo.CurrentCulture);

      if (Request.QueryString["IPSSpecimenResultId"] == null)
      {
        Label_CurrentSpecimenResultHeading.Text = Convert.ToString("New Laboratory Result", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_CurrentSpecimenResultHeading.Text = Convert.ToString("Selected Laboratory Result", CultureInfo.CurrentCulture);
      }

      Label_OrganismHeading.Text = Convert.ToString("Organism", CultureInfo.CurrentCulture);

      if (Request.QueryString["IPSOrganismId"] == null)
      {
        Label_CurrentOrganismHeading.Text = Convert.ToString("New Organism", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_CurrentOrganismHeading.Text = Convert.ToString("Selected Organism", CultureInfo.CurrentCulture);
      }

      Label_AntibiogramHeading.Text = Convert.ToString("Antibiogram", CultureInfo.CurrentCulture);
      Label_AntibiogramListHeading.Text = Convert.ToString("Antibiogram", CultureInfo.CurrentCulture);

      if (Request.QueryString["IPSInfectionId"] == null)
      {
        Label_CurrentInfectionCompleteHeading.Text = Convert.ToString("Selected Infection Completed", CultureInfo.CurrentCulture);
      }
      else
      {
        FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Request.QueryString["IPSInfectionId"]);
        string IsHAI = FromDataBase_IsHAI_Current.IsHAI;

        if (IsHAI == "True")
        {
          Label_CurrentInfectionCompleteHeading.Text = Convert.ToString("Selected Infection Completed and HAI Investigation", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_CurrentInfectionCompleteHeading.Text = Convert.ToString("Selected Infection Completed", CultureInfo.CurrentCulture);
        }
      }
    }

    protected void PageSpecimenTable()
    {
      FromDataBase_SpecimenStatusList FromDataBase_SpecimenStatusList_Current = GetSpecimenStatusList();
      string IPSSpecimenStatusList = FromDataBase_SpecimenStatusList_Current.IPSSpecimenStatusList;

      TableSpecimen.Visible = true;
      TableCurrentSpecimen.Visible = true;

      if (IPSSpecimenStatusList == "4941")
      {
        TableSpecimenResult.Visible = false; DivSpecimenResult.Visible = false;
        TableCurrentSpecimenResult.Visible = false; DivCurrentSpecimenResult.Visible = false;
      }
      else
      {
        FromDataBase_SpecimenIsActive FromDataBase_SpecimenIsActive_Current = GetSpecimenIsActive();
        string IPSSpecimenIsActive = FromDataBase_SpecimenIsActive_Current.IPSSpecimenIsActive;

        if (IPSSpecimenIsActive == "False")
        {
          TableSpecimenResult.Visible = false; DivSpecimenResult.Visible = false;
          TableCurrentSpecimenResult.Visible = false; DivCurrentSpecimenResult.Visible = false;
        }
        else
        {
          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
          {
            TableSpecimenResult.Visible = true; DivSpecimenResult.Visible = true;
            TableCurrentSpecimenResult.Visible = true; DivCurrentSpecimenResult.Visible = true;
          }
          else
          {
            if (Request.QueryString["IPSSpecimenResultId"] == null)
            {
              TableSpecimenResult.Visible = true; DivSpecimenResult.Visible = true;
              TableCurrentSpecimenResult.Visible = false; DivCurrentSpecimenResult.Visible = false;
            }
            else
            {
              TableSpecimenResult.Visible = true; DivSpecimenResult.Visible = true;
              TableCurrentSpecimenResult.Visible = true; DivCurrentSpecimenResult.Visible = true;
            }
          }

          SetCurrentSpecimenResultVisibility();

          TableCurrentSpecimenResultVisible();
        }
      }
    }

    protected void PageSpecimenResultTable()
    {
      FromDataBase_SpecimenStatusList FromDataBase_SpecimenStatusList_Current = GetSpecimenStatusList();
      string IPSSpecimenStatusList = FromDataBase_SpecimenStatusList_Current.IPSSpecimenStatusList;

      if (IPSSpecimenStatusList == "5019")
      {
        TableOrganism.Visible = false; DivOrganism.Visible = false;
        TableCurrentOrganism.Visible = false; DivCurrentOrganism.Visible = false;
      }
      else
      {
        FromDataBase_SpecimenResultIsActive FromDataBase_SpecimenResultIsActive_Current = GetSpecimenResultIsActive();
        string IPSSpecimenResultIsActive = FromDataBase_SpecimenResultIsActive_Current.IPSSpecimenResultIsActive;

        if (IPSSpecimenResultIsActive == "False")
        {
          TableOrganism.Visible = false; DivOrganism.Visible = false;
          TableCurrentOrganism.Visible = false; DivCurrentOrganism.Visible = false;
        }
        else
        {
          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
          {
            TableOrganism.Visible = true; DivOrganism.Visible = true;
            TableCurrentOrganism.Visible = true; DivCurrentOrganism.Visible = true;
          }
          else
          {
            if (Request.QueryString["IPSOrganismId"] == null)
            {
              TableOrganism.Visible = true; DivOrganism.Visible = true;
              TableCurrentOrganism.Visible = false; DivCurrentOrganism.Visible = false;
            }
            else
            {
              TableOrganism.Visible = true; DivOrganism.Visible = true;
              TableCurrentOrganism.Visible = true; DivCurrentOrganism.Visible = true;
            }
          }

          SetCurrentOrganismVisibility();

          TableCurrentOrganismVisible();
        }
      }
    }

    protected void PageOrganismTable()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      FromDataBase_SpecimenIsActive FromDataBase_SpecimenIsActive_Current = GetSpecimenIsActive();
      string IPSSpecimenIsActive = FromDataBase_SpecimenIsActive_Current.IPSSpecimenIsActive;

      FromDataBase_SpecimenResultIsActive FromDataBase_SpecimenResultIsActive_Current = GetSpecimenResultIsActive();
      string IPSSpecimenResultIsActive = FromDataBase_SpecimenResultIsActive_Current.IPSSpecimenResultIsActive;

      FromDataBase_OrganismIsActive FromDataBase_OrganismIsActive_Current = GetOrganismIsActive();
      string IPSOrganismAntibiogramNotRequired = FromDataBase_OrganismIsActive_Current.IPSOrganismAntibiogramNotRequired;
      string IPSOrganismIsActive = FromDataBase_OrganismIsActive_Current.IPSOrganismIsActive;


      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
        {
          if (IPSSpecimenIsActive == "True")
          {
            if (IPSSpecimenResultIsActive == "True")
            {
              if (IPSOrganismIsActive == "True")
              {
                if (IPSOrganismAntibiogramNotRequired == "True")
                {
                  TableAntibiogram.Visible = false; DivAntibiogram.Visible = false;
                  TableAntibiogramList.Visible = false;
                }
                else
                {
                  TableAntibiogram.Visible = true; DivAntibiogram.Visible = true;
                  TableAntibiogramList.Visible = false;
                }
              }
              else
              {
                TableAntibiogram.Visible = false; DivAntibiogram.Visible = false;
                TableAntibiogramList.Visible = false;
              }
            }
            else
            {
              TableAntibiogram.Visible = false; DivAntibiogram.Visible = true;
              TableAntibiogramList.Visible = true;
            }
          }
          else
          {
            TableAntibiogram.Visible = false; DivAntibiogram.Visible = true;
            TableAntibiogramList.Visible = true;
          }
        }
        else
        {
          TableAntibiogram.Visible = false; DivAntibiogram.Visible = true;
          TableAntibiogramList.Visible = true;
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";

        if (IPSOrganismAntibiogramNotRequired == "False")
        {
          TableAntibiogram.Visible = false; DivAntibiogram.Visible = true;
          TableAntibiogramList.Visible = true;
        }
        else
        {
          TableAntibiogram.Visible = false; DivAntibiogram.Visible = false;
          TableAntibiogramList.Visible = false;
        }
      }

      if (Security == "1")
      {
        Security = "0";

        if (IPSOrganismAntibiogramNotRequired == "False")
        {
          TableAntibiogram.Visible = false; DivAntibiogram.Visible = true;
          TableAntibiogramList.Visible = true;
        }
        else
        {
          TableAntibiogram.Visible = false; DivAntibiogram.Visible = false;
          TableAntibiogramList.Visible = false;
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

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (SecurityRole_Id = '1' OR Form_Id IN ('37')) AND (Facility_Id IN (SELECT Facility_Id FROM Form_IPS_VisitInformation WHERE IPS_VisitInformation_Id = @IPS_VisitInformation_Id) OR (SecurityRole_Rank = 1))";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_UserName", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@IPS_VisitInformation_Id", Request.QueryString["IPSVisitInformationId"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            FromDataBase_SecurityRole_New.SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            FromDataBase_SecurityRole_New.SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '22'");
            FromDataBase_SecurityRole_New.SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '155'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '11'");
            FromDataBase_SecurityRole_New.SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '10'");
          }
        }
      }

      return FromDataBase_SecurityRole_New;
    }

    private class FromDataBase_InfectionCompleted
    {
      public string IPSInfectionCompleted { get; set; }
      public string IPSInfectionIsActive { get; set; }
    }

    private FromDataBase_InfectionCompleted GetInfectionCompleted()
    {
      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_New = new FromDataBase_InfectionCompleted();

      string SQLStringInfection = "SELECT IPS_Infection_Completed , IPS_Infection_IsActive FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              FromDataBase_InfectionCompleted_New.IPSInfectionCompleted = DataRow_Row["IPS_Infection_Completed"].ToString();
              FromDataBase_InfectionCompleted_New.IPSInfectionIsActive = DataRow_Row["IPS_Infection_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_InfectionCompleted_New;
    }

    private class FromDataBase_SpecimenCompleted
    {
      public string Specimen { get; set; }
    }

    private FromDataBase_SpecimenCompleted GetSpecimenCompleted()
    {
      FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_New = new FromDataBase_SpecimenCompleted();

      string SQLStringSpecimen = "EXECUTE spForm_Get_IPS_SpecimenCompleted @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Specimen = new SqlCommand(SQLStringSpecimen))
      {
        SqlCommand_Specimen.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Specimen;
        using (DataTable_Specimen = new DataTable())
        {
          DataTable_Specimen.Locale = CultureInfo.CurrentCulture;
          DataTable_Specimen = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Specimen).Copy();
          if (DataTable_Specimen.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Specimen.Rows)
            {
              FromDataBase_SpecimenCompleted_New.Specimen = DataRow_Row["Specimen"].ToString();
            }
          }
        }
      }

      return FromDataBase_SpecimenCompleted_New;
    }

    private class FromDataBase_IsHAI
    {
      public string IsHAI { get; set; }
    }

    private static FromDataBase_IsHAI GetIsHAI(string ipsInfectionId)
    {
      FromDataBase_IsHAI FromDataBase_IsHAI_New = new FromDataBase_IsHAI();

      string SQLStringIPSInfectionHAI = "SELECT CASE WHEN IPS_Infection_Category_List IN (4799) THEN 'True' ELSE 'False' END AS IsHAI FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_IPSInfectionHAI = new SqlCommand(SQLStringIPSInfectionHAI))
      {
        SqlCommand_IPSInfectionHAI.Parameters.AddWithValue("@IPS_Infection_Id", ipsInfectionId);
        DataTable DataTable_IPSInfectionHAI;
        using (DataTable_IPSInfectionHAI = new DataTable())
        {
          DataTable_IPSInfectionHAI.Locale = CultureInfo.CurrentCulture;
          DataTable_IPSInfectionHAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IPSInfectionHAI).Copy();
          if (DataTable_IPSInfectionHAI.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_IPSInfectionHAI.Rows)
            {
              FromDataBase_IsHAI_New.IsHAI = DataRow_Row["IsHAI"].ToString();
            }
          }
        }
      }

      return FromDataBase_IsHAI_New;
    }

    private class FromDataBase_HAICompleted
    {
      public string IPSHAICompleted { get; set; }
    }

    private FromDataBase_HAICompleted GetHAICompleted()
    {
      FromDataBase_HAICompleted FromDataBase_HAICompleted_New = new FromDataBase_HAICompleted();

      string SQLStringHAI = "SELECT IPS_HAI_Investigation_Completed FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_HAI = new SqlCommand(SQLStringHAI))
      {
        SqlCommand_HAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_HAI;
        using (DataTable_HAI = new DataTable())
        {
          DataTable_HAI.Locale = CultureInfo.CurrentCulture;
          DataTable_HAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAI).Copy();
          if (DataTable_HAI.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HAI.Rows)
            {
              FromDataBase_HAICompleted_New.IPSHAICompleted = DataRow_Row["IPS_HAI_Investigation_Completed"].ToString();
            }
          }
        }
      }

      return FromDataBase_HAICompleted_New;
    }

    private class FromDataBase_InfectionSite
    {
      public string IPSInfectionSiteInfectionIsActive { get; set; }
    }

    private FromDataBase_InfectionSite GetInfectionSite()
    {
      FromDataBase_InfectionSite FromDataBase_InfectionSite_New = new FromDataBase_InfectionSite();

      string SQLStringInfection = "SELECT CASE WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4996 THEN 'True' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'False' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4997 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4996') THEN 'False' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND (vForm_IPS_Infection.IPS_Infection_Site_Infection_IsActive = 0 OR vForm_IPS_Infection.IPS_Infection_Site_Infection_Category_List != 4799) THEN 'False' WHEN vForm_IPS_Infection.IPS_Infection_Site_List = 4998 AND IPS_Infection_Site_Infection_Site_List NOT LIKE ('4997') THEN 'False' ELSE 'True' END	AS IPS_Infection_Site_Infection_IsActive FROM vForm_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              FromDataBase_InfectionSite_New.IPSInfectionSiteInfectionIsActive = DataRow_Row["IPS_Infection_Site_Infection_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_InfectionSite_New;
    }

    private class FromDataBase_SpecimenIsActive
    {
      public string IPSSpecimenIsActive { get; set; }
    }

    private FromDataBase_SpecimenIsActive GetSpecimenIsActive()
    {
      FromDataBase_SpecimenIsActive FromDataBase_SpecimenIsActive_New = new FromDataBase_SpecimenIsActive();

      string SQLStringSpecimen = "SELECT IPS_Specimen_IsActive FROM Form_IPS_Specimen WHERE IPS_Specimen_Id = @IPS_Specimen_Id";
      using (SqlCommand SqlCommand_Specimen = new SqlCommand(SQLStringSpecimen))
      {
        SqlCommand_Specimen.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
        DataTable DataTable_Specimen;
        using (DataTable_Specimen = new DataTable())
        {
          DataTable_Specimen.Locale = CultureInfo.CurrentCulture;
          DataTable_Specimen = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Specimen).Copy();
          if (DataTable_Specimen.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Specimen.Rows)
            {
              FromDataBase_SpecimenIsActive_New.IPSSpecimenIsActive = DataRow_Row["IPS_Specimen_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_SpecimenIsActive_New;
    }

    private class FromDataBase_SpecimenStatusList
    {
      public string IPSSpecimenStatusList { get; set; }
    }

    private FromDataBase_SpecimenStatusList GetSpecimenStatusList()
    {
      FromDataBase_SpecimenStatusList FromDataBase_SpecimenStatusList_New = new FromDataBase_SpecimenStatusList();

      string SQLStringSpecimen = "SELECT IPS_Specimen_Status_List FROM Form_IPS_Specimen WHERE IPS_Specimen_Id = @IPS_Specimen_Id";
      using (SqlCommand SqlCommand_Specimen = new SqlCommand(SQLStringSpecimen))
      {
        SqlCommand_Specimen.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
        DataTable DataTable_Specimen;
        using (DataTable_Specimen = new DataTable())
        {
          DataTable_Specimen.Locale = CultureInfo.CurrentCulture;
          DataTable_Specimen = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Specimen).Copy();
          if (DataTable_Specimen.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Specimen.Rows)
            {
              FromDataBase_SpecimenStatusList_New.IPSSpecimenStatusList = DataRow_Row["IPS_Specimen_Status_List"].ToString();
            }
          }
        }
      }

      return FromDataBase_SpecimenStatusList_New;
    }

    private class FromDataBase_SpecimenResultIsActive
    {
      public string IPSSpecimenResultIsActive { get; set; }
    }

    private FromDataBase_SpecimenResultIsActive GetSpecimenResultIsActive()
    {
      FromDataBase_SpecimenResultIsActive FromDataBase_SpecimenResultIsActive_New = new FromDataBase_SpecimenResultIsActive();

      string SQLStringSpecimenResult = "SELECT IPS_SpecimenResult_IsActive FROM Form_IPS_SpecimenResult WHERE IPS_SpecimenResult_Id = @IPS_SpecimenResult_Id";
      using (SqlCommand SqlCommand_SpecimenResult = new SqlCommand(SQLStringSpecimenResult))
      {
        SqlCommand_SpecimenResult.Parameters.AddWithValue("@IPS_SpecimenResult_Id", Request.QueryString["IPSSpecimenResultId"]);
        DataTable DataTable_SpecimenResult;
        using (DataTable_SpecimenResult = new DataTable())
        {
          DataTable_SpecimenResult.Locale = CultureInfo.CurrentCulture;
          DataTable_SpecimenResult = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SpecimenResult).Copy();
          if (DataTable_SpecimenResult.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SpecimenResult.Rows)
            {
              FromDataBase_SpecimenResultIsActive_New.IPSSpecimenResultIsActive = DataRow_Row["IPS_SpecimenResult_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_SpecimenResultIsActive_New;
    }

    private class FromDataBase_OrganismIsActive
    {
      public string IPSOrganismAntibiogramNotRequired { get; set; }
      public string IPSOrganismIsActive { get; set; }
    }

    private FromDataBase_OrganismIsActive GetOrganismIsActive()
    {
      FromDataBase_OrganismIsActive FromDataBase_OrganismIsActive_New = new FromDataBase_OrganismIsActive();

      string SQLStringOrganism = "SELECT IPS_Organism_AntibiogramNotRequired , IPS_Organism_IsActive FROM Form_IPS_Organism WHERE IPS_Organism_Id = @IPS_Organism_Id";
      using (SqlCommand SqlCommand_Organism = new SqlCommand(SQLStringOrganism))
      {
        SqlCommand_Organism.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
        DataTable DataTable_Organism;
        using (DataTable_Organism = new DataTable())
        {
          DataTable_Organism.Locale = CultureInfo.CurrentCulture;
          DataTable_Organism = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Organism).Copy();
          if (DataTable_Organism.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Organism.Rows)
            {
              FromDataBase_OrganismIsActive_New.IPSOrganismAntibiogramNotRequired = DataRow_Row["IPS_Organism_AntibiogramNotRequired"].ToString();
              FromDataBase_OrganismIsActive_New.IPSOrganismIsActive = DataRow_Row["IPS_Organism_IsActive"].ToString();
            }
          }
        }
      }

      return FromDataBase_OrganismIsActive_New;
    }


    //--START-- --VisitInfo--//
    private void TableInfoVisible()
    {
      Session["IPSInfectionId"] = "";
      Session["FacilityFacilityDisplayName"] = "";
      Session["IPSVisitInformationVisitNumber"] = "";
      Session["PatientInformationName"] = "";
      Session["PatientInformationSurname"] = "";
      Session["IPSInfectionReportNumber"] = "";
      Session["IPSInfectionCategoryName"] = "";
      Session["IPSInfectionTypeName"] = "";
      Session["IPSInfectionCompleted"] = "";
      Session["IPSInfectionModifiedDate"] = "";
      Session["IPSInfectionModifiedBy"] = "";
      Session["IPSInfectionHistory"] = "";
      Session["IPSInfectionIsActive"] = "";
      Session["IPSHAIId"] = "";
      Session["IPSHAIModifiedDate"] = "";
      Session["Specimen"] = "";
      Session["Infection"] = "";
      Session["HAI"] = "";
      string SQLStringVisitInfo = "EXECUTE spForm_Get_IPS_InfectionInformation @IPS_Infection_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_VisitInfo;
        using (DataTable_VisitInfo = new DataTable())
        {
          DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
          if (DataTable_VisitInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
            {
              Session["IPSInfectionId"] = DataRow_Row["IPS_Infection_Id"];
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["IPSVisitInformationVisitNumber"] = DataRow_Row["IPS_VisitInformation_VisitNumber"];
              Session["PatientInformationName"] = DataRow_Row["PatientInformation_Name"];
              Session["PatientInformationSurname"] = DataRow_Row["PatientInformation_Surname"];
              Session["IPSInfectionReportNumber"] = DataRow_Row["IPS_Infection_ReportNumber"];
              Session["IPSInfectionCategoryName"] = DataRow_Row["IPS_Infection_Category_Name"];
              Session["IPSInfectionTypeName"] = DataRow_Row["IPS_Infection_Type_Name"];
              Session["IPSInfectionCompleted"] = DataRow_Row["IPS_Infection_Completed"];
              Session["IPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["IPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSInfectionIsActive"] = DataRow_Row["IPS_Infection_IsActive"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
              Session["IPSHAIModifiedDate"] = DataRow_Row["IPS_HAI_ModifiedDate"];
              Session["Specimen"] = DataRow_Row["Specimen"];
              Session["Infection"] = DataRow_Row["Infection"];
              Session["HAI"] = DataRow_Row["HAI"];
            }
          }
        }
      }

      Label_IFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_IVisitNumber.Text = Session["IPSVisitInformationVisitNumber"].ToString();
      Label_IName.Text = Session["PatientInformationSurname"].ToString() + Convert.ToString(", ", CultureInfo.CurrentCulture) + Session["PatientInformationName"].ToString();
      Label_IInfectionReportNumber.Text = Session["IPSInfectionReportNumber"].ToString();
      Label_IInfectionCategoryName.Text = Session["IPSInfectionCategoryName"].ToString();
      Label_IInfectionTypeName.Text = Session["IPSInfectionTypeName"].ToString();
      Label_IInfectionCompleted.Text = Session["IPSInfectionCompleted"].ToString();
      Label_IHAI.Text = Session["HAI"].ToString();
      Label_ISpecimen.Text = Session["Specimen"].ToString();

      Session.Remove("IPSInfectionId");
      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("IPSVisitInformationVisitNumber");
      Session.Remove("PatientInformationName");
      Session.Remove("PatientInformationSurname");
      Session.Remove("IPSInfectionReportNumber");
      Session.Remove("IPSInfectionCategoryName");
      Session.Remove("IPSInfectionTypeName");
      Session.Remove("IPSInfectionCompleted");
      Session.Remove("IPSInfectionModifiedDate");
      Session.Remove("IPSInfectionModifiedBy");
      Session.Remove("IPSInfectionHistory");
      Session.Remove("IPSInfectionIsActive");
      Session.Remove("IPSHAIId");
      Session.Remove("IPSHAIModifiedDate");
      Session.Remove("Specimen");
      Session.Remove("Infection");
      Session.Remove("HAI");
    }

    protected void Button_InfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }
    //---END--- --VisitInfo--//


    //--START-- --Specimen--//
    protected void SqlDataSource_IPS_Specimen_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenSpecimenTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_Specimen_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }


      for (int i = 0; i < GridView_IPS_Specimen_List.Rows.Count; i++)
      {
        if (GridView_IPS_Specimen_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_IPS_Specimen_List.Rows[i].Cells[5].Text == "No")
          {
            GridView_IPS_Specimen_List.Rows[i].Cells[5].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_Specimen_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_IPS_Specimen_List.Rows[i].Cells[5].Text == "Yes")
          {
            GridView_IPS_Specimen_List.Rows[i].Cells[5].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_Specimen_List.Rows[i].Cells[5].ForeColor = Color.FromName("#333333");
          }


          if (GridView_IPS_Specimen_List.Rows[i].Cells[5].Text == "Yes")
          {
            if (GridView_IPS_Specimen_List.Rows[i].Cells[6].Text == "Incomplete")
            {
              GridView_IPS_Specimen_List.Rows[i].Cells[6].BackColor = Color.FromName("#d46e6e");
              GridView_IPS_Specimen_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
            }
            else
            {
              GridView_IPS_Specimen_List.Rows[i].Cells[6].BackColor = Color.FromName("#77cf9c");
              GridView_IPS_Specimen_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
            }
          }
          else if (GridView_IPS_Specimen_List.Rows[i].Cells[5].Text == "No")
          {
            GridView_IPS_Specimen_List.Rows[i].Cells[6].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_Specimen_List.Rows[i].Cells[6].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_IPS_Specimen_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_SpecimenTotalRecords = (Label)e.Row.FindControl("Label_SpecimenTotalRecords");
          Label_SpecimenTotalRecords.Text = Label_HiddenSpecimenTotalRecords.Text;
        }


        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_CaptureNewSpecimen = (Button)e.Row.FindControl("Button_CaptureNewSpecimen");

          FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
          DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
          DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
          DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
          DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
          DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
          {
            Security = "0";
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
            {
              Button_CaptureNewSpecimen.Visible = true;
            }
            else
            {
              Button_CaptureNewSpecimen.Visible = false;
            }
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
          {
            Security = "0";
            Button_CaptureNewSpecimen.Visible = false;
          }

          if (Security == "1")
          {
            Security = "0";
            Button_CaptureNewSpecimen.Visible = false;
          }
        }
      }
    }

    protected void Button_CaptureNewSpecimen_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentSpecimen"), false);
    }

    protected void Button_SpecimenAll_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_SpecimenAll.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"]), false);
    }

    public string GetSpecimenLink(object ips_Specimen_Id)
    {
      string FinalURL = "";
      if (ips_Specimen_Id != null)
      {
        if (ips_Specimen_Id.ToString() == Request.QueryString["IPSSpecimenId"])
        {
          FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + ips_Specimen_Id) + "#CurrentSpecimen'>Selected</a>";
        }
        else
        {
          FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + ips_Specimen_Id) + "#CurrentSpecimen'>Select</a>";
        }
      }

      return FinalURL;
    }
    //---END--- --Specimen--//


    //--START-- --CurrentSpecimen--//
    protected void SetTableCurrentSpecimenVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["IPSSpecimenId"]))
      {
        SetTableCurrentSpecimenVisibility_Insert();
      }
      else
      {
        SetTableCurrentSpecimenVisibility_Edit();
      }
    }

    protected void SetTableCurrentSpecimenVisibility_Insert()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
        {
          FormView_IPS_Specimen_Form.ChangeMode(FormViewMode.Insert);
        }
        else
        {
          FormView_IPS_Specimen_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_IPS_Specimen_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_IPS_Specimen_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetTableCurrentSpecimenVisibility_Edit()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
        {
          FormView_IPS_Specimen_Form.ChangeMode(FormViewMode.Edit);
        }
        else
        {
          FormView_IPS_Specimen_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_IPS_Specimen_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_IPS_Specimen_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentSpecimenVisible()
    {
      if (FormView_IPS_Specimen_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_InsertSpecimenStatusList")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
        ((TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_InsertSpecimenDate")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
        ((TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_InsertSpecimenDate")).Attributes.Add("OnInput", "Validation_SpecimenForm();");
        ((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_InsertSpecimenTimeHours")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
        ((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_InsertSpecimenTimeMinutes")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
        ((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_InsertSpecimenTypeList")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
      }

      if (FormView_IPS_Specimen_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenStatusList")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
        ((TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_EditSpecimenDate")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
        ((TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_EditSpecimenDate")).Attributes.Add("OnInput", "Validation_SpecimenForm();");
        ((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenTimeHours")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
        ((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenTimeMinutes")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
        ((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenTypeList")).Attributes.Add("OnChange", "Validation_SpecimenForm();");
      }
    }


    protected void FormView_IPS_Specimen_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_Specimen();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentSpecimen);

          ((Label)FormView_IPS_Specimen_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_IPS_Specimen_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          SqlDataSource_IPS_Specimen_Form.InsertParameters["IPS_Infection_Id"].DefaultValue = Request.QueryString["IPSInfectionId"];

          SqlDataSource_IPS_Specimen_Form.InsertParameters["IPS_Specimen_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_Specimen_Form.InsertParameters["IPS_Specimen_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_Specimen_Form.InsertParameters["IPS_Specimen_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_Specimen_Form.InsertParameters["IPS_Specimen_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_Specimen_Form.InsertParameters["IPS_Specimen_History"].DefaultValue = "";
          SqlDataSource_IPS_Specimen_Form.InsertParameters["IPS_Specimen_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation_Specimen()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_InsertSpecimenStatusList = (DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_InsertSpecimenStatusList");
      TextBox TextBox_InsertSpecimenDate = (TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_InsertSpecimenDate");
      DropDownList DropDownList_InsertSpecimenTimeHours = (DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_InsertSpecimenTimeHours");
      DropDownList DropDownList_InsertSpecimenTimeMinutes = (DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_InsertSpecimenTimeMinutes");
      DropDownList DropDownList_InsertSpecimenTypeList = (DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_InsertSpecimenTypeList");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertSpecimenStatusList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertSpecimenDate.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertSpecimenTimeHours.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertSpecimenTimeMinutes.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_InsertSpecimenTypeList.SelectedValue))
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
        InvalidFormMessage = InsertFieldValidation_Specimen();
      }

      return InvalidFormMessage;
    }

    protected string InsertFieldValidation_Specimen()
    {
      string InvalidFormMessage = "";

      TextBox TextBox_InsertSpecimenDate = (TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_InsertSpecimenDate");

      string DateToValidateSpecimenDate = TextBox_InsertSpecimenDate.Text.ToString();
      DateTime ValidatedDateSpecimenDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateSpecimenDate);

      if (ValidatedDateSpecimenDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + "Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
      }
      else
      {
        DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_InsertSpecimenDate")).Text, CultureInfo.CurrentCulture);
        DateTime CurrentDate = DateTime.Now;

        if (PickedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_Specimen_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["IPS_Specimen_Id"] = e.Command.Parameters["@IPS_Specimen_Id"].Value;
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Session["IPS_Specimen_Id"] + "#CurrentSpecimen"), false);
      }
    }


    protected void FormView_IPS_Specimen_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIPSSpecimenModifiedDate"] = e.OldValues["IPS_Specimen_ModifiedDate"];
        object OLDIPSSpecimenModifiedDate = Session["OLDIPSSpecimenModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSSpecimenModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareIPSSpecimen = (DataView)SqlDataSource_IPS_Specimen_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareIPSSpecimen = DataView_CompareIPSSpecimen[0];
        Session["DBIPSSpecimenModifiedDate"] = Convert.ToString(DataRowView_CompareIPSSpecimen["IPS_Specimen_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIPSSpecimenModifiedBy"] = Convert.ToString(DataRowView_CompareIPSSpecimen["IPS_Specimen_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIPSSpecimenModifiedDate = Session["DBIPSSpecimenModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIPSSpecimenModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentSpecimen);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSSpecimenModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_IPS_Specimen_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_IPS_Specimen_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_Specimen();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentSpecimen);
            ((Label)FormView_IPS_Specimen_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_IPS_Specimen_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;

            e.NewValues["IPS_Specimen_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["IPS_Specimen_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Specimen", "IPS_Specimen_Id = " + Request.QueryString["IPSSpecimenId"]);

            DataView DataView_IPSSpecimen = (DataView)SqlDataSource_IPS_Specimen_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_IPSSpecimen = DataView_IPSSpecimen[0];
            Session["IPSSpecimenHistory"] = Convert.ToString(DataRowView_IPSSpecimen["IPS_Specimen_History"], CultureInfo.CurrentCulture);

            Session["IPSSpecimenHistory"] = Session["History"].ToString() + Session["IPSSpecimenHistory"].ToString();
            e.NewValues["IPS_Specimen_History"] = Session["IPSSpecimenHistory"].ToString();

            Session.Remove("IPSSpecimenHistory");
            Session.Remove("History");
          }
        }
      }
    }

    protected string EditValidation_Specimen()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_EditSpecimenStatusList = (DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenStatusList");
      TextBox TextBox_EditSpecimenDate = (TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_EditSpecimenDate");
      DropDownList DropDownList_EditSpecimenTimeHours = (DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenTimeHours");
      DropDownList DropDownList_EditSpecimenTimeMinutes = (DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenTimeMinutes");
      DropDownList DropDownList_EditSpecimenTypeList = (DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenTypeList");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_EditSpecimenStatusList.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditSpecimenDate.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditSpecimenTimeHours.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditSpecimenTimeMinutes.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(DropDownList_EditSpecimenTypeList.SelectedValue))
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
        InvalidFormMessage = EditFieldValidation_Specimen();
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation_Specimen()
    {
      string InvalidFormMessage = "";

      TextBox TextBox_EditSpecimenDate = (TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_EditSpecimenDate");

      string DateToValidateSpecimenDate = TextBox_EditSpecimenDate.Text.ToString();
      DateTime ValidatedDateSpecimenDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateSpecimenDate);

      if (ValidatedDateSpecimenDate.ToString() == "0001/01/01 12:00:00 AM")
      {
        InvalidFormMessage = InvalidFormMessage + "Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
      }
      else
      {
        DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_IPS_Specimen_Form.FindControl("TextBox_EditSpecimenDate")).Text, CultureInfo.CurrentCulture);
        DateTime CurrentDate = DateTime.Now;

        if (PickedDate.CompareTo(CurrentDate) > 0)
        {
          InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_Specimen_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (!string.IsNullOrEmpty(Request.QueryString["IPSSpecimenId"]))
          {
            if (((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenStatusList")).SelectedValue.ToString() == "4941")
            {
              string SQLStringDeleteOrganismResistanceMechanism = "DELETE FROM Form_IPS_Organism_ResistanceMechanism WHERE IPS_Organism_Id IN ( SELECT IPS_Organism_Id FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id ) )";
              using (SqlCommand SqlCommand_DeleteOrganismResistanceMechanism = new SqlCommand(SQLStringDeleteOrganismResistanceMechanism))
              {
                SqlCommand_DeleteOrganismResistanceMechanism.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteOrganismResistanceMechanism);
              }


              string SQLStringDeleteAntibiogram = "DELETE FROM Form_IPS_Antibiogram WHERE IPS_Organism_Id IN ( SELECT IPS_Organism_Id FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id ) )";
              using (SqlCommand SqlCommand_DeleteAntibiogram = new SqlCommand(SQLStringDeleteAntibiogram))
              {
                SqlCommand_DeleteAntibiogram.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteAntibiogram);
              }


              string SQLStringDeleteOrganism = "DELETE FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id )";
              using (SqlCommand SqlCommand_DeleteOrganism = new SqlCommand(SQLStringDeleteOrganism))
              {
                SqlCommand_DeleteOrganism.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteOrganism);
              }


              string SQLStringDeleteSpecimenResult = "DELETE FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id";
              using (SqlCommand SqlCommand_DeleteSpecimenResult = new SqlCommand(SQLStringDeleteSpecimenResult))
              {
                SqlCommand_DeleteSpecimenResult.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteSpecimenResult);
              }
            }

            if (((DropDownList)FormView_IPS_Specimen_Form.FindControl("DropDownList_EditSpecimenStatusList")).SelectedValue.ToString() == "5019")
            {
              string SQLStringDeleteOrganismResistanceMechanism = "DELETE FROM Form_IPS_Organism_ResistanceMechanism WHERE IPS_Organism_Id IN ( SELECT IPS_Organism_Id FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id ) )";
              using (SqlCommand SqlCommand_DeleteOrganismResistanceMechanism = new SqlCommand(SQLStringDeleteOrganismResistanceMechanism))
              {
                SqlCommand_DeleteOrganismResistanceMechanism.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteOrganismResistanceMechanism);
              }


              string SQLStringDeleteAntibiogram = "DELETE FROM Form_IPS_Antibiogram WHERE IPS_Organism_Id IN ( SELECT IPS_Organism_Id FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id ) )";
              using (SqlCommand SqlCommand_DeleteAntibiogram = new SqlCommand(SQLStringDeleteAntibiogram))
              {
                SqlCommand_DeleteAntibiogram.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteAntibiogram);
              }


              string SQLStringDeleteOrganism = "DELETE FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id )";
              using (SqlCommand SqlCommand_DeleteOrganism = new SqlCommand(SQLStringDeleteOrganism))
              {
                SqlCommand_DeleteOrganism.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteOrganism);
              }
            }

            if (((CheckBox)FormView_IPS_Specimen_Form.FindControl("CheckBox_EditIsActive")).Checked == false)
            {
              string SQLStringDeleteAntibiogram = "DELETE FROM Form_IPS_Antibiogram WHERE IPS_Organism_Id IN ( SELECT IPS_Organism_Id FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id ) )";
              using (SqlCommand SqlCommand_DeleteAntibiogram = new SqlCommand(SQLStringDeleteAntibiogram))
              {
                SqlCommand_DeleteAntibiogram.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteAntibiogram);
              }

              string SQLStringDeleteOrganismResistanceMechanism = "DELETE FROM Form_IPS_Organism_ResistanceMechanism WHERE IPS_Organism_Id IN ( SELECT IPS_Organism_Id FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id ) )";
              using (SqlCommand SqlCommand_DeleteOrganismResistanceMechanism = new SqlCommand(SQLStringDeleteOrganismResistanceMechanism))
              {
                SqlCommand_DeleteOrganismResistanceMechanism.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteOrganismResistanceMechanism);
              }

              string SQLStringDeleteOrganism = "DELETE FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id IN ( SELECT IPS_SpecimenResult_Id FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id )";
              using (SqlCommand SqlCommand_DeleteOrganism = new SqlCommand(SQLStringDeleteOrganism))
              {
                SqlCommand_DeleteOrganism.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteOrganism);
              }

              string SQLStringDeleteSpecimenResult = "DELETE FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id";
              using (SqlCommand SqlCommand_DeleteSpecimenResult = new SqlCommand(SQLStringDeleteSpecimenResult))
              {
                SqlCommand_DeleteSpecimenResult.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteSpecimenResult);
              }
            }
          }


          string SQLStringDeleteLabReports = "EXECUTE spForm_Execute_IPS_HAI_LabReports_Delete @IPS_Infection_Id";
          using (SqlCommand SqlCommand_DeleteLabReports = new SqlCommand(SQLStringDeleteLabReports))
          {
            SqlCommand_DeleteLabReports.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteLabReports);
          }


          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "#CurrentSpecimen"), false);
        }
      }
    }


    protected void FormView_IPS_Specimen_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_IPS_Specimen_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["IPSSpecimenId"]))
        {
          ReadOnlyDataBound_CurrentSpecimen();
        }
      }
    }

    protected void ReadOnlyDataBound_CurrentSpecimen()
    {
      Session["IPSSpecimenStatusName"] = "";
      Session["IPSSpecimenTypeName"] = "";
      string SQLStringSpecimen = "SELECT IPS_Specimen_Status_Name , IPS_Specimen_Type_Name , 'Ward: ' + IPS_BedHistory_Department + '; Room: ' + IPS_BedHistory_Room + '; Bed: ' + IPS_BedHistory_Bed + '; Date: ' + IPS_BedHistory_Date + ';' AS BedHistory FROM vForm_IPS_Specimen WHERE IPS_Specimen_Id = @IPS_Specimen_Id";
      using (SqlCommand SqlCommand_Specimen = new SqlCommand(SQLStringSpecimen))
      {
        SqlCommand_Specimen.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
        DataTable DataTable_Specimen;
        using (DataTable_Specimen = new DataTable())
        {
          DataTable_Specimen.Locale = CultureInfo.CurrentCulture;
          DataTable_Specimen = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Specimen).Copy();
          if (DataTable_Specimen.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Specimen.Rows)
            {
              Session["IPSSpecimenStatusName"] = DataRow_Row["IPS_Specimen_Status_Name"];
              Session["IPSSpecimenTypeName"] = DataRow_Row["IPS_Specimen_Type_Name"];
              Session["BedHistory"] = DataRow_Row["BedHistory"];
            }
          }
        }
      }


      Label Label_ItemSpecimenStatusList = (Label)FormView_IPS_Specimen_Form.FindControl("Label_ItemSpecimenStatusList");
      Label_ItemSpecimenStatusList.Text = Session["IPSSpecimenStatusName"].ToString();

      Label Label_ItemSpecimenTypeList = (Label)FormView_IPS_Specimen_Form.FindControl("Label_ItemSpecimenTypeList");
      Label_ItemSpecimenTypeList.Text = Session["IPSSpecimenTypeName"].ToString();

      Label Label_ItemSpecimenBedHistory = (Label)FormView_IPS_Specimen_Form.FindControl("Label_ItemSpecimenBedHistory");
      Label_ItemSpecimenBedHistory.Text = Session["BedHistory"].ToString();

      Session.Remove("IPSSpecimenStatusName");
      Session.Remove("IPSSpecimenTypeName");
      Session.Remove("BedHistory");
    }


    protected void Button_InsertSpecimenInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void Button_EditSpecimenNew_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentSpecimen"), false);
    }

    protected void Button_EditSpecimenInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void Button_ItemSpecimenInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }
    //---END--- --CurrentSpecimen--//


    //--START-- --SpecimenResult--//
    protected void SqlDataSource_IPS_SpecimenResult_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenSpecimenResultTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_SpecimenResult_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }


      for (int i = 0; i < GridView_IPS_SpecimenResult_List.Rows.Count; i++)
      {
        if (GridView_IPS_SpecimenResult_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_IPS_SpecimenResult_List.Rows[i].Cells[2].Text == "No")
          {
            GridView_IPS_SpecimenResult_List.Rows[i].Cells[2].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_SpecimenResult_List.Rows[i].Cells[2].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_IPS_SpecimenResult_List.Rows[i].Cells[2].Text == "Yes")
          {
            GridView_IPS_SpecimenResult_List.Rows[i].Cells[2].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_SpecimenResult_List.Rows[i].Cells[2].ForeColor = Color.FromName("#333333");
          }


          if (GridView_IPS_SpecimenResult_List.Rows[i].Cells[2].Text == "Yes")
          {
            if (GridView_IPS_SpecimenResult_List.Rows[i].Cells[3].Text == "Incomplete")
            {
              GridView_IPS_SpecimenResult_List.Rows[i].Cells[3].BackColor = Color.FromName("#d46e6e");
              GridView_IPS_SpecimenResult_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
            }
            else
            {
              GridView_IPS_SpecimenResult_List.Rows[i].Cells[3].BackColor = Color.FromName("#77cf9c");
              GridView_IPS_SpecimenResult_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
            }
          }
          else if (GridView_IPS_SpecimenResult_List.Rows[i].Cells[2].Text == "No")
          {
            GridView_IPS_SpecimenResult_List.Rows[i].Cells[3].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_SpecimenResult_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_IPS_SpecimenResult_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_SpecimenResultTotalRecords = (Label)e.Row.FindControl("Label_SpecimenResultTotalRecords");
          Label_SpecimenResultTotalRecords.Text = Label_HiddenSpecimenResultTotalRecords.Text;
        }


        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_CaptureSpecimenResult = (Button)e.Row.FindControl("Button_CaptureSpecimenResult");

          FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
          DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
          DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
          DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
          DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
          DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          FromDataBase_SpecimenIsActive FromDataBase_SpecimenIsActive_Current = GetSpecimenIsActive();
          string IPSSpecimenIsActive = FromDataBase_SpecimenIsActive_Current.IPSSpecimenIsActive;

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
          {
            Security = "0";
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
            {
              if (IPSSpecimenIsActive == "True")
              {
                Button_CaptureSpecimenResult.Visible = true;
              }
              else
              {
                Button_CaptureSpecimenResult.Visible = false;
              }
            }
            else
            {
              Button_CaptureSpecimenResult.Visible = false;
            }
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
          {
            Security = "0";
            Button_CaptureSpecimenResult.Visible = false;
          }

          if (Security == "1")
          {
            Security = "0";
            Button_CaptureSpecimenResult.Visible = false;
          }
        }
      }
    }

    protected void Button_CaptureSpecimenResult_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "#CurrentSpecimenResult"), false);
    }

    public string GetSpecimenResultLink(object ips_SpecimenResult_Id)
    {
      string FinalURL = "";
      if (ips_SpecimenResult_Id != null)
      {
        if (ips_SpecimenResult_Id.ToString() == Request.QueryString["IPSSpecimenResultId"])
        {
          FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + ips_SpecimenResult_Id + "#CurrentSpecimenResult") + "'>Selected</a>";
        }
        else
        {
          FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + ips_SpecimenResult_Id + "#CurrentSpecimenResult") + "'>Select</a>";
        }
      }

      return FinalURL;
    }
    //---END--- --SpecimenResult--//


    //--START-- --CurrentSpecimenResult--//
    protected void SetCurrentSpecimenResultVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["IPSSpecimenResultId"]))
      {
        SetCurrentSpecimenResultVisibility_Insert();
      }
      else
      {
        SetCurrentSpecimenResultVisibility_Edit();
      }
    }

    protected void SetCurrentSpecimenResultVisibility_Insert()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      FromDataBase_SpecimenIsActive FromDataBase_SpecimenIsActive_Current = GetSpecimenIsActive();
      string IPSSpecimenIsActive = FromDataBase_SpecimenIsActive_Current.IPSSpecimenIsActive;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
        {
          if (IPSSpecimenIsActive == "True")
          {
            FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.Insert);
          }
          else
          {
            FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.ReadOnly);
          }
        }
        else
        {
          FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetCurrentSpecimenResultVisibility_Edit()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      FromDataBase_SpecimenIsActive FromDataBase_SpecimenIsActive_Current = GetSpecimenIsActive();
      string IPSSpecimenIsActive = FromDataBase_SpecimenIsActive_Current.IPSSpecimenIsActive;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
        {
          if (IPSSpecimenIsActive == "True")
          {
            FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.Edit);
          }
          else
          {
            FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.ReadOnly);
          }
        }
        else
        {
          FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_IPS_SpecimenResult_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentSpecimenResultVisible()
    {
      if (FormView_IPS_SpecimenResult_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_InsertSpecimenResultLabNumber")).Attributes.Add("OnKeyUp", "Validation_SpecimenResultForm();");
        ((TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_InsertSpecimenResultLabNumber")).Attributes.Add("OnInput", "Validation_SpecimenResultForm();");
      }

      if (FormView_IPS_SpecimenResult_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_EditSpecimenResultLabNumber")).Attributes.Add("OnKeyUp", "Validation_SpecimenResultForm();");
        ((TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_EditSpecimenResultLabNumber")).Attributes.Add("OnInput", "Validation_SpecimenResultForm();");
      }
    }


    protected void FormView_IPS_SpecimenResult_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_SpecimenResult();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentSpecimenResult);

          ((Label)FormView_IPS_SpecimenResult_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_IPS_SpecimenResult_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";

          InsertRegisterPostBackControl_CurrentSpecimenResult();
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          SqlDataSource_IPS_SpecimenResult_Form.InsertParameters["IPS_Specimen_Id"].DefaultValue = Request.QueryString["IPSSpecimenId"];

          SqlDataSource_IPS_SpecimenResult_Form.InsertParameters["IPS_SpecimenResult_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_SpecimenResult_Form.InsertParameters["IPS_SpecimenResult_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_SpecimenResult_Form.InsertParameters["IPS_SpecimenResult_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_SpecimenResult_Form.InsertParameters["IPS_SpecimenResult_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_SpecimenResult_Form.InsertParameters["IPS_SpecimenResult_History"].DefaultValue = "";
          SqlDataSource_IPS_SpecimenResult_Form.InsertParameters["IPS_SpecimenResult_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation_SpecimenResult()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertSpecimenResultLabNumber = (TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_InsertSpecimenResultLabNumber");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertSpecimenResultLabNumber.Text))
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
        InvalidFormMessage = InsertFieldValidation_SpecimenResult();
      }

      return InvalidFormMessage;
    }

    protected string InsertFieldValidation_SpecimenResult()
    {
      string InvalidFormMessage = "";

      TextBox TextBox_InsertSpecimenResultLabNumber = (TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_InsertSpecimenResultLabNumber");

      Session["IPSSpecimenResultLabNumber"] = "";
      string SQLStringLabNumber = "SELECT IPS_SpecimenResult_LabNumber FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id AND IPS_SpecimenResult_LabNumber = @IPS_SpecimenResult_LabNumber AND IPS_SpecimenResult_IsActive = @IPS_SpecimenResult_IsActive";
      using (SqlCommand SqlCommand_LabNumber = new SqlCommand(SQLStringLabNumber))
      {
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_SpecimenResult_LabNumber", TextBox_InsertSpecimenResultLabNumber.Text.ToString());
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_SpecimenResult_IsActive", 1);
        DataTable DataTable_LabNumber;
        using (DataTable_LabNumber = new DataTable())
        {
          DataTable_LabNumber.Locale = CultureInfo.CurrentCulture;

          DataTable_LabNumber = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LabNumber).Copy();
          if (DataTable_LabNumber.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_LabNumber.Rows)
            {
              Session["IPSSpecimenResultLabNumber"] = DataRow_Row["IPS_SpecimenResult_LabNumber"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSSpecimenResultLabNumber"].ToString()))
      {
        InvalidFormMessage = "";
      }
      else
      {
        InvalidFormMessage = Convert.ToString("A Lab Number with the Number '" + Session["IPSSpecimenResultLabNumber"].ToString() + "' already exists", CultureInfo.CurrentCulture);
      }

      Session["IPSSpecimenResultLabNumber"] = "";

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_SpecimenResult_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["IPS_SpecimenResult_Id"] = e.Command.Parameters["@IPS_SpecimenResult_Id"].Value;
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + Session["IPS_SpecimenResult_Id"].ToString() + "#CurrentSpecimenResult"), false);
      }
    }


    protected void FormView_IPS_SpecimenResult_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIPSSpecimenResultModifiedDate"] = e.OldValues["IPS_SpecimenResult_ModifiedDate"];
        object OLDIPSSpecimenResultModifiedDate = Session["OLDIPSSpecimenResultModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSSpecimenResultModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareIPSSpecimenResult = (DataView)SqlDataSource_IPS_SpecimenResult_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareIPSSpecimenResult = DataView_CompareIPSSpecimenResult[0];
        Session["DBIPSSpecimenResultModifiedDate"] = Convert.ToString(DataRowView_CompareIPSSpecimenResult["IPS_SpecimenResult_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIPSSpecimenResultModifiedBy"] = Convert.ToString(DataRowView_CompareIPSSpecimenResult["IPS_SpecimenResult_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIPSSpecimenResultModifiedDate = Session["DBIPSSpecimenResultModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIPSSpecimenResultModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentSpecimenResult);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSSpecimenResultModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_IPS_SpecimenResult_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_IPS_SpecimenResult_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;

          EditRegisterPostBackControl_CurrentSpecimenResult();
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_SpecimenResult();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentSpecimenResult);
            ((Label)FormView_IPS_SpecimenResult_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_IPS_SpecimenResult_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";

            EditRegisterPostBackControl_CurrentSpecimenResult();
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;

            e.NewValues["IPS_SpecimenResult_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["IPS_SpecimenResult_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_SpecimenResult", "IPS_SpecimenResult_Id = " + Request.QueryString["IPSSpecimenResultId"]);

            DataView DataView_IPSSpecimenResult = (DataView)SqlDataSource_IPS_SpecimenResult_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_IPSSpecimenResult = DataView_IPSSpecimenResult[0];
            Session["IPSSpecimenResultHistory"] = Convert.ToString(DataRowView_IPSSpecimenResult["IPS_SpecimenResult_History"], CultureInfo.CurrentCulture);

            Session["IPSSpecimenResultHistory"] = Session["History"].ToString() + Session["IPSSpecimenResultHistory"].ToString();
            e.NewValues["IPS_SpecimenResult_History"] = Session["IPSSpecimenResultHistory"].ToString();

            Session.Remove("IPSSpecimenResultHistory");
            Session.Remove("History");
          }
        }
      }
    }

    protected string EditValidation_SpecimenResult()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditSpecimenResultLabNumber = (TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_EditSpecimenResultLabNumber");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditSpecimenResultLabNumber.Text))
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
        InvalidFormMessage = EditFieldValidation_SpecimenResult();
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation_SpecimenResult()
    {
      string InvalidFormMessage = "";

      TextBox TextBox_EditSpecimenResultLabNumber = (TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_EditSpecimenResultLabNumber");

      Session["IPSSpecimenResultLabNumber"] = "";
      Session["IPSSpecimenResultId"] = "";
      string SQLStringLabNumber = "SELECT IPS_SpecimenResult_Id , IPS_SpecimenResult_LabNumber FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id AND IPS_SpecimenResult_LabNumber = @IPS_SpecimenResult_LabNumber AND IPS_SpecimenResult_IsActive = @IPS_SpecimenResult_IsActive";
      using (SqlCommand SqlCommand_LabNumber = new SqlCommand(SQLStringLabNumber))
      {
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_SpecimenResult_LabNumber", TextBox_EditSpecimenResultLabNumber.Text.ToString());
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_SpecimenResult_IsActive", 1);
        DataTable DataTable_LabNumber;
        using (DataTable_LabNumber = new DataTable())
        {
          DataTable_LabNumber.Locale = CultureInfo.CurrentCulture;

          DataTable_LabNumber = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LabNumber).Copy();
          if (DataTable_LabNumber.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_LabNumber.Rows)
            {
              Session["IPSSpecimenResultLabNumber"] = DataRow_Row["IPS_SpecimenResult_LabNumber"];
              Session["IPSSpecimenResultId"] = DataRow_Row["IPS_SpecimenResult_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSSpecimenResultLabNumber"].ToString()))
      {
        InvalidFormMessage = "";
      }
      else
      {
        if (Session["IPSSpecimenResultId"].ToString() == Request.QueryString["IPSSpecimenResultId"])
        {
          InvalidFormMessage = "";
        }
        else
        {
          InvalidFormMessage = Convert.ToString("A Lab Number with the Number '" + Session["IPSSpecimenResultLabNumber"].ToString() + "' already exists", CultureInfo.CurrentCulture);
        }
      }

      Session["IPSSpecimenResultLabNumber"] = "";
      Session["IPSSpecimenResultId"] = "";

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_SpecimenResult_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (!string.IsNullOrEmpty(Request.QueryString["IPSSpecimenResultId"]))
          {
            if (((CheckBox)FormView_IPS_SpecimenResult_Form.FindControl("CheckBox_EditIsActive")).Checked == false)
            {
              string SQLStringDeleteAntibiogram = "DELETE FROM Form_IPS_Antibiogram WHERE IPS_Organism_Id IN ( SELECT IPS_Organism_Id FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id = @IPS_SpecimenResult_Id )";
              using (SqlCommand SqlCommand_DeleteAntibiogram = new SqlCommand(SQLStringDeleteAntibiogram))
              {
                SqlCommand_DeleteAntibiogram.Parameters.AddWithValue("@IPS_SpecimenResult_Id", Request.QueryString["IPSSpecimenResultId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteAntibiogram);
              }

              string SQLStringDeleteOrganismResistanceMechanism = "DELETE FROM Form_IPS_Organism_ResistanceMechanism WHERE IPS_Organism_Id IN ( SELECT IPS_Organism_Id FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id = @IPS_SpecimenResult_Id )";
              using (SqlCommand SqlCommand_DeleteOrganismResistanceMechanism = new SqlCommand(SQLStringDeleteOrganismResistanceMechanism))
              {
                SqlCommand_DeleteOrganismResistanceMechanism.Parameters.AddWithValue("@IPS_SpecimenResult_Id", Request.QueryString["IPSSpecimenResultId"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteOrganismResistanceMechanism);
              }

              string SQLStringDeleteOrganism = "DELETE FROM Form_IPS_Organism WHERE IPS_SpecimenResult_Id = @IPS_SpecimenResult_Id";
              using (SqlCommand SqlCommand_DeleteOrganism = new SqlCommand(SQLStringDeleteOrganism))
              {
                SqlCommand_DeleteOrganism.Parameters.AddWithValue("@IPS_SpecimenResult_Id", Request.QueryString["IPSSpecimenResultId"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteOrganism);
              }
            }
          }


          string SQLStringDeleteLabReports = "EXECUTE spForm_Execute_IPS_HAI_LabReports_Delete @IPS_Infection_Id";
          using (SqlCommand SqlCommand_DeleteLabReports = new SqlCommand(SQLStringDeleteLabReports))
          {
            SqlCommand_DeleteLabReports.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteLabReports);
          }


          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + Request.QueryString["IPSSpecimenResultId"] + "#CurrentSpecimenResult"), false);
        }
      }
    }


    protected void InsertRegisterPostBackControl_CurrentSpecimenResult()
    {
      TextBox TextBox_InsertSpecimenResultLabNumber = (TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_InsertSpecimenResultLabNumber");

      ScriptManager ScriptManager_Insert = ScriptManager.GetCurrent(Page);

      ScriptManager_Insert.RegisterPostBackControl(TextBox_InsertSpecimenResultLabNumber);
    }

    protected void TextBox_InsertSpecimenResultLabNumber_TextChanged(object sender, EventArgs e)
    {
      System.Threading.Thread.Sleep(100);

      TextBox TextBox_InsertSpecimenResultLabNumber = (TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_InsertSpecimenResultLabNumber");
      Label Label_InsertSpecimenResultLabNumberError = (Label)FormView_IPS_SpecimenResult_Form.FindControl("Label_InsertSpecimenResultLabNumberError");

      Session["IPSSpecimenResultLabNumber"] = "";
      string SQLStringLabNumber = "SELECT IPS_SpecimenResult_LabNumber FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id AND IPS_SpecimenResult_LabNumber = @IPS_SpecimenResult_LabNumber AND IPS_SpecimenResult_IsActive = @IPS_SpecimenResult_IsActive";
      using (SqlCommand SqlCommand_LabNumber = new SqlCommand(SQLStringLabNumber))
      {
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_SpecimenResult_LabNumber", TextBox_InsertSpecimenResultLabNumber.Text.ToString());
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_SpecimenResult_IsActive", 1);
        DataTable DataTable_LabNumber;
        using (DataTable_LabNumber = new DataTable())
        {
          DataTable_LabNumber.Locale = CultureInfo.CurrentCulture;

          DataTable_LabNumber = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LabNumber).Copy();
          if (DataTable_LabNumber.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_LabNumber.Rows)
            {
              Session["IPSSpecimenResultLabNumber"] = DataRow_Row["IPS_SpecimenResult_LabNumber"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSSpecimenResultLabNumber"].ToString()))
      {
        Label_InsertSpecimenResultLabNumberError.Text = "";
      }
      else
      {
        Label_InsertSpecimenResultLabNumberError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Lab Number with the Number '" + Session["IPSSpecimenResultLabNumber"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["IPSSpecimenResultLabNumber"] = "";
    }

    protected void Button_InsertSpecimenResultInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void EditRegisterPostBackControl_CurrentSpecimenResult()
    {
      TextBox TextBox_EditSpecimenResultLabNumber = (TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_EditSpecimenResultLabNumber");

      ScriptManager ScriptManager_Edit = ScriptManager.GetCurrent(Page);

      ScriptManager_Edit.RegisterPostBackControl(TextBox_EditSpecimenResultLabNumber);
    }

    protected void TextBox_EditSpecimenResultLabNumber_TextChanged(object sender, EventArgs e)
    {
      System.Threading.Thread.Sleep(100);

      TextBox TextBox_EditSpecimenResultLabNumber = (TextBox)FormView_IPS_SpecimenResult_Form.FindControl("TextBox_EditSpecimenResultLabNumber");
      Label Label_EditSpecimenResultLabNumberError = (Label)FormView_IPS_SpecimenResult_Form.FindControl("Label_EditSpecimenResultLabNumberError");

      Session["IPSSpecimenResultLabNumber"] = "";
      Session["IPSSpecimenResultId"] = "";
      string SQLStringLabNumber = "SELECT IPS_SpecimenResult_Id , IPS_SpecimenResult_LabNumber FROM Form_IPS_SpecimenResult WHERE IPS_Specimen_Id = @IPS_Specimen_Id AND IPS_SpecimenResult_LabNumber = @IPS_SpecimenResult_LabNumber AND IPS_SpecimenResult_IsActive = @IPS_SpecimenResult_IsActive";
      using (SqlCommand SqlCommand_LabNumber = new SqlCommand(SQLStringLabNumber))
      {
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_Specimen_Id", Request.QueryString["IPSSpecimenId"]);
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_SpecimenResult_LabNumber", TextBox_EditSpecimenResultLabNumber.Text.ToString());
        SqlCommand_LabNumber.Parameters.AddWithValue("@IPS_SpecimenResult_IsActive", 1);
        DataTable DataTable_LabNumber;
        using (DataTable_LabNumber = new DataTable())
        {
          DataTable_LabNumber.Locale = CultureInfo.CurrentCulture;

          DataTable_LabNumber = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_LabNumber).Copy();
          if (DataTable_LabNumber.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_LabNumber.Rows)
            {
              Session["IPSSpecimenResultLabNumber"] = DataRow_Row["IPS_SpecimenResult_LabNumber"];
              Session["IPSSpecimenResultId"] = DataRow_Row["IPS_SpecimenResult_Id"];
            }
          }
        }
      }

      if (string.IsNullOrEmpty(Session["IPSSpecimenResultLabNumber"].ToString()))
      {
        Label_EditSpecimenResultLabNumberError.Text = "";
      }
      else
      {
        if (Session["IPSSpecimenResultId"].ToString() == Request.QueryString["IPSSpecimenResultId"])
        {
          Label_EditSpecimenResultLabNumberError.Text = "";
        }
        else
        {
          Label_EditSpecimenResultLabNumberError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Lab Number with the Number '" + Session["IPSSpecimenResultLabNumber"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["IPSSpecimenResultLabNumber"] = "";
      Session["IPSSpecimenResultId"] = "";
    }

    protected void Button_EditSpecimenResultNewSpecimen_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentSpecimen"), false);
    }

    protected void Button_EditSpecimenResultNew_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "#CurrentSpecimenResult"), false);
    }

    protected void Button_EditSpecimenResultInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void Button_ItemSpecimenResultInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }
    //---END--- --CurrentSpecimenResult--//


    //--START-- --Organism--//
    protected void SqlDataSource_IPS_Organism_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenOrganismTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_Organism_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }


      for (int i = 0; i < GridView_IPS_Organism_List.Rows.Count; i++)
      {
        if (GridView_IPS_Organism_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_IPS_Organism_List.Rows[i].Cells[8].Text == "No")
          {
            GridView_IPS_Organism_List.Rows[i].Cells[8].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_Organism_List.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_IPS_Organism_List.Rows[i].Cells[8].Text == "Yes")
          {
            GridView_IPS_Organism_List.Rows[i].Cells[8].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_Organism_List.Rows[i].Cells[8].ForeColor = Color.FromName("#333333");
          }
        }

        if (GridView_IPS_Organism_List.Rows[i].Cells[8].Text == "Yes")
        {
          if (GridView_IPS_Organism_List.Rows[i].Cells[9].Text == "Incomplete")
          {
            GridView_IPS_Organism_List.Rows[i].Cells[9].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_Organism_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
          }
          else
          {
            GridView_IPS_Organism_List.Rows[i].Cells[9].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_Organism_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
          }
        }
        else if (GridView_IPS_Organism_List.Rows[i].Cells[8].Text == "No")
        {
          GridView_IPS_Organism_List.Rows[i].Cells[9].BackColor = Color.FromName("#77cf9c");
          GridView_IPS_Organism_List.Rows[i].Cells[9].ForeColor = Color.FromName("#333333");
        }
      }
    }

    protected void GridView_IPS_Organism_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_OrganismTotalRecords = (Label)e.Row.FindControl("Label_OrganismTotalRecords");
          Label_OrganismTotalRecords.Text = Label_HiddenOrganismTotalRecords.Text;
        }


        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_CaptureOrganism = (Button)e.Row.FindControl("Button_CaptureOrganism");

          FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
          DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
          DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
          DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
          DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
          DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

          FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
          string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
          string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

          FromDataBase_SpecimenIsActive FromDataBase_SpecimenIsActive_Current = GetSpecimenIsActive();
          string IPSSpecimenIsActive = FromDataBase_SpecimenIsActive_Current.IPSSpecimenIsActive;

          FromDataBase_SpecimenResultIsActive FromDataBase_SpecimenResultIsActive_Current = GetSpecimenResultIsActive();
          string IPSSpecimenResultIsActive = FromDataBase_SpecimenResultIsActive_Current.IPSSpecimenResultIsActive;

          string Security = "1";
          if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
          {
            Security = "0";
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
            {
              if (IPSSpecimenIsActive == "True")
              {
                if (IPSSpecimenResultIsActive == "True")
                {
                  Button_CaptureOrganism.Visible = true;
                }
                else
                {
                  Button_CaptureOrganism.Visible = false;
                }
              }
              else
              {
                Button_CaptureOrganism.Visible = false;
              }
            }
            else
            {
              Button_CaptureOrganism.Visible = false;
            }
          }

          if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
          {
            Security = "0";
            Button_CaptureOrganism.Visible = false;
          }

          if (Security == "1")
          {
            Security = "0";
            Button_CaptureOrganism.Visible = false;
          }
        }
      }
    }

    protected void Button_CaptureOrganism_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + Request.QueryString["IPSSpecimenResultId"] + "#CurrentOrganism"), false);
    }

    public string GetOrganismLink(object ips_Organism_Id)
    {
      string FinalURL = "";
      if (ips_Organism_Id != null)
      {
        if (ips_Organism_Id.ToString() == Request.QueryString["IPSOrganismId"])
        {
          FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + Request.QueryString["IPSSpecimenResultId"] + "&IPSOrganismId=" + ips_Organism_Id + "#CurrentOrganism") + "'>Selected</a>";
        }
        else
        {
          FinalURL = "<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + Request.QueryString["IPSSpecimenResultId"] + "&IPSOrganismId=" + ips_Organism_Id + "#CurrentOrganism") + "'>Select</a>";
        }
      }

      return FinalURL;
    }
    //---END--- --Organism--//


    //--START-- --CurrentOrganism--//
    protected void SetCurrentOrganismVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["IPSOrganismId"]))
      {
        SetCurrentOrganismVisibility_Insert();
      }
      else
      {
        SetCurrentOrganismVisibility_Edit();
      }
    }

    protected void SetCurrentOrganismVisibility_Insert()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      FromDataBase_SpecimenIsActive FromDataBase_SpecimenIsActive_Current = GetSpecimenIsActive();
      string IPSSpecimenIsActive = FromDataBase_SpecimenIsActive_Current.IPSSpecimenIsActive;

      FromDataBase_SpecimenResultIsActive FromDataBase_SpecimenResultIsActive_Current = GetSpecimenResultIsActive();
      string IPSSpecimenResultIsActive = FromDataBase_SpecimenResultIsActive_Current.IPSSpecimenResultIsActive;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
        {
          if (IPSSpecimenIsActive == "True")
          {
            if (IPSSpecimenResultIsActive == "True")
            {
              FormView_IPS_Organism_Form.ChangeMode(FormViewMode.Insert);
            }
            else
            {
              FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
            }
          }
          else
          {
            FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
          }
        }
        else
        {
          FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void SetCurrentOrganismVisibility_Edit()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      FromDataBase_SpecimenIsActive FromDataBase_SpecimenIsActive_Current = GetSpecimenIsActive();
      string IPSSpecimenIsActive = FromDataBase_SpecimenIsActive_Current.IPSSpecimenIsActive;

      FromDataBase_SpecimenResultIsActive FromDataBase_SpecimenResultIsActive_Current = GetSpecimenResultIsActive();
      string IPSSpecimenResultIsActive = FromDataBase_SpecimenResultIsActive_Current.IPSSpecimenResultIsActive;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";
        if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
        {
          if (IPSSpecimenIsActive == "True")
          {
            if (IPSSpecimenResultIsActive == "True")
            {
              FormView_IPS_Organism_Form.ChangeMode(FormViewMode.Edit);
            }
            else
            {
              FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
            }
          }
          else
          {
            FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
          }
        }
        else
        {
          FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";
        FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
      }

      if (Security == "1")
      {
        Security = "0";
        FormView_IPS_Organism_Form.ChangeMode(FormViewMode.ReadOnly);
      }
    }

    protected void TableCurrentOrganismVisible()
    {
      if (FormView_IPS_Organism_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNameLookup")).Attributes.Add("OnKeyUp", "Validation_OrganismForm();");
        ((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNameLookup")).Attributes.Add("OnInput", "Validation_OrganismForm();");
        ((DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_InsertOrganismNameLookup")).Attributes.Add("OnChange", "Validation_OrganismForm();");
        ((DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_InsertOrganismResistanceList")).Attributes.Add("OnChange", "Validation_OrganismForm();");
        ((CheckBoxList)FormView_IPS_Organism_Form.FindControl("CheckBoxList_InsertRMMechanismItemList")).Attributes.Add("OnClick", "Validation_OrganismForm();");
        ((CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_InsertOrganismNotifiableDepartmentOfHealth")).Attributes.Add("OnClick", "Validation_OrganismForm();");
        ((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNotifiableDepartmentOfHealthDate")).Attributes.Add("OnChange", "Validation_OrganismForm();");
        ((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNotifiableDepartmentOfHealthDate")).Attributes.Add("OnInput", "Validation_OrganismForm();");
      }

      if (FormView_IPS_Organism_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNameLookup")).Attributes.Add("OnKeyUp", "Validation_OrganismForm();");
        ((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNameLookup")).Attributes.Add("OnInput", "Validation_OrganismForm();");
        ((DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_EditOrganismNameLookup")).Attributes.Add("OnChange", "Validation_OrganismForm();");
        ((DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_EditOrganismResistanceList")).Attributes.Add("OnChange", "Validation_OrganismForm();");
        ((CheckBoxList)FormView_IPS_Organism_Form.FindControl("CheckBoxList_EditRMMechanismItemList")).Attributes.Add("OnClick", "Validation_OrganismForm();");
        ((CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_EditOrganismNotifiableDepartmentOfHealth")).Attributes.Add("OnClick", "Validation_OrganismForm();");
        ((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthDate")).Attributes.Add("OnChange", "Validation_OrganismForm();");
        ((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthDate")).Attributes.Add("OnInput", "Validation_OrganismForm();");
      }
    }


    protected void FormView_IPS_Organism_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation_Organism();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentOrganism);

          ((Label)FormView_IPS_Organism_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_IPS_Organism_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";

          InsertRegisterPostBackControl_CurrentOrganism();
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          SqlDataSource_IPS_Organism_Form.InsertParameters["IPS_SpecimenResult_Id"].DefaultValue = Request.QueryString["IPSSpecimenResultId"];

          SqlDataSource_IPS_Organism_Form.InsertParameters["IPS_Organism_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_Organism_Form.InsertParameters["IPS_Organism_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_Organism_Form.InsertParameters["IPS_Organism_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_Organism_Form.InsertParameters["IPS_Organism_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_IPS_Organism_Form.InsertParameters["IPS_Organism_History"].DefaultValue = "";
          SqlDataSource_IPS_Organism_Form.InsertParameters["IPS_Organism_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation_Organism()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertOrganismNameLookup = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNameLookup");
      DropDownList DropDownList_InsertOrganismNameLookup = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_InsertOrganismNameLookup");
      DropDownList DropDownList_InsertOrganismResistanceList = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_InsertOrganismResistanceList");
      CheckBoxList CheckBoxList_InsertRMMechanismItemList = (CheckBoxList)FormView_IPS_Organism_Form.FindControl("CheckBoxList_InsertRMMechanismItemList");
      CheckBox CheckBox_InsertOrganismNotifiableDepartmentOfHealth = (CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_InsertOrganismNotifiableDepartmentOfHealth");
      TextBox TextBox_InsertOrganismNotifiableDepartmentOfHealthDate = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNotifiableDepartmentOfHealthDate");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertOrganismNameLookup.Text) || string.IsNullOrEmpty(DropDownList_InsertOrganismNameLookup.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertOrganismResistanceList.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        string MechanismItemCompleted = "No";
        foreach (System.Web.UI.WebControls.ListItem ListItem_RMMechanismItemList in CheckBoxList_InsertRMMechanismItemList.Items)
        {
          if (ListItem_RMMechanismItemList.Selected == true)
          {
            MechanismItemCompleted = "Yes";
            break;
          }
          else if (ListItem_RMMechanismItemList.Selected == false)
          {
            MechanismItemCompleted = "No";
          }
        }

        if (MechanismItemCompleted == "No")
        {
          InvalidForm = "Yes";
        }

        if (CheckBox_InsertOrganismNotifiableDepartmentOfHealth.Checked == true)
        {
          if (string.IsNullOrEmpty(TextBox_InsertOrganismNotifiableDepartmentOfHealthDate.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = InsertFieldValidation_Organism();
      }

      return InvalidFormMessage;
    }

    protected string InsertFieldValidation_Organism()
    {
      string InvalidFormMessage = "";

      CheckBox CheckBox_InsertOrganismNotifiableDepartmentOfHealth = (CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_InsertOrganismNotifiableDepartmentOfHealth");
      TextBox TextBox_InsertOrganismNotifiableDepartmentOfHealthDate = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNotifiableDepartmentOfHealthDate");

      if (CheckBox_InsertOrganismNotifiableDepartmentOfHealth.Checked == true)
      {
        string DateToValidateOrganismDate = TextBox_InsertOrganismNotifiableDepartmentOfHealthDate.Text.ToString();
        DateTime ValidatedDateOrganismDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateOrganismDate);

        if (ValidatedDateOrganismDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Department of Health Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNotifiableDepartmentOfHealthDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_Organism_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["IPS_Organism_Id"] = e.Command.Parameters["@IPS_Organism_Id"].Value;

        if (!string.IsNullOrEmpty(Session["IPS_Organism_Id"].ToString()))
        {
          CheckBoxList CheckBoxList_InsertRMMechanismItemList = (CheckBoxList)FormView_IPS_Organism_Form.FindControl("CheckBoxList_InsertRMMechanismItemList");
          foreach (System.Web.UI.WebControls.ListItem ListItem_RMMechanismItemList in CheckBoxList_InsertRMMechanismItemList.Items)
          {
            if (ListItem_RMMechanismItemList.Selected)
            {
              string SQLStringInsertResistanceMechanism = "INSERT INTO Form_IPS_Organism_ResistanceMechanism ( IPS_Organism_Id , IPS_RM_Mechanism_Item_List , IPS_RM_CreatedDate , IPS_RM_CreatedBy ) VALUES ( @IPS_Organism_Id , @IPS_RM_Mechanism_Item_List , @IPS_RM_CreatedDate , @IPS_RM_CreatedBy )";
              using (SqlCommand SqlCommand_InsertResistanceMechanism = new SqlCommand(SQLStringInsertResistanceMechanism))
              {
                SqlCommand_InsertResistanceMechanism.Parameters.AddWithValue("@IPS_Organism_Id", Session["IPS_Organism_Id"].ToString());
                SqlCommand_InsertResistanceMechanism.Parameters.AddWithValue("@IPS_RM_Mechanism_Item_List", ListItem_RMMechanismItemList.Value.ToString());
                SqlCommand_InsertResistanceMechanism.Parameters.AddWithValue("@IPS_RM_CreatedDate", DateTime.Now);
                SqlCommand_InsertResistanceMechanism.Parameters.AddWithValue("@IPS_RM_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertResistanceMechanism);
              }
            }
          }
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + Request.QueryString["IPSSpecimenResultId"] + "&IPSOrganismId=" + Session["IPS_Organism_Id"] + "#CurrentOrganism"), false);
      }
    }


    protected void FormView_IPS_Organism_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIPSOrganismModifiedDate"] = e.OldValues["IPS_Organism_ModifiedDate"];
        object OLDIPSOrganismModifiedDate = Session["OLDIPSOrganismModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSOrganismModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareIPSOrganism = (DataView)SqlDataSource_IPS_Organism_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareIPSOrganism = DataView_CompareIPSOrganism[0];
        Session["DBIPSOrganismModifiedDate"] = Convert.ToString(DataRowView_CompareIPSOrganism["IPS_Organism_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIPSOrganismModifiedBy"] = Convert.ToString(DataRowView_CompareIPSOrganism["IPS_Organism_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIPSOrganismModifiedDate = Session["DBIPSOrganismModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIPSOrganismModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentOrganism);

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
            "Record could not be updated<br/>" +
            "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSOrganismModifiedBy"].ToString() + "<br/>" +
            "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_IPS_Organism_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_IPS_Organism_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;

          EditRegisterPostBackControl_CurrentOrganism();
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation_Organism();

          if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = true;
            ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentOrganism);
            ((Label)FormView_IPS_Organism_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_IPS_Organism_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";

            EditRegisterPostBackControl_CurrentOrganism();
          }
          else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
          {
            e.Cancel = false;

            e.NewValues["IPS_Organism_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["IPS_Organism_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Organism", "IPS_Organism_Id = " + Request.QueryString["IPSOrganismId"]);

            DataView DataView_IPSOrganism = (DataView)SqlDataSource_IPS_Organism_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_IPSOrganism = DataView_IPSOrganism[0];
            Session["IPSOrganismHistory"] = Convert.ToString(DataRowView_IPSOrganism["IPS_Organism_History"], CultureInfo.CurrentCulture);

            Session["IPSOrganismHistory"] = Session["History"].ToString() + Session["IPSOrganismHistory"].ToString();
            e.NewValues["IPS_Organism_History"] = Session["IPSOrganismHistory"].ToString();

            Session.Remove("IPSOrganismHistory");
            Session.Remove("History");
          }
        }
      }
    }

    protected string EditValidation_Organism()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditOrganismNameLookup = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNameLookup");
      DropDownList DropDownList_EditOrganismNameLookup = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_EditOrganismNameLookup");
      DropDownList DropDownList_EditOrganismResistanceList = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_EditOrganismResistanceList");
      CheckBoxList CheckBoxList_EditRMMechanismItemList = (CheckBoxList)FormView_IPS_Organism_Form.FindControl("CheckBoxList_EditRMMechanismItemList");
      CheckBox CheckBox_EditOrganismNotifiableDepartmentOfHealth = (CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_EditOrganismNotifiableDepartmentOfHealth");
      TextBox TextBox_EditOrganismNotifiableDepartmentOfHealthDate = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthDate");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditOrganismNameLookup.Text) || string.IsNullOrEmpty(DropDownList_EditOrganismNameLookup.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditOrganismResistanceList.SelectedValue))
          {
            InvalidForm = "Yes";
          }
        }

        string ComplaintCategoryCompleted = "No";
        foreach (System.Web.UI.WebControls.ListItem ListItem_RMMechanismItemList in CheckBoxList_EditRMMechanismItemList.Items)
        {
          if (ListItem_RMMechanismItemList.Selected == true)
          {
            ComplaintCategoryCompleted = "Yes";
            break;
          }
          else if (ListItem_RMMechanismItemList.Selected == false)
          {
            ComplaintCategoryCompleted = "No";
          }
        }

        if (ComplaintCategoryCompleted == "No")
        {
          InvalidForm = "Yes";
        }

        if (CheckBox_EditOrganismNotifiableDepartmentOfHealth.Checked == true)
        {
          if (string.IsNullOrEmpty(TextBox_EditOrganismNotifiableDepartmentOfHealthDate.Text))
          {
            InvalidForm = "Yes";
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        InvalidFormMessage = EditFieldValidation_Organism();
      }

      return InvalidFormMessage;
    }

    protected string EditFieldValidation_Organism()
    {
      string InvalidFormMessage = "";

      CheckBox CheckBox_EditOrganismNotifiableDepartmentOfHealth = (CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_EditOrganismNotifiableDepartmentOfHealth");
      TextBox TextBox_EditOrganismNotifiableDepartmentOfHealthDate = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthDate");

      if (CheckBox_EditOrganismNotifiableDepartmentOfHealth.Checked == true)
      {
        string DateToValidateOrganismDate = TextBox_EditOrganismNotifiableDepartmentOfHealthDate.Text.ToString();
        DateTime ValidatedDateOrganismDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateOrganismDate);

        if (ValidatedDateOrganismDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Department of Health Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
        }
        else
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (PickedDate.CompareTo(CurrentDate) > 0)
          {
            InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_IPS_Organism_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (!string.IsNullOrEmpty(Request.QueryString["IPSOrganismId"]))
          {
            string SQLStringIPSOrganismResistanceMechanism = "DELETE FROM Form_IPS_Organism_ResistanceMechanism WHERE IPS_Organism_Id = @IPS_Organism_Id";
            using (SqlCommand SqlCommand_IPSOrganismResistanceMechanism = new SqlCommand(SQLStringIPSOrganismResistanceMechanism))
            {
              SqlCommand_IPSOrganismResistanceMechanism.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);

              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_IPSOrganismResistanceMechanism);
            }

            foreach (System.Web.UI.WebControls.ListItem ListItem_RMMechanismItemList in ((CheckBoxList)FormView_IPS_Organism_Form.FindControl("CheckBoxList_EditRMMechanismItemList")).Items)
            {
              if (ListItem_RMMechanismItemList.Selected)
              {
                string SQLStringInsertResistanceMechanism = "INSERT INTO Form_IPS_Organism_ResistanceMechanism ( IPS_Organism_Id , IPS_RM_Mechanism_Item_List , IPS_RM_CreatedDate , IPS_RM_CreatedBy ) VALUES ( @IPS_Organism_Id , @IPS_RM_Mechanism_Item_List , @IPS_RM_CreatedDate , @IPS_RM_CreatedBy )";
                using (SqlCommand SqlCommand_InsertResistanceMechanism = new SqlCommand(SQLStringInsertResistanceMechanism))
                {
                  SqlCommand_InsertResistanceMechanism.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
                  SqlCommand_InsertResistanceMechanism.Parameters.AddWithValue("@IPS_RM_Mechanism_Item_List", ListItem_RMMechanismItemList.Value.ToString());
                  SqlCommand_InsertResistanceMechanism.Parameters.AddWithValue("@IPS_RM_CreatedDate", DateTime.Now);
                  SqlCommand_InsertResistanceMechanism.Parameters.AddWithValue("@IPS_RM_CreatedBy", Request.ServerVariables["LOGON_USER"]);

                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertResistanceMechanism);
                }
              }
            }


            if (((CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_EditAntibiogramNotRequired")).Checked == true)
            {
              string SQLStringDeleteAntibiogram = "DELETE FROM Form_IPS_Antibiogram WHERE IPS_Organism_Id = @IPS_Organism_Id";
              using (SqlCommand SqlCommand_DeleteAntibiogram = new SqlCommand(SQLStringDeleteAntibiogram))
              {
                SqlCommand_DeleteAntibiogram.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteAntibiogram);
              }
            }


            if (((CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_EditIsActive")).Checked == false)
            {
              string SQLStringDeleteAntibiogram = "DELETE FROM Form_IPS_Antibiogram WHERE IPS_Organism_Id = @IPS_Organism_Id";
              using (SqlCommand SqlCommand_DeleteAntibiogram = new SqlCommand(SQLStringDeleteAntibiogram))
              {
                SqlCommand_DeleteAntibiogram.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteAntibiogram);
              }
            }
          }


          string SQLStringDeleteLabReports = "EXECUTE spForm_Execute_IPS_HAI_LabReports_Delete @IPS_Infection_Id";
          using (SqlCommand SqlCommand_DeleteLabReports = new SqlCommand(SQLStringDeleteLabReports))
          {
            SqlCommand_DeleteLabReports.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteLabReports);
          }


          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + Request.QueryString["IPSSpecimenResultId"] + "&IPSOrganismId=" + Request.QueryString["IPSOrganismId"] + "#CurrentOrganism"), false);
        }
      }
    }


    protected void FormView_IPS_Organism_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_IPS_Organism_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (!string.IsNullOrEmpty(Request.QueryString["IPSOrganismId"]))
        {
          ReadOnlyDataBound_CurrentOrganism();
        }
      }
    }

    protected void ReadOnlyDataBound_CurrentOrganism()
    {
      Session["IPSOrganismLookup"] = "";
      Session["IPSOrganismResistanceName"] = "";
      string SQLStringOrganism = "SELECT IPS_Organism_Lookup_Description + ' (' + IPS_Organism_Lookup_Code + ')' AS IPS_Organism_Lookup , IPS_Organism_Resistance_Name FROM vForm_IPS_Organism WHERE IPS_Organism_Id = @IPS_Organism_Id";
      using (SqlCommand SqlCommand_Organism = new SqlCommand(SQLStringOrganism))
      {
        SqlCommand_Organism.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
        DataTable DataTable_Organism;
        using (DataTable_Organism = new DataTable())
        {
          DataTable_Organism.Locale = CultureInfo.CurrentCulture;
          DataTable_Organism = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Organism).Copy();
          if (DataTable_Organism.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Organism.Rows)
            {
              Session["IPSOrganismLookup"] = DataRow_Row["IPS_Organism_Lookup"];
              Session["IPSOrganismResistanceName"] = DataRow_Row["IPS_Organism_Resistance_Name"];
            }
          }
        }
      }


      Label Label_ItemOrganismNameLookup = (Label)FormView_IPS_Organism_Form.FindControl("Label_ItemOrganismNameLookup");
      Label_ItemOrganismNameLookup.Text = Session["IPSOrganismLookup"].ToString();

      Label Label_ItemOrganismResistanceList = (Label)FormView_IPS_Organism_Form.FindControl("Label_ItemOrganismResistanceList");
      Label_ItemOrganismResistanceList.Text = Session["IPSOrganismResistanceName"].ToString();

      Session.Remove("IPSOrganismLookup");
      Session.Remove("IPSOrganismResistanceName");
    }


    protected void InsertRegisterPostBackControl_CurrentOrganism()
    {
      TextBox TextBox_InsertOrganismNameLookup = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNameLookup");
      DropDownList DropDownList_InsertOrganismNameLookup = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_InsertOrganismNameLookup");

      ScriptManager ScriptManager_Insert = ScriptManager.GetCurrent(Page);

      ScriptManager_Insert.RegisterPostBackControl(TextBox_InsertOrganismNameLookup);
      ScriptManager_Insert.RegisterPostBackControl(DropDownList_InsertOrganismNameLookup);
    }

    protected void TextBox_InsertOrganismNameLookup_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertOrganismNameLookup = (TextBox)sender;
      DropDownList DropDownList_InsertOrganismNameLookup = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_InsertOrganismNameLookup");
      Label Label_InsertInvalidFormMessage = (Label)FormView_IPS_Organism_Form.FindControl("Label_InsertInvalidFormMessage");

      string IPSOrganismLookupId = "";
      string IPSOrganismLookupNotifiable = "";
      string IPSOrganismLookupResistance = "";
      string SQLStringOrganismLookup = "SELECT IPS_Organism_Lookup_Id , IPS_Organism_Lookup_Notifiable , IPS_Organism_Lookup_Resistance FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Code = @IPS_Organism_Lookup_Code AND IPS_Organism_Lookup_IsActive = 1";
      using (SqlCommand SqlCommand_OrganismLookup = new SqlCommand(SQLStringOrganismLookup))
      {
        SqlCommand_OrganismLookup.Parameters.AddWithValue("@IPS_Organism_Lookup_Code", TextBox_InsertOrganismNameLookup.Text.ToString());
        DataTable DataTable_OrganismLookup;
        using (DataTable_OrganismLookup = new DataTable())
        {
          DataTable_OrganismLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_OrganismLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OrganismLookup).Copy();
          if (DataTable_OrganismLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OrganismLookup.Rows)
            {
              IPSOrganismLookupId = DataRow_Row["IPS_Organism_Lookup_Id"].ToString();
              IPSOrganismLookupNotifiable = DataRow_Row["IPS_Organism_Lookup_Notifiable"].ToString();
              IPSOrganismLookupResistance = DataRow_Row["IPS_Organism_Lookup_Resistance"].ToString();
            }
          }
        }
      }

      if (string.IsNullOrEmpty(IPSOrganismLookupId))
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(TextBox_InsertOrganismNameLookup);

        Label_InsertInvalidFormMessage.Text = Convert.ToString("Organism code does not exist", CultureInfo.CurrentCulture);

        DropDownList_InsertOrganismNameLookup.SelectedValue = IPSOrganismLookupId;
      }
      else
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_InsertOrganismNameLookup);

        Label_InsertInvalidFormMessage.Text = "";

        DropDownList_InsertOrganismNameLookup.SelectedValue = IPSOrganismLookupId;

        if (IPSOrganismLookupNotifiable == "True")
        {
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = true;
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = true;
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = true;
        }
        else
        {
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = false;
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = false;
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = false;

          CheckBox CheckBox_InsertOrganismNotifiableDepartmentOfHealth = (CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_InsertOrganismNotifiableDepartmentOfHealth");
          TextBox TextBox_InsertOrganismNotifiableDepartmentOfHealthDate = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNotifiableDepartmentOfHealthDate");
          TextBox TextBox_InsertOrganismNotifiableDepartmentOfHealthReferenceNumber = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNotifiableDepartmentOfHealthReferenceNumber");

          CheckBox_InsertOrganismNotifiableDepartmentOfHealth.Checked = false;
          TextBox_InsertOrganismNotifiableDepartmentOfHealthDate.Text = "";
          TextBox_InsertOrganismNotifiableDepartmentOfHealthReferenceNumber.Text = "";
        }

        if (IPSOrganismLookupResistance == "True")
        {
          FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = true;
        }
        else
        {
          FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = false;

          DropDownList DropDownList_InsertOrganismResistanceList = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_InsertOrganismResistanceList");

          DropDownList_InsertOrganismResistanceList.SelectedValue = "";
        }
      }
    }

    protected void DropDownList_InsertOrganismNameLookup_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertOrganismNameLookup = (DropDownList)sender;
      TextBox TextBox_InsertOrganismNameLookup = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNameLookup");
      Label Label_InsertInvalidFormMessage = (Label)FormView_IPS_Organism_Form.FindControl("Label_InsertInvalidFormMessage");

      string IPSOrganismLookupCode = "";
      string IPSOrganismLookupNotifiable = "";
      string IPSOrganismLookupResistance = "";
      string SQLStringOrganismLookup = "SELECT IPS_Organism_Lookup_Code , IPS_Organism_Lookup_Notifiable , IPS_Organism_Lookup_Resistance FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Id = @IPS_Organism_Lookup_Id AND IPS_Organism_Lookup_IsActive = 1";
      using (SqlCommand SqlCommand_OrganismLookup = new SqlCommand(SQLStringOrganismLookup))
      {
        SqlCommand_OrganismLookup.Parameters.AddWithValue("@IPS_Organism_Lookup_Id", DropDownList_InsertOrganismNameLookup.SelectedValue.ToString());
        DataTable DataTable_OrganismLookup;
        using (DataTable_OrganismLookup = new DataTable())
        {
          DataTable_OrganismLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_OrganismLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OrganismLookup).Copy();
          if (DataTable_OrganismLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OrganismLookup.Rows)
            {
              IPSOrganismLookupCode = DataRow_Row["IPS_Organism_Lookup_Code"].ToString();
              IPSOrganismLookupNotifiable = DataRow_Row["IPS_Organism_Lookup_Notifiable"].ToString();
              IPSOrganismLookupResistance = DataRow_Row["IPS_Organism_Lookup_Resistance"].ToString();
            }
          }
        }
      }

      ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_InsertOrganismNameLookup);

      Label_InsertInvalidFormMessage.Text = "";
      TextBox_InsertOrganismNameLookup.Text = IPSOrganismLookupCode;

      if (IPSOrganismLookupNotifiable == "True")
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = true;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = true;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = true;
      }
      else
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = false;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = false;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = false;

        CheckBox CheckBox_InsertOrganismNotifiableDepartmentOfHealth = (CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_InsertOrganismNotifiableDepartmentOfHealth");
        TextBox TextBox_InsertOrganismNotifiableDepartmentOfHealthDate = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNotifiableDepartmentOfHealthDate");
        TextBox TextBox_InsertOrganismNotifiableDepartmentOfHealthReferenceNumber = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_InsertOrganismNotifiableDepartmentOfHealthReferenceNumber");

        CheckBox_InsertOrganismNotifiableDepartmentOfHealth.Checked = false;
        TextBox_InsertOrganismNotifiableDepartmentOfHealthDate.Text = "";
        TextBox_InsertOrganismNotifiableDepartmentOfHealthReferenceNumber.Text = "";
      }

      if (IPSOrganismLookupResistance == "True")
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = true;
      }
      else
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = false;

        DropDownList DropDownList_InsertOrganismResistanceList = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_InsertOrganismResistanceList");

        DropDownList_InsertOrganismResistanceList.SelectedValue = "";
      }
    }

    protected void HiddenField_InsertRMMechanismItemListTotal_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_InsertRMMechanismItemListTotal = (HiddenField)sender;
      CheckBoxList CheckBoxList_InsertRMMechanismItemList = (CheckBoxList)FormView_IPS_Organism_Form.FindControl("CheckBoxList_InsertRMMechanismItemList");
      HiddenField_InsertRMMechanismItemListTotal.Value = CheckBoxList_InsertRMMechanismItemList.Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void Button_InsertOrganismInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void EditRegisterPostBackControl_CurrentOrganism()
    {
      TextBox TextBox_EditOrganismNameLookup = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNameLookup");
      DropDownList DropDownList_EditOrganismNameLookup = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_EditOrganismNameLookup");

      ScriptManager ScriptManager_Edit = ScriptManager.GetCurrent(Page);

      ScriptManager_Edit.RegisterPostBackControl(TextBox_EditOrganismNameLookup);
      ScriptManager_Edit.RegisterPostBackControl(DropDownList_EditOrganismNameLookup);
    }

    protected void TextBox_EditOrganismNameLookup_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditOrganismNameLookup = (TextBox)sender;
      DropDownList DropDownList_EditOrganismNameLookup = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_EditOrganismNameLookup");
      Label Label_EditInvalidFormMessage = (Label)FormView_IPS_Organism_Form.FindControl("Label_EditInvalidFormMessage");

      string IPSOrganismLookupId = "";
      string IPSOrganismLookupNotifiable = "";
      string IPSOrganismLookupResistance = "";
      string SQLStringOrganismLookup = "SELECT IPS_Organism_Lookup_Id , IPS_Organism_Lookup_Notifiable , IPS_Organism_Lookup_Resistance FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Code = @IPS_Organism_Lookup_Code AND IPS_Organism_Lookup_IsActive = 1";
      using (SqlCommand SqlCommand_OrganismLookup = new SqlCommand(SQLStringOrganismLookup))
      {
        SqlCommand_OrganismLookup.Parameters.AddWithValue("@IPS_Organism_Lookup_Code", TextBox_EditOrganismNameLookup.Text.ToString());
        DataTable DataTable_OrganismLookup;
        using (DataTable_OrganismLookup = new DataTable())
        {
          DataTable_OrganismLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_OrganismLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OrganismLookup).Copy();
          if (DataTable_OrganismLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OrganismLookup.Rows)
            {
              IPSOrganismLookupId = DataRow_Row["IPS_Organism_Lookup_Id"].ToString();
              IPSOrganismLookupNotifiable = DataRow_Row["IPS_Organism_Lookup_Notifiable"].ToString();
              IPSOrganismLookupResistance = DataRow_Row["IPS_Organism_Lookup_Resistance"].ToString();
            }
          }
        }
      }

      if (string.IsNullOrEmpty(IPSOrganismLookupId))
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(TextBox_EditOrganismNameLookup);

        Label_EditInvalidFormMessage.Text = Convert.ToString("Organism code does not exist", CultureInfo.CurrentCulture);

        DropDownList_EditOrganismNameLookup.SelectedValue = IPSOrganismLookupId;
      }
      else
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_EditOrganismNameLookup);

        Label_EditInvalidFormMessage.Text = "";

        DropDownList_EditOrganismNameLookup.SelectedValue = IPSOrganismLookupId;

        if (IPSOrganismLookupNotifiable == "True")
        {
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = true;
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = true;
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = true;
        }
        else
        {
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = false;
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = false;
          FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = false;

          CheckBox CheckBox_EditOrganismNotifiableDepartmentOfHealth = (CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_EditOrganismNotifiableDepartmentOfHealth");
          TextBox TextBox_EditOrganismNotifiableDepartmentOfHealthDate = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthDate");
          TextBox TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber");

          CheckBox_EditOrganismNotifiableDepartmentOfHealth.Checked = false;
          TextBox_EditOrganismNotifiableDepartmentOfHealthDate.Text = "";
          TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber.Text = "";
        }

        if (IPSOrganismLookupResistance == "True")
        {
          FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = true;
        }
        else
        {
          FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = false;

          DropDownList DropDownList_EditOrganismResistanceList = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_EditOrganismResistanceList");

          DropDownList_EditOrganismResistanceList.SelectedValue = "";
        }
      }
    }

    protected void DropDownList_EditOrganismNameLookup_DataBound(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditOrganismNameLookup = (DropDownList)sender;
      TextBox TextBox_EditOrganismNameLookup = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNameLookup");
      Label Label_EditInvalidFormMessage = (Label)FormView_IPS_Organism_Form.FindControl("Label_EditInvalidFormMessage");

      string IPSOrganismLookupCode = "";
      string IPSOrganismLookupNotifiable = "";
      string IPSOrganismLookupResistance = "";
      string SQLStringOrganismLookup = "SELECT IPS_Organism_Lookup_Code , IPS_Organism_Lookup_Notifiable , IPS_Organism_Lookup_Resistance FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Id = @IPS_Organism_Lookup_Id AND IPS_Organism_Lookup_IsActive = 1";
      using (SqlCommand SqlCommand_OrganismLookup = new SqlCommand(SQLStringOrganismLookup))
      {
        SqlCommand_OrganismLookup.Parameters.AddWithValue("@IPS_Organism_Lookup_Id", DropDownList_EditOrganismNameLookup.SelectedValue.ToString());
        DataTable DataTable_OrganismLookup;
        using (DataTable_OrganismLookup = new DataTable())
        {
          DataTable_OrganismLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_OrganismLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OrganismLookup).Copy();
          if (DataTable_OrganismLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OrganismLookup.Rows)
            {
              IPSOrganismLookupCode = DataRow_Row["IPS_Organism_Lookup_Code"].ToString();
              IPSOrganismLookupNotifiable = DataRow_Row["IPS_Organism_Lookup_Notifiable"].ToString();
              IPSOrganismLookupResistance = DataRow_Row["IPS_Organism_Lookup_Resistance"].ToString();
            }
          }
        }
      }

      ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_EditOrganismNameLookup);

      Label_EditInvalidFormMessage.Text = "";
      TextBox_EditOrganismNameLookup.Text = IPSOrganismLookupCode;

      if (IPSOrganismLookupNotifiable == "True")
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = true;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = true;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = true;
      }
      else
      {
        CheckBox CheckBox_EditOrganismNotifiableDepartmentOfHealth = (CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_EditOrganismNotifiableDepartmentOfHealth");
        TextBox TextBox_EditOrganismNotifiableDepartmentOfHealthDate = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthDate");
        TextBox TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber");

        CheckBox_EditOrganismNotifiableDepartmentOfHealth.Checked = false;
        TextBox_EditOrganismNotifiableDepartmentOfHealthDate.Text = "";
        TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber.Text = "";

        FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = false;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = false;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = false;
      }

      if (IPSOrganismLookupResistance == "True")
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = true;
      }
      else
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = false;

        DropDownList DropDownList_EditOrganismResistanceList = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_EditOrganismResistanceList");

        DropDownList_EditOrganismResistanceList.SelectedValue = "";
      }
    }

    protected void DropDownList_EditOrganismNameLookup_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditOrganismNameLookup = (DropDownList)sender;
      TextBox TextBox_EditOrganismNameLookup = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNameLookup");
      Label Label_EditInvalidFormMessage = (Label)FormView_IPS_Organism_Form.FindControl("Label_EditInvalidFormMessage");

      string IPSOrganismLookupCode = "";
      string IPSOrganismLookupNotifiable = "";
      string IPSOrganismLookupResistance = "";
      string SQLStringOrganismLookup = "SELECT IPS_Organism_Lookup_Code , IPS_Organism_Lookup_Notifiable , IPS_Organism_Lookup_Resistance FROM Form_IPS_Organism_Lookup WHERE IPS_Organism_Lookup_Id = @IPS_Organism_Lookup_Id AND IPS_Organism_Lookup_IsActive = 1";
      using (SqlCommand SqlCommand_OrganismLookup = new SqlCommand(SQLStringOrganismLookup))
      {
        SqlCommand_OrganismLookup.Parameters.AddWithValue("@IPS_Organism_Lookup_Id", DropDownList_EditOrganismNameLookup.SelectedValue.ToString());
        DataTable DataTable_OrganismLookup;
        using (DataTable_OrganismLookup = new DataTable())
        {
          DataTable_OrganismLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_OrganismLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OrganismLookup).Copy();
          if (DataTable_OrganismLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OrganismLookup.Rows)
            {
              IPSOrganismLookupCode = DataRow_Row["IPS_Organism_Lookup_Code"].ToString();
              IPSOrganismLookupNotifiable = DataRow_Row["IPS_Organism_Lookup_Notifiable"].ToString();
              IPSOrganismLookupResistance = DataRow_Row["IPS_Organism_Lookup_Resistance"].ToString();
            }
          }
        }
      }

      ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_EditOrganismNameLookup);

      Label_EditInvalidFormMessage.Text = "";
      TextBox_EditOrganismNameLookup.Text = IPSOrganismLookupCode;

      if (IPSOrganismLookupNotifiable == "True")
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = true;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = true;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = true;
      }
      else
      {
        CheckBox CheckBox_EditOrganismNotifiableDepartmentOfHealth = (CheckBox)FormView_IPS_Organism_Form.FindControl("CheckBox_EditOrganismNotifiableDepartmentOfHealth");
        TextBox TextBox_EditOrganismNotifiableDepartmentOfHealthDate = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthDate");
        TextBox TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber = (TextBox)FormView_IPS_Organism_Form.FindControl("TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber");

        CheckBox_EditOrganismNotifiableDepartmentOfHealth.Checked = false;
        TextBox_EditOrganismNotifiableDepartmentOfHealthDate.Text = "";
        TextBox_EditOrganismNotifiableDepartmentOfHealthReferenceNumber.Text = "";

        FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = false;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = false;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = false;
      }

      if (IPSOrganismLookupResistance == "True")
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = true;
      }
      else
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = false;

        DropDownList DropDownList_EditOrganismResistanceList = (DropDownList)FormView_IPS_Organism_Form.FindControl("DropDownList_EditOrganismResistanceList");

        DropDownList_EditOrganismResistanceList.SelectedValue = "";
      }
    }

    protected void CheckBoxList_EditRMMechanismItemList_DataBound(object sender, EventArgs e)
    {
      CheckBoxList CheckBoxList_EditRMMechanismItemList = (CheckBoxList)sender;

      for (int i = 0; i < CheckBoxList_EditRMMechanismItemList.Items.Count; i++)
      {
        Session["IPSRMMechanismItemList"] = "";
        string SQLStringIPSRMMechanismItemList = "SELECT DISTINCT IPS_RM_Mechanism_Item_List FROM Form_IPS_Organism_ResistanceMechanism WHERE IPS_Organism_Id = @IPS_Organism_Id AND IPS_RM_Mechanism_Item_List = @IPS_RM_Mechanism_Item_List";
        using (SqlCommand SqlCommand_IPSRMMechanismItemList = new SqlCommand(SQLStringIPSRMMechanismItemList))
        {
          SqlCommand_IPSRMMechanismItemList.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
          SqlCommand_IPSRMMechanismItemList.Parameters.AddWithValue("@IPS_RM_Mechanism_Item_List", CheckBoxList_EditRMMechanismItemList.Items[i].Value);
          DataTable DataTable_IPSRMMechanismItemList;
          using (DataTable_IPSRMMechanismItemList = new DataTable())
          {
            DataTable_IPSRMMechanismItemList.Locale = CultureInfo.CurrentCulture;
            DataTable_IPSRMMechanismItemList = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_IPSRMMechanismItemList).Copy();
            if (DataTable_IPSRMMechanismItemList.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_IPSRMMechanismItemList.Rows)
              {
                Session["IPSRMMechanismItemList"] = DataRow_Row["IPS_RM_Mechanism_Item_List"];
                CheckBoxList_EditRMMechanismItemList.Items[i].Selected = true;
              }
            }
          }
        }

        Session["IPSRMMechanismItemList"] = "";
      }
    }

    protected void HiddenField_EditRMMechanismItemListTotal_DataBinding(object sender, EventArgs e)
    {
      HiddenField HiddenField_EditRMMechanismItemListTotal = (HiddenField)sender;
      CheckBoxList CheckBoxList_EditRMMechanismItemList = (CheckBoxList)FormView_IPS_Organism_Form.FindControl("CheckBoxList_EditRMMechanismItemList");
      HiddenField_EditRMMechanismItemListTotal.Value = CheckBoxList_EditRMMechanismItemList.Items.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void Button_EditOrganismNewSpecimenResult_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "#CurrentSpecimenResult"), false);
    }

    protected void Button_EditOrganismNew_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + Request.QueryString["IPSSpecimenResultId"] + "#CurrentOrganism"), false);
    }

    protected void Button_EditOrganismInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void Label_ItemOrganismNameLookup_DataBinding(object sender, EventArgs e)
    {
      string IPSOrganismLookupNotifiable = "";
      string IPSOrganismLookupResistance = "";
      string SQLStringOrganismLookup = "SELECT IPS_Organism_Lookup_Notifiable , IPS_Organism_Lookup_Resistance FROM vForm_IPS_Organism WHERE IPS_Organism_Id = @IPS_Organism_Id";
      using (SqlCommand SqlCommand_OrganismLookup = new SqlCommand(SQLStringOrganismLookup))
      {
        SqlCommand_OrganismLookup.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
        DataTable DataTable_OrganismLookup;
        using (DataTable_OrganismLookup = new DataTable())
        {
          DataTable_OrganismLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_OrganismLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_OrganismLookup).Copy();
          if (DataTable_OrganismLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_OrganismLookup.Rows)
            {
              IPSOrganismLookupNotifiable = DataRow_Row["IPS_Organism_Lookup_Notifiable"].ToString();
              IPSOrganismLookupResistance = DataRow_Row["IPS_Organism_Lookup_Resistance"].ToString();
            }
          }
        }
      }

      if (IPSOrganismLookupNotifiable == "True")
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = true;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = true;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = true;
      }
      else
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH1").Visible = false;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH2").Visible = false;
        FormView_IPS_Organism_Form.FindControl("ShowHideDOH3").Visible = false;
      }

      if (IPSOrganismLookupResistance == "True")
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = true;
      }
      else
      {
        FormView_IPS_Organism_Form.FindControl("ShowHideResistance").Visible = false;
      }
    }

    protected void SqlDataSource_IPS_ItemOrganismResistanceMechanism_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Int32 TotalRecords = e.AffectedRows;
        GridView GridView_ItemOrganismResistanceMechanism = (GridView)FormView_IPS_Organism_Form.FindControl("GridView_ItemOrganismResistanceMechanism");

        if (TotalRecords > 0)
        {
          GridView_ItemOrganismResistanceMechanism.Visible = true;
        }
        else
        {
          GridView_ItemOrganismResistanceMechanism.Visible = false;
        }
      }
    }

    protected void GridView_ItemOrganismResistanceMechanism_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_ItemOrganismResistanceMechanism_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_ItemOrganismInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }
    //---END--- --CurrentOrganism--//


    //--START-- --Antibiogram--//
    protected void SqlDataSource_IPS_Antibiogram_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenAntibiogramTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);


        FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_Current = GetSpecimenCompleted();
        string Specimen = FromDataBase_SpecimenCompleted_Current.Specimen;

        if (Specimen == "Incomplete")
        {
          TableCurrentInfectionComplete.Visible = false; DivCurrentInfectionComplete.Visible = false;
        }
        else
        {
          TableCurrentInfectionComplete.Visible = true; DivCurrentInfectionComplete.Visible = true;
        }

        if (TableCurrentInfectionComplete.Visible == true)
        {
          TableCurrentInfectionCompleteVisible();
        }
      }
    }

    protected void SqlDataSource_IPS_Antibiogram_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          TableInfoVisible();
          SqlDataSource_IPS_Specimen_List.DataBind();
          GridView_IPS_Specimen_List.DataBind();
          SqlDataSource_IPS_SpecimenResult_List.DataBind();
          GridView_IPS_SpecimenResult_List.DataBind();
          SqlDataSource_IPS_Organism_List.DataBind();
          GridView_IPS_Organism_List.DataBind();

          ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", "<script language='javascript'>alert('Antibiogram has been updated');</script>", false);
        }
      }
    }

    protected void GridView_IPS_Antibiogram_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_IPS_Antibiogram_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_AntibiogramTotalRecords = (Label)e.Row.FindControl("Label_AntibiogramTotalRecords");
          Label_AntibiogramTotalRecords.Text = Label_HiddenAntibiogramTotalRecords.Text;
        }

        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Button Button_AntibiogramCompleteInfection = (Button)e.Row.FindControl("Button_AntibiogramCompleteInfection");

          FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_Current = GetSpecimenCompleted();
          string Specimen = FromDataBase_SpecimenCompleted_Current.Specimen;

          if (Specimen == "Incomplete")
          {
            Button_AntibiogramCompleteInfection.Enabled = false;
            Button_AntibiogramCompleteInfection.Text = Convert.ToString("Infection Incomplete", CultureInfo.CurrentCulture);
          }
          else
          {
            Button_AntibiogramCompleteInfection.Enabled = true;
            Button_AntibiogramCompleteInfection.Text = Convert.ToString("Complete Infection", CultureInfo.CurrentCulture);
          }
        }
      }
    }

    protected void GridView_IPS_Antibiogram_DataBound(object sender, EventArgs e)
    {
      for (int i = 0; i < GridView_IPS_Antibiogram.Rows.Count; i++)
      {
        for (int a = 1; a <= 10; a++)
        {
          SqlDataSource SqlDataSource_IPS_InsertAntibiogramNameLookup = (SqlDataSource)GridView_IPS_Antibiogram.Rows[i].Cells[0].FindControl("SqlDataSource_IPS_InsertAntibiogramNameLookup_" + a);
          if (SqlDataSource_IPS_InsertAntibiogramNameLookup != null)
          {
            SqlDataSource_IPS_InsertAntibiogramNameLookup.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
            SqlDataSource_IPS_InsertAntibiogramNameLookup.SelectCommand = "SELECT IPS_Antibiogram_Lookup_Id ,IPS_Antibiogram_Lookup_Description + ' (' + IPS_Antibiogram_Lookup_Code + ')' AS IPS_Antibiogram_Lookup FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_IsActive = 1 ORDER BY IPS_Antibiogram_Lookup_Description";
            SqlDataSource_IPS_InsertAntibiogramNameLookup.DataBind();
          }
        }

        HiddenField HiddenField_EditAntibiogramId = (HiddenField)GridView_IPS_Antibiogram.Rows[i].Cells[0].FindControl("HiddenField_EditAntibiogramId"); 
        SqlDataSource SqlDataSource_IPS_EditAntibiogramNameLookup = (SqlDataSource)GridView_IPS_Antibiogram.Rows[i].Cells[0].FindControl("SqlDataSource_IPS_EditAntibiogramNameLookup");
        if (SqlDataSource_IPS_EditAntibiogramNameLookup != null)
        {
          SqlDataSource_IPS_EditAntibiogramNameLookup.SelectParameters["IPS_Antibiogram_Id"].DefaultValue = HiddenField_EditAntibiogramId.Value;
          SqlDataSource_IPS_EditAntibiogramNameLookup.DataBind();
        }
      }
    }

    protected void Button_AntibiogramInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }

    protected void Button_AntibiogramCompleteInfection_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSSpecimenId=" + Request.QueryString["IPSSpecimenId"] + "&IPSSpecimenResultId=" + Request.QueryString["IPSSpecimenResultId"] + "&IPSOrganismId=" + Request.QueryString["IPSOrganismId"] + "#CurrentAntibiogram"), false);
    }


    protected void TextBox_InsertAntibiogramCode_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertAntibiogramCode = (TextBox)sender;

      string AntibiogramContol = TextBox_InsertAntibiogramCode.ClientID;
      Int32 LastIndexOfCharacter = AntibiogramContol.LastIndexOf("_", StringComparison.CurrentCulture);
      AntibiogramContol = AntibiogramContol.Remove(0, LastIndexOfCharacter);

      GridViewRow GridViewRow_IPS_Antibiogram = (GridViewRow)TextBox_InsertAntibiogramCode.NamingContainer;
      DropDownList DropDownList_InsertAntibiogramNameLookup = (DropDownList)GridViewRow_IPS_Antibiogram.FindControl("DropDownList_InsertAntibiogramNameLookup" + AntibiogramContol);
      Label Label_InsertValidationMessage = (Label)GridViewRow_IPS_Antibiogram.FindControl("Label_InsertValidationMessage" + AntibiogramContol);

      string IPSAntibiogramLookupId = "";
      string SQLStringAntibiogramLookup = "SELECT IPS_Antibiogram_Lookup_Id FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_Code = @IPS_Antibiogram_Lookup_Code AND IPS_Antibiogram_Lookup_IsActive = 1";
      using (SqlCommand SqlCommand_AntibiogramLookup = new SqlCommand(SQLStringAntibiogramLookup))
      {
        SqlCommand_AntibiogramLookup.Parameters.AddWithValue("@IPS_Antibiogram_Lookup_Code", TextBox_InsertAntibiogramCode.Text.ToString());
        DataTable DataTable_AntibiogramLookup;
        using (DataTable_AntibiogramLookup = new DataTable())
        {
          DataTable_AntibiogramLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_AntibiogramLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntibiogramLookup).Copy();
          if (DataTable_AntibiogramLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AntibiogramLookup.Rows)
            {
              IPSAntibiogramLookupId = DataRow_Row["IPS_Antibiogram_Lookup_Id"].ToString();
            }
          }
        }
      }

      if (string.IsNullOrEmpty(IPSAntibiogramLookupId))
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(TextBox_InsertAntibiogramCode);

        Label_InsertValidationMessage.Visible = true;
        Label_InsertValidationMessage.Text = Convert.ToString("Antibiogram code does not exist", CultureInfo.CurrentCulture);

        DropDownList_InsertAntibiogramNameLookup.SelectedValue = IPSAntibiogramLookupId;
      }
      else
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_InsertAntibiogramNameLookup);

        Label_InsertValidationMessage.Visible = false;
        Label_InsertValidationMessage.Text = "";

        DropDownList_InsertAntibiogramNameLookup.SelectedValue = IPSAntibiogramLookupId;
      }
    }

    protected void DropDownList_InsertAntibiogramNameLookup_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertAntibiogramNameLookup = (DropDownList)sender;

      string AntibiogramContol = DropDownList_InsertAntibiogramNameLookup.ClientID;
      Int32 LastIndexOfCharacter = AntibiogramContol.LastIndexOf("_", StringComparison.CurrentCulture);
      AntibiogramContol = AntibiogramContol.Remove(0, LastIndexOfCharacter);

      GridViewRow GridViewRow_IPS_Antibiogram = (GridViewRow)DropDownList_InsertAntibiogramNameLookup.NamingContainer;
      TextBox TextBox_InsertAntibiogramCode = (TextBox)GridViewRow_IPS_Antibiogram.FindControl("TextBox_InsertAntibiogramCode" + AntibiogramContol);
      Label Label_InsertValidationMessage = (Label)GridViewRow_IPS_Antibiogram.FindControl("Label_InsertValidationMessage" + AntibiogramContol);

      string IPSAntibiogramLookupCode = "";
      string SQLStringAntibiogramLookup = "SELECT IPS_Antibiogram_Lookup_Code FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_Id = @IPS_Antibiogram_Lookup_Id AND IPS_Antibiogram_Lookup_IsActive = 1";
      using (SqlCommand SqlCommand_AntibiogramLookup = new SqlCommand(SQLStringAntibiogramLookup))
      {
        SqlCommand_AntibiogramLookup.Parameters.AddWithValue("@IPS_Antibiogram_Lookup_Id", DropDownList_InsertAntibiogramNameLookup.SelectedValue.ToString());
        DataTable DataTable_AntibiogramLookup;
        using (DataTable_AntibiogramLookup = new DataTable())
        {
          DataTable_AntibiogramLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_AntibiogramLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntibiogramLookup).Copy();
          if (DataTable_AntibiogramLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AntibiogramLookup.Rows)
            {
              IPSAntibiogramLookupCode = DataRow_Row["IPS_Antibiogram_Lookup_Code"].ToString();
            }
          }
        }
      }

      ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_InsertAntibiogramNameLookup);

      Label_InsertValidationMessage.Visible = false;
      Label_InsertValidationMessage.Text = "";
      TextBox_InsertAntibiogramCode.Text = IPSAntibiogramLookupCode;
    }

    protected void DropDownList_InsertAntibiogramNameLookup_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertAntibiogramNameLookup = (DropDownList)sender;
      GridViewRow GridViewRow_IPS_Antibiogram = (GridViewRow)DropDownList_InsertAntibiogramNameLookup.NamingContainer;

      for (int a = 1; a <= 10; a++)
      {
        SqlDataSource SqlDataSource_IPS_InsertAntibiogramNameLookup = (SqlDataSource)GridViewRow_IPS_Antibiogram.FindControl("SqlDataSource_IPS_InsertAntibiogramNameLookup_" + a);
        SqlDataSource_IPS_InsertAntibiogramNameLookup.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_IPS_InsertAntibiogramNameLookup.SelectCommand = "SELECT IPS_Antibiogram_Lookup_Id ,IPS_Antibiogram_Lookup_Description + ' (' + IPS_Antibiogram_Lookup_Code + ')' AS IPS_Antibiogram_Lookup FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_IsActive = 1 ORDER BY IPS_Antibiogram_Lookup_Description";
        SqlDataSource_IPS_InsertAntibiogramNameLookup.DataBind();
      }
    }

    protected void Button_InsertAntibiogram_OnClick(object sender, EventArgs e)
    {
      string InsertValidation_Antibiogram_Valid = "Yes";

      for (int a = 1; a <= 10; a++)
      {
        Button Button_InsertAntibiogram = (Button)sender;
        GridViewRow GridView_IPS_Antibiogram_Current = (GridViewRow)Button_InsertAntibiogram.NamingContainer;

        Label Label_InsertValidationMessage = (Label)GridView_IPS_Antibiogram_Current.FindControl("Label_InsertValidationMessage_" + a);
        HiddenField HiddenField_InsertAntibiogramInserted = (HiddenField)GridView_IPS_Antibiogram_Current.FindControl("HiddenField_InsertAntibiogramInserted_" + a);
        DropDownList DropDownList_InsertAntibiogramNameLookup = (DropDownList)GridView_IPS_Antibiogram_Current.FindControl("DropDownList_InsertAntibiogramNameLookup_" + a);
        RadioButtonList RadioButtonList_InsertAntibiogramSRI = (RadioButtonList)GridView_IPS_Antibiogram_Current.FindControl("RadioButtonList_InsertAntibiogramSRI_" + a);

        string SaveValidation_Insert_Message = InsertValidation_Antibiogram(GridView_IPS_Antibiogram_Current, a);

        if (!string.IsNullOrEmpty(DropDownList_InsertAntibiogramNameLookup.SelectedValue) && !string.IsNullOrEmpty(RadioButtonList_InsertAntibiogramSRI.SelectedValue))
        {
          if (!string.IsNullOrEmpty(SaveValidation_Insert_Message))
          {
            InsertValidation_Antibiogram_Valid = "No";
            Label_InsertValidationMessage.Visible = true;
            Label_InsertValidationMessage.Text = SaveValidation_Insert_Message;
          }
          else
          {
            Label_InsertValidationMessage.Visible = false;
            Label_InsertValidationMessage.Text = "";

            if (HiddenField_InsertAntibiogramInserted.Value != "Yes")
            {
              string SQLStringInsertAntibiogram = "INSERT INTO Form_IPS_Antibiogram ( IPS_Organism_Id ,IPS_Antibiogram_Name_Lookup ,IPS_Antibiogram_SRI_List ,IPS_Antibiogram_CreatedDate ,IPS_Antibiogram_CreatedBy ,IPS_Antibiogram_ModifiedDate ,IPS_Antibiogram_ModifiedBy ,IPS_Antibiogram_History ,IPS_Antibiogram_IsActive ) VALUES ( @IPS_Organism_Id ,@IPS_Antibiogram_Name_Lookup ,@IPS_Antibiogram_SRI_List ,@IPS_Antibiogram_CreatedDate ,@IPS_Antibiogram_CreatedBy ,@IPS_Antibiogram_ModifiedDate ,@IPS_Antibiogram_ModifiedBy ,@IPS_Antibiogram_History ,@IPS_Antibiogram_IsActive )";
              using (SqlCommand SqlCommand_InsertAntibiogram = new SqlCommand(SQLStringInsertAntibiogram))
              {
                SqlCommand_InsertAntibiogram.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
                SqlCommand_InsertAntibiogram.Parameters.AddWithValue("@IPS_Antibiogram_Name_Lookup", DropDownList_InsertAntibiogramNameLookup.SelectedValue);
                SqlCommand_InsertAntibiogram.Parameters.AddWithValue("@IPS_Antibiogram_SRI_List", RadioButtonList_InsertAntibiogramSRI.SelectedValue);
                SqlCommand_InsertAntibiogram.Parameters.AddWithValue("@IPS_Antibiogram_CreatedDate", DateTime.Now.ToString());
                SqlCommand_InsertAntibiogram.Parameters.AddWithValue("@IPS_Antibiogram_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                SqlCommand_InsertAntibiogram.Parameters.AddWithValue("@IPS_Antibiogram_ModifiedDate", DateTime.Now.ToString());
                SqlCommand_InsertAntibiogram.Parameters.AddWithValue("@IPS_Antibiogram_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                SqlCommand_InsertAntibiogram.Parameters.AddWithValue("@IPS_Antibiogram_History", "");
                SqlCommand_InsertAntibiogram.Parameters.AddWithValue("@IPS_Antibiogram_IsActive", "true");
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertAntibiogram);
              }

              HiddenField_InsertAntibiogramInserted.Value = "Yes";
            }
          }
        }
      }


      if (InsertValidation_Antibiogram_Valid == "Yes")
      {
        TableInfoVisible();
        SqlDataSource_IPS_Specimen_List.DataBind();
        GridView_IPS_Specimen_List.DataBind();
        SqlDataSource_IPS_SpecimenResult_List.DataBind();
        GridView_IPS_SpecimenResult_List.DataBind();
        SqlDataSource_IPS_Organism_List.DataBind();
        GridView_IPS_Organism_List.DataBind();
        SqlDataSource_IPS_Antibiogram.DataBind();
        GridView_IPS_Antibiogram.DataBind();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", "<script language='javascript'>alert('Antibiogram has been inserted');</script>", false);
      }
    }

    protected string InsertValidation_Antibiogram(Control gridView_IPS_Antibiogram_Current, Int32 insertAntibiogramControl)
    {
      string InvalidAntibiogramMessage = "";

      if (gridView_IPS_Antibiogram_Current != null)
      {
        HiddenField HiddenField_InsertAntibiogramInserted = (HiddenField)gridView_IPS_Antibiogram_Current.FindControl("HiddenField_InsertAntibiogramInserted_" + insertAntibiogramControl);
        DropDownList DropDownList_InsertAntibiogramNameLookup = (DropDownList)gridView_IPS_Antibiogram_Current.FindControl("DropDownList_InsertAntibiogramNameLookup_" + insertAntibiogramControl);
        RadioButtonList RadioButtonList_InsertAntibiogramSRI = (RadioButtonList)gridView_IPS_Antibiogram_Current.FindControl("RadioButtonList_InsertAntibiogramSRI_" + insertAntibiogramControl);

        if (HiddenField_InsertAntibiogramInserted.Value != "Yes")
        {
          if (!string.IsNullOrEmpty(DropDownList_InsertAntibiogramNameLookup.SelectedValue) && !string.IsNullOrEmpty(RadioButtonList_InsertAntibiogramSRI.SelectedValue))
          {
            //if (String.IsNullOrEmpty(DropDownList_InsertAntibiogramNameLookup.SelectedValue))
            //{
            //  InvalidAntibiogramMessage = InvalidAntibiogramMessage + Convert.ToString("Code or Description is Required<br/>", CultureInfo.CurrentCulture);
            //}
            //else
            //{
            //  //String IPSAntibiogramLookupId = "";
            //  //String SQLStringAntibiogramLookup = "SELECT IPS_Antibiogram_Lookup_Id FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_Code = @IPS_Antibiogram_Lookup_Code AND IPS_Antibiogram_Lookup_IsActive = 1";
            //  //using (SqlCommand SqlCommand_AntibiogramLookup = new SqlCommand(SQLStringAntibiogramLookup))
            //  //{
            //  //  SqlCommand_AntibiogramLookup.Parameters.AddWithValue("@IPS_Antibiogram_Lookup_Code", TextBox_InsertAntibiogramCode.Text.ToString());
            //  //  DataTable DataTable_AntibiogramLookup;
            //  //  using (DataTable_AntibiogramLookup = new DataTable())
            //  //  {
            //  //    DataTable_AntibiogramLookup.Locale = CultureInfo.CurrentCulture;

            //  //    DataTable_AntibiogramLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntibiogramLookup).Copy();
            //  //    if (DataTable_AntibiogramLookup.Rows.Count > 0)
            //  //    {
            //  //      foreach (DataRow DataRow_Row in DataTable_AntibiogramLookup.Rows)
            //  //      {
            //  //        IPSAntibiogramLookupId = DataRow_Row["IPS_Antibiogram_Lookup_Id"].ToString();
            //  //      }
            //  //    }
            //  //  }
            //  //}

            //  //if (String.IsNullOrEmpty(IPSAntibiogramLookupId))
            //  //{
            //  //  ToolkitScriptManager_IPS_Specimen.SetFocus(TextBox_InsertAntibiogramCode);

            //  //  InvalidAntibiogramMessage = InvalidAntibiogramMessage + Convert.ToString("Antibiogram code does not exist<br/>", CultureInfo.CurrentCulture);
            //  //}
            //}

            //if (String.IsNullOrEmpty(RadioButtonList_InsertAntibiogramSRI.SelectedValue))
            //{
            //  InvalidAntibiogramMessage = InvalidAntibiogramMessage + Convert.ToString("SRI is Required<br/>", CultureInfo.CurrentCulture);
            //}

            if (string.IsNullOrEmpty(InvalidAntibiogramMessage))
            {
              string IPSAntibiogramId = "";
              string IPSAntibiogramLookupDescription = "";
              string IPSAntibiogramSRIName = "";
              string SQLStringAntibiogram = "SELECT IPS_Antibiogram_Id , IPS_Antibiogram_Lookup_Description , IPS_Antibiogram_SRI_Name FROM vForm_IPS_Antibiogram WHERE IPS_Organism_Id = @IPS_Organism_Id AND IPS_Antibiogram_IsActive = 1 AND IPS_Antibiogram_Name_Lookup = @IPS_Antibiogram_Name_Lookup AND IPS_Antibiogram_SRI_List = @IPS_Antibiogram_SRI_List";
              using (SqlCommand SqlCommand_Antibiogram = new SqlCommand(SQLStringAntibiogram))
              {
                SqlCommand_Antibiogram.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
                SqlCommand_Antibiogram.Parameters.AddWithValue("@IPS_Antibiogram_Name_Lookup", DropDownList_InsertAntibiogramNameLookup.SelectedValue);
                SqlCommand_Antibiogram.Parameters.AddWithValue("@IPS_Antibiogram_SRI_List", RadioButtonList_InsertAntibiogramSRI.SelectedValue);
                DataTable DataTable_Antibiogram;
                using (DataTable_Antibiogram = new DataTable())
                {
                  DataTable_Antibiogram.Locale = CultureInfo.CurrentCulture;

                  DataTable_Antibiogram = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Antibiogram).Copy();
                  if (DataTable_Antibiogram.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_Antibiogram.Rows)
                    {
                      IPSAntibiogramId = DataRow_Row["IPS_Antibiogram_Id"].ToString();
                      IPSAntibiogramLookupDescription = DataRow_Row["IPS_Antibiogram_Lookup_Description"].ToString();
                      IPSAntibiogramSRIName = DataRow_Row["IPS_Antibiogram_SRI_Name"].ToString();
                    }
                  }
                }
              }

              if (!string.IsNullOrEmpty(IPSAntibiogramId))
              {
                ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_InsertAntibiogramNameLookup);

                InvalidAntibiogramMessage = InvalidAntibiogramMessage + Convert.ToString("An Antibiogram with the name '" + IPSAntibiogramLookupDescription + "' and SRI with name '" + IPSAntibiogramSRIName + "' already exists<br/>", CultureInfo.CurrentCulture);
              }
            }
          }
        }
      }

      return InvalidAntibiogramMessage;
    }


    protected void TextBox_EditAntibiogramCode_DataBinding(object sender, EventArgs e)
    {
      TextBox TextBox_EditAntibiogramCode = (TextBox)sender;
      GridViewRow GridViewRow_IPS_Antibiogram = (GridViewRow)TextBox_EditAntibiogramCode.NamingContainer;
      HiddenField HiddenField_EditAntibiogramId = (HiddenField)GridViewRow_IPS_Antibiogram.FindControl("HiddenField_EditAntibiogramId");

      string IPSAntibiogramLookupCode = "";
      string SQLStringAntibiogramLookup = "SELECT IPS_Antibiogram_Lookup_Code FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_Id IN ( SELECT IPS_Antibiogram_Name_Lookup FROM Form_IPS_Antibiogram WHERE IPS_Antibiogram_Id = @IPS_Antibiogram_Id )";
      using (SqlCommand SqlCommand_AntibiogramLookup = new SqlCommand(SQLStringAntibiogramLookup))
      {
        SqlCommand_AntibiogramLookup.Parameters.AddWithValue("@IPS_Antibiogram_Id", HiddenField_EditAntibiogramId.Value.ToString());
        DataTable DataTable_AntibiogramLookup;
        using (DataTable_AntibiogramLookup = new DataTable())
        {
          DataTable_AntibiogramLookup.Locale = CultureInfo.CurrentCulture;
          DataTable_AntibiogramLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntibiogramLookup).Copy();
          if (DataTable_AntibiogramLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AntibiogramLookup.Rows)
            {
              IPSAntibiogramLookupCode = DataRow_Row["IPS_Antibiogram_Lookup_Code"].ToString();
            }
          }
        }
      }

      TextBox_EditAntibiogramCode.Text = IPSAntibiogramLookupCode;
    }

    protected void TextBox_EditAntibiogramCode_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditAntibiogramCode = (TextBox)sender;
      GridViewRow GridViewRow_IPS_Antibiogram = (GridViewRow)TextBox_EditAntibiogramCode.NamingContainer;
      DropDownList DropDownList_EditAntibiogramNameLookup = (DropDownList)GridViewRow_IPS_Antibiogram.FindControl("DropDownList_EditAntibiogramNameLookup");
      Label Label_EditInvalidFormMessage = (Label)GridViewRow_IPS_Antibiogram.FindControl("Label_EditInvalidFormMessage");

      string IPSAntibiogramLookupId = "";
      string SQLStringAntibiogramLookup = "SELECT IPS_Antibiogram_Lookup_Id FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_Code = @IPS_Antibiogram_Lookup_Code AND IPS_Antibiogram_Lookup_IsActive = 1";
      using (SqlCommand SqlCommand_AntibiogramLookup = new SqlCommand(SQLStringAntibiogramLookup))
      {
        SqlCommand_AntibiogramLookup.Parameters.AddWithValue("@IPS_Antibiogram_Lookup_Code", TextBox_EditAntibiogramCode.Text.ToString());
        DataTable DataTable_AntibiogramLookup;
        using (DataTable_AntibiogramLookup = new DataTable())
        {
          DataTable_AntibiogramLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_AntibiogramLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntibiogramLookup).Copy();
          if (DataTable_AntibiogramLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AntibiogramLookup.Rows)
            {
              IPSAntibiogramLookupId = DataRow_Row["IPS_Antibiogram_Lookup_Id"].ToString();
            }
          }
        }
      }

      if (string.IsNullOrEmpty(IPSAntibiogramLookupId))
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(TextBox_EditAntibiogramCode);

        Label_EditInvalidFormMessage.Visible = true;
        Label_EditInvalidFormMessage.Text = Convert.ToString("Antibiogram code does not exist", CultureInfo.CurrentCulture);

        DropDownList_EditAntibiogramNameLookup.SelectedValue = IPSAntibiogramLookupId;
      }
      else
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_EditAntibiogramNameLookup);

        Label_EditInvalidFormMessage.Visible = false;
        Label_EditInvalidFormMessage.Text = "";

        DropDownList_EditAntibiogramNameLookup.SelectedValue = IPSAntibiogramLookupId;
      }
    }

    protected void DropDownList_EditAntibiogramNameLookup_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditAntibiogramNameLookup = (DropDownList)sender;
      GridViewRow GridViewRow_IPS_Antibiogram = (GridViewRow)DropDownList_EditAntibiogramNameLookup.NamingContainer;
      TextBox TextBox_EditAntibiogramCode = (TextBox)GridViewRow_IPS_Antibiogram.FindControl("TextBox_EditAntibiogramCode");
      Label Label_EditInvalidFormMessage = (Label)GridViewRow_IPS_Antibiogram.FindControl("Label_EditInvalidFormMessage");

      string IPSAntibiogramLookupCode = "";
      string SQLStringAntibiogramLookup = "SELECT IPS_Antibiogram_Lookup_Code FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_Id = @IPS_Antibiogram_Lookup_Id AND IPS_Antibiogram_Lookup_IsActive = 1";
      using (SqlCommand SqlCommand_AntibiogramLookup = new SqlCommand(SQLStringAntibiogramLookup))
      {
        SqlCommand_AntibiogramLookup.Parameters.AddWithValue("@IPS_Antibiogram_Lookup_Id", DropDownList_EditAntibiogramNameLookup.SelectedValue.ToString());
        DataTable DataTable_AntibiogramLookup;
        using (DataTable_AntibiogramLookup = new DataTable())
        {
          DataTable_AntibiogramLookup.Locale = CultureInfo.CurrentCulture;

          DataTable_AntibiogramLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntibiogramLookup).Copy();
          if (DataTable_AntibiogramLookup.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_AntibiogramLookup.Rows)
            {
              IPSAntibiogramLookupCode = DataRow_Row["IPS_Antibiogram_Lookup_Code"].ToString();
            }
          }
        }
      }

      ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_EditAntibiogramNameLookup);

      Label_EditInvalidFormMessage.Visible = false;
      Label_EditInvalidFormMessage.Text = "";
      TextBox_EditAntibiogramCode.Text = IPSAntibiogramLookupCode;
    }

    protected void DropDownList_EditAntibiogramNameLookup_DataBinding(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditAntibiogramNameLookup = (DropDownList)sender;
      GridViewRow GridViewRow_IPS_Antibiogram = (GridViewRow)DropDownList_EditAntibiogramNameLookup.NamingContainer;

      HiddenField HiddenField_EditAntibiogramId = (HiddenField)GridViewRow_IPS_Antibiogram.FindControl("HiddenField_EditAntibiogramId"); 
      SqlDataSource SqlDataSource_IPS_EditAntibiogramNameLookup = (SqlDataSource)GridViewRow_IPS_Antibiogram.FindControl("SqlDataSource_IPS_EditAntibiogramNameLookup");
      SqlDataSource_IPS_EditAntibiogramNameLookup.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_IPS_EditAntibiogramNameLookup.SelectCommand = "SELECT IPS_Antibiogram_Lookup_Id , IPS_Antibiogram_Lookup_Description , IPS_Antibiogram_Lookup_Description + ' (' + IPS_Antibiogram_Lookup_Code + ')' AS IPS_Antibiogram_Lookup FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_IsActive = 1 UNION SELECT IPS_Antibiogram_Name_Lookup , IPS_Antibiogram_Lookup_Description , IPS_Antibiogram_Lookup_Description + ' (' + IPS_Antibiogram_Lookup_Code + ')' AS IPS_Antibiogram_Lookup FROM Form_IPS_Antibiogram LEFT JOIN Form_IPS_Antibiogram_Lookup ON Form_IPS_Antibiogram.IPS_Antibiogram_Name_Lookup = Form_IPS_Antibiogram_Lookup.IPS_Antibiogram_Lookup_Id WHERE IPS_Antibiogram_Id = @IPS_Antibiogram_Id ORDER BY IPS_Antibiogram_Lookup_Description";
      SqlDataSource_IPS_EditAntibiogramNameLookup.SelectParameters.Clear();
      SqlDataSource_IPS_EditAntibiogramNameLookup.SelectParameters.Add("IPS_Antibiogram_Id", TypeCode.String, HiddenField_EditAntibiogramId.Value);
      SqlDataSource_IPS_EditAntibiogramNameLookup.DataBind();
    }

    protected void Button_EditAntibiogram_OnClick(object sender, EventArgs e)
    {
      Button Button_EditAntibiogram = (Button)sender;
      GridViewRow GridView_IPS_Antibiogram_Current = (GridViewRow)Button_EditAntibiogram.NamingContainer;
      
      HiddenField HiddenField_EditAntibiogramModifiedDate = (HiddenField)GridView_IPS_Antibiogram_Current.FindControl("HiddenField_EditAntibiogramModifiedDate");
      Session["OLDIPSAntibiogramModifiedDate"] = HiddenField_EditAntibiogramModifiedDate.Value;
      object OLDAntibiogramModifiedDate = Session["OLDIPSAntibiogramModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDAntibiogramModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      DataView DataView_CompareAntibiogram = (DataView)SqlDataSource_IPS_Antibiogram.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_CompareAntibiogram = DataView_CompareAntibiogram[GridView_IPS_Antibiogram_Current.RowIndex];
      Session["DBIPSAntibiogramModifiedDate"] = Convert.ToString(DataRowView_CompareAntibiogram["IPS_Antibiogram_ModifiedDate"], CultureInfo.CurrentCulture);
      Session["DBIPSAntibiogramModifiedBy"] = Convert.ToString(DataRowView_CompareAntibiogram["IPS_Antibiogram_ModifiedBy"], CultureInfo.CurrentCulture);
      object DBAntibiogramModifiedDate = Session["DBIPSAntibiogramModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBAntibiogramModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSAntibiogramModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        ((Label)GridView_IPS_Antibiogram_Current.FindControl("Label_EditInvalidFormMessage")).Text = "";
        ((Label)GridView_IPS_Antibiogram_Current.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_EditInvalidFormMessage = EditValidation_Antibiogram(GridView_IPS_Antibiogram_Current);

        if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
        {
          ((Label)GridView_IPS_Antibiogram_Current.FindControl("Label_EditInvalidFormMessage")).Visible = true;
          ((Label)GridView_IPS_Antibiogram_Current.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditInvalidFormMessage;
        }
        else
        {
          ((Label)GridView_IPS_Antibiogram_Current.FindControl("Label_EditInvalidFormMessage")).Visible = false;
          ((Label)GridView_IPS_Antibiogram_Current.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";

          DropDownList DropDownList_EditAntibiogramNameLookup = (DropDownList)GridView_IPS_Antibiogram_Current.FindControl("DropDownList_EditAntibiogramNameLookup");
          RadioButtonList RadioButtonList_EditAntibiogramSRI = (RadioButtonList)GridView_IPS_Antibiogram_Current.FindControl("RadioButtonList_EditAntibiogramSRI");
          CheckBox CheckBox_EditAntibiogramIsActive = (CheckBox)GridView_IPS_Antibiogram_Current.FindControl("CheckBox_EditAntibiogramIsActive");
          HiddenField HiddenField_EditAntibiogramId = (HiddenField)GridView_IPS_Antibiogram_Current.FindControl("HiddenField_EditAntibiogramId");

          SqlDataSource_IPS_Antibiogram.UpdateParameters["IPS_Antibiogram_Name_Lookup"].DefaultValue = DropDownList_EditAntibiogramNameLookup.SelectedValue;
          SqlDataSource_IPS_Antibiogram.UpdateParameters["IPS_Antibiogram_SRI_List"].DefaultValue = RadioButtonList_EditAntibiogramSRI.SelectedValue;
          SqlDataSource_IPS_Antibiogram.UpdateParameters["IPS_Antibiogram_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_IPS_Antibiogram.UpdateParameters["IPS_Antibiogram_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];

          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Antibiogram", "IPS_Antibiogram_Id = " + HiddenField_EditAntibiogramId.Value.ToString());
          DataView DataView_IPS_Antibiogram = (DataView)SqlDataSource_IPS_Antibiogram.Select(DataSourceSelectArguments.Empty);
          DataRowView DataRowView_IPS_Antibiogram = DataView_IPS_Antibiogram[GridView_IPS_Antibiogram_Current.RowIndex];
          Session["IPSAntibiogramHistory"] = Convert.ToString(DataRowView_IPS_Antibiogram["IPS_Antibiogram_History"], CultureInfo.CurrentCulture);
          Session["IPSAntibiogramHistory"] = Session["History"].ToString() + Session["IPSAntibiogramHistory"].ToString();
          SqlDataSource_IPS_Antibiogram.UpdateParameters["IPS_Antibiogram_History"].DefaultValue = Session["IPSAntibiogramHistory"].ToString();
          Session["IPSAntibiogramHistory"] = "";
          Session["History"] = "";

          SqlDataSource_IPS_Antibiogram.UpdateParameters["IPS_Antibiogram_IsActive"].DefaultValue = CheckBox_EditAntibiogramIsActive.Checked.ToString();
          SqlDataSource_IPS_Antibiogram.UpdateParameters["IPS_Antibiogram_Id"].DefaultValue = HiddenField_EditAntibiogramId.Value;

          SqlDataSource_IPS_Antibiogram.Update();


          string SQLStringDeleteLabReports = "EXECUTE spForm_Execute_IPS_HAI_LabReports_Delete @IPS_Infection_Id";
          using (SqlCommand SqlCommand_DeleteLabReports = new SqlCommand(SQLStringDeleteLabReports))
          {
            SqlCommand_DeleteLabReports.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteLabReports);
          }
        }
      }
    }

    protected string EditValidation_Antibiogram(Control gridView_IPS_Antibiogram_Current)
    {
      string InvalidAntibiogramMessage = "";

      if (gridView_IPS_Antibiogram_Current != null)
      {
        HiddenField HiddenField_EditAntibiogramId = (HiddenField)gridView_IPS_Antibiogram_Current.FindControl("HiddenField_EditAntibiogramId");
        DropDownList DropDownList_EditAntibiogramNameLookup = (DropDownList)gridView_IPS_Antibiogram_Current.FindControl("DropDownList_EditAntibiogramNameLookup");
        RadioButtonList RadioButtonList_EditAntibiogramSRI = (RadioButtonList)gridView_IPS_Antibiogram_Current.FindControl("RadioButtonList_EditAntibiogramSRI");
        CheckBox CheckBox_EditAntibiogramIsActive = (CheckBox)gridView_IPS_Antibiogram_Current.FindControl("CheckBox_EditAntibiogramIsActive");

        if (CheckBox_EditAntibiogramIsActive.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditAntibiogramNameLookup.SelectedValue))
          {
            InvalidAntibiogramMessage = InvalidAntibiogramMessage + Convert.ToString("Code or Description is Required<br/>", CultureInfo.CurrentCulture);
          }
          else
          {
            //String IPSAntibiogramLookupId = "";
            //String SQLStringAntibiogramLookup = "SELECT IPS_Antibiogram_Lookup_Id FROM Form_IPS_Antibiogram_Lookup WHERE IPS_Antibiogram_Lookup_Code = @IPS_Antibiogram_Lookup_Code AND IPS_Antibiogram_Lookup_IsActive = 1";
            //using (SqlCommand SqlCommand_AntibiogramLookup = new SqlCommand(SQLStringAntibiogramLookup))
            //{
            //  SqlCommand_AntibiogramLookup.Parameters.AddWithValue("@IPS_Antibiogram_Lookup_Code", TextBox_EditAntibiogramCode.Text.ToString());
            //  DataTable DataTable_AntibiogramLookup;
            //  using (DataTable_AntibiogramLookup = new DataTable())
            //  {
            //    DataTable_AntibiogramLookup.Locale = CultureInfo.CurrentCulture;

            //    DataTable_AntibiogramLookup = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AntibiogramLookup).Copy();
            //    if (DataTable_AntibiogramLookup.Rows.Count > 0)
            //    {
            //      foreach (DataRow DataRow_Row in DataTable_AntibiogramLookup.Rows)
            //      {
            //        IPSAntibiogramLookupId = DataRow_Row["IPS_Antibiogram_Lookup_Id"].ToString();
            //      }
            //    }
            //  }
            //}

            //if (String.IsNullOrEmpty(IPSAntibiogramLookupId))
            //{
            //  ToolkitScriptManager_IPS_Specimen.SetFocus(TextBox_EditAntibiogramCode);

            //  InvalidAntibiogramMessage = InvalidAntibiogramMessage + Convert.ToString("Antibiogram code does not exist<br/>", CultureInfo.CurrentCulture);
            //}
          }

          if (string.IsNullOrEmpty(RadioButtonList_EditAntibiogramSRI.SelectedValue))
          {
            InvalidAntibiogramMessage = InvalidAntibiogramMessage + Convert.ToString("SRI is Required<br/>", CultureInfo.CurrentCulture);
          }

          if (string.IsNullOrEmpty(InvalidAntibiogramMessage))
          {
            string IPSAntibiogramId = "";
            string IPSAntibiogramLookupDescription = "";
            string IPSAntibiogramSRIName = "";
            string SQLStringAntibiogram = "SELECT IPS_Antibiogram_Id , IPS_Antibiogram_Lookup_Description , IPS_Antibiogram_SRI_Name FROM vForm_IPS_Antibiogram WHERE IPS_Organism_Id = @IPS_Organism_Id AND IPS_Antibiogram_IsActive = 1 AND IPS_Antibiogram_Name_Lookup = @IPS_Antibiogram_Name_Lookup AND IPS_Antibiogram_SRI_List = @IPS_Antibiogram_SRI_List";
            using (SqlCommand SqlCommand_Antibiogram = new SqlCommand(SQLStringAntibiogram))
            {
              SqlCommand_Antibiogram.Parameters.AddWithValue("@IPS_Organism_Id", Request.QueryString["IPSOrganismId"]);
              SqlCommand_Antibiogram.Parameters.AddWithValue("@IPS_Antibiogram_Name_Lookup", DropDownList_EditAntibiogramNameLookup.SelectedValue);
              SqlCommand_Antibiogram.Parameters.AddWithValue("@IPS_Antibiogram_SRI_List", RadioButtonList_EditAntibiogramSRI.SelectedValue);
              DataTable DataTable_Antibiogram;
              using (DataTable_Antibiogram = new DataTable())
              {
                DataTable_Antibiogram.Locale = CultureInfo.CurrentCulture;

                DataTable_Antibiogram = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Antibiogram).Copy();
                if (DataTable_Antibiogram.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_Antibiogram.Rows)
                  {
                    IPSAntibiogramId = DataRow_Row["IPS_Antibiogram_Id"].ToString();
                    IPSAntibiogramLookupDescription = DataRow_Row["IPS_Antibiogram_Lookup_Description"].ToString();
                    IPSAntibiogramSRIName = DataRow_Row["IPS_Antibiogram_SRI_Name"].ToString();
                  }
                }
              }
            }

            if (!string.IsNullOrEmpty(IPSAntibiogramId))
            {
              if (IPSAntibiogramId != HiddenField_EditAntibiogramId.Value)
              {
                ToolkitScriptManager_IPS_Specimen.SetFocus(DropDownList_EditAntibiogramNameLookup);

                InvalidAntibiogramMessage = InvalidAntibiogramMessage + Convert.ToString("An Antibiogram with the name '" + IPSAntibiogramLookupDescription + "' and SRI with name '" + IPSAntibiogramSRIName + "' already exists<br/>", CultureInfo.CurrentCulture);
              }
            }
          }
        }
      }

      return InvalidAntibiogramMessage;
    }    
    //---END--- --Antibiogram--//


    //--START-- --AntibiogramList--//
    protected void SqlDataSource_IPS_Antibiogram_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_HiddenAntibiogramListTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_IPS_Antibiogram_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }


      for (int i = 0; i < GridView_IPS_Antibiogram_List.Rows.Count; i++)
      {
        if (GridView_IPS_Antibiogram_List.Rows[i].RowType == DataControlRowType.DataRow)
        {
          if (GridView_IPS_Antibiogram_List.Rows[i].Cells[3].Text == "No")
          {
            GridView_IPS_Antibiogram_List.Rows[i].Cells[3].BackColor = Color.FromName("#d46e6e");
            GridView_IPS_Antibiogram_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
          else if (GridView_IPS_Antibiogram_List.Rows[i].Cells[3].Text == "Yes")
          {
            GridView_IPS_Antibiogram_List.Rows[i].Cells[3].BackColor = Color.FromName("#77cf9c");
            GridView_IPS_Antibiogram_List.Rows[i].Cells[3].ForeColor = Color.FromName("#333333");
          }
        }
      }
    }

    protected void GridView_IPS_Antibiogram_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Pager || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
          Label Label_AntibiogramListTotalRecords = (Label)e.Row.FindControl("Label_AntibiogramListTotalRecords");
          Label_AntibiogramListTotalRecords.Text = Label_HiddenAntibiogramListTotalRecords.Text;
        }
      }
    }
    //---END--- --AntibiogramList--//


    //--START-- --CurrentInfectionComplete--//
    private void TableCurrentInfectionCompleteVisible()
    {
      TableCurrentInfectionCompleteVisible_Controls();


      FromDataBase_IsHAI FromDataBase_IsHAI_Current = GetIsHAI(Request.QueryString["IPSInfectionId"]);
      string IsHAI = FromDataBase_IsHAI_Current.IsHAI;


      if (IsHAI == "True")
      {
        TableCurrentInfectionCompleteVisible_ButtonsHAIYes();
      }
      else
      {
        TableCurrentInfectionCompleteVisible_ButtonsHAINo();
      }
    }

    private void TableCurrentInfectionCompleteVisible_Controls()
    {
      Session["IPSInfectionId"] = "";
      Session["FacilityFacilityDisplayName"] = "";
      Session["IPSVisitInformationVisitNumber"] = "";
      Session["PatientInformationName"] = "";
      Session["PatientInformationSurname"] = "";
      Session["IPSInfectionReportNumber"] = "";
      Session["IPSInfectionCategoryName"] = "";
      Session["IPSInfectionTypeName"] = "";
      Session["IPSInfectionCompleted"] = "";
      Session["IPSInfectionModifiedDate"] = "";
      Session["IPSInfectionModifiedBy"] = "";
      Session["IPSInfectionHistory"] = "";
      Session["IPSInfectionIsActive"] = "";
      Session["IPSHAIId"] = "";
      Session["IPSHAIModifiedDate"] = "";
      Session["Specimen"] = "";
      Session["Infection"] = "";
      Session["HAI"] = "";
      string SQLStringVisitInfo = "EXECUTE spForm_Get_IPS_InfectionInformation @IPS_Infection_Id";
      using (SqlCommand SqlCommand_VisitInfo = new SqlCommand(SQLStringVisitInfo))
      {
        SqlCommand_VisitInfo.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_VisitInfo;
        using (DataTable_VisitInfo = new DataTable())
        {
          DataTable_VisitInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_VisitInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitInfo).Copy();
          if (DataTable_VisitInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_VisitInfo.Rows)
            {
              Session["IPSInfectionId"] = DataRow_Row["IPS_Infection_Id"];
              Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["IPSVisitInformationVisitNumber"] = DataRow_Row["IPS_VisitInformation_VisitNumber"];
              Session["PatientInformationName"] = DataRow_Row["PatientInformation_Name"];
              Session["PatientInformationSurname"] = DataRow_Row["PatientInformation_Surname"];
              Session["IPSInfectionReportNumber"] = DataRow_Row["IPS_Infection_ReportNumber"];
              Session["IPSInfectionCategoryName"] = DataRow_Row["IPS_Infection_Category_Name"];
              Session["IPSInfectionTypeName"] = DataRow_Row["IPS_Infection_Type_Name"];
              Session["IPSInfectionCompleted"] = DataRow_Row["IPS_Infection_Completed"];
              Session["IPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["IPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSInfectionIsActive"] = DataRow_Row["IPS_Infection_IsActive"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
              Session["IPSHAIModifiedDate"] = DataRow_Row["IPS_HAI_ModifiedDate"];
              Session["Specimen"] = DataRow_Row["Specimen"];
              Session["Infection"] = DataRow_Row["Infection"];
              Session["HAI"] = DataRow_Row["HAI"];
            }
          }
        }
      }

      Label_CurrentInfectionCompleteInfection.Text = Session["Infection"].ToString();
      Label_CurrentInfectionCompleteHAIInvestigation.Text = Session["HAI"].ToString();

      if (Session["Infection"].ToString() == "Incomplete")
      {
        Hyperlink_CurrentInfectionCompleteHAIInvestigation.Text = Session["HAI"].ToString();
      }
      else
      {
        if (Session["HAI"].ToString() == "Not Required")
        {
          Hyperlink_CurrentInfectionCompleteHAIInvestigation.Text = Session["HAI"].ToString();
        }
        else
        {
          Hyperlink_CurrentInfectionCompleteHAIInvestigation.Text = Convert.ToString("<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Session["IPSInfectionId"].ToString() + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI") + "' Class='Controls_HyperLink_ColorBackground'>" + Session["HAI"].ToString() + "</a>", CultureInfo.CurrentCulture);
        }
      }

      HiddenField_CurrentInfectionCompleteHAIId.Value = Session["IPSHAIId"].ToString();
      Label_CurrentInfectionCompleteSpecimen.Text = Session["Specimen"].ToString();
      Hyperlink_CurrentInfectionCompleteSpecimen.Text = Convert.ToString("<a href='" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_Specimen", "Form_IPS_Specimen.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Session["IPSInfectionId"].ToString() + "") + "' Class='Controls_HyperLink_ColorBackground'>" + Session["Specimen"].ToString() + "</a>", CultureInfo.CurrentCulture);
      HiddenField_CurrentInfectionComplete_ModifiedDate.Value = Session["IPSInfectionModifiedDate"].ToString();
      HiddenField_CurrentInfectionComplete_ModifiedBy.Value = Session["IPSInfectionModifiedBy"].ToString();
      HiddenField_CurrentInfectionComplete_History.Value = Session["IPSInfectionHistory"].ToString();
      HiddenField_CurrentInfectionComplete_HAIModifiedDate.Value = Session["IPSHAIModifiedDate"].ToString();
      Label_CurrentInfectionCompleteIsActive.Text = Session["IPSInfectionIsActive"].ToString();

      if (Session["IPSInfectionIsActive"].ToString() == "Yes")
      {
        if (Session["Infection"].ToString() == "Incomplete")
        {
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "background-color: #d46e6e");
        }
        else
        {
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteInfection.Attributes.Add("Style", "background-color: #77cf9c");
        }

        if (Session["HAI"].ToString() == "Incomplete")
        {
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "background-color: #d46e6e");
        }
        else
        {
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "background-color: #77cf9c");
        }

        if (Session["Specimen"].ToString() == "Incomplete")
        {
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "background-color: #d46e6e");
        }
        else
        {
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "color: #333333");
          CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "background-color: #77cf9c");
        }

        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "background-color: #77cf9c");
      }
      else
      {
        CurrentInfectionCompleteInfection.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteInfection.Attributes.Add("Style", "background-color: #77cf9c");
        CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteHAIInvestigation.Attributes.Add("Style", "background-color: #77cf9c");
        CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteSpecimen.Attributes.Add("Style", "background-color: #77cf9c");
        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "color: #333333");
        CurrentInfectionCompleteIsActive.Attributes.Add("Style", "background-color: #d46e6e");
      }

      Session.Remove("IPSInfectionId");
      Session.Remove("FacilityFacilityDisplayName");
      Session.Remove("IPSVisitInformationVisitNumber");
      Session.Remove("PatientInformationName");
      Session.Remove("PatientInformationSurname");
      Session.Remove("IPSInfectionReportNumber");
      Session.Remove("IPSInfectionCategoryName");
      Session.Remove("IPSInfectionTypeName");
      Session.Remove("IPSInfectionCompleted");
      Session.Remove("IPSInfectionModifiedDate");
      Session.Remove("IPSInfectionModifiedBy");
      Session.Remove("IPSInfectionHistory");
      Session.Remove("IPSInfectionIsActive");
      Session.Remove("IPSHAIId");
      Session.Remove("IPSHAIModifiedDate");
      Session.Remove("Specimen");
      Session.Remove("Infection");
      Session.Remove("HAI");
    }

    private void TableCurrentInfectionCompleteVisible_ButtonsHAIYes()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_InfectionSite FromDataBase_InfectionSite_Current = GetInfectionSite();
      string IPSInfectionSiteInfectionIsActive = FromDataBase_InfectionSite_Current.IPSInfectionSiteInfectionIsActive;

      FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_Current = GetSpecimenCompleted();
      string Specimen = FromDataBase_SpecimenCompleted_Current.Specimen;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;

      FromDataBase_HAICompleted FromDataBase_HAICompleted_Current = GetHAICompleted();
      string IPSHAICompleted = FromDataBase_HAICompleted_Current.IPSHAICompleted;


      Button_HAINo_SpecimenIncomplete.Visible = false;
      Button_HAINo_InfectionCanceled.Visible = false;
      Button_HAINo_CompleteInfection.Visible = false;
      Button_HAINo_OpenInfection.Visible = false;
      Button_HAINo_CaptureNewInfection.Visible = false;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (IPSInfectionSiteInfectionIsActive == "False")
        {
          if (IPSInfectionIsActive == "True")
          {
            Button_HAIYes_LinkedSiteRequired.Visible = true;
            Button_HAIYes_SpecimenIncomplete.Visible = false;
            Button_HAIYes_InfectionCanceled.Visible = false;
            Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenInfection.Visible = false;
            Button_HAIYes_GoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenHAIInvestigation.Visible = false;
            Button_HAIYes_CaptureNewInfection.Visible = false;
          }
          else
          {
            Button_HAIYes_LinkedSiteRequired.Visible = false;
            Button_HAIYes_SpecimenIncomplete.Visible = false;
            Button_HAIYes_InfectionCanceled.Visible = true;
            Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenInfection.Visible = false;
            Button_HAIYes_GoToHAIInvestigation.Visible = false;
            Button_HAIYes_OpenHAIInvestigation.Visible = false;
            Button_HAIYes_CaptureNewInfection.Visible = true;
          }
        }
        else
        {
          if (Specimen == "Incomplete")
          {
            if (IPSInfectionIsActive == "True")
            {
              Button_HAIYes_LinkedSiteRequired.Visible = false;
              Button_HAIYes_SpecimenIncomplete.Visible = true;
              Button_HAIYes_InfectionCanceled.Visible = false;
              Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenInfection.Visible = false;
              Button_HAIYes_GoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenHAIInvestigation.Visible = false;
              Button_HAIYes_CaptureNewInfection.Visible = false;
            }
            else
            {
              Button_HAIYes_LinkedSiteRequired.Visible = false;
              Button_HAIYes_SpecimenIncomplete.Visible = false;
              Button_HAIYes_InfectionCanceled.Visible = true;
              Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenInfection.Visible = false;
              Button_HAIYes_GoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenHAIInvestigation.Visible = false;
              Button_HAIYes_CaptureNewInfection.Visible = true;
            }
          }
          else
          {
            if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
            {
              Button_HAIYes_LinkedSiteRequired.Visible = false;
              Button_HAIYes_SpecimenIncomplete.Visible = false;
              Button_HAIYes_InfectionCanceled.Visible = false;
              Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = true;
              Button_HAIYes_OpenInfection.Visible = false;
              Button_HAIYes_GoToHAIInvestigation.Visible = false;
              Button_HAIYes_OpenHAIInvestigation.Visible = false;
              Button_HAIYes_CaptureNewInfection.Visible = false;
            }
            else
            {
              if (IPSInfectionIsActive == "True")
              {
                if (IPSHAICompleted == "False")
                {
                  Button_HAIYes_LinkedSiteRequired.Visible = false;
                  Button_HAIYes_SpecimenIncomplete.Visible = false;
                  Button_HAIYes_InfectionCanceled.Visible = false;
                  Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
                  Button_HAIYes_OpenInfection.Visible = true;
                  Button_HAIYes_GoToHAIInvestigation.Visible = true;
                  Button_HAIYes_OpenHAIInvestigation.Visible = false;
                  Button_HAIYes_CaptureNewInfection.Visible = false;
                }
                else
                {
                  Button_HAIYes_LinkedSiteRequired.Visible = false;
                  Button_HAIYes_SpecimenIncomplete.Visible = false;
                  Button_HAIYes_InfectionCanceled.Visible = false;
                  Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
                  Button_HAIYes_OpenInfection.Visible = true;
                  Button_HAIYes_GoToHAIInvestigation.Visible = false;
                  Button_HAIYes_OpenHAIInvestigation.Visible = true;
                  Button_HAIYes_CaptureNewInfection.Visible = true;
                }
              }
              else
              {
                Button_HAIYes_LinkedSiteRequired.Visible = false;
                Button_HAIYes_SpecimenIncomplete.Visible = false;
                Button_HAIYes_InfectionCanceled.Visible = true;
                Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
                Button_HAIYes_OpenInfection.Visible = false;
                Button_HAIYes_GoToHAIInvestigation.Visible = false;
                Button_HAIYes_OpenHAIInvestigation.Visible = false;
                Button_HAIYes_CaptureNewInfection.Visible = true;
              }
            }
          }
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";

        Button_HAIYes_LinkedSiteRequired.Visible = false;
        Button_HAIYes_SpecimenIncomplete.Visible = false;
        Button_HAIYes_InfectionCanceled.Visible = false;
        Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenInfection.Visible = false;
        Button_HAIYes_GoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenHAIInvestigation.Visible = false;
        Button_HAIYes_CaptureNewInfection.Visible = false;
      }

      if (Security == "1")
      {
        Security = "0";

        Button_HAIYes_LinkedSiteRequired.Visible = false;
        Button_HAIYes_SpecimenIncomplete.Visible = false;
        Button_HAIYes_InfectionCanceled.Visible = false;
        Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenInfection.Visible = false;
        Button_HAIYes_GoToHAIInvestigation.Visible = false;
        Button_HAIYes_OpenHAIInvestigation.Visible = false;
        Button_HAIYes_CaptureNewInfection.Visible = false;
      }
    }

    private void TableCurrentInfectionCompleteVisible_ButtonsHAINo()
    {
      FromDataBase_SecurityRole FromDataBase_SecurityRole_Current = GetSecurityRole();
      DataRow[] SecurityAdmin = FromDataBase_SecurityRole_Current.SecurityAdmin;
      DataRow[] SecurityFormAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFormAdminUpdate;
      DataRow[] SecurityFormAdminView = FromDataBase_SecurityRole_Current.SecurityFormAdminView;
      DataRow[] SecurityFacilityAdminUpdate = FromDataBase_SecurityRole_Current.SecurityFacilityAdminUpdate;
      DataRow[] SecurityFacilityAdminView = FromDataBase_SecurityRole_Current.SecurityFacilityAdminView;

      FromDataBase_SpecimenCompleted FromDataBase_SpecimenCompleted_Current = GetSpecimenCompleted();
      string Specimen = FromDataBase_SpecimenCompleted_Current.Specimen;

      FromDataBase_InfectionCompleted FromDataBase_InfectionCompleted_Current = GetInfectionCompleted();
      string IPSInfectionCompleted = FromDataBase_InfectionCompleted_Current.IPSInfectionCompleted;
      string IPSInfectionIsActive = FromDataBase_InfectionCompleted_Current.IPSInfectionIsActive;


      Button_HAIYes_LinkedSiteRequired.Visible = false;
      Button_HAIYes_SpecimenIncomplete.Visible = false;
      Button_HAIYes_InfectionCanceled.Visible = false;
      Button_HAIYes_CompleteInfectionGoToHAIInvestigation.Visible = false;
      Button_HAIYes_OpenInfection.Visible = false;
      Button_HAIYes_GoToHAIInvestigation.Visible = false;
      Button_HAIYes_OpenHAIInvestigation.Visible = false;
      Button_HAIYes_CaptureNewInfection.Visible = false;

      string Security = "1";
      if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
      {
        Security = "0";

        if (Specimen == "Incomplete")
        {
          if (IPSInfectionIsActive == "True")
          {
            Button_HAINo_SpecimenIncomplete.Visible = true;
            Button_HAINo_InfectionCanceled.Visible = false;
            Button_HAINo_CompleteInfection.Visible = false;
            Button_HAINo_OpenInfection.Visible = false;
            Button_HAINo_CaptureNewInfection.Visible = false;
          }
          else
          {
            Button_HAINo_SpecimenIncomplete.Visible = false;
            Button_HAINo_InfectionCanceled.Visible = true;
            Button_HAINo_CompleteInfection.Visible = false;
            Button_HAINo_OpenInfection.Visible = false;
            Button_HAINo_CaptureNewInfection.Visible = true;
          }
        }
        else
        {
          if (IPSInfectionCompleted == "False" && IPSInfectionIsActive == "True")
          {
            Button_HAINo_SpecimenIncomplete.Visible = false;
            Button_HAINo_InfectionCanceled.Visible = false;
            Button_HAINo_CompleteInfection.Visible = true;
            Button_HAINo_OpenInfection.Visible = false;
            Button_HAINo_CaptureNewInfection.Visible = false;
          }
          else
          {
            if (IPSInfectionIsActive == "True")
            {
              Button_HAINo_SpecimenIncomplete.Visible = false;
              Button_HAINo_InfectionCanceled.Visible = false;
              Button_HAINo_CompleteInfection.Visible = false;
              Button_HAINo_OpenInfection.Visible = true;
              Button_HAINo_CaptureNewInfection.Visible = true;
            }
            else
            {
              Button_HAINo_SpecimenIncomplete.Visible = false;
              Button_HAINo_InfectionCanceled.Visible = true;
              Button_HAINo_CompleteInfection.Visible = false;
              Button_HAINo_OpenInfection.Visible = false;
              Button_HAINo_CaptureNewInfection.Visible = true;
            }
          }
        }
      }

      if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
      {
        Security = "0";

        Button_HAINo_SpecimenIncomplete.Visible = false;
        Button_HAINo_InfectionCanceled.Visible = false;
        Button_HAINo_CompleteInfection.Visible = false;
        Button_HAINo_OpenInfection.Visible = false;
        Button_HAINo_CaptureNewInfection.Visible = false;
      }

      if (Security == "1")
      {
        Security = "0";

        Button_HAINo_SpecimenIncomplete.Visible = false;
        Button_HAINo_InfectionCanceled.Visible = false;
        Button_HAINo_CompleteInfection.Visible = false;
        Button_HAINo_OpenInfection.Visible = false;
        Button_HAINo_CaptureNewInfection.Visible = false;
      }
    }


    protected void Button_InfectionInfectionHome_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
    }


    protected void Button_HAIYes_LinkedSiteRequired_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAIYes_SpecimenIncomplete_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAIYes_InfectionCanceled_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAIYes_CompleteInfectionGoToHAIInvestigation_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History , IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 1);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI"), false);
        }
      }
    }

    protected void Button_HAIYes_OpenInfection_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History , IPS_HAI_Id FROM Form_IPS_Infection LEFT JOIN Form_IPS_HAI ON Form_IPS_Infection.IPS_Infection_Id = Form_IPS_HAI.IPS_Infection_Id WHERE Form_IPS_Infection.IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id ; UPDATE Form_IPS_HAI SET IPS_HAI_Investigation_Completed = 0 , IPS_HAI_Investigation_CompletedDate = NULL WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 0);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DBNull.Value);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentInfection"), false);
        }
      }
    }

    protected void Button_HAIYes_GoToHAIInvestigation_OnClick(object sender, EventArgs e)
    {
      string SQLStringHAIId = "SELECT IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_HAIId = new SqlCommand(SQLStringHAIId))
      {
        SqlCommand_HAIId.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_HAIId;
        using (DataTable_HAIId = new DataTable())
        {
          DataTable_HAIId.Locale = CultureInfo.CurrentCulture;
          DataTable_HAIId = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAIId).Copy();
          if (DataTable_HAIId.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HAIId.Rows)
            {
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI"), false);
    }

    protected void Button_HAIYes_OpenHAIInvestigation_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSHAIModifiedDate"] = HiddenField_CurrentInfectionComplete_HAIModifiedDate.Value.ToString();
      object OLDIPSHAIModifiedDate = Session["OLDIPSHAIModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSHAIModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringHAI = "SELECT IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy , IPS_HAI_History , IPS_HAI_Id FROM Form_IPS_HAI WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_HAI = new SqlCommand(SQLStringHAI))
      {
        SqlCommand_HAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_HAI;
        using (DataTable_HAI = new DataTable())
        {
          DataTable_HAI.Locale = CultureInfo.CurrentCulture;
          DataTable_HAI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_HAI).Copy();
          if (DataTable_HAI.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_HAI.Rows)
            {
              Session["DBIPSHAIModifiedDate"] = DataRow_Row["IPS_HAI_ModifiedDate"];
              Session["DBIPSHAIModifiedBy"] = DataRow_Row["IPS_HAI_ModifiedBy"];
              Session["IPSHAIHistory"] = DataRow_Row["IPS_HAI_History"];
              Session["IPSHAIId"] = DataRow_Row["IPS_HAI_Id"];
            }
          }
        }
      }

      object DBIPSHAIModifiedDate = Session["DBIPSHAIModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSHAIModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSHAIModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_HAI", "IPS_HAI_Id = " + Session["IPSHAIId"].ToString());

          Session["IPSHAIHistory"] = Session["History"].ToString() + Session["IPSHAIHistory"].ToString();

          string SQLStringUpdateHAI = "UPDATE Form_IPS_HAI SET IPS_HAI_Investigation_Completed = @IPS_HAI_Investigation_Completed , IPS_HAI_Investigation_CompletedDate = @IPS_HAI_Investigation_CompletedDate , IPS_HAI_ModifiedDate = @IPS_HAI_ModifiedDate , IPS_HAI_ModifiedBy = @IPS_HAI_ModifiedBy , IPS_HAI_History = @IPS_HAI_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateHAI = new SqlCommand(SQLStringUpdateHAI))
          {
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_Completed", 0);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_Investigation_CompletedDate", DBNull.Value);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateHAI.Parameters.AddWithValue("@IPS_HAI_History", Session["IPSHAIHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateHAI);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS_HAI", "Form_IPS_HAI.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "&IPSHAIId=" + Session["IPSHAIId"].ToString() + "#CurrentHAI"), false);
        }
      }
    }

    protected void Button_HAIYes_CaptureNewInfection_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx"), false);
    }


    protected void Button_HAINo_SpecimenIncomplete_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAINo_InfectionCanceled_OnClick(object sender, EventArgs e)
    {
    }

    protected void Button_HAINo_CompleteInfection_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 1);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"]), false);
        }
      }
    }

    protected void Button_HAINo_OpenInfection_OnClick(object sender, EventArgs e)
    {
      Session["OLDIPSInfectionModifiedDate"] = HiddenField_CurrentInfectionComplete_ModifiedDate.Value.ToString();
      object OLDIPSInfectionModifiedDate = Session["OLDIPSInfectionModifiedDate"].ToString();
      DateTime OLDModifiedDate1 = DateTime.Parse(OLDIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      string SQLStringInfection = "SELECT IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy , IPS_Infection_History FROM Form_IPS_Infection WHERE IPS_Infection_Id = @IPS_Infection_Id";
      using (SqlCommand SqlCommand_Infection = new SqlCommand(SQLStringInfection))
      {
        SqlCommand_Infection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
        DataTable DataTable_Infection;
        using (DataTable_Infection = new DataTable())
        {
          DataTable_Infection.Locale = CultureInfo.CurrentCulture;
          DataTable_Infection = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Infection).Copy();
          if (DataTable_Infection.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Infection.Rows)
            {
              Session["DBIPSInfectionModifiedDate"] = DataRow_Row["IPS_Infection_ModifiedDate"];
              Session["DBIPSInfectionModifiedBy"] = DataRow_Row["IPS_Infection_ModifiedBy"];
              Session["IPSInfectionHistory"] = DataRow_Row["IPS_Infection_History"];
            }
          }
        }
      }

      object DBIPSInfectionModifiedDate = Session["DBIPSInfectionModifiedDate"].ToString();
      DateTime DBModifiedDate1 = DateTime.Parse(DBIPSInfectionModifiedDate.ToString(), CultureInfo.CurrentCulture);
      string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

      if (OLDModifiedDateNew != DBModifiedDateNew)
      {
        ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

        string Label_ConcurrencyMessageText = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIPSInfectionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

        Label_InvalidFormMessage.Text = "";
        Label_ConcurrencyUpdateMessage.Text = Label_ConcurrencyMessageText;
      }
      else if (OLDModifiedDateNew == DBModifiedDateNew)
      {
        string Label_InvalidFormMessageText = "";

        if (!string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          ToolkitScriptManager_IPS_Specimen.SetFocus(LinkButton_CurrentInfectionComplete);

          Label_InvalidFormMessage.Text = Label_InvalidFormMessageText;
          Label_ConcurrencyUpdateMessage.Text = "";
        }
        else if (string.IsNullOrEmpty(Label_InvalidFormMessageText))
        {
          Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Form_IPS_Infection", "IPS_Infection_Id = " + Request.QueryString["IPSInfectionId"]);

          Session["IPSInfectionHistory"] = Session["History"].ToString() + Session["IPSInfectionHistory"].ToString();

          string SQLStringUpdateInfection = "UPDATE Form_IPS_Infection SET IPS_Infection_Completed = @IPS_Infection_Completed , IPS_Infection_CompletedDate = @IPS_Infection_CompletedDate , IPS_Infection_ModifiedDate = @IPS_Infection_ModifiedDate , IPS_Infection_ModifiedBy = @IPS_Infection_ModifiedBy , IPS_Infection_History = @IPS_Infection_History WHERE IPS_Infection_Id = @IPS_Infection_Id";
          using (SqlCommand SqlCommand_UpdateInfection = new SqlCommand(SQLStringUpdateInfection))
          {
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Id", Request.QueryString["IPSInfectionId"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_Completed", 0);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_CompletedDate", DBNull.Value);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedDate", DateTime.Now.ToString());
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_UpdateInfection.Parameters.AddWithValue("@IPS_Infection_History", Session["IPSInfectionHistory"].ToString());
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateInfection);
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx?IPSVisitInformationId=" + Request.QueryString["IPSVisitInformationId"] + "&IPSInfectionId=" + Request.QueryString["IPSInfectionId"] + "#CurrentInfection"), false);
        }
      }
    }

    protected void Button_HAINo_CaptureNewInfection_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form_IPS", "Form_IPS.aspx"), false);
    }
    //---END--- --CurrentInfectionComplete--//
  }
}