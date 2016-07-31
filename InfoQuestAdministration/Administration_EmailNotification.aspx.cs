using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_EmailNotification : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_EmailNotification_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_EmailNotification, GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["EmailNotification_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_EmailNotification.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["EmailNotification_Id"] != null)
      {
        FormView_EmailNotification_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_EmailNotification_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_EmailNotification_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_InsertAssembly")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_InsertMethod")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_InsertEmail")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_EmailNotification_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_EditAssembly")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_EditMethod")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_EditEmail")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_EmailNotificationAssembly"];
      string SearchField2 = Request.QueryString["Search_EmailNotificationMethod"];
      string SearchField3 = Request.QueryString["Search_EmailNotificationEmail"];
      string SearchField4 = Request.QueryString["Search_EmailNotificationIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_EmailNotification_Assembly=" + Request.QueryString["Search_EmailNotificationAssembly"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_EmailNotification_Method=" + Request.QueryString["Search_EmailNotificationMethod"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_EmailNotification_Email=" + Request.QueryString["Search_EmailNotificationEmail"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_EmailNotification_IsActive=" + Request.QueryString["Search_EmailNotificationIsActive"] + "&";
      }

      string FinalURL = "Administration_EmailNotification_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Email Notification List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_EmailNotification_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_EmailNotification_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_EmailNotification_Form.InsertParameters["EmailNotification_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_EmailNotification_Form.InsertParameters["EmailNotification_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_EmailNotification_Form.InsertParameters["EmailNotification_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_EmailNotification_Form.InsertParameters["EmailNotification_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_EmailNotification_Form.InsertParameters["EmailNotification_History"].DefaultValue = "";
          SqlDataSource_EmailNotification_Form.InsertParameters["EmailNotification_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertAssembly = (TextBox)FormView_EmailNotification_Form.FindControl("TextBox_InsertAssembly");
      TextBox TextBox_InsertMethod = (TextBox)FormView_EmailNotification_Form.FindControl("TextBox_InsertMethod");
      TextBox TextBox_InsertEmail = (TextBox)FormView_EmailNotification_Form.FindControl("TextBox_InsertEmail");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertAssembly.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertMethod.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertEmail.Text))
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
        string EmailNotificationId = "";
        string SQLStringEmailNotification = "SELECT EmailNotification_Id FROM Administration_EmailNotification WHERE EmailNotification_Assembly = @EmailNotification_Assembly AND EmailNotification_Method = @EmailNotification_Method AND EmailNotification_Email = @EmailNotification_Email";
        using (SqlCommand SqlCommand_EmailNotification = new SqlCommand(SQLStringEmailNotification))
        {
          SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Assembly", TextBox_InsertAssembly.Text.ToString());
          SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Method", TextBox_InsertMethod.Text.ToString());
          SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Email", TextBox_InsertEmail.Text.ToString());
          DataTable DataTable_EmailNotification;
          using (DataTable_EmailNotification = new DataTable())
          {
            DataTable_EmailNotification.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailNotification = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailNotification).Copy();
            if (DataTable_EmailNotification.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_EmailNotification.Rows)
              {
                EmailNotificationId = DataRow_Row["EmailNotification_Id"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(EmailNotificationId))
        {
          InvalidFormMessage = InvalidFormMessage + "A Email Notification for Assembly: '" + TextBox_InsertAssembly.Text.ToString() + "', Method: '" + TextBox_InsertMethod.Text.ToString() + "' and Email: '" + TextBox_InsertEmail.Text.ToString() + "' already exists<br />";
        }

        EmailNotificationId = "";


        string EmailTextBox = ((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_InsertEmail")).Text.ToString();
        EmailTextBox = EmailTextBox.Replace(";", Convert.ToString(",", CultureInfo.CurrentCulture));
        EmailTextBox = EmailTextBox.Replace(":", Convert.ToString(",", CultureInfo.CurrentCulture));

        string EmailTextBoxSplit = EmailTextBox;
        string[] EmailTextBoxSplitEmails = EmailTextBoxSplit.Split(',');

        foreach (string EmailTextBoxSplitEmail in EmailTextBoxSplitEmails)
        {
          InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
          string ValidEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailTextBoxSplitEmail);

          if (ValidEmailAddress == "No")
          {
            InvalidFormMessage = InvalidFormMessage + "Invalid Email Address '" + EmailTextBoxSplitEmail + "'<br />";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_EmailNotification_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["EmailNotification_Id"] = e.Command.Parameters["@EmailNotification_Id"].Value;

        string EmailNotificationMethod = "";
        string EmailNotificationEmail = "";
        string SQLStringEmailNotification = "SELECT EmailNotification_Method , EmailNotification_Email FROM Administration_EmailNotification WHERE EmailNotification_Id = @EmailNotification_Id";
        using (SqlCommand SqlCommand_EmailNotification = new SqlCommand(SQLStringEmailNotification))
        {
          SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Id", Session["EmailNotification_Id"].ToString());
          DataTable DataTable_EmailNotification;
          using (DataTable_EmailNotification = new DataTable())
          {
            DataTable_EmailNotification.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailNotification = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailNotification).Copy();
            if (DataTable_EmailNotification.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_EmailNotification.Rows)
              {
                EmailNotificationMethod = DataRow_Row["EmailNotification_Method"].ToString();
                EmailNotificationEmail = DataRow_Row["EmailNotification_Email"].ToString();

                if (!string.IsNullOrEmpty(EmailNotificationMethod) && !string.IsNullOrEmpty(EmailNotificationEmail))
                {
                  string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("72");
                  string BodyString = EmailTemplate;

                  BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + EmailNotificationEmail + "");
                  BodyString = BodyString.Replace(";replace;Method;replace;", "" + EmailNotificationMethod + "");
                  BodyString = BodyString.Replace(";replace;Email;replace;", "" + EmailNotificationEmail + "");

                  string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                  string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                  string EmailBody = HeaderString + BodyString + FooterString;

                  string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailNotificationEmail, "InfoQuest Email Notification", EmailBody);

                  if (!string.IsNullOrEmpty(EmailSend))
                  {
                    EmailSend = "";
                  }

                  EmailTemplate = "";
                  BodyString = "";
                  HeaderString = "";
                  FooterString = "";
                  EmailBody = "";
                }
              }
            }
          }
        }

        EmailNotificationMethod = "";
        EmailNotificationEmail = "";

        Response.Redirect(InfoQuestWCF.InfoQuest_All.All_RedirectLink("Email Notification List", "Administration_EmailNotification_List.aspx?s_EmailNotification_Id=" + Session["EmailNotification_Id"].ToString() + ""), false);
      }
    }


    protected void FormView_EmailNotification_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDEmailNotificationModifiedDate"] = e.OldValues["EmailNotification_ModifiedDate"];
        object OLDEmailNotificationModifiedDate = Session["OLDEmailNotificationModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDEmailNotificationModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareEmailNotification = (DataView)SqlDataSource_EmailNotification_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareEmailNotification = DataView_CompareEmailNotification[0];
        Session["DBEmailNotificationModifiedDate"] = Convert.ToString(DataRowView_CompareEmailNotification["EmailNotification_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBEmailNotificationModifiedBy"] = Convert.ToString(DataRowView_CompareEmailNotification["EmailNotification_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBEmailNotificationModifiedDate = Session["DBEmailNotificationModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBEmailNotificationModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBEmailNotificationModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_EmailNotification_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_EmailNotification_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_EmailNotification_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_EmailNotification_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["EmailNotification_Email"] = ((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_EditEmail")).Text;
            e.NewValues["EmailNotification_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["EmailNotification_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_EmailNotification", "EmailNotification_Id = " + Request.QueryString["EmailNotification_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_EmailNotification_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["EmailNotificationHistory"] = Convert.ToString(DataRowView_Form["EmailNotification_History"], CultureInfo.CurrentCulture);

            Session["EmailNotificationHistory"] = Session["History"].ToString() + Session["EmailNotificationHistory"].ToString();
            e.NewValues["EmailNotification_History"] = Session["EmailNotificationHistory"].ToString();

            Session["EmailNotificationHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDEmailNotificationModifiedDate"] = "";
        Session["DBEmailNotificationModifiedDate"] = "";
        Session["DBEmailNotificationModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditAssembly = (TextBox)FormView_EmailNotification_Form.FindControl("TextBox_EditAssembly");
      TextBox TextBox_EditMethod = (TextBox)FormView_EmailNotification_Form.FindControl("TextBox_EditMethod");
      TextBox TextBox_EditEmail = (TextBox)FormView_EmailNotification_Form.FindControl("TextBox_EditEmail");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditAssembly.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditMethod.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditEmail.Text))
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
        string EmailNotificationId = "";
        string SQLStringEmailNotification = "SELECT EmailNotification_Id FROM Administration_EmailNotification WHERE EmailNotification_Assembly = @EmailNotification_Assembly AND EmailNotification_Method = @EmailNotification_Method AND EmailNotification_Email = @EmailNotification_Email";
        using (SqlCommand SqlCommand_EmailNotification = new SqlCommand(SQLStringEmailNotification))
        {
          SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Assembly", TextBox_EditAssembly.Text.ToString());
          SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Method", TextBox_EditMethod.Text.ToString());
          SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Email", TextBox_EditEmail.Text.ToString());
          DataTable DataTable_EmailNotification;
          using (DataTable_EmailNotification = new DataTable())
          {
            DataTable_EmailNotification.Locale = CultureInfo.CurrentCulture;
            DataTable_EmailNotification = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailNotification).Copy();
            if (DataTable_EmailNotification.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_EmailNotification.Rows)
              {
                EmailNotificationId = DataRow_Row["EmailNotification_Id"].ToString();
              }
            }
          }
        }

        if (!string.IsNullOrEmpty(EmailNotificationId))
        {
          if (EmailNotificationId != Request.QueryString["EmailNotification_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A Email Notification for Assembly: '" + TextBox_EditAssembly.Text.ToString() + "', Method: '" + TextBox_EditMethod.Text.ToString() + "' and Email: '" + TextBox_EditEmail.Text.ToString() + "' already exists<br />";
          }
        }

        EmailNotificationId = "";


        string EmailTextBox = ((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_EditEmail")).Text.ToString();
        EmailTextBox = EmailTextBox.Replace(";", Convert.ToString(",", CultureInfo.CurrentCulture));
        EmailTextBox = EmailTextBox.Replace(":", Convert.ToString(",", CultureInfo.CurrentCulture));

        string EmailTextBoxSplit = EmailTextBox;
        string[] EmailTextBoxSplitEmails = EmailTextBoxSplit.Split(',');

        foreach (string EmailTextBoxSplitEmail in EmailTextBoxSplitEmails)
        {
          InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
          string ValidEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailTextBoxSplitEmail);

          if (ValidEmailAddress == "No")
          {
            InvalidFormMessage = InvalidFormMessage + "Invalid Email Address '" + EmailTextBoxSplitEmail + "'<br />";
          }
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_EmailNotification_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        if (e.AffectedRows == 1)
        {
          if (Button_EditUpdateClicked == true)
          {
            Button_EditUpdateClicked = false;

            if (((TextBox)FormView_EmailNotification_Form.FindControl("TextBox_EditEmail")).Text != ((HiddenField)FormView_EmailNotification_Form.FindControl("HiddenField_EditEmail")).Value)
            {
              string EmailNotificationMethod = "";
              string EmailNotificationEmail = "";
              string SQLStringEmailNotification = "SELECT EmailNotification_Method , EmailNotification_Email FROM Administration_EmailNotification WHERE EmailNotification_Id = @EmailNotification_Id";
              using (SqlCommand SqlCommand_EmailNotification = new SqlCommand(SQLStringEmailNotification))
              {
                SqlCommand_EmailNotification.Parameters.AddWithValue("@EmailNotification_Id", Request.QueryString["EmailNotification_Id"]);
                DataTable DataTable_EmailNotification;
                using (DataTable_EmailNotification = new DataTable())
                {
                  DataTable_EmailNotification.Locale = CultureInfo.CurrentCulture;
                  DataTable_EmailNotification = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_EmailNotification).Copy();
                  if (DataTable_EmailNotification.Rows.Count > 0)
                  {
                    foreach (DataRow DataRow_Row in DataTable_EmailNotification.Rows)
                    {
                      EmailNotificationMethod = DataRow_Row["EmailNotification_Method"].ToString();
                      EmailNotificationEmail = DataRow_Row["EmailNotification_Email"].ToString();

                      if (!string.IsNullOrEmpty(EmailNotificationMethod) && !string.IsNullOrEmpty(EmailNotificationEmail))
                      {
                        string EmailTemplate = InfoQuestWCF.InfoQuest_All.All_SystemEmailTemplate("72");
                        string BodyString = EmailTemplate;

                        BodyString = BodyString.Replace(";replace;EmailUserFullName;replace;", "" + EmailNotificationEmail + "");
                        BodyString = BodyString.Replace(";replace;Method;replace;", "" + EmailNotificationMethod + "");
                        BodyString = BodyString.Replace(";replace;Email;replace;", "" + EmailNotificationEmail + "");

                        string HeaderString = InfoQuestWCF.InfoQuest_All.All_EmailHeader();
                        string FooterString = InfoQuestWCF.InfoQuest_All.All_EmailFooter();
                        string EmailBody = HeaderString + BodyString + FooterString;

                        string EmailSend = InfoQuestWCF.InfoQuest_All.All_SendEmail(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_Noreply@Lifehealthcare.co.za", EmailNotificationEmail, "InfoQuest Email Notification", EmailBody);

                        if (!string.IsNullOrEmpty(EmailSend))
                        {
                          EmailSend = "";
                        }

                        EmailTemplate = "";
                        BodyString = "";
                        HeaderString = "";
                        FooterString = "";
                        EmailBody = "";
                      }
                    }
                  }
                }
              }

              EmailNotificationMethod = "";
              EmailNotificationEmail = "";
            }

            RedirectToList();
          }
        }
      }
    }


    protected void FormView_EmailNotification_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    protected void TextBox_InsertEmail_TextChanged(object sender, EventArgs e)
    {
      string EmailErrorMessage = "";
      TextBox TextBox_InsertEmail = (TextBox)sender;
      Label Label_InsertEmailError = (Label)FormView_EmailNotification_Form.FindControl("Label_InsertEmailError");

      if (!string.IsNullOrEmpty(TextBox_InsertEmail.Text.ToString()))
      {
        string EmailTextBox = TextBox_InsertEmail.Text.ToString();
        EmailTextBox = EmailTextBox.Replace(";", Convert.ToString(",", CultureInfo.CurrentCulture));
        EmailTextBox = EmailTextBox.Replace(":", Convert.ToString(",", CultureInfo.CurrentCulture));

        string EmailTextBoxSplit = EmailTextBox;
        string[] EmailTextBoxSplitEmails = EmailTextBoxSplit.Split(',');

        foreach (string EmailTextBoxSplitEmail in EmailTextBoxSplitEmails)
        {
          InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
          string ValidEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailTextBoxSplitEmail);

          if (ValidEmailAddress == "No")
          {
            ToolkitScriptManager_EmailNotification.SetFocus(TextBox_InsertEmail);
            EmailErrorMessage = EmailErrorMessage + Convert.ToString("Email Address " + EmailTextBoxSplitEmail + " is not a valid Email address<br />", CultureInfo.CurrentCulture);
          }
        }

        TextBox_InsertEmail.Text = EmailTextBox;
      }

      Label_InsertEmailError.Text = EmailErrorMessage;
    }

    protected void TextBox_EditEmail_TextChanged(object sender, EventArgs e)
    {
      string EmailErrorMessage = "";
      TextBox TextBox_EditEmail = (TextBox)sender;
      Label Label_EditEmailError = (Label)FormView_EmailNotification_Form.FindControl("Label_EditEmailError");

      if (!string.IsNullOrEmpty(TextBox_EditEmail.Text.ToString()))
      {
        string EmailTextBox = TextBox_EditEmail.Text.ToString();
        EmailTextBox = EmailTextBox.Replace(";", Convert.ToString(",", CultureInfo.CurrentCulture));
        EmailTextBox = EmailTextBox.Replace(":", Convert.ToString(",", CultureInfo.CurrentCulture));

        string EmailTextBoxSplit = EmailTextBox;
        string[] EmailTextBoxSplitEmails = EmailTextBoxSplit.Split(',');

        foreach (string EmailTextBoxSplitEmail in EmailTextBoxSplitEmails)
        {
          InfoQuestWCF.InfoQuest_Regex InfoQuest_Regex_ValidEmailAddress = new InfoQuestWCF.InfoQuest_Regex();
          string ValidEmailAddress = InfoQuest_Regex_ValidEmailAddress.Regex_ValidEmailAddress(EmailTextBoxSplitEmail);

          if (ValidEmailAddress == "No")
          {
            ToolkitScriptManager_EmailNotification.SetFocus(TextBox_EditEmail);
            EmailErrorMessage = EmailErrorMessage + Convert.ToString("Email Address " + EmailTextBoxSplitEmail + " is not a valid Email address<br />", CultureInfo.CurrentCulture);
          }
        }

        TextBox_EditEmail.Text = EmailTextBox;
      }

      Label_EditEmailError.Text = EmailErrorMessage;
    }

    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}