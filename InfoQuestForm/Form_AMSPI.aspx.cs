using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_AMSPI : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;
    private bool Button_EditPrintClicked = false;
    private bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          DropDownList_Facility.Attributes.Add("OnChange", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnKeyUp", "Validation_Search();");
          TextBox_PatientVisitNumber.Attributes.Add("OnInput", "Validation_Search();");

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("29")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("29").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_PatientInfoHeading.Text = Convert.ToString("Patient Information", CultureInfo.CurrentCulture);
          Label_InterventionsHeading.Text = Convert.ToString("Interventions", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("29").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_AMSPI_PatientVisitNumber"] != null)
          {
            if (Request.QueryString["AMSPI_Intervention_Id"] != null)
            {
              SqlDataSource_AMSPI_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
              SqlDataSource_AMSPI_Facility.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_AMSPI_Intervention";
              SqlDataSource_AMSPI_Facility.SelectParameters["TableWHERE"].DefaultValue = "AMSPI_Intervention_Id = " + Request.QueryString["AMSPI_Intervention_Id"] + "";
            }

            TablePatientInfo.Visible = true;
            TableForm.Visible = true;
            TableList.Visible = true;

            PatientDataPI();

            if (TablePatientInfo.Visible == true)
            {
              SetFormVisibility();
            }
          }
          else
          {
            Label_InvalidSearchMessage.Text = "";
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            TableList.Visible = false;
          }


          if (TablePatientInfo.Visible == true)
          {
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
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('29'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('29')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
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
      if (PageSecurity() == "1")
      {

      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_AMSPI_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_AMSPI_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AMSPI_Facility.SelectParameters.Clear();
      SqlDataSource_AMSPI_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_AMSPI_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "29");
      SqlDataSource_AMSPI_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_AMSPI_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_AMSPI_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_AMSPI_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_AMSPI_InsertCommunicationList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertCommunicationList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id , ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 72 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Communication_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Communication_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertCommunicationList.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertCommunicationList.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertCommunicationList.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertInterventionList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertInterventionList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 73 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Intervention_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Intervention_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertInterventionList.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertInterventionList.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertInterventionList.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertInterventionInList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertInterventionInList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 74 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_InterventionIn_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_InterventionIn_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertInterventionInList.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertInterventionInList.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertInterventionInList.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_AMSPI_InsertUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AMSPI_InsertUnit.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_AMSPI_InsertUnit.SelectParameters.Add("Form_Id", TypeCode.String, "29");
      SqlDataSource_AMSPI_InsertUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_AMSPI_InsertUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_AMSPI_InsertUnit.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_AMSPI_InsertUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_AMSPI_InsertType1List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertType1List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 81 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type1_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type1_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertType1List.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertType1List.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertType1List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertType2List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertType2List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 82 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type2_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type2_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertType2List.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertType2List.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertType2List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertType3List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertType3List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 83 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type3_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type3_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertType3List.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertType3List.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertType3List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertType4List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertType4List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 84 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type4_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type4_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertType4List.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertType4List.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertType4List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertType7List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertType7List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 85 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type7_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type7_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertType7List.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertType7List.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertType7List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertType9List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertType9List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 86 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type9_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type9_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertType9List.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertType9List.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertType9List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertType10List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertType10List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 87 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type10_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type10_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertType10List.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertType10List.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertType10List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_InsertType11List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_InsertType11List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 80 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type11_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type11_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_InsertType11List.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_InsertType11List.SelectParameters.Clear();
      SqlDataSource_AMSPI_InsertType11List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditCommunicationList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditCommunicationList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 72 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Communication_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Communication_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditCommunicationList.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditCommunicationList.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditInterventionList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditInterventionList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 73 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Intervention_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Intervention_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditInterventionList.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditInterventionList.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditInterventionInList.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditInterventionInList.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 74 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_InterventionIn_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_InterventionIn_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditInterventionInList.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditInterventionInList.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditUnit.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditUnit.SelectCommand = "spAdministration_Execute_Facility_Unit";
      SqlDataSource_AMSPI_EditUnit.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AMSPI_EditUnit.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditUnit.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_AMSPI_EditUnit.SelectParameters.Add("Form_Id", TypeCode.String, "29");
      SqlDataSource_AMSPI_EditUnit.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_AMSPI_EditUnit.SelectParameters.Add("TableSELECT", TypeCode.String, "Unit_Id");
      SqlDataSource_AMSPI_EditUnit.SelectParameters.Add("TableFROM", TypeCode.String, "InfoQuest_Form_AMSPI_Intervention");
      SqlDataSource_AMSPI_EditUnit.SelectParameters.Add("TableWHERE", TypeCode.String, "AMSPI_Intervention_Id = " + Request.QueryString["AMSPI_Intervention_Id"] + "");

      SqlDataSource_AMSPI_EditType1List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditType1List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 81 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type1_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type1_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditType1List.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditType1List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditType2List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditType2List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 82 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type2_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type2_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditType2List.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditType2List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditType3List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditType3List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 83 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type3_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type3_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditType3List.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditType3List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditType4List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditType4List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 84 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type4_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type4_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditType4List.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditType4List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditType7List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditType7List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 85 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type7_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type7_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditType7List.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditType7List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditType9List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditType9List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 86 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type9_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type9_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditType9List.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditType9List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);

      SqlDataSource_AMSPI_EditType10List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditType10List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 87 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type10_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type10_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditType10List.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditType10List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);
      
      SqlDataSource_AMSPI_EditType11List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_EditType11List.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 29 AND ListCategory_Id = 80 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type11_List,vAdministration_ListItem_Active.ListItem_Name FROM InfoQuest_Form_AMSPI_Intervention , vAdministration_ListItem_Active WHERE InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Type11_List = vAdministration_ListItem_Active.ListItem_Id AND InfoQuest_Form_AMSPI_Intervention.AMSPI_Intervention_Id = @AMSPI_Intervention_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_AMSPI_EditType11List.SelectParameters.Clear();
      SqlDataSource_AMSPI_EditType11List.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.String, Request.QueryString["AMSPI_Intervention_Id"]);
      
      SqlDataSource_AMSPI_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_Form.InsertCommand="INSERT INTO [InfoQuest_Form_AMSPI_Intervention] ([Facility_Id] ,[AMSPI_Intervention_PatientVisitNumber] ,[AMSPI_Intervention_ReportNumber] ,[Unit_Id] ,[AMSPI_Intervention_Date] ,[AMSPI_Intervention_Communication_List] ,[AMSPI_Intervention_Time] ,[AMSPI_Intervention_Intervention_List] ,[AMSPI_Intervention_InterventionIn_List] ,[AMSPI_Intervention_Type1] ,[AMSPI_Intervention_Type1_List] ,[AMSPI_Intervention_Type2] ,[AMSPI_Intervention_Type2_List] ,[AMSPI_Intervention_Type3] ,[AMSPI_Intervention_Type3_List] ,[AMSPI_Intervention_Type4] ,[AMSPI_Intervention_Type4_List] ,[AMSPI_Intervention_Type5] ,[AMSPI_Intervention_Type6] ,[AMSPI_Intervention_Type7] ,[AMSPI_Intervention_Type7_List] ,[AMSPI_Intervention_Type8] ,[AMSPI_Intervention_Type9] ,[AMSPI_Intervention_Type9_List] ,[AMSPI_Intervention_Type10] ,[AMSPI_Intervention_Type10_List] ,[AMSPI_Intervention_Type11] ,[AMSPI_Intervention_Type11_List] ,[AMSPI_Intervention_TypeTotal] ,[AMSPI_Intervention_CreatedDate] ,[AMSPI_Intervention_CreatedBy] ,[AMSPI_Intervention_ModifiedDate] ,[AMSPI_Intervention_ModifiedBy] ,[AMSPI_Intervention_History] ,[AMSPI_Intervention_IsActive]) VALUES (@Facility_Id ,@AMSPI_Intervention_PatientVisitNumber ,@AMSPI_Intervention_ReportNumber ,@Unit_Id ,@AMSPI_Intervention_Date ,@AMSPI_Intervention_Communication_List ,@AMSPI_Intervention_Time ,@AMSPI_Intervention_Intervention_List ,@AMSPI_Intervention_InterventionIn_List ,@AMSPI_Intervention_Type1 ,@AMSPI_Intervention_Type1_List ,@AMSPI_Intervention_Type2 ,@AMSPI_Intervention_Type2_List ,@AMSPI_Intervention_Type3 ,@AMSPI_Intervention_Type3_List ,@AMSPI_Intervention_Type4 ,@AMSPI_Intervention_Type4_List ,@AMSPI_Intervention_Type5 ,@AMSPI_Intervention_Type6 ,@AMSPI_Intervention_Type7 ,@AMSPI_Intervention_Type7_List ,@AMSPI_Intervention_Type8 ,@AMSPI_Intervention_Type9 ,@AMSPI_Intervention_Type9_List ,@AMSPI_Intervention_Type10 ,@AMSPI_Intervention_Type10_List ,@AMSPI_Intervention_Type11 ,@AMSPI_Intervention_Type11_List ,@AMSPI_Intervention_TypeTotal ,@AMSPI_Intervention_CreatedDate ,@AMSPI_Intervention_CreatedBy ,@AMSPI_Intervention_ModifiedDate ,@AMSPI_Intervention_ModifiedBy ,@AMSPI_Intervention_History ,@AMSPI_Intervention_IsActive); SELECT @AMSPI_Intervention_Id = SCOPE_IDENTITY()";
      SqlDataSource_AMSPI_Form.SelectCommand="SELECT * FROM [InfoQuest_Form_AMSPI_Intervention] WHERE ([AMSPI_Intervention_Id] = @AMSPI_Intervention_Id)";
      SqlDataSource_AMSPI_Form.UpdateCommand="UPDATE [InfoQuest_Form_AMSPI_Intervention] SET [Unit_Id] = @Unit_Id ,[AMSPI_Intervention_Date] = @AMSPI_Intervention_Date ,[AMSPI_Intervention_Communication_List] = @AMSPI_Intervention_Communication_List ,[AMSPI_Intervention_Time] = @AMSPI_Intervention_Time ,[AMSPI_Intervention_Intervention_List] = @AMSPI_Intervention_Intervention_List ,[AMSPI_Intervention_InterventionIn_List] = @AMSPI_Intervention_InterventionIn_List ,[AMSPI_Intervention_Type1] = @AMSPI_Intervention_Type1 ,[AMSPI_Intervention_Type1_List] = @AMSPI_Intervention_Type1_List ,[AMSPI_Intervention_Type2] = @AMSPI_Intervention_Type2 ,[AMSPI_Intervention_Type2_List] = @AMSPI_Intervention_Type2_List ,[AMSPI_Intervention_Type3] = @AMSPI_Intervention_Type3 ,[AMSPI_Intervention_Type3_List] = @AMSPI_Intervention_Type3_List ,[AMSPI_Intervention_Type4] = @AMSPI_Intervention_Type4 ,[AMSPI_Intervention_Type4_List] = @AMSPI_Intervention_Type4_List ,[AMSPI_Intervention_Type5] = @AMSPI_Intervention_Type5 ,[AMSPI_Intervention_Type6] = @AMSPI_Intervention_Type6 ,[AMSPI_Intervention_Type7] = @AMSPI_Intervention_Type7 ,[AMSPI_Intervention_Type7_List] = @AMSPI_Intervention_Type7_List ,[AMSPI_Intervention_Type8] = @AMSPI_Intervention_Type8 ,[AMSPI_Intervention_Type9] = @AMSPI_Intervention_Type9 ,[AMSPI_Intervention_Type9_List] = @AMSPI_Intervention_Type9_List ,[AMSPI_Intervention_Type10] = @AMSPI_Intervention_Type10 ,[AMSPI_Intervention_Type10_List] = @AMSPI_Intervention_Type10_List ,[AMSPI_Intervention_Type11] = @AMSPI_Intervention_Type11 ,[AMSPI_Intervention_Type11_List] = @AMSPI_Intervention_Type11_List ,[AMSPI_Intervention_TypeTotal] = @AMSPI_Intervention_TypeTotal ,[AMSPI_Intervention_ModifiedDate] = @AMSPI_Intervention_ModifiedDate ,[AMSPI_Intervention_ModifiedBy] = @AMSPI_Intervention_ModifiedBy ,[AMSPI_Intervention_History] = @AMSPI_Intervention_History ,[AMSPI_Intervention_IsActive] = @AMSPI_Intervention_IsActive WHERE [AMSPI_Intervention_Id] = @AMSPI_Intervention_Id";
      SqlDataSource_AMSPI_Form.InsertParameters.Clear();
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Id", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_AMSPI_Form.InsertParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_PatientVisitNumber", TypeCode.Int32, Request.QueryString["s_AMSPI_PatientVisitNumber"]);
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_ReportNumber", TypeCode.String, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Date", TypeCode.DateTime, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Communication_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Time", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Intervention_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_InterventionIn_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type1", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type1_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type2", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type2_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type3", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type3_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type4", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type4_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type5", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type6", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type7", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type7_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type8", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type9", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type9_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type10", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type10_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type11", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_Type11_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_TypeTotal", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_CreatedBy", TypeCode.String, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_ModifiedBy", TypeCode.String, "");
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_History", TypeCode.String, "");
      SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_AMSPI_Form.InsertParameters.Add("AMSPI_Intervention_IsActive", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.SelectParameters.Clear();
      SqlDataSource_AMSPI_Form.SelectParameters.Add("AMSPI_Intervention_Id", TypeCode.Int32, Request.QueryString["AMSPI_Intervention_Id"]);
      SqlDataSource_AMSPI_Form.UpdateParameters.Clear();
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("Unit_Id", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Date", TypeCode.DateTime, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Communication_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Time", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Intervention_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_InterventionIn_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type1", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type1_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type2", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type2_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type3", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type3_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type4", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type4_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type5", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type6", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type7", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type7_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type8", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type9", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type9_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type10", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type10_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type11", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Type11_List", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_TypeTotal", TypeCode.Int32, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_ModifiedBy", TypeCode.String, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_History", TypeCode.String, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_IsActive", TypeCode.Boolean, "");
      SqlDataSource_AMSPI_Form.UpdateParameters.Add("AMSPI_Intervention_Id", TypeCode.Int32, "");      

      SqlDataSource_AMSPI_Intervention.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_AMSPI_Intervention.SelectCommand = "spForm_Get_AMSPI_Intervention";
      SqlDataSource_AMSPI_Intervention.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_AMSPI_Intervention.CancelSelectOnNullParameter = false;
      SqlDataSource_AMSPI_Intervention.SelectParameters.Clear();
      SqlDataSource_AMSPI_Intervention.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_AMSPI_Intervention.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_AMSPI_Intervention.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_AMSPI_PatientVisitNumber"]);
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
        if (Request.QueryString["s_AMSPI_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_AMSPI_PatientVisitNumber"];
        }
      }
    }

    private void PatientDataPI()
    {
      DataTable DataTable_PatientDataPI;
      using (DataTable_PatientDataPI = new DataTable())
      {
        DataTable_PatientDataPI.Locale = CultureInfo.CurrentCulture;
        DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_AMSPI_PatientVisitNumber"]).Copy();

        if (DataTable_PatientDataPI.Columns.Count == 1)
        {
          Session["AMSPIPIId"] = "";
          string SQLStringPatientInfo = "SELECT AMSPI_PI_Id FROM InfoQuest_Form_AMSPI_PatientInformation WHERE Facility_Id = @FacilityId AND AMSPI_PI_PatientVisitNumber = @AMSPIPIPatientVisitNumber";
          using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
          {
            SqlCommand_PatientInfo.Parameters.AddWithValue("@FacilityId", Request.QueryString["s_Facility_Id"]);
            SqlCommand_PatientInfo.Parameters.AddWithValue("@AMSPIPIPatientVisitNumber", Request.QueryString["s_AMSPI_PatientVisitNumber"]);
            DataTable DataTable_PatientInfo;
            using (DataTable_PatientInfo = new DataTable())
            {
              DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
              DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
              if (DataTable_PatientInfo.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                {
                  Session["AMSPIPIId"] = DataRow_Row1["AMSPI_PI_Id"];
                }
              }
            }
          }

          if (string.IsNullOrEmpty(Session["AMSPIPIId"].ToString()))
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
            Session["Error"] = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["Error"] = DataRow_Row["Error"];
            }

            Label_InvalidSearchMessage.Text = Session["Error"].ToString() + Convert.ToString("<br />No Patient related data could be updated but you can continue capturing new form(s) and updating and viewing previous form(s)", CultureInfo.CurrentCulture);
            Session["Error"] = "";
          }

          Session["AMSPIPIId"] = "";
        }
        else if (DataTable_PatientDataPI.Columns.Count != 1)
        {
          if (DataTable_PatientDataPI.Rows.Count == 0)
          {
            Label_InvalidSearchMessage.Text = Convert.ToString("Patient Visit Number " + Request.QueryString["s_AMSPI_PatientVisitNumber"] + " does not Exist", CultureInfo.CurrentCulture);
            TablePatientInfo.Visible = false;
            TableForm.Visible = false;
            TableList.Visible = false;
          }
          else
          {
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["VisitNumber"] = DataRow_Row["VisitNumber"];
              Session["NameSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];
              Session["Age"] = DataRow_Row["PatientAge"];
              Session["AdmissionDate"] = DataRow_Row["DateOfAdmission"];
              Session["DischargeDate"] = DataRow_Row["DateOfDischarge"];

              string NameSurnamePI = Session["NameSurname"].ToString();
              NameSurnamePI = NameSurnamePI.Replace("'", "");
              Session["NameSurname"] = NameSurnamePI;
              NameSurnamePI = "";

              Session["AMSPIPIId"] = "";
              string SQLStringPatientInfo = "SELECT AMSPI_PI_Id FROM InfoQuest_Form_AMSPI_PatientInformation WHERE Facility_Id = @FacilityId AND AMSPI_PI_PatientVisitNumber = @AMSPIPIPatientVisitNumber";
              using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
              {
                SqlCommand_PatientInfo.Parameters.AddWithValue("@FacilityId", Request.QueryString["s_Facility_Id"]);
                SqlCommand_PatientInfo.Parameters.AddWithValue("@AMSPIPIPatientVisitNumber", Request.QueryString["s_AMSPI_PatientVisitNumber"]);
                DataTable DataTable_PatientInfo;
                using (DataTable_PatientInfo = new DataTable())
                {
                  DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
                  DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
                  if (DataTable_PatientInfo.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                    {
                      Session["AMSPIPIId"] = DataRow_Row1["AMSPI_PI_Id"];
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(Session["AMSPIPIId"].ToString()))
              {
                string SQLStringInsertAMSPIPI = "INSERT INTO InfoQuest_Form_AMSPI_PatientInformation ( Facility_Id , AMSPI_PI_PatientVisitNumber , AMSPI_PI_PatientName , AMSPI_PI_PatientAge , AMSPI_PI_PatientDateOfAdmission , AMSPI_PI_PatientDateofDischarge , AMSPI_PI_Archived ) VALUES  ( @Facility_Id , @AMSPI_PI_PatientVisitNumber , @AMSPI_PI_PatientName , @AMSPI_PI_PatientAge , @AMSPI_PI_PatientDateOfAdmission , @AMSPI_PI_PatientDateofDischarge , @AMSPI_PI_Archived )";
                using (SqlCommand SqlCommand_InsertAMSPIPI = new SqlCommand(SQLStringInsertAMSPIPI))
                {
                  SqlCommand_InsertAMSPIPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_InsertAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientVisitNumber", Session["VisitNumber"].ToString());
                  SqlCommand_InsertAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientName", Session["NameSurname"].ToString());
                  SqlCommand_InsertAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientAge", Session["Age"].ToString());
                  SqlCommand_InsertAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientDateOfAdmission", Session["AdmissionDate"].ToString());
                  SqlCommand_InsertAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientDateofDischarge", Session["DischargeDate"].ToString());
                  SqlCommand_InsertAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_Archived", 0);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertAMSPIPI);
                }
              }
              else
              {
                string SQLStringUpdateAMSPIPI = "UPDATE InfoQuest_Form_AMSPI_PatientInformation SET AMSPI_PI_PatientName = @AMSPI_PI_PatientName , AMSPI_PI_PatientAge = @AMSPI_PI_PatientAge , AMSPI_PI_PatientDateOfAdmission = @AMSPI_PI_PatientDateOfAdmission , AMSPI_PI_PatientDateofDischarge = @AMSPI_PI_PatientDateofDischarge WHERE Facility_Id = @Facility_Id AND AMSPI_PI_PatientVisitNumber = @AMSPI_PI_PatientVisitNumber ";
                using (SqlCommand SqlCommand_UpdateAMSPIPI = new SqlCommand(SQLStringUpdateAMSPIPI))
                {
                  SqlCommand_UpdateAMSPIPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                  SqlCommand_UpdateAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientVisitNumber", Session["VisitNumber"].ToString());
                  SqlCommand_UpdateAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientName", Session["NameSurname"].ToString());
                  SqlCommand_UpdateAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientAge", Session["Age"].ToString());
                  SqlCommand_UpdateAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientDateOfAdmission", Session["AdmissionDate"].ToString());
                  SqlCommand_UpdateAMSPIPI.Parameters.AddWithValue("@AMSPI_PI_PatientDateofDischarge", Session["DischargeDate"].ToString());
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateAMSPIPI);
                }
              }
              Session["AMSPIPIId"] = "";
            }
          }
        }
      }
    }

    private void SetFormVisibility()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('29')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@LOGON_USER", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();
          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '114'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '115'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '116'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '117'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              if (Request.QueryString["AMSPI_Intervention_Id"] != null)
              {
                if (Request.QueryString["ViewMode"] == "1")
                {
                  FormView_AMSPI_Form.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                  FormView_AMSPI_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
              else
              {
                FormView_AMSPI_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Session["Security"] = "0";
              FormView_AMSPI_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Session["Security"].ToString() == "1" && SecurityFacilityAdminUpdate.Length > 0)
            {
              Session["Security"] = "0";
              if (Request.QueryString["AMSPI_Intervention_Id"] != null)
              {
                if (Request.QueryString["ViewMode"] == "1")
                {
                  FormView_AMSPI_Form.ChangeMode(FormViewMode.Edit);
                }
                else
                {
                  FormView_AMSPI_Form.ChangeMode(FormViewMode.ReadOnly);
                }
              }
              else
              {
                FormView_AMSPI_Form.ChangeMode(FormViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              FormView_AMSPI_Form.ChangeMode(FormViewMode.ReadOnly);
            }
            Session["Security"] = "1";
          }
        }
      }
    }

    private void TablePatientInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["AMSPIPIPatientVisitNumber"] = "";
      Session["AMSPIPIPatientName"] = "";
      Session["AMSPIPIPatientAge"] = "";
      Session["AMSPIPIPatientDateOfAdmission"] = "";
      Session["AMSPIPIPatientDateofDischarge"] = "";

      string SQLStringPatientInfo = "SELECT DISTINCT Facility_FacilityDisplayName , AMSPI_PI_PatientVisitNumber , AMSPI_PI_PatientName , AMSPI_PI_PatientAge , AMSPI_PI_PatientDateOfAdmission , AMSPI_PI_PatientDateofDischarge FROM vForm_AMSPI_PatientInformation WHERE Facility_Id = @Facility_Id AND AMSPI_PI_PatientVisitNumber = @AMSPI_PI_PatientVisitNumber";
      using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
      {
        SqlCommand_PatientInfo.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PatientInfo.Parameters.AddWithValue("@AMSPI_PI_PatientVisitNumber", Request.QueryString["s_AMSPI_PatientVisitNumber"]);
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
              Session["AMSPIPIPatientVisitNumber"] = DataRow_Row["AMSPI_PI_PatientVisitNumber"];
              Session["AMSPIPIPatientName"] = DataRow_Row["AMSPI_PI_PatientName"];
              Session["AMSPIPIPatientAge"] = DataRow_Row["AMSPI_PI_PatientAge"];
              Session["AMSPIPIPatientDateOfAdmission"] = DataRow_Row["AMSPI_PI_PatientDateOfAdmission"];
              Session["AMSPIPIPatientDateofDischarge"] = DataRow_Row["AMSPI_PI_PatientDateofDischarge"];
            }
          }
        }
      }

      Label_PIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_PIVisitNumber.Text = Session["AMSPIPIPatientVisitNumber"].ToString();
      Label_PIName.Text = Session["AMSPIPIPatientName"].ToString();
      Label_PIAge.Text = Session["AMSPIPIPatientAge"].ToString();
      Label_PIDateAdmission.Text = Session["AMSPIPIPatientDateOfAdmission"].ToString();
      Label_PIDateDischarge.Text = Session["AMSPIPIPatientDateofDischarge"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["AMSPIPIPatientVisitNumber"] = "";
      Session["AMSPIPIPatientName"] = "";
      Session["AMSPIPIPatientAge"] = "";
      Session["AMSPIPIPatientDateOfAdmission"] = "";
      Session["AMSPIPIPatientDateofDischarge"] = "";
    }

    private void TableFormVisible()
    {
      if (FormView_AMSPI_Form.CurrentMode == FormViewMode.Insert)
      {
        SqlDataSource_AMSPI_InsertUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];

        ((TextBox)FormView_AMSPI_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_AMSPI_Form.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertUnit")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertCommunicationList")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_AMSPI_Form.FindControl("TextBox_InsertTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_AMSPI_Form.FindControl("TextBox_InsertTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertInterventionList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertInterventionInList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType1List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType2List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType3List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType4List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType7List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType9List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType10List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType11List")).Attributes.Add("OnChange", "Validation_Form();");

        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType3")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType4")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType5")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType6")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType7")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType8")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType9")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType10")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType11")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
      }

      if (FormView_AMSPI_Form.CurrentMode == FormViewMode.Edit)
      {
        SqlDataSource_AMSPI_EditUnit.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];

        ((TextBox)FormView_AMSPI_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_AMSPI_Form.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditUnit")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditCommunicationList")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_AMSPI_Form.FindControl("TextBox_EditTime")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_AMSPI_Form.FindControl("TextBox_EditTime")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditInterventionList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditInterventionInList")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType1List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType2List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType3List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType4List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType7List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType9List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType10List")).Attributes.Add("OnChange", "Validation_Form();");
        ((DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType11List")).Attributes.Add("OnChange", "Validation_Form();");

        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType1")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType2")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType3")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType4")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType5")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType6")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType7")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType8")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType9")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType10")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
        ((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType11")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();ShowHide_Form();");
      }
    }


    //--START-- --Search--//
    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_AMSPI.aspx";
      Response.Redirect(FinalURL, false);
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string Label_InvalidSearchMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InvalidSearchMessageText))
      {
        Response.Redirect("Form_AMSPI.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_AMSPI_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "", false);
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
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_UnitId"];
      string SearchField3 = Request.QueryString["Search_AMSPIPatientVisitNumber"];
      string SearchField4 = Request.QueryString["Search_AMSPIPatientName"];
      string SearchField5 = Request.QueryString["Search_AMSPIReportNumber"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null && SearchField4 == null && SearchField5 == null)
      {
        FinalURL = "Form_AMSPI_List.aspx";
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_Facility_Id=" + Request.QueryString["Search_FacilityId"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_Unit_Id=" + Request.QueryString["Search_UnitId"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_AMSPI_PatientVisitNumber=" + Request.QueryString["Search_AMSPIPatientVisitNumber"] + "&";
        }

        if (SearchField4 == null)
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_AMSPI_PatientName=" + Request.QueryString["Search_AMSPIPatientName"] + "&";
        }

        if (SearchField5 == null)
        {
          SearchField5 = "";
        }
        else
        {
          SearchField5 = "s_AMSPI_ReportNumber=" + Request.QueryString["Search_AMSPIReportNumber"] + "&";
        }

        string SearchURL = "Form_AMSPI_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --TableList--//
    protected void SqlDataSource_AMSPI_Intervention_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_AMSPI_Intervention.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_AMSPI_Intervention.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_AMSPI_Intervention.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_AMSPI_Intervention.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_AMSPI_Intervention_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_AMSPI_Intervention.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_AMSPI_Intervention.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_AMSPI_Intervention.PageSize > 20 && GridView_AMSPI_Intervention.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_AMSPI_Intervention.PageSize > 50 && GridView_AMSPI_Intervention.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_AMSPI_Intervention_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_AMSPI_Intervention.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_AMSPI_Intervention.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_AMSPI_Intervention.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_AMSPI_Intervention_RowCreated(object sender, GridViewRowEventArgs e)
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
      Response.Redirect("Form_AMSPI.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_AMSPI_PatientVisitNumber=" + Request.QueryString["s_AMSPI_PatientVisitNumber"] + "", false);
    }

    public string GetLink(object amspi_Intervention_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "" +
          "<a href='Form_AMSPI.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_AMSPI_PatientVisitNumber=" + Request.QueryString["s_AMSPI_PatientVisitNumber"] + "&AMSPI_Intervention_Id=" + amspi_Intervention_Id + "&ViewMode=0'>View</a>&nbsp;/&nbsp;" +
          "<a href='Form_AMSPI.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_AMSPI_PatientVisitNumber=" + Request.QueryString["s_AMSPI_PatientVisitNumber"] + "&AMSPI_Intervention_Id=" + amspi_Intervention_Id + "&ViewMode=1'>Update</a>";
        }
        else if (viewUpdate.ToString() == "No")
        {
          LinkURL = "" +
          "<a href='Form_AMSPI.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_AMSPI_PatientVisitNumber=" + Request.QueryString["s_AMSPI_PatientVisitNumber"] + "&AMSPI_Intervention_Id=" + amspi_Intervention_Id + "&ViewMode=0'>View</a>";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      FinalURL = CurrentURL;

      return FinalURL;
    }
    //---END--- --TableList--//


    //--START-- --TableForm--//
    protected void FormView_AMSPI_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_AMSPI_Form.FindControl("Label_InvalidForm")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_AMSPI_Form.FindControl("Label_ConcurrencyUpdate")).Text = "";
        }
        else if (e.Cancel == false)
        {
          Session["AMSPI_Intervention_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Request.QueryString["s_Facility_Id"], "29");

          SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_ReportNumber"].DefaultValue = Session["AMSPI_Intervention_ReportNumber"].ToString();
          SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_History"].DefaultValue = "";
          SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_IsActive"].DefaultValue = "true";




          int TypeTotal = 0;
          for (int a = 1; a <= 11; a++)
          {
            if (((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType" + a + "")).Checked == true)
            {
              TypeTotal = TypeTotal + 1;
            }
          }

          SqlDataSource_AMSPI_Form.InsertParameters["AMSPI_Intervention_TypeTotal"].DefaultValue = TypeTotal.ToString(CultureInfo.CurrentCulture);

          Session["AMSPI_Intervention_ReportNumber"] = "";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        TextBox TextBox_InsertDate = (TextBox)FormView_AMSPI_Form.FindControl("TextBox_InsertDate");
        string DateToValidate = TextBox_InsertDate.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        string ValidDates = "Yes";
        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid date, date must be in the format yyyy/mm/dd<br />";
          ValidDates = "No";
        }

        if (ValidDates == "Yes")
        {
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_AMSPI_Form.FindControl("TextBox_InsertDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          Session["CutOffDay"] = "";
          string SQLStringCutOffDay = "SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 29";
          using (SqlCommand SqlCommand_CutOffDay = new SqlCommand(SQLStringCutOffDay))
          {
            DataTable DataTable_CutOffDay;
            using (DataTable_CutOffDay = new DataTable())
            {
              DataTable_CutOffDay.Locale = CultureInfo.CurrentCulture;
              DataTable_CutOffDay = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CutOffDay).Copy();
              if (DataTable_CutOffDay.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_CutOffDay.Rows)
                {
                  Session["CutOffDay"] = DataRow_Row["ValidCutOffDay"];
                }
              }
            }
          }

          if (string.IsNullOrEmpty(Session["CutOffDay"].ToString()))
          {
            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
            }
          }
          else
          {
            int CutOffDay = Convert.ToInt32(Session["CutOffDay"].ToString(), CultureInfo.CurrentCulture);

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
            }
            else
            {
              Session["CorrectDate"] = InsertValidation_CorrectDate(PickedDate, CurrentDate, CutOffDay);

              if (Session["CorrectDate"].ToString() == "0")
              {
                InvalidFormMessage = InvalidFormMessage + "Date selection is not valid. Forms may be captured between the 1st of a calendar month until the " + CutOffDay + "th of the following month <br />";
              }
              Session["CorrectDate"] = "";
            }
          }
          Session["CutOffDay"] = "";
        }
      }

      return InvalidFormMessage;
    }

    protected static string InsertValidation_CorrectDate(DateTime pickedDate, DateTime currentDate, int cutoffDay)
    {
      string CorrectDate = "";

      int PickedDateMonth = pickedDate.Month;
      int PickedDateYear = pickedDate.Year;

      int CurrentDateDay = currentDate.Day;
      int CurrentDateMonth = currentDate.Month;
      int CurrentDateYear = currentDate.Year;

      if ((PickedDateYear == CurrentDateYear) && (PickedDateMonth == CurrentDateMonth))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth + 1 == CurrentDateMonth) && (PickedDateYear == CurrentDateYear) && (CurrentDateDay < cutoffDay))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth + 1 == CurrentDateMonth) && (PickedDateYear == CurrentDateYear) && (CurrentDateDay > cutoffDay))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth + 1 < CurrentDateMonth) && (PickedDateYear == CurrentDateYear))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth == 12) && (CurrentDateMonth == 1) && (PickedDateYear + 1 == CurrentDateYear) && (CurrentDateDay < cutoffDay))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth == 12) && (CurrentDateMonth == 1) && (PickedDateYear + 1 == CurrentDateYear) && (CurrentDateDay > cutoffDay))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth <= 12) && (CurrentDateMonth > 1) && (PickedDateYear + 1 == CurrentDateYear))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth <= 12) && (CurrentDateMonth > 1) && (PickedDateYear + 1 < CurrentDateYear))
      {
        CorrectDate = "0";
      }

      return CorrectDate;
    }

    protected void SqlDataSource_AMSPI_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["AMSPI_Intervention_Id"] = e.Command.Parameters["@AMSPI_Intervention_Id"].Value;
        Session["AMSPI_Intervention_ReportNumber"] = e.Command.Parameters["@AMSPI_Intervention_ReportNumber"].Value;
        Response.Redirect("InfoQuest_ReportNumber.aspx?ReportPage=Form_AMSPI&ReportNumber=" + Session["AMSPI_Intervention_ReportNumber"].ToString() + "", false);
      }
    }


    protected void FormView_AMSPI_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDAMSPIInterventionModifiedDate"] = e.OldValues["AMSPI_Intervention_ModifiedDate"];
        object OLDAMSPIInterventionModifiedDate = Session["OLDAMSPIInterventionModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDAMSPIInterventionModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareAMSPI = (DataView)SqlDataSource_AMSPI_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareAMSPI = DataView_CompareAMSPI[0];
        Session["DBAMSPIInterventionModifiedDate"] = Convert.ToString(DataRowView_CompareAMSPI["AMSPI_Intervention_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBAMSPIInterventionModifiedBy"] = Convert.ToString(DataRowView_CompareAMSPI["AMSPI_Intervention_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBAMSPIInterventionModifiedDate = Session["DBAMSPIInterventionModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBAMSPIInterventionModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_AMSPI_Form.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_AMSPI_Form.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBAMSPIInterventionModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_EditInvalidFormMessage = EditValidation(e.OldValues["AMSPI_Intervention_Date"]);

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
            ((Label)FormView_AMSPI_Form.FindControl("Label_InvalidForm")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_AMSPI_Form.FindControl("Label_ConcurrencyUpdate")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["AMSPI_Intervention_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["AMSPI_Intervention_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_AMSPI_Intervention", "AMSPI_Intervention_Id = " + Request.QueryString["AMSPI_Intervention_Id"]);

            DataView DataView_AMSPI = (DataView)SqlDataSource_AMSPI_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_AMSPI = DataView_AMSPI[0];
            Session["AMSPIInterventionHistory"] = Convert.ToString(DataRowView_AMSPI["AMSPI_Intervention_History"], CultureInfo.CurrentCulture);

            Session["AMSPIInterventionHistory"] = Session["History"].ToString() + Session["AMSPIInterventionHistory"].ToString();
            e.NewValues["AMSPI_Intervention_History"] = Session["AMSPIInterventionHistory"].ToString();

            Session["AMSPIInterventionHistory"] = "";
            Session["History"] = "";

            int TypeTotal = 0;
            for (int a = 1; a <= 11; a++)
            {
              if (((CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType" + a + "")).Checked == true)
              {
                TypeTotal = TypeTotal + 1;
              }
            }

            e.NewValues["AMSPI_Intervention_TypeTotal"] = TypeTotal.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDAMSPIInterventionModifiedDate"] = "";
        Session["DBAMSPIInterventionModifiedDate"] = "";
        Session["DBAMSPIInterventionModifiedBy"] = "";
      }
    }

    protected string EditValidation(object oldAMSPI_Intervention_Date)
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        TextBox TextBox_EditDate = (TextBox)FormView_AMSPI_Form.FindControl("TextBox_EditDate");
        string DateToValidate = TextBox_EditDate.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        string ValidDates = "Yes";
        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          InvalidFormMessage = InvalidFormMessage + "Not a valid date, date must be in the format yyyy/mm/dd<br />";
          ValidDates = "No";
        }

        if (ValidDates == "Yes")
        {
          Session["Date"] = Convert.ToDateTime(oldAMSPI_Intervention_Date, CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);

          DateTime DBDate = Convert.ToDateTime(Session["Date"].ToString(), CultureInfo.CurrentCulture);
          DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_AMSPI_Form.FindControl("TextBox_EditDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if ((PickedDate).CompareTo(DBDate) != 0)
          {
            Session["CutOffDay"] = "";
            string SQLStringCutOffDay = "SELECT CASE WHEN Form_CutOffDay IS NULL THEN DAY(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ELSE Form_CutOffDay END AS ValidCutOffDay FROM Administration_Form WHERE Form_Id = 29";
            using (SqlCommand SqlCommand_CutOffDay = new SqlCommand(SQLStringCutOffDay))
            {
              DataTable DataTable_CutOffDay;
              using (DataTable_CutOffDay = new DataTable())
              {
                DataTable_CutOffDay.Locale = CultureInfo.CurrentCulture;
                DataTable_CutOffDay = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_CutOffDay).Copy();
                if (DataTable_CutOffDay.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_CutOffDay.Rows)
                  {
                    Session["CutOffDay"] = DataRow_Row["ValidCutOffDay"];
                  }
                }
                else
                {
                  Session["CutOffDay"] = "";
                }
              }
            }

            if (string.IsNullOrEmpty(Session["CutOffDay"].ToString()))
            {
              if (PickedDate.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
              }
            }
            else
            {
              int CutOffDay = Convert.ToInt32(Session["CutOffDay"].ToString(), CultureInfo.CurrentCulture);

              if (PickedDate.CompareTo(CurrentDate) > 0)
              {
                InvalidFormMessage = InvalidFormMessage + "No future Observation dates allowed<br />";
              }
              else
              {
                int PickedDateMonth = PickedDate.Month;
                int PickedDateYear = PickedDate.Year;

                Session["CorrectDate"] = EditValidation_CorrectDate(PickedDate, CurrentDate, CutOffDay);

                if (Session["CorrectDate"].ToString() == "0")
                {
                  DateTime OldDate = Convert.ToDateTime(oldAMSPI_Intervention_Date, CultureInfo.CurrentCulture);

                  int OldPickedDateMonth = OldDate.Month;
                  int OldPickedDateYear = OldDate.Year;

                  if ((PickedDateMonth != OldPickedDateMonth) || (PickedDateYear != OldPickedDateYear))
                  {
                    InvalidFormMessage = InvalidFormMessage + "Date selection is not valid. Forms may be captured between the 1st of a calendar month until the " + CutOffDay + "th of the following month<br />";
                  }
                }
                Session["CorrectDate"] = "";
              }
            }
            Session["CutOffDay"] = "";
          }
          Session["Date"] = "";
        }
      }

      return InvalidFormMessage;
    }

    protected static string EditValidation_CorrectDate(DateTime pickedDate, DateTime currentDate, int cutoffDay)
    {
      string CorrectDate = "";

      int PickedDateMonth = pickedDate.Month;
      int PickedDateYear = pickedDate.Year;

      int CurrentDateDay = currentDate.Day;
      int CurrentDateMonth = currentDate.Month;
      int CurrentDateYear = currentDate.Year;

      if ((PickedDateYear == CurrentDateYear) && (PickedDateMonth == CurrentDateMonth))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth + 1 == CurrentDateMonth) && (PickedDateYear == CurrentDateYear) && (CurrentDateDay < cutoffDay))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth + 1 == CurrentDateMonth) && (PickedDateYear == CurrentDateYear) && (CurrentDateDay > cutoffDay))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth + 1 < CurrentDateMonth) && (PickedDateYear == CurrentDateYear))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth == 12) && (CurrentDateMonth == 1) && (PickedDateYear + 1 == CurrentDateYear) && (CurrentDateDay < cutoffDay))
      {
        CorrectDate = "1";
      }

      if ((PickedDateMonth == 12) && (CurrentDateMonth == 1) && (PickedDateYear + 1 == CurrentDateYear) && (CurrentDateDay > cutoffDay))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth <= 12) && (CurrentDateMonth > 1) && (PickedDateYear + 1 == CurrentDateYear))
      {
        CorrectDate = "0";
      }

      if ((PickedDateMonth <= 12) && (CurrentDateMonth > 1) && (PickedDateYear + 1 < CurrentDateYear))
      {
        CorrectDate = "0";
      }

      return CorrectDate;
    }

    protected void SqlDataSource_AMSPI_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;
            Response.Redirect("Form_AMSPI.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_AMSPI_PatientVisitNumber=" + Request.QueryString["s_AMSPI_PatientVisitNumber"] + "", false);
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;

            ClientScript.RegisterStartupScript(this.GetType(), "Print", "<script language='javascript'>FormPrint('InfoQuest_Print.aspx?PrintPage=Form_AMSPI&PrintValue=" + Request.QueryString["AMSPI_Intervention_Id"] + "')</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "Reload", "<script language='javascript'>window.location.href='" + Request.Url.AbsoluteUri + "'</script>");
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ClientScript.RegisterStartupScript(this.GetType(), "Email", "<script language='javascript'>FormEmail('InfoQuest_Email.aspx?EmailPage=Form_AMSPI&EmailValue=" + Request.QueryString["AMSPI_Intervention_Id"] + "')</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "Reload", "<script language='javascript'>window.location.href='" + Request.Url.AbsoluteUri + "'</script>");
          }
        }
      }
    }


    protected void FormView_AMSPI_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["AMSPI_Intervention_Id"] != null)
          {
            Response.Redirect("Form_AMSPI.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_AMSPI_PatientVisitNumber=" + Request.QueryString["s_AMSPI_PatientVisitNumber"] + "", false);
          }
        }
      }
    }

    protected void FormView_AMSPI_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_AMSPI_Form.CurrentMode == FormViewMode.Edit)
      {
        if (Request.QueryString["AMSPI_Intervention_Id"] != null)
        {
          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 29";
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
            ((Button)FormView_AMSPI_Form.FindControl("Button_EditPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_AMSPI_Form.FindControl("Button_EditPrint")).Visible = true;
          }

          if (Email == "False")
          {
            ((Button)FormView_AMSPI_Form.FindControl("Button_EditEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_AMSPI_Form.FindControl("Button_EditEmail")).Visible = true;
          }

          Email = "";
          Print = "";
        }
      }

      if (FormView_AMSPI_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        if (Request.QueryString["AMSPI_Intervention_Id"] != null)
        {
          Session["UnitName"] = "";
          Session["AMSPIInterventionCommunicationName"] = "";
          Session["AMSPIInterventionInterventionName"] = "";
          Session["AMSPIInterventionInterventionInName"] = "";
          Session["AMSPIInterventionType1Name"] = "";
          Session["AMSPIInterventionType2Name"] = "";
          Session["AMSPIInterventionType3Name"] = "";
          Session["AMSPIInterventionType4Name"] = "";
          Session["AMSPIInterventionType7Name"] = "";
          Session["AMSPIInterventionType9Name"] = "";
          Session["AMSPIInterventionType10Name"] = "";
          Session["AMSPIInterventionType11Name"] = "";
          string SQLStringAMSPIIntervention = "SELECT Unit_Name , AMSPI_Intervention_Communication_Name , AMSPI_Intervention_Intervention_Name , AMSPI_Intervention_InterventionIn_Name , AMSPI_Intervention_Type1_Name , AMSPI_Intervention_Type2_Name , AMSPI_Intervention_Type3_Name , AMSPI_Intervention_Type4_Name , AMSPI_Intervention_Type7_Name , AMSPI_Intervention_Type9_Name , AMSPI_Intervention_Type10_Name , AMSPI_Intervention_Type11_Name FROM vForm_AMSPI_Intervention WHERE AMSPI_Intervention_Id = @AMSPI_Intervention_Id";
          using (SqlCommand SqlCommand_AMSPIIntervention = new SqlCommand(SQLStringAMSPIIntervention))
          {
            SqlCommand_AMSPIIntervention.Parameters.AddWithValue("@AMSPI_Intervention_Id", Request.QueryString["AMSPI_Intervention_Id"]);
            DataTable DataTable_AMSPIIntervention;
            using (DataTable_AMSPIIntervention = new DataTable())
            {
              DataTable_AMSPIIntervention.Locale = CultureInfo.CurrentCulture;
              DataTable_AMSPIIntervention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_AMSPIIntervention).Copy();
              if (DataTable_AMSPIIntervention.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_AMSPIIntervention.Rows)
                {
                  Session["UnitName"] = DataRow_Row["Unit_Name"];
                  Session["AMSPIInterventionCommunicationName"] = DataRow_Row["AMSPI_Intervention_Communication_Name"];
                  Session["AMSPIInterventionInterventionName"] = DataRow_Row["AMSPI_Intervention_Intervention_Name"];
                  Session["AMSPIInterventionInterventionInName"] = DataRow_Row["AMSPI_Intervention_InterventionIn_Name"];
                  Session["AMSPIInterventionType1Name"] = DataRow_Row["AMSPI_Intervention_Type1_Name"];
                  Session["AMSPIInterventionType2Name"] = DataRow_Row["AMSPI_Intervention_Type2_Name"];
                  Session["AMSPIInterventionType3Name"] = DataRow_Row["AMSPI_Intervention_Type3_Name"];
                  Session["AMSPIInterventionType4Name"] = DataRow_Row["AMSPI_Intervention_Type4_Name"];
                  Session["AMSPIInterventionType7Name"] = DataRow_Row["AMSPI_Intervention_Type7_Name"];
                  Session["AMSPIInterventionType9Name"] = DataRow_Row["AMSPI_Intervention_Type9_Name"];
                  Session["AMSPIInterventionType10Name"] = DataRow_Row["AMSPI_Intervention_Type10_Name"];
                  Session["AMSPIInterventionType11Name"] = DataRow_Row["AMSPI_Intervention_Type11_Name"];
                }
              }
            }
          }

          Label Label_ItemUnit = (Label)FormView_AMSPI_Form.FindControl("Label_ItemUnit");
          Label_ItemUnit.Text = Session["UnitName"].ToString();

          Label Label_ItemCommunicationList = (Label)FormView_AMSPI_Form.FindControl("Label_ItemCommunicationList");
          Label_ItemCommunicationList.Text = Session["AMSPIInterventionCommunicationName"].ToString();

          Label Label_ItemInterventionList = (Label)FormView_AMSPI_Form.FindControl("Label_ItemInterventionList");
          Label_ItemInterventionList.Text = Session["AMSPIInterventionInterventionName"].ToString();

          Label Label_ItemInterventionInList = (Label)FormView_AMSPI_Form.FindControl("Label_ItemInterventionInList");
          Label_ItemInterventionInList.Text = Session["AMSPIInterventionInterventionInName"].ToString();

          Label Label_ItemType1List = (Label)FormView_AMSPI_Form.FindControl("Label_ItemType1List");
          Label_ItemType1List.Text = Session["AMSPIInterventionType1Name"].ToString();

          Label Label_ItemType2List = (Label)FormView_AMSPI_Form.FindControl("Label_ItemType2List");
          Label_ItemType2List.Text = Session["AMSPIInterventionType2Name"].ToString();

          Label Label_ItemType3List = (Label)FormView_AMSPI_Form.FindControl("Label_ItemType3List");
          Label_ItemType3List.Text = Session["AMSPIInterventionType3Name"].ToString();

          Label Label_ItemType4List = (Label)FormView_AMSPI_Form.FindControl("Label_ItemType4List");
          Label_ItemType4List.Text = Session["AMSPIInterventionType4Name"].ToString();

          Label Label_ItemType7List = (Label)FormView_AMSPI_Form.FindControl("Label_ItemType7List");
          Label_ItemType7List.Text = Session["AMSPIInterventionType7Name"].ToString();

          Label Label_ItemType9List = (Label)FormView_AMSPI_Form.FindControl("Label_ItemType9List");
          Label_ItemType9List.Text = Session["AMSPIInterventionType9Name"].ToString();

          Label Label_ItemType10List = (Label)FormView_AMSPI_Form.FindControl("Label_ItemType10List");
          Label_ItemType10List.Text = Session["AMSPIInterventionType10Name"].ToString();

          Label Label_ItemType11List = (Label)FormView_AMSPI_Form.FindControl("Label_ItemType11List");
          Label_ItemType11List.Text = Session["AMSPIInterventionType11Name"].ToString();

          Session["UnitName"] = "";
          Session["AMSPIInterventionCommunicationName"] = "";
          Session["AMSPIInterventionInterventionName"] = "";
          Session["AMSPIInterventionInterventionInName"] = "";
          Session["AMSPIInterventionType1Name"] = "";
          Session["AMSPIInterventionType2Name"] = "";
          Session["AMSPIInterventionType3Name"] = "";
          Session["AMSPIInterventionType4Name"] = "";
          Session["AMSPIInterventionType7Name"] = "";
          Session["AMSPIInterventionType9Name"] = "";
          Session["AMSPIInterventionType10Name"] = "";
          Session["AMSPIInterventionType11Name"] = "";

          string Email = "";
          string Print = "";
          string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 29";
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
            ((Button)FormView_AMSPI_Form.FindControl("Button_ItemPrint")).Visible = false;
          }
          else
          {
            ((Button)FormView_AMSPI_Form.FindControl("Button_ItemPrint")).Visible = true;
            ((Button)FormView_AMSPI_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('InfoQuest_Print.aspx?PrintPage=Form_AMSPI&PrintValue=" + Request.QueryString["AMSPI_Intervention_Id"] + "')";
          }

          if (Email == "False")
          {
            ((Button)FormView_AMSPI_Form.FindControl("Button_ItemEmail")).Visible = false;
          }
          else
          {
            ((Button)FormView_AMSPI_Form.FindControl("Button_ItemEmail")).Visible = true;
            ((Button)FormView_AMSPI_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('InfoQuest_Email.aspx?EmailPage=Form_AMSPI&EmailValue=" + Request.QueryString["AMSPI_Intervention_Id"] + "')";
          }

          Email = "";
          Print = "";
        }
      }
    }


    protected void CustomValidator_InsertType1List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_InsertType1 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType1");
        DropDownList DropDownList_InsertType1List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType1List");

        if (CheckBox_InsertType1.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertType1List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_EditType1List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_EditType1 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType1");
        DropDownList DropDownList_EditType1List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType1List");

        if (CheckBox_EditType1.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditType1List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_InsertType2List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_InsertType2 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType2");
        DropDownList DropDownList_InsertType2List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType2List");

        if (CheckBox_InsertType2.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertType2List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_EditType2List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_EditType2 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType2");
        DropDownList DropDownList_EditType2List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType2List");

        if (CheckBox_EditType2.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditType2List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_InsertType3List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_InsertType3 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType3");
        DropDownList DropDownList_InsertType3List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType3List");

        if (CheckBox_InsertType3.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertType3List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_EditType3List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_EditType3 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType3");
        DropDownList DropDownList_EditType3List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType3List");

        if (CheckBox_EditType3.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditType3List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_InsertType4List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_InsertType4 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType4");
        DropDownList DropDownList_InsertType4List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType4List");

        if (CheckBox_InsertType4.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertType4List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_EditType4List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_EditType4 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType4");
        DropDownList DropDownList_EditType4List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType4List");

        if (CheckBox_EditType4.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditType4List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_InsertType7List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_InsertType7 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType7");
        DropDownList DropDownList_InsertType7List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType7List");

        if (CheckBox_InsertType7.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertType7List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_EditType7List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_EditType7 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType7");
        DropDownList DropDownList_EditType7List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType7List");

        if (CheckBox_EditType7.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditType7List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_InsertType9List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_InsertType9 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType9");
        DropDownList DropDownList_InsertType9List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType9List");

        if (CheckBox_InsertType9.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertType9List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_EditType9List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_EditType9 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType9");
        DropDownList DropDownList_EditType9List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType9List");

        if (CheckBox_EditType9.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditType9List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_InsertType10List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_InsertType10 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType10");
        DropDownList DropDownList_InsertType10List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType10List");

        if (CheckBox_InsertType10.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertType10List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_EditType10List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_EditType10 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType10");
        DropDownList DropDownList_EditType10List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType10List");

        if (CheckBox_EditType10.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditType10List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_InsertType11List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_InsertType11 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_InsertType11");
        DropDownList DropDownList_InsertType11List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_InsertType11List");

        if (CheckBox_InsertType11.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_InsertType11List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
      }
    }

    protected void CustomValidator_EditType11List_ServerValidate(object sender, ServerValidateEventArgs e)
    {
      if (e != null)
      {
        CheckBox CheckBox_EditType11 = (CheckBox)FormView_AMSPI_Form.FindControl("CheckBox_EditType11");
        DropDownList DropDownList_EditType11List = (DropDownList)FormView_AMSPI_Form.FindControl("DropDownList_EditType11List");

        if (CheckBox_EditType11.Checked == true)
        {
          if (string.IsNullOrEmpty(DropDownList_EditType11List.SelectedValue))
          {
            e.IsValid = false;
          }
          else
          {
            e.IsValid = true;
          }
        }
        else
        {
          e.IsValid = true;
        }
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
  }
}