using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Globalization;

namespace InfoQuestAdministration
{
  public partial class Administration_SystemEmailTemplate : InfoQuestWCF.Override_SystemWebUIPage
  {
    private bool Button_EditUpdateClicked = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (PageSecurity() == "1")
      {
        Page.MaintainScrollPositionOnPostBack = true;

        SqlDataSource_SystemEmailTemplate_Form.ConnectionString = InfoQuestWCF.InfoQuest_Connections.Connections("InfoQuest");

        ScriptManager.RegisterStartupScript(UpdatePanel_SystemEmailTemplate, this.GetType(), "UpdateProgress", "Validation_Form();", true);

        if (!IsPostBack)
        {
          InfoQuestWCF.InfoQuest_All.All_View(Page.Title, Request.Url.AbsoluteUri, Request.ServerVariables["LOGON_USER"]);

          if (Request.QueryString["SystemEmailTemplate_Id"] != null)
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
        Label Label_UpdateProgress = (Label)PageUpdateProgress_SystemEmailTemplate.FindControl("Label_UpdateProgress");
        Label_UpdateProgress.Text = InfoQuestWCF.InfoQuest_All.All_UpdateProgress();

        NavigationMenu_Page.NavigationId.Add("InfoQuest", "1");
        NavigationMenu_Page.NavigationId.Add("Administration", "2");
        NavigationMenu_Page.NavigationId.Add("AllForms", "3");
      }
    }

    private void SetFormVisibility()
    {
      if (Request.QueryString["SystemEmailTemplate_Id"] != null)
      {
        FormView_SystemEmailTemplate_Form.ChangeMode(FormViewMode.Edit);
      }
      else
      {
        FormView_SystemEmailTemplate_Form.ChangeMode(FormViewMode.Insert);
      }
    }

