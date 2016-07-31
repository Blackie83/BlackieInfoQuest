using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace InfoQuestForm
{
  public partial class Form_Isidima_Rules : InfoQuestWCF.Override_SystemWebUIPage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        SqlDataSourceSetup();

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          Label_Title.Text = Convert.ToString((InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString() + " : Rules", CultureInfo.CurrentCulture);
          Label_SearchHeading.Text = Convert.ToString("Search " + (InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString() + " : Rules", CultureInfo.CurrentCulture);
          Label_GridHeading.Text = Convert.ToString("List of " + (InfoQuestWCF.InfoQuest_All.All_FormName("27").Replace(" Form", "")).ToString() + " : Rules", CultureInfo.CurrentCulture);

          SetFormQueryString();

          if (!string.IsNullOrEmpty(Request.QueryString["s_Section_Id"]))
          {
            TableRules.Visible = true;
          }
          else
          {
            TableRules.Visible = false;
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

        string SQLStringSecurity = "SELECT DISTINCT SecurityUser_UserName FROM vAdministration_SecurityAccess_Active WHERE (SecurityUser_UserName = @SecurityUser_UserName) AND (Form_Id IN ('27')) AND (SecurityRole_Id = 86)";
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
      InfoQuestWCF.InfoQuest_All.All_Maintenance("27");

      if (PageSecurity() == "1")
      {
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Isidima_Rules.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();
      }
    }

    private void SqlDataSourceSetup()
    {
      SqlDataSource_Isidima_Rules_Section.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Rules_Section.SelectCommand = "spAdministration_Execute_List";
      SqlDataSource_Isidima_Rules_Section.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
      SqlDataSource_Isidima_Rules_Section.SelectParameters.Clear();
      SqlDataSource_Isidima_Rules_Section.SelectParameters.Add("Form_Id", TypeCode.String, "27");
      SqlDataSource_Isidima_Rules_Section.SelectParameters.Add("ListCategory_Id", TypeCode.Int32, "66");
      SqlDataSource_Isidima_Rules_Section.SelectParameters.Add("ListItem_Parent", TypeCode.Int32, "-1");
      SqlDataSource_Isidima_Rules_Section.SelectParameters.Add("TableSELECT", TypeCode.String, "0");
      SqlDataSource_Isidima_Rules_Section.SelectParameters.Add("TableFROM", TypeCode.String, "0");
      SqlDataSource_Isidima_Rules_Section.SelectParameters.Add("TableWHERE", TypeCode.String, "0");
      
      SqlDataSource_Isidima_Rules.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");
      SqlDataSource_Isidima_Rules.InsertCommand="INSERT INTO Form_Isidima_Rules ( Isidima_Rules_Section_List ,Isidima_Rules_QuestionId ,Isidima_Rules_Question ,Isidima_Rules_Question_YesWeight ,Isidima_Rules_Question_NoWeight ,Isidima_Rules_ProfessionalAction ,Isidima_Rules_ActionTaken ,Isidima_Rules_CategoryResponsible ,Isidima_Rules_MaterialAvailable ,Isidima_Rules_Risk ,Isidima_Rules_CreatedDate ,Isidima_Rules_CreatedBy ,Isidima_Rules_ModifiedDate ,Isidima_Rules_ModifiedBy ,Isidima_Rules_History ,Isidima_Rules_IsActive ) VALUES ( @Isidima_Rules_Section_List ,@Isidima_Rules_QuestionId ,@Isidima_Rules_Question ,@Isidima_Rules_Question_YesWeight ,@Isidima_Rules_Question_NoWeight ,@Isidima_Rules_ProfessionalAction ,@Isidima_Rules_ActionTaken ,@Isidima_Rules_CategoryResponsible ,@Isidima_Rules_MaterialAvailable ,@Isidima_Rules_Risk ,@Isidima_Rules_CreatedDate ,@Isidima_Rules_CreatedBy ,@Isidima_Rules_ModifiedDate ,@Isidima_Rules_ModifiedBy ,@Isidima_Rules_History ,@Isidima_Rules_IsActive )";
      SqlDataSource_Isidima_Rules.SelectCommand="SELECT * FROM Form_Isidima_Rules WHERE Isidima_Rules_Section_List = @Isidima_Rules_Section_List ORDER BY Isidima_Rules_Section_List , Isidima_Rules_QuestionId";
      SqlDataSource_Isidima_Rules.UpdateCommand="UPDATE Form_Isidima_Rules SET Isidima_Rules_Question = @Isidima_Rules_Question ,Isidima_Rules_Question_YesWeight = @Isidima_Rules_Question_YesWeight ,Isidima_Rules_Question_NoWeight = @Isidima_Rules_Question_NoWeight ,Isidima_Rules_ProfessionalAction = @Isidima_Rules_ProfessionalAction ,Isidima_Rules_ActionTaken = @Isidima_Rules_ActionTaken ,Isidima_Rules_CategoryResponsible = @Isidima_Rules_CategoryResponsible ,Isidima_Rules_MaterialAvailable = @Isidima_Rules_MaterialAvailable ,Isidima_Rules_Risk = @Isidima_Rules_Risk ,Isidima_Rules_ModifiedDate = @Isidima_Rules_ModifiedDate ,Isidima_Rules_ModifiedBy = @Isidima_Rules_ModifiedBy ,Isidima_Rules_History = @Isidima_Rules_History ,Isidima_Rules_IsActive = @Isidima_Rules_IsActive WHERE Isidima_Rules_Id = @Isidima_Rules_Id";
      SqlDataSource_Isidima_Rules.InsertParameters.Clear();
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_Section_List", TypeCode.Int32, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_QuestionId", TypeCode.Int32, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_Question", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_Question_YesWeight", TypeCode.Int32, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_Question_NoWeight", TypeCode.Int32, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_ProfessionalAction", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_ActionTaken", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_CategoryResponsible", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_MaterialAvailable", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_Risk", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_CreatedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_CreatedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_History", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_History"].ConvertEmptyStringToNull = true;
      SqlDataSource_Isidima_Rules.InsertParameters.Add("Isidima_Rules_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Isidima_Rules.SelectParameters.Clear();
      SqlDataSource_Isidima_Rules.SelectParameters.Add("Isidima_Rules_Section_List", TypeCode.Int32, Request.QueryString["s_Section_Id"]);
      SqlDataSource_Isidima_Rules.UpdateParameters.Clear();
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_Question", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_Question_YesWeight", TypeCode.Int32, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_Question_NoWeight", TypeCode.Int32, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_ProfessionalAction", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_ActionTaken", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_CategoryResponsible", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_MaterialAvailable", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_Risk", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_ModifiedDate", TypeCode.DateTime, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_ModifiedBy", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_History", TypeCode.String, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_IsActive", TypeCode.Boolean, "");
      SqlDataSource_Isidima_Rules.UpdateParameters.Add("Isidima_Rules_Id", TypeCode.Int32, "");
    }

    private void SetFormQueryString()
    {
      if (string.IsNullOrEmpty(DropDownList_Section.SelectedValue.ToString()))
      {
        if (string.IsNullOrEmpty(Request.QueryString["s_Section_Id"]))
        {
          DropDownList_Section.SelectedValue = "";
        }
        else
        {
          DropDownList_Section.SelectedValue = Request.QueryString["s_Section_Id"];
        }
      }
    }


    //--START-- --Search--//
    protected void Button_Search_Click(object sender, EventArgs e)
    {
      string SearchField1 = DropDownList_Section.SelectedValue;

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Section_Id=" + DropDownList_Section.SelectedValue.ToString() + "&";
      }

      string FinalURL = "Form_Isidima_Rules.aspx?" + SearchField1;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Isidima Rules", FinalURL);

      Response.Redirect(FinalURL, false);
    }

    protected void Button_Clear_Click(object sender, EventArgs e)
    {
      string FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Isidima Rules", "Form_Isidima_Rules.aspx");
      Response.Redirect(FinalURL, false);
    }
    //---END--- --Search--//


    //--START-- --List--//
    protected void SqlDataSource_Isidima_Rules_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Label_TotalRecords.Text = e.AffectedRows.ToString(CultureInfo.CurrentCulture);
      }
    }

    protected void GridView_Isidima_Rules_PreRender(object sender, EventArgs e)
    {
      GridView GridView_List = (GridView)sender;
      GridViewRow GridViewRow_List = (GridViewRow)GridView_List.BottomPagerRow;
      if (GridViewRow_List != null)
      {
        GridViewRow_List.Visible = true;
      }
    }

    protected void Label_EditSectionList_DataBinding(object sender, EventArgs e)
    {
      Label Label_EditSection_List = (Label)sender;

      string ListItemName = "";
      string SQLStringSection = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Id = @ListItem_Id";
      using (SqlCommand SqlCommand_Section = new SqlCommand(SQLStringSection))
      {
        SqlCommand_Section.Parameters.AddWithValue("@ListItem_Id", Request.QueryString["s_Section_Id"]);
        DataTable DataTable_Section;
        using (DataTable_Section = new DataTable())
        {
          DataTable_Section.Locale = CultureInfo.CurrentCulture;
          DataTable_Section = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Section).Copy();
          if (DataTable_Section.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Section.Rows)
            {
              ListItemName = DataRow_Row["ListItem_Name"].ToString();
            }
          }
        }
      }

      Label_EditSection_List.Text = ListItemName;
    }

    protected void Label_InsertSectionList_DataBinding(object sender, EventArgs e)
    {
      Label Label_InsertSection_List = (Label)sender;

      string ListItemName = "";
      string SQLStringSection = "SELECT ListItem_Name FROM Administration_ListItem WHERE ListItem_Id = @ListItem_Id";
      using (SqlCommand SqlCommand_Section = new SqlCommand(SQLStringSection))
      {
        SqlCommand_Section.Parameters.AddWithValue("@ListItem_Id", Request.QueryString["s_Section_Id"]);
        DataTable DataTable_Section;
        using (DataTable_Section = new DataTable())
        {
          DataTable_Section.Locale = CultureInfo.CurrentCulture;
          DataTable_Section = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Section).Copy();
          if (DataTable_Section.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Section.Rows)
            {
              ListItemName = DataRow_Row["ListItem_Name"].ToString();
            }
          }
        }
      }

      Label_InsertSection_List.Text = ListItemName;
    }

    protected void Label_InsertQuestionId_DataBinding(object sender, EventArgs e)
    {
      Label Label_InsertQuestionId = (Label)sender;

      Int32 TotalRecords = Convert.ToInt32(Label_TotalRecords.Text, CultureInfo.CurrentCulture);
      TotalRecords = TotalRecords + 1;

      Label_InsertQuestionId.Text = TotalRecords.ToString(CultureInfo.CurrentCulture);
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
      Button Button_Update = (Button)sender;
      GridViewRow GridViewRow_Isidima_Rules = (GridViewRow)Button_Update.NamingContainer;

      Label Label_EditValidationMessage = (Label)GridViewRow_Isidima_Rules.FindControl("Label_EditValidationMessage");
      TextBox TextBox_EditQuestion = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_EditQuestion");
      TextBox TextBox_EditQuestionYesWeight = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_EditQuestionYesWeight");
      TextBox TextBox_EditQuestionNoWeight = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_EditQuestionNoWeight");
      TextBox TextBox_EditProfessionalAction = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_EditProfessionalAction");
      TextBox TextBox_EditActionTaken = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_EditActionTaken");
      TextBox TextBox_EditCategoryResponsible = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_EditCategoryResponsible");
      TextBox TextBox_EditMaterialAvailable = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_EditMaterialAvailable");
      TextBox TextBox_EditRisk = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_EditMaterialAvailable");
      CheckBox CheckBox_EditIsActive = (CheckBox)GridViewRow_Isidima_Rules.FindControl("CheckBox_EditIsActive");
      HiddenField HiddenField_EditRulesId = (HiddenField)GridViewRow_Isidima_Rules.FindControl("HiddenField_EditRulesId");

      if (string.IsNullOrEmpty(TextBox_EditQuestion.Text) || string.IsNullOrEmpty(TextBox_EditQuestionYesWeight.Text) || string.IsNullOrEmpty(TextBox_EditQuestionNoWeight.Text))
      {
        Label_EditValidationMessage.Visible = true;
      }
      else
      {
        Label_EditValidationMessage.Visible = false;

        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_Question"].DefaultValue = Server.HtmlEncode(TextBox_EditQuestion.Text);
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_Question_YesWeight"].DefaultValue = Server.HtmlEncode(TextBox_EditQuestionYesWeight.Text);
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_Question_NoWeight"].DefaultValue = Server.HtmlEncode(TextBox_EditQuestionNoWeight.Text);
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_ProfessionalAction"].DefaultValue = Server.HtmlEncode(TextBox_EditProfessionalAction.Text);
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_ActionTaken"].DefaultValue = Server.HtmlEncode(TextBox_EditActionTaken.Text);
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_CategoryResponsible"].DefaultValue = Server.HtmlEncode(TextBox_EditCategoryResponsible.Text);
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_MaterialAvailable"].DefaultValue = Server.HtmlEncode(TextBox_EditMaterialAvailable.Text);
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_Risk"].DefaultValue = Server.HtmlEncode(TextBox_EditRisk.Text);
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];

        Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("vForm_Isidima_Rules", "Isidima_Rules_Id = " + HiddenField_EditRulesId.Value.ToString());
        DataView DataView_Isidima_Rules = (DataView)SqlDataSource_Isidima_Rules.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_Isidima_Rules = DataView_Isidima_Rules[0];
        Session["IsidimaRulesHistory"] = Convert.ToString(DataRowView_Isidima_Rules["Isidima_Rules_History"], CultureInfo.CurrentCulture);
        Session["IsidimaRulesHistory"] = Session["History"].ToString() + Session["IsidimaRulesHistory"].ToString();
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_History"].DefaultValue = Session["IsidimaRulesHistory"].ToString();
        Session["IsidimaRulesHistory"] = "";
        Session["History"] = "";

        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_IsActive"].DefaultValue = CheckBox_EditIsActive.Checked.ToString();
        SqlDataSource_Isidima_Rules.UpdateParameters["Isidima_Rules_Id"].DefaultValue = HiddenField_EditRulesId.Value;
        SqlDataSource_Isidima_Rules.Update();
      }
    }

    protected void Button_Insert_Click(object sender, EventArgs e)
    {
      Button Button_Insert = (Button)sender;
      GridViewRow GridViewRow_Isidima_Rules = (GridViewRow)Button_Insert.NamingContainer;

      Label Label_InsertValidationMessage = (Label)GridViewRow_Isidima_Rules.FindControl("Label_InsertValidationMessage");
      Label Label_InsertQuestionId = (Label)GridViewRow_Isidima_Rules.FindControl("Label_InsertQuestionId");
      TextBox TextBox_InsertQuestion = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_InsertQuestion");
      TextBox TextBox_InsertQuestionYesWeight = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_InsertQuestionYesWeight");
      TextBox TextBox_InsertQuestionNoWeight = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_InsertQuestionNoWeight");
      TextBox TextBox_InsertProfessionalAction = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_InsertProfessionalAction");
      TextBox TextBox_InsertActionTaken = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_InsertActionTaken");
      TextBox TextBox_InsertCategoryResponsible = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_InsertCategoryResponsible");
      TextBox TextBox_InsertMaterialAvailable = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_InsertMaterialAvailable");
      TextBox TextBox_InsertRisk = (TextBox)GridViewRow_Isidima_Rules.FindControl("TextBox_InsertMaterialAvailable");

      if (string.IsNullOrEmpty(TextBox_InsertQuestion.Text) || string.IsNullOrEmpty(TextBox_InsertQuestionYesWeight.Text) || string.IsNullOrEmpty(TextBox_InsertQuestionNoWeight.Text))
      {
        Label_InsertValidationMessage.Visible = true;
      }
      else
      {
        Label_InsertValidationMessage.Visible = false;

        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_Section_List"].DefaultValue = Request.QueryString["s_Section_Id"];
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_QuestionId"].DefaultValue = Label_InsertQuestionId.Text;
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_Question"].DefaultValue = Server.HtmlEncode(TextBox_InsertQuestion.Text);
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_Question_YesWeight"].DefaultValue = Server.HtmlEncode(TextBox_InsertQuestionYesWeight.Text);
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_Question_NoWeight"].DefaultValue = Server.HtmlEncode(TextBox_InsertQuestionNoWeight.Text);
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_ProfessionalAction"].DefaultValue = Server.HtmlEncode(TextBox_InsertProfessionalAction.Text);
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_ActionTaken"].DefaultValue = Server.HtmlEncode(TextBox_InsertActionTaken.Text);
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_CategoryResponsible"].DefaultValue = Server.HtmlEncode(TextBox_InsertCategoryResponsible.Text);
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_MaterialAvailable"].DefaultValue = Server.HtmlEncode(TextBox_InsertMaterialAvailable.Text);
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_Risk"].DefaultValue = Server.HtmlEncode(TextBox_InsertRisk.Text);
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_CreatedDate"].DefaultValue = DateTime.Now.ToString();
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_History"].DefaultValue = "";
        SqlDataSource_Isidima_Rules.InsertParameters["Isidima_Rules_IsActive"].DefaultValue = "true";

        SqlDataSource_Isidima_Rules.Insert();
      }
    }
    //---END--- --List--//    
  }
}