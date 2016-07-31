using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_InfectionPrevention : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected bool Button_EditUpdateClicked = false;
    protected bool Button_EditPrintClicked = false;
    protected bool Button_EditEmailClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSource_InfectionPrevention_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_InsertFacility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_InsertUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_InsertInfectionType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_InsertSSISubType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_InsertRDNotifiableDisease.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_EditUnitId.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_EditInfectionType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_EditSSISubType.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
        SqlDataSource_InfectionPrevention_EditRDNotifiableDisease.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_InfectionPrevention, this.GetType(), "UpdateProgress_Start", "Validation_Form();ShowHide_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          SqlDataSource_InfectionPrevention_InsertFacility.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_InfectionPrevention_InsertUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_InfectionPrevention_EditUnitId.SelectParameters["SecurityUser_UserName"].DefaultValue = Request.ServerVariables["LOGON_USER"];

          Label_Title.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("4")).ToString();
          Label_FormHeading.Text = (InfoQuestWCF.InfoQuest_All.All_FormName("4")).ToString();

          TableForm.Visible = true;

          SetFormVisibility();

          TableFormVisible();

          SetFormQueryString();

          if (!string.IsNullOrEmpty(Request.QueryString["s_FacilityId"]) && !string.IsNullOrEmpty(Request.QueryString["s_PatientVisitNumber"]))
          {
            if (string.IsNullOrEmpty(Request.QueryString["InfectionFormID"]))
            {
              ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDateInfectionReported")).Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);

              ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertUnitId")).Items.Clear();
              SqlDataSource_InfectionPrevention_InsertUnitId.SelectParameters["Facility_Id"].DefaultValue = Request.QueryString["s_FacilityId"];
              ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertUnitId")).Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select Unit", CultureInfo.CurrentCulture), ""));
              ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertUnitId")).DataBind();

              string PatientErrorMessage = "";

              if (string.IsNullOrEmpty(((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")).Text))
              {
                ToolkitScriptManager_InfectionPrevention.SetFocus(((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")));
                PatientErrorMessage = PatientErrorMessage + Convert.ToString("Visit Number not provided to find Patient Information<br />", CultureInfo.CurrentCulture);
              }
              else
              {
                if (string.IsNullOrEmpty(((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility")).SelectedValue))
                {
                  ToolkitScriptManager_InfectionPrevention.SetFocus(((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")));
                  PatientErrorMessage = PatientErrorMessage + Convert.ToString("Facility not Selected to find Patient Information<br />", CultureInfo.CurrentCulture);
                }
                else
                {
                  Session["NameandSurname"] = "";
                  Session["Age"] = "";
                  Session["DateofAdmission"] = "";
                  DataTable DataTable_DataPatient;
                  using (DataTable_DataPatient = new DataTable())
                  {
                    DataTable_DataPatient.Locale = CultureInfo.CurrentCulture;
                    //DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility")).SelectedValue.ToString(), TextBox_InsertPatientVisitNumber.Text).Copy();
                    DataTable_DataPatient = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility")).SelectedValue.ToString(), ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")).Text).Copy();

                    if (DataTable_DataPatient.Columns.Count == 1)
                    {
                      foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
                      {
                        PatientErrorMessage = PatientErrorMessage + DataRow_Row["Error"];
                      }
                    }
                    else if (DataTable_DataPatient.Columns.Count != 1)
                    {
                      if (DataTable_DataPatient.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_DataPatient.Rows)
                        {
                          Session["NameandSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];
                          Session["Age"] = DataRow_Row["PatientAge"];
                          Session["DateofAdmission"] = DataRow_Row["DateOfAdmission"];

                          string NameandSurname = Session["NameandSurname"].ToString();
                          NameandSurname = NameandSurname.Replace("'", "");
                          Session["NameandSurname"] = NameandSurname;
                          NameandSurname = "";

                          PatientErrorMessage = "";

                          ((Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertPatientName")).Text = Session["NameandSurname"].ToString();
                          ((Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertAge")).Text = Session["Age"].ToString();
                          ((Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertDateOfAdmission")).Text = Session["DateofAdmission"].ToString();
                        }
                      }
                      else
                      {
                        PatientErrorMessage = PatientErrorMessage + Convert.ToString("Patient Information not found for specific Patient Visit Number,<br />Please type in the Patient Information<br />", CultureInfo.CurrentCulture);
                      }
                    }
                  }

                  Session["NameandSurname"] = "";
                  Session["Age"] = "";
                  Session["DateofAdmission"] = "";

                  if (string.IsNullOrEmpty(PatientErrorMessage))
                  {
                    ((CheckBoxList)FormView_InfectionPrevention_Form.FindControl("CheckBoxList_InsertVisitDiagnosis")).Items.Clear();

                    Session["CDGCDCODE"] = "";
                    Session["CDDESCRIPTION"] = "";
                    DataTable DataTable_InfectionPrevention_VistDiagnosis;
                    using (DataTable_InfectionPrevention_VistDiagnosis = new DataTable())
                    {
                      DataTable_InfectionPrevention_VistDiagnosis.Locale = CultureInfo.CurrentCulture;
                      DataTable_InfectionPrevention_VistDiagnosis = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_IMeds_InfectionPrevention_VisitDiagnosis(((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility")).SelectedValue.ToString(), ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")).Text).Copy();

                      if (DataTable_InfectionPrevention_VistDiagnosis.Columns.Count == 1)
                      {
                        foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_VistDiagnosis.Rows)
                        {
                          PatientErrorMessage = PatientErrorMessage + DataRow_Row["Error"];
                        }
                      }
                      else if (DataTable_InfectionPrevention_VistDiagnosis.Columns.Count != 1)
                      {
                        if (DataTable_InfectionPrevention_VistDiagnosis.Rows.Count > 0)
                        {
                          foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_VistDiagnosis.Rows)
                          {
                            Session["CDGCDCODE"] = DataRow_Row["CDG_CD_CODE"];
                            Session["CDDESCRIPTION"] = DataRow_Row["CD_DESCRIPTION"];

                            string CDDESCRIPTION = Session["CDDESCRIPTION"].ToString();
                            CDDESCRIPTION = CDDESCRIPTION.Replace("'", "");
                            Session["CDDESCRIPTION"] = CDDESCRIPTION;
                            CDDESCRIPTION = "";

                            PatientErrorMessage = "";

                            ((CheckBoxList)FormView_InfectionPrevention_Form.FindControl("CheckBoxList_InsertVisitDiagnosis")).Items.Add(Session["CDGCDCODE"].ToString() + " | " + Session["CDDESCRIPTION"].ToString());
                          }
                        }
                        else
                        {
                          PatientErrorMessage = PatientErrorMessage + Convert.ToString("Patient Information not found for specific Patient Visit Number,<br />Please type in the Patient Information<br />", CultureInfo.CurrentCulture);
                        }
                      }
                    }

                    Session["CDGCDCODE"] = "";
                    Session["CDDESCRIPTION"] = "";

                    if (string.IsNullOrEmpty(PatientErrorMessage))
                    {
                      DataTable DataTable_InfectionPrevention_Site_LabReport;
                      using (DataTable_InfectionPrevention_Site_LabReport = new DataTable())
                      {
                        DataTable_InfectionPrevention_Site_LabReport.Locale = CultureInfo.CurrentCulture;
                        DataTable_InfectionPrevention_Site_LabReport = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_IMeds_InfectionPrevention_Site_LabReport(((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility")).SelectedValue.ToString(), ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")).Text).Copy();

                        if (DataTable_InfectionPrevention_Site_LabReport.Columns.Count == 1)
                        {
                          foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Site_LabReport.Rows)
                          {
                            PatientErrorMessage = PatientErrorMessage + DataRow_Row["Error"];
                          }
                        }
                      }

                      ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_InsertInfectionPrevention_LabReport")).DataSource = DataTable_InfectionPrevention_Site_LabReport;
                      ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_InsertInfectionPrevention_LabReport")).DataBind();

                      if (string.IsNullOrEmpty(PatientErrorMessage))
                      {
                        DataTable DataTable_InfectionPrevention_Site_BedHistory;
                        using (DataTable_InfectionPrevention_Site_BedHistory = new DataTable())
                        {
                          DataTable_InfectionPrevention_Site_BedHistory.Locale = CultureInfo.CurrentCulture;
                          DataTable_InfectionPrevention_Site_BedHistory = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_IMeds_InfectionPrevention_Site_BedHistory(((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility")).SelectedValue.ToString(), ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")).Text).Copy();

                          if (DataTable_InfectionPrevention_Site_BedHistory.Columns.Count == 1)
                          {
                            foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Site_BedHistory.Rows)
                            {
                              PatientErrorMessage = PatientErrorMessage + DataRow_Row["Error"];
                            }
                          }
                        }

                        ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_InsertInfectionPrevention_BedHistory")).DataSource = DataTable_InfectionPrevention_Site_BedHistory;
                        ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_InsertInfectionPrevention_BedHistory")).DataBind();


                        if (string.IsNullOrEmpty(PatientErrorMessage))
                        {
                          DataTable DataTable_InfectionPrevention_Site_Surgery;
                          using (DataTable_InfectionPrevention_Site_Surgery = new DataTable())
                          {
                            DataTable_InfectionPrevention_Site_Surgery.Locale = CultureInfo.CurrentCulture;
                            DataTable_InfectionPrevention_Site_Surgery = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_IMeds_InfectionPrevention_Site_Surgery(((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility")).SelectedValue.ToString(), ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")).Text).Copy();

                            if (DataTable_InfectionPrevention_Site_Surgery.Columns.Count == 1)
                            {
                              foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Site_Surgery.Rows)
                              {
                                PatientErrorMessage = PatientErrorMessage + DataRow_Row["Error"];
                              }
                            }
                          }

                          if (string.IsNullOrEmpty(PatientErrorMessage))
                          {
                            ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_InsertInfectionPrevention_Surgery")).DataSource = DataTable_InfectionPrevention_Site_Surgery;
                            ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_InsertInfectionPrevention_Surgery")).DataBind();
                          }
                        }
                      }
                    }
                  }
                }
              }

              if (string.IsNullOrEmpty(PatientErrorMessage))
              {
                ((HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_InsertValidIMedsData")).Value = "Yes";
              }
              else
              {
                ((HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_InsertValidIMedsData")).Value = "No";
              }

              ((Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertPatientError")).Text = PatientErrorMessage;

              InsertRegisterPostBackControl();
            }
            else
            {              
              if (((HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_EditValidIMedsData")) != null)
              {
                string sFormStatus = "";
                string SQLStringInfectionPrevention = "SELECT sFormStatus FROM tblInfectionPrevention WHERE pkiInfectionFormID = @pkiInfectionFormID";
                using (SqlCommand SqlCommand_InfectionPrevention = new SqlCommand(SQLStringInfectionPrevention))
                {
                  SqlCommand_InfectionPrevention.Parameters.AddWithValue("@pkiInfectionFormID", Request.QueryString["InfectionFormID"]);
                  DataTable DataTable_InfectionPrevention;
                  using (DataTable_InfectionPrevention = new DataTable())
                  {
                    DataTable_InfectionPrevention.Locale = CultureInfo.CurrentCulture;
                    DataTable_InfectionPrevention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention).Copy();
                    if (DataTable_InfectionPrevention.Rows.Count > 0)
                    {
                      foreach (DataRow DataRow_Row in DataTable_InfectionPrevention.Rows)
                      {
                        sFormStatus = DataRow_Row["sFormStatus"].ToString();
                      }
                    }
                  }
                }


                if (sFormStatus == "Approved")
                {
                  ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditApprove")).Visible = false;
                  ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditReject")).Visible = true;
                  ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditUpdate")).Visible = true;
                }
                else if (sFormStatus == "Pending Approval")
                {
                  ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditApprove")).Visible = true;
                  ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditReject")).Visible = true;
                  ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditUpdate")).Visible = false;
                }
                else if (sFormStatus == "Rejected")
                {
                  ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditApprove")).Visible = true;
                  ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditReject")).Visible = false;
                  ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditUpdate")).Visible = false;
                }

                string PatientErrorMessage = "";

                ((CheckBoxList)FormView_InfectionPrevention_Form.FindControl("CheckBoxList_EditVisitDiagnosis")).Items.Clear();

                Session["sCode"] = "";
                Session["sDescription"] = "";
                Session["bSelected"] = "";
                string SQLStringVisitDiagnosis = "SELECT sCode ,sDescription ,bSelected FROM dbo.tblInfectionPrevention_VisitDiagnosis WHERE fkiInfectionFormID = @fkiInfectionFormID";
                using (SqlCommand SqlCommand_VisitDiagnosis = new SqlCommand(SQLStringVisitDiagnosis))
                {
                  SqlCommand_VisitDiagnosis.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
                  DataTable DataTable_VisitDiagnosis;
                  using (DataTable_VisitDiagnosis = new DataTable())
                  {
                    DataTable_VisitDiagnosis.Locale = CultureInfo.CurrentCulture;
                    DataTable_VisitDiagnosis = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_VisitDiagnosis).Copy();
                    if (DataTable_VisitDiagnosis.Rows.Count > 0)
                    {
                      foreach (DataRow DataRow_Row in DataTable_VisitDiagnosis.Rows)
                      {
                        Session["sCode"] = DataRow_Row["sCode"];
                        Session["sDescription"] = DataRow_Row["sDescription"];
                        Session["bSelected"] = DataRow_Row["bSelected"];

                        string sDescription = Session["sDescription"].ToString();
                        sDescription = sDescription.Replace("'", "");
                        Session["sDescription"] = sDescription;
                        sDescription = "";

                        PatientErrorMessage = "";

                        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(Convert.ToString(Session["sCode"].ToString() + " | " + Session["sDescription"].ToString(), CultureInfo.CurrentCulture));
                        item.Selected = (bool)Session["bSelected"]; 
                        ((CheckBoxList)FormView_InfectionPrevention_Form.FindControl("CheckBoxList_EditVisitDiagnosis")).Items.Add(item);
                      }
                    }
                  }
                }


                //START InfectionPrevention_Site_Surgery
                string SQLStringInfectionPrevention_Site_Surgery = "SELECT  pkiSurgeryID ,fkiSiteID ,sFacility ,sVisitNumber ,sProcedure ,sSurgeryDate ,sSurgeryDuration ,sTheatre ,sTheatreInvoice ,sSurgeon ,sAssistant ,sScrubNurse ,sAnaesthesist ,sWound ,sCategory ,sFinalDiagnosis ,sAdmissionDate ,sDischargeDate ,bSelected FROM tblInfectionPrevention_Site_Surgery WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
                using (SqlCommand SqlCommand_InfectionPrevention_Site_Surgery = new SqlCommand(SQLStringInfectionPrevention_Site_Surgery))
                {
                  SqlCommand_InfectionPrevention_Site_Surgery.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
                  DataTable DataTable_InfectionPrevention_Site_Surgery;
                  using (DataTable_InfectionPrevention_Site_Surgery = new DataTable())
                  {
                    DataTable_InfectionPrevention_Site_Surgery.Locale = CultureInfo.CurrentCulture;
                    DataTable_InfectionPrevention_Site_Surgery = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_Surgery).Copy();
                  }

                  ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_EditInfectionPrevention_Surgery")).DataSource = DataTable_InfectionPrevention_Site_Surgery;
                  ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_EditInfectionPrevention_Surgery")).DataBind();
                }
                //END InfectionPrevention_Site_Surgery


                //START InfectionPrevention_Site_BedHistory
                string SQLStringInfectionPrevention_Site_BedHistory = "SELECT pkiBedHistoryID ,fkiSiteID ,sFromUnit ,sToUnit ,sDateTransferred ,bSelected FROM tblInfectionPrevention_Site_BedHistory WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
                using (SqlCommand SqlCommand_InfectionPrevention_Site_BedHistory = new SqlCommand(SQLStringInfectionPrevention_Site_BedHistory))
                {
                  SqlCommand_InfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
                  DataTable DataTable_InfectionPrevention_Site_BedHistory;
                  using (DataTable_InfectionPrevention_Site_BedHistory = new DataTable())
                  {
                    DataTable_InfectionPrevention_Site_BedHistory.Locale = CultureInfo.CurrentCulture;
                    DataTable_InfectionPrevention_Site_BedHistory = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_BedHistory).Copy();
                  }

                  ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_EditInfectionPrevention_BedHistory")).DataSource = DataTable_InfectionPrevention_Site_BedHistory;
                  ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_EditInfectionPrevention_BedHistory")).DataBind();
                }
                //END InfectionPrevention_Site_BedHistory


                //START InfectionPrevention_Site_LabReport
                string SQLStringInfectionPrevention_Site_LabReport = "SELECT pkiLabReportID ,fkiSiteID ,sLabDate ,sSpecimen ,sOrganism ,sLabNumber ,bSelected FROM tblInfectionPrevention_Site_LabReport WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
                using (SqlCommand SqlCommand_InfectionPrevention_Site_LabReport = new SqlCommand(SQLStringInfectionPrevention_Site_LabReport))
                {
                  SqlCommand_InfectionPrevention_Site_LabReport.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
                  DataTable DataTable_InfectionPrevention_Site_LabReport;
                  using (DataTable_InfectionPrevention_Site_LabReport = new DataTable())
                  {
                    DataTable_InfectionPrevention_Site_LabReport.Locale = CultureInfo.CurrentCulture;
                    DataTable_InfectionPrevention_Site_LabReport = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_LabReport).Copy();
                  }

                  ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_EditInfectionPrevention_LabReport")).DataSource = DataTable_InfectionPrevention_Site_LabReport;
                  ((GridView)FormView_InfectionPrevention_Form.FindControl("GridView_EditInfectionPrevention_LabReport")).DataBind();
                }
                //END InfectionPrevention_Site_LabReport


                if (string.IsNullOrEmpty(PatientErrorMessage))
                {
                  ((HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_EditValidIMedsData")).Value = "Yes";
                }
                else
                {
                  ((HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_EditValidIMedsData")).Value = "No";
                }

                EditRegisterPostBackControl();
              }
            }
          }
        }
      }
    }

    protected string PageSecurity()
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('37'))";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("37");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_InfectionPrevention.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(Request.QueryString["InfectionFormID"]))
      {
        if (FormView_InfectionPrevention_Form.CurrentMode == FormViewMode.Insert)
        {
          DropDownList DropDownList_InsertFacility = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility");
          TextBox TextBox_InsertPatientVisitNumber = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber");

          if (string.IsNullOrEmpty(DropDownList_InsertFacility.SelectedValue.ToString()))
          {
            if (Request.QueryString["s_FacilityId"] == null)
            {
              DropDownList_InsertFacility.SelectedValue = "";
            }
            else
            {
              DropDownList_InsertFacility.SelectedValue = Request.QueryString["s_FacilityId"];
            }
          }

          if (string.IsNullOrEmpty(TextBox_InsertPatientVisitNumber.Text.ToString()))
          {
            if (Request.QueryString["s_PatientVisitNumber"] == null)
            {
              TextBox_InsertPatientVisitNumber.Text = "";
            }
            else
            {
              TextBox_InsertPatientVisitNumber.Text = Request.QueryString["s_PatientVisitNumber"];
            }
          }
        }
      }
    }

    protected void SetFormVisibility()
    {
      if (string.IsNullOrEmpty(Request.QueryString["InfectionFormID"]))
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
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('37'))";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '22'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '155'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '11'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '10'");

            string Security = "1";
            if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
            {
              Security = "0";
              //FormView_InfectionPrevention_Form.ChangeMode(FormViewMode.Insert);
              Response.Redirect("Form_InfectionPrevention_List.aspx", true);
            }

            if (Security == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0))
            {
              Security = "0";
              FormView_InfectionPrevention_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Security == "1")
            {
              Security = "0";
              FormView_InfectionPrevention_Form.ChangeMode(FormViewMode.ReadOnly);
            }
          }
        }
      }
    }

    protected void SetFormVisibility_Edit()
    {
      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('37')) AND (Facility_Id IN (SELECT fkiFacilityID FROM tblInfectionPrevention WHERE pkiInfectionFormID = @pkiInfectionFormID) OR SecurityRole_Rank = 1)";
      using (SqlCommand SqlCommand_FormMode = new SqlCommand(SQLStringFormMode))
      {
        SqlCommand_FormMode.Parameters.AddWithValue("@SecurityUser_Username", Request.ServerVariables["LOGON_USER"]);
        SqlCommand_FormMode.Parameters.AddWithValue("@pkiInfectionFormID", Request.QueryString["InfectionFormID"]);

        DataTable DataTable_FormMode;
        using (DataTable_FormMode = new DataTable())
        {
          DataTable_FormMode.Locale = CultureInfo.CurrentCulture;
          DataTable_FormMode = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormMode).Copy();

          if (DataTable_FormMode.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_FormMode.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '22'");
            DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '155'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '11'");
            DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '10'");

            Session["bInvestigationCompleted"] = "";
            string SQLStringInfectionPrevention = "SELECT bInvestigationCompleted FROM tblInfectionPrevention WHERE pkiInfectionFormID = @pkiInfectionFormID";
            using (SqlCommand SqlCommand_InfectionPrevention = new SqlCommand(SQLStringInfectionPrevention))
            {
              SqlCommand_InfectionPrevention.Parameters.AddWithValue("@pkiInfectionFormID", Request.QueryString["InfectionFormID"]);
              DataTable DataTable_InfectionPrevention;
              using (DataTable_InfectionPrevention = new DataTable())
              {
                DataTable_InfectionPrevention.Locale = CultureInfo.CurrentCulture;
                DataTable_InfectionPrevention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention).Copy();
                if (DataTable_InfectionPrevention.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_InfectionPrevention.Rows)
                  {
                    Session["bInvestigationCompleted"] = DataRow_Row["bInvestigationCompleted"];
                  }
                }
              }
            }

            string Security = "1";
            if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
            {
              Security = "0";
              FormView_InfectionPrevention_Form.ChangeMode(FormViewMode.Edit);
            }

            if (Security == "1" && (SecurityFormAdminView.Length > 0))
            {
              Security = "0";
              FormView_InfectionPrevention_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Security == "1" && (SecurityFacilityAdminUpdate.Length > 0))
            {
              Security = "0";
              if ((Boolean)Session["bInvestigationCompleted"] == false)
              {
                FormView_InfectionPrevention_Form.ChangeMode(FormViewMode.Edit);
              }
              else
              {
                FormView_InfectionPrevention_Form.ChangeMode(FormViewMode.ReadOnly);
              }
            }

            if (Security == "1" && (SecurityFacilityAdminView.Length > 0))
            {
              Security = "0";
              FormView_InfectionPrevention_Form.ChangeMode(FormViewMode.ReadOnly);
            }

            if (Security == "1")
            {
              Security = "0";

              FormView_InfectionPrevention_Form.ChangeMode(FormViewMode.ReadOnly);
            }
          }
        }
      }
    }

    protected void TableFormVisible()
    {
      if (FormView_InfectionPrevention_Form.CurrentMode == FormViewMode.Insert)
      {
        ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDateInfectionReported")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDateInfectionReported")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertInfectionType")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertSSISubType")).Attributes.Add("OnChange", "Validation_Form();");
        ((RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_InsertSeverity")).Attributes.Add("OnClick", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDays")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDays")).Attributes.Add("OnInput", "Validation_Form();");
        ((RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_InsertCompliance")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeSSI1")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeSSI2")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeSSI3")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeSSI4")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP1")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP2")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP3")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP4")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP5")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI1")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI2")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI3")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI4")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI5")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI6")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI7")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCAUTI1")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCAUTI2")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCAUTI3")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCAUTI4")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_InsertCompliance")).Attributes.Add("OnClick", "Validation_Form('Compliance');");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationDesignation")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationDesignation")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationIPCSName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationIPCSName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationTeamMembers")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationTeamMembers")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertInvestigationCompleted")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      }

      if (FormView_InfectionPrevention_Form.CurrentMode == FormViewMode.Edit)
      {
        ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditUnitId")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDateInfectionReported")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDateInfectionReported")).Attributes.Add("OnInput", "Validation_Form();");
        ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditInfectionType")).Attributes.Add("OnChange", "Validation_Form();ShowHide_Form();");
        ((DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditSSISubType")).Attributes.Add("OnChange", "Validation_Form();");
        ((RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_EditSeverity")).Attributes.Add("OnClick", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDays")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDays")).Attributes.Add("OnInput", "Validation_Form();");
        ((RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_EditCompliance")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI1")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI2")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI3")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI4")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP1")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP2")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP3")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP4")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP5")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI1")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI2")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI3")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI4")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI5")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI6")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI7")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI1")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI2")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI3")).Attributes.Add("OnClick", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI4")).Attributes.Add("OnClick", "Validation_Form();");
        ((RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_EditCompliance")).Attributes.Add("OnClick", "Validation_Form('Compliance');");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDate")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDesignation")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDesignation")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationIPCSName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationIPCSName")).Attributes.Add("OnInput", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationTeamMembers")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationTeamMembers")).Attributes.Add("OnInput", "Validation_Form();");
        ((CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditInvestigationCompleted")).Attributes.Add("OnClick", "Validation_Form();ShowHide_Form();");
      }
    }


    //--START-- --Search--//
    protected void Button_InsertSearch_OnClick(object sender, EventArgs e)
    {
      string Label_InsertInvalidFormMessageText = SearchValidation();

      if (string.IsNullOrEmpty(Label_InsertInvalidFormMessageText))
      {
        DropDownList DropDownList_InsertFacility = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility");
        TextBox TextBox_InsertPatientVisitNumber = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber");

        Response.Redirect("Form_InfectionPrevention.aspx?s_FacilityId=" + DropDownList_InsertFacility.SelectedValue.ToString() + "&s_PatientVisitNumber=" + Server.HtmlEncode(TextBox_InsertPatientVisitNumber.Text.ToString()) + "", false);
      }
      else
      {
        Label Label_InsertInvalidFormMessage = (Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertInvalidFormMessage");

        Label_InsertInvalidFormMessage.Text = Label_InsertInvalidFormMessageText;
      }
    }

    protected string SearchValidation()
    {
      string InvalidSearch = "No";
      string InvalidSearchMessage = "";

      DropDownList DropDownList_InsertFacility = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility");
      TextBox TextBox_InsertPatientVisitNumber = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber");

      if (InvalidSearch == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFacility.SelectedValue))
        {
          InvalidSearch = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertPatientVisitNumber.Text))
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

      string SearchField1 = Request.QueryString["Search_Facility"];
      string SearchField2 = Request.QueryString["Search_ReportNumber"];
      string SearchField3 = Request.QueryString["Search_PatientName"];
      string SearchField4 = Request.QueryString["Search_PatientVisitNumber"];
      string SearchField5 = Request.QueryString["Search_InfectionType"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null && SearchField4 == null && SearchField5 == null)
      {
        FinalURL = "Form_InfectionPrevention_List.aspx";
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_Facility=" + Request.QueryString["Search_Facility"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_ReportNumber=" + Request.QueryString["Search_ReportNumber"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_PatientName=" + Request.QueryString["Search_PatientName"] + "&";
        }

        if (SearchField4 == null)
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_PatientVisitNumber=" + Request.QueryString["Search_PatientVisitNumber"] + "&";
        }

        if (SearchField5 == null)
        {
          SearchField5 = "";
        }
        else
        {
          SearchField5 = "s_InfectionType=" + Request.QueryString["Search_InfectionType"] + "&";
        }

        string SearchURL = "Form_InfectionPrevention_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, true);
    }
    //---END--- --Search--//


    //--START-- --TableForm--//
    //--START-- --Insert--//
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1809:AvoidExcessiveLocals"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
    protected void FormView_InfectionPrevention_Form_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InsertInvalidFormMessage = InsertValidation();

        if (!string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_InfectionPrevention.SetFocus(UpdatePanel_InfectionPrevention);

          ((Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
          ((Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertConcurrencyInsertMessage")).Text = "";

          InsertRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_InsertInvalidFormMessage))
        {
          e.Cancel = false;

          string sFormStatus = "";
          string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @SecurityUser_Username) AND (SecurityRole_Id = '1' OR Form_Id IN ('37'))";
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
                DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '22'");
                DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '155'");
                DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '11'");
                DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '10'");

                string Security = "1";
                if (Security == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0))
                {
                  Security = "0";
                  sFormStatus = "Approved";
                }

                if (Security == "1" && (SecurityFormAdminView.Length > 0))
                {
                  Security = "0";
                  sFormStatus = "Pending Approval";
                }

                if (Security == "1" && (SecurityFacilityAdminUpdate.Length > 0))
                {
                  Security = "0";
                  sFormStatus = "Approved";
                }

                if (Security == "1" && (SecurityFacilityAdminView.Length > 0))
                {
                  Security = "0";
                  sFormStatus = "Pending Approval";
                }

                if (Security == "1")
                {
                  Security = "0";
                  sFormStatus = "Pending Approval";
                }
              }
            }
          }

          //START INSERT
          //HiddenField HiddenField_InsertValidIMedsData = (HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_InsertValidIMedsData");
          DropDownList DropDownList_InsertFacility = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility");
          TextBox TextBox_InsertPatientVisitNumber = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber");
          Label Label_InsertPatientName = (Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertPatientName");
          Label Label_InsertAge = (Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertAge");
          Label Label_InsertDateOfAdmission = (Label)FormView_InfectionPrevention_Form.FindControl("Label_InsertDateOfAdmission");
          CheckBoxList CheckBoxList_InsertVisitDiagnosis = (CheckBoxList)FormView_InfectionPrevention_Form.FindControl("CheckBoxList_InsertVisitDiagnosis");

          DropDownList DropDownList_InsertUnitId = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertUnitId");
          TextBox TextBox_InsertDateInfectionReported = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDateInfectionReported");
          DropDownList DropDownList_InsertInfectionType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertInfectionType");
          DropDownList DropDownList_InsertSSISubType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertSSISubType");
          RadioButtonList RadioButtonList_InsertSeverity = (RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_InsertSeverity");
          TextBox TextBox_InsertDescription = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDescription");
          TextBox TextBox_InsertDays = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDays");
          RadioButtonList RadioButtonList_InsertCompliance = (RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_InsertCompliance");
          CheckBox CheckBox_InsertRiskTPN = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertRiskTPN");
          CheckBox CheckBox_InsertRiskEnteralFeeding = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertRiskEnteralFeeding");
          CheckBox CheckBox_InsertTypeSSI1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeSSI1");
          CheckBox CheckBox_InsertTypeSSI2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeSSI2");
          CheckBox CheckBox_InsertTypeSSI3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeSSI3");
          CheckBox CheckBox_InsertTypeSSI4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeSSI4");
          CheckBox CheckBox_InsertTypeCLABSI1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI1");
          CheckBox CheckBox_InsertTypeCLABSI2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI2");
          CheckBox CheckBox_InsertTypeCLABSI3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI3");
          CheckBox CheckBox_InsertTypeCLABSI4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI4");
          CheckBox CheckBox_InsertTypeCLABSI5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI5");
          CheckBox CheckBox_InsertTypeCLABSI6 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI6");
          CheckBox CheckBox_InsertTypeCLABSI7 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCLABSI7");
          CheckBox CheckBox_InsertTypeVAP1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP1");
          CheckBox CheckBox_InsertTypeVAP2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP2");
          CheckBox CheckBox_InsertTypeVAP3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP3");
          CheckBox CheckBox_InsertTypeVAP4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP4");
          CheckBox CheckBox_InsertTypeVAP5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeVAP5");
          CheckBox CheckBox_InsertTypeCAUTI1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCAUTI1");
          CheckBox CheckBox_InsertTypeCAUTI2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCAUTI2");
          CheckBox CheckBox_InsertTypeCAUTI3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCAUTI3");
          CheckBox CheckBox_InsertTypeCAUTI4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertTypeCAUTI4");

          CheckBox CheckBox_InsertInvestigationCompleted = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertInvestigationCompleted");
          TextBox TextBox_InsertInvestigationDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationDate");
          TextBox TextBox_InsertInvestigationName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationName");
          TextBox TextBox_InsertInvestigationDesignation = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationDesignation");
          TextBox TextBox_InsertInvestigationIPCSName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationIPCSName");
          TextBox TextBox_InsertInvestigationTeamMembers = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationTeamMembers");
          TextBox TextBox_InsertInvestigationCompletedDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationCompletedDate");

          CheckBox CheckBox_InsertPIM1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPIM1");
          CheckBox CheckBox_InsertPIM2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPIM2");
          CheckBox CheckBox_InsertPIM3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPIM3");
          CheckBox CheckBox_InsertPIM4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPIM4");
          CheckBox CheckBox_InsertPIM5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPIM5");
          CheckBox CheckBox_InsertPIM6 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPIM6");

          //START InfectionPrevention
          string sReportNumber = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], DropDownList_InsertFacility.SelectedValue, "4");

          string SQLStringInsertInfectionPrevention = "INSERT INTO tblInfectionPrevention ( fkiFacilityID ,fkiFormStatusID ,sReportNumber ,sPatientVisitNumber ,sPatientName ,sPatientAge ,sPatientDateOfAdmission ,dtDateOfInvestigation ,sInvestigatorName ,sInvestigatorDesignation ,sIPCSName ,sTeamMembers ,bInvestigationCompleted ,dtInvestigationCompleted ,dtCreated ,sCreatedBy ,dtModified ,sModifiedBy ,sFormStatus ,sApprovedRejectedBy ,dtApprovedRejected ,sRejectionReason ) VALUES ( @fkiFacilityID ,@fkiFormStatusID ,@sReportNumber ,@sPatientVisitNumber ,@sPatientName ,@sPatientAge ,@sPatientDateOfAdmission ,@dtDateOfInvestigation ,@sInvestigatorName ,@sInvestigatorDesignation ,@sIPCSName ,@sTeamMembers ,@bInvestigationCompleted ,@dtInvestigationCompleted ,@dtCreated ,@sCreatedBy ,@dtModified ,@sModifiedBy ,@sFormStatus ,@sApprovedRejectedBy ,@dtApprovedRejected ,@sRejectionReason ); SELECT SCOPE_IDENTITY()";
          string LastpkiInfectionFormID = "";
          using (SqlCommand SqlCommand_InsertInfectionPrevention = new SqlCommand(SQLStringInsertInfectionPrevention))
          {
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@fkiFacilityID", DropDownList_InsertFacility.SelectedValue);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@fkiFormStatusID", 0);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sReportNumber", sReportNumber);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sPatientVisitNumber", TextBox_InsertPatientVisitNumber.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sPatientName", Label_InsertPatientName.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sPatientAge", Label_InsertAge.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sPatientDateOfAdmission", Label_InsertDateOfAdmission.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@dtDateOfInvestigation", TextBox_InsertInvestigationDate.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sInvestigatorName", TextBox_InsertInvestigationName.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sInvestigatorDesignation", TextBox_InsertInvestigationDesignation.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sIPCSName", TextBox_InsertInvestigationIPCSName.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sTeamMembers", TextBox_InsertInvestigationTeamMembers.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@bInvestigationCompleted", CheckBox_InsertInvestigationCompleted.Checked.ToString());
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@dtInvestigationCompleted", TextBox_InsertInvestigationCompletedDate.Text);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@dtCreated", DateTime.Now);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sCreatedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@dtModified", DateTime.Now);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sFormStatus", sFormStatus);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sApprovedRejectedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@dtApprovedRejected", DateTime.Now);
            SqlCommand_InsertInfectionPrevention.Parameters.AddWithValue("@sRejectionReason", DBNull.Value);
            LastpkiInfectionFormID = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertInfectionPrevention);
          }
          //END InfectionPrevention

          if (!string.IsNullOrEmpty(LastpkiInfectionFormID))
          {
            //START InfectionPrevention_VisitDiagnosis
            for (int i = 0; i < CheckBoxList_InsertVisitDiagnosis.Items.Count; i++)
            {
              string sCode = "";
              string sDescription = "";
              string VisitDiagnosis = CheckBoxList_InsertVisitDiagnosis.Items[i].Text;

              string[] Split = VisitDiagnosis.Split("|".ToCharArray(), StringSplitOptions.None);
              sCode = Split[0];
              sCode = sCode.Trim();
              sDescription = Split[1];
              sDescription = sDescription.Trim();

              string SQLStringInsertInfectionPrevention_VisitDiagnosis = "INSERT INTO tblInfectionPrevention_VisitDiagnosis ( fkiInfectionFormID ,sCode ,sDescription ,bSelected ) VALUES ( @fkiInfectionFormID ,@sCode ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_VisitDiagnosis = new SqlCommand(SQLStringInsertInfectionPrevention_VisitDiagnosis))
              {
                SqlCommand_InsertInfectionPrevention_VisitDiagnosis.Parameters.AddWithValue("@fkiInfectionFormID", LastpkiInfectionFormID);
                SqlCommand_InsertInfectionPrevention_VisitDiagnosis.Parameters.AddWithValue("@sCode", sCode);
                SqlCommand_InsertInfectionPrevention_VisitDiagnosis.Parameters.AddWithValue("@sDescription", sDescription);
                SqlCommand_InsertInfectionPrevention_VisitDiagnosis.Parameters.AddWithValue("@bSelected", CheckBoxList_InsertVisitDiagnosis.Items[i].Selected);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_VisitDiagnosis);
              }
            }
            //END InfectionPrevention_VisitDiagnosis


            //START InfectionPrevention_PrecautionaryMeasure
            string SQLStringInsertInfectionPrevention_PrecautionaryMeasure1 = "INSERT INTO tblInfectionPrevention_PrecautionaryMeasure ( fkiInfectionFormID ,fkiPrecautionaryMeasureID ,bSelected ) VALUES ( @fkiInfectionFormID ,@fkiPrecautionaryMeasureID ,@bSelected )";
            using (SqlCommand SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure1 = new SqlCommand(SQLStringInsertInfectionPrevention_PrecautionaryMeasure1))
            {
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure1.Parameters.AddWithValue("@fkiInfectionFormID", LastpkiInfectionFormID);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure1.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1201);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure1.Parameters.AddWithValue("@bSelected", CheckBox_InsertPIM1.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure1);
            }

            string SQLStringInsertInfectionPrevention_PrecautionaryMeasure2 = "INSERT INTO tblInfectionPrevention_PrecautionaryMeasure ( fkiInfectionFormID ,fkiPrecautionaryMeasureID ,bSelected ) VALUES ( @fkiInfectionFormID ,@fkiPrecautionaryMeasureID ,@bSelected )";
            using (SqlCommand SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure2 = new SqlCommand(SQLStringInsertInfectionPrevention_PrecautionaryMeasure2))
            {
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure2.Parameters.AddWithValue("@fkiInfectionFormID", LastpkiInfectionFormID);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure2.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1202);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure2.Parameters.AddWithValue("@bSelected", CheckBox_InsertPIM2.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure2);
            }

            string SQLStringInsertInfectionPrevention_PrecautionaryMeasure3 = "INSERT INTO tblInfectionPrevention_PrecautionaryMeasure ( fkiInfectionFormID ,fkiPrecautionaryMeasureID ,bSelected ) VALUES ( @fkiInfectionFormID ,@fkiPrecautionaryMeasureID ,@bSelected )";
            using (SqlCommand SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure3 = new SqlCommand(SQLStringInsertInfectionPrevention_PrecautionaryMeasure3))
            {
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure3.Parameters.AddWithValue("@fkiInfectionFormID", LastpkiInfectionFormID);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure3.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1203);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure3.Parameters.AddWithValue("@bSelected", CheckBox_InsertPIM3.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure3);
            }

            string SQLStringInsertInfectionPrevention_PrecautionaryMeasure4 = "INSERT INTO tblInfectionPrevention_PrecautionaryMeasure ( fkiInfectionFormID ,fkiPrecautionaryMeasureID ,bSelected ) VALUES ( @fkiInfectionFormID ,@fkiPrecautionaryMeasureID ,@bSelected )";
            using (SqlCommand SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure4 = new SqlCommand(SQLStringInsertInfectionPrevention_PrecautionaryMeasure4))
            {
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure4.Parameters.AddWithValue("@fkiInfectionFormID", LastpkiInfectionFormID);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure4.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 2735);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure4.Parameters.AddWithValue("@bSelected", CheckBox_InsertPIM4.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure4);
            }

            string SQLStringInsertInfectionPrevention_PrecautionaryMeasure5 = "INSERT INTO tblInfectionPrevention_PrecautionaryMeasure ( fkiInfectionFormID ,fkiPrecautionaryMeasureID ,bSelected ) VALUES ( @fkiInfectionFormID ,@fkiPrecautionaryMeasureID ,@bSelected )";
            using (SqlCommand SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure5 = new SqlCommand(SQLStringInsertInfectionPrevention_PrecautionaryMeasure5))
            {
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure5.Parameters.AddWithValue("@fkiInfectionFormID", LastpkiInfectionFormID);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure5.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1205);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure5.Parameters.AddWithValue("@bSelected", CheckBox_InsertPIM5.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure5);
            }

            string SQLStringInsertInfectionPrevention_PrecautionaryMeasure6 = "INSERT INTO tblInfectionPrevention_PrecautionaryMeasure ( fkiInfectionFormID ,fkiPrecautionaryMeasureID ,bSelected ) VALUES ( @fkiInfectionFormID ,@fkiPrecautionaryMeasureID ,@bSelected )";
            using (SqlCommand SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure6 = new SqlCommand(SQLStringInsertInfectionPrevention_PrecautionaryMeasure6))
            {
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure6.Parameters.AddWithValue("@fkiInfectionFormID", LastpkiInfectionFormID);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure6.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1204);
              SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure6.Parameters.AddWithValue("@bSelected", CheckBox_InsertPIM6.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_PrecautionaryMeasure6);
            }
            //END InfectionPrevention_PrecautionaryMeasure


            //START InfectionPrevention_Antibiotic
            string IM_LONG_DESCRIPTION = "";
            DataTable DataTable_InfectionPrevention_Antibiotic;
            using (DataTable_InfectionPrevention_Antibiotic = new DataTable())
            {
              DataTable_InfectionPrevention_Antibiotic.Locale = CultureInfo.CurrentCulture;
              DataTable_InfectionPrevention_Antibiotic = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_IMeds_InfectionPrevention_Antibiotic(DropDownList_InsertFacility.SelectedValue.ToString(), TextBox_InsertPatientVisitNumber.Text).Copy();

              if (DataTable_InfectionPrevention_Antibiotic.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Antibiotic.Rows)
                {
                  IM_LONG_DESCRIPTION = DataRow_Row["IM_LONG_DESCRIPTION"].ToString();

                  string SQLStringInsertInfectionPrevention_Antibiotic = "INSERT INTO tblInfectionPrevention_Antibiotic ( fkiInfectionFormID ,sDescription ) VALUES ( @fkiInfectionFormID ,@sDescription )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Antibiotic = new SqlCommand(SQLStringInsertInfectionPrevention_Antibiotic))
                  {
                    SqlCommand_InsertInfectionPrevention_Antibiotic.Parameters.AddWithValue("@fkiInfectionFormID", LastpkiInfectionFormID);
                    SqlCommand_InsertInfectionPrevention_Antibiotic.Parameters.AddWithValue("@sDescription", IM_LONG_DESCRIPTION);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Antibiotic);
                  }
                }
              }
            }
            IM_LONG_DESCRIPTION = "";
            //END InfectionPrevention_Antibiotic


            //START InfectionPrevention_Site
            string SQLStringInsertInfectionPrevention_Site = "INSERT INTO tblInfectionPrevention_Site ( fkiInfectionFormID ,fkiFacilityUnitID ,fkiInfectionTypeID ,fkiInfectionSubTypeID ,fkiSeverityTypeID ,fkiBundleComplianceID ,iSiteNumber ,dtReported ,sDescription ,sRelatedHighRiskProcedures ,sInfectionDays ) VALUES ( @fkiInfectionFormID ,@fkiFacilityUnitID ,@fkiInfectionTypeID ,@fkiInfectionSubTypeID ,@fkiSeverityTypeID ,@fkiBundleComplianceID ,@iSiteNumber ,@dtReported ,@sDescription ,@sRelatedHighRiskProcedures ,@sInfectionDays ); SELECT SCOPE_IDENTITY()";
            string LastpkiSiteID = "";
            Int32 BundleComplianceID = 0;
            if (RadioButtonList_InsertCompliance.SelectedValue == "1")
            {
              BundleComplianceID = 1;
            }
            else if (RadioButtonList_InsertCompliance.SelectedValue == "2")
            {
              BundleComplianceID = 2;
            }
            else
            {
              BundleComplianceID = 3;
            }

            string RelatedHighRiskProcedures = "";
            if (CheckBox_InsertRiskEnteralFeeding.Checked == true && CheckBox_InsertRiskTPN.Checked == true)
            {
              RelatedHighRiskProcedures = "Enteral Feeding, TPN";
            }
            else
            {
              if (CheckBox_InsertRiskEnteralFeeding.Checked == true)
              {
                RelatedHighRiskProcedures = "Enteral Feeding";
              }

              if (CheckBox_InsertRiskTPN.Checked == true)
              {
                RelatedHighRiskProcedures = "TPN";
              }
            }

            using (SqlCommand SqlCommand_InsertInfectionPrevention_Site = new SqlCommand(SQLStringInsertInfectionPrevention_Site))
            {
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@fkiInfectionFormID", LastpkiInfectionFormID);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@fkiFacilityUnitID", DropDownList_InsertUnitId.SelectedValue);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@fkiInfectionTypeID", DropDownList_InsertInfectionType.SelectedValue);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@fkiInfectionSubTypeID", DropDownList_InsertSSISubType.SelectedValue);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@fkiSeverityTypeID", RadioButtonList_InsertSeverity.SelectedValue);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@fkiBundleComplianceID", BundleComplianceID);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@iSiteNumber", 1);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@dtReported", TextBox_InsertDateInfectionReported.Text);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@sDescription", TextBox_InsertDescription.Text);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@sRelatedHighRiskProcedures", RelatedHighRiskProcedures);
              SqlCommand_InsertInfectionPrevention_Site.Parameters.AddWithValue("@sInfectionDays", TextBox_InsertDays.Text);

              LastpkiSiteID = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetLastId(SqlCommand_InsertInfectionPrevention_Site);
            }
            //END InfectionPrevention_Site

            if (!string.IsNullOrEmpty(LastpkiSiteID))
            {
              //START InfectionPrevention_Site_BundleComplianceItem
              if (DropDownList_InsertInfectionType.SelectedValue == "1131" || DropDownList_InsertInfectionType.SelectedValue == "1135" || DropDownList_InsertInfectionType.SelectedValue == "1137" || DropDownList_InsertInfectionType.SelectedValue == "1139")
              {
                if (DropDownList_InsertInfectionType.SelectedValue == "1131")
                {
                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem11 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem11 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem11))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem11.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem11.Parameters.AddWithValue("@fkiBundleItemTypeID", 1206);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem11.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeSSI1.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem11);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem12 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem12 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem12))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem12.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem12.Parameters.AddWithValue("@fkiBundleItemTypeID", 1207);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem12.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeSSI2.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem12);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem13 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem13 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem13))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem13.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem13.Parameters.AddWithValue("@fkiBundleItemTypeID", 1208);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem13.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeSSI3.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem13);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem14 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem14 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem14))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem14.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem14.Parameters.AddWithValue("@fkiBundleItemTypeID", 1209);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem14.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeSSI4.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem14);
                  }
                }

                if (DropDownList_InsertInfectionType.SelectedValue == "1137")
                {
                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem21 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem21 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem21))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem21.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem21.Parameters.AddWithValue("@fkiBundleItemTypeID", 2682);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem21.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCLABSI1.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem21);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem22 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem22 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem22))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem22.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem22.Parameters.AddWithValue("@fkiBundleItemTypeID", 2683);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem22.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCLABSI2.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem22);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem23 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem23 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem23))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem23.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem23.Parameters.AddWithValue("@fkiBundleItemTypeID", 2684);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem23.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCLABSI3.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem23);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem24 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem24 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem24))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem24.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem24.Parameters.AddWithValue("@fkiBundleItemTypeID", 2685);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem24.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCLABSI4.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem24);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem25 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem25 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem25))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem25.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem25.Parameters.AddWithValue("@fkiBundleItemTypeID", 2686);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem25.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCLABSI5.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem25);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem26 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem26 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem26))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem26.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem26.Parameters.AddWithValue("@fkiBundleItemTypeID", 2687);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem26.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCLABSI6.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem26);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem27 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem27 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem27))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem27.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem27.Parameters.AddWithValue("@fkiBundleItemTypeID", 2688);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem27.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCLABSI7.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem27);
                  }
                }

                if (DropDownList_InsertInfectionType.SelectedValue == "1135")
                {
                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem31 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem31 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem31))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem31.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem31.Parameters.AddWithValue("@fkiBundleItemTypeID", 1222);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem31.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeVAP1.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem31);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem32 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem32 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem32))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem32.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem32.Parameters.AddWithValue("@fkiBundleItemTypeID", 1223);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem32.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeVAP2.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem32);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem33 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem33 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem33))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem33.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem33.Parameters.AddWithValue("@fkiBundleItemTypeID", 1224);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem33.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeVAP3.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem33);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem34 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem34 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem34))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem34.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem34.Parameters.AddWithValue("@fkiBundleItemTypeID", 1225);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem34.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeVAP4.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem34);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem35 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem35 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem35))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem35.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem35.Parameters.AddWithValue("@fkiBundleItemTypeID", 1226);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem35.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeVAP5.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem35);
                  }
                }

                if (DropDownList_InsertInfectionType.SelectedValue == "1139")
                {
                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem41 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem41 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem41))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem41.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem41.Parameters.AddWithValue("@fkiBundleItemTypeID", 1227);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem41.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCAUTI1.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem41);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem42 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem42 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem42))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem42.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem42.Parameters.AddWithValue("@fkiBundleItemTypeID", 1228);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem42.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCAUTI2.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem42);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem43 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem43 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem43))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem43.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem43.Parameters.AddWithValue("@fkiBundleItemTypeID", 1230);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem43.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCAUTI3.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem43);
                  }

                  string SQLStringInsertInfectionPrevention_Site_BundleComplianceItem44 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                  using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem44 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BundleComplianceItem44))
                  {
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem44.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem44.Parameters.AddWithValue("@fkiBundleItemTypeID", 1231);
                    SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem44.Parameters.AddWithValue("@bSelected", CheckBox_InsertTypeCAUTI4.Checked);
                    InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BundleComplianceItem44);
                  }
                }
              }
              //END InfectionPrevention_Site_BundleComplianceItem


              //START InfectionPrevention_Site_ReportableDisease
              CheckBox CheckBox_InsertRD = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertRD");
              DropDownList DropDownList_InsertRDNotifiableDisease = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertRDNotifiableDisease");
              TextBox TextBox_InsertRDDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertRDDate");
              TextBox TextBox_InsertRDReferenceNumber = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertRDReferenceNumber");

              string SQLStringInsertInfectionPrevention_Site_ReportableDisease = "INSERT INTO tblInfectionPrevention_Site_ReportableDisease ( fkiSiteID , sReportedToDepartment , fkiDiseaseID , sDateReported , sReferenceNumber , bSelected ) VALUES ( @fkiSiteID , @sReportedToDepartment , @fkiDiseaseID , @sDateReported , @sReferenceNumber , @bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_ReportableDisease = new SqlCommand(SQLStringInsertInfectionPrevention_Site_ReportableDisease))
              {
                SqlCommand_InsertInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@sReportedToDepartment", "Department of Health");
                SqlCommand_InsertInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@fkiDiseaseID", DropDownList_InsertRDNotifiableDisease.SelectedValue);
                SqlCommand_InsertInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@sDateReported", TextBox_InsertRDDate.Text);
                SqlCommand_InsertInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@sReferenceNumber", TextBox_InsertRDReferenceNumber.Text);
                SqlCommand_InsertInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@bSelected", CheckBox_InsertRD.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_ReportableDisease);
              }
              //END InfectionPrevention_Site_ReportableDisease


              //START InfectionPrevention_Site_PredisposingCondition
              CheckBox CheckBox_InsertPCCF1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF1");
              CheckBox CheckBox_InsertPCCF2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF2");
              CheckBox CheckBox_InsertPCCF3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF3");
              CheckBox CheckBox_InsertPCCF4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF4");
              CheckBox CheckBox_InsertPCCF5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF5");
              CheckBox CheckBox_InsertPCCF6 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF6");
              CheckBox CheckBox_InsertPCCF7 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF7");
              CheckBox CheckBox_InsertPCCF8 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF8");
              CheckBox CheckBox_InsertPCCF9 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF9");
              CheckBox CheckBox_InsertPCCF10 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF10");
              CheckBox CheckBox_InsertPCCF11 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF11");
              CheckBox CheckBox_InsertPCCF12 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF12");
              CheckBox CheckBox_InsertPCCF13 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF13");
              CheckBox CheckBox_InsertPCCF14 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF14");
              CheckBox CheckBox_InsertPCCF15 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertPCCF15");

              TextBox TextBox_InsertPCCF1 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF1");
              TextBox TextBox_InsertPCCF2 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF2");
              TextBox TextBox_InsertPCCF3 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF3");
              TextBox TextBox_InsertPCCF4 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF4");
              TextBox TextBox_InsertPCCF5 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF5");
              TextBox TextBox_InsertPCCF6 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF6");
              TextBox TextBox_InsertPCCF7 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF7");
              TextBox TextBox_InsertPCCF8 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF8");
              TextBox TextBox_InsertPCCF9 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF9");
              TextBox TextBox_InsertPCCF10 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF10");
              TextBox TextBox_InsertPCCF11 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF11");
              TextBox TextBox_InsertPCCF12 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF12");
              TextBox TextBox_InsertPCCF13 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF13");
              TextBox TextBox_InsertPCCF14 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF14");
              TextBox TextBox_InsertPCCF15 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPCCF15");

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition1 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition1 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition1))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition1.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition1.Parameters.AddWithValue("@fkiConditionID", 1163);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition1.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF1.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition1.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF1.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition1);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition2 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition2 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition2))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition2.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition2.Parameters.AddWithValue("@fkiConditionID", 1164);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition2.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF2.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition2.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF2.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition2);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition3 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition3 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition3))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition3.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition3.Parameters.AddWithValue("@fkiConditionID", 1156);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition3.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF3.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition3.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF3.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition3);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition4 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition4 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition4))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition4.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition4.Parameters.AddWithValue("@fkiConditionID", 1159);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition4.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF4.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition4.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF4.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition4);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition5 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition5 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition5))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition5.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition5.Parameters.AddWithValue("@fkiConditionID", 1158);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition5.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF5.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition5.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF5.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition5);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition6 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition6 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition6))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition6.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition6.Parameters.AddWithValue("@fkiConditionID", 1154);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition6.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF6.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition6.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF6.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition6);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition7 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition7 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition7))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition7.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition7.Parameters.AddWithValue("@fkiConditionID", 1155);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition7.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF7.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition7.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF7.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition7);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition8 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition8 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition8))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition8.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition8.Parameters.AddWithValue("@fkiConditionID", 2738);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition8.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF8.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition8.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF8.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition8);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition9 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition9 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition9))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition9.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition9.Parameters.AddWithValue("@fkiConditionID", 2739);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition9.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF9.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition9.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF9.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition9);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition10 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition10 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition10))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition10.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition10.Parameters.AddWithValue("@fkiConditionID", 1161);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition10.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF10.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition10.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF10.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition10);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition11 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition11 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition11))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition11.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition11.Parameters.AddWithValue("@fkiConditionID", 1152);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition11.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF11.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition11.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF11.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition11);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition12 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition12 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition12))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition12.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition12.Parameters.AddWithValue("@fkiConditionID", 1153);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition12.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF12.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition12.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF12.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition12);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition13 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition13 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition13))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition13.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition13.Parameters.AddWithValue("@fkiConditionID", 1162);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition13.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF13.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition13.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF13.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition13);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition14 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition14 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition14))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition14.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition14.Parameters.AddWithValue("@fkiConditionID", 1157);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition14.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF14.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition14.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF14.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition14);
              }

              string SQLStringInsertInfectionPrevention_Site_PredisposingCondition15 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
              using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition15 = new SqlCommand(SQLStringInsertInfectionPrevention_Site_PredisposingCondition15))
              {
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition15.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition15.Parameters.AddWithValue("@fkiConditionID", 1160);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition15.Parameters.AddWithValue("@sDescription", TextBox_InsertPCCF15.Text);
                SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition15.Parameters.AddWithValue("@bSelected", CheckBox_InsertPCCF15.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_PredisposingCondition15);
              }
              //END InfectionPrevention_Site_PredisposingCondition


              //START InfectionPrevention_Site_LabReport
              GridView GridView_InsertInfectionPrevention_LabReport = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_InsertInfectionPrevention_LabReport");

              for (int i = 0; i < GridView_InsertInfectionPrevention_LabReport.Rows.Count; i++)
              {
                CheckBox CheckBox_InsertLabReport = (CheckBox)GridView_InsertInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("CheckBox_InsertLabReport");
                Label Label_InsertLabReport_Date = (Label)GridView_InsertInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("Label_InsertLabReport_Date");
                Label Label_InsertLabReport_Specimen = (Label)GridView_InsertInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("Label_InsertLabReport_Specimen");
                Label Label_InsertLabReport_Organism = (Label)GridView_InsertInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("Label_InsertLabReport_Organism");
                Label Label_InsertLabReport_LabNumber = (Label)GridView_InsertInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("Label_InsertLabReport_LabNumber");

                string SQLStringInsertInfectionPrevention_Site_LabReport = "INSERT INTO tblInfectionPrevention_Site_LabReport ( fkiSiteID ,sLabDate ,sSpecimen ,sOrganism ,sLabNumber ,bSelected ) VALUES ( @fkiSiteID ,@sLabDate ,@sSpecimen ,@sOrganism ,@sLabNumber ,@bSelected )";
                using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_LabReport = new SqlCommand(SQLStringInsertInfectionPrevention_Site_LabReport))
                {
                  SqlCommand_InsertInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_InsertInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@sLabDate", Label_InsertLabReport_Date.Text);
                  SqlCommand_InsertInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@sSpecimen", Label_InsertLabReport_Specimen.Text);
                  SqlCommand_InsertInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@sOrganism", Label_InsertLabReport_Organism.Text);
                  SqlCommand_InsertInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@sLabNumber", Label_InsertLabReport_LabNumber.Text);
                  SqlCommand_InsertInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@bSelected", CheckBox_InsertLabReport.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_LabReport);
                }
              }
              //END InfectionPrevention_Site_LabReport


              //START InfectionPrevention_Site_BedHistory
              GridView GridView_InsertInfectionPrevention_BedHistory = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_InsertInfectionPrevention_BedHistory");
              string LastToUnit = "";

              for (int i = 0; i < GridView_InsertInfectionPrevention_BedHistory.Rows.Count; i++)
              {
                CheckBox CheckBox_InsertBedHistory = (CheckBox)GridView_InsertInfectionPrevention_BedHistory.Rows[i].Cells[0].FindControl("CheckBox_InsertBedHistory");
                Label Label_InsertBedHistory_To = (Label)GridView_InsertInfectionPrevention_BedHistory.Rows[i].Cells[0].FindControl("Label_InsertBedHistory_To");
                Label Label_InsertBedHistory_Date = (Label)GridView_InsertInfectionPrevention_BedHistory.Rows[i].Cells[0].FindControl("Label_InsertBedHistory_Date");

                string SQLStringInsertInfectionPrevention_Site_BedHistory = "INSERT INTO tblInfectionPrevention_Site_BedHistory ( fkiSiteID ,sFromUnit ,sToUnit ,sDateTransferred ,bSelected ) VALUES ( @fkiSiteID ,@sFromUnit ,@sToUnit ,@sDateTransferred ,@bSelected )";
                using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_BedHistory = new SqlCommand(SQLStringInsertInfectionPrevention_Site_BedHistory))
                {
                  SqlCommand_InsertInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_InsertInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@sFromUnit", LastToUnit);
                  SqlCommand_InsertInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@sToUnit", Label_InsertBedHistory_To.Text);
                  SqlCommand_InsertInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@sDateTransferred", Label_InsertBedHistory_Date.Text);
                  SqlCommand_InsertInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@bSelected", CheckBox_InsertBedHistory.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_BedHistory);
                }

                LastToUnit = Label_InsertBedHistory_To.Text;
              }
              //END InfectionPrevention_Site_BedHistory


              //START InfectionPrevention_Site_Surgery
              GridView GridView_InsertInfectionPrevention_Surgery = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_InsertInfectionPrevention_Surgery");

              for (int i = 0; i < GridView_InsertInfectionPrevention_Surgery.Rows.Count; i++)
              {
                CheckBox CheckBox_InsertSurgery = (CheckBox)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("CheckBox_InsertSurgery");
                Label Label_InsertSurgery_FacilityName = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_FacilityName");
                Label Label_InsertSurgery_DateOfAdmission = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_DateOfAdmission");
                Label Label_InsertSurgery_TheatreInvoice = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_TheatreInvoice");
                Label Label_InsertSurgery_Surgeon = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_Surgeon");
                Label Label_InsertSurgery_Procedure = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_Procedure");
                Label Label_InsertSurgery_VisitNumber = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_VisitNumber");
                Label Label_InsertSurgery_DischargeDate = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_DischargeDate");
                Label Label_InsertSurgery_Theatre = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_Theatre");
                Label Label_InsertSurgery_Anesthetist = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_Anesthetist");
                Label Label_InsertSurgery_ScrubNurse = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_ScrubNurse");
                Label Label_InsertSurgery_FinalDiagnosis = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_FinalDiagnosis");
                Label Label_InsertSurgery_Date = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_Date");
                Label Label_InsertSurgery_Duration = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_Duration");
                Label Label_InsertSurgery_AssistantSurgeon = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_AssistantSurgeon");
                Label Label_InsertSurgery_WoundCategory = (Label)GridView_InsertInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_InsertSurgery_WoundCategory");

                string SQLStringInsertInfectionPrevention_Site_Surgery = "INSERT INTO tblInfectionPrevention_Site_Surgery ( fkiSiteID ,sFacility ,sVisitNumber ,sProcedure ,sSurgeryDate ,sSurgeryDuration ,sTheatre ,sTheatreInvoice ,sSurgeon ,sAssistant ,sScrubNurse ,sAnaesthesist ,sWound ,sCategory ,sFinalDiagnosis ,sAdmissionDate ,sDischargeDate ,bSelected ) VALUES ( @fkiSiteID ,@sFacility ,@sVisitNumber ,@sProcedure ,@sSurgeryDate ,@sSurgeryDuration ,@sTheatre ,@sTheatreInvoice ,@sSurgeon ,@sAssistant ,@sScrubNurse ,@sAnaesthesist ,@sWound ,@sCategory ,@sFinalDiagnosis ,@sAdmissionDate ,@sDischargeDate ,@bSelected )";
                using (SqlCommand SqlCommand_InsertInfectionPrevention_Site_Surgery = new SqlCommand(SQLStringInsertInfectionPrevention_Site_Surgery))
                {
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sFacility", Label_InsertSurgery_FacilityName.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sVisitNumber", Label_InsertSurgery_VisitNumber.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sProcedure", Label_InsertSurgery_Procedure.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sSurgeryDate", Label_InsertSurgery_Date.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sSurgeryDuration", Label_InsertSurgery_Duration.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sTheatre", Label_InsertSurgery_Theatre.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sTheatreInvoice", Label_InsertSurgery_TheatreInvoice.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sSurgeon", Label_InsertSurgery_Surgeon.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sAssistant", Label_InsertSurgery_AssistantSurgeon.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sScrubNurse", Label_InsertSurgery_ScrubNurse.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sAnaesthesist", Label_InsertSurgery_Anesthetist.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sWound", Label_InsertSurgery_WoundCategory.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sCategory", DBNull.Value);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sFinalDiagnosis", Label_InsertSurgery_FinalDiagnosis.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sAdmissionDate", Label_InsertSurgery_DateOfAdmission.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sDischargeDate", Label_InsertSurgery_DischargeDate.Text);
                  SqlCommand_InsertInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@bSelected", CheckBox_InsertSurgery.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_Site_Surgery);
                }
              }
              //END InfectionPrevention_Site_Surgery
            }
          }

          Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Report Number", "InfoQuest_ReportNumber.aspx?ReportPage=Form_HAI&ReportNumber=" + sReportNumber + ""), false);
        }
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      DropDownList DropDownList_InsertFacility = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertFacility");
      TextBox TextBox_InsertPatientVisitNumber = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertPatientVisitNumber");
      HiddenField HiddenField_InsertValidIMedsData = (HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_InsertValidIMedsData");
      DropDownList DropDownList_InsertUnitId = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertUnitId");
      TextBox TextBox_InsertDateInfectionReported = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDateInfectionReported");
      DropDownList DropDownList_InsertInfectionType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertInfectionType");
      DropDownList DropDownList_InsertSSISubType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertSSISubType");
      RadioButtonList RadioButtonList_InsertSeverity = (RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_InsertSeverity");
      TextBox TextBox_InsertDescription = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDescription");
      TextBox TextBox_InsertDays = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDays");

      CheckBox CheckBox_InsertInvestigationCompleted = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertInvestigationCompleted");
      TextBox TextBox_InsertInvestigationDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationDate");
      TextBox TextBox_InsertInvestigationName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationName");
      TextBox TextBox_InsertInvestigationDesignation = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationDesignation");
      TextBox TextBox_InsertInvestigationIPCSName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationIPCSName");
      TextBox TextBox_InsertInvestigationTeamMembers = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationTeamMembers");
      TextBox TextBox_InsertInvestigationCompletedDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationCompletedDate");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(DropDownList_InsertFacility.SelectedValue))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertPatientVisitNumber.Text))
        {
          InvalidForm = "Yes";
        }

        if (HiddenField_InsertValidIMedsData.Value == "Yes")
        {
          if (string.IsNullOrEmpty(DropDownList_InsertUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(TextBox_InsertDateInfectionReported.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_InsertInfectionType.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_InsertInfectionType.SelectedValue == "1131")
          {
            if (string.IsNullOrEmpty(DropDownList_InsertSSISubType.SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }

          if (string.IsNullOrEmpty(RadioButtonList_InsertSeverity.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(TextBox_InsertDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_InsertInfectionType.SelectedValue == "1131" || DropDownList_InsertInfectionType.SelectedValue == "1135" || DropDownList_InsertInfectionType.SelectedValue == "1137" || DropDownList_InsertInfectionType.SelectedValue == "1139")
          {
            if (string.IsNullOrEmpty(TextBox_InsertDays.Text))
            {
              InvalidForm = "Yes";
            }
          }


          if (CheckBox_InsertInvestigationCompleted.Checked == true)
          {
            if (string.IsNullOrEmpty(TextBox_InsertInvestigationDate.Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(TextBox_InsertInvestigationName.Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(TextBox_InsertInvestigationDesignation.Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(TextBox_InsertInvestigationIPCSName.Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(TextBox_InsertInvestigationTeamMembers.Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(TextBox_InsertInvestigationCompletedDate.Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
        else
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
        if (HiddenField_InsertValidIMedsData.Value == "Yes")
        {
          string DateToValidateDateReceived = TextBox_InsertDateInfectionReported.Text.ToString();
          DateTime ValidatedDateDateReceived = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDateReceived);

          if (ValidatedDateDateReceived.ToString() == "0001/01/01 12:00:00 AM")
          {
            InvalidFormMessage = InvalidFormMessage + "Date Infection Reported is not in the correct format, date must be in the format yyyy/mm/dd<br />";
          }
          else
          {
            DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertDateInfectionReported")).Text, CultureInfo.CurrentCulture);
            DateTime CurrentDate = DateTime.Now;

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
            }
          }
        }

        if (CheckBox_InsertInvestigationCompleted.Checked == true)
        {
          string DateToValidateDateReceived = TextBox_InsertInvestigationDate.Text.ToString();
          DateTime ValidatedDateDateReceived = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDateReceived);

          if (ValidatedDateDateReceived.ToString() == "0001/01/01 12:00:00 AM")
          {
            InvalidFormMessage = InvalidFormMessage + "Investigation Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
          }
          else
          {
            DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationDate")).Text, CultureInfo.CurrentCulture);
            DateTime CurrentDate = DateTime.Now;

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
            }
          }
        }

        if (CheckBox_InsertInvestigationCompleted.Checked == true)
        {
          string DateToValidateDateReceived = TextBox_InsertInvestigationCompletedDate.Text.ToString();
          DateTime ValidatedDateDateReceived = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDateReceived);

          if (ValidatedDateDateReceived.ToString() == "0001/01/01 12:00:00 AM")
          {
            InvalidFormMessage = InvalidFormMessage + "Investigation Completed Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
          }
          else
          {
            DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationCompletedDate")).Text, CultureInfo.CurrentCulture);
            DateTime CurrentDate = DateTime.Now;

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
            }
          }
        }
      }

      return InvalidFormMessage;
    }
    //---END--- --Insert--//


    //--START-- --Edit--//
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1809:AvoidExcessiveLocals")]
    protected void FormView_InfectionPrevention_Form_ItemUpdating(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_EditInvalidFormMessage = EditValidation();

        if (!string.IsNullOrEmpty(Label_EditInvalidFormMessage))
        {
          e.Cancel = true;
          ToolkitScriptManager_InfectionPrevention.SetFocus(UpdatePanel_InfectionPrevention);
          ((Label)FormView_InfectionPrevention_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
          ((Label)FormView_InfectionPrevention_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";

          EditRegisterPostBackControl();
        }
        else if (string.IsNullOrEmpty(Label_EditInvalidFormMessage))
        {
          e.Cancel = false;

          //START InfectionPrevention
          TextBox TextBox_EditInvestigationDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDate");
          TextBox TextBox_EditInvestigationName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationName");
          TextBox TextBox_EditInvestigationDesignation = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDesignation");
          TextBox TextBox_EditInvestigationIPCSName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationIPCSName");
          TextBox TextBox_EditInvestigationTeamMembers = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationTeamMembers");
          CheckBox CheckBox_EditInvestigationCompleted = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditInvestigationCompleted");
          TextBox TextBox_EditInvestigationCompletedDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationCompletedDate");

          string SQLStringEditInfectionPrevention = "UPDATE tblInfectionPrevention SET dtDateOfInvestigation = @dtDateOfInvestigation,sInvestigatorName = @sInvestigatorName,sInvestigatorDesignation = @sInvestigatorDesignation ,sIPCSName = @sIPCSName ,sTeamMembers = @sTeamMembers ,bInvestigationCompleted = @bInvestigationCompleted ,dtInvestigationCompleted = @dtInvestigationCompleted , dtModified = @dtModified ,sModifiedBy = @sModifiedBy WHERE pkiInfectionFormID = @pkiInfectionFormID";
          using (SqlCommand SqlCommand_EditInfectionPrevention = new SqlCommand(SQLStringEditInfectionPrevention))
          {

            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@dtDateOfInvestigation", TextBox_EditInvestigationDate.Text);
            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sInvestigatorName", TextBox_EditInvestigationName.Text);
            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sInvestigatorDesignation", TextBox_EditInvestigationDesignation.Text);
            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sIPCSName", TextBox_EditInvestigationIPCSName.Text);
            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sTeamMembers", TextBox_EditInvestigationTeamMembers.Text);
            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@bInvestigationCompleted", CheckBox_EditInvestigationCompleted.Checked.ToString());
            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@dtInvestigationCompleted", TextBox_EditInvestigationCompletedDate.Text);
            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@dtModified", DateTime.Now);
            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sModifiedBy", Request.ServerVariables["LOGON_USER"]);
            SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@pkiInfectionFormID", Request.QueryString["InfectionFormID"]);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention);
          }
          //END InfectionPrevention


          //START InfectionPrevention_VisitDiagnosis
          CheckBoxList CheckBoxList_EditVisitDiagnosis = (CheckBoxList)FormView_InfectionPrevention_Form.FindControl("CheckBoxList_EditVisitDiagnosis");
          for (int i = 0; i < CheckBoxList_EditVisitDiagnosis.Items.Count; i++)
          {
            string sCode = "";
            string sDescription = "";
            string VisitDiagnosis = CheckBoxList_EditVisitDiagnosis.Items[i].Text;
            Boolean VisitDiagnosisSelected = CheckBoxList_EditVisitDiagnosis.Items[i].Selected;

            string[] Split = VisitDiagnosis.Split("|".ToCharArray(), StringSplitOptions.None);
            sCode = Split[0];
            sCode = sCode.Trim();
            sDescription = Split[1];
            sDescription = sDescription.Trim();

            string SQLStringInsertInfectionPrevention_VisitDiagnosis = "UPDATE tblInfectionPrevention_VisitDiagnosis SET bSelected = @bSelected WHERE sCode = @sCode AND sDescription = @sDescription AND fkiInfectionFormID = @fkiInfectionFormID";
            using (SqlCommand SqlCommand_InsertInfectionPrevention_VisitDiagnosis = new SqlCommand(SQLStringInsertInfectionPrevention_VisitDiagnosis))
            {
              SqlCommand_InsertInfectionPrevention_VisitDiagnosis.Parameters.AddWithValue("@bSelected", VisitDiagnosisSelected);
              SqlCommand_InsertInfectionPrevention_VisitDiagnosis.Parameters.AddWithValue("@sCode", sCode);
              SqlCommand_InsertInfectionPrevention_VisitDiagnosis.Parameters.AddWithValue("@sDescription", sDescription);
              SqlCommand_InsertInfectionPrevention_VisitDiagnosis.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertInfectionPrevention_VisitDiagnosis);
            }
          }
          //END InfectionPrevention_VisitDiagnosis


          //START InfectionPrevention_PrecautionaryMeasure
          CheckBox CheckBox_EditPIM1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM1");
          CheckBox CheckBox_EditPIM2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM2");
          CheckBox CheckBox_EditPIM3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM3");
          CheckBox CheckBox_EditPIM4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM4");
          CheckBox CheckBox_EditPIM5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM5");
          CheckBox CheckBox_EditPIM6 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM6");

          string SQLStringEditInfectionPrevention_PrecautionaryMeasure1 = "UPDATE tblInfectionPrevention_PrecautionaryMeasure SET bSelected = @bSelected WHERE fkiPrecautionaryMeasureID = @fkiPrecautionaryMeasureID AND fkiInfectionFormID = @fkiInfectionFormID";
          using (SqlCommand SqlCommand_EditInfectionPrevention_PrecautionaryMeasure1 = new SqlCommand(SQLStringEditInfectionPrevention_PrecautionaryMeasure1))
          {
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure1.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure1.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1201);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure1.Parameters.AddWithValue("@bSelected", CheckBox_EditPIM1.Checked);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_PrecautionaryMeasure1);
          }

          string SQLStringEditInfectionPrevention_PrecautionaryMeasure2 = "UPDATE tblInfectionPrevention_PrecautionaryMeasure SET bSelected = @bSelected WHERE fkiPrecautionaryMeasureID = @fkiPrecautionaryMeasureID AND fkiInfectionFormID = @fkiInfectionFormID";
          using (SqlCommand SqlCommand_EditInfectionPrevention_PrecautionaryMeasure2 = new SqlCommand(SQLStringEditInfectionPrevention_PrecautionaryMeasure2))
          {
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure2.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure2.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1202);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure2.Parameters.AddWithValue("@bSelected", CheckBox_EditPIM2.Checked);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_PrecautionaryMeasure2);
          }

          string SQLStringEditInfectionPrevention_PrecautionaryMeasure3 = "UPDATE tblInfectionPrevention_PrecautionaryMeasure SET bSelected = @bSelected WHERE fkiPrecautionaryMeasureID = @fkiPrecautionaryMeasureID AND fkiInfectionFormID = @fkiInfectionFormID";
          using (SqlCommand SqlCommand_EditInfectionPrevention_PrecautionaryMeasure3 = new SqlCommand(SQLStringEditInfectionPrevention_PrecautionaryMeasure3))
          {
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure3.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure3.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1203);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure3.Parameters.AddWithValue("@bSelected", CheckBox_EditPIM3.Checked);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_PrecautionaryMeasure3);
          }

          string SQLStringEditInfectionPrevention_PrecautionaryMeasure4 = "UPDATE tblInfectionPrevention_PrecautionaryMeasure SET bSelected = @bSelected WHERE fkiPrecautionaryMeasureID = @fkiPrecautionaryMeasureID AND fkiInfectionFormID = @fkiInfectionFormID";
          using (SqlCommand SqlCommand_EditInfectionPrevention_PrecautionaryMeasure4 = new SqlCommand(SQLStringEditInfectionPrevention_PrecautionaryMeasure4))
          {
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure4.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure4.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 2735);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure4.Parameters.AddWithValue("@bSelected", CheckBox_EditPIM4.Checked);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_PrecautionaryMeasure4);
          }

          string SQLStringEditInfectionPrevention_PrecautionaryMeasure5 = "UPDATE tblInfectionPrevention_PrecautionaryMeasure SET bSelected = @bSelected WHERE fkiPrecautionaryMeasureID = @fkiPrecautionaryMeasureID AND fkiInfectionFormID = @fkiInfectionFormID";
          using (SqlCommand SqlCommand_EditInfectionPrevention_PrecautionaryMeasure5 = new SqlCommand(SQLStringEditInfectionPrevention_PrecautionaryMeasure5))
          {
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure5.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure5.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1205);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure5.Parameters.AddWithValue("@bSelected", CheckBox_EditPIM5.Checked);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_PrecautionaryMeasure5);
          }

          string SQLStringEditInfectionPrevention_PrecautionaryMeasure6 = "UPDATE tblInfectionPrevention_PrecautionaryMeasure SET bSelected = @bSelected WHERE fkiPrecautionaryMeasureID = @fkiPrecautionaryMeasureID AND fkiInfectionFormID = @fkiInfectionFormID";
          using (SqlCommand SqlCommand_EditInfectionPrevention_PrecautionaryMeasure6 = new SqlCommand(SQLStringEditInfectionPrevention_PrecautionaryMeasure6))
          {
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure6.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure6.Parameters.AddWithValue("@fkiPrecautionaryMeasureID", 1204);
            SqlCommand_EditInfectionPrevention_PrecautionaryMeasure6.Parameters.AddWithValue("@bSelected", CheckBox_EditPIM6.Checked);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_PrecautionaryMeasure6);
          }
          //END InfectionPrevention_PrecautionaryMeasure


          //START InfectionPrevention_Site
          RadioButtonList RadioButtonList_EditCompliance = (RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_EditCompliance");
          CheckBox CheckBox_EditRiskEnteralFeeding = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditRiskEnteralFeeding");
          CheckBox CheckBox_EditRiskTPN = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditRiskTPN");
          DropDownList DropDownList_EditUnitId = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditUnitId");
          DropDownList DropDownList_EditInfectionType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditInfectionType");
          DropDownList DropDownList_EditSSISubType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditSSISubType");
          RadioButtonList RadioButtonList_EditSeverity = (RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_EditSeverity");
          TextBox TextBox_EditDateInfectionReported = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDateInfectionReported");
          TextBox TextBox_EditDescription = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDescription");
          TextBox TextBox_EditDays = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDays");

          string SQLStringEditInfectionPrevention_Site = "UPDATE tblInfectionPrevention_Site SET fkiFacilityUnitID = @fkiFacilityUnitID ,fkiInfectionTypeID = @fkiInfectionTypeID ,fkiInfectionSubTypeID = @fkiInfectionSubTypeID ,fkiSeverityTypeID = @fkiSeverityTypeID ,fkiBundleComplianceID = @fkiBundleComplianceID ,dtReported = @dtReported ,sDescription = @sDescription ,sRelatedHighRiskProcedures = @sRelatedHighRiskProcedures ,sInfectionDays = @sInfectionDays WHERE fkiInfectionFormID = @fkiInfectionFormID";
          Int32 BundleComplianceID = 0;
          if (RadioButtonList_EditCompliance.SelectedValue == "1")
          {
            BundleComplianceID = 1;
          }
          else if (RadioButtonList_EditCompliance.SelectedValue == "2")
          {
            BundleComplianceID = 2;
          }
          else
          {
            BundleComplianceID = 3;
          }

          string RelatedHighRiskProcedures = "";
          if (CheckBox_EditRiskEnteralFeeding.Checked == true && CheckBox_EditRiskTPN.Checked == true)
          {
            RelatedHighRiskProcedures = "Enteral Feeding, TPN";
          }
          else
          {
            if (CheckBox_EditRiskEnteralFeeding.Checked == true)
            {
              RelatedHighRiskProcedures = "Enteral Feeding";
            }

            if (CheckBox_EditRiskTPN.Checked == true)
            {
              RelatedHighRiskProcedures = "TPN";
            }
          }

          using (SqlCommand SqlCommand_EditInfectionPrevention_Site = new SqlCommand(SQLStringEditInfectionPrevention_Site))
          {
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@fkiFacilityUnitID", DropDownList_EditUnitId.SelectedValue);
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@fkiInfectionTypeID", DropDownList_EditInfectionType.SelectedValue);
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@fkiInfectionSubTypeID", DropDownList_EditSSISubType.SelectedValue);
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@fkiSeverityTypeID", RadioButtonList_EditSeverity.SelectedValue);
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@fkiBundleComplianceID", BundleComplianceID);
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@dtReported", TextBox_EditDateInfectionReported.Text);
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@sDescription", TextBox_EditDescription.Text);
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@sRelatedHighRiskProcedures", RelatedHighRiskProcedures);
            SqlCommand_EditInfectionPrevention_Site.Parameters.AddWithValue("@sInfectionDays", TextBox_EditDays.Text);
            InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site);
          }
          //END InfectionPrevention_Site

          string LastpkiSiteID = "";
          string SQLStringInfectionPrevention = "SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID";
          using (SqlCommand SqlCommand_InfectionPrevention = new SqlCommand(SQLStringInfectionPrevention))
          {
            SqlCommand_InfectionPrevention.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
            DataTable DataTable_InfectionPrevention;
            using (DataTable_InfectionPrevention = new DataTable())
            {
              DataTable_InfectionPrevention.Locale = CultureInfo.CurrentCulture;
              DataTable_InfectionPrevention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention).Copy();
              if (DataTable_InfectionPrevention.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_InfectionPrevention.Rows)
                {
                  LastpkiSiteID = DataRow_Row["pkiSiteID"].ToString();
                }
              }
            }
          }

          if (!string.IsNullOrEmpty(LastpkiSiteID))
          {
            //START InfectionPrevention_Site_BundleComplianceItem
            string SQLStringDeleteInfectionPrevention_Site_BundleComplianceItem = "DELETE FROM tblInfectionPrevention_Site_BundleComplianceItem WHERE fkiSiteID = @fkiSiteID";
            using (SqlCommand SqlCommand_DeleteInfectionPrevention_Site_BundleComplianceItem = new SqlCommand(SQLStringDeleteInfectionPrevention_Site_BundleComplianceItem))
            {
              SqlCommand_DeleteInfectionPrevention_Site_BundleComplianceItem.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteInfectionPrevention_Site_BundleComplianceItem);
            }

            
            CheckBox CheckBox_EditTypeSSI1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI1");
            CheckBox CheckBox_EditTypeSSI2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI2");
            CheckBox CheckBox_EditTypeSSI3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI3");
            CheckBox CheckBox_EditTypeSSI4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI4");
            CheckBox CheckBox_EditTypeCLABSI1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI1");
            CheckBox CheckBox_EditTypeCLABSI2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI2");
            CheckBox CheckBox_EditTypeCLABSI3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI3");
            CheckBox CheckBox_EditTypeCLABSI4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI4");
            CheckBox CheckBox_EditTypeCLABSI5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI5");
            CheckBox CheckBox_EditTypeCLABSI6 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI6");
            CheckBox CheckBox_EditTypeCLABSI7 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI7");
            CheckBox CheckBox_EditTypeVAP1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP1");
            CheckBox CheckBox_EditTypeVAP2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP2");
            CheckBox CheckBox_EditTypeVAP3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP3");
            CheckBox CheckBox_EditTypeVAP4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP4");
            CheckBox CheckBox_EditTypeVAP5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP5");
            CheckBox CheckBox_EditTypeCAUTI1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI1");
            CheckBox CheckBox_EditTypeCAUTI2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI2");
            CheckBox CheckBox_EditTypeCAUTI3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI3");
            CheckBox CheckBox_EditTypeCAUTI4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI4");

            if (DropDownList_EditInfectionType.SelectedValue == "1131" || DropDownList_EditInfectionType.SelectedValue == "1135" || DropDownList_EditInfectionType.SelectedValue == "1137" || DropDownList_EditInfectionType.SelectedValue == "1139")
            {
              if (DropDownList_EditInfectionType.SelectedValue == "1131")
              {
                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem11 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem11 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem11))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem11.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem11.Parameters.AddWithValue("@fkiBundleItemTypeID", 1206);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem11.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeSSI1.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem11);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem12 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem12 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem12))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem12.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem12.Parameters.AddWithValue("@fkiBundleItemTypeID", 1207);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem12.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeSSI2.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem12);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem13 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem13 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem13))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem13.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem13.Parameters.AddWithValue("@fkiBundleItemTypeID", 1208);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem13.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeSSI3.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem13);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem14 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem14 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem14))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem14.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem14.Parameters.AddWithValue("@fkiBundleItemTypeID", 1209);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem14.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeSSI4.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem14);
                }
              }

              if (DropDownList_EditInfectionType.SelectedValue == "1137")
              {
                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem21 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem21 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem21))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem21.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem21.Parameters.AddWithValue("@fkiBundleItemTypeID", 2682);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem21.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCLABSI1.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem21);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem22 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem22 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem22))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem22.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem22.Parameters.AddWithValue("@fkiBundleItemTypeID", 2683);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem22.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCLABSI2.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem22);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem23 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem23 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem23))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem23.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem23.Parameters.AddWithValue("@fkiBundleItemTypeID", 2684);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem23.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCLABSI3.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem23);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem24 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem24 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem24))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem24.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem24.Parameters.AddWithValue("@fkiBundleItemTypeID", 2685);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem24.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCLABSI4.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem24);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem25 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem25 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem25))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem25.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem25.Parameters.AddWithValue("@fkiBundleItemTypeID", 2686);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem25.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCLABSI5.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem25);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem26 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem26 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem26))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem26.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem26.Parameters.AddWithValue("@fkiBundleItemTypeID", 2687);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem26.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCLABSI6.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem26);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem27 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem27 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem27))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem27.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem27.Parameters.AddWithValue("@fkiBundleItemTypeID", 2688);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem27.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCLABSI7.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem27);
                }
              }

              if (DropDownList_EditInfectionType.SelectedValue == "1135")
              {
                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem31 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem31 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem31))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem31.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem31.Parameters.AddWithValue("@fkiBundleItemTypeID", 1222);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem31.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeVAP1.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem31);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem32 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem32 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem32))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem32.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem32.Parameters.AddWithValue("@fkiBundleItemTypeID", 1223);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem32.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeVAP2.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem32);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem33 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem33 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem33))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem33.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem33.Parameters.AddWithValue("@fkiBundleItemTypeID", 1224);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem33.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeVAP3.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem33);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem34 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem34 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem34))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem34.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem34.Parameters.AddWithValue("@fkiBundleItemTypeID", 1225);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem34.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeVAP4.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem34);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem35 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem35 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem35))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem35.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem35.Parameters.AddWithValue("@fkiBundleItemTypeID", 1226);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem35.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeVAP5.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem35);
                }
              }

              if (DropDownList_EditInfectionType.SelectedValue == "1139")
              {
                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem41 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem41 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem41))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem41.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem41.Parameters.AddWithValue("@fkiBundleItemTypeID", 1227);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem41.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCAUTI1.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem41);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem42 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem42 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem42))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem42.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem42.Parameters.AddWithValue("@fkiBundleItemTypeID", 1228);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem42.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCAUTI2.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem42);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem43 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem43 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem43))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem43.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem43.Parameters.AddWithValue("@fkiBundleItemTypeID", 1230);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem43.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCAUTI3.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem43);
                }

                string SQLStringEditInfectionPrevention_Site_BundleComplianceItem44 = "INSERT INTO tblInfectionPrevention_Site_BundleComplianceItem ( fkiSiteID ,fkiBundleItemTypeID ,bSelected ) VALUES ( @fkiSiteID ,@fkiBundleItemTypeID ,@bSelected )";
                using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem44 = new SqlCommand(SQLStringEditInfectionPrevention_Site_BundleComplianceItem44))
                {
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem44.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem44.Parameters.AddWithValue("@fkiBundleItemTypeID", 1231);
                  SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem44.Parameters.AddWithValue("@bSelected", CheckBox_EditTypeCAUTI4.Checked);
                  InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BundleComplianceItem44);
                }
              }
            }
            //END InfectionPrevention_Site_BundleComplianceItem


            //START InfectionPrevention_Site_ReportableDisease
            string SQLStringDeleteInfectionPrevention_Site_ReportableDisease = "DELETE FROM tblInfectionPrevention_Site_ReportableDisease WHERE fkiSiteID = @fkiSiteID";
            using (SqlCommand SqlCommand_DeleteInfectionPrevention_Site_ReportableDisease = new SqlCommand(SQLStringDeleteInfectionPrevention_Site_ReportableDisease))
            {
              SqlCommand_DeleteInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteInfectionPrevention_Site_ReportableDisease);
            }


            CheckBox CheckBox_EditRD = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditRD");
            DropDownList DropDownList_EditRDNotifiableDisease = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditRDNotifiableDisease");
            TextBox TextBox_EditRDDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditRDDate");
            TextBox TextBox_EditRDReferenceNumber = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditRDReferenceNumber");

            string SQLStringEditInfectionPrevention_Site_ReportableDisease = "INSERT INTO tblInfectionPrevention_Site_ReportableDisease ( fkiSiteID , sReportedToDepartment , fkiDiseaseID , sDateReported , sReferenceNumber , bSelected ) VALUES ( @fkiSiteID , @sReportedToDepartment , @fkiDiseaseID , @sDateReported , @sReferenceNumber , @bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_ReportableDisease = new SqlCommand(SQLStringEditInfectionPrevention_Site_ReportableDisease))
            {
              SqlCommand_EditInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@sReportedToDepartment", "Department of Health");
              SqlCommand_EditInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@fkiDiseaseID", DropDownList_EditRDNotifiableDisease.SelectedValue);
              SqlCommand_EditInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@sDateReported", TextBox_EditRDDate.Text);
              SqlCommand_EditInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@sReferenceNumber", TextBox_EditRDReferenceNumber.Text);
              SqlCommand_EditInfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@bSelected", CheckBox_EditRD.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_ReportableDisease);
            }
            //END InfectionPrevention_Site_ReportableDisease


            //START InfectionPrevention_Site_LabReport
            string SQLStringDeleteInfectionPrevention_Site_LabReport = "DELETE FROM tblInfectionPrevention_Site_LabReport WHERE fkiSiteID = @fkiSiteID";
            using (SqlCommand SqlCommand_DeleteInfectionPrevention_Site_LabReport = new SqlCommand(SQLStringDeleteInfectionPrevention_Site_LabReport))
            {
              SqlCommand_DeleteInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteInfectionPrevention_Site_LabReport);
            }


            GridView GridView_EditInfectionPrevention_LabReport = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_EditInfectionPrevention_LabReport");

            for (int i = 0; i < GridView_EditInfectionPrevention_LabReport.Rows.Count; i++)
            {
              CheckBox CheckBox_EditLabReport = (CheckBox)GridView_EditInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("CheckBox_EditLabReport");
              Label Label_EditLabReport_Date = (Label)GridView_EditInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("Label_EditLabReport_Date");
              Label Label_EditLabReport_Specimen = (Label)GridView_EditInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("Label_EditLabReport_Specimen");
              Label Label_EditLabReport_Organism = (Label)GridView_EditInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("Label_EditLabReport_Organism");
              Label Label_EditLabReport_LabNumber = (Label)GridView_EditInfectionPrevention_LabReport.Rows[i].Cells[0].FindControl("Label_EditLabReport_LabNumber");

              string SQLStringEditInfectionPrevention_Site_LabReport = "INSERT INTO tblInfectionPrevention_Site_LabReport ( fkiSiteID ,sLabDate ,sSpecimen ,sOrganism ,sLabNumber ,bSelected ) VALUES ( @fkiSiteID ,@sLabDate ,@sSpecimen ,@sOrganism ,@sLabNumber ,@bSelected )";
              using (SqlCommand SqlCommand_EditInfectionPrevention_Site_LabReport = new SqlCommand(SQLStringEditInfectionPrevention_Site_LabReport))
              {
                SqlCommand_EditInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_EditInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@sLabDate", Label_EditLabReport_Date.Text);
                SqlCommand_EditInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@sSpecimen", Label_EditLabReport_Specimen.Text);
                SqlCommand_EditInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@sOrganism", Label_EditLabReport_Organism.Text);
                SqlCommand_EditInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@sLabNumber", Label_EditLabReport_LabNumber.Text);
                SqlCommand_EditInfectionPrevention_Site_LabReport.Parameters.AddWithValue("@bSelected", CheckBox_EditLabReport.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_LabReport);
              }
            }
            //END InfectionPrevention_Site_LabReport


            //START InfectionPrevention_Site_BedHistory
            string SQLStringDeleteInfectionPrevention_Site_BedHistory = "DELETE FROM tblInfectionPrevention_Site_BedHistory WHERE fkiSiteID = @fkiSiteID";
            using (SqlCommand SqlCommand_DeleteInfectionPrevention_Site_BedHistory = new SqlCommand(SQLStringDeleteInfectionPrevention_Site_BedHistory))
            {
              SqlCommand_DeleteInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteInfectionPrevention_Site_BedHistory);
            }


            GridView GridView_EditInfectionPrevention_BedHistory = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_EditInfectionPrevention_BedHistory");
            string LastToUnit = "";

            for (int i = 0; i < GridView_EditInfectionPrevention_BedHistory.Rows.Count; i++)
            {
              CheckBox CheckBox_EditBedHistory = (CheckBox)GridView_EditInfectionPrevention_BedHistory.Rows[i].Cells[0].FindControl("CheckBox_EditBedHistory");
              Label Label_EditBedHistory_To = (Label)GridView_EditInfectionPrevention_BedHistory.Rows[i].Cells[0].FindControl("Label_EditBedHistory_To");
              Label Label_EditBedHistory_Date = (Label)GridView_EditInfectionPrevention_BedHistory.Rows[i].Cells[0].FindControl("Label_EditBedHistory_Date");

              string SQLStringEditInfectionPrevention_Site_BedHistory = "INSERT INTO tblInfectionPrevention_Site_BedHistory ( fkiSiteID ,sFromUnit ,sToUnit ,sDateTransferred ,bSelected ) VALUES ( @fkiSiteID ,@sFromUnit ,@sToUnit ,@sDateTransferred ,@bSelected )";
              using (SqlCommand SqlCommand_EditInfectionPrevention_Site_BedHistory = new SqlCommand(SQLStringEditInfectionPrevention_Site_BedHistory))
              {
                SqlCommand_EditInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_EditInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@sFromUnit", LastToUnit);
                SqlCommand_EditInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@sToUnit", Label_EditBedHistory_To.Text);
                SqlCommand_EditInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@sDateTransferred", Label_EditBedHistory_Date.Text);
                SqlCommand_EditInfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@bSelected", CheckBox_EditBedHistory.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_BedHistory);
              }

              LastToUnit = Label_EditBedHistory_To.Text;
            }
            //END InfectionPrevention_Site_BedHistory


            //START InfectionPrevention_Site_Surgery
            string SQLStringDeleteInfectionPrevention_Site_Surgery = "DELETE FROM tblInfectionPrevention_Site_Surgery WHERE fkiSiteID = @fkiSiteID";
            using (SqlCommand SqlCommand_DeleteInfectionPrevention_Site_Surgery = new SqlCommand(SQLStringDeleteInfectionPrevention_Site_Surgery))
            {
              SqlCommand_DeleteInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteInfectionPrevention_Site_Surgery);
            }


            GridView GridView_EditInfectionPrevention_Surgery = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_EditInfectionPrevention_Surgery");

            for (int i = 0; i < GridView_EditInfectionPrevention_Surgery.Rows.Count; i++)
            {
              CheckBox CheckBox_EditSurgery = (CheckBox)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("CheckBox_EditSurgery");
              Label Label_EditSurgery_FacilityName = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_FacilityName");
              Label Label_EditSurgery_DateOfAdmission = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_DateOfAdmission");
              Label Label_EditSurgery_TheatreInvoice = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_TheatreInvoice");
              Label Label_EditSurgery_Surgeon = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_Surgeon");
              Label Label_EditSurgery_Procedure = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_Procedure");
              Label Label_EditSurgery_VisitNumber = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_VisitNumber");
              Label Label_EditSurgery_DischargeDate = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_DischargeDate");
              Label Label_EditSurgery_Theatre = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_Theatre");
              Label Label_EditSurgery_Anesthetist = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_Anesthetist");
              Label Label_EditSurgery_ScrubNurse = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_ScrubNurse");
              Label Label_EditSurgery_FinalDiagnosis = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_FinalDiagnosis");
              Label Label_EditSurgery_Date = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_Date");
              Label Label_EditSurgery_Duration = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_Duration");
              Label Label_EditSurgery_AssistantSurgeon = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_AssistantSurgeon");
              Label Label_EditSurgery_WoundCategory = (Label)GridView_EditInfectionPrevention_Surgery.Rows[i].Cells[0].FindControl("Label_EditSurgery_WoundCategory");

              string SQLStringEditInfectionPrevention_Site_Surgery = "INSERT INTO tblInfectionPrevention_Site_Surgery ( fkiSiteID ,sFacility ,sVisitNumber ,sProcedure ,sSurgeryDate ,sSurgeryDuration ,sTheatre ,sTheatreInvoice ,sSurgeon ,sAssistant ,sScrubNurse ,sAnaesthesist ,sWound ,sCategory ,sFinalDiagnosis ,sAdmissionDate ,sDischargeDate ,bSelected ) VALUES ( @fkiSiteID ,@sFacility ,@sVisitNumber ,@sProcedure ,@sSurgeryDate ,@sSurgeryDuration ,@sTheatre ,@sTheatreInvoice ,@sSurgeon ,@sAssistant ,@sScrubNurse ,@sAnaesthesist ,@sWound ,@sCategory ,@sFinalDiagnosis ,@sAdmissionDate ,@sDischargeDate ,@bSelected )";
              using (SqlCommand SqlCommand_EditInfectionPrevention_Site_Surgery = new SqlCommand(SQLStringEditInfectionPrevention_Site_Surgery))
              {
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sFacility", Label_EditSurgery_FacilityName.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sVisitNumber", Label_EditSurgery_VisitNumber.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sProcedure", Label_EditSurgery_Procedure.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sSurgeryDate", Label_EditSurgery_Date.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sSurgeryDuration", Label_EditSurgery_Duration.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sTheatre", Label_EditSurgery_Theatre.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sTheatreInvoice", Label_EditSurgery_TheatreInvoice.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sSurgeon", Label_EditSurgery_Surgeon.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sAssistant", Label_EditSurgery_AssistantSurgeon.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sScrubNurse", Label_EditSurgery_ScrubNurse.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sAnaesthesist", Label_EditSurgery_Anesthetist.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sWound", Label_EditSurgery_WoundCategory.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sCategory", DBNull.Value);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sFinalDiagnosis", Label_EditSurgery_FinalDiagnosis.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sAdmissionDate", Label_EditSurgery_DateOfAdmission.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@sDischargeDate", Label_EditSurgery_DischargeDate.Text);
                SqlCommand_EditInfectionPrevention_Site_Surgery.Parameters.AddWithValue("@bSelected", CheckBox_EditSurgery.Checked);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_Surgery);
              }
            }
            //END InfectionPrevention_Site_Surgery


            //START InfectionPrevention_Site_PredisposingCondition
            string SQLStringDeleteInfectionPrevention_Site_PredisposingCondition = "DELETE FROM tblInfectionPrevention_Site_PredisposingCondition WHERE fkiSiteID = @fkiSiteID";
            using (SqlCommand SqlCommand_DeleteInfectionPrevention_Site_PredisposingCondition = new SqlCommand(SQLStringDeleteInfectionPrevention_Site_PredisposingCondition))
            {
              SqlCommand_DeleteInfectionPrevention_Site_PredisposingCondition.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_DeleteInfectionPrevention_Site_PredisposingCondition);
            }

            CheckBox CheckBox_EditPCCF1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF1");
            CheckBox CheckBox_EditPCCF2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF2");
            CheckBox CheckBox_EditPCCF3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF3");
            CheckBox CheckBox_EditPCCF4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF4");
            CheckBox CheckBox_EditPCCF5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF5");
            CheckBox CheckBox_EditPCCF6 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF6");
            CheckBox CheckBox_EditPCCF7 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF7");
            CheckBox CheckBox_EditPCCF8 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF8");
            CheckBox CheckBox_EditPCCF9 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF9");
            CheckBox CheckBox_EditPCCF10 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF10");
            CheckBox CheckBox_EditPCCF11 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF11");
            CheckBox CheckBox_EditPCCF12 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF12");
            CheckBox CheckBox_EditPCCF13 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF13");
            CheckBox CheckBox_EditPCCF14 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF14");
            CheckBox CheckBox_EditPCCF15 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF15");

            TextBox TextBox_EditPCCF1 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF1");
            TextBox TextBox_EditPCCF2 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF2");
            TextBox TextBox_EditPCCF3 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF3");
            TextBox TextBox_EditPCCF4 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF4");
            TextBox TextBox_EditPCCF5 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF5");
            TextBox TextBox_EditPCCF6 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF6");
            TextBox TextBox_EditPCCF7 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF7");
            TextBox TextBox_EditPCCF8 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF8");
            TextBox TextBox_EditPCCF9 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF9");
            TextBox TextBox_EditPCCF10 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF10");
            TextBox TextBox_EditPCCF11 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF11");
            TextBox TextBox_EditPCCF12 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF12");
            TextBox TextBox_EditPCCF13 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF13");
            TextBox TextBox_EditPCCF14 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF14");
            TextBox TextBox_EditPCCF15 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF15");

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition1 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition1 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition1))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition1.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition1.Parameters.AddWithValue("@fkiConditionID", 1163);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition1.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF1.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition1.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF1.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition1);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition2 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition2 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition2))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition2.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition2.Parameters.AddWithValue("@fkiConditionID", 1164);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition2.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF2.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition2.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF2.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition2);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition3 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition3 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition3))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition3.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition3.Parameters.AddWithValue("@fkiConditionID", 1156);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition3.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF3.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition3.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF3.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition3);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition4 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition4 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition4))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition4.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition4.Parameters.AddWithValue("@fkiConditionID", 1159);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition4.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF4.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition4.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF4.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition4);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition5 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition5 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition5))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition5.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition5.Parameters.AddWithValue("@fkiConditionID", 1158);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition5.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF5.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition5.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF5.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition5);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition6 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition6 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition6))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition6.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition6.Parameters.AddWithValue("@fkiConditionID", 1154);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition6.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF6.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition6.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF6.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition6);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition7 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition7 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition7))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition7.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition7.Parameters.AddWithValue("@fkiConditionID", 1155);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition7.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF7.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition7.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF7.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition7);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition8 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition8 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition8))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition8.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition8.Parameters.AddWithValue("@fkiConditionID", 2738);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition8.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF8.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition8.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF8.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition8);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition9 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition9 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition9))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition9.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition9.Parameters.AddWithValue("@fkiConditionID", 2739);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition9.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF9.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition9.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF9.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition9);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition10 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition10 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition10))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition10.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition10.Parameters.AddWithValue("@fkiConditionID", 1161);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition10.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF10.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition10.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF10.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition10);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition11 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition11 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition11))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition11.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition11.Parameters.AddWithValue("@fkiConditionID", 1152);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition11.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF11.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition11.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF11.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition11);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition12 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition12 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition12))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition12.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition12.Parameters.AddWithValue("@fkiConditionID", 1153);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition12.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF12.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition12.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF12.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition12);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition13 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition13 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition13))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition13.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition13.Parameters.AddWithValue("@fkiConditionID", 1162);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition13.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF13.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition13.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF13.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition13);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition14 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition14 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition14))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition14.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition14.Parameters.AddWithValue("@fkiConditionID", 1157);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition14.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF14.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition14.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF14.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition14);
            }

            string SQLStringEditInfectionPrevention_Site_PredisposingCondition15 = "INSERT INTO tblInfectionPrevention_Site_PredisposingCondition ( fkiSiteID ,fkiConditionID ,sDescription ,bSelected ) VALUES ( @fkiSiteID ,@fkiConditionID ,@sDescription ,@bSelected )";
            using (SqlCommand SqlCommand_EditInfectionPrevention_Site_PredisposingCondition15 = new SqlCommand(SQLStringEditInfectionPrevention_Site_PredisposingCondition15))
            {
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition15.Parameters.AddWithValue("@fkiSiteID", LastpkiSiteID);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition15.Parameters.AddWithValue("@fkiConditionID", 1160);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition15.Parameters.AddWithValue("@sDescription", TextBox_EditPCCF15.Text);
              SqlCommand_EditInfectionPrevention_Site_PredisposingCondition15.Parameters.AddWithValue("@bSelected", CheckBox_EditPCCF15.Checked);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention_Site_PredisposingCondition15);
            }
            //END InfectionPrevention_Site_PredisposingCondition
          }

          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            RedirectToList();
          }

          if (Button_EditPrintClicked == true)
          {
            Button_EditPrintClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_InfectionPrevention, this.GetType(), "Print", "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Print", "InfoQuest_Print.aspx?PrintPage=Form_HAI&PrintValue=" + Request.QueryString["InfectionFormID"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_InfectionPrevention, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }

          if (Button_EditEmailClicked == true)
          {
            Button_EditEmailClicked = false;

            ScriptManager.RegisterStartupScript(UpdatePanel_InfectionPrevention, this.GetType(), "Email", "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Email", "InfoQuest_Email.aspx?EmailPage=Form_InfectionPrevention&EmailValue=" + Request.QueryString["InfectionFormID"] + "") + "')", true);
            ScriptManager.RegisterStartupScript(UpdatePanel_InfectionPrevention, this.GetType(), "Reload", "window.location.href='" + Request.Url.AbsoluteUri + "'", true);
          }
        }        
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      //DropDownList DropDownList_EditFacility = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditFacility");
      //TextBox TextBox_EditPatientVisitNumber = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPatientVisitNumber");
      HiddenField HiddenField_EditValidIMedsData = (HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_EditValidIMedsData");
      DropDownList DropDownList_EditUnitId = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditUnitId");
      TextBox TextBox_EditDateInfectionReported = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDateInfectionReported");
      DropDownList DropDownList_EditInfectionType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditInfectionType");
      DropDownList DropDownList_EditSSISubType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditSSISubType");
      RadioButtonList RadioButtonList_EditSeverity = (RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_EditSeverity");
      TextBox TextBox_EditDescription = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDescription");
      TextBox TextBox_EditDays = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDays");

      CheckBox CheckBox_EditInvestigationCompleted = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditInvestigationCompleted");
      TextBox TextBox_EditInvestigationDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDate");
      TextBox TextBox_EditInvestigationName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationName");
      TextBox TextBox_EditInvestigationDesignation = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDesignation");
      TextBox TextBox_EditInvestigationIPCSName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationIPCSName");
      TextBox TextBox_EditInvestigationTeamMembers = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationTeamMembers");
      TextBox TextBox_EditInvestigationCompletedDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationCompletedDate");

      if (InvalidForm == "No")
      {
        if (HiddenField_EditValidIMedsData.Value == "Yes")
        {
          if (string.IsNullOrEmpty(DropDownList_EditUnitId.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(TextBox_EditDateInfectionReported.Text))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(DropDownList_EditInfectionType.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_EditInfectionType.SelectedValue == "1131")
          {
            if (string.IsNullOrEmpty(DropDownList_EditSSISubType.SelectedValue))
            {
              InvalidForm = "Yes";
            }
          }

          if (string.IsNullOrEmpty(RadioButtonList_EditSeverity.SelectedValue))
          {
            InvalidForm = "Yes";
          }

          if (string.IsNullOrEmpty(TextBox_EditDescription.Text))
          {
            InvalidForm = "Yes";
          }

          if (DropDownList_EditInfectionType.SelectedValue == "1131" || DropDownList_EditInfectionType.SelectedValue == "1135" || DropDownList_EditInfectionType.SelectedValue == "1137" || DropDownList_EditInfectionType.SelectedValue == "1139")
          {
            if (string.IsNullOrEmpty(TextBox_EditDays.Text))
            {
              InvalidForm = "Yes";
            }
          }


          if (CheckBox_EditInvestigationCompleted.Checked == true)
          {
            if (string.IsNullOrEmpty(TextBox_EditInvestigationDate.Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(TextBox_EditInvestigationName.Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(TextBox_EditInvestigationDesignation.Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(TextBox_EditInvestigationIPCSName.Text))
            {
              InvalidForm = "Yes";
            }

            if (string.IsNullOrEmpty(TextBox_EditInvestigationTeamMembers.Text))
            {
              InvalidForm = "Yes";
            }
          }
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = Convert.ToString("All red fields are required", CultureInfo.CurrentCulture);
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        if (HiddenField_EditValidIMedsData.Value == "Yes")
        {
          string DateToValidateDateReceived = TextBox_EditDateInfectionReported.Text.ToString();
          DateTime ValidatedDateDateReceived = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDateReceived);

          if (ValidatedDateDateReceived.ToString() == "0001/01/01 12:00:00 AM")
          {
            InvalidFormMessage = InvalidFormMessage + "Date Infection Reported is not in the correct format, date must be in the format yyyy/mm/dd<br />";
          }
          else
          {
            DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDateInfectionReported")).Text, CultureInfo.CurrentCulture);
            DateTime CurrentDate = DateTime.Now;

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
            }
          }
        }

        if (CheckBox_EditInvestigationCompleted.Checked == true)
        {
          string DateToValidateDateReceived = TextBox_EditInvestigationDate.Text.ToString();
          DateTime ValidatedDateDateReceived = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDateReceived);

          if (ValidatedDateDateReceived.ToString() == "0001/01/01 12:00:00 AM")
          {
            InvalidFormMessage = InvalidFormMessage + "Investigation Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
          }
          else
          {
            DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDate")).Text, CultureInfo.CurrentCulture);
            DateTime CurrentDate = DateTime.Now;

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
            }
          }
        }

        if (CheckBox_EditInvestigationCompleted.Checked == true)
        {
          string DateToValidateDateReceived = TextBox_EditInvestigationCompletedDate.Text.ToString();
          DateTime ValidatedDateDateReceived = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidateDateReceived);

          if (ValidatedDateDateReceived.ToString() == "0001/01/01 12:00:00 AM")
          {
            InvalidFormMessage = InvalidFormMessage + "Investigation Completed Date is not in the correct format, date must be in the format yyyy/mm/dd<br />";
          }
          else
          {
            DateTime PickedDate = Convert.ToDateTime(((TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationCompletedDate")).Text, CultureInfo.CurrentCulture);
            DateTime CurrentDate = DateTime.Now;

            if (PickedDate.CompareTo(CurrentDate) > 0)
            {
              InvalidFormMessage = InvalidFormMessage + "No future dates allowed<br />";
            }
          }
        }
      }

      return InvalidFormMessage;
    }
    //---END--- --Edit--//


    protected void FormView_InfectionPrevention_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          if (Request.QueryString["InfectionFormID"] != null)
          {
            Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Form", "Form_InfectionPrevention.aspx?InfectionFormID=" + Request.QueryString["InfectionFormID"] + "&s_FacilityId=" + Request.QueryString["s_FacilityId"] + "&s_PatientVisitNumber=" + Request.QueryString["s_PatientVisitNumber"] + ""), false);
          }
        }
      }
    }

    protected void FormView_InfectionPrevention_Form_DataBound(object sender, EventArgs e)
    {
      if (FormView_InfectionPrevention_Form.CurrentMode == FormViewMode.Insert)
      {
        InsertDataBound();
      }

      if (FormView_InfectionPrevention_Form.CurrentMode == FormViewMode.Edit)
      {
        EditDataBound();
      }

      if (FormView_InfectionPrevention_Form.CurrentMode == FormViewMode.ReadOnly)
      {
        ReadOnlyDataBound();
      }
    }

    protected static void InsertDataBound()
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1809:AvoidExcessiveLocals"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
    protected void EditDataBound()
    {
      //START Main Table
      Session["sReportNumber"] = "";
      Session["Facility_FacilityDisplayName"] = "";
      Session["sPatientVisitNumber"] = "";
      Session["sPatientName"] = "";
      Session["sPatientAge"] = "";
      Session["sPatientDateOfAdmission"] = "";
      Session["dtDateOfInvestigation"] = "";
      Session["sInvestigatorName"] = "";
      Session["sInvestigatorDesignation"] = "";
      Session["sIPCSName"] = "";
      Session["sTeamMembers"] = "";
      Session["bInvestigationCompleted"] = "";
      Session["dtInvestigationCompleted"] = "";
      Session["sFormStatus"] = "";
      string SQLStringInfectionPrevention = "SELECT sReportNumber , Facility_FacilityDisplayName , sPatientVisitNumber , sPatientName , sPatientAge , sPatientDateOfAdmission , CASE WHEN dtDateOfInvestigation = '1900-01-01 00:00:00.000' THEN NULL ELSE dtDateOfInvestigation END AS dtDateOfInvestigation ,sInvestigatorName ,sInvestigatorDesignation ,sIPCSName ,sTeamMembers ,bInvestigationCompleted ,CASE WHEN dtInvestigationCompleted = '1900-01-01 00:00:00.000' THEN NULL ELSE dtInvestigationCompleted END AS dtInvestigationCompleted , sFormStatus FROM tblInfectionPrevention LEFT JOIN vAdministration_Facility_All ON tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id WHERE pkiInfectionFormID = @pkiInfectionFormID";
      using (SqlCommand SqlCommand_InfectionPrevention = new SqlCommand(SQLStringInfectionPrevention))
      {
        SqlCommand_InfectionPrevention.Parameters.AddWithValue("@pkiInfectionFormID", Request.QueryString["InfectionFormID"]);
        DataTable DataTable_InfectionPrevention;
        using (DataTable_InfectionPrevention = new DataTable())
        {
          DataTable_InfectionPrevention.Locale = CultureInfo.CurrentCulture;
          DataTable_InfectionPrevention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention).Copy();
          if (DataTable_InfectionPrevention.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfectionPrevention.Rows)
            {
              Session["sReportNumber"] = DataRow_Row["sReportNumber"];
              Session["Facility_FacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
              Session["sPatientVisitNumber"] = DataRow_Row["sPatientVisitNumber"];
              Session["sPatientName"] = DataRow_Row["sPatientName"];
              Session["sPatientAge"] = DataRow_Row["sPatientAge"];
              Session["sPatientDateOfAdmission"] = DataRow_Row["sPatientDateOfAdmission"];
              Session["dtDateOfInvestigation"] = DataRow_Row["dtDateOfInvestigation"];
              Session["sInvestigatorName"] = DataRow_Row["sInvestigatorName"];
              Session["sInvestigatorDesignation"] = DataRow_Row["sInvestigatorDesignation"];
              Session["sIPCSName"] = DataRow_Row["sIPCSName"];
              Session["sTeamMembers"] = DataRow_Row["sTeamMembers"];
              Session["bInvestigationCompleted"] = DataRow_Row["bInvestigationCompleted"];
              Session["dtInvestigationCompleted"] = DataRow_Row["dtInvestigationCompleted"];
              Session["sFormStatus"] = DataRow_Row["sFormStatus"];
            }
          }
        }
      }

      Label Label_EditReportNumber = (Label)FormView_InfectionPrevention_Form.FindControl("Label_EditReportNumber");
      Label_EditReportNumber.Text = Session["sReportNumber"].ToString();

      Label Label_EditFacility = (Label)FormView_InfectionPrevention_Form.FindControl("Label_EditFacility");
      Label_EditFacility.Text = Session["Facility_FacilityDisplayName"].ToString();

      Label Label_EditPatientVisitNumber = (Label)FormView_InfectionPrevention_Form.FindControl("Label_EditPatientVisitNumber");
      Label_EditPatientVisitNumber.Text = Session["sPatientVisitNumber"].ToString();

      Label Label_EditPatientName = (Label)FormView_InfectionPrevention_Form.FindControl("Label_EditPatientName");
      Label_EditPatientName.Text = Session["sPatientName"].ToString();

      Label Label_EditAge = (Label)FormView_InfectionPrevention_Form.FindControl("Label_EditAge");
      Label_EditAge.Text = Session["sPatientAge"].ToString();

      Label Label_EditDateOfAdmission = (Label)FormView_InfectionPrevention_Form.FindControl("Label_EditDateOfAdmission");
      Label_EditDateOfAdmission.Text = Session["sPatientDateOfAdmission"].ToString();

      TextBox TextBox_EditInvestigationDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDate");
      if (!string.IsNullOrEmpty(Session["dtDateOfInvestigation"].ToString()))
      {
        TextBox_EditInvestigationDate.Text = Convert.ToDateTime(Session["dtDateOfInvestigation"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }
      else
      {
        TextBox_EditInvestigationDate.Text = "";
      }

      TextBox TextBox_EditInvestigationName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationName");
      TextBox_EditInvestigationName.Text = Session["sInvestigatorName"].ToString();

      TextBox TextBox_EditInvestigationDesignation = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationDesignation");
      TextBox_EditInvestigationDesignation.Text = Session["sInvestigatorDesignation"].ToString();

      TextBox TextBox_EditInvestigationIPCSName = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationIPCSName");
      TextBox_EditInvestigationIPCSName.Text = Session["sIPCSName"].ToString();

      TextBox TextBox_EditInvestigationTeamMembers = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationTeamMembers");
      TextBox_EditInvestigationTeamMembers.Text = Session["sTeamMembers"].ToString();

      CheckBox CheckBox_EditInvestigationCompleted = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditInvestigationCompleted");
      CheckBox_EditInvestigationCompleted.Checked = Convert.ToBoolean(Session["bInvestigationCompleted"].ToString(), CultureInfo.CurrentCulture);

      TextBox TextBox_EditInvestigationCompletedDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationCompletedDate");
      if (!string.IsNullOrEmpty(Session["dtInvestigationCompleted"].ToString()))
      {
        TextBox_EditInvestigationCompletedDate.Text = Convert.ToDateTime(Session["dtInvestigationCompleted"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }
      else
      {
        TextBox_EditInvestigationCompletedDate.Text = "";
      }

      Label Label_EditFormStatus = (Label)FormView_InfectionPrevention_Form.FindControl("Label_EditFormStatus");
      Label_EditFormStatus.Text = Session["sFormStatus"].ToString();

      Session["sReportNumber"] = "";
      Session["Facility_FacilityDisplayName"] = "";
      Session["sPatientVisitNumber"] = "";
      Session["sPatientName"] = "";
      Session["sPatientAge"] = "";
      Session["sPatientDateOfAdmission"] = "";
      Session["dtDateOfInvestigation"] = "";
      Session["sInvestigatorName"] = "";
      Session["sInvestigatorDesignation"] = "";
      Session["sIPCSName"] = "";
      Session["sTeamMembers"] = "";
      Session["bInvestigationCompleted"] = "";
      Session["dtInvestigationCompleted"] = "";
      Session["sFormStatus"] = "";
      //END Main Table


      //START InfectionPrevention_PrecautionaryMeasure
      CheckBox CheckBox_EditPIM1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM1");
      CheckBox CheckBox_EditPIM2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM2");
      CheckBox CheckBox_EditPIM3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM3");
      CheckBox CheckBox_EditPIM4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM4");
      CheckBox CheckBox_EditPIM5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM5");
      CheckBox CheckBox_EditPIM6 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPIM6");

      string SQLStringInfectionPrevention_PrecautionaryMeasure = "SELECT bSelected , ListItem_Name FROM tblInfectionPrevention_PrecautionaryMeasure LEFT JOIN Administration_ListItem ON tblInfectionPrevention_PrecautionaryMeasure.fkiPrecautionaryMeasureID = Administration_ListItem.ListItem_Id WHERE fkiInfectionFormID = @fkiInfectionFormID";
      using (SqlCommand SqlCommand_InfectionPrevention_PrecautionaryMeasure = new SqlCommand(SQLStringInfectionPrevention_PrecautionaryMeasure))
      {
        SqlCommand_InfectionPrevention_PrecautionaryMeasure.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
        DataTable DataTable_InfectionPrevention_PrecautionaryMeasure;
        using (DataTable_InfectionPrevention_PrecautionaryMeasure = new DataTable())
        {
          DataTable_InfectionPrevention_PrecautionaryMeasure.Locale = CultureInfo.CurrentCulture;
          DataTable_InfectionPrevention_PrecautionaryMeasure = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_PrecautionaryMeasure).Copy();
          if (DataTable_InfectionPrevention_PrecautionaryMeasure.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_PrecautionaryMeasure.Rows)
            {
              Session["bSelected"] = DataRow_Row["bSelected"];
              Session["ListItem_Name"] = DataRow_Row["ListItem_Name"];

              if (CheckBox_EditPIM1.Text == Session["ListItem_Name"].ToString())
              {
                CheckBox_EditPIM1.Checked = (Boolean)Session["bSelected"];
              }

              if (CheckBox_EditPIM2.Text == Session["ListItem_Name"].ToString())
              {
                CheckBox_EditPIM2.Checked = (Boolean)Session["bSelected"];
              }

              if (CheckBox_EditPIM3.Text == Session["ListItem_Name"].ToString())
              {
                CheckBox_EditPIM3.Checked = (Boolean)Session["bSelected"];
              }

              if (CheckBox_EditPIM4.Text == Session["ListItem_Name"].ToString())
              {
                CheckBox_EditPIM4.Checked = (Boolean)Session["bSelected"];
              }

              if (CheckBox_EditPIM5.Text == Session["ListItem_Name"].ToString())
              {
                CheckBox_EditPIM5.Checked = (Boolean)Session["bSelected"];
              }

              if (CheckBox_EditPIM6.Text == Session["ListItem_Name"].ToString())
              {
                CheckBox_EditPIM6.Checked = (Boolean)Session["bSelected"];
              }
            }
          }
        }
      }

      //END InfectionPrevention_PrecautionaryMeasure


      //START InfectionPrevention_Site
      Session["fkiFacilityUnitID"] = "";
      Session["dtReported"] = "";
      Session["fkiInfectionTypeID"] = "";
      Session["fkiInfectionSubTypeID"] = "";
      Session["fkiSeverityTypeID"] = "";
      Session["sDescription"] = "";
      Session["sInfectionDays"] = "";
      Session["sRelatedHighRiskProcedures"] = "";
      string SQLStringInfectionPrevention_Site = "SELECT fkiFacilityUnitID, dtReported, fkiInfectionTypeID, fkiInfectionSubTypeID, fkiSeverityTypeID, sDescription , sInfectionDays , sRelatedHighRiskProcedures FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID";
      using (SqlCommand SqlCommand_InfectionPrevention_Site = new SqlCommand(SQLStringInfectionPrevention_Site))
      {
        SqlCommand_InfectionPrevention_Site.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
        DataTable DataTable_InfectionPrevention_Site;
        using (DataTable_InfectionPrevention_Site = new DataTable())
        {
          DataTable_InfectionPrevention_Site.Locale = CultureInfo.CurrentCulture;
          DataTable_InfectionPrevention_Site = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site).Copy();
          if (DataTable_InfectionPrevention_Site.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Site.Rows)
            {
              Session["fkiFacilityUnitID"] = DataRow_Row["fkiFacilityUnitID"];
              Session["dtReported"] = DataRow_Row["dtReported"];
              Session["fkiInfectionTypeID"] = DataRow_Row["fkiInfectionTypeID"];
              Session["fkiInfectionSubTypeID"] = DataRow_Row["fkiInfectionSubTypeID"];
              Session["fkiSeverityTypeID"] = DataRow_Row["fkiSeverityTypeID"];
              Session["sDescription"] = DataRow_Row["sDescription"];
              Session["sInfectionDays"] = DataRow_Row["sInfectionDays"];
              Session["sRelatedHighRiskProcedures"] = DataRow_Row["sRelatedHighRiskProcedures"];
            }
          }
        }
      }

      DropDownList DropDownList_EditUnitId = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditUnitId");
      DropDownList_EditUnitId.SelectedValue = Session["fkiFacilityUnitID"].ToString();

      TextBox TextBox_EditDateInfectionReported = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDateInfectionReported");
      TextBox_EditDateInfectionReported.Text = Convert.ToDateTime(Session["dtReported"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);

      DropDownList DropDownList_EditInfectionType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditInfectionType");
      DropDownList_EditInfectionType.SelectedValue = Session["fkiInfectionTypeID"].ToString();

      DropDownList DropDownList_EditSSISubType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditSSISubType");
      DropDownList_EditSSISubType.SelectedValue = Session["fkiInfectionSubTypeID"].ToString();

      RadioButtonList RadioButtonList_EditSeverity = (RadioButtonList)FormView_InfectionPrevention_Form.FindControl("RadioButtonList_EditSeverity");
      RadioButtonList_EditSeverity.SelectedValue = Session["fkiSeverityTypeID"].ToString();

      TextBox TextBox_EditDescription = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDescription");
      TextBox_EditDescription.Text = Session["sDescription"].ToString();

      TextBox TextBox_EditDays = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditDays");
      TextBox_EditDays.Text = Session["sInfectionDays"].ToString();

      CheckBox CheckBox_EditRiskTPN = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditRiskTPN");
      CheckBox CheckBox_EditRiskEnteralFeeding = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditRiskEnteralFeeding");
      if (Session["sRelatedHighRiskProcedures"].ToString() == "Enteral Feeding, TPN")
      {
        CheckBox_EditRiskTPN.Checked = true;
        CheckBox_EditRiskEnteralFeeding.Checked = true;
      }
      else if (Session["sRelatedHighRiskProcedures"].ToString() == "Enteral Feeding")
      {
        CheckBox_EditRiskTPN.Checked = false;
        CheckBox_EditRiskEnteralFeeding.Checked = true;
      }
      else if (Session["sRelatedHighRiskProcedures"].ToString() == "TPN")
      {
        CheckBox_EditRiskTPN.Checked = true;
        CheckBox_EditRiskEnteralFeeding.Checked = false;
      }
      else if (string.IsNullOrEmpty(Session["sRelatedHighRiskProcedures"].ToString()))
      {
        CheckBox_EditRiskTPN.Checked = false;
        CheckBox_EditRiskEnteralFeeding.Checked = false;
      }


      Session["fkiFacilityUnitID"] = "";
      Session["dtReported"] = "";
      Session["fkiInfectionTypeID"] = "";
      Session["fkiInfectionSubTypeID"] = "";
      Session["fkiSeverityTypeID"] = "";
      Session["sDescription"] = "";
      Session["sInfectionDays"] = "";
      Session["sRelatedHighRiskProcedures"] = "";


      DropDownList_EditSSISubType.Items.Clear();

      SqlDataSource_InfectionPrevention_EditSSISubType.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_EditInfectionType.SelectedValue;

      DropDownList_EditSSISubType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select SSI Sub Type", CultureInfo.CurrentCulture), "0"));

      DropDownList_EditSSISubType.DataBind();
      //End InfectionPrevention_Site


      //START InfectionPrevention_Site_BundleComplianceItem
      CheckBox CheckBox_EditTypeSSI1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI1");
      CheckBox CheckBox_EditTypeSSI2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI2");
      CheckBox CheckBox_EditTypeSSI3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI3");
      CheckBox CheckBox_EditTypeSSI4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeSSI4");

      CheckBox CheckBox_EditTypeCLABSI1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI1");
      CheckBox CheckBox_EditTypeCLABSI2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI2");
      CheckBox CheckBox_EditTypeCLABSI3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI3");
      CheckBox CheckBox_EditTypeCLABSI4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI4");
      CheckBox CheckBox_EditTypeCLABSI5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI5");
      CheckBox CheckBox_EditTypeCLABSI6 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI6");
      CheckBox CheckBox_EditTypeCLABSI7 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCLABSI7");

      CheckBox CheckBox_EditTypeVAP1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP1");
      CheckBox CheckBox_EditTypeVAP2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP2");
      CheckBox CheckBox_EditTypeVAP3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP3");
      CheckBox CheckBox_EditTypeVAP4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP4");
      CheckBox CheckBox_EditTypeVAP5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeVAP5");

      CheckBox CheckBox_EditTypeCAUTI1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI1");
      CheckBox CheckBox_EditTypeCAUTI2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI2");
      CheckBox CheckBox_EditTypeCAUTI3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI3");
      CheckBox CheckBox_EditTypeCAUTI4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditTypeCAUTI4");

      Session["bSelected"] = "";
      Session["ListItem_Name"] = "";
      string SQLStringInfectionPrevention_Site_BundleComplianceItem = "SELECT bSelected , LEFT(RTRIM(LTRIM(ListItem_Name)),3) AS ListItem_Name  FROM tblInfectionPrevention_Site_BundleComplianceItem LEFT JOIN Administration_ListItem ON tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID = Administration_ListItem.ListItem_Id WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
      using (SqlCommand SqlCommand_InfectionPrevention_Site_BundleComplianceItem = new SqlCommand(SQLStringInfectionPrevention_Site_BundleComplianceItem))
      {
        SqlCommand_InfectionPrevention_Site_BundleComplianceItem.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
        DataTable DataTable_InfectionPrevention_Site_BundleComplianceItem;
        using (DataTable_InfectionPrevention_Site_BundleComplianceItem = new DataTable())
        {
          DataTable_InfectionPrevention_Site_BundleComplianceItem.Locale = CultureInfo.CurrentCulture;
          DataTable_InfectionPrevention_Site_BundleComplianceItem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_BundleComplianceItem).Copy();
          if (DataTable_InfectionPrevention_Site_BundleComplianceItem.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Site_BundleComplianceItem.Rows)
            {
              Session["bSelected"] = DataRow_Row["bSelected"];
              Session["ListItem_Name"] = DataRow_Row["ListItem_Name"];

              if (Session["ListItem_Name"].ToString() == "1.1")
              {
                CheckBox_EditTypeSSI1.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "1.2")
              {
                CheckBox_EditTypeSSI2.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "1.3")
              {
                CheckBox_EditTypeSSI3.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "1.4")
              {
                CheckBox_EditTypeSSI4.Checked = (Boolean)Session["bSelected"];
              }


              if (Session["ListItem_Name"].ToString() == "2.1")
              {
                CheckBox_EditTypeCLABSI1.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "2.2")
              {
                CheckBox_EditTypeCLABSI2.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "2.3")
              {
                CheckBox_EditTypeCLABSI3.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "2.4")
              {
                CheckBox_EditTypeCLABSI4.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "2.5")
              {
                CheckBox_EditTypeCLABSI5.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "2.6")
              {
                CheckBox_EditTypeCLABSI6.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "2.7")
              {
                CheckBox_EditTypeCLABSI7.Checked = (Boolean)Session["bSelected"];
              }


              if (Session["ListItem_Name"].ToString() == "3.1")
              {
                CheckBox_EditTypeVAP1.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "3.2")
              {
                CheckBox_EditTypeVAP2.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "3.3")
              {
                CheckBox_EditTypeVAP3.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "3.4")
              {
                CheckBox_EditTypeVAP4.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "3.5")
              {
                CheckBox_EditTypeVAP5.Checked = (Boolean)Session["bSelected"];
              }


              if (Session["ListItem_Name"].ToString() == "4.1")
              {
                CheckBox_EditTypeCAUTI1.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "4.2")
              {
                CheckBox_EditTypeCAUTI2.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "4.3")
              {
                CheckBox_EditTypeCAUTI3.Checked = (Boolean)Session["bSelected"];
              }

              if (Session["ListItem_Name"].ToString() == "4.4")
              {
                CheckBox_EditTypeCAUTI4.Checked = (Boolean)Session["bSelected"];
              }
            }
          }
        }
      }

      Session["bSelected"] = "";
      Session["ListItem_Name"] = "";
      //END InfectionPrevention_Site_BundleComplianceItem


      //START InfectionPrevention_Site_ReportableDisease
      Session["bSelected"] = "";
      Session["sDescription"] = "";
      Session["ListItem_Name"] = "";
      string SQLStringInfectionPrevention_Site_ReportableDisease = "SELECT fkiDiseaseID ,sDateReported ,sReferenceNumber ,bSelected FROM tblInfectionPrevention_Site_ReportableDisease WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
      using (SqlCommand SqlCommand_InfectionPrevention_Site_ReportableDisease = new SqlCommand(SQLStringInfectionPrevention_Site_ReportableDisease))
      {
        SqlCommand_InfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
        DataTable DataTable_InfectionPrevention_Site_ReportableDisease;
        using (DataTable_InfectionPrevention_Site_ReportableDisease = new DataTable())
        {
          DataTable_InfectionPrevention_Site_ReportableDisease.Locale = CultureInfo.CurrentCulture;
          DataTable_InfectionPrevention_Site_ReportableDisease = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_ReportableDisease).Copy();
          if (DataTable_InfectionPrevention_Site_ReportableDisease.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Site_ReportableDisease.Rows)
            {
              Session["fkiDiseaseID"] = DataRow_Row["fkiDiseaseID"];
              Session["sDateReported"] = DataRow_Row["sDateReported"];
              Session["sReferenceNumber"] = DataRow_Row["sReferenceNumber"];
              Session["bSelected"] = DataRow_Row["bSelected"];

              CheckBox CheckBox_EditRD = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditRD");
              DropDownList DropDownList_EditRDNotifiableDisease = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditRDNotifiableDisease");
              TextBox TextBox_EditRDDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditRDDate");
              TextBox TextBox_EditRDReferenceNumber = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditRDReferenceNumber");

              CheckBox_EditRD.Checked = (Boolean)Session["bSelected"];
              DropDownList_EditRDNotifiableDisease.SelectedValue = Session["fkiDiseaseID"].ToString();
              TextBox_EditRDDate.Text = Session["sDateReported"].ToString();
              TextBox_EditRDReferenceNumber.Text = Session["sReferenceNumber"].ToString();
            }
          }
        }
      }
      Session["bSelected"] = "";
      Session["sDescription"] = "";
      Session["ListItem_Name"] = "";
      //END InfectionPrevention_Site_ReportableDisease


      //START InfectionPrevention_Site_PredisposingCondition
      CheckBox CheckBox_EditPCCF1 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF1");
      CheckBox CheckBox_EditPCCF2 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF2");
      CheckBox CheckBox_EditPCCF3 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF3");
      CheckBox CheckBox_EditPCCF4 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF4");
      CheckBox CheckBox_EditPCCF5 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF5");
      CheckBox CheckBox_EditPCCF6 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF6");
      CheckBox CheckBox_EditPCCF7 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF7");
      CheckBox CheckBox_EditPCCF8 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF8");
      CheckBox CheckBox_EditPCCF9 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF9");
      CheckBox CheckBox_EditPCCF10 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF10");
      CheckBox CheckBox_EditPCCF11 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF11");
      CheckBox CheckBox_EditPCCF12 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF12");
      CheckBox CheckBox_EditPCCF13 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF13");
      CheckBox CheckBox_EditPCCF14 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF14");
      CheckBox CheckBox_EditPCCF15 = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditPCCF15");

      TextBox TextBox_EditPCCF1 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF1");
      TextBox TextBox_EditPCCF2 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF2");
      TextBox TextBox_EditPCCF3 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF3");
      TextBox TextBox_EditPCCF4 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF4");
      TextBox TextBox_EditPCCF5 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF5");
      TextBox TextBox_EditPCCF6 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF6");
      TextBox TextBox_EditPCCF7 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF7");
      TextBox TextBox_EditPCCF8 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF8");
      TextBox TextBox_EditPCCF9 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF9");
      TextBox TextBox_EditPCCF10 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF10");
      TextBox TextBox_EditPCCF11 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF11");
      TextBox TextBox_EditPCCF12 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF12");
      TextBox TextBox_EditPCCF13 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF13");
      TextBox TextBox_EditPCCF14 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF14");
      TextBox TextBox_EditPCCF15 = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditPCCF15");

      Session["bSelected"] = "";
      Session["sDescription"] = "";
      Session["ListItem_Name"] = "";
      string SQLStringInfectionPrevention_Site_PredisposingCondition = "SELECT bSelected , sDescription, ListItem_Name FROM tblInfectionPrevention_Site_PredisposingCondition LEFT JOIN Administration_ListItem ON tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID = Administration_ListItem.ListItem_Id WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
      using (SqlCommand SqlCommand_InfectionPrevention_Site_PredisposingCondition = new SqlCommand(SQLStringInfectionPrevention_Site_PredisposingCondition))
      {
        SqlCommand_InfectionPrevention_Site_PredisposingCondition.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
        DataTable DataTable_InfectionPrevention_Site_PredisposingCondition;
        using (DataTable_InfectionPrevention_Site_PredisposingCondition = new DataTable())
        {
          DataTable_InfectionPrevention_Site_PredisposingCondition.Locale = CultureInfo.CurrentCulture;
          DataTable_InfectionPrevention_Site_PredisposingCondition = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_PredisposingCondition).Copy();
          if (DataTable_InfectionPrevention_Site_PredisposingCondition.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Site_PredisposingCondition.Rows)
            {
              Session["bSelected"] = DataRow_Row["bSelected"];
              Session["sDescription"] = DataRow_Row["sDescription"];
              Session["ListItem_Name"] = DataRow_Row["ListItem_Name"];


              if (Session["ListItem_Name"].ToString() == "Carcinoma")
              {
                CheckBox_EditPCCF1.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF1.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Chronic respiratory disease")
              {
                CheckBox_EditPCCF2.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF2.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Diabetes")
              {
                CheckBox_EditPCCF3.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF3.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Haematology")
              {
                CheckBox_EditPCCF4.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF4.Text = Session["sDescription"].ToString();
              }

              if (Session["ListItem_Name"].ToString() == "Hypertention")
              {
                CheckBox_EditPCCF5.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF5.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Immune compromised")
              {
                CheckBox_EditPCCF6.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF6.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Immune suppressed")
              {
                CheckBox_EditPCCF7.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF7.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Obesity")
              {
                CheckBox_EditPCCF8.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF8.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Other")
              {
                CheckBox_EditPCCF9.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF9.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Prematurity")
              {
                CheckBox_EditPCCF10.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF10.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Prolonged hospital stay")
              {
                CheckBox_EditPCCF11.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF11.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Prolonged ventilation")
              {
                CheckBox_EditPCCF12.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF12.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Prosthetic implants")
              {
                CheckBox_EditPCCF13.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF13.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Renal failure")
              {
                CheckBox_EditPCCF14.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF14.Text = Session["sDescription"].ToString();
              }
              
              if (Session["ListItem_Name"].ToString() == "Steroid treatment")
              {
                CheckBox_EditPCCF15.Checked = (Boolean)Session["bSelected"];
                TextBox_EditPCCF15.Text = Session["sDescription"].ToString();
              }
              
            }
          }
        }
      }
      Session["bSelected"] = "";
      Session["sDescription"] = "";
      Session["ListItem_Name"] = "";
      //END InfectionPrevention_Site_PredisposingCondition


      string Email = "";
      string Print = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 4";
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
        ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditPrint")).Visible = false;
      }
      else
      {
        ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditPrint")).Visible = true;
      }

      if (Email == "False")
      {
        ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditEmail")).Visible = false;
      }
      else
      {
        ((Button)FormView_InfectionPrevention_Form.FindControl("Button_EditEmail")).Visible = true;
      }

      Email = "";
      Print = "";
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1809:AvoidExcessiveLocals"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
    protected void ReadOnlyDataBound()
    {
      if (!string.IsNullOrEmpty(Request.QueryString["InfectionFormID"]))
      {
        Session["sReportNumber"] = "";
        Session["Facility_FacilityDisplayName"] = "";
        Session["sPatientVisitNumber"] = "";
        Session["sPatientName"] = "";
        Session["sPatientAge"] = "";
        Session["sPatientDateOfAdmission"] = "";
        Session["dtDateOfInvestigation"] = "";
        Session["sInvestigatorName"] = "";
        Session["sInvestigatorDesignation"] = "";
        Session["sIPCSName"] = "";
        Session["sTeamMembers"] = "";
        Session["bInvestigationCompleted"] = "";
        Session["dtInvestigationCompleted"] = "";
        Session["sFormStatus"] = "";
        string SQLStringInfectionPrevention = "SELECT sReportNumber , Facility_FacilityDisplayName , sPatientVisitNumber , sPatientName , sPatientAge , sPatientDateOfAdmission ,CASE WHEN dtDateOfInvestigation = '1900-01-01 00:00:00.000' THEN NULL ELSE dtDateOfInvestigation END AS dtDateOfInvestigation ,sInvestigatorName ,sInvestigatorDesignation ,sIPCSName ,sTeamMembers ,CASE WHEN bInvestigationCompleted = 1 THEN 'Yes' ELSE 'No' END AS bInvestigationCompleted ,CASE WHEN dtInvestigationCompleted = '1900-01-01 00:00:00.000' THEN NULL ELSE dtInvestigationCompleted END AS dtInvestigationCompleted , sFormStatus FROM tblInfectionPrevention LEFT JOIN vAdministration_Facility_All ON tblInfectionPrevention.fkiFacilityID = vAdministration_Facility_All.Facility_Id WHERE pkiInfectionFormID = @pkiInfectionFormID";
        using (SqlCommand SqlCommand_InfectionPrevention = new SqlCommand(SQLStringInfectionPrevention))
        {
          SqlCommand_InfectionPrevention.Parameters.AddWithValue("@pkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention;
          using (DataTable_InfectionPrevention = new DataTable())
          {
            DataTable_InfectionPrevention.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention).Copy();
            if (DataTable_InfectionPrevention.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_InfectionPrevention.Rows)
              {
                Session["sReportNumber"] = DataRow_Row["sReportNumber"];
                Session["Facility_FacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
                Session["sPatientVisitNumber"] = DataRow_Row["sPatientVisitNumber"];
                Session["sPatientName"] = DataRow_Row["sPatientName"];
                Session["sPatientAge"] = DataRow_Row["sPatientAge"];
                Session["sPatientDateOfAdmission"] = DataRow_Row["sPatientDateOfAdmission"];
                Session["dtDateOfInvestigation"] = DataRow_Row["dtDateOfInvestigation"];
                Session["sInvestigatorName"] = DataRow_Row["sInvestigatorName"];
                Session["sInvestigatorDesignation"] = DataRow_Row["sInvestigatorDesignation"];
                Session["sIPCSName"] = DataRow_Row["sIPCSName"];
                Session["sTeamMembers"] = DataRow_Row["sTeamMembers"];
                Session["bInvestigationCompleted"] = DataRow_Row["bInvestigationCompleted"];
                Session["dtInvestigationCompleted"] = DataRow_Row["dtInvestigationCompleted"];
                Session["sFormStatus"] = DataRow_Row["sFormStatus"];
              }
            }
          }
        }

        Label Label_ItemReportNumber = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemReportNumber");
        Label_ItemReportNumber.Text = Session["sReportNumber"].ToString();

        Label Label_ItemFacility = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemFacility");
        Label_ItemFacility.Text = Session["Facility_FacilityDisplayName"].ToString();

        Label Label_ItemPatientVisitNumber = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemPatientVisitNumber");
        Label_ItemPatientVisitNumber.Text = Session["sPatientVisitNumber"].ToString();

        Label Label_ItemPatientName = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemPatientName");
        Label_ItemPatientName.Text = Session["sPatientName"].ToString();

        Label Label_ItemAge = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemAge");
        Label_ItemAge.Text = Session["sPatientAge"].ToString();

        Label Label_ItemDateOfAdmission = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemDateOfAdmission");
        Label_ItemDateOfAdmission.Text = Session["sPatientDateOfAdmission"].ToString();

        Label Label_ItemInvestigationDate = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemInvestigationDate");
        Label_ItemInvestigationDate.Text = Session["dtDateOfInvestigation"].ToString();

        Label Label_ItemInvestigationName = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemInvestigationName");
        Label_ItemInvestigationName.Text = Session["sInvestigatorName"].ToString();

        Label Label_ItemInvestigationDesignation = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemInvestigationDesignation");
        Label_ItemInvestigationDesignation.Text = Session["sInvestigatorDesignation"].ToString();

        Label Label_ItemInvestigationIPCSName = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemInvestigationIPCSName");
        Label_ItemInvestigationIPCSName.Text = Session["sIPCSName"].ToString();

        Label Label_ItemInvestigationTeamMembers = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemInvestigationTeamMembers");
        Label_ItemInvestigationTeamMembers.Text = Session["sTeamMembers"].ToString();

        Label Label_ItemInvestigationCompleted = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemInvestigationCompleted");
        Label_ItemInvestigationCompleted.Text = Session["bInvestigationCompleted"].ToString();

        HiddenField HiddenField_ItemInvestigationCompleted = (HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_ItemInvestigationCompleted");
        HiddenField_ItemInvestigationCompleted.Value = Session["bInvestigationCompleted"].ToString();

        Label Label_ItemInvestigationCompletedDate = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemInvestigationCompletedDate");
        Label_ItemInvestigationCompletedDate.Text = Session["dtInvestigationCompleted"].ToString();

        Label Label_ItemFormStatus = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemFormStatus");
        Label_ItemFormStatus.Text = Session["sFormStatus"].ToString();


        Session["sReportNumber"] = "";
        Session["Facility_FacilityDisplayName"] = "";
        Session["sPatientVisitNumber"] = "";
        Session["sPatientName"] = "";
        Session["sPatientAge"] = "";
        Session["sPatientDateOfAdmission"] = "";
        Session["dtDateOfInvestigation"] = "";
        Session["sInvestigatorName"] = "";
        Session["sInvestigatorDesignation"] = "";
        Session["sIPCSName"] = "";
        Session["sTeamMembers"] = "";
        Session["bInvestigationCompleted"] = "";
        Session["dtInvestigationCompleted"] = "";
        Session["sFormStatus"] = "";


        //START InfectionPrevention_VisitDiagnosis
        GridView GridView_ItemVisitDiagnosis = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_ItemVisitDiagnosis");

        string SQLStringInfectionPrevention_VisitDiagnosis = "SELECT * FROM tblInfectionPrevention_VisitDiagnosis WHERE fkiInfectionFormID = @fkiInfectionFormID";
        using (SqlCommand SqlCommand_InfectionPrevention_VisitDiagnosis = new SqlCommand(SQLStringInfectionPrevention_VisitDiagnosis))
        {
          SqlCommand_InfectionPrevention_VisitDiagnosis.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_VisitDiagnosis;
          using (DataTable_InfectionPrevention_VisitDiagnosis = new DataTable())
          {
            DataTable_InfectionPrevention_VisitDiagnosis.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_VisitDiagnosis = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_VisitDiagnosis).Copy();
            GridView_ItemVisitDiagnosis.DataSource = DataTable_InfectionPrevention_VisitDiagnosis;
            GridView_ItemVisitDiagnosis.DataBind();
          }
        }
        //END InfectionPrevention_VisitDiagnosis


        //START InfectionPrevention_PrecautionaryMeasure
        GridView GridView_ItemPIM = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_ItemPIM");

        string SQLStringInfectionPrevention_PrecautionaryMeasure = "SELECT bSelected , ListItem_Name FROM tblInfectionPrevention_PrecautionaryMeasure LEFT JOIN Administration_ListItem ON tblInfectionPrevention_PrecautionaryMeasure.fkiPrecautionaryMeasureID = Administration_ListItem.ListItem_Id WHERE fkiInfectionFormID = @fkiInfectionFormID";
        using (SqlCommand SqlCommand_InfectionPrevention_PrecautionaryMeasure = new SqlCommand(SQLStringInfectionPrevention_PrecautionaryMeasure))
        {
          SqlCommand_InfectionPrevention_PrecautionaryMeasure.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_PrecautionaryMeasure;
          using (DataTable_InfectionPrevention_PrecautionaryMeasure = new DataTable())
          {
            DataTable_InfectionPrevention_PrecautionaryMeasure.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_PrecautionaryMeasure = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_PrecautionaryMeasure).Copy();
            GridView_ItemPIM.DataSource = DataTable_InfectionPrevention_PrecautionaryMeasure;
            GridView_ItemPIM.DataBind();
          }
        }
        //END InfectionPrevention_PrecautionaryMeasure


        //START InfectionPrevention_Site
        Session["Unit_Name"] = "";
        Session["dtReported"] = "";
        Session["fkiInfectionTypeID"] = "";
        Session["ListItem_Name1"] = "";
        Session["ListItem_Name2"] = "";
        Session["sDescription1"] = "";
        Session["sDescription2"] = "";
        Session["sInfectionDays"] = "";
        Session["sDescription3"] = "";
        Session["sRelatedHighRiskProcedures"] = "";
        string SQLStringInfectionPrevention_Site = "SELECT Administration_Unit.Unit_Name , tblInfectionPrevention_Site.dtReported , tblInfectionPrevention_Site.fkiInfectionTypeID , A.ListItem_Name AS ListItem_Name1, B.ListItem_Name AS ListItem_Name2 , tblSeverityType.sDescription AS sDescription1 , tblInfectionPrevention_Site.sDescription AS sDescription2 , tblInfectionPrevention_Site.sInfectionDays , tblOveralBundleCompliance.sDescription AS sDescription3 , tblInfectionPrevention_Site.sRelatedHighRiskProcedures FROM tblInfectionPrevention_Site LEFT JOIN Administration_Unit ON tblInfectionPrevention_Site.fkiFacilityUnitID = Unit_Id LEFT JOIN Administration_ListItem AS A ON tblInfectionPrevention_Site.fkiInfectionTypeID = A.ListItem_Id LEFT JOIN Administration_ListItem AS B ON tblInfectionPrevention_Site.fkiInfectionSubTypeID = B.ListItem_Id LEFT JOIN tblSeverityType ON tblInfectionPrevention_Site.fkiSeverityTypeID = tblSeverityType.pkiSeverityTypeID LEFT JOIN tblOveralBundleCompliance ON tblInfectionPrevention_Site.fkiBundleComplianceID = tblOveralBundleCompliance.pkiBundleComplianceID WHERE fkiInfectionFormID = @fkiInfectionFormID";
        using (SqlCommand SqlCommand_InfectionPrevention_Site = new SqlCommand(SQLStringInfectionPrevention_Site))
        {
          SqlCommand_InfectionPrevention_Site.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_Site;
          using (DataTable_InfectionPrevention_Site = new DataTable())
          {
            DataTable_InfectionPrevention_Site.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_Site = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site).Copy();
            if (DataTable_InfectionPrevention_Site.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Site.Rows)
              {
                Session["Unit_Name"] = DataRow_Row["Unit_Name"];
                Session["dtReported"] = DataRow_Row["dtReported"];
                Session["fkiInfectionTypeID"] = DataRow_Row["fkiInfectionTypeID"];
                Session["ListItem_Name1"] = DataRow_Row["ListItem_Name1"];
                Session["ListItem_Name2"] = DataRow_Row["ListItem_Name2"];
                Session["sDescription1"] = DataRow_Row["sDescription1"];
                Session["sDescription2"] = DataRow_Row["sDescription2"];
                Session["sInfectionDays"] = DataRow_Row["sInfectionDays"];
                Session["sDescription3"] = DataRow_Row["sDescription3"];
                Session["sRelatedHighRiskProcedures"] = DataRow_Row["sRelatedHighRiskProcedures"];
              }
            }
          }
        }

        Label Label_ItemUnitId = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemUnitId");
        Label_ItemUnitId.Text = Session["Unit_Name"].ToString();

        Label Label_ItemDateInfectionReported = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemDateInfectionReported");
        Label_ItemDateInfectionReported.Text = Session["dtReported"].ToString();

        Label Label_ItemInfectionType = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemInfectionType");
        Label_ItemInfectionType.Text = Session["ListItem_Name1"].ToString();

        HiddenField HiddenField_ItemInfectionType = (HiddenField)FormView_InfectionPrevention_Form.FindControl("HiddenField_ItemInfectionType");
        HiddenField_ItemInfectionType.Value = Session["fkiInfectionTypeID"].ToString();

        Label Label_ItemSSISubType = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemSSISubType");
        Label_ItemSSISubType.Text = Session["ListItem_Name2"].ToString();

        Label Label_ItemSeverity = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemSeverity");
        Label_ItemSeverity.Text = Session["sDescription1"].ToString();

        Label Label_ItemDescription = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemDescription");
        Label_ItemDescription.Text = Session["sDescription2"].ToString();

        Label Label_Days = (Label)FormView_InfectionPrevention_Form.FindControl("Label_Days");
        Label_Days.Text = Session["sInfectionDays"].ToString();

        Label Label_ItemCompliance = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemCompliance");
        Label_ItemCompliance.Text = Session["sDescription3"].ToString();

        Label Label_ItemRiskTPN = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemRiskTPN");
        Label_ItemRiskTPN.Text = Session["sRelatedHighRiskProcedures"].ToString();


        Session["Unit_Name"] = "";
        Session["dtReported"] = "";
        Session["fkiInfectionTypeID"] = "";
        Session["ListItem_Name1"] = "";
        Session["ListItem_Name2"] = "";
        Session["sDescription1"] = "";
        Session["sDescription2"] = "";
        Session["sInfectionDays"] = "";
        Session["sDescription3"] = "";
        Session["sRelatedHighRiskProcedures"] = "";
        //END InfectionPrevention_Site


        //START InfectionPrevention_Site_BundleComplianceItem
        Session["ListItem_Name1"] = "";
        string SQLStringInfectionPrevention_Site_BundleComplianceItem = "SELECT b.ListItem_Name AS ListItem_Name1, CASE WHEN bSelected = 1 THEN 'Yes' ELSE 'No' END AS bSelected , A.ListItem_Name AS ListItem_Name2 FROM tblInfectionPrevention_Site_BundleComplianceItem LEFT JOIN Administration_ListItem AS A ON tblInfectionPrevention_Site_BundleComplianceItem.fkiBundleItemTypeID = A.ListItem_Id LEFT JOIN tblInfectionPrevention_Site ON tblInfectionPrevention_Site_BundleComplianceItem.fkiSiteID = tblInfectionPrevention_Site.pkiSiteID LEFT JOIN Administration_ListItem AS B ON tblInfectionPrevention_Site.fkiInfectionTypeID = B.ListItem_Id WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
        using (SqlCommand SqlCommand_InfectionPrevention_Site_BundleComplianceItem = new SqlCommand(SQLStringInfectionPrevention_Site_BundleComplianceItem))
        {
          SqlCommand_InfectionPrevention_Site_BundleComplianceItem.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_Site_BundleComplianceItem;
          using (DataTable_InfectionPrevention_Site_BundleComplianceItem = new DataTable())
          {
            DataTable_InfectionPrevention_Site_BundleComplianceItem.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_Site_BundleComplianceItem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_BundleComplianceItem).Copy();
            if (DataTable_InfectionPrevention_Site_BundleComplianceItem.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_InfectionPrevention_Site_BundleComplianceItem.Rows)
              {
                Session["ListItem_Name1"] = DataRow_Row["ListItem_Name1"];
              }
            }
          }
        }

        Label Label_ItemBundleType = (Label)FormView_InfectionPrevention_Form.FindControl("Label_ItemBundleType");
        Label_ItemBundleType.Text = Session["ListItem_Name1"].ToString();


        Session["ListItem_Name1"] = "";


        GridView GridView_ItemBundleType = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_ItemBundleType");
        using (SqlCommand SqlCommand_InfectionPrevention_Site_BundleComplianceItem = new SqlCommand(SQLStringInfectionPrevention_Site_BundleComplianceItem))
        {
          SqlCommand_InfectionPrevention_Site_BundleComplianceItem.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_Site_BundleComplianceItem;
          using (DataTable_InfectionPrevention_Site_BundleComplianceItem = new DataTable())
          {
            DataTable_InfectionPrevention_Site_BundleComplianceItem.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_Site_BundleComplianceItem = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_BundleComplianceItem).Copy();
            GridView_ItemBundleType.DataSource = DataTable_InfectionPrevention_Site_BundleComplianceItem;
            GridView_ItemBundleType.DataBind();
          }
        }
        //END InfectionPrevention_Site_BundleComplianceItem


        //START InfectionPrevention_Site_LabReport
        GridView GridView_ItemLabReport = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_ItemLabReport");

        string SQLStringInfectionPrevention_Site_LabReport = "SELECT CASE WHEN bSelected = 1 THEN 'Yes' ELSE 'No' END AS bSelected , sLabDate , sSpecimen , sOrganism , sLabNumber FROM tblInfectionPrevention_Site_LabReport WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
        using (SqlCommand SqlCommand_InfectionPrevention_Site_LabReport = new SqlCommand(SQLStringInfectionPrevention_Site_LabReport))
        {
          SqlCommand_InfectionPrevention_Site_LabReport.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_Site_LabReport;
          using (DataTable_InfectionPrevention_Site_LabReport = new DataTable())
          {
            DataTable_InfectionPrevention_Site_LabReport.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_Site_LabReport = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_LabReport).Copy();
            GridView_ItemLabReport.DataSource = DataTable_InfectionPrevention_Site_LabReport;
            GridView_ItemLabReport.DataBind();
          }
        }
        //END InfectionPrevention_Site_LabReport


        //START lInfectionPrevention_Site_ReportableDisease
        GridView GridView_ItemReportableDiseases = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_ItemReportableDiseases");

        string SQLStringInfectionPrevention_Site_ReportableDisease = "SELECT CASE WHEN bSelected = 1 THEN 'Yes' ELSE 'No' END AS bSelected , sReportedToDepartment , ListItem_Name , sDateReported , sReferenceNumber FROM tblInfectionPrevention_Site_ReportableDisease LEFT JOIN Administration_ListItem ON tblInfectionPrevention_Site_ReportableDisease.fkiDiseaseID = Administration_ListItem.ListItem_Id WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
        using (SqlCommand SqlCommand_InfectionPrevention_Site_ReportableDisease = new SqlCommand(SQLStringInfectionPrevention_Site_ReportableDisease))
        {
          SqlCommand_InfectionPrevention_Site_ReportableDisease.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_Site_ReportableDisease;
          using (DataTable_InfectionPrevention_Site_ReportableDisease = new DataTable())
          {
            DataTable_InfectionPrevention_Site_ReportableDisease.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_Site_ReportableDisease = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_ReportableDisease).Copy();
            GridView_ItemReportableDiseases.DataSource = DataTable_InfectionPrevention_Site_ReportableDisease;
            GridView_ItemReportableDiseases.DataBind();
          }
        }
        //END lInfectionPrevention_Site_ReportableDisease


        //START InfectionPrevention_Site_BedHistory
        GridView GridView_ItemBedHistory = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_ItemBedHistory");

        string SQLStringInfectionPrevention_Site_BedHistory = "SELECT CASE WHEN bSelected = 1 THEN 'Yes' ELSE 'No' END AS bSelected , sToUnit , sDateTransferred FROM tblInfectionPrevention_Site_BedHistory WHERE fkiSiteID IN (SELECT pkiSiteID FROM tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
        using (SqlCommand SqlCommand_InfectionPrevention_Site_BedHistory = new SqlCommand(SQLStringInfectionPrevention_Site_BedHistory))
        {
          SqlCommand_InfectionPrevention_Site_BedHistory.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_Site_BedHistory;
          using (DataTable_InfectionPrevention_Site_BedHistory = new DataTable())
          {
            DataTable_InfectionPrevention_Site_BedHistory.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_Site_BedHistory = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_BedHistory).Copy();
            GridView_ItemBedHistory.DataSource = DataTable_InfectionPrevention_Site_BedHistory;
            GridView_ItemBedHistory.DataBind();
          }
        }
        //END InfectionPrevention_Site_BedHistory

        //START InfectionPrevention_Site_PredisposingCondition
        GridView GridView_ItemPCCF = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_ItemPCCF");

        string SQLStringInfectionPrevention_Site_PredisposingCondition = "SELECT CASE WHEN bSelected = 1 THEN 'Yes' ELSE 'No' END AS bSelected , ListItem_Name , sDescription FROM tblInfectionPrevention_Site_PredisposingCondition LEFT JOIN Administration_ListItem ON tblInfectionPrevention_Site_PredisposingCondition.fkiConditionID = Administration_ListItem.ListItem_Id WHERE fkiSiteID IN (SELECT pkiSiteID FROM dbo.tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
        using (SqlCommand SqlCommand_InfectionPrevention_Site_PredisposingCondition = new SqlCommand(SQLStringInfectionPrevention_Site_PredisposingCondition))
        {
          SqlCommand_InfectionPrevention_Site_PredisposingCondition.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_Site_PredisposingCondition;
          using (DataTable_InfectionPrevention_Site_PredisposingCondition = new DataTable())
          {
            DataTable_InfectionPrevention_Site_PredisposingCondition.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_Site_PredisposingCondition = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_PredisposingCondition).Copy();
            GridView_ItemPCCF.DataSource = DataTable_InfectionPrevention_Site_PredisposingCondition;
            GridView_ItemPCCF.DataBind();
          }
        }
        //END InfectionPrevention_Site_PredisposingCondition


        //START InfectionPrevention_Site_Surgery
        GridView GridView_ItemSurgery = (GridView)FormView_InfectionPrevention_Form.FindControl("GridView_ItemSurgery");

        string SQLStringInfectionPrevention_Site_Surgery = "SELECT CASE WHEN bSelected = 1 THEN 'Yes' ELSE 'No' END AS bSelected,sFacility ,sVisitNumber ,sProcedure ,sSurgeryDate ,sSurgeryDuration ,sTheatre ,sTheatreInvoice ,sSurgeon ,sAssistant ,sScrubNurse ,sAnaesthesist ,sWound ,sCategory ,sFinalDiagnosis ,sAdmissionDate ,sDischargeDate FROM tblInfectionPrevention_Site_Surgery WHERE fkiSiteID IN (SELECT pkiSiteID FROM dbo.tblInfectionPrevention_Site WHERE fkiInfectionFormID = @fkiInfectionFormID)";
        using (SqlCommand SqlCommand_InfectionPrevention_Site_Surgery = new SqlCommand(SQLStringInfectionPrevention_Site_Surgery))
        {
          SqlCommand_InfectionPrevention_Site_Surgery.Parameters.AddWithValue("@fkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          DataTable DataTable_InfectionPrevention_Site_Surgery;
          using (DataTable_InfectionPrevention_Site_Surgery = new DataTable())
          {
            DataTable_InfectionPrevention_Site_Surgery.Locale = CultureInfo.CurrentCulture;
            DataTable_InfectionPrevention_Site_Surgery = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_InfectionPrevention_Site_Surgery).Copy();
            GridView_ItemSurgery.DataSource = DataTable_InfectionPrevention_Site_Surgery;
            GridView_ItemSurgery.DataBind();
          }
        }
        //END InfectionPrevention_Site_Surgery


        string Email = "";
        string Print = "";
        string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 4";
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
          ((Button)FormView_InfectionPrevention_Form.FindControl("Button_ItemPrint")).Visible = false;
        }
        else
        {
          ((Button)FormView_InfectionPrevention_Form.FindControl("Button_ItemPrint")).Visible = true;
          ((Button)FormView_InfectionPrevention_Form.FindControl("Button_ItemPrint")).OnClientClick = "FormPrint('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Print", "InfoQuest_Print.aspx?PrintPage=Form_HAI&PrintValue=" + Request.QueryString["InfectionFormID"] + "") + "')";
        }

        if (Email == "False")
        {
          ((Button)FormView_InfectionPrevention_Form.FindControl("Button_ItemEmail")).Visible = false;
        }
        else
        {
          ((Button)FormView_InfectionPrevention_Form.FindControl("Button_ItemEmail")).Visible = true;
          ((Button)FormView_InfectionPrevention_Form.FindControl("Button_ItemEmail")).OnClientClick = "FormEmail('" + InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Email", "InfoQuest_Email.aspx?EmailPage=Form_InfectionPrevention&EmailValue=" + Request.QueryString["InfectionFormID"] + "") + "')";
        }

        Email = "";
        Print = "";
      }
    }


    //--START-- --Insert Controls--//
    protected void InsertRegisterPostBackControl()
    {
      DropDownList DropDownList_InsertInfectionType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertInfectionType");
      CheckBox CheckBox_InsertInvestigationCompleted = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertInvestigationCompleted");
      
      ScriptManager ScriptManager_Insert = ScriptManager.GetCurrent(Page);

      ScriptManager_Insert.RegisterPostBackControl(DropDownList_InsertInfectionType);
      ScriptManager_Insert.RegisterPostBackControl(CheckBox_InsertInvestigationCompleted);
    }

    protected void DropDownList_InsertInfectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertInfectionType = (DropDownList)sender;
      DropDownList DropDownList_InsertSSISubType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_InsertSSISubType");

      ToolkitScriptManager_InfectionPrevention.SetFocus(DropDownList_InsertInfectionType);

      DropDownList_InsertSSISubType.Items.Clear();

      SqlDataSource_InfectionPrevention_InsertSSISubType.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_InsertInfectionType.SelectedValue;

      DropDownList_InsertSSISubType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select SSI Sub Type", CultureInfo.CurrentCulture), ""));

      DropDownList_InsertSSISubType.DataBind();

      InsertRegisterPostBackControl();
    }

    protected void CheckBox_InsertInvestigationCompleted_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox CheckBox_InsertInvestigationCompleted = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_InsertInvestigationCompleted");
      TextBox TextBox_InsertInvestigationCompletedDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_InsertInvestigationCompletedDate");
      Button Button_InsertAdd = (Button)FormView_InfectionPrevention_Form.FindControl("Button_InsertAdd");

      ToolkitScriptManager_InfectionPrevention.SetFocus(Button_InsertAdd);

      if (CheckBox_InsertInvestigationCompleted.Checked == true)
      {
        TextBox_InsertInvestigationCompletedDate.Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }
      else
      {
        TextBox_InsertInvestigationCompletedDate.Text = "";
      }

      InsertRegisterPostBackControl();
    }

    protected void GridView_InsertInfectionPrevention_LabReport_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_InsertInfectionPrevention_LabReport_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_InsertInfectionPrevention_BedHistory_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_InsertInfectionPrevention_BedHistory_RowCreated(object sender, GridViewRowEventArgs e)
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

    public static string GetInsertBedHistory_To(object description, object roomNumber, object bedNumber)
    {
      string Description = "";

      Description = description + " (Room " + roomNumber + ", Bed " + bedNumber + ")";

      return Description;
    }

    public static string GetInsertSurgery_Procedure(object procedureDescription, object procedureCode)
    {
      string Description = "";

      Description = procedureDescription + " (" + procedureCode + ")";

      return Description;
    }

    public static string GetInsertSurgery_Theatre(object theatreDescription, object theatreCode)
    {
      string Description = "";

      Description = theatreDescription + " (" + theatreCode + ")";

      return Description;
    }

    public static string GetInsertSurgery_FinalDiagnosis(object finalDiagnosisDescription, object finalDiagnosis)
    {
      string Description = "";

      Description = finalDiagnosisDescription + " (" + finalDiagnosis + ")";

      return Description;
    }

    protected void GridView_InsertInfectionPrevention_Surgery_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_InsertInfectionPrevention_Surgery_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_InsertCaptured_OnClick(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_InsertClear_OnClick(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Form", "Form_InfectionPrevention.aspx"), false);
    }
    //---END--- --Insert Controls--//


    //--START-- --Edit Controls--//
    protected void EditRegisterPostBackControl()
    {
      Button Button_EditApprove = (Button)FormView_InfectionPrevention_Form.FindControl("Button_EditApprove");
      Button Button_EditReject = (Button)FormView_InfectionPrevention_Form.FindControl("Button_EditReject");

      ScriptManager ScriptManager_Edit = ScriptManager.GetCurrent(Page);

      ScriptManager_Edit.RegisterPostBackControl(Button_EditApprove);
      ScriptManager_Edit.RegisterPostBackControl(Button_EditReject);
    }

    protected void DropDownList_EditInfectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditInfectionType = (DropDownList)sender;
      DropDownList DropDownList_EditSSISubType = (DropDownList)FormView_InfectionPrevention_Form.FindControl("DropDownList_EditSSISubType");

      ToolkitScriptManager_InfectionPrevention.SetFocus(DropDownList_EditInfectionType);

      DropDownList_EditSSISubType.Items.Clear();

      SqlDataSource_InfectionPrevention_EditSSISubType.SelectParameters["ListItem_Parent"].DefaultValue = DropDownList_EditInfectionType.SelectedValue;

      DropDownList_EditSSISubType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Convert.ToString("Select SSI Sub Type", CultureInfo.CurrentCulture), ""));

      DropDownList_EditSSISubType.DataBind();

      EditRegisterPostBackControl();
    }

    protected void CheckBox_EditInvestigationCompleted_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox CheckBox_EditInvestigationCompleted = (CheckBox)FormView_InfectionPrevention_Form.FindControl("CheckBox_EditInvestigationCompleted");
      TextBox TextBox_EditInvestigationCompletedDate = (TextBox)FormView_InfectionPrevention_Form.FindControl("TextBox_EditInvestigationCompletedDate");
      Button Button_EditUpdate = (Button)FormView_InfectionPrevention_Form.FindControl("Button_EditUpdate");

      ToolkitScriptManager_InfectionPrevention.SetFocus(Button_EditUpdate);

      if (CheckBox_EditInvestigationCompleted.Checked == true)
      {
        TextBox_EditInvestigationCompletedDate.Text = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);
      }
      else
      {
        TextBox_EditInvestigationCompletedDate.Text = "";
      }

      EditRegisterPostBackControl();
    }

    protected void GridView_EditInfectionPrevention_Surgery_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_EditInfectionPrevention_Surgery_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_EditInfectionPrevention_BedHistory_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_EditInfectionPrevention_BedHistory_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_EditInfectionPrevention_LabReport_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_EditInfectionPrevention_LabReport_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_EditApprove_DataBinding(object sender, EventArgs e)
    {
      Button Button_EditApprove = (Button)sender;
      Button_EditApprove.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Approve the form');");
    }

    protected void Button_EditApprove_OnClick(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(Request.QueryString["InfectionFormID"]))
      {
        string SQLStringEditInfectionPrevention = "UPDATE tblInfectionPrevention SET sFormStatus = @sFormStatus , sApprovedRejectedBy = @sApprovedRejectedBy , dtApprovedRejected = @dtApprovedRejected , sRejectionReason = @sRejectionReason WHERE pkiInfectionFormID = @pkiInfectionFormID";
        using (SqlCommand SqlCommand_EditInfectionPrevention = new SqlCommand(SQLStringEditInfectionPrevention))
        {
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sFormStatus", "Approved");
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sApprovedRejectedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@dtApprovedRejected", DateTime.Now);
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sRejectionReason", DBNull.Value);
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@pkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention);
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Form", "Form_InfectionPrevention.aspx?InfectionFormID=" + Request.QueryString["InfectionFormID"] + "&s_FacilityId=" + Request.QueryString["s_FacilityId"] + "&s_PatientVisitNumber=" + Request.QueryString["s_PatientVisitNumber"] + ""), true);
      }
    }

    protected void Button_EditReject_DataBinding(object sender, EventArgs e)
    {
      Button Button_EditReject = (Button)sender;
      Button_EditReject.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to Reject the form');");
    }

    protected void Button_EditReject_OnClick(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(Request.QueryString["InfectionFormID"]))
      {
        string SQLStringEditInfectionPrevention = "UPDATE tblInfectionPrevention SET sFormStatus = @sFormStatus , sApprovedRejectedBy = @sApprovedRejectedBy , dtApprovedRejected = @dtApprovedRejected , sRejectionReason = @sRejectionReason WHERE pkiInfectionFormID = @pkiInfectionFormID";
        using (SqlCommand SqlCommand_EditInfectionPrevention = new SqlCommand(SQLStringEditInfectionPrevention))
        {
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sFormStatus", "Rejected");
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sApprovedRejectedBy", Request.ServerVariables["LOGON_USER"]);
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@dtApprovedRejected", DateTime.Now);
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@sRejectionReason", "Rejected by " + Request.ServerVariables["LOGON_USER"]);
          SqlCommand_EditInfectionPrevention.Parameters.AddWithValue("@pkiInfectionFormID", Request.QueryString["InfectionFormID"]);
          InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_EditInfectionPrevention);
        }

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Form", "Form_InfectionPrevention.aspx?InfectionFormID=" + Request.QueryString["InfectionFormID"] + "&s_FacilityId=" + Request.QueryString["s_FacilityId"] + "&s_PatientVisitNumber=" + Request.QueryString["s_PatientVisitNumber"] + ""), true);
      }
    }

    protected void Button_EditPrint_Click(object sender, EventArgs e)
    {
      Button_EditPrintClicked = true;
    }

    protected void Button_EditEmail_Click(object sender, EventArgs e)
    {
      Button_EditEmailClicked = true;
    }

    protected void Button_EditCaptured_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_EditClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Form", "Form_InfectionPrevention.aspx"), false);
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Edit Controls--//


    //--START-- --Item Controls--//
    public static string GetItemVisitDiagnosis(object code, object description)
    {
      string Description = "";

      Description = code + " - " + description + ")";

      return Description;
    }

    protected void GridView_ItemVisitDiagnosis_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_ItemVisitDiagnosis_RowCreated(object sender, GridViewRowEventArgs e)
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIM")]
    protected void GridView_ItemPIM_DataBound(object sender, EventArgs e)
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PIM")]
    protected void GridView_ItemPIM_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_ItemBundleType_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_ItemBundleType_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_ItemLabReport_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_ItemLabReport_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_ItemReportableDiseases_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_ItemReportableDiseases_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_ItemBedHistory_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_ItemBedHistory_RowCreated(object sender, GridViewRowEventArgs e)
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PCCF")]
    protected void GridView_ItemPCCF_DataBound(object sender, EventArgs e)
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PCCF")]
    protected void GridView_ItemPCCF_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void GridView_ItemSurgery_DataBound(object sender, EventArgs e)
    {
    }

    protected void GridView_ItemSurgery_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void Button_ItemCaptured_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_ItemClear_Click(object sender, EventArgs e)
    {
      Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Infection Prevention Form", "Form_InfectionPrevention.aspx"), false);
    }
    //---END--- --Item Controls--//
  }
}