    private void TableFormVisible()
    {
      if (FormView_SystemEmailTemplate_Form.CurrentMode == FormViewMode.Insert)
      {
        ((TextBox)FormView_SystemEmailTemplate_Form.FindControl("TextBox_InsertDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }

      if (FormView_SystemEmailTemplate_Form.CurrentMode == FormViewMode.Edit)
      {
        ((TextBox)FormView_SystemEmailTemplate_Form.FindControl("TextBox_EditDescription")).Attributes.Add("OnKeyUp", "Validation_Form();");
      }
    }

    private void RedirectToList()
    {
      string SearchField1 = Request.QueryString["Search_SystemEmailTemplateId"];
      string SearchField2 = Request.QueryString["Search_SystemEmailTemplateIsActive"];

      if (!string.IsNullOrEmpty(SearchField1))
      {
        SearchField1 = "s_SystemEmailTemplate_Id=" + Request.QueryString["Search_SystemEmailTemplateId"] + "&";
      }

      if (!string.IsNullOrEmpty(SearchField2))
      {
        SearchField2 = "s_SystemEmailTemplate_IsActive=" + Request.QueryString["Search_SystemEmailTemplateIsActive"] + "&";
      }

      string FinalURL = "Administration_SystemEmailTemplate_List.aspx?" + SearchField1 + SearchField2;
      FinalURL = FinalURL.Remove(FinalURL.Length - 1, 1);
      FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Email Template List", FinalURL);

      Response.Redirect(FinalURL, false);
    }


    //--START-- --Form--//
    protected void FormView_SystemEmailTemplate_Form_ItemInserting(object sender, CancelEventArgs e)
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
          ((Label)FormView_SystemEmailTemplate_Form.FindControl("Label_InsertInvalidFormMessage")).Text = Label_InsertInvalidFormMessage;
        }
        else if (e.Cancel == false)
        {
          TextBox TextBox_InsertDescription = (TextBox)FormView_SystemEmailTemplate_Form.FindControl("TextBox_InsertDescription");
          AjaxControlToolkit.HTMLEditor.Editor Editor_InsertTemplate = (AjaxControlToolkit.HTMLEditor.Editor)FormView_SystemEmailTemplate_Form.FindControl("Editor_InsertTemplate");

          SqlDataSource_SystemEmailTemplate_Form.InsertParameters["SystemEmailTemplate_Description"].DefaultValue = Server.HtmlEncode(TextBox_InsertDescription.Text);
          SqlDataSource_SystemEmailTemplate_Form.InsertParameters["SystemEmailTemplate_Template"].DefaultValue = Editor_InsertTemplate.Content;
          SqlDataSource_SystemEmailTemplate_Form.InsertParameters["SystemEmailTemplate_CreatedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SystemEmailTemplate_Form.InsertParameters["SystemEmailTemplate_CreatedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SystemEmailTemplate_Form.InsertParameters["SystemEmailTemplate_ModifiedDate"].DefaultValue = DateTime.Now.ToString();
          SqlDataSource_SystemEmailTemplate_Form.InsertParameters["SystemEmailTemplate_ModifiedBy"].DefaultValue = Request.ServerVariables["LOGON_USER"];
          SqlDataSource_SystemEmailTemplate_Form.InsertParameters["SystemEmailTemplate_History"].DefaultValue = "";
          SqlDataSource_SystemEmailTemplate_Form.InsertParameters["SystemEmailTemplate_IsActive"].DefaultValue = "true";
        }
      }
    }

    protected string InsertValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_InsertDescription = (TextBox)FormView_SystemEmailTemplate_Form.FindControl("TextBox_InsertDescription");
      AjaxControlToolkit.HTMLEditor.Editor Editor_InsertTemplate = (AjaxControlToolkit.HTMLEditor.Editor)FormView_SystemEmailTemplate_Form.FindControl("Editor_InsertTemplate");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_InsertDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(Editor_InsertTemplate.Content))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "Description and Template fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["SystemEmailTemplateDescription"] = "";
        string SQLStringSystemEmailTemplateDescription = "SELECT SystemEmailTemplate_Description FROM Administration_SystemEmailTemplate WHERE SystemEmailTemplate_Description = @SystemEmailTemplate_Description AND SystemEmailTemplate_IsActive = @SystemEmailTemplate_IsActive";
        using (SqlCommand SqlCommand_SystemEmailTemplateDescription = new SqlCommand(SQLStringSystemEmailTemplateDescription))
        {
          SqlCommand_SystemEmailTemplateDescription.Parameters.AddWithValue("@SystemEmailTemplate_Description", TextBox_InsertDescription.Text.ToString());
          SqlCommand_SystemEmailTemplateDescription.Parameters.AddWithValue("@SystemEmailTemplate_IsActive", 1);
          DataTable DataTable_SystemEmailTemplateDescription;
          using (DataTable_SystemEmailTemplateDescription = new DataTable())
          {
            DataTable_SystemEmailTemplateDescription.Locale = CultureInfo.CurrentCulture;
            DataTable_SystemEmailTemplateDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemEmailTemplateDescription).Copy();
            if (DataTable_SystemEmailTemplateDescription.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SystemEmailTemplateDescription.Rows)
              {
                Session["SystemEmailTemplateDescription"] = DataRow_Row["SystemEmailTemplate_Description"];
              }
            }
            else
            {
              Session["SystemEmailTemplateDescription"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["SystemEmailTemplateDescription"].ToString()))
        {
          InvalidFormMessage = InvalidFormMessage + "A System Email Template with the Description '" + Session["SystemEmailTemplateDescription"].ToString() + "' already exists<br />";
        }

        Session["SystemEmailTemplateDescription"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SystemEmailTemplate_Form_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
      if (e != null)
      {
        Session["SystemEmailTemplate_Id"] = e.Command.Parameters["@SystemEmailTemplate_Id"].Value;

        string SearchField1 = "s_SystemEmailTemplate_Id=" + Session["SystemEmailTemplate_Id"].ToString() + "";
        string FinalURL = "Administration_SystemEmailTemplate_List.aspx?" + SearchField1;
        FinalURL = InfoQuestWCF.InfoQuest_All.All_RedirectLink("System Email Template List", FinalURL);

        Response.Redirect(FinalURL, false);
      }
    }


