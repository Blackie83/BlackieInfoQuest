using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace InfoQuestAdministration
{
  public partial class InfoQuest_DisasterRecovery : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected Dictionary<string, string> FileContentTypeHandler = new Dictionary<string, string>();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_InfoQuestSystemAdministrator.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfoQuestSystemServer.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_PatientEDWFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_PatientODSFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_PatientODSPXMPostDischargeSurveyFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_PatientODSPatientSearchFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_InfoQuestConnectionString.Text = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
          Label_EmployeeVisionConnectionString.Text = InfoQuestWCF.InfoQuest_Connections.Connections("EmployeeDetailVision");

          Label_PatientEDWConnectionString.Text = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailEDW");
          GridView_PatientEDWPatientInformation.DataSource = null;
          GridView_PatientEDWPatientInformation.DataBind();
          Label_PatientEDWPatientInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientEDWVisitInformation.DataSource = null;
          GridView_PatientEDWVisitInformation.DataBind();
          Label_PatientEDWVisitInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientEDWTheatreInformation.DataSource = null;
          GridView_PatientEDWTheatreInformation.DataBind();
          Label_PatientEDWTheatreInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientEDWIPSVisitInformation.DataSource = null;
          GridView_PatientEDWIPSVisitInformation.DataBind();
          Label_PatientEDWIPSVisitInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientEDWIPSTheatreInformation.DataSource = null;
          GridView_PatientEDWIPSTheatreInformation.DataBind();
          Label_PatientEDWIPSTheatreInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientEDWIPSCodingInformation.DataSource = null;
          GridView_PatientEDWIPSCodingInformation.DataBind();
          Label_PatientEDWIPSCodingInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientEDWIPSAccommodationInformation.DataSource = null;
          GridView_PatientEDWIPSAccommodationInformation.DataBind();
          Label_PatientEDWIPSAccommodationInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientEDWIPSAntibioticInformation.DataSource = null;
          GridView_PatientEDWIPSAntibioticInformation.DataBind();
          Label_PatientEDWIPSAntibioticInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

          Label_PatientODSConnectionString.Text = InfoQuestWCF.InfoQuest_Connections.Connections("PatientDetailODS");
          GridView_PatientODSVisitInformation.DataBind();
          Label_PatientODSVisitInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientODSCodingInformation.DataSource = null;
          GridView_PatientODSCodingInformation.DataBind();
          Label_PatientODSCodingInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientODSAccommodationInformation.DataSource = null;
          GridView_PatientODSAccommodationInformation.DataBind();
          Label_PatientODSAccommodationInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientODSPractitionerInformation.DataSource = null;
          GridView_PatientODSPractitionerInformation.DataBind();
          Label_PatientODSPractitionerInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientODSPXMPostDischargeSurvey.DataSource = null;
          GridView_PatientODSPXMPostDischargeSurvey.DataBind();
          Label_PatientODSPXMPostDischargeSurveyTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          GridView_PatientODSPatientSearch.DataSource = null;
          GridView_PatientODSPatientSearch.DataBind();
          Label_PatientODSPatientSearchTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

          Label_EmailServer.Text = Dns.GetHostEntry(Environment.MachineName).HostName.ToString().ToLower(CultureInfo.CurrentCulture);
          Label_File_Folder.Text = UploadPath();

          UploadedFiles();

          DirectoryCleanUp();
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
        SecurityAllow = "0";
        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("No Access", "InfoQuest_PageText.aspx?PageTextValue=5"), false);
        Response.End();
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("0");

      if (PageSecurity() == "1")
      {
        ((Label)PageUpdateProgress_DisasterRecovery.FindControl("Label_UpdateProgress")).Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }


    //--START-- --InfoQuest--//
    protected void SqlDataSource_InfoQuestSystemAdministrator_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_InfoQuestSystemAdministratorTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_InfoQuestSystemAdministrator_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_InfoQuestSystemAdministrator_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void SqlDataSource_InfoQuestSystemServer_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_InfoQuestSystemServerTotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_InfoQuestSystemServer_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void GridView_InfoQuestSystemServer_RowCreated(object sender, GridViewRowEventArgs e)
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
    //---END--- --InfoQuest--//


    //--START-- --Employee Data--//
    protected void Button_Employee_Click(object sender, EventArgs e)
    {
      EmployeeAD();

      EmployeeVision();
    }

    protected void Button_EmployeeClear_Click(object sender, EventArgs e)
    {
      Label_EmployeeUserNameInvalidMessage.Text = "";
      TextBox_EmployeeUserName.Text = "";

      Label_EmployeeADError.Text = "";
      Label_EmployeeADUserName.Text = "";
      Label_EmployeeADDisplayName.Text = "";
      Label_EmployeeADFirstName.Text = "";
      Label_EmployeeADLastName.Text = "";
      Label_EmployeeADEmployeeNumber.Text = "";
      Label_EmployeeADEmail.Text = "";
      Label_EmployeeADManagerUserName.Text = "";

      Label_EmployeeVisionError.Text = "";
      Label_EmployeeVisionDisplayName.Text = "";
      Label_EmployeeVisionEmployeeNumber.Text = "";
    }

    private void EmployeeAD()
    {
      if (string.IsNullOrEmpty(TextBox_EmployeeUserName.Text))
      {
        Label_EmployeeUserNameInvalidMessage.Text = Convert.ToString("UserName Required", CultureInfo.CurrentCulture);
        Label_EmployeeADError.Text = Convert.ToString("UserName Required", CultureInfo.CurrentCulture);

        Label_EmployeeADUserName.Text = "";
        Label_EmployeeADDisplayName.Text = "";
        Label_EmployeeADFirstName.Text = "";
        Label_EmployeeADLastName.Text = "";
        Label_EmployeeADEmployeeNumber.Text = "";
        Label_EmployeeADEmail.Text = "";
        Label_EmployeeADManagerUserName.Text = "";
      }
      else
      {
        Label_EmployeeUserNameInvalidMessage.Text = "";

        string AD_UserName = "";
        string AD_DisplayName = "";
        string AD_FirstName = "";
        string AD_LastName = "";
        string AD_EmployeeNumber = "";
        string AD_Email = "";
        string AD_ManagerUserName = "";
        string AD_Error = "";

        DataTable DataTable_AD;
        using (DataTable_AD = new DataTable())
        {
          DataTable_AD.Locale = CultureInfo.CurrentCulture;
          DataTable_AD = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(TextBox_EmployeeUserName.Text).Copy();
          if (DataTable_AD.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_AD.Rows)
            {
              AD_Error = DataRow_Row["Error"].ToString();
            }

            AD_UserName = "";
            AD_DisplayName = "";
            AD_FirstName = "";
            AD_LastName = "";
            AD_EmployeeNumber = "";
            AD_Email = "";
            AD_ManagerUserName = "";
          }
          else if (DataTable_AD.Columns.Count != 1)
          {
            if (DataTable_AD.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_AD.Rows)
              {
                AD_UserName = DataRow_Row["UserName"].ToString();
                AD_DisplayName = DataRow_Row["DisplayName"].ToString();
                AD_FirstName = DataRow_Row["FirstName"].ToString();
                AD_LastName = DataRow_Row["LastName"].ToString();
                AD_EmployeeNumber = DataRow_Row["EmployeeNumber"].ToString();
                AD_Email = DataRow_Row["Email"].ToString();
                AD_ManagerUserName = DataRow_Row["ManagerUserName"].ToString();
              }
            }
            else
            {

              AD_Error = Convert.ToString("No Employee Data", CultureInfo.CurrentCulture);
              AD_UserName = "";
              AD_DisplayName = "";
              AD_FirstName = "";
              AD_LastName = "";
              AD_EmployeeNumber = "";
              AD_Email = "";
              AD_ManagerUserName = "";
            }
          }
        }

        if (string.IsNullOrEmpty(AD_Error))
        {
          Label_EmployeeADError.Text = Convert.ToString("No Error", CultureInfo.CurrentCulture);
          Label_EmployeeADUserName.Text = AD_UserName;
          Label_EmployeeADDisplayName.Text = AD_DisplayName;
          Label_EmployeeADFirstName.Text = AD_FirstName;
          Label_EmployeeADLastName.Text = AD_LastName;
          Label_EmployeeADEmployeeNumber.Text = AD_EmployeeNumber;
          Label_EmployeeADEmail.Text = AD_Email;
          Label_EmployeeADManagerUserName.Text = AD_ManagerUserName;
        }
        else
        {
          Label_EmployeeADError.Text = AD_Error;
          Label_EmployeeADUserName.Text = "";
          Label_EmployeeADDisplayName.Text = "";
          Label_EmployeeADFirstName.Text = "";
          Label_EmployeeADLastName.Text = "";
          Label_EmployeeADEmployeeNumber.Text = "";
          Label_EmployeeADEmail.Text = "";
          Label_EmployeeADManagerUserName.Text = "";
        }
      }
    }

    private void EmployeeVision()
    {
      if (string.IsNullOrEmpty(TextBox_EmployeeUserName.Text))
      {
        Label_EmployeeUserNameInvalidMessage.Text = Convert.ToString("UserName Required", CultureInfo.CurrentCulture);
        Label_EmployeeVisionError.Text = Convert.ToString("UserName Required", CultureInfo.CurrentCulture);

        Label_EmployeeVisionDisplayName.Text = "";
        Label_EmployeeVisionEmployeeNumber.Text = "";
      }
      else
      {
        Label_EmployeeUserNameInvalidMessage.Text = "";

        string AD_EmployeeNumber = "";
        string AD_Error = "";

        DataTable DataTable_AD;
        using (DataTable_AD = new DataTable())
        {
          DataTable_AD.Locale = CultureInfo.CurrentCulture;
          DataTable_AD = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindOne_UserName(TextBox_EmployeeUserName.Text).Copy();
          if (DataTable_AD.Columns.Count == 1)
          {
            foreach (DataRow DataRow_Row in DataTable_AD.Rows)
            {
              AD_Error = DataRow_Row["Error"].ToString();
            }

            AD_EmployeeNumber = "";
          }
          else if (DataTable_AD.Columns.Count != 1)
          {
            if (DataTable_AD.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_AD.Rows)
              {
                AD_EmployeeNumber = DataRow_Row["EmployeeNumber"].ToString();
              }
            }
            else
            {
              AD_EmployeeNumber = "";
            }
          }
        }

        if (string.IsNullOrEmpty(AD_Error))
        {
          string Vision_DisplayName = "";
          string Vision_EmployeeNumber = "";
          string Vision_Error = "";

          DataTable DataTable_Vision;
          using (DataTable_Vision = new DataTable())
          {
            DataTable_Vision.Locale = CultureInfo.CurrentCulture;
            DataTable_Vision = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_Vision_FindDisplayName_SearchEmployeeNumber(AD_EmployeeNumber).Copy();
            if (DataTable_Vision.Columns.Count == 1)
            {
              foreach (DataRow DataRow_Row in DataTable_Vision.Rows)
              {
                Vision_Error = DataRow_Row["Error"].ToString();
              }

              Vision_DisplayName = "";
              Vision_EmployeeNumber = "";
            }
            else if (DataTable_Vision.Columns.Count != 1)
            {
              if (DataTable_Vision.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Vision.Rows)
                {
                  Vision_DisplayName = DataRow_Row["DisplayName"].ToString();
                  Vision_EmployeeNumber = DataRow_Row["EmployeeNumber"].ToString();
                }
              }
              else
              {
                Vision_Error = Convert.ToString("No Employee Data", CultureInfo.CurrentCulture);
                Vision_DisplayName = "";
                Vision_EmployeeNumber = "";
              }
            }
          }

          if (string.IsNullOrEmpty(Vision_Error))
          {
            Label_EmployeeVisionError.Text = Convert.ToString("No Error", CultureInfo.CurrentCulture);
            Label_EmployeeVisionDisplayName.Text = Vision_DisplayName;
            Label_EmployeeVisionEmployeeNumber.Text = Vision_EmployeeNumber;
          }
          else
          {
            Label_EmployeeVisionError.Text = Vision_Error;
            Label_EmployeeVisionDisplayName.Text = "";
            Label_EmployeeVisionEmployeeNumber.Text = "";
          }
        }
        else
        {
          Label_EmployeeVisionError.Text = AD_Error;
          Label_EmployeeVisionDisplayName.Text = "";
          Label_EmployeeVisionEmployeeNumber.Text = "";
        }
      }
    }
    //---END--- --Employee Data--//


    //--START-- --Patient Data--//
    protected void Button_PatientEDW_Click(object sender, EventArgs e)
    {
      PatientEDW();
    }

    protected void Button_PatientEDWClear_Click(object sender, EventArgs e)
    {
      Label_PatientEDWFacilityInvalidMessage.Text = "";
      Label_PatientEDWVisitNumberInvalidMessage.Text = "";

      DropDownList_PatientEDWFacility.SelectedValue = "";
      TextBox_PatientEDWVisitNumber.Text = "";

      GridView_PatientEDWPatientInformation.DataSource = null;
      GridView_PatientEDWPatientInformation.DataBind();
      Label_PatientEDWPatientInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientEDWVisitInformation.DataSource = null;
      GridView_PatientEDWVisitInformation.DataBind();
      Label_PatientEDWVisitInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientEDWTheatreInformation.DataSource = null;
      GridView_PatientEDWTheatreInformation.DataBind();
      Label_PatientEDWTheatreInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientEDWIPSVisitInformation.DataSource = null;
      GridView_PatientEDWIPSVisitInformation.DataBind();
      Label_PatientEDWIPSVisitInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientEDWIPSTheatreInformation.DataSource = null;
      GridView_PatientEDWIPSTheatreInformation.DataBind();
      Label_PatientEDWIPSTheatreInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientEDWIPSCodingInformation.DataSource = null;
      GridView_PatientEDWIPSCodingInformation.DataBind();
      Label_PatientEDWIPSCodingInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientEDWIPSAccommodationInformation.DataSource = null;
      GridView_PatientEDWIPSAccommodationInformation.DataBind();
      Label_PatientEDWIPSAccommodationInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientEDWIPSAntibioticInformation.DataSource = null;
      GridView_PatientEDWIPSAntibioticInformation.DataBind();
      Label_PatientEDWIPSAntibioticInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
    }

    private void PatientEDW()
    {
      if (string.IsNullOrEmpty(DropDownList_PatientEDWFacility.SelectedValue) || string.IsNullOrEmpty(TextBox_PatientEDWVisitNumber.Text))
      {
        if (string.IsNullOrEmpty(DropDownList_PatientEDWFacility.SelectedValue))
        {
          Label_PatientEDWFacilityInvalidMessage.Text = Convert.ToString("Facility Required", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_PatientEDWFacilityInvalidMessage.Text = "";
        }

        if (string.IsNullOrEmpty(TextBox_PatientEDWVisitNumber.Text))
        {
          Label_PatientEDWVisitNumberInvalidMessage.Text = Convert.ToString("Visit Number Required", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_PatientEDWVisitNumberInvalidMessage.Text = "";
        }

        GridView_PatientEDWPatientInformation.DataSource = null;
        GridView_PatientEDWPatientInformation.DataBind();
        Label_PatientEDWPatientInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientEDWVisitInformation.DataSource = null;
        GridView_PatientEDWVisitInformation.DataBind();
        Label_PatientEDWVisitInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientEDWTheatreInformation.DataSource = null;
        GridView_PatientEDWTheatreInformation.DataBind();
        Label_PatientEDWTheatreInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientEDWIPSVisitInformation.DataSource = null;
        GridView_PatientEDWIPSVisitInformation.DataBind();
        Label_PatientEDWIPSVisitInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientEDWIPSTheatreInformation.DataSource = null;
        GridView_PatientEDWIPSTheatreInformation.DataBind();
        Label_PatientEDWIPSTheatreInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientEDWIPSCodingInformation.DataSource = null;
        GridView_PatientEDWIPSCodingInformation.DataBind();
        Label_PatientEDWIPSCodingInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientEDWIPSAccommodationInformation.DataSource = null;
        GridView_PatientEDWIPSAccommodationInformation.DataBind();
        Label_PatientEDWIPSAccommodationInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientEDWIPSAntibioticInformation.DataSource = null;
        GridView_PatientEDWIPSAntibioticInformation.DataBind();
        Label_PatientEDWIPSAntibioticInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_PatientEDWFacilityInvalidMessage.Text = "";
        Label_PatientEDWVisitNumberInvalidMessage.Text = "";

        PatientEDW_EDWPatientInformation();
        PatientEDW_EDWVisitInformation();
        PatientEDW_EDWTheatreInformation();
        PatientEDW_EDWIPSVisitInformation();
        PatientEDW_EDWIPSTheatreInformation();
        PatientEDW_EDWIPSCodingInformation();
        PatientEDW_EDWIPSAccommodationInformation();
        PatientEDW_EDWIPSAntibioticInformation();
      }
    }

    private void PatientEDW_EDWPatientInformation()
    {
      DataTable DataTable_PatientEDWPatientInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_PatientInformation(DropDownList_PatientEDWFacility.SelectedValue, TextBox_PatientEDWVisitNumber.Text);
      DataTable DataTable_PatientEDWPatientInformationClone = DataTable_PatientEDWPatientInformation.Clone();
      for (int a = 0; a < DataTable_PatientEDWPatientInformation.Columns.Count; a++)
      {
        DataTable_PatientEDWPatientInformationClone.Columns[a].DataType = typeof(string);
      }

      foreach (DataRow row in DataTable_PatientEDWPatientInformation.Rows)
      {
        DataTable_PatientEDWPatientInformationClone.ImportRow(row);
      }

      GridView_PatientEDWPatientInformation.DataSource = DataTable_PatientEDWPatientInformationClone;
      GridView_PatientEDWPatientInformation.DataBind();
      Label_PatientEDWPatientInformationTotalRecords.Text = DataTable_PatientEDWPatientInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    private void PatientEDW_EDWVisitInformation()
    {
      DataTable DataTable_PatientEDWVisitInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(DropDownList_PatientEDWFacility.SelectedValue, TextBox_PatientEDWVisitNumber.Text);
      DataTable DataTable_PatientEDWVisitInformationClone = DataTable_PatientEDWVisitInformation.Clone();
      for (int a = 0; a < DataTable_PatientEDWVisitInformation.Columns.Count; a++)
      {
        DataTable_PatientEDWVisitInformationClone.Columns[a].DataType = typeof(string);
      }

      foreach (DataRow row in DataTable_PatientEDWVisitInformation.Rows)
      {
        DataTable_PatientEDWVisitInformationClone.ImportRow(row);
      }

      GridView_PatientEDWVisitInformation.DataSource = DataTable_PatientEDWVisitInformationClone;
      GridView_PatientEDWVisitInformation.DataBind();
      Label_PatientEDWVisitInformationTotalRecords.Text = DataTable_PatientEDWVisitInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    private void PatientEDW_EDWTheatreInformation()
    {
      DataTable DataTable_PatientEDWTheatreInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_TheatreInformation(DropDownList_PatientEDWFacility.SelectedValue, TextBox_PatientEDWVisitNumber.Text);
      DataTable DataTable_PatientEDWTheatreInformationClone = DataTable_PatientEDWTheatreInformation.Clone();
      for (int a = 0; a < DataTable_PatientEDWTheatreInformation.Columns.Count; a++)
      {
        DataTable_PatientEDWTheatreInformationClone.Columns[a].DataType = typeof(string);
      }

      foreach (DataRow row in DataTable_PatientEDWTheatreInformation.Rows)
      {
        DataTable_PatientEDWTheatreInformationClone.ImportRow(row);
      }

      GridView_PatientEDWTheatreInformation.DataSource = DataTable_PatientEDWTheatreInformationClone;
      GridView_PatientEDWTheatreInformation.DataBind();
      Label_PatientEDWTheatreInformationTotalRecords.Text = DataTable_PatientEDWTheatreInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    private void PatientEDW_EDWIPSVisitInformation()
    {
      DataTable DataTable_PatientEDWIPSVisitInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_VisitInformation(DropDownList_PatientEDWFacility.SelectedValue, TextBox_PatientEDWVisitNumber.Text);
      DataTable DataTable_PatientEDWIPSVisitInformationClone = DataTable_PatientEDWIPSVisitInformation.Clone();
      for (int a = 0; a < DataTable_PatientEDWIPSVisitInformation.Columns.Count; a++)
      {
        DataTable_PatientEDWIPSVisitInformationClone.Columns[a].DataType = typeof(string);
      }

      foreach (DataRow row in DataTable_PatientEDWIPSVisitInformation.Rows)
      {
        DataTable_PatientEDWIPSVisitInformationClone.ImportRow(row);
      }

      GridView_PatientEDWIPSVisitInformation.DataSource = DataTable_PatientEDWIPSVisitInformationClone;
      GridView_PatientEDWIPSVisitInformation.DataBind();
      Label_PatientEDWIPSVisitInformationTotalRecords.Text = DataTable_PatientEDWIPSVisitInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    private void PatientEDW_EDWIPSTheatreInformation()
    {
      DataTable DataTable_PatientEDWIPSTheatreInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_TheatreInformation(DropDownList_PatientEDWFacility.SelectedValue, TextBox_PatientEDWVisitNumber.Text);
      DataTable DataTable_PatientEDWIPSTheatreInformationClone = DataTable_PatientEDWIPSTheatreInformation.Clone();
      for (int a = 0; a < DataTable_PatientEDWIPSTheatreInformation.Columns.Count; a++)
      {
        DataTable_PatientEDWIPSTheatreInformationClone.Columns[a].DataType = typeof(string);
      }

      foreach (DataRow row in DataTable_PatientEDWIPSTheatreInformation.Rows)
      {
        DataTable_PatientEDWIPSTheatreInformationClone.ImportRow(row);
      }

      GridView_PatientEDWIPSTheatreInformation.DataSource = DataTable_PatientEDWIPSTheatreInformationClone;
      GridView_PatientEDWIPSTheatreInformation.DataBind();
      Label_PatientEDWIPSTheatreInformationTotalRecords.Text = DataTable_PatientEDWIPSTheatreInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    private void PatientEDW_EDWIPSCodingInformation()
    {
      DataTable DataTable_PatientEDWIPSCodingInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_CodingInformation(DropDownList_PatientEDWFacility.SelectedValue, TextBox_PatientEDWVisitNumber.Text);
      DataTable DataTable_PatientEDWIPSCodingInformationClone = DataTable_PatientEDWIPSCodingInformation.Clone();
      for (int a = 0; a < DataTable_PatientEDWIPSCodingInformation.Columns.Count; a++)
      {
        DataTable_PatientEDWIPSCodingInformationClone.Columns[a].DataType = typeof(string);
      }

      foreach (DataRow row in DataTable_PatientEDWIPSCodingInformation.Rows)
      {
        DataTable_PatientEDWIPSCodingInformationClone.ImportRow(row);
      }

      GridView_PatientEDWIPSCodingInformation.DataSource = DataTable_PatientEDWIPSCodingInformationClone;
      GridView_PatientEDWIPSCodingInformation.DataBind();
      Label_PatientEDWIPSCodingInformationTotalRecords.Text = DataTable_PatientEDWIPSCodingInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    private void PatientEDW_EDWIPSAccommodationInformation()
    {
      DataTable DataTable_PatientEDWIPSAccommodationInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_AccommodationInformation(DropDownList_PatientEDWFacility.SelectedValue, TextBox_PatientEDWVisitNumber.Text);
      DataTable DataTable_PatientEDWIPSAccommodationInformationClone = DataTable_PatientEDWIPSAccommodationInformation.Clone();
      for (int a = 0; a < DataTable_PatientEDWIPSAccommodationInformation.Columns.Count; a++)
      {
        DataTable_PatientEDWIPSAccommodationInformationClone.Columns[a].DataType = typeof(string);
      }

      foreach (DataRow row in DataTable_PatientEDWIPSAccommodationInformation.Rows)
      {
        DataTable_PatientEDWIPSAccommodationInformationClone.ImportRow(row);
      }

      GridView_PatientEDWIPSAccommodationInformation.DataSource = DataTable_PatientEDWIPSAccommodationInformationClone;
      GridView_PatientEDWIPSAccommodationInformation.DataBind();
      Label_PatientEDWIPSAccommodationInformationTotalRecords.Text = DataTable_PatientEDWIPSAccommodationInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    private void PatientEDW_EDWIPSAntibioticInformation()
    {
      DataTable DataTable_PatientEDWIPSAntibioticInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_IPS_AntibioticInformation(DropDownList_PatientEDWFacility.SelectedValue, TextBox_PatientEDWVisitNumber.Text);
      DataTable DataTable_PatientEDWIPSAntibioticInformationClone = DataTable_PatientEDWIPSAntibioticInformation.Clone();
      for (int a = 0; a < DataTable_PatientEDWIPSAntibioticInformation.Columns.Count; a++)
      {
        DataTable_PatientEDWIPSAntibioticInformationClone.Columns[a].DataType = typeof(string);
      }

      foreach (DataRow row in DataTable_PatientEDWIPSAntibioticInformation.Rows)
      {
        DataTable_PatientEDWIPSAntibioticInformationClone.ImportRow(row);
      }

      GridView_PatientEDWIPSAntibioticInformation.DataSource = DataTable_PatientEDWIPSAntibioticInformationClone;
      GridView_PatientEDWIPSAntibioticInformation.DataBind();
      Label_PatientEDWIPSAntibioticInformationTotalRecords.Text = DataTable_PatientEDWIPSAntibioticInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
    }

    protected void GridView_PatientEDWPatientInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientEDWVisitInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientEDWTheatreInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientEDWIPSVisitInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientEDWIPSTheatreInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientEDWIPSCodingInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientEDWIPSAccommodationInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientEDWIPSAntibioticInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }


    protected void Button_PatientODS_Click(object sender, EventArgs e)
    {
      PatientODS();
    }

    protected void Button_PatientODSClear_Click(object sender, EventArgs e)
    {
      Label_PatientODSFacilityInvalidMessage.Text = "";
      Label_PatientODSVisitNumberInvalidMessage.Text = "";

      DropDownList_PatientODSFacility.SelectedValue = "";
      TextBox_PatientODSVisitNumber.Text = "";

      GridView_PatientODSPatientInformation.DataSource = null;
      GridView_PatientODSPatientInformation.DataBind();
      Label_PatientODSPatientInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientODSVisitInformation.DataSource = null;
      GridView_PatientODSVisitInformation.DataBind();
      Label_PatientODSVisitInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientODSCodingInformation.DataSource = null;
      GridView_PatientODSCodingInformation.DataBind();
      Label_PatientODSCodingInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientODSAccommodationInformation.DataSource = null;
      GridView_PatientODSAccommodationInformation.DataBind();
      Label_PatientODSAccommodationInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

      GridView_PatientODSPractitionerInformation.DataSource = null;
      GridView_PatientODSPractitionerInformation.DataBind();
      Label_PatientODSPractitionerInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
    }

    private void PatientODS()
    {
      if (string.IsNullOrEmpty(DropDownList_PatientODSFacility.SelectedValue) || string.IsNullOrEmpty(TextBox_PatientODSVisitNumber.Text))
      {
        if (string.IsNullOrEmpty(DropDownList_PatientODSFacility.SelectedValue))
        {
          Label_PatientODSFacilityInvalidMessage.Text = Convert.ToString("Facility Required", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_PatientODSFacilityInvalidMessage.Text = "";
        }

        if (string.IsNullOrEmpty(TextBox_PatientODSVisitNumber.Text))
        {
          Label_PatientODSVisitNumberInvalidMessage.Text = Convert.ToString("Visit Number Required", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_PatientODSVisitNumberInvalidMessage.Text = "";
        }

        GridView_PatientODSPatientInformation.DataSource = null;
        GridView_PatientODSPatientInformation.DataBind();
        Label_PatientODSPatientInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientODSVisitInformation.DataSource = null;
        GridView_PatientODSVisitInformation.DataBind();
        Label_PatientODSVisitInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientODSCodingInformation.DataSource = null;
        GridView_PatientODSCodingInformation.DataBind();
        Label_PatientODSCodingInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientODSAccommodationInformation.DataSource = null;
        GridView_PatientODSAccommodationInformation.DataBind();
        Label_PatientODSAccommodationInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);

        GridView_PatientODSPractitionerInformation.DataSource = null;
        GridView_PatientODSPractitionerInformation.DataBind();
        Label_PatientODSPractitionerInformationTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_PatientODSFacilityInvalidMessage.Text = "";
        Label_PatientODSVisitNumberInvalidMessage.Text = "";


        //--START-- --ODSPatientInformation--//
        DataTable DataTable_PatientODSPatientInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PatientInformation(DropDownList_PatientODSFacility.SelectedValue, TextBox_PatientODSVisitNumber.Text);
        DataTable DataTable_PatientODSPatientInformationClone = DataTable_PatientODSPatientInformation.Clone();
        for (int a = 0; a < DataTable_PatientODSPatientInformation.Columns.Count; a++)
        {
          DataTable_PatientODSPatientInformationClone.Columns[a].DataType = typeof(string);
        }

        foreach (DataRow row in DataTable_PatientODSPatientInformation.Rows)
        {
          DataTable_PatientODSPatientInformationClone.ImportRow(row);
        }

        GridView_PatientODSPatientInformation.DataSource = DataTable_PatientODSPatientInformationClone;
        GridView_PatientODSPatientInformation.DataBind();
        Label_PatientODSPatientInformationTotalRecords.Text = DataTable_PatientODSPatientInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
        //---END--- --ODSPatientInformation--//


        //--START-- --ODSVisitInformation--//
        DataTable DataTable_PatientODSVisitInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(DropDownList_PatientODSFacility.SelectedValue, TextBox_PatientODSVisitNumber.Text);
        DataTable DataTable_PatientODSVisitInformationClone = DataTable_PatientODSVisitInformation.Clone();
        for (int a = 0; a < DataTable_PatientODSVisitInformation.Columns.Count; a++)
        {
          DataTable_PatientODSVisitInformationClone.Columns[a].DataType = typeof(string);
        }

        foreach (DataRow row in DataTable_PatientODSVisitInformation.Rows)
        {
          DataTable_PatientODSVisitInformationClone.ImportRow(row);
        }

        GridView_PatientODSVisitInformation.DataSource = DataTable_PatientODSVisitInformationClone;
        GridView_PatientODSVisitInformation.DataBind();
        Label_PatientODSVisitInformationTotalRecords.Text = DataTable_PatientODSVisitInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
        //---END--- --ODSVisitInformation--//


        //--START-- --ODSCodingInformation--//
        DataTable DataTable_PatientODSCodingInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_CodingInformation(DropDownList_PatientODSFacility.SelectedValue, TextBox_PatientODSVisitNumber.Text);
        DataTable DataTable_PatientODSCodingInformationClone = DataTable_PatientODSCodingInformation.Clone();
        for (int a = 0; a < DataTable_PatientODSCodingInformation.Columns.Count; a++)
        {
          DataTable_PatientODSCodingInformationClone.Columns[a].DataType = typeof(string);
        }

        foreach (DataRow row in DataTable_PatientODSCodingInformation.Rows)
        {
          DataTable_PatientODSCodingInformationClone.ImportRow(row);
        }

        GridView_PatientODSCodingInformation.DataSource = DataTable_PatientODSCodingInformationClone;
        GridView_PatientODSCodingInformation.DataBind();
        Label_PatientODSCodingInformationTotalRecords.Text = DataTable_PatientODSCodingInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
        //---END--- --ODSCodingInformation--//


        //--START-- --ODSAccommodationInformation--//
        DataTable DataTable_PatientODSAccommodationInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_AccommodationInformation(DropDownList_PatientODSFacility.SelectedValue, TextBox_PatientODSVisitNumber.Text);
        DataTable DataTable_PatientODSAccommodationInformationClone = DataTable_PatientODSAccommodationInformation.Clone();
        for (int a = 0; a < DataTable_PatientODSAccommodationInformation.Columns.Count; a++)
        {
          DataTable_PatientODSAccommodationInformationClone.Columns[a].DataType = typeof(string);
        }

        foreach (DataRow row in DataTable_PatientODSAccommodationInformation.Rows)
        {
          DataTable_PatientODSAccommodationInformationClone.ImportRow(row);
        }

        GridView_PatientODSAccommodationInformation.DataSource = DataTable_PatientODSAccommodationInformationClone;
        GridView_PatientODSAccommodationInformation.DataBind();
        Label_PatientODSAccommodationInformationTotalRecords.Text = DataTable_PatientODSAccommodationInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
        //---END--- --ODSAccommodationInformation--//


        //--START-- --ODSPractitionerInformation--//
        DataTable DataTable_PatientODSPractitionerInformation = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PractitionerInformation(DropDownList_PatientODSFacility.SelectedValue, TextBox_PatientODSVisitNumber.Text);
        DataTable DataTable_PatientODSPractitionerInformationClone = DataTable_PatientODSPractitionerInformation.Clone();
        for (int a = 0; a < DataTable_PatientODSPractitionerInformation.Columns.Count; a++)
        {
          DataTable_PatientODSPractitionerInformationClone.Columns[a].DataType = typeof(string);
        }

        foreach (DataRow row in DataTable_PatientODSPractitionerInformation.Rows)
        {
          DataTable_PatientODSPractitionerInformationClone.ImportRow(row);
        }

        GridView_PatientODSPractitionerInformation.DataSource = DataTable_PatientODSPractitionerInformationClone;
        GridView_PatientODSPractitionerInformation.DataBind();
        Label_PatientODSPractitionerInformationTotalRecords.Text = DataTable_PatientODSPractitionerInformationClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
        //---END--- --ODSPractitionerInformation--//
      }
    }

    protected void GridView_PatientODSPatientInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientODSVisitInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientODSCodingInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientODSAccommodationInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void GridView_PatientODSPractitionerInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void Button_PatientODSPXMPostDischargeSurvey_Click(object sender, EventArgs e)
    {
      PatientODSPXMPostDischargeSurvey();
    }

    protected void Button_PatientODSPXMPostDischargeSurveyClear_Click(object sender, EventArgs e)
    {
      Label_PatientODSPXMPostDischargeSurveyFacilityInvalidMessage.Text = "";
      Label_PatientODSPXMPostDischargeSurveyStartDateInvalidMessage.Text = "";
      Label_PatientODSPXMPostDischargeSurveyEndDateInvalidMessage.Text = "";

      DropDownList_PatientODSPXMPostDischargeSurveyFacility.SelectedValue = "";
      TextBox_PatientODSPXMPostDischargeSurveyStartDate.Text = "";
      TextBox_PatientODSPXMPostDischargeSurveyEndDate.Text = "";

      GridView_PatientODSPXMPostDischargeSurvey.DataSource = null;
      GridView_PatientODSPXMPostDischargeSurvey.DataBind();
      Label_PatientODSPXMPostDischargeSurveyTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
    }

    private void PatientODSPXMPostDischargeSurvey()
    {
      if (string.IsNullOrEmpty(DropDownList_PatientODSPXMPostDischargeSurveyFacility.SelectedValue) || string.IsNullOrEmpty(TextBox_PatientODSPXMPostDischargeSurveyStartDate.Text) || string.IsNullOrEmpty(TextBox_PatientODSPXMPostDischargeSurveyEndDate.Text))
      {
        if (string.IsNullOrEmpty(DropDownList_PatientODSPXMPostDischargeSurveyFacility.SelectedValue))
        {
          Label_PatientODSPXMPostDischargeSurveyFacilityInvalidMessage.Text = Convert.ToString("Facility Required", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_PatientODSPXMPostDischargeSurveyFacilityInvalidMessage.Text = "";
        }

        if (string.IsNullOrEmpty(TextBox_PatientODSPXMPostDischargeSurveyStartDate.Text))
        {
          Label_PatientODSPXMPostDischargeSurveyStartDateInvalidMessage.Text = Convert.ToString("Start Date Required", CultureInfo.CurrentCulture);
        }
        else
        {
          string DateToValidatePatientODSStartDate = TextBox_PatientODSPXMPostDischargeSurveyStartDate.Text.ToString();
          DateTime ValidatedDatePatientODSStartDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidatePatientODSStartDate);

          if (ValidatedDatePatientODSStartDate.ToString() == "0001/01/01 12:00:00 AM")
          {
            Label_PatientODSPXMPostDischargeSurveyStartDateInvalidMessage.Text = Convert.ToString("Start Date is not in the correct format, date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
          }
          else
          {
            Label_PatientODSPXMPostDischargeSurveyStartDateInvalidMessage.Text = "";
          }
        }

        if (string.IsNullOrEmpty(TextBox_PatientODSPXMPostDischargeSurveyEndDate.Text))
        {
          Label_PatientODSPXMPostDischargeSurveyEndDateInvalidMessage.Text = Convert.ToString("End Date Required", CultureInfo.CurrentCulture);
        }
        else
        {
          string DateToValidatePatientODSEndDate = TextBox_PatientODSPXMPostDischargeSurveyEndDate.Text.ToString();
          DateTime ValidatedDatePatientODSEndDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidatePatientODSEndDate);

          if (ValidatedDatePatientODSEndDate.ToString() == "0001/01/01 12:00:00 AM")
          {
            Label_PatientODSPXMPostDischargeSurveyEndDateInvalidMessage.Text = Convert.ToString("End Date is not in the correct format, date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
          }
          else
          {
            Label_PatientODSPXMPostDischargeSurveyEndDateInvalidMessage.Text = "";
          }
        }

        GridView_PatientODSPXMPostDischargeSurvey.DataSource = null;
        GridView_PatientODSPXMPostDischargeSurvey.DataBind();
        Label_PatientODSPXMPostDischargeSurveyTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_PatientODSPXMPostDischargeSurveyFacilityInvalidMessage.Text = "";


        //--START-- --ODSPXMPostDischargeSurvey--//
        string DateToValidatePatientODSStartDate = TextBox_PatientODSPXMPostDischargeSurveyStartDate.Text.ToString();
        DateTime ValidatedDatePatientODSStartDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidatePatientODSStartDate);

        if (ValidatedDatePatientODSStartDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          Label_PatientODSPXMPostDischargeSurveyStartDateInvalidMessage.Text = Convert.ToString("Start Date is not in the correct format, date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_PatientODSPXMPostDischargeSurveyStartDateInvalidMessage.Text = "";
        }

        string DateToValidatePatientODSEndDate = TextBox_PatientODSPXMPostDischargeSurveyEndDate.Text.ToString();
        DateTime ValidatedDatePatientODSEndDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidatePatientODSEndDate);

        if (ValidatedDatePatientODSEndDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          Label_PatientODSPXMPostDischargeSurveyEndDateInvalidMessage.Text = Convert.ToString("End Date is not in the correct format, date must be in the format yyyy/mm/dd<br />", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_PatientODSPXMPostDischargeSurveyEndDateInvalidMessage.Text = "";
        }


        if (string.IsNullOrEmpty(Label_PatientODSPXMPostDischargeSurveyStartDateInvalidMessage.Text) && string.IsNullOrEmpty(Label_PatientODSPXMPostDischargeSurveyEndDateInvalidMessage.Text))
        {
          string ODSPXMPostDischargeSurveyFacility = "";

          if (DropDownList_PatientODSPXMPostDischargeSurveyFacility.SelectedValue == "All")
          {
            ODSPXMPostDischargeSurveyFacility = "";
          }
          else
          {
            ODSPXMPostDischargeSurveyFacility = DropDownList_PatientODSPXMPostDischargeSurveyFacility.SelectedValue;
          }

          DataTable DataTable_PatientODSPXMPostDischargeSurvey = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PXM_PostDischargeSurvey(Convert.ToDateTime(TextBox_PatientODSPXMPostDischargeSurveyStartDate.Text, CultureInfo.CurrentCulture), Convert.ToDateTime(TextBox_PatientODSPXMPostDischargeSurveyEndDate.Text, CultureInfo.CurrentCulture), ODSPXMPostDischargeSurveyFacility);
          DataTable DataTable_PatientODSPXMPostDischargeSurveyClone = DataTable_PatientODSPXMPostDischargeSurvey.Clone();
          for (int a = 0; a < DataTable_PatientODSPXMPostDischargeSurvey.Columns.Count; a++)
          {
            DataTable_PatientODSPXMPostDischargeSurveyClone.Columns[a].DataType = typeof(string);
          }

          foreach (DataRow row in DataTable_PatientODSPXMPostDischargeSurvey.Rows)
          {
            DataTable_PatientODSPXMPostDischargeSurveyClone.ImportRow(row);
          }

          GridView_PatientODSPXMPostDischargeSurvey.DataSource = DataTable_PatientODSPXMPostDischargeSurveyClone;
          GridView_PatientODSPXMPostDischargeSurvey.DataBind();
          Label_PatientODSPXMPostDischargeSurveyTotalRecords.Text = DataTable_PatientODSPXMPostDischargeSurveyClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
        }
        //---END--- --ODSPXMPostDischargeSurvey--//
      }
    }

    protected void GridView_PatientODSPXMPostDischargeSurvey_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }

    protected void Button_PatientODSPatientSearch_Click(object sender, EventArgs e)
    {
      PatientODSPatientSearch();
    }

    protected void Button_PatientODSPatientSearchClear_Click(object sender, EventArgs e)
    {
      Label_PatientODSPatientSearchFacilityInvalidMessage.Text = "";
      Label_PatientODSPatientSearchPatientInvalidMessage.Text = "";

      DropDownList_PatientODSPatientSearchFacility.SelectedValue = "";
      TextBox_PatientODSPatientSearchPatient.Text = "";

      GridView_PatientODSPatientSearch.DataSource = null;
      GridView_PatientODSPatientSearch.DataBind();
      Label_PatientODSPatientSearchTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
    }

    private void PatientODSPatientSearch()
    {
      if (string.IsNullOrEmpty(DropDownList_PatientODSPatientSearchFacility.SelectedValue) || string.IsNullOrEmpty(TextBox_PatientODSPatientSearchPatient.Text))
      {
        if (string.IsNullOrEmpty(DropDownList_PatientODSPatientSearchFacility.SelectedValue))
        {
          Label_PatientODSPatientSearchFacilityInvalidMessage.Text = Convert.ToString("Facility Required", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_PatientODSPatientSearchFacilityInvalidMessage.Text = "";
        }

        if (string.IsNullOrEmpty(TextBox_PatientODSPatientSearchPatient.Text))
        {
          Label_PatientODSPatientSearchPatientInvalidMessage.Text = Convert.ToString("Patient Required", CultureInfo.CurrentCulture);
        }
        else
        {
          Label_PatientODSPatientSearchPatientInvalidMessage.Text = "";
        }

        GridView_PatientODSPatientSearch.DataSource = null;
        GridView_PatientODSPatientSearch.DataBind();
        Label_PatientODSPatientSearchTotalRecords.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
      }
      else
      {
        Label_PatientODSPatientSearchFacilityInvalidMessage.Text = "";
        Label_PatientODSPatientSearchPatientInvalidMessage.Text = "";

        //--START-- --ODSPatientSearch--//
        string ODSPatientSearchFacility = "";

        if (DropDownList_PatientODSPatientSearchFacility.SelectedValue == "All")
        {
          ODSPatientSearchFacility = "";
        }
        else
        {
          ODSPatientSearchFacility = DropDownList_PatientODSPatientSearchFacility.SelectedValue;
        }

        DataTable DataTable_PatientODSPatientSearch = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_PatientSearch(ODSPatientSearchFacility, TextBox_PatientODSPatientSearchPatient.Text);
        DataTable DataTable_PatientODSPatientSearchClone = DataTable_PatientODSPatientSearch.Clone();
        for (int a = 0; a < DataTable_PatientODSPatientSearch.Columns.Count; a++)
        {
          DataTable_PatientODSPatientSearchClone.Columns[a].DataType = typeof(string);
        }

        foreach (DataRow row in DataTable_PatientODSPatientSearch.Rows)
        {
          DataTable_PatientODSPatientSearchClone.ImportRow(row);
        }

        GridView_PatientODSPatientSearch.DataSource = DataTable_PatientODSPatientSearchClone;
        GridView_PatientODSPatientSearch.DataBind();
        Label_PatientODSPatientSearchTotalRecords.Text = DataTable_PatientODSPatientSearchClone.Rows.Count.ToString(CultureInfo.CurrentCulture);
        //---END--- --ODSPatientSearch--//
      }
    }

    protected void GridView_PatientODSPatientSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e != null)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          for (int a = 0; a < e.Row.Controls.Count; a++)
          {
            e.Row.Cells[a].HorizontalAlign = HorizontalAlign.Left;
          }
        }
      }
    }
    //---END--- --Patient Data--//


    //--START-- --Email--//
    protected void Button_Email_Click(object sender, EventArgs e)
    {
      Email();
    }

    protected void Button_EmailClear_Click(object sender, EventArgs e)
    {
      TextBox_Email.Text = "";
      TextBox_EmailDescription.Text = "";

      Label_EmailError.Text = "";
      Label_EmailAddress.Text = "";
      Label_EmailDescription.Text = "";
      Label_EmailSuccess.Text = "";
    }

    private void Email()
    {
      if (string.IsNullOrEmpty(TextBox_Email.Text))
      {
        Label_EmailError.Text = Convert.ToString("Email Required", CultureInfo.CurrentCulture);

        Label_EmailAddress.Text = "";
        Label_EmailDescription.Text = "";
        Label_EmailSuccess.Text = "";
      }
      else
      {
        Label_EmailAddress.Text = TextBox_Email.Text;
        Label_EmailDescription.Text = TextBox_EmailDescription.Text;

        string From = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za";
        string To = TextBox_Email.Text;
        To = To.Replace(";", ",");
        To = To.Replace(":", ",");
        string Subject = "Disaster Recovery Test";
        string BodyString = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("64");
        BodyString = BodyString.Replace(";replace;To;replace;", "" + To + "");
        BodyString = BodyString.Replace(";replace;DateTime;replace;", "" + DateTime.Now + "");
        BodyString = BodyString.Replace(";replace;Server;replace;", "" + Dns.GetHostEntry(Environment.MachineName).HostName.ToString().ToLower(CultureInfo.CurrentCulture) + "");
        BodyString = BodyString.Replace(";replace;Discription;replace;", "" + TextBox_EmailDescription.Text.ToString() + "");
        string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
        string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
        string Body = HeaderString + BodyString + FooterString;

        using (MailMessage MailMessage_Email = new MailMessage())
        {
          using (System.Net.Mail.SmtpClient SmtpClient_SmtpServer = new System.Net.Mail.SmtpClient(InfoQuestWCF.InfoQuest_All.All_SystemServer("1").ToString()))
          {
            try
            {
              MailMessage_Email.From = new MailAddress("" + From + "");
              MailMessage_Email.To.Add("" + To + "");
              MailMessage_Email.Subject = "" + Subject + "";
              MailMessage_Email.Body = "" + Body + "";
              MailMessage_Email.IsBodyHtml = true;

              SmtpClient_SmtpServer.Send(MailMessage_Email);
              Label_EmailError.Text = Convert.ToString("No Error", CultureInfo.CurrentCulture);
              Label_EmailSuccess.Text = Convert.ToString("Yes", CultureInfo.CurrentCulture);
            }
            catch (Exception Exception_Error)
            {
              if (!string.IsNullOrEmpty(Exception_Error.ToString()))
              {
                Label_EmailError.Text = Exception_Error.ToString();
                Label_EmailSuccess.Text = Convert.ToString("No", CultureInfo.CurrentCulture);
              }
              else
              {
                throw;
              }
            }
          }
        }
      }
    }
    //---END--- --Email--//


    //--START-- --File--//
    protected void Button_FileUpload_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_DisasterRecovery.SetFocus(LinkButton_File);

      string UploadMessage = "";

      if (FileUpload_FileUpload.HasFile)
      {
        string FileName = Path.GetFileName(FileUpload_FileUpload.FileName);
        string FileExtension = Path.GetExtension(FileName);
        FileExtension = FileExtension.ToLower(CultureInfo.CurrentCulture);
        string FileContentTypeValue = FileContentType(FileExtension);
        decimal FileSize = FileUpload_FileUpload.PostedFile.ContentLength;
        decimal FileSizeMB = (FileSize / 1024 / 1024);
        string FileSizeMBString = FileSizeMB.ToString("N2", CultureInfo.CurrentCulture);

        if ((!string.IsNullOrEmpty(FileContentTypeValue)) && (FileSize < 5242880))
        {
          try
          {
            if (!Directory.Exists(UploadPath() + UploadUserFolder()))
            {
              Directory.CreateDirectory(UploadPath() + UploadUserFolder());
            }

            if (File.Exists(UploadPath() + UploadUserFolder() + "\\" + FileName))
            {
              UploadMessage = "<span style='color: #d46e6e'>File Uploading Failed<br/>File already uploaded<br/>File Name: " + FileName + "</span>";
            }
            else
            {
              FileUpload_FileUpload.SaveAs(UploadPath() + UploadUserFolder() + "\\" + FileName);

              UploadMessage = "<span style='color: #77cf9c'>File Uploading Successful</span>";
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              UploadMessage = "<span style='color: #d46e6e'>File Uploading Failed</span>";
            }
            else
            {
              throw;
            }
          }
        }
        else
        {
          if (string.IsNullOrEmpty(FileContentTypeValue))
          {
            UploadMessage = UploadMessage + "<span style='color: #d46e6e'>File Uploading Failed<br/>Only doc, docx, xls, xlsx, pdf, tif, tiff, txt, msg, jpeg, jpg, gif and png files can be uploaded<br/>File Name: " + FileName + "</span>";
          }

          if (FileSize > 5242880)
          {
            UploadMessage = UploadMessage + "<span style='color: #d46e6e'>File Uploading Failed<br/>Only files smaller then 5 MB can be uploaded<br/>File Name: " + FileName + "<br/>File Size: " + FileSizeMBString + " MB</span>";
          }
        }
      }
      else
      {
        UploadMessage = UploadMessage + "<span style='color: #d46e6e'>File Uploading Failed<br/>No file chosen</span>";
      }

      Label_FileUpload.Text = Convert.ToString(UploadMessage, CultureInfo.CurrentCulture);

      ShowHide("Upload");
      UploadedFiles();
    }

    protected void Button_FileDeleteAll_Click(object sender, EventArgs e)
    {
      ToolkitScriptManager_DisasterRecovery.SetFocus(LinkButton_File);

      string DeleteMessage = "";

      for (int i = 0; i < BulletedList_FileUploadedFiles.Items.Count; i++)
      {
        string UploadedFilesCheckBoxListValues = BulletedList_FileUploadedFiles.Items[i].Text;

        if (!string.IsNullOrEmpty(UploadedFilesCheckBoxListValues))
        {
          try
          {
            if (Directory.Exists(UploadPath()))
            {
              if (File.Exists(UploadPath() + UploadedFilesCheckBoxListValues))
              {
                File.Delete(UploadPath() + UploadedFilesCheckBoxListValues);
                DirectoryCleanUp();

                DeleteMessage = DeleteMessage + "<span style='color: #77cf9c'>File Deletion Successful<br/>File Name: " + UploadedFilesCheckBoxListValues + "<br/><br/></span>";
              }
            }
          }
          catch (Exception Exception_Error)
          {
            if (!string.IsNullOrEmpty(Exception_Error.ToString()))
            {
              DeleteMessage = DeleteMessage + "<span style='color: #d46e6e'>File Deletion Failed<br/>File Name: " + UploadedFilesCheckBoxListValues + "<br/><br/></span>";
            }
            else
            {
              throw;
            }
          }
        }
      }

      Label_FileDelete.Text = Convert.ToString(DeleteMessage, CultureInfo.CurrentCulture);

      ShowHide("Delete");
      UploadedFiles();
    }

    protected void RegisterPostBackControl()
    {
      ScriptManager ScriptManager_Upload = ScriptManager.GetCurrent(Page);

      ScriptManager_Upload.RegisterPostBackControl(Button_FileUpload);
      ScriptManager_Upload.RegisterPostBackControl(Button_FileDeleteAll);
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

    private string UploadPath()
    {
      string UploadPath = Server.MapPath("App_Files/InfoQuest_DisasterRecovery/");

      return UploadPath;
    }

    private string UploadUserFolder()
    {
      string UserFolder = Request.ServerVariables["LOGON_USER"];
      UserFolder = UserFolder.Substring(UserFolder.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
      UserFolder = UserFolder.ToLower(CultureInfo.CurrentCulture);

      return UserFolder;
    }

    private void DirectoryCleanUp()
    {
      if (Directory.Exists(UploadPath() + UploadUserFolder()))
      {
        string[] UploadedFiles = Directory.GetFiles(UploadPath() + UploadUserFolder(), "*.*", SearchOption.AllDirectories);

        if (UploadedFiles.Length == 0)
        {
          Directory.Delete(UploadPath() + UploadUserFolder(), true);
        }
      }
    }

    private void UploadedFiles()
    {
      RegisterPostBackControl();

      string ShowHideButtons = "1";

      try
      {
        if (Directory.Exists(UploadPath()))
        {
          string[] UploadedFiles = Directory.GetFiles(UploadPath(), "*.*", SearchOption.AllDirectories);

          if (UploadedFiles.Length > 0)
          {
            BulletedList_FileUploadedFiles.Items.Clear();
            Label_TotalFiles.Text = "" + UploadedFiles.Length + "";
            Label_FileUploadedFiles.Text = "";

            foreach (string Files in Directory.GetFiles(UploadPath(), "*.*", SearchOption.AllDirectories))
            {
              string FileName = Files.Substring(Files.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
              if (UploadedFiles.Length == 1 && FileName == "placeholder.txt")
              {
                ShowHideButtons = "0";

                BulletedList_FileUploadedFiles.Items.Clear();
                Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
                Label_FileUploadedFiles.Text = Convert.ToString("No Uploaded Files", CultureInfo.CurrentCulture);
              }
              else if (UploadedFiles.Length > 1 && FileName == "placeholder.txt")
              {
                Int32 TotalFiles = UploadedFiles.Length;
                TotalFiles = TotalFiles - 1;
                Label_TotalFiles.Text = TotalFiles.ToString(CultureInfo.CurrentCulture);
              }
              else
              {
                string FileFolder = Files.Replace("\\" + FileName + "", "");
                FileFolder = FileFolder.Substring(FileFolder.LastIndexOf(@"\", StringComparison.CurrentCulture) + 1);
                BulletedList_FileUploadedFiles.Items.Add((new ListItem(Convert.ToString(FileFolder + "\\" + FileName, CultureInfo.CurrentCulture), "App_Files\\InfoQuest_DisasterRecovery\\" + FileFolder + "\\" + FileName + "")));
              }
            }
          }
          else
          {
            ShowHideButtons = "0";

            BulletedList_FileUploadedFiles.Items.Clear();
            Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
            Label_FileUploadedFiles.Text = Convert.ToString("No Uploaded Files", CultureInfo.CurrentCulture);
          }
        }
        else
        {
          ShowHideButtons = "0";

          BulletedList_FileUploadedFiles.Items.Clear();
          Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          Label_FileUploadedFiles.Text = Convert.ToString("No Uploaded Files", CultureInfo.CurrentCulture);
        }
      }
      catch (Exception Exception_Error)
      {
        if (!string.IsNullOrEmpty(Exception_Error.ToString()))
        {
          ShowHideButtons = "0";
          BulletedList_FileUploadedFiles.Items.Clear();
          Label_TotalFiles.Text = Convert.ToString("0", CultureInfo.CurrentCulture);
          Label_FileUploadedFiles.Text = Convert.ToString("Error Accessing Folders and Files", CultureInfo.CurrentCulture);
        }
        else
        {
          throw;
        }
      }

      if (ShowHideButtons == "1")
      {
        Button_FileDeleteAll.Visible = true;
      }
      else if (ShowHideButtons == "0")
      {
        Button_FileDeleteAll.Visible = false;
      }
    }

    private void ShowHide(string ButtonClicked)
    {
      if (ButtonClicked == "Upload")
      {
        Label_FileDelete.Text = "";
      }
      else if (ButtonClicked == "Delete")
      {
        Label_FileUpload.Text = "";
      }
      else
      {
        Label_FileUpload.Text = "";
        Label_FileDelete.Text = "";
      }
    }
    //---END--- --File--//
  }
}