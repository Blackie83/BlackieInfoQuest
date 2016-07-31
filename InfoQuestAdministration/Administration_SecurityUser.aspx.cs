using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.Permissions;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class SecurityUser : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_SecurityUser_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_SecurityUser, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["SecurityUser_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SecurityUser.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["SecurityUser_Id"] != null)
      {
        FormView_SecurityUser_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_SecurityUser_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_SecurityUser_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertUserName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertDisplayName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertFirstName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertLastName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertEmployeeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertEmail")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertManagerUserName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_SecurityUser_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditUserName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditDisplayName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditFirstName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditLastName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditEmployeeNumber")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditEmail")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditManagerUserName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_SecurityUserUserName"];
      string SearchField2 = Request.QueryString["Search_SecurityUserDisplayName"];
      string SearchField3 = Request.QueryString["Search_SecurityUserEmployeeNumber"];
      string SearchField4 = Request.QueryString["Search_SecurityUserManagerUserName"];
      string SearchField5 = Request.QueryString["Search_SecurityUserIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SecurityUser_UserName=" + Request.QueryString["Search_SecurityUserUserName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "";
      }
      else
      {
        SearchField2 = "s_SecurityUser_DisplayName=" + Request.QueryString["Search_SecurityUserDisplayName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField3))
      {
        SearchField3 = "s_SecurityUser_EmployeeNumber=" + Request.QueryString["Search_SecurityUserEmployeeNumber"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField4))
      {
        SearchField4 = "s_SecurityUser_ManagerUserName=" + Request.QueryString["Search_SecurityUserManagerUserName"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField5))
      {
        SearchField5 = "s_SecurityUser_IsActive=" + Request.QueryString["Search_SecurityUserIsActive"] + "&";
      }

      string FinalURL = "Administration_SecurityUser_List.aspx?" + SearchField1 + SearchField2 + SearchField3 + SearchField4 + SearchField5;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security User List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_SecurityUser_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_SecurityUser_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_SecurityUser_Form.InsertParameters["SecurityUser_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SecurityUser_Form.InsertParameters["SecurityUser_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SecurityUser_Form.InsertParameters["SecurityUser_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SecurityUser_Form.InsertParameters["SecurityUser_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SecurityUser_Form.InsertParameters["SecurityUser_History"].DefaultValue = "";
          SqlDataSource_SecurityUser_Form.InsertParameters["SecurityUser_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertUserName");
      TextBox TextBox_InsertDisplayName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertDisplayName");
      TextBox TextBox_InsertFirstName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertFirstName");
      TextBox TextBox_InsertLastName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertLastName");
      TextBox TextBox_InsertEmployeeNumber = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertEmployeeNumber");
      TextBox TextBox_InsertEmail = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertEmail");
      TextBox TextBox_InsertManagerUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertManagerUserName");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertUserName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertDisplayName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertFirstName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertLastName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertEmployeeNumber.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertEmail.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertManagerUserName.Text))
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
        string ValidLHCUserName = TextBox_InsertUserName.Text.Substring(0, 4);

        if (ValidLHCUserName != "LHC\\")
        {
          InvalidFormMessage = InvalidFormMessage + "UserName is not in the correct format,<br />UserName must be in the format LHC\\username<br />";
        }
        else
        {
          Session["SecurityUserUserName"] = "";
          string SQLStringSecurityUserUserName = "SELECT SecurityUser_UserName FROM Administration_SecurityUser WHERE SecurityUser_UserName = @SecurityUser_UserName AND SecurityUser_IsActive = @SecurityUser_IsActive";
          using (SqlCommand SqlCommand_SecurityUserUserName = new SqlCommand(SQLStringSecurityUserUserName))
          {
            SqlCommand_SecurityUserUserName.Parameters.AddWithValue("@SecurityUser_UserName", TextBox_InsertUserName.Text.ToString());
            SqlCommand_SecurityUserUserName.Parameters.AddWithValue("@SecurityUser_IsActive", 1);
            DataTable DataTable_SecurityUserUserName;
            using (DataTable_SecurityUserUserName = new DataTable())
            {
              DataTable_SecurityUserUserName.Locale = CultureInfo.CurrentCulture;
              DataTable_SecurityUserUserName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUserUserName).Copy();
              if (DataTable_SecurityUserUserName.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_SecurityUserUserName.Rows)
                {
                  Session["SecurityUserUserName"] = DataRow_Row["SecurityUser_UserName"];
                }
              }
              else
              {
                Session["SecurityUserUserName"] = "";
              }
            }
          }

          if (!string.IsNullOrEmpty(Session["SecurityUserUserName"].ToString()))
          {
            InvalidFormMessage = InvalidFormMessage + "A Security User with the Name '" + Session["SecurityUserUserName"].ToString() + "' already exists<br />";
          }

          Session["SecurityUserUserName"] = "";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SecurityUser_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["SecurityUser_Id"] = e.Command.Parameters["@SecurityUser_Id"].Value;

        string SearchField1 = "s_SecurityUser_Id=" + Session["SecurityUser_Id"].ToString() + "";
        string FinalURL = "Administration_SecurityUser_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("Security User List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_SecurityUser_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDSecurityUserModifiedDate"] = e.OldValues["SecurityUser_ModifiedDate"];
        object OLDSecurityUserModifiedDate = Session["OLDSecurityUserModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDSecurityUserModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareSecurityUser = (DataView)SqlDataSource_SecurityUser_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareSecurityUser = DataView_CompareSecurityUser[0];
        Session["DBSecurityUserModifiedDate"] = Convert.ToString(DataRowView_CompareSecurityUser["SecurityUser_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBSecurityUserModifiedBy"] = Convert.ToString(DataRowView_CompareSecurityUser["SecurityUser_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBSecurityUserModifiedDate = Session["DBSecurityUserModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBSecurityUserModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBSecurityUserModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_SecurityUser_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_SecurityUser_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_SecurityUser_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_SecurityUser_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["SecurityUser_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["SecurityUser_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_SecurityUser", "SecurityUser_Id = " + Request.QueryString["SecurityUser_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_SecurityUser_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["SecurityUserHistory"] = Convert.ToString(DataRowView_Form["SecurityUser_History"], CultureInfo.CurrentCulture);

            Session["SecurityUserHistory"] = Session["History"].ToString() + Session["SecurityUserHistory"].ToString();
            e.NewValues["SecurityUser_History"] = Session["SecurityUserHistory"].ToString();

            Session["SecurityUserHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDSecurityUserModifiedDate"] = "";
        Session["DBSecurityUserModifiedDate"] = "";
        Session["DBSecurityUserModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditUserName");
      TextBox TextBox_EditDisplayName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditDisplayName");
      TextBox TextBox_EditFirstName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditFirstName");
      TextBox TextBox_EditLastName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditLastName");
      TextBox TextBox_EditEmployeeNumber = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditEmployeeNumber");
      TextBox TextBox_EditEmail = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditEmail");
      TextBox TextBox_EditManagerUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditManagerUserName");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditUserName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditDisplayName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditFirstName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditLastName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditEmployeeNumber.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditEmail.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditManagerUserName.Text))
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
        string ValidLHCUserName = TextBox_EditUserName.Text.Substring(0, 4);

        if (ValidLHCUserName != "LHC\\")
        {
          InvalidFormMessage = InvalidFormMessage + "UserName is not in the correct format,<br />UserName must be in the format LHC\\username<br />";
        }
        else
        {
          Session["SecurityUserId"] = "";
          Session["SecurityUserUserName"] = "";
          string SQLStringSecurityUserUserName = "SELECT SecurityUser_Id , SecurityUser_UserName FROM Administration_SecurityUser WHERE SecurityUser_UserName = @SecurityUser_UserName AND SecurityUser_IsActive = @SecurityUser_IsActive";
          using (SqlCommand SqlCommand_SecurityUserUserName = new SqlCommand(SQLStringSecurityUserUserName))
          {
            SqlCommand_SecurityUserUserName.Parameters.AddWithValue("@SecurityUser_UserName", TextBox_EditUserName.Text.ToString());
            SqlCommand_SecurityUserUserName.Parameters.AddWithValue("@SecurityUser_IsActive", 1);
            DataTable DataTable_SecurityUserUserName;
            using (DataTable_SecurityUserUserName = new DataTable())
            {
              DataTable_SecurityUserUserName.Locale = CultureInfo.CurrentCulture;
              DataTable_SecurityUserUserName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUserUserName).Copy();
              if (DataTable_SecurityUserUserName.Rows.Count > 0)
              {
                foreach (DataRow DataRow_Row in DataTable_SecurityUserUserName.Rows)
                {
                  Session["SecurityUserId"] = DataRow_Row["SecurityUser_Id"];
                  Session["SecurityUserUserName"] = DataRow_Row["SecurityUser_UserName"];
                }
              }
              else
              {
                Session["SecurityUserId"] = "";
                Session["SecurityUserUserName"] = "";
              }
            }
          }

          if (!string.IsNullOrEmpty(Session["SecurityUserUserName"].ToString()))
          {
            if (Session["SecurityUserId"].ToString() != Request.QueryString["SecurityUser_Id"])
            {
              InvalidFormMessage = InvalidFormMessage + "A Security User with the Name '" + Session["SecurityUserUserName"].ToString() + "' already exists<br />";
            }
          }

          Session["SecurityUserId"] = "";
          Session["SecurityUserUserName"] = "";
        }
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SecurityUser_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_SecurityUser_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    protected void TextBox_InsertUserName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertUserName");
      Label Label_InsertUserNameError = (Label)FormView_SecurityUser_Form.FindControl("Label_InsertUSerNameError");

      TextBox TextBox_InsertDisplayName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertDisplayName");
      TextBox TextBox_InsertFirstName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertFirstName");
      TextBox TextBox_InsertLastName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertLastName");
      TextBox TextBox_InsertEmployeeNumber = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertEmployeeNumber");
      TextBox TextBox_InsertEmail = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertEmail");
      TextBox TextBox_InsertManagerUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertManagerUserName");

      string ValidLHCUserName = TextBox_InsertUserName.Text.Substring(0, 4);

      if (ValidLHCUserName != "LHC\\")
      {
        Label_InsertUserNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>UserName is not in the correct format,<br />UserName must be in the format LHC\\username</div>", CultureInfo.CurrentCulture);

        InsertTextBoxClear();
      }
      else
      {
        Session["SecurityUserUserName"] = "";
        string SQLStringSecurityUserUserName = "SELECT SecurityUser_UserName FROM Administration_SecurityUser WHERE SecurityUser_UserName = @SecurityUser_UserName AND SecurityUser_IsActive = @SecurityUser_IsActive";
        using (SqlCommand SqlCommand_SecurityUserUserName = new SqlCommand(SQLStringSecurityUserUserName))
        {
          SqlCommand_SecurityUserUserName.Parameters.AddWithValue("@SecurityUser_UserName", TextBox_InsertUserName.Text.ToString());
          SqlCommand_SecurityUserUserName.Parameters.AddWithValue("@SecurityUser_IsActive", 1);
          DataTable DataTable_SecurityUserUserName;
          using (DataTable_SecurityUserUserName = new DataTable())
          {
            DataTable_SecurityUserUserName.Locale = CultureInfo.CurrentCulture;
            DataTable_SecurityUserUserName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUserUserName).Copy();
            if (DataTable_SecurityUserUserName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SecurityUserUserName.Rows)
              {
                Session["SecurityUserUserName"] = DataRow_Row["SecurityUser_UserName"];
              }
            }
          }
        }

        Session["UserName"] = "";
        Session["DisplayName"] = "";
        Session["FirstName"] = "";
        Session["LastName"] = "";
        Session["EmployeeNumber"] = "";
        Session["Email"] = "";
        Session["ManagerUserName"] = "";
        Session["Error"] = "";

        if (string.IsNullOrEmpty(Session["SecurityUserUserName"].ToString()))
        {
          InsertUserName_SecurityUserUserName_Empty(TextBox_InsertUserName.Text);

          if (!string.IsNullOrEmpty(Session["Error"].ToString()))
          {
            Label_InsertUserNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);

            InsertTextBoxClear();
          }
          else
          {
            Label_InsertUserNameError.Text = "";

            TextBox_InsertDisplayName.Text = Session["DisplayName"].ToString();
            TextBox_InsertFirstName.Text = Session["FirstName"].ToString();
            TextBox_InsertLastName.Text = Session["LastName"].ToString();

            if (string.IsNullOrEmpty(Session["EmployeeNumber"].ToString()))
            {
              TextBox_InsertEmployeeNumber.Text = Convert.ToString("No Employee Number", CultureInfo.CurrentCulture);
            }
            else
            {
              TextBox_InsertEmployeeNumber.Text = Session["EmployeeNumber"].ToString();
            }

            TextBox_InsertEmail.Text = Session["Email"].ToString();

            if (string.IsNullOrEmpty(Session["ManagerUserName"].ToString()))
            {
              TextBox_InsertManagerUserName.Text = Convert.ToString("No Manager UserName", CultureInfo.CurrentCulture);
            }
            else
            {
              TextBox_InsertManagerUserName.Text = Convert.ToString("LHC\\", CultureInfo.CurrentCulture) + Session["ManagerUserName"].ToString();
            }
          }
        }
        else
        {
          Label_InsertUserNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Security User with the Name '" + Session["SecurityUserUserName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);

          InsertTextBoxClear();
        }

        Session["SecurityUserUserName"] = "";

        Session["UserName"] = "";
        Session["DisplayName"] = "";
        Session["FirstName"] = "";
        Session["LastName"] = "";
        Session["EmployeeNumber"] = "";
        Session["Email"] = "";
        Session["ManagerUserName"] = "";
        Session["Error"] = "";
      }
    }

    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    protected void InsertUserName_SecurityUserUserName_Empty(string userName)
    {
      Label Label_InsertUserNameError = (Label)FormView_SecurityUser_Form.FindControl("Label_InsertUSerNameError");

      Label_InsertUserNameError.Text = "";

      DataTable DataTable_DataEmployee;
      using (DataTable_DataEmployee = new DataTable())
      {
        DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
        DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindAll(userName, "", "", "").Copy();
        if (DataTable_DataEmployee.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }
        }
        else if (DataTable_DataEmployee.Columns.Count != 1)
        {
          if (DataTable_DataEmployee.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Session["UserName"] = DataRow_Row["UserName"];
              Session["DisplayName"] = DataRow_Row["DisplayName"];
              Session["FirstName"] = DataRow_Row["FirstName"];
              Session["LastName"] = DataRow_Row["LastName"];
              Session["EmployeeNumber"] = DataRow_Row["EmployeeNumber"];
              Session["Email"] = DataRow_Row["Email"];
              Session["ManagerUserName"] = DataRow_Row["ManagerUserName"];
              Session["Error"] = "";
            }
          }
          else
          {
            Session["Error"] = "Employee Information not found for specific UserName,<br/>Please type in the Employee Information";
          }
        }
      }
    }

    protected void InsertTextBoxClear()
    {
      TextBox TextBox_InsertDisplayName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertDisplayName");
      TextBox TextBox_InsertFirstName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertFirstName");
      TextBox TextBox_InsertLastName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertLastName");
      TextBox TextBox_InsertEmployeeNumber = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertEmployeeNumber");
      TextBox TextBox_InsertEmail = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertEmail");
      TextBox TextBox_InsertManagerUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_InsertManagerUserName");

      TextBox_InsertDisplayName.Text = "";
      TextBox_InsertFirstName.Text = "";
      TextBox_InsertLastName.Text = "";
      TextBox_InsertEmployeeNumber.Text = "";
      TextBox_InsertEmail.Text = "";
      TextBox_InsertManagerUserName.Text = "";
    }


    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    protected void TextBox_EditUserName_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditUserName");
      Label Label_EditUserNameError = (Label)FormView_SecurityUser_Form.FindControl("Label_EditUserNameError");

      TextBox TextBox_EditDisplayName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditDisplayName");
      TextBox TextBox_EditFirstName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditFirstName");
      TextBox TextBox_EditLastName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditLastName");
      TextBox TextBox_EditEmployeeNumber = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditEmployeeNumber");
      TextBox TextBox_EditEmail = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditEmail");
      TextBox TextBox_EditManagerUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditManagerUserName");

      string ValidLHCUserName = TextBox_EditUserName.Text.Substring(0, 4);

      if (ValidLHCUserName != "LHC\\")
      {
        Label_EditUserNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>UserName is not in the correct format,<br />UserName must be in the format LHC\\username</div>", CultureInfo.CurrentCulture);

        EditTextBoxClear();
      }
      else
      {
        Session["SecurityUserId"] = "";
        Session["SecurityUserUserName"] = "";
        string SQLStringSecurityUserUserName = "SELECT SecurityUser_Id , SecurityUser_UserName FROM Administration_SecurityUser WHERE SecurityUser_UserName = @SecurityUser_UserName AND SecurityUser_IsActive = @SecurityUser_IsActive";
        using (SqlCommand SqlCommand_SecurityUserUserName = new SqlCommand(SQLStringSecurityUserUserName))
        {
          SqlCommand_SecurityUserUserName.Parameters.AddWithValue("@SecurityUser_UserName", TextBox_EditUserName.Text.ToString());
          SqlCommand_SecurityUserUserName.Parameters.AddWithValue("@SecurityUser_IsActive", 1);
          DataTable DataTable_SecurityUserUserName;
          using (DataTable_SecurityUserUserName = new DataTable())
          {
            DataTable_SecurityUserUserName.Locale = CultureInfo.CurrentCulture;
            DataTable_SecurityUserUserName = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SecurityUserUserName).Copy();
            if (DataTable_SecurityUserUserName.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SecurityUserUserName.Rows)
              {
                Session["SecurityUserId"] = DataRow_Row["SecurityUser_Id"];
                Session["SecurityUserUserName"] = DataRow_Row["SecurityUser_UserName"];
              }
            }
            else
            {
              Session["SecurityUserId"] = "";
              Session["SecurityUserUserName"] = "";
            }
          }
        }

        Session["UserName"] = "";
        Session["DisplayName"] = "";
        Session["FirstName"] = "";
        Session["LastName"] = "";
        Session["EmployeeNumber"] = "";
        Session["Email"] = "";
        Session["ManagerUserName"] = "";
        Session["Error"] = "";

        if (string.IsNullOrEmpty(Session["SecurityUserUserName"].ToString()))
        {
          EditUserName_SecurityUserUserName_Empty(TextBox_EditUserName.Text);

          if (!string.IsNullOrEmpty(Session["Error"].ToString()))
          {
            Label_EditUserNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);

            EditTextBoxClear();
          }
          else
          {
            Label_EditUserNameError.Text = "";

            TextBox_EditDisplayName.Text = Session["DisplayName"].ToString();
            TextBox_EditFirstName.Text = Session["FirstName"].ToString();
            TextBox_EditLastName.Text = Session["LastName"].ToString();

            if (string.IsNullOrEmpty(Session["EmployeeNumber"].ToString()))
            {
              TextBox_EditEmployeeNumber.Text = Convert.ToString("No Employee Number", CultureInfo.CurrentCulture);
            }
            else
            {
              TextBox_EditEmployeeNumber.Text = Session["EmployeeNumber"].ToString();
            }

            TextBox_EditEmail.Text = Session["Email"].ToString();

            if (string.IsNullOrEmpty(Session["ManagerUserName"].ToString()))
            {
              TextBox_EditManagerUserName.Text = Convert.ToString("No Manager UserName", CultureInfo.CurrentCulture);
            }
            else
            {
              TextBox_EditManagerUserName.Text = Convert.ToString("LHC\\", CultureInfo.CurrentCulture) + Session["ManagerUserName"].ToString();
            }
          }
        }
        else
        {
          if (Session["SecurityUserId"].ToString() == Request.QueryString["SecurityUser_Id"])
          {
            EditUserName_SecurityUserUserName_NotEmpty(TextBox_EditUserName.Text);

            if (!string.IsNullOrEmpty(Session["Error"].ToString()))
            {
              Label_EditUserNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>" + Session["Error"].ToString() + "</div>", CultureInfo.CurrentCulture);

              EditTextBoxClear();
            }
            else
            {
              Label_EditUserNameError.Text = "";

              TextBox_EditDisplayName.Text = Session["DisplayName"].ToString();
              TextBox_EditFirstName.Text = Session["FirstName"].ToString();
              TextBox_EditLastName.Text = Session["LastName"].ToString();

              if (string.IsNullOrEmpty(Session["EmployeeNumber"].ToString()))
              {
                TextBox_EditEmployeeNumber.Text = Convert.ToString("No Employee Number", CultureInfo.CurrentCulture);
              }
              else
              {
                TextBox_EditEmployeeNumber.Text = Session["EmployeeNumber"].ToString();
              }

              TextBox_EditEmail.Text = Session["Email"].ToString();

              if (string.IsNullOrEmpty(Session["ManagerUserName"].ToString()))
              {
                TextBox_EditManagerUserName.Text = Convert.ToString("No Manager UserName", CultureInfo.CurrentCulture);
              }
              else
              {
                TextBox_EditManagerUserName.Text = Convert.ToString("LHC\\", CultureInfo.CurrentCulture) + Session["ManagerUserName"].ToString();
              }
            }
          }
          else
          {
            Label_EditUserNameError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A Security Role with the Name '" + Session["SecurityUserUserName"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);

            EditTextBoxClear();
          }
        }

        Session["SecurityUserId"] = "";
        Session["SecurityUserUserName"] = "";

        Session["UserName"] = "";
        Session["DisplayName"] = "";
        Session["FirstName"] = "";
        Session["LastName"] = "";
        Session["EmployeeNumber"] = "";
        Session["Email"] = "";
        Session["ManagerUserName"] = "";
        Session["Error"] = "";
      }
    }

    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    protected void EditUserName_SecurityUserUserName_Empty(string userName)
    {
      Label Label_EditUserNameError = (Label)FormView_SecurityUser_Form.FindControl("Label_EditUserNameError");

      Label_EditUserNameError.Text = "";

      DataTable DataTable_DataEmployee;
      using (DataTable_DataEmployee = new DataTable())
      {
        DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
        DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindAll(userName, "", "", "").Copy();
        if (DataTable_DataEmployee.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }
        }
        else if (DataTable_DataEmployee.Columns.Count != 1)
        {
          if (DataTable_DataEmployee.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Session["UserName"] = DataRow_Row["UserName"];
              Session["DisplayName"] = DataRow_Row["DisplayName"];
              Session["FirstName"] = DataRow_Row["FirstName"];
              Session["LastName"] = DataRow_Row["LastName"];
              Session["EmployeeNumber"] = DataRow_Row["EmployeeNumber"];
              Session["Email"] = DataRow_Row["Email"];
              Session["ManagerUserName"] = DataRow_Row["ManagerUserName"];
              Session["Error"] = "";
            }
          }
          else
          {
            Session["Error"] = "Employee Information not found for specific UserName,<br/>Please type in the Employee Information";
          }
        }
      }
    }

    [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
    protected void EditUserName_SecurityUserUserName_NotEmpty(string userName)
    {
      Label Label_EditUserNameError = (Label)FormView_SecurityUser_Form.FindControl("Label_EditUserNameError");

      Label_EditUserNameError.Text = "";

      DataTable DataTable_DataEmployee;
      using (DataTable_DataEmployee = new DataTable())
      {
        DataTable_DataEmployee.Locale = CultureInfo.CurrentCulture;
        DataTable_DataEmployee = InfoQuestWCF.InfoQuest_DataEmployee.DataEmployee_AD_AccountManagement_FindAll(userName, "", "", "").Copy();
        if (DataTable_DataEmployee.Columns.Count == 1)
        {
          foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
          {
            Session["Error"] = DataRow_Row["Error"];
          }
        }
        else if (DataTable_DataEmployee.Columns.Count != 1)
        {
          if (DataTable_DataEmployee.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_DataEmployee.Rows)
            {
              Session["UserName"] = DataRow_Row["UserName"];
              Session["DisplayName"] = DataRow_Row["DisplayName"];
              Session["FirstName"] = DataRow_Row["FirstName"];
              Session["LastName"] = DataRow_Row["LastName"];
              Session["EmployeeNumber"] = DataRow_Row["EmployeeNumber"];
              Session["Email"] = DataRow_Row["Email"];
              Session["ManagerUserName"] = DataRow_Row["ManagerUserName"];
              Session["Error"] = "";
            }
          }
          else
          {
            Session["Error"] = "Employee Information not found for specific UserName,<br/>Please type in the Employee Information";
          }
        }
      }
    }

    protected void EditTextBoxClear()
    {
      TextBox TextBox_EditDisplayName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditDisplayName");
      TextBox TextBox_EditFirstName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditFirstName");
      TextBox TextBox_EditLastName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditLastName");
      TextBox TextBox_EditEmployeeNumber = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditEmployeeNumber");
      TextBox TextBox_EditEmail = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditEmail");
      TextBox TextBox_EditManagerUserName = (TextBox)FormView_SecurityUser_Form.FindControl("TextBox_EditManagerUserName");

      TextBox_EditDisplayName.Text = "";
      TextBox_EditFirstName.Text = "";
      TextBox_EditLastName.Text = "";
      TextBox_EditEmployeeNumber.Text = "";
      TextBox_EditEmail.Text = "";
      TextBox_EditManagerUserName.Text = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}