    protected void FormView_SystemEmailTemplate_Form_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
      if (e != null)
      {
        Session["OLDSystemEmailTemplateModifiedDate"] = e.OldValues["SystemEmailTemplate_ModifiedDate"];
        object OLDSystemEmailTemplateModifiedDate = Session["OLDSystemEmailTemplateModifiedDate"].ToString();
        DateTime OLDModifiedDate1 = DateTime.Parse(OLDSystemEmailTemplateModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string OLDModifiedDateNew = OLDModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        DataView DataView_CompareSystemEmailTemplate = (DataView)SqlDataSource_SystemEmailTemplate_Form.Select(DataSourceSelectArguments.Empty);
        DataRowView DataRowView_CompareSystemEmailTemplate = DataView_CompareSystemEmailTemplate[0];
        Session["DBSystemEmailTemplateModifiedDate"] = Convert.ToString(DataRowView_CompareSystemEmailTemplate["SystemEmailTemplate_ModifiedDate"], CultureInfo.CurrentCulture);
        Session["DBSystemEmailTemplateModifiedBy"] = Convert.ToString(DataRowView_CompareSystemEmailTemplate["SystemEmailTemplate_ModifiedBy"], CultureInfo.CurrentCulture);
        object DBSystemEmailTemplateModifiedDate = Session["DBSystemEmailTemplateModifiedDate"].ToString();
        DateTime DBModifiedDate1 = DateTime.Parse(DBSystemEmailTemplateModifiedDate.ToString(), CultureInfo.CurrentCulture);
        string DBModifiedDateNew = DBModifiedDate1.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.CurrentCulture);

        if (OLDModifiedDateNew != DBModifiedDateNew)
        {
          e.Cancel = true;

          string Label_EditConcurrencyUpdateMessage = Convert.ToString("" +
          "Record could not be updated<br/>" +
          "It was updated at " + DBModifiedDateNew + " by " + Session["DBSystemEmailTemplateModifiedBy"].ToString() + "<br/>" +
          "<a href='" + Request.Url.AbsoluteUri + "' style='color:#b0262e;'>Reload Page</a> to view changes<br/><br/>", CultureInfo.CurrentCulture);

          ((Label)FormView_SystemEmailTemplate_Form.FindControl("Label_EditInvalidFormMessage")).Text = "";
          ((Label)FormView_SystemEmailTemplate_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = Label_EditConcurrencyUpdateMessage;
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
            ((Label)FormView_SystemEmailTemplate_Form.FindControl("Label_EditInvalidFormMessage")).Text = Label_EditInvalidFormMessage;
            ((Label)FormView_SystemEmailTemplate_Form.FindControl("Label_EditConcurrencyUpdateMessage")).Text = "";
          }
          else if (e.Cancel == false)
          {
            TextBox TextBox_EditDescription = (TextBox)FormView_SystemEmailTemplate_Form.FindControl("TextBox_EditDescription");
            AjaxControlToolkit.HTMLEditor.Editor Editor_EditTemplate = (AjaxControlToolkit.HTMLEditor.Editor)FormView_SystemEmailTemplate_Form.FindControl("Editor_EditTemplate");

            e.NewValues["SystemEmailTemplate_Description"] = Server.HtmlEncode(TextBox_EditDescription.Text);
            e.NewValues["SystemEmailTemplate_Template"] = Editor_EditTemplate.Content;
            e.NewValues["SystemEmailTemplate_ModifiedDate"] = DateTime.Now.ToString();
            e.NewValues["SystemEmailTemplate_ModifiedBy"] = Request.ServerVariables["LOGON_USER"];

            Session["History"] = InfoQuestWCF.InfoQuest_All.All_HistoryGet("Administration_SystemEmailTemplate", "SystemEmailTemplate_Id = " + Request.QueryString["SystemEmailTemplate_Id"]);

            DataView DataView_Form = (DataView)SqlDataSource_SystemEmailTemplate_Form.Select(DataSourceSelectArguments.Empty);
            DataRowView DataRowView_Form = DataView_Form[0];
            Session["SystemEmailTemplateHistory"] = Convert.ToString(DataRowView_Form["SystemEmailTemplate_History"], CultureInfo.CurrentCulture);

            Session["SystemEmailTemplateHistory"] = Session["History"].ToString() + Session["SystemEmailTemplateHistory"].ToString();
            e.NewValues["SystemEmailTemplate_History"] = Session["SystemEmailTemplateHistory"].ToString();

            Session["SystemEmailTemplateHistory"] = "";
            Session["History"] = "";
          }
        }

        Session["OLDSystemEmailTemplateModifiedDate"] = "";
        Session["DBSystemEmailTemplateModifiedDate"] = "";
        Session["DBSystemEmailTemplateModifiedBy"] = "";
      }
    }

