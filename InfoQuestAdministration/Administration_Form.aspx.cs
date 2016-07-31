using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_Form : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_Form_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_Form, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["Form_Id"] != null)
          {
            TableForm.Visible = true;

            SetFormVisibility();
          }
          else
          {
            TableForm.Visible = true;
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_Form.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["Form_Id"] != null)
      {
        FormView_Form_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_Form_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_Form_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_Form_Form.FindControl("TextBox_InsertName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Form_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Form_Form.FindControl("TextBox_InsertReportNumberIdentifier")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_Form_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_Form_Form.FindControl("TextBox_EditName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Form_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_Form_Form.FindControl("TextBox_EditReportNumberIdentifier")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_FormId"];
      string SearchField2 = Request.QueryString["Search_FormIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_Form_Id=" + Request.QueryString["Search_FormId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_Form_IsActive=" + Request.QueryString["Search_FormIsActive"] + "&";
      }

      string FinalURL = "Administration_Form_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_Form_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_Form_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          TextBox TextBox_InsertReportNumberIdentifier = (TextBox)FormView_Form_Form.FindControl("TextBox_InsertReportNumberIdentifier");
          SqlDataSource_Form_Form.InsertParameters["Form_ReportNumberIdentifier"].DefaultValue = TextBox_InsertReportNumberIdentifier.Text.ToString().ToUpper(CultureInfo.CurrentCulture).Replace(" ", "");

          SqlDataSource_Form_Form.InsertParameters["Form_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Form_Form.InsertParameters["Form_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Form_Form.InsertParameters["Form_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_Form_Form.InsertParameters["Form_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_Form_Form.InsertParameters["Form_History"].DefaultValue = "";
          SqlDataSource_Form_Form.InsertParameters["Form_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertName = (TextBox)FormView_Form_Form.FindControl("TextBox_InsertName");
      TextBox TextBox_InsertDescription = (TextBox)FormView_Form_Form.FindControl("TextBox_InsertDescription");
      TextBox TextBox_InsertReportNumberIdentifier = (TextBox)FormView_Form_Form.FindControl("TextBox_InsertReportNumberIdentifier");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertReportNumberIdentifier.Text))
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
        Session["FormName"] = "";
        string SQLStringFormName = "SELECT Form_Name FROM Administration_Form WHERE Form_Name = @Form_Name AND Form_IsActive = @Form_IsActive";
        using (SqlCommand SqlCommand_FormName = new SqlCommand(SQLStringFormName))
        {
          SqlCommand_FormName.Parameters.AddWithValue("@Form_Name", TextBox_InsertName.Text.ToString());
          SqlCommand_FormName.Parameters.AddWithValue("@Form_IsActive", 1);
          DataTable DataTable_FormName;
          using (DataTable_FormName = new DataTable())
          {
            DataTable_FormName.Locale = CultureInfo.CurrentCulture;
            DataTable_FormName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormName).Copy();
            if (DataTable_FormName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FormName.Rows)
              {
                Session["FormName"] = DataRow_Row["Form_Name"];
              }
            }
            else
            {
              Session["FormName"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FormName"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Form with the Name '" + Session["FormName"].ToString() + "' already exists<br />";
        }

        Session["FormName"] = "";


        Session["FormReportNumberIdentifier"] = "";
        string SQLStringFormReportNumberIdentifier = "SELECT Form_ReportNumberIdentifier FROM Administration_Form WHERE Form_ReportNumberIdentifier = @Form_ReportNumberIdentifier AND Form_IsActive = @Form_IsActive";
        using (SqlCommand SqlCommand_FormReportNumberIdentifier = new SqlCommand(SQLStringFormReportNumberIdentifier))
        {
          SqlCommand_FormReportNumberIdentifier.Parameters.AddWithValue("@Form_ReportNumberIdentifier", TextBox_InsertReportNumberIdentifier.Text.ToString());
          SqlCommand_FormReportNumberIdentifier.Parameters.AddWithValue("@Form_IsActive", 1);
          DataTable DataTable_FormReportNumberIdentifier;
          using (DataTable_FormReportNumberIdentifier = new DataTable())
          {
            DataTable_FormReportNumberIdentifier.Locale = CultureInfo.CurrentCulture;
            DataTable_FormReportNumberIdentifier = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormReportNumberIdentifier).Copy();
            if (DataTable_FormReportNumberIdentifier.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FormReportNumberIdentifier.Rows)
              {
                Session["FormReportNumberIdentifier"] = DataRow_Row["Form_ReportNumberIdentifier"];
              }
            }
            else
            {
              Session["FormReportNumberIdentifier"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FormReportNumberIdentifier"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A Report Number Identifier with the Name '" + Session["FormReportNumberIdentifier"].ToString() + "' already exists<br />";
        }

        Session["FormReportNumberIdentifier"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Form_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["Form_Id"] = e.Command.Parameters["@Form_Id"].Value;

        string SearchField1 = "s_Form_Id=" + Session["Form_Id"].ToString() + "";
        string FinalURL = "Administration_Form_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Form List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_Form_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDFormModifiedDate"] = e.OldValues["Form_ModifiedDate"];
        object OLDFormModifiedDate = Session["OLDFormModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDFormModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareForm = (DataView)SqlDataSource_Form_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareForm = DataView_CompareForm[0];
        Session["DBFormModifiedDate"] = Convert.ToString(DataRowView_CompareForm["Form_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBFormModifiedBy"] = Convert.ToString(DataRowView_CompareForm["Form_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBFormModifiedDate = Session["DBFormModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBFormModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBFormModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_Form_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_Form_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            Page.MaintainScrollPositionOnPostBack = false;
            ((Label)FormView_Form_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_Form_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            TextBox TextBox_EditReportNumberIdentifier = (TextBox)FormView_Form_Form.FindControl("TextBox_EditReportNumberIdentifier");
            e.NewValues["Form_ReportNumberIdentifier"] = TextBox_EditReportNumberIdentifier.Text.ToString().ToUpper(CultureInfo.CurrentCulture).Replace(" ", "");

            e.NewValues["Form_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["Form_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_Form", "Form_Id = " + Request.QueryString["Form_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_Form_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["FormHistory"] = Convert.ToString(DataRowView_Form["Form_History"], CultureInfo.CurrentCulture);

            Session["FormHistory"] = Session["History"].ToString() + Session["FormHistory"].ToString();
            e.NewValues["Form_History"] = Session["FormHistory"].ToString();

            Session["FormHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDFormModifiedDate"] = "";
        Session["DBFormModifiedDate"] = "";
        Session["DBFormModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditName = (TextBox)FormView_Form_Form.FindControl("TextBox_EditName");
      TextBox TextBox_EditDescription = (TextBox)FormView_Form_Form.FindControl("TextBox_EditDescription");
      TextBox TextBox_EditReportNumberIdentifier = (TextBox)FormView_Form_Form.FindControl("TextBox_EditReportNumberIdentifier");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditReportNumberIdentifier.Text))
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
        Session["FormId"] = "";
        Session["FormName"] = "";
        string SQLStringFormName = "SELECT Form_Id , Form_Name FROM Administration_Form WHERE Form_Name = @Form_Name AND Form_IsActive = @Form_IsActive";
        using (SqlCommand SqlCommand_FormName = new SqlCommand(SQLStringFormName))
        {
          SqlCommand_FormName.Parameters.AddWithValue("@Form_Name", TextBox_EditName.Text.ToString());
          SqlCommand_FormName.Parameters.AddWithValue("@Form_IsActive", 1);
          DataTable DataTable_FormName;
          using (DataTable_FormName = new DataTable())
          {
            DataTable_FormName.Locale = CultureInfo.CurrentCulture;
            DataTable_FormName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormName).Copy();
            if (DataTable_FormName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FormName.Rows)
              {
                Session["FormId"] = DataRow_Row["Form_Id"];
                Session["FormName"] = DataRow_Row["Form_Name"];
              }
            }
            else
            {
              Session["FormId"] = "";
              Session["FormName"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FormName"].ToString()))
        {
          if (Session["FormId"].ToString() != Request.QueryString["Form_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Form with the Name '" + Session["FormName"].ToString() + "' already exists<br />";
          }
        }

        Session["FormId"] = "";
        Session["FormName"] = "";


        Session["FormId"] = "";
        Session["FormReportNumberIdentifier"] = "";
        string SQLStringFormReportNumberIdentifier = "SELECT Form_Id , Form_ReportNumberIdentifier FROM Administration_Form WHERE Form_ReportNumberIdentifier = @Form_ReportNumberIdentifier AND Form_IsActive = @Form_IsActive";
        using (SqlCommand SqlCommand_FormReportNumberIdentifier = new SqlCommand(SQLStringFormReportNumberIdentifier))
        {
          SqlCommand_FormReportNumberIdentifier.Parameters.AddWithValue("@Form_ReportNumberIdentifier", TextBox_EditReportNumberIdentifier.Text.ToString());
          SqlCommand_FormReportNumberIdentifier.Parameters.AddWithValue("@Form_IsActive", 1);
          DataTable DataTable_FormReportNumberIdentifier;
          using (DataTable_FormReportNumberIdentifier = new DataTable())
          {
            DataTable_FormReportNumberIdentifier.Locale = CultureInfo.CurrentCulture;
            DataTable_FormReportNumberIdentifier = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_FormReportNumberIdentifier).Copy();
            if (DataTable_FormReportNumberIdentifier.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_FormReportNumberIdentifier.Rows)
              {
                Session["FormId"] = DataRow_Row["Form_Id"];
                Session["FormReportNumberIdentifier"] = DataRow_Row["Form_ReportNumberIdentifier"];
              }
            }
            else
            {
              Session["FormId"] = "";
              Session["FormReportNumberIdentifier"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["FormReportNumberIdentifier"].ToString()))
        {
          if (Session["FormId"].ToString() != Request.QueryString["Form_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Report Number Identifier with the Name '" + Session["FormReportNumberIdentifier"].ToString() + "' already exists<br />";
          }
        }

        Session["FormId"] = "";
        Session["FormReportNumberIdentifier"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_Form_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_Form_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    protected void TextBox_InsertName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertName = (TextBox)FormView_Form_Form.FindControl("TextBox_InsertName");
      Label Label_InsertNameError = (Label)FormView_Form_Form.FindControl("Label_InsertNameError");

      Session["FormName"] = "";
      string SQLStringForm = "SELECT Form_Name FROM Administration_Form WHERE Form_Name = @Form_Name AND Form_IsActive = @Form_IsActive";
      using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
      {
        SqlCommand_Form.Parameters.AddWithValue("@Form_Name", TextBox_InsertName.Text.ToString());
        SqlCommand_Form.Parameters.AddWithValue("@Form_IsActive", 1);
        DataTable DataTable_Form;
        using (DataTable_Form = new DataTable())
        {
          DataTable_Form.Locale = CultureInfo.CurrentCulture;

          DataTable_Form = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
          if (DataTable_Form.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Form.Rows)
            {
              Session["FormName"] = DataRow_Row["Form_Name"];
            }
          }
          else
          {
            Session["FormName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FormName"].ToString()))
      {
        Label_InsertNameError.Text = "";
      }
      else
      {
        Label_InsertNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Form with the Name '" + Session["FormName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["FormName"] = "";
    }

    protected void TextBox_InsertReportNumberIdentifier_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertReportNumberIdentifier = (TextBox)FormView_Form_Form.FindControl("TextBox_InsertReportNumberIdentifier");
      Label Label_InsertReportNumberIdentifierError = (Label)FormView_Form_Form.FindControl("Label_InsertReportNumberIdentifierError");

      Session["FormReportNumberIdentifier"] = "";
      string SQLStringForm = "SELECT Form_ReportNumberIdentifier FROM Administration_Form WHERE Form_ReportNumberIdentifier = @Form_ReportNumberIdentifier AND Form_IsActive = @Form_IsActive";
      using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
      {
        SqlCommand_Form.Parameters.AddWithValue("@Form_ReportNumberIdentifier", TextBox_InsertReportNumberIdentifier.Text.ToString());
        SqlCommand_Form.Parameters.AddWithValue("@Form_IsActive", 1);
        DataTable DataTable_Form;
        using (DataTable_Form = new DataTable())
        {
          DataTable_Form.Locale = CultureInfo.CurrentCulture;

          DataTable_Form = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
          if (DataTable_Form.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Form.Rows)
            {
              Session["FormReportNumberIdentifier"] = DataRow_Row["Form_ReportNumberIdentifier"];
            }
          }
          else
          {
            Session["FormReportNumberIdentifier"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FormReportNumberIdentifier"].ToString()))
      {
        Label_InsertReportNumberIdentifierError.Text = "";
      }
      else
      {
        Label_InsertReportNumberIdentifierError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Report Number Identifier with the Name '" + Session["FormReportNumberIdentifier"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["FormReportNumberIdentifier"] = "";
    }

    protected void TextBox_EditName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditName = (TextBox)FormView_Form_Form.FindControl("TextBox_EditName");
      Label Label_EditNameError = (Label)FormView_Form_Form.FindControl("Label_EditNameError");

      Session["FormId"] = "";
      Session["FormName"] = "";
      string SQLStringForm = "SELECT Form_Id , Form_Name FROM Administration_Form WHERE Form_Name = @Form_Name AND Form_IsActive = @Form_IsActive";
      using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
      {
        SqlCommand_Form.Parameters.AddWithValue("@Form_Name", TextBox_EditName.Text.ToString());
        SqlCommand_Form.Parameters.AddWithValue("@Form_IsActive", 1);
        DataTable DataTable_Form;
        using (DataTable_Form = new DataTable())
        {
          DataTable_Form.Locale = CultureInfo.CurrentCulture;

          DataTable_Form = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
          if (DataTable_Form.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Form.Rows)
            {
              Session["FormId"] = DataRow_Row["Form_Id"];
              Session["FormName"] = DataRow_Row["Form_Name"];
            }
          }
          else
          {
            Session["FormId"] = "";
            Session["FormName"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FormName"].ToString()))
      {
        Label_EditNameError.Text = "";
      }
      else
      {
        if (Session["FormId"].ToString() == Request.QueryString["Form_Id"])
        {
          Label_EditNameError.Text = "";
        }
        else
        {
          Label_EditNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Form with the Name '" + Session["FormName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["FormId"] = "";
      Session["FormName"] = "";
    }

    protected void TextBox_EditReportNumberIdentifier_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditReportNumberIdentifier = (TextBox)FormView_Form_Form.FindControl("TextBox_EditReportNumberIdentifier");
      Label Label_EditReportNumberIdentifierError = (Label)FormView_Form_Form.FindControl("Label_EditReportNumberIdentifierError");

      Session["FormId"] = "";
      Session["FormReportNumberIdentifier"] = "";
      string SQLStringForm = "SELECT Form_Id , Form_ReportNumberIdentifier FROM Administration_Form WHERE Form_ReportNumberIdentifier = @Form_ReportNumberIdentifier AND Form_IsActive = @Form_IsActive";
      using (SqlCommand SqlCommand_Form = new SqlCommand(SQLStringForm))
      {
        SqlCommand_Form.Parameters.AddWithValue("@Form_ReportNumberIdentifier", TextBox_EditReportNumberIdentifier.Text.ToString());
        SqlCommand_Form.Parameters.AddWithValue("@Form_IsActive", 1);
        DataTable DataTable_Form;
        using (DataTable_Form = new DataTable())
        {
          DataTable_Form.Locale = CultureInfo.CurrentCulture;

          DataTable_Form = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_Form).Copy();
          if (DataTable_Form.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_Form.Rows)
            {
              Session["FormId"] = DataRow_Row["Form_Id"];
              Session["FormReportNumberIdentifier"] = DataRow_Row["Form_ReportNumberIdentifier"];
            }
          }
          else
          {
            Session["FormId"] = "";
            Session["FormReportNumberIdentifier"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["FormReportNumberIdentifier"].ToString()))
      {
        Label_EditReportNumberIdentifierError.Text = "";
      }
      else
      {
        if (Session["FormId"].ToString() == Request.QueryString["Form_Id"])
        {
          Label_EditReportNumberIdentifierError.Text = "";
        }
        else
        {
          Label_EditReportNumberIdentifierError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Report Number Identifier with the Name '" + Session["FormReportNumberIdentifier"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["FormId"] = "";
      Session["FormReportNumberIdentifier"] = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}