using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SystemAdministrator : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_SystemAdministrator_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_SystemAdministrator, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["SystemAdministrator_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SystemAdministrator.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["SystemAdministrator_Id"] != null)
      {
        FormView_SystemAdministrator_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_SystemAdministrator_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_SystemAdministrator_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_InsertDomain")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_InsertUserName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_SystemAdministrator_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_EditDomain")).Attributes.Add("OnKeyUp", "Validation_Form();");
        ((TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_EditUserName")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_SystemAdministratorId"];
      string SearchField2 = Request.QueryString["Search_SystemAdministratorIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SystemAdministrator_Id=" + Request.QueryString["Search_SystemAdministratorId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SystemAdministrator_IsActive=" + Request.QueryString["Search_SystemAdministratorIsActive"] + "&";
      }

      string FinalURL = "Administration_SystemAdministrator_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Administrator List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_SystemAdministrator_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_SystemAdministrator_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          SqlDataSource_SystemAdministrator_Form.InsertParameters["SystemAdministrator_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SystemAdministrator_Form.InsertParameters["SystemAdministrator_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SystemAdministrator_Form.InsertParameters["SystemAdministrator_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SystemAdministrator_Form.InsertParameters["SystemAdministrator_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SystemAdministrator_Form.InsertParameters["SystemAdministrator_History"].DefaultValue = "";
          SqlDataSource_SystemAdministrator_Form.InsertParameters["SystemAdministrator_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertDescription = (TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_InsertDescription");
      TextBox TextBox_InsertDomain = (TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_InsertDomain");
      TextBox TextBox_InsertUserName = (TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_InsertUserName");

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

    protected void SqlDataSource_SystemAdministrator_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["SystemAdministrator_Id"] = e.Command.Parameters["@SystemAdministrator_Id"].Value;

        string SearchField1 = "s_SystemAdministrator_Id=" + Session["SystemAdministrator_Id"].ToString() + "";
        string FinalURL = "Administration_SystemAdministrator_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Administrator List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_SystemAdministrator_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDSystemAdministratorModifiedDate"] = e.OldValues["SystemAdministrator_ModifiedDate"];
        object OLDSystemAdministratorModifiedDate = Session["OLDSystemAdministratorModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDSystemAdministratorModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareSystemAdministrator = (DataView)SqlDataSource_SystemAdministrator_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareSystemAdministrator = DataView_CompareSystemAdministrator[0];
        Session["DBSystemAdministratorModifiedDate"] = Convert.ToString(DataRowView_CompareSystemAdministrator["SystemAdministrator_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBSystemAdministratorModifiedBy"] = Convert.ToString(DataRowView_CompareSystemAdministrator["SystemAdministrator_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBSystemAdministratorModifiedDate = Session["DBSystemAdministratorModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBSystemAdministratorModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBSystemAdministratorModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_SystemAdministrator_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_SystemAdministrator_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_SystemAdministrator_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_SystemAdministrator_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            e.NewValues["SystemAdministrator_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["SystemAdministrator_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_SystemAdministrator", "SystemAdministrator_Id = " + Request.QueryString["SystemAdministrator_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_SystemAdministrator_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["SystemAdministratorHistory"] = Convert.ToString(DataRowView_Form["SystemAdministrator_History"], CultureInfo.CurrentCulture);

            Session["SystemAdministratorHistory"] = Session["History"].ToString() + Session["SystemAdministratorHistory"].ToString();
            e.NewValues["SystemAdministrator_History"] = Session["SystemAdministratorHistory"].ToString();

            Session["SystemAdministratorHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDSystemAdministratorModifiedDate"] = "";
        Session["DBSystemAdministratorModifiedDate"] = "";
        Session["DBSystemAdministratorModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditDescription = (TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_EditDescription");
      TextBox TextBox_EditDomain = (TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_EditDomain");
      TextBox TextBox_EditUserName = (TextBox)FormView_SystemAdministrator_Form.FindControl("TextBox_EditUserName");

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

    protected void SqlDataSource_SystemAdministrator_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_SystemAdministrator_Form_ItemCommand(object sender, CommandEventArgs e)
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