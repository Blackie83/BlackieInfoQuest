using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Isidima_Admin : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString() + " : Visit Administration", CultureInfo.CurrentCulture);
          Label_Info.Text = Convert.ToString("Information", CultureInfo.CurrentCulture);
          Label_Link.Text = Convert.ToString("Linked " + (InfoQuestWCF.InfoQuest_All.All_FormName("27")).ToString() + "s", CultureInfo.CurrentCulture);
          Label_LinkGrid.Text = Convert.ToString("List of Linked " + (InfoQuestWCF.InfoQuest_All.All_FormName("27")).ToString() + "s", CultureInfo.CurrentCulture);
          Label_Discharge.Text = Convert.ToString("Discharge Patient in " + (InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_Period.Text = Convert.ToString("Patient Assessment Period", CultureInfo.CurrentCulture);

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_Isidima_PatientVisitNumber"] != null)
          {
            TableForm.Visible = true;
            TableSplit1.Visible = true;
            TableLink.Visible = true;
            TableSplit2.Visible = true;
            TableDischarge.Visible = true;
            TableSplit3.Visible = true;
            TablePeriod.Visible = true;
            TableSplit4.Visible = true;


            if (Request.QueryString["Section"] == null)
            {
              ((Label)FormView_Isidima_AdminDischarge.FindControl("Label_ValidCapture")).Text = "";
              ((Label)FormView_Isidima_AdminAssessmentPeriod.FindControl("Label_ValidCapture")).Text = "";
            }
            else if (Request.QueryString["Section"] == "Link")
            {
              ((Label)FormView_Isidima_AdminDischarge.FindControl("Label_ValidCapture")).Text = "";
              ((Label)FormView_Isidima_AdminAssessmentPeriod.FindControl("Label_ValidCapture")).Text = "";
            }
            else if (Request.QueryString["Section"] == "Discharge")
            {
              CheckBox CheckBox_EditAdminDischarge_Patient = (CheckBox)FormView_Isidima_AdminDischarge.FindControl("CheckBox_EditAdminDischarge_Patient");
              if (CheckBox_EditAdminDischarge_Patient.Checked == true)
              {
                ((Label)FormView_Isidima_AdminDischarge.FindControl("Label_ValidCapture")).Text = Convert.ToString("Patient has been removed from Next Assessment list", CultureInfo.CurrentCulture);
                ((Label)FormView_Isidima_AdminAssessmentPeriod.FindControl("Label_ValidCapture")).Text = "";
              }
              else
              {
                ((Label)FormView_Isidima_AdminDischarge.FindControl("Label_ValidCapture")).Text = Convert.ToString("Patient has been added to Next Assessment list", CultureInfo.CurrentCulture);
                ((Label)FormView_Isidima_AdminAssessmentPeriod.FindControl("Label_ValidCapture")).Text = "";
              }
            }
            else if (Request.QueryString["Section"] == "Period")
            {
              ((Label)FormView_Isidima_AdminDischarge.FindControl("Label_ValidCapture")).Text = "";
              ((Label)FormView_Isidima_AdminAssessmentPeriod.FindControl("Label_ValidCapture")).Text = Convert.ToString("Patient Assessment Period has been updated", CultureInfo.CurrentCulture);
            }


            FormView_Isidima_AdminDischarge.ChangeMode(FormViewMode.Edit);
            FormView_Isidima_AdminAssessmentPeriod.ChangeMode(FormViewMode.Edit);

            if (Request.QueryString["Isidima_AdminLink_Id"] == null)
            {
              FormView_Isidima_AdminLink.ChangeMode(FormViewMode.Insert);

              DropDownList_InsertLinkedFacility_Id_SelectedIndexChanged(sender, e);
              DropDownList_InsertLinkedPatientVisitNumber_SelectedIndexChanged(sender, e);
            }
            else
            {
              FormView_Isidima_AdminLink.ChangeMode(FormViewMode.Edit);

              DropDownList_EditLinkedFacility_Id_SelectedIndexChanged(sender, e);
              DropDownList_EditLinkedPatientVisitNumber_SelectedIndexChanged(sender, e);
            }


            Session["FacilityFacilityDisplayName"] = "";
            Session["IsidimaPIPatientVisitNumber"] = "";
            Session["IsidimaPIPatientName"] = "";
            Session["IsidimaPIPatientAge"] = "";
            Session["IsidimaPIPatientDateOfAdmission"] = "";
            Session["IsidimaPIPatientDateOfDischarge"] = "";
            string SQLStringPI = "SELECT Facility_FacilityDisplayName , Isidima_PI_PatientVisitNumber , Isidima_PI_PatientName , Isidima_PI_PatientAge , Isidima_PI_PatientDateOfAdmission , Isidima_PI_PatientDateOfDischarge FROM vForm_Isidima_PatientInformation WHERE Facility_Id = @Facility_Id AND Isidima_PI_PatientVisitNumber = @Isidima_PI_PatientVisitNumber";
            using (SqlCommand SqlCommand_PI = new SqlCommand(SQLStringPI))
            {
              SqlCommand_PI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
              SqlCommand_PI.Parameters.AddWithValue("@Isidima_PI_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
              DataTable DataTable_PI;
              using (DataTable_PI = new DataTable())
              {
                DataTable_PI.Locale = CultureInfo.CurrentCulture;
                DataTable_PI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PI).Copy();
                if (DataTable_PI.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row in DataTable_PI.Rows)
                  {
                    Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
                    Session["IsidimaPIPatientVisitNumber"] = DataRow_Row["Isidima_PI_PatientVisitNumber"];
                    Session["IsidimaPIPatientName"] = DataRow_Row["Isidima_PI_PatientName"];
                    Session["IsidimaPIPatientAge"] = DataRow_Row["Isidima_PI_PatientAge"];
                    Session["IsidimaPIPatientDateOfAdmission"] = DataRow_Row["Isidima_PI_PatientDateOfAdmission"];
                    Session["IsidimaPIPatientDateOfDischarge"] = DataRow_Row["Isidima_PI_PatientDateOfDischarge"];
                  }
                }
              }
            }

            Label_Facility.Text = Session["FacilityFacilityDisplayName"].ToString();
            Label_VisitNumber.Text = Session["IsidimaPIPatientVisitNumber"].ToString();
            Label_PatientName.Text = Session["IsidimaPIPatientName"].ToString();


            if (FormView_Isidima_AdminLink.CurrentMode == FormViewMode.Insert)
            {
              ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_InsertLinkedFacility_Id")).Attributes.Add("OnChange", "Validation_Link();");
              ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_InsertLinkedPatientVisitNumber")).Attributes.Add("OnChange", "Validation_Link();");

              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIFacility")).Text = Session["FacilityFacilityDisplayName"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIVisitNumber")).Text = Session["IsidimaPIPatientVisitNumber"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIName")).Text = Session["IsidimaPIPatientName"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIAge")).Text = Session["IsidimaPIPatientAge"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIDateAdmission")).Text = Session["IsidimaPIPatientDateOfAdmission"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIDateDischarge")).Text = Session["IsidimaPIPatientDateOfDischarge"].ToString();
            }

            if (FormView_Isidima_AdminLink.CurrentMode == FormViewMode.Edit)
            {
              ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_EditLinkedFacility_Id")).Attributes.Add("OnChange", "Validation_Link();");
              ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_EditLinkedPatientVisitNumber")).Attributes.Add("OnChange", "Validation_Link();");

              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIFacility")).Text = Session["FacilityFacilityDisplayName"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIVisitNumber")).Text = Session["IsidimaPIPatientVisitNumber"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIName")).Text = Session["IsidimaPIPatientName"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIAge")).Text = Session["IsidimaPIPatientAge"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIDateAdmission")).Text = Session["IsidimaPIPatientDateOfAdmission"].ToString();
              ((Label)FormView_Isidima_AdminLink.FindControl("Label_PIDateDischarge")).Text = Session["IsidimaPIPatientDateOfDischarge"].ToString();
            }

            if (FormView_Isidima_AdminAssessmentPeriod.CurrentMode == FormViewMode.Edit)
            {
              Session["Assessments"] = "";
              string SQLStringNumberOfAssessments = "SELECT COUNT(Facility_Id) AS Assessments FROM vForm_Isidima WHERE Isidima_Category_IsActive = 1 AND Facility_Id = @Facility_Id AND Isidima_Category_PatientVisitNumber = @Isidima_Category_PatientVisitNumber GROUP BY Isidima_Category_PatientVisitNumber , Facility_Id";
              using (SqlCommand SqlCommand_NumberOfAssessments = new SqlCommand(SQLStringNumberOfAssessments))
              {
                SqlCommand_NumberOfAssessments.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_NumberOfAssessments.Parameters.AddWithValue("@Isidima_Category_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
                DataTable DataTable_NumberOfAssessments;
                using (DataTable_NumberOfAssessments = new DataTable())
                {
                  DataTable_NumberOfAssessments.Locale = CultureInfo.CurrentCulture;
                  DataTable_NumberOfAssessments = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_NumberOfAssessments).Copy();
                  if (DataTable_NumberOfAssessments.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_NumberOfAssessments.Rows)
                    {
                      Session["Assessments"] = DataRow_Row["Assessments"];
                    }
                  }
                }
              }

              ((Label)FormView_Isidima_AdminAssessmentPeriod.FindControl("Label_EditNumberOfAssessments")).Text = Session["Assessments"].ToString();
              Session["Assessments"] = "";
            }

            Session["FacilityFacilityDisplayName"] = "";
            Session["IsidimaPIPatientVisitNumber"] = "";
            Session["IsidimaPIPatientName"] = "";
            Session["IsidimaPIPatientAge"] = "";
            Session["IsidimaPIPatientDateOfAdmission"] = "";
            Session["IsidimaPIPatientDateOfDischarge"] = "";
          }
          else
          {
            TableForm.Visible = false;
            TableSplit1.Visible = false;
            TableLink.Visible = false;
            TableSplit2.Visible = false;
            TableDischarge.Visible = false;
            TableSplit3.Visible = false;
            TablePeriod.Visible = false;
            TableSplit4.Visible = false;
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
          SQLStringSecurity = "";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('27')) AND (((Facility_Id IN (@Facility_Id) AND (SecurityRole_Rank IN (3))) OR (SecurityRole_Rank = 1)))";
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
      SqlDataSource_Isidima_AdminLink.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_AdminLink.InsertCommand="INSERT INTO [InfoQuest_Form_Isidima_AdminLink] (Facility_Id ,Isidima_AdminLink_PatientVisitNumber ,Isidima_AdminLink_LinkedFacility_Id ,Isidima_AdminLink_LinkedPatientVisitNumber ,Isidima_AdminLink_CreatedDate ,Isidima_AdminLink_CreatedBy ,Isidima_AdminLink_ModifiedDate ,Isidima_AdminLink_ModifiedBy ,Isidima_AdminLink_History ,Isidima_AdminLink_IsActive) VALUES (@Facility_Id ,@Isidima_AdminLink_PatientVisitNumber ,@Isidima_AdminLink_LinkedFacility_Id ,@Isidima_AdminLink_LinkedPatientVisitNumber ,@Isidima_AdminLink_CreatedDate ,@Isidima_AdminLink_CreatedBy ,@Isidima_AdminLink_ModifiedDate ,@Isidima_AdminLink_ModifiedBy ,@Isidima_AdminLink_History ,@Isidima_AdminLink_IsActive)";
      SqlDataSource_Isidima_AdminLink.SelectCommand="SELECT * FROM [InfoQuest_Form_Isidima_AdminLink] WHERE ([Isidima_AdminLink_Id] = @Isidima_AdminLink_Id)";
      SqlDataSource_Isidima_AdminLink.UpdateCommand="UPDATE [InfoQuest_Form_Isidima_AdminLink] SET [Isidima_AdminLink_LinkedFacility_Id] = @Isidima_AdminLink_LinkedFacility_Id , [Isidima_AdminLink_LinkedPatientVisitNumber] = @Isidima_AdminLink_LinkedPatientVisitNumber , [Isidima_AdminLink_ModifiedDate] = @Isidima_AdminLink_ModifiedDate , [Isidima_AdminLink_ModifiedBy] = @Isidima_AdminLink_ModifiedBy , [Isidima_AdminLink_History] = @Isidima_AdminLink_History , [Isidima_AdminLink_IsActive] = @Isidima_AdminLink_IsActive WHERE ([Isidima_AdminLink_Id] = @Isidima_AdminLink_Id)";
      SqlDataSource_Isidima_AdminLink.InsertParameters.Clear();
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Isidima_AdminLink_PatientVisitNumber", TypeCode.String, Request.QueryString["s_Isidima_PatientVisitNumber"]);
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Isidima_AdminLink_LinkedFacility_Id", TypeCode.Int32, "");
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Isidima_AdminLink_LinkedPatientVisitNumber", TypeCode.String, "");
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Isidima_AdminLink_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Isidima_AdminLink_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Isidima_AdminLink_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Isidima_AdminLink_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Isidima_AdminLink_History", TypeCode.String, "");
      SqlDataSource_Isidima_AdminLink.InsertParameters["Isidima_AdminLink_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_AdminLink.InsertParameters.Add("Isidima_AdminLink_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Isidima_AdminLink.SelectParameters.Clear();
      SqlDataSource_Isidima_AdminLink.SelectParameters.Add("Isidima_AdminLink_Id", TypeCode.Int32, Request.QueryString["Isidima_AdminLink_Id"]);
      SqlDataSource_Isidima_AdminLink.UpdateParameters.Clear();
      SqlDataSource_Isidima_AdminLink.UpdateParameters.Add("Isidima_AdminLink_LinkedFacility_Id", TypeCode.Int32, "");
      SqlDataSource_Isidima_AdminLink.UpdateParameters.Add("Isidima_AdminLink_LinkedPatientVisitNumber", TypeCode.String, "");
      SqlDataSource_Isidima_AdminLink.UpdateParameters.Add("Isidima_AdminLink_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_AdminLink.UpdateParameters.Add("Isidima_AdminLink_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_AdminLink.UpdateParameters.Add("Isidima_AdminLink_History", TypeCode.String, "");
      SqlDataSource_Isidima_AdminLink.UpdateParameters.Add("Isidima_AdminLink_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Isidima_AdminLink.UpdateParameters.Add("Isidima_AdminLink_Id", TypeCode.Int32, "");

      SqlDataSource_Isidima_AdminLink_InsertLinkedFacility_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_AdminLink_InsertLinkedFacility_Id.SelectCommand = "SELECT DISTINCT Facility_Id , Facility_FacilityDisplayName FROM vForm_Isidima_Category WHERE Isidima_Category_IsActive = 1 ORDER BY Facility_FacilityDisplayName";
      SqlDataSource_Isidima_AdminLink_InsertLinkedFacility_Id.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Isidima_AdminLink_EditLinkedFacility_Id.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_AdminLink_InsertLinkedFacility_Id.SelectCommand = "SELECT DISTINCT Facility_Id , Facility_FacilityDisplayName FROM vForm_Isidima_Category WHERE Isidima_Category_IsActive = 1 ORDER BY Facility_FacilityDisplayName";
      SqlDataSource_Isidima_AdminLink_InsertLinkedFacility_Id.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSource_Isidima_AdminLink_EditLinkedPatientVisitNumber.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_AdminLink_EditLinkedPatientVisitNumber.SelectCommand = "SELECT DISTINCT Facility_Id , Facility_FacilityDisplayName , Isidima_Category_PatientVisitNumber FROM vForm_Isidima_Category WHERE Isidima_Category_PatientVisitNumber NOT IN (@Current_PatientVisitNumber) AND CAST(Facility_Id AS NVARCHAR(MAX)) + '::' + CAST(Isidima_Category_PatientVisitNumber AS NVARCHAR(MAX)) IN (SELECT CAST(Isidima_AdminLink_LinkedFacility_Id AS NVARCHAR(MAX)) + '::' + CAST(Isidima_AdminLink_LinkedPatientVisitNumber AS NVARCHAR(MAX)) FROM InfoQuest_Form_Isidima_AdminLink WHERE Isidima_AdminLink_PatientVisitNumber IN (@Current_PatientVisitNumber) AND Isidima_AdminLink_Id IN (@Isidima_AdminLink_Id)) ORDER BY Facility_FacilityDisplayName";
      SqlDataSource_Isidima_AdminLink_EditLinkedPatientVisitNumber.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Isidima_AdminLink_EditLinkedPatientVisitNumber.SelectParameters.Clear();
      SqlDataSource_Isidima_AdminLink_EditLinkedPatientVisitNumber.SelectParameters.Add("Current_PatientVisitNumber", TypeCode.String, Request.QueryString["s_Isidima_PatientVisitNumber"]);
      SqlDataSource_Isidima_AdminLink_EditLinkedPatientVisitNumber.SelectParameters.Add("Isidima_AdminLink_Id", TypeCode.Int32, Request.QueryString["Isidima_AdminLink_Id"]);

      SqlDataSource_Isidima_AdminLink_List.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_AdminLink_List.SelectCommand = "spForm_Get_Isidima_AdminLink_List";
      SqlDataSource_Isidima_AdminLink_List.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Isidima_AdminLink_List.CancelSelectOnNullParameter = false;
      SqlDataSource_Isidima_AdminLink_List.SelectParameters.Clear();
      SqlDataSource_Isidima_AdminLink_List.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_AdminLink_List.SelectParameters.Add("PatientVisitNumber", TypeCode.Int32, Request.QueryString["s_Isidima_PatientVisitNumber"]);

      SqlDataSource_Isidima_AdminDischarge.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_AdminDischarge.DeleteCommand="DELETE FROM [InfoQuest_Form_Isidima_AdminDischarge] WHERE ([Facility_Id] = @Facility_Id AND [Isidima_AdminDischarge_PatientVisitNumber] = @Isidima_AdminDischarge_PatientVisitNumber)";
      SqlDataSource_Isidima_AdminDischarge.InsertCommand="INSERT INTO [InfoQuest_Form_Isidima_AdminDischarge] (Facility_Id ,Isidima_AdminDischarge_PatientVisitNumber ,Isidima_AdminDischarge_Patient ,Isidima_AdminDischarge_CreatedDate ,Isidima_AdminDischarge_CreatedBy ,Isidima_AdminDischarge_ModifiedDate ,Isidima_AdminDischarge_ModifiedBy ,Isidima_AdminDischarge_History) VALUES (@Facility_Id ,@Isidima_AdminDischarge_PatientVisitNumber ,@Isidima_AdminDischarge_Patient ,@Isidima_AdminDischarge_CreatedDate ,@Isidima_AdminDischarge_CreatedBy ,@Isidima_AdminDischarge_ModifiedDate ,@Isidima_AdminDischarge_ModifiedBy ,@Isidima_AdminDischarge_History)";
      SqlDataSource_Isidima_AdminDischarge.SelectCommand="SELECT * FROM [InfoQuest_Form_Isidima_AdminDischarge] WHERE ([Facility_Id] = @Facility_Id AND [Isidima_AdminDischarge_PatientVisitNumber] = @Isidima_AdminDischarge_PatientVisitNumber)";
      SqlDataSource_Isidima_AdminDischarge.UpdateCommand="UPDATE [InfoQuest_Form_Isidima_AdminDischarge] SET [Isidima_AdminDischarge_Patient] = @Isidima_AdminDischarge_Patient , [Isidima_AdminDischarge_ModifiedDate] = @Isidima_AdminDischarge_ModifiedDate , [Isidima_AdminDischarge_ModifiedBy] = @Isidima_AdminDischarge_ModifiedBy , [Isidima_AdminDischarge_History] = @Isidima_AdminDischarge_History WHERE ([Facility_Id] = @Facility_Id AND [Isidima_AdminDischarge_PatientVisitNumber] = @Isidima_AdminDischarge_PatientVisitNumber)";
      SqlDataSource_Isidima_AdminDischarge.DeleteParameters.Clear();
      SqlDataSource_Isidima_AdminDischarge.DeleteParameters.Add("Facility_Id", TypeCode.Int32, "");
      SqlDataSource_Isidima_AdminDischarge.DeleteParameters.Add("Isidima_AdminDischarge_PatientVisitNumber", TypeCode.String, "");
      SqlDataSource_Isidima_AdminDischarge.SelectParameters.Clear();
      SqlDataSource_Isidima_AdminDischarge.SelectParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_AdminDischarge.SelectParameters.Add("Isidima_AdminDischarge_PatientVisitNumber", TypeCode.String, Request.QueryString["s_Isidima_PatientVisitNumber"]);
      SqlDataSource_Isidima_AdminDischarge.UpdateParameters.Clear();
      SqlDataSource_Isidima_AdminDischarge.UpdateParameters.Add("Isidima_AdminDischarge_Patient", TypeCode.Int32, "");
      SqlDataSource_Isidima_AdminDischarge.UpdateParameters.Add("Isidima_AdminDischarge_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_AdminDischarge.UpdateParameters.Add("Isidima_AdminDischarge_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_AdminDischarge.UpdateParameters.Add("Isidima_AdminDischarge_History", TypeCode.String, "");
      SqlDataSource_Isidima_AdminDischarge.UpdateParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_AdminDischarge.UpdateParameters.Add("Isidima_AdminDischarge_PatientVisitNumber", TypeCode.String, Request.QueryString["s_Isidima_PatientVisitNumber"]);

      SqlDataSource_Isidima_AdminAssessmentPeriod.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_AdminAssessmentPeriod.DeleteCommand="DELETE FROM [InfoQuest_Form_Isidima_AdminAssessmentPeriod] WHERE ([Facility_Id] = @Facility_Id AND [Isidima_AdminAssessmentPeriod_PatientVisitNumber] = @Isidima_AdminAssessmentPeriod_PatientVisitNumber)";
      SqlDataSource_Isidima_AdminAssessmentPeriod.SelectCommand="SELECT * FROM [InfoQuest_Form_Isidima_AdminAssessmentPeriod] WHERE ([Facility_Id] = @Facility_Id AND [Isidima_AdminAssessmentPeriod_PatientVisitNumber] = @Isidima_AdminAssessmentPeriod_PatientVisitNumber)";
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateCommand="UPDATE [InfoQuest_Form_Isidima_AdminAssessmentPeriod] SET [Isidima_AdminAssessmentPeriod_Period1] = @Isidima_AdminAssessmentPeriod_Period1 , [Isidima_AdminAssessmentPeriod_Period2] = @Isidima_AdminAssessmentPeriod_Period2 , [Isidima_AdminAssessmentPeriod_Period3] = @Isidima_AdminAssessmentPeriod_Period3 , [Isidima_AdminAssessmentPeriod_ModifiedDate] = @Isidima_AdminAssessmentPeriod_ModifiedDate , [Isidima_AdminAssessmentPeriod_ModifiedBy] = @Isidima_AdminAssessmentPeriod_ModifiedBy , [Isidima_AdminAssessmentPeriod_History] = @Isidima_AdminAssessmentPeriod_History WHERE ([Facility_Id] = @Facility_Id AND [Isidima_AdminAssessmentPeriod_PatientVisitNumber] = @Isidima_AdminAssessmentPeriod_PatientVisitNumber)";
      SqlDataSource_Isidima_AdminAssessmentPeriod.DeleteParameters.Clear();
      SqlDataSource_Isidima_AdminAssessmentPeriod.DeleteParameters.Add("Facility_Id", TypeCode.Int32, "");
      SqlDataSource_Isidima_AdminAssessmentPeriod.DeleteParameters.Add("Isidima_AdminAssessmentPeriod_PatientVisitNumber", TypeCode.String, "");
      SqlDataSource_Isidima_AdminAssessmentPeriod.SelectParameters.Clear();
      SqlDataSource_Isidima_AdminAssessmentPeriod.SelectParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_AdminAssessmentPeriod.SelectParameters.Add("Isidima_AdminAssessmentPeriod_PatientVisitNumber", TypeCode.String, Request.QueryString["s_Isidima_PatientVisitNumber"]);
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateParameters.Clear();
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateParameters.Add("Isidima_AdminAssessmentPeriod_Period1", TypeCode.Int32, "");
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateParameters.Add("Isidima_AdminAssessmentPeriod_Period2", TypeCode.Int32, "");
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateParameters.Add("Isidima_AdminAssessmentPeriod_Period3", TypeCode.Int32, "");
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateParameters.Add("Isidima_AdminAssessmentPeriod_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateParameters.Add("Isidima_AdminAssessmentPeriod_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateParameters.Add("Isidima_AdminAssessmentPeriod_History", TypeCode.String, "");
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateParameters.Add("Facility_Id", TypeCode.Int32, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_AdminAssessmentPeriod.UpdateParameters.Add("Isidima_AdminAssessmentPeriod_PatientVisitNumber", TypeCode.String, Request.QueryString["s_Isidima_PatientVisitNumber"]);

      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period1.SelectCommand = "SELECT * FROM ( SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 27 AND ListCategory_Id = 75 AND ListItem_Parent = -1 UNION SELECT Administration_ListItem.ListItem_Name FROM InfoQuest_Form_Isidima_AdminAssessmentPeriod , Administration_ListItem WHERE CAST(InfoQuest_Form_Isidima_AdminAssessmentPeriod.Isidima_AdminAssessmentPeriod_Period1 AS NVARCHAR(MAX)) = CAST(Administration_ListItem.ListItem_Name AS NVARCHAR(MAX)) AND Facility_Id = @Facility_Id AND Isidima_AdminAssessmentPeriod_PatientVisitNumber = @Isidima_AdminAssessmentPeriod_PatientVisitNumber ) AS TempTableAll ORDER BY CAST(TempTableAll.ListItem_Name AS INT)";
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period1.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period1.SelectParameters.Clear();
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period1.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period1.SelectParameters.Add("Isidima_AdminAssessmentPeriod_PatientVisitNumber", TypeCode.Int32, Request.QueryString["s_Isidima_PatientVisitNumber"]);

      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period2.SelectCommand = "SELECT * FROM ( SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 27 AND ListCategory_Id = 75 AND ListItem_Parent = -1 UNION SELECT Administration_ListItem.ListItem_Name FROM InfoQuest_Form_Isidima_AdminAssessmentPeriod , Administration_ListItem WHERE CAST(InfoQuest_Form_Isidima_AdminAssessmentPeriod.Isidima_AdminAssessmentPeriod_Period2 AS NVARCHAR(MAX)) = CAST(Administration_ListItem.ListItem_Name AS NVARCHAR(MAX)) AND Facility_Id = @Facility_Id AND Isidima_AdminAssessmentPeriod_PatientVisitNumber = @Isidima_AdminAssessmentPeriod_PatientVisitNumber ) AS TempTableAll ORDER BY CAST(TempTableAll.ListItem_Name AS INT)";
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period2.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period2.SelectParameters.Clear();
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period2.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period2.SelectParameters.Add("Isidima_AdminAssessmentPeriod_PatientVisitNumber", TypeCode.Int32, Request.QueryString["s_Isidima_PatientVisitNumber"]);

      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period3.SelectCommand = "SELECT * FROM ( SELECT ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 27 AND ListCategory_Id = 75 AND ListItem_Parent = -1 UNION SELECT Administration_ListItem.ListItem_Name FROM InfoQuest_Form_Isidima_AdminAssessmentPeriod , Administration_ListItem WHERE CAST(InfoQuest_Form_Isidima_AdminAssessmentPeriod.Isidima_AdminAssessmentPeriod_Period3 AS NVARCHAR(MAX)) = CAST(Administration_ListItem.ListItem_Name AS NVARCHAR(MAX)) AND Facility_Id = @Facility_Id AND Isidima_AdminAssessmentPeriod_PatientVisitNumber = @Isidima_AdminAssessmentPeriod_PatientVisitNumber ) AS TempTableAll ORDER BY CAST(TempTableAll.ListItem_Name AS INT)";
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period3.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period3.SelectParameters.Clear();
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period3.SelectParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_EditAdminAssessmentPeriod_Period3.SelectParameters.Add("Isidima_AdminAssessmentPeriod_PatientVisitNumber", TypeCode.Int32, Request.QueryString["s_Isidima_PatientVisitNumber"]);    
    }


    protected void Button_GoToForm_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }


    protected void SqlDataSource_Isidima_AdminLink_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima_Admin.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Section=Link#Link", false);
    }

    protected void SqlDataSource_Isidima_AdminLink_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima_Admin.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Section=Link#Link", false);
        }
      }
    }

    protected void FormView_Isidima_AdminLink_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima_Admin.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Section=Link#Link", false);
        }
      }
    }

    protected void FormView_Isidima_AdminLink_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_AdminLink.InsertParameters["Isidima_AdminLink_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_AdminLink.InsertParameters["Isidima_AdminLink_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_AdminLink.InsertParameters["Isidima_AdminLink_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_AdminLink.InsertParameters["Isidima_AdminLink_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_AdminLink.InsertParameters["Isidima_AdminLink_History"].DefaultValue = "";
          SqlDataSource_Isidima_AdminLink.InsertParameters["Isidima_AdminLink_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected void FormView_Isidima_AdminLink_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaAdminLinkModifiedDate"] = e.OldValues["Isidima_AdminLink_ModifiedDate"];
        object OLDIsidimaAdminLinkModifiedDate = Session["OLDIsidimaAdminLinkModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaAdminLinkModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareAdminLink = (DataView)SqlDataSource_Isidima_AdminLink.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareAdminLink = DataView_CompareAdminLink[0];
        Session["DBIsidimaAdminLinkModifiedDate"] = Convert.ToString(DataRowView_CompareAdminLink["Isidima_AdminLink_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaAdminLinkModifiedBy"] = Convert.ToString(DataRowView_CompareAdminLink["Isidima_AdminLink_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaAdminLinkModifiedDate = Session["DBIsidimaAdminLinkModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaAdminLinkModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_Isidima_AdminLink.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_Isidima_AdminLink.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaAdminLinkModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_AdminLink_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_AdminLink_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_AdminLink", "Isidima_AdminLink_Id = " + Request.QueryString["Isidima_AdminLink_Id"]);

            DataView DataView_AdminLink = (DataView)SqlDataSource_Isidima_AdminLink.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_AdminLink = DataView_AdminLink[0];
            Session["IsidimaAdminLinkHistory"] = Convert.ToString(DataRowView_AdminLink["Isidima_AdminLink_History"], CultureInfo.CurrentCulture);

            Session["IsidimaAdminLinkHistory"] = Session["History"].ToString() + Session["IsidimaAdminLinkHistory"].ToString();
            e.NewValues["Isidima_AdminLink_History"] = Session["IsidimaAdminLinkHistory"].ToString();

            Session["IsidimaAdminLinkHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDIsidimaAdminLinkModifiedDate"] = "";
        Session["DBIsidimaAdminLinkModifiedDate"] = "";
        Session["DBIsidimaAdminLinkModifiedBy"] = "";
      }
    }

    protected void DropDownList_InsertLinkedFacility_Id_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertLinkedFacility_Id = ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_InsertLinkedFacility_Id"));
      DropDownList DropDownList_InsertLinkedPatientVisitNumber = ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_InsertLinkedPatientVisitNumber"));

      DropDownList_InsertLinkedPatientVisitNumber.Items.Clear();

      ListItem ListItem_NoSelection = new ListItem();
      ListItem_NoSelection.Value = "";
      ListItem_NoSelection.Text = Convert.ToString("Select Visit Number", CultureInfo.CurrentCulture);
      DropDownList_InsertLinkedPatientVisitNumber.Items.Add(ListItem_NoSelection);

      ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIName")).Text = "";
      ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIAge")).Text = "";
      ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIDateAdmission")).Text = "";
      ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIDateDischarge")).Text = "";

      if (!string.IsNullOrEmpty(DropDownList_InsertLinkedFacility_Id.SelectedValue.ToString()))
      {
        Session["FacilityId"] = "";
        Session["FacilityFacilityDisplayName"] = "";
        Session["IsidimaPIPatientVisitNumber"] = "";
        string SQLStringPI = "SELECT DISTINCT Facility_Id , Facility_FacilityDisplayName , Isidima_Category_PatientVisitNumber FROM vForm_Isidima_Category WHERE Facility_Id = @Facility_Id AND Isidima_Category_PatientVisitNumber NOT IN (@Current_PatientVisitNumber) AND CAST(Facility_Id AS NVARCHAR(MAX)) + '::' + CAST(Isidima_Category_PatientVisitNumber AS NVARCHAR(MAX)) NOT IN (SELECT CAST(Isidima_AdminLink_LinkedFacility_Id AS NVARCHAR(MAX)) + '::' + CAST(Isidima_AdminLink_LinkedPatientVisitNumber AS NVARCHAR(MAX)) FROM InfoQuest_Form_Isidima_AdminLink WHERE Facility_Id = @Current_Facility_Id AND Isidima_AdminLink_PatientVisitNumber IN (@Current_PatientVisitNumber)) ORDER BY Facility_FacilityDisplayName";
        using (SqlCommand SqlCommand_PI = new SqlCommand(SQLStringPI))
        {
          SqlCommand_PI.Parameters.AddWithValue("@Facility_Id", DropDownList_InsertLinkedFacility_Id.SelectedValue.ToString());
          SqlCommand_PI.Parameters.AddWithValue("@Current_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
          SqlCommand_PI.Parameters.AddWithValue("@Current_Facility_Id", Request.QueryString["s_Facility_Id"]);
          DataTable DataTable_PI;
          using (DataTable_PI = new DataTable())
          {
            DataTable_PI.Locale = CultureInfo.CurrentCulture;
            DataTable_PI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PI).Copy();
            if (DataTable_PI.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PI.Rows)
              {
                Session["FacilityId"] = DataRow_Row["Facility_Id"];
                Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
                Session["IsidimaPIPatientVisitNumber"] = DataRow_Row["Isidima_Category_PatientVisitNumber"];

                DropDownList_InsertLinkedPatientVisitNumber.Items.Add(Session["IsidimaPIPatientVisitNumber"].ToString());
              }
            }
          }
        }
        Session["FacilityId"] = "";
        Session["FacilityFacilityDisplayName"] = "";
        Session["IsidimaPIPatientVisitNumber"] = "";
      }
    }

    protected void DropDownList_InsertLinkedPatientVisitNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_InsertLinkedFacility_Id = ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_InsertLinkedFacility_Id"));
      DropDownList DropDownList_InsertLinkedPatientVisitNumber = ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_InsertLinkedPatientVisitNumber"));

      if (!string.IsNullOrEmpty(DropDownList_InsertLinkedFacility_Id.SelectedValue.ToString()) && !string.IsNullOrEmpty(DropDownList_InsertLinkedPatientVisitNumber.SelectedValue.ToString()))
      {
        Session["IsidimaPIPatientName"] = "";
        Session["IsidimaPIPatientAge"] = "";
        Session["IsidimaPIPatientDateOfAdmission"] = "";
        Session["IsidimaPIPatientDateOfDischarge"] = "";
        string SQLStringPI = "SELECT Isidima_PI_PatientName , Isidima_PI_PatientAge , Isidima_PI_PatientDateOfAdmission , Isidima_PI_PatientDateOfDischarge FROM vForm_Isidima_PatientInformation WHERE Facility_Id = @Facility_Id AND Isidima_PI_PatientVisitNumber = @Isidima_PI_PatientVisitNumber";
        using (SqlCommand SqlCommand_PI = new SqlCommand(SQLStringPI))
        {
          SqlCommand_PI.Parameters.AddWithValue("@Facility_Id", DropDownList_InsertLinkedFacility_Id.SelectedValue.ToString());
          SqlCommand_PI.Parameters.AddWithValue("@Isidima_PI_PatientVisitNumber", DropDownList_InsertLinkedPatientVisitNumber.SelectedValue.ToString());
          DataTable DataTable_PI;
          using (DataTable_PI = new DataTable())
          {
            DataTable_PI.Locale = CultureInfo.CurrentCulture;
            DataTable_PI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PI).Copy();
            if (DataTable_PI.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PI.Rows)
              {
                Session["IsidimaPIPatientName"] = DataRow_Row["Isidima_PI_PatientName"];
                Session["IsidimaPIPatientAge"] = DataRow_Row["Isidima_PI_PatientAge"];
                Session["IsidimaPIPatientDateOfAdmission"] = DataRow_Row["Isidima_PI_PatientDateOfAdmission"];
                Session["IsidimaPIPatientDateOfDischarge"] = DataRow_Row["Isidima_PI_PatientDateOfDischarge"];
              }
            }
          }
        }

        ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIName")).Text = Session["IsidimaPIPatientName"].ToString();
        ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIAge")).Text = Session["IsidimaPIPatientAge"].ToString();
        ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIDateAdmission")).Text = Session["IsidimaPIPatientDateOfAdmission"].ToString();
        ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIDateDischarge")).Text = Session["IsidimaPIPatientDateOfDischarge"].ToString();

        Session["IsidimaPIPatientName"] = "";
        Session["IsidimaPIPatientAge"] = "";
        Session["IsidimaPIPatientDateOfAdmission"] = "";
        Session["IsidimaPIPatientDateOfDischarge"] = "";
      }
    }

    protected void DropDownList_EditLinkedFacility_Id_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditLinkedFacility_Id = ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_EditLinkedFacility_Id"));
      DropDownList DropDownList_EditLinkedPatientVisitNumber = ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_EditLinkedPatientVisitNumber"));

      Session["PatientVisitNumber"] = "";
      Session["FormPatientVisitNumber"] = "";
      Session["FormFacilityId"] = "";
      Session["DBPatientVisitNumber"] = "";
      Session["DBFacilityId"] = "";

      Session["FormPatientVisitNumber"] = DropDownList_EditLinkedPatientVisitNumber.SelectedValue;
      Session["FormFacilityId"] = DropDownList_EditLinkedFacility_Id.SelectedValue;

      DataView DataView_AdminLink = (DataView)SqlDataSource_Isidima_AdminLink.Select(DataSourceSelectArguments.Empty);
      DataRowView DataRowView_AdminLink = DataView_AdminLink[0];
      Session["DBPatientVisitNumber"] = Convert.ToString(DataRowView_AdminLink["Isidima_AdminLink_LinkedPatientVisitNumber"], CultureInfo.CurrentCulture);
      Session["DBFacilityId"] = Convert.ToString(DataRowView_AdminLink["Isidima_AdminLink_LinkedFacility_Id"], CultureInfo.CurrentCulture);

      if (Session["FormFacilityId"].ToString() == Session["DBFacilityId"].ToString())
      {
        if (Session["FormPatientVisitNumber"].ToString() == Session["DBPatientVisitNumber"].ToString())
        {
          Session["PatientVisitNumber"] = Session["FormPatientVisitNumber"];
        }
        else
        {
          Session["PatientVisitNumber"] = "";
        }
      }
      else
      {
        Session["PatientVisitNumber"] = "";
      }


      DropDownList_EditLinkedPatientVisitNumber.Items.Clear();
      ListItem ListItem_NoSelection = new ListItem();
      ListItem_NoSelection.Value = "";
      ListItem_NoSelection.Text = Convert.ToString("Select Visit Number", CultureInfo.CurrentCulture);
      DropDownList_EditLinkedPatientVisitNumber.Items.Add(ListItem_NoSelection);

      ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIName")).Text = "";
      ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIAge")).Text = "";
      ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIDateAdmission")).Text = "";
      ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIDateDischarge")).Text = "";

      if (!string.IsNullOrEmpty(DropDownList_EditLinkedFacility_Id.SelectedValue.ToString()))
      {
        Session["FacilityId"] = "";
        Session["FacilityFacilityDisplayName"] = "";
        Session["IsidimaPIPatientVisitNumber"] = "";
        string SQLStringPI = "SELECT DISTINCT Facility_Id , Facility_FacilityDisplayName , Isidima_Category_PatientVisitNumber FROM vForm_Isidima_Category WHERE Facility_Id = @Facility_Id AND Isidima_Category_PatientVisitNumber NOT IN (@Current_PatientVisitNumber) AND CAST(Facility_Id AS NVARCHAR(MAX)) + '::' + CAST(Isidima_Category_PatientVisitNumber AS NVARCHAR(MAX)) NOT IN (SELECT CAST(Isidima_AdminLink_LinkedFacility_Id AS NVARCHAR(MAX)) + '::' + CAST(Isidima_AdminLink_LinkedPatientVisitNumber AS NVARCHAR(MAX)) FROM InfoQuest_Form_Isidima_AdminLink WHERE Facility_Id = @Current_Facility_Id AND Isidima_AdminLink_PatientVisitNumber IN (@Current_PatientVisitNumber) AND Isidima_AdminLink_Id NOT IN (@Isidima_AdminLink_Id)) ORDER BY Facility_FacilityDisplayName";
        using (SqlCommand SqlCommand_PI = new SqlCommand(SQLStringPI))
        {
          SqlCommand_PI.Parameters.AddWithValue("@Facility_Id", DropDownList_EditLinkedFacility_Id.SelectedValue.ToString());
          SqlCommand_PI.Parameters.AddWithValue("@Current_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
          SqlCommand_PI.Parameters.AddWithValue("@Current_Facility_Id", Request.QueryString["s_Facility_Id"]);
          SqlCommand_PI.Parameters.AddWithValue("@Isidima_AdminLink_Id", Request.QueryString["Isidima_AdminLink_Id"]);
          DataTable DataTable_PI;
          using (DataTable_PI = new DataTable())
          {
            DataTable_PI.Locale = CultureInfo.CurrentCulture;
            DataTable_PI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PI).Copy();
            if (DataTable_PI.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PI.Rows)
              {
                Session["FacilityId"] = DataRow_Row["Facility_Id"];
                Session["FacilityFacilityDisplayName"] = DataRow_Row["Facility_FacilityDisplayName"];
                Session["IsidimaPIPatientVisitNumber"] = DataRow_Row["Isidima_Category_PatientVisitNumber"];

                DropDownList_EditLinkedPatientVisitNumber.Items.Add(Session["IsidimaPIPatientVisitNumber"].ToString());
              }
            }
          }
        }
        Session["FacilityId"] = "";
        Session["FacilityFacilityDisplayName"] = "";
        Session["IsidimaPIPatientVisitNumber"] = "";
      }

      DropDownList_EditLinkedPatientVisitNumber.SelectedValue = Session["PatientVisitNumber"].ToString();
      Session["PatientVisitNumber"] = "";
      Session["FormPatientVisitNumber"] = "";
      Session["FormFacilityId"] = "";
      Session["DBPatientVisitNumber"] = "";
      Session["DBFacilityId"] = "";
    }

    protected void DropDownList_EditLinkedPatientVisitNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList DropDownList_EditLinkedFacility_Id = ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_EditLinkedFacility_Id"));
      DropDownList DropDownList_EditLinkedPatientVisitNumber = ((DropDownList)FormView_Isidima_AdminLink.FindControl("DropDownList_EditLinkedPatientVisitNumber"));

      if (!string.IsNullOrEmpty(DropDownList_EditLinkedFacility_Id.SelectedValue.ToString()) && !string.IsNullOrEmpty(DropDownList_EditLinkedPatientVisitNumber.SelectedValue.ToString()))
      {
        Session["IsidimaPIPatientName"] = "";
        Session["IsidimaPIPatientAge"] = "";
        Session["IsidimaPIPatientDateOfAdmission"] = "";
        Session["IsidimaPIPatientDateOfDischarge"] = "";
        string SQLStringPI = "SELECT Isidima_PI_PatientName , Isidima_PI_PatientAge , Isidima_PI_PatientDateOfAdmission , Isidima_PI_PatientDateOfDischarge FROM vForm_Isidima_PatientInformation WHERE Facility_Id = @Facility_Id AND Isidima_PI_PatientVisitNumber = @Isidima_PI_PatientVisitNumber";
        using (SqlCommand SqlCommand_PI = new SqlCommand(SQLStringPI))
        {
          SqlCommand_PI.Parameters.AddWithValue("@Facility_Id", DropDownList_EditLinkedFacility_Id.SelectedValue.ToString());
          SqlCommand_PI.Parameters.AddWithValue("@Isidima_PI_PatientVisitNumber", DropDownList_EditLinkedPatientVisitNumber.SelectedValue.ToString());
          DataTable DataTable_PI;
          using (DataTable_PI = new DataTable())
          {
            DataTable_PI.Locale = CultureInfo.CurrentCulture;
            DataTable_PI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PI).Copy();
            if (DataTable_PI.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_PI.Rows)
              {
                Session["IsidimaPIPatientName"] = DataRow_Row["Isidima_PI_PatientName"];
                Session["IsidimaPIPatientAge"] = DataRow_Row["Isidima_PI_PatientAge"];
                Session["IsidimaPIPatientDateOfAdmission"] = DataRow_Row["Isidima_PI_PatientDateOfAdmission"];
                Session["IsidimaPIPatientDateOfDischarge"] = DataRow_Row["Isidima_PI_PatientDateOfDischarge"];
              }
            }
          }
        }

        ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIName")).Text = Session["IsidimaPIPatientName"].ToString();
        ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIAge")).Text = Session["IsidimaPIPatientAge"].ToString();
        ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIDateAdmission")).Text = Session["IsidimaPIPatientDateOfAdmission"].ToString();
        ((Label)FormView_Isidima_AdminLink.FindControl("Label_LinkedPIDateDischarge")).Text = Session["IsidimaPIPatientDateOfDischarge"].ToString();

        Session["IsidimaPIPatientName"] = "";
        Session["IsidimaPIPatientAge"] = "";
        Session["IsidimaPIPatientDateOfAdmission"] = "";
        Session["IsidimaPIPatientDateOfDischarge"] = "";
      }
    }


    protected void SqlDataSource_Isidima_AdminLink_List_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageSize = (DropDownList)GridView_Isidima_AdminLink_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
      GridView_Isidima_AdminLink_List.PageSize = Convert.ToInt32(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DropDownList DropDownList_PageList = (DropDownList)GridView_Isidima_AdminLink_List.BottomPagerRow.Cells[0].FindControl("DropDownList_Page");
      GridView_Isidima_AdminLink_List.PageIndex = DropDownList_PageList.SelectedIndex;
    }

    protected void GridView_Isidima_AdminLink_List_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)GridView_Isidima_AdminLink_List.BottomPagerRow.Cells[0].FindControl("DropDownList_PageSize");
        if (GridView_Isidima_AdminLink_List.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
        else if (GridView_Isidima_AdminLink_List.PageSize > 20 && GridView_Isidima_AdminLink_List.PageSize <= 50)
        {
          DropDownList_PageSize.SelectedValue = "50";
        }
        else if (GridView_Isidima_AdminLink_List.PageSize > 50 && GridView_Isidima_AdminLink_List.PageSize <= 100)
        {
          DropDownList_PageSize.SelectedValue = "100";
        }
      }
    }

    protected void GridView_Isidima_AdminLink_List_DataBound(object sender, EventArgs e)
    {
      GridViewRow GridViewRow_List = GridView_Isidima_AdminLink_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        DropDownList DropDownList_PageList = (DropDownList)GridViewRow_List.Cells[0].FindControl("DropDownList_Page");
        if (GridViewRow_List != null)
        {
          for (int i = 0; i < GridView_Isidima_AdminLink_List.PageCount; i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == GridView_Isidima_AdminLink_List.PageIndex)
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }
        }
      }
    }

    protected void GridView_Isidima_AdminLink_List_RowCreated(object sender, GridViewRowEventArgs e)
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

    public string GetLink(object isidima_AdminLink_Id)
    {
      string LinkURL = "";
      LinkURL = "<a href='Form_Isidima_Admin.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Isidima_AdminLink_Id=" + isidima_AdminLink_Id + "&Section=Link#Link'>View</a>";

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      FinalURL = CurrentURL;

      return FinalURL;
    }


    protected void SqlDataSource_Isidima_AdminDischarge_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima_Admin.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Section=Discharge#Discharge", false);
        }
      }
    }

    protected void FormView_Isidima_AdminDischarge_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaAdminDischargeModifiedDate"] = e.OldValues["Isidima_AdminDischarge_ModifiedDate"];
        object OLDIsidimaAdminDischargeModifiedDate = Session["OLDIsidimaAdminDischargeModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaAdminDischargeModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareAdminDischarge = (DataView)SqlDataSource_Isidima_AdminDischarge.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareAdminDischarge = DataView_CompareAdminDischarge[0];
        Session["DBIsidimaAdminDischargeModifiedDate"] = Convert.ToString(DataRowView_CompareAdminDischarge["Isidima_AdminDischarge_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaAdminDischargeModifiedBy"] = Convert.ToString(DataRowView_CompareAdminDischarge["Isidima_AdminDischarge_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaAdminDischargeModifiedDate = Session["DBIsidimaAdminDischargeModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaAdminDischargeModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_Isidima_AdminDischarge.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_Isidima_AdminDischarge.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaAdminLinkModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_AdminDischarge_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_AdminDischarge_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("InfoQuest_Form_Isidima_AdminDischarge", "Facility_Id = " + Request.QueryString["s_Facility_Id"] + " AND Isidima_AdminDischarge_PatientVisitNumber = " + Request.QueryString["s_Isidima_PatientVisitNumber"]);

            DataView DataView_AdminDischarge = (DataView)SqlDataSource_Isidima_AdminDischarge.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_AdminDischarge = DataView_AdminDischarge[0];
            Session["IsidimaAdminDischargeHistory"] = Convert.ToString(DataRowView_AdminDischarge["Isidima_AdminDischarge_History"], CultureInfo.CurrentCulture);

            Session["IsidimaAdminDischargeHistory"] = Session["History"].ToString() + Session["IsidimaAdminDischargeHistory"].ToString();
            e.NewValues["Isidima_AdminDischarge_History"] = Session["IsidimaAdminDischargeHistory"].ToString();

            Session["IsidimaAdminDischargeHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDIsidimaAdminDischargeModifiedDate"] = "";
        Session["DBIsidimaAdminDischargeModifiedDate"] = "";
        Session["DBIsidimaAdminDischargeModifiedBy"] = "";
      }
    }


    protected void SqlDataSource_Isidima_AdminAssessmentPeriod_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima_Admin.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Section=Period#Period", false);
        }
      }
    }

    protected void FormView_Isidima_AdminAssessmentPeriod_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaAdminAssessmentPeriodModifiedDate"] = e.OldValues["Isidima_AdminAssessmentPeriod_ModifiedDate"];
        object OLDIsidimaAdminAssessmentPeriodModifiedDate = Session["OLDIsidimaAdminAssessmentPeriodModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaAdminAssessmentPeriodModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareAdminAssessmentPeriod = (DataView)SqlDataSource_Isidima_AdminAssessmentPeriod.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareAdminAssessmentPeriod = DataView_CompareAdminAssessmentPeriod[0];
        Session["DBIsidimaAdminAssessmentPeriodModifiedDate"] = Convert.ToString(DataRowView_CompareAdminAssessmentPeriod["Isidima_AdminAssessmentPeriod_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaAdminAssessmentPeriodModifiedBy"] = Convert.ToString(DataRowView_CompareAdminAssessmentPeriod["Isidima_AdminAssessmentPeriod_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaAdminAssessmentPeriodModifiedDate = Session["DBIsidimaAdminAssessmentPeriodModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaAdminAssessmentPeriodModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)FormView_Isidima_AdminAssessmentPeriod.FindControl("Label_ConcurrencyUpdate")).Visible = true;

          ((Label)FormView_Isidima_AdminAssessmentPeriod.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaAdminLinkModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_AdminAssessmentPeriod_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_AdminAssessmentPeriod_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("InfoQuest_Form_Isidima_AdminAssessmentPeriod", "Facility_Id = " + Request.QueryString["s_Facility_Id"] + " AND Isidima_AdminAssessmentPeriod_PatientVisitNumber = " + Request.QueryString["s_Isidima_PatientVisitNumber"]);

            DataView DataView_AdminAssessmentPeriod = (DataView)SqlDataSource_Isidima_AdminAssessmentPeriod.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_AdminAssessmentPeriod = DataView_AdminAssessmentPeriod[0];
            Session["IsidimaAdminAssessmentPeriodHistory"] = Convert.ToString(DataRowView_AdminAssessmentPeriod["Isidima_AdminAssessmentPeriod_History"], CultureInfo.CurrentCulture);

            Session["IsidimaAdminAssessmentPeriodHistory"] = Session["History"].ToString() + Session["IsidimaAdminAssessmentPeriodHistory"].ToString();
            e.NewValues["Isidima_AdminAssessmentPeriod_History"] = Session["IsidimaAdminAssessmentPeriodHistory"].ToString();

            Session["IsidimaAdminAssessmentPeriodHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDIsidimaAdminAssessmentPeriodModifiedDate"] = "";
        Session["DBIsidimaAdminAssessmentPeriodModifiedDate"] = "";
        Session["DBIsidimaAdminAssessmentPeriodModifiedBy"] = "";
      }
    }
  }
}