    protected string EditValidation()
    {
      string InvalidForm = "No";
      string InvalidFormMessage = "";

      TextBox TextBox_EditDescription = (TextBox)FormView_SystemEmailTemplate_Form.FindControl("TextBox_EditDescription");
      AjaxControlToolkit.HTMLEditor.Editor Editor_EditTemplate = (AjaxControlToolkit.HTMLEditor.Editor)FormView_SystemEmailTemplate_Form.FindControl("Editor_EditTemplate");

      if (InvalidForm == "No")
      {
        if (string.IsNullOrEmpty(TextBox_EditDescription.Text))
        {
          InvalidForm = "Yes";
        }

        if (string.IsNullOrEmpty(Editor_EditTemplate.Content))
        {
          InvalidForm = "Yes";
        }
      }

      if (InvalidForm == "Yes")
      {
        InvalidFormMessage = "Description and Template  fields are required";
      }

      if (InvalidForm == "No" && string.IsNullOrEmpty(InvalidFormMessage))
      {
        Session["SystemEmailTemplateId"] = "";
        Session["SystemEmailTemplateDescription"] = "";
        string SQLStringSystemEmailTemplateDescription = "SELECT SystemEmailTemplate_Id , SystemEmailTemplate_Description FROM Administration_SystemEmailTemplate WHERE SystemEmailTemplate_Description = @SystemEmailTemplate_Description AND SystemEmailTemplate_IsActive = @SystemEmailTemplate_IsActive";
        using (SqlCommand SqlCommand_SystemEmailTemplateDescription = new SqlCommand(SQLStringSystemEmailTemplateDescription))
        {
          SqlCommand_SystemEmailTemplateDescription.Parameters.AddWithValue("@SystemEmailTemplate_Description", TextBox_EditDescription.Text.ToString());
          SqlCommand_SystemEmailTemplateDescription.Parameters.AddWithValue("@SystemEmailTemplate_IsActive", 1);
          DataTable DataTable_SystemEmailTemplateDescription;
          using (DataTable_SystemEmailTemplateDescription = new DataTable())
          {
            DataTable_SystemEmailTemplateDescription.Locale = CultureInfo.CurrentCulture;
            DataTable_SystemEmailTemplateDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemEmailTemplateDescription).Copy();
            if (DataTable_SystemEmailTemplateDescription.Rows.Count > 0)
            {
              foreach (DataRow DataRow_Row in DataTable_SystemEmailTemplateDescription.Rows)
              {
                Session["SystemEmailTemplateId"] = DataRow_Row["SystemEmailTemplate_Id"];
                Session["SystemEmailTemplateDescription"] = DataRow_Row["SystemEmailTemplate_Description"];
              }
            }
            else
            {
              Session["SystemEmailTemplateId"] = "";
              Session["SystemEmailTemplateDescription"] = "";
            }
          }
        }

        if (!string.IsNullOrEmpty(Session["SystemEmailTemplateDescription"].ToString()))
        {
          if (Session["SystemEmailTemplateId"].ToString() != Request.QueryString["SystemEmailTemplate_Id"])
          {
            InvalidFormMessage = InvalidFormMessage + "A System Email Template with the Description '" + Session["SystemEmailTemplateDescription"].ToString() + "' already exists<br />";
          }
        }

        Session["SystemEmailTemplateId"] = "";
        Session["SystemEmailTemplateDescription"] = "";
      }

