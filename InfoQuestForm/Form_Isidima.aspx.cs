using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Isidima : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Form0PatientCategoryChanged = false;

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

          TablePatientInfo.Visible = false; TableListLinks.Visible = false; TableList.Visible = false;
          TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);
          Label_PatientInfoHeading.Text = Convert.ToString("Patient Information", CultureInfo.CurrentCulture);
          Label_Form0ViewHeading.Text = Convert.ToString("Form Information", CultureInfo.CurrentCulture);
          Label_Form0Heading.Text = Convert.ToString("Form Information", CultureInfo.CurrentCulture);
          Label_Form1Heading.Text = Convert.ToString("Administration (MHA)", CultureInfo.CurrentCulture);
          Label_Form2Heading.Text = Convert.ToString("Administration (VPA)", CultureInfo.CurrentCulture);
          Label_Form3Heading.Text = Convert.ToString("Child (J)", CultureInfo.CurrentCulture);
          Label_Form4Heading.Text = Convert.ToString("Discharge (DMH)", CultureInfo.CurrentCulture);
          Label_Form5Heading.Text = Convert.ToString("Function (F)", CultureInfo.CurrentCulture);
          Label_Form6Heading.Text = Convert.ToString("Independence (I)", CultureInfo.CurrentCulture);
          Label_Form7Heading.Text = Convert.ToString("Mental Health (PSY)", CultureInfo.CurrentCulture);
          Label_Form8Heading.Text = Convert.ToString("Personality Traits (T)", CultureInfo.CurrentCulture);
          Label_Form9Heading.Text = Convert.ToString("Physical (B)", CultureInfo.CurrentCulture);
          Label_Form10Heading.Text = Convert.ToString("Recreational (R)", CultureInfo.CurrentCulture);
          Label_Form11Heading.Text = Convert.ToString("Social (S)", CultureInfo.CurrentCulture);
          Label_Form12Heading.Text = Convert.ToString("Vocational (V)", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString(), CultureInfo.CurrentCulture);

          SetFormQueryString();

          if (Request.QueryString["s_Facility_Id"] != null && Request.QueryString["s_Isidima_PatientVisitNumber"] != null)
          {
            if (Request.QueryString["AMSPI_Intervention_Id"] != null)
            {
              SqlDataSource_Isidima_Facility.SelectParameters["TableSELECT"].DefaultValue = "Facility_Id";
              SqlDataSource_Isidima_Facility.SelectParameters["TableFROM"].DefaultValue = "InfoQuest_Form_Isidima_Category";
              SqlDataSource_Isidima_Facility.SelectParameters["TableWHERE"].DefaultValue = "Isidima_Category_Id = " + Request.QueryString["Isidima_Category_Id"] + "";
            }

            if (Request.QueryString["Form"] == null)
            {
              TablePatientInfo.Visible = true; TableListLinks.Visible = true; TableList.Visible = true;
              TableForm0View.Visible = false; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

              PatientDataPI();
            }
            else
            {
              SetFormVisibility();
            }
          }
          else
          {
            Label_InvalidSearch.Text = "";
            TablePatientInfo.Visible = false; TableListLinks.Visible = false; TableList.Visible = false;
            TableForm0View.Visible = false; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;
          }


          if (TablePatientInfo.Visible == true)
          {
            TablePatientInfoVisible();
          }

          if (TableListLinks.Visible == true)
          {
            TableListLinksVisible();
          }

          if (TableForm0View.Visible == true)
          {
            TableForm0ViewVisible();
          }

          if (TableForm0.Visible == true)
          {
            Form0Visible();
          }

          if (TableForm1.Visible == true)
          {
            Form1Visible();
          }

          if (TableForm2.Visible == true)
          {
            Form2Visible();
          }

          if (TableForm3.Visible == true)
          {
            Form3Visible();
          }

          if (TableForm4.Visible == true)
          {
            Form4Visible();
          }

          if (TableForm5.Visible == true)
          {
            Form5Visible();
          }

          if (TableForm6.Visible == true)
          {
            Form6Visible();
          }

          if (TableForm7.Visible == true)
          {
            Form7Visible();
          }

          if (TableForm8.Visible == true)
          {
            Form8Visible();
          }

          if (TableForm9.Visible == true)
          {
            Form9Visible();
          }

          if (TableForm10.Visible == true)
          {
            Form10Visible();
          }

          if (TableForm11.Visible == true)
          {
            Form11Visible();
          }

          if (TableForm12.Visible == true)
          {
            Form12Visible();
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
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('27'))";
        }
        else
        {
          SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('27')) AND (Facility_Id IN (@Facility_Id) OR (SecurityRole_Rank = 1))";
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
      SqlDataSource_Isidima_Facility.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Facility.SelectCommand = "spAdministration_Execute_Facility_Form";
      SqlDataSource_Isidima_Facility.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Isidima_Facility.SelectParameters.Clear();
      SqlDataSource_Isidima_Facility.SelectParameters.Add("SecurityUser_UserName", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Isidima_Facility.SelectParameters.Add("Form_Id", TypeCode.String, "27");
      SqlDataSource_Isidima_Facility.SelectParameters.Add("Facility_Type", TypeCode.String, "0");
      SqlDataSource_Isidima_Facility.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Isidima_Facility.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Isidima_Facility.SelectParameters.Add("TableWHERE", TypeCode.String, "0");

      SqlDataSource_Isidima_EditPatientCategory.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_EditPatientCategory.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 27 AND ListCategory_Id = 64 AND ListItem_Parent = -1 UNION SELECT InfoQuest_Form_Isidima_Category.Isidima_Category_PatientCategory_List,Administration_ListItem.ListItem_Name FROM InfoQuest_Form_Isidima_Category , Administration_ListItem WHERE InfoQuest_Form_Isidima_Category.Isidima_Category_PatientCategory_List = Administration_ListItem.ListItem_Id AND InfoQuest_Form_Isidima_Category.Isidima_Category_Id = @Isidima_Category_Id ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Isidima_EditPatientCategory.SelectCommandType = SqlDataSourceCommandType.Text;
      SqlDataSource_Isidima_EditPatientCategory.SelectParameters.Clear();
      SqlDataSource_Isidima_EditPatientCategory.SelectParameters.Add("Isidima_Category_Id", TypeCode.String, Request.QueryString["Isidima_Category_Id"]);

      SqlDataSource_Isidima_InsertPatientCategory.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_InsertPatientCategory.SelectCommand = "SELECT * FROM ( SELECT ListItem_Id,ListItem_Name FROM vAdministration_ListItem_Active WHERE Form_Id = 27 AND ListCategory_Id = 64 AND ListItem_Parent = -1 ) AS TempTableAll ORDER BY TempTableAll.ListItem_Name";
      SqlDataSource_Isidima_InsertPatientCategory.SelectCommandType = SqlDataSourceCommandType.Text;

      SqlDataSourceSetup_Form0();
      SqlDataSourceSetup_Form1();
      SqlDataSourceSetup_Form2();
      SqlDataSourceSetup_Form3();
      SqlDataSourceSetup_Form4();
      SqlDataSourceSetup_Form5();
      SqlDataSourceSetup_Form6();
      SqlDataSourceSetup_Form7();
      SqlDataSourceSetup_Form8();
      SqlDataSourceSetup_Form9();
      SqlDataSourceSetup_Form10();
      SqlDataSourceSetup_Form11();
      SqlDataSourceSetup_Form12();

      SqlDataSource_Isidima_Category.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Category.SelectCommand = "spForm_Get_Isidima_Category";
      SqlDataSource_Isidima_Category.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Isidima_Category.CancelSelectOnNullParameter = false;
      SqlDataSource_Isidima_Category.SelectParameters.Clear();
      SqlDataSource_Isidima_Category.SelectParameters.Add("SecurityUser", TypeCode.String, Request.ServerVariables["LOGON_USER"]);
      SqlDataSource_Isidima_Category.SelectParameters.Add("FacilityId", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_Category.SelectParameters.Add("PatientVisitNumber", TypeCode.String, Request.QueryString["s_Isidima_PatientVisitNumber"]);
    }

    private void SqlDataSourceSetup_Form0()
    {
      SqlDataSource_Isidima_Form0.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form0.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Category] ([Facility_Id] , [Isidima_Category_PatientVisitNumber] , [Isidima_Category_ReportNumber] , [Isidima_Category_PatientCategory_List] , [Isidima_Category_Date] , [Isidima_Category_CreatedDate] , [Isidima_Category_CreatedBy] , [Isidima_Category_ModifiedDate] , [Isidima_Category_ModifiedBy] , [Isidima_Category_History] , [Isidima_Category_IsActive]) VALUES (@Facility_Id , @Isidima_Category_PatientVisitNumber , @Isidima_Category_ReportNumber , @Isidima_Category_PatientCategory_List , @Isidima_Category_Date , @Isidima_Category_CreatedDate , @Isidima_Category_CreatedBy , @Isidima_Category_ModifiedDate , @Isidima_Category_ModifiedBy , @Isidima_Category_History , @Isidima_Category_IsActive); SELECT @Isidima_Category_Id = SCOPE_IDENTITY()";
      SqlDataSource_Isidima_Form0.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Category] WHERE ([Isidima_Category_Id] = @Isidima_Category_Id)";
      SqlDataSource_Isidima_Form0.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Category] SET [Isidima_Category_PatientCategory_List] = @Isidima_Category_PatientCategory_List , [Isidima_Category_Date] = @Isidima_Category_Date , [Isidima_Category_ModifiedDate] = @Isidima_Category_ModifiedDate , [Isidima_Category_ModifiedBy] = @Isidima_Category_ModifiedBy , [Isidima_Category_History] = @Isidima_Category_History , [Isidima_Category_IsActive] = @Isidima_Category_IsActive WHERE [Isidima_Category_Id] = @Isidima_Category_Id";
      SqlDataSource_Isidima_Form0.InsertParameters.Clear();
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form0.InsertParameters["Isidima_Category_Id"].Direction = ParameterDirection.Output;
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Facility_Id", TypeCode.String, Request.QueryString["s_Facility_Id"]);
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_PatientVisitNumber", TypeCode.String, Request.QueryString["s_Isidima_PatientVisitNumber"]);
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_ReportNumber", TypeCode.String, "");
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_PatientCategory_List", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_Date", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form0.InsertParameters["Isidima_Category_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form0.InsertParameters.Add("Isidima_Category_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Isidima_Form0.SelectParameters.Clear();
      SqlDataSource_Isidima_Form0.SelectParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form0.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form0.UpdateParameters.Add("Isidima_Category_PatientCategory_List", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form0.UpdateParameters.Add("Isidima_Category_Date", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form0.UpdateParameters.Add("Isidima_Category_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form0.UpdateParameters.Add("Isidima_Category_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form0.UpdateParameters.Add("Isidima_Category_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form0.UpdateParameters.Add("Isidima_Category_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Isidima_Form0.UpdateParameters.Add("Isidima_Category_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form1()
    {
      SqlDataSource_Isidima_Form1.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form1.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_MHA] ([Isidima_Category_Id] ,[Isidima_Section_MHA_A01] ,[Isidima_Section_MHA_A02] ,[Isidima_Section_MHA_A03] ,[Isidima_Section_MHA_A04] ,[Isidima_Section_MHA_A05] ,[Isidima_Section_MHA_A06] ,[Isidima_Section_MHA_A07] ,[Isidima_Section_MHA_A08] ,[Isidima_Section_MHA_A09] ,[Isidima_Section_MHA_A10] ,[Isidima_Section_MHA_A11] ,[Isidima_Section_MHA_A12] ,[Isidima_Section_MHA_A13] ,[Isidima_Section_MHA_A14] ,[Isidima_Section_MHA_A15] ,[Isidima_Section_MHA_A16] ,[Isidima_Section_MHA_A17] ,[Isidima_Section_MHA_A18] ,[Isidima_Section_MHA_A19] ,[Isidima_Section_MHA_Total] ,[Isidima_Section_MHA_CreatedDate] ,[Isidima_Section_MHA_CreatedBy] ,[Isidima_Section_MHA_ModifiedDate] ,[Isidima_Section_MHA_ModifiedBy] ,[Isidima_Section_MHA_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_MHA_A01 ,@Isidima_Section_MHA_A02 ,@Isidima_Section_MHA_A03 ,@Isidima_Section_MHA_A04 ,@Isidima_Section_MHA_A05 ,@Isidima_Section_MHA_A06 ,@Isidima_Section_MHA_A07 ,@Isidima_Section_MHA_A08 ,@Isidima_Section_MHA_A09 ,@Isidima_Section_MHA_A10 ,@Isidima_Section_MHA_A11 ,@Isidima_Section_MHA_A12 ,@Isidima_Section_MHA_A13 ,@Isidima_Section_MHA_A14 ,@Isidima_Section_MHA_A15 ,@Isidima_Section_MHA_A16 ,@Isidima_Section_MHA_A17 ,@Isidima_Section_MHA_A18 ,@Isidima_Section_MHA_A19 ,@Isidima_Section_MHA_Total ,@Isidima_Section_MHA_CreatedDate ,@Isidima_Section_MHA_CreatedBy ,@Isidima_Section_MHA_ModifiedDate ,@Isidima_Section_MHA_ModifiedBy ,@Isidima_Section_MHA_History)";
      SqlDataSource_Isidima_Form1.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_MHA] WHERE ([Isidima_Section_MHA_Id] = @Isidima_Section_MHA_Id)";
      SqlDataSource_Isidima_Form1.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_MHA] SET [Isidima_Section_MHA_A01] = @Isidima_Section_MHA_A01 ,[Isidima_Section_MHA_A02] = @Isidima_Section_MHA_A02 ,[Isidima_Section_MHA_A03] = @Isidima_Section_MHA_A03 ,[Isidima_Section_MHA_A04] = @Isidima_Section_MHA_A04 ,[Isidima_Section_MHA_A05] = @Isidima_Section_MHA_A05 ,[Isidima_Section_MHA_A06] = @Isidima_Section_MHA_A06 ,[Isidima_Section_MHA_A07] = @Isidima_Section_MHA_A07 ,[Isidima_Section_MHA_A08] = @Isidima_Section_MHA_A08 ,[Isidima_Section_MHA_A09] = @Isidima_Section_MHA_A09 ,[Isidima_Section_MHA_A10] = @Isidima_Section_MHA_A10 ,[Isidima_Section_MHA_A11] = @Isidima_Section_MHA_A11 ,[Isidima_Section_MHA_A12] = @Isidima_Section_MHA_A12 ,[Isidima_Section_MHA_A13] = @Isidima_Section_MHA_A13 ,[Isidima_Section_MHA_A14] = @Isidima_Section_MHA_A14 ,[Isidima_Section_MHA_A15] = @Isidima_Section_MHA_A15 ,[Isidima_Section_MHA_A16] = @Isidima_Section_MHA_A16 ,[Isidima_Section_MHA_A17] = @Isidima_Section_MHA_A17 ,[Isidima_Section_MHA_A18] = @Isidima_Section_MHA_A18 ,[Isidima_Section_MHA_A19] = @Isidima_Section_MHA_A19 ,[Isidima_Section_MHA_Total] = @Isidima_Section_MHA_Total ,[Isidima_Section_MHA_ModifiedDate] = @Isidima_Section_MHA_ModifiedDate ,[Isidima_Section_MHA_ModifiedBy] = @Isidima_Section_MHA_ModifiedBy ,[Isidima_Section_MHA_History] = @Isidima_Section_MHA_History WHERE [Isidima_Section_MHA_Id] = @Isidima_Section_MHA_Id";
      SqlDataSource_Isidima_Form1.InsertParameters.Clear();
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A01", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A02", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A03", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A04", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A05", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A06", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A07", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A08", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A09", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A10", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A11", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A12", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A13", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A14", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A15", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A16", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A17", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A18", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_A19", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters.Add("Isidima_Section_MHA_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.InsertParameters["Isidima_Section_MHA_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form1.SelectParameters.Clear();
      SqlDataSource_Isidima_Form1.SelectParameters.Add("Isidima_Section_MHA_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_MHA_Id"]);
      SqlDataSource_Isidima_Form1.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A01", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A02", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A03", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A04", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A05", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A06", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A07", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A08", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A09", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A10", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A11", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A12", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A13", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A14", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A15", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A16", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A17", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A18", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_A19", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form1.UpdateParameters.Add("Isidima_Section_MHA_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form2()
    {
      SqlDataSource_Isidima_Form2.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form2.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_VPA] ([Isidima_Category_Id] ,[Isidima_Section_VPA_A01] ,[Isidima_Section_VPA_A02] ,[Isidima_Section_VPA_A03] ,[Isidima_Section_VPA_A04] ,[Isidima_Section_VPA_A05] ,[Isidima_Section_VPA_A06] ,[Isidima_Section_VPA_A07] ,[Isidima_Section_VPA_A08] ,[Isidima_Section_VPA_A09] ,[Isidima_Section_VPA_A10] ,[Isidima_Section_VPA_A11] ,[Isidima_Section_VPA_A12] ,[Isidima_Section_VPA_A13] ,[Isidima_Section_VPA_Total] ,[Isidima_Section_VPA_CreatedDate] ,[Isidima_Section_VPA_CreatedBy] ,[Isidima_Section_VPA_ModifiedDate] ,[Isidima_Section_VPA_ModifiedBy] ,[Isidima_Section_VPA_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_VPA_A01 ,@Isidima_Section_VPA_A02 ,@Isidima_Section_VPA_A03 ,@Isidima_Section_VPA_A04 ,@Isidima_Section_VPA_A05 ,@Isidima_Section_VPA_A06 ,@Isidima_Section_VPA_A07 ,@Isidima_Section_VPA_A08 ,@Isidima_Section_VPA_A09 ,@Isidima_Section_VPA_A10 ,@Isidima_Section_VPA_A11 ,@Isidima_Section_VPA_A12 ,@Isidima_Section_VPA_A13 ,@Isidima_Section_VPA_Total ,@Isidima_Section_VPA_CreatedDate ,@Isidima_Section_VPA_CreatedBy ,@Isidima_Section_VPA_ModifiedDate ,@Isidima_Section_VPA_ModifiedBy ,@Isidima_Section_VPA_History)";
      SqlDataSource_Isidima_Form2.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_VPA] WHERE ([Isidima_Section_VPA_Id] = @Isidima_Section_VPA_Id)";
      SqlDataSource_Isidima_Form2.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_VPA] SET [Isidima_Section_VPA_A01] = @Isidima_Section_VPA_A01 ,[Isidima_Section_VPA_A02] = @Isidima_Section_VPA_A02 ,[Isidima_Section_VPA_A03] = @Isidima_Section_VPA_A03 ,[Isidima_Section_VPA_A04] = @Isidima_Section_VPA_A04 ,[Isidima_Section_VPA_A05] = @Isidima_Section_VPA_A05 ,[Isidima_Section_VPA_A06] = @Isidima_Section_VPA_A06 ,[Isidima_Section_VPA_A07] = @Isidima_Section_VPA_A07 ,[Isidima_Section_VPA_A08] = @Isidima_Section_VPA_A08 ,[Isidima_Section_VPA_A09] = @Isidima_Section_VPA_A09 ,[Isidima_Section_VPA_A10] = @Isidima_Section_VPA_A10 ,[Isidima_Section_VPA_A11] = @Isidima_Section_VPA_A11 ,[Isidima_Section_VPA_A12] = @Isidima_Section_VPA_A12 ,[Isidima_Section_VPA_A13] = @Isidima_Section_VPA_A13 ,[Isidima_Section_VPA_Total] = @Isidima_Section_VPA_Total ,[Isidima_Section_VPA_ModifiedDate] = @Isidima_Section_VPA_ModifiedDate ,[Isidima_Section_VPA_ModifiedBy] = @Isidima_Section_VPA_ModifiedBy ,[Isidima_Section_VPA_History] = @Isidima_Section_VPA_History WHERE [Isidima_Section_VPA_Id] = @Isidima_Section_VPA_Id";
      SqlDataSource_Isidima_Form2.InsertParameters.Clear();
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A01", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A02", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A03", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A04", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A05", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A06", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A07", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A08", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A09", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A10", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A11", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A12", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_A13", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters.Add("Isidima_Section_VPA_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.InsertParameters["Isidima_Section_VPA_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form2.SelectParameters.Clear();
      SqlDataSource_Isidima_Form2.SelectParameters.Add("Isidima_Section_VPA_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_VPA_Id"]);
      SqlDataSource_Isidima_Form2.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A01", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A02", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A03", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A04", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A05", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A06", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A07", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A08", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A09", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A10", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A11", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A12", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_A13", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form2.UpdateParameters.Add("Isidima_Section_VPA_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form3()
    {
      SqlDataSource_Isidima_Form3.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form3.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_J] ([Isidima_Category_Id] ,[Isidima_Section_J_J01] ,[Isidima_Section_J_J02] ,[Isidima_Section_J_J03] ,[Isidima_Section_J_J04] ,[Isidima_Section_J_J05] ,[Isidima_Section_J_J06] ,[Isidima_Section_J_J07] ,[Isidima_Section_J_J08] ,[Isidima_Section_J_J09] ,[Isidima_Section_J_J10] ,[Isidima_Section_J_J11] ,[Isidima_Section_J_J12] ,[Isidima_Section_J_J13] ,[Isidima_Section_J_J14] ,[Isidima_Section_J_J15] ,[Isidima_Section_J_J16] ,[Isidima_Section_J_J17] ,[Isidima_Section_J_J18] ,[Isidima_Section_J_J19] ,[Isidima_Section_J_J20] ,[Isidima_Section_J_J21] ,[Isidima_Section_J_J22] ,[Isidima_Section_J_J23] ,[Isidima_Section_J_J24] ,[Isidima_Section_J_J25] ,[Isidima_Section_J_J26] ,[Isidima_Section_J_Total] ,[Isidima_Section_J_CreatedDate] ,[Isidima_Section_J_CreatedBy] ,[Isidima_Section_J_ModifiedDate] ,[Isidima_Section_J_ModifiedBy] ,[Isidima_Section_J_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_J_J01 ,@Isidima_Section_J_J02 ,@Isidima_Section_J_J03 ,@Isidima_Section_J_J04 ,@Isidima_Section_J_J05 ,@Isidima_Section_J_J06 ,@Isidima_Section_J_J07 ,@Isidima_Section_J_J08 ,@Isidima_Section_J_J09 ,@Isidima_Section_J_J10 ,@Isidima_Section_J_J11 ,@Isidima_Section_J_J12 ,@Isidima_Section_J_J13 ,@Isidima_Section_J_J14 ,@Isidima_Section_J_J15 ,@Isidima_Section_J_J16 ,@Isidima_Section_J_J17 ,@Isidima_Section_J_J18 ,@Isidima_Section_J_J19 ,@Isidima_Section_J_J20 ,@Isidima_Section_J_J21 ,@Isidima_Section_J_J22 ,@Isidima_Section_J_J23 ,@Isidima_Section_J_J24 ,@Isidima_Section_J_J25 ,@Isidima_Section_J_J26 ,@Isidima_Section_J_Total ,@Isidima_Section_J_CreatedDate ,@Isidima_Section_J_CreatedBy ,@Isidima_Section_J_ModifiedDate ,@Isidima_Section_J_ModifiedBy ,@Isidima_Section_J_History)";
      SqlDataSource_Isidima_Form3.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_J] WHERE ([Isidima_Section_J_Id] = @Isidima_Section_J_Id)";
      SqlDataSource_Isidima_Form3.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_J] SET [Isidima_Section_J_J01] = @Isidima_Section_J_J01 ,[Isidima_Section_J_J02] = @Isidima_Section_J_J02 ,[Isidima_Section_J_J03] = @Isidima_Section_J_J03 ,[Isidima_Section_J_J04] = @Isidima_Section_J_J04 ,[Isidima_Section_J_J05] = @Isidima_Section_J_J05 ,[Isidima_Section_J_J06] = @Isidima_Section_J_J06 ,[Isidima_Section_J_J07] = @Isidima_Section_J_J07 ,[Isidima_Section_J_J08] = @Isidima_Section_J_J08 ,[Isidima_Section_J_J09] = @Isidima_Section_J_J09 ,[Isidima_Section_J_J10] = @Isidima_Section_J_J10 ,[Isidima_Section_J_J11] = @Isidima_Section_J_J11 ,[Isidima_Section_J_J12] = @Isidima_Section_J_J12 ,[Isidima_Section_J_J13] = @Isidima_Section_J_J13 ,[Isidima_Section_J_J14] = @Isidima_Section_J_J14 ,[Isidima_Section_J_J15] = @Isidima_Section_J_J15 ,[Isidima_Section_J_J16] = @Isidima_Section_J_J16 ,[Isidima_Section_J_J17] = @Isidima_Section_J_J17 ,[Isidima_Section_J_J18] = @Isidima_Section_J_J18 ,[Isidima_Section_J_J19] = @Isidima_Section_J_J19 ,[Isidima_Section_J_J20] = @Isidima_Section_J_J20 ,[Isidima_Section_J_J21] = @Isidima_Section_J_J21 ,[Isidima_Section_J_J22] = @Isidima_Section_J_J22 ,[Isidima_Section_J_J23] = @Isidima_Section_J_J23 ,[Isidima_Section_J_J24] = @Isidima_Section_J_J24 ,[Isidima_Section_J_J25] = @Isidima_Section_J_J25 ,[Isidima_Section_J_J26] = @Isidima_Section_J_J26 ,[Isidima_Section_J_Total] = @Isidima_Section_J_Total ,[Isidima_Section_J_ModifiedDate] = @Isidima_Section_J_ModifiedDate ,[Isidima_Section_J_ModifiedBy] = @Isidima_Section_J_ModifiedBy ,[Isidima_Section_J_History] = @Isidima_Section_J_History WHERE [Isidima_Section_J_Id] = @Isidima_Section_J_Id";
      SqlDataSource_Isidima_Form3.InsertParameters.Clear();
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J01", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J02", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J03", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J04", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J05", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J06", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J07", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J08", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J09", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J10", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J11", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J12", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J13", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J14", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J15", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J16", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J17", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J18", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J19", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J20", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J21", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J22", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J23", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J24", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J25", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_J26", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters.Add("Isidima_Section_J_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.InsertParameters["Isidima_Section_J_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form3.SelectParameters.Clear();
      SqlDataSource_Isidima_Form3.SelectParameters.Add("Isidima_Section_J_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_J_Id"]);
      SqlDataSource_Isidima_Form3.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J01", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J02", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J03", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J04", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J05", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J06", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J07", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J08", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J09", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J10", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J11", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J12", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J13", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J14", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J15", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J16", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J17", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J18", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J19", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J20", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J21", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J22", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J23", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J24", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J25", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_J26", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form3.UpdateParameters.Add("Isidima_Section_J_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form4()
    {
      SqlDataSource_Isidima_Form4.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form4.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_DMH] ([Isidima_Category_Id] ,[Isidima_Section_DMH_S01] ,[Isidima_Section_DMH_S02] ,[Isidima_Section_DMH_S03] ,[Isidima_Section_DMH_S04] ,[Isidima_Section_DMH_S05] ,[Isidima_Section_DMH_S06] ,[Isidima_Section_DMH_S07] ,[Isidima_Section_DMH_S08] ,[Isidima_Section_DMH_S09] ,[Isidima_Section_DMH_S10] ,[Isidima_Section_DMH_S11] ,[Isidima_Section_DMH_S12] ,[Isidima_Section_DMH_S13] ,[Isidima_Section_DMH_S14] ,[Isidima_Section_DMH_S15] ,[Isidima_Section_DMH_S16] ,[Isidima_Section_DMH_S17] ,[Isidima_Section_DMH_S18] ,[Isidima_Section_DMH_S19] ,[Isidima_Section_DMH_S20] ,[Isidima_Section_DMH_S21] ,[Isidima_Section_DMH_S22] ,[Isidima_Section_DMH_Total] ,[Isidima_Section_DMH_CreatedDate] ,[Isidima_Section_DMH_CreatedBy] ,[Isidima_Section_DMH_ModifiedDate] ,[Isidima_Section_DMH_ModifiedBy] ,[Isidima_Section_DMH_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_DMH_S01 ,@Isidima_Section_DMH_S02 ,@Isidima_Section_DMH_S03 ,@Isidima_Section_DMH_S04 ,@Isidima_Section_DMH_S05 ,@Isidima_Section_DMH_S06 ,@Isidima_Section_DMH_S07 ,@Isidima_Section_DMH_S08 ,@Isidima_Section_DMH_S09 ,@Isidima_Section_DMH_S10 ,@Isidima_Section_DMH_S11 ,@Isidima_Section_DMH_S12 ,@Isidima_Section_DMH_S13 ,@Isidima_Section_DMH_S14 ,@Isidima_Section_DMH_S15 ,@Isidima_Section_DMH_S16 ,@Isidima_Section_DMH_S17 ,@Isidima_Section_DMH_S18 ,@Isidima_Section_DMH_S19 ,@Isidima_Section_DMH_S20 ,@Isidima_Section_DMH_S21 ,@Isidima_Section_DMH_S22 ,@Isidima_Section_DMH_Total ,@Isidima_Section_DMH_CreatedDate ,@Isidima_Section_DMH_CreatedBy ,@Isidima_Section_DMH_ModifiedDate ,@Isidima_Section_DMH_ModifiedBy ,@Isidima_Section_DMH_History)";
      SqlDataSource_Isidima_Form4.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_DMH] WHERE ([Isidima_Section_DMH_Id] = @Isidima_Section_DMH_Id)";
      SqlDataSource_Isidima_Form4.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_DMH] SET [Isidima_Section_DMH_S01] = @Isidima_Section_DMH_S01 ,[Isidima_Section_DMH_S02] = @Isidima_Section_DMH_S02 ,[Isidima_Section_DMH_S03] = @Isidima_Section_DMH_S03 ,[Isidima_Section_DMH_S04] = @Isidima_Section_DMH_S04 ,[Isidima_Section_DMH_S05] = @Isidima_Section_DMH_S05 ,[Isidima_Section_DMH_S06] = @Isidima_Section_DMH_S06 ,[Isidima_Section_DMH_S07] = @Isidima_Section_DMH_S07 ,[Isidima_Section_DMH_S08] = @Isidima_Section_DMH_S08 ,[Isidima_Section_DMH_S09] = @Isidima_Section_DMH_S09 ,[Isidima_Section_DMH_S10] = @Isidima_Section_DMH_S10 ,[Isidima_Section_DMH_S11] = @Isidima_Section_DMH_S11 ,[Isidima_Section_DMH_S12] = @Isidima_Section_DMH_S12 ,[Isidima_Section_DMH_S13] = @Isidima_Section_DMH_S13 ,[Isidima_Section_DMH_S14] = @Isidima_Section_DMH_S14 ,[Isidima_Section_DMH_S15] = @Isidima_Section_DMH_S15 ,[Isidima_Section_DMH_S16] = @Isidima_Section_DMH_S16 ,[Isidima_Section_DMH_S17] = @Isidima_Section_DMH_S17 ,[Isidima_Section_DMH_S18] = @Isidima_Section_DMH_S18 ,[Isidima_Section_DMH_S19] = @Isidima_Section_DMH_S19 ,[Isidima_Section_DMH_S20] = @Isidima_Section_DMH_S20 ,[Isidima_Section_DMH_S21] = @Isidima_Section_DMH_S21 ,[Isidima_Section_DMH_S22] = @Isidima_Section_DMH_S22 ,[Isidima_Section_DMH_Total] = @Isidima_Section_DMH_Total ,[Isidima_Section_DMH_ModifiedDate] = @Isidima_Section_DMH_ModifiedDate ,[Isidima_Section_DMH_ModifiedBy] = @Isidima_Section_DMH_ModifiedBy ,[Isidima_Section_DMH_History] = @Isidima_Section_DMH_History WHERE [Isidima_Section_DMH_Id] = @Isidima_Section_DMH_Id";
      SqlDataSource_Isidima_Form4.InsertParameters.Clear();
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S01", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S02", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S03", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S04", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S05", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S06", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S07", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S08", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S09", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S10", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S11", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S12", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S13", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S14", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S15", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S16", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S17", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S18", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S19", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S20", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S21", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_S22", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters.Add("Isidima_Section_DMH_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.InsertParameters["Isidima_Section_DMH_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form4.SelectParameters.Clear();
      SqlDataSource_Isidima_Form4.SelectParameters.Add("Isidima_Section_DMH_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_DMH_Id"]);
      SqlDataSource_Isidima_Form4.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S01", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S02", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S03", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S04", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S05", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S06", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S07", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S08", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S09", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S10", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S11", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S12", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S13", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S14", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S15", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S16", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S17", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S18", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S19", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S20", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S21", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_S22", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form4.UpdateParameters.Add("Isidima_Section_DMH_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form5()
    {
      SqlDataSource_Isidima_Form5.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form5.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_F] ([Isidima_Category_Id] ,[Isidima_Section_F_F01] ,[Isidima_Section_F_F02] ,[Isidima_Section_F_F03] ,[Isidima_Section_F_F04] ,[Isidima_Section_F_F05] ,[Isidima_Section_F_F06] ,[Isidima_Section_F_F07] ,[Isidima_Section_F_F08] ,[Isidima_Section_F_F09] ,[Isidima_Section_F_F10] ,[Isidima_Section_F_F11] ,[Isidima_Section_F_F12] ,[Isidima_Section_F_F13] ,[Isidima_Section_F_F14] ,[Isidima_Section_F_F15] ,[Isidima_Section_F_F16] ,[Isidima_Section_F_F17] ,[Isidima_Section_F_F18] ,[Isidima_Section_F_F19] ,[Isidima_Section_F_F20] ,[Isidima_Section_F_F21] ,[Isidima_Section_F_F22] ,[Isidima_Section_F_Total] ,[Isidima_Section_F_CreatedDate] ,[Isidima_Section_F_CreatedBy] ,[Isidima_Section_F_ModifiedDate] ,[Isidima_Section_F_ModifiedBy] ,[Isidima_Section_F_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_F_F01 ,@Isidima_Section_F_F02 ,@Isidima_Section_F_F03 ,@Isidima_Section_F_F04 ,@Isidima_Section_F_F05 ,@Isidima_Section_F_F06 ,@Isidima_Section_F_F07 ,@Isidima_Section_F_F08 ,@Isidima_Section_F_F09 ,@Isidima_Section_F_F10 ,@Isidima_Section_F_F11 ,@Isidima_Section_F_F12 ,@Isidima_Section_F_F13 ,@Isidima_Section_F_F14 ,@Isidima_Section_F_F15 ,@Isidima_Section_F_F16 ,@Isidima_Section_F_F17 ,@Isidima_Section_F_F18 ,@Isidima_Section_F_F19 ,@Isidima_Section_F_F20 ,@Isidima_Section_F_F21 ,@Isidima_Section_F_F22 ,@Isidima_Section_F_Total ,@Isidima_Section_F_CreatedDate ,@Isidima_Section_F_CreatedBy ,@Isidima_Section_F_ModifiedDate ,@Isidima_Section_F_ModifiedBy ,@Isidima_Section_F_History)";
      SqlDataSource_Isidima_Form5.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_F] WHERE ([Isidima_Section_F_Id] = @Isidima_Section_F_Id)";
      SqlDataSource_Isidima_Form5.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_F] SET [Isidima_Section_F_F01] = @Isidima_Section_F_F01 ,[Isidima_Section_F_F02] = @Isidima_Section_F_F02 ,[Isidima_Section_F_F03] = @Isidima_Section_F_F03 ,[Isidima_Section_F_F04] = @Isidima_Section_F_F04 ,[Isidima_Section_F_F05] = @Isidima_Section_F_F05 ,[Isidima_Section_F_F06] = @Isidima_Section_F_F06 ,[Isidima_Section_F_F07] = @Isidima_Section_F_F07 ,[Isidima_Section_F_F08] = @Isidima_Section_F_F08 ,[Isidima_Section_F_F09] = @Isidima_Section_F_F09 ,[Isidima_Section_F_F10] = @Isidima_Section_F_F10 ,[Isidima_Section_F_F11] = @Isidima_Section_F_F11 ,[Isidima_Section_F_F12] = @Isidima_Section_F_F12 ,[Isidima_Section_F_F13] = @Isidima_Section_F_F13 ,[Isidima_Section_F_F14] = @Isidima_Section_F_F14 ,[Isidima_Section_F_F15] = @Isidima_Section_F_F15 ,[Isidima_Section_F_F16] = @Isidima_Section_F_F16 ,[Isidima_Section_F_F17] = @Isidima_Section_F_F17 ,[Isidima_Section_F_F18] = @Isidima_Section_F_F18 ,[Isidima_Section_F_F19] = @Isidima_Section_F_F19 ,[Isidima_Section_F_F20] = @Isidima_Section_F_F20 ,[Isidima_Section_F_F21] = @Isidima_Section_F_F21 ,[Isidima_Section_F_F22] = @Isidima_Section_F_F22 ,[Isidima_Section_F_Total] = @Isidima_Section_F_Total ,[Isidima_Section_F_ModifiedDate] = @Isidima_Section_F_ModifiedDate ,[Isidima_Section_F_ModifiedBy] = @Isidima_Section_F_ModifiedBy ,[Isidima_Section_F_History] = @Isidima_Section_F_History WHERE [Isidima_Section_F_Id] = @Isidima_Section_F_Id";
      SqlDataSource_Isidima_Form5.InsertParameters.Clear();
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F01", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F02", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F03", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F04", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F05", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F06", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F07", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F08", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F09", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F10", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F11", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F12", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F13", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F14", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F15", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F16", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F17", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F18", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F19", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F20", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F21", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_F22", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters.Add("Isidima_Section_F_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.InsertParameters["Isidima_Section_F_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form5.SelectParameters.Clear();
      SqlDataSource_Isidima_Form5.SelectParameters.Add("Isidima_Section_F_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_F_Id"]);
      SqlDataSource_Isidima_Form5.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F01", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F02", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F03", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F04", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F05", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F06", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F07", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F08", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F09", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F10", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F11", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F12", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F13", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F14", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F15", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F16", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F17", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F18", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F19", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F20", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F21", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_F22", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form5.UpdateParameters.Add("Isidima_Section_F_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form6()
    {
      SqlDataSource_Isidima_Form6.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form6.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_I] ([Isidima_Category_Id] ,[Isidima_Section_I_I01] ,[Isidima_Section_I_I02] ,[Isidima_Section_I_I03] ,[Isidima_Section_I_I04] ,[Isidima_Section_I_I05] ,[Isidima_Section_I_I06] ,[Isidima_Section_I_I07] ,[Isidima_Section_I_I08] ,[Isidima_Section_I_I09] ,[Isidima_Section_I_I10] ,[Isidima_Section_I_I11] ,[Isidima_Section_I_I12] ,[Isidima_Section_I_I13] ,[Isidima_Section_I_I14] ,[Isidima_Section_I_I15] ,[Isidima_Section_I_I16] ,[Isidima_Section_I_I17] ,[Isidima_Section_I_I18] ,[Isidima_Section_I_I19] ,[Isidima_Section_I_I20] ,[Isidima_Section_I_I21] ,[Isidima_Section_I_I22] ,[Isidima_Section_I_I23] ,[Isidima_Section_I_I24] ,[Isidima_Section_I_I25] ,[Isidima_Section_I_I26] ,[Isidima_Section_I_I27] ,[Isidima_Section_I_I28] ,[Isidima_Section_I_Total] ,[Isidima_Section_I_CreatedDate] ,[Isidima_Section_I_CreatedBy] ,[Isidima_Section_I_ModifiedDate] ,[Isidima_Section_I_ModifiedBy] ,[Isidima_Section_I_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_I_I01 ,@Isidima_Section_I_I02 ,@Isidima_Section_I_I03 ,@Isidima_Section_I_I04 ,@Isidima_Section_I_I05 ,@Isidima_Section_I_I06 ,@Isidima_Section_I_I07 ,@Isidima_Section_I_I08 ,@Isidima_Section_I_I09 ,@Isidima_Section_I_I10 ,@Isidima_Section_I_I11 ,@Isidima_Section_I_I12 ,@Isidima_Section_I_I13 ,@Isidima_Section_I_I14 ,@Isidima_Section_I_I15 ,@Isidima_Section_I_I16 ,@Isidima_Section_I_I17 ,@Isidima_Section_I_I18 ,@Isidima_Section_I_I19 ,@Isidima_Section_I_I20 ,@Isidima_Section_I_I21 ,@Isidima_Section_I_I22 ,@Isidima_Section_I_I23 ,@Isidima_Section_I_I24 ,@Isidima_Section_I_I25 ,@Isidima_Section_I_I26 ,@Isidima_Section_I_I27 ,@Isidima_Section_I_I28 ,@Isidima_Section_I_Total ,@Isidima_Section_I_CreatedDate ,@Isidima_Section_I_CreatedBy ,@Isidima_Section_I_ModifiedDate ,@Isidima_Section_I_ModifiedBy ,@Isidima_Section_I_History)";
      SqlDataSource_Isidima_Form6.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_I] WHERE ([Isidima_Section_I_Id] = @Isidima_Section_I_Id)";
      SqlDataSource_Isidima_Form6.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_I] SET [Isidima_Section_I_I01] = @Isidima_Section_I_I01 ,[Isidima_Section_I_I02] = @Isidima_Section_I_I02 ,[Isidima_Section_I_I03] = @Isidima_Section_I_I03 ,[Isidima_Section_I_I04] = @Isidima_Section_I_I04 ,[Isidima_Section_I_I05] = @Isidima_Section_I_I05 ,[Isidima_Section_I_I06] = @Isidima_Section_I_I06 ,[Isidima_Section_I_I07] = @Isidima_Section_I_I07 ,[Isidima_Section_I_I08] = @Isidima_Section_I_I08 ,[Isidima_Section_I_I09] = @Isidima_Section_I_I09 ,[Isidima_Section_I_I10] = @Isidima_Section_I_I10 ,[Isidima_Section_I_I11] = @Isidima_Section_I_I11 ,[Isidima_Section_I_I12] = @Isidima_Section_I_I12 ,[Isidima_Section_I_I13] = @Isidima_Section_I_I13 ,[Isidima_Section_I_I14] = @Isidima_Section_I_I14 ,[Isidima_Section_I_I15] = @Isidima_Section_I_I15 ,[Isidima_Section_I_I16] = @Isidima_Section_I_I16 ,[Isidima_Section_I_I17] = @Isidima_Section_I_I17 ,[Isidima_Section_I_I18] = @Isidima_Section_I_I18 ,[Isidima_Section_I_I19] = @Isidima_Section_I_I19 ,[Isidima_Section_I_I20] = @Isidima_Section_I_I20 ,[Isidima_Section_I_I21] = @Isidima_Section_I_I21 ,[Isidima_Section_I_I22] = @Isidima_Section_I_I22 ,[Isidima_Section_I_I23] = @Isidima_Section_I_I23 ,[Isidima_Section_I_I24] = @Isidima_Section_I_I24 ,[Isidima_Section_I_I25] = @Isidima_Section_I_I25 ,[Isidima_Section_I_I26] = @Isidima_Section_I_I26 ,[Isidima_Section_I_I27] = @Isidima_Section_I_I27 ,[Isidima_Section_I_I28] = @Isidima_Section_I_I28 ,[Isidima_Section_I_Total] = @Isidima_Section_I_Total ,[Isidima_Section_I_ModifiedDate] = @Isidima_Section_I_ModifiedDate ,[Isidima_Section_I_ModifiedBy] = @Isidima_Section_I_ModifiedBy ,[Isidima_Section_I_History] = @Isidima_Section_I_History WHERE [Isidima_Section_I_Id] = @Isidima_Section_I_Id";
      SqlDataSource_Isidima_Form6.InsertParameters.Clear();
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I01", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I02", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I03", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I04", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I05", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I06", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I07", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I08", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I09", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I10", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I11", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I12", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I13", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I14", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I15", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I16", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I17", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I18", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I19", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I20", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I21", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I22", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I23", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I24", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I25", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I26", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I27", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_I28", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters.Add("Isidima_Section_I_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.InsertParameters["Isidima_Section_I_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form6.SelectParameters.Clear();
      SqlDataSource_Isidima_Form6.SelectParameters.Add("Isidima_Section_I_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_I_Id"]);
      SqlDataSource_Isidima_Form6.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I01", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I02", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I03", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I04", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I05", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I06", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I07", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I08", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I09", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I10", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I11", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I12", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I13", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I14", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I15", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I16", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I17", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I18", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I19", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I20", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I21", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I22", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I23", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I24", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I25", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I26", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I27", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_I28", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form6.UpdateParameters.Add("Isidima_Section_I_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form7()
    {
      SqlDataSource_Isidima_Form7.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form7.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_PSY] ([Isidima_Category_Id] ,[Isidima_Section_PSY_C01] ,[Isidima_Section_PSY_C02] ,[Isidima_Section_PSY_C03] ,[Isidima_Section_PSY_C04] ,[Isidima_Section_PSY_C05] ,[Isidima_Section_PSY_C06] ,[Isidima_Section_PSY_C07] ,[Isidima_Section_PSY_C08] ,[Isidima_Section_PSY_C09] ,[Isidima_Section_PSY_C10] ,[Isidima_Section_PSY_C11] ,[Isidima_Section_PSY_C12] ,[Isidima_Section_PSY_C13] ,[Isidima_Section_PSY_C14] ,[Isidima_Section_PSY_C15] ,[Isidima_Section_PSY_C16] ,[Isidima_Section_PSY_C17] ,[Isidima_Section_PSY_C18] ,[Isidima_Section_PSY_C19] ,[Isidima_Section_PSY_C20] ,[Isidima_Section_PSY_C21] ,[Isidima_Section_PSY_C22] ,[Isidima_Section_PSY_C23] ,[Isidima_Section_PSY_C24] ,[Isidima_Section_PSY_C25] ,[Isidima_Section_PSY_C26] ,[Isidima_Section_PSY_Total] ,[Isidima_Section_PSY_CreatedDate] ,[Isidima_Section_PSY_CreatedBy] ,[Isidima_Section_PSY_ModifiedDate] ,[Isidima_Section_PSY_ModifiedBy] ,[Isidima_Section_PSY_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_PSY_C01 ,@Isidima_Section_PSY_C02 ,@Isidima_Section_PSY_C03 ,@Isidima_Section_PSY_C04 ,@Isidima_Section_PSY_C05 ,@Isidima_Section_PSY_C06 ,@Isidima_Section_PSY_C07 ,@Isidima_Section_PSY_C08 ,@Isidima_Section_PSY_C09 ,@Isidima_Section_PSY_C10 ,@Isidima_Section_PSY_C11 ,@Isidima_Section_PSY_C12 ,@Isidima_Section_PSY_C13 ,@Isidima_Section_PSY_C14 ,@Isidima_Section_PSY_C15 ,@Isidima_Section_PSY_C16 ,@Isidima_Section_PSY_C17 ,@Isidima_Section_PSY_C18 ,@Isidima_Section_PSY_C19 ,@Isidima_Section_PSY_C20 ,@Isidima_Section_PSY_C21 ,@Isidima_Section_PSY_C22 ,@Isidima_Section_PSY_C23 ,@Isidima_Section_PSY_C24 ,@Isidima_Section_PSY_C25 ,@Isidima_Section_PSY_C26 ,@Isidima_Section_PSY_Total ,@Isidima_Section_PSY_CreatedDate ,@Isidima_Section_PSY_CreatedBy ,@Isidima_Section_PSY_ModifiedDate ,@Isidima_Section_PSY_ModifiedBy ,@Isidima_Section_PSY_History)";
      SqlDataSource_Isidima_Form7.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_PSY] WHERE ([Isidima_Section_PSY_Id] = @Isidima_Section_PSY_Id)";
      SqlDataSource_Isidima_Form7.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_PSY] SET [Isidima_Section_PSY_C01] = @Isidima_Section_PSY_C01 ,[Isidima_Section_PSY_C02] = @Isidima_Section_PSY_C02 ,[Isidima_Section_PSY_C03] = @Isidima_Section_PSY_C03 ,[Isidima_Section_PSY_C04] = @Isidima_Section_PSY_C04 ,[Isidima_Section_PSY_C05] = @Isidima_Section_PSY_C05 ,[Isidima_Section_PSY_C06] = @Isidima_Section_PSY_C06 ,[Isidima_Section_PSY_C07] = @Isidima_Section_PSY_C07 ,[Isidima_Section_PSY_C08] = @Isidima_Section_PSY_C08 ,[Isidima_Section_PSY_C09] = @Isidima_Section_PSY_C09 ,[Isidima_Section_PSY_C10] = @Isidima_Section_PSY_C10 ,[Isidima_Section_PSY_C11] = @Isidima_Section_PSY_C11 ,[Isidima_Section_PSY_C12] = @Isidima_Section_PSY_C12 ,[Isidima_Section_PSY_C13] = @Isidima_Section_PSY_C13 ,[Isidima_Section_PSY_C14] = @Isidima_Section_PSY_C14 ,[Isidima_Section_PSY_C15] = @Isidima_Section_PSY_C15 ,[Isidima_Section_PSY_C16] = @Isidima_Section_PSY_C16 ,[Isidima_Section_PSY_C17] = @Isidima_Section_PSY_C17 ,[Isidima_Section_PSY_C18] = @Isidima_Section_PSY_C18 ,[Isidima_Section_PSY_C19] = @Isidima_Section_PSY_C19 ,[Isidima_Section_PSY_C20] = @Isidima_Section_PSY_C20 ,[Isidima_Section_PSY_C21] = @Isidima_Section_PSY_C21 ,[Isidima_Section_PSY_C22] = @Isidima_Section_PSY_C22 ,[Isidima_Section_PSY_C23] = @Isidima_Section_PSY_C23 ,[Isidima_Section_PSY_C24] = @Isidima_Section_PSY_C24 ,[Isidima_Section_PSY_C25] = @Isidima_Section_PSY_C25 ,[Isidima_Section_PSY_C26] = @Isidima_Section_PSY_C26 ,[Isidima_Section_PSY_Total] = @Isidima_Section_PSY_Total ,[Isidima_Section_PSY_ModifiedDate] = @Isidima_Section_PSY_ModifiedDate ,[Isidima_Section_PSY_ModifiedBy] = @Isidima_Section_PSY_ModifiedBy ,[Isidima_Section_PSY_History] = @Isidima_Section_PSY_History WHERE [Isidima_Section_PSY_Id] = @Isidima_Section_PSY_Id";
      SqlDataSource_Isidima_Form7.InsertParameters.Clear();
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C01", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C02", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C03", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C04", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C05", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C06", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C07", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C08", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C09", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C10", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C11", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C12", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C13", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C14", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C15", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C16", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C17", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C18", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C19", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C20", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C21", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C22", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C23", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C24", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C25", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_C26", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters.Add("Isidima_Section_PSY_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.InsertParameters["Isidima_Section_PSY_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form7.SelectParameters.Clear();
      SqlDataSource_Isidima_Form7.SelectParameters.Add("Isidima_Section_PSY_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_PSY_Id"]);
      SqlDataSource_Isidima_Form7.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C01", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C02", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C03", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C04", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C05", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C06", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C07", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C08", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C09", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C10", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C11", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C12", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C13", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C14", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C15", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C16", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C17", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C18", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C19", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C20", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C21", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C22", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C23", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C24", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C25", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_C26", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form7.UpdateParameters.Add("Isidima_Section_PSY_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form8()
    {
      SqlDataSource_Isidima_Form8.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form8.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_T] ([Isidima_Category_Id] ,[Isidima_Section_T_T01] ,[Isidima_Section_T_T02] ,[Isidima_Section_T_T03] ,[Isidima_Section_T_T04] ,[Isidima_Section_T_T05] ,[Isidima_Section_T_T06] ,[Isidima_Section_T_T07] ,[Isidima_Section_T_T08] ,[Isidima_Section_T_T09] ,[Isidima_Section_T_T10] ,[Isidima_Section_T_T11] ,[Isidima_Section_T_T12] ,[Isidima_Section_T_T13] ,[Isidima_Section_T_T14] ,[Isidima_Section_T_T15] ,[Isidima_Section_T_T16] ,[Isidima_Section_T_T17] ,[Isidima_Section_T_T18] ,[Isidima_Section_T_T19] ,[Isidima_Section_T_T20] ,[Isidima_Section_T_T21] ,[Isidima_Section_T_T22] ,[Isidima_Section_T_T23] ,[Isidima_Section_T_Total] ,[Isidima_Section_T_CreatedDate] ,[Isidima_Section_T_CreatedBy] ,[Isidima_Section_T_ModifiedDate] ,[Isidima_Section_T_ModifiedBy] ,[Isidima_Section_T_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_T_T01 ,@Isidima_Section_T_T02 ,@Isidima_Section_T_T03 ,@Isidima_Section_T_T04 ,@Isidima_Section_T_T05 ,@Isidima_Section_T_T06 ,@Isidima_Section_T_T07 ,@Isidima_Section_T_T08 ,@Isidima_Section_T_T09 ,@Isidima_Section_T_T10 ,@Isidima_Section_T_T11 ,@Isidima_Section_T_T12 ,@Isidima_Section_T_T13 ,@Isidima_Section_T_T14 ,@Isidima_Section_T_T15 ,@Isidima_Section_T_T16 ,@Isidima_Section_T_T17 ,@Isidima_Section_T_T18 ,@Isidima_Section_T_T19 ,@Isidima_Section_T_T20 ,@Isidima_Section_T_T21 ,@Isidima_Section_T_T22 ,@Isidima_Section_T_T23 ,@Isidima_Section_T_Total ,@Isidima_Section_T_CreatedDate ,@Isidima_Section_T_CreatedBy ,@Isidima_Section_T_ModifiedDate ,@Isidima_Section_T_ModifiedBy ,@Isidima_Section_T_History)";
      SqlDataSource_Isidima_Form8.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_T] WHERE ([Isidima_Section_T_Id] = @Isidima_Section_T_Id)";
      SqlDataSource_Isidima_Form8.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_T] SET [Isidima_Section_T_T01] = @Isidima_Section_T_T01 ,[Isidima_Section_T_T02] = @Isidima_Section_T_T02 ,[Isidima_Section_T_T03] = @Isidima_Section_T_T03 ,[Isidima_Section_T_T04] = @Isidima_Section_T_T04 ,[Isidima_Section_T_T05] = @Isidima_Section_T_T05 ,[Isidima_Section_T_T06] = @Isidima_Section_T_T06 ,[Isidima_Section_T_T07] = @Isidima_Section_T_T07 ,[Isidima_Section_T_T08] = @Isidima_Section_T_T08 ,[Isidima_Section_T_T09] = @Isidima_Section_T_T09 ,[Isidima_Section_T_T10] = @Isidima_Section_T_T10 ,[Isidima_Section_T_T11] = @Isidima_Section_T_T11 ,[Isidima_Section_T_T12] = @Isidima_Section_T_T12 ,[Isidima_Section_T_T13] = @Isidima_Section_T_T13 ,[Isidima_Section_T_T14] = @Isidima_Section_T_T14 ,[Isidima_Section_T_T15] = @Isidima_Section_T_T15 ,[Isidima_Section_T_T16] = @Isidima_Section_T_T16 ,[Isidima_Section_T_T17] = @Isidima_Section_T_T17 ,[Isidima_Section_T_T18] = @Isidima_Section_T_T18 ,[Isidima_Section_T_T19] = @Isidima_Section_T_T19 ,[Isidima_Section_T_T20] = @Isidima_Section_T_T20 ,[Isidima_Section_T_T21] = @Isidima_Section_T_T21 ,[Isidima_Section_T_T22] = @Isidima_Section_T_T22 ,[Isidima_Section_T_T23] = @Isidima_Section_T_T23 ,[Isidima_Section_T_Total] = @Isidima_Section_T_Total ,[Isidima_Section_T_ModifiedDate] = @Isidima_Section_T_ModifiedDate ,[Isidima_Section_T_ModifiedBy] = @Isidima_Section_T_ModifiedBy ,[Isidima_Section_T_History] = @Isidima_Section_T_History WHERE [Isidima_Section_T_Id] = @Isidima_Section_T_Id";
      SqlDataSource_Isidima_Form8.InsertParameters.Clear();
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T01", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T02", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T03", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T04", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T05", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T06", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T07", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T08", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T09", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T10", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T11", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T12", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T13", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T14", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T15", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T16", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T17", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T18", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T19", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T20", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T21", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T22", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_T23", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters.Add("Isidima_Section_T_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.InsertParameters["Isidima_Section_T_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form8.SelectParameters.Clear();
      SqlDataSource_Isidima_Form8.SelectParameters.Add("Isidima_Section_T_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_T_Id"]);
      SqlDataSource_Isidima_Form8.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T01", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T02", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T03", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T04", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T05", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T06", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T07", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T08", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T09", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T10", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T11", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T12", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T13", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T14", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T15", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T16", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T17", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T18", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T19", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T20", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T21", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T22", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_T23", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form8.UpdateParameters.Add("Isidima_Section_T_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form9()
    {
      SqlDataSource_Isidima_Form9.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form9.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_B] ([Isidima_Category_Id] ,[Isidima_Section_B_B01] ,[Isidima_Section_B_B02] ,[Isidima_Section_B_B03] ,[Isidima_Section_B_B04] ,[Isidima_Section_B_B05] ,[Isidima_Section_B_B06] ,[Isidima_Section_B_B07] ,[Isidima_Section_B_B08] ,[Isidima_Section_B_B09] ,[Isidima_Section_B_B10] ,[Isidima_Section_B_B11] ,[Isidima_Section_B_B12] ,[Isidima_Section_B_B13] ,[Isidima_Section_B_B14] ,[Isidima_Section_B_B15] ,[Isidima_Section_B_B16] ,[Isidima_Section_B_B17] ,[Isidima_Section_B_B18] ,[Isidima_Section_B_B19] ,[Isidima_Section_B_B20] ,[Isidima_Section_B_B21] ,[Isidima_Section_B_B22] ,[Isidima_Section_B_B23] ,[Isidima_Section_B_B24] ,[Isidima_Section_B_B25] ,[Isidima_Section_B_B26] ,[Isidima_Section_B_B27] ,[Isidima_Section_B_B28] ,[Isidima_Section_B_Total] ,[Isidima_Section_B_CreatedDate] ,[Isidima_Section_B_CreatedBy] ,[Isidima_Section_B_ModifiedDate] ,[Isidima_Section_B_ModifiedBy] ,[Isidima_Section_B_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_B_B01 ,@Isidima_Section_B_B02 ,@Isidima_Section_B_B03 ,@Isidima_Section_B_B04 ,@Isidima_Section_B_B05 ,@Isidima_Section_B_B06 ,@Isidima_Section_B_B07 ,@Isidima_Section_B_B08 ,@Isidima_Section_B_B09 ,@Isidima_Section_B_B10 ,@Isidima_Section_B_B11 ,@Isidima_Section_B_B12 ,@Isidima_Section_B_B13 ,@Isidima_Section_B_B14 ,@Isidima_Section_B_B15 ,@Isidima_Section_B_B16 ,@Isidima_Section_B_B17 ,@Isidima_Section_B_B18 ,@Isidima_Section_B_B19 ,@Isidima_Section_B_B20 ,@Isidima_Section_B_B21 ,@Isidima_Section_B_B22 ,@Isidima_Section_B_B23 ,@Isidima_Section_B_B24 ,@Isidima_Section_B_B25 ,@Isidima_Section_B_B26 ,@Isidima_Section_B_B27 ,@Isidima_Section_B_B28 ,@Isidima_Section_B_Total ,@Isidima_Section_B_CreatedDate ,@Isidima_Section_B_CreatedBy ,@Isidima_Section_B_ModifiedDate ,@Isidima_Section_B_ModifiedBy ,@Isidima_Section_B_History)";
      SqlDataSource_Isidima_Form9.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_B] WHERE ([Isidima_Section_B_Id] = @Isidima_Section_B_Id)";
      SqlDataSource_Isidima_Form9.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_B] SET [Isidima_Section_B_B01] = @Isidima_Section_B_B01 ,[Isidima_Section_B_B02] = @Isidima_Section_B_B02 ,[Isidima_Section_B_B03] = @Isidima_Section_B_B03 ,[Isidima_Section_B_B04] = @Isidima_Section_B_B04 ,[Isidima_Section_B_B05] = @Isidima_Section_B_B05 ,[Isidima_Section_B_B06] = @Isidima_Section_B_B06 ,[Isidima_Section_B_B07] = @Isidima_Section_B_B07 ,[Isidima_Section_B_B08] = @Isidima_Section_B_B08 ,[Isidima_Section_B_B09] = @Isidima_Section_B_B09 ,[Isidima_Section_B_B10] = @Isidima_Section_B_B10 ,[Isidima_Section_B_B11] = @Isidima_Section_B_B11 ,[Isidima_Section_B_B12] = @Isidima_Section_B_B12 ,[Isidima_Section_B_B13] = @Isidima_Section_B_B13 ,[Isidima_Section_B_B14] = @Isidima_Section_B_B14 ,[Isidima_Section_B_B15] = @Isidima_Section_B_B15 ,[Isidima_Section_B_B16] = @Isidima_Section_B_B16 ,[Isidima_Section_B_B17] = @Isidima_Section_B_B17 ,[Isidima_Section_B_B18] = @Isidima_Section_B_B18 ,[Isidima_Section_B_B19] = @Isidima_Section_B_B19 ,[Isidima_Section_B_B20] = @Isidima_Section_B_B20 ,[Isidima_Section_B_B21] = @Isidima_Section_B_B21 ,[Isidima_Section_B_B22] = @Isidima_Section_B_B22 ,[Isidima_Section_B_B23] = @Isidima_Section_B_B23 ,[Isidima_Section_B_B24] = @Isidima_Section_B_B24 ,[Isidima_Section_B_B25] = @Isidima_Section_B_B25 ,[Isidima_Section_B_B26] = @Isidima_Section_B_B26 ,[Isidima_Section_B_B27] = @Isidima_Section_B_B27 ,[Isidima_Section_B_B28] = @Isidima_Section_B_B28 ,[Isidima_Section_B_Total] = @Isidima_Section_B_Total ,[Isidima_Section_B_ModifiedDate] = @Isidima_Section_B_ModifiedDate ,[Isidima_Section_B_ModifiedBy] = @Isidima_Section_B_ModifiedBy ,[Isidima_Section_B_History] = @Isidima_Section_B_History WHERE [Isidima_Section_B_Id] = @Isidima_Section_B_Id";
      SqlDataSource_Isidima_Form9.InsertParameters.Clear();
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B01", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B02", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B03", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B04", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B05", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B06", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B07", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B08", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B09", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B10", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B11", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B12", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B13", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B14", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B15", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B16", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B17", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B18", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B19", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B20", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B21", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B22", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B23", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B24", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B25", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B26", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B27", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_B28", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters.Add("Isidima_Section_B_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.InsertParameters["Isidima_Section_B_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form9.SelectParameters.Clear();
      SqlDataSource_Isidima_Form9.SelectParameters.Add("Isidima_Section_B_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_B_Id"]);
      SqlDataSource_Isidima_Form9.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B01", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B02", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B03", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B04", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B05", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B06", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B07", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B08", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B09", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B10", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B11", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B12", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B13", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B14", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B15", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B16", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B17", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B18", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B19", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B20", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B21", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B22", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B23", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B24", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B25", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B26", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B27", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_B28", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form9.UpdateParameters.Add("Isidima_Section_B_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form10()
    {
      SqlDataSource_Isidima_Form10.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form10.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_R] ([Isidima_Category_Id] ,[Isidima_Section_R_R01] ,[Isidima_Section_R_R02] ,[Isidima_Section_R_R03] ,[Isidima_Section_R_R04] ,[Isidima_Section_R_R05] ,[Isidima_Section_R_R06] ,[Isidima_Section_R_R07] ,[Isidima_Section_R_R08] ,[Isidima_Section_R_R09] ,[Isidima_Section_R_R10] ,[Isidima_Section_R_R11] ,[Isidima_Section_R_R12] ,[Isidima_Section_R_R13] ,[Isidima_Section_R_R14] ,[Isidima_Section_R_R15] ,[Isidima_Section_R_R16] ,[Isidima_Section_R_R17] ,[Isidima_Section_R_R18] ,[Isidima_Section_R_Total] ,[Isidima_Section_R_CreatedDate] ,[Isidima_Section_R_CreatedBy] ,[Isidima_Section_R_ModifiedDate] ,[Isidima_Section_R_ModifiedBy] ,[Isidima_Section_R_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_R_R01 ,@Isidima_Section_R_R02 ,@Isidima_Section_R_R03 ,@Isidima_Section_R_R04 ,@Isidima_Section_R_R05 ,@Isidima_Section_R_R06 ,@Isidima_Section_R_R07 ,@Isidima_Section_R_R08 ,@Isidima_Section_R_R09 ,@Isidima_Section_R_R10 ,@Isidima_Section_R_R11 ,@Isidima_Section_R_R12 ,@Isidima_Section_R_R13 ,@Isidima_Section_R_R14 ,@Isidima_Section_R_R15 ,@Isidima_Section_R_R16 ,@Isidima_Section_R_R17 ,@Isidima_Section_R_R18 ,@Isidima_Section_R_Total ,@Isidima_Section_R_CreatedDate ,@Isidima_Section_R_CreatedBy ,@Isidima_Section_R_ModifiedDate ,@Isidima_Section_R_ModifiedBy ,@Isidima_Section_R_History)";
      SqlDataSource_Isidima_Form10.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_R] WHERE ([Isidima_Section_R_Id] = @Isidima_Section_R_Id)";
      SqlDataSource_Isidima_Form10.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_R] SET [Isidima_Section_R_R01] = @Isidima_Section_R_R01 ,[Isidima_Section_R_R02] = @Isidima_Section_R_R02 ,[Isidima_Section_R_R03] = @Isidima_Section_R_R03 ,[Isidima_Section_R_R04] = @Isidima_Section_R_R04 ,[Isidima_Section_R_R05] = @Isidima_Section_R_R05 ,[Isidima_Section_R_R06] = @Isidima_Section_R_R06 ,[Isidima_Section_R_R07] = @Isidima_Section_R_R07 ,[Isidima_Section_R_R08] = @Isidima_Section_R_R08 ,[Isidima_Section_R_R09] = @Isidima_Section_R_R09 ,[Isidima_Section_R_R10] = @Isidima_Section_R_R10 ,[Isidima_Section_R_R11] = @Isidima_Section_R_R11 ,[Isidima_Section_R_R12] = @Isidima_Section_R_R12 ,[Isidima_Section_R_R13] = @Isidima_Section_R_R13 ,[Isidima_Section_R_R14] = @Isidima_Section_R_R14 ,[Isidima_Section_R_R15] = @Isidima_Section_R_R15 ,[Isidima_Section_R_R16] = @Isidima_Section_R_R16 ,[Isidima_Section_R_R17] = @Isidima_Section_R_R17 ,[Isidima_Section_R_R18] = @Isidima_Section_R_R18 ,[Isidima_Section_R_Total] = @Isidima_Section_R_Total ,[Isidima_Section_R_ModifiedDate] = @Isidima_Section_R_ModifiedDate ,[Isidima_Section_R_ModifiedBy] = @Isidima_Section_R_ModifiedBy ,[Isidima_Section_R_History] = @Isidima_Section_R_History WHERE [Isidima_Section_R_Id] = @Isidima_Section_R_Id";
      SqlDataSource_Isidima_Form10.InsertParameters.Clear();
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R01", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R02", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R03", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R04", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R05", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R06", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R07", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R08", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R09", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R10", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R11", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R12", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R13", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R14", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R15", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R16", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R17", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_R18", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters.Add("Isidima_Section_R_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.InsertParameters["Isidima_Section_R_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form10.SelectParameters.Clear();
      SqlDataSource_Isidima_Form10.SelectParameters.Add("Isidima_Section_R_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_R_Id"]);
      SqlDataSource_Isidima_Form10.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R01", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R02", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R03", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R04", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R05", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R06", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R07", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R08", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R09", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R10", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R11", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R12", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R13", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R14", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R15", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R16", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R17", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_R18", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form10.UpdateParameters.Add("Isidima_Section_R_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form11()
    {
      SqlDataSource_Isidima_Form11.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form11.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_S] ([Isidima_Category_Id] ,[Isidima_Section_S_S01] ,[Isidima_Section_S_S02] ,[Isidima_Section_S_S03] ,[Isidima_Section_S_S04] ,[Isidima_Section_S_S05] ,[Isidima_Section_S_S06] ,[Isidima_Section_S_S07] ,[Isidima_Section_S_S08] ,[Isidima_Section_S_S09] ,[Isidima_Section_S_S10] ,[Isidima_Section_S_S11] ,[Isidima_Section_S_S12] ,[Isidima_Section_S_S13] ,[Isidima_Section_S_S14] ,[Isidima_Section_S_S15] ,[Isidima_Section_S_S16] ,[Isidima_Section_S_S17] ,[Isidima_Section_S_S18] ,[Isidima_Section_S_S19] ,[Isidima_Section_S_S20] ,[Isidima_Section_S_S21] ,[Isidima_Section_S_S22] ,[Isidima_Section_S_S23] ,[Isidima_Section_S_S24] ,[Isidima_Section_S_S25] ,[Isidima_Section_S_Total] ,[Isidima_Section_S_CreatedDate] ,[Isidima_Section_S_CreatedBy] ,[Isidima_Section_S_ModifiedDate] ,[Isidima_Section_S_ModifiedBy] ,[Isidima_Section_S_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_S_S01 ,@Isidima_Section_S_S02 ,@Isidima_Section_S_S03 ,@Isidima_Section_S_S04 ,@Isidima_Section_S_S05 ,@Isidima_Section_S_S06 ,@Isidima_Section_S_S07 ,@Isidima_Section_S_S08 ,@Isidima_Section_S_S09 ,@Isidima_Section_S_S10 ,@Isidima_Section_S_S11 ,@Isidima_Section_S_S12 ,@Isidima_Section_S_S13 ,@Isidima_Section_S_S14 ,@Isidima_Section_S_S15 ,@Isidima_Section_S_S16 ,@Isidima_Section_S_S17 ,@Isidima_Section_S_S18 ,@Isidima_Section_S_S19 ,@Isidima_Section_S_S20 ,@Isidima_Section_S_S21 ,@Isidima_Section_S_S22 ,@Isidima_Section_S_S23 ,@Isidima_Section_S_S24 ,@Isidima_Section_S_S25 ,@Isidima_Section_S_Total ,@Isidima_Section_S_CreatedDate ,@Isidima_Section_S_CreatedBy ,@Isidima_Section_S_ModifiedDate ,@Isidima_Section_S_ModifiedBy ,@Isidima_Section_S_History)";
      SqlDataSource_Isidima_Form11.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_S] WHERE ([Isidima_Section_S_Id] = @Isidima_Section_S_Id)";
      SqlDataSource_Isidima_Form11.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_S] SET [Isidima_Section_S_S01] = @Isidima_Section_S_S01 ,[Isidima_Section_S_S02] = @Isidima_Section_S_S02 ,[Isidima_Section_S_S03] = @Isidima_Section_S_S03 ,[Isidima_Section_S_S04] = @Isidima_Section_S_S04 ,[Isidima_Section_S_S05] = @Isidima_Section_S_S05 ,[Isidima_Section_S_S06] = @Isidima_Section_S_S06 ,[Isidima_Section_S_S07] = @Isidima_Section_S_S07 ,[Isidima_Section_S_S08] = @Isidima_Section_S_S08 ,[Isidima_Section_S_S09] = @Isidima_Section_S_S09 ,[Isidima_Section_S_S10] = @Isidima_Section_S_S10 ,[Isidima_Section_S_S11] = @Isidima_Section_S_S11 ,[Isidima_Section_S_S12] = @Isidima_Section_S_S12 ,[Isidima_Section_S_S13] = @Isidima_Section_S_S13 ,[Isidima_Section_S_S14] = @Isidima_Section_S_S14 ,[Isidima_Section_S_S15] = @Isidima_Section_S_S15 ,[Isidima_Section_S_S16] = @Isidima_Section_S_S16 ,[Isidima_Section_S_S17] = @Isidima_Section_S_S17 ,[Isidima_Section_S_S18] = @Isidima_Section_S_S18 ,[Isidima_Section_S_S19] = @Isidima_Section_S_S19 ,[Isidima_Section_S_S20] = @Isidima_Section_S_S20 ,[Isidima_Section_S_S21] = @Isidima_Section_S_S21 ,[Isidima_Section_S_S22] = @Isidima_Section_S_S22 ,[Isidima_Section_S_S23] = @Isidima_Section_S_S23 ,[Isidima_Section_S_S24] = @Isidima_Section_S_S24 ,[Isidima_Section_S_S25] = @Isidima_Section_S_S25 ,[Isidima_Section_S_Total] = @Isidima_Section_S_Total ,[Isidima_Section_S_ModifiedDate] = @Isidima_Section_S_ModifiedDate ,[Isidima_Section_S_ModifiedBy] = @Isidima_Section_S_ModifiedBy ,[Isidima_Section_S_History] = @Isidima_Section_S_History WHERE [Isidima_Section_S_Id] = @Isidima_Section_S_Id";
      SqlDataSource_Isidima_Form11.InsertParameters.Clear();
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S01", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S02", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S03", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S04", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S05", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S06", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S07", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S08", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S09", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S10", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S11", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S12", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S13", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S14", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S15", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S16", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S17", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S18", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S19", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S20", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S21", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S22", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S23", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S24", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_S25", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters.Add("Isidima_Section_S_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.InsertParameters["Isidima_Section_S_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form11.SelectParameters.Clear();
      SqlDataSource_Isidima_Form11.SelectParameters.Add("Isidima_Section_S_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_S_Id"]);
      SqlDataSource_Isidima_Form11.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S01", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S02", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S03", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S04", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S05", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S06", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S07", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S08", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S09", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S10", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S11", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S12", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S13", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S14", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S15", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S16", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S17", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S18", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S19", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S20", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S21", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S22", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S23", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S24", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_S25", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form11.UpdateParameters.Add("Isidima_Section_S_Id", TypeCode.Int32, "");
    }

    private void SqlDataSourceSetup_Form12()
    {
      SqlDataSource_Isidima_Form12.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Form12.InsertCommand = "INSERT INTO [InfoQuest_Form_Isidima_Section_V] ([Isidima_Category_Id] ,[Isidima_Section_V_V01] ,[Isidima_Section_V_V02] ,[Isidima_Section_V_V03] ,[Isidima_Section_V_V04] ,[Isidima_Section_V_V05] ,[Isidima_Section_V_V06] ,[Isidima_Section_V_V07] ,[Isidima_Section_V_V08] ,[Isidima_Section_V_V09] ,[Isidima_Section_V_V10] ,[Isidima_Section_V_V11] ,[Isidima_Section_V_V12] ,[Isidima_Section_V_V13] ,[Isidima_Section_V_V14] ,[Isidima_Section_V_V15] ,[Isidima_Section_V_V16] ,[Isidima_Section_V_V17] ,[Isidima_Section_V_V18] ,[Isidima_Section_V_V19] ,[Isidima_Section_V_V20] ,[Isidima_Section_V_V21] ,[Isidima_Section_V_Total] ,[Isidima_Section_V_CreatedDate] ,[Isidima_Section_V_CreatedBy] ,[Isidima_Section_V_ModifiedDate] ,[Isidima_Section_V_ModifiedBy] ,[Isidima_Section_V_History]) VALUES (@Isidima_Category_Id ,@Isidima_Section_V_V01 ,@Isidima_Section_V_V02 ,@Isidima_Section_V_V03 ,@Isidima_Section_V_V04 ,@Isidima_Section_V_V05 ,@Isidima_Section_V_V06 ,@Isidima_Section_V_V07 ,@Isidima_Section_V_V08 ,@Isidima_Section_V_V09 ,@Isidima_Section_V_V10 ,@Isidima_Section_V_V11 ,@Isidima_Section_V_V12 ,@Isidima_Section_V_V13 ,@Isidima_Section_V_V14 ,@Isidima_Section_V_V15 ,@Isidima_Section_V_V16 ,@Isidima_Section_V_V17 ,@Isidima_Section_V_V18 ,@Isidima_Section_V_V19 ,@Isidima_Section_V_V20 ,@Isidima_Section_V_V21 ,@Isidima_Section_V_Total ,@Isidima_Section_V_CreatedDate ,@Isidima_Section_V_CreatedBy ,@Isidima_Section_V_ModifiedDate ,@Isidima_Section_V_ModifiedBy ,@Isidima_Section_V_History)";
      SqlDataSource_Isidima_Form12.SelectCommand = "SELECT * FROM [InfoQuest_Form_Isidima_Section_V] WHERE ([Isidima_Section_V_Id] = @Isidima_Section_V_Id)";
      SqlDataSource_Isidima_Form12.UpdateCommand = "UPDATE [InfoQuest_Form_Isidima_Section_V] SET [Isidima_Section_V_V01] = @Isidima_Section_V_V01 ,[Isidima_Section_V_V02] = @Isidima_Section_V_V02 ,[Isidima_Section_V_V03] = @Isidima_Section_V_V03 ,[Isidima_Section_V_V04] = @Isidima_Section_V_V04 ,[Isidima_Section_V_V05] = @Isidima_Section_V_V05 ,[Isidima_Section_V_V06] = @Isidima_Section_V_V06 ,[Isidima_Section_V_V07] = @Isidima_Section_V_V07 ,[Isidima_Section_V_V08] = @Isidima_Section_V_V08 ,[Isidima_Section_V_V09] = @Isidima_Section_V_V09 ,[Isidima_Section_V_V10] = @Isidima_Section_V_V10 ,[Isidima_Section_V_V11] = @Isidima_Section_V_V11 ,[Isidima_Section_V_V12] = @Isidima_Section_V_V12 ,[Isidima_Section_V_V13] = @Isidima_Section_V_V13 ,[Isidima_Section_V_V14] = @Isidima_Section_V_V14 ,[Isidima_Section_V_V15] = @Isidima_Section_V_V15 ,[Isidima_Section_V_V16] = @Isidima_Section_V_V16 ,[Isidima_Section_V_V17] = @Isidima_Section_V_V17 ,[Isidima_Section_V_V18] = @Isidima_Section_V_V18 ,[Isidima_Section_V_V19] = @Isidima_Section_V_V19 ,[Isidima_Section_V_V20] = @Isidima_Section_V_V20 ,[Isidima_Section_V_V21] = @Isidima_Section_V_V21 ,[Isidima_Section_V_Total] = @Isidima_Section_V_Total ,[Isidima_Section_V_ModifiedDate] = @Isidima_Section_V_ModifiedDate ,[Isidima_Section_V_ModifiedBy] = @Isidima_Section_V_ModifiedBy ,[Isidima_Section_V_History] = @Isidima_Section_V_History WHERE [Isidima_Section_V_Id] = @Isidima_Section_V_Id";
      SqlDataSource_Isidima_Form12.InsertParameters.Clear();
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Category_Id", TypeCode.Int32, Request.QueryString["Isidima_Category_Id"]);
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V01", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V02", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V03", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V04", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V05", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V06", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V07", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V08", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V09", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V10", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V11", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V12", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V13", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V14", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V15", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V16", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V17", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V18", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V19", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V20", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_V21", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters.Add("Isidima_Section_V_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.InsertParameters["Isidima_Section_V_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Form12.SelectParameters.Clear();
      SqlDataSource_Isidima_Form12.SelectParameters.Add("Isidima_Section_V_Id", TypeCode.Int32, Request.QueryString["Isidima_Section_V_Id"]);
      SqlDataSource_Isidima_Form12.UpdateParameters.Clear();
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V01", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V02", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V03", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V04", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V05", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V06", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V07", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V08", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V09", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V10", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V11", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V12", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V13", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V14", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V15", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V16", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V17", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V18", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V19", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V20", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_V21", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_Total", TypeCode.Int32, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_History", TypeCode.String, "");
      SqlDataSource_Isidima_Form12.UpdateParameters.Add("Isidima_Section_V_Id", TypeCode.Int32, "");
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
        if (Request.QueryString["s_Isidima_PatientVisitNumber"] == null)
        {
          TextBox_PatientVisitNumber.Text = "";
        }
        else
        {
          TextBox_PatientVisitNumber.Text = Request.QueryString["s_Isidima_PatientVisitNumber"];
        }
      }
    }

    protected void Button_CaptureNew_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=0", false);
    }

    protected void Button_Admin_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_Isidima_Admin.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    private void PatientDataPI()
    {
      DataTable DataTable_PatientDataPI;
      using (DataTable_PatientDataPI = new DataTable())
      {
        DataTable_PatientDataPI.Locale = CultureInfo.CurrentCulture;
        //DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_ODS_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_Isidima_PatientVisitNumber"]).Copy();
        DataTable_PatientDataPI = InfoQuestWCF.InfoQuest_DataPatient.DataPatient_EDW_VisitInformation(Request.QueryString["s_Facility_Id"], Request.QueryString["s_Isidima_PatientVisitNumber"]).Copy();

        if (DataTable_PatientDataPI.Columns.Count == 1)
        {
          Session["IsidimaPIId"] = "";
          string SQLStringPatientInfo = "SELECT Isidima_PI_Id FROM InfoQuest_Form_Isidima_PatientInformation WHERE Facility_Id = @FacilityId AND Isidima_PI_PatientVisitNumber = @IsidimaPIPatientVisitNumber";
          using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
          {
            SqlCommand_PatientInfo.Parameters.AddWithValue("@FacilityId", Request.QueryString["s_Facility_Id"]);
            SqlCommand_PatientInfo.Parameters.AddWithValue("@IsidimaPIPatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
            DataTable DataTable_PatientInfo;
            using (DataTable_PatientInfo = new DataTable())
            {
              DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
              DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
              if (DataTable_PatientInfo.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                {
                  Session["IsidimaPIId"] = DataRow_Row1["Isidima_PI_Id"];
                }
              }
            }
          }

          if (string.IsNullOrEmpty(Session["IsidimaPIId"].ToString()))
          {
            Session["Error"] = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["Error"] = DataRow_Row["Error"];
            }

            Label_InvalidSearch.Text = Session["Error"].ToString();
            TablePatientInfo.Visible = false; TableListLinks.Visible = false; TableList.Visible = false;
            TableForm0View.Visible = false; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;
            Session["Error"] = "";
          }
          else
          {
            Session["Error"] = "";
            foreach (DataRow DataRow_Row in DataTable_PatientDataPI.Rows)
            {
              Session["Error"] = DataRow_Row["Error"];
            }

            Label_InvalidSearch.Text = Session["Error"].ToString() + Convert.ToString("<br />No Patient related data could be updated but you can continue capturing new form(s) and updating and viewing previous form(s)", CultureInfo.CurrentCulture);
            Session["Error"] = "";
          }

          Session["IsidimaPIId"] = "";
        }
        else if (DataTable_PatientDataPI.Columns.Count != 1)
        {
          PatientDataPI_PatientInformation(DataTable_PatientDataPI);
        }
      }
    }

    private void PatientDataPI_PatientInformation(DataTable dataTable_PatientDataPI)
    {
      if (dataTable_PatientDataPI != null)
      {
        if (dataTable_PatientDataPI.Rows.Count == 0)
        {
          Label_InvalidSearch.Text = Convert.ToString("Patient Visit Number " + Request.QueryString["s_Isidima_PatientVisitNumber"] + " does not Exist", CultureInfo.CurrentCulture);
          TablePatientInfo.Visible = false; TableListLinks.Visible = false; TableList.Visible = false;
          TableForm0View.Visible = false; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;
        }
        else
        {
          foreach (DataRow DataRow_Row in dataTable_PatientDataPI.Rows)
          {
            Session["VisitNumber"] = DataRow_Row["VisitNumber"];
            Session["NameSurname"] = DataRow_Row["Surname"] + "," + DataRow_Row["Name"];
            Session["Age"] = DataRow_Row["PatientAge"];
            Session["AdmissionDate"] = DataRow_Row["DateOfAdmission"];
            Session["DischargeDate"] = DataRow_Row["DateOfDischarge"];
            Session["Ward"] = DataRow_Row["Ward"];

            string NameSurnamePI = Session["NameSurname"].ToString();
            NameSurnamePI = NameSurnamePI.Replace("'", "");
            Session["NameSurname"] = NameSurnamePI;
            NameSurnamePI = "";

            string WardPI = Session["Ward"].ToString();
            WardPI = WardPI.Replace("'", "");
            Session["Ward"] = WardPI;
            WardPI = "";

            Session["IsidimaPIId"] = "";
            string SQLStringPatientInfo = "SELECT Isidima_PI_Id FROM InfoQuest_Form_Isidima_PatientInformation WHERE Facility_Id = @FacilityId AND Isidima_PI_PatientVisitNumber = @IsidimaPIPatientVisitNumber";
            using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
            {
              SqlCommand_PatientInfo.Parameters.AddWithValue("@FacilityId", Request.QueryString["s_Facility_Id"]);
              SqlCommand_PatientInfo.Parameters.AddWithValue("@IsidimaPIPatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
              DataTable DataTable_PatientInfo;
              using (DataTable_PatientInfo = new DataTable())
              {
                DataTable_PatientInfo.Locale = CultureInfo.CurrentCulture;
                DataTable_PatientInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PatientInfo).Copy();
                if (DataTable_PatientInfo.Rows.Count > 0)
                {
                  foreach (DataRow DataRow_Row1 in DataTable_PatientInfo.Rows)
                  {
                    Session["IsidimaPIId"] = DataRow_Row1["Isidima_PI_Id"];
                  }
                }
              }
            }

            if (string.IsNullOrEmpty(Session["IsidimaPIId"].ToString()))
            {
              string SQLStringInsertIsidimaPI = "INSERT INTO InfoQuest_Form_Isidima_PatientInformation ( Facility_Id , Isidima_PI_PatientVisitNumber , Isidima_PI_PatientName , Isidima_PI_PatientAge , Isidima_PI_PatientDateOfAdmission , Isidima_PI_PatientDateofDischarge , Isidima_PI_PatientWard , Isidima_PI_Archived ) VALUES ( @Facility_Id , @Isidima_PI_PatientVisitNumber , @Isidima_PI_PatientName , @Isidima_PI_PatientAge , @Isidima_PI_PatientDateOfAdmission , @Isidima_PI_PatientDateofDischarge , @Isidima_PI_PatientWard , @Isidima_PI_Archived )";
              using (SqlCommand SqlCommand_InsertIsidimaPI = new SqlCommand(SQLStringInsertIsidimaPI))
              {
                SqlCommand_InsertIsidimaPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_InsertIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientVisitNumber", Session["VisitNumber"].ToString());
                SqlCommand_InsertIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientName", Session["NameSurname"].ToString());
                SqlCommand_InsertIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientAge", Session["Age"].ToString());
                SqlCommand_InsertIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientDateOfAdmission", Session["AdmissionDate"].ToString());
                SqlCommand_InsertIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientDateofDischarge", Session["DischargeDate"].ToString());
                SqlCommand_InsertIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientWard", Session["Ward"].ToString());
                SqlCommand_InsertIsidimaPI.Parameters.AddWithValue("@Isidima_PI_Archived", 0);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertIsidimaPI);
              }


              Session["First"] = "";
              Session["Second"] = "";
              Session["Third"] = "";
              string SQLStringDefaultAssessmentPeriod = "SELECT [77] AS [First] , [78] AS [Second] , [70] AS [Third] FROM (SELECT ListItem_Name , ListCategory_Id FROM vAdministration_ListItem_Active WHERE ListCategory_Id IN (70 , 77 , 78)) AS TempTable PIVOT (MAX(ListItem_Name) FOR ListCategory_Id IN ([70] , [77] , [78])) AS TempTable";
              using (SqlCommand SqlCommand_DefaultAssessmentPeriod = new SqlCommand(SQLStringDefaultAssessmentPeriod))
              {
                DataTable DataTable_DefaultAssessmentPeriod;
                using (DataTable_DefaultAssessmentPeriod = new DataTable())
                {
                  DataTable_DefaultAssessmentPeriod.Locale = CultureInfo.CurrentCulture;
                  DataTable_DefaultAssessmentPeriod = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_DefaultAssessmentPeriod).Copy();
                  if (DataTable_DefaultAssessmentPeriod.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row1 in DataTable_DefaultAssessmentPeriod.Rows)
                    {
                      Session["First"] = DataRow_Row1["First"];
                      Session["Second"] = DataRow_Row1["Second"];
                      Session["Third"] = DataRow_Row1["Third"];
                    }
                  }
                }
              }

              string SQLStringInsertAdminAssessmentPeriod = "INSERT INTO InfoQuest_Form_Isidima_AdminAssessmentPeriod ( Facility_Id , Isidima_AdminAssessmentPeriod_PatientVisitNumber , Isidima_AdminAssessmentPeriod_Period1 , Isidima_AdminAssessmentPeriod_Period2 , Isidima_AdminAssessmentPeriod_Period3 , Isidima_AdminAssessmentPeriod_CreatedDate , Isidima_AdminAssessmentPeriod_CreatedBy , Isidima_AdminAssessmentPeriod_ModifiedDate , Isidima_AdminAssessmentPeriod_ModifiedBy) VALUES ( @Facility_Id , @Isidima_AdminAssessmentPeriod_PatientVisitNumber , @Isidima_AdminAssessmentPeriod_Period1 , @Isidima_AdminAssessmentPeriod_Period2 , @Isidima_AdminAssessmentPeriod_Period3 , @Isidima_AdminAssessmentPeriod_CreatedDate , @Isidima_AdminAssessmentPeriod_CreatedBy , @Isidima_AdminAssessmentPeriod_ModifiedDate , @Isidima_AdminAssessmentPeriod_ModifiedBy)";
              using (SqlCommand SqlCommand_InsertAdminAssessmentPeriod = new SqlCommand(SQLStringInsertAdminAssessmentPeriod))
              {
                SqlCommand_InsertAdminAssessmentPeriod.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_InsertAdminAssessmentPeriod.Parameters.AddWithValue("@Isidima_AdminAssessmentPeriod_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
                SqlCommand_InsertAdminAssessmentPeriod.Parameters.AddWithValue("@Isidima_AdminAssessmentPeriod_Period1", Session["First"].ToString());
                SqlCommand_InsertAdminAssessmentPeriod.Parameters.AddWithValue("@Isidima_AdminAssessmentPeriod_Period2", Session["Second"].ToString());
                SqlCommand_InsertAdminAssessmentPeriod.Parameters.AddWithValue("@Isidima_AdminAssessmentPeriod_Period3", Session["Third"].ToString());
                SqlCommand_InsertAdminAssessmentPeriod.Parameters.AddWithValue("@Isidima_AdminAssessmentPeriod_CreatedDate", DateTime.Now);
                SqlCommand_InsertAdminAssessmentPeriod.Parameters.AddWithValue("@Isidima_AdminAssessmentPeriod_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                SqlCommand_InsertAdminAssessmentPeriod.Parameters.AddWithValue("@Isidima_AdminAssessmentPeriod_ModifiedDate", DateTime.Now);
                SqlCommand_InsertAdminAssessmentPeriod.Parameters.AddWithValue("@Isidima_AdminAssessmentPeriod_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertAdminAssessmentPeriod);
              }

              Session["First"] = "";
              Session["Second"] = "";
              Session["Third"] = "";

              string SQLStringInsertAdminDischarge = "INSERT INTO InfoQuest_Form_Isidima_AdminDischarge ( Facility_Id , Isidima_AdminDischarge_PatientVisitNumber , Isidima_AdminDischarge_Patient , Isidima_AdminDischarge_CreatedDate , Isidima_AdminDischarge_CreatedBy , Isidima_AdminDischarge_ModifiedDate , Isidima_AdminDischarge_ModifiedBy) VALUES (@Facility_Id , @Isidima_AdminDischarge_PatientVisitNumber , @Isidima_AdminDischarge_Patient , @Isidima_AdminDischarge_CreatedDate , @Isidima_AdminDischarge_CreatedBy , @Isidima_AdminDischarge_ModifiedDate , @Isidima_AdminDischarge_ModifiedBy)";
              using (SqlCommand SqlCommand_InsertAdminDischarge = new SqlCommand(SQLStringInsertAdminDischarge))
              {
                SqlCommand_InsertAdminDischarge.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_InsertAdminDischarge.Parameters.AddWithValue("@Isidima_AdminDischarge_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
                SqlCommand_InsertAdminDischarge.Parameters.AddWithValue("@Isidima_AdminDischarge_Patient", 0);
                SqlCommand_InsertAdminDischarge.Parameters.AddWithValue("@Isidima_AdminDischarge_CreatedDate", DateTime.Now);
                SqlCommand_InsertAdminDischarge.Parameters.AddWithValue("@Isidima_AdminDischarge_CreatedBy", Request.ServerVariables["LOGON_USER"]);
                SqlCommand_InsertAdminDischarge.Parameters.AddWithValue("@Isidima_AdminDischarge_ModifiedDate", DateTime.Now);
                SqlCommand_InsertAdminDischarge.Parameters.AddWithValue("@Isidima_AdminDischarge_ModifiedBy", Request.ServerVariables["LOGON_USER"]);
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_InsertAdminDischarge);
              }
            }
            else
            {
              string SQLStringUpdateIsidimaPI = "UPDATE InfoQuest_Form_Isidima_PatientInformation SET Isidima_PI_PatientName = @Isidima_PI_PatientName , Isidima_PI_PatientAge = @Isidima_PI_PatientAge , Isidima_PI_PatientDateOfAdmission = @Isidima_PI_PatientDateOfAdmission , Isidima_PI_PatientDateofDischarge = @Isidima_PI_PatientDateofDischarge , Isidima_PI_PatientWard = @Isidima_PI_PatientWard WHERE Facility_Id = @Facility_Id AND Isidima_PI_PatientVisitNumber = @Isidima_PI_PatientVisitNumber ";
              using (SqlCommand SqlCommand_UpdateIsidimaPI = new SqlCommand(SQLStringUpdateIsidimaPI))
              {
                SqlCommand_UpdateIsidimaPI.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_UpdateIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientVisitNumber", Session["VisitNumber"].ToString());
                SqlCommand_UpdateIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientName", Session["NameSurname"].ToString());
                SqlCommand_UpdateIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientAge", Session["Age"].ToString());
                SqlCommand_UpdateIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientDateOfAdmission", Session["AdmissionDate"].ToString());
                SqlCommand_UpdateIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientDateofDischarge", Session["DischargeDate"].ToString());
                SqlCommand_UpdateIsidimaPI.Parameters.AddWithValue("@Isidima_PI_PatientWard", Session["Ward"].ToString());
                InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateIsidimaPI);
              }
            }
            Session["IsidimaPIId"] = "";
          }
        }
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["Form"] == "0")
      {
        SetForm0Visibility();
      }
      else if (Request.QueryString["Form"] != "0" && Request.QueryString["Isidima_Category_Id"] != null)
      {
        if (Request.QueryString["Form"] == "1")
        {
          SetForm1Visibility();
        }
        else if (Request.QueryString["Form"] == "2")
        {
          SetForm2Visibility();
        }
        else if (Request.QueryString["Form"] == "3")
        {
          SetForm3Visibility();
        }
        else if (Request.QueryString["Form"] == "4")
        {
          SetForm4Visibility();
        }
        else if (Request.QueryString["Form"] == "5")
        {
          SetForm5Visibility();
        }
        else if (Request.QueryString["Form"] == "6")
        {
          SetForm6Visibility();
        }
        else if (Request.QueryString["Form"] == "7")
        {
          SetForm7Visibility();
        }
        else if (Request.QueryString["Form"] == "8")
        {
          SetForm8Visibility();
        }
        else if (Request.QueryString["Form"] == "9")
        {
          SetForm9Visibility();
        }
        else if (Request.QueryString["Form"] == "10")
        {
          SetForm10Visibility();
        }
        else if (Request.QueryString["Form"] == "11")
        {
          SetForm11Visibility();
        }
        else if (Request.QueryString["Form"] == "12")
        {
          SetForm12Visibility();
        }
        else
        {
          TablePatientInfo.Visible = true; TableListLinks.Visible = true; TableList.Visible = true;
          TableForm0View.Visible = false; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;
        }
      }
    }

    private void SetForm0Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = false; TableForm0.Visible = true; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
            DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              if (Request.QueryString["Isidima_Category_Id"] != null)
              {
                DetailsView_Isidima_Form0.ChangeMode(DetailsViewMode.Edit);
              }
              else
              {
                DetailsView_Isidima_Form0.ChangeMode(DetailsViewMode.Insert);
              }
            }

            if (Session["Security"].ToString() == "1")
            {
              Session["Security"] = "0";
              TableForm0.Visible = false;
            }
            Session["Security"] = "1";
          }
        }
      }
    }
    private void Form0Visible()
    {
      if (DetailsView_Isidima_Form0.CurrentMode == DetailsViewMode.Insert)
      {
        ((DropDownList)DetailsView_Isidima_Form0.FindControl("DropDownList_InsertPatientCategory")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_InsertDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_InsertDate")).Attributes.Add("OnInput", "Validation_Form();");
      }

      if (DetailsView_Isidima_Form0.CurrentMode == DetailsViewMode.Edit)
      {
        ((DropDownList)DetailsView_Isidima_Form0.FindControl("DropDownList_EditPatientCategory")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_EditDate")).Attributes.Add("OnChange", "Validation_Form();");
        ((TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_EditDate")).Attributes.Add("OnInput", "Validation_Form();");
      }
    }

    private void SetForm1Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = true; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3004");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm1.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityAdministration_MHA_Update = DataTable_FormMode.Select("SecurityRole_Id = '90'");
              DataRow[] SecurityFacilityAdministration_MHA_View = DataTable_FormMode.Select("SecurityRole_Id = '91'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityAdministration_MHA_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_MHA_Id"] != null)
                {
                  DetailsView_Isidima_Form1.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionMHAId"] = "";
                  string SQLStringSectionMHA = "SELECT Isidima_Section_MHA_Id FROM InfoQuest_Form_Isidima_Section_MHA WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionMHA = new SqlCommand(SQLStringSectionMHA))
                  {
                    SqlCommand_SectionMHA.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionMHA;
                    using (DataTable_SectionMHA = new DataTable())
                    {
                      DataTable_SectionMHA.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionMHA = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionMHA).Copy();
                      if (DataTable_SectionMHA.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionMHA.Rows)
                        {
                          Session["IsidimaSectionMHAId"] = DataRow_Row["Isidima_Section_MHA_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionMHAId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionMHAId"].ToString()))
                  {
                    DetailsView_Isidima_Form1.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=1&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_MHA_Id=" + Session["IsidimaSectionMHAId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionMHAId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityAdministration_MHA_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form1.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm1.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form1Visible()
    {
      int MHA_TotalQuestions = 19;
      HiddenField HiddenField_MHA_TotalQuestions = (HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_TotalQuestions");
      HiddenField_MHA_TotalQuestions.Value = MHA_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form1.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form1.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3004);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form1.FindControl("Label_MHA_A0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form1.FindControl("RadioButtonList_" + ViewMode + "MHA_A0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("A0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form1.FindControl("Label_MHA_A" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form1.FindControl("RadioButtonList_" + ViewMode + "MHA_A" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("A" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= MHA_TotalQuestions; a++)
      //{
      //  Label Label_MHA_A;
      //  HiddenField HiddenField_MHA_AY;
      //  HiddenField HiddenField_MHA_AN;

      //  if (a < 10)
      //  {
      //    Label_MHA_A = (Label)DetailsView_Isidima_Form1.FindControl("Label_MHA_A0" + a + "");
      //    HiddenField_MHA_AY = (HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A0" + a + "Yes");
      //    HiddenField_MHA_AN = (HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form1.FindControl("RadioButtonList_" + ViewMode + "MHA_A0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_MHA_A = (Label)DetailsView_Isidima_Form1.FindControl("Label_MHA_A" + a + "");
      //    HiddenField_MHA_AY = (HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A" + a + "Yes");
      //    HiddenField_MHA_AN = (HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form1.FindControl("RadioButtonList_" + ViewMode + "MHA_A" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "2"; Label_MHA_A.Text = Convert.ToString("A01. Completed LE Application Form", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "2"; Label_MHA_A.Text = Convert.ToString("A02. Prescription from referring facility", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A03. MHCA 04", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A04. MHCA 05 X 2", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A05. MHCA 06", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A06. MHCA 11", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A07. MHCA 08", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A08. MHCA 14", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "2"; Label_MHA_A.Text = Convert.ToString("A09. Complies with SLA conditions for admission", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A10. Family contact person available", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A11. Family meeting held pre-admission", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A12. Grants have been cancelled", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_MHA_AY.Value = "1"; HiddenField_MHA_AN.Value = "0"; Label_MHA_A.Text = Convert.ToString("A13. Declassified State patient", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "1"; Label_MHA_A.Text = Convert.ToString("A14. Admission has been discussed with user", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "2"; Label_MHA_A.Text = Convert.ToString("A15. Identity document is present", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "2"; Label_MHA_A.Text = Convert.ToString("A16. Person is resident in the Province", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "3"; Label_MHA_A.Text = Convert.ToString("A17. MDT / Admission Screening team meeting held pre-admission", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "3"; Label_MHA_A.Text = Convert.ToString("A18. Users personal property has been attended to", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_MHA_AY.Value = "0"; HiddenField_MHA_AN.Value = "3"; Label_MHA_A.Text = Convert.ToString("A19. South African citizen", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm2Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = true; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3005");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm2.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityAdministration_VPA_Update = DataTable_FormMode.Select("SecurityRole_Id = '92'");
              DataRow[] SecurityFacilityAdministration_VPA_View = DataTable_FormMode.Select("SecurityRole_Id = '93'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityAdministration_VPA_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_VPA_Id"] != null)
                {
                  DetailsView_Isidima_Form2.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionVPAId"] = "";
                  string SQLStringSectionVPA = "SELECT Isidima_Section_VPA_Id FROM InfoQuest_Form_Isidima_Section_VPA WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionVPA = new SqlCommand(SQLStringSectionVPA))
                  {
                    SqlCommand_SectionVPA.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionVPA;
                    using (DataTable_SectionVPA = new DataTable())
                    {
                      DataTable_SectionVPA.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionVPA = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionVPA).Copy();
                      if (DataTable_SectionVPA.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionVPA.Rows)
                        {
                          Session["IsidimaSectionVPAId"] = DataRow_Row["Isidima_Section_VPA_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionVPAId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionVPAId"].ToString()))
                  {
                    DetailsView_Isidima_Form2.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=2&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_VPA_Id=" + Session["IsidimaSectionVPAId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionVPAId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityAdministration_VPA_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form2.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm2.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form2Visible()
    {
      int VPA_TotalQuestions = 13;
      HiddenField HiddenField_VPA_TotalQuestions = (HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_TotalQuestions");
      HiddenField_VPA_TotalQuestions.Value = VPA_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form2.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form2.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3005);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form2.FindControl("Label_VPA_A0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form2.FindControl("RadioButtonList_" + ViewMode + "VPA_A0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("A0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form2.FindControl("Label_VPA_A" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form2.FindControl("RadioButtonList_" + ViewMode + "VPA_A" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("A" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= VPA_TotalQuestions; a++)
      //{
      //  Label Label_VPA_A;
      //  HiddenField HiddenField_VPA_AY;
      //  HiddenField HiddenField_VPA_AN;

      //  if (a < 10)
      //  {
      //    Label_VPA_A = (Label)DetailsView_Isidima_Form2.FindControl("Label_VPA_A0" + a + "");
      //    HiddenField_VPA_AY = (HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A0" + a + "Yes");
      //    HiddenField_VPA_AN = (HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form2.FindControl("RadioButtonList_" + ViewMode + "VPA_A0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_VPA_A = (Label)DetailsView_Isidima_Form2.FindControl("Label_VPA_A" + a + "");
      //    HiddenField_VPA_AY = (HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A" + a + "Yes");
      //    HiddenField_VPA_AN = (HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form2.FindControl("RadioButtonList_" + ViewMode + "VPA_A" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "1"; Label_VPA_A.Text = Convert.ToString("A01. Completed LE Application form", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "1"; Label_VPA_A.Text = Convert.ToString("A02. Prescription from referring facility", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "1"; Label_VPA_A.Text = Convert.ToString("A03. Complies with SLA conditions for admission", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "1"; Label_VPA_A.Text = Convert.ToString("A04. Family contact person available", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "1"; Label_VPA_A.Text = Convert.ToString("A05. Family meeting pre-admission", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "1"; Label_VPA_A.Text = Convert.ToString("A06. Full referral docs re condition available", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "1"; Label_VPA_A.Text = Convert.ToString("A07. Family contact made pre-admission", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "1"; Label_VPA_A.Text = Convert.ToString("A08. Date set for admission", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "2"; Label_VPA_A.Text = Convert.ToString("A09. ID present", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "2"; Label_VPA_A.Text = Convert.ToString("A10. Resident in the province", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "2"; Label_VPA_A.Text = Convert.ToString("A11. MDT Meeting pre-admission", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "2"; Label_VPA_A.Text = Convert.ToString("A12. South African citizen", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_VPA_AY.Value = "0"; HiddenField_VPA_AN.Value = "2"; Label_VPA_A.Text = Convert.ToString("A13. Special request from Dept", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm3Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = true; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3006");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm3.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityChild_J_Update = DataTable_FormMode.Select("SecurityRole_Id = '94'");
              DataRow[] SecurityFacilityChild_J_View = DataTable_FormMode.Select("SecurityRole_Id = '95'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityChild_J_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_J_Id"] != null)
                {
                  DetailsView_Isidima_Form3.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionJId"] = "";
                  string SQLStringSectionJ = "SELECT Isidima_Section_J_Id FROM InfoQuest_Form_Isidima_Section_J WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionJ = new SqlCommand(SQLStringSectionJ))
                  {
                    SqlCommand_SectionJ.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionJ;
                    using (DataTable_SectionJ = new DataTable())
                    {
                      DataTable_SectionJ.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionJ = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionJ).Copy();
                      if (DataTable_SectionJ.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionJ.Rows)
                        {
                          Session["IsidimaSectionJId"] = DataRow_Row["Isidima_Section_J_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionJId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionJId"].ToString()))
                  {
                    DetailsView_Isidima_Form3.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=3&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_J_Id=" + Session["IsidimaSectionJId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionJId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityChild_J_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form3.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm3.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form3Visible()
    {
      int J_TotalQuestions = 26;
      HiddenField HiddenField_J_TotalQuestions = (HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_TotalQuestions");
      HiddenField_J_TotalQuestions.Value = J_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form3.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form3.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3006);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form3.FindControl("Label_J_J0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form3.FindControl("RadioButtonList_" + ViewMode + "J_J0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("J0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form3.FindControl("Label_J_J" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form3.FindControl("RadioButtonList_" + ViewMode + "J_J" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("J" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= J_TotalQuestions; a++)
      //{
      //  Label Label_J_J;
      //  HiddenField HiddenField_J_JY;
      //  HiddenField HiddenField_J_JN;

      //  if (a < 10)
      //  {
      //    Label_J_J = (Label)DetailsView_Isidima_Form3.FindControl("Label_J_J0" + a + "");
      //    HiddenField_J_JY = (HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J0" + a + "Yes");
      //    HiddenField_J_JN = (HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form3.FindControl("RadioButtonList_" + ViewMode + "J_J0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_J_J = (Label)DetailsView_Isidima_Form3.FindControl("Label_J_J" + a + "");
      //    HiddenField_J_JY = (HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J" + a + "Yes");
      //    HiddenField_J_JN = (HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form3.FindControl("RadioButtonList_" + ViewMode + "J_J" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J01. Smiles and laughs", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J02. Claps hands", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J03. Goes up and down stairs", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J04. Reacts to changes in environment", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J05. Shouts and makes a noise", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J06. Runs / jumps / climbs", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J07. Obeys instructions - stop, go, come here", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J08. Toilet trained", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J09. Identifies self", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J10. Speaks", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J11. Feeds self", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J12. Makes eye contact", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J13. Knows where places are in the environment", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J14. Counts", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J15. Remembers songs and rhymes", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J16. Identifies pictures / colors", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J17. Uses toys appropriately", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "1"; Label_J_J.Text = Convert.ToString("J18. Packs away / fetches items from cupboards", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "2"; Label_J_J.Text = Convert.ToString("J19. Height within normal range", CultureInfo.CurrentCulture); break;
      //    case 20: HiddenField_J_JY.Value = "0"; HiddenField_J_JN.Value = "2"; Label_J_J.Text = Convert.ToString("J20. Weight within normal range", CultureInfo.CurrentCulture); break;
      //    case 21: HiddenField_J_JY.Value = "2"; HiddenField_J_JN.Value = "0"; Label_J_J.Text = Convert.ToString("J21. Withdrawn", CultureInfo.CurrentCulture); break;
      //    case 22: HiddenField_J_JY.Value = "2"; HiddenField_J_JN.Value = "0"; Label_J_J.Text = Convert.ToString("J22. Listless / no energy", CultureInfo.CurrentCulture); break;
      //    case 23: HiddenField_J_JY.Value = "2"; HiddenField_J_JN.Value = "0"; Label_J_J.Text = Convert.ToString("J23. Excessive energy", CultureInfo.CurrentCulture); break;
      //    case 24: HiddenField_J_JY.Value = "2"; HiddenField_J_JN.Value = "0"; Label_J_J.Text = Convert.ToString("J24. Bite / grabs", CultureInfo.CurrentCulture); break;
      //    case 25: HiddenField_J_JY.Value = "2"; HiddenField_J_JN.Value = "0"; Label_J_J.Text = Convert.ToString("J25. Harms self", CultureInfo.CurrentCulture); break;
      //    case 26: HiddenField_J_JY.Value = "2"; HiddenField_J_JN.Value = "0"; Label_J_J.Text = Convert.ToString("J26. Harms others", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm4Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = true; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3007");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm4.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityDischarge_DMH_Update = DataTable_FormMode.Select("SecurityRole_Id = '96'");
              DataRow[] SecurityFacilityDischarge_DMH_View = DataTable_FormMode.Select("SecurityRole_Id = '97'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityDischarge_DMH_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_DMH_Id"] != null)
                {
                  DetailsView_Isidima_Form4.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionDMHId"] = "";
                  string SQLStringSectionDMH = "SELECT Isidima_Section_DMH_Id FROM InfoQuest_Form_Isidima_Section_DMH WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionDMH = new SqlCommand(SQLStringSectionDMH))
                  {
                    SqlCommand_SectionDMH.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionDMH;
                    using (DataTable_SectionDMH = new DataTable())
                    {
                      DataTable_SectionDMH.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionDMH = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionDMH).Copy();
                      if (DataTable_SectionDMH.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionDMH.Rows)
                        {
                          Session["IsidimaSectionDMHId"] = DataRow_Row["Isidima_Section_DMH_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionDMHId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionDMHId"].ToString()))
                  {
                    DetailsView_Isidima_Form4.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=4&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_DMH_Id=" + Session["IsidimaSectionDMHId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionDMHId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityDischarge_DMH_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form4.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm4.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form4Visible()
    {
      int DMH_TotalQuestions = 22;
      HiddenField HiddenField_DMH_TotalQuestions = (HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_TotalQuestions");
      HiddenField_DMH_TotalQuestions.Value = DMH_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form4.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form4.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3007);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form4.FindControl("Label_DMH_S0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form4.FindControl("RadioButtonList_" + ViewMode + "DMH_S0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("S0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form4.FindControl("Label_DMH_S" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form4.FindControl("RadioButtonList_" + ViewMode + "DMH_S" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("S" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= DMH_TotalQuestions; a++)
      //{
      //  Label Label_DMH_S;
      //  HiddenField HiddenField_DMH_SY;
      //  HiddenField HiddenField_DMH_SN;

      //  if (a < 10)
      //  {
      //    Label_DMH_S = (Label)DetailsView_Isidima_Form4.FindControl("Label_DMH_S0" + a + "");
      //    HiddenField_DMH_SY = (HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S0" + a + "Yes");
      //    HiddenField_DMH_SN = (HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form4.FindControl("RadioButtonList_" + ViewMode + "DMH_S0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_DMH_S = (Label)DetailsView_Isidima_Form4.FindControl("Label_DMH_S" + a + "");
      //    HiddenField_DMH_SY = (HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S" + a + "Yes");
      //    HiddenField_DMH_SN = (HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form4.FindControl("RadioButtonList_" + ViewMode + "DMH_S" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S01. All documentation is complete", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S02. Family education session completed", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S03. Family maintains regular contact", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S04. Family counseling done", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S05. A family member is at home during the day", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S06. Family has financial means", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S07. There is a clinic in close proximity to home", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S08. Has been on LOA without incident", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S09. Transport available for easy access to resources", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S10. User status changed", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S11. Disability Grant has been applied for", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S12. User will live independently", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S13. User has insight into condition", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S14. User will attend follow up community services", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S15. Qualifies for UIF / private pension", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S16. Has work skills / is financially independent", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "1"; Label_DMH_S.Text = Convert.ToString("S17. User has been involved in discharge planning", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "2"; Label_DMH_S.Text = Convert.ToString("S18. Home environment is suitable for discharge", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_DMH_SY.Value = "3"; HiddenField_DMH_SN.Value = "0"; Label_DMH_S.Text = Convert.ToString("S19. Aggressive towards family members", CultureInfo.CurrentCulture); break;
      //    case 20: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "2"; Label_DMH_S.Text = Convert.ToString("S20. Family able to take user home", CultureInfo.CurrentCulture); break;
      //    case 21: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "3"; Label_DMH_S.Text = Convert.ToString("S21. Day care available near accommodation", CultureInfo.CurrentCulture); break;
      //    case 22: HiddenField_DMH_SY.Value = "0"; HiddenField_DMH_SN.Value = "3"; Label_DMH_S.Text = Convert.ToString("S22. NGO / Shelter / OAH available to place user", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm5Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = true; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3008");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm5.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityFunction_F_Update = DataTable_FormMode.Select("SecurityRole_Id = '98'");
              DataRow[] SecurityFacilityFunction_F_View = DataTable_FormMode.Select("SecurityRole_Id = '99'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityFunction_F_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_F_Id"] != null)
                {
                  DetailsView_Isidima_Form5.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionFId"] = "";
                  string SQLStringSectionF = "SELECT Isidima_Section_F_Id FROM InfoQuest_Form_Isidima_Section_F WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionF = new SqlCommand(SQLStringSectionF))
                  {
                    SqlCommand_SectionF.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionF;
                    using (DataTable_SectionF = new DataTable())
                    {
                      DataTable_SectionF.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionF = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionF).Copy();
                      if (DataTable_SectionF.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionF.Rows)
                        {
                          Session["IsidimaSectionFId"] = DataRow_Row["Isidima_Section_F_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionFId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionFId"].ToString()))
                  {
                    DetailsView_Isidima_Form5.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=5&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_F_Id=" + Session["IsidimaSectionFId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionFId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityFunction_F_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form5.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm5.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form5Visible()
    {
      int F_TotalQuestions = 22;
      HiddenField HiddenField_F_TotalQuestions = (HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_TotalQuestions");
      HiddenField_F_TotalQuestions.Value = F_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form5.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form5.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3008);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form5.FindControl("Label_F_F0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form5.FindControl("RadioButtonList_" + ViewMode + "F_F0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("F0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form5.FindControl("Label_F_F" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form5.FindControl("RadioButtonList_" + ViewMode + "F_F" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("F" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= F_TotalQuestions; a++)
      //{
      //  Label Label_F_F;
      //  HiddenField HiddenField_F_FY;
      //  HiddenField HiddenField_F_FN;

      //  if (a < 10)
      //  {
      //    Label_F_F = (Label)DetailsView_Isidima_Form5.FindControl("Label_F_F0" + a + "");
      //    HiddenField_F_FY = (HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F0" + a + "Yes");
      //    HiddenField_F_FN = (HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form5.FindControl("RadioButtonList_" + ViewMode + "F_F0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_F_F = (Label)DetailsView_Isidima_Form5.FindControl("Label_F_F" + a + "");
      //    HiddenField_F_FY = (HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F" + a + "Yes");
      //    HiddenField_F_FN = (HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form5.FindControl("RadioButtonList_" + ViewMode + "F_F" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F01. Can hear", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F02. Can see", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F03. Can swallow when fed", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F04. Can eat by himself / herself", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F05. Responds to stimulation (tough, light, voice etc)", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F06. Orientated to place, person and time", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F07. Short term memory functional", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F08. Long term memory functional", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F09. Identifies and solve problems", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F10. Follows basic instructions", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F11. Makes eye contact", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F12. Follows basic conversation", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F13. Makes sounds to make needs known", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F14. Gestures to make self understood", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F15. Uses expressive language", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F16. Speaks when spoken to", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_F_FY.Value = "0"; HiddenField_F_FN.Value = "1"; Label_F_F.Text = Convert.ToString("F17. Speech is clearly understood by others", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_F_FY.Value = "2"; HiddenField_F_FN.Value = "0"; Label_F_F.Text = Convert.ToString("F18. Needs special equipment - Assistive devices", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_F_FY.Value = "2"; HiddenField_F_FN.Value = "0"; Label_F_F.Text = Convert.ToString("F19. Assistance needed during meal times", CultureInfo.CurrentCulture); break;
      //    case 20: HiddenField_F_FY.Value = "2"; HiddenField_F_FN.Value = "0"; Label_F_F.Text = Convert.ToString("F20. 24 hour nursing required for physical care", CultureInfo.CurrentCulture); break;
      //    case 21: HiddenField_F_FY.Value = "2"; HiddenField_F_FN.Value = "0"; Label_F_F.Text = Convert.ToString("F21. Specialized stimulation required", CultureInfo.CurrentCulture); break;
      //    case 22: HiddenField_F_FY.Value = "2"; HiddenField_F_FN.Value = "0"; Label_F_F.Text = Convert.ToString("F22. Additional therapy needed - speech, physiotherapy", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm6Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = true; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3009");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm6.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityIndependence_I_Update = DataTable_FormMode.Select("SecurityRole_Id = '100'");
              DataRow[] SecurityFacilityIndependence_I_View = DataTable_FormMode.Select("SecurityRole_Id = '101'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityIndependence_I_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_I_Id"] != null)
                {
                  DetailsView_Isidima_Form6.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionIId"] = "";
                  string SQLStringSectionI = "SELECT Isidima_Section_I_Id FROM InfoQuest_Form_Isidima_Section_I WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionI = new SqlCommand(SQLStringSectionI))
                  {
                    SqlCommand_SectionI.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionI;
                    using (DataTable_SectionI = new DataTable())
                    {
                      DataTable_SectionI.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionI).Copy();
                      if (DataTable_SectionI.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionI.Rows)
                        {
                          Session["IsidimaSectionIId"] = DataRow_Row["Isidima_Section_I_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionIId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionIId"].ToString()))
                  {
                    DetailsView_Isidima_Form6.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=6&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_I_Id=" + Session["IsidimaSectionIId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionIId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityIndependence_I_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form6.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm6.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form6Visible()
    {
      int I_TotalQuestions = 28;
      HiddenField HiddenField_I_TotalQuestions = (HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_TotalQuestions");
      HiddenField_I_TotalQuestions.Value = I_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form6.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form6.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3009);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form6.FindControl("Label_I_I0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form6.FindControl("RadioButtonList_" + ViewMode + "I_I0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("I0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form6.FindControl("Label_I_I" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form6.FindControl("RadioButtonList_" + ViewMode + "I_I" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("I" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= I_TotalQuestions; a++)
      //{
      //  Label Label_I_I;
      //  HiddenField HiddenField_I_IY;
      //  HiddenField HiddenField_I_IN;

      //  if (a < 10)
      //  {
      //    Label_I_I = (Label)DetailsView_Isidima_Form6.FindControl("Label_I_I0" + a + "");
      //    HiddenField_I_IY = (HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I0" + a + "Yes");
      //    HiddenField_I_IN = (HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form6.FindControl("RadioButtonList_" + ViewMode + "I_I0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_I_I = (Label)DetailsView_Isidima_Form6.FindControl("Label_I_I" + a + "");
      //    HiddenField_I_IY = (HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I" + a + "Yes");
      //    HiddenField_I_IN = (HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form6.FindControl("RadioButtonList_" + ViewMode + "I_I" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I01. Shows appropriate emotion", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I02. Dresses appropriately", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I03. Pulls up zips / does buttons & ties laces", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I04. Wears weather appropriate clothing", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I05. Cares for own personal hygiene unaided", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I06. Wakes up independently", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I07. Uses the toilet appropriately", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I08. Uses the telephone", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I09. Makes own bed", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I10. Eats unaided", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I11. Reports illness / discomfort", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I12. Follows instructions", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I13. Goes to places unaided", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I14. Makes own meals", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I15. Does own washing and ironing", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I16. Budgets own money", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I17. Takes own medication", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I18. Shows initiative", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I19. Makes and acts on independent decisions", CultureInfo.CurrentCulture); break;
      //    case 20: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I20. Acts culture, role, gender and age appropriately", CultureInfo.CurrentCulture); break;
      //    case 21: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "1"; Label_I_I.Text = Convert.ToString("I21. Differentiates between right and wrong", CultureInfo.CurrentCulture); break;
      //    case 22: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "2"; Label_I_I.Text = Convert.ToString("I22. Orientated to time place & date", CultureInfo.CurrentCulture); break;
      //    case 23: HiddenField_I_IY.Value = "0"; HiddenField_I_IN.Value = "2"; Label_I_I.Text = Convert.ToString("I23. Identifies personal belongings", CultureInfo.CurrentCulture); break;
      //    case 24: HiddenField_I_IY.Value = "3"; HiddenField_I_IN.Value = "0"; Label_I_I.Text = Convert.ToString("I24. Is aggressive", CultureInfo.CurrentCulture); break;
      //    case 25: HiddenField_I_IY.Value = "3"; HiddenField_I_IN.Value = "0"; Label_I_I.Text = Convert.ToString("I25. Is destructive to property", CultureInfo.CurrentCulture); break;
      //    case 26: HiddenField_I_IY.Value = "3"; HiddenField_I_IN.Value = "0"; Label_I_I.Text = Convert.ToString("I26. Harms self / others", CultureInfo.CurrentCulture); break;
      //    case 27: HiddenField_I_IY.Value = "3"; HiddenField_I_IN.Value = "0"; Label_I_I.Text = Convert.ToString("I27. Deviant sexual behaviors and thinking", CultureInfo.CurrentCulture); break;
      //    case 28: HiddenField_I_IY.Value = "2"; HiddenField_I_IN.Value = "0"; Label_I_I.Text = Convert.ToString("I28. Substance abuse", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm7Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = true; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3010");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm7.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityMentalHealth_PSY_Update = DataTable_FormMode.Select("SecurityRole_Id = '102'");
              DataRow[] SecurityFacilityMentalHealth_PSY_View = DataTable_FormMode.Select("SecurityRole_Id = '103'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityMentalHealth_PSY_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_PSY_Id"] != null)
                {
                  DetailsView_Isidima_Form7.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionPSYId"] = "";
                  string SQLStringSectionPSY = "SELECT Isidima_Section_PSY_Id FROM InfoQuest_Form_Isidima_Section_PSY WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionPSY = new SqlCommand(SQLStringSectionPSY))
                  {
                    SqlCommand_SectionPSY.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionPSY;
                    using (DataTable_SectionPSY = new DataTable())
                    {
                      DataTable_SectionPSY.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionPSY = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionPSY).Copy();
                      if (DataTable_SectionPSY.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionPSY.Rows)
                        {
                          Session["IsidimaSectionPSYId"] = DataRow_Row["Isidima_Section_PSY_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionPSYId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionPSYId"].ToString()))
                  {
                    DetailsView_Isidima_Form7.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=7&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_PSY_Id=" + Session["IsidimaSectionPSYId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionPSYId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityMentalHealth_PSY_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form7.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm7.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form7Visible()
    {
      int PSY_TotalQuestions = 26;
      HiddenField HiddenField_PSY_TotalQuestions = (HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_TotalQuestions");
      HiddenField_PSY_TotalQuestions.Value = PSY_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form7.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form7.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3010);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form7.FindControl("Label_PSY_C0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form7.FindControl("RadioButtonList_" + ViewMode + "PSY_C0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("C0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form7.FindControl("Label_PSY_C" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form7.FindControl("RadioButtonList_" + ViewMode + "PSY_C" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("C" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= PSY_TotalQuestions; a++)
      //{
      //  Label Label_PSY_C;
      //  HiddenField HiddenField_PSY_CY;
      //  HiddenField HiddenField_PSY_CN;

      //  if (a < 10)
      //  {
      //    Label_PSY_C = (Label)DetailsView_Isidima_Form7.FindControl("Label_PSY_C0" + a + "");
      //    HiddenField_PSY_CY = (HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C0" + a + "Yes");
      //    HiddenField_PSY_CN = (HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form7.FindControl("RadioButtonList_" + ViewMode + "PSY_C0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_PSY_C = (Label)DetailsView_Isidima_Form7.FindControl("Label_PSY_C" + a + "");
      //    HiddenField_PSY_CY = (HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C" + a + "Yes");
      //    HiddenField_PSY_CN = (HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form7.FindControl("RadioButtonList_" + ViewMode + "PSY_C" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_PSY_CY.Value = "0"; HiddenField_PSY_CN.Value = "1"; Label_PSY_C.Text = Convert.ToString("C01. Orientated to self", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_PSY_CY.Value = "0"; HiddenField_PSY_CN.Value = "1"; Label_PSY_C.Text = Convert.ToString("C02. Orientated to date, place, time", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_PSY_CY.Value = "0"; HiddenField_PSY_CN.Value = "1"; Label_PSY_C.Text = Convert.ToString("C03. Remembers personal details", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_PSY_CY.Value = "0"; HiddenField_PSY_CN.Value = "1"; Label_PSY_C.Text = Convert.ToString("C04. Handles stress appropriately", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_PSY_CY.Value = "0"; HiddenField_PSY_CN.Value = "1"; Label_PSY_C.Text = Convert.ToString("C05. Handles anxiety appropriately", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C06. Depressed mood", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C07. Elevated mood", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C08. Obsessive behaviors", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C09. Hallucinations", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C10. Delusions", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C11. Illogical communication", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C12. Paranoia", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C13. Epilepsy", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C14. Memory loss", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C15. Dementia", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_PSY_CY.Value = "1"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C16. Bizarre behaviors", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_PSY_CY.Value = "0"; HiddenField_PSY_CN.Value = "1"; Label_PSY_C.Text = Convert.ToString("C17. Identifies onset of symptoms", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_PSY_CY.Value = "0"; HiddenField_PSY_CN.Value = "1"; Label_PSY_C.Text = Convert.ToString("C18. Compliant on medication", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_PSY_CY.Value = "0"; HiddenField_PSY_CN.Value = "2"; Label_PSY_C.Text = Convert.ToString("C19. Insight into condition", CultureInfo.CurrentCulture); break;
      //    case 20: HiddenField_PSY_CY.Value = "2"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C20. Behavioral problems", CultureInfo.CurrentCulture); break;
      //    case 21: HiddenField_PSY_CY.Value = "2"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C21. Co-morbid substance abuse disorder", CultureInfo.CurrentCulture); break;
      //    case 22: HiddenField_PSY_CY.Value = "2"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C22. Co morbid HIV / Aids", CultureInfo.CurrentCulture); break;
      //    case 23: HiddenField_PSY_CY.Value = "2"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C23. Absconds", CultureInfo.CurrentCulture); break;
      //    case 24: HiddenField_PSY_CY.Value = "2"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C24. Needs continuous supervision", CultureInfo.CurrentCulture); break;
      //    case 25: HiddenField_PSY_CY.Value = "2"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C25. History of suicide attempts", CultureInfo.CurrentCulture); break;
      //    case 26: HiddenField_PSY_CY.Value = "2"; HiddenField_PSY_CN.Value = "0"; Label_PSY_C.Text = Convert.ToString("C26. Sexually deviant behaviors / thinking", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm8Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = true; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3011");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm8.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityPersonalityTraits_T_Update = DataTable_FormMode.Select("SecurityRole_Id = '104'");
              DataRow[] SecurityFacilityPersonalityTraits_T_View = DataTable_FormMode.Select("SecurityRole_Id = '105'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityPersonalityTraits_T_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_T_Id"] != null)
                {
                  DetailsView_Isidima_Form8.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionTId"] = "";
                  string SQLStringSectionT = "SELECT Isidima_Section_T_Id FROM InfoQuest_Form_Isidima_Section_T WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionT = new SqlCommand(SQLStringSectionT))
                  {
                    SqlCommand_SectionT.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionT;
                    using (DataTable_SectionT = new DataTable())
                    {
                      DataTable_SectionT.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionT = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionT).Copy();
                      if (DataTable_SectionT.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionT.Rows)
                        {
                          Session["IsidimaSectionTId"] = DataRow_Row["Isidima_Section_T_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionTId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionTId"].ToString()))
                  {
                    DetailsView_Isidima_Form8.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=8&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_T_Id=" + Session["IsidimaSectionTId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionTId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityPersonalityTraits_T_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form8.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm8.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form8Visible()
    {
      int T_TotalQuestions = 23;
      HiddenField HiddenField_T_TotalQuestions = (HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_TotalQuestions");
      HiddenField_T_TotalQuestions.Value = T_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form8.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form8.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3011);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form8.FindControl("Label_T_T0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form8.FindControl("RadioButtonList_" + ViewMode + "T_T0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("T0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form8.FindControl("Label_T_T" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form8.FindControl("RadioButtonList_" + ViewMode + "T_T" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("T" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= T_TotalQuestions; a++)
      //{
      //  Label Label_T_T;
      //  HiddenField HiddenField_T_TY;
      //  HiddenField HiddenField_T_TN;

      //  if (a < 10)
      //  {
      //    Label_T_T = (Label)DetailsView_Isidima_Form8.FindControl("Label_T_T0" + a + "");
      //    HiddenField_T_TY = (HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T0" + a + "Yes");
      //    HiddenField_T_TN = (HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form8.FindControl("RadioButtonList_" + ViewMode + "T_T0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_T_T = (Label)DetailsView_Isidima_Form8.FindControl("Label_T_T" + a + "");
      //    HiddenField_T_TY = (HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T" + a + "Yes");
      //    HiddenField_T_TN = (HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form8.FindControl("RadioButtonList_" + ViewMode + "T_T" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T01. Harms self to get attention", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T02. Harms others to get attention", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T03. Damages property to get attention", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T04. Cuts or burns self", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_T_TY.Value = "0"; HiddenField_T_TN.Value = "1"; Label_T_T.Text = Convert.ToString("T05. Is charming and personable", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T06. Uses others to get what he / she wants", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T07. Is sadistic", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T08. Likes to be the centre of attention", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T09. Is manipulative", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T10. Cries to get attention", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T11. Is self defeating", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T12. Is clingy and hangs on to people", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T13. Has an odd way of thinking", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T14. Worries all the time", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T15. Thinks about the same things over and over", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T16. Bizarre thinking / behaviors", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T17. Becomes over-involved in relationships", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T18. Is always sad", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_T_TY.Value = "1"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T19. Enjoys hurting others - verbally or physically", CultureInfo.CurrentCulture); break;
      //    case 20: HiddenField_T_TY.Value = "3"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T20. Is aggressive", CultureInfo.CurrentCulture); break;
      //    case 21: HiddenField_T_TY.Value = "2"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T21. Deviant sexual behaviors or thinking", CultureInfo.CurrentCulture); break;
      //    case 22: HiddenField_T_TY.Value = "3"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T22. Suicidal / Para suicidal behavior", CultureInfo.CurrentCulture); break;
      //    case 23: HiddenField_T_TY.Value = "2"; HiddenField_T_TN.Value = "0"; Label_T_T.Text = Convert.ToString("T23. Substance abuse", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm9Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = true; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3012");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm9.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityPhysical_B_Update = DataTable_FormMode.Select("SecurityRole_Id = '106'");
              DataRow[] SecurityFacilityPhysical_B_View = DataTable_FormMode.Select("SecurityRole_Id = '107'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityPhysical_B_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_B_Id"] != null)
                {
                  DetailsView_Isidima_Form9.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionBId"] = "";
                  string SQLStringSectionB = "SELECT Isidima_Section_B_Id FROM InfoQuest_Form_Isidima_Section_B WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionB = new SqlCommand(SQLStringSectionB))
                  {
                    SqlCommand_SectionB.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionB;
                    using (DataTable_SectionB = new DataTable())
                    {
                      DataTable_SectionB.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionB = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionB).Copy();
                      if (DataTable_SectionB.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionB.Rows)
                        {
                          Session["IsidimaSectionBId"] = DataRow_Row["Isidima_Section_B_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionBId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionBId"].ToString()))
                  {
                    DetailsView_Isidima_Form9.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=9&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_B_Id=" + Session["IsidimaSectionBId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionBId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityPhysical_B_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form9.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm9.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form9Visible()
    {
      int B_TotalQuestions = 28;
      HiddenField HiddenField_B_TotalQuestions = (HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_TotalQuestions");
      HiddenField_B_TotalQuestions.Value = B_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form9.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form9.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3012);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form9.FindControl("Label_B_B0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form9.FindControl("RadioButtonList_" + ViewMode + "B_B0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("B0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form9.FindControl("Label_B_B" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form9.FindControl("RadioButtonList_" + ViewMode + "B_B" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("B" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= B_TotalQuestions; a++)
      //{
      //  Label Label_B_B;
      //  HiddenField HiddenField_B_BY;
      //  HiddenField HiddenField_B_BN;

      //  if (a < 10)
      //  {
      //    Label_B_B = (Label)DetailsView_Isidima_Form9.FindControl("Label_B_B0" + a + "");
      //    HiddenField_B_BY = (HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B0" + a + "Yes");
      //    HiddenField_B_BN = (HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form9.FindControl("RadioButtonList_" + ViewMode + "B_B0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_B_B = (Label)DetailsView_Isidima_Form9.FindControl("Label_B_B" + a + "");
      //    HiddenField_B_BY = (HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B" + a + "Yes");
      //    HiddenField_B_BN = (HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form9.FindControl("RadioButtonList_" + ViewMode + "B_B" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_B_BY.Value = "0"; HiddenField_B_BN.Value = "3"; Label_B_B.Text = Convert.ToString("B01. A full examination  has been done of the user", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_B_BY.Value = "3"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B02. Has signs of recent physical trauma", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B03. Has signs of past physical trauma", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_B_BY.Value = "2"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B04. Has bedsores", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B05. Has a skin condition", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B06. Has poor personal hygiene", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B07. Has parasites - lice, worms, scabies", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B08. Has gynae / urogenital issues", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B09. Weight is outside normal range", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_B_BY.Value = "3"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B10. EPSEs present. Stiffness shakes etc.", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B11. Irregular bowel movements", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_B_BY.Value = "0"; HiddenField_B_BN.Value = "1"; Label_B_B.Text = Convert.ToString("B12. Appetite present", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B13. Sleeping problems", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_B_BY.Value = "0"; HiddenField_B_BN.Value = "3"; Label_B_B.Text = Convert.ToString("B14. Blood levels recent and up to date", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B15. Visual pathology present", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B16. Hypertension", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B17. Coughs", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B18. Cardiac pathology", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B19. Excessive drowsiness", CultureInfo.CurrentCulture); break;
      //    case 20: HiddenField_B_BY.Value = "1"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B20. Chest pathology", CultureInfo.CurrentCulture); break;
      //    case 21: HiddenField_B_BY.Value = "3"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B21. Signs of long term substance abuse", CultureInfo.CurrentCulture); break;
      //    case 22: HiddenField_B_BY.Value = "3"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B22. TB", CultureInfo.CurrentCulture); break;
      //    case 23: HiddenField_B_BY.Value = "3"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B23. Diabetes", CultureInfo.CurrentCulture); break;
      //    case 24: HiddenField_B_BY.Value = "3"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B24. HIV Aids", CultureInfo.CurrentCulture); break;
      //    case 25: HiddenField_B_BY.Value = "3"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B25. Taking ARVs", CultureInfo.CurrentCulture); break;
      //    case 26: HiddenField_B_BY.Value = "2"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B26. Is terminal", CultureInfo.CurrentCulture); break;
      //    case 27: HiddenField_B_BY.Value = "3"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B27. Is on motivated medication", CultureInfo.CurrentCulture); break;
      //    case 28: HiddenField_B_BY.Value = "2"; HiddenField_B_BN.Value = "0"; Label_B_B.Text = Convert.ToString("B28. On chronic medication", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm10Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = true; TableForm11.Visible = false; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3013");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm10.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityRecreational_R_Update = DataTable_FormMode.Select("SecurityRole_Id = '108'");
              DataRow[] SecurityFacilityRecreational_R_View = DataTable_FormMode.Select("SecurityRole_Id = '109'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityRecreational_R_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_R_Id"] != null)
                {
                  DetailsView_Isidima_Form10.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionRId"] = "";
                  string SQLStringSectionR = "SELECT Isidima_Section_R_Id FROM InfoQuest_Form_Isidima_Section_R WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionR = new SqlCommand(SQLStringSectionR))
                  {
                    SqlCommand_SectionR.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionR;
                    using (DataTable_SectionR = new DataTable())
                    {
                      DataTable_SectionR.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionR = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionR).Copy();
                      if (DataTable_SectionR.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionR.Rows)
                        {
                          Session["IsidimaSectionRId"] = DataRow_Row["Isidima_Section_R_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionRId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionRId"].ToString()))
                  {
                    DetailsView_Isidima_Form10.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=10&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_R_Id=" + Session["IsidimaSectionRId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionRId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityRecreational_R_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form10.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm10.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form10Visible()
    {
      int R_TotalQuestions = 18;
      HiddenField HiddenField_R_TotalQuestions = (HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_TotalQuestions");
      HiddenField_R_TotalQuestions.Value = R_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form10.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form10.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3013);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form10.FindControl("Label_R_R0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form10.FindControl("RadioButtonList_" + ViewMode + "R_R0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("R0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form10.FindControl("Label_R_R" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form10.FindControl("RadioButtonList_" + ViewMode + "R_R" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("R" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= R_TotalQuestions; a++)
      //{
      //  Label Label_R_R;
      //  HiddenField HiddenField_R_RY;
      //  HiddenField HiddenField_R_RN;

      //  if (a < 10)
      //  {
      //    Label_R_R = (Label)DetailsView_Isidima_Form10.FindControl("Label_R_R0" + a + "");
      //    HiddenField_R_RY = (HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R0" + a + "Yes");
      //    HiddenField_R_RN = (HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form10.FindControl("RadioButtonList_" + ViewMode + "R_R0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_R_R = (Label)DetailsView_Isidima_Form10.FindControl("Label_R_R" + a + "");
      //    HiddenField_R_RY = (HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R" + a + "Yes");
      //    HiddenField_R_RN = (HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form10.FindControl("RadioButtonList_" + ViewMode + "R_R" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R01. Seeks solitary activities", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_R_RY.Value = "0"; HiddenField_R_RN.Value = "1"; Label_R_R.Text = Convert.ToString("R02. Reads", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_R_RY.Value = "0"; HiddenField_R_RN.Value = "1"; Label_R_R.Text = Convert.ToString("R03. Watches TV", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R04. Walks around the facility continually", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_R_RY.Value = "0"; HiddenField_R_RN.Value = "1"; Label_R_R.Text = Convert.ToString("R05. Plays board games when prompted", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R06. Needs to be involved or just sits", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R07. Sleeps when there are no organized activities", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R08. Seeks recreational equipment", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R09. Trains for events regularly", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R10. Initiates groups activities", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R11. Regularly attends the library", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R12. Requests activities and entertainment", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_R_RY.Value = "1"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R13. Participates in sporting events outside of the facility", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_R_RY.Value = "2"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R14. Seeks criminal activity when bored", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_R_RY.Value = "2"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R15. Encourages others to break rules", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_R_RY.Value = "3"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R16. Bullies others for cigarettes / sexual favors", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_R_RY.Value = "2"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R17. Abuses substances", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_R_RY.Value = "2"; HiddenField_R_RN.Value = "0"; Label_R_R.Text = Convert.ToString("R18. Absconds", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm11Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = true; TableForm12.Visible = false;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3014");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm11.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilitySocial_S_Update = DataTable_FormMode.Select("SecurityRole_Id = '110'");
              DataRow[] SecurityFacilitySocial_S_View = DataTable_FormMode.Select("SecurityRole_Id = '111'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilitySocial_S_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_S_Id"] != null)
                {
                  DetailsView_Isidima_Form11.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionSId"] = "";
                  string SQLStringSectionS = "SELECT Isidima_Section_S_Id FROM InfoQuest_Form_Isidima_Section_S WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionS = new SqlCommand(SQLStringSectionS))
                  {
                    SqlCommand_SectionS.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionS;
                    using (DataTable_SectionS = new DataTable())
                    {
                      DataTable_SectionS.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionS = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionS).Copy();
                      if (DataTable_SectionS.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionS.Rows)
                        {
                          Session["IsidimaSectionSId"] = DataRow_Row["Isidima_Section_S_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionSId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionSId"].ToString()))
                  {
                    DetailsView_Isidima_Form11.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=11&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_S_Id=" + Session["IsidimaSectionSId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionSId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilitySocial_S_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form11.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm11.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form11Visible()
    {
      int S_TotalQuestions = 25;
      HiddenField HiddenField_S_TotalQuestions = (HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_TotalQuestions");
      HiddenField_S_TotalQuestions.Value = S_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form11.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form11.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3014);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form11.FindControl("Label_S_S0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form11.FindControl("RadioButtonList_" + ViewMode + "S_S0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("S0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form11.FindControl("Label_S_S" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form11.FindControl("RadioButtonList_" + ViewMode + "S_S" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("S" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= S_TotalQuestions; a++)
      //{
      //  Label Label_S_S;
      //  HiddenField HiddenField_S_SY;
      //  HiddenField HiddenField_S_SN;

      //  if (a < 10)
      //  {
      //    Label_S_S = (Label)DetailsView_Isidima_Form11.FindControl("Label_S_S0" + a + "");
      //    HiddenField_S_SY = (HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S0" + a + "Yes");
      //    HiddenField_S_SN = (HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form11.FindControl("RadioButtonList_" + ViewMode + "S_S0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_S_S = (Label)DetailsView_Isidima_Form11.FindControl("Label_S_S" + a + "");
      //    HiddenField_S_SY = (HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S" + a + "Yes");
      //    HiddenField_S_SN = (HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form11.FindControl("RadioButtonList_" + ViewMode + "S_S" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S01. Communicates verbally", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S02. Speaks when spoken to", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S03. Has social conversations unaided", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S04. Asks for help when necessary", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S05. Identifies others", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S06. Can call others", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S07. Can identify own belongings", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S08. Acts age, role & gender appropriately", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S09. Works as part of a group", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S10. Follows instructions", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S11. Reacts positively towards family members", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S12. Interacts with others voluntarily", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S13. Initiates group activities with others", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S14. Shares with others", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S15. Asks before taking others belongings", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S16. Displays empathy for others", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S17. Can use public transport", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_S_SY.Value = "0"; HiddenField_S_SN.Value = "1"; Label_S_S.Text = Convert.ToString("S18. Assists others with tasks", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_S_SY.Value = "2"; HiddenField_S_SN.Value = "0"; Label_S_S.Text = Convert.ToString("S19. Is destructive when frustrated", CultureInfo.CurrentCulture); break;
      //    case 20: HiddenField_S_SY.Value = "2"; HiddenField_S_SN.Value = "0"; Label_S_S.Text = Convert.ToString("S20. Steals", CultureInfo.CurrentCulture); break;
      //    case 21: HiddenField_S_SY.Value = "2"; HiddenField_S_SN.Value = "0"; Label_S_S.Text = Convert.ToString("S21. Throws temper tantrums", CultureInfo.CurrentCulture); break;
      //    case 22: HiddenField_S_SY.Value = "2"; HiddenField_S_SN.Value = "0"; Label_S_S.Text = Convert.ToString("S22. Is demanding and threatening when ignored", CultureInfo.CurrentCulture); break;
      //    case 23: HiddenField_S_SY.Value = "2"; HiddenField_S_SN.Value = "0"; Label_S_S.Text = Convert.ToString("S23. Bullies others for cigarettes / sexual favors", CultureInfo.CurrentCulture); break;
      //    case 24: HiddenField_S_SY.Value = "2"; HiddenField_S_SN.Value = "0"; Label_S_S.Text = Convert.ToString("S24. Encourages others to break rules", CultureInfo.CurrentCulture); break;
      //    case 25: HiddenField_S_SY.Value = "2"; HiddenField_S_SN.Value = "0"; Label_S_S.Text = Convert.ToString("S25. Feels threatened by others", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    private void SetForm12Visibility()
    {
      TablePatientInfo.Visible = true; TableListLinks.Visible = false; TableList.Visible = true;
      TableForm0View.Visible = true; TableForm0.Visible = false; TableForm1.Visible = false; TableForm2.Visible = false; TableForm3.Visible = false; TableForm4.Visible = false; TableForm5.Visible = false; TableForm6.Visible = false; TableForm7.Visible = false; TableForm8.Visible = false; TableForm9.Visible = false; TableForm10.Visible = false; TableForm11.Visible = false; TableForm12.Visible = true;

      Session["IsidimaCategoryId"] = SetFormVisibility_CategoryId("3015");

      if (string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
      {
        TableForm0View.Visible = false;
        TableForm12.Visible = false;
      }
      else
      {
        string SQLStringFormMode = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_Username = @LOGON_USER) AND (SecurityRole_Id = '1' OR Form_Id IN ('27')) AND (Facility_Id IN (@s_Facility_Id) OR SecurityRole_Rank = 1)";
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
              DataRow[] SecurityFormAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '86'");
              DataRow[] SecurityFormAdminView = DataTable_FormMode.Select("SecurityRole_Id = '87'");
              DataRow[] SecurityFacilityAdminUpdate = DataTable_FormMode.Select("SecurityRole_Id = '88'");
              DataRow[] SecurityFacilityAdminView = DataTable_FormMode.Select("SecurityRole_Id = '89'");
              DataRow[] SecurityFacilityVocational_V_Update = DataTable_FormMode.Select("SecurityRole_Id = '112'");
              DataRow[] SecurityFacilityVocational_V_View = DataTable_FormMode.Select("SecurityRole_Id = '113'");

              Session["Security"] = "1";
              if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0 || SecurityFacilityVocational_V_Update.Length > 0))
              {
                Session["Security"] = "0";
                if (Request.QueryString["Isidima_Section_V_Id"] != null)
                {
                  DetailsView_Isidima_Form12.ChangeMode(DetailsViewMode.Edit);
                }
                else
                {
                  Session["IsidimaSectionVId"] = "";
                  string SQLStringSectionV = "SELECT Isidima_Section_V_Id FROM InfoQuest_Form_Isidima_Section_V WHERE Isidima_Category_Id = @Isidima_Category_Id";
                  using (SqlCommand SqlCommand_SectionV = new SqlCommand(SQLStringSectionV))
                  {
                    SqlCommand_SectionV.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
                    DataTable DataTable_SectionV;
                    using (DataTable_SectionV = new DataTable())
                    {
                      DataTable_SectionV.Locale = CultureInfo.CurrentCulture;
                      DataTable_SectionV = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionV).Copy();
                      if (DataTable_SectionV.Rows.Count > 0)
                      {
                        foreach (DataRow DataRow_Row in DataTable_SectionV.Rows)
                        {
                          Session["IsidimaSectionVId"] = DataRow_Row["Isidima_Section_V_Id"];
                        }
                      }
                      else
                      {
                        Session["IsidimaSectionVId"] = "";
                      }
                    }
                  }

                  if (string.IsNullOrEmpty(Session["IsidimaSectionVId"].ToString()))
                  {
                    DetailsView_Isidima_Form12.ChangeMode(DetailsViewMode.Insert);
                  }
                  else
                  {
                    Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=12&Isidima_Category_Id=" + Request.QueryString["Isidima_Category_Id"] + "&Isidima_Section_V_Id=" + Session["IsidimaSectionVId"].ToString() + "", false);
                  }
                  Session["IsidimaSectionVId"] = "";
                }
              }

              if (Session["Security"].ToString() == "1" && (SecurityFormAdminView.Length > 0 || SecurityFacilityAdminView.Length > 0 || SecurityFacilityVocational_V_View.Length > 0))
              {
                Session["Security"] = "0";
                DetailsView_Isidima_Form12.ChangeMode(DetailsViewMode.ReadOnly);
              }

              if (Session["Security"].ToString() == "1")
              {
                Session["Security"] = "0";
                TableForm0View.Visible = false;
                TableForm12.Visible = false;
              }
              Session["Security"] = "1";
            }
          }
        }
      }
      Session["IsidimaCategoryId"] = "";
    }
    private void Form12Visible()
    {
      int V_TotalQuestions = 21;
      HiddenField HiddenField_V_TotalQuestions = (HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_TotalQuestions");
      HiddenField_V_TotalQuestions.Value = V_TotalQuestions.ToString(CultureInfo.CurrentCulture);

      string ViewMode = "";
      if (DetailsView_Isidima_Form12.CurrentMode == DetailsViewMode.Insert)
      {
        ViewMode = "Insert";
      }

      if (DetailsView_Isidima_Form12.CurrentMode == DetailsViewMode.Edit)
      {
        ViewMode = "Edit";
      }

      //--------------------------------------------------
      string SQLStringQuestions = "SELECT Isidima_Rules_QuestionId , Isidima_Rules_Question_YesWeight , Isidima_Rules_Question_NoWeight , Isidima_Rules_Question FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      using (SqlCommand SqlCommand_Questions = new SqlCommand(SQLStringQuestions))
      {
        SqlCommand_Questions.Parameters.AddWithValue("@Isidima_Rules_Section_List", 3015);
        DataTable DataTable_Questions;
        using (DataTable_Questions = new DataTable())
        {
          DataTable_Questions.Locale = CultureInfo.CurrentCulture;
          DataTable_Questions = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Questions).Copy();
          if (DataTable_Questions.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row1 in DataTable_Questions.Rows)
            {
              Int32 QuestionId = Convert.ToInt32(DataRow_Row1["Isidima_Rules_QuestionId"], CultureInfo.CurrentCulture);
              string ValueYes = DataRow_Row1["Isidima_Rules_Question_YesWeight"].ToString();
              string ValueNo = DataRow_Row1["Isidima_Rules_Question_NoWeight"].ToString();
              string Question = DataRow_Row1["Isidima_Rules_Question"].ToString();

              string LabelQuestionId = "";
              Label Label_Question;
              HiddenField HiddenField_ValueYes;
              HiddenField HiddenField_ValueNo;

              if (QuestionId < 10)
              {
                Label_Question = (Label)DetailsView_Isidima_Form12.FindControl("Label_V_V0" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V0" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V0" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form12.FindControl("RadioButtonList_" + ViewMode + "V_V0" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("V0" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }
              else
              {
                Label_Question = (Label)DetailsView_Isidima_Form12.FindControl("Label_V_V" + QuestionId + "");
                HiddenField_ValueYes = (HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V" + QuestionId + "Yes");
                HiddenField_ValueNo = (HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V" + QuestionId + "No");

                ((RadioButtonList)DetailsView_Isidima_Form12.FindControl("RadioButtonList_" + ViewMode + "V_V" + QuestionId + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");

                LabelQuestionId = Convert.ToString("V" + QuestionId + ". ", CultureInfo.CurrentCulture);
              }

              HiddenField_ValueYes.Value = ValueYes;
              HiddenField_ValueNo.Value = ValueNo;
              Label_Question.Text = LabelQuestionId + Question;

              QuestionId = 0;
              ValueYes = "";
              ValueNo = "";
              Question = "";

              LabelQuestionId = "";
            }
          }
        }
      }
      //--------------------------------------------------

      //for (int a = 1; a <= V_TotalQuestions; a++)
      //{
      //  Label Label_V_V;
      //  HiddenField HiddenField_V_VY;
      //  HiddenField HiddenField_V_VN;

      //  if (a < 10)
      //  {
      //    Label_V_V = (Label)DetailsView_Isidima_Form12.FindControl("Label_V_V0" + a + "");
      //    HiddenField_V_VY = (HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V0" + a + "Yes");
      //    HiddenField_V_VN = (HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V0" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form12.FindControl("RadioButtonList_" + ViewMode + "V_V0" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }
      //  else
      //  {
      //    Label_V_V = (Label)DetailsView_Isidima_Form12.FindControl("Label_V_V" + a + "");
      //    HiddenField_V_VY = (HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V" + a + "Yes");
      //    HiddenField_V_VN = (HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V" + a + "No");

      //    ((RadioButtonList)DetailsView_Isidima_Form12.FindControl("RadioButtonList_" + ViewMode + "V_V" + a + "")).Attributes.Add("OnClick", "Validation_Form();Calculation_Form();");
      //  }

      //  switch (a)
      //  {
      //    case 1: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V01. Can do repetitive tasks", CultureInfo.CurrentCulture); break;
      //    case 2: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V02. Can do novel tasks", CultureInfo.CurrentCulture); break;
      //    case 3: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V03. Can follow instructions", CultureInfo.CurrentCulture); break;
      //    case 4: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V04. Do tasks requiring eye-hand coordination", CultureInfo.CurrentCulture); break;
      //    case 5: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V05. Do administrative tasks", CultureInfo.CurrentCulture); break;
      //    case 6: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V06. Takes out required tools / materials", CultureInfo.CurrentCulture); break;
      //    case 7: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V07. Is punctual", CultureInfo.CurrentCulture); break;
      //    case 8: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V08. Work / output is neat, tidy and of good quality", CultureInfo.CurrentCulture); break;
      //    case 9: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V09. Tidies up after work", CultureInfo.CurrentCulture); break;
      //    case 10: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V10. Was employed previously", CultureInfo.CurrentCulture); break;
      //    case 11: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V11. Has work skills / qualification", CultureInfo.CurrentCulture); break;
      //    case 12: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V12. Has a CV", CultureInfo.CurrentCulture); break;
      //    case 13: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V13. Can follow a planned work day", CultureInfo.CurrentCulture); break;
      //    case 14: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V14. Can use specialized equipment", CultureInfo.CurrentCulture); break;
      //    case 15: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "1"; Label_V_V.Text = Convert.ToString("V15. Can use own or public transport", CultureInfo.CurrentCulture); break;
      //    case 16: HiddenField_V_VY.Value = "3"; HiddenField_V_VN.Value = "0"; Label_V_V.Text = Convert.ToString("V16. Psychotic", CultureInfo.CurrentCulture); break;
      //    case 17: HiddenField_V_VY.Value = "0"; HiddenField_V_VN.Value = "2"; Label_V_V.Text = Convert.ToString("V17. Compliant on medication", CultureInfo.CurrentCulture); break;
      //    case 18: HiddenField_V_VY.Value = "3"; HiddenField_V_VN.Value = "0"; Label_V_V.Text = Convert.ToString("V18. Aggressive", CultureInfo.CurrentCulture); break;
      //    case 19: HiddenField_V_VY.Value = "2"; HiddenField_V_VN.Value = "0"; Label_V_V.Text = Convert.ToString("V19. Has limited social skills", CultureInfo.CurrentCulture); break;
      //    case 20: HiddenField_V_VY.Value = "2"; HiddenField_V_VN.Value = "0"; Label_V_V.Text = Convert.ToString("V20. Has limited concentration", CultureInfo.CurrentCulture); break;
      //    case 21: HiddenField_V_VY.Value = "3"; HiddenField_V_VN.Value = "0"; Label_V_V.Text = Convert.ToString("V21. Steals", CultureInfo.CurrentCulture); break;
      //  }
      //}
    }

    protected string SetFormVisibility_CategoryId(string listItem_Id)
    {
      string IsidimaCategoryId = "";
      string SQLStringSectionAllowed = "SELECT Isidima_Category_Id FROM Administration_ListItem AS A , Administration_ListItem AS B , Administration_ListItem AS C , vForm_Isidima_Category WHERE B.ListItem_Parent = A.ListItem_Id AND B.ListItem_Name = C.ListItem_Id AND vForm_Isidima_Category.Isidima_Category_PatientCategory_List = A.ListItem_Id AND A.ListCategory_Id = 64 AND B.ListCategory_Id = 65 AND C.ListCategory_Id = 66 AND vForm_Isidima_Category.Isidima_Category_Id = @Isidima_Category_Id AND C.ListItem_Id = @ListItem_Id";
      using (SqlCommand SqlCommand_SectionAllowed = new SqlCommand(SQLStringSectionAllowed))
      {
        SqlCommand_SectionAllowed.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
        SqlCommand_SectionAllowed.Parameters.AddWithValue("@ListItem_Id", listItem_Id);
        DataTable DataTable_SectionAllowed;
        using (DataTable_SectionAllowed = new DataTable())
        {
          DataTable_SectionAllowed.Locale = CultureInfo.CurrentCulture;
          DataTable_SectionAllowed = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionAllowed).Copy();
          if (DataTable_SectionAllowed.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SectionAllowed.Rows)
            {
              IsidimaCategoryId = DataRow_Row["Isidima_Category_Id"].ToString();
            }
          }
        }
      }

      return IsidimaCategoryId;
    }

    private void TablePatientInfoVisible()
    {
      Session["FacilityFacilityDisplayName"] = "";
      Session["IsidimaPIPatientVisitNumber"] = "";
      Session["IsidimaPIPatientName"] = "";
      Session["IsidimaPIPatientAge"] = "";
      Session["IsidimaPIPatientDateOfAdmission"] = "";
      Session["IsidimaPIPatientDateofDischarge"] = "";
      Session["IsidimaPIPatientWard"] = "";

      string SQLStringPatientInfo = "SELECT DISTINCT Facility_FacilityDisplayName , Isidima_PI_PatientVisitNumber , Isidima_PI_PatientName , Isidima_PI_PatientAge , Isidima_PI_PatientDateOfAdmission , Isidima_PI_PatientDateofDischarge , Isidima_PI_PatientWard FROM vForm_Isidima_PatientInformation WHERE Facility_Id = @s_Facility_Id AND Isidima_PI_PatientVisitNumber = @s_Isidima_PatientVisitNumber";
      using (SqlCommand SqlCommand_PatientInfo = new SqlCommand(SQLStringPatientInfo))
      {
        SqlCommand_PatientInfo.Parameters.AddWithValue("@s_Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PatientInfo.Parameters.AddWithValue("@s_Isidima_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
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
              Session["IsidimaPIPatientVisitNumber"] = DataRow_Row["Isidima_PI_PatientVisitNumber"];
              Session["IsidimaPIPatientName"] = DataRow_Row["Isidima_PI_PatientName"];
              Session["IsidimaPIPatientAge"] = DataRow_Row["Isidima_PI_PatientAge"];
              Session["IsidimaPIPatientDateOfAdmission"] = DataRow_Row["Isidima_PI_PatientDateOfAdmission"];
              Session["IsidimaPIPatientDateofDischarge"] = DataRow_Row["Isidima_PI_PatientDateofDischarge"];
              Session["IsidimaPIPatientWard"] = DataRow_Row["Isidima_PI_PatientWard"];
            }
          }
        }
      }

      Label_PIFacility.Text = Session["FacilityFacilityDisplayName"].ToString();
      Label_PIVisitNumber.Text = Session["IsidimaPIPatientVisitNumber"].ToString();
      Label_PIName.Text = Session["IsidimaPIPatientName"].ToString();
      Label_PIAge.Text = Session["IsidimaPIPatientAge"].ToString();
      Label_PIDateAdmission.Text = Session["IsidimaPIPatientDateOfAdmission"].ToString();
      Label_PIDateDischarge.Text = Session["IsidimaPIPatientDateofDischarge"].ToString();
      Label_PIWard.Text = Session["IsidimaPIPatientWard"].ToString();

      Session["FacilityFacilityDisplayName"] = "";
      Session["IsidimaPIPatientVisitNumber"] = "";
      Session["IsidimaPIPatientName"] = "";
      Session["IsidimaPIPatientAge"] = "";
      Session["IsidimaPIPatientDateOfAdmission"] = "";
      Session["IsidimaPIPatientDateofDischarge"] = "";
      Session["IsidimaPIPatientWard"] = "";
    }

    private void TableListLinksVisible()
    {
      Button_CaptureNew.Visible = false;
      Button_Print.Visible = false;
      Button_Email.Visible = false;
      Button_Admin.Visible = false;

      string Email = "";
      string Print = "";
      string Admin = "";
      string SQLStringEmail = "SELECT Form_Email , Form_Print , Form_Admin FROM Administration_Form WHERE Form_Id = 27";
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
              Admin = DataRow_Row["Form_Admin"].ToString();
            }
          }
        }
      }

      string SQLStringNoRecords = "SELECT DISTINCT SecurityRole_Id , Facility_Id FROM vAdministration_SecurityAccess_Active WHERE Form_Id IN ('-1','27') AND SecurityUser_Username = @LOGON_USER";
      using (SqlCommand SqlCommand_NoRecords = new SqlCommand(SQLStringNoRecords))
      {
        SqlCommand_NoRecords.Parameters.AddWithValue("@LOGON_USER", Request.ServerVariables["LOGON_USER"]);
        DataTable DataTable_NoRecords;
        using (DataTable_NoRecords = new DataTable())
        {
          DataTable_NoRecords.Locale = CultureInfo.CurrentCulture;
          DataTable_NoRecords = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_NoRecords).Copy();
          if (DataTable_NoRecords.Rows.Count > 0)
          {
            DataRow[] SecurityAdmin = DataTable_NoRecords.Select("SecurityRole_Id = '1'");
            DataRow[] SecurityFormAdminUpdate = DataTable_NoRecords.Select("SecurityRole_Id = '86'");
            DataRow[] SecurityFacilityAdminUpdate = DataTable_NoRecords.Select("SecurityRole_Id = '88'");

            Session["Security"] = "1";
            if (Session["Security"].ToString() == "1" && (SecurityAdmin.Length > 0 || SecurityFormAdminUpdate.Length > 0 || SecurityFacilityAdminUpdate.Length > 0))
            {
              Session["Security"] = "0";
              Button_CaptureNew.Visible = true;

              if (Admin == "False")
              {
                Button_Admin.Visible = false;
              }
              else
              {
                Button_Admin.Visible = true;
                //Button_Admin.OnClientClick = "FormAdmin('Form_Isidima_Admin.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "')";
              }

              Admin = "";
            }
            Session["Security"] = "1";
          }
        }
      }

      Session["PrintEmail"] = "";
      string SQLStringPrintEmail = "SELECT TOP 1 Isidima_Category_Id FROM InfoQuest_Form_Isidima_Category WHERE Facility_Id = @Facility_Id AND Isidima_Category_PatientVisitNumber = @Isidima_Category_PatientVisitNumber";
      using (SqlCommand SqlCommand_PrintEmail = new SqlCommand(SQLStringPrintEmail))
      {
        SqlCommand_PrintEmail.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
        SqlCommand_PrintEmail.Parameters.AddWithValue("@Isidima_Category_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
        DataTable DataTable_PrintEmail;
        using (DataTable_PrintEmail = new DataTable())
        {
          DataTable_PrintEmail.Locale = CultureInfo.CurrentCulture;
          DataTable_PrintEmail = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_PrintEmail).Copy();
          if (DataTable_PrintEmail.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_PrintEmail.Rows)
            {
              Session["PrintEmail"] = DataRow_Row["Isidima_Category_Id"];
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(Session["PrintEmail"].ToString()))
      {
        if (Email == "False")
        {
          Button_Email.Visible = false;
        }
        else
        {
          Button_Email.Visible = true;
          Button_Email.OnClientClick = "FormEmail('InfoQuest_Email.aspx?EmailPage=Form_Isidima&EmailValue=" + Session["PrintEmail"] + "')";
        }

        if (Print == "False")
        {
          Button_Print.Visible = false;
        }
        else
        {
          Button_Print.Visible = true;
          Button_Print.OnClientClick = "FormPrint('InfoQuest_Print.aspx?PrintPage=Form_Isidima&PrintValue=" + Session["PrintEmail"].ToString() + "')";
        }
      }

      Email = "";
      Print = "";
      Admin = "";
      Session["PrintEmail"] = "";
    }

    private void TableForm0ViewVisible()
    {
      Session["IsidimaCategoryReportNumber"] = "";
      Session["IsidimaCategoryPatientCategoryName"] = "";
      Session["IsidimaCategoryDate"] = "";
      Session["IsidimaCategoryIsActive"] = "";

      string SQLStringFormInfo = "SELECT DISTINCT Isidima_Category_ReportNumber , Isidima_Category_PatientCategory_Name , CONVERT(NVARCHAR(MAX),Isidima_Category_Date,111) AS Isidima_Category_Date , CASE WHEN Isidima_Category_IsActive = 1 THEN 'Yes' WHEN Isidima_Category_IsActive = 0 THEN 'No' END AS Isidima_Category_IsActive FROM vForm_Isidima_Category WHERE Isidima_Category_Id = @Isidima_Category_Id";
      using (SqlCommand SqlCommand_FormInfo = new SqlCommand(SQLStringFormInfo))
      {
        SqlCommand_FormInfo.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
        DataTable DataTable_FormInfo;
        using (DataTable_FormInfo = new DataTable())
        {
          DataTable_FormInfo.Locale = CultureInfo.CurrentCulture;
          DataTable_FormInfo = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormInfo).Copy();
          if (DataTable_FormInfo.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_FormInfo.Rows)
            {
              Session["IsidimaCategoryReportNumber"] = DataRow_Row["Isidima_Category_ReportNumber"];
              Session["IsidimaCategoryPatientCategoryName"] = DataRow_Row["Isidima_Category_PatientCategory_Name"];
              Session["IsidimaCategoryDate"] = DataRow_Row["Isidima_Category_Date"];
              Session["IsidimaCategoryIsActive"] = DataRow_Row["Isidima_Category_IsActive"];
            }
          }
          else
          {
            Session["IsidimaCategoryReportNumber"] = "";
            Session["IsidimaCategoryPatientCategoryName"] = "";
            Session["IsidimaCategoryDate"] = "";
            Session["IsidimaCategoryIsActive"] = "";
          }
        }
      }

      Label_FIReportNumber.Text = Session["IsidimaCategoryReportNumber"].ToString();
      Label_FIPatientCategory.Text = Session["IsidimaCategoryPatientCategoryName"].ToString();
      Label_FIDate.Text = Session["IsidimaCategoryDate"].ToString();
      Label_FIIsActive.Text = Session["IsidimaCategoryIsActive"].ToString();

      Session["IsidimaCategoryReportNumber"] = "";
      Session["IsidimaCategoryPatientCategoryName"] = "";
      Session["IsidimaCategoryDate"] = "";
      Session["IsidimaCategoryIsActive"] = "";
    }

    private void RedirectToNext()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["SearchN_FacilityId"];
      string SearchField2 = Request.QueryString["SearchN_IsidimaPatientVisitNumber"];
      string SearchField3 = Request.QueryString["SearchN_IsidimaPatientWard"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null)
      {
        FinalURL = "Form_Isidima_Next.aspx";
      }
      else
      {
        if (SearchField1 == null)
        {
          SearchField1 = "";
        }
        else
        {
          SearchField1 = "s_Facility_Id=" + Request.QueryString["SearchN_FacilityId"] + "&";
        }

        if (SearchField2 == null)
        {
          SearchField2 = "";
        }
        else
        {
          SearchField2 = "s_Isidima_PatientVisitNumber=" + Request.QueryString["SearchN_IsidimaPatientVisitNumber"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_Isidima_PatientWard=" + Request.QueryString["SearchN_IsidimaPatientWard"] + "&";
        }

        string SearchURL = "Form_Isidima_Next.aspx?" + SearchField1 + SearchField2 + SearchField3;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }

    private void RedirectToList()
    {
      string FinalURL = "";

      string SearchField1 = Request.QueryString["Search_FacilityId"];
      string SearchField2 = Request.QueryString["Search_IsidimaPatientVisitNumber"];
      string SearchField3 = Request.QueryString["Search_IsidimaPatientName"];
      string SearchField4 = Request.QueryString["Search_IsidimaReportNumber"];

      if (SearchField1 == null && SearchField2 == null && SearchField3 == null && SearchField4 == null)
      {
        FinalURL = "Form_Isidima_List.aspx";
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
          SearchField2 = "s_Isidima_PatientVisitNumber=" + Request.QueryString["Search_IsidimaPatientVisitNumber"] + "&";
        }

        if (SearchField3 == null)
        {
          SearchField3 = "";
        }
        else
        {
          SearchField3 = "s_Isidima_PatientName=" + Request.QueryString["Search_IsidimaPatientName"] + "&";
        }

        if (SearchField4 == null)
        {
          SearchField4 = "";
        }
        else
        {
          SearchField4 = "s_Isidima_ReportNumber=" + Request.QueryString["Search_IsidimaReportNumber"] + "&";
        }

        string SearchURL = "Form_Isidima_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
        SearchURL = SearchURL.Remove(SearchURL.Length - 1, 1);

        FinalURL = SearchURL;
      }

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Search--//
    protected void Button_GoToNext_Click(object sender, EventArgs e)
    {
      RedirectToNext();
    }

    protected void Button_GoToList_Click(object sender, EventArgs e)
    {
      RedirectToList();
    }

    protected void Button_Search_Click(object sender, EventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + DropDownList_Facility.SelectedValue.ToString() + "&s_Isidima_PatientVisitNumber=" + Server.HtmlEncode(TextBox_PatientVisitNumber.Text.ToString()) + "", false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = "";
      FinalURL = "Form_Isidima.aspx";
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --TableList--//
    protected void ListView_Isidima_Category_PreRender(object sender, EventArgs e)
    {
      ListView ListView_List = (ListView)sender;
      DataPager DataPager_Isidima_Category = (DataPager)ListView_Isidima_Category.FindControl("DataPager_Isidima_Category");

      if (ListView_List.Items.Count != 0)
      {
        ListView_Isidima_Category.Visible = true;
        DropDownList DropDownList_PageSize = (DropDownList)DataPager_Isidima_Category.Controls[0].FindControl("DropDownList_PageSize");
        if (DataPager_Isidima_Category.PageSize <= 5)
        {
          DropDownList_PageSize.SelectedValue = "5";
        }
        else if (DataPager_Isidima_Category.PageSize > 5 && DataPager_Isidima_Category.PageSize <= 10)
        {
          DropDownList_PageSize.SelectedValue = "10";
        }
        else if (DataPager_Isidima_Category.PageSize > 10 && DataPager_Isidima_Category.PageSize <= 20)
        {
          DropDownList_PageSize.SelectedValue = "20";
        }
      }
    }

    protected void ListView_Isidima_Category_DataBound(object sender, EventArgs e)
    {
      ListView ListView_List = (ListView)sender;
      DataPager DataPager_Isidima_Category = (DataPager)ListView_Isidima_Category.FindControl("DataPager_Isidima_Category");

      if (ListView_List.Items.Count != 0)
      {
        DropDownList DropDownList_PageList = (DropDownList)DataPager_Isidima_Category.Controls[0].FindControl("DropDownList_Page");
        if (ListView_List.Items.Count != 0)
        {
          for (int i = 0; i < (Math.Ceiling((decimal)((decimal)DataPager_Isidima_Category.TotalRowCount / (decimal)DataPager_Isidima_Category.PageSize))); i++)
          {
            int pageNumber = i + 1;
            ListItem ListItem_Item = new ListItem(pageNumber.ToString(CultureInfo.CurrentCulture));
            if (i == (DataPager_Isidima_Category.StartRowIndex / DataPager_Isidima_Category.PageSize))
            {
              ListItem_Item.Selected = true;
            }

            DropDownList_PageList.Items.Add(ListItem_Item);
          }

          Label Label_TotalPages = (Label)DataPager_Isidima_Category.Controls[0].FindControl("Label_TotalPages");
          Label_TotalPages.Text = (Math.Ceiling((decimal)((decimal)DataPager_Isidima_Category.TotalRowCount / (decimal)DataPager_Isidima_Category.PageSize))).ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void PagerCommand(object sender, DataPagerCommandEventArgs e)
    {
      if (e != null)
      {
        switch (e.CommandName)
        {
          case "First":
            e.NewStartRowIndex = 0;
            e.NewMaximumRows = e.Item.Pager.MaximumRows;
            break;
          case "Prev":
            int NewIndexPrev = (e.Item.Pager.StartRowIndex - e.Item.Pager.PageSize);
            if (NewIndexPrev < 0)
            {
              e.NewStartRowIndex = 0;
              e.NewMaximumRows = e.Item.Pager.MaximumRows;
            }
            else
            {
              e.NewStartRowIndex = NewIndexPrev;
              e.NewMaximumRows = e.Item.Pager.MaximumRows;
            }
            break;
          case "Next":
            int NewIndexNext = (e.Item.Pager.StartRowIndex + e.Item.Pager.PageSize);
            if (NewIndexNext <= (e.TotalRowCount - 1))
            {
              e.NewStartRowIndex = NewIndexNext;
              e.NewMaximumRows = e.Item.Pager.MaximumRows;
            }
            break;
          case "Last":
            e.NewStartRowIndex = ((e.TotalRowCount - 1) - ((e.TotalRowCount - 1) % e.Item.Pager.PageSize));
            e.NewMaximumRows = e.Item.Pager.MaximumRows;
            break;
        }
      }
    }

    protected void DropDownList_PageSize_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DataPager DataPager_Isidima_Category = (DataPager)ListView_Isidima_Category.FindControl("DataPager_Isidima_Category");
      DropDownList DropDownList_PageSize = (DropDownList)sender;//DataPager_Isidima_Category.Controls[0].FindControl("DropDownList_PageSize");
      DataPager_Isidima_Category.SetPageProperties(0, int.Parse(DropDownList_PageSize.SelectedValue, CultureInfo.CurrentCulture), true);
    }

    protected void DropDownList_Page_SelectedIndexChanged(Object sender, EventArgs e)
    {
      DataPager DataPager_Isidima_Category = (DataPager)ListView_Isidima_Category.FindControl("DataPager_Isidima_Category");
      DropDownList DropDownList_PageList = (DropDownList)sender; //DataPager_Isidima_Category.Controls[0].FindControl("DropDownList_PageSize");
      DataPager_Isidima_Category.SetPageProperties(((Convert.ToInt32(DropDownList_PageList.SelectedValue, CultureInfo.CurrentCulture) - 1) * DataPager_Isidima_Category.PageSize), DataPager_Isidima_Category.PageSize, true);
    }

    protected void SqlDataSource_Isidima_Category_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    public string GetLinkForm0(object isidima_Category_Id, object viewUpdate)
    {
      string LinkURL = "";
      if (viewUpdate != null)
      {
        if (viewUpdate.ToString() == "Yes")
        {
          LinkURL = "<a href='Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "&Form=0&Isidima_Category_Id=" + isidima_Category_Id + "'>Update</a>";
        }
        else
        {
          LinkURL = "";
        }
      }

      string CurrentURL = "";
      CurrentURL = LinkURL;

      string FinalURL = "";

      FinalURL = CurrentURL;

      return FinalURL;
    }
    //---END--- --TableList--//


    //--START-- --Form0--//
    protected void SqlDataSource_Isidima_Form0_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form0_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Form0PatientCategoryChanged == true)
          {
            string SQLStringUpdateOnsetDate = "" +
              "DELETE FROM InfoQuest_Form_Isidima_Section_MHA WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_VPA WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_J WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_DMH WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_F WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_I WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_PSY WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_T WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_B WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_R WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_S WHERE Isidima_Category_Id = @Isidima_Category_Id " +
              "DELETE FROM InfoQuest_Form_Isidima_Section_V WHERE Isidima_Category_Id = @Isidima_Category_Id";
            using (SqlCommand SqlCommand_UpdateOnsetDate = new SqlCommand(SQLStringUpdateOnsetDate))
            {
              SqlCommand_UpdateOnsetDate.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
              InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlExecute(SqlCommand_UpdateOnsetDate);
            }
          }

          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form0_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form0_DataBound(object sender, EventArgs e)
    {
      if (DetailsView_Isidima_Form0.CurrentMode == DetailsViewMode.Insert)
      {
        //((TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_InsertDate")).Text = DateTime.Now.ToString("yyyy/MM/dd");
      }
    }

    protected void DetailsView_Isidima_Form0_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        string Label_InvalidForm = "";

        TextBox TextBox_InsertDate = (TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_InsertDate");
        string DateToValidate = TextBox_InsertDate.Text.ToString();
        DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

        string ValidDate = "Yes";
        if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
        {
          Label_InvalidForm = Label_InvalidForm + "Not a valid date, date must be in the format yyyy/mm/dd<br />";
          ValidDate = "No";
        }

        if (ValidDate == "Yes")
        {
          DateTime Date = Convert.ToDateTime(((TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_InsertDate")).Text, CultureInfo.CurrentCulture);
          DateTime CurrentDate = DateTime.Now;

          if (Date.CompareTo(CurrentDate) > 0)
          {
            Label_InvalidForm = Label_InvalidForm + "No future dates allowed<br />";
            e.Cancel = true;
          }

          Session["IsidimaCategoryId"] = "";
          string SQLStringIsidima = "SELECT Isidima_Category_Id FROM InfoQuest_Form_Isidima_Category WHERE Facility_Id = @Facility_Id AND Isidima_Category_PatientVisitNumber = @Isidima_Category_PatientVisitNumber AND Isidima_Category_Date = @Isidima_Category_Date";
          using (SqlCommand SqlCommand_Isidima = new SqlCommand(SQLStringIsidima))
          {
            SqlCommand_Isidima.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
            SqlCommand_Isidima.Parameters.AddWithValue("@Isidima_Category_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
            SqlCommand_Isidima.Parameters.AddWithValue("@Isidima_Category_Date", Date);
            DataTable DataTable_Isidima;
            using (DataTable_Isidima = new DataTable())
            {
              DataTable_Isidima.Locale = CultureInfo.CurrentCulture;
              DataTable_Isidima = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Isidima).Copy();
              if (DataTable_Isidima.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_Isidima.Rows)
                {
                  Session["IsidimaCategoryId"] = DataRow_Row["Isidima_Category_Id"];
                }
              }
              else
              {
                Session["IsidimaCategoryId"] = "";
              }
            }
          }

          if (!string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
          {
            Label_InvalidForm = Label_InvalidForm + "A form has already been captured for this <br />Date : " + Date.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture) + "<br />";
            e.Cancel = true;
          }
          Session["IsidimaCategoryId"] = "";
        }
        else
        {
          e.Cancel = true;
        }

        if (e.Cancel == true)
        {
          ((Label)DetailsView_Isidima_Form0.FindControl("Label_InvalidForm")).Text = Convert.ToString(Label_InvalidForm, CultureInfo.CurrentCulture);
        }
        else if (e.Cancel == false)
        {
          Session["Isidima_Category_ReportNumber"] = InfoQuestWCF.InfoQuest_All.All_ReportNumber(Request.ServerVariables["LOGON_USER"], Request.QueryString["s_Facility_Id"], "27");

          SqlDataSource_Isidima_Form0.InsertParameters["Isidima_Category_ReportNumber"].DefaultValue = Session["Isidima_Category_ReportNumber"].ToString();
          SqlDataSource_Isidima_Form0.InsertParameters["Isidima_Category_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form0.InsertParameters["Isidima_Category_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form0.InsertParameters["Isidima_Category_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form0.InsertParameters["Isidima_Category_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form0.InsertParameters["Isidima_Category_History"].DefaultValue = "";
          SqlDataSource_Isidima_Form0.InsertParameters["Isidima_Category_IsActive"].DefaultValue = "true";

          Session["Isidima_Category_ReportNumber"] = "";
        }
      }
    }

    protected void DetailsView_Isidima_Form0_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaCategoryModifiedDate"] = e.OldValues["Isidima_Category_ModifiedDate"];
        object OLDIsidimaCategoryModifiedDate = Session["OLDIsidimaCategoryModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaCategoryModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm0 = (DataView)SqlDataSource_Isidima_Form0.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm0 = DataView_CompareForm0[0];
        Session["DBIsidimaCategoryModifiedDate"] = Convert.ToString(DataRowView_CompareForm0["Isidima_Category_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaCategoryModifiedBy"] = Convert.ToString(DataRowView_CompareForm0["Isidima_Category_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaCategoryModifiedDate = Session["DBIsidimaCategoryModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaCategoryModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form0.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form0.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaCategoryModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          string Label_InvalidForm = "";

          TextBox TextBox_EditDate = (TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_EditDate");
          string DateToValidate = TextBox_EditDate.Text.ToString();
          DateTime ValidatedDate = InfoQuestWCF.InfoQuest_All.All_ValidateDate(DateToValidate);

          string ValidDates = "Yes";
          if (ValidatedDate.ToString() == "0001/01/01 12:00:00 AM")
          {
            Label_InvalidForm = Label_InvalidForm + "Not a valid date, date must be in the format yyyy/mm/dd<br />";
            ValidDates = "No";
          }

          if (ValidDates == "Yes")
          {
            Session["Date"] = Convert.ToDateTime(e.OldValues["Isidima_Category_Date"], CultureInfo.CurrentCulture).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture);

            DateTime DBDate = Convert.ToDateTime(Session["Date"].ToString(), CultureInfo.CurrentCulture);
            DateTime PickedDate = Convert.ToDateTime(((TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_EditDate")).Text, CultureInfo.CurrentCulture);

            if ((PickedDate).CompareTo(DBDate) != 0)
            {
              DateTime PickedDate1 = Convert.ToDateTime(((TextBox)DetailsView_Isidima_Form0.FindControl("TextBox_EditDate")).Text, CultureInfo.CurrentCulture);
              DateTime CurrentDate = DateTime.Now;

              if (PickedDate1.CompareTo(CurrentDate) > 0)
              {
                Label_InvalidForm = Label_InvalidForm + "No future dates allowed <br />";
                e.Cancel = true;
              }

              Session["IsidimaCategoryId"] = "";
              string SQLStringIsidima = "SELECT Isidima_Category_Id FROM InfoQuest_Form_Isidima_Category WHERE Facility_Id = @Facility_Id AND Isidima_Category_PatientVisitNumber = @Isidima_Category_PatientVisitNumber AND Isidima_Category_Date = @Isidima_Category_Date";
              using (SqlCommand SqlCommand_Isidima = new SqlCommand(SQLStringIsidima))
              {
                SqlCommand_Isidima.Parameters.AddWithValue("@Facility_Id", Request.QueryString["s_Facility_Id"]);
                SqlCommand_Isidima.Parameters.AddWithValue("@Isidima_Category_PatientVisitNumber", Request.QueryString["s_Isidima_PatientVisitNumber"]);
                SqlCommand_Isidima.Parameters.AddWithValue("@Isidima_Category_Date", PickedDate1);
                DataTable DataTable_Isidima;
                using (DataTable_Isidima = new DataTable())
                {
                  DataTable_Isidima.Locale = CultureInfo.CurrentCulture;
                  DataTable_Isidima = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Isidima).Copy();
                  if (DataTable_Isidima.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_Isidima.Rows)
                    {
                      Session["IsidimaCategoryId"] = DataRow_Row["Isidima_Category_Id"];
                    }
                  }
                  else
                  {
                    Session["IsidimaCategoryId"] = "";
                  }
                }
              }

              if (!string.IsNullOrEmpty(Session["IsidimaCategoryId"].ToString()))
              {
                Label_InvalidForm = Label_InvalidForm + "A form has already been captured for this <br />Date : " + PickedDate1.ToString("yyyy/MM/dd", CultureInfo.CurrentCulture) + "<br />";
                e.Cancel = true;
              }
              Session["IsidimaCategoryId"] = "";
            }

            Session["Date"] = "";
          }
          else
          {
            e.Cancel = true;
          }

          if (e.Cancel == true)
          {
            ((Label)DetailsView_Isidima_Form0.FindControl("Label_InvalidForm")).Text = Convert.ToString(Label_InvalidForm, CultureInfo.CurrentCulture);
          }
          else if (e.Cancel == false)
          {
            if (Convert.ToString(e.OldValues["Isidima_Category_PatientCategory_List"], CultureInfo.CurrentCulture) == Convert.ToString(e.NewValues["Isidima_Category_PatientCategory_List"], CultureInfo.CurrentCulture))
            {
              Form0PatientCategoryChanged = false;
            }
            else
            {
              Form0PatientCategoryChanged = true;
            }

            e.NewValues["Isidima_Category_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Category_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Category", "Isidima_Category_Id = " + Request.QueryString["Isidima_Category_Id"]);

            DataView DataView_Isidima_Form0 = (DataView)SqlDataSource_Isidima_Form0.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form0 = DataView_Isidima_Form0[0];
            Session["IsidimaCategoryHistory"] = Convert.ToString(DataRowView_Isidima_Form0["Isidima_Category_History"], CultureInfo.CurrentCulture);

            Session["IsidimaCategoryHistory"] = Session["History"].ToString() + Session["IsidimaCategoryHistory"].ToString();
            e.NewValues["Isidima_Category_History"] = Session["IsidimaCategoryHistory"].ToString();

            Session["IsidimaCategoryHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDIsidimaCategoryModifiedDate"] = "";
        Session["DBIsidimaCategoryModifiedDate"] = "";
        Session["DBIsidimaCategoryModifiedBy"] = "";
      }
    }
    //---END--- --Form0--//


    //--START-- --Form1--//
    protected void SqlDataSource_Isidima_Form1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form1_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form1_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form1_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionMHAId"] = "";
        string SQLStringSectionMHA = "SELECT Isidima_Section_MHA_Id FROM InfoQuest_Form_Isidima_Section_MHA WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionMHA = new SqlCommand(SQLStringSectionMHA))
        {
          SqlCommand_SectionMHA.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionMHA;
          using (DataTable_SectionMHA = new DataTable())
          {
            DataTable_SectionMHA.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionMHA = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionMHA).Copy();
            if (DataTable_SectionMHA.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionMHA.Rows)
              {
                Session["IsidimaSectionMHAId"] = DataRow_Row["Isidima_Section_MHA_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionMHAId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionMHAId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form1.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section MHA has already been completed, please refer to bottom grid to view captured MHA section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionMHAId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form1.InsertParameters["Isidima_Section_MHA_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form1.InsertParameters["Isidima_Section_MHA_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form1.InsertParameters["Isidima_Section_MHA_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form1.InsertParameters["Isidima_Section_MHA_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form1.InsertParameters["Isidima_Section_MHA_History"].DefaultValue = "";

          int Form1Total = 0;
          int HiddenField_MHA_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_MHA_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form1.FindControl("RadioButtonList_InsertMHA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form1Total = Form1Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form1Total = Form1Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form1.FindControl("RadioButtonList_InsertMHA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form1Total = Form1Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form1Total = Form1Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form1.InsertParameters["Isidima_Section_MHA_Total"].DefaultValue = Form1Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionMHAModifiedDate"] = e.OldValues["Isidima_Section_MHA_ModifiedDate"];
        object OLDIsidimaSectionMHAModifiedDate = Session["OLDIsidimaSectionMHAModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionMHAModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm1 = (DataView)SqlDataSource_Isidima_Form1.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm1 = DataView_CompareForm1[0];
        Session["DBIsidimaSectionMHAModifiedDate"] = Convert.ToString(DataRowView_CompareForm1["Isidima_Section_MHA_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionMHAModifiedBy"] = Convert.ToString(DataRowView_CompareForm1["Isidima_Section_MHA_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionMHAModifiedDate = Session["DBIsidimaSectionMHAModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionMHAModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form1.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form1.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionMHAModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_MHA_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_MHA_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_MHA", "Isidima_Section_MHA_Id = " + Request.QueryString["Isidima_Section_MHA_Id"]);

            DataView DataView_Isidima_Form1 = (DataView)SqlDataSource_Isidima_Form1.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form1 = DataView_Isidima_Form1[0];
            Session["IsidimaSectionMHAHistory"] = Convert.ToString(DataRowView_Isidima_Form1["Isidima_Section_MHA_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionMHAHistory"] = Session["History"].ToString() + Session["IsidimaSectionMHAHistory"].ToString();
            e.NewValues["Isidima_Section_MHA_History"] = Session["IsidimaSectionMHAHistory"].ToString();

            Session["IsidimaSectionMHAHistory"] = "";
            Session["History"] = "";


            int Form1Total = 0;
            int HiddenField_MHA_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_MHA_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form1.FindControl("RadioButtonList_EditMHA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form1Total = Form1Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form1Total = Form1Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form1.FindControl("RadioButtonList_EditMHA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form1Total = Form1Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form1Total = Form1Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form1.FindControl("HiddenField_MHA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_MHA_Total"] = Form1Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionMHAModifiedDate"] = "";
        Session["DBIsidimaSectionMHAModifiedDate"] = "";
        Session["DBIsidimaSectionMHAModifiedBy"] = "";
      }
    }
    //---END--- --Form1--//


    //--START-- --Form2--//
    protected void SqlDataSource_Isidima_Form2_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form2_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form2_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form2_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionVPAId"] = "";
        string SQLStringSectionVPA = "SELECT Isidima_Section_VPA_Id FROM InfoQuest_Form_Isidima_Section_VPA WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionVPA = new SqlCommand(SQLStringSectionVPA))
        {
          SqlCommand_SectionVPA.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionVPA;
          using (DataTable_SectionVPA = new DataTable())
          {
            DataTable_SectionVPA.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionVPA = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionVPA).Copy();
            if (DataTable_SectionVPA.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionVPA.Rows)
              {
                Session["IsidimaSectionVPAId"] = DataRow_Row["Isidima_Section_VPA_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionVPAId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionVPAId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form2.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section VPA has already been completed, please refer to bottom grid to view captured VPA section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionVPAId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form2.InsertParameters["Isidima_Section_VPA_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form2.InsertParameters["Isidima_Section_VPA_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form2.InsertParameters["Isidima_Section_VPA_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form2.InsertParameters["Isidima_Section_VPA_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form2.InsertParameters["Isidima_Section_VPA_History"].DefaultValue = "";

          int Form2Total = 0;
          int HiddenField_VPA_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_VPA_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form2.FindControl("RadioButtonList_InsertVPA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form2Total = Form2Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form2Total = Form2Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form2.FindControl("RadioButtonList_InsertVPA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form2Total = Form2Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form2Total = Form2Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form2.InsertParameters["Isidima_Section_VPA_Total"].DefaultValue = Form2Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form2_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionVPAModifiedDate"] = e.OldValues["Isidima_Section_VPA_ModifiedDate"];
        object OLDIsidimaSectionVPAModifiedDate = Session["OLDIsidimaSectionVPAModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionVPAModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm2 = (DataView)SqlDataSource_Isidima_Form2.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm2 = DataView_CompareForm2[0];
        Session["DBIsidimaSectionVPAModifiedDate"] = Convert.ToString(DataRowView_CompareForm2["Isidima_Section_VPA_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionVPAModifiedBy"] = Convert.ToString(DataRowView_CompareForm2["Isidima_Section_VPA_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionVPAModifiedDate = Session["DBIsidimaSectionVPAModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionVPAModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form2.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form2.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionVPAModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_VPA_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_VPA_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_VPA", "Isidima_Section_VPA_Id = " + Request.QueryString["Isidima_Section_VPA_Id"]);

            DataView DataView_Isidima_Form2 = (DataView)SqlDataSource_Isidima_Form2.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form2 = DataView_Isidima_Form2[0];
            Session["IsidimaSectionVPAHistory"] = Convert.ToString(DataRowView_Isidima_Form2["Isidima_Section_VPA_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionVPAHistory"] = Session["History"].ToString() + Session["IsidimaSectionVPAHistory"].ToString();
            e.NewValues["Isidima_Section_VPA_History"] = Session["IsidimaSectionVPAHistory"].ToString();

            Session["IsidimaSectionVPAHistory"] = "";
            Session["History"] = "";


            int Form2Total = 0;
            int HiddenField_VPA_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_VPA_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form2.FindControl("RadioButtonList_EditVPA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form2Total = Form2Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form2Total = Form2Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form2.FindControl("RadioButtonList_EditVPA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form2Total = Form2Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form2Total = Form2Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form2.FindControl("HiddenField_VPA_A" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_VPA_Total"] = Form2Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionVPAModifiedDate"] = "";
        Session["DBIsidimaSectionVPAModifiedDate"] = "";
        Session["DBIsidimaSectionVPAModifiedBy"] = "";
      }
    }
    //---END--- --Form2--//


    //--START-- --Form3--//
    protected void SqlDataSource_Isidima_Form3_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form3_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form3_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form3_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionJId"] = "";
        string SQLStringSectionJ = "SELECT Isidima_Section_J_Id FROM InfoQuest_Form_Isidima_Section_J WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionJ = new SqlCommand(SQLStringSectionJ))
        {
          SqlCommand_SectionJ.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionJ;
          using (DataTable_SectionJ = new DataTable())
          {
            DataTable_SectionJ.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionJ = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionJ).Copy();
            if (DataTable_SectionJ.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionJ.Rows)
              {
                Session["IsidimaSectionJId"] = DataRow_Row["Isidima_Section_J_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionJId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionJId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form3.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section J has already been completed, please refer to bottom grid to view captured J section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionJId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form3.InsertParameters["Isidima_Section_J_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form3.InsertParameters["Isidima_Section_J_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form3.InsertParameters["Isidima_Section_J_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form3.InsertParameters["Isidima_Section_J_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form3.InsertParameters["Isidima_Section_J_History"].DefaultValue = "";

          int Form3Total = 0;
          int HiddenField_J_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_J_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form3.FindControl("RadioButtonList_InsertJ_J0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form3Total = Form3Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form3Total = Form3Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form3.FindControl("RadioButtonList_InsertJ_J" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form3Total = Form3Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form3Total = Form3Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form3.InsertParameters["Isidima_Section_J_Total"].DefaultValue = Form3Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form3_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionJModifiedDate"] = e.OldValues["Isidima_Section_J_ModifiedDate"];
        object OLDIsidimaSectionJModifiedDate = Session["OLDIsidimaSectionJModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionJModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm3 = (DataView)SqlDataSource_Isidima_Form3.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm3 = DataView_CompareForm3[0];
        Session["DBIsidimaSectionJModifiedDate"] = Convert.ToString(DataRowView_CompareForm3["Isidima_Section_J_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionJModifiedBy"] = Convert.ToString(DataRowView_CompareForm3["Isidima_Section_J_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionJModifiedDate = Session["DBIsidimaSectionJModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionJModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form3.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form3.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionJModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0363e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_J_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_J_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_J", "Isidima_Section_J_Id = " + Request.QueryString["Isidima_Section_J_Id"]);

            DataView DataView_Isidima_Form3 = (DataView)SqlDataSource_Isidima_Form3.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form3 = DataView_Isidima_Form3[0];
            Session["IsidimaSectionJHistory"] = Convert.ToString(DataRowView_Isidima_Form3["Isidima_Section_J_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionJHistory"] = Session["History"].ToString() + Session["IsidimaSectionJHistory"].ToString();
            e.NewValues["Isidima_Section_J_History"] = Session["IsidimaSectionJHistory"].ToString();

            Session["IsidimaSectionJHistory"] = "";
            Session["History"] = "";


            int Form3Total = 0;
            int HiddenField_J_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_J_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form3.FindControl("RadioButtonList_EditJ_J0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form3Total = Form3Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form3Total = Form3Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form3.FindControl("RadioButtonList_EditJ_J" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form3Total = Form3Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form3Total = Form3Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form3.FindControl("HiddenField_J_J" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_J_Total"] = Form3Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionJModifiedDate"] = "";
        Session["DBIsidimaSectionJModifiedDate"] = "";
        Session["DBIsidimaSectionJModifiedBy"] = "";
      }
    }
    //---END--- --Form3--//


    //--START-- --Form4--//
    protected void SqlDataSource_Isidima_Form4_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form4_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form4_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form4_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionDMHId"] = "";
        string SQLStringSectionDMH = "SELECT Isidima_Section_DMH_Id FROM InfoQuest_Form_Isidima_Section_DMH WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionDMH = new SqlCommand(SQLStringSectionDMH))
        {
          SqlCommand_SectionDMH.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionDMH;
          using (DataTable_SectionDMH = new DataTable())
          {
            DataTable_SectionDMH.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionDMH = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionDMH).Copy();
            if (DataTable_SectionDMH.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionDMH.Rows)
              {
                Session["IsidimaSectionDMHId"] = DataRow_Row["Isidima_Section_DMH_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionDMHId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionDMHId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form4.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section DMH has already been completed, please refer to bottom grid to view captured DMH section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionDMHId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form4.InsertParameters["Isidima_Section_DMH_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form4.InsertParameters["Isidima_Section_DMH_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form4.InsertParameters["Isidima_Section_DMH_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form4.InsertParameters["Isidima_Section_DMH_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form4.InsertParameters["Isidima_Section_DMH_History"].DefaultValue = "";

          int Form4Total = 0;
          int HiddenField_DMH_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_DMH_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form4.FindControl("RadioButtonList_InsertDMH_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form4Total = Form4Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form4Total = Form4Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form4.FindControl("RadioButtonList_InsertDMH_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form4Total = Form4Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form4Total = Form4Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form4.InsertParameters["Isidima_Section_DMH_Total"].DefaultValue = Form4Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form4_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionDMHModifiedDate"] = e.OldValues["Isidima_Section_DMH_ModifiedDate"];
        object OLDIsidimaSectionDMHModifiedDate = Session["OLDIsidimaSectionDMHModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionDMHModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm4 = (DataView)SqlDataSource_Isidima_Form4.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm4 = DataView_CompareForm4[0];
        Session["DBIsidimaSectionDMHModifiedDate"] = Convert.ToString(DataRowView_CompareForm4["Isidima_Section_DMH_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionDMHModifiedBy"] = Convert.ToString(DataRowView_CompareForm4["Isidima_Section_DMH_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionDMHModifiedDate = Session["DBIsidimaSectionDMHModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionDMHModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form4.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form4.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionDMHModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0464e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_DMH_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_DMH_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_DMH", "Isidima_Section_DMH_Id = " + Request.QueryString["Isidima_Section_DMH_Id"]);

            DataView DataView_Isidima_Form4 = (DataView)SqlDataSource_Isidima_Form4.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form4 = DataView_Isidima_Form4[0];
            Session["IsidimaSectionDMHHistory"] = Convert.ToString(DataRowView_Isidima_Form4["Isidima_Section_DMH_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionDMHHistory"] = Session["History"].ToString() + Session["IsidimaSectionDMHHistory"].ToString();
            e.NewValues["Isidima_Section_DMH_History"] = Session["IsidimaSectionDMHHistory"].ToString();

            Session["IsidimaSectionDMHHistory"] = "";
            Session["History"] = "";


            int Form4Total = 0;
            int HiddenField_DMH_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_DMH_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form4.FindControl("RadioButtonList_EditDMH_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form4Total = Form4Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form4Total = Form4Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form4.FindControl("RadioButtonList_EditDMH_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form4Total = Form4Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form4Total = Form4Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form4.FindControl("HiddenField_DMH_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_DMH_Total"] = Form4Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionDMHModifiedDate"] = "";
        Session["DBIsidimaSectionDMHModifiedDate"] = "";
        Session["DBIsidimaSectionDMHModifiedBy"] = "";
      }
    }
    //---END--- --Form4--//


    //--START-- --Form5--//
    protected void SqlDataSource_Isidima_Form5_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form5_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form5_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form5_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionFId"] = "";
        string SQLStringSectionF = "SELECT Isidima_Section_F_Id FROM InfoQuest_Form_Isidima_Section_F WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionF = new SqlCommand(SQLStringSectionF))
        {
          SqlCommand_SectionF.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionF;
          using (DataTable_SectionF = new DataTable())
          {
            DataTable_SectionF.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionF = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionF).Copy();
            if (DataTable_SectionF.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionF.Rows)
              {
                Session["IsidimaSectionFId"] = DataRow_Row["Isidima_Section_F_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionFId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionFId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form5.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section F has already been completed, please refer to bottom grid to view captured F section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionFId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form5.InsertParameters["Isidima_Section_F_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form5.InsertParameters["Isidima_Section_F_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form5.InsertParameters["Isidima_Section_F_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form5.InsertParameters["Isidima_Section_F_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form5.InsertParameters["Isidima_Section_F_History"].DefaultValue = "";

          int Form5Total = 0;
          int HiddenField_F_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_F_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form5.FindControl("RadioButtonList_InsertF_F0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form5Total = Form5Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form5Total = Form5Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form5.FindControl("RadioButtonList_InsertF_F" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form5Total = Form5Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form5Total = Form5Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form5.InsertParameters["Isidima_Section_F_Total"].DefaultValue = Form5Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form5_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionFModifiedDate"] = e.OldValues["Isidima_Section_F_ModifiedDate"];
        object OLDIsidimaSectionFModifiedDate = Session["OLDIsidimaSectionFModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionFModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm5 = (DataView)SqlDataSource_Isidima_Form5.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm5 = DataView_CompareForm5[0];
        Session["DBIsidimaSectionFModifiedDate"] = Convert.ToString(DataRowView_CompareForm5["Isidima_Section_F_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionFModifiedBy"] = Convert.ToString(DataRowView_CompareForm5["Isidima_Section_F_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionFModifiedDate = Session["DBIsidimaSectionFModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionFModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form5.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form5.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionFModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0565e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_F_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_F_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_F", "Isidima_Section_F_Id = " + Request.QueryString["Isidima_Section_F_Id"]);

            DataView DataView_Isidima_Form5 = (DataView)SqlDataSource_Isidima_Form5.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form5 = DataView_Isidima_Form5[0];
            Session["IsidimaSectionFHistory"] = Convert.ToString(DataRowView_Isidima_Form5["Isidima_Section_F_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionFHistory"] = Session["History"].ToString() + Session["IsidimaSectionFHistory"].ToString();
            e.NewValues["Isidima_Section_F_History"] = Session["IsidimaSectionFHistory"].ToString();

            Session["IsidimaSectionFHistory"] = "";
            Session["History"] = "";


            int Form5Total = 0;
            int HiddenField_F_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_F_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form5.FindControl("RadioButtonList_EditF_F0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form5Total = Form5Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form5Total = Form5Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form5.FindControl("RadioButtonList_EditF_F" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form5Total = Form5Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form5Total = Form5Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form5.FindControl("HiddenField_F_F" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_F_Total"] = Form5Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionFModifiedDate"] = "";
        Session["DBIsidimaSectionFModifiedDate"] = "";
        Session["DBIsidimaSectionFModifiedBy"] = "";
      }
    }
    //---END--- --Form5--//


    //--START-- --Form6--//
    protected void SqlDataSource_Isidima_Form6_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form6_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form6_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form6_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionIId"] = "";
        string SQLStringSectionI = "SELECT Isidima_Section_I_Id FROM InfoQuest_Form_Isidima_Section_I WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionI = new SqlCommand(SQLStringSectionI))
        {
          SqlCommand_SectionI.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionI;
          using (DataTable_SectionI = new DataTable())
          {
            DataTable_SectionI.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionI = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionI).Copy();
            if (DataTable_SectionI.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionI.Rows)
              {
                Session["IsidimaSectionIId"] = DataRow_Row["Isidima_Section_I_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionIId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionIId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form6.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section I has already been completed, please refer to bottom grid to view captured I section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionIId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form6.InsertParameters["Isidima_Section_I_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form6.InsertParameters["Isidima_Section_I_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form6.InsertParameters["Isidima_Section_I_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form6.InsertParameters["Isidima_Section_I_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form6.InsertParameters["Isidima_Section_I_History"].DefaultValue = "";

          int Form6Total = 0;
          int HiddenField_I_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_I_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form6.FindControl("RadioButtonList_InsertI_I0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form6Total = Form6Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form6Total = Form6Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form6.FindControl("RadioButtonList_InsertI_I" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form6Total = Form6Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form6Total = Form6Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form6.InsertParameters["Isidima_Section_I_Total"].DefaultValue = Form6Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form6_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionIModifiedDate"] = e.OldValues["Isidima_Section_I_ModifiedDate"];
        object OLDIsidimaSectionIModifiedDate = Session["OLDIsidimaSectionIModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionIModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm6 = (DataView)SqlDataSource_Isidima_Form6.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm6 = DataView_CompareForm6[0];
        Session["DBIsidimaSectionIModifiedDate"] = Convert.ToString(DataRowView_CompareForm6["Isidima_Section_I_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionIModifiedBy"] = Convert.ToString(DataRowView_CompareForm6["Isidima_Section_I_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionIModifiedDate = Session["DBIsidimaSectionIModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionIModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form6.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form6.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionIModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0666e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_I_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_I_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_I", "Isidima_Section_I_Id = " + Request.QueryString["Isidima_Section_I_Id"]);

            DataView DataView_Isidima_Form6 = (DataView)SqlDataSource_Isidima_Form6.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form6 = DataView_Isidima_Form6[0];
            Session["IsidimaSectionIHistory"] = Convert.ToString(DataRowView_Isidima_Form6["Isidima_Section_I_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionIHistory"] = Session["History"].ToString() + Session["IsidimaSectionIHistory"].ToString();
            e.NewValues["Isidima_Section_I_History"] = Session["IsidimaSectionIHistory"].ToString();

            Session["IsidimaSectionIHistory"] = "";
            Session["History"] = "";


            int Form6Total = 0;
            int HiddenField_I_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_I_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form6.FindControl("RadioButtonList_EditI_I0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form6Total = Form6Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form6Total = Form6Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form6.FindControl("RadioButtonList_EditI_I" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form6Total = Form6Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form6Total = Form6Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form6.FindControl("HiddenField_I_I" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_I_Total"] = Form6Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionIModifiedDate"] = "";
        Session["DBIsidimaSectionIModifiedDate"] = "";
        Session["DBIsidimaSectionIModifiedBy"] = "";
      }
    }
    //---END--- --Form6--//


    //--START-- --Form7--//
    protected void SqlDataSource_Isidima_Form7_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form7_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form7_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form7_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionPSYId"] = "";
        string SQLStringSectionPSY = "SELECT Isidima_Section_PSY_Id FROM InfoQuest_Form_Isidima_Section_PSY WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionPSY = new SqlCommand(SQLStringSectionPSY))
        {
          SqlCommand_SectionPSY.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionPSY;
          using (DataTable_SectionPSY = new DataTable())
          {
            DataTable_SectionPSY.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionPSY = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionPSY).Copy();
            if (DataTable_SectionPSY.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionPSY.Rows)
              {
                Session["IsidimaSectionPSYId"] = DataRow_Row["Isidima_Section_PSY_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionPSYId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionPSYId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form7.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section PSY has already been completed, please refer to bottom grid to view captured PSY section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionPSYId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form7.InsertParameters["Isidima_Section_PSY_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form7.InsertParameters["Isidima_Section_PSY_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form7.InsertParameters["Isidima_Section_PSY_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form7.InsertParameters["Isidima_Section_PSY_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form7.InsertParameters["Isidima_Section_PSY_History"].DefaultValue = "";

          int Form7Total = 0;
          int HiddenField_PSY_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_PSY_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form7.FindControl("RadioButtonList_InsertPSY_C0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form7Total = Form7Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form7Total = Form7Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form7.FindControl("RadioButtonList_InsertPSY_C" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form7Total = Form7Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form7Total = Form7Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form7.InsertParameters["Isidima_Section_PSY_Total"].DefaultValue = Form7Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form7_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionPSYModifiedDate"] = e.OldValues["Isidima_Section_PSY_ModifiedDate"];
        object OLDIsidimaSectionPSYModifiedDate = Session["OLDIsidimaSectionPSYModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionPSYModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm7 = (DataView)SqlDataSource_Isidima_Form7.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm7 = DataView_CompareForm7[0];
        Session["DBIsidimaSectionPSYModifiedDate"] = Convert.ToString(DataRowView_CompareForm7["Isidima_Section_PSY_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionPSYModifiedBy"] = Convert.ToString(DataRowView_CompareForm7["Isidima_Section_PSY_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionPSYModifiedDate = Session["DBIsidimaSectionPSYModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionPSYModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form7.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form7.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionPSYModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0868e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_PSY_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_PSY_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_PSY", "Isidima_Section_PSY_Id = " + Request.QueryString["Isidima_Section_PSY_Id"]);

            DataView DataView_Isidima_Form7 = (DataView)SqlDataSource_Isidima_Form7.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form7 = DataView_Isidima_Form7[0];
            Session["IsidimaSectionPSYHistory"] = Convert.ToString(DataRowView_Isidima_Form7["Isidima_Section_PSY_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionPSYHistory"] = Session["History"].ToString() + Session["IsidimaSectionPSYHistory"].ToString();
            e.NewValues["Isidima_Section_PSY_History"] = Session["IsidimaSectionPSYHistory"].ToString();

            Session["IsidimaSectionPSYHistory"] = "";
            Session["History"] = "";


            int Form7Total = 0;
            int HiddenField_PSY_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_PSY_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form7.FindControl("RadioButtonList_EditPSY_C0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form7Total = Form7Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form7Total = Form7Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form7.FindControl("RadioButtonList_EditPSY_C" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form7Total = Form7Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form7Total = Form7Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form7.FindControl("HiddenField_PSY_C" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_PSY_Total"] = Form7Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionPSYModifiedDate"] = "";
        Session["DBIsidimaSectionPSYModifiedDate"] = "";
        Session["DBIsidimaSectionPSYModifiedBy"] = "";
      }
    }
    //---END--- --Form7--//


    //--START-- --Form8--//
    protected void SqlDataSource_Isidima_Form8_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form8_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form8_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form8_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionTId"] = "";
        string SQLStringSectionT = "SELECT Isidima_Section_T_Id FROM InfoQuest_Form_Isidima_Section_T WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionT = new SqlCommand(SQLStringSectionT))
        {
          SqlCommand_SectionT.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionT;
          using (DataTable_SectionT = new DataTable())
          {
            DataTable_SectionT.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionT = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionT).Copy();
            if (DataTable_SectionT.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionT.Rows)
              {
                Session["IsidimaSectionTId"] = DataRow_Row["Isidima_Section_T_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionTId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionTId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form8.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section T has already been completed, please refer to bottom grid to view captured T section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionTId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form8.InsertParameters["Isidima_Section_T_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form8.InsertParameters["Isidima_Section_T_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form8.InsertParameters["Isidima_Section_T_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form8.InsertParameters["Isidima_Section_T_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form8.InsertParameters["Isidima_Section_T_History"].DefaultValue = "";

          int Form8Total = 0;
          int HiddenField_T_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_T_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form8.FindControl("RadioButtonList_InsertT_T0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form8Total = Form8Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form8Total = Form8Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form8.FindControl("RadioButtonList_InsertT_T" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form8Total = Form8Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form8Total = Form8Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form8.InsertParameters["Isidima_Section_T_Total"].DefaultValue = Form8Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form8_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionTModifiedDate"] = e.OldValues["Isidima_Section_T_ModifiedDate"];
        object OLDIsidimaSectionTModifiedDate = Session["OLDIsidimaSectionTModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionTModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm8 = (DataView)SqlDataSource_Isidima_Form8.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm8 = DataView_CompareForm8[0];
        Session["DBIsidimaSectionTModifiedDate"] = Convert.ToString(DataRowView_CompareForm8["Isidima_Section_T_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionTModifiedBy"] = Convert.ToString(DataRowView_CompareForm8["Isidima_Section_T_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionTModifiedDate = Session["DBIsidimaSectionTModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionTModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form8.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form8.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionTModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0868e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_T_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_T_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_T", "Isidima_Section_T_Id = " + Request.QueryString["Isidima_Section_T_Id"]);

            DataView DataView_Isidima_Form8 = (DataView)SqlDataSource_Isidima_Form8.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form8 = DataView_Isidima_Form8[0];
            Session["IsidimaSectionTHistory"] = Convert.ToString(DataRowView_Isidima_Form8["Isidima_Section_T_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionTHistory"] = Session["History"].ToString() + Session["IsidimaSectionTHistory"].ToString();
            e.NewValues["Isidima_Section_T_History"] = Session["IsidimaSectionTHistory"].ToString();

            Session["IsidimaSectionTHistory"] = "";
            Session["History"] = "";


            int Form8Total = 0;
            int HiddenField_T_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_T_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form8.FindControl("RadioButtonList_EditT_T0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form8Total = Form8Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form8Total = Form8Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form8.FindControl("RadioButtonList_EditT_T" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form8Total = Form8Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form8Total = Form8Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form8.FindControl("HiddenField_T_T" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_T_Total"] = Form8Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionTModifiedDate"] = "";
        Session["DBIsidimaSectionTModifiedDate"] = "";
        Session["DBIsidimaSectionTModifiedBy"] = "";
      }
    }
    //---END--- --Form8--//


    //--START-- --Form9--//
    protected void SqlDataSource_Isidima_Form9_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form9_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form9_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form9_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionBId"] = "";
        string SQLStringSectionB = "SELECT Isidima_Section_B_Id FROM InfoQuest_Form_Isidima_Section_B WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionB = new SqlCommand(SQLStringSectionB))
        {
          SqlCommand_SectionB.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionB;
          using (DataTable_SectionB = new DataTable())
          {
            DataTable_SectionB.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionB = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionB).Copy();
            if (DataTable_SectionB.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionB.Rows)
              {
                Session["IsidimaSectionBId"] = DataRow_Row["Isidima_Section_B_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionBId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionBId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form9.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section B has already been completed, please refer to bottom grid to view captured B section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionBId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form9.InsertParameters["Isidima_Section_B_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form9.InsertParameters["Isidima_Section_B_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form9.InsertParameters["Isidima_Section_B_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form9.InsertParameters["Isidima_Section_B_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form9.InsertParameters["Isidima_Section_B_History"].DefaultValue = "";

          int Form9Total = 0;
          int HiddenField_B_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_B_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form9.FindControl("RadioButtonList_InsertB_B0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form9Total = Form9Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form9Total = Form9Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form9.FindControl("RadioButtonList_InsertB_B" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form9Total = Form9Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form9Total = Form9Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form9.InsertParameters["Isidima_Section_B_Total"].DefaultValue = Form9Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form9_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionBModifiedDate"] = e.OldValues["Isidima_Section_B_ModifiedDate"];
        object OLDIsidimaSectionBModifiedDate = Session["OLDIsidimaSectionBModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionBModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm9 = (DataView)SqlDataSource_Isidima_Form9.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm9 = DataView_CompareForm9[0];
        Session["DBIsidimaSectionBModifiedDate"] = Convert.ToString(DataRowView_CompareForm9["Isidima_Section_B_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionBModifiedBy"] = Convert.ToString(DataRowView_CompareForm9["Isidima_Section_B_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionBModifiedDate = Session["DBIsidimaSectionBModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionBModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form9.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form9.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionBModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0969e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_B_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_B_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_B", "Isidima_Section_B_Id = " + Request.QueryString["Isidima_Section_B_Id"]);

            DataView DataView_Isidima_Form9 = (DataView)SqlDataSource_Isidima_Form9.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form9 = DataView_Isidima_Form9[0];
            Session["IsidimaSectionBHistory"] = Convert.ToString(DataRowView_Isidima_Form9["Isidima_Section_B_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionBHistory"] = Session["History"].ToString() + Session["IsidimaSectionBHistory"].ToString();
            e.NewValues["Isidima_Section_B_History"] = Session["IsidimaSectionBHistory"].ToString();

            Session["IsidimaSectionBHistory"] = "";
            Session["History"] = "";


            int Form9Total = 0;
            int HiddenField_B_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_B_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form9.FindControl("RadioButtonList_EditB_B0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form9Total = Form9Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form9Total = Form9Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form9.FindControl("RadioButtonList_EditB_B" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form9Total = Form9Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form9Total = Form9Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form9.FindControl("HiddenField_B_B" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_B_Total"] = Form9Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionBModifiedDate"] = "";
        Session["DBIsidimaSectionBModifiedDate"] = "";
        Session["DBIsidimaSectionBModifiedBy"] = "";
      }
    }
    //---END--- --Form9--//


    //--START-- --Form10--//
    protected void SqlDataSource_Isidima_Form10_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form10_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form10_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form10_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionRId"] = "";
        string SQLStringSectionR = "SELECT Isidima_Section_R_Id FROM InfoQuest_Form_Isidima_Section_R WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionR = new SqlCommand(SQLStringSectionR))
        {
          SqlCommand_SectionR.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionR;
          using (DataTable_SectionR = new DataTable())
          {
            DataTable_SectionR.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionR = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionR).Copy();
            if (DataTable_SectionR.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionR.Rows)
              {
                Session["IsidimaSectionRId"] = DataRow_Row["Isidima_Section_R_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionRId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionRId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form10.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section R has already been completed, please refer to bottom grid to view captured R section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionRId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form10.InsertParameters["Isidima_Section_R_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form10.InsertParameters["Isidima_Section_R_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form10.InsertParameters["Isidima_Section_R_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form10.InsertParameters["Isidima_Section_R_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form10.InsertParameters["Isidima_Section_R_History"].DefaultValue = "";

          int Form10Total = 0;
          int HiddenField_R_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_R_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form10.FindControl("RadioButtonList_InsertR_R0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form10Total = Form10Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form10Total = Form10Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form10.FindControl("RadioButtonList_InsertR_R" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form10Total = Form10Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form10Total = Form10Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form10.InsertParameters["Isidima_Section_R_Total"].DefaultValue = Form10Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form10_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionRModifiedDate"] = e.OldValues["Isidima_Section_R_ModifiedDate"];
        object OLDIsidimaSectionRModifiedDate = Session["OLDIsidimaSectionRModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionRModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm10 = (DataView)SqlDataSource_Isidima_Form10.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm10 = DataView_CompareForm10[0];
        Session["DBIsidimaSectionRModifiedDate"] = Convert.ToString(DataRowView_CompareForm10["Isidima_Section_R_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionRModifiedBy"] = Convert.ToString(DataRowView_CompareForm10["Isidima_Section_R_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionRModifiedDate = Session["DBIsidimaSectionRModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionRModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form10.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form10.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionRModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b010610e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_R_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_R_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_R", "Isidima_Section_R_Id = " + Request.QueryString["Isidima_Section_R_Id"]);

            DataView DataView_Isidima_Form10 = (DataView)SqlDataSource_Isidima_Form10.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form10 = DataView_Isidima_Form10[0];
            Session["IsidimaSectionRHistory"] = Convert.ToString(DataRowView_Isidima_Form10["Isidima_Section_R_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionRHistory"] = Session["History"].ToString() + Session["IsidimaSectionRHistory"].ToString();
            e.NewValues["Isidima_Section_R_History"] = Session["IsidimaSectionRHistory"].ToString();

            Session["IsidimaSectionRHistory"] = "";
            Session["History"] = "";


            int Form10Total = 0;
            int HiddenField_R_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_R_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form10.FindControl("RadioButtonList_EditR_R0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form10Total = Form10Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form10Total = Form10Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form10.FindControl("RadioButtonList_EditR_R" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form10Total = Form10Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form10Total = Form10Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form10.FindControl("HiddenField_R_R" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_R_Total"] = Form10Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionRModifiedDate"] = "";
        Session["DBIsidimaSectionRModifiedDate"] = "";
        Session["DBIsidimaSectionRModifiedBy"] = "";
      }
    }
    //---END--- --Form10--//


    //--START-- --Form11--//
    protected void SqlDataSource_Isidima_Form11_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form11_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form11_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form11_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionSId"] = "";
        string SQLStringSectionS = "SELECT Isidima_Section_S_Id FROM InfoQuest_Form_Isidima_Section_S WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionS = new SqlCommand(SQLStringSectionS))
        {
          SqlCommand_SectionS.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionS;
          using (DataTable_SectionS = new DataTable())
          {
            DataTable_SectionS.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionS = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionS).Copy();
            if (DataTable_SectionS.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionS.Rows)
              {
                Session["IsidimaSectionSId"] = DataRow_Row["Isidima_Section_S_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionSId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionSId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form11.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section S has already been completed, please refer to bottom grid to view captured S section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionSId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form11.InsertParameters["Isidima_Section_S_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form11.InsertParameters["Isidima_Section_S_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form11.InsertParameters["Isidima_Section_S_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form11.InsertParameters["Isidima_Section_S_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form11.InsertParameters["Isidima_Section_S_History"].DefaultValue = "";

          int Form11Total = 0;
          int HiddenField_S_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_S_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form11.FindControl("RadioButtonList_InsertS_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form11Total = Form11Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form11Total = Form11Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form11.FindControl("RadioButtonList_InsertS_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form11Total = Form11Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form11Total = Form11Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form11.InsertParameters["Isidima_Section_S_Total"].DefaultValue = Form11Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form11_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionSModifiedDate"] = e.OldValues["Isidima_Section_S_ModifiedDate"];
        object OLDIsidimaSectionSModifiedDate = Session["OLDIsidimaSectionSModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionSModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm11 = (DataView)SqlDataSource_Isidima_Form11.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm11 = DataView_CompareForm11[0];
        Session["DBIsidimaSectionSModifiedDate"] = Convert.ToString(DataRowView_CompareForm11["Isidima_Section_S_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionSModifiedBy"] = Convert.ToString(DataRowView_CompareForm11["Isidima_Section_S_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionSModifiedDate = Session["DBIsidimaSectionSModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionSModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form11.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form11.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionSModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b011611e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_S_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_S_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_S", "Isidima_Section_S_Id = " + Request.QueryString["Isidima_Section_S_Id"]);

            DataView DataView_Isidima_Form11 = (DataView)SqlDataSource_Isidima_Form11.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form11 = DataView_Isidima_Form11[0];
            Session["IsidimaSectionSHistory"] = Convert.ToString(DataRowView_Isidima_Form11["Isidima_Section_S_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionSHistory"] = Session["History"].ToString() + Session["IsidimaSectionSHistory"].ToString();
            e.NewValues["Isidima_Section_S_History"] = Session["IsidimaSectionSHistory"].ToString();

            Session["IsidimaSectionSHistory"] = "";
            Session["History"] = "";


            int Form11Total = 0;
            int HiddenField_S_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_S_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form11.FindControl("RadioButtonList_EditS_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form11Total = Form11Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form11Total = Form11Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form11.FindControl("RadioButtonList_EditS_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form11Total = Form11Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form11Total = Form11Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form11.FindControl("HiddenField_S_S" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_S_Total"] = Form11Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionSModifiedDate"] = "";
        Session["DBIsidimaSectionSModifiedDate"] = "";
        Session["DBIsidimaSectionSModifiedBy"] = "";
      }
    }
    //---END--- --Form11--//


    //--START-- --Form12--//
    protected void SqlDataSource_Isidima_Form12_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
    }

    protected void SqlDataSource_Isidima_Form12_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form12_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          Response.Redirect("Form_Isidima.aspx?s_Facility_Id=" + Request.QueryString["s_Facility_Id"] + "&s_Isidima_PatientVisitNumber=" + Request.QueryString["s_Isidima_PatientVisitNumber"] + "", false);
        }
      }
    }

    protected void DetailsView_Isidima_Form12_ItemInserting(object sender, CancelEventArgs e)
    {
      if (e != null)
      {
        Session["IsidimaSectionVId"] = "";
        string SQLStringSectionV = "SELECT Isidima_Section_V_Id FROM InfoQuest_Form_Isidima_Section_V WHERE Isidima_Category_Id = @Isidima_Category_Id";
        using (SqlCommand SqlCommand_SectionV = new SqlCommand(SQLStringSectionV))
        {
          SqlCommand_SectionV.Parameters.AddWithValue("@Isidima_Category_Id", Request.QueryString["Isidima_Category_Id"]);
          DataTable DataTable_SectionV;
          using (DataTable_SectionV = new DataTable())
          {
            DataTable_SectionV.Locale = CultureInfo.CurrentCulture;
            DataTable_SectionV = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SectionV).Copy();
            if (DataTable_SectionV.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SectionV.Rows)
              {
                Session["IsidimaSectionVId"] = DataRow_Row["Isidima_Section_V_Id"];
              }
            }
            else
            {
              Session["IsidimaSectionVId"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["IsidimaSectionVId"].ToString()))
        {
          ((Label)DetailsView_Isidima_Form12.FindControl("Label_InvalidForm")).Text = Convert.ToString("Section V has already been completed, please refer to bottom grid to view captured V section", CultureInfo.CurrentCulture);
          e.Cancel = true;
        }
        Session["IsidimaSectionVId"] = "";

        if (e.Cancel == false)
        {
          SqlDataSource_Isidima_Form12.InsertParameters["Isidima_Section_V_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form12.InsertParameters["Isidima_Section_V_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form12.InsertParameters["Isidima_Section_V_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Isidima_Form12.InsertParameters["Isidima_Section_V_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Isidima_Form12.InsertParameters["Isidima_Section_V_History"].DefaultValue = "";

          int Form12Total = 0;
          int HiddenField_V_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_TotalQuestions")).Value, CultureInfo.CurrentCulture);
          for (int a = 1; a <= HiddenField_V_TotalQuestions; a++)
          {
            if (a < 10)
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form12.FindControl("RadioButtonList_InsertV_V0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form12Total = Form12Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form12Total = Form12Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
            else
            {
              if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form12.FindControl("RadioButtonList_InsertV_V" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
              {
                Form12Total = Form12Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
              }
              else
              {
                Form12Total = Form12Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
              }
            }
          }

          SqlDataSource_Isidima_Form12.InsertParameters["Isidima_Section_V_Total"].DefaultValue = Form12Total.ToString(CultureInfo.CurrentCulture);
        }
      }
    }

    protected void DetailsView_Isidima_Form12_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDIsidimaSectionVModifiedDate"] = e.OldValues["Isidima_Section_V_ModifiedDate"];
        object OLDIsidimaSectionVModifiedDate = Session["OLDIsidimaSectionVModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDIsidimaSectionVModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm12 = (DataView)SqlDataSource_Isidima_Form12.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm12 = DataView_CompareForm12[0];
        Session["DBIsidimaSectionVModifiedDate"] = Convert.ToString(DataRowView_CompareForm12["Isidima_Section_V_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBIsidimaSectionVModifiedBy"] = Convert.ToString(DataRowView_CompareForm12["Isidima_Section_V_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBIsidimaSectionVModifiedDate = Session["DBIsidimaSectionVModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBIsidimaSectionVModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          ((Label)DetailsView_Isidima_Form12.FindControl("Label_ConcurrencyUpdate")).Visible = true;
          ((Label)DetailsView_Isidima_Form12.FindControl("Label_ConcurrencyUpdate")).Text = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBIsidimaSectionVModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b012612e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);
        }
        else if (OLDModifiedDateNew == DBModifiedDateNew)
        {
          if (e.Cancel == false)
          {
            e.NewValues["Isidima_Section_V_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Isidima_Section_V_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Section_V", "Isidima_Section_V_Id = " + Request.QueryString["Isidima_Section_V_Id"]);

            DataView DataView_Isidima_Form12 = (DataView)SqlDataSource_Isidima_Form12.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Isidima_Form12 = DataView_Isidima_Form12[0];
            Session["IsidimaSectionVHistory"] = Convert.ToString(DataRowView_Isidima_Form12["Isidima_Section_V_History"], CultureInfo.CurrentCulture);

            Session["IsidimaSectionVHistory"] = Session["History"].ToString() + Session["IsidimaSectionVHistory"].ToString();
            e.NewValues["Isidima_Section_V_History"] = Session["IsidimaSectionVHistory"].ToString();

            Session["IsidimaSectionVHistory"] = "";
            Session["History"] = "";


            int Form12Total = 0;
            int HiddenField_V_TotalQuestions = Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_TotalQuestions")).Value, CultureInfo.CurrentCulture);
            for (int a = 1; a <= HiddenField_V_TotalQuestions; a++)
            {
              if (a < 10)
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form12.FindControl("RadioButtonList_EditV_V0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form12Total = Form12Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form12Total = Form12Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V0" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
              else
              {
                if (Convert.ToString(((RadioButtonList)DetailsView_Isidima_Form12.FindControl("RadioButtonList_EditV_V" + Convert.ToString(a, CultureInfo.CurrentCulture) + "")).SelectedValue, CultureInfo.CurrentCulture) == "Yes")
                {
                  Form12Total = Form12Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V" + Convert.ToString(a, CultureInfo.CurrentCulture) + "Yes")).Value, CultureInfo.CurrentCulture);
                }
                else
                {
                  Form12Total = Form12Total + Convert.ToInt32(((HiddenField)DetailsView_Isidima_Form12.FindControl("HiddenField_V_V" + Convert.ToString(a, CultureInfo.CurrentCulture) + "No")).Value, CultureInfo.CurrentCulture);
                }
              }
            }

            e.NewValues["Isidima_Section_V_Total"] = Form12Total.ToString(CultureInfo.CurrentCulture);
          }
        }

        Session["OLDIsidimaSectionVModifiedDate"] = "";
        Session["DBIsidimaSectionVModifiedDate"] = "";
        Session["DBIsidimaSectionVModifiedBy"] = "";
      }
    }
    //---END--- --Form12--//
  }
}