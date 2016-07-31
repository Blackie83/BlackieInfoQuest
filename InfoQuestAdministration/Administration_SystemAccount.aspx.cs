using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SystemAccount : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_SystemAccount_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_SystemAccount, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["SystemAccount_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SystemAccount.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["SystemAccount_Id"] != null)
      {
        FormView_SystemAccount_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_SystemAccount_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_SystemAccount_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_SystemAccount_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAccount_Form.FindControl("TextBox_InsertDomain")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAccount_Form.FindControl("TextBox_InsertUserName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAccount_Form.FindControl("TextBox_InsertPassword")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_SystemAccount_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_SystemAccount_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAccount_Form.FindControl("TextBox_EditDomain")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAccount_Form.FindControl("TextBox_EditUserName")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAccount_Form.FindControl("TextBox_EditPassword")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_SystemAccountId"];
      string SearchField2 = Request.QueryString["Search_SystemAccountIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SystemAccount_Id=" + Request.QueryString["Search_SystemAccountId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SystemAccount_IsActive=" + Request.QueryString["Search_SystemAccountIsActive"] + "&";
      }

      string FinalURL = "Administration_SystemAccount_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Account List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_SystemAccount_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_SystemAccount_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_SystemAccount_Form.InsertParameters["SystemAccount_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SystemAccount_Form.InsertParameters["SystemAccount_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SystemAccount_Form.InsertParameters["SystemAccount_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SystemAccount_Form.InsertParameters["SystemAccount_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SystemAccount_Form.InsertParameters["SystemAccount_History"].DefaultValue = "";
          SqlDataSource_SystemAccount_Form.InsertParameters["SystemAccount_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertDescription = (TextBox)FormView_SystemAccount_Form.FindControl("TextBox_InsertDescription");
      TextBox TextBox_InsertDomain = (TextBox)FormView_SystemAccount_Form.FindControl("TextBox_InsertDomain");
      TextBox TextBox_InsertUserName = (TextBox)FormView_SystemAccount_Form.FindControl("TextBox_InsertUserName");
      TextBox TextBox_InsertPassword = (TextBox)FormView_SystemAccount_Form.FindControl("TextBox_InsertPassword");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertDomain.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertUserName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_InsertPassword.Text))
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

      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SystemAccount_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["SystemAccount_Id"] = e.Command.Parameters["@SystemAccount_Id"].Value;

        string SearchField1 = "s_SystemAccount_Id=" + Session["SystemAccount_Id"].ToString() + "";
        string FinalURL = "Administration_SystemAccount_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Account List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_SystemAccount_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDSystemAccountModifiedDate"] = e.OldValues["SystemAccount_ModifiedDate"];
        object OLDSystemAccountModifiedDate = Session["OLDSystemAccountModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDSystemAccountModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareSystemAccount = (DataView)SqlDataSource_SystemAccount_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareSystemAccount = DataView_CompareSystemAccount[0];
        Session["DBSystemAccountModifiedDate"] = Convert.ToString(DataRowView_CompareSystemAccount["SystemAccount_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBSystemAccountModifiedBy"] = Convert.ToString(DataRowView_CompareSystemAccount["SystemAccount_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBSystemAccountModifiedDate = Session["DBSystemAccountModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBSystemAccountModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBSystemAccountModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_SystemAccount_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_SystemAccount_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_SystemAccount_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_SystemAccount_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["SystemAccount_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["SystemAccount_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_SystemAccount", "SystemAccount_Id = " + Request.QueryString["SystemAccount_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_SystemAccount_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["SystemAccountHistory"] = Convert.ToString(DataRowView_Form["SystemAccount_History"], CultureInfo.CurrentCulture);

            Session["SystemAccountHistory"] = Session["History"].ToString() + Session["SystemAccountHistory"].ToString();
            e.NewValues["SystemAccount_History"] = Session["SystemAccountHistory"].ToString();

            Session["SystemAccountHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDSystemAccountModifiedDate"] = "";
        Session["DBSystemAccountModifiedDate"] = "";
        Session["DBSystemAccountModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditDescription = (TextBox)FormView_SystemAccount_Form.FindControl("TextBox_EditDescription");
      TextBox TextBox_EditDomain = (TextBox)FormView_SystemAccount_Form.FindControl("TextBox_EditDomain");
      TextBox TextBox_EditUserName = (TextBox)FormView_SystemAccount_Form.FindControl("TextBox_EditUserName");
      TextBox TextBox_EditPassword = (TextBox)FormView_SystemAccount_Form.FindControl("TextBox_EditPassword");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditDomain.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditUserName.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(TextBox_EditPassword.Text))
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

      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SystemAccount_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_SystemAccount_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}