      return InvalidFormMessage;
    }

    protected void SqlDataSource_SystemEmailTemplate_Form_Updated(object sender, SqlDataSourceStatusEventArgs e)
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


    protected void FormView_SystemEmailTemplate_Form_ItemCommand(object sender, CommandEventArgs e)
    {
      if (e != null)
      {
        if (e.CommandName == "Cancel")
        {
          RedirectToList();
        }
      }
    }


    protected void TextBox_InsertDescription_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_InsertDescription = (TextBox)FormView_SystemEmailTemplate_Form.FindControl("TextBox_InsertDescription");
      Label Label_InsertDescriptionError = (Label)FormView_SystemEmailTemplate_Form.FindControl("Label_InsertDescriptionError");

      Session["SystemEmailTemplateDescription"] = "";
      string SQLStringSystemEmailTemplateDescription = "SELECT SystemEmailTemplate_Description FROM Administration_SystemEmailTemplate WHERE SystemEmailTemplate_Description = @SystemEmailTemplate_Description AND SystemEmailTemplate_IsActive = @SystemEmailTemplate_IsActive";
      using (SqlCommand SqlCommand_SystemEmailTemplateDescription = new SqlCommand(SQLStringSystemEmailTemplateDescription))
      {
        SqlCommand_SystemEmailTemplateDescription.Parameters.AddWithValue("@SystemEmailTemplate_Description", TextBox_InsertDescription.Text.ToString());
        SqlCommand_SystemEmailTemplateDescription.Parameters.AddWithValue("@SystemEmailTemplate_IsActive", 1);
        DataTable DataTable_SystemEmailTemplateDescription;
        using (DataTable_SystemEmailTemplateDescription = new DataTable())
        {
          DataTable_SystemEmailTemplateDescription.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemEmailTemplateDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemEmailTemplateDescription).Copy();
          if (DataTable_SystemEmailTemplateDescription.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemEmailTemplateDescription.Rows)
            {
              Session["SystemEmailTemplateDescription"] = DataRow_Row["SystemEmailTemplate_Description"];
            }
          }
          else
          {
            Session["SystemEmailTemplateDescription"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["SystemEmailTemplateDescription"].ToString()))
      {
        Label_InsertDescriptionError.Text = "";
      }
      else
      {
        Label_InsertDescriptionError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A System Email Template with the Description '" + Session["SystemEmailTemplateDescription"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
      }

      Session["SystemEmailTemplateDescription"] = "";
    }

    protected void TextBox_EditDescription_TextChanged(object sender, EventArgs e)
    {
      TextBox TextBox_EditDescription = (TextBox)FormView_SystemEmailTemplate_Form.FindControl("TextBox_EditDescription");
      Label Label_EditDescriptionError = (Label)FormView_SystemEmailTemplate_Form.FindControl("Label_EditDescriptionError");

      Session["SystemEmailTemplateId"] = "";
      Session["SystemEmailTemplateDescription"] = "";
      string SQLStringSystemEmailTemplateDescription = "SELECT SystemEmailTemplate_Id , SystemEmailTemplate_Description FROM Administration_SystemEmailTemplate WHERE SystemEmailTemplate_Description = @SystemEmailTemplate_Description AND SystemEmailTemplate_IsActive = @SystemEmailTemplate_IsActive";
      using (SqlCommand SqlCommand_SystemEmailTemplateDescription = new SqlCommand(SQLStringSystemEmailTemplateDescription))
      {
        SqlCommand_SystemEmailTemplateDescription.Parameters.AddWithValue("@SystemEmailTemplate_Description", TextBox_EditDescription.Text.ToString());
        SqlCommand_SystemEmailTemplateDescription.Parameters.AddWithValue("@SystemEmailTemplate_IsActive", 1);
        DataTable DataTable_SystemEmailTemplateDescription;
        using (DataTable_SystemEmailTemplateDescription = new DataTable())
        {
          DataTable_SystemEmailTemplateDescription.Locale = CultureInfo.CurrentCulture;
          DataTable_SystemEmailTemplateDescription = InfoQuestWCF.InfoQuest_DataInfoQuest.DataInfoQuest_SqlGetData(SqlCommand_SystemEmailTemplateDescription).Copy();
          if (DataTable_SystemEmailTemplateDescription.Rows.Count > 0)
          {
            foreach (DataRow DataRow_Row in DataTable_SystemEmailTemplateDescription.Rows)
            {
              Session["SystemEmailTemplateId"] = DataRow_Row["SystemEmailTemplate_Id"];
              Session["SystemEmailTemplateDescription"] = DataRow_Row["SystemEmailTemplate_Description"];
            }
          }
          else
          {
            Session["SystemEmailTemplateId"] = "";
            Session["SystemEmailTemplateDescription"] = "";
          }
        }
      }

      if (string.IsNullOrEmpty(Session["SystemEmailTemplateDescription"].ToString()))
      {
        Label_EditDescriptionError.Text = "";
      }
      else
      {
        if (Session["SystemEmailTemplateId"].ToString() == Request.QueryString["SystemEmailTemplate_Id"])
        {
          Label_EditDescriptionError.Text = "";
        }
        else
        {
          Label_EditDescriptionError.Text = Convert.ToString("<br/><div style='color:#B0262E;'>A System Email Template with the Description '" + Session["SystemEmailTemplateDescription"].ToString() + "' already exists</div>", CultureInfo.CurrentCulture);
        }
      }

      Session["SystemEmailTemplateId"] = "";
      Session["SystemEmailTemplateDescription"] = "";
    }


    protected void Button_EditUpdate_Click(object sender, EventArgs e)
    {
      Button_EditUpdateClicked = true;
    }
    //---END--- --Form--//
